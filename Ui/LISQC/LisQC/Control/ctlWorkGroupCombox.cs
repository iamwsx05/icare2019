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
    public class ctlWorkGroupCombox : ComboBox
    {
        // Fields
        private IContainer components;
        //private clsDcl_QCLisControl m_objDomain;

        // Methods
        public ctlWorkGroupCombox()
        {
            this.components = null; 
            this.InitializeComponent();
            base.DropDownStyle = ComboBoxStyle.DropDownList;
           // this.m_objDomain = new clsDcl_QCLisControl(); 
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
            base.Name = "clsWorkGroupCombox1";
            base.Size = new Size(120, 0x15); 
        }

        private void m_mthLoadData()
        {
            //加入空项
            clsLisWorkGroupVO obj = new clsLisWorkGroupVO();
            obj.m_strName = string.Empty;
            obj.m_intSeq = DBAssist.NullInt;
            obj.m_enmStatus = enmQCStatus.Natrural;
            this.Items.Add(obj);
            //加载数据
            clsLisWorkGroupVO[] objGroupArr = null;
            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngFindWorkGroup(out objGroupArr);
            if (lngRes > 0 && objGroupArr != null)
            {
                foreach (clsLisWorkGroupVO objGroup in objGroupArr)
                {
                    if (objGroup.m_enmStatus == enmQCStatus.Natrural)
                    {
                        this.Items.Add(objGroup);
                    }
                }
            } 
        }

        protected void OnHandleCreated(EventArgs e)
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

        // Properties
        public clsLisWorkGroupVO SelectedWorkGroup
        {
            get
            {
                if (this.SelectedItem != null && (((clsLisWorkGroupVO)this.SelectedItem).m_intSeq != -1))
                    return (clsLisWorkGroupVO)this.SelectedItem;
                return null;
            }
        }

        public int Value
        {
            get
            {
                if (this.SelectedItem != null)
                    return ((clsLisWorkGroupVO)this.SelectedItem).m_intSeq;
                return DBAssist.NullInt;
            }
            set
            {
                this.SelectedItem = null;
                foreach (clsLisWorkGroupVO objGroup in this.Items)
                {
                    if (objGroup.m_intSeq == value)
                    {
                        this.SelectedItem = objGroup;
                        break;
                    }
                }
            }
        }
    }
}
