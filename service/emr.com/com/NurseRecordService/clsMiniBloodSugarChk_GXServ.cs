using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;
namespace com.digitalwave.clsMiniBloodSugarChk_GXServ
{
	/// <summary>
	/// clsMiniBloodSugarChkServ 的摘要说明。
    /// 微量血糖检测结果记录中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsMiniBloodSugarChk_GXServ : clsDiseaseTrackService
	{
        #region 即将弃用
        /// <summary>
        ///  添加记录
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewRecoed(clsMiniBloodSugarChkValue_GX p_objValue)
        {
            if (p_objValue == null)
                return -1;
            if (p_objValue.m_strInPatientID == string.Empty || p_objValue.m_dtmInPatientDate == DateTime.MinValue)
                return -1;
            string strSql = @"insert into t_emr_minibldsugarchk_gx (inpatientid,inpatientdate,opendate,content_limosis,
			content_breakfast2h,content_beforelunch11am,content_afterlunch2h,content_beforesupper5pm,content_weehours0am,
			content_weehours3am,content_random,createduserid,status,createdate,custom1content,custom2content,custom1name,custom2name) 
			values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(18, out objDPArr);

                objDPArr[0].Value = p_objValue.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objValue.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objValue.m_dtmOpenDate;
                objDPArr[3].Value = p_objValue.m_strCONTENT_LIMOSIS;
                objDPArr[4].Value = p_objValue.m_strCONTENT_BREAKFAST2H;
                objDPArr[5].Value = p_objValue.m_strCONTENT_BEFORELUNCH11AM;
                objDPArr[6].Value = p_objValue.m_strCONTENT_AFTERLUNCH2H;
                objDPArr[7].Value = p_objValue.m_strCONTENT_BEFORESUPPER5PM;
                objDPArr[8].Value = p_objValue.m_strCONTENT_WEEHOURS0AM;
                objDPArr[9].Value = p_objValue.m_strCONTENT_WEEHOURS3AM;
                objDPArr[10].Value = p_objValue.m_strCONTENT_RANDOM;
                objDPArr[11].Value = p_objValue.m_strCreateUserID;
                objDPArr[12].Value = 0;
                objDPArr[13].DbType = DbType.DateTime;
                objDPArr[13].Value = p_objValue.m_dtmCreateDate;
                objDPArr[14].Value = p_objValue.m_strCustom1Content;
                objDPArr[15].Value = p_objValue.m_strCustom2Content;
                objDPArr[16].Value = p_objValue.m_strCustom1Name;
                objDPArr[17].Value = p_objValue.m_strCustom2Name;

                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        #region 保存数据
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngAddNewRecoed( clsMiniBloodSugarChkValue_GX p_objValue)
        //{
        //    //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCheckRoomService", "m_lngGetRecordTimeList");
        //    //if (lngCheckRes <= 0)
        //    //return lngCheckRes;

        //    //检查参数
        //    if (p_objValue == null)
        //        return -1;
        //    if (p_objValue.m_strInPatientID == string.Empty)
        //        return -1;

        //    #region SQL
        //    string strSql = @"insert into t_emr_minbloodsugar
        //                      (inpatientid,
        //                       inpatientdate,
        //                       opendate,
        //                       createdate,
        //                       createuserid,
        //                       sequence_int,
        //                       bloodsugar,
        //                       bloodsugar_xml,
        //                       status,
        //                       markstatus)
        //                    values
        //                      (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

        //    string strSqlDetail = @"insert into t_emr_minbloodsugarcon
        //                              (inpatientid,
        //                               inpatientdate,
        //                               opendate,
        //                               modifydate,
        //                               modifyuserid,
        //                               meattype,
        //                               description,
        //                               bloodsugar_right,
        //                               status)
        //                            values
        //                              (?, ?, ?, ?, ?, ?, ?, ?, ?)";
        //    #endregion

        //    long lngRes = 0;
        //    clsHRPTableService objHRPServ = new clsHRPTableService();

        //    //获取签名流水号
        //    long lngSequence = 0;
        //    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
        //    lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

        //    try
        //    {
        //        #region 主表
        //        IDataParameter[] objDPArr = null;
        //        objHRPServ.CreateDatabaseParameter(10, out objDPArr);

        //        objDPArr[0].Value = p_objValue.m_strInPatientID;
        //        objDPArr[1].DbType = DbType.DateTime;
        //        objDPArr[1].Value = p_objValue.m_dtmInPatientDate;
        //        objDPArr[2].DbType = DbType.DateTime;
        //        objDPArr[2].Value = p_objValue.m_dtmOpenDate;
        //        objDPArr[3].DbType = DbType.DateTime;
        //        objDPArr[3].Value = p_objValue.m_dtmCreateDate;
        //        objDPArr[4].Value = p_objValue.m_strCreateUserID;
        //        objDPArr[5].Value = lngSequence;
        //        objDPArr[6].Value = p_objValue.m_strBloodSugar;
        //        objDPArr[7].Value = p_objValue.m_strBloodSugarXML;
        //        objDPArr[8].Value = 0;
        //        objDPArr[9].Value = p_objValue.m_intMarkStatus;

        //        long lngAff = 0;
        //        //保存主表记录
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
        //        #endregion

        //        if (lngRes <= 0) return lngRes;
        //        //保存签名集合
        //        lngRes = objSign.m_lngAddSign(p_objValue.objSignerArr, lngSequence);

        //        #region 子表
        //        //保存子表数据
        //        System.Data.IDataParameter[] objDPArr1 = null;
        //        objHRPServ.CreateDatabaseParameter(9, out objDPArr1);
        //        objDPArr1[0].Value = p_objValue.m_strInPatientID;
        //        objDPArr1[1].DbType = DbType.DateTime;
        //        objDPArr1[1].Value = p_objValue.m_dtmInPatientDate;
        //        objDPArr1[2].DbType = DbType.DateTime;
        //        objDPArr1[2].Value = p_objValue.m_dtmOpenDate;
        //        objDPArr1[3].DbType = DbType.DateTime;
        //        objDPArr1[3].Value = p_objValue.m_dtmModifyDate;
        //        objDPArr1[4].Value = p_objValue.m_strModifyUserID;
        //        objDPArr1[5].Value = p_objValue.m_strMeatType;
        //        objDPArr1[6].Value = p_objValue.m_strDescription;
        //        objDPArr1[7].Value = p_objValue.m_strBloodSugar_Right;
        //        objDPArr1[8].Value = 0;
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSqlDetail, ref lngAff, objDPArr1);
        //        #endregion

        //    }
        //    catch (Exception objEx)
        //    {
        //        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
        //        bool blnRes = objLogger.LogError(objEx);
        //    }
        //    finally
        //    {
        //        //objHRPServ.Dispose();
        //        objHRPServ = null;
        //    }
        //    return lngRes;
        //}

        #endregion		/// <summary>

        ///  修改记录
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyRecoed(clsMiniBloodSugarChkValue_GX p_objValue)
        {
            if (p_objValue == null)
                return -1;
            if (p_objValue.m_strInPatientID == string.Empty || p_objValue.m_dtmInPatientDate == DateTime.MinValue)
                return -1;
            string strSql = @"update t_emr_minibldsugarchk_gx
			set content_limosis = ?,content_breakfast2h = ?,content_beforelunch11am = ?,content_afterlunch2h = ?,
			content_beforesupper5pm = ?,content_weehours0am = ?,content_weehours3am = ?,content_random = ?,opendate=?,
			custom1content = ?,custom2content = ? where inpatientid = ? and inpatientdate = ? and createdate = ?";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(14, out objDPArr);
                objDPArr[0].Value = p_objValue.m_strCONTENT_LIMOSIS;
                objDPArr[1].Value = p_objValue.m_strCONTENT_BREAKFAST2H;
                objDPArr[2].Value = p_objValue.m_strCONTENT_BEFORELUNCH11AM;
                objDPArr[3].Value = p_objValue.m_strCONTENT_AFTERLUNCH2H;
                objDPArr[4].Value = p_objValue.m_strCONTENT_BEFORESUPPER5PM;
                objDPArr[5].Value = p_objValue.m_strCONTENT_WEEHOURS0AM;
                objDPArr[6].Value = p_objValue.m_strCONTENT_WEEHOURS3AM;
                objDPArr[7].Value = p_objValue.m_strCONTENT_RANDOM;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objValue.m_dtmOpenDate;
                objDPArr[9].Value = p_objValue.m_strCustom1Content;
                objDPArr[10].Value = p_objValue.m_strCustom2Content;
                objDPArr[11].Value = p_objValue.m_strInPatientID.Trim();
                objDPArr[12].DbType = DbType.DateTime;
                objDPArr[12].Value = p_objValue.m_dtmInPatientDate;
                objDPArr[13].DbType = DbType.DateTime;
                objDPArr[13].Value = p_objValue.m_dtmCreateDate;

                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        #region 修改记录
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngModifyRecoed( clsMiniBloodSugarChkValue_GX p_objValue)
        //{
        //    //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCheckRoomService", "m_lngGetRecordTimeList");
        //    //if (lngCheckRes <= 0)
        //    //return lngCheckRes;

        //    //检查参数
        //    if (p_objValue == null)
        //        return -1;
        //    if (p_objValue.m_strInPatientID == string.Empty)
        //        return -1;
        //    string strSQL1 = @"update t_emr_minbloodsugar
        //                       set sequence_int = ?, bloodsugar = ?, bloodsugar_xml = ?, markstatus = ?
        //                     where inpatientid = ?
        //                       and inpatientdate = ?
        //                       and opendate = ?";
        //    string strSQL2 = @"update t_emr_minbloodsugarcon
        //                       set status = ?
        //                     where inpatientid = ?
        //                       and inpatientdate = ?
        //                       and opendate = ?";
        //    string strSqlDetail = @"insert into t_emr_minbloodsugarcon
        //                              (inpatientid,
        //                               inpatientdate,
        //                               opendate,
        //                               modifydate,
        //                               modifyuserid,
        //                               meattype,
        //                               description,
        //                               bloodsugar_right,
        //                               status)
        //                            values
        //                              (?, ?, ?, ?, ?, ?, ?, ?, ?)";

        //    long lngRes = 0;
        //    clsHRPTableService objHRPServ = new clsHRPTableService();
        //    //获取签名流水号
        //    long lngSequence = 0;
        //    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
        //    lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

        //    try
        //    {

        //        #region update 主表
        //        IDataParameter[] objDPArr1 = null;
        //        objHRPServ.CreateDatabaseParameter(7, out objDPArr1);

        //        objDPArr1[0].Value = lngSequence;
        //        objDPArr1[1].Value = p_objValue.m_strBloodSugar;
        //        objDPArr1[2].Value = p_objValue.m_strBloodSugarXML;
        //        objDPArr1[3].Value = p_objValue.m_intMarkStatus;
        //        objDPArr1[4].Value = p_objValue.m_strInPatientID;
        //        objDPArr1[5].DbType = DbType.DateTime;
        //        objDPArr1[5].Value = p_objValue.m_dtmInPatientDate;
        //        objDPArr1[6].DbType = DbType.DateTime;
        //        objDPArr1[6].Value = p_objValue.m_dtmOpenDate;

        //        long lngAff = 0;
        //        //更新主表记录
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL1, ref lngAff, objDPArr1);
        //        #endregion

        //        if (lngRes <= 0) return lngRes;
        //        //保存签名集合
        //        lngRes = objSign.m_lngAddSign(p_objValue.objSignerArr, lngSequence);

        //        #region update 子表状态
        //        IDataParameter[] objDPArr2 = null;

        //        objHRPServ.CreateDatabaseParameter(4, out objDPArr2);

        //        objDPArr2[0].Value = 1;
        //        objDPArr2[1].Value = p_objValue.m_strInPatientID;
        //        objDPArr2[2].DbType = DbType.DateTime;
        //        objDPArr2[2].Value = p_objValue.m_dtmInPatientDate;
        //        objDPArr2[3].DbType = DbType.DateTime;
        //        objDPArr2[3].Value = p_objValue.m_dtmOpenDate;

        //        lngAff = 0;
        //        //更新主表记录
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL2, ref lngAff, objDPArr2);
        //        #endregion

        //        #region 保存子表数据
        //        System.Data.IDataParameter[] objDPArr3 = null;
        //        objHRPServ.CreateDatabaseParameter(9, out objDPArr3);
        //        objDPArr3[0].Value = p_objValue.m_strInPatientID;
        //        objDPArr3[1].DbType = DbType.DateTime;
        //        objDPArr3[1].Value = p_objValue.m_dtmInPatientDate;
        //        objDPArr3[2].DbType = DbType.DateTime;
        //        objDPArr3[2].Value = p_objValue.m_dtmOpenDate;
        //        objDPArr3[3].DbType = DbType.DateTime;
        //        objDPArr3[3].Value = p_objValue.m_dtmModifyDate;
        //        objDPArr3[4].Value = p_objValue.m_strModifyUserID;
        //        objDPArr3[5].Value = p_objValue.m_strMeatType;
        //        objDPArr3[6].Value = p_objValue.m_strDescription;
        //        objDPArr3[7].Value = p_objValue.m_strBloodSugar_Right;
        //        objDPArr3[8].Value = 0;
        //        lngAff = 0;
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSqlDetail, ref lngAff, objDPArr3);
        //        #endregion

        //    }
        //    catch (Exception objEx)
        //    {
        //        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
        //        bool blnRes = objLogger.LogError(objEx);
        //    }
        //    finally
        //    {
        //        //objHRPServ.Dispose();
        //        objHRPServ = null;

        //    }
        //    return lngRes;
        //}

        #endregion


        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecoed(clsMiniBloodSugarChkValue_GX p_objValue)
        {
            if (p_objValue == null)
                return -1;
            if (p_objValue.m_strInPatientID == string.Empty || p_objValue.m_dtmInPatientDate == DateTime.MinValue)
                return -1;
            string strSql = @"update t_emr_minibldsugarchk_gx set status = ?,deactiveddate = ?,deactivedoperatorid = ?
 where inpatientid = ? and inpatientdate = ? and createdate = ?";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;

                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = 1;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objValue.m_dtmDeActivedDate;
                objDPArr[2].Value = p_objValue.m_strDeActivedOperatorID;
                objDPArr[3].Value = p_objValue.m_strInPatientID.Trim();
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objValue.m_dtmInPatientDate;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objValue.m_dtmCreateDate;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objValue"></param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngDeleteRecoed( clsMiniBloodSugarChkValue_GX p_objValue)
        //{
        //    //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCheckRoomService", "m_lngGetRecordTimeList");
        //    //if (lngCheckRes <= 0)
        //    //return lngCheckRes;

        //    //检查参数
        //    if (p_objValue == null)
        //        return -1;
        //    if (p_objValue.m_strInPatientID == string.Empty || p_objValue.m_dtmInPatientDate == DateTime.MinValue)
        //        return -1;
        //    string strSql = @"update t_emr_minbloodsugar
        //                       set status = ?, deactiveddate = ?, deactivedoperatorid = ?
        //                     where inpatientid = ?
        //                       and inpatientdate = ?
        //                       and createdate = ?";

        //    long lngRes = 0;
        //    clsHRPTableService objHRPServ = new clsHRPTableService();
        //    try
        //    {

        //        IDataParameter[] objDPArr = null;

        //        objHRPServ.CreateDatabaseParameter(6, out objDPArr);
        //        objDPArr[0].Value = 1;
        //        objDPArr[1].DbType = DbType.DateTime;
        //        objDPArr[1].Value = p_objValue.m_dtmDeActivedDate;
        //        objDPArr[2].Value = p_objValue.m_strDeActivedOperatorID;
        //        objDPArr[3].Value = p_objValue.m_strInPatientID.Trim();
        //        objDPArr[4].DbType = DbType.DateTime;
        //        objDPArr[4].Value = p_objValue.m_dtmInPatientDate;
        //        objDPArr[5].DbType = DbType.DateTime;
        //        objDPArr[5].Value = p_objValue.m_dtmCreateDate;
        //        long lngAff = 0;
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
        //    }
        //    catch (Exception objEx)
        //    {

        //        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
        //        bool blnRes = objLogger.LogError(objEx);
        //    }
        //    finally
        //    {
        //        //objHRPServ.Dispose();
        //        objHRPServ = null;

        //    }
        //    return lngRes;
        //}

        #endregion		/// <summary>

        #region 根据病人查找记录 即将弃用
        /// 根据病人查找记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_objValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecoedByInPatient(string p_strInPatientID, DateTime p_dtmInPatientDate, out clsMiniBloodSugarChkValue_GX[] p_objValues)
        {
            p_objValues = null;
            if (string.IsNullOrEmpty(p_strInPatientID) || p_dtmInPatientDate == DateTime.MinValue)
                return -1;
            string strSql = @"select inpatientid,
       inpatientdate,
       opendate,
       content_limosis,
       content_breakfast2h,
       content_beforelunch11am,
       content_afterlunch2h,
       content_beforesupper5pm,
       content_weehours0am,
       content_weehours3am,
       content_random,
       createduserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       createdate,
       custom1content,
       custom2content,
       custom1name,
       custom2name
  from t_emr_minibldsugarchk_gx
 where inpatientid = ?
   and inpatientdate = ?
   and status = 0
 order by createdate";

            DataTable dtbValues = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInPatientDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValues, objDPArr);
                if (lngRes <= 0 || dtbValues.Rows.Count <= 0)
                    return 0;
                p_objValues = new clsMiniBloodSugarChkValue_GX[dtbValues.Rows.Count];
                for (int i = 0; i < dtbValues.Rows.Count; i++)
                {
                    p_objValues[i] = new clsMiniBloodSugarChkValue_GX();
                    p_objValues[i].m_strInPatientID = p_strInPatientID;
                    p_objValues[i].m_dtmInPatientDate = p_dtmInPatientDate;
                    p_objValues[i].m_dtmCreateDate = DateTime.Parse(dtbValues.Rows[i]["CREATEDATE"].ToString());
                    p_objValues[i].m_dtmOpenDate = DateTime.Parse(dtbValues.Rows[i]["OPENDATE"].ToString());
                    p_objValues[i].m_strCreateUserID = dtbValues.Rows[i]["CREATEDUSERID"].ToString();
                    p_objValues[i].m_strCONTENT_LIMOSIS = dtbValues.Rows[i]["CONTENT_LIMOSIS"].ToString();
                    p_objValues[i].m_strCONTENT_BREAKFAST2H = dtbValues.Rows[i]["CONTENT_BREAKFAST2H"].ToString();
                    p_objValues[i].m_strCONTENT_BEFORELUNCH11AM = dtbValues.Rows[i]["CONTENT_BEFORELUNCH11AM"].ToString();
                    p_objValues[i].m_strCONTENT_AFTERLUNCH2H = dtbValues.Rows[i]["CONTENT_AFTERLUNCH2H"].ToString();
                    p_objValues[i].m_strCONTENT_BEFORESUPPER5PM = dtbValues.Rows[i]["CONTENT_BEFORESUPPER5PM"].ToString();
                    p_objValues[i].m_strCONTENT_WEEHOURS0AM = dtbValues.Rows[i]["CONTENT_WEEHOURS0AM"].ToString();
                    p_objValues[i].m_strCONTENT_WEEHOURS3AM = dtbValues.Rows[i]["CONTENT_WEEHOURS3AM"].ToString();
                    p_objValues[i].m_strCONTENT_RANDOM = dtbValues.Rows[i]["CONTENT_RANDOM"].ToString();
                    p_objValues[i].m_strCustom1Content = dtbValues.Rows[i]["CUSTOM1CONTENT"].ToString();
                    p_objValues[i].m_strCustom2Content = dtbValues.Rows[i]["CUSTOM2CONTENT"].ToString();
                    p_objValues[i].m_strCustom1Name = dtbValues.Rows[i]["CUSTOM1NAME"].ToString();
                    p_objValues[i].m_strCustom2Name = dtbValues.Rows[i]["CUSTOM2NAME"].ToString();
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        #endregion

        #region 获取指定记录信息
        /// <summary>
        /// 获取指定记录信息
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_objValues"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecoedByInPatient( string p_strInPatientID, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate, out clsMiniBloodSugarChkValue_GX p_objValues)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCheckRoomService", "m_lngGetRecordTimeList");
            //if (lngCheckRes <= 0)
            //return lngCheckRes;

            //检查参数
            p_objValues = null;
            if (string.IsNullOrEmpty(p_strInPatientID) || p_dtmInPatientDate == DateTime.MinValue)
                return -1;
            string strSql = @"select t.inpatientdate,
                               t.inpatientid,
                               t.opendate,
                               t.createdate,
                               t.createuserid,
                               t.sequence_int,
                               t.bloodsugar,
                               t.bloodsugar_xml,
                               t.status,
                               t.markstatus,
                               d.modifydate,
                               d.modifyuserid,
                               d.meattype,
                               d.description,
                               d.bloodsugar_right,
                               d.status
                          from t_emr_minbloodsugar t, t_emr_minbloodsugarcon d
                         where t.inpatientid = d.inpatientid
                           and t.inpatientdate = d.inpatientdate
                           and t.opendate = d.opendate
                           and t.inpatientid =?
                           and t.inpatientdate = ?
                           and t.opendate =?
                           and d.status = ?";

            DataTable dtbValues = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr2 = null;

                objHRPServ.CreateDatabaseParameter(4, out objDPArr2);


                objDPArr2[0].Value = p_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = p_dtmOpenDate;
                objDPArr2[3].Value = 0;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValues, objDPArr2);
                if (lngRes <= 0 || dtbValues.Rows.Count <= 0)
                    return 0;
                p_objValues = new clsMiniBloodSugarChkValue_GX();
                p_objValues.m_strInPatientID = p_strInPatientID.Trim();
                p_objValues.m_dtmInPatientDate = p_dtmInPatientDate;
                p_objValues.m_dtmCreateDate = DateTime.Parse(dtbValues.Rows[0]["CREATEDATE"].ToString());
                p_objValues.m_dtmOpenDate = DateTime.Parse(dtbValues.Rows[0]["OPENDATE"].ToString());
                p_objValues.m_strCreateUserID = dtbValues.Rows[0]["CREATEDUSERID"].ToString();
                p_objValues.m_dtmModifyDate = DateTime.Parse(dtbValues.Rows[0]["MODIFYDATE"].ToString());
                p_objValues.m_strModifyUserID = dtbValues.Rows[0]["MODIFYUSERID"].ToString();
                p_objValues.m_strMeatType = dtbValues.Rows[0]["MEATTYPE"].ToString();
                p_objValues.m_strDescription = dtbValues.Rows[0]["DESCRIPTION"].ToString();
                p_objValues.m_strBloodSugar = dtbValues.Rows[0]["BLOODSUGAR"].ToString();
                p_objValues.m_strBloodSugar_Right = dtbValues.Rows[0]["BLOODSUGAR_RIGHT"].ToString();
                p_objValues.m_strBloodSugarXML = dtbValues.Rows[0]["BLOODSUGAR_XML"].ToString();
                //获取签名集合
                if (dtbValues.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                {
                    long lngS = long.Parse(dtbValues.Rows[0]["SEQUENCE_INT"].ToString());
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    long lngTemp = objSign.m_lngGetSign(lngS, out p_objValues.objSignerArr);
                    //释放
                    objSign = null;
                }
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        #endregion

        #region 设置自定义标头名
        /// <summary>
        /// 设置自定义标头名
        /// </summary>
        /// <param name="p_strCustomName">自定义标头名</param>
        /// <param name="p_strColumnName">对应的字段名</param>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_dtmInPatientDate">入院日期</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetCustomName(string p_strCustomName,
            string p_strColumnName,
            string p_strInPatientID,
            DateTime p_dtmInPatientDate)
        {
            if (p_strCustomName == null || p_strColumnName == null || p_strColumnName.Trim() == string.Empty
                || p_strInPatientID == null || p_strInPatientID.Trim() == string.Empty)
                return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"update t_emr_minibldsugarchk_gx set " + p_strColumnName + @" = ? where 
                            inpatientid = ? and inpatientdate = ?";

                IDataParameter[] objDPArr = null;

                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strCustomName.Trim();
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = Convert.ToDateTime(p_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));

                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        
        #endregion

        #region 获取字典列表
        /// <summary>
        /// 获取字典列表
        /// 用于列头显示使用
        /// </summary>
        /// <param name="strMealTypeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMealType(out string[] strMealTypeArr)
        {
            long lngRes = 0;
            strMealTypeArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //此处字典表 编号为65 硬代码
                string strSQL = @"select jxcode_chr,
       wbcode_chr,
       pycode_chr,
       dictname_vchr,
       dictid_chr,
       dictkind_chr,
       dictseqid_chr,
       dictdefinecode_vchr
  from t_aid_dict t
 where dictkind_chr = '65'
   and dictid_chr > 0
 order by t.dictdefinecode_vchr";
                DataTable dtbValues = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbValues);
                if (lngRes <= 0 || dtbValues.Rows.Count <= 0)
                    return 0;
                strMealTypeArr = new string[dtbValues.Rows.Count];
                for (int i = 0; i < dtbValues.Rows.Count; i++)
                {
                    strMealTypeArr[i] = dtbValues.Rows[i]["dictname_vchr"].ToString();
                }

            }
            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 根据病人获取列表
        /// <summary>
        /// 根据病人获取列表
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_objValues"></param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngGetRecoedByInPatient( string p_strInPatientID, DateTime p_dtmInPatientDate, out clsMiniBloodSugarChkValue_GX[] p_objValues)
        //{
        //    //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCheckRoomService", "m_lngGetRecordTimeList");
        //    //if (lngCheckRes <= 0)
        //    //return lngCheckRes;

        //    //检查参数
        //    p_objValues = null;
        //    if (string.IsNullOrEmpty(p_strInPatientID) || p_dtmInPatientDate == DateTime.MinValue)
        //        return -1;
        //    string strSql = @"select t.inpatientdate,
        //                       t.inpatientid,
        //                       t.opendate,
        //                       t.createdate,
        //                       t.createuserid,
        //                       t.sequence_int,
        //                       t.bloodsugar,
        //                       t.bloodsugar_xml,
        //                       t.status,
        //                       t.markstatus,
        //                       d.modifydate,
        //                       d.modifyuserid,
        //                       d.meattype,
        //                       d.description,
        //                       d.bloodsugar_right,
        //                       d.status
        //                  from t_emr_minbloodsugar t, t_emr_minbloodsugarcon d
        //                 where t.inpatientid = d.inpatientid
        //                   and t.inpatientdate = d.inpatientdate
        //                   and t.opendate = d.opendate
        //                   and t.inpatientid =?
        //                   and t.inpatientdate = ?
        //                   and d.status = ?";

        //    DataTable dtbValues = new DataTable();

        //    long lngRes = 0;
        //    clsHRPTableService objHRPServ = new clsHRPTableService();
        //    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();

        //    try
        //    {
        //        IDataParameter[] objDPArr2 = null;

        //        objHRPServ.CreateDatabaseParameter(3, out objDPArr2);


        //        objDPArr2[0].Value = p_strInPatientID.Trim();
        //        objDPArr2[1].DbType = DbType.DateTime;
        //        objDPArr2[1].Value = p_dtmInPatientDate;
        //        objDPArr2[2].Value = 0;

        //        lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValues, objDPArr2);
        //        if (lngRes <= 0 || dtbValues.Rows.Count <= 0)
        //            return 0;
        //        p_objValues = new clsMiniBloodSugarChkValue_GX[dtbValues.Rows.Count];
        //        for (int i = 0; i < dtbValues.Rows.Count; i++)
        //        {
        //            p_objValues[i] = new clsMiniBloodSugarChkValue_GX();
        //            p_objValues[i].m_strInPatientID = p_strInPatientID.Trim();
        //            p_objValues[i].m_dtmInPatientDate = p_dtmInPatientDate;
        //            p_objValues[i].m_dtmCreateDate = DateTime.Parse(dtbValues.Rows[i]["CREATEDATE"].ToString());
        //            p_objValues[i].m_dtmOpenDate = DateTime.Parse(dtbValues.Rows[i]["OPENDATE"].ToString());
        //            p_objValues[i].m_strCreateUserID = dtbValues.Rows[i]["CREATEDUSERID"].ToString();
        //            p_objValues[i].m_dtmModifyDate = DateTime.Parse(dtbValues.Rows[i]["MODIFYDATE"].ToString());
        //            p_objValues[i].m_strModifyUserID = dtbValues.Rows[i]["MODIFYUSERID"].ToString();
        //            p_objValues[i].m_strMeatType = dtbValues.Rows[i]["MEATTYPE"].ToString();
        //            p_objValues[i].m_strDescription = dtbValues.Rows[i]["DESCRIPTION"].ToString();
        //            p_objValues[i].m_strBloodSugar = dtbValues.Rows[i]["BLOODSUGAR"].ToString();
        //            p_objValues[i].m_strBloodSugar_Right = dtbValues.Rows[i]["BLOODSUGAR_RIGHT"].ToString();
        //            p_objValues[i].m_strBloodSugarXML = dtbValues.Rows[i]["BLOODSUGAR_XML"].ToString();
        //            //获取签名集合
        //            if (dtbValues.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
        //            {
        //                long lngS = long.Parse(dtbValues.Rows[i]["SEQUENCE_INT"].ToString());
        //                long lngTemp = objSign.m_lngGetSign(lngS, out p_objValues[i].objSignerArr);

        //            }
        //        }
        //    }
        //    finally
        //    {
        //        //objHRPServ.Dispose();

        //    }

        //    return lngRes;
        //}
        [AutoComplete]
        public long m_lngGetRecoedByInPatient( string p_strInPatientID, DateTime p_dtmInPatientDate, out DataTable p_objValues)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCheckRoomService", "m_lngGetRecordTimeList");
            //if (lngCheckRes <= 0)
            //return lngCheckRes;

            //检查参数
            p_objValues = null;
            if (string.IsNullOrEmpty(p_strInPatientID) || p_dtmInPatientDate == DateTime.MinValue)
                return -1;
            string strSql = @"select t.inpatientdate,
                               t.inpatientid,
                               t.opendate,
                               t.createdate,
                               t.createuserid,
                               t.sequence_int,
                               t.bloodsugar,
                               t.bloodsugar_xml,
                               t.status,
                               t.markstatus,
                               d.modifydate,
                               d.modifyuserid,
                               d.meattype,
                               d.description,
                               d.bloodsugar_right,
                               d.status
                          from t_emr_minbloodsugar t, t_emr_minbloodsugarcon d
                         where t.inpatientid = d.inpatientid
                           and t.inpatientdate = d.inpatientdate
                           and t.opendate = d.opendate
                           and t.inpatientid =?
                           and t.inpatientdate = ?
                           and d.status = ?";

            DataTable dtbValues = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();

            try
            {
                IDataParameter[] objDPArr2 = null;

                objHRPServ.CreateDatabaseParameter(3, out objDPArr2);


                objDPArr2[0].Value = p_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_dtmInPatientDate;
                objDPArr2[2].Value = 0;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValues, objDPArr2);
                if (lngRes <= 0 || dtbValues.Rows.Count <= 0)
                    return 0;
                p_objValues = dtbValues;
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        }

        #endregion

        #region 获取病人的该记录时间列表
        /// <summary>
        /// 获取病人的该记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetRecordTimeList");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string c_strGetTimeListSQL = @"select createdate, opendate
														from t_emr_minbloodsugar
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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

                //objHRPServ.Dispose();
            }
            return lngRes;
        } 
        #endregion

        #region 更新数据库中的首次打印时间
        /// <summary>
        /// 更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string c_strUpdateFirstPrintDateSQL = @"update t_emr_minbloodsugar
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";
            try
            {
                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        } 
        #endregion

        #region 获取病人的已经被删除记录时间列表 指定用户
        /// <summary>
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strDeleteUserID">删除者ID</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeList");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            string c_strGetDeleteRecordTimeListSQL = @"select createdate, opendate
																	from t_emr_minbloodsugar
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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

                //objHRPServ.Dispose();
            }
            return lngRes;
        } 
        #endregion

        #region 获取病人的已经被删除记录时间列表
        /// <summary>
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeListAll");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            string c_strGetDeleteRecordTimeListAllSQL = @"select createdate, opendate
																		from t_emr_minbloodsugar
																	where inpatientid = ?
																		and inpatientdate = ?
																		and status = 1";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //objHRPServ.Dispose();
            }
            return lngRes;
        } 
        #endregion

        #region 获取指定记录的内容
        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ">弃用，传入null</param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return (long)enmOperationResult.Parameter_Error;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCheckRoomService", "m_lngGetRecordTimeList");
            //if (lngCheckRes <= 0)
            //return lngCheckRes;

            string strSql = @"select t.inpatientdate,
                               t.inpatientid,
                               t.opendate,
                               t.createdate,
                               t.createuserid,
                               t.sequence_int,
                               t.bloodsugar,
                               t.bloodsugar_xml,
                               t.status,
                               t.markstatus,
                               d.modifydate,
                               d.modifyuserid,
                               d.meattype,
                               d.description,
                               d.bloodsugar_right,
                               d.status
                          from t_emr_minbloodsugar t, t_emr_minbloodsugarcon d
                         where t.inpatientid = d.inpatientid
                           and t.inpatientdate = d.inpatientdate
                           and t.opendate = d.opendate
                           and t.inpatientid =?
                           and t.inpatientdate = ?
                           and t.opendate =?
                           and d.status = ?";

            DataTable dtbValues = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr2 = null;

                objHRPServ.CreateDatabaseParameter(4, out objDPArr2);


                objDPArr2[0].Value = p_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr2[3].Value = 0;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValues, objDPArr2);
                if (lngRes <= 0 || dtbValues.Rows.Count <= 0)
                    return 0;
                clsMiniBloodSugarChkValue_GX p_objValues = new clsMiniBloodSugarChkValue_GX();
                p_objValues.m_strInPatientID = p_strInPatientID.Trim();
                p_objValues.m_dtmInPatientDate =DateTime.Parse( p_strInPatientDate);
                p_objValues.m_dtmCreateDate = DateTime.Parse(dtbValues.Rows[0]["CREATEDATE"].ToString());
                p_objValues.m_dtmOpenDate = DateTime.Parse(dtbValues.Rows[0]["OPENDATE"].ToString());
                p_objValues.m_strCreateUserID = dtbValues.Rows[0]["CREATEDUSERID"].ToString();
                p_objValues.m_dtmModifyDate = DateTime.Parse(dtbValues.Rows[0]["MODIFYDATE"].ToString());
                p_objValues.m_strModifyUserID = dtbValues.Rows[0]["MODIFYUSERID"].ToString();
                p_objValues.m_strMeatType = dtbValues.Rows[0]["MEATTYPE"].ToString();
                p_objValues.m_strDescription = dtbValues.Rows[0]["DESCRIPTION"].ToString();
                p_objValues.m_strBloodSugar = dtbValues.Rows[0]["BLOODSUGAR"].ToString();
                p_objValues.m_strBloodSugar_Right = dtbValues.Rows[0]["BLOODSUGAR_RIGHT"].ToString();
                p_objValues.m_strBloodSugarXML = dtbValues.Rows[0]["BLOODSUGAR_XML"].ToString();
                //获取签名集合
                if (dtbValues.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                {
                    long lngS = long.Parse(dtbValues.Rows[0]["SEQUENCE_INT"].ToString());
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    long lngTemp = objSign.m_lngGetSign(lngS, out p_objValues.objSignerArr);
                    //释放
                    objSign = null;
                }
                p_objRecordContent = p_objValues;
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        } 
        #endregion

        #region 保存记录到数据库。添加主表,添加子表.
        /// <summary>
        /// 保存记录到数据库。添加主表,添加子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ">弃用 传入null</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;

            clsMiniBloodSugarChkValue_GX p_objValue = (clsMiniBloodSugarChkValue_GX)p_objRecordContent;

            #region SQL
            string strSql = @"insert into t_emr_minbloodsugar
                              (inpatientid,
                               inpatientdate,
                               opendate,
                               createdate,
                               createuserid,
                               sequence_int,
                               bloodsugar,
                               bloodsugar_xml,
                               status,
                               markstatus)
                            values
                              (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            string strSqlDetail = @"insert into t_emr_minbloodsugarcon
                                      (inpatientid,
                                       inpatientdate,
                                       opendate,
                                       modifydate,
                                       modifyuserid,
                                       meattype,
                                       description,
                                       bloodsugar_right,
                                       status)
                                    values
                                      (?, ?, ?, ?, ?, ?, ?, ?, ?)";
            #endregion

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            //获取签名流水号
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

            try
            {
                #region 主表
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(10, out objDPArr);

                objDPArr[0].Value = p_objValue.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objValue.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objValue.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objValue.m_dtmCreateDate;
                objDPArr[4].Value = p_objValue.m_strCreateUserID;
                objDPArr[5].Value = lngSequence;
                objDPArr[6].Value = p_objValue.m_strBloodSugar;
                objDPArr[7].Value = p_objValue.m_strBloodSugarXML;
                objDPArr[8].Value = 0;
                objDPArr[9].Value = p_objValue.m_intMarkStatus;

                long lngAff = 0;
                //保存主表记录
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                #endregion

                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objValue.objSignerArr, lngSequence);

                #region 子表
                //保存子表数据
                System.Data.IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(9, out objDPArr1);
                objDPArr1[0].Value = p_objValue.m_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = p_objValue.m_dtmInPatientDate;
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = p_objValue.m_dtmOpenDate;
                objDPArr1[3].DbType = DbType.DateTime;
                objDPArr1[3].Value = p_objValue.m_dtmModifyDate;
                objDPArr1[4].Value = p_objValue.m_strModifyUserID;
                objDPArr1[5].Value = p_objValue.m_strMeatType;
                objDPArr1[6].Value = p_objValue.m_strDescription;
                objDPArr1[7].Value = p_objValue.m_strBloodSugar_Right;
                objDPArr1[8].Value = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSqlDetail, ref lngAff, objDPArr1);
                #endregion

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        } 
        #endregion

        #region 把新修改的内容保存到数据库。更新主表,添加子表
        /// <summary>
        /// 把新修改的内容保存到数据库。更新主表,添加子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ">弃用 传入null</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;

            clsMiniBloodSugarChkValue_GX p_objValue = (clsMiniBloodSugarChkValue_GX)p_objRecordContent;
             string strSQL1 = @"update t_emr_minbloodsugar
                               set sequence_int = ?, bloodsugar = ?, bloodsugar_xml = ?, markstatus = ?
                             where inpatientid = ?
                               and inpatientdate = ?
                               and opendate = ?";
            string strSQL2 = @"update t_emr_minbloodsugarcon
                               set status = ?
                             where inpatientid = ?
                               and inpatientdate = ?
                               and opendate = ?";
            string strSqlDetail = @"insert into t_emr_minbloodsugarcon
                                      (inpatientid,
                                       inpatientdate,
                                       opendate,
                                       modifydate,
                                       modifyuserid,
                                       meattype,
                                       description,
                                       bloodsugar_right,
                                       status)
                                    values
                                      (?, ?, ?, ?, ?, ?, ?, ?, ?)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            //获取签名流水号
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

            try
            {

                #region update 主表
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(7, out objDPArr1);

                objDPArr1[0].Value = lngSequence;
                objDPArr1[1].Value = p_objValue.m_strBloodSugar;
                objDPArr1[2].Value = p_objValue.m_strBloodSugarXML;
                objDPArr1[3].Value = p_objValue.m_intMarkStatus;
                objDPArr1[4].Value = p_objValue.m_strInPatientID;
                objDPArr1[5].DbType = DbType.DateTime;
                objDPArr1[5].Value = p_objValue.m_dtmInPatientDate;
                objDPArr1[6].DbType = DbType.DateTime;
                objDPArr1[6].Value = p_objValue.m_dtmOpenDate;

                long lngAff = 0;
                //更新主表记录
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL1, ref lngAff, objDPArr1);
                #endregion

                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objValue.objSignerArr, lngSequence);

                #region update 子表状态
                IDataParameter[] objDPArr2 = null;

                objHRPServ.CreateDatabaseParameter(4, out objDPArr2);

                objDPArr2[0].Value = 1;
                objDPArr2[1].Value = p_objValue.m_strInPatientID;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = p_objValue.m_dtmInPatientDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = p_objValue.m_dtmOpenDate;

                lngAff = 0;
                //更新子表记录，使任何时候都只有一条有效
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL2, ref lngAff, objDPArr2);
                #endregion

                #region 保存子表数据
                System.Data.IDataParameter[] objDPArr3 = null;
                objHRPServ.CreateDatabaseParameter(9, out objDPArr3);
                objDPArr3[0].Value = p_objValue.m_strInPatientID;
                objDPArr3[1].DbType = DbType.DateTime;
                objDPArr3[1].Value = p_objValue.m_dtmInPatientDate;
                objDPArr3[2].DbType = DbType.DateTime;
                objDPArr3[2].Value = p_objValue.m_dtmOpenDate;
                objDPArr3[3].DbType = DbType.DateTime;
                objDPArr3[3].Value = p_objValue.m_dtmModifyDate;
                objDPArr3[4].Value = p_objValue.m_strModifyUserID;
                objDPArr3[5].Value = p_objValue.m_strMeatType;
                objDPArr3[6].Value = p_objValue.m_strDescription;
                objDPArr3[7].Value = p_objValue.m_strBloodSugar_Right;
                objDPArr3[8].Value = 0;
                lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSqlDetail, ref lngAff, objDPArr3);
                #endregion

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;

            }
            return lngRes;
        } 
        #endregion

        #region 把记录从数据中“删除”
        /// <summary>
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;

            clsMiniBloodSugarChkValue_GX p_objValue = (clsMiniBloodSugarChkValue_GX)p_objRecordContent;
            string strSql = @"update t_emr_minbloodsugar
                               set status = ?, deactiveddate = ?, deactivedoperatorid = ?
                             where inpatientid = ?
                               and inpatientdate = ?
                               and createdate = ?";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;

                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = 1;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objValue.m_dtmDeActivedDate;
                objDPArr[2].Value = p_objValue.m_strDeActivedOperatorID;
                objDPArr[3].Value = p_objValue.m_strInPatientID.Trim();
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objValue.m_dtmInPatientDate;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objValue.m_dtmCreateDate;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;

            }
            return lngRes;
        } 
        #endregion

        #region 获取指定已经被删除记录的内容
        /// <summary>
        /// 获取指定已经被删除记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ">弃用 传入null</param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return (long)enmOperationResult.Parameter_Error;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCheckRoomService", "m_lngGetRecordTimeList");
            //if (lngCheckRes <= 0)
            //return lngCheckRes;

            string strSql = @"select t.inpatientdate,
                               t.inpatientid,
                               t.opendate,
                               t.createdate,
                               t.createuserid,
                               t.sequence_int,
                               t.bloodsugar,
                               t.bloodsugar_xml,
                               t.status,
                               t.markstatus,
                               d.modifydate,
                               d.modifyuserid,
                               d.meattype,
                               d.description,
                               d.bloodsugar_right,
                               d.status
                          from t_emr_minbloodsugar t, t_emr_minbloodsugarcon d
                         where t.inpatientid = d.inpatientid
                           and t.inpatientdate = d.inpatientdate
                           and t.opendate = d.opendate
                           and t.inpatientid =?
                           and t.inpatientdate = ?
                           and t.opendate =?
                           and d.status = ?";

            DataTable dtbValues = new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr2 = null;

                objHRPServ.CreateDatabaseParameter(4, out objDPArr2);


                objDPArr2[0].Value = p_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr2[3].Value = 1;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValues, objDPArr2);
                if (lngRes <= 0 || dtbValues.Rows.Count <= 0)
                    return 0;
                clsMiniBloodSugarChkValue_GX p_objValues = new clsMiniBloodSugarChkValue_GX();
                p_objValues.m_strInPatientID = p_strInPatientID.Trim();
                p_objValues.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                p_objValues.m_dtmCreateDate = DateTime.Parse(dtbValues.Rows[0]["CREATEDATE"].ToString());
                p_objValues.m_dtmOpenDate = DateTime.Parse(dtbValues.Rows[0]["OPENDATE"].ToString());
                p_objValues.m_strCreateUserID = dtbValues.Rows[0]["CREATEDUSERID"].ToString();
                p_objValues.m_dtmModifyDate = DateTime.Parse(dtbValues.Rows[0]["MODIFYDATE"].ToString());
                p_objValues.m_strModifyUserID = dtbValues.Rows[0]["MODIFYUSERID"].ToString();
                p_objValues.m_strMeatType = dtbValues.Rows[0]["MEATTYPE"].ToString();
                p_objValues.m_strDescription = dtbValues.Rows[0]["DESCRIPTION"].ToString();
                p_objValues.m_strBloodSugar = dtbValues.Rows[0]["BLOODSUGAR"].ToString();
                p_objValues.m_strBloodSugar_Right = dtbValues.Rows[0]["BLOODSUGAR_RIGHT"].ToString();
                p_objValues.m_strBloodSugarXML = dtbValues.Rows[0]["BLOODSUGAR_XML"].ToString();
                //获取签名集合
                if (dtbValues.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                {
                    long lngS = long.Parse(dtbValues.Rows[0]["SEQUENCE_INT"].ToString());
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    long lngTemp = objSign.m_lngGetSign(lngS, out p_objValues.objSignerArr);
                    //释放
                    objSign = null;
                }
                p_objRecordContent = p_objValues;
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
        } 
        #endregion

        #region 查看是否有相同的记录时间 未实现
        /// <summary>
        /// 查看是否有相同的记录时间
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            try
            {
                //未实现
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 查看当前记录是否最新的记录 未实现
        /// <summary>
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>		
        /// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            try
            {
                //未实现
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region 获取数据库中最新的修改时间和首次打印时间 未实现
        /// <summary>
        /// 获取数据库中最新的修改时间和首次打印时间
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_dtmModifyDate">修改时间</param>
        /// <param name="p_strFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.Now;
            p_strFirstPrintDate = null;
            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            try
            {
                //未实现 
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion
	}
}
