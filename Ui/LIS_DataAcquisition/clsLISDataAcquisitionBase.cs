using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using weCare.Core.Entity;
using System.Windows.Forms;
using com.digitalwave.iCare.middletier.LIS;
using com.digitalwave.Utility;
using System.IO.Ports;

namespace com.digitalwave.iCare.LIS
{
    /// <summary>
    /// �������ݲɼ������࣬Ĭ��ʹ�ô��ڣ���������ӿ�(���ڵ�)��Ҫ��дvirtual����
    /// </summary>
    public class clsLISDataAcquisitionBase : infLISDataAcquisition
    {
        #region ��Ա����
        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        clsLIS_Equip_ConfigVO _objDeviceConfigVO;
        /// <summary>
        /// ��������
        /// </summary>
        Form _frmParent;
        /// <summary>
        /// ����ͨѸ������
        /// </summary>
        clsSerialPortIO m_objSerialPort;

        string m_strMsgTitle = "���ݲɼ�";
        /// <summary>
        /// �Ƿ��¼��־��true = ��
        /// </summary>
        bool _blnLogger = true;
        /// <summary>
        /// ��־��¼��
        /// </summary>
        clsLogText m_objLogger;
        /// <summary>
        /// ��ʾ�������
        /// </summary>
        public event DataShowEventHandler evnDataShow;
        /// <summary>
        /// ��ʾ�ӿ�״̬��Ϣ
        /// </summary>
        public event DataAcquisitionInfoEventHandler evnAcquisitionInfo;
        /// <summary>
        /// ����ͨ������������Ŀת��
        /// </summary>
        private Hashtable m_hasItemConvert = null;
        /// <summary>
        /// �������ת��
        /// </summary>
        private Hashtable m_hasConvertValue = null;
        /// <summary>
        /// ʱ�����
        /// </summary>
        private System.Timers.Timer m_objTimer;
        /// <summary>
        /// �ɼ����ݸ�ʽ
        /// </summary>
        private enuReceiveDateType _enuDateType = enuReceiveDateType.ASCII;

        clsDcl_DataAcquisition m_objDomain;
        /// <summary>
        /// ÿ�α���ʱ�Ƿ������һ�𱣴�
        /// </summary>
        bool m_blnMuiltySample = false;

        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsLISDataAcquisitionBase()
        {
            m_objLogger = new clsLogText();
            m_objDomain = new clsDcl_DataAcquisition();
        }
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ��¼��־��true = ��
        /// </summary>
        public bool m_blnLogger
        {
            get { return _blnLogger; }
            set
            {
                _blnLogger = value;
                if (_blnLogger && m_objLogger == null)
                {
                    m_objLogger = new clsLogText();
                }
            }
        }
        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        public clsLIS_Equip_ConfigVO m_objDeviceConfigVO
        {
            get
            {
                return _objDeviceConfigVO;
            }
            set
            {
                _objDeviceConfigVO = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public Form m_frmParent
        {
            get
            {
                return _frmParent;
            }
            set
            {
                _frmParent = value;
            }
        }
        /// <summary>
        /// �ɼ����ݸ�ʽ
        /// </summary>
        public enuReceiveDateType m_enuDateType
        {
            get { return _enuDateType; }

            set { _enuDateType = value; }
        }

        #endregion

        public void m_mthSetMuiltySample(bool p_blnMuiltySample)
        {
            m_blnMuiltySample = p_blnMuiltySample;
        }

        #region infLISDataAcquisition ��Ա
        /// <summary>
        /// ��ʼ���ӿ�
        /// </summary>
        /// <returns></returns>
        public long m_lngInitDataAcquisition()
        {
            return m_lngInit();
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <returns></returns>
        public virtual long m_lngStartWork()
        {
            if (m_frmParent == null || m_objDeviceConfigVO == null)
            {
                m_mthShowMsg("����������Ϣ���豸������Ϣ����");
                return -1;
            }
            if (m_objDeviceConfigVO.strCOM_No == "0")
            {
                m_mthShowMsg("COM�ڴ���");
                return -1;
            }

            m_objSerialPort = new clsSerialPortIO(m_objDeviceConfigVO);
            //m_frmParent.Controls.Add(m_objSerialPort);

            long lngRes = 0;
            if (m_objSerialPort.IsOpen)
            {
                m_mthShowMsg("ָ���Ĵ����ѱ�ռ�ã�");
            }
            else
            {
                try
                {
                    m_objSerialPort.Open();
                }
                catch (Exception objEx)
                {
                    m_mthShowMsg(objEx.Message);
                    m_objSerialPort.Dispose();
                    return 0;
                }
                m_objSerialPort.DataComing += new DataComingEvent(m_objSerialPort_DataComing);
                //m_objSerialPort.ErrorReceived += new ErrorReceivedEvent(m_objSerialPort_ErrorReceived);
                m_frmParent.Controls.Add(m_objSerialPort);
                lngRes = 1;
            }

            if (lngRes > 0)
            {
                if (!string.IsNullOrEmpty(m_objDeviceConfigVO.strSend_Command))
                {
                    int iSendInver = 0;
                    int.TryParse(m_objDeviceConfigVO.strSend_Command_Internal, out iSendInver);

                    if (iSendInver > 0)
                    {
                        m_objTimer = new System.Timers.Timer();
                        m_objTimer.Interval = iSendInver;
                        m_objTimer.AutoReset = true;
                        m_objTimer.Elapsed += new System.Timers.ElapsedEventHandler(m_objTimer_Elapsed);
                        m_objTimer.Enabled = true;
                    }
                }
            }
            return lngRes;
        }

        void m_objTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string str = com.digitalwave.Utility.clsHexConvert.m_strToTextString(m_objDeviceConfigVO.strSend_Command);
            m_mthSend(str);
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="p_frmParent">��������</param>
        /// <param name="p_objDeviceConfigVO">�豸������Ϣ</param>
        /// <returns></returns>
        public virtual long m_lngStartWork(Form p_frmParent, clsLIS_Equip_ConfigVO p_objDeviceConfigVO)
        {
            m_frmParent = p_frmParent;
            m_objDeviceConfigVO = p_objDeviceConfigVO;
            return m_lngStartWork();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public virtual long m_lngFinishWork()
        {
            if (m_objTimer != null)
            {
                m_objTimer.Enabled = false;
                m_objTimer.Dispose();
            }

            if (m_objSerialPort != null)
            {
                m_objSerialPort.Close();
                m_frmParent.Controls.Remove(m_objSerialPort);
                m_objSerialPort.Dispose();
            }
            m_objSerialPort = null;

            return 1;
        }
        /// <summary>
        /// �̳��������д�˷���
        /// </summary>
        /// <param name="p_strRawData"></param>
        /// <param name="p_DeviceResultArr"></param>
        /// <returns></returns>
        protected virtual long m_lngDataAnalysis(string p_strRawData, out clsLIS_Device_Test_ResultVO[] p_DeviceResultArr)
        {
            p_DeviceResultArr = null;
            return 0;
        }
        /// <summary>
        /// �̳��������д�˷���
        /// </summary>
        /// <param name="p_strRawData"></param>
        /// <returns></returns>
        protected virtual string[] m_strGetIntactData(string p_strRawData)
        {
            return null;
        }
        protected virtual string[] m_strGetIntactData(byte[] p_objData)
        {
            return null;
        }

        #endregion

        void m_objSerialPort_DataComing(object sender, EventArgs e)
        {
            clsSerialPortIO objSP = sender as clsSerialPortIO;
            if (objSP == null)
            {
                return;
            }
            string strInstrument_ID = objSP.Name;
            object objData_Received;
            if (m_enuDateType == enuReceiveDateType.ASCII)
            {
                objData_Received = objSP.Read();
            }
            else
            {
                byte[] byteReceived = null;
                objSP.Read(out byteReceived);
                objData_Received = byteReceived;
            }

            m_mthLogDate(objData_Received);

            this.m_lngDataComing(sender, objData_Received, m_objDeviceConfigVO);
        }

        void m_objSerialPort_ErrorReceived(object sender, EventArgs e)
        {
            SerialErrorReceivedEventArgs errorE = (SerialErrorReceivedEventArgs)e;
            if (errorE != null)
            {
                string strErrorMsg = "";
                string strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " -- ";
                switch (errorE.EventType)
                {
                    case SerialError.Frame:
                        strErrorMsg = "Ӳ����⵽һ����֡����";
                        break;

                    case SerialError.Overrun:
                        strErrorMsg = "�����ַ��������������һ���ַ�����ʧ��";
                        break;
                    case SerialError.RXOver:
                        strErrorMsg = "�������뻺������������뻺�����ռ䲻�㣬�����ļ�β (EOF) �ַ�֮����յ��ַ���";
                        break;
                    case SerialError.RXParity:
                        strErrorMsg = "Ӳ����⵽��żУ�����";
                        break;
                    case SerialError.TXFull:
                        strErrorMsg = "Ӧ�ó����Դ���һ���ַ����������������������";
                        break;
                }

                strErrorMsg = strNow + strErrorMsg;
                m_objLogger.LogError(strErrorMsg);

                m_mthShowMsg(strErrorMsg);
            }

        }

        #region ���ݴ���
        /// <summary>
        /// ���ݴ��� һ�����
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="p_objReceiveData"></param>
        /// <param name="p_objDeviceConfigVO"></param>
        /// <returns></returns>
        protected virtual long m_lngDataComing(object Sender, object p_objReceiveData, clsLIS_Equip_ConfigVO p_objDeviceConfigVO)
        {
            long lngRes = 0;
            try
            {
                #region ���ݴ��� һ�����
                string[] strIntactDataList = null;

                if (m_enuDateType == enuReceiveDateType.ASCII)
                {
                    strIntactDataList = this.m_strGetIntactData(p_objReceiveData.ToString());
                }
                else if (m_enuDateType == enuReceiveDateType.BINARY)
                {
                    strIntactDataList = this.m_strGetIntactData((byte[])p_objReceiveData);
                }
                if (strIntactDataList != null && strIntactDataList.Length > 0)
                {
                    for (int idx = 0; idx < strIntactDataList.Length; idx++)
                    {
                        string strIntactData = strIntactDataList[idx];
                        clsLIS_Device_Test_ResultVO[] objDeviceResultArr = null;

                        m_mthLogInfo("Data Analysis");

                        lngRes = this.m_lngDataAnalysis(strIntactData, out objDeviceResultArr);

                        if (objDeviceResultArr != null && objDeviceResultArr.Length > 0)
                        {
                            try
                            {
                                string strDeviceID = p_objDeviceConfigVO.strLIS_Instrument_ID;
                                for (int i = 0; i < objDeviceResultArr.Length; i++)
                                {
                                    objDeviceResultArr[i].strDevice_ID = strDeviceID;
                                }

                                m_mthLogInfo("Data insert database");
                                clsLIS_Device_Test_ResultVO[] objOutDeviceResultArr = null;
                                lngRes = m_objDomain.m_lngAddLabResult(objDeviceResultArr, m_blnMuiltySample, out objOutDeviceResultArr);

                                if (lngRes > 0)
                                {
                                    objDeviceResultArr = objOutDeviceResultArr;
                                }
                            }
                            catch (Exception ex)
                            {
                                lngRes = 0;
                                if (m_objLogger == null)
                                {
                                    m_objLogger = new clsLogText();
                                }
                                m_objLogger.LogDetailError(ex, false);
                            }

                            #region ��ʾʵʱ��Ϣ
                            if (lngRes > 0 && objDeviceResultArr != null && objDeviceResultArr.Length > 0)
                            {
                                m_mthDataShow(p_objDeviceConfigVO, objDeviceResultArr, m_blnMuiltySample);
                            }
                            #endregion
                        }
                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                if (evnAcquisitionInfo != null)
                {
                    evnAcquisitionInfo(objEx.Message);
                }
                if (m_objLogger == null)
                {
                    m_objLogger = new clsLogText();
                }
                m_objLogger.LogDetailError(objEx, false);
            }
            return lngRes;
        }
        /// <summary>
        /// ���ݴ���
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        protected virtual long m_lngDataProcess(clsLIS_Device_Test_ResultVO[] p_objResultArr)
        {
            long lngRes = 0;
            if (p_objResultArr == null || p_objResultArr.Length <= 0)
                return lngRes;

            if (m_objDeviceConfigVO == null)
                return lngRes;

            try
            {
                string strDeviceID = m_objDeviceConfigVO.strLIS_Instrument_ID;
                for (int idx = 0; idx < p_objResultArr.Length; idx++)
                {
                    p_objResultArr[idx].strDevice_ID = strDeviceID;
                }

                m_mthLogInfo("Data insert database");
                clsLIS_Device_Test_ResultVO[] objOutDeviceResultArr = null;
                lngRes = m_objDomain.m_lngAddLabResult(p_objResultArr, m_blnMuiltySample, out objOutDeviceResultArr);

                if (lngRes > 0)
                {
                    m_mthDataShow(m_objDeviceConfigVO, objOutDeviceResultArr, m_blnMuiltySample);
                }
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                if (evnAcquisitionInfo != null)
                {
                    evnAcquisitionInfo(objEx.Message);
                }
                if (m_objLogger == null)
                {
                    m_objLogger = new clsLogText();
                }
                m_objLogger.LogDetailError(objEx, false);
            }
            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="p_objDeviceConfigVO"></param>
        /// <param name="p_objResultArr"></param>
        protected void m_mthDataShow(clsLIS_Equip_ConfigVO p_objDeviceConfigVO, clsLIS_Device_Test_ResultVO[] p_objResultArr)
        {
            if (evnDataShow != null)
            {
                m_mthLogInfo("Data Show");

                clsDeviceSampleDataKey objKey = new clsDeviceSampleDataKey();
                objKey.intResultBeginIndex = p_objResultArr[0].intIndex;
                objKey.intResultEndIndex = p_objResultArr[p_objResultArr.Length - 1].intIndex;
                objKey.strDeviceID = p_objDeviceConfigVO.strLIS_Instrument_ID;
                objKey.strDeviceName = p_objDeviceConfigVO.strLIS_Instrument_Name;
                objKey.strDeviceSampleID = p_objResultArr[0].strDevice_Sample_ID;
                objKey.strCheckDate = p_objResultArr[0].strCheck_Date;

                m_frmParent.Invoke(evnDataShow, new object[] { objKey, p_objResultArr });
            }
        }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="p_objDeviceConfigVO"></param>
        /// <param name="p_objResultArr"></param>
        protected void m_mthDataShow(clsLIS_Equip_ConfigVO p_objDeviceConfigVO, clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample)
        {
            if (evnDataShow != null)
            {
                m_mthLogInfo("Data Show");

                clsDeviceSampleDataKey objKey = null;
                if (p_blnMuiltySample)
                {
                    string strSampleID = "";
                    string strSampleIDTemp = null;
                    List<string> lstSampleID = new List<string>();
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
                    List<clsLIS_Device_Test_ResultVO> lstResult = new List<clsLIS_Device_Test_ResultVO>();
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
                            objKey = new clsDeviceSampleDataKey();
                            objKey.intResultBeginIndex = lstResult[0].intIndex;
                            objKey.intResultEndIndex = lstResult[lstResult.Count - 1].intIndex;
                            objKey.strDeviceID = p_objDeviceConfigVO.strLIS_Instrument_ID;
                            objKey.strDeviceName = p_objDeviceConfigVO.strLIS_Instrument_Name;
                            objKey.strDeviceSampleID = lstResult[0].strDevice_Sample_ID;
                            objKey.strCheckDate = lstResult[0].strCheck_Date;

                            m_frmParent.Invoke(evnDataShow, new object[] { objKey, lstResult.ToArray() });
                        }
                    }
                }
                else
                {
                    objKey = new clsDeviceSampleDataKey();
                    objKey.intResultBeginIndex = p_objResultArr[0].intIndex;
                    objKey.intResultEndIndex = p_objResultArr[p_objResultArr.Length - 1].intIndex;
                    objKey.strDeviceID = p_objDeviceConfigVO.strLIS_Instrument_ID;
                    objKey.strDeviceName = p_objDeviceConfigVO.strLIS_Instrument_Name;
                    objKey.strDeviceSampleID = p_objResultArr[0].strDevice_Sample_ID;
                    objKey.strCheckDate = p_objResultArr[0].strCheck_Date;

                    m_frmParent.Invoke(evnDataShow, new object[] { objKey, p_objResultArr });
                }
            }
        }
        /// <summary>
        /// ��ʾ��Ϣ�Ի���
        /// </summary>
        /// <param name="p_strMsg"></param>
        protected void m_mthShowMsg(string p_strMsg)
        {
            MessageBox.Show(p_strMsg, m_strMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// ��¼����������־
        /// </summary>
        /// <param name="p_strDate"></param>
        protected void m_mthLogDate(object p_objDate)
        {
            if (p_objDate == null)
                return;

            if (m_blnLogger)
            {
                if (p_objDate is byte[])
                {
                    m_objLogger.Log2File(@"D:\code\logData.txt", Encoding.Default.GetString((byte[])p_objDate));
                }
                else
                {
                    m_objLogger.Log2File(@"D:\code\logData.txt", p_objDate.ToString());
                }
            }
        }
        /// <summary>
        /// ��¼�ӿ�״̬
        /// </summary>
        /// <param name="p_strInfo"></param>
        protected void m_mthLogInfo(string p_strInfo)
        {
            if (m_blnLogger)
            {
                m_objLogger.Log2File(@"D:\code\logInfo.txt", p_strInfo, DateTime.Now.ToLongTimeString());
            }

        }
        /// <summary>
        /// ��ȡ����ͨ����
        /// </summary>
        /// <returns></returns>
        protected List<string> m_lstStrGetDeviceItemNO()
        {
            List<string> lstStr = null;
            if (m_hasItemConvert != null)
            {
                lstStr = m_hasItemConvert.Keys as List<string>;
            }
            return lstStr;
        }

        protected virtual string m_strConvertItem(string p_strItem)
        {
            string key = null;
            if (!string.IsNullOrEmpty(p_strItem))
            {
                key = p_strItem.ToLower();
            }
            if ((key != null) && this.m_hasItemConvert != null)
            {
                if (this.m_hasItemConvert.Contains(key))
                {
                    return this.m_hasItemConvert[key].ToString();
                }
            }
            return p_strItem;
        }

        protected virtual string m_strConvertValue(string p_strItemName, string p_strItemValue)
        {
            if (p_strItemName == null)
            {
                p_strItemName = "";
            }
            if (p_strItemValue == null)
            {
                p_strItemValue = "";
            }
            if (string.IsNullOrEmpty(p_strItemName) && string.IsNullOrEmpty(p_strItemValue))
                return p_strItemValue;

            string key = p_strItemName + "-" + p_strItemValue;
            if (!string.IsNullOrEmpty(key) && this.m_hasConvertValue != null)
            {
                if (this.m_hasConvertValue.Contains(key))
                {
                    return this.m_hasConvertValue[key].ToString();
                }
            }
            return p_strItemValue;
        }

        protected virtual string[] m_strGetIntactData(ref string p_strRawData, string p_strStart, string p_strEnd)
        {
            int startIndex = 0;
            string[] strArray = null;
            if (((p_strRawData == null) || (p_strStart == null)) || (p_strEnd == null))
            {
                return null;
            }
            ArrayList list = new ArrayList();
            int index = p_strRawData.IndexOf(p_strStart);
            while (index >= 0)
            {
                int num3 = -1;
                if ((index + p_strStart.Length) < p_strRawData.Length)
                {
                    num3 = p_strRawData.IndexOf(p_strEnd, (int)(index + p_strStart.Length));
                }
                if (num3 >= 0)
                {
                    list.Add(p_strRawData.Substring(index, (num3 - index) + p_strEnd.Length));
                    startIndex = num3 + p_strEnd.Length;
                    if (startIndex < p_strRawData.Length)
                    {
                        index = p_strRawData.IndexOf(p_strStart, startIndex);
                    }
                    else
                    {
                        index = -1;
                    }
                }
                else
                {
                    index = -1;
                }
            }
            if (list.Count != 0)
            {
                strArray = (string[])list.ToArray(typeof(string));
            }
            p_strRawData = p_strRawData.Remove(0, startIndex);
            return strArray;
        }

        protected virtual string[] m_strGetIntactData(string p_strRawData, string p_strStart, string p_strEnd, out int p_intCutIdx)
        {
            p_intCutIdx = 0;
            string[] strArray = null;
            if (((p_strRawData == null) || (p_strStart == null)) || (p_strEnd == null))
            {
                return null;
            }

            List<string> lstStr = new List<string>();
            int index = p_strRawData.IndexOf(p_strStart);
            while (index >= 0)
            {
                int num2 = -1;
                if ((index + p_strStart.Length) < p_strRawData.Length)
                {
                    num2 = p_strRawData.IndexOf(p_strEnd, (int)(index + p_strStart.Length));
                }
                if (num2 >= 0)
                {
                    lstStr.Add(p_strRawData.Substring(index, (num2 - index) + p_strEnd.Length));
                    p_intCutIdx = num2 + p_strEnd.Length;
                    if (p_intCutIdx < p_strRawData.Length)
                    {
                        index = p_strRawData.IndexOf(p_strStart, p_intCutIdx);
                    }
                    else
                    {
                        index = -1;
                    }
                }
                else
                {
                    index = -1;
                }
            }
            if (lstStr.Count != 0)
            {
                strArray = lstStr.ToArray();
            }
            return strArray;
        }

        public void m_mthDoEvnAcquisitionInfo(string p_strAcquisitionInfo)
        {
            if (string.IsNullOrEmpty(p_strAcquisitionInfo))
                return;
            if (this.evnAcquisitionInfo != null)
            {
                this.evnAcquisitionInfo(p_strAcquisitionInfo);
            }
        }

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_strOrder"></param>
        protected virtual void m_mthSend(string p_strOrder)
        {
            if (m_objSerialPort != null)
            {
                m_objLogger.Log2File(@"D:\code\logData.txt", p_strOrder, DateTime.Now.ToLongTimeString() + " HOST-->");
                m_objSerialPort.Send(p_strOrder);
            }
        }
        #endregion

        #region ��ʼ���ӿ�
        /// <summary>
        /// ��ʼ���ӿ�
        /// </summary>
        /// <returns></returns>
        protected long m_lngInit()
        {
            long lngRes = 0;
            try
            {
                if (m_objDeviceConfigVO != null)
                {
                    clsDeviceItemNameItemNO[] objDeviceItemArr = null;
                    clsDeviceItemValueConvert_VO[] objItemConvertArr = null;
                    lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceNOConvertInfo(m_objDeviceConfigVO.strLIS_Instrument_ID, out objDeviceItemArr, out objItemConvertArr);

                    if (lngRes > 0)
                    {
                        string strTemp = null;
                        if (objDeviceItemArr != null && objDeviceItemArr.Length > 0)
                        {
                            m_hasItemConvert = new Hashtable();
                            foreach (clsDeviceItemNameItemNO objTemp in objDeviceItemArr)
                            {
                                if (!string.IsNullOrEmpty(objTemp.m_strDeviceItemNO) && !m_hasItemConvert.Contains(objTemp.m_strDeviceItemNO.ToLower()))
                                {
                                    m_hasItemConvert.Add(objTemp.m_strDeviceItemNO.ToLower(), objTemp.m_strDeviceItemName);
                                }
                            }
                        }
                        if (objItemConvertArr != null && objItemConvertArr.Length > 0)
                        {
                            m_hasConvertValue = new Hashtable();
                            foreach (clsDeviceItemValueConvert_VO objValueConvert in objItemConvertArr)
                            {
                                strTemp = objValueConvert.m_strDeviceItemName + "-" + objValueConvert.m_strDeviceItemValue;
                                if (!m_hasConvertValue.Contains(strTemp))
                                {
                                    m_hasConvertValue.Add(strTemp, objValueConvert.m_strDeviceConvertValue);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                if (evnAcquisitionInfo != null)
                {
                    evnAcquisitionInfo(objEx.Message);
                }
                m_objLogger.LogDetailError(objEx, false);
            }
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ͨ�����������뵥������Ŀ��Ӧ��ͨ����
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strDeviceNoArr"></param>
        /// <returns></returns>
        protected long m_lngQueryAppDeviceNOByBarCode(string p_strBarCode, out string[] p_strDeviceNoArr)
        {
            p_strDeviceNoArr = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strBarCode))
                return lngRes;
            if (m_objDeviceConfigVO == null || string.IsNullOrEmpty(m_objDeviceConfigVO.strLIS_Instrument_ID))
                return lngRes;

            lngRes = m_objDomain.m_lngQueryAppDeviceNOByBarCode(m_objDeviceConfigVO.strLIS_Instrument_ID, p_strBarCode, out p_strDeviceNoArr);
            return lngRes;
        }
        /// <summary>
        /// ͨ�����������뵥��Ϣ�������Ŀ��Ӧ��ͨ����
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_objAppMainVO"></param>
        /// <param name="p_strDeviceNOArr"></param>
        /// <returns></returns>
        protected long m_lngQueryAppInfoAndDeviceNO(string p_strBarCode, out clsLisApplMainVO p_objAppMainVO, out string[] p_strDeviceNOArr)
        {
            p_objAppMainVO = null;
            p_strDeviceNOArr = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strBarCode))
                return lngRes;
            if (m_objDeviceConfigVO == null || string.IsNullOrEmpty(m_objDeviceConfigVO.strLIS_Instrument_ID))
                return lngRes;

            lngRes = m_objDomain.m_lngQueryAppInfoAndDeviceNO(m_objDeviceConfigVO.strLIS_Instrument_ID, p_strBarCode, out p_objAppMainVO, out p_strDeviceNOArr);
            return lngRes;
        }
        /// <summary>
        /// ���¼����ţ�ͬʱ����T_OPR_LIS_DEVICE_RELATION��
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <returns></returns>
        protected long m_lngUpdateAppCheckNO(string p_strBarCode, string p_strDeviceSampleID)
        {
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strBarCode))
                return lngRes;
            if (m_objDeviceConfigVO == null || string.IsNullOrEmpty(m_objDeviceConfigVO.strLIS_Instrument_ID))
                return lngRes;

            lngRes = m_objDomain.m_lngUpdateAppCheckNO(p_strBarCode, m_objDeviceConfigVO.strLIS_Instrument_ID, m_objDeviceConfigVO.strLIS_Instrument_NO, p_strDeviceSampleID);
            return lngRes;
        }
        #endregion
    }

    /// <summary>
    /// �ɼ����ݸ�ʽ
    /// </summary>
    public enum enuReceiveDateType
    {
        /// <summary>
        /// �� Ascii ����գ�ת��String 
        /// </summary>
        ASCII = 0,
        /// <summary>
        /// �� Binary ����գ�ת��Ϊbyte[]
        /// </summary>
        BINARY = 1
    }
}
