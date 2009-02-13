﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anolis.Gui.Pages {
	
	public partial class ModifyPackagePage : BaseInteriorPage {
		
		public ModifyPackagePage() {
			
			InitializeComponent();
			
			this.Load += new EventHandler(Extracting_Load);
			
		}
		
		private void Extracting_Load(object sender, EventArgs e) {
			
			WizardForm.EnableNext = true;
			
			//PackageManager 
			
		}
		
		public override W3b.Wizards.WizardPage PreviousPage {
			get { return Program.PageICSelectPackage; }
		}
		
		public override W3b.Wizards.WizardPage NextPage {
			get { return Program.PageIFInstallationOptions; }
		}
		
	}
}
