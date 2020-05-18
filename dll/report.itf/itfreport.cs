using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using weCare.Core.Entity;

namespace Report.Itf
{
    [ServiceContract]
    public interface ItfReport : weCare.Core.Itf.IWcf
    {
        #region OP

        #region 1 

        [OperationContract]
        long m_lngGetRegisterStatDataByInvoArr(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult);

        [OperationContract]
        long GetRegisterBillReprintByInvoArr(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult);

        [OperationContract(Name = "m_lngGetCheckMan01")]
        long m_lngGetCheckMan(out DataTable dtCheckMan);

        [OperationContract]
        long m_lngGetNOCheckOutInvoice(string startDate, string endDate, out DataTable p_dtResult);

        [OperationContract]
        long m_lngQueryArrearsPatientByDate(string p_strStartDate, string p_strEndDate, out DataTable p_dtResult, bool p_blnALL);

        [OperationContract]
        long m_lngQueryPayedPatientByDate(string p_strStartDate, string p_strEndDate, out DataTable p_dtResult);

        [OperationContract(Name = "m_lngGetDeptArea01")]
        long m_lngGetDeptArea(out DataTable dt);

        [OperationContract]
        long m_lngGetRptDoctorPerformance(string p_beginDate, string p_endDate, string p_strStatType, string p_strDoctorID, List<string> DeptIDArr, int intFlag, out DataTable dtResult);

        [OperationContract]
        long m_lngGetStatiticsData(string p_dtm1, string p_dtm2, out DataTable p_dtb);

        [OperationContract(Name = "GetCheckOutData01")]
        long GetCheckOutData(string OPREMPID, string strDate, string strRptId, out DataTable dtCheckOut);

        [OperationContract(Name = "GetCheckOutData02")]
        long GetCheckOutData(int intMode, string OPREMPID, string strStartDate, string strEndDate, string strRptId, out DataTable dtCheckOut);

        [OperationContract(Name = "m_mthGetbalancerepeatinvoinfo01")]
        void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string BalanceTime, out string[] InvonoArr, int status);

        [OperationContract(Name = "m_mthGetbalancerepeatinvoinfo02")]
        void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string strBeginDate, string strEndDate, out string[] InvonoArr, int intMode);

        [OperationContract(Name = "GetCheckOutHistory01")]
        long GetCheckOutHistory(string strDate, string BALANCEEMP, string strRptId, out DataTable dtCheckOut);

        [OperationContract]
        long m_lngGetHistor(string startDate, string endDate, string checkMan, out DataTable dt);

        [OperationContract]
        long m_lngCheckData(string OperID, out string CheckDate);

        [OperationContract]
        long m_lngCheckDataByDate(string OperID, string strIdentCheckDate, out string CheckDate);

        [OperationContract]
        long m_lngGetCheckedOutDataByCondition(int m_intStatDateType, string m_strCheckManID, string m_strBalanceDeptID, string m_strBeginTime, string m_strEndTime, string strRptId, List<string> fysfcdeptidARR, out DataTable m_dtCheckOutData, out DataTable dtTemp);

        [OperationContract(Name = "m_lngGetCheckMan02")]
        long m_lngGetCheckMan(out DataTable dtEmpAll, string strINTERNALFLAG);

        [OperationContract]
        long m_lngGetRegdept(out DataTable dtdept, string strEmpId);

        [OperationContract]
        long m_lngGetAllCheckMan(out DataTable dtEmp);

        #endregion

        #region 2

        [OperationContract(Name = "GetInvoiceInfoByDate01")]
        long GetInvoiceInfoByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult);

        [OperationContract(Name = "GetInvoiceInfoByDate02")]
        long GetInvoiceInfoByDate(string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult);

        [OperationContract(Name = "GetInvoiceReprintByDate01")]
        long GetInvoiceReprintByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult);

        [OperationContract(Name = "GetInvoiceReprintByDate02")]
        long GetInvoiceReprintByDate(string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult);

        [OperationContract(Name = "m_lngSelectDoctorEarning")]
        long m_lngSelectDoctorEarning(string strBeginDat, string strEndDat, out DataTable m_dtbReport);

        [OperationContract]
        long m_lngGetTypeID(string p_strRptID, string p_strGroupID, out DataTable p_dtbTypeID);

        [OperationContract]
        long m_lngGetDoctorEarningCollect(string strBeginDat, string strEndDat, string[] p_strTypeIDArr1, string[] p_strTypeIDArr2, out DataTable m_dtbReport);

        [OperationContract]
        long m_lngGetRegisterStatData(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult);

        [OperationContract]
        long GetRegisterBillReprintByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult);

        [OperationContract]
        long m_lngGetYBPatientPayType(out clsPatientType_VO[] arrPatientType);

        [OperationContract]
        long m_lngGetPatientPayType(string patientPayTypeId, out clsPatientType_VO patientType);

        [OperationContract(Name = "m_lngFindAll01")]
        long m_lngFindAll(out clsYBDefPayTypeVO[] arrYBDefPayType);

        [OperationContract(Name = "m_lngFindAll02")]
        long m_lngFind(string payTypeId, out clsYBDefPayTypeVO ybDefPayType);

        [OperationContract]
        long m_lngDelete(clsYBDefPayTypeVO objVo);

        [OperationContract]
        long m_lngUpdate(clsYBDefPayTypeVO objVo);

        [OperationContract]
        long m_lngInsert(clsYBDefPayTypeVO ybDefPayTypeVO);

        [OperationContract]
        long m_lngGetDeptInfo(out DataTable m_dtDept, string strINTERNALFLAG);

        [OperationContract(Name = "GetCheckOutData03")]
        long GetCheckOutData(string OPREMPID, string strDate, string strRptId, out DataTable dtCheckOut, out DataTable dtDiffSum);

        [OperationContract]
        void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string BalanceTime, out string[] InvonoArr, int status, out decimal[] invoMoneyArr);

        [OperationContract(Name = "GetCheckOutHistory02")]
        long GetCheckOutHistory(string strDate, string BALANCEEMP, string strRptId, out DataTable dtCheckOut, out DataTable dtTemp);

        [OperationContract]
        long m_lngGetCheckManByDeptId(out DataTable dt, string strdeptId);

         

        #endregion

        #region HISMedTypeManage

        [OperationContract]
        long m_mthGetPayTypeInfoByID(int intFlag, out DataTable dt);

        [OperationContract]
        long m_mthGetBalanceEmpInfo(out DataTable dt);

        [OperationContract]
        long m_mthGetStatDeptInfo(out DataTable dt);

        [OperationContract]
        long m_mthGetPatientPayTypeInfo(out DataTable dt);



        #endregion

        #endregion

        #region IP

        #region clsHISReportZy_Supported_Svc

        [OperationContract]
        long m_lngGetCollectorReport_PatientSource(string p_dtmStart, string p_dtmEnd, string p_dtmOutStart, string p_dtmOutEnd, out DataTable p_dtbReulst);

        [OperationContract]
        DataTable GetYGItem(string beginDate, string endDate);

        [OperationContract]
        DataTable GetCriticalDeal(string beginDate, string endDate);

        [OperationContract]
        DataTable GetCriticalExecute(string beginDate, string endDate);

        [OperationContract]
        DataTable GetCriticalClinicaldept(string beginDate, string endDate, string deptStr);

        [OperationContract]
        DataTable GetCriticalMedicaldept(string beginDate, string endDate, string deptStr);

        [OperationContract]
        DataTable GetCriticalAreaDpet(string beginDate, string endDate, string deptStr);

        [OperationContract]
        DataTable GetCriticalUnreport(string beginDate, string endDate, string deptStr);

        [OperationContract]
        DataTable GetSamplePackStat(List<string> lstParam, string dteStart, string dteEnd, string sampleType, string patName, string barCode, string peno, string deptStr, int checkState, int timeType);

        [OperationContract]
        DataTable GetLisSampletype(string beginDate, string endDate);

        [OperationContract]
        DataTable GetLisSampletype2(string beginDate, string endDate, string groupId, string applyUnitId, string patType, string strDept);

        [OperationContract]
        DataTable GetBysswSampletype(string beginDate, string endDate, string groupId, string applyUnitId, string patType, string strDept);

        [OperationContract]
        DataTable GetStatSampleBack(string beginDate, string endDate, string sampleType, int patType);

        [OperationContract]
        DataTable GetStatSampleBack2(string beginDate, string endDate, string sampleType, int patType);

        [OperationContract]
        DataTable GetStatSampleBackReson(string beginDate, string endDate);

        [OperationContract]
        DataTable GetDeptList(int deptint);

        [OperationContract]
        DataTable GetCrval();

        [OperationContract]
        DataTable GetCrval2();

        [OperationContract]
        DataTable GetSampleType();

        [OperationContract]
        DataTable GetGategoryType();

        [OperationContract]
        DataTable GetGategoryType2(string classid);

        [OperationContract]
        DataTable GetYGItemType();

        [OperationContract]
        DataTable GeItemContent(string findStr);

        [OperationContract]
        DataTable GetLisCheckItem(string beginDate, string endDate, string CheckLisId);

        [OperationContract]
        DataTable GetPacsCheckItem(string beginDate, string endDate, string CheckPacsId);

        [OperationContract]
        long GetWorkLoadCount(string dteStart, string dteEnd, string categoryId, out DataTable dtbResult);

        [OperationContract]
        long GetSampleMedSpec(out DataTable dtbResult, string dteStart, string dteEnd, string groupId, string applyUnitId, string strDept, string enmergencyFlg, string patType,int tsFlg,bool peFlg);

        [OperationContract]
        long GetSampleColor(out DataTable dtbResult, string dteStart, string dteEnd);

        [OperationContract]
        long GetSampleAcceptable(out DataTable dtbResult, string dteStart, string dteEnd, string applyUnitId, string strDept, string enmergencyFlg, string patType);

        [OperationContract]
        long GetPositiveReport(out DataTable dtbResult, string dteStart, string dteEnd, string checkItemId, string strDept, string groupId, string patNo);

        [OperationContract]
        long GetPmpctDetail(string dteStart, string dteEnd, string patType, string applyUnitId, out DataTable dtbResult);

        [OperationContract]
        long GetPmpctStat(string dteStart, string dteEnd, string patType, string applyUnitId, out DataTable dtbResult);

        [OperationContract]
        long GetCheckerQc(out DataTable dtbResult, string dteStart, string dteEnd);

        [OperationContract]
        long GetSampleArenaStat(out DataTable dtbResult, string dteStart, string dteEnd);

        [OperationContract]
        long GetLimitTime(out DataTable dtbResult, string applyunitid);

        [OperationContract]
        long GetAllLimitTime(out DataTable dtbResult);

        [OperationContract]
        long SaveLimitTime(DataTable dt);

        [OperationContract]
        long DeleteLimitTime(string applyunitid);

        [OperationContract]
        long GetAllCheckSpec(out DataTable dtbResult);

        [OperationContract]
        long GetAllCheckItem(out DataTable dtbResult, string groupId);

        [OperationContract]
        long GetAllCheckItemCpy(out DataTable dtbResult, string groupId);

        [OperationContract]
        long GetAllCheckItemDetail(out DataTable dtbResult, string groupId);

        [OperationContract]
        long GetCheckItemByName(string strTempName, string groupId, out DataTable dtbResult);

        [OperationContract]
        long GetCheckItemByNameCpy(string strTempName, string groupId, out DataTable dtbResult);

        [OperationContract]
        long GetCheckItemDetailByNameCpy(string strTempName, string groupId, out DataTable dtbResult);

        [OperationContract]
        long GetDeptArea(out DataTable dt);

        [OperationContract]
        long lngGetAllAnti(out DataTable dtbResult);

        [OperationContract]
        long lngGetFuzzyQueryAnti(string micName, bool IsEnglish, out DataTable dtbResult);

        [OperationContract]
        long lngGetAllMic(out DataTable dtbResult);

        [OperationContract]
        long lngGetAllGlMic(out DataTable dtbResult);

        [OperationContract]
        long lngGetFuzzyQueryMic(string micName, bool IsEnglish, out DataTable dtbResult);

        [OperationContract]
        long lngGetBacteriaDistribution(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, string TestMethod, out DataTable dtbResult);

        [OperationContract]
        long lngGetMicSensitive(string applicationStr, string sampleId, string DisNo, string Sex, string strTempAntiName, string strTempAnti, out DataTable dtbResult);

        [OperationContract]
        long lngGetMicdistributionTend(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, string TestMethod, out DataTable dtbResult);

        [OperationContract]
        long lngGetSensitiveTend(string applicationStr, string sampleId, string DisNo, string Sex, string strTempAntiID, out DataTable dtbResult);

        [OperationContract]
        long lngGetMicCumulative(string applicationStr, string sampleId, string DisNo, string Sex, string TestMethod, string strTempAntiID, out DataTable dtbResult);

        [OperationContract]
        long lngGetAntiDetail(string strTempName, DateTime p_dtDateFrom, DateTime p_dtDateTO, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult);

        [OperationContract]
        long lngGetAntiSample(string strTempName, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult);

        [OperationContract]
        long lngGetAntiByDept(string strTempName, string applicationStr, string DeptIdArr, string sampleId, string DisNo, string Sex, out DataTable dtbResult);

        [OperationContract]
        long lngGetApplicateion(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string criticalStr, string DeptIdArr, out DataTable dtbResult);

        [OperationContract]
        long lngGetAnti(string micname, DateTime p_dtDateFrom, DateTime p_dtDateTO, string DeptIdArr, out DataTable dtbResult);

        [OperationContract]
        long lngGetGss(string applicationStr, out DataTable dtbResult);

        [OperationContract]
        long lngGetAntiCheckItem(DateTime p_dtDateFrom, DateTime p_dtDateTO, out DataTable dtbResult);

        [OperationContract]
        long lngGetDeptName(out DataTable dtbResult);

        [OperationContract]
        long lngGetSamType(out DataTable dtbResult);

        [OperationContract]
        DataTable GetRptCross(string month);

        [OperationContract]
        long SaveCrossSum(DataTable dt);

        [OperationContract]
        long AddCharegToTable(DataTable dt, string date);

        [OperationContract]
        DataTable GetDeptChareg(string beginDate, string endDate, List<string> DeptIDArr);

        [OperationContract]
        DataTable GetICUchareg(string beginDate, string endDate);

        [OperationContract]
        DataTable GetPatTransf(string registerid);

        [OperationContract]
        long lngGetPretestMedStatment(string dteStart, string dteEnd, string deptStr, string orderType, string medName, out DataTable dtbResult);

        [OperationContract]
        long lngGetRePretestMedStat(string putmedreqIdStr, out DataTable dtbResult);

        [OperationContract]
        long lngGetCureMedStatment(string dteStart, string dteEnd, string deptStr, string medName, out DataTable dtbResult);

        [OperationContract]
        long lngGetKsMedStatment(string dteStart, string dteEnd, string deptStr, out DataTable dtbResult);

        [OperationContract]
        long lngGetKsMed(string dteStart, string dteEnd, string deptStr, out DataTable dtbResult);

        [OperationContract]
        long GetArea(out DataTable dt);

        [OperationContract]
        void GetAdjustPrice(string beginDate, string endDate, string effectDate, out DataTable dtItem, out DataTable dtCharge);

        [OperationContract]
        long SaveAdjustPrice(DataTable dt);

        #endregion

        #region clsReportZY_Svc

        [OperationContract]
        long m_lngRptItemDetailStat(string CodeNo, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt);

        [OperationContract]
        long m_lngGetGroupInComeByDoctor(ref clsGroupInComeByDoctorOrArea_VO objvalue_Param, out DataTable dtbResult);

        [OperationContract]
        long m_lngFindArea(string strCode, out clsBIHArea[] arrArea);

        [OperationContract]
        long m_lngGetPayTypeInfo(out DataTable dt);

        [OperationContract(Name = "m_lngGetDeptArea02")]
        long m_lngGetDeptArea(out DataTable dt, int Flag);

        [OperationContract]
        long m_lngGetBedinfo(string AreaID, int status, out DataTable dt);

        [OperationContract(Name = "m_lngGetPatientinfoByZyh01")]
        long m_lngGetPatientinfoByZyh(string no, out DataTable dt, int type);

        [OperationContract]
        long m_lngGetPatientinfo(string SqlWhereZY, int Status, bool IsIncludeMZ, string SqlWhereMZ, clsCommonQueryDate_VO CommonQueryDate_VO, out DataTable dt);

        [OperationContract(Name = "m_lngGetPatientinfoByZyh02")]
        long m_lngGetPatientinfoByZyh(string Zyh, out DataTable dt);

        [OperationContract(Name = "m_lngFindChargeItem01")]
        long m_lngFindChargeItem(string FindStr, string PatType, out DataTable dt);

        [OperationContract(Name = "m_lngFindChargeItem02")]
        long m_lngFindChargeItem(string ItemID, out DataTable dt);

        [OperationContract]
        long m_lngGetPatientFeeDetByPayType(string RegID, string PayTypeID, out DataTable dt);

        [OperationContract(Name = "GetPatientBihStatistics01")]
        long GetPatientBihStatistics(DateTime dtStartime, DateTime dtEndTime, out DataTable dtbResult);

        [OperationContract(Name = "GetPatientLeftStatistics01")]
        long GetPatientLeftStatistics(DateTime dtStartime, DateTime dtEndTime, out DataTable dtbResult);

        [OperationContract(Name = "GetPatientBihStatistics02")]
        long GetPatientBihStatistics(DateTime dtStartime, DateTime dtEndTime, string strPaytypeId, out DataTable dtbResult);

        [OperationContract(Name = "GetPatientLeftStatistics02")]
        long GetPatientLeftStatistics(DateTime dtStartime, DateTime dtEndTime, string strPaytypeId, out DataTable dtbResult);

        [OperationContract]
        long m_lngGetOwnCastItem(string strInpinsurancetype, string RegisterID, out DataTable dtResult);

        [OperationContract]
        long m_lngGetRegisterID(string inPatientID, out DataTable dt, int p_intType);

        [OperationContract]
        long m_lngGetRptNursingLog(DateTime dtmTmp, string DeptID, out DataTable dt);

        [OperationContract]
        long m_lngGetRptNusingPatientCount(DateTime dtmTmp, string DeptID, out DataTable dt);

        [OperationContract]
        long m_lngContractUnitPayType(string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult);

        #endregion

        #endregion
         
    }
}
