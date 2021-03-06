﻿using System;
using System.IO;

using Anolis.Core.Data;
using Anolis.Core.Native;

using Cult    = System.Globalization.CultureInfo;
using Marshal = System.Runtime.InteropServices.Marshal;

namespace Anolis.Core.Source {
	
	/// <summary>Encapsulates a Windows Portal Executable resource source.</summary>
	public sealed class PEResourceSource : ResourceSource {
		
		private String   _path;
		private IntPtr   _moduleHandle;
		private ResourceSourceInfo _sourceInfo;
		
		public PEResourceSource(String filename, Boolean isReadOnly, ResourceSourceLoadMode mode) : base( isReadOnly || IsPathReadonly(filename), mode) {
			
			if( mode == ResourceSourceLoadMode.PreemptiveLoad ) throw new NotImplementedException("Support for preemptive data loading is not implemented yet");
			
			FileInfo = new FileInfo( _path = filename );
			if(!FileInfo.Exists) throw new FileNotFoundException("The specified Win32 PE Image was not found", filename);
			
			if(LoadMode > 0) Reload();
			
		}
		
		public override String Name {
			get { return Path.GetFileName( _path ); }
		}
		
		private static Boolean IsPathReadonly(String filename) {
			
			return ( File.GetAttributes(filename) & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly;
			
		}
		
		public override void CommitChanges() {
			
			if( IsReadOnly ) throw new InvalidOperationException("Changes cannot be commited because the current ResourceSource is read-only");
			
			// Unload self
			if(LoadMode > 0) Unload();
			
			IntPtr updateHandle = NativeMethods.BeginUpdateResource( _path, false );
			
			foreach(ResourceLang lang in AllActiveLangs) {
				
				IntPtr pData;
				Int32 length;
				
				if(lang.Action == ResourceDataAction.Delete) {
					
					// pData must be NULL to delete resource
					pData  = IntPtr.Zero;
					length = 0;
					
				} else {
					
					if( !lang.DataIsLoaded ) throw new AnolisException("Cannot perform action when ResourceData is not loaded");
					
					length = lang.Data.RawData.Length;
					pData  = Marshal.AllocHGlobal( length );
					
					Marshal.Copy( lang.Data.RawData, 0, pData, length );
				}
				
				IntPtr typeId = lang.Name.Type.Identifier.NativeId;
				IntPtr nameId = lang.Name.Identifier.NativeId;
				ushort langId = lang.LanguageId;
				
				NativeMethods.UpdateResource( updateHandle, typeId, nameId, langId, pData, length );
				
				Marshal.FreeHGlobal( pData );
				
				lang.Action = ResourceDataAction.None;
			}
			
			NativeMethods.EndUpdateResource(updateHandle, false);
			
			if(LoadMode > 0) Reload();
			
		}
		
		public override void Reload() {
			
			_moduleHandle = NativeMethods.LoadLibraryEx( _path, IntPtr.Zero, NativeMethods.LoadLibraryFlags.LoadLibraryAsDatafile );
			
			if( _moduleHandle == IntPtr.Zero ) {
				
				String win32Message = NativeMethods.GetLastErrorString();
				
				throw new ApplicationException("PE/COFF ResourceSource could not be loaded: " + win32Message);
			}
			
			PopulateResources();
			
		}
		
		private void Unload() {
			
			// HACK: should I dispose of all ResourceTypeIdentifiers and ResourceIdentifiers?
			
			if( _moduleHandle != IntPtr.Zero )
				NativeMethods.FreeLibrary( _moduleHandle );
		}
		
		public override ResourceData GetResourceData(ResourceLang lang) {
			
			if( lang.Name.Type.Source != this ) throw new ArgumentException("The specified ResourceLang does not exist in this ResourceSource");
			
			// TODO: Check that ResourceLang refers to a Resource that actually does exist in this resource and is not a pending add operation
			
			// use FindResourceEx and LoadResource to get a handle to the resource
			// use SizeOfResource to get the length of the byte array
			// then LockResource to get a pointer to it. Use Marshal to get a byte array and take it from there
			
			IntPtr resInfo = NativeMethods.FindResourceEx( _moduleHandle, lang.Name.Type.Identifier.NativeId, lang.Name.Identifier.NativeId, lang.LanguageId );
			IntPtr resData = NativeMethods.LoadResource  ( _moduleHandle, resInfo );
			Int32  size    = NativeMethods.SizeOfResource( _moduleHandle, resInfo );
			
			if(resData == IntPtr.Zero) return null;
			
			IntPtr resPtr  = NativeMethods.LockResource( resData ); // there is no method to unlock resources, but they should be freed anyway
			
			Byte[] data = new Byte[ size ];
			
			Marshal.Copy( resPtr, data, 0, size );
			
			NativeMethods.FreeResource( resData );
			
			ResourceData retval = ResourceData.FromResource(lang, data);
			
			return retval;
			
		}
		
		////////////////////////////
		// Useful implementation-specific bits
		
		public FileInfo FileInfo { get; private set; }
		
		public override ResourceSourceInfo SourceInfo {
			get {
				if(_sourceInfo == null) GetSourceInfo();
				return _sourceInfo;
			}
		}
		
		////////////////////////////
		// Destructor / Dispose
		
		protected override void Dispose(Boolean managed) {
			
			Unload();
			
			base.Dispose(managed);
		}
		
//		~PEResourceSource() {
//			Dispose(false);
//		}
//		
//		public override sealed void Dispose() {
//			Dispose(true);
//			GC.SuppressFinalize( this );
//		}
//		
//		public void Dispose(Boolean disposeManaged) {
//			Unload();
//		}
		
#region ResourceSourceInfo
		
		private void GetSourceInfo() {
			
			_sourceInfo = new ResourceSourceInfo();
			_sourceInfo.Add("Size"    , FileInfo.Length        .ToString(Cult.InvariantCulture));
			_sourceInfo.Add("Accessed", FileInfo.LastAccessTime.ToString(Cult.InvariantCulture));
			_sourceInfo.Add("Modified", FileInfo.LastWriteTime .ToString(Cult.InvariantCulture));
			_sourceInfo.Add("Created" , FileInfo.CreationTime  .ToString(Cult.InvariantCulture));
			
//			using(FileStream stream = FileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read)) {
//				
//				BinaryReader br = new BinaryReader(stream);
//				
//				// I CBA to work out how to read a COFF/PE header right now
//				
//			}
//			
//			_sourceInfo = null;
			
		}
		
#endregion
		
#region Resource Enumeration
		
		private Object _enumerating = new Object();
		
		private void PopulateResources() {
			
			lock(_enumerating) {
				
				if( Utility.Environment.IsGteVista ) {
					
					NativeMethods.EnumResTypeProc callback = new NativeMethods.EnumResTypeProc( GetResourceTypesCallbackEx );
					NativeMethods.EnumResourceTypesEx( _moduleHandle, callback, IntPtr.Zero, NativeMethods.MuiResourceFlags.EnumLn, 0 );
					
				} else {
					
					NativeMethods.EnumResTypeProc callback = new NativeMethods.EnumResTypeProc( GetResourceTypesCallback );
					NativeMethods.EnumResourceTypes( _moduleHandle, callback, IntPtr.Zero);
					
				}
				
				
			}
			
		}
		
		private Boolean GetResourceTypesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr userParam) {
			
			ResourceType type = new ResourceType( pType, this );
			
			UnderlyingAdd( _currentType = type );
			
			// enumerate all resources for that type
			NativeMethods.EnumResNameProc callback = new NativeMethods.EnumResNameProc( GetResourceNamesCallback );
			NativeMethods.EnumResourceNames( moduleHandle, pType, callback, IntPtr.Zero );
			
			return true;
			
		}
		
		private ResourceType _currentType;
		
		private Boolean GetResourceNamesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr pName, IntPtr userParam) {
			
			ResourceType type = _currentType;
			
			ResourceName name = new ResourceName( pName, type );
			
			UnderlyingAdd( type, _currentName = name );
			
			NativeMethods.EnumResLangProc callback = new NativeMethods.EnumResLangProc( GetResourceLanguagesCallback );
			NativeMethods.EnumResourceLanguages(moduleHandle, pType, pName, callback, IntPtr.Zero );
			
			return true;
		}
		
		private ResourceName _currentName;
		
		private Boolean GetResourceLanguagesCallback(IntPtr moduleHandle, IntPtr pType, IntPtr pName, UInt16 langId, IntPtr userParam) {
			
			ResourceName name = _currentName;
			
			ResourceLang lang = new ResourceLang( langId, name );
			
			UnderlyingAdd( name, lang );
			
			return true;
		}
		
	#region Resource Enumeration Ex
		
		// I could just branch within the callbacks, but I want to accomodate Vista separately
		
		private Boolean GetResourceTypesCallbackEx(IntPtr moduleHandle, IntPtr pType, IntPtr userParam) {
			
			ResourceType type = new ResourceType( pType, this );
			
			UnderlyingAdd( _currentType = type );
			
			// enumerate all resources for that type
			NativeMethods.EnumResNameProc callback = new NativeMethods.EnumResNameProc( GetResourceNamesCallbackEx );
			NativeMethods.EnumResourceNamesEx( moduleHandle, pType, callback, IntPtr.Zero, NativeMethods.MuiResourceFlags.EnumLn, 0 );
			
			return true;
			
		}
		
		private Boolean GetResourceNamesCallbackEx(IntPtr moduleHandle, IntPtr pType, IntPtr pName, IntPtr userParam) {
			
			ResourceType type = _currentType;
			
			ResourceName name = new ResourceName( pName, type );
			
			UnderlyingAdd( type, _currentName = name );
			
			NativeMethods.EnumResLangProc callback = new NativeMethods.EnumResLangProc( GetResourceLanguagesCallbackEx );
			NativeMethods.EnumResourceLanguagesEx(moduleHandle, pType, pName, callback, IntPtr.Zero, NativeMethods.MuiResourceFlags.EnumLn, 0 );
			
			return true;
		}
		
		private Boolean GetResourceLanguagesCallbackEx(IntPtr moduleHandle, IntPtr pType, IntPtr pName, UInt16 langId, IntPtr userParam) {
			
			ResourceName name = _currentName;
			
			ResourceLang lang = new ResourceLang( langId, name );
			
			UnderlyingAdd( name, lang );
			
			return true;
		}
		
	#endregion
		
#endregion
		
#region Resource Updates
		
		
		
#endregion
		
	}
}
