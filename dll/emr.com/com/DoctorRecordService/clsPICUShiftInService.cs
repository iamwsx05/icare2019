using System;
using System.EnterpriseServices;
using System.Data;
using System.Text;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.PublicMiddleTier;
using weCare.Core.Entity;


namespace com.digitalwave.PICUShiftService
{

    /// <summary>
    /// �������ϣ���picת���¼
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPICUShiftInService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        private const string c_strCheckLastModifyRecordSQL = @"select b.modifydate,b.modifyuserid from picushiftinrecord a,picushiftinrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.createdate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.createdate=a.createdate and
						b.modifydate=(select max(modifydate) from picushiftinrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and createdate=a.createdate)";

        /// <summary>
        /// ��ConsultationRecord��ȡɾ��������Ҫ��Ϣ��
        /// </summary>
        private const string c_strGetDeleteRecordSQL = "select deactiveddate,deactivedoperatorid from picushiftinrecord where inpatientid = ? and inpatientdate= ? and createdate= ? and status=1 ";

        /// <summary>
        /// ����ConsultationRecord��ɾ����¼����Ϣ
        /// </summary>
        private const string c_strDeleteRecordSQL = "update picushiftinrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and createdate=? and status=0";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strResultXml">���صĽ��</param>
        /// <param name="p_intResultRows">��¼������</param>
        /// <returns>
        /// ���������
        /// 0��ʧ�ܡ�
        /// 1���ɹ���
        /// </returns>
        [AutoComplete]
        public long m_lngGetPatientShiftInfo(
                string p_strInPatientID, string p_strInPatientDate, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPICUShiftInService", "m_lngGetPatientShiftInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                string strSQL = @"select createdate 
									from picushiftinrecord
									where inpatientid = ?
									and inpatientdate = ?
									and status=0 
									order by createdate desc";

                clsHRPTableService m_objServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                m_objServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = m_objServ.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strResultXml">���صĽ��</param>
        /// <param name="p_intResultRows">��¼������</param>
        /// <returns>
        /// ���������
        /// 0��ʧ�ܡ�
        /// 1���ɹ���
        /// </returns>
        [AutoComplete]
        public long m_lngGetPICUShiftInfo(
                string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPICUShiftInService", "m_lngGetPICUShiftInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                string strSQL = @"select main.createid,
       content.inpatientid,
       content.inpatientdate,
       content.createdate,
       content.modifydate,
       content.modifyuserid,
       content.turnfromdeptid,
       content.picudoctorid,
       content.indiagnose,
       content.operationname,
       content.anaesthesiatype,
       content.turndiagnose,
       content.indiagnosecourse,
       content.temperature,
       content.heartrate,
       content.pulse,
       content.systolic,
       content.diastolic,
       content.mind,
       content.pupildiameterright,
       content.pupildiameterleft,
       content.pupilreflectionright,
       content.pupilreflectionleft,
       content.glasgowvalue,
       content.glasgowopeneye,
       content.glasgowlanguage,
       content.glasgowsport,
       content.other,
       content.rbc,
       content.wbc,
       content.plt,
       content.lymphocyte,
       content.bandleukocyte,
       content.dispartleftleukocyte,
       content.monocyte,
       content.acidophil,
       content.basophil,
       content.bloodk,
       content.bloodna,
       content.bloodcl,
       content.bloodsugar,
       content.bun,
       content.bloodca,
       content.ph,
       content.pao2,
       content.paco2,
       content.be,
       content.hco3,
       content.woundinfo
  from picushiftinrecord main, picushiftinrecordcontent content
 where main.inpatientid = content.inpatientid
   and main.inpatientdate = content.inpatientdate
   and main.createdate = content.createdate
   and main.inpatientid = ?
   and main.inpatientdate = ?
   and main.createdate = ?
   and main.status = 0
   and content.modifydate = (select max(modifydate)
                               from picushiftinrecordcontent
                              where inpatientid = ?
                                and inpatientdate = ?
                                and createdate = ?)";

                clsHRPTableService m_objServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                m_objServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strCreateDate);

                lngRes = m_objServ.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;

        }
        [AutoComplete]
        public long m_lngGetDeletedPICUShiftInfo(
                    string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, ref string p_strResultXml, ref int p_intResultRows)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPICUShiftInService", "m_lngGetDeletedPICUShiftInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                string strSQL = @"select main.createid,
       content.inpatientid,
       content.inpatientdate,
       content.createdate,
       content.modifydate,
       content.modifyuserid,
       content.turnfromdeptid,
       content.picudoctorid,
       content.indiagnose,
       content.operationname,
       content.anaesthesiatype,
       content.turndiagnose,
       content.indiagnosecourse,
       content.temperature,
       content.heartrate,
       content.pulse,
       content.systolic,
       content.diastolic,
       content.mind,
       content.pupildiameterright,
       content.pupildiameterleft,
       content.pupilreflectionright,
       content.pupilreflectionleft,
       content.glasgowvalue,
       content.glasgowopeneye,
       content.glasgowlanguage,
       content.glasgowsport,
       content.other,
       content.hb,
       content.rbc,
       content.wbc,
       content.plt,
       content.lymphocyte,
       content.bandleukocyte,
       content.dispartleftleukocyte,
       content.monocyte,
       content.acidophil,
       content.basophil,
       content.bloodk,
       content.bloodna,
       content.bloodcl,
       content.bloodsugar,
       content.bun,
       content.bloodca,
       content.ph,
       content.pao2,
       content.paco2,
       content.be,
       content.hco3,
       content.woundinfo
  from picushiftinrecord main, picushiftinrecordcontent content
 where main.inpatientid = content.inpatientid
   and main.inpatientdate = content.inpatientdate
   and main.createdate = content.createdate
   and main.inpatientid = ?
   and main.inpatientdate = ?
   and main.createdate = ?
   and main.status = '1'
   and content.modifydate = (select max(modifydate)
                               from picushiftinrecordcontent
                              where inpatientid = ?
                                and inpatientdate = ?
                                and createdate = ?)";

                clsHRPTableService m_objServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                m_objServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strCreateDate);

                lngRes = m_objServ.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;

        }
        #region AddNew
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strMainXml"></param>
        /// <returns>
        /// ���������
        /// 0��ʧ�ܡ�
        /// 1���ɹ���
        /// </returns>
        [AutoComplete]
        public long m_lngAddNew(
                string p_strMainXml, string p_strContentXml)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPICUShiftInService", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                lngRes = objHRPServ.add_new_record("PICUShiftInRecord", p_strMainXml);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = objHRPServ.add_new_record("PICUShiftInRecordContent", p_strContentXml);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;

        }
        #endregion AddNew

        #region Modify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strMainXml"></param>
        /// <returns>
        /// ���������
        /// 0��ʧ�ܡ�
        /// 1���ɹ���
        /// </returns>
        [AutoComplete]
        public long m_lngModify(
            string p_strMainXml, string p_strContentXml)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPICUShiftInService", "m_lngModify");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                lngRes = 1;//objHRPServ.add_new_record("PICUShiftInRecord",p_strMainXml);			

                if (lngRes <= 0)
                    return lngRes;

                lngRes = objHRPServ.add_new_record("PICUShiftInRecordContent", p_strContentXml);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;

        }
        #endregion Modify

        #region Delete
        /// <summary>
        /// ɾ����¼��
        /// 1.���� HRPServ��
        /// 2.ɾ����¼��(m_lngDeleteRecordWithServ)
        /// </summary>
        /// <param name="p_objRecordContent">��ǰҪɾ���ļ�¼</param>	
        /// <param name="p_objModifyInfo">����ǰҪɾ���ļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord(
                clsTrackRecordContent p_objRecordContent,
                out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPICUShiftInService", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngCheckLastModifyRecord(p_objRecordContent, objHRPServ, out p_objModifyInfo);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = m_lngDeleteRecord2DB(p_objRecordContent, objHRPServ);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
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
        protected long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
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

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from DeadRecord Where InPatientID = ? and InPatientDate= ? and CreateDate= ? and Status=1 ";
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

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;


        }

        /// <summary>
        /// �Ѽ�¼�������С�ɾ������
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
                clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
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
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;


        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_blnCheckResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckNewCreateDate(
                string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out bool p_blnIsAddNew)
        {
            p_blnIsAddNew = false;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPICUShiftInService", "m_lngCheckNewCreateDate");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;
                clsHRPTableService m_objServ = new clsHRPTableService();
                string strSQL = @"select inpatientid,
       inpatientdate,
       createdate,
       createid,
       ifconfirm,
       confirmreason,
       confirmreasonxmlstring,
       status,
       deactiveddate,
       deactivedoperatorid
  from picushiftinrecord
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and status = 0";

                DataTable dtResult = new DataTable();
                IDataParameter[] objDPArr = null;
                m_objServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = m_objServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                p_blnIsAddNew = false;

                if (lngRes <= 0)
                    return lngRes;
                else
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        p_blnIsAddNew = false;
                    }
                    else
                    {
                        p_blnIsAddNew = true;
                    }

                    return 1;
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
            //����
            return lngRes;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateDate"></param>
        /// <param name="p_blnCheckResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckLastModifyDate(
                string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, string p_strLastModifyDate, out bool p_blnIsLast)
        {
            p_blnIsLast = false;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPICUShiftInService", "m_lngCheckLastModifyDate");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                string strSQL = @"select max(modifydate) as lastmodifydate
									from picushiftinrecordcontent
									where inpatientid=?
									and inpatientdate= ? and createdate=?";

                DataTable dtResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                p_blnIsLast = false;

                if (lngRes <= 0)
                    return lngRes;
                else
                {
                    if (dtResult.Rows[0][0].ToString().Trim() != "")
                        if (DateTime.Parse(dtResult.Rows[0][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(p_strLastModifyDate).ToString("yyyy-MM-dd HH:mm:ss"))
                        {
                            p_blnIsLast = true;
                        }

                    return 1;
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
            //����
            return lngRes;


        }
    }
}
