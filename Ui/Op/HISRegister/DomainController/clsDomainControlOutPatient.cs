using System;
using System.Data;
//using com.digitalwave.iCare.middletier.HIS;//HISMedStore_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	#region �����շѺ��������ɱ���
	/// <summary>	
	/// �����շѺ��������ɱ���
	/// Create ��ΰ�� by 2005-09-13
	/// </summary>
	public class clsDomainControlOutPatient : clsDomainController_Base//GUI_Base.dll
	{
		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public clsDomainControlOutPatient()
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
    //        com.digitalwave.iCare.middletier.HIS.clsOutPatientSvc objSvc = 
				//(com.digitalwave.iCare.middletier.HIS.clsOutPatientSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOutPatientSvc));
            return (new weCare.Proxy.ProxyOP01()).Service.m_dtmGetServerDate();
		}
		#endregion

		#region ��ʱ��ͳ���շ� 
		/// <summary>
		/// ��ʱ��ͳ���շ�
		/// </summary>
		/// <param name="p_dtm1">��ʼʱ��</param>
		/// <param name="p_dtm2">����ʱ��</param>
		/// <param name="p_dtb">��ѯ�õ��Ľ��</param>
		/// <returns></returns>
		public long m_lngGetStatiticsData(string p_dtm1,string p_dtm2,out DataTable p_dtb)
		{
			long lngRes = 0;
			//com.digitalwave.iCare.middletier.HIS.clsOutPatientSvc objSvc = 
			//	(com.digitalwave.iCare.middletier.HIS.clsOutPatientSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOutPatientSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetStatiticsData(  p_dtm1, p_dtm2, out p_dtb);
            //objSvc.Dispose();
            //objSvc = null;
			return lngRes;
		}
		#endregion

	}
	#endregion

}
