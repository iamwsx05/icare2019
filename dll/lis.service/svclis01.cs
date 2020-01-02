using com.digitalwave.iCare.middletier.LIS;
using System;
using System.Collections.Generic;
using System.Data;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Lis.Service
{
    public class SvcLis01 : Lis.Itf.ItfLis01
    {

        #region bizCriticalValue

        // 获取申请单信息 
        public EntityCriticalMain GetLisApplication(string applyId)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetLisApplication(applyId);
            }
        }

        // 保存检验危急值 
        public int SaveCriticalValue(string applyId, string confirmOperID, DateTime confirmDate, List<EntityCriticalLis> lstItem, bool isYG, bool isValid)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.SaveCriticalValue(applyId, confirmOperID, confirmDate, lstItem, isYG, isValid);
            }
        }

        // 删除检验危急值 
        public int DelCriticalValue(string applyId)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.DelCriticalValue(applyId);
            }
        }

        /// <summary>
        /// 获取危急值列表
        /// </summary>
        /// <param name="typeId">1 门诊; 2 住院</param>
        /// <param name="qid"></param>
        /// <param name="lstMain" ></param>
        /// <param name="lstDet"></param> 
        public void GetCriticalvalList(int typeId, string qid, out List<EntityCriticalMain> lstMain, out List<EntityCriticalLis> lstDet)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                svc.GetCriticalvalList(typeId, qid, out lstMain, out lstDet);
            }
        }

        /// <summary>
        /// 临床应答
        /// </summary>
        /// <param name="respVo"></param>
        /// <returns></returns> 
        public int ResponseCriticalValue(EntityCriResponse respVo)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.ResponseCriticalValue(respVo);
            }
        }

        /// <summary>
        /// 检验科认证
        /// </summary>
        /// <param name="cvmId"></param>
        /// <param name="empId"></param>
        /// <param name="desc"></param> 
        public int LisVerify(decimal cvmId, string empId, string desc)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.LisVerify(cvmId, empId, desc);
            }
        }

        /// <summary>
        /// 获取危急值监控类型
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="lstDeptId"></param>
        /// <returns></returns> 
        public EntityCriMonitorType GetCriMonitorType(string empId, List<string> lstDeptId)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetCriMonitorType(empId, lstDeptId);
            }
        }

        /// <summary>
        /// 获取危急值监控循环时间
        /// </summary>
        /// <returns>毫秒, 0 -- 不监控</returns> 
        public int GetCriTime()
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetCriTime();
            }
        }

        /// <summary>
        /// 获取危急值条目
        /// </summary>
        /// <param name="typeId">1 门诊; 2 住院</param>
        /// <param name="qid"></param>
        /// <returns></returns> 
        public List<EntityCriticalMain> GetCriList(int typeId, string qid)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetCriList(typeId, qid);
            }
        }

        /// <summary>
        /// 获取危急值明细
        /// </summary>
        /// <param name="cvmId"></param>
        /// <returns></returns> 
        public List<EntityCriticalLis> GetCriDetail(decimal cvmId)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetCriDetail(cvmId);
            }
        }

        /// <summary>
        /// 获取pacs危急值
        /// </summary>
        /// <param name="cvmId"></param>
        /// <returns></returns> 
        public EntityCriticalPacs GetCriPacs(decimal cvmId)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetCriPacs(cvmId);
            }
        }

        /// <summary>
        /// 危急值汇总报表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptIdArr"></param>
        /// <param name="isYG">是否院感</param>
        /// <returns></returns> 
        public List<EntityCriReport> GetCriReport(DateTime startDate, DateTime endDate, string deptIdArr, bool isYG)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetCriReport(startDate, endDate, deptIdArr, isYG);
            }
        }

        /// <summary>
        /// 获取指定病人的危急值记录
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns> 
        public List<EntityCriticalMain> GetCriListByPid(string pid)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetCriListByPid(pid);
            }
        }

        /// <summary>
        /// 取消危急值
        /// </summary>
        /// <param name="cancelVo"></param>
        /// <returns></returns> 
        public int CancelCriticalValue(EntityCriCancel cancelVo)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.CancelCriticalValue(cancelVo);
            }
        }

        /// <summary>
        /// 获取体检申请项目
        /// </summary>
        /// <param name="regNo"></param>
        /// <returns></returns> 
        public DataTable GetAppItem(string regNo)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return Function.ReNameDatatable(svc.GetAppItem(regNo));
            }
        }

        /// <summary>
        /// 获取院感危急值
        /// </summary>
        /// <returns></returns> 
        public DataTable GetCriYG()
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return Function.ReNameDatatable(svc.GetCriYG());
            }
        }

        public int SaveCriYG(List<string> lstRefDesc)
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.SaveCriYG(lstRefDesc);
            }
        }

        /// <summary>
        /// 获取检验科室电脑IP
        /// </summary>
        /// <returns></returns> 
        public List<string> GetLisPC()
        {
            using (bizCriticalValue svc = new bizCriticalValue())
            {
                return svc.GetLisPC();
            }
        }

        #endregion

        #region clsAdvis2120Svc

        /// <summary>
        /// 添加仪器检验项目与检验项目对应关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns> 
        public long m_lngAddNewCheckItemDeviceCheckItem(clsLisCheckItemDeviceCheckItem_VO p_objRecord)
        {
            using (clsAdvis2120Svc svc = new clsAdvis2120Svc())
            {
                return svc.m_lngAddNewCheckItemDeviceCheckItem(p_objRecord);
            }
        }

        /// <summary>
        /// 修改仪器检验项目与检验项目对应关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSourceCheckItemID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns> 
        public long m_lngModifyCheckItemDeviceCheckItem(string p_strSourceCheckItemID, clsLisCheckItemDeviceCheckItem_VO p_objRecord)
        {
            using (clsAdvis2120Svc svc = new clsAdvis2120Svc())
            {
                return svc.m_lngModifyCheckItemDeviceCheckItem(p_strSourceCheckItemID, p_objRecord);
            }
        }

        /// <summary>
        /// 删除仪器检验项目与检验项目对应关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSourceCheckItemID"></param>
        /// <returns></returns> 
        public long m_lngDelCheckItemDeviceCheckItem(string p_strSourceCheckItemID)
        {
            using (clsAdvis2120Svc svc = new clsAdvis2120Svc())
            {
                return svc.m_lngDelCheckItemDeviceCheckItem(p_strSourceCheckItemID);
            }
        }

        /// <summary>
        /// 插入报告单to表t_opr_lis_check_result
        /// </summary>
        /// <param name="objPrincipal"></param>
        /// <param name="p_intNum"></param>
        /// <param name="p_objResultList"></param>
        /// <returns></returns> 
        public long m_lngInsertReport(int intNum, List<clsAdvia2120ResultInf_VO> p_objResultList, out int p_intInsertNum)
        {
            using (clsAdvis2120Svc svc = new clsAdvis2120Svc())
            {
                return svc.m_lngInsertReport(intNum, p_objResultList, out p_intInsertNum);
            }
        }

        /// <summary>
        /// 为表 t_opr_lis_app_report 新增,修改,删除 记录时用
        /// </summary>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns> 
        public long m_lngInsertAppReportRecord(List<clsT_OPR_LIS_APP_REPORT_VO> p_objRecordVOList)
        {
            using (clsAdvis2120Svc svc = new clsAdvis2120Svc())
            {
                return svc.m_lngInsertAppReportRecord(p_objRecordVOList);
            }
        }

        /// <summary>
        /// 调用本方法时,必需传入 p_strSampleIDArr 中的所有的样本的所有检验项目结果,且只能传入
        /// 在 p_strSampleIDArr 列表之中的样本的检验项目结果;
        /// </summary>
        /// <param name="p_strSampleIDArr"></param>
        /// <param name="p_strOriginDate"></param>
        /// <returns></returns> 
        public long m_lngAddCheckResultList(List<string> p_strSampleIDList, string p_strOriginDate)
        {
            using (clsAdvis2120Svc svc = new clsAdvis2120Svc())
            {
                return svc.m_lngAddCheckResultList(p_strSampleIDList, p_strOriginDate);
            }
        }

        #endregion

        #region clsAppGroupSvc

        // 更新表t_aid_lis_appluser_group的has_child_group字段 
        public long m_lngUpdApplUserGroup(string p_strHasChildGroup, string p_strApplUserGroupID)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngUpdApplUserGroup(p_strHasChildGroup, p_strApplUserGroupID);
            }
        }

        // 查询所有的自定义组的子组信息 
        public long m_lngGetAllApplUserGroupRelation(out clsApplUserGroupRelation_VO[] p_objResultArr)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngGetAllApplUserGroupRelation(out p_objResultArr);
            }
        }

        // 查询一个自定义组下(不含自定义组);的所有检验项目明细 
        public long m_lngGetCheckItemInApplGroupDetailByApplUserGroupID(string strApplUserGroupID, out DataTable dtbCheckItem)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                long rec = svc.m_lngGetCheckItemInApplGroupDetailByApplUserGroupID(strApplUserGroupID, out dtbCheckItem);
                dtbCheckItem = Function.ReNameDatatable(dtbCheckItem);
                return rec;
            }
        }

        // 查询一个自定义组下(含自定义组);的所有检验项目明细 
        public long m_lngGetCheckItemApplGroupRelationByApplUserGroupID(string strApplUserGroupID, out DataTable dtbCheckItem)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                long rec = svc.m_lngGetCheckItemApplGroupRelationByApplUserGroupID(strApplUserGroupID, out dtbCheckItem);
                dtbCheckItem = Function.ReNameDatatable(dtbCheckItem);
                return rec;
            }
        }

        // 更新表t_aid_lis_appluser_group中的字段 
        public long m_lngSetApplUserGroup(ref clsApplUserGroup_VO objApplUserGroupVO)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngSetApplUserGroup(ref objApplUserGroupVO);
            }
        }

        // 删除自定义组合的明细和关系 
        public long m_lngDelApplUserGroupDetailAndRelation(string strApplUserGroupID)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngDelApplUserGroupDetailAndRelation(strApplUserGroupID);
            }
        }

        // 删除自定义组合相关的所有信息 
        public long m_lngDelApplUserGroupInfo(string strApplUserGroupID, string strParentUserGroupID)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngDelApplUserGroupInfo(strApplUserGroupID, strParentUserGroupID);
            }
        }

        // 删除表t_aid_lis_appluser_group_relate的自定义组合关系 
        public long m_lngDelApplUserGroupRelation(string strApplUserGroupID, string strParentUserGroupId)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngDelApplUserGroupRelation(strApplUserGroupID, strParentUserGroupId);
            }
        }

        public long m_lngDelApplUserGroupRelation(string strApplUserGroupID)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngDelApplUserGroupRelation(strApplUserGroupID);
            }
        }

        // 删除表t_aid_lis_appluser_group_detail的自定义组合明细 
        public long m_lngDelApplUserGroupDetail(string strApplUserGroupID)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngDelApplUserGroupDetail(strApplUserGroupID);
            }
        }

        // 删除表t_aid_lis_appluser_group的自定义组合 
        public long m_lngDelApplUserGroup(string strApplUserGroupID)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngDelApplUserGroup(strApplUserGroupID);
            }
        }

        // 获取所有的自定义组合下的所有申请单元信息 
        public long m_lngGetAllUserGroupApplUnitID(out DataTable dtbApplUnit)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                long rec = svc.m_lngGetAllUserGroupApplUnitID(out dtbApplUnit);
                dtbApplUnit = Function.ReNameDatatable(dtbApplUnit);
                return rec;
            }
        }

        // 保存用户自定义申请组合及其明细、关系 
        public long m_lngAddApplUserGroupAndDetail(ref clsApplUserGroup_VO objApplUserGroupVO, ref clsApplUserGroupDetail_VO[] objApplUserGroupDetailList, ref clsApplUserGroupRelation_VO[] objApplUserGroupRelation, clsApplUserGroupRelation_VO p_objParentRelation)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngAddApplUserGroupAndDetail(ref objApplUserGroupVO, ref objApplUserGroupDetailList, ref objApplUserGroupRelation, p_objParentRelation);
            }
        }

        // 保存记录到t_aid_lis_appuser_relate 
        public long m_lngAddApplUserRelation(ref clsApplUserGroupRelation_VO objApplUserGroupRelationVO)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngAddApplUserRelation(ref objApplUserGroupRelationVO);
            }
        }

        // 保存一条记录到t_aid_lis_appuser_group_detail 
        public long m_lngAddApplUserGroupDetail(ref clsApplUserGroupDetail_VO objApplUserGroupDetailVO)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngAddApplUserGroupDetail(ref objApplUserGroupDetailVO);
            }
        }

        // 保存一条记录到t_aid_lis_appuser_group 
        public long m_lngAddAppUserGroup(ref clsApplUserGroup_VO objApplUserGroup)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngAddAppUserGroup(ref objApplUserGroup);
            }
        }

        // 根据用户自定义组ID查询其包含的申请单元信息 
        public long m_lngGetApplUnitByUserGroupID(string strUserGroupID, out clsApplUnit_VO[] objApplUnitVOList)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngGetApplUnitByUserGroupID(strUserGroupID, out objApplUnitVOList);
            }
        }

        // 根据用户自定义组ID查询其子组信息 
        public long m_lngGetChildGroupByUserGroupID(string strUserGroupID, out clsApplUserGroup_VO[] objAppUserGroupVOList)
        {
            using (clsAppGroupSvc svc = new clsAppGroupSvc())
            {
                return svc.m_lngGetChildGroupByUserGroupID(strUserGroupID, out objAppUserGroupVOList);
            }
        }

        #endregion

        #region clsApplicationSvc

        // 更新申请单到发状态 
        public long m_lngSendApplictions(string[] p_strApplicationIDArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngSendApplictions(p_strApplicationIDArr);
            }
        }

        //  作废申请单的样本 
        public long m_lngAddBlankOutInfo(clsLisApplMainVO p_objApplMainVO, clsBlankOutApplicationVO p_objBlankOutInfo)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddBlankOutInfo(p_objApplMainVO, p_objBlankOutInfo);
            }
        }

        // 作废样本子方法 
        public long m_lngAddNewApplForBlankOut(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewApplForBlankOut(p_objLisApplMainVO, out p_objLisApplMainOutVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr);
            }
        }

        public long m_lngAddNewAppAndSampleInfoWithBarcode(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppAndSampleInfoWithBarcode(p_objLisApplMainVO, out p_objLisApplMainOutVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            }
        }

        public long m_lngAddNewAppAndSampleInfoWithoutReceive(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppAndSampleInfoWithoutReceive(p_objLisApplMainVO, out p_objLisApplMainOutVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            }
        }

        public long m_lngInsertAttachrelation(clsLisApplMainVO p_objMainVO)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngInsertAttachrelation(p_objMainVO);
            }
        }

        public long m_lngPISApplication(clsPISApplicationInfoToLIS p_objRecord, out string[] p_strApplicationIDArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngPISApplication(p_objRecord, out p_strApplicationIDArr);
            }
        }

        /// <summary>
        /// 根据申请单ID作废相应的仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strOringinDate"></param>
        /// <returns></returns> 
        public long m_lngDeleteDeviceRelationByApplicationID(string p_strApplicationID, string p_strOringinDate)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngDeleteDeviceRelationByApplicationID(p_strApplicationID, p_strOringinDate);
            }
        }

        // 修改申请单病人信息并相应修改样本信息 
        public long m_lngSetApplicationAndSamplePatientInfo(clsLisApplMainVO p_objApplVO)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngSetApplicationAndSamplePatientInfo(p_objApplVO);
            }
        }

        /// <summary>
        /// 为表 添加一批记录到申请单申请单元项目表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns> 
        public long m_lngAddNewAppUintItemArr(clsLisAppUnitItemVO[] p_objRecordVOArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppUintItemArr(p_objRecordVOArr);
            }
        }

        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strOpID"></param>
        /// <returns></returns> 
        public long m_lngDeleteApp(string p_strAppID, string p_strOpID)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngDeleteApp(p_strAppID, p_strOpID);
            }
        }

        /// <summary>
        /// 联合查询申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSchVO">承载联合查询条件的VO,其中值为NULL的成员会被必忽略</param>
        /// <param name="p_objAppVOArr">返回的承载申请单信息的数组</param>
        /// <returns>0:失败;1:成功</returns> 
        public long m_lngGetAppInfoByCondition(clsLISApplicationSchVO p_objSchVO, out clsLisApplMainVO[] p_objAppVOArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetAppInfoByCondition(p_objSchVO, out p_objAppVOArr);
            }
        }

        /// <summary>
        /// 根据申请单ID查询申请单下的报告组
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns> 
        public long m_lngGetAppReportVOArrByApplicationID(string p_strApplicationID, out clsT_OPR_LIS_APP_REPORT_VO[] p_objResultArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetAppReportVOArrByApplicationID(p_strApplicationID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 根据申请单ID和报告组ID查询申请申请单下的标本组
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strReportGroupID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns> 
        public long m_lngGetAppSampleGroupVOArr(string p_strApplicationID, string p_strReportGroupID, out clsT_OPR_LIS_APP_SAMPLE_VO[] p_objResultArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetAppSampleGroupVOArr(p_strApplicationID, p_strReportGroupID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 根据申请单ID查询申请单下的标本组
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns> 
        public long m_lngGetAppSampleGroupVOArr(string p_strApplicationID, out clsT_OPR_LIS_APP_SAMPLE_VO[] p_objResultArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetAppSampleGroupVOArr(p_strApplicationID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 根据申请单ID、样本组ID和报告组ID查询申请单下的检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID">申请单ID</param>
        /// <param name="p_strSampleGroupID">样本组ID</param>
        /// <param name="p_strReportGroupID">报告组ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns> 
        public long m_lngGetAppCheckItemVOArr(string p_strApplicationID, string p_strSampleGroupID, string p_strReportGroupID, out clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objResultArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetAppCheckItemVOArr(p_strApplicationID, p_strSampleGroupID, p_strReportGroupID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 根据申请单查询申请单下的检验项目
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID">申请单ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns> 
        public long m_lngGetAppCheckItemVOArr(string p_strApplicationID, out clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objResultArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetAppCheckItemVOArr(p_strApplicationID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 根据申请单ID查询申请单下的申请单元
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID">申请单ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns> 
        public long m_lngGetAppApplyUnitVOByApplicationID(string p_strApplicationID, out clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objResultArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetAppApplyUnitVOByApplicationID(p_strApplicationID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 为表 T_OPR_LIS_APP_CHECK_ITEM 新增记录时用
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns> 
        public long m_lngAddNewAppCheckItem(clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objRecordVOArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppCheckItem(p_objRecordVOArr);
            }
        }

        /// <summary>
        /// 为表 T_OPR_LIS_APP_SAMPLE 新增记录时用 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns> 
        public long m_lngAddNewAppSampleGroup(clsT_OPR_LIS_APP_SAMPLE_VO[] p_objRecordVOArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppSampleGroup(p_objRecordVOArr);
            }
        }

        /// <summary>
        /// 为表 t_opr_lis_app_report 新增,修改,删除 记录时用  
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns> 
        public long m_lngInsertAppReportRecord(clsT_OPR_LIS_APP_REPORT_VO[] p_objRecordVOArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngInsertAppReportRecord(p_objRecordVOArr);
            }
        }

        // m_lngAddNewAppl 增加新的检验申请记录(修改,删除时都为新增记录); 
        public long m_lngAddNewAppl(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppl(p_objLisApplMainVO, out p_objLisApplMainOutVO);
            }
        }

        // 增加-组新的检验申请信息(包含报告组,申请单元,样本组,检验项目在内的全部信息); 
        public long m_lngAddNewAppInfo(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppInfo(p_objLisApplMainVO, out p_objLisApplMainOutVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            }
        }

        // 增加-组新的检验申请信息(包含报告组,申请单元,样本组,检验项目,和样本在内的全部信息); 
        public long m_lngAddNewAppAndSampleInfo(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppAndSampleInfo(p_objLisApplMainVO, out p_objLisApplMainOutVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            }
        }

        // 增加-组新的检验申请信息(包含报告组,申请单元,样本组,检验项目,和样本在内的全部信息); 解决三层爆错的问题 加上了一个ref
        public long m_lngAddNewAppAndSampleInfoNew(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO, ref clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, ref clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, ref clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, ref clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, ref clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddNewAppAndSampleInfoNew(p_objLisApplMainVO, out p_objLisApplMainOutVO, ref p_objReportArr, ref p_objAppSampleArr, ref p_objAppItemArr, ref p_objAppUnitArr, ref p_objAppUnitItemArr);
            }
        }

        // 确认报告
        public long m_lngConfirmAppReport(string p_strAppID, string p_strReportID, string p_strConfirmerID, string p_strConfirmDate)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngConfirmAppReport(p_strAppID, p_strReportID, p_strConfirmerID, p_strConfirmDate);
            }
        }

        // 更新SampleID 到 AppSampleGroup      
        public long m_lngUpdateAppSampleGroupSampleID(string p_strAppID, string p_strSampleID)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngUpdateAppSampleGroupSampleID(p_strAppID, p_strSampleID);
            }
        }

        // 根据Application_ID_CHR更新t_opr_lis_application_detail的状态字为
        public long m_lngSetStatusToConfirmByApplicationIDAndGroupID(string strGroupID, string strApplID, string strStatus)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngSetStatusToConfirmByApplicationIDAndGroupID(strGroupID, strApplID, strStatus);
            }
        }

        // 新增记录组合与样本的关系的记录
        public long m_lngAddGroupSampleRelation(string strApplForm, string strSampleID, string strGroupID)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngAddGroupSampleRelation(strApplForm, strSampleID, strGroupID);
            }
        }

        // 根据application_id和group_id更新t_opr_lis_application_detail表的检验师意见
        public long m_lngSetSummaryByApplicationIDAndGroupID(string strSummary, string strApplID, string strGroupID)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngSetSummaryByApplicationIDAndGroupID(strSummary, strApplID, strGroupID);
            }
        }

        // 更新申请单信息，需要insert两个表，一个为t_opr_lis_application,一个为t_opr_lis_application_detail
        public long m_lngUpdateAppl(clsLisApplMainVO p_objLisApplMainVO)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngUpdateAppl(p_objLisApplMainVO);
            }
        }

        // 在增加新的申请单时，判断t_opr_lis_application中的application_form_no_chr在所有PStatus_int>0的记录中是否唯一
        public long m_lngQueryApplFormNo(string p_strApplFormNo)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngQueryApplFormNo(p_strApplFormNo);
            }
        }

        // 根据申请号，设置其处理状态（PStatus_int);
        public long m_lngSetApplicationStatus(string strAppId, int intStatus)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngSetApplicationStatus(strAppId, intStatus);
            }
        }

        // 作废申请单
        public long m_lngUpdateVoidApply(string p_strAppID, string p_strOperatorID)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngUpdateVoidApply(p_strAppID, p_strOperatorID);
            }
        }

        // 通过申请单号判断是否审核
        /// <summary>
        /// 通过申请单号判断是否审核
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strApplicationID"></param>
        /// <param name="p_strStatus"></param>
        /// <returns></returns> 
        public long m_lnqQueryConfirmReport(string p_strApplicationID, out DataTable p_dtResult, out DataTable p_dtUnitResult)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                long rec = svc.m_lnqQueryConfirmReport(p_strApplicationID, out p_dtResult, out p_dtUnitResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                p_dtUnitResult = Function.ReNameDatatable(p_dtUnitResult);
                return rec;
            }
        }

        // 获取系统参数  
        public long m_lngGetSysParm(string p_strParmCode, out string p_strParmValue)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetSysParm(p_strParmCode, out p_strParmValue);
            }
        }

        // 修改t_opr_lis_app_report 表的打印次数  
        public long m_lngUpdatePrinctTime(string p_strApplicaionID)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngUpdatePrinctTime(p_strApplicaionID);
            }
        }

        // 取消审核
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="p_strAppID">申请单ID</param>
        /// <param name="p_strOperatorID">操作员工ID</param>
        /// <returns>大于0成功，否则失败</returns> 
        public long m_lngCancelConfimReport(string p_strAppID, string p_strOperatorID)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngCancelConfimReport(p_strAppID, p_strOperatorID);
            }
        }

        // 获取检验类别
        /// <summary>
        /// 获取检验类别
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <returns></returns> 
        public long m_lngQueryCheckCategory(out DataTable p_dtResult)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                long rec = svc.m_lngQueryCheckCategory(out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        // 查询申请信息
        /// <summary>
        /// 联合查询申请单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSchVO">承载联合查询条件的VO,其中值为NULL的成员会被必忽略</param>
        /// <param name="p_objAppVOArr">返回的承载申请单信息的数组</param>
        /// <returns>0:失败;1:成功</returns> 
        public long m_lngGetAppInfoByModifDate(clsLISApplicationSchVO p_objSchVO, string p_strCheckCategory, out clsLisApplMainVO[] p_objAppVOArr)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.m_lngGetAppInfoByModifDate(p_objSchVO, p_strCheckCategory, out p_objAppVOArr);
            }
        }

        // 修改急诊状态  
        public int UpdateEmergencyStatus(clsLisApplMainVO appMainVo)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return svc.UpdateEmergencyStatus(appMainVo);
            }
        }

        #region 根据申请单元ID获取标本
        /// <summary>
        /// 根据申请单元ID获取标本
        /// </summary>
        /// <param name="appUnitId"></param>
        /// <returns></returns>
        public DataTable GetSampleInfo(string appUnitId)
        {
            using (clsApplicationSvc svc = new clsApplicationSvc())
            {
                return Function.ReNameDatatable(svc.GetSampleInfo(appUnitId));
            }
        }
        #endregion

        #endregion

        #region clsApplyPropertySv

        // 保存基本资料 
        public long m_lngSavePropertyAndValue(clsApplyUnitPropertyDoc p_objDoc, out clsApplyUnitPropertyDoc p_objOutDoc)
        {
            using (clsApplyPropertySv svc = new clsApplyPropertySv())
            {
                return svc.m_lngSavePropertyAndValue(p_objDoc, out p_objOutDoc);
            }
        }

        // 保存关联 
        public long m_lngSaveRelate(string p_strApplyUnitID, clsUnitPropertyRelate_VO[] p_objVOArr)
        {
            using (clsApplyPropertySv svc = new clsApplyPropertySv())
            {
                return svc.m_lngSaveRelate(p_strApplyUnitID, p_objVOArr);
            }
        }

        #endregion

        #region clsApplyUnitSvc

        // 更新申请单元 
        public long m_lngSetApplyUnit(clsApplUnit_VO p_objApplyUnit, List<clsApplUnitDetail_VO> p_arlAddDetail, List<clsApplUnitDetail_VO> p_arlRemoveDetail)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngSetApplyUnit(p_objApplyUnit, p_arlAddDetail, p_arlRemoveDetail);
            }
        }

        // 根据申请单元ID和检验项目ID删除申请单元明细信息 
        public long m_lngDelApplyUnitDetailByApplyIDAndCheckItemID(clsApplUnitDetail_VO p_objRecord)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngDelApplyUnitDetailByApplyIDAndCheckItemID(p_objRecord);
            }
        }

        // 更新申请单元的基本资料 
        public long m_lngSetApplUnit(ref clsApplUnit_VO objApplUnit)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngSetApplUnit(ref objApplUnit);
            }
        }

        // 删除申请单元及其明细 
        public long m_lngDelApplUnitAndDetail(string strApplUnitID)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngDelApplUnitAndDetail(strApplUnitID);
            }
        }

        // 删除申请单元明细 
        public long m_lngDelApplUnitDetail(string strApplUnitID)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngDelApplUnitDetail(strApplUnitID);
            }
        }

        // 删除申请单元 
        public long m_lngDelApplUnit(string strApplUnitID)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngDelApplUnit(strApplUnitID);
            }
        }

        // 保存申请单元及其明细 
        public long m_lngAddApplUnitAndDetail(ref clsApplUnit_VO objApplUnitVO, ref clsApplUnitDetail_VO[] objApplUnitDetailVOList)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngAddApplUnitAndDetail(ref objApplUnitVO, ref objApplUnitDetailVOList);
            }
        }

        // 保存一条记录到t_aid_lis_apply_unit_detail 
        public long m_lngAddApplUnitDetail(ref clsApplUnitDetail_VO objApplUnitDetailVO)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngAddApplUnitDetail(ref objApplUnitDetailVO);
            }
        }

        // 保存一条记录到条t_aid_lis_apply_unit表 
        public long m_lngAddApplUnit(ref clsApplUnit_VO objApplUnitVO)
        {
            using (clsApplyUnitSvc svc = new clsApplyUnitSvc())
            {
                return svc.m_lngAddApplUnit(ref objApplUnitVO);
            }
        }

        #endregion

        #region clsBaseCheckGroupSvc

        // 根据样本的类型ID查询所有可能做的基本检验组  
        public long m_lngGetBaseCheckGroupBySampleTypeID(string p_strSampleTypeID, out System.Data.DataTable p_dtbGroup)
        {
            using (clsBaseCheckGroupSvc svc = new clsBaseCheckGroupSvc())
            {
                long rec = svc.m_lngGetBaseCheckGroupBySampleTypeID(p_strSampleTypeID, out p_dtbGroup);
                p_dtbGroup = Function.ReNameDatatable(p_dtbGroup);
                return rec;
            }
        }

        // 根据基本检验组ID查找能做这组检验的所有的仪器类型ID  
        public long m_lngGetDeviceModelByBaseCheckGroupID(string p_strBaseCheckGroupID, out System.Data.DataTable p_dtbDeviceModel)
        {
            using (clsBaseCheckGroupSvc svc = new clsBaseCheckGroupSvc())
            {
                long rec = svc.m_lngGetDeviceModelByBaseCheckGroupID(p_strBaseCheckGroupID, out p_dtbDeviceModel);
                p_dtbDeviceModel = Function.ReNameDatatable(p_dtbDeviceModel);
                return rec;
            }
        }

        #endregion

        #region clsBatchSaveReportSvc

        /// <summary>
        /// 批量保存检验编号
        /// </summary>
        /// <param name="p_objMainArr">源数据</param>
        /// <param name="p_strOperator">操作者</param>
        /// <returns>大于0成功，否则失败</returns> 
        public long m_lngUpdateCheckNUM(clsLisApplMainVO[] p_objMainArr, string p_strOperator)
        {
            using (clsBatchSaveReportSvc svc = new clsBatchSaveReportSvc())
            {
                return svc.m_lngUpdateCheckNUM(p_objMainArr, p_strOperator);
            }
        }

        #endregion

        #region clsBatchSaveReportQuerySvc

        /// <summary>
        /// 查询申请单信息通过条码号
        /// </summary>
        /// <param name="p_strBarcode">条码号</param>
        /// <param name="p_objMainVO">返回的病人信息</param>
        /// <returns>大于0成功，否则失败</returns> 
        public long m_lngQuerySampleInfo(string p_strBarcode, out clsLisApplMainVO p_objMainVO)
        {
            using (clsBatchSaveReportQuerySvc svc = new clsBatchSaveReportQuerySvc())
            {
                return svc.m_lngQuerySampleInfo(p_strBarcode, out p_objMainVO);
            }
        }

        #endregion

        #region clsCheckGroupSvc

        // 删除报告组及其明细  
        public long m_lngDelReportGroupAndDetail(string strReportGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelReportGroupAndDetail(strReportGroupID);
            }
        }

        // 删除报告组明细 (go); 
        public long m_lngDelReportGroupDetail(string strReportGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelReportGroupDetail(strReportGroupID);
            }
        }

        // 删除报告组 (go); 
        public long m_lngDelReportGroup(string strReportGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelReportGroup(strReportGroupID);
            }
        }

        // 保存报告组及其明细 (go); 
        public long m_lngAddReportGroupAndDetail(ref clsReportGroup_VO objReportGroupVO, ref clsReportGroupDetail_VO[] objReportGroupDetailVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddReportGroupAndDetail(ref objReportGroupVO, ref objReportGroupDetailVO);
            }
        }

        // 保存记录到t_aid_lis_report_group_detail (go); 
        public long m_lngAddReportGroupDetail(ref clsReportGroupDetail_VO objReportGroupDetailVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddReportGroupDetail(ref objReportGroupDetailVO);
            }
        }

        // 保存记录到t_aid_lis_report_group (go); 
        public long m_lngAddReportGroup(ref clsReportGroup_VO objReportGroupVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddReportGroup(ref objReportGroupVO);
            }
        }

        // 删除标本组及其明细 (go); 
        public long m_lngDelSampleGroupAndDetail(string strSampleGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelSampleGroupAndDetail(strSampleGroupID);
            }
        }

        // 删除标本组明细 (go); 
        public long m_lngDelSampleGroupDetail(string strSampleGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelSampleGroupDetail(strSampleGroupID);
            }
        }

        // 删除标本组(go); 
        public long m_lngDelSampleGroup(string strSampleGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelSampleGroup(strSampleGroupID);
            }
        }

        // 保存标本组及其明细 (go); 
        public long m_lngAddSampleGroupAndDetail(ref clsSampleGroup_VO objSampleGroup, ref clsSampleGroupDetail_VO[] objSampleGroupDetailVOList)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddSampleGroupAndDetail(ref objSampleGroup, ref objSampleGroupDetailVOList);
            }
        }

        // 保存记录到t_aid_lis_sample_group_detail表 (go); 
        public long m_lngAddSampleGroupDetail(clsSampleGroupDetail_VO objSampleGroupDetailVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddSampleGroupDetail(objSampleGroupDetailVO);
            }
        }

        // 保存记录到t_aid_lis_sample_group表 (go); 
        public long m_lngAddSampleGroup(ref clsSampleGroup_VO objSampleGroupVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddSampleGroup(ref objSampleGroupVO);
            }
        }

        // 根据CheckGroupID更新t_aid_lis_check_group的信息  
        public long m_lngUpdCheckGroupByGroupID(ref clsCheckGroup_VO objCheckGroupVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngUpdCheckGroupByGroupID(ref objCheckGroupVO);
            }
        }

        // 修改检验组信息  
        public long m_lngSetCheckGroup(ref clsCheckGroup_VO objCheckGroupVO, ref clsCheckGroupDetail_VO[] objCheckGroupDetail, ref clsCheckGroupRelation_VO[] objCheckGroupRelation, ref clsGroupSample_VO[] objGroupSample, string strCheckGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngSetCheckGroup(ref objCheckGroupVO, ref objCheckGroupDetail, ref objCheckGroupRelation, ref objGroupSample, strCheckGroupID);
            }
        }

        // 删除检验组相关的表信息  
        public long m_lngDelCheckGroupRelatedInfo(string strCheckGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelCheckGroupRelatedInfo(strCheckGroupID);
            }
        }

        // 根据GroupID删除表t_aid_lis_check_group_detail中的记录 
        public long m_lngDelCheckGroupDetail(string strCheckGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelCheckGroupDetail(strCheckGroupID);
            }
        }

        // 根据GroupID删除表t_aid_lis_check_group_relation中的记录 
        public long m_lngDelCheckGroupRelation(string strCheckGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelCheckGroupRelation(strCheckGroupID);
            }
        }

        // 根据GroupID删除表t_aid_lis_group_sample  
        public long m_lngDelGroupSample(string strGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelGroupSample(strGroupID);
            }
        }

        // 根据GroupID删除表t_aid_lis_check_group  
        public long m_lngDelCheckGroup(string strGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelCheckGroup(strGroupID);
            }
        }

        // 删除检验组相关的所有信息  
        public long m_lngDelAllCheckGroupInfo(string strCheckGroupID)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngDelAllCheckGroupInfo(strCheckGroupID);
            }
        }

        // 增加一条新的检验组记录到表t_aid_lis_check_group  
        public long m_lngAddCheckGroup(ref clsCheckGroup_VO objCheckGroupVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddCheckGroup(ref objCheckGroupVO);
            }
        }

        // 增加一条新的检验组关系记录到表t_aid_lis_check_group_relation  
        public long m_lngAddCheckGroupRelation(ref clsCheckGroupRelation_VO objCheckGroupRelationVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddCheckGroupRelation(ref objCheckGroupRelationVO);
            }
        }

        // 增加一条新的检验组明细到表t_aid_lis_check_group_detail 
        public long m_lngAddCheckGroupDetail(ref clsCheckGroupDetail_VO objCheckGroupDetailVO)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddCheckGroupDetail(ref objCheckGroupDetailVO);
            }
        }

        // 增加一条记录到表T_AID_LIS_GROUP_SAMPLE  
        public long m_lngAddGroupSample(ref clsGroupSample_VO objGroupSample)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddGroupSample(ref objGroupSample);
            }
        }

        // 新增检验组   
        public long m_lngAddAllCheckGroupInfo(ref clsCheckGroup_VO objCheckGroup, ref clsCheckGroupDetail_VO[] objCheckGroupDetail, ref clsCheckGroupRelation_VO[] objCheckGroupRelation, ref clsGroupSample_VO[] objGroupSample)
        {
            using (clsCheckGroupSvc svc = new clsCheckGroupSvc())
            {
                return svc.m_lngAddAllCheckGroupInfo(ref objCheckGroup, ref objCheckGroupDetail, ref objCheckGroupRelation, ref objGroupSample);
            }
        }

        #endregion

        #region clsCheckItemSvc

        // 新增记录到表T_AID_LIS_VALUETEMPLATE_ITEM
        public long m_lngAddNewValueTemplateItem(clsLisValueTemplateItem_VO p_objRecord)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddNewValueTemplateItem(p_objRecord);
            }
        }

        // 删除表T_AID_LIS_VALUETEMPLATE_ITEM的记录
        public long m_lngDelValueTemplateItem(clsLisValueTemplateItem_VO p_objRecord)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelValueTemplateItem(p_objRecord);
            }
        }

        // 复用模板 
        public long m_lngReuseTemplate(clsLisValueTemplateItem_VO p_objOldRecord, clsLisValueTemplateItem_VO p_objNewRecord)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngReuseTemplate(p_objOldRecord, p_objNewRecord);
            }
        }

        // 新增记录到表T_AID_LIS_VALUETEMPLATE
        public long m_lngAddNewValueTemplate(clsLisValueTemplate_VO p_objRecord)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddNewValueTemplate(p_objRecord);
            }
        }

        // 删除表T_AID_LIS_VALUETEMPLATE的记录
        public long m_lngDelValueTemplate(string p_strTemplateID)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelValueTemplate(p_strTemplateID);
            }
        }

        // 新增记录到表T_AID_LIS_VALUETEMPLATE_DETAIL
        public long m_lngAddNewValueTemplateDetail(clsLisValueTemplateDetail_VO p_objRecord)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddNewValueTemplateDetail(p_objRecord);
            }
        }

        // 删除表T_AID_LIS_VALUETEMPLATE_DETAIL的记录
        public long m_lngDelVauleTemplateDetail(string p_strTemplateID, int p_strIdx)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelVauleTemplateDetail(p_strTemplateID, p_strIdx);
            }
        }

        // 更新表T_AID_LIS_VALUETEMPLATE_DETAIL的记录
        public long m_lngModifyVauleTemplateDetail(clsLisValueTemplateDetail_VO p_objRecord)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngModifyVauleTemplateDetail(p_objRecord);
            }
        }

        // 根据模板ID更新T_AID_LIS_VALUETEMPLATE_DETAIL的默认标记
        public long m_lngSetDefaultFlagByTemplateID(string p_strTemplateID, int p_intFlag)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngSetDefaultFlagByTemplateID(p_strTemplateID, p_intFlag);
            }
        }

        // 根据模板ID和idx更新T_AID_LIS_VALUETEMPLATE_DETAIL的默认标记
        public long m_lngSetDefaultFlagByTemplateIDAndIdx(string p_strTemplateID, string p_strIdx, int p_intFlag)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngSetDefaultFlagByTemplateIDAndIdx(p_strTemplateID, p_strIdx, p_intFlag);
            }
        }

        // 新增检验项目的模板及其明细
        public long m_lngAddNewCheckItemVauleTemplate(clsLisValueTemplate_VO p_objValueTemplate, clsLisValueTemplateItem_VO p_objValueTemplateItem, clsLisValueTemplateDetail_VO[] p_objValueTemplateDetailArr)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddNewCheckItemVauleTemplate(p_objValueTemplate, p_objValueTemplateItem, p_objValueTemplateDetailArr);
            }
        }

        // 批量新增、删除和修改模板明细信息
        public long m_lngValueTemplateDetailArr(List<clsLisValueTemplateDetail_VO> p_objAddNewArr, List<clsLisValueTemplateDetail_VO> p_objDelArr, List<clsLisValueTemplateDetail_VO> p_objUpdArr, string p_strTemplateID, string p_strIdx)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngValueTemplateDetailArr(p_objAddNewArr, p_objDelArr, p_objUpdArr, p_strTemplateID, p_strIdx);
            }
        }

        // 修改检验项目及参考值范围
        public long m_lngSetCheckItemAndRef(clsCheckItem_VO p_objCheckItem, clsCheckItemRef_VO[] p_objCheckItemRefArr)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngSetCheckItemAndRef(p_objCheckItem, p_objCheckItemRefArr);
            }
        }

        // 删除检验项目及参考值范围
        public long m_lngDelCheckItemAndRef(string p_strCheckItemID)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelCheckItemAndRef(p_strCheckItemID);
            }
        }

        // 在t_bse_lis_check_item新增检验项目
        public long m_lngAddCheckItem(ref clsCheckItem_VO objCheckItemVO)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddCheckItem(ref objCheckItemVO);
            }
        }

        // 批量新增检验项目参考值范围
        public long m_lngAddCheckItemRefList(ref clsCheckItemRef_VO[] objCheckItemRefVOList)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddCheckItemRefList(ref objCheckItemRefVOList);
            }
        }

        // 新增检验项目的参考值范围
        public long m_lngAddNewItemRef(ref clsCheckItemRef_VO objCheckItemRefVO, DataTable dtDepts)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddNewItemRef(ref objCheckItemRefVO, dtDepts);
            }
        }

        // 更新检验类别信息
        public long m_lngSetCheckCategoryInfo(string strCheckCategory, string strCheckCategoryID)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngSetCheckCategoryInfo(strCheckCategory, strCheckCategoryID);
            }
        }

        // [U]新增检验项目类别
        public long m_lngAddCheckCategory(ref clsCheckCategory_VO p_objCheckCategoryVO)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddCheckCategory(ref p_objCheckCategoryVO);
            }
        }

        // 删除选中的检验类别
        public long m_lngDelCheckCategory(string strCategory)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelCheckCategory(strCategory);
            }
        }

        // 根据check_item_id更新表t_bse_lis_check_item中属于该检验项目的明细资料
        public long m_lngSetCheckItemDetailByCheckItemID(ref clsCheckItem_VO objCheckItemVO)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngSetCheckItemDetailByCheckItemID(ref objCheckItemVO);
            }
        }

        // 根据check_item_id更新t_bse_lis_itemref中属于该检验项目的参考值明细资料
        public long m_lngSetCheckItemRefByCheckItemID(ref clsCheckItemRef_VO objCheckItemRef)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngSetCheckItemRefByCheckItemID(ref objCheckItemRef);
            }
        }

        // 在已经存在的CheckItem的基础上新增检验参考值范围
        public long m_lngAddItemRefByCheckItemID(ref clsCheckItemRef_VO objCheckItemRefVO)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddItemRefByCheckItemID(ref objCheckItemRefVO);
            }
        }

        // 删除表t_bse_lis_check_item检验项目
        public long m_lngDelCheckItem(string strCheckItemID)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelCheckItem(strCheckItemID);
            }
        }

        // 删除表t_bse_lis_itemref所有与该检验项目相关的参考值
        public long m_lngDelCheckItemRef(string strCheckItemID)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelCheckItemRef(strCheckItemID);
            }
        }

        // 删除表t_bse_lis_itemref中某一个序号的检验项目参考值
        public long m_lngDelCheckItemRefBySEQ(string strCheckItemID, string strSEQ)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelCheckItemRefBySEQ(strCheckItemID, strSEQ);
            }
        }

        // 新增样品类别到表T_AID_LIS_SAMPLETYPE
        public long m_lngAddSampleType(ref clsSampleType_VO objSampleTypeVO)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddSampleType(ref objSampleTypeVO);
            }
        }

        // 删除表T_AID_LIS_SAMPLETYPE中的记录
        public long m_lngDelSampleTypeBySampleTypeID(string strSampleTypeID)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelSampleTypeBySampleTypeID(strSampleTypeID);
            }
        }

        // 更新表T_AID_LIS_SAMPLETYPE中的记录
        public long m_lngSetSampleTypeDetailBySampleTypeID(string strSampleType, string strPyCode, string strWbCode, string strSampleTypeID, int intHasFlag)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngSetSampleTypeDetailBySampleTypeID(strSampleType, strPyCode, strWbCode, strSampleTypeID, intHasFlag);
            }
        }

        // 查询出表T_AID_LIS_SAMPLE_CHARACTER所有的样本性状
        public long m_lngGetAllSampleCharacter(string strSampleTypeID, out System.Data.DataTable dtbAllSampleCharacter)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                long rec = svc.m_lngGetAllSampleCharacter(strSampleTypeID, out dtbAllSampleCharacter);
                dtbAllSampleCharacter = Function.ReNameDatatable(dtbAllSampleCharacter);
                return rec;
            }
        }

        // 更新表T_AID_LIS_SAMPLE_CHARACTER中某一序号的样本状态
        public long m_lngSetSampleCharacterBySampleTypeIDAndSEQ(string strSampleTypeID, string strSEQ, string strSampleCharacter, string strPyCode, string strWbCode)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngSetSampleCharacterBySampleTypeIDAndSEQ(strSampleTypeID, strSEQ, strSampleCharacter, strPyCode, strWbCode);
            }
        }

        // 新增样本性状到表T_AID_LIS_SAMPLE_CHARACTER
        public long m_lngAddSampleCharacterBySampleTypeID(ref clsSampleCharacter_VO objSampleCharacterVO)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngAddSampleCharacterBySampleTypeID(ref objSampleCharacterVO);
            }
        }

        // 删除表T_AID_LIS_SAMPLE_CHARACTER的样本性状
        public long m_lngDelSampleCharacterBySampleTypeIDAndSEQ(string strSampleTypeID, string strSEQ)
        {
            using (clsCheckItemSvc svc = new clsCheckItemSvc())
            {
                return svc.m_lngDelSampleCharacterBySampleTypeIDAndSEQ(strSampleTypeID, strSEQ);
            }
        }

        #endregion

        #region clsCheckResultSvc

        // 更新T_OPR_LIS_RESULT_IMPORT_REQ的状态
        public long m_lngSetResultImportReqStatus(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDat, string p_strStatus)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngSetResultImportReqStatus(p_strDeviceID, p_strDeviceSampleID, p_strCheckDat, p_strStatus);
            }
        }

        // 调整结束指针的位置
        public long m_lngSetResultImportReqEndPoint(clsLisResultImportReq_VO p_objRecord)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngSetResultImportReqEndPoint(p_objRecord);
            }
        }

        // 更新表T_OPR_LIS_RESULT_IMPORT_REQ的信息
        public long m_lngSetResultImportReq(clsLisResultImportReq_VO p_objRecord)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngSetResultImportReq(p_objRecord);
            }
        }

        // 向t_opr_lis_check_result表插入多条记录    
        public long m_lngAddCheckResultList(clsCheckResult_VO[] p_objCheckResultList, string[] p_strSampleIDArr, string p_strOriginDate)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngAddCheckResultList(p_objCheckResultList, p_strSampleIDArr, p_strOriginDate);
            }
        }

        // 设置仪器样本重做标志 
        public long m_lngSetDeviceSamplesRecheck(string p_strDeviceID, int p_intImportReq)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngSetDeviceSamplesRecheck(p_strDeviceID, p_intImportReq);
            }
        }

        // 新增融合后的仪器检验结果及日志信息
        public long m_lngAddNewDeviceCheckResultArrANDLog(clsDeviceReslutVO[] p_objDeviceResultArr, clsResultLogVO p_objResultLog)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngAddNewDeviceCheckResultArrANDLog(p_objDeviceResultArr, p_objResultLog);
            }
        }

        // 向表t_opr_lis_result加一条记录
        public long m_lngAddNewDeviceResult(clsDeviceReslutVO objDeviceResultVO)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngAddNewDeviceResult(objDeviceResultVO);
            }
        }

        // 更新t_opr_lis_result_log的User_flag字段
        public long m_lngSetUseFlagByCondition(string p_strDevcieID, int p_strImpReqInt)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngSetUseFlagByCondition(p_strDevcieID, p_strImpReqInt);
            }
        }

        // 向表t_opr_lis_result_log增加一条记录
        public long m_lngAddNewResultLog(clsResultLogVO objResultLogVO)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngAddNewResultLog(objResultLogVO);
            }
        }

        // 向表t_opr_lis_result和t_opr_lis_result_log 1
        public long m_lngAddNullResultAndReslutLog(string strDeviceID, string strDeviceSampleID, string strCheckDat)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngAddNullResultAndReslutLog(strDeviceID, strDeviceSampleID, strCheckDat);
            }
        }

        // 向表t_opr_lis_result加一条记录
        public long m_lngAddDeviceResult(clsDeviceReslutVO objDeviceResultVO)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngAddDeviceResult(objDeviceResultVO);
            }
        }

        // 向表t_opr_lis_result_log增加一条记录
        public long m_lngAddResultLog(clsResultLogVO objResultLogVO)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngAddResultLog(objResultLogVO);
            }
        }

        // 根据样品ID号和check_item_id更新表t_opr_lis_check_result中的记录信息
        public long m_lngSetCheckResultBySampleIDAndCheck_item_id(clsCheckResult_VO p_objCheckResultVO)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngSetCheckResultBySampleIDAndCheck_item_id(p_objCheckResultVO);
            }
        }

        // 更新表t_opr_lis_check_result多条记录
        public long m_lngSetCheckResultList(clsCheckResult_VO[] p_objCheckResultList)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngSetCheckResultList(p_objCheckResultList);
            }
        }

        // 向表t_opr_lis_device_relation增加一条新记录
        public long m_lngAddnewDeviceRelation(clsDeviceRelation_VO p_objDeviceRelation_VO)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngAddnewDeviceRelation(p_objDeviceRelation_VO);
            }
        }

        // 根据SampleID设置表t_opr_lis_req_check字段stepflag_chr的状态
        public long m_lngSetReqCheckStepFlag(string p_strSampleID, int p_intStepFlag)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngSetReqCheckStepFlag(p_strSampleID, p_intStepFlag);
            }
        }

        // 更新仪器结果记录的状态
        public long m_lngRefreshDeviceResultStatus(string p_strDeviceID, DateTime p_dtCheckDate, string p_strDeviceSampleID, int p_intStatus)
        {
            using (clsCheckResultSvc svc = new clsCheckResultSvc())
            {
                return svc.m_lngRefreshDeviceResultStatus(p_strDeviceID, p_dtCheckDate, p_strDeviceSampleID, p_intStatus);
            }
        }

        #endregion

        #region clsDeviceSvc

        // 查询所有当前可用检验仪器列表     
        public long m_lngGetDeviceList(out DataTable p_dtbDeviceList)
        {
            using (clsDeviceSvc svc = new clsDeviceSvc())
            {
                long rec = svc.m_lngGetDeviceList(out p_dtbDeviceList);
                p_dtbDeviceList = Function.ReNameDatatable(p_dtbDeviceList);
                return rec;
            }
        }

        // 查询所有的仪器类型名称 
        public long m_lngGetDeviceModelNameByDeviceID(out DataTable dtbAllDeciveName)
        {
            using (clsDeviceSvc svc = new clsDeviceSvc())
            {
                long rec = svc.m_lngGetDeviceModelNameByDeviceID(out dtbAllDeciveName);
                dtbAllDeciveName = Function.ReNameDatatable(dtbAllDeciveName);
                return rec;
            }
        }

        // 查询某台仪器所有能做的质控项目 
        public long m_lngGetQCCheckItemByDeviceID(string strDeviceID, out DataTable dtbQCCheckItem)
        {
            using (clsDeviceSvc svc = new clsDeviceSvc())
            {
                long rec = svc.m_lngGetQCCheckItemByDeviceID(strDeviceID, out dtbQCCheckItem);
                dtbQCCheckItem = Function.ReNameDatatable(dtbQCCheckItem);
                return rec;
            }
        }

        // 根据检验项目号，查询可以做该检验项目的仪器列表 
        public long m_lngGetDeviceListByCheckGroup(string p_strCheckGroupId, out DataTable p_dtbDeviceList)
        {
            using (clsDeviceSvc svc = new clsDeviceSvc())
            {
                long rec = svc.m_lngGetDeviceListByCheckGroup(p_strCheckGroupId, out p_dtbDeviceList);
                p_dtbDeviceList = Function.ReNameDatatable(p_dtbDeviceList);
                return rec;
            }
        }

        // 查询某台仪器所有可能的检验项目 
        public long m_lngGetDeviceCheckGroupList(string strDeviceId, out DataTable p_dtbDeviceCheckGroupList)
        {
            using (clsDeviceSvc svc = new clsDeviceSvc())
            {
                long rec = svc.m_lngGetDeviceCheckGroupList(strDeviceId, out p_dtbDeviceCheckGroupList);
                p_dtbDeviceCheckGroupList = Function.ReNameDatatable(p_dtbDeviceCheckGroupList);
                return rec;
            }
        }

        // 查询所有的仪器类型 
        public long m_lngQueryAllDevType(out System.Data.DataTable p_dtbDevType)
        {
            using (clsDeviceSvc svc = new clsDeviceSvc())
            {
                long rec = svc.m_lngQueryAllDevType(out p_dtbDevType);
                p_dtbDevType = Function.ReNameDatatable(p_dtbDevType);
                return rec;
            }
        }

        // 查询某一类的所有具体仪器 
        public long m_lngQueryAllDev(string p_strDevType, out System.Data.DataTable p_dtbDev)
        {
            using (clsDeviceSvc svc = new clsDeviceSvc())
            {
                long rec = svc.m_lngQueryAllDev(p_strDevType, out p_dtbDev);
                p_dtbDev = Function.ReNameDatatable(p_dtbDev);
                return rec;
            }
        }

        // 根据某台仪器ID查询该仪器所能做的所有检验单项信息 
        public long m_lngGetCheckItemsByDevID(string p_strDevID, out System.Data.DataTable p_dtbCheckItem)
        {
            using (clsDeviceSvc svc = new clsDeviceSvc())
            {
                long rec = svc.m_lngGetCheckItemsByDevID(p_strDevID, out p_dtbCheckItem);
                p_dtbCheckItem = Function.ReNameDatatable(p_dtbCheckItem);
                return rec;
            }
        }

        #endregion

        #region clsDictSvc

        // 根据字典种类得到内容列表(除去第一条的类型说明);        
        public long m_lngGetDictListFor(string p_strDictKind, out DataTable p_dtbDictList)
        {
            using (clsDictSvc svc = new clsDictSvc())
            {
                long rec = svc.m_lngGetDictListFor(p_strDictKind, out p_dtbDictList);
                p_dtbDictList = Function.ReNameDatatable(p_dtbDictList);
                return rec;
            }
        }

        // 根据字典种类得到内容列表（除去第一条类型说明）  
        public long m_lngGetDictListFor(string p_strDictKind, out clsAIDDICT_VO[] p_objResultArr)
        {
            using (clsDictSvc svc = new clsDictSvc())
            {
                return svc.m_lngGetDictListFor(p_strDictKind, out p_objResultArr);
            }
        }

        #endregion

        #region clsInputGroupSvc

        // 新增录入组合 
        public long m_lngAddNewInputGroup(clsInputGroupBaseInfo_VO p_objBaseInfo, clsInputGroupDetail_VO[] p_objResults, out string strID)
        {
            using (clsInputGroupSvc svc = new clsInputGroupSvc())
            {
                return svc.m_lngAddNewInputGroup(p_objBaseInfo, p_objResults, out strID);
            }
        }

        // 更新录入组合 
        public long m_lngUpdateInputGroup(clsInputGroupBaseInfo_VO p_objBaseInfo, clsInputGroupDetail_VO[] p_objResults)
        {
            using (clsInputGroupSvc svc = new clsInputGroupSvc())
            {
                return svc.m_lngUpdateInputGroup(p_objBaseInfo, p_objResults);
            }
        }

        // 删除录入组合 
        public long m_lngDeleteInputGroup(string strGroupID)
        {
            using (clsInputGroupSvc svc = new clsInputGroupSvc())
            {
                return svc.m_lngDeleteInputGroup(strGroupID);
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
