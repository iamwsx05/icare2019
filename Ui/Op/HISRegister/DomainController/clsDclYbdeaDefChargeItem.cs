using System;
using System.Data;
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �ض����ֶ�Ӧ�շ���Ŀά��ҵ����Ʋ�
    /// ���ߣ�He Guiqiu
    /// ����ʱ��:2006-06-24
    /// </summary>
    class clsDclYbdeaDefChargeItem : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDclYbdeaDefChargeItem()
        {
            //
        }

        #region ȡҽ�����ֲ�
        public long GetSpecialDisease(out DataTable p_dtResult)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.GetSpecialDisease(out p_dtResult);

            //objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ȡ�շ���Ŀ
        public long GetChargeItem(out DataTable p_dtResult)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.GetChargeItem(out p_dtResult);

            //objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����ҽ����ȡ��Ӧ���շ���Ŀ
        public long GetChargeItemByDeaCode(string p_strDeaCode, out DataTable p_dtResult)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.GetChargeItemByDeaCode(p_strDeaCode, out p_dtResult);

            //objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����
        public long SaveDeaDefChargeItem(string p_strDeaCode, System.Collections.Generic.List<string> p_newArr, System.Collections.Generic.List<string> p_removeArr)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYbdeaDefChargeitemSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.SaveDeaDefChargeItem(p_strDeaCode, p_removeArr, p_newArr);

            //objSvc.Dispose();

            return lngRes;
        }
        #endregion


    }

}
