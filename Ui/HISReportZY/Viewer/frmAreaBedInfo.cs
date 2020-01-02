using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    /// <summary>
    /// ����һ��
    /// </summary>
    public partial class frmAreaBedInfo : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ����
        private DataTable dtReocrd = new DataTable();
        internal int CurrRow = -1;
        /// <summary>
        /// ������Ժ�Ǽ���ˮ��
        /// </summary>
        internal string regid = "";
        /// <summary>
        /// ����ID
        /// </summary>
        internal string pid = "";
        /// <summary>
        /// סԺ��
        /// </summary>
        internal string Zyh = "";
        /// <summary>
        /// סԺ����
        /// </summary>
        internal int Zycs = 0;
        /// <summary>
        /// ���ƿ���
        /// </summary>
        internal string CardNo = "";
        /// <summary>
        /// ��������
        /// </summary>
        internal string patname = "";
        /// <summary>
        /// ����(����)��ʾ��Χ 0 ȫ�� 1 ����
        /// </summary>
        internal int ShowScope = 0;
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="dt"></param>
        public frmAreaBedInfo(DataTable dt, int scope)
        {
            InitializeComponent();
            dtReocrd = dt;
            ShowScope = scope;
        }

        /// <summary>
        /// ����������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.Reports.clsCtl_AreaBedInfo();
            objController.Set_GUI_Apperance(this);
        }

        private void frmAreaBedInfo_Load(object sender, EventArgs e)
        {
            ((clsCtl_AreaBedInfo)this.objController).m_mthLoadArea(dtReocrd);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmAreaBedInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void tvArea_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string areaid = e.Node.Tag.ToString();

            if (areaid.Trim() == "" || areaid.ToLower() == "root")
            {
                this.CurrRow = -1;
                return;
            }
            else
            {                
                ((clsCtl_AreaBedInfo)this.objController).m_mthShowBedInfo(areaid);
            }            
        }

        private void dgBed_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CurrRow = e.RowIndex;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ((clsCtl_AreaBedInfo)this.objController).m_mthGetZyh(CurrRow);
        }

        private void dgBed_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ((clsCtl_AreaBedInfo)this.objController).m_mthGetZyh(CurrRow);
        }
    }
}