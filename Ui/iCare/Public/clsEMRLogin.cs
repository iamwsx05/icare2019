using System;
using System.Windows.Forms;
using com.digitalwave.Emr.StaticObject;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// ��¼��Ϣ:��ϵͳ��¼���ʼ�����Ӳ����������Ϣ
    /// </summary>
    public class clsEMRLogin : MarshalByRefObject
    {
        //private System.Windows.Forms.Timer tmGetMsg;
        private string m_strDoctorID;

        public clsEMRLogin()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            //this.tmGetMsg = new System.Windows.Forms.Timer();

            //this.tmGetMsg.Enabled = false;
            //this.tmGetMsg.Interval = 300000;
            //this.tmGetMsg.Tick += new System.EventHandler(this.tmGetMsg_Tick);
        }

        public void m_mthInit(Form p_frmMDI, clsLoginInfo p_objLoginInfo)
        {
            s_frmMDI = p_frmMDI;
            m_objLoginInfo = p_objLoginInfo;

            clsLoginContext objLogin = clsLoginContext.s_ObjLoginContext;
            objLogin.m_StrEmployeeID = p_objLoginInfo.m_strEmpNo;
            MDIParent.m_mthClearAll();
            m_objCurDeptOfEmpArr = null;
            new clsForWholeHosInfoManager().m_lngGetDepartmentByUserID(p_objLoginInfo.m_strEmpID, out m_objCurDeptOfEmpArr);

            m_mthGetCurrentHospitalInfo();

            #region ����ȫ��Ա����������Ա���࣬�þ�̬��ᱻ�ⲿ����ģ����õ���Ҫ��֤��ȷ��ֵ tfzhang 2006��04��07
            clsEmrEmployeeBase_VO m_objEmployeeTemp;
            com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objsrv = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            long lngRes = objsrv.m_lngGetEmpByID(m_objLoginInfo.m_strEmpID, out m_objEmployeeTemp);
            //δ����״̬ ����ǩ������
            m_objEmployeeTemp.m_intSTATUS_INT = 0;
            com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentEmployee = m_objEmployeeTemp;
            #endregion


            iCare.CustomForm.clsExteriorFunctionInterface.m_ObjUserInfo = p_objLoginInfo;
            iCare.CustomForm.clsExteriorFunctionInterface.m_ObjCurDeptOfEmpArr = m_objCurDeptOfEmpArr;

            //��ȡ�޸�ʱ�� Сʱ
            if (clsEMR_StaticObject.s_IntCanModifyTime <= 0)
            { 
                (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRTBChangeTime("3001", out strCanModifyTime);
            }
            else
            {
                strCanModifyTime = clsEMR_StaticObject.s_IntCanModifyTime.ToString();
            }

            m_strDoctorID = p_objLoginInfo.m_strEmpNo;
        }

        #region MDI����
        private static Form s_frmMDI = null;
        public static Form s_FrmMDI
        {
            get { return s_frmMDI; }
        }
        #endregion

        /// <summary>
        /// ��ʱ��ȡ��ǰҽ��������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmGetMsg_Tick(object sender, System.EventArgs e)
        {
            //��ʱ���� �˲���Ӧ�����м������ thzhang 2006��04��01
            //System.Data.DataTable dtbMsg=new System.Data.DataTable();
            //string strSQL="SELECT * FROM T_TODAYMESSAGES WHERE DOCTORID='"+m_strDoctorID.Trim()+"'";
            //com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objServ = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            //long lngRes=objServ.DoGetDataTable(strSQL,ref dtbMsg);
            ////�յ���Ϣ��ɾ����Ϣ
            //strSQL = "DELETE FROM T_TODAYMESSAGES WHERE DOCTORID='"+m_strDoctorID.Trim()+"'";
            //lngRes = objServ.DoExcute(strSQL);
            ////objServ.Dispose();
            //if(dtbMsg.Rows.Count>0)
            //{
            //    string strMSG=dtbMsg.Rows[0]["DOCTORNAME"].ToString().Trim()+":����!";
            //    for(int i=0;i<dtbMsg.Rows.Count;i++)
            //    {					
            //        strMSG=strMSG+"\n"+dtbMsg.Rows[i]["BENNAME"].ToString().Trim()+"���Ļ��ߡ�"+dtbMsg.Rows[i]["PATIENTNAME"].ToString().Trim()+"��";
            //        strMSG=strMSG+"\n"+dtbMsg.Rows[i]["TODAYMESSAGES"].ToString().Trim();					
            //    }
            //    tmGetMsg.Stop();
            //    clsPublicFunction.ShowInformationMessageBox(strMSG);
            //}
            //GC.Collect();
        }

        /// <summary>
        /// ��ȡ��ǰҽԺ���Ƽ�����
        /// </summary>
        private void m_mthGetCurrentHospitalInfo()
        {
            if (clsEMR_StaticObject.s_StrCurrentHospitalName == string.Empty || clsEMR_StaticObject.s_StrCurrentHospitalNO == string.Empty)
            { 
                long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetHospitalInfo(out m_strCurrentHospitalName, out m_strCurrentHospitalNO);
            }
            else
            {
                m_strCurrentHospitalName = clsEMR_StaticObject.s_StrCurrentHospitalName;
                m_strCurrentHospitalNO = clsEMR_StaticObject.s_StrCurrentHospitalNO;
            }
        }
        /// <summary>
        /// ��ȡ��¼Ա����Ϣ ��
        /// </summary>
        private static weCare.Core.Entity.clsLoginInfo m_objLoginInfo;
        /// <summary>
        /// ��ȡ��¼Ա����ϢVO ��
        /// </summary>
        private static weCare.Core.Entity.clsEmrEmployeeBase_VO m_objLoginEmployee;
        /// <summary>
        /// ��ȡ��¼Ա����Ϣ �� 
        /// </summary>
        public static weCare.Core.Entity.clsLoginInfo LoginInfo
        {
            get
            {
                if (m_objLoginInfo != null)
                    return m_objLoginInfo;
                else
                {
                    return new  clsLoginInfo();

                }
            }
        }
        /// <summary>
        ///  ��ȡ��¼Ա����ϢVO ��
        /// </summary>
        public static weCare.Core.Entity.clsEmrEmployeeBase_VO LoginEmployee
        {
            get
            {
                if (m_objLoginEmployee != null)
                {
                    m_objLoginEmployee.m_intSTATUS_INT = 0;
                    return m_objLoginEmployee;
                }

                else if (clsEMR_StaticObject.s_ObjCurrentEmployee != null)
                {
                    clsEMR_StaticObject.s_ObjCurrentEmployee.m_intSTATUS_INT = 0;
                    return clsEMR_StaticObject.s_ObjCurrentEmployee;
                }
                else
                {
                    com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objsrv = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                    long lngRes = objsrv.m_lngGetEmpByID(m_objLoginInfo.m_strEmpID, out m_objLoginEmployee);
                    if (m_objLoginEmployee != null)
                    {
                        //δ����״̬ ����ǩ������
                        m_objLoginEmployee.m_intSTATUS_INT = 0;
                    }
                    return m_objLoginEmployee;
                }
            }
        }

        private static clsDepartmentVO[] m_objCurDeptOfEmpArr;
        /// <summary>
        /// ��ȡ��¼Ա����������
        /// </summary>
        public static clsDepartmentVO[] m_ObjCurDeptOfEmpArr
        {
            get { return m_objCurDeptOfEmpArr; }
        }

        private static string m_strCurrentHospitalName;
        /// <summary>
        /// ��ȡ��ǰҽԺ����
        /// </summary>
        public static string m_StrCurrentHospitalName
        {
            get { return m_strCurrentHospitalName; }
        }

        private static string m_strCurrentHospitalNO;
        /// <summary>
        /// ��ȡ��ǰҽԺ����(������������(7λ)+���)
        /// </summary>
        public static string m_StrCurrentHospitalNO
        {
            get { return m_strCurrentHospitalNO; }
        }
        private static string strCanModifyTime;
        /// <summary>
        /// ��ȡ�޸�ʱЧ
        /// </summary>
        public static string StrCanModifyTime
        {
            get { return strCanModifyTime; }
        }

    }
}
