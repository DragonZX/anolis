﻿#define UNIONS

using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using Anolis.Core.Data;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Anolis.Core.Native {
	
	// word  = ushort = UInt16
	// dword = uint   = UInt32
	// long  = int    = Int32

#region Bitmaps
	
	namespace Gdi {
		
		/// <remarks>OS/2 DIB Header. bcBitCount can be one of 1, 4, 8, or 24</remarks>
		[StructLayout(LayoutKind.Sequential, Pack=2)]
		internal struct BitmapCoreHeader {
			public UInt32 bcSize;
			public UInt16 bcWidth;
			public UInt16 bcHeight;
			public UInt16 bcPlanes;
			public UInt16 bcBitCount;
			
			public BitmapCoreHeader(BinaryReader rdr) {
				bcSize     = rdr.ReadUInt32();
				bcWidth    = rdr.ReadUInt16();
				bcHeight   = rdr.ReadUInt16();
				bcPlanes   = rdr.ReadUInt16();
				bcBitCount = rdr.ReadUInt16();
			}
		}
		
		/// <remarks>Windows 3.x DIB Header</remarks>
		[StructLayout(LayoutKind.Sequential, Pack=2)]
		internal struct BitmapInfoHeader {
			
			public UInt32 biSize;
			public Int32  biWidth;
			public Int32  biHeight;
			public UInt16 biPlanes;
			public UInt16 biBitCount; // Core end
			public BiCompression biCompression;
			public UInt32 biSizeImage;
			public Int32  biXPelsPerMeter;
			public Int32  biYPelsPerMeter;
			public UInt32 biClrUsed;
			public UInt32 biClrImportant;
			
			public BitmapInfoHeader(BinaryReader rdr) {
				biSize          = rdr.ReadUInt32();
				biWidth         = rdr.ReadInt32();
				biHeight        = rdr.ReadInt32();
				biPlanes        = rdr.ReadUInt16();
				biBitCount      = rdr.ReadUInt16();
				biCompression   = (BiCompression)rdr.ReadUInt32();
				biSizeImage     = rdr.ReadUInt32();
				biXPelsPerMeter = rdr.ReadInt32();
				biYPelsPerMeter = rdr.ReadInt32();
				biClrUsed       = rdr.ReadUInt32();
				biClrImportant  = rdr.ReadUInt32();
			}
		}
		
		/// <remarks>Windows 95/ NT 4.0 DIB Header</remarks>
		[StructLayout(LayoutKind.Sequential,Pack=2)]
		internal struct BitmapV4Header {
			
			public UInt32 bV4Size;
			public Int32  bV4Width;
			public Int32  bV4Height;
			public UInt16 bV4Planes;
			public UInt16 bV4BitCount; // Core end
			public BiCompression bV4Compression;
			public UInt32 bV4SizeImage;
			public Int32  bV4XPelsPerMeter;
			public Int32  bV4YPelsPerMeter;
			public UInt32 bV4ClrUsed;
			public UInt32 bV4ClrImportant; // V3 end
			public UInt32 bV4RedMask;
			public UInt32 bV4GreenMask;
			public UInt32 bV4BlueMask;
			public UInt32 bV4AlphaMask;
			public UInt32 bV4CSType;
			public CieXyzTriple bV4Endpoints;
			public UInt32 bV4GammaRed;
			public UInt32 bV4GammaGreen;
			public UInt32 bV4GammaBlue;
			
			public BitmapV4Header(BinaryReader rdr) {
				bV4Size          = rdr.ReadUInt32();
				bV4Width         = rdr.ReadInt32();
				bV4Height        = rdr.ReadInt32();
				bV4Planes        = rdr.ReadUInt16();
				bV4BitCount      = rdr.ReadUInt16();
				bV4Compression   = (BiCompression)rdr.ReadUInt32();
				bV4SizeImage     = rdr.ReadUInt32();
				bV4XPelsPerMeter = rdr.ReadInt32();
				bV4YPelsPerMeter = rdr.ReadInt32();
				bV4ClrUsed       = rdr.ReadUInt32();
				bV4ClrImportant  = rdr.ReadUInt32();
				bV4RedMask       = rdr.ReadUInt32();
				bV4GreenMask     = rdr.ReadUInt32();
				bV4BlueMask      = rdr.ReadUInt32();
				bV4AlphaMask     = rdr.ReadUInt32();
				bV4CSType        = rdr.ReadUInt32();
				bV4Endpoints     = new CieXyzTriple(rdr);
				bV4GammaRed      = rdr.ReadUInt32();
				bV4GammaGreen    = rdr.ReadUInt32();
				bV4GammaBlue     = rdr.ReadUInt32();
			}
		}
		
		/// <remarks>Windows 98 / 2000 DIB Header</remarks>
		[StructLayout(LayoutKind.Sequential,Pack=2)]
		internal struct BitmapV5Header {
			
			public UInt32 bV5Size;
			public Int32  bV5Width;
			public Int32  bV5Height;
			public UInt16 bV5Planes;
			public UInt16 bV5BitCount; // Core end
			public BiCompression bV5Compression;
			public UInt32 bV5SizeImage;
			public Int32  bV5XPelsPerMeter;
			public Int32  bV5YPelsPerMeter;
			public UInt32 bV5ClrUsed;
			public UInt32 bV5ClrImportant; // V3 end
			public UInt32 bV5RedMask;
			public UInt32 bV5GreenMask;
			public UInt32 bV5BlueMask;
			public UInt32 bV5AlphaMask;
			public UInt32 bV5CSType;
			public CieXyzTriple bV5Endpoints;
			public UInt32 bV5GammaRed;
			public UInt32 bV5GammaGreen;
			public UInt32 bV5GammaBlue; // V4 end
			public UInt32 bV5Intent;
			public UInt32 bV5ProfileData;
			public UInt32 bV5ProfileSize;
			public UInt32 bV5Reserved;
			
			public BitmapV5Header(BinaryReader rdr) {
				bV5Size          = rdr.ReadUInt32();
				bV5Width         = rdr.ReadInt32();
				bV5Height        = rdr.ReadInt32();
				bV5Planes        = rdr.ReadUInt16();
				bV5BitCount      = rdr.ReadUInt16();
				bV5Compression   = (BiCompression)rdr.ReadUInt32();
				bV5SizeImage     = rdr.ReadUInt32();
				bV5XPelsPerMeter = rdr.ReadInt32();
				bV5YPelsPerMeter = rdr.ReadInt32();
				bV5ClrUsed       = rdr.ReadUInt32();
				bV5ClrImportant  = rdr.ReadUInt32();
				bV5RedMask       = rdr.ReadUInt32();
				bV5GreenMask     = rdr.ReadUInt32();
				bV5BlueMask      = rdr.ReadUInt32();
				bV5AlphaMask     = rdr.ReadUInt32();
				bV5CSType        = rdr.ReadUInt32();
				bV5Endpoints     = new CieXyzTriple(rdr);
				bV5GammaRed      = rdr.ReadUInt32();
				bV5GammaGreen    = rdr.ReadUInt32();
				bV5GammaBlue     = rdr.ReadUInt32();
				bV5Intent        = rdr.ReadUInt32();
				bV5ProfileData   = rdr.ReadUInt32();
				bV5ProfileSize   = rdr.ReadUInt32();
				bV5Reserved      = rdr.ReadUInt32();
			}
			
		}
		
		[StructLayout(LayoutKind.Sequential,Pack=2)]
		internal struct CieXyzTriple {
			public CieXyz cieXyzRed;
			public CieXyz cieXyzGreen;
			public CieXyz cieXyzBlue;
			
			public CieXyzTriple(BinaryReader rdr) {
				cieXyzRed   = new CieXyz(rdr);
				cieXyzGreen = new CieXyz(rdr);
				cieXyzBlue  = new CieXyz(rdr);
			}
		}
		
		[StructLayout(LayoutKind.Sequential,Pack=2)]
		internal struct CieXyz {
			// WinGdi.h:
			// typedef long            FXPT2DOT30, FAR *LPFXPT2DOT30;
			//   FXPT2DOT30 ciexyzX;
			public Int32 cieXyzX;
			public Int32 cieXyzY;
			public Int32 cieXyzZ;
			
			public CieXyz(BinaryReader rdr) {
				cieXyzX = rdr.ReadInt32();
				cieXyzY = rdr.ReadInt32();
				cieXyzZ = rdr.ReadInt32();
			}
		}
		
		[StructLayout(LayoutKind.Sequential, Pack=2)]
		internal struct BitmapFileHeader { 
			public UInt16 bfType;
			public UInt32 bfSize;
			public UInt16 bfReserved1;
			public UInt16 bfReserved2;
			public UInt32 bfOffBits;
			
			public BitmapFileHeader(BinaryReader rdr) {
				
				bfType      = rdr.ReadUInt16();
				bfSize      = rdr.ReadUInt32();
				bfReserved1 = rdr.ReadUInt16();
				bfReserved2 = rdr.ReadUInt16();
				bfOffBits   = rdr.ReadUInt32();
			}
			
			public void Write(BinaryWriter wtr) {
				wtr.Write( bfType );
				wtr.Write( bfSize );
				wtr.Write( bfReserved1 );
				wtr.Write( bfReserved2 );
				wtr.Write( bfOffBits );
			}
			
		}
		
		internal enum BiCompression : uint {
			BiRgb       = 0,
			BiRle8      = 1,
			BiRle4      = 2,
			BiBitFields = 3,
			BiJpeg      = 4,
			BiPng       = 5
		}
		
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal struct RgbQuad {
			public byte rgbBlue;
			public byte rgbGreen;
			public byte rgbRed;
			public byte rgbReserved;
		}
		
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		internal struct RgbTriple {
			public byte rgbBlue;
			public byte rgbGreen;
			public byte rgbRed;
		}
	
	}
	
#endregion
	
#region Icons and Cursors
	
	/// <summary>Defines the memory layout of a RT_GROUP_ICON Win32 resource or *.ico file.</summary>
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct IconDirectory {
		
		public ushort wReserved;
		public ushort wType;
		public ushort wCount;
		
		public const Int32 Size = 6;
		
		public IconDirectory(BinaryReader rdr) {
			
			wReserved = rdr.ReadUInt16();
			wType     = rdr.ReadUInt16();
			wCount    = rdr.ReadUInt16();
		}
		
		public void Write(BinaryWriter wtr) {
			
			wtr.Write( wReserved );
			wtr.Write( wType );
			wtr.Write( wCount );
		}
	}
	
	/// <summary>Helps to defines the memory layout of a RT_GROUP_ICON resource. In particular its wId member indicates the RT_ICON resource for the directory entry.</summary>
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	internal struct ResIconDirectoryEntry {
		public byte  bWidth;
		public byte  bHeight;
		public byte  bColorCount;
		public byte  bReserved;
		
		public ushort wPlanes;
		public ushort wBitCount;
		
		public uint   dwBytesInRes;
		public ushort wId;
		
		public const Int32 Size = 14;
		
		public ResIconDirectoryEntry(BinaryReader rdr) {
			
			bWidth      = rdr.ReadByte();
			bHeight     = rdr.ReadByte();
			bColorCount = rdr.ReadByte();
			bReserved   = rdr.ReadByte();
			
			wPlanes     = rdr.ReadUInt16(); // also XHotspot
			wBitCount   = rdr.ReadUInt16(); // also YHotspot
			
			dwBytesInRes = rdr.ReadUInt32();
			wId          = rdr.ReadUInt16();
		}
		
		public void Write(BinaryWriter wtr) {
			
			wtr.Write( bWidth );
			wtr.Write( bHeight );
			wtr.Write( bColorCount );
			wtr.Write( bReserved );
			
			wtr.Write( wPlanes );
			wtr.Write( wBitCount );
			
			wtr.Write( dwBytesInRes );
			wtr.Write( wId );
		}
		
	}
	
	/// <summary>Defines the peristent format of an icon directory entry in a .ICO file.</summary>
	[StructLayout(LayoutKind.Explicit)]
	internal struct FileIconDirectoryEntry {
		[FieldOffset( 0)] public byte   bWidth;
		[FieldOffset( 1)] public byte   bHeight;
		[FieldOffset( 2)] public byte   bColorCount; // Number of colors in image (0 if >=8bpp)
		[FieldOffset( 3)] public byte   bReserved;
		
		[FieldOffset( 4)] public ushort wPlanes; // Icon-specific
		[FieldOffset( 6)] public ushort wBitCount;
		[FieldOffset( 4)] public ushort wXHotspot; // Cursor-specific
		[FieldOffset( 6)] public ushort wYHotspot;
		
		[FieldOffset( 8)] public uint   dwBytesInRes;
		[FieldOffset(12)] public uint   dwImageOffset;
		
		public const Int32 Size = 16;
		
		public FileIconDirectoryEntry(BinaryReader rdr) {
			
			wXHotspot = 0;
			wYHotspot = 0;
			
			bWidth        = rdr.ReadByte();
			bHeight       = rdr.ReadByte();
			bColorCount   = rdr.ReadByte();
			bReserved     = rdr.ReadByte();
			
			wPlanes       = rdr.ReadUInt16(); // also XHotspot
			wBitCount     = rdr.ReadUInt16(); // also YHotspot
			
			dwBytesInRes  = rdr.ReadUInt32();
			dwImageOffset = rdr.ReadUInt32();
		}
		
		public void Write(BinaryWriter wtr) {
			
			wtr.Write( bWidth );
			wtr.Write( bHeight );
			wtr.Write( bColorCount );
			wtr.Write( bReserved );
			
			wtr.Write( wPlanes );   // also XHotspot
			wtr.Write( wBitCount ); // also YHotspot
			
			wtr.Write( dwBytesInRes );
			wtr.Write( dwImageOffset );
		}
	}

#endregion
	
#region Version
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct VSVersionInfo {
		public UInt16 wLength;
		public UInt16 wValueLength;
		public UInt16 wType;
		public String szKey;
		public Byte[] Padding1;
		public VSFixedFileInfo Value;
		public Byte[] Padding2;
		public UInt16[] Children;
		
//		public VSVersionInfo(BinaryReader rdr) {
//			wLength      = rdr.ReadUInt16();
//			wValueLength = rdr.ReadUInt16();
//			wType        = rdr.ReadUInt16();
//			szKey        = System.Text.Encoding.Unicode.GetString( rdr.ReadBytes( "VS_VERSION_INFO".Length * 2 ) );
//			
//		}
	}

#if UNIONS
	
	[StructLayout(LayoutKind.Explicit, Pack=2)]
	internal struct VSFixedFileInfo {
		[FieldOffset(0x00)] public UInt32 dwSignature;
		[FieldOffset(0x04)] public UInt32 dwStrucVersion;
		[FieldOffset(0x08)] public UInt32 dwFileVersionMS;
		[FieldOffset(0x0C)] public UInt32 dwFileVersionLS;
		[FieldOffset(0x10)] public UInt32 dwProductVersionMS; 
		[FieldOffset(0x14)] public UInt32 dwProductVersionLS; 
		[FieldOffset(0x18)] public UInt32 dwFileFlagsMask; 
		[FieldOffset(0x1C)] public VSFixedFileFlags dwFileFlags; 
		[FieldOffset(0x20)] public VSFixedFileOS    dwFileOS; 
		[FieldOffset(0x24)] public VSFixedFileType  dwFileType;
		[FieldOffset(0x28)] public UInt32 dwFileSubType;
		[FieldOffset(0x28)] public VSFixedFileSubTypeDriver dwFileSubTypeDriver;
		[FieldOffset(0x28)] public VSFixedFileSubTypeFont   dwFileSubTypeFont;
		[FieldOffset(0x2C)] public UInt32 dwFileDateMS; 
		[FieldOffset(0x30)] public UInt32 dwFileDateLS;
		
		public VSFixedFileInfo(BinaryReader rdr) {
			
			dwFileSubTypeDriver = 0;
			dwFileSubTypeFont   = 0;
			
			dwSignature        = rdr.ReadUInt32();
			dwStrucVersion     = rdr.ReadUInt32();
			dwFileVersionMS    = rdr.ReadUInt32();
			dwFileVersionLS    = rdr.ReadUInt32();
			dwProductVersionMS = rdr.ReadUInt32();
			dwProductVersionLS = rdr.ReadUInt32();
			dwFileFlagsMask    = rdr.ReadUInt32();
			dwFileFlags        = (VSFixedFileFlags)rdr.ReadUInt32();
			dwFileOS           = (VSFixedFileOS)   rdr.ReadUInt32();
			dwFileType         = (VSFixedFileType) rdr.ReadUInt32();
			dwFileSubType      = rdr.ReadUInt32();
			dwFileDateMS       = rdr.ReadUInt32();
			dwFileDateLS       = rdr.ReadUInt32();
		}
		
		public VSFixedFileInfo(Byte[] data) {
			
			if(data.Length < 0x34) throw new ArgumentException("data needs to be at least 0x34 bytes long", "data");
			
			dwFileSubTypeDriver = 0;
			dwFileSubTypeFont   = 0;
			
			dwSignature        = ReadUInt32(data, 0x00);
			dwStrucVersion     = ReadUInt32(data, 0x04);
			dwFileVersionMS    = ReadUInt32(data, 0x08);
			dwFileVersionLS    = ReadUInt32(data, 0x0C);
			dwProductVersionMS = ReadUInt32(data, 0x10);
			dwProductVersionLS = ReadUInt32(data, 0x14);
			dwFileFlagsMask    = ReadUInt32(data, 0x18);
			dwFileFlags        = (VSFixedFileFlags)ReadUInt32(data, 0x1C);
			dwFileOS           = (VSFixedFileOS)   ReadUInt32(data, 0x20);
			dwFileType         = (VSFixedFileType) ReadUInt32(data, 0x24);
			dwFileSubType      = ReadUInt32(data, 0x28);
			dwFileDateMS       = ReadUInt32(data, 0x2C);
			dwFileDateLS       = ReadUInt32(data, 0x30);
			
		}
		
		private static UInt32 ReadUInt32(Byte[] data, Int32 offset) {
			
			 return (UInt32)(((data[offset] | (data[offset + 1] << 8)) | (data[offset + 2] << 0x10)) | (data[offset + 3] << 0x18));
			
		}
		
	}
#else
	
	[StructLayout(LayoutKind.Sequential, Pack=2)]
	internal struct VsFixedFileInfo {
		public UInt32 dwSignature;
		public UInt32 dwStrucVersion;
		public UInt32 dwFileVersionMS;
		public UInt32 dwFileVersionLS;
		public UInt32 dwProductVersionMS; 
		public UInt32 dwProductVersionLS; 
		public UInt32 dwFileFlagsMask; 
		public UInt32 dwFileFlags; 
		public UInt32 dwFileOS; 
		public UInt32 dwFileType; 
		public UInt32 dwFileSubType;
		public UInt32 dwFileDateMS; 
		public UInt32 dwFileDateLS;
		
		public VsFixedFileInfo(BinaryReader rdr) {
			dwSignature        = rdr.ReadUInt32();
			dwStrucVersion     = rdr.ReadUInt32();
			dwFileVersionMS    = rdr.ReadUInt32();
			dwFileVersionLS    = rdr.ReadUInt32();
			dwProductVersionMS = rdr.ReadUInt32();
			dwProductVersionLS = rdr.ReadUInt32();
			dwFileFlagsMask    = rdr.ReadUInt32();
			dwFileFlags        = rdr.ReadUInt32();
			dwFileOS           = rdr.ReadUInt32();
			dwFileType         = rdr.ReadUInt32();
			dwFileSubType      = rdr.ReadUInt32();
			dwFileDateMS       = rdr.ReadUInt32();
			dwFileDateLS       = rdr.ReadUInt32();
		}
	}
#endif
	
	[Flags]
	internal enum VSFixedFileFlags : uint {
		Debug         = 0x01,
		InfoInferred  = 0x02,
		Patched       = 0x04,
		Prerelease    = 0x08,
		PrivateBuild  = 0x10,
		SpecialBuild  = 0x20
	}
	
	[Flags]
	internal enum VSFixedFileOS : uint {
		Unknown = 0x00000000,
		DOS     = 0x00010000,
		OS216   = 0x00020000,
		OS232   = 0x00030000,
		WinNT   = 0x00040000,
		WinCE   = 0x00050000,
		
		Win16   = 0x00000001,
		PM16    = 0x00000002,
		PM32    = 0x00000003,
		Win32   = 0x00000004
	}
	
	internal enum VSFixedFileType : uint {
		Unknown     = 0x0,
		Application = 0x1,
		Dll         = 0x2,
		Driver      = 0x3,
		Font        = 0x4,
		Vxd         = 0x5,
		StaticLib   = 0x7
	}
	
	internal enum VSFixedFileSubTypeDriver : uint {
		Unknown                  = 0x0,
		// Driver specific
		
		DriverComm               = 0xA,
		DriverPrinter            = 0x1,
		DriverKeyboard           = 0x2,
		DriverLanguage           = 0x3,
		DriverDisplay            = 0x4,
		DriverMouse              = 0x5,
		DriverNetwork            = 0x6,
		DriverSystem             = 0x7,
		DriverInstallable        = 0x8,
		DriverSound              = 0x9,
		DriverInputMethod        = 0xB,
		DriverVersionedPrinter   = 0xC
	}
	
	internal enum VSFixedFileSubTypeFont : uint {
		FontRaster               = 0x1,
		FontVector               = 0x2,
		FontTrueType             = 0x3
	}
	
#endregion
	
#region System Info
	
	internal struct SystemInfo {
		
		public ProcessorArchitecture wProcessorArchitecture;
		public UInt16 wReserved;
		public UInt32 dwPageSize;
		public IntPtr lpMinimumApplicationAddress;
		public IntPtr lpMaximumApplicationAddress;
		public UInt32 dwActiveProcessorMask;
		public UInt32 dwNumberOfProcessors;
		public ProcessorType dwProcessorType;
		public UInt32 dwAllocationGranularity;
		public UInt16 wProcessorLevel;
		public UInt16 wProcessorRevision;
		
	}
	
	internal enum ProcessorArchitecture : ushort { // WORD
		Amd64   = 9,
		IA32    = 6,
		IA64    = 0,
		Unknown = 0xFFFF
	}
	
	internal enum ProcessorType : uint { // DWORD
		i386    = 386,
		i486    = 486,
		Pentium = 586,
		IA64    = 2200,
		X8664   = 8664
	}
	
#endregion
	
#region Dialogs
	
	[StructLayout(LayoutKind.Sequential,Pack=1)]
	internal struct DlgTemplate {
		public UInt32 style;
		public UInt32 dwExtendedStyle;
		public UInt16 cdit;
		public Int16  x;
		public Int16  y;
		public Int16  cx;
		public Int16  cy;
		
		public Object exMenu;
		public Object exWindowClass;
		public String exTitle;
		
		public UInt16 exfSize;
		public String exfTypeface;
		
		public DlgTemplate(BinaryReader rdr) {
			style           = rdr.ReadUInt32();
			dwExtendedStyle = rdr.ReadUInt32();
			cdit            = rdr.ReadUInt16();
			x               = rdr.ReadInt16();
			y               = rdr.ReadInt16();
			cx              = rdr.ReadInt16();
			cy              = rdr.ReadInt16();
			
			// there's a bunch stuff in the Remarks section that's painful, yet similar to DlgTemplateEx
			// "the DLGTEMPLATE structure is always immediately followed by three variable-length arrays that specify the menu, class, and title for the dialog box."
			
			exMenu        = rdr.ReadIdentifier();
			exWindowClass = rdr.ReadIdentifier();
			exTitle       = rdr.ReadSZString();
			
			if( (style & 0x40) == 0x40 ) {
				
				exfSize     = rdr.ReadUInt16();
				exfTypeface = rdr.ReadSZString();
			} else {
				
				exfSize     = 0;
				exfTypeface = null;
			}
		}
	}
	
	[StructLayout(LayoutKind.Sequential,Pack=2)]
	internal struct DlgItemTemplate {
		public UInt32 style;
		public UInt32 dwExtendedStyle;
		public Int16  x;
		public Int16  y;
		public Int16  cx;
		public Int16  cy;
		public UInt16 id;
		
		public Object exWindowClass;
		public Object exTitle;
		public Byte[] exCreationData;
		
		public DlgItemTemplate(BinaryReader rdr) {
			style           = rdr.ReadUInt32();
			dwExtendedStyle = rdr.ReadUInt32();
			x               = rdr.ReadInt16();
			y               = rdr.ReadInt16();
			cx              = rdr.ReadInt16();
			cy              = rdr.ReadInt16();
			id              = rdr.ReadUInt16();
			
			// as with DlgTemplate there exists trailing data
			// "the DLGITEMTEMPLATE structure is always immediately followed by three variable-length arrays specifying the class, title, and creation data for the control. Each array consists of one or more 16-bit elements."
			
			exWindowClass  = rdr.ReadIdentifier();
			exTitle        = rdr.ReadIdentifier();
			exCreationData = null;
			
			rdr.Align2();
			
			UInt16 creationSize = rdr.ReadUInt16();
			if(creationSize > 0) {
				exCreationData = rdr.ReadBytes( creationSize - 2 );
				// creationSize's value includes the 2 bytes in the first word containing the size
			}
		}
	}
	
	[StructLayout(LayoutKind.Sequential,Pack=1)]
	internal struct DlgTemplateEx {
		public UInt16 dlgVer;
		public UInt16 signature;
		public UInt32 helpId;
		public UInt32 exStyle;
		public UInt32 style;
		public UInt16 cDlgItems;
		public Int16  x;
		public Int16  y;
		public Int16  cx;
		public Int16  cy;
		public Object menu;
		public Object windowClass;
		public String title;
		public UInt16 pointSize;
		public UInt16 weight;
		public Byte   italic;
		public Byte   charset;
		public String typeface;
		
		public DlgTemplateEx(BinaryReader rdr) {
			dlgVer    = rdr.ReadUInt16();
			signature = rdr.ReadUInt16();
			helpId    = rdr.ReadUInt32();
			exStyle   = rdr.ReadUInt32();
			style     = rdr.ReadUInt32();
			
			cDlgItems = rdr.ReadUInt16();
			
			x           = rdr.ReadInt16();
			y           = rdr.ReadInt16();
			cx          = rdr.ReadInt16();
			cy          = rdr.ReadInt16();
			
			menu        = rdr.ReadIdentifier();
			windowClass = rdr.ReadIdentifier();
			title       = rdr.ReadSZString();
			
			if( (style & 0x40) == 0x40 ) {
				// 0x40 = DS_SETFONT
				
				pointSize   = rdr.ReadUInt16();
				weight      = rdr.ReadUInt16();
				italic      = rdr.ReadByte();
				charset     = rdr.ReadByte();
				typeface    = rdr.ReadSZString();
			} else {
				
				pointSize   = 0;
				weight      = 0;
				italic      = 0;
				charset     = 0;
				typeface    = null;
			}
			
		}
	}
	
	[StructLayout(LayoutKind.Sequential,Pack=2)]
	internal struct DlgItemTemplateEx {
		public UInt32 helpId;
		public UInt32 exStyle;
		public UInt32 style;
		public Int16  x;
		public Int16  y;
		public Int16  cx;
		public Int16  cy;
		public UInt32 id;
		public Object windowClass;
		public Object title;
		public UInt16 extraCount;
		public Byte[] creationData;
		
		public DlgItemTemplateEx(BinaryReader rdr) {
			helpId      = rdr.ReadUInt32();
			exStyle     = rdr.ReadUInt32();
			style       = rdr.ReadUInt32();
			x           = rdr.ReadInt16();
			y           = rdr.ReadInt16();
			cx          = rdr.ReadInt16();
			cy          = rdr.ReadInt16();
			id          = rdr.ReadUInt32();
			windowClass = rdr.ReadIdentifier();
			title       = rdr.ReadIdentifier();
			extraCount  = rdr.ReadUInt16();
			creationData = null;
			
			if(extraCount > 0) {
				rdr.Align2();
				creationData = rdr.ReadBytes( extraCount );
			}
		}
	}
	
#endregion
	
#region Menus
	
	// Template Header
	
	/// <summary>Renamed from MenuItemTemplateHeader</summary>
	[StructLayout(LayoutKind.Sequential,Pack=2)]
	internal struct MenuTemplateHeader {
		
		public UInt16 wVersion;
		public UInt16 wOffset;
		
		public MenuTemplateHeader(BinaryReader rdr) {
			wVersion = rdr.ReadUInt16();
			wOffset  = rdr.ReadUInt16();
		}
	}
	
	[StructLayout(LayoutKind.Sequential,Pack=2)]
	internal struct MenuExTemplateHeader {
		
		public UInt16 wVersion;
		public UInt16 wOffset;
		public UInt32 dwHelpId;
		
		public MenuExTemplateHeader(BinaryReader rdr) {
			wVersion = rdr.ReadUInt16();
			wOffset  = rdr.ReadUInt16();
			dwHelpId = rdr.ReadUInt32();
		}
	}
	
	// Template Items
	
	internal struct MenuTemplateItem {
		
		public MenuTemplateItemOptions mtOption;
		public UInt16 mtId;
		public String mtString;
		
		public MenuTemplateItem(BinaryReader rdr) {
			
			mtOption = (MenuTemplateItemOptions)rdr.ReadUInt16();
			
			if( (mtOption & MenuTemplateItemOptions.Popup) == MenuTemplateItemOptions.Popup ) { // if it's a popup
				
				mtId     = 0;
				mtString = rdr.ReadSZString();
				
			} else {
				
				mtId     = rdr.ReadUInt16();
				mtString = rdr.ReadSZString();
				if(mtString.Length == 0) mtString = "-";
				
			}
			
		}
	}
	
	[Flags]
	public enum MenuTemplateItemOptions : ushort {
		
		/// <summary>Indicates that the menu item is initially inactive and drawn with a gray effect. (0x0001)</summary>
		Greyed       = 0x0001,
		Inactive     = 0x0002,
		Bitmap       = 0x0004,
		/// <summary>Indicates that the menu item has a check mark next to it. (0x0008)</summary>
		Checked      = 0x0008,
		/// <summary>Indicates that the menu item is placed in a new column. The old and new columns are separated by a bar. (0x0020)</summary>
		MenuBarBreak = 0x0020,
		/// <summary>Indicates that the menu item is placed in a new column. (0x0040)</summary>
		MenuBreak    = 0x0040,
		/// <summary>Indicates that the owner window of the menu is responsible for drawing all visual aspects of the menu item, including highlighted, selected, and inactive states. This option is not valid for an item in a menu bar. (0x0100)</summary>
		OwnerDraw    = 0x0100,
		/// <summary>Indicates that the menu item has a vertical separator to its left. (0x4000)</summary>
		Help         = 0x4000,
		
		/// <summary>Indicates that the item is one that opens a drop-down menu or submenu. (0x0010)</summary>
		Popup        = 0x0010,
		EndMenu      = 0x0080
	}
	
	internal struct MenuExTemplateItem {
		
		public MenuExTemplateItemType dwType;
		public MenuExTemplateItemState dwState;
		public UInt32 menuId;
		public MenuExTemplateItemInfo bResInfo;
		public String szText;
		public UInt32 dwHelpId;
		
		public MenuExTemplateItem(BinaryReader rdr) {
			
			dwType   = (MenuExTemplateItemType) rdr.ReadUInt32();
			dwState  = (MenuExTemplateItemState)rdr.ReadUInt32();
			menuId   =                          rdr.ReadUInt32();
			bResInfo = (MenuExTemplateItemInfo) rdr.ReadUInt16(); // not really a byte
			
			if( (dwType & MenuExTemplateItemType.Separator) == 0 ) { // if this is not a separator
				
				szText = rdr.ReadSZString();
			} else {
				szText = "-";
			}
			
			rdr.Align4();
			
			if( ( bResInfo & MenuExTemplateItemInfo.HasChildren ) == MenuExTemplateItemInfo.HasChildren ) { // if it HAS children
				
				dwHelpId = rdr.ReadUInt32();
			} else {
				
				dwHelpId = 0;
			}
			
		}
		
	}
	
	[Flags]
	internal enum MenuExTemplateItemInfo : ushort {
		LastItem    = 0x0080,
		HasChildren = 0x0001
	}
	
	[Flags]
	internal enum MenuExTemplateItemType : uint {
		
		Bitmap       = 0x0004,
		MenuBarBreak = 0x0020,
		MenuBreak    = 0x0040,
		OwnerDraw    = 0x0100,
		RadioCheck   = 0x0200,
		RightJustify = 0x4000,
		RightOrder   = 0x2000,
		Separator    = 0x0800,
		String       = 0x0000
	}
	
	[Flags]
	internal enum MenuExTemplateItemState : uint {
		
		Checked      = 0x0008,
		Default      = 0x1000,
		Disabled     = 0x0001,
		Enabled      = 0x0000,
		Grayed       = 0x0003,
		Hilite       = 0x0080,
		Unchecked    = 0x0000,
		Unhilite     = 0x0000
	}
	
#endregion
	
#region *.res Files
	
	internal struct ResResourceHeader {
		/// <summary>Size of the ResourceData following this header</summary>
		public UInt32 DataSize;
		public UInt32 HeaderSize;
		public Object Type;
		public Object Name;
		public UInt32 DataVersion;
		public MemoryFlags MemoryFlags;
		public UInt16 LanguageId;
		public UInt32 Version;
		public UInt32 Characteristics;
		
		public ResResourceHeader(BinaryReader rdr) {
			
			Int64 pos = rdr.BaseStream.Position;
			
			DataSize        = rdr.ReadUInt32();
			HeaderSize      = rdr.ReadUInt32();
			
			if(DataSize == 0) { // abort
				Type            = 0;
				Name            = 0;
				DataVersion     = 0;
				MemoryFlags     = 0;
				LanguageId      = 0;
				Version         = 0;
				Characteristics = 0;
				return;
			}
			
			Type            = rdr.ReadIdentifier();
			Name            = rdr.ReadIdentifier();
			
			rdr.Align4();
			
			DataVersion     = rdr.ReadUInt32();
			MemoryFlags     = (MemoryFlags)rdr.ReadUInt16();
			LanguageId      = rdr.ReadUInt16();
			Version         = rdr.ReadUInt32();
			Characteristics = rdr.ReadUInt32();
			
			// Despite the docs, HeaderSize is the total size of the header INCLUDING DataSize and HeaderSize (i.e. inclusive of those 8 bytes )
			Int32 cruft = (int)(rdr.BaseStream.Position - pos);
			if( cruft > HeaderSize ) {
				
				Byte[] cruftBytes = rdr.ReadBytes( cruft );
			}
		}
		
		public ResResourceHeader(ResourceLang lang) {
			
			DataSize        = (uint)lang.Data.RawData.Length;
			HeaderSize      = 32;
			Type            = GetIdentifier( lang.Name.Type.Identifier );
			Name            = GetIdentifier( lang.Name.Identifier );
			
			DataVersion     = 0;
			MemoryFlags     = MemoryFlags.None; // MemoryFlags are a vestige from Win3x
			LanguageId      = lang.LanguageId;
			Version         = 0;
			Characteristics = 0;
			
			// HeaderSize is not always 32, if the identifiers are strings, for example, it's larger
			// there is no padding between Type and Name, but there may be padding between Name and DataVersion
			
			int headerSize = 
				4 + // DataSize
				4 + // HeaderSize
				(Type is String ? 2 * ((Type as String).Length + 1) : 4 ) +
				(Name is String ? 2 * ((Name as String).Length + 1) : 4 ) +
				
				4 + // DataVersion
				2 + // MemoryFlags
				2 + // LanguageId
				4 + // Version
				4 + // Characteristics
			0;
			
			int paddingLength = headerSize % 4;
			
			HeaderSize = (uint)( headerSize + paddingLength );
		}
		
		public void Write(BinaryWriter wtr) {
			
			wtr.Write( DataSize );
			wtr.Write( HeaderSize );
			
			WriteIdentifier( Type, wtr );
			WriteIdentifier( Name, wtr );
			
			wtr.Align4();
			
			wtr.Write( DataVersion );
			wtr.Write( (ushort)MemoryFlags );
			wtr.Write( LanguageId );
			wtr.Write( Version );
			wtr.Write( Characteristics );
			
		}
		
		private static void WriteIdentifier(Object ident, BinaryWriter wtr) {
			
			String sid = ident as String;
			if( sid != null ) wtr.WriteSZString( sid );
			else {
				
				Int32 id = (Int32)ident; // must unbox first, before doing numeric type conversion
				
				wtr.Write( (ushort)0xFFFF );
				wtr.Write( (ushort)id     );
			}
			
		}
		
		private static Object GetIdentifier(ResourceIdentifier id) {
			
			if( id.IntegerId == null ) return id.StringId;
			return id.IntegerId;
			
		}
		
		public static void WriteResource(ResourceLang lang, BinaryWriter wtr) {
			
			ResResourceHeader header = new ResResourceHeader( lang );
			header.Write( wtr );
			
			wtr.Write( lang.Data.RawData );
			
			wtr.Align4();
		}
		
		
	};
	
	[Flags]
	internal enum MemoryFlags : ushort {
		
		None        = 0,
		Discardable = 0x1000,
		
		Movable     = 0x0010,
		Fixed       = unchecked( (ushort)~Movable ),
		
		Pure        = 0x0020,
		Impure      = unchecked( (ushort)~Pure ),
		
		Preload     = 0x0040,
		LoadOnCall  = unchecked( (ushort)~Preload )
		
	}
	
#endregion
	
#region BinPatch
	
	internal enum WfpResult {
		Success = 0,
		Error   = 1
	}
	
	/// <summary>IMAGE_FILE_MACHINE</summary>
	internal enum MachineType : ushort {
		Unknown    = 0,
		I386       = 0x014c,  // Intel 386.
		R3000      = 0x0162,  // MIPS little-endian, 0x160 big-endian
		R4000      = 0x0166,  // MIPS little-endian
		R10000     = 0x0168,  // MIPS little-endian
		WCEMIPSV2  = 0x0169,  // MIPS little-endian WCE v2
		Alpha      = 0x0184,  // Alpha_AXP
		SH3        = 0x01a2,  // SH3 little-endian
		SH3Dsp     = 0x01a3,
		SH3E       = 0x01a4,  // SH3E little-endian
		SH4        = 0x01a6,  // SH4 little-endian
		SH5        = 0x01a8,  // SH5
		Arm        = 0x01c0,  // ARM Little-Endian
		THUMB      = 0x01c2,
		AM33       = 0x01d3,
		PowerPC    = 0x01F0,  // IBM PowerPC Little-Endian
		PowerPCFP  = 0x01f1,
		IA64       = 0x0200,  // Intel 64
		Mips16     = 0x0266,  // MIPS
		Alpha64    = 0x0284,  // ALPHA64
		MipsFPU    = 0x0366,  // MIPS
		MipsFPU16  = 0x0466,  // MIPS
		Tricore    = 0x0520,  // Infineon
		CEF        = 0x0CEF,
		EBC        = 0x0EBC,  // EFI Byte Code
		Amd64      = 0x8664,  // AMD64 (K8)
		M32R       = 0x9041,  // M32R little-endian
		CEE        = 0xC0EE
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct DosHeader {
		public       UInt16 e_magic;                     // Magic number
		public       UInt16 e_cblp;                      // Bytes on last page of file
		public       UInt16 e_cp;                        // Pages in file
		public       UInt16 e_crlc;                      // Relocations
		public       UInt16 e_cparhdr;                   // Size of header in paragraphs
		public       UInt16 e_minalloc;                  // Minimum extra paragraphs needed
		public       UInt16 e_maxalloc;                  // Maximum extra paragraphs needed
		public       UInt16 e_ss;                        // Initial (relative) SS value
		public       UInt16 e_sp;                        // Initial SP value
		public       UInt16 e_csum;                      // Checksum
		public       UInt16 e_ip;                        // Initial IP value
		public       UInt16 e_cs;                        // Initial (relative) CS value
		public       UInt16 e_lfarlc;                    // File address of relocation table
		public       UInt16 e_ovno;                      // Overlay number
		public fixed UInt16 e_res[4];                    // Reserved UInt16s
		public       UInt16 e_oemid;                     // OEM identifier (for e_oeminfo)
		public       UInt16 e_oeminfo;                   // OEM information; e_oemid specific
		public fixed UInt16 e_res2[10];                  // Reserved UInt16s
		public       UInt32 e_lfanew;                    // File address of new exe header
		
		public static UInt16 DosMagic = 0x5A4D; // "MZ";
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal struct NTHeader {
		public UInt32            Signature;
		public NTImageFileHeader FileHeader;
		public NTHeaders32       OptionalHeader;
		
		public static UInt32 NTSignature = 0x00004550; // "PE00";
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal struct NTImageFileHeader {
		public MachineType Machine;
		public UInt16 NumberOfSections;
		public UInt32 TimeDateStamp;
		public UInt32 PointerToSymbolTable;
		public UInt32 NumberOfSymbols;
		public UInt16 SizeOfOptionalHeader;
		public UInt16 Characteristics;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct NTHeaders32 {
		// Standard fields.
		public UInt16  Magic;
		public Byte    MajorLinkerVersion;
		public Byte    MinorLinkerVersion;
		public UInt32  SizeOfCode;
		public UInt32  SizeOfInitializedData;
		public UInt32  SizeOfUninitializedData;
		public UInt32  AddressOfEntryPoint;
		public UInt32  BaseOfCode;
		public UInt32  BaseOfData;
		// NT additional fields.
		public UInt32  ImageBase;
		public UInt32  SectionAlignment;
		public UInt32  FileAlignment;
		public UInt16  MajorOperatingSystemVersion;
		public UInt16  MinorOperatingSystemVersion;
		public UInt16  MajorImageVersion;
		public UInt16  MinorImageVersion;
		public UInt16  MajorSubsystemVersion;
		public UInt16  MinorSubsystemVersion;
		public UInt32  Win32VersionValue;
		public UInt32  SizeOfImage;
		public UInt32  SizeOfHeaders;
		public UInt32  CheckSum;
		public UInt16  Subsystem;
		public UInt16  DllCharacteristics;
		public UInt32  SizeOfStackReserve;
		public UInt32  SizeOfStackCommit;
		public UInt32  SizeOfHeapReserve;
		public UInt32  SizeOfHeapCommit;
		public UInt32  LoaderFlags;
		public UInt32  NumberOfRvaAndSizes;
//		public fixed ImageDataDirectory DataDirectory[16];
		public fixed Byte DataDirectory[ 16 * 8 ]; // 8 == sizeof(ImageDataDirectory)
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct NTHeaders64 {
		// Standard fields.
		public UInt16  Magic;
		public Byte    MajorLinkerVersion;
		public Byte    MinorLinkerVersion;
		public UInt32  SizeOfCode;
		public UInt32  SizeOfInitializedData;
		public UInt32  SizeOfUninitializedData;
		public UInt32  AddressOfEntryPoint;
		public UInt32  BaseOfCode;
		public UInt64  ImageBase;
		public UInt32  SectionAlignment;
		public UInt32  FileAlignment;
		public UInt16  MajorOperatingSystemVersion;
		public UInt16  MinorOperatingSystemVersion;
		public UInt16  MajorImageVersion;
		public UInt16  MinorImageVersion;
		public UInt16  MajorSubsystemVersion;
		public UInt16  MinorSubsystemVersion;
		public UInt32  Win32VersionValue;
		public UInt32  SizeOfImage;
		public UInt32  SizeOfHeaders;
		public UInt32  CheckSum;
		public UInt16  Subsystem;
		public UInt16  DllCharacteristics;
		public UInt64  SizeOfStackReserve;
		public UInt64  SizeOfStackCommit;
		public UInt64  SizeOfHeapReserve;
		public UInt64  SizeOfHeapCommit;
		public UInt32  LoaderFlags;
		public UInt32  NumberOfRvaAndSizes;
//		public fixed ImageDataDirectory DataDirectory[16];
		public fixed Byte DataDirectory[ 16 * 8 ]; // 8 == sizeof(ImageDataDirectory)
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal struct ImageDataDirectory {
		public UInt32 VirtualAddress;
		public UInt32 Size;
	}
	
#endregion
	
#region File Maps
	
	[Flags]
	public enum FileMapAccess : uint {
		FileMapCopy = 0x0001,
		FileMapWrite = 0x0002,
		FileMapRead = 0x0004,
		FileMapAllAccess = 0x001f,
		fileMapExecute = 0x0020,
	}
	
	[Flags]
	public enum FileMapProtection : uint {
		/// <summary>Sets read-only access. An attempt to write to the file view results in an access violation. The file that the hFile parameter specifies must be created with the GENERIC_READ access right.</summary>
		PageReadOnly = 0x02,
		/// <summary>Sets read/write access. The file that hFile specifies must be created with the GENERIC_READ and GENERIC_WRITE access rights.</summary>
		PageReadWrite = 0x04,
		/// <summary>Sets copy-on-write access. The files that the hFile parameter specifies must be created with the GENERIC_READ access right.</summary>
		PageWriteCopy = 0x08,
		PageExecuteRead = 0x20,
		PageExecuteReadWrite = 0x40,
		SectionCommit = 0x8000000,
		SectionImage = 0x1000000,
		SectionNoCache = 0x10000000,
		SectionReserve = 0x4000000,
	}
	
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal sealed class SafeFileMappingHandle : SafeHandleZeroOrMinusOneIsInvalid {
		
		public SafeFileMappingHandle() : base(true) {
		}
		
		public SafeFileMappingHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle) {
			SetHandle(handle);
		}
		
		protected override Boolean ReleaseHandle() {
			return NativeMethods.CloseHandle(handle);
		}
	}
	
#endregion
	
#region System Restore

	/// <summary>Contains information used by the SRSetRestorePoint function</summary>
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
	internal struct RestorePointInfo {
		public Int32 dwEventType; // The type of event
		public Int32 dwRestorePtType; // The type of restore point
		public Int64 llSequenceNumber; // The sequence number of the restore point
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256 + 1)]
		public String szDescription; // The description to be displayed so the user can easily identify a restore point
	}
	
	/// <summary>Contains status information used by the SRSetRestorePoint function</summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct StateManagerStatus {
		public Int32 nStatus; // The status code
		public Int64 llSequenceNumber; // The sequence number of the restore point
	}
	
#endregion
	
	public enum SpiAction : uint {
		
		None                 = 0,
		
		// Accessibility
		SetFocusBorderWidth  = 0x200F, // pvParam = value
		SetFocusBorderHeight = 0x2011, // pvParam = value
		SetMouseSonar        = 0x101D, // pvParam = TRUE | FALSE
		
		// Desktop
		SetCursors           = 0x0057, // 
		SetDesktopWallpaper  = 0x0014, // pvParam = pSz wallpaper BMP image (JPEG in Vista and later)
		SetDropShadow        = 0x1025, // pvParam = TRUE | FALSE
		SetFlatMenu          = 0x1023, // pvParam = TRUE | FALSE
		
		SetFontSmoothing            = 0x004B, // uiParam = TRUE | FALSE
		SetFontSmoothingType        = 0x200B, // pvParam = 1 for Standard Antialiasing, 2 for ClearType
		SetFontSmoothingContrast    = 0x200D, // pvParam = 1000 to 2200, default is 1400
		SetFontSmoothingOrientation = 0x2013, // pvParam = 0 for BGR, 1 for RGB
		
		// Icons
		IconHorizontalSpacing = 0x000D, // uiParam = value, pvParam = pointer to an int that gets the value
		IconVerticalSpacing   = 0x0018, // uiParam = value, pvParam = pointer to an int that gets the value
		SetIcons              = 0x0058, //
		SetIconTitleWrap      = 0x001A, // uiParam = TRUE | FALSE
		
		// Mouse
		SetMouseTrails        = 0x005D, // uiParam = 0 or 1 to disable, or n > 1 for n cursors
		
		// Menus
		SetMenuDropAlignment  = 0x001C, // uiParam = TRUE for right, FALSE for left
		SetMenuFade           = 0x1013, // pvParam = TRUE | FALSE
		SetMenuShowDelay      = 0x006B, // uiParam = time in miliseconds
		
		// UI Effects
		SetUIEffects          = 0x103F, // pvParam = TRUE | FALSE to enable/disable all UI effects at once
		// I'd define all of them here, but there's little point
		
		// Windows
		SetActiveWindowTracking  = 0x1001, // pvParam = TRUE | FALSE
		SetActiveWndTrkZOrder    = 0x100D, // pvParam = TRUE | FALSE
		SetActiveWndTrkTimeout   = 0x2003, // pvParam = time in miliseconds
		SetBorder                = 0x0006, // uiParam = multiplier width of sizing border
		SetCaretWidth            = 0x2007, // pvParam = width in pixels
		SetDragFullWindows       = 0x0025, // uiParam = TRUE | FALSE
		SetForegroundFlashCount  = 0x2005, // pvParam = # times to flash
		SetForegroundLockTimeout = 0x2001,  // pvParam = timeout in miliseconds
		
		
		// Get
		GetFocusBorderWidth  = 0x200E, // pvParam = Pointer to a UINT that receives the value
		GetFocusBorderHeight = 0x2010, // pvParam = Pointer to a UINT that receives the value
		GetMouseSonar        = 0x101C, // pvParam = Pointer to a BOOL
		
		GetDesktopWallpaper  = 0x0073, // pvParam = Pointer to a string buffer, uiParam = Size of the pvParam buffer
		GetDropShadow        = 0x1024, // pvParam = Pointer to a BOOL
		GetFlatMenu          = 0x1022, // pvParam = Pointer to a BOOL
		
		GetFontSmoothing            = 0x004A, // pvParam = Pointer to a BOOL
		GetFontSmoothingType        = 0x200A, // pvParam = Pointer to a UINT
		GetFontSmoothingContrast    = 0x200C, // pvParam = Pointer to a UINT
		GetFontSmoothingOrientation = 0x2012, // pvParam = Pointer to a UINT
		
		GetMouseTrails           = 0x005E, // pvParam = Pointer to an integer that received the value. If it's <= 1 then it's disabled. >= 2 is enabled with n many trails
		GetMenuDropAlignment     = 0x001B, // pvParam = Pointer to a BOOL (TRUE = right, FALSE = left)
		GetMenuFade              = 0x1012, // pvParam = Pointer to a BOOL
		GetMenuShowDelay         = 0x006A, // pvParam = Pointer to a DWORD, which is the length of the delay
		
		GetUIEffects             = 0x103E, // pvParam = Pointer to a BOOL
		
		GetActiveWindowTracking  = 0x1000, // pvParam = Pointer to a BOOL
		GetBorder                = 0x0005, // pvParam = Pointer to an integer
		GetCaretWidth            = 0x2006, // pvParam = Pointer to a DWORD
		GetDragFullWindows       = 0x0026, // pvParam = Pointer to a BOOL
		GetForegroundFlashCount  = 0x2004, // pvParam = Pointer to a DWORD
		GetForegroundLockTimeout = 0x2000  // pvParam = Pointer to a DWORD
	}
	
}