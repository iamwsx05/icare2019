using com.digitalwave.iCare.middletier.LIS;
using System;
using System.Collections.Generic;
using System.Data;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Lis.Service
{
    public class SvcLis02 : Lis.Itf.ItfLis02
    {
        #region clsQueryLIS_Svc

        /// <summary>
        /// 获取仪器参数 
        /// </summary>
        /// <param name="strData_Acquisition_Computer_IP"></param>
        /// <param name="objConfig_List"></param>
        /// <returns></returns>
        public long lngGetInstrumentSerialSetting2(string strData_Acquisition_Computer_IP, out clsLIS_Equip_Base[] objConfig_List)
        {
            using (clsQueryLIS_Svc svc = new clsQueryLIS_Svc())
            {
                return svc.lngGetInstrumentSerialSetting2(strData_Acquisition_Computer_IP, out objConfig_List);
            }
        }

        /// <summary>
        /// 获取指定检验编号的检验项目通道号数组
        /// </summary>
        /// <param name="p_strCheckSampleNO"></param>
        /// <param name="p_strCheckItemstring"></param>
        /// <returns></returns>
        public long m_lngGetSampleCheckItems(string p_strCheckSampleNO, out string p_strCheckItemstring)
        {
            using (clsQueryLIS_Svc svc = new clsQueryLIS_Svc())
            {
                return svc.m_lngGetSampleCheckItems(p_strCheckSampleNO, out p_strCheckItemstring);
            }
        }

        /// <summary>
        /// GetOttomanAppItems
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public DataTable GetOttomanAppItems(string barCode)
        {
            using (clsQueryLIS_Svc svc = new clsQueryLIS_Svc())
            {
                return Function.ReNameDatatable(svc.GetOttomanAppItems(barCode));
            }
        }

        /// <summary>
        /// GetOttomanCheckResult
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public List<clsDeviceReslutVO> GetOttomanCheckResult(string barCode)
        {
            using (clsQueryLIS_Svc svc = new clsQueryLIS_Svc())
            {
                return svc.GetOttomanCheckResult(barCode);
            }
        }

        #endregion

        #region clsQueryReportGroupSvc

        // 根据报告组ID获取报告组VO
        public long m_lngGetReportGroupVOByReportGroupID(string p_strReportGroupID, out clsReportGroup_VO p_objResultVO)
        {
            using (clsQueryReportGroupSvc svc = new clsQueryReportGroupSvc())
            {
                return svc.m_lngGetReportGroupVOByReportGroupID(p_strReportGroupID, out p_objResultVO);
            }
        }

        /// <summary>
        /// 根据检验标本组ID得到它所在报告组的VO 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objSampleGroupVO"></param>
        /// <returns></returns>
        public long m_lngGetReportGoupVOBySampleGroupID(string p_strSampleGroupID, out clsReportGroup_VO p_objReportGroupVO)
        {
            using (clsQueryReportGroupSvc svc = new clsQueryReportGroupSvc())
            {
                return svc.m_lngGetReportGoupVOBySampleGroupID(p_strSampleGroupID, out p_objReportGroupVO);
            }
        }

        // 获取所有的报告组
        public long m_lngGetAllReportGroup(ref clsReportGroup_VO[] objReportGroupList)
        {
            using (clsQueryReportGroupSvc svc = new clsQueryReportGroupSvc())
            {
                return svc.m_lngGetAllReportGroup(ref objReportGroupList);
            }
        }

        #endregion

        #region clsQueryReportSvc

        // 根据条件组合查询报表源数据
        public long m_lngGetWorkloadReportByCondition(
            string p_strFromDat, string p_strToDat, string p_strCheckItemID, string p_strApplEmpID, string p_strApplDeptID,
            string p_strReprotorID, string p_strPatientTypeID, string p_strCheckCategoryID, out DataTable p_dtbResult)
        {
            using (clsQueryReportSvc svc = new clsQueryReportSvc())
            {
                long rec = svc.m_lngGetWorkloadReportByCondition(p_strFromDat, p_strToDat, p_strCheckItemID, p_strApplEmpID, p_strApplDeptID, p_strReprotorID, p_strPatientTypeID, p_strCheckCategoryID, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // Get
        public long m_lngGetReportObject(string p_strApplicationID, out clsReportObject p_objReportObject)
        {
            using (clsQueryReportSvc svc = new clsQueryReportSvc())
            {
                return svc.m_lngGetReportObject(p_strApplicationID, out p_objReportObject);
            }
        }

        #endregion

        #region clsQuerySampleBackSvc

        /// <summary>
        /// 获取样本反馈信息
        /// </summary>
        /// <param name="p_strFromDate">开始日期</param>
        /// <param name="p_strToDate">结束日期</param>
        /// <param name="p_strPatientName">病人姓名</param>
        /// <param name="p_strInHospitalNO">住院号</param>
        /// <param name="p_strAppDeptID">申请科室ID</param>
        /// <param name="p_dtResult">返回结果表</param>
        /// <returns>大于0成功，小于或等于0失败</returns>
        public long m_lngQuerySampleBack(string p_strFromDate, string p_strToDate, string p_strPatientName, string p_strInHospitalNO, string p_strAppDeptID, out DataTable p_dtResult)
        {
            using (clsQuerySampleBackSvc svc = new clsQuerySampleBackSvc())
            {
                long rec = svc.m_lngQuerySampleBack(p_strFromDate, p_strToDate, p_strPatientName, p_strInHospitalNO, p_strAppDeptID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        #endregion

        #region clsQuerySampleGroupSvc

        // 根据申请单元ID查询标本组与申请单元的关系
        public long m_lngGetSampleGroupUnitByApplUnitID(string p_strApplUnitID, out DataTable p_dtbResult)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                long rec = svc.m_lngGetSampleGroupUnitByApplUnitID(p_strApplUnitID, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        /// <summary>
        /// 根据样本组ID查询该样本组下包含的申请单元
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleGroupID">＝"" || =null为查询全部</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetApplUnitBySampleGroupID(string p_strSampleGroupID, out clsLisSampleGroupUnit_VO[] p_objResultArr)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                return svc.m_lngGetApplUnitBySampleGroupID(p_strSampleGroupID, out p_objResultArr);
            }
        }

        // 根据标本组ID获取该组的标本类型
        public long m_lngGetGroupSampleTypeBySampleGroupID(string p_strSampleGroupID, out clsLisGroupSampleType_VO[] p_objResultArr)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                return svc.m_lngGetGroupSampleTypeBySampleGroupID(p_strSampleGroupID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 根据样本组ID 得到样本组对应的仪器型号列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleGroupID"></param>
        /// <param name="p_strSampleGroupModelArr"></param>
        /// <returns></returns>
        public long m_lngGetSampleGroupModelArr(string p_strSampleGroupID, out string[] p_strSampleGroupModelArr)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                return svc.m_lngGetSampleGroupModelArr(p_strSampleGroupID, out p_strSampleGroupModelArr);
            }
        }

        // 根据标本组ID获取对应的仪器型号列表
        public long m_lngGetDeviceModelArrBySampleGroupID(string p_strSampleGroupID, out clsLisSampleGroupModel_VO[] p_objResultArr)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                return svc.m_lngGetDeviceModelArrBySampleGroupID(p_strSampleGroupID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 得到样本组的列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCategory"></param>
        /// <param name="p_strSampleType"></param>
        /// <param name="p_dtpResult"></param>
        /// <returns></returns>
        public long m_lngGetSampleGroupList(string p_strCategory, string p_strSampleType, out DataTable p_dtpResult)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                long rec = svc.m_lngGetSampleGroupList(p_strCategory, p_strSampleType, out p_dtpResult);
                p_dtpResult = Function.ReNameDatatable(p_dtpResult);
                return rec;
            }
        }

        // 根据标本ID获取标本组VO
        public long m_lngGetSampleGroupVOBySampleGroupID(string p_strSampleGroupID, out clsSampleGroup_VO p_objResultVO)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                return svc.m_lngGetSampleGroupVOBySampleGroupID(p_strSampleGroupID, out p_objResultVO);
            }
        }

        /// <summary>
        /// 根据检验项目ID得到它所在标本组的VO,及打印顺序  
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_intPrintSeq"></param>
        /// <param name="p_objSampleGroupVO"></param>
        /// <returns></returns>
        public long m_lngGetSampleGoupVOByApplyUnitID(string p_strApplyUnitID, out clsSampleGroup_VO p_objSampleGroupVO)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                return svc.m_lngGetSampleGoupVOByApplyUnitID(p_strApplyUnitID, out p_objSampleGroupVO);
            }
        }

        // 获取某一标本组下的明细资料
        public long m_lngGetAllSampleGroupDetail(string strSampleGroupID, ref clsSampleGroupDetail_VO[] objSampleGroupDetailVOList)
        {
            using (clsQuerySampleGroupSvc svc = new clsQuerySampleGroupSvc())
            {
                return svc.m_lngGetAllSampleGroupDetail(strSampleGroupID, ref objSampleGroupDetailVOList);
            }
        }

        #endregion

        #region clsQueryCheckGroupSvc

        // 获取所有的标本组
        public long m_lngGetAllSampleGroup(ref clsSampleGroup_VO[] objSampleGroupVOList)
        {
            using (clsQueryCheckGroupSvc svc = new clsQueryCheckGroupSvc())
            {
                return svc.m_lngGetAllSampleGroup(ref objSampleGroupVOList);
            }
        }

        #endregion

        #region clsQuerySampleSvc

        //	根据标本号查询标本状态
        public long m_lngFindStatusBySampleID(string p_strSampleID, out int p_intStatus)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngFindStatusBySampleID(p_strSampleID, out p_intStatus);
            }
        }

        /// <summary>
        /// 根据BarCode查询待接收的样本信息
        /// </summary>
        public long m_mthGetUnReceivedSampleByBarCode(string p_strBarCode, out clsSampleReceive_VO p_objRecord)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_mthGetUnReceivedSampleByBarCode(p_strBarCode, out p_objRecord);
            }
        }

        // 根据条件查询已接收的标本信息
        public long m_lngGetReceivedSampleByCondition(string p_strDatFrom, string p_strDatTo,
            string p_strSampleType, string p_strAcceptEmp, string p_strPatientName, string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum, out clsSampleReceive_VO[] p_objResultArr)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetReceivedSampleByCondition(p_strDatFrom, p_strDatTo, p_strSampleType, p_strAcceptEmp, p_strPatientName, p_strPatientCardID, p_strBarCode, p_strCheckCategory, p_strSendPeopleID, p_strInPatientNum, out p_objResultArr);
            }
        }

        // 根据条件查询已采集但未接收的标本信息
        public long m_lngGetUnReceivedSampleByCondition(string p_strDatFrom,
            string p_strDatTo, string p_strSampleType, string p_strConlectEmp, string p_strPatientName,
            string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum, out clsSampleUnReceive_VO[] p_objResultArr)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetUnReceivedSampleByCondition(p_strDatFrom, p_strDatTo, p_strSampleType, p_strConlectEmp, p_strPatientName, p_strPatientCardID, p_strBarCode, p_strCheckCategory, p_strSendPeopleID, p_strInPatientNum, out p_objResultArr);
            }
        }

        /// <summary>
        /// 返回自定义组所有申请单元 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckCategory"></param>
        /// <param name="p_dtbDetail"></param>
        /// <returns></returns>
        public long m_lngGetAppuserGroupDetail(string p_strCheckCategory, out DataTable p_dtbDetail)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetAppuserGroupDetail(p_strCheckCategory, out p_dtbDetail);
                p_dtbDetail = Function.ReNameDatatable(p_dtbDetail);
                return rec;
            }
        }

        // 根据仪器ID查询插队记录
        public long m_lngGetSampleInterposeByDeviceID(string p_strDeviceID, out clsLisSampleInterposeVO p_objResult)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetSampleInterposeByDeviceID(p_strDeviceID, out p_objResult);
            }
        }

        // 根据条件查询仪器与样本之间的关系
        public long m_lngGetDeviceRelationVOArrByCondition(string p_strDeviceID, string p_strReceptDatFrom, string p_strReceptDatTo, out clsT_LIS_DeviceRelationVO[] p_objResultArr)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetDeviceRelationVOArrByCondition(p_strDeviceID, p_strReceptDatFrom, p_strReceptDatTo, out p_objResultArr);
            }
        }

        // 根据标本的BarCode查询相应的标本及标本组信息
        public long m_lngGetSampleInfoByBarCode(string p_strBarCode, out DataTable p_dtbResult)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetSampleInfoByBarCode(p_strBarCode, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 获取所有的标本类型信息
        public long m_lngGetSampleTypeArr(out clsSampleType_VO[] p_objResultArr)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetSampleTypeArr(out p_objResultArr);
            }
        }

        // 根据标本ID查询标本和仪器标本的关系VO
        public long m_lngGetDeviceRelationVOArrBySampleID(string p_strSampleID, out clsT_LIS_DeviceRelationVO[] p_objResultArr)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetDeviceRelationVOArrBySampleID(p_strSampleID, out p_objResultArr);
            }
        }

        // 根据标本ID查询标本信息
        public long m_lngGetSampleVOArrBySampleID(string p_strSampleID, out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetSampleVOArrBySampleID(p_strSampleID, out p_objResultArr);
            }
        }

        // 根据BarCode 得到样本VO
        public long m_lngGetSampleVOByBarcode(string p_strBarCode, out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetSampleVOByBarcode(p_strBarCode, out p_objResultArr);
            }
        }

        // 获得全部的样品种类的列表
        public long m_lngGetSampleTypeList(out System.Data.DataTable p_dtbSampleType)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetSampleTypeList(out p_dtbSampleType);
                p_dtbSampleType = Function.ReNameDatatable(p_dtbSampleType);
                return rec;
            }
        }

        public long m_lngGetCheckCategoryList(out System.Data.DataTable p_dtbCheckCategory)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetCheckCategoryList(out p_dtbCheckCategory);
                p_dtbCheckCategory = Function.ReNameDatatable(p_dtbCheckCategory);
                return rec;
            }
        }

        // 得到所有的样本状态信息列表
        public long m_lngGetSampleState(out System.Data.DataTable p_dtbSampleState)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetSampleState(out p_dtbSampleState);
                p_dtbSampleState = Function.ReNameDatatable(p_dtbSampleState);
                return rec;
            }
        }

        /// <summary>
        /// 根据样品类别ID查询样本状态信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleTypeID"></param>
        /// <param name="p_dtbSampleState"> 
        /// </param>
        /// <returns></returns>
        public long m_lngGetSampleState(string p_strSampleTypeID, out System.Data.DataTable p_dtbSampleState)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetSampleState(p_strSampleTypeID, out p_dtbSampleState);
                p_dtbSampleState = Function.ReNameDatatable(p_dtbSampleState);
                return rec;
            }
        }

        // 根据BarCode判断该标本是否已经核收
        public long m_lngGetReceptedSampleInfoByBarCode(string p_strBarCode, out clsT_OPR_LIS_SAMPLE_VO p_objRecord)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetReceptedSampleInfoByBarCode(p_strBarCode, out p_objRecord);
            }
        }

        // 查询在某段时间内采集且已申请但未核收的标本 童华
        public long m_lngGetAllNotReceptSample(string p_strFromDat, string p_strToDat, out DataTable p_dtbResult)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetAllNotReceptSample(p_strFromDat, p_strToDat, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 根据日期范围查询已核收的标本
        public long m_lngGetReceptedSampleByDateRange(string p_strDeviceID, string p_strFromDat, string p_strToDat, out DataTable p_dtbResult)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetReceptedSampleByDateRange(p_strDeviceID, p_strFromDat, p_strToDat, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        /// <summary>
        /// 查询所有未核收的标本（含未审请的） 
        /// </summary>
        /// <param name="p_objPrincipal"></param>		
        /// <param name="dtbAllNotReceptSample"></param>
        /// <returns></returns>
        public long m_lngGetAllNotReceptSample(out DataTable dtbAllNotReceptSample)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetAllNotReceptSample(out dtbAllNotReceptSample);
                dtbAllNotReceptSample = Function.ReNameDatatable(dtbAllNotReceptSample);
                return rec;
            }
        }

        // 根据检验申请表号和检验组（第一层）查询已经采集的各标本数量
        public long m_lngGetAllSampleCountByApplFormNoAndGroupID(string strApplFormNo, string strGroupID, out DataTable dtbGroupSampleCount)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetAllSampleCountByApplFormNoAndGroupID(strApplFormNo, strGroupID, out dtbGroupSampleCount);
                dtbGroupSampleCount = Function.ReNameDatatable(dtbGroupSampleCount);
                return rec;
            }
        }

        // 查询t_opr_lis_application_detail下的Group的标本状态
        public long m_lngGetSampleStatusByGroup(string strSampleID, string strApplFormNo, out DataTable dtbGroupSample)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetSampleStatusByGroup(strSampleID, strApplFormNo, out dtbGroupSample);
                dtbGroupSample = Function.ReNameDatatable(dtbGroupSample);
                return rec;
            }
        }

        // 根据检验申请表号查询已经采集的各标本数量 
        public long m_lngGetAllSampleCountByApplFormNo(string strApplFormNo, out DataTable dtbAllSampleCount)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetAllSampleCountByApplFormNo(strApplFormNo, out dtbAllSampleCount);
                dtbAllSampleCount = Function.ReNameDatatable(dtbAllSampleCount);
                return rec;
            }
        }

        // 根据检验申请表上的号查出本申请已经有的样品。
        public long m_lngGetSampleInfoByFormId(string strFormNo, out System.Data.DataTable dtbSampleInfo)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetSampleInfoByFormId(strFormNo, out dtbSampleInfo);
                dtbSampleInfo = Function.ReNameDatatable(dtbSampleInfo);
                return rec;
            }
        }

        // 查询报告单上的样本信息和一些申请单信息(有些字段是ID的要查询相关表，查出ID对应的说明);
        public long m_lngGetApplSampleInfo(string p_strSampleID, out System.Data.DataTable p_dtbSample)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetApplSampleInfo(p_strSampleID, out p_dtbSample);
                p_dtbSample = Function.ReNameDatatable(p_dtbSample);
                return rec;
            }
        }

        // 根据Application_ID查找该申请单中各种检查所需的各类样品的数量
        public long m_lngGetSampleTotalQtyByApplicationID(string p_strApplication_ID, out System.Data.DataTable p_dtbSampleQty)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetSampleTotalQtyByApplicationID(p_strApplication_ID, out p_dtbSampleQty);
                p_dtbSampleQty = Function.ReNameDatatable(p_dtbSampleQty);
                return rec;
            }
        }

        // 根据DevID查询表t_opr_lis_device_relation（查询已核收的,STATUS_INT=1）
        public long m_lngGetDevRelationInfo(string p_strDevID, out System.Data.DataTable p_dtbDevRelation)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetDevRelationInfo(p_strDevID, out p_dtbDevRelation);
                p_dtbDevRelation = Function.ReNameDatatable(p_dtbDevRelation);
                return rec;
            }
        }

        // 查找某一申请单中使用某一种样品的所有检验组
        public long m_lngGetCheckGroupListByAppID_SampleType(string p_strApplication_ID, string p_strSampleTypeID, out System.Data.DataTable p_dtbCheckGroupList)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                long rec = svc.m_lngGetCheckGroupListByAppID_SampleType(p_strApplication_ID, p_strSampleTypeID, out p_dtbCheckGroupList);
                p_dtbCheckGroupList = Function.ReNameDatatable(p_dtbCheckGroupList);
                return rec;
            }
        }

        public long m_lngGetSampleDetailByAppID_SampleType(string p_strApplication_ID, string p_strSampleTypeID, out clsSampleVO[] colSampleList)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetSampleDetailByAppID_SampleType(p_strApplication_ID, p_strSampleTypeID, out colSampleList);
            }
        }

        public long m_lngGetSampleInfoByBarCode(string strBarCode, out clsSampleVO objSampleVO)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngGetSampleInfoByBarCode(strBarCode, out objSampleVO);
            }
        }

        /// <summary>
        /// 获取样本状态
        /// </summary>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_intStatus"></param>
        /// <returns></returns>
        public long m_lngQuerySampleStatus(string p_strSampleID, out int p_intStatus, out string p_strIsSampleBack)
        {
            using (clsQuerySampleSvc svc = new clsQuerySampleSvc())
            {
                return svc.m_lngQuerySampleStatus(p_strSampleID, out p_intStatus, out p_strIsSampleBack);
            }
        }

        #endregion

        #region clsQueryStatSvc

        // 根据条件查询学术统计的信息
        public long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo,
            string p_strCheckItemID, string p_strResultFrom, string p_strResultTo, string p_strAgeFrom, string p_strAgeTo, string p_strSex,
            string p_strLowCompare, string p_strCondition, string p_strUpCompare, out DataTable dtbResult)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetScienceStatByCondition(p_strDatFrom, p_strDatTo,
              p_strCheckItemID, p_strResultFrom, p_strResultTo, p_strAgeFrom, p_strAgeTo, p_strSex,
              p_strLowCompare, p_strCondition, p_strUpCompare, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        // 根据条件查询学术统计的信息
        public long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo,
            string p_strAgeFrom, string p_strAgeTo, string p_strSex, clsLisScienceStatItemQueryCondition[] p_objRecordArr, out DataTable dtbResult)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetScienceStatByCondition(p_strDatFrom, p_strDatTo,
              p_strAgeFrom, p_strAgeTo, p_strSex, p_objRecordArr, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        // 根据条件查询学术统计的信息
        public long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo,
            string p_strAgeFrom, string p_strAgeTo, string p_strSex, clsLisScienceStatItemQueryCondition[] p_objRecordArr, out DataTable dtbHead,
            out DataTable dtbDetail)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetScienceStatByCondition(p_strDatFrom, p_strDatTo, p_strAgeFrom, p_strAgeTo, p_strSex, p_objRecordArr, out dtbHead, out dtbDetail);
                dtbHead = Function.ReNameDatatable(dtbHead);
                dtbDetail = Function.ReNameDatatable(dtbDetail);
                return rec;
            }
        }

        // 查询所有的工作组信息
        public long m_lngGetAllWorkGroupInfo(out DataTable p_dtbResult)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetAllWorkGroupInfo(out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 查询所有的工作组信息
        public long m_lngGetAllWorkGroupInfo(out clsLisWorkGroup_VO[] p_objResultArr)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                return svc.m_lngGetAllWorkGroupInfo(out p_objResultArr);
            }
        }

        // 获取所有的统计组信息
        public long m_lngGetAllStatGroupInfo(out clsLisStatGroup_VO[] p_objResultArr)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                return svc.m_lngGetAllStatGroupInfo(out p_objResultArr);
            }
        }

        // 获取所有的统计组申请单元信息
        public long m_lngGetAllStatGroupUnitInfo(out clsLisStatGroupUnit_VO[] p_objResultArr)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                return svc.m_lngGetAllStatGroupUnitInfo(out p_objResultArr);
            }
        }

        // 根据统计组ID获取该组下的申请单元信息
        public long m_lngGetApplUnitByStatGroupID(string p_strStatGroupID, out clsApplUnit_VO[] p_objResultArr)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                return svc.m_lngGetApplUnitByStatGroupID(p_strStatGroupID, out p_objResultArr);
            }
        }

        // 工作量统计汇总
        public long m_lngGetStatTotalReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetStatTotalReport(p_strDatFrom, p_strDatTo, p_strOprID, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 工作量统计明细
        public long m_lngGetStatDetailReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetStatDetailReport(p_strDatFrom, p_strDatTo, p_strOprID, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 检验费用汇总统计
        public long m_lngGetCheckPriceTotalReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetCheckPriceTotalReport(p_strDatFrom, p_strDatTo, p_strOprID, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 检验费明细统计
        public long m_lngGetCheckPriceDetailReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetCheckPriceDetailReport(p_strDatFrom, p_strDatTo, p_strOprID, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        /// <summary>
        /// 病区标本送检量
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        public long m_lngGetSamplesCheckTotal(out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetSamplesCheckTotal(out p_dtbResult, strDateFrom, strDateTo);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 报表-细菌发生率统计
        /// <summary>
        /// 病区标本送检量
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        public long m_lngGetGermOccurRate(out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetGermOccurRate(out p_dtbResult, strDateFrom, strDateTo);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 报表-细菌分布趋势

        /// <summary>
        /// 细菌分布趋势报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        public long m_lngGetGermDistributeTrend(out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetGermDistributeTrend(out p_dtbResult, strDateFrom, strDateTo);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 报表-微生物检测汇总表
        /// <summary>
        /// 微生物检测汇总
        /// </summary>
        /// <param name="p_objPrincipal">权限密探</param>
        /// <param name="p_dtbResult">符合条件的DataTable</param>
        /// <param name="strDateFrom">开始日期</param>
        /// <param name="strDateTo">终止日期</param>
        /// <param name="listSamples">标本集合</param>
        /// <param name="listPatientArea">病区集合</param>
        /// <returns></returns>
        public long m_lngGetAnimalculeCheckTotal(out DataTable p_dtbResult, string strDateFrom, string strDateTo, List<string> listSamples, List<string> listPatientArea)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                long rec = svc.m_lngGetAnimalculeCheckTotal(out p_dtbResult, strDateFrom, strDateTo, listSamples, listPatientArea);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        /// <summary>
        /// 统计仪器工作量 
        /// </summary>
        /// <param name="p_datStart"></param>
        /// <param name="p_datEnd"></param>
        /// <param name="p_dtStatisResult"></param>
        public void m_mthGetDeviceCheckStatis(DateTime p_datStart, DateTime p_datEnd, out DataTable p_dtStatisResult)
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                p_dtStatisResult = null;
                svc.m_mthGetDeviceCheckStatis(p_datStart, p_datEnd, ref p_dtStatisResult);
                p_dtStatisResult = Function.ReNameDatatable(p_dtStatisResult);
            }
        }

        public DataTable m_dtbGetSamplesList()
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                return Function.ReNameDatatable(svc.m_dtbGetSamplesList());
            }
        }

        public DataTable m_dtbGetDeptList()
        {
            using (clsQueryStatSvc svc = new clsQueryStatSvc())
            {
                return Function.ReNameDatatable(svc.m_dtbGetDeptList());
            }
        }

        #endregion

        #region clsReportGroupSvc

        // 更新标本组的打印顺序
        public long m_lngSetSampleGroupPrintOrder(clsSampleGroup_VO p_objRecord)
        {
            using (clsReportGroupSvc svc = new clsReportGroupSvc())
            {
                return svc.m_lngSetSampleGroupPrintOrder(p_objRecord);
            }
        }

        // 修改报告组 
        public long m_lngModifyReportGroup(ref clsReportGroup_VO objReportGroupVO)
        {
            using (clsReportGroupSvc svc = new clsReportGroupSvc())
            {
                return svc.m_lngModifyReportGroup(ref objReportGroupVO);
            }
        }

        #endregion

        #region clsReportSvc

        // Insert
        public long m_lngInsertReportObject(clsReportObject p_objReportObject)
        {
            using (clsReportSvc svc = new clsReportSvc())
            {
                return svc.m_lngInsertReportObject(p_objReportObject);
            }
        }

        // Update
        public long m_lngUpdateReportObject(clsReportObject p_objReportObject)
        {
            using (clsReportSvc svc = new clsReportSvc())
            {
                return svc.m_lngUpdateReportObject(p_objReportObject);
            }
        }

        // Delete
        public long m_lngDeleteReportObject(string p_strApplicationID)
        {
            using (clsReportSvc svc = new clsReportSvc())
            {
                return svc.m_lngDeleteReportObject(p_strApplicationID);
            }
        }

        // 更新体检登记表，使其状态为保存
        /// <summary>
        /// 更新体检登记表，使其状态为保存
        /// </summary>
        /// <param name="strApplicationID"></param>
        /// <returns></returns>
        public long m_lngUpdatePEReg(string strApplicationID)
        {
            using (clsReportSvc svc = new clsReportSvc())
            {
                return svc.m_lngUpdatePEReg(strApplicationID);
            }
        }

        #endregion

        #region clsSampleGroupSvc

        // 批量更新标本组下申请单元的检验项目的打印顺序
        public long m_lngSetApplUnitItemPrintSeqArr(clsApplUnitDetail_VO[] p_objRecordArr)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngSetApplUnitItemPrintSeqArr(p_objRecordArr);
            }
        }

        // 更新标本组下申请单元的检验项目的打印顺序 
        public long m_lngSetApplUnitItemPrintSeq(clsApplUnitDetail_VO p_objRecord)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngSetApplUnitItemPrintSeq(p_objRecord);
            }
        }

        // 根据样本组ID删除样本组下的申请单元 
        public long m_lngDelSampleGroupUnitBySampleGroupID(string p_strSampleGroupID)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngDelSampleGroupUnitBySampleGroupID(p_strSampleGroupID);
            }
        }

        // 批量新增样本组下的申请单元 
        public long m_lngAddNewSampleGroupUnitArr(string p_strSampleGroupID, clsLisSampleGroupUnit_VO[] p_objRecordArr)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngAddNewSampleGroupUnitArr(p_strSampleGroupID, p_objRecordArr);
            }
        }

        // 新增样本组下的申请单元
        public long m_lngAddNewSampleGroupUnit(clsLisSampleGroupUnit_VO p_objRecord)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngAddNewSampleGroupUnit(p_objRecord);
            }
        }

        // 根据sample_group_id删除标本组的标本类型 
        public long m_lngDelGroupSampleTypeBySampleGroupID(string p_strSampleGroupID)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngDelGroupSampleTypeBySampleGroupID(p_strSampleGroupID);
            }
        }


        // 批量修改标本组的标本类型列表 
        public long m_lngModifyGroupSampleTypeArr(List<clsLisGroupSampleType_VO> p_arlAdd, List<clsLisGroupSampleType_VO> p_arlRemove)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngModifyGroupSampleTypeArr(p_arlAdd, p_arlRemove);
            }
        }

        // 批量新增标本组的标本类型列表 
        public long m_lngAddNewGroupSampleTypeArr(string p_strSampleGroupID, List<clsLisGroupSampleType_VO> p_arlAdd)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngAddNewGroupSampleTypeArr(p_strSampleGroupID, p_arlAdd);
            }
        }

        // 新增标本组的标本类型 
        public long m_lngAddNewGroupSampleType(clsLisGroupSampleType_VO p_objRecord)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngAddNewGroupSampleType(p_objRecord);
            }
        }

        // 删除标本组的标本类型
        public long m_lngDelGroupSampleTypeByCondition(string p_strSampleGroupID, string p_strSampleTypeID)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngDelGroupSampleTypeByCondition(p_strSampleGroupID, p_strSampleTypeID);
            }
        }

        // 根据sample_group_id删除表T_AID_LIS_SAMPLE_GROUP_MODEL的记录 
        public long m_lngDelSampleGroupModelBySampleGroupID(string p_strSampleGroupID)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngDelSampleGroupModelBySampleGroupID(p_strSampleGroupID);
            }
        }

        // 批量新增标本组的仪器型号列表 
        public long m_lngAddNewSampleGroupModelArr(string p_strSampleGroupNo, List<clsLisSampleGroupModel_VO> p_arlAdd)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngAddNewSampleGroupModelArr(p_strSampleGroupNo, p_arlAdd);
            }
        }

        // 批量修改标本组的仪器型号列表
        public long m_lngSetSampleGroupModelArr(List<clsLisSampleGroupModel_VO> p_arlAdd, List<clsLisSampleGroupModel_VO> p_arlRemove)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngSetSampleGroupModelArr(p_arlAdd, p_arlRemove);
            }
        }

        // 删除表T_AID_LIS_SAMPLE_GROUP_MODEL的记录 
        public long m_lngDelSampleGroupModel(clsLisSampleGroupModel_VO p_objRecord)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngDelSampleGroupModel(p_objRecord);
            }
        }

        // 新增记录到表T_AID_LIS_SAMPLE_GROUP_MODEL 
        public long m_lngAddNewSampleGroupModel(clsLisSampleGroupModel_VO p_objRecord)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngAddNewSampleGroupModel(p_objRecord);
            }
        }

        // 更新标本组的基本信息 
        public long m_lngSetSampleGroupInfo(ref clsSampleGroup_VO objSampleGroupVO)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngSetSampleGroupInfo(ref objSampleGroupVO);
            }
        }

        // 保存标本组及其明细
        public long m_lngAddSampleGroupAndDetail(ref clsSampleGroup_VO objSampleGroup,
            ref clsLisSampleGroupUnit_VO[] objSampleGroupUnitList, clsApplUnitDetail_VO[] p_objApplUnitDetailArr, List<clsLisSampleGroupModel_VO> p_arlAdd,
            List<clsLisSampleGroupModel_VO> p_arlRemove, List<clsLisGroupSampleType_VO> p_arlAddSampleType, List<clsLisGroupSampleType_VO> p_arlRemoveSampleType)
        {
            using (clsSampleGroupSvc svc = new clsSampleGroupSvc())
            {
                return svc.m_lngAddSampleGroupAndDetail(ref objSampleGroup, ref objSampleGroupUnitList, p_objApplUnitDetailArr, p_arlAdd, p_arlRemove, p_arlAddSampleType, p_arlRemoveSampleType);
            }
        }

        #endregion

        #region clsSampleSvc

        // [U] 修改检验版本号
        public long m_lngModifyBarCode(string strSampleID, string strAppID)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngModifyBarCode(strSampleID, strAppID);
            }
        }

        // 更新样本标志位
        public long m_lngUpdateSampleFlag(string[] p_strSampleIDArr, int p_intSourceStatus, int p_intTargetStatus)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngUpdateSampleFlag(p_strSampleIDArr, p_intSourceStatus, p_intTargetStatus);
            }
        }

        /// 更新样本标志位
        public long m_lngUpdateSampleFlag(string[] p_strSampleIDArr, int p_intSourceStatus, int p_intTargetStatus, string p_strOriginDate)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngUpdateSampleFlag(p_strSampleIDArr, p_intSourceStatus, p_intTargetStatus, p_strOriginDate);
            }
        }

        // 接收标本或退回
        /// <summary>
        /// 接收标本或退回
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus">3-核收 7-退回</param>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_strReceiveDat">核收日期</param>
        /// <param name="p_strReceiveEmp">核收员工</param>
        /// <returns></returns>
        public long m_lngReceiveSample(int p_intStatus, string p_strSampleID, string p_strReceiveDat, string p_strReceiveEmp, string p_strSendPeopleID)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngReceiveSample(p_intStatus, p_strSampleID, p_strReceiveDat, p_strReceiveEmp, p_strSendPeopleID);
            }
        }

        // 新增一条记录
        public long m_lngAddNewSampleInterpose(clsLisSampleInterposeVO p_objRecord)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngAddNewSampleInterpose(p_objRecord);
            }
        }

        // 根据仪器ID更新插队记录
        public long m_lngSetSampleInterpose(clsLisSampleInterposeVO p_objRecord)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngSetSampleInterpose(p_objRecord);
            }
        }

        // 插队处理
        public long m_lngSampleInterpose(clsLisSampleInterposeVO p_objRecord)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngSampleInterpose(p_objRecord);
            }
        }

        // 更新表T_OPR_LIS_DEVICE_RELATION
        public long m_lngSetLisDeviceRelation(clsT_LIS_DeviceRelationVO p_objRecord)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngSetLisDeviceRelation(p_objRecord);
            }
        }

        // [U]增加一个样本,同时修改申请样本组
        /// <summary>
        /// 增加一个样本,同时修改申请样本组
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strReportID"></param>
        /// <param name="p_strSampleGroupID"></param>
        /// <param name="p_objRecordVO"></param>
        /// <returns></returns>
        public long m_lngAddNewSampleAndModifyAppSampleGroup(string p_strAppID, clsT_OPR_LIS_SAMPLE_VO p_objRecordVO)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngAddNewSampleAndModifyAppSampleGroup(p_strAppID, p_objRecordVO);
            }
        }

        // [U]增加一个样本,同时修改申请样本组
        /// <summary>
        /// 增加一个样本,同时修改申请样本组
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strReportID"></param>
        /// <param name="p_strSampleGroupID"></param>
        /// <param name="p_objRecordVO"></param>
        /// <returns></returns>
        public long m_lngAddNewSampleAndModifyAppSampleGroup(string p_strAppID, ref clsT_OPR_LIS_SAMPLE_VO p_objRecordVO)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngAddNewSampleAndModifyAppSampleGroup(p_strAppID, ref p_objRecordVO);
            }
        }

        /// <summary>
        /// 为表 t_opr_lis_sample 新增,修改,删除 记录时用 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        public long m_lngInsertSampleRecord(clsT_OPR_LIS_SAMPLE_VO[] p_objRecordVOArr)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngInsertSampleRecord(p_objRecordVOArr);
            }
        }

        /// <summary>
        /// 为表 t_opr_lis_device_relation  新增 记录时用 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngAddNewDeviceRelation(clsT_LIS_DeviceRelationVO p_objRecord)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngAddNewDeviceRelation(p_objRecord);
            }
        }

        /// <summary>
        /// 删除仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strRelationDate"></param>
        /// <param name="p_strSeq"></param>
        /// <returns></returns>
        public long m_lngDeleteDeviceRelation(string p_strDeviceID, string p_strRelationDate, string p_strSeq)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngDeleteDeviceRelation(p_strDeviceID, p_strRelationDate, p_strSeq);
            }
        }

        /// <summary>
        /// 删除仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRelation"></param>
        /// <returns></returns>
        public long m_lngDeleteDeviceRelation(clsT_LIS_DeviceRelationVO p_objRelation)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngDeleteDeviceRelation(p_objRelation);
            }
        }

        /// <summary>
        /// 通过样品ID删除仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleID"></param>
        /// <returns></returns>
        public long m_lngDelDevicRelation(string p_strSampleID)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngDelDevicRelation(p_strSampleID);
            }
        }

        /// <summary>
        /// 删除仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSourceVO"></param>
        /// <param name="p_objTargetVO"></param>
        /// <returns></returns>
        public long m_lngModifyBind(clsT_LIS_DeviceRelationVO p_objSourceVO, clsT_LIS_DeviceRelationVO p_objTargetVO)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngModifyBind(p_objSourceVO, p_objTargetVO);
            }
        }

        // 核收时更新样本表的标志位信息
        public long m_lngModifySampleInfoOnRecepting(clsT_OPR_LIS_SAMPLE_VO p_objRecord)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngModifySampleInfoOnRecepting(p_objRecord);
            }
        }

        // 核收仪器标本
        public long m_lngReceptSample(clsT_OPR_LIS_SAMPLE_VO p_objSampleVO, clsT_LIS_DeviceRelationVO p_objDeviceRelationVO)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngReceptSample(p_objSampleVO, p_objDeviceRelationVO);
            }
        }

        // 审核样本,并修改相关的所有标志位     
        public long m_lngAuditingSample(string p_strSampleID)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngAuditingSample(p_strSampleID);
            }
        }

        // 根据DeviceID和DeviceSampleID设置表t_opr_lis_device_relation的标本记录状态
        public long m_lngSetStatus(ref clsDeviceRelation_VO objDeviceRelationVO)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngSetStatus(ref objDeviceRelationVO);
            }
        }

        // 根据DeviceID和DeviceSampleID更新t_opr_lis_device_relation的标本架位号
        public long m_lngSetPositionANDSampleID(ref clsDeviceRelation_VO objDeviceRelationVO, ref clsSampleVO objSampleVO)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngSetPositionANDSampleID(ref objDeviceRelationVO, ref objSampleVO);
            }
        }

        // 增加一个样本到样本表中。Old
        public long m_lngAddSample(ref clsSampleVO aSampleVO)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngAddSample(ref aSampleVO);
            }
        }

        // 根据SampleBarCode设置标本的状态
        public long m_lngSetSampleStatusBySampleBarCode(string p_strSampleBarCode, int p_intSampleStatus)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngSetSampleStatusBySampleBarCode(p_strSampleBarCode, p_intSampleStatus);
            }
        }

        // 根据SampleId设置标本的状态
        public long m_lngSetSampleStatusBySampleId(string p_strSampleId, int p_intSampleStatus)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngSetSampleStatusBySampleId(p_strSampleId, p_intSampleStatus);
            }
        }

        public long m_lngInsertSampleFeedBack(clslissample_feedback p_objSampleFeedBack)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngInsertSampleFeedBack(p_objSampleFeedBack);
            }
        }

        // 修改采样人员
        /// <summary>
        /// 修改采样人员
        /// </summary>
        /// <param name="p_objPrincil"></param>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        public long m_lngInsertCollector(string p_strEmpId, string p_strSampleId, string p_strApplicationID)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngInsertCollector(p_strEmpId, p_strSampleId, p_strApplicationID);
            }
        }

        /// <summary>
        /// 通过申请单号更改t_opr_lis_application表内的打印状态
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppliID"></param>
        /// <returns></returns>
        public long m_lngUpdateApplPrint(string p_strAppliID, bool blnPrint)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.m_lngUpdateApplPrint(p_strAppliID, blnPrint);
            }
        }

        /// <summary>
        /// 修改采样时间
        /// </summary>
        /// <param name="lstBarCode"></param>
        /// <param name="collTime"></param>
        /// <returns></returns>
        public int UpdateCollectorTime(List<string> lstBarCode, DateTime collTime)
        {
            using (clsSampleSvc svc = new clsSampleSvc())
            {
                return svc.UpdateCollectorTime(lstBarCode, collTime);
            }
        }

        #endregion

        #region clsStatSvc

        // 新增工作组 
        public long m_lngAddNewWorkGroup(clsLisWorkGroup_VO p_objRecord)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngAddNewWorkGroup(p_objRecord);
            }
        }

        // 更新工作组信息
        public long m_lngModifyWorkGroup(clsLisWorkGroup_VO p_objRecord)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngModifyWorkGroup(p_objRecord);
            }
        }

        // 删除工作组信息 
        public long m_lngDelWorkGroup(string p_strWorkGroupID, string p_strStatus)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngDelWorkGroup(p_strWorkGroupID, p_strStatus);
            }
        }

        // 新增统计组基本信息 
        public long m_lngAddNewStatGroupBaseInfo(clsLisStatGroup_VO p_objRecord)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngAddNewStatGroupBaseInfo(p_objRecord);
            }
        }

        // 新增统计组和申请单元的关系 
        public long m_lngAddNewStatGroupUnit(clsLisStatGroupUnit_VO p_objRecord)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngAddNewStatGroupUnit(p_objRecord);
            }
        }

        // 新增统计组及其明细 
        public long m_lngAddNewStatGroup(clsLisStatGroup_VO p_objStatGroup, clsLisStatGroupUnit_VO[] p_objStatGroupUnitArr)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngAddNewStatGroup(p_objStatGroup, p_objStatGroupUnitArr);
            }
        }

        // 更新统计组基本信息
        public long m_lngSetStatGroupBaseInfo(clsLisStatGroup_VO p_objRecord)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngSetStatGroupBaseInfo(p_objRecord);
            }
        }

        // 根据统计组ID删除该组下的申请单元 
        public long m_lngDelStatGroupUnitByStatGroupID(string p_strStatGroupID)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngDelStatGroupUnitByStatGroupID(p_strStatGroupID);
            }
        }

        // 更新统计组及明细
        public long m_lngModifyStatGroup(clsLisStatGroup_VO p_objStatGroup, clsLisStatGroupUnit_VO[] p_objStatGroupUnitArr)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngModifyStatGroup(p_objStatGroup, p_objStatGroupUnitArr);
            }
        }

        // 删除统计组 
        public long m_lngDelStatGroup(string p_strStatGroupID)
        {
            using (clsStatSvc svc = new clsStatSvc())
            {
                return svc.m_lngDelStatGroup(p_strStatGroupID);
            }
        }

        #endregion

        #region clsWorkStatsticSvc

        /// <summary>
        /// 获取开单医生或检验者
        /// </summary>
        /// <param name="dtbEmp"></param>
        /// <returns></returns>
        public long lngGetEmployee(out DataTable dtbEmp)
        {
            using (clsWorkStatsticSvc svc = new clsWorkStatsticSvc())
            {
                long rec = svc.lngGetEmployee(out dtbEmp);
                dtbEmp = Function.ReNameDatatable(dtbEmp);
                return rec;
            }
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="dtbDept"></param>
        /// <returns></returns>
        public long lngGetDept(out DataTable dtbDept)
        {
            using (clsWorkStatsticSvc svc = new clsWorkStatsticSvc())
            {
                long rec = svc.lngGetDept(out dtbDept);
                dtbDept = Function.ReNameDatatable(dtbDept);
                return rec;
            }
        }

        /// <summary>
        /// 工作量统计
        /// </summary>
        /// <param name="p_intQueryType">0 = 审核时间，1 = 申请时间</param>
        /// <param name="p_dtDateFrom"></param>
        /// <param name="p_dtDateTO"></param>
        /// <param name="p_strQuery"> 0 = 按开单科室，1=按开单医生，2=检验人员，3=检验科（全院）</param>
        /// <param name="p_strCondition"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long lngGetWorkStatstic(int p_intQueryType, DateTime p_dtDateFrom, DateTime p_dtDateTO, int p_strQuery, string p_strCondition, out DataTable dtbResult)
        {
            using (clsWorkStatsticSvc svc = new clsWorkStatsticSvc())
            {
                long rec = svc.lngGetWorkStatstic(p_intQueryType, p_dtDateFrom, p_dtDateTO, p_strQuery, p_strCondition, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        #endregion

        #region clsSchBaseInfoSvc

        // 返回检验项目树
        public long m_lngGetCheckItemTree(out clsLISUserGroupNode root)
        {
            using (clsSchBaseInfoSvc svc = new clsSchBaseInfoSvc())
            {
                return svc.m_lngGetCheckItemTree(out root);
            }
        }

        #endregion

        #region clsSchQCBatchSvc

        /// <summary>
        /// 根据组合条件查询质控批
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFromDat"></param>
        /// <param name="p_strToDat"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        public long m_lngFindQCBatchCombinatorial(clsLisQCBatchSchVO p_objCondition, out clsLisQCBatchVO[] p_objRecordArr)
        {
            using (clsSchQCBatchSvc svc = new clsSchQCBatchSvc())
            {
                return svc.m_lngFindQCBatchCombinatorial(p_objCondition, out p_objRecordArr);
            }
        }

        #endregion

        #region clsTmdCheckMethodSvc

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        public long m_lngInsert(clsLisCheckMethodVO p_objCheckMethod, out int p_intSeq)
        {
            using (clsTmdCheckMethodSvc svc = new clsTmdCheckMethodSvc())
            {
                return svc.m_lngInsert(p_objCheckMethod, out p_intSeq);
            }
        }

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        public long m_lngUpdate(clsLisCheckMethodVO p_objCheckMethod)
        {
            using (clsTmdCheckMethodSvc svc = new clsTmdCheckMethodSvc())
            {
                return svc.m_lngUpdate(p_objCheckMethod);
            }
        }

        // DELETE
        public long m_lngDelete(int p_intSeq)
        {
            using (clsTmdCheckMethodSvc svc = new clsTmdCheckMethodSvc())
            {
                return svc.m_lngDelete(p_intSeq);
            }
        }

        /// <summary>
        /// 根据检验方法序号查找
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngFind(int p_intSeq, out clsLisCheckMethodVO p_objCheckMethod)
        {
            using (clsTmdCheckMethodSvc svc = new clsTmdCheckMethodSvc())
            {
                return svc.m_lngFind(p_intSeq, out p_objCheckMethod);
            }
        }

        public long m_lngFind(out clsLisCheckMethodVO[] p_objResultArr)
        {
            using (clsTmdCheckMethodSvc svc = new clsTmdCheckMethodSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        public void ConstructVO(DataTable dtRow, ref clsLisCheckMethodVO p_objCheckMethod)
        {
            using (clsTmdCheckMethodSvc svc = new clsTmdCheckMethodSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objCheckMethod);
            }
        }

        #endregion

        #region clsTmdConcentrationSvc

        public void ConstructVO(DataTable dtRow, ref clsLisConcentrationVO p_objConcentration)
        {
            using (clsTmdConcentrationSvc svc = new clsTmdConcentrationSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objConcentration);
            }
        }

        public long m_lngInsert(clsLisConcentrationVO p_objConcentration, out int p_intSeq)
        {
            using (clsTmdConcentrationSvc svc = new clsTmdConcentrationSvc())
            {
                return svc.m_lngInsert(p_objConcentration, out p_intSeq);
            }
        }

        public long m_lngUpdate(clsLisConcentrationVO QCBatch)
        {
            using (clsTmdConcentrationSvc svc = new clsTmdConcentrationSvc())
            {
                return svc.m_lngUpdate(QCBatch);
            }
        }

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        public long m_lngFind(int p_intSeq, out clsLisConcentrationVO p_objConcentration)
        {
            using (clsTmdConcentrationSvc svc = new clsTmdConcentrationSvc())
            {
                return svc.m_lngFind(p_intSeq, out p_objConcentration);
            }
        }

        public long m_lngFind(out clsLisConcentrationVO[] p_objResultArr)
        {
            using (clsTmdConcentrationSvc svc = new clsTmdConcentrationSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        #endregion

        #region clsTmdQCRulesSvc

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        public long m_lngInsert(clsLisQCRuleVO p_objRule, out int p_intSeq)
        {
            using (clsTmdQCRulesSvc svc = new clsTmdQCRulesSvc())
            {
                return svc.m_lngInsert(p_objRule, out p_intSeq);
            }
        }

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        public long m_lngUpdate(clsLisQCRuleVO p_objRule)
        {
            using (clsTmdQCRulesSvc svc = new clsTmdQCRulesSvc())
            {
                return svc.m_lngUpdate(p_objRule);
            }
        }

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        /// <summary>
        /// 根据检验方法序号查找
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngFind(int p_intSeq, out clsLisQCRuleVO p_objRule)
        {
            using (clsTmdQCRulesSvc svc = new clsTmdQCRulesSvc())
            {
                return svc.m_lngFind(p_intSeq, out p_objRule);
            }
        }

        public long m_lngFind(out clsLisQCRuleVO[] p_objResultArr)
        {
            using (clsTmdQCRulesSvc svc = new clsTmdQCRulesSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        /// <summary>
        /// 从数据库中构造实体
        /// </summary>
        /// <param name="p_dtrSource"></param>
        /// <param name="p_objRule"></param>
        public void ConstructVO(DataTable dtRow, ref clsLisQCRuleVO p_objRule)
        {
            using (clsTmdQCRulesSvc svc = new clsTmdQCRulesSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objRule);
            }
        }

        #endregion

        #region clsTmdVendorSvc

        public long m_lngInsert(clsLisVendorVO p_objVendor, out int p_intSeq)
        {
            using (clsTmdVendorSvc svc = new clsTmdVendorSvc())
            {
                return svc.m_lngInsert(p_objVendor, out p_intSeq);
            }
        }

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        public long m_lngUpdate(clsLisVendorVO p_objVendor)
        {
            using (clsTmdVendorSvc svc = new clsTmdVendorSvc())
            {
                return svc.m_lngUpdate(p_objVendor);
            }
        }

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        public long m_lngFind(int p_intSeq, out clsLisVendorVO p_objVendor)
        {
            using (clsTmdVendorSvc svc = new clsTmdVendorSvc())
            {
                return svc.m_lngFind(p_intSeq, out p_objVendor);
            }
        }

        public long m_lngFind(out clsLisVendorVO[] p_objResultArr)
        {
            using (clsTmdVendorSvc svc = new clsTmdVendorSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        /// <summary>
        /// 从数据库中构造实体
        /// </summary>
        /// <param name="p_dtrSource"></param>
        /// <param name="p_objVendor"></param>
        public void ConstructVO(DataTable dtRow, ref clsLisVendorVO p_objVendor)
        {
            using (clsTmdVendorSvc svc = new clsTmdVendorSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objVendor);
            }
        }

        #endregion

        #region clsTmdWorkGroupSvc

        public long m_lngInsert(clsLisWorkGroupVO p_objWorkGroup, out int p_intSeq)
        {
            using (clsTmdWorkGroupSvc svc = new clsTmdWorkGroupSvc())
            {
                return svc.m_lngInsert(p_objWorkGroup, out p_intSeq);
            }
        }

        public long m_lngUpdate(clsLisWorkGroupVO p_objWorkGroup)
        {
            using (clsTmdWorkGroupSvc svc = new clsTmdWorkGroupSvc())
            {
                return svc.m_lngUpdate(p_objWorkGroup);
            }
        }

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);


        public long m_lngFind(int p_intSeq, out clsLisWorkGroupVO p_objWorkGroup)
        {
            using (clsTmdWorkGroupSvc svc = new clsTmdWorkGroupSvc())
            {
                return svc.m_lngFind(p_intSeq, out p_objWorkGroup);
            }
        }

        public long m_lngFind(out clsLisWorkGroupVO[] p_objResultArr)
        {
            using (clsTmdWorkGroupSvc svc = new clsTmdWorkGroupSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        public void ConstructVO(DataTable dtRow, ref clsLisWorkGroupVO p_objWorkGroup)
        {
            using (clsTmdWorkGroupSvc svc = new clsTmdWorkGroupSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objWorkGroup);
            }
        }

        #endregion

        #region clsTmdQCBatchConcentrationSvc

        public void ConstructVO(DataTable dtRow, ref clsLisQCConcentrationVO p_objQCConcentration)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objQCConcentration);
            }
        }

        public long m_lngInsert(clsLisQCConcentrationVO p_objQCConcentration)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                return svc.m_lngInsert(p_objQCConcentration);
            }
        }
        public long m_lngUpdate(clsLisQCConcentrationVO p_objQCConcentration)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                return svc.m_lngUpdate(p_objQCConcentration);
            }
        }
        public long m_lngDelete(int p_intQCBatchSeq, int p_intConcentrationSeq)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                return svc.m_lngDelete(p_intQCBatchSeq, p_intConcentrationSeq);
            }
        }
        public long m_lngFind(int p_intQCBatchSeq, int p_intConcentrationSeq, out clsLisQCConcentrationVO p_objQCConcentration)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                return svc.m_lngFind(p_intQCBatchSeq, p_intConcentrationSeq, out p_objQCConcentration);
            }
        }

        public long m_lngFind(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                return svc.m_lngFind(p_intQCBatchSeq, out p_objResultArr);
            }
        }
        public long m_lngFindDeleted(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                return svc.m_lngFindDeleted(p_intQCBatchSeq, out p_objResultArr);
            }
        }

        /// <summary>
        /// 查找指定的质控浓度
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intQCBatchSeqArr"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFind(int[] p_intQCBatchSeqArr, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                return svc.m_lngFind(p_intQCBatchSeqArr, out p_objResultArr);
            }
        }

        public long m_lngFind(out clsLisQCConcentrationVO[] p_objResultArr)
        {
            using (clsTmdQCBatchConcentrationSvc svc = new clsTmdQCBatchConcentrationSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        #endregion

        #region clsTmdQCBatchSvc

        public void ConstructVO(DataTable dtRow, ref clsLisQCBatchVO p_objQCBatch)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objQCBatch);
            }
        }

        public long m_lngInsert(clsLisQCBatchVO p_objQCBatch, out int p_intSeq)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngInsert(p_objQCBatch, out p_intSeq);
            }
        }

        /// <summary>
        /// 保存质控批类
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objQCBatchArr"></param>
        /// <param name="p_intSeqArr"></param>
        /// <returns></returns>
        public long m_lngInsertByArr(clsLisQCBatchVO[] p_objQCBatchArr, out int[] p_intSeqArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngInsertByArr(p_objQCBatchArr, out p_intSeqArr);
            }
        }

        public long m_lngUpdate(clsLisQCBatchVO QCBatch)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngUpdate(QCBatch);
            }
        }

        /// <summary>
        /// 更新质控样本的结果
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objQCDataArr"></param>
        /// <returns></returns>
        public long m_lngUpdateByArr(clsLisQCDataVO[] p_objQCDataArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngUpdateByArr(p_objQCDataArr);
            }
        }

        //// DELETE
        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        /// <summary>
        /// 删除质控样本结果数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeqArr"></param>
        /// <returns></returns>
        public long m_lngDeleteByArr(int[] p_intSeqArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngDeleteByArr(p_intSeqArr);
            }
        }

        public long m_lngFind(int p_intSeq, bool p_blnExtFind, out clsLisQCBatchVO p_objQCBatch)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFind(p_intSeq, p_blnExtFind, out p_objQCBatch);
            }
        }

        public long m_lngFind(out clsLisQCBatchVO[] p_objResultArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        /// <summary>
        /// 查找定质控批序号的质控设置
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeqArr"></param>
        /// <param name="p_blnExtFind"></param>
        /// <param name="p_objQCBatchArr"></param>
        /// <returns></returns>
        public long m_lngFind(int[] p_intSeqArr, bool p_blnExtFind, out clsLisQCBatchVO[] p_objQCBatchArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFind(p_intSeqArr, p_blnExtFind, out p_objQCBatchArr);
            }
        }

        public long m_lngQueryDeviceSampleID(int p_intBatchSeq, out string p_strSampleId)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngQueryDeviceSampleID(p_intBatchSeq, out p_strSampleId);
            }
        }

        /// <summary>
        /// m_lngQueryDeviceSampleID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intBatchSeq"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        public long m_lngReceiveDeviceQCDataBySampleID(string p_strSampleID, string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngReceiveDeviceQCDataBySampleID(p_strSampleID, p_strStartDat, p_strEndDat, p_intBatchSeqArr, out p_objQCDataArr);
            }
        }

        public long m_lngFindQCBatch(int[] p_intSeqArr, bool p_blnExtFind, out clsLisQCBatchVO[] p_objQCBatchArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCBatch(p_intSeqArr, p_blnExtFind, out p_objQCBatchArr);
            }
        }

        public long m_lngFindQCConcentration(int[] p_intQCBatchSeqArr, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCConcentration(p_intQCBatchSeqArr, out p_objResultArr);
            }
        }

        public long m_lngFindQCData(out clsLisQCDataVO[] p_objResultArr, int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCData(out p_objResultArr, p_intQCBatchSeqArr, p_datBegin, p_datEnd);
            }
        }

        public void ConstructVO(DataTable dtRow, ref clsLisQCDataVO p_objQCData)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objQCData);
            }
        }

        public long m_lngFindQCReport(int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCReport(p_intQCBatchSeqArr, p_datBegin, p_datEnd, p_status, out p_objQCReportArr);
            }
        }

        public long m_lngFindQCBatch(int intSeq, bool blnExtFind, out clsLisQCBatchVO qcBatchVo)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCBatch(intSeq, blnExtFind, out qcBatchVo);
            }
        }

        public long m_lngFindQCConcentration(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCConcentration(p_intQCBatchSeq, out p_objResultArr);
            }
        }

        public long m_lngFindQCData(out clsLisQCDataVO[] p_objResultArr, int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCData(out p_objResultArr, p_intQCBatchSeq, p_datBegin, p_datEnd);
            }
        }

        public long m_lngFindQCReport(int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCReport(p_intQCBatchSeq, p_datBegin, p_datEnd, p_status, out p_objQCReportArr);
            }
        }

        public long m_lngUpdateQCData(clsLisQCDataVO QCBatch)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngUpdateQCData(QCBatch);
            }
        }

        public long m_lngInsertQCData(clsLisQCDataVO p_objQCData, out int p_intSeq)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngInsertQCData(p_objQCData, out p_intSeq);
            }
        }

        public long m_lngDeleteQCData(int p_intSeq)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngDeleteQCData(p_intSeq);
            }
        }

        public long m_lngSaveAllQCData(clsLisQCDataVO[] p_objInsertArr, clsLisQCDataVO[] p_objUpdateArr, int[] p_intDelArr, out int[] p_intISeqArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngSaveAllQCData(p_objInsertArr, p_objUpdateArr, p_intDelArr, out p_intISeqArr);
            }
        }

        public long m_lngUpdateQCDataByArr(clsLisQCDataVO[] p_objQCDataArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngUpdateQCDataByArr(p_objQCDataArr);
            }
        }

        public long m_lngInsertQCDataByArr(clsLisQCDataVO[] p_objQCDataArr, out int[] p_intSeqArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngInsertQCDataByArr(p_objQCDataArr, out p_intSeqArr);
            }
        }

        public long m_lngDeleteQCDataByArr(int[] p_intSeqArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngDeleteQCDataByArr(p_intSeqArr);
            }
        }

        public long m_lngReceiveDeviceQCData(string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngReceiveDeviceQCData(p_strStartDat, p_strEndDat, p_intBatchSeqArr, out p_objQCDataArr);
            }
        }

        public long m_lngUpdateSDXCV(clsLisQCConcentrationVO p_objQCConcentration)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngUpdateSDXCV(p_objQCConcentration);
            }
        }

        public long m_lngGetSysParam(string p_strParam, out string p_strParamValue)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngGetSysParam(p_strParam, out p_strParamValue);
            }
        }

        public long m_lngFindQCRule(int p_intSeq, out clsLisQCRuleVO p_objRule)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCRule(p_intSeq, out p_objRule);
            }
        }

        public long m_lngFindQCRule(out clsLisQCRuleVO[] p_objResultArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngFindQCRule(out p_objResultArr);
            }
        }

        public long m_lngGetDeviceQCCheckItemByID(string p_strDeviceID, out clsLISCheckItemNode[] p_objResultArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngGetDeviceQCCheckItemByID(p_strDeviceID, out p_objResultArr);
            }
        }

        public long m_lngInsertQCBatch(clsLisQCBatchVO p_objQCBatch, out int p_intSeq)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngInsertQCBatch(p_objQCBatch, out p_intSeq);
            }
        }

        public long m_lngInsertQCBatchByArr(clsLisQCBatchVO[] p_objQCBatchArr, out int[] p_intSeqArr)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngInsertQCBatchByArr(p_objQCBatchArr, out p_intSeqArr);
            }
        }

        public long m_lngUpdateQCBatch(clsLisQCBatchVO QCBatch)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngUpdateQCBatch(QCBatch);
            }
        }

        public long m_lngDeleteQCBatch(int p_intSeq)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngDeleteQCBatch(p_intSeq);
            }
        }

        public long m_lngInsertBatchSet(DataTable p_dtbResult)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngInsertBatchSet(p_dtbResult);
            }
        }

        public long m_lngInsertBatchSet(List<clsLisQCBatchVO> p_lstResult, List<clsLisQCConcentrationVO> p_lstContion)
        {
            using (clsTmdQCBatchSvc svc = new clsTmdQCBatchSvc())
            {
                return svc.m_lngInsertBatchSet(p_lstResult, p_lstContion);
            }
        }

        //[OperationContract]
        //void ConstructVO(DataRow p_dtrSource, ref clsLisQCDataVO p_objQCData);

        #endregion

        #region clsTmdQCDataSvc

        public long m_lngInsert(clsLisQCDataVO p_objQCData, out int p_intSeq)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngInsert(p_objQCData, out p_intSeq);
            }
        }

        /// <summary>
        /// 保存质控样本的结果
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objQCDataArr"></param>
        /// <param name="p_intSeqArr"></param>
        /// <returns></returns>
        public long m_lngInsertByArr(clsLisQCDataVO[] p_objQCDataArr, out int[] p_intSeqArr)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngInsertByArr(p_objQCDataArr, out p_intSeqArr);
            }
        }

        public long m_lngUpdate(clsLisQCDataVO QCBatch)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngUpdate(QCBatch);
            }
        }

        public long m_lngFind(out clsLisQCDataVO[] p_objResultArr, int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngFind(out p_objResultArr, p_intQCBatchSeqArr, p_datBegin, p_datEnd);
            }
        }

        public long m_lngFind(int p_intSeq, out clsLisQCDataVO p_objQCData)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngFind(p_intSeq, out p_objQCData);
            }
        }

        public long m_lngFind(out clsLisQCDataVO[] p_objResultArr)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        public long m_lngFind(out clsLisQCDataVO[] p_objResultArr, int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngFind(out p_objResultArr, p_intQCBatchSeq, p_datBegin, p_datEnd);
            }
        }

        public long m_lngSaveAll(clsLisQCDataVO[] p_objInsertArr, clsLisQCDataVO[] p_objUpdateArr, int[] p_intDelArr, out int[] p_intISeqArr)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngSaveAll(p_objInsertArr, p_objUpdateArr, p_intDelArr, out p_intISeqArr);
            }
        }

        public long m_lngInsertQCReport(clsLisQCReportVO p_objQCReport, out int p_intSeq)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngInsertQCReport(p_objQCReport, out p_intSeq);
            }
        }

        public long m_lngUpdateQCReport(clsLisQCReportVO QCBatch)
        {
            using (clsTmdQCDataSvc svc = new clsTmdQCDataSvc())
            {
                return svc.m_lngUpdateQCReport(QCBatch);
            }
        }

        #endregion

        #region clsTmdQCLisServ

        /// <summary>
        /// 获取质控样本结果数据，采用固定检验样本编号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleID">仪器样本编号</param>
        /// <param name="p_strStartDat">开始时间</param>
        /// <param name="p_strEndDat">结束时间</param>
        /// <param name="p_intBatchSeqArr">质控批序号</param>
        /// <param name="p_objQCDataArr"></param>
        /// <returns></returns>
        public long m_lngGetDeviceQCDataBySampleID(string p_strSampleID, string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr)
        {
            using (clsTmdQCLisServ svc = new clsTmdQCLisServ())
            {
                return svc.m_lngGetDeviceQCDataBySampleID(p_strSampleID, p_strStartDat, p_strEndDat, p_intBatchSeqArr, out p_objQCDataArr);
            }
        }

        #endregion

        #region clsTmdQCReportSvc

        public void ConstructVO(DataTable dtRow, ref clsLisQCReportVO p_objQCReport)
        {
            using (clsTmdQCReportSvc svc = new clsTmdQCReportSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objQCReport);
            }
        }

        public long m_lngInsert(clsLisQCReportVO p_objQCReport, out int p_intSeq)
        {
            using (clsTmdQCReportSvc svc = new clsTmdQCReportSvc())
            {
                return svc.m_lngInsert(p_objQCReport, out p_intSeq);
            }
        }

        public long m_lngUpdate(clsLisQCReportVO QCBatch)
        {
            using (clsTmdQCReportSvc svc = new clsTmdQCReportSvc())
            {
                return svc.m_lngUpdate(QCBatch);
            }
        }

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);


        public long m_lngFind(int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr)
        {
            using (clsTmdQCReportSvc svc = new clsTmdQCReportSvc())
            {
                return svc.m_lngFind(p_intQCBatchSeqArr, p_datBegin, p_datEnd, p_status, out p_objQCReportArr);
            }
        }

        public long m_lngFind(int p_intSeq, out clsLisQCReportVO p_objQCReport)
        {
            using (clsTmdQCReportSvc svc = new clsTmdQCReportSvc())
            {
                return svc.m_lngFind(p_intSeq, out p_objQCReport);
            }
        }

        public long m_lngFind(out clsLisQCReportVO[] p_objResultArr)
        {
            using (clsTmdQCReportSvc svc = new clsTmdQCReportSvc())
            {
                return svc.m_lngFind(out p_objResultArr);
            }
        }

        public long m_lngFind(int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr)
        {
            using (clsTmdQCReportSvc svc = new clsTmdQCReportSvc())
            {
                return svc.m_lngFind(p_intQCBatchSeq, p_datBegin, p_datEnd, p_status, out p_objQCReportArr);
            }
        }

        #endregion

        #region clsTmdQCSampleLotParaSvc

        public void ConstructVO(DataTable dtRow, ref clsLisQCSampleLotParaVO objQCSamplePara)
        {
            using (clsTmdQCSampleLotParaSvc svc = new clsTmdQCSampleLotParaSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref objQCSamplePara);
            }
        }

        public long m_lngInsert(clsLisQCSampleLotParaVO clsLisQCSamplePara)
        {
            using (clsTmdQCSampleLotParaSvc svc = new clsTmdQCSampleLotParaSvc())
            {
                return svc.m_lngInsert(clsLisQCSamplePara);
            }
        }

        public long m_lngUpdate(clsLisQCSampleLotParaVO objQCSamplePara)
        {
            using (clsTmdQCSampleLotParaSvc svc = new clsTmdQCSampleLotParaSvc())
            {
                return svc.m_lngUpdate(objQCSamplePara);
            }
        }

        public long m_lngDelete(string p_strCheckItemId, int p_intQCSmplotSeq)
        {
            using (clsTmdQCSampleLotParaSvc svc = new clsTmdQCSampleLotParaSvc())
            {
                return svc.m_lngDelete(p_strCheckItemId, p_intQCSmplotSeq);
            }
        }

        public long m_lngFind(string p_strCheckItemId, int p_intQCSmplotSeq, out clsLisQCSampleLotParaVO objQCSamplePara)
        {
            using (clsTmdQCSampleLotParaSvc svc = new clsTmdQCSampleLotParaSvc())
            {
                return svc.m_lngFind(p_strCheckItemId, p_intQCSmplotSeq, out objQCSamplePara);
            }
        }

        #endregion

        #region clsTmdQCSampleLotSvc

        public void ConstructVO(DataTable dtRow, ref clsLisQCSamplelotVO p_objQCSamplelot)
        {
            using (clsTmdQCSampleLotSvc svc = new clsTmdQCSampleLotSvc())
            {
                svc.ConstructVO(dtRow.Rows[0], ref p_objQCSamplelot);
            }
        }

        public long m_lngInsert(clsLisQCSamplelotVO p_objQCSamplelot, out int p_intSeq)
        {
            using (clsTmdQCSampleLotSvc svc = new clsTmdQCSampleLotSvc())
            {
                return svc.m_lngInsert(p_objQCSamplelot, out p_intSeq);
            }
        }

        public long m_lngUpdate(clsLisQCSamplelotVO QCBatch)
        {
            using (clsTmdQCSampleLotSvc svc = new clsTmdQCSampleLotSvc())
            {
                return svc.m_lngUpdate(QCBatch);
            }
        }

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        public long m_lngFind(int p_intSeq, out clsLisQCSamplelotVO p_objQCSamplelot)
        {
            using (clsTmdQCSampleLotSvc svc = new clsTmdQCSampleLotSvc())
            {
                return svc.m_lngFind(p_intSeq, out p_objQCSamplelot);
            }
        }

        #endregion

        #region Verify
        /// <summary>
        /// Verify
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        { return true; }
        #endregion

        #region IDispose
        /// <summary>
        /// IDispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
