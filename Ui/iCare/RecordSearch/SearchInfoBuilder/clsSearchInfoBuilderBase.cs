using System;
using System.Collections;

namespace iCare.RecordSearch.SearchInfoBuilder
{
	/// <summary>
	/// �����û���������ϲ�ѯ��Ϣ
	/// </summary>
	public class clsSearchInfoBuilderBase
	{
		/// <summary>
		/// ���ɲ�ѯ��Ϣ
		/// </summary>
		/// <param name="p_objFormInfo">����Ϣ</param>
		/// <param name="p_objConditionStatusEnumerator">��ѯ����</param>
		/// <returns></returns>
		public virtual weCare.Core.Entity.clsRecordSearch_SearchInfo m_objBuildSearchInfo(RecordSearch.clsRecordSearchDomain.clsFormInfo p_objFormInfo,IEnumerator p_objConditionStatusEnumerator)
		{
			return null;
		}

		/// <summary>
		/// ��ѯ����������
		/// </summary>
		public virtual string m_StrBuildType
		{
			get
			{
				return "none";
			}
		}		
	}
}