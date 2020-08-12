using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlMedUseful ��ժҪ˵����
    /// </summary>
    public class clsDomainControlMedUseful : com.digitalwave.GUI_Base.clsDomainController_Base  //GUI_Base.dll
    {
        public clsDomainControlMedUseful()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ��ȡҩƷ����ʧЧ��ҩƷ��Ϣ
        /// </summary>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_strDate"></param>
        /// <param name="p_outDt"></param>
        /// <returns></returns>
        public long m_lngGetUsefulMedInfo(string p_strStorageID, string p_strDate, out DataTable p_outDt)
        {
            long lngRes = 1;
            p_outDt = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageMedDetailSvc));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetUsefulMedInfo( p_strStorageID, p_strDate, out p_outDt);

            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        /// <param name="p_outDt"></param>
        /// <returns></returns>
        public long m_lngGetStorageInfo(out System.Data.DataTable p_outDt)
        {
            long lngRes = 1;
            p_outDt = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageMedicineSvc objSVC = new com.digitalwave.iCare.middletier.HIS.clsStorageMedicineSvc();
            System.Data.DataTable dt = new DataTable();
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetAllStorage(  out p_outDt);
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
    }
}
