using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// 液晶电视管理界面
    /// 2008.07.26 by kenny
    /// </summary>
    public class clsLCDManager
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="objdataModule">诊区所有信息</param>
        public clsLCDManager(clsDataModule objdataModule)
        {
            m_objDataModule = objdataModule;
            this.m_mthReadXML();
        }
        #endregion

        #region 变量

        /// <summary>
        /// 诊区数据VO
        /// </summary>
        clsDataModule m_objDataModule = null;
        /// <summary>
        /// LCD主屏
        /// </summary>
        frmLCDShow frmShow;
        /// <summary>
        /// 叫号信息
        /// </summary>
        List<string> lstMessage = new List<string>();
        /// <summary>
        /// 队列改变事件
        /// </summary>
        internal event LCDContentChangeEventHandler LCDQueueChanged;
        /// <summary>
        /// 单个诊室控件的宽度
        /// </summary>
        int m_intRoomWidth = 268;
        /// <summary>
        /// 单个诊室控件的高度
        /// </summary>
        int m_intRoomHeight = 273;
        /// <summary>
        /// 自定义发送界面
        /// </summary>
        frmSetLCDMonSize frm = null; 

        #endregion

        public void SendToMon()
        {
            frm = new frmSetLCDMonSize();
            frm.sendNewSize += new frmSetLCDMonSize.SendNewSizeEvtHandle(sendTo_Click);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.Dispose();
                frm = null;
            }
        }

        private void sendTo_Click(object sender, EventArgs e)
        {
            this.m_intRoomHeight = frm.m_intSendHeightValue;
            this.m_intRoomWidth = frm.m_intSendWidthValue;
        }

        #region 读取配置
        /// <summary>
        /// 读取配置
        /// </summary>
        private void m_mthReadXML()
        {
            try
            {
                string strVar1 = com.digitalwave.iCare.gui.HIS.clsPublic.m_strReadXML("MFZ_GUI", "LCDRoomWidth", "default");
                string strVar2 = com.digitalwave.iCare.gui.HIS.clsPublic.m_strReadXML("MFZ_GUI", "LCDRoomHeight", "default");
                m_intRoomWidth = Convert.ToInt32(strVar1);
                m_intRoomHeight = Convert.ToInt32(strVar2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常错误");
            }
        }
        #endregion

        #region 显示当前屏
        /// <summary>
        /// 显示当前屏
        /// </summary>
        public void m_mthShowMeCurrent()
        {
            if (frmShow == null || frmShow.IsDisposed)
            {
                frmShow = new frmLCDShow(lstMessage, m_objDataModule);
                frmShow.m_intRoomWidth = this.m_intRoomWidth;
                frmShow.m_intRoomHeight = this.m_intRoomHeight;
                frmShow.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmShow_FormClosed);
                frmShow.Show();
            }
            else
            {
                frmShow.Show();
                frmShow.BringToFront();
            }
        }
        #endregion

        #region 关闭
        private void frmShow_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmShow.Dispose();
            frmShow = null;
        }
        #endregion

        #region 添加呼叫信息
        /// <summary>
        /// 添加呼叫病人,发生在叫后事件之后
        /// </summary>
        /// <param name="strMessage">叫号信息</param>
        /// <param name="strDoctID">当前操作医生</param>
        /// <param name="p_callpatient">当前被叫病人实例</param>
        public void addToLCD(string strMessage, string strDoctID, weCare.Core.Entity.clsMFZPatientVO p_callpatient)
        {
            if (strMessage != "")
                this.addMessage(strMessage);

            //this.m_mthRefreshLCD(strDoctID, p_callpatient);
        }
        #endregion

        #region 更新屏幕
        /// <summary>
        /// 刷新LCD屏
        /// </summary>
        /// <param name="strDoctID"></param>
        /// <param name="p_callpatient">被叫病人</param>
        public void m_mthRefreshLCD(string strDoctID, weCare.Core.Entity.clsMFZPatientVO p_callpatient)
        {
            if (frmShow != null)
                frmShow.m_mthRefreshUI(strDoctID, p_callpatient);
        }
        #endregion

        #region 添加叫号信息
        /// <summary>
        /// 添加叫号信息,目前最多三条
        /// </summary>
        private void addMessage(string strMessage)
        {
            lstMessage.Insert(0, strMessage);
            if (lstMessage.Count > 3)
            {
                lstMessage.RemoveAt(3);
            }
        }
        #endregion
    }

    public delegate void LCDContentChangeEventHandler(object sender, PatientQueueChangeEventArgs e);

    public class PatientQueueChangeEventArgs : System.EventArgs
    {
        string strmessage;
        string strdoctid;
        weCare.Core.Entity.clsMFZPatientVO callpatient;

        public string strMessage
        {
            get { return strmessage; }
        }

        public string strDoctID
        {
            get { return strdoctid; }
        }
        public weCare.Core.Entity.clsMFZPatientVO CallPatient
        {
            get { return callpatient; }
        }
        public PatientQueueChangeEventArgs(string message, string DoctID, weCare.Core.Entity.clsMFZPatientVO patient)
        {
            this.strmessage = message;
            this.strdoctid = DoctID;
            this.callpatient = patient;
        }
    }
}
