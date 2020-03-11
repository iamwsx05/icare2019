using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity; 

namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// ���������¼�����м���ĸ��ࡣ
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public abstract class clsRecordsService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		/// <summary>
		///  ��ȡָ����¼�����ݡ�
		/// </summary>
        /// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_objTansDataInfoArr">���صļ�¼����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTransDataInfoArr(
			string p_strInPatientID,
			string p_strInPatientDate,
			out clsTransDataInfo[] p_objTansDataInfoArr)
		{
			p_objTansDataInfoArr = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetTransDataInfoArr");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //�������                     
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngGetTransDataInfoArrWithServ(p_strInPatientID, p_strInPatientDate, objHRPServ, out p_objTansDataInfoArr);

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
        #region ����
        /// <summary>
        ///  ��ȡָ����¼�����ݣ���ɽ�������ų��Ļ�����ã���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strFormID">��ID</param>
        /// <param name="p_objTansDataInfoArr">���صļ�¼����</param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngGetTransDataInfoArr(
        //    string p_strInPatientID,
        //    string p_strInPatientDate,string p_strFormID,
        //    out clsTransDataInfo[] p_objTansDataInfoArr)
        //{
        //    p_objTansDataInfoArr = null;
        //    long lngRes = 0;
        //    clsHRPTableService objHRPServ = new clsHRPTableService();
        //    try
        //    {
        //        //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetTransDataInfoArr");
        //        //if (lngCheckRes <= 0)
        //        //return lngCheckRes;

        //        //�������                     
        //        if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
        //            return (long)enmOperationResult.Parameter_Error;

        //        lngRes = m_lngGetTransDataInfoArrWithServ(p_strInPatientID, p_strInPatientDate,p_strFormID, objHRPServ, out p_objTansDataInfoArr);

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
        //    //����
        //    return lngRes;
        //}
        #endregion
        /// <summary>
        ///  ��ȡָ����¼������,ʹ��סԺ�ǼǺ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_objTansDataInfoArr">���صļ�¼����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTransDataInfoArr(
            string p_strRegisterId,out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            p_objTansDataInfoArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetTransDataInfoArr");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //�������                     
                if (string.IsNullOrEmpty(p_strRegisterId))
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngGetTransDataInfoArrWithServ(p_strRegisterId,1, out p_objTansDataInfoArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPServ = null;
                //objHRPServ.Dispose();
            }
            //����
            return lngRes;
        }


		/// <summary>
		/// ��ȡָ����¼������
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_objHRPServ"></param>
		/// <param name="p_objTansDataInfoArr">���صļ�¼����</param>
		/// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            p_objTansDataInfoArr = null;
            return 0;
        }
        #region ����
        /// <summary>
        /// ��ȡָ����¼�����ݣ���ɽ�������ų��Ļ�����ã�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strFormID">��ID</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objTansDataInfoArr">���صļ�¼����</param>
        /// <returns></returns>
        //[AutoComplete]
        //protected virtual long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
        //    string p_strInPatientDate,string p_strFormID,
        //    clsHRPTableService p_objHRPServ,
        //    out clsTransDataInfo[] p_objTansDataInfoArr)
        //{
        //    p_objTansDataInfoArr = null;
        //    return 0;
        //}
        #endregion
        /// <summary>
        /// ��ȡָ����¼������,ʹ��סԺ�ǼǺ�
        /// </summary>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strStatus">״̬0�����ϼ�¼��1��������¼</param>
        /// <param name="p_objTansDataInfoArr">���صļ�¼����</param>
        /// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetTransDataInfoArrWithServ(string p_strRegisterId,
            int p_intStatus,
            out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            p_objTansDataInfoArr = null;
            return 0;
        }


		/// <summary>
		///  �鿴��ǰ��¼�Ƿ����µļ�¼��
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngCheckLastModifyRecord(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo);


		/// <summary>
		/// ɾ����¼��
		/// 1.���� HRPServ��
		/// 2.ɾ����¼��(m_lngDeleteRecordWithServ)
		/// </summary>
		/// <param name="p_intRecordType">��ǰҪɾ���ļ�¼</param>
		/// <param name="p_objRecordContent">��ǰ��¼����</param>
		/// <param name="p_objModifyInfo">����ǰҪɾ���ļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecord(
			int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //�������                     
                if (p_intRecordType < 0 || p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;
                lngRes = m_lngDeleteRecordWithServ(p_intRecordType, p_objRecordContent, objHRPServ, out p_objModifyInfo);

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
		/// ɾ����¼��
		/// 1.�鿴��ǰ��¼�Ƿ����µļ�¼����m_lngCheckLastModifyRecord��
		/// 2.�Ѽ�¼�������С�ɾ������(m_lngDeleteRecord2DB)
		/// </summary>
		/// <param name="p_intRecordType">��ǰҪɾ���ļ�¼</param>
		/// <param name="p_objRecordContent">��ǰ��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">����ǰҪɾ���ļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngDeleteRecordWithServ(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo = null;
			long lngRes = 0;
			try
			{
				if(p_intRecordType < 0 || p_objRecordContent == null || p_objHRPServ == null)
					return (long)enmOperationResult.Parameter_Error;
			
				lngRes = m_lngCheckLastModifyRecord(p_intRecordType,p_objRecordContent,p_objHRPServ,out p_objModifyInfo);           
			
				if(lngRes <= 0)
					return lngRes;                     
			
				lngRes= m_lngDeleteRecord2DB(p_intRecordType,p_objRecordContent,p_objHRPServ);

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//����
			return lngRes;
		}

		/// <summary>
		///  �Ѽ�¼�������С�ɾ������
		/// </summary>
		/// <param name="p_intRecordType">��ǰҪɾ���ļ�¼</param>
		/// <param name="p_objRecordContent">��ǰ��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngDeleteRecord2DB(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ);

		/// <summary>
		/// ��ȡ��ӡ��Ϣ��
		/// 1.��ȡ��ӡ���ݣ�����������p_dtmModifyDate�������µ�ModifyDate��������� p_objContent
		///   �������µ����ݣ������������Ϊnull��
		/// 2.��ȡ��ӡʱ�䣺������� p_dtmFirstPrintDate ����״δ�ӡʱ�䡣p_blnIsFirstPrint���
		///   �Ƿ��״δ�ӡ�������Ϊtrue���ͻ����ڴ�ӡ����Ҫ����p_dtmFirstPrintDate�����ݿ⡣
		/// </summary>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_objTransDataInfoArr"></param>
		/// <param name="p_dtmFirstPrintDateArr"></param>
		/// <param name="p_blnIsFirstPrintArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPrintInfo(
			string p_strInPatientID,
			string p_strInPatientDate,
			out clsTransDataInfo[] p_objTransDataInfoArr,
			out DateTime[] p_dtmFirstPrintDateArr,
			out bool[] p_blnIsFirstPrintArr)
		{
			p_objTransDataInfoArr = null;
			p_dtmFirstPrintDateArr = null;        
			p_blnIsFirstPrintArr = null;
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetPrintInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //�������                     
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                 lngRes = m_lngGetTransDataInfoArrWithServ(p_strInPatientID, p_strInPatientDate, objHRP, out p_objTransDataInfoArr);

                if (lngRes <= 0)
                    return lngRes;

                p_dtmFirstPrintDateArr = new DateTime[p_objTransDataInfoArr.Length];
                p_blnIsFirstPrintArr = new bool[p_objTransDataInfoArr.Length];

                for (int i = 0; i < p_objTransDataInfoArr.Length; i++)
                {
                    //�ж�p_objTransDataInfoArr[i].m_dtmFirstPrintDate�Ƿ�ΪDateTime.MinValue
                    //�����
                    if (p_objTransDataInfoArr[i].m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue)
                    {
                        p_dtmFirstPrintDateArr[i] = DateTime.Now;
                        p_blnIsFirstPrintArr[i] = true;
                    }
                    else
                    {
                        //�������
                        p_dtmFirstPrintDateArr[i] = p_objTransDataInfoArr[i].m_objRecordContent.m_dtmFirstPrintDate;
                        p_blnIsFirstPrintArr[i] = false;
                    }
                }


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
			//����
			return lngRes;
		}
        /// <summary>
        /// ��ȡ��ӡ��Ϣ������סԺ�ǼǺš�
        /// 1.��ȡ��ӡ���ݣ�����������p_dtmModifyDate�������µ�ModifyDate��������� p_objContent
        ///   �������µ����ݣ������������Ϊnull��
        /// 2.��ȡ��ӡʱ�䣺������� p_dtmFirstPrintDate ����״δ�ӡʱ�䡣p_blnIsFirstPrint���
        ///   �Ƿ��״δ�ӡ�������Ϊtrue���ͻ����ڴ�ӡ����Ҫ����p_dtmFirstPrintDate�����ݿ⡣
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="p_strStatus"></param>
        /// <param name="p_objTransDataInfoArr"></param>
        /// <param name="p_dtmFirstPrintDateArr"></param>
        /// <param name="p_blnIsFirstPrintArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrintInfo(
            string p_strRegisterId,int p_intStatus,
            out clsTransDataInfo[] p_objTransDataInfoArr,
            out DateTime[] p_dtmFirstPrintDateArr,
            out bool[] p_blnIsFirstPrintArr)
        {
            p_objTransDataInfoArr = null;
            p_dtmFirstPrintDateArr = null;
            p_blnIsFirstPrintArr = null;
            long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetPrintInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //�������                     
                if (string.IsNullOrEmpty(p_strRegisterId))
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngGetTransDataInfoArrWithServ(p_strRegisterId, p_intStatus, out p_objTransDataInfoArr);

                if (lngRes <= 0)
                    return lngRes;

                p_dtmFirstPrintDateArr = new DateTime[p_objTransDataInfoArr.Length];
                p_blnIsFirstPrintArr = new bool[p_objTransDataInfoArr.Length];

                for (int i = 0 ; i < p_objTransDataInfoArr.Length ; i++)
                {
                    //�ж�p_objTransDataInfoArr[i].m_dtmFirstPrintDate�Ƿ�ΪDateTime.MinValue
                    //�����
                    if (p_objTransDataInfoArr[i].m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue)
                    {
                        p_dtmFirstPrintDateArr[i] = DateTime.Now;
                        p_blnIsFirstPrintArr[i] = true;
                    }
                    else
                    {
                        //�������
                        p_dtmFirstPrintDateArr[i] = p_objTransDataInfoArr[i].m_objRecordContent.m_dtmFirstPrintDate;
                        p_blnIsFirstPrintArr[i] = false;
                    }
                }


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
            //����
            return lngRes;
        }

		/// <summary>
		/// �������ݿ��е��״δ�ӡʱ�䡣
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_intRecordTypeArr"></param>
		/// <param name="p_dtmOpenDateArr"></param>
		/// <param name="p_dtmFirstPrintDate"></param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            int[] p_intRecordTypeArr,
            DateTime[] p_dtmOpenDateArr,
            DateTime p_dtmFirstPrintDate)
        {
            return 0;
        }
        /// <summary>
        /// �������ݿ��е��״δ�ӡʱ��,����סԺ�ǼǺš�
        /// </summary>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreatedDate">��������</param>
        /// <param name="p_strStatus"></param>
        /// <param name="p_intRecordTypeArr"></param>
        /// <param name="p_dtmOpenDateArr"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
            int[] p_intRecordTypeArr,
            DateTime[] p_dtmCreatedDateArr,
            DateTime p_dtmFirstPrintDate)
        {
            return 0;
        }

        /// <summary>
        /// ��ȡһ��סԺȫ�����ϼ�¼
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public virtual long m_lngGetAllInactiveInfo(string p_strSQL, string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            p_objInactiveRecordInfoArr = null;
            if (string.IsNullOrEmpty(p_strInpatientId) || p_dtmInpatientDate == DateTime.MinValue || string.IsNullOrEmpty(p_strSQL)) return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                DataTable dtExist = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(p_strSQL, ref dtExist, objDPArr);
                if (lngRes > 0 && dtExist.Rows.Count > 0)
                {
                    p_objInactiveRecordInfoArr = new clsInactiveRecordInfo_VO[dtExist.Rows.Count];
                    DataRow objRow = null;
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtExist.Rows.Count; i++)
                    {
                        objRow = dtExist.Rows[i];
                        clsInactiveRecordInfo_VO objInfo = new clsInactiveRecordInfo_VO();
                        objInfo.m_StrInpatientId = p_strInpatientId;
                        objInfo.m_DtmInpatientDate = p_dtmInpatientDate;
                        if (DateTime.TryParse(objRow["createdate"].ToString(), out dtmTemp))
                            objInfo.m_DtmCreatedDate = dtmTemp;
                        if (DateTime.TryParse(objRow["opendate"].ToString(), out dtmTemp))
                            objInfo.m_DtmOpenDate = dtmTemp;
                        if (DateTime.TryParse(objRow["deactiveddate"].ToString(), out dtmTemp))
                            objInfo.m_DtmDeactiveDate = dtmTemp;
                        objInfo.m_StrCreatedUser = objRow["createdusername"].ToString();
                        objInfo.m_StrDeactiveUser = objRow["deactiveusername"].ToString();
                        p_objInactiveRecordInfoArr[i] = objInfo;
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

        /// <summary>
        /// ת����Ϣ��ѯ
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_intdetpID"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetTransferInfo(
                  string p_strRegisterID, string p_intdetpID, out DateTime[] p_objModifyInfo)
        {
            p_objModifyInfo = null;
            return 0;
        }
	}
}
