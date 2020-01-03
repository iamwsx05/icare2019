using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Utility;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// Э������ά��UI
    /// </summary>
    public partial class frmAccordRecipeList : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ����
        /// <summary>
        /// ��ǰ���ڵ�
        /// </summary>
        internal TreeNode CurrTn;
        /// <summary>
        /// ��ҩ��Ŀ����
        /// </summary>
        internal ArrayList Arr1 = new ArrayList();
        /// <summary>
        /// ��ҩ��Ŀ����
        /// </summary>
        public ArrayList ItemArr1
        {
            get
            {
                return Arr1;
            }
        }
        /// <summary>
        /// ��ҩ��Ŀ����
        /// </summary>
        internal ArrayList Arr2 = new ArrayList();
        /// <summary>
        /// ��ҩ��Ŀ����
        /// </summary>
        public ArrayList ItemArr2
        {
            get
            {
                return Arr2;
            }
        }
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        internal ArrayList Arr3 = new ArrayList();
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        public ArrayList ItemArr3
        {
            get
            {
                return Arr3;
            }
        }
        /// <summary>
        /// �����Ŀ����
        /// </summary>
        internal ArrayList Arr4 = new ArrayList();
        /// <summary>
        /// �����Ŀ����
        /// </summary>
        public ArrayList ItemArr4
        {
            get
            {
                return Arr4;
            }
        }
        /// <summary>
        /// ����������Ŀ����
        /// </summary>
        internal ArrayList Arr5 = new ArrayList();
        /// <summary>
        /// ����������Ŀ����
        /// </summary>
        public ArrayList ItemArr5
        {
            get
            {
                return Arr5;
            }
        }
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        internal ArrayList Arr6 = new ArrayList();
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        public ArrayList ItemArr6
        {
            get
            {
                return Arr6;
            }
        }
        /// <summary>
        /// �ⲿ����ģʽ
        /// </summary>
        private bool ExtFindMode = false;
        /// <summary>
        /// ��������
        /// </summary>
        private string FindStr = "";
        #endregion

        #region ����CTL����
        /// <summary>
        /// ����CTL����
        /// </summary>
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_AccordRecipeList();
            objController.Set_GUI_Apperance(this);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public frmAccordRecipeList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="blnExtFindMode">�Ƿ��ⲿ����ģʽ</param>
        /// <param name="strFindStr">��������</param>
        public frmAccordRecipeList(bool blnExtFindMode, string strFindStr, bool _isChildPrice)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                ExtFindMode = blnExtFindMode;
                FindStr = strFindStr;
                ((clsCtl_AccordRecipeList)this.objController).isChildPrice = _isChildPrice;
            }
        }
        #endregion

        private void frmAccordRecipeList_Load(object sender, EventArgs e)
        {
            ((clsCtl_AccordRecipeList)this.objController).m_mthCreateTree(this.LoginInfo.m_strEmpID);

            if (ExtFindMode)
            {
                ((clsCtl_AccordRecipeList)this.objController).m_mthFindTree(FindStr, true);
            }
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string FindStr = this.txtFind.Text.Trim();

                if (FindStr == "")
                {
                    return;
                }

                //((clsCtl_AccordRecipeList)this.objController).m_mthFindTree(FindStr, false);
                ((clsCtl_AccordRecipeList)this.objController).m_mthFindTree(FindStr, true);
            }
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name.ToString().ToLower().StartsWith("child"))
            {
                this.CurrTn = e.Node;
                AccordRecipeEdit obj = e.Node.Tag as AccordRecipeEdit;
                ((clsCtl_AccordRecipeList)this.objController).m_mthShow(obj.RecipeID_Chr);
            }
        }

        private void frmAccordRecipeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                this.btnOk_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (((clsCtl_AccordRecipeList)this.objController).m_blnChoose())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("��ѡ����Ŀ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dtgOrderItem.Rows.Count; i++)
            {
                DataRow dr = this.dtgOrderItem.Rows[i].Tag as DataRow;
                if (clsPublic.ConvertObjToDecimal(dr["ifstop_int"].ToString()) != 0)
                    this.dtgOrderItem.Rows[i].Cells[0].Value = "F";
                else if (clsPublic.ConvertObjToDecimal(dr["noqtyflag_int"].ToString()) != 0)
                    this.dtgOrderItem.Rows[i].Cells[0].Value = "F";
                else
                    this.dtgOrderItem.Rows[i].Cells[0].Value = "T";
            }
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dtgOrderItem.Rows.Count; i++)
            {
                if (this.dtgOrderItem.Rows[i].Cells[0].Value.ToString() == "T")
                {
                    this.dtgOrderItem.Rows[i].Cells[0].Value = "F";
                }
                else
                {
                    DataRow dr = this.dtgOrderItem.Rows[i].Tag as DataRow;
                    if (clsPublic.ConvertObjToDecimal(dr["ifstop_int"].ToString()) != 0)
                        this.dtgOrderItem.Rows[i].Cells[0].Value = "F";
                    else if (clsPublic.ConvertObjToDecimal(dr["noqtyflag_int"].ToString()) != 0)
                        this.dtgOrderItem.Rows[i].Cells[0].Value = "F";
                    else
                        this.dtgOrderItem.Rows[i].Cells[0].Value = "T";
                }
            }
        }
    }
}