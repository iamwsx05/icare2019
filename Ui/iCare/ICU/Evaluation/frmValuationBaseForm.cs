using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
using iCare.ICU;
//using iCare.ICU.Espial;
//using com.digitalwave.iCare.middletier.ICU; 
using com.digitalwave.iCare.common;
//using iCare.Common;
//using iCare.Controls;
using com.digitalwave.Emr.StaticObject;

namespace iCare.ICU.Evaluation
{
    /// <summary>
    /// �������ָ�����
    /// </summary>
    public partial class frmValuationBaseForm : frmViewBase, PublicFunction
    {
        #region Declare
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        protected System.Windows.Forms.TreeView trvActivityTime;

        /// <summary>
        /// ��ǰ����
        /// </summary>
        //protected clsPatient m_objCurrentPatient;
        /// <summary>
        /// �Ƿ񴥷��ı��ı��¼�
        /// </summary>
        protected bool m_blnCanTextChange = true;
        protected string m_strInPatientID;
        protected string m_strInPatientDate;
        protected string m_strCreateDate = "";
        protected System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
        private const string c_strPromptForNum = "����������!";
        //protected System.Windows.Forms.Label lblDept;
        //protected System.Windows.Forms.Label lblAreaTitle;
        //protected com.digitalwave.Utility.Controls.ctlComboBox m_cboDept;
        //protected com.digitalwave.Utility.Controls.ctlComboBox m_cboArea;
        //protected System.Windows.Forms.Button m_cmdPre;
        //protected System.Windows.Forms.Button m_cmdNext;
        //protected System.Windows.Forms.ListView m_lsvBedNO;
        private System.Windows.Forms.ColumnHeader clmBedNO_BaseForm;
        //protected System.Windows.Forms.Label lblBedNoTitle;
        //protected com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtBedNO;

        //protected bool m_blnDirectPrint;

        /// <summary>
        /// Ge������IP��ַ
        /// </summary>
        protected string m_strGEIP = "";
        private PinkieControls.ButtonXP m_cmdSave;
        private PinkieControls.ButtonXP m_cmdDelete;
        private PinkieControls.ButtonXP m_cmdNew;
        private PinkieControls.ButtonXP m_cmdClear;
        private PinkieControls.ButtonXP m_cmdPrintPreview;
        private PinkieControls.ButtonXP m_cmdPrint;

        /// <summary>
        /// ����������
        /// </summary>
        //protected clsICUGESimulateGetData m_objICUGESimulateGetData;
        ///// <summary>
        ///// �Ƿ�ֱ�Ӵ�ӡ
        ///// </summary>
        //public bool m_BlnDirectPrint
        //{
        //    set
        //    {
        //        m_blnDirectPrint = value;
        //    }
        //}

        #endregion

        public frmValuationBaseForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //objPIArr = clsLoginContext.s_ObjLoginContext.m_ObjPIArr;
            //m_objICUGESimulateGetData = new clsICUGESimulateGetData(this);
        }



        /// <summary>
        /// ���ò�����Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient">����</param>
        protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
        {
            clsBaseInfo.s_ObjCurrentPatient = p_objSelectedPatient;
            m_mthSetPatient(p_objSelectedPatient);
        }

        /// <summary>
        /// ���ò��˻�����Ϣ
        /// </summary>
        /// <param name="p_objPatient"></param>
        protected void m_mthSetPatientBaseInfo(clsPatient p_objPatient)
        {
            ClearUp();
            m_objCurrentPatient = p_objPatient;

            if (p_objPatient != null)
            {
                m_strInPatientDate = p_objPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss");
                m_strInPatientID = p_objPatient.m_StrInPatientID;



                //m_objICUGESimulateGetData.m_mthChangeApparatus(p_objPatient.m_StrInPatientID, p_objPatient.m_DtmLastInDate.ToString(), p_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID);
            }

            m_mthLoadAllRecordTimeOfAPatient(m_strInPatientID, m_strInPatientDate, m_dtmBeginTime.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private DateTime m_dtmBeginTime = new DateTime(1900, 1, 1, 0, 0, 0);

        /// <summary>
        /// ������м�¼��������ʵ��
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_strPatientDate"></param>
        /// <param name="p_strFromDate"></param>
        /// <param name="p_strToDate"></param>
        protected virtual void m_mthLoadAllRecordTimeOfAPatient(string p_strPatientID, string p_strPatientDate, string p_strFromDate, string p_strToDate)
        {
        }

        public void m_mthSetAllRecToTimeView()
        {
            m_mthLoadAllRecordTimeOfAPatient(m_strInPatientID, m_strInPatientDate, m_dtmBeginTime.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// ��ʼ��������Ϣ�ʹ���״̬
        /// </summary>
        public void m_mthSetPatient(clsPatient p_objPatient)
        {
            m_mthSetPatientBaseInfo(p_objPatient);
            m_blnCurrApparatus();
            //MDIParent.m_mthChangeFormText(this,MDIParent.enmFormEditStatus.AddNew);
            ////��¼���ô��嵱ǰ״̬
            //MDIParent.s_ObjSaveCue.m_mthAddFormStatus(this);
        }

        protected void trvActivityTime_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {

                ClearUp();
                if (this.trvActivityTime.SelectedNode.Tag == null || this.trvActivityTime.SelectedNode.Tag.ToString() == "0")
                {
                }
                else
                {
                    m_strCreateDate = ((DateTime)trvActivityTime.SelectedNode.Tag).ToString("yyyy-MM-dd HH:mm:ss");

                    m_mthDisplay();
                }

            }
            catch
            { }
        }

        /// <summary>
        /// ��ս��棬������ʵ��
        /// </summary>
        protected virtual void ClearUp()
        {
        }

        /// <summary>
        /// ��ʾ��¼���ݣ�������ʵ��
        /// </summary>
        protected virtual void m_mthDisplay()
        {
        }
        /// <summary>
        /// ��ȡICU����
        /// </summary>
        protected void m_mthGetICUDataByTime(DateTime p_dtmStart, out clsCMSData p_objCMSData, out clsVentilatorData p_objVentilatorData)
        {
            //����ID�������λ����Ȼ�ᳬ��long�����Χ
            //string strLongBedID = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID.Substring(4)+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastRoomInfo.m_ObjRoom.m_StrRoomID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            string strLongBedID = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID + m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            strLongBedID = strLongBedID.PadRight(17, '0');
            //new clsICUDataUtil(m_objCurrentPatient.m_StrInPatientID,strLongBedID).m_mthGetICUDataByTime("",p_dtmStart,out p_objCMSData,out p_objVentilatorData);
            //new clsICUDataUtil(m_objCurrentPatient.m_StrInPatientID,strLongBedID).m_mthGetICUNumericParmatByTime(p_dtmStart,m_objCurrentPatient.m_DtmLastInDate,out p_objCMSData,out p_objVentilatorData);
            p_objCMSData = null;
            p_objVentilatorData = null;
        }

        protected void m_mthGetICUDataByTime(string p_strStartTime, out clsCMSData p_objCMSData, out clsVentilatorData p_objVentilatorData, string[] p_strTypeArry)
        {
            //����ID�������λ����Ȼ�ᳬ��long�����Χ
            //string strLongBedID = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID.Substring(4)+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastRoomInfo.m_ObjRoom.m_StrRoomID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            string strLongBedID = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID + m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            strLongBedID = strLongBedID.PadRight(17, '0');
            //new clsICUDataUtil(m_objCurrentPatient.m_StrInPatientID, strLongBedID).m_mthGetICUNumericParmatByTime(p_strStartTime, m_objCurrentPatient.m_DtmLastInDate.ToString(), out p_objCMSData, out p_objVentilatorData, p_strTypeArry);
            p_objCMSData = null;
            p_objVentilatorData = null;
        }

        #region �жϵ�ǰ�û��Ƿ�����GE����

        private bool m_blnIsConnected = false;

        protected bool m_blnCurrApparatus()
        {
            string strGENo = "";

            bool blnIsExist = false;
            System.Data.DataTable dtRecord = null;

            if (m_objCurrentPatient == null)
                return false;
            //new iCare.ICU.Espial.clsBedGEMaintenanceDomain().m_mthGetBedGEinf(m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID, ref strGENo, ref blnIsExist);

            if (blnIsExist)
            {
                //new iCare.ICU.Espial.clsBedGEMaintenanceDomain().m_mthGetGEInf(strGENo, ref dtRecord);
                if (dtRecord != null && dtRecord.Rows.Count > 0)
                {
                    m_strGEIP = dtRecord.Rows[0]["GE_IP"].ToString();
                }

                if (!string.IsNullOrEmpty(m_strGEIP))
                {
                    m_mthOpenMonitor();
                    m_blnIsConnected = true;
                    //System.Threading.Thread.Sleep(2000);
                }
            }

            return blnIsExist;
        }
        #endregion �жϵ�ǰ�û��Ƿ�����GE����

        #region ��ȡICU GE����
        protected void m_mthGetICUGEDataByTime(string p_strStart, out clsGECMSData p_objGECMSData)
        {
            p_objGECMSData = null;
            string strLongBedID = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID + m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID.Substring(4) + m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastRoomInfo.m_ObjRoom.m_StrRoomID + m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
            //new clsICUDataUtil(m_objCurrentPatient.m_StrInPatientID, strLongBedID).m_mthGetICUGEDataByTime(m_objCurrentPatient.m_StrInPatientID, p_strStart, m_objCurrentPatient.m_DtmLastInDate.ToString(), out p_objGECMSData);
            //new clsICUDataUtil(m_objCurrentPatient.m_StrInPatientID, strLongBedID).m_mthGetICUGEDataByTime(m_strGEIP, p_strStart, out p_objGECMSData);
        }
        #endregion ��ȡICU GE����

        #region HB Remark
        //		protected void m_mthGetICUDataByTime(DateTime p_dtmStart,out clsMP60Data p_objData)
        //		{
        //			//����ID�������λ����Ȼ�ᳬ��long�����Χ
        //			//			string strLongBedID = m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaID.Substring(4)+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastRoomInfo.m_ObjRoom.m_StrRoomID+m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedID;
        //			//			new clsICUDataUtil(m_objCurrentPatient.m_StrInPatientID,strLongBedID).m_mthGetICUDataByTime("",p_dtmStart,out p_objData);
        //
        //			p_objData = null;
        //
        //			System.Data.DataTable dtResult = new System.Data.DataTable();
        //			long lngRes = new com.digitalwave.InfoCenter_HL7.MidTier.clsInfoCenterServ().m_lngGetICUDataByTime(m_objCurrentPatient.m_StrInPatientID,p_dtmStart,out dtResult);
        //
        //			if(lngRes <= 0 || dtResult == null)
        //				return;
        //
        //			p_objData = new clsMP60Data();
        //			p_objData.m_strHR = m_strGetParamValue(dtResult,"HR");
        //			p_objData.m_strNBPd = m_strGetParamValue(dtResult,"NBPd");
        //			p_objData.m_strNBPm = m_strGetParamValue(dtResult,"NBPm");
        //			p_objData.m_strNBPs = m_strGetParamValue(dtResult,"NBPs");
        //			p_objData.m_strPulse = m_strGetParamValue(dtResult,"Pulse");
        //			p_objData.m_strPVC = m_strGetParamValue(dtResult,"PVC");
        //			p_objData.m_strResp = m_strGetParamValue(dtResult,"Resp");
        //			p_objData.m_strSpO2 = m_strGetParamValue(dtResult,"SpO2");
        //			p_objData.m_strST_aVF = m_strGetParamValue(dtResult,"ST-aVF");
        //			p_objData.m_strST_aVL = m_strGetParamValue(dtResult,"ST-aVL");
        //			p_objData.m_strST_aVR = m_strGetParamValue(dtResult,"ST-aVR");
        //			p_objData.m_strST_I = m_strGetParamValue(dtResult,"ST-I");
        //			p_objData.m_strST_II = m_strGetParamValue(dtResult,"ST-II");
        //			p_objData.m_strST_III = m_strGetParamValue(dtResult,"ST-III");
        //		}
        #endregion

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <param name="p_strParamName"></param>
        /// <returns></returns>
        private string m_strGetParamValue(System.Data.DataTable p_dtResult, string p_strParamName)
        {
            for (int i = 0; i < p_dtResult.Rows.Count; i++)
            {
                if (p_dtResult.Rows[i]["ParamName"].ToString().Trim() == p_strParamName)
                    return p_dtResult.Rows[i]["ParamValue"].ToString().Trim();
            }
            return "";
        }

        /// <summary>
        /// ����(A-a)DO2
        /// </summary>
        /// <param name="p_dblFiO2"></param>
        /// <param name="p_dblPaCO2"></param>
        /// <param name="p_dblPaO2"></param>
        /// <returns></returns>
        protected double m_dblCalAaDO2(double p_dblFiO2, double p_dblPaCO2, double p_dblPaO2)
        {
            /*��ʽ:A-aDO2=PAO2-PaO2��PAO2=FiO2(PB-47)*-PaCO2*1/R��
			 * ʽ��PB=����ѹ760mmHg��R=�����̣���0.8���㣬��47��=ˮ����ѹ��37��ʱ�� 
			 */
            return p_dblFiO2 * (760 - 47) - p_dblPaCO2 / 0.8 - p_dblPaO2;
        }

        /// <summary>
        /// �����Ƿ���Ч
        /// </summary>
        /// <param name="p_txtArr"></param>
        /// <returns></returns>
        protected bool m_blnInputValid(TextBox[] p_txtArr)
        {
            for (int i = 0; i < p_txtArr.Length; i++)
            {
                try { double.Parse(p_txtArr[i].Text); }
                catch
                {
                    MessageBox.Show(c_strPromptForNum);
                    p_txtArr[i].Focus();
                    return false;
                }
            }
            return true;
        }

        #region PrintFunction

        /// <summary>
        /// ��ӡǰ��ʾ����
        /// </summary>
        protected virtual DialogResult m_dlgHandleSaveBeforePrint()
        {
            DialogResult dlgResult = DialogResult.None;
            dlgResult = clsPublicFunction.ShowQuestionMessageBox("[" + this.Text + "]���˸Ķ����Ƿ񱣴棿", MessageBoxButtons.YesNoCancel);

            if (dlgResult == DialogResult.Yes)
            {
                m_lngSave();
            }
            return dlgResult;
        }

        /// <summary>
        /// �Ӵ�������ʵ��
        /// </summary>
        /// <returns></returns>
        public virtual long m_lngSubSave()
        {
            return 1;
        }

        public long m_lngSave()
        {
            this.Cursor = Cursors.WaitCursor;
            long lngRes = m_lngSubSave();
            this.Cursor = Cursors.Default;

            return lngRes;
        }

        public long m_lngPrint()
        {
            this.Cursor = Cursors.WaitCursor;
            //			long lngRes = 0;
            if (m_dlgHandleSaveBeforePrint() != DialogResult.Cancel)
            {
                m_mthPrint();
            }
            this.Cursor = Cursors.Default;

            return 1;
        }

        #region �ⲿ��ӡ	

        // ��ʼ��ӡ��
        private PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();

        private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            objPrintTool.m_mthPrintPage(e);

            if (ppdPrintPreview != null)
                while (!ppdPrintPreview.m_blnHandlePrint(e))
                    objPrintTool.m_mthPrintPage(e);
        }

        private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthBeginPrint(e);
        }

        private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthEndPrint(e);
        }

        protected clsValuationPrintBase objPrintTool;
        public void m_mthPrint()
        {
            m_mthSetPrint();

            m_mthStartPrint();
        }

        /// <summary>
        /// �Ӵ�������ʵ��
        /// </summary>
        public virtual void m_mthSetPrint()
        { }

        private bool m_blnDirectPrint = false;

        private void m_mthStartPrint()
        {
            if (m_blnDirectPrint)
            {
                m_pdcPrintDocument.Print();
            }
            else
            {
                ppdPrintPreview.Document = m_pdcPrintDocument;
                ppdPrintPreview.ShowDialog();
            }
        }
        #endregion �ⲿ��ӡ

        private void frmValuationBaseForm_Load(object sender, System.EventArgs e)
        {
            if (Site != null && this.Site.DesignMode)
            {
                return;
            }
            if (!clsBaseInfo.s_BlnIsInit)
            {
                clsBaseInfo.s_mthInit(clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR);
            }
            if (clsBaseInfo.LoginEmployee.m_strEMPNO_CHR != "0001")
            {
                m_cmdParamSetting.Visible = false;
            }
            TreeNode trnNode = new TreeNode("��������");
            trnNode.Tag = "0";
            this.trvActivityTime.Nodes.Add(trnNode);
            if (clsBaseInfo.s_ObjCurrentPatient != null)
            {
                try
                {
                    this.m_ctlAreaPatientSelection.m_mthSetPatient(clsBaseInfo.s_ObjCurrentPatient.m_strAreaNewID, clsBaseInfo.s_ObjCurrentPatient.m_StrPatientID, clsBaseInfo.s_ObjCurrentPatient.m_StrRegisterId);
                }
                catch
                {
                }
            }
        }

        #endregion

        /// <summary>
        /// �Ƿ�����Ҹı��¼�
        /// </summary>
        protected bool m_blnCanDeptSelectIndexChangeEventTakePlace = true;
        /// <summary>
        /// �Ƿ������ı��¼�
        /// </summary>
        protected bool m_blnCanAreaSelectIndexChangeEventTakePlace = true;


        protected override void m_mthClearAllInfo(Control p_ctlControl)
        {
            base.m_mthClearAllInfo(p_ctlControl);
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName == "ctlRichTextBox")
            {
                //haozhong.liu 2008-6-6
                //if(p_ctlControl is iCare.CustomForm.ctlRichTextBox)//�Զ�����е�cltRichTextBox
                //    ((iCare.CustomForm.ctlRichTextBox)p_ctlControl).Text = "";
                //else
                //    ((ctlRichTextBox)p_ctlControl).m_mthClearText();	
            }
            else if (strTypeName == "ctlBorderTextBox" && p_ctlControl.Name != "txtInPatientID" && p_ctlControl.Name != "m_txtPatientName" && p_ctlControl.Name != "m_txtBedNO")
                ((ctlBorderTextBox)p_ctlControl).Text = "";
            else if (strTypeName == "TreeView")
            {
                if (((TreeView)p_ctlControl).Nodes.Count > 0)
                    ((TreeView)p_ctlControl).Nodes[0].Nodes.Clear();
            }
            else if (strTypeName == "ListView")
                ((ListView)p_ctlControl).Items.Clear();
            else if (strTypeName == "DateTimePicker")
                ((DateTimePicker)p_ctlControl).Value = DateTime.Now;
            if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker" && strTypeName != "TabPage")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthClearAllInfo(subcontrol);
                }
            }
        }
        protected clsDepartmentManager m_objDepartmentManager = new clsDepartmentManager();

        private void m_cmdNext_Click(object sender, System.EventArgs e)
        {
            m_mthGetBedNOArr();

        }
        /// <summary>
        /// �õ���ǰҪ���ҵĲ��˴���
        /// </summary>
        private void m_mthGetBedNOArr()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsPatient[] objPatientArr = m_objGetPatientByBedNO();

                if (objPatientArr == null || objPatientArr.Length == 0)
                {
                    this.Cursor = Cursors.Default;
                    m_mthClearAllInfo(this);
                    m_mthClearAllInfo(this);//��ճ���ǰ�ؼ���������д�������.
                                            //					m_mthShowNoPatient();
                    return;
                }

                //��������ID����Name,��ʾΪ����Name����				
                for (int i = 0; i < objPatientArr.Length; i++)
                {
                    ListViewItem lviPatient = new ListViewItem(
                        new string[]{
                                        objPatientArr[i].m_strBedCode
                                    });
                    lviPatient.Tag = objPatientArr[i];
                }
            }
            catch (Exception exp)
            {
                string strError = exp.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected virtual clsPatient[] m_objGetPatientByBedNO()
        {
            if (m_ctlAreaPatientSelection.CurrentArea == null)
            {
                MessageBox.Show("����ѡ����!");
                return null;
            }
            //			
            //			if(m_blnGetAllBedNo)            
            return m_objCurrentContext.m_ObjPatientManager.m_objGetPatientByLikeBedNO_InArea(m_ctlAreaPatientSelection.CurrentArea.m_strDEPTID_CHR, "");
            //			else
            //				return m_objCurrentContext.m_ObjPatientManager.m_objGetPatientByLikeBedNO_InArea(((clsInPatientArea)(this.m_cboArea.SelectedItem)).m_StrAreaID,m_txtBedNO.Text);
        }

        private void frmValuationBaseForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //m_objICUGESimulateGetData.m_mthStopReceiveData();
            m_mthCloseMonitor();
        }

        private void m_cmdSetParman_Click(object sender, System.EventArgs e)
        {
            frmMedicalParameterCollate frm = new frmMedicalParameterCollate(this);
            frm.Show();
        }

        protected clsSystemContext m_objCurrentContext
        {
            get
            {
                return clsSystemContext.s_ObjCurrentContext;
            }
        }

        /// <summary>
        /// ��ʽ����ʾ������Ϣ
        /// </summary>
        /// <param name="p_strParamData"></param>
        /// <returns></returns>
        protected string m_strFormatShowParamData(string p_strParamData)
        {
            if (p_strParamData == null || p_strParamData.Length <= 0)
                return "";
            else
                return Math.Floor(double.Parse(p_strParamData)).ToString();
            //return p_strParamData.Substring(0,p_strParamData.IndexOf("."));
        }

        #region ��ȡ������Ϣ
        protected Hashtable m_hasControlByChedkedResult = new Hashtable();
        protected void m_mthGetCheckInfo()
        {
            if (m_objCurrentPatient == null)
                return;

            string[] strItemArry = null;

            //ArrayList altContrlArry = new ArrayList();
            Control[] CtlArry = null;

            string[] strCheckInfoArry = null;

            m_mthGetCheckItem(ref strItemArry, ref CtlArry);
            m_hasControlByChedkedResult = new Hashtable();
            frmMedicalParameterCollate.m_mthGetCheckInfo(m_strInPatientID, m_strInPatientDate, strItemArry, ref strCheckInfoArry, CtlArry);
            m_hasControlByChedkedResult = frmMedicalParameterCollate.m_hasControlByChedkedResult;
        }

        protected virtual void m_mthGetCheckItem(ref string[] p_strCheckItemID, ref Control[] p_ctlTemp)
        {

            //clsICU_QuerySvc objSvc =
            //    (clsICU_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsICU_QuerySvc));

            List<clsICUFormCheckItem> lstCheckItem = null;
            //objSvc.m_lngGetFormCheckItem(this.Name, out lstCheckItem);
            if (lstCheckItem != null)
            {
                //strGroupArry = new string[dtRecord.Rows.Count];
                p_strCheckItemID = new string[lstCheckItem.Count];
                p_ctlTemp = new Control[lstCheckItem.Count];
                int i = 0;
                foreach (clsICUFormCheckItem item in lstCheckItem)
                {
                    //strGroupArry[i] = dtRecord.Rows[i]["SORTID"].ToString();
                    p_strCheckItemID[i] = item.m_strCheckItemID;
                    bool blnReturn = false;
                    p_ctlTemp[i] = m_ctlGetControlByCheck(item.m_strControlName, this, ref blnReturn);
                    i++;
                }
            }
        }

        private Control m_ctlGetControlByCheck(string p_strControlName, Control p_clt, ref bool p_blnReturn)
        {
            Control ctl = null;
            if (p_blnReturn)
                return ctl;
            if (p_clt.Name.Trim().ToUpper() == p_strControlName.Trim().ToUpper())
            {
                p_blnReturn = true;
                ctl = p_clt;
                return ctl;
            }

            for (int i = 0; i < p_clt.Controls.Count; i++)
            {
                ctl = m_ctlGetControlByCheck(p_strControlName, p_clt.Controls[i], ref p_blnReturn);
                if (p_blnReturn)
                    return ctl;
            }
            return ctl;
        }
        #endregion ��ȡ������Ϣ

        private void frmValuationBaseForm_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
        {
            string strHelpPath = Application.StartupPath + "\\Help\\ICUϵͳ.CHM";
            Help.ShowHelp(this, strHelpPath);
        }

        #region ����ʵʱGE������ȡ����
        //private static ctlMonitor m_ctlMonitor;

        private void m_mthIniMonitor()
        {
            //m_ctlMonitor = new ctlMonitor(false);
            //m_ctlMonitor.m_BlnIsShowWave = false;
            //m_ctlMonitor.e_GetMonitorParamData += new OnReceiveDataEventHandler(m_ctlMonitor_e_GetMonitorParamData);
            //m_ctlMonitor.m_StrMonitorIp = m_strGEIP;
            //m_ctlMonitor.m_BlnFlag = true;
        }

        void m_ctlMonitor_e_GetMonitorParamData(clsReceiverParam p_objParam)
        {
            m_objMonitorParamValue = p_objParam;
        }

        private clsReceiverParam m_objMonitorParamValue;
        protected clsGECMSData m_objGECMSData;

        /// <summary>
        /// ��ȡʵʱ���β���
        /// </summary>
        protected void m_mthGetMonitorParamGE()
        {
            //m_objMonitorParamValue = m_ctlMonitor.m_ObjMonitorParamValue;
            if (m_objMonitorParamValue == null)
            {
                m_objGECMSData = null;
            }
            else
            {
                m_objGECMSData = new clsGECMSData();
                m_objGECMSData.m_Breath = m_objMonitorParamValue.m_fltRR.ToString();
                m_objGECMSData.m_strARTDiastolic = m_objMonitorParamValue.m_fltARTDiastolic.ToString();
                m_objGECMSData.m_strARTMean = m_objMonitorParamValue.m_fltARTMean.ToString();
                m_objGECMSData.m_strARTSystolic = m_objMonitorParamValue.m_fltARTSystolic.ToString();
                m_objGECMSData.m_strHR = m_objMonitorParamValue.m_fltHR.ToString();
                m_objGECMSData.m_strNBPDiastolic = m_objMonitorParamValue.m_fltNBPDiastolic.ToString();
                m_objGECMSData.m_strNBPMean = m_objMonitorParamValue.m_fltNBPMean.ToString();
                m_objGECMSData.m_strNBPSystolic = m_objMonitorParamValue.m_fltNBPSystolic.ToString();
                m_objGECMSData.m_strPluse = m_objMonitorParamValue.m_fltPluse.ToString();
                m_objGECMSData.m_strSpO2 = m_objMonitorParamValue.m_fltSpO2.ToString();
                m_objGECMSData.m_strTEMP1 = m_objMonitorParamValue.m_fltTEMP1.ToString();
                m_objGECMSData.m_strTEMP2 = m_objMonitorParamValue.m_fltTEMP2.ToString();

            }
        }

        /// <summary>
        /// �����໤����
        /// </summary>
        private void m_mthOpenMonitor()
        {
            m_mthIniMonitor();
            //m_ctlMonitor.m_mthStart();
        }

        /// <summary>
        /// �رռ໤����
        /// </summary>
        private void m_mthCloseMonitor()
        {
            //if (m_ctlMonitor != null)
            //{
            //    m_ctlMonitor.m_mthStop();
            //}
        }
        #endregion ����ʵʱGE������ȡ����

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            m_lngSave();
        }

        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }


        private void m_cmdNew_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in trvActivityTime.Nodes)
            {
                if (node.Parent == null)
                {
                    trvActivityTime.SelectedNode = node;
                    break;
                }
            }
        }

        private void m_cmdClear_Click(object sender, EventArgs e)
        {
            ClearUp();
        }

        public void m_mthPrintPreview()
        {
            m_blnDirectPrint = false;
            m_mthPrint();
        }

        private void m_cmdPrintPreview_Click(object sender, EventArgs e)
        {
            m_mthPrintPreview();
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            m_blnDirectPrint = true;
            m_mthPrint();
        }

        #region interface
        public virtual void Copy()
        {
        }

        public virtual void Cut()
        {

        }

        public virtual void Paste()
        {
        }

        public virtual void Redo()
        {

        }

        public virtual void Undo()
        {

        }

        public virtual void Verify()
        {
        }

        public virtual void Print()
        {
            m_mthPrintPreview();
        }


        public virtual void Delete()
        {
        }

        public virtual void Display()
        {

        }

        public virtual void Display(string cardno, string ActivityTime)
        {
        }

        public virtual void Save()
        {
        }
        #endregion
    }
}
