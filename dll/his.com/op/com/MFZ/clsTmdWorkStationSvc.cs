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

    #region 工作站Svc
    /// <summary>
    /// 工作站Svc
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTmdWorkStationSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL
        // WORKSTATIONID_INT, WORKSTATIONDESC_VCHR,WORKSTATIONNAME_VCHR, ROOMID_INT, SUMMARY_VCHR
        private const string m_strInsertSql = @"INSERT INTO T_MFZ_BSE_WORKSTATION(WORKSTATIONID_INT, WORKSTATIONDESC_VCHR, WORKSTATIONNAME_VCHR,ROOMID_INT,SUMMARY_VCHR)
                                                       VALUES (?, ?, ?,?,?)";
        private const string m_strUpdateSql = @"UPDATE T_MFZ_BSE_WORKSTATION
                                                       SET WORKSTATIONDESC_VCHR = ?,
                                                           WORKSTATIONNAME_VCHR = ?,
                                                           ROOMID_INT=?,
                                                           SUMMARY_VCHR = ?
                                                       WHERE WORKSTATIONID_INT = ?";
        private const string m_strDeleteSql = @"DELETE  T_MFZ_BSE_WORKSTATION WHERE WORKSTATIONID_INT=?";
        private const string m_strFindSql = @"SELECT * FROM T_MFZ_BSE_WORKSTATION WHERE WORKSTATIONID_INT=?";
        private const string m_strFindAllSql = @"SELECT * FROM T_MFZ_BSE_WORKSTATION";
        private const string m_strFindStationsByRoomIDSql = @"SELECT * FROM T_MFZ_BSE_WORKSTATION WHERE ROOMID_INT=? Order by WORKSTATIONID_INT ASC";

        // other table 
        private const string m_strFindStationByDoctorID = @"select a.* from t_mfz_bse_workstation a,t_mfz_bse_doctor b
                                                            where a.workstationid_int=b.workstationid_int and 
                                                            b.doctorid_chr=? and b.scheme_seq_int=?";

        private const string m_strFindDoctorStationByRoomIDSql = @"
                                                                    SELECT a.workstationid_int, a.workstationdesc_vchr, a.workstationname_vchr,
                                                                           a.roomid_int, b.*, c.lastname_vchr
                                                                      FROM t_mfz_bse_workstation a,
                                                                           (SELECT a.*, b.deptname_vchr
                                                                              FROM t_mfz_bse_doctor a, t_bse_deptdesc b
                                                                             WHERE a.deptid_chr = b.deptid_chr AND scheme_seq_int = ?) b,
                                                                           t_bse_employee c
                                                                     WHERE a.workstationid_int = b.workstationid_int(+) AND b.doctorid_chr = c.empid_chr(+)
                                                                           AND a.roomid_int = ?
                                                                  ";


        //        private const string m_strFindDoctorStationByRoomIDSql = @"  SELECT A.*,B.*,C.LASTNAME_VCHR FROM T_MFZ_BSE_WORKSTATION A,(SELECT a.*, b.deptname_vchr
        //                                                                      FROM t_mfz_bse_doctor a, t_bse_deptdesc b
        //                                                                     WHERE a.deptid_chr = b.deptid_chr AND scheme_seq_int = ?) B,T_BSE_EMPLOYEE C
        //                                                                      WHERE A.WORKSTATIONID_INT=B.WORKSTATIONID_INT(+) 
        //                                                                      AND B.DOCTORID_CHR=C.EMPID_CHR(+)
        //                                                                      AND A.ROOMID_INT=?";  // 获取医生（含名字）工作站集合
        private const string m_strFindWorkStationsByAreaID = @"SELECT *
                                                                      FROM t_mfz_bse_workstation w, t_mfz_bse_room r
                                                                      WHERE w.roomid_int = r.roomid_int(+) AND r.diagnoseareaid_int =?
                                                                     ";

        private const string m_strTableName = "T_MFZ_BSE_WORKSTATION";
        private const string m_strPrimaryKey = "WORKSTATIONID_INT";
        #endregion

        #region  构造实例和构造参数列表

        /// <summary>
        /// 构建医生诊室安排实体
        /// </summary>
        /// <param name="p_dtrSource"></param>
        /// <param name="objDoctorStation"></param>
        [AutoComplete]
        private void ConstructDoctorStationVO(DataRow p_dtrSource, ref clsMFZDoctorStationVO objDoctorStation)
        {
            clsMFZWorkStationVO objStation = new clsMFZWorkStationVO();
            objStation.m_intWorkStationID = DBAssist.ToInt32(p_dtrSource["WORKSTATIONID_INT"]);
            objStation.m_strWorkStationDesc = p_dtrSource["WORKSTATIONDESC_VCHR"].ToString();
            objStation.m_strWorkStationName = p_dtrSource["WORKSTATIONNAME_VCHR"].ToString();
            objStation.m_intRoomID = DBAssist.ToInt32(p_dtrSource["ROOMID_INT"]);
            objStation.m_strSummary = p_dtrSource["SUMMARY_VCHR"].ToString();

            clsMFZDoctorVO objDoctor = new clsMFZDoctorVO();

            if (p_dtrSource["DOCTORID_CHR"] == DBNull.Value)
            {
                objDoctor = null;
            }
            else
            {
                objDoctor.m_strDeptID = p_dtrSource["DEPTID_CHR"].ToString();
                objDoctor.m_intSchemeSeq = DBAssist.ToInt32(p_dtrSource["SCHEME_SEQ_INT"]);
                objDoctor.m_intRoomID = DBAssist.ToInt32(p_dtrSource["ROOMID_INT"]);
                objDoctor.m_intWorkStationID = DBAssist.ToInt32(p_dtrSource["WORKSTATIONID_INT"]);
                objDoctor.m_enmDoctorType = DBAssist.ToInt32(p_dtrSource["DOCTORTYPE_INT"]) == 0 ? enmMFZDoctorType.Common : enmMFZDoctorType.Expert;
                objDoctor.m_strSummary = p_dtrSource["SUMMARY_VCHR"].ToString();
                objDoctor.m_strDoctorName = p_dtrSource["LASTNAME_VCHR"].ToString().Trim();
                objDoctor.m_strDoctorID = p_dtrSource["DOCTORID_CHR"].ToString();
                if (p_dtrSource.Table.Columns.Contains("DEPTNAME_VCHR"))
                {
                    objDoctor.m_strDeptName = p_dtrSource["DEPTNAME_VCHR"].ToString();
                }
            }

            objDoctorStation.m_objDoctor = objDoctor;
            objDoctorStation.m_objStation = objStation;

        }

        [AutoComplete]
        private void ConstructVO(DataRow p_dtrSource, ref clsMFZWorkStationVO objReader)
        {
            // WORKSTATIONID_INT, WORKSTATIONDESC_VCHR,WORKSTATIONNAME_VCHR, ROOMID_INT, SUMMARY_VCHR
            objReader.m_intWorkStationID = DBAssist.ToInt32(p_dtrSource["WORKSTATIONID_INT"]);
            objReader.m_strWorkStationDesc = p_dtrSource["WORKSTATIONDESC_VCHR"].ToString();
            objReader.m_strWorkStationName = p_dtrSource["WORKSTATIONNAME_VCHR"].ToString();
            objReader.m_intRoomID = DBAssist.ToInt32(p_dtrSource["ROOMID_INT"]);
            objReader.m_strSummary = p_dtrSource["SUMMARY_VCHR"].ToString();
        }

        [AutoComplete]
        private clsMFZWorkStationVO[] ConstructVOArr(DataTable dtbResult)
        {
            clsMFZWorkStationVO[] p_objResultArr = new clsMFZWorkStationVO[dtbResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZWorkStationVO();
                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
            }
            return p_objResultArr;
        }

        /// <summary>
        /// 构造医生工作集合
        /// </summary>
        [AutoComplete]
        private clsMFZDoctorStationVO[] ConstructDoctorStationArr(DataTable dtResult)
        {
            clsMFZDoctorStationVO[] p_objResultArr = new clsMFZDoctorStationVO[dtResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZDoctorStationVO();
                ConstructDoctorStationVO(dtResult.Rows[i], ref p_objResultArr[i]);
            }
            return p_objResultArr;
        }

        [AutoComplete]
        private IDataParameter[] GetInsertDataParameterArr(clsMFZWorkStationVO objReader, int stationID)
        {
            IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                           stationID,
                           objReader.m_strWorkStationDesc,
                           objReader.m_strWorkStationName,
                           objReader.m_intRoomID,
                           objReader.m_strSummary
            });
            return objODPArr;
        }

        [AutoComplete]
        private System.Data.IDataParameter[] GetUpdateDataParameterArr(clsMFZWorkStationVO objReader)
        {
            System.Data.IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                           objReader.m_strWorkStationDesc,
                           objReader.m_strWorkStationName,
                           objReader.m_intRoomID,
                           objReader.m_strSummary,
                           objReader.m_intWorkStationID
            });
            return objODPArr;
        }
        #endregion

        #region INSERT

        [AutoComplete]
        public long m_lngInsert(clsMFZWorkStationVO objReader, out int workStationID)
        {
            long lngRes = 0;
            workStationID = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.m_lngGenerateNewID(m_strTableName, m_strPrimaryKey, out workStationID);
                if (lngRes <= 0)
                    return -1;

                System.Data.IDataParameter[] paramsArr = GetInsertDataParameterArr(objReader, workStationID);

                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(m_strInsertSql, ref lngRecEff, paramsArr);
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
        public long m_lngUpdate(clsMFZWorkStationVO objReader)
        {
            long lngRes = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = GetUpdateDataParameterArr(objReader);
                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(m_strUpdateSql, ref lngRecEff, paramsArr);
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
        public long m_lngDelete(int workStationID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = CreateParms.Parms(new object[] { workStationID });

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(m_strDeleteSql, ref lngRecEff, paramsArr);
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

        [AutoComplete]
        public long m_lngFind(int workStationID, out clsMFZWorkStationVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { workStationID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindSql, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZWorkStationVO();
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
        /// 查找某诊室的工作站集合
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFind(int roomID, out clsMFZWorkStationVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { roomID });
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindStationsByRoomIDSql, ref dtbResult, paramsArr);
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

        [AutoComplete]
        public long m_lngFind(out clsMFZWorkStationVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(m_strFindAllSql, ref dtbResult);
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

        [AutoComplete]
        public long m_lngFindByDoctorID(string doctorID, int schemeID, out clsMFZWorkStationVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { doctorID, schemeID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindStationByDoctorID, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZWorkStationVO();
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
        /// 查找诊区的工作站集合
        /// </summary>
        [AutoComplete]
        public long m_lngFindByAreaID(int areaID, out clsMFZWorkStationVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { areaID });
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindWorkStationsByAreaID, ref dtbResult, paramsArr);
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
        /// 查找医生工作站集合
        /// </summary>
        /// <param name="staionID"></param>
        /// <param name="objReader"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindDoctorStations(int roomID, int schemeID, out clsMFZDoctorStationVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { schemeID, roomID });
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindDoctorStationByRoomIDSql, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = ConstructDoctorStationArr(dtbResult);
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

    #region 诊区部门Svc
    //    /// <summary>
    //    /// 诊区部门Svc
    //    /// </summary>
    //    [Transaction(TransactionOption.Required)]
    //    [ObjectPooling(true)]
    //    public class clsTmdDeptSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    //    {
    //        #region SQL
    //        private const string m_sqlInsert = @"INSERT INTO T_MFZ_BSE_DEPT (DEPTID_CHR, DIAGNOSEAREAID_INT,DEPTNAMESHORT_CHR, SUMMARY_VCHR)
    //                                                                         VALUES(?,?,?,?)";
    //        private const string m_sqlUpdate = @"UPDATE T_MFZ_BSE_DEPT SET DEPTID_CHR =? ,DEPTNAMESHORT_CHR=?, SUMMARY_VCHR=?   WHERE  DIAGNOSEAREAID_INT=? AND DEPTID_CHR=?";
    //        private const string m_sqlDelete = @"DELETE T_MFZ_BSE_DEPT WHERE DEPTID_CHR=? and DIAGNOSEAREAID_INT=?";
    //        private const string m_sqlFindByDeptID = @"SELECT A.DEPTID_CHR, A.DIAGNOSEAREAID_INT,A.DEPTNAMESHORT_CHR, A.SUMMARY_VCHR,B.DEPTNAME_VCHR FROM T_MFZ_BSE_DEPT A,T_BSE_DEPTDESC B WHERE A.DEPTID_CHR=B.DEPTID_CHR AND A.DEPTID_CHR=?";
    //        private const string m_sqlFindByDiagnoseAreaID = @"SELECT A.DEPTID_CHR, A.DIAGNOSEAREAID_INT,A.DEPTNAMESHORT_CHR, A.SUMMARY_VCHR,B.DEPTNAME_VCHR FROM T_MFZ_BSE_DEPT A,T_BSE_DEPTDESC B WHERE A.DEPTID_CHR=B.DEPTID_CHR AND A.DIAGNOSEAREAID_INT=?";
    //        private const string m_sqlFindAll = @"SELECT A.DEPTID_CHR, A.DIAGNOSEAREAID_INT,A.DEPTNAMESHORT_CHR, A.SUMMARY_VCHR,B.DEPTNAME_VCHR FROM T_MFZ_BSE_DEPT A,T_BSE_DEPTDESC B
    //                                                                        WHERE A.DEPTID_CHR=B.DEPTID_CHR";

    //        private const string m_strTableName = "T_MFZ_BSE_DEPT";
    //        private const string m_strPrimaryKey = "DEPTID_CHR";
    //        #endregion

    //        #region 构造实例和构造参数列表
    //        [AutoComplete]
    //        private void ConstructVO(DataRow p_dtrSource, ref clsMFZDeptVO objReader)
    //        {
    //            //ROOMID_INT,ROOMID_INT, ROOMNAME_VCHR, DEPTID_CHR, SUMMARY_VCHR
    //            objReader.m_strDeptID = p_dtrSource["DEPTID_CHR"].ToString();
    //            objReader.m_intDiagnoseAreaID = DBAssist.ToInt32(p_dtrSource["DIAGNOSEAREAID_INT"].ToString());
    //            objReader.m_strDeptName = p_dtrSource["DEPTNAME_VCHR"].ToString();
    //            objReader.m_strDeptNameShort = p_dtrSource["DEPTNAMESHORT_CHR"].ToString();
    //            objReader.m_strSummary = p_dtrSource["SUMMARY_VCHR"].ToString();
    //        }

    //        [AutoComplete]
    //        private clsMFZDeptVO[] ConstructVOArr(DataTable dtbResult)
    //        {
    //            clsMFZDeptVO[] p_objResultArr = new clsMFZDeptVO[dtbResult.Rows.Count];
    //            for (int i = 0; i < p_objResultArr.Length; i++)
    //            {
    //                p_objResultArr[i] = new clsMFZDeptVO();
    //                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
    //            }
    //            return p_objResultArr;
    //        }

    //        [AutoComplete]
    //        private IDataParameter[] GetInsertDataParameterArr(clsMFZDeptVO objReader)
    //        {
    //            IDataParameter[] objODPArr = m_objConstructIDataParameterArr(
    //                           objReader.m_strDeptID,
    //                           objReader.m_intDiagnoseAreaID,
    //                           objReader.m_strDeptNameShort,
    //                           objReader.m_strSummary
    //                );
    //            return objODPArr;
    //        }

    //        [AutoComplete]
    //        private System.Data.IDataParameter[] GetUpdateDataParameterArr(clsMFZDeptVO objReader,string oldDeptID)
    //        {
    //            System.Data.IDataParameter[] objODPArr = this.m_objConstructIDataParameterArr
    //                        (
    //                           objReader.m_strDeptID,
    //                           objReader.m_strDeptNameShort,
    //                           objReader.m_strSummary,
    //                           objReader.m_intDiagnoseAreaID,
    //                           oldDeptID
    //                        );
    //            return objODPArr;
    //        }
    //        #endregion

    //        #region INSERT

    //        [AutoComplete]
    //        public long m_lngInsert(clsMFZDeptVO objReader)
    //        {
    //            long lngRes = 0;
    //            clsHRPTableService objHRPSvc = new clsHRPTableService();
    //            try
    //            {
    //                System.Data.IDataParameter[] paramsArr = GetInsertDataParameterArr(objReader);

    //                long lngRecEff = -1;
    //                //往表增加记录
    //                lngRes = objHRPSvc.lngExecuteParameterSQL(m_sqlInsert, ref lngRecEff, paramsArr);
    //                objHRPSvc.Dispose();
    //            }
    //            catch (Exception objEx)
    //            {
    //                new clsLogText().LogError(objEx);
    //            }
    //            return lngRes;
    //        }


    //        #endregion

    //        #region UPDATE

    //        [AutoComplete]
    //        public long m_lngUpdate(clsMFZDeptVO objReader,string p_strOldDeptID)
    //        {
    //            long lngRes = 0;

    //            clsHRPTableService objHRPSvc = new clsHRPTableService();
    //            try
    //            {
    //                IDataParameter[] paramsArr = GetUpdateDataParameterArr(objReader, p_strOldDeptID);
    //                long lngRecEff = -1;
    //                lngRes = objHRPSvc.lngExecuteParameterSQL(m_sqlUpdate, ref lngRecEff, paramsArr);
    //                objHRPSvc.Dispose();
    //            }
    //            catch (Exception objEx)
    //            {
    //                new clsLogText().LogError(objEx);
    //            }
    //            return lngRes;
    //        }


    //        #endregion

    //        #region DELETE
    //        [AutoComplete]
    //        public long m_lngDelete(string deptID,int areaId)
    //        {
    //            long lngRes = 0;
    //            clsHRPTableService objHRPSvc = new clsHRPTableService();
    //            try
    //            {
    //                System.Data.IDataParameter[] paramsArr = m_objConstructIDataParameterArr(deptID,areaId);

    //                long lngRecEff = -1;
    //                lngRes = objHRPSvc.lngExecuteParameterSQL(m_sqlDelete, ref lngRecEff, paramsArr);
    //                objHRPSvc.Dispose();
    //            }
    //            catch (Exception objEx)
    //            {
    //                new clsLogText().LogError(objEx);
    //            }
    //            return lngRes;
    //        }

    //        #endregion

    //        #region FIND

    //        /// <summary>
    //        /// 通过诊室ID，查找科室VO
    //        /// </summary>
    //        /// <param name="p_strDeptID"></param>
    //        /// <param name="objReader"></param>
    //        /// <returns></returns>
    //        [AutoComplete]
    //        public long m_lngFindByDeptID(string p_strDeptID, out clsMFZDeptVO objReader)
    //        {
    //            long lngRes = 0;
    //            objReader = null;

    //            clsHRPTableService objHRPSvc = new clsHRPTableService();
    //            try
    //            {
    //                IDataParameter[] paramsArr = this.m_objConstructIDataParameterArr(p_strDeptID);
    //                DataTable dtbResult = null;

    //                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_sqlFindByDeptID, ref dtbResult, paramsArr);
    //                objHRPSvc.Dispose();
    //                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
    //                {
    //                    objReader = new clsMFZDeptVO();
    //                    ConstructVO(dtbResult.Rows[0], ref objReader);
    //                }
    //            }
    //            catch (Exception objEx)
    //            {
    //                new clsLogText().LogError(objEx);
    //            }
    //            return lngRes;
    //        }

    //        /// <summary>
    //        /// 根据诊区ID，查找科室集合
    //        /// </summary>
    //        /// <param name="p_objResultArr"></param>
    //        /// <returns></returns>
    //        [AutoComplete]
    //        public long m_lngFindByDiagnoseAreaID(int p_intDiagnoseAreaId,int p_intSchemeId, out clsMFZDeptVO[] p_objResultArr)
    //        {
    //            string sql = @"
    //                                SELECT distinct a.deptid_chr,b.diagnoseareaid_int,''deptnameshort_chr ,'' summary_vchr,c.deptname_vchr
    //                              FROM t_mfz_bse_doctor a, t_mfz_bse_room b, t_bse_deptdesc c
    //                             WHERE a.deptid_chr = c.deptid_chr
    //                               AND a.roomid_int = b.roomid_int
    //                               and a.scheme_seq_int=?
    //                               AND b.diagnoseareaid_int = ?
    //                        ";

    //            long lngRes = 0;
    //            p_objResultArr = null;
    //            clsHRPTableService objHRPSvc = new clsHRPTableService();
    //            try
    //            {
    //                IDataParameter[] paramsArr = this.m_objConstructIDataParameterArr(p_intSchemeId,p_intDiagnoseAreaId);
    //                DataTable dtbResult = null;
    //                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtbResult, paramsArr);
    //                objHRPSvc.Dispose();
    //                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
    //                {
    //                    p_objResultArr = ConstructVOArr(dtbResult);
    //                }
    //            }
    //            catch (Exception objEx)
    //            {
    //                new clsLogText().LogError(objEx);
    //            }
    //            return lngRes;
    //        }

    //        /// <summary>
    //        /// 查找全部部门
    //        /// </summary>
    //        /// <param name="p_objResultArr"></param>
    //        /// <returns></returns>
    //        [AutoComplete]
    //        public long m_lngFind(out clsMFZDeptVO[] p_objResultArr)
    //        {
    //            long lngRes = 0;
    //            p_objResultArr = null;
    //            clsHRPTableService objHRPSvc = new clsHRPTableService();
    //            try
    //            {
    //                DataTable dtbResult = null;
    //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(m_sqlFindAll, ref dtbResult);
    //                objHRPSvc.Dispose();
    //                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
    //                {
    //                    p_objResultArr = ConstructVOArr(dtbResult);
    //                }
    //            }
    //            catch (Exception objEx)
    //            {
    //                new clsLogText().LogError(objEx);
    //            }
    //            return lngRes;
    //        }

    //        #endregion
    //    } 
    #endregion

    #region 诊区Svc
    /// <summary>
    /// 诊区Svc
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTmdDiagnoseAreaSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL
        private const string m_sqlInsert = @"INSERT INTO T_MFZ_BSE_DIAGNOSEAREA (DIAGNOSEAREAID_INT, DIAGNOSEAREANAME_VCHR, SUMMARY_VCHR)
                                                                         VALUES(?,?,?)";
        private const string m_sqlUpdate = @"UPDATE T_MFZ_BSE_DIAGNOSEAREA SET  DIAGNOSEAREANAME_VCHR=?, SUMMARY_VCHR=? WHERE DIAGNOSEAREAID_INT=?";
        private const string m_sqlDelete = @"DELETE T_MFZ_BSE_DIAGNOSEAREA WHERE DIAGNOSEAREAID_INT=?";
        private const string m_sqlFindByAreaID = @"SELECT DIAGNOSEAREAID_INT, DIAGNOSEAREANAME_VCHR, SUMMARY_VCHR FROM T_MFZ_BSE_DIAGNOSEAREA WHERE DIAGNOSEAREAID_INT=?";
        private const string m_sqlFindAll = @"SELECT DIAGNOSEAREAID_INT, DIAGNOSEAREANAME_VCHR, SUMMARY_VCHR FROM T_MFZ_BSE_DIAGNOSEAREA ";
        private const string m_sqlFindByRoomID = @"
                                                    SELECT A.diagnoseareaid_int, A.diagnoseareaname_vchr, a.summary_vchr
                                                      FROM t_mfz_bse_diagnosearea a,T_MFZ_BSE_ROOM b
                                                      WHERE A.diagnoseareaid_int=B.diagnoseareaid_int AND B.roomid_int=?
                                                  ";

        private const string m_strTableName = "T_MFZ_BSE_DIAGNOSEAREA";
        private const string m_strPrimaryKey = "DIAGNOSEAREAID_INT";
        #endregion

        #region 构造实例和构造参数列表
        [AutoComplete]
        private void ConstructVO(DataRow p_dtrSource, ref clsMFZDiagnoseAreaVO objReader)
        {
            objReader.m_intDiagnoseAreaID = DBAssist.ToInt32(p_dtrSource["DIAGNOSEAREAID_INT"].ToString());
            objReader.m_strDiagnoseAreaName = p_dtrSource["DIAGNOSEAREANAME_VCHR"].ToString();
            objReader.m_strSummary = p_dtrSource["SUMMARY_VCHR"].ToString();
        }

        [AutoComplete]
        private clsMFZDiagnoseAreaVO[] ConstructVOArr(DataTable dtbResult)
        {
            clsMFZDiagnoseAreaVO[] p_objResultArr = new clsMFZDiagnoseAreaVO[dtbResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZDiagnoseAreaVO();
                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
            }
            return p_objResultArr;
        }

        [AutoComplete]
        private IDataParameter[] GetInsertDataParameterArr(clsMFZDiagnoseAreaVO objReader, int diagnoseAreaID)
        {
            IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                            diagnoseAreaID,
                            objReader.m_strDiagnoseAreaName,
                            objReader.m_strSummary
            });
            return objODPArr;
        }

        [AutoComplete]
        private System.Data.IDataParameter[] GetUpdateDataParameterArr(clsMFZDiagnoseAreaVO objReader)
        {
            System.Data.IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                            objReader.m_strDiagnoseAreaName,
                            objReader.m_strSummary,
                            objReader.m_intDiagnoseAreaID
            });
            return objODPArr;
        }
        #endregion

        #region INSERT

        [AutoComplete]
        public long m_lngInsert(clsMFZDiagnoseAreaVO objReader, out int diagnoseAreaID)
        {
            long lngRes = 0;
            diagnoseAreaID = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.m_lngGenerateNewID(m_strTableName, m_strPrimaryKey, out diagnoseAreaID);
                if (lngRes <= 0)
                    return -1;
                lngRes = 0;

                System.Data.IDataParameter[] paramsArr = GetInsertDataParameterArr(objReader, diagnoseAreaID);

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
        public long m_lngUpdate(clsMFZDiagnoseAreaVO objReader)
        {
            long lngRes = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = GetUpdateDataParameterArr(objReader);
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
        public long m_lngDelete(int diagnoseAreaID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = CreateParms.Parms(new object[] { diagnoseAreaID });

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

        [AutoComplete]
        public long m_lngFind(int diagnoseAreaID, out clsMFZDiagnoseAreaVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { diagnoseAreaID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_sqlFindByAreaID, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZDiagnoseAreaVO();
                    ConstructVO(dtbResult.Rows[0], ref objReader);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngFindByRoomID(int roomID, out clsMFZDiagnoseAreaVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { roomID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_sqlFindByRoomID, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZDiagnoseAreaVO();
                    ConstructVO(dtbResult.Rows[0], ref objReader);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngFind(out clsMFZDiagnoseAreaVO[] p_objResultArr)
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

    #region 班次Svc
    /// <summary>
    /// 班次Svc
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTmdSchemeSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL
        private const string m_sqlInsert = @"INSERT INTO T_MFZ_BSE_SCHEME (SCHEME_SEQ_INT,SCHEME_DESC_VCHR, BEGIN_TIME, END_TIME,WEEKDAY_INT)VALUES(?,?,?,?,?)";
        private const string m_sqlUpdate = @"UPDATE T_MFZ_BSE_SCHEME SET  SCHEME_DESC_VCHR=?, BEGIN_TIME=?,END_TIME=?,WEEKDAY_INT=? WHERE SCHEME_SEQ_INT=?";
        private const string m_sqlDelete = @"DELETE T_MFZ_BSE_SCHEME WHERE SCHEME_SEQ_INT=?";
        private const string m_sqlFindAll = @"select SCHEME_SEQ_INT, SCHEME_DESC_VCHR, BEGIN_TIME, END_TIME,WEEKDAY_INT FROM T_MFZ_BSE_SCHEME ORDER BY SCHEME_SEQ_INT ASC";
        private const string m_sqlFindByID = @"select SCHEME_SEQ_INT, SCHEME_DESC_VCHR, BEGIN_TIME, END_TIME,WEEKDAY_INT FROM T_MFZ_BSE_SCHEME where SCHEME_SEQ_INT=?";

        private const string m_strTableName = "T_MFZ_BSE_SCHEME";
        private const string m_strPrimaryKey = "SCHEME_SEQ_INT";
        #endregion

        #region 构造实例和构造参数列表
        [AutoComplete]
        private void ConstructVO(DataRow p_dtrSource, ref clsMFZSchemeVO objReader)
        {
            //SCHEME_DESC_VCHR, BEGIN_TIME, END_TIME
            objReader.m_intSchemeSeq = DBAssist.ToInt32(p_dtrSource["SCHEME_SEQ_INT"]);
            objReader.m_strSchemeDesc = p_dtrSource["SCHEME_DESC_VCHR"].ToString();
            objReader.m_dtBegin = DBAssist.ToDateTime(p_dtrSource["BEGIN_TIME"]);
            objReader.m_dtEnd = DBAssist.ToDateTime(p_dtrSource["END_TIME"]);
            objReader.m_intWeekDay = DBAssist.ToInt32(p_dtrSource["WEEKDAY_INT"]);
        }

        [AutoComplete]
        private clsMFZSchemeVO[] ConstructVOArr(DataTable dtbResult)
        {
            clsMFZSchemeVO[] p_objResultArr = new clsMFZSchemeVO[dtbResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZSchemeVO();
                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
            }
            return p_objResultArr;
        }

        [AutoComplete]
        private IDataParameter[] GetInsertDataParameterArr(clsMFZSchemeVO objReader, int schemeID)
        {
            IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                            schemeID,
                            objReader.m_strSchemeDesc,
                            objReader.m_dtBegin.ToString("HH:mm"),
                            objReader.m_dtEnd.ToString("HH:mm"),
                            objReader.m_intWeekDay.ToString()
            });
            return objODPArr;
        }

        [AutoComplete]
        private System.Data.IDataParameter[] GetUpdateDataParameterArr(clsMFZSchemeVO objReader)
        {
            System.Data.IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                            objReader.m_strSchemeDesc,
                            objReader.m_dtBegin.ToString("HH:mm"),
                            objReader.m_dtEnd.ToString("HH:mm"),
                            objReader.m_intWeekDay.ToString(),
                            objReader.m_intSchemeSeq
            });
            return objODPArr;
        }
        #endregion

        #region INSERT

        [AutoComplete]
        public long m_lngInsert(clsMFZSchemeVO objReader, out int schemeID)
        {
            long lngRes = 0;
            schemeID = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.m_lngGenerateNewID(m_strTableName, m_strPrimaryKey, out schemeID);
                if (lngRes <= 0)
                    return -1;
                lngRes = 0;

                System.Data.IDataParameter[] paramsArr = GetInsertDataParameterArr(objReader, schemeID);

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
        public long m_lngUpdate(clsMFZSchemeVO objReader)
        {
            long lngRes = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = GetUpdateDataParameterArr(objReader);
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
        public long m_lngDelete(int schemeID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = CreateParms.Parms(new object[] { schemeID });

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

        [AutoComplete]
        public long m_lngFind(int schemeID, out clsMFZSchemeVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { schemeID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_sqlFindByID, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZSchemeVO();
                    ConstructVO(dtbResult.Rows[0], ref objReader);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }


        [AutoComplete]
        public long m_lngFind(out clsMFZSchemeVO[] p_objResultArr)
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