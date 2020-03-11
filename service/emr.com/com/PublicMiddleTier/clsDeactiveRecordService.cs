using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.DeactiveRecordService
{
	/// <summary>
	/// Summary description for clsDeactiveRecordService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDeactiveRecordService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
        private const string c_strGetDeactiveFormInfo = @"select form_id,
       form_desc,
       mainformclassname,
       maintablename,
       createdatename,
       primarydatename
  from deactiveforminfo
 order by form_desc";		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objFormInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDeactiveFormInfo(out clsDeactiveFormInfo[] p_objFormInfoArr)
		{
			p_objFormInfoArr = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDomainUserLoginInfoServ", "m_lngAddDomainUserLoginInfo");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                DataTable objDataTableResult = new DataTable();

                lngRes = objHRPServ.DoGetDataTable(c_strGetDeactiveFormInfo, ref objDataTableResult);

                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objFormInfoArr = new clsDeactiveFormInfo[objDataTableResult.Rows.Count];
                    for (int i = 0; i < objDataTableResult.Rows.Count; i++)
                    {
                        p_objFormInfoArr[i] = new clsDeactiveFormInfo();
                        p_objFormInfoArr[i].m_intFormID = int.Parse(objDataTableResult.Rows[i]["FORM_ID"].ToString());
                        p_objFormInfoArr[i].m_strFormName = objDataTableResult.Rows[i]["FORM_DESC"].ToString();
                        p_objFormInfoArr[i].m_strMainFormClassName = objDataTableResult.Rows[i]["MAINFORMCLASSNAME"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//·µ»Ø
			return lngRes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_intFormID"></param>
		/// <param name="p_objDeactiveInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDeactiveInfo(string p_strInPatientID,string p_strInPatientDate,int p_intFormID,out clsDeactiveInfo [] p_objDeactiveInfoArr)
		{
			p_objDeactiveInfoArr = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsDomainUserLoginInfoServ", "m_lngAddDomainUserLoginInfo");
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                string strGetFormInfo = @"select maintablename,createdatename,primarydatename
											from deactiveforminfo
											where form_id = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_intFormID;

                DataTable objDataTableResult = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetFormInfo, ref objDataTableResult, objDPArr);

                if (lngRes > 0 && objDataTableResult.Rows.Count == 1)
                {
                    string strTableName = objDataTableResult.Rows[0]["MAINTABLENAME"].ToString();
                    string strCreateDateName = objDataTableResult.Rows[0]["CREATEDATENAME"].ToString();
                    string strPrimaryDateName = objDataTableResult.Rows[0]["PRIMARYDATENAME"].ToString();

                    string strGetFormDeactiveInfo = @"select ebi.lastname_vchr as deactiveusername,base.*
														from t_bse_employee ebi inner join
														(select distinct deactiveddate,deactivedoperatorid," + strCreateDateName + @" as createdate," + strPrimaryDateName + @" as primarydate, inpatientdate
														from " + strTableName + @"
														where inpatientid = ?
														and status =1
														)base
														on base.deactivedoperatorid = ebi.empno_chr
														order by base.createdate";

                    IDataParameter[] objDPArr1 = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr1);
                    objDPArr1[0].Value = p_strInPatientID;

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strGetFormDeactiveInfo, ref objDataTableResult, objDPArr1);

                    if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                    {
                        p_objDeactiveInfoArr = new clsDeactiveInfo[objDataTableResult.Rows.Count];
                        for (int i = 0; i < objDataTableResult.Rows.Count; i++)
                        {
                            p_objDeactiveInfoArr[i] = new clsDeactiveInfo();
                            p_objDeactiveInfoArr[i].m_dtmCreateDate = (DateTime)objDataTableResult.Rows[i]["CREATEDATE"];
                            p_objDeactiveInfoArr[i].m_dtmDeactiveDate = (DateTime)objDataTableResult.Rows[i]["DEACTIVEDDATE"];
                            p_objDeactiveInfoArr[i].m_dtmPrimaryDate = (DateTime)objDataTableResult.Rows[i]["PRIMARYDATE"];
                            p_objDeactiveInfoArr[i].m_strDeactiveUserName = objDataTableResult.Rows[i]["DEACTIVEUSERNAME"].ToString().Trim();
                            p_objDeactiveInfoArr[i].m_dtmSelectedInDate = Convert.ToDateTime(objDataTableResult.Rows[i]["inpatientdate"].ToString());
                        }
                    }
                }
                else
                    lngRes = 0;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//·µ»Ø
			return lngRes;

		}
	}
}
