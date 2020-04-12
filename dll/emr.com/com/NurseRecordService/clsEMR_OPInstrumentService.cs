using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// ������е�����ϵ�����(��)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_OPInstrumentService : clsDiseaseTrackService
    {
        #region SQL���

        #region  ��ȡָ�����˵�����δɾ����¼��ʱ��
        /// <summary>
        /// ��ȡָ�����˵�����δɾ����¼��ʱ��
        /// </summary>
        private const string c_strGetTimeListSQL = @"select distinct createdate,opendate 
													from t_emr_opinstrument 
													where inpatientid = ?
													 and inpatientdate= ?
													 and status=0";
        #endregion

        #region ���ұ�����
        /// <summary>
        /// ���ұ�����
        /// </summary>
        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.modifydate,
       a.modifyuserid,
       a.emr_seq,
       a.instrumentid,
       a.beforeop,
       a.beforeclose,
       a.afterclose,
       a.sequence_int,
       a.recorddate,
       b.instrumentid dict_instrumentid,
       b.instrumentname dict_instrumentname,
       b.orderid dict_orderid,
       b.status dict_status
  from t_emr_opinstrument_dict b
  left outer join (select inpatientid,
                          inpatientdate,
                          opendate,
                          createdate,
                          createuserid,
                          firstprintdate,
                          deactiveddate,
                          deactivedoperatorid,
                          status,
                          modifydate,
                          modifyuserid,
                          emr_seq,
                          instrumentid,
                          beforeop,
                          beforeclose,
                          afterclose,
                          sequence_int,
                          recorddate
                     from t_emr_opinstrument
                    where inpatientid = ?
                      and inpatientdate = ?
                      and opendate = ?
                      and status = 0) a on a.instrumentid = b.instrumentid
 where b.status = 0
 order by b.orderid";
        #endregion

        #region ��ȡָ��ʱ��ı�
        /// <summary>
        /// ��ȡָ��ʱ��ı�
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select distinct createuserid,opendate
														from t_emr_opinstrument
														where emr_seq = ? 
														and status=0";
        #endregion

        #region ��ȡָ����������޸�ʱ��
        /// <summary>
        /// ��ȡָ����������޸�ʱ��
        /// </summary>
        private const string c_strCheckLastModifyRecordSQL = @"select distinct a.modifydate, a.modifyuserid
                                                              from t_emr_opinstrument a
                                                             where emr_seq = ?
                                                               and a.status = 0";
        #endregion

        #region ��ȡɾ��������Ҫ��Ϣ
        /// <summary>
        /// ��ȡɾ��������Ҫ��Ϣ
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select distinct deactiveddate,deactivedoperatorid 
														from t_emr_opinstrument 
														where inpatientid = ?
														and inpatientdate= ?
														and opendate= ? 
														and status=1 ";
        #endregion

        #region ��Ӽ�¼
        /// <summary>
        /// ��Ӽ�¼
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_opinstrument (inpatientid,inpatientdate,opendate,
        createdate,createuserid,status,modifydate,modifyuserid,emr_seq,instrumentid,beforeop,beforeclose,afterclose,sequence_int,recorddate) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?)";
        #endregion

        #region ���þɼ�¼״̬Ϊ2
        /// <summary>
        /// ���þɼ�¼״̬Ϊ2
        /// </summary>
        private const string c_strSetOldRecordSQL = @"update t_emr_opinstrument set status = 2 where emr_seq = ? and status = 0";
        #endregion

        #region �޸ļ�¼
        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        private const string c_strModifyRecordSQL = c_strAddNewRecordSQL;
        #endregion

        #region ɾ����¼
        /// <summary>
        /// ɾ����¼
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_opinstrument 
													set status=1,deactiveddate=?,deactivedoperatorid=? 
													where emr_seq = ? 
													and status = 0";
        #endregion

        #region ��ȡLastModifyDate��FirstPrintDate
        /// <summary>
        /// ��ȡLastModifyDate��FirstPrintDate
        /// </summary>
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select distinct a.firstprintdate, a.modifydate
                                                                          from t_emr_opinstrument a
                                                                         where a.inpatientid = ?
                                                                           and a.inpatientdate = ?
                                                                           and a.opendate = ?
                                                                           and a.status = 0";
        #endregion

        #region ����FirstPrintDate
        /// <summary>
        /// ����FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update  t_emr_opinstrument 
															set firstprintdate= ? 
															where inpatientid= ? 
															and inpatientdate= ? 
															and opendate=? 
															and firstprintdate is null 
															and status=0";
        #endregion

        #region ��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ��
        /// <summary>
        /// ��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select distinct createdate,opendate 
																from t_emr_opinstrument 
																where inpatientid = ? 
																and inpatientdate= ? 
																and deactivedoperatorid= ? 
																and status=1";
        #endregion

        #region ��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ��
        /// <summary>
        /// ��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select distinct createdate,opendate 
																from t_emr_opinstrument 
																where inpatientid = ? 
																and inpatientdate= ? and status=1";
        #endregion

        #region ��ȡ��ɾ����¼
        /// <summary>
        /// ��ȡ��ɾ����¼
        /// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.modifydate,
       a.modifyuserid,
       a.emr_seq,
       a.instrumentid,
       a.beforeop,
       a.beforeclose,
       a.afterclose,
       a.sequence_int,
       a.recorddate,
       b.instrumentid dict_instrumentid,
       b.instrumentname dict_instrumentname,
       b.orderid dict_orderid,
       b.status dict_status
  from t_emr_opinstrument_dict b
  left outer join (select inpatientid,
                          inpatientdate,
                          opendate,
                          createdate,
                          createuserid,
                          firstprintdate,
                          deactiveddate,
                          deactivedoperatorid,
                          status,
                          modifydate,
                          modifyuserid,
                          emr_seq,
                          instrumentid,
                          beforeop,
                          beforeclose,
                          afterclose,
                          sequence_int,
                          recorddate
                     from t_emr_opinstrument
                    where inpatientid = ?
                      and inpatientdate = ?
                      and opendate = ?
                      and status = 1) a on a.instrumentid = b.instrumentid
 where b.status = 0
 order by b.orderid";
        #endregion

        #region ��Ӽ�¼���ֵ��
        /// <summary>
        /// �����Ŀ���ֵ��
        /// </summary>
        private const string c_strAddNewToDict = @"insert into t_emr_opinstrument_dict (instrumentid,instrumentname,orderid,status) values (?,?,?,?)"; 
        #endregion

        #region �޸���Ŀ����
        /// <summary>
        /// �޸���Ŀ����
        /// </summary>
        private const string c_strModifyToDict = @"update t_emr_opinstrument_dict set instrumentname = ? where instrumentid = ?"; 
        #endregion

        #region ͣ���ֵ����Ŀ
        /// <summary>
        /// ͣ���ֵ����Ŀ
        /// </summary>
        private const string c_strDelFromDict = @"update t_emr_opinstrument_dict set status = 1,orderid = ? where instrumentid = ?"; 
        #endregion

        #region �����ֵ����Ŀ
        /// <summary>
        /// �����ֵ����Ŀ
        /// </summary>
        private const string c_strActiveFromDict = @"update t_emr_opinstrument_dict set status = 0 where instrumentid = ?";
        #endregion

        #region ������������Ŀ˳��
        /// <summary>
        /// ������������Ŀ˳��
        /// </summary>
        private const string c_strSetOrderID = @"update t_emr_opinstrument_dict set orderid = ? where instrumentid = ?"; 
        #endregion

        #region ��ȡ�����ֵ����Ŀ
        /// <summary>
        /// ��ȡ�����ֵ����Ŀ
        /// </summary>
        private const string c_strGetAllDictItems = @"select instrumentid, instrumentname, orderid, status
  from t_emr_opinstrument_dict
 order by instrumentname"; 
        #endregion

        #region ��ȡ��������Ŀ
        /// <summary>
        /// ��ȡ��������Ŀ
        /// </summary>
        private const string c_strGetActiveDictItems = @"select instrumentid, instrumentname, orderid, status from t_emr_opinstrument_dict where status = 0 order by orderid"; 
        #endregion

        #region ����ֵ���Ƿ����и���Ŀ
        /// <summary>
        /// ����ֵ���Ƿ����и���Ŀ
        /// </summary>
        private const string c_strCheckSameItem = @"select instrumentid from t_emr_opinstrument_dict where instrumentname = ?"; 
        #endregion

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

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
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
                //����
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ	

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID.Trim();
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
            //����
            return lngRes;
        } 
        #endregion

        #region ��ȡ���˵��Ѿ���ָ��ɾ����ɾ����¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵��Ѿ���ָ��ɾ����ɾ����¼ʱ���б�
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

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strDeleteUserID))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID.Trim();
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
            //����
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

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
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
            //����
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
        /// <param name="objHRPServ"></param>
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

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate)
                    || string.IsNullOrEmpty(p_strOpenDate) || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsEMR_OPInstrument objOPInstrument = new clsEMR_OPInstrument();
                    objOPInstrument.m_strInPatientID = p_strInPatientID.Trim();
                    objOPInstrument.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objOPInstrument.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objOPInstrument.m_objOPInstrument = new clsEMR_OPInstrumentItem[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            objOPInstrument.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                            objOPInstrument.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                            objOPInstrument.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                            objOPInstrument.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                            if (dtbValue.Rows[0]["FirstPrintDate"] == DBNull.Value)
                                objOPInstrument.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objOPInstrument.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[0]["FirstPrintDate"]);
                            objOPInstrument.m_bytStatus = 0;
                            objOPInstrument.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);
                            if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                            {
                                objOPInstrument.m_lngSign_SEQ = Convert.ToInt64(dtbValue.Rows[0]["SEQUENCE_INT"]);
                            }
                            else
                            {
                                objOPInstrument.m_lngSign_SEQ = -1;
                            }
                            objOPInstrument.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                        }
                        objOPInstrument.m_objOPInstrument[i] = new clsEMR_OPInstrumentItem();
                        objOPInstrument.m_objOPInstrument[i].m_strAfterClose = dtbValue.Rows[i]["AFTERCLOSE"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_strBeforeClose = dtbValue.Rows[i]["BEFORECLOSE"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_strBeforeOP = dtbValue.Rows[i]["BEFOREOP"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo = new clsEMR_OPInstrument_Dict();
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOPInstrumentID =
                            Convert.ToInt32(dtbValue.Rows[i]["Dict_instrumentid"]);
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOrderID =
                            Convert.ToInt32(dtbValue.Rows[i]["Dict_orderid"]);
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_strOPInstrumentName =
                            dtbValue.Rows[i]["Dict_instrumentname"].ToString();
                    }
                    if (objOPInstrument.m_lngSign_SEQ != -1)
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        lngRes = objSign.m_lngGetSign(objOPInstrument.m_lngSign_SEQ, out objOPInstrument.objSignerArr);
                        objSign = null;
                    }
                    p_objRecordContent = objOPInstrument;
                }
                //����
                return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        } 
        #endregion

        #region �鿴�Ƿ�����ͬ�ļ�¼ʱ��
        /// <summary>
        /// �鿴�Ƿ�����ͬ�ļ�¼ʱ��
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="objHRPServ"></param>
        /// <param name="p_objPreModifyInfo">������ͬ��¼,���ظ���ͬ��¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
                clsHRPTableService p_objHRPServ,
                out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_OPInstrument objOPInstrument = p_objRecordContent as clsEMR_OPInstrument;
                if (objOPInstrument == null)
                    return -1;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = objOPInstrument.m_lngEMR_SEQ;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //�鿴DataTable.Rows.Count
                //�������1����ʾ�Ѿ��и�CreateDate�����Ҳ���ɾ���ļ�¼��
                //��ȡ�ü�¼����Ϣ����ֵ��p_objModifyInfo�С�����ֵʹ��Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//����
            return lngRes;
        } 
        #endregion

        #region �����¼�����ݿ�
        /// <summary>
        /// �����¼�����ݿ⡣
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
                clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            long lngSequence = 0;
            long lngSignSeq = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
                clsEMR_OPInstrument objContent = p_objRecordContent as clsEMR_OPInstrument;
                if (objContent == null || objContent.m_objOPInstrument == null)
                    return (long)enmOperationResult.Parameter_Error;

                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSeq = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSeq.m_lngGetSequenceValue("seq_emr", out lngSequence);
                lngRes = objSeq.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSeq);
                DateTime dtNow = DateTime.Now;

                for (int i = 0; i < objContent.m_objOPInstrument.Length; i++)
                {
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(15, out objDPArr);

                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[4].Value = objContent.m_strCreateUserID;
                    objDPArr[5].Value = 0;
                    objDPArr[6].DbType = DbType.DateTime;
                    objDPArr[6].Value = DateTime.Parse(objContent.m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[7].Value = objContent.m_strModifyUserID;
                    objDPArr[8].Value = lngSequence;
                    if (objContent.m_objOPInstrument[i].m_objOPInstrumentInfo == null)
                        objDPArr[9].Value = DBNull.Value;
                    else
                        objDPArr[9].Value = objContent.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOPInstrumentID;
                    objDPArr[10].Value = objContent.m_objOPInstrument[i].m_strBeforeOP;
                    objDPArr[11].Value = objContent.m_objOPInstrument[i].m_strBeforeClose;
                    objDPArr[12].Value = objContent.m_objOPInstrument[i].m_strAfterClose;
                    objDPArr[13].Value = lngSignSeq;
                    objDPArr[14].DbType = DbType.DateTime;
                    objDPArr[14].Value = DateTime.Parse(objContent.m_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    //ִ��SQL
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);

                    if (lngRes < 0)
                        return lngRes;
                }
                //����ǩ������
                if (objContent.objSignerArr != null && objContent.objSignerArr.Length > 0)
                {
                    lngRes = objSeq.m_lngAddSign(objContent.objSignerArr, lngSignSeq);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        } 
        #endregion

        #region �鿴��ǰ��¼�Ƿ����µļ�¼
        /// <summary>
        /// �鿴��ǰ��¼�Ƿ����µļ�¼��
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="objHRPServ"></param>		
        /// <param name="p_objModifyInfo">����ǰ��¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
                clsHRPTableService p_objHRPServ,
                out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_OPInstrument objOPInstrument = p_objRecordContent as clsEMR_OPInstrument;
                if (objOPInstrument == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = objOPInstrument.m_lngEMR_SEQ;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    IDataParameter[] objDPArr2 = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr2);

                    objDPArr2[0].Value = objOPInstrument.m_strInPatientID;
                    objDPArr2[1].DbType = DbType.DateTime;
                    objDPArr2[1].Value = objOPInstrument.m_dtmInPatientDate;
                    objDPArr2[2].DbType = DbType.DateTime;
                    objDPArr2[2].Value = objOPInstrument.m_dtmOpenDate;
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr2);

                    if (lngRes > 0 && dtbValue.Rows.Count > 0)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //��DataTable�л�ȡModifyDate��ʹ֮��p_objRecordContent.m_dtmModifyDate�Ƚ�
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //�����ͬ������DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //���򣬷���Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        } 
        #endregion

        #region �����޸ĵ����ݱ��浽���ݿ�
        /// <summary>
        /// �����޸ĵ����ݱ��浽���ݿ�
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
                clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                long lngSignSeq = -1;
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSeq);
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ			
                clsEMR_OPInstrument objContent = p_objRecordContent as clsEMR_OPInstrument;

                if (objContent == null || objContent.m_objOPInstrument == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArrSetOld = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArrSetOld);
                objDPArrSetOld[0].Value = objContent.m_lngEMR_SEQ;
                long lngEffOld = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strSetOldRecordSQL, ref lngEffOld, objDPArrSetOld);
                if (lngRes < 0)
                    return lngRes;

                for (int i = 0; i < objContent.m_objOPInstrument.Length; i++)
                {
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(15, out objDPArr);

                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(objContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[4].Value = objContent.m_strCreateUserID;
                    objDPArr[5].Value = 0;
                    objDPArr[6].DbType = DbType.DateTime;
                    objDPArr[6].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[7].Value = objContent.m_strModifyUserID;
                    objDPArr[8].Value = objContent.m_lngEMR_SEQ;
                    if (objContent.m_objOPInstrument[i].m_objOPInstrumentInfo == null)
                        objDPArr[9].Value = DBNull.Value;
                    else
                        objDPArr[9].Value = objContent.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOPInstrumentID;
                    objDPArr[10].Value = objContent.m_objOPInstrument[i].m_strBeforeOP;
                    objDPArr[11].Value = objContent.m_objOPInstrument[i].m_strBeforeClose;
                    objDPArr[12].Value = objContent.m_objOPInstrument[i].m_strAfterClose;
                    objDPArr[13].Value = lngSignSeq;
                    objDPArr[14].DbType = DbType.DateTime;
                    objDPArr[14].Value = DateTime.Parse(objContent.m_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    //ִ��SQL
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);

                    if (lngRes < 0)
                        return lngRes;
                }
                //����ǩ������
                if (objContent.objSignerArr != null && objContent.objSignerArr.Length > 0)
                {
                    lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSeq);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        } 
        #endregion

        #region �Ѽ�¼��������ɾ��
        /// <summary>
        /// �Ѽ�¼�������С�ɾ������
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
                clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_OPInstrument objOPInstrument = p_objRecordContent as clsEMR_OPInstrument;
                if (objOPInstrument == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objOPInstrument.m_dtmDeActivedDate;
                objDPArr[1].Value = objOPInstrument.m_strDeActivedOperatorID;
                objDPArr[2].Value = objOPInstrument.m_lngEMR_SEQ;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//����
            return lngRes;
        } 
        #endregion

        #region ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        /// <summary>
        /// ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="objHRPServ"></param>
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
                //����
                return lngRes;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//����
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
        /// <param name="objHRPServ"></param>
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

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsEMR_OPInstrument objOPInstrument = new clsEMR_OPInstrument();
                    objOPInstrument.m_strInPatientID = p_strInPatientID.Trim();
                    objOPInstrument.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objOPInstrument.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objOPInstrument.m_objOPInstrument = new clsEMR_OPInstrumentItem[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            objOPInstrument.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                            objOPInstrument.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                            objOPInstrument.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                            objOPInstrument.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                            if (dtbValue.Rows[0]["FirstPrintDate"] == DBNull.Value)
                                objOPInstrument.m_dtmFirstPrintDate = DateTime.MinValue;
                            else objOPInstrument.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[0]["FirstPrintDate"]);
                            objOPInstrument.m_bytStatus = 0;
                            objOPInstrument.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);
                            if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                            {
                                objOPInstrument.m_lngSign_SEQ = Convert.ToInt64(dtbValue.Rows[0]["SEQUENCE_INT"]);
                            }
                            else
                            {
                                objOPInstrument.m_lngSign_SEQ = -1;
                            } 
                            objOPInstrument.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                        }
                        objOPInstrument.m_objOPInstrument[i] = new clsEMR_OPInstrumentItem();
                        objOPInstrument.m_objOPInstrument[i].m_strAfterClose = dtbValue.Rows[i]["AFTERCLOSE"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_strBeforeClose = dtbValue.Rows[i]["BEFORECLOSE"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_strBeforeOP = dtbValue.Rows[i]["BEFOREOP"].ToString();
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo = new clsEMR_OPInstrument_Dict();
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOPInstrumentID =
                            Convert.ToInt32(dtbValue.Rows[i]["Dict_instrumentid"]);
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_intOrderID =
                            Convert.ToInt32(dtbValue.Rows[i]["Dict_orderid"]);
                        objOPInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_strOPInstrumentName =
                            dtbValue.Rows[i]["Dict_instrumentname"].ToString();
                    }
                    if (objOPInstrument.m_lngSign_SEQ != -1)
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        lngRes = objSign.m_lngGetSign(objOPInstrument.m_lngSign_SEQ, out objOPInstrument.objSignerArr);
                        objSign = null;
                    }
                    p_objRecordContent = objOPInstrument;
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }		//����
            return lngRes;
        } 
        #endregion

        #region �����Ŀ���ֵ��
        /// <summary>
        /// �����Ŀ���ֵ��
        /// </summary>
        /// <param name="p_strItemName">��Ŀ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewToDict(string p_strItemName)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                if (string.IsNullOrEmpty(p_strItemName))
                    return (long)enmOperationResult.Parameter_Error;

                int intOPInstrumentID = 0;
                lngRes = objHRPServ.m_lngGenerateNewID("T_EMR_OPINSTRUMENT_DICT", "INSTRUMENTID", out intOPInstrumentID);
                
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);

                objDPArr[0].Value = intOPInstrumentID;
                objDPArr[1].Value = p_strItemName.Trim();
                objDPArr[2].Value = DBNull.Value;
                objDPArr[3].Value = 1;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewToDict, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region �޸��ֵ����Ŀ
        /// <summary>
        /// �޸��ֵ����Ŀ
        /// </summary>
        /// <param name="p_intOPInstrumentID"></param>
        /// <param name="p_strOPInstrumentName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyToDisc(int p_intOPInstrumentID, string p_strOPInstrumentName)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                if (string.IsNullOrEmpty(p_strOPInstrumentName))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strOPInstrumentName.Trim();
                objDPArr[1].Value = p_intOPInstrumentID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyToDict, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region ͣ���ֵ����Ŀ
        /// <summary>
        /// ͣ���ֵ����Ŀ
        /// </summary>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeActiveItemFromDict(int p_intOPInstrumentID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = DBNull.Value;
                objDPArr[1].Value = p_intOPInstrumentID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDelFromDict, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region �����ֵ����Ŀ
        /// <summary>
        /// �����ֵ����Ŀ
        /// </summary>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngActiveItemFromDict(int p_intOPInstrumentID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_intOPInstrumentID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strActiveFromDict, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region ������������Ŀ˳��
        /// <summary>
        /// ������������Ŀ˳��
        /// </summary>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <param name="p_intOrderID">˳���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderID(int p_intOPInstrumentID, int p_intOrderID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_intOrderID;
                objDPArr[1].Value = p_intOPInstrumentID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strSetOrderID, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region ��ȡ�����ֵ����Ŀ
        /// <summary>
        /// ��ȡ�����ֵ����Ŀ
        /// </summary>
        /// <param name="p_obDictItems">������Ŀ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllItemsFromDict(out clsEMR_OPInstrument_Dict[] p_obDictItems)
        {
            p_obDictItems = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.DoGetDataTable(c_strGetAllDictItems,ref dtbValue);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_obDictItems = new clsEMR_OPInstrument_Dict[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_obDictItems[i] = new clsEMR_OPInstrument_Dict();
                        p_obDictItems[i].m_intOPInstrumentID = Convert.ToInt32(dtbValue.Rows[i]["INSTRUMENTID"]);
                        if(dtbValue.Rows[i]["ORDERID"] != DBNull.Value)
                            p_obDictItems[i].m_intOrderID = Convert.ToInt32(dtbValue.Rows[i]["ORDERID"]);
                        p_obDictItems[i].m_intStatus = Convert.ToInt32(dtbValue.Rows[i]["STATUS"]);
                        p_obDictItems[i].m_strOPInstrumentName = dtbValue.Rows[i]["INSTRUMENTNAME"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion

        #region ��ȡ�������ֵ����Ŀ
        /// <summary>
        /// ��ȡ�������ֵ����Ŀ
        /// </summary>
        /// <param name="p_obDictItems">��������Ŀ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetActiveItemsFromDict(out clsEMR_OPInstrument_Dict[] p_obDictItems)
        {
            p_obDictItems = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.DoGetDataTable(c_strGetActiveDictItems, ref dtbValue);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_obDictItems = new clsEMR_OPInstrument_Dict[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_obDictItems[i] = new clsEMR_OPInstrument_Dict();
                        p_obDictItems[i].m_intOPInstrumentID = Convert.ToInt32(dtbValue.Rows[i]["INSTRUMENTID"]);
                        if (dtbValue.Rows[i]["ORDERID"] != DBNull.Value)
                            p_obDictItems[i].m_intOrderID = Convert.ToInt32(dtbValue.Rows[i]["ORDERID"]);
                        p_obDictItems[i].m_intStatus = Convert.ToInt32(dtbValue.Rows[i]["STATUS"]);
                        p_obDictItems[i].m_strOPInstrumentName = dtbValue.Rows[i]["INSTRUMENTNAME"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ֵ���Ƿ����и���Ŀ
        /// <summary>
        /// ����ֵ���Ƿ����и���Ŀ
        /// </summary>
        /// <param name="p_strOPInstrumentName">��Ŀ����</param>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckSameItemID(string p_strOPInstrumentName,out int p_intOPInstrumentID)
        {
            long lngRes = 0;
            p_intOPInstrumentID = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if(string.IsNullOrEmpty(p_strOPInstrumentName))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strOPInstrumentName.Trim();

                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckSameItem, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_intOPInstrumentID = Convert.ToInt32(dtbValue.Rows[0][0]);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        } 
        #endregion
    }
}