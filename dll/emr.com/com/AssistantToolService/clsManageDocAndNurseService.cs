using System;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Collections;
using System.EnterpriseServices;
//using System.Data;

namespace com.digitalwave.CommonUseServ
{
	/// <summary>
	/// 员工常用值和所有员工
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsManageDocAndNurseService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		#region 旧的签名常用值
		/// <summary>
		/// 获取特定部门下的员工。包括员工常用值和所有员工。
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_intFlag">员工类型：0为所有员工；1为医生；2为护士</param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_objArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetSpecialEmployeeInDept( int p_intFlag,string p_strDeptID,out clsDocAndNur[] p_objArr)
		{
			p_objArr = null;

			string strSql = "";

			switch(p_intFlag)
			{
				case 0 :
					strSql = @"select e.empno_chr as employeeid, e.lastname_vchr as employeename
								from t_bse_employee e
								inner join t_bse_deptemp d on e.empid_chr = d.empid_chr
								where d.deptid_chr = ?
								and e.status_int = 1";
					break;
				case 1 :
					strSql = @"select e.empno_chr as employeeid, e.lastname_vchr as employeename
								from t_bse_employee e
								inner join t_bse_deptemp d on e.empid_chr = d.empid_chr
								where d.deptid_chr = ?
								and e.status_int = 1
								and e.hasprescriptionright_chr = '1'";
					break;
				case 2 :
					strSql = @"select e.empno_chr as employeeid, e.lastname_vchr as employeename
								from t_bse_employee e
								inner join t_bse_deptemp d on e.empid_chr = d.empid_chr
								where d.deptid_chr = ?
								and e.status_int = 1
								and e.hasprescriptionright_chr = '0'";
					break;
			}
			
			DataTable dtResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strDeptID;

            long lngRes = objTabService.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
            //objTabService.Dispose();
			if(lngRes < 0) return lngRes;

			int intCount = dtResult.Rows.Count;
			if(intCount > 0)
			{
				p_objArr = new clsDocAndNur[intCount];

				for(int i = 0;i < intCount;i++)
				{
					p_objArr[i] = new clsDocAndNur();
					p_objArr[i].m_strDeptID = p_strDeptID;
					p_objArr[i].m_strEmployeeID = dtResult.Rows[i]["EMPLOYEEID"].ToString().Trim();
					p_objArr[i].m_strEmployeeName = dtResult.Rows[i]["EMPLOYEENAME"].ToString().Trim();
				}
			}
			return 1;
		}


		[AutoComplete]
		public long m_lngGetSpecialEmployeeInDeptArr( int p_intFlag,string p_strEmpID,out clsDocAndNur[] p_objArr)
		{
			p_objArr = null;
			if(p_strEmpID == null || p_strEmpID == "") return -1;
			string strSql = "";

			switch(p_intFlag)
			{
				case 0 :
					strSql = @"select distinct e.empid_chr,e.empno_chr,e.lastname_vchr,d.deptid_chr from t_bse_employee e
inner join t_bse_deptemp d on e.empid_chr = d.empid_chr
inner join (select deptid_chr from t_bse_deptemp a inner join t_bse_employee b
on a.empid_chr = b.empid_chr where b.empno_chr = ?) dept
on d.deptid_chr = dept.deptid_chr
 where e.status_int = 1";
					break;
				case 1 :
					strSql = @"select distinct e.empid_chr,e.empno_chr,e.lastname_vchr,d.deptid_chr from t_bse_employee e
inner join t_bse_deptemp d on e.empid_chr = d.empid_chr
inner join (select deptid_chr from t_bse_deptemp a inner join t_bse_employee b
on a.empid_chr = b.empid_chr where b.empno_chr = ?) dept
on d.deptid_chr = dept.deptid_chr
 where e.status_int = 1 and e.hasprescriptionright_chr = '1'";
					break;
				case 2 :
					strSql = @"select distinct e.empid_chr,e.empno_chr,e.lastname_vchr,d.deptid_chr from t_bse_employee e
inner join t_bse_deptemp d on e.empid_chr = d.empid_chr
inner join (select deptid_chr from t_bse_deptemp a inner join t_bse_employee b
on a.empid_chr = b.empid_chr where b.empno_chr = ?) dept
on d.deptid_chr = dept.deptid_chr
 where e.status_int = 1 and e.hasprescriptionright_chr = '0'";
					break;
			}
			
			DataTable dtResult = new DataTable();
            clsHRPTableService objTabService = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objTabService.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strEmpID;

            long lngRes = objTabService.lngGetDataTableWithParameters(strSql, ref dtResult, objDPArr);
            //objTabService.Dispose();
			if(lngRes < 0) return lngRes;

			int intCount = dtResult.Rows.Count;
			if(intCount > 0)
			{
				p_objArr = new clsDocAndNur[intCount];

				for(int i = 0;i < intCount;i++)
				{
					p_objArr[i] = new clsDocAndNur();
					p_objArr[i].m_strDeptID = dtResult.Rows[i]["deptid_chr"].ToString().Trim();;
					p_objArr[i].m_strEmployeeID = dtResult.Rows[i]["empno_chr"].ToString().Trim();
					p_objArr[i].m_strEmployeeName = dtResult.Rows[i]["lastname_vchr"].ToString().Trim();
				}
			}
			return 1;
		}

		[AutoComplete]
		public long m_lngAssignDocAndNur( clsDocAndNur[] p_objArr)
		{
			if(p_objArr == null || p_objArr.Length < 1) return 0;

			ArrayList arlInsert = new ArrayList();
			ArrayList arlUpdate = new ArrayList();

			clsHRPTableService objHRPServ = new clsHRPTableService();
			string strSelect,strInsert;
			DataTable dtResult = new DataTable();
			long lngRes;

            IDataParameter[] objDPArr = null;
            long lngEff = -1;
			for(int i = 0;i < p_objArr.Length; i++)
			{
                strSelect = @"select deptid, employeeid, flag from docandnurindept
where (deptid = ?) and (employeeid = ?)";

				strInsert = @"insert into docandnurindept (deptid, employeeid, flag)
values (?,?,?)";

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objArr[i].m_strDeptID;
                objDPArr[1].Value = p_objArr[i].m_strEmployeeID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSelect, ref dtResult, objDPArr);
                if (lngRes < 0)
                {
            //objHRPServ.Dispose();
                    return lngRes;
                }
				if(dtResult.Rows.Count > 0)
				{
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objArr[i].m_strDeptID;
                    objDPArr[1].Value = p_objArr[i].m_strEmployeeID;
                    objDPArr[2].Value = p_objArr[i].m_intFlag;

					objHRPServ.lngExecuteParameterSQL(strInsert,ref lngEff ,objDPArr);
				}
				else
				{
					
				}

			}
            //objHRPServ.Dispose();
			return 1;
			
		}

        [AutoComplete]
        public long m_lngSave(  bool[] p_blnArr, clsDocAndNur[] p_objArr)
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            long lngEff = -1;
            IDataParameter[] objDPArr = null;
            for (int i = 0; i < p_blnArr.Length; i++)
            {
                if (p_blnArr[i])
                {
                    //					string strInsert = @"INSERT INTO DocAndNurInDept
                    //      (DeptID, EmployeeID, Flag)
                    //VALUES ('"+p_objArr[i].m_strDeptID+"','"+p_objArr[i].m_strEmployeeID+"','"+p_objArr[i].m_intFlag+"')";
                    string strInsert = @"update t_bse_employee
   set hasprescriptionright_chr = ? where empno_chr = ?";
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_objArr[i].m_intFlag;
                    objDPArr[1].Value = p_objArr[i].m_strEmployeeID;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strInsert,ref lngEff,objDPArr);
                    if (lngRes < 0)
                    {
                        //objHRPServ.Dispose();
                        return lngRes;
                    }
                }
                else
                {
                    //					string strRemove = @"Delete DocAndNurInDept where DeptID ='"+p_objArr[i].m_strDeptID+
                    //"' and EmployeeID = '"+p_objArr[i].m_strEmployeeID+"'";
                    string strRemove = @"update t_bse_employee
   set hasprescriptionright_chr = null where empno_chr = ?";

                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = p_objArr[i].m_strEmployeeID;

                    lngRes = objHRPServ.lngExecuteParameterSQL(strRemove,ref lngEff,objDPArr);
                    if (lngRes < 0)
                    {
                        //objHRPServ.Dispose();
                        return lngRes;
                    }
                }
            }
            //objHRPServ.Dispose();
            return lngRes;
        }
		#endregion

		#region 新签名常用值
		/// <summary>
		/// 获取员工签名
		/// 适用不指定部门
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_intFlag">员工类型：0为所有员工；1为医生；2为护士</param>
		/// <param name="p_objdt">返回表</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetEmployeeSignWithoutDept( int p_intFlag,out DataTable p_objdt)
		{
			long lngRes = 0;
            clsHRPTableService objTabService = new clsHRPTableService();
			p_objdt = null;
            try
            {
                string strSql = "";

                switch (p_intFlag)
                {
                    case 0:
                        strSql = @" select t.empno_chr,
												t.lastname_vchr,
												t.technicalrank_chr,
												t.pycode_chr,
												t.empid_chr,
												t.psw_chr,
												t.digitalsign_dta,
												t.technicallevel_chr
										from t_bse_employee t
										where t.status_int = 1
										order by t.technicallevel_chr desc";

                        break;
                    case 1:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
								from t_bse_employee t
								where t.status_int = 1
								and t.technicallevel_chr like '5%'
								order by t.technicallevel_chr desc";
                        break;
                    case 2:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
								from t_bse_employee t
								where t.status_int = 1
								and t.technicallevel_chr like '4%'
								order by t.technicallevel_chr desc";
                        break;
                }

                lngRes = objTabService.DoGetDataTable(strSql, ref p_objdt);
            }
            catch (Exception exp)
            {
                lngRes = 0;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(exp);
            }
            finally
            {
                //objTabService.Dispose();
            }

			
			return lngRes;
		}


		/// <summary>
		/// 获取员工签名
		/// 适用指定部门
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_intFlag">员工类型：0为所有员工；1为医生；2为护士</param>
		/// <param name="p_objdt">返回表</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetEmployeeSignWithDept( int p_intFlag,string p_strDeptID,out DataTable p_objdt)
		{
			long lngRes = 0;
			p_objdt = null;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                string strSql = "";

                switch (p_intFlag)
                {
                    case 0:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
									and t.empid_chr = d.empid_chr
									and d.deptid_chr = ?
									order by t.technicallevel_chr desc";

                        break;
                    case 1:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
									and t.technicallevel_chr like '5%'
									and t.empid_chr = d.empid_chr
									and d.deptid_chr = ?
									order by t.technicallevel_chr desc";
                        break;
                    case 2:
                        strSql = @"select t.empno_chr,
										t.lastname_vchr,
										t.technicalrank_chr,
										t.pycode_chr,
										t.empid_chr,
										t.psw_chr,
										t.digitalsign_dta,
										t.technicallevel_chr
									from t_bse_employee t, t_bse_deptemp d
									where t.status_int = 1
									and t.technicallevel_chr like '4%'
									and t.empid_chr = d.empid_chr
									and d.deptid_chr = ?
									order by t.technicallevel_chr desc";
                        break;
                }
                IDataParameter[] objDPArr = null;
                objTabService.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeptID.Trim();

                lngRes = objTabService.lngGetDataTableWithParameters(strSql, ref p_objdt, objDPArr);
            }
            catch (Exception exp)
            {
                lngRes = 0;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(exp);
            }
            finally
            {
                //objTabService.Dispose();
            }
			
			return lngRes;
		}

		#endregion

	}
}
