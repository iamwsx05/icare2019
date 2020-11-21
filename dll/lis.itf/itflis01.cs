using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using weCare.Core.Entity;

namespace Lis.Itf
{
    [ServiceContract]
    public interface ItfLis01 : weCare.Core.Itf.IWcf
    {
        #region bizCriticalValue

        // 获取申请单信息
        [OperationContract]
        EntityCriticalMain GetLisApplication(string applyId);

        // 保存检验危急值
        [OperationContract]
        int SaveCriticalValue(string applyId, string confirmOperID, DateTime confirmDate, List<EntityCriticalLis> lstItem, bool isYG, bool isValid);

        // 删除检验危急值
        [OperationContract]
        int DelCriticalValue(string applyId);

        /// <summary>
        /// 获取危急值列表
        /// </summary>
        /// <param name="typeId">1 门诊; 2 住院</param>
        /// <param name="qid"></param>
        /// <param name="lstMain" ></param>
        /// <param name="lstDet"></param>
        [OperationContract]
        void GetCriticalvalList(int typeId, string qid, out List<EntityCriticalMain> lstMain, out List<EntityCriticalLis> lstDet);

        /// <summary>
        /// 临床应答
        /// </summary>
        /// <param name="respVo"></param>
        /// <returns></returns>
        [OperationContract]
        int ResponseCriticalValue(EntityCriResponse respVo);

        /// <summary>
        /// 检验科认证
        /// </summary>
        /// <param name="cvmId"></param>
        /// <param name="empId"></param>
        /// <param name="desc"></param>
        [OperationContract]
        int LisVerify(decimal cvmId, string empId, string desc);

        /// <summary>
        /// 获取危急值监控类型
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="lstDeptId"></param>
        /// <returns></returns>
        [OperationContract]
        EntityCriMonitorType GetCriMonitorType(string empId, List<string> lstDeptId);

        /// <summary>
        /// 获取危急值监控循环时间
        /// </summary>
        /// <returns>毫秒, 0 -- 不监控</returns>
        [OperationContract]
        int GetCriTime();

        /// <summary>
        /// 获取危急值条目
        /// </summary>
        /// <param name="typeId">1 门诊; 2 住院</param>
        /// <param name="qid"></param>
        /// <returns></returns>
        [OperationContract]
        List<EntityCriticalMain> GetCriList(int typeId, string qid);

        /// <summary>
        /// 获取危急值明细
        /// </summary>
        /// <param name="cvmId"></param>
        /// <returns></returns>
        [OperationContract]
        List<EntityCriticalLis> GetCriDetail(decimal cvmId);

        /// <summary>
        /// 获取pacs危急值
        /// </summary>
        /// <param name="cvmId"></param>
        /// <returns></returns>
        [OperationContract]
        EntityCriticalPacs GetCriPacs(decimal cvmId);

        /// <summary>
        /// 危急值汇总报表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptIdArr"></param>
        /// <param name="isYG">是否院感</param>
        /// <returns></returns>
        [OperationContract]
        List<EntityCriReport> GetCriReport(DateTime startDate, DateTime endDate, string deptIdArr, bool isYG);

        /// <summary>
        /// 获取指定病人的危急值记录
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [OperationContract]
        List<EntityCriticalMain> GetCriListByPid(string pid);

        /// <summary>
        /// 取消危急值
        /// </summary>
        /// <param name="cancelVo"></param>
        /// <returns></returns>
        [OperationContract]
        int CancelCriticalValue(EntityCriCancel cancelVo);

        /// <summary>
        /// 获取体检申请项目
        /// </summary>
        /// <param name="regNo"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAppItem(string regNo);

        /// <summary>
        /// 获取院感危急值
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetCriYG();

        [OperationContract]
        int SaveCriYG(List<string> lstRefDesc);

        /// <summary>
        /// 获取检验科室电脑IP
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> GetLisPC();

        #endregion

        #region clsAdvis2120Svc

        /// <summary>
        /// 添加仪器检验项目与检验项目对应关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngAddNewCheckItemDeviceCheckItem(clsLisCheckItemDeviceCheckItem_VO p_objRecord);

        /// <summary>
        /// 修改仪器检验项目与检验项目对应关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSourceCheckItemID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngModifyCheckItemDeviceCheckItem(string p_strSourceCheckItemID, clsLisCheckItemDeviceCheckItem_VO p_objRecord);

        /// <summary>
        /// 删除仪器检验项目与检验项目对应关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSourceCheckItemID"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDelCheckItemDeviceCheckItem(string p_strSourceCheckItemID);

        /// <summary>
        /// 插入报告单to表t_opr_lis_check_result
        /// </summary>
        /// <param name="objPrincipal"></param>
        /// <param name="p_intNum"></param>
        /// <param name="p_objResultList"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngInsertReport(int intNum, List<clsAdvia2120ResultInf_VO> p_objResultList, out int p_intInsertNum);

        /// <summary>
        /// 为表 t_opr_lis_app_report 新增,修改,删除 记录时用
        /// </summary>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngInsertAppReportRecord2")]
        long m_lngInsertAppReportRecord(List<clsT_OPR_LIS_APP_REPORT_VO> p_objRecordVOList);

        /// <summary>
        /// 调用本方法时,必需传入 p_strSampleIDArr 中的所有的样本的所有检验项目结果,且只能传入
        /// 在 p_strSampleIDArr 列表之中的样本的检验项目结果;
        /// </summary>
        /// <param name="p_strSampleIDArr"></param>
        /// <param name="p_strOriginDate"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngAddCheckResultList2")]
        long m_lngAddCheckResultList(List<string> p_strSampleIDList, string p_strOriginDate);

        #endregion

        #region clsAppGroupSvc

        // 更新表t_aid_lis_appluser_group的has_child_group字段
        [OperationContract]
        long m_lngUpdApplUserGroup(string p_strHasChildGroup, string p_strApplUserGroupID);

        // 查询所有的自定义组的子组信息
        [OperationContract]
        long m_lngGetAllApplUserGroupRelation(out clsApplUserGroupRelation_VO[] p_objResultArr);

        // 查询一个自定义组下(不含自定义组);的所有检验项目明细
        [OperationContract]
        long m_lngGetCheckItemInApplGroupDetailByApplUserGroupID(string strApplUserGroupID, out DataTable dtbCheckItem);

        // 查询一个自定义组下(含自定义组);的所有检验项目明细
        [OperationContract]
        long m_lngGetCheckItemApplGroupRelationByApplUserGroupID(string strApplUserGroupID, out DataTable dtbCheckItem);

        // 更新表t_aid_lis_appluser_group中的字段
        [OperationContract]
        long m_lngSetApplUserGroup(ref clsApplUserGroup_VO objApplUserGroupVO);

        // 删除自定义组合的明细和关系
        [OperationContract]
        long m_lngDelApplUserGroupDetailAndRelation(string strApplUserGroupID);

        // 删除自定义组合相关的所有信息
        [OperationContract]
        long m_lngDelApplUserGroupInfo(string strApplUserGroupID, string strParentUserGroupID);

        // 删除表t_aid_lis_appluser_group_relate的自定义组合关系
        [OperationContract(Name = "m_lngDelApplUserGroupRelation1")]
        long m_lngDelApplUserGroupRelation(string strApplUserGroupID, string strParentUserGroupId);

        [OperationContract(Name = "m_lngDelApplUserGroupRelation2")]
        long m_lngDelApplUserGroupRelation(string strApplUserGroupID);

        // 删除表t_aid_lis_appluser_group_detail的自定义组合明细
        [OperationContract]
        long m_lngDelApplUserGroupDetail(string strApplUserGroupID);

        // 删除表t_aid_lis_appluser_group的自定义组合
        [OperationContract]
        long m_lngDelApplUserGroup(string strApplUserGroupID);

        // 获取所有的自定义组合下的所有申请单元信息
        [OperationContract]
        long m_lngGetAllUserGroupApplUnitID(out DataTable dtbApplUnit);

        // 保存用户自定义申请组合及其明细、关系
        [OperationContract]
        long m_lngAddApplUserGroupAndDetail(ref clsApplUserGroup_VO objApplUserGroupVO, ref clsApplUserGroupDetail_VO[] objApplUserGroupDetailList, ref clsApplUserGroupRelation_VO[] objApplUserGroupRelation, clsApplUserGroupRelation_VO p_objParentRelation);

        // 保存记录到t_aid_lis_appuser_relate
        [OperationContract]
        long m_lngAddApplUserRelation(ref clsApplUserGroupRelation_VO objApplUserGroupRelationVO);

        // 保存一条记录到t_aid_lis_appuser_group_detail
        [OperationContract]
        long m_lngAddApplUserGroupDetail(ref clsApplUserGroupDetail_VO objApplUserGroupDetailVO);

        // 保存一条记录到t_aid_lis_appuser_group
        [OperationContract]
        long m_lngAddAppUserGroup(ref clsApplUserGroup_VO objApplUserGroup);

        // 根据用户自定义组ID查询其包含的申请单元信息
        [OperationContract]
        long m_lngGetApplUnitByUserGroupID(string strUserGroupID, out clsApplUnit_VO[] objApplUnitVOList);

        // 根据用户自定义组ID查询其子组信息
        [OperationContract]
        long m_lngGetChildGroupByUserGroupID(string strUserGroupID, out clsApplUserGroup_VO[] objAppUserGroupVOList);

        #endregion

        #region clsApplicationSvc

        // 更新申请单到发状态
        [OperationContract]
        long m_lngSendApplictions(string[] p_strApplicationIDArr);

        //  作废申请单的样本
        [OperationContract]
        long m_lngAddBlankOutInfo(clsLisApplMainVO p_objApplMainVO, clsBlankOutApplicationVO p_objBlankOutInfo);

        // 作废样本子方法
        [OperationContract]
        long m_lngAddNewApplForBlankOut(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr);

        [OperationContract]
        long m_lngAddNewAppAndSampleInfoWithBarcode(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr);

        [OperationContract]
        long m_lngAddNewAppAndSampleInfoWithoutReceive(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr);

        [OperationContract]
        long m_lngInsertAttachrelation(clsLisApplMainVO p_objMainVO);

        [OperationContract]
        long m_lngPISApplication(clsPISApplicationInfoToLIS p_objRecord, out string[] p_strApplicationIDArr);

        /// <summary>
        /// 根据申请单ID作废相应的仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strOringinDate"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDeleteDeviceRelationByApplicationID(string p_strApplicationID, string p_strOringinDate);

        // 修改申请单病人信息并相应修改样本信息
        [OperationContract]
        long m_lngSetApplicationAndSamplePatientInfo(clsLisApplMainVO p_objApplVO);

        /// <summary>
        /// 为表 添加一批记录到申请单申请单元项目表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngAddNewAppUintItemArr(clsLisAppUnitItemVO[] p_objRecordVOArr);

        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strOpID"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngDeleteApp(string p_strAppID, string p_strOpID);

        /// <summary>
        /// 联合查询申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSchVO">承载联合查询条件的VO,其中值为NULL的成员会被必忽略</param>
        /// <param name="p_objAppVOArr">返回的承载申请单信息的数组</param>
        /// <returns>0:失败;1:成功</returns>
        [OperationContract]
        long m_lngGetAppInfoByCondition(clsLISApplicationSchVO p_objSchVO, out clsLisApplMainVO[] p_objAppVOArr);

        /// <summary>
        /// 根据申请单ID查询申请单下的报告组
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetAppReportVOArrByApplicationID(string p_strApplicationID, out clsT_OPR_LIS_APP_REPORT_VO[] p_objResultArr);

        /// <summary>
        /// 根据申请单ID和报告组ID查询申请申请单下的标本组
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strReportGroupID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetAppSampleGroupVOArr1")]
        long m_lngGetAppSampleGroupVOArr(string p_strApplicationID, string p_strReportGroupID, out clsT_OPR_LIS_APP_SAMPLE_VO[] p_objResultArr);

        /// <summary>
        /// 根据申请单ID查询申请单下的标本组
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetAppSampleGroupVOArr2")]
        long m_lngGetAppSampleGroupVOArr(string p_strApplicationID, out clsT_OPR_LIS_APP_SAMPLE_VO[] p_objResultArr);

        /// <summary>
        /// 根据申请单ID、样本组ID和报告组ID查询申请单下的检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID">申请单ID</param>
        /// <param name="p_strSampleGroupID">样本组ID</param>
        /// <param name="p_strReportGroupID">报告组ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetAppCheckItemVOArr1")]
        long m_lngGetAppCheckItemVOArr(string p_strApplicationID, string p_strSampleGroupID, string p_strReportGroupID, out clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objResultArr);

        /// <summary>
        /// 根据申请单查询申请单下的检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID">申请单ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetAppCheckItemVOArr2")]
        long m_lngGetAppCheckItemVOArr(string p_strApplicationID, out clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objResultArr);

        /// <summary>
        /// 根据申请单ID查询申请单下的申请单元
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID">申请单ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetAppApplyUnitVOByApplicationID(string p_strApplicationID, out clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objResultArr);

        /// <summary>
        /// 为表 T_OPR_LIS_APP_CHECK_ITEM 新增记录时用
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngAddNewAppCheckItem(clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objRecordVOArr);

        /// <summary>
        /// 为表 T_OPR_LIS_APP_SAMPLE 新增记录时用 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngAddNewAppSampleGroup(clsT_OPR_LIS_APP_SAMPLE_VO[] p_objRecordVOArr);

        /// <summary>
        /// 为表 t_opr_lis_app_report 新增,修改,删除 记录时用 
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngInsertAppReportRecord1")]
        long m_lngInsertAppReportRecord(clsT_OPR_LIS_APP_REPORT_VO[] p_objRecordVOArr);

        // m_lngAddNewAppl 增加新的检验申请记录(修改,删除时都为新增记录);
        [OperationContract]
        long m_lngAddNewAppl(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO);

        // 增加-组新的检验申请信息(包含报告组,申请单元,样本组,检验项目在内的全部信息);
        [OperationContract(Name = "m_lngAddNewAppInfo2")]
        long m_lngAddNewAppInfo(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr);

        // 增加-组新的检验申请信息(包含报告组,申请单元,样本组,检验项目,和样本在内的全部信息);
        [OperationContract]
        long m_lngAddNewAppAndSampleInfo(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr);

        // 增加-组新的检验申请信息(包含报告组,申请单元,样本组,检验项目,和样本在内的全部信息); 解决三层爆错的问题 加上了一个ref
        [OperationContract]
        long m_lngAddNewAppAndSampleInfoNew(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, ref clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, ref clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, ref clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, ref clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, ref clsLisAppUnitItemVO[] p_objAppUnitItemArr);

        // 确认报告
        [OperationContract]
        long m_lngConfirmAppReport(string p_strAppID, string p_strReportID, string p_strConfirmerID, string p_strConfirmDate);

        // 更新SampleID 到 AppSampleGroup     
        [OperationContract]
        long m_lngUpdateAppSampleGroupSampleID(string p_strAppID, string p_strSampleID);

        // 根据Application_ID_CHR更新t_opr_lis_application_detail的状态字为
        [OperationContract]
        long m_lngSetStatusToConfirmByApplicationIDAndGroupID(string strGroupID, string strApplID, string strStatus);

        // 新增记录组合与样本的关系的记录
        [OperationContract]
        long m_lngAddGroupSampleRelation(string strApplForm, string strSampleID, string strGroupID);

        // 根据application_id和group_id更新t_opr_lis_application_detail表的检验师意见
        [OperationContract]
        long m_lngSetSummaryByApplicationIDAndGroupID(string strSummary, string strApplID, string strGroupID);

        // 更新申请单信息，需要insert两个表，一个为t_opr_lis_application,一个为t_opr_lis_application_detail
        [OperationContract]
        long m_lngUpdateAppl(clsLisApplMainVO p_objLisApplMainVO);

        // 在增加新的申请单时，判断t_opr_lis_application中的application_form_no_chr在所有PStatus_int>0的记录中是否唯一
        [OperationContract]
        long m_lngQueryApplFormNo(string p_strApplFormNo);

        // 根据申请号，设置其处理状态（PStatus_int);
        [OperationContract]
        long m_lngSetApplicationStatus(string strAppId, int intStatus);

        // 作废申请单
        [OperationContract]
        long m_lngUpdateVoidApply(string p_strAppID, string p_strOperatorID);

        // 通过申请单号判断是否审核
        /// <summary>
        /// 通过申请单号判断是否审核
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strStatus"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lnqQueryConfirmReport(string p_strApplicationID, out DataTable p_dtResult, out DataTable p_dtUnitResult);

        // 获取系统参数 
        [OperationContract]
        long m_lngGetSysParm(string p_strParmCode, out string p_strParmValue);

        // 修改t_opr_lis_app_report 表的打印次数 
        [OperationContract]
        long m_lngUpdatePrinctTime(string p_strApplicaionID);

        // 取消审核
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="p_strAppID">申请单ID</param>
        /// <param name="p_strOperatorID">操作员工ID</param>
        /// <returns>大于0成功，否则失败</returns>
        [OperationContract]
        long m_lngCancelConfimReport(string p_strAppID, string p_strOperatorID);

        // 获取检验类别
        /// <summary>
        /// 获取检验类别
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngQueryCheckCategory(out DataTable p_dtResult);

        // 查询申请信息
        /// <summary>
        /// 联合查询申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSchVO">承载联合查询条件的VO,其中值为NULL的成员会被必忽略</param>
        /// <param name="p_objAppVOArr">返回的承载申请单信息的数组</param>
        /// <returns>0:失败;1:成功</returns>
        [OperationContract]
        long m_lngGetAppInfoByModifDate(clsLISApplicationSchVO p_objSchVO, string p_strCheckCategory, out clsLisApplMainVO[] p_objAppVOArr);

        // 修改急诊状态 
        [OperationContract]
        int UpdateEmergencyStatus(clsLisApplMainVO appMainVo);

        /// <summary>
        /// 根据申请单元ID获取标本
        /// </summary>
        /// <param name="appUnitId"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetSampleInfo(string appUnitId);

        #endregion

        #region clsApplyPropertySv

        // 保存基本资料
        [OperationContract]
        long m_lngSavePropertyAndValue(clsApplyUnitPropertyDoc p_objDoc, out clsApplyUnitPropertyDoc p_objOutDoc);

        // 保存关联
        [OperationContract]
        long m_lngSaveRelate(string p_strApplyUnitID, clsUnitPropertyRelate_VO[] p_objVOArr);

        #endregion

        #region clsApplyUnitSvc

        // 更新申请单元
        [OperationContract]
        long m_lngSetApplyUnit(clsApplUnit_VO p_objApplyUnit, List<clsApplUnitDetail_VO> p_arlAddDetail, List<clsApplUnitDetail_VO> p_arlRemoveDetail);

        // 根据申请单元ID和检验项目ID删除申请单元明细信息
        [OperationContract]
        long m_lngDelApplyUnitDetailByApplyIDAndCheckItemID(clsApplUnitDetail_VO p_objRecord);

        // 更新申请单元的基本资料
        [OperationContract]
        long m_lngSetApplUnit(ref clsApplUnit_VO objApplUnit);

        // 删除申请单元及其明细
        [OperationContract]
        long m_lngDelApplUnitAndDetail(string strApplUnitID);

        // 删除申请单元明细
        [OperationContract]
        long m_lngDelApplUnitDetail(string strApplUnitID);

        // 删除申请单元
        [OperationContract]
        long m_lngDelApplUnit(string strApplUnitID);

        // 保存申请单元及其明细
        [OperationContract]
        long m_lngAddApplUnitAndDetail(ref clsApplUnit_VO objApplUnitVO, ref clsApplUnitDetail_VO[] objApplUnitDetailVOList);

        // 保存一条记录到t_aid_lis_apply_unit_detail
        [OperationContract]
        long m_lngAddApplUnitDetail(ref clsApplUnitDetail_VO objApplUnitDetailVO);

        // 保存一条记录到条t_aid_lis_apply_unit表
        [OperationContract]
        long m_lngAddApplUnit(ref clsApplUnit_VO objApplUnitVO);

        #endregion

        #region clsBaseCheckGroupSvc

        // 根据样本的类型ID查询所有可能做的基本检验组 
        [OperationContract]
        long m_lngGetBaseCheckGroupBySampleTypeID(string p_strSampleTypeID, out System.Data.DataTable p_dtbGroup);

        // 根据基本检验组ID查找能做这组检验的所有的仪器类型ID 
        [OperationContract]
        long m_lngGetDeviceModelByBaseCheckGroupID(string p_strBaseCheckGroupID, out System.Data.DataTable p_dtbDeviceModel);

        #endregion

        #region clsBatchSaveReportSvc

        /// <summary>
        /// 批量保存检验编号
        /// </summary>
        /// <param name="p_objMainArr">源数据</param>
        /// <param name="p_strOperator">操作者</param>
        /// <returns>大于0成功，否则失败</returns>
        [OperationContract]
        long m_lngUpdateCheckNUM(clsLisApplMainVO[] p_objMainArr, string p_strOperator);

        #endregion

        #region clsBatchSaveReportQuerySvc

        /// <summary>
        /// 查询申请单信息通过条码号
        /// </summary>
        /// <param name="p_strBarcode">条码号</param>
        /// <param name="p_objMainVO">返回的病人信息</param>
        /// <returns>大于0成功，否则失败</returns>
        [OperationContract]
        long m_lngQuerySampleInfo(string p_strBarcode, out clsLisApplMainVO p_objMainVO);

        #endregion

        #region clsCheckGroupSvc

        // 删除报告组及其明细 
        [OperationContract]
        long m_lngDelReportGroupAndDetail(string strReportGroupID);

        // 删除报告组明细 (go);
        [OperationContract]
        long m_lngDelReportGroupDetail(string strReportGroupID);

        // 删除报告组 (go);
        [OperationContract]
        long m_lngDelReportGroup(string strReportGroupID);

        // 保存报告组及其明细 (go);
        [OperationContract]
        long m_lngAddReportGroupAndDetail(ref clsReportGroup_VO objReportGroupVO, ref clsReportGroupDetail_VO[] objReportGroupDetailVO);

        // 保存记录到t_aid_lis_report_group_detail (go);
        [OperationContract]
        long m_lngAddReportGroupDetail(ref clsReportGroupDetail_VO objReportGroupDetailVO);

        // 保存记录到t_aid_lis_report_group (go);
        [OperationContract]
        long m_lngAddReportGroup(ref clsReportGroup_VO objReportGroupVO);

        #endregion

        #region clsCheckGroupSvc

        // 删除标本组及其明细 (go);
        [OperationContract]
        long m_lngDelSampleGroupAndDetail(string strSampleGroupID);

        // 删除标本组明细 (go);
        [OperationContract]
        long m_lngDelSampleGroupDetail(string strSampleGroupID);

        // 删除标本组(go);
        [OperationContract]
        long m_lngDelSampleGroup(string strSampleGroupID);

        // 保存标本组及其明细 (go);
        [OperationContract(Name = "m_lngAddSampleGroupAndDetail2")]
        long m_lngAddSampleGroupAndDetail(ref clsSampleGroup_VO objSampleGroup, ref clsSampleGroupDetail_VO[] objSampleGroupDetailVOList);

        // 保存记录到t_aid_lis_sample_group_detail表 (go);
        [OperationContract]
        long m_lngAddSampleGroupDetail(clsSampleGroupDetail_VO objSampleGroupDetailVO);

        // 保存记录到t_aid_lis_sample_group表 (go);
        [OperationContract]
        long m_lngAddSampleGroup(ref clsSampleGroup_VO objSampleGroupVO);

        // 根据CheckGroupID更新t_aid_lis_check_group的信息 
        [OperationContract]
        long m_lngUpdCheckGroupByGroupID(ref clsCheckGroup_VO objCheckGroupVO);

        // 修改检验组信息 
        [OperationContract]
        long m_lngSetCheckGroup(ref clsCheckGroup_VO objCheckGroupVO, ref clsCheckGroupDetail_VO[] objCheckGroupDetail, ref clsCheckGroupRelation_VO[] objCheckGroupRelation, ref clsGroupSample_VO[] objGroupSample, string strCheckGroupID);

        // 删除检验组相关的表信息 
        [OperationContract]
        long m_lngDelCheckGroupRelatedInfo(string strCheckGroupID);

        // 根据GroupID删除表t_aid_lis_check_group_detail中的记录
        [OperationContract]
        long m_lngDelCheckGroupDetail(string strCheckGroupID);

        // 根据GroupID删除表t_aid_lis_check_group_relation中的记录
        [OperationContract]
        long m_lngDelCheckGroupRelation(string strCheckGroupID);

        // 根据GroupID删除表t_aid_lis_group_sample 
        [OperationContract]
        long m_lngDelGroupSample(string strGroupID);

        // 根据GroupID删除表t_aid_lis_check_group 
        [OperationContract]
        long m_lngDelCheckGroup(string strGroupID);

        // 删除检验组相关的所有信息 
        [OperationContract]
        long m_lngDelAllCheckGroupInfo(string strCheckGroupID);

        // 增加一条新的检验组记录到表t_aid_lis_check_group 
        [OperationContract]
        long m_lngAddCheckGroup(ref clsCheckGroup_VO objCheckGroupVO);

        // 增加一条新的检验组关系记录到表t_aid_lis_check_group_relation 
        [OperationContract]
        long m_lngAddCheckGroupRelation(ref clsCheckGroupRelation_VO objCheckGroupRelationVO);

        // 增加一条新的检验组明细到表t_aid_lis_check_group_detail
        [OperationContract]
        long m_lngAddCheckGroupDetail(ref clsCheckGroupDetail_VO objCheckGroupDetailVO);

        // 增加一条记录到表T_AID_LIS_GROUP_SAMPLE 
        [OperationContract]
        long m_lngAddGroupSample(ref clsGroupSample_VO objGroupSample);

        // 新增检验组  
        [OperationContract]
        long m_lngAddAllCheckGroupInfo(ref clsCheckGroup_VO objCheckGroup, ref clsCheckGroupDetail_VO[] objCheckGroupDetail, ref clsCheckGroupRelation_VO[] objCheckGroupRelation, ref clsGroupSample_VO[] objGroupSample);

        #endregion

        #region clsCheckItemSvc

        // 新增记录到表T_AID_LIS_VALUETEMPLATE_ITEM
        [OperationContract]
        long m_lngAddNewValueTemplateItem(clsLisValueTemplateItem_VO p_objRecord);

        // 删除表T_AID_LIS_VALUETEMPLATE_ITEM的记录
        [OperationContract]
        long m_lngDelValueTemplateItem(clsLisValueTemplateItem_VO p_objRecord);

        // 复用模板 
        [OperationContract]
        long m_lngReuseTemplate(clsLisValueTemplateItem_VO p_objOldRecord, clsLisValueTemplateItem_VO p_objNewRecord);

        // 新增记录到表T_AID_LIS_VALUETEMPLATE
        [OperationContract]
        long m_lngAddNewValueTemplate(clsLisValueTemplate_VO p_objRecord);

        // 删除表T_AID_LIS_VALUETEMPLATE的记录
        [OperationContract]
        long m_lngDelValueTemplate(string p_strTemplateID);

        // 新增记录到表T_AID_LIS_VALUETEMPLATE_DETAIL
        [OperationContract]
        long m_lngAddNewValueTemplateDetail(clsLisValueTemplateDetail_VO p_objRecord);

        // 删除表T_AID_LIS_VALUETEMPLATE_DETAIL的记录
        [OperationContract]
        long m_lngDelVauleTemplateDetail(string p_strTemplateID, int p_strIdx);

        // 更新表T_AID_LIS_VALUETEMPLATE_DETAIL的记录
        [OperationContract]
        long m_lngModifyVauleTemplateDetail(clsLisValueTemplateDetail_VO p_objRecord);

        // 根据模板ID更新T_AID_LIS_VALUETEMPLATE_DETAIL的默认标记
        [OperationContract]
        long m_lngSetDefaultFlagByTemplateID(string p_strTemplateID, int p_intFlag);

        // 根据模板ID和idx更新T_AID_LIS_VALUETEMPLATE_DETAIL的默认标记
        [OperationContract]
        long m_lngSetDefaultFlagByTemplateIDAndIdx(string p_strTemplateID, string p_strIdx, int p_intFlag);

        // 新增检验项目的模板及其明细
        [OperationContract]
        long m_lngAddNewCheckItemVauleTemplate(clsLisValueTemplate_VO p_objValueTemplate, clsLisValueTemplateItem_VO p_objValueTemplateItem, clsLisValueTemplateDetail_VO[] p_objValueTemplateDetailArr);

        // 批量新增、删除和修改模板明细信息
        [OperationContract]
        long m_lngValueTemplateDetailArr(List<clsLisValueTemplateDetail_VO> p_objAddNewArr, List<clsLisValueTemplateDetail_VO> p_objDelArr, List<clsLisValueTemplateDetail_VO> p_objUpdArr, string p_strTemplateID, string p_strIdx);

        // 修改检验项目及参考值范围
        [OperationContract]
        long m_lngSetCheckItemAndRef(clsCheckItem_VO p_objCheckItem, clsCheckItemRef_VO[] p_objCheckItemRefArr);

        // 删除检验项目及参考值范围
        [OperationContract]
        long m_lngDelCheckItemAndRef(string p_strCheckItemID);

        // 在t_bse_lis_check_item新增检验项目
        [OperationContract]
        long m_lngAddCheckItem(ref clsCheckItem_VO objCheckItemVO);

        // 批量新增检验项目参考值范围
        [OperationContract]
        long m_lngAddCheckItemRefList(ref clsCheckItemRef_VO[] objCheckItemRefVOList);

        // 新增检验项目的参考值范围
        [OperationContract]
        long m_lngAddNewItemRef(ref clsCheckItemRef_VO objCheckItemRefVO, DataTable dtDepts);

        // 更新检验类别信息
        [OperationContract]
        long m_lngSetCheckCategoryInfo(string strCheckCategory, string strCheckCategoryID);

        // [U]新增检验项目类别
        [OperationContract]
        long m_lngAddCheckCategory(ref clsCheckCategory_VO p_objCheckCategoryVO);

        // 删除选中的检验类别
        [OperationContract]
        long m_lngDelCheckCategory(string strCategory);

        // 根据check_item_id更新表t_bse_lis_check_item中属于该检验项目的明细资料
        [OperationContract]
        long m_lngSetCheckItemDetailByCheckItemID(ref clsCheckItem_VO objCheckItemVO);

        // 根据check_item_id更新t_bse_lis_itemref中属于该检验项目的参考值明细资料
        [OperationContract]
        long m_lngSetCheckItemRefByCheckItemID(ref clsCheckItemRef_VO objCheckItemRef);

        // 在已经存在的CheckItem的基础上新增检验参考值范围
        [OperationContract]
        long m_lngAddItemRefByCheckItemID(ref clsCheckItemRef_VO objCheckItemRefVO);

        // 删除表t_bse_lis_check_item检验项目
        [OperationContract]
        long m_lngDelCheckItem(string strCheckItemID);

        // 删除表t_bse_lis_itemref所有与该检验项目相关的参考值
        [OperationContract]
        long m_lngDelCheckItemRef(string strCheckItemID);

        // 删除表t_bse_lis_itemref中某一个序号的检验项目参考值
        [OperationContract]
        long m_lngDelCheckItemRefBySEQ(string strCheckItemID, string strSEQ);

        // 新增样品类别到表T_AID_LIS_SAMPLETYPE
        [OperationContract]
        long m_lngAddSampleType(ref clsSampleType_VO objSampleTypeVO);

        // 删除表T_AID_LIS_SAMPLETYPE中的记录
        [OperationContract]
        long m_lngDelSampleTypeBySampleTypeID(string strSampleTypeID);

        // 更新表T_AID_LIS_SAMPLETYPE中的记录
        [OperationContract]
        long m_lngSetSampleTypeDetailBySampleTypeID(string strSampleType, string strPyCode, string strWbCode, string strSampleTypeID, int intHasFlag);

        // 查询出表T_AID_LIS_SAMPLE_CHARACTER所有的样本性状
        [OperationContract]
        long m_lngGetAllSampleCharacter(string strSampleTypeID, out System.Data.DataTable dtbAllSampleCharacter);

        // 更新表T_AID_LIS_SAMPLE_CHARACTER中某一序号的样本状态
        [OperationContract]
        long m_lngSetSampleCharacterBySampleTypeIDAndSEQ(string strSampleTypeID, string strSEQ, string strSampleCharacter, string strPyCode, string strWbCode);

        // 新增样本性状到表T_AID_LIS_SAMPLE_CHARACTER
        [OperationContract]
        long m_lngAddSampleCharacterBySampleTypeID(ref clsSampleCharacter_VO objSampleCharacterVO);

        // 删除表T_AID_LIS_SAMPLE_CHARACTER的样本性状
        [OperationContract]
        long m_lngDelSampleCharacterBySampleTypeIDAndSEQ(string strSampleTypeID, string strSEQ);

        #endregion

        #region clsCheckResultSvc

        // 更新T_OPR_LIS_RESULT_IMPORT_REQ的状态
        [OperationContract]
        long m_lngSetResultImportReqStatus(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDat, string p_strStatus);

        // 调整结束指针的位置
        [OperationContract]
        long m_lngSetResultImportReqEndPoint(clsLisResultImportReq_VO p_objRecord);

        // 更新表T_OPR_LIS_RESULT_IMPORT_REQ的信息
        [OperationContract]
        long m_lngSetResultImportReq(clsLisResultImportReq_VO p_objRecord);

        // 向t_opr_lis_check_result表插入多条记录    
        [OperationContract(Name = "m_lngAddCheckResultList1")]
        long m_lngAddCheckResultList(clsCheckResult_VO[] p_objCheckResultList, string[] p_strSampleIDArr, string p_strOriginDate);

        // 设置仪器样本重做标志 
        [OperationContract]
        long m_lngSetDeviceSamplesRecheck(string p_strDeviceID, int p_intImportReq);

        // 新增融合后的仪器检验结果及日志信息
        [OperationContract]
        long m_lngAddNewDeviceCheckResultArrANDLog(clsDeviceReslutVO[] p_objDeviceResultArr, clsResultLogVO p_objResultLog);

        // 向表t_opr_lis_result加一条记录
        [OperationContract]
        long m_lngAddNewDeviceResult(clsDeviceReslutVO objDeviceResultVO);

        // 更新t_opr_lis_result_log的User_flag字段
        [OperationContract]
        long m_lngSetUseFlagByCondition(string p_strDevcieID, int p_strImpReqInt);

        // 向表t_opr_lis_result_log增加一条记录
        [OperationContract]
        long m_lngAddNewResultLog(clsResultLogVO objResultLogVO);

        // 向表t_opr_lis_result和t_opr_lis_result_log 1
        [OperationContract]
        long m_lngAddNullResultAndReslutLog(string strDeviceID, string strDeviceSampleID, string strCheckDat);

        // 向表t_opr_lis_result加一条记录
        [OperationContract]
        long m_lngAddDeviceResult(clsDeviceReslutVO objDeviceResultVO);

        // 向表t_opr_lis_result_log增加一条记录
        [OperationContract]
        long m_lngAddResultLog(clsResultLogVO objResultLogVO);

        // 根据样品ID号和check_item_id更新表t_opr_lis_check_result中的记录信息
        [OperationContract]
        long m_lngSetCheckResultBySampleIDAndCheck_item_id(clsCheckResult_VO p_objCheckResultVO);

        // 更新表t_opr_lis_check_result多条记录
        [OperationContract]
        long m_lngSetCheckResultList(clsCheckResult_VO[] p_objCheckResultList);

        // 向表t_opr_lis_device_relation增加一条新记录
        [OperationContract]
        long m_lngAddnewDeviceRelation(clsDeviceRelation_VO p_objDeviceRelation_VO);

        // 根据SampleID设置表t_opr_lis_req_check字段stepflag_chr的状态
        [OperationContract]
        long m_lngSetReqCheckStepFlag(string p_strSampleID, int p_intStepFlag);

        // 更新仪器结果记录的状态
        [OperationContract]
        long m_lngRefreshDeviceResultStatus(string p_strDeviceID, DateTime p_dtCheckDate, string p_strDeviceSampleID, int p_intStatus);

        #endregion

        #region clsDeviceSvc

        // 查询所有当前可用检验仪器列表    
        [OperationContract]
        long m_lngGetDeviceList(out DataTable p_dtbDeviceList);

        // 查询所有的仪器类型名称
        [OperationContract]
        long m_lngGetDeviceModelNameByDeviceID(out DataTable dtbAllDeciveName);

        // 查询某台仪器所有能做的质控项目
        [OperationContract]
        long m_lngGetQCCheckItemByDeviceID(string strDeviceID, out DataTable dtbQCCheckItem);

        // 根据检验项目号，查询可以做该检验项目的仪器列表
        [OperationContract]
        long m_lngGetDeviceListByCheckGroup(string p_strCheckGroupId, out DataTable p_dtbDeviceList);

        // 查询某台仪器所有可能的检验项目
        [OperationContract]
        long m_lngGetDeviceCheckGroupList(string strDeviceId, out DataTable p_dtbDeviceCheckGroupList);

        // 查询所有的仪器类型
        [OperationContract]
        long m_lngQueryAllDevType(out System.Data.DataTable p_dtbDevType);

        // 查询某一类的所有具体仪器
        [OperationContract]
        long m_lngQueryAllDev(string p_strDevType, out System.Data.DataTable p_dtbDev);

        // 根据某台仪器ID查询该仪器所能做的所有检验单项信息
        [OperationContract]
        long m_lngGetCheckItemsByDevID(string p_strDevID, out System.Data.DataTable p_dtbCheckItem);

        #endregion

        #region clsDictSvc

        // 根据字典种类得到内容列表(除去第一条的类型说明);       
        [OperationContract(Name = "m_lngGetDictListFor1")]
        long m_lngGetDictListFor(string p_strDictKind, out DataTable p_dtbDictList);

        // 根据字典种类得到内容列表（除去第一条类型说明） 
        [OperationContract(Name = "m_lngGetDictListFor2")]
        long m_lngGetDictListFor(string p_strDictKind, out clsAIDDICT_VO[] p_objResultArr);

        #endregion

        #region clsInputGroupSvc

        // 新增录入组合
        [OperationContract]
        long m_lngAddNewInputGroup(clsInputGroupBaseInfo_VO p_objBaseInfo, clsInputGroupDetail_VO[] p_objResults, out string strID);

        // 更新录入组合
        [OperationContract]
        long m_lngUpdateInputGroup(clsInputGroupBaseInfo_VO p_objBaseInfo, clsInputGroupDetail_VO[] p_objResults);

        // 删除录入组合
        [OperationContract]
        long m_lngDeleteInputGroup(string strGroupID);

        #endregion

    }
}
