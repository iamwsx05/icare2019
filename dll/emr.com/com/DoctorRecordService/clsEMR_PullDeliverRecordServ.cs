using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// ����̥ͷ����������������¼
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_PullDeliverRecordServ : clsDiseaseTrackService
    {
        #region SQL���
        #region ��ȡָ�����˵�����û��ɾ����¼��ʱ��
        /// <summary>
        /// ��ȡָ�����˵�����û��ɾ����¼��ʱ��
        /// </summary>
        private const string c_strGetTimeListSQL = @"select createdate, opendate
                                                      from t_emr_pulldeliverrecord
                                                     where inpatientid = ?
                                                       and inpatientdate = ?
                                                       and status = 0";
        #endregion 

        #region ����ָ��������Ϣ�����ұ�������
        /// <summary>
        /// ����ָ��������Ϣ�����ұ�������
        /// </summary>
        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.emr_seq,
       a.pregnanttimes,
       a.laytimes,
       a.opdate,
       a.diagnosisbeforeop,
       a.diagnosisbeforeopxml,
       a.opindication,
       a.opindicationxml,
       a.uterusheight,
       a.uterusheightxml,
       a.abdomenround,
       a.abdomenroundxml,
       a.presentation,
       a.presentationxml,
       a.linkup,
       a.fetusweight,
       a.fetusweightxml,
       a.ischialspine,
       a.coccyxradian,
       a.ischiumnotch,
       a.dc,
       a.dcxml,
       a.uterusora,
       a.uterusoraxml,
       a.amniocentesis,
       a.fetusplace,
       a.fetusplacexml,
       a.presentationheitht,
       a.presentationheithtxml,
       a.skull,
       a.caputsuccedaneumsize,
       a.caputsuccedaneumsizexml,
       a.caputsuccedaneumplace,
       a.caputsuccedaneumplacexml,
       a.uterusoraopen,
       a.uterusoraopenxml,
       a.presentationplace,
       a.presentationplacexml,
       a.lateralincisorana,
       a.lateralincisoranaxml,
       a.minuspress,
       a.minuspressxml,
       a.apgar1,
       a.apgar1xml,
       a.apgar2,
       a.apgar2xml,
       a.afterchildbearing,
       a.afterchildbearingxml,
       a.bleedinginop,
       a.bleedinginopxml,
       a.diagnosisafterop,
       a.diagnosisafteropxml,
       a.anamode,
       a.anamodexml,
       a.recorddate,
       a.registerid_chr,
       a.sequence_int,
       a.pulltime,
       a.pulltimexml,
       b.modifydate,
       b.modifyuserid,
       b.status,
       b.diagnosisbeforeop_right,
       b.opindication_right,
       b.uterusheight_right,
       b.abdomenround_right,
       b.presentation_right,
       b.fetusweight_right,
       b.dc_right,
       b.uterusora_right,
       b.fetusplace_right,
       b.presentationheitht_right,
       b.caputsuccedaneumsize_right,
       b.caputsuccedaneumplace_right,
       b.uterusoraopen_right,
       b.presentationplace_right,
       b.lateralincisorana_right,
       b.minuspress_right,
       b.apgar1_right,
       b.apgar2_right,
       b.afterchildbearing_right,
       b.bleedinginop_right,
       b.diagnosisafterop_right,
       b.anamode_right,
       b.pulltime_right
  from t_emr_pulldeliverrecord a, t_emr_pulldeliverrecordcon b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and a.emr_seq = b.emr_seq
   and b.status = 0"; 
        #endregion

        #region ��ȡָ��ʱ��ı�
        /// <summary>
        /// ��ȡָ��ʱ��ı�
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid, opendate
                                                          from t_emr_pulldeliverrecord
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and createdate = ?
                                                           and status = 0";
        #endregion

        #region ��ȡָ����������޸�ʱ��
        /// <summary>
        /// ��ȡָ����������޸�ʱ��
        /// </summary>
        private const string c_strCheckLastModifyRecordSQL = @"select b.modifydate, b.modifyuserid
                                                              from t_emr_pulldeliverrecordcon b
                                                             where emr_seq = ?
                                                               and b.status = 0";
        #endregion

        #region ��ȡɾ��������Ҫ��Ϣ
        /// <summary>
        /// ��ȡɾ��������Ҫ��Ϣ
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
                                                          from t_emr_pulldeliverrecord
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and opendate = ?
                                                           and status = 1 ";
        #endregion

        #region ��Ӽ�¼��T_EMR_PULLDELIVERRECORD
        /// <summary>
        /// ��Ӽ�¼��T_EMR_PULLDELIVERRECORD
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_pulldeliverrecord (inpatientid,inpatientdate,opendate,
        createdate,createuserid,status,emr_seq,pregnanttimes,laytimes,opdate,diagnosisbeforeop,diagnosisbeforeopxml,opindication,
        opindicationxml,uterusheight,uterusheightxml,abdomenround,abdomenroundxml,presentation,presentationxml,linkup,fetusweight,
        fetusweightxml,ischialspine,coccyxradian,ischiumnotch,dc,dcxml,uterusora,uterusoraxml,amniocentesis,fetusplace,fetusplacexml,
        presentationheitht,presentationheithtxml,skull,caputsuccedaneumsize,caputsuccedaneumsizexml,caputsuccedaneumplace,
        caputsuccedaneumplacexml,uterusoraopen,uterusoraopenxml,presentationplace,presentationplacexml,lateralincisorana,
        lateralincisoranaxml,minuspress,minuspressxml,apgar1,apgar1xml,apgar2,apgar2xml,afterchildbearing,afterchildbearingxml,
        bleedinginop,bleedinginopxml,diagnosisafterop,diagnosisafteropxml,anamode,anamodexml,recorddate,registerid_chr,sequence_int,pulltime,pulltimexml)
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?)";
        #endregion

        #region ��Ӽ�¼��T_EMR_PULLDELIVERRECORDCON
        /// <summary>
        /// ��Ӽ�¼��T_EMR_PULLDELIVERRECORDCON
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_pulldeliverrecordcon (inpatientid,inpatientdate,opendate,
        modifydate,modifyuserid,emr_seq,status,registerid_chr,diagnosisbeforeop_right,opindication_right,uterusheight_right,
        abdomenround_right,presentation_right,fetusweight_right,dc_right,uterusora_right,fetusplace_right,presentationheitht_right,
        caputsuccedaneumsize_right,caputsuccedaneumplace_right,uterusoraopen_right,presentationplace_right,lateralincisorana_right,
        minuspress_right,apgar1_right,apgar2_right,afterchildbearing_right,bleedinginop_right,diagnosisafterop_right,anamode_right,pulltime_right) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,?)";
        #endregion

        #region �޸ļ�¼��T_EMR_PULLDELIVERRECORD
        /// <summary>
        /// �޸ļ�¼��T_EMR_PULLDELIVERRECORD
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_pulldeliverrecord set pregnanttimes = ?,laytimes = ?,opdate = ?,diagnosisbeforeop = ?,
        diagnosisbeforeopxml = ?,opindication = ?,opindicationxml = ?,uterusheight = ?,uterusheightxml = ?,abdomenround = ?,abdomenroundxml = ?,presentation = ?,
        presentationxml = ?,linkup = ?,fetusweight = ?,fetusweightxml = ?,ischialspine = ?,coccyxradian = ?,ischiumnotch = ?,dc = ?,dcxml = ?,uterusora = ?,uterusoraxml = ?,
        amniocentesis = ?,fetusplace = ?,fetusplacexml = ?,presentationheitht = ?,presentationheithtxml = ?,skull = ?,caputsuccedaneumsize = ?,caputsuccedaneumsizexml = ?,
        caputsuccedaneumplace = ?,caputsuccedaneumplacexml = ?,uterusoraopen = ?,uterusoraopenxml = ?,presentationplace = ?,presentationplacexml = ?,lateralincisorana = ?,
        lateralincisoranaxml = ?,minuspress = ?,minuspressxml = ?,apgar1 = ?,apgar1xml = ?,apgar2 = ?,apgar2xml = ?,afterchildbearing = ?,afterchildbearingxml = ?,
        bleedinginop = ?,bleedinginopxml = ?,diagnosisafterop = ?,diagnosisafteropxml = ?,anamode = ?,anamodexml = ?,recorddate = ?,sequence_int = ?,pulltime = ?,pulltimexml = ? 
        where emr_seq = ? and status=0";
        #endregion

        #region �޸ļ�¼��T_EMR_PULLDELIVERRECORDCON
        /// <summary>
        /// ����T_EMR_PULLDELIVERRECORDCON�ɼ�¼״̬Ϊ2
        /// </summary>
        private const string c_strSetOldRecordSQL = @"update t_emr_pulldeliverrecordcon set status = 2 where emr_seq = ? and status = 0";

        /// <summary>
        /// �޸ļ�¼��T_EMR_PULLDELIVERRECORDCON
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;
        #endregion

        #region ����T_EMR_PULLDELIVERRECORD��ɾ����¼����Ϣ
        /// <summary>
        /// ����T_EMR_PULLDELIVERRECORD��ɾ����¼����Ϣ
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_pulldeliverrecord
                                                       set status = 1, deactiveddate = ?, deactivedoperatorid = ?
                                                     where emr_seq = ?
                                                       and status = 0";
        #endregion

        #region ��ȡLastModifyDate��FirstPrintDate
        /// <summary>
        /// ��ȡLastModifyDate��FirstPrintDate
        /// </summary>
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate, b.modifydate
                                                                      from t_emr_pulldeliverrecord a, t_emr_pulldeliverrecordcon b
                                                                     where a.inpatientid = ?
                                                                       and a.inpatientdate = ?
                                                                       and a.opendate = ?
                                                                       and a.status = 0
                                                                       and a.emr_seq = b.emr_seq
                                                                       and b.status = 0";
        #endregion

        #region ����FirstPrintDate
        /// <summary>
        /// ����FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update  t_emr_pulldeliverrecord 
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
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate,opendate 
																from t_emr_pulldeliverrecord 
																where inpatientid = ? 
																and inpatientdate= ? 
																and deactivedoperatorid= ? 
																and status=1";
        #endregion

        #region ��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ��
        /// <summary>
        /// ��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate,opendate 
																from t_emr_pulldeliverrecord 
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
       a.emr_seq,
       a.pregnanttimes,
       a.laytimes,
       a.opdate,
       a.diagnosisbeforeop,
       a.diagnosisbeforeopxml,
       a.opindication,
       a.opindicationxml,
       a.uterusheight,
       a.uterusheightxml,
       a.abdomenround,
       a.abdomenroundxml,
       a.presentation,
       a.presentationxml,
       a.linkup,
       a.fetusweight,
       a.fetusweightxml,
       a.ischialspine,
       a.coccyxradian,
       a.ischiumnotch,
       a.dc,
       a.dcxml,
       a.uterusora,
       a.uterusoraxml,
       a.amniocentesis,
       a.fetusplace,
       a.fetusplacexml,
       a.presentationheitht,
       a.presentationheithtxml,
       a.skull,
       a.caputsuccedaneumsize,
       a.caputsuccedaneumsizexml,
       a.caputsuccedaneumplace,
       a.caputsuccedaneumplacexml,
       a.uterusoraopen,
       a.uterusoraopenxml,
       a.presentationplace,
       a.presentationplacexml,
       a.lateralincisorana,
       a.lateralincisoranaxml,
       a.minuspress,
       a.minuspressxml,
       a.apgar1,
       a.apgar1xml,
       a.apgar2,
       a.apgar2xml,
       a.afterchildbearing,
       a.afterchildbearingxml,
       a.bleedinginop,
       a.bleedinginopxml,
       a.diagnosisafterop,
       a.diagnosisafteropxml,
       a.anamode,
       a.anamodexml,
       a.recorddate,
       a.registerid_chr,
       a.sequence_int,
       a.pulltime,
       a.pulltimexml,
       b.modifydate,
       b.modifyuserid,
       b.status,
       b.diagnosisbeforeop_right,
       b.opindication_right,
       b.uterusheight_right,
       b.abdomenround_right,
       b.presentation_right,
       b.fetusweight_right,
       b.dc_right,
       b.uterusora_right,
       b.fetusplace_right,
       b.presentationheitht_right,
       b.caputsuccedaneumsize_right,
       b.caputsuccedaneumplace_right,
       b.uterusoraopen_right,
       b.presentationplace_right,
       b.lateralincisorana_right,
       b.minuspress_right,
       b.apgar1_right,
       b.apgar2_right,
       b.afterchildbearing_right,
       b.bleedinginop_right,
       b.diagnosisafterop_right,
       b.anamode_right,
       b.pulltime_right
  from t_emr_pulldeliverrecord a, t_emr_pulldeliverrecordcon b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and a.emr_seq = b.emr_seq
   and b.status = 0";
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
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }		//����
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
                //������                              
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return (long)enmOperationResult.Parameter_Error;
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

        #region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
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
        /// <param name="p_objHRPServ"></param>
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
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    clsEMR_PullDeliverRecordvalue objRecordContent = new clsEMR_PullDeliverRecordvalue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"] == DBNull.Value)
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["STATUS"] == DBNull.Value)
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    if (dtbValue.Rows[0]["EMR_SEQ"] == DBNull.Value)
                        return -1;
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);

                    if (dtbValue.Rows[0]["PREGNANTTIMES"] == DBNull.Value)
                        objRecordContent.m_intPREGNANTTIMES = -1;
                    else
                        objRecordContent.m_intPREGNANTTIMES = Convert.ToInt32(dtbValue.Rows[0]["PREGNANTTIMES"].ToString());
                    if(dtbValue.Rows[0]["LAYTIMES"] == DBNull.Value)
                        objRecordContent.m_intLAYTIMES = -1;
                    else
                        objRecordContent.m_intLAYTIMES = Convert.ToInt32(dtbValue.Rows[0]["LAYTIMES"].ToString());
                    objRecordContent.m_dtmOPDATE = Convert.ToDateTime(dtbValue.Rows[0]["OPDATE"]);
                    objRecordContent.m_strDIAGNOSISBEFOREOP = dtbValue.Rows[0]["DIAGNOSISBEFOREOP"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOPXML = dtbValue.Rows[0]["DIAGNOSISBEFOREOPXML"].ToString();
                    objRecordContent.m_strOPINDICATION = dtbValue.Rows[0]["OPINDICATION"].ToString();
                    objRecordContent.m_strOPINDICATIONXML = dtbValue.Rows[0]["OPINDICATIONXML"].ToString();
                    objRecordContent.m_strUTERUSHEIGHT = dtbValue.Rows[0]["UTERUSHEIGHT"].ToString();
                    objRecordContent.m_strUTERUSHEIGHTXML = dtbValue.Rows[0]["UTERUSHEIGHTXML"].ToString();
                    objRecordContent.m_strABDOMENROUND = dtbValue.Rows[0]["ABDOMENROUND"].ToString();
                    objRecordContent.m_strABDOMENROUNDXML = dtbValue.Rows[0]["ABDOMENROUNDXML"].ToString();
                    objRecordContent.m_strPRESENTATION = dtbValue.Rows[0]["PRESENTATION"].ToString();
                    objRecordContent.m_strPRESENTATIONXML = dtbValue.Rows[0]["PRESENTATIONXML"].ToString();
                    objRecordContent.m_strLINKUP = dtbValue.Rows[0]["LINKUP"].ToString();
                    objRecordContent.m_strFETUSWEIGHT = dtbValue.Rows[0]["FETUSWEIGHT"].ToString();
                    objRecordContent.m_strFETUSWEIGHTXML = dtbValue.Rows[0]["FETUSWEIGHTXML"].ToString();
                    objRecordContent.m_strISCHIALSPINE = dtbValue.Rows[0]["ISCHIALSPINE"].ToString();
                    objRecordContent.m_strCOCCYXRADIAN = dtbValue.Rows[0]["COCCYXRADIAN"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH = dtbValue.Rows[0]["ISCHIUMNOTCH"].ToString();
                    objRecordContent.m_strDC = dtbValue.Rows[0]["DC"].ToString();
                    objRecordContent.m_strDCXML = dtbValue.Rows[0]["DCXML"].ToString();
                    objRecordContent.m_strUTERUSORA = dtbValue.Rows[0]["UTERUSORA"].ToString();
                    objRecordContent.m_strUTERUSORAXML = dtbValue.Rows[0]["UTERUSORAXML"].ToString();
                    objRecordContent.m_strAMNIOCENTESIS = dtbValue.Rows[0]["AMNIOCENTESIS"].ToString();
                    objRecordContent.m_strFETUSPLACE = dtbValue.Rows[0]["FETUSPLACE"].ToString();
                    objRecordContent.m_strFETUSPLACEXML = dtbValue.Rows[0]["FETUSPLACEXML"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHT = dtbValue.Rows[0]["PRESENTATIONHEITHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHTXML = dtbValue.Rows[0]["PRESENTATIONHEITHTXML"].ToString();
                    objRecordContent.m_strSKULL = dtbValue.Rows[0]["SKULL"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE = dtbValue.Rows[0]["CAPUTSUCCEDANEUMSIZE"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEXML = dtbValue.Rows[0]["CAPUTSUCCEDANEUMSIZEXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE = dtbValue.Rows[0]["CAPUTSUCCEDANEUMPLACE"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACEXML = dtbValue.Rows[0]["CAPUTSUCCEDANEUMPLACEXML"].ToString();
                    objRecordContent.m_strUTERUSORAOPEN = dtbValue.Rows[0]["UTERUSORAOPEN"].ToString();
                    objRecordContent.m_strUTERUSORAOPENXML = dtbValue.Rows[0]["UTERUSORAOPENXML"].ToString();
                    objRecordContent.m_strPRESENTATIONPLACE = dtbValue.Rows[0]["PRESENTATIONPLACE"].ToString();
                    objRecordContent.m_strPRESENTATIONPLACEXML = dtbValue.Rows[0]["PRESENTATIONPLACEXML"].ToString();
                    objRecordContent.m_strLATERALINCISORANA = dtbValue.Rows[0]["LATERALINCISORANA"].ToString();
                    objRecordContent.m_strLATERALINCISORANAXML = dtbValue.Rows[0]["LATERALINCISORANAXML"].ToString();
                    objRecordContent.m_strMINUSPRESS = dtbValue.Rows[0]["MINUSPRESS"].ToString();
                    objRecordContent.m_strMINUSPRESSXML = dtbValue.Rows[0]["MINUSPRESSXML"].ToString();
                    objRecordContent.m_strAPGAR1 = dtbValue.Rows[0]["APGAR1"].ToString();
                    objRecordContent.m_strAPGAR1XML = dtbValue.Rows[0]["APGAR1XML"].ToString();
                    objRecordContent.m_strAPGAR2 = dtbValue.Rows[0]["APGAR2"].ToString();
                    objRecordContent.m_strAPGAR2XML = dtbValue.Rows[0]["APGAR2XML"].ToString();
                    objRecordContent.m_strAFTERCHILDBEARING = dtbValue.Rows[0]["AFTERCHILDBEARING"].ToString();
                    objRecordContent.m_strAFTERCHILDBEARINGXML = dtbValue.Rows[0]["AFTERCHILDBEARINGXML"].ToString();
                    objRecordContent.m_strBLEEDINGINOP = dtbValue.Rows[0]["BLEEDINGINOP"].ToString();
                    objRecordContent.m_strBLEEDINGINOPXML = dtbValue.Rows[0]["BLEEDINGINOPXML"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROP = dtbValue.Rows[0]["DIAGNOSISAFTEROP"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROPXML = dtbValue.Rows[0]["DIAGNOSISAFTEROPXML"].ToString();
                    objRecordContent.m_strANAMODE = dtbValue.Rows[0]["ANAMODE"].ToString();
                    objRecordContent.m_strANAMODEXML = dtbValue.Rows[0]["ANAMODEXML"].ToString();
                    objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    objRecordContent.m_strREGISTERID_CHR = dtbValue.Rows[0]["REGISTERID_CHR"].ToString();
                    objRecordContent.m_strPULLTIME = dtbValue.Rows[0]["PULLTIME"].ToString();
                    objRecordContent.m_strPULLTIMEXML = dtbValue.Rows[0]["PULLTIMEXML"].ToString();

                    objRecordContent.m_strOPINDICATION_RIGHT = dtbValue.Rows[0]["OPINDICATION_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSHEIGHT_RIGHT = dtbValue.Rows[0]["UTERUSHEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strABDOMENROUND_RIGHT = dtbValue.Rows[0]["ABDOMENROUND_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION_RIGHT = dtbValue.Rows[0]["PRESENTATION_RIGHT"].ToString();
                    objRecordContent.m_strFETUSWEIGHT_RIGHT = dtbValue.Rows[0]["FETUSWEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strDC_RIGHT = dtbValue.Rows[0]["DC_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSORA_RIGHT = dtbValue.Rows[0]["UTERUSORA_RIGHT"].ToString();
                    objRecordContent.m_strFETUSPLACE_RIGHT = dtbValue.Rows[0]["FETUSPLACE_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHT_RIGHT = dtbValue.Rows[0]["PRESENTATIONHEITHT_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT = dtbValue.Rows[0]["CAPUTSUCCEDANEUMSIZE_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE_RIGHT = dtbValue.Rows[0]["CAPUTSUCCEDANEUMPLACE_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSORAOPEN_RIGHT = dtbValue.Rows[0]["UTERUSORAOPEN_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONPLACE_RIGHT = dtbValue.Rows[0]["PRESENTATIONPLACE_RIGHT"].ToString();
                    objRecordContent.m_strLATERALINCISORANA_RIGHT = dtbValue.Rows[0]["LATERALINCISORANA_RIGHT"].ToString();
                    objRecordContent.m_strMINUSPRESS_RIGHT = dtbValue.Rows[0]["MINUSPRESS_RIGHT"].ToString();
                    objRecordContent.m_strAPGAR1_RIGHT = dtbValue.Rows[0]["APGAR1_RIGHT"].ToString();
                    objRecordContent.m_strAPGAR2_RIGHT = dtbValue.Rows[0]["APGAR2_RIGHT"].ToString();
                    objRecordContent.m_strAFTERCHILDBEARING_RIGHT = dtbValue.Rows[0]["AFTERCHILDBEARING_RIGHT"].ToString();
                    objRecordContent.m_strBLEEDINGINOP_RIGHT = dtbValue.Rows[0]["BLEEDINGINOP_RIGHT"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROP_RIGHT = dtbValue.Rows[0]["DIAGNOSISAFTEROP_RIGHT"].ToString();
                    objRecordContent.m_strANAMODE_RIGHT = dtbValue.Rows[0]["ANAMODE_RIGHT"].ToString();
                    objRecordContent.m_strPULLTIME_RIGHT = dtbValue.Rows[0]["PULLTIME_RIGHT"].ToString();

                    //��ȡǩ������
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
                    }

                    p_objRecordContent = objRecordContent;
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

        #region �鿴�Ƿ�����ͬ�ļ�¼ʱ��
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

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

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

        #region �����¼�����ݿ⡣�������,����ӱ�.
        /// <summary>
        /// �����¼�����ݿ⡣�������,����ӱ�.
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ	
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                long lngSignSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);
                lngRes = objSign.m_lngGetSequenceValue("seq_emr", out lngSequence);

                clsEMR_PullDeliverRecordvalue objContent = (clsEMR_PullDeliverRecordvalue)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(65, out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID.Trim();
                objDPArr[5].Value = 0;
                objDPArr[6].Value = lngSequence;
                if (objContent.m_intPREGNANTTIMES == -1)
                    objDPArr[7].Value = null;
                else
                    objDPArr[7].Value = objContent.m_intPREGNANTTIMES;
                if (objContent.m_intLAYTIMES == -1)
                    objDPArr[8].Value = null;
                else
                    objDPArr[8].Value = objContent.m_intLAYTIMES;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = objContent.m_dtmOPDATE;
                objDPArr[10].Value = objContent.m_strDIAGNOSISBEFOREOP;
                objDPArr[11].Value = objContent.m_strDIAGNOSISBEFOREOPXML;
                objDPArr[12].Value = objContent.m_strOPINDICATION;
                objDPArr[13].Value = objContent.m_strOPINDICATIONXML;
                objDPArr[14].Value = objContent.m_strUTERUSHEIGHT;
                objDPArr[15].Value = objContent.m_strUTERUSHEIGHTXML;
                objDPArr[16].Value = objContent.m_strABDOMENROUND;
                objDPArr[17].Value = objContent.m_strABDOMENROUNDXML;
                objDPArr[18].Value = objContent.m_strPRESENTATION;
                objDPArr[19].Value = objContent.m_strPRESENTATIONXML;
                objDPArr[20].Value = objContent.m_strLINKUP;
                objDPArr[21].Value = objContent.m_strFETUSWEIGHT;
                objDPArr[22].Value = objContent.m_strFETUSWEIGHTXML;
                objDPArr[23].Value = objContent.m_strISCHIALSPINE;
                objDPArr[24].Value = objContent.m_strCOCCYXRADIAN;
                objDPArr[25].Value = objContent.m_strISCHIUMNOTCH;
                objDPArr[26].Value = objContent.m_strDC;
                objDPArr[27].Value = objContent.m_strDCXML;
                objDPArr[28].Value = objContent.m_strUTERUSORA;
                objDPArr[29].Value = objContent.m_strUTERUSORAXML;
                objDPArr[30].Value = objContent.m_strAMNIOCENTESIS;
                objDPArr[31].Value = objContent.m_strFETUSPLACE;
                objDPArr[32].Value = objContent.m_strFETUSPLACEXML;
                objDPArr[33].Value = objContent.m_strPRESENTATIONHEITHT;
                objDPArr[34].Value = objContent.m_strPRESENTATIONHEITHTXML;
                objDPArr[35].Value = objContent.m_strSKULL;
                objDPArr[36].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE;
                objDPArr[37].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEXML;
                objDPArr[38].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE;
                objDPArr[39].Value = objContent.m_strCAPUTSUCCEDANEUMPLACEXML;
                objDPArr[40].Value = objContent.m_strUTERUSORAOPEN;
                objDPArr[41].Value = objContent.m_strUTERUSORAOPENXML;
                objDPArr[42].Value = objContent.m_strPRESENTATIONPLACE;
                objDPArr[43].Value = objContent.m_strPRESENTATIONPLACEXML;
                objDPArr[44].Value = objContent.m_strLATERALINCISORANA;
                objDPArr[45].Value = objContent.m_strLATERALINCISORANAXML;
                objDPArr[46].Value = objContent.m_strMINUSPRESS;
                objDPArr[47].Value = objContent.m_strMINUSPRESSXML;
                objDPArr[48].Value = objContent.m_strAPGAR1;
                objDPArr[49].Value = objContent.m_strAPGAR1XML;
                objDPArr[50].Value = objContent.m_strAPGAR2;
                objDPArr[51].Value = objContent.m_strAPGAR2XML;
                objDPArr[52].Value = objContent.m_strAFTERCHILDBEARING;
                objDPArr[53].Value = objContent.m_strAFTERCHILDBEARINGXML;
                objDPArr[54].Value = objContent.m_strBLEEDINGINOP;
                objDPArr[55].Value = objContent.m_strBLEEDINGINOPXML;
                objDPArr[56].Value = objContent.m_strDIAGNOSISAFTEROP;
                objDPArr[57].Value = objContent.m_strDIAGNOSISAFTEROPXML;
                objDPArr[58].Value = objContent.m_strANAMODE;
                objDPArr[59].Value = objContent.m_strANAMODEXML;
                objDPArr[60].DbType = DbType.DateTime;
                objDPArr[60].Value = objContent.m_dtmRECORDDATE;
                objDPArr[61].Value = objContent.m_strREGISTERID_CHR;
                objDPArr[62].Value = lngSignSequence;
                objDPArr[63].Value = objContent.m_strPULLTIME;
                objDPArr[64].Value = objContent.m_strPULLTIMEXML;


                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(31, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = lngSequence;
                objDPArr2[6].Value = 0;
                objDPArr2[7].Value = objContent.m_strREGISTERID_CHR;
                objDPArr2[8].Value = objContent.m_strDIAGNOSISBEFOREOP_RIGHT;
                objDPArr2[9].Value = objContent.m_strOPINDICATION_RIGHT;
                objDPArr2[10].Value = objContent.m_strUTERUSHEIGHT_RIGHT;
                objDPArr2[11].Value = objContent.m_strABDOMENROUND_RIGHT;
                objDPArr2[12].Value = objContent.m_strPRESENTATION_RIGHT;
                objDPArr2[13].Value = objContent.m_strFETUSWEIGHT_RIGHT;
                objDPArr2[14].Value = objContent.m_strDC_RIGHT;
                objDPArr2[15].Value = objContent.m_strUTERUSORA_RIGHT;
                objDPArr2[16].Value = objContent.m_strFETUSPLACE_RIGHT;
                objDPArr2[17].Value = objContent.m_strPRESENTATIONHEITHT_RIGHT;
                objDPArr2[18].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT;
                objDPArr2[19].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE_RIGHT;
                objDPArr2[20].Value = objContent.m_strUTERUSORAOPEN_RIGHT;
                objDPArr2[21].Value = objContent.m_strPRESENTATIONPLACE_RIGHT;
                objDPArr2[22].Value = objContent.m_strLATERALINCISORANA_RIGHT;
                objDPArr2[23].Value = objContent.m_strMINUSPRESS_RIGHT;
                objDPArr2[24].Value = objContent.m_strAPGAR1_RIGHT;
                objDPArr2[25].Value = objContent.m_strAPGAR2_RIGHT;
                objDPArr2[26].Value = objContent.m_strAFTERCHILDBEARING_RIGHT;
                objDPArr2[27].Value = objContent.m_strBLEEDINGINOP_RIGHT;
                objDPArr2[28].Value = objContent.m_strDIAGNOSISAFTEROP_RIGHT;
                objDPArr2[29].Value = objContent.m_strANAMODE_RIGHT;
                objDPArr2[30].Value = objContent.m_strPULLTIME_RIGHT;

                //ִ��SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                
                //�ͷ�
                objSign = null;
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
        /// <param name="p_objHRPServ"></param>		
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_PullDeliverRecordvalue objContent = p_objRecordContent as clsEMR_PullDeliverRecordvalue;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = objContent.m_lngEMR_SEQ;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = objContent.m_dtmOpenDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
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
        /// �����޸ĵ����ݱ��浽���ݿ⡣��������,����ӱ�.
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
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
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ	
                //��ȡǩ����ˮ��
                long lngSignSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);

                clsEMR_PullDeliverRecordvalue objContent = (clsEMR_PullDeliverRecordvalue)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(58, out objDPArr);
                if (objContent.m_intPREGNANTTIMES == -1)
                    objDPArr[0].Value = null;
                else
                    objDPArr[0].Value = objContent.m_intPREGNANTTIMES;
                if (objContent.m_intLAYTIMES == -1)
                    objDPArr[1].Value = null;
                else
                    objDPArr[1].Value = objContent.m_intLAYTIMES;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOPDATE;
                objDPArr[3].Value = objContent.m_strDIAGNOSISBEFOREOP;
                objDPArr[4].Value = objContent.m_strDIAGNOSISBEFOREOPXML;
                objDPArr[5].Value = objContent.m_strOPINDICATION;
                objDPArr[6].Value = objContent.m_strOPINDICATIONXML;
                objDPArr[7].Value = objContent.m_strUTERUSHEIGHT;
                objDPArr[8].Value = objContent.m_strUTERUSHEIGHTXML;
                objDPArr[9].Value = objContent.m_strABDOMENROUND;
                objDPArr[10].Value = objContent.m_strABDOMENROUNDXML;
                objDPArr[11].Value = objContent.m_strPRESENTATION;
                objDPArr[12].Value = objContent.m_strPRESENTATIONXML;
                objDPArr[13].Value = objContent.m_strLINKUP;
                objDPArr[14].Value = objContent.m_strFETUSWEIGHT;
                objDPArr[15].Value = objContent.m_strFETUSWEIGHTXML;
                objDPArr[16].Value = objContent.m_strISCHIALSPINE;
                objDPArr[17].Value = objContent.m_strCOCCYXRADIAN;
                objDPArr[18].Value = objContent.m_strISCHIUMNOTCH;
                objDPArr[19].Value = objContent.m_strDC;
                objDPArr[20].Value = objContent.m_strDCXML;
                objDPArr[21].Value = objContent.m_strUTERUSORA;
                objDPArr[22].Value = objContent.m_strUTERUSORAXML;
                objDPArr[23].Value = objContent.m_strAMNIOCENTESIS;
                objDPArr[24].Value = objContent.m_strFETUSPLACE;
                objDPArr[25].Value = objContent.m_strFETUSPLACEXML;
                objDPArr[26].Value = objContent.m_strPRESENTATIONHEITHT;
                objDPArr[27].Value = objContent.m_strPRESENTATIONHEITHTXML;
                objDPArr[28].Value = objContent.m_strSKULL;
                objDPArr[29].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE;
                objDPArr[30].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEXML;
                objDPArr[31].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE;
                objDPArr[32].Value = objContent.m_strCAPUTSUCCEDANEUMPLACEXML;
                objDPArr[33].Value = objContent.m_strUTERUSORAOPEN;
                objDPArr[34].Value = objContent.m_strUTERUSORAOPENXML;
                objDPArr[35].Value = objContent.m_strPRESENTATIONPLACE;
                objDPArr[36].Value = objContent.m_strPRESENTATIONPLACEXML;
                objDPArr[37].Value = objContent.m_strLATERALINCISORANA;
                objDPArr[38].Value = objContent.m_strLATERALINCISORANAXML;
                objDPArr[39].Value = objContent.m_strMINUSPRESS;
                objDPArr[40].Value = objContent.m_strMINUSPRESSXML;
                objDPArr[41].Value = objContent.m_strAPGAR1;
                objDPArr[42].Value = objContent.m_strAPGAR1XML;
                objDPArr[43].Value = objContent.m_strAPGAR2;
                objDPArr[44].Value = objContent.m_strAPGAR2XML;
                objDPArr[45].Value = objContent.m_strAFTERCHILDBEARING;
                objDPArr[46].Value = objContent.m_strAFTERCHILDBEARINGXML;
                objDPArr[47].Value = objContent.m_strBLEEDINGINOP;
                objDPArr[48].Value = objContent.m_strBLEEDINGINOPXML;
                objDPArr[49].Value = objContent.m_strDIAGNOSISAFTEROP;
                objDPArr[50].Value = objContent.m_strDIAGNOSISAFTEROPXML;
                objDPArr[51].Value = objContent.m_strANAMODE;
                objDPArr[52].Value = objContent.m_strANAMODEXML;
                objDPArr[53].DbType = DbType.DateTime;
                objDPArr[53].Value = objContent.m_dtmRECORDDATE;
                objDPArr[54].Value = lngSignSequence;
                objDPArr[55].Value = objContent.m_strPULLTIME;
                objDPArr[56].Value = objContent.m_strPULLTIMEXML;
                objDPArr[57].Value = objContent.m_lngEMR_SEQ;


                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSequence);

                //���þɼ�¼״̬Ϊ2
                IDataParameter[] objDPArrStatus = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArrStatus);
                objDPArrStatus[0].Value = objContent.m_lngEMR_SEQ;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strSetOldRecordSQL, ref lngEff, objDPArrStatus);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(31, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_lngEMR_SEQ;
                objDPArr2[6].Value = 0;
                objDPArr2[7].Value = objContent.m_strREGISTERID_CHR;
                objDPArr2[8].Value = objContent.m_strDIAGNOSISBEFOREOP_RIGHT;
                objDPArr2[9].Value = objContent.m_strOPINDICATION_RIGHT;
                objDPArr2[10].Value = objContent.m_strUTERUSHEIGHT_RIGHT;
                objDPArr2[11].Value = objContent.m_strABDOMENROUND_RIGHT;
                objDPArr2[12].Value = objContent.m_strPRESENTATION_RIGHT;
                objDPArr2[13].Value = objContent.m_strFETUSWEIGHT_RIGHT;
                objDPArr2[14].Value = objContent.m_strDC_RIGHT;
                objDPArr2[15].Value = objContent.m_strUTERUSORA_RIGHT;
                objDPArr2[16].Value = objContent.m_strFETUSPLACE_RIGHT;
                objDPArr2[17].Value = objContent.m_strPRESENTATIONHEITHT_RIGHT;
                objDPArr2[18].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT;
                objDPArr2[19].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE_RIGHT;
                objDPArr2[20].Value = objContent.m_strUTERUSORAOPEN_RIGHT;
                objDPArr2[21].Value = objContent.m_strPRESENTATIONPLACE_RIGHT;
                objDPArr2[22].Value = objContent.m_strLATERALINCISORANA_RIGHT;
                objDPArr2[23].Value = objContent.m_strMINUSPRESS_RIGHT;
                objDPArr2[24].Value = objContent.m_strAPGAR1_RIGHT;
                objDPArr2[25].Value = objContent.m_strAPGAR2_RIGHT;
                objDPArr2[26].Value = objContent.m_strAFTERCHILDBEARING_RIGHT;
                objDPArr2[27].Value = objContent.m_strBLEEDINGINOP_RIGHT;
                objDPArr2[28].Value = objContent.m_strDIAGNOSISAFTEROP_RIGHT;
                objDPArr2[29].Value = objContent.m_strANAMODE_RIGHT;
                objDPArr2[30].Value = objContent.m_strPULLTIME_RIGHT;

                //ִ��SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);


                //�ͷ�
                objSign = null;
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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                clsEMR_PullDeliverRecordvalue objContent = p_objRecordContent as clsEMR_PullDeliverRecordvalue;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objContent.m_lngEMR_SEQ;

                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

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
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
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
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
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

        #region ��ȡָ���Ѿ���ɾ����¼������
        /// <summary>
        /// ��ȡָ���Ѿ���ɾ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objHRPServ"></param>
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
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    clsEMR_PullDeliverRecordvalue objRecordContent = new clsEMR_PullDeliverRecordvalue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE"] == DBNull.Value)
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    if (dtbValue.Rows[0]["STATUS"] == DBNull.Value)
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    if (dtbValue.Rows[0]["EMR_SEQ"] == DBNull.Value)
                        return -1;
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValue.Rows[0]["EMR_SEQ"]);

                    if (dtbValue.Rows[0]["PREGNANTTIMES"] == DBNull.Value)
                        objRecordContent.m_intPREGNANTTIMES = -1;
                    else
                        objRecordContent.m_intPREGNANTTIMES = Convert.ToInt32(dtbValue.Rows[0]["PREGNANTTIMES"].ToString());
                    if (dtbValue.Rows[0]["LAYTIMES"] == DBNull.Value)
                        objRecordContent.m_intLAYTIMES = -1;
                    else
                        objRecordContent.m_intLAYTIMES = Convert.ToInt32(dtbValue.Rows[0]["LAYTIMES"].ToString());
                    objRecordContent.m_dtmOPDATE = Convert.ToDateTime(dtbValue.Rows[0]["OPDATE"]);
                    objRecordContent.m_strDIAGNOSISBEFOREOP = dtbValue.Rows[0]["DIAGNOSISBEFOREOP"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOPXML = dtbValue.Rows[0]["DIAGNOSISBEFOREOPXML"].ToString();
                    objRecordContent.m_strOPINDICATION = dtbValue.Rows[0]["OPINDICATION"].ToString();
                    objRecordContent.m_strOPINDICATIONXML = dtbValue.Rows[0]["OPINDICATIONXML"].ToString();
                    objRecordContent.m_strUTERUSHEIGHT = dtbValue.Rows[0]["UTERUSHEIGHT"].ToString();
                    objRecordContent.m_strUTERUSHEIGHTXML = dtbValue.Rows[0]["UTERUSHEIGHTXML"].ToString();
                    objRecordContent.m_strABDOMENROUND = dtbValue.Rows[0]["ABDOMENROUND"].ToString();
                    objRecordContent.m_strABDOMENROUNDXML = dtbValue.Rows[0]["ABDOMENROUNDXML"].ToString();
                    objRecordContent.m_strPRESENTATION = dtbValue.Rows[0]["PRESENTATION"].ToString();
                    objRecordContent.m_strPRESENTATIONXML = dtbValue.Rows[0]["PRESENTATIONXML"].ToString();
                    objRecordContent.m_strLINKUP = dtbValue.Rows[0]["LINKUP"].ToString();
                    objRecordContent.m_strFETUSWEIGHT = dtbValue.Rows[0]["FETUSWEIGHT"].ToString();
                    objRecordContent.m_strFETUSWEIGHTXML = dtbValue.Rows[0]["FETUSWEIGHTXML"].ToString();
                    objRecordContent.m_strISCHIALSPINE = dtbValue.Rows[0]["ISCHIALSPINE"].ToString();
                    objRecordContent.m_strCOCCYXRADIAN = dtbValue.Rows[0]["COCCYXRADIAN"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH = dtbValue.Rows[0]["ISCHIUMNOTCH"].ToString();
                    objRecordContent.m_strDC = dtbValue.Rows[0]["DC"].ToString();
                    objRecordContent.m_strDCXML = dtbValue.Rows[0]["DCXML"].ToString();
                    objRecordContent.m_strUTERUSORA = dtbValue.Rows[0]["UTERUSORA"].ToString();
                    objRecordContent.m_strUTERUSORAXML = dtbValue.Rows[0]["UTERUSORAXML"].ToString();
                    objRecordContent.m_strAMNIOCENTESIS = dtbValue.Rows[0]["AMNIOCENTESIS"].ToString();
                    objRecordContent.m_strFETUSPLACE = dtbValue.Rows[0]["FETUSPLACE"].ToString();
                    objRecordContent.m_strFETUSPLACEXML = dtbValue.Rows[0]["FETUSPLACEXML"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHT = dtbValue.Rows[0]["PRESENTATIONHEITHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHTXML = dtbValue.Rows[0]["PRESENTATIONHEITHTXML"].ToString();
                    objRecordContent.m_strSKULL = dtbValue.Rows[0]["SKULL"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE = dtbValue.Rows[0]["CAPUTSUCCEDANEUMSIZE"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEXML = dtbValue.Rows[0]["CAPUTSUCCEDANEUMSIZEXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE = dtbValue.Rows[0]["CAPUTSUCCEDANEUMPLACE"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACEXML = dtbValue.Rows[0]["CAPUTSUCCEDANEUMPLACEXML"].ToString();
                    objRecordContent.m_strUTERUSORAOPEN = dtbValue.Rows[0]["UTERUSORAOPEN"].ToString();
                    objRecordContent.m_strUTERUSORAOPENXML = dtbValue.Rows[0]["UTERUSORAOPENXML"].ToString();
                    objRecordContent.m_strPRESENTATIONPLACE = dtbValue.Rows[0]["PRESENTATIONPLACE"].ToString();
                    objRecordContent.m_strPRESENTATIONPLACEXML = dtbValue.Rows[0]["PRESENTATIONPLACEXML"].ToString();
                    objRecordContent.m_strLATERALINCISORANA = dtbValue.Rows[0]["LATERALINCISORANA"].ToString();
                    objRecordContent.m_strLATERALINCISORANAXML = dtbValue.Rows[0]["LATERALINCISORANAXML"].ToString();
                    objRecordContent.m_strMINUSPRESS = dtbValue.Rows[0]["MINUSPRESS"].ToString();
                    objRecordContent.m_strMINUSPRESSXML = dtbValue.Rows[0]["MINUSPRESSXML"].ToString();
                    objRecordContent.m_strAPGAR1 = dtbValue.Rows[0]["APGAR1"].ToString();
                    objRecordContent.m_strAPGAR1XML = dtbValue.Rows[0]["APGAR1XML"].ToString();
                    objRecordContent.m_strAPGAR2 = dtbValue.Rows[0]["APGAR2"].ToString();
                    objRecordContent.m_strAPGAR2XML = dtbValue.Rows[0]["APGAR2XML"].ToString();
                    objRecordContent.m_strAFTERCHILDBEARING = dtbValue.Rows[0]["AFTERCHILDBEARING"].ToString();
                    objRecordContent.m_strAFTERCHILDBEARINGXML = dtbValue.Rows[0]["AFTERCHILDBEARINGXML"].ToString();
                    objRecordContent.m_strBLEEDINGINOP = dtbValue.Rows[0]["BLEEDINGINOP"].ToString();
                    objRecordContent.m_strBLEEDINGINOPXML = dtbValue.Rows[0]["BLEEDINGINOPXML"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROP = dtbValue.Rows[0]["DIAGNOSISAFTEROP"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROPXML = dtbValue.Rows[0]["DIAGNOSISAFTEROPXML"].ToString();
                    objRecordContent.m_strANAMODE = dtbValue.Rows[0]["ANAMODE"].ToString();
                    objRecordContent.m_strANAMODEXML = dtbValue.Rows[0]["ANAMODEXML"].ToString();
                    objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
                    objRecordContent.m_strREGISTERID_CHR = dtbValue.Rows[0]["REGISTERID_CHR"].ToString();
                    objRecordContent.m_strPULLTIME = dtbValue.Rows[0]["PULLTIME"].ToString();
                    objRecordContent.m_strPULLTIMEXML = dtbValue.Rows[0]["PULLTIMEXML"].ToString();

                    objRecordContent.m_strOPINDICATION_RIGHT = dtbValue.Rows[0]["OPINDICATION_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSHEIGHT_RIGHT = dtbValue.Rows[0]["UTERUSHEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strABDOMENROUND_RIGHT = dtbValue.Rows[0]["ABDOMENROUND_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION_RIGHT = dtbValue.Rows[0]["PRESENTATION_RIGHT"].ToString();
                    objRecordContent.m_strFETUSWEIGHT_RIGHT = dtbValue.Rows[0]["FETUSWEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strDC_RIGHT = dtbValue.Rows[0]["DC_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSORA_RIGHT = dtbValue.Rows[0]["UTERUSORA_RIGHT"].ToString();
                    objRecordContent.m_strFETUSPLACE_RIGHT = dtbValue.Rows[0]["FETUSPLACE_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHT_RIGHT = dtbValue.Rows[0]["PRESENTATIONHEITHT_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT = dtbValue.Rows[0]["CAPUTSUCCEDANEUMSIZE_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE_RIGHT = dtbValue.Rows[0]["CAPUTSUCCEDANEUMPLACE_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSORAOPEN_RIGHT = dtbValue.Rows[0]["UTERUSORAOPEN_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONPLACE_RIGHT = dtbValue.Rows[0]["PRESENTATIONPLACE_RIGHT"].ToString();
                    objRecordContent.m_strLATERALINCISORANA_RIGHT = dtbValue.Rows[0]["LATERALINCISORANA_RIGHT"].ToString();
                    objRecordContent.m_strMINUSPRESS_RIGHT = dtbValue.Rows[0]["MINUSPRESS_RIGHT"].ToString();
                    objRecordContent.m_strAPGAR1_RIGHT = dtbValue.Rows[0]["APGAR1_RIGHT"].ToString();
                    objRecordContent.m_strAPGAR2_RIGHT = dtbValue.Rows[0]["APGAR2_RIGHT"].ToString();
                    objRecordContent.m_strAFTERCHILDBEARING_RIGHT = dtbValue.Rows[0]["AFTERCHILDBEARING_RIGHT"].ToString();
                    objRecordContent.m_strBLEEDINGINOP_RIGHT = dtbValue.Rows[0]["BLEEDINGINOP_RIGHT"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROP_RIGHT = dtbValue.Rows[0]["DIAGNOSISAFTEROP_RIGHT"].ToString();
                    objRecordContent.m_strANAMODE_RIGHT = dtbValue.Rows[0]["ANAMODE_RIGHT"].ToString();
                    objRecordContent.m_strPULLTIME_RIGHT = dtbValue.Rows[0]["PULLTIME_RIGHT"].ToString();

                    //��ȡǩ������
                    if (dtbValue.Rows[0]["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
                    }                   

                    p_objRecordContent = objRecordContent;
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
    }
}
