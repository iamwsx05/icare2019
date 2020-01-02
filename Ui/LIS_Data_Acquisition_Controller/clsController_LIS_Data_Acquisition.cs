using System;
using System.Collections.Generic;
using weCare.Core.Entity; //iCareData.dll
using com.digitalwave.iCare.middletier.LIS; //ILISDataAnalysis.dll --- data analysis interface; LIS_Svc.dll --- middle tier
using System.Net;
using System.Net.Sockets;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller
{

    public class clsDataReceivedEventArgs : System.EventArgs
    {
        private string m_strReceiveData;
        public string ReceiveData
        {
            get
            {
                return this.m_strReceiveData;
            }
        }
        public clsDataReceivedEventArgs(string p_strReceiveData)
        {
            this.m_strReceiveData = p_strReceiveData;
        }
    }
    public delegate void DataReceivedEventHandler(object sender, clsDataReceivedEventArgs e);//����ί��

    /// <summary>
    /// 
    /// </summary>
    public class clsInstrument_Connection_Info
    {
        public clsLIS_Equip_ConfigVO objInstrument_Config_VO = null;
        //2012-01-19��Ӿ���޸�
        public clsLIS_Equip_Base objInstrument_Config_VO2 = null;
        public infLISDataAnalysis objDataAnalyzer = null;
        public System.Windows.Forms.Timer objTimer = null;
    }


    /// <summary>
    /// frmLIS_Data_Acquisition_Controller�����߼�������	xing.chen���ע��
    /// </summary>
    public class clsController_LIS_Data_Acquisition
    {
        //		public com.digitalwave.iCare.ValueObject.clsLIS_Equip_ConfigVO[] objConfig_List = null;

        //xing.chen add test code
        private com.digitalwave.Utility.clsLogText testLog = new com.digitalwave.Utility.clsLogText();

        public frmLIS_Data_Acquisition_Controller m_objViewer;
        public clsController_LIS_Data_Acquisition()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        /// <summary>
        /// ��ȡ�뱾�������ص��������趨��Ϣ	xing.chen���ע��
        /// </summary>
        /// <param name="frmLIS_Controller">frmLIS_Data_Acquisition_Controller����</param>
        /// <param name="objConfig_List">�豸��ϸ������Ϣ��VO��</param>
        public void GetInstrumentSerialSetting(com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller.frmLIS_Data_Acquisition_Controller frmLIS_Controller, ref clsLIS_Equip_ConfigVO[] objConfig_List)
        {
            try
            {
                string strHost_Name = System.Net.Dns.GetHostName();
                System.Net.IPAddress objHost_IP = System.Net.Dns.GetHostEntry(strHost_Name).AddressList[0];
                
                string strHost_IP = objHost_IP.ToString();

                long lngRes = (new weCare.Proxy.ProxyLis()).Service.lngGetInstrumentSerialSetting(strHost_Name, out objConfig_List);
                if (lngRes == 1)
                {
                    if (objConfig_List != null)
                    {
                        int intCount = objConfig_List.Length;
                        if (intCount > 0)
                        {
                            frmLIS_Controller.m_cboInstrument.AddRangeItems(objConfig_List);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Output(ex.ToString());
            }
           
        }


        /// <summary>
        /// ��ȡ�뱾�������ص��������趨��Ϣ	yongchao.li���ע�� 2012-01-19
        /// </summary>
        /// <param name="frmLIS_Controller"></param>
        /// <param name="objConfig_List"></param>
        public void GetInstrumentSerialSetting2(com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller.frmLIS_Data_Acquisition_Controller frmLIS_Controller, ref clsLIS_Equip_Base[] objConfig_List)
        {
            string strHost_Name = System.Net.Dns.GetHostName();
            System.Net.IPAddress objHost_IP = System.Net.Dns.GetHostEntry(strHost_Name).AddressList[0];
            string strHost_IP = objHost_IP.ToString();

            //com.digitalwave.iCare.middletier.LIS.clsQueryLIS_Svc objLIS_Svc = (clsQueryLIS_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsQueryLIS_Svc));

            //long lngRes = objLIS_Svc.lngGetInstrumentSerialSetting2(strHost_Name, out objConfig_List);
            //if (lngRes == 1)
            //{
            //    if (objConfig_List != null)
            //    {
            //        int intCount = objConfig_List.Length;
            //        if (intCount > 0)
            //        {
            //            frmLIS_Controller.m_cboInstrument.AddRangeItems(objConfig_List);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// ѡ���豸���豸�趨��Ϣ�봰���еĿؼ���	xing.chen���ע��  2012-01-19 ��Ӿ���޸�
        /// </summary>
        /// <param name="frmLIS_Controller">frmLIS_Data_Acquisition_Controller����</param>
        public void SelectInstrument(com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller.frmLIS_Data_Acquisition_Controller frmLIS_Controller)
        {
            clsLIS_Equip_ConfigVO2 objEquip_ConfigVO1 = frmLIS_Controller.m_cboInstrument.SelectedItem as clsLIS_Equip_ConfigVO2;
            if (objEquip_ConfigVO1 != null)
            {
                frmLIS_Controller.m_txtBaudRate.Text = objEquip_ConfigVO1.strBaud_Rate;
                frmLIS_Controller.m_txtComNum.Text = objEquip_ConfigVO1.strCOM_No;
                frmLIS_Controller.m_txtDataBit.Text = objEquip_ConfigVO1.strData_Bit;
                switch (objEquip_ConfigVO1.strFlow_Control)
                {
                    case "0":
                        frmLIS_Controller.m_txtFlowControl.Text = "None";
                        break;
                    case "1":
                        frmLIS_Controller.m_txtFlowControl.Text = "Software";
                        break;
                    case "2":
                        frmLIS_Controller.m_txtFlowControl.Text = "Hardware";
                        break;
                }
                switch (objEquip_ConfigVO1.strParity)
                {
                    case "0":
                        frmLIS_Controller.m_txtParity.Text = "None";
                        break;
                    case "1":
                        frmLIS_Controller.m_txtParity.Text = "Even";
                        break;
                    case "2":
                        frmLIS_Controller.m_txtParity.Text = "Odd";
                        break;
                    case "3":
                        frmLIS_Controller.m_txtParity.Text = "Mark";
                        break;
                    case "4":
                        frmLIS_Controller.m_txtParity.Text = "Space";
                        break;
                }
                frmLIS_Controller.m_txtReceiveBuffer.Text = objEquip_ConfigVO1.strReceive_Buffer;
                frmLIS_Controller.m_txtSendBuffer.Text = objEquip_ConfigVO1.strSend_Buffer;
                frmLIS_Controller.m_txtStopBit.Text = objEquip_ConfigVO1.strStop_Bit;
                //frmLIS_Controller.m_ctlRemark.Text = "";
            }

            else
            {
                frmLIS_Controller.m_txtBaudRate.Text = "";
                frmLIS_Controller.m_txtComNum.Text = "";
                frmLIS_Controller.m_txtDataBit.Text = "";
                frmLIS_Controller.m_txtFlowControl.Text = "";
                frmLIS_Controller.m_txtParity.Text = "";
                frmLIS_Controller.m_txtReceiveBuffer.Text = "";
                frmLIS_Controller.m_txtSendBuffer.Text = "";
                frmLIS_Controller.m_txtStopBit.Text = "";
            }

            #region �ް汾ģʽ   2012-01-19 yongchao.li �޸�
            //com.digitalwave.iCare.ValueObject.clsLIS_Equip_ConfigVO objEquip_ConfigVO = (clsLIS_Equip_ConfigVO)frmLIS_Controller.m_cboInstrument.SelectedItem;

            //frmLIS_Controller.m_txtBaudRate.Text = objEquip_ConfigVO.strBaud_Rate;
            //frmLIS_Controller.m_txtComNum.Text = objEquip_ConfigVO.strCOM_No;
            //frmLIS_Controller.m_txtDataBit.Text = objEquip_ConfigVO.strData_Bit;
            //switch (objEquip_ConfigVO.strFlow_Control)
            //{
            //    case "0":
            //        frmLIS_Controller.m_txtFlowControl.Text = "None";
            //        break;
            //    case "1":
            //        frmLIS_Controller.m_txtFlowControl.Text = "Software";
            //        break;
            //    case "2":
            //        frmLIS_Controller.m_txtFlowControl.Text = "Hardware";
            //        break;
            //}

            //switch (objEquip_ConfigVO.strParity)
            //{
            //    case "0":
            //        frmLIS_Controller.m_txtParity.Text = "None";
            //        break;
            //    case "1":
            //        frmLIS_Controller.m_txtParity.Text = "Even";
            //        break;
            //    case "2":
            //        frmLIS_Controller.m_txtParity.Text = "Odd";
            //        break;
            //    case "3":
            //        frmLIS_Controller.m_txtParity.Text = "Mark";
            //        break;
            //    case "4":
            //        frmLIS_Controller.m_txtParity.Text = "Space";
            //        break;
            //}
            //frmLIS_Controller.m_txtReceiveBuffer.Text = objEquip_ConfigVO.strReceive_Buffer;
            //frmLIS_Controller.m_txtSendBuffer.Text = objEquip_ConfigVO.strSend_Buffer;
            //frmLIS_Controller.m_txtStopBit.Text = objEquip_ConfigVO.strStop_Bit;
            #endregion

        }
        /// <summary>
        /// ѡ���豸���豸�趨��Ϣ�봰���еĿؼ���	yongchao.li���ע��  2012-01-19
        /// </summary>
        /// <param name="frmLIS_Controller"></param>

        public void SelectInstrument2(com.digitalwave.iCare.gui.LIS_Data_Acquisition_Controller.frmLIS_Data_Acquisition_Controller frmLIS_Controller)
        {
            clsLIS_Equip_ConfigVO objEquip_ConfigVO = (clsLIS_Equip_ConfigVO)frmLIS_Controller.m_cboInstrument.SelectedItem;
            if (objEquip_ConfigVO != null)
            {
                frmLIS_Controller.m_txtBaudRate.Text = objEquip_ConfigVO.strBaud_Rate;
                frmLIS_Controller.m_txtComNum.Text = objEquip_ConfigVO.strCOM_No;
                frmLIS_Controller.m_txtDataBit.Text = objEquip_ConfigVO.strData_Bit;
                switch (objEquip_ConfigVO.strFlow_Control)
                {
                    case "0":
                        frmLIS_Controller.m_txtFlowControl.Text = "None";
                        break;
                    case "1":
                        frmLIS_Controller.m_txtFlowControl.Text = "Software";
                        break;
                    case "2":
                        frmLIS_Controller.m_txtFlowControl.Text = "Hardware";
                        break;
                }

                switch (objEquip_ConfigVO.strParity)
                {
                    case "0":
                        frmLIS_Controller.m_txtParity.Text = "None";
                        break;
                    case "1":
                        frmLIS_Controller.m_txtParity.Text = "Even";
                        break;
                    case "2":
                        frmLIS_Controller.m_txtParity.Text = "Odd";
                        break;
                    case "3":
                        frmLIS_Controller.m_txtParity.Text = "Mark";
                        break;
                    case "4":
                        frmLIS_Controller.m_txtParity.Text = "Space";
                        break;
                }
                frmLIS_Controller.m_txtReceiveBuffer.Text = objEquip_ConfigVO.strReceive_Buffer;
                frmLIS_Controller.m_txtSendBuffer.Text = objEquip_ConfigVO.strSend_Buffer;
                frmLIS_Controller.m_txtStopBit.Text = objEquip_ConfigVO.strStop_Bit;
            }
            else
            {
                frmLIS_Controller.m_txtBaudRate.Text = "";
                frmLIS_Controller.m_txtComNum.Text = "";
                frmLIS_Controller.m_txtDataBit.Text = "";
                frmLIS_Controller.m_txtFlowControl.Text = "";
                frmLIS_Controller.m_txtParity.Text = "";
                frmLIS_Controller.m_txtReceiveBuffer.Text = "";
                frmLIS_Controller.m_txtSendBuffer.Text = "";
                frmLIS_Controller.m_txtStopBit.Text = "";
            }
        }

        /// <summary>
        /// �������������ݽ����ļ��������ݣ�Ȼ���������ͬʱʵʱ��ʾ����	xing.chen���ע��
        /// </summary>
        /// <param name="objDataAnalyzer">�����ļ������</param>
        /// <param name="strData_Received">���յ�����</param>
        /// <param name="strInstrument_ID">����id</param>
        /// <param name="strInstrument_Name">������</param>
        public void DigitalSerial_DataComing(com.digitalwave.iCare.middletier.LIS.infLISDataAnalysis objDataAnalyzer, string strData_Received, string strInstrument_ID, string strInstrument_Name)
        {
            try
            {
                if (objDataAnalyzer != null)
                {
                    string[] strIntactDataList = objDataAnalyzer.strGetIntactData(strData_Received);
                    if (strIntactDataList != null)
                    {
                        for (int j = 0; j < strIntactDataList.Length; j++)
                        {
                            string strIntactData = strIntactDataList[j];
                            List<clsLIS_Device_Test_ResultVO> arlResult = null;
                            System.Collections.ArrayList arlResult2 = null;

                            //xing.chen add test code
                            testLog.Log2File(@"D:\logInfo.txt", "Data Analysis", DateTime.Now.ToLongTimeString());
                            ////testLog.Log2File(@"D:\code\log.txt", strIntactData, DateTime.Now.ToLongTimeString());
                            //testLog.Log2File(@"D:\logInfo.txt", strIntactData, DateTime.Now.ToLongTimeString());

                            long lngRes = objDataAnalyzer.lngDataAnalysis(strIntactData, out arlResult2);
                            //							int z=0;
                            //							while(z<arlResult.Count)
                            //							{
                            //								if(((clsLIS_Device_Test_ResultVO)arlResult[z]).intIsGraphResult == 1)
                            //								{
                            //									arlResult.RemoveAt(z);
                            //								}
                            //								else
                            //								{
                            //									z++;
                            //								}
                            //							}
                            clsLIS_Device_Test_ResultVO[] colDevice_Test_Results = (clsLIS_Device_Test_ResultVO[])arlResult2.ToArray(typeof(clsLIS_Device_Test_ResultVO));
                            if (arlResult != null)
                            {
                                try
                                {
                                    //string str = arlResult.Count.ToString() + " 123";
                                    //testLog.Log2File(@"D:\logInfo.txt", str, DateTime.Now.ToLongTimeString());
                                    for (int i = 0; i < colDevice_Test_Results.Length; i++)
                                    {
                                        colDevice_Test_Results[i].strDevice_ID = strInstrument_ID;
                                    }

                                    lngRes = 0;
                                    List<clsLIS_Device_Test_ResultVO> arlOut = null;

                                    //xing.chen add test code

                                    testLog.Log2File(@"D:\logInfo.txt", "Data insert database", DateTime.Now.ToLongTimeString());

                                    lngRes = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(arlResult, out arlOut);
                                    if (lngRes > 0)
                                    {
                                        arlResult = arlOut;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string srr = ex.Message;
                                }
                                #region ��ʾʵʱ��Ϣ
                                if (lngRes > 0)
                                {
                                    //xing.chen add test code
                                    testLog.Log2File(@"D:\logInfo.txt", "Data Show", DateTime.Now.ToLongTimeString());

                                    colDevice_Test_Results = arlResult.ToArray();

                                    clsDeviceSampleDataKey objKey = new clsDeviceSampleDataKey();
                                    objKey.intResultBeginIndex = colDevice_Test_Results[0].intIndex;
                                    objKey.intResultEndIndex = colDevice_Test_Results[colDevice_Test_Results.Length - 1].intIndex;
                                    objKey.strDeviceID = strInstrument_ID;
                                    objKey.strDeviceName = strInstrument_Name;
                                    objKey.strDeviceSampleID = colDevice_Test_Results[0].strDevice_Sample_ID;
                                    objKey.strCheckDate = colDevice_Test_Results[0].strCheck_Date;
                                    m_objViewer.m_mthShowMessage(objKey, colDevice_Test_Results);
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// �������ݽ����ļ�����	xing.chen���ע��
        /// </summary>
        /// <param name="strData_Analysis_DLL">���ݽ����ļ���dll����</param>
        /// <param name="strData_Analysis_Namespace">���ݽ����ļ������ƿռ�</param>
        /// <returns></returns>
        public object objGetDataAnalyzer(string strData_Analysis_DLL, string strData_Analysis_Namespace)
        {
            System.Reflection.Assembly objAssembly = null;
            System.Type objType = null;
            try
            {
                string strFilePath = System.AppDomain.CurrentDomain.BaseDirectory;
                strFilePath += "LIS_DataAnalyse\\";
                strFilePath += strData_Analysis_DLL;
                objAssembly = System.Reflection.Assembly.LoadFrom(strFilePath);
                if (objAssembly == null)
                    return null;
                objType = objAssembly.GetType(strData_Analysis_Namespace);
                if (objType == null)
                    return null;
            }
            catch
            {
                return null;
            }
            return Activator.CreateInstance(objType);
        }

        /// <summary>
        /// ������������	xing.chen���ע��
        /// </summary>
        /// <param name="p_strDeviceID">����id</param>
        /// <param name="p_strDeviceSampleID">��������id</param>
        /// <param name="p_strCheckDate">������������</param>
        /// <param name="p_intBeginIndex"></param>
        /// <param name="p_intEndIndex"></param>
        /// <param name="p_objDeviceResultList">�������ݣ�out VO��</param>
        public void m_mthGetDeviceData(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate,
            int p_intBeginIndex, int p_intEndIndex, out clsDeviceReslutVO[] p_objDeviceResultList)
        {
            p_objDeviceResultList = null;
            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetDeviceData(p_strDeviceID, p_strDeviceSampleID, p_strCheckDate,
             p_intBeginIndex, p_intEndIndex, out p_objDeviceResultList);

        }
    }

    /// <summary>
    /// ��дtoString���������������������������ַ��� xing.chen���ע��
    /// </summary>
    public class clsDeviceSampleDataKey
    {
        public string strDeviceID;
        public string strDeviceName;
        public string strDeviceSampleID;
        public string strCheckDate;
        public int intResultBeginIndex;
        public int intResultEndIndex;
        public string strCommingDateTime;
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(strDeviceID);
            sb.Append("||");
            sb.Append(strCheckDate);
            sb.Append("||");
            sb.Append(strDeviceSampleID);
            sb.Append("||");
            sb.Append(intResultBeginIndex.ToString());
            sb.Append("||");
            sb.Append(intResultEndIndex.ToString());
            sb.Append("||");
            sb.Append(strCommingDateTime);
            return sb.ToString();
        }

    }

    #region xing.chen 8/10/2005	����udp���Զ����װ�ģ�����������
    public class clsController_NetWorkListener
    {
        public event DataReceivedEventHandler DataReceived;     //�����¼�
        public event DataReceivedEventHandler Info;
        private string m_strIpAddress;
        private string m_strPort;
        private com.digitalwave.Utility.clsLogText m_log = new com.digitalwave.Utility.clsLogText();
        public clsController_NetWorkListener(string p_strIpAddress, string p_strPort)
        {
            //���캯��
            this.m_strIpAddress = p_strIpAddress;
            this.m_strPort = p_strPort;
        }


        private Socket m_Listener;
        private Socket m_socket;
        private byte[] m_buffer = new byte[30720];

        public void Start()
        {
            int intLocalPort = -1;
            try
            {
                intLocalPort = int.Parse(this.m_strPort);

                m_Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                System.Net.EndPoint localEP = new System.Net.IPEndPoint(System.Net.IPAddress.Any, intLocalPort);

                m_Listener.Bind(localEP);


                m_Listener.Listen(10);

                while (true)
                {
                    m_socket = m_Listener.Accept();
                    while (true)
                    {
                        int count = m_socket.Receive(m_buffer);
                        if (count > 0)
                        {
                            if (DataReceived != null)
                            {
                                DataReceived(this, new clsDataReceivedEventArgs(System.Text.Encoding.ASCII.GetString(m_buffer)));
                            }
                        }
                        else
                        {
                            try
                            {
                                m_socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                            }
                            catch { }
                            m_socket.Close();
                            m_socket = null;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (m_Listener != null)
                {
                    if (m_Listener.Connected)
                    {
                        try
                        {
                            m_Listener.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                        }
                        catch { }
                    }
                    m_Listener.Close();
                    m_Listener = null;
                }
                if (m_socket != null)
                {
                    if (m_socket.Connected)
                    {
                        try
                        {
                            m_socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                        }
                        catch { }
                    }
                    m_socket.Close();
                    m_socket = null;
                }
                try
                {
                    m_log.LogError(ex);
                }
                catch { }
                if (Info != null)
                {
                    Info(this, new clsDataReceivedEventArgs("���Ӵ���"));
                }
            }
        }

        public void Stop()
        {
            if (m_Listener != null)
            {
                if (m_Listener.Connected)
                {
                    try
                    {
                        m_Listener.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    }
                    catch { }
                }
                m_Listener.Close();
                m_Listener = null;
            }
            if (m_socket != null)
            {
                if (m_socket.Connected)
                {
                    try
                    {
                        m_socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    }
                    catch { }
                }
                m_socket.Close();
                m_socket = null;
            }
        }
    }
    #endregion
}
