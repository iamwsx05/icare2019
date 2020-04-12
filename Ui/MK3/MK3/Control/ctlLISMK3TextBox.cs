using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS.Control
{
    public class ctlLISMK3TextBox : TextBox
    {
        // Fields
        private m_enmSampleType _m_enmSampleType;
        private string _m_strCheckItemName;
        private string _m_strItemID;
        private string _m_strSampleID;
        private IContainer components;
        private int intSampleID;

        // Methods
        public ctlLISMK3TextBox()
        {
            this.components = null; 
            this.InitializeComponent(); 
        }

        public ctlLISMK3TextBox(IContainer container)
        {
            this.components = null; 
            container.Add(this);
            this.InitializeComponent(); 
        } 

        private void InitializeComponent()
        {
            this.components = new Container(); 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Properties
        public m_enmSampleType m_enmSelceType
        {
            get
            {
                return this._m_enmSampleType; 
            }
            set
            {
                this._m_enmSampleType = value; 
            }
        }

        public string m_strCheckItemName
        {
            get
            {
                return this._m_strCheckItemName; 
            }
            set
            {
                this._m_strCheckItemName = value; 
            }
        }

        public string m_strItmeID
        {
            get
            {
                return this._m_strItemID; 
            }
            set
            {
                this._m_strItemID = value; 
            }
        }

        public string m_strSampleID
        {
            get
            { 
                return this._m_strSampleID; 
            }
            set
            {
                this._m_strSampleID = value; 
            }
        }

        // Nested Types
        public enum m_enmSampleType
        {
            None,
            QC,
            Smp,
            Neg,
            Pos,
            Blk
        }
    } 
}
