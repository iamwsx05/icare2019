using System;
using System.Collections ;
using weCare.Core.Entity;


namespace iCare
{
	/// <summary>
	/// Summary description for clsPSGOrderDomain.
	/// </summary>
	public class clsPSGOrderDomain
	{
        //private clsPSGOrderService  m_objService;

		public clsPSGOrderDomain()
		{
             //m_objService =new clsPSGOrderService();
			//
			// TODO: Add constructor logic here
			//
		}

			
		public ArrayList m_objPSGOrders=new ArrayList();

		/// <summary>
		/// ���ݲ���ID����һ����ҽѧ������뵥��������
		/// </summary>
		/// <param name="p_strPatientID">����ID</param>
		/// <param name="p_objEKGOrderArr">������뵥�������飬out�͵�</param>
		/// <returns>���ض���ĸ���</returns>
		public string[] lngGetPSGOrderArrByPatientID(string p_strPatientID,string p_strPatientDate)
		{
			string[] m_strRe=new String[0];

            //clsPSGOrderService m_objService =
            //    (clsPSGOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPSGOrderService));

            try
            {
                (new weCare.Proxy.ProxyEmr03()).Service.clsPSGOrderService_m_lngGetTimeInfoOfAPatient(  p_strPatientID, p_strPatientDate, ref m_strRe);
            }
            finally
            {
                ////m_objService.Dispose();
            }
			return m_strRe;
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="p_objEKGOrderArr">Ҫ����Ķ���</param>
		/// <returns>����1��ʾ����ɹ�������0��ʾ����ʧ��</returns>
		public long lngSave(bool p_bnlIsNew,clsPSGOrder p_objPSGOrder)
		{
            long lngRes = 0;
            //clsPSGOrderService m_objService =
            //    (clsPSGOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPSGOrderService));

            try
            {
			    if(p_bnlIsNew==true)
				    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngUpdatePSGOrder(p_objPSGOrder,enmUpdateAction.enmAddNew);
			    else
				    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngUpdatePSGOrder(p_objPSGOrder,enmUpdateAction.enmEdit);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
		}

		public long lngDelete(clsPSGOrder p_objPSGOrder)
		{
            long lngRes = 0;
            //clsPSGOrderService m_objService =
            //    (clsPSGOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPSGOrderService));

            try
            {
			    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngUpdatePSGOrder(p_objPSGOrder,enmUpdateAction.enmDelete);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;

		}

		public long GetPSGOrder(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate, out clsPSGOrder p_objPSGOrder)
		{
			long lngRes = 0;
            //clsPSGOrderService m_objService =
            //    (clsPSGOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPSGOrderService));

            try
            {
			    lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetPSGOrder( p_strInPatientID,p_strInPatientDate,p_strCreateDate,out p_objPSGOrder);
            }
            finally
            {
                ////m_objService.Dispose();
            }
            return lngRes;
		}
	}
	

}
