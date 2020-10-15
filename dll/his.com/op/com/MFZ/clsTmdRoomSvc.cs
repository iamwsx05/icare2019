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

    /// <summary>
    /// 诊室Svc
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTmdRoomSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL
        private const string m_strInsertSql = @"INSERT INTO T_MFZ_BSE_ROOM(ROOMID_INT, ROOMNAME_VCHR, DEPTID_CHR,SUMMARY_VCHR,DIAGNOSEAREAID_INT)
                                                       VALUES (?, ?, ?,?,?)";
        private const string m_strUpdateSql = @"UPDATE T_MFZ_BSE_ROOM
                                                       SET ROOMNAME_VCHR = ?,
                                                           DEPTID_CHR = ?,
                                                           SUMMARY_VCHR = ?,
                                                           DIAGNOSEAREAID_INT=?
                                                       WHERE ROOMID_INT = ?";
        private const string m_strDeleteSql = @"DELETE  T_MFZ_BSE_ROOM WHERE ROOMID_INT=?";
        private const string m_strFindSql = @"SELECT ROOMID_INT, ROOMNAME_VCHR, DEPTID_CHR, SUMMARY_VCHR,DIAGNOSEAREAID_INT FROM T_MFZ_BSE_ROOM WHERE ROOMID_INT=?";
        private const string m_strFindAllSql = @"SELECT ROOMID_INT,ROOMNAME_VCHR,DEPTID_CHR,SUMMARY_VCHR,DIAGNOSEAREAID_INT FROM T_MFZ_BSE_ROOM Order by diagnoseareaid_int,deptid_chr,roomname_vchr";
        private const string m_strTableName = "T_MFZ_BSE_ROOM";
        private const string m_strPrimaryKey = "ROOMID_INT";
        #endregion

        #region 构造实例和构造参数列表
        /// <summary>
        /// 构造VO
        /// </summary>
        /// <param name="p_dtrSource"></param>
        /// <param name="objReader"></param>
        [AutoComplete]
        private void ConstructVO(DataRow p_dtrSource, ref clsMFZRoomVO objReader)
        {
            //ROOMID_INT,ROOMID_INT, ROOMNAME_VCHR, DEPTID_CHR, SUMMARY_VCHR
            objReader.m_intRoomID = DBAssist.ToInt32(p_dtrSource["ROOMID_INT"]);
            objReader.m_strDeptID = p_dtrSource["DEPTID_CHR"].ToString();
            objReader.m_strRoomName = p_dtrSource["ROOMNAME_VCHR"].ToString();
            objReader.m_strSummary = p_dtrSource["SUMMARY_VCHR"].ToString();
            objReader.m_intAreaId = DBAssist.ToInt32(p_dtrSource["DIAGNOSEAREAID_INT"].ToString());
            if (p_dtrSource.Table.Columns.Contains("DEPTNAME_VCHR"))
            {
                objReader.m_strDeptName = p_dtrSource["DEPTNAME_VCHR"].ToString();
            }
        }

        /// <summary>
        /// 构造VO集合
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        private clsMFZRoomVO[] ConstructVOArr(DataTable dtbResult)
        {
            clsMFZRoomVO[] p_objResultArr = new clsMFZRoomVO[dtbResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZRoomVO();
                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
            }
            return p_objResultArr;
        }

        [AutoComplete]
        private IDataParameter[] GetInsertDataParameterArr(clsMFZRoomVO objReader, int roomID)
        {
            IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                           roomID,
                           objReader.m_strRoomName,
                           objReader.m_strDeptID,
                           objReader.m_strSummary,
                           objReader.m_intAreaId }
                );
            return objODPArr;
        }

        [AutoComplete]
        private System.Data.IDataParameter[] GetUpdateDataParameterArr(clsMFZRoomVO objReader)
        {
            System.Data.IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                           objReader.m_strRoomName,
                           objReader.m_strDeptID,
                           objReader.m_strSummary,
                           objReader.m_intAreaId,
                           objReader.m_intRoomID
            });
            return objODPArr;
        }
        #endregion

        #region INSERT

        [AutoComplete]
        public long m_lngInsert(clsMFZRoomVO objReader, out int roomID)
        {
            long lngRes = 0;
            roomID = -1;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.m_lngGenerateNewID(m_strTableName, m_strPrimaryKey, out roomID);
                if (lngRes <= 0)
                    return -1;
                lngRes = 0;

                System.Data.IDataParameter[] paramsArr = GetInsertDataParameterArr(objReader, roomID);

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
        public long m_lngUpdate(clsMFZRoomVO objReader)
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
        public long m_lngDelete(int roomID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = CreateParms.Parms(new object[] { roomID });

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
        public long m_lngFind(int roomID, out clsMFZRoomVO objReader)
        {
            IDataParameter[] paramsArr = CreateParms.Parms(new object[] { roomID });
            return m_lngFindCommon(m_strFindSql, paramsArr, out objReader);
        }

        [AutoComplete]
        public long m_lngFind(out clsMFZRoomVO[] p_objResultArr)
        {
            return m_lngFindCommon(m_strFindAllSql, out p_objResultArr);
        }

        /// <summary>
        /// 查找诊区下所有诊室
        /// </summary>
        /// <param name="diagnoseAreaID">diagnoseAreaID</param>
        /// <param name="p_objResultArr">诊室集合</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindByAreaID(int diagnoseAreaID, int schemeId, out clsMFZRoomVO[] p_objResultArr)
        {
            string sql = @"
                             SELECT DISTINCT d.roomid_int, d.roomname_vchr, d.summary_vchr,
                                    d.diagnoseareaid_int,
                                    decode(c.deptid_chr,null,d.deptid_chr,c.deptid_chr) deptid_chr
                               FROM (SELECT DISTINCT b.roomid_int, b.deptid_chr
                                                FROM t_mfz_bse_room a, t_mfz_bse_doctor b
                                               WHERE a.diagnoseareaid_int = ?
                                                 AND scheme_seq_int = ?
                                                 AND a.roomid_int = b.roomid_int) c,
                                    t_mfz_bse_room d
                              WHERE c.roomid_int(+) = d.roomid_int AND d.diagnoseareaid_int = ? order by roomname_vchr
                          ";
            IDataParameter[] paramsArr = CreateParms.Parms(new object[] { diagnoseAreaID, schemeId, diagnoseAreaID });
            return m_lngFindCommon(sql, paramsArr, out p_objResultArr);
        }

        [AutoComplete]
        public long m_lngFind(int areaId, out clsMFZRoomVO[] p_objResultArr)
        {
            string sql = @"
                            select * from t_mfz_bse_room where diagnoseAreaId_int=?
                        ";
            IDataParameter[] paramsArr = CreateParms.Parms(new object[] { areaId });
            return m_lngFindCommon(sql, paramsArr, out p_objResultArr);
        }

        #region Find的公共方法
        /// <summary>
        /// 普通方法
        /// </summary>
        /// <param name="paramsArr"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngFindCommon(string sql, IDataParameter[] paramsArr, out clsMFZRoomVO[] resultArr)
        {
            long lngRes = 0;
            resultArr = new clsMFZRoomVO[0];
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
        private long m_lngFindCommon(string sql, out clsMFZRoomVO[] p_objResultArr)
        {
            return m_lngFindCommon(sql, null, out p_objResultArr);
        }

        [AutoComplete]
        private long m_lngFindCommon(string sql, IDataParameter[] paramsArr, out clsMFZRoomVO p_objResult)
        {
            clsMFZRoomVO[] p_objResultArr = null;
            long lngRes = 0;
            lngRes = m_lngFindCommon(sql, paramsArr, out p_objResultArr);
            p_objResult = p_objResultArr[0];
            return lngRes;
        }

        [AutoComplete]
        private long m_lngFindCommon(string sql, out clsMFZRoomVO p_objResult)
        {
            long lngRes = 0;
            clsMFZRoomVO[] p_objResultArr = null;
            lngRes = m_lngFindCommon(sql, null, out p_objResultArr);
            p_objResult = p_objResultArr[0];
            return lngRes;
        }

        #endregion

        #endregion
    }//cs

    #region DB Access Assist
    /// <summary>
    /// 访问数据库辅助类
    /// </summary>
    public class clsSvcAssist : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 普通方法
        /// </summary>
        /// <param name="paramsArr"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngFindCommon(string sql, IDataParameter[] paramsArr, out BaseDataEntity[] resultArr)
        {
            long lngRes = 0;
            resultArr = null;
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
        private long m_lngFindCommon(string sql, out BaseDataEntity[] p_objResultArr)
        {
            return m_lngFindCommon(sql, null, out p_objResultArr);
        }

        [AutoComplete]
        private long m_lngFindCommon(string sql, IDataParameter[] paramsArr, out BaseDataEntity p_objResult)
        {
            BaseDataEntity[] p_objResultArr = null;
            long lngRes = 0;
            lngRes = m_lngFindCommon(sql, paramsArr, out p_objResultArr);
            p_objResult = p_objResultArr[0];
            return lngRes;
        }

        [AutoComplete]
        private long m_lngFindCommon(string sql, out BaseDataEntity p_objResult)
        {
            long lngRes = 0;
            BaseDataEntity[] p_objResultArr = null;
            lngRes = m_lngFindCommon(sql, null, out p_objResultArr);
            p_objResult = p_objResultArr[0];
            return lngRes;
        }

        [AutoComplete]
        private BaseDataEntity[] ConstructVOArr(DataTable dtbResult)
        {
            return null;
        }
    }

    public interface IVOConstruct
    {
        void ConstructVOArr(DataTable dtbResult);
    }
    #endregion
}//ns
