using System;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsManageDocAndNurDomain.
    /// </summary>
    public class clsManageDocAndNurDomain
    {
        //private clsManageDocAndNurseService m_objServ;
        public clsManageDocAndNurDomain()
        {
            //m_objServ = new clsManageDocAndNurseService();
        }

        public long m_lngGetSpecialEmployeeInDept(int p_intFlag, out clsDocAndNur[] p_objArr)
        {
            p_objArr = null;

            //clsManageDocAndNurseService m_objServ =
            //    (clsManageDocAndNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsManageDocAndNurseService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngGetSpecialEmployeeInDeptArr(p_intFlag, clsEMRLogin.LoginInfo.m_strEmpNo, out p_objArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        //����д���ǲ������ģ���Ϊ����long��Ϊ�˽�����Ը���long��ֵ��ShowһЩ��Ӧ�Ĵ�����Ϣ
        //��Domain�ǲ�������MessageBox.Show���κ�������йصĲ����ģ������ŷ��Ϸֲ��˼�롣
        public clsDocAndNur[] m_objGetSpecialEmployeeInDept(int p_intFlag)
        {
            clsDocAndNur[] objArr = null;

            //clsManageDocAndNurseService m_objServ =
            //    (clsManageDocAndNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsManageDocAndNurseService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngGetSpecialEmployeeInDeptArr(p_intFlag, clsEMRLogin.LoginInfo.m_strEmpNo, out objArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return objArr;
        }

        public long m_lngSave(bool[] p_blnArr, clsDocAndNur[] p_objArr)
        {
            //�����ﲻ���ж�p_blnArr����p_objArr�Ƿ�Ϊ�ջ��߳���Ϊ0����Ϊ������֮ǰ�Ѿ��жϹ�
            //clsManageDocAndNurseService m_objServ =
            //    (clsManageDocAndNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsManageDocAndNurseService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngSave(p_blnArr, p_objArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public clsDocAndNur[] m_objGetSpecialEmployeeInDept(int p_intFlag, string p_strDeptID)
        {
            clsDocAndNur[] objArr = null;

            //clsManageDocAndNurseService m_objServ =
            //    (clsManageDocAndNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsManageDocAndNurseService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngGetSpecialEmployeeInDept(p_intFlag, p_strDeptID, out objArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return objArr;
        }


    }
}
