using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.clsEmployee_BaseInfoService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsEmployee_BaseInfoService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
//		private const string c_strGetNewIDSQL="select nvl(MAX(EmployeeID),'9900000') AS EmployeeID from EmployeeBaseInfo where EmployeeID LIKE '99%'";

		/// <summary>
		/// 添加记录到EmployeeBaseInfo
		/// </summary>
		private const string c_strAddNewRecordSQL= @"insert into  employeebaseinfo(employeeid,begindate,firstname,idcard,sex,titleofatechnicalpost,officephone,homephone,mobile,officeaddress,homeaddress,officepc,homepc,email,status) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
		/// <summary>
		/// 添加记录到Dept_Employee
		/// </summary>
//		private const string c_strAddNewRecordSQL_Dept_Employee= @"insert into  Dept_Employee(DeptID,EmployeeID,ModifyDate,EndDate) 
//				values(?,?,?,"+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")";

		/// <summary>
		/// 添加记录到Dept_Employee
		/// </summary>
		private const string c_strAddNewRecordSQL_EmployeePSW= @"insert into  employeepsw(employeeid,opendate,loginname,psw,status) 
				values(?,?,?,?,?)";

		/// <summary>
		/// 修改记录到EmployeeBaseInfo
		/// </summary>
		private const string c_strModifyRecordSQL= "update employeebaseinfo set firstname=?,idcard=? ,sex=?,titleofatechnicalpost=? , officephone=? , homephone=? , mobile=? , officeaddress=?,homeaddress=? ,officepc=? ,homepc=?,email=? where  employeeid =? and status=0";

		/// <summary>
		/// 设置EmployeeBaseInfo中删除记录的信息
		/// </summary>
		private const string c_strDeleteRecordSQL= "update employeebaseinfo set status=1,deactivedate=?,operatorid=? where  employeeid =? and status=0";


		/// <summary>
		/// 添加新记录。
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewRecord( clsEmployee_BaseInfo p_objRecordContent,out string p_strNewEmployeeID)
		{
			p_strNewEmployeeID="";
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            { 
                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;


                lngRes = m_lngAddNewRecord2DB(p_objRecordContent, objHRP, out p_strNewEmployeeID);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();
            }
			//返回
			return lngRes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objRecordContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyRecord( clsEmployee_BaseInfo p_objRecordContent)
		{
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            { 
                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;


                lngRes = m_lngModifyRecord2DB(p_objRecordContent, objHRP);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();
            }
			//返回
			return lngRes;
			
		}
		
		[AutoComplete]
		public long m_lngDeleteRecord( clsEmployee_BaseInfo p_objRecordContent)
		{
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            { 
                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;


                lngRes = m_lngDeleteRecord2DB(p_objRecordContent, objHRP);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();
            }
			//返回
			return lngRes;
		}

		/// <summary>
		/// 保存记录到数据库。
		/// </summary>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngAddNewRecord2DB(clsEmployee_BaseInfo p_objRecordContent,
			clsHRPTableService p_objHRPServ,out string strNewID)
		{		
			 string c_strGetNewIDSQL=null;
			strNewID=null;
			if (clsHRPTableService.bytDatabase_Selector==0)
			{
				c_strGetNewIDSQL="select isnull(max(employeeid),'9900000') as employeeid from employeebaseinfo where employeeid like '99%'";
			}
			else
			{
				c_strGetNewIDSQL="select nvl(max(employeeid),'9900000') as employeeid from employeebaseinfo where employeeid like '99%'";
			
			}
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                strNewID = "";
                //执行查询，填充结果到DataTable
                lngRes = objTabService.DoGetDataTable(c_strGetNewIDSQL, ref dtbValue);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    strNewID = dtbValue.Rows[0]["EMPLOYEEID"].ToString();
                    if (strNewID == "")
                        strNewID = "9900000";
                    strNewID = (long.Parse(strNewID) + 1).ToString();
                }
                //返回
                else return lngRes;

                //				//获取IDataParameter数组			
                //				IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[15];
                //				//按顺序给IDataParameter赋值
                //				for(int i=0;i<objDPArr.Length;i++)
                //					objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                //				clsHRPTableService p_objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(15, out objDPArr);

                objDPArr[0].Value = strNewID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[2].Value = p_objRecordContent.m_strFirstName;
                objDPArr[3].Value = p_objRecordContent.m_strIDCard;
                objDPArr[4].Value = p_objRecordContent.m_strSex;
                objDPArr[5].Value = p_objRecordContent.m_strTitleOfaTechnicalPost;
                objDPArr[6].Value = p_objRecordContent.m_strOfficePhone;
                objDPArr[7].Value = p_objRecordContent.m_strHomePhone;
                objDPArr[8].Value = p_objRecordContent.m_strMobile;
                objDPArr[9].Value = p_objRecordContent.m_strOfficeAddress;
                objDPArr[10].Value = p_objRecordContent.m_strHomeAddress;
                objDPArr[11].Value = p_objRecordContent.m_strOfficePC;
                objDPArr[12].Value = p_objRecordContent.m_strHomePC;
                objDPArr[13].Value = p_objRecordContent.m_strEMail;
                objDPArr[14].Value = 0;
                for (int i = 0; i < objDPArr.Length; i++)
                    if (objDPArr[i].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //
                //				//获取IDataParameter数组			
                //				objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                //				//按顺序给IDataParameter赋值
                //				for(int i=0;i<objDPArr.Length;i++)
                //					objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                IDataParameter[] objDPArr1 = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr1);

                objDPArr1[0].Value = p_objRecordContent.m_strDeptID;
                objDPArr1[1].Value = strNewID;
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                for (int i = 0; i < objDPArr1.Length; i++)
                    if (objDPArr1[i].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }

                //执行SQL
                string c_strAddNewRecordSQL_Dept_Employee = @"insert into  dept_employee(deptid,employeeid,modifydate,enddate) 
					values(?,?,?," + clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat() + @")";

                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Dept_Employee, ref lngEff, objDPArr1);
                if (lngRes <= 0) return lngRes;

                //				//获取IDataParameter数组			
                //				objDPArr = new Oracle.DataAccess.Client.OracleParameter[5];
                //				//按顺序给IDataParameter赋值
                //				for(int i=0;i<objDPArr.Length;i++)
                //					objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr2);

                objDPArr2[0].Value = strNewID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr2[2].Value = strNewID;
                objDPArr2[3].Value = "";
                objDPArr2[4].Value = "0";
                for (int i = 0; i < objDPArr2.Length; i++)
                    if (objDPArr2[i].Value == null)
                    {
                        return (long)enmOperationResult.Parameter_Error;
                    }

                //执行SQL
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_EmployeePSW, ref lngEff, objDPArr2);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			//返回
			return lngRes;

		}

		
		/// <summary>
		/// 把新修改的内容保存到数据库。
		/// </summary>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		private  long m_lngModifyRecord2DB(clsEmployee_BaseInfo p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//获取IDataParameter数组			
//			IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[13];
//			//按顺序给IDataParameter赋值
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
//			clsHRPTableService p_objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			p_objHRPServ.CreateDatabaseParameter(13,out objDPArr);

			objDPArr[0].Value=p_objRecordContent.m_strFirstName;
			objDPArr[1].Value=p_objRecordContent.m_strIDCard;
			objDPArr[2].Value=p_objRecordContent.m_strSex;
			objDPArr[3].Value=p_objRecordContent.m_strTitleOfaTechnicalPost;						
			objDPArr[4].Value=p_objRecordContent.m_strOfficePhone;
			objDPArr[5].Value=p_objRecordContent.m_strHomePhone;
			objDPArr[6].Value=p_objRecordContent.m_strMobile;
			objDPArr[7].Value=p_objRecordContent.m_strOfficeAddress;
			objDPArr[8].Value=p_objRecordContent.m_strHomeAddress;
			objDPArr[9].Value=p_objRecordContent.m_strOfficePC;
			objDPArr[10].Value=p_objRecordContent.m_strHomePC;
			objDPArr[11].Value=p_objRecordContent.m_strEMail;
			objDPArr[12].Value=p_objRecordContent.m_strEmployeeID;			
			for(int i=0;i<objDPArr.Length;i++)
				if(objDPArr[i].Value==null)
				{
					return (long)enmOperationResult.Parameter_Error;
				}
			
			//执行SQL
			long lngEff=0;
			long lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objDPArr);
            //p_objHRPServ.Dispose();
			return lngRes;	

		}

		/// <summary>
		/// 把记录从数据中“删除”。
		/// </summary>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngDeleteRecord2DB(clsEmployee_BaseInfo p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{			
//			IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
//			//按顺序给IDataParameter赋值
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
//			clsHRPTableService p_objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);

            objDPArr[0].DbType = DbType.DateTime;
			objDPArr[0].Value=DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			objDPArr[1].Value=p_objRecordContent.m_strDeActivedOperatorID;			
			objDPArr[2].Value=p_objRecordContent.m_strEmployeeID;
			for(int i=0;i<objDPArr.Length;i++)
				if(objDPArr[i].Value==null)
				{
					return (long)enmOperationResult.Parameter_Error;
				}
			//执行SQL
			long lngEff=0;
			return p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);

		}
	}
}
