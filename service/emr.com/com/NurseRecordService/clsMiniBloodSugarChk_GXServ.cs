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
	/// clsMiniBloodSugarChkServ ��ժҪ˵����
    /// ΢��Ѫ�Ǽ������¼�м��
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsMiniBloodSugarChk_GXServ : clsDiseaseTrackService
	{
        #region ��������
        /// <summary>
        ///  ��Ӽ�¼
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

        #region ��������
        /// <summary>
        /// ��Ӽ�¼
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

        //    //������
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

        //    //��ȡǩ����ˮ��
        //    long lngSequence = 0;
        //    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
        //    lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

        //    try
        //    {
        //        #region ����
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
        //        //���������¼
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
        //        #endregion

        //        if (lngRes <= 0) return lngRes;
        //        //����ǩ������
        //        lngRes = objSign.m_lngAddSign(p_objValue.objSignerArr, lngSequence);

        //        #region �ӱ�
        //        //�����ӱ�����
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

        ///  �޸ļ�¼
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

        #region �޸ļ�¼
        /// <summary>
        /// �޸ļ�¼
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

        //    //������
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
        //    //��ȡǩ����ˮ��
        //    long lngSequence = 0;
        //    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
        //    lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

        //    try
        //    {

        //        #region update ����
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
        //        //���������¼
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL1, ref lngAff, objDPArr1);
        //        #endregion

        //        if (lngRes <= 0) return lngRes;
        //        //����ǩ������
        //        lngRes = objSign.m_lngAddSign(p_objValue.objSignerArr, lngSequence);

        //        #region update �ӱ�״̬
        //        IDataParameter[] objDPArr2 = null;

        //        objHRPServ.CreateDatabaseParameter(4, out objDPArr2);

        //        objDPArr2[0].Value = 1;
        //        objDPArr2[1].Value = p_objValue.m_strInPatientID;
        //        objDPArr2[2].DbType = DbType.DateTime;
        //        objDPArr2[2].Value = p_objValue.m_dtmInPatientDate;
        //        objDPArr2[3].DbType = DbType.DateTime;
        //        objDPArr2[3].Value = p_objValue.m_dtmOpenDate;

        //        lngAff = 0;
        //        //���������¼
        //        lngRes = objHRPServ.lngExecuteParameterSQL(strSQL2, ref lngAff, objDPArr2);
        //        #endregion

        //        #region �����ӱ�����
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
        /// ɾ����¼
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

        #region ɾ����¼
        /// <summary>
        /// ɾ����¼
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

        //    //������
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

        #region ���ݲ��˲��Ҽ�¼ ��������
        /// ���ݲ��˲��Ҽ�¼
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

        #region ��ȡָ����¼��Ϣ
        /// <summary>
        /// ��ȡָ����¼��Ϣ
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

            //������
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
                //��ȡǩ������
                if (dtbValues.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                {
                    long lngS = long.Parse(dtbValues.Rows[0]["SEQUENCE_INT"].ToString());
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    long lngTemp = objSign.m_lngGetSign(lngS, out p_objValues.objSignerArr);
                    //�ͷ�
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

        #region �����Զ����ͷ��
        /// <summary>
        /// �����Զ����ͷ��
        /// </summary>
        /// <param name="p_strCustomName">�Զ����ͷ��</param>
        /// <param name="p_strColumnName">��Ӧ���ֶ���</param>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
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

        #region ��ȡ�ֵ��б�
        /// <summary>
        /// ��ȡ�ֵ��б�
        /// ������ͷ��ʾʹ��
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
                //�˴��ֵ�� ���Ϊ65 Ӳ����
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

        #region ���ݲ��˻�ȡ�б�
        /// <summary>
        /// ���ݲ��˻�ȡ�б�
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

        //    //������
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
        //            //��ȡǩ������
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

            //������
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

        #region ��ȡ���˵ĸü�¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵ĸü�¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
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

            //������
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
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
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

        #region �������ݿ��е��״δ�ӡʱ��
        /// <summary>
        /// �������ݿ��е��״δ�ӡʱ�䡣
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
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
                //������                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //ִ��SQL
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

        #region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б� ָ���û�
        /// <summary>
        /// ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strDeleteUserID">ɾ����ID</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
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

            //������
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
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
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

        #region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
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

            //������
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
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
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

        #region ��ȡָ����¼������
        /// <summary>
        /// ��ȡָ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objHRPServ">���ã�����null</param>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            //������
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
                //��ȡǩ������
                if (dtbValues.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                {
                    long lngS = long.Parse(dtbValues.Rows[0]["SEQUENCE_INT"].ToString());
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    long lngTemp = objSign.m_lngGetSign(lngS, out p_objValues.objSignerArr);
                    //�ͷ�
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

        #region �����¼�����ݿ⡣�������,����ӱ�.
        /// <summary>
        /// �����¼�����ݿ⡣�������,����ӱ�.
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ">���� ����null</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //������                              
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

            //��ȡǩ����ˮ��
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

            try
            {
                #region ����
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
                //���������¼
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                #endregion

                if (lngRes <= 0) return lngRes;
                //����ǩ������
                lngRes = objSign.m_lngAddSign(p_objValue.objSignerArr, lngSequence);

                #region �ӱ�
                //�����ӱ�����
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

        #region �����޸ĵ����ݱ��浽���ݿ⡣��������,����ӱ�
        /// <summary>
        /// �����޸ĵ����ݱ��浽���ݿ⡣��������,����ӱ�.
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ">���� ����null</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //������
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
            //��ȡǩ����ˮ��
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

            try
            {

                #region update ����
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
                //���������¼
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL1, ref lngAff, objDPArr1);
                #endregion

                if (lngRes <= 0) return lngRes;
                //����ǩ������
                lngRes = objSign.m_lngAddSign(p_objValue.objSignerArr, lngSequence);

                #region update �ӱ�״̬
                IDataParameter[] objDPArr2 = null;

                objHRPServ.CreateDatabaseParameter(4, out objDPArr2);

                objDPArr2[0].Value = 1;
                objDPArr2[1].Value = p_objValue.m_strInPatientID;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = p_objValue.m_dtmInPatientDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = p_objValue.m_dtmOpenDate;

                lngAff = 0;
                //�����ӱ��¼��ʹ�κ�ʱ��ֻ��һ����Ч
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL2, ref lngAff, objDPArr2);
                #endregion

                #region �����ӱ�����
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

        #region �Ѽ�¼�������С�ɾ����
        /// <summary>
        /// �Ѽ�¼�������С�ɾ������
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //������
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

        #region ��ȡָ���Ѿ���ɾ����¼������
        /// <summary>
        /// ��ȡָ���Ѿ���ɾ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objHRPServ">���� ����null</param>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            //������
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
                //��ȡǩ������
                if (dtbValues.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                {
                    long lngS = long.Parse(dtbValues.Rows[0]["SEQUENCE_INT"].ToString());
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    long lngTemp = objSign.m_lngGetSign(lngS, out p_objValues.objSignerArr);
                    //�ͷ�
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

        #region �鿴�Ƿ�����ͬ�ļ�¼ʱ�� δʵ��
        /// <summary>
        /// �鿴�Ƿ�����ͬ�ļ�¼ʱ��
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo">������ͬ��¼,���ظ���ͬ��¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            //������
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            try
            {
                //δʵ��
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region �鿴��ǰ��¼�Ƿ����µļ�¼ δʵ��
        /// <summary>
        /// �鿴��ǰ��¼�Ƿ����µļ�¼��
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>		
        /// <param name="p_objModifyInfo">����ǰ��¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            //������
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            try
            {
                //δʵ��
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ�� δʵ��
        /// <summary>
        /// ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_dtmModifyDate">�޸�ʱ��</param>
        /// <param name="p_strFirstPrintDate">�״δ�ӡʱ��</param>
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
            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            try
            {
                //δʵ�� 
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
