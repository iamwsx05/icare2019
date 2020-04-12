using System;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;

using System.EnterpriseServices;

namespace com.digitalwave.CommonUseServ
{
	/// <summary>
	/// 操作默认值的中间层
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDefaultValueService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		private const string c_strAdd = @"insert into defaultvalue
(deptid,area_id,formname, controlname, content)
values (?,?,?,?,?)";

		private const string c_strDel = @"delete from defaultvalue
where deptid = ? and area_id = ? and formname = ?";

        private const string c_strGet = @"select deptid, area_id, formname, controlname, content from defaultvalue
where deptid = ? and area_id = ? and formname = ?";

		[AutoComplete]
		public long m_lngSaveDefaultValue( clsCustomDefaultValue[] p_objArr)
		{
			if(p_objArr == null || p_objArr.Length ==0)
				return 0;

			long lngEff = 0;
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {

                if (m_blnHasExist(p_objArr[0].m_strDeptID, p_objArr[0].m_strAreaID, p_objArr[0].m_strFormName))
                {
                    IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    //					for(int j = 0; j < objDPArr.Length; j++)
                    //						objDPArr[j] = new Oracle.DataAccess.Client.OracleParameter();
                    objDPArr[0].Value = p_objArr[0].m_strDeptID;
                    objDPArr[1].Value = p_objArr[0].m_strAreaID;
                    objDPArr[2].Value = p_objArr[0].m_strFormName;

                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strDel, ref lngEff, objDPArr);
                    if (lngRes < 0)
                    {
                        return lngRes;
                    }
                }

                for (int i = 0; i < p_objArr.Length; i++)
                {
                    //最好不要拼字符串，因为用户可能会输入"'"delete等可能会影响到数据库的内容
                    //					string strSql = @"";
                    IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                    objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                    //					for(int j = 0; j < objDPArr.Length; j++)
                    //						objDPArr[j] = new Oracle.DataAccess.Client.OracleParameter();
                    objDPArr[0].Value = p_objArr[i].m_strDeptID;
                    objDPArr[1].Value = p_objArr[i].m_strAreaID;
                    objDPArr[2].Value = p_objArr[i].m_strFormName;
                    objDPArr[3].Value = p_objArr[i].m_strControlName;
                    objDPArr[4].Value = p_objArr[i].m_strContent;

                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strAdd, ref lngEff, objDPArr);
                    if (lngRes < 0)
                    {
                        return lngRes;
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

		[AutoComplete]
		private bool m_blnHasExist(string p_strDeptID,string p_strAreaID,string p_strFormName)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //			for(int j = 0; j < objDPArr.Length; j++)
                //				objDPArr[j] = new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].Value = p_strAreaID;
                objDPArr[2].Value = p_strFormName;

                DataTable dtResult = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGet, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    return true;
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
			return false;
		}

		/// <summary>
		/// 获取默认值
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_strFormName"></param>
		/// <param name="p_objArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDefaultValue( string p_strDeptID,string p_strAreaID,string p_strFormName,out clsCustomDefaultValue[] p_objArr)
		{
			p_objArr = null;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);
				//			for(int j = 0; j < objDPArr.Length; j++)
				//				objDPArr[j] = new Oracle.DataAccess.Client.OracleParameter();
				objDPArr[0].Value = p_strDeptID;
				objDPArr[1].Value = p_strAreaID;
				objDPArr[2].Value = p_strFormName;

				DataTable  dtResult = new DataTable();
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGet,ref dtResult,objDPArr);
				int intCount = dtResult.Rows.Count;
				if(lngRes > 0 && intCount > 0)
				{
					p_objArr = new clsCustomDefaultValue[intCount];
                    DataRow objRow = null;
					for(int i = 0; i < intCount; i++)
					{
                        objRow = dtResult.Rows[i];
                        clsCustomDefaultValue obj = new clsCustomDefaultValue();
                        obj.m_strDeptID = objRow["DEPTID"].ToString().Trim();
                        obj.m_strAreaID = objRow["AREA_ID"].ToString().Trim();
                        obj.m_strFormName = objRow["FORMNAME"].ToString();
                        obj.m_strControlName = objRow["CONTROLNAME"].ToString();
                        obj.m_strContent = objRow["CONTENT"].ToString();
                        p_objArr[i] = obj;
					}
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogDetailError(objEx,true);
			}
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}
	}
}
