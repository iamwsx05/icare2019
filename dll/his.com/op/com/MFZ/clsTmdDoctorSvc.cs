using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;
using Oracle.DataAccess.Client;

namespace com.digitalwave.iCare.middletier.MFZ
{

    #region 医生Svc

    /// <summary>
    /// 医生排班Svc
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTmdDoctorSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region SQL
        private const string m_strInsertSql = @"INSERT INTO T_MFZ_BSE_DOCTOR(DOCTORID_CHR,DEPTID_CHR,SCHEME_SEQ_INT, ROOMID_INT, WORKSTATIONID_INT,DOCTORTYPE_INT,SUMMARY_VCHR)
                                                           VALUES (?,?,?,?,?,?,?)";
        private const string m_strUpdateSql = @"UPDATE T_MFZ_BSE_DOCTOR
                                                       SET ROOMID_INT = ?,
                                                           WORKSTATIONID_INT = ?,
                                                           DOCTORTYPE_INT=?,
                                                           SUMMARY_VCHR=?
                                                       WHERE DOCTORID_CHR = ? and DEPTID_CHR = ? and SCHEME_SEQ_INT=?";
        private const string m_strDeleteSql = @"DELETE  T_MFZ_BSE_DOCTOR WHERE DOCTORID_CHR=? and  DEPTID_CHR = ? and SCHEME_SEQ_INT=?";
        ////查找医生根据 根据安排Id
        private const string m_strFindDoctorsByShemeId = @"SELECT * FROM t_mfz_bse_doctor  WHERE scheme_seq_int=?";
        //查找医生根据 工作站Id
        private const string m_strFindDoctorsByStationID = "select *  from T_MFZ_BSE_DOCTOR  where WorkstationID_int=?";
        //查找医生根据 医生Id,部门Id,安排Id
        private const string m_strFindSql = @"SELECT empid_chr, lastname_vchr, b.*
                                                     FROM t_bse_employee a, t_mfz_bse_doctor b
                                                     WHERE a.empid_chr = b.doctorid_chr And DiagnoseAreaId=? AND doctorid_chr = ? AND deptid_chr = ? AND scheme_seq_int=?";
        //查找医生列表,根据排班Id
        private const string m_strFindAllSql = @"SELECT EMPID_CHR, LASTNAME_VCHR, B.*
                                                        FROM T_BSE_EMPLOYEE A, T_MFZ_BSE_DOCTOR B
                                                        WHERE A.EMPID_CHR = B.DOCTORID_CHR AND B.scheme_seq_int=?";

        private const string m_strFindDoctorsByDeptID = @" SELECT e.empid_chr AS doctorid_chr, e.lastname_vchr,
                                                                   e.isexpert_chr AS doctortype_int, m.deptid_chr, d.roomid_int,
                                                                   d.workstationid_int,d.SCHEME_SEQ_INT, d.summary_vchr
                                                           FROM t_bse_deptemp m,
                                                                   t_bse_employee e,
                                                                   (SELECT *
                                                                      FROM t_mfz_bse_doctor
                                                                     WHERE scheme_seq_int = ?) d
                                                          WHERE m.empid_chr = e.empid_chr
                                                               AND m.empid_chr = d.doctorid_chr(+)
                                                               AND m.deptid_chr = d.deptid_chr(+)
                                                               AND roomid_int IS NULL
                                                               AND workstationid_int IS NULL
                                                               AND e.status_int = 1
                                                               AND e.hasprescriptionright_chr = 1
                                                               AND m.deptid_chr = ?";
        //查找诊区的所有医生
        private const string m_strFindDoctorsByAreaID = @"SELECT doctor.*, employee.lastname_vchr
                                                              FROM t_mfz_bse_doctor doctor, t_bse_employee employee, t_mfz_bse_room room
                                                             WHERE doctor.doctorid_chr = employee.empid_chr
                                                               AND doctor.roomid_int = room.roomid_int
                                                               AND room.diagnoseareaid_int =?
                                                               AND doctor.scheme_seq_int = ?";


        #endregion

        #region 构造实体和构建参数列表
        [AutoComplete]
        private void ConstructVO(DataRow p_dtrSource, ref clsMFZDoctorVO objReader)
        {
            //DOCTORID_CHR,DEPTID_CHR, ROOMID_INT, WORKSTATIONID_INT,DOCTORTYPE_INT,SUMMARY_VCHR
            objReader.m_strDoctorID = p_dtrSource["DOCTORID_CHR"].ToString();
            if (p_dtrSource.Table.Columns.Contains("DEPTNAME_VCHR"))
            {
                objReader.m_strDeptName = p_dtrSource["DEPTNAME_VCHR"].ToString();
            }
            objReader.m_strDeptID = p_dtrSource["DEPTID_CHR"].ToString();
            objReader.m_intSchemeSeq = DBAssist.ToInt32(p_dtrSource["SCHEME_SEQ_INT"]);
            objReader.m_intRoomID = DBAssist.ToInt32(p_dtrSource["ROOMID_INT"]);
            objReader.m_intWorkStationID = DBAssist.ToInt32(p_dtrSource["WORKSTATIONID_INT"]);
            objReader.m_enmDoctorType = DBAssist.ToInt32(p_dtrSource["DOCTORTYPE_INT"]) == 1 ? enmMFZDoctorType.Expert : enmMFZDoctorType.Common;
            objReader.m_strSummary = p_dtrSource["SUMMARY_VCHR"].ToString();
            if (p_dtrSource.Table.Columns.Contains("LASTNAME_VCHR"))
            {
                objReader.m_strDoctorName = p_dtrSource["LASTNAME_VCHR"].ToString().Trim();
            }
        }


        [AutoComplete]
        private clsMFZDoctorVO[] ConstructVOArr(DataTable dtbResult)
        {
            clsMFZDoctorVO[] p_objResultArr = new clsMFZDoctorVO[dtbResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZDoctorVO();
                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
            }

            if (p_objResultArr == null)
            {
                p_objResultArr = new clsMFZDoctorVO[0];
            }
            return p_objResultArr;
        }

        [AutoComplete]
        private IDataParameter[] GetInsertDataParameterArr(clsMFZDoctorVO objReader)
        {
            IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                           objReader.m_strDoctorID,
                           objReader.m_strDeptID,
                           objReader.m_intSchemeSeq,
                           DBAssist.ToObject(objReader.m_intRoomID),
                            DBAssist.ToObject(objReader.m_intWorkStationID),
                           objReader.m_enmDoctorType == enmMFZDoctorType.Common ? 0 : 1,
                           objReader.m_strSummary
            });
            return objODPArr;
        }

        [AutoComplete]
        private System.Data.IDataParameter[] GetUpdateDataParameterArr(clsMFZDoctorVO objReader)
        {
            System.Data.IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                           DBAssist.ToObject(objReader.m_intRoomID),
                           DBAssist.ToObject(objReader.m_intWorkStationID),
                           objReader.m_enmDoctorType == enmMFZDoctorType.Common ? 0 : 1,
                           objReader.m_strSummary,
                           objReader.m_strDoctorID,
                           objReader.m_strDeptID,
                           objReader.m_intSchemeSeq
            });
            return objODPArr;
        }

        #endregion

        #region INSERT

        [AutoComplete]
        public long m_lngInsert(clsMFZDoctorVO objReader)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = GetInsertDataParameterArr(objReader);

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
        public long m_lngUpdate(clsMFZDoctorVO objReader)
        {
            long lngRes = 0;
            long lngRecEff = -1;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = GetUpdateDataParameterArr(objReader);
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
        public long m_lngDelete(string doctorID, string deptID, int schemeID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = CreateParms.Parms(new object[] { doctorID, deptID, schemeID });

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

        /// <summary>
        /// 删除诊区下的某个安排
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelete(int areaId, int schemeId)
        {
            string sql = @"
                            Delete  t_mfz_bse_doctor
                             WHERE scheme_seq_int = ? and roomid_int IN (SELECT roomid_int
                                                FROM t_mfz_bse_room
                                               WHERE diagnoseareaid_int = ?)
                            ";
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = CreateParms.Parms(new object[] { schemeId, areaId });

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(sql, ref lngRecEff, paramsArr);
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
        /// 查找医生根据 医生Id,部门Id,安排Id
        /// </summary>
        /// <param name="doctorID">医生Id</param>
        /// <param name="deptID">部门Id</param>
        /// <param name="schemeID">安排Id</param>
        /// <param name="objReader">医生VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFind(int areaId, string doctorID, string deptID, int schemeID, out clsMFZDoctorVO objReader)
        {
            long lngRes = 0;
            objReader = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { areaId, doctorID, deptID, schemeID });
                DataTable dtbResult = null;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindSql, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
                if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    objReader = new clsMFZDoctorVO();
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
        /// 查找医生列表,根据安排Id
        /// </summary>
        /// <param name="p_objResultArr">医生列表</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngFind(int schemeID, out clsMFZDoctorVO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMFZDoctorVO[0];
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] paramsArr = CreateParms.Parms(new object[] { schemeID });
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(m_strFindAllSql, ref dtbResult, paramsArr);
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
        /// 查找诊区计划下科室的医生（不包含已经分配诊室的医生）
        /// </summary>
        /// <param name="deptID">areaID</param>
        /// <param name="p_objResultArr">schemeID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindDoctorsByDeptID(int areaID, int schemeID, string deptID, out clsMFZDoctorVO[] p_objResultArr)
        {
            string sql = @"
                            SELECT b.doctorid_chr doctid, a.*
                              FROM (SELECT a.deptid_chr, b.lastname_vchr, b.empid_chr doctorid_chr,
                                           c.deptname_vchr, b.isexpert_chr doctortype_int, '' roomid_int,
                                           '' workstationid_int, '' scheme_seq_int, '' summary_vchr
                                      FROM t_bse_deptemp a, t_bse_employee b, t_bse_deptdesc c
                                     WHERE a.empid_chr = b.empid_chr
                                       AND c.deptid_chr = a.deptid_chr
                                       AND (c.inpatientoroutpatient_int = 0 or c.inpatientoroutpatient_int = 2)
                                       AND b.status_int = 1
                                       AND b.hasprescriptionright_chr = 1
                                       AND a.deptid_chr = ?) a,
                                   (SELECT a.doctorid_chr
                                      FROM t_mfz_bse_doctor a, t_mfz_bse_room b
                                     WHERE a.roomid_int = b.roomid_int
                                       AND a.scheme_seq_int = ?
                                       AND a.deptid_chr = ?
                                       AND b.diagnoseareaid_int = ? ) b
                             WHERE a.doctorid_chr = b.doctorid_chr(+) AND b.doctorid_chr IS NULL
                             Order by a.doctorid_chr
                          ";
            IDataParameter[] arrParams = CreateParms.Parms(new object[] { deptID, schemeID, deptID, areaID });
            //m_strFindDoctorsByDeptID
            return m_lngFindCommon(sql, arrParams, out p_objResultArr);
        }

        [AutoComplete]
        public long m_lngFindDoctorsByDeptID(string deptID, out clsMFZDoctorVO[] p_objResultArr)
        {
            string sql = @"
                              SELECT b.empid_chr as doctid,a.deptid_chr, b.lastname_vchr, b.empid_chr doctorid_chr,
                                   c.deptname_vchr, b.isexpert_chr doctortype_int, '' roomid_int,
                                   '' workstationid_int, '' scheme_seq_int, '' summary_vchr
                              FROM t_bse_deptemp a, t_bse_employee b, t_bse_deptdesc c
                             WHERE a.empid_chr = b.empid_chr
                               AND c.deptid_chr = a.deptid_chr
                               AND b.status_int = 1
                               AND b.hasprescriptionright_chr = 1
                               AND a.deptid_chr =?
                               Order by b.empid_chr
                          ";
            IDataParameter[] arrParams = CreateParms.Parms(new object[] { deptID });
            return m_lngFindCommon(sql, arrParams, out p_objResultArr);
        }

        /// <summary>
        /// 查找诊区的医生(不包含已经分配诊室的医生)
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindNoWorkStationDoctorsByAreaID(int areaID, int schemeID, out clsMFZDoctorVO[] p_objResultArr)
        {
            string sql = @"
                              SELECT   b.doctorid_chr doctid, a.*
                                FROM (SELECT a.deptid_chr, b.lastname_vchr, b.empid_chr doctorid_chr,
                                             c.deptname_vchr, b.isexpert_chr doctortype_int,
                                             '' roomid_int, '' workstationid_int, '' scheme_seq_int,
                                             '' summary_vchr
                                        FROM t_bse_deptemp a, t_bse_employee b, t_bse_deptdesc c
                                       WHERE a.empid_chr = b.empid_chr
                                         AND (c.inpatientoroutpatient_int = 0 or c.inpatientoroutpatient_int = 2)
                                         AND c.deptid_chr = a.deptid_chr
                                         AND b.status_int = 1
                                         AND b.hasprescriptionright_chr = 1) a,
                                     (SELECT a.doctorid_chr
                                        FROM t_mfz_bse_doctor a, t_mfz_bse_room b
                                       WHERE a.roomid_int = b.roomid_int
                                         AND a.scheme_seq_int = ?
                                         AND b.diagnoseareaid_int = ?) b
                                    WHERE a.doctorid_chr = b.doctorid_chr(+) AND b.doctorid_chr IS NULL
                                ORDER BY a.deptid_chr
                          ";
            IDataParameter[] arrParams = CreateParms.Parms(new object[] { schemeID, areaID });
            return m_lngFindCommon(sql, arrParams, out p_objResultArr);
        }

        /// <summary>
        /// 查找医生列表
        /// </summary>
        /// <param name="areaID">诊区Id</param>
        /// <param name="schemeID">计划,安排Id</param>
        /// <param name="p_objResultArr">结果列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindDoctorsByAreaID(int areaID, int schemeID, out clsMFZDoctorVO[] p_objResultArr)
        {
            IDataParameter[] arrParams = CreateParms.Parms(new object[] { areaID, schemeID });
            return m_lngFindCommon(m_strFindDoctorsByAreaID, arrParams, out p_objResultArr);
        }

        /// <summary>
        /// 根据工作站ID查找医生
        /// </summary>
        /// <param name="workStationId">工作站ID</param>
        /// <param name="p_objResultArr">结果数组</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindDoctorsByWorkStationID(int workStationId, out clsMFZDoctorVO[] p_objResultArr)
        {
            IDataParameter[] arrParams = CreateParms.Parms(new object[] { workStationId });
            return m_lngFindCommon(m_strFindDoctorsByStationID, arrParams, out p_objResultArr);
        }

        /// <summary>
        /// 根据班次Id查找医生列表
        /// </summary>
        /// <param name="schemeId"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindDoctorsBySchemeID(int schemeId, out clsMFZDoctorVO[] p_objResultArr)
        {
            IDataParameter[] arrParams = CreateParms.Parms(new object[] { schemeId });
            return m_lngFindCommon(m_strFindDoctorsByShemeId, arrParams, out p_objResultArr);
        }


        #region Find的公共方法
        /// <summary>
        /// 普通方法
        /// </summary>
        /// <param name="paramsArr"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngFindCommon(string sql, IDataParameter[] paramsArr, out clsMFZDoctorVO[] resultArr)
        {
            long lngRes = 0;
            resultArr = new clsMFZDoctorVO[0];
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            DataTable dtbResult = null;
            try
            {
                if (paramsArr == null)
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(sql, ref dtbResult);
                }
                else
                {
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtbResult, paramsArr);
                }

                objHRPSvc.Dispose();
                bool isValid = lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0;
                if (isValid)
                {
                    resultArr = ConstructVOArr(dtbResult);
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        private long m_lngFindCommon(string sql, out clsMFZDoctorVO[] p_objResultArr)
        {
            return m_lngFindCommon(sql, null, out p_objResultArr);
        }

        [AutoComplete]
        private long m_lngFindCommon(string sql, IDataParameter[] paramsArr, out clsMFZDoctorVO p_objResult)
        {
            clsMFZDoctorVO[] p_objResultArr = new clsMFZDoctorVO[0];
            long lngRes = 0;
            lngRes = m_lngFindCommon(sql, paramsArr, out p_objResultArr);
            p_objResult = p_objResultArr[0];
            return lngRes;
        }

        [AutoComplete]
        private long m_lngFindCommon(string sql, out clsMFZDoctorVO p_objResult)
        {
            long lngRes = 0;
            clsMFZDoctorVO[] p_objResultArr = new clsMFZDoctorVO[0];
            lngRes = m_lngFindCommon(sql, null, out p_objResultArr);
            p_objResult = p_objResultArr[0];
            return lngRes;
        }

        #endregion


        #endregion

    }
    #endregion

    #region CreateParms
    /// <summary>
    /// CreateParms
    /// </summary>
    public class CreateParms
    {
        public static System.Data.IDataParameter[] Parms(object[] objs)
        {
            System.Data.IDataParameter[] parms = new OracleParameter[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                parms[i] = new OracleParameter();
                parms[i].Value = objs[i];
            }
            return parms;
        }         
    }
    #endregion
}
