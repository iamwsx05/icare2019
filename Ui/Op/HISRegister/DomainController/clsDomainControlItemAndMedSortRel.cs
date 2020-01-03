using System;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.DLL
using System.Data;
using System.Windows.Forms;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlItemAndMedSortRel ��ժҪ˵����
    /// </summary>
    public class clsDomainControlItemAndMedSortRel : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlItemAndMedSortRel()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        public long m_lngGetFeeSort(out DataTable p_outDtResult, out DataTable p_outDtMedType, out DataTable p_outDtMedStorage, out DataTable p_outStorage)
        {
            p_outDtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc));
            long lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetFeeSort(out p_outDtResult, out p_outDtMedType, out p_outDtMedStorage, out p_outStorage);

            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        public long m_lngGetFeeAndMedSortRel(out DataTable p_outDtResult)
        {
            p_outDtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc));

            long lngRes;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetFeeAndMedSortRel(out p_outDtResult);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
        public long m_lngSaveFeeAndMedSortRel(DataTable p_dtRelation, DataTable p_dtDel)
        {
            //com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsFeeAndMedSortRelSvc));
            long lngRes;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngSaveFeeAndMedSortRel(p_dtRelation, p_dtDel);
                p_dtRelation.AcceptChanges();
                p_dtDel = null;
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
    }
}
