using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using PinkieControls;
using System.Drawing;
using ZedGraph;
using System.Xml;
using System.IO;
using ExpressionEvaluation;

namespace com.digitalwave.iCare.gui.LIS.QC.Control
{
    public class ctlVendorCombox : ComboBox
    {
        // Fields
        private IContainer components;
        //private clsDcl_QCLisControl m_objDomain;

        // Methods
        public ctlVendorCombox()
        {
            this.components = null; 
            this.InitializeComponent();
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.m_objDomain = new clsDcl_QCLisControl(); 
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private void InitializeComponent()
        {
            this.components = new Container();
            base.Name = "ctlVendorCombox1";
            base.Size = new Size(120, 0x15); 
        }

        private void m_mthLoadData()
        {
            //加入空项
            clsLisVendorVO obj = new clsLisVendorVO();
            obj.m_strVendor = string.Empty;
            obj.m_intSeq = DBAssist.NullInt;
            this.Items.Add(obj);
            //加载数据
            clsLisVendorVO[] objVendorArr = null;
            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFindVendor(out objVendorArr);
            if (lngRes > 0 && objVendorArr != null)
            {
                this.Items.AddRange(objVendorArr);
            } 
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!this.DesignMode)
            {
                try
                {
                    this.m_mthLoadData();
                }
                catch { }
            }
        }
    } 
}
