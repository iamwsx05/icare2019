using System;
using System.Data;
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �ض����ֶ�ӦICD10���ά��ҵ����Ʋ�
    /// ���ߣ�He Guiqiu
    /// ����ʱ��:2006-06-22
    /// </summary>
    class clsDclYbdeadeficd : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDclYbdeadeficd()
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

        #region ȡICD10
        public long GetICD(out DataTable p_dtResult)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.GetICD(out p_dtResult);

            //objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����ҽ����ȡ��Ӧ��ICD10
        public long GetICDByDeaCode(string p_strDeaCode, out DataTable p_dtResult)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.GetICDByDeaCode(p_strDeaCode, out p_dtResult);

            //objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����
        public long SaveDeaDef(string p_strDeaCode, System.Collections.Generic.List<string> p_newArr, System.Collections.Generic.List<string> p_removeArr)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYbdeadeficdSvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.SaveDeaDef(p_strDeaCode, p_removeArr, p_newArr);

            //objSvc.Dispose();

            return lngRes;
        }
        #endregion


    }

}
