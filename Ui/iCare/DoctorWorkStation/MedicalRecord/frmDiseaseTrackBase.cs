using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using HRP;
using com.digitalwave.emr.DigitalSign;
using com.digitalwave.Emr.Signature_gui;
//using iCare.ICU.Espial; 
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    ///  ���̼�¼�������¼���Ļ����塣
    /// �ڴ˴��崦���桢�޸ġ�ɾ�������������������ۼ���˫���߹�����ս����ͨ���߼���
    /// ���ò�����Ϣ�ͻ�ȡ������Ϣ��ͨ���߼���
    /// ��ӡ���ܣ���ȡ�Ѿ�ɾ���ļ�¼��
    /// </summary>
    public class frmDiseaseTrackBase : frmHRPBaseForm, PublicFunction
    {

        #region Designer generated code
        public System.Windows.Forms.TreeView m_trvCreateDate;
        protected System.Windows.Forms.Label lblCreateDateTitle;
        protected com.digitalwave.Utility.Controls.ctlTimePicker m_dtpCreateDate;
        protected System.Windows.Forms.ContextMenu m_cmuRichTextBoxMenu;
        protected System.Windows.Forms.MenuItem mniDoubleStrikeOutDelete;
        protected System.Drawing.Printing.PrintDocument m_pdtPintDocument;
        protected com.digitalwave.Utility.Controls.ctlTimePicker m_dtpGetDataTime;
        protected System.Windows.Forms.Label m_lblGetDataTime;
        private System.ComponentModel.IContainer components = null;







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


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCreateDateTitle = new System.Windows.Forms.Label();
            this.m_dtpCreateDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.m_trvCreateDate = new System.Windows.Forms.TreeView();
            this.m_cmuRichTextBoxMenu = new System.Windows.Forms.ContextMenu();
            this.mniDoubleStrikeOutDelete = new System.Windows.Forms.MenuItem();
            this.m_pdtPintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_dtpGetDataTime = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.m_lblGetDataTime = new System.Windows.Forms.Label();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(32, 80);
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(582, 86);
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(80, 76);
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(721, 80);
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(80, 40);
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(32, 48);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.ForeColor = System.Drawing.Color.Black;
            this.m_lblForTitle.Location = new System.Drawing.Point(272, 16);
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(448, 40);
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // lblCreateDateTitle
            // 
            this.lblCreateDateTitle.AutoSize = true;
            this.lblCreateDateTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCreateDateTitle.ForeColor = System.Drawing.Color.Black;
            this.lblCreateDateTitle.Location = new System.Drawing.Point(268, 120);
            this.lblCreateDateTitle.Name = "lblCreateDateTitle";
            this.lblCreateDateTitle.Size = new System.Drawing.Size(70, 14);
            this.lblCreateDateTitle.TabIndex = 6068;
            this.lblCreateDateTitle.Text = "��¼ʱ��:";
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.BorderColor = System.Drawing.Color.Black;
            this.m_dtpCreateDate.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpCreateDate.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_dtpCreateDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpCreateDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpCreateDate.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpCreateDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpCreateDate.Location = new System.Drawing.Point(348, 116);
            this.m_dtpCreateDate.m_BlnOnlyTime = false;
            this.m_dtpCreateDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpCreateDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpCreateDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpCreateDate.Name = "m_dtpCreateDate";
            this.m_dtpCreateDate.ReadOnly = false;
            this.m_dtpCreateDate.Size = new System.Drawing.Size(212, 22);
            this.m_dtpCreateDate.TabIndex = 11;
            this.m_dtpCreateDate.TextBackColor = System.Drawing.Color.White;
            this.m_dtpCreateDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_trvCreateDate
            // 
            this.m_trvCreateDate.BackColor = System.Drawing.Color.White;
            this.m_trvCreateDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_trvCreateDate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_trvCreateDate.HideSelection = false;
            this.m_trvCreateDate.ItemHeight = 18;
            this.m_trvCreateDate.Location = new System.Drawing.Point(32, 112);
            this.m_trvCreateDate.Name = "m_trvCreateDate";
            this.m_trvCreateDate.Size = new System.Drawing.Size(212, 88);
            this.m_trvCreateDate.TabIndex = 10;
            this.m_trvCreateDate.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_trvCreateDate_AfterSelect);
            // 
            // m_cmuRichTextBoxMenu
            // 
            this.m_cmuRichTextBoxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniDoubleStrikeOutDelete});
            // 
            // mniDoubleStrikeOutDelete
            // 
            this.mniDoubleStrikeOutDelete.Index = 0;
            this.mniDoubleStrikeOutDelete.Text = "˫����ɾ��";
            this.mniDoubleStrikeOutDelete.Click += new System.EventHandler(this.mniDoubleStrikeOutDelete_Click);
            // 
            // m_pdtPintDocument
            // 
            this.m_pdtPintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdtPintDocument_PrintPage);
            this.m_pdtPintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdtPintDocument_EndPrint);
            this.m_pdtPintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdtPintDocument_BeginPrint);
            // 
            // m_dtpGetDataTime
            // 
            this.m_dtpGetDataTime.BorderColor = System.Drawing.Color.Black;
            this.m_dtpGetDataTime.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpGetDataTime.DropButtonBackColor = System.Drawing.SystemColors.Control;
            this.m_dtpGetDataTime.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpGetDataTime.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpGetDataTime.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpGetDataTime.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_dtpGetDataTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpGetDataTime.Location = new System.Drawing.Point(352, 152);
            this.m_dtpGetDataTime.m_BlnOnlyTime = false;
            this.m_dtpGetDataTime.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpGetDataTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpGetDataTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpGetDataTime.Name = "m_dtpGetDataTime";
            this.m_dtpGetDataTime.ReadOnly = false;
            this.m_dtpGetDataTime.Size = new System.Drawing.Size(212, 22);
            this.m_dtpGetDataTime.TabIndex = 10000004;
            this.m_dtpGetDataTime.TextBackColor = System.Drawing.Color.White;
            this.m_dtpGetDataTime.TextForeColor = System.Drawing.Color.Black;
            this.m_dtpGetDataTime.Visible = false;
            // 
            // m_lblGetDataTime
            // 
            this.m_lblGetDataTime.AutoSize = true;
            this.m_lblGetDataTime.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblGetDataTime.ForeColor = System.Drawing.Color.Black;
            this.m_lblGetDataTime.Location = new System.Drawing.Point(248, 152);
            this.m_lblGetDataTime.Name = "m_lblGetDataTime";
            this.m_lblGetDataTime.Size = new System.Drawing.Size(98, 14);
            this.m_lblGetDataTime.TabIndex = 6068;
            this.m_lblGetDataTime.Text = "��ȡ����ʱ��:";
            this.m_lblGetDataTime.Visible = false;
            // 
            // frmDiseaseTrackBase
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(996, 733);
            this.Controls.Add(this.m_dtpGetDataTime);
            this.Controls.Add(this.lblCreateDateTitle);
            this.Controls.Add(this.m_trvCreateDate);
            this.Controls.Add(this.m_dtpCreateDate);
            this.Controls.Add(this.m_lblGetDataTime);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "frmDiseaseTrackBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDiseaseTrackBase_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblGetDataTime, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.m_trvCreateDate, 0);
            this.Controls.SetChildIndex(this.lblCreateDateTitle, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_dtpGetDataTime, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region �ֶ�
        // ���̼�¼�������ʵ��
        protected clsDiseaseTrackDomain m_objDiseaseTrackDomain;

        protected clsTrackRecordContent m_objReAddNewOld;

        // ���浱ǰ��ʾ�ļ�¼���ݵı���
        protected clsTrackRecordContent m_objCurrentRecordContent;

        protected TreeNode m_trnRoot;
        /// <summary>
        /// ������
        /// </summary>
        protected clsPatient m_objCurrentPatient;
        /// <summary>
        /// ����
        /// </summary>
        protected string m_strOpenDate;

        protected clsBorderTool m_objBorderTool;

        // ��ӡ����������ĵ�
        protected PrintDocument m_pdcPrintDocument;
        //��һ�δ�ӡʱ��
        protected DateTime m_dtmFirstPrintDate;

        // ����Ƿ��״δ�ӡ
        protected bool m_blnIsFirstPrint;

        protected bool m_blnAlreadySetPrintTools = false;

        public bool m_blnIsAddNew = false; //�Ƿ������¼--wf20080121

        /// <summary>
        /// �Ƿ���Դ������ڵ��ѡ���¼�
        /// </summary>
        protected bool m_blnCanTreeNodeAfterSelectEventTakePlace = true;

        /// <summary>
        /// ���Ƿ�����޺ۼ��޸ġ�Ĭ��Ϊtrue
        /// �˱���ֻ����checkbox��������״̬
        /// </summary>
        bool m_blnIsModifyWithoutMark = true;

        protected override bool blnIsModifyWithoutMark
        {
            get { return m_blnIsModifyWithoutMark; }
        }

        protected bool m_blnCanShowDiseaseTrack = true;
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ��ǰ��״̬
        /// </summary>
        protected override enmFormState m_EnmCurrentFormState
        {
            get
            {
                return enmFormState.NowUser;
            }
        }

        /// <summary>
        /// �Ƿ�����Ӽ�¼��true������ӣ�false���޸ġ�
        /// </summary>
        protected override bool m_BlnIsAddNew
        {
            get
            {
                return m_objCurrentRecordContent == null;
            }
        }
        #endregion

        #region ���캯��
        public frmDiseaseTrackBase()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();


            // TODO: Add any initialization after the InitializeComponent call
            m_objBorderTool = new clsBorderTool(Color.White);
            m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[] { m_trvCreateDate });
            if (m_trvCreateDate.Nodes.Count == 0)
                m_trvCreateDate.Nodes.Add("��¼ʱ��");
            m_trnRoot = m_trvCreateDate.Nodes[0];

            m_objDiseaseTrackDomain = m_objGetDiseaseTrackDomain();
        }
        #endregion

        #region �ӿ�ʵ��
        public void Save()
        {
            long m_lngRe = this.m_lngSave();
            if (m_lngRe > 0)
            {
                if (this.m_trvCreateDate.SelectedNode != null)
                {
                    this.m_trvCreateDate_AfterSelect(this.m_trvCreateDate, new System.Windows.Forms.TreeViewEventArgs(this.m_trvCreateDate.SelectedNode));
                }
                clsPublicFunction.ShowInformationMessageBox("����ɹ���");
            }
            else
                clsPublicFunction.ShowInformationMessageBox("����ʧ�ܣ�");
        }

        public void Delete()
        {
            //ָ��������Ϊ����
            //if (this.Name == "frmConsultation" || this.Name == "frmOutHospital" || this.Name == "frmEMR_OutHospitalIn24Hours" || this.Name == "frmDeathRecordIn24Hours" || this.Name == "frmDeathCaseDiscuss")
            //    intFormType = 1;
            //else
            //    intFormType = 2;
            long m_lngRe = m_lngDelete();
            if (m_lngRe > 0)
            {
                //this.m_trvCreateDate.SelectedNode=this.m_trvCreateDate.Nodes[0];
                if (this.m_trvCreateDate.SelectedNode != null)
                {
                    this.m_trvCreateDate_AfterSelect(this.m_trvCreateDate, new System.Windows.Forms.TreeViewEventArgs(this.m_trvCreateDate.SelectedNode));
                }


            }

        }
        public void Display() { }
        public void Display(string strInPatientID, string strInPatientDate)
        {
        }
        public void Print()
        {
            this.m_lngPrint();
        }
        public void Copy() { m_lngCopy(); }
        public void Cut() { m_lngCut(); }
        public void Paste() { m_lngPaste(); }
        public void Verify()
        {
            try
            {
                //��鵱ǰ���˱����Ƿ�Ϊnull          
                if (m_objCurrentPatient == null)
                {
                    clsPublicFunction.ShowInformationMessageBox("δѡ������,�޷���֤!");
                }
                //��鵱ǰ��¼�Ƿ�Ϊnull
                if (m_objCurrentRecordContent == null)
                {
                    clsPublicFunction.ShowInformationMessageBox("δѡ����¼,�޷���֤!");
                }
                string strInPatientID = m_objCurrentRecordContent.m_strInPatientID;
                //string strInPatientDate = m_objCurrentRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");
                string strRecordID = strInPatientID.Trim() + "-" + m_objCurrentRecordContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                long lngRes = m_lngSignVerify(this.Name.Trim(), strRecordID);
            }
            catch (Exception exp)
            {
                MessageBox.Show("ǩ����֤�����쳣��" + exp.Message, "Message" + " ȷ���Ƿ����key��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void Redo() { }
        public void Undo() { }
        #endregion

        #region ��ʼ�����

        /// <summary>
        /// �Ƿ������޸ļ�¼ʱ��ȼ�¼��Ϣ��
        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected void m_mthEnableModify(bool p_blnEnable)
        {
            //���ü�¼ʱ��� Enable = p_blnEnable
            m_dtpCreateDate.Enabled = p_blnEnable;

            //���þ����¼���������
            m_mthEnableModifySub(p_blnEnable);
        }
        /// <summary>
        /// ��ս���
        /// </summary>
        protected void m_mthClearAll()
        {
            //��ղ��˻�����Ϣ            
            m_mthClearPatientBaseInfo();
            //���ʱ���б���
            m_trnRoot.Nodes.Clear();

            //���õ�ǰ���˱���
            m_objCurrentPatient = null;

            //��յ�ǰ��¼��
            m_mthClearPatientRecordInfo();
        }

        /// <summary>
        /// ��ղ��˼�¼������Ϣ��
        /// </summary>
        protected void m_mthClearPatientRecordInfo()
        {
            //�Ѽ�¼ʱ��ָ�����ǰʱ��      
            m_dtpCreateDate.Value = DateTime.Now;

            m_mthEnableModify(true);

            //��ռ�¼����                       
            m_mthClearRecordInfo();

            //��ձ��浱ǰ��¼�ı���
            m_objCurrentRecordContent = null;

            //��գ����ã�������Ϣ 
            m_objReAddNewOld = null;

            m_mthSetModifyControl(null, true);
        }

        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected virtual void m_mthClearRecordInfo()
        {
            //��վ����¼���ݣ����Ӵ�������ʵ��
        }
        /// <summary>
        /// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected void m_mthEnablePatientSelect(bool p_blnEnable)
        {
            //���ò���ѡ����Ϣ�� Enable = p_blnEnable
            if (p_blnEnable == false)
            {
                m_lblForTitle.Visible = false;
                lblAreaTitle.Visible = false;
                m_cboArea.Visible = false;
                lblBedNoTitle.Visible = false;
                m_txtBedNO.Visible = false;
                lblNameTitle.Visible = false;
                m_txtPatientName.Visible = false;
                lblInHospitalNoTitle.Visible = false;
                txtInPatientID.Visible = false;
                m_trvCreateDate.Visible = false;
                lblSexTitle.Visible = false;
                lblSex.Visible = false;
                lblAgeTitle.Visible = false;
                lblAge.Visible = false;
                m_objBorderTool.m_mthUnChangedControlBorder(m_trvCreateDate);

                m_cboArea.Enabled = p_blnEnable;
                m_txtBedNO.ReadOnly = !p_blnEnable;
                m_txtPatientName.ReadOnly = !p_blnEnable;
                txtInPatientID.ReadOnly = !p_blnEnable;

                //����ʱ���б����� Enable = p_blnEnable			
                m_trvCreateDate.Enabled = p_blnEnable;

            }


            m_mthEnablePatientSelectSub(p_blnEnable);
        }


        /// <summary>
        /// ����ʱ����Ĭ����Ϣ
        /// </summary>
        /// <param name="p_objPatient"></param>
        public void m_mthSetDiseaseTrackInfoForAddNew(clsPatient p_objPatient)
        {
            //�������  
            if (p_objPatient == null)
                return;

            m_mthSetPatient(p_objPatient);

            m_mthSetDefaultValue(p_objPatient);

            m_mthEnablePatientSelect(false);

            m_mthAddFormStatusForClosingSave();
        }


        #endregion

        #region TreeView�йز���
        /// <summary>
        /// ����TeeeViewĬ��ѡ��Ľڵ�
        /// </summary>
        protected virtual void m_mthSetNodeSelected()
        {
            if (m_trnRoot.Nodes.Count == 0)
                m_trvCreateDate.SelectedNode = m_trnRoot;
            else
                m_trvCreateDate.SelectedNode = m_trnRoot.Nodes[0];
        }
        /// <summary>
        /// ��ӽڵ㵽ʱ���б���,��ѡ��
        /// </summary>
        /// <param name="p_objContent"></param>
        protected virtual void m_mthAddNode(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
            {
                return;
            }
            string strCreateDate = p_objContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            TreeNode trnNode = new TreeNode(strCreateDate);
            trnNode.Tag = p_objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");

            m_blnCanTreeNodeAfterSelectEventTakePlace = false;

            if (m_trnRoot.Nodes.Count == 0 || trnNode.Text.CompareTo(m_trnRoot.LastNode.Text) < 0)
            {
                m_trnRoot.Nodes.Add(trnNode);
                m_trvCreateDate.SelectedNode = m_trnRoot.LastNode;//
            }
            else
            {
                for (int i = 0; i < m_trnRoot.Nodes.Count; i++)
                {
                    if (trnNode.Text.CompareTo(m_trnRoot.Nodes[i].Text) > 0)
                    {
                        m_trnRoot.Nodes.Insert(i, trnNode);
                        m_trvCreateDate.SelectedNode = m_trnRoot.Nodes[i];//
                        break;
                    }
                }
            }
            m_blnCanTreeNodeAfterSelectEventTakePlace = true;
            m_dtpCreateDate.Enabled = false;
        }

        #endregion

        #region �麯��
        /// <summary>
        /// ���ݸ���
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected virtual void m_mthDataShare(clsPatient p_objSelectedPatient)
        {
            return;
        }

        /// <summary>
        /// �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
        /// </summary>
        /// <returns></returns>
        protected virtual clsTrackRecordContent m_objGetContentFromGUI()
        {
            //�ӽ����ȡ��ֵ�����Ӵ�������ʵ��			
            return null;
        }

        protected virtual void m_mthGetPCAnaesthetist(clsTrackRecordContent p_objContent)
        {
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
        }

        /// <summary>
        /// �������¼��ֵ��ʾ�������ϡ�
        /// </summary>
        /// <param name="p_objContent"></param>
        protected virtual void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
        {
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
        }

        protected virtual void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
        {
            //�ѱ�ֵ��ֵ�����棬���Ӵ�������ʵ��
        }
        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected virtual void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            //��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݣ����Ӵ�������ʵ�֡�
        }

        /// <summary>
        /// �Ƿ������޸������¼�ļ�¼��Ϣ��
        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected virtual void m_mthEnableModifySub(bool p_blnEnable)
        {
            //�����¼���������,�����Ӵ������Ҫ����ʵ��
        }

        /// <summary>
        /// �����޸Ŀؼ���Ϣ
        /// �Ӵ���ʵ��
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_blnReset"></param>
        protected virtual void m_mthSetModifyControlSub(clsTrackRecordContent p_objRecordContent,
            bool p_blnReset)
        {
            //�������,�����Ӵ������Ҫ����ʵ��
        }

        /// <summary>
        /// ��ȡ���̼�¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected virtual clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ���̼�¼�������ʵ�������Ӵ�������ʵ��			
            return null;
        }

        /// <summary>
        /// ������װģ������������������
        /// </summary>
        protected virtual void m_mthSaveTemplateSet_Associate()
        {
            return;
        }

        /// <summary>
        /// ��ȡ��ǰ�����ⲡ�̼�¼��Ϣ
        /// </summary>
        /// <returns></returns>
        public virtual clsDiseaseTrackInfo m_objGetDiseaseTrackInfo()
        {
            //��ȡ��ǰ�����ⲡ�̼�¼��Ϣ�����Ӵ�������ʵ��
            return null;
        }

        /// <summary>
        /// �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected virtual void m_mthEnablePatientSelectSub(bool p_blnEnable)
        {
            //���Ӵ�������ʵ��	
        }
        #endregion

        #region ��ȡָ�����̼�¼����
        /// <summary>
        /// ���ò��˱���Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
        {
            //�жϲ�����Ϣ�Ƿ�Ϊnull������ǣ�ֱ�ӷ��ء�
            if (p_objSelectedPatient == null || m_objDiseaseTrackDomain == null)
                return;

            //��ղ��˼�¼��Ϣ
            m_mthClearPatientRecordInfo();

            //���ʱ���б�����ʱ��ڵ� 
            //			m_trnRoot.Nodes.Clear();
            m_trvCreateDate.Nodes.Clear();
            m_trvCreateDate.Nodes.Add("��¼ʱ��");
            m_trnRoot = m_trvCreateDate.Nodes[0];

            //��¼������Ϣ
            m_objCurrentPatient = p_objSelectedPatient;
            //��������Ҫʱ���б������ִ���������
            if (this.Name != "frmMiniBooldSugarContent" || this.Name != "frmICUNurseRecordContent")
            {

                //��ȡ���˼�¼�б�
                //string[] strCreateTimeListArr;
                //string[] strOpenTimeListArr;
                //m_mthGetTimeList(p_objSelectedPatient, out strCreateTimeListArr, out strOpenTimeListArr);


                //��ʱ��ڵ���е�������
                //new clsSortTool().m_mthSortTreeNode(m_trnRoot, true);

                //����TeeeViewĬ��ѡ��Ľڵ�
                //m_mthSetNodeSelected();

                //չ������ʾ����ʱ��ڵ㡣
                //m_trnRoot.Expand();

            }

            m_mthIsReadOnly();

            m_blnCanShowDiseaseTrack = m_blnCanShowRecordContent();

        }

        /// <summary>
        /// ��ȡ���˼�¼�б�
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        /// <param name="strCreateTimeListArr"></param>
        /// <param name="strOpenTimeListArr"></param>
        /// <returns></returns>
        protected virtual void m_mthGetTimeList(clsPatient p_objSelectedPatient, out string[] strCreateTimeListArr, out string[] strOpenTimeListArr)
        {
            strCreateTimeListArr = null;
            strOpenTimeListArr = null;
            if (p_objSelectedPatient == null || m_objDiseaseTrackDomain == null)
                return;

            long lngRes = m_objDiseaseTrackDomain.m_lngGetRecordTimeList(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strCreateTimeListArr, out strOpenTimeListArr);

            if (lngRes <= 0 || strCreateTimeListArr == null || strOpenTimeListArr == null || strOpenTimeListArr.Length != strCreateTimeListArr.Length)
            {
                m_mthSetNodeSelected();
                return;
            }

            //��Ӳ�ѯ����ʱ�䵽ʱ������ 
            for (int i = strCreateTimeListArr.Length - 1; i >= 0; i--)
            {
                TreeNode trnRecordDate = new TreeNode(strCreateTimeListArr[i]);
                trnRecordDate.Tag = strOpenTimeListArr[i];
                m_trnRoot.Nodes.Add(trnRecordDate);
            }
        }

        /// <summary>
        /// ��ȡָ�����̼�¼����
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        public virtual clsTrackRecordContent m_objGetRecordContent(clsPatient p_objSelectedPatient, string p_strOpenDate)
        {
            clsTrackRecordContent objContent;
            //��ȡ��¼
            m_objDiseaseTrackDomain.m_lngGetRecordContent(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strOpenDate, out objContent);
            return objContent;
        }
        #region ����
        /// <summary>
        /// ��ȡָ�����̼�¼����(��ɽ�������ų��Ļ������)
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_stFormID">��ID</param>
        /// <returns></returns>
        //public virtual clsTrackRecordContent m_objGetRecordContent(clsPatient p_objSelectedPatient, string p_strOpenDate,string p_strFormID)
        //{
        //    clsTrackRecordContent objContent;
        //    //��ȡ��¼
        //    m_objDiseaseTrackDomain.m_lngGetRecordContent(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strOpenDate,p_strFormID, out objContent);
        //    return objContent;
        //}
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        /// <param name="p_strOpenDate"></param>
        protected void m_mthUseReAddNew(clsPatient p_objSelectedPatient,
            string p_strOpenDate)
        {
            //������
            if (p_objSelectedPatient == null)
            {
                m_mthShowNoPatient();
                return;
            }
            if (p_strOpenDate == null || p_strOpenDate == "")
            {
                clsPublicFunction.ShowInformationMessageBox("��ѡ��Ҫ���ϵļ�¼��Ӧ�ļ�¼ʱ��!");
                return;
            }

            clsTrackRecordContent objContent;
            //��ȡ��¼
            long lngRes = m_objDiseaseTrackDomain.m_lngGetRecordContent(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strOpenDate, out objContent);

            if (lngRes <= 0 || objContent == null)
                return;

            m_objReAddNewOld = objContent;
            m_objCurrentRecordContent = null;

            //����ʱ��,��ʹ֮�����޸�
            m_dtpCreateDate.Enabled = true;

            m_mthReAddNewRecord(objContent);


        }
        #endregion

        #region ����ѡ�����ݵ�����

        /// <summary>
        /// ����ѡ���˵ļ�¼��Ϣ��
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        /// <param name="p_strOpenDate"></param>
        protected void m_mthSetSelectedRecord(clsPatient p_objSelectedPatient, string p_strOpenDate)
        {
            //������
            if (p_objSelectedPatient == null || p_strOpenDate == null || p_strOpenDate == "")
                return;

            clsTrackRecordContent objContent = m_objGetRecordContent(p_objSelectedPatient, p_strOpenDate); ;
            //��ȡ��¼
            //			long lngRes = m_objDiseaseTrackDomain.m_lngGetRecordContent(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),p_strOpenDate ,out objContent);


            if (objContent == null)
                return;


            //���õ�ǰ��¼����¼ʱ�� 
            m_objCurrentPatient = p_objSelectedPatient;
            m_strOpenDate = p_strOpenDate;

            txtInPatientID.Text = this.m_objCurrentPatient.m_StrHISInPatientID;
            m_dtpCreateDate.Value = objContent.m_dtmCreateDate;
            m_objCurrentRecordContent = objContent;

            m_dtmCreatedDate = objContent.m_dtmOpenDate;
            if (objContent.m_intMarkStatus != 0)
            {
                chkModifyWithoutMatk.Checked = false;
                chkModifyWithoutMatk.Visible = false;
            }
            else
            {
                chkModifyWithoutMatk.Checked = true;
                chkModifyWithoutMatk.Visible = true;
            }
            m_mthImplementset(objContent);

            //			m_mthGetPCAnaesthetist(objContent);
            //
            //			m_mthSetGUIFromContent(objContent);
            //
            m_mthSetSign(objContent.m_strModifyUserID);

        }
        #region ����
        /// <summary>
        /// ����ѡ���˵ļ�¼��Ϣ(��ɽ�������ų��Ļ������)��
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strFormID"></param>
        //protected void m_mthSetSelectedRecord(clsPatient p_objSelectedPatient, string p_strOpenDate,string p_strFormID)
        //{
        //    //������
        //    if (p_objSelectedPatient == null || p_strOpenDate == null || p_strOpenDate == "")
        //        return;

        //    clsTrackRecordContent objContent = m_objGetRecordContent(p_objSelectedPatient, p_strOpenDate,p_strFormID); ;
        //    //��ȡ��¼
        //    //			long lngRes = m_objDiseaseTrackDomain.m_lngGetRecordContent(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"),p_strOpenDate ,out objContent);


        //    if (objContent == null)
        //        return;


        //    //���õ�ǰ��¼����¼ʱ�� 
        //    m_objCurrentPatient = p_objSelectedPatient;
        //    m_strOpenDate = p_strOpenDate;

        //    txtInPatientID.Text = this.m_objCurrentPatient.m_StrHISInPatientID;
        //    m_dtpCreateDate.Value = objContent.m_dtmCreateDate;
        //    m_objCurrentRecordContent = objContent;

        //    m_dtmCreatedDate = objContent.m_dtmOpenDate;
        //    if (objContent.m_intMarkStatus != 0)
        //    {
        //        chkModifyWithoutMatk.Checked = false;
        //        chkModifyWithoutMatk.Visible = false;
        //    }
        //    else
        //    {
        //        chkModifyWithoutMatk.Checked = true;
        //        chkModifyWithoutMatk.Visible = true;
        //    }
        //    m_mthImplementset(objContent);

        //    //			m_mthGetPCAnaesthetist(objContent);
        //    //
        //    //			m_mthSetGUIFromContent(objContent);
        //    //
        //    m_mthSetSign(objContent.m_strModifyUserID);

        //}
        #endregion
        /// <summary>
        /// ʵ����Ӧ������
        /// ��m_mthSetSelectedRecord�������
        /// </summary>
        /// <param name="p_objContent"></param>
        private void m_mthImplementset(clsTrackRecordContent p_objContent)
        {
            m_mthGetPCAnaesthetist(p_objContent);

            m_mthSetGUIFromContent(p_objContent);

            //m_mthSetSign(p_objContent.m_strModifyUserID);

            m_mthEnableModify(true);

            m_mthSetModifyControl(p_objContent, false);
        }
        /// <summary>
        /// ���ò��̼�¼��Ϣ������ʾ�޸ġ�
        /// </summary>
        /// <param name="p_objPatient"></param>
        /// <param name="p_dtmRecordTime"></param>
        public void m_mthSetDiseaseTrackInfo(clsPatient p_objPatient,
            DateTime p_dtmRecordTime)
        {
            //�������  
            if (p_objPatient == null)
                return;
            m_mthSetPatient(p_objPatient);

            //���ü�¼��Ϣ
            m_mthSetSelectedRecord(p_objPatient, p_dtmRecordTime.ToString("yyyy-MM-dd HH:mm:ss"));

            //�������û��޸ļ�¼�Ļ�����Ϣ                        
            m_mthEnablePatientSelect(false);

            m_mthAddFormStatusForClosingSave();
        }
        #region ����
        /// <summary>
        /// ���ò��̼�¼��Ϣ������ʾ�޸�(��ɽ�������ų��Ļ������)��
        /// </summary>
        /// <param name="p_objPatient"></param>
        /// <param name="p_dtmRecordTime"></param>
        /// <param name="p_dtmFormID">��ID</param>
        //public void m_mthSetDiseaseTrackInfo(clsPatient p_objPatient,
        //    DateTime p_dtmRecordTime,string p_strFormID)
        //{
        //    //�������  
        //    if (p_objPatient == null)
        //        return;

        //    m_mthSetPatient(p_objPatient);

        //    //���ü�¼��Ϣ
        //    m_mthSetSelectedRecord(p_objPatient, p_dtmRecordTime.ToString("yyyy-MM-dd HH:mm:ss"),p_strFormID);

        //    //�������û��޸ļ�¼�Ļ�����Ϣ                        
        //    m_mthEnablePatientSelect(false);

        //    m_mthAddFormStatusForClosingSave();
        //}
        #endregion
        #endregion

        #region �����¼
        /// <summary>
        /// ����ǩ�����ɣ�
        /// �Ժ���ʹ���µ�ǩ��
        /// ���ڼ��ݿ��� ����
        /// </summary>
        /// <param name="p_strUserID"></param>
        protected virtual void m_mthSetSign(string p_strUserID)
        {
            foreach (Control ctlSub in this.Controls)
            {
                if (ctlSub.Name == "m_txtSign")
                {
                    clsEmployee objEmp = new clsEmployee(p_strUserID);
                    ctlSub.Text = objEmp.m_StrLastName;
                    ctlSub.Tag = objEmp;
                }
            }
        }
        /// <summary>
        /// �����¼
        /// </summary>
        /// <returns></returns>
        protected override long m_lngSubAddNew()
        {
            long lngRes = 0;
            if (m_objReAddNewOld != null)
                lngRes = m_lngReAddNew();
            else
                lngRes = m_lngAddNewRecord();

            if (lngRes > 0 && com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee.m_strTECHNICALRANK_CHR == "��ϰҽʦ")
            {
                clsEMR_CaseAuditVO objAuditVO = m_objGetAuditCase();
                objAuditVO.m_dtmCREATEDATE = m_objCurrentRecordContent.m_dtmOpenDate;
                m_mthAddAuditCase(objAuditVO);
            }
            return lngRes;
        }

        #region �½���¼����
        /// <summary>
        /// ����¼�¼�����ݿⱣ�档
        /// </summary>
        /// <returns></returns>
        protected long m_lngAddNewRecord()
        {
            long lngRes = 0;
            try
            {
                //��鵱ǰ���˱����Ƿ�Ϊnull
                if (m_objCurrentPatient == null)
                {
                    m_mthShowNoPatient();
                    return -1;
                }
                //��ȡ������ʱ��
                string strTimeNow = new clsPublicDomain().m_strGetServerTime();
                //�ӽ����ȡ��¼��Ϣ
                //clsTrackRecordContent objContent = m_objGetContentFromGUI();     
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                //�½�����϶�Ϊ�޺ۼ�״̬
                chkModifyWithoutMatk.Checked = true;
                //����ȡֵ
                objContent = m_objGetContentFromGUI();

                //��������ֵ����           
                if (objContent == null)
                    return -1;

                //���� clsTrackRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmOpenDate��m_dtmModifyDate��
                m_mthSetSubCreatedDateInfo(ref objContent, true);
                objContent.m_intMarkStatus = 0; //�½��������޺ۼ��޸�

                #region ��ǩ��ʱ��֤����ǩ���� ������

                //����ǩ�� 
                //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_objCurrentPatient.m_StrRegisterId;

                if (objContent.objSignerArr != null)
                {
                    ArrayList objSignerArr = new ArrayList();
                    for (int i = 0; i < objContent.objSignerArr.Length; i++)
                    {
                        if (objContent.objSignerArr[i].controlName == "lsvSign" || objContent.objSignerArr[i].controlName == "txtSign")
                            objSignerArr.Add(objContent.objSignerArr[i].objEmployee);
                    }

                    clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                    if (objCheck.CheckSigner(objContent, objSign_VO) == -1)
                        return -1;

                }
                else
                {
                    objContent.m_strModifyUserID = MDIParent.OperatorID;
                    clsCheckSignersController objCheck = new clsCheckSignersController();
                    if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                        return -1;

                }
                #endregion


                //�����¼
                clsPreModifyInfo objModifyInfo = null;
                lngRes = m_objDiseaseTrackDomain.m_lngAddNewRecord(objContent, out objModifyInfo);

                //���ݽ������ͬ�Ĵ���
                switch ((enmOperationResult)lngRes)
                {
                    case enmOperationResult.DB_Succeed:
                        m_mthSaveTemplateSet_Associate();

                        m_objCurrentRecordContent = objContent;
                        m_dtmCreatedDate = objContent.m_dtmOpenDate;
                        //��ӽڵ㵽ʱ���б���,��ѡ��
                        m_mthAddNode(m_objCurrentRecordContent);
                        break;

                    case enmOperationResult.DB_Fail:
                        clsPublicFunction.ShowInformationMessageBox("�Բ���,���ʧ��!");
                        break;
                    case enmOperationResult.Parameter_Error:
                        clsPublicFunction.ShowInformationMessageBox("��������!");
                        break;
                    case enmOperationResult.Record_Already_Exist:
                        m_mthShowRecordTimeDouble();
                        break;
                        //...
                }
            }

            catch (NullReferenceException ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }

            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
                MessageBox.Show(ex.Message, "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            //���ؽ��
            return lngRes;
        }
        #endregion

        #region �����޸ļ�¼
        protected override long m_lngSubModify()
        {
            long lngRes = 0;
            try
            {
                //��鵱ǰ���˱����Ƿ�Ϊnull
                if (m_objCurrentPatient == null)
                {
                    m_mthShowNoPatient();
                    return -1;
                }

                //��ȡ������ʱ��
                string strTimeNow = new clsPublicDomain().m_strGetServerTime();
                //�ӽ����ȡ��¼��Ϣ
                clsTrackRecordContent objContent = m_objGetContentFromGUI();

                //��������ֵ����           
                if (objContent == null)
                    return -1;

                //���� clsTrackRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmModifyDate��	
                m_mthSetSubCreatedDateInfo(ref objContent, false);
                objContent.m_dtmOpenDate = m_objCurrentRecordContent.m_dtmOpenDate;
                objContent.m_strCreateUserID = m_objCurrentRecordContent.m_strCreateUserID;
                objContent.m_dtmCreateDate = m_objCurrentRecordContent.m_dtmCreateDate;

                #region �Ƿ�����޺ۼ��޸�
                if (chkModifyWithoutMatk.Checked)
                    objContent.m_intMarkStatus = 0;
                else
                    objContent.m_intMarkStatus = 1;
                #endregion
                //objContent.m_dtmModifyDate=DateTime.Parse(strTimeNow);
                ////�������м�¼�Ŀ�ʼʹ��ʱ��
                //objContent.m_bytIfConfirm=0;
                //objContent.m_bytStatus=0;
                //if ( this.Name != "frmICUNurseRecordContent")
                //{
                //    objContent.m_dtmCreateDate=m_dtpCreateDate.Value;
                //}
                //objContent.m_dtmInPatientDate=m_objCurrentPatient.m_DtmSelectedInDate;
                //objContent.m_strConfirmReason="";
                //objContent.m_strConfirmReasonXML="";
                //objContent.m_strInPatientID=m_objCurrentPatient.m_StrInPatientID;
                //objContent.m_strRegisterID = m_objCurrentRecordContent.m_strRegisterID;
                //objContent.m_strModifyUserID=MDIParent.OperatorID;




                #region ��ǩ��ʱ��֤����ǩ����
                clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
                objSign_VO.m_strFORMID_VCHR = this.Name;
                objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
                objSign_VO.m_strRegisterId = m_objCurrentPatient.m_StrRegisterId;

                //if (objContent.objSignerArr != null)//����ǩ������Ҫkey��֤ ��ʱ
                if (objContent.objSignerArr != null &&
                    this.Name != "frmGeneralNurseRecord" &&
                    this.Name != "frmGeneralNurseRecord_GXCon" &&
                    this.Name != "frmGeneralNurseRecord_GXRec" &&
                    this.Name != "frmICUNurseRecord_GXCon" &&
                    this.Name != "frmIntensiveTend_GXContent" &&
                    this.Name != "frmIntensiveTend_GX" &&
                    this.Name != "frmVeinSpecialUseDrugCon" &&
                    this.Name != "frmIntakeAndOutputVolumeCon" &&
                    this.Name != "frmOXTIntravenousDripCon" &&
                    this.Name != "frmWaitLayRecord_AcadCon_GX" &&
                    this.Name != "frmVaginalExaminationRecord" &&
                    this.Name != "frmEMR_MicroBooldSugarCheckCon")
                {
                    ArrayList objSignerArr = new ArrayList();
                    for (int i = 0; i < objContent.objSignerArr.Length; i++)
                    {
                        if (objContent.objSignerArr[i].controlName == "lsvSign" || objContent.objSignerArr[i].controlName == "txtSign")
                            objSignerArr.Add(objContent.objSignerArr[i].objEmployee);
                    }
                    if (objContent.m_intMarkStatus == 0)
                    {
                        clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, true);
                        if (objCheck.CheckSigner(objContent, objSign_VO) == -1)
                            return -1;
                    }
                    else if (objContent.m_intMarkStatus != 0)
                    {
                        clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                        if (objCheck.CheckSigner(objContent, objSign_VO) == -1)
                            return -1;
                    }
                }
                else
                {
                    if (this.Name != "frmIntakeAndOutputVolumeCon" &&
                    this.Name != "frmOXTIntravenousDripCon" &&
                    this.Name != "frmWaitLayRecord_AcadCon_GX" &&
                    this.Name != "frmVaginalExaminationRecord" &&
                    this.Name != "frmEMR_MicroBooldSugarCheckCon")
                    {
                        objContent.m_strModifyUserID = MDIParent.OperatorID;
                    }
                    clsCheckSignersController objCheck = new clsCheckSignersController();
                    if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                        return -1;

                }
                #endregion

                //�޸ļ�¼
                clsPreModifyInfo objModifyInfo = null;
                lngRes = m_objDiseaseTrackDomain.m_lngModifyRecord(m_objCurrentRecordContent, objContent, out objModifyInfo);

                //���ݽ������ͬ�Ĵ���
                switch ((enmOperationResult)lngRes)
                {
                    case enmOperationResult.DB_Succeed:
                        m_objCurrentRecordContent = objContent;
                        m_dtmCreatedDate = objContent.m_dtmOpenDate;
                        if (objContent.m_intMarkStatus != 0 && chkModifyWithoutMatk.Visible)
                            chkModifyWithoutMatk.Visible = false;
                        break;
                    case enmOperationResult.DB_Fail:
                        clsPublicFunction.ShowInformationMessageBox("�Բ���,�޸�ʧ��!");
                        break;
                    case enmOperationResult.Parameter_Error:
                        clsPublicFunction.ShowInformationMessageBox("��������!");
                        break;
                    case enmOperationResult.Record_Already_Modify:
                        if (objModifyInfo != null)
                            m_bolShowRecordModified(objModifyInfo.m_strActionUserID, objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        else m_mthShowDBError();
                        break;
                    case enmOperationResult.Record_Already_Delete:
                        if (objModifyInfo != null)
                            m_mthShowRecordDeleted(objModifyInfo.m_strActionUserID, objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        else m_mthShowDBError();
                        break;
                        //...
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
                MessageBox.Show(ex.Message, "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            //���ؽ��
            return lngRes;
        }

        #endregion

        #region �������������ݿⱣ�档
        /// <summary>
        /// �����Ӵ���Ĵ���ʱ�����ʱ��ȣ�Ϊ���ʺ���RegisterId����ҵ��Ҫ����
        /// </summary>
        /// <param name="p_objContent"></param>
        protected virtual void m_mthSetSubCreatedDateInfo(ref clsTrackRecordContent p_objContent, bool p_blnIsAddNew)
        {
            if (p_objContent != null)
            {
                string strTimeNow = new clsPublicDomain().m_strGetServerTime();
                if (p_blnIsAddNew)
                {
                    p_objContent.m_dtmOpenDate = DateTime.Parse(strTimeNow);
                    if (this.Name != "frmICUNurseRecordContent" && this.Name != "frmOXTIntravenousDripCon"
                        && this.Name != "frmWaitLayRecord_AcadCon_GX" && this.Name != "frmIntakeAndOutputVolumeCon"
                        && this.Name != "frmIntakeAndOutputVolumeSummary" && this.Name != "frmEMR_MicroBooldSugarCheckCon")
                    {
                        p_objContent.m_dtmCreateDate = m_dtpCreateDate.Value;
                    }
                    if (this.Name != "frmVeinSpecialUseDrugCon" && this.Name != "frmICUNurseRecord_GXCon"
                        && this.Name != "frmGeneralNurseRecord_GXCon" && this.Name != "frmGeneralNurseRecord_GXRec"
                        && this.Name != "frmGeneralNurseRecord_CSCon" && this.Name != "frmGeneralNurseRecord_CSRec"
                        && this.Name != "frmGeneralNurseRecord" && this.Name != "frmIntensiveTend_GX"
                        && this.Name != "frmICUNurseRecordContent" && this.Name != "frmOXTIntravenousDripCon"
                        && this.Name != "frmWaitLayRecord_AcadCon_GX" && this.Name != "frmIntakeAndOutputVolumeCon"
                        && this.Name != "frmIntakeAndOutputVolumeSummary" && this.Name != "frmEMR_MicroBooldSugarCheckCon"
                        && this.Name != "frmOperationRequisition")
                    {
                        p_objContent.m_strCreateUserID = MDIParent.OperatorID;
                    }
                }
                p_objContent.m_dtmModifyDate = DateTime.Parse(strTimeNow);
                p_objContent.m_bytIfConfirm = 0;
                p_objContent.m_bytStatus = 0;
                p_objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
                p_objContent.m_strConfirmReason = "";
                p_objContent.m_strConfirmReasonXML = "";
                p_objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
                p_objContent.m_strRegisterID = m_objCurrentPatient.m_StrRegisterId;
            }
        }
        /// <summary>
        /// �������������ݿⱣ�档
        /// </summary>
        /// <returns></returns>
        protected long m_lngReAddNew()
        {
            //��鵱ǰ���˱����Ƿ�Ϊnull
            if (m_objCurrentPatient == null)
            {
                m_mthShowNoPatient();
                return -1;
            }


            //��ȡ������ʱ��
            string strTimeNow = new clsPublicDomain().m_strGetServerTime();
            //�ӽ����ȡ��¼��Ϣ
            clsTrackRecordContent objContent = m_objGetContentFromGUI();

            //��������ֵ����           
            if (objContent == null)
                return -1;

            //���� clsTrackRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmOpenDate��m_dtmModifyDate��
            m_mthSetSubCreatedDateInfo(ref objContent, true);
            //objContent.m_strModifyUserID=MDIParent.OperatorID;

            //����������¼
            clsPreModifyInfo objModifyInfo = null;

            #region ��ǩ��ʱ��֤����ǩ����
            clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
            objSign_VO.m_strFORMID_VCHR = this.Name;
            objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objSign_VO.m_strRegisterId = m_objCurrentPatient.m_StrRegisterId;
            if (objContent.objSignerArr != null)
            {
                ArrayList objSignerArr = new ArrayList();
                for (int i = 0; i < objContent.objSignerArr.Length; i++)
                {
                    if (objContent.objSignerArr[i].controlName == "lsvSign" || objContent.objSignerArr[i].controlName == "txtSign")
                        objSignerArr.Add(objContent.objSignerArr[i].objEmployee);
                }
                clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
                if (objCheck.CheckSigner(objContent, objSign_VO) == -1)
                    return -1;
            }
            else
            {
                objContent.m_strModifyUserID = MDIParent.OperatorID;
                clsCheckSignersController objCheck = new clsCheckSignersController();
                if (objCheck.m_lngSign(objContent, objSign_VO) == -1)
                    return -1;

            }
            #endregion

            long lngRes = m_objDiseaseTrackDomain.m_lngReAddNewRecord(m_objReAddNewOld, objContent, out objModifyInfo);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    m_objCurrentRecordContent = objContent;
                    m_dtmCreatedDate = objContent.m_dtmOpenDate;
                    m_objReAddNewOld = null;
                    break;
                case enmOperationResult.DB_Fail:
                    clsPublicFunction.ShowInformationMessageBox("�Բ���,�޸�ʧ��!");
                    break;
                case enmOperationResult.Parameter_Error:
                    clsPublicFunction.ShowInformationMessageBox("��������!");
                    break;
                case enmOperationResult.Record_Already_Modify:
                    if (objModifyInfo != null)
                        m_bolShowRecordModified(objModifyInfo.m_strActionUserID, objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    else m_mthShowDBError();
                    break;
                case enmOperationResult.Record_Already_Delete:
                    if (objModifyInfo != null)
                        m_mthShowRecordDeleted(objModifyInfo.m_strActionUserID, objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    else m_mthShowDBError();
                    break;
                    //...
            }

            //���ؽ��
            return lngRes;

        }
        #endregion

        #endregion

        #region ɾ����¼
        /// <summary>
        /// ��д��ȡ��¼����������
        /// ����ָ����¼������ID
        /// </summary>
        protected override string m_StrRecorder_ID
        {
            get
            {
                return m_objCurrentRecordContent.m_strCreateUserID.Trim();
            }
        }
        protected override long m_lngSubDelete()
        {
            //��鵱ǰ���˱����Ƿ�Ϊnull          
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
            {
                m_mthShowNoPatient();
                return -1;
            }
            //��鵱ǰ��¼�Ƿ�Ϊnull
            if (m_objCurrentRecordContent == null)
            {
                return -1;
            }

            //��ȡ������ʱ��      
            string strTimeNow = new clsPublicDomain().m_strGetServerTime();
            //���� m_objCurrentRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmDeActivedDate��
            m_objCurrentRecordContent.m_dtmDeActivedDate = DateTime.Parse(strTimeNow);
            m_objCurrentRecordContent.m_strDeActivedOperatorID = MDIParent.OperatorID;

            //Ȩ���ж�
            string strDeptIDTemp = m_ObjCurrentEmrPatientSession.m_strAreaId;
            bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, m_objCurrentRecordContent.m_strCreateUserID, clsEMRLogin.LoginEmployee, intFormType);
            if (!blnIsAllow)
                return -1;

            //ɾ����¼
            clsPreModifyInfo objModifyInfo = null;
            long lngRes = m_objDiseaseTrackDomain.m_lngDeleteRecord(m_objCurrentRecordContent, out objModifyInfo);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    clsEMR_CaseAuditVO objAuditVO = m_objGetAuditCase();
                    objAuditVO.m_dtmCREATEDATE = m_objCurrentRecordContent.m_dtmOpenDate;
                    m_mthDelAuditCase(objAuditVO);

                    m_objCurrentRecordContent = null;
                    m_dtmCreatedDate = DateTime.Now;

                    m_blnCanTreeNodeAfterSelectEventTakePlace = false;
                    //ɾ��ѡ�нڵ� 
                    m_trvCreateDate.SelectedNode.Remove();
                    //��ռ�¼��Ϣ   
                    m_mthClearPatientRecordInfo();
                    //ѡ�и��ڵ�
                    m_trvCreateDate.SelectedNode = m_trnRoot;
                    m_blnCanTreeNodeAfterSelectEventTakePlace = true;
                    break;
                case enmOperationResult.DB_Fail:
                    clsPublicFunction.ShowInformationMessageBox("�Բ���,ɾ��ʧ��!");
                    break;
                case enmOperationResult.Parameter_Error:
                    clsPublicFunction.ShowInformationMessageBox("��������!");
                    break;
                case enmOperationResult.Record_Already_Modify:
                    if (objModifyInfo != null)
                        m_bolShowRecordModified(objModifyInfo.m_strActionUserID, objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    else m_mthShowDBError();
                    break;
                case enmOperationResult.Record_Already_Delete:
                    if (objModifyInfo != null)
                        m_mthShowRecordDeleted(objModifyInfo.m_strActionUserID, objModifyInfo.m_dtmActionTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    else m_mthShowDBError();
                    break;
            }

            //���ؽ��
            return lngRes;
        }

        #endregion

        #region ctlRichTextBox��˫���ߡ�������������
        /// <summary>
        /// �ۼ�����
        /// �����ڿ��޺ۼ��޸�ʱЧ�ڡ�ͨ������chkModifyWithoutMatk��
        /// �ﵽ�����Ƿ��С��޺ۼ��޸�
        /// </summary>
        /// <returns></returns>
        protected override bool m_mthModifyWithoutMark()
        {

            bool blnRes = chkModifyWithoutMatk.Checked;
            if (chkModifyWithoutMatk.Checked == false)
            {

                //��ʾǩ��
                if (MessageBox.Show("���Ҫ�����кۼ��޸ģ���ǰ�����޸ĵ����ݽ���ʧ��Ҫ������", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    //����load����
                    m_blnIsModifyWithoutMark = false;
                    m_mthImplementset(m_objCurrentRecordContent);
                }
                else
                    blnRes = !blnRes;
            }
            else
            {
                //��ʾǩ��
                if (MessageBox.Show("���Ҫ�����޺ۼ��޸ģ���ǰ�����޸ĵ����ݽ���ʧ������Ҫ��һ��֤ÿ��ǩ���ߣ�Ҫ������", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    //����load����
                    m_blnIsModifyWithoutMark = true;
                    m_mthImplementset(m_objCurrentRecordContent);
                }
                else
                    blnRes = !blnRes;

            }
            return blnRes;

        }
        /// <summary>
        /// ����˫����
        /// </summary>
        protected void m_mthSetRichTextBoxDoubleStrike()
        {
            //��ȡRichTextBox        
            //ctlRichTextBox objRichTextBox = (ctlRichTextBox)m_ctmRichTextBoxMenu.SourceControl;

            //objRichTextBox.m_mthSelectionDoubleStrikeThough(true);
            if (m_txtFocusedRichTextBox != null)
            {
                if (m_txtFocusedRichTextBox is com.digitalwave.Utility.Controls.ctlRichTextBox)
                    ((com.digitalwave.Utility.Controls.ctlRichTextBox)m_txtFocusedRichTextBox).m_mthSelectionDoubleStrikeThough(true);
                else if (m_txtFocusedRichTextBox is com.digitalwave.controls.ctlRichTextBox)
                    ((com.digitalwave.controls.ctlRichTextBox)m_txtFocusedRichTextBox).m_mthSelectionDoubleStrikeThough(true);
            }
        }

        /// <summary>
        /// ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID����ɫ�ȣ���
        /// </summary>
        /// <param name="p_objRichTextBox"></param>
        protected void m_mthSetRichTextBoxAttrib(Control p_objRichTextBox)
        {
            if (p_objRichTextBox.GetType().FullName == "com.digitalwave.controls.ctlRichTextBox")
            {
                m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[] { (com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox });
                //�����Ҽ��˵�			
                //			p_objRichTextBox.ContextMenu=m_cmuRichTextBoxMenu;
                ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);

                //������������	
                if (this.Name == "frmGeneralNurseRecord"
                        || this.Name == "frmGeneralNurseRecord_GXRec"
                        || this.Name == "frmSubICUIntensiveTend"
                        || this.Name == "frmSubWatchItemRecord"
                        || this.Name == "frmSubICUBreath"
                        || this.Name == "frmPostPartum_AcadCon"
                        || this.Name == "frmIntensiveTend_GXContent"
                        || this.Name == "frmQuickeningTutelar_AcadCon"
                        || this.Name == "frmCardiovascularTend_GX"
                        || this.Name == "frmICUNurseRecord_GXCon"
                        || this.Name == "frmIntensiveTend"
                        || this.Name == "frmIntensiveTend_FC"
                        || this.Name == "frmIntensiveTend_FContent"
                        || this.Name == "frmIntensiveTend_GX"
                        || this.Name == "frmIntensiveTend_GXContent"
                        || this.Name == "frmSurgeryICUWardshipEdit"
                        || this.Name == "frmVeinSpecialUseDrugCon"
                        || this.Name == "frmWaitLayRecord_AcadCon"
                        || this.Name == "frmConsultation"
                        || this.Name == "frmDeathCaseDiscuss"
                        || this.Name == "frmDeathRecord"
                        || this.Name == "frmDeathRecordIn24Hours"
                        || this.Name == "frmEMR_OutHospitalIn24Hours"
                        || this.Name == "frmOutHospital")
                {
                    ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserID = clsEMRLogin.LoginEmployee.m_strEMPNO_CHR.Trim();
                    ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserName = clsEMRLogin.LoginEmployee.m_strLASTNAME_VCHR.Trim();


                }
                else
                {
                    ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserID = clsEMRLogin.LoginEmployee.m_strEMPID_CHR.Trim();
                    ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserName = clsEMRLogin.LoginEmployee.m_strLASTNAME_VCHR.Trim();

                }

                ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_ClrOldPartInsertText = Color.Black;
                ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_ClrDST = Color.Red;
            }
        }

        protected void m_mthSetRichTextBoxAttribInControl(Control p_ctlControl)
        {
            if (p_ctlControl.GetType().FullName == "com.digitalwave.controls.ctlRichTextBox")
            {
                m_mthSetRichTextBoxAttrib((com.digitalwave.controls.ctlRichTextBox)p_ctlControl);
            }

            if (p_ctlControl.HasChildren && p_ctlControl.GetType().Name != "DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextBoxAttribInControl(subcontrol);
                }
            }
        }

        private void mniDoubleStrikeOutDelete_Click(object sender, System.EventArgs e)
        {
            m_mthSetRichTextBoxDoubleStrike();
        }
        private Control m_txtFocusedRichTextBox = null;//��ŵ�ǰ��ý����RichTextBox
        private void m_txtRichTextBox_GotFocus(object sender, System.EventArgs e)
        {
            m_txtFocusedRichTextBox = ((Control)(sender));
        }

        #endregion ctlRichTextBox��˫���ߡ�������������

        #region ������ӡ���



        protected void m_mthSetFormViewStyle()
        {
            if (!MDIParent.s_bolIAnaSystem)
                return;

            this.BackColor = SystemColors.Control;
            this.ForeColor = SystemColors.ControlText;
            foreach (Control m_objControl in this.Controls)
            {
                m_mthSetControlViewStyle(m_objControl);

            }
        }

        protected void m_mthSetControlViewStyle(Control p_objControl)
        {
            //�����������Ƶ�¼���˳�����
            if (!MDIParent.s_bolIAnaSystem)
                return;

            switch (p_objControl.GetType().FullName)
            {
                case "Label":
                case "Button":
                case "CheckBox":
                case "RadioButton":
                case "GroupBox":
                case "TabControl":
                case "System.Windows.Forms.TabPage":
                    {
                        p_objControl.ForeColor = SystemColors.ControlText;
                        p_objControl.BackColor = SystemColors.Control;

                        break;

                    }

                case "System.Windows.Forms.TreeView":
                    {
                        p_objControl.ForeColor = SystemColors.ControlText;
                        p_objControl.BackColor = SystemColors.Info;

                        break;
                    }
                case "System.Windows.Forms.ListView":
                    {
                        p_objControl.ForeColor = SystemColors.ControlText;
                        p_objControl.BackColor = SystemColors.Info;

                        foreach (ListViewItem m_lvi in ((ListView)p_objControl).Items)
                        {
                            m_lvi.ForeColor = SystemColors.ControlText;
                            m_lvi.BackColor = SystemColors.Info;
                        }
                        break;
                    }

                case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                    {
                        ((ctlBorderTextBox)p_objControl).ForeColor = SystemColors.ControlText;
                        ((ctlBorderTextBox)p_objControl).BackColor = SystemColors.Info;
                        ((ctlBorderTextBox)p_objControl).BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                        ((ctlBorderTextBox)p_objControl).BorderColor = SystemColors.Control;

                        break;

                    }
                case "com.digitalwave.Utility.Controls.ctlTimePicker":
                    ((ctlTimePicker)p_objControl).ForeColor = SystemColors.ControlText;
                    ((ctlTimePicker)p_objControl).BackColor = SystemColors.Info;
                    ((ctlTimePicker)p_objControl).BorderColor = SystemColors.ControlText;
                    ((ctlTimePicker)p_objControl).DropButtonBackColor = SystemColors.Control;
                    ((ctlTimePicker)p_objControl).DropButtonForeColor = SystemColors.ControlText;
                    ((ctlTimePicker)p_objControl).TextBackColor = SystemColors.Info;
                    ((ctlTimePicker)p_objControl).TextForeColor = SystemColors.ControlText;

                    break;

                case "com.digitalwave.Utility.Controls.ctlComboBox":
                    {
                        ((ctlComboBox)p_objControl).ForeColor = SystemColors.ControlText;
                        ((ctlComboBox)p_objControl).BackColor = SystemColors.Info;
                        ((ctlComboBox)p_objControl).BorderColor = SystemColors.ControlText;
                        ((ctlComboBox)p_objControl).DropButtonBackColor = SystemColors.Control;
                        ((ctlComboBox)p_objControl).DropButtonForeColor = SystemColors.ControlText;

                        ((ctlComboBox)p_objControl).ListBackColor = SystemColors.Info;
                        ((ctlComboBox)p_objControl).ListForeColor = SystemColors.ControlText;
                        ((ctlComboBox)p_objControl).ListSelectedBackColor = SystemColors.Control;
                        ((ctlComboBox)p_objControl).ListSelectedForeColor = SystemColors.ControlText;
                        ((ctlComboBox)p_objControl).TextBackColor = SystemColors.Info;
                        ((ctlComboBox)p_objControl).TextForeColor = SystemColors.ControlText;


                        break;
                    }

                case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                    {
                        ((ctlRichTextBox)p_objControl).ForeColor = SystemColors.ControlText;
                        ((ctlRichTextBox)p_objControl).BackColor = SystemColors.Info;
                        ((ctlRichTextBox)p_objControl).BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                        ((ctlRichTextBox)p_objControl).m_ClrOldPartInsertText = SystemColors.WindowText;
                        //					
                        break;
                    }

                case "com.digitalwave.controls.ctlRichTextBox":
                    {
                        ((com.digitalwave.controls.ctlRichTextBox)p_objControl).ForeColor = SystemColors.ControlText;
                        ((com.digitalwave.controls.ctlRichTextBox)p_objControl).BackColor = SystemColors.Info;
                        ((com.digitalwave.controls.ctlRichTextBox)p_objControl).BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                        ((com.digitalwave.controls.ctlRichTextBox)p_objControl).m_ClrOldPartInsertText = SystemColors.WindowText;
                        //					
                        break;
                    }
                default:
                    {
                        p_objControl.ForeColor = SystemColors.ControlText;
                        p_objControl.BackColor = SystemColors.Control;
                        break;
                    }

            }

            foreach (Control m_ctlSub in p_objControl.Controls)
            {
                m_mthSetControlViewStyle(m_ctlSub);

            }

        }

        #region ��ӡ
        private void m_pdtPintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            m_mthPrintPage(e);
        }

        private void m_pdtPintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            m_mthBeginPrint(e);
        }

        private void m_pdtPintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            m_mthEndPrint(e);
        }

        protected override long m_lngSubPrint()
        {
            //����Ƿ��д�ӡ���ݣ�����У���ӡ�����ݱ��������ӡ�ձ������ձ�����ֵ��
            if (m_objCurrentRecordContent != null)
            {
                //��������Ƿ����£���ȡ�������ݺ��״δ�ӡʱ��   
                clsTrackRecordContent objNewTrackInfo;
                long lngRes = m_objDiseaseTrackDomain.m_lngGetPrintInfo(m_objCurrentRecordContent.m_strInPatientID, m_objCurrentRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objCurrentRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objCurrentRecordContent.m_dtmModifyDate, out objNewTrackInfo, out m_dtmFirstPrintDate, out m_blnIsFirstPrint);
                if (lngRes <= 0)
                    return lngRes;

                //��������������������ݣ��ѵ�ǰ���ݼ�¼��objNewTrackInfo��
                if (objNewTrackInfo == null)
                {
                    objNewTrackInfo = m_objCurrentRecordContent;
                }

                //���ñ����ݵ���ӡ��
                m_mthSetPrintContent(objNewTrackInfo, m_dtmFirstPrintDate);
            }

            //���û�����ù���ӡ���������ô�ӡ����        
            if (!m_blnAlreadySetPrintTools)
            {
                m_mthInitPrintTool();
                m_blnAlreadySetPrintTools = true;
            }

            //��ʼ��ӡ
            m_mthStartPrint();

            return 1;
        }

        /// <summary>
        ///  ���ô�ӡ���ݡ�
        /// </summary>
        /// <param name="p_objContent"></param>
        protected virtual void m_mthSetPrintContent(clsTrackRecordContent p_objContent, DateTime p_dtmFirstPrintDate)
        {
            //ȱʡ�����κζ������Ӵ����������ṩ������
        }

        /// <summary>
        /// ��ʼ����ӡ����
        /// </summary>
        protected virtual void m_mthInitPrintTool()
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
            //��ʼ�����ݰ������д�ӡʹ�õ��ı��������塢���ʡ���ˢ����ӡ��ȡ�
        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        protected virtual void m_mthDisposePrintTools()
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
            //�ͷ����ݰ�����ӡʹ�õ������塢���ʡ���ˢ��ʹ��ϵͳ��Դ�ı�����
        }

        /// <summary>
        /// ��ʼ��ӡ��
        /// </summary>
        protected virtual void m_mthStartPrint()
        {
            //ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��
            if (m_blnDirectPrint)
            {
                m_pdcPrintDocument.Print();
            }
            else
            {
                PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();
                ppdPrintPreview.Document = m_pdcPrintDocument;
                ppdPrintPreview.ShowDialog();
            }
        }

        /// <summary>
        /// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected void m_mthBeginPrint(PrintEventArgs p_objPrintArg)
        {
            m_mthBeginPrintSub(p_objPrintArg);
        }

        /// <summary>
        /// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected virtual void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
        }

        /// <summary>
        /// ��ӡҳ
        /// </summary>
        /// <param name="p_objPrintPageArg"></param>
        protected void m_mthPrintPage(PrintPageEventArgs p_objPrintPageArg)
        {
            m_mthPrintPageSub(p_objPrintPageArg);
        }

        /// <summary>
        /// ��ӡҳ
        /// </summary>
        /// <param name="p_objPrintPageArg"></param>
        protected virtual void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {

        }

        /// <summary>
        /// ��ӡ����ʱ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected void m_mthEndPrint(PrintEventArgs p_objPrintArg)
        {
            //�����ӡ�ɹ������Ҳ��Ǵ�ӡ�ձ���������Ҫ�����״δ�ӡʱ�䣬�����״δ�ӡʱ�䡣
            if (!p_objPrintArg.Cancel)
            {
                m_mthUpdateFirstPrintDate();
            }

            m_mthEndPrintSub(p_objPrintArg);
        }

        protected void m_mthUpdateFirstPrintDate()
        {
            if (m_objCurrentRecordContent != null && m_blnIsFirstPrint)
            {
                m_objDiseaseTrackDomain.m_lngUpdateFirstPrintDate(m_objCurrentRecordContent.m_strInPatientID, m_objCurrentRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objCurrentRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtmFirstPrintDate);
                m_blnIsFirstPrint = false;
            }
        }

        /// <summary>
        /// ��ӡ����ʱ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected virtual void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            //���Ӵ����������ṩ����
        }

        #endregion ��ӡ

        // ��ʾ�Ѿ�ɾ���ļ�¼���û�ѡ�񣬲����û�ѡ���������������Ϊ��ȫ��ȷ�����ݣ���ʾ�ڽ��档
        protected void m_mthLoadDeleteRecord()
        {
            //			frmSelectDeleteRecord frmSel = new frmSelectDeleteRecord(m_objDomain,m_objCurrentPatient,m_strReloadFormTitle(),MDIParent.OperatorID);
            //		
            //			if(frmSel.ShowDialog() == DialogResult.OK)
            //			{   
            //			
            //				clsTrackRecordContent objContent = frmSel.m_objGetDeleteRecord();              
            //			
            //				if(objContent == null)
            //					return;   
            //				         
            //				//��ʾ�Ḳ�ǵ�ǰ����
            //				//����û�������
            //				//return;
            //		              
            //				m_mthClearPatientRecordInfo();                     
            //			                             
            //				m_mthReAddNewRecord(objContent);
            //			}
        }

        // ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        public virtual string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��
            return "";
        }

        protected virtual void m_trvCreateDate_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (m_blnCanTreeNodeAfterSelectEventTakePlace == false)
                return;

            m_mthRecordChangedToSave();

            m_mthClearPatientRecordInfo();



            if (m_trvCreateDate.SelectedNode == m_trnRoot)
            {
                m_mthSelectRootNode();
                if (m_objCurrentPatient != null)
                {
                    //���ò��˵���סԺ�Ļ�����Ϣ
                    m_mthOnlySetPatientInfo(m_objCurrentPatient);
                    //if (m_trvCreateDate.Nodes[0].Text != "��¼ʱ��")
                    //    m_objCurrentPatient.m_ObjPeopleInfo = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvCreateDate.Nodes[0].Nodes.Count - m_trvCreateDate.SelectedNode.Index - 1).m_ObjPeopleInfo;

                    m_mthSetDefaultValue(m_objCurrentPatient);
                }

                //��ǰ����������¼״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.AddNew);
                chkModifyWithoutMatk.Visible = false;
                chkModifyWithoutMatk.Checked = true;
            }
            else if (m_trvCreateDate.SelectedNode.Tag != null)
            {
                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objCurrentPatient);

                if (!m_blnCanShowDiseaseTrack)
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                m_mthSetSelectedRecord(m_objCurrentPatient, m_trvCreateDate.SelectedNode.Tag.ToString());

                //��ǰ�����޸ļ�¼״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.Modify);
            }

            m_mthAddFormStatusForClosingSave();
        }

        /// <summary>
        /// ��ѡ����ڵ�ʱ,���������Ĭ��ֵ(���Ӵ�����Ҫ,������ʵ��)
        /// </summary>
        protected virtual void m_mthSelectRootNode()
        {
        }

        /// <summary>
        /// ���ø������͵�Ĭ��ֵ
        /// </summary>
        /// <param name="p_objPatient"></param>
        protected virtual void m_mthSetDefaultValue(clsPatient p_objPatient)
        {
            if (m_objCurrentPatient == null)
            {
                m_objCurrentPatient = p_objPatient;
            }
            m_mthSetSpecialPatientTemplateSet(m_objCurrentPatient);
            m_mthDataShare(m_objCurrentPatient);

            //Ĭ��ֵ
            new clsDefaultValueTool(this, p_objPatient).m_mthSetDefaultValue();
        }

        //���ι鵵��ʾ
        protected override void m_mthPromtForArchiving(bool p_blnIfReadOnly, string p_strTimeRemaing)
        {
        }

        private void frmDiseaseTrackBase_Load(object sender, System.EventArgs e)
        {
            if (m_BlnNeedContextMenu)
                m_mthAddRichTemplateInContainer(this);
        }

        #region �жϵ�ǰ�û��Ƿ�����GE����
        protected bool m_blnCurrApparatus()
        {
            string strGENo = "";
            bool blnIsExist = false;
            //new clsBedGEMaintenanceDomain().m_mthGetBedGEinf(MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID, ref strGENo, ref blnIsExist);
            return blnIsExist;
        }
        #endregion �жϵ�ǰ�û��Ƿ�����GE����

        #region �Ӽ໤�ǻ�ȡ����
        /// <summary>
        /// ��ȡ��ʾ�໤���������Ӵ���ʵ�־�����ʾ����
        /// </summary>
        protected virtual void GetData()
        {
        }

        #region ��������������
        protected void m_mthGetICUDataByTime(string p_strStartTime, out clsCMSData p_objCMSData, out clsVentilatorData p_objVentilatorData, string[] p_strTypeArry)
        {
            p_objCMSData = null;
            p_objVentilatorData = null;
            //����ID�������λ����Ȼ�ᳬ��long�����Χ
            //string strLongBedID = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID.Substring(4)+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastRoomInfo.m_ObjRoom.m_StrRoomID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            string strLongBedID = MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID + MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            strLongBedID = strLongBedID.PadRight(17, '0');
            //new clsICUDataUtil(MDIParent.s_ObjCurrentPatient.m_StrInPatientID,strLongBedID).m_mthGetICUDataByTime("",p_dtmStart,out p_objCMSData,out p_objVentilatorData);
            //new clsICUDataUtil(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, strLongBedID).m_mthGetICUNumericParmatByTime(p_strStartTime, MDIParent.s_ObjCurrentPatient.m_DtmLastInDate.ToString(), out p_objCMSData, out p_objVentilatorData, p_strTypeArry);
        }
        #endregion ��������������

        #region ��ȡICU GE����
        protected void m_mthGetICUGEDataByTime(string p_strStart, out clsGECMSData p_objGECMSData)
        {
            p_objGECMSData = null;
            string strLongBedID = MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID + MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID.Substring(4) + MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastRoomInfo.m_ObjRoom.m_StrRoomID + MDIParent.s_ObjCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            //			new clsICUDataUtil(MDIParent.s_ObjCurrentPatient.m_StrInPatientID,strLongBedID).m_mthGetICUGEDataByTime(MDIParent.s_ObjCurrentPatient.m_StrInPatientID,p_dtmStart,out p_objGECMSData);
            //new clsICUDataUtil(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, strLongBedID).m_mthGetICUGEDataByTime(MDIParent.s_ObjCurrentPatient.m_StrInPatientID, p_strStart, MDIParent.s_ObjCurrentPatient.m_DtmLastInDate.ToString(), out p_objGECMSData);
        }
        #endregion ��ȡICU GE����

        #endregion �Ӽ໤�ǻ�ȡ����

        protected virtual void m_mthSelectDeptOrArea(ref int p_intDetpIndex, ref int p_intAreaIndex, clsPatient p_objPatient)
        {
            p_intDetpIndex = -1;
            p_intAreaIndex = -1;

            for (int i = 0; i < m_cboDept.GetItemsCount(); i++)
            {
                if (((clsDepartment)m_cboDept.GetItem(i)).m_StrDeptID == p_objPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjDept.m_ObjDept.m_StrDeptID)
                {
                    p_intDetpIndex = i;
                    break;
                }
            }

            for (int i = 0; i < m_cboArea.GetItemsCount(); i++)
            {
                if (((clsInPatientArea)m_cboArea.GetItem(i)).m_StrAreaID == p_objPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID)
                {
                    p_intAreaIndex = i;
                    break;
                }
            }

        }

        protected void m_mthThisAddRichTextInfo(Control p_ctlChild)
        {
            foreach (Control ctlChild in p_ctlChild.Controls)
            {
                m_mthAddRichTextInfo(ctlChild);

                if (ctlChild.HasChildren)
                {
                    m_mthThisAddRichTextInfo(ctlChild);
                }
            }
        }
        //protected override string m_StrRecorder_ID
        //{
        //    get
        //    {
        //        clsTrackRecordContent objContent = m_objGetRecordContent(m_objCurrentPatient, m_trvCreateDate.SelectedNode.Tag.ToString()); ;              
        //         return objContent.m_strCreateUserID;
        //    }
        //}
        #endregion


        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            m_objCurrentPatient = m_objBaseCurrentPatient;

            if (m_objCurrentPatient == null)
            {
                return;
            }

            m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
            m_objCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;
            m_objCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
            m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;

            m_mthIsReadOnly();
            if (!m_blnCanShowRecordContent())
            {
                clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                return;
            }

            m_mthSetPatientFormInfo(m_objCurrentPatient);
            //��ȡ���˼�¼�б�
            string[] strCreateTimeListArr;
            string[] strOpenTimeListArr;
            m_mthGetTimeList(m_objCurrentPatient, out strCreateTimeListArr, out strOpenTimeListArr);
            //��ʱ��ڵ���е�������
            new clsSortTool().m_mthSortTreeNode(m_trnRoot, true);

            //����TeeeViewĬ��ѡ��Ľڵ�
            m_mthSetNodeSelected();

            //չ������ʾ����ʱ��ڵ㡣
            m_trnRoot.Expand();
            if (m_blnIsAddNew || strOpenTimeListArr == null)
            {
                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthSetDefaultValue(m_objCurrentPatient);
                //��ǰ����������¼״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.AddNew);
            }
            else
            {
                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objCurrentPatient);

                if (!m_blnCanShowDiseaseTrack)
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                m_mthSetSelectedRecord(m_objCurrentPatient, strCreateTimeListArr[0]);

                //��ǰ�����޸ļ�¼״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.Modify);
            }
            m_mthAddFormStatusForClosingSave();

        }

        #region ɾ����¼��ѯ
        /// <summary>
        /// ����ɾ����Ĳ��̼�¼��Ϣ������ʾ�޸ġ�
        /// </summary>
        /// <param name="p_objPatient"></param>
        /// <param name="p_dtmRecordTime"></param>
        public void m_mthSetDeletedDiseaseTrackInfo(clsPatient p_objPatient,
            DateTime p_dtmRecordTime)
        {
            //�������  
            if (p_objPatient == null)
                return;

            m_mthSetPatient(p_objPatient);

            //���ü�¼��Ϣ
            m_mthSetSelectedDeletedRecord(p_objPatient, p_dtmRecordTime.ToString("yyyy-MM-dd HH:mm:ss"));

            m_mthEnablePatientSelect(false);

            m_mthAddFormStatusForClosingSave();

            //			m_mthEnablePatientSelectSub(false);
        }
        /// <summary>
        /// ����ɾ����¼�ݵ�����
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        /// <param name="p_strOpenDate"></param>
        protected virtual void m_mthSetSelectedDeletedRecord(clsPatient p_objSelectedPatient,
            string p_strOpenDate)
        {
            //������
            if (p_objSelectedPatient == null || p_strOpenDate == null || p_strOpenDate == "")
                return;

            clsTrackRecordContent objContent;
            //��ȡ��¼
            //long lngRes = m_objDiseaseTrackDomain.m_lngGetDeleteRecordContent(p_objSelectedPatient.m_StrInPatientID,m_trvCreateDate.SelectedNode.Tag.ToString(),p_strOpenDate ,out objContent);
            long lngRes = m_objDiseaseTrackDomain.m_lngGetDeleteRecordContent(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_strOpenDate, out objContent);

            if (lngRes <= 0 || objContent == null)
                return;


            //���õ�ǰ��¼����¼ʱ�� 
            m_objCurrentPatient = p_objSelectedPatient;
            txtInPatientID.Text = this.m_objCurrentPatient.m_StrHISInPatientID;
            //			m_dtpCreateDate.Value=objContent.m_dtmCreateDate;

            //��һ�������ע�͵Ļ���m_BlnIsAddNew�ͻ���true�ˣ���ô�Ͳ��������¼��
            //			m_objCurrentRecordContent=objContent;

            m_mthSetDeletedGUIFromContent(objContent);

            //			m_mthSetModifyControl(objContent,false);		

        }
        /// <summary>
        /// ������������Ϊֻ��,��ʱ����Ҫ������ʾ
        /// </summary>
        public virtual void m_mthSetReadOnly()
        {
            MDIParent.s_ObjSaveCue.m_mthRemoveForm(this);
        }
        #endregion ɾ����¼��ѯ
    }
}


