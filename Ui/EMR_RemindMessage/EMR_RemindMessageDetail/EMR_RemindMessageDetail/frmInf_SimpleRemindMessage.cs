using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace com.digitalwave.iCare.RemindMessage
{
    /// <summary>
    /// ��Ⱦ����ʾ����
    /// </summary>
    public partial class frmInf_SimpleRemindMessage : Form
    {
        [DllImport("User32.dll")]
        static extern Boolean MessageBeep(UInt32 beepType);
        /// <summary>
        /// �����
        /// </summary>
        private int iTotalCount = 300;
        /// <summary>
        /// ��ǰ��
        /// </summary>
        private int iCurrentCount = 0;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string m_strMessate = string.Empty;

        /// <summary>
        /// �����Ѵ���
        /// </summary>
        public frmInf_SimpleRemindMessage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ȡ������������Ϣ
        /// </summary>
        public string m_StrMessage
        {
            get { return m_txtSimpleMessage.Text; }
            set 
            {
                if (string.IsNullOrEmpty(m_txtSimpleMessage.Text))
                {
                    m_txtSimpleMessage.AppendText ( value);
                }
                else
                {
                    m_txtSimpleMessage.AppendText( System.Environment.NewLine + System.Environment.NewLine + value);
                }
                iCurrentCount = 0;
                m_timer.Enabled = true;
                m_txtSimpleMessage.ScrollToCaret();
            }
        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            if (iCurrentCount < iTotalCount)
                iCurrentCount++;
            else if (iCurrentCount < 2 * iTotalCount)
            {
                iCurrentCount++;
                MessageBeep(16);
            }
            else
            {
                m_timer.Enabled = false;
            }
        }

        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            m_timer.Enabled = false;
            m_txtSimpleMessage.Clear();
            this.Close();
            
        }

        private void frmInf_SimpleRemindMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_timer.Enabled = false;
            m_txtSimpleMessage.Clear();

            //m_mthOpenForm();
        }

        /// <summary>
        /// �򿪴���
        /// </summary>
        public void m_mthOpenForm()
        {
            Form m_frmWellOpen = null;
            System.Reflection.MethodInfo mi = null;
            Form[] frmArr = this.Owner.MdiChildren;

            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440606001")
            {
                // �׽�ҽԺ
                if (frmArr != null && frmArr.Length > 0)
                {
                    for (int index = 0; index < frmArr.Length; index++)
                    {
                        if (frmArr[index].AccessibleDescription == "��������������Ϣ����ϵͳ")
                        {
                            m_frmWellOpen = frmArr[index];
                            break;
                        }

                    }
                }

                if (m_frmWellOpen != null)
                {
                    mi = m_frmWellOpen.GetType().GetMethod("ShowInfectionManage");
                    mi.Invoke(m_frmWellOpen, null);
                }
                else
                {
                    m_mthInvokeMethodWithReflection("CommunitySystem_Main.dll", "com.digitalwave.iCare.CommunitySystem.frmCommunitySystem_Main", "ShowInfectionManage", null, this.Owner);
                }
            }
            else
            {
                // ����ֻ�й�ҽ��Ժ��
                if (frmArr != null && frmArr.Length > 0)
                {
                    for (int index = 0; index < frmArr.Length; index++)
                    {
                        if (frmArr[index].GetType().Name == "ctlInfectionManage")
                        {
                            m_frmWellOpen = frmArr[index];
                            break;
                        }

                    }
                }

                if (m_frmWellOpen != null)
                {
                    mi = m_frmWellOpen.GetType().GetMethod("ShowInfectionManage");
                    mi.Invoke(m_frmWellOpen, null);
                }
                else
                {
                    m_mthInvokeMethodWithReflection("CommunitySystem_Main.dll", "com.digitalwave.iCare.CommunitySystem.frmCommunitySystem", "ShowInfectionManage", null, this.Owner);
                }

            }

        }

        /// <summary>
        /// ͨ������ִ�з���
        /// </summary>
        /// <param name="p_strDllName">DLL����</param>
        /// <param name="p_strClassName">����(����namespace)</param>
        /// <param name="p_strMethodName">��������</param>
        /// <param name="p_objParameter">����</param>
        /// <param name="p_objParent"></param>
        public object m_mthInvokeMethodWithReflection(string p_strDllName, string p_strClassName, string p_strMethodName, object p_objParameter, Form p_objParent)
        {
            if (string.IsNullOrEmpty(p_strDllName) || string.IsNullOrEmpty(p_strMethodName))
            {
                return null;
            }

            try
            {
                Form frmOpen = null;
                
                System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(Application.StartupPath + "\\" + p_strDllName);
                frmOpen = asm.CreateInstance(p_strClassName) as Form;
                if (frmOpen == null)
                {
                    return null;
                }

                frmOpen.MdiParent = p_objParent;

                System.Reflection.MethodInfo mi = null;

                if (p_objParameter == null)
                {
                    mi = frmOpen.GetType().GetMethod(p_strMethodName);
                    return mi.Invoke(frmOpen, null);
                }
                else
                {
                    mi = frmOpen.GetType().GetMethod(p_strMethodName, new Type[] { p_objParameter.GetType() });
                    return mi.Invoke(frmOpen, new object[] { p_objParameter });
                }
            }
            catch (Exception Ex)
            {
                string strErr = Ex.Message;
            }

            return null;

        }

        


    }
}