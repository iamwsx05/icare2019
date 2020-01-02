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
    public class ctlCheckMethodCombox : ComboBox
    {
        // Fields
        private IContainer components;
        //private clsDcl_QCLisControl m_objDomain;

        // Methods
        public ctlCheckMethodCombox()
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
            base.Name = "ctlCheckMethodCombox1";
            base.Size = new Size(120, 0x15); 
        }

        public void m_mthLoadData()
        {
            //加入空项
            clsLisCheckMethodVO obj = new clsLisCheckMethodVO();
            obj.m_strName = string.Empty;
            obj.m_intSeq = DBAssist.NullInt;
            this.Items.Add(obj);
            //加载数据
            clsLisCheckMethodVO[] objCheckMethodArr = null;
            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFindCheckMethod(out objCheckMethodArr);
            if (lngRes > 0 && objCheckMethodArr != null)
            {
                this.Items.AddRange(objCheckMethodArr);
            } 
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!this.DesignMode)
            {
                try
                {
                    m_mthLoadData();
                }
                catch { }
            }
        }
    }
}
