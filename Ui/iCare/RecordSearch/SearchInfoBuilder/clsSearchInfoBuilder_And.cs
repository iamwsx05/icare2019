using System;
using weCare.Core.Entity;

namespace iCare.RecordSearch.SearchInfoBuilder
{
	/// <summary>
	/// �Բ�ѯ������And��ϵĲ�ѯ��Ϣ������
	/// </summary>
	public class clsSearchInfoBuilder_And : clsSearchInfoBuilderBase
	{
		
		public override string m_StrBuildType
		{
			get
			{
				return "and";
			}
		}

		public override  weCare.Core.Entity.clsRecordSearch_SearchInfo m_objBuildSearchInfo(iCare.RecordSearch.clsRecordSearchDomain.clsFormInfo p_objFormInfo, System.Collections.IEnumerator p_objConditionStatusEnumerator)
		{
			clsRecordSearch_SearchInfo objSearchInfo = new clsRecordSearch_SearchInfo();

			System.Text.StringBuilder sbdTemp = new System.Text.StringBuilder(" 1=1 ");
			
			while(p_objConditionStatusEnumerator.MoveNext())
			{
				RecordSearch.ConditionMaker.clsConditionMakerBase.clsConditionStatus objConditionStatus = p_objConditionStatusEnumerator.Current as RecordSearch.ConditionMaker.clsConditionMakerBase.clsConditionStatus;

				if(objConditionStatus == null)
					continue;	
			
				sbdTemp.Append(" and ");

				sbdTemp.Append(objConditionStatus.m_strConditionSQL);			
			}		
	
			objSearchInfo.m_strSQL = p_objFormInfo.m_strMainSearchInfo+" "+sbdTemp.ToString();
			
			return objSearchInfo;
		}
	}
}
