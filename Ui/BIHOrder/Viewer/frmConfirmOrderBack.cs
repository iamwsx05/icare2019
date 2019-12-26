using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ������ҩͬ���ŵ��ж��߼�
    /// </summary>
    public partial class frmConfirmOrderBack : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ��������
        /// </summary>
        internal string m_strPatientName = "";
        /// <summary>
        /// ��������	{���磺ֹͣ��������}
        /// </summary>
        internal string m_strOperateName = "";
        /// <summary>
        /// ҽ���ɣ�
        /// </summary>
        internal string m_strOrderID = "";
        /// <summary>
        /// ҽ������
        /// </summary>
        internal string m_strOrderName = "";

        internal bool IsChildPrice { get; set; }

        public frmConfirmOrderBack()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���캯��	
        /// </summary>
        /// <param name="m_strOrderID">ҽ���ɣ�</param>
        public frmConfirmOrderBack(string p_strOrderID,string p_strOrderName)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            m_strOrderID = p_strOrderID;
            m_strOrderName = p_strOrderName;

        }

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.BIHOrder.clsCtl_ConfirmOrderBack();
            objController.Set_GUI_Apperance(this);
        }
        private void m_cmdOk_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmConfirmOrderBack_Load(object sender, EventArgs e)
        {
            ((clsCtl_ConfirmOrderBack)this.objController).LoadData();
        }
    }
}