using System.EnterpriseServices;
using System;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;
using System.Data;

namespace com.digitalwave.BaseCaseHistorySevice
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public abstract class clsBaseCaseHistorySevice : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsBaseCaseHistorySevice()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // ��ȡ���˵ĸü�¼ʱ���б�
        [AutoComplete]
        public abstract long m_lngGetRecordTimeList(string p_strInPatientID,
                                                     out string[] p_strInPatientDateArr,
                                                     out string[] p_strCreateRecordTimeArr,
                                                     out string[] p_strOpenRecordTimeArr);

        // ��ȡָ����¼�����ݡ�
        [AutoComplete]
        public long m_lngGetRecordContent(string p_strInPatientID,
                                           string p_strInPatientDate,
                                           /* string p_strOpenRecordTime, */
                                           out clsBaseCaseHistoryInfo p_objRecordContent,
                                           out clsPictureBoxValue[] p_objPicValueArr)
        {
            p_objRecordContent = null;
            p_objPicValueArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngGetRecordContent");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" /* || p_strOpenRecordTime==null || p_strOpenRecordTime=="" */)
                    return (long)enmOperationResult.Parameter_Error;


                lngRes = m_lngGetRecordContentWithServ(p_strInPatientID, p_strInPatientDate,/*p_strOpenRecordTime,*/objHRPServ, out p_objRecordContent, out p_objPicValueArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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

        // ����¼�¼��
        // 1.���� HRPServ��
        // 2.����¼�¼��(m_lngAddNewRecordWithServ)
        [AutoComplete]
        public long m_lngAddNewRecord(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngAddNewRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                lngRes = m_lngAddNewRecordWithServ(p_objRecordContent, p_objPicValueArr, p_strDiseaseID, p_strDeptID, objHRPServ, out p_objModifyInfo);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;


        }

        // �޸ļ�¼��
        // 1.���� HRPServ��
        // 2.�޸ļ�¼��(m_lngModifyRecordWithServ)
        [AutoComplete]
        public long m_lngModifyRecord(clsBaseCaseHistoryInfo p_objOldRecordContent,
            clsBaseCaseHistoryInfo p_objNewRecordContent, clsPictureBoxValue[] p_objPicValueArr,
            string p_strDiseaseID, string p_strDeptID,
            out clsPreModifyInfo p_objPreModifyInfo)
        {
            p_objPreModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngModifyRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;


                lngRes = m_lngModifyRecordWithServ(p_objOldRecordContent, p_objNewRecordContent, objHRPServ, p_objPicValueArr, p_strDiseaseID, p_strDeptID, out p_objPreModifyInfo);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;


        }

        // ɾ����¼��
        // 1.���� HRPServ��
        // 2.ɾ����¼��(m_lngDeleteRecordWithServ)
        [AutoComplete]
        public long m_lngDeleteRecord(clsBaseCaseHistoryInfo p_objRecordContent,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                lngRes = m_lngDeleteRecordWithServ(p_objRecordContent, objHRPServ, out p_objModifyInfo);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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

        #region �ɵ���������
        // ����������¼��
        // 1.���� HRPServ��
        // 2.����Ƿ����ɾ����¼����Ӽ�¼��
        // 3.ɾ����¼��
        // 4.��Ӽ�¼��
        [AutoComplete]
        public long m_lngReAddNewRecord(clsBaseCaseHistoryInfo p_objDelRecord,
                                     clsBaseCaseHistoryInfo p_objAddNewRecord,
                                     out clsPreModifyInfo p_objPreModifyInfo)
        {
            p_objPreModifyInfo = null;

            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngReAddNewRecord");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                lngRes = m_lngCheckLastModifyRecord(p_objDelRecord, objHRPServ, out p_objPreModifyInfo);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = m_lngDeleteRecord2DB(p_objDelRecord, objHRPServ);

                if (lngRes <= 0)
                    return lngRes;

                //		return m_lngAddNewRecord2DB(p_objAddNewRecord,objHRP); 
                return 1;

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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

        // ��ȡ��ӡ��Ϣ��
        // 1.��ȡ��ӡ���ݣ�����������p_dtmModifyDate�������µ�ModifyDate��������� p_objContent
        //   �������µ����ݣ������������Ϊnull��
        // 2.��ȡ��ӡʱ�䣺������� p_dtmFirstPrintDate ����״δ�ӡʱ�䡣p_blnIsFirstPrint���
        //   �Ƿ��״δ�ӡ�������Ϊtrue���ͻ����ڴ�ӡ����Ҫ����p_dtmFirstPrintDate�����ݿ⡣
        //   
        // 
        [AutoComplete]
        public long m_lngGetPrintInfo(string p_strInPatientID,
                string p_strInPatientDate,
                /*string p_strOpenRecordTime,*/DateTime p_dtmModifyDate,
                                   out clsBaseCaseHistoryInfo p_objContent,
                                   out clsPictureBoxValue[] p_objPicValueArr,
                                   out DateTime p_dtmFirstPrintDate,
                                   out bool p_blnIsFirstPrint)
        {
            //�������                     

            p_objContent = null;
            p_objPicValueArr = null;
            p_dtmFirstPrintDate = DateTime.MinValue;
            p_blnIsFirstPrint = false;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngGetPrintInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;



                //��ȡʱ��
                DateTime dtmModifyDate;
                string strFirstPrintDate;
                lngRes = m_lngGetModifyDateAndFirstPrintDate(p_strInPatientID, p_strInPatientDate,/*p_strOpenRecordTime,*/objHRPServ, out dtmModifyDate, out strFirstPrintDate);

                if (lngRes <= 0)
                    return lngRes;

                //�ж�dtmModifyDate��p_dtmModifyDate�Ƿ�һ��
                if (p_dtmModifyDate != dtmModifyDate)
                //�����һ��
                {
                    lngRes = m_lngGetRecordContentWithServ(p_strInPatientID, p_strInPatientDate,/*p_strOpenRecordTime,*/objHRPServ, out p_objContent, out p_objPicValueArr);
                    if (lngRes <= 0)
                        return lngRes;
                }
                //�ж�strFirstPrintDate�Ƿ�Ϊnull���߲�Ϊnull��Ϊ��ֵ
                if (strFirstPrintDate == null || strFirstPrintDate == "")
                {//�����
                    p_dtmFirstPrintDate = DateTime.Now;
                    p_blnIsFirstPrint = true;
                }
                else
                {//�������
                    p_dtmFirstPrintDate = DateTime.Parse(strFirstPrintDate);
                    p_blnIsFirstPrint = false;
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

        // �������ݿ��е��״δ�ӡʱ�䡣
        [AutoComplete]
        public abstract long m_lngUpdateFirstPrintDate(
                    string p_strInPatientID,
                    string p_strInPatientDate,
                    string p_strOpenDate,
                    DateTime p_dtmFirstPrintDate);

        // ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        [AutoComplete]
        public abstract long m_lngGetDeleteRecordTimeList(
                string p_strInPatientID,
                                                           string p_strInPatientDate,
                                                           string p_strDeleteUserID,
                                                           out string[] p_strCreateRecordTimeArr,
                                                           out string[] p_strOpenRecordTimeArr);

        // ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        [AutoComplete]
        public abstract long m_lngGetDeleteRecordTimeListAll(
                                                                string p_strInPatientID,
                                                              string p_strInPatientDate,
                                                              out string[] p_strCreateRecordTimeArr,
                                                              out string[] p_strOpenRecordTimeArr);

        // ��ȡָ���Ѿ���ɾ����¼�����ݡ�
        [AutoComplete]
        public long m_lngGetDeleteRecordContent(string p_strInPatientID,
                                                 string p_strInPatientDate,
                                                 string p_strOpenRecordTime,
                                                 out clsBaseCaseHistoryInfo p_objRecordContent)
        {
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBaseCaseHistorySevice", "m_lngGetDeleteRecordContent");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;


                lngRes = m_lngGetDeleteRecordContentWithServ(p_strInPatientID, p_strInPatientDate, p_strOpenRecordTime, objHRPServ, out p_objRecordContent);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }           //����
            return lngRes;


        }

        // ��ȡָ����¼�����ݡ�
        [AutoComplete]
        protected abstract long m_lngGetRecordContentWithServ(
                                                                string p_strInPatientID,
                                                               string p_strInPatientDate,
                                                               /*string p_strOpenRecordTime,*/
                                                               clsHRPTableService p_objHRPServ,
                                                               out clsBaseCaseHistoryInfo p_objRecordContent,
                                                                out clsPictureBoxValue[] p_objPicValueArr);

        // ����¼�¼��
        // 1.�Ȳ鿴�Ƿ�����ͬ�ļ�¼ʱ�䡣��m_lngCheckCreateDate��
        // 2.��Ӽ�¼�����ݿ⡣(m_lngAddNewRecord2DB)
        [AutoComplete]
        protected long m_lngAddNewRecordWithServ(clsBaseCaseHistoryInfo p_objRecordContent,
                                                    clsPictureBoxValue[] p_objPicValueArr,
                                                    string p_strDiseaseID, string p_strDeptID,
                                                  clsHRPTableService p_objHRPServ,
                                                  out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            try
            {

                if (p_objRecordContent == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                //m_mthDeleteAlreadyExistRecord(p_objRecordContent,p_objHRPServ);

                lngRes = m_lngAddNewRecord2DB(p_objRecordContent, p_objPicValueArr, p_strDiseaseID, p_strDeptID, p_objHRPServ);

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

        /// <summary>
        /// ɾ����ͬһʱ�������ɵļ�¼
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        [AutoComplete]
        private void m_mthDeleteAlreadyExistRecord(clsBaseCaseHistoryInfo p_objRecordContent, clsHRPTableService p_objHRPServ)
        {
            //long lngRes = 0;
            try
            {
                string strSql = @"update inpatientcasehistory_history set status = 1 where 
				inpatientid = ? and inpatientdate = ? and status = 0";

                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;

                long lngEff = -1;
                p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }


        }

        // �鿴�Ƿ�����ͬ�ļ�¼ʱ��
        [AutoComplete]
        protected abstract long m_lngCheckCreateDate(clsBaseCaseHistoryInfo p_objRecordContent,
                                                      clsHRPTableService p_objHRPServ,
                                                      out clsPreModifyInfo p_objPreModifyInfo);

        // �����¼�����ݿ⡣
        [AutoComplete]
        protected abstract long m_lngAddNewRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
                                                        clsPictureBoxValue[] p_objPicValueArr,
                                                        string p_strDiseaseID, string p_strDeptID,
                                                      clsHRPTableService p_objHRPServ);

        // �޸ļ�¼��
        // 1.�鿴��ǰ��¼�Ƿ����µļ�¼����m_lngCheckLastModifyRecord��
        // 2.�����޸ĵ����ݱ��浽���ݿ⡣(m_lngModifyRecord2DB)
        [AutoComplete]
        protected long m_lngModifyRecordWithServ(clsBaseCaseHistoryInfo p_objOldRecordContent,
                                              clsBaseCaseHistoryInfo p_objNewRecordContent,
                                              clsHRPTableService p_objHRPServ,
                                              clsPictureBoxValue[] p_objPicValueArr,
                                              string p_strDiseaseID, string p_strDeptID,
                                              out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            if (p_objNewRecordContent == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            try
            {
                lngRes = m_lngCheckLastModifyRecord(p_objOldRecordContent, p_objHRPServ, out p_objModifyInfo);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = m_lngModifyRecord2DB(p_objNewRecordContent, p_objPicValueArr, p_strDiseaseID, p_strDeptID, p_objHRPServ);

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

        // �鿴��ǰ��¼�Ƿ����µļ�¼��
        [AutoComplete]
        protected abstract long m_lngCheckLastModifyRecord(clsBaseCaseHistoryInfo p_objRecordContent,
                    clsHRPTableService p_objHRPServ,
                    out clsPreModifyInfo p_objModifyInfo);


        // �����޸ĵ����ݱ��浽���ݿ⡣
        [AutoComplete]
        protected abstract long m_lngModifyRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
                                                      clsPictureBoxValue[] p_objPicValueArr,
                                                      string p_strDiseaseID, string p_strDeptID,
                                                      clsHRPTableService p_objHRPServ);

        // ɾ����¼��
        // 1.�鿴��ǰ��¼�Ƿ����µļ�¼����m_lngCheckLastModifyRecord��
        // 2.�Ѽ�¼�������С�ɾ������(m_lngDeleteRecord2DB)
        [AutoComplete]
        protected long m_lngDeleteRecordWithServ(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            try
            {
                if (p_objRecordContent == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngCheckLastModifyRecord(p_objRecordContent, p_objHRPServ, out p_objModifyInfo);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = m_lngDeleteRecord2DB(p_objRecordContent, p_objHRPServ);

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

        // �Ѽ�¼�������С�ɾ������
        [AutoComplete]
        protected abstract long m_lngDeleteRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
                                                  clsHRPTableService p_objHRPServ);

        // ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        [AutoComplete]
        protected abstract long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,/*string p_strOpenRecordTime,*/clsHRPTableService p_objHRPServ,
                                                             out DateTime p_dtmModifyDate,
                                                             out string p_strFirstPrintDate);

        // ��ȡָ���Ѿ���ɾ����¼�����ݡ�
        [AutoComplete]
        protected abstract long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
                                                                 string p_strInPatientDate,
                                                                 string p_strOpenRecordTime,
                                                                 clsHRPTableService p_objHRPServ,
                                                                 out clsBaseCaseHistoryInfo p_objRecordContent);

    }
}
