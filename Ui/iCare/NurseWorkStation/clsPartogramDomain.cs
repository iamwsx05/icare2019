using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace iCare
{
    public class clsPartogramDomain
    {
        /// <summary>
        /// �������¼
        /// </summary>
        /// <param name="p_objMain"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngAddNewMain(clsPartogramMain_VO p_objMain, clsPartogramContent_VO p_objContent)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNewMain( p_objMain, p_objContent);
            return lngRes;
        }
        /// <summary>
        /// �޸�����¼
        /// </summary>
        /// <param name="p_objMain"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngModifyMain(clsPartogramMain_VO p_objMain, clsPartogramContent_VO p_objContent)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngModifyMain(  p_objMain, p_objContent);
            return lngRes;
        }
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <returns></returns>
        public long m_lngDeleteMain(string p_strRegisterId, DateTime p_dtmCreatedDate)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngDeleteMain(  p_strRegisterId, p_dtmCreatedDate, clsEMRLogin.LoginInfo.m_strEmpID);
            return lngRes;
        }
        /// <summary>
        /// ��ȡȫ����¼
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngGetValues(string p_strRegisterId, out clsPartogramAll_VO p_objContent)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetValues( p_strRegisterId, out p_objContent);
            return lngRes;
        }
        /// <summary>
        /// ��ȡȫ����Сʱ��¼
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        public long m_lngGetAllHourValues(string p_strRegisterId, out clsPartogram_VO[] p_objContentArr)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetAllHourValues(  p_strRegisterId, out p_objContentArr);
            return lngRes;
        }
        /// <summary>
        /// ����һ��Сʱ�ļ�¼
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <param name="p_intSelectedHour"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        public long m_lngGetOneHourValues(string p_strRegisterId, int p_intSelectedHour, out clsPartogram_VO p_objContent)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetOneHourValues(  p_strRegisterId, p_intSelectedHour, out p_objContent);
            return lngRes;
        }
        /// <summary>
        /// ����һ��Сʱ�ĵ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_intSelectedHour"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        public long m_lngGetOneHourPointValues(string p_strRegisterId, int p_intSelectedHour, out clsPartogram_Point[] p_objContentArr)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetOneHourPointValues(  p_strRegisterId, p_intSelectedHour, out p_objContentArr);
            return lngRes;
        }
        /// <summary>
        /// ���һ��Сʱ�ļ�¼
        /// </summary>
        /// <param name="p_objSub"></param>
        /// <returns></returns>
        public long m_lngAddNewSub(clsPartogram_VO p_objSub)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNewSub(  p_objSub);
            return lngRes;
        }
        /// <summary>
        /// �޸�һ��Сʱ�ļ�¼
        /// </summary>
        /// <param name="p_objSub"></param>
        /// <returns></returns>
        public long m_lngModifySub(clsPartogram_VO p_objSub, int p_intPartogarm)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngModifySub(  p_objSub, p_intPartogarm);
            return lngRes;
        }
        /// <summary>
        /// ɾ��һ��Сʱ�ļ�¼
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <param name="p_intHour"></param>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_dtmDeactiveDate"></param>
        /// <returns></returns>
        public long m_lngDeleteHour(string p_strRegisterId, DateTime p_dtmCreatedDate, int p_intHour, string p_strEmpId, DateTime p_dtmDeactiveDate)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngDeleteHour(  p_strRegisterId, p_dtmCreatedDate, p_intHour, p_strEmpId, p_dtmDeactiveDate);
            return lngRes;
        }
        /// <summary>
        /// ��һ����¼������
        /// m_intSTATUS_INT ��0Ϊɾ��
        /// m_intSTATUS_INT �� 1 Ϊ����
        /// m_intSTATUS_INT  �� 2 Ϊ�޸ģ�����������ӣ�
        /// </summary>
        /// <param name="p_objPointArr"></param>
        /// <returns></returns>
        public long m_lngSetPointToDb(clsPartogram_Point[] p_objPointArr)
        {
            //clsPartogramService objServ =
            //    (clsPartogramService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPartogramService));

            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngSetPointToDb( clsEMRLogin.LoginInfo.m_strEmpID, p_objPointArr);
            return lngRes;
        }
    }
}
