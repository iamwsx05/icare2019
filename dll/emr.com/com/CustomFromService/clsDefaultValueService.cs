using System;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;

using System.EnterpriseServices;

namespace iCare.CustomFromService
{
	/// <summary>
	/// 操作默认值的中间层
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDefaultValueService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		private const string c_strAdd = @"insert into t_opr_defaultvalue
(deptid_chr,areaid_chr,pageid_chr,clsformname_vchr, controlname_vchr, defaultvalue)
values (?,?,?,?,?,?)";

		private const string c_strDel = @"delete from t_opr_defaultvalue
where deptid_chr = ? and areaid_chr = ? and clsformname_vchr = ? and pageid_chr = ?";

		private const string c_strGet = @"select * from t_opr_defaultvalue
where deptid_chr = ? and areaid_chr = ? and clsformname_vchr = ? and pageid_chr = ?";

		[AutoComplete]
		public long m_lngSaveDefaultValue(clsDefaultValue_VO p_objValue)
		{
			if(p_objValue == null)
				return -1;
			if(p_objValue.m_objDefaultValueArr == null || p_objValue.m_objDefaultValueArr.Length == 0)
				return -1;

			long lngEff = 0;
			long lngRes = 0;
				clsHRPTableService objHRPServ =new clsHRPTableService();
                try
                {
                    if (m_blnHasExist(p_objValue))
                    {
                        IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                        objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                        //					for(int j = 0; j < objDPArr.Length; j++)
                        //						objDPArr[j] = new Oracle.DataAccess.Client.OracleParameter();
                        objDPArr[0].Value = p_objValue.m_strDeptID;
                        objDPArr[1].Value = p_objValue.m_strAreaID;
                        objDPArr[2].Value = p_objValue.m_strCLSFormName;
                        objDPArr[3].Value = p_objValue.m_strPageID;

                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strDel, ref lngEff, objDPArr);
                        if (lngRes < 0) return lngRes;
                    }

                    for (int i = 0; i < p_objValue.m_objDefaultValueArr.Length; i++)
                    {
                        //最好不要拼字符串，因为用户可能会输入"'"delete等可能会影响到数据库的内容
                        //					string strSql = @"";
                        IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                        objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                        //					for(int j = 0; j < objDPArr.Length; j++)
                        //						objDPArr[j] = new Oracle.DataAccess.Client.OracleParameter();
                        objDPArr[0].Value = p_objValue.m_strDeptID;
                        objDPArr[1].Value = p_objValue.m_strAreaID;
                        objDPArr[2].Value = p_objValue.m_strPageID;
                        objDPArr[3].Value = p_objValue.m_strCLSFormName;
                        objDPArr[4].Value = p_objValue.m_objDefaultValueArr[i].m_strControlName;
                        objDPArr[5].Value = p_objValue.m_objDefaultValueArr[i].m_strDefaultValue;

                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAdd, ref lngEff, objDPArr);
                        if (lngRes < 0) return lngRes;
                    }
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
			return lngRes;
		}

		[AutoComplete]
		private bool m_blnHasExist(clsDefaultValue_VO p_objValue)
		{
			long lngRes = 0;
				clsHRPTableService objHRPServ =new clsHRPTableService();
                try
                {
                    IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    //			for(int j = 0; j < objDPArr.Length; j++)
                    //				objDPArr[j] = new Oracle.DataAccess.Client.OracleParameter();
                    objDPArr[0].Value = p_objValue.m_strDeptID;
                    objDPArr[1].Value = p_objValue.m_strAreaID;
                    objDPArr[2].Value = p_objValue.m_strCLSFormName;
                    objDPArr[3].Value = p_objValue.m_strPageID;

                    DataTable dtResult = new DataTable();
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGet, ref dtResult, objDPArr);
                    if (lngRes > 0 && dtResult.Rows.Count > 0)
                        return true;
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
			return false;
		}

		/// <summary>
		/// 获取默认值
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_strFormName"></param>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDefaultValue(string p_strDeptID,string p_strAreaID,string p_strFormName,string p_strPageID,out clsDefaultValue_VO p_objValue)
		{
			p_objValue = null;
			
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {
                DataTable dtResult = new DataTable();
                string SQL = @"Select * 
                                From T_OPR_DEFAULTVALUE
                                Where DEPTID_CHR = '" + p_strDeptID + @"' 
                                  AND AREAID_CHR = '" + p_strAreaID + @"'
                                  AND CLSFORMNAME_VCHR = '" + p_strFormName + @"'
                                  AND PAGEID_CHR = '" + p_strPageID + "'";
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(SQL, ref dtResult);

                //IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                ////			for(int j = 0; j < objDPArr.Length; j++)
                ////				objDPArr[j] = new Oracle.DataAccess.Client.OracleParameter();
                //objDPArr[0].Value = p_strDeptID;
                //objDPArr[1].Value = p_strAreaID;
                //objDPArr[2].Value = p_strFormName;
                //objDPArr[3].Value = p_strPageID;

                //DataTable dtResult = new DataTable();
                //lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGet, ref dtResult, objDPArr);
                int intCount = dtResult.Rows.Count;
                if (lngRes > 0 && intCount > 0)
                {
                    p_objValue = new clsDefaultValue_VO();
                    p_objValue.m_strDeptID = dtResult.Rows[0]["DEPTID_CHR"].ToString().Trim();
                    p_objValue.m_strAreaID = dtResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                    p_objValue.m_strCLSFormName = dtResult.Rows[0]["CLSFORMNAME_VCHR"].ToString().Trim();
                    p_objValue.m_strPageID = dtResult.Rows[0]["PAGEID_CHR"].ToString().Trim();
                    p_objValue.m_objDefaultValueArr = new clsDefaultValue_Control_VO[intCount];
                    for (int i = 0; i < intCount; i++)
                    {
                        p_objValue.m_objDefaultValueArr[i] = new clsDefaultValue_Control_VO();
                        p_objValue.m_objDefaultValueArr[i].m_strControlName = dtResult.Rows[i]["CONTROLNAME_VCHR"].ToString().Trim();
                        p_objValue.m_objDefaultValueArr[i].m_strDefaultValue = dtResult.Rows[i]["DEFAULTVALUE"].ToString().Trim();
                    }
                }
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
			return lngRes;
		}
	}
	
}
