using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using ServMain = com.digitalwave.iCare.middletier.HRPService;//10g
using com.digitalwave.Utility;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// ������������
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsLockCaseServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// ��ȡָ��ʱ���ڵ���������
        /// </summary>
        /// <param name="p_dtmBeginDate">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEndDate">��ѯ����ʱ��</param>
        /// <param name="p_dtbLockData">������������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLockData(DateTime p_dtmBeginDate, DateTime p_dtmEndDate, out DataTable p_dtbLockData)
        {
            long lngRes = 0;
            p_dtbLockData = null;
            try
            {
                string strSQL = @"select t.emr_seq,
       t.registerid_chr,
       t.formid_vchr,
       t.status_int,
       t.unlockuserid_vchr,
       t.unlockdate_dat,
       t.lockdate_dat,
       t.formname_vchr,
       r.inpatientid_chr,
       e.lastname_vchr doctor,
       d.deptname_vchr dept,
       rd.lastname_vchr patientname,
       rd.sex_chr patientsex
  from t_emr_lockcase t
 inner join t_opr_bih_register r on t.registerid_chr = r.registerid_chr
                                and r.status_int = 1
 inner join t_opr_bih_registerdetail rd on r.registerid_chr =
                                           rd.registerid_chr
 inner join t_bse_employee e on e.empid_chr = r.casedoctor_chr
                            and e.status_int = 1
 inner join t_bse_deptdesc d on d.deptid_chr = r.areaid_chr
                            and d.status_int = 1
 where t.lockdate_dat between ? and ?
 order by t.status_int desc";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(2, out objSeqArr1);
                objSeqArr1[0].DbType = DbType.DateTime;
                objSeqArr1[0].Value = p_dtmBeginDate;
                objSeqArr1[1].DbType = DbType.DateTime;
                objSeqArr1[1].Value = p_dtmEndDate;

                lngRes = objHRPMain.lngGetDataTableWithParameters(strSQL, ref p_dtbLockData, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        /// <summary>
        /// ���ָ�������Ƿ��ѽ���
        /// </summary>
        /// <param name="p_strFormID">����ID</param>
        /// <param name="p_strRegisterID">������Ժ�ǼǺ�</param>
        /// <param name="p_dtmLastCreateTime">����������һ�μ�¼ʱ��</param>
        /// <param name="p_blnAlreadyLock">�Ƿ��Ѵ���������¼(�����ж����δ�������Ƿ���Ҫ���������¼)</param>
        /// <param name="p_blnIsUnlock">�Ƿ��ѽ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckIsUnLock(string p_strFormID, string p_strRegisterID, DateTime p_dtmLastCreateTime,out bool p_blnAlreadyLock, out bool p_blnIsUnlock)
        {
            p_blnIsUnlock = false;
            p_blnAlreadyLock = false;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.emr_seq,
       t.registerid_chr,
       t.formid_vchr,
       t.status_int,
       t.unlockuserid_vchr,
       t.unlockdate_dat,
       t.lockdate_dat
  from t_emr_lockcase t
 where t.registerid_chr = ?
   and t.formid_vchr = ?
 order by t.lockdate_dat desc";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(2, out objSeqArr1);
                objSeqArr1[0].Value = p_strRegisterID;
                objSeqArr1[1].Value = p_strFormID;

                DataTable dtbValue = null;
                lngRes = objHRPMain.lngGetDataTableWithParameters(strSQL, ref dtbValue, objSeqArr1);
                if (dtbValue == null || dtbValue.Rows.Count == 0)
                {
                    p_blnIsUnlock = false;
                }
                else
                {
                    if (dtbValue.Rows[0]["status_int"].ToString() == "0")
                    {
                        DateTime dtmUnlockDate = DateTime.MinValue;
                        if (DateTime.TryParse(dtbValue.Rows[0]["unlockdate_dat"].ToString(), out dtmUnlockDate))
                        {
                            if (dtmUnlockDate < p_dtmLastCreateTime)
                            {
                                p_blnAlreadyLock = false;
                                p_blnIsUnlock = false;
                            }
                            else
                            {
                                p_blnIsUnlock = true;
                            }
                        }
                    }
                    else
                    {
                        p_blnAlreadyLock = true;
                        p_blnIsUnlock = false;
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        /// <summary>
        /// ���̼�¼�Ƿ����
        /// </summary>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngCheckYoN(out long checkNo)
        {
            long lngRes = 0;
            checkNo = 0;
            try
            {
                string strSQL = @"select t.setstatus_int from t_sys_setting t 
                            where t.moduleid_chr=?
                            and t.setid_chr=?";
                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(2, out objSeqArr1);
                objSeqArr1[0].Value = "0006";
                objSeqArr1[1].Value = "3022";

                DataTable dtbValue = null;
                lngRes = objHRPMain.lngGetDataTableWithParameters(strSQL, ref dtbValue, objSeqArr1);
                if (dtbValue == null || dtbValue.Rows.Count == 0)
                {
                    lngRes = 0;
                }
                else
                {
                   long.TryParse(dtbValue.Rows[0]["SETSTATUS_INT"].ToString(), out checkNo);
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
    }

    /// <summary>
    /// ������������
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsLockCaseServ_Modify : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// ����������������
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�ǼǺ�</param>
        /// <param name="p_strFormID">����ID</param>
        /// <param name="p_strFormName">��������</param>
        /// <param name="p_dtmLockData">��������ʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddLockData(string p_strRegisterID, string p_strFormID,string p_strFormName, DateTime p_dtmLockData)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"insert into t_emr_lockcase
   (emr_seq, registerid_chr, formid_vchr, status_int, lockdate_dat,formname_vchr)
 values
   (seq_emr.nextval, ?, ?, ?, ?, ?)";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(5, out objSeqArr1);
                objSeqArr1[0].Value = p_strRegisterID;
                objSeqArr1[1].Value = p_strFormID;
                objSeqArr1[2].Value = 1;
                objSeqArr1[3].DbType = DbType.DateTime;
                objSeqArr1[3].Value = p_dtmLockData;
                objSeqArr1[4].Value = p_strFormName;
                
                long lngEff = -1;
                lngRes = objHRPMain.lngExecuteParameterSQL(strSQL, ref lngEff, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }


        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="p_lngSeq">���к�</param>
        /// <param name="p_strUnlockUserID">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUnLockData(long p_lngSeq, string p_strUnlockUserID)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_emr_lockcase t
     set t.unlockuserid_vchr = ?, t.unlockdate_dat = ?, t.status_int = 0
   where t.emr_seq = ?";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(3, out objSeqArr1);
                objSeqArr1[0].Value = p_strUnlockUserID;
                objSeqArr1[1].DbType = DbType.DateTime;
                objSeqArr1[1].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objSeqArr1[2].Value = p_lngSeq;

                long lngEff = -1;
                lngRes = objHRPMain.lngExecuteParameterSQL(strSQL, ref lngEff, objSeqArr1);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }




        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="p_lngSeqArr">���к�</param>
        /// <param name="p_strUnlockUserID">������ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUnLockData(long[] p_lngSeqArr, string p_strUnlockUserID)
        {
            long lngRes = 0;
            if (p_lngSeqArr == null)
            {
                return -1;
            }
            try
            {
                string strSQL = @"update t_emr_lockcase t
     set t.unlockuserid_vchr = ?, t.unlockdate_dat = ?, t.status_int = 0
   where t.emr_seq = ?";

                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                DbType[] dbTypes = new DbType[] { DbType.String, DbType.DateTime, DbType.Int64 };
                object[][] objValues = new object[7][];
                if (p_lngSeqArr.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[p_lngSeqArr.Length];//��ʼ��
                    }

                    for (int k1 = 0; k1 < p_lngSeqArr.Length; k1++)
                    {
                        objValues[0][k1] = p_strUnlockUserID;
                        objValues[1][k1] = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        objValues[2][k1] = p_lngSeqArr[k1];
                    }
                    lngRes = objHRPMain.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
       
    }
}
