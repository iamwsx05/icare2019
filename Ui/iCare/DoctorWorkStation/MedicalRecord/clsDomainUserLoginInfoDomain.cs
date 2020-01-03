using System;
using System.Xml;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsDomainUserLoginInfoDomain.
    /// </summary>
    public class clsDomainUserLoginInfoDomain
    {
        //private clsDomainUserLoginInfoServ m_objServ=new clsDomainUserLoginInfoServ();
        public clsDomainUserLoginInfoDomain()
        {
            //
            // TODO: Add constructor logic here
            //

        }
        public long m_lngAddDomainUserLoginInfo(clsDomainUserLoginInfo p_objDomainUserLoginInfo, out string p_strDateTimeNow)
        {
            //clsDomainUserLoginInfoServ m_objServ =
            //    (clsDomainUserLoginInfoServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDomainUserLoginInfoServ));

            long lngRes = 0;
            try
            {
                string strXML = "<Employee EmployeeID='" + p_objDomainUserLoginInfo.strEmployeeID + "' " +
                    "LoginDateTime=''  " +
                    "IPAddress='" + p_objDomainUserLoginInfo.strIPAddress + "' " +
                    "ComputerName='" + p_objDomainUserLoginInfo.strComputerName + "' " +
                    "LeaveDateTime='1900-1-1' />";

                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngAddDomainUserLoginInfo(strXML, out p_strDateTimeNow);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        public long m_lngModifyDomainUserLoginInfo(string p_strEmployeeID, string p_strLoginDateTime, string p_strIPAddress)
        {
            //clsDomainUserLoginInfoServ m_objServ =
            //    (clsDomainUserLoginInfoServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsDomainUserLoginInfoServ));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngModifyDomainUserLoginInfo(p_strEmployeeID, p_strLoginDateTime, p_strIPAddress);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }
    }

    /// <summary>
    /// �û���½��Ϣ����,��Ӧ���ݿ���DomainUserLoginInfo���е�����
    /// </summary>
    public class clsDomainUserLoginInfo
    {
        public string strEmployeeID;//�û�id
        public string strLoginDateTime;//��½ʱ��
        public string strIPAddress;//ip��ַ
        public string strComputerName;//���������
                                      //public string strLeaveDateTime;//�뿪ʱ��
    }
}
