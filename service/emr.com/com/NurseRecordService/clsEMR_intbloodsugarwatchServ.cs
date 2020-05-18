using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// �ڷ��ڿ�Ѫ�ǹ۲��
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
 public   class clsEMR_intbloodsugarwatchServ : clsDiseaseTrackService
    {
        #region SQL���
        /// <summary>
        /// ��T_EMR_intbloodsugarwatch��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣

        /// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDateʱ��
        /// </summary>
        private const string c_strGetTimeListSQL = @"select t.createdate_dat, t.recorddate_dat
  from t_emr_intbloodsugarwatch t
 where t.registerid_chr = ?
   and t.status_int = 1";

        /// <summary>
        /// ��T_EMR_intbloodsugarwatch�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ

        /// InPatientID ,InPatientDate ,CreateDate,Status = 1
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select t.createdate_dat, t.createuserid_chr
  from t_emr_intbloodsugarwatch t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

        /// <summary>
        /// ��t_emr_intbloodsugarwatch��ȡɾ��������Ҫ��Ϣ��

        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_chr
  from t_emr_intbloodsugarwatch t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 0";

        /// <summary>
        /// ��Ӽ�¼��t_emr_intbloodsugarwatch
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_intbloodsugarwatch 
(registerid_chr,createdate_dat,createuserid_chr,ifconfirm_int,status_int,recorduserid_vchr,recorddate_dat,sequence_int,
markstatus,nullabdomen_vchr, nullabdomen_xml, twobreakfast_vchr, twobreakfast_xml,beforelunch_vchr,
beforelunch_xml, twoafterlunch_vchr,twoafterlunch_xml, beforedinner_vchr,
 beforedinner_xml,twoafterdinner_vchr,twoafterdinner_xml,beforesleep_vchr,     
    beforesleep_xml,beizhu_vchr, beizhu_xml)              
                
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// ��Ӽ�¼��T_EMR_MICROBOOLDSUGARCHECKCON
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into T_EMR_intbloodsugarwatchcon 
(registerid_chr,createdate_dat,status_int,modifydate,modifyuserid, nullabdomen_right,twobreakfast_rigth,beforelunch_right,
 twoafterlunch_right,
 beforedinner_right,   
  twoafterdinner_right, 
  beforesleep_right,    
  beizhu_right) 
values (?,?,?,?,?,?,?,?,?,?,?,?,?)";//7������


        /// <summary>
        /// �޸ļ�¼��T_EMR_MICROBOOLDSUGARCHECK
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_intbloodsugarwatch
   set recorduserid_vchr = ?,recorddate_dat = ?,sequence_int = ?,markstatus = ?,
nullabdomen_vchr = ?, nullabdomen_xml = ?, twobreakfast_vchr = ?, twobreakfast_xml = ?,beforelunch_vchr = ?,
beforelunch_xml = ?, twoafterlunch_vchr = ?,twoafterlunch_xml = ?, beforedinner_vchr = ?,
 beforedinner_xml = ?,twoafterdinner_vchr = ?,twoafterdinner_xml = ?,beforesleep_vchr = ?,     
    beforesleep_xml = ?,beizhu_vchr = ?, beizhu_xml = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//


        /// <summary>
        /// �޸ļ�¼��T_EMR_MICROBOOLDSUGARCHECKCON
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// ����T_EMR_MICROBOOLDSUGARCHECK��ɾ����¼����Ϣ
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_intbloodsugarwatch t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";

        /// <summary>
        /// ����T_EMR_MICROBOOLDSUGARCHECK��FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update t_emr_intbloodsugarwatch t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";


        /// <summary>
        /// ��T_EMR_MICROBOOLDSUGARCHECK��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣

        /// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDate��OpenDateʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select t.createdate_dat, recorddate_dat
  from t_emr_intbloodsugarwatch t
 where t.registerid_chr = ?
   and t.deactivedoperatorid_chr = ?
   and t.status_int = 0";

        /// <summary>
        /// ��T_EMR_MICROBOOLDSUGARCHECK��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣

        /// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDate��OpenDateʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select t.createdate_dat,recorddate_dat
  from t_emr_intbloodsugarwatch t
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
        public override long m_lngGetRecordTimeList(
            string p_strRegisterId, out string[] p_strCreateDateArr, out string[] p_strRecordDateArr)
        {
            p_strCreateDateArr = null;
            p_strRecordDateArr = null;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngUpdateFirstPrintDate");
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
                    p_strRecordDateArr = new string[dtbValue.Rows.Count];
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
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

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
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
        public override long m_lngGetDeleteRecordTimeList(
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
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
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
        public override long m_lngGetDeleteRecordTimeListAll(
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
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
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

            string strGetRecordContentSQL = @"select a.registerid_chr,
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
       a.markstatus,
         a.nullabdomen_vchr,        
       a.nullabdomen_xml,        
       a.twobreakfast_vchr,      
       a.twobreakfast_xml,    
       a.beforelunch_vchr,        
       a.beforelunch_xml,        
       a.twoafterlunch_vchr,      
       a.twoafterlunch_xml,      
       a.beforedinner_vchr,      
       a.beforedinner_xml,       
       a.twoafterdinner_vchr,     
        a.twoafterdinner_xml,    
       a.beforesleep_vchr,        
       a.beforesleep_xml,         
       a.beizhu_vchr,             
       a.beizhu_xml,              
       b.status_int,
       b.modifydate,
       b.modifyuserid,
   b.nullabdomen_right,    
  b.twobreakfast_rigth,   
  b.beforelunch_right,    
  b.twoafterlunch_right,  
  b.beforedinner_right,   
  b.twoafterdinner_right, 
  b.beforesleep_right,    
  b.beizhu_right
  from T_EMR_intbloodsugarwatch a
 inner join T_EMR_intbloodsugarwatchcon b on a.registerid_chr =
                                               b.registerid_chr
                                           and a.createdate_dat =
                                               b.createdate_dat
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
                    clsEMR_intbloodsugarwatchValue objRecordContent = new clsEMR_intbloodsugarwatchValue();
                    DataRow dtrSelected = dtbValue.Rows[0];

                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtrSelected["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtrSelected["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtrSelected["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtrSelected["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(dtrSelected["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;


                    objRecordContent.m_strNULLABDOMEN_VCHR = dtrSelected["nullabdomen_vchr"].ToString();
                    objRecordContent.m_strNULLABDOMEN_XML = dtrSelected["nullabdomen_xml"].ToString();
                    objRecordContent.m_strTWOBREAKFAST_VCHR = dtrSelected["twobreakfast_vchr"].ToString();
                    objRecordContent.m_strTWOBREAKFAST_XML = dtrSelected["twobreakfast_xml"].ToString();

                    objRecordContent.m_strBEFORELUNCH_VCHR = dtrSelected["beforelunch_vchr"].ToString();
                    objRecordContent.m_strBEFORELUNCH_XML = dtrSelected["beforelunch_xml"].ToString();
                    objRecordContent.m_strTWOAFTERLUNCH_VCHR = dtrSelected["twoafterlunch_vchr"].ToString();
                    objRecordContent.m_strTWOAFTERLUNCH_XML = dtrSelected["twoafterlunch_xml"].ToString();
                    objRecordContent.m_strBEFOREDINNER_VCHR = dtrSelected["beforedinner_vchr"].ToString();
                    objRecordContent.m_strBEFOREDINNER_XML = dtrSelected["beforedinner_xml"].ToString();

                    objRecordContent.m_strTWOAFTERDINNER_VCHR = dtrSelected["twoafterdinner_vchr"].ToString();
                    objRecordContent.m_strTWOAFTERDINNER_XML = dtrSelected["twoafterdinner_xml"].ToString();
                    objRecordContent.m_strBEFORESLEEP_VCHR = dtrSelected["beforesleep_vchr"].ToString();
                    objRecordContent.m_strBEFORESLEEP_XML = dtrSelected["beforesleep_xml"].ToString();
                    objRecordContent.m_strBEIZHU_VCHR = dtrSelected["beizhu_vchr"].ToString();
                    objRecordContent.m_strBEIZHU_XML = dtrSelected["beizhu_xml"].ToString();

                    objRecordContent.m_strNULLABDOMEN_RIGHT = dtrSelected["nullabdomen_right"].ToString();
                    objRecordContent.m_strTWOBREAKFAST_RIGTH = dtrSelected["twobreakfast_rigth"].ToString();
                    objRecordContent.m_strBEFORELUNCH_RIGHT = dtrSelected["beforelunch_right"].ToString();
                    objRecordContent.m_strTWOAFTERLUNCH_RIGHT = dtrSelected["twoafterlunch_right"].ToString();
                    objRecordContent.m_strBEFOREDINNER_RIGHT = dtrSelected["beforedinner_right"].ToString();
                    objRecordContent.m_strTWOAFTERDINNER_RIGHT = dtrSelected["twoafterdinner_right"].ToString();
                    objRecordContent.m_strBEFORESLEEP_RIGHT = dtrSelected["beforesleep_right"].ToString();
                    objRecordContent.m_strBEIZHU_RIGHT = dtrSelected["beizhu_right"].ToString();

                    objRecordContent.m_intMarkStatus = Convert.ToInt32(dtrSelected["MARKSTATUS"]);
                    //��ȡǩ������
                    long lngS = 0;
                    if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        if (lngS != -1)//�Ӿɱ�����������û�е���ǩ����ʡȥ��ѯ�Ĳ���
                        {
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                            //�ͷ�
                            objSign = null;
                        }
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
                objHRPServ = null;
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
                objHRPServ = null;
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
            clsEMR_intbloodsugarwatchValue objRecordContent = (clsEMR_intbloodsugarwatchValue)p_objRecordContent;
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
                objHRPServ.CreateDatabaseParameter(25, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[2].Value = objRecordContent.m_strCreateUserID;
                objDPArr[3].Value = 0;
                objDPArr[4].Value = 1;
                objDPArr[5].Value = objRecordContent.m_strRecordUserID;
                objDPArr[6].DbType = DbType.DateTime;
                objDPArr[6].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[7].Value = lngSequence;
                objDPArr[8].Value = objRecordContent.m_intMarkStatus;
                objDPArr[9].Value = objRecordContent.m_strNULLABDOMEN_VCHR;
                objDPArr[10].Value = objRecordContent.m_strNULLABDOMEN_XML;
                objDPArr[11].Value = objRecordContent.m_strTWOBREAKFAST_VCHR;
                objDPArr[12].Value = objRecordContent.m_strTWOBREAKFAST_XML;
                objDPArr[13].Value = objRecordContent.m_strBEFORELUNCH_VCHR;
                objDPArr[14].Value = objRecordContent.m_strBEFORELUNCH_XML;
                objDPArr[15].Value = objRecordContent.m_strTWOAFTERLUNCH_VCHR;
                objDPArr[16].Value = objRecordContent.m_strTWOAFTERLUNCH_XML;
                objDPArr[17].Value = objRecordContent.m_strBEFOREDINNER_VCHR;
                objDPArr[18].Value = objRecordContent.m_strBEFOREDINNER_XML;
                objDPArr[19].Value = objRecordContent.m_strTWOAFTERDINNER_VCHR;
                objDPArr[20].Value = objRecordContent.m_strTWOAFTERDINNER_XML;
                objDPArr[21].Value = objRecordContent.m_strBEFORESLEEP_VCHR;
                objDPArr[22].Value = objRecordContent.m_strBEFORESLEEP_XML;
                objDPArr[23].Value = objRecordContent.m_strBEIZHU_VCHR;
                objDPArr[24].Value = objRecordContent.m_strBEIZHU_XML;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(13, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr2[2].Value = 1;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strNULLABDOMEN_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strTWOBREAKFAST_RIGTH;
                objDPArr2[7].Value = objRecordContent.m_strBEFORELUNCH_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strTWOAFTERLUNCH_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strBEFOREDINNER_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strTWOAFTERDINNER_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strBEFORESLEEP_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strBEIZHU_RIGHT; 
                 
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

            clsEMR_intbloodsugarwatchValue objRecordContent = (clsEMR_intbloodsugarwatchValue)p_objRecordContent;
            /// <summary>
            /// ��IntensiveTendRecordContent1��ȡָ����������޸�ʱ�䡣

            /// </summary>
            string strCheckLastModifyRecordSQL = @"select t2.modifydate, t2.modifyuserid
  from T_EMR_intbloodsugarwatchcon t2
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
                objHRPServ = null;

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

            clsEMR_intbloodsugarwatchValue objRecordContent = (clsEMR_intbloodsugarwatchValue)p_objRecordContent;
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
                objHRPServ.CreateDatabaseParameter(22, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strRecordUserID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmRecordDate;
                objDPArr[2].Value = lngSequence;
                objDPArr[3].Value = objRecordContent.m_intMarkStatus;
                objDPArr[4].Value = objRecordContent.m_strNULLABDOMEN_VCHR;
                objDPArr[5].Value = objRecordContent.m_strNULLABDOMEN_XML;
                objDPArr[6].Value = objRecordContent.m_strTWOBREAKFAST_VCHR;
                objDPArr[7].Value = objRecordContent.m_strTWOBREAKFAST_XML;
                objDPArr[8].Value = objRecordContent.m_strBEFORELUNCH_VCHR;
                objDPArr[9].Value = objRecordContent.m_strBEFORELUNCH_XML;
                objDPArr[10].Value = objRecordContent.m_strTWOAFTERLUNCH_VCHR;
                objDPArr[11].Value = objRecordContent.m_strTWOAFTERLUNCH_XML;
                objDPArr[12].Value = objRecordContent.m_strBEFOREDINNER_VCHR;
                objDPArr[13].Value = objRecordContent.m_strBEFOREDINNER_XML;
                objDPArr[14].Value = objRecordContent.m_strTWOAFTERDINNER_VCHR;
                objDPArr[15].Value = objRecordContent.m_strTWOAFTERDINNER_XML;
                objDPArr[16].Value = objRecordContent.m_strBEFORESLEEP_VCHR;
                objDPArr[17].Value = objRecordContent.m_strBEFORESLEEP_XML;
                objDPArr[18].Value = objRecordContent.m_strBEIZHU_VCHR;
                objDPArr[19].Value = objRecordContent.m_strBEIZHU_XML;

                objDPArr[20].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[21].DbType = DbType.DateTime;
                objDPArr[21].Value = p_objRecordContent.m_dtmCreateDate;

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
                objHRPServ.CreateDatabaseParameter(13, out objDPArr2);
                objDPArr2[0].Value = objRecordContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmCreateDate;
                objDPArr2[2].Value = 1;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[4].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[5].Value = objRecordContent.m_strNULLABDOMEN_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strTWOBREAKFAST_RIGTH;
                objDPArr2[7].Value = objRecordContent.m_strBEFORELUNCH_RIGHT;
                objDPArr2[8].Value = objRecordContent.m_strTWOAFTERLUNCH_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strBEFOREDINNER_RIGHT;
                objDPArr2[10].Value = objRecordContent.m_strTWOAFTERDINNER_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strBEFORESLEEP_RIGHT;
                objDPArr2[12].Value = objRecordContent.m_strBEIZHU_RIGHT;
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
                objHRPServ = null;

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
            string strSql = @" update t_emr_intbloodsugarwatchcon t set t.status_int = 0
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

            clsEMR_intbloodsugarwatchValue objRecordContent = (clsEMR_intbloodsugarwatchValue)p_objRecordContent;
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
            /// ��T_EMR_WAITLAYRECORD_GX��T_EMR_WAITLAYRECORD_RIGHT_GX��ȡLastModifyDate��FirstPrintDate
            /// </summary>
            string strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate_dat, b.modifydate
  from T_EMR_intbloodsugarwatch a
 inner join T_EMR_intbloodsugarwatchcon b on a.registerid_chr =
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

            string c_strGetDeleteRecordContentSQL = @"select t1.registerid_chr,
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
       t1.markstatus,
        t1.nullabdomen_vchr
        t1.nullabdomen_xml
         t1.twobreakfast_vchr
         t1.twobreakfast_xml
         t1.beforelunch_vchr
         t1.beforelunch_xml
         t1.twoafterlunch_vchr
         t1.twoafterlunch_xml
          t1.beforedinner_vchr
          t1.beforedinner_xml
          t1.twoafterdinner_vchr
         t1.twoafterdinner_xml
          t1.beforesleep_vchr 
          t1.beforesleep_xml
          t1.beizhu_vchr
          t1.beizhu_xml
          t2.modifydate,
         t2.modifyuserid,
          t2.nullabdomen_right,    
         t2.twobreakfast_rigth,   
          t2.beforelunch_right,    
          t2.twoafterlunch_right,  
          t2.beforedinner_right,   
          t2.twoafterdinner_right, 
          t2.beforesleep_right,    
          t2.beizhu_right, 
  from t_emr_intbloodsugarwatch t1
 inner join t_emr_intbloodsugarwatchcon t2 on t1.registerid_chr =
                                                t2.registerid_chr
                                            and t1.createdate_dat =
                                                t2.createdate_dat
 where t1.status_int = 0
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?
   and t2.modifydate = (select max(modifydate)
                          from t_emr_intbloodsugarwatchcon
                         where registerid_chr = ?
                           and createdate_dat = ?)";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);
                objDPArr[2].Value = p_strRegisterId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strCreatedDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���

                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region ���ý��
                    clsEMR_intbloodsugarwatchValue objRecordContent = new clsEMR_intbloodsugarwatchValue();
                    DataRow dtrSelected = dtbValue.Rows[0];

                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtrSelected["CREATEUSERID_CHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtrSelected["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtrSelected["MODIFYUSERID"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtrSelected["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;


                    int intTemp = 0;
                    int.TryParse(dtrSelected["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(dtrSelected["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strNULLABDOMEN_VCHR = dtrSelected["nullabdomen_vchr"].ToString();
                    objRecordContent.m_strNULLABDOMEN_XML = dtrSelected["nullabdomen_xml"].ToString();
                    objRecordContent.m_strTWOBREAKFAST_VCHR = dtrSelected["twobreakfast_vchr"].ToString();
                    objRecordContent.m_strTWOBREAKFAST_XML = dtrSelected["twobreakfast_xml"].ToString();

                    objRecordContent.m_strBEFORELUNCH_VCHR = dtrSelected["beforelunch_vchr"].ToString();
                    objRecordContent.m_strBEFORELUNCH_XML = dtrSelected["beforelunch_xml"].ToString();
                    objRecordContent.m_strTWOAFTERLUNCH_VCHR = dtrSelected["twoafterlunch_vchr"].ToString();
                    objRecordContent.m_strTWOAFTERLUNCH_XML = dtrSelected["twoafterlunch_xml"].ToString();
                    objRecordContent.m_strBEFOREDINNER_VCHR = dtrSelected["beforedinner_vchr"].ToString();
                    objRecordContent.m_strBEFOREDINNER_XML = dtrSelected["beforedinner_xml"].ToString();

                    objRecordContent.m_strTWOAFTERDINNER_VCHR = dtrSelected["twoafterdinner_vchr"].ToString();
                    objRecordContent.m_strTWOAFTERDINNER_XML = dtrSelected["twoafterdinner_xml"].ToString();
                    objRecordContent.m_strBEFORESLEEP_VCHR = dtrSelected["beforesleep_vchr"].ToString();
                    objRecordContent.m_strBEFORESLEEP_XML = dtrSelected["beforesleep_xml"].ToString();
                    objRecordContent.m_strBEIZHU_VCHR = dtrSelected["beizhu_vchr"].ToString();
                    objRecordContent.m_strBEIZHU_XML = dtrSelected["beizhu_xml"].ToString();

                    objRecordContent.m_strNULLABDOMEN_RIGHT = dtrSelected["nullabdomen_right"].ToString();
                    objRecordContent.m_strTWOBREAKFAST_RIGTH = dtrSelected["twobreakfast_rigth"].ToString();
                    objRecordContent.m_strBEFORELUNCH_RIGHT = dtrSelected["beforelunch_right"].ToString();
                    objRecordContent.m_strTWOAFTERLUNCH_RIGHT = dtrSelected["twoafterlunch_right"].ToString();
                    objRecordContent.m_strBEFOREDINNER_RIGHT = dtrSelected["beforedinner_right"].ToString();
                    objRecordContent.m_strTWOAFTERDINNER_RIGHT = dtrSelected["twoafterdinner_right"].ToString();
                    objRecordContent.m_strBEFORESLEEP_RIGHT = dtrSelected["beforesleep_right"].ToString();
                    objRecordContent.m_strBEIZHU_RIGHT = dtrSelected["beizhu_right"].ToString();

                    objRecordContent.m_intMarkStatus = Convert.ToInt32(dtrSelected["MARKSTATUS"]);

                    //��ȡǩ������
                    long lngS = 0;
                    if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        if (lngS != -1)//�Ӿɱ�����������û�е���ǩ����ʡȥ��ѯ�Ĳ���
                        {
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                            //�ͷ�
                            objSign = null;
                        }
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
