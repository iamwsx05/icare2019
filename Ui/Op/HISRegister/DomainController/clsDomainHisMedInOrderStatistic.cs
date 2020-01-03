using System;
using System.Data;
//using com.digitalwave.iCare.middletier.HIS;//HISMedStore_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	#region ҩ����ⵥͳ��ҵ������� ��created by weiling.huang  at 2005-9-14
	/// <summary>
	/// clsDomainHisMedInOrderStatistic ��ժҪ˵����
	/// </summary>
	public class clsDomainHisMedInOrderStatistic : clsDomainController_Base//GUI_Base.dll
	{
		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public clsDomainHisMedInOrderStatistic()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

		#region  ���ϵͳʱ��
		/// <summary>
		/// ���ϵͳʱ�� Create ��ΰ�� by 2005-09-13
		/// <summary>
		/// <returns>DateTime</returns>
		public  DateTime m_dtmGetServerDate()
		{			
			//com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc objSvc = 
			//	(com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc));
			return (new weCare.Proxy.ProxyOP01()).Service.m_dtmGetServerDate();
						
		}
		#endregion

		#region ��ʱ��ͳ��ҩ�������� 
		/// <summary>
		/// ��ʱ��ͳ��ҩ��������
		/// </summary>
		/// <param name="p_strPeriodId">��ѯ������ID</param>
		/// <param name="p_dtb">��ѯ�õ��Ľ��</param>
		/// <returns></returns>
		public long m_lngGetStatiticsData(out DataTable p_dtb, string p_strPeriodId)
		{
			long lngRes = 0;
			//com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc objSvc = 
			//	(com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetStatiticsData(  out p_dtb, p_strPeriodId);
            //objSvc.Dispose();
            //objSvc = null;
			return lngRes;
		}
		#endregion

		#region ��ȡ������
		/// <summary>
		/// ��ȡ������
		/// </summary>
		/// <param name="p_dtb">��ѯ�õ��Ľ��</param>
		/// <returns></returns>
		public long m_lngGetPeriodList(out clsPeriod_VO[] p_objResultArr)
		{
			long lngRes = 0;
			//com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc objSvc = 
			//	(com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisMedInOrderStatisticSvc));
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPeriodList(  out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
			return lngRes;
		}
		#endregion


	}
	#endregion

}
