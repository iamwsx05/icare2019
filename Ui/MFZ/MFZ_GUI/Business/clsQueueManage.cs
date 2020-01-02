using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using weCare.Core.Entity;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using System.Messaging;
using SpeechLib;
using Microsoft.VisualBasic;
using System.Xml;

namespace com.digitalwave.iCare.gui.MFZ
{

    #region 队列管理

    public class clsQueueManage
    {

        #region 构造函数[事件注册]

        public clsQueueManage()
        {
            this.QueueModified += new QueueModifiedEventHandler(clsQueueManage_QueueModified);
            this.ClientConnected += new ClientConnectedEventHandler(clsQueueManage_ClientConnected);
            this.PatientCountChanged += new PatientCountChangeEventHandler(clsQueueManage_PatientCountChanged);
            comSvc = new clsCommunicationSvc();
            comSvc.ClientConnectRequest += new ClientConnectRequestEventHandler(comSvc_ClientConnectRequest);
            comSvc.ConnectionError += new ConnectionErrorEventHandler(comSvc_ConnectionError);
            comSvc.DataReceived += new DataReceivedEventHandler(comSvc_DataReceived);
            voice = new SpVoiceClass();
        }

        #endregion

        #region 成　  员

        public clsLED ledShow = null;
        public clsLCDManager lcdShow = null;
        private clsCommunicationSvc comSvc;
        private clsDataModule dataModule;
        private string path = Application.StartupPath + "\\LoginFile.xml";
        /// <summary>
        /// 所有队列数据
        /// </summary>
        private Dictionary<string, List<clsMFZPatientVO>> queueDictionary = new Dictionary<string, List<clsMFZPatientVO>>();
        /// <summary>
        /// 医生,电脑IP对应集合
        /// </summary>
        private Dictionary<string, string> doctCoputerDictionary = new Dictionary<string, string>();
        /// <summary>
        /// 医生叫过的患者计数
        /// </summary>
        private Hashtable hasCalledPatient = new Hashtable();
        private int m_intSchemeId;

        public string strVoiceLibPTH = "";
        public string strVoiceLibYy = "";
        public SpVoiceClass voice = null;
        SpVoice voicePTH = new SpVoice();
        SpVoice voiceYy = new SpVoice();

        /// <summary>
        /// 队列改变事件
        /// </summary>
        internal event QueueModifiedEventHandler QueueModified;
        /// <summary>
        /// 客户端连接上事件
        /// </summary>
        internal event ClientConnectedEventHandler ClientConnected;
        /// <summary>
        /// 客户端连接失败事件
        /// </summary>
        internal event ConnectionErrorEventHandler ConnectionErrored;
        /// <summary>
        /// 数据接受事件
        /// </summary>
        internal event DataReceivedEventHandler ClientDataReceived;
        /// <summary>
        /// 叫号数改变事件
        /// </summary>
        internal event PatientCountChangeEventHandler PatientCountChanged;

        #endregion

        #region 静态常量
        public const string CALLEDQUEUE = "calledQueue";
        public const string COMMONWAITQUEUE = "comShareWaitQueue";
        public const string EXPWAITQUEUE = "expShareWaitQueue";
        #endregion

        #region 事件处理实现

        /// <summary>
        /// 根据参数设置发声类所用的声音库
        /// </summary>
        public void m_mthSetVoice()
        {
            ISpeechObjectTokens obj = voicePTH.GetVoices("", "");
            for (int i = 0; i < obj.Count; i++)
            {
                string desc = obj.Item(i).GetDescription(0);
                if (desc.Equals(strVoiceLibPTH))
                {
                    voicePTH.Voice = obj.Item(i);
                }
                if (desc.Equals(strVoiceLibYy))
                {
                    voiceYy.Voice = obj.Item(i);
                }
            } 
        }

        /// <summary>
        /// 使用设定语音库读信息
        /// </summary>
        /// <param name="p_strText"></param>
        public void m_mthSpeak(string p_strText)
        {
            //SpeechVoiceSpeakFlags SSF = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            ////普通话读信息
            //voicePTH.Speak(p_strText, SSF);
            //voicePTH.WaitUntilDone(30000);
            ////转换为繁体使用粤语读信息
            //p_strText = Microsoft.VisualBasic.Strings.StrConv(p_strText, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
            //voiceYy.Speak(p_strText, SSF);
            int FlagPth, FlagYy;//FlagPth 普通话，FlagYy 粤语
            m_mthGetVoiceSetting(out FlagPth, out FlagYy);
            SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            com.digitalwave.Utility.clsLogText objlog = new com.digitalwave.Utility.clsLogText();
            objlog.LogError(p_strText);
            //转换成繁体
            string strYy = Strings.StrConv(p_strText, VbStrConv.TraditionalChinese, 0);
            try
            {
                lock (voice)
                {
                    voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(FlagYy);
                    voice.Speak(strYy, SpFlags);
                    voice.WaitUntilDone(1000);
                    voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(FlagPth);
                    voice.Speak(strYy, SpFlags);
                }
            }
            catch (Exception ex)
            {
                objlog.LogError(ex.Message.ToString());
            }
        }

        /// <summary>
        /// 语音设置
        /// </summary>
        /// <param name="FlagPth"></param>
        /// <param name="FlagYy"></param>
        private void m_mthGetVoiceSetting(out int FlagPth, out int FlagYy)
        {
            FlagPth = -1; FlagYy = -1;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            string strVoiceLibPTH = doc["Main"]["MFZ_GUI"]["VoiceLibPTH"].Attributes["value"].Value.ToString();
            string strVoiceLibYy = doc["Main"]["MFZ_GUI"]["VoiceLibYy"].Attributes["value"].Value;
            ISpeechObjectTokens obj = voicePTH.GetVoices("", "");
            for (int i = 0; i < obj.Count; i++)
            {
                string desc = obj.Item(i).GetDescription(0);
                if (desc.Equals(strVoiceLibPTH))
                {
                    FlagPth = i;
                }
                if (desc.Equals(strVoiceLibYy))
                {
                    FlagYy = i;
                }
            } 
        }

        private void clsQueueManage_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            string strClient = doctCoputerDictionary[e.DoctorID];

            DataReceivedEventArgs receivedE = new DataReceivedEventArgs(strClient, null);
            ClientGetQueueStatus(receivedE, e.DoctorID, GetQueueShareName(e.DoctorID), null);
        }
        private void clsQueueManage_QueueModified(object sender, QueueModifiedEventArgs e)
        {
            lock (dataModule)
            {
                NotifyClientQueueStatus();
                SerializeData();
            }
        }
        private void clsQueueManage_PatientCountChanged(object sender, PatientCountChangeEventArgs e)
        {
            lock (hasCalledPatient)
            {
                string key = e.DoctId;
                if (!hasCalledPatient.Contains(key))
                {
                    if (e.IsAdded)
                    {
                        hasCalledPatient.Add(key, 1);
                    }
                    else 
                    {
                        hasCalledPatient.Add(key,0);
                    }
                }
                else
                {
                    int tempCount = (int)hasCalledPatient[key];
                    if (e.IsAdded)
                    {
                        tempCount += 1;
                    }
                    else 
                    {
                        tempCount -= 1;
                    }
                    hasCalledPatient[key] = tempCount;
                }
                e.CalledPatientCount = (int)hasCalledPatient[key];
                SerializeData();
            }
        }

        private void comSvc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (ClientDataReceived != null)
            {
                ClientDataReceived(this, e);
            }


            clsMFZCommunicationVO comObj;
            if (!GetDeSerializeObj(e, out comObj) || comObj == null)
            {
                return;
            }

            lock (dataModule)
            {
                if (doctCoputerDictionary.ContainsValue(e.Client))
                {
                    string strDocID = GetDoctID(e.Client); // docID
                    string strQueueName = GetQueueShareName(strDocID); //comQueue / expQueue 
                    if (strDocID != string.Empty)
                    {
                        switch (comObj.option)
                        {
                            case enmMFZCommunicationOption.CallNextPatient:
                                ClientCallNextPatient(e, strDocID, strQueueName, comObj);
                                break;
                            case enmMFZCommunicationOption.CallSomePatient:
                                ClientCallSomePatient(e, strDocID, strQueueName, comObj);
                                break;
                            case enmMFZCommunicationOption.GetQueueStatus:
                                ClientGetQueueStatus(e, strDocID, strQueueName, comObj);
                                break;
                            case enmMFZCommunicationOption.GetSomeQueue:
                                ClientGetSomeQueue(e, strDocID, strQueueName, comObj);
                                break;
                            case enmMFZCommunicationOption.RecallSomePatient:
                                ClientRecallSomePatient(e, strDocID, strQueueName, comObj);
                                break;
                            case enmMFZCommunicationOption.MovePatientToLast:
                                clsMFZPatientVO paitent= comObj.data as clsMFZPatientVO;
                                ClientMovePatientToLast(strDocID, paitent);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        private void comSvc_ConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            lock (((ICollection)doctCoputerDictionary).SyncRoot)
            {
                if (e.Client == null)
                {
                    ConnectionErrored(this, new ConnectionErrorEventArgs(e.ErrorMessage, null));
                    return;
                }
                else
                {
                    if (doctCoputerDictionary.ContainsValue(e.Client))
                    {
                        string strDocID = GetDoctID(e.Client);
                        if (strDocID != null)
                        {
                            ConnectionErrored(this, new ConnectionErrorEventArgs(e.ErrorMessage, strDocID));
                            return;
                        }
                    }
                }
            }
        }
        private void comSvc_ClientConnectRequest(object sender, ClientConnectRequestEventArgs e)
        {
            lock (((ICollection)doctCoputerDictionary).SyncRoot)
            {
                string strDocID = GetDoctID(e.Client);
                if (strDocID != string.Empty)
                {
                    ClientConnected(this, new ClientConnectedEventArgs(strDocID));
                    return;
                }
            }
            e.Reject = true;
            e.RejectReason = "未在诊区" + dataModule.daigArea.strDiagAreaName + "注册";
        }

        #endregion

        #region 客户端信息交互方法

        private static bool GetDeSerializeObj(DataReceivedEventArgs e, out clsMFZCommunicationVO obj)
        {
            obj = null;
            try
            {
                byte[] byts = (byte[])e.Data;
                IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.MemoryStream ms = new System.IO.MemoryStream(byts);
                obj = formatter.Deserialize(ms) as clsMFZCommunicationVO;
            }
            catch (Exception ex)
            {
                // 接收数据不成功
                //ShowMessage(MessageBoxIcon.Error, ex.Message);
                new com.digitalwave.Utility.clsLogText().LogError(ex.Message);
                return false;
            }
            return true;
        }
        private bool ClientCallNextPatient(DataReceivedEventArgs e, string strDoctID, string strQueueName, clsMFZCommunicationVO comObj)
        {
            clsMFZCommunicationVO resObj = new clsMFZCommunicationVO();
            if (queueDictionary[strDoctID].Count == 0)
            {
                List<clsMFZPatientVO> lstPatients = GetDoctShareQueue(strDoctID);

                if (lstPatients.Count == 0)
                {
                    resObj.option = enmMFZCommunicationOption.SendMessage;
                    resObj.data = "当前没有患者！";
                    resObj.strSouce = string.Empty;
                    bool res = SendToClient(e.Client, resObj);
                    if (res)
                    {
                        return true;
                    }
                }
                else
                {
                    resObj.option = enmMFZCommunicationOption.PatientCalled;
                    resObj.data = lstPatients[0];
                    resObj.strSouce = string.Empty;

                    if (SendToClient(e.Client, resObj))
                    {
                        CallNextPatient(strDoctID, strQueueName);
                        return true;
                    }
                }
            }
            else
            {
                resObj.option = enmMFZCommunicationOption.PatientCalled;
                resObj.data = queueDictionary[strDoctID][0];
                resObj.strSouce = string.Empty;
                if (SendToClient(e.Client, resObj))
                {
                    CallNextPatient(strDoctID, strQueueName);
                    return true;
                }
            }
            return false;
        }

        private bool ClientCallSomePatient(DataReceivedEventArgs e, string strDoctID, string strQueueName, clsMFZCommunicationVO comObj)
        {
            clsMFZCommunicationVO resObj = new clsMFZCommunicationVO();
            clsMFZPatientQueueType comPatient = comObj.data as clsMFZPatientQueueType;
            if (comPatient != null)
            {
                if (comPatient.queueType == enmMFZQueueType.PrivateWait)
                {
                    foreach (clsMFZPatientVO patient in queueDictionary[strDoctID])
                    {
                        if (patient.m_strPatientID == comPatient.patient.m_strPatientID)
                        {
                            resObj.option = enmMFZCommunicationOption.PatientCalled;
                            resObj.data = patient;
                            resObj.strSouce = string.Empty;
                            if (SendToClient(e.Client, resObj))
                            {
                                CallSomePatient(strDoctID, patient, true);
                                return true;
                            }
                        }
                    }
                    resObj.option = enmMFZCommunicationOption.SendMessage;
                    resObj.data = "患者已不在私有队列中！";
                    resObj.strSouce = string.Empty;
                    bool res2 = SendToClient(e.Client, resObj);
                    if (res2)
                    {
                        return true;
                    }
                }
                if (comPatient.queueType == enmMFZQueueType.ShareWait)
                {
                    if (strQueueName != string.Empty)
                    {
                        foreach (clsMFZPatientVO patient in queueDictionary[strQueueName])
                        {
                            if (patient.m_strPatientID == comPatient.patient.m_strPatientID)
                            {
                                resObj.option = enmMFZCommunicationOption.PatientCalled;
                                resObj.data = patient;
                                resObj.strSouce = string.Empty;
                                if (SendToClient(e.Client, resObj))
                                {
                                    CallSomePatient(strDoctID, patient, false);
                                    return true;
                                }
                            }
                        }

                        resObj.option = enmMFZCommunicationOption.SendMessage;
                        resObj.data = "患者已不在共享队列中！";
                        resObj.strSouce = string.Empty;
                        bool res2 = SendToClient(e.Client, resObj);
                        if (res2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool ClientGetQueueStatus(DataReceivedEventArgs e, string strDoctID, string strQueueName, clsMFZCommunicationVO comObj)
        {
            clsMFZCommunicationVO resObj = new clsMFZCommunicationVO();
            clsQueueStatus[] queueStatusArr = new clsQueueStatus[2];

            clsQueueStatus queuePrivate = new clsQueueStatus();
            queuePrivate.queueType = enmMFZQueueType.PrivateWait;
            queuePrivate.count = queueDictionary[strDoctID].Count;
            queueStatusArr[0] = queuePrivate;

            if (strQueueName != string.Empty)
            {
                clsQueueStatus queueShare = new clsQueueStatus();
                queueShare.queueType = enmMFZQueueType.ShareWait;

                queueShare.count = GetDoctShareQueue(strDoctID).Count;
                queueStatusArr[1] = queueShare;
            }

            resObj.option = enmMFZCommunicationOption.SetQueueStatus;
            resObj.strSouce = string.Empty;
            resObj.data = queueStatusArr;
            bool res = SendToClient(e.Client, resObj);
            if (res)
            {
                return true;
            }
            return false;
        }
        private bool ClientGetSomeQueue(DataReceivedEventArgs e, string strDoctID, string strQueueName, clsMFZCommunicationVO comObj)
        {
            clsMFZCommunicationVO resObj = new clsMFZCommunicationVO();
            enmMFZQueueType queueType = (enmMFZQueueType)comObj.data;
            if (queueType == enmMFZQueueType.PrivateWait)
            {
                clsMFZPatientVO[] patients = new clsMFZPatientVO[queueDictionary[strDoctID].Count];
                for (int i = 0; i < patients.Length; i++)
                {
                    patients[i] = queueDictionary[strDoctID][i];
                }

                clsMFZQueue queue = new clsMFZQueue();
                queue.queueType = queueType;
                queue.patients = patients;

                resObj.option = enmMFZCommunicationOption.SetSomeQueue;
                resObj.data = queue;
                resObj.strSouce = string.Empty;
                bool res = SendToClient(e.Client, resObj);
                if (res)
                {
                    return true;
                }
            }
            if (queueType == enmMFZQueueType.ShareWait)
            {
                List<clsMFZPatientVO> lstSharePatient = GetDoctShareQueue(strDoctID);
                clsMFZPatientVO[] patients = new clsMFZPatientVO[lstSharePatient.Count];
                for (int i = 0; i < patients.Length; i++)
                {
                    patients[i] = lstSharePatient[i];
                }

                clsMFZQueue queue = new clsMFZQueue();
                queue.queueType = queueType;
                queue.patients = patients;

                resObj.option = enmMFZCommunicationOption.SetSomeQueue;
                resObj.data = queue;
                resObj.strSouce = string.Empty;
                bool res = SendToClient(e.Client, resObj);
                if (res)
                {
                    return true;
                }
            }
            return false;
        }
        private bool ClientRecallSomePatient(DataReceivedEventArgs e, string strDoctID, string strQueueName, clsMFZCommunicationVO comObj)
        {
            clsMFZCommunicationVO resObj = new clsMFZCommunicationVO();
            clsMFZPatientVO patient = comObj.data as clsMFZPatientVO;
            if (patient != null)
            {
                RecallPatient(strDoctID, patient);
            }
            return true;
        }
        private bool ClientMovePatientToLast(string strDocID,clsMFZPatientVO patient)
        {
            bool isCalled=false;
            foreach (clsMFZPatientVO patientVO in queueDictionary[CALLEDQUEUE])
            {
                if (patientVO.m_strPatientCardNO == patient.m_strPatientCardNO)
                {
                    isCalled = true;
                    break;
                }
            }

            if (isCalled)
            {
                bool isContains = false;
                foreach (clsMFZPatientVO patientTemp in queueDictionary[strDocID])
                {
                    if (patientTemp.m_strPatientCardNO == patient.m_strPatientCardNO)
                    {
                        isContains = true;
                        break;
                    }
                }

                if (!isContains) 
                {
                    int len = queueDictionary[strDocID].Count;
                    QueueModified(this,new QueueModifiedEventArgs(strDocID,true,patient,len));
                    PatientCountChanged(this, new PatientCountChangeEventArgs(strDocID, false));
                    queueDictionary[strDocID].Add(patient);
                    NotifyClientQueueStatus();
                }
            }
            return true;
        }


        private void CallNextPatient(string strDoctID, string strQueueName)
        {
            List<clsMFZPatientVO> lstPatients = new List<clsMFZPatientVO>();
            clsMFZPatientVO callPatient;
            clsMFZPatientVO preparePatient;
            string ledMessage;
            string strTmpRoomName = GetDoctRoomName(strDoctID);
            if (queueDictionary[strDoctID].Count == 0)
            {
                lstPatients = GetDoctShareQueue(strDoctID);
                if (lstPatients.Count > 0)
                {
                    callPatient = lstPatients[0];
                    ledMessage = string.Format(clsConfig.CurrentConfig.CallPatientStyle, callPatient.m_strPatientName, strTmpRoomName);
                    //ledShow.Add(strDoctID, objMessage);
                    //ledShow.Show();
                    lcdShow.addToLCD(ledMessage, strDoctID, callPatient);
                    ledMessage = this.ReGenerateCallMessage(1, "", callPatient.m_strPatientName, strTmpRoomName);
                    //TTSClient.TTSClient.PlaySound(ledMessage);
                    m_mthSpeak(ledMessage);
                    MovePatient(lstPatients[0], strQueueName, CALLEDQUEUE, 0);
                    if (PatientCountChanged != null)
                    {
                        PatientCountChangeEventArgs e = new PatientCountChangeEventArgs(strDoctID,true);
                        PatientCountChanged(this, e);
                    }
                }
            }
            else
            {
                callPatient = queueDictionary[strDoctID][0];
                if (queueDictionary[strDoctID].Count > 1)
                {
                    preparePatient = queueDictionary[strDoctID][1];
                    ledMessage = string.Format(clsConfig.CurrentConfig.CallPatientStyle, callPatient.m_strPatientName, strTmpRoomName) + string.Format(clsConfig.CurrentConfig.PreparePatientStyle, preparePatient.m_strPatientName);
                }
                else
                {
                    ledMessage = string.Format(clsConfig.CurrentConfig.CallPatientStyle, callPatient.m_strPatientName, strTmpRoomName);
                }
                //ledShow.Add(strDoctID, objMessage);
                //ledShow.Show();
                lcdShow.addToLCD(ledMessage, strDoctID, callPatient);
                ledMessage = this.ReGenerateCallMessage(2, strDoctID, "", strTmpRoomName);
                m_mthSpeak(ledMessage);
                //TTSClient.TTSClient.PlaySound(ledMessage);
                MovePatient(queueDictionary[strDoctID][0], strDoctID, CALLEDQUEUE, 0);
                if (PatientCountChanged != null)
                {
                    PatientCountChangeEventArgs e = new PatientCountChangeEventArgs(strDoctID,true);
                    PatientCountChanged(this, e);
                }
            }
        }

        private void CallSomePatient(string strDoctID, clsMFZPatientVO patient, bool isPrivate)
        {
            string strTmpRoomName = GetDoctRoomName(strDoctID);
            string ledMessage = string.Format(clsConfig.CurrentConfig.CallPatientStyle, patient.m_strPatientName, strTmpRoomName);

            //ledShow.Add(strDoctID, objMessage);
            //ledShow.Show();
            lcdShow.addToLCD(ledMessage, strDoctID, patient);
            ledMessage = this.ReGenerateCallMessage(1, "", patient.m_strPatientName, strTmpRoomName);
            m_mthSpeak(ledMessage);
            //TTSClient.TTSClient.PlaySound(ledMessage);
            //接诊医生为非挂号医生情况的处理,strDoctID 必须为分诊台医生队列ID
            if (queueDictionary[strDoctID].IndexOf(patient) < 0)
            {
                foreach (string queueID in queueDictionary.Keys)
                {
                    if (queueDictionary[queueID].IndexOf(patient) >= 0)
                    {
                        strDoctID = queueID;
                        break;
                    }
                }
            }

            if (isPrivate)
            {
                MovePatient(patient, strDoctID, CALLEDQUEUE, 0);
            }
            else
            {
                string strQueue = GetQueueShareName(strDoctID);
                MovePatient(patient, strQueue, CALLEDQUEUE, 0);
            }
            if (PatientCountChanged != null)
            {
                PatientCountChangeEventArgs e = new PatientCountChangeEventArgs(strDoctID,true);
                PatientCountChanged(this, e);
            }
        }
        private void RecallPatient(string strDoctID, clsMFZPatientVO patient)
        {
            string ledMessage = string.Format(clsConfig.CurrentConfig.CallPatientStyle, patient.m_strPatientName.PadLeft(4, '　'), GetDoctRoomName(strDoctID));
            //TTSClient.TTSClient.PlaySound(ledMessage);
            m_mthSpeak(ledMessage);
        }

        /// <summary>
        /// 重新处理叫号信息
        /// </summary>
        /// <param name="flag">1-已定义叫号病人和诊室名称 2-未定义叫号信息</param>
        /// <param name="strDocID">flag = 2时,不能为空</param>
        /// <param name="callName">flag = 1时,不能为空</param>
        /// <param name="callRoom">任何时候不能为空</param>
        /// <returns></returns>
        private string ReGenerateCallMessage(int flag, string strDocID, string callName, string callRoom)
        {
            string strResult = string.Empty;
            string strTmpName = string.Empty; //当前病人
            string strTmpPreName = string.Empty; //准备病人

            if (flag == 2)
            {
                clsMFZPatientVO callPatient = queueDictionary[strDocID][0];

                #region 处理1
                char[] arrChrName = callPatient.m_strPatientName.ToCharArray();
                for (int i1 = 0; i1 < arrChrName.Length; i1++)
                {
                    strTmpName += arrChrName[i1].ToString() + "  ";
                }
                #endregion

                if (queueDictionary[strDocID].Count > 1)
                {
                    string preparePatient = queueDictionary[strDocID][1].m_strPatientName;

                    #region 处理2
                    char[] arrPreName = preparePatient.ToCharArray();
                    for (int i2 = 0; i2 < arrPreName.Length; i2++)
                    {
                        strTmpPreName += arrPreName[i2].ToString() + "  ";
                    }
                    #endregion

                    strResult = string.Format(clsConfig.CurrentConfig.CallPatientStyle, strTmpName, callRoom) + string.Format(clsConfig.CurrentConfig.PreparePatientStyle, strTmpPreName);
                }
                else
                {
                    strResult = string.Format(clsConfig.CurrentConfig.CallPatientStyle, strTmpName, callRoom);
                }
            }
            else
            {
                #region 处理3
                char[] arrChrName = callName.ToCharArray();
                for (int i3 = 0; i3< arrChrName.Length; i3++)
                {
                    strTmpName += arrChrName[i3].ToString() + "  ";
                }
                #endregion

                strResult = string.Format(clsConfig.CurrentConfig.CallPatientStyle, strTmpName, callRoom);
            }
            return strResult;
        }
        #endregion

        #region 添加,移动,删除患者

        public string Add(clsMFZPatientVO patient, delAddedContinue addContinueMeth)
        {
            //List<string> paitentQueue = ConstructDoctorPatientPages(clsConfig.CurrentConfig.PatientQueueLineNum, clsConfig.CurrentConfig.PatientQueueColumnNum, GetPatientQueue());
            //new clsLEDImage().GeneratorPatientQueue(paitentQueue);

            clsDept choiceDept = new clsDept();
            string msg = string.Empty;
            lock (dataModule)
            {
                bool isPatientInCurrentArea = false;
                foreach (clsDept dept in dataModule.daigArea.depts)
                {
                    if (dept.strDeptID == patient.m_strRegisterDeptID)
                    {
                        isPatientInCurrentArea = true;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(patient.m_strRegisterID))
                {
                    msg = "该患者没有挂号，是否继续添加?";
                    if (addContinueMeth(msg, true, out choiceDept))
                    {
                        patient.m_strRegisterDeptID = choiceDept.strDeptID;
                        patient.m_strRegisterDeptName = choiceDept.strDeptName;
                        AddSpecialPatient(patient, false);
                        return string.Empty;
                    }
                    else
                    {
                        return string.Empty;
                    }

                }
                if (!isPatientInCurrentArea)
                {
                    msg = "该患者挂号的部门不在当前诊区,是否继续添加?";
                    if (addContinueMeth(msg, true, out choiceDept))
                    {
                        patient.m_strRegisterDeptID = choiceDept.strDeptID;
                        patient.m_strRegisterDeptName = choiceDept.strDeptName;
                        AddSpecialPatient(patient, false);
                        return string.Empty;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }

                if (patient.m_enmDiagStatus == enmMFZ_DiagnoseStatus.Done)
                {
                    #region 结诊患者处理
                    msg = "该患者已结诊,是否继续添加?";
                    if (addContinueMeth(msg, false, out choiceDept))
                    {
                        AddSpecialPatient(patient, false);
                        return string.Empty;
                    }
                    return string.Empty;
                    #endregion
                }
                else if (patient.m_enmDiagStatus == enmMFZ_DiagnoseStatus.Taked)
                {
                    if (patient.m_strTakedDocID != null && patient.m_strTakedDocID.Trim() != string.Empty)
                    {
                        if (!queueDictionary.ContainsKey(patient.m_strTakedDocID))
                        {
                            #region 已接诊病人,但接诊医生不在当前诊区处理
                            msg = string.Format("该患者已由医生({0})接诊，但接诊医生不在当前诊区！是否继续添加?", patient.m_strTakedDocName);
                            if (addContinueMeth(msg, false, out choiceDept))
                            {
                                AddSpecialPatient(patient, true);
                                return string.Empty;
                            }
                            return string.Empty;
                            #endregion
                        }
                    }
                    else
                    {
                        #region 挂号到医生,但接诊医生不明的处理
                        msg = string.Format("该患者已接诊，但接诊医生不明！是否继续添加?");
                        if (addContinueMeth(msg, false, out choiceDept))
                        {
                            AddSpecialPatient(patient, true);
                            return string.Empty;
                        }
                        return string.Empty;
                        #endregion
                    }
                }
                else
                {
                    if (patient.m_strRegisterDocID != null && patient.m_strRegisterDocID.Trim() != string.Empty)
                    {
                        if (!queueDictionary.ContainsKey(patient.m_strRegisterDocID))
                        {
                            msg = string.Format("该患者挂号到医生({0})，但该医生不在当前诊区！是否继续添加?", patient.m_strRegisterDocName);
                            if (addContinueMeth(msg, false, out choiceDept))
                            {
                                AddSpecialPatient(patient, false);
                                return string.Empty;
                            }
                            return string.Empty;
                        }
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////
                bool called = IsCalled(patient.m_strPatientCardNO);
                int intPosition = 0;
                string strQueueID = string.Empty;

                //加入到普通共享队列
                if (patient.m_enmRegisterType == enmMFZ_RegisterType.Common
                    && patient.m_enmDiagStatus == enmMFZ_DiagnoseStatus.UnTaked
                    && (patient.m_strRegisterDocID == null || patient.m_strRegisterDocID.Trim() == string.Empty))
                {
                    if (called)
                    {
                        RemoveCalledPatient(patient);

                        dataModule.comShareWaitQueue.Insert(0, patient);
                        intPosition = 0;
                    }
                    else
                    {
                        dataModule.comShareWaitQueue.Add(patient);
                        intPosition = dataModule.comShareWaitQueue.Count - 1;
                    }
                    strQueueID = COMMONWAITQUEUE;

                    if (QueueModified != null)
                    {
                        QueueModified(this, new QueueModifiedEventArgs(strQueueID, true, patient, intPosition));
                    }
                    return string.Empty;
                }
                //加入到专家共享队列
                if (patient.m_enmRegisterType == enmMFZ_RegisterType.Expert
                    && patient.m_enmDiagStatus == enmMFZ_DiagnoseStatus.UnTaked
                    && (patient.m_strRegisterDocID == null || patient.m_strRegisterDocID.Trim() == string.Empty))
                {
                    if (called)
                    {
                        RemoveCalledPatient(patient);

                        dataModule.expShareWaitQueue.Insert(0, patient);
                        intPosition = 0;
                    }
                    else
                    {
                        dataModule.expShareWaitQueue.Add(patient);
                        intPosition = dataModule.expShareWaitQueue.Count - 1;
                    }
                    strQueueID = EXPWAITQUEUE;

                    if (QueueModified != null)
                    {
                        QueueModified(this, new QueueModifiedEventArgs(strQueueID, true, patient, intPosition));
                    }
                    return string.Empty;
                }


                //加入到医生队列
                if (patient.m_enmDiagStatus == enmMFZ_DiagnoseStatus.Taked)
                {
                    queueDictionary[patient.m_strTakedDocID].Insert(0, patient);
                    intPosition = 0;
                    strQueueID = patient.m_strTakedDocID;
                }
                else if (called)
                {
                    RemoveCalledPatient(patient);

                    queueDictionary[patient.m_strRegisterDocID].Insert(0, patient);
                    intPosition = 0;
                    strQueueID = patient.m_strRegisterDocID;
                }
                else
                {
                    msg = string.Format("该患者挂的是医生({0})的号，是否自动添加的该医生的排队列表中?", patient.m_strRegisterDocName);
                    if (addContinueMeth(msg, false, out choiceDept))
                    {
                        queueDictionary[patient.m_strRegisterDocID].Add(patient);
                        intPosition = queueDictionary[patient.m_strRegisterDocID].Count - 1;
                        strQueueID = patient.m_strRegisterDocID;
                    }
                    else
                        return string.Empty;
                }
                if (QueueModified != null)
                {
                    QueueModified(this, new QueueModifiedEventArgs(strQueueID, true, patient, intPosition));
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 添加特殊病人（已叫过，接诊，结诊等）
        /// </summary>
        /// <param name="patient">患者</param>
        /// <param name="isAddFirst">是否把患者添加到队列的首位</param>
        private void AddSpecialPatient(clsMFZPatientVO patient, bool isAddFirst)
        {
            string strQueueID = string.Empty;
            int intPosition = -1;
            switch (patient.m_enmRegisterType)
            {
                case enmMFZ_RegisterType.Common:
                    strQueueID = COMMONWAITQUEUE;
                    break;
                case enmMFZ_RegisterType.Expert:
                    strQueueID = EXPWAITQUEUE;
                    break;
                default:
                    strQueueID = COMMONWAITQUEUE;
                    break;
            }

            if (isAddFirst)
            {
                queueDictionary[strQueueID].Insert(0, patient);
                intPosition = 0;
            }
            else
            {
                queueDictionary[strQueueID].Add(patient);
                intPosition = queueDictionary[strQueueID].Count - 1;
            }

            if (QueueModified != null)
            {
                QueueModified(this, new QueueModifiedEventArgs(strQueueID, true, patient, intPosition));
            }
        }

        private void RemoveCalledPatient(clsMFZPatientVO patient)
        {
            //int delPos = dataModule.calledQueue.IndexOf(patient);
            foreach (clsMFZPatientVO temp in dataModule.calledQueue)
            {
                if (temp.m_strPatientCardNO == patient.m_strPatientCardNO)
                {
                    int delPos = dataModule.calledQueue.IndexOf(temp);
                    dataModule.calledQueue.Remove(temp);
                    if (QueueModified != null)
                    {
                        QueueModified(this, new QueueModifiedEventArgs(CALLEDQUEUE, false, temp, delPos));
                    }
                    return;
                }
            }


        }

        public void RemovePatient(clsMFZPatientVO patient, string strQueueID)
        {
            lock (dataModule)
            {
                int index = queueDictionary[strQueueID].IndexOf(patient);
                queueDictionary[strQueueID].Remove(patient);
                QueueModified(this, new QueueModifiedEventArgs(strQueueID, false, patient, index));
            }
        }

        public void MovePatient(clsMFZPatientVO dragPatient, string dragQueueID, string dropQueueID, int dropIndex)
        {
            lock (dataModule)
            {
                int delIndex = queueDictionary[dragQueueID].IndexOf(dragPatient);
                queueDictionary[dragQueueID].Remove(dragPatient);
                QueueModified(this, new QueueModifiedEventArgs(dragQueueID, false, dragPatient, delIndex));

                queueDictionary[dropQueueID].Insert(dropIndex, dragPatient);
                QueueModified(this, new QueueModifiedEventArgs(dropQueueID, true, dragPatient, dropIndex));
            }
        }

        #endregion

        #region 初始化诊区数据和清空连接

        // 初始化DataModule
        public void LoadArea(int p_intAreaID, int schemeId)
        {
            m_intSchemeId = schemeId;
            Reset();
            InitDataModuleTree(p_intAreaID, schemeId);
            LoadSerialData(schemeId);
            LoadDataToTree();
            GeneratorLEDImage();

            string strMsg;
            bool blnres = comSvc.Start(out strMsg);

            ledShow = new clsLED();
            ledShow.Start();
            //初始化电视屏
            lcdShow = new clsLCDManager(dataModule);
        }

        // 重新加载树
        private bool InitDataModuleTree(int p_intAreaID, int schemeID)
        {
            dataModule = new clsDataModule();
            queueDictionary.Clear();
            doctCoputerDictionary.Clear();


            clsMFZDiagnoseAreaVO diagArea;
            clsTmdDiagnoseAreaSmp.s_object.m_lngFind(p_intAreaID, out diagArea);

            clsMFZDeptVO[] depts = null;
            clsMFZRoomVO[] rooms = null;
            clsMFZWorkStationVO[] stations = null;
            clsMFZDoctorVO[] doctors = null;
            long lngRes = clsQueueManageSmp.s_object.m_lngFind(p_intAreaID, schemeID, out depts, out rooms, out stations, out doctors);
            if (lngRes == 0)
            {
                clsCommonDialog.m_mthShowDBError();
                return false;
            }
            if (depts == null)
            {
                depts = new clsMFZDeptVO[0];
            }
            if (rooms == null)
            {
                rooms = new clsMFZRoomVO[0];
            }
            if (stations == null)
            {
                stations = new clsMFZWorkStationVO[0];
            }
            if (doctors == null)
            {
                doctors = new clsMFZDoctorVO[0];
            }
            if (diagArea == null)
            {
                return false;
            }
            // 加载诊区信息
            dataModule.daigArea = new clsDiagArea();
            dataModule.daigArea.intDiagAreaID = diagArea.m_intDiagnoseAreaID;
            dataModule.daigArea.strDiagAreaName = diagArea.m_strDiagnoseAreaName;

            // 添加普通共享列、专家共享对列和叫过队列
            queueDictionary.Add(COMMONWAITQUEUE, dataModule.comShareWaitQueue);
            queueDictionary.Add(EXPWAITQUEUE, dataModule.expShareWaitQueue);
            queueDictionary.Add(CALLEDQUEUE, dataModule.calledQueue);

            foreach (clsMFZDeptVO dept in depts)
            {
                clsDept temp = new clsDept();
                temp.strDeptID = dept.m_strDeptID;
                temp.strDeptName = dept.m_strDeptName;
                dataModule.daigArea.depts.Add(temp);

                foreach (clsMFZRoomVO room in rooms)
                {
                    if (room.m_strDeptID == dept.m_strDeptID)
                    {
                        clsRoom tempRoom = new clsRoom();
                        tempRoom.intRoomID = room.m_intRoomID;
                        tempRoom.strRoomName = room.m_strRoomName;
                        temp.rooms.Add(tempRoom);

                        foreach (clsMFZDoctorVO doctor in doctors)
                        {
                            if (doctor.m_intRoomID == room.m_intRoomID)
                            {
                                clsDoctor tempDoctor = new clsDoctor();
                                tempDoctor.strDoctID = doctor.m_strDoctorID;
                                tempDoctor.strDoctName = doctor.m_strDoctorName;
                                tempDoctor.doctorType = doctor.m_enmDoctorType;
                                tempDoctor.strSummary = doctor.m_strSummary;
                                tempRoom.doctors.Add(tempDoctor);

                                clsMFZWorkStationVO station = null;
                                clsTmdWorkStationSmp.s_object.m_lngFindByDoctorID(tempDoctor.strDoctID, schemeID, out station);
                                queueDictionary.Add(tempDoctor.strDoctID, tempDoctor.firstQueue);
                                hasCalledPatient.Add(tempDoctor.strDoctID,0);
                                doctCoputerDictionary.Add(tempDoctor.strDoctID, station.m_strWorkStationName);
                            }
                        }
                    }
                }
            }
            return true;
        }

        // 加载本地序列化数据
        private void LoadSerialData(int schemeID)
        {
            clsDataSerial serial = new clsDataSerial(dataModule.daigArea.intDiagAreaID);
            serial.DeSerialize();

            clsMFZSchemeVO schemeSerial = GetScheme(serial.serializeTime);

            // 属于同一个班次
            if (schemeSerial != null && schemeID == schemeSerial.m_intSchemeSeq)
            {
                Dictionary<string, List<clsMFZPatientVO>> queueLocalData = serial.queueDictionary;
                hasCalledPatient = serial.HasCalledPatient;
                if (queueLocalData != null)
                {
                    Dictionary<string, List<clsMFZPatientVO>> errorDictionary = new Dictionary<string, List<clsMFZPatientVO>>();
                    foreach (string queueLocalDataID in queueLocalData.Keys)
                    {
                        bool isEqual = false;
                        foreach (string queueID in queueDictionary.Keys)
                        {
                            if (queueID == queueLocalDataID)
                            {
                                queueDictionary[queueID] = queueLocalData[queueLocalDataID];
                                isEqual = true;
                                break;
                            }
                        }

                        // 医生已经不存在了
                        if (isEqual == false)
                        {
                            string msg = string.Empty;
                            foreach (clsMFZPatientVO patient in queueLocalData[queueLocalDataID])
                            {
                                AddSpecialPatient(patient, true);
                            }
                        }
                    }
                }
            }
            else
            {
                //clsDataStatistics stat = new clsDataStatistics(serial);
                //if (!stat.Save())
                //{
                //    new com.digitalwave.Utility.clsLogText().LogError("门诊医生叫号信息保存到数据库失败！");
                //}
                /*
                    如果上一个班次的信息，0
                 */
            }
        }

        // 把数据加载到DataModule树上
        private void LoadDataToTree()
        {
            dataModule.comShareWaitQueue = queueDictionary[COMMONWAITQUEUE];
            dataModule.expShareWaitQueue = queueDictionary[EXPWAITQUEUE];
            dataModule.calledQueue = queueDictionary[CALLEDQUEUE];

            foreach (clsDept dept in dataModule.daigArea.depts)
            {
                foreach (clsRoom room in dept.rooms)
                {
                    foreach (clsDoctor doctor in room.doctors)
                    {
                        foreach (string docQueueID in queueDictionary.Keys)
                        {
                            if (doctor.strDoctID == docQueueID)
                            {
                                doctor.firstQueue = queueDictionary[docQueueID];
                                if (hasCalledPatient.Contains(docQueueID))
                                {
                                    doctor.calledCount = (int)hasCalledPatient[docQueueID];
                                }
                            }
                        }
                    }
                }

            }
        }

        public void Reset()
        {
            comSvc.Stop();
            if (ledShow != null)
            {
                ledShow.Stop();
                ledShow = null;
            }
        }

        #endregion

        #region 传上班次叫号信息到数据库以进行统计

        #endregion

        #region 数据的序列化

        public clsDataModule GetDataModule()
        {
            return dataModule;
        }

        private void SerializeData()
        {
            clsDataSerial serial = new clsDataSerial(dataModule.daigArea.intDiagAreaID, queueDictionary, hasCalledPatient);
            serial.SchemeId = this.m_intSchemeId;
            serial.SerializeDataToLocal();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 患者是否被叫过
        /// </summary>
        /// <param name="strPatientCardID">卡号</param>
        /// <returns>True/False</returns>
        private bool IsCalled(string strPatientCardID)
        {
            foreach (clsMFZPatientVO patient in dataModule.calledQueue)
            {
                if (patient.m_strPatientCardNO == strPatientCardID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 患者是否添加过
        /// </summary>
        /// <param name="strPatientCardID">卡号</param>
        /// <returns>True/False</returns>
        public bool IsAdded(string strPatientCardID)
        {
            foreach (string queueID in queueDictionary.Keys)
            {
                if (queueID != CALLEDQUEUE)
                {
                    foreach (clsMFZPatientVO patient in queueDictionary[queueID])
                    {
                        if (patient.m_strPatientCardNO == strPatientCardID)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 患者是否添加过
        /// </summary>
        /// <param name="strPatientCardID">卡号</param>
        /// <param name="clsMFZPatientVO">病人</param>
        /// <returns>True/False</returns>
        public bool IsAdded(string strPatientCardID, ref clsMFZPatientVO objpatient)
        {
            foreach (string queueID in queueDictionary.Keys)
            {
                if (queueID != CALLEDQUEUE)
                {
                    foreach (clsMFZPatientVO patient in queueDictionary[queueID])
                    {
                        if (patient.m_strPatientCardNO == strPatientCardID)
                        {
                            objpatient = patient;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 工作站名称获取所属的医生ID
        /// </summary>
        /// <param name="stationIP">Ip地址</param>
        /// <returns>医生Id</returns>
        private string GetDoctID(string stationIP)
        {
            lock (((ICollection)doctCoputerDictionary).SyncRoot)
            {
                if (doctCoputerDictionary.ContainsValue(stationIP))
                {
                    foreach (KeyValuePair<string, string> keyValuePair in doctCoputerDictionary)
                    {
                        if (keyValuePair.Value.ToString() == stationIP)
                        {
                            return keyValuePair.Key.ToString();
                        }
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 当前医生ID获取共享队列的QueueID
        /// </summary>
        /// <param name="doctID"></param>
        /// <returns></returns>
        private string GetQueueShareName(string doctID)
        {
            foreach (clsDept dept in dataModule.daigArea.depts)
            {
                foreach (clsRoom room in dept.rooms)
                {
                    foreach (clsDoctor doct in room.doctors)
                    {
                        if (doct.strDoctID == doctID)
                        {
                            if (doct.doctorType == enmMFZDoctorType.Common)
                            {
                                return COMMONWAITQUEUE;
                            }
                            if (doct.doctorType == enmMFZDoctorType.Expert)
                            {
                                return EXPWAITQUEUE;
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 医生ID获取诊室名称
        /// </summary>
        /// <param name="doctID"></param>
        /// <returns></returns>
        private string GetDoctRoomName(string doctID)
        {
            foreach (clsDept dept in dataModule.daigArea.depts)
            {
                foreach (clsRoom room in dept.rooms)
                {
                    foreach (clsDoctor doct in room.doctors)
                    {
                        if (doct.strDoctID == doctID)
                        {
                            return room.strRoomName;
                        }
                    }
                }
            }
            return string.Empty;
        }

        private string GetDoctorName(string doctorId) 
        {
            foreach (clsDept dept in dataModule.daigArea.depts)
            {
                foreach (clsRoom room in dept.rooms)
                {
                    foreach (clsDoctor doct in room.doctors)
                    {
                        if (doct.strDoctID == doctorId)
                        {
                            return doct.strDoctName;
                        }
                    }
                }
            }
            return string.Empty;
        }

        private clsDoctor GetDoctor(string doctorId)
        {
            foreach (clsDept dept in dataModule.daigArea.depts)
            {
                foreach (clsRoom room in dept.rooms)
                {
                    foreach (clsDoctor doct in room.doctors)
                    {
                        if (doct.strDoctID == doctorId)
                        {
                            return doct;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 医生ID获取诊区(普通/专家)共享队列中科室对列
        /// </summary>
        /// <param name="doctID"></param>
        /// <returns></returns>
        private List<clsMFZPatientVO> GetDoctShareQueue(string doctID)
        {
            List<clsMFZPatientVO> lstDeptPatients = new List<clsMFZPatientVO>();
            string shareQueueID = GetQueueShareName(doctID);
            string doctDeptID = GetDoctDeptID(doctID);
            foreach (clsMFZPatientVO patient in queueDictionary[shareQueueID])
            {
                if (patient.m_strRegisterDeptID == doctDeptID)
                {
                    lstDeptPatients.Add(patient);
                }
            }
            return lstDeptPatients;
        }

        /// <summary>
        /// 医生ID获取所在部门ID
        /// </summary>
        /// <param name="doctID">医生Id</param>
        /// <returns>部门Id</returns>
        private string GetDoctDeptID(string doctID)
        {
            foreach (clsDept dept in dataModule.daigArea.depts)
            {
                foreach (clsRoom room in dept.rooms)
                {
                    foreach (clsDoctor doct in room.doctors)
                    {
                        if (doct.strDoctID == doctID)
                        {
                            return dept.strDeptID;
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取显示屏的显示内容
        /// </summary>
        /// <returns></returns>
        private string GetLEDText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("普通列 ");
            sb.Append("专家列 ");
            sb.AppendLine();
            for (int i = 0; i < 5; i++)
            {
                if (i < this.queueDictionary[COMMONWAITQUEUE].Count)
                {
                    string patientName = queueDictionary[COMMONWAITQUEUE][i].m_strPatientName;
                    if (patientName.Length == 2)
                    {
                        char[] arrNames = patientName.ToCharArray();
                        patientName = arrNames[0] + "  " + arrNames[1];
                    }
                    sb.Append(patientName + " ");
                }
                else
                {
                    sb.Append("       ");
                }

                if (i < queueDictionary[EXPWAITQUEUE].Count)
                {
                    string patientName = queueDictionary[EXPWAITQUEUE][i].m_strPatientName;
                    if (patientName.Length == 2)
                    {
                        char[] arrNames = patientName.ToCharArray();
                        patientName = arrNames[0] + "  " + arrNames[1];
                    }
                    sb.Append(patientName + " ");
                }
                else
                {
                    sb.Append("       ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        /// <summary>
        /// 根据时间获取属于那个班次
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public clsMFZSchemeVO GetScheme(DateTime dt)
        {
            clsMFZSchemeVO[] schemes;
            clsTmdSchemeSmp.s_object.m_lngFind(out schemes);
            foreach (clsMFZSchemeVO scheme in schemes)
            {
                if (scheme.m_intWeekDay == GetWeekDay(dt.DayOfWeek) && dt > scheme.m_dtBegin && dt < scheme.m_dtEnd)
                {
                    return scheme;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取当前是星期几(1-7)
        /// </summary>
        /// <param name="weekDay">DayOfWeek枚举</param>
        /// <returns>星期几</returns>
        private int GetWeekDay(DayOfWeek weekDay)
        {
            int result = 0;
            switch (weekDay)
            {
                case DayOfWeek.Monday:
                    result = 1;
                    break;
                case DayOfWeek.Tuesday:
                    result = 2;
                    break;
                case DayOfWeek.Wednesday:
                    result = 3;
                    break;
                case DayOfWeek.Thursday:
                    result = 4;
                    break;
                case DayOfWeek.Friday:
                    result = 5;
                    break;
                case DayOfWeek.Saturday:
                    result = 6;
                    break;
                case DayOfWeek.Sunday:
                    result = 7;
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// 发送信息给客户端
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="var"></param>
        /// <returns></returns>
        private bool SendToClient(string clientName, clsMFZCommunicationVO var)
        {
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            formatter.Serialize(ms, var);
            byte[] byts = ms.ToArray();

            string msg;
            bool res = this.comSvc.Send(clientName, byts, out msg);
            if (!res)
            {
                new com.digitalwave.Utility.clsLogText().LogError(msg);
            }
            return res;
        }

        /// <summary>
        /// 发送最新状态给客户端
        /// </summary>
        /// <returns></returns>
        private bool NotifyClientQueueStatus()
        {
            clsMFZCommunicationVO resObj = new clsMFZCommunicationVO();
            foreach (string doctID in doctCoputerDictionary.Keys)
            {
                clsQueueStatus[] queueStatusArr = new clsQueueStatus[2];

                clsQueueStatus queuePrivate = new clsQueueStatus();
                queuePrivate.queueType = enmMFZQueueType.PrivateWait;
                queuePrivate.count = queueDictionary[doctID].Count;
                queueStatusArr[0] = queuePrivate;

                string strQueueName = GetQueueShareName(doctID);

                clsQueueStatus queueShare = new clsQueueStatus();
                queueShare.queueType = enmMFZQueueType.ShareWait;
                queueShare.count = GetDoctShareQueue(doctID).Count;
                queueStatusArr[1] = queueShare;

                resObj.option = enmMFZCommunicationOption.SetQueueStatus;
                resObj.strSouce = string.Empty;
                resObj.data = queueStatusArr;

                SendToClient(doctCoputerDictionary[doctID], resObj);
            }
            return true;
        }

        #endregion

        #region 生成医生安排表图片

        private string TidyDoctorName(string doctorName)
        {
            if (doctorName.Length == 2)
            {
                return doctorName[0].ToString().PadRight(2, '　') + doctorName[1];
            }
            return doctorName;
        }

        private List<string> ConstructDeptArrange(int rowNum,int colNum, List<string> lstRoomDoctor)
        {
            List<string> lstLeds = new List<string>();
            StringBuilder sb = new StringBuilder();
            int screenNum =(int)Math.Ceiling((double)((lstRoomDoctor.Count+2) * 1.0 / (rowNum * 2.0)));
            int startPos = 0;

            for (int i = 0; i < screenNum; i++)
            {

                startPos = i == 0 ? 0 : colNum *rowNum* i - 2;
                if (i == 0)
                {
                    string head = clsConfig.CurrentConfig.ArrangeTitle + "\n";
                    lstLeds.Add(head+GetPageString(startPos, rowNum - 1, colNum, lstRoomDoctor));
                }
                else
                {
                    lstLeds.Add(GetPageString(startPos, rowNum, colNum, lstRoomDoctor));
                }
            }
            return lstLeds;

            #region foreach
            //sb.AppendLine(clsConfig.CurrentConfig.ArrangeTitle);
            //for (int pageNum = 0; pageNum < screenNum; pageNum++)
            //{
            //    if (pageNum>0)
            //    {
            //        startPos = (2 * rowNum) * pageNum;
            //    }

            //    if (pageNum == screenNum - 1)
            //    {
            //        for (int i = 0; i < rowNum; i++)
            //        {
            //            for (int j = 0; j < 2; j++)
            //            {
            //                int pos = startPos+j * rowNum + i;
            //                if (pos < lstRoomDoctor.Count)
            //                {
            //                    if (j==1)
            //                    {
            //                        sb.AppendLine("  " + lstRoomDoctor[pos]) ;
            //                    }
            //                    else
            //                    {
            //                        sb.Append(lstRoomDoctor[pos]);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        sb.Remove(0, sb.Length);
            //        for (int i = 0; i < rowNum; i++)
            //        {
            //            sb.AppendLine(string.Format("{0}   {1}", lstRoomDoctor[i + startPos], lstRoomDoctor[rowNum + i + startPos]));
            //        }
            //    }
            //    lstLeds.Add(sb.ToString());
            //}
            //return lstLeds;
           
            ////rowCount++;
            
            
            //foreach (KeyValuePair<string, List<clsDoctor>> pair in dicDeptDoctors)
            //{
            //    if (pair.Value.Count == 0) continue;

            //    sb.Append(pair.Key);
            //    //sb.Append(pair.Key);
            //    //sb.Append(Convert.ToChar(Keys.Space));
            //    int len = (int)Math.Ceiling((Double)((pair.Value.Count * 1.0) / colNum * 1.0));
            //    for (int i = 0; i < len; i++)
            //    {
            //        for (int j = 0; j < colNum; j++)
            //        {
            //            if (i * colNum + j < pair.Value.Count)
            //            {
            //                //append 医生名字
            //                clsDoctor doct = pair.Value[i * colNum + j];
            //                string strRoomName = GetDoctRoomName(doct.strDoctID).PadLeft(2,'0');
            //                if (!strRoomName.Contains("室"))
            //                {
            //                    strRoomName += "室";
            //                }
            //                string strDoctRoomName = TidyDoctorName(doct.strDoctName).Trim() + "(" + strRoomName + ")";
            //                sb.Append(strDoctRoomName.PadRight(9, '　'));
            //                //sb.Append(strDoctRoomName);
            //                //sb.Append(Convert.ToChar(Keys.Tab));
            //            }
            //        }

            //        if (i != len - 1)
            //        {
            //            //同科室
            //            sb.AppendLine();

            //            rowCount++;
            //            if (rowCount == rowNum)
            //            {
            //                string str = sb.ToString();
            //                sb.Remove(0, sb.Length);
            //                rowCount = 0;
            //                lstLeds.Add(str);
            //            }
            //            sb.Append(string.Empty.PadRight(11, ' '));

            //        }
            //        else
            //        {
            //            sb.AppendLine();

            //            rowCount++;
            //            if (rowCount == rowNum)
            //            {
            //                string str = sb.ToString();
            //                sb.Remove(0, sb.Length);
            //                rowCount = 0;
            //                lstLeds.Add(str);
            //            }
            //        }

                   
            //    }
            //} 
            #endregion

            //if (num<rowNum&&num!=0)
            //{
            //    lstLeds.Add(sb.ToString());
            //}

           
        }

        private string GetPageString(int startPos,int rowNum,int colNum,List<string> lstRoomDoctor)
        {
            StringBuilder sb = new StringBuilder();
            int pos = startPos;
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    
                        pos = startPos + rowNum * j + i;
                        if (pos < lstRoomDoctor.Count)
                        {
                            sb.Append(lstRoomDoctor[pos]);
                            sb.Append("  ");
                        }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private List<string> GetDeptList()
        {
            List<string> lstRoomDoctor = new List<string>();
            string strRowContent = string.Empty;

            foreach (clsDept dept in dataModule.daigArea.depts)
            {
                foreach (clsRoom room in dept.rooms)
                {
                    if (room.doctors.Count == 1)
                    {
                        clsDoctor doctor = GetDoctor(room.doctors[0].strDoctID);
                        string projectName = string.Empty;
                        if (doctor != null)
                        {
                            clsDoctorDiscrible doctorStyle = new clsDoctorDiscrible(doctor.strSummary);

                            if (doctor.doctorType == enmMFZDoctorType.Common)
                            {

                                projectName = doctorStyle.GetDescrible("普通");
                            }
                            if (doctor.doctorType == enmMFZDoctorType.Expert)
                            {
                                projectName = doctorStyle.GetDescrible("专家");
                            }
                        }

                        // 医技诊区显示样式
                        if (dataModule.daigArea.intDiagAreaID == 9)
                        {
                            if (projectName == string.Empty)
                            {
                                strRowContent = string.Format("{0}:{1} ", room.strRoomName.PadRight(5, '　'), doctor.strDoctName.PadRight(5, '　'));
                            }
                            else 
                            {
                                strRowContent = string.Format("{0}:{1}-{2}", room.strRoomName.PadRight(5, '　'), projectName, doctor.strDoctName);
                            }
                            lstRoomDoctor.Add(strRowContent.PadRight(15, '　'));
                        }
                        else 
                        {
                            if (projectName == string.Empty)
                            {
                                strRowContent = string.Format("{0}:{1} ", room.strRoomName,doctor.strDoctName.PadLeft(5, '　'));
                            }
                            else
                            {
                                strRowContent = string.Format("{0}:{1}-{2}", room.strRoomName, projectName.PadLeft(5, '　'), doctor.strDoctName);
                            }

                            lstRoomDoctor.Add(strRowContent.PadRight(15, '　'));
                        }
                     
                    }
                    else
                    {
                        // 医技诊区显示样式
                        if (dataModule.daigArea.intDiagAreaID == 9)
                        {
                            strRowContent = string.Format("{0}:{2} {1}", room.strRoomName.PadRight(5, '　'), "　　　", "　　　　　");
                            lstRoomDoctor.Add(strRowContent.PadRight(15, '　'));
                        }
                        else 
                        {
                            strRowContent = string.Format("{0}:{2} {1}", room.strRoomName, "　　　", "　　　　　");
                            lstRoomDoctor.Add(strRowContent.PadRight(15, '　'));
                        }
                    }
                }
            }

            lstRoomDoctor.Sort(delegate(string x, string y) { return x.CompareTo(y); });
            return lstRoomDoctor;

           
            /*foreach (clsDept dept in dataModule.daigArea.depts)
            {
                List<clsDoctor> lstDept = new List<clsDoctor>();
                foreach (clsRoom room in dept.rooms)
                {
                    foreach (clsDoctor doct in room.doctors)
                    {
                        lstDept.Add(doct);
                    }
                }

                string strPrev = (dept.strDeptName.Split('('))[0];

                if (strPrev.Length == 2)
                {
                    strPrev = dept.strDeptName[0].ToString().PadLeft(2, '　').PadRight(3, '　') + dept.strDeptName[1].ToString();
                }
                if (strPrev.Length == 3)
                {
                    strPrev = strPrev.PadLeft(4, '　');
                }
                if (strPrev.Length >= 5)
                {
                    strPrev = strPrev.Substring(0, 4);
                }
                strPrev = strPrev.PadLeft(4, '　').PadRight(5, '　') + ":";
                dic.Add(strPrev, lstDept);
            }*/
        }

        /// <summary>
        /// 生成LED屏的医生排班列表
        /// </summary>
        private void GeneratorLEDImage() 
        {
            //生成医生值班安排图片
            List<string> deptDoctors = ConstructDeptArrange(clsConfig.CurrentConfig.ArrangeLineNum, clsConfig.CurrentConfig.ArrangeColumnNum, GetDeptList());
            new clsLEDImage().GeneratorDeptArrange(deptDoctors);
        }
        #endregion

        #region 生成挂号到医生的患者等待队列

        private Dictionary<string, List<string>> GetPatientQueue()
        {
            Dictionary<string, List<string>> lstResult = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, List<clsMFZPatientVO>> pairPatient in queueDictionary)
            {
                bool isDoctorQueue = !(pairPatient.Key == COMMONWAITQUEUE || pairPatient.Key == EXPWAITQUEUE
                                   || pairPatient.Key == CALLEDQUEUE);
                string doctorId=pairPatient.Key;
                if (pairPatient.Value.Count>0)
                {
                    if (isDoctorQueue)
                    {
                        List<string> lstPatientName = new List<string>();
                        foreach (clsMFZPatientVO patient in pairPatient.Value)
                        {
                            lstPatientName.Add(patient.m_strPatientName);
                        }
                        string headName = string.Format("{0}({1})", TidyDoctorName(GetDoctorName(doctorId)),GetDoctRoomName(doctorId));
                        lstResult.Add(headName.PadRight(8,'　')+":", lstPatientName);
                    }
                }
            }

            return lstResult;
        }

        private List<string> ConstructDoctorPatientPages(int rowNum, int colNum, Dictionary<string, List<string>> dicDoctorPatient)
        {
            List<string> lstLeds = new List<string>();
            StringBuilder sb = new StringBuilder();
            int rowCount = 0;
            sb.AppendLine(string.Format("              {0}门诊就诊队列列表", dataModule.daigArea.strDiagAreaName));
            rowCount++;

            #region foreach
            foreach (KeyValuePair<string, List<string>> pair in dicDoctorPatient)
            {
                if (pair.Value.Count == 0) continue;

                sb.Append(pair.Key);
                //sb.Append(pair.Key);
                //sb.Append(Convert.ToChar(Keys.Space));
                int len = (int)Math.Ceiling((Double)((pair.Value.Count * 1.0) / colNum * 1.0));
                if (len > 2) { len = 2; }

                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < colNum; j++)
                    {
                        if (i * colNum + j < pair.Value.Count)
                        {
                            //append 医生名字
                            string PatientName = pair.Value[i * colNum + j];
                            sb.Append(PatientName.PadRight(4, '　'));
                        }
                    }

                    if (i != len - 1)
                    {
                        sb.AppendLine();

                        rowCount++;
                        if (rowCount == rowNum)
                        {
                            string str = sb.ToString();
                            sb.Remove(0, sb.Length);
                            rowCount = 0;

                            lstLeds.Add(str);
                        }
                        sb.Append(string.Empty.PadRight(14, ' '));

                    }
                    else
                    {
                        sb.AppendLine();

                        rowCount++;
                        if (rowCount == rowNum)
                        {
                            string str = sb.ToString();
                            sb.Remove(0, sb.Length);
                            rowCount = 0;
                            lstLeds.Add(str);
                        }
                    }


                }
            }
            #endregion

            if (rowCount < rowNum && rowCount != 0)
            {
                lstLeds.Add(sb.ToString());
            }

            return lstLeds;
        }

        #endregion

        #region 消息队列模式代码
        /// <summary>
        /// 初始化消息队列
        /// </summary>
        /// <param name="m_blnFlag">false-关闭 true-打开</param>
        public void m_mthInitalMSMQ(bool m_blnFlag)
        {
            if (m_blnFlag)
            {
                MessageQueue mq = null;
                if (!MessageQueue.Exists(".\\Private$\\iCareOPWaitList"))
                {
                    MessageQueue.Create(".\\Private$\\iCareOPWaitList");
                }
                mq = new MessageQueue(".\\Private$\\iCareOPWaitList");
                mq.ReceiveCompleted += new ReceiveCompletedEventHandler(MessageQueue_ReceiveCompleted);
                mq.BeginReceive();
            }
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = null;
            try
            {
                mq = (MessageQueue)sender;
                System.Messaging.Message msg = mq.EndReceive(asyncResult.AsyncResult);
                msg.Formatter = new BinaryMessageFormatter();
                clsMFZCommunicationVO resObj = (clsMFZCommunicationVO)msg.Body;

                if (resObj != null)
                {
                    lock (dataModule)
                    {
                        if (doctCoputerDictionary.ContainsValue(resObj.strSouce))
                        {
                            string strDocID = GetDoctID(resObj.strSouce); // 根据终端Ip攻取医生ID
                            string strQueueName = GetQueueShareName(strDocID); //获取医生队列类型
                            if (strDocID != string.Empty)
                            {
                                switch (resObj.option)
                                {
                                    case enmMFZCommunicationOption.CallSomePatient:
                                        m_mthClientCallSomePatient(strDocID, strQueueName, resObj);
                                        break;
                                    default:
                                        break;
                                };
                            }
                        }
                    }
                }                
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(objEx);
            }
            mq.BeginReceive();
        }
        /// <summary>
        /// 呼叫病人就诊
        /// </summary>
        /// <param name="strDoctID"></param>
        /// <param name="strQueueName"></param>
        /// <param name="comObj"></param>
        private void m_mthClientCallSomePatient(string strDoctID, string strQueueName, clsMFZCommunicationVO comObj)
        {
            clsMFZPatientQueueType comPatient = comObj.data as clsMFZPatientQueueType;
            clsMFZPatientVO patient = null;
            if (IsAdded(comPatient.patient.m_strPatientCardNO, ref patient))
            {
                CallSomePatient(strDoctID, patient, true);
            }
        }
        #endregion

        #region 电视屏新增内容
        /// <summary>
        /// 刷新LCD屏幕
        /// </summary>
        /// <param name="e"></param>
        public void RefreshLCD(QueueModifiedEventArgs e)
        {
            lcdShow.m_mthRefreshLCD(e.QueueID, e.Patient);
        }

        #region 获取用于显示屏的队列数据
        /// <summary>
        /// 获取用于显示屏的队列数据(停用)
        /// </summary>
        /// <param name="p_strDocID"></param>
        /// <param name="p_lstPatient"></param>
        /// <returns></returns>
        private clsLEDShowVO m_objGetLedShowValue(string p_strDocID, List<clsMFZPatientVO> p_lstPatient)
        {
            clsLEDShowVO objLedShowVO = new clsLEDShowVO();
            clsDoctor objdoctor = GetDoctor(p_strDocID);
            objLedShowVO.m_strDoctorID = p_strDocID;
            objLedShowVO.m_strRoomName = GetDoctRoomName(p_strDocID);
            objLedShowVO.m_strDoctorName = objdoctor.strDoctName;
            objLedShowVO.m_enmDoctorType = objdoctor.doctorType;
            objLedShowVO.m_lstPatient = p_lstPatient;
            return objLedShowVO;
        }

        /// <summary>
        /// 呼叫特定病人的显示数据(停用)
        /// </summary>
        /// <param name="p_strDocID"></param>
        /// <param name="patient"></param>
        /// <param name="p_lstPatient"></param>
        /// <returns></returns>
        private clsLEDShowVO m_objGetLedShowValue(string p_strDocID, clsMFZPatientVO patient, List<clsMFZPatientVO> p_lstPatient)
        {
            clsLEDShowVO objLedShowVO = new clsLEDShowVO();
            clsDoctor objdoctor = GetDoctor(p_strDocID);
            objLedShowVO.m_strDoctorID = p_strDocID;
            objLedShowVO.m_strRoomName = GetDoctRoomName(p_strDocID);
            objLedShowVO.m_strDoctorName = objdoctor.strDoctName;
            objLedShowVO.m_enmDoctorType = objdoctor.doctorType;
            // 构造新队列
            objLedShowVO.m_lstPatient = new List<clsMFZPatientVO>();
            objLedShowVO.m_lstPatient.Add(patient); //优先
            foreach (clsMFZPatientVO pat in p_lstPatient)
            {
                if (pat.m_strPatientCardNO != patient.m_strPatientCardNO)
                {
                    objLedShowVO.m_lstPatient.Add(pat);
                }
            }

            return objLedShowVO;
        }
        #endregion

        #endregion
    } 

    #endregion

    #region 事件和事件数据定义

    public delegate bool delAddedContinue(string msg, bool isNeedChoiceDept, out clsDept dept);
    public delegate void ClientConnectedEventHandler(object sender, ClientConnectedEventArgs e);
    public delegate void QueueModifiedEventHandler(object sender, QueueModifiedEventArgs e);
    public delegate void PatientCountChangeEventHandler(object sender, PatientCountChangeEventArgs e);

    public class ClientConnectedEventArgs : System.EventArgs
    {
        string doctorID;
        public string DoctorID
        {
            get
            {
                return doctorID;
            }
        }
        public ClientConnectedEventArgs(string doctorid)
        {
            doctorID = doctorid;
        }
    }

    public class QueueModifiedEventArgs : System.EventArgs
    {
        string strQueueID;
        bool isAdded;
        clsMFZPatientVO patient;
        int currPosition;
        public string QueueID
        {
            get
            {
                return strQueueID;
            }
        }
        public bool IsAdded
        {
            get
            {
                return isAdded;
            }
        }
        public clsMFZPatientVO Patient
        {
            get
            {
                return patient;
            }
        }
        public int CurrentPosition
        {
            get
            {
                return currPosition;
            }
        }
        public QueueModifiedEventArgs(string p_strQueueID, bool p_isAdded, clsMFZPatientVO p_patient, int p_currPosition)
        {
            strQueueID = p_strQueueID;
            isAdded = p_isAdded;
            patient = p_patient;
            currPosition = p_currPosition;
        }
    }
 
    public class PatientCountChangeEventArgs : System.EventArgs 
    {
        string strDoctId;
        int calledPatientCount;
        bool isAdded;

        public bool IsAdded 
        {
            get { return isAdded; }
        }

        public int CalledPatientCount 
        {
            get { return calledPatientCount; }
            set { calledPatientCount = value; }
        }
        public string DoctId 
        {
            get { return strDoctId; }
        }
        public PatientCountChangeEventArgs(string strDoctId,bool isAdded)
        {
            this.strDoctId = strDoctId;
            this.isAdded = isAdded;
        }
    }

    #endregion

    #region 结构树类

    public class clsDataModule
    {
        public clsDiagArea daigArea;
        public List<clsMFZPatientVO> comShareWaitQueue = new List<clsMFZPatientVO>();
        public List<clsMFZPatientVO> expShareWaitQueue = new List<clsMFZPatientVO>();
        public List<clsMFZPatientVO> calledQueue = new List<clsMFZPatientVO>();
    }
    public class clsDiagArea
    {
        public int intDiagAreaID;
        public string strDiagAreaName;
        public List<clsDept> depts = new List<clsDept>();
    }
    public class clsDept
    {
        public string strDeptID;
        public string strDeptName;
        public List<clsRoom> rooms = new List<clsRoom>();
        public override string ToString()
        {
            return strDeptName;
        }
    }
    public class clsRoom
    {
        public int intRoomID;
        public string strRoomName;
        public List<clsDoctor> doctors = new List<clsDoctor>();
    }
    public class clsDoctor
    {
        public string strDoctID;
        public string strDoctName;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string strSummary; 
        public int calledCount;
        public weCare.Core.Entity.enmMFZDoctorType doctorType;

        public List<clsMFZPatientVO> firstQueue = new List<clsMFZPatientVO>();
    } 

    #endregion
}
