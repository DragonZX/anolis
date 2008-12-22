﻿using System;
using System.Drawing;
using System.Text;

using Stream = System.IO.Stream;
using Anolis.Core.Utility;

namespace Anolis.Core.Data {
	
	public class BmpImageResourceDataFactory : ResourceDataFactory {
		
		public override Compatibility HandlesType(ResourceTypeIdentifier type) {
			
			if( type.KnownType == Win32ResourceType.Bitmap ) return Compatibility.Yes;
			if( type.KnownType == Win32ResourceType.IconImage ) return Compatibility.Maybe;
			if( type.KnownType == Win32ResourceType.CursorImage ) return Compatibility.Maybe;
			
			if( type.KnownType != Win32ResourceType.Custom ) return Compatibility.No;
			
			return Compatibility.Maybe;
			
		}
		
		public override Compatibility HandlesExtension(String filenameExtension) {
			
			// TODO: Sort this out...
			switch(filenameExtension) {
				case "bmp":
				case "jpeg":
				case "jpg":
				case "png":
				case "gif":
				case "dib":
					return Compatibility.Yes;
			}
			
			return Compatibility.No;
			
		}
		
		public override ResourceData FromFile(Stream stream, String extension) {
			
			Byte[] fileData = GetAllBytesFromStream(stream);
			
			return FromResource(null, fileData);
			
		}
		
		public override ResourceData FromResource(ResourceLang lang, byte[] data) {
			
			BmpImageResourceData rd;
			
			if( BmpImageResourceData.TryCreate(null, data, out rd) ) return rd;
			
			return null;
			
		}
		
		public override String Name {
			get { return "Bitmap"; }
		}
	}
	
	public sealed class BmpImageResourceData : ImageResourceData {
		
		private FileDib _dib;
		
		private BmpImageResourceData(FileDib dib, Image image, ResourceLang lang, Byte[] rawData) : base(image, lang, rawData) {
			_dib = dib;
		}
		
		internal static Boolean TryCreate(ResourceLang lang, Byte[] rawData, out BmpImageResourceData typed) {
			
			// check if the data is of the right format before working with it
			
			FileDib dib = new FileDib( rawData );
			
			Bitmap bmp;
			
			if(!dib.TryToBitmap(out bmp)) {
				typed = null;
				return false;
			}
			
			typed = new BmpImageResourceData(dib, bmp, lang, rawData);
			
			return true;
			
		}
		
		public override void SaveAs(Stream stream, String extension) {
			
			// don't use Image.Save since we want to save it without any added .NET Image class nonsense, and preserve 32-bit BMPs
			
			if(extension == "bmp") {
				
				Byte[] bitmapFileData = _dib.Data;
				
				stream.Write( bitmapFileData, 0, bitmapFileData.Length );
				
			} else {
				base.SaveAs(stream, extension); // assuming ImageResourceData handles other extensions
			}
			
		}
		
		public override String[] SaveFileFilter {
			get { return new String[] { "BMP Image (*.bmp)|*.bmp" }; }
		}
		
	}
	
}