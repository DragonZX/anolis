﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Anolis.Core;
using Anolis.Core.Data;

namespace Anolis.Resourcer.TypeViewers {
	
	public partial class IconCursorViewer : TypeViewer {
		
		public IconCursorViewer() {
			InitializeComponent();
			
			__images.Height = 70 + new HScrollBar().Height + __images.Margin.Top;
			__currentImage.Height = this.Height - __images.Height - __images.Margin.Top;
			
		}
		
		public override void RenderResource(ResourceData resource) {
			
			IconDirectoryResourceData iDirectory = resource as IconDirectoryResourceData;
			if(iDirectory != null) {
				RenderIconDirectory( iDirectory );
				return;
			}
			
			CursorDirectoryResourceData cDirectory = resource as CursorDirectoryResourceData;
			if(cDirectory != null) {
				RenderCursorDirectory( cDirectory );
				return;
			}
			
			// show some Not Supported message of some sort
			
		}
		
		private void RenderIconDirectory(IconDirectoryResourceData dir) {
			
			this.__images.Controls.Clear();
			
			foreach(IDirectoryMember member in dir.Members) {
				
				MemberPic mp = new MemberPic();
				mp.Member = member;
				mp.Click += new EventHandler(mp_Click);
				
				this.__images.Controls.Add( mp );
				
			}
			
		}
		
		private void mp_Click(Object sender, EventArgs e) {
			
			MemberPic member = sender as MemberPic;
			
			foreach(MemberPic mp in __images.Controls) {
				mp.Selected = false;
			}
			
			member.Selected = true;
			
			ImageResourceData ird = member.Member.ResourceData as ImageResourceData;
			
			__currentImage.Image = ird.Image;
			
			__images.Invalidate(true);
			
		}
		
		private void RenderCursorDirectory(CursorDirectoryResourceData dir) {
			
			// TODO
			
		}
		
		public override TypeViewerCompatibility CanHandleResource(ResourceData data) {
			
			if( data is DirectoryResourceData ) return TypeViewerCompatibility.Ideal;
			
			return TypeViewerCompatibility.None;
			
		}
		
		public override String ViewerName {
			get { return "Icon and Cursor Viewer"; }
		}
		
		private class MemberPic : Control {
			
			public IDirectoryMember Member { get; set; }
			
			public Boolean Selected { get; set; }
			
			public MemberPic() {
				this.Width  = 70;
				this.Height = 70;
				this.Margin = new Padding(3, 0, 3, 0);
			}
			
			protected override void OnPaint(PaintEventArgs e) {
				
				if(Member != null) {
					
					Image bmp = (Member.ResourceData as ImageResourceData).Image;
					
					Size textSize = TextRenderer.MeasureText( e.Graphics, Member.Description, SystemFonts.DefaultFont );
					
					Rectangle textRect = new Rectangle( Width / 2 - textSize.Width / 2, 54, textSize.Width, textSize.Height );
					
					if(Selected) e.Graphics.FillRectangle( SystemBrushes.Highlight, 0, 0, this.Width, this.Height );
					
					///////////////////////////////////
					// Prototype Rectangles:
					
//					e.Graphics.DrawRectangle( Pens.Red, 0, 0, Width - 1, Height - 1 );
					
					Rectangle imageRect = GetImageRect(bmp.Width, bmp.Height);
					
//					e.Graphics.DrawRectangle( Pens.Blue, imageRect );
					
//					e.Graphics.DrawRectangle( Pens.Green, textRect );
					
					Rectangle magRect = new Rectangle( Width - 16, textRect.Y - 16 - 4, 16, 16 );
					
//					if(bmp.Width > 48 || bmp.Height > 48) {
//						e.Graphics.DrawRectangle( Pens.Yellow, Width - 16, textRect.Y - 16 - 4, 16, 16 );
//					}
					
					///////////////////////////////////
					// Actual Bits:
					
					TextRenderer.DrawText( e.Graphics, Member.Description, SystemFonts.DefaultFont, textRect, Selected ? SystemColors.HighlightText : SystemColors.ControlText );
					
					if(bmp.Width > 48 || bmp.Height > 48) {
						
						e.Graphics.DrawImage( bmp, GetImageRect(48, 48) );
						
						e.Graphics.DrawImage( Properties.Resources.Dir_Zoomable, magRect );
					} else {
						e.Graphics.DrawImage( bmp, imageRect );
						
					}
					
				}
				
				base.OnPaint(e);
			}
			
			private Rectangle GetImageRect(Int32 imageWidth, Int32 imageHeight) {
				return new Rectangle( Width / 2 - imageWidth / 2,    Height / 2 - imageHeight / 2 - 5,  imageWidth, imageHeight );
			}
			
		}
		
	}
}
