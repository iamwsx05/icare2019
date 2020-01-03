using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_ItemCatMapping ��ժҪ˵����
    /// </summary>
    public class clsDcl_ItemCatMapping : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_ItemCatMapping()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP();
            }
        }
        #endregion 

        #region ������listview��ѡ��
        public long m_mthLoadMainListViewItem(out clsChargeItemEXType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngFindChargeItemEXTypeListByFlag("2", out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region �����������
        public long m_mthGetSubjectionCat(out DataTable dt, string strCatID, int flag)
        {
            dt = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc));
            lngRes = proxy.Service.m_mthGetSubjectionCat(out dt, strCatID, flag);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ��������
        public long m_mthSaveData(clsItemCatMapping_VO[] ICM_VO, string strCatID, int flag)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc));
            lngRes = proxy.Service.m_mthSaveData(ICM_VO, strCatID, flag);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ѯҩ����Ϣ
        public long m_mthMedstoreInfo(out DataTable dt, string strExpen)
        {
            dt = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc));
            lngRes = proxy.Service.m_mthMedstoreInfo(out dt, strExpen);
            //objSvc.Dispose();
            return lngRes;
        }

        #endregion
        #region ����ҩ��ID�������
        public long m_mthWindowInfoByID(out DataTable dt, string strExpen)
        {
            dt = null;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsItemCatMappingSvc));
            lngRes = proxy.Service.m_mthWindowInfoByID(out dt, strExpen);
            //objSvc.Dispose();
            return lngRes;
        }

        #endregion
    }
}
