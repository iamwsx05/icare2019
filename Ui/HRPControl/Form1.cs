using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;

namespace ApplicationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            m_mthTestPartogram();
        }

        void m_mthTestPartogram()
        {
        }

        private void ctlPartogramRecord1_m_evnPartogramEveryHourMouseDown(object sender, EventArgs e)
        {
            clsPartogramEveryHourEventArgs objArgs = e as clsPartogramEveryHourEventArgs;
            if (objArgs != null)
            {
                if (objArgs.m_objPartogramArgs != null)
                {
                    MessageBox.Show(objArgs.m_objPartogramArgs.m_intPARTOGRAM_INT + "Hour");
                }
            }
        }

    }
}