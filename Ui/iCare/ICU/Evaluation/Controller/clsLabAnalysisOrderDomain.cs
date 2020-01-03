using System;
using weCare.Core.Entity;

namespace iCare.ICU.Evaluation
{
    /// <summary>
    /// Summary description for clsLabAnalysisOrderDomain.
    /// </summary>
    public class clsLabAnalysisOrderDomain
    {
        public clsLabAnalysisOrderDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            //m_objServ = new com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService();
        }

        //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDateArr"></param>
        /// <param name="p_strOpenDateArr"></param>
        /// <returns></returns>
        public long m_lngGetRecordTimeList(string p_strInPatientID, string p_strInPatientDate, out string[] p_strCreateDateArr, out string[] p_strOpenDateArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.clsLabAnalysisOrderService_m_lngGetRecordTimeList(p_strInPatientID, p_strInPatientDate, out p_strCreateDateArr, out p_strOpenDateArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        public long m_lngGetSendTimeAndBarCodeList(string p_strInPatientID, string p_strInPatientDate, out string[] p_strSendDateArr, out string[] p_strBarCodeArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetSendTimeAndBarCodeList(p_strInPatientID, p_strInPatientDate, out p_strSendDateArr, out p_strBarCodeArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        public long m_lngAddNewRecord2DB(clsLabCheckOrderContent p_objRecordContent)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngAddNewRecord2DB(p_objRecordContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        public long m_lngGetRecordContentWithServ(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsLabCheckOrderContent p_objRecordContent)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetRecordContentWithServ(p_strInPatientID, p_strInPatientDate, p_strCreateDate, out p_objRecordContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        public long m_lngDeleteRecord2DB(clsLabCheckOrderContent p_objRecordContent)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngDeleteRecord2DB(p_objRecordContent);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <returns></returns>
        public long m_lngGetMaxBarCode(out string p_strBarCode)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetMaxBarCode(out p_strBarCode);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strBarCodeArr"></param>
        /// <returns></returns>
        public long m_lngGetBarCodeList(string p_strBarCode, out string[] p_strBarCodeArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetBarCodeList(p_strBarCode, out p_strBarCodeArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 获取所有的检验号
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strBarCodeArr"></param>
        /// <returns></returns>
        public long m_lngGetBarCodeList_Pat_ID(string p_strBarCode, out string[] p_strBarCodeArr, out string[] p_strDeptNameArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetBarCodeList_Pat_ID(p_strBarCode, out p_strBarCodeArr, out p_strDeptNameArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_objPatient"></param>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objQXResultArr"></param>
        /// <param name="p_objDYResultArr"></param>
        /// <returns></returns>
        public long m_lngGetReportInfomation(string p_strBarCode, out clsJY_BRZL p_objPatient, out clsJY_JG[] p_objResultArr, out clsJY_QXJG[] p_objQXResultArr, out clsJY_DYJG[] p_objDYResultArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetReportInfomation(p_strBarCode, out p_objPatient, out p_objResultArr, out p_objQXResultArr, out p_objDYResultArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 得到所有的检验结果的别名
        /// </summary>
        /// <param name="p_strAliasArr"></param>
        /// <returns></returns>
        public long m_lngGetAllLabCheckAlias(out clsPublicIDAndName[] p_strAliasArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetAllLabCheckAlias(out p_strAliasArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 根据病历号获取检验项目
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_objPatientArr"></param>
        /// <returns></returns>
        public long m_lngGetCheckItem(string p_strInPatientID, out clsJY_BRZL[] p_objPatientArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetCheckItem(p_strInPatientID, out p_objPatientArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 根据检验号获取所有检验结果
        /// </summary>
        /// <param name="p_strPatID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetCheckResult(string p_strPatID, out clsJY_JG[] p_objResultArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetCheckResult(p_strPatID, out p_objResultArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 根据检验号获取所有描述结果
        /// </summary>
        /// <param name="p_strPatID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetJY_MSJG(string p_strPatID, out clsJY_MSJG[] p_objResultArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetJY_MSJG(p_strPatID, out p_objResultArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 根据检验号获取所有药敏结果
        /// </summary>
        /// <param name="p_strPatID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetJY_YMJG(string p_strPatID, out clsJY_YMJG[] p_objResultArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetJY_YMJG(p_strPatID, out p_objResultArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        /// <summary>
        /// 获取所有检验项目的结果
        /// </summary>
        /// <param name="p_strInPatientID">病人住院号</param>
        /// <param name="p_strSendDate">送检日期</param>
        /// <param name="p_strItemNameArr">检验报告中的子项目的名称</param>
        /// <param name="p_objRecordContentArr"></param>
        /// <returns></returns>
        public long m_lngGetLabCheckItemResultArr(string p_strInPatientID, string p_strSendDate, string[] p_strItemNameArr,
            out clsJY_JG[] p_objRecordContentArr)
        {
            p_objRecordContentArr = null;
            return 0;
        }

        /// <summary>
        /// 获取所有检验项目的结果
        /// </summary>
        /// <param name="p_strInPatientID">病人住院号</param>
        /// <param name="p_strSendDate">送检日期</param>
        /// <param name="p_strItemNameArr">检验报告中的子项目的名称</param>
        /// <param name="p_objRecordContentArr"></param>
        /// <returns></returns>
        public long m_lngGetLabCheckItemResultArr(string p_strResID, string[] p_strItemNameArr,
            out clsJY_JG[] p_objRecordContentArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetLabCheckItemResultArr(p_strResID, p_strItemNameArr, out p_objRecordContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }

        public long m_lngGetLabCheckItemChoiceArr(string p_strInPatientID, DateTime p_dtmCreateDate,
            out clsJY_ItemChoice[] p_objRecordContentArr)
        {
            //com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService m_objServ =
            //    (com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.LabAnalysisOrderService.clsLabAnalysisOrderService));

            long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetLabCheckItemChoiceArr(p_strInPatientID, p_dtmCreateDate, out p_objRecordContentArr);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return m_lngRes;
        }
    }
}
