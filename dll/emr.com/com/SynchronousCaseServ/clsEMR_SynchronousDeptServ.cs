using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCaseServ
{
    /// <summary>
    /// 同步病案科室
    /// </summary>
    [Transaction(TransactionOption.NotSupported)]
    [ObjectPooling(true)]
    public class clsEMR_SynchronousDeptServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 获取病区(专业组)列表
        /// <summary>
        /// 获取病区(专业组)列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intDeptType">iCare病案科室同步配置,0时获取专业组,=1时获取科室</param>
        /// <param name="p_objDeptArr">科室列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetICareDeptList(int p_intDeptType, out clsEmrDept_VO[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;
            try
            {
                string strSQL = string.Empty;

                if (p_intDeptType == 0)
                {
                    strSQL = @"select t.groupid_chr code,t.groupname_vchr name from t_bse_groupdesc t order by sort_int";
                }
                else if (p_intDeptType == 1)
                {
                    strSQL = @"select t.deptid_chr code, t.deptname_vchr name
  from t_bse_deptdesc t
 where t.attributeid = '0000002'
   and t.inpatientoroutpatient_int = 1
   and t.status_int = 1
  order by t.deptname_vchr";
                }

                clsHRPTableService objHRPServ = new clsHRPTableService();
                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (dtbResult == null)
                {
                    return -1;
                }


                int intRowsCount = dtbResult.Rows.Count;
                if (intRowsCount <= 0)
                {
                    return -1;
                }

                p_objDeptArr = new clsEmrDept_VO[intRowsCount];
                DataRow drCurrent = null;
                for (int iR = 0; iR < intRowsCount; iR++)
                {
                    drCurrent = dtbResult.Rows[iR];
                    p_objDeptArr[iR] = new clsEmrDept_VO();
                    p_objDeptArr[iR].m_strDEPTID_CHR = drCurrent["code"].ToString();
                    p_objDeptArr[iR].m_strDEPTNAME_VCHR = drCurrent["name"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取病案系统科室列表
        /// <summary>
        /// 获取病案系统科室列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objDeptArr">科室列表</param>
        /// <returns></returns>
        //[AutoComplete]
        public long m_lngGetBADeptList( out clsEmrDept_VO[] p_objDeptArr)
        {
            p_objDeptArr = null;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);

                string strSQL = @"select t.ftykh, t.fksname
  from tworkroom t
 where t.fnoused = 0
   and t.ftype = 2
  order by t.fksname";

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (dtbResult == null)
                {
                    return -1;
                }


                int intRowsCount = dtbResult.Rows.Count;
                if (intRowsCount <= 0)
                {
                    return -1;
                }

                p_objDeptArr = new clsEmrDept_VO[intRowsCount];
                DataRow drCurrent = null;
                for (int iR = 0; iR < intRowsCount; iR++)
                {
                    drCurrent = dtbResult.Rows[iR];
                    p_objDeptArr[iR] = new clsEmrDept_VO();
                    p_objDeptArr[iR].m_strDEPTID_CHR = drCurrent["ftykh"].ToString();
                    p_objDeptArr[iR].m_strDEPTNAME_VCHR = drCurrent["fksname"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 修改专业组科室关联

        /// <summary>
        /// 修改专业组科室关联
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strGroupID">科室ID</param>
        /// <param name="p_strBA_DeptNum">病案系统科号</param>
        /// <param name="p_intDeptType">iCare病案科室同步配置,=0专业组,=1科室</param>
        /// <param name="p_strICareDeptName">科室名称</param>
        /// <param name="p_strBA_DeptName">病案系统科室名称</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModiftGroupRelation(string[] p_strGroupID, string p_strBA_DeptNum, int p_intDeptType,
            string[] p_strICareDeptName, string p_strBA_DeptName)
        {
            if (string.IsNullOrEmpty(p_strBA_DeptNum))
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strDelSQL = @"delete from t_emr_group_relation t where t.ba_deptnum = ? and t.depttype = ?";

                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr1);
                objDPArr1[0].Value = p_strBA_DeptNum;
                objDPArr1[1].Value = p_intDeptType;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strDelSQL, ref lngEff, objDPArr1);

                if (lngRes <= 0)
                {
                    return -1;
                }

                if (p_strGroupID == null || p_strGroupID.Length <= 0)
                {
                    return 1;
                }

                string strSQL = @"insert into t_emr_group_relation t (t.groupid_chr,t.ba_deptnum,t.depttype,icare_deptname,ba_deptname) values(?,?,?,?,?)";
                IDataParameter[] objDPArr = null;

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int iR = 0; iR < p_strGroupID.Length; iR++)
                    {
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = p_strGroupID[iR];
                        objDPArr[1].Value = p_strBA_DeptNum;
                        objDPArr[2].Value = p_intDeptType;
                        objDPArr[3].Value = p_strICareDeptName[iR];
                        objDPArr[4].Value = p_strBA_DeptName;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.Int32, DbType.String, DbType.String };
                    object[][] objValues = new object[5][];

                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_strGroupID.Length];//初始化


                    }

                    for (int k1 = 0; k1 < p_strGroupID.Length; k1++)
                    {
                        objValues[0][k1] = p_strGroupID[k1];
                        objValues[1][k1] = p_strBA_DeptNum;
                        objValues[2][k1] = p_intDeptType;
                        objValues[3][k1] = p_strICareDeptName[k1];
                        objValues[4][k1] = p_strBA_DeptName;
                    }
                    lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取相关的病案系统科号

        /// <summary>
        /// 获取相关的病案系统科号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strGroupID">iCare系统专业组ID</param>
        /// <param name="p_intSet">iCare病案科室同步配置,=0专业组,=1科室</param>
        /// <param name="p_strBA_DeptNum">病案系统科号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBA_DeptNum( string p_strGroupID, int p_intDeptType, out string p_strBA_DeptNum)
        {
            p_strBA_DeptNum = string.Empty;
            if (string.IsNullOrEmpty(p_strGroupID))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select ba_deptnum from t_emr_group_relation where groupid_chr = ? and depttype = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strGroupID;
                objDPArr[1].Value = p_intDeptType;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0)
                {
                    if (dtbResult != null && dtbResult.Rows.Count == 1)
                    {
                        p_strBA_DeptNum = dtbResult.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取相关的iCare系统专业组ID
        /// <summary>
        /// 获取相关的iCare系统专业组ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBA_DeptNum">病案系统科号</param>
        /// <param name="p_intSet">iCare病案科室同步配置,=0专业组,=1科室</param>
        /// <param name="p_strGroupIDArr">iCare系统专业组ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetICare_GroupID(string p_strBA_DeptNum, int p_intDeptType, out string[] p_strGroupIDArr)
        {
            p_strGroupIDArr = null;
            if (string.IsNullOrEmpty(p_strBA_DeptNum))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select groupid_chr from t_emr_group_relation where ba_deptnum = ? and depttype = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strBA_DeptNum;
                objDPArr[1].Value = p_intDeptType;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0)
                {
                    if (dtbResult != null)
                    {
                        int intRowsCount = dtbResult.Rows.Count;
                        if (intRowsCount > 0)
                        {
                            p_strGroupIDArr = new string[intRowsCount];

                            for (int iR = 0; iR < intRowsCount; iR++)
                            {
                                p_strGroupIDArr[iR] = dtbResult.Rows[iR][0].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取相关的病案系统科号

        /// <summary>
        /// 获取相关的病案系统科号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strGroupID">iCare系统专业组ID</param>
        /// <param name="p_intSet">iCare病案科室同步配置,=0专业组,=1科室</param>
        /// <param name="p_strBA_DeptNum">病案系统科号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBADeptNum( string p_strGroupID, int p_intDeptType, out string p_strBA_DeptNum)
        {
            p_strBA_DeptNum = string.Empty;
            if (string.IsNullOrEmpty(p_strGroupID))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"select ba_deptnum from t_emr_group_relation where groupid_chr = ? and depttype = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strGroupID;
                objDPArr[1].Value = p_intDeptType;

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                if (lngRes > 0)
                {
                    if (dtbResult != null && dtbResult.Rows.Count == 1)
                    {
                        p_strBA_DeptNum = dtbResult.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取系统设置
        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSetID">设置ID</param>
        /// <param name="p_intDeptType">设置代码</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSysSetting( string p_strSetID, out int p_intDeptType)
        {
            p_intDeptType = 0;
            if (p_strSetID == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select setstatus_int from t_sys_setting where setid_chr=?";

                DataTable dtbValue = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSetID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_intDeptType = Convert.ToInt32(dtbValue.Rows[0][0]);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        /// <summary>
        /// 获取科室关联表数据
        /// </summary>
        /// <param name="p_dtbDict">关联表数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeptRelationDict(out DataTable p_dtbDict)
        {
            p_dtbDict = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.groupid_chr, a.ba_deptnum
  from t_emr_group_relation a, t_sys_setting b
 where a.depttype = b.setstatus_int
   and b.setid_chr = '3017'";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbDict);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
    }
}
