using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using iCare;
using weCare.Core.Entity;
using HRP;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using com.digitalwave.Utility;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing.Printing;
using System.Drawing.Imaging; 
namespace iCare
{
    /// <summary>
    /// ȫ�ײ�����ӡ������
    /// </summary>
    public class frmInPatientCaseHistory_SetForm : iCare.frmHRPBaseForm
    {
        private System.ComponentModel.IContainer components = null;
        // System.Windows.Forms.Button button1;
        private clsInPatientCaseHistory_SetValue m_objSetValue = new clsInPatientCaseHistory_SetValue();
        private frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
        private bool blnIsPreview = false;

        #region Members
        //private clsBorderTool m_objBorderTool;	// ���ױ�
        protected clsPatient m_objSetInPatient = null;
        protected DateTime m_dtmSetInPatientDate;
        protected System.Windows.Forms.TreeView m_trvTime;
        private ArrayList m_arlTemp = new ArrayList();//����ʱ�洢��
        protected const int MAXFORMNUM = 19;
        protected infPrintRecord[] m_objPrintRecArr = new infPrintRecord[MAXFORMNUM];//�ɸ���Ҫ��ӡ�Ĵ�����Ŀ���趨
        private System.Collections.Hashtable[] m_hasItemsArr = new Hashtable[8];
        /// <summary>
        /// ��¼��ǰ�򵽵ڼ��ֱ����㿪ʼ��
        /// </summary>
        private int m_intCurrentForm = 0;
        /// <summary>
        /// ��¼���ֱ��ж��ٷݡ��������ʾ�Ѿ�ȫ�����꣬����m_intCurrentForm�ļ��㷽���෴
        /// </summary>
        private int m_intCurrentCopies = 0;
        /// <summary>
        ///  ��¼��ǰҳ�Ƿ���ɣ�������ɣ���Ϊtrue
        /// </summary>
        private bool m_blnNewPage = true;
        protected PinkieControls.ButtonXP m_cmdOpen;
        protected PinkieControls.ButtonXP m_cmdClose;
        protected PinkieControls.ButtonXP m_cmdPreview;
        protected PinkieControls.ButtonXP m_cmdSave;

        System.Drawing.Printing.PrintDocument m_pdcPrintDocument;//��ӡ��
        System.Drawing.Printing.PrintDocument m_pdcPrintDocumentTemp = new PrintDocument();
        private System.Drawing.Printing.PrintDocument printDocument1;
        private int intPages = 0;
        protected CheckBox m_chkDoctor;
        protected CheckBox m_chkNurser;
        protected PinkieControls.ButtonXP m_cmdPrint;
        private ArrayList arlTemp;
        #endregion


        public frmInPatientCaseHistory_SetForm()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            this.Load += new EventHandler(frmInPatientCaseHistory_SetForm_Load);

            m_mthInit();
            #region White Border
            //m_objBorderTool = new clsBorderTool(Color.White);

            //foreach (Control ctlControl in this.Controls)
            //{
            //    string typeName = ctlControl.GetType().Name;
            //    if (typeName == "ctlRichTextBox")
            //    {
            //        m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
            //                                            {
            //                                                ctlControl ,
            //        });
            //    }
            //    if (typeName == "DataGrid")
            //    {
            //        m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
            //                                            {
            //                                                ctlControl ,
            //        });
            //        ((DataGrid)ctlControl).AllowSorting = false;
            //    }

            //    if (typeName == "TreeView")
            //    {
            //        m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]
            //                                            {
            //                                                ctlControl ,
            //        });

            //    }
            //}
            #endregion
            #region ��ʼ����
            TreeNode trnNode = new TreeNode("��Ժ��¼");
            trnNode.Tag = "0";
            this.m_trvTime.Nodes.Add(trnNode);
            #endregion

            m_cmdPreview.Enabled = true;
            m_cmdSave.Enabled = true;

            m_objSetValue = new clsInPatientCaseHistory_SetValue();

            #region ���ô�ӡ�¼�
            //			m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
            //			m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
            //			m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
            //			m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);		
            #endregion
            #region
            frmPreview.m_evtBeginPrint += new System.Drawing.Printing.PrintEventHandler(m_pdcPrintDocument_BeginPrint);
            frmPreview.m_evtEndPrint += new System.Drawing.Printing.PrintEventHandler(m_pdcPrintDocument_EndPrint);
            frmPreview.m_evtPrintContent += new System.Drawing.Printing.PrintPageEventHandler(m_pdcPrintDocument_PrintPage);
            //			frmPreview.m_evtPrintFrame +=new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
            //			frmPreview.ShowDialog();


            #endregion

        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void m_mthInit()
        {
            
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

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_trvTime = new System.Windows.Forms.TreeView();
            this.m_cmdOpen = new PinkieControls.ButtonXP();
            this.m_cmdClose = new PinkieControls.ButtonXP();
            this.m_cmdPreview = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_chkDoctor = new System.Windows.Forms.CheckBox();
            this.m_chkNurser = new System.Windows.Forms.CheckBox();
            this.m_cmdPrint = new PinkieControls.ButtonXP();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(338, 135);
            this.lblSex.Size = new System.Drawing.Size(24, 19);
            this.lblSex.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(325, 135);
            this.lblAge.Visible = false;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(277, 128);
            this.lblBedNoTitle.Visible = false;
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(332, 138);
            this.lblInHospitalNoTitle.Visible = false;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(320, 135);
            this.lblNameTitle.Visible = false;
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(320, 140);
            this.lblSexTitle.Visible = false;
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(335, 135);
            this.lblAgeTitle.Visible = false;
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(335, 140);
            this.lblAreaTitle.Visible = false;
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(358, 135);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(30, 23);
            this.m_lsvInPatientID.Visible = false;
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(301, 126);
            this.txtInPatientID.Size = new System.Drawing.Size(128, 23);
            this.txtInPatientID.Visible = false;
            this.txtInPatientID.TextChanged += new System.EventHandler(this.txtInPatientID_TextChanged);
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(359, 135);
            this.m_txtPatientName.ReadOnly = true;
            this.m_txtPatientName.Size = new System.Drawing.Size(30, 23);
            this.m_txtPatientName.Visible = false;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(264, 125);
            this.m_txtBedNO.Size = new System.Drawing.Size(128, 23);
            this.m_txtBedNO.Visible = false;
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(341, 135);
            this.m_cboArea.Size = new System.Drawing.Size(25, 23);
            this.m_cboArea.Visible = false;
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(358, 135);
            this.m_lsvPatientName.Size = new System.Drawing.Size(33, 19);
            this.m_lsvPatientName.Visible = false;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(328, 121);
            this.m_lsvBedNO.Size = new System.Drawing.Size(44, 31);
            this.m_lsvBedNO.Visible = false;
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(185, 125);
            this.m_cboDept.Visible = false;
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(155, 129);
            this.lblDept.Visible = false;
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(346, 122);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cmdNext.Location = new System.Drawing.Point(341, 118);
            this.m_cmdNext.Size = new System.Drawing.Size(23, 23);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cmdPre.Location = new System.Drawing.Point(368, 128);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(0, 0);
            this.m_lblForTitle.Size = new System.Drawing.Size(416, 4);
            this.m_lblForTitle.Visible = true;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(280, -31);
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(280, -34);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.Location = new System.Drawing.Point(26, 7);
            this.m_pnlNewBase.Size = new System.Drawing.Size(506, 65);
            this.m_pnlNewBase.Visible = true;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(504, 34);
            // 
            // m_trvTime
            // 
            this.m_trvTime.BackColor = System.Drawing.Color.White;
            this.m_trvTime.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_trvTime.ForeColor = System.Drawing.Color.Black;
            this.m_trvTime.Location = new System.Drawing.Point(358, 135);
            this.m_trvTime.Name = "m_trvTime";
            this.m_trvTime.Size = new System.Drawing.Size(71, 19);
            this.m_trvTime.TabIndex = 499;
            this.m_trvTime.Visible = false;
            this.m_trvTime.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_trvTime_AfterSelect);
            // 
            // m_cmdOpen
            // 
            this.m_cmdOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdOpen.DefaultScheme = true;
            this.m_cmdOpen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdOpen.Hint = "";
            this.m_cmdOpen.Location = new System.Drawing.Point(334, 94);
            this.m_cmdOpen.Name = "m_cmdOpen";
            this.m_cmdOpen.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdOpen.Size = new System.Drawing.Size(92, 32);
            this.m_cmdOpen.TabIndex = 10000005;
            this.m_cmdOpen.Text = "��  ��";
            this.m_cmdOpen.Click += new System.EventHandler(this.m_cmdOpen_Click);
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(435, 94);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(92, 32);
            this.m_cmdClose.TabIndex = 10000004;
            this.m_cmdClose.Text = "��  ��";
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // m_cmdPreview
            // 
            this.m_cmdPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdPreview.DefaultScheme = true;
            this.m_cmdPreview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPreview.Hint = "";
            this.m_cmdPreview.Location = new System.Drawing.Point(28, 94);
            this.m_cmdPreview.Name = "m_cmdPreview";
            this.m_cmdPreview.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPreview.Size = new System.Drawing.Size(92, 32);
            this.m_cmdPreview.TabIndex = 10000005;
            this.m_cmdPreview.Text = "Ԥ��";
            this.m_cmdPreview.Click += new System.EventHandler(this.m_cmdPreview_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(231, 94);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(92, 32);
            this.m_cmdSave.TabIndex = 10000004;
            this.m_cmdSave.Text = "��  ��";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_chkDoctor
            // 
            this.m_chkDoctor.AutoSize = true;
            this.m_chkDoctor.Location = new System.Drawing.Point(99, 47);
            this.m_chkDoctor.Name = "m_chkDoctor";
            this.m_chkDoctor.Size = new System.Drawing.Size(124, 18);
            this.m_chkDoctor.TabIndex = 10000006;
            this.m_chkDoctor.Text = "ҽ������վ����";
            this.m_chkDoctor.UseVisualStyleBackColor = true;
            // 
            // m_chkNurser
            // 
            this.m_chkNurser.AutoSize = true;
            this.m_chkNurser.Location = new System.Drawing.Point(229, 47);
            this.m_chkNurser.Name = "m_chkNurser";
            this.m_chkNurser.Size = new System.Drawing.Size(124, 18);
            this.m_chkNurser.TabIndex = 10000007;
            this.m_chkNurser.Text = "��ʿ����վ����";
            this.m_chkNurser.UseVisualStyleBackColor = true;
            // 
            // m_cmdPrint
            // 
            this.m_cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdPrint.DefaultScheme = true;
            this.m_cmdPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrint.Hint = "";
            this.m_cmdPrint.Location = new System.Drawing.Point(129, 94);
            this.m_cmdPrint.Name = "m_cmdPrint";
            this.m_cmdPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrint.Size = new System.Drawing.Size(92, 32);
            this.m_cmdPrint.TabIndex = 10000005;
            this.m_cmdPrint.Text = "��ӡ";
            this.m_cmdPrint.Click += new System.EventHandler(this.m_cmdPrint_Click);
            // 
            // frmInPatientCaseHistory_SetForm
            // 
            this.ClientSize = new System.Drawing.Size(559, 239);
            this.Controls.Add(this.m_cmdSave);
            this.Controls.Add(this.m_cmdPreview);
            this.Controls.Add(this.m_chkDoctor);
            this.Controls.Add(this.m_cmdOpen);
            this.Controls.Add(this.m_cmdPrint);
            this.Controls.Add(this.m_cmdClose);
            this.Controls.Add(this.m_chkNurser);
            this.Controls.Add(this.m_trvTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInPatientCaseHistory_SetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ȫ�ײ�����ӡ������";
            this.Load += new System.EventHandler(this.frmInPatientCaseHistory_SetForm_Load_1);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_trvTime, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.m_chkNurser, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_cmdClose, 0);
            this.Controls.SetChildIndex(this.m_cmdPrint, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.m_cmdOpen, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_chkDoctor, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.m_cmdPreview, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdSave, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
        {
            //base.m_mthSetPatientBaseInfo(p_objSelectedPatient);
            m_objSetInPatient = p_objSelectedPatient;
            //m_objBaseCurrentPatient;
            //m_mthLoadTree();
            m_dtmSetInPatientDate = m_objSetInPatient.m_DtmSelectedInDate;

        }

        /// <summary>
        /// ���ⲿ����ȫ�ײ���
        /// </summary>
        public void m_mthSetAllCaseFromOtherProj()
        {
            if (MDIParent.m_objCurrentPatient == null)
                return;

            clsPeopleInfo objPeo = new clsPeopleInfo();
            objPeo.m_StrLastName = MDIParent.m_objCurrentPatient.m_strLASTNAME_VCHR;
            objPeo.m_StrFirstName = MDIParent.m_objCurrentPatient.m_strLASTNAME_VCHR;
            clsPatient objPatient = new clsPatient(MDIParent.m_objCurrentPatient.m_strEMRInPatientID,
                MDIParent.m_objCurrentPatient.m_strHISInPatientID, objPeo);

            objPatient.m_strDeptNewID = MDIParent.m_objCurrentPatient.m_strDEPTID_CHR;
            objPatient.m_strAreaNewID = MDIParent.m_objCurrentPatient.m_strAREAID_CHR;
            objPatient.m_strBedCode = MDIParent.m_objCurrentPatient.m_strCODE_CHR;
            if (objPatient == null)
                return;

            m_mthSetPatientFormInfo(objPatient);
        }

        #region ����ʵ�ֵ���print toolװ����Ե�����
        //��ȡר�Ʋ����Ĵ�ӡ����
        private object[] m_objGetContentArr(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime[] p_dtmOpenDateArr)
        {
            object[] objObjArr = new object[p_dtmOpenDateArr.Length];
            try
            {
                //m_arlTemp.Clear();
                for (int i = 0; i < p_dtmOpenDateArr.Length; i++)
                {
                    p_objPrintRec.m_mthSetPrintInfo(p_objPatient, p_dtmInPatientDate, p_dtmOpenDateArr[i]);
                    p_objPrintRec.m_mthInitPrintContent();
                    if (clsInpatMedRecPrintBase.m_hasItems == null)
                        return null;
                    else
                    {
                        objObjArr[i] = p_objPrintRec.m_objGetPrintInfo();
                    }
                }

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return objObjArr;

        }
        //��ȡ���������Ĵ�ӡ����(��ר�Ʋ���֮��)
        private object[] m_objGetContentArr(infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime[] p_dtmOpenDateArr)
        {
            object[] objObjArr = new object[p_dtmOpenDateArr.Length];
            try
            {
                //m_arlTemp.Clear();
                for (int i = 0; i < p_dtmOpenDateArr.Length; i++)
                {
                    p_objPrintRec.m_mthSetPrintInfo(p_objPatient, p_dtmInPatientDate, p_dtmOpenDateArr[i]);
                    p_objPrintRec.m_mthInitPrintContent();
                    objObjArr[i] = p_objPrintRec.m_objGetPrintInfo();
                    if (objObjArr[i] == null)
                        return null;
                }
                if (objObjArr.Length == 0)
                    return null;


            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return objObjArr;//m_arlTemp.ToArray(p_objContentType);
        }
        //strDate����Ϊ��Ч����������,����Ϊnull������������ֵ
        private object[] m_objGetContentArr(infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate, string[] p_strOpenDateArr)
        {
            object[] objObjArr = new object[p_strOpenDateArr.Length];
            try
            {
                for (int i = 0; i < p_strOpenDateArr.Length; i++)
                {
                    p_objPrintRec.m_mthSetPrintInfo(p_objPatient, p_dtmInPatientDate, DateTime.Parse(p_strOpenDateArr[i]));
                    p_objPrintRec.m_mthInitPrintContent();
                    objObjArr[i] = p_objPrintRec.m_objGetPrintInfo();
                    if (objObjArr[i] == null)
                        return null;
                }

                if (objObjArr.Length == 0) return null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return objObjArr;
        }

        #endregion

        #region ���ô����domain��print toolװ����Ե����ݵĺ���
        /*
        #region ҽ������վ����
        //��ͨסԺ����
        protected long lngLoadInPatientCaseHistory(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                //��������������㣬˵���ò���û�д�ӡ���ݣ����Բ����ӡ����������������
                if (m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr == null || m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ͨסԺ����(���)
        protected long lngLoadInPatientCaseHistory_F2(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr == null || m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���̼�¼
        protected long lngLoadSubDiseaseTrack(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //			clsRecordsDomain objRecordsDomain=clsRecordsDomainFactory.s_objGetRecordsDomain(enmRecordsType.IntensiveTend);
                //string[] strDate=null; //objRecordsDomain..m_strGetAllTendRecordCreateDateArr(p_objPatient.m_StrInPatientID,p_dtmInPatientDate.ToString());
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {

                    //infPrintRecord p_objPrintRec=new clsIntensiveTendMainPrintTool();
                    m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr == null || m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����¼
        protected long lngLoadConsultation(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                string[] strDate = null;
                string[] strTempDate = null;
                lngRes = new clsConsultationDomain().m_lngGetRecordTimeList(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strTempDate, out strDate);
                if (strDate != null || p_objPatient == null)
                {
                    m_objSetValue.m_objPrintInfo_ConsultationArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);
                }
                if (m_objSetValue.m_objPrintInfo_ConsultationArr == null ||
                    m_objSetValue.m_objPrintInfo_ConsultationArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���Ǻ������֪��ͬ����
        protected long lngLoadOperationAgreed(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new clsOperationAgreedRecordDomain().m_dtmGetTimeInfoOfAPatientArr(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString());
                if (dtmDate != null || p_objPrintRec == null)
                {

                    //infPrintRecord p_objPrintRec=new clsIntensiveTendMainPrintTool();
                    m_objSetValue.m_objPrintInfo_OperationAgreedArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_OperationAgreedArr == null ||
                    m_objSetValue.m_objPrintInfo_OperationAgreedArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //B�����뵥
        protected long lngLoadBUltrasonicCheckOrder(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new clsBUltrasonicCheckOrderDomain().m_dtmGetTimeInfoOfAPatientArr(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString());
                if (dtmDate != null || p_objPrintRec == null)
                {

                    //infPrintRecord p_objPrintRec=new clsIntensiveTendMainPrintTool();
                    m_objSetValue.m_objPrintInfo_BUltrasonicCheckOrderArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_BUltrasonicCheckOrderArr == null ||
                    m_objSetValue.m_objPrintInfo_BUltrasonicCheckOrderArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ǰС��
        protected long lngLoadBeforeOperationSummary(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                string[] strDate = null;
                lngRes = new clsBeforeOperationSummaryDomain().m_lngGetOperationDateArr(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strDate);
                if (strDate != null && p_objPrintRec != null)
                {
                    //infPrintRecord objPrintRec=new clsIntensiveTendMainPrintTool();
                    m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);
                }
                if (m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr == null ||
                    m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr[0] == null) p_objPrintRec = null;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;

        }
        //������¼��
        protected long lngLoadOperationRecordDoctor(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[,] dtmTempDate = new clsOperationRecordDoctorDomain().m_dtmGetTimeInfoOfAPatientArr(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString());
                if (dtmTempDate == null)
                {
                    p_objPrintRec = null;
                    return 0;
                }
                DateTime[] dtmDate = new DateTime[dtmTempDate.Length / 2];
                if (dtmTempDate != null)
                {

                    for (int i = 0; i < dtmTempDate.Length / 2; i++)
                    {
                        dtmDate[i] = dtmTempDate[i, 1];
                    }
                }
                if (dtmDate != null && p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr == null ||
                    m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr[0] == null) p_objPrintRec = null;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //ICUת���¼
        protected long lngLoadICUShiftIn(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                string[] strDate = new clsPICUShiftInDomain().m_strGetCreateDateArr(p_objPatient);
                if (strDate != null && p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_PICUShiftInArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);
                }
                if (m_objSetValue.m_objPrintInfo_PICUShiftInArr == null ||
                    m_objSetValue.m_objPrintInfo_PICUShiftInArr[0] == null) p_objPrintRec = null;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //ICUת����¼
        protected long lngLoadICUShiftOut(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                string[] strDate = new clsPICUShiftOutDomain().m_strGetCreateDateArr(p_objPatient);
                if (strDate != null && p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_PICUShiftOutArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);
                }
                if (m_objSetValue.m_objPrintInfo_PICUShiftOutArr == null ||
                    m_objSetValue.m_objPrintInfo_PICUShiftOutArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��Ժ��¼
        protected long lngLoadOutHospital(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                string[] strDate = null;
                string[] strTempDate = null;
                lngRes = new clsOutHospitalDomain().m_lngGetRecordTimeList(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strTempDate, out strDate);
                if (strDate != null || p_objPatient == null)
                {
                    m_objSetValue.m_objPrintInfo_OutHospitalArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);
                }
                if (m_objSetValue.m_objPrintInfo_OutHospitalArr == null ||
                    m_objSetValue.m_objPrintInfo_OutHospitalArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //סԺ������ҳ
        protected long lngLoadInHospitalMainRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //string[] strDate=new string[1];
                //lngRes=new clsInHospitalMainRecordDomain().m_lngGetOpenDateInfo(p_objPatient.m_StrInPatientID,p_dtmInPatientDate.ToString(),out strDate[0]);
                if (p_objPrintRec != null)
                {
                    //	infPrintRecord objPrintRec=new clsIntensiveTendMainPrintTool();
                    //����һ����Ժֻ�и�����OpenDate��㴫һ��
                    m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr == null || m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��Ժ��������	����һ����Ժֻ�и�����OpenDate��㴫һ��
        protected long lngLoadInPatientEvaluate(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //infPrintRecord objPrintRec=new clsIntensiveTendMainPrintTool();
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_InPatientEvaluateArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_InPatientEvaluateArr == null ||
                    m_objSetValue.m_objPrintInfo_InPatientEvaluateArr[0] == null) p_objPrintRec = null;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;

        }
        //����ժҪ
        //protected void lngLoadCaseHistorySummary(clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        //{
        //    if (p_objPatient != null)
        //    {
        //        m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr = new object[2];
        //        m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr[0] = p_objPatient;
        //        m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr[1] = p_dtmInPatientDate;
        //    }
        //}

        //�����¼�
        //24Сʱ�����Ժ��¼
        protected long lngLoadEMR_OutHospitalIn24Hours(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                //DateTime[] dtmDate = new DateTime[1];
                //dtmDate[0] = new DateTime(0);
                string[] strDate = null;
                string[] strTempDate = null;
                lngRes = new clsOutHospitalIn24HoursDomain().m_lngGetRecordTimeList(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strTempDate, out strDate);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (strDate != null && p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_EMR_OutHospitalIn24HoursArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);

                }
                if (m_objSetValue.m_objPrintInfo_EMR_OutHospitalIn24HoursArr == null || m_objSetValue.m_objPrintInfo_EMR_OutHospitalIn24HoursArr[0] == null || ((clsPrintInfo_OutHospitalIn24Hours)m_objSetValue.m_objPrintInfo_EMR_OutHospitalIn24HoursArr[0]).m_objRecordContent == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��Ժ24Сʱ��������¼
        protected long lngLoadDeathRecordIn24Hours(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                //DateTime[] dtmDate = new DateTime[1];
                //dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                string[] strDate = null;
                string[] strTempDate = null;
                lngRes = new clsDeathRecordIn24HoursDomain().m_lngGetRecordTimeList(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strTempDate, out strDate);
                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_DeathRecordIn24HoursArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);

                }
                if (m_objSetValue.m_objPrintInfo_DeathRecordIn24HoursArr == null || m_objSetValue.m_objPrintInfo_DeathRecordIn24HoursArr[0] == null || ((clsPrintInfo_DeathRecordIn24Hours)m_objSetValue.m_objPrintInfo_DeathRecordIn24HoursArr[0]).m_objRecordContent == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //������¼
        protected long lngLoadDeathrecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                //DateTime[] dtmDate = new DateTime[1];
                //dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                string[] strDate = null;
                string[] strTempDate = null;
                lngRes = new clsDeathRecordDomain().m_lngGetRecordTimeList(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strTempDate, out strDate);

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_DeathrecordArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);

                }
                if (m_objSetValue.m_objPrintInfo_DeathrecordArr == null || m_objSetValue.m_objPrintInfo_DeathrecordArr[0] == null || ((clsPrintInfo_DeathRecord)m_objSetValue.m_objPrintInfo_DeathrecordArr[0]).m_objRecordContent == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����������ۼ�¼
        protected long lngLoadDeathDiscuss(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                //DateTime[] dtmDate = new DateTime[1];
                //dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                string[] strDate = null;
                string[] strTempDate = null;
                lngRes = new clsDiseaseTrackDomain(enmDiseaseTrackType.DeathCaseDiscuss).m_lngGetRecordTimeList(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strTempDate, out strDate);

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_DeathDiscussArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);

                }
                if (m_objSetValue.m_objPrintInfo_DeathDiscussArr == null || m_objSetValue.m_objPrintInfo_DeathDiscussArr[0] == null || ((clsPrintInfo_DeathCaseDiscussRecord)m_objSetValue.m_objPrintInfo_DeathDiscussArr[0]).m_objRecordContent == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        */
#endregion
        #region ���뵥
        //B�ͳ������������뵥
        //CT������뵥
        //X�����뵥
        //SPECT������뵥
        //��ѹ���������뵥
        //���������֯�ͼ쵥
        //MRI���뵥
        //�ĵ�ͼ���뵥
        //���Զർ˯��ͼ������뵥
        //��ҽѧ������뵥
        #endregion
        
        #region ר�Ʋ�����

        /*
        //���ﲡ��
        protected long lngLoadEmergencyCall(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_EmergencyCallArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_EmergencyCallArr != null)
                        m_hasItemsArr[0] = clsInpatMedRecPrintBase.m_hasItems;

                }
                if (m_objSetValue.m_objPrintInfo_EmergencyCallArr == null || m_objSetValue.m_objPrintInfo_EmergencyCallArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //ѪҺר��סԺ����
        protected long lngLoadBloodAcadInHospitalCaseHistoryOne(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_BloodAcadInHospitalCaseHistoryOneArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_BloodAcadInHospitalCaseHistoryOneArr != null)
                        m_hasItemsArr[1] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_BloodAcadInHospitalCaseHistoryOneArr == null || m_objSetValue.m_objPrintInfo_BloodAcadInHospitalCaseHistoryOneArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }

        //����������ʱ��¼
        protected long lngLoadChildbearingRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_ChildbearingRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_ChildbearingRecordArr != null)
                        m_hasItemsArr[2] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_ChildbearingRecordArr == null || m_objSetValue.m_objPrintInfo_ChildbearingRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����סԺ����(��һ)
        protected long lngLoadGynecologyCaseHis(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_GynecologyCaseHisArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_GynecologyCaseHisArr != null)
                        m_hasItemsArr[3] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_GynecologyCaseHisArr == null || m_objSetValue.m_objPrintInfo_GynecologyCaseHisArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����ڿ�סԺ����
        protected long lngLoadIMR_Breath(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_BreathArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_BreathArr != null)
                        m_hasItemsArr[20] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_BreathArr == null || m_objSetValue.m_objPrintInfo_IMR_BreathArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�������סԺ����(���ʽ)
        protected long lngLoadIMR_BurnSuergery(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_BurnSuergeryArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_BurnSuergeryArr != null)
                        m_hasItemsArr[5] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_BurnSuergeryArr == null || m_objSetValue.m_objPrintInfo_IMR_BurnSuergeryArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��Ѫ�����סԺ����
        protected long lngLoadIMR_Cardiovascular(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_CardiovascularArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_CardiovascularArr != null)
                        m_hasItemsArr[6] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_CardiovascularArr == null || m_objSetValue.m_objPrintInfo_IMR_CardiovascularArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��������������֪��ͬ����
        protected long lngLoadIMR_CataractSuffererApprove(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_CataractSuffererApproveArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_CataractSuffererApproveArr != null)
                        m_hasItemsArr[7] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_CataractSuffererApproveArr == null || m_objSetValue.m_objPrintInfo_IMR_CataractSuffererApproveArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�ʹ���������¼
        protected long lngLoadIMR_CesareanRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr != null)
                        m_hasItemsArr[8] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����סԺ����(ʳ������������)
        protected long lngLoadIMR_ChestSurgery_I(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IArr != null)
                        m_hasItemsArr[9] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IArr == null || m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����סԺ����(�ط��ݸ���)
        protected long lngLoadIMR_ChestSurgery_II(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIArr != null)
                        m_hasItemsArr[10] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIArr == null || m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����סԺ����(���ٲ�)
        protected long lngLoadIMR_ChestSurgery_III(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIIArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIIArr != null)
                        m_hasItemsArr[11] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIIArr == null || m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIIArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����¼
        protected long lngLoadIMR_childbirth(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_childbirthArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_childbirthArr != null)
                        m_hasItemsArr[12] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_childbirthArr == null || m_objSetValue.m_objPrintInfo_IMR_childbirthArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //������ת�Ƽ�¼
        protected long lngLoadIMR_ChildbirthTransitSection(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ChildbirthTransitSectionArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ChildbirthTransitSectionArr != null)
                        m_hasItemsArr[13] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ChildbirthTransitSectionArr == null || m_objSetValue.m_objPrintInfo_IMR_ChildbirthTransitSectionArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����סԺ����
        protected long lngLoadIMR_DiabetesHospital(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_DiabetesHospitalArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_DiabetesHospitalArr != null)
                        m_hasItemsArr[14] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_DiabetesHospitalArr == null || m_objSetValue.m_objPrintInfo_IMR_DiabetesHospitalArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�������۲���
        protected long lngLoadIMR_EmergencyCallWound(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_EmergencyCallWoundArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_EmergencyCallWoundArr != null)
                        m_hasItemsArr[15] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_EmergencyCallWoundArr == null || m_objSetValue.m_objPrintInfo_IMR_EmergencyCallWoundArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ǯ������¼
        protected long lngLoadIMR_ForpecsRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr != null)
                        m_hasItemsArr[16] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��������������¼
        protected long lngLoadIMR_GestationMisbirth_1(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr != null)
                        m_hasItemsArr[17] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr == null || m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����������������¼
        protected long lngLoadIMR_GestationMisbirth_2(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_2Arr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_2Arr != null)
                        m_hasItemsArr[18] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_2Arr == null || m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_2Arr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ѹ����ǯ����������¼
        protected long lngLoadIMR_GestationMisbirth_3(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_3Arr =
                        m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_3Arr != null)
                        m_hasItemsArr[19] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_3Arr == null || m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_3Arr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����סԺ����
        protected long lngLoadIMR_Gynecology(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_GynecologyArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_GynecologyArr != null)
                        m_hasItemsArr[4] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_GynecologyArr == null || m_objSetValue.m_objPrintInfo_IMR_GynecologyArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���ಡסԺ��¼
        protected long lngLoadIMR_HeartHospitalRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_HeartHospitalRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_HeartHospitalRecordArr != null)
                        m_hasItemsArr[21] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_HeartHospitalRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_HeartHospitalRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����ҽѧ����Ժ��¼
        protected long lngLoadIMR_Herbalism(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_HerbalismArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_HerbalismArr != null)
                        m_hasItemsArr[22] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_HerbalismArr == null || m_objSetValue.m_objPrintInfo_IMR_HerbalismArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����ҽ��Ͽ�
        protected long lngLoadIMR_Herbalist_Western(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_Herbalist_WesternArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_Herbalist_WesternArr != null)
                        m_hasItemsArr[23] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_Herbalist_WesternArr == null || m_objSetValue.m_objPrintInfo_IMR_Herbalist_WesternArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //ҩ��������¼��
        protected long lngLoadIMR_MedcineMiscarryRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_MedcineMiscarryRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_MedcineMiscarryRecordArr != null)
                        m_hasItemsArr[24] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_MedcineMiscarryRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_MedcineMiscarryRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //������סԺ����
        protected long lngLoadIMR_Neonatal(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_NeonatalArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_NeonatalArr != null)
                        m_hasItemsArr[25] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_NeonatalArr == null || m_objSetValue.m_objPrintInfo_IMR_NeonatalArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���ڿ�סԺ����
        protected long lngLoadIMR_Neuromedicine(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_NeuromedicineArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_NeuromedicineArr != null)
                        m_hasItemsArr[26] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_NeuromedicineArr == null || m_objSetValue.m_objPrintInfo_IMR_NeuromedicineArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����סԺ����
        protected long lngLoadIMR_Neurosurgery(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_NeurosurgeryArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_NeurosurgeryArr != null)
                        m_hasItemsArr[27] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_NeurosurgeryArr == null || m_objSetValue.m_objPrintInfo_IMR_NeurosurgeryArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��������Ժ��¼
        protected long lngLoadIMR_NewChild(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_NewChildArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_NewChildArr != null)
                        m_hasItemsArr[28] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_NewChildArr == null || m_objSetValue.m_objPrintInfo_IMR_NewChildArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����סԺ����
        protected long lngLoadIMR_Obstetric(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ObstetricArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ObstetricArr != null)
                        m_hasItemsArr[43] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ObstetricArr == null || m_objSetValue.m_objPrintInfo_IMR_ObstetricArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //������Ժ��¼
        protected long lngLoadIMR_Obstetric_Criterion(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_Obstetric_CriterionArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_Obstetric_CriterionArr != null)
                        m_hasItemsArr[30] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_Obstetric_CriterionArr == null || m_objSetValue.m_objPrintInfo_IMR_Obstetric_CriterionArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���������¼[Doctor]
        protected long lngLoadIMR_OperateManageRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_OperateManageRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_OperateManageRecordArr != null)
                        m_hasItemsArr[48] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_OperateManageRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_OperateManageRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�ۿ�סԺ����
        protected long lngLoadIMR_Ophthalmology(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_OphthalmologyArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_OphthalmologyArr != null)
                        m_hasItemsArr[31] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_OphthalmologyArr == null || m_objSetValue.m_objPrintInfo_IMR_OphthalmologyArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�ǿ�(��������΢�������)����֪��ͬ����
        protected long lngLoadIMR_OrthopaedicsSuffererAprrove(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_OrthopaedicsSuffererAprroveArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_OrthopaedicsSuffererAprroveArr != null)
                        m_hasItemsArr[32] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_OrthopaedicsSuffererAprroveArr == null || m_objSetValue.m_objPrintInfo_IMR_OrthopaedicsSuffererAprroveArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����סԺ����
        protected long lngLoadIMR_Paediatrics(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_PaediatricsArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_PaediatricsArr != null)
                        m_hasItemsArr[33] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_PaediatricsArr == null || m_objSetValue.m_objPrintInfo_IMR_PaediatricsArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //������Ժ��¼
        protected long lngLoadIMR_Paediatrics01(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_Paediatrics01Arr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_Paediatrics01Arr != null)
                        m_hasItemsArr[34] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_Paediatrics01Arr == null || m_objSetValue.m_objPrintInfo_IMR_Paediatrics01Arr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���ڽ�������������¼��
        protected long lngLoadIMR_PalaceBirthControlLaySkill(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_PalaceBirthControlLaySkillArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_PalaceBirthControlLaySkillArr != null)
                        m_hasItemsArr[35] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_PalaceBirthControlLaySkillArr == null || m_objSetValue.m_objPrintInfo_IMR_PalaceBirthControlLaySkillArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ǰ������ӵ�
        protected long lngLoadIMR_PrePostOperateSee(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_PrePostOperateSeeArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_PrePostOperateSeeArr != null)
                        m_hasItemsArr[36] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_PrePostOperateSeeArr == null || m_objSetValue.m_objPrintInfo_IMR_PrePostOperateSeeArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ʪ�����ڿ�סԺ����
        protected long lngLoadIMR_RheumatismImmunity(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_RheumatismImmunityArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_RheumatismImmunityArr != null)
                        m_hasItemsArr[37] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_RheumatismImmunityArr == null || m_objSetValue.m_objPrintInfo_IMR_RheumatismImmunityArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //θ�����סԺ����
        protected long lngLoadIMR_StoIntesChirurgeryl(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_StoIntesChirurgerylArr =
                        m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_StoIntesChirurgerylArr != null)
                        m_hasItemsArr[38] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_StoIntesChirurgerylArr == null || m_objSetValue.m_objPrintInfo_IMR_StoIntesChirurgerylArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���ڽ�����ȡ������¼��
        protected long lngLoadIMR_WombBirthControlRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_WombBirthControlRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_WombBirthControlRecordArr != null)
                        m_hasItemsArr[39] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_WombBirthControlRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_WombBirthControlRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���ڿ�סԺ����
        protected long lngLoadKidneyMedicineBeInHospital(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_KidneyMedicineBeInHospitalArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_KidneyMedicineBeInHospitalArr != null)
                        m_hasItemsArr[40] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_KidneyMedicineBeInHospitalArr == null || m_objSetValue.m_objPrintInfo_KidneyMedicineBeInHospitalArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�˹��������������ѹܼ�¼
        protected long lngLoadManpowerAbortion(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_ManpowerAbortionArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_ManpowerAbortionArr != null)
                        m_hasItemsArr[41] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_ManpowerAbortionArr == null || m_objSetValue.m_objPrintInfo_ManpowerAbortionArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����סԺ����(��һ)
        protected long lngLoadObstetricCaseHis(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_ObstetricCaseHisArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_ObstetricCaseHisArr != null)
                        m_hasItemsArr[42] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_ObstetricCaseHisArr == null || m_objSetValue.m_objPrintInfo_ObstetricCaseHisArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��Ѫ�ܲ�����
        protected long lngLoadIMR_Cerebrovascular(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_CerebrovascularArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_CerebrovascularArr != null)
                        m_hasItemsArr[29] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_CerebrovascularArr == null || m_objSetValue.m_objPrintInfo_IMR_CerebrovascularArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����������
        protected long lngLoadIMR_NerveSystem(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_NerveSystemArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_NerveSystemArr != null)
                        m_hasItemsArr[44] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_NerveSystemArr == null || m_objSetValue.m_objPrintInfo_IMR_NerveSystemArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���ʰ�����
        protected long lngLoadIMR_NasopharyngelCarcinoma(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_NasopharyngelCarcinomaArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_NasopharyngelCarcinomaArr != null)
                        m_hasItemsArr[45] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_NasopharyngelCarcinomaArr == null || m_objSetValue.m_objPrintInfo_IMR_NasopharyngelCarcinomaArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���Ǻ�Ʋ���
        protected long lngLoadIMR_IllnessHistoryRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_IllnessHistoryRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_IllnessHistoryRecordArr != null)
                        m_hasItemsArr[46] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_IllnessHistoryRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_IllnessHistoryRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����̥ͷ����������������¼
        protected long lngLoadEMR_PullDeliverRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_EMR_PullDeliverRecordArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    
                }
                if (m_objSetValue.m_objPrintInfo_EMR_PullDeliverRecordArr == null || m_objSetValue.m_objPrintInfo_EMR_PullDeliverRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        #endregion
         */
        //סԺ������ҳ
        protected long lngLoadInHospitalMainRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {

            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //string[] strDate=new string[1];
                //lngRes=new clsInHospitalMainRecordDomain().m_lngGetOpenDateInfo(p_objPatient.m_StrInPatientID,p_dtmInPatientDate.ToString(),out strDate[0]);
                if (p_objPrintRec != null)
                {
                    //	infPrintRecord objPrintRec=new clsIntensiveTendMainPrintTool();
                    //����һ����Ժֻ�и�����OpenDate��㴫һ��
                    m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr == null || m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ͨסԺ����(���)
        protected long lngLoadInPatientCaseHistory_F2(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr =
                    m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate,dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr == null || m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��Ժ��������	����һ����Ժֻ�и�����OpenDate��㴫һ��
        protected long lngLoadInPatientEvaluate(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //infPrintRecord objPrintRec=new clsIntensiveTendMainPrintTool();
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_InPatientEvaluateArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_InPatientEvaluateArr == null ||
                    m_objSetValue.m_objPrintInfo_InPatientEvaluateArr[0] == null) p_objPrintRec = null;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;

        }
        //����סԺ����
        protected long lngLoadIMR_Gynecology(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_GynecologyArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_GynecologyArr != null)
                        m_hasItemsArr[0] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_GynecologyArr == null || m_objSetValue.m_objPrintInfo_IMR_GynecologyArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����סԺ����
        protected long lngLoadIMR_Obstetric(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ObstetricArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ObstetricArr != null)
                        m_hasItemsArr[1] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ObstetricArr == null || m_objSetValue.m_objPrintInfo_IMR_ObstetricArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����������ʱ��¼
        protected long lngLoadChildbearingRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_ChildbearingRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_ChildbearingRecordArr != null)
                        m_hasItemsArr[2] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_ChildbearingRecordArr == null || m_objSetValue.m_objPrintInfo_ChildbearingRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����������ȡ�����¾��󸹲����ѹܽ�����¼
        protected long lngLoadManpowerAbortionRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ManpowerAbortionRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ManpowerAbortionRecordArr != null)
                        m_hasItemsArr[3] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ManpowerAbortionRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_ManpowerAbortionRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //������¼��
        protected long lngLoadInducedLaborRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_InducedLaborRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_InducedLaborRecordArr != null)
                        m_hasItemsArr[4] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_InducedLaborRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_InducedLaborRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ǯ������¼
        protected long lngLoadIMR_ForpecsRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr != null)
                        m_hasItemsArr[5] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�ʹ���������¼
        protected long lngLoadIMR_CesareanRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr != null)
                        m_hasItemsArr[6] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr == null || m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��������������¼
        protected long lngLoadIMR_GestationMisbirth_1(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr =
                            m_objGetContentArr(ref p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                    if (m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr != null)
                        m_hasItemsArr[7] = clsInpatMedRecPrintBase.m_hasItems;
                }
                if (m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr == null || m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��ǰС��
        protected long lngLoadBeforeOperationSummary(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                string[] strDate = null;
                lngRes = new clsBeforeOperationSummaryDomain().m_lngGetOperationDateArr(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strDate);
                if (strDate != null && p_objPrintRec != null)
                {
                    //infPrintRecord objPrintRec=new clsIntensiveTendMainPrintTool();
                    m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);
                }
                if (m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr == null ||
                    m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr[0] == null) p_objPrintRec = null;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;

        }
        //��Ժ��¼
        protected long lngLoadOutHospital(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                string[] strDate = null;
                string[] strTempDate = null;
                lngRes = new clsOutHospitalDomain().m_lngGetRecordTimeList(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out strTempDate, out strDate);
                if (strDate != null || p_objPatient == null)
                {
                    m_objSetValue.m_objPrintInfo_OutHospitalArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, strDate);
                }
                if (m_objSetValue.m_objPrintInfo_OutHospitalArr == null ||
                    m_objSetValue.m_objPrintInfo_OutHospitalArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���̼�¼
        protected long lngLoadSubDiseaseTrack(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //			clsRecordsDomain objRecordsDomain=clsRecordsDomainFactory.s_objGetRecordsDomain(enmRecordsType.IntensiveTend);
                //string[] strDate=null; //objRecordsDomain..m_strGetAllTendRecordCreateDateArr(p_objPatient.m_StrInPatientID,p_dtmInPatientDate.ToString());
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {

                    //infPrintRecord p_objPrintRec=new clsIntensiveTendMainPrintTool();
                    m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr == null || m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��Ӥ��Ӥ��������
        protected long lngLoadAYQBabyAssessmentRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_AYQBabyAssessmentRecordArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_AYQBabyAssessmentRecordArr == null ||
                    m_objSetValue.m_objPrintInfo_AYQBabyAssessmentRecordArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���̼�¼
        protected long lngLoadWaitLayRecord_Acad1(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr == null ||
                    m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�߲��ؾ�����ι۲��
        protected long lngLoadOXTIntravenousDrip(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_OXTIntravenousDripArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_OXTIntravenousDripArr == null ||
                    m_objSetValue.m_objPrintInfo_OXTIntravenousDripArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����¼
        protected long lngLoadWaitLayRecord_Acad(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr == null ||
                    m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        #endregion
        #region ��ʿ����վ����
        //�����
        protected long lngLoadThreeMeasureRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr == null || m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;

        }
        //һ�㻤���¼
        protected long lngLoadGeneralTendRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                //lngRes=new clsGeneralTendRecordDomain().m_lngGetTimeInfoOfAPatientArr(p_objPatient.m_StrInPatientID,p_dtmInPatientDate.ToString(),out dtmDate);
                if (p_objPrintRec != null)
                {
                    //	infPrintRecord objPrintRec=new clsIntensiveTendMainPrintTool();
                    m_objSetValue.m_objPrintInfo_GeneralTendRecordArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_GeneralTendRecordArr == null || m_objSetValue.m_objPrintInfo_GeneralTendRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�۲���Ŀ��¼	����һ����Ժֻ�и�����OpenDate��㴫һ��
        protected long lngLoadWatchItemRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                //infPrintRecord objPrintRec=new clsIntensiveTendMainPrintTool();
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_WatchItemRecordArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_WatchItemRecordArr == null ||
                    m_objSetValue.m_objPrintInfo_WatchItemRecordArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //Σ�ػ��߻����¼
        protected long lngLoadIntensiveTendMain(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //clsRecordsDomain objRecordsDomain=clsRecordsDomainFactory.s_objGetRecordsDomain(enmRecordsType.IntensiveTend);
                //string[] strDate=null; //objRecordsDomain..m_strGetAllTendRecordCreateDateArr(p_objPatient.m_StrInPatientID,p_dtmInPatientDate.ToString());
                //����һ����Ժֻ�и�����OpenDate��㴫һ��
                if (p_objPrintRec != null)
                {

                    //infPrintRecord p_objPrintRec=new clsIntensiveTendMainPrintTool();
                    m_objSetValue.m_objPrintInfo_IntensiveTendMainArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_IntensiveTendMainArr == null || m_objSetValue.m_objPrintInfo_IntensiveTendMainArr[0] == null)
                    p_objPrintRec = null;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;

        }
        //ICUΣ�ز��˻����¼,Alex˵�Ȳ�����

        //���������¼�ۻ�ʿ��
        protected long lngLoadOperationRecordNurse(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new clsOperationRecordDomain().m_dtmGetTimeInfoOfAPatientArr(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString());
                if (dtmDate != null && p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_OperationRecordNurseArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_OperationRecordNurseArr == null ||
                    m_objSetValue.m_objPrintInfo_OperationRecordNurseArr[0] == null) p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;

        }
        //������е�����ϵ�����
        protected long lngLoadOperationEquipmentQty(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate, dtmTempDate;
                lngRes = new clsOperationEqipmentQtyDomain().m_lngGetTimeInfoOfAPatientArr(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString(), out dtmDate, out dtmTempDate);
                if (dtmDate != null && p_objPrintRec != null)
                {
                    m_objSetValue.m_objPrintInfo_OperationEquipmentQtyArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_OperationEquipmentQtyArr == null ||
                    m_objSetValue.m_objPrintInfo_OperationEquipmentQtyArr[0] == null) p_objPrintRec = null;
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;

        }
        //Σ��֢�໤�����ػ���¼��
        protected long lngLoadICUIntensiveTend(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr =
                        m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);
                }
                if (m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr == null || m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�����¼�
        //һ�㻼�߻����¼
        protected long lngLoadGeneralNurseRecord_GX(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_GeneralNurseRecord_GXArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_GeneralNurseRecord_GXArr == null || m_objSetValue.m_objPrintInfo_GeneralNurseRecord_GXArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����ICU���������Ƽ໤��¼��
        protected long lngLoadMainICUBreath(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_MainICUBreathArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_MainICUBreathArr == null || m_objSetValue.m_objPrintInfo_MainICUBreathArr[0] == null)
                    p_objPrintRec = null;


            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����΢��Ѫ�Ǽ���¼��
        protected long lngLoadMiniBooldSugarChk(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_MiniBooldSugarChkArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_MiniBooldSugarChkArr == null || m_objSetValue.m_objPrintInfo_MiniBooldSugarChkArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //ICU�����¼
        protected long lngLoadICUNurseRecord(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_ICUNurseRecordArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_ICUNurseRecordArr == null || m_objSetValue.m_objPrintInfo_ICUNurseRecordArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //���ICU�໤��¼
        protected long lngLoadSurgeryICUWardship(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_SurgeryICUWardshipArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_SurgeryICUWardshipArr == null || m_objSetValue.m_objPrintInfo_SurgeryICUWardshipArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //��Ѫ������ػ���¼
        protected long lngLoadCardiovascularTend(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;

            try
            {
                DateTime[] dtmDateArr;
                clsCardiovascularBaseInfo_GX[] m_objCarBaseInfo_GXArr;

                //clsCardiovascularTend_GXService m_objService =
                //    (clsCardiovascularTend_GXService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCardiovascularTend_GXService));

                (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetBaseInfo(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out m_objCarBaseInfo_GXArr);
                if (m_objCarBaseInfo_GXArr != null)
                {
                    dtmDateArr = new DateTime[m_objCarBaseInfo_GXArr.Length];

                    for (int i = 0; i < m_objCarBaseInfo_GXArr.Length; i++)
                    {
                        dtmDateArr[i] = m_objCarBaseInfo_GXArr[i].m_dtmOpenDate;
                    }

                    if (p_objPatient != null && dtmDateArr != null)
                    {
                        m_objSetValue.m_objPrintInfo_CardiovascularTendArr =
                                m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDateArr);

                    }
                    if (m_objSetValue.m_objPrintInfo_CardiovascularTendArr == null || m_objSetValue.m_objPrintInfo_CardiovascularTendArr[0] == null)
                        p_objPrintRec = null;
                }
                else
                    p_objPrintRec = null;


            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //����������ҩ�۲��¼��
        protected long lngLoadVeinSpecialUseDrug(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            ((clsVeinSpecialUseDrugPrintTool)p_objPrintRec).isOutPrint = true;
            try
            {
                DateTime[] dtmCheckDateArr;

                //clsVeinSpecialUseDrug_MainService objVSUDServ =
                //    (clsVeinSpecialUseDrug_MainService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsVeinSpecialUseDrug_MainService));

                if (p_objPatient != null)
                {
                    lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetCheckDate(p_objPatient.m_StrInPatientID, p_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out dtmCheckDateArr);
                    if (dtmCheckDateArr != null)
                        m_objSetValue.m_objPrintInfo_VeinSpecialUseDrugArr =
                                m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmCheckDateArr);
                }
                if (m_objSetValue.m_objPrintInfo_VeinSpecialUseDrugArr == null)// || m_objSetValue.m_objPrintInfo_VeinSpecialUseDrugArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        //�������ǼǱ�
        protected long lngLoadRegisterQuantity(ref infPrintRecord p_objPrintRec, clsPatient p_objPatient, DateTime p_dtmInPatientDate)
        {
            long lngRes = 1;
            try
            {
                DateTime[] dtmDate = new DateTime[1];
                dtmDate[0] = new DateTime(0);
                //����һ����Ժֻ�и�����OpenDate��㴫һ��

                if (p_objPatient != null)
                {
                    m_objSetValue.m_objPrintInfo_RegisterQuantityArr =
                            m_objGetContentArr(p_objPrintRec, p_objPatient, p_dtmInPatientDate, dtmDate);

                }
                if (m_objSetValue.m_objPrintInfo_RegisterQuantityArr == null || m_objSetValue.m_objPrintInfo_RegisterQuantityArr[0] == null)
                    p_objPrintRec = null;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
            return lngRes;
        }
        #endregion

        private void frmInPatientCaseHistory_SetForm_Load(object sender, System.EventArgs e)
        {
            //			#region ��ʼ����
            //			TreeNode trnNode = new TreeNode("��Ժ��¼");
            //			trnNode.Tag ="0";
            //			this.m_trvTime.Nodes.Add(trnNode);
            //			#endregion
            //
            //			m_cmdPreview.Enabled=false;
            //			m_cmdSave.Enabled=false;
            //
            //			m_objSetValue=new clsInPatientCaseHistory_SetValue();
            //
            #region ���ô�ӡ�¼�
            m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
            m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
            m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
            m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);
            #endregion
        }
        #region ���ؼ�
        private void m_mthLoadTree()
        {
            m_trvTime.Nodes[0].Nodes.Clear();
            for (int i = m_objSetInPatient.m_ObjInBedInfo.m_intGetSessionCount() - 1; i >= 0; i--)
            {

                TreeNode trnRecordDate = new TreeNode(m_objSetInPatient.m_ObjInBedInfo.m_objGetSessionByIndex(i).m_DtmHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss"));
                trnRecordDate.Tag = m_objSetInPatient.m_ObjInBedInfo.m_objGetSessionByIndex(i).m_DtmEMRInDate;
                //				if(strOpenTimeListArr!=null)
                //				{
                //					trnRecordDate.Tag =(string)strOpenTimeListArr[i];
                //				}
                m_trvTime.Nodes[0].Nodes.Add(trnRecordDate);

            }
            m_trvTime.SelectedNode = null;
            m_trvTime.ExpandAll();

        }



        private void m_trvTime_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (m_trvTime.SelectedNode == null || m_trvTime.SelectedNode.Tag == null || this.m_trvTime.SelectedNode.Tag.ToString() == "0")
            {

                m_cmdPreview.Enabled = false;
                m_cmdSave.Enabled = false;
                m_chkNurser.Enabled = false;
                m_chkDoctor.Enabled = false;
                m_chkNurser.Checked = false;
                m_chkDoctor.Checked = false;

                return;
            }

            m_cmdPreview.Enabled = true;
            m_cmdSave.Enabled = true;
            m_chkDoctor.Enabled = true;
            m_chkNurser.Enabled = true;
            m_chkDoctor.Checked = true;
            m_chkNurser.Checked = true;
            m_dtmSetInPatientDate = (DateTime)m_trvTime.SelectedNode.Tag;
            txtInPatientID.Text = m_objSetInPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvTime.Nodes[0].Nodes.Count - m_trvTime.SelectedNode.Index - 1).m_StrHISInPatientID;
            m_objSetInPatient.m_StrHISInPatientID = txtInPatientID.Text;
            m_objSetInPatient.m_DtmSelectedHISInDate = DateTime.Parse(m_trvTime.SelectedNode.Text);
            MDIParent.s_ObjSaveCue.m_mthRemoveForm(this);
            //lngLoadBUltrasonicCheckOrder(m_objSetInPatient,m_dtmSetInPatientDate);
            //lngLoadIntensiveTendMain(m_objSetInPatient,m_dtmSetInPatientDate);
        }
        #endregion
        #region ��ӡ���Ƽ�����
        private void m_mthPrint()
        {
            int intTemp = -1;
            try
            {
                this.Cursor = this.Cursor = Cursors.WaitCursor;
                m_objSetValue = new clsInPatientCaseHistory_SetValue();
                m_objPrintRecArr = new infPrintRecord[MAXFORMNUM];
                if (m_hasItemsArr == null)
                    m_hasItemsArr = new Hashtable[2];
                m_intCurrentForm = 0;
                m_intCurrentCopies = 0;
                m_blnNewPage = true;
                m_mthSetPrintToolsContent();
                this.Cursor = Cursors.Default;
                if (!m_blnHasMoreForm(intTemp)) return;

                PrintController pcl = m_pdcPrintDocument.PrintController;
                float scale = Convert.ToSingle(100) / 100f;
                long quality = Convert.ToInt64(100);
                string output = @"c:\";
                clsPreviewPrintControllerFile controller = new clsPreviewPrintControllerFile(ImageFormat.Jpeg, scale, quality, output, false);
                if (blnIsPreview)
                {//Ԥ��
                    m_pdcPrintDocument.PrintController = new PrintControllerWithStatusDialog((PrintController)controller, "Ԥ��...");
                    m_pdcPrintDocument.Print();

                    arlTemp = controller.m_arlImage;
                    intPages = 0;
                    PrintTool.frmPrintPreviewDialogAll frmDlg = new iCare.PrintTool.frmPrintPreviewDialogAll(arlTemp, false);
                    frmDlg.BringToFront();
                    //				frmDlg.TopLevel=true;
                    frmDlg.ShowDialog();
                }
                else//ֱ�Ӵ�ӡ
                {
                    m_pdcPrintDocument.PrintController = m_pdcPrintDocumentTemp.PrintController;
                    m_pdcPrintDocument.Print();
                }



                //				PrintPreviewDialog dlg = new PrintPreviewDialog();
                //				dlg.Document = m_pdcPrintDocument;
                //				dlg.ShowDialog();
                //				ppdPrintPreview.Document = m_pdcPrintDocument;
                //				ppdPrintPreview.ShowDialog();
                //				frmPreview.ShowDialog();
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message + ex.TargetSite.ToString());
                this.Cursor = Cursors.Default;
            }
        }

        private PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();
        //		private iCare.frmPrintPreviewDialogPF ppdPrintPreview=new frmPrintPreviewDialogPF();
        private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            m_mthSubPrintPage(e);

            //			if(ppdPrintPreview != null)
            //��ʱ����
            //				while(!frmPreview.m_blnHandlePrint(e))
            //					m_mthSubPrintPage(e);
        }
        private void m_mthSubPrintPage(System.Drawing.Printing.PrintPageEventArgs e)
        {

            switch (m_intCurrentForm)
            {
                #region �ɵĴ�ӡ˳��
                //				case 0: //���һ�ֱ���������
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr,e);
                //					}
                //					else
                //					{
                //						//m_objPrintRecArr[m_intCurrentForm]=null;//��ֹendpringpageʱ���ֿ�����
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);//��ֹ��ӡ��ҳ
                //					}
                //					
                //					break;
                //				case 1: //���һ�ֱ���������
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr,e);
                //					}
                //					else
                //					{
                //						//m_objPrintRecArr[m_intCurrentForm]=null;//��ֹendpringpageʱ���ֿ�����
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);//��ֹ��ӡ��ҳ
                //					}
                //					
                //					break;
                //
                //				case 2: //���һ�ֱ���������
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_ConsultationArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_ConsultationArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_ConsultationArr,e);
                //					}
                //					else
                //					{
                //						//m_objPrintRecArr[m_intCurrentForm]=null;//��ֹendpringpageʱ���ֿ�����
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);//��ֹ��ӡ��ҳ
                //					}
                //					
                //					break;
                //
                //				case 3: //���һ�ֱ���������
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null /*&& m_objSetValue.m_objPrintInfo_OperationAgreedArr!=null*/)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_OperationAgreedArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_OperationAgreedArr,e);
                //					}
                //					else
                //					{
                //						//m_objPrintRecArr[m_intCurrentForm]=null;//��ֹendpringpageʱ���ֿ�����
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);//��ֹ��ӡ��ҳ
                //					}
                //					
                //					break;
                //
                //
                //
                //				case 4: 
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					break;
                //				
                //				case 5: 
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					break;
                //
                //				case 6:
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_PICUShiftInArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_PICUShiftInArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_PICUShiftInArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					break;
                //
                //				case 7:
                //					
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_PICUShiftOutArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_PICUShiftOutArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_PICUShiftOutArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					break;
                //
                //				case 8:
                //					
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_OutHospitalArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_OutHospitalArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_OutHospitalArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					break;
                //
                //				case 9:
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					break;
                //
                //				case 10:
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_InPatientEvaluateArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_InPatientEvaluateArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_InPatientEvaluateArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					break;
                //
                //				case 11:
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					break;
                //
                //				case 12: 
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_GeneralTendRecordArr!=null)
                //					{
                //						
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_GeneralTendRecordArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_GeneralTendRecordArr,e);
                //					}
                //					else
                //					{
                //						//m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					break;
                //				case 13: 
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_WatchItemRecordArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_WatchItemRecordArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_WatchItemRecordArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					break;
                //				case 14:	
                //					
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_IntensiveTendMainArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_IntensiveTendMainArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_IntensiveTendMainArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					
                //					break;
                //
                //				case 15:	
                //					
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					
                //					break;
                //
                //				case 16:	
                //					
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_OperationRecordNurseArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_OperationRecordNurseArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_OperationRecordNurseArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					
                //					break;
                //
                //				case 17:	
                //					e.HasMorePages=false;	
                //					if(m_objPrintRecArr[m_intCurrentForm]!=null && m_objSetValue.m_objPrintInfo_OperationEquipmentQtyArr!=null)
                //					{
                //						if(m_intCurrentCopies==0 && m_blnNewPage) m_intCurrentCopies=m_objSetValue.m_objPrintInfo_OperationEquipmentQtyArr.Length;
                //						m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_OperationEquipmentQtyArr,e);
                //					}
                //					else
                //					{
                //						//	m_objPrintRecArr[m_intCurrentForm]=null;
                //						m_intCurrentForm++;
                //						m_mthSubPrintPage(e);
                //					}
                //					//e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                //					
                //					break;
                #endregion
                #region
                /*
                case 0: //������ҳ
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 1: //��Ժ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_OutHospitalArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_OutHospitalArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_OutHospitalArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 2: //סԺ־
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr, e);
                    }
                    else
                    {
                        //m_objPrintRecArr[m_intCurrentForm]=null;//��ֹendpringpageʱ���ֿ�����
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);//��ֹ��ӡ��ҳ
                    }
                    break;
                case 3: //��ͨסԺ����(���)
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr, e);
                    }
                    else
                    {
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 4: //���̼�¼				
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr, e);
                    }
                    else
                    {
                        //m_objPrintRecArr[m_intCurrentForm]=null;//��ֹendpringpageʱ���ֿ�����
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);//��ֹ��ӡ��ҳ
                    }
                    break;
                    */
#endregion
                #region ר�Ʋ�����
                    /*
                case 5://���ﲡ��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_EmergencyCallArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[0];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_EmergencyCallArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_EmergencyCallArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;

                case 6://ѪҺר��סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_BloodAcadInHospitalCaseHistoryOneArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[1];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_BloodAcadInHospitalCaseHistoryOneArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_BloodAcadInHospitalCaseHistoryOneArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;

                case 7://����������ʱ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ChildbearingRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[2];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ChildbearingRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ChildbearingRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 8://����סԺ����(��һ)
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_GynecologyCaseHisArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[3];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_GynecologyCaseHisArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_GynecologyCaseHisArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 9://����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_GynecologyArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[4];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_GynecologyArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_GynecologyArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                
                case 10://�������סԺ����(���ʽ)
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_BurnSuergeryArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[5];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_BurnSuergeryArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_BurnSuergeryArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 11://��Ѫ�����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_CardiovascularArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[6];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_CardiovascularArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_CardiovascularArr, e);
                    }
                    else
                    {
                        //m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 12://��������������֪��ͬ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_CataractSuffererApproveArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[7];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_CataractSuffererApproveArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_CataractSuffererApproveArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 13://�ʹ���������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[8];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr, e);

                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 14://�����סԺ����(ʳ������������)
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[9];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 15://�����סԺ����(�ط��ݸ���)
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[10];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 16://�����סԺ����(���ٲ�)
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIIArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[11];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIIArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ChestSurgery_IIIArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 17://�����¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_childbirthArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[12];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_childbirthArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_childbirthArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 18://������ת�Ƽ�¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ChildbirthTransitSectionArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[13];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ChildbirthTransitSectionArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ChildbirthTransitSectionArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 19://����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_DiabetesHospitalArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[14];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_DiabetesHospitalArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_DiabetesHospitalArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 20://�������۲���
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_EmergencyCallWoundArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[15];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_EmergencyCallWoundArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_EmergencyCallWoundArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 21://��ǯ������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[16];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 22://��������������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[17];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 23://�����������������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_2Arr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[18];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_2Arr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_2Arr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 24://��ѹ����ǯ����������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_3Arr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[19];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_3Arr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_3Arr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 25://�����ڿ�סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_BreathArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[20];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_BreathArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_BreathArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 26://���ಡסԺ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_HeartHospitalRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[21];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_HeartHospitalRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_HeartHospitalRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 27://����ҽѧ����Ժ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_HerbalismArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[22];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_HerbalismArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_HerbalismArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 28://����ҽ��Ͽ�
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_Herbalist_WesternArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[23];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_Herbalist_WesternArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_Herbalist_WesternArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 29://ҩ��������¼��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_MedcineMiscarryRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[24];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_MedcineMiscarryRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_MedcineMiscarryRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 30://������סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_NeonatalArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[25];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_NeonatalArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_NeonatalArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 31://���ڿ�סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_NeuromedicineArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[26];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_NeuromedicineArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_NeuromedicineArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 32://�����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_NeurosurgeryArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[27];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_NeurosurgeryArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_NeurosurgeryArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 33://��������Ժ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_NewChildArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[28];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_NewChildArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_NewChildArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 34://��Ѫ�ܲ�����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_CerebrovascularArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[29];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_CerebrovascularArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_CerebrovascularArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 35://������Ժ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_Obstetric_CriterionArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[30];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_Obstetric_CriterionArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_Obstetric_CriterionArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                
                case 36://�ۿ�סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_OphthalmologyArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[31];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_OphthalmologyArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_OphthalmologyArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 37://�ǿ�(��������΢�������)����֪��ͬ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_OrthopaedicsSuffererAprroveArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[32];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_OrthopaedicsSuffererAprroveArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_OrthopaedicsSuffererAprroveArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 38://����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_PaediatricsArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[33];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_PaediatricsArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_PaediatricsArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 39://������Ժ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_Paediatrics01Arr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[34];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_Paediatrics01Arr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_Paediatrics01Arr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 40://���ڽ�������������¼��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_PalaceBirthControlLaySkillArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[35];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_PalaceBirthControlLaySkillArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_PalaceBirthControlLaySkillArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 41://��ǰ������ӵ�
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_PrePostOperateSeeArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[36];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_PrePostOperateSeeArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_PrePostOperateSeeArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 42://��ʪ�����ڿ�סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_RheumatismImmunityArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[37];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_RheumatismImmunityArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_RheumatismImmunityArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 43://θ�����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_StoIntesChirurgerylArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[38];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_StoIntesChirurgerylArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_StoIntesChirurgerylArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;

                case 44://���ڽ�����ȡ������¼��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_WombBirthControlRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[39];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_WombBirthControlRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_WombBirthControlRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 45://���ڿ�סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_KidneyMedicineBeInHospitalArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[40];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_KidneyMedicineBeInHospitalArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_KidneyMedicineBeInHospitalArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 46://�˹��������������ѹܼ�¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ManpowerAbortionArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[41];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ManpowerAbortionArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ManpowerAbortionArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 47://����סԺ����(��һ)
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ObstetricCaseHisArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[42];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ObstetricCaseHisArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ObstetricCaseHisArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 48://����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ObstetricArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[43];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ObstetricArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ObstetricArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 49://����������
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_NerveSystemArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[44];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_NerveSystemArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_NerveSystemArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 50://���ʰ�����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_NasopharyngelCarcinomaArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[45];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_NasopharyngelCarcinomaArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_NasopharyngelCarcinomaArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 51://���Ǻ�Ʋ���
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_IllnessHistoryRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[46];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_IllnessHistoryRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_IllnessHistoryRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 52://����̥ͷ����������������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_EMR_PullDeliverRecordArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_EMR_PullDeliverRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_EMR_PullDeliverRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                #endregion

                case 53: //��ǰС��	
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr, e);
                    }
                    else
                    {
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;

                case 54: //������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    //e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                    break;
                case 55://�����¼					
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ConsultationArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ConsultationArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ConsultationArr, e);
                    }
                    else
                    {
                        //m_objPrintRecArr[m_intCurrentForm]=null;//��ֹendpringpageʱ���ֿ�����
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);//��ֹ��ӡ��ҳ
                    }
                    break;
                case 56://24Сʱ�����Ժ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_EMR_OutHospitalIn24HoursArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_EMR_OutHospitalIn24HoursArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_EMR_OutHospitalIn24HoursArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 57:  //��Ժ24Сʱ��������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_DeathRecordIn24HoursArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_DeathRecordIn24HoursArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_DeathRecordIn24HoursArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 58:  //������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_DeathrecordArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_DeathrecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_DeathrecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 59://�����������ۼ�¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_DeathDiscussArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_DeathDiscussArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_DeathDiscussArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;

                case 60://����ժҪ
                    try
                    {
                        if (m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr != null)
                        {
                            frmCaseHistorySummary frmCHS = new frmCaseHistorySummary();
                            frmCHS.blnIsOutPrint = true;
                            frmCHS.objPatient = (clsPatient)m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr[1];
                            frmCHS.objRecordContent = (clsCaseHistorySummary)m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr[0];
                            frmCHS.m_mthPrintPage(e);
                            e.HasMorePages = m_blnHasMoreForm(m_intCurrentForm);
                            if (e.HasMorePages)
                                m_intCurrentForm++;
                        }
                        else
                        {
                            m_intCurrentForm++;
                            m_mthSubPrintPage(e);
                        }
                    }
                    catch (Exception ex)
                    {
                        string strMeg = ex.ToString();
                    }
                    break;

                case 61://���������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_OperationRecordNurseArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_OperationRecordNurseArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_OperationRecordNurseArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 62://һ�㻤���¼					
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_GeneralTendRecordArr != null)
                    {

                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_GeneralTendRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_GeneralTendRecordArr, e);
                    }
                    else
                    {
                        //m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 63://Σ�ػ��߻����¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IntensiveTendMainArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IntensiveTendMainArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IntensiveTendMainArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;

                case 64://Σ��֢�໤�����ػ���¼��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    //e.HasMorePages=m_blnHasMoreForm( m_intCurrentForm);
                    break;

                case 65://���µ�
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;

                case 66: //һ�㻼�߻����¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_GeneralNurseRecord_GXArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_GeneralNurseRecord_GXArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_GeneralNurseRecord_GXArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 67://����ICU���������Ƽ໤��¼��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_MainICUBreathArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_MainICUBreathArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_MainICUBreathArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 68://����΢��Ѫ�Ǽ���¼��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_MiniBooldSugarChkArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_MiniBooldSugarChkArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_MiniBooldSugarChkArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 69://ICU�����¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ICUNurseRecordArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ICUNurseRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ICUNurseRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 70: //���ICU�໤��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_SurgeryICUWardshipArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_SurgeryICUWardshipArr.Length;
                        ((clsSurgeryICUWardship_PrintTool)m_objPrintRecArr[m_intCurrentForm]).printView = true;
                        ((clsSurgeryICUWardship_PrintTool)m_objPrintRecArr[m_intCurrentForm]).pageCount = 2;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_SurgeryICUWardshipArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 71://��Ѫ������ػ���¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_CardiovascularTendArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_CardiovascularTendArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_CardiovascularTendArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 72: //����������ҩ�۲��¼��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_VeinSpecialUseDrugArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_VeinSpecialUseDrugArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_VeinSpecialUseDrugArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                
                case 73: //�������ǼǱ�
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_RegisterQuantityArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_RegisterQuantityArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_RegisterQuantityArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                default: break;
                     */
                #endregion
                case 0: //������ҳ
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 1: //��Ժ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_OutHospitalArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_OutHospitalArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_OutHospitalArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 2: //��ͨסԺ����(���)
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm],m_objSetValue.m_objPrintInfo_InPatientCaseHistory_F2Arr, e);
                    }
                    else
                    {
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }

                    break;
                case 3://����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_GynecologyArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[0];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_GynecologyArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_GynecologyArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 4://����סԺ����
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ObstetricArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[1];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ObstetricArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ObstetricArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 5: //���̼�¼				
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr, e);
                    }
                    else
                    {
                        //m_objPrintRecArr[m_intCurrentForm]=null;//��ֹendpringpageʱ���ֿ�����
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);//��ֹ��ӡ��ҳ
                    }
                    break;
                case 6://�ʹ���������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[2];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_CesareanRecordArr, e);

                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 7: //��Ժ������
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_InPatientEvaluateArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_InPatientEvaluateArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_InPatientEvaluateArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 8://һ�㻤���¼					
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_GeneralTendRecordArr != null)
                    {

                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_GeneralTendRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_GeneralTendRecordArr, e);
                    }
                    else
                    {
                        //m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 9://Σ�ػ��߻����¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IntensiveTendMainArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IntensiveTendMainArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IntensiveTendMainArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 10://����������ʱ��¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ChildbearingRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[3];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ChildbearingRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ChildbearingRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 11://����������ȡ�����¾��󸹲����ѹܽ�����¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ManpowerAbortionRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[4];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ManpowerAbortionRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ManpowerAbortionRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 12://������¼��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_InducedLaborRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[5];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_InducedLaborRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_InducedLaborRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 13://��ǯ������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[6];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_ForpecsRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                
                case 14://��������������¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr != null)
                    {
                        clsInpatMedRecPrintBase.m_hasItems = m_hasItemsArr[7];
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_IMR_GestationMisbirth_1Arr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 15: //��ǰС��	
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr, e);
                    }
                    else
                    {
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                
                
                
                //case 15://�����
                //    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr != null)
                //    {
                //        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr.Length;
                //        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr, e);
                //    }
                //    else
                //    {
                //        //	m_objPrintRecArr[m_intCurrentForm]=null;
                //        m_intCurrentForm++;
                //        m_mthSubPrintPage(e);
                //    }
                //    break;
                case 16://��Ӥ��Ӥ��������
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_AYQBabyAssessmentRecordArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_AYQBabyAssessmentRecordArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_AYQBabyAssessmentRecordArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                //case 17://���̼�¼
                //    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr != null)
                //    {
                //        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr.Length;
                //        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr, e);
                //    }
                //    else
                //    {
                //        //	m_objPrintRecArr[m_intCurrentForm]=null;
                //        m_intCurrentForm++;
                //        m_mthSubPrintPage(e);
                //    }
                //    break;
                case 17://�߲��ؾ�����ι۲��
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_OXTIntravenousDripArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_OXTIntravenousDripArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_OXTIntravenousDripArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                case 18://�����¼
                    if (m_objPrintRecArr[m_intCurrentForm] != null && m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr != null)
                    {
                        if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentCopies = m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr.Length;
                        m_mthSubPrintEachPage(m_objPrintRecArr[m_intCurrentForm], m_objSetValue.m_objPrintInfo_WaitLayRecord_AcadArr, e);
                    }
                    else
                    {
                        //	m_objPrintRecArr[m_intCurrentForm]=null;
                        m_intCurrentForm++;
                        m_mthSubPrintPage(e);
                    }
                    break;
                
                
                default:
                    break;
            }
        }
        private void m_mthSubPrintEachPage(infPrintRecord p_objPrintRec, object[] p_objArr, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                if (m_blnNewPage)
                {
                    if (m_intCurrentCopies == 0)
                    {
                        m_intCurrentForm++;
                        return;
                    }
                    p_objPrintRec.m_mthSetPrintContent(p_objArr[p_objArr.Length - m_intCurrentCopies]);
                    m_intCurrentCopies--;
                }
                p_objPrintRec.m_mthPrintPage(e);
                m_blnNewPage = !e.HasMorePages;
                e.HasMorePages = m_blnHasMoreForm(m_intCurrentForm) || m_intCurrentCopies > 0 || e.HasMorePages;
                if (m_intCurrentCopies == 0 && m_blnNewPage) m_intCurrentForm++;

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }

        }

        /// <summary>
        /// �жϵ��굱ǰ����Ƿ��б�Ҫ��
        /// </summary>
        /// <param name="p_intCurrentForm">���뵱ǰ��,����������зǿյı�,-1��ʾ��û��ʼ��</param>
        /// <returns></returns>
        private bool m_blnHasMoreForm(int p_intCurrentForm)
        {
            for (int i = p_intCurrentForm + 1; i < MAXFORMNUM; i++)
            {
                if (m_objPrintRecArr[i] != null)
                {
                    //p_intCurrentForm=i;
                    return true;
                }
            }
            p_intCurrentForm = MAXFORMNUM;//�Ҳ������Լ�����ı��Ͱ�����Ϊ��ֵ����ʾ�ޱ�ɴ�
            return false;
        }

        private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            m_mthSubBeginPrint(e);
        }
        private void m_mthSubBeginPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            //��Ҫ�жϽӿڵ��������m_objPatient�Ƿ�Ϊ��
            for (int i = 0; i < MAXFORMNUM; i++)
            {
                if (m_objPrintRecArr[i] != null)
                    m_objPrintRecArr[i].m_mthBeginPrint(e);
            }
            m_intCurrentForm = 0;
            m_intCurrentCopies = 0;
            m_blnNewPage = true;
        }

        private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //	objPrintTool.m_mthEndPrint(e);
            m_mthSubEndPrint(e);
        }

        private void m_mthSubEndPrint(System.Drawing.Printing.PrintEventArgs e)
        {

            for (int j = 0; j < m_hasItemsArr.Length; j++)
            {
                if (m_hasItemsArr[j] != null)
                {
                    m_hasItemsArr[j].Clear();
                }

            }
            for (int i = 0; i < MAXFORMNUM; i++)
            {
                if (m_objPrintRecArr[i] != null)
                    m_objPrintRecArr[i].m_mthEndPrint(e);
            }
            //			PreviewPageInfo[] ppia = GetPreviewPageInfo();
        }

        //ÿ����Ҫ��ӡ�򱣴�ǰ������ô˺����Դ����ݿ��ж�ȡ��Ӧ����
        protected virtual void m_mthSetPrintToolsContent()
        {
            #region �ɵ�˳��
            //			if(m_objPrintRecArr[0]==null) m_objPrintRecArr[0]=new clsInPatientCaseHistoryPrintTool();
            //			lngLoadInPatientCaseHistory(ref m_objPrintRecArr[0],m_objSetInPatient,m_dtmSetInPatientDate);
            //
            //			if(m_objPrintRecArr[1]==null) m_objPrintRecArr[1]=new  clsSubDiseaseTrackPrintTool();
            //			lngLoadSubDiseaseTrack(ref m_objPrintRecArr[1],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[2]==null) m_objPrintRecArr[2]=new clsConsultationPrintTool();
            //			lngLoadConsultation(ref m_objPrintRecArr[2],m_objSetInPatient,m_dtmSetInPatientDate);
            //
            //
            //			m_objPrintRecArr[3]=null;
            //			//			if(m_objPrintRecArr[3]==null) m_objPrintRecArr[3]=new  clsOperationAgreedRecordPrintTool();
            //			//			lngLoadOperationAgreed(ref m_objPrintRecArr[3],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[4]==null) m_objPrintRecArr[4]=new  clsBeforeOperationSummaryPrintTool();
            //			lngLoadBeforeOperationSummary(ref m_objPrintRecArr[4],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[5]==null) m_objPrintRecArr[5]=new  clsOperationRecordDoctorPrintTool();
            //			lngLoadOperationRecordDoctor(ref m_objPrintRecArr[5],m_objSetInPatient,m_dtmSetInPatientDate);
            //			//			
            //			if(m_objPrintRecArr[6]==null) m_objPrintRecArr[6]=new  clsPICUShiftBasePrintTool(true);
            //			lngLoadICUShiftIn(ref m_objPrintRecArr[6],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[7]==null) m_objPrintRecArr[7]=new  clsPICUShiftBasePrintTool(false);
            //			lngLoadICUShiftOut(ref m_objPrintRecArr[7],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[8]==null) m_objPrintRecArr[8]=new clsOutHospitalPrintTool();
            //			lngLoadOutHospital(ref m_objPrintRecArr[8],m_objSetInPatient,m_dtmSetInPatientDate);
            //
            //			if(m_objPrintRecArr[9]==null) m_objPrintRecArr[9]=new  clsInHospitalMainRecordPrintTool(true);
            //			lngLoadInHospitalMainRecord(ref m_objPrintRecArr[9],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[10]==null) m_objPrintRecArr[10]=new  clsInPatientEvaluatePrintTool();
            //			lngLoadInPatientEvaluate(ref m_objPrintRecArr[10],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[11]==null) m_objPrintRecArr[11]=new  clsThreeMeasureRecordPrintTool_New();
            //			lngLoadThreeMeasureRecord(ref m_objPrintRecArr[11],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[12]==null) m_objPrintRecArr[12]=new  clsGeneralNurseRecordPrintTool();
            //			lngLoadGeneralTendRecord(ref m_objPrintRecArr[12],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[13]==null) m_objPrintRecArr[13]=new  clsWatchItemTrackPrintTool();
            //			lngLoadWatchItemRecord(ref m_objPrintRecArr[13],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[14]==null) m_objPrintRecArr[14]=new  clsIntensiveTendMainPrintTool();
            //			lngLoadIntensiveTendMain(ref m_objPrintRecArr[14],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			m_objPrintRecArr[15]=null;
            //			//			if(m_objPrintRecArr[15]==null) m_objPrintRecArr[15]=new  clsICUIntensiveTendPrintTool();
            //			//			lngLoadICUIntensiveTend(ref m_objPrintRecArr[15],m_objSetInPatient,m_dtmSetInPatientDate);
            //
            //			if(m_objPrintRecArr[16]==null) m_objPrintRecArr[16]=new  clsOperationRecordPrintTool();
            //			lngLoadOperationRecordNurse(ref m_objPrintRecArr[16],m_objSetInPatient,m_dtmSetInPatientDate);
            //			
            //			if(m_objPrintRecArr[17]==null) m_objPrintRecArr[17]=new  clsOperationEquipmentQtyPrintTool();
            //			lngLoadOperationEquipmentQty(ref m_objPrintRecArr[17],m_objSetInPatient,m_dtmSetInPatientDate);
            #endregion
            #region
            /*
            #region ҽ������վ����

            if (m_chkDoctor.Checked)
            {
                //������ҳ
                if (m_objPrintRecArr[0] == null)
                    m_objPrintRecArr[0] = new clsInHospitalMainRecordPrintTool(true);
                lngLoadInHospitalMainRecord(ref m_objPrintRecArr[0], m_objSetInPatient, m_dtmSetInPatientDate);

                //��Ժ
                if (m_objPrintRecArr[1] == null)
                    m_objPrintRecArr[1] = new clsOutHospitalPrintTool();
                lngLoadOutHospital(ref m_objPrintRecArr[1], m_objSetInPatient, m_dtmSetInPatientDate);

                //��ͨסԺ����
                if (m_objPrintRecArr[2] == null)
                    m_objPrintRecArr[2] = new clsInPatientCaseHistoryPrintTool();
                lngLoadInPatientCaseHistory(ref m_objPrintRecArr[2], m_objSetInPatient, m_dtmSetInPatientDate);
                //��ͨסԺ����(���)
                if (m_objPrintRecArr[3] == null)
                    m_objPrintRecArr[3] = new clsInPatientCaseHistory_F2PrintTool();
                lngLoadInPatientCaseHistory_F2(ref m_objPrintRecArr[3], m_objSetInPatient, m_dtmSetInPatientDate);

                //���̼�¼
                if (m_objPrintRecArr[4] == null)
                    m_objPrintRecArr[4] = new clsSubDiseaseTrackPrintTool();
                lngLoadSubDiseaseTrack(ref m_objPrintRecArr[4], m_objSetInPatient, m_dtmSetInPatientDate);

                #region ר�Ʋ�����
                //���ﲡ��
                string p_strTypeID = "";
                if (m_objPrintRecArr[5] == null)
                {
                    p_strTypeID = "frm_EmergencyCall";
                    m_objPrintRecArr[5] = new cls_EmergencyCallPrintTool(p_strTypeID);
                }
                lngLoadEmergencyCall(ref m_objPrintRecArr[5], m_objSetInPatient, m_dtmSetInPatientDate);
                //ѪҺר��סԺ����
                if (m_objPrintRecArr[6] == null)
                {
                    p_strTypeID = "frmBloodAcadInHospitalCaseHistoryOne";
                    m_objPrintRecArr[6] = new clsBloodAcadInHospitalCaseHistoryOnePrintTool(p_strTypeID);
                }
                lngLoadBloodAcadInHospitalCaseHistoryOne(ref m_objPrintRecArr[6], m_objSetInPatient, m_dtmSetInPatientDate);
                //����������ʱ��¼
                if (m_objPrintRecArr[7] == null)
                {
                    p_strTypeID = "frmChildbearingRecord";
                    m_objPrintRecArr[7] = new clsChildbearingRecordPrintTool(p_strTypeID);
                }
                lngLoadChildbearingRecord(ref m_objPrintRecArr[7], m_objSetInPatient, m_dtmSetInPatientDate);
                //����סԺ����(��һ)
                if (m_objPrintRecArr[8] == null)
                {
                    p_strTypeID = "frmGynecologyCaseHis";
                    m_objPrintRecArr[8] = new clsGynecologyCaseHisPrintTool(p_strTypeID);
                }
                lngLoadGynecologyCaseHis(ref m_objPrintRecArr[8], m_objSetInPatient, m_dtmSetInPatientDate);
                //����סԺ����
                if (m_objPrintRecArr[9] == null)
                {
                    p_strTypeID = "frmIMR_Gynecology";
                    m_objPrintRecArr[9] = new clsIMR_GynecologyPrintTool(p_strTypeID);
                }
                lngLoadIMR_Gynecology(ref m_objPrintRecArr[9], m_objSetInPatient, m_dtmSetInPatientDate);
                //�������סԺ����(���ʽ)
                if (m_objPrintRecArr[10] == null)
                {
                    p_strTypeID = "frmIMR_BurnSuergery";
                    m_objPrintRecArr[10] = new clsIMR_BurnSuergeryPrintTool(p_strTypeID);

                }
                lngLoadIMR_BurnSuergery(ref m_objPrintRecArr[10], m_objSetInPatient, m_dtmSetInPatientDate);
                //��Ѫ�����סԺ����
                if (m_objPrintRecArr[11] == null)
                {
                    p_strTypeID = "frmIMR_Cardiovascular";
                    m_objPrintRecArr[11] = new clsIMR_CardiovascularPrintTool(p_strTypeID);
                }
                lngLoadIMR_Cardiovascular(ref m_objPrintRecArr[11], m_objSetInPatient, m_dtmSetInPatientDate);
                //��������������֪��ͬ����
                if (m_objPrintRecArr[12] == null)
                {
                    p_strTypeID = "frmIMR_CataractSuffererApprove";
                    m_objPrintRecArr[12] = new clsIMR_CataractSuffererApprovePrintTool(p_strTypeID);
                }
                lngLoadIMR_CataractSuffererApprove(ref m_objPrintRecArr[12], m_objSetInPatient, m_dtmSetInPatientDate);
                //�ʹ���������¼
                if (m_objPrintRecArr[13] == null)
                {
                    p_strTypeID = "frmIMR_CesareanRecord";
                    m_objPrintRecArr[13] = new clsIMR_CesareanRecordPrintTool(p_strTypeID);
                }
                lngLoadIMR_CesareanRecord(ref m_objPrintRecArr[13], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����סԺ����(ʳ������������)
                if (m_objPrintRecArr[14] == null)
                {
                    p_strTypeID = "frmIMR_ChestSurgery_I";
                    m_objPrintRecArr[14] = new clsIMR_ChestSurgeryPrintTool(p_strTypeID);
                }
                lngLoadIMR_ChestSurgery_I(ref m_objPrintRecArr[14], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����סԺ����(�ط��ݸ���)
                if (m_objPrintRecArr[15] == null)
                {
                    p_strTypeID = "frmIMR_ChestSurgery_II";
                    m_objPrintRecArr[15] = new clsIMR_ChestSurgeryPrintTool(p_strTypeID);
                }
                lngLoadIMR_ChestSurgery_II(ref m_objPrintRecArr[15], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����סԺ����(���ٲ�)
                if (m_objPrintRecArr[16] == null)
                {
                    p_strTypeID = "frmIMR_ChestSurgery_III";
                    m_objPrintRecArr[16] = new clsIMR_ChestSurgeryPrintTool(p_strTypeID);
                }
                lngLoadIMR_ChestSurgery_III(ref m_objPrintRecArr[16], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����¼
                if (m_objPrintRecArr[17] == null)
                {
                    p_strTypeID = "frmIMR_childbirth";
                    m_objPrintRecArr[17] = new clsIMR_childbirthPrintTool(p_strTypeID);
                }
                lngLoadIMR_childbirth(ref m_objPrintRecArr[17], m_objSetInPatient, m_dtmSetInPatientDate);
                //������ת�Ƽ�¼
                if (m_objPrintRecArr[18] == null)
                {
                    p_strTypeID = "frmIMR_ChildbirthTransitSection";
                    m_objPrintRecArr[18] = new clsIMR_ChildbirthTransitSectionPrintTool(p_strTypeID);
                }
                lngLoadIMR_ChildbirthTransitSection(ref m_objPrintRecArr[18], m_objSetInPatient, m_dtmSetInPatientDate);
                //����סԺ����
                if (m_objPrintRecArr[19] == null)
                {
                    p_strTypeID = "frmIMR_DiabetesHospital";
                    m_objPrintRecArr[19] = new clsIMR_DiabetesHospitalPrintTool(p_strTypeID);
                }
                lngLoadIMR_DiabetesHospital(ref m_objPrintRecArr[19], m_objSetInPatient, m_dtmSetInPatientDate);
                //�������۲���
                if (m_objPrintRecArr[20] == null)
                {
                    p_strTypeID = "frmIMR_EmergencyCallWound";
                    m_objPrintRecArr[20] = new clsIMR_EmergenceWoundPrintTool(p_strTypeID);
                }
                lngLoadIMR_EmergencyCallWound(ref m_objPrintRecArr[20], m_objSetInPatient, m_dtmSetInPatientDate);
                //��ǯ������¼
                if (m_objPrintRecArr[21] == null)
                {
                    p_strTypeID = "frmIMR_ForpecsRecord";
                    m_objPrintRecArr[21] = new clsIMR_ForpecsRecordPrintTool(p_strTypeID);
                }
                lngLoadIMR_ForpecsRecord(ref m_objPrintRecArr[21], m_objSetInPatient, m_dtmSetInPatientDate);
                //��������������¼
                if (m_objPrintRecArr[22] == null)
                {
                    p_strTypeID = "frmIMR_GestationMisbirth_1";
                    m_objPrintRecArr[22] = new clsIMR_GestationMisbirth_1PrintTool(p_strTypeID);
                }
                lngLoadIMR_GestationMisbirth_1(ref m_objPrintRecArr[22], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����������������¼
                if (m_objPrintRecArr[23] == null)
                {
                    p_strTypeID = "frmIMR_GestationMisbirth_2";
                    m_objPrintRecArr[23] = new clsIMR_GestationMisbirth_2PrintTool(p_strTypeID);
                }
                lngLoadIMR_GestationMisbirth_2(ref m_objPrintRecArr[23], m_objSetInPatient, m_dtmSetInPatientDate);
                //��ѹ����ǯ����������¼
                if (m_objPrintRecArr[24] == null)
                {
                    p_strTypeID = "frmIMR_GestationMisbirth_3";
                    m_objPrintRecArr[24] = new clsIMR_GestationMisbirth_3PrintTool(p_strTypeID);
                }
                lngLoadIMR_GestationMisbirth_3(ref m_objPrintRecArr[24], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����ڿ�סԺ����
                if (m_objPrintRecArr[25] == null)
                {
                    p_strTypeID = "frmIMR_Breath";
                    m_objPrintRecArr[25] = new clsIMR_BreathPrintTool(p_strTypeID);
                }
                lngLoadIMR_Breath(ref m_objPrintRecArr[25], m_objSetInPatient, m_dtmSetInPatientDate);
                //���ಡסԺ��¼
                if (m_objPrintRecArr[26] == null)
                {
                    p_strTypeID = "frmIMR_HeartHospitalRecord";
                    m_objPrintRecArr[26] = new clsIMR_HeartHospitalRecordPrintTool(p_strTypeID);
                }
                lngLoadIMR_HeartHospitalRecord(ref m_objPrintRecArr[26], m_objSetInPatient, m_dtmSetInPatientDate);
                //����ҽѧ����Ժ��¼
                if (m_objPrintRecArr[27] == null)
                {
                    p_strTypeID = "frmIMR_Herbalism";
                    m_objPrintRecArr[27] = new clsIMR_HerbalismPrintTool(p_strTypeID);
                }
                lngLoadIMR_Herbalism(ref m_objPrintRecArr[27], m_objSetInPatient, m_dtmSetInPatientDate);
                //����ҽ��Ͽ�סԺ����
                if (m_objPrintRecArr[28] == null)
                {
                    p_strTypeID = "frmIMR_Herbalist_Western";
                    m_objPrintRecArr[28] = new clsIMR_Herbalist_WestPrintTool(p_strTypeID);
                }
                lngLoadIMR_Herbalist_Western(ref m_objPrintRecArr[28], m_objSetInPatient, m_dtmSetInPatientDate);
                //ҩ��������¼��
                if (m_objPrintRecArr[29] == null)
                {
                    p_strTypeID = "frmIMR_MedcineMiscarryRecord";
                    m_objPrintRecArr[29] = new clsIMR_MedcineMiscarryRecordPrintTool(p_strTypeID);
                }
                lngLoadIMR_MedcineMiscarryRecord(ref m_objPrintRecArr[29], m_objSetInPatient, m_dtmSetInPatientDate);
                //������סԺ����
                if (m_objPrintRecArr[30] == null)
                {
                    p_strTypeID = "frmIMR_Neonatal";
                    m_objPrintRecArr[30] = new clsIMR_NeonatalPrintTool(p_strTypeID);
                }
                lngLoadIMR_Neonatal(ref m_objPrintRecArr[30], m_objSetInPatient, m_dtmSetInPatientDate);
                //���ڿ�סԺ����
                if (m_objPrintRecArr[31] == null)
                {
                    p_strTypeID = "frmIMR_Neuromedicine";
                    m_objPrintRecArr[31] = new clsIMR_NeuromedicinePrintTool(p_strTypeID);
                }
                lngLoadIMR_Neuromedicine(ref m_objPrintRecArr[31], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����סԺ����
                if (m_objPrintRecArr[32] == null)
                {
                    p_strTypeID = "frmIMR_Neurosurgery";
                    m_objPrintRecArr[32] = new clsIMR_NeurosurgeryPrintTool(p_strTypeID);
                }
                lngLoadIMR_Neurosurgery(ref m_objPrintRecArr[32], m_objSetInPatient, m_dtmSetInPatientDate);
                //��������Ժ��¼
                if (m_objPrintRecArr[33] == null)
                {
                    p_strTypeID = "frmIMR_NewChild";
                    m_objPrintRecArr[33] = new clsIMR_NewChildPrintTool(p_strTypeID);
                }
                lngLoadIMR_NewChild(ref m_objPrintRecArr[33], m_objSetInPatient, m_dtmSetInPatientDate);
                //��Ѫ�ܲ�����
                if (m_objPrintRecArr[34] == null)
                {
                    p_strTypeID = "frmIMR_Cerebrovascular";
                    m_objPrintRecArr[34] = new clsIMR_CerebrovascularPrintTool(p_strTypeID);
                }
                lngLoadIMR_Cerebrovascular(ref m_objPrintRecArr[34], m_objSetInPatient, m_dtmSetInPatientDate);
                //������Ժ��¼
                if (m_objPrintRecArr[35] == null)
                {
                    p_strTypeID = "frmIMR_Obstetric_Criterion";
                    m_objPrintRecArr[35] = new clsIMR_Obstetric_CriterionPrintTool(p_strTypeID);
                }
                lngLoadIMR_Obstetric_Criterion(ref m_objPrintRecArr[35], m_objSetInPatient, m_dtmSetInPatientDate);               
                //�ۿ�סԺ����
                if (m_objPrintRecArr[36] == null)
                {
                    p_strTypeID = "frmIMR_Ophthalmology";
                    m_objPrintRecArr[36] = new clsIMR_OphthalmologyPrintTool(p_strTypeID);
                }
                lngLoadIMR_Ophthalmology(ref m_objPrintRecArr[36], m_objSetInPatient, m_dtmSetInPatientDate);
                //�ǿ�(��������΢�������)����֪��ͬ����
                if (m_objPrintRecArr[37] == null)
                {
                    p_strTypeID = "frmIMR_OrthopaedicsSuffererAprrove";
                    m_objPrintRecArr[37] = new clsIMR_OrthopaedicsSuffererAprrovePrintTool(p_strTypeID);
                }
                lngLoadIMR_OrthopaedicsSuffererAprrove(ref m_objPrintRecArr[37], m_objSetInPatient, m_dtmSetInPatientDate);
                //����סԺ����
                if (m_objPrintRecArr[38] == null)
                {
                    p_strTypeID = "frmIMR_Paediatrics";
                    m_objPrintRecArr[38] = new clsIMR_PaediatricsPrintTool(p_strTypeID);
                }
                lngLoadIMR_Paediatrics(ref m_objPrintRecArr[38], m_objSetInPatient, m_dtmSetInPatientDate);
                //������Ժ��¼
                if (m_objPrintRecArr[39] == null)
                {
                    p_strTypeID = "frmIMR_Paediatrics01";
                    m_objPrintRecArr[39] = new clsIMR_PaediatricsPrintTool01(p_strTypeID);
                }
                lngLoadIMR_Paediatrics01(ref m_objPrintRecArr[39], m_objSetInPatient, m_dtmSetInPatientDate);
                //���ڽ�������������¼��
                if (m_objPrintRecArr[40] == null)
                {
                    p_strTypeID = "frmIMR_PalaceBirthControlLaySkill";
                    m_objPrintRecArr[40] = new clsIMR_PalaceBirthControlLaySkillPrintTool(p_strTypeID);
                }
                lngLoadIMR_PalaceBirthControlLaySkill(ref m_objPrintRecArr[40], m_objSetInPatient, m_dtmSetInPatientDate);
                //��ǰ������ӵ�
                if (m_objPrintRecArr[41] == null)
                {
                    p_strTypeID = "frmIMR_PrePostOperateSee";
                    m_objPrintRecArr[41] = new clsIMR_PrePostOperateSeePrintTool(p_strTypeID);
                }
                lngLoadIMR_PrePostOperateSee(ref m_objPrintRecArr[41], m_objSetInPatient, m_dtmSetInPatientDate);
                //��ʪ�����ڿ�סԺ����
                if (m_objPrintRecArr[42] == null)
                {
                    p_strTypeID = "frmIMR_RheumatismImmunity";
                    m_objPrintRecArr[42] = new clsIMR_RheumatismImmunityPrintTool(p_strTypeID);
                }
                lngLoadIMR_RheumatismImmunity(ref m_objPrintRecArr[42], m_objSetInPatient, m_dtmSetInPatientDate);
                //θ�����סԺ����
                if (m_objPrintRecArr[43] == null)
                {
                    p_strTypeID = "frmIMR_StoIntesChirurgeryl";
                    m_objPrintRecArr[43] = new clsIMR_StoIntesChirurgerylPrintTool(p_strTypeID);
                }
                lngLoadIMR_StoIntesChirurgeryl(ref m_objPrintRecArr[43], m_objSetInPatient, m_dtmSetInPatientDate);
                //���ڽ�����ȡ������¼��
                if (m_objPrintRecArr[44] == null)
                {
                    p_strTypeID = "frmIMR_WombBirthControlRecord";
                    m_objPrintRecArr[44] = new clsIMR_WombBirthControlRecordPrintTool(p_strTypeID);
                }
                lngLoadIMR_WombBirthControlRecord(ref m_objPrintRecArr[44], m_objSetInPatient, m_dtmSetInPatientDate);
                //���ڿ�סԺ����
                if (m_objPrintRecArr[45] == null)
                {
                    p_strTypeID = "frmKidneyMedicineBeInHospital";
                    m_objPrintRecArr[45] = new clsKidneyMedicineBeInHospitalPrintTool(p_strTypeID);
                }
                lngLoadKidneyMedicineBeInHospital(ref m_objPrintRecArr[45], m_objSetInPatient, m_dtmSetInPatientDate);
                //�˹��������������ѹܼ�¼
                if (m_objPrintRecArr[46] == null)
                {
                    p_strTypeID = "frmManpowerAbortion";
                    m_objPrintRecArr[46] = new clsManpowerAbortionPrintTool(p_strTypeID);
                }
                lngLoadManpowerAbortion(ref m_objPrintRecArr[46], m_objSetInPatient, m_dtmSetInPatientDate);
                //����סԺ����(��һ)
                if (m_objPrintRecArr[47] == null)
                {
                    p_strTypeID = "frmObstetricCaseHis";
                    m_objPrintRecArr[47] = new clsObstetricCaseHisPrintTool(p_strTypeID);
                }
                lngLoadObstetricCaseHis(ref m_objPrintRecArr[47], m_objSetInPatient, m_dtmSetInPatientDate);
                //����סԺ����
                if (m_objPrintRecArr[48] == null)
                {
                    p_strTypeID = "frmIMR_Obstetric";
                    m_objPrintRecArr[48] = new clsIMR_ObstetricPrintTool(p_strTypeID);
                }
                lngLoadIMR_Obstetric(ref m_objPrintRecArr[48], m_objSetInPatient, m_dtmSetInPatientDate); 
                //����������
                if (m_objPrintRecArr[49] == null)
                {
                    p_strTypeID = "frmIMR_NerveSystem";
                    m_objPrintRecArr[49] = new clsIMR_NerveSystemPrintTool(p_strTypeID);
                }
                lngLoadIMR_NerveSystem(ref m_objPrintRecArr[49], m_objSetInPatient, m_dtmSetInPatientDate);
                //���ʰ�����
                if (m_objPrintRecArr[50] == null)
                {
                    p_strTypeID = "frmIMR_NasopharyngelCarcinoma";
                    m_objPrintRecArr[50] = new clsIMR_NasopharyngelCarcinomaPrintTool(p_strTypeID);
                }
                lngLoadIMR_NasopharyngelCarcinoma(ref m_objPrintRecArr[50], m_objSetInPatient, m_dtmSetInPatientDate);
                //���Ǻ�Ʋ���
                if (m_objPrintRecArr[51] == null)
                {
                    p_strTypeID = "frmIMR_IllnessHistoryRecord";
                    m_objPrintRecArr[51] = new clsIMR_IllnessHistoryRecordPrintTool(p_strTypeID);
                }
                lngLoadIMR_IllnessHistoryRecord(ref m_objPrintRecArr[51], m_objSetInPatient, m_dtmSetInPatientDate);
                //����̥ͷ����������������¼
                if (m_objPrintRecArr[52] == null)
                {
                    m_objPrintRecArr[52] = new clsEMR_PullDeliverRecordPrintTool();
                }
                lngLoadEMR_PullDeliverRecord(ref m_objPrintRecArr[52], m_objSetInPatient, m_dtmSetInPatientDate);
                #endregion

                //��ǰС��
                if (m_objPrintRecArr[53] == null)
                    m_objPrintRecArr[53] = new clsBeforeOperationSummaryPrintTool();
                lngLoadBeforeOperationSummary(ref m_objPrintRecArr[53], m_objSetInPatient, m_dtmSetInPatientDate);
                //������¼
                if (m_objPrintRecArr[54] == null)
                    m_objPrintRecArr[54] = new clsOperationRecordDoctorPrintTool();
                lngLoadOperationRecordDoctor(ref m_objPrintRecArr[54], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����¼
                if (m_objPrintRecArr[55] == null)
                    m_objPrintRecArr[55] = new clsConsultationPrintTool();
                lngLoadConsultation(ref m_objPrintRecArr[55], m_objSetInPatient, m_dtmSetInPatientDate);
                //24Сʱ�����Ժ��¼
                if (m_objPrintRecArr[56] == null)
                    m_objPrintRecArr[56] = new clsEMR_OutHospitalIn24HoursPrintTool();
                lngLoadEMR_OutHospitalIn24Hours(ref m_objPrintRecArr[56], m_objSetInPatient, m_dtmSetInPatientDate);
                //��Ժ24Сʱ��������¼
                if (m_objPrintRecArr[57] == null)
                    m_objPrintRecArr[57] = new clsDeathRecordIn24HoursPrintTool();
                lngLoadDeathRecordIn24Hours(ref m_objPrintRecArr[57], m_objSetInPatient, m_dtmSetInPatientDate);
                //������¼
                if (m_objPrintRecArr[58] == null)
                    m_objPrintRecArr[58] = new clsDeathrecordPrintTool();
                lngLoadDeathrecord(ref m_objPrintRecArr[58], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����������ۼ�¼
                if (m_objPrintRecArr[59] == null)
                    m_objPrintRecArr[59] = new clsDeathDiscussPrintTool();
                lngLoadDeathDiscuss(ref m_objPrintRecArr[59], m_objSetInPatient, m_dtmSetInPatientDate);
                //����ժҪ
                if (m_objPrintRecArr[60] == null)
                {
                    clsCaseHistorySummary objRecordContent = null;
                    long lngRef = -1;

                    clsInHospitalMainRecordServ objServ =
                        (clsInHospitalMainRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsInHospitalMainRecordServ));

                    try
                    {
                        lngRef = objServ.m_lngGetAllergicValue("frmCaseHistorySummary", m_objSetInPatient.m_StrInPatientID, m_dtmSetInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),false, out objRecordContent);
                        if (lngRef > 0 && objRecordContent != null)
                        {
                            m_objPrintRecArr[14] = new cls_EmergencyCallPrintTool(p_strTypeID);//��㸳��ֵʹ�䲻Ϊ��
                            m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr = new object[2];
                            m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr[0] = objRecordContent;
                            m_objSetValue.m_objPrintInfo_CaseHistorySummaryArr[1] = m_objSetInPatient;
                        }
                    }
                    catch (Exception ex)
                    {
                        string strMeg = ex.ToString();
                    }
                }
                //lngLoadCaseHistorySummary(m_objSetInPatient, m_dtmSetInPatientDate);
            }
#endregion
            #region ��ʿ����վ����
            if (m_chkNurser.Checked)
            {
                //���������¼
                if (m_objPrintRecArr[61] == null)
                    m_objPrintRecArr[61] = new clsOperationRecordPrintTool();
                lngLoadOperationRecordNurse(ref m_objPrintRecArr[61], m_objSetInPatient, m_dtmSetInPatientDate);

                //һ�㻤���¼
                if (m_objPrintRecArr[62] == null)
                    m_objPrintRecArr[62] = new clsGeneralNurseRecordPrintTool();
                lngLoadGeneralTendRecord(ref m_objPrintRecArr[62], m_objSetInPatient, m_dtmSetInPatientDate);

                //Σ�ػ��߻����¼
                if (m_objPrintRecArr[63] == null)
                    m_objPrintRecArr[63] = new clsIntensiveTendMainPrintTool();
                lngLoadIntensiveTendMain(ref m_objPrintRecArr[63], m_objSetInPatient, m_dtmSetInPatientDate);

                //Σ��֢�໤�����ػ���¼��
                if (m_objPrintRecArr[64] == null)
                    m_objPrintRecArr[64] = new clsICUIntensiveTendPrintTool();
                lngLoadICUIntensiveTend(ref m_objPrintRecArr[64], m_objSetInPatient, m_dtmSetInPatientDate);

                //���µ�
                if (m_objPrintRecArr[65] == null)
                {
                    if (clsEMRLogin.m_StrCurrentHospitalNO == "450101001")//����
                    {
                        m_objPrintRecArr[65] = new iCare.GN.clsThreeMeasureRecordPrintTool_New();
                    }
                    else
                    {
                        m_objPrintRecArr[65] = new clsThreeMeasureRecordPrintTool_New();
                    }
                }
                lngLoadThreeMeasureRecord(ref m_objPrintRecArr[65], m_objSetInPatient, m_dtmSetInPatientDate);
                //�����
                //һ�㻼�߻����¼
                if (m_objPrintRecArr[66] == null)
                {
                    string[] m_strCustomColumn ={ "", "" };
                    m_objPrintRecArr[66] = new clsGeneralNurseRecord_GXPrintTool(m_strCustomColumn);
                }
                lngLoadGeneralNurseRecord_GX(ref m_objPrintRecArr[66], m_objSetInPatient, m_dtmSetInPatientDate);
                //����ICU���������Ƽ໤��¼��
                if (m_objPrintRecArr[67] == null)
                    m_objPrintRecArr[67] = new clsICUBreathPrintTool();
                lngLoadMainICUBreath(ref m_objPrintRecArr[67], m_objSetInPatient, m_dtmSetInPatientDate);
                //����΢��Ѫ�Ǽ���¼��
                if (m_objPrintRecArr[68] == null)
                    m_objPrintRecArr[68] = new clsMiniBooldSugarChkPrintTool();
                lngLoadMiniBooldSugarChk(ref m_objPrintRecArr[68], m_objSetInPatient, m_dtmSetInPatientDate);
                //ICU�����¼
                if (m_objPrintRecArr[69] == null)
                    m_objPrintRecArr[69] = new clsICUNurseRecord_GX_PrintTool();
                lngLoadICUNurseRecord(ref m_objPrintRecArr[69], m_objSetInPatient, m_dtmSetInPatientDate);
                //���ICU�໤��¼
                if (m_objPrintRecArr[70] == null)
                    m_objPrintRecArr[70] = new clsSurgeryICUWardship_PrintTool();
                lngLoadSurgeryICUWardship(ref m_objPrintRecArr[70], m_objSetInPatient, m_dtmSetInPatientDate);
                //��Ѫ������ػ���¼
                if (m_objPrintRecArr[71] == null)
                    m_objPrintRecArr[71] = new clsCardiovascularTendPrintTool();
                lngLoadCardiovascularTend(ref m_objPrintRecArr[71], m_objSetInPatient, m_dtmSetInPatientDate);
                //����������ҩ�۲��¼��
                if (m_objPrintRecArr[72] == null)
                    m_objPrintRecArr[72] = new clsVeinSpecialUseDrugPrintTool();
                lngLoadVeinSpecialUseDrug(ref m_objPrintRecArr[72], m_objSetInPatient, m_dtmSetInPatientDate);
                //�������ǼǱ�
                if (m_objPrintRecArr[73] == null)
                    m_objPrintRecArr[73] = new clsVeinSpecialUseDrugPrintTool(); ;
                //lngLoadRegisterQuantity(ref m_objPrintRecArr[64], m_objSetInPatient, m_dtmSetInPatientDate);
            }
            #endregion
             */
            #endregion
            //���Ӧ����
            #region ר�Ʊ�
            string p_strTypeID = "";
            //������ҳ
            if (m_objPrintRecArr[0] == null)
                m_objPrintRecArr[0] = new clsInHospitalMainRecordPrintTool(true);
            lngLoadInHospitalMainRecord(ref m_objPrintRecArr[0], m_objSetInPatient, m_dtmSetInPatientDate);
            //��Ժ��¼
            if (m_objPrintRecArr[1] == null)
                m_objPrintRecArr[1] = new clsOutHospitalPrintTool();
            lngLoadOutHospital(ref m_objPrintRecArr[1], m_objSetInPatient, m_dtmSetInPatientDate);

            //��ͨסԺ����(���)
            if (m_objPrintRecArr[2] == null)
                m_objPrintRecArr[2] = new clsInPatientCaseHistory_F2PrintTool();
            lngLoadInPatientCaseHistory_F2(ref m_objPrintRecArr[2], m_objSetInPatient,m_dtmSetInPatientDate);

            //����סԺ����
            if (m_objPrintRecArr[3] == null)
            {
                p_strTypeID = "frmIMR_GynecologyF2";
                m_objPrintRecArr[3] = new clsIMR_GynecologyF2PrintTool(p_strTypeID);
            }
            lngLoadIMR_Gynecology(ref m_objPrintRecArr[3], m_objSetInPatient, m_dtmSetInPatientDate);
            //����סԺ����
            if (m_objPrintRecArr[4] == null)
            {
                p_strTypeID = "frmIMR_Obstetric_F2";
                m_objPrintRecArr[4] = new clsIMR_Obstetric_F2PrintTool(p_strTypeID);
            }
            lngLoadIMR_Obstetric(ref m_objPrintRecArr[4], m_objSetInPatient, m_dtmSetInPatientDate);
            //���̼�¼
            if (m_objPrintRecArr[5] == null)
                m_objPrintRecArr[5] = new clsSubDiseaseTrackPrintTool();
            lngLoadSubDiseaseTrack(ref m_objPrintRecArr[5], m_objSetInPatient, m_dtmSetInPatientDate);
            
            //�ʹ���������¼
            if (m_objPrintRecArr[6] == null)
            {
                p_strTypeID = "frmIMR_CesareanRecord";
                m_objPrintRecArr[6] = new clsIMR_CesareanRecordPrintTool(p_strTypeID);
            }
            lngLoadIMR_CesareanRecord(ref m_objPrintRecArr[6], m_objSetInPatient, m_dtmSetInPatientDate);
            //��Ժ������
            if (m_objPrintRecArr[7] == null)
                m_objPrintRecArr[7] = new clsEMR_InPatientEvaluatePrintTool();
            lngLoadInPatientEvaluate(ref m_objPrintRecArr[7], m_objSetInPatient, m_dtmSetInPatientDate);
            //һ�㻤���¼
            if (m_objPrintRecArr[8] == null)
                m_objPrintRecArr[8] = new clsGeneralNurseRecordPrintTool();
            lngLoadGeneralTendRecord(ref m_objPrintRecArr[8], m_objSetInPatient, m_dtmSetInPatientDate);
            //Σ�ػ��߻����¼
            if (m_objPrintRecArr[9] == null)
                m_objPrintRecArr[9] = new clsIntensiveTendMain_FC_PrintTool();
            lngLoadIntensiveTendMain(ref m_objPrintRecArr[9], m_objSetInPatient, m_dtmSetInPatientDate);
            //����������ʱ��¼
            if (m_objPrintRecArr[10] == null)
            {
                p_strTypeID = "frmChildbearingRecord_F2";
                m_objPrintRecArr[10] = new clsChildbearingRecord_F2PrintTool(p_strTypeID);
            }
            lngLoadChildbearingRecord(ref m_objPrintRecArr[10], m_objSetInPatient, m_dtmSetInPatientDate);
            //����������ȡ�����¾��󸹲����ѹܽ�����¼
            if (m_objPrintRecArr[11] == null)
            {
                p_strTypeID = "frmIMR_ManpowerAbortionRecord";
                m_objPrintRecArr[11] = new clsIMR_ManpowerAbortionRecordPrintTool(p_strTypeID);
            }
            lngLoadManpowerAbortionRecord(ref m_objPrintRecArr[11], m_objSetInPatient, m_dtmSetInPatientDate);
            //������¼��
            if (m_objPrintRecArr[12] == null)
            {
                p_strTypeID = "frmInducedLaborRecord";
                m_objPrintRecArr[12] = new clsIMR_InducedLaborRecordPrintTool(p_strTypeID);
            }
            lngLoadInducedLaborRecord(ref m_objPrintRecArr[12], m_objSetInPatient, m_dtmSetInPatientDate);
            //��ǯ������¼
            if (m_objPrintRecArr[13] == null)
            {
                p_strTypeID = "frmIMR_ForpecsRecord";
                m_objPrintRecArr[13] = new clsIMR_ForpecsRecordPrintTool(p_strTypeID);
            }
            lngLoadIMR_ForpecsRecord(ref m_objPrintRecArr[13], m_objSetInPatient, m_dtmSetInPatientDate);
            
            //��������������¼
            if (m_objPrintRecArr[14] == null)
            {
                p_strTypeID = "frmIMR_GestationMisbirth_1";
                m_objPrintRecArr[14] = new clsIMR_GestationMisbirth_1PrintTool(p_strTypeID);
            }
            lngLoadIMR_GestationMisbirth_1(ref m_objPrintRecArr[14], m_objSetInPatient, m_dtmSetInPatientDate);
            //��ǰС��
            if (m_objPrintRecArr[15] == null)
                m_objPrintRecArr[15] = new clsBeforeOperationSummaryPrintTool();
            lngLoadBeforeOperationSummary(ref m_objPrintRecArr[15], m_objSetInPatient, m_dtmSetInPatientDate);
            
            
            
            //�����
            //if (m_objPrintRecArr[13] == null)
            //m_objPrintRecArr[13] = new clsThreeMeasureRecordPrintTool();
            //lngLoadThreeMeasureRecord(ref m_objPrintRecArr[13], m_objSetInPatient, m_dtmSetInPatientDate);
            //��Ӥ��Ӥ��������
            if (m_objPrintRecArr[16] == null)
                m_objPrintRecArr[16] = new clsAYQBabyAssessmentRecordPrintTool();
            lngLoadAYQBabyAssessmentRecord(ref m_objPrintRecArr[16], m_objSetInPatient, m_dtmSetInPatientDate);
            //���̼�¼
            //if (m_objPrintRecArr[15] == null)
                //m_objPrintRecArr[15] = new clsWaitLayRecord_AcadPrintTool();
            //lngLoadWaitLayRecord_Acad(ref m_objPrintRecArr[15], m_objSetInPatient, m_dtmSetInPatientDate);
            //�߲��ؾ�����ι۲��
            if (m_objPrintRecArr[17] == null)
                m_objPrintRecArr[17] = new clsEMR_OXTIntravenousDripPrintTool();
            lngLoadOXTIntravenousDrip(ref m_objPrintRecArr[17], m_objSetInPatient, m_dtmSetInPatientDate);
            //�����¼
            if (m_objPrintRecArr[18] == null)
                m_objPrintRecArr[18] = new clsWaitLayRecord_AcadPrintTool();
            lngLoadWaitLayRecord_Acad(ref m_objPrintRecArr[18], m_objSetInPatient, m_dtmSetInPatientDate);
            
            #endregion
            for (int i = 0; i < MAXFORMNUM; i++)
            {
                if (m_objPrintRecArr[i] != null)
                    m_objPrintRecArr[i].m_mthInitPrintTool(null);
            }

        }

        private void m_mthSubSave()
        {
            SaveFileDialog objDlg = new SaveFileDialog();
            objDlg.Title = "��ѡ�񱣴�·��";
            objDlg.OverwritePrompt = true;
            objDlg.CheckPathExists = true;
            //	objDlg.RestoreDirectory=true;
            objDlg.AddExtension = true;
            objDlg.DefaultExt = "emr";
            objDlg.Filter = "�嫵��Ӳ��� (*.emr)|*.emr|�����ļ� (*.*)|*.*";
            if (objDlg.ShowDialog() != DialogResult.OK) return;
            int intTemp = -1;
            try
            {
                PrintDocument pdoc = new PrintDocument();
                //				this.Cursor=Cursors.WaitCursor;
                //				m_objSetValue=new clsInPatientCaseHistory_SetValue();
                //				m_mthSetPrintToolsContent();
                string strPath = objDlg.FileName;
                //
                //				
                float scale = Convert.ToSingle(100) / 100f;
                long quality = Convert.ToInt64(100);
                string output = strPath;
                this.Cursor = this.Cursor = Cursors.WaitCursor;
                m_objSetValue = new clsInPatientCaseHistory_SetValue();
                m_objPrintRecArr = new infPrintRecord[MAXFORMNUM];
                if (m_hasItemsArr == null)
                    m_hasItemsArr = new Hashtable[49];
                m_mthSetPrintToolsContent();
                this.Cursor = Cursors.Default;
                if (!m_blnHasMoreForm(intTemp)) return;

                PrintController controller = new clsPreviewPrintControllerFile(ImageFormat.Jpeg, scale, quality, output, true);
                m_pdcPrintDocument.PrintController = new PrintControllerWithStatusDialog(controller, "�����ļ�...");
                m_pdcPrintDocument.Print();
            }
            catch
            {
                clsPublicFunction.ShowInformationMessageBox("�޷������ļ��������ļ����Ƿ���ֻ���ļ���������̿ռ䲻�㡣");
                this.Cursor = Cursors.Default;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_mthSubOpen()
        {

            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);

            OpenFileDialog objDlg = new OpenFileDialog();
            objDlg.Title = "��ѡ��Ҫ�򿪵��ļ�";
            objDlg.CheckFileExists = true;
            objDlg.CheckPathExists = true;
            objDlg.AddExtension = true;
            objDlg.DefaultExt = "emr";
            objDlg.Filter = "�嫵��Ӳ��� (*.emr)|*.emr|�����ļ� (*.*)|*.*";
            if (objDlg.ShowDialog() != DialogResult.OK) return;


            try
            {
                #region ע��
                //				this.Cursor=Cursors.WaitCursor;
                //				IFormatter objForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //				Stream objStream = new System.IO.FileStream(objDlg.FileName,FileMode.Open);
                //				m_objSetValue=null;
                //				m_objSetValue = (clsInPatientCaseHistory_SetValue)objForm.Deserialize(objStream);
                //				objStream.Close();
                //				
                //				for(int i=0;i<MAXFORMNUM;i++)
                //				{
                //					m_objPrintRecArr[i]=null;
                //				}
                //
                //				#region �ɵĴ�ӡ˳��
                ////				if(m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr!=null) m_objPrintRecArr[0]=new  clsInPatientCaseHistoryPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr!=null) m_objPrintRecArr[1]=new  clsSubDiseaseTrackPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_ConsultationArr!=null) m_objPrintRecArr[2]=new  clsConsultationPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_OperationAgreedArr!=null) m_objPrintRecArr[3]=null;
                ////				if(m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr!=null) m_objPrintRecArr[4]=new  clsBeforeOperationSummaryPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr!=null) m_objPrintRecArr[5]=new  clsOperationRecordDoctorPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_PICUShiftInArr!=null) m_objPrintRecArr[6]=new  clsPICUShiftBasePrintTool(true);
                ////				if(m_objSetValue.m_objPrintInfo_PICUShiftOutArr!=null) m_objPrintRecArr[7]=new  clsPICUShiftBasePrintTool(false);
                ////				if(m_objSetValue.m_objPrintInfo_OutHospitalArr!=null) m_objPrintRecArr[8]=new  clsOutHospitalPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr!=null) m_objPrintRecArr[9]=new clsInHospitalMainRecordPrintTool();
                ////				
                ////				if(m_objSetValue.m_objPrintInfo_InPatientEvaluateArr!=null) m_objPrintRecArr[10]=new  clsInPatientEvaluatePrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr!=null) m_objPrintRecArr[11]=new  clsThreeMeasureRecordPrintTool_New();
                ////				if(m_objSetValue.m_objPrintInfo_GeneralTendRecordArr!=null) m_objPrintRecArr[12]=new  clsGeneralNurseRecordPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_WatchItemRecordArr!=null) m_objPrintRecArr[13]=new  clsWatchItemTrackPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_IntensiveTendMainArr!=null) m_objPrintRecArr[14]=new  clsIntensiveTendMainPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr!=null) m_objPrintRecArr[15]=new  clsICUIntensiveTendPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_OperationRecordNurseArr!=null) m_objPrintRecArr[16]=new clsOperationRecordPrintTool();
                ////				if(m_objSetValue.m_objPrintInfo_OperationEquipmentQtyArr!=null) m_objPrintRecArr[17]=new clsOperationEquipmentQtyPrintTool();
                //				#endregion
                //				//������ҳ
                //				if(m_objSetValue.m_objPrintInfo_InHospitalMainRecordArr!=null) m_objPrintRecArr[0]=new clsInHospitalMainRecordPrintTool(true);
                //				//��Ժ
                //				if(m_objSetValue.m_objPrintInfo_OutHospitalArr!=null) m_objPrintRecArr[1]=new  clsOutHospitalPrintTool();
                //				//סԺ־
                //				if(m_objSetValue.m_objPrintInfo_InPatientCaseHistoryArr!=null) m_objPrintRecArr[2]=new  clsInPatientCaseHistoryPrintTool();
                //				//���̼�¼
                //				if(m_objSetValue.m_objPrintInfo_SubDiseaseTrackArr!=null) m_objPrintRecArr[3]=new  clsSubDiseaseTrackPrintTool();
                //				//��ǰС��
                //				if(m_objSetValue.m_objPrintInfo_BeforeOperationSummaryArr!=null) m_objPrintRecArr[4]=new  clsBeforeOperationSummaryPrintTool();
                //				//������¼
                //				if(m_objSetValue.m_objPrintInfo_OperationRecordDoctorArr!=null) m_objPrintRecArr[5]=new  clsOperationRecordDoctorPrintTool();
                //				//�����¼
                //				if(m_objSetValue.m_objPrintInfo_ConsultationArr!=null) m_objPrintRecArr[6]=new  clsConsultationPrintTool();
                //				//���������¼
                //				if(m_objSetValue.m_objPrintInfo_OperationRecordNurseArr!=null) m_objPrintRecArr[7]=new clsOperationRecordPrintTool();
                //				//һ�㻤���¼
                //				if(m_objSetValue.m_objPrintInfo_GeneralTendRecordArr!=null) m_objPrintRecArr[8]=new  clsGeneralNurseRecordPrintTool();
                //				//Σ�ػ��߻����¼
                //				if(m_objSetValue.m_objPrintInfo_IntensiveTendMainArr!=null) m_objPrintRecArr[9]=new  clsIntensiveTendMainPrintTool();
                //				//Σ��֢�໤�����ػ���¼��
                //				if(m_objSetValue.m_objPrintInfo_ICUIntensiveTendArr!=null) m_objPrintRecArr[10]=new  clsICUIntensiveTendPrintTool();
                //				//���µ�
                //				if(m_objSetValue.m_objPrintInfo_ThreeMeasureRecordArr!=null) m_objPrintRecArr[11]=new  clsThreeMeasureRecordPrintTool_New();
                //				
                //				for(int i=0;i<MAXFORMNUM;i++)
                //				{
                //					if(m_objPrintRecArr[i]!=null)m_objPrintRecArr[i].m_mthInitPrintTool(null);
                //				}
                //
                //				this.Cursor=this.Cursor=Cursors.Default;
                //				//				PrintPreviewDialog ppdPrintPreview = new PrintPreviewDialog();
                ////				ppdPrintPreview.Document = m_pdcPrintDocument;
                ////				ppdPrintPreview.ShowDialog();
                //				frmPreview.ShowDialog();
                #endregion
                FileStream fs = new FileStream(objDlg.FileName, FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    // Deserialize the hashtable from the file and 
                    // assign the reference to the local variable.
                    arlTemp = (ArrayList)formatter.Deserialize(fs);
                    intPages = 0;
                    PrintTool.frmPrintPreviewDialogAll frmDlg = new iCare.PrintTool.frmPrintPreviewDialogAll(arlTemp, true);
                    frmDlg.ShowDialog();
                    //					PrintPreviewDialog dlg = new PrintPreviewDialog();
                    //					dlg.Document = printDocument1;
                    // 					dlg.ShowDialog();

                }
                catch (SerializationException exp)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + exp.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }


            }
            catch
            {
                clsPublicFunction.ShowInformationMessageBox("�޷����ļ��������ļ��Ƿ���Ч���嫵��Ӳ�����");
                this.Cursor = Cursors.Default;
            }
        }

        #endregion ��ӡ���Ƽ�����

        #region ��ť�¼�
        private void m_cmdPreview_Click(object sender, System.EventArgs e)
        {
            blnIsPreview = true;
            try
            {
                m_mthPrint();
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message + ex.TargetSite.ToString());
            }
        }

        private void m_cmdSave_Click(object sender, System.EventArgs e)
        {
            m_mthSubSave();
        }

        private void m_cmdOpen_Click(object sender, System.EventArgs e)
        {
            m_mthSubOpen();
        }

        private void m_cmdClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmInPatientCaseHistory_SetForm_Load_1(object sender, System.EventArgs e)
        {
            m_trvTime.Focus();
        }

        #endregion

        #region ����
        //clsSubDiseaseTrackPrintTool objPrintTool;
        //		private void m_mthDemoPrint_FromDataSource()
        //		{	
        //			objPrintTool=new clsSubDiseaseTrackPrintTool();
        //			objPrintTool.m_mthInitPrintTool(null);	
        //			if(m_objBaseCurrentPatient==null)
        //				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
        //			else 
        //				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.MinValue);
        //						
        //			objPrintTool.m_mthInitPrintContent();	
        //
        //			//���浽�ļ�
        //			object objtemp=objPrintTool.m_objGetPrintInfo();
        //			IFormatter objForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //
        //			Stream objStream = new System.IO.FileStream("d:\\code\\Tese.bin",FileMode.Create);
        //
        //			objForm.Serialize(objStream,objtemp);
        //
        //			objStream.Flush();
        //			objStream.Close();
        //		
        //			m_mthStartPrint();
        //		}
        //		private void m_mthDemoPrint_FromFile()
        //		{	
        //			objPrintTool=new clsSubDiseaseTrackPrintTool();
        //			objPrintTool.m_mthInitPrintTool(null);	
        //
        //			IFormatter objForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //			Stream objStream = new System.IO.FileStream("d:\\code\\Tese.bin",FileMode.Open);
        //			object objtemp = objForm.Deserialize(objStream);//
        //			objStream.Close();
        //		
        //			objPrintTool.m_mthSetPrintContent(objtemp);		
        //
        //			m_mthStartPrint();
        //		}
        ////		private void m_mthStartPrint()
        ////		{			
        ////			PrintPreviewDialog ppdPrintPreview = new PrintPreviewDialog();
        ////			ppdPrintPreview.Document = m_pdcPrintDocument;
        ////			ppdPrintPreview.ShowDialog();
        ////		}
        //		
        ////		bool bbb=true;	// just try
        //		private void button1_Click(object sender, System.EventArgs e)
        //		{
        ////			//Print();
        ////			if(bbb) m_mthDemoPrint_FromDataSource();
        ////			else 
        ////			{
        ////				m_mthDemoPrint_FromFile();
        ////				clsPublicFunction.ShowInformationMessageBox("from file");
        ////			}
        ////			bbb=!bbb;
        //		}
        //	}

        /// <summary>
        /// ����Ҫ������ʾ
        /// </summary>
        #endregion
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle destRect = new Rectangle(0, 0, 827, 1169);
            byte[] data = (byte[])arlTemp[intPages];
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            Image imgxx = new Bitmap(ms);
            ms.Close();
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            e.Graphics.DrawImage(imgxx, destRect);
            if (intPages < arlTemp.Count - 1)
            {
                intPages++;
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            intPages = 0;
            m_intCurrentForm = 0;
            m_intCurrentCopies = 0;
            m_blnNewPage = true;
        }

        private void txtInPatientID_TextChanged(object sender, EventArgs e)
        {
            //m_cmdPreview.Enabled = false;
            //m_cmdSave.Enabled = false;
            //m_chkNurser.Enabled = false;
            //m_chkDoctor.Enabled = false;
            //m_chkNurser.Checked = false;
            //m_chkDoctor.Checked = false;
        }
        protected override void m_mthAddFormStatusForClosingSave()
        {

        }
        protected override void m_mthSetPatientBaseInfo(clsPatient p_objSelectedPatient)
        { }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            blnIsPreview = false;
            try
            {
                m_mthPrint();
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message + ex.TargetSite.ToString());
            }
        }


    }
}

