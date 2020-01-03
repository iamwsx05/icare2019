//#define FunctionPrivilege
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using weCare.Core.Entity;
using System.Windows.Forms;
using com.digitalwave.controls;
using System.Drawing.Printing;
using System.Data;
using System.Drawing.Drawing2D;
using com.digitalwave.Emr.Signature_gui;

namespace iCare
{
    public class frmRecordsBase : frmHRPBaseForm, PublicFunction
    {
        protected com.digitalwave.Controls.ctlDataGrid m_dtgRecordDetail;
        protected System.Windows.Forms.ContextMenu ctmRecordControl;
        protected System.Windows.Forms.MenuItem mniAppend;
        private System.Windows.Forms.MenuItem mniEdit;
        private System.Windows.Forms.MenuItem mniDelete;
        protected System.Windows.Forms.DataGridTableStyle dgtsStyles;
        protected System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
        protected System.Windows.Forms.TreeView m_trvInPatientDate;
        private System.Windows.Forms.MenuItem mniApprove;
        private System.Windows.Forms.MenuItem mniUnApprove;
        protected System.Windows.Forms.MenuItem m_mniAddBlank;
        protected System.Windows.Forms.MenuItem m_mniDeleteBlank;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  ���̼�¼�������¼���Ļ����塣
        /// �ڴ˴��崦���桢�޸ġ�ɾ�������������������ۼ���˫���߹�����ս����ͨ���߼���
        /// ���ò�����Ϣ�ͻ�ȡ������Ϣ��ͨ���߼���
        /// ��ӡ���ܣ���ȡ�Ѿ�ɾ���ļ�¼��
        /// Alex 2003-5-10
        /// </summary>
        public frmRecordsBase()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            m_trvInPatientDate.HideSelection = false;
            // TODO: Add any initialization after the InitializeComponent call
            m_objCurrentPatient = null;

            //m_objBorderTool = new clsBorderTool(Color.White);
            //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{m_dtgRecordDetail,m_trvInPatientDate});

            m_objRecordsDomain = m_objGetRecordsDomain();

            m_dtbRecords = new DataTable("RecordDetail");

            m_objPublicDomain = new clsPublicDomain();

            this.m_dtgRecordDetail.HeaderFont = m_FntHeaderFont;
            m_BlnNeedContextMenu = false;


        }

        /// <summary>
        /// ��ȡ������ʱ��Ĺ�����Domain
        /// </summary>
        private clsPublicDomain m_objPublicDomain;
        /// <summary>
        ///  ���̼�¼�������ʵ��
        /// </summary>
        protected clsRecordsDomain m_objRecordsDomain;
        /// <summary>
        /// ��ǰ����
        /// </summary>
        protected clsPatient m_objCurrentPatient;
        /// <summary>
        /// �ı�ؼ��߿򹤾�
        /// </summary>
        //protected clsBorderTool m_objBorderTool;

        /// <summary>
        /// ��ӡ����������ĵ�
        /// </summary>

        protected bool m_blnAlreadySetPrintTools = false;
        /// <summary>
        /// ��¼�����
        /// </summary>
        protected DataTable m_dtbRecords;
        /// <summary>
        /// ��һ�δ�ӡ����
        /// </summary>
        private DateTime[] m_dtmFirstPrintDateArr;
        /// <summary>
        /// �Ƿ��һ�δ�ӡ����
        /// </summary>
        private bool[] m_blnIsFirstPrintArr;
        /// <summary>
        /// 
        /// </summary>
        private clsTransDataInfo[] m_objTransDataArr;

        /// <summary>
        /// ������Ӽ�¼ʱ��ʱ�����������
        /// </summary>
        private DataTable m_dtbTempTable;
        /// <summary>
        /// ����������(�麯��)
        /// </summary>
        protected virtual Font m_FntHeaderFont
        {
            get
            {
                return new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            }
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
            if (m_blnAlreadySetPrintTools)//�ͷŴ�ӡ����
            {
                m_mthDisposePrintTools();
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
            this.m_dtgRecordDetail = new com.digitalwave.Controls.ctlDataGrid();
            this.dgtsStyles = new System.Windows.Forms.DataGridTableStyle();
            this.ctmRecordControl = new System.Windows.Forms.ContextMenu();
            this.mniAppend = new System.Windows.Forms.MenuItem();
            this.mniEdit = new System.Windows.Forms.MenuItem();
            this.mniDelete = new System.Windows.Forms.MenuItem();
            this.mniApprove = new System.Windows.Forms.MenuItem();
            this.mniUnApprove = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.m_mniAddBlank = new System.Windows.Forms.MenuItem();
            this.m_mniDeleteBlank = new System.Windows.Forms.MenuItem();
            this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_trvInPatientDate = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(640, 96);
            this.lblSex.Name = "lblSex";
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(752, 96);
            this.lblAge.Name = "lblAge";
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(288, 96);
            this.lblBedNoTitle.Name = "lblBedNoTitle";
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(812, 96);
            this.lblInHospitalNoTitle.Name = "lblInHospitalNoTitle";
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(428, 96);
            this.lblNameTitle.Name = "lblNameTitle";
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(588, 96);
            this.lblSexTitle.Name = "lblSexTitle";
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(696, 96);
            this.lblAgeTitle.Name = "lblAgeTitle";
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(40, 112);
            this.lblAreaTitle.Name = "lblAreaTitle";
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(880, 116);
            this.m_lsvInPatientID.Name = "m_lsvInPatientID";
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(880, 92);
            this.txtInPatientID.Name = "txtInPatientID";
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(476, 92);
            this.m_txtPatientName.Name = "m_txtPatientName";
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(336, 92);
            this.m_txtBedNO.Name = "m_txtBedNO";
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(88, 108);
            this.m_cboArea.Name = "m_cboArea";
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(476, 120);
            this.m_lsvPatientName.Name = "m_lsvPatientName";
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(336, 120);
            this.m_lsvBedNO.Name = "m_lsvBedNO";
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(88, 72);
            this.m_cboDept.Name = "m_cboDept";
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(40, 80);
            this.lblDept.Name = "lblDept";
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.Name = "m_cmdNewTemplate";
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.Name = "m_cmdNext";
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.Name = "m_cmdPre";
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Name = "m_lblForTitle";
            this.m_lblForTitle.Text = "��ȡ�����¼���͵ĸ���";
            this.m_lblForTitle.Visible = true;
            // 
            // m_dtgRecordDetail
            // 
            this.m_dtgRecordDetail.BackColor = System.Drawing.Color.White;
            this.m_dtgRecordDetail.BackgroundColor = System.Drawing.Color.FromArgb(((System.Byte)(52)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_dtgRecordDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_dtgRecordDetail.CaptionBackColor = System.Drawing.Color.FromArgb(((System.Byte)(52)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_dtgRecordDetail.CaptionFont = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.m_dtgRecordDetail.CaptionText = "��¼����";
            this.m_dtgRecordDetail.CaptionVisible = false;
            this.m_dtgRecordDetail.DataMember = "";
            this.m_dtgRecordDetail.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.m_dtgRecordDetail.ForeColor = System.Drawing.Color.Black;
            this.m_dtgRecordDetail.HeaderFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.m_dtgRecordDetail.HeaderForeColor = System.Drawing.Color.Black;
            this.m_dtgRecordDetail.Location = new System.Drawing.Point(8, 140);
            this.m_dtgRecordDetail.Name = "m_dtgRecordDetail";
            this.m_dtgRecordDetail.ParentRowsForeColor = System.Drawing.Color.White;
            this.m_dtgRecordDetail.RowHeaderWidth = 1;
            this.m_dtgRecordDetail.Size = new System.Drawing.Size(992, 468);
            this.m_dtgRecordDetail.TabIndex = 5001;
            this.m_dtgRecordDetail.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
                                                                                                          this.dgtsStyles});
            // 
            // dgtsStyles
            // 
            this.dgtsStyles.AllowSorting = false;
            this.dgtsStyles.DataGrid = this.m_dtgRecordDetail;
            this.dgtsStyles.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgtsStyles.MappingName = "RecordDetail";
            this.dgtsStyles.ReadOnly = true;
            this.dgtsStyles.RowHeadersVisible = false;
            this.dgtsStyles.RowHeaderWidth = 1;
            // 
            // ctmRecordControl
            // 
            this.ctmRecordControl.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                             this.mniAppend,
                                                                                             this.mniEdit,
                                                                                             this.mniDelete,
                                                                                             this.mniApprove,
                                                                                             this.mniUnApprove,
                                                                                             this.menuItem3,
                                                                                             this.m_mniAddBlank,
                                                                                             this.m_mniDeleteBlank});
            this.ctmRecordControl.Popup += new System.EventHandler(this.ctmRecordControl_Popup);
            // 
            // mniAppend
            // 
            this.mniAppend.Index = 0;
            this.mniAppend.Text = "��Ӽ�¼";
            this.mniAppend.Click += new System.EventHandler(this.mniAppend_Click);
            // 
            // mniEdit
            // 
            this.mniEdit.Index = 1;
            this.mniEdit.Text = "�޸ļ�¼";
            this.mniEdit.Click += new System.EventHandler(this.mniEdit_Click);
            // 
            // mniDelete
            // 
            this.mniDelete.Index = 2;
            this.mniDelete.Text = "ɾ����¼";
            this.mniDelete.Click += new System.EventHandler(this.mniDelete_Click);
            // 
            // mniApprove
            // 
            this.mniApprove.Index = 3;
            this.mniApprove.Text = "��  ��";
            this.mniApprove.Click += new System.EventHandler(this.mniApprove_Click);
            // 
            // mniUnApprove
            // 
            this.mniUnApprove.Index = 4;
            this.mniUnApprove.Text = "��  ��";
            this.mniUnApprove.Click += new System.EventHandler(this.mniUnApprove_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 5;
            this.menuItem3.Text = "-";
            // 
            // m_mniAddBlank
            // 
            this.m_mniAddBlank.Index = 6;
            this.m_mniAddBlank.Text = "��ӿ���";
            this.m_mniAddBlank.Click += new System.EventHandler(this.m_mniAddBlank_Click);
            // 
            // m_mniDeleteBlank
            // 
            this.m_mniDeleteBlank.Index = 7;
            this.m_mniDeleteBlank.Text = "ɾ������";
            this.m_mniDeleteBlank.Click += new System.EventHandler(this.m_mniDeleteBlank_Click);
            // 
            // m_pdcPrintDocument
            // 
            this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
            this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
            this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);
            // 
            // m_trvInPatientDate
            // 
            this.m_trvInPatientDate.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_trvInPatientDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_trvInPatientDate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.m_trvInPatientDate.ForeColor = System.Drawing.SystemColors.Window;
            this.m_trvInPatientDate.HideSelection = false;
            this.m_trvInPatientDate.ImageIndex = -1;
            this.m_trvInPatientDate.Location = new System.Drawing.Point(60, 8);
            this.m_trvInPatientDate.Name = "m_trvInPatientDate";
            this.m_trvInPatientDate.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
                                                                                           new System.Windows.Forms.TreeNode("��Ժʱ��")});
            this.m_trvInPatientDate.SelectedImageIndex = -1;
            this.m_trvInPatientDate.ShowRootLines = false;
            this.m_trvInPatientDate.Size = new System.Drawing.Size(228, 60);
            this.m_trvInPatientDate.TabIndex = 5002;
            this.m_trvInPatientDate.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_trvInPatientDate_AfterSelect);
            // 
            // frmRecordsBase
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1016, 653);
            this.Closing += new CancelEventHandler(frmRecordsBase_Closing);
            this.Controls.Add(this.m_trvInPatientDate);
            this.Controls.Add(this.m_dtgRecordDetail);
            this.Name = "frmRecordsBase";
            this.Text = "��ȡ�����¼���͵ĸ���";
            this.Load += new System.EventHandler(this.frmRecordsBase_Load);
            this.Controls.SetChildIndex(this.m_dtgRecordDetail, 0);
            this.Controls.SetChildIndex(this.m_trvInPatientDate, 0);
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
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgRecordDetail)).EndInit();
            this.ResumeLayout(false);

        }

        void frmRecordsBase_Closing(object sender, CancelEventArgs e)
        {
            if (m_frmCurrentSub != null)
            {
                m_frmCurrentSub.Close();
            }
        }
        #endregion

        #region ctlRichTextBox��˫���ߡ�������������	

        /// <summary>
        /// ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID����ɫ�ȣ���
        /// </summary>
        /// <param name="p_objRichTextBox"></param>
        protected void m_mthSetRichTextBoxAttrib(ctlRichTextBox p_objRichTextBox)
        {
            //������������			
            p_objRichTextBox.m_StrUserID = MDIParent.OperatorID;
            p_objRichTextBox.m_StrUserName = MDIParent.strOperatorName;
            p_objRichTextBox.m_ClrOldPartInsertText = Color.Black;
            p_objRichTextBox.m_ClrDST = Color.Red;
        }

        protected void m_mthSetRichTextBoxAttribInControl(Control p_ctlControl)
        {
            if (p_ctlControl.GetType().FullName == "com.digitalwave.controls.ctlRichTextBox")
            {
                m_mthSetRichTextBoxAttrib((ctlRichTextBox)p_ctlControl);
            }

            if (p_ctlControl.HasChildren)
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextBoxAttribInControl(subcontrol);
                }
            }
        }
        #endregion ctlRichTextBox��˫���ߡ�������������

        protected void frmRecordsBase_Load(object sender, System.EventArgs e)
        {
            frmLoadMethod();
        }
        protected virtual void frmLoadMethod()
        {
            m_mthInitDataTable(m_dtbRecords);
            m_dtgRecordDetail.DataSource = m_dtbRecords;
            m_mthSetRichTextBoxAttribInControl(this);
            m_mthSetQuickKeys();
        }

        #region OverRide Function

        protected override bool m_BlnCanTextChanged
        {
            get
            {
                return true;
            }
        }

        protected override enmFormState m_EnmCurrentFormState
        {
            get
            {
                return enmFormState.NowUser;
            }
        }


        protected override long m_lngSubAddNew()
        {
            return (long)enmOperationResult.DB_Succeed;
        }

        protected override bool m_BlnIsAddNew
        {
            get
            {
                return false;
            }
        }

        protected override long m_lngSubModify()
        {
            return (long)enmOperationResult.DB_Succeed;
        }
        protected override long m_lngSubDelete()
        {
            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        /// <summary>
        ///  ��ս���
        /// </summary>
        protected void m_mthClearAll()
        {
            //��ղ��˻�����Ϣ
            m_mthClearPatientBaseInfo();
            //���õ�ǰ���˱���
            m_objCurrentPatient = null;
            //��յ�ǰ��¼��
            m_mthClearPatientRecordInfo();

            m_trvInPatientDate.Nodes[0].Nodes.Clear();
        }

        /// <summary>
        ///  ��ղ��˼�¼������Ϣ��
        /// </summary>
        protected virtual void m_mthClearPatientRecordInfo()
        {
            //���DataGrid
            m_mthSetDataGridFirstRowFocus();

            if (m_dtgRecordDetail != null)
            {
                m_dtgRecordDetail.CurrentRowIndex = 0;
                m_dtbRecords.Rows.Clear();
            }
            //��ռ�¼����                       
            m_mthClearRecordInfo();

        }

        /// <summary>
        ///  ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected virtual void m_mthClearRecordInfo()
        {
            //��վ����¼���ݣ����Ӵ�������ʵ��
        }

        /// <summary>
        /// ��ʼ���������DataTable��
        /// ע�⣬DataTable�ĵ�һ��Column�����Ǵ�ż�¼ʱ��CreateDate���ַ������ڶ���Column�����Ǵ�ż�¼���͵�intֵ��������Column�����Ǵ�ż�¼��OpenDate���ַ���
        /// </summary>
        /// <param name="p_dtbRecordTable"></param>
        protected virtual void m_mthInitDataTable(DataTable p_dtbRecordTable)
        {
            //���Ӵ�������ʵ��
        }

        /// <summary>
        ///  ���ò��˱���Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
        {
            try
            {
                //�жϲ�����Ϣ�Ƿ�Ϊnull������ǣ�ֱ�ӷ��ء�
                if (p_objSelectedPatient == null)
                    return;

                //��ղ��˼�¼��Ϣ				
                m_mthClearPatientRecordInfo();

                //��¼������Ϣ
                m_objCurrentPatient = p_objSelectedPatient;


                //m_trvInPatientDate.Nodes[0].Nodes.Clear();
                //TreeNode trnNewNode;
                //for(int i1=(p_objSelectedPatient.m_ObjInBedInfo.m_intGetSessionCount()-1);i1>=0;i1--)
                //{
                //    clsInBedSessionInfo objSession=p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(i1);
                //    //trnNewNode = new TreeNode(objSession.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss"));
                //    trnNewNode = new TreeNode(objSession.m_DtmHISInDate.ToString("yyyy-MM-dd HH:mm:ss"));
                //    trnNewNode.Tag = objSession.m_DtmOutDate;//��ų�Ժʱ��.
                //    m_trvInPatientDate.Nodes[0].Nodes.Add(trnNewNode);
                //}
                //m_trvInPatientDate.SelectedNode = null;
                ////ѡ��Ĭ�Ͻڵ�
                //for(int i = 0; i < m_trvInPatientDate.Nodes[0].Nodes.Count; i++)
                //{
                //    if(m_trvInPatientDate.Nodes[0].Nodes[i].Text == p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"))
                //        m_trvInPatientDate.SelectedNode = m_trvInPatientDate.Nodes[0].Nodes[i];
                //}
                //if(m_trvInPatientDate.Nodes[0].Nodes.Count>0 && m_trvInPatientDate.SelectedNode == null)//������Ҫ�˾����ĬȻѡ�����ڵ��¼�
                //    m_trvInPatientDate.SelectedNode=m_trvInPatientDate.Nodes[0].Nodes[0];



                //m_trvInPatientDate.ExpandAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// �����ò��˵Ļ�����Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthOnlySetPatientInfo(clsPatient p_objSelectedPatient)
        {
            //lblSex.Text = p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_ObjPeopleInfo.m_StrSex;
            //lblAge.Text = p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_ObjPeopleInfo.m_StrAge;

        }
        protected virtual void m_mthGetTransDataInfoArr(out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            //string m_strInPatientID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrEMRInPatientID;
            //string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");
            p_objTansDataInfoArr = null;
            if (m_ObjCurrentEmrPatientSession == null)
            {
                return;
            }
            m_objRecordsDomain.m_lngGetTransDataInfoArr(m_ObjCurrentEmrPatientSession.m_strEMRInpatientId, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out p_objTansDataInfoArr);
            // m_objRecordsDomain.m_lngGetTransDataInfoArr(m_ObjCurrentEmrPatientSession.m_strEMRInpatientId, out p_objTansDataInfoArr);

        }
        #region ����
        //��ɽ�������ų��Ļ������
        //protected virtual void m_mthGetTransDataInfoArr(string p_strFormID,out clsTransDataInfo[] p_objTansDataInfoArr)
        //{
        //    p_objTansDataInfoArr = null;
        //    if (m_ObjCurrentEmrPatientSession == null)
        //    {
        //        return;
        //    }
        //    m_objRecordsDomain.m_lngGetTransDataInfoArr(m_ObjCurrentEmrPatientSession.m_strEMRInpatientId, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"),p_strFormID, out p_objTansDataInfoArr);

        //}
        #endregion
        protected virtual void m_trvInPatientDate_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {
                //��ղ��˼�¼��Ϣ				
                m_mthClearPatientRecordInfo();

                if (m_trvInPatientDate.SelectedNode == null || m_trvInPatientDate.SelectedNode == m_trvInPatientDate.Nodes[0] || m_objCurrentPatient == null)
                {
                    return;
                }

                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objCurrentPatient);
                m_objCurrentPatient.m_ObjPeopleInfo = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_ObjPeopleInfo;

                string m_strInPatientID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrEMRInPatientID;
                string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");

                txtInPatientID.Text = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrHISInPatientID;
                m_objCurrentPatient.m_DtmSelectedInDate = DateTime.Parse(m_strInPatientDate);
                m_objCurrentPatient.m_StrHISInPatientID = txtInPatientID.Text;
                m_objCurrentPatient.m_DtmSelectedHISInDate = Convert.ToDateTime(m_trvInPatientDate.SelectedNode.Text);

                #region ��ȡ���˵�����Ժ�ǼǺ�
                string strRegisterID = "";

                //com.digitalwave.PatientManagerService.clsPatientManagerService objServ =
                //    (com.digitalwave.PatientManagerService.clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PatientManagerService.clsPatientManagerService));

                long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRegisterIDByPatient(m_objCurrentPatient.m_StrPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
                if (!string.IsNullOrEmpty(strRegisterID))
                {
                    com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR = strRegisterID;
                    m_objCurrentPatient.m_StrRegisterId = strRegisterID;
                    m_objBaseCurrentPatient.m_StrRegisterId = strRegisterID;
                }
                #endregion

                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                //��ȡ���˼�¼�б�
                clsTransDataInfo[] objTansDataInfoArr;
                m_mthGetTransDataInfoArr(out objTansDataInfoArr);
                //long lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_strInPatientID,m_strInPatientDate,out objTansDataInfoArr);

                if (objTansDataInfoArr == null)
                {
                    return;
                }

                //����¼ʱ��(CreateDate)����
                //modified by  thfzhang 2005-11-12 Σ�ػ�����Ҫ����
                if (this.Name != "frmIntensiveTendMain_FC")
                    m_mthSortTransData(ref objTansDataInfoArr);

                DataTable dtbAddBlank;
                clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
                objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

                //��Ӽ�¼����DataTable
                object[][] objDataArr;
                for (int i1 = 0; i1 < objTansDataInfoArr.Length; i1++)
                {
                    if (dtbAddBlank != null && dtbAddBlank.Rows.Count > 0)
                    {
                        //���Ҽ�¼֮ǰ�з���м�¼,�в������
                        foreach (DataRow drtAdd in dtbAddBlank.Rows)
                        {
                            if (objTansDataInfoArr[i1].m_objRecordContent != null)
                            {
                                if (objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                                //							if(objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse( drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                                {
                                    object[] objBlank = new object[5];
                                    objBlank[1] = 100;
                                    objBlank[2] = drtAdd[2].ToString();
                                    m_dtbRecords.Rows.Add(objBlank);
                                    for (int k3 = 0; k3 < (Int32.Parse(drtAdd[3].ToString()) - 1); k3++)
                                    {
                                        m_dtbRecords.Rows.Add(new object[] { });
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);

                    if (objDataArr == null)
                        continue;
                    //						return;
                    m_dtbRecords.BeginLoadData();
                    for (int j2 = 0; j2 < objDataArr.Length; j2++)
                    {
                        //m_dtbRecords.Rows.Add(objDataArr[j2]);	
                        m_dtbRecords.LoadDataRow(objDataArr[j2], true);
                    }
                    m_dtbRecords.EndLoadData();
                    m_dtgRecordDetail.EnsureVisible(m_dtbRecords.Rows.Count - 1);
                    Application.DoEvents();
                }
                //��ʾdatagrid(Σ�ػ����¼)
                //				DisplayDataToDatagrid(m_dtbRecords);

                //				if(m_dtbRecords.Rows.Count > 0)//�����񲡳̼�¼����ֻ��һ��column����� alex 2003-05-29
                //				{
                //					m_dtgRecordDetail.CurrentCell = new DataGridCell(1,0);
                //					m_dtgRecordDetail.CurrentCell = new DataGridCell(0,0);
                //				}		

                if (m_dtbRecords.Rows.Count == 0 && !m_blnIfNewDeletedRecord)
                {
                    m_mthAutoAddNewRecord();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// ����¼˳��(CreateDate)�������p_objTansDataInfoArr����
        /// </summary>
        /// <param name="p_objTansDataInfoArr"></param>
        protected void m_mthSortTransData(ref clsTransDataInfo[] p_objTansDataInfoArr)
        {
            ArrayList m_arlSort = new ArrayList(p_objTansDataInfoArr);
            m_arlSort.Sort();
            p_objTansDataInfoArr = (clsTransDataInfo[])m_arlSort.ToArray(typeof(clsTransDataInfo));
        }

        /// <summary>
        /// ��ȡ��ӵ�DataTable������
        /// </summary>
        /// <param name="p_objTransDataInfoArr"></param>
        /// <returns></returns>
        protected virtual object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
        {
            //���Ӵ�������
            return null;
        }

        /// <summary>
        /// ��ȡ���̼�¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected virtual clsRecordsDomain m_objGetRecordsDomain()
        {
            //��ȡ���̼�¼�������ʵ�������Ӵ�������ʵ��
            return null;
        }

        //		private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //		{
        //			m_mthPrintPage(e);
        //		}
        //
        //		private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //		{
        //			m_mthBeginPrint(e);
        //		}
        //
        //		private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //		{
        //			m_mthEndPrint(e);
        //		}

        #region ���ⲿ���Ա���ӡ����ʾʵ��.	


        //				System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
        //				private void m_mthfrmLoad()
        //				{	
        //					if(m_pdcPrintDocument==null)
        //						this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
        //					this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
        //					this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
        //					this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);		
        //				}
        private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            objPrintTool.m_mthPrintPage(e);

            if (this.Name != "frmMainGeneralNurseRecord" && this.Name != "frmSubDiseaseTrack" && !m_blnDirectPrint)
            {
                if (ppdPrintPreview != null)
                    while (!ppdPrintPreview.m_blnHandlePrint(e))
                        objPrintTool.m_mthPrintPage(e);
            }
        }

        private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthBeginPrint(e);
        }

        private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthEndPrint(e);
        }


        protected infPrintRecord objPrintTool;
        protected virtual infPrintRecord m_objGetPrintTool()
        {
            return null;
        }
        //clsIntensiveTendMainPrintTool objPrintTool;
        protected virtual void m_mthDemoPrint_FromDataSource()
        {
            objPrintTool = m_objGetPrintTool();//new clsIntensiveTendMainPrintTool();
            if (objPrintTool == null)
            {
                //						clsPublicFunction.ShowInformationMessageBox("������m_objGetPrintTool()������");
                return;
            }
            objPrintTool.m_mthInitPrintTool(null);
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
            else
            {
                m_objBaseCurrentPatient.m_StrHISInPatientID = m_ObjCurrentEmrPatientSession.m_strHISInpatientId;
                m_objBaseCurrentPatient.m_DtmSelectedHISInDate = m_ObjCurrentEmrPatientSession.m_dtmHISInpatientDate;
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_objCurrentPatient.m_DtmSelectedInDate, DateTime.MinValue);
            }

            objPrintTool.m_mthInitPrintContent();

            m_mthStartPrint();
        }

        //				private void m_mthStartPrint()
        //				{			
        //					//PrintPreviewDialog ppdPrintPreview = new PrintPreviewDialog();
        //					ppdPrintPreview.Document = m_pdcPrintDocument;
        //					ppdPrintPreview.ShowDialog();
        //				}
        protected override long m_lngSubPrint()//����ԭ�����е�ͬ����ӡ����
        {
            m_mthDemoPrint_FromDataSource();
            return 1;
        }
        #endregion ���ⲿ���Ա���ӡ����ʾʵ��.
        /// <summary>
        ///  ��ӡ
        /// </summary>
        /// <returns></returns>
        protected long m_lngSubPrint1()
        {
            if (m_objRecordsDomain == new clsRecordsDomain(enmRecordsType.IntensiveTend))
            {

            }

            if (m_objCurrentPatient != null)
            {

                string m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
                string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");
                //��ȡ�������ݺ��״δ�ӡʱ��
                long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_strInPatientID, m_strInPatientDate, out m_objTransDataArr, out m_dtmFirstPrintDateArr, out m_blnIsFirstPrintArr);
                if (lngRes <= 0)
                    return lngRes;

                //����¼ʱ��(CreateDate)���� 
                m_mthSortTransData(ref m_objTransDataArr);

                //���ñ����ݵ���ӡ��
                m_mthSetPrintContent(m_objTransDataArr, m_dtmFirstPrintDateArr);

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
        /// <param name="p_objTransDataArr"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        protected virtual void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
            DateTime[] p_dtmFirstPrintDateArr)
        {
            //ȱʡ�����κζ������Ӵ����������ṩ������
        }

        /// <summary>
        ///  ��ʼ����ӡ����
        /// </summary>
        protected virtual void m_mthInitPrintTool()
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
            //��ʼ�����ݰ������д�ӡʹ�õ��ı��������塢���ʡ���ˢ����ӡ��ȡ�
        }

        /// <summary>
        ///  �ͷŴ�ӡ����
        /// </summary>
        protected virtual void m_mthDisposePrintTools()
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
            //�ͷ����ݰ�����ӡʹ�õ������塢���ʡ���ˢ��ʹ��ϵͳ��Դ�ı�����
        }

        /// <summary>
        ///  ��ʼ��ӡ��
        /// </summary>
        /// 
        private PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();
        protected virtual void m_mthStartPrint()
        {
            //ȱʡʹ�ô�ӡԤ�����Ӵ��������ṩ�µ�ʵ��			
            if (m_blnDirectPrint)
            {
                clsContinuousPrintTool objConinuePrintTool = objPrintTool as clsContinuousPrintTool;
                if (objConinuePrintTool != null)
                {
                    objConinuePrintTool.m_mthSetContinuePrint(m_objBaseCurrentPatient.m_StrInPatientID, m_trvInPatientDate.SelectedNode.Text);
                    if (objConinuePrintTool.m_blnHavePrintAllRecords())
                    {
                        clsPublicFunction.ShowInformationMessageBox("��һ�δ�ӡ�Ѵ�ӡ��ȫ����¼���������´�ӡ���밴��ӡԤ����");
                        return;
                    }
                }
                m_pdcPrintDocument.Print();
            }
            else
            {
                ppdPrintPreview.Document = m_pdcPrintDocument;
                ppdPrintPreview.ShowDialog();
            }
        }

        /// <summary>
        ///  ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected void m_mthBeginPrint(PrintEventArgs p_objPrintArg)
        {
            m_mthBeginPrintSub(p_objPrintArg);
        }

        /// <summary>
        ///  ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected virtual void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
        }

        /// <summary>
        ///  ��ӡҳ
        /// </summary>
        /// <param name="p_objPrintPageArg"></param>
        protected void m_mthPrintPage(PrintPageEventArgs p_objPrintPageArg)
        {
            m_mthPrintPageSub(p_objPrintPageArg);
        }

        /// <summary>
        ///  ��ӡҳ
        /// </summary>
        /// <param name="p_objPrintPageArg"></param>
        protected virtual void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            //ȱʡ�����κζ������Ӵ����������ṩ����
        }

        /// <summary>
        ///  ��ӡ����ʱ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected void m_mthEndPrint(PrintEventArgs p_objPrintArg)
        {
            if (m_objCurrentPatient == null) return;//added-Jacky-2003-6-4
                                                    //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            string m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");
            if (!p_objPrintArg.Cancel && m_blnIsFirstPrintArr != null)
            {
                ArrayList arlRecordType = new ArrayList();
                ArrayList arlOpenDate = new ArrayList();
                int intUpdateIndex = -1;//��û���κμ�¼
                for (int i = 0; i < m_blnIsFirstPrintArr.Length; i++)
                {
                    if (m_blnIsFirstPrintArr[i])
                    {
                        //���¼�¼��ֻ��ʹ���µ��״δ�ӡʱ����Ϊ��Ч�����������
                        //��ż�¼����
                        arlRecordType.Add(m_objTransDataArr[i].m_intFlag);
                        //��ż�¼��OpenDate
                        arlOpenDate.Add(m_objTransDataArr[i].m_objRecordContent.m_dtmOpenDate);
                        intUpdateIndex = i;
                    }
                }

                if (intUpdateIndex >= 0)
                {
                    m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_strInPatientID, m_strInPatientDate, (int[])arlRecordType.ToArray(typeof(int)), (DateTime[])arlOpenDate.ToArray(typeof(DateTime)), m_dtmFirstPrintDateArr[intUpdateIndex]);
                }
                m_objTransDataArr = null;
                m_blnIsFirstPrintArr = null;
            }
            m_mthEndPrintSub(p_objPrintArg);
        }

        /// <summary>
        ///  ��ӡ����ʱ�Ĳ���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        protected virtual void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            //���Ӵ����������ṩ����
        }

        /// <summary>
        /// ������Ϊ�麯����Ĭ�ϼ̳б�����������Ӵ��嶼ִ�б��麯����
        /// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
        /// </summary>
        /// <param name="p_intRecordType"></param>
        protected virtual void m_mthAddNewRecord(int p_intRecordType)
        {
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
            {
                MDIParent.ShowInformationMessageBox("����ѡ��һ������!");
                return;
            }
            if (!m_blnCanShowNewForm)
            {
                if (m_frmCurrentSub != null)
                {
                    m_frmCurrentSub.Activate();
                    m_frmCurrentSub.WindowState = FormWindowState.Normal;
                }
                return;
            }

            //��ȡ��Ӽ�¼�Ĵ���
            frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType);
            frmAddNewForm.m_blnIsAddNew = true;

            if (frmAddNewForm == null)
                return;

            //��ӿ���
            //������˳�� // modify by tfzhang at 2005-12-8 19:13
            //m_mthShowSubForm(frmAddNewForm,p_intRecordType,false);
            frmAddNewForm.m_mthSetDiseaseTrackInfoForAddNew(m_objCurrentPatient);

            m_mthShowSubForm(frmAddNewForm, p_intRecordType, false);

            MDIParent.s_ObjSaveCue.m_mthAddFormStatus(frmAddNewForm);
        }

        protected void m_mthShowSubForm(frmDiseaseTrackBase p_frmSubForm, int p_intRecordType, bool p_blnRemoveOld)
        {
            p_frmSubForm.Closed += new EventHandler(m_mthSubFormClosed);
            p_frmSubForm.MaximizeBox = true;
            m_intCurrentFormType = p_intRecordType;
            m_blnCurrentFormRemoveOld = p_blnRemoveOld;
            m_blnCanShowNewForm = false;
            m_frmCurrentSub = p_frmSubForm;
            //p_frmSubForm.TopMost = true;
            p_frmSubForm.Show(this);
            p_frmSubForm.Activate();
        }

        protected frmDiseaseTrackBase m_frmCurrentSub = null;
        public frmDiseaseTrackBase m_FrmCurrentSub
        {
            get
            {
                return m_frmCurrentSub;
            }
            set
            {
                m_frmCurrentSub = value;
            }
        }
        private int m_intCurrentFormType;
        private bool m_blnCurrentFormRemoveOld;
        protected bool m_blnCanShowNewForm = true;
        private void m_mthSubFormClosed(object p_objSender, EventArgs p_objArg)
        {
            frmDiseaseTrackBase frmAddNewForm = (frmDiseaseTrackBase)p_objSender;
            //��ʾ����
            if (frmAddNewForm.DialogResult == DialogResult.Yes)
            {
                m_mthHandleSubFormClosedWithYes(frmAddNewForm);
            }

            m_blnCanShowNewForm = true;
            m_frmCurrentSub = null;
        }
        protected virtual void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
        {
            //��ȡ�û���ӵ�����
            clsDiseaseTrackInfo objTrackInfo = p_frmSubForm.m_objGetDiseaseTrackInfo();
            clsTransDataInfo objDataInfo = new clsTransDataInfo();
            objDataInfo.m_intFlag = m_intCurrentFormType;
            objDataInfo.m_objRecordContent = objTrackInfo.m_ObjRecordContent;

            //�������ݵ�DataTable
            object[][] objDataArr = m_objGetRecordsValueArr(objDataInfo);

            if (m_blnCurrentFormRemoveOld)
            {
                //��DataTableɾ������
                m_mthRemoveDataFromDataTable(m_intCurrentFormType, objTrackInfo.m_ObjRecordContent.m_dtmOpenDate);
            }

            //������ݵ�DataTable
            m_mthAddDataToDataTable(objDataArr, objTrackInfo.m_ObjRecordContent.m_dtmCreateDate);

        }

        /// <summary>
        ///  ��ȡ�û�ѡ��ļ�¼�Ŀ�ʼ��λ��
        /// </summary>
        /// <param name="p_intSelectRowIndex">��������</param>
        /// <returns></returns>
        protected virtual int m_intGetRecordStartRow(int p_intSelectRowIndex)
        {
            //��p_intSelectRow��ʼ���Ӻ���ǰѭ��DataTable
            //�����ǰ��¼�ĵ�һ���ֶβ�Ϊ��
            //��������
            for (int i1 = p_intSelectRowIndex; i1 >= 0; i1--)
            {
                if (m_dtbRecords.Rows[i1][1].ToString() != "")
                {
                    return i1;
                }
            }
            return -1;
        }

        /// <summary>
        ///  ��ȡ�û�ѡ��ļ�¼�Ŀ�ʼ��λ��  ���̼�¼��
        /// </summary>
        /// <param name="p_intSelectRowIndex">��������</param>
        /// <returns></returns>
        protected virtual int m_intGetRecordStartRows(int p_intSelectRowIndex)
        {
            //��p_intSelectRow��ʼ���Ӻ���ǰѭ��DataTable
            //�����ǰ��¼�ĵ�һ���ֶβ�Ϊ��
            //��������
            for (int i1 = p_intSelectRowIndex; i1 >= 0; i1--)
            {
                if (m_dtbRecords.Rows[i1][0].ToString() != "")
                {
                    return i1;
                }
            }
            return -1;
        }
        /// <summary>
        /// ������Ϊ�麯����Ĭ�ϼ̳б�����������Ӵ��嶼ִ�б��麯����
        /// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_dtmCreateRecordTime"></param>
        protected virtual void m_mthModifyRecord(int p_intRecordType,
            DateTime p_dtmOpenRecordTime)
        {
            if (!m_blnCanShowNewForm)
            {
                if (m_frmCurrentSub != null)
                {
                    m_frmCurrentSub.Activate();
                    m_frmCurrentSub.WindowState = FormWindowState.Normal;
                }
                return;
            }

            //��ȡ��Ӽ�¼�Ĵ���
            frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType);
            frmAddNewForm.m_blnIsAddNew = false;
            //��鵱ǰ�û��Ƿ���Ȩ�޸ļ�¼����
            if (!m_lngCanYouDoIt())
            {
                clsPublicFunction.ShowInformationMessageBox("�õ��ѱ��ϼ���˹�������Ȩ�޸ģ�");
                return;
            }
            //������˳�� // modify by tfzhang at 2005-12-8 19:13
            //m_mthShowSubForm(frmAddNewForm,p_intRecordType,true);

            frmAddNewForm.m_mthSetDiseaseTrackInfo(m_objCurrentPatient, p_dtmOpenRecordTime);
            m_mthShowSubForm(frmAddNewForm, p_intRecordType, true);


            MDIParent.s_ObjSaveCue.m_mthAddFormStatus(frmAddNewForm);
        }

        /// <summary>
        /// ������Ϊ�麯����Ĭ�ϼ̳б�����������Ӵ��嶼ִ�б��麯����
        /// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_dtmCreateRecordTime"></param>
        protected virtual void m_mthGetDeletedRecord(int p_intRecordType,
            DateTime p_dtmCreateRecordTime)
        {
            if (!m_blnCanShowNewForm)
            {
                if (m_frmCurrentSub != null)
                {
                    //��ʾ��ɾ����¼���ر�ԭ�д���ʱ����ʾ����
                    m_frmCurrentSub.m_trvCreateDate.SelectedNode = m_frmCurrentSub.m_trvCreateDate.Nodes[0];
                    m_frmCurrentSub.Close();
                }
            }

            //��ȡ��Ӽ�¼�Ĵ���
            frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(p_intRecordType);

            if (frmAddNewForm == null)
                return;

            frmAddNewForm.m_mthSetDeletedDiseaseTrackInfo(m_objCurrentPatient, p_dtmCreateRecordTime);

            m_mthShowSubForm(frmAddNewForm, p_intRecordType, false);

            //MDIParent.s_ObjSaveCue.m_mthAddFormStatus(frmAddNewForm);
            MDIParent.s_ObjSaveCue.m_mthRemoveForm(frmAddNewForm);
            frmAddNewForm.TopMost = true;
        }


        //infPrintRecord objPrintTool;
        protected override void m_mthSubPreviewInactiveRecord(IWin32Window p_infOwner, clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            if (p_objSelectedValue != null)
            {
                using (frmDiseaseTrackBase frmPreForm = m_frmGetRecordForm(-1))
                {
                    if (frmPreForm == null)
                        return;

                    frmPreForm.m_mthSetDeletedDiseaseTrackInfo(m_objCurrentPatient, p_objSelectedValue.m_DtmOpenDate);
                    frmPreForm.m_mthSetReadOnly();
                    frmPreForm.ShowDialog(p_infOwner);
                }
            }
        }


        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objDataArr"></param>
        protected void m_mthDeleteRecord(int p_intRecordType,
            object[] p_objDataArr)
        {
            //��ȡ��Ҫ����
            clsTrackRecordContent objContent = m_objGetRecordMainContent(p_intRecordType, p_objDataArr);
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_strRegisterID = m_objCurrentPatient.m_StrRegisterId;
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_strDeActivedOperatorID = MDIParent.OperatorID;
            objContent.m_dtmDeActivedDate = DateTime.Parse(m_objPublicDomain.m_strGetServerTime());

            //�޸ļ�¼
            clsPreModifyInfo objModifyInfo = null;
            //����ɾ������
            long lngRes = m_objRecordsDomain.m_lngDeleteRecord(p_intRecordType, objContent, out objModifyInfo);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    clsEMR_CaseAuditVO objAuditVO = m_objGetAuditCase();
                    objAuditVO.m_dtmCREATEDATE = objContent.m_dtmOpenDate;
                    m_mthDelAuditCase(objAuditVO);

                    //ɾ��ѡ�нڵ�		
                    m_mthRemoveDataFromDataTable(p_intRecordType, objContent.m_dtmOpenDate);
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
                default:
                    break;
            }
        }

        /// <summary>
        /// ������ݵ�DataTable
        /// </summary>
        /// <param name="p_objDataArr"></param>
        /// <param name="p_dtmCreateRecordTime"></param>
        protected void m_mthAddDataToDataTable(object[][] p_objDataArr,
            DateTime p_dtmCreateRecordTime)
        {
            //���Ҳ����
            //ѭ��DataTable�ļ�¼����ȡ��¼�����ڣ���һ�ֶΣ�
            //����м�¼����
            //�Ƚ�����������p_dtmCreateDate
            //����������ڱ�p_dtmCreateDate��
            //�����м�¼ǰ��Ӽ�¼�����飩������

            //û���ҵ���p_dtmCreateDate������ڣ���DataTable�����	
            if (p_objDataArr == null)
                return;

            int m_intInsertIndex = -1;
            string m_strRecordTime = null;
            DataRow m_dtrNewRow;
            for (int i1 = 0; i1 < m_dtbRecords.Rows.Count; i1++)
            {
                if (m_dtbRecords.Rows[i1][0].ToString() != "")
                {
                    m_strRecordTime = m_dtbRecords.Rows[i1][0].ToString();
                    if (DateTime.Parse(m_strRecordTime) > p_dtmCreateRecordTime)
                    {
                        m_intInsertIndex = i1;
                        break;
                    }
                }
            }
            if (m_intInsertIndex < 0)//û���ҵ���p_dtmOpenRecordTime������ڣ���DataTable�����		
            {
                for (int i1 = 0; i1 < p_objDataArr.Length; i1++)
                {
                    //m_dtbRecords.Rows.Add(p_objDataArr[i1]);
                    m_dtrNewRow = m_dtbRecords.NewRow();
                    m_dtrNewRow.ItemArray = p_objDataArr[i1];
                    m_dtbRecords.Rows.Add(m_dtrNewRow);
                }
            }
            else//���򣬽�p_dtmCreateDate ֮��ļ�¼�ŵ��ڴ���,����������ļ�¼��Ȼ����ڴ��еļ�¼������ӻ�ȥ
            {
                if (m_dtbTempTable == null)
                {
                    m_dtbTempTable = m_dtbRecords.Clone();
                }
                while ((m_intInsertIndex < m_dtbRecords.Rows.Count))//��p_dtmCreateDate ֮��ļ�¼�ŵ��ڴ���
                {
                    m_mthSetDataGridFirstRowFocus();
                    m_dtrNewRow = m_dtbTempTable.NewRow();
                    m_dtrNewRow.ItemArray = m_dtbRecords.Rows[m_intInsertIndex].ItemArray;
                    m_dtbTempTable.Rows.Add(m_dtrNewRow);
                    m_dtbRecords.Rows.RemoveAt(m_intInsertIndex);
                }
                for (int i1 = 0; i1 < p_objDataArr.Length; i1++)//�����ļ�¼
                {
                    m_dtrNewRow = m_dtbRecords.NewRow();
                    m_dtrNewRow.ItemArray = p_objDataArr[i1];
                    m_dtbRecords.Rows.Add(m_dtrNewRow);
                }
                for (int i1 = 0; i1 < m_dtbTempTable.Rows.Count; i1++)//���ڴ��еļ�¼������ӻ�ȥ
                {
                    m_dtrNewRow = m_dtbRecords.NewRow();
                    m_dtrNewRow.ItemArray = m_dtbTempTable.Rows[i1].ItemArray;
                    m_dtbRecords.Rows.Add(m_dtrNewRow);
                }
                if (m_dtbTempTable != null)
                {
                    m_dtbTempTable.Rows.Clear();
                }
            }
            if (m_dtbRecords != null && m_dtbRecords.Rows.Count > 0)
            {
                m_dtgRecordDetail.EnsureVisible(m_dtbRecords.Rows.Count - 1);
            }
        }

        /// <summary>
        /// ������Ϊ�麯����Ĭ�ϼ̳б�����������Ӵ��嶼ִ�б��麯����
        /// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_dtmCreateRecordTime"></param>
        protected virtual void m_mthRemoveDataFromDataTable(int p_intRecordType,
            DateTime p_dtmOpenDate)
        {
            //����p_dtmCreateDate��p_intRecordType���Ҷ�Ӧ�ļ�¼
            int intDeleteIndex = -1;
            //ѭ��DataTable�ļ�¼����ȡ��¼�����ڣ���һ�ֶΣ��ͼ�¼���ͣ��ڶ����ֶΣ�
            //����м�¼���ںͼ�¼����
            //���������p_dtmCreateDate��ͬ����¼������p_intRecordType��ͬ
            //intDeleteIndex = �±�;
            //����ѭ��
            for (int i1 = 0; i1 < m_dtbRecords.Rows.Count; i1++)
            {
                if (m_dtbRecords.Rows[i1][2].ToString() != "" && m_dtbRecords.Rows[i1][1].ToString() != "")
                {
                    if (DateTime.Parse(m_dtbRecords.Rows[i1][2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") == p_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") && (int)m_dtbRecords.Rows[i1][1] == p_intRecordType)
                    {
                        intDeleteIndex = i1;
                        break;
                    }
                }
            }

            if (intDeleteIndex >= 0)
            {
                //����intDeleteIndexѭ��ɾ����¼
                //ɾ��intDeleteIndex��

                //���intDeleteIndexû�г���DataTable��Χ
                //��ȡ��¼���鿴���޼�¼������
                //����У�˵����¼�Ѿ�ɾ����ϣ�����

                //����ޣ�����ɾ����
                //intDeleteIndex������Χ(>=DataTable.Rows.Count)������

                m_mthSetDataGridFirstRowFocus();

                m_dtbRecords.Rows.RemoveAt(intDeleteIndex);

                //���ֻ�и�����¼ֻ��һ������ʱ��������ִ�д�ѭ��
                while ((intDeleteIndex < m_dtbRecords.Rows.Count) && (m_dtbRecords.Rows[intDeleteIndex][2].ToString() == ""))
                {
                    m_mthSetDataGridFirstRowFocus();
                    m_dtbRecords.Rows.RemoveAt(intDeleteIndex);
                }
            }
        }

        /// <summary>
        /// ʹ��DataGrid�ĵ�һ�л�ý���
        /// </summary>
        protected void m_mthSetDataGridFirstRowFocus()
        {
            if (this.IsHandleCreated)

                //modified, Jacky-2003-6-12
                m_dtgRecordDetail.CurrentCell = new DataGridCell(m_dtbRecords.Rows.Count, 0);
        }
        /// <summary>
        /// ��ȡ������Ӻ��޸ģ���¼�Ĵ��塣
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <returns></returns>
        protected virtual frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
        {
            //���Ӵ�������ʵ��
            return null;
        }

        /// <summary>
        /// ��ȡ��¼����Ҫ��Ϣ�������ȡ����CreateDate,OpenDate,LastModifyDate��
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objDataArr"></param>
        /// <returns></returns>
        protected virtual clsTrackRecordContent m_objGetRecordMainContent(int p_intRecordType,
            object[] p_objDataArr)
        {
            //���Ӵ�������ʵ��
            return null;
        }

        protected virtual void mniEdit_Click(object sender, System.EventArgs e)
        {
            //enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF),this.GetType().Name);
#if FunctionPrivilege
						if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.AddOrModify))
						{
							clsPublicFunction.s_mthShowNotPermitMessage();
							return;
						}			
#endif

            //			if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,this,enmFormState.NowUser)
            //				== enmDBControlCheckResult.Disable)
            //			{
            //				clsPublicFunction.s_mthShowNotPermitMessage();
            //				return;
            //			}

            if (!m_blnCheckDataGridCurrentRow())
                return;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
                if (intSelectedRecordStartRow < 0)
                    return;

                string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
                int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];
                if (intRecordType == 100)
                {
                    clsPublicFunction.ShowInformationMessageBox("�����ǿ��У���ѡ��һ����¼");
                    return;
                }
                else if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO != "450101001" && intRecordType == (int)enmDiseaseTrackType.FirstIllnessNote)
                    intRecordType = (int)enmDiseaseTrackType.FirstIllnessNote_F2;
                m_mthModifyRecord(intRecordType, DateTime.Parse(strOpenDate));
            }
            catch (Exception exp)//��׽δ֪���� Alex 2003-7-31
            {
                string strtemp = exp.Message;
                MessageBox.Show(exp.Message.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void mniDelete_Click(object sender, System.EventArgs e)
        {
            if (m_ObjCurrentEmrPatientSession == null)
            {
                return;
            }

            enmPrivilegeSF enmSF = (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF), this.GetType().Name);
#if FunctionPrivilege
						if(!clsPublicFunction.s_blnCheckCurrentPrivilege(enmSF,enmPrivilegeOperation.Delete))
						{
							clsPublicFunction.s_mthShowNotPermitMessage();
							return;
						}			
#endif


            //if(m_objCurrentContext.m_ObjControl.m_enmDeleteCheck(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,this,enmFormState.NowUser)
            //    == enmDBControlCheckResult.Disable)
            //{
            //    clsPublicFunction.s_mthShowNotPermitMessage();
            //    return;
            //}	

            if (m_blnReadOnly)
            {
                clsPublicFunction.ShowInformationMessageBox("�˲���Ϊֻ��������ɾ����");
                return;
            }

            if (!m_blnCheckDataGridCurrentRow())
                return;
            if (!m_lngCanYouDoIt())
            {
                clsPublicFunction.ShowInformationMessageBox("�õ��ѱ��ϼ���˹�������Ȩɾ����");
                return;
            }


            int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
            if (intSelectedRecordStartRow < 0)
                return;
            string strCreateDate = m_dtbRecords.Rows[intSelectedRecordStartRow][0].ToString();
            int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];
            if (intRecordType == 100)
                return;
            if (!clsPublicFunction.s_blnAskForDelete())
                return;

            //��ȡ������ID
            clsTrackRecordContent objContent = m_objGetRecordMainContent(intRecordType, m_dtbRecords.Rows[intSelectedRecordStartRow].ItemArray);

            string strCreateUserID;
            try
            {
                if (objContent != null && objContent.m_strCreateUserID != null)
                    strCreateUserID = objContent.m_strCreateUserID.Trim();
                else
                    strCreateUserID = "";
            }
            catch (Exception)
            {

                throw;
            }


            //Ȩ���ж�
            string strDeptIDTemp = m_ObjCurrentEmrPatientSession.m_strAreaId;
            bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, strCreateUserID, clsEMRLogin.LoginEmployee, intFormType);
            if (!blnIsAllow)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                m_mthDeleteRecord(intRecordType, m_dtbRecords.Rows[intSelectedRecordStartRow].ItemArray);

            }
            finally
            {
                this.Cursor = Cursors.Default;

            }
        }



        /// <summary>
        /// ����DataGrid�ڵĿؼ��������¼����Ҽ��˵�
        /// </summary>
        /// <param name="p_objControl"></param>
        protected void m_mthSetControl(DataGridTextBoxColumn p_objControl)
        {
            Control m_objControl;
            m_objControl = (DataGridTextBox)p_objControl.TextBox;
            m_objControl.ContextMenu = ctmRecordControl;
            m_objControl.DoubleClick += new EventHandler(m_mthTxtDoubleClick);
        }

        /// <summary>
        /// ����DataGrid�ڵĿؼ��������¼����Ҽ��˵�
        /// </summary>
        /// <param name="p_objControl"></param>
        protected void m_mthSetControl(com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox p_objControl)
        {
            p_objControl.m_RtbBase.ContextMenu = ctmRecordControl;
            p_objControl.m_RtbBase.MouseDown += new MouseEventHandler(cltDataGridDSTRichTextBox_MouseDown);
        }

        protected void m_mthSetControl(com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox[] p_objControlArr)
        {
            foreach (com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox obj in p_objControlArr)
            {
                obj.m_RtbBase.ContextMenu = ctmRecordControl;
                obj.m_RtbBase.MouseDown += new MouseEventHandler(cltDataGridDSTRichTextBox_MouseDown);
            }
        }

        /// <summary>
        /// ˫��DataGrid�ڵĿؼ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cltDataGridDSTRichTextBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!m_blnCheckDataGridCurrentRow())
                return;
            try
            {
                if (e.Clicks > 1)
                {
                    int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
                    if (intSelectedRecordStartRow < 0)
                        return;
                    string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
                    int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];
                    m_mthModifyRecord(intRecordType, DateTime.Parse(strOpenDate));
                }
            }
            catch//��׽δ֪���� Alex 2003-7-31
            {

            }
        }

        /// <summary>
        /// ˫��DataGrid�ڵĿؼ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void m_mthTxtDoubleClick(object sender, EventArgs e)
        {
            if (!m_blnCheckDataGridCurrentRow())
                return;
            try
            {
                int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
                if (intSelectedRecordStartRow < 0)
                    return;
                string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
                int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];
                m_mthModifyRecord(intRecordType, DateTime.Parse(strOpenDate));
            }
            catch (Exception exp)//��׽δ֪���� Alex 2003-7-31
            {
                string strErrorMessage = exp.Message;
            }
        }

        /// <summary>
        /// ����֮ǰ�ж�DataGrid��DataTable�Ĺ�ϵ
        /// </summary>
        /// <returns></returns>
        protected virtual bool m_blnCheckDataGridCurrentRow()
        {
            try
            {
                if (m_dtbRecords.Rows.Count <= 0)
                    return false;
                if (m_dtgRecordDetail.CurrentCell.RowNumber >= m_dtbRecords.Rows.Count)
                {
                    return false;
                }
                return true;
            }
            catch//��׽δ֪���� Alex 2003-7-31
            {
                return false;
            }

        }

        #region DataControl  PublicFunction
        public void Save() { m_mthSave(); }
        public void Delete()
        {
            //ָ��������Ϊҽ������վ
            intFormType = 1;
            m_mthDelete();
        }
        public void Display() { }
        public void Display(string cardno, string sendcheckdate) { }
        public void Print() { m_lngPrint(); }
        public void Copy() { m_lngCopy(); }
        public void Cut() { m_lngCut(); }
        public void Paste() { m_lngPaste(); }
        public void Redo() { }
        public void Undo() { }
        public void Verify()
        {
            //long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
        }
        #endregion DataControl]

        protected virtual void m_mthSave()
        {
            return;
        }

        protected virtual void m_mthDelete()
        {
            return;
        }

        #region ��Ӽ��̿�ݼ�
        protected void m_mthSetQuickKeys()
        {
            m_mthSetControlEvent(this);
        }

        private void m_mthSetControlEvent(Control p_ctlControl)
        {
            #region ���õݹ���ã���ȡ���������н����¼�,Jacky-2003-2-21	
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName != "Lable" && strTypeName != "Button")
            {
                p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
                if (p_ctlControl.HasChildren && strTypeName != "DateTimePicker" && strTypeName != "ctlComboBox")
                {
                    foreach (Control subcontrol in p_ctlControl.Controls)
                    {
                        string strSubTypeName = subcontrol.GetType().Name;
                        if (strSubTypeName != "Lable" && strSubTypeName != "Button")
                            m_mthSetControlEvent(subcontrol);
                    }
                }
            }
            #endregion
        }

        protected virtual void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {//F1 112  ����, F2 113 Save��F3  114 Del��F4 115 Print��F5 116 Refresh��F6 117 Search
                case Keys.Enter:// enter				
                    break;
                case Keys.Up:
                    break;
                case Keys.F2://save
                    this.Save();
                    break;
                case Keys.F3://del
                    break;
                case Keys.F4://print
                    this.m_lngSubPrint();
                    break;
                case Keys.F5://refresh
                    m_mthClearAll();
                    break;
                case Keys.F6://Search
                    break;
            }
        }

        #endregion

        private void ctmRecordControl_Popup(object sender, System.EventArgs e)
        {
            //��ʱ����Ȩ��У�� ��������������������bhuang��
            bool blnEnable = true;
            //			if(m_objCurrentPatient !=null && m_trvInPatientDate.SelectedNode.Tag !=null && m_trvInPatientDate.SelectedNode.Tag.GetType().Name=="DateTime")
            //			{
            //				//Ϊ1900-1-1�ĳ�Ժʱ�����û�г�Ժ,�����޸ļ�¼
            //				if( (DateTime)m_trvInPatientDate.SelectedNode.Tag ==DateTime.Parse("1900-1-1") )
            //					blnEnable=true;
            //				else
            //				{//��Ժʱ����6Сʱ֮�ڵ�,Ҳ�����޸ļ�¼
            //					TimeSpan span=DateTime.Parse(m_objPublicDomain.m_strGetServerTime()) - ((DateTime)m_trvInPatientDate.SelectedNode.Tag);
            //					if(span.TotalHours < 24*7)//����д��<=,��Ϊ����û�м�������
            //						blnEnable=true;
            //				}
            //			}
            //
            //			if(!m_blnCanShowNewForm)
            //				blnEnable = false;
            //
            mniAppend.Enabled = blnEnable;
            mniEdit.Enabled = blnEnable;
            mniDelete.Enabled = blnEnable;
            mniApprove.Enabled = blnEnable;
            mniUnApprove.Enabled = blnEnable;
        }

        void mniAppend_Click(object sender, System.EventArgs e)
        {
            //			if(m_objCurrentContext.m_ObjControl.m_enmAddNewCheck(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,this,enmFormState.NowUser)
            //				== enmDBControlCheckResult.Disable)
            //			{
            //				clsPublicFunction.s_mthShowNotPermitMessage();
            //				return;
            //			}
        }

        #region ���
        public clsApprove_FlowDomain m_objApprove = new clsApprove_FlowDomain();

        protected virtual bool m_lngCanYouDoIt()
        {
            int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
            if (intSelectedRecordStartRow < 0)
                return false;

            string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
            int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];

            //��ȡ��¼�Ĵ���
            string strFormID = m_strGetFormID(intRecordType);

            return m_objApprove.lngCanYouDoIt(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID, strFormID, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strOpenDate, m_ObjCurrentEmrPatientSession.m_strAreaId);
        }

        protected string m_strGetFormID(int p_intRecordType)
        {
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                #region ��ʿ����վ
                case enmDiseaseTrackType.GeneralNurseRecord:
                    return ((int)enmPrivilegeSF.frmGeneralNurseRecord).ToString();
                case enmDiseaseTrackType.WatchItem:
                    return ((int)enmPrivilegeSF.frmSubWatchItemRecord).ToString();
                case enmDiseaseTrackType.IntensiveTend:
                    return ((int)enmPrivilegeSF.frmIntensiveTend).ToString();
                case enmDiseaseTrackType.ICUIntensiveTend:
                    return ((int)enmPrivilegeSF.frmSubICUIntensiveTend).ToString();
                case enmDiseaseTrackType.GeneralNurseRecord_GX:
                    return ((int)enmPrivilegeSF.frmGeneralNurseRecord_GX).ToString();
                #endregion

                #region ���̼�¼
                case enmDiseaseTrackType.GeneralDisease:
                    return ((int)enmPrivilegeSF.frmGeneralDisease).ToString();
                case enmDiseaseTrackType.HandOver:
                    return ((int)enmPrivilegeSF.frmHandOver).ToString();
                case enmDiseaseTrackType.TakeOver:
                    return ((int)enmPrivilegeSF.frmTakeOver).ToString();
                case enmDiseaseTrackType.Consultation:
                    return ((int)enmPrivilegeSF.frmConsultation).ToString();
                case enmDiseaseTrackType.Convey:
                    return ((int)enmPrivilegeSF.frmConvey).ToString();
                case enmDiseaseTrackType.TurnIn:
                    return ((int)enmPrivilegeSF.frmTurnIn).ToString();
                case enmDiseaseTrackType.DiseaseSummary:
                    return ((int)enmPrivilegeSF.frmDiseaseSummary).ToString();
                case enmDiseaseTrackType.CheckRoom:
                    return ((int)enmPrivilegeSF.frmCheckRoom).ToString();
                case enmDiseaseTrackType.CaseDiscuss:
                    return ((int)enmPrivilegeSF.frmCaseDiscuss).ToString();
                case enmDiseaseTrackType.BeforeOperationDiscuss:
                    return ((int)enmPrivilegeSF.frmBeforeOperationDiscuss).ToString();
                case enmDiseaseTrackType.DeadCaseDiscuss:
                    return ((int)enmPrivilegeSF.frmDeadCaseDiscuss).ToString();
                case enmDiseaseTrackType.DeathCaseDiscuss:
                    return ((int)enmPrivilegeSF.frmDeathCaseDiscuss).ToString();
                case enmDiseaseTrackType.AfterOperation:
                    return ((int)enmPrivilegeSF.frmAfterOperation).ToString();
                case enmDiseaseTrackType.Dead:
                    return ((int)enmPrivilegeSF.frmDeadRecord).ToString();
                case enmDiseaseTrackType.Death:
                    return ((int)enmPrivilegeSF.frmDeathRecord).ToString();
                case enmDiseaseTrackType.OutHospital:
                    return ((int)enmPrivilegeSF.frmOutHospital).ToString();
                case enmDiseaseTrackType.Save:
                    return ((int)enmPrivilegeSF.frmSaveRecord).ToString();
                    #endregion
            }

            return "";
        }

        protected virtual void mniApprove_Click(object sender, System.EventArgs e)
        {
            if (!m_blnCheckDataGridCurrentRow())
                return;

            int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
            if (intSelectedRecordStartRow < 0)
                return;

            string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
            int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];

            if (this.GetType().Name == "frmWatchItemTrack")
            {
                intRecordType = 3;
            }
            //��ȡ��¼�Ĵ���
            string strFormID = m_strGetFormID(intRecordType);

            long lngEff = 0;
            lngEff = m_objApprove.lngApproveDocument(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID, strFormID, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strOpenDate, m_ObjCurrentEmrPatientSession.m_strAreaId, ref lngEff);//((clsDepartment)this.m_cboDept.SelectedItem).m_StrDeptID,ref lngEff);

            #region ���ݽ������ͬ�Ĵ���
            switch ((enmApproveResult)lngEff)
            {
                case enmApproveResult.DB_Succeed:
                    clsPublicFunction.ShowInformationMessageBox("��˳ɹ�!");
                    break;
                case enmApproveResult.System_Not_Define:
                    clsPublicFunction.ShowInformationMessageBox("ϵͳû�ж���õ���������̣�Ӧ�������ݿ��ж��壩!");
                    break;
                case enmApproveResult.Document_Has_Been_Finished:
                    clsPublicFunction.ShowInformationMessageBox("���Ѿ��������󣬲������������!");
                    break;
                case enmApproveResult.No_Purview:
                    clsPublicFunction.ShowInformationMessageBox("���û���Ȩ���������еĸò���!");
                    break;
                case enmApproveResult.EmployeeID_Error:
                    clsPublicFunction.ShowInformationMessageBox("Ա���Ŵ���!");
                    break;
                case enmApproveResult.Not_Found_Approve_Info:
                    clsPublicFunction.ShowInformationMessageBox("û���ҵ��õ�������˵���Ϣ!");
                    break;
                case enmApproveResult.Is_Top_Level:
                    clsPublicFunction.ShowInformationMessageBox("�Ѿ��˻ص�����һ��!");
                    break;
                case enmApproveResult.Document_Has_Been_Deleted:
                    clsPublicFunction.ShowInformationMessageBox("�õ��Ѿ�ɾ��!");
                    break;
                default:
                    break;
            }
            #endregion ���ݽ������ͬ�Ĵ���

        }

        protected virtual void mniUnApprove_Click(object sender, System.EventArgs e)
        {
            if (!m_blnCheckDataGridCurrentRow())
                return;

            int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
            if (intSelectedRecordStartRow < 0)
                return;

            string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
            int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];

            //��ȡ��¼�Ĵ���
            string strFormID = m_strGetFormID(intRecordType);

            long lngEff = 0;
            lngEff = m_objApprove.lngUntreadDocumentOneLevel(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID, strFormID, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), strOpenDate, m_ObjCurrentEmrPatientSession.m_strAreaId, ref lngEff);//((clsDepartment)this.m_cboDept.SelectedItem).m_StrDeptID,ref lngEff);

            #region ���ݽ������ͬ�Ĵ���
            switch ((enmApproveResult)lngEff)
            {
                case enmApproveResult.DB_Succeed:
                    clsPublicFunction.ShowInformationMessageBox("����ɹ�!");
                    break;
                case enmApproveResult.System_Not_Define:
                    clsPublicFunction.ShowInformationMessageBox("ϵͳû�ж���õ���������̣�Ӧ�������ݿ��ж��壩!");
                    break;
                case enmApproveResult.Document_Has_Been_Finished:
                    clsPublicFunction.ShowInformationMessageBox("���Ѿ��������󣬲������������!");
                    break;
                case enmApproveResult.No_Purview:
                    clsPublicFunction.ShowInformationMessageBox("���û���Ȩ���������еĸò���!");
                    break;
                case enmApproveResult.EmployeeID_Error:
                    clsPublicFunction.ShowInformationMessageBox("Ա���Ŵ���!");
                    break;
                case enmApproveResult.Not_Found_Approve_Info:
                    clsPublicFunction.ShowInformationMessageBox("û���ҵ��õ�������˵���Ϣ!");
                    break;
                case enmApproveResult.Is_Top_Level:
                    clsPublicFunction.ShowInformationMessageBox("�Ѿ��˻ص�����һ��!");
                    break;
                case enmApproveResult.Document_Has_Been_Deleted:
                    clsPublicFunction.ShowInformationMessageBox("�õ��Ѿ�ɾ��!");
                    break;
                default:
                    break;
            }
            #endregion ���ݽ������ͬ�Ĵ���
        }

        #endregion

        /// <summary>
        /// ����Ҫ������ʾ
        /// </summary>
        protected override void m_mthAddFormStatusForClosingSave()
        {
        }

        /// <summary>
        /// ��ӡǰ����Ҫ��ʾ����
        /// </summary>
        /// <returns></returns>
        protected override DialogResult m_dlgHandleSaveBeforePrint()
        {
            return DialogResult.None;
        }

        /// <summary>
        /// ��ʾ���ݵ�datagrid(Σ�ػ����¼)
        /// </summary>
        protected virtual void DisplayDataToDatagrid(DataTable p_dtData)
        {
            return;
        }

        /// <summary>
        /// ����û�м�¼���Զ�����Ӧ�Ĳ��̼�¼
        /// </summary>
        protected virtual void m_mthAutoAddNewRecord()
        {
            return;
        }



        protected override void m_mthAddRichTemplateInContainer(Control p_ctlContainer)
        {
        }
        #region ���÷�ҳ��һ�㻤���¼��
        #region  ���ɾ�����̼�¼����
        //��ӿ���
        private void m_mniAddBlank_Click(object sender, System.EventArgs e)
        {
            if (!m_blnCheckDataGridCurrentRow())
                return;
            int intRowsCount = 0;
            using (frmAddBlankInDiseaseTrack frmAddBlankdlg = new frmAddBlankInDiseaseTrack())
            {
                if (frmAddBlankdlg.ShowDialog() == DialogResult.OK)
                    intRowsCount = frmAddBlankdlg.m_IntLineCount;
            }
            if (intRowsCount == 0)
                return;
            int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
            if (intSelectedRecordStartRow < 0)
                return;
            string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
            m_mthAddBlankToDiseaseTrack(strOpenDate, intRowsCount);

        }
        /// <summary>
        /// ���ӿ��м�¼�����̼�¼
        /// </summary>
        /// <param name="p_strOpenDate">��¼������</param>
        /// <param name="p_intRowsCount">��ӵ�����</param>
        private void m_mthAddBlankToDiseaseTrack(string p_strOpenDate, int p_intRowsCount)
        {
            object[][] objDataArr = new object[p_intRowsCount][];
            for (int i = 0; i < objDataArr.Length; i++)
                objDataArr[i] = new object[5];
            bool blnAddnew = true;
            DataRow dtrNewRow;
            int intInsertIndex = -1;
            //ѭ��DataTable�ļ�¼����ȡ��¼�Ĵ����ڣ������ֶΣ�
            //����м�¼���ڣ����������p_dtmOpenDate��ͬ
            //intInsertIndex = �±�;
            //�����¼�����Ǿɿ��м�¼,�¿��м�¼���������ں����ͱ�ʶ,�±��ټ�1
            //����ѭ��
            for (int i1 = 0; i1 < m_dtbRecords.Rows.Count; i1++)
            {
                if (m_dtbRecords.Rows[i1][2].ToString() != "")
                {
                    if (DateTime.Parse(m_dtbRecords.Rows[i1][2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(p_strOpenDate).ToString("yyyy-MM-dd HH:mm:ss"))
                    {
                        intInsertIndex = i1;
                        if (m_dtbRecords.Rows[i1][1].ToString() == "100")
                        {
                            intInsertIndex++;
                            blnAddnew = false;
                        }
                        else
                        {
                            objDataArr[0][1] = 100;
                            objDataArr[0][2] = p_strOpenDate;
                        }
                        break;
                    }
                }
            }
            if (intInsertIndex == -1) return;
            if (m_dtbTempTable == null)
            {
                m_dtbTempTable = m_dtbRecords.Clone();
            }
            while ((intInsertIndex < m_dtbRecords.Rows.Count))
            {
                m_mthSetDataGridFirstRowFocus();
                dtrNewRow = m_dtbTempTable.NewRow();
                dtrNewRow.ItemArray = m_dtbRecords.Rows[intInsertIndex].ItemArray;
                m_dtbTempTable.Rows.Add(dtrNewRow);
                m_dtbRecords.Rows.RemoveAt(intInsertIndex);
            }
            for (int i1 = 0; i1 < objDataArr.Length; i1++)//�����ļ�¼
            {
                dtrNewRow = m_dtbRecords.NewRow();
                dtrNewRow.ItemArray = objDataArr[i1];
                m_dtbRecords.Rows.Add(dtrNewRow);
            }
            for (int i1 = 0; i1 < m_dtbTempTable.Rows.Count; i1++)//���ڴ��еļ�¼������ӻ�ȥ
            {
                dtrNewRow = m_dtbRecords.NewRow();
                dtrNewRow.ItemArray = m_dtbTempTable.Rows[i1].ItemArray;
                m_dtbRecords.Rows.Add(dtrNewRow);
            }
            if (m_dtbTempTable != null)
            {
                m_dtbTempTable.Rows.Clear();
            }
            clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
            //��ȡ��Ҫ����
            clsTrackRecordContent objContent = new clsTrackRecordContent();
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
            objContent.m_strRowsNumber = p_intRowsCount.ToString();
            if (blnAddnew)
            {
                objAddBlankDomain.m_lngAddNewBlankRecord(objContent);
            }
            else
            {
                DataTable dtbBlankRecord;
                objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbBlankRecord);
                if (dtbBlankRecord != null && dtbBlankRecord.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow drtAdd in dtbBlankRecord.Rows)
                    {
                        if (DateTime.Parse(drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(p_strOpenDate).ToString("yyyy-MM-dd HH:mm:ss"))
                        {
                            objContent.m_strRowsNumber = Convert.ToString(Convert.ToInt32(drtAdd[3].ToString()) + p_intRowsCount);
                            break;
                        }
                    }
                }
                objAddBlankDomain.m_lngModifyBlankRecord(objContent);
            }
        }

        //ɾ������
        private void m_mniDeleteBlank_Click(object sender, System.EventArgs e)
        {
            int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
            if (intSelectedRecordStartRow < 0)
                return;
            string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
            //����p_dtmOpenDate���Ҷ�Ӧ�ļ�¼
            int intDeleteIndex = -1;
            //ѭ��DataTable�ļ�¼����ȡ��¼�����ڣ��������ֶΣ��ͼ�¼���ͣ��ڶ����ֶΣ�
            //����м�¼���ںͼ�¼����
            //����м�¼���ڣ����������p_dtmOpenDate��ͬ ,���Ҽ�¼��������м�¼������ͬ
            //intDeleteIndex = �±�;
            //����ѭ��
            for (int i1 = 0; i1 < m_dtbRecords.Rows.Count; i1++)
            {
                if (m_dtbRecords.Rows[i1][2].ToString() != "" && m_dtbRecords.Rows[i1][1].ToString() != "")
                {
                    if (DateTime.Parse(m_dtbRecords.Rows[i1][2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(strOpenDate).ToString("yyyy-MM-dd HH:mm:ss") && (int)m_dtbRecords.Rows[i1][1] == 100)
                    {
                        intDeleteIndex = i1;
                        break;
                    }
                }
            }
            //ɾ���Ŀ�����
            int intBlankLine = 0;
            if (intDeleteIndex >= 0)
            {
                //����intDeleteIndexѭ��ɾ����¼
                //ɾ��intDeleteIndex��

                //���intDeleteIndexû�г���DataTable��Χ
                //��ȡ��¼���鿴���޼�¼������
                //����У�˵����¼�Ѿ�ɾ����ϣ�����

                //����ޣ�����ɾ����
                //intDeleteIndex������Χ(>=DataTable.Rows.Count)������

                m_mthSetDataGridFirstRowFocus();

                m_dtbRecords.Rows.RemoveAt(intDeleteIndex);
                intBlankLine++;

                //���ֻ�и�����¼ֻ��һ������ʱ��������ִ�д�ѭ��
                while ((intDeleteIndex < m_dtbRecords.Rows.Count) && (m_dtbRecords.Rows[intDeleteIndex][2].ToString() == ""))
                {
                    m_mthSetDataGridFirstRowFocus();
                    m_dtbRecords.Rows.RemoveAt(intDeleteIndex);
                    intBlankLine++;
                }
            }
            #region  �����ݿ��ȡ������(No used)
            //			DataTable dtbBlankRecord;
            //			
            //			objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID,m_objCurrentPatient.m_DtmSelectedInDate,out dtbBlankRecord);
            //			
            //			if (dtbBlankRecord != null && dtbBlankRecord.Rows.Count > 0)
            //			{
            //				foreach(System.Data.DataRow drtAdd in dtbBlankRecord.Rows)
            //				{
            //					if (DateTime.Parse(drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(strOpenDate).ToString("yyyy-MM-dd HH:mm:ss"))
            //					{
            //						strBlankLine = drtAdd[3].ToString();
            //						break;
            //					}
            //				}
            //			}
            #endregion
            //��ȡ��Ҫ����
            clsTrackRecordContent objContent = new clsTrackRecordContent();
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_dtmOpenDate = DateTime.Parse(strOpenDate);
            objContent.m_strRowsNumber = intBlankLine.ToString();

            clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
            objAddBlankDomain.m_lngDeleteBlankRecord(objContent);
        }
        #endregion

        //		/// <summary>
        //		/// ��ӷ�ҳ��־
        //		/// </summary>
        //		/// <param name="sender"></param>
        //		/// <param name="e"></param>
        //		private void mniPageAdd_Click(object sender, System.EventArgs e)
        //		{
        //			m_mthSetPagination("1");
        //		}
        //		/// <summary>
        //		/// ɾ����ҳ��־
        //		/// </summary>
        //		/// <param name="sender"></param>
        //		/// <param name="e"></param>
        //		private void mniPageRemove_Click(object sender, System.EventArgs e)
        //		{
        //			m_mthSetPagination("0");
        //		}
        /// <summary>
        /// ���÷�ҳ��־��1����ҳ   0������ҳ��
        /// </summary>
        /// <param name="p_strIsAddPage"></param>
        protected void m_mthSetPagination(string p_strIsAddPage)
        {
            if (!m_blnCheckDataGridCurrentRow())
                return;
            int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecordDetail.CurrentCell.RowNumber);
            if (intSelectedRecordStartRow < 0)
                return;
            try
            {
                string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
                int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];
                //��ȡ��Ӽ�¼�Ĵ���
                frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(intRecordType);

                clsTrackRecordContent objContent = frmAddNewForm.m_objGetRecordContent(m_objCurrentPatient, strOpenDate);
                if (objContent != null)
                {
                    //com.digitalwave.clsMainGeneralNurseRecordService.clsMainGeneralNurseRecordService objServ =
                    //    (com.digitalwave.clsMainGeneralNurseRecordService.clsMainGeneralNurseRecordService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsMainGeneralNurseRecordService.clsMainGeneralNurseRecordService));

                    long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngSetPagination(objContent, p_strIsAddPage);
                    if (lngRes > 0)
                    {
                        objContent.m_StrPagination = p_strIsAddPage;
                        clsTransDataInfo objDataInfo = new clsTransDataInfo();
                        objDataInfo.m_intFlag = intRecordType;
                        objDataInfo.m_objRecordContent = objContent;
                        try
                        {
                            //��ղ��˼�¼��Ϣ				
                            m_mthClearPatientRecordInfo();
                            if (m_ObjCurrentEmrPatientSession == null || m_objCurrentPatient == null)
                            {
                                return;
                            }

                            m_objCurrentPatient.m_DtmSelectedInDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;

                            //��ȡ���˼�¼�б�
                            clsTransDataInfo[] objTansDataInfoArr;
                            //string m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
                            //string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss"); //m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss");
                            lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_ObjCurrentEmrPatientSession.m_strEMRInpatientId, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out objTansDataInfoArr);

                            if (lngRes <= 0 || objTansDataInfoArr == null)
                            {
                                return;
                            }

                            //����¼ʱ��(CreateDate)����
                            m_mthSortTransData(ref objTansDataInfoArr);

                            DataTable dtbAddBlank;
                            clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
                            objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

                            //��Ӽ�¼����DataTable
                            object[][] objDataArr;
                            for (int i1 = 0; i1 < objTansDataInfoArr.Length; i1++)
                            {
                                //���Ҽ�¼֮ǰ�з���м�¼,�в������
                                foreach (DataRow drtAdd in dtbAddBlank.Rows)
                                {
                                    if (objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                                    {
                                        object[] objBlank = new object[5];
                                        objBlank[1] = 100;
                                        objBlank[2] = drtAdd[2].ToString();
                                        m_dtbRecords.Rows.Add(objBlank);
                                        for (int k3 = 0; k3 < (Int32.Parse(drtAdd[3].ToString()) - 1); k3++)
                                        {
                                            m_dtbRecords.Rows.Add(new object[] { });
                                        }
                                        break;
                                    }
                                }

                                objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);

                                if (objDataArr == null)
                                    return;
                                for (int j2 = 0; j2 < objDataArr.Length; j2++)
                                {
                                    m_dtbRecords.Rows.Add(objDataArr[j2]);
                                }
                            }
                            //				if(m_dtbRecords.Rows.Count > 0)//�����񲡳̼�¼����ֻ��һ��column����� alex 2003-05-29
                            //				{
                            //					m_dtgRecordDetail.CurrentCell = new DataGridCell(1,0);
                            //					m_dtgRecordDetail.CurrentCell = new DataGridCell(0,0);
                            //				}		

                            if (m_dtbRecords.Rows.Count == 0 && !m_blnIfNewDeletedRecord)
                            {
                                m_mthAutoAddNewRecord();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                        }

                        //���µ�datatable
                        //datagrid�н��б�־


                        //�������ݵ�DataTable �Ӵ�������
                        //					object [][] objDataArr = m_objGetRecordsValueArr(objDataInfo);  

                        //��DataTableɾ������
                        //					m_mthRemoveDataFromDataTable(intRecordType,objContent.m_dtmOpenDate);

                        //������ݵ�DataTable
                        //					m_mthAddDataToDataTable(objDataArr,objContent.m_dtmCreateDate);
                    }
                }

            }
            catch (Exception exp)
            {
                string strTemp = exp.Message;
            }

        }

        #endregion ;

        /// <summary>
        /// ���÷�ҳ��־��1����ҳ   0������ҳ�����̼�¼
        /// </summary>
        /// <param name="p_strIsAddPage"></param>
        protected void m_mthSetPaginations(string p_strIsAddPage)
        {
            if (!m_blnCheckDataGridCurrentRow())
                return;
            int intSelectedRecordStartRow = m_intGetRecordStartRows(m_dtgRecordDetail.CurrentCell.RowNumber);
            if (intSelectedRecordStartRow < 0)
                return;
            try
            {
                string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][2].ToString();
                int intRecordType = (int)m_dtbRecords.Rows[intSelectedRecordStartRow][1];
                //��ȡ��Ӽ�¼�Ĵ���
                frmDiseaseTrackBase frmAddNewForm = m_frmGetRecordForm(intRecordType);
                string getfromtype = frmAddNewForm.Text;
                clsTrackRecordContent objContent = frmAddNewForm.m_objGetRecordContent(m_objCurrentPatient, strOpenDate);
                if (objContent != null)
                {
                    //com.digitalwave.clsSubDiseaseTrackServer.clsSubDiseaseTrackServer objServ =
                    //    (com.digitalwave.clsSubDiseaseTrackServer.clsSubDiseaseTrackServer)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsSubDiseaseTrackServer.clsSubDiseaseTrackServer));

                    long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngSetPagination(objContent, p_strIsAddPage, getfromtype);
                    if (lngRes > 0)
                    {
                        objContent.m_StrPagination = p_strIsAddPage;
                        clsTransDataInfo objDataInfo = new clsTransDataInfo();
                        objDataInfo.m_intFlag = intRecordType;
                        objDataInfo.m_objRecordContent = objContent;
                        try
                        {
                            //��ղ��˼�¼��Ϣ				
                            m_mthClearPatientRecordInfo();
                            if (m_ObjCurrentEmrPatientSession == null || m_objCurrentPatient == null)
                            {
                                return;
                            }

                            m_objCurrentPatient.m_DtmSelectedInDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;

                            //��ȡ���˼�¼�б�
                            clsTransDataInfo[] objTansDataInfoArr;
                            //string m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
                            //string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss"); //m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss");
                            lngRes = m_objRecordsDomain.m_lngGetTransDataInfoArr(m_ObjCurrentEmrPatientSession.m_strEMRInpatientId, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out objTansDataInfoArr);

                            if (lngRes <= 0 || objTansDataInfoArr == null)
                            {
                                return;
                            }

                            //����¼ʱ��(CreateDate)����
                            m_mthSortTransData(ref objTansDataInfoArr);

                            DataTable dtbAddBlank;
                            clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
                            objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

                            //��Ӽ�¼����DataTable
                            object[][] objDataArr;
                            for (int i1 = 0; i1 < objTansDataInfoArr.Length; i1++)
                            {
                                //���Ҽ�¼֮ǰ�з���м�¼,�в������
                                foreach (DataRow drtAdd in dtbAddBlank.Rows)
                                {
                                    if (objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                                    {
                                        object[] objBlank = new object[5];
                                        objBlank[1] = 100;
                                        objBlank[2] = drtAdd[2].ToString();
                                        m_dtbRecords.Rows.Add(objBlank);
                                        for (int k3 = 0; k3 < (Int32.Parse(drtAdd[3].ToString()) - 1); k3++)
                                        {
                                            m_dtbRecords.Rows.Add(new object[] { });
                                        }
                                        break;
                                    }
                                }

                                objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);

                                if (objDataArr == null)
                                    return;
                                for (int j2 = 0; j2 < objDataArr.Length; j2++)
                                {
                                    m_dtbRecords.Rows.Add(objDataArr[j2]);
                                }
                            }
                            //				if(m_dtbRecords.Rows.Count > 0)//�����񲡳̼�¼����ֻ��һ��column����� alex 2003-05-29
                            //				{
                            //					m_dtgRecordDetail.CurrentCell = new DataGridCell(1,0);
                            //					m_dtgRecordDetail.CurrentCell = new DataGridCell(0,0);
                            //				}		

                            if (m_dtbRecords.Rows.Count == 0 && !m_blnIfNewDeletedRecord)
                            {
                                m_mthAutoAddNewRecord();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                        }

                        //���µ�datatable
                        //datagrid�н��б�־


                        //�������ݵ�DataTable �Ӵ�������
                        //					object [][] objDataArr = m_objGetRecordsValueArr(objDataInfo);  

                        //��DataTableɾ������
                        //					m_mthRemoveDataFromDataTable(intRecordType,objContent.m_dtmOpenDate);

                        //������ݵ�DataTable
                        //					m_mthAddDataToDataTable(objDataArr,objContent.m_dtmCreateDate);
                    }
                }

            }
            catch (Exception exp)
            {
                string strTemp = exp.Message;
            }

        }

        //		protected override void m_mthAssociateComboBoxItemEvent(Control p_ctlParent)
        //		{}

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null) return;
            try
            {
                //��ղ��˼�¼��Ϣ
                if (m_dtgRecordDetail != null)
                {
                    m_mthClearPatientRecordInfo();
                }

                if (p_objSelectedSession == null || m_objCurrentPatient == null)
                {
                    return;
                }

                //���ò��˵���סԺ�Ļ�����Ϣ

                m_objCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
                m_objCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;

                m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;

                m_mthOnlySetPatientInfo(m_objCurrentPatient);

                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                //��ȡ���˼�¼�б�
                clsTransDataInfo[] objTansDataInfoArr;
                m_mthGetTransDataInfoArr(out objTansDataInfoArr);

                if (objTansDataInfoArr == null)
                {
                    return;
                }

                //����¼ʱ��(CreateDate)����
                //modified by  thfzhang 2005-11-12 Σ�ػ�����Ҫ����
                if (this.Name != "frmIntensiveTendMain_FC")
                    m_mthSortTransData(ref objTansDataInfoArr);

                DataTable dtbAddBlank;
                clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
                objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

                //��Ӽ�¼����DataTable
                object[][] objDataArr;
                for (int i1 = 0; i1 < objTansDataInfoArr.Length; i1++)
                {
                    if (dtbAddBlank != null && dtbAddBlank.Rows.Count > 0)
                    {
                        //���Ҽ�¼֮ǰ�з���м�¼,�в������
                        foreach (DataRow drtAdd in dtbAddBlank.Rows)
                        {
                            if (objTansDataInfoArr[i1].m_objRecordContent != null)
                            {
                                if (objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                                {
                                    object[] objBlank = new object[5];
                                    objBlank[1] = 100;
                                    objBlank[2] = drtAdd[2].ToString();
                                    m_dtbRecords.Rows.Add(objBlank);
                                    for (int k3 = 0; k3 < (Int32.Parse(drtAdd[3].ToString()) - 1); k3++)
                                    {
                                        m_dtbRecords.Rows.Add(new object[] { });
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);

                    if (objDataArr == null)
                        continue;

                    m_dtbRecords.BeginLoadData();
                    for (int j2 = 0; j2 < objDataArr.Length; j2++)
                    {
                        m_dtbRecords.LoadDataRow(objDataArr[j2], true);
                    }
                    m_dtbRecords.EndLoadData();

                }
                m_dtgRecordDetail.EnsureVisible(m_dtbRecords.Rows.Count - 1);
                Application.DoEvents();
                m_mthAfterLoadData();

                if (m_dtbRecords.Rows.Count == 0 && !m_blnIfNewDeletedRecord)
                {
                    m_mthAutoAddNewRecord();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// ��ʾ��������������������
        /// </summary>
        protected virtual void m_mthAfterLoadData()
        {
        }

        protected bool m_blnSubIsExists()
        {
            if (m_frmCurrentSub != null && m_frmCurrentSub.CanFocus)
            {
                //MessageBox.Show("ע�⣺��ǰ���д򿪵�δ����ı༭���壬�Ƿ�رպ������ã�");
                return true;
            }
            return false;
        }
    }

}

