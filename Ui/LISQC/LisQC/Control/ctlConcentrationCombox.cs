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
    public class ctlConcentrationCombox : ComboBox
    {
        // Fields
        private IContainer components;
        // private clsDcl_QCLisControl m_objDomain;

        // Methods
        public ctlConcentrationCombox()
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
            base.Name = "ctlConcentrationCombox1";
            base.Size = new Size(120, 0x15);
        }

        private void m_mthLoadData()
        {
            clsLisConcentrationVO nvo;
            clsLisConcentrationVO[] nvoArray;
            long num;
            clsLisConcentrationVO nvo2;
            bool flag;
            clsLisConcentrationVO[] nvoArray2;
            int num2;
            nvo = new clsLisConcentrationVO();
            nvo.m_strConcentration = string.Empty;
            nvo.m_intSeq = -2147483648;
            nvo.m_enmStatus = enmQCStatus.Natrural;
            Items.Add(nvo);
            nvoArray = null;

            (new weCare.Proxy.ProxyLis03()).Service.m_lngFindConcentration(out nvoArray);
            if(nvoArray == null)
            {
                goto Label_009B;
            }
            nvoArray2 = nvoArray;
            num2 = 0;
            goto Label_008C;
        Label_005D:
            nvo2 = nvoArray2[num2];
            if (nvo2.m_enmStatus == enmQCStatus.Delete)
            {
                goto Label_0085;
            }
            Items.Add(nvo2);
        Label_0085:
            num2 += 1;
        Label_008C:
            if (num2 < ((int)nvoArray2.Length))
            {
                goto Label_005D;
            }
        Label_009B:
            return;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                this.m_mthLoadData();
            }
        }

        // Properties
        public clsLisConcentrationVO SelectedConcentration
        {
            get
            {
                if (SelectedItem == null || ((clsLisConcentrationVO)base.SelectedItem).m_intSeq == -1)
                    return null;
                else
                    return (clsLisConcentrationVO)SelectedItem; 
            }
        }

        public int Value
        {
            get
            {
                if (SelectedItem == null)
                    return -2147483648;
                else
                    return ((clsLisConcentrationVO)SelectedItem).m_intSeq;
            }
            set
            {
                clsLisConcentrationVO nvo;
                IEnumerator enumerator;
                bool flag;
                IDisposable disposable;
                base.SelectedItem = null;
                enumerator = base.Items.GetEnumerator();
            Label_0016:
                try
                {
                    goto Label_0041;
                Label_0018:
                    nvo = (clsLisConcentrationVO)enumerator.Current;
                    if (nvo.m_intSeq != value)
                    {
                        goto Label_0040;
                    }
                    base.SelectedItem = nvo;
                    goto Label_004B;
                Label_0040:;
                Label_0041:
                    if (enumerator.MoveNext())
                    {
                        goto Label_0018;
                    }
                Label_004B:
                    goto Label_0064;
                }
                finally
                {
                Label_004D:
                    disposable = enumerator as IDisposable;
                    if ((disposable == null))
                    {
                        goto Label_0063;
                    }
                    disposable.Dispose();
                Label_0063:;
                }
            Label_0064:
                return;
            }
        }
    }
}
