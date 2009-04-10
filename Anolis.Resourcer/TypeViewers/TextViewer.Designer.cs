﻿namespace Anolis.Resourcer.TypeViewers {
	partial class TextViewer {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.ToolStripSeparator @__toolsSep;
			this.@__tools = new System.Windows.Forms.ToolStrip();
			this.@__toolsEncodingLbl = new System.Windows.Forms.ToolStripLabel();
			this.@__toolsEncoding = new System.Windows.Forms.ToolStripDropDownButton();
			this.@__toolsEncAscii = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsEncUtf7 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsEncUtf8 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsEncUtf16 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsEncUtf32 = new System.Windows.Forms.ToolStripMenuItem();
			this.@__toolsBom = new System.Windows.Forms.ToolStripButton();
			this.@__toolsEndian = new System.Windows.Forms.ToolStripButton();
			this.@__toolsWrap = new System.Windows.Forms.ToolStripButton();
			this.@__toolsFont = new System.Windows.Forms.ToolStripButton();
			this.@__text = new System.Windows.Forms.TextBox();
			this.@__fdlg = new System.Windows.Forms.FontDialog();
			@__toolsSep = new System.Windows.Forms.ToolStripSeparator();
			this.@__tools.SuspendLayout();
			this.SuspendLayout();
			// 
			// __toolsSep
			// 
			@__toolsSep.Name = "__toolsSep";
			@__toolsSep.Size = new System.Drawing.Size(6, 25);
			// 
			// __tools
			// 
			this.@__tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.@__tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__toolsEncodingLbl,
            this.@__toolsEncoding,
            this.@__toolsBom,
            this.@__toolsEndian,
            @__toolsSep,
            this.@__toolsWrap,
            this.@__toolsFont});
			this.@__tools.Location = new System.Drawing.Point(0, 0);
			this.@__tools.Name = "__tools";
			this.@__tools.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
			this.@__tools.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.@__tools.Size = new System.Drawing.Size(644, 25);
			this.@__tools.TabIndex = 2;
			this.@__tools.Text = "toolStrip1";
			// 
			// __toolsEncodingLbl
			// 
			this.@__toolsEncodingLbl.Name = "__toolsEncodingLbl";
			this.@__toolsEncodingLbl.Size = new System.Drawing.Size(54, 22);
			this.@__toolsEncodingLbl.Text = "Encoding:";
			// 
			// __toolsEncoding
			// 
			this.@__toolsEncoding.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.@__toolsEncoding.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.@__toolsEncAscii,
            this.@__toolsEncUtf7,
            this.@__toolsEncUtf8,
            this.@__toolsEncUtf16,
            this.@__toolsEncUtf32});
			this.@__toolsEncoding.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsEncoding.Name = "__toolsEncoding";
			this.@__toolsEncoding.Size = new System.Drawing.Size(49, 22);
			this.@__toolsEncoding.Text = "UTF-8";
			// 
			// __toolsEncAscii
			// 
			this.@__toolsEncAscii.Name = "__toolsEncAscii";
			this.@__toolsEncAscii.Size = new System.Drawing.Size(109, 22);
			this.@__toolsEncAscii.Text = "ASCII";
			// 
			// __toolsEncUtf7
			// 
			this.@__toolsEncUtf7.Name = "__toolsEncUtf7";
			this.@__toolsEncUtf7.Size = new System.Drawing.Size(109, 22);
			this.@__toolsEncUtf7.Text = "UTF-7";
			// 
			// __toolsEncUtf8
			// 
			this.@__toolsEncUtf8.Name = "__toolsEncUtf8";
			this.@__toolsEncUtf8.Size = new System.Drawing.Size(109, 22);
			this.@__toolsEncUtf8.Text = "UTF-8";
			// 
			// __toolsEncUtf16
			// 
			this.@__toolsEncUtf16.Name = "__toolsEncUtf16";
			this.@__toolsEncUtf16.Size = new System.Drawing.Size(109, 22);
			this.@__toolsEncUtf16.Text = "UTF-16";
			// 
			// __toolsEncUtf32
			// 
			this.@__toolsEncUtf32.Name = "__toolsEncUtf32";
			this.@__toolsEncUtf32.Size = new System.Drawing.Size(109, 22);
			this.@__toolsEncUtf32.Text = "UTF-32";
			// 
			// __toolsBom
			// 
			this.@__toolsBom.CheckOnClick = true;
			this.@__toolsBom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__toolsBom.Image = global::Anolis.Resourcer.Properties.Resources.TextViewer_Bom;
			this.@__toolsBom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsBom.Name = "__toolsBom";
			this.@__toolsBom.Size = new System.Drawing.Size(23, 22);
			this.@__toolsBom.Text = "Byte Order Mark";
			this.@__toolsBom.ToolTipText = "Use Byte Order Mark (BOM) if present";
			// 
			// __toolsEndian
			// 
			this.@__toolsEndian.CheckOnClick = true;
			this.@__toolsEndian.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.@__toolsEndian.Image = global::Anolis.Resourcer.Properties.Resources.TextViewer_Endian;
			this.@__toolsEndian.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsEndian.Name = "__toolsEndian";
			this.@__toolsEndian.Size = new System.Drawing.Size(23, 22);
			this.@__toolsEndian.Text = "Unicode Big-Endian";
			// 
			// __toolsWrap
			// 
			this.@__toolsWrap.Image = global::Anolis.Resourcer.Properties.Resources.TextViewer_WordWrap;
			this.@__toolsWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsWrap.Name = "__toolsWrap";
			this.@__toolsWrap.Size = new System.Drawing.Size(82, 22);
			this.@__toolsWrap.Text = "Word Wrap";
			// 
			// __toolsFont
			// 
			this.@__toolsFont.Image = global::Anolis.Resourcer.Properties.Resources.TextViewer_Font;
			this.@__toolsFont.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.@__toolsFont.Name = "__toolsFont";
			this.@__toolsFont.Size = new System.Drawing.Size(49, 22);
			this.@__toolsFont.Text = "Font";
			this.@__toolsFont.Click += new System.EventHandler(this.@__toolsFont_Click);
			// 
			// __text
			// 
			this.@__text.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@__text.Location = new System.Drawing.Point(0, 25);
			this.@__text.Multiline = true;
			this.@__text.Name = "__text";
			this.@__text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.@__text.Size = new System.Drawing.Size(644, 422);
			this.@__text.TabIndex = 3;
			// 
			// __fdlg
			// 
			this.@__fdlg.FontMustExist = true;
			this.@__fdlg.ShowApply = true;
			this.@__fdlg.ShowEffects = false;
			// 
			// TextViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.@__text);
			this.Controls.Add(this.@__tools);
			this.Name = "TextViewer";
			this.Size = new System.Drawing.Size(644, 447);
			this.@__tools.ResumeLayout(false);
			this.@__tools.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip __tools;
		private System.Windows.Forms.ToolStripLabel __toolsEncodingLbl;
		private System.Windows.Forms.ToolStripDropDownButton __toolsEncoding;
		private System.Windows.Forms.ToolStripMenuItem __toolsEncAscii;
		private System.Windows.Forms.ToolStripMenuItem __toolsEncUtf7;
		private System.Windows.Forms.ToolStripMenuItem __toolsEncUtf8;
		private System.Windows.Forms.ToolStripMenuItem __toolsEncUtf16;
		private System.Windows.Forms.ToolStripMenuItem __toolsEncUtf32;
		private System.Windows.Forms.ToolStripButton __toolsBom;
		private System.Windows.Forms.ToolStripButton __toolsEndian;
		private System.Windows.Forms.ToolStripButton __toolsWrap;
		private System.Windows.Forms.TextBox __text;
		private System.Windows.Forms.ToolStripButton __toolsFont;
		private System.Windows.Forms.FontDialog __fdlg;
	}
}
