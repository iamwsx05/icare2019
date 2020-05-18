using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService ;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.AssistantToolService
{
	/// <summary>
	/// 特殊符号中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsSpecialSymbolServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		#region SQL定义	

		// 从CommonUseValue中获取指定的表单。
		private const string c_strCheckNameSQL=@"select specialsymbolvalue from specialsymbol where deptid = ? and specialsymbolvalue=? ";
		
		// 设置CommonUseValue中删除记录的信息
		private const string c_strDeleteRecordSQL="delete from specialsymbol where deptid = ? and specialsymbolvalue=?";		
		
		
		#endregion SQL定义
		
		public clsSpecialSymbolServ()
		{}
		/// <summary>
		/// 获取科室使用的特殊符号
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_dtbResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllSpecialSymbolValue( string p_strDeptID,out DataTable p_dtbResult)
		{			
			p_dtbResult = null;

//			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsSpecialSymbolServ","m_lngGetAllSpecialSymbolValue");
//			//if(lngCheckRes <= 0)
//				//return lngCheckRes;
            clsHRPTableService objHRPServ = new clsHRPTableService();
			string strSQL = "select specialsymbolvalue from specialsymbol where deptid = ? order by valueindex";
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strDeptID.Trim();

            long lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]
		private long m_lngCheckSame(string p_strCheckDeptID,string p_strCheckName,string p_strCheckSQL)
		{
			//检查参数
			if(p_strCheckDeptID == null || p_strCheckName == null)
				return (long)enmOperationResult.Parameter_Error;
		   clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
            try
            {

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strCheckDeptID.Trim();
                objDPArr[1].Value = p_strCheckName;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(p_strCheckSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有相同的p_strCheckString。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    return (long)enmOperationResult.Record_Already_Exist;//重名
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
		private long m_lngCheckName(string p_strDeptID,string p_strSpecialSymbol)
		{			
			return m_lngCheckSame(p_strDeptID,p_strSpecialSymbol,c_strCheckNameSQL);
		}

		/// <summary>
		/// 保存记录到数据库。
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewRecord2DB( clsSpecialSymbolValue p_objRecordContent)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strDeptID == null || p_objRecordContent.m_strSpecialSymbolValue==null)
				return (long)enmOperationResult.Parameter_Error;			
		
			long lngRes=m_lngCheckName(p_objRecordContent.m_strDeptID,p_objRecordContent.m_strSpecialSymbolValue);
			if(lngRes<=0)return lngRes;
	      clsHRPTableService objHRPServ =new clsHRPTableService();
			lngRes = 0;
            try
            {
                string strSql = "";
                if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                      strSql = "select max(valueindex) + 1 from specialsymbol where deptid = ?";
                }
                else
                {
                      strSql = "select " + clsDatabaseSQLConvert.s_StrGetNullFuncName + @"(max(valueindex),0) + 1 from specialsymbol where deptid = ?";
                }
                DataTable dtResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strDeptID.Trim();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
                if (lngRes <= 0 || dtResult.Rows.Count == 0)
                {
                    return lngRes;
                }
                string strIndex = dtResult.Rows[0][0].ToString();
                int intIndex = 1;
                try
                {
                    intIndex = Int32.Parse(strIndex);
                }
                catch (Exception)
                {
                }
                
                strSql = "insert into specialsymbol(deptid,valueindex,specialsymbolvalue) values(?,?,?)";
                //			strSql = "execute P_INSERTSPECIALSYMBOL('"+p_objRecordContent.m_strDeptID+"','"+p_objRecordContent.m_strSpecialSymbolValue+"');";

                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strDeptID.Trim();
                objDPArr[1].Value = intIndex;
                objDPArr[2].Value = p_objRecordContent.m_strSpecialSymbolValue;

                //执行SQL			
                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff,objDPArr);
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
		
		/// <summary>
		/// 修改记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngModifyRecord2DB( clsSpecialSymbolValue p_objOldRecordContent,clsSpecialSymbolValue p_objNewRecordContent)
		{
			//检查参数
			if(p_objOldRecordContent==null || p_objOldRecordContent.m_strSpecialSymbolValue==null ||
				p_objNewRecordContent==null || p_objNewRecordContent.m_strDeptID==null || p_objNewRecordContent.m_strSpecialSymbolValue==null)
				return (long)enmOperationResult.Parameter_Error;
				
			long lngRes=m_lngDeleteRecord2DB( p_objOldRecordContent);
			if(lngRes<=0)return lngRes;

			lngRes=m_lngAddNewRecord2DB( p_objNewRecordContent);			
			return lngRes;
		}		
		
		/// <summary>
		/// 删除记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngDeleteRecord2DB( clsSpecialSymbolValue p_objRecordContent)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strDeptID==null || p_objRecordContent.m_strSpecialSymbolValue==null)
				return (long)enmOperationResult.Parameter_Error;
			
				clsHRPTableService objHRPServ =new clsHRPTableService();
			//获取IDataParameter数组
			IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];
			objHRPServ.CreateDatabaseParameter(2,out objDPArr);
			//按顺序给IDataParameter赋值
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
			objDPArr[0].Value=p_objRecordContent.m_strDeptID;
			objDPArr[1].Value=p_objRecordContent.m_strSpecialSymbolValue;
			
			//执行SQL
			long lngEff=0;
			long lngRes= objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);
            //objHRPServ.Dispose();
			return lngRes;
		}	
	}
}
