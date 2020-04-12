using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using System.Security;
using weCare.Core.Entity;

namespace com.digitalwave.Emr.DigitalSign_srv
{
    /// <summary>
    /// 电子签名表操作数据库
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsDigitalSign_srv : ServicedComponent
    {
        #region 保存数字签名信息
        /// <summary>
        /// 保存数字签名信息
        /// </summary>
        /// <param name="p_objPrincipal">角色权限</param>
        /// <param name="p_strInsertSql">签名vo</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddDigitalSign(clsEmrDigitalSign_VO p_objRecord)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();

            //验证权限
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
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(9, out objDPArr);
                //按顺序给IDataParameter赋值
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
                //执行
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            //释放
            objHRPSvc = null;

            return lngRes;
        }
        #endregion

        #region 检查是否需要电子签名
        // <summary>
        /// 检查是否需要电子签名
        /// </summary>
        /// <param name="p_objPrincipal">角色权限</param>
        /// <param name="p_strFormID">窗体ID</param>
        /// <param name="p_blnNeed">false不需要 true需要</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckNeedToSign(string p_strFormID, out bool p_blnNeed)
        {
            long lngRes = 0;
            p_blnNeed = false;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                //检查窗体ID是否合法
                if (p_strFormID == null || p_strFormID.Trim().Length == 0)
                    return -1;
                //验证权限
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsDigitalSignService","m_lngCheckNeedToSign");
                //if(lngCheckRes <= 0)
                //    return lngCheckRes;
                //检查需要电子签名，状态=0需要电子签名
                string strSQL = @"select count(*) as RecordCount
										from t_aid_emr_form t
										where t.signflag_int = ?
										and t.formname_vchr =?";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                //按顺序给IDataParameter赋值
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
            //释放
            objHRPSvc = null;
            return lngRes;
        }
        #endregion

        #region 获取指定单据的最新数字签名信息
        /// <summary>
        /// 获取指定单据的最新数字签名信息;
        /// 先到实时表查，如果实时表没有再到历史表查
        /// </summary>
        /// <param name="p_objPrincipal">角色权限</param>
        /// <param name="p_strFormID">窗体ID</param>
        /// <param name="p_dtbValue">返回表</param>
        /// <param name="p_blnIsOutPatient">(false=未出过院，无须到历史表获取数据)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDigitalSign(string p_strFormID, string p_strFormRecordID, bool p_blnIsOutPatient, out DataTable p_dtbValue)
        {
            long lngRes = 0;
            p_dtbValue = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            { 
                //取最近签名的一条记录
                string strSQL = @"select *
								from (select t.*, t.rowid
										from t_emr_signcontent_realtime t
										where t.formid_vchr =?
											and t.formrecordid_vchr=?
										order by t.signid_int desc)
								where rownum = 1";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                //按顺序给IDataParameter赋值
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
                    //按顺序给IDataParameter赋值
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
            //释放
            objHRPSvc = null;
            return lngRes;
        }
        #endregion

        #region 把指定病人的记录从实时表移到历史表
        /// <summary>
        /// 把指定病人的记录从实时表移到历史表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strFormRecordID">住院号-住院日期:例(00134272-2005-11-18 08:11:50)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMoveDigitalSign(string p_strRegisterID)
        {
            if (string.IsNullOrEmpty(p_strRegisterID)) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            { 
                //取最近签名的一条记录
                string strSQL = @"insert into t_emr_signcontent
  select t.*
    from t_emr_signcontent_realtime t
   where t.REGISTERID_CHR = ?";

                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                //按顺序给IDataParameter赋值
                objDPArr[0].Value = p_strRegisterID.Trim();
                long lngRff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRff, objDPArr);

                if (lngRes > 0)
                {
                    strSQL = @"delete from t_emr_signcontent_realtime where REGISTERID_CHR = ?"; 
                    objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    //按顺序给IDataParameter赋值
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
            //释放
            objHRPSvc = null;
            return lngRes;
        }
        #endregion 把指定病人的记录从实时表移到历史表

    }
}
