using System;
using weCare.Core.Entity;
using System.Data;

namespace iCare
{
	/// <summary>
	/// Summary description for clsBeforeOperSumShareDomain.
	/// </summary>
	public class clsBeforeOperSumShareDomain
	{
        //private clsBeforeOperSumShareService m_objServ;
		public clsBeforeOperSumShareDomain()
		{
			//
			// TODO: Add constructor logic here
			//
            //m_objServ = new clsBeforeOperSumShareService();
		}

		public long m_lngGetShareValue(string p_strInPaitentID,string p_strInPatientDate,out stuShare p_stuShare)
		{
			p_stuShare.m_strSpecialHandle = "";

			DataTable dtResult;

            //clsBeforeOperSumShareService m_objServ =
            //    (clsBeforeOperSumShareService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperSumShareService));

			long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr02()).Service.clsBeforeOperSumShareService_m_lngGetShareValue( p_strInPaitentID,p_strInPatientDate,out dtResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (m_lngRes < 0) return m_lngRes;

			if(dtResult.Rows.Count != 1) return 0;

			p_stuShare.m_strSpecialHandle = dtResult.Rows[0]["SPECIALHANDLE"].ToString().Trim();

			return 1;
		}

		public clsBeforeOperSumShareDomain.stuShare m_stuGetShareValue(string p_strInPaitentID,string p_strInPatientDate)
		{
			stuShare stuValue;

			DataTable dtResult;

            //clsBeforeOperSumShareService m_objServ =
            //    (clsBeforeOperSumShareService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBeforeOperSumShareService));

			long m_lngRes = 0;
            try
            {
                m_lngRes = (new weCare.Proxy.ProxyEmr02()).Service.clsBeforeOperSumShareService_m_lngGetShareValue( p_strInPaitentID,p_strInPatientDate,out dtResult);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (m_lngRes < 0 || dtResult.Rows.Count != 1) 
			{
				stuValue.m_strSpecialHandle = "";
			}
			
			stuValue.m_strSpecialHandle = dtResult.Rows[0]["SPECIALHANDLE"].ToString().Trim();

			return stuValue;
		}

		/// <summary>
		/// ������ݵĽṹ��
		/// </summary>
		public struct stuShare
		{
			public string m_strSpecialHandle;	//����������ʽ������ע��������������Ԥ��������
		}

	}
}
