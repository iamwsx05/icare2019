using System;

namespace iCare.RecordSearch.PatientDetailMaker
{
	/// <summary>
	/// ������ϸ��Ϣ������
	/// </summary>
	public class clsPatientDetailMakerBase
	{
		/// <summary>
		/// ��ȡ������ϸ��Ϣ����
		/// </summary>
		/// <param name="p_objPatientList"></param>
		/// <returns></returns>
		public virtual string m_strMakerPatientDetailDesc(clsPatient p_objPatient,RecordSearch.clsRecordSearchDomain.clsPatientList p_objPatientList)
		{
			return "";
		}

		public virtual string m_StrFormName
		{
			get
			{
				return "none";
			}
		}
	}
}
