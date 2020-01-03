using System;
//using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using System.Windows.Forms;
using System.Windows;

namespace iCare
{
    /// <summary>
    /// Summary description for frmGetAllRuningForms.
    /// </summary>
    public class frmGetAllRuningForms : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ListBox m_lstForms;
        private PinkieControls.ButtonXP m_cmdGetAllForm;
        private System.Windows.Forms.ListBox m_lstControls;
        private PinkieControls.ButtonXP m_cmdGetDescription;
        //private clsGetAllRuningFormsServ m_objServ = new clsGetAllRuningFormsServ();

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmGetAllRuningForms()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_lstForms = new System.Windows.Forms.ListBox();
            this.m_cmdGetAllForm = new PinkieControls.ButtonXP();
            this.m_lstControls = new System.Windows.Forms.ListBox();
            this.m_cmdGetDescription = new PinkieControls.ButtonXP();
            this.SuspendLayout();
            // 
            // m_lstForms
            // 
            this.m_lstForms.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_lstForms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lstForms.ForeColor = System.Drawing.Color.White;
            this.m_lstForms.HorizontalScrollbar = true;
            this.m_lstForms.ItemHeight = 16;
            this.m_lstForms.Location = new System.Drawing.Point(0, 0);
            this.m_lstForms.Name = "m_lstForms";
            this.m_lstForms.Size = new System.Drawing.Size(404, 400);
            this.m_lstForms.TabIndex = 0;
            // 
            // m_cmdGetAllForm
            // 
            this.m_cmdGetAllForm.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_cmdGetAllForm.DefaultScheme = true;
            this.m_cmdGetAllForm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetAllForm.Font = new System.Drawing.Font("����", 10.5F);
            this.m_cmdGetAllForm.ForeColor = System.Drawing.Color.White;
            this.m_cmdGetAllForm.Hint = "";
            this.m_cmdGetAllForm.Location = new System.Drawing.Point(448, 412);
            this.m_cmdGetAllForm.Name = "m_cmdGetAllForm";
            this.m_cmdGetAllForm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetAllForm.Size = new System.Drawing.Size(128, 32);
            this.m_cmdGetAllForm.TabIndex = 1;
            this.m_cmdGetAllForm.Text = "ģ���������";
            this.m_cmdGetAllForm.Click += new System.EventHandler(this.m_cmdGetAllForm_Click);
            // 
            // m_lstControls
            // 
            this.m_lstControls.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_lstControls.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lstControls.ForeColor = System.Drawing.Color.White;
            this.m_lstControls.HorizontalScrollbar = true;
            this.m_lstControls.ItemHeight = 16;
            this.m_lstControls.Location = new System.Drawing.Point(412, 0);
            this.m_lstControls.Name = "m_lstControls";
            this.m_lstControls.Size = new System.Drawing.Size(404, 400);
            this.m_lstControls.TabIndex = 3;
            // 
            // m_cmdGetDescription
            // 
            this.m_cmdGetDescription.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_cmdGetDescription.DefaultScheme = true;
            this.m_cmdGetDescription.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdGetDescription.Font = new System.Drawing.Font("����", 10.5F);
            this.m_cmdGetDescription.ForeColor = System.Drawing.Color.White;
            this.m_cmdGetDescription.Hint = "";
            this.m_cmdGetDescription.Location = new System.Drawing.Point(632, 412);
            this.m_cmdGetDescription.Name = "m_cmdGetDescription";
            this.m_cmdGetDescription.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdGetDescription.Size = new System.Drawing.Size(148, 32);
            this.m_cmdGetDescription.TabIndex = 1;
            this.m_cmdGetDescription.Text = "ר�Ʋ�����������";
            this.m_cmdGetDescription.Click += new System.EventHandler(this.m_cmdGetDescription_Click);
            // 
            // frmGetAllRuningForms
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.ClientSize = new System.Drawing.Size(820, 481);
            this.Controls.Add(this.m_lstControls);
            this.Controls.Add(this.m_cmdGetAllForm);
            this.Controls.Add(this.m_lstForms);
            this.Controls.Add(this.m_cmdGetDescription);
            this.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.Name = "frmGetAllRuningForms";
            this.Text = "frmGetAllRuningForms";
            this.ResumeLayout(false);

        }
        #endregion

        private void m_cmdGetAllForm_Click(object sender, System.EventArgs e)
        {

            Form frmMDI = clsEMRLogin.s_FrmMDI;

            //clsGetAllRuningFormsServ m_objServ =
            //    (clsGetAllRuningFormsServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsGetAllRuningFormsServ));

            Form[] frmRunningFormArr = frmMDI.MdiChildren;
            this.m_lstForms.Items.Clear();
            this.m_lstControls.Items.Clear();
            for (int i = 0; i < frmRunningFormArr.Length; i++)
            {
                this.m_lstForms.Items.Add(frmRunningFormArr[i].Name + "," + frmRunningFormArr[i].AccessibleDescription);

                (new weCare.Proxy.ProxyEmr01()).Service.clsGetAllRuningFormsServ_m_lngDelGUI_Info_Detail(frmRunningFormArr[i].Name);
                (new weCare.Proxy.ProxyEmr01()).Service.clsGetAllRuningFormsServ_m_lngSaveGUI_Info(frmRunningFormArr[i].Name, frmRunningFormArr[i].AccessibleDescription);
                foreach (Control ctlChild in frmRunningFormArr[i].Controls)
                {
                    m_mthOldSetDescription(frmRunningFormArr[i].Name, ctlChild);
                }

            }

        }

        private void m_mthOldSaveDescription(string p_strFormName, Control p_ctlContent)
        {
            string strCtlName = p_ctlContent.GetType().Name;
            long lngRef = 0;
            if (!p_ctlContent.HasChildren || strCtlName == "ctlComboBox")
            {
                //clsGetAllRuningFormsServ m_objServ =
                //    (clsGetAllRuningFormsServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsGetAllRuningFormsServ));

                switch (strCtlName)
                {
                    case "TextBox":
                    case "RichTextBox":
                    case "ctlComboBox":
                    case "ctlRichTextBox":
                        lngRef = (new weCare.Proxy.ProxyEmr01()).Service.clsGetAllRuningFormsServ_m_lngSaveGUI_Info_Detail(p_strFormName, p_ctlContent.AccessibleDescription,
                            p_ctlContent.Name, p_ctlContent.TabIndex.ToString());
                        if (lngRef > 0)
                            this.m_lstControls.Items.Add(p_strFormName + "." + p_ctlContent.Name);
                        else
                            this.m_lstControls.Items.Add("ERROR: " + p_strFormName + "." + p_ctlContent.Name);
                        break;
                }
            }
            else
                foreach (Control ctlChild in p_ctlContent.Controls)
                    m_mthOldSetDescription(p_strFormName, ctlChild);

        }


        /// <summary>
        /// �ݹ��ȡ���пؼ�����,��ר�Ʋ�����
        /// </summary>
        /// <param name="p_strFormName"></param>
        /// <param name="p_ctlContent"></param>
        private void m_mthOldSetDescription(string p_strFormName, Control p_ctlContent)
        {
            string strCtlName = p_ctlContent.GetType().Name;
            if (!p_ctlContent.HasChildren || strCtlName == "ctlComboBox")
            {
                switch (strCtlName)
                {
                    case "TextBox":
                    case "ctlComboBox":
                    case "RichTextBox":
                    case "ctlRichTextBox":
                        m_mthOldSaveDescription(p_strFormName, p_ctlContent);
                        break;
                }
            }
            else
                foreach (Control ctlChild in p_ctlContent.Controls)
                    m_mthOldSetDescription(p_strFormName, ctlChild);
        }

        private void m_cmdGetDescription_Click(object sender, System.EventArgs e)
        {
            Form frmMDI = clsEMRLogin.s_FrmMDI;

            Form[] frmRunningFormArr = frmMDI.MdiChildren;
            this.m_lstForms.Items.Clear();
            this.m_lstControls.Items.Clear();

            //clsGetAllRuningFormsServ m_objServ =
            //    (clsGetAllRuningFormsServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsGetAllRuningFormsServ));

            for (int i = 0; i < frmRunningFormArr.Length; i++)
            {
                if (frmRunningFormArr[i].Name != "Form_HRPExplorer" && frmRunningFormArr[i].Name != "frmGetAllRuningForms")
                {
                    this.m_lstForms.Items.Add(frmRunningFormArr[i].Name + "," + frmRunningFormArr[i].AccessibleDescription);

                    (new weCare.Proxy.ProxyEmr01()).Service.clsGetAllRuningFormsServ_m_lngDel_IMR_Type_Item(frmRunningFormArr[i].Name);
                    foreach (Control ctlChild in frmRunningFormArr[i].Controls)
                    {
                        if (ctlChild.Name == "m_pnlContent")
                            m_mthSetDescription(frmRunningFormArr[i].Name, ctlChild);
                    }
                }

            }

        }

        /// <summary>
        /// �ݹ��ȡ���пؼ�����,��ר�Ʋ�����
        /// </summary>
        /// <param name="p_strTypeID">סԺ����������</param>
        /// <param name="p_ctlContent">���ؼ�</param>
        private void m_mthSetDescription(string p_strTypeID, Control p_ctlContent)
        {
            if (p_ctlContent == null || p_ctlContent.HasChildren == false)
                return;
            long lngRef = 0;

            //clsGetAllRuningFormsServ m_objServ =
            //    (clsGetAllRuningFormsServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsGetAllRuningFormsServ));

            foreach (Control ctlChild in p_ctlContent.Controls)
            {
                switch (ctlChild.GetType().Name)
                {
                    case "DateTimePicker":
                    case "TextBox":
                    case "RichTextBox":
                    case "ctlRichTextBox":
                    case "ctlComboBox":
                    case "ComboBox":
                    case "RadioButton":
                    case "ctlTimePicker":
                    case "ctlBorderTextBox":
                    case "CheckBox":
                        lngRef = (new weCare.Proxy.ProxyEmr01()).Service.clsGetAllRuningFormsServ_m_lngSaveIMR_Type_Item(p_strTypeID, ctlChild.Name, ctlChild.AccessibleDescription,
                            ctlChild.GetType().Name, ctlChild.TabIndex.ToString());
                        if (lngRef > 0)
                            this.m_lstControls.Items.Add(p_strTypeID + "." + ctlChild.Name + "," + ctlChild.AccessibleDescription);
                        else
                            this.m_lstControls.Items.Add("ERROR: " + p_strTypeID + "." + ctlChild.Name + "," + ctlChild.AccessibleDescription);
                        break;
                    case "GroupBox":
                        m_mthSetDescription(p_strTypeID, ctlChild);
                        break;
                    case "Panel":
                        m_mthSetDescription(p_strTypeID, ctlChild);
                        break;
                    case "TabControl":
                        m_mthSetDescription(p_strTypeID, ctlChild);
                        break;
                    case "TabPage":
                        m_mthSetDescription(p_strTypeID, ctlChild);
                        break;
                    default:
                        break;
                }
            }


        }

    }
}
