using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService ;
using System.Data;
using weCare.Core.Entity; 
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.CommonUseServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsCommonUseServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		#region SQL定义	

		// 从CommonUseValue中获取指定的表单。
		private const string c_strCheckNameSQL=@"select commonusevalue from commonusevalue where typeid = ? and deptid = ? and commonusevalue=? and status=0 ";
		
		// 设置CommonUseValue中删除记录的信息
		private const string c_strDeleteRecordSQL="update commonusevalue set status=1 where typeid=? and deptid = ? and commonusevalue=? and status=0";		
		
		#endregion SQL定义

		public clsCommonUseServ()
		{}
		[AutoComplete]
		public long m_lngGetAllCommonUseType( out DataTable p_dtbResult)
		{		
			p_dtbResult=null;
            clsHRPTableService objHRPServ =new clsHRPTableService(); 

			string strSQL = "select distinct typeid,typename from commonusetype";
			long lngRes= objHRPServ.DoGetDataTable(strSQL ,ref p_dtbResult);
            //objHRPServ.Dispose();
            return lngRes;
		}
		[AutoComplete]
		public long m_lngGetAllCommonUseValue( string p_strCommonUseTypeID,string p_strDeptID,out DataTable p_dtbResult)
		{			
			p_dtbResult=null;
clsHRPTableService objHRPServ =new clsHRPTableService();  
			string strSQL = "select commonusevalue from commonusevalue where typeid=? and deptid =? and status=0 order by valueindex";

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = long.Parse(p_strCommonUseTypeID);
            objDPArr[1].Value = p_strDeptID;

            long lng = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            //objHRPServ.Dispose();
            return lng;

		}
			
		[AutoComplete]
		private long m_lngCheckSame(string p_strCheckID,string p_strDeptID,string p_strCheckName,
			string p_strCheckSQL)
		{
			//检查参数
			if(p_strCheckID == null || p_strDeptID == null || p_strCheckName == null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
	        clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strCheckID;
                objDPArr[1].Value = p_strDeptID;
                objDPArr[2].Value = p_strCheckName;

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
		private long m_lngCheckName(string p_strTypeID,string p_strDeptID,string p_strCommonUseValue)
		{			
			return m_lngCheckSame(p_strTypeID,p_strDeptID,p_strCommonUseValue,c_strCheckNameSQL);
		}
		
		/// <summary>
		/// 保存记录到数据库。
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngAddNewRecord2DB( clsCommonUseValue p_objRecordContent)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strTypeID==null || p_objRecordContent.m_strDeptID == null || p_objRecordContent.m_strCommonUseValue==null)
				return (long)enmOperationResult.Parameter_Error;	
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
            try
            {

                //			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCommonUseServ","m_lngAddNewRecord2DB");
                //			//if(lngCheckRes <= 0)
                //				////return lngCheckRes;

                lngRes = m_lngCheckName(p_objRecordContent.m_strTypeID, p_objRecordContent.m_strDeptID, p_objRecordContent.m_strCommonUseValue);
                if (lngRes <= 0) return lngRes;
                #region
                // 添加记录到CommonUseValue
                //			string strAddNewRecordSQL= 	@"declare intIndex number;
                //begin
                //select Max(ValueIndex) into intIndex from CommonUseValue
                //where TypeID='"+p_objRecordContent.m_strTypeID+ 
                //@"' and DeptID = '"+p_objRecordContent.m_strDeptID+@"';
                //if intIndex is null then
                //intIndex := 0;
                //end if;
                //intIndex := intIndex+1;
                //insert into CommonUseValue(TypeID,DeptID,ValueIndex,CommonUseValue,Status)
                //values('"+p_objRecordContent.m_strTypeID+@"','"+p_objRecordContent.m_strDeptID+"',intIndex,'"+p_objRecordContent.m_strCommonUseValue+@"','0');
                //end;";
                #endregion
                string strSql = "select " + clsDatabaseSQLConvert.s_StrGetNullFuncName + @"(max(valueindex),0) + 1 from commonusevalue where typeid='" + p_objRecordContent.m_strTypeID + "' and deptid = '" + p_objRecordContent.m_strDeptID + "'";
                DataTable dtResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSql, ref dtResult);
                if (lngRes <= 0 || dtResult.Rows.Count == 0)
                {
                    return lngRes;
                }
                string strIndex = dtResult.Rows[0][0].ToString();
                strSql = "insert into commonusevalue(typeid,deptid,valueindex,commonusevalue,status) values(?,?,?,?,'0')";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = long.Parse(p_objRecordContent.m_strTypeID);
                objDPArr[1].Value = p_objRecordContent.m_strDeptID;
                objDPArr[2].Value = int.Parse(strIndex);
                objDPArr[3].Value = p_objRecordContent.m_strCommonUseValue;

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
		public  long m_lngModifyRecord2DB( clsCommonUseValue p_objOldRecordContent,clsCommonUseValue p_objNewRecordContent)
		{
			//检查参数
			if(p_objOldRecordContent==null || p_objOldRecordContent.m_strCommonUseValue==null ||
				p_objNewRecordContent==null || p_objNewRecordContent.m_strTypeID==null || p_objNewRecordContent.m_strCommonUseValue==null)
				return (long)enmOperationResult.Parameter_Error;
				
			long lngRes=m_lngDeleteRecord2DB( p_objOldRecordContent);
            if (lngRes > 0)
            {

                lngRes = m_lngAddNewRecord2DB(  p_objNewRecordContent);
            }
			return lngRes;
		}		
		
		/// <summary>
		/// 删除记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public  long m_lngDeleteRecord2DB( clsCommonUseValue p_objRecordContent)
		{
			//检查参数                              
			if(p_objRecordContent==null || p_objRecordContent.m_strTypeID==null || p_objRecordContent.m_strCommonUseValue==null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //按顺序给IDataParameter赋值
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_objRecordContent.m_strTypeID;
                objDPArr[1].Value = p_objRecordContent.m_strDeptID;
                objDPArr[2].Value = p_objRecordContent.m_strCommonUseValue;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

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
