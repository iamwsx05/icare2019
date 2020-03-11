using System;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using System.Collections;
using com.digitalwave.iCare.middletier.HRPService; 

namespace com.digitalwave.PrivilegeSystemService
{
    /// <summary>
    /// 权限系统的中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPrivilegeSystemService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        //private clsHRPTableService objHRPServ = new clsHRPTableService();

        #region sql
        /// <summary>
        /// 获取所有的Role，并且以RoleCategory排序
        /// </summary>
        private const string c_strGetAllRole = @"select rd.role_id,
       rd.createdate,
       rd.status,
       rd.deactiveddate,
       rd.operatorid,
       ri.modifydate,
       ri.role_name,
       ri.description,
       ri.category,
       ri.description as role_description
  from role_definition rd, role_info ri
 where rd.role_id = ri.role_id
   and rd.status = 0
 order by category desc";

        /// <summary>
        /// 根据员工ID获取权限
        /// </summary>
        private const string c_strGetPIByEmployeeID = @"select re.role_id,
       dsor.baseid,
       dsor.sf_id,
       dsor.operation_id,
       dsor.opendate,
       dsor.status,
       dsor.deactiveddate,
       dsor.operatorid,
       sd.description as sf_description,
       od.description as operation_description
  from employeebaseinfo       ebi,
       role_employee          re,
       dept_sf_operation_role dsor,
       sf_definition          sd,
       operation_definition   od
 where (ebi.employeeid) = ?
   and ebi.employeeid = re.employeeid
   and re.role_id = dsor.role_id
   and dsor.sf_id = sd.sf_id
   and dsor.operation_id = od.operation_id
   and re.status = 0
   and dsor.status = 0
 order by re.role_id";

        /// <summary>
        /// 根据角色ID获取权限
        /// </summary>
        private const string c_strGetPIByRoleID = @"select dsor.baseid,
       dsor.sf_id,
       dsor.operation_id,
       dsor.role_id,
       dsor.opendate,
       dsor.status,
       dsor.deactiveddate,
       dsor.operatorid,
       sd.description as sf_description,
       od.description as operation_description
  from dept_sf_operation_role dsor,
       sf_definition          sd,
       operation_definition   od
 where dsor.role_id = ?
   and dsor.sf_id = sd.sf_id
   and dsor.operation_id = od.operation_id
   and dsor.status = 0";


        /// <summary>
        /// 根据Role_ID获取该Role的信息
        /// </summary>
        private const string c_strGetRoleInfoByRoleID = @"select role_id, modifydate, role_name, description, category
  from role_info
 where role_id = ? ";

        /// <summary>
        /// 根据员工ID获取Roles
        /// </summary>
        private const string c_strGetRolesByEmployee = @"select ebi.employeeid,
       re.role_id,
       re.opendate,
       re.status,
       re.deactiveddate,
       re.operatorid,
       rd.createdate,
       rd.status,
       rd.deactiveddate,
       rd.operatorid,
       ri.modifydate,
       ri.role_name,
       ri.description,
       ri.category,
       ri.description as role_description
  from role_definition  rd,
       role_info        ri,
       role_employee    re,
       employeebaseinfo ebi
 where rtrim(ebi.employeeid) = ?
   and ebi.employeeid = re.employeeid
   and re.role_id = rd.role_id
   and rd.role_id = ri.role_id
   and re.status = 0
   and rd.status = 0
 order by rd.role_id";

        /// <summary>
        /// 根据分类获取Role
        /// </summary>
        private const string c_strGetRolesByCategory = @"select ri.role_id, ri.modifydate, ri.role_name, ri.description, ri.category
  from role_info ri
 where ri.category = ?
 order by role_id";

        /// <summary>
        /// 获取在Role中的员工
        /// </summary>
        private const string c_strGetEmployeesInRole = @"select rd.role_id,
       rd.createdate,
       rd.status,
       rd.deactiveddate,
       rd.operatorid,
       re.employeeid,
       re.opendate,
       re.status,
       re.deactiveddate,
       re.operatorid,
       re.employeeid as re_employeeid,
       ebi.begindate,
       ebi.firstname,
       ebi.lastname,
       ebi.idcard,
       ebi.pycode,
       ebi.sex,
       ebi.educationallevel,
       ebi.married,
       ebi.titleofatechnicalpost,
       ebi.languageability,
       ebi.birth,
       ebi.officephone,
       ebi.homephone,
       ebi.mobile,
       ebi.officeaddress,
       ebi.homeaddress,
       ebi.officepc,
       ebi.homepc,
       ebi.email,
       ebi.firstnameofannouncer,
       ebi.lastnameofannouncer,
       ebi.phoneofannouncer,
       ebi.experience,
       ebi.remark,
       ebi.status,
       ebi.deactivedate,
       ebi.operatorid,
       ebi.shortname
  from role_definition rd, role_employee re, employeebaseinfo ebi
 where rd.role_id = ?
   and rd.role_id = re.role_id
   and re.employeeid = ebi.employeeid
   and re.status = 0";

        /// <summary>
        /// 添加员工到Role
        /// </summary>
        private const string c_strAddEmployeeToRole = @"insert into  role_employee
														(role_id,employeeid,opendate,status)
														 values(?,?,?,?)";

        /// <summary>
        /// 从Role移去员工
        /// </summary>
        private const string c_strRemoveEmployeeFromRole = @"update role_employee 
															set status=1,deactiveddate=?,operatorid=? 
															where role_id=? 
															and rtrim(employeeid)=?														
															and status=0";

        /// <summary>
        /// 整批添加员工到Role
        /// </summary>
        private const string c_strBatchAddEmployeeToRole = c_strAddEmployeeToRole;

        /// <summary>
        /// 整批从Role中移去员工
        /// </summary>
        private const string c_strBatchRemoveEmployeeFromRole = c_strRemoveEmployeeFromRole;

        /// <summary>
        /// 根据员工ID所属的权限查找所有的部门
        /// </summary>
        private const string c_strGetDeptInRoleByEmployeeID = @"select re.role_id,rda.*,dd.deptname 
																from role_employee re,role_dept_assignment rda,dept_desc dd
																where rtrim(re.employeeid)=?
																and re.role_id = rda.role_id
																and rda.deptid = dd.deptid
																and rda.canview = 1";

        /// <summary>
        /// 根据员工ID获取其能在HRPExplorer树上见到的部门
        /// </summary>
        private const string c_strGetDeptByEmployeeID = @"select distinct baseid,deptname
														from dept_sf_operation_role dsor,dept_desc dd,role_employee re
														where rtrim(employeeid) =?
														and re.role_id = dsor.role_id
														and dsor.baseid = dd.deptid
														and re.status=0";
        #endregion


        /// <summary>
        ///  获取所有的Role，并且以RoleID排序
        /// </summary>
        /// <param name="p_objRoleArr">所有角色的数组</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllRole(out clsRole[] p_objRoleArr)
        {
            p_objRoleArr = null;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //执行查询，填充结果到DataTable
            long lngRes = objHRPServ.DoGetDataTable(c_strGetAllRole, ref dtbValue);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果

                p_objRoleArr = new  clsRole[dtbValue.Rows.Count];
                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                     clsRole objRole = new  clsRole();
                    objRole.m_strRoleID = dtbValue.Rows[i]["ROLE_ID"].ToString().Trim();
                    objRole.m_strRoleName = dtbValue.Rows[i]["ROLE_NAME"].ToString().Trim();//object转化成string
                    objRole.m_strRoleCategory = dtbValue.Rows[i]["CATEGORY"].ToString().Trim();
                    objRole.m_strRoleDesc = dtbValue.Rows[i]["ROLE_DESCRIPTION"].ToString().Trim();

                    p_objRoleArr[i] = objRole;
                }

                #endregion 从DataTable.Rows中获取结果

            }

            return (long)enmOperationResult.DB_Succeed;

        }

        /// <summary>
        /// 根据员工ID获取权限
        /// </summary>
        /// <param name="p_strRoleID"></param>
        /// <param name="p_objPI"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPIByEmployeeID(string p_strEmployeeID, out clsPrivilegeInfo p_objPI)
        {
            p_objPI = null;

            //检查参数                              
            if (p_strEmployeeID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //获取IDataParameter数组
            IDataParameter[] objDPArr;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strEmployeeID;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetPIByEmployeeID, ref dtbValue, objDPArr);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果

                //一个Role对应一个PI
                p_objPI = new clsPrivilegeInfo();

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    clsSystemFunction objSF = new clsSystemFunction();
                    objSF.m_intSFID = int.Parse(dtbValue.Rows[i]["SF_ID"].ToString());
                    objSF.m_strSFDesc = dtbValue.Rows[i]["SF_DESCRIPTION"].ToString();

                    clsOperation objOpt = new clsOperation();
                    objOpt.m_intOperationID = int.Parse(dtbValue.Rows[i]["OPERATION_ID"].ToString());
                    objOpt.m_strOperationDesc = dtbValue.Rows[i]["OPERATION_DESCRIPTION"].ToString();

                    clsOISF objOISF = new clsOISF();
                    objOISF.m_objSF = objSF;
                    objOISF.m_objOperation = objOpt;
                    objOISF.m_strBaseID = dtbValue.Rows[i]["BASEID"].ToString();

                    p_objPI.m_mthAddOISF(objOISF);
                }
                #endregion 从DataTable.Rows中获取结果						

            }

            return lngRes;

        }

        /// <summary>
        /// 根据角色ID获取权限
        /// </summary>
        /// <param name="p_strRoleID"></param>
        /// <param name="p_objPI"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPIByRoleID(string p_strRoleID, out clsPrivilegeInfo p_objPI)
        {
            p_objPI = null;

            //检查参数                              
            if (p_strRoleID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //获取IDataParameter数组
            IDataParameter[] objDPArr;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strRoleID;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetPIByRoleID, ref dtbValue, objDPArr);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果

                //一个Role对应一个PI
                p_objPI = new clsPrivilegeInfo();

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    clsSystemFunction objSF = new clsSystemFunction();
                    objSF.m_intSFID = int.Parse(dtbValue.Rows[i]["SF_ID"].ToString());
                    objSF.m_strSFDesc = dtbValue.Rows[i]["SF_DESCRIPTION"].ToString();

                    clsOperation objOpt = new clsOperation();
                    objOpt.m_intOperationID = int.Parse(dtbValue.Rows[i]["OPERATION_ID"].ToString());
                    objOpt.m_strOperationDesc = dtbValue.Rows[i]["OPERATION_DESCRIPTION"].ToString();

                    clsOISF objOISF = new clsOISF();
                    objOISF.m_objSF = objSF;
                    objOISF.m_objOperation = objOpt;
                    objOISF.m_strBaseID = dtbValue.Rows[i]["BASEID"].ToString().Trim();


                    p_objPI.m_mthAddOISF(objOISF);
                }
                #endregion 从DataTable.Rows中获取结果						

            }

            return lngRes;

        }

        /// <summary>
        /// 根据角色ID获取权限(批量处理) 
        /// </summary>
        /// <param name="p_arrRoleIDs"></param>
        /// <param name="p_arrPIs"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPIBySomeRoleIDs(string[] p_arrRoleIDs, out clsPrivilegeInfo[] p_arrPIs)
        {
            p_arrPIs = null;

            //检查参数                              
            if (p_arrRoleIDs == null) return (long)enmOperationResult.Parameter_Error;

            //sql
            string strSql = @"select dsor.baseid,
       dsor.sf_id,
       dsor.operation_id,
       dsor.role_id,
       dsor.opendate,
       dsor.status,
       dsor.deactiveddate,
       dsor.operatorid,
       sd.description as sf_description,
       od.description as operation_description
  from dept_sf_operation_role dsor,
       sf_definition          sd,
       operation_definition   od
 where dsor.role_id in ( [RoleIDArrayValue] )
   and dsor.sf_id = sd.sf_id
   and dsor.operation_id = od.operation_id
   and dsor.status = 0
 order by dsor.role_id";
            //condition
            string strRoleIDs = "";
            bool blnIsFirst = true;
            for (int i = 0; i < p_arrRoleIDs.Length; i++)
            {
                if (p_arrRoleIDs[i] == null) continue;
                if (!blnIsFirst) strRoleIDs = strRoleIDs + ",";

                strRoleIDs = strRoleIDs + "'" + p_arrRoleIDs[i] + "' ";

                blnIsFirst = false;
            }

            strSql = strSql.Replace("[RoleIDArrayValue]", strRoleIDs);
            clsHRPTableService objHRPServ = new clsHRPTableService();

            //执行查询，填充结果到DataTable
            DataTable dtbValue = new DataTable();
            long lngRes = objHRPServ.DoGetDataTable(strSql, ref dtbValue);
            //objHRPServ.Dispose();
            if ((lngRes > 0) && (dtbValue.Rows.Count > 0))
            {
                ArrayList arlPIs = new ArrayList();

                string strCurRoleID = "";
                clsPrivilegeInfo objCurPI = null;

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    string strRoleID = dtbValue.Rows[i]["ROLE_ID"].ToString();
                    if ((strRoleID.Trim() != strCurRoleID.Trim()) || (objCurPI == null))
                    {
                        objCurPI = new clsPrivilegeInfo();
                        arlPIs.Add(objCurPI);

                        strCurRoleID = strRoleID;
                    }


                    //										
                    clsSystemFunction objSF = new clsSystemFunction();
                    objSF.m_intSFID = int.Parse(dtbValue.Rows[i]["SF_ID"].ToString());
                    objSF.m_strSFDesc = dtbValue.Rows[i]["SF_DESCRIPTION"].ToString();

                    clsOperation objOpt = new clsOperation();
                    objOpt.m_intOperationID = int.Parse(dtbValue.Rows[i]["OPERATION_ID"].ToString());
                    objOpt.m_strOperationDesc = dtbValue.Rows[i]["OPERATION_DESCRIPTION"].ToString();

                    clsOISF objOISF = new clsOISF();
                    objOISF.m_objSF = objSF;
                    objOISF.m_objOperation = objOpt;
                    objOISF.m_strBaseID = dtbValue.Rows[i]["BASEID"].ToString();


                    objCurPI.m_mthAddOISF(objOISF);
                }

                p_arrPIs = (clsPrivilegeInfo[])(arlPIs.ToArray(typeof(clsPrivilegeInfo)));

            }

            return lngRes;
        }

        /// <summary>
        /// 根据Role_ID获取该角色的信息
        /// </summary>
        /// <param name="p_strRoleID">Role_ID</param>
        /// <param name="p_objRole">角色的类</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleInfoByRoleID(string p_strRoleID, out clsRole p_objRole)
        {
            p_objRole = null;

            //检查参数                              
            if (p_strRoleID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //获取IDataParameter数组
            IDataParameter[] objDPArr;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strRoleID;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRoleInfoByRoleID, ref dtbValue, objDPArr);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果

                p_objRole = new  clsRole();

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objRole.m_strRoleID = dtbValue.Rows[i]["ROLE_ID"].ToString();
                    p_objRole.m_strRoleName = dtbValue.Rows[i]["ROLE_NAME"].ToString();
                    p_objRole.m_strRoleCategory = dtbValue.Rows[i]["CATEGORY"].ToString();
                    p_objRole.m_strRoleDesc = dtbValue.Rows[i]["DESCRIPTION"].ToString();
                }

                #endregion 从DataTable.Rows中获取结果						

            }

            return lngRes;
        }

        /// <summary>
        /// 根据员工ID获取Roles
        /// </summary>
        /// <param name="p_strEmployeeID">员工ID</param>
        /// <param name="p_objRoleArr">员工属于哪些角色</param>
        /// <returns></returns> 
        [AutoComplete]
        public long m_lngGetRolesByEmployee(string p_strEmployeeID, out clsRole[] p_objRoleArr)
        {
            p_objRoleArr = null;

            //检查参数                              
            if (p_strEmployeeID == null)
                return (long)enmOperationResult.Parameter_Error;

            //			IDataParameter[] objDPArr;
            //			objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            //			objDPArr[0].Value=p_strEmployeeID;	
            clsHRPTableService objHRPServ = new clsHRPTableService();

            string strGetRolesByEmployee = @"select ebi.employeeid,
       re.role_id,
       re.opendate,
       re.status,
       re.deactiveddate,
       re.operatorid,
       rd.createdate,
       rd.status,
       rd.deactiveddate,
       rd.operatorid,
       ri.modifydate,
       ri.role_name,
       ri.description,
       ri.category,
       ri.description as role_description
  from role_definition  rd,
       role_info        ri,
       role_employee    re,
       employeebaseinfo ebi
 where ebi.employeeid = ?
   and ebi.employeeid = re.employeeid
   and re.role_id = rd.role_id
   and rd.role_id = ri.role_id
   and re.status = 0
   and rd.status = 0
 order by rd.role_id";

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strEmployeeID;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            //			long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRolesByEmployee,ref dtbValue,objDPArr);
            long lngRes = objHRPServ.lngGetDataTableWithParameters(strGetRolesByEmployee, ref dtbValue, objDPArr);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果

                p_objRoleArr = new  clsRole[dtbValue.Rows.Count];
                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    clsRole objRole = new  clsRole();
                    objRole.m_strRoleID = dtbValue.Rows[i]["ROLE_ID"].ToString().Trim();
                    objRole.m_strRoleName = dtbValue.Rows[i]["ROLE_NAME"].ToString();//object转化成string
                    objRole.m_strRoleCategory = dtbValue.Rows[i]["CATEGORY"].ToString();
                    objRole.m_strRoleDesc = dtbValue.Rows[i]["ROLE_DESCRIPTION"].ToString();

                    p_objRoleArr[i] = objRole;
                }

                #endregion 从DataTable.Rows中获取结果

            }

            return (long)enmOperationResult.DB_Succeed;
        }

        /// <summary>
        /// 获取在Role中的员工
        /// </summary>
        /// <param name="p_strRoleID">角色ID</param></param>
        /// <param name="p_objEmployeeArr">角色中包含员工的数组</param>
        /// <returns></returns> 
        [AutoComplete]
        public long m_lngGetEmployeesInRole(string p_strRoleID,
            out clsEmployee_BaseInfo[] p_objEmployeeArr)
        {
            p_objEmployeeArr = null;

            //检查参数                              
            if (p_strRoleID == null)
                return (long)enmOperationResult.Parameter_Error;

            ArrayList arlEmployee = new ArrayList();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //获取IDataParameter数组
            IDataParameter[] objDPArr;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strRoleID;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetEmployeesInRole, ref dtbValue, objDPArr);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    clsEmployee_BaseInfo objEBI = new clsEmployee_BaseInfo();
                    objEBI.m_strEmployeeID = dtbValue.Rows[i]["EMPLOYEEID"].ToString().Trim();
                    objEBI.m_strFirstName = dtbValue.Rows[i]["FIRSTNAME"].ToString();

                    arlEmployee.Add(objEBI);

                }

                #endregion 从DataTable.Rows中获取结果

                //返回结果到p_objEmployeeArr
                p_objEmployeeArr = (clsEmployee_BaseInfo[])arlEmployee.ToArray(typeof(clsEmployee_BaseInfo));

            }

            return lngRes;
        }

        /// <summary>
        /// 添加员工到Role
        /// </summary>
        /// <param name="p_strRoleID"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <returns></returns> 
        [AutoComplete]
        public long m_lngAddEmployeeToRole(string p_strRoleID,
            string p_strEmployeeID)
        {
            //检查参数                              
            if (p_strRoleID == null || p_strEmployeeID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strRoleID;
                objDPArr[1].Value = p_strEmployeeID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Now;
                objDPArr[3].Value = 0;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddEmployeeToRole, ref lngEff, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// 从Role移去员工
        /// </summary>
        /// <param name="p_strRoleID">角色ID</param>
        /// <param name="p_strEmployeeID">员工ID</param>
        /// <returns></returns> 
        [AutoComplete]
        public long m_lngRemoveEmployeeFromRole(string p_strRoleID,
            string p_strEmployeeID, string p_strDeActivedOperatorID)
        {
            //检查参数
            if (p_strRoleID == null || p_strEmployeeID == null || p_strDeActivedOperatorID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Now;
                objDPArr[1].Value = p_strDeActivedOperatorID;
                objDPArr[2].Value = p_strRoleID;
                objDPArr[3].Value = p_strEmployeeID;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strRemoveEmployeeFromRole, ref lngEff, objDPArr);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;

        }

        /// <summary>
        /// 整批添加员工到Role
        /// 输入参数中员工与相关的Role对应
        /// </summary>
        /// <param name="p_strRoleIDArr">角色ID数组</param>
        /// <param name="p_strEmployeeIDArr">员工ID数组</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBatchAddEmployeeToRole(string[] p_strRoleIDArr,
            string[] p_strEmployeeIDArr)
        {
            //检查参数
            if (p_strRoleIDArr.Length == 0 || p_strEmployeeIDArr.Length == 0)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                for (int j = 0; j < p_strRoleIDArr.Length; j++)
                {
                    //获取IDataParameter数组
                    IDataParameter[] objDPArr;
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].Value = p_strRoleIDArr[j];
                    objDPArr[1].Value = p_strEmployeeIDArr[j];
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Now;
                    objDPArr[3].Value = 0;

                    //执行SQL
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strBatchAddEmployeeToRole, ref lngEff, objDPArr);
                    //执行失败时才返回
                    if (lngRes <= 0)
                    {
                        return lngRes;
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 整批从Role中移去员工
        /// 输入参数中员工与相关的Role对应
        /// </summary>
        /// <param name="p_strRoleIDArr">角色ID数组</param>
        /// <param name="p_strEmployeeIDArr">员工ID数组</param>
        /// <param name="p_strDeActivedOperatorID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBatchRemoveEmployeeFromRole(string[] p_strRoleIDArr,
            string[] p_strEmployeeIDArr, string p_strDeActivedOperatorID)
        {
            //检查参数
            if (p_strRoleIDArr.Length == 0 || p_strEmployeeIDArr.Length == 0 || p_strDeActivedOperatorID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = 0;
            try
            {
                for (int j = 0; j < p_strRoleIDArr.Length; j++)
                {
                    //获取IDataParameter数组
                    IDataParameter[] objDPArr;
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = DateTime.Now;
                    objDPArr[1].Value = p_strDeActivedOperatorID;
                    objDPArr[2].Value = p_strRoleIDArr[j];
                    objDPArr[3].Value = p_strEmployeeIDArr[j];

                    //执行SQL
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strBatchRemoveEmployeeFromRole, ref lngEff, objDPArr);
                    if (lngRes <= 0)
                    {
                        return lngRes;
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetRolsByCategory(string p_strCategory, out clsRole[] p_objRoleArr)
        {
            p_objRoleArr = null;

            //检查参数
            if (p_strCategory == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            ArrayList arlRole = new ArrayList();

            //获取IDataParameter数组
            IDataParameter[] objDPArr;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strCategory;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRolesByCategory, ref dtbValue, objDPArr);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果			
                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                     clsRole objRole = new  clsRole();
                    objRole.m_strRoleID = dtbValue.Rows[i]["ROLE_ID"].ToString().Trim();
                    objRole.m_strRoleName = dtbValue.Rows[i]["ROLE_NAME"].ToString();
                    objRole.m_strRoleCategory = dtbValue.Rows[i]["CATEGORY"].ToString();
                    objRole.m_strRoleDesc = dtbValue.Rows[i]["DESCRIPTION"].ToString();

                    arlRole.Add(objRole);

                }

                #endregion 从DataTable.Rows中获取结果				
            }

            //返回结果到p_objRoleArr
            p_objRoleArr = ( clsRole[])arlRole.ToArray(typeof( clsRole));

            return lngRes;
        }

        /// <summary>
        /// 成批添加或者删除员工角色
        /// </summary>
        /// <param name="p_blnAddRemoveArr"></param>
        /// <param name="p_strRoleIDArr"></param>
        /// <param name="p_strEmployeeIDArr"></param>
        /// <param name="p_strDeActivedOperatorID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBatchAddRemoveRoleEmployee(bool[] p_blnAddRemoveArr,
            string[] p_strRoleIDArr, string[] p_strEmployeeIDArr,
            string p_strDeActivedOperatorID)
        {
            //检查参数
            if (p_strRoleIDArr.Length == 0 || p_strEmployeeIDArr.Length == 0 || p_strDeActivedOperatorID == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                for (int i = 0; i < p_blnAddRemoveArr.Length; i++)
                {
                    if (p_blnAddRemoveArr[i])
                    {
                        //获取IDataParameter数组
                        IDataParameter[] objDPArr;
                        objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                        objDPArr[0].Value = p_strRoleIDArr[i];
                        objDPArr[1].Value = p_strEmployeeIDArr[i];
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Now;
                        objDPArr[3].Value = 0;

                        //执行SQL
                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strBatchAddEmployeeToRole, ref lngEff, objDPArr);
                        //执行失败时才返回
                        if (lngRes <= 0)
                        {
                            return lngRes;
                        }
                    }

                    else
                    {
                        //获取IDataParameter数组
                        IDataParameter[] objDPArr;
                        objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = DateTime.Now;
                        objDPArr[1].Value = p_strDeActivedOperatorID;
                        objDPArr[2].Value = p_strRoleIDArr[i];
                        objDPArr[3].Value = p_strEmployeeIDArr[i];

                        //执行SQL
                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strBatchRemoveEmployeeFromRole, ref lngEff, objDPArr);
                        if (lngRes <= 0)
                        {
                            return lngRes;
                        }
                    }
                }
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;

        }

        [AutoComplete]
        public long m_lngGetDeptInRoleByEmployeeID(string p_strEmployeeID, out clsRoleDeptAssignment[] p_objRDAArr)
        {
            p_objRDAArr = null;

            //检查参数
            if (p_strEmployeeID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            ArrayList arlRDA = new ArrayList();

            //获取IDataParameter数组
            IDataParameter[] objDPArr;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strEmployeeID;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeptInRoleByEmployeeID, ref dtbValue, objDPArr);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果			
                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    clsRoleDeptAssignment objRDA = new clsRoleDeptAssignment();
                    objRDA.m_strRoleID = dtbValue.Rows[i]["ROLE_ID"].ToString().Trim();
                    objRDA.m_strDeptID = dtbValue.Rows[i]["DEPTID"].ToString().Trim();
                    objRDA.m_strDeptName = dtbValue.Rows[i]["DEPTNAME"].ToString().Trim();
                    objRDA.m_blnCanView = bool.Parse(dtbValue.Rows[i]["CANVIEW"].ToString());
                    objRDA.m_blnCanView = bool.Parse(dtbValue.Rows[i]["CANADDNEW"].ToString());
                    objRDA.m_blnCanModify = bool.Parse(dtbValue.Rows[i]["CANMODIFY"].ToString());
                    objRDA.m_blnCanDelete = bool.Parse(dtbValue.Rows[i]["CANDELETE"].ToString());
                    objRDA.m_blnCanPrint = bool.Parse(dtbValue.Rows[i]["CANPRINT"].ToString());

                    arlRDA.Add(objRDA);

                }

                #endregion 从DataTable.Rows中获取结果				
            }

            //返回结果到p_objRDAArr
            p_objRDAArr = (clsRoleDeptAssignment[])arlRDA.ToArray(typeof(clsRoleDeptAssignment));

            return lngRes;
        }

        /// <summary>
        /// 根据员工ID获取其能在HRPExplorer树上见到的部门
        /// </summary>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_objRDAArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptByEmployeeID(string p_strEmployeeID, out clsOISF[] p_objOISFArr)
        {
            p_objOISFArr = null;

            //检查参数
            if (p_strEmployeeID == null)
                return (long)enmOperationResult.Parameter_Error;

            ArrayList arlOISF = new ArrayList();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //objHRPServ = new clsHRPTableService();
            //获取IDataParameter数组
            IDataParameter[] objDPArr;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strEmployeeID;

            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeptByEmployeeID, ref dtbValue, objDPArr);
            //objHRPServ.Dispose();
            //循环DataTable
            if (lngRes > 0 && dtbValue.Rows.Count > 0)
            {
                #region 从DataTable.Rows中获取结果			
                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    clsOISF objOISF = new clsOISF();
                    objOISF.m_strBaseID = dtbValue.Rows[i]["BASEID"].ToString().Trim();
                    objOISF.m_strBaseName = dtbValue.Rows[i]["DEPTNAME"].ToString().Trim();

                    arlOISF.Add(objOISF);

                }

                #endregion 从DataTable.Rows中获取结果				
            }

            //返回结果到p_objOISFArr
            p_objOISFArr = (clsOISF[])arlOISF.ToArray(typeof(clsOISF));

            return lngRes;
        }

    }
}
