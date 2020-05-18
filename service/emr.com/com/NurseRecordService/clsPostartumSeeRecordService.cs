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
	/// ��������������۲��¼  �����м��
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsPostartumSeeRecordService : clsDiseaseTrackService
	{
		#region SQL���
		/// <summary>
		/// ��ICUACAD_POSTPARTUMSEERECORD��ȡָ�����˵�����û��ɾ����¼��ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 0 ������п��õ�CreateDateʱ��
		/// </summary>
        private const string c_strGetTimeListSQL = @"select t.createdate_dat,recorddate_dat
  from icuacad_postpartumseerecord t
 where t.registerid_chr = ?
   and t.status_int = 1";

		/// <summary>
		/// ��ICUACAD_POSTPARTUMSEERECORD�л�ȡָ��ʱ��ı�,��ȡ�Ѿ����ڼ�¼����Ҫ��Ϣ
		/// InPatientID ,InPatientDate ,CreateDate,Status = 0
		/// </summary>
        private const string c_strCheckCreateDateSQL = @"select t.createdate_dat, t.createuserid_vchr
  from icuacad_postpartumseerecord t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 1";

		/// <summary>
		/// ��ICUACAD_POSTPARTUMSEERECORD��ȡɾ��������Ҫ��Ϣ��
		/// </summary>
        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_vchr
  from icuacad_postpartumseerecord t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.status_int = 0";

		/// <summary>
		/// ��Ӽ�¼��ICUACAD_POSTPARTUMSEERECORD
		/// </summary>
        private const string c_strAddNewRecordSQL = @"insert into icuacad_postpartumseerecord
  (createdate_dat,
   createuserid_vchr,
   sequence_int,
   status_int,
   recorduserid_vchr,
   recorddate_dat,
   ifconfirm_int,
   bloodpressure_chr,
   bodytemparture_chr,
   pulse_chr,
   uterus_chr,
   blooded_chr,
   breakwater_chr,
   embryo_chr,
   uterussize_chr,
   bloodpressure_chrxml,
   bodytemparture_chrxml,
   pulse_chrxml,
   uterus_chrxml,
   blooded_chrxml,
   breakwater_chrxml,
   embryo_chrxml,
   uterussize_chrxml,
   registerid_chr,
   RECORDDATE)
values
  (?,?, ?, 1, ?, ?, 0, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";//22������

		/// <summary>
		/// ��Ӽ�¼��ICUACAD_POSTPARTUMSEECONTENT
		/// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into icuacad_postpartumseecontent
  (status_int, createdate_dat, modifydate, modifyuserid, bloodpressure_chr_right, bodytemparture_chr_right,
pulse_chr_right, uterus_chr_right, blooded_chr_right, breakwater_chr_right, embryo_chr_right,
uterussize_chr_right, registerid_chr,RECORDDATE)
values
  (1, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";//12������

		/// <summary>
		/// �޸ļ�¼��ICUACAD_POSTPARTUMSEERECORD
		/// </summary>
        private const string c_strModifyRecordSQL = @"update icuacad_postpartumseerecord
   set sequence_int = ?,
       recorduserid_vchr = ?,
       recorddate_dat = ?,
       bloodpressure_chr = ?,
       bodytemparture_chr = ?,
       pulse_chr = ?,
       uterus_chr = ?,
       blooded_chr = ?,
       breakwater_chr = ?,
       embryo_chr = ?,
       uterussize_chr = ?,
       bloodpressure_chrxml = ?,
       bodytemparture_chrxml = ?,
       pulse_chrxml = ?,
       uterus_chrxml = ?,
       blooded_chrxml = ?,
       breakwater_chrxml = ?,
       embryo_chrxml = ?,
       uterussize_chrxml = ?,
       RECORDDATE=?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//21������

		/// <summary>
		/// �޸ļ�¼��ICUACAD_POSTPARTUMSEERECORD
		/// </summary>
		private const string c_strModifyRecordContentSQL= c_strAddNewRecordContentSQL;

		/// <summary>
		/// ����ICUACAD_POSTPARTUMSEERECORD��ɾ����¼����Ϣ
		/// </summary>
        private const string c_strDeleteRecordSQL = @"update icuacad_postpartumseerecord t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_vchr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";

		/// <summary>
		/// ����ICUACAD_POSTPARTUMSEERECORD��FirstPrintDate
		/// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update icuacad_postpartumseerecord t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";

		/// <summary>
		/// ����ICUACAD_POSTPARTUMSEERECORD��FirstPrintDate
		/// </summary>
        private const string c_strUpdateALLFirstPrintDateSQL = @"update icuacad_postpartumseerecord t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";

		/// <summary>
		/// ��ICUACAD_POSTPARTUMSEERECORD��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select t.createdate_dat,recorddate_dat
  from icuacad_postpartumseerecord t
 where t.registerid_chr = ?
   and t.deactivedoperatorid_vchr = ?
   and t.status_int = 0";

		/// <summary>
		/// ��ICUACAD_POSTPARTUMSEERECORD��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ�䡣
		/// �� InPatientID ,InPatientDate ,Status = 1 ������п��õ�CreateDate��OpenDateʱ��
		/// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select t.createdate_dat,recorddate_dat
  from icuacad_postpartumseerecord t
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
			p_strCreateDateArr=null;
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
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString(),out dtmTemp);
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
                bool blnRes = objLogger.LogDetailError(objEx,true);
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

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
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
                bool blnRes = objLogger.LogDetailError(objEx,true);
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
			p_strCreateDateArr=null;
            p_strRecordTimeArr = null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngGetDeleteRecordTimeListAll");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//������
			if(string.IsNullOrEmpty(p_strRegisterId))
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
                bool blnRes = objLogger.LogDetailError(objEx,true);
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
			p_objRecordContent=null;
			
			//������
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCteatedDate))
				return (long)enmOperationResult.Parameter_Error;
			//,t2.MODIFYDATE as MODIFYDATE,t2.MODIFYUSERID as MODIFYUSERID
            string c_strGetRecordContentSQL = @"select a.createdate_dat,
       a.createuserid_vchr,
       a.deactiveddate_dat,
       a.deactivedoperatorid_vchr,
       a.firstprintdate_dat,
       a.sequence_int,
       a.status_int,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.ifconfirm_int,
       a.bloodpressure_chr,
       a.bodytemparture_chr,
       a.pulse_chr,
       a.uterus_chr,
       a.blooded_chr,
       a.breakwater_chr,
       a.embryo_chr,
       a.uterussize_chr,
       a.bloodpressure_chrxml,
       a.bodytemparture_chrxml,
       a.pulse_chrxml,
       a.uterus_chrxml,
       a.blooded_chrxml,
       a.breakwater_chrxml,
       a.embryo_chrxml,
       a.uterussize_chrxml,
       a.registerid_chr,
       b.status_int,
       b.modifydate,
       b.modifyuserid,
       b.bloodpressure_chr_right,
       b.bodytemparture_chr_right,
       b.pulse_chr_right,
       b.uterus_chr_right,
       b.blooded_chr_right,
       b.breakwater_chr_right,
       b.embryo_chr_right,
       b.uterussize_chr_right
  from icuacad_postpartumseerecord a
 inner join icuacad_postpartumseecontent b on a.registerid_chr =
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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    #region ���ý��
                    clsIcuACAD_PostPartumseeRecord_VO objRecordContent = new clsIcuACAD_PostPartumseeRecord_VO();
                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    //objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    //objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["CREATEDATE_DAT"].ToString(),out dtmTemp);
                    objRecordContent.m_dtmCreateDate = dtmTemp;
                    
                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["MODIFYDATE"].ToString(),out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;
                    
                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["RECORDDATE_DAT"].ToString(),out dtmTemp);
                    objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID_VCHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtbValue.Rows[0]["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    
                    int intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;

                    intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strBLOODPRESSURE_CHR = dtbValue.Rows[0]["BLOODPRESSURE_CHR"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT = dtbValue.Rows[0]["BLOODPRESSURE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_CHRXML = dtbValue.Rows[0]["BLOODPRESSURE_CHRXML"].ToString();

                    objRecordContent.m_strBODYTEMPARTURE_CHR = dtbValue.Rows[0]["BODYTEMPARTURE_CHR"].ToString();
                    objRecordContent.m_strBODYTEMPARTURE_CHR_RIGHT = dtbValue.Rows[0]["BODYTEMPARTURE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBODYTEMPARTURE_CHRXML = dtbValue.Rows[0]["BODYTEMPARTURE_CHRXML"].ToString();

                    objRecordContent.m_strPULSE_CHR = dtbValue.Rows[0]["PULSE_CHR"].ToString();
                    objRecordContent.m_strPULSE_CHR_RIGHT = dtbValue.Rows[0]["PULSE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPULSE_CHRXML = dtbValue.Rows[0]["PULSE_CHRXML"].ToString();

                    objRecordContent.m_strUTERUS_CHR = dtbValue.Rows[0]["UTERUS_CHR"].ToString();
                    objRecordContent.m_strUTERUS_CHR_RIGHT = dtbValue.Rows[0]["UTERUS_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUS_CHRXML = dtbValue.Rows[0]["UTERUS_CHRXML"].ToString();

                    objRecordContent.m_strBLOODED_CHR = dtbValue.Rows[0]["BLOODED_CHR"].ToString();
                    objRecordContent.m_strBLOODED_CHR_RIGHT = dtbValue.Rows[0]["BLOODED_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBLOODED_CHRXML = dtbValue.Rows[0]["BLOODED_CHRXML"].ToString();

                    objRecordContent.m_strBREAKWATER_CHR = dtbValue.Rows[0]["BREAKWATER_CHR"].ToString();
                    objRecordContent.m_strBREAKWATER_CHRXML = dtbValue.Rows[0]["BREAKWATER_CHRXML"].ToString();
                    objRecordContent.m_strBREAKWATER_CHR_RIGHT = dtbValue.Rows[0]["BREAKWATER_CHR_RIGHT"].ToString();


                    objRecordContent.m_strEMBRYO_CHR = dtbValue.Rows[0]["EMBRYO_CHR"].ToString();
                    objRecordContent.m_strEMBRYO_CHR_RIGHT = dtbValue.Rows[0]["EMBRYO_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEMBRYO_CHRXML = dtbValue.Rows[0]["EMBRYO_CHRXML"].ToString();


                    objRecordContent.m_strUTERUSSIZE_CHR = dtbValue.Rows[0]["UTERUSSIZE_CHR"].ToString();
                    objRecordContent.m_strUTERUSSIZE_CHR_RIGHT = dtbValue.Rows[0]["UTERUSSIZE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSSIZE_CHRXML = dtbValue.Rows[0]["UTERUSSIZE_CHRXML"].ToString();
                  
                    #endregion
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
			p_objModifyInfo=null;

			//������
			if(p_objRecordContent==null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
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
			if(p_objRecordContent==null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
				return (long)enmOperationResult.Parameter_Error;
			clsIcuACAD_PostPartumseeRecord_VO objRecordContent = (clsIcuACAD_PostPartumseeRecord_VO)p_objRecordContent;
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
                objHRPServ.CreateDatabaseParameter(23, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objRecordContent.m_dtmCreateDate;
                objDPArr[1].Value = objRecordContent.m_strCreateUserID;
                objDPArr[2].Value = lngSequence;
                objDPArr[3].Value = objRecordContent.m_strRecordUserID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = objRecordContent.m_dtmRecordDate;

                objDPArr[5].Value = objRecordContent.m_strBLOODPRESSURE_CHR;
                objDPArr[6].Value = objRecordContent.m_strBODYTEMPARTURE_CHR;
                objDPArr[7].Value = objRecordContent.m_strPULSE_CHR;
                objDPArr[8].Value = objRecordContent.m_strUTERUS_CHR;
                objDPArr[9].Value = objRecordContent.m_strBLOODED_CHR;
                objDPArr[10].Value = objRecordContent.m_strBREAKWATER_CHR;
                objDPArr[11].Value = objRecordContent.m_strEMBRYO_CHR;
                objDPArr[12].Value = objRecordContent.m_strUTERUSSIZE_CHR;

                objDPArr[13].Value = objRecordContent.m_strBLOODPRESSURE_CHRXML;
                objDPArr[14].Value = objRecordContent.m_strBODYTEMPARTURE_CHRXML;
                objDPArr[15].Value = objRecordContent.m_strPULSE_CHRXML;
                objDPArr[16].Value = objRecordContent.m_strUTERUS_CHRXML;
                objDPArr[17].Value = objRecordContent.m_strBLOODED_CHRXML;
                objDPArr[18].Value = objRecordContent.m_strBREAKWATER_CHRXML;
                objDPArr[19].Value = objRecordContent.m_strEMBRYO_CHRXML;
                objDPArr[20].Value = objRecordContent.m_strUTERUSSIZE_CHRXML;

                objDPArr[21].Value = objRecordContent.m_strRegisterID;
                objDPArr[22].DbType = DbType.DateTime;
                objDPArr[22].Value = objRecordContent.m_dtmRecordDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
               
                //����ǩ������
                lngRes = objSign.m_lngAddSign(objRecordContent.objSignerArr, lngSequence);
                

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(13, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = objRecordContent.m_dtmCreateDate;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[2].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[3].Value = objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT;
                objDPArr2[4].Value = objRecordContent.m_strBODYTEMPARTURE_CHR_RIGHT;

                objDPArr2[5].Value = objRecordContent.m_strPULSE_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strUTERUS_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strBLOODED_CHR_RIGHT;

                objDPArr2[8].Value = objRecordContent.m_strBREAKWATER_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strEMBRYO_CHR_RIGHT;

                objDPArr2[10].Value = objRecordContent.m_strUTERUSSIZE_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strRegisterID;
                objDPArr2[12].DbType = DbType.DateTime;
                objDPArr2[12].Value = objRecordContent.m_dtmRecordDate;
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
			p_objModifyInfo=null;
			//������
			if(p_objRecordContent==null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
				return (long)enmOperationResult.Parameter_Error;

			clsIcuACAD_PostPartumseeRecord_VO objRecordContent = (clsIcuACAD_PostPartumseeRecord_VO)p_objRecordContent;
			/// <summary>
			/// ��IntensiveTendRecordContent1��ȡָ����������޸�ʱ�䡣
			/// </summary>
            string strCheckLastModifyRecordSQL = @"select t2.modifydate, t2.modifyuserid
  from icuacad_postpartumseecontent t2
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
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID_VCHR"].ToString();
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
			if(p_objRecordContent==null || p_objRecordContent.m_dtmCreateDate == DateTime.MinValue || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
				return (long)enmOperationResult.Parameter_Error;
		
			clsIcuACAD_PostPartumseeRecord_VO objRecordContent = (clsIcuACAD_PostPartumseeRecord_VO)p_objRecordContent;
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
                objDPArr[0].Value = lngSequence;
                objDPArr[1].Value = objRecordContent.m_strRecordUserID;
                objDPArr[2].DbType = DbType.Date;
                objDPArr[2].Value = objRecordContent.m_dtmRecordDate;

                objDPArr[3].Value = objRecordContent.m_strBLOODPRESSURE_CHR;
                objDPArr[4].Value = objRecordContent.m_strBODYTEMPARTURE_CHR;
                objDPArr[5].Value = objRecordContent.m_strPULSE_CHR;
                objDPArr[6].Value = objRecordContent.m_strUTERUS_CHR;
                objDPArr[7].Value = objRecordContent.m_strBLOODED_CHR;
                objDPArr[8].Value = objRecordContent.m_strBREAKWATER_CHR;
                objDPArr[9].Value = objRecordContent.m_strEMBRYO_CHR;
                objDPArr[10].Value = objRecordContent.m_strUTERUSSIZE_CHR;

                objDPArr[11].Value = objRecordContent.m_strBLOODPRESSURE_CHRXML;
                objDPArr[12].Value = objRecordContent.m_strBODYTEMPARTURE_CHRXML;
                objDPArr[13].Value = objRecordContent.m_strPULSE_CHRXML;
                objDPArr[14].Value = objRecordContent.m_strUTERUS_CHRXML;
                objDPArr[15].Value = objRecordContent.m_strBLOODED_CHRXML;
                objDPArr[16].Value = objRecordContent.m_strBREAKWATER_CHRXML;
                objDPArr[17].Value = objRecordContent.m_strEMBRYO_CHRXML;
                objDPArr[18].Value = objRecordContent.m_strUTERUSSIZE_CHRXML;
                 
                objDPArr[19].DbType = DbType.DateTime;
                objDPArr[19].Value = p_objRecordContent.m_dtmRecordDate;


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
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = objRecordContent.m_dtmCreateDate;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objRecordContent.m_dtmModifyDate;
                objDPArr2[2].Value = objRecordContent.m_strModifyUserID;
                objDPArr2[3].Value = objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT;
                objDPArr2[4].Value = objRecordContent.m_strBODYTEMPARTURE_CHR_RIGHT;

                objDPArr2[5].Value = objRecordContent.m_strPULSE_CHR_RIGHT;
                objDPArr2[6].Value = objRecordContent.m_strUTERUS_CHR_RIGHT;
                objDPArr2[7].Value = objRecordContent.m_strBLOODED_CHR_RIGHT;

                objDPArr2[8].Value = objRecordContent.m_strBREAKWATER_CHR_RIGHT;
                objDPArr2[9].Value = objRecordContent.m_strEMBRYO_CHR_RIGHT;

                objDPArr2[10].Value = objRecordContent.m_strUTERUSSIZE_CHR_RIGHT;
                objDPArr2[11].Value = objRecordContent.m_strRegisterID;
                objDPArr2[12].DbType = DbType.DateTime;
                objDPArr2[12].Value = objRecordContent.m_dtmRecordDate;

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
        long m_lngDeleteContentInfo(string p_strRegisterId,DateTime p_dtmCreatedDate)
        {
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmCreatedDate == DateTime.MaxValue)
                return -1;
            string strSql = @" update Icuacad_Postpartumseecontent t set t.status_int = 0
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
			if(p_objRecordContent==null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
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
			p_dtmModifyDate=DateTime.Now;
			p_strFirstPrintDate=null;
			//������
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
				return (long)enmOperationResult.Parameter_Error;
			/// <summary>
			/// ��ICUACAD_POSTPARTUMSEERECORD��ICUACAD_POSTPARTUMSEECONTENT��ȡLastModifyDate��FirstPrintDate
			/// </summary>
            string strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate_dat, b.modifydate
  from icuacad_postpartumseerecord a
 inner join icuacad_postpartumseecontent b on a.registerid_chr =
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
			p_objRecordContent=null;
			//������
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
				return (long)enmOperationResult.Parameter_Error;

            string c_strGetDeleteRecordContentSQL = @"select t1.createdate_dat,
       t1.createuserid_vchr,
       t1.deactiveddate_dat,
       t1.deactivedoperatorid_vchr,
       t1.firstprintdate_dat,
       t1.sequence_int,
       t1.status_int,
       t1.recorduserid_vchr,
       t1.recorddate_dat,
       t1.ifconfirm_int,
       t1.bloodpressure_chr,
       t1.bodytemparture_chr,
       t1.pulse_chr,
       t1.uterus_chr,
       t1.blooded_chr,
       t1.breakwater_chr,
       t1.embryo_chr,
       t1.uterussize_chr,
       t1.bloodpressure_chrxml,
       t1.bodytemparture_chrxml,
       t1.pulse_chrxml,
       t1.uterus_chrxml,
       t1.blooded_chrxml,
       t1.breakwater_chrxml,
       t1.embryo_chrxml,
       t1.uterussize_chrxml,
       t1.registerid_chr,
       t2.modifydate as modifydate,
       t2.modifyuserid as modifyuserid
  from icuacad_postpartumseerecord t1
 inner join icuacad_postpartumseecontent t2 on t1.registerid_chr =
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
                    clsIcuACAD_PostPartumseeRecord_VO objRecordContent = new clsIcuACAD_PostPartumseeRecord_VO();
                    objRecordContent.m_strRegisterID = p_strRegisterId;
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(p_strCreatedDate);

                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["MODIFYDATE"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmModifyDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID_VCHR"].ToString();
                    objRecordContent.m_strRecordUserID = dtbValue.Rows[0]["RECORDUSERID_VCHR"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();

                    int intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["IFCONFIRM_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytIfConfirm = intTemp;

                    intTemp = 0;
                    int.TryParse(dtbValue.Rows[0]["STATUS_INT"].ToString(), out intTemp);
                    objRecordContent.m_bytStatus = intTemp;

                    objRecordContent.m_strBLOODPRESSURE_CHR = dtbValue.Rows[0]["BLOODPRESSURE_CHR"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT = dtbValue.Rows[0]["BLOODPRESSURE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBLOODPRESSURE_CHRXML = dtbValue.Rows[0]["BLOODPRESSURE_CHRXML"].ToString();

                    objRecordContent.m_strBODYTEMPARTURE_CHR = dtbValue.Rows[0]["BODYTEMPARTURE_CHR"].ToString();
                    objRecordContent.m_strBODYTEMPARTURE_CHR_RIGHT = dtbValue.Rows[0]["BODYTEMPARTURE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBODYTEMPARTURE_CHRXML = dtbValue.Rows[0]["BODYTEMPARTURE_CHRXML"].ToString();

                    objRecordContent.m_strPULSE_CHR = dtbValue.Rows[0]["PULSE_CHR"].ToString();
                    objRecordContent.m_strPULSE_CHR_RIGHT = dtbValue.Rows[0]["PULSE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strPULSE_CHRXML = dtbValue.Rows[0]["PULSE_CHRXML"].ToString();

                    objRecordContent.m_strUTERUS_CHR = dtbValue.Rows[0]["UTERUS_CHR"].ToString();
                    objRecordContent.m_strUTERUS_CHR_RIGHT = dtbValue.Rows[0]["UTERUS_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUS_CHRXML = dtbValue.Rows[0]["UTERUS_CHRXML"].ToString();

                    objRecordContent.m_strBLOODED_CHR = dtbValue.Rows[0]["BLOODED_CHR"].ToString();
                    objRecordContent.m_strBLOODED_CHR_RIGHT = dtbValue.Rows[0]["BLOODED_CHR_RIGHT"].ToString();
                    objRecordContent.m_strBLOODED_CHRXML = dtbValue.Rows[0]["BLOODED_CHRXML"].ToString();

                    objRecordContent.m_strBREAKWATER_CHR = dtbValue.Rows[0]["BREAKWATER_CHR"].ToString();
                    objRecordContent.m_strBREAKWATER_CHRXML = dtbValue.Rows[0]["BREAKWATER_CHRXML"].ToString();
                    objRecordContent.m_strBREAKWATER_CHR_RIGHT = dtbValue.Rows[0]["BREAKWATER_CHR_RIGHT"].ToString();


                    objRecordContent.m_strEMBRYO_CHR = dtbValue.Rows[0]["EMBRYO_CHR"].ToString();
                    objRecordContent.m_strEMBRYO_CHR_RIGHT = dtbValue.Rows[0]["EMBRYO_CHR_RIGHT"].ToString();
                    objRecordContent.m_strEMBRYO_CHRXML = dtbValue.Rows[0]["EMBRYO_CHRXML"].ToString();


                    objRecordContent.m_strUTERUSSIZE_CHR = dtbValue.Rows[0]["UTERUSSIZE_CHR"].ToString();
                    objRecordContent.m_strUTERUSSIZE_CHR_RIGHT = dtbValue.Rows[0]["UTERUSSIZE_CHR_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSSIZE_CHRXML = dtbValue.Rows[0]["UTERUSSIZE_CHRXML"].ToString();

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



		#region �������л�ȡ����ûɾ��������
		/// <summary>
		/// �������л�ȡ����ûɾ��������
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strBEFOREHAND_CHR">����</param>
		/// <param name="p_strLAYCOUNT_CHR">����</param>
		/// <returns></returns>
//        [AutoComplete]
//        public   long m_lngGetAllMainRecord(string p_strRegisterID,
//            string p_strCreatedDate,
//            out clsIcuACAD_PostPartumseeRecord_VO[] p_objResultArr)
//        {			
//            p_objResultArr = new clsIcuACAD_PostPartumseeRecord_VO[0];
//            //������
//            if(string.IsNullOrEmpty(p_strRegisterID)|| string.IsNullOrEmpty(p_strCreatedDate))
//                return (long)enmOperationResult.Parameter_Error;
	
//            long lngRes = 0;
		
//            //			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
//            //lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.HIS.clsHISMedChargeTypeSvc","m_lngGetMedChargeTypeInfo");
//            //if(lngRes < 0)//Ȩ��
//            //{
//            //    return -1;
//            //}
//            string c_strSQL=  @"SELECT distinct T1.*										
//														FROM ICUACAD_POSTPARTUMSEERECORD T1,															
//															T_BSE_EMPLOYEE             T4
//														WHERE 									
//														 T1.Status =0
//														AND T1.InPatientID = ?
//														AND T1.InPatientDate = ?
//														AND T1.CREATEUSERID = T4.EMPNO_CHR
//														ORDER BY T1.CREATEDATE";
//            clsHRPTableService objHRPServ = new clsHRPTableService();
//            try
//            {
//                DataTable dtbValue = new DataTable();
//                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = objHRPServ;

//                //��ȡIDataParameter����
//                IDataParameter[] objDPArr = null;
//                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
//                objDPArr[0].Value = p_strRegisterID;
//                objDPArr[1].DbType = DbType.DateTime;
//                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);

//                lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strSQL, ref dtbValue, objDPArr);
//                if (lngRes > 0 && dtbValue.Rows.Count > 0)
//                {
//                    p_objResultArr = new clsIcuACAD_PostPartumseeRecord_VO[dtbValue.Rows.Count];
//                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
//                    {
//                        #region set value
//                        p_objResultArr[i1] = new clsIcuACAD_PostPartumseeRecord_VO();

//                        p_objResultArr[i1].m_strInPatientID = p_strRegisterID;
//                        p_objResultArr[i1].m_dtmInPatientDate = DateTime.Parse(p_strCreatedDate);
//                        p_objResultArr[i1].m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[i1]["CREATEDATE"].ToString());

//                        p_objResultArr[i1].m_strBLOODPRESSURE_CHR = dtbValue.Rows[i1]["BLOODPRESSURE_CHR"].ToString();

//                        p_objResultArr[i1].m_strBODYTEMPARTURE_CHR = dtbValue.Rows[i1]["BODYTEMPARTURE_CHR"].ToString();

//                        p_objResultArr[i1].m_strPULSE_CHR = dtbValue.Rows[i1]["PULSE_CHR"].ToString();

//                        p_objResultArr[i1].m_strUTERUS_CHR = dtbValue.Rows[i1]["UTERUS_CHR"].ToString();

//                        p_objResultArr[i1].m_strBLOODED_CHR = dtbValue.Rows[i1]["BLOODED_CHR"].ToString();

//                        p_objResultArr[i1].m_strBREAKWATER_CHR = dtbValue.Rows[i1]["BREAKWATER_CHR"].ToString();

//                        p_objResultArr[i1].m_strEMBRYO_CHR = dtbValue.Rows[i1]["EMBRYO_CHR"].ToString();

//                        p_objResultArr[i1].m_strUTERUSSIZE_CHR = dtbValue.Rows[i1]["UTERUSSIZE_CHR"].ToString();

//                        //p_objResultArr[i1].m_strSIGNNAME_CHR = dtbValue.Rows[i1]["SIGNNAME_CHR"].ToString();
//                        #endregion
//                    }
//                }
//            }
//            catch (Exception objEx)
//            {

//                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
//                bool blnRes = objLogger.LogError(objEx);
//            }

//            finally
//            {
//                //objHRPServ.Dispose();

//            } 
//            return lngRes;
//        }
		#endregion 

		#region update all first print date
		/// <summary>
		/// update all first print date��
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
		/// <returns></returns>
        //[AutoComplete]
        //public  long m_lngUpdateALLFirstPrintDate(	string p_strInPatientID,
        //    string p_strInPatientDate,
        //    DateTime p_dtmFirstPrintDate)
        //{
        //    //			long lngCheckRes =new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordService","m_lngUpdateALLFirstPrintDate");
        //    //			//if(lngCheckRes <= 0)
        //    //				//return lngCheckRes;	

        //    long lngRes = 0;
        //    clsHRPTableService objHRPServ = new clsHRPTableService();
        //    try
        //    {
        //        //������                              
        //        if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
        //            return (long)enmOperationResult.Parameter_Error;

        //        //��ȡIDataParameter����
        //        IDataParameter[] objDPArr = null;
        //        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
        //        objDPArr[0].DbType = DbType.DateTime;
        //        objDPArr[0].Value = p_dtmFirstPrintDate;
        //        objDPArr[1].Value = p_strInPatientID;
        //        objDPArr[2].DbType = DbType.DateTime;
        //        objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

        //        //ִ��SQL
        //        long lngEff = 0;
        //        lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateALLFirstPrintDateSQL, ref lngEff, objDPArr);

        //    }
        //    catch (Exception objEx)
        //    {

        //        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
        //        bool blnRes = objLogger.LogError(objEx);
        //    }

        //    finally
        //    {
        //        //objHRPServ.Dispose();

        //    } 
        //    return lngRes;		
        //}
		#endregion
	}
}
