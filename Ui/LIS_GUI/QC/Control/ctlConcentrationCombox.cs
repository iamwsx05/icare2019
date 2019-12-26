using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    public partial class ctlConcentrationCombox : System.Windows.Forms.ComboBox
    {
        public ctlConcentrationCombox()
        {
            InitializeComponent();
            this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
        private void m_mthLoadData()
        {
            //�������
            clsLisConcentrationVO obj = new clsLisConcentrationVO();
            obj.m_strConcentration = string.Empty;
            obj.m_intSeq = DBAssist.NullInt;
            obj.m_enmStatus = enmQCStatus.Natrural;
            this.Items.Add(obj);
            //��������
            clsLisConcentrationVO[] objConcentrationArr = null;
            long lngRes = clsTmdConcentrationSmp.s_object.m_lngFind(out objConcentrationArr);
            if (lngRes > 0 && objConcentrationArr != null)
            {
                foreach (clsLisConcentrationVO objConcentration in objConcentrationArr)
                {
                    if (objConcentration.m_enmStatus == enmQCStatus.Natrural)
                    {
                        this.Items.Add(objConcentration);
                    }
                }
            }
        }
        public int Value
        {
            get
            {
                if (this.SelectedItem != null)
                    return ((clsLisConcentrationVO)this.SelectedItem).m_intSeq;
                return DBAssist.NullInt;
            }
            set
            {
                this.SelectedItem = null;
                foreach (clsLisConcentrationVO obj in this.Items)
                {
                    if (obj.m_intSeq == value)
                    {
                        this.SelectedItem = obj;
                        break;
                    }
                }
            }
        }
        public clsLisConcentrationVO SelectedConcentration
        {
            get
            {
                if (this.SelectedItem != null && (((clsLisConcentrationVO)this.SelectedItem).m_intSeq != -1))
                    return (clsLisConcentrationVO)this.SelectedItem;
                return null;
            }
        }
    }
}
