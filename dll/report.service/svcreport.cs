using com.digitalwave.iCare.middletier.HIS.Report;
using Report.Com;
using System;
using System.Collections.Generic;
using System.Data;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Report.Service
{
    public class SvcReport : Report.Itf.ItfReport
    {
        #region OP

        #region 1

        public long m_lngGetRegisterStatDataByInvoArr(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetRegisterStatDataByInvoArr(p_strOperatorId, p_strStartDate, p_strEndDate, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        public long GetRegisterBillReprintByInvoArr(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetRegisterBillReprintByInvoArr(p_strOperatorId, p_strStartDate, p_strEndDate, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngGetCheckMan(out DataTable dtCheckMan)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetCheckMan(out dtCheckMan);
                dtCheckMan = Function.ReNameDatatable(dtCheckMan);
                return rec;
            }
        }

        public long m_lngGetNOCheckOutInvoice(string startDate, string endDate, out DataTable p_dtResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetNOCheckOutInvoice(startDate, endDate, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngQueryArrearsPatientByDate(string p_strStartDate, string p_strEndDate, out DataTable p_dtResult, bool p_blnALL)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngQueryArrearsPatientByDate(p_strStartDate, p_strEndDate, out p_dtResult, p_blnALL);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngQueryPayedPatientByDate(string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngQueryPayedPatientByDate(p_strStartDate, p_strEndDate, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngGetDeptArea(out DataTable dt)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetDeptArea(out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetRptDoctorPerformance(string p_beginDate, string p_endDate, string p_strStatType, string p_strDoctorID, List<string> DeptIDArr, int intFlag, out DataTable dtResult)
        {
            using (BizOpReport svc = new BizOpReport())
            {
                dtResult = new DataTable();
                long rec = svc.m_lngGetRptDoctorPerformance(p_beginDate, p_endDate, p_strStatType, p_strDoctorID, DeptIDArr, intFlag, ref dtResult);
                dtResult = Function.ReNameDatatable(dtResult);
                return rec;
            }

            //using (clsOpReport svc = new clsOpReport())
            //{
            //    dtResult = new DataTable();
            //    long rec = svc.m_lngGetRptDoctorPerformance(p_beginDate, p_endDate, p_strStatType, p_strDoctorID, DeptIDArr, intFlag, ref dtResult);
            //    dtResult = Function.ReNameDatatable(dtResult);
            //    return rec;
            //}
        }

        public long m_lngGetStatiticsData(string p_dtm1, string p_dtm2, out DataTable p_dtb)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetStatiticsData(p_dtm1, p_dtm2, out p_dtb);
                p_dtb = Function.ReNameDatatable(p_dtb);
                return rec;
            }
        }

        public long GetCheckOutData(string OPREMPID, string strDate, string strRptId, out DataTable dtCheckOut)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetCheckOutData(OPREMPID, strDate, strRptId, out dtCheckOut);
                dtCheckOut = Function.ReNameDatatable(dtCheckOut);
                return rec;
            }
        }

        public long GetCheckOutData(int intMode, string OPREMPID, string strStartDate, string strEndDate, string strRptId, out DataTable dtCheckOut)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetCheckOutData(intMode, OPREMPID, strStartDate, strEndDate, strRptId, out dtCheckOut);
                dtCheckOut = Function.ReNameDatatable(dtCheckOut);
                return rec;
            }
        }

        public void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string BalanceTime, out string[] InvonoArr, int status)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                svc.m_mthGetbalancerepeatinvoinfo(BalanceEmp, BalanceTime, out InvonoArr, status);
            }
        }

        public void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string strBeginDate, string strEndDate, out string[] InvonoArr, int intMode)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                svc.m_mthGetbalancerepeatinvoinfo(BalanceEmp, strBeginDate, strEndDate, out InvonoArr, intMode);
            }
        }

        public long GetCheckOutHistory(string strDate, string BALANCEEMP, string strRptId, out DataTable dtCheckOut)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetCheckOutHistory(strDate, BALANCEEMP, strRptId, out dtCheckOut);
                dtCheckOut = Function.ReNameDatatable(dtCheckOut);
                return rec;
            }
        }

        public long m_lngGetHistor(string startDate, string endDate, string checkMan, out DataTable dt)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetHistor(startDate, endDate, checkMan, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngCheckData(string OperID, out string CheckDate)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngCheckData(OperID, out CheckDate);
            }
        }

        public long m_lngCheckDataByDate(string OperID, string strIdentCheckDate, out string CheckDate)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngCheckDataByDate(OperID, strIdentCheckDate, out CheckDate);
            }
        }

        public long m_lngGetCheckedOutDataByCondition(int m_intStatDateType, string m_strCheckManID, string m_strBalanceDeptID, string m_strBeginTime, string m_strEndTime, string strRptId, List<string> fysfcdeptidARR, out DataTable m_dtCheckOutData, out DataTable dtTemp)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetCheckedOutDataByCondition(m_intStatDateType, m_strCheckManID, m_strBalanceDeptID, m_strBeginTime, m_strEndTime, strRptId, fysfcdeptidARR, out m_dtCheckOutData, out dtTemp);
                m_dtCheckOutData = Function.ReNameDatatable(m_dtCheckOutData);
                dtTemp = Function.ReNameDatatable(dtTemp);
                return rec;
            }
        }

        public long m_lngGetCheckMan(out DataTable dtEmpAll, string strINTERNALFLAG)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetCheckMan(out dtEmpAll, strINTERNALFLAG);
                dtEmpAll = Function.ReNameDatatable(dtEmpAll);
                return rec;
            }
        }

        public long m_lngGetRegdept(out DataTable dtdept, string strEmpId)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetRegdept(out dtdept, strEmpId);
                dtdept = Function.ReNameDatatable(dtdept);
                return rec;
            }
        }

        public long m_lngGetAllCheckMan(out DataTable dtEmp)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetAllCheckMan(out dtEmp);
                dtEmp = Function.ReNameDatatable(dtEmp);
                return rec;
            }
        }
        #endregion

        #region 2

        public long GetInvoiceInfoByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetInvoiceInfoByDate(p_strOperatorId, p_strStartDate, p_strEndDate, p_strBalanceDeptID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long GetInvoiceInfoByDate(string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetInvoiceInfoByDate(p_strStartDate, p_strEndDate, p_strBalanceDeptID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long GetInvoiceReprintByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetInvoiceReprintByDate(p_strOperatorId, p_strStartDate, p_strEndDate, p_strBalanceDeptID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long GetInvoiceReprintByDate(string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetInvoiceReprintByDate(p_strStartDate, p_strEndDate, p_strBalanceDeptID, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngSelectDoctorEarning(string strBeginDat, string strEndDat, out DataTable m_dtbReport)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngSelectDoctorEarning(strBeginDat, strEndDat, out m_dtbReport);
                m_dtbReport = Function.ReNameDatatable(m_dtbReport);
                return rec;
            }
        }

        public long m_lngGetTypeID(string p_strRptID, string p_strGroupID, out DataTable p_dtbTypeID)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetTypeID(p_strRptID, p_strGroupID, out p_dtbTypeID);
                p_dtbTypeID = Function.ReNameDatatable(p_dtbTypeID);
                return rec;
            }
        }

        public long m_lngGetDoctorEarningCollect(string strBeginDat, string strEndDat, string[] p_strTypeIDArr1, string[] p_strTypeIDArr2, out DataTable m_dtbReport)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetDoctorEarningCollect(strBeginDat, strEndDat, p_strTypeIDArr1, p_strTypeIDArr2, out m_dtbReport);
                m_dtbReport = Function.ReNameDatatable(m_dtbReport);
                return rec;
            }
        }

        public long m_lngGetRegisterStatData(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetRegisterStatData(p_strOperatorId, p_strStartDate, p_strEndDate, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        public long GetRegisterBillReprintByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetRegisterBillReprintByDate(p_strOperatorId, p_strStartDate, p_strEndDate, out p_dtResult);
                p_dtResult = Function.ReNameDatatable(p_dtResult);
                return rec;
            }
        }

        public long m_lngGetYBPatientPayType(out clsPatientType_VO[] arrPatientType)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngGetYBPatientPayType(out arrPatientType);
            }
        }

        public long m_lngGetPatientPayType(string patientPayTypeId, out clsPatientType_VO patientType)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngGetPatientPayType(patientPayTypeId, out patientType);
            }
        }

        public long m_lngFindAll(out clsYBDefPayTypeVO[] arrYBDefPayType)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngFindAll(out arrYBDefPayType);
            }
        }

        public long m_lngFind(string payTypeId, out clsYBDefPayTypeVO ybDefPayType)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngFind(payTypeId, out ybDefPayType);
            }
        }

        public long m_lngDelete(clsYBDefPayTypeVO objVo)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngDelete(objVo);
            }
        }

        public long m_lngUpdate(clsYBDefPayTypeVO objVo)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngUpdate(objVo);
            }
        }

        public long m_lngInsert(clsYBDefPayTypeVO ybDefPayTypeVO)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                return svc.m_lngInsert(ybDefPayTypeVO);
            }
        }

        public long m_lngGetDeptInfo(out DataTable m_dtDept, string strINTERNALFLAG)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetDeptInfo(out m_dtDept, strINTERNALFLAG);
                m_dtDept = Function.ReNameDatatable(m_dtDept);
                return rec;
            }
        }

        public long GetCheckOutData(string OPREMPID, string strDate, string strRptId, out DataTable dtCheckOut, out DataTable dtDiffSum)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetCheckOutData(OPREMPID, strDate, strRptId, out dtCheckOut, out dtDiffSum);
                dtCheckOut = Function.ReNameDatatable(dtCheckOut);
                dtDiffSum = Function.ReNameDatatable(dtDiffSum);
                return rec;
            }
        }

        public void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string BalanceTime, out string[] InvonoArr, int status, out decimal[] invoMoneyArr)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                svc.m_mthGetbalancerepeatinvoinfo(BalanceEmp, BalanceTime, out InvonoArr, status, out invoMoneyArr);
            }
        }

        public long GetCheckOutHistory(string strDate, string BALANCEEMP, string strRptId, out DataTable dtCheckOut, out DataTable dtTemp)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.GetCheckOutHistory(strDate, BALANCEEMP, strRptId, out dtCheckOut, out dtTemp);
                dtCheckOut = Function.ReNameDatatable(dtCheckOut);
                dtTemp = Function.ReNameDatatable(dtTemp);
                return rec;
            }
        }

        public long m_lngGetCheckManByDeptId(out DataTable dt, string strdeptId)
        {
            using (clsOpReport svc = new clsOpReport())
            {
                long rec = svc.m_lngGetCheckManByDeptId(out dt, strdeptId);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        #endregion

        #region HISMedTypeManage

        public long m_mthGetPayTypeInfoByID(int intFlag, out DataTable dt)
        {
            using (clsHISMedTypeManage svc = new clsHISMedTypeManage())
            {
                long rec = svc.m_mthGetPayTypeInfoByID(intFlag, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_mthGetBalanceEmpInfo(out DataTable dt)
        {
            using (clsHISMedTypeManage svc = new clsHISMedTypeManage())
            {
                long rec = svc.m_mthGetBalanceEmpInfo(out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_mthGetStatDeptInfo(out DataTable dt)
        {
            using (clsHISMedTypeManage svc = new clsHISMedTypeManage())
            {
                long rec = svc.m_mthGetStatDeptInfo(out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_mthGetPatientPayTypeInfo(out DataTable dt)
        {
            using (clsHISMedTypeManage svc = new clsHISMedTypeManage())
            {
                long rec = svc.m_mthGetPatientPayTypeInfo(out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }
        #endregion

        #endregion

        #region IP

        #region clsHISReportZy_Supported_Svc

        public long m_lngGetCollectorReport_PatientSource(string p_dtmStart, string p_dtmEnd, string p_dtmOutStart, string p_dtmOutEnd, out DataTable p_dtbReulst)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.m_lngGetCollectorReport_PatientSource(p_dtmStart, p_dtmEnd, p_dtmOutStart, p_dtmOutEnd, out p_dtbReulst);
                p_dtbReulst = Function.ReNameDatatable(p_dtbReulst);
                return rec;
            }
        }

        public DataTable GetYGItem(string beginDate, string endDate)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetYGItem(beginDate, endDate));
            }
        }

        public DataTable GetCriticalDeal(string beginDate, string endDate)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetCriticalDeal(beginDate, endDate));
            }
        }

        public DataTable GetCriticalExecute(string beginDate, string endDate)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetCriticalExecute(beginDate, endDate));
            }
        }

        public DataTable GetCriticalClinicaldept(string beginDate, string endDate, string deptStr)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetCriticalClinicaldept(beginDate, endDate, deptStr));
            }
        }

        public DataTable GetCriticalMedicaldept(string beginDate, string endDate, string deptStr)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetCriticalMedicaldept(beginDate, endDate, deptStr));
            }
        }

        public DataTable GetCriticalAreaDpet(string beginDate, string endDate, string deptStr)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetCriticalAreaDpet(beginDate, endDate, deptStr));
            }
        }

        public DataTable GetCriticalUnreport(string beginDate, string endDate, string deptStr)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetCriticalUnreport(beginDate, endDate, deptStr));
            }
        }

        public DataTable GetSamplePackStat(List<string> lstParam, string dteStart, string dteEnd, string sampleType, string patName, string barCode, string peno, string deptStr, int checkState, int timeType)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetSamplePackStat(lstParam, dteStart, dteEnd, sampleType, patName, barCode, peno, deptStr, checkState, timeType));
            }
        }

        public DataTable GetLisSampletype(string beginDate, string endDate)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetLisSampletype(beginDate, endDate));
            }
        }

        public DataTable GetLisSampletype2(string beginDate, string endDate, string groupId, string applyUnitId, string patType, string strDept)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetLisSampletype2(beginDate, endDate, groupId, applyUnitId, patType, strDept));
            }
        }

        public DataTable GetBysswSampletype(string beginDate, string endDate, string groupId, string applyUnitId, string patType, string strDept)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetBysswSampletype(beginDate, endDate, groupId, applyUnitId, patType, strDept));
            }
        }

        public DataTable GetStatSampleBack(string beginDate, string endDate, string sampleType, int patType)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetStatSampleBack(beginDate, endDate, sampleType, patType));
            }
        }

        public DataTable GetStatSampleBack2(string beginDate, string endDate, string sampleType, int patType)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetStatSampleBack2(beginDate, endDate, sampleType, patType));
            }
        }

        public DataTable GetStatSampleBackReson(string beginDate, string endDate)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetStatSampleBackReson(beginDate, endDate));
            }
        }

        public DataTable GetDeptList(int deptint)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetDeptList(deptint));
            }
        }

        public DataTable GetCrval()
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetCrval());
            }
        }

        public DataTable GetCrval2()
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetCrval2());
            }
        }

        public DataTable GetSampleType()
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetSampleType());
            }
        }

        public DataTable GetGategoryType()
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetGategoryType());
            }
        }

        public DataTable GetGategoryType2()
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetGategoryType2());
            }
        }

        public DataTable GetYGItemType()
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetYGItemType());
            }
        }

        public DataTable GeItemContent(string findStr)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GeItemContent(findStr));
            }
        }

        public DataTable GetLisCheckItem(string beginDate, string endDate, string CheckLisId)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetLisCheckItem(beginDate, endDate, CheckLisId));
            }
        }

        public DataTable GetPacsCheckItem(string beginDate, string endDate, string CheckPacsId)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetPacsCheckItem(beginDate, endDate, CheckPacsId));
            }
        }

        public long GetWorkLoadCount(string dteStart, string dteEnd, string categoryId, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetWorkLoadCount(dteStart, dteEnd, categoryId, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetSampleMedSpec(out DataTable dtbResult, string dteStart, string dteEnd, string groupId, string applyUnitId, string strDept, string enmergencyFlg, string patType,int tsFlg)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetSampleMedSpec(out dtbResult, dteStart, dteEnd, groupId, applyUnitId, strDept, enmergencyFlg, patType,tsFlg);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetSampleAcceptable(out DataTable dtbResult, string dteStart, string dteEnd, string applyUnitId, string strDept, string enmergencyFlg, string patType)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetSampleAcceptable(out dtbResult, dteStart, dteEnd, applyUnitId, strDept, enmergencyFlg, patType);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetPositiveReport(out DataTable dtbResult, string dteStart, string dteEnd, string checkItemId, string strDept, string groupId, string patNo)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetPositiveReport(out dtbResult, dteStart, dteEnd, checkItemId, strDept, groupId, patNo);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetPmpctDetail(string dteStart, string dteEnd, string patType, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetPmpctDetail(dteStart, dteEnd, patType, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetPmpctStat(string dteStart, string dteEnd, string patType, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetPmpctStat(dteStart, dteEnd, patType, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetCheckerQc(out DataTable dtbResult, string dteStart, string dteEnd)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetCheckerQc(out dtbResult, dteStart, dteEnd);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetLimitTime(out DataTable dtbResult, string applyunitid)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetLimitTime(out dtbResult, applyunitid);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetAllLimitTime(out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetAllLimitTime(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long SaveLimitTime(DataTable dt)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return svc.SaveLimitTime(dt);
            }
        }

        public long DeleteLimitTime(string applyunitid)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return svc.DeleteLimitTime(applyunitid);
            }
        }

        public long GetAllCheckSpec(out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetAllCheckSpec(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetAllCheckItem(out DataTable dtbResult, string groupId)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetAllCheckItem(out dtbResult, groupId);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetAllCheckItemCpy(out DataTable dtbResult, string groupId)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetAllCheckItemCpy(out dtbResult, groupId);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetAllCheckItemDetail(out DataTable dtbResult, string groupId)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetAllCheckItemDetail(out dtbResult, groupId);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetCheckItemByName(string strTempName, string groupId, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetCheckItemByName(strTempName, groupId, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetCheckItemByNameCpy(string strTempName, string groupId, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetCheckItemByNameCpy(strTempName, groupId, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetCheckItemDetailByNameCpy(string strTempName, string groupId, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetCheckItemDetailByNameCpy(strTempName, groupId, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetDeptArea(out DataTable dt)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetDeptArea(out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long lngGetAllAnti(out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetAllAnti(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetFuzzyQueryAnti(string micName, bool IsEnglish, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetFuzzyQueryAnti(micName, IsEnglish, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetAllMic(out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetAllMic(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetAllGlMic(out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetAllGlMic(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetFuzzyQueryMic(string micName, bool IsEnglish, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetFuzzyQueryMic(micName, IsEnglish, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetBacteriaDistribution(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, string TestMethod, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetBacteriaDistribution(micname, p_dtDateFrom, p_dtDateTO, applicationStr, DeptIdArr, sampleId, DisNo, Sex, TestMethod, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetMicSensitive(string applicationStr, string sampleId, string DisNo, string Sex, string strTempAntiName, string strTempAnti, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetMicSensitive(applicationStr, sampleId, DisNo, Sex, strTempAntiName, strTempAnti, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetMicdistributionTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, string TestMethod, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetMicdistributionTend(micname, p_dtDateFrom, p_dtDateTO, applicationStr, DeptIdArr, sampleId, DisNo, Sex, TestMethod, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetSensitiveTend(string applicationStr, string sampleId, string DisNo, string Sex, string strTempAntiID, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetSensitiveTend(applicationStr, sampleId, DisNo, Sex, strTempAntiID, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetMicCumulative(string applicationStr, string sampleId, string DisNo, string Sex, string TestMethod, string strTempAntiID, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetMicCumulative(applicationStr, sampleId, DisNo, Sex, TestMethod, strTempAntiID, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetAntiDetail(string strTempName, DateTime p_dtDateFrom, DateTime p_dtDateTO, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetAntiDetail(strTempName, p_dtDateFrom, p_dtDateTO, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetAntiSample(string strTempName, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetAntiSample(strTempName, applicationStr, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetAntiByDept(string strTempName, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetAntiByDept(strTempName, applicationStr, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetApplicateion(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string criticalStr, string DeptIdArr, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetApplicateion(micname, p_dtDateFrom, p_dtDateTO, criticalStr, DeptIdArr, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetAnti(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string DeptIdArr, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetAnti(micname, p_dtDateFrom, p_dtDateTO, DeptIdArr, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetGss(string applicationStr, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetGss(applicationStr, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetAntiCheckItem(DateTime p_dtDateFrom, DateTime p_dtDateTO, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetAntiCheckItem(p_dtDateFrom, p_dtDateTO, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetDeptName(out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetDeptName(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetSamType(out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetSamType(out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public DataTable GetRptCross(string month)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetRptCross(month));
            }
        }

        public long SaveCrossSum(DataTable dt)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return svc.SaveCrossSum(dt);
            }
        }

        public long AddCharegToTable(DataTable dt, string date)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return svc.AddCharegToTable(dt, date);
            }
        }

        public DataTable GetDeptChareg(string beginDate, string endDate, List<string> DeptIDArr)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetDeptChareg(beginDate, endDate, DeptIDArr));
            }
        }

        public DataTable GetICUchareg(string beginDate, string endDate)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetICUchareg(beginDate, endDate));
            }
        }

        public DataTable GetPatTransf(string registerid)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return Function.ReNameDatatable(svc.GetPatTransf(registerid));
            }
        }

        public long lngGetPretestMedStatment(string dteStart, string dteEnd, string deptStr, string orderType, string medName, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetPretestMedStatment(dteStart, dteEnd, deptStr, orderType, medName, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetRePretestMedStat(string putmedreqIdStr, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetRePretestMedStat(putmedreqIdStr, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetCureMedStatment(string dteStart, string dteEnd, string deptStr, string medName, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetCureMedStatment(dteStart, dteEnd, deptStr, medName, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetKsMedStatment(string dteStart, string dteEnd, string deptStr, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetKsMedStatment(dteStart, dteEnd, deptStr, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long lngGetKsMed(string dteStart, string dteEnd, string deptStr, out DataTable dtbResult)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.lngGetKsMed(dteStart, dteEnd, deptStr, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetArea(out DataTable dt)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                long rec = svc.GetArea(out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public void GetAdjustPrice(string beginDate, string endDate, string effectDate, out DataTable dtItem, out DataTable dtCharge)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                svc.GetAdjustPrice(beginDate, endDate, effectDate, out dtItem, out dtCharge);
                dtItem = Function.ReNameDatatable(dtItem);
                dtCharge = Function.ReNameDatatable(dtCharge);
            }
        }

        public long SaveAdjustPrice(DataTable dt)
        {
            using (clsHISReportZy_Supported_Svc svc = new clsHISReportZy_Supported_Svc())
            {
                return svc.SaveAdjustPrice(dt);
            }
        }

        #endregion

        #region clsReportZY_Svc

        public long m_lngRptItemDetailStat(string CodeNo, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngRptItemDetailStat(CodeNo, BeginDate, EndDate, DeptIDArr, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetGroupInComeByDoctor(ref clsGroupInComeByDoctorOrArea_VO objvalue_Param, out DataTable dtbResult)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                dtbResult = new DataTable();
                long rec = svc.m_lngGetGroupInComeByDoctor(ref objvalue_Param, ref dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long m_lngFindArea(string strCode, out clsBIHArea[] arrArea)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                return svc.m_lngFindArea(strCode, out arrArea);
            }
        }

        public long m_lngGetPayTypeInfo(out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetPayTypeInfo(out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetDeptArea(out DataTable dt, int Flag)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetDeptArea(out dt, Flag);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetBedinfo(string AreaID, int status, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetBedinfo(AreaID, status, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetPatientinfoByZyh(string no, out DataTable dt, int type)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetPatientinfoByZyh(no, out dt, type);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetPatientinfo(string SqlWhereZY, int Status, bool IsIncludeMZ, string SqlWhereMZ, clsCommonQueryDate_VO CommonQueryDate_VO, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetPatientinfo(SqlWhereZY, Status, IsIncludeMZ, SqlWhereMZ, CommonQueryDate_VO, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetPatientinfoByZyh(string Zyh, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetPatientinfoByZyh(Zyh, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngFindChargeItem(string FindStr, string PatType, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngFindChargeItem(FindStr, PatType, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngFindChargeItem(string ItemID, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngFindChargeItem(ItemID, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetPatientFeeDetByPayType(string RegID, string PayTypeID, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetPatientFeeDetByPayType(RegID, PayTypeID, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long GetPatientBihStatistics(DateTime dtStartime, DateTime dtEndTime, out DataTable dtbResult)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.GetPatientBihStatistics(dtStartime, dtEndTime, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetPatientLeftStatistics(DateTime dtStartime, DateTime dtEndTime, out DataTable dtbResult)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.GetPatientLeftStatistics(dtStartime, dtEndTime, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetPatientBihStatistics(DateTime dtStartime, DateTime dtEndTime, string strPaytypeId, out DataTable dtbResult)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.GetPatientBihStatistics(dtStartime, dtEndTime, strPaytypeId, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long GetPatientLeftStatistics(DateTime dtStartime, DateTime dtEndTime, string strPaytypeId, out DataTable dtbResult)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.GetPatientLeftStatistics(dtStartime, dtEndTime, strPaytypeId, out dtbResult);
                dtbResult = Function.ReNameDatatable(dtbResult);
                return rec;
            }
        }

        public long m_lngGetOwnCastItem(string strInpinsurancetype, string RegisterID, out DataTable dtResult)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetOwnCastItem(strInpinsurancetype, RegisterID, out dtResult);
                dtResult = Function.ReNameDatatable(dtResult);
                return rec;
            }
        }

        public long m_lngGetRegisterID(string inPatientID, out DataTable dt, int p_intType)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetRegisterID(inPatientID, out dt, p_intType);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetRptNursingLog(DateTime dtmTmp, string DeptID, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetRptNursingLog(dtmTmp, DeptID, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngGetRptNusingPatientCount(DateTime dtmTmp, string DeptID, out DataTable dt)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngGetRptNusingPatientCount(dtmTmp, DeptID, out dt);
                dt = Function.ReNameDatatable(dt);
                return rec;
            }
        }

        public long m_lngContractUnitPayType(string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult)
        {
            using (clsReportZY_Svc svc = new clsReportZY_Svc())
            {
                long rec = svc.m_lngContractUnitPayType(p_strStartDate, p_strEndDate, out p_dtbResult);
                p_dtbResult = Function.ReNameDatatable(p_dtbResult);
                return rec;
            }
        }

        #endregion

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
