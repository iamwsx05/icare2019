using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Emr.Signature_gui;
namespace iCare
{
    /// <summary>
    /// ������Ҫ���ĳЩ������Ҫ�����ϼ�ȷ�Ϸ���ִ�н�����֤
    /// ͨ�����ݴ���Ĳ��������ж����
    /// �˴��Ŀ����Ρ���ʿ����ָ����������ְ�ƣ�ͨ����ɫȥʵ�֣����Ǽ����ϵ�ְ��
    /// </summary>
    public partial class frmValidateByDirector : Form
    {
        //����ǩ����
        private clsEmrSignToolCollection m_objSign;
        /// <summary>
        /// ��֤���
        /// ͨ��ΪTrue ��֮
        /// </summary>
        public bool BlnValidateResult;

        /// <summary>
        /// �ϼ��쵼ID
        /// </summary>
        public string mOperatorID;

         

        /// <summary>
        /// ���캯��
        /// </summary>
        public frmValidateByDirector()
        {
            InitializeComponent();
            BlnValidateResult = false;
            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();
            //m_mthBindEmployeeSign(��ť,ǩ����,����ҽʦ10or��ʿ��20,�����֤trueorfalse);
            m_objSign.m_mthBindEmployeeSign(btnDirector, txtDirectror, 10, true, clsEMRLogin.LoginInfo.m_strEmpID);

        }
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmValidateByDirector(int intFormType,string strDeptID)
        {
            InitializeComponent();
            BlnValidateResult = false;
            //ǩ������ֵ
            m_objSign = new clsEmrSignToolCollection();
            if (intFormType==1)
                //m_mthBindEmployeeSign(��ť,ǩ����,����ҽʦ10or��ʿ��20,�����֤trueorfalse);
                m_objSign.m_mthBindEmployeeSign(btnDirector, txtDirectror, 10, strDeptID,true);
            else
                //m_mthBindEmployeeSign(��ť,ǩ����,����ҽʦ10or��ʿ��20,�����֤trueorfalse);
                m_objSign.m_mthBindEmployeeSign(btnDirector, txtDirectror, 20,strDeptID, true );

        }

        /// <summary>
        /// ȷ����֤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtDirectror.Text.Trim().Length == 0)
            {
                MessageBox.Show(this,"�ϼ��쵼δǩ��","iCare Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else 
            {
                BlnValidateResult = true;
                this.Close();
            }
        }
        /// <summary>
        /// ȡ����֤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            BlnValidateResult = false;
            this.Close();
        }
    }
}