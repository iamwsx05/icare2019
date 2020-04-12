using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using System.Security;
using weCare.Core.Entity;

namespace com.digitalwave.Emr.DigitalSign_srv
{
    /// <summary>
    /// ����ǩ����������ݿ�
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsDigitalSign_srv : ServicedComponent
    {
        #region ��������ǩ����Ϣ
        /// <summary>
        /// ��������ǩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ɫȨ��</param>
        /// <param name="p_strInsertSql">ǩ��vo</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddDigitalSign(clsEmrDigitalSign_VO p_objRecord)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();

            //��֤Ȩ��
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDigitalSignService","m_lngAddDigitalSign");
            //if(lngCheckRes <= 0)
            //    return lngCheckRes;
            //string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"INSERT INTO T_EMR_SIGNCONTENT_REALTIME 
								(SIGNID_INT,
								CONTENT_TXT,
								DSCONTENT_TXT,
								SIGNDATE_DAT,
								SIGNIDID_VCHR,
								SIGNNAME_VCHR,
								DESCRIPTION_VCHR,
								FORMID_VCHR,
								FORMRECORDID_VCHR,REGISTERID_CHR)
								VALUES
								(SEQ_EMR_DIGITALSIGN.Nextval, ?, ?, ?, ?, ?, ?, ?,?,?)";//9
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(9, out objDPArr);
                //��˳���IDataParameter��ֵ
                objDPArr[0].DbType = DbType.Binary;
                objDPArr[0].Value = p_objRecord.m_bteCONTENT_TXT;
                objDPArr[1].Value = p_objRecord.m_strDSCONTENT_TXT;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecord.m_datSIGNDATE_DAT;
                objDPArr[3].Value = p_objRecord.m_strSIGNIDID_VCHR;
                objDPArr[4].Value = p_objRecord.m_strSIGNNAME_VCHR;
                objDPArr[5].Value = p_objRecord.m_strDESCRIPTION_VCHR;
                objDPArr[6].Value = p_objRecord.m_strFORMID_VCHR;
                objDPArr[7].Value = p_objRecord.m_strFORMRECORDID_VCHR;
                objDPArr[8].Value = p_objRecord.m_strRegisterId;
                long lngRecEff = -1;
                //ִ��
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            //�ͷ�
            objHRPSvc = null;

            return lngRes;
        }
        #endregion

        #region ����Ƿ���Ҫ����ǩ��
        // <summary>
        /// ����Ƿ���Ҫ����ǩ��
        /// </summary>
        /// <param name="p_objPrincipal">��ɫȨ��</param>
        /// <param name="p_strFormID">����ID</param>
        /// <param name="p_blnNeed">false����Ҫ true��Ҫ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckNeedToSign(string p_strFormID, out bool p_blnNeed)
        {
            long lngRes = 0;
            p_blnNeed = false;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //��鴰��ID�Ƿ�Ϸ�
                if (p_strFormID == null || p_strFormID.Trim().Length == 0)
                    return -1;
                //��֤Ȩ��
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDigitalSignService","m_lngCheckNeedToSign");
                //if(lngCheckRes <= 0)
                //    return lngCheckRes;
                //�����Ҫ����ǩ����״̬=0��Ҫ����ǩ��
                string strSQL = @"select count(*) as RecordCount
										from t_aid_emr_form t
										where t.signflag_int = ?
										and t.formname_vchr =?";

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                //��˳���IDataParameter��ֵ
                objDPArr[0].Value = 0;
                objDPArr[1].Value = p_strFormID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    int intCount = dtResult.Rows[0]["RECORDCOUNT"] == null ? 0 : Convert.ToInt32(dtResult.Rows[0]["RECORDCOUNT"]);
                    if (intCount == 1)
                        p_blnNeed = true;
                }

            }
            catch (Exception objEx)
            {
                //string strTmp=objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            //�ͷ�
            objHRPSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡָ�����ݵ���������ǩ����Ϣ
        /// <summary>
        /// ��ȡָ�����ݵ���������ǩ����Ϣ;
        /// �ȵ�ʵʱ��飬���ʵʱ��û���ٵ���ʷ���
        /// </summary>
        /// <param name="p_objPrincipal">��ɫȨ��</param>
        /// <param name="p_strFormID">����ID</param>
        /// <param name="p_dtbValue">���ر�</param>
        /// <param name="p_blnIsOutPatient">(false=δ����Ժ�����뵽��ʷ���ȡ����)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDigitalSign(string p_strFormID, string p_strFormRecordID, bool p_blnIsOutPatient, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            { 
                //ȡ���ǩ����һ����¼
                string strSQL = @"select *
								from (select t.*, t.rowid
										from t_emr_signcontent_realtime t
										where t.formid_vchr =?
											and t.formrecordid_vchr=?
										order by t.signid_int desc)
								where rownum = 1";

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                //��˳���IDataParameter��ֵ
                objDPArr[0].Value = p_strFormID.Trim();
                objDPArr[1].Value = p_strFormRecordID.Trim();

                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult.Rows.Count > 0)
                    p_dtbValue = dtResult;
                else if (p_blnIsOutPatient)
                {
                    strSQL = @"select *
								from (select t.*, t.rowid
										from t_emr_signcontent t
										where t.formid_vchr =?
											and t.formrecordid_vchr=?
										order by t.signid_int desc)
								where rownum = 1"; 
                    objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                    //��˳���IDataParameter��ֵ
                    objDPArr[0].Value = p_strFormID.Trim();
                    objDPArr[1].Value = p_strFormRecordID.Trim();

                    dtResult = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                    if (lngRes > 0)
                        p_dtbValue = dtResult;
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            //�ͷ�
            objHRPSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ָ�����˵ļ�¼��ʵʱ���Ƶ���ʷ��
        /// <summary>
        /// ��ָ�����˵ļ�¼��ʵʱ���Ƶ���ʷ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormRecordID">סԺ��-סԺ����:��(00134272-2005-11-18 08:11:50)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMoveDigitalSign(string p_strRegisterID)
        {
            if (string.IsNullOrEmpty(p_strRegisterID)) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            { 
                //ȡ���ǩ����һ����¼
                string strSQL = @"insert into t_emr_signcontent
  select t.*
    from t_emr_signcontent_realtime t
   where t.REGISTERID_CHR = ?";

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                //��˳���IDataParameter��ֵ
                objDPArr[0].Value = p_strRegisterID.Trim();
                long lngRff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);

                if (lngRes > 0)
                {
                    strSQL = @"delete from t_emr_signcontent_realtime where REGISTERID_CHR = ?"; 
                    objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    //��˳���IDataParameter��ֵ
                    objDPArr[0].Value = p_strRegisterID.Trim();
                    lngRff = -1;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            //�ͷ�
            objHRPSvc = null;
            return lngRes;
        }
        #endregion ��ָ�����˵ļ�¼��ʵʱ���Ƶ���ʷ��

    }
}
