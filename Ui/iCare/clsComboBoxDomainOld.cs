using System;
using System.Data;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsComboBoxDomain.
    /// </summary>
    public class clsComboBoxDomainOld
    {

        public clsComboBoxDomainOld()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ��ȡ������Ŀ
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strItemID"></param>
        /// <param name="p_objValueArr"></param>
        /// <returns></returns>
        public long m_lngGetAllItem(string p_strDeptID, string p_strTypeID, string p_strItemID, out clsComboBoxValue[] p_objValueArr)
        {
            p_objValueArr = null;

            //clsComboBoxService m_objService =
            //    (clsComboBoxService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsComboBoxService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.clsComboBoxService_m_lngGetAllItem(p_strDeptID, p_strTypeID, p_strItemID, out p_objValueArr);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ���һ��
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        public long m_lngAddItemToDB(clsComboBoxValue p_objValue)
        {
            //clsComboBoxService m_objService =
            //    (clsComboBoxService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsComboBoxService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.clsComboBoxService_m_lngAddItemToDB(p_objValue);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ɾ��һ��
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        public long m_lngDeleteItem(clsComboBoxValue p_objValue)
        {
            //clsComboBoxService m_objService =
            //    (clsComboBoxService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsComboBoxService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.clsComboBoxService_m_lngDeleteItem(p_objValue);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// �޸�һ��
        /// </summary>
        /// <param name="p_objOldValue"></param>
        /// <param name="p_strNewItemContent"></param>
        /// <returns></returns>
        public long m_lngModifyItem(clsComboBoxValue p_objOldValue, string p_strNewItemContent)
        {
            //clsComboBoxService m_objService =
            //    (clsComboBoxService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsComboBoxService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.clsComboBoxService_m_lngModifyItem(p_objOldValue, p_strNewItemContent);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
        }

    }
}
