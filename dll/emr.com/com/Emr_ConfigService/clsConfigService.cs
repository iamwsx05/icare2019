using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using weCare.Core.Entity;

namespace com.digitalwave.Emr.ConfigService
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsConfigService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 获取一个员工可见的全部模块
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetModuleInfo(string p_strEmployeeID, out clsEmrModuleNode_VO p_objValue)
        {
            p_objValue = null;
            if (string.IsNullOrEmpty(p_strEmployeeID))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select a.moduleid_int,
       a.modulename_vchr,
       a.description_vchr,
       a.displaytype_int,
       a.imageindex_int,
       a.parent_int,
       a.powercode_int,
       a.tstatus_int,
       a.index_int,
       a.ismenuitem_int,
       b.*
  from t_emr_module a
  left join t_aid_emr_form b on a.formid_int = b.formid_int
 where ((a.powercode_int = 1) or
       (a.powercode_int = 2 and
       a.moduleid_int in
       (select d.moduleid_int
            from t_emr_moduledept d, t_bse_deptemp de
           where d.deptid_chr = de.deptid_chr
             and de.empid_chr = ?)) or
       (a.powercode_int = 3 and
       a.moduleid_int in
       (select r.moduleid_int
            from t_emr_modulerole r, t_sys_emprolemap er
           where r.roleid_chr = er.roleid_chr
             and er.empid_chr = ?)) or
       (a.powercode_int = 4 and
       a.moduleid_int in
       (select e.moduleid_int
            from t_emr_moduleemp e
           where e.empid_chr = ?)))";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strEmployeeID;
                objDPArr[1].Value = p_strEmployeeID;
                objDPArr[2].Value = p_strEmployeeID;
                //table
                DataTable dtResult = new DataTable();
                //执行
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_objValue = new clsEmrModuleNode_VO();
                    p_objValue.m_ObjCurrentModuleNode = null;
                    m_mthSetModuleNodes(dtResult, "0", ref p_objValue, true);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 获取全部模块
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllModule(out clsEmrModuleNode_VO p_objValue)
        {
            p_objValue = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select * from t_emr_module";

                //table
                DataTable dtResult = new DataTable();
                //执行
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_objValue = new clsEmrModuleNode_VO();
                    p_objValue.m_ObjCurrentModuleNode = null;
                    m_mthSetModuleNodes(dtResult, "0", ref p_objValue, false);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 获取全部模块,包括表单信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllModuleTable(out DataTable p_dtbValue)
        {
            p_dtbValue = new DataTable();
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select a.moduleid_int,
       a.modulename_vchr,
       a.description_vchr,
       a.displaytype_int,
       a.imageindex_int,
       a.parent_int,
       a.powercode_int,
       a.tstatus_int,
       a.index_int,a.ismenuitem_int,
       b.formdesc_vchr,
       nvl(b.formid_int, -1) formid_int
  from t_emr_module a
  left join t_aid_emr_form b on a.formid_int = b.formid_int ";

                //table
                DataTable dtResult = new DataTable();
                //执行
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_dtbValue = dtResult;
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        [AutoComplete]
        private void m_mthSetModuleNodes(DataTable p_dtResult, string p_strParentID, ref clsEmrModuleNode_VO p_objValue, bool p_blnIsInitFormInfo)
        {
            DataRow[] rowModules = p_dtResult.Select("parent_int='" + p_strParentID + "'", "INDEX_INT");

            if (rowModules.Length > 0)
            {
                DataRow objRow = null;
                int intTemp = 0;
                for (int i = 0; i < rowModules.Length; i++)
                {
                    objRow = rowModules[i];
                    clsEmrModuleNode_VO objNode = new clsEmrModuleNode_VO();
                    objNode.m_ObjCurrentModuleNode = new clsEmrModule_VO();
                    objNode.m_ObjCurrentModuleNode.m_StrModuleId = objRow["moduleid_int"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrModuleName = objRow["modulename_vchr"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrDescription = objRow["description_vchr"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrDisplayType = objRow["displaytype_int"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrImageindex = objRow["imageindex_int"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrParentId = objRow["parent_int"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrPowerCode = objRow["powercode_int"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrFormId = objRow["formid_int"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrTstatus = objRow["tstatus_int"].ToString();
                    objNode.m_ObjCurrentModuleNode.m_StrIndex = objRow["index_int"].ToString();
                    if (int.TryParse(objRow["ISMENUITEM_INT"].ToString(), out intTemp))
                        objNode.m_ObjCurrentModuleNode.m_IntIsMenuitem = intTemp;
                    else
                        objNode.m_ObjCurrentModuleNode.m_IntIsMenuitem = 0;

                    if (objNode.m_ObjCurrentModuleNode.m_StrFormId != "-1" && p_blnIsInitFormInfo)
                    {
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo = new clsEmrFormInfo_VO();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrDLLName = objRow["DLLNAME_VCHR"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrFormClassName = objRow["FORMNAME_VCHR"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrFormDesc = objRow["FORMDESC_VCHR"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrFormId = objRow["FORMID_INT"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrFormnameSpace = objRow["FORMNAMESPACE_VCHR"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrIsSubForm = objRow["ISSUBFORM"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrModuleName = objRow["modulename_vchr"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrOpraClassName = objRow["OPRACLASSNAME_VCHR"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrOpraMethodName = objRow["OPRAMETHODNAME_VCHR"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrSignFlag = objRow["SIGNFLAG_INT"].ToString();
                        objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_StrUsageFlag = objRow["USAGEFLAG_INT"].ToString();
                        if (int.TryParse(objRow["DEACTIVESTATE_INT"].ToString(), out intTemp))
                            objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_IntIsDeactive = intTemp;
                        else
                            objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_IntIsDeactive = 1;
                        if (int.TryParse(objRow["FORMSTATE_INT"].ToString(), out intTemp))
                            objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_IntFormState = intTemp;
                        else
                            objNode.m_ObjCurrentModuleNode.m_ObjFormInfo.m_IntFormState = 1;
                    }

                    m_mthSetModuleNodes(p_dtResult, objNode.m_ObjCurrentModuleNode.m_StrModuleId, ref objNode, p_blnIsInitFormInfo);
                    p_objValue.m_ArlChilds.Add(objNode);
                }
            }
        }
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddModuleItem(clsEmrModule_VO p_objValue, out int p_intModuleId, string[] p_strLists)
        {
            p_intModuleId = -1;
            if (p_objValue == null)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //if (p_objValue.m_ObjFormInfo != null)
                //{
                //    int intFormId = -1;
                //    lngRes = m_lngAddFormInfo(p_objPrincipal, p_objValue.m_ObjFormInfo, out intFormId);
                //    if (lngRes <= 0)
                //        return lngRes;
                //    p_objValue.m_StrFormId = intFormId;
                //}
                lngRes = objHRPServ.m_lngGenerateNewID("t_emr_module", "moduleid_int", out p_intModuleId);
                if (lngRes <= 0 || p_intModuleId == -1)
                    return 0;
                string strSQL = @"insert into t_emr_module
  (moduleid_int,
   modulename_vchr,
   description_vchr,
   displaytype_int,
   imageindex_int,
   parent_int,
   powercode_int,
   formid_int,
   tstatus_int,
   index_int,
   ismenuitem_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?,?,?)";//11

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(11, out objDPArr);
                //赋值
                objDPArr[0].Value = p_intModuleId;
                objDPArr[1].Value = p_objValue.m_StrModuleName;
                objDPArr[2].Value = p_objValue.m_StrDescription;
                objDPArr[3].Value = p_objValue.m_IntDisplayType;
                objDPArr[4].Value = p_objValue.m_IntImageindex;
                objDPArr[5].Value = p_objValue.m_StrParentId;
                objDPArr[6].Value = p_objValue.m_IntPowerCode;
                objDPArr[7].Value = p_objValue.m_StrFormId;
                objDPArr[8].Value = p_objValue.m_IntTstatus;
                objDPArr[9].Value = p_objValue.m_IntIndex;
                objDPArr[10].Value = p_objValue.m_IntIsMenuitem;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);
                if (p_strLists != null && p_strLists.Length > 0 && lngRes > 0)
                {
                    if (p_objValue.m_IntPowerCode == 2)
                    {
                        lngRes = m_lngAddModuleDept(p_intModuleId.ToString(), p_strLists);
                    }
                    else if (p_objValue.m_IntPowerCode == 3)
                    {
                        lngRes = m_lngAddModuleRole(p_intModuleId.ToString(), p_strLists);
                    }
                    else if (p_objValue.m_IntPowerCode == 4)
                    {
                        lngRes = m_lngAddModuleEmp(p_intModuleId.ToString(), p_strLists);
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
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyModuleItem(clsEmrModule_VO p_objValue, string[] p_strLists)
        {
            if (p_objValue == null)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //if (p_objValue.m_ObjFormInfo != null)
                //{
                //    lngRes = m_lngModifyFormInfo(p_objPrincipal, p_objValue.m_ObjFormInfo);
                //    if (lngRes <= 0)
                //        return lngRes;
                //}
                string strSQL = @"update t_emr_module
   set modulename_vchr  = ?,
       description_vchr = ?,
       displaytype_int  = ?,
       imageindex_int   = ?,
       parent_int       = ?,
       powercode_int    = ?,
       formid_int       = ?,
       tstatus_int      = ?,
       index_int = ?,
       ismenuitem_int = ?
 where moduleid_int = ?";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(11, out objDPArr);
                //赋值
                objDPArr[0].Value = p_objValue.m_StrModuleName;
                objDPArr[1].Value = p_objValue.m_StrDescription;
                objDPArr[2].Value = p_objValue.m_IntDisplayType;
                objDPArr[3].Value = p_objValue.m_IntImageindex;
                objDPArr[4].Value = p_objValue.m_StrParentId;
                objDPArr[5].Value = p_objValue.m_IntPowerCode;
                objDPArr[6].Value = p_objValue.m_StrFormId;
                objDPArr[7].Value = p_objValue.m_IntTstatus;
                objDPArr[8].Value = p_objValue.m_IntIndex;
                objDPArr[9].Value = p_objValue.m_IntIsMenuitem;
                objDPArr[10].Value = p_objValue.m_StrModuleId;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);

                if (p_strLists != null && p_strLists.Length > 0 && lngRes > 0)
                {
                    if (p_objValue.m_IntPowerCode == 2)
                    {
                        lngRes = m_lngAddModuleDept(p_objValue.m_StrModuleId, p_strLists);
                    }
                    else if (p_objValue.m_IntPowerCode == 3)
                    {
                        lngRes = m_lngAddModuleRole(p_objValue.m_StrModuleId, p_strLists);
                    }
                    else if (p_objValue.m_IntPowerCode == 4)
                    {
                        lngRes = m_lngAddModuleEmp(p_objValue.m_StrModuleId, p_strLists);
                    }
                    if (lngRes <= 0)
                        throw new Exception("权限关联失败！");
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        ///  删除模块
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteModuleItem(string p_strModuleId)
        {
            if (string.IsNullOrEmpty(p_strModuleId))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                //if (p_objValue.m_ObjFormInfo != null)
                //{
                //    lngRes = m_lngDeleteFormInfo(p_objPrincipal, p_objValue.);
                //    if (lngRes <= 0)
                //        return lngRes;
                //}
                string strSQL = @"delete from  t_emr_module where moduleid_int = ?";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strModuleId;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }

        #region 科室模块关联
        /// <summary>
        /// 删除模块科室关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteModuleDept(string p_strModuleId)
        {
            if (string.IsNullOrEmpty(p_strModuleId))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"delete from  t_emr_moduledept where moduleid_int = ?";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strModuleId;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 添加模块科室关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <param name="p_strDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddModuleDept(string p_strModuleId, string[] p_strDeptArr)
        {
            if (string.IsNullOrEmpty(p_strModuleId) || p_strDeptArr == null)
                return -1;
            long lngRes = m_lngDeleteModuleDept(p_strModuleId);
            if (lngRes <= 0)
                return 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"insert into t_emr_moduledept  (moduleid_int, deptid_chr) values  (?, ?)";
                //参数
                IDataParameter[] objDPArr = null;
                for (int i = 0; i < p_strDeptArr.Length; i++)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    //赋值
                    objDPArr[0].Value = p_strModuleId;
                    objDPArr[1].Value = p_strDeptArr[i];
                    //table
                    long lngRff = 0;
                    try
                    {
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);
                    }
                    catch { }
                }


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return 1;
        }
        /// <summary>
        /// 获取模块关联的科室
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <param name="p_strDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptByModuleId(string p_strModuleId, out string[] p_strDeptArr)
        {
            p_strDeptArr = null;
            if (string.IsNullOrEmpty(p_strModuleId))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select deptid_chr from t_emr_moduledept  where moduleid_int =?";
                //参数
                IDataParameter[] objDPArr = null;
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strModuleId;
                //table
                DataTable dtbReault = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbReault, objDPArr);
                if (lngRes > 0 && dtbReault.Rows.Count > 0)
                {
                    p_strDeptArr = new string[dtbReault.Rows.Count];
                    for (int i = 0; i < dtbReault.Rows.Count; i++)
                    {
                        p_strDeptArr[i] = dtbReault.Rows[i][0].ToString();
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
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        #endregion 科室模块关联

        #region 角色模块关联
        /// <summary>
        /// 删除角色科室关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteModuleRole(string p_strModuleId)
        {
            if (string.IsNullOrEmpty(p_strModuleId))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"delete from  t_emr_modulerole where moduleid_int = ?";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strModuleId;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 添加模块角色关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <param name="p_strRoleArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddModuleRole(string p_strModuleId, string[] p_strRoleArr)
        {
            if (string.IsNullOrEmpty(p_strModuleId) || p_strRoleArr.Length == 0)
                return -1;
            long lngRes = m_lngDeleteModuleRole(p_strModuleId);
            if (lngRes <= 0)
                return 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"insert into t_emr_modulerole  (moduleid_int, roleid_chr) values  (?, ?)";
                //参数
                IDataParameter[] objDPArr = null;
                for (int i = 0; i < p_strRoleArr.Length; i++)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    //赋值
                    objDPArr[0].Value = p_strModuleId;
                    objDPArr[1].Value = p_strRoleArr[i];
                    //table
                    long lngRff = 0;
                    try
                    {
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);
                    }
                    catch { }
                }


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return 1;
        }
        /// <summary>
        /// 获取模块关联的科室
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <param name="p_strDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRoleByModuleId(string p_strModuleId, out string[] p_strRoleArr)
        {
            p_strRoleArr = null;
            if (string.IsNullOrEmpty(p_strModuleId))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select roleid_chr from t_emr_modulerole  where moduleid_int =?";
                //参数
                IDataParameter[] objDPArr = null;
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strModuleId;
                //table
                DataTable dtbReault = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbReault, objDPArr);
                if (lngRes > 0 && dtbReault.Rows.Count > 0)
                {
                    p_strRoleArr = new string[dtbReault.Rows.Count];
                    for (int i = 0; i < dtbReault.Rows.Count; i++)
                    {
                        p_strRoleArr[i] = dtbReault.Rows[i][0].ToString();
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
                //objHrpServ.Dispose();
            }
            return lngRes;
        }

        #endregion 角色模块关联

        #region 员工模块关联
        /// <summary>
        /// 删除员工科室关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteModuleEmp(string p_strModuleId)
        {
            if (string.IsNullOrEmpty(p_strModuleId))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"delete from  t_emr_moduleemp where moduleid_int = ?";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strModuleId;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 添加员工角色关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <param name="p_strEmpArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddModuleEmp(string p_strModuleId, string[] p_strEmpArr)
        {
            if (string.IsNullOrEmpty(p_strModuleId) || p_strEmpArr.Length == 0)
                return -1;
            long lngRes = m_lngDeleteModuleEmp(p_strModuleId);
            if (lngRes <= 0)
                return 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"insert into t_emr_moduleemp  (moduleid_int, empid_chr) values  (?, ?)";
                //参数
                IDataParameter[] objDPArr = null;
                for (int i = 0; i < p_strEmpArr.Length; i++)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    //赋值
                    objDPArr[0].Value = p_strModuleId;
                    objDPArr[1].Value = p_strEmpArr[i];
                    //table
                    long lngRff = 0;
                    try
                    {
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);
                    }
                    catch { }
                }


            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return 1;
        }
        /// <summary>
        /// 获取模块关联的科室
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strModuleId"></param>
        /// <param name="p_strDeptArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmpByModuleId(string p_strModuleId, out string[] p_strEmpArr)
        {
            p_strEmpArr = null;
            if (string.IsNullOrEmpty(p_strModuleId))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select empid_chr from t_emr_moduleemp  where moduleid_int =?";
                //参数
                IDataParameter[] objDPArr = null;
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strModuleId;
                //table
                DataTable dtbReault = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbReault, objDPArr);
                if (lngRes > 0 && dtbReault.Rows.Count > 0)
                {
                    p_strEmpArr = new string[dtbReault.Rows.Count];
                    for (int i = 0; i < dtbReault.Rows.Count; i++)
                    {
                        p_strEmpArr[i] = dtbReault.Rows[i][0].ToString();
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
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        #endregion 员工模块关联

        [AutoComplete]
        public long m_lngUpdateIndex(string p_strPrevModule, int p_intPrevModuleIndex, string p_strNextModule, int p_intNextModuleIndex)
        {
            if (string.IsNullOrEmpty(p_strPrevModule) || string.IsNullOrEmpty(p_strNextModule) || p_intPrevModuleIndex < 0 || p_intNextModuleIndex < 0)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"update t_emr_module t set t.index_int = ? where t.moduleid_int = ?";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //赋值
                objDPArr[0].DbType = DbType.Int32;
                objDPArr[0].Value = p_intNextModuleIndex;
                objDPArr[1].DbType = DbType.Int32;
                objDPArr[1].Value = p_strPrevModule;
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);
                if (lngRes > 0)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    //赋值
                    objDPArr[0].DbType = DbType.Int32;
                    objDPArr[0].Value = p_intPrevModuleIndex;
                    objDPArr[1].DbType = DbType.Int32;
                    objDPArr[1].Value = p_strNextModule;
                    //执行
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }

        #region FormInfo
        [AutoComplete]
        public long m_lngGetAllFormInfo(out clsEmrFormInfo_VO[] p_objValue)
        {
            p_objValue = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = @"select a.formid_int,
       a.formnamespace_vchr,
       a.formname_vchr,
       a.formdesc_vchr,
       a.usageflag_int,
       a.signflag_int,
       a.dllname_vchr,
       a.opraclassname_vchr,
       a.opramethodname_vchr,
       a.issubform,
       a.deactivestate_int,
       a.formstate_int,
       a.printstate_int,
       b.modulename_vchr,
       null printrole
  from t_aid_emr_form a
  left join t_emr_module b on a.formid_int = b.formid_int
 where a.printstate_int < 2
union
select a.formid_int,
       a.formnamespace_vchr,
       a.formname_vchr,
       a.formdesc_vchr,
       a.usageflag_int,
       a.signflag_int,
       a.dllname_vchr,
       a.opraclassname_vchr,
       a.opramethodname_vchr,
       a.issubform,
       a.deactivestate_int,
       a.formstate_int,
       a.printstate_int,
       b.modulename_vchr,
       r.roleidarr_chr printrole
  from t_aid_emr_form a
  left join t_emr_module b on a.formid_int = b.formid_int
 inner join t_emr_formprintrole r on a.formid_int = r.formid_int
 where a.printstate_int = 2
   and r.status_int = 1";

                //table
                DataTable dtResult = new DataTable();
                //执行
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    DataTable dtbDistinctForm = dtResult.DefaultView.ToTable(true, new string[] {"formid_int","formnamespace_vchr","dllname_vchr","formname_vchr",
                        "FORMDESC_VCHR","ISSUBFORM","OPRAMETHODNAME_VCHR","OPRACLASSNAME_VCHR","SIGNFLAG_INT","USAGEFLAG_INT","DEACTIVESTATE_INT","FORMSTATE_INT","PRINTROLE","printstate_int"});
                    int intRowCount = dtbDistinctForm.Rows.Count;
                    p_objValue = new clsEmrFormInfo_VO[intRowCount];
                    DataRow objRow = null;
                    int intTemp = 0;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbDistinctForm.Rows[i];
                        clsEmrFormInfo_VO objValue = new clsEmrFormInfo_VO();
                        objValue.m_StrFormId = objRow["FORMID_INT"].ToString();
                        objValue.m_StrFormnameSpace = objRow["FORMNAMESPACE_VCHR"].ToString();
                        objValue.m_StrDLLName = objRow["DLLNAME_VCHR"].ToString();
                        objValue.m_StrFormClassName = objRow["FORMNAME_VCHR"].ToString();
                        objValue.m_StrFormDesc = objRow["FORMDESC_VCHR"].ToString();
                        objValue.m_StrIsSubForm = objRow["ISSUBFORM"].ToString();
                        objValue.m_StrOpraMethodName = objRow["OPRAMETHODNAME_VCHR"].ToString();
                        objValue.m_StrOpraClassName = objRow["OPRACLASSNAME_VCHR"].ToString();
                        objValue.m_StrSignFlag = objRow["SIGNFLAG_INT"].ToString();
                        objValue.m_StrUsageFlag = objRow["USAGEFLAG_INT"].ToString();
                        if (int.TryParse(objRow["DEACTIVESTATE_INT"].ToString(), out intTemp))
                            objValue.m_IntIsDeactive = intTemp;
                        else
                            objValue.m_IntIsDeactive = 1;
                        if (int.TryParse(objRow["FORMSTATE_INT"].ToString(), out intTemp))
                            objValue.m_IntFormState = intTemp;
                        else
                            objValue.m_IntFormState = 1;
                        if (int.TryParse(objRow["printstate_int"].ToString(), out intTemp))
                            objValue.m_IntPrintState = intTemp;
                        else
                            objValue.m_IntFormState = 1;
                        objValue.m_StrPrintRoles = objRow["PRINTROLE"].ToString();
                        DataRow[] rows = dtResult.Select("FORMID_INT = '" + objValue.m_StrFormId + "'");
                        if (rows.Length > 0)
                        {
                            objValue.m_StrModuleName += rows[0]["MODULENAME_VCHR"].ToString();
                            if (rows.Length >= 2)
                                for (int j = 1; j < rows.Length; j++)
                                    objValue.m_StrModuleName += "、" + rows[j]["MODULENAME_VCHR"].ToString();
                        }
                        p_objValue[i] = objValue;
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
                //objHrpServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 获取窗体信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormName">窗体类名</param>
        /// <param name="p_objValue">表单</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFormInfo(string p_strFormName, out clsEmrFormInfo_VO p_objValue)
        {
            p_objValue = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.formid_int,
       t.formnamespace_vchr,
       t.formname_vchr,
       t.formdesc_vchr,
       t.usageflag_int,
       t.signflag_int,
       t.dllname_vchr,
       t.opraclassname_vchr,
       t.opramethodname_vchr,
       t.issubform,
       t.deactivestate_int,
       t.formstate_int,
       t.printstate_int
  from t_aid_emr_form t
 where t.formname_vchr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strFormName;

                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_objValue = new clsEmrFormInfo_VO();
                    p_objValue.m_StrFormId = dtbValue.Rows[0]["formid_int"].ToString();
                    p_objValue.m_StrFormnameSpace = dtbValue.Rows[0]["formnamespace_vchr"].ToString();
                    p_objValue.m_StrFormClassName = dtbValue.Rows[0]["formname_vchr"].ToString();
                    p_objValue.m_StrFormDesc = dtbValue.Rows[0]["formdesc_vchr"].ToString();
                    p_objValue.m_StrUsageFlag = dtbValue.Rows[0]["usageflag_int"].ToString();
                    p_objValue.m_StrSignFlag = dtbValue.Rows[0]["signflag_int"].ToString();
                    p_objValue.m_StrDLLName = dtbValue.Rows[0]["dllname_vchr"].ToString();
                    p_objValue.m_StrOpraClassName = dtbValue.Rows[0]["opraclassname_vchr"].ToString();
                    p_objValue.m_StrOpraMethodName = dtbValue.Rows[0]["opramethodname_vchr"].ToString();
                    p_objValue.m_StrIsSubForm = dtbValue.Rows[0]["issubform"].ToString();
                    p_objValue.m_IntIsDeactive = Convert.ToInt32(dtbValue.Rows[0]["deactivestate_int"]);
                    p_objValue.m_IntFormState = Convert.ToInt32(dtbValue.Rows[0]["formstate_int"]);
                    p_objValue.m_IntPrintState = Convert.ToInt32(dtbValue.Rows[0]["printstate_int"]);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 添加窗体信息(自定义窗体用)
        /// </summary>
        /// <param name="p_objValue">窗体信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddFormInfo_CustomForm(clsEmrFormInfo_VO p_objValue)
        {
            if (p_objValue == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsEmrFormInfo_VO objVOtemp = null;
                lngRes = m_lngGetFormInfo(p_objValue.m_StrFormClassName, out objVOtemp);
                if (objVOtemp != null)
                {
                    lngRes = m_lngDeleteFormInfo(objVOtemp.m_StrFormId);
                }
                int intFormID = 0;
                lngRes = m_lngAddFormInfo(p_objValue, out intFormID);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 添加窗体信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <param name="p_intFormId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddFormInfo(clsEmrFormInfo_VO p_objValue, out int p_intFormId)
        {
            p_intFormId = -1;
            if (p_objValue == null)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                lngRes = objHRPServ.m_lngGenerateNewID("t_aid_emr_form", "formid_int", out p_intFormId);
                if (lngRes <= 0 || p_intFormId == -1)
                    return 0;
                string strSQL = @"insert into t_aid_emr_form
  (formid_int,
   formnamespace_vchr,
   formname_vchr,
   formdesc_vchr,
   usageflag_int,
   signflag_int,
   dllname_vchr,
   opramethodname_vchr,
   opraclassname_vchr,
   issubform,deactivestate_int,formstate_int,printstate_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?,?,?,?,?)";//13

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(13, out objDPArr);
                //赋值
                objDPArr[0].Value = p_intFormId;
                objDPArr[1].Value = p_objValue.m_StrFormnameSpace;
                objDPArr[2].Value = p_objValue.m_StrFormClassName;
                objDPArr[3].Value = p_objValue.m_StrFormDesc;
                objDPArr[4].Value = p_objValue.m_IntUsageFlag;
                objDPArr[5].Value = p_objValue.m_IntSignFlag;
                objDPArr[6].Value = p_objValue.m_StrDLLName;
                objDPArr[7].Value = p_objValue.m_StrOpraMethodName;
                objDPArr[8].Value = p_objValue.m_StrOpraClassName;
                objDPArr[9].Value = p_objValue.m_IntIsSubForm;
                objDPArr[10].Value = p_objValue.m_IntIsDeactive;
                objDPArr[11].Value = p_objValue.m_IntFormState;
                objDPArr[12].Value = p_objValue.m_IntPrintState;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);
                if (lngRes > 0 && p_objValue.m_IntPrintState == 2 && !string.IsNullOrEmpty(p_objValue.m_StrPrintRoles))
                {
                    lngRes = m_lngAddFormPtintRole(p_intFormId, p_objValue.m_StrPrintRoles);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 修改窗体信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyFormInfo(clsEmrFormInfo_VO p_objValue)
        {
            if (p_objValue == null)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                string strSQL = @"update t_aid_emr_form
   set formnamespace_vchr = ?,
       formname_vchr      = ?,
       formdesc_vchr      = ?,
       usageflag_int      = ?,
       signflag_int       = ?,
       dllname_vchr       = ?,
       opramethodname_vchr = ?,
       opraclassname_vchr = ?,
       issubform          = ?,
       deactivestate_int = ?,
       formstate_int = ?,
       printstate_int = ?
 where formid_int = ?";//13

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(13, out objDPArr);
                //赋值
                objDPArr[0].Value = p_objValue.m_StrFormnameSpace;
                objDPArr[1].Value = p_objValue.m_StrFormClassName;
                objDPArr[2].Value = p_objValue.m_StrFormDesc;
                objDPArr[3].Value = p_objValue.m_IntUsageFlag;
                objDPArr[4].Value = p_objValue.m_IntSignFlag;
                objDPArr[5].Value = p_objValue.m_StrDLLName;
                objDPArr[6].Value = p_objValue.m_StrOpraMethodName;
                objDPArr[7].Value = p_objValue.m_StrOpraClassName;
                objDPArr[8].Value = p_objValue.m_IntIsSubForm;
                objDPArr[9].Value = p_objValue.m_IntIsDeactive;
                objDPArr[10].Value = p_objValue.m_IntFormState;
                objDPArr[11].Value = p_objValue.m_IntPrintState;
                objDPArr[12].Value = p_objValue.m_StrFormId;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);

                if (lngRes > 0 && p_objValue.m_IntPrintState == 2 && !string.IsNullOrEmpty(p_objValue.m_StrPrintRoles))
                {
                    lngRes = m_lngModifyFormPtintRole(int.Parse(p_objValue.m_StrFormId), p_objValue.m_StrPrintRoles);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_lngDeleteFormInfo(string p_strFormId)
        {
            if (string.IsNullOrEmpty(p_strFormId))
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                string strSQL = @"delete t_aid_emr_form where formid_int = ?";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_strFormId;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);

                if (lngRes > 0)
                {
                    lngRes = m_lngDeleteFormPtintRole(int.Parse(p_strFormId));
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// 获取所有可见得专科病历窗体
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpId">传入空值表示获取所有专科表单</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSPFormInfo(string p_strEmpId, out clsInpatMedRec_Type[] p_objMedRec_TypeArr)
        {
            p_objMedRec_TypeArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                string strSQL = string.Empty;
                DataTable dt = new DataTable();
                if (string.IsNullOrEmpty(p_strEmpId))
                {
                    strSQL = @"select b.formid_int, b.formnamespace_vchr, b.formname_vchr, b.formdesc_vchr
  from t_emr_module a
 inner join t_aid_emr_form b on a.formid_int = b.formid_int
 where a.powercode_int > 0
   and b.usageflag_int = 0
   and b.formstate_int = 2";
                    lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dt);
                }
                else
                {
                    strSQL = @"select b.formid_int, b.formnamespace_vchr, b.formname_vchr, b.formdesc_vchr
  from t_emr_module a
 inner join t_aid_emr_form b on a.formid_int = b.formid_int
                            and b.usageflag_int = 0
                            and b.formstate_int = 2
 where a.powercode_int = 1
union
select b.formid_int, b.formnamespace_vchr, b.formname_vchr, b.formdesc_vchr
  from t_emr_module a
 inner join t_aid_emr_form b on a.formid_int = b.formid_int
                            and b.usageflag_int = 0
                            and b.formstate_int = 2
 where a.powercode_int = 2
   and exists (select d.moduleid_int
          from t_emr_moduledept d, t_bse_deptemp de
         where d.deptid_chr = de.deptid_chr
           and de.empid_chr = ?
           and d.moduleid_int = a.moduleid_int)
union
select b.formid_int, b.formnamespace_vchr, b.formname_vchr, b.formdesc_vchr
  from t_emr_module a
 inner join t_aid_emr_form b on a.formid_int = b.formid_int
                            and b.usageflag_int = 0
                            and b.formstate_int = 2
 where a.powercode_int = 3
   and exists (select r.moduleid_int
          from t_emr_modulerole r, t_sys_emprolemap er
         where r.roleid_chr = er.roleid_chr
           and er.empid_chr = ?
           and r.moduleid_int = a.moduleid_int)
union
select b.formid_int, b.formnamespace_vchr, b.formname_vchr, b.formdesc_vchr
  from t_emr_module a
 inner join t_aid_emr_form b on a.formid_int = b.formid_int
                            and b.usageflag_int = 0
                            and b.formstate_int = 2
 where a.powercode_int = 4
   and exists (select e.moduleid_int
          from t_emr_moduleemp e
         where e.empid_chr = ?
           and e.moduleid_int = a.moduleid_int)";//3
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strEmpId;
                    objDPArr[1].Value = p_strEmpId;
                    objDPArr[2].Value = p_strEmpId;
                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
                }

                int intRowCount = dt.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    DataRow objRow = null;
                    p_objMedRec_TypeArr = new clsInpatMedRec_Type[intRowCount];
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dt.Rows[i];
                        clsInpatMedRec_Type objType = new clsInpatMedRec_Type();
                        objType.m_strTypeID = objRow["formname_vchr"].ToString();
                        objType.m_strTypeName = objRow["formdesc_vchr"].ToString();
                        p_objMedRec_TypeArr[i] = objType;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                //objHrpServ.Dispose();
            }
            return lngRes;
        }
        #region printRole
        /// <summary>
        /// 添加打印权限控制的角色
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intFormId"></param>
        /// <param name="p_strRoles"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddFormPtintRole(int p_intFormId, string p_strRoles)
        {
            if (p_intFormId < 0 || string.IsNullOrEmpty(p_strRoles)) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                string strSQL = @"insert into t_emr_formprintrole
  (formid_int, roleidarr_chr, status_int, id_int)
values
  (?, ?, 1, seq_emr.nextval)";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //赋值
                objDPArr[0].Value = p_intFormId;
                objDPArr[1].Value = p_strRoles;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }


        /// <summary>
        /// 添加打印权限控制的角色
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intFormId"></param>
        /// <param name="p_strRoles"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyFormPtintRole(int p_intFormId, string p_strRoles)
        {
            if (p_intFormId < 0 || string.IsNullOrEmpty(p_strRoles)) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;
                lngRes = m_lngDeleteFormPtintRole(p_intFormId);
                if (lngRes > 0)
                {
                    lngRes = m_lngAddFormPtintRole(p_intFormId, p_strRoles);
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 添加打印权限控制的角色
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intFormId"></param>
        /// <param name="p_strRoles"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteFormPtintRole(int p_intFormId)
        {
            if (p_intFormId < 0) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                string strSQL = @"update t_emr_formprintrole
   set status_int = 0
 where formid_int = ?
   and status_int = 1";

                //参数
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值
                objDPArr[0].Value = p_intFormId;
                //table
                long lngRff = 0;
                //执行
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// 检查是否有打印的权限
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strNameSpace"></param>
        /// <param name="p_strClassName"></param>
        /// <param name="p_strRoles"></param>
        /// <returns>true ＝ 有；false ＝ 无</returns>
        [AutoComplete]
        public bool m_blnCheckCanPrint(string p_strNameSpace, string p_strClassName, string[] p_strRoles)
        {
            if (string.IsNullOrEmpty(p_strNameSpace) || string.IsNullOrEmpty(p_strClassName) || p_strRoles == null || p_strRoles.Length == 0) return true;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
                //long lngCheckRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal, "clsHospitalManagerService", "m_lngGetDeptInfo");
                //objPrivilege.Dispose();
                //if (lngCheckRes <= 0)
                //    return lngCheckRes;

                string strSQL = @"select t.printstate_int, r.roleidarr_chr
  from t_aid_emr_form t
  left join t_emr_formprintrole r on t.formid_int = r.formid_int
                                 and r.status_int = 1
 where t.formnamespace_vchr = ?
   and t.formname_vchr = ?
   and t.usageflag_int = 0";//2


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strNameSpace;
                objDPArr[1].Value = p_strClassName;

                DataTable dtValues = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtValues, objDPArr);
                if (lngRes > 0 && dtValues.Rows.Count > 0)
                {
                    string strPrintState = dtValues.Rows[0]["printstate_int"].ToString();
                    if (strPrintState == "1")
                        return true;
                    if (strPrintState == "2")
                    {
                        string strRoles = dtValues.Rows[0]["roleidarr_chr"].ToString();
                        for (int i = 0; i < p_strRoles.Length; i++)
                        {
                            if (strRoles.Contains(p_strRoles[i]))
                                return true;
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return false;
        }
        #endregion printRole

        #endregion FormInfo
    }
}
