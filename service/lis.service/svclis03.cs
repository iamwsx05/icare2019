using com.digitalwave.iCare.middletier.LIS;
using System;
using System.Collections.Generic;
using System.Data;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Lis.Service
{
    public class SvcLis03 : Lis.Itf.ItfLis03
    {

        #region clsQueryCheckResultSvc

        // 根据条件查询T_OPR_LIS_RESULT_IMPORT_REQ表的信息
        public long m_lngGetResultImportReqByCondition(string p_strDeviceID, string p_strCheckDatFrom, string p_strCheckDatTo, out clsLisResultImportReq_VO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetResultImportReqByCondition(p_strDeviceID, p_strCheckDatFrom, p_strCheckDatTo, out p_objResultArr);
            }
        }

        /// <summary>
        /// 查询得到  CheckResultVO
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objResultVO"></param>
        /// <returns></returns>
        public long m_lngGetCheckResultVO(string p_strSampleID, string p_strCheckItemID, out clsCheckResult_VO p_objResultVO)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetCheckResultVO(p_strSampleID, p_strCheckItemID, out p_objResultVO);
            }
        }

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
        public long m_lngGetCheckResultTable(string p_strAppID, string p_strOringinDate, bool p_blnRealResult, out DataTable p_dtbResultList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetCheckResultTable(p_strAppID, p_strOringinDate, p_blnRealResult, out p_dtbResultList);
                p_dtbResultList = Function.ReNameDatatable(p_dtbResultList);
                return rec;
            }
        }

        // 批量打印报告单
        public long m_lngGetBatchReportDataByCondition(string p_strFromSampleID,
            string p_strToSampleID, string p_strFromConfirmDat, string p_strToConfirmDat, string p_strReportGroupID, out clsLisBatchReport_VO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetBatchReportDataByCondition(p_strFromSampleID, p_strToSampleID, p_strFromConfirmDat, p_strToConfirmDat, p_strReportGroupID, out p_objResultArr);
            }
        }

        // 根据条件查询批量打印的报告单列表
        public long m_lngGetLisBatchReportListByCondition(string p_strFromSampleID,
            string p_strToSampleID, string p_strFromConfirmDat, string p_strToConfirmDat, string p_strReportGroupID, string p_strPatientType, out clsLisBatchReportList_VO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetLisBatchReportListByCondition(p_strFromSampleID, p_strToSampleID, p_strFromConfirmDat, p_strToConfirmDat, p_strReportGroupID, p_strPatientType, out p_objResultArr);
            }
        }

        // 根据申请单号和报告组号查询批量打印信息列表
        public long m_lngGetLisBatchReportDetailByCondition(clsLisBatchReportList_VO[] p_objReportList, out clsLisBatchReportDetail_VO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long l = svc.m_lngGetLisBatchReportDetailByCondition(p_objReportList, out p_objResultArr);
                if (p_objResultArr != null && p_objResultArr.Length > 0)
                {
                    for (int i = 0; i < p_objResultArr.Length; i++)
                    {
                        p_objResultArr[i].m_dtbReportBaseInfo = Function.ReNameDatatable(p_objResultArr[i].m_dtbReportBaseInfo);
                        p_objResultArr[i].m_dtbCheckResult = Function.ReNameDatatable(p_objResultArr[i].m_dtbCheckResult);
                    }
                }
                return l;
            }
        }

        /// <summary>
        /// 根据report_group_id和application_id_chr查询报告单相关信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strReportGroupID">报告组ID</param>
        /// <param name="p_strApplID">申请单ID</param>
        /// <param name="p_blnConfirmed">是否审核</param>
        /// <param name="p_dtbReportInfo">返回报告单相关信息</param>
        /// <returns></returns>
        public long m_lngGetReportInfoByReportGroupIDAndApplicationID(string p_strReportGroupID, string p_strApplID, bool p_blnConfirmed, out DataTable p_dtbReportInfo)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetReportInfoByReportGroupIDAndApplicationID(p_strReportGroupID, p_strApplID, p_blnConfirmed, out p_dtbReportInfo);
                p_dtbReportInfo = Function.ReNameDatatable(p_dtbReportInfo);
                return rec;
            }
        }

        /// <summary>
        /// 根据report_group_id和application_id_chr查询报告的结果记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strApplicationID">申请单ID</param>
        /// <param name="strReportGroupID">报告组ID</param>
        /// <param name="blnConfirmed">是否审核</param>
        /// <param name="dtbCheckResult">返回结果信息</param>
        /// <returns></returns>
        public long m_lngGetCheckResultByReportGroupIDAndApplicationID(string p_strApplicationID, string p_strReportGroupID, bool p_blnConfirmed, out DataTable p_dtbCheckResult)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetCheckResultByReportGroupIDAndApplicationID(p_strApplicationID, p_strReportGroupID, p_blnConfirmed, out p_dtbCheckResult);
                p_dtbCheckResult = Function.ReNameDatatable(p_dtbCheckResult);
                return rec;
            }
        }

        /// <summary>
        /// 根据检验日期和仪器编号查询DeviceResultLog
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckDatFrom"></param>
        /// <param name="p_strCheckDatTo"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetDeviceResultLogByCondition(string p_strCheckDatFrom, string p_strCheckDatTo, string p_strDeviceID, out clsResultLogVO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetDeviceResultLogByCondition(p_strCheckDatFrom, p_strCheckDatTo, p_strDeviceID, out p_objResultArr);
            }
        }

        /// <summary>
        /// 根据申请号得到对应样本的仪器关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_objDRVOArr"></param>
        /// <returns></returns>
        public long m_lngGetDeviceRelationByAppID(string p_strAppID, out clsT_LIS_DeviceRelationVO[] p_objDRVOArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetDeviceRelationByAppID(p_strAppID, out p_objDRVOArr);
            }
        }

        // 根据Imp_Req_int和仪器ID查询标本列表 
        public long m_lngGetDeviceSampleListByCondition(string p_strImpReq, string p_strDeviceID, out clsResultLogVO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetDeviceSampleListByCondition(p_strImpReq, p_strDeviceID, out p_objResultArr);
            }
        }

        // 根据仪器样本号，仪器ID和检验时间查询标本列表 
        public long m_lngGetDeviceSampleListByCondition(string p_strDeviceSampleID, string p_strDeviceID, string p_strCheckDat, out clsResultLogVO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetDeviceSampleListByCondition(p_strDeviceSampleID, p_strDeviceID, p_strCheckDat, out p_objResultArr);
            }
        }

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
        public long m_lngQueryBindAndGetDeviceDataByAppointment(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, out clsDeviceReslutVO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngQueryBindAndGetDeviceDataByAppointment(p_strDeviceID, p_strDeviceSampleID, p_strCheckDate, out p_objResultArr);
            }
        }

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
        public long m_lngQueryBindByAppointment(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, out clsResultLogVO p_objResultLogVO)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngQueryBindByAppointment(p_strDeviceID, p_strDeviceSampleID, p_strCheckDate, out p_objResultLogVO);
            }
        }

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
        public long m_lngGetDeviceData(string p_strDeviceID, int p_intImportReq, out clsDeviceReslutVO[] p_objDeviceResultList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetDeviceData(p_strDeviceID, p_intImportReq, out p_objDeviceResultList);
            }
        }

        /// <summary>
        /// 根据指定的仪器编号,REQ 查询DeviceResultLog  
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
        public long m_lngQueryResultLog(string p_strDeviceID, int p_intImportReq, out clsResultLogVO p_objResultLogVO)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngQueryResultLog(p_strDeviceID, p_intImportReq, out p_objResultLogVO);
            }
        }

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
        public long m_lngGetDeviceData(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, int p_intBeginIndex, int p_intEndIndex, out clsDeviceReslutVO[] p_objDeviceResultList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngGetDeviceData(p_strDeviceID, p_strDeviceSampleID, p_strCheckDate, p_intBeginIndex, p_intEndIndex, out p_objDeviceResultList);
            }
        }

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
        public long m_lngQueryBindAndGetDeviceDataByAutoBind(string p_strDeviceID, out clsDeviceReslutVO[] p_objResultArr)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngQueryBindAndGetDeviceDataByAutoBind(p_strDeviceID, out p_objResultArr);
            }
        }

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
        public long m_lngQueryBindByAutoBind(string p_strDeviceID, out clsResultLogVO p_objResultLogVO)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.m_lngQueryBindByAutoBind(p_strDeviceID, out p_objResultLogVO);
            }
        }

        // 查找指定病人，指定检验项目ID的检验结果
        /// <summary>
        /// 查找指定病人，指定检验项目ID的检验结果
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientID">病人ID</param>
        /// <param name="p_strExcItemID"> 需要特殊处理的项目ID</param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQueryPatientExcItemResult(string p_strPatientID, string p_strExcItemID, out DataTable p_dtResult)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngQueryPatientExcItemResult(p_strPatientID, p_strExcItemID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        // 根据申请单和检验大组查询检验结果
        public long m_lngGetCheckResultByApplFormNoAndCheckGroupID(string strApplFormNo, string strGroupID, out DataTable dtbCheckResult)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetCheckResultByApplFormNoAndCheckGroupID(strApplFormNo, strGroupID, out dtbCheckResult);
                dtbCheckResult = Function.ReNameDatatable(dtbCheckResult);
                return rec;
            }
        }

        // 根据样品号查询表t_opr_lis_check_result的结果信息
        public long m_lngGetManualCheckResultBySampleID(string strSampleID, out DataTable dtbManualCheckResult)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetManualCheckResultBySampleID(strSampleID, out dtbManualCheckResult);
                dtbManualCheckResult = Function.ReNameDatatable(dtbManualCheckResult);
                return rec;
            }
        }

        // 根据申请日期查询手工录入的检验项目的标本资料
        public long m_lngGetSampleListByApplDat(DateTime p_dtBegin, DateTime p_dtEnd, string flag, out System.Data.DataTable p_dtbSampleList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetSampleListByApplDat(p_dtBegin, p_dtEnd, flag, out p_dtbSampleList);
                p_dtbSampleList = Function.ReNameDatatable(p_dtbSampleList);
                return rec;
            }
        }

        // 根据SampleID和GroupID查询手工录入的具体检验项目
        public long m_lngGetCheckItemBySampleIDAndGroupID(string strSampleID, string strGroupID, out System.Data.DataTable dtbManualCheckItemList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetCheckItemBySampleIDAndGroupID(strSampleID, strGroupID, out dtbManualCheckItemList);
                dtbManualCheckItemList = Function.ReNameDatatable(dtbManualCheckItemList);
                return rec;
            }
        }

        /// <summary>
        /// 根据仪器号（不是型号，是一台具体仪器的代号）和检验日期范围，查询出符合条件(该仪器在该段时间内所检验的）标本资料 返回结果必须包含仪器标本号，样品Barcode 和检验日期。
        /// </summary>
        /// <param name="strDeviceId">设备ID</param>
        /// <param name="dtBegin">查询开始时间</param>
        /// <param name="dtEnd">查询结束时间</param>
        /// <param name="dtbDeviceSampleList"></param>
        /// <returns></returns>
        public long m_lngGetDeviceSampleList(string p_strDeviceID, string strBeginDate, string strEndDate, out System.Data.DataTable p_dtbDeviceSampleList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetDeviceSampleList(p_strDeviceID, strBeginDate, strEndDate, out p_dtbDeviceSampleList);
                p_dtbDeviceSampleList = Function.ReNameDatatable(p_dtbDeviceSampleList);
                return rec;
            }
        }

        /// <summary>
        /// 根据仪器号（不是型号，是一台具体仪器的代号）和检验日期范围，查询出符合条件(该仪器在该段时间内所检验的）标本资料 返回结果必须包含仪器标本号，检验的项目（无子组的项目） 和检验日期。
        /// </summary>
        /// <param name="strDeviceId">设备ID</param>
        /// <param name="dtBegin">查询开始时间</param>
        /// <param name="dtEnd">查询结束时间</param>
        /// <param name="intDeviceSampleObj">0-全部标本;1-未审核标本</param>
        /// <param name="dtbDeviceSampleList"></param>
        /// <returns></returns>
        public long m_lngGetDeviceSampleList(string p_strDeviceID, DateTime p_dtBegin, DateTime p_dtEnd, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSampleList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetDeviceSampleList(p_strDeviceID, p_dtBegin, p_dtEnd, p_intDeviceSampleObj, out p_dtbDeviceSampleList);
                p_dtbDeviceSampleList = Function.ReNameDatatable(p_dtbDeviceSampleList);
                return rec;
            }
        }

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
        public long m_lngGetDeviceSampleList(string p_strDeviceID, DateTime p_dtBegin, DateTime p_dtEnd, string p_strCheckGroupID, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSample)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetDeviceSampleList(p_strDeviceID, p_dtBegin, p_dtEnd, p_strCheckGroupID, p_intDeviceSampleObj, out p_dtbDeviceSample);
                p_dtbDeviceSample = Function.ReNameDatatable(p_dtbDeviceSample);
                return rec;
            }
        }

        // 根据仪器号（不是型号，是一台具体仪器的代号）和检验日期范围，再加上仪器标本号范围，查询出符合条件（该仪器在该段时间内所检验的标本范围内的）标本资料。
        public long m_lngGetDeviceSampleList(string p_strDeviceId, DateTime p_dtBegin, DateTime p_dtEnd, string p_strDeviceSampleIDBegin, string p_strDeviceSampleIDEnd, int p_intDeviceSampleObj, out System.Data.DataTable p_dtbDeviceSampleList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetDeviceSampleList(p_strDeviceId, p_dtBegin, p_dtEnd, p_strDeviceSampleIDBegin, p_strDeviceSampleIDEnd, p_intDeviceSampleObj, out p_dtbDeviceSampleList);
                p_dtbDeviceSampleList = Function.ReNameDatatable(p_dtbDeviceSampleList);
                return rec;
            }
        }

        /// <summary>
        /// 根据仪器标本号列表（即一个集合，放在DataTable中）, 查询出该标本的所有检验项目的结果项的代号、名称、值等详细内容。 注意，这里是从t_opr_lis_result表中查询，所得到的结果为仪器输出的结果
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbDeviceSampleList">此结构和上面方法的DataTable结构相同</param>
        /// <param name="p_dtbDeviceResultList"></param>
        /// <returns></returns>
        public long m_lngGetDeviceResultList(System.Data.DataTable p_dtbDeviceSampleList, out System.Data.DataTable p_dtbDeviceResultList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetDeviceResultList(p_dtbDeviceSampleList, out p_dtbDeviceResultList);
                p_dtbDeviceResultList = Function.ReNameDatatable(p_dtbDeviceResultList);
                return rec;
            }
        }

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
        public long m_lngGetResultList(System.Data.DataTable p_dtbDeviceSample, out System.Data.DataTable p_dtbResultList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetResultList(p_dtbDeviceSample, out p_dtbResultList);
                p_dtbResultList = Function.ReNameDatatable(p_dtbResultList);
                return rec;
            }
        }

        // 根据仪器标本号列表（即一个集合，放在DataTable中），查询出该标本的检验项目的已经有的结果详细内容。
        public long m_lngGetCurrentResultList(System.Data.DataTable p_dtbDeviceSampleList, out System.Data.DataTable p_dtbCurrentResults)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetCurrentResultList(p_dtbDeviceSampleList, out p_dtbCurrentResults);
                p_dtbCurrentResults = Function.ReNameDatatable(p_dtbCurrentResults);
                return rec;
            }
        }

        // 根据样本号、检验项目（不含子组），查询对应的检验结果（这里从t_opr_lis_check_result查询）
        public long m_lngGetCheckResult(string p_strSampleID, string p_strCheckGroupID, out System.Data.DataTable p_dtbCheckResult)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetCheckResult(p_strSampleID, p_strCheckGroupID, out p_dtbCheckResult);
                p_dtbCheckResult = Function.ReNameDatatable(p_dtbCheckResult);
                return rec;
            }
        }

        // 查询两个标本的检验结果
        public long m_lngGetUnionCheckResult(string p_strSmapleIDFirst, string p_strGroupIDFirst, string p_strSampleIDSecond, string p_strGroupIDSecond, out System.Data.DataTable p_dtbResult)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetUnionCheckResult(p_strSmapleIDFirst, p_strGroupIDFirst, p_strSampleIDSecond, p_strGroupIDSecond, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 根据检验日期或者审核日期查询检验单号(不得重复);
        public long m_lngGetApplFormNOByDateRange(string p_strDateFieldName, System.DateTime p_dtBegin, System.DateTime p_dtEnd, out System.Data.DataTable p_dtbAppl)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetApplFormNOByDateRange(p_strDateFieldName, p_dtBegin, p_dtEnd, out p_dtbAppl);
                p_dtbAppl = Function.ReNameDatatable(p_dtbAppl);
                return rec;
            }
        }

        // 根据SampleID,GroupID（基本检验组）查询结果项,这里要查询两个表，一个是t_opr_lis_check_result,一个是t_aid_lis_check_group_detail，打印时按打印类别，打印顺序打印。
        public long m_lngGetPrintResult(string p_strGroupID, string p_strSmapleID, out System.Data.DataTable p_dtbPrintResult)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetPrintResult(p_strGroupID, p_strSmapleID, out p_dtbPrintResult);
                p_dtbPrintResult = Function.ReNameDatatable(p_dtbPrintResult);
                return rec;
            }
        }

        //  根据仪器标本号, 仪器号，检验日期查询出该标本的所有结果项的代号、名称、值等详细内容。
        public long m_lngGetDeviceSampleResultList(string strDeviceID, string strDeviceSampleID, string strCheckDate, out System.Data.DataTable dtbSampleResultList)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                long rec = svc.m_lngGetDeviceSampleResultList(strDeviceID, strDeviceSampleID, strCheckDate, out dtbSampleResultList);
                dtbSampleResultList = Function.ReNameDatatable(dtbSampleResultList);
                return rec;
            }
        }

        /// <summary>
        /// 获取签名图片
        /// </summary>
        /// <param name="empName"></param>
        /// <returns></returns>
        public byte[] GetEmpSign(string empName)
        {
            using (clsQueryCheckResultSvc svc = new clsQueryCheckResultSvc())
            {
                return svc.GetEmpSign(empName);
            }
        }

        #endregion

        #region clsQueryInputGroupSvc

        // 获取筛选后的项目列表
        public long m_lngGetFiltedItems(string[] p_strApplyUnitIDArr, string[] p_strInputGroupIDArr, out string[] p_strItemResultArr)
        {
            using (clsQueryInputGroupSvc svc = new clsQueryInputGroupSvc())
            {
                return svc.m_lngGetFiltedItems(p_strApplyUnitIDArr, p_strInputGroupIDArr, out p_strItemResultArr);
            }
        }

        // 获取指定申请单元及其名称列表
        public long m_lngGetApplyUnitInfo(string[] p_strApplyUnitIDArr, out DataTable p_dtbResult)
        {
            using (clsQueryInputGroupSvc svc = new clsQueryInputGroupSvc())
            {
                long rec = svc.m_lngGetApplyUnitInfo(p_strApplyUnitIDArr, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 获取指定申请单元下可用的录入组合
        public long m_lngGetInputGroupsByUnit(string[] p_strApplyUnitIDArr, out DataTable p_dtbResult)
        {
            using (clsQueryInputGroupSvc svc = new clsQueryInputGroupSvc())
            {
                long rec = svc.m_lngGetInputGroupsByUnit(p_strApplyUnitIDArr, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        // 获取检验分类-申请单元-录入组合的的联合信息
        public long m_lngGetUnitedInputGroupInfo(out clsInputGroupUnited_VO[] p_objResults)
        {
            using (clsQueryInputGroupSvc svc = new clsQueryInputGroupSvc())
            {
                return svc.m_lngGetUnitedInputGroupInfo(out p_objResults);
            }
        }

        // 获取申请单元项目明细
        public long m_lngGetApplyUnitItems(string p_strApplyUnitID, out clsCheckItemSimple_VO[] p_objResults)
        {
            using (clsQueryInputGroupSvc svc = new clsQueryInputGroupSvc())
            {
                return svc.m_lngGetApplyUnitItems(p_strApplyUnitID, out p_objResults);
            }
        }

        // 获取录入组合及明细
        public long m_lngGetInputGroupInfo(string p_strInputGroupID, out clsInputGroupBaseInfo_VO p_objBaseInfo, out clsInputGroupDetail_VO[] p_objResults)
        {
            using (clsQueryInputGroupSvc svc = new clsQueryInputGroupSvc())
            {
                return svc.m_lngGetInputGroupInfo(p_strInputGroupID, out p_objBaseInfo, out p_objResults);
            }
        }

        #endregion

        #region clsApplicationMainSvc

        public long m_lngGetApplicationValid(string applicationId, out bool isValid)
        {
            using (clsApplicationMainSvc svc = new clsApplicationMainSvc())
            {
                return svc.m_lngGetApplicationValid(applicationId, out isValid);
            }
        }

        public long m_lngGetApplication(string orderId, out List<clsLisApplMainVO> lstAppMain)
        {
            using (clsApplicationMainSvc svc = new clsApplicationMainSvc())
            {
                return svc.m_lngGetApplication(orderId, out lstAppMain);
            }
        }

        public long m_lngDeleteApplication(string applicationId)
        {
            using (clsApplicationMainSvc svc = new clsApplicationMainSvc())
            {
                return svc.m_lngDeleteApplication(applicationId);
            }
        }

        public long m_lngAddNewAppInfo(clsLisApplMainVO applMain, out clsLisApplMainVO applMainOut, bool isSend,
                                            clsT_OPR_LIS_APP_REPORT_VO[] arrReports, clsT_OPR_LIS_APP_SAMPLE_VO[] arrSamples,
                                            clsT_OPR_LIS_APP_CHECK_ITEM_VO[] arrCheckItems, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] arrApplyUnits,
                                            clsLisAppUnitItemVO[] arrUnitItems)
        {
            using (clsApplicationMainSvc svc = new clsApplicationMainSvc())
            {
                return svc.m_lngAddNewAppInfo(applMain, out applMainOut, isSend, arrReports, arrSamples, arrCheckItems, arrApplyUnits, arrUnitItems);
            }
        }
        /// <summary>
        /// 设置打印状态
        /// </summary>
        /// <param name="arrApplicationId"></param>
        /// <param name="isPrinted"></param>
        /// <returns></returns>
        public long m_lngSetApplPrintedStatus(string[] arrApplicationId, bool isPrinted)
        {
            using (clsApplicationMainSvc svc = new clsApplicationMainSvc())
            {
                return svc.m_lngSetApplPrintedStatus(arrApplicationId, isPrinted);
            }
        }

        /// <summary>
        /// 根据医嘱ID获得检验申请单主要信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="applMain"></param>
        /// <returns></returns>
        public long m_lngGetApplVO(string orderId, out clsLisApplMainVO applMain)
        {
            using (clsApplicationMainSvc svc = new clsApplicationMainSvc())
            {
                return svc.m_lngGetApplVO(orderId, out applMain);
            }
        }

        #endregion

        #region clsChargeInfoStatusSvc

        /// <summary>
        /// 查找申请单的收费状态
        /// </summary>
        /// <param name="applicationId">申请单Id</param>
        /// <param name="chargeStatusVO">收费状态VO</param>
        /// <returns></returns>
        public long m_lngFind(string applicationId, out clsChargeStatusVO chargeStatusVO)
        {
            using (clsChargeInfoStatusSvc svc = new clsChargeInfoStatusSvc())
            {
                return svc.m_lngFind(applicationId, out chargeStatusVO);
            }
        }

        #endregion

        #region clsApplicationBizSvc

        // 增加-组新的检验申请信息(包含报告组,申请单元,样本组,检验项目在内的全部信息); 
        public long m_lngAddNewApplication(clsLisApplMainVO p_objLisApplMainVO, out clsLisApplMainVO p_objLisApplMainOutVO,
                                           clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr,
                                           clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr,
                                           clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            using (clsApplicationBizSvc svc = new clsApplicationBizSvc())
            {
                return svc.m_lngAddNewApplication(p_objLisApplMainVO, out p_objLisApplMainOutVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            }
        }

        // 修改-组检验申请信息(包含报告组,申请单元,样本组,检验项目在内的全部信息);
        public long m_lngModifyAppInfo(clsLisApplMainVO p_objLisApplMainVO, clsT_OPR_LIS_APP_REPORT_VO[] p_objReportArr, clsT_OPR_LIS_APP_SAMPLE_VO[] p_objAppSampleArr, clsT_OPR_LIS_APP_CHECK_ITEM_VO[] p_objAppItemArr, clsT_OPR_LIS_APP_APPLY_UNIT_VO[] p_objAppUnitArr, clsLisAppUnitItemVO[] p_objAppUnitItemArr)
        {
            using (clsApplicationBizSvc svc = new clsApplicationBizSvc())
            {
                return svc.m_lngModifyAppInfo(p_objLisApplMainVO, p_objReportArr, p_objAppSampleArr, p_objAppItemArr, p_objAppUnitArr, p_objAppUnitItemArr);
            }
        }

        /// <summary>
        /// 为表 T_OPR_LIS_APP_APPLY_UNIT 新增记录时用 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        public long m_lngAddNewAppApplyUint(clsT_OPR_LIS_APP_APPLY_UNIT_VO[] arrRecodes)
        {
            using (clsApplicationBizSvc svc = new clsApplicationBizSvc())
            {
                return svc.m_lngAddNewAppApplyUint(arrRecodes);
            }
        }

        #endregion

        #region clsLisInterfaceSvc

        public long CreateApplicationArray(clsLisApplyAppliationInfo[] arrApplication)
        {
            using (clsLisInterfaceSvc svc = new clsLisInterfaceSvc())
            {
                return svc.CreateApplicationArray(arrApplication);
            }
        }

        public long CreateSendApplicationArray(clsLisApplyAppliationInfo[] arrApplication, bool isSended)
        {
            using (clsLisInterfaceSvc svc = new clsLisInterfaceSvc())
            {
                return svc.CreateSendApplicationArray(arrApplication, isSended);
            }
        }

        #endregion

        #region clsLisSearchSvc

        // 住院部分不关联收费信息
        public long m_lngGetAppAndSampleInfo(int sampleStatus, string areaId,
                                             string beginDate, string endDate, string patientName,
                                             string patientCardId, string hosipitalNo, string bedNo, int p_intSampleBack, out clsLisApplMainVO[] p_objAppVOArr)
        {
            using (clsLisSearchSvc svc = new clsLisSearchSvc())
            {
                return svc.m_lngGetAppAndSampleInfo(sampleStatus, areaId, beginDate, endDate, patientName, patientCardId, hosipitalNo, bedNo, p_intSampleBack, out p_objAppVOArr);
            }
        }

        /// <summary>
        /// 构建 clsLisApplMainVO 
        /// </summary>
        /// <param name="p_objRow"></param>
        /// <returns></returns>
        public clsLisApplMainVO m_objConstructAppMainVO(DataTable dtRow)
        {
            using (clsLisSearchSvc svc = new clsLisSearchSvc())
            {
                return svc.m_objConstructAppMainVO(dtRow.Rows[0]);
            }
        }

        #endregion

        #region clsCPReportPrintQuery_Svc

        public long m_lngGetLisApplicationIDByPatientID(string p_strPatienID, out DataTable dtRes)
        {
            using (iCare.Lis.Self.CPReportPrint.svc.clsCPReportPrintQuery_Svc svc = new iCare.Lis.Self.CPReportPrint.svc.clsCPReportPrintQuery_Svc())
            {
                long rec = svc.m_lngGetLisApplicationIDByPatientID(p_strPatienID, out dtRes);
                dtRes = Function.ReNameDatatable(dtRes);
                return rec;
            }
        }

        public long m_lngGetLisApplicationIDByPatientCardID(string p_strPatienCardID, out DataTable dtRes)
        {
            using (iCare.Lis.Self.CPReportPrint.svc.clsCPReportPrintQuery_Svc svc = new iCare.Lis.Self.CPReportPrint.svc.clsCPReportPrintQuery_Svc())
            {
                long rec = svc.m_lngGetLisApplicationIDByPatientCardID(p_strPatienCardID, out dtRes);
                dtRes = Function.ReNameDatatable(dtRes);
                return rec;
            }
        }

        public long m_lngGetLisLisApplicationIDBySBCardID(string p_strSBCardID, int p_intSBType, out DataTable dtRes)
        {
            using (iCare.Lis.Self.CPReportPrint.svc.clsCPReportPrintQuery_Svc svc = new iCare.Lis.Self.CPReportPrint.svc.clsCPReportPrintQuery_Svc())
            {
                long rec = svc.m_lngGetLisLisApplicationIDBySBCardID(p_strSBCardID, p_intSBType, out dtRes);
                dtRes = Function.ReNameDatatable(dtRes);
                return rec;
            }
        }

        public long m_lngGetReportPrintInfo(string PatientID, out clsLis_ReportPrint_VO p_objReportPrint)
        {
            using (iCare.Lis.Self.CPReportPrint.svc.clsCPReportPrintQuery_Svc svc = new iCare.Lis.Self.CPReportPrint.svc.clsCPReportPrintQuery_Svc())
            {
                return svc.m_lngGetReportPrintInfo(PatientID, out p_objReportPrint);
            }
        }

        #endregion

        #region clsReportPrintUpdate_Svc

        public long m_mthWriteReportPrintState(string m_strAPPLICATION_ID)
        {
            using (iCare.Lis.Self.ReportPrint.Svc.clsReportPrintUpdate_Svc svc = new iCare.Lis.Self.ReportPrint.Svc.clsReportPrintUpdate_Svc())
            {
                return svc.m_mthWriteReportPrintState(m_strAPPLICATION_ID);
            }
        }

        #endregion

        #region clsLIS_DataAcquisitionServ

        public long clsLIS_DataAcquisitionServ_lngAddLabResult(List<clsLIS_Device_Test_ResultVO> p_arlResult, out List<clsLIS_Device_Test_ResultVO> p_arlResultOut)
        {
            using (clsLIS_DataAcquisitionServ svc = new clsLIS_DataAcquisitionServ())
            {
                return svc.lngAddLabResult(p_arlResult, out p_arlResultOut);
            }
        }

        public long clsLIS_DataAcquisitionServ_lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            using (clsLIS_DataAcquisitionServ svc = new clsLIS_DataAcquisitionServ())
            {
                return svc.lngAddLabResult(p_objResultArr, out p_objOutResultArr);
            }
        }

        public long clsLIS_DataAcquisitionServ_lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            using (clsLIS_DataAcquisitionServ svc = new clsLIS_DataAcquisitionServ())
            {
                return svc.lngAddLabResult(p_objResultArr, p_blnMuiltySample, out p_objOutResultArr);
            }
        }

        public long m_lngUpdateAppCheckNO(string p_strBarCode, string p_strDeviceID, string p_strDeviceNO, string p_strDeviceSampleID)
        {
            using (clsLIS_DataAcquisitionServ svc = new clsLIS_DataAcquisitionServ())
            {
                return svc.m_lngUpdateAppCheckNO(p_strBarCode, p_strDeviceID, p_strDeviceNO, p_strDeviceSampleID);
            }
        }

        public long m_lngUpdateAppCheckSampleNO(string p_strBarCode, string p_strDeviceID, string p_strDeviceNO, string p_strDeviceSampleID)
        {
            using (clsLIS_DataAcquisitionServ svc = new clsLIS_DataAcquisitionServ())
            {
                return svc.m_lngUpdateAppCheckSampleNO(p_strBarCode, p_strDeviceID, p_strDeviceNO, p_strDeviceSampleID);
            }
        }

        public long m_lnginsertResultExe(int p_intFlag, DataTable p_dt)
        {
            using (clsLIS_DataAcquisitionServ svc = new clsLIS_DataAcquisitionServ())
            {
                return svc.m_lnginsertResultExe(p_intFlag, p_dt);
            }
        }

        public long m_lnginsertResultMic(int p_intFlag, DataTable p_dt)
        {
            using (clsLIS_DataAcquisitionServ svc = new clsLIS_DataAcquisitionServ())
            {
                return svc.m_lnginsertResultMic(p_intFlag, p_dt);
            }
        }

        public long m_lnginsertResultbill(int p_intFlag, DataTable p_dt)
        {
            using (clsLIS_DataAcquisitionServ svc = new clsLIS_DataAcquisitionServ())
            {
                return svc.m_lnginsertResultbill(p_intFlag, p_dt);
            }
        }

        #endregion

        #region clsLIS_QueryDataAcquisitionServ

        public bool clsLIS_QueryDataAcquisitionServ_m_blnIsAppendResult(ref Dictionary<string, string> has, clsLIS_Device_Test_ResultVO[] p_objResultArr, out string[] strConditionList)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_blnIsAppendResult(ref has, p_objResultArr, out strConditionList);
            }
        }

        public long lngGetInstrumentSerialSetting(string strData_Acquisition_Computer_IP, out clsLIS_Equip_Base[] objConfig_List)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.lngGetInstrumentSerialSetting(strData_Acquisition_Computer_IP, out objConfig_List);
            }
        }

        public long m_lngGetDeviceNOConvertInfo(string p_strDeviceID, out clsDeviceItemNameItemNO[] p_objDeviceItemNameNOArr, out clsDeviceItemValueConvert_VO[] p_objItemConvertArr)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngGetDeviceNOConvertInfo(p_strDeviceID, out p_objDeviceItemNameNOArr, out p_objItemConvertArr);
            }
        }

        public long m_lngQueryDeviceItemNO(string p_strDeviceID, out clsDeviceItemNameItemNO[] p_objDeviceItemNameNOArr)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngQueryDeviceItemNO(p_strDeviceID, out p_objDeviceItemNameNOArr);
            }
        }

        public long m_lngQueryDeviceItemValueConvert(string p_strDeviceID, out clsDeviceItemValueConvert_VO[] p_objItemConvertArr)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngQueryDeviceItemValueConvert(p_strDeviceID, out p_objItemConvertArr);
            }
        }

        public long clsLIS_QueryDataAcquisitionServ_m_lngGetDeviceData(string p_strDeviceID, string p_strDeviceSampleID, string p_strCheckDate, int p_intBeginIndex, int p_intEndIndex, out clsDeviceReslutVO[] p_objDeviceResultList)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngGetDeviceData(p_strDeviceID, p_strDeviceSampleID, p_strCheckDate, p_intBeginIndex, p_intEndIndex, out p_objDeviceResultList);
            }
        }

        public long m_lngQueryAppInfoAndDeviceNO(string p_strDeviceID, string p_strBarCode, out clsLisApplMainVO p_objAppMainVO, out string[] p_strDeviceNOArr)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngQueryAppInfoAndDeviceNO(p_strDeviceID, p_strBarCode, out p_objAppMainVO, out p_strDeviceNOArr);
            }
        }

        public long m_lngQueryAppDeviceNOByBarCode(string p_strDeviceID, string p_strBarCode, out string[] p_strDeviceNOArr)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngQueryAppDeviceNOByBarCode(p_strDeviceID, p_strBarCode, out p_strDeviceNOArr);
            }
        }

        public long m_lngGetAppInfoByBarCode(string p_strBarCode, out clsLisApplMainVO p_objAppMainVO)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngGetAppInfoByBarCode(p_strBarCode, out p_objAppMainVO);
            }
        }

        public long m_lngQueryPatientInfo(string p_strBarCode_vchr, out clsT_OPR_LIS_SAMPLE_VO p_objSampleVO)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngQueryPatientInfo(p_strBarCode_vchr, out p_objSampleVO);
            }
        }

        public long clsLIS_QueryDataAcquisitionServ_m_lngQueryPatientInfo(string p_strBarCode_vchr, out clsT_OPR_LIS_SAMPLE_VO p_objSampleVO)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngQueryPatientInfo(p_strBarCode_vchr, out p_objSampleVO);
            }
        }

        public long m_lngGetCheckNOByBarcode(string p_strBarcode, out string p_strCheckNO)
        {
            using (clsLIS_QueryDataAcquisitionServ svc = new clsLIS_QueryDataAcquisitionServ())
            {
                return svc.m_lngGetCheckNOByBarcode(p_strBarcode, out p_strCheckNO);
            }
        }

        #endregion

        #region clsLis_ReportPrinterServ

        public long m_lngGetLisReportPrintInfo(string p_strPatientCard, string p_strMinTime, out clsLis_ReportPrint_VO p_objReportPrint, out string p_strPatientName)
        {
            using (com.digitalwave.iCare.middletier.LIS.LIS_ReportPrinterServ.clsLis_ReportPrinterServ svc = new com.digitalwave.iCare.middletier.LIS.LIS_ReportPrinterServ.clsLis_ReportPrinterServ())
            {
                return svc.m_lngGetLisReportPrintInfo(p_strPatientCard, p_strMinTime, out p_objReportPrint, out p_strPatientName);
            }
        }

        public long clsLis_ReportPrinterServ_m_mthWriteReportPrintState(string m_strAPPLICATION_ID)
        {
            using (com.digitalwave.iCare.middletier.LIS.LIS_ReportPrinterServ.clsLis_ReportPrinterServ svc = new com.digitalwave.iCare.middletier.LIS.LIS_ReportPrinterServ.clsLis_ReportPrinterServ())
            {
                return svc.m_mthWriteReportPrintState(m_strAPPLICATION_ID);
            }
        }

        public long m_lngGetPatientCardByIDCard(string p_strIDCard, ref string p_strPatientCard)
        {
            using (com.digitalwave.iCare.middletier.LIS.LIS_ReportPrinterServ.clsLis_ReportPrinterServ svc = new com.digitalwave.iCare.middletier.LIS.LIS_ReportPrinterServ.clsLis_ReportPrinterServ())
            {
                return svc.m_lngGetPatientCardByIDCard(p_strIDCard, ref p_strPatientCard);
            }
        }

        #endregion

        #region clsMicReportSvc

        public long lngGetAllAnti(out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetAllAnti(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetFuzzyQueryAnti(string micName, bool IsEnglish, out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetFuzzyQueryAnti(micName, IsEnglish, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetAllMic(out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetAllMic(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetFuzzyQueryMic(string micName, bool IsEnglish, out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetFuzzyQueryMic(micName, IsEnglish, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetBacteriaDistribution(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetBacteriaDistribution(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetMicSensitive(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetMicSensitive(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, strTempAntiID, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetMicdistributionTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetMicdistributionTend(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetSensitiveTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetSensitiveTend(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, strTempAntiID, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetMicCumulative(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetMicCumulative(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, strTempAntiID, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetSensitiveRate(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string SamtNo, string DisNo, string Sex, string AgeFrom, string AgeTo, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetSensitiveRate(micname, p_dtDateFrom, p_dtDateTO, SamtNo, DisNo, Sex, AgeFrom, AgeTo, TestMethod, strTempAntiID, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetDeptName(out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetDeptName(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetSamType(out DataTable dtbResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMicReportSvc svc = new com.digitalwave.iCare.middletier.LIS.clsMicReportSvc())
            {
                long rec = svc.lngGetSamType(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        #endregion

        #region clsST360Svc

        public long m_lngFindSTGroupResult(out List<clsDeviceReslutVO> lstResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsST360Svc svc = new com.digitalwave.iCare.middletier.LIS.clsST360Svc())
            {
                return svc.m_lngFindSTGroupResult(out lstResult);
            }
        }

        public long m_lngUpdateDeviceResult(clsDeviceReslutVO deviceResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsST360Svc svc = new com.digitalwave.iCare.middletier.LIS.clsST360Svc())
            {
                return svc.m_lngUpdateDeviceResult(deviceResult);
            }
        }

        #endregion

        #region clsST360CheckResultSvc

        public long m_lngInsert(clsST360CheckResultVO m_objResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsST360CheckResultSvc svc = new com.digitalwave.iCare.middletier.LIS.clsST360CheckResultSvc())
            {
                return svc.m_lngInsert(m_objResult);
            }
        }

        public long m_lngFindBoardName(out string[] arrBoardName, DateTime begin, DateTime end)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsST360CheckResultSvc svc = new com.digitalwave.iCare.middletier.LIS.clsST360CheckResultSvc())
            {
                return svc.m_lngFindBoardName(out arrBoardName, begin, end);
            }
        }

        public long m_lngFindBoardName(out string[] arrBoardName)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsST360CheckResultSvc svc = new com.digitalwave.iCare.middletier.LIS.clsST360CheckResultSvc())
            {
                return svc.m_lngFindBoardName(out arrBoardName);
            }
        }

        public long m_lngFind(string boardNo, out clsST360CheckResultVO[] arrCheckResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsST360CheckResultSvc svc = new com.digitalwave.iCare.middletier.LIS.clsST360CheckResultSvc())
            {
                return svc.m_lngFind(boardNo, out arrCheckResult);
            }
        }

        #endregion

        #region clsItemSetSvc

        public long m_lngGetAllCheckItem(string p_strDeviceModelID, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                long rec = svc.m_lngGetAllCheckItem(p_strDeviceModelID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngGetAllCheckItemCustomInfo(out clsLisCheckItemCustom[] p_objCheckItemCustomVO, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                long rec = svc.m_lngGetAllCheckItemCustomInfo(out p_objCheckItemCustomVO, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngUpdateCheckItemCustom(clsLisCheckItemCustom p_objCheckItemCustomVO)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngUpdateCheckItemCustom(p_objCheckItemCustomVO);
            }
        }

        public long m_lngDelteCheckItemCustom(string p_strCheckItemID)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngDelteCheckItemCustom(p_strCheckItemID);
            }
        }

        public long m_lngInsertCheckItemCustom(clsLisCheckItemCustom p_objCheckItemCustomVO)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngInsertCheckItemCustom(p_objCheckItemCustomVO);
            }
        }

        public long m_lngQueryCheckItemCustomRes(string p_strCheckItemID, out clsLisCheckItemCustomRes[] p_objCheckItemCustomRes)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngQueryCheckItemCustomRes(p_strCheckItemID, out p_objCheckItemCustomRes);
            }
        }

        public long m_lngInsertCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngInsertCheckItemCustomRes(p_objCheckItemCustomResVO);
            }
        }

        public long m_lngUpdateCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngUpdateCheckItemCustomRes(p_objCheckItemCustomResVO);
            }
        }

        public long m_lngDeleteCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngDeleteCheckItemCustomRes(p_objCheckItemCustomResVO);
            }
        }

        public long lngAddLabResult_clsItemSetSvc(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.lngAddLabResult(p_objResultArr, p_blnMuiltySample, out p_objOutResultArr);
            }
        }

        public long lngAddLabResult_clsItemSetSvc(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.lngAddLabResult(p_objResultArr, out p_objOutResultArr);
            }
        }

        public int m_mthGetNewResultIndex_clsItemSetSvc(int p_intRowNum, bool p_blnNext)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_mthGetNewResultIndex(p_intRowNum, p_blnNext);
            }
        }

        public bool m_blnIsAppendResult_clsItemSetSvc(ref Dictionary<string, string> has, clsLIS_Device_Test_ResultVO[] p_objResultArr, out string[] strConditionList)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_blnIsAppendResult(ref has, p_objResultArr, out strConditionList);
            }
        }

        public long m_lngQueryDevceCheckItem(string p_strDevicModelID, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                long rec = svc.m_lngQueryDevceCheckItem(p_strDevicModelID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngGetAllItemLayout(out DataTable p_dtResult, out DataTable p_dtLayoutInfo)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                long rec = svc.m_lngGetAllItemLayout(out p_dtResult, out p_dtLayoutInfo);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                p_dtLayoutInfo = Function.ReNameDatatable(p_dtLayoutInfo);
                return rec;
            }
        }

        public long m_lngAddItemLayout(clslisItemLayout[] p_objLisItemLayoutVO)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngAddItemLayout(p_objLisItemLayoutVO);
            }
        }

        public long m_lngDeleteItemLayout(string p_strLayoutName)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngDeleteItemLayout(p_strLayoutName);
            }
        }

        public long m_lngQueryPlateName(string p_strPlateName, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                long rec = svc.m_lngQueryPlateName(p_strPlateName, p_strStartDate, p_strEndDate, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngQueryPlateResult(string p_strPlateNameID, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                long rec = svc.m_lngQueryPlateResult(p_strPlateNameID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngInsertPlateResult(string p_strPlateName, string p_strPlateChName, clslisPlateResult[] p_objPlateResultArr, out string p_strPlateResultID)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngInsertPlateResult(p_strPlateName, p_strPlateChName, p_objPlateResultArr, out p_strPlateResultID);
            }
        }

        public long m_lngGetSequenceArr_clsItemSetSvc(string p_strSeqName, int p_intNumber, out int[] p_intSeqIdArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngGetSequenceArr(p_strSeqName, p_intNumber, out p_intSeqIdArr);
            }
        }

        public long m_lngDeletePlateResult(string p_strPlateNameID)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngDeletePlateResult(p_strPlateNameID);
            }
        }

        public long m_lngGetAllPlateResult(out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                long rec = svc.m_lngGetAllPlateResult(out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngInsertDeviceResult(clsLIS_Device_Test_ResultVO[] p_objResultArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                return svc.m_lngInsertDeviceResult(p_objResultArr);
            }
        }

        public long m_lngQueryCheckItemCustomRes(string p_strCheckItemID, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsItemSetSvc svc = new com.digitalwave.iCare.middletier.LIS.clsItemSetSvc())
            {
                long rec = svc.m_lngQueryCheckItemCustomRes(p_strCheckItemID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        #endregion

        #region clsMK3DeviceCommunications

        public long m_lngQueryChcekItemCustomOrder(string p_strCheckItemID, out clsLisCheckItemCustomOrder p_objCheckItemCustomOrder)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications svc = new com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications())
            {
                return svc.m_lngQueryChcekItemCustomOrder(p_strCheckItemID, out p_objCheckItemCustomOrder);
            }
        }

        public long m_lngUpdateChcekItemCustomOrder(clsLisCheckItemCustomOrder p_objCheckItemCustomOrderVO)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications svc = new com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications())
            {
                return svc.m_lngUpdateChcekItemCustomOrder(p_objCheckItemCustomOrderVO);
            }
        }

        public long m_lngInsertCheckItemCustomOrder(clsLisCheckItemCustomOrder p_objCheckItemCustomOrderVO)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications svc = new com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications())
            {
                return svc.m_lngInsertCheckItemCustomOrder(p_objCheckItemCustomOrderVO);
            }
        }

        public long m_lngDeleteCheckItemCustomOrder(string p_strCheckItemID)
        {
            using (com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications svc = new com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications())
            {
                return svc.m_lngDeleteCheckItemCustomOrder(p_strCheckItemID);
            }
        }


        #endregion

        #region clsQcNew

        public long m_lngQueryDeviceInfo(string p_strComputerName, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                long rec = svc.m_lngQueryDeviceInfo(p_strComputerName, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngQueryQCDeviceResult(int p_strQCId, out List<double> lstDbl)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngQueryQCDeviceResult(p_strQCId, out lstDbl);
            }
        }

        public long m_lngQueryQCInfo(int p_intQCID, out List<clsLisQCConcentrationVO> p_lstQCConTemp)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngQueryQCInfo(p_intQCID, out p_lstQCConTemp);
            }
        }

        public long m_lngGetAllCheckItemInfo(out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                long rec = svc.m_lngGetAllCheckItemInfo(out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngQueryItemQCResult(clsLisQCBatchSchVO objSch, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                long rec = svc.m_lngQueryItemQCResult(objSch, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngQCCheckItem(string p_strDeviceId, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                long rec = svc.m_lngQCCheckItem(p_strDeviceId, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngQueryQCLot(clsLisQCBatchSchVO objSch, out DataTable p_dtResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                long rec = svc.m_lngQueryQCLot(objSch, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngQueryResult(clsLisQCBatchSchVO objSch, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult, out DataTable p_dtQcResult)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                long rec = svc.m_lngQueryResult(objSch, p_strStartDate, p_strEndDate, out p_dtResult, out p_dtQcResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                p_dtQcResult = Function.ReNameDatatable(p_dtQcResult);
                return rec;
            }
        }

        public long m_lngUpdateConcentration(clsLisConcentrationVO QCBatch)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngUpdateConcentration(QCBatch);
            }
        }

        public long m_lngInsertCheckMethod(clsLisCheckMethodVO p_objCheckMethod, out int p_intSeq)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngInsertCheckMethod(p_objCheckMethod, out p_intSeq);
            }
        }

        public long m_lngUpdateCheckMethod(clsLisCheckMethodVO p_objCheckMethod)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngUpdateCheckMethod(p_objCheckMethod);
            }
        }

        public long m_lngInsertConcentration(clsLisConcentrationVO p_objConcentration, out int p_intSeq)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngInsertConcentration(p_objConcentration, out p_intSeq);
            }
        }

        public long m_lngDeleteCheckMethod(int p_intSeq)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngDeleteCheckMethod(p_intSeq);
            }
        }

        public long m_lngInsertVendor(clsLisVendorVO p_objVendor, out int p_intSeq)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngInsertVendor(p_objVendor, out p_intSeq);
            }
        }

        public long m_lngUpdateVendor(clsLisVendorVO p_objVendor)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngUpdateVendor(p_objVendor);
            }
        }

        public long m_lngUpdateWorkGruop(clsLisWorkGroupVO p_objWorkGroup)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngUpdateWorkGruop(p_objWorkGroup);
            }
        }

        public long m_lngInsertWorkGroup(clsLisWorkGroupVO p_objWorkGroup, out int p_intSeq)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngInsertWorkGroup(p_objWorkGroup, out p_intSeq);
            }
        }

        public long m_lngFindCheckMethod(out clsLisCheckMethodVO[] p_objResultArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngFindCheckMethod(out p_objResultArr);
            }
        }

        public long m_lngFindConcentration(out clsLisConcentrationVO[] p_objResultArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngFindConcentration(out p_objResultArr);
            }
        }

        public long m_lngFindVendor(out clsLisVendorVO[] p_objResultArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngFindVendor(out p_objResultArr);
            }
        }

        public long m_lngFindWorkGroup(out clsLisWorkGroupVO[] p_objResultArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngFindWorkGroup(out p_objResultArr);
            }
        }

        public long m_lngInsertQCConcentration(clsLisQCConcentrationVO p_objQCConcentration)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngInsertQCConcentration(p_objQCConcentration);
            }
        }

        public long m_lngUpdateQCConcentration(clsLisQCConcentrationVO p_objQCConcentration)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngUpdateQCConcentration(p_objQCConcentration);
            }
        }

        public long m_lngFindDelQCConcentration(int p_intQCBatchSeq, out clsLisQCConcentrationVO[] p_objResultArr)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngFindDelQCConcentration(p_intQCBatchSeq, out p_objResultArr);
            }
        }

        public long m_lngDeleteQCRule(int p_intSeq)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngDeleteQCRule(p_intSeq);
            }
        }

        public long m_lngInsertQCRule(clsLisQCRuleVO p_objRule, out int p_intSeq)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngInsertQCRule(p_objRule, out p_intSeq);
            }
        }

        public long m_lngUpdateQCRule(clsLisQCRuleVO p_objRule)
        {
            using (com.digitalwave.iCare.middletier.LIS.bizNewQC svc = new com.digitalwave.iCare.middletier.LIS.bizNewQC())
            {
                return svc.m_lngUpdateQCRule(p_objRule);
            }
        }

        #endregion

        #region bizLisReport

        public long m_lngGetLISCheck_Category(out DataTable p_dtCategory, bool isCS)
        {
            if (isCS)
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport_cs svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport_cs())
                {
                    long rec = svc.m_lngGetLISCheck_Category(out p_dtCategory);
                    p_dtCategory = Function.ReNameDatatable(p_dtCategory);
                    return rec;
                }
            }
            else
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport())
                {
                    long rec = svc.m_lngGetLISCheck_Category(out p_dtCategory);
                    p_dtCategory = Function.ReNameDatatable(p_dtCategory);
                    return rec;
                }
            }
        }

        public long m_lngQueryWorkLoadByAppDept(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS)
        {
            if (isCS)
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport_cs svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport_cs())
                {
                    long rec = svc.m_lngQueryWorkLoadByAppDept(p_strCatoryID, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
            else
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport())
                {
                    long rec = svc.m_lngQueryWorkLoadByAppDept(p_strCatoryID, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
        }

        public long m_lngQueryWorkLoadByAppDoctor(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS)
        {
            if (isCS)
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport_cs svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport_cs())
                {
                    long rec = svc.m_lngQueryWorkLoadByAppDoctor(p_strCatory, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
            else
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport())
                {
                    long rec = svc.m_lngQueryWorkLoadByAppDoctor(p_strCatory, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
        }

        public long m_lngQueryWorkLoadByCheckItem(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS)
        {
            if (isCS)
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport_cs svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport_cs())
                {
                    long rec = svc.m_lngQueryWorkLoadByCheckItem(p_strCatoryID, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
            else
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport())
                {
                    long rec = svc.m_lngQueryWorkLoadByCheckItem(p_strCatoryID, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
        }

        public long m_lngQueryWorkLoadByCheckResult(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS)
        {
            if (isCS)
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport_cs svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport_cs())
                {
                    long rec = svc.m_lngQueryWorkLoadByCheckResult(p_strCatory, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
            else
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport())
                {
                    long rec = svc.m_lngQueryWorkLoadByCheckResult(p_strCatory, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
        }

        public long m_lngQueryWorkLoadByCommitorID(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS)
        {
            if (isCS)
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport_cs svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport_cs())
                {
                    long rec = svc.m_lngQueryWorkLoadByCommitorID(p_strCatoryID, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
            else
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport())
                {
                    long rec = svc.m_lngQueryWorkLoadByCommitorID(p_strCatoryID, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
        }

        public long m_lngQueryWorkLoadByDevice(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult, bool isCS)
        {
            if (isCS)
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport_cs svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport_cs())
                {
                    long rec = svc.m_lngQueryWorkLoadByDevice(p_strCatory, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
            }
            else
            {
                using (com.digitalwave.iCare.middletier.LIS.bizLisReport svc = new com.digitalwave.iCare.middletier.LIS.bizLisReport())
                {
                    long rec = svc.m_lngQueryWorkLoadByDevice(p_strCatory, p_strStartDat, p_strEndDat, out p_dtResult);
                    p_dtResult = Function.ReNameDatatable(p_dtResult);
                    return rec;
                }
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
