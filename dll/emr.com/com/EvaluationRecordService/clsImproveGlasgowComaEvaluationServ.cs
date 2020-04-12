using System;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.ImproveGlasgowComaEvaluationServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsImproveGlasgowComaEvaluationServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsImproveGlasgowComaEvaluationServ()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strFromDate"></param>
		/// <param name="p_strToDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTimeInfoOfAPatient( string p_strInPatientID,string p_strInPatientDate,string p_strFromDate,string p_strToDate,ref string  p_strXml,ref int  intRows)
		{
			string strCommand="";
            clsHRPTableService clsTablService = new clsHRPTableService();
			long lngRes = -1;
            try
            { 
                //			strCommand="SELECT distinct ActivityTime FROM ImproveGlasgowComaEvluation WHERE InPatientNO ='" +
                //				p_strInPatientID +"'" +" AND Status =0 and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and ActivityTime>=" + clsHRPTableService.s_strOracleDateTime(p_strFromDate )+ " and ActivityTime<=" + clsHRPTableService.s_strOracleDateTime(p_strToDate) ;
                strCommand = "SELECT distinct ActivityTime FROM ImproveGlasgowComaEvluation WHERE InPatientNO ='" +
                    p_strInPatientID + "'" + " AND Status =0 and InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " ";


                lngRes = clsTablService.lngGetXMLTable(strCommand, ref p_strXml, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}

		[AutoComplete]
		public long m_lngAddNew( string p_strMainXml)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (p_strMainXml == null || p_strMainXml == "")
                    return -1;

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intIndex = p_strMainXml.IndexOf(' ');

                p_strMainXml = p_strMainXml.Insert(intIndex, " ModifyDate=\"" + strCurrentTime + "\"");

                lngRes = clsTablService.add_new_record("ImproveGlasgowComaEvluation", p_strMainXml);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}

		[AutoComplete]
		public long m_lngGetImproveGlasgowComaValuation( string p_strPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			string strCommand="";
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            { 
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 *  from ImproveGlasgowComaEvluation a  " +
                        "  WHERE a.InPatientNO='" +
                        p_strPatientID + "'" + " AND a.Status =0 and a.ActivityTime=" +
                        clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " order by ModifyDate desc";
                else
                    strCommand = "select * from(select *  from ImproveGlasgowComaEvluation a  " +
                        "  WHERE a.InPatientNO='" +
                        p_strPatientID + "'" + " AND a.Status =0 and a.ActivityTime=" +
                        clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " order by ModifyDate desc)where rownum = 1";

                lngRes = clsTablService.lngGetXMLTable(strCommand, ref p_strXml, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}
	}
}
