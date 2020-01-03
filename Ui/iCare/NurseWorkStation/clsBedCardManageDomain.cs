using System;
using weCare.Core.Entity;

namespace iCare
{
	/// <summary>
	/// clsBedCardManageDomain ��ժҪ˵����
	/// </summary>
	public class clsBedCardManageDomain
	{
        //private com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev;
		public clsBedCardManageDomain()
		{
            //m_objBedCardSev = new com.digitalwave.BedCardManageServ.clsBedCardManageServ();
		}
		/// <summary>
		/// ��ȡ��ͷ����Ϣ
		/// </summary>
		/// <param name="p_objBedCardvalue"></param>
		/// <returns></returns>
		public long m_lngGetBedCardValue(ref clsBedCardValue p_objBedCardvalue)
		{
            //com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev =
            //    (com.digitalwave.BedCardManageServ.clsBedCardManageServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.BedCardManageServ.clsBedCardManageServ));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetBedCardValue(ref p_objBedCardvalue);
            }
            finally
            {
                //m_objBedCardSev.Dispose();
            }
            return lngRes;
		}

		/// <summary>
		/// ���洲ͷ����Ϣ
		/// </summary>
		/// <param name="p_objBedCardvalue"></param>
		/// <returns></returns>
		public long m_lngSaveBedCardValue(clsBedCardValue p_objBedCardvalue)
		{
            //com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev =
            //    (com.digitalwave.BedCardManageServ.clsBedCardManageServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.BedCardManageServ.clsBedCardManageServ));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngSaveBedCardValue(p_objBedCardvalue);
            }
            finally
            {
                //m_objBedCardSev.Dispose();
            }
            return lngRes;
		}
		/// <summary>
		/// ���ݴ�λID��ȡ�ܴ�ҽʦ
		/// </summary>
		/// <param name="p_strBedID"></param>
		/// <param name="p_strDoctor"></param>
		/// <returns></returns>
		public long m_lngGetManageDocWithBedID(string p_strBedID,out string p_strDoctor)
		{
			p_strDoctor = null;

            //com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev =
            //    (com.digitalwave.BedCardManageServ.clsBedCardManageServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.BedCardManageServ.clsBedCardManageServ));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetManageDocWithBedID(p_strBedID,out p_strDoctor);
            }
            finally
            {
                //m_objBedCardSev.Dispose();
            }
            return lngRes;
		}
		/// <summary>
		/// ��ȡ������λ��Ϣ
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_objInfoArr"></param>
		/// <returns></returns>
		public long m_lngGetBedInfoByAreaID(string p_strAreaID,out clsBed_PatientInfo[] p_objInfoArr)
		{
			p_objInfoArr = null;

            //com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev =
            //    (com.digitalwave.BedCardManageServ.clsBedCardManageServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.BedCardManageServ.clsBedCardManageServ));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetBedInfoByAreaID(p_strAreaID,out p_objInfoArr);
            }
            finally
            {
                //m_objBedCardSev.Dispose();
            }
            return lngRes;
		}
		/// <summary>
		/// ��Ӵ�λ���ܴ�ҽ��
		/// </summary>
		/// <param name="p_strBedID"></param>
		/// <param name="p_strManageDocID"></param>
		/// <returns></returns>
		public long m_lngAddBed_ManageDoc(string p_strBedID,string p_strManageDocID,string p_strAreaID)
		{
            //com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev =
            //    (com.digitalwave.BedCardManageServ.clsBedCardManageServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.BedCardManageServ.clsBedCardManageServ));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngAddBed_ManageDoc(p_strBedID,p_strManageDocID,p_strAreaID);
            }
            finally
            {
                //m_objBedCardSev.Dispose();
            }
            return lngRes;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_strBedID"></param>
		/// <param name="p_objInfo"></param>
		/// <returns></returns>
		public long m_lngGetPatientByBedID(string p_strAreaID,string p_strBedID,ref clsBed_PatientInfo p_objInfo)
		{
            //com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev =
            //    (com.digitalwave.BedCardManageServ.clsBedCardManageServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.BedCardManageServ.clsBedCardManageServ));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetPatientByBedID(p_strAreaID,p_strBedID,ref p_objInfo);
            }
            finally
            {
                //m_objBedCardSev.Dispose();
            }
            return lngRes;
		}
		/// <summary>
		/// ���ݲ�����ȡ��λ���ܴ�ҽ��
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_objValueArr"></param>
		/// <returns></returns>
		public long m_lngGetBed_ManageDoc(string p_strAreaID,out clsBed_ManageDocValue[] p_objValueArr)
		{
			p_objValueArr = null;

            //com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev =
            //    (com.digitalwave.BedCardManageServ.clsBedCardManageServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.BedCardManageServ.clsBedCardManageServ));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetBed_ManageDoc(p_strAreaID,out p_objValueArr);
            }
            finally
            {
                //m_objBedCardSev.Dispose();
            }
            return lngRes;
		}
		/// <summary>
		/// ���ݲ���ɾ����λ���ܴ�ҽ��
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <returns></returns>
		public long m_lngDeleteBed_ManageDo(string p_strAreaID)
		{
            //com.digitalwave.BedCardManageServ.clsBedCardManageServ m_objBedCardSev =
            //    (com.digitalwave.BedCardManageServ.clsBedCardManageServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.BedCardManageServ.clsBedCardManageServ));

			long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngDeleteBed_ManageDo(p_strAreaID);
            }
            finally
            {
                //m_objBedCardSev.Dispose();
            }
            return lngRes;
		}
	}
}
