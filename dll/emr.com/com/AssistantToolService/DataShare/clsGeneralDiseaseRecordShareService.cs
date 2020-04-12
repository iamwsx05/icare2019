using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.DataShareService
{
	/// <summary>
	/// Summary description for clsGeneralDiseaseRecordShareService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsGeneralDiseaseRecordShareService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		
		[AutoComplete]
		public long m_lngGetFirstDiseaseInfoShareValue(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lng = 0;
            try
            {
                //			string strSql = @"select * from(select RecordContent_Right
                //FROM GeneralDiseaseRecord a
                //inner join GeneralDiseaseRecordContent b
                //on a.InPatientID = b.InPatientID
                //and a.InPatientDate = b.InPatientDate
                //and a.OpenDate = b.OpenDate
                //WHERE (a.InPatientID = '"+p_strInPaitentID+@"') 
                //and (a.InPatientDate = " +clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+@")
                //and a.RecordTitle = '首次病程记录' 
                //ORDER BY a.OpenDate DESC, b.ModifyDate DESC)where rownum = 1";			
                string strSql = clsDatabaseSQLConvert.s_StrTop1 + @" mostlycontent_right,originaldiagnose_right,thereunderdiagnose_right,diagnosediffe_right,cureplan_right 
			from firstillnessnoterecord  a
			inner join firstillnessnoterecordcontent  b
			on a.inpatientid = b.inpatientid
			and a.inpatientdate = b.inpatientdate
			and a.opendate = b.opendate
			where (a.inpatientid = ?) 
			and (a.inpatientdate = ?) and a.status=0 order by a.opendate desc, b.modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

                IDataParameter[] objDPArr = null;

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPaitentID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                long lngRef = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);
            }
            catch(Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            //objTabService.Dispose();
            return lng;
		}	
	}
}
