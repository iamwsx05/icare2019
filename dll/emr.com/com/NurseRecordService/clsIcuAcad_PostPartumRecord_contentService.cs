using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService ;

namespace com.digitalwave.clsRecordsService
{
    /// <summary>
    /// �����¼(��ӣ��޸�)
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsIcuAcad_PostPartumRecord_contentService : clsDiseaseTrackService
    {
        #region SQL���
        /// <summary>
        /// ��ICUACAD_POSTPARTUMSEERECORD��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
        /// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
        /// </summary>
        private const string c_strGetTimeListSQL = @"select t.createdate_dat, t.recorddate_dat
  from t_emr_postpartum_all t
 Where t.registerid_chr = ?
   and t.status_int = 1";

        /// <summary>
        /// ��ICUACAD_POSTPARTUMSEERECORD�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
        /// InPatientID ,InPatientDate ,CreateDate,Status = 0
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select t.createdate_dat, t.createuserid_chr
  from t_emr_postpartum_all t
 Where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

        /// <summary>
        /// ��ICUACAD_POSTPARTUMSEERECORD��ȡɾ��������Ҫ��Ϣ��
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.DEACTIVEDOPERATORID_CHR
  from t_emr_postpartum_all t
 Where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 0";

        /// <summary>
        /// ��Ӽ�¼��ICUACAD_POSTPARTUMSEERECORD
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_postpartum_all
  (postportum_num_chr,
   uterusbottom_chr,
   uteruspinch_chr,
   milknum_chr,
   breastbulge_chr,
   nipple_chr,
   dewnum_chr,
   dewcolor_chr,
   dewfuck_chr,
   perineum_chr,
   bp_chr,
   urine_chr,
   annotations_chr,
   postportum_num_chrxml,
   uterusbottom_chrxml,
   uteruspinch_chrxml,
   milknum_chrxml,
   breastbulge_chrxml,
   nipple_chrxml,
   dewnum_chrxml,
   dewcolor_chrxml,
   dewfuck_chrxml,
   perineum_chrxml,
   bp_chrxml,
   urine_chrxml,
   annotations_chrxml,
   registerid_chr,
   createdate_dat,
   createuserid_chr,
   ifconfirm_int,
   status_int,
   recorduserid_vchr,
   recorddate_dat,
   sequence_int)
values
  (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
   ?,?,?,?,?,?,?,?,?,?,?,?,?,0,1,?,?,?)";//32������

        /// <summary>
        /// ��Ӽ�¼��ICUACAD_POSTPARTUMSEECONTENT
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_postpartum_right
  (modifydate,
   modifyuserid,
   postportum_num_chr_right,
   uterusbottom_chr_right,
   uteruspinch_chr_right,
   milknum_chr_right,
   breastbulge_chr_right,
   nipple_chr_right,
   dewnum_chr_right,
   dewcolor_chr_right,
   dewfuck_chr_right,
   perineum_chr_right,
   bp_chr_right,
   urine_chr_right,
   annotations_chr_right,
   registerid_chr,
   createdate_dat,
   status_int)
values
  (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,1)";//17������

        /// <summary>
        /// �޸ļ�¼��ICUACAD_POSTPARTUMSEERECORD
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_postpartum_all
   set postportum_num_chr = ?,
       uterusbottom_chr = ?,
       uteruspinch_chr = ?,
       milknum_chr = ?,
       breastbulge_chr = ?,
       nipple_chr = ?,
       dewnum_chr = ?,
       dewcolor_chr = ?,
       dewfuck_chr = ?,
       perineum_chr = ?,
       bp_chr = ?,
       urine_chr = ?,
       annotations_chr = ?,
       postportum_num_chrxml = ?,
       uterusbottom_chrxml = ?,
       uteruspinch_chrxml = ?,
       milknum_chrxml = ?,
       breastbulge_chrxml = ?,
       nipple_chrxml = ?,
       dewnum_chrxml = ?,
       dewcolor_chrxml = ?,
       dewfuck_chrxml = ?,
       perineum_chrxml = ?,
       bp_chrxml = ?,
       urine_chrxml = ?,
       annotations_chrxml = ?,
       recorduserid_vchr = ?,
       recorddate_dat = ?,
       sequence_int = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//31������

        /// <summary>
        /// �޸ļ�¼��ICUACAD_POSTPARTUMSEERECORD
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// ����ICUACAD_POSTPARTUMSEERECORD��ɾ����¼����Ϣ
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_postpartum_all t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and Status_int = 1";

        /// <summary>
        /// ����ICUACAD_POSTPARTUMSEERECORD��FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update t_emr_postpartum_all t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";


        /// <summary>
        /// ��ICUACAD_POSTPARTUMSEERECORD��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
        /// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select t.createdate_dat, recorddate_dat
  from t_emr_postpartum_all t
 Where t.registerid_chr = ?
   and t.deactivedoperatorid_chr = ?
   and t.status_int = 0";

        /// <summary>
        /// ��ICUACAD_POSTPARTUMSEERECORD��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
        /// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select t.createdate_dat,recorddate_dat
  from t_emr_postpartum_all t
 where t.registerid_chr = ?
   and t.status_int = 0";



        #endregion
        #region ��ȡ���˵ĸü�¼ʱ���б�
        /// <summary>
        /// ����סԺ�ǼǺŻ�ȡ���˵ĸü�¼ʱ���б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreateDateArr">����ʱ������</param>
        /// <param name="p_strRecordDateArr">�����¼ʱ������</param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetRecordTimeList(
            string p_strRegisterId, out string[] p_strCreateDateArr, out string[] p_strRecordDateArr)
        {
            p_strCreateDateArr = null;
            p_strRecordDateArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //������
            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //���ý��
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreateDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
                //objHRPServ.Dispose();

            }
            return lngRes;
        }
        #endregion

        #region �������ݿ��е��״δ�ӡʱ��
        /// <summary>
        ///  �������ݿ��е��״δ�ӡʱ�䡣
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreatedDate">����Ϊ�գ����ڸ������д�ӡʱ��</param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
            string p_strCreatedDate,
            DateTime p_dtmFirstPrintDate)
        {
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                    return (long)enmOperationResult.Parameter_Error;

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterId;
                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

                string strSql = @"update t_emr_postpartumannoall t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterId;
                //ִ��SQL
                lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵��Ѿ���ĳ�û�ɾ����¼ʱ���б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strDeleteUserID">ɾ��������ID</param>
        /// <param name="p_strRecordTimeArr">�û���д�ļ�¼ʱ������</param>
        /// <param name="p_strCreatedDateArr">ϵͳ���ɵļ�¼ʱ������</param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeList(
            string p_strRegisterId,
            string p_strDeleteUserID,
            out string[] p_strRecordTimeArr,
            out string[] p_strCreatedDateArr)
        {
            p_strRecordTimeArr = null;
            p_strCreatedDateArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetDeleteRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //������
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strDeleteUserID))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].Value = p_strDeleteUserID;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strRecordTimeArr = new string[dtbValue.Rows.Count];
                    p_strCreatedDateArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //���ý��
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordTimeArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreatedDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreateDateArr">����ʱ��</param>
        /// <param name="p_strRecordTimeArr">����ļ�¼ʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeListAll(
            string p_strRegisterId,
            out string[] p_strCreateDateArr,
            out string[] p_strRecordTimeArr)
        {
            p_strCreateDateArr = null;
            p_strRecordTimeArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetDeleteRecordTimeListAll");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //������
            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strRecordTimeArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //���ý��
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        p_strCreateDateArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        p_strRecordTimeArr[i] = dtmTemp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region ��ȡָ����¼������
        /// <summary>
        /// ����סԺ�ǼǺŻ�ȡָ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCteatedDate">����ʱ��</param>
        /// <param name="p_objRecordContent">���صļ�¼����</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strRegisterId, string p_strCteatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            //������
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCteatedDate))
                return (long)enmOperationResult.Parameter_Error;
            //,t2.MODIFYDATE as MODIFYDATE,t2.MODIFYUSERID as MODIFYUSERID
            string strGetRecordContentSQL = @"select a.postportum_num_chr,
       a.uterusbottom_chr,
       a.uteruspinch_chr,
       a.milknum_chr,
       a.breastbulge_chr,
       a.nipple_chr,
       a.dewnum_chr,
       a.dewcolor_chr,
       a.dewfuck_chr,
       a.perineum_chr,
       a.bp_chr,
       a.urine_chr,
       a.annotations_chr,
       a.postportum_num_chrxml,
       a.uterusbottom_chrxml,
       a.uteruspinch_chrxml,
       a.milknum_chrxml,
       a.breastbulge_chrxml,
       a.nipple_chrxml,
       a.dewnum_chrxml,
       a.dewcolor_chrxml,
       a.dewfuck_chrxml,
       a.perineum_chrxml,
       a.bp_chrxml,
       a.urine_chrxml,
       a.annotations_chrxml,
       a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_int,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.firstprintdate_dat,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.sequence_int,
       b.modifydate,
       b.modifyuserid,
       b.postportum_num_chr_right,
       b.uterusbottom_chr_right,
       b.uteruspinch_chr_right,
       b.milknum_chr_right,
       b.breastbulge_chr_right,
       b.nipple_chr_right,
       b.dewnum_chr_right,
       b.dewcolor_chr_right,
       b.dewfuck_chr_right,
       b.perineum_chr_right,
       b.bp_chr_right,
       b.urine_chr_right,
       b.annotations_chr_right,
       b.status_int
  from t_emr_postpartum_all a
 inner join t_emr_postpartum_right b on a.registerid_chr = b.registerid_chr
                                    and a.createdate_dat = b.createdate_dat
 where a.status_int = 1
   and b.status_int = 1
   and a.registerid_chr = ?
   and a.createdate_dat = ?";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCteatedDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region ���ý��
                    clsIcuAcad_PostPartumRecord_Value objRecordContent = new clsIcuAcad_PostPartumRecord_Value();
                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtbValue.Rows[0]["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strANNOTATIONS_CHR = dtbValue.Rows[0]["ANNOTATIONS_CHR"].ToString();
                    objRecordContent.m_strANNOTATIONS_CHR_RIGHT = dtbValue.Rows[0]["ANNOTATIONS_CHR_RIGHT"].ToString();
                    objRecordContent.m_strANNOTATIONS_CHRXML = dtbValue.Rows[0]["ANNOTATIONS_CHRXML"].ToString();

                    objRecordContent.m_strBP_CHR = dtbValue.Rows[0]["BP_CHR"].ToString();
                    objRecordContent.m_strBP_CHR_RIGHT = dtbValue.Rows[0]["BP_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBP_CHRXML = dtbValue.Rows[0]["BP_CHRXML"].ToString();

                    objRecordContent.m_strBREASTBULGE_CHR = dtbValue.Rows[0]["BREASTBULGE_CHR"].ToString();
                    objRecordContent.m_strBREASTBULGE_CHR_RIGHT = dtbValue.Rows[0]["BREASTBULGE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBREASTBULGE_CHRXML = dtbValue.Rows[0]["BREASTBULGE_CHRXML"].ToString();

                    objRecordContent.m_strDEWCOLOR_CHR = dtbValue.Rows[0]["DEWCOLOR_CHR"].ToString();
                    objRecordContent.m_strDEWCOLOR_CHR_RIGHT = dtbValue.Rows[0]["DEWCOLOR_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWCOLOR_CHRXML = dtbValue.Rows[0]["DEWCOLOR_CHRXML"].ToString();

                    objRecordContent.m_strDEWFUCK_CHR = dtbValue.Rows[0]["DEWFUCK_CHR"].ToString();
                    objRecordContent.m_strDEWFUCK_CHR_RIGHT = dtbValue.Rows[0]["DEWFUCK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWFUCK_CHRXML = dtbValue.Rows[0]["DEWFUCK_CHRXML"].ToString();

                    objRecordContent.m_strDEWNUM_CHR = dtbValue.Rows[0]["DEWNUM_CHR"].ToString();
                    objRecordContent.m_strDEWNUM_CHR_RIGHT = dtbValue.Rows[0]["DEWNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWNUM_CHRXML = dtbValue.Rows[0]["DEWNUM_CHRXML"].ToString();

                    objRecordContent.m_strMILKNUM_CHR = dtbValue.Rows[0]["MILKNUM_CHR"].ToString();
                    objRecordContent.m_strMILKNUM_CHR_RIGHT = dtbValue.Rows[0]["MILKNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMILKNUM_CHRXML = dtbValue.Rows[0]["MILKNUM_CHRXML"].ToString();

                    objRecordContent.m_strNIPPLE_CHR = dtbValue.Rows[0]["NIPPLE_CHR"].ToString();
                    objRecordContent.m_strNIPPLE_CHR_RIGHT = dtbValue.Rows[0]["NIPPLE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strNIPPLE_CHRXML = dtbValue.Rows[0]["NIPPLE_CHRXML"].ToString();

                    objRecordContent.m_strPERINEUM_CHR = dtbValue.Rows[0]["PERINEUM_CHR"].ToString();
                    objRecordContent.m_strPERINEUM_CHR_RIGHT = dtbValue.Rows[0]["PERINEUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPERINEUM_CHRXML = dtbValue.Rows[0]["PERINEUM_CHRXML"].ToString();

                    objRecordContent.m_strPOSTPORTUM_NUM_CHR = dtbValue.Rows[0]["POSTPORTUM_NUM_CHR"].ToString();
                    objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT = dtbValue.Rows[0]["POSTPORTUM_NUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPOSTPORTUM_NUM_CHRXML = dtbValue.Rows[0]["POSTPORTUM_NUM_CHRXML"].ToString();

                    objRecordContent.m_strURINE_CHR = dtbValue.Rows[0]["URINE_CHR"].ToString();
                    objRecordContent.m_strURINE_CHR_RIGHT = dtbValue.Rows[0]["URINE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strURINE_CHRXML = dtbValue.Rows[0]["URINE_CHRXML"].ToString();

                    objRecordContent.m_strUTERUSBOTTOM_CHR = dtbValue.Rows[0]["UTERUSBOTTOM_CHR"].ToString();
                    objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT = dtbValue.Rows[0]["UTERUSBOTTOM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSBOTTOM_CHRXML = dtbValue.Rows[0]["UTERUSBOTTOM_CHRXML"].ToString();

                    objRecordContent.m_strUTERUSPINCH_CHR = dtbValue.Rows[0]["UTERUSPINCH_CHR"].ToString();
                    objRecordContent.m_strUTERUSPINCH_CHR_RIGHT = dtbValue.Rows[0]["UTERUSPINCH_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSPINCH_CHRXML = dtbValue.Rows[0]["UTERUSPINCH_CHRXML"].ToString();

                    //��ȡǩ������
                    long lngS = 0;
                    if (long.TryParse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
                    }
                    #endregion 
                    p_objRecordContent = objRecordContent;
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

            //������
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
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
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //������                              
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            clsIcuAcad_PostPartumRecord_Value objRecordContent = (clsIcuAcad_PostPartumRecord_Value)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region ��ֵ
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(32, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHR;
                objDPArr[1].Value = objRecordContent.m_strUTERUSBOTTOM_CHR;
                objDPArr[2].Value = objRecordContent.m_strUTERUSPINCH_CHR;
                objDPArr[3].Value = objRecordContent.m_strMILKNUM_CHR;
                objDPArr[4].Value = objRecordContent.m_strBREASTBULGE_CHR;
                objDPArr[5].Value = objRecordContent.m_strNIPPLE_CHR;
                objDPArr[6].Value = objRecordContent.m_strDEWNUM_CHR;
                objDPArr[7].Value = objRecordContent.m_strDEWCOLOR_CHR;
                objDPArr[8].Value = objRecordContent.m_strDEWFUCK_CHR;
                objDPArr[9].Value = objRecordContent.m_strPERINEUM_CHR;
                objDPArr[10].Value = objRecordContent.m_strBP_CHR;
                objDPArr[11].Value = objRecordContent.m_strURINE_CHR;
                objDPArr[12].Value = objRecordContent.m_strANNOTATIONS_CHR;

                objDPArr[13].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHRXML;
                objDPArr[14].Value = objRecordContent.m_strUTERUSBOTTOM_CHRXML;
                objDPArr[15].Value = objRecordContent.m_strUTERUSPINCH_CHRXML;
                objDPArr[16].Value = objRecordContent.m_strMILKNUM_CHRXML;
                objDPArr[17].Value = objRecordContent.m_strBREASTBULGE_CHRXML;
                objDPArr[18].Value = objRecordContent.m_strNIPPLE_CHRXML;
                objDPArr[19].Value = objRecordContent.m_strDEWNUM_CHRXML;
                objDPArr[20].Value = objRecordContent.m_strDEWCOLOR_CHRXML;
                objDPArr[21].Value = objRecordContent.m_strDEWFUCK_CHRXML;
                objDPArr[22].Value = objRecordContent.m_strPERINEUM_CHRXML;
                objDPArr[23].Value = objRecordContent.m_strBP_CHRXML;
                objDPArr[24].Value = objRecordContent.m_strURINE_CHRXML;
                objDPArr[25].Value = objRecordContent.m_strANNOTATIONS_CHRXML;

                objDPArr[26].Value = objRecordContent.m_strRegisterID;
                objDPArr[27].DbType = DbType.Date;
                objDPArr[27].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[28].Value = objRecordContent.m_strCreateUserID;
                objDPArr[29].Value = objRecordContent.m_strRecordUserID;
                objDPArr[30].DbType = DbType.Date;
                objDPArr[30].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[31].Value = lngSequence;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(17, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[1].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[2].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT;
                objDPArr2[3].Value = objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT;
                objDPArr2[4].Value = objRecordContent.m_strUTERUSPINCH_CHR_RIGHT;
                objDPArr2[5].Value = objRecordContent.m_strMILKNUM_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strBREASTBULGE_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strNIPPLE_CHR_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strDEWNUM_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strDEWCOLOR_CHR_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strDEWFUCK_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strPERINEUM_CHR_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strBP_CHR_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strURINE_CHR_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strANNOTATIONS_CHR_RIGHT;

                objDPArr2[15].Value = objRecordContent.m_strRegisterID;
                objDPArr2[16].DbType = DbType.Date;
                objDPArr2[16].Value = objRecordContent.m_dtmCreateDate;
                #endregion
                //ִ��SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);

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
            //������
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsIcuAcad_PostPartumRecord_Value objRecordContent = (clsIcuAcad_PostPartumRecord_Value)p_objRecordContent;
            /// <summary>
            /// ��IntensiveTendRecordContent1��ȡָ����������޸�ʱ�䡣
            /// </summary>
            string strCheckLastModifyRecordSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_postpartum_right t2
 where t2.registerid_chr = ?
   and t2.createdate_dat = ?
   and t2.status_int = 1";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����			
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = objRecordContent.m_strRegisterID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                    dtbValue = new DataTable();
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID_CHR"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE_DAT"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //��DataTable�л�ȡModifyDate��ʹ֮��objRecordContent.m_dtmModifyDate�Ƚ�
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //�����ͬ������DB_Succees
                    if (objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
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

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
        }
        #endregion

        #region �����޸ĵ����ݱ��浽���ݿ⡣��������,����ӱ�.
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
            //������
            if (p_objRecordContent == null || p_objRecordContent.m_dtmCreateDate == DateTime.MinValue || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsIcuAcad_PostPartumRecord_Value objRecordContent = (clsIcuAcad_PostPartumRecord_Value)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region set value
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(31, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHR;
                objDPArr[1].Value = objRecordContent.m_strUTERUSBOTTOM_CHR;
                objDPArr[2].Value = objRecordContent.m_strUTERUSPINCH_CHR;
                objDPArr[3].Value = objRecordContent.m_strMILKNUM_CHR;
                objDPArr[4].Value = objRecordContent.m_strBREASTBULGE_CHR;
                objDPArr[5].Value = objRecordContent.m_strNIPPLE_CHR;
                objDPArr[6].Value = objRecordContent.m_strDEWNUM_CHR;
                objDPArr[7].Value = objRecordContent.m_strDEWCOLOR_CHR;
                objDPArr[8].Value = objRecordContent.m_strDEWFUCK_CHR;
                objDPArr[9].Value = objRecordContent.m_strPERINEUM_CHR;
                objDPArr[10].Value = objRecordContent.m_strBP_CHR;
                objDPArr[11].Value = objRecordContent.m_strURINE_CHR;
                objDPArr[12].Value = objRecordContent.m_strANNOTATIONS_CHR;

                objDPArr[13].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHRXML;
                objDPArr[14].Value = objRecordContent.m_strUTERUSBOTTOM_CHRXML;
                objDPArr[15].Value = objRecordContent.m_strUTERUSPINCH_CHRXML;
                objDPArr[16].Value = objRecordContent.m_strMILKNUM_CHRXML;
                objDPArr[17].Value = objRecordContent.m_strBREASTBULGE_CHRXML;
                objDPArr[18].Value = objRecordContent.m_strNIPPLE_CHRXML;
                objDPArr[19].Value = objRecordContent.m_strDEWNUM_CHRXML;
                objDPArr[20].Value = objRecordContent.m_strDEWCOLOR_CHRXML;
                objDPArr[21].Value = objRecordContent.m_strDEWFUCK_CHRXML;
                objDPArr[22].Value = objRecordContent.m_strPERINEUM_CHRXML;
                objDPArr[23].Value = objRecordContent.m_strBP_CHRXML;
                objDPArr[24].Value = objRecordContent.m_strURINE_CHRXML;
                objDPArr[25].Value = objRecordContent.m_strANNOTATIONS_CHRXML;

                objDPArr[26].Value = p_objRecordContent.m_strRecordUserID;
                objDPArr[27].DbType = DbType.Date;
                objDPArr[27].Value = p_objRecordContent.m_dtmRecordDate;
                objDPArr[28].Value = lngSequence;
                objDPArr[29].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[30].DbType = DbType.Date;
                objDPArr[30].Value = p_objRecordContent.m_dtmCreateDate;

                #endregion
                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);

                #region set value

                lngRes = m_lngDeleteContentInfo(objRecordContent.m_strRegisterID, objRecordContent.m_dtmCreateDate);

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(17, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[1].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[2].Value = objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT;
                objDPArr2[3].Value = objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT;
                objDPArr2[4].Value = objRecordContent.m_strUTERUSPINCH_CHR_RIGHT;
                objDPArr2[5].Value = objRecordContent.m_strMILKNUM_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strBREASTBULGE_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strNIPPLE_CHR_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strDEWNUM_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strDEWCOLOR_CHR_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strDEWFUCK_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strPERINEUM_CHR_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strBP_CHR_RIGHT;
                objDPArr2[13].Value = objRecordContent.m_strURINE_CHR_RIGHT;
                objDPArr2[14].Value = objRecordContent.m_strANNOTATIONS_CHR_RIGHT;

                objDPArr2[15].Value = objRecordContent.m_strRegisterID;
                objDPArr2[16].DbType = DbType.Date;
                objDPArr2[16].Value = objRecordContent.m_dtmCreateDate;
                #endregion
                //ִ��SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
        }
        #endregion

        #region �Ѽ�¼�������С�ɾ������
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <returns></returns>
        [AutoComplete]
        long m_lngDeleteContentInfo(string p_strRegisterId, DateTime p_dtmCreatedDate)
        {
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmCreatedDate == DateTime.MaxValue)
                return -1;
            string strSql = @" update t_emr_postpartum_right t set t.status_int = 0
 where t.registerid_chr = ? and t.createdate_dat = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmCreatedDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
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
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;

            clsIcuACAD_PostPartumseeRecord_VO objRecordContent = (clsIcuACAD_PostPartumseeRecord_VO)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmCreateDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

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

            } return lngRes;
        }
        #endregion

        #region  ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        /// <summary>
        /// ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="p_dtmModifyDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strRegisterId, string p_strCreatedDate,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.Now;
            p_strFirstPrintDate = null;
            //������
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                return (long)enmOperationResult.Parameter_Error;
            /// <summary>
            /// ��ICUACAD_POSTPARTUMSEERECORD��ICUACAD_POSTPARTUMSEECONTENT��ȡLastModifyDate��FirstPrintDate
            /// </summary>
            string strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate_dat, b.modifydate
  from t_emr_postpartum_all a
 inner join t_emr_postpartum_right b on a.registerid_chr =
                                              b.registerid_chr
                                          and a.createdate_dat =
                                              b.createdate_dat
 where a.registerid_chr = ?
   and a.createdate_dat = ?
   and a.status_int = 1
   and b.status_int = 1";


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    p_strFirstPrintDate = dtbValue.Rows[0]["firstprintdate_dat"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["modifydate"].ToString());
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
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion

        #region ��ȡָ���Ѿ���ɾ����¼������(������ʾ��DG�����)
        /// <summary>
        /// ��ȡָ���Ѿ���ɾ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strRegisterId, string p_strCreatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            //������
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = @"select t1.postportum_num_chr,
       t1.uterusbottom_chr,
       t1.uteruspinch_chr,
       t1.milknum_chr,
       t1.breastbulge_chr,
       t1.nipple_chr,
       t1.dewnum_chr,
       t1.dewcolor_chr,
       t1.dewfuck_chr,
       t1.perineum_chr,
       t1.bp_chr,
       t1.urine_chr,
       t1.annotations_chr,
       t1.postportum_num_chrxml,
       t1.uterusbottom_chrxml,
       t1.uteruspinch_chrxml,
       t1.milknum_chrxml,
       t1.breastbulge_chrxml,
       t1.nipple_chrxml,
       t1.dewnum_chrxml,
       t1.dewcolor_chrxml,
       t1.dewfuck_chrxml,
       t1.perineum_chrxml,
       t1.bp_chrxml,
       t1.urine_chrxml,
       t1.annotations_chrxml,
       t1.registerid_chr,
       t1.createdate_dat,
       t1.createuserid_chr,
       t1.ifconfirm_int,
       t1.status_int,
       t1.deactiveddate_dat,
       t1.deactivedoperatorid_chr,
       t1.firstprintdate_dat,
       t1.recorduserid_vchr,
       t1.recorddate_dat,
       t1.sequence_int,
       t2.modifydate as modifydate,
       t2.modifyuserid as modifyuserid
  from t_emr_postpartum_all t1
 inner join t_emr_postpartum_right t2 on t1.registerid_chr =
                                         t2.registerid_chr
                                     and t1.createdate_dat =
                                         t2.createdate_dat
 where t1.status_int = 0
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?
   and t2.modifydate =
       (select max(modifydate)
          from icuacad_postpartumseecontent
         where registerid_chr = t1.registerid_chr
           and createdate_dat = t1.createdate_dat)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region ���ý��
                    clsIcuAcad_PostPartumRecord_Value objRecordContent = new clsIcuAcad_PostPartumRecord_Value();
                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtbValue.Rows[0]["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strANNOTATIONS_CHR = dtbValue.Rows[0]["ANNOTATIONS_CHR"].ToString();
                    objRecordContent.m_strANNOTATIONS_CHR_RIGHT = dtbValue.Rows[0]["ANNOTATIONS_CHR_RIGHT"].ToString();
                    objRecordContent.m_strANNOTATIONS_CHRXML = dtbValue.Rows[0]["ANNOTATIONS_CHRXML"].ToString();

                    objRecordContent.m_strBP_CHR = dtbValue.Rows[0]["BP_CHR"].ToString();
                    objRecordContent.m_strBP_CHR_RIGHT = dtbValue.Rows[0]["BP_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBP_CHRXML = dtbValue.Rows[0]["BP_CHRXML"].ToString();

                    objRecordContent.m_strBREASTBULGE_CHR = dtbValue.Rows[0]["BREASTBULGE_CHR"].ToString();
                    objRecordContent.m_strBREASTBULGE_CHR_RIGHT = dtbValue.Rows[0]["BREASTBULGE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBREASTBULGE_CHRXML = dtbValue.Rows[0]["BREASTBULGE_CHRXML"].ToString();

                    objRecordContent.m_strDEWCOLOR_CHR = dtbValue.Rows[0]["DEWCOLOR_CHR"].ToString();
                    objRecordContent.m_strDEWCOLOR_CHR_RIGHT = dtbValue.Rows[0]["DEWCOLOR_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWCOLOR_CHRXML = dtbValue.Rows[0]["DEWCOLOR_CHRXML"].ToString();

                    objRecordContent.m_strDEWFUCK_CHR = dtbValue.Rows[0]["DEWFUCK_CHR"].ToString();
                    objRecordContent.m_strDEWFUCK_CHR_RIGHT = dtbValue.Rows[0]["DEWFUCK_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWFUCK_CHRXML = dtbValue.Rows[0]["DEWFUCK_CHRXML"].ToString();

                    objRecordContent.m_strDEWNUM_CHR = dtbValue.Rows[0]["DEWNUM_CHR"].ToString();
                    objRecordContent.m_strDEWNUM_CHR_RIGHT = dtbValue.Rows[0]["DEWNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strDEWNUM_CHRXML = dtbValue.Rows[0]["DEWNUM_CHRXML"].ToString();

                    objRecordContent.m_strMILKNUM_CHR = dtbValue.Rows[0]["MILKNUM_CHR"].ToString();
                    objRecordContent.m_strMILKNUM_CHR_RIGHT = dtbValue.Rows[0]["MILKNUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strMILKNUM_CHRXML = dtbValue.Rows[0]["MILKNUM_CHRXML"].ToString();

                    objRecordContent.m_strNIPPLE_CHR = dtbValue.Rows[0]["NIPPLE_CHR"].ToString();
                    objRecordContent.m_strNIPPLE_CHR_RIGHT = dtbValue.Rows[0]["NIPPLE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strNIPPLE_CHRXML = dtbValue.Rows[0]["NIPPLE_CHRXML"].ToString();

                    objRecordContent.m_strPERINEUM_CHR = dtbValue.Rows[0]["PERINEUM_CHR"].ToString();
                    objRecordContent.m_strPERINEUM_CHR_RIGHT = dtbValue.Rows[0]["PERINEUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPERINEUM_CHRXML = dtbValue.Rows[0]["PERINEUM_CHRXML"].ToString();

                    objRecordContent.m_strPOSTPORTUM_NUM_CHR = dtbValue.Rows[0]["POSTPORTUM_NUM_CHR"].ToString();
                    objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT = dtbValue.Rows[0]["POSTPORTUM_NUM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPOSTPORTUM_NUM_CHRXML = dtbValue.Rows[0]["POSTPORTUM_NUM_CHRXML"].ToString();

                    objRecordContent.m_strURINE_CHR = dtbValue.Rows[0]["URINE_CHR"].ToString();
                    objRecordContent.m_strURINE_CHR_RIGHT = dtbValue.Rows[0]["URINE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strURINE_CHRXML = dtbValue.Rows[0]["URINE_CHRXML"].ToString();

                    objRecordContent.m_strUTERUSBOTTOM_CHR = dtbValue.Rows[0]["UTERUSBOTTOM_CHR"].ToString();
                    objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT = dtbValue.Rows[0]["UTERUSBOTTOM_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSBOTTOM_CHRXML = dtbValue.Rows[0]["UTERUSBOTTOM_CHRXML"].ToString();

                    objRecordContent.m_strUTERUSPINCH_CHR = dtbValue.Rows[0]["UTERUSPINCH_CHR"].ToString();
                    objRecordContent.m_strUTERUSPINCH_CHR_RIGHT = dtbValue.Rows[0]["UTERUSPINCH_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSPINCH_CHRXML = dtbValue.Rows[0]["UTERUSPINCH_CHRXML"].ToString();

                    //��ȡǩ������
                    long lngS = 0;
                    if (long.TryParse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
                    }
                    p_objRecordContent = objRecordContent;
                    #endregion
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
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion
    }
}
