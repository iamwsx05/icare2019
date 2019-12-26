using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

    public class ctlLISDeviceComboBox : ComboBox
    {
        // Fields
        private Container components;
        // private clsDcl_LisControl m_objDomain;

        // Methods
        public ctlLISDeviceComboBox()
        {
            this.components = null;
            this.InitializeComponent();
            this.m_mthGetData();
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
            base.Name = "UserControl1";
            base.Size = new Size(120, 0x15);
        }

        private void m_mthGetData()
        {
            try
            {
                DataTable dtbDevice = null;
                (new weCare.Proxy.ProxyLis01()).Service.m_lngGetDeviceList(out dtbDevice);
                if (dtbDevice != null)
                {
                    DataView dtv = new DataView(dtbDevice);
                    this.DataSource = dtv;
                    this.DisplayMember = "DEVICENAME_VCHR";
                    this.ValueMember = "DEVICEID_CHR";
                }
            }
            catch
            {
                this.DataSource = null;
                this.DisplayMember = null;
                this.ValueMember = null;
            }
        }

        public void m_mthRefreshData()
        {
            try
            {
                DataTable dtbDevice = null;
                (new weCare.Proxy.ProxyLis01()).Service.m_lngGetDeviceList(out dtbDevice);
                if (dtbDevice != null)
                {
                    DataView dtv = (DataView)this.DataSource;
                    dtv.Table = dtbDevice;
                }
            }
            catch
            {
            }
        }

        public void m_mthShowDeviceByModelID(string[] p_strDeviceModelIDArr)
        {
            DataView view;
            string str;
            StringBuilder builder;
            int num;
            string str2;
        Label_0003:
            return;
        }

        public string m_strGetDeviceName(string p_strDeviceID)
        {
            DataView view;
            DataRow row;
            string str;
            bool flag;
            IEnumerator enumerator;
            IDisposable disposable;
            if (base.DataSource == null)
            {
                goto Label_00A7;
            }
            view = (DataView)base.DataSource;
            enumerator = view.Table.Rows.GetEnumerator();
        Label_0031:
            try
            {
                goto Label_007D;
            Label_0033:
                row = (DataRow)enumerator.Current;
                if (row["DEVICEID_CHR"].ToString().Trim() != p_strDeviceID)
                {
                    goto Label_007C;
                }
                str = row["DEVICENAME_VCHR"].ToString().Trim();
                goto Label_00AF;
            Label_007C:;
            Label_007D:
                if (enumerator.MoveNext())
                {
                    goto Label_0033;
                }
                goto Label_00A5;
            }
            finally
            {
            Label_008A:
                disposable = enumerator as IDisposable;
                if ((disposable == null))
                {
                    goto Label_00A4;
                }
                disposable.Dispose();
            Label_00A4:;
            }
        Label_00A5:;
        Label_00A7:
            str = "";
        Label_00AF:
            return str;
        }
    }
}
