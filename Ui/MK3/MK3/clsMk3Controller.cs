using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System.IO.Ports;
using System.Data;
using MK3;
using System.IO;

namespace com.digitalwave.iCare.gui.LIS
{
    internal class clsMk3Controller
    {
        #region ����
        /// <summary>
        /// ������
        /// </summary>
        frmMK3Operation m_objViewer;
        /// <summary>
        /// ����
        /// </summary>
        //internal SerialPort SerialCom = new SerialPort();
        /// <summary>
        /// ������������
        /// </summary>
        string[] strDataArr = null;
        /// <summary>
        /// ʱ�����
        /// </summary>
        private System.Timers.Timer m_objTimers;
        /// <summary>
        /// ������������
        /// </summary>
        byte[] bytStart;
        /// <summary>
        /// ����״̬
        /// </summary>
        int intOk = 0;
        List<string> lstReceiveData;
        string m_strData_Holder = null;
        clsDataTreatment m_objController;
        clsCtl_MK3ItemSet m_objDomain;
        internal DataTable m_dtResult = null;

        // MS.Comm�ؼ�
        AxMSCommLib.AxMSComm axMSComm;

        frmMSComm frmMS = null;

        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_objViewer"></param>
        public clsMk3Controller(frmMK3Operation p_objViewer, clsDataTreatment p_objController)
        {
            m_objViewer = p_objViewer;
            m_objController = p_objController;
            m_objDomain = new clsCtl_MK3ItemSet(); 
        }
        #endregion

        #region InitComm
        /// <summary>
        /// InitComm
        /// </summary>
        internal void InitComm()
        {
            #region MSComm32

            frmMS = new frmMSComm();
            frmMS.Location = new System.Drawing.Point(-200, 0);
            frmMS.Show();

            axMSComm = frmMS.axMSComm;
            axMSComm.Name = "MK3";
            axMSComm.CommPort = short.Parse(m_objViewer.m_strPortName);
            axMSComm.Settings = "9600,n,8," + (int)StopBits.One;
            axMSComm.DTREnable = true;
            axMSComm.EOFEnable = false;
            axMSComm.Handshaking = MSCommLib.HandshakeConstants.comNone;
            axMSComm.InBufferSize = 1024;
            axMSComm.InputLen = 20000;
            axMSComm.InputMode = MSCommLib.InputModeConstants.comInputModeText;
            axMSComm.OutBufferSize = 1024;
            axMSComm.RThreshold = 1;
            axMSComm.SThreshold = 0;
            #endregion
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMSComm_OnComm(object sender, EventArgs e)
        {
            try
            {
                bool blnSure = true;
                string inPutData = axMSComm.Input.ToString();
                if (bytStart == null)
                {
                    return;
                }
                if (bytStart[0] != 80)
                {
                    if (!string.IsNullOrEmpty(inPutData))
                    {
                        strDataArr = null;
                        strDataArr = inPutData.Split(new char[] { '\r', '\n', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (strDataArr[0] == "OK" || strDataArr[0] == "ER3" || strDataArr[0] == "ER1")
                        {
                            switch (bytStart[0])
                            {
                                case clsDPEMessage.m_bytAirBlank:
                                    m_mthReplyJudge(strDataArr[0], "�����հ����ò��ԣ��Ƿ����", out blnSure);
                                    if (!blnSure)
                                        return;
                                    m_mthX2Order();
                                    break;
                                case clsDPEMessage.m_bytShock:
                                    m_mthReplyJudge(strDataArr[0], "��ģʽ���ò��ԣ��Ƿ����", out blnSure);
                                    if (!blnSure)
                                        return;
                                    m_mthZ5Order();
                                    break;
                                case clsDPEMessage.m_bytShockTime:
                                    m_mthReplyJudge(strDataArr[0], "���ٶ����ò��ԣ��Ƿ����", out blnSure);
                                    if (!blnSure)
                                        return;
                                    m_mthE0Order();
                                    break;
                                case clsDPEMessage.m_bytContinueWay:
                                    m_mthReplyJudge(strDataArr[0], "���淽ʽ���ò��ԣ��Ƿ����", out blnSure);
                                    if (!blnSure)
                                        return;
                                    m_mthF2Order();
                                    break;
                                case clsDPEMessage.m_bytSelectFilter:
                                    m_mthReplyJudge(strDataArr[0], "�˹�Ƭ���ò��ԣ��Ƿ����", out blnSure);
                                    if (!blnSure)
                                        return;
                                    m_mthPOrder();
                                    break;
                                case clsDPEMessage.m_bytStart:
                                    //m_objViewer.m_txtState.Text = "�ɹ��������������";
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    m_strData_Holder += inPutData;
                    if (m_strData_Holder != null)
                    {
                        if (m_strData_Holder.Length == 592)
                        {
                            m_mthDataTreatment();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
            }
        }
        #endregion

        #region ��ȡ�뱾�������ص��������趨��Ϣ
        /// <summary>
        /// ��ȡ�뱾�������ص��������趨��Ϣ
        /// </summary>
        public void GetInstrumentSerialSetting()
        {
            //SerialCom.PortName = m_objViewer.m_strPortName;
            //SerialCom.BaudRate = 9600;
            //SerialCom.DataBits = 8;
            //SerialCom.StopBits = StopBits.One;
            //SerialCom.Parity = Parity.None;
            //SerialCom.DtrEnable = true; 
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public virtual long m_lngStartWork()
        {
            lstReceiveData = new List<string>();
            bytStart = null;
            //if (SerialCom.IsOpen)
            if (axMSComm.PortOpen)
            {
                MessageBox.Show(m_objViewer, "ָ���Ĵ����Ѿ���", "ø���ǲ�����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            else
            {
                try
                {
                    //SerialCom.Open();
                    //SerialCom.DataReceived += new SerialDataReceivedEventHandler(SerialCom_DataReceived);

                    axMSComm.PortOpen = true;
                    axMSComm.OnComm -= new System.EventHandler(this.axMSComm_OnComm);
                    axMSComm.OnComm += new System.EventHandler(this.axMSComm_OnComm);
                }
                catch (Exception objEx)
                {
                    MessageBox.Show(objEx.Message, "ø���ǲ�����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //SerialCom.Dispose();
                    return 0;
                }
                return 1;
            }
        }

        void SerialCom_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //bool blnSure = true;
            //string inPutData = SerialCom.ReadExisting();
            //if (bytStart == null)
            //{
            //    return;
            //}
            //if (bytStart[0] != 80)
            //{
            //    if (!string.IsNullOrEmpty(inPutData))
            //    {
            //        strDataArr = null;
            //        strDataArr = inPutData.Split(new char[] { '\r', '\n', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //        if (strDataArr[0] == "OK" || strDataArr[0] == "ER3" || strDataArr[0] == "ER1")
            //        {
            //            switch (bytStart[0])
            //            {
            //                case clsDPEMessage.m_bytAirBlank:
            //                    m_mthReplyJudge(strDataArr[0], "�����հ����ò��ԣ��Ƿ����", out blnSure);
            //                    if (!blnSure)
            //                        return;
            //                    m_mthX2Order();
            //                    break;
            //                case clsDPEMessage.m_bytShock:
            //                    m_mthReplyJudge(strDataArr[0], "��ģʽ���ò��ԣ��Ƿ����", out blnSure);
            //                    if (!blnSure)
            //                        return;
            //                    m_mthZ5Order();
            //                    break;
            //                case clsDPEMessage.m_bytShockTime:
            //                    m_mthReplyJudge(strDataArr[0], "���ٶ����ò��ԣ��Ƿ����", out blnSure);
            //                    if (!blnSure)
            //                        return;
            //                    m_mthE0Order();
            //                    break;
            //                case clsDPEMessage.m_bytContinueWay:
            //                    m_mthReplyJudge(strDataArr[0], "���淽ʽ���ò��ԣ��Ƿ����", out blnSure);
            //                    if (!blnSure)
            //                        return;
            //                    m_mthF2Order();
            //                    break;
            //                case clsDPEMessage.m_bytSelectFilter:
            //                    m_mthReplyJudge(strDataArr[0], "�˹�Ƭ���ò��ԣ��Ƿ����", out blnSure);
            //                    if (!blnSure)
            //                        return;
            //                    m_mthPOrder();
            //                    break;
            //                case clsDPEMessage.m_bytStart:
            //                    //m_objViewer.m_txtState.Text = "�ɹ��������������";
            //                    break;
            //            }
            //        }
            //    }
            //}
            //else
            //{

            //    m_strData_Holder += inPutData;
            //    if (m_strData_Holder != null)
            //    {
            //        if (m_strData_Holder.Length == 592)
            //        {
            //            m_mthDataTreatment();
            //        }
            //    }
            //}
        }

        #endregion

        #region ֹͣ����
        /// <summary>
        /// ֹͣ����
        /// </summary>
        /// <returns></returns>
        public virtual long m_lngFinishWork()
        {
            intOk = 0;
            //if (SerialCom != null)
            //{
            //    SerialCom.Close();
            //    SerialCom.Dispose();
            //    return 1;
            //}
            if (axMSComm.PortOpen)
            {
                axMSComm.PortOpen = false;
                return 1;
            }
            return 0;
        }
        #endregion

        #region �������������
        /// <summary>
        /// �������������
        /// </summary>
        /// <returns></returns>
        public virtual void m_lngStratComputer()
        {
            bytStart = null;
            bytStart = new byte[3];
            bytStart[0] = clsDPEMessage.m_bytStart;
            bytStart[1] = clsDPEMessage.m_bytCR;
            bytStart[2] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="buffer"></param>
        protected virtual void m_mthSend(byte[] buffer)
        {
            try
            {
                intOk = 0;
                //SerialCom.Write(buffer, 0, buffer.Length);
                axMSComm.Output = System.Text.Encoding.Default.GetString(buffer);
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
            }
        }
        #endregion

        #region �ȴ���������
        /// <summary>
        /// �ȴ���������
        /// </summary>
        public void m_mthWait()
        {
            while (intOk == 0)
            {
            }
            return;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public virtual void m_mthReadPlate()
        {
            bool blnSure = false;

            #region ����A����

            m_mthWait();
            //m_thInstrumentReply("�����հ����ò���", out blnSure);
            if (!blnSure)
            {
                return;
            }
            #endregion

            #region ����X2
            blnSure = false;
            bytStart = null;
            bytStart = new byte[4];
            bytStart[0] = 88;
            bytStart[1] = 50;
            bytStart[2] = clsDPEMessage.m_bytCR;
            bytStart[3] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
            m_mthWait();
            //m_thInstrumentReply("��ģʽ���ò���", out blnSure);
            if (!blnSure)
            {
                return;
            }
            #endregion

            #region ����Z05����
            bytStart = null;
            bytStart = new byte[5];
            bytStart[0] = 90;
            bytStart[1] = 32;
            bytStart[2] = 53;
            bytStart[3] = clsDPEMessage.m_bytCR;
            bytStart[4] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
            m_mthWait();
            //m_thInstrumentReply("���ٶ����ò���", out blnSure);
            //if (!blnSure)
            //{
            //    return;
            //}
            #endregion

            #region ����EO����
            bytStart = null;
            bytStart = new byte[4];
            bytStart[0] = clsDPEMessage.m_bytContinueWay;
            bytStart[1] = clsDPEMessage.m_bytContinueModel;
            bytStart[2] = clsDPEMessage.m_bytCR;
            bytStart[3] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
            m_mthWait();
            //m_thInstrumentReply("���淽ʽ���ò���", out blnSure);
            if (!blnSure)
            {
                return;
            }
            #endregion

            #region ����F2����
            bytStart = null;
            bytStart = new byte[4];
            bytStart[0] = clsDPEMessage.m_bytSelectFilter;
            bytStart[1] = clsDPEMessage.m_bytSelectFilterModel;
            bytStart[2] = clsDPEMessage.m_bytCR;
            bytStart[3] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
            m_mthWait();
            //m_thInstrumentReply("ѡ���˹�Ƭ��ʽ����", out blnSure);
            if (!blnSure)
            {
                return;
            }
            #endregion

            #region ����P����
            bytStart = null;
            bytStart = new byte[3];
            bytStart[0] = clsDPEMessage.m_bytMeasurement;
            bytStart[1] = clsDPEMessage.m_bytCR;
            bytStart[2] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
            m_mthWait();
            //m_thInstrumentReply("����ģʽ���ò���", out blnSure);
            if (!blnSure)
            {
                return;
            }
            #endregion
        }
        #endregion

        #region ����A����
        /// <summary>
        /// ����A����
        /// </summary>
        public virtual void m_mthAOrder()
        {
            lstReceiveData = new List<string>();
            bytStart = null;
            bytStart = new byte[3];
            bytStart[0] = clsDPEMessage.m_bytAirBlank;
            bytStart[1] = clsDPEMessage.m_bytCR;
            bytStart[2] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
        }
        #endregion

        #region ����X2����
        /// <summary>
        /// ����X2����
        /// </summary>
        public virtual void m_mthX2Order()
        {
            bytStart = null;
            bytStart = new byte[4];
            bytStart[0] = 88;
            bytStart[1] = 50;
            bytStart[2] = clsDPEMessage.m_bytCR;
            bytStart[3] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
        }
        #endregion

        #region ����Z 5����
        /// <summary>
        /// ����Z 5����
        /// </summary>
        public virtual void m_mthZ5Order()
        {
            bytStart = null;
            bytStart = new byte[5];
            bytStart[0] = 90;
            bytStart[1] = 32;
            bytStart[2] = 53;
            bytStart[3] = clsDPEMessage.m_bytCR;
            bytStart[4] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
        }
        #endregion

        #region ����EO����
        /// <summary>
        /// ����EO����
        /// </summary>
        public virtual void m_mthE0Order()
        {
            bytStart = null;
            bytStart = new byte[4];
            bytStart[0] = clsDPEMessage.m_bytContinueWay;
            bytStart[1] = clsDPEMessage.m_bytContinueModel;
            bytStart[2] = clsDPEMessage.m_bytCR;
            bytStart[3] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
        }
        #endregion

        #region ����F2����
        /// <summary>
        /// ����F2����
        /// </summary>
        public virtual void m_mthF2Order()
        {
            bytStart = null;
            bytStart = new byte[4];
            bytStart[0] = clsDPEMessage.m_bytSelectFilter;
            bytStart[1] = clsDPEMessage.m_bytSelectFilterModel;
            bytStart[2] = clsDPEMessage.m_bytCR;
            bytStart[3] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);
        }
        #endregion

        #region ����P����
        /// <summary>
        /// ����P����
        /// </summary>
        public virtual void m_mthPOrder()
        {
            bytStart = null;
            bytStart = new byte[3];
            bytStart[0] = clsDPEMessage.m_bytMeasurement;
            bytStart[1] = clsDPEMessage.m_bytCR;
            bytStart[2] = clsDPEMessage.m_bytLF;
            m_mthSend(bytStart);

        }
        #endregion

        #region ���ݷ���
        /// <summary>
        /// ���ݷ���
        /// </summary>
        public void m_mthDataTreatment()
        {
            if (string.IsNullOrEmpty(m_strData_Holder))
            {
                return;
            }
            int j = m_strData_Holder.Length;
            string[] strReviceDataArr = m_strData_Holder.Split(new char[] { '\r', '\n', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            m_strData_Holder = null;
            List<clsLIS_Device_Test_ResultVO> p_lstResult = new List<clsLIS_Device_Test_ResultVO>();
            //clsLIS_Svc objServ = (clsLIS_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsLIS_Svc));
            clsLIS_Device_Test_ResultVO[] objOutResultArr = null;
            string[] strSampleId = new string[96];
            string strCheckDate = null;
            string strDeviceId = null;
            string strCheckItemName = null;
            long lngRes = 0;
            strDeviceId = m_objViewer.m_strDeviceId;
            strCheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strCheckItemName = m_objViewer.m_strCheckItemName;
            double dblCO;
            double dblTemp = 0;
            switch (strCheckItemName.ToLower())
            {
                case "hbeab":
                case "hbcab":
                    double.TryParse(strReviceDataArr[1], out dblTemp);
                    dblCO = dblTemp * 0.5;
                    break;
                default:
                    double.TryParse(strReviceDataArr[1], out dblCO);
                    if (dblCO < 0.05)
                    {
                        dblCO = 0.05;
                    }
                    dblCO = 2.1 * dblCO;
                    break;

            }
            if (strReviceDataArr == null)
            {
                return;
            }
            clsLIS_Device_Test_ResultVO objTemp = null;
            for (int i = 0; i < m_objViewer.m_strSampleArr.Length; i++)
            {
                objTemp = new clsLIS_Device_Test_ResultVO();
                objTemp.strDevice_Sample_ID = m_objViewer.m_strSampleArr[i];
                objTemp.strDevice_Check_Item_Name = strCheckItemName;
                objTemp.strCheck_Date = strCheckDate;
                objTemp.strDevice_ID = strDeviceId;
                double.TryParse(strReviceDataArr[i], out dblTemp);
                switch (strCheckItemName.ToLower())
                {
                    case "hbeab":
                    case "hbcab":
                        if (dblTemp > dblCO)
                        {
                            objTemp.strResult = "����";
                        }
                        else
                        {
                            objTemp.strResult = "����";
                        }
                        break;
                    default:
                        if (dblTemp < dblCO)
                        {
                            objTemp.strResult = "����";
                        }
                        else
                        {
                            objTemp.strResult = "����";
                        }
                        break;
                }
                p_lstResult.Add(objTemp);
            }
            if (p_lstResult.Count > 0)
            {
                //lngRes = lngAddLabResult(p_lstResult.ToArray(), true, out objOutResultArr);
                lngRes = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(p_lstResult.ToArray(), true, out objOutResultArr);
                if (lngRes > 0 && objOutResultArr != null)
                {
                    m_mthDataShow(p_lstResult.ToArray());
                }
            }
        }
        #endregion

        #region ������ʾ
        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="objOutResultArr"></param>
        public void m_mthDataShow(clsLIS_Device_Test_ResultVO[] objOutResultArr)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("strDevice_Sample_ID", typeof(string));
            dtResult.Columns.Add("strResult", typeof(string));
            DataRow dtRow = null;
            for (int i = 0; i < objOutResultArr.Length; i++)
            {
                dtRow = dtResult.NewRow();
                dtRow["strDevice_Sample_ID"] = objOutResultArr[i].strDevice_Sample_ID;
                dtRow["strResult"] = objOutResultArr[i].strResult;
                dtResult.Rows.Add(dtRow);
            }
            //m_objViewer.m_dgvResult.Invoke(new MethodInvoker(delegate()
            //{
            //    m_objViewer.m_dgvResult.DataSource = dtResult;
            //}));
        }
        #endregion

        #region ���Ӽ����������, ������
        /// <summary>
        /// ���Ӽ����������, ������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_blnMuiltySample"> TRUE = ������</param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        public long lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            p_objOutResultArr = null;
            //clsLIS_Svc objServ = (clsLIS_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsLIS_Svc));
            long lngRes = 0;
            if (p_objResultArr == null || p_objResultArr.Length <= 0)
            {
                return lngRes;
            }

            if (p_blnMuiltySample)
            {
                List<string> lstSampleID = new List<string>();
                List<clsLIS_Device_Test_ResultVO> lstResult = new List<clsLIS_Device_Test_ResultVO>();
                List<clsLIS_Device_Test_ResultVO> lstOutResult = new List<clsLIS_Device_Test_ResultVO>();

                string strSampleID = "";
                string strSampleIDTemp = null;
                int idx = 0;
                for (idx = 0; idx < p_objResultArr.Length; idx++)
                {
                    strSampleID = p_objResultArr[idx].strDevice_Sample_ID;
                    if (strSampleID != strSampleIDTemp)
                    {
                        if (!lstSampleID.Contains(strSampleID))
                        {
                            lstSampleID.Add(strSampleID);
                        }
                        strSampleIDTemp = strSampleID;
                    }
                }

                clsLIS_Device_Test_ResultVO[] objResultTempArr = null;
                foreach (string str in lstSampleID)
                {
                    lstResult.Clear();
                    for (idx = 0; idx < p_objResultArr.Length; idx++)
                    {
                        if (str == p_objResultArr[idx].strDevice_Sample_ID)
                        {
                            lstResult.Add(p_objResultArr[idx]);
                        }
                    }
                    if (lstResult.Count > 0)
                    {
                        lngRes = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(lstResult.ToArray(), out objResultTempArr);
                        if (lngRes > 0 && objResultTempArr != null && objResultTempArr.Length > 0)
                        {
                            lstOutResult.AddRange(objResultTempArr);
                        }
                    }
                }
                p_objOutResultArr = lstOutResult.ToArray();
            }
            else
            {
                lngRes = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(p_objResultArr, out p_objOutResultArr);
            }

            return lngRes;
        }
        #endregion

        #region �����ظ���Ϣ�ж�
        /// <summary>
        /// �����ظ���Ϣ�ж�
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <param name="p_strMessage"></param>
        /// <param name="p_blnSure"></param>
        public virtual void m_mthReplyJudge(string p_strResult, string p_strMessage, out bool p_blnSure)
        {
            if (p_strResult == "OK")
            {
                p_blnSure = true;
            }
            else
            {
                p_blnSure = MessageBox.Show(m_objViewer, p_strMessage, "ø���ǲ�����ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK;

            }
        }
        #endregion

        #region ��ȡ��Ŀ��Ϣ
        /// <summary>
        /// ��ȡ��Ŀ��Ϣ
        /// </summary>
        public void m_mthGetAllCheckItemCustomInfo()
        {
            long lngRes = 0;
            clsLisCheckItemCustom[] objCheckItemCustomArr = null;

            lngRes = m_objDomain.m_lngGetAllCheckItemCustomInfo(out objCheckItemCustomArr, out m_dtResult);
            if (lngRes > 0)
            {
                m_objViewer.m_cboItem.DisplayMember = "check_item_name_vchr";
                m_objViewer.m_cboItem.ValueMember = "check_item_id_chr";
                m_objViewer.m_cboItem.DataSource = m_dtResult;
            }
        }
        #endregion
    }

    #region Log
    public class Log
    {
        public static void Output(string txt)
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string strFile = System.AppDomain.CurrentDomain.BaseDirectory + @"\log\" + strDate + ".txt";
            bool blnAllWaysNew = false;
            StreamWriter sw = null;
            try
            {
                FileInfo fi = new FileInfo(strFile);
                if (fi.Exists)
                {
                    if (fi.Length >= 2000000)
                    {
                        fi.CopyTo(System.AppDomain.CurrentDomain.BaseDirectory + @"\log\" + strDate + "-" + DateTime.Now.ToString("HHmm") + ".txt", true);
                        sw = fi.CreateText();
                    }
                    else
                    {
                        if (blnAllWaysNew)
                        {
                            sw = fi.CreateText();
                        }
                        else
                        {
                            sw = fi.AppendText();
                        }
                    }
                }
                else
                {
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    sw = fi.CreateText();
                }
                sw.WriteLine("-->>>>> " + strTime);
                sw.WriteLine(txt);
                sw.WriteLine();
            }
            catch (Exception e)
            {
                //throw e;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        public static void Output(string fileName, string txt)
        {
            StreamWriter sw = null;
            try
            {
                FileInfo fi = new FileInfo(fileName);
                if (fi.Exists)
                {
                    sw = fi.AppendText();
                }
                else
                {
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    sw = fi.CreateText();
                }
                sw.WriteLine(txt);
                sw.WriteLine();
            }
            catch (Exception e)
            {
                //throw e;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
    #endregion
}
