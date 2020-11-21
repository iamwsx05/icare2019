using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using weCare.Core.Entity;
using System.Drawing;
using System.Collections;
using System.Threading;

namespace com.digitalwave.iCare.gui.MFZ
{
    public class clsLED
    {
        private int maxLinesNum = 0;
        private int lineNum = 0;
        private Thread showThread = null;// ��ʾLED����Ϣ���߳�
        private SortedList<string, clsLEDShowVO> lstMessage = new SortedList<string, clsLEDShowVO>();
        private List<clsLEDScreen> lstLEDScreen = new List<clsLEDScreen>();
        private int refreshTime = 0; // LEDˢ�����


        com.digitalwave.iCare.LEDManager.LianChen.LEDManager ledManager;
        System.Drawing.Font font = new System.Drawing.Font("����", 9f);

        public clsLED()
        {
            ledManager = new com.digitalwave.iCare.LEDManager.LianChen.LEDManager();
            string strMsg = string.Empty;
            bool res = ledManager.Load("MFZServer", out strMsg);
            if (!res)
            {
                new com.digitalwave.Utility.clsLogText().LogError("�������ϵͳ����LED��ʾ������ʱ��������:" + strMsg);
            }
            System.Collections.Specialized.NameValueCollection nv = ledManager.GetMasterCustomStyle(0);
            try
            {
                maxLinesNum = int.Parse(nv["LineNum"]);
            }
            catch
            {
                maxLinesNum = 8;
            }

            try
            {
                refreshTime = int.Parse(nv["RefreshTime"])*1000;
            }
            catch (Exception)
            {
                
                refreshTime=6000;
            }

            for (int i = 0; i < ledManager.LEDCount; i++)
            {
                System.Collections.Specialized.NameValueCollection customConfig = ledManager.GetMasterCustomStyle(i);
                try
                {
                    font = new System.Drawing.Font(customConfig["Font"], float.Parse(nv["FontSize"]));
                    lineNum = int.Parse(customConfig["LineNum"]);
                    clsLEDScreen entity = new clsLEDScreen();
                    entity.font = font;
                    entity.linesNum = lineNum;
                    entity.width = ledManager.GetSize(i).Width;
                    entity.height = ledManager.GetSize(i).Height;
                    lstLEDScreen.Add(entity);

                }
                catch
                {
                    new com.digitalwave.Utility.clsLogText().LogError("��������ļ����ô���");
                }
            }
        }
        /// <summary>
        /// ����һ����¼��������
        /// </summary>
        /// <param name="strPatientCardNO"></param>
        /// <param name="strLedMessage"></param>
        public void Add(string p_strDoctID, clsLEDShowVO strLedMessage)
        {
            lock (lstMessage)
            {
                RemoveFromList(p_strDoctID);
                lstMessage.Add(p_strDoctID, strLedMessage);
                if (lstMessage.Count > maxLinesNum)
                {
                    lstMessage.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// ���ݿ���ɾ����������¼
        /// </summary>
        /// <param name="strPatientCardNO"></param>
        public void RemoveFromList(string p_strDoctID)
        {
            if (lstMessage.ContainsKey(p_strDoctID))
            {
                lstMessage.Remove(p_strDoctID);
            }
        }

        private string GetContent(int lineNum)
        {
            StringBuilder sb = new StringBuilder();

            if (lstMessage.Count == 0) { return string.Empty; }

            sb.Append(m_strConvertString("����", 13) + m_strConvertString("ҽ������", 12) +
                m_strConvertString("ְ��", 5) + m_strConvertString("��һ������", 11) +
                m_strConvertString("����2", 11) + m_strConvertString("����3", 11) + "\r\n");
            for (int i = lstMessage.Count - 1; i >= 0; i--)
            {
                clsLEDShowVO obj = lstMessage.Values[i];
                string docType = (obj.m_enmDoctorType == enmMFZDoctorType.Expert ? "ר��" : "��ͨ");
                string v = m_strConvertString(obj.m_strRoomName, 13) + m_strConvertString(obj.m_strDoctorName, 12) +
                   m_strConvertString(docType, 5);
                if (obj.m_lstPatient != null || obj.m_lstPatient.Count == 0)
                {
                    int i2 = 0;
                    foreach (clsMFZPatientVO patient in obj.m_lstPatient)
                    {
                        i2++;
                        v += m_strConvertString(patient.m_strPatientName, 11);
                        if (i2 == 3)
                            break;
                    }
                }
                v += "\r\n";
                sb.Append(v);
            }

            return sb.ToString();
        }

        /// <summary>
        /// �����ַ������
        /// </summary>
        /// <param name="s">ԭ�ַ���</param>
        /// <param name="num">��Ԫ�񳤶�</param>
        /// <returns></returns>
        public string m_strConvertString(string s, int num)
        {
            string strTemp = string.Empty;
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] b = n.GetBytes(s);
            int l = 0; // l Ϊ�ַ������㳤�� 
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63) //�ж��Ƿ�Ϊ���ֻ�ȫ�ŷ��� 
                {
                    l++;
                }
            }
            strTemp = s.PadRight(num - l, ' ');
            return strTemp;
        }

        public void Start()
        {
            //if (this.showThread == null)
            //{
            //    showThread = new Thread(new ThreadStart(Show));
            //    showThread.IsBackground = true;
            //    showThread.Start();
            //}
        }

        public void Stop()
        {
            if (this.showThread != null)
            {
                this.showThread.Abort();
                this.showThread = null;
            }
            //ledManager.Unload();
            //ledManager = null;
            //GC.Collect();
            //System.GC.WaitForPendingFinalizers();
        }

        public void Show()
        {
            for (int i = 0; i < lstLEDScreen.Count; i++)
            {
                Bitmap bmp = new Bitmap(lstLEDScreen[i].width + 4, lstLEDScreen[i].height + 4);
                string content = GetContent(lstLEDScreen[i].linesNum);
                if (!string.IsNullOrEmpty(content))
                {
                    Graphics g = Graphics.FromImage(bmp);
                    SolidBrush solidbrush = new SolidBrush(Color.FromArgb(214, 223, 247));
                    g.FillRectangle(solidbrush, 0, 0, bmp.Width, bmp.Height);
                    g.DrawString(content, font, Brushes.Blue, 0, 0);
                    g.Dispose();
                    ledManager.Show(bmp, i, false);
                }
            }
        }
       

        /// <summary>
        /// ��ʾ����
        /// </summary>
        public void CloseLED()
        {
            ledManager.ShowBlack(true);
        }
        /// <summary>
        /// ��ʾ��ǰ�̣ţ���Ļ����
        /// </summary>
        public void ShowLEDContent()
        {
            ledManager.ShowMeCurrent();
        }
        /// <summary>
        /// ��ͣ����
        /// </summary>
        public void PauseServer()
        {
            ledManager.ShowFunction("Wait", 0, true);
        }
        /// <summary>
        /// ȡ������
        /// </summary>
        public void GoOnServer()
        {
            ledManager.Unlock();
        }
        /// <summary>
        /// �����Զ������ݻ�ͼƬ���̣ţ���
        /// </summary>
        public void SendTo()
        {
            ledManager.SendTo();
        }
    }

    #region LED����
    public class clsLEDScreen
    {
        // ledNO="1" width="300" height="112"  linesNum="8" Font="����" FontSize="9"
        public int ledNO;
        public int width;
        public int height;
        public int linesNum;
        public System.Drawing.Font font;

        public clsLEDScreen()
        {
            
        }
    }
    #endregion
}
