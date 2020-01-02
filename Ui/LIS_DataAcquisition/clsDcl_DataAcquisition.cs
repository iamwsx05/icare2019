using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.LIS
{
    public class clsDcl_DataAcquisition
    {
        #region 操作数据库

        #region 添加实验室检验结果
        /// <summary>
        /// 添加实验室检验结果, 多样本
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_blnMuiltySample">TRUE = 多样本</param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        public long m_lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            return (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(p_objResultArr, p_blnMuiltySample, out p_objOutResultArr);
            //p_objOutResultArr = null;
            //long lngRes = 0;
            //ArrayList m_arlResult = new ArrayList();
            //m_arlResult.AddRange(p_objResultArr);
            //ArrayList m_arOutResult = null;

            //com.digitalwave.iCare.middletier.LIS.clsLIS_Svc objLIS_Svc = (clsLIS_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsLIS_Svc));
            //lngRes = objLIS_Svc.lngAddLabResult(m_arlResult, out m_arOutResult);
            //if (lngRes > 0 && m_arOutResult != null && m_arOutResult.Count > 0)
            //{
            //    p_objOutResultArr = (clsLIS_Device_Test_ResultVO[])m_arOutResult.ToArray(typeof(clsLIS_Device_Test_ResultVO));
            //}
            //return lngRes;
        }
        #endregion

        #region 通过条码获得申请单检验项目对应的通道号
        /// <summary>
        /// 通过条码获得申请单检验项目对应的通道号
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strDeviceNOArr"></param>
        /// <returns></returns>
        public long m_lngQueryAppDeviceNOByBarCode(string p_strDeviceID, string p_strBarCode, out string[] p_strDeviceNOArr)
        {
            p_strDeviceNOArr = null;

            if (string.IsNullOrEmpty(p_strBarCode) || string.IsNullOrEmpty(p_strDeviceID))
                return 0;

            return (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryAppDeviceNOByBarCode(p_strDeviceID, p_strBarCode, out p_strDeviceNOArr);
        }

        #endregion

        #region 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表
        /// <summary>
        /// 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceNO"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <returns></returns>
        public long m_lngUpdateAppCheckNO(string p_strBarCode, string p_strDeviceID, string p_strDeviceNO, string p_strDeviceSampleID)
        {
            return (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateAppCheckNO(p_strBarCode, p_strDeviceID, p_strDeviceNO, p_strDeviceSampleID);
        }
        #endregion

        #region 通过条码获得申请单信息
        /// <summary>
        /// 通过条码获得申请单信息
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_objAppMainVO"></param>
        /// <returns></returns>
        public long m_lngGetAppInfoByBarCode(string p_strBarCode, out clsLisApplMainVO p_objAppMainVO)
        {
            return (new weCare.Proxy.ProxyLis03()).Service.m_lngGetAppInfoByBarCode(p_strBarCode, out p_objAppMainVO);
        }

        #endregion

        /// <summary>
        /// 通过条码获得申请单信息与检验项目对应的通道号
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_objAppMainVO"></param>
        /// <param name="p_strDeviceNOArr"></param>
        /// <returns></returns>
        public long m_lngQueryAppInfoAndDeviceNO(string p_strDeviceID, string p_strBarCode, out clsLisApplMainVO p_objAppMainVO, out string[] p_strDeviceNOArr)
        {
            return (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryAppInfoAndDeviceNO(p_strDeviceID, p_strBarCode, out p_objAppMainVO, out p_strDeviceNOArr);
        }


        #endregion

        #region 通过条码获取病人相关信息
        public long m_lngGetPatientInfoByCardID(string p_strBarCode_vchr, out clsT_OPR_LIS_SAMPLE_VO p_objSampleVO)
        {
            return (new weCare.Proxy.ProxyLis03()).Service.clsLIS_QueryDataAcquisitionServ_m_lngQueryPatientInfo(p_strBarCode_vchr, out p_objSampleVO);
        }
        #endregion


        #region 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表 用于双工自动绑定检验编号 yongchao.li 2012-03-20
        public long m_lngUpdateAppCheckSampleNO(string p_strBarCode, string p_strDeviceID, string p_strDeviceNO, string p_strDeviceSampleID)
        {
            return (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateAppCheckSampleNO(p_strBarCode, p_strDeviceID, p_strDeviceNO, p_strDeviceSampleID);
        }
        #endregion


        #region 通过条码号获取仪器检验序号
        /// <summary>
        /// 通过条码号获取仪器检验序号
        /// </summary>
        /// <param name="p_strBarcode"></param>
        /// <param name="p_strCheckNO"></param>
        /// <returns></returns>
        public long m_lngGetCheckNOByBarcode(string p_strBarcode, out string p_strCheckNO)
        {
            return (new weCare.Proxy.ProxyLis03()).Service.m_lngGetCheckNOByBarcode(p_strBarcode, out p_strCheckNO);
        }
        #endregion


    }
}
