using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity; 
using System.Data;

namespace com.digitalwave.DiseaseTrackService
{

	/// <summary>
	/// ���̼�¼�м���ĸ��ࡣ
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public abstract class clsDiseaseTrackService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		/// <summary>
		/// ��ȡ���˵ĸü�¼ʱ���б�
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
		/// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetRecordTimeList( string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr, out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            return 0;
        }
        /// <summary>
        /// ����סԺ�ǼǺŻ�ȡ���˵ĸü�¼ʱ���б�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreateDateArr">����ʱ������</param>
        /// <param name="p_strRecordDateArr">�����¼ʱ������</param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetRecordTimeList(  string p_strRegisterId,out string[] p_strCreateDateArr,out string[] p_strRecordDateArr)
        { 
            p_strCreateDateArr = null;
            p_strRecordDateArr = null;
            return 0;
        }

		/// <summary>
		/// ��ȡָ����¼������
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_objRecordContent">���صļ�¼����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent( string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenDate,
			out clsTrackRecordContent p_objRecordContent)
		{
			p_objRecordContent = null; 
			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngGetRecordContentWithServ(p_strInPatientID,p_strInPatientDate,p_strOpenDate,objHRP,out p_objRecordContent);
        }
        #region ����
        /// <summary>
        /// ��ȡָ����¼������(��ɽ�������ų��Ļ������)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_strFormID">��ID</param>
        /// <param name="p_objRecordContent">���صļ�¼����</param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngGetRecordContent(
        //    string p_strInPatientID,
        //    string p_strInPatientDate,
        //    string p_strOpenDate,string p_strFormID,
        //    out clsTrackRecordContent p_objRecordContent)
        //{
        //    p_objRecordContent = null;
        //    com.digitalwave.security.clsPrivilegeHandleService obj = new com.digitalwave.security.clsPrivilegeHandleService();
        //    long lngCheckRes = obj.m_lngCheckCallPrivilege(p_objPrincipal, "clsDiseaseTrackService", "m_lngGetRecordContent");
        //    //obj.Dispose();
        //    if (lngCheckRes <= 0)
        //        return lngCheckRes;

        //    clsHRPTableService objHRP = new clsHRPTableService();
        //    return m_lngGetRecordContentWithServ(p_strInPatientID, p_strInPatientDate, p_strOpenDate,p_strFormID, objHRP, out p_objRecordContent);
        //}
        #endregion
        /// <summary>
        ///  ����סԺ�ǼǺŻ�ȡָ����¼������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCteatedDate">����ʱ��</param>
        /// <param name="p_objRecordContent">���صļ�¼����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordContent( string p_strRegisterId,
            string p_strCteatedDate,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null; 
            clsHRPTableService objHRP = new clsHRPTableService();
            return m_lngGetRecordContentWithServ(p_strRegisterId, p_strCteatedDate,out p_objRecordContent);
        }
        /// <summary>
        /// ����סԺ�ǼǺŻ�ȡָ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCteatedDate">����ʱ��</param>
        /// <param name="p_objRecordContent">���صļ�¼����</param>
        /// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetRecordContentWithServ(string p_strRegisterId, string p_strCteatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            return 0;
        }

		/// <summary>
		/// ��ȡָ����¼�����ݡ�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContent">���صļ�¼����</param>
		/// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        { 
            p_objRecordContent = null;
            return 0;
        }
        #region ����
        /// <summary>
        /// ��ȡָ����¼������(��ɽ�������ų��Ļ������)��
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_strFormID">��ID</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">���صļ�¼����</param>
        /// <returns></returns>
        //[AutoComplete]
        //protected virtual long m_lngGetRecordContentWithServ(string p_strInPatientID,
        //    string p_strInPatientDate,
        //    string p_strOpenDate,
        //     string p_strFormID,
        //    clsHRPTableService p_objHRPServ,
        //    out clsTrackRecordContent p_objRecordContent)
        //{
        //    p_objRecordContent = null;
        //    return 0;
        //}
        #endregion
        /// <summary>
		///  ����¼�¼��
		/// 1.���� HRPServ��
		/// 2.����¼�¼��(m_lngAddNewRecordWithServ)
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objModifyInfo">��������ͬ�Ĵ���ʱ��,���ظü�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewRecord( clsTrackRecordContent p_objRecordContent,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo = null; 
			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngAddNewRecordWithServ(p_objRecordContent,objHRP,out p_objModifyInfo);
		}

		/// <summary>
		/// ����¼�¼��
		/// 1.�Ȳ鿴�Ƿ�����ͬ�ļ�¼ʱ�䡣��m_lngCheckCreateDate��
		/// 2.��Ӽ�¼�����ݿ⡣(m_lngAddNewRecord2DB)
		/// </summary>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">��������ͬ��¼,���ظü�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngAddNewRecordWithServ(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;
			
			if(p_objRecordContent == null || p_objHRPServ == null)
				return -1; 
		
			long lngRes = m_lngCheckCreateDate(p_objRecordContent,p_objHRPServ,out p_objModifyInfo);           
		
			if(lngRes <= 0)
				return lngRes;     
		
			return m_lngAddNewRecord2DB(p_objRecordContent,p_objHRPServ);
			

		}	

		/// <summary>
		///  �޸ļ�¼��
		/// 1.���� HRPServ��
		/// 2.�޸ļ�¼��(m_lngModifyRecordWithServ)
		/// </summary>
		/// <param name="p_objOldRecordContent">�޸�֮ǰ��ԭ��¼����</param>
		/// <param name="p_objNewRecordContent">�޸ĺ�ļ�¼����</param>
		/// <param name="p_objModifyInfo">����ǰ��¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyRecord( clsTrackRecordContent p_objOldRecordContent,
			clsTrackRecordContent p_objNewRecordContent,
			out clsPreModifyInfo p_objModifyInfo)
		{
			
			p_objModifyInfo=null; 
			if(p_objOldRecordContent == null || p_objNewRecordContent == null)
				return (long)enmOperationResult.Parameter_Error;

			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngModifyRecordWithServ(p_objOldRecordContent,p_objNewRecordContent,objHRP,out  p_objModifyInfo);
		}

		/// <summary>
		/// �޸ļ�¼��
		/// 1.�鿴��ǰ��¼�Ƿ����µļ�¼����m_lngCheckLastModifyRecord��
		/// 2.�����޸ĵ����ݱ��浽���ݿ⡣(m_lngModifyRecord2DB)
		/// </summary>
		/// <param name="p_objOldRecordContent">�޸�֮ǰ��ԭ��¼����</param>
		/// <param name="p_objNewRecordContent">�޸ĺ�ļ�¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">����ǰ��¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngModifyRecordWithServ(clsTrackRecordContent p_objOldRecordContent,
			clsTrackRecordContent p_objNewRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{			
			p_objModifyInfo=null;
			
			if(p_objOldRecordContent == null || p_objNewRecordContent == null|| p_objHRPServ == null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = m_lngCheckLastModifyRecord(p_objOldRecordContent,p_objHRPServ,out p_objModifyInfo);           
		
			if(lngRes <= 0)
				return lngRes;                     
			
			return m_lngModifyRecord2DB(p_objNewRecordContent,p_objHRPServ);
		}

		/// <summary>
		/// ɾ����¼��
		/// 1.���� HRPServ��
		/// 2.ɾ����¼��(m_lngDeleteRecordWithServ)
		/// </summary>
		/// <param name="p_objRecordContent">��ǰҪɾ���ļ�¼</param>	
		/// <param name="p_objModifyInfo">����ǰҪɾ���ļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecord( clsTrackRecordContent p_objRecordContent,			
			out clsPreModifyInfo p_objModifyInfo)
		{			
			p_objModifyInfo=null; 
			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngDeleteRecordWithServ(p_objRecordContent,objHRP,out p_objModifyInfo);
		}

		/// <summary>
		///  ɾ����¼��
		/// 1.�鿴��ǰ��¼�Ƿ����µļ�¼����m_lngCheckLastModifyRecord��
		/// 2.�Ѽ�¼�������С�ɾ������(m_lngDeleteRecord2DB)	
		/// </summary>
		/// <param name="p_objRecordContent">��ǰҪɾ���ļ�¼</param>
		/// <param name="p_objHRPServ"></param>		
		/// <param name="p_objModifyInfo">����ǰҪɾ���ļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngDeleteRecordWithServ(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,			
			out clsPreModifyInfo p_objModifyInfo)
		{			
			p_objModifyInfo=null;

			if(p_objRecordContent == null || p_objHRPServ == null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = m_lngCheckLastModifyRecord(p_objRecordContent,p_objHRPServ,out p_objModifyInfo);           
		
			if(lngRes <= 0)
				return lngRes;                     
		
			return m_lngDeleteRecord2DB(p_objRecordContent,p_objHRPServ);
		}

		/// <summary>
		/// ����������¼��
		/// 1.���� HRPServ��
		/// 2.����Ƿ����ɾ����¼����Ӽ�¼��
		/// 3.ɾ����¼��
		/// 4.��Ӽ�¼��
		/// </summary>
		/// <param name="p_objDelRecord">Ҫ���ϵļ�¼</param>
		/// <param name="p_objAddNewRecord">�µļ�¼</param>		
		/// <param name="p_objPreModifyInfo">����ǰҪ���ϵļ�¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngReAddNewRecord( clsTrackRecordContent p_objDelRecord,
			clsTrackRecordContent p_objAddNewRecord,			
			out clsPreModifyInfo p_objPreModifyInfo)			
		{
			p_objPreModifyInfo = null; 
			clsHRPTableService objHRP = new clsHRPTableService();   
		
			long lngRes = m_lngCheckLastModifyRecord(p_objDelRecord,objHRP,out p_objPreModifyInfo); 
		
			if(lngRes <= 0)
				return lngRes;   
		
			lngRes = m_lngDeleteRecord2DB(p_objDelRecord,objHRP);  
		
			if(lngRes <= 0)
				return lngRes;   
		
			return m_lngAddNewRecord2DB(p_objAddNewRecord,objHRP); 
		}			

		/// <summary>
		/// �鿴�Ƿ�����ͬ�ļ�¼ʱ��
		/// </summary>
		/// <param name="p_objRecordContent">��ǰ��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objPreModifyInfo">������ͬ��¼,���ظ���ͬ��¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objPreModifyInfo);

		/// <summary>
		/// �����¼�����ݿ⡣
		/// </summary>
		/// <param name="p_objRecordContent">��ǰ��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ);

		/// <summary>
		/// �鿴��ǰ��¼�Ƿ����µļ�¼��
		/// </summary>
		/// <param name="p_objRecordContent">��ǰ��¼����</param>
		/// <param name="p_objHRPServ"></param>		
		/// <param name="p_objModifyInfo">����ǰ��¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,			
			out clsPreModifyInfo p_objModifyInfo);

		/// <summary>
		/// �����޸ĵ����ݱ��浽���ݿ⡣
		/// </summary>
		/// <param name="p_objRecordContent">��ǰ��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ);

		/// <summary>
		/// �Ѽ�¼�������С�ɾ������
		/// </summary>
		/// <param name="p_objRecordContent">��ǰ��¼����</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ);

		/// <summary>
		/// ��ȡ��ӡ��Ϣ��
		/// 1.��ȡ��ӡ���ݣ�����������p_dtmModifyDate�������µ�ModifyDate��������� p_objContent
		///   �������µ����ݣ������������Ϊnull��
		/// 2.��ȡ��ӡʱ�䣺������� p_dtmFirstPrintDate ����״δ�ӡʱ�䡣p_blnIsFirstPrint���
		///   �Ƿ��״δ�ӡ�������Ϊtrue���ͻ����ڴ�ӡ����Ҫ����p_dtmFirstPrintDate�����ݿ⡣		
        /// </summary>
        /// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_dtmModifyDate">�޸�ʱ��</param>
		/// <param name="p_objContent">��¼����</param>
		/// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
		/// <param name="p_blnIsFirstPrint">�Ƿ��״δ�ӡ</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPrintInfo( string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenDate,
			DateTime p_dtmModifyDate,
			out clsTrackRecordContent p_objContent,
			out DateTime p_dtmFirstPrintDate,
			out bool p_blnIsFirstPrint)
		{
			p_objContent = null;
			p_dtmFirstPrintDate=DateTime.Now;
			p_blnIsFirstPrint=true; 
			//�������                     
			if(p_strInPatientID==null||p_strInPatientID==""||p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			clsHRPTableService objHRP = new clsHRPTableService();
		
			//��ȡʱ��
			DateTime dtmModifyDate;
			string strFirstPrintDate;
			long lngRes = m_lngGetModifyDateAndFirstPrintDate(p_strInPatientID,p_strInPatientDate,p_strOpenDate,objHRP,out dtmModifyDate,out strFirstPrintDate);
		
			if(lngRes <= 0)
				return lngRes;
		
			//�ж�dtmModifyDate��p_dtmModifyDate�Ƿ�һ��
			//�����һ��
			if(p_dtmModifyDate!=dtmModifyDate)
			{
				lngRes = m_lngGetRecordContentWithServ(p_strInPatientID,p_strInPatientDate,p_strOpenDate,objHRP,out p_objContent);       
				if(lngRes <= 0)
					return lngRes;
			}
			//�ж�strFirstPrintDate�Ƿ�Ϊnull
			//�����
			if(strFirstPrintDate==null|| strFirstPrintDate.ToString().Trim().Length==0)
			{
				p_dtmFirstPrintDate = DateTime.Now;
				p_blnIsFirstPrint = true;
			}				
			else//�������
			{
				p_dtmFirstPrintDate = DateTime.Parse(strFirstPrintDate);
				p_blnIsFirstPrint = false;
			}
            //objHRP.Dispose();
			return lngRes;
		}
        /// <summary>
        /// ��ȡ��ӡ��Ϣ��
        /// 1.��ȡ��ӡ���ݣ�����������p_dtmModifyDate�������µ�ModifyDate��������� p_objContent
        ///   �������µ����ݣ������������Ϊnull��
        /// 2.��ȡ��ӡʱ�䣺������� p_dtmFirstPrintDate ����״δ�ӡʱ�䡣p_blnIsFirstPrint���
        ///   �Ƿ��״δ�ӡ�������Ϊtrue���ͻ����ڴ�ӡ����Ҫ����p_dtmFirstPrintDate�����ݿ⡣		
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreatedDate">��������</param>
        /// <param name="p_dtmModifyDate">�޸�ʱ��</param>
        /// <param name="p_objContent">��¼����</param>
        /// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
        /// <param name="p_blnIsFirstPrint">�Ƿ��״δ�ӡ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrintInfo(  string p_strRegisterId,
            string p_strCreatedDate,
            DateTime p_dtmModifyDate,
            out clsTrackRecordContent p_objContent,
            out DateTime p_dtmFirstPrintDate,
            out bool p_blnIsFirstPrint)
        {
            p_objContent = null;
            p_dtmFirstPrintDate = DateTime.Now;
            p_blnIsFirstPrint = true; 
            //�������                     
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRP = new clsHRPTableService();

            //��ȡʱ��
            DateTime dtmModifyDate;
            string strFirstPrintDate;
            long lngRes = m_lngGetModifyDateAndFirstPrintDate(p_strRegisterId, p_strCreatedDate, out dtmModifyDate, out strFirstPrintDate);

            if (lngRes <= 0)
                return lngRes;

            //�ж�dtmModifyDate��p_dtmModifyDate�Ƿ�һ��
            //�����һ��
            if (p_dtmModifyDate != dtmModifyDate)
            {
                lngRes = m_lngGetRecordContentWithServ(p_strRegisterId, p_strCreatedDate, out p_objContent);
                if (lngRes <= 0)
                    return lngRes;
            }
            //�ж�strFirstPrintDate�Ƿ�Ϊnull
            //�����
            if (strFirstPrintDate == null || strFirstPrintDate.ToString().Trim().Length == 0)
            {
                p_dtmFirstPrintDate = DateTime.Now;
                p_blnIsFirstPrint = true;
            }
            else//�������
            {
                p_dtmFirstPrintDate = DateTime.Parse(strFirstPrintDate);
                p_blnIsFirstPrint = false;
            }
            //objHRP.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="dtmModifyDate">�޸�ʱ��</param>
        /// <param name="strFirstPrintDate">�״δ�ӡʱ��</param>
        /// <returns></returns>
		[AutoComplete]
        protected virtual long m_lngGetModifyDateAndFirstPrintDate(string p_strRegisterId, string p_strCreatedDate, out DateTime p_dtmModifyDate, out string p_strFirstPrintDate)
        {
            p_strFirstPrintDate = null;
            p_dtmModifyDate = DateTime.MinValue;
            return 0;
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
        protected virtual long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        { 
            p_strFirstPrintDate = null;
            p_dtmModifyDate = DateTime.MinValue;
            return 0;
        }

		/// <summary>
		/// �������ݿ��е��״δ�ӡʱ�䡣
		/// </summary>
        /// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenDate">��¼ʱ��</param>
		/// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            return 0;
        }
        /// <summary>
        ///  �������ݿ��е��״δ�ӡʱ�䡣
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreatedDate">����ʱ��</param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
            string p_strCreatedDate,
            DateTime p_dtmFirstPrintDate)
        {
            return 0;
        }

		/// <summary>
		/// ��ȡ���˵��Ѿ���ĳ�û�ɾ����¼ʱ���б�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strDeleteUserID">ɾ��������ID</param>
		/// <param name="p_strCreateRecordTimeArr">�û���д�ļ�¼ʱ������</param>
		/// <param name="p_strOpenRecordTimeArr">ϵͳ���ɵļ�¼ʱ������</param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            return 0;
        }
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
            return 0;
        }

		/// <summary>
		/// ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
		/// </summary>
        /// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strCreateRecordTimeArr">��Ժ����</param>
		/// <param name="p_strOpenRecordTimeArr">ϵͳ���ɵļ�¼ʱ������</param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            return 0;
        }
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
            return 0;
        }


		/// <summary>
		/// ��ȡָ���Ѿ���ɾ����¼�����ݡ�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenRecordTime">��¼ʱ��</param>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDeleteRecordContent( string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenRecordTime,
			out clsTrackRecordContent p_objRecordContent)
		{
			p_objRecordContent = null; 
			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngGetDeleteRecordContentWithServ(p_strInPatientID,p_strInPatientDate,p_strOpenRecordTime,objHRP,out p_objRecordContent);
		}
        /// <summary>
        /// ��ȡָ���Ѿ���ɾ����¼������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreatedDate">����ʱ��</param>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeleteRecordContent( string p_strRegisterId,
            string p_strCreatedDate,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null; 
            clsHRPTableService objHRP = new clsHRPTableService();
            return m_lngGetDeleteRecordContentWithServ(p_strRegisterId, p_strCreatedDate, out p_objRecordContent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
        /// <param name="p_strCreatedDate">����ʱ��</param>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetDeleteRecordContentWithServ(string p_strRegisterId, string p_strCreatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            return 0;
        }

		/// <summary>
		/// ��ȡָ���Ѿ���ɾ����¼�����ݡ�
		/// </summary>
		/// <param name="p_strInPatientID">סԺ��</param>
		/// <param name="p_strInPatientDate">��Ժ����</param>
		/// <param name="p_strOpenRecordTime">��¼ʱ��</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContent">��¼����</param>
		/// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenRecordTime,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        { 
            p_objRecordContent = null;
            return 0;
        }


        /// <summary>
        /// ��ȡһ��סԺȫ�����ϼ�¼
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public virtual long m_lngGetAllInactiveInfo(string p_strSQL,string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
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


	}// END CLASS DEFINITION clsDiseaseTrackService
}
