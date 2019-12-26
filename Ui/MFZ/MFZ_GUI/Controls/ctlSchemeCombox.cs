using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.MFZ.Controls
{
    /// <summary>
    /// 班次选择控件
    /// </summary>
    public partial class ctlSchemeCombox : ComboBox
    {

        #region 构造函数

        public ctlSchemeCombox()
        {
            InitializeComponent();
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public ctlSchemeCombox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        } 

        #endregion

        #region 属 性

        private int m_intScheme; //班次ID
        public int SchemeID
        {
            get
            {
                if (SelectedItem != null)
                {
                    clsMFZSchemeVO scheme = SelectedItem as clsMFZSchemeVO;
                    return scheme.m_intSchemeSeq;
                }
                return DBAssist.NullInt;
            }
            set
            {
                this.SelectedItem = null;
                m_intScheme = value;
                foreach (clsMFZSchemeVO scheme in Items)
                {
                    if (scheme.m_intSchemeSeq == m_intScheme)
                    {
                        this.SelectedItem = scheme;
                    }
                }
            }

        }
        public clsMFZSchemeVO SelectedSheme 
        {
            get
            {
                if (this.SelectedItem != null && ((clsMFZSchemeVO)this.SelectedItem).m_intSchemeSeq != DBAssist.NullInt)
                {
                    return (clsMFZSchemeVO)SelectedItem;
                }
                return null;
            }
        } 

        #endregion

        #region 加载数据

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

        /// <summary>
        /// 加载数据
        /// </summary>
        private void m_mthLoadData()
        {
            this.Items.Clear();
            //clsMFZSchemeVO scheme = new clsMFZSchemeVO();
            //scheme.m_intSchemeSeq = 0;
            //scheme.m_strSchemeDesc = string.Empty;
            //scheme.m_dtBegin = DBAssist.NullDateTime;
            //scheme.m_dtEnd = DBAssist.NullDateTime;
            //Items.Add(scheme);

            clsMFZSchemeVO[] arrSchemes;
            clsTmdSchemeSmp.s_object.m_lngFind(out arrSchemes);
            if (arrSchemes == null)
            {
                arrSchemes = new clsMFZSchemeVO[0];
            }
            foreach (clsMFZSchemeVO objScheme in arrSchemes)
            {
                Items.Add(objScheme);
            }
            if (Items.Count != 0)
            {
                this.SelectedIndex = 0;
            }
        } 

        #endregion

    }
}
