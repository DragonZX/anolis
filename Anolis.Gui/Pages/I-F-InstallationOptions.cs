﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Gui.Pages {
	
	public partial class InstallationOptionsPage : BaseInteriorPage {
		
		public InstallationOptionsPage() {
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
			
		}
		
		private void Extracting_Load(object sender, EventArgs e) {
		}
		
		public override W3b.Wizards.WizardPage PreviousPage {
			get { return Program.PageIEModifyPackage; }
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return null; }
		}
		
	}
}
