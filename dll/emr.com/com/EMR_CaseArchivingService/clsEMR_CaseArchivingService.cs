using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.emr.EMR_CaseArchivingService
{
    /// <summary>
    /// 病案归档
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_CaseArchivingService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 出院超过期限的病人自动归档

        /// <summary>
        /// 出院超过期限的病人自动归档

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intDays">自动归档期限</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCaseArchivingAutomatically(int p_intDays)
        {
            long lngRes = 0;
            if (p_intDays < 0)
            {
                return -1;
            }

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strDateDiff = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    strDateDiff = "(sysdate - le.MODIFY_DAT)";
                }
                else
                {
                    strDateDiff = "(DATEDIFF(mi, le.MODIFY_DAT, getdate()) / 1440)";
                }

                string strSQL = @"insert into T_EMR_CASEARCHIVING t
  select ?,
         le.registerid_chr,
         " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @",
         null,
         1,
         2,
         1,
         le.modify_dat
    from T_OPR_BIH_LEAVE le
   where " + strDateDiff + @" > ?
     and le.status_int = 1
     and le.PSTATUS_INT = 1
     and not exists
   (select *
            from T_EMR_CASEARCHIVING t1
           where t1.registerid_chr = le.registerid_chr)";

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_CASEARCHIVING", out lngSequence);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //赋值

                objDPArr[0].Value = lngSequence;
                objDPArr[1].Value = p_intDays;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 保存指定病人至归档表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_dtmLeaveDate">出院日期</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveSpecifyPatientToArchiving(string p_strRegisterID, DateTime p_dtmLeaveDate)
        {
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strRegisterID))
            {
                return -1;
            }

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"insert into T_EMR_CASEARCHIVING
  (ARCHIVEDID_INT,
   REGISTERID_CHR,
   ARCHIVEDDATE_DAT,
   STATUS_INT,
   ARCHIVEDSTATUS_INT,
   Caselevel_Int,
   OUTDATE_DAT)
values
  (?, ?, " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @", 1, 0, 0, ?)";

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_CASEARCHIVING", out lngSequence);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                //赋值

                objDPArr[0].Value = lngSequence;
                objDPArr[2].Value = p_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmLeaveDate;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 超过期限的取消归档的病案自动归档
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intDays">自动归档期限</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyCancelArchivingCaseAutomatically(int p_intDays)
        {
            long lngRes = 0;
            if (p_intDays < 0)
            {
                return -1;
            }

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strDateDiff = string.Empty;
                if (clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    strDateDiff = "(sysdate - le.MODIFY_DAT)";
                }
                else
                {
                    strDateDiff = "(DATEDIFF(mi, le.MODIFY_DAT, getdate()) / 1440)";
                }

                string strSQL = @"update T_EMR_CASEARCHIVING t
   set t.archiveddate_dat = " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @", t.archivedstatus_int = 2
 where t.archivedstatus_int = -1
   and exists (select *
          from T_OPR_BIH_LEAVE le
         where " + strDateDiff + @" > ?
           and le.status_int = 1
           and le.PSTATUS_INT = 1
           and le.registerid_chr = t.registerid_chr)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //赋值

                objDPArr[0].Value = p_intDays;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 取消归档
        /// <summary>
        /// 取消归档
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngArchivedIDArr">归档流水号</param>
        /// <param name="p_strEMPID">操作人ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCancelArchiving(long[] p_lngArchivedIDArr, string p_strEMPID)
        {
            long lngRes = 0;
            if (p_lngArchivedIDArr == null || p_lngArchivedIDArr.Length <= 0 || string.IsNullOrEmpty(p_strEMPID))
            {
                return -1;
            }
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"update T_EMR_CASEARCHIVING t
   set t.ARCHIVEDSTATUS_INT = -1,ARCHIVEUSER_CHR = ?
 where t.archivedid_int = ?";

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_lngArchivedIDArr.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;
                        objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                        objDPArr2[0].Value = p_strEMPID;
                        objDPArr2[1].Value = p_lngArchivedIDArr[i];

                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr2);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.Int64 };
                    object[][] objValues = new object[2][];

                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_lngArchivedIDArr.Length];//初始化

                    }
                    for (int k1 = 0; k1 < p_lngArchivedIDArr.Length; k1++)
                    {
                        objValues[0][k1] = p_strEMPID;
                        objValues[1][k1] = p_lngArchivedIDArr[k1];
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

        #region 病案归档
        /// <summary>
        /// 病案归档
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">归档人ID</param>
        /// <param name="p_lngArchivedIDArr">归档流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngArchiving(string p_strEmpID, long[] p_lngArchivedIDArr)
        {
            long lngRes = 0;
            if (p_lngArchivedIDArr == null || p_lngArchivedIDArr.Length <= 0 || string.IsNullOrEmpty(p_strEmpID))
            {
                return -1;
            }
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"update T_EMR_CASEARCHIVING t
   set t.ARCHIVEDSTATUS_INT = 1,ARCHIVEDDATE_DAT= " + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @",ARCHIVEUSER_CHR=?,t.CASELEVEL_INT = 1
 where t.archivedid_int = ? and (ARCHIVEDSTATUS_INT=-1 or ARCHIVEDSTATUS_INT=0)";

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_lngArchivedIDArr.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;
                        objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                        objDPArr2[0].Value = p_strEmpID;
                        objDPArr2[1].Value = p_lngArchivedIDArr[i];

                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr2);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.Int64 };
                    object[][] objValues = new object[2][];

                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_lngArchivedIDArr.Length];//初始化

                    }
                    for (int k1 = 0; k1 < p_lngArchivedIDArr.Length; k1++)
                    {
                        objValues[0][k1] = p_strEmpID;
                        objValues[1][k1] = p_lngArchivedIDArr[k1];
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

        /// <summary>
        /// 病案归档
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpID">归档人ID</param>
        /// <param name="p_strRegisterIDArr">病人入院登记号</param>
        /// <param name="p_dtmOutDateArr">病人出院日期</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngArchiving(string p_strEmpID, string[] p_strRegisterIDArr, DateTime[] p_dtmOutDateArr)
        {
            long lngRes = 0;
            if (p_strRegisterIDArr == null || p_strRegisterIDArr.Length <= 0
                || p_dtmOutDateArr == null || p_dtmOutDateArr.Length < 0)
            {
                return -1;
            }
            try
            {
                int[] intStatusArr = null;
                m_lngGetArchivedStatus(p_strRegisterIDArr, out intStatusArr);
                if (intStatusArr != null && intStatusArr.Length > 0)//某些病人已存在记录，不能直接插入数据
                {
                    return -99;
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSQL = @"insert into T_EMR_CASEARCHIVING
values
  (?,?," + clsDatabaseSQLConvert.s_StrGetServDateFuncName + @",?,1,1,1,?)";

                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_strRegisterIDArr.Length; i++)
                    {
                        lngRes = objSign.m_lngGetSequenceValue("SEQ_CASEARCHIVING", out lngSequence);
                        IDataParameter[] objDPArr2 = null;
                        objHRPServ.CreateDatabaseParameter(4, out objDPArr2);
                        objDPArr2[0].Value = lngSequence;
                        objDPArr2[1].Value = p_strRegisterIDArr[i];
                        objDPArr2[2].Value = p_strEmpID;
                        objDPArr2[3].DbType = DbType.DateTime;
                        objDPArr2[3].Value = p_dtmOutDateArr[i];

                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr2);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Int64, DbType.String, DbType.String, DbType.Date };
                    object[][] objValues = new object[4][];

                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_strRegisterIDArr.Length];//初始化


                    }
                    for (int k1 = 0; k1 < p_strRegisterIDArr.Length; k1++)
                    {
                        lngRes = objSign.m_lngGetSequenceValue("SEQ_CASEARCHIVING", out lngSequence);
                        objValues[0][k1] = lngSequence;
                        objValues[1][k1] = p_strRegisterIDArr[k1];
                        objValues[2][k1] = p_strEmpID;
                        objValues[3][k1] = p_dtmOutDateArr[k1];
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

        /// <summary>
        /// 获取病案当前状态

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterIDArr">入院登记号</param>
        /// <param name="p_intArchivedStatus">病案状态</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetArchivedStatus(string[] p_strRegisterIDArr, out int[] p_intArchivedStatus)
        {
            p_intArchivedStatus = null;
            if (p_strRegisterIDArr == null || p_strRegisterIDArr.Length <= 0)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                StringBuilder stbSQL = new StringBuilder(100);
                for (int i = 0; i < p_strRegisterIDArr.Length; i++)
                {
                    if (i < p_strRegisterIDArr.Length - 1)
                    {
                        stbSQL.Append("?,");
                    }
                    else
                    {
                        stbSQL.Append("?");
                    }
                }
                string strSQL = @"select t.archivedstatus_int
  from T_EMR_CASEARCHIVING t
 where t.registerid_chr in (" + stbSQL.ToString() + ")";

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(p_strRegisterIDArr.Length, out objDPArr2);
                for (int j1 = 0; j1 < p_strRegisterIDArr.Length; j1++)
                {
                    objDPArr2[j1].Value = p_strRegisterIDArr[j1];
                }

                DataTable dtbResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr2);

                if (lngRes > 0 && dtbResult != null)
                {
                    int rowsCount = dtbResult.Rows.Count;
                    if (rowsCount <= 0)
                    {
                        return -1;
                    }
                    List<int> intStatus = new List<int>();
                    for (int j2 = 0; j2 < rowsCount; j2++)
                    {
                        int intTemp = 0;
                        if (int.TryParse(dtbResult.Rows[j2][0].ToString(), out intTemp))
                        {
                            intStatus.Add(intTemp);
                        }
                    }
                    p_intArchivedStatus = intStatus.ToArray();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region 设置病案等级
        /// <summary>
        /// 设置病案等级
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngArchivedIDArr">归档流水号</param>
        /// <param name="p_intCaseClass">病案等级</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetCaseLevel(long[] p_lngArchivedIDArr, int p_intCaseClass)
        {
            long lngRes = 0;
            if (p_lngArchivedIDArr == null || p_lngArchivedIDArr.Length <= 0
                || (p_intCaseClass != 0 && p_intCaseClass != 1 && p_intCaseClass != 2))
            {
                return -1;
            }

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"update T_EMR_CASEARCHIVING t
   set t.CASELEVEL_INT = ?
 where t.archivedid_int = ? and (ARCHIVEDSTATUS_INT=1 or ARCHIVEDSTATUS_INT=2)";

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_lngArchivedIDArr.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;
                        objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                        objDPArr2[0].Value = p_intCaseClass;
                        objDPArr2[1].Value = p_lngArchivedIDArr[i];

                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr2);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.Int64 };
                    object[][] objValues = new object[2][];

                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_lngArchivedIDArr.Length];//初始化


                    }
                    for (int k1 = 0; k1 < p_lngArchivedIDArr.Length; k1++)
                    {
                        objValues[0][k1] = p_intCaseClass;
                        objValues[1][k1] = p_lngArchivedIDArr[k1];
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

        #region 获取所有科室的出院病人归档情况
        /// <summary>
        /// 获取所有科室出院病人归档情况

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtmStart">查询时间范围－开始时间</param>
        /// <param name="p_dtmEnd">查询时间范围－结束时间</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllArchivingOutPatient(DateTime p_dtmStart, DateTime p_dtmEnd, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select re.REGISTERID_CHR,
       re.DEPTID_CHR,
       re.AREAID_CHR,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       re.PATIENTID_CHR,
       pa.LASTNAME_VCHR,
       pa.SEX_CHR,
       dept.deptname_vchr deptname,
       ar.archivedid_int,
       ar.archiveddate_dat,
       ar.archiveuser_chr,
       ar.status_int,
       ar.archivedstatus_int,
       ar.caselevel_int,
       le.MODIFY_DAT outdate_dat,
       ar.lastname_vchr archiveusername
  from T_OPR_BIH_LEAVE le
 inner join t_opr_bih_register re on le.registerid_chr = re.registerid_chr and re.STATUS_INT = 1
 inner join t_bse_deptdesc dept on dept.deptid_chr = re.AREAID_CHR
 inner join t_opr_bih_registerdetail pa on le.REGISTERID_CHR =
                                           pa.REGISTERID_CHR
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           le.registerid_chr
  left outer join (select cr.*, emp.LASTNAME_VCHR
                     from T_EMR_CASEARCHIVING cr
                     left outer join T_BSE_EMPLOYEE emp on emp.empid_chr =
                                                           cr.ARCHIVEUSER_CHR
                    where cr.status_int = 1) ar on ar.registerid_chr =
                                                   le.registerid_chr
 where le.status_int = 1
   and le.PSTATUS_INT = 1 
   and le.modify_dat between ? and ?
 order by re.DEPTID_CHR asc,outdate_dat desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //赋值


                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取指定科室的出院病人归档情况

        /// <summary>
        /// 获取指定科室的出院病人归档情况

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_strDeptName">科室名称</param>
        /// <param name="p_dtmStart">查询时间范围－开始时间</param>
        /// <param name="p_dtmEnd">查询时间范围－结束时间</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSepcifyDeptArchivingOutPatient(string p_strDeptID, string p_strDeptName, DateTime p_dtmStart, DateTime p_dtmEnd, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strDeptID) || string.IsNullOrEmpty(p_strDeptName))
            {
                return -1;
            }

            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select re.REGISTERID_CHR,
       re.DEPTID_CHR,
       re.AREAID_CHR,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       re.PATIENTID_CHR,
       pa.LASTNAME_VCHR,
       pa.SEX_CHR,
       ar.archivedid_int,
       ar.archiveddate_dat,
       ar.archiveuser_chr,
       ar.status_int,
       ar.archivedstatus_int,
       ar.caselevel_int,
       le.MODIFY_DAT outdate_dat,
       dept.deptname_vchr deptname,
       ar.lastname_vchr archiveusername
  from T_OPR_BIH_LEAVE le
 inner join t_opr_bih_register re on le.registerid_chr = re.registerid_chr
                                 and re.AREAID_CHR = ? and re.STATUS_INT = 1
 inner join t_bse_deptdesc dept on dept.deptid_chr = ?
 inner join t_opr_bih_registerdetail pa on le.REGISTERID_CHR =
                                           pa.REGISTERID_CHR
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           le.registerid_chr
  left outer join (select cr.*, emp.LASTNAME_VCHR
                     from T_EMR_CASEARCHIVING cr
                     left outer join T_BSE_EMPLOYEE emp on emp.empid_chr =
                                                           cr.ARCHIVEUSER_CHR
                    where cr.status_int = 1) ar on ar.registerid_chr =
                                                   le.registerid_chr
 where le.status_int = 1
   and le.PSTATUS_INT = 1
   and le.modify_dat between ? and ?
 order by re.DEPTID_CHR asc,outdate_dat desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                //赋值

                objDPArr[0].Value = p_strDeptID;
                objDPArr[1].Value = p_strDeptID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmStart;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmEnd;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取指定科室的出院病人归档情况

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeptIDArr">科室ID列表</param>
        /// <param name="p_dtmStart">查询时间范围－开始时间</param>
        /// <param name="p_dtmEnd">查询时间范围－结束时间</param>
        /// <param name="p_dtbResult">返回结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSepcifyDeptArchivingOutPatient(string[] p_strDeptIDArr, DateTime p_dtmStart, DateTime p_dtmEnd, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            if (p_strDeptIDArr == null || p_strDeptIDArr.Length <= 0)
            {
                return -1;
            }

            try
            {
                StringBuilder sbDept = new StringBuilder(" and re.AREAID_CHR in (", 100);
                for (int i1 = 0; i1 < p_strDeptIDArr.Length; i1++)
                {
                    if (i1 != p_strDeptIDArr.Length - 1)
                    {
                        sbDept.Append("?,");
                    }
                    else
                    {
                        sbDept.Append("?)");
                    }
                }

                clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSQL = @"select re.REGISTERID_CHR,
       re.DEPTID_CHR,
       re.AREAID_CHR,
       rehis.emrinpatientid,
       rehis.emrinpatientdate,
       rehis.hisinpatientid_chr,
       rehis.hisinpatientdate,
       re.PATIENTID_CHR,
       pa.LASTNAME_VCHR,
       pa.SEX_CHR,
       ar.archivedid_int,
       ar.archiveddate_dat,
       ar.archiveuser_chr,
       ar.status_int,
       ar.archivedstatus_int,
       ar.caselevel_int,
       le.MODIFY_DAT outdate_dat,
       dept.deptname_vchr deptname,
       ar.lastname_vchr archiveusername
  from T_OPR_BIH_LEAVE le
 inner join t_opr_bih_register re on le.registerid_chr = re.registerid_chr and re.STATUS_INT = 1
 inner join t_bse_deptdesc dept on dept.deptid_chr = re.AREAID_CHR
 inner join t_opr_bih_registerdetail pa on le.REGISTERID_CHR =
                                           pa.REGISTERID_CHR
 inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
                                           le.registerid_chr
  left outer join (select cr.*, emp.LASTNAME_VCHR
                     from T_EMR_CASEARCHIVING cr
                     left outer join T_BSE_EMPLOYEE emp on emp.empid_chr =
                                                           cr.ARCHIVEUSER_CHR
                    where cr.status_int = 1) ar on ar.registerid_chr =
                                                   le.registerid_chr
 where le.status_int = 1
   and le.PSTATUS_INT = 1
   and le.modify_dat between ? and ?
   " + sbDept.ToString() + @"   
 order by re.DEPTID_CHR asc,outdate_dat desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(p_strDeptIDArr.Length + 2, out objDPArr);
                //赋值

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStart;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEnd;
                for (int j1 = 0; j1 < p_strDeptIDArr.Length; j1++)
                {
                    objDPArr[j1 + 2].Value = p_strDeptIDArr[j1];
                }

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 检查当前病人病历是否只读

        /// <summary>
        /// 检查当前病人病历是否只读

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_objArchivingValue">病案归档内容</param>
        /// <returns></returns>
 //       [AutoComplete]
 //       public long m_lngCheckFormReadOnly(string p_strRegisterID, out clsEMR_CaseArchivingValue p_objArchivingValue)
 //       {
 //           p_objArchivingValue = null;

 //           if (string.IsNullOrEmpty(p_strRegisterID))
 //               return (long)enmOperationResult.Parameter_Error;

 //           long lngRes = 0;
 //           string strSql = "";
 //           if (clsHRPTableService.bytDatabase_Selector == 0)
 //           {
 //               strSql = @"select distinct re.REGISTERID_CHR,
 //               rehis.emrinpatientid,
 //               rehis.emrinpatientdate,
 //               rehis.hisinpatientid_chr,
 //               rehis.hisinpatientdate,
 //               nvl(ar.ARCHIVEDSTATUS_INT, 0) IfArchived,
 //               (DATEDIFF(mi, le.MODIFY_DAT, getdate())) / 60 as passHour
 // from T_OPR_BIH_REGISTER re
 //inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
 //                                          re.registerid_chr
 //inner join T_OPR_BIH_LEAVE le on re.REGISTERID_CHR = le.REGISTERID_CHR
 // left join T_EMR_CASEARCHIVING ar on re.registerid_chr = ar.registerid_chr
 //where re.PSTATUS_INT = 3
 //  and re.STATUS_INT > 0
 //  and le.STATUS_INT = 1
 //  and le.PSTATUS_INT = 1
 //  and re.registerid_chr = ?";
 //           }
 //           else if (clsHRPTableService.bytDatabase_Selector == 2)
 //           {
 //               strSql = @"select distinct re.REGISTERID_CHR,
 //               re.INPATIENTID_CHR,
 //               re.INPATIENT_DAT,
 //               rehis.emrinpatientid,
 //               rehis.emrinpatientdate,
 //               rehis.hisinpatientid_chr,
 //               rehis.hisinpatientdate,
 //               nvl(ar.ARCHIVEDSTATUS_INT, 0) IfArchived,
 //               round(to_number(sysdate - le.MODIFY_DAT) * 24) passHour
 // from T_OPR_BIH_REGISTER re
 //inner join t_bse_hisemr_relation rehis on rehis.registerid_chr =
 //                                          re.registerid_chr
 //inner join T_OPR_BIH_LEAVE le on re.REGISTERID_CHR = le.REGISTERID_CHR
 // left join T_EMR_CASEARCHIVING ar on re.registerid_chr = ar.registerid_chr
 //where re.PSTATUS_INT = 3
 //  and re.STATUS_INT > 0
 //  and le.STATUS_INT = 1
 //  and le.PSTATUS_INT = 1
 //  and re.registerid_chr = ?";
 //           }
 //           try
 //           {
 //               IDataParameter[] objDPArr = null;
 //               new clsHRPTableService().CreateDatabaseParameter(1, out objDPArr);

 //               objDPArr[0].Value = p_strRegisterID;

 //               DataTable dt = new DataTable();
 //               lngRes = new clsHRPTableService().lngGetDataTableWithParameters(strSql, ref dt, objDPArr);
 //               if (lngRes > 0 && dt.Rows.Count == 1)
 //               {
 //                   p_objArchivingValue = new clsEMR_CaseArchivingValue();
 //                   p_objArchivingValue.m_intARCHIVEDSTATUS_INT = Convert.ToInt32(dt.Rows[0]["IfArchived"]);
 //                   p_objArchivingValue.m_strREGISTERID_CHR = dt.Rows[0]["REGISTERID_CHR"].ToString();
 //                   p_objArchivingValue.m_intPassHour = Convert.ToInt32(dt.Rows[0]["passHour"]);
 //               }
 //           }
 //           catch (Exception objEx)
 //           {
 //               string strTmp = objEx.Message;
 //               com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
 //               bool blnRes = objLogger.LogError(objEx);
 //           }
 //           return lngRes;
 //       }
        #endregion

        #region m_lngCheckFormReadOnly
        /// <summary>
        /// m_lngCheckFormReadOnly
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_blnIsWorkDaysOnly"></param>
        /// <param name="p_objArchivingValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckFormReadOnly(string p_strRegisterID, bool p_blnIsWorkDaysOnly, out clsEMR_CaseArchivingValue p_objArchivingValue)
        {
            string Sql = string.Empty;
            p_objArchivingValue = null;

            Sql = @"select distinct re.registerid_chr,
                                    re.inpatientid_chr,
                                    re.inpatient_dat,
                                    rehis.emrinpatientid,
                                    rehis.emrinpatientdate,
                                    rehis.hisinpatientid_chr,
                                    rehis.hisinpatientdate,
                                    nvl(ar.archivedstatus_int, 0) ifarchived,
                                    le.modify_dat
                      from t_opr_bih_register re
                     inner join t_bse_hisemr_relation rehis
                        on rehis.registerid_chr = re.registerid_chr
                     inner join t_opr_bih_leave le
                        on re.registerid_chr = le.registerid_chr
                      left join t_emr_casearchiving ar
                        on re.registerid_chr = ar.registerid_chr
                     where re.pstatus_int = 3
                       and re.status_int > 0
                       and le.status_int = 1
                       and le.pstatus_int = 1
                       and re.registerid_chr = ?";

            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            svc.CreateDatabaseParameter(1, out parm);
            parm[0].Value = p_strRegisterID;
            DataTable dt = null;
            svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                p_objArchivingValue = new clsEMR_CaseArchivingValue();
                p_objArchivingValue.m_intARCHIVEDSTATUS_INT = Convert.ToInt32(dt.Rows[0]["IfArchived"]);
                p_objArchivingValue.m_strREGISTERID_CHR = dt.Rows[0]["REGISTERID_CHR"].ToString();
                DateTime time = Convert.ToDateTime(dt.Rows[0]["modify_dat"]);
                DateTime time2 = DateTime.Now;
                TimeSpan span;

                int num2;
                int num3;
                DateTime time3;
                int num4;
                TimeSpan span2;

                if (p_blnIsWorkDaysOnly == false)
                {
                    // if (((time.DayOfWeek == 6) == 0) != null)
                    if (time.DayOfWeek != DayOfWeek.Saturday)
                    {
                        goto Label_0135;
                    }
                    time = Convert.ToDateTime(time.AddDays(2.0).ToString("yyyy-MM-dd 00:00:00"));
                    goto Label_016F;
                Label_0135:
                    //if (((&time.DayOfWeek == 0) == 0) != null)
                    if (time.DayOfWeek != DayOfWeek.Sunday)
                    {
                        goto Label_016F;
                    }
                    time = Convert.ToDateTime(time.AddDays(1.0).ToString("yyyy-MM-dd 00:00:00"));
                Label_016F:
                    //if (((&time.DayOfWeek == 0) == 0) != null)
                    if (time.DayOfWeek != DayOfWeek.Sunday)
                    {
                        goto Label_01AB;
                    }
                    time2 = Convert.ToDateTime(time.AddDays(-2.0).ToString("yyyy-MM-dd 23:59:59"));
                    goto Label_01E5;
                Label_01AB:
                    //if (((&time.DayOfWeek == 6) == 0) != null)
                    if (time.DayOfWeek != DayOfWeek.Saturday)
                    {
                        goto Label_01E5;
                    }
                    time2 = Convert.ToDateTime(time.AddDays(-1.0).ToString("yyyy-MM-dd 23:59:59"));
                Label_01E5:
                    // if (((time >= time2) == 0) != null)
                    if (time < time2)
                    {
                        goto Label_0207;
                    }
                    p_objArchivingValue.m_intPassHour = 0;
                    goto Label_0297;
                Label_0207:
                    span = time2 - time;
                    num2 = (int)span.TotalDays;
                    num3 = 0;
                    time3 = DateTime.MinValue;
                    num4 = 0;
                    goto Label_0268;
                Label_022C:
                    time3 = time.AddDays((double)num4);
                    // if (((&time3.DayOfWeek == 6) ? 0 : ((&time3.DayOfWeek == 0) == 0)) != null)
                    if (time3.DayOfWeek != DayOfWeek.Saturday && time3.DayOfWeek != DayOfWeek.Sunday)
                    {
                        goto Label_0261;
                    }
                    num3 += 1;
                Label_0261:
                    num4 += 1;
                Label_0268:
                    //if ((num4 < num2) != null)
                    if (num4 < num2)
                    {
                        goto Label_022C;
                    }
                    span2 = time2 - time;
                    p_objArchivingValue.m_intPassHour = (int)(span2.TotalHours - ((double)(num3 * 0x18)));
                Label_0297:
                    return 1;
                }
                else
                {
                    span = time2 - time;
                    p_objArchivingValue.m_intPassHour = (int)span.TotalHours;
                }
                return 1;
            }
            return 0;
        }
        #endregion

    }
}
