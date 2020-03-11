using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// ����ͨѶ����Domain��
    /// </summary>
    public class clsDomainController_MK3DeviceCommunications : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡ�Զ�����Ŀ�ķ�������
        /// <summary>
        /// ��ȡ�Զ�����Ŀ�ķ�������
        /// </summary>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objCheckItemCustomOrderArr"></param>
        /// <returns></returns>
        public long m_lngQueryChcekItemCustomOrder(string p_strCheckItemID, out clsLisCheckItemCustomOrder p_objCheckItemCustomOrder)
        {
            p_objCheckItemCustomOrder = null;
            long lngRes = 0;
            //clsMK3DeviceCommunications objSvc =
            //    (clsMK3DeviceCommunications)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMK3DeviceCommunications));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryChcekItemCustomOrder(p_strCheckItemID, out p_objCheckItemCustomOrder);
            return lngRes;
        }
        #endregion

        #region �޸��Զ�����Ŀ���͸�����������
        /// <summary>
        /// �޸��Զ�����Ŀ���͸�����������
        /// </summary>
        /// <param name="p_objCheckItemCustomOrderVO"></param>
        /// <returns></returns>
        public long m_lngUpdateChcekItemCustomOrder(clsLisCheckItemCustomOrder p_objCheckItemCustomOrderVO)
        {
            long lngRes = 0;
            //clsMK3DeviceCommunications objSvc =
            //    (clsMK3DeviceCommunications)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMK3DeviceCommunications));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateChcekItemCustomOrder(p_objCheckItemCustomOrderVO);
            return lngRes;
        }
        #endregion

        #region ����Զ�����Ŀ�ķ�������
        /// <summary>
        /// ����Զ�����Ŀ�ķ�������
        /// </summary>
        /// <param name="p_objCheckItemCustomOrderVO"></param>
        /// <returns></returns>
        public long m_lngInsertCheckItemCustomOrder(clsLisCheckItemCustomOrder p_objCheckItemCustomOrderVO)
        {
            long lngRes = 0;
            //clsMK3DeviceCommunications objSvc =
            //   (clsMK3DeviceCommunications)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMK3DeviceCommunications));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertCheckItemCustomOrder(p_objCheckItemCustomOrderVO);
            return lngRes;
        }
        #endregion

        #region ɾ���Զ�����Ŀ������
        /// <summary>
        /// ɾ���Զ�����Ŀ������
        /// </summary>
        /// <param name="p_strCheckItemID"></param>
        /// <returns></returns>
        public long m_lngDeleteCheckItemCustomOrder(string p_strCheckItemID)
        {
            long lngRes = 0;
            //clsMK3DeviceCommunications objSvc =
            //    (clsMK3DeviceCommunications)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMK3DeviceCommunications));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngDeleteCheckItemCustomOrder(p_strCheckItemID);
            return lngRes;
        }
        #endregion
    }
}
