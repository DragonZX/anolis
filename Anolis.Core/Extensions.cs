﻿using System;
using XmlElement = System.Xml.XmlElement;
using System.IO;
using System.Text;

// Extension methods seem to require System.Core.dll, which is not in .NET2.0
// so here's an ersatz Extension attribute class

namespace System.Runtime.CompilerServices {
	
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class ExtensionAttribute : Attribute {
		
		public ExtensionAttribute() {
		}
		
	}
	
}

namespace Anolis.Core {
	
	public static class Extensions {
		
		public static Byte[] SubArray(this Byte[] array, Int32 startIndex, Int32 length) {
			
			if(startIndex >= array.Length         ) throw new ArgumentOutOfRangeException("startIndex");
			if(startIndex + length >= array.Length) throw new ArgumentOutOfRangeException("length");
			
			Byte[] retval = new Byte[ length ];
			
			for(int i=0;i<length;i++) retval[i] = array[startIndex + i];
			
			return retval;
			
		}
		
		public static String Left(this String str, Int32 length) {
			
			if(length < 0)          throw new ArgumentOutOfRangeException("length", length, "value cannot be less than zero");
			if(length > str.Length) throw new ArgumentOutOfRangeException("length", length, "value cannot be greater than the length of the string");
			
			return str.Substring(0, length);
			
		}
		
		public static String Right(this String str, Int32 length) {
			
			if(length < 0)          throw new ArgumentOutOfRangeException("length", length, "value cannot be less than zero");
			if(length > str.Length) throw new ArgumentOutOfRangeException("length", length, "value cannot be greater than the length of the string");
			
			return str.Substring( str.Length - length );
			
		}
		
		public static Int32 IndexOf(this Array array, Object item) {
			
			return Array.IndexOf( array, item );
			
		}
		
#region Binary Reader
		
		/// <summary>Reads a null-terminated string.</summary>
		public static String ReadSZString(this BinaryReader rdr) {
			
			StringBuilder sb = new StringBuilder();
			Char c;
			while( (c = rdr.ReadChar()) != 0 ) {
				sb.Append( c );
			}
			
			return sb.ToString();
		}
		
		public static Byte[] Align2(this BinaryReader rdr) {
			
/*			Int64 pos = rdr.BaseStream.Position;
			pos = (pos + 1) & ~1;
			
			Int64 offset = pos - rdr.BaseStream.Position;
			
			return rdr.ReadBytes( (int)offset );*/
			
			Int64 pos = rdr.BaseStream.Position;
			Int32 rem = (int)(pos % 2L);
			
			return rdr.ReadBytes( rem );
		}
		
		public static Byte[] Align4(this BinaryReader rdr) {
			
			Int64 pos = rdr.BaseStream.Position;
			pos = (pos + 3) & ~3;
			
			Int64 offset = pos - rdr.BaseStream.Position;
			
			return rdr.ReadBytes( (int)offset );
		}
		
		public static Object ReadIdentifier(this BinaryReader rdr) {
			
			UInt16 word0 = rdr.ReadUInt16();
			if(word0 == 0x0000) return null;
			if(word0 == 0xFFFF) {
				// then the next UInt16 is a numeric identifier
				
				return rdr.ReadUInt16();
				
			} else {
				// then word0 is the first character in a null-terminated string
				// so concatenate it with the string it's about to read in
				
				Char c0 = (Char)word0;
				return c0 + rdr.ReadSZString();
				
			}
			
		}
		
#endregion
		
#region BinaryWriter
		
		public static void WriteSZString(this BinaryWriter wtr, String s) {
			
			Char[] chars = s.ToCharArray();
			
			wtr.Write( chars );
			wtr.Write( 0x00 );
			
		}
		
		public static void Align2(this BinaryWriter wtr) {
			
			Int64 pos = wtr.BaseStream.Position;
			Int32 rem = (int)(pos % 2L);
			
			Byte[] writeThis = new Byte[rem];
			writeThis.Initialize();
			
			wtr.Write( writeThis );
			
		}
		
		public static void Align4(this BinaryWriter wtr) {
			
			Int64 pos = wtr.BaseStream.Position;
			pos = (pos + 3) & ~3;
			
			Int64 rem = pos - wtr.BaseStream.Position;
			
			Byte[] writeThis = new Byte[rem];
			writeThis.Initialize();
			
			wtr.Write( writeThis );
			
		}
		
#endregion
		
	}
}
