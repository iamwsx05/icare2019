using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// 电视屏主屏界面
    /// 2008.07.28 by kenny
    /// </summary>
    public partial class frmLCDShow : Form
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_lstmessage">叫号信息</param>
        /// <param name="p_dataModule">诊区信息</param>
        public frmLCDShow(List<string> p_lstmessage, clsDataModule p_dataModule)
        {
            InitializeComponent();
            m_objDataModule = p_dataModule;
            lstmessage = p_lstmessage;
        }
        #endregion

        #region 变量

        /// <summary>
        /// 叫号信息数组
        /// </summary>
        List<string> lstmessage = null;
        /// <summary>
        /// 诊区主信息
        /// </summary>
        clsDataModule m_objDataModule;
        /// <summary>
        /// 控件宽度
        /// </summary>
        public int m_intRoomWidth = 260;
        /// <summary>
        /// 控件高度
        /// </summary>
        public int m_intRoomHeight = 273;

        #endregion

        #region 加载窗体
        private void frmLCDShow_Load(object sender, EventArgs e)
        {
            if (m_objDataModule != null)
            {
                foreach (clsDept dept in m_objDataModule.daigArea.depts)
                {
                    foreach (clsRoom room in dept.rooms)
                    {
                        foreach (clsDoctor doctor in room.doctors)
                        {
                            ctlLCDRoomElement ctlRoom = new ctlLCDRoomElement();
                            ctlRoom.Size = new Size(m_intRoomWidth, m_intRoomHeight);
                            ctlRoom.m_strRoomName = room.strRoomName;
                            ctlRoom.m_strDeptName = dept.strDeptName;
                            ctlRoom.m_strDocName = " " + doctor.strDoctName;
                            ctlRoom.m_strDocType = (doctor.doctorType == weCare.Core.Entity.enmMFZDoctorType.Expert ? "专家门诊" : "普通门诊");
                            ctlRoom.m_strWaitCount = doctor.firstQueue.Count.ToString();
                            ctlRoom.m_lstPatients = doctor.firstQueue;
                            ctlRoom.Tag = doctor.strDoctID;
                            this.flowLayoutPanel1.Controls.Add(ctlRoom);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("当前诊区不存在诊室!", "系统提示");
            }
            this.showMessage(); // 显示叫号信息
        }
        #endregion

        #region 显示叫号信息
        /// <summary>
        /// 显示叫号信息
        /// </summary>
        private void showMessage()
        {
            if (lstmessage.Count > 0)
            {
                this.label1.Text = lstmessage[0];

                if (lstmessage.Count > 1)
                    this.label2.Text = lstmessage[1];

                if (lstmessage.Count > 2)
                    this.label3.Text = lstmessage[2];
            }
        }
        #endregion

        #region 刷新屏幕,操作在叫号事件之后
        /// <summary>
        /// 刷新屏幕,数据已经和管理台同步.主要是m_objDataModule和lstmessage
        /// 需考虑呼叫下一病人和呼叫特定病人两种情况
        /// </summary>
        /// <param name="strDoctID">操作医生</param>
        /// <param name="callpatient">当前呼叫病人</param>
        public void m_mthRefreshUI(string strDoctID, weCare.Core.Entity.clsMFZPatientVO callpatient)
        {
            foreach (Control ctl in this.flowLayoutPanel1.Controls)
            {
                ctlLCDRoomElement objControl = ctl as ctlLCDRoomElement;
                if (objControl != null)
                {
                    // 更新病人队列
                    if (objControl.Tag.ToString() == strDoctID)
                    {
                        objControl.m_mthSetlistViewReDraw();

                        //if (objControl.m_lstPatients.Count > 0)
                        //{
                        //    if (callpatient.m_strPatientCardNO == objControl.m_lstPatients[0].m_strPatientCardNO)
                        //    {
                        //        // 呼叫下一病人
                        //        objControl.m_lstPatients = objControl.m_lstPatients; // 触发赋值,毋须更改
                        //    }
                        //    else
                        //    {
                        //        // 呼叫特定病人
                        //        objControl.m_mthCallSomePatient(callpatient);
                        //    }
                        //}
                        //else
                        //{
                        //    // 呼叫特定病人
                        //    objControl.m_mthCallSomePatient(callpatient);
                        //}
                    }

                    // 更新候诊人数
                    objControl.m_strWaitCount = objControl.m_lstPatients.Count.ToString();
                }
            }
            this.showMessage(); // 显示叫号信息
        }
        #endregion
    }
}