using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Collections;
using com.digitalwave.iCare.middletier.LIS;
using com.digitalwave.Utility;
using System.IO;

namespace com.digitalwave.iCare.LIS
{
    /// <summary>
    /// �������ݲɼ������࣬��ȡ����ģʽ
    /// </summary>
    public class clsLISDataAcquisitionBase_DB:infLISDataAcquisition_DB
    {
        #region ����
        private clsLIS_Equip_DB_ConfigVO _objDeviceConfigVO;
        private Form _frmParent;
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
        private bool _blnLogger = false;
        /// <summary>
        /// ��־��¼��
        /// </summary>
        clsLogText m_objLogger;
        string m_strMsgTitle = "���ݲɼ�";
        /// <summary>
        /// ÿ�α���ʱ�Ƿ������һ�𱣴�
        /// </summary>
        bool m_blnMuiltySample = false;
        clsDcl_DataAcquisition m_objDomain;
        /// <summary>
        /// ���ݷ�����
        /// </summary>
        public clsLISDBService m_objService = null;
        

        /// <summary>
        /// ����DSN  2012-01-20 yongchao.li����
        /// </summary>
        public string m_strDSN = null;

        #endregion

        public clsLISDataAcquisitionBase_DB()
        {
            m_objLogger = new clsLogText();
            m_objDomain = new clsDcl_DataAcquisition();
        }

        #region infLISDataAcquisition_DB ��Ա
        public event DataShowEventHandler evnDataShow;

        public event DataAcquisitionInfoEventHandler evnAcquisitionInfo;
        /// <summary>
        /// �豸������Ϣ  2012-01-20 yongchao.li�޸�
        /// </summary>
        public clsLIS_Equip_DB_ConfigVO m_objDeviceConfigVO
        {
            get
            {
                return _objDeviceConfigVO;
            }
            set
            {
                _objDeviceConfigVO = value;
              
                if(_objDeviceConfigVO!=null)
                {
                    m_strDSN = _objDeviceConfigVO.strONLINE_DNS_VCHR;
                }
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

        public bool m_blnLogger
        {
            get
            {
                return _blnLogger;
            }
            set
            {
                _blnLogger = value;
            }
        }

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
        public long m_lngStartWork()
        {
            if (m_frmParent == null || m_objDeviceConfigVO == null)
            {
                m_mthShowMsg("����������Ϣ���豸������Ϣ����");
                return -1;
            }

            if (string.IsNullOrEmpty(m_objDeviceConfigVO.strONLINE_MODULE_CHR))
            {
                m_mthShowMsg("�豸������Ϣ���ô���");
                return -1;
            }

            switch (m_objDeviceConfigVO.strONLINE_MODULE_CHR)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    if (string.IsNullOrEmpty(m_objDeviceConfigVO.strONLINE_DNS_VCHR))
                    {
                        m_mthShowMsg("�豸������Ϣ(DNS)���ô���");
                        return -1;
                    }
                    break;
            }
            if (string.IsNullOrEmpty(m_objDeviceConfigVO.strWORK_MODULE_CHR))
            {
                m_mthShowMsg("�豸������Ϣ(������ʽ)���ô���");
                return -1;
            }

            if (m_objDeviceConfigVO.strWORK_MODULE_CHR.Substring(0, 1) == "1")
            {
                if (!string.IsNullOrEmpty(m_objDeviceConfigVO.strWORK_AUTO_INTERNAL_VCHR))
                {
                    int iInternal = 0;
                    int.TryParse(m_objDeviceConfigVO.strWORK_AUTO_INTERNAL_VCHR, out iInternal);
                    if (iInternal > 0)
                    {
                        m_objTimer = new System.Timers.Timer();
                        m_objTimer.AutoReset = false;
                        m_objTimer.Interval = iInternal * 1000;
                        m_objTimer.Elapsed += new System.Timers.ElapsedEventHandler(m_objTimer_Elapsed);
                        m_objTimer.Enabled = true;
                    }
                }
            }
            return 1;
        }

        void m_objTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            m_objTimer.Stop();
            try
            {
                m_lngWorkByAuto();
            }
            catch (Exception objEx)
            {
                m_mthLogError(objEx);
            }
            m_objTimer.Start();
        }

        public long m_lngStartWork(Form p_frmParent, clsLIS_Equip_DB_ConfigVO p_objDeviceConfigVO)
        {
            m_frmParent = p_frmParent;
            m_objDeviceConfigVO = p_objDeviceConfigVO;

            return m_lngStartWork();
        }

        public virtual long m_lngWorkByAuto()
        {
            return 1;
        }

        public virtual long m_lngWorkByHandle()
        {
            return 1;
        }

        public long m_lngFinishWork()
        {
            m_objService = null;
            m_objTimer.Close();
            m_objTimer.Dispose();
            return 1;
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
                     lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceNOConvertInfo(  m_objDeviceConfigVO.strLIS_Instrument_ID, out objDeviceItemArr, out objItemConvertArr);
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
                    if (!string.IsNullOrEmpty(m_objDeviceConfigVO.strONLINE_MODULE_CHR) && 
                        m_objDeviceConfigVO.strONLINE_MODULE_CHR != "5" && 
                        !string.IsNullOrEmpty(m_objDeviceConfigVO.strONLINE_DNS_VCHR))
                    {
                        m_objService = new clsLISDBService(m_objDeviceConfigVO.strONLINE_MODULE_CHR, m_objDeviceConfigVO.strONLINE_DNS_VCHR);
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

        #region ��������
        public void m_mthSetMuiltySample(bool p_blnMuiltySample)
        {
            m_blnMuiltySample = p_blnMuiltySample;
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

        /// <summary>
        /// ��ʾ��Ϣ�Ի���
        /// </summary>
        /// <param name="p_strMsg"></param>
        protected void m_mthShowMsg(string p_strMsg)
        {
            MessageBox.Show(p_strMsg, m_strMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// ��¼�쳣��Ϣ
        /// </summary>
        /// <param name="objEx"></param>
        protected void m_mthLogError(Exception objEx)
        {
            if (m_blnLogger)
            {
                m_objLogger.LogDetailError(objEx, false);
            }
        }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="p_objDeviceConfigVO"></param>
        /// <param name="p_objResultArr"></param>
        protected void m_mthDataShow(clsLIS_Equip_DB_ConfigVO p_objDeviceConfigVO, clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample)
        {
            if (evnDataShow != null)
            {
                m_mthLogInfo("Data Show");

                clsDeviceSampleDataKey objKey = null;
                if (p_blnMuiltySample)
                {
                    List<string> lstSampleID = new List<string>();
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
        #endregion

        #region ���ݴ���
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

        #region ����ͼ��
        /// <summary>
        /// ����ͼ��
        /// </summary>
        /// <param name="p_strPath">ͼƬ·��</param>
        /// <returns></returns>
        protected byte[] m_objConstructImage(string p_strPath)
        {
            byte[] b_img = null;
            if (File.Exists(p_strPath))
            {
                try
                {
                    FileStream fs = new FileStream(p_strPath, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    b_img = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();
                    return b_img;
                }
                catch (Exception objEx)
                {
                    m_mthLogError(objEx);
                }
            }
            return b_img;
        }
        #endregion
    }
}
