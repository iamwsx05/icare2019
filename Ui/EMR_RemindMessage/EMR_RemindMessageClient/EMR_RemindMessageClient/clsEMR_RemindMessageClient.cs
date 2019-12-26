using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using com.digitalwave.Utility;
using System.Text.RegularExpressions; 
using System.Threading;
using weCare.Core.Entity;
using System.Xml;
using com.digitalwave.Emr.StaticObject;

namespace com.digitalwave.iCare.RemindMessage
{
    /// <summary>
    /// ���յ�������Ϣʱ���ƴ���򿪲���
    /// </summary>
    public delegate void ShowRemindFormHandler(enmMessageItemType enmItemType, string message);

    /// <summary>
    /// �����¼��ͻ���
    /// </summary>
    public class clsEMR_RemindMessageClient
    {
        private clsEventWrapper wrapper = null;
        private IBroadCast watch = null;

        /// <summary>
        /// ��¼�û���ע����Ϣ
        /// </summary>
        private string m_strEventInfo = string.Empty;
        /// <summary>
        /// �Ƿ�������������ͨ��
        /// </summary>
        private bool IsReady = false;

        private static clsEMR_RemindMessageClient instance = null;

        /// <summary>
        /// �����¼��ͻ���
        /// </summary>
        protected clsEMR_RemindMessageClient()
        {
        }

        /// <summary>
        /// ���յ�������Ϣʱ�������������ò������Ѵ��塣
        /// </summary>
        public event ShowRemindFormHandler RemindFormShow;

        /// <summary>
        /// ���ɿͻ���ʵ��
        /// </summary>
        /// <returns></returns>
        public static clsEMR_RemindMessageClient Instance()
        {
            if(instance == null)
            {
                instance = new clsEMR_RemindMessageClient();
            }
            return instance;
        }

        /// <summary>
        /// ��ʼ�����¼�
        /// </summary>
        public void m_mthSubscibeStart()
        {
            try
            {
                m_mthSetLoginEMPEventInfo();

                string strRegIP = @"(\d+)\.(\d+)\.(\d+)\.(\d+)";
                string strServiceIP = m_strGetIP().Trim();
                if (!Regex.IsMatch(strServiceIP, strRegIP))
                {
                    IsReady = false;
                    return;
                }
                else
                {
                    IsReady = true;
                }

                BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
                BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
                serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

                IDictionary props = new Hashtable();
                props["port"] = 0;
                TcpChannel channel = new TcpChannel(props, clientProvider, serverProvider);
                ChannelServices.RegisterChannel(channel, false);

                watch = (IBroadCast)Activator.GetObject(
                    typeof(IBroadCast), "tcp://" + strServiceIP + ":4747/BroadCastMessage.soap");

                if (watch == null)
                {
                    IsReady = false;
                    return;
                }
                else
                {
                    IsReady = true;
                }

                wrapper = new clsEventWrapper();
                wrapper.LocalBroadCastEvent += new BroadCastEventHandler(BroadCastingMessage);
                watch.SubscribeEvent(m_strEventInfo, wrapper.BroadCasting);
            }
            catch(Exception ex)
            {
                IsReady = false;
                string strError = ex.Message;
            }
        }

        /// <summary>
        /// ���õ�ǰ��¼�û���ע����Ϣ
        /// </summary>
        private void m_mthSetLoginEMPEventInfo()
        {
            if (clsEMR_StaticObject.s_ObjCurrentEmployee == null)
            {
                return;
            }

            XmlDocument doc = new XmlDocument();

            XmlElement elEMPRoot = doc.CreateElement("EMPLOYEE");
            elEMPRoot.SetAttribute("ID", clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR);
            doc.AppendChild(elEMPRoot);

            // ����סԺ����
            if (clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr == null || clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr.Length <= 0)
            {
                XmlElement elDEPT = doc.CreateElement("DEPT");
                elDEPT.SetAttribute("ID", "0");
                elEMPRoot.AppendChild(elDEPT);
            }
            else
            {
                for (int i1 = 0; i1 < clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr.Length; i1++)
                {
                    XmlElement elDEPT = doc.CreateElement("DEPT");
                    elDEPT.SetAttribute("ID", clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr[i1].m_strDEPTID_CHR);
                    elEMPRoot.AppendChild(elDEPT);
                }
            }
            // �����������
            if (clsEMR_StaticObject.s_ObjEmpDeptAndAreaHisArr != null && clsEMR_StaticObject.s_ObjEmpDeptAndAreaHisArr.Length > 0)
            {
                for (int i1 = 0; i1 < clsEMR_StaticObject.s_ObjEmpDeptAndAreaHisArr.Length; i1++)
                {
                    XmlElement elDEPT = doc.CreateElement("DEPT");
                    elDEPT.SetAttribute("ID", clsEMR_StaticObject.s_ObjEmpDeptAndAreaHisArr[i1].m_strDEPTID_CHR);
                    elEMPRoot.AppendChild(elDEPT);
                }
            }

            //��ʱ������ID����ID��ͳһ��DEPT
            XmlElement elAREA = doc.CreateElement("AREA");
            elAREA.SetAttribute("ID", "0");
            elEMPRoot.AppendChild(elAREA);

            if ( clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleIDArr == null ||  clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleIDArr.Length <= 0)
            {
                XmlElement elROLE = doc.CreateElement("ROLE");
                elROLE.SetAttribute("ID", "0");
                elEMPRoot.AppendChild(elROLE);
            }
            else
            {
                for (int i = 0; i < clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleIDArr.Length; i++)
                {
                    XmlElement elROLE = doc.CreateElement("ROLE");
                    elROLE.SetAttribute("ID", clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleIDArr[i]);
                    elEMPRoot.AppendChild(elROLE);
                }
            }

            m_strEventInfo = doc.OuterXml;
        }

        /// <summary>
        /// ȡ�������¼�
        /// </summary>
        public void m_mthSubscibeCancle()
        {
            if (!IsReady)
                return;
            try
            {
                watch.UnSubscribeEvent(m_strEventInfo, wrapper.BroadCasting);
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
            }
        }

        private void BroadCastingMessage(string message)
        {
            m_strBroadCastingMessage = message;
            clsAnalyseMessage objCheck = new clsAnalyseMessage();
            enmMessageItemType enmItemType = enmMessageItemType.None;
            if (!m_strSendMessage.Equals(message) && (objCheck.m_blnIsMatching(message, out enmItemType)))
            {
                RemindFormShow(enmItemType, message);
            }
        }

        string m_strSendMessage = string.Empty;
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="p_strRemindMessage"></param>
        public void m_mthSendRemindMessage(string p_strRemindMessage)
        {
            if (!IsReady)
                return;
            m_strSendMessage = p_strRemindMessage;

            Thread thrSendMessage = new Thread(new ThreadStart(m_mthSendRemindMessage));
            thrSendMessage.IsBackground = true;
            thrSendMessage.Start();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private void m_mthSendRemindMessage()
        {
            if (!string.IsNullOrEmpty(m_strSendMessage))
            {
                watch.RaiseEvent(m_strSendMessage);
            }
        }

        /// <summary>
        /// ���յ��ķ������㲥��Ϣ
        /// </summary>
        private string m_strBroadCastingMessage = string.Empty;
        /// <summary>
        /// ��ȡ���յ��ķ������㲥��Ϣ
        /// </summary>
        public string StrBroadCastingMessage
        {
            get
            {
                return m_strBroadCastingMessage;
            }
        }

        /// <summary>
        /// ��ȡini�ļ���ȡ����˵�IP��ַ
        /// </summary>
        /// <returns></returns>
        protected string m_strGetIP()
        {
            string strIniFile = ".\\ReminMessageService.ini";
            if (System.IO.File.Exists(strIniFile))
            {
                clsIniFile objIni = new clsIniFile(strIniFile);
                return objIni.ReadString("Server", "IP", "");
            }
            else
            {
                return "";
            }
        }
    }
}
