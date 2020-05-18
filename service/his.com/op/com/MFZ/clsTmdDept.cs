using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.iCare.middletier.MFZ
{

    #region 诊区部门Svc
    /// <summary>
    /// 诊区部门Svc
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTmdDeptSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL
        private const string m_sqlInsert = @"insert into t_mfz_bse_dept (deptid_chr, diagnoseareaid_int, deptnameshort_chr, summary_vchr)
                                                                         values(?, 000999, ?, ?)";
        private const string m_sqlUpdate = @"UPDATE T_MFZ_BSE_DEPT SET DEPTID_CHR =? ,DEPTNAMESHORT_CHR=?, SUMMARY_VCHR=?   WHERE  DEPTID_CHR=?";
        private const string m_sqlDelete = @"DELETE T_MFZ_BSE_DEPT WHERE DEPTID_CHR=?";
        private const string m_sqlFindByDeptID = @"SELECT a.*,b.deptname_vchr FROM T_MFZ_BSE_DEPT a,t_bse_deptdesc b
                                                                where a.deptid_chr=b.deptid_chr WHERE a.DEPTID_CHR=?";
        private const string m_sqlFindAll = @"SELECT a.*,b.deptname_vchr FROM T_MFZ_BSE_DEPT a,t_bse_deptdesc b
                                                where a.deptid_chr=b.deptid_chr";
        private const string m_strTableName = "T_MFZ_BSE_DEPT";
        private const string m_strPrimaryKey = "DEPTID_CHR";
        #endregion

        #region 构造实例和构造参数列表

        [AutoComplete]
        private void ConstructVO(DataRow p_dtrSource, ref clsMFZDeptVO objReader)
        {
            //ROOMID_INT,ROOMID_INT, ROOMNAME_VCHR, DEPTID_CHR, SUMMARY_VCHR
            objReader.m_strDeptID = p_dtrSource["DEPTID_CHR"].ToString();
            objReader.m_strDeptName = p_dtrSource["DEPTNAME_VCHR"].ToString();
            objReader.m_strDeptNameShort = p_dtrSource["DEPTNAMESHORT_CHR"].ToString();
            objReader.m_strSummary = p_dtrSource["SUMMARY_VCHR"].ToString();
        }

        [AutoComplete]
        private clsMFZDeptVO[] ConstructVOArr(DataTable dtbResult)
        {
            clsMFZDeptVO[] p_objResultArr = new clsMFZDeptVO[dtbResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZDeptVO();
                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
            }
            return p_objResultArr;
        }

        [AutoComplete]
        private IDataParameter[] GetInsertDataParameterArr(clsMFZDeptVO objReader)
        {
            IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                           objReader.m_strDeptID,
                           objReader.m_strDeptNameShort,
                           objReader.m_strSummary
            });
            return objODPArr;
        }

        [AutoComplete]
        private System.Data.IDataParameter[] GetUpdateDataParameterArr(clsMFZDeptVO objReader, string oldDeptID)
        {
            System.Data.IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                           objReader.m_strDeptID,
                           objReader.m_strDeptNameShort,
                           objReader.m_strSummary,
                           oldDeptID
            });
            return objODPArr;
        }
        #endregion

        #region INSERT

        [AutoComplete]
        public long m_lngInsert(clsMFZDeptVO objReader)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = GetInsertDataParameterArr(objReader);

                long lngRecEff = -1;
                //往表增加记录

                lngRes = objHRPSvc.lngExecuteParameterSQL(m_sqlInsert, ref lngRecEff, paramsArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        #endregion

        #region UPDATE

        [AutoComplete]
        public long m_lngUpdate(clsMFZDeptVO objReader, string p_strOldDeptID)
        {
            long lngRes = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = GetUpdateDataParameterArr(objReader, p_strOldDeptID);
                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(m_sqlUpdate, ref lngRecEff, paramsArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }


        #endregion

        #region DELETE
        [AutoComplete]
        public long m_lngDelete(string deptID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = CreateParms.Parms(new object[] { deptID });

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(m_sqlDelete, ref lngRecEff, paramsArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        #endregion

        #region FIND

        /// <summary>
        /// 根据诊区ID，查找科室集合

        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindByDiagnoseAreaID(int p_intDiagnoseAreaId, int p_intSchemeId, out clsMFZDeptVO[] p_objResultArr)
        {
//            string sql = @"
//                         SELECT distinct a.deptid_chr,b.diagnoseareaid_int,''deptnameshort_chr ,'' summary_vchr,c.deptname_vchr
//                       FROM t_mfz_bse_doctor a, t_mfz_bse_room b, t_bse_deptdesc c
//                      WHERE a.deptid_chr = c.deptid_chr
//                        AND a.roomid_int = b.roomid_int
//                        and a.scheme_seq_int=?
//                        AND b.diagnoseareaid_int = ?
//                 ";
                string sql = @"
                                    SELECT a.*, b.deptname_vchr
                                      FROM (SELECT DISTINCT d.diagnoseareaid_int, '' deptnameshort_chr,
                                                            '' summary_vchr,
                                                            DECODE (c.deptid_chr,
                                                                    NULL, d.deptid_chr,
                                                                    c.deptid_chr
                                                                   ) deptid_chr
                                                       FROM (SELECT DISTINCT b.roomid_int, b.deptid_chr
                                                                        FROM t_mfz_bse_room a, t_mfz_bse_doctor b
                                                                       WHERE a.diagnoseareaid_int = ?
                                                                         AND scheme_seq_int = ?
                                                                         AND a.roomid_int = b.roomid_int) c,
                                                            t_mfz_bse_room d
                                                      WHERE c.roomid_int(+) = d.roomid_int
                                                            AND d.diagnoseareaid_int = ?) a,
                                           t_bse_deptdesc b
                                     WHERE a.deptid_chr = b.deptid_chr
                            ";

            long lngRes = 0;
            p_objResultArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { p_intDiagnoseAreaId, p_intSchemeId, p_intDiagnoseAreaId });
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = ConstructVOArr(dtbResult);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 通过诊室ID，查找科室VO
        /// </summary>
        /// <param name="p_strDeptID"></param>
        /// <param name="objReader"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindByDeptID(string p_strDeptID, out clsMFZDeptVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { p_strDeptID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_sqlFindByDeptID, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZDeptVO();
                    ConstructVO(dtbResult.Rows[0], ref objReader);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 查找全部部门
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFind(out clsMFZDeptVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(m_sqlFindAll, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = ConstructVOArr(dtbResult);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        #endregion

    }
    #endregion
}
