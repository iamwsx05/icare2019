using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace com.digitalwave.iCare.RemindMessage
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}