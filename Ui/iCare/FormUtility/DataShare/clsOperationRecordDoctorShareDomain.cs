using System;
using System.Data;
using weCare.Core.Entity;

namespace iCare
{
	/// <summary>
	/// ������¼�����ݹ���
	/// </summary>
	public class clsOperationRecordDoctorShareDomain
	{
        //private clsOperationRecordDoctorShareService m_objServ;

		public clsOperationRecordDoctorShareDomain()
		{
            //m_objServ = new clsOperationRecordDoctorShareService();
		}

		/// <summary>
		/// ��ȡ�������ݣ�ֻΪסԺ������ҳ�ṩ����
		/// </summary>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_stuResultArr"></param>
		/// <returns></returns>
		public long m_lngGetBaseOperationValueArr(string p_strInPaitentID,string p_strInPatientDate, out stuBaseOperationValue[] p_stuResultArr)
		{
			p_stuResultArr = null;
			
			DataTable dtbResult;

            //clsOperationRecordDoctorShareService m_objServ =
            //    (clsOperationRecordDoctorShareService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorShareService));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.clsOperationRecordDoctorShareService_m_lngGetBaseOperationValue( p_strInPaitentID, p_strInPatientDate,out dtbResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
			if(lngRes <= 0)
				return lngRes;

			p_stuResultArr = new stuBaseOperationValue[dtbResult.Rows.Count];

			for(int i=0;i<p_stuResultArr.Length;i++)
			{
                //������¼��
				p_stuResultArr[i].m_strAnaesther = dtbResult.Rows[i]["ANAESTHER"].ToString();
				p_stuResultArr[i].m_strAnaesthesiaCategoryDosage = dtbResult.Rows[i]["ANAESTHESIACATEGORYDOSAGE"].ToString();
				p_stuResultArr[i].m_strOperationBeginDate = dtbResult.Rows[i]["OPERATIONBEGINDATE"].ToString();
				p_stuResultArr[i].m_strFirstAssistantID = dtbResult.Rows[i]["FIRSTASSISTANTID"].ToString();
				p_stuResultArr[i].m_strFirstAssistantName = dtbResult.Rows[i]["FIRSTASSISTANTNAME"].ToString();
				p_stuResultArr[i].m_strOperationDoctorID = dtbResult.Rows[i]["OPERATIONDOCTORID"].ToString();
				p_stuResultArr[i].m_strOperationDoctorName = dtbResult.Rows[i]["OPERATIONDOCTORNAME"].ToString();
				p_stuResultArr[i].m_strOperationName = dtbResult.Rows[i]["OPERATIONNAME"].ToString();
				p_stuResultArr[i].m_strSecondAssistantID = dtbResult.Rows[i]["SECONDASSISTANTID"].ToString();
				p_stuResultArr[i].m_strSecondAssistantName = dtbResult.Rows[i]["SECONDASSISTANTNAME"].ToString();
                p_stuResultArr[i].m_strOperationCutLevel = ""; //dtbResult.Rows[i]["CUTLEVEL"].ToString();
                p_stuResultArr[i].m_strZqOperation = "";// dtbResult.Rows[i]["OPERATIONELECTIVE"].ToString();
                p_stuResultArr[i].m_strAnaesthesiaCategoryDosageID = "";// dtbResult.Rows[i]["AANAESTHESIAMODEID"].ToString();
                p_stuResultArr[i].m_strOperationLevel = "";// dtbResult.Rows[i]["OPERATIONLEVEL"].ToString();
                //
			}

			return 1;
		}

		/// <summary>
		/// ��ȡ�������ݣ�ֻΪ�����󲡳̼�¼�ṩ����
		/// </summary>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_dtmRecordDate">��¼���ڣ���ȡ������֮ǰ�������Ϣ</param>
		/// <param name="p_stuResultArr"></param>
		/// <returns></returns>
		public long m_lngGetLatestOperationInfo(string p_strInPaitentID,string p_strInPatientDate,DateTime p_dtmRecordDate, out stuLatestOperationValue p_stuResult)
		{
			p_stuResult = new stuLatestOperationValue();
			
			DataTable dtbResult;

            //clsOperationRecordDoctorShareService m_objServ =
            //    (clsOperationRecordDoctorShareService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationRecordDoctorShareService));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr02()).Service.clsOperationRecordDoctorShareService_m_lngGetLatestOperationInfo( p_strInPaitentID, p_strInPatientDate,p_dtmRecordDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),out dtbResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
			if(lngRes <= 0)
				return lngRes;

			if(dtbResult.Rows.Count == 1)
			{			
				p_stuResult.m_strOperationBeginDate = dtbResult.Rows[0]["OPERATIONBEGINDATE"].ToString();
				p_stuResult.m_strOperationName = dtbResult.Rows[0]["OPERATIONNAME"].ToString();
				p_stuResult.m_strAnaesthesiaCategoryDosage = dtbResult.Rows[0]["ANAESTHESIACATEGORYDOSAGE"].ToString();
				p_stuResult.m_strDiagnoseAfterOperation = dtbResult.Rows[0]["DIAGNOSEAFTEROPERATION"].ToString();
				p_stuResult.m_strOperationProcess = dtbResult.Rows[0]["OPERATIONPROCESS"].ToString();				
			}
			else
			{
				p_stuResult.m_strOperationBeginDate = DateTime.Now.ToString();
				p_stuResult.m_strOperationName = "";
				p_stuResult.m_strAnaesthesiaCategoryDosage = "";
				p_stuResult.m_strDiagnoseAfterOperation = "";
				p_stuResult.m_strOperationProcess = "";				
			}
			
			return 1;
		}

		/// <summary>
		/// �����󲡳̼�¼ʹ�õ��ֶ�
		/// </summary>
		public struct stuLatestOperationValue
		{
			/// <summary>
			/// ������ʼ����
			/// </summary>
			public string m_strOperationBeginDate;
			/// <summary>
			/// ��������
			/// </summary>
			public string m_strOperationName;
			/// <summary>
			/// ����ʽ
			/// </summary>
			public string m_strAnaesthesiaCategoryDosage;

			/// <summary>
			/// �������
			/// </summary>
			public string m_strDiagnoseAfterOperation;

			/// <summary>
			/// ��������
			/// </summary>
			public string m_strOperationProcess;			
		}

		/// <summary>
		/// סԺ������ҳ��ʹ�õ��ֶ�
		/// </summary>
		public struct stuBaseOperationValue
		{
			/// <summary>
			/// ������ʼ����
			/// </summary>
			public string m_strOperationBeginDate;
			/// <summary>
			/// ��������
			/// </summary>
			public string m_strOperationName;
			/// <summary>
			/// ����ʽ
			/// </summary>
			public string m_strAnaesthesiaCategoryDosage;
            /// <summary>
            /// ����ʽ
            /// </summary>
            public string m_strAnaesthesiaCategoryDosageID;
			/// <summary>
			/// ����ҽʦ
			/// </summary>
			public string m_strAnaesther;
			/// <summary>
			/// ��������
			/// </summary>
			public string m_strOperationDoctorName;
			/// <summary>
			/// һ������
			/// </summary>
			public string m_strFirstAssistantName;
			/// <summary>
			/// ��������
			/// </summary>
			public string m_strSecondAssistantName;
			/// <summary>
			/// ����ID
			/// </summary>
			public string m_strOperationDoctorID;
			/// <summary>
			/// һ��ID
			/// </summary>
			public string m_strFirstAssistantID;
			/// <summary>
			/// ����ID
			/// </summary>
			public string m_strSecondAssistantID;

            //su.liang
            /// <summary>
            /// �п����ϵȼ�
            /// </summary>
            public string m_strOperationCutLevel;
            /// <summary>
            /// ��������
            /// </summary>
            public string m_strZqOperation;
            /// <summary>
            /// �����ȼ�
            /// </summary>
            public string m_strOperationLevel;
		}
	}
}
