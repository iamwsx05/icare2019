using System;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// Summary description for clsApprove_FlowDomain.
    /// </summary>
    public class clsApprove_FlowDomain
    {
        public clsApprove_FlowDomain()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //clsApprove_FlowService m_objApprove_FlowServ=new clsApprove_FlowService();

        /// <summary>
        /// ��˵�
        /// </summary>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strSubject_ID"></param>
        /// <param name="p_strInPatient_ID"></param>
        /// <param name="p_strInPatient_Date"></param>
        /// <param name="p_strOpen_Date"></param>
        /// <param name="p_strDept_ID"></param>
        /// <param name="lngEff"></param>
        /// <returns>
        /// ����ֵ��
        /// -13:Ա���Ŵ���
        /// -10:ϵͳû�ж���õ���������̣�Ӧ�������ݿ��ж��壩
        /// -11:���Ѿ��������󣬲������������
        /// -12:���û���Ȩ���������еĸò���
        /// -16:�õ��Ѿ�ɾ��
        /// ����:�������ݿ������Ϣ
        /// </returns>
        public long lngApproveDocument(string p_strEmployeeID, string p_strSubject_ID, string p_strInPatient_ID, string p_strInPatient_Date, string p_strOpen_Date, string p_strDept_ID, ref long lngEff)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr01()).Service.lngApproveDocument(p_strEmployeeID, p_strSubject_ID, p_strInPatient_ID, p_strInPatient_Date, p_strOpen_Date, p_strDept_ID, ref lngEff));
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// �˻���һ��
        /// </summary>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strSubject_ID"></param>
        /// <param name="p_strInPatient_ID"></param>
        /// <param name="p_strInPatient_Date"></param>
        /// <param name="p_strOpen_Date"></param>
        /// <param name="p_strDept_ID"></param>
        /// <param name="lngEff"></param>
        /// <returns>
        /// -14:û���ҵ��õ��������Ϣ
        /// -15:�Ѿ��˻ص�����һ��
        /// ����:���ݿ������Ϣ
        /// </returns>
        public long lngUntreadDocumentOneLevel(string p_strEmployeeID, string p_strSubject_ID, string p_strInPatient_ID, string p_strInPatient_Date, string p_strOpen_Date, string p_strDept_ID, ref long lngEff)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr01()).Service.lngUntreadDocumentOneLevel(p_strEmployeeID, p_strSubject_ID, p_strInPatient_ID, p_strInPatient_Date, p_strOpen_Date, p_strDept_ID, ref lngEff));
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// �˻ص�����һ��
        /// </summary>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strSubject_ID"></param>
        /// <param name="p_strInPatient_ID"></param>
        /// <param name="p_strInPatient_Date"></param>
        /// <param name="p_strOpen_Date"></param>
        /// <param name="p_strDept_ID"></param>
        /// <param name="lngEff"></param>
        /// <returns>
        /// -14:û���ҵ��õ��������Ϣ
        /// </returns>
        public long lngUntreadDocumentAll(string p_strEmployeeID, string p_strSubject_ID, string p_strInPatient_ID, string p_strInPatient_Date, string p_strOpen_Date, string p_strDept_ID, ref long lngEff)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr01()).Service.lngUntreadDocumentAll(p_strEmployeeID, p_strSubject_ID, p_strInPatient_ID, p_strInPatient_Date, p_strOpen_Date, p_strDept_ID, ref lngEff));
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strSubject_ID"></param>
        /// <param name="p_strInPatient_ID"></param>
        /// <param name="p_strInPatient_Date"></param>
        /// <param name="p_strOpen_Date"></param>
        /// <param name="p_strDept_ID"></param>
        /// <param name="lngEff"></param>
        /// <returns></returns>
        public long lngDeleteDocument(string p_strEmployeeID, string p_strSubject_ID, string p_strInPatient_ID, string p_strInPatient_Date, string p_strOpen_Date, string p_strDept_ID, ref long lngEff)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr01()).Service.lngDeleteDocument(p_strEmployeeID, p_strSubject_ID, p_strInPatient_ID, p_strInPatient_Date, p_strOpen_Date, p_strDept_ID, ref lngEff));
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
        }

        public bool lngCanYouDoIt(string p_strEmployeeID, string p_strSubject_ID, string p_strInPatient_ID, string p_strInPatient_Date, string p_strOpen_Date, string p_strDept_ID)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            bool blnRes = false;
            try
            {
                blnRes = ((new weCare.Proxy.ProxyEmr01()).Service.lngCanYouDoIt(p_strEmployeeID, p_strSubject_ID, p_strInPatient_ID, p_strInPatient_Date, p_strOpen_Date, p_strDept_ID));
            }
            finally
            {
                //m_objService.Dispose();
            }
            return blnRes;
        }

        /// <summary>
        /// �Ƿ���˼�¼
        /// </summary>
        /// <param name="p_strFormName"></param>
        /// <param name="p_strFormType"></param>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        public long m_blnCanAuditing(string p_strFormName, string p_strPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strCurrUserLevel, string p_strCreateUserLevel, ref int p_intCurrAppNo, bool p_blnCZType)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr01()).Service.lngCanAuditing(p_strFormName, p_strPatientID, p_strInPatientDate, p_strOpenDate, p_strCurrUserLevel, p_strCreateUserLevel, ref p_intCurrAppNo, p_blnCZType));
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
            //			string Sql="";
            //			long lngRet=0;
            //			//long lngAffecdet=0;	
            //			System.Data.DataTable dtRecord=null;
            //
            //			if(p_strFormName.Trim().Length==0)
            //				return -1;
            //
            //			if(p_strPatientID.Trim().Length==0)
            //				return -1;
            //			
            //			if(p_blnCZType==true)
            //			{
            //				if(int.Parse(p_strCurrUserLevel)<=int.Parse(p_strCreateUserLevel))
            //				{
            //					return -1;
            //				}
            //			}
            //			Sql="select AUDI_LEVEL from AUDITING_INFO where rtrim(AUDI_INPATIENTID)=? and AUDI_INPATIENTDATE=? and AUDI_OPENDATE=? and AUDI_FORMID=? and rownum<2 order by AUDI_NO desc";
            //
            //			System.Data.IDataParameter[] objDPArrSelect = null;//new Oracle.DataAccess.Client.OracleParameter[4];
            //			new clsHRPTableService().CreateDatabaseParameter(4,out objDPArrSelect);
            //			objDPArrSelect[0].Value=p_strPatientID;
            //			objDPArrSelect[1].Value=DateTime.Parse(p_strInPatientDate);
            //			objDPArrSelect[2].Value=DateTime.Parse(p_strOpenDate);
            //			objDPArrSelect[3].Value=p_strFormName;
            //			lngRet=new  clsHRPTableService().lngGetDataTableWithParameters(Sql,ref dtRecord,objDPArrSelect);
            //			if(dtRecord!=null)
            //			{
            //				if(dtRecord.Rows.Count>0)
            //				{
            //					p_intCurrAppNo=int.Parse(dtRecord.Rows[0][0].ToString());
            //					if(int.Parse(p_strCurrUserLevel)<int.Parse(dtRecord.Rows[0][0].ToString()))
            //						return -1;
            //					else if(int.Parse(p_strCurrUserLevel)==int.Parse(dtRecord.Rows[0][0].ToString()))
            //						return 0;
            //					else
            //						return 1;
            //				}
            //				else
            //				{
            //					return 1;
            //				}
            //			}
            //			else
            //				p_intCurrAppNo=-1;
            //			return -1;
        }

        /// <summary>
        /// ��˼�¼
        /// </summary>
        /// <param name="p_strFormName"></param>
        /// <param name="p_strFormType"></param>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <returns></returns>
        public void m_mthExecAuditing(string p_strFormName, string p_strPatientID, string p_strInPatientDate, string p_strOpenDate, string p_strCurrUserLevel)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.lngExecAuditing(MDIParent.OperatorID.Trim(), p_strFormName, p_strPatientID, p_strInPatientDate, p_strOpenDate, p_strCurrUserLevel);
            }
            finally
            {
                //m_objService.Dispose();
            }
            //			string Sql="";
            //			long lngRet=0;
            //			long lngAffecdet=0;
            //			System.Data.IDataParameter[] objDPArrInsert = null;//new Oracle.DataAccess.Client.OracleParameter[6];
            //
            ////			for(int i=0;i<objDPArrInsert.Length;i++)
            ////				objDPArrInsert[i]=new Oracle.DataAccess.Client.OracleParameter();
            //			new clsHRPTableService().CreateDatabaseParameter(6,out objDPArrInsert);
            //			objDPArrInsert[0].Value=p_strPatientID;
            //			objDPArrInsert[1].Value=Convert.ToDateTime(p_strInPatientDate);
            //			objDPArrInsert[2].Value=Convert.ToDateTime(p_strOpenDate);
            //			objDPArrInsert[3].Value=p_strFormName;
            //			objDPArrInsert[4].Value=p_strCurrUserLevel;
            //			objDPArrInsert[5].Value=MDIParent.OperatorID;
            //			//��Ӧoracle sqlserver�汾���� tfzhang 2005-6-23 17:57:21
            //			Sql="insert into AUDITING_INFO (AUDI_NO,AUDI_INPATIENTID,AUDI_INPATIENTDATE,AUDI_OPENDATE,AUDI_FORMID,AUDI_LEVEL,AUDI_RID)"+
            //				" values ((select nvl(max(AUDI_NO),0)+1 from AUDITING_INFO),?,?,?,?,?,?)";
            //			lngRet=new  com.digitalwave.iCare.middletier.HRPService.clsHRPTableService().lngExecuteParameterSQL(Sql, ref lngAffecdet,objDPArrInsert);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p_strFormName"></param>
        /// <param name="p_strFormType"></param>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        public long m_lngBackAuditing(string p_strFormName, string p_strPatientID, string p_strInPatientDate, string p_strOpenDate, int p_intCurrAppNo)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            long lngRes = 0;
            try
            {
                lngRes = ((new weCare.Proxy.ProxyEmr01()).Service.lngBackAuditing(MDIParent.OperatorID.Trim(), p_strFormName, p_strPatientID, p_strInPatientDate, p_strOpenDate, p_intCurrAppNo));
            }
            finally
            {
                //m_objService.Dispose();
            }
            return lngRes;
            //			string Sql="";
            //			long lngRet=0;
            //			long lngAffecdet=0;
            //			
            //			if(p_intCurrAppNo==-1 || p_intCurrAppNo==0)
            //				return -99;
            //
            //			System.Data.IDataParameter[] objDPArrInsert = null;//new Oracle.DataAccess.Client.OracleParameter[6];
            //
            //			new clsHRPTableService().CreateDatabaseParameter(6,out objDPArrInsert);
            ////			for(int i=0;i<objDPArrInsert.Length;i++)
            ////				objDPArrInsert[i]=new Oracle.DataAccess.Client.OracleParameter();
            //
            //			objDPArrInsert[0].Value=p_strPatientID;
            //			objDPArrInsert[1].Value=Convert.ToDateTime(p_strInPatientDate);
            //			objDPArrInsert[2].Value=Convert.ToDateTime(p_strOpenDate);
            //			objDPArrInsert[3].Value=p_strFormName;
            //			objDPArrInsert[4].Value=p_intCurrAppNo-1;
            //			//			objDPArrInsert[4].Value="";
            //			objDPArrInsert[5].Value=MDIParent.OperatorID;
            //			Sql="insert into AUDITING_INFO (AUDI_NO,AUDI_INPATIENTID,AUDI_INPATIENTDATE,AUDI_OPENDATE,AUDI_FORMID,AUDI_LEVEL,AUDI_RID)"+
            //				" values ((select nvl(max(AUDI_NO),0)+1 from AUDITING_INFO),?,?,?,?,?,?)";
            //			lngRet=new  com.digitalwave.iCare.middletier.HRPService.clsHRPTableService().lngExecuteParameterSQL(Sql,ref lngAffecdet,objDPArrInsert);
            //
            //			return lngRet; 
        }

        /// <summary>
        /// ��ȡԱ������
        /// </summary>
        /// <param name="p_strUserId"></param>
        /// <param name="p_strLevel"></param>
        /// <param name="p_enmFormType"></param>
        public void m_mthGetUserLevel(string p_strUserId, ref string p_strLevel, enmApproveType p_enmFormType)
        {
            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.lngGetUserLevel(p_strUserId, ref p_strLevel, (int)p_enmFormType);
            }
            finally
            {
                //m_objService.Dispose();
            }
            //			string Sql="";
            //			long lngRet=0;
            //			System.Data.DataTable dtRecord=null;
            //
            //			System.Data.IDataParameter[] objDPArrInsert = null;//new Oracle.DataAccess.Client.OracleParameter[2];
            //
            ////			for(int i=0;i<objDPArrInsert.Length;i++)
            ////				objDPArrInsert[i]=new Oracle.DataAccess.Client.OracleParameter();
            //			new clsHRPTableService().CreateDatabaseParameter(2,out objDPArrInsert);
            //			objDPArrInsert[0].Value=p_strUserId;
            //			objDPArrInsert[1].Value= (int)p_enmFormType;
            //
            //			//Sql="select AUDL_LEVEL from AUDITING_LEVEL where AUDL_ROLEID in (select ROLE_ID from ROLE_EMPLOYEE where trim(EMPLOYEEID)=? and STATUS=0) and AUDL_TYPE=?";
            //			Sql="select AUDL_LEVEL from AUDITING_LEVEL where AUDL_ROLETYPEID in (select ROLETYPEID from ROLE_EMPLOYEE,ROLE_TYPE,ROLE_INFO where rtrim(EMPLOYEEID)=? and rtrim(ROLE_NAME)=rtrim(ROLETYPENAME) and ROLE_EMPLOYEE.ROLE_ID=ROLE_INFO.ROLE_ID and STATUS=0) and AUDL_TYPE=?";
            //
            //			lngRet=new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService().lngGetDataTableWithParameters(Sql,ref dtRecord,objDPArrInsert);
            //
            //			if(dtRecord!=null && dtRecord.Rows.Count>0)
            //			{
            //				p_strLevel=dtRecord.Rows[0][0].ToString();
            //			}
            //			else
            //				p_strLevel="0";
        }

        /// <summary>
        /// �ж�Ա�����еĽ�ɫ��ָ����ɫp_strRoleName��ͬ
        /// </summary>
        /// <param name="p_strEmployeeID"></param>
        /// <param name="p_strRoleName"></param>
        /// <returns></returns>
        public bool blnEmployeeMatchingRole(string p_strEmployeeID, string p_strRoleName)
        {
            string[] strEmployeeRoleArry;
            bool blnEmployeeMatchingRole = false;

            //clsApprove_FlowService m_objService =
            //    (clsApprove_FlowService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsApprove_FlowService));

            try
            {
                strEmployeeRoleArry = (new weCare.Proxy.ProxyEmr01()).Service.strGetEmployeeRole(p_strEmployeeID);

                if (strEmployeeRoleArry != null || strEmployeeRoleArry.Length > 0)
                {
                    for (int i = 0; i < strEmployeeRoleArry.Length; i++)
                    {
                        if (strEmployeeRoleArry[i].Trim() == p_strRoleName.Trim())
                        {
                            blnEmployeeMatchingRole = true;
                            break;
                        }
                    }
                }
            }
            finally
            {
                //m_objService.Dispose();
            }
            return blnEmployeeMatchingRole;
        }
    }

    public enum enmApproveType
    {
        /// <summary>
        /// ��
        /// </summary>
        none = 0,
        /// <summary>
        /// ������
        /// </summary>
        CaseHistory,
        /// <summary>
        /// ��ʿ��
        /// </summary>
        Nurses
    }

}
