using System;
using System.Collections;
using weCare.Core.Entity;



namespace iCare
{
    /// <summary>
    /// Summary description for clsNuclearOrderDomain.
    /// </summary>
    public class clsNuclearOrderDomain
    {
        //private clsNuclearOrderService m_objService;

        public clsNuclearOrderDomain()
        {
            //m_objService=new clsNuclearOrderService();
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ���ݲ���ID����һ�����Զർ˯��ͼ������뵥��������
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_objEKGOrderArr">������뵥�������飬out�͵�</param>
        /// <returns>���ض���ĸ���</returns>
        public string[] lngGetNuclearOrderArrByPatientID(string p_strPatientID, string p_strPatientDate)
        {
            //clsNuclearOrderService m_objService =
            //    (clsNuclearOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsNuclearOrderService));

            string[] m_strRe = new String[0];
            try
            {
                (new weCare.Proxy.ProxyEmr03()).Service.clsNuclearOrderService_m_lngGetTimeInfoOfAPatient(p_strPatientID, p_strPatientDate, ref m_strRe);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return m_strRe;


        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="p_objEKGOrderArr">Ҫ����Ķ���</param>
        /// <returns>����1��ʾ����ɹ�������0��ʾ����ʧ��</returns>
        public long lngSave(bool p_bnlIsNew, clsNuclearOrder p_objNuclearOrder)
        {
            //clsNuclearOrderService m_objService =
            //    (clsNuclearOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsNuclearOrderService));

            long lngRes = 0;
            try
            {
                if (p_bnlIsNew == true)
                    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngUpdateNuclearOrder(p_objNuclearOrder, enmUpdateAction.enmAddNew);
                else
                    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngUpdateNuclearOrder(p_objNuclearOrder, enmUpdateAction.enmEdit);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
        }

        public long lngDelete(clsNuclearOrder p_objNuclearOrder)
        {
            //clsNuclearOrderService m_objService =
            //    (clsNuclearOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsNuclearOrderService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngUpdateNuclearOrder(p_objNuclearOrder, enmUpdateAction.enmDelete);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
        }

        public long GetNuclearOrder(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsNuclearOrder p_objNuclearOrder)
        {
            //clsNuclearOrderService m_objService =
            //    (clsNuclearOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsNuclearOrderService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetNuclearOrder(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_objNuclearOrder);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
        }
    }



}
