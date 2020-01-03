using System;
using System.Collections;
using weCare.Core.Entity;

namespace iCare
{
	/// <summary>
	/// clsMiniBooldSugarChkDomin ��ժҪ˵����
	/// </summary>
	public class clsMiniBooldSugarChk_GXDomin
	{
        //clsMiniBloodSugarChk_GXServ m_objServ;
		public clsMiniBooldSugarChk_GXDomin()
		{
            //m_objServ = new clsMiniBloodSugarChk_GXServ();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		public long m_lngAddNewRecoed(clsMiniBloodSugarChkValue_GX p_objValue)
		{
            //clsMiniBloodSugarChk_GXServ m_objServ =
            //    (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));

            long lngResult = 0;
			try
            {
                lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNewRecoed(p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		public long m_lngModifyRecoed(clsMiniBloodSugarChkValue_GX p_objValue)
		{
            //clsMiniBloodSugarChk_GXServ m_objServ =
            //    (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));

            long lngResult = 0;
			try
            {
                lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngModifyRecoed(p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objValue"></param>
		/// <returns></returns>
		public long m_lngDeleteRecoed(clsMiniBloodSugarChkValue_GX p_objValue)
		{
			//clsMiniBloodSugarChk_GXServ m_objServ = 
   //             (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));

            long lngResult = 0;
			try
            {
                lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngDeleteRecoed(p_objValue);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_objValues"></param>
		/// <returns></returns>
		public long m_lngGetRecoedByInPatient(string p_strInPatientID,DateTime p_dtmInPatientDate,out clsMiniBloodSugarChkValue_GX[] p_objValues)
		{
            //clsMiniBloodSugarChk_GXServ m_objServ =
            //    (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));

            long lngResult = 0;
			try
            {
                lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRecoedByInPatient(p_strInPatientID,p_dtmInPatientDate,out p_objValues);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngResult;
		}

        /// <summary>
        /// �����Զ����ͷ��
        /// </summary>
        /// <param name="p_strCustomName">�Զ����ͷ��</param>
        /// <param name="p_strColumnName">��Ӧ���ֶ���</param>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
        /// <returns></returns>
        public long m_lngSetCustomName(string p_strCustomName,
            string p_strColumnName,
            string p_strInPatientID,
            DateTime p_dtmInPatientDate)
        {
            //clsMiniBloodSugarChk_GXServ m_objServ =
            //    (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));

            long lngResult = (new weCare.Proxy.ProxyEmr()).Service.m_lngSetCustomName(p_strCustomName, p_strColumnName, p_strInPatientID, p_dtmInPatientDate);
            return lngResult;
        }
	}
}
