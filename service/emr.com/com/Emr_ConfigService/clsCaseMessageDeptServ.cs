using System;
using System.Collections;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;

namespace com.digitalwave.Emr.ConfigService
{
    /// <summary>
    /// clsCaseMessage 的摘要说明。
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCaseMessageDeptServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 处理特殊科室
        [AutoComplete]
        public long m_lngSetSpDept(clsEmrCaseMessDept_VO[] p_objrCaseMessDept)
        {
            if (p_objrCaseMessDept == null || p_objrCaseMessDept.Length == 0) return -1;
            long lngRes = 0;
            ArrayList arlInsert = new ArrayList(5);
            ArrayList arlUpdate = new ArrayList(5);
            ArrayList arlDelete = new ArrayList(5);
            for (int i = 0; i < p_objrCaseMessDept.Length; i++)
            {
                if (p_objrCaseMessDept[i].m_intStatus == -1)
                    arlDelete.Add(p_objrCaseMessDept[i]);
                else if (p_objrCaseMessDept[i].m_intStatus == 0)
                    arlInsert.Add(p_objrCaseMessDept[i]);
                else if (p_objrCaseMessDept[i].m_intStatus == 1)
                    arlUpdate.Add(p_objrCaseMessDept[i]);
            }
            lngRes = 1;
            if (arlInsert.Count > 0)
            {
                lngRes = m_lngInsertSpDept((clsEmrCaseMessDept_VO[])arlInsert.ToArray(typeof(clsEmrCaseMessDept_VO)));
            }
            if (lngRes > 0)
            {
                if (arlUpdate.Count > 0)
                {
                    lngRes = m_lngUpdateSpDept((clsEmrCaseMessDept_VO[])arlUpdate.ToArray(typeof(clsEmrCaseMessDept_VO)));
                }
                if (lngRes > 0)
                {
                    if (arlDelete.Count > 0)
                    {
                        lngRes = m_lngDeleteSpDept((clsEmrCaseMessDept_VO[])arlDelete.ToArray(typeof(clsEmrCaseMessDept_VO)));
                    }
                    if (lngRes <= 0)
                        throw new Exception("m_lngDeletepDept-删除失败");
                }
                else
                    throw new Exception("m_lngUpdateSpDept-更新失败");
            }

            return lngRes;
        }
        [AutoComplete]
        private long m_lngInsertSpDept(clsEmrCaseMessDept_VO[] p_objrCaseMessDept)
        {
            if (p_objrCaseMessDept == null || p_objrCaseMessDept.Length == 0) return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSql = @"insert into t_emr_message_typepower
  (cuetypeid_int, deptid_chr, curtime_int)
values (?, ?, ?)";//3

                long lngRff = 0;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_objrCaseMessDept.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                        objDPArr2[0].Value = p_objrCaseMessDept[i].m_strId;
                        objDPArr2[1].Value = p_objrCaseMessDept[i].m_strDeptId;
                        objDPArr2[2].Value = p_objrCaseMessDept[i].m_intCurTime;

                        lngRff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRff, objDPArr2);

                    }
                }
                else
                {
                    if (p_objrCaseMessDept.Length > 0)
                    {
                        DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.String, DbType.Int32 };
                        object[][] objValues = new object[3][];
                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[p_objrCaseMessDept.Length];//初始化
                        }

                        for (int k1 = 0; k1 < p_objrCaseMessDept.Length; k1++)
                        {
                            objValues[0][k1] = p_objrCaseMessDept[k1].m_strId;
                            objValues[1][k1] = p_objrCaseMessDept[k1].m_strDeptId;
                            objValues[2][k1] = p_objrCaseMessDept[k1].m_intCurTime;
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, objValues, dbTypes);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            return lngRes;
        }
        [AutoComplete]
        private long m_lngUpdateSpDept(clsEmrCaseMessDept_VO[] p_objrCaseMessDept)
        {
            if (p_objrCaseMessDept == null || p_objrCaseMessDept.Length == 0) return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSql = @"update t_emr_message_typepower set curtime_int = ? where cuetypeid_int = ? and deptid_chr=?";//3

                long lngRff = 0;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_objrCaseMessDept.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                        objDPArr2[0].Value = p_objrCaseMessDept[i].m_intCurTime;
                        objDPArr2[1].Value = p_objrCaseMessDept[i].m_strId;
                        objDPArr2[2].Value = p_objrCaseMessDept[i].m_strDeptId;

                        lngRff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRff, objDPArr2);

                    }
                }
                else
                {
                    if (p_objrCaseMessDept.Length > 0)
                    {
                        DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.Int32, DbType.String };
                        object[][] objValues = new object[3][];
                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[p_objrCaseMessDept.Length];//初始化
                        }

                        for (int k1 = 0; k1 < p_objrCaseMessDept.Length; k1++)
                        {
                            objValues[0][k1] = p_objrCaseMessDept[k1].m_intCurTime;
                            objValues[1][k1] = p_objrCaseMessDept[k1].m_strId;
                            objValues[2][k1] = p_objrCaseMessDept[k1].m_strDeptId;
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, objValues, dbTypes);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            return lngRes;
        }
        [AutoComplete]
        private long m_lngDeleteSpDept(clsEmrCaseMessDept_VO[] p_objrCaseMessDept)
        {
            if (p_objrCaseMessDept == null || p_objrCaseMessDept.Length == 0) return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSql = @"delete from t_emr_message_typepower where cuetypeid_int = ? and deptid_chr=?";//2

                long lngRff = 0;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_objrCaseMessDept.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                        objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                        objDPArr2[0].Value = p_objrCaseMessDept[i].m_strId;
                        objDPArr2[1].Value = p_objrCaseMessDept[i].m_strDeptId;

                        lngRff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngRff, objDPArr2);

                    }
                }
                else
                {
                    if (p_objrCaseMessDept.Length > 0)
                    {
                        DbType[] dbTypes = new DbType[] { DbType.Int32, DbType.String };
                        object[][] objValues = new object[2][];
                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[p_objrCaseMessDept.Length];//初始化
                        }

                        for (int k1 = 0; k1 < p_objrCaseMessDept.Length; k1++)
                        {
                            objValues[0][k1] = p_objrCaseMessDept[k1].m_strId;
                            objValues[1][k1] = p_objrCaseMessDept[k1].m_strDeptId;
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, objValues, dbTypes);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetAllCueType(out clsEmrMessageCuetype_VO[] p_objCueTypes)
        {
            p_objCueTypes = null;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSql = @"select cuetypeid_int,
       cuetypeno_int,
       cuedesc_vchr
  from t_emr_message_cuetype
  order by index_int";//1

                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSql, ref dtbValue);
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objCueTypes = new clsEmrMessageCuetype_VO[intRowCount];
                    DataRow objRow = null;
                    int intTemp = 0;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbValue.Rows[i];
                        clsEmrMessageCuetype_VO objCuetype = new clsEmrMessageCuetype_VO();
                        //if (int.TryParse(objRow["cuestate_int"].ToString(), out intTemp))
                        //    objCuetype.m_intCueState = intTemp;
                        if (int.TryParse(objRow["cuetypeno_int"].ToString(), out intTemp))
                            objCuetype.m_intCueTypeNo = intTemp;
                        else
                            objCuetype.m_intCueTypeNo = -1;
                        //if (int.TryParse(objRow["curtime_int"].ToString(), out intTemp))
                        //    objCuetype.m_intCurTime = intTemp;
                        //else
                        //    objCuetype.m_intCurTime = -1;
                        //if (int.TryParse(objRow["index_int"].ToString(), out intTemp))
                        //    objCuetype.m_intIndex = intTemp;
                        //if (int.TryParse(objRow["minneedrecordcount_int"].ToString(), out intTemp))
                        //    objCuetype.m_intMinNeedRecordCount = intTemp;
                        //else
                        //    objCuetype.m_intMinNeedRecordCount = 1;
                        //if (int.TryParse(objRow["cueparamstype_int"].ToString(), out intTemp))
                        //    objCuetype.m_intCueParamsType = intTemp;
                        //else
                        //    objCuetype.m_intCueParamsType = -1;
                        //if (int.TryParse(objRow["cuetimestarttype_int"].ToString(), out intTemp))
                        //    objCuetype.m_intCueTimeStartType = intTemp;
                        //else
                        //    objCuetype.m_intCueTimeStartType = -1;
                        //objCuetype.m_strCueAfterMessage = objRow["cueaftermessage_vchr"].ToString();
                        //objCuetype.m_strCueBeforeMessage = objRow["cuebeforemessage_vchr"].ToString();
                        //objCuetype.m_strCueCountSql = objRow["cuecountsql_vchr"].ToString();
                        objCuetype.m_strCueDesc = objRow["cuedesc_vchr"].ToString();
                        objCuetype.m_strCueTypeId = objRow["cuetypeid_int"].ToString();
                        p_objCueTypes[i] = objCuetype;
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_lngGetSpDept(string p_strCueTypeNo, out DataTable p_dtbResult, out string p_strCurTypeId)
        {
            p_dtbResult = new DataTable();
            p_strCurTypeId = "";
            if (string.IsNullOrEmpty(p_strCueTypeNo)) return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPServ = new clsHRPTableService();

                string strSql = @"select de.deptname_vchr,
       t.curtime_int,
       t.cuetypeno_int,
       de.deptid_chr,
       (select cuetypeid_int
          from t_emr_message_cuetype
         where cuetypeno_int = ?) cuetypeid_int
  from t_bse_deptdesc de
  left join (select mt.deptid_chr,
                    mc.cuetypeno_int,
                    mt.curtime_int,
                    mt.cuetypeid_int
               from t_emr_message_typepower mt
              inner join t_emr_message_cuetype mc on mt.cuetypeid_int =
                                                     mc.cuetypeid_int
              where mc.cuetypeno_int = ?) t on de.deptid_chr =
                                                t.deptid_chr
 where de.inpatientoroutpatient_int = 1
   and de.status_int = 1
 order by de.deptid_chr";//1

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strCueTypeNo;
                objDPArr[1].Value = p_strCueTypeNo;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, objDPArr);
                if (lngRes > 0 && p_dtbResult.Rows.Count > 0)
                    p_strCurTypeId = p_dtbResult.Rows[0]["cuetypeid_int"].ToString();
                p_dtbResult.Columns.Remove("cuetypeid_int");
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            return lngRes;
        }
        #endregion 处理特殊科室
    }
}
