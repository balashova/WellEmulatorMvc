namespace WellEmulator.Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WellEmulatorServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.WellEmulatorServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // WellEmulatorServiceProcessInstaller
            // 
            this.WellEmulatorServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.WellEmulatorServiceProcessInstaller.Password = null;
            this.WellEmulatorServiceProcessInstaller.Username = null;
            // 
            // WellEmulatorServiceInstaller
            // 
            this.WellEmulatorServiceInstaller.Description = "Well Emulator Service";
            this.WellEmulatorServiceInstaller.DisplayName = "WellEmulatorService";
            this.WellEmulatorServiceInstaller.ServiceName = "WellEmulatorService";
            this.WellEmulatorServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WellEmulatorServiceProcessInstaller,
            this.WellEmulatorServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller WellEmulatorServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller WellEmulatorServiceInstaller;
    }
}