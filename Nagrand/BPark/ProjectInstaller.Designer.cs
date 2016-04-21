namespace BPark
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
            this.NagrandProcess = new System.ServiceProcess.ServiceProcessInstaller();
            this.NagrandService = new System.ServiceProcess.ServiceInstaller();
            // 
            // NagrandProcess
            // 
            this.NagrandProcess.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.NagrandProcess.Password = null;
            this.NagrandProcess.Username = null;
            // 
            // NagrandService
            // 
            this.NagrandService.Description = "Servicio que gestiona el dispositivo biometrico";
            this.NagrandService.DisplayName = "Nagrand";
            this.NagrandService.ServiceName = "BPark";
            this.NagrandService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.NagrandService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.NagrandProcess,
            this.NagrandService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller NagrandProcess;
        private System.ServiceProcess.ServiceInstaller NagrandService;
    }
}