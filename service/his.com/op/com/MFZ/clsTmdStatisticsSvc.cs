using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.iCare.middletier.MFZ
{
    /// <summary>
    /// 叫号统计报表Svc
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsTmdStatisticsSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region SQL
        private const string m_sqlInsert = @"INSERT INTO t_mfz_statistics
                                                    (doctorid_chr, deptid_chr, areaid_int, doctorname_vchr,
                                                     doctorcallednum_int, schemeid_int,startTime,endTime
                                                    )
                                             VALUES (?, ?, ?, ?,
                                                     ?, ?, ?, ?
                                                    )";
        private const string m_sqlUpdate = @"UPDATE t_mfz_statistics
                                               SET doctorid_chr = ?,
                                                   deptid_chr = ?,
                                                   areaid_int = ?,
                                                   doctorname_vchr = ?,
                                                   doctorcallednum_int = ?,
                                                   schemeid_int = ?,
                                                   startTime=?,
                                                   endTime=?";
        private const string m_sqlFindAll = @"
                                                SELECT doctorid_chr, deptid_chr, areaid_int, doctorname_vchr,
                                                       doctorcallednum_int, schemeid_int, NULL schemename, starttime, endtime
                                                  FROM t_mfz_statistics
                                            ";
        

        #endregion x

        #region 构造实例和构造参数列表

        [AutoComplete]
        private void ConstructVO(DataRow p_dtrSource, ref clsMFZStatistics objReader)
        {
            objReader.m_strDoctorId = p_dtrSource["doctorid_chr"].ToString();
            objReader.m_strDeptId = p_dtrSource["deptid_chr"].ToString();
            objReader.m_intAreaId = DBAssist.ToInt32(p_dtrSource["areaid_int"].ToString());
            objReader.m_strDoctorName = p_dtrSource["doctorname_vchr"].ToString();
            objReader.m_intDoctorCalledNum = DBAssist.ToInt32(p_dtrSource["doctorcallednum_int"].ToString());
            objReader.m_intSchemeId =DBAssist.ToInt32(p_dtrSource["schemeid_int"].ToString());
            objReader.m_dtStartTime = DBAssist.ToDateTime(p_dtrSource["startTime"].ToString());
            objReader.m_dtEndTime = DBAssist.ToDateTime(p_dtrSource["endTime"].ToString());
        }

        #endregion

        [AutoComplete]
        private clsMFZStatistics[] ConstructVOArr(DataTable dtbResult)
        {
            clsMFZStatistics[] p_objResultArr = new clsMFZStatistics[dtbResult.Rows.Count];
            for (int i = 0; i < p_objResultArr.Length; i++)
            {
                p_objResultArr[i] = new clsMFZStatistics();
                ConstructVO(dtbResult.Rows[i], ref p_objResultArr[i]);
            }
            return p_objResultArr;
        }

        [AutoComplete]
        private IDataParameter[] GetInsertDataParameterArr(clsMFZStatistics objReader)
        {
            IDataParameter[] objODPArr = CreateParms.Parms(new object[]{
                            objReader.m_strDoctorId,
                            //objReader.m_strDeptId,
                            "0001",
                            objReader.m_intAreaId,
                            //objReader.m_strDoctorName,
                            "空名字",
                            objReader.m_intDoctorCalledNum,
                            objReader.m_intSchemeId,
                            objReader.m_dtStartTime,
                            objReader.m_dtEndTime
            });
            return objODPArr;
        }

        #region INSERT

        [AutoComplete]
        public long m_lngInsert(clsMFZStatistics objReader)
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


        #endregion

        #region DELETE
       

        #endregion

        #region FIND

        [AutoComplete]
        public long m_lngFind(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(m_sqlFindAll, ref dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngSaved(clsMFZStatistics objReader,out int count)
        {
            count = 0;
            long lngRes = 0;
            DataTable dtbResult = null;
            string sql = @"
                            SELECT COUNT (areaid_int) NUM
                              FROM t_mfz_statistics
                             WHERE areaid_int = ? AND schemeid_int = ? AND starttime >= ? AND endtime <= ?
                        ";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                System.Data.IDataParameter[] paramsArr = CreateParms.Parms(new object[]{
                                                                                            objReader.m_intAreaId,
                                                                                            objReader.m_intSchemeId,
                                                                                            objReader.m_dtStartTime,
                                                                                            objReader.m_dtEndTime
                });
                
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtbResult, paramsArr);
                objHRPSvc.Dispose();
            }
            catch
            {
                //new clsLogText().LogError("");
            }

            if (dtbResult != null && dtbResult.Rows.Count > 0)
            {

                count = DBAssist.ToInt32(dtbResult.Rows[0]["NUM"].ToString());
                if (count == DBAssist.NullInt)
                {
                    count = 0;
                }
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngFind(int areaId,int schemeid,string doctId,DateTime dt,out DataTable dtResult) 
        {
            dtResult = null;
            return 1;
        
        }

        #region 注释掉

        //[AutoComplete]
        //public long m_lngFindByRoomID(int roomID, out clsMFZStatistics objReader)
        //{
        //    long lngRes = 0;
        //    objReader = null;

        //    clsHRPTableService objHRPSvc = new clsHRPTableService();
        //    try
        //    {
        //        IDataParameter[] paramsArr = this.m_objConstructIDataParameterArr(roomID);
        //        DataTable dtbResult = null;

        //        lngRes = objHRPSvc.lngGetDataTableWithParameters(m_sqlFindByRoomID, ref dtbResult, paramsArr);
        //        objHRPSvc.Dispose();
        //        if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
        //        {
        //            objReader = new clsMFZStatistics();
        //            ConstructVO(dtbResult.Rows[0], ref objReader);
        //        }
        //    }
        //    catch (Exception objEx)
        //    {
        //        new clsLogText().LogError(objEx);
        //    }
        //    return lngRes;
        //}

        //[AutoComplete]
        //public long m_lngFind(out clsMFZStatistics[] p_objResultArr)
        //{
        //    long lngRes = 0;
        //    p_objResultArr = null;
        //    clsHRPTableService objHRPSvc = new clsHRPTableService();
        //    try
        //    {
        //        DataTable dtbResult = null;
        //        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(m_sqlFindAll, ref dtbResult);
        //        objHRPSvc.Dispose();
        //        if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
        //        {
        //            p_objResultArr = ConstructVOArr(dtbResult);
        //        }
        //    }
        //    catch (Exception objEx)
        //    {
        //        new clsLogText().LogError(objEx);
        //    }
        //    return lngRes;
        //} 
        #endregion

        #endregion
    }
}
