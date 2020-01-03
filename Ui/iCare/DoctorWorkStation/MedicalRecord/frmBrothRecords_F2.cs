using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using iCareData;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data;
using com.digitalwave.controls;
using com.digitalwave.Emr.Signature_gui;

 

namespace iCare
{
    /// <summary>
    /// ��ʱ��¼(���)
    /// </summary>
    public partial class frmBrothRecords_F2 : iCare.frmBaseCaseHistory
    {

        //����ǩ����
        private clsEmrSignToolCollection m_objSign;
        private System.Windows.Forms.ContextMenu m_ctmRecordControl;
        private new clsBrothRecords_F2Domain m_objDomain;
        private string m_strCurrentOpenDate = "";
        private bool m_blnCanShowNewForm = true;
        private com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
        public com.digitalwave.Utility.Controls.ctlTimePicker m_dtpCheckDate;
        private new clsBrothRecords_F2 m_objCurrentRecordContent;

        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcXueYa;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcGongsuoJianxie;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcGongSuoTime;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcTaiXin;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcGongKouKaiDa;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcTaiMo;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcXianLu;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcJianChaFa;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcGongDiSiZhou;
        private com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox m_dtcFenMiWu;
       
        private System.Windows.Forms.DataGridTextBoxColumn m_clmSign;


        /// <summary>
        /// ����������
        /// </summary>
        protected virtual Font m_FntHeaderFont
        {
            get
            {
                return new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            }
        }

        private DataTable m_dtbRecords;

        public frmBrothRecords_F2()
        {
            InitializeComponent();

            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();
            //m_mthBindEmployeeSign(��ť,ǩ����,ҽ��1or��ʿ2,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(m_cmdDoctorSign, m_txtJieSheng, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdZhuLiSign, m_txtZhuLi, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdHuLiSign, m_txtHuLi, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdZhiDaoSign, m_txtZhiDao, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);

            m_dtbRecords = new DataTable("RecordDetail");
            this.m_dtgRecord.HeaderFont = m_FntHeaderFont;
            m_objDomain = new clsBrothRecords_F2Domain();
        }


        /// <summary>
        /// ���Ӽ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mniAddBabyCircsRecord_Click(object sender, System.EventArgs e)
        {
            DateTime dtSelectedInPatientDate = DateTime.MinValue;
            if (m_ObjCurrentEmrPatientSession != null)
            {
                dtSelectedInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
            }
            else 
            {
                m_dtbRecords.Clear();
                return;
            }

            if (m_objCurrentPatient == null)
                return;
            //dtSelectedInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            string strOpenDate = "";
            frmBrothRecordsRec_F2 frm = new frmBrothRecordsRec_F2(true, m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, ref strOpenDate);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                clsBrothRecords_F2Rec[] objCircsRecordArr;
                long lngRes = m_objDomain.m_lngGetAllCircsRecordContent(m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, out objCircsRecordArr);

                //�������ݵ�DataTable
                object[][] objDataArr = m_objGetRecordsValueArr(objCircsRecordArr);

                //������ݵ�DataTable
                if (strOpenDate == "")
                    strOpenDate = "1900-01-01 00:00:00";
                m_mthAddDataToDataTable(objDataArr, DateTime.Parse(strOpenDate));

                //TreeNode trvTemp=trvTime.SelectedNode;
                //trvTime.SelectedNode=null;
                //trvTime.SelectedNode=trvTemp;
            }
        }

        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mmiDelBabyCircsRecord_Click(object sender, System.EventArgs e)
        {
            DateTime dtSelectedInPatientDate = DateTime.MinValue;
            DateTime dtOpen = DateTime.MinValue;
            DateTime dtModify = DateTime.MinValue;

            if (m_ObjCurrentEmrPatientSession != null)
            {
                dtSelectedInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
            }
            else
            {
                m_dtbRecords.Clear();
                return;
            }
            try
            {
                dtOpen = Convert.ToDateTime(m_dtbRecords.Rows[m_dtgRecord.CurrentRowIndex][1]);
                dtModify = Convert.ToDateTime(m_dtbRecords.Rows[m_dtgRecord.CurrentRowIndex][2]);
            }
            catch
            {
                MDIParent.ShowInformationMessageBox("����ѡ��һ����¼");
                return;
            }
            //���ݲ���
            int intSelectedRecordStartRow = m_dtgRecord.CurrentCell.RowNumber;
            //ȷ��
            if (MessageBox.Show("ȷ��Ҫɾ��ѡ�еĲ����¼���ݣ�", "ɾ����ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            //�򿪴���
            //ɾ��
            clsBrothRecords_F2Rec objDelRecord = new clsBrothRecords_F2Rec();
            objDelRecord.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objDelRecord.m_dtmOpenDate = dtOpen;
            objDelRecord.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objDelRecord.m_strDeActivedOperatorID = MDIParent.OperatorID;
            objDelRecord.m_dtmDeActivedDate = DateTime.Now;
            objDelRecord.m_dtmModifyDate = dtModify;

            long lngRes = m_objDomain.m_lngDeleteCircsRecord(objDelRecord);
            //����
            if (lngRes > 0)
            {
                clsBrothRecords_F2Rec[] objCircsRecordArr;
                lngRes = m_objDomain.m_lngGetAllCircsRecordContent(m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, out objCircsRecordArr);

                //�������ݵ�DataTable
                object[][] objDataArr = m_objGetRecordsValueArr(objCircsRecordArr);

                //������ݵ�DataTable
                m_dtbRecords.Clear();
                m_mthAddDataToDataTable(objDataArr, DateTime.Parse("1900-01-01 00:00:00"));
            }
        }

        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mmiModifyBabyCircsRecord_Click(object sender, System.EventArgs e)
        {
            DateTime dtSelectedInPatientDate = DateTime.MinValue;
            if (m_ObjCurrentEmrPatientSession != null)
            {
                dtSelectedInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
            }
            else
            {
                m_dtbRecords.Clear();
                return;
            }
            if (m_objCurrentPatient == null)
                return;
            //dtSelectedInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            string strOpenDate = m_dtbRecords.Rows[m_dtgRecord.CurrentRowIndex][1].ToString(); ;
            frmBrothRecordsRec_F2 frm = new frmBrothRecordsRec_F2(false, m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, ref strOpenDate);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                clsBrothRecords_F2Rec[] objCircsRecordArr;
                long lngRes = m_objDomain.m_lngGetAllCircsRecordContent(m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, out objCircsRecordArr);

                //�������ݵ�DataTable
                object[][] objDataArr = m_objGetRecordsValueArr(objCircsRecordArr);

                //������ݵ�DataTable
                m_mthAddDataToDataTable(objDataArr, DateTime.Parse("1900-01-01 00:00:00"));

                //TreeNode trvTemp=trvTime.SelectedNode;
                //trvTime.SelectedNode=null;
                //trvTime.SelectedNode=trvTemp;
            }
        }

        private void frmBrothRecords_F2_Load(object sender, EventArgs e)
        {
            m_mthInitDataTable(m_dtbRecords);
            m_dtgRecord.DataSource = m_dtbRecords;
            m_mthSetRichTextBoxAttribInControl(this);
            m_mthSetAllDataGridTextBoxColum();
            m_dtmPreRecordDate = DateTime.MinValue;
        }

        private void m_mthInitDataTable(DataTable p_dtbRecordTable)
        {
            //��ż�¼ʱ����ַ���
            p_dtbRecordTable.Columns.Add("CreateDate");//0
            //��ż�¼��OpenDate�ַ���
            p_dtbRecordTable.Columns.Add("OpenDate");  //1
            //��ż�¼��ModifyDate�ַ���
            p_dtbRecordTable.Columns.Add("ModifyDate"); //2

            DataColumn dc1 = p_dtbRecordTable.Columns.Add("RecordDate");//3
            dc1.DefaultValue = "";
            //DataColumn dc2 = p_dtbRecordTable.Columns.Add("BirthDays");//4
            //dc2.DefaultValue = "";
            p_dtbRecordTable.Columns.Add("XUEYA", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//5
            p_dtbRecordTable.Columns.Add("GONGSUOJIANXUE", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//6
            p_dtbRecordTable.Columns.Add("GONGSUOTIME", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//7
            p_dtbRecordTable.Columns.Add("TAIXIN", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//8
            p_dtbRecordTable.Columns.Add("GONGKOU", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//9
            p_dtbRecordTable.Columns.Add("TAIMO", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//10
            p_dtbRecordTable.Columns.Add("XIANLU", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//11
            p_dtbRecordTable.Columns.Add("JIANCHAFA", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//12
            p_dtbRecordTable.Columns.Add("GONGDIJIZHOU", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//13
            p_dtbRecordTable.Columns.Add("FENMIWU", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//14
            //p_dtbRecordTable.Columns.Add("Agnail", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//15
            //p_dtbRecordTable.Columns.Add("RedStern", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//16
            //p_dtbRecordTable.Columns.Add("SternSkin", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//17		
            //p_dtbRecordTable.Columns.Add("HeartLung", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue)); //18
            //p_dtbRecordTable.Columns.Add("Abdomen", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//19			
            //p_dtbRecordTable.Columns.Add("Remark", typeof(com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue));//20
            DataColumn dc3 = p_dtbRecordTable.Columns.Add("Sign");//21
            dc3.DefaultValue = "";

            //����������
            this.m_clmRecordDate.HeaderText = "��  ��";
            this.m_dtcXueYa.HeaderText = "Ѫѹ";
            this.m_dtcGongsuoJianxie.HeaderText = "������Ъ";
            this.m_dtcGongSuoTime.HeaderText = "������ʱ";
            this.m_dtcTaiXin.HeaderText = "̥�Ĵ���/��";
            this.m_dtcGongKouKaiDa.HeaderText = "���ڿ���̶�";
            this.m_dtcTaiMo.HeaderText = "̥Ĥ���";
            this.m_dtcXianLu.HeaderText = "��¶�ߵ�";
            this.m_dtcJianChaFa.HeaderText = "��鷨";
            this.m_dtcGongDiSiZhou.HeaderText = "���ͼ�����";
            this.m_dtcFenMiWu.HeaderText = "���׷��������";
             
            this.m_clmSign.HeaderText = "ǩ  ��";
        }

        private void m_mthSetAllDataGridTextBoxColum()
        {
            m_mthSetControl(m_clmRecordDate);
            m_mthSetControl(m_dtcXueYa);
            m_mthSetControl(m_dtcGongsuoJianxie);
            m_mthSetControl(m_dtcGongSuoTime);
            m_mthSetControl(m_dtcTaiXin);
            m_mthSetControl(m_dtcGongKouKaiDa);
            m_mthSetControl(m_dtcTaiMo);
            m_mthSetControl(m_dtcXianLu);
            m_mthSetControl(m_dtcJianChaFa);
            m_mthSetControl(m_dtcGongDiSiZhou);
            m_mthSetControl(m_dtcFenMiWu);
            m_mthSetControl(m_clmSign); 
        }


        /// <summary>
        /// ����DataGrid�ڵĿؼ��������¼����Ҽ��˵�
        /// </summary>
        /// <param name="p_objControl"></param>
        private void m_mthSetControl(DataGridTextBoxColumn p_objControl)
        {
            Control m_objControl;
            m_objControl = (DataGridTextBox)p_objControl.TextBox;
            m_objControl.ContextMenu = m_ctmRecordControl;
            m_objControl.DoubleClick += new EventHandler(m_mthTxtDoubleClick);
        }

        /// <summary>
        /// ����DataGrid�ڵĿؼ��������¼����Ҽ��˵�
        /// </summary>
        /// <param name="p_objControl"></param>
        private void m_mthSetControl(com.digitalwave.Utility.Controls.cltDataGridDSTRichTextBox p_objControl)
        {
            p_objControl.m_RtbBase.ContextMenu = m_ctmRecordControl;
            p_objControl.m_RtbBase.MouseDown += new MouseEventHandler(cltDataGridDSTRichTextBox_MouseDown);
        }

        /// <summary>
        /// ˫��DataGrid�ڵĿؼ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthTxtDoubleClick(object sender, EventArgs e)
        {
            if (!m_blnCheckDataGridCurrentRow())
                return;
            try
            {
                int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecord.CurrentCell.RowNumber);
                if (intSelectedRecordStartRow < 0)
                    return;
                string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][1].ToString();
                m_mthModifyRecord(DateTime.Parse(strOpenDate));
            }
            catch (Exception exp)
            {
                string strErrorMessage = exp.Message;
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
                    int intSelectedRecordStartRow = m_intGetRecordStartRow(m_dtgRecord.CurrentCell.RowNumber);
                    if (intSelectedRecordStartRow < 0)
                        return;
                    string strOpenDate = m_dtbRecords.Rows[intSelectedRecordStartRow][1].ToString();
                    //m_mthModifyRecord(DateTime.Parse(strOpenDate));

                    DateTime dtSelectedInPatientDate = DateTime.MinValue;
                    if (m_ObjCurrentEmrPatientSession != null)
                    {
                        dtSelectedInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
                    }
                    else
                    {
                        m_dtbRecords.Clear();
                        return;
                    }

                    frmBrothRecordsRec_F2 frm = new frmBrothRecordsRec_F2(false, m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, ref strOpenDate);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (frm.ShowDialog() == DialogResult.Yes)
                    {
                        clsBrothRecords_F2Rec[] objCircsRecordArr;
                        long lngRes = m_objDomain.m_lngGetAllCircsRecordContent(m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, out objCircsRecordArr);

                        //�������ݵ�DataTable
                        object[][] objDataArr = m_objGetRecordsValueArr(objCircsRecordArr);

                        //������ݵ�DataTable
                        m_mthAddDataToDataTable(objDataArr, DateTime.Parse("1900-01-01 00:00:00"));

                    }
                }
            }
            catch
            {

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
                if (m_dtgRecord.CurrentCell.RowNumber >= m_dtbRecords.Rows.Count)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }

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


        protected void m_mthModifyRecord(DateTime p_dtmOpenRecordTime)
        {
            if (!m_blnCanShowNewForm)
                return;

            DateTime dtSelectedInPatientDate = DateTime.MinValue;
            //try
            //{
            //    dtSelectedInPatientDate = DateTime.Parse(trvTime.SelectedNode.Text.Trim());
            //}
            //catch
            //{
            //    m_dtbRecords.Clear();
            //}
            if (m_ObjCurrentEmrPatientSession != null)
            {
                dtSelectedInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
            }
            else
            {
                m_dtbRecords.Clear();
            }
            //��ȡ��Ӽ�¼�Ĵ���
            string strOpenDate = p_dtmOpenRecordTime.ToString("yyyy-MM-dd HH:mm:ss");
            frmBrothRecordsRec_F2 frmAddNewForm = new frmBrothRecordsRec_F2(false, m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, ref strOpenDate);

            m_mthShowSubForm(frmAddNewForm); 

            MDIParent.s_ObjSaveCue.m_mthAddFormStatus(frmAddNewForm);
        }


        protected void m_mthShowSubForm(Form p_frmSubForm)
        {
            p_frmSubForm.Closed += new EventHandler(m_mthSubFormClosed);
            m_blnCanShowNewForm = false;
            m_frmCurrentSub = (frmBrothRecordsRec_F2)p_frmSubForm;

            p_frmSubForm.TopMost = true;
            p_frmSubForm.Show();
        }

        private frmBrothRecordsRec_F2 m_frmCurrentSub = null;
        private void m_mthSubFormClosed(object p_objSender, EventArgs p_objArg)
        {
            frmBrothRecordsRec_F2 frmAddNewForm = (frmBrothRecordsRec_F2)p_objSender;

            m_blnCanShowNewForm = true;
            m_frmCurrentSub = null;
        }

        // ��ȡ���̼�¼�������ʵ��
        protected override clsBaseCaseHistoryDomain m_objGetDomain()
        {
            return new clsBaseCaseHistoryDomain(enmBaseCaseHistoryTypeInfo.BrothRecords_F2);
        }

        private void ctmRecordControl_Popup(object sender, System.EventArgs e)
        {
            bool blnEnable = true;
            m_mniAddBabyCircsRecord.Enabled = blnEnable;
            m_mmiModifyBabyCircsRecord.Enabled = blnEnable;
            m_mmiDelBabyCircsRecord.Enabled = blnEnable;
        }

        #region ����
        protected override enmApproveType m_EnmAppType
        {
            get { return enmApproveType.CaseHistory; }
        }
        protected override string m_StrRecorder_ID
        {
            get
            {
                if (m_txtJieSheng.Tag != null)
                    return m_txtJieSheng.Tag.ToString();
                return "";

                if (m_txtZhuLi.Tag != null)
                    return m_txtZhuLi.Tag.ToString();
                return "";

                if (m_txtHuLi.Tag != null)
                    return m_txtHuLi.Tag.ToString();
                return "";

                if (m_txtZhiDao.Tag != null)
                    return m_txtZhiDao.Tag.ToString();
                return "";
            }

        }
        #endregion ����


        protected override void m_mthClearRecordInfo()
        {
            m_dtpGongSuoTime.Value = DateTime.Now;
            m_dtpPoMoTime.Value = DateTime.Now;
            m_dtpGongKouKaiTime.Value = DateTime.Now;
            m_cboTaiCi.Text = "";         
            m_cboTaiWei.Text = "";
            m_cboYunQi.Text = "";
            m_dtpBrothTime.Value = DateTime.Now;
            m_dtpTaiPanTime.Value = DateTime.Now;
            m_cboXingZhuang.Text = "";
            m_cboZhongLiang.Text = "";
            m_cboDaXiao.Text = "";
            m_cboWanZheng.Text = "";
            m_cboQiDai.Text = "";
            m_cboQiChang.Text = "";
            m_cboZzhiLiu.Text = "";
            m_cboNeiFeng.Text = "";
            m_cboWaiFeng.Text = "";
            m_cboShiXue.Text = "";
            m_cboGongDiGao.Text = "";
          //  m_txtHeadRound.m_mthClearText();
            m_cboGongJingQingKuang.Text = "";
            m_cboXueYa1.Text = "";
            m_cboXueYa2.Text = "";
            m_cboHuXi.Text = "";
            m_cboMaiBo.Text = "";
            m_cboSiWangYuanYin.Text = "";
            m_cboTiZhong.Text = "";
            m_cboShenChang.Text = "";
            m_cboShuangDingJing.Text = "";
            m_cboZhenJing.Text = "";
            m_cboXin.Text = "";
            m_cboFei.Text = "";
            m_cboJiXing.Text = "";
         //   m_txtOtherRecord.m_mthClearText();
         //   m_txtInHospitalDays.m_mthClearText();
         //   m_txtWeight.m_mthClearText();
            m_cboYiChanCheng.Text = "";
            m_cboErChanCheng.Text = "";
            m_cboSanChanCheng.Text = "";
            m_cboQuanCheng.Text = "";
            m_cboairenname.Text = "";
            m_cboage.Text = "";
            m_cbojiguan.Text = "";
            m_cbozhiye.Text = "";
            m_cborenzhi.Text = "";
            m_cbozhuzhi.Text = "";
            m_cbobabyid.Text = "";

           
        }

        protected override void m_mthUnEnableRichTextBox()
        {

        }

        protected override void m_mthEnableRichTextBox()
        {

        }

        // �����Ƿ����ѡ���˺ͼ�¼ʱ���б��ڴӲ��̼�¼�������ʱ��Ҫʹ�á�
        protected override void m_mthEnablePatientSelectSub(bool p_blnEnable)
        {

        }

        // ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        public override void m_strReloadFormTitle()
        {

        }
        // �Ƿ������޸������¼�ļ�¼��Ϣ��
        protected override void m_mthEnableModifySub(bool p_blnEnable)
        {

        }

        // �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
        protected clsBrothRecords_F2 m_objGetContentFromGUI()
        {
            clsBrothRecords_F2 objContent = new clsBrothRecords_F2();

            objContent.M_DTPGONGSUOTIME = m_dtpGongSuoTime.Value;
            objContent.M_DTPPOMOTIME = m_dtpPoMoTime.Value;
            string strCHILDBEARING = "";
            strCHILDBEARING += m_rdbZiPo.Checked ? "1" : "0";
            strCHILDBEARING += m_rdbShouShuPo.Checked ? "1" : "0";
            objContent.M_RDBMOPO = strCHILDBEARING;
            objContent.M_DTPGONGKOUKAITIME = m_dtpGongKouKaiTime.Value;
            objContent.M_CBOTAICI = m_cboTaiCi.Text;
            objContent.M_CBOYUNQI = m_cboYunQi.Text;
            objContent.M_CBOTAIWEI = m_cboTaiWei.Text;
            objContent.M_DTPBROTHTIME = m_dtpBrothTime.Value;
            string strCHILDBEARING2 = "";
            strCHILDBEARING2 += m_rdbZiRanChan.Checked ? "1" : "0";
            strCHILDBEARING2 += m_rdbShouShuChan.Checked ? "1" : "0";
            objContent.M_DTPBROTHTYPE = strCHILDBEARING2;
            objContent.M_DTPTAIPANTIME = m_dtpTaiPanTime.Value;
            string strCHILDBEARING3 = "";
            strCHILDBEARING3 += m_rdbZiRanMian.Checked ? "1" : "0";
            strCHILDBEARING3 += m_rdbPoChuMian.Checked ? "1" : "0";
            strCHILDBEARING3 += m_rdbBoLiMian.Checked ? "1" : "0";
            strCHILDBEARING3 += m_rdbChanShiMian.Checked ? "1" : "0";
            strCHILDBEARING3 += m_rdbPoShuiMian.Checked ? "1" : "0";
            objContent.M_RDBTAIPANTYPE = strCHILDBEARING3;
            objContent.M_CBOXINGZHUANG = m_cboXingZhuang.Text;
            objContent.M_CBOZHONGLIANG = m_cboZhongLiang.Text;
            objContent.M_CBODAXIAO = m_cboDaXiao.Text;
            objContent.M_CBOWANZHENG = m_cboWanZheng.Text;
            objContent.M_CBOQIDAI = m_cboQiDai.Text;
            objContent.M_CBOQICHANG = m_cboQiChang.Text;
            objContent.M_CBOZZHILIU = m_cboZzhiLiu.Text;

            string strCHILDBEARING4 = "";
            strCHILDBEARING4 += m_rdbWeiPo.Checked ? "1" : "0";
            strCHILDBEARING4 += m_rdbJiuPo.Checked ? "1" : "0";
            strCHILDBEARING4 += m_rdbXinPo.Checked ? "1" : "0";
            strCHILDBEARING4 += m_rdb1Du.Checked ? "1" : "0";
            strCHILDBEARING4 += m_rdb2Du.Checked ? "1" : "0";
            strCHILDBEARING4 += m_rdb3Du.Checked ? "1" : "0";           
            objContent.M_RDBHUIYINTYPE = strCHILDBEARING4;
            string strCHILDBEARING5 = "";
            strCHILDBEARING5 += m_rdbZuo.Checked ? "1" : "0";
            strCHILDBEARING5 += m_rdbYou.Checked ? "1" : "0";
            strCHILDBEARING5 += m_rdbZhengZhong.Checked ? "1" : "0";
            objContent.M_RDBHUIYINQIEKAITYPE = strCHILDBEARING5;

            objContent.M_CBONEIFENG = m_cboNeiFeng.Text;
            objContent.M_CBOWAIFENG = m_cboWaiFeng.Text;
            objContent.M_CBOSHIXUE = m_cboShiXue.Text;

            objContent.M_CBOGONGDIGAO = m_cboGongDiGao.Text;
            objContent.M_CBOGONGJINGQINGKUANG = m_cboGongJingQingKuang.Text;
            objContent.M_CBOXUEYA1 = m_cboXueYa1.Text;
            objContent.M_CBOXUEYA2 = m_cboXueYa2.Text;
            objContent.M_CBOHUXI = m_cboHuXi.Text;
            objContent.M_CBOMAIBO = m_cboMaiBo.Text;

            string strCHILDBEARING6 = "";
            strCHILDBEARING6 += m_rdbNan.Checked ? "1" : "0";
            strCHILDBEARING6 += m_rdbNv.Checked ? "1" : "0";
            strCHILDBEARING6 += m_rdbSiYing.Checked ? "1" : "0";
            strCHILDBEARING6 += m_rdbTai.Checked ? "1" : "0";
            objContent.M_RDBYINGER = strCHILDBEARING6;

            objContent.M_CBOSIWANGYUANYIN = m_cboSiWangYuanYin.Text;

            string strCHILDBEARING7 = "";
            strCHILDBEARING7 += m_rdbZiRanHuXi.Checked ? "1" : "0";
            strCHILDBEARING7 += m_rdbQingDuZiXi.Checked ? "1" : "0";
            strCHILDBEARING7 += m_rdbQingZiZiXi.Checked ? "1" : "0";
            strCHILDBEARING7 += m_rdbCangBaiZiXi.Checked ? "1" : "0";
            objContent.M_RDBHUXITYPE = strCHILDBEARING7;


            objContent.M_CBOTIZHONG = m_cboTiZhong.Text;
            objContent.M_CBOSHENCHANG = m_cboShenChang.Text;
            objContent.M_CBOSHUANGDINGJING = m_cboShuangDingJing.Text;
            objContent.M_CBOZHENJING = m_cboZhenJing.Text;
            objContent.M_CBOXIN = m_cboXin.Text;
            objContent.M_CBOFEI = m_cboFei.Text;
            objContent.M_CBOJIXING = m_cboJiXing.Text;
            objContent.M_CBOYICHANCHENG = m_cboYiChanCheng.Text;
            objContent.M_CBOERCHANCHENG = m_cboErChanCheng.Text;
            objContent.M_CBOSANCHANCHENG = m_cboSanChanCheng.Text;
            objContent.M_CBOQUANCHENG = m_cboQuanCheng.Text;

            objContent.M_TXTAIRENNAME = m_cboairenname.Text;
            objContent.M_TXTAGE = m_cboage.Text;
            objContent.M_TXTJIGUAN = m_cbojiguan.Text;
            objContent.M_TXTZHIYE = m_cbozhiye.Text;
            objContent.M_TXTRENZHI = m_cborenzhi.Text;
            objContent.M_TXTZHUZHI = m_cbozhuzhi.Text;
            objContent.M_TXTBABYID = m_cbobabyid.Text;
           
            if (m_txtJieSheng.Tag != null)
            {
                objContent.M_TXTJIESHENID = ((clsEmrEmployeeBase_VO)m_txtJieSheng.Tag).m_strEMPID_CHR;
            }
            else
            {
                objContent.M_TXTJIESHENID = string.Empty;
            }

            if (m_txtZhuLi.Tag != null)
            {
                objContent.M_TXTZHULIID = ((clsEmrEmployeeBase_VO)m_txtZhuLi.Tag).m_strEMPID_CHR;
            }
            else
            {
                objContent.M_TXTZHULIID = string.Empty;
            }


            if (m_txtHuLi.Tag != null)
            {
                objContent.M_TXTHULIID = ((clsEmrEmployeeBase_VO)m_txtHuLi.Tag).m_strEMPID_CHR;
            }
            else
            {
                objContent.M_TXTHULIID = string.Empty;
            }


            if (m_txtZhiDao.Tag != null)
            {
                objContent.M_TXTZHIDAOID= ((clsEmrEmployeeBase_VO)m_txtZhiDao.Tag).m_strEMPID_CHR;
            }
            else
            {
                objContent.M_TXTZHIDAOID = string.Empty;
            }

            objContent.M_TXTJIESHEN = m_txtJieSheng.Text;
            objContent.M_TXTZHULI = m_txtZhuLi.Text;
            objContent.M_TXTHULI = m_txtHuLi.Text;
            objContent.M_TXTZHIDAO = m_txtZhiDao.Text;

            return objContent;

            m_txtBedNO.Focus();
        }


        // �������¼��ֵ��ʾ�������ϡ�
        protected void m_mthSetGUIFromContent(clsBrothRecords_F2 objContent)
        {
            if (objContent.m_strInPatientID != null && objContent.m_strInPatientID != "")
            {
                m_strCurrentOpenDate = objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");
            }           

            m_dtpGongSuoTime.Value = objContent.M_DTPGONGSUOTIME;
            m_dtpPoMoTime.Value = objContent.M_DTPPOMOTIME;
            m_rdbZiPo.Checked = objContent.M_RDBMOPO[0].ToString() == "1" ? true : false;
            m_rdbShouShuPo.Checked = objContent.M_RDBMOPO[1].ToString() == "1" ? true : false;
            m_dtpGongKouKaiTime.Value = objContent.M_DTPGONGKOUKAITIME;
            m_cboTaiCi.Text = objContent.M_CBOTAICI;
            m_cboYunQi.Text = objContent.M_CBOYUNQI;
            m_cboTaiWei.Text = objContent.M_CBOTAIWEI;
            m_dtpBrothTime.Value = objContent.M_DTPBROTHTIME;
            m_rdbZiRanChan.Checked = objContent.M_DTPBROTHTYPE[0].ToString() == "1" ? true : false;
            m_rdbShouShuChan.Checked = objContent.M_DTPBROTHTYPE[1].ToString() == "1" ? true : false;
            m_dtpTaiPanTime.Value = objContent.M_DTPTAIPANTIME;
            m_rdbZiRanMian.Checked = objContent.M_RDBTAIPANTYPE[0].ToString() == "1" ? true : false;
            m_rdbPoChuMian.Checked = objContent.M_RDBTAIPANTYPE[1].ToString() == "1" ? true : false;
            m_rdbBoLiMian.Checked = objContent.M_RDBTAIPANTYPE[2].ToString() == "1" ? true : false;
            m_rdbChanShiMian.Checked = objContent.M_RDBTAIPANTYPE[3].ToString() == "1" ? true : false;
            m_rdbPoShuiMian.Checked = objContent.M_RDBTAIPANTYPE[4].ToString() == "1" ? true : false;

            m_cboXingZhuang.Text = objContent.M_CBOXINGZHUANG;
            m_cboZhongLiang.Text = objContent.M_CBOZHONGLIANG;
            m_cboDaXiao.Text = objContent.M_CBODAXIAO;
            m_cboWanZheng.Text = objContent.M_CBOWANZHENG;
            m_cboQiDai.Text = objContent.M_CBOQIDAI;
            m_cboQiChang.Text = objContent.M_CBOQICHANG;
            m_cboZzhiLiu.Text = objContent.M_CBOZZHILIU;

            m_rdbWeiPo.Checked = objContent.M_RDBHUIYINTYPE[0].ToString() == "1" ? true : false;
            m_rdbJiuPo.Checked = objContent.M_RDBHUIYINTYPE[1].ToString() == "1" ? true : false;
            m_rdbXinPo.Checked = objContent.M_RDBHUIYINTYPE[2].ToString() == "1" ? true : false;
            m_rdb1Du.Checked = objContent.M_RDBHUIYINTYPE[3].ToString() == "1" ? true : false;
            m_rdb2Du.Checked = objContent.M_RDBHUIYINTYPE[4].ToString() == "1" ? true : false;
            m_rdb3Du.Checked = objContent.M_RDBHUIYINTYPE[5].ToString() == "1" ? true : false;

            m_rdbZuo.Checked = objContent.M_RDBHUIYINQIEKAITYPE[0].ToString() == "1" ? true : false;
            m_rdbYou.Checked = objContent.M_RDBHUIYINQIEKAITYPE[1].ToString() == "1" ? true : false;
            m_rdbZhengZhong.Checked = objContent.M_RDBHUIYINQIEKAITYPE[2].ToString() == "1" ? true : false;

            m_cboNeiFeng.Text = objContent.M_CBONEIFENG;
            m_cboWaiFeng.Text = objContent.M_CBOWAIFENG;
            m_cboShiXue.Text = objContent.M_CBOSHIXUE;
            m_cboGongDiGao.Text = objContent.M_CBOGONGDIGAO;
            m_cboGongJingQingKuang.Text = objContent.M_CBOGONGJINGQINGKUANG;
            m_cboXueYa1.Text = objContent.M_CBOXUEYA1;
            m_cboXueYa2.Text = objContent.M_CBOXUEYA2;
            m_cboHuXi.Text = objContent.M_CBOHUXI;
            m_cboMaiBo.Text = objContent.M_CBOMAIBO;

            m_rdbNan.Checked = objContent.M_RDBYINGER[0].ToString() == "1" ? true : false;
            m_rdbNv.Checked = objContent.M_RDBYINGER[1].ToString() == "1" ? true : false;
            m_rdbSiYing.Checked = objContent.M_RDBYINGER[2].ToString() == "1" ? true : false;
            m_rdbTai.Checked = objContent.M_RDBYINGER[3].ToString() == "1" ? true : false;

            m_cboSiWangYuanYin.Text = objContent.M_CBOSIWANGYUANYIN;

            m_rdbZiRanHuXi.Checked = objContent.M_RDBHUXITYPE[0].ToString() == "1" ? true : false;
            m_rdbQingDuZiXi.Checked = objContent.M_RDBHUXITYPE[1].ToString() == "1" ? true : false;
            m_rdbQingZiZiXi.Checked = objContent.M_RDBHUXITYPE[2].ToString() == "1" ? true : false;
            m_rdbCangBaiZiXi.Checked = objContent.M_RDBHUXITYPE[3].ToString() == "1" ? true : false;

            m_cboTiZhong.Text = objContent.M_CBOTIZHONG;
            m_cboShenChang.Text = objContent.M_CBOSHENCHANG;
            m_cboShuangDingJing.Text = objContent.M_CBOSHUANGDINGJING;
            m_cboZhenJing.Text = objContent.M_CBOZHENJING;
            m_cboXin.Text = objContent.M_CBOXIN;
            m_cboFei.Text = objContent.M_CBOFEI;
            m_cboJiXing.Text = objContent.M_CBOJIXING;

            m_cboYiChanCheng.Text = objContent.M_CBOYICHANCHENG;
            m_cboErChanCheng.Text = objContent.M_CBOERCHANCHENG;
            m_cboSanChanCheng.Text = objContent.M_CBOSANCHANCHENG;
            m_cboQuanCheng.Text = objContent.M_CBOQUANCHENG;


            m_cboairenname.Text = objContent.M_TXTAIRENNAME;
            m_cboage.Text = objContent.M_TXTAGE;
            m_cbojiguan.Text = objContent.M_TXTJIGUAN;
            m_cbozhiye.Text = objContent.M_TXTZHIYE;
            m_cborenzhi.Text = objContent.M_TXTRENZHI;
            m_cbozhuzhi.Text = objContent.M_TXTZHUZHI;
            m_cbobabyid.Text = objContent.M_TXTBABYID;


            clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
            objEmployeeSign.m_lngGetEmpByID(objContent.M_TXTJIESHENID, out objEmpVO);
            if (objEmpVO != null)
            {
                m_txtJieSheng.Tag = objEmpVO;
                m_txtJieSheng.Text = objEmpVO.m_strLASTNAME_VCHR;
            }
            clsEmrEmployeeBase_VO objEmpVO1 = new clsEmrEmployeeBase_VO();
            objEmployeeSign.m_lngGetEmpByID(objContent.M_TXTZHULIID, out objEmpVO1);
            if (objEmpVO1 != null)
            {
                m_txtZhuLi.Tag = objEmpVO1;
                m_txtZhuLi.Text = objEmpVO1.m_strLASTNAME_VCHR;
            }
            clsEmrEmployeeBase_VO objEmpVO2 = new clsEmrEmployeeBase_VO();
            objEmployeeSign.m_lngGetEmpByID(objContent.M_TXTHULIID, out objEmpVO2);
            if (objEmpVO2 != null)
            {
                m_txtHuLi.Tag = objEmpVO2;
                m_txtHuLi.Text = objEmpVO2.m_strLASTNAME_VCHR;
            }
            clsEmrEmployeeBase_VO objEmpVO3 = new clsEmrEmployeeBase_VO();
            objEmployeeSign.m_lngGetEmpByID(objContent.M_TXTZHIDAOID, out objEmpVO3);
            if (objEmpVO3 != null)
            {
                m_txtZhiDao.Tag = objEmpVO3;
                m_txtZhiDao.Text = objEmpVO3.m_strLASTNAME_VCHR;
            }

            m_objCurrentRecordContent = objContent;

            m_txtBedNO.Focus();
        }


        // ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        protected override void m_mthReAddNewRecord(clsInPatientCaseHistoryContent p_objRecordContent)
        {

        }

        protected override void m_mthHandleAddRecordSucceed()
        {
            if (trvTime.SelectedNode != null)
                trvTime.SelectedNode.Tag = (string)m_objCurrentRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //���
        protected override string m_StrCurrentOpenDate
        {
            get
            {
                //if(this.trvTime.SelectedNode==null || this.trvTime.SelectedNode.Tag==null)
                //{
                //    clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
                //    return "";
                //}
                //return (string)this.trvTime.SelectedNode.Tag;
                if (string.IsNullOrEmpty(m_strCurrentOpenDate))
                {
                    clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
                    return string.Empty;
                }
                return m_strCurrentOpenDate;
            }
        }

        protected override bool m_BlnCanApprove
        {
            get
            {
                return true;
            }
        }


        protected override void m_mthSetNewRecord()
        {
            if (m_objCurrentPatient != null)
            {
                //ǩ��Ĭ��ֵ
                //clsEmployeeSignTool.s_mthSetDefaulEmployee(m_txtDoctorSign);
                //clsEmployeeSignTool.s_mthSetDefaulEmployee(m_txtCheckDocSign);
                MDIParent.m_mthSetDefaulEmployee(m_txtJieSheng);
                MDIParent.m_mthSetDefaulEmployee(m_txtZhuLi);
                MDIParent.m_mthSetDefaulEmployee(m_txtHuLi);
                MDIParent.m_mthSetDefaulEmployee(m_txtZhiDao);

                //Ĭ��ֵ m_IntCurCase
                new clsDefaultValueTool(this, m_objCurrentPatient).m_mthSetDefaultValue();

                //����Ĭ��ֵ��ص���괲��
                m_txtBedNO.Focus();

            }
        }

        /// <summary>
        /// ��ȡ��ǰ���˵���������
        /// </summary>
        /// <param name="p_dtmRecordDate">��¼����</param>
        /// <param name="p_intFormID">����ID</param>
        protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate, int p_intFormID)
        {
            clsBrothRecords_F2 objContent = new clsBrothRecords_F2();
            if (m_objBaseCurrentPatient == null || m_objBaseCurrentPatient.m_StrInPatientID == null || m_objBaseCurrentPatient.m_DtmSelectedInDate == DateTime.MinValue)
            {
                return;
            }

            long lngRes = m_objDomain.m_lngGetDeleteRecordContent(m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
            if (lngRes <= 0 || objContent == null)
            {
                switch (lngRes)
                {
                    case (long)(iCareData.enmOperationResult.Not_permission):
                        m_mthShowNotPermitted(); break;
                    case (long)(iCareData.enmOperationResult.DB_Fail):
                        m_mthShowDBError(); break;
                }
                return;
            }

            m_dtpGongSuoTime.Value = objContent.M_DTPGONGSUOTIME;
            m_dtpPoMoTime.Value = objContent.M_DTPPOMOTIME;
            m_rdbZiPo.Checked = objContent.M_RDBMOPO[0].ToString() == "1" ? true : false;
            m_rdbShouShuPo.Checked = objContent.M_RDBMOPO[1].ToString() == "1" ? true : false;
            m_dtpGongKouKaiTime.Value = objContent.M_DTPGONGKOUKAITIME;
            m_cboTaiCi.Text = objContent.M_CBOTAICI;
            m_cboYunQi.Text = objContent.M_CBOYUNQI;
            m_cboTaiWei.Text = objContent.M_CBOTAIWEI;
            m_dtpBrothTime.Value = objContent.M_DTPBROTHTIME;
            m_rdbZiRanChan.Checked = objContent.M_DTPBROTHTYPE[0].ToString() == "1" ? true : false;
            m_rdbShouShuChan.Checked = objContent.M_DTPBROTHTYPE[1].ToString() == "1" ? true : false;
            m_dtpTaiPanTime.Value = objContent.M_DTPTAIPANTIME;
            m_rdbZiRanMian.Checked = objContent.M_RDBTAIPANTYPE[0].ToString() == "1" ? true : false;
            m_rdbPoChuMian.Checked = objContent.M_RDBTAIPANTYPE[1].ToString() == "1" ? true : false;
            m_rdbBoLiMian.Checked = objContent.M_RDBTAIPANTYPE[2].ToString() == "1" ? true : false;
            m_rdbChanShiMian.Checked = objContent.M_RDBTAIPANTYPE[3].ToString() == "1" ? true : false;
            m_rdbPoShuiMian.Checked = objContent.M_RDBTAIPANTYPE[4].ToString() == "1" ? true : false;

            m_cboXingZhuang.Text = objContent.M_CBOXINGZHUANG;
            m_cboZhongLiang.Text = objContent.M_CBOZHONGLIANG;
            m_cboDaXiao.Text = objContent.M_CBODAXIAO;
            m_cboWanZheng.Text = objContent.M_CBOWANZHENG;
            m_cboQiDai.Text = objContent.M_CBOQIDAI;
            m_cboQiChang.Text = objContent.M_CBOQICHANG;
            m_cboZzhiLiu.Text = objContent.M_CBOZZHILIU;

            m_rdbWeiPo.Checked = objContent.M_RDBHUIYINTYPE[0].ToString() == "1" ? true : false;
            m_rdbJiuPo.Checked = objContent.M_RDBHUIYINTYPE[1].ToString() == "1" ? true : false;
            m_rdbXinPo.Checked = objContent.M_RDBHUIYINTYPE[2].ToString() == "1" ? true : false;
            m_rdb1Du.Checked = objContent.M_RDBHUIYINTYPE[3].ToString() == "1" ? true : false;
            m_rdb2Du.Checked = objContent.M_RDBHUIYINTYPE[4].ToString() == "1" ? true : false;
            m_rdb3Du.Checked = objContent.M_RDBHUIYINTYPE[5].ToString() == "1" ? true : false;

            m_rdbZuo.Checked = objContent.M_RDBHUIYINQIEKAITYPE[0].ToString() == "1" ? true : false;
            m_rdbYou.Checked = objContent.M_RDBHUIYINQIEKAITYPE[1].ToString() == "1" ? true : false;
            m_rdbZhengZhong.Checked = objContent.M_RDBHUIYINQIEKAITYPE[2].ToString() == "1" ? true : false;

            m_cboNeiFeng.Text = objContent.M_CBONEIFENG;
            m_cboWaiFeng.Text = objContent.M_CBOWAIFENG;
            m_cboShiXue.Text = objContent.M_CBOSHIXUE;
            m_cboGongDiGao.Text = objContent.M_CBOGONGDIGAO;
            m_cboGongJingQingKuang.Text = objContent.M_CBOGONGJINGQINGKUANG;
            m_cboXueYa1.Text = objContent.M_CBOXUEYA1;
            m_cboXueYa2.Text = objContent.M_CBOXUEYA2;
            m_cboHuXi.Text = objContent.M_CBOHUXI;
            m_cboMaiBo.Text = objContent.M_CBOMAIBO;

            m_rdbNan.Checked = objContent.M_RDBYINGER[0].ToString() == "1" ? true : false;
            m_rdbNv.Checked = objContent.M_RDBYINGER[1].ToString() == "1" ? true : false;
            m_rdbSiYing.Checked = objContent.M_RDBYINGER[2].ToString() == "1" ? true : false;
            m_rdbTai.Checked = objContent.M_RDBYINGER[3].ToString() == "1" ? true : false;

            m_cboSiWangYuanYin.Text = objContent.M_CBOSIWANGYUANYIN;

            m_rdbZiRanHuXi.Checked = objContent.M_RDBHUXITYPE[0].ToString() == "1" ? true : false;
            m_rdbQingDuZiXi.Checked = objContent.M_RDBHUXITYPE[1].ToString() == "1" ? true : false;
            m_rdbQingZiZiXi.Checked = objContent.M_RDBHUXITYPE[2].ToString() == "1" ? true : false;
            m_rdbCangBaiZiXi.Checked = objContent.M_RDBHUXITYPE[3].ToString() == "1" ? true : false;

            m_cboTiZhong.Text = objContent.M_CBOTIZHONG;
            m_cboShenChang.Text = objContent.M_CBOSHENCHANG;
            m_cboShuangDingJing.Text = objContent.M_CBOSHUANGDINGJING;
            m_cboZhenJing.Text = objContent.M_CBOZHENJING;
            m_cboXin.Text = objContent.M_CBOXIN;
            m_cboFei.Text = objContent.M_CBOFEI;
            m_cboJiXing.Text = objContent.M_CBOJIXING;

            m_cboYiChanCheng.Text = objContent.M_CBOYICHANCHENG;
            m_cboErChanCheng.Text = objContent.M_CBOERCHANCHENG;
            m_cboSanChanCheng.Text = objContent.M_CBOSANCHANCHENG;
            m_cboQuanCheng.Text = objContent.M_CBOQUANCHENG;        
           
            m_txtJieSheng.Text = objContent.M_TXTJIESHEN;
            m_txtZhuLi.Text = objContent.M_TXTZHULIID;
            m_txtHuLi.Text = objContent.M_TXTHULIID;
            m_txtZhiDao.Text = objContent.M_TXTZHIDAO;

            m_cboairenname.Text = objContent.M_TXTAIRENNAME;
            m_cboage.Text = objContent.M_TXTAGE;
            m_cbojiguan.Text = objContent.M_TXTJIGUAN;
            m_cbozhiye.Text = objContent.M_TXTZHIYE;
            m_cborenzhi.Text = objContent.M_TXTRENZHI;
            m_cbozhiye.Text = objContent.M_TXTZHIYE;
            m_cbobabyid.Text = objContent.M_TXTBABYID;

            m_txtBedNO.Focus();
        }

        public override int m_IntFormID
        {
            get
            {
                return 182;
            }
        }

        protected new void m_mthSetSelectedRecord(clsPatient p_objPatient,
        string p_strRecordTime)
        {
            //������
            if (p_objPatient == null || m_ObjCurrentEmrPatientSession == null)
            {
                m_objCurrentRecordContent = null;
                return;
            }

            clsBaseCaseHistoryInfo objContent = null;
            clsPictureBoxValue[] objPicValueArr = null;
            //��ȡ��¼

            long lngRes = m_objDomain.m_lngGetRecordContent(p_objPatient.m_StrInPatientID, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss"),/*p_strRecordTime ,*/ out objContent, out objPicValueArr);

            if (lngRes <= 0 || objContent == null)
            {
                m_objCurrentRecordContent = null;
                return;
            }

            //���ü�¼ʱ��     
            m_objCurrentRecordContent = (clsBrothRecords_F2)objContent;

            m_mthSetGUIFromContent((clsBrothRecords_F2)objContent);
            m_mthEnableModify(false);

            m_mthSetModifyControl((clsBrothRecords_F2)objContent, false);

        }

        /// <summary>
        /// �����Ƿ�����޸ģ��޸����ۼ�����
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_blnReset"></param>
        protected void m_mthSetModifyControl(clsBrothRecords_F2 p_objRecordContent,
            bool p_blnReset)
        {
            //������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
            if (p_blnReset == true)
            {
                m_mthSetRichTextModifyColor(this, clsHRPColor.s_ClrInputFore);
                m_mthSetRichTextCanModifyLast(this, true);
            }
            else if (p_objRecordContent != null)
            {
                m_mthSetRichTextModifyColor(this, Color.Red);
                m_mthSetRichTextCanModifyLast(this, m_blnGetCanModifyLast(p_objRecordContent.m_strModifyUserID));
            }

        }


        /// <summary>
        /// ���ô����пؼ������ı�����ɫ
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_clrColor"></param>
        private void m_mthSetRichTextModifyColor(Control p_ctlControl, System.Drawing.Color p_clrColor)
        {
            #region ���ÿؼ������ı�����ɫ,Jacky-2003-3-24
            string strTypeName = p_ctlControl.GetType().FullName;
            if (strTypeName == "com.digitalwave.Utility.Controls.ctlRichTextBox")
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;
            else if (strTypeName == "com.digitalwave.controls.ctlRichTextBox")
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;

            if (p_ctlControl.HasChildren && strTypeName != "System.Windows.Forms.DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextModifyColor(subcontrol, p_clrColor);
                }
            }
            #endregion
        }

        private void m_mthSetRichTextCanModifyLast(Control p_ctlControl, bool p_blnCanModifyLast)
        {
            #region ���ÿؼ������ı����Ƿ�����޸�,Jacky-2003-3-24
            string strTypeName = p_ctlControl.GetType().FullName;
            if (strTypeName == "com.digitalwave.Utility.Controls.ctlRichTextBox")
            {
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
            }
            else if (strTypeName == "com.digitalwave.controls.ctlRichTextBox")
            {
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
            }

            if (p_ctlControl.HasChildren && strTypeName != "System.Windows.Forms.DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextCanModifyLast(subcontrol, p_blnCanModifyLast);
                }
            }
            #endregion
        }

        protected new long m_lngAddNewRecord()
        {
            //��鵱ǰ���˱����Ƿ�Ϊnull
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return (long)enmOperationResult.Parameter_Error;

            //��ȡ������ʱ��
            clsPublicDomain m_objPDomain = new clsPublicDomain();

            //�ӽ����ȡ��¼��Ϣ
            clsBrothRecords_F2 objContent = m_objGetContentFromGUI();

            //��ȡ��ͼ��Ϣ
            clsPictureBoxValue[] objPicValueArr = m_objGetPicContentFromGUI();

            string strDiseaseID = "";
            //��������ֵ����
            if (objContent == null)
                return (long)enmOperationResult.Parameter_Error;

            //���� clsInPatientCaseHistoryContent ����Ϣ��ʹ�÷�����ʱ������m_dtmOpenDate��m_dtmModifyDate��
            string strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            objContent.m_bytIfConfirm = 0;
            objContent.m_bytStatus = 0;
            objContent.m_dtmInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
            objContent.m_dtmModifyDate = DateTime.Parse(strNow);
            objContent.m_dtmOpenDate = DateTime.Parse(strNow);
            objContent.m_strCreateUserID = MDIParent.strOperatorID;
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_strModifyUserID = MDIParent.strOperatorID;
            objContent.m_dtmCreateDate = DateTime.Parse(strNow);

            //�����¼
            clsPreModifyInfo p_objModifyInfo = null;

            long lngRes = m_objDomain.m_lngAddNewRecord(objContent, objPicValueArr, strDiseaseID, out p_objModifyInfo);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:

                    if ((enmOperationResult)lngRes == enmOperationResult.DB_Succeed)
                    {
                        m_objCurrentRecordContent = objContent;

                        m_mthHandleAddRecordSucceed();

                     //   this.m_dtpCheckDate.Enabled = false;
                    }

                    break;
                //...
                case enmOperationResult.Record_Already_Exist:
                    m_mthShowRecordTimeDouble();
                    return lngRes;
            }
            this.trvTime.ExpandAll();
            //���ؽ��
            return lngRes;
        }

        protected override long m_lngSubModify()
        {
            //if (trvTime.Nodes[0].Nodes.Count > 0 && trvTime.SelectedNode != trvTime.Nodes[0].FirstNode)
            //    return 1;//����ֻ����
            //��鵱ǰ���˱����Ƿ�Ϊnull
            if (m_objCurrentPatient == null)
                return (long)enmOperationResult.Parameter_Error;
            //��ȡ������ʱ��
            clsPublicDomain m_objPDomain = new clsPublicDomain();
            //�ӽ����ȡ��¼��Ϣ
            clsBrothRecords_F2 objContent = m_objGetContentFromGUI();

            //��ȡ��ͼ��Ϣ
            clsPictureBoxValue[] objPicValueArr = m_objGetPicContentFromGUI();

            //��ȡ����
            string strDiseaseID = "";

            //��������ֵ����           
            if (objContent == null)
                return (long)enmOperationResult.Parameter_Error;

            //���� clsInPatientCaseHistoryContent ����Ϣ��ʹ�÷�����ʱ������m_dtmModifyDate��
            objContent.m_bytIfConfirm = 0;
            objContent.m_bytStatus = 0;
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_dtmModifyDate = DateTime.Parse(m_objPDomain.m_strGetServerTime());
            objContent.m_dtmCreateDate = m_objCurrentRecordContent.m_dtmCreateDate;
            objContent.m_strCreateUserID = MDIParent.strOperatorID;
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_strModifyUserID = MDIParent.strOperatorID;

            //�������м�¼�Ŀ�ʼʹ��ʱ��
            objContent.m_dtmOpenDate = m_objCurrentRecordContent.m_dtmOpenDate;

            clsPreModifyInfo m_objModifyInfo;
            long lngRes = m_objDomain.m_lngModifyRecord(m_objCurrentRecordContent, objContent, objPicValueArr, strDiseaseID, out m_objModifyInfo);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:

                    if ((enmOperationResult)lngRes == enmOperationResult.DB_Succeed)
                    {
                        m_objCurrentRecordContent = objContent;
                    }
                    break;
                //...
            }
            //չ������ʾ����ʱ��ڵ㡣
            //			trvTime.ExpandAll();
            //���ؽ��
            return lngRes;
        }

        protected override long m_lngSubDelete()
        {
            //��鵱ǰ���˱����Ƿ�Ϊnull  
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
            {
                clsPublicFunction.ShowInformationMessageBox("δѡ������,�޷�ɾ��!");//�޺�褣�2003-5-27
                return (long)enmOperationResult.Parameter_Error;
            }
            //��鵱ǰ��¼�Ƿ�Ϊnull
            if (m_objCurrentRecordContent == null)
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ��¼����Ϊ��,�޷�ɾ��!");//�޺�褣�2003-5-27
                return (long)enmOperationResult.Parameter_Error;
            }
            //��ȡ������ʱ��      
            clsPublicDomain m_objPDomain = new clsPublicDomain();

            //ɾ����¼
            clsBrothRecords_F2 objContent = m_objGetContentFromGUI();
            objContent.m_bytStatus = 0;
            objContent.m_dtmCreateDate = m_objCurrentRecordContent.m_dtmCreateDate;
            objContent.m_dtmInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_strDeActivedOperatorID = MDIParent.OperatorID;
            objContent.m_dtmOpenDate = m_objCurrentRecordContent.m_dtmOpenDate;

            //���� m_objCurrentRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmDeActivedDate��
            objContent.m_dtmDeActivedDate = DateTime.Parse(m_objPDomain.m_strGetServerTime());

            clsPreModifyInfo m_objModifyInfo = null;

            long lngRes = m_objDomain.m_lngDeleteRecord(objContent, out m_objModifyInfo);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    //��ռ�¼��Ϣ   

                    m_objCurrentRecordContent = null;
                    m_mthClearPatientRecordInfo();
                    //ѡ�и��ڵ�
                    m_blnCanTreeAfterSelect = false;
                    m_mthUnEnableRichTextBox();
                    m_blnCanTreeAfterSelect = true;

                    m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
                    break;
                //...
            }

            //���ؽ��
            return lngRes;
        }

        // �������������ݿⱣ�档
        protected new long m_lngReAddNew()
        {
            //��鵱ǰ���˱����Ƿ�Ϊnull

            //��ȡ������ʱ��

            //�ӽ����ȡ��¼��Ϣ
            clsBrothRecords_F2 objContent = m_objGetContentFromGUI();

            //��������ֵ����           
            if (objContent == null)
                return -1;
            clsPreModifyInfo m_objModifyInfo = null;
            long lngRes = m_objDomain.m_lngReAddNewRecord(m_objReAddNewOld, objContent, out m_objModifyInfo);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    m_objCurrentRecordContent = objContent;
                    m_objReAddNewOld = null;
                    break;
                //...
            }

            //���ؽ��
            return lngRes;

        }

        /// <summary>
        /// ������ڣ�������ɫ�����÷���
        /// ����ü�¼������޸��˾��ǵ�ǰ�ĵ�½�ˣ������޸ĸü�¼
        /// ���򣬲����޸�
        /// </summary>
        /// <returns></returns>
        private bool m_blnGetCanModifyLast(string p_strModifyUserID)
        {
            if (p_strModifyUserID == null || p_strModifyUserID.Trim() == MDIParent.OperatorID.Trim())
                return true;
            else
                return false;
        }

        protected override void trvTime_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (!m_blnCanTreeAfterSelect)
                return;

            m_mthRecordChangedToSave();

            try
            {
                DateTime dtInDate = DateTime.Parse(trvTime.SelectedNode.Text);
                m_mthClearRecordInfo();

                txtInPatientID.Text = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_StrHISInPatientID;
                DateTime dtmEMRInDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_DtmEMRInDate;
                string strEMRInPatientID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_StrEMRInPatientID;

                m_objCurrentPatient.m_StrHISInPatientID = txtInPatientID.Text;
                m_objCurrentPatient.m_DtmSelectedHISInDate = Convert.ToDateTime(trvTime.SelectedNode.Text);
                m_objCurrentPatient.m_DtmSelectedInDate = dtmEMRInDate;
                m_objBaseCurrentPatient.m_DtmSelectedInDate = dtmEMRInDate;

                if (string.IsNullOrEmpty(m_objBaseCurrentPatient.m_StrRegisterId))
                {
                    string strRegisterID = string.Empty;
                    long lngRes = new clsPublicDomain().m_lngGetRegisterID(m_objCurrentPatient.m_StrPatientID, Convert.ToDateTime(trvTime.SelectedNode.Text).ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
                    m_objBaseCurrentPatient.m_StrRegisterId = strRegisterID;
                    m_objCurrentPatient.m_StrRegisterId = strRegisterID;
                }
                m_mthIsReadOnly();

                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                m_mthEnableRichTextBox();
              //  m_dtpInHospitalDate.Value = dtInDate;
                m_mthSetSelectedRecord(m_objCurrentPatient, (string)this.trvTime.SelectedNode.Tag);
                if (m_objCurrentRecordContent != null)
                {
                    this.m_dtpCreateDate.Enabled = true;

                    //��ǰ�����޸ļ�¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.Modify);
                }
                else
                {
                    m_mthSetNewRecord();
                    //��ǰ����������¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.AddNew);
                }
            }
            catch (Exception exp)
            {
                string strtemp = exp.Message;
                m_mthClearRecordInfo();

                m_mthUnEnableRichTextBox();

                m_objCurrentRecordContent = null;
                m_mthEnableModify(true);
                this.m_dtpCreateDate.Enabled = true;
                this.m_dtpCreateDate.Text = DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss");

                m_mthSetNullPrintContext();

                //��ǰ���ڽ�ֹ����״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.None);
            }
            finally
            {
                m_mthDoAfterSelect();
                m_mthAddFormStatusForClosingSave();
            }
        }

        /// <summary>
        /// ѡ�����ڵ��Ĳ���
        /// </summary>
        protected override void m_mthDoAfterSelect()
        {
            object[][] objDataArr;
            clsBrothRecords_F2Rec[] objCircsRecordArr;
            DateTime dtSelectedInPatientDate = DateTime.MinValue;
            //try
            //{
            //    dtSelectedInPatientDate = DateTime.Parse(trvTime.SelectedNode.Text.Trim());
            //}
            //catch
            //{
            //    m_dtbRecords.Clear();
            //    return;
            //}
            if (m_ObjCurrentEmrPatientSession != null)
            {
                dtSelectedInPatientDate = m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate;
            }
            else
            {
                m_dtbRecords.Clear();
                return;
            }

            long lngRes = m_objDomain.m_lngGetAllCircsRecordContent(m_objCurrentPatient.m_StrInPatientID, dtSelectedInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_dtpCreateDate.Text, out objCircsRecordArr);
            objDataArr = m_objGetRecordsValueArr(objCircsRecordArr);

            m_dtbRecords.Clear();
            if (objDataArr == null)
                return;
            for (int j2 = 0; j2 < objDataArr.Length; j2++)
            {
                m_dtbRecords.Rows.Add(objDataArr[j2]);
            }
        }

        /// <summary>
        /// ��Ӽ�¼
        /// </summary>
        /// <returns></returns>
        protected override long m_lngSubAddNew()
        {
            if (m_objReAddNewOld != null)
                return m_lngReAddNew();
            else
                return m_lngAddNewRecord();


        }

        /// <summary>
        /// �Ƿ�������¼�¼�Ĳ�����true������¼�¼��false,�޸ļ�¼
        /// </summary>
        protected override bool m_BlnIsAddNew
        {
            get
            {
                return m_objCurrentRecordContent == null;
            }
        }

        /// <summary>
        /// ��ȡ�ۼ�����
        /// </summary>
        /// <param name="p_strText"></param>
        /// <param name="p_strModifyUserID"></param>
        /// <param name="p_strModifyUserName"></param>
        /// <returns></returns>
        private string m_strGetDSTTextXML(string p_strText, string p_strModifyUserID, string p_strModifyUserName)
        {
            return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText, p_strModifyUserID, p_strModifyUserName, Color.Black, Color.White);
        }

        private DataTable m_dtbTempTable;
        private DateTime m_dtmPreRecordDate;
        private object[][] m_objGetRecordsValueArr(clsBrothRecords_F2Rec[] p_objTransDataInfo)
        {
            #region ��ʾ��¼��DataGrid
            try
            {
                object[] objData;
                ArrayList objReturnData = new ArrayList();
                m_dtmPreRecordDate = DateTime.MinValue;

                com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
                string strText, strXml;

                if (p_objTransDataInfo == null)
                    return null;

                int intRecordCount = p_objTransDataInfo.Length;

                for (int i = 0; i < intRecordCount; i++)
                {
                    clsBrothRecords_F2Rec objCurrent = p_objTransDataInfo[i];
                    clsBrothRecords_F2Rec objNext = new clsBrothRecords_F2Rec();//��һ����¼
                    if (i < intRecordCount - 1)
                        objNext = p_objTransDataInfo[i + 1];
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate)
                    {
                        continue;
                    }

                    #region ��Źؼ��ֶ�
                    objData = new object[15];
                    if (objCurrent.m_dtmCreateDate != DateTime.MinValue)
                    {
                        objData[0] = objCurrent.m_dtmCreateDate;//��ż�¼ʱ����ַ���
                        objData[1] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                        objData[2] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   


                        if (objCurrent.m_dtmRecordDate.ToString("yyyy-MM-dd HH:mm") != m_dtmPreRecordDate.ToString("yyyy-MM-dd HH:mm"))
                        {
                            objData[3] = objCurrent.m_dtmRecordDate.ToString("yyyy-MM-dd HH:mm");//�����ַ���
                        }
                    }
                    m_dtmPreRecordDate = objCurrent.m_dtmRecordDate;
                    #endregion ;

                    #region ��ŵ�����Ϣ
                    //��������
                  //  objData[4] = objCurrent.m_strBIRTHDAYS;

                    //Ѫѹ
                    strText = objCurrent.str_XUEYA;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_XUEYA != objCurrent.str_XUEYA)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_XUEYA, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[4] = objclsDSTRichTextBoxValue;

                    //������Ъ
                    strText = objCurrent.str_GONGSUOJIANXUE;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_GONGSUOJIANXUE != objCurrent.str_GONGSUOJIANXUE)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_GONGSUOJIANXUE, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[5] = objclsDSTRichTextBoxValue;

                    //������ʱ
                    strText = objCurrent.str_GONGSUOTIME;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_GONGSUOTIME != objCurrent.str_GONGSUOTIME)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_GONGSUOTIME, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[6] = objclsDSTRichTextBoxValue;

                    //̥��
                    strText = objCurrent.str_TAIXIN;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_TAIXIN != objCurrent.str_TAIXIN)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_TAIXIN, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[7] = objclsDSTRichTextBoxValue;

                    //���ڿ���
                    strText = objCurrent.str_GONGKOU;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_GONGKOU != objCurrent.str_GONGKOU)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_GONGKOU, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[8] = objclsDSTRichTextBoxValue;

                    //̥Ĥ���
                    strText = objCurrent.str_TAIMO;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_TAIMO != objCurrent.str_TAIMO)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_TAIMO, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[9] = objclsDSTRichTextBoxValue;

                    //��¶�ߵ�
                    strText = objCurrent.str_XIANLU;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_XIANLU != objCurrent.str_XIANLU)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_XIANLU, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[10] = objclsDSTRichTextBoxValue;

                    //��鷨
                    strText = objCurrent.str_JIANCHAFA;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_JIANCHAFA != objCurrent.str_JIANCHAFA)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_JIANCHAFA, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[11] = objclsDSTRichTextBoxValue;

                    //���ͼ�����
                    strText = objCurrent.str_GONGDIJIZHOU;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_GONGDIJIZHOU != objCurrent.str_GONGDIJIZHOU)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_GONGDIJIZHOU, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[12] = objclsDSTRichTextBoxValue;

                    //�������������
                    strText = objCurrent.str_FENMIWU;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.str_FENMIWU != objCurrent.str_FENMIWU)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.str_FENMIWU, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new com.digitalwave.Utility.Controls.clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[13] = objclsDSTRichTextBoxValue;                   
                 
                    //ǩ��	
                    objData[14] = objCurrent.m_strSignUserName;

                    objReturnData.Add(objData);
                    #endregion
                }
                object[][] m_objRe = new object[objReturnData.Count][];

                for (int m = 0; m < objReturnData.Count; m++)
                    m_objRe[m] = (object[])objReturnData[m];
                return m_objRe;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            #endregion
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
            m_dtbRecords.Clear();
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
                    m_dtbRecords.Rows.Add(p_objDataArr[i1]);
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
        }

        /// <summary>
        /// ʹ��DataGrid�ĵ�һ�л�ý���
        /// </summary>
        protected void m_mthSetDataGridFirstRowFocus()
        {
            m_dtgRecord.CurrentCell = new DataGridCell(m_dtbRecords.Rows.Count, 0);
        }

        protected override long m_lngSubPrint()
        {
            m_mthPrintRecord();
            return 1;
        }


        //��ӡ
        private clsBrothRecords_F2PrintTool objPrintTool;
        private void m_mthPrintRecord()
        {
            objPrintTool = new clsBrothRecords_F2PrintTool();
            objPrintTool.m_mthInitPrintTool(null);
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
            else
            {
                m_objBaseCurrentPatient.m_StrHISInPatientID = m_ObjCurrentEmrPatientSession.m_strHISInpatientId;
                m_objBaseCurrentPatient.m_DtmSelectedHISInDate = m_ObjCurrentEmrPatientSession.m_dtmHISInpatientDate;
                DateTime dtmTemp = DateTime.MinValue;
                if (!DateTime.TryParse(m_strCurrentOpenDate, out dtmTemp))
                    objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_ObjLastEmrPatientSession.m_dtmEMRInpatientDate, DateTime.MinValue);
                else
                    objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate, DateTime.Parse(m_strCurrentOpenDate));
            }
            objPrintTool.m_mthInitPrintContent();

            m_mthStartPrint();
        }


        protected override void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            objPrintTool.m_mthPrintPage(e);

            if (ppdPrintPreview != null)
                while (!ppdPrintPreview.m_blnHandlePrint(e))
                    objPrintTool.m_mthPrintPage(e);
        }

        protected override void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthBeginPrint(e);
        }

        protected override void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthEndPrint(e);
        }

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null)
                return;

            m_mthRecordChangedToSave();

            try
            {
                DateTime dtInDate = p_objSelectedSession.m_dtmHISInpatientDate;
                m_mthClearRecordInfo();

                DateTime dtmEMRInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                string strEMRInPatientID = p_objSelectedSession.m_strEMRInpatientId;

                m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
                m_objCurrentPatient.m_DtmSelectedHISInDate = dtInDate;
                m_objCurrentPatient.m_DtmSelectedInDate = dtmEMRInDate;
                m_objBaseCurrentPatient.m_DtmSelectedInDate = dtmEMRInDate;

                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;

                m_mthIsReadOnly();

                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                m_mthEnableRichTextBox();
            //    m_dtpInHospitalDate.Value = dtInDate;
                m_mthSetSelectedRecord(m_objCurrentPatient, string.Empty);
                if (m_objCurrentRecordContent != null)
                {
                    this.m_dtpCreateDate.Enabled = true;

                    //��ǰ�����޸ļ�¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.Modify);
                }
                else
                {
                    m_mthSetNewRecord();
                    //��ǰ����������¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.AddNew);
                }
            }
            catch (Exception exp)
            {
                string strtemp = exp.Message;
                m_mthClearRecordInfo();

                m_mthUnEnableRichTextBox();

                m_objCurrentRecordContent = null;
                m_mthEnableModify(true);
                this.m_dtpCreateDate.Enabled = true;
                this.m_dtpCreateDate.Text = DateTime.Now.ToString("yyyy��MM��dd�� HH:mm:ss");

                m_mthSetNullPrintContext();

                //��ǰ���ڽ�ֹ����״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.None);
            }
            finally
            {
                m_mthDoAfterSelect();
                m_mthAddFormStatusForClosingSave();
            }
        }
        
    }
}