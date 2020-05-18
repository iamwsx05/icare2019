using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 病案同步
    /// </summary>
    public class clsEMR_SynchronousCaseDomain : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region 获取同步科室列表
        /// <summary>
        /// 获取同步科室列表
        /// </summary>
        /// <param name="p_objDeptArr">科室列表</param>
        /// <returns></returns>
        public long m_lngGetSynchronousDeptList(out clsEmrDept_VO[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ objServ =
            //       (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetSynchronousDeptList(out p_objDeptArr);
            return lngRes;
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="p_dtmStartDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbResult">结果</param>
        /// <returns></returns>
        public long m_lngGetSynchronousData(DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ objServ =
            //       (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));

            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3017") == 0)//专科组
            {
                lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetCaseData(p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            }
            else
            {
                lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetCaseData_dept(p_dtmStartDate, p_dtmEndDate, out p_dtbResult);//科室
            }
            return lngRes;
        }
        #endregion

        #region 同步费用
        /// <summary>
        /// 同步费用
        /// </summary>
        /// <param name="p_strRegisterID">病人入院登记号</param>
        /// <param name="p_strPatientID">病人ID</param>
        /// <returns></returns>
        private long m_lngSynchronousCalcHISCharge(string p_strRegisterID, string p_strPatientID)
        {
            long lngRes = 0;

            //clsCaseHisChargeStatSvc objServ =
            //       (clsCaseHisChargeStatSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCaseHisChargeStatSvc));

            lngRes = (new weCare.Proxy.ProxyBase()).Service.m_lngSynchronizeCalcHisCharge(p_strRegisterID, p_strPatientID);
            return lngRes;
        }
        #endregion

        #region 获取费用信息

        /// <summary>
        /// 获取自付金额
        /// </summary>
        /// <param name="p_strInpatientID"></param>
        /// <param name="p_dtmInhospitalDate"></param>
        /// <param name="p_objRecordcontent"></param>
        /// <returns></returns>
        public long m_lngGetSelfPay(string p_strRegisterID,
           out clsInHospitalMainRecord_Content p_objRecordcontent)
        {
            long lngRes = 0;
            //clsEMR_SynchronousCaseServ objServ =
            //           (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));

            lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetSelfPay(p_strRegisterID, out p_objRecordcontent);
            return lngRes;
        }
        /// <summary>
        /// 获取费用信息
        /// </summary>
        /// <param name="p_strRegisterID">病人入院登记号</param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngGetCHRCATE(string p_strRegisterID, string p_strPatientID, out clsInHospitalMainCharge[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;

            lngRes = m_lngSynchronousCalcHISCharge(p_strRegisterID, p_strPatientID);
            if (lngRes > 0)
            {
                //clsEMR_SynchronousCaseServ objServ =
                //       (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));

                lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetCHRCATE(p_strRegisterID, out p_objRecordArr);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取费用信息
        /// </summary>
        /// <param name="p_strRegisterID">病人入院登记号</param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngGetChargeChanKe(string p_strRegisterID, DataTable p_strbbRegisterID, out clsInHospitalMainCharge[] p_objRecordArr)
        {
            long lngRes = 0;
            p_objRecordArr = null;
            //clsEMR_SynchronousCaseServ objServ =
            //       (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));

            lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetChargeChanKe(p_strRegisterID, p_strbbRegisterID, out p_objRecordArr);
            return lngRes;
        }
        /// <summary>
        /// 关联产妇住院号获取婴儿流水号
        /// </summary>
        /// <param name="p_strInpatientID"></param>
        /// <returns></returns>
        public DataTable m_lngGetRgisterIDByInpatientID(string p_strInpatientID)
        {
            //clsEMR_SynchronousCaseServ objServ =
            //           (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));
            return (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetRgisterIDByInpatientID(p_strInpatientID);
        }
        #endregion

        #region 同步病案资料
        /// <summary>
        /// 同步病案资料
        /// </summary>
        /// <param name="p_objValue">病案内容</param>
        /// <returns></returns>
        public long m_lngCommitToBATemp(clsEMR_SynchronousCaseValue p_objValue)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ objServ =
            //       (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCommitToBATemp(p_objValue);
            return lngRes;
        }
        #endregion

        #region 检查指定病人的病案资料是否已同步

        /// <summary>
        /// 检查指定病人的病案资料是否已同步

        /// </summary>
        /// <param name="p_strPRN">病案号</param>
        /// <param name="p_strTimes">入院次数</param>
        /// <param name="p_blnHasSynchronous">是否已同步</param>
        /// <returns></returns>
        public long m_lngCheckHasSynchronous(string p_strPRN, string p_strTimes, out bool p_blnHasSynchronous)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ objServ =
            //       (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngCheckHasSynchronous(p_strPRN, p_strTimes, out p_blnHasSynchronous);
            return lngRes;
        }
        #endregion

        #region 获取已同步病人

        /// <summary>
        /// 获取已同步病人

        /// </summary>
        /// <param name="p_dtmStartDate">查询开始时间</param>
        /// <param name="p_dtmEndDate">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        public long m_lngGetHasSynchronousPatien(DateTime p_dtmStartDate, DateTime p_dtmEndDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;

            //clsEMR_SynchronousCaseServ objServ =
            //       (clsEMR_SynchronousCaseServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_SynchronousCaseServ));

            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetHasSynchronousPatien(p_dtmStartDate, p_dtmEndDate, out p_dtbResult);
            return lngRes;
        }
        #endregion
    }
}
