using Common.Entity;
using Common.Utils;
using weCare.Core.Entity;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    public partial class frmSetSelfDefineColumn : System.Windows.Forms.Form
    {
        private string m_strColCode = string.Empty;
        private string m_strColCaption = string.Empty;
        private int m_intPageNo = 1;
        public string strColCaption { get; set; }
        public string strOrgColCaption { get; set; }

        public frmSetSelfDefineColumn(string p_strColCode, string p_strColCaption, int p_intPageNo)
        {
            InitializeComponent();
            this.m_strColCode = p_strColCode;
            this.m_strColCaption = p_strColCaption;
            this.m_intPageNo = p_intPageNo;
            this.txtColCaption.Text = p_strColCaption;
            this.strOrgColCaption = p_strColCaption;
        }

        private void frmSetSelfDefineColumn_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X, this.Location.Y - 50);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (clsDialog.Msg("是否保存自定义列信息？", MessageBoxIcon.Question) == DialogResult.Yes)
            if (GlobalPatient.currPatient == null || string.IsNullOrEmpty(GlobalPatient.currPatient.RegisterID)) return;

            int intRet = 0;
            string strColDesc = this.txtColCaption.Text;

            EntityEmrSelfDefineCol vo = new EntityEmrSelfDefineCol();
            vo.registerId = GlobalPatient.currPatient.RegisterID;
            vo.caseCode = GlobalCase.caseInfo.CaseCode;
            vo.colCode = this.m_strColCode;
            vo.colDesc = strColDesc;
            vo.pageNo = this.m_intPageNo;
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                try
                {
                    intRet = proxy.Service.DeleteByPk(vo);
                    intRet = proxy.Service.Insert(vo);
                    if (intRet > 0)
                    {
                        DialogBox.Msg("保存成功！");
                        strColCaption = strColDesc;
                        strOrgColCaption = strColCaption;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        DialogBox.Msg("保存失败.", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    DialogBox.Msg("保存失败." + ex.Message, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogBox.Msg("是否删除自定义列信息？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EntityEmrSelfDefineCol vo = new EntityEmrSelfDefineCol();
                vo.registerId = GlobalPatient.currPatient.RegisterID;
                vo.caseCode = GlobalCase.caseInfo.CaseCode;
                vo.colCode = this.m_strColCode;
                vo.pageNo = this.m_intPageNo;
                using (ProxyEntityFactory proxy = new ProxyEntityFactory())
                {
                    int intRet = proxy.Service.DeleteByPk(vo);
                    if (intRet > 0)
                    {
                        DialogBox.Msg("删除成功！");
                    }
                    else
                    {
                        DialogBox.Msg("删除失败.", MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
