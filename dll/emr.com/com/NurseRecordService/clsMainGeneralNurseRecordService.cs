using System;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections; 

namespace com.digitalwave.clsMainGeneralNurseRecordService
{
	/// <summary>
	/// ʵ�ֻ����¼���м����
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsMainGeneralNurseRecordService	: com.digitalwave.clsRecordsService.clsRecordsService
	{
		public clsMainGeneralNurseRecordService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		private const string c_strUpdateFirstPrintDateSQL_Normal="update  generalnurserecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

		private const string c_strCheckLastModifyRecordSQL_Normal=@"select b.modifydate,b.modifyuserid from generalnurserecord a,generalnurserecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from generalnurserecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

		private const string c_strGetDeleteRecordSQL_Normal="select deactiveddate,deactivedoperatorid from generalnurserecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

		private const string c_strDeleteRecordSQL_Normal="update generalnurserecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strGetRecordContentSQL_Normal = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.recordcontent,
       a.recordcontentxml,
       a.opendate as opendate_main,
       b.modifydate,
       b.modifyuserid,
       b.recordcontent_right,
       b.pagination,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,generalnurserecord a
                 where e.empno_chr = a.createuserid
                   and e.status_int <> -1
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = a.createuserid
           and rownum = 1) firstname
  from generalnurserecord a, generalnurserecordcontent b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and b.inpatientid = a.inpatientid
   and b.inpatientdate = a.inpatientdate
   and b.opendate = a.opendate
   and b.modifydate = (select max(modifydate)
                         from generalnurserecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";
//		private const string c_strGetDoctorContentSQL_Normal= @"select sub2.EmployeeID,PBI.FirstName from GeneralDiseaseRecord a,GeneralDiseaseRecordDoctor sub2,EmployeeBaseInfo PBI where trim(a.InPatientID) = ? and a.InPatientDate= ? and a.OpenDate= ? and a.Status=0 and sub2.EmployeeID=PBI.EmployeeID and sub2.InPatientID=a.InPatientID and sub2.InPatientDate=a.InPatientDate and sub2.OpenDate=a.OpenDate and
//						sub2.ModifyDate=(select Max(ModifyDate) from GeneralDiseaseRecordDoctor Where InPatientID=a.InPatientID and InPatientDate=a.InPatientDate and OpenDate=a.OpenDate)";

		private const string c_strGetRecordContentSQL_1="";
//
		private const string c_strGetRecordContentSQL_2="";
//
		private const string c_strGetRecordContentSQL_="";
//
		private const string c_strCheckLastModifyRecordSQL="";

		private const string c_strGetModifyRecordSQL="";

		private const string c_strGetDeleteRecordSQL="";

		private const string c_strDeleteRecordSQL="";

		/// <summary>
		/// �������ݿ��е��״δ�ӡʱ�䡣
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_intRecordTypeArr">��¼����</param>
		/// <param name="p_dtmOpenDateArr">��¼ʱ��(���¼���ͼ���λ��һһ��Ӧ)</param>
		/// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
		/// <returns></returns>
		[AutoComplete]
		public override long m_lngUpdateFirstPrintDate(
			string p_strInPatientID,
			string p_strInPatientDate,
			int[] p_intRecordTypeArr,
			DateTime[] p_dtmOpenDateArr,
			DateTime p_dtmFirstPrintDate)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMainGeneralNurseRecordService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	

			//������
			if(p_strInPatientID==null||p_strInPatientID==""||p_strInPatientDate==null||p_strInPatientDate==""||
				p_intRecordTypeArr==null||p_dtmOpenDateArr==null||p_intRecordTypeArr.Length != p_dtmOpenDateArr.Length ||p_dtmFirstPrintDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];

                for (int i = 0; i < p_intRecordTypeArr.Length; i++)
                {
                    //���ݲ�ͬ���ӱ�����ȡ��ͬ��SQL���
                    string strSQL = null;
                    switch ((enmDiseaseTrackType)p_intRecordTypeArr[i])
                    {
                        case enmDiseaseTrackType.GeneralNurseRecord:
                            strSQL = c_strUpdateFirstPrintDateSQL_Normal;
                            break;

                        default: return (long)enmOperationResult.Parameter_Error;
                    }

                    //��˳���IDataParameter��ֵ(ʹ��p_dtmOpenDateArr[i]��p_dtmFirstPrintDate)
                    //				for(int j2=0;j2<objDPArr.Length;j2++)
                    //					objDPArr[j2]=new Oracle.DataAccess.Client.OracleParameter();
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = p_dtmFirstPrintDate;
                    objDPArr[1].Value = p_strInPatientID;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = p_dtmOpenDateArr[i];
                    //ִ��SQL			
                    long lngEff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

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
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objTansDataInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsTransDataInfo[] p_objTansDataInfoArr)
		{
			p_objTansDataInfoArr=null;

			//������
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			ArrayList arlTransData = new ArrayList();    
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region һ�㻤���¼
                //һ�㻤���¼��ʹ��c_strGetRecordContentSQL_Normal,//ע��:��ʱ��ѯ������û��OpenDate,��һ�㲡�̼�¼��Server��SQL������ͬ,�ʽ�����ж���
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;// new Oracle.DataAccess.Client.OracleParameter[2];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable       
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Normal, ref dtbValue, objDPArr);

                //ѭ��DataTable
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    DataRow objRow = null;
                    for (int j = 0; j < dtbValue.Rows.Count; j++)
                    {
                        objRow = dtbValue.Rows[j];
                        #region ��DataTable.Rows�л�ȡ���
                        //���ý��
                        clsGeneralNurseRecordContent objRecordContent = new clsGeneralNurseRecordContent();
                        objRecordContent.m_strInPatientID = p_strInPatientID;
                        objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(objRow["OPENDATE_MAIN"].ToString());
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(objRow["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(objRow["MODIFYDATE"].ToString());

                        if (objRow["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();

                        objRecordContent.m_strSignName = objRow["FIRSTNAME"].ToString();
                        objRecordContent.m_StrPagination = objRow["PAGINATION"].ToString();
                        if (objRow["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(objRow["IFCONFIRM"].ToString());
                        if (objRow["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(objRow["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = objRow["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = objRow["CONFIRMREASONXML"].ToString();

                        //					objRecordContent.m_strRecordTitle=objRow["RECORDTITLE"].ToString();
                        //					if(objRow["RECORDTITLETYPE"].ToString()=="")
                        //						objRecordContent.m_intRecordTitleType=-1;
                        //					else objRecordContent.m_intRecordTitleType=int.Parse(objRow["RECORDTITLETYPE"].ToString());
                        objRecordContent.m_strRecordContent_Right = objRow["RECORDCONTENT_RIGHT"].ToString();
                        objRecordContent.m_strRecordContent = objRow["RECORDCONTENT"].ToString();
                        objRecordContent.m_strRecordContentXml = objRow["RECORDCONTENTXML"].ToString();


                        #endregion ��DataTable.Rows�л�ȡ���

                        //���� clsTransDataInfo
                        clsTransDataInfo objInfo = new clsTransDataInfo();
                        objInfo.m_intFlag = (int)enmDiseaseTrackType.GeneralNurseRecord;

                        //���ý���� objInfo.m_objRecordContent
                        objInfo.m_objRecordContent = objRecordContent;

                        arlTransData.Add(objInfo);
                    }
                }
                #endregion һ�㻤���¼

                //������¼������ͬ��һ�㻤���¼���Ĳ���

                //���ؽ����p_objTansDataInfoArr
                p_objTansDataInfoArr = (clsTransDataInfo[])arlTransData.ToArray(typeof(clsTransDataInfo));

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
		/// �鿴��ǰ��¼�Ƿ����µļ�¼��
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngCheckLastModifyRecord(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;

			//������          
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //���ݲ�ͬ���ӱ�����ȡ��ͬ��SQL���
                string strSQL = null;
                switch ((enmDiseaseTrackType)p_intRecordType)
                {
                    case enmDiseaseTrackType.GeneralNurseRecord:
                        strSQL = c_strCheckLastModifyRecordSQL_Normal;
                        break;

                    default: return (long)enmOperationResult.Parameter_Error;
                }

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //ʹ��strSQL����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable            
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from GeneralDiseaseRecord Where trim(InPatientID) = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    //���ݲ�ͬ���ӱ�����ȡ��ͬ��SQL���
                    string strSQL2 = null;
                    switch ((enmDiseaseTrackType)p_intRecordType)
                    {
                        case enmDiseaseTrackType.GeneralNurseRecord:
                            strSQL2 = c_strGetDeleteRecordSQL_Normal;
                            break;

                        default: return (long)enmOperationResult.Parameter_Error;
                    }

                    //					//��˳���IDataParameter��ֵ
                    //					for(int i=0;i<objDPArr.Length;i++)//�������·����ڴ�,��ʹ����ͬ������
                    //						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    //					clsHRPTableService p_objHRPServ =  objHRPServ;
                    //					IDataParameter[] objDPArr = null;
                    p_objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                    objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL2, ref dtbValue, objDPArr);

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
                    //if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //���򣬷���Record_Already_Modify
                    //p_objModifyInfo = new clsPreModifyInfo();
                    //p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    //p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    //return (long)enmOperationResult.Record_Already_Modify;
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
		/// �Ѽ�¼�������С�ɾ������
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngDeleteRecord2DB(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			//���ݲ�ͬ���ӱ�����ȡ��ͬ��SQL���
			string strSQL = "";
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //			switch((enmDiseaseTrackType)p_intRecordType)
                //			{
                //				case enmDiseaseTrackType.GeneralNurseRecord:
                strSQL = c_strDeleteRecordSQL_Normal;
                //					break;
                //
                //				default: return (long)enmOperationResult.Parameter_Error; 
                //			}

                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                //��˳���IDataParameter��ֵ
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

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
		[AutoComplete]
		public long m_lngSetPagination(clsTrackRecordContent p_objContent,string p_strIsAddPage)
		{
			if(p_objContent == null || p_strIsAddPage == null)
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"update generalnurserecordcontent set pagination = ? where inpatientid = ?
                     and inpatientdate = ? and opendate = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strIsAddPage.Trim();
                objDPArr[1].Value = p_objContent.m_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = Convert.ToDateTime(p_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = Convert.ToDateTime(p_objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"));

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff,objDPArr);

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
        /// ��ȡһ��סԺȫ�����ϼ�¼
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public override long m_lngGetAllInactiveInfo(string p_strSQL, string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            p_strSQL = @"select t.createdate,
       t.opendate,
       t.deactiveddate,
       e2.lastname_vchr createdusername,
       e3.lastname_vchr deactiveusername
  from generalnurserecord t, t_bse_employee e2, t_bse_employee e3
 where t.createuserid = e2.empno_chr
   and t.deactivedoperatorid = e3.empno_chr
   and t.inpatientid = ?
   and t.inpatientdate = ?
   and t.status = 1
 order by t.opendate desc";
            return base.m_lngGetAllInactiveInfo(p_strSQL, p_strInpatientId, p_dtmInpatientDate, out  p_objInactiveRecordInfoArr);
        }


	}// END CLASS DEFINITION clsMainDiseaseTrackService

}
