using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.InPatientCaseHistoryServ
{
    /// <summary>
    /// ��ʱ��¼ - ��ʱ�����¼
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBrothRecords_F2RecService : clsDiseaseTrackService
    {
        public clsBrothRecords_F2RecService()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region SQL���
        /// <summary>
        /// ��NEWBABYCIRCSRECORD��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
        /// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
        /// </summary>
        private const string c_strGetTimeListSQL = @"select createdate, opendate
														from brothrecords_f2rec
													where inpatientid = ?
														and inpatientdate = ?
														and status = 0";

        /// <summary>
        /// ��NEWBABYCIRCSRECORD�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
        /// InPatientID ,InPatientDate ,CreateDate,Status = 0
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid, opendate
															from brothrecords_f2rec
														where inpatientid = ?
															and inpatientdate = ?
															and createdate = ?
																and status = 0";

        /// <summary>
        /// ��NEWBABYCIRCSRECORD��ȡɾ��������Ҫ��Ϣ��
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from brothrecords_f2rec
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

        /// <summary>
        /// ��Ӽ�¼��NEWBABYCIRCSRECORD
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into brothrecords_f2rec (inpatientid,inpatientdate,xueya,gongsuojianxue,
		gongsuotime,taixin,gongkou,taimo,xianlu,jianchafa,gongdijizhou,fenmiwu,
		xueyaxml,gongsuojianxuexml,gongsuotimexml,taixinxml,gongkouxml,taimoxml,xianluxml,
		jianchafaxml,gongdijizhouxml,fenmiwuxml,modifydate,modifyuserid,status,opendate,
		createuserid,createdate,signuserid,signusername,recorddate) 
						values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// �޸ļ�¼��NEWBABYCIRCSRECORD
        /// </summary>
        //		private const string c_strModifyRecordSQL= @"Update NEWBABYCIRCSRECORD 
        //			Set BIRTHTIME=?,BIRTHDAYS=?,BIRTHBURL=?,HAEMATOMA=?,FONTANEL=?,CONJUNCTIVA=?,SECRETION=?,PHARYNX=?,WHITEPOINT=?,ICTERUS=?,FESTER=?,
        //		BLEEDING=?,AGNAIL=?,REDSTERN=?,STERNSKIN=?,HEARTLUNG=?,ABDOMEN=?,REMARK=?,REMARKXML=?,BIRTHBURLXML=?,HAEMATOMAXML=?,FONTANELXML=?,
        //		CONJUNCTIVAXML=?,SECRETIONXML=?,PHARYNXXML=?,WHITEPOINTXML=?,ICTERUSXML=?,FESTERXML=?,BLEEDINGXML=?,AGNAILXML=?,REDSTERNXML=?,
        //		STERNSKINXML=?,HEARTLUNGXML=?,ABDOMENXML=?,MODIFYDATE=?,MODIFYUSERID=?,CREATEUSERID=?,CREATEDATE=?,SIGNUSERID=?,SIGNUSERNAME=?,RECORDDATE=?
        //			Where InPatientID=? and InPatientDate=? and OPENDATE=? and Status=0";
        private const string c_strModifyRecordSQL = c_strAddNewRecordSQL;


        /// <summary>
        /// ����ICUNURSERECORD_GXRECORD��ɾ����¼����Ϣ
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update brothrecords_f2rec
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

        /// <summary>
        /// ����ICUNURSERECORD_GXRECORD��FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update brothrecords_f2rec
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

        /// <summary>
        /// ��ICUNURSERECORD_GXRECORD��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
        /// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate, opendate
																	from brothrecords_f2rec
																where inpatientid = ?
																	and inpatientdate = ?
																	and deactivedoperatorid = ?
																	and status = 1";

        /// <summary>
        /// ��ICUNURSERECORD_GXRECORD��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
        /// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate, opendate
																		from brothrecords_f2rec
																	where inpatientid = ?
																		and inpatientdate = ?
																		and status = 1";
        #endregion

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

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
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
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            long lngRes = 0;
            return lngRes;
        }

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

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
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

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecordService","m_lngGetDeleteRecordTimeListAll");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
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

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }


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

            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            string c_strGetRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" inpatientid,
       inpatientdate,
       xueya,
       gongsuojianxue,
       gongsuotime,
       taixin,
       gongkou,
       taimo,
       xianlu,
       jianchafa,
       gongdijizhou,
       fenmiwu,     
       xueyaxml,
       gongsuojianxuexml,
       gongsuotimexml,
       taixinxml,
       gongkouxml,
       taimoxml,
       xianluxml,
       jianchafaxml,
       gongdijizhouxml,
       fenmiwuxml,       
       modifydate,
       modifyuserid,
       status,
       opendate,
       deactiveddate,
       deactivedoperatorid,
       createuserid,
       createdate,
       signuserid,
       signusername,
       recorddate
  from brothrecords_f2rec
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0
 order by modifydate desc " + clsDatabaseSQLConvert.s_StrRownum;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
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
                    #region ���ý��
                    clsBrothRecords_F2Rec objRecordContent = new clsBrothRecords_F2Rec();

                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);


                    objRecordContent.str_XUEYA = dtbValue.Rows[0]["XUEYA"].ToString();
                    objRecordContent.str_GONGSUOJIANXUE = dtbValue.Rows[0]["GONGSUOJIANXUE"].ToString();
                    objRecordContent.str_GONGSUOTIME = dtbValue.Rows[0]["GONGSUOTIME"].ToString();
                    objRecordContent.str_TAIXIN = dtbValue.Rows[0]["TAIXIN"].ToString();
                    objRecordContent.str_GONGKOU = dtbValue.Rows[0]["GONGKOU"].ToString();
                    objRecordContent.str_TAIMO = dtbValue.Rows[0]["TAIMO"].ToString();
                    objRecordContent.str_XIANLU = dtbValue.Rows[0]["XIANLU"].ToString();
                    objRecordContent.str_JIANCHAFA = dtbValue.Rows[0]["JIANCHAFA"].ToString();
                    objRecordContent.str_GONGDIJIZHOU = dtbValue.Rows[0]["GONGDIJIZHOU"].ToString();
                    objRecordContent.str_FENMIWU = dtbValue.Rows[0]["FENMIWU"].ToString();

                    objRecordContent.str_XUEYAXML = dtbValue.Rows[0]["XUEYAXML"].ToString();
                    objRecordContent.str_GONGSUOJIANXUEXML = dtbValue.Rows[0]["GONGSUOJIANXUEXML"].ToString();
                    objRecordContent.str_GONGSUOTIMEXML = dtbValue.Rows[0]["GONGSUOTIMEXML"].ToString();
                    objRecordContent.str_TAIXINXML = dtbValue.Rows[0]["TAIXINXML"].ToString();
                    objRecordContent.str_GONGKOUXML = dtbValue.Rows[0]["GONGKOUXML"].ToString();
                    objRecordContent.str_TAIMOXML = dtbValue.Rows[0]["TAIMOXML"].ToString();
                    objRecordContent.str_XIANLUXML = dtbValue.Rows[0]["XIANLUXML"].ToString();
                    objRecordContent.str_JIANCHAFAXML = dtbValue.Rows[0]["JIANCHAFAXML"].ToString();
                    objRecordContent.str_GONGDIJIZHOUXML = dtbValue.Rows[0]["GONGDIJIZHOUXML"].ToString();
                    objRecordContent.str_FENMIWUXML = dtbValue.Rows[0]["FENMIWUXML"].ToString();

                 
                    objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    objRecordContent.m_dtmOpenDate = Convert.ToDateTime(p_strOpenDate);
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                    objRecordContent.m_strSignUserID = dtbValue.Rows[0]["SIGNUSERID"].ToString();
                    objRecordContent.m_strSignUserName = dtbValue.Rows[0]["SIGNUSERNAME"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);
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

            }
            return lngRes;
        }

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
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
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

        /// <summary>
        /// �����¼�����ݿ⡣
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //������                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;
            clsBrothRecords_F2Rec p_objRecord = (clsBrothRecords_F2Rec)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(31, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = p_objRecord.m_strInPatientID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = p_objRecord.m_dtmInPatientDate;
               
                objLisAddItemRefArr[2].Value = p_objRecord.str_XUEYA;
                objLisAddItemRefArr[3].Value = p_objRecord.str_GONGSUOJIANXUE;
                objLisAddItemRefArr[4].Value = p_objRecord.str_GONGSUOTIME;
                objLisAddItemRefArr[5].Value = p_objRecord.str_TAIXIN;
                objLisAddItemRefArr[6].Value = p_objRecord.str_GONGKOU;
                objLisAddItemRefArr[7].Value = p_objRecord.str_TAIMO;
                objLisAddItemRefArr[8].Value = p_objRecord.str_XIANLU;
                objLisAddItemRefArr[9].Value = p_objRecord.str_JIANCHAFA;
                objLisAddItemRefArr[10].Value = p_objRecord.str_GONGDIJIZHOU;
                objLisAddItemRefArr[11].Value = p_objRecord.str_FENMIWU;

                objLisAddItemRefArr[12].Value = p_objRecord.str_XUEYAXML;
                objLisAddItemRefArr[13].Value = p_objRecord.str_GONGSUOJIANXUEXML;
                objLisAddItemRefArr[14].Value = p_objRecord.str_GONGSUOTIMEXML;
                objLisAddItemRefArr[15].Value = p_objRecord.str_TAIXINXML;
                objLisAddItemRefArr[16].Value = p_objRecord.str_GONGKOUXML;
                objLisAddItemRefArr[17].Value = p_objRecord.str_TAIMOXML;
                objLisAddItemRefArr[18].Value = p_objRecord.str_XIANLUXML;
                objLisAddItemRefArr[19].Value = p_objRecord.str_JIANCHAFAXML;
                objLisAddItemRefArr[20].Value = p_objRecord.str_GONGDIJIZHOUXML;
                objLisAddItemRefArr[21].Value = p_objRecord.str_FENMIWUXML;

                //objLisAddItemRefArr[22].Value = p_objRecord.m_strHAEMATOMAXML;
                //objLisAddItemRefArr[23].Value = p_objRecord.m_strFONTANELXML;
                //objLisAddItemRefArr[24].Value = p_objRecord.m_strCONJUNCTIVAXML;
                //objLisAddItemRefArr[25].Value = p_objRecord.m_strSECRETIONXML;
                //objLisAddItemRefArr[26].Value = p_objRecord.m_strPHARYNXXML;
                //objLisAddItemRefArr[27].Value = p_objRecord.m_strWHITEPOINTXML;
                //objLisAddItemRefArr[28].Value = p_objRecord.m_strICTERUSXML;
                //objLisAddItemRefArr[29].Value = p_objRecord.m_strFESTERXML;
                //objLisAddItemRefArr[30].Value = p_objRecord.m_strBLEEDINGXML;
                //objLisAddItemRefArr[31].Value = p_objRecord.m_strAGNAILXML;
                //objLisAddItemRefArr[32].Value = p_objRecord.m_strREDSTERNXML;
                //objLisAddItemRefArr[33].Value = p_objRecord.m_strSTERNSKINXML;
                //objLisAddItemRefArr[34].Value = p_objRecord.m_strHEARTLUNGXML;
                //objLisAddItemRefArr[35].Value = p_objRecord.m_strABDOMENXML;
                objLisAddItemRefArr[22].DbType = DbType.DateTime;
                objLisAddItemRefArr[22].Value = p_objRecord.m_dtmModifyDate;
                objLisAddItemRefArr[23].Value = p_objRecord.m_strModifyUserID;
                objLisAddItemRefArr[24].Value = 0;
                objLisAddItemRefArr[25].DbType = DbType.DateTime;
                objLisAddItemRefArr[25].Value = p_objRecord.m_dtmOpenDate;
                objLisAddItemRefArr[26].Value = p_objRecord.m_strCreateUserID;
                objLisAddItemRefArr[27].DbType = DbType.DateTime;
                objLisAddItemRefArr[27].Value = p_objRecord.m_dtmCreateDate;
                objLisAddItemRefArr[28].Value = p_objRecord.m_strSignUserID;
                objLisAddItemRefArr[29].Value = p_objRecord.m_strSignUserName;
                objLisAddItemRefArr[30].DbType = DbType.DateTime;
                objLisAddItemRefArr[30].Value = p_objRecord.m_dtmRecordDate;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngRecEff, objLisAddItemRefArr);
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

            clsBrothRecords_F2Rec objRecordContent = (clsBrothRecords_F2Rec)p_objRecordContent;
            /// <summary>
            /// ��IntensiveTendRecordContent1��ȡָ����������޸�ʱ�䡣
            /// </summary>
            string c_strCheckLastModifyRecordSQL = clsDatabaseSQLConvert.s_StrTop1 + @" modifydate,modifyuserid from brothrecords_f2rec
			where status =0	and inpatientid = ? and inpatientdate = ? and opendate = ? order by modifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����			
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objRecordContent.m_dtmOpenDate;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //��DataTable�л�ȡModifyDate��ʹ֮��objRecordContent.m_dtmModifyDate�Ƚ�
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //�����ͬ������DB_Succees
                    if (objRecordContent.m_dtmModifyDate == Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]))
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

            }
            return lngRes;
        }

        /// <summary>
        /// �����޸ĵ����ݱ��浽���ݿ⡣
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //������
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            clsBrothRecords_F2Rec p_objRecord = (clsBrothRecords_F2Rec)p_objRecordContent;
            long lngRes = 0;


            try
            {
                #region
                //				//��ȡIDataParameter����
                //				IDataParameter[] objLisAddItemRefArr = null; 
                //				p_objHRPServ.CreateDatabaseParameter(43,out objLisAddItemRefArr);
                //
                //				objLisAddItemRefArr[0].Value = p_objRecord.m_dtmBIRTHTIME;
                //				objLisAddItemRefArr[1].Value = p_objRecord.m_strBIRTHDAYS;
                //				objLisAddItemRefArr[2].Value = p_objRecord.m_strBIRTHBURL;
                //				objLisAddItemRefArr[3].Value = p_objRecord.m_strHAEMATOMA;
                //				objLisAddItemRefArr[4].Value = p_objRecord.m_strFONTANEL;
                //				objLisAddItemRefArr[5].Value = p_objRecord.m_strCONJUNCTIVA;
                //				objLisAddItemRefArr[6].Value = p_objRecord.m_strSECRETION;
                //				objLisAddItemRefArr[7].Value = p_objRecord.m_strPHARYNX;
                //				objLisAddItemRefArr[8].Value = p_objRecord.m_strWHITEPOINT;
                //				objLisAddItemRefArr[9].Value = p_objRecord.m_strICTERUS;
                //				objLisAddItemRefArr[10].Value = p_objRecord.m_strFESTER;
                //				objLisAddItemRefArr[11].Value = p_objRecord.m_strBLEEDING;
                //				objLisAddItemRefArr[12].Value = p_objRecord.m_strAGNAIL;
                //				objLisAddItemRefArr[13].Value = p_objRecord.m_strREDSTERN;
                //				objLisAddItemRefArr[14].Value = p_objRecord.m_strSTERNSKIN;
                //				objLisAddItemRefArr[15].Value = p_objRecord.m_strHEARTLUNG;
                //				objLisAddItemRefArr[16].Value = p_objRecord.m_strABDOMEN;
                //				objLisAddItemRefArr[17].Value = p_objRecord.m_strREMARK;
                //				objLisAddItemRefArr[18].Value = p_objRecord.m_strREMARKXML;
                //				objLisAddItemRefArr[19].Value = p_objRecord.m_strBIRTHBURLXML;
                //				objLisAddItemRefArr[20].Value = p_objRecord.m_strHAEMATOMAXML;
                //				objLisAddItemRefArr[21].Value = p_objRecord.m_strFONTANELXML;
                //				objLisAddItemRefArr[22].Value = p_objRecord.m_strCONJUNCTIVAXML;
                //				objLisAddItemRefArr[23].Value = p_objRecord.m_strSECRETIONXML;
                //				objLisAddItemRefArr[24].Value = p_objRecord.m_strPHARYNXXML;
                //				objLisAddItemRefArr[25].Value = p_objRecord.m_strWHITEPOINTXML;
                //				objLisAddItemRefArr[26].Value = p_objRecord.m_strICTERUSXML;
                //				objLisAddItemRefArr[27].Value = p_objRecord.m_strFESTERXML;
                //				objLisAddItemRefArr[28].Value = p_objRecord.m_strBLEEDINGXML;
                //				objLisAddItemRefArr[29].Value = p_objRecord.m_strAGNAILXML;
                //				objLisAddItemRefArr[30].Value = p_objRecord.m_strREDSTERNXML;
                //				objLisAddItemRefArr[31].Value = p_objRecord.m_strSTERNSKINXML;
                //				objLisAddItemRefArr[32].Value = p_objRecord.m_strHEARTLUNGXML;
                //				objLisAddItemRefArr[33].Value = p_objRecord.m_strABDOMENXML;
                //				objLisAddItemRefArr[34].Value = p_objRecord.m_dtmModifyDate;
                //				objLisAddItemRefArr[35].Value = p_objRecord.m_strModifyUserID;
                //				objLisAddItemRefArr[36].Value = p_objRecord.m_strCreateUserID;
                //				objLisAddItemRefArr[37].Value = p_objRecord.m_dtmCreateDate;
                //				objLisAddItemRefArr[37].Value = p_objRecord.m_strSignUserID;
                //				objLisAddItemRefArr[38].Value = p_objRecord.m_strSignUserName;
                //				objLisAddItemRefArr[39].Value = p_objRecord.m_dtmRecordDate;
                //				objLisAddItemRefArr[40].Value = p_objRecord.m_strInPatientID;
                //				objLisAddItemRefArr[41].Value = p_objRecord.m_dtmInPatientDate;
                //				objLisAddItemRefArr[42].Value = p_objRecord.m_dtmOpenDate;
                //
                //				//ִ��SQL
                //				long lngEff=0;
                //				lngRes =  p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL,ref lngEff,objLisAddItemRefArr);
                #endregion
                m_lngAddNewRecord2DB(p_objRecord, p_objHRPServ);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        /// <summary>
        /// �Ѽ�¼�������С�ɾ����(���ⲿֱ�ӵ���)
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteCircsRecord(clsTrackRecordContent p_objRecordContent)
        {


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngDeleteRecord2DB(p_objRecordContent, objHRPServ);
            }
            finally
            {
                //objHRPServ.Dispose();

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
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            clsBrothRecords_F2Rec objRecordContent = (clsBrothRecords_F2Rec)p_objRecordContent;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objRecordContent.m_dtmOpenDate;

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

            } return lngRes;
        }
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
            return lngRes;
        }

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
            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = clsDatabaseSQLConvert.s_StrTop1 + @" inpatientid,
       inpatientdate,
       xueya,
       gongsuojianxue,
       gongsuotime,
       taixin,
       gongkou,
       taimo,
       xianlu,
       jianchafa,
       gongdijizhou,
       fenmiwu,     
       xueyaxml,
       gongsuojianxuexml,
       gongsuotimexml,
       taixinxml,
       gongkouxml,
       taimoxml,
       xianluxml,
       jianchafaxml,
       gongdijizhouxml,
       fenmiwuxml,      
       modifydate,
       modifyuserid,
       status,
       opendate,
       deactiveddate,
       deactivedoperatorid,
       createuserid,
       createdate,
       signuserid,
       signusername,
       recorddate
  from brothrecords_f2rec
 where status = 1
   and inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
 order by modifydate desc " + clsDatabaseSQLConvert.s_StrRownum;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
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
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region ���ý��
                    clsBrothRecords_F2Rec objRecordContent = new clsBrothRecords_F2Rec();

                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);


                    objRecordContent.str_XUEYA = dtbValue.Rows[0]["XUEYA"].ToString();
                    objRecordContent.str_GONGSUOJIANXUE = dtbValue.Rows[0]["GONGSUOJIANXUE"].ToString();
                    objRecordContent.str_GONGSUOTIME = dtbValue.Rows[0]["GONGSUOTIME"].ToString();
                    objRecordContent.str_TAIXIN = dtbValue.Rows[0]["TAIXIN"].ToString();
                    objRecordContent.str_GONGKOU = dtbValue.Rows[0]["GONGKOU"].ToString();
                    objRecordContent.str_TAIMO = dtbValue.Rows[0]["TAIMO"].ToString();
                    objRecordContent.str_XIANLU = dtbValue.Rows[0]["XIANLU"].ToString();
                    objRecordContent.str_JIANCHAFA = dtbValue.Rows[0]["JIANCHAFA"].ToString();
                    objRecordContent.str_GONGDIJIZHOU = dtbValue.Rows[0]["GONGDIJIZHOU"].ToString();
                    objRecordContent.str_FENMIWU = dtbValue.Rows[0]["FENMIWU"].ToString();

                    objRecordContent.str_XUEYAXML = dtbValue.Rows[0]["XUEYAXML"].ToString();
                    objRecordContent.str_GONGSUOJIANXUEXML = dtbValue.Rows[0]["GONGSUOJIANXUEXML"].ToString();
                    objRecordContent.str_GONGSUOTIMEXML = dtbValue.Rows[0]["GONGSUOTIMEXML"].ToString();
                    objRecordContent.str_TAIXINXML = dtbValue.Rows[0]["TAIXINXML"].ToString();
                    objRecordContent.str_GONGKOUXML = dtbValue.Rows[0]["GONGKOUXML"].ToString();
                    objRecordContent.str_TAIMOXML = dtbValue.Rows[0]["TAIMOXML"].ToString();
                    objRecordContent.str_XIANLUXML = dtbValue.Rows[0]["XIANLUXML"].ToString();
                    objRecordContent.str_JIANCHAFAXML = dtbValue.Rows[0]["JIANCHAFAXML"].ToString();
                    objRecordContent.str_GONGDIJIZHOUXML = dtbValue.Rows[0]["GONGDIJIZHOUXML"].ToString();
                    objRecordContent.str_FENMIWUXML = dtbValue.Rows[0]["FENMIWUXML"].ToString();



                    objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[0]["MODIFYDATE"]);
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    objRecordContent.m_dtmOpenDate = Convert.ToDateTime(p_strOpenDate);
                    if (dtbValue.Rows[0]["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());
                    objRecordContent.m_dtmDeActivedDate = Convert.ToDateTime(dtbValue.Rows[0]["DEACTIVEDDATE"]);
                    objRecordContent.m_strDeActivedOperatorID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[0]["CREATEDATE"]);
                    objRecordContent.m_strSignUserID = dtbValue.Rows[0]["SIGNUSERID"].ToString();
                    objRecordContent.m_strSignUserName = dtbValue.Rows[0]["SIGNUSERNAME"].ToString();
                    objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[0]["RECORDDATE"]);

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

            }
            return lngRes;
        }


        /// <summary>
        /// ��ȡ���������¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strBirthTime"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strBirthTime,
            out clsBrothRecords_F2Rec[] p_objRecordArr)
        {
            p_objRecordArr = null;
            long lngRes = -1;
            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            string strGetAllContentSQL = @"select t4.lastname_vchr as createusername, t1.inpatientid,
       t1.inpatientdate,
       t1. xueya,
        t1.gongsuojianxue,
        t1.gongsuotime,
        t1.taixin,
        t1.gongkou,
        t1.taimo,
        t1.xianlu,
        t1.jianchafa,
        t1.gongdijizhou,
        t1.fenmiwu,     
        t1.xueyaxml,
        t1.gongsuojianxuexml,
        t1.gongsuotimexml,
        t1.taixinxml,
        t1.gongkouxml,
        t1.taimoxml,
        t1.xianluxml,
        t1.jianchafaxml,
        t1.gongdijizhouxml,
        t1.fenmiwuxml,      
       t1.modifydate,
       t1.modifyuserid,
       t1.status,
       t1.opendate,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.createuserid,
       t1.createdate,
       t1.signuserid,
       t1.signusername,
       t1.recorddate
  from brothrecords_f2rec t1, t_bse_employee t4
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.createuserid = t4.empno_chr
 order by t1.createdate, modifydate";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbValue = new DataTable();//�����¼����  

                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetAllContentSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsBrothRecords_F2Rec objRecordContent = null;
                    p_objRecordArr = new clsBrothRecords_F2Rec[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordContent = new clsBrothRecords_F2Rec();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);


                        objRecordContent.str_XUEYA = dtbValue.Rows[0]["XUEYA"].ToString();
                        objRecordContent.str_GONGSUOJIANXUE = dtbValue.Rows[0]["GONGSUOJIANXUE"].ToString();
                        objRecordContent.str_GONGSUOTIME = dtbValue.Rows[0]["GONGSUOTIME"].ToString();
                        objRecordContent.str_TAIXIN = dtbValue.Rows[0]["TAIXIN"].ToString();
                        objRecordContent.str_GONGKOU = dtbValue.Rows[0]["GONGKOU"].ToString();
                        objRecordContent.str_TAIMO = dtbValue.Rows[0]["TAIMO"].ToString();
                        objRecordContent.str_XIANLU = dtbValue.Rows[0]["XIANLU"].ToString();
                        objRecordContent.str_JIANCHAFA = dtbValue.Rows[0]["JIANCHAFA"].ToString();
                        objRecordContent.str_GONGDIJIZHOU = dtbValue.Rows[0]["GONGDIJIZHOU"].ToString();
                        objRecordContent.str_FENMIWU = dtbValue.Rows[0]["FENMIWU"].ToString();

                        objRecordContent.str_XUEYAXML = dtbValue.Rows[0]["XUEYAXML"].ToString();
                        objRecordContent.str_GONGSUOJIANXUEXML = dtbValue.Rows[0]["GONGSUOJIANXUEXML"].ToString();
                        objRecordContent.str_GONGSUOTIMEXML = dtbValue.Rows[0]["GONGSUOTIMEXML"].ToString();
                        objRecordContent.str_TAIXINXML = dtbValue.Rows[0]["TAIXINXML"].ToString();
                        objRecordContent.str_GONGKOUXML = dtbValue.Rows[0]["GONGKOUXML"].ToString();
                        objRecordContent.str_TAIMOXML = dtbValue.Rows[0]["TAIMOXML"].ToString();
                        objRecordContent.str_XIANLUXML = dtbValue.Rows[0]["XIANLUXML"].ToString();
                        objRecordContent.str_JIANCHAFAXML = dtbValue.Rows[0]["JIANCHAFAXML"].ToString();
                        objRecordContent.str_GONGDIJIZHOUXML = dtbValue.Rows[0]["GONGDIJIZHOUXML"].ToString();
                        objRecordContent.str_FENMIWUXML = dtbValue.Rows[0]["FENMIWUXML"].ToString();

                        objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                        objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        objRecordContent.m_strSignUserID = dtbValue.Rows[i]["SIGNUSERID"].ToString();
                        objRecordContent.m_strSignUserName = dtbValue.Rows[i]["SIGNUSERNAME"].ToString();
                        objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[i]["RECORDDATE"]);

                        p_objRecordArr[i] = objRecordContent;
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return (long)enmOperationResult.DB_Succeed;
        }

        /// <summary>
        /// ��ȡ�������µ������������¼
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strBirthTime"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllModifiedContent(string p_strInPatientID,
            string p_strInPatientDate,
            out clsBrothRecords_F2Rec[] p_objRecordArr)
        {
            p_objRecordArr = null;
            long lngRes = -1;
            //������
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                return (long)enmOperationResult.Parameter_Error;

            string strGetAllContentSQL = @"select t4.lastname_vchr as createusername, t1.inpatientid,
       t1.inpatientdate,
       t1. xueya,
       t1.gongsuojianxue,
       t1.gongsuotime,
       t1.taixin,
       t1.gongkou,
       t1.taimo,
       t1.xianlu,
       t1.jianchafa,
       t1.gongdijizhou,
       t1.fenmiwu,     
       t1.xueyaxml,
       t1.gongsuojianxuexml,
       t1.gongsuotimexml,
       t1.taixinxml,
       t1.gongkouxml,
       t1.taimoxml,
       t1.xianluxml,
       t1.jianchafaxml,
       t1.gongdijizhouxml,
       t1.fenmiwuxml,      
       t1.modifydate,
       t1.modifyuserid,
       t1.status,
       t1.opendate,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.createuserid,
       t1.createdate,
       t1.signuserid,
       t1.signusername,
       t1.recorddate
  from brothrecords_f2rec t1, t_bse_employee t4
 where t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.createuserid = t4.empno_chr
   and t1.modifydate = (select max(modifydate)
                          from brothrecords_f2rec
                         where inpatientid = t1.inpatientid
                           and inpatientdate = t1.inpatientdate                          
                           and createdate = t1.createdate)
 order by t1.createdate, modifydate";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                DataTable dtbValue = new DataTable();//�����¼����  

                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetAllContentSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsBrothRecords_F2Rec objRecordContent = null;
                    p_objRecordArr = new clsBrothRecords_F2Rec[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordContent = new clsBrothRecords_F2Rec();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);


                        objRecordContent.str_XUEYA = dtbValue.Rows[0]["XUEYA"].ToString();
                        objRecordContent.str_GONGSUOJIANXUE = dtbValue.Rows[0]["GONGSUOJIANXUE"].ToString();
                        objRecordContent.str_GONGSUOTIME = dtbValue.Rows[0]["GONGSUOTIME"].ToString();
                        objRecordContent.str_TAIXIN = dtbValue.Rows[0]["TAIXIN"].ToString();
                        objRecordContent.str_GONGKOU = dtbValue.Rows[0]["GONGKOU"].ToString();
                        objRecordContent.str_TAIMO = dtbValue.Rows[0]["TAIMO"].ToString();
                        objRecordContent.str_XIANLU = dtbValue.Rows[0]["XIANLU"].ToString();
                        objRecordContent.str_JIANCHAFA = dtbValue.Rows[0]["JIANCHAFA"].ToString();
                        objRecordContent.str_GONGDIJIZHOU = dtbValue.Rows[0]["GONGDIJIZHOU"].ToString();
                        objRecordContent.str_FENMIWU = dtbValue.Rows[0]["FENMIWU"].ToString();

                        objRecordContent.str_XUEYAXML = dtbValue.Rows[0]["XUEYAXML"].ToString();
                        objRecordContent.str_GONGSUOJIANXUEXML = dtbValue.Rows[0]["GONGSUOJIANXUEXML"].ToString();
                        objRecordContent.str_GONGSUOTIMEXML = dtbValue.Rows[0]["GONGSUOTIMEXML"].ToString();
                        objRecordContent.str_TAIXINXML = dtbValue.Rows[0]["TAIXINXML"].ToString();
                        objRecordContent.str_GONGKOUXML = dtbValue.Rows[0]["GONGKOUXML"].ToString();
                        objRecordContent.str_TAIMOXML = dtbValue.Rows[0]["TAIMOXML"].ToString();
                        objRecordContent.str_XIANLUXML = dtbValue.Rows[0]["XIANLUXML"].ToString();
                        objRecordContent.str_JIANCHAFAXML = dtbValue.Rows[0]["JIANCHAFAXML"].ToString();
                        objRecordContent.str_GONGDIJIZHOUXML = dtbValue.Rows[0]["GONGDIJIZHOUXML"].ToString();
                        objRecordContent.str_FENMIWUXML = dtbValue.Rows[0]["FENMIWUXML"].ToString();


                        objRecordContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        objRecordContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        objRecordContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        objRecordContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                        objRecordContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        objRecordContent.m_strSignUserID = dtbValue.Rows[i]["SIGNUSERID"].ToString();
                        objRecordContent.m_strSignUserName = dtbValue.Rows[i]["SIGNUSERNAME"].ToString();
                        objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[i]["RECORDDATE"]);

                        p_objRecordArr[i] = objRecordContent;
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return (long)enmOperationResult.DB_Succeed;
        }
    }
}