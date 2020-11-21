using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using weCare.Core.Entity;

namespace Lis.Itf
{
    [ServiceContract]
    public interface ItfLis02 : weCare.Core.Itf.IWcf
    {
        #region clsQueryLIS_Svc

        /// <summary>
        /// 获取仪器参数 
        /// </summary>
        /// <param name="strData_Acquisition_Computer_IP"></param>
        /// <param name="objConfig_List"></param>
        /// <returns></returns>
        [OperationContract]
        long lngGetInstrumentSerialSetting2(string strData_Acquisition_Computer_IP, out clsLIS_Equip_Base[] objConfig_List);

        /// <summary>
        /// 获取指定检验编号的检验项目通道号数组
        /// </summary>
        /// <param name="p_strCheckSampleNO"></param>
        /// <param name="p_strCheckItemstring"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetSampleCheckItems1")]
        long m_lngGetSampleCheckItems(string p_strCheckSampleNO, out string p_strCheckItemstring);

        /// <summary>
        /// GetOttomanAppItems
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetOttomanAppItems(string barCode);

        /// <summary>
        /// GetOttomanCheckResult
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<clsDeviceReslutVO> GetOttomanCheckResult(string barCode);

        #endregion

        #region clsQueryReportGroupSvc

        // 根据报告组ID获取报告组VO
        [OperationContract]
        long m_lngGetReportGroupVOByReportGroupID(string p_strReportGroupID, out clsReportGroup_VO p_objResultVO);

        /// <summary>
        /// 根据检验标本组ID得到它所在报告组的VO 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objSampleGroupVO"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetReportGoupVOBySampleGroupID(string p_strSampleGroupID, out clsReportGroup_VO p_objReportGroupVO);

        // 获取所有的报告组
        [OperationContract]
        long m_lngGetAllReportGroup(ref clsReportGroup_VO[] objReportGroupList);

        #endregion

        #region clsQueryReportSvc

        // 根据条件组合查询报表源数据
        [OperationContract]
        long m_lngGetWorkloadReportByCondition(
            string p_strFromDat, string p_strToDat, string p_strCheckItemID, string p_strApplEmpID, string p_strApplDeptID,
            string p_strReprotorID, string p_strPatientTypeID, string p_strCheckCategoryID, out DataTable p_dtbResult);

        // Get
        [OperationContract]
        long m_lngGetReportObject(string p_strApplicationID, out clsReportObject p_objReportObject);

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
        [OperationContract]
        long m_lngQuerySampleBack(string p_strFromDate, string p_strToDate, string p_strPatientName, string p_strInHospitalNO, string p_strAppDeptID, out DataTable p_dtResult);

        #endregion

        #region clsQuerySampleGroupSvc

        // 根据申请单元ID查询标本组与申请单元的关系
        [OperationContract]
        long m_lngGetSampleGroupUnitByApplUnitID(string p_strApplUnitID, out DataTable p_dtbResult);

        /// <summary>
        /// 根据样本组ID查询该样本组下包含的申请单元
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleGroupID">＝"" || =null为查询全部</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetApplUnitBySampleGroupID(string p_strSampleGroupID, out clsLisSampleGroupUnit_VO[] p_objResultArr);

        // 根据标本组ID获取该组的标本类型
        [OperationContract]
        long m_lngGetGroupSampleTypeBySampleGroupID(string p_strSampleGroupID, out clsLisGroupSampleType_VO[] p_objResultArr);

        /// <summary>
        /// 根据样本组ID 得到样本组对应的仪器型号列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleGroupID"></param>
        /// <param name="p_strSampleGroupModelArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetSampleGroupModelArr(string p_strSampleGroupID, out string[] p_strSampleGroupModelArr);

        // 根据标本组ID获取对应的仪器型号列表
        [OperationContract]
        long m_lngGetDeviceModelArrBySampleGroupID(string p_strSampleGroupID, out clsLisSampleGroupModel_VO[] p_objResultArr);

        /// <summary>
        /// 得到样本组的列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCategory"></param>
        /// <param name="p_strSampleType"></param>
        /// <param name="p_dtpResult"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetSampleGroupList(string p_strCategory, string p_strSampleType, out DataTable p_dtpResult);

        // 根据标本ID获取标本组VO
        [OperationContract]
        long m_lngGetSampleGroupVOBySampleGroupID(string p_strSampleGroupID, out clsSampleGroup_VO p_objResultVO);

        /// <summary>
        /// 根据检验项目ID得到它所在标本组的VO,及打印顺序  
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_intPrintSeq"></param>
        /// <param name="p_objSampleGroupVO"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetSampleGoupVOByApplyUnitID(string p_strApplyUnitID, out clsSampleGroup_VO p_objSampleGroupVO);

        // 获取某一标本组下的明细资料
        [OperationContract(Name = "m_lngGetAllSampleGroupDetail2")]
        long m_lngGetAllSampleGroupDetail(string strSampleGroupID, ref clsSampleGroupDetail_VO[] objSampleGroupDetailVOList);

        #endregion

        #region clsQueryCheckGroupSvc

        // 获取所有的标本组
        [OperationContract]
        long m_lngGetAllSampleGroup(ref clsSampleGroup_VO[] objSampleGroupVOList);

        #endregion

        #region clsQuerySampleSvc

        //	根据标本号查询标本状态
        [OperationContract]
        long m_lngFindStatusBySampleID(string p_strSampleID, out int p_intStatus);

        /// <summary>
        /// 根据BarCode查询待接收的样本信息
        /// </summary>
        [OperationContract]
        long m_mthGetUnReceivedSampleByBarCode(string p_strBarCode, out clsSampleReceive_VO p_objRecord);

        // 根据条件查询已接收的标本信息
        [OperationContract]
        long m_lngGetReceivedSampleByCondition(string p_strDatFrom, string p_strDatTo,
            string p_strSampleType, string p_strAcceptEmp, string p_strPatientName, string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum, out clsSampleReceive_VO[] p_objResultArr);

        // 根据条件查询已采集但未接收的标本信息
        [OperationContract]
        long m_lngGetUnReceivedSampleByCondition(string p_strDatFrom,
            string p_strDatTo, string p_strSampleType, string p_strConlectEmp, string p_strPatientName,
            string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, string p_strSendPeopleID, string p_strInPatientNum, out clsSampleUnReceive_VO[] p_objResultArr);

        /// <summary>
        /// 返回自定义组所有申请单元 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckCategory"></param>
        /// <param name="p_dtbDetail"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetAppuserGroupDetail(string p_strCheckCategory, out DataTable p_dtbDetail);

        // 根据仪器ID查询插队记录
        [OperationContract]
        long m_lngGetSampleInterposeByDeviceID(string p_strDeviceID, out clsLisSampleInterposeVO p_objResult);

        // 根据条件查询仪器与样本之间的关系
        [OperationContract]
        long m_lngGetDeviceRelationVOArrByCondition(string p_strDeviceID, string p_strReceptDatFrom, string p_strReceptDatTo, out clsT_LIS_DeviceRelationVO[] p_objResultArr);

        // 根据标本的BarCode查询相应的标本及标本组信息
        [OperationContract(Name = "m_lngGetSampleInfoByBarCode1")]
        long m_lngGetSampleInfoByBarCode(string p_strBarCode, out DataTable p_dtbResult);

        // 获取所有的标本类型信息
        [OperationContract]
        long m_lngGetSampleTypeArr(out clsSampleType_VO[] p_objResultArr);

        // 根据标本ID查询标本和仪器标本的关系VO
        [OperationContract]
        long m_lngGetDeviceRelationVOArrBySampleID(string p_strSampleID, out clsT_LIS_DeviceRelationVO[] p_objResultArr);

        // 根据标本ID查询标本信息
        [OperationContract]
        long m_lngGetSampleVOArrBySampleID(string p_strSampleID, out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr);

        // 根据BarCode 得到样本VO
        [OperationContract]
        long m_lngGetSampleVOByBarcode(string p_strBarCode, out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr);

        // 获得全部的样品种类的列表
        [OperationContract]
        long m_lngGetSampleTypeList(out System.Data.DataTable p_dtbSampleType);

        [OperationContract]
        long m_lngGetCheckCategoryList(out System.Data.DataTable p_dtbCheckCategory);

        // 得到所有的样本状态信息列表
        [OperationContract(Name = "m_lngGetSampleState1")]
        long m_lngGetSampleState(out System.Data.DataTable p_dtbSampleState);

        /// <summary>
        /// 根据样品类别ID查询样本状态信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleTypeID"></param>
        /// <param name="p_dtbSampleState"> 
        /// </param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetSampleState2")]
        long m_lngGetSampleState(string p_strSampleTypeID, out System.Data.DataTable p_dtbSampleState);

        // 根据BarCode判断该标本是否已经核收
        [OperationContract]
        long m_lngGetReceptedSampleInfoByBarCode(string p_strBarCode, out clsT_OPR_LIS_SAMPLE_VO p_objRecord);

        // 查询在某段时间内采集且已申请但未核收的标本 童华
        [OperationContract(Name = "m_lngGetAllNotReceptSample01")]
        long m_lngGetAllNotReceptSample(string p_strFromDat, string p_strToDat, out DataTable p_dtbResult);

        // 根据日期范围查询已核收的标本
        [OperationContract]
        long m_lngGetReceptedSampleByDateRange(string p_strDeviceID, string p_strFromDat, string p_strToDat, out DataTable p_dtbResult);

        /// <summary>
        /// 查询所有未核收的标本（含未审请的） 
        /// </summary>
        /// <param name="p_objPrincipal"></param>		
        /// <param name="dtbAllNotReceptSample"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetAllNotReceptSample02")]
        long m_lngGetAllNotReceptSample(out DataTable dtbAllNotReceptSample);

        // 根据检验申请表号和检验组（第一层）查询已经采集的各标本数量
        [OperationContract]
        long m_lngGetAllSampleCountByApplFormNoAndGroupID(string strApplFormNo, string strGroupID, out DataTable dtbGroupSampleCount);

        // 查询t_opr_lis_application_detail下的Group的标本状态
        [OperationContract]
        long m_lngGetSampleStatusByGroup(string strSampleID, string strApplFormNo, out DataTable dtbGroupSample);

        // 根据检验申请表号查询已经采集的各标本数量 
        [OperationContract]
        long m_lngGetAllSampleCountByApplFormNo(string strApplFormNo, out DataTable dtbAllSampleCount);

        // 根据检验申请表上的号查出本申请已经有的样品。
        [OperationContract]
        long m_lngGetSampleInfoByFormId(string strFormNo, out System.Data.DataTable dtbSampleInfo);

        // 查询报告单上的样本信息和一些申请单信息(有些字段是ID的要查询相关表，查出ID对应的说明);
        [OperationContract]
        long m_lngGetApplSampleInfo(string p_strSampleID, out System.Data.DataTable p_dtbSample);

        // 根据Application_ID查找该申请单中各种检查所需的各类样品的数量
        [OperationContract]
        long m_lngGetSampleTotalQtyByApplicationID(string p_strApplication_ID, out System.Data.DataTable p_dtbSampleQty);

        // 根据DevID查询表t_opr_lis_device_relation（查询已核收的,STATUS_INT=1）
        [OperationContract]
        long m_lngGetDevRelationInfo(string p_strDevID, out System.Data.DataTable p_dtbDevRelation);

        // 查找某一申请单中使用某一种样品的所有检验组
        [OperationContract]
        long m_lngGetCheckGroupListByAppID_SampleType(string p_strApplication_ID, string p_strSampleTypeID, out System.Data.DataTable p_dtbCheckGroupList);

        [OperationContract]
        long m_lngGetSampleDetailByAppID_SampleType(string p_strApplication_ID, string p_strSampleTypeID, out clsSampleVO[] colSampleList);

        [OperationContract(Name = "m_lngGetSampleInfoByBarCode2")]
        long m_lngGetSampleInfoByBarCode(string strBarCode, out clsSampleVO objSampleVO);

        /// <summary>
        /// 获取样本状态
        /// </summary>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_intStatus"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngQuerySampleStatus(string p_strSampleID, out int p_intStatus, out string p_strIsSampleBack);

        #endregion

        #region clsQueryStatSvc

        // 根据条件查询学术统计的信息
        [OperationContract(Name = "m_lngGetScienceStatByCondition1")]
        long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo,
            string p_strCheckItemID, string p_strResultFrom, string p_strResultTo, string p_strAgeFrom, string p_strAgeTo, string p_strSex,
            string p_strLowCompare, string p_strCondition, string p_strUpCompare, out DataTable dtbResult);

        // 根据条件查询学术统计的信息
        [OperationContract(Name = "m_lngGetScienceStatByCondition2")]
        long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo,
            string p_strAgeFrom, string p_strAgeTo, string p_strSex, clsLisScienceStatItemQueryCondition[] p_objRecordArr, out DataTable dtbResult);

        // 根据条件查询学术统计的信息
        [OperationContract(Name = "m_lngGetScienceStatByCondition3")]
        long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo,
            string p_strAgeFrom, string p_strAgeTo, string p_strSex, clsLisScienceStatItemQueryCondition[] p_objRecordArr, out DataTable dtbHead,
            out DataTable dtbDetail);

        // 查询所有的工作组信息
        [OperationContract(Name = "m_lngGetAllWorkGroupInfo1")]
        long m_lngGetAllWorkGroupInfo(out DataTable p_dtbResult);

        // 查询所有的工作组信息
        [OperationContract(Name = "m_lngGetAllWorkGroupInfo2")]
        long m_lngGetAllWorkGroupInfo(out clsLisWorkGroup_VO[] p_objResultArr);

        // 获取所有的统计组信息
        [OperationContract]
        long m_lngGetAllStatGroupInfo(out clsLisStatGroup_VO[] p_objResultArr);

        // 获取所有的统计组申请单元信息
        [OperationContract]
        long m_lngGetAllStatGroupUnitInfo(out clsLisStatGroupUnit_VO[] p_objResultArr);

        // 根据统计组ID获取该组下的申请单元信息
        [OperationContract]
        long m_lngGetApplUnitByStatGroupID(string p_strStatGroupID, out clsApplUnit_VO[] p_objResultArr);

        // 工作量统计汇总
        [OperationContract]
        long m_lngGetStatTotalReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult);

        // 工作量统计明细
        [OperationContract]
        long m_lngGetStatDetailReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult);

        // 检验费用汇总统计
        [OperationContract]
        long m_lngGetCheckPriceTotalReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult);

        // 检验费明细统计
        [OperationContract]
        long m_lngGetCheckPriceDetailReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult);

        /// <summary>
        /// 病区标本送检量
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetSamplesCheckTotal(out DataTable p_dtbResult, string strDateFrom, string strDateTo);

        // 报表-细菌发生率统计
        /// <summary>
        /// 病区标本送检量
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetGermOccurRate(out DataTable p_dtbResult, string strDateFrom, string strDateTo);

        // 报表-细菌分布趋势

        /// <summary>
        /// 细菌分布趋势报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetGermDistributeTrend(out DataTable p_dtbResult, string strDateFrom, string strDateTo);

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
        [OperationContract]
        long m_lngGetAnimalculeCheckTotal(out DataTable p_dtbResult, string strDateFrom, string strDateTo, List<string> listSamples, List<string> listPatientArea);

        /// <summary>
        /// 统计仪器工作量 
        /// </summary>
        /// <param name="p_datStart"></param>
        /// <param name="p_datEnd"></param>
        /// <param name="p_dtStatisResult"></param>
        [OperationContract]
        void m_mthGetDeviceCheckStatis(DateTime p_datStart, DateTime p_datEnd, out DataTable p_dtStatisResult);

        [OperationContract]
        DataTable m_dtbGetSamplesList();

        [OperationContract]
        DataTable m_dtbGetDeptList();

        #endregion

        #region clsReportGroupSvc

        // 更新标本组的打印顺序
        [OperationContract]
        long m_lngSetSampleGroupPrintOrder(clsSampleGroup_VO p_objRecord);

        // 修改报告组 
        [OperationContract]
        long m_lngModifyReportGroup(ref clsReportGroup_VO objReportGroupVO);

        /// <summary>
        /// 保存报告组及其明细 
        /// </summary>
        /// <param name="objReportGroupVO"></param>
        /// <param name="objReportGroupDetailVO"></param>
        /// <returns></returns>
        [OperationContract]
        long clsReportGroupSvc_m_lngAddReportGroupAndDetail(ref clsReportGroup_VO objReportGroupVO, ref clsReportGroupDetail_VO[] objReportGroupDetailVO);

        #endregion

        #region clsReportSvc

        // Insert
        [OperationContract]
        long m_lngInsertReportObject(clsReportObject p_objReportObject);

        // Update
        [OperationContract]
        long m_lngUpdateReportObject(clsReportObject p_objReportObject);

        // Delete
        [OperationContract]
        long m_lngDeleteReportObject(string p_strApplicationID);

        // 更新体检登记表，使其状态为保存
        /// <summary>
        /// 更新体检登记表，使其状态为保存
        /// </summary>
        /// <param name="strApplicationID"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngUpdatePEReg(string strApplicationID);

        #endregion

        #region clsSampleGroupSvc

        // 批量更新标本组下申请单元的检验项目的打印顺序
        [OperationContract]
        long m_lngSetApplUnitItemPrintSeqArr(clsApplUnitDetail_VO[] p_objRecordArr);

        // 更新标本组下申请单元的检验项目的打印顺序 
        [OperationContract]
        long m_lngSetApplUnitItemPrintSeq(clsApplUnitDetail_VO p_objRecord);

        // 根据样本组ID删除样本组下的申请单元 
        [OperationContract]
        long m_lngDelSampleGroupUnitBySampleGroupID(string p_strSampleGroupID);

        // 批量新增样本组下的申请单元 
        [OperationContract]
        long m_lngAddNewSampleGroupUnitArr(string p_strSampleGroupID,
            clsLisSampleGroupUnit_VO[] p_objRecordArr);

        // 新增样本组下的申请单元
        [OperationContract]
        long m_lngAddNewSampleGroupUnit(clsLisSampleGroupUnit_VO p_objRecord);

        // 根据sample_group_id删除标本组的标本类型 
        [OperationContract]
        long m_lngDelGroupSampleTypeBySampleGroupID(string p_strSampleGroupID);


        // 批量修改标本组的标本类型列表 
        [OperationContract]
        long m_lngModifyGroupSampleTypeArr(List<clsLisGroupSampleType_VO> p_arlAdd, List<clsLisGroupSampleType_VO> p_arlRemove);

        // 批量新增标本组的标本类型列表 
        [OperationContract]
        long m_lngAddNewGroupSampleTypeArr(string p_strSampleGroupID, List<clsLisGroupSampleType_VO> p_arlAdd);

        // 新增标本组的标本类型 
        [OperationContract]
        long m_lngAddNewGroupSampleType(clsLisGroupSampleType_VO p_objRecord);

        // 删除标本组的标本类型
        [OperationContract]
        long m_lngDelGroupSampleTypeByCondition(string p_strSampleGroupID,
            string p_strSampleTypeID);

        // 根据sample_group_id删除表T_AID_LIS_SAMPLE_GROUP_MODEL的记录 
        [OperationContract]
        long m_lngDelSampleGroupModelBySampleGroupID(string p_strSampleGroupID);

        // 批量新增标本组的仪器型号列表 
        [OperationContract]
        long m_lngAddNewSampleGroupModelArr(string p_strSampleGroupNo, List<clsLisSampleGroupModel_VO> p_arlAdd);

        // 批量修改标本组的仪器型号列表
        [OperationContract]
        long m_lngSetSampleGroupModelArr(List<clsLisSampleGroupModel_VO> p_arlAdd, List<clsLisSampleGroupModel_VO> p_arlRemove);

        // 删除表T_AID_LIS_SAMPLE_GROUP_MODEL的记录 
        [OperationContract]
        long m_lngDelSampleGroupModel(clsLisSampleGroupModel_VO p_objRecord);

        // 新增记录到表T_AID_LIS_SAMPLE_GROUP_MODEL 
        [OperationContract]
        long m_lngAddNewSampleGroupModel(clsLisSampleGroupModel_VO p_objRecord);

        // 更新标本组的基本信息 
        [OperationContract]
        long m_lngSetSampleGroupInfo(ref clsSampleGroup_VO objSampleGroupVO);

        // 保存标本组及其明细
        [OperationContract(Name = "m_lngAddSampleGroupAndDetail1")]
        long m_lngAddSampleGroupAndDetail(ref clsSampleGroup_VO objSampleGroup,
            ref clsLisSampleGroupUnit_VO[] objSampleGroupUnitList, clsApplUnitDetail_VO[] p_objApplUnitDetailArr, List<clsLisSampleGroupModel_VO> p_arlAdd,
            List<clsLisSampleGroupModel_VO> p_arlRemove, List<clsLisGroupSampleType_VO> p_arlAddSampleType, List<clsLisGroupSampleType_VO> p_arlRemoveSampleType);

        #endregion

        #region clsSampleSvc

        // [U] 修改检验版本号
        [OperationContract]
        long m_lngModifyBarCode(string strSampleID, string strAppID);

        // 更新样本标志位
        [OperationContract(Name = "m_lngUpdateSampleFlag1")]
        long m_lngUpdateSampleFlag(string[] p_strSampleIDArr, int p_intSourceStatus, int p_intTargetStatus);

        /// 更新样本标志位
        [OperationContract(Name = "m_lngUpdateSampleFlag2")]
        long m_lngUpdateSampleFlag(string[] p_strSampleIDArr, int p_intSourceStatus, int p_intTargetStatus, string p_strOriginDate);

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
        [OperationContract]
        long m_lngReceiveSample(int p_intStatus, string p_strSampleID, string p_strReceiveDat,
            string p_strReceiveEmp, string p_strSendPeopleID);

        // 新增一条记录
        [OperationContract]
        long m_lngAddNewSampleInterpose(clsLisSampleInterposeVO p_objRecord);

        // 根据仪器ID更新插队记录
        [OperationContract]
        long m_lngSetSampleInterpose(clsLisSampleInterposeVO p_objRecord);

        // 插队处理
        [OperationContract]
        long m_lngSampleInterpose(clsLisSampleInterposeVO p_objRecord);

        // 更新表T_OPR_LIS_DEVICE_RELATION
        [OperationContract]
        long m_lngSetLisDeviceRelation(clsT_LIS_DeviceRelationVO p_objRecord);

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
        [OperationContract(Name = "m_lngAddNewSampleAndModifyAppSampleGroup1")]
        long m_lngAddNewSampleAndModifyAppSampleGroup(string p_strAppID, clsT_OPR_LIS_SAMPLE_VO p_objRecordVO);

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
        [OperationContract(Name = "m_lngAddNewSampleAndModifyAppSampleGroup2")]
        long m_lngAddNewSampleAndModifyAppSampleGroup(string p_strAppID, ref clsT_OPR_LIS_SAMPLE_VO p_objRecordVO);

        /// <summary>
        /// 为表 t_opr_lis_sample 新增,修改,删除 记录时用 ;
        /// 刘彬 2004.05.26
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngInsertSampleRecord(clsT_OPR_LIS_SAMPLE_VO[] p_objRecordVOArr);

        /// <summary>
        /// 为表 t_opr_lis_device_relation  新增 记录时用 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngAddNewDeviceRelation(clsT_LIS_DeviceRelationVO p_objRecord);

        /// <summary>
        /// 删除仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strRelationDate"></param>
        /// <param name="p_strSeq"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngDeleteDeviceRelation1")]
        long m_lngDeleteDeviceRelation(
            string p_strDeviceID, string p_strRelationDate, string p_strSeq);

        /// <summary>
        /// 删除仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRelation"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngDeleteDeviceRelation2")]
        long m_lngDeleteDeviceRelation(clsT_LIS_DeviceRelationVO p_objRelation);

        /// <summary>
        /// 通过样品ID删除仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleID"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDelDevicRelation(string p_strSampleID);

        /// <summary>
        /// 删除仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSourceVO"></param>
        /// <param name="p_objTargetVO"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngModifyBind(
            clsT_LIS_DeviceRelationVO p_objSourceVO, clsT_LIS_DeviceRelationVO p_objTargetVO);

        // 核收时更新样本表的标志位信息
        [OperationContract]
        long m_lngModifySampleInfoOnRecepting(clsT_OPR_LIS_SAMPLE_VO p_objRecord);


        // 核收仪器标本
        [OperationContract]
        long m_lngReceptSample(clsT_OPR_LIS_SAMPLE_VO p_objSampleVO,
            clsT_LIS_DeviceRelationVO p_objDeviceRelationVO);

        // 审核样本,并修改相关的所有标志位     
        [OperationContract]
        long m_lngAuditingSample(string p_strSampleID);

        // 根据DeviceID和DeviceSampleID设置表t_opr_lis_device_relation的标本记录状态
        [OperationContract]
        long m_lngSetStatus(ref clsDeviceRelation_VO objDeviceRelationVO);

        // 根据DeviceID和DeviceSampleID更新t_opr_lis_device_relation的标本架位号
        [OperationContract]
        long m_lngSetPositionANDSampleID(ref clsDeviceRelation_VO objDeviceRelationVO, ref clsSampleVO objSampleVO);

        // 增加一个样本到样本表中。Old
        [OperationContract]
        long m_lngAddSample(ref clsSampleVO aSampleVO);

        // 根据SampleBarCode设置标本的状态
        [OperationContract]
        long m_lngSetSampleStatusBySampleBarCode(string p_strSampleBarCode, int p_intSampleStatus);

        // 根据SampleId设置标本的状态
        [OperationContract]
        long m_lngSetSampleStatusBySampleId(string p_strSampleId, int p_intSampleStatus);

        [OperationContract]
        long m_lngInsertSampleFeedBack(clslissample_feedback p_objSampleFeedBack);

        // 修改采样人员
        /// <summary>
        /// 修改采样人员
        /// </summary>
        /// <param name="p_objPrincil"></param>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngInsertCollector(string p_strEmpId, string p_strSampleId, string p_strApplicationID);

        /// <summary>
        /// 通过申请单号更改t_opr_lis_application表内的打印状态
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppliID"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngUpdateApplPrint(string p_strAppliID, bool blnPrint);

        /// <summary>
        /// 修改采样时间
        /// </summary>
        /// <param name="lstBarCode"></param>
        /// <param name="collTime"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateCollectorTime(List<string> lstBarCode, DateTime collTime);

        #endregion

        #region clsStatSvc

        // 新增工作组 
        [OperationContract]
        long m_lngAddNewWorkGroup(clsLisWorkGroup_VO p_objRecord);

        // 更新工作组信息
        [OperationContract]
        long m_lngModifyWorkGroup(clsLisWorkGroup_VO p_objRecord);

        // 删除工作组信息 
        [OperationContract]
        long m_lngDelWorkGroup(string p_strWorkGroupID, string p_strStatus);

        // 新增统计组基本信息 
        [OperationContract]
        long m_lngAddNewStatGroupBaseInfo(clsLisStatGroup_VO p_objRecord);

        // 新增统计组和申请单元的关系 
        [OperationContract]
        long m_lngAddNewStatGroupUnit(clsLisStatGroupUnit_VO p_objRecord);

        // 新增统计组及其明细 
        [OperationContract]
        long m_lngAddNewStatGroup(clsLisStatGroup_VO p_objStatGroup,
            clsLisStatGroupUnit_VO[] p_objStatGroupUnitArr);

        // 更新统计组基本信息
        [OperationContract]
        long m_lngSetStatGroupBaseInfo(clsLisStatGroup_VO p_objRecord);

        // 根据统计组ID删除该组下的申请单元 
        [OperationContract]
        long m_lngDelStatGroupUnitByStatGroupID(string p_strStatGroupID);

        // 更新统计组及明细
        [OperationContract]
        long m_lngModifyStatGroup(clsLisStatGroup_VO p_objStatGroup,
            clsLisStatGroupUnit_VO[] p_objStatGroupUnitArr);

        // 删除统计组 
        [OperationContract]
        long m_lngDelStatGroup(string p_strStatGroupID);

        #endregion

        #region clsWorkStatsticSvc

        /// <summary>
        /// 获取开单医生或检验者
        /// </summary>
        /// <param name="dtbEmp"></param>
        /// <returns></returns>
        [OperationContract]
        long lngGetEmployee(out DataTable dtbEmp);

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="dtbDept"></param>
        /// <returns></returns>
        [OperationContract]
        long lngGetDept(out DataTable dtbDept);

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
        [OperationContract]
        long lngGetWorkStatstic(int p_intQueryType, DateTime p_dtDateFrom, DateTime p_dtDateTO, int p_strQuery, string p_strCondition, out DataTable dtbResult);

        #endregion

        #region clsSchBaseInfoSvc

        // 返回检验项目树
        [OperationContract]
        long m_lngGetCheckItemTree(out clsLISUserGroupNode root);

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
        [OperationContract]
        long m_lngFindQCBatchCombinatorial(clsLisQCBatchSchVO p_objCondition, out clsLisQCBatchVO[] p_objRecordArr);

        #endregion

        #region clsTmdCheckMethodSvc

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngInsert1")]
        long m_lngInsert(clsLisCheckMethodVO p_objCheckMethod, out int p_intSeq);

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngUpdate1")]
        long m_lngUpdate(clsLisCheckMethodVO p_objCheckMethod);

        // DELETE
        [OperationContract(Name = "m_lngDelete3")]
        long m_lngDelete(int p_intSeq);

        /// <summary>
        /// 根据检验方法序号查找
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngFind01")]
        long m_lngFind(int p_intSeq, out clsLisCheckMethodVO p_objCheckMethod);

        [OperationContract(Name = "m_lngFind02")]
        long m_lngFind(out clsLisCheckMethodVO[] p_objResultArr);

        [OperationContract(Name = "ConstructVO1")]
        void ConstructVO(DataTable dtRow, ref clsLisCheckMethodVO p_objCheckMethod);

        #endregion

        #region clsTmdConcentrationSvc

        [OperationContract(Name = "ConstructVO2")]
        void ConstructVO(DataTable dtRow, ref clsLisConcentrationVO p_objConcentration);

        [OperationContract(Name = "m_lngInsert2")]
        long m_lngInsert(clsLisConcentrationVO p_objConcentration, out int p_intSeq);

        [OperationContract(Name = "m_lngUpdate2")]
        long m_lngUpdate(clsLisConcentrationVO QCBatch);

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        [OperationContract(Name = "m_lngFind03")]
        long m_lngFind(int p_intSeq, out clsLisConcentrationVO p_objConcentration);

        [OperationContract(Name = "m_lngFind04")]
        long m_lngFind(out clsLisConcentrationVO[] p_objResultArr);

        #endregion

        #region clsTmdQCRulesSvc

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngInsert3")]
        long m_lngInsert(clsLisQCRuleVO p_objRule, out int p_intSeq);

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngUpdate3")]
        long m_lngUpdate(clsLisQCRuleVO p_objRule);

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        /// <summary>
        /// 根据检验方法序号查找
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngFind05")]
        long m_lngFind(int p_intSeq, out clsLisQCRuleVO p_objRule);

        [OperationContract(Name = "m_lngFind06")]
        long m_lngFind(out clsLisQCRuleVO[] p_objResultArr);

        /// <summary>
        /// 从数据库中构造实体
        /// </summary>
        /// <param name="p_dtrSource"></param>
        /// <param name="p_objRule"></param>
        [OperationContract(Name = "ConstructVO3")]
        void ConstructVO(DataTable dtRow, ref clsLisQCRuleVO p_objRule);

        #endregion

        #region clsTmdVendorSvc

        [OperationContract(Name = "m_lngInsert4")]
        long m_lngInsert(clsLisVendorVO p_objVendor, out int p_intSeq);

        /// <summary>
        /// 增加一条检测方法
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckMethod">检测方法实体</param>
        /// <param name="p_intSeq">检测方法序号</param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngUpdate4")]
        long m_lngUpdate(clsLisVendorVO p_objVendor);

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        [OperationContract(Name = "m_lngFind07")]
        long m_lngFind(int p_intSeq, out clsLisVendorVO p_objVendor);

        [OperationContract(Name = "m_lngFind08")]
        long m_lngFind(out clsLisVendorVO[] p_objResultArr);

        /// <summary>
        /// 从数据库中构造实体
        /// </summary>
        /// <param name="p_dtrSource"></param>
        /// <param name="p_objVendor"></param>
        [OperationContract(Name = "ConstructVO4")]
        void ConstructVO(DataTable dtRow, ref clsLisVendorVO p_objVendor);

        #endregion

        #region clsTmdWorkGroupSvc

        [OperationContract(Name = "m_lngInsert5")]
        long m_lngInsert(clsLisWorkGroupVO p_objWorkGroup, out int p_intSeq);

        [OperationContract(Name = "m_lngUpdate5")]
        long m_lngUpdate(clsLisWorkGroupVO p_objWorkGroup);

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);


        [OperationContract(Name = "m_lngFind09")]
        long m_lngFind(int p_intSeq, out clsLisWorkGroupVO p_objWorkGroup);

        [OperationContract(Name = "m_lngFind10")]
        long m_lngFind(out clsLisWorkGroupVO[] p_objResultArr);

        [OperationContract(Name = "ConstructVO5")]
        void ConstructVO(DataTable dtRow, ref clsLisWorkGroupVO p_objWorkGroup);

        #endregion

        #region clsTmdQCBatchConcentrationSvc

        [OperationContract(Name = "ConstructVO6")]
        void ConstructVO(DataTable dtRow, ref clsLisQCConcentrationVO p_objQCConcentration);

        [OperationContract(Name = "m_lngInsert6")]
        long m_lngInsert(clsLisQCConcentrationVO p_objQCConcentration);
        [OperationContract(Name = "m_lngUpdate6")]
        long m_lngUpdate(clsLisQCConcentrationVO p_objQCConcentration);
        [OperationContract(Name = "m_lngDelete1")]
        long m_lngDelete(int p_intQCBatchSeq, int p_intConcentrationSeq);
        [OperationContract(Name = "m_lngFind11")]
        long m_lngFind(int p_intQCBatchSeq, int p_intConcentrationSeq, out clsLisQCConcentrationVO p_objQCConcentration);

        [OperationContract(Name = "m_lngFind12")]
        long m_lngFind(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr);
        [OperationContract]
        long m_lngFindDeleted(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr);

        /// <summary>
        /// 查找指定的质控浓度
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intQCBatchSeqArr"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngFind13")]
        long m_lngFind(int[] p_intQCBatchSeqArr, out clsLisQCConcentrationVO[] p_objResultArr);

        [OperationContract(Name = "m_lngFind14")]
        long m_lngFind(out clsLisQCConcentrationVO[] p_objResultArr);

        #endregion

        #region clsTmdQCBatchSvc

        [OperationContract(Name = "ConstructVO7")]
        void ConstructVO(DataTable dtRow, ref clsLisQCBatchVO p_objQCBatch);

        [OperationContract(Name = "m_lngInsert7")]
        long m_lngInsert(clsLisQCBatchVO p_objQCBatch, out int p_intSeq);

        /// <summary>
        /// 保存质控批类
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objQCBatchArr"></param>
        /// <param name="p_intSeqArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngInsertByArr1")]
        long m_lngInsertByArr(clsLisQCBatchVO[] p_objQCBatchArr, out int[] p_intSeqArr);

        [OperationContract(Name = "m_lngUpdate7")]
        long m_lngUpdate(clsLisQCBatchVO QCBatch);

        /// <summary>
        /// 更新质控样本的结果
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objQCDataArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngUpdateByArr(clsLisQCDataVO[] p_objQCDataArr);

        //// DELETE
        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        /// <summary>
        /// 删除质控样本结果数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeqArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDeleteByArr(int[] p_intSeqArr);

        [OperationContract(Name = "m_lngFind15")]
        long m_lngFind(int p_intSeq, bool p_blnExtFind, out clsLisQCBatchVO p_objQCBatch);

        [OperationContract(Name = "m_lngFind16")]
        long m_lngFind(out clsLisQCBatchVO[] p_objResultArr);

        /// <summary>
        /// 查找定质控批序号的质控设置
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeqArr"></param>
        /// <param name="p_blnExtFind"></param>
        /// <param name="p_objQCBatchArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngFind17")]
        long m_lngFind(int[] p_intSeqArr, bool p_blnExtFind, out clsLisQCBatchVO[] p_objQCBatchArr);

        [OperationContract]
        long m_lngQueryDeviceSampleID(int p_intBatchSeq, out string p_strSampleId);

        /// <summary>
        /// m_lngQueryDeviceSampleID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intBatchSeq"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngReceiveDeviceQCDataBySampleID(string p_strSampleID, string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr);

        [OperationContract(Name = "m_lngFindQCBatch1")]
        long m_lngFindQCBatch(int[] p_intSeqArr, bool p_blnExtFind, out clsLisQCBatchVO[] p_objQCBatchArr);

        [OperationContract(Name = "m_lngFindQCConcentration1")]
        long m_lngFindQCConcentration(int[] p_intQCBatchSeqArr, out clsLisQCConcentrationVO[] p_objResultArr);

        [OperationContract(Name = "m_lngFindQCData1")]
        long m_lngFindQCData(out clsLisQCDataVO[] p_objResultArr, int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd);

        [OperationContract(Name = "ConstructVO8")]
        void ConstructVO(DataTable dtRow, ref clsLisQCDataVO p_objQCData);

        [OperationContract(Name = "m_lngFindQCReport1")]
        long m_lngFindQCReport(int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr);

        [OperationContract(Name = "m_lngFindQCBatch2")]
        long m_lngFindQCBatch(int intSeq, bool blnExtFind, out clsLisQCBatchVO qcBatchVo);

        [OperationContract(Name = "m_lngFindQCConcentration2")]
        long m_lngFindQCConcentration(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr);

        [OperationContract(Name = "m_lngFindQCData2")]
        long m_lngFindQCData(out clsLisQCDataVO[] p_objResultArr, int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd);

        [OperationContract(Name = "m_lngFindQCReport2")]
        long m_lngFindQCReport(int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr);

        [OperationContract]
        long m_lngUpdateQCData(clsLisQCDataVO QCBatch);

        [OperationContract]
        long m_lngInsertQCData(clsLisQCDataVO p_objQCData, out int p_intSeq);

        [OperationContract]
        long m_lngDeleteQCData(int p_intSeq);

        [OperationContract]
        long m_lngSaveAllQCData(clsLisQCDataVO[] p_objInsertArr, clsLisQCDataVO[] p_objUpdateArr, int[] p_intDelArr, out int[] p_intISeqArr);

        [OperationContract]
        long m_lngUpdateQCDataByArr(clsLisQCDataVO[] p_objQCDataArr);

        [OperationContract]
        long m_lngInsertQCDataByArr(clsLisQCDataVO[] p_objQCDataArr, out int[] p_intSeqArr);

        [OperationContract]
        long m_lngDeleteQCDataByArr(int[] p_intSeqArr);

        [OperationContract]
        long m_lngReceiveDeviceQCData(string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr);

        [OperationContract]
        long m_lngUpdateSDXCV(clsLisQCConcentrationVO p_objQCConcentration);

        [OperationContract]
        long m_lngGetSysParam(string p_strParam, out string p_strParamValue);

        [OperationContract(Name = "m_lngFindQCRule1")]
        long m_lngFindQCRule(int p_intSeq, out clsLisQCRuleVO p_objRule);

        [OperationContract(Name = "m_lngFindQCRule2")]
        long m_lngFindQCRule(out clsLisQCRuleVO[] p_objResultArr);

        [OperationContract]
        long m_lngGetDeviceQCCheckItemByID(string p_strDeviceID, out clsLISCheckItemNode[] p_objResultArr);

        [OperationContract]
        long m_lngInsertQCBatch(clsLisQCBatchVO p_objQCBatch, out int p_intSeq);

        [OperationContract]
        long m_lngInsertQCBatchByArr(clsLisQCBatchVO[] p_objQCBatchArr, out int[] p_intSeqArr);

        [OperationContract]
        long m_lngUpdateQCBatch(clsLisQCBatchVO QCBatch);

        [OperationContract]
        long m_lngDeleteQCBatch(int p_intSeq);

        [OperationContract(Name = "m_lngInsertBatchSet1")]
        long m_lngInsertBatchSet(DataTable p_dtbResult);

        [OperationContract(Name = "m_lngInsertBatchSet2")]
        long m_lngInsertBatchSet(List<clsLisQCBatchVO> p_lstResult, List<clsLisQCConcentrationVO> p_lstContion);

        //[OperationContract]
        //void ConstructVO(DataRow p_dtrSource, ref clsLisQCDataVO p_objQCData);

        #endregion

        #region clsTmdQCDataSvc

        [OperationContract(Name = "m_lngInsert8")]
        long m_lngInsert(clsLisQCDataVO p_objQCData, out int p_intSeq);

        /// <summary>
        /// 保存质控样本的结果
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objQCDataArr"></param>
        /// <param name="p_intSeqArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngInsertByArr2")]
        long m_lngInsertByArr(clsLisQCDataVO[] p_objQCDataArr, out int[] p_intSeqArr);

        [OperationContract(Name = "m_lngUpdate8")]
        long m_lngUpdate(clsLisQCDataVO QCBatch);

        [OperationContract(Name = "m_lngFind18")]
        long m_lngFind(out clsLisQCDataVO[] p_objResultArr, int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd);

        [OperationContract(Name = "m_lngFind19")]
        long m_lngFind(int p_intSeq, out clsLisQCDataVO p_objQCData);

        [OperationContract(Name = "m_lngFind20")]
        long m_lngFind(out clsLisQCDataVO[] p_objResultArr);

        [OperationContract(Name = "m_lngFind21")]
        long m_lngFind(out clsLisQCDataVO[] p_objResultArr, int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd);

        [OperationContract]
        long m_lngSaveAll(clsLisQCDataVO[] p_objInsertArr, clsLisQCDataVO[] p_objUpdateArr, int[] p_intDelArr, out int[] p_intISeqArr);

        [OperationContract]
        long m_lngInsertQCReport(clsLisQCReportVO p_objQCReport, out int p_intSeq);

        [OperationContract]
        long m_lngUpdateQCReport(clsLisQCReportVO QCBatch);

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
        [OperationContract]
        long m_lngGetDeviceQCDataBySampleID(string p_strSampleID,
            string p_strStartDat, string p_strEndDat, int[] p_intBatchSeqArr, out clsLisQCDataVO[] p_objQCDataArr);

        #endregion

        #region clsTmdQCReportSvc

        [OperationContract(Name = "ConstructVO9")]
        void ConstructVO(DataTable dtRow, ref clsLisQCReportVO p_objQCReport);

        [OperationContract(Name = "m_lngInsert9")]
        long m_lngInsert(clsLisQCReportVO p_objQCReport, out int p_intSeq);

        [OperationContract(Name = "m_lngUpdate9")]
        long m_lngUpdate(clsLisQCReportVO QCBatch);

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);


        [OperationContract(Name = "m_lngFind22")]
        long m_lngFind(int[] p_intQCBatchSeqArr, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr);

        [OperationContract(Name = "m_lngFind23")]
        long m_lngFind(int p_intSeq, out clsLisQCReportVO p_objQCReport);

        [OperationContract(Name = "m_lngFind24")]
        long m_lngFind(out clsLisQCReportVO[] p_objResultArr);

        [OperationContract(Name = "m_lngFind25")]
        long m_lngFind(int p_intQCBatchSeq, DateTime p_datBegin, DateTime p_datEnd, enmQCStatus p_status, out clsLisQCReportVO[] p_objQCReportArr);

        #endregion

        #region clsTmdQCSampleLotParaSvc

        [OperationContract(Name = "ConstructVO10")]
        void ConstructVO(DataTable dtRow, ref clsLisQCSampleLotParaVO objQCSamplePara);

        [OperationContract(Name = "m_lngInsert10")]
        long m_lngInsert(clsLisQCSampleLotParaVO clsLisQCSamplePara);

        [OperationContract(Name = "m_lngUpdate10")]
        long m_lngUpdate(clsLisQCSampleLotParaVO objQCSamplePara);

        [OperationContract(Name = "m_lngDelete2")]
        long m_lngDelete(string p_strCheckItemId, int p_intQCSmplotSeq);

        [OperationContract(Name = "m_lngFind26")]
        long m_lngFind(string p_strCheckItemId, int p_intQCSmplotSeq, out clsLisQCSampleLotParaVO objQCSamplePara);

        #endregion

        #region clsTmdQCSampleLotSvc

        [OperationContract(Name = "ConstructVO11")]
        void ConstructVO(DataTable dtRow, ref clsLisQCSamplelotVO p_objQCSamplelot);

        [OperationContract(Name = "m_lngInsert11")]
        long m_lngInsert(clsLisQCSamplelotVO p_objQCSamplelot, out int p_intSeq);

        [OperationContract(Name = "m_lngUpdate11")]
        long m_lngUpdate(clsLisQCSamplelotVO QCBatch);

        //[OperationContract]
        //long m_lngDelete( int p_intSeq);

        [OperationContract(Name = "m_lngFind27")]
        long m_lngFind(int p_intSeq, out clsLisQCSamplelotVO p_objQCSamplelot);

        #endregion


    }
}
