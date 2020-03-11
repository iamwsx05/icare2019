using System;
//using Microsoft.Data.Odbc;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.DataShareService
{
	/// <summary>
	/// 术前小结共享数据
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsBeforeOperSumShareService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetShareValue(
			string p_strInPaitentID,string p_strInPatientDate, out DataTable p_dtbResult)
		{
			p_dtbResult = null;
            long lng = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strSql = clsDatabaseSQLConvert.s_StrTop1 + @" inpatientid,
       inpatientdate,
       opendate,
       modifydate,
       modifyuserid,
       operatedoctorid,
       chargedoctorid,
       diagnose,
       diagnosegist,
       bodyinfo,
       specialhandle,
       preparation,
       patientnotion,
       anaesthesia,
       afternotice,
       discussnotion,
       operationdate
from beforeoperationsummarycontent
where (inpatientid = ?) and (inpatientdate = ?)
order by opendate desc, modifydate desc" +clsDatabaseSQLConvert.s_StrRownum;
            
            IDataParameter[] objDPArr = null;

            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPaitentID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            long lngRef = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);

            //objTabService.Dispose();
            return lng;
		}	
	}
}
