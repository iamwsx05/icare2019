using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// 医嘱系统-检查申请单
    /// </summary>
    internal class clsEMR_HIS_CheckRequisitionDomain
    {
        #region 获取检查申请单内容
        /// <summary>
        /// 获取检查申请单内容
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strOrderID">医嘱流水号</param>
        /// <param name="p_objValue">申请单内容</param>
        /// <returns></returns>
        internal long m_lngGetCheckRequisitionValue(string p_strRegisterID, string p_strOrderID, out clsEMR_HIS_CheckRequisitionValue p_objValue)
        {
            p_objValue = null;
            //clsEMR_HIS_CheckRequisitionServ objServ =
            //    (clsEMR_HIS_CheckRequisitionServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_HIS_CheckRequisitionServ));

            long lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngGetCheckRequisitionValue(  p_strRegisterID, p_strOrderID, out p_objValue);
            //objServ = null;
            return lngRes;
        }

        /// <summary>
        /// 获取检查申请单内容
        /// </summary>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_objValueArr">申请单内容</param>
        /// <returns></returns>
        internal long m_lngGetCheckRequisitionValue(string p_strRegisterID, out clsEMR_HIS_CheckRequisitionValue[] p_objValueArr)
        {
            p_objValueArr = null;
            //clsEMR_HIS_CheckRequisitionServ objServ =
            //    (clsEMR_HIS_CheckRequisitionServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_HIS_CheckRequisitionServ));

            long lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngGetCheckRequisitionValue( p_strRegisterID, out p_objValueArr);
            //objServ = null;
            return lngRes;
        }
        #endregion

        #region 添加申请
        /// <summary>
        /// 添加申请
        /// </summary>
        /// <param name="p_objValue">申请单内容</param>
        /// <returns></returns>
        internal long m_lngAddNewRequisition(clsEMR_HIS_CheckRequisitionValue p_objValue)
        {
            //clsEMR_HIS_CheckRequisitionServ objServ =
            //    (clsEMR_HIS_CheckRequisitionServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_HIS_CheckRequisitionServ));

            long lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngAddNewRequisition(  p_objValue);
            //objServ = null;
            return lngRes;
        }
        #endregion

        #region 修改申请内容
        /// <summary>
        /// 修改申请内容
        /// </summary>
        /// <param name="p_objValue">申请单内容</param>
        /// <returns></returns>
        internal long m_lngModifyRequisition(clsEMR_HIS_CheckRequisitionValue p_objValue)
        {
            //clsEMR_HIS_CheckRequisitionServ objServ =
            //    (clsEMR_HIS_CheckRequisitionServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_HIS_CheckRequisitionServ));

            long lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngModifyRequisition( p_objValue);
            //objServ = null;
            return lngRes;
        }
        #endregion

        #region 删除申请
        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="p_strDeactivedUser">删除者ID</param>
        /// <param name="p_strRegisterID">入院登记流水号</param>
        /// <param name="p_strOrderID">医嘱流水号</param>
        /// <returns></returns>
        internal long m_lngDeleteRequisition(string p_strDeactivedUser, string p_strRegisterID, string p_strOrderID)
        {
            //clsEMR_HIS_CheckRequisitionServ objServ =
            //    (clsEMR_HIS_CheckRequisitionServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_HIS_CheckRequisitionServ));

            long lngRes = (new weCare.Proxy.ProxyEmr01()).Service.m_lngDeleteRequisition(  p_strDeactivedUser, p_strRegisterID, p_strOrderID);
            //objServ = null;
            return lngRes;
        }
        #endregion
    }
}
