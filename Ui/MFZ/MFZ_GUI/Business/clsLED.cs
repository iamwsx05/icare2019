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
        private Thread showThread = null;// 显示LED屏信息的线程
        private SortedList<string, clsLEDShowVO> lstMessage = new SortedList<string, clsLEDShowVO>();
        private List<clsLEDScreen> lstLEDScreen = new List<clsLEDScreen>();
        private int refreshTime = 0; // LED刷屏间隔


        com.digitalwave.iCare.LEDManager.LianChen.LEDManager ledManager;
        System.Drawing.Font font = new System.Drawing.Font("宋体", 9f);

        public clsLED()
        {
            ledManager = new com.digitalwave.iCare.LEDManager.LianChen.LEDManager();
            string strMsg = string.Empty;
            bool res = ledManager.Load("MFZServer", out strMsg);
            if (!res)
            {
                new com.digitalwave.Utility.clsLogText().LogError("门诊分诊系统加载LED显示屏配置时发生错误:" + strMsg);
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
                    new com.digitalwave.Utility.clsLogText().LogError("门诊分诊文件配置错误！");
                }
            }
        }
        /// <summary>
        /// 增加一条记录到电子屏
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
        /// 根据卡号删除电子屏记录
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

            sb.Append(m_strConvertString("诊室", 13) + m_strConvertString("医生姓名", 12) +
                m_strConvertString("职称", 5) + m_strConvertString("下一个病人", 11) +
                m_strConvertString("病人2", 11) + m_strConvertString("病人3", 11) + "\r\n");
            for (int i = lstMessage.Count - 1; i >= 0; i--)
            {
                clsLEDShowVO obj = lstMessage.Values[i];
                string docType = (obj.m_enmDoctorType == enmMFZDoctorType.Expert ? "专家" : "普通");
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
        /// 换算字符串输出
        /// </summary>
        /// <param name="s">原字符串</param>
        /// <param name="num">单元格长度</param>
        /// <returns></returns>
        public string m_strConvertString(string s, int num)
        {
            string strTemp = string.Empty;
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] b = n.GetBytes(s);
            int l = 0; // l 为字符串少算长度 
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63) //判断是否为汉字或全脚符号 
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
        /// 显示黑屏
        /// </summary>
        public void CloseLED()
        {
            ledManager.ShowBlack(true);
        }
        /// <summary>
        /// 显示当前ＬＥＤ屏幕内容
        /// </summary>
        public void ShowLEDContent()
        {
            ledManager.ShowMeCurrent();
        }
        /// <summary>
        /// 暂停服务
        /// </summary>
        public void PauseServer()
        {
            ledManager.ShowFunction("Wait", 0, true);
        }
        /// <summary>
        /// 取消锁定
        /// </summary>
        public void GoOnServer()
        {
            ledManager.Unlock();
        }
        /// <summary>
        /// 发送自定义内容或图片到ＬＥＤ屏
        /// </summary>
        public void SendTo()
        {
            ledManager.SendTo();
        }
    }

    #region LED屏类
    public class clsLEDScreen
    {
        // ledNO="1" width="300" height="112"  linesNum="8" Font="宋体" FontSize="9"
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
