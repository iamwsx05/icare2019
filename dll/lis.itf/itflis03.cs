using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using weCare.Core.Entity;

namespace Lis.Itf
{
    [ServiceContract]
    public interface ItfLis03 : weCare.Core.Itf.IWcf
    {
        #region clsQueryCheckResultSvc

        // 根据条件查询T_OPR_LIS_RESULT_IMPORT_REQ表的信息
        [OperationContract]
        long m_lngGetResultImportReqByCondition(string p_strDeviceID, string p_strCheckDatFrom, string p_strCheckDatTo, out clsLisResultImportReq_VO[] p_objResultArr);

        /// <summary>
        /// 查询得到  CheckResultVO
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objResultVO"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetCheckResultVO(string p_strSampleID, string p_strCheckItemID, out clsCheckResult_VO p_objResultVO);

        /// <summary>
        /// 查询得到  CheckResultTable 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strReportID"></param>
        /// <param name="p_strGroupID"></param>
        /// <param name="p_blnRealResult"></param>
        /// <param name="p_dtbResultList"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetCheckResultTable(string p_strAppID, string p_strOringinDate, bool p_blnRealResult, out DataTable p_dtbResultList);

        // 批量打印报告单
        [OperationContract]
        long m_lngGetBatchReportDataByCondition(string p_strFromSampleID,
            string p_strToSampleID, string p_strFromConfirmDat, string p_strToConfirmDat, string p_strReportGroupID, out clsLisBatchReport_VO[] p_objResultArr);

        // 根据条件查询批量打印的报告单列表
        [OperationContract]
        long m_lngGetLisBatchReportListByCondition(string p_strFromSampleID,
            string p_strToSampleID, string p_strFromConfirmDat, string p_strToConfirmDat, string p_strReportGroupID, string p_strPatientType, out clsLisBatchReportList_VO[] p_objResultArr);

        // 根据申请单号和报告组号查询批量打印信息列表
        [OperationContract]
        long m_lngGetLisBatchReportDetailByCondition(clsLisBatchReportList_VO[] p_objReportList, out clsLisBatchReportDetail_VO[] p_objResultArr);

        /// <summary>
        /// 根据report_group_id和application_id_chr查询报告单相关信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strReportGroupID">报告组ID</param>
        /// <param name="p_strApplID">申请单ID</param>
        /// <param name="p_blnConfirmed">是否审核</param>
        /// <param name="p_dtbReportInfo">返回报告单相关信息</param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetReportInfoByReportGroupIDAndApplicationID(
            string p_strReportGroupID, string p_strApplID, bool p_blnConfirmed, out DataTable p_dtbReportInfo);

        /// <summary>
        /// 根据report_group_id和application_id_chr查询报告的结果记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strApplicationID">申请单ID</param>
        /// <param name="strReportGroupID">报告组ID</param>
        /// <param name="blnConfirmed">是否审核</param>
        /// <param name="dtbCheckResult">返回结果信息</param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetCheckResultByReportGroupIDAndApplicationID(
            string p_strApplicationID, string p_strReportGroupID, bool p_blnConfirmed, out DataTable p_dtbCheckResult);

        /// <summary>
        /// 根据检验日期和仪器编号查询DeviceResultLog
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckDatFrom"></param>
        /// <param name="p_strCheckDatTo"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetDeviceResultLogByCondition(string p_strCheckDatFrom,
            string p_strCheckDatTo, string p_strDeviceID, out clsResultLogVO[] p_objResultArr);

        /// <summary>
        /// 根据申请号得到对应样本的仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_objDRVOArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetDeviceRelationByAppID(string p_strAppID, out clsT_LIS_DeviceRelationVO[] p_objDRVOArr);

        // 根据Imp_Req_int和仪器ID查询标本列表 
        [OperationContract(Name = "m_lngGetDeviceSampleListByCondition1")]
        long m_lngGetDeviceSampleListByCondition(string p_strImpReq, string p_strDeviceID, out clsResultLogVO[] p_objResultArr);

        // 根据仪器样本号，仪器ID和检验时间查询标本列表 
        [OperationContract(Name = "m_lngGetDeviceSampleListByCondition2")]
        long m_lngGetDeviceSampleListByCondition(string p_strDeviceSampleID, string p_strDeviceID, string p_strCheckDat, out clsResultLogVO[] p_objResultArr);

        /// <summary>
        /// 以指定编号方式,根据指定的仪器编号,检验日期(trunc);,和仪器样本编号查询绑定和提取数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <param name="p_strCheckDate"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns>
        /// 小于等于 0 : 查询失败; 
        /// 100: 无可绑定的仪器样本; 
        /// 300: 指定的仪器样本号无历史记录; 
        /// 400:指定的仪器样本无原始数据; 
        /// 其它: 成功返回
        /// </returns>
        [OperationContract]
        long m_lngQueryBindAndGetDeviceDataByAppointment(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, out clsDeviceReslutVO[] p_objResultArr);

        /// <summary>
        /// 以指定编号方式,根据指定的仪器编号,检验日期(trunc);,和仪器样本编号查询绑定 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <param name="p_strCheckDate"></param>
        /// <param name="p_objResultLogVO">以此到仪器结果表提取数据</param>
        /// <returns>
        /// 小于等于 0 : 查询失败;
        /// 100: 无可绑定的仪器样本;
        /// 300: 指定的仪器样本号无历史记录;
        /// 其它: 成功返回
        /// </returns>
        [OperationContract]
        long m_lngQueryBindByAppointment(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, out clsResultLogVO p_objResultLogVO);

        /// <summary>
        ///  根据指定的仪器编号,REQ ,查询 DeviceResultLog 和提取仪器数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_intImportReq"></param>
        /// <param name="p_objDeviceResultList"></param>
        /// <returns>
        /// 小于 0 : 查询失败;
        /// 300:指定的仪器样本无历史记录
        /// 400:指定的仪器样本无原始数据
        /// 其它: 成功返回 
        /// </returns>
        [OperationContract(Name = "m_lngGetDeviceData1")]
        long m_lngGetDeviceData(string p_strDeviceID, int p_intImportReq, out clsDeviceReslutVO[] p_objDeviceResultList);

        /// <summary>
        /// 根据指定的仪器编号,REQ 查询DeviceResultLog 刘彬 2004.06.10
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_intImportReq"></param>
        /// <param name="p_objResultLogVO">以此到仪器结果表提取数据</param>
        /// <returns>
        /// 小于 0 : 查询失败; 
        /// 300: 指定的仪器样本号无历史记录; 
        /// 其它: 成功返回 
        /// </returns>
        [OperationContract]
        long m_lngQueryResultLog(string p_strDeviceID, int p_intImportReq, out clsResultLogVO p_objResultLogVO);

        /// <summary>
        ///  根据指定的仪器编号,检验日期,和仪器样本编号,及开始索引和结束索引提取仪器数据 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <param name="p_strCheckDate"></param>
        /// <param name="p_intBeginIndex"></param>
        /// <param name="p_intEndIndex"></param>
        /// <param name="p_objDeviceResultList"></param>
        /// <returns>
        /// 小于等于 0 : 查询失败; 
        /// 400:指定的仪器样本无原始数据
        /// 其它: 成功返回 
        /// </returns>
        [OperationContract(Name = "m_lngGetDeviceData2")]
        long m_lngGetDeviceData(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, int p_intBeginIndex, int p_intEndIndex, out clsDeviceReslutVO[] p_objDeviceResultList);

        /// <summary>
        /// 以指定编号方式,根据指定的仪器编号,和仪器样本编号查询绑定和提取数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns>
        /// 小于 0 : 查询失败; 
        /// 100: 无可绑定的仪器样本;
        /// 300: 指定的仪器样本号存在且未绑定,但无历史记录; 
        /// 400:指定的仪器样本无原始数据; 
        /// 其它: 成功返回
        /// </returns>
        [OperationContract]
        long m_lngQueryBindAndGetDeviceDataByAutoBind(string p_strDeviceID, out clsDeviceReslutVO[] p_objResultArr);

        /// <summary>
        /// 以自动绑定方式,根据指定的仪器编号 查询绑定 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objResultLogVO">以此到仪器结果表提取数据</param>
        /// <returns>
        /// 小于 0 : 查询失败;
        /// 100: 无可绑定的仪器样本;
        /// 300: 可绑定的仪器样本号存在且未绑定,但无历史记录 
        /// 其它: 成功返回
        /// </returns>
        [OperationContract]
        long m_lngQueryBindByAutoBind(string p_strDeviceID, out clsResultLogVO p_objResultLogVO);

        // 查找指定病人，指定检验项目ID的检验结果
        /// <summary>
        /// 查找指定病人，指定检验项目ID的检验结果
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientID">病人ID</param>
        /// <param name="p_strExcItemID"> 需要特殊处理的项目ID</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngQueryPatientExcItemResult(string p_strPatientID, string p_strExcItemID, out DataTable p_dtResult);

        // 根据申请单和检验大组查询检验结果
        [OperationContract]
        long m_lngGetCheckResultByApplFormNoAndCheckGroupID(string strApplFormNo, string strGroupID, out DataTable dtbCheckResult);

        // 根据样品号查询表t_opr_lis_check_result的结果信息
        [OperationContract]
        long m_lngGetManualCheckResultBySampleID(string strSampleID, out DataTable dtbManualCheckResult);

        // 根据申请日期查询手工录入的检验项目的标本资料
        [OperationContract]
        long m_lngGetSampleListByApplDat(DateTime p_dtBegin, DateTime p_dtEnd, string flag, out System.Data.DataTable p_dtbSampleList);

        // 根据SampleID和GroupID查询手工录入的具体检验项目
        [OperationContract]
        long m_lngGetCheckItemBySampleIDAndGroupID(string strSampleID, string strGroupID, out System.Data.DataTable dtbManualCheckItemList);

        /// <summary>
        /// 根据仪器号（不是型号，是一台具体仪器的代号）和检验日期范围，查询出符合条件(该仪器在该段时间内所检验的）标本资料 返回结果必须包含仪器标本号，样品Barcode 和检验日期。
        /// </summary>
        /// <param name="strDeviceId">设备ID</param>
        /// <param name="dtBegin">查询开始时间</param>
        /// <param name="dtEnd">查询结束时间</param>
        /// <param name="dtbDeviceSampleList"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetDeviceSampleList1")]
        long m_lngGetDeviceSampleList(string p_strDeviceID, string strBeginDate, string strEndDate, out System.Data.DataTable p_dtbDeviceSampleList);

        /// <summary>
        /// 根据仪器号（不是型号，是一台具体仪器的代号）和检验日期范围，查询出符合条件(该仪器在该段时间内所检验的）标本资料 返回结果必须包含仪器标本号，检验的项目（无子组的项目） 和检验日期。
        /// </summary>
        /// <param name="strDeviceId">设备ID</param>
        /// <param name="dtBegin">查询开始时间</param>
        /// <param name="dtEnd">查询结束时间</param>
        /// <param name="intDeviceSampleObj">0-全部标本;1-未审核标本</param>
        /// <param name="dtbDeviceSampleList"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetDeviceSampleList2")]
        long m_lngGetDeviceSampleList(string p_strDeviceID, DateTime p_dtBegin, DateTime p_dtEnd, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSampleList);

        /// <summary>
        /// 根据仪器号（不是型号，是一台具体仪器的代号）和检验日期范围，再加上一个检验项目（无子组）号，查询出符合条件（该仪器在该段时间内所检验的该检验项目的）标本资料。 返回结果必须包含仪器标本号，检验的项目（无子组的项目）和检验日期。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_dtBegin"></param>
        /// <param name="p_dtEnd"></param>
        /// <param name="strCheckGroupID">组合ID（不包含子组）</param>
        /// <param name="p_intDeviceSampleObj">0-全部标本;1-未审核标本</param>
        /// <param name="p_dtbDeviceSample"></param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngGetDeviceSampleList3")]
        long m_lngGetDeviceSampleList(string p_strDeviceID, DateTime p_dtBegin, DateTime p_dtEnd, string p_strCheckGroupID, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSample);

        // 根据仪器号（不是型号，是一台具体仪器的代号）和检验日期范围，再加上仪器标本号范围，查询出符合条件（该仪器在该段时间内所检验的标本范围内的）标本资料。
        [OperationContract(Name = "m_lngGetDeviceSampleList4")]
        long m_lngGetDeviceSampleList(string p_strDeviceId, DateTime p_dtBegin, DateTime p_dtEnd, string p_strDeviceSampleIDBegin, string p_strDeviceSampleIDEnd, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSampleList);

        /// <summary>
        /// 根据仪器标本号列表（即一个集合，放在DataTable中）, 查询出该标本的所有检验项目的结果项的代号、名称、值等详细内容。 注意，这里是从t_opr_lis_result表中查询，所得到的结果为仪器输出的结果
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbDeviceSampleList">此结构和上面方法的DataTable结构相同</param>
        /// <param name="p_dtbDeviceResultList"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetDeviceResultList(System.Data.DataTable p_dtbDeviceSampleList, out System.Data.DataTable p_dtbDeviceResultList);

        /// <summary>
        /// 根据仪器标本号列表（即一个集合，放在DataTable中），查询出该标本的所要进行的检验项目的结果项的代号、名称等详细内容。
        /// 注意，这里要根据仪器标本号查询出对应的标本号（系统内部给定的）；根据标本号，从t_opr_lis_req_check和t_opr_lis_req_check_detail中查询结果。这里得到的结果与方法11所得到的结果相似，但没有结果值。另外，可能多了一些计算类的结果记录。
        /// Long getResultList(DataTable dtbDeviceSample, out DataTable dtbResultList);
        /// 说明：这里，dtbDeviceSample的结构与方法8~11一样。其中，包含了检验日期。另外，dtbResultList中要有结果值字段，虽然该字段暂时为空。请参考t_opr_lis_check_result表结构确定此返回结果的结构。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbDeviceSample"></param>
        /// <param name="p_dtbResultList"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetResultList(System.Data.DataTable p_dtbDeviceSample, out System.Data.DataTable p_dtbResultList);

        // 根据仪器标本号列表（即一个集合，放在DataTable中），查询出该标本的检验项目的已经有的结果详细内容。
        [OperationContract]
        long m_lngGetCurrentResultList(System.Data.DataTable p_dtbDeviceSampleList, out System.Data.DataTable p_dtbCurrentResults);

        // 根据样本号、检验项目（不含子组），查询对应的检验结果（这里从t_opr_lis_check_result查询）
        [OperationContract]
        long m_lngGetCheckResult(string p_strSampleID, string p_strCheckGroupID, out System.Data.DataTable p_dtbCheckResult);

        // 查询两个标本的检验结果
        [OperationContract]
        long m_lngGetUnionCheckResult(string p_strSmapleIDFirst, string p_strGroupIDFirst, string p_strSampleIDSecond, string p_strGroupIDSecond, out System.Data.DataTable p_dtbResult);

        // 根据检验日期或者审核日期查询检验单号(不得重复);
        [OperationContract]
        long m_lngGetApplFormNOByDateRange(string p_strDateFieldName, System.DateTime p_dtBegin, System.DateTime p_dtEnd, out System.Data.DataTable p_dtbAppl);

        // 根据SampleID,GroupID（基本检验组）查询结果项,这里要查询两个表，一个是t_opr_lis_check_result,一个是t_aid_lis_check_group_detail，打印时按打印类别，打印顺序打印。
        [OperationContract]
        long m_lngGetPrintResult(string p_strGroupID, string p_strSmapleID, out System.Data.DataTable p_dtbPrintResult);

        //  根据仪器标本号, 仪器号，检验日期查询出该标本的所有结果项的代号、名称、值等详细内容。
        [OperationContract]
        long m_lngGetDeviceSampleResultList(string strDeviceID, string strDeviceSampleID, string strCheckDate, out System.Data.DataTable dtbSampleResultList);

        /// <summary>
        /// 获取签名图片
        /// </summary>
        /// <param name="empName"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] GetEmpSign(string empName);

        #endregion

        #region clsQueryInputGroupSvc

        // 获取筛选后的项目列表
        [OperationContract]
        long m_lngGetFiltedItems(string[] p_strApplyUnitIDArr, string[] p_strInputGroupIDArr, out string[] p_strItemResultArr);

        // 获取指定申请单元及其名称列表
        [OperationContract]
        long m_lngGetApplyUnitInfo(string[] p_strApplyUnitIDArr, out DataTable p_dtbResult);

        // 获取指定申请单元下可用的录入组合
        [OperationContract]
        long m_lngGetInputGroupsByUnit(string[] p_strApplyUnitIDArr, out DataTable p_dtbResult);

        // 获取检验分类-申请单元-录入组合的的联合信息
        [OperationContract]
        long m_lngGetUnitedInputGroupInfo(out clsInputGroupUnited_VO[] p_objResults);

        // 获取申请单元项目明细
        [OperationContract]
        long m_lngGetApplyUnitItems(string p_strApplyUnitID, out clsCheckItemSimple_VO[] p_objResults);

        // 获取录入组合及明细
        [OperationContract]
        long m_lngGetInputGroupInfo(string p_strInputGroupID, out clsInputGroupBaseInfo_VO p_objBaseInfo, out clsInputGroupDetail_VO[] p_objResults);

        #endregion

        #region clsApplicationMainSvc

        [OperationContract]
        long m_lngGetApplicationValid(string applicationId, out bool isValid);

        [OperationContract]
        long m_lngGetApplication(string orderId, out List<clsLisApplMainVO> lstAppMain);

        [OperationContract]
        long m_lngDeleteApplication(string applicationId);

        [OperationContract(Name = "m_lngAddNewAppInfo1")]
        long m_lngAddNewAppInfo(clsLisApplMainVO applMain, out clsLisApplMainVO applMainOut, bool isSend,
                                            clsT_OPR_LIS_APP_REPORT_VO[] arrReports, clsT_OPR_LIS_APP_SAMPLE_VO[] arrSamples,
                                            clsT_OPR_LIS_APP_CHECK_ITEM_VO[] arrCheckItems, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] arrApplyUnits,
                                            clsLisAppUnitItemVO[] arrUnitItems);
        /// <summary>
        /// 设置打印状态
        /// </summary>
        /// <param name="arrApplicationId"></param>
        /// <param name="isPrinted"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngSetApplPrintedStatus(string[] arrApplicationId, bool isPrinted);

        /// <summary>
        /// 根据医嘱ID获得检验申请单主要信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="applMain"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngGetApplVO(string orderId, out clsLisApplMainVO applMain);

        #endregion

        #region clsChargeInfoStatusSvc

        /// <summary>
        /// 查找申请单的收费状态
        /// </summary>
        /// <param name="applicationId">申请单Id</param>
        /// <param name="chargeStatusVO">收费状态VO</param>
        /// <returns></returns>
        [OperationContract(Name = "m_lngFind28")]
        long m_lngFind(string applicationId, out clsChargeStatusVO chargeStatusVO);

        #endregion

        #region clsApplicationBizSvc

        // 增加-组新的检验申请信息(包含报告组,申请单元,样本组,检验项目在内的全部信息); 
        [OperationContract]
        long m_lngAddNewApplication(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO,
                                           clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr,
                                           clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr,
                                           clsLisAppUnitItemVO[] p_objAppUnitItemArr);

        // 修改-组检验申请信息(包含报告组,申请单元,样本组,检验项目在内的全部信息);
        [OperationContract]
        long m_lngModifyAppInfo(clsLisApplMainVO p_objLisApplMainVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr);

        /// <summary>
        /// 为表 T_OPR_LIS_APP_APPLY_UNIT 新增记录时用 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        [OperationContract]
        long m_lngAddNewAppApplyUint(clsT_OPR_LIS_APP_APPLY_UNIT_VO[] arrRecodes);

        #endregion

        #region clsLisInterfaceSvc

        [OperationContract]
        long CreateApplicationArray(clsLisApplyAppliationInfo[] arrApplication);

        [OperationContract]
        long CreateSendApplicationArray(clsLisApplyAppliationInfo[] arrApplication, bool isSended);

        #endregion

        #region clsLisSearchSvc

        // 住院部分不关联收费信息
        [OperationContract(Name = "m_lngGetAppAndSampleInfo3")]
        long m_lngGetAppAndSampleInfo(int sampleStatus, string areaId,
                                             string beginDate, string endDate, string patientName,
                                             string patientCardId, string hosipitalNo, string bedNo, int p_intSampleBack, out clsLisApplMainVO[] p_objAppVOArr);

        /// <summary>
        /// 构建 clsLisApplMainVO 
        /// </summary>
        /// <param name="p_objRow"></param>
        /// <returns></returns>
        [OperationContract]
        clsLisApplMainVO m_objConstructAppMainVO(DataTable dtRow);

        #endregion

        #region clsCPReportPrintQuery_Svc

        [OperationContract]
        long m_lngGetLisApplicationIDByPatientID(string p_strPatienID, out DataTable dtRes);

        [OperationContract]
        long m_lngGetLisApplicationIDByPatientCardID(string p_strPatienCardID, out DataTable dtRes);

        [OperationContract]
        long m_lngGetLisLisApplicationIDBySBCardID(string p_strSBCardID, int p_intSBType, out DataTable dtRes);

        [OperationContract]
        long m_lngGetReportPrintInfo(string PatientID, out clsLis_ReportPrint_VO p_objReportPrint);

        #endregion

        #region clsReportPrintUpdate_Svc

        [OperationContract]
        long m_mthWriteReportPrintState(string m_strAPPLICATION_ID);

        #endregion

        #region clsLIS_DataAcquisitionServ

        [OperationContract(Name = "clsLIS_DataAcquisitionServ_lngAddLabResult1")]
        long clsLIS_DataAcquisitionServ_lngAddLabResult(List<clsLIS_Device_Test_ResultVO> p_arlResult, out List<clsLIS_Device_Test_ResultVO> p_arlResultOut);

        [OperationContract(Name = "clsLIS_DataAcquisitionServ_lngAddLabResult2")]
        long clsLIS_DataAcquisitionServ_lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr);

        [OperationContract(Name = "clsLIS_DataAcquisitionServ_lngAddLabResult3")]
        long clsLIS_DataAcquisitionServ_lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr);

        [OperationContract]
        long m_lngUpdateAppCheckNO(string p_strBarCode, string p_strDeviceID, string p_strDeviceNO, string p_strDeviceSampleID);

        [OperationContract]
        long m_lngUpdateAppCheckSampleNO(string p_strBarCode, string p_strDeviceID, string p_strDeviceNO, string p_strDeviceSampleID);

        [OperationContract]
        long m_lnginsertResultExe(int p_intFlag, DataTable p_dt);

        [OperationContract]
        long m_lnginsertResultMic(int p_intFlag, DataTable p_dt);

        [OperationContract]
        long m_lnginsertResultbill(int p_intFlag, DataTable p_dt);

        #endregion

        #region clsLIS_QueryDataAcquisitionServ

        [OperationContract(Name = "clsLIS_QueryDataAcquisitionServ_m_blnIsAppendResult")]
        bool clsLIS_QueryDataAcquisitionServ_m_blnIsAppendResult(ref Dictionary<string, string> has, clsLIS_Device_Test_ResultVO[] p_objResultArr, out string[] strConditionList);

        [OperationContract(Name = "lngGetInstrumentSerialSetting02")]
        long lngGetInstrumentSerialSetting(string strData_Acquisition_Computer_IP, out clsLIS_Equip_Base[] objConfig_List);

        [OperationContract]
        long m_lngGetDeviceNOConvertInfo(string p_strDeviceID, out clsDeviceItemNameItemNO[] p_objDeviceItemNameNOArr, out clsDeviceItemValueConvert_VO[] p_objItemConvertArr);

        [OperationContract]
        long m_lngQueryDeviceItemNO(string p_strDeviceID, out clsDeviceItemNameItemNO[] p_objDeviceItemNameNOArr);

        [OperationContract]
        long m_lngQueryDeviceItemValueConvert(string p_strDeviceID, out clsDeviceItemValueConvert_VO[] p_objItemConvertArr);

        [OperationContract(Name = "clsLIS_QueryDataAcquisitionServ_m_lngGetDeviceData")]
        long clsLIS_QueryDataAcquisitionServ_m_lngGetDeviceData(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, int p_intBeginIndex, int p_intEndIndex, out clsDeviceReslutVO[] p_objDeviceResultList);

        [OperationContract]
        long m_lngQueryAppInfoAndDeviceNO(string p_strDeviceID, string p_strBarCode, out clsLisApplMainVO p_objAppMainVO, out string[] p_strDeviceNOArr);

        [OperationContract]
        long m_lngQueryAppDeviceNOByBarCode(string p_strDeviceID, string p_strBarCode, out string[] p_strDeviceNOArr);

        [OperationContract]
        long m_lngGetAppInfoByBarCode(string p_strBarCode, out clsLisApplMainVO p_objAppMainVO);

        [OperationContract(Name = "m_lngQueryPatientInfo02")]
        long m_lngQueryPatientInfo(string p_strBarCode_vchr, out clsT_OPR_LIS_SAMPLE_VO p_objSampleVO);

        [OperationContract(Name = "clsLIS_QueryDataAcquisitionServ_m_lngQueryPatientInfo")]
        long clsLIS_QueryDataAcquisitionServ_m_lngQueryPatientInfo(string p_strBarCode_vchr, out clsT_OPR_LIS_SAMPLE_VO p_objSampleVO);

        [OperationContract]
        long m_lngGetCheckNOByBarcode(string p_strBarcode, out string p_strCheckNO);

        #endregion

        #region clsLis_ReportPrinterServ

        [OperationContract]
        long m_lngGetLisReportPrintInfo(string p_strPatientCard, string p_strMinTime, out clsLis_ReportPrint_VO p_objReportPrint, out string p_strPatientName);

        [OperationContract]
        long clsLis_ReportPrinterServ_m_mthWriteReportPrintState(string m_strAPPLICATION_ID);

        [OperationContract]
        long m_lngGetPatientCardByIDCard(string p_strIDCard, ref string p_strPatientCard);

        #endregion

        #region clsMicReportSvc

        [OperationContract]
        long lngGetAllAnti(out DataTable dtbResult);

        [OperationContract]
        long lngGetFuzzyQueryAnti(string micName, bool IsEnglish, out DataTable dtbResult);

        [OperationContract]
        long lngGetAllMic(out DataTable dtbResult);

        [OperationContract]
        long lngGetFuzzyQueryMic(string micName, bool IsEnglish, out DataTable dtbResult);

        [OperationContract]
        long lngGetBacteriaDistribution(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, out DataTable dtbResult);

        [OperationContract]
        long lngGetMicSensitive(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult);

        [OperationContract]
        long lngGetMicdistributionTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, out DataTable dtbResult);

        [OperationContract]
        long lngGetSensitiveTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult);

        [OperationContract]
        long lngGetMicCumulative(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult);

        [OperationContract]
        long lngGetSensitiveRate(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult);

        [OperationContract]
        long lngGetDeptName(out DataTable dtbResult);

        [OperationContract]
        long lngGetSamType(out DataTable dtbResult);

        #endregion

        #region clsST360Svc

        [OperationContract]
        long m_lngFindSTGroupResult(out List<clsDeviceReslutVO> lstResult);

        [OperationContract]
        long m_lngUpdateDeviceResult(clsDeviceReslutVO deviceResult);

        #endregion

        #region clsST360CheckResultSvc

        [OperationContract]
        long m_lngInsert(clsST360CheckResultVO m_objResult);

        [OperationContract(Name = "m_lngFindBoardName01")]
        long m_lngFindBoardName(out string[] arrBoardName, DateTime begin, DateTime end);

        [OperationContract(Name = "m_lngFindBoardName02")]
        long m_lngFindBoardName(out string[] arrBoardName);

        [OperationContract(Name = "m_lngFind29")]
        long m_lngFind(string boardNo, out clsST360CheckResultVO[] arrCheckResult);

        #endregion

        #region clsItemSetSvc

        [OperationContract(Name = "m_lngGetAllCheckItem02")]
        long m_lngGetAllCheckItem(string p_strDeviceModelID, out DataTable p_dtResult);

        [OperationContract]
        long m_lngGetAllCheckItemCustomInfo(out clsLisCheckItemCustom[] p_objCheckItemCustomVO, out DataTable p_dtResult);

        [OperationContract]
        long m_lngUpdateCheckItemCustom(clsLisCheckItemCustom p_objCheckItemCustomVO);

        [OperationContract]
        long m_lngDelteCheckItemCustom(string p_strCheckItemID);

        [OperationContract]
        long m_lngInsertCheckItemCustom(clsLisCheckItemCustom p_objCheckItemCustomVO);

        [OperationContract(Name = "m_lngQueryCheckItemCustomRes01")]
        long m_lngQueryCheckItemCustomRes(string p_strCheckItemID, out clsLisCheckItemCustomRes[] p_objCheckItemCustomRes);

        [OperationContract]
        long m_lngInsertCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO);

        [OperationContract]
        long m_lngUpdateCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO);

        [OperationContract]
        long m_lngDeleteCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO);

        [OperationContract(Name = "lngAddLabResult_clsItemSetSvc01")]
        long lngAddLabResult_clsItemSetSvc(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr);

        [OperationContract(Name = "lngAddLabResult_clsItemSetSvc02")]
        long lngAddLabResult_clsItemSetSvc(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr);

        [OperationContract]
        int m_mthGetNewResultIndex_clsItemSetSvc(int p_intRowNum, bool p_blnNext);

        [OperationContract]
        bool m_blnIsAppendResult_clsItemSetSvc(ref Dictionary<string, string> has, clsLIS_Device_Test_ResultVO[] p_objResultArr, out string[] strConditionList);

        [OperationContract]
        long m_lngQueryDevceCheckItem(string p_strDevicModelID, out DataTable p_dtResult);

        [OperationContract]
        long m_lngGetAllItemLayout(out DataTable p_dtResult, out DataTable p_dtLayoutInfo);

        [OperationContract]
        long m_lngAddItemLayout(clslisItemLayout[] p_objLisItemLayoutVO);

        [OperationContract]
        long m_lngDeleteItemLayout(string p_strLayoutName);

        [OperationContract]
        long m_lngQueryPlateName(string p_strPlateName, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult);

        [OperationContract]
        long m_lngQueryPlateResult(string p_strPlateNameID, out DataTable p_dtResult);

        [OperationContract]
        long m_lngInsertPlateResult(string p_strPlateName, string p_strPlateChName, clslisPlateResult[] p_objPlateResultArr, out string p_strPlateResultID);

        [OperationContract]
        long m_lngGetSequenceArr_clsItemSetSvc(string p_strSeqName, int p_intNumber, out int[] p_intSeqIdArr);

        [OperationContract]
        long m_lngDeletePlateResult(string p_strPlateNameID);

        [OperationContract]
        long m_lngGetAllPlateResult(out DataTable p_dtResult);

        [OperationContract]
        long m_lngInsertDeviceResult(clsLIS_Device_Test_ResultVO[] p_objResultArr);

        [OperationContract(Name = "m_lngQueryCheckItemCustomRes02")]
        long m_lngQueryCheckItemCustomRes(string p_strCheckItemID, out DataTable p_dtResult);

        #endregion

        #region clsMK3DeviceCommunications

        [OperationContract]
        long m_lngQueryChcekItemCustomOrder(string p_strCheckItemID, out clsLisCheckItemCustomOrder p_objCheckItemCustomOrder);

        [OperationContract]
        long m_lngUpdateChcekItemCustomOrder(clsLisCheckItemCustomOrder p_objCheckItemCustomOrderVO);

        [OperationContract]
        long m_lngInsertCheckItemCustomOrder(clsLisCheckItemCustomOrder p_objCheckItemCustomOrderVO);

        [OperationContract]
        long m_lngDeleteCheckItemCustomOrder(string p_strCheckItemID);


        #endregion

        #region clsQcNew

        [OperationContract]
        long m_lngQueryDeviceInfo(string p_strComputerName, out DataTable p_dtResult);

        [OperationContract]
        long m_lngQueryQCDeviceResult(int p_strQCId, out List<double> lstDbl);

        [OperationContract]
        long m_lngQueryQCInfo(int p_intQCID, out List<clsLisQCConcentrationVO> p_lstQCConTemp);

        [OperationContract]
        long m_lngGetAllCheckItemInfo(out DataTable p_dtResult);

        [OperationContract]
        long m_lngQueryItemQCResult(clsLisQCBatchSchVO objSch, out DataTable p_dtResult);

        [OperationContract]
        long m_lngQCCheckItem(string p_strDeviceId, out DataTable p_dtResult);

        [OperationContract]
        long m_lngQueryQCLot(clsLisQCBatchSchVO objSch, out DataTable p_dtResult);

        [OperationContract]
        long m_lngQueryResult(clsLisQCBatchSchVO objSch, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult, out DataTable p_dtQcResult);

        [OperationContract]
        long m_lngUpdateConcentration(clsLisConcentrationVO QCBatch);

        [OperationContract]
        long m_lngInsertCheckMethod(clsLisCheckMethodVO p_objCheckMethod, out int p_intSeq);

        [OperationContract]
        long m_lngUpdateCheckMethod(clsLisCheckMethodVO p_objCheckMethod);

        [OperationContract]
        long m_lngInsertConcentration(clsLisConcentrationVO p_objConcentration, out int p_intSeq);

        [OperationContract]
        long m_lngDeleteCheckMethod(int p_intSeq);

        [OperationContract]
        long m_lngInsertVendor(clsLisVendorVO p_objVendor, out int p_intSeq);

        [OperationContract]
        long m_lngUpdateVendor(clsLisVendorVO p_objVendor);

        [OperationContract]
        long m_lngUpdateWorkGruop(clsLisWorkGroupVO p_objWorkGroup);

        [OperationContract]
        long m_lngInsertWorkGroup(clsLisWorkGroupVO p_objWorkGroup, out int p_intSeq);

        [OperationContract]
        long m_lngFindCheckMethod(out clsLisCheckMethodVO[] p_objResultArr);

        [OperationContract]
        long m_lngFindConcentration(out clsLisConcentrationVO[] p_objResultArr);

        [OperationContract]
        long m_lngFindVendor(out clsLisVendorVO[] p_objResultArr);

        [OperationContract]
        long m_lngFindWorkGroup(out clsLisWorkGroupVO[] p_objResultArr);

        [OperationContract]
        long m_lngInsertQCConcentration(clsLisQCConcentrationVO p_objQCConcentration);

        [OperationContract]
        long m_lngUpdateQCConcentration(clsLisQCConcentrationVO p_objQCConcentration);

        [OperationContract]
        long m_lngFindDelQCConcentration(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr);

        [OperationContract]
        long m_lngDeleteQCRule(int p_intSeq);

        [OperationContract]
        long m_lngInsertQCRule(clsLisQCRuleVO p_objRule, out int p_intSeq);

        [OperationContract]
        long m_lngUpdateQCRule(clsLisQCRuleVO p_objRule);

        #endregion

        #region bizLisReport

        [OperationContract]
        long m_lngGetLISCheck_Category(out DataTable p_dtCategory, bool isCS);

        [OperationContract]
        long m_lngQueryWorkLoadByAppDept(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS);

        [OperationContract]
        long m_lngQueryWorkLoadByAppDoctor(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS);

        [OperationContract]
        long m_lngQueryWorkLoadByCheckItem(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS);

        [OperationContract]
        long m_lngQueryWorkLoadByCheckResult(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS);

        [OperationContract]
        long m_lngQueryWorkLoadByCommitorID(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS);

        [OperationContract]
        long m_lngQueryWorkLoadByDevice(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS);

        #endregion

    }
}
