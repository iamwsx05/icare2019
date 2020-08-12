using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDomainControlImpExpType : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public long m_lngGetAllType(out clsImpExpType_VO[] objTypes)
        {
    //        com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc objSvc =
    //(com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc));
            return (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllType(out objTypes);
        }

        public long m_lngInsertData(clsImpExpType_VO objVO)
        {
//            com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc objSvc =
//(com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc));
            return (new weCare.Proxy.ProxyMedStore()).Service.m_lngInsertData(objVO);
        }

        public long m_lngUpdate(clsImpExpType_VO objVO, string oldCode)
        {
//            com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc objSvc =
//(com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc));
            return (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdate(objVO, oldCode);
        }

        public long m_lngUpdateStatus(int Status, string Typecode)
        {
//            com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc objSvc =
//(com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsImpExpTypeSvc));
            return (new weCare.Proxy.ProxyMedStore()).Service.m_lngUpdateStatus(Status, Typecode);
        }
    }
}
