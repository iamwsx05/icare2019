using System;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlMedCommonUse 的摘要说明。
    /// </summary>
    public class clsDomainControlMedCommonUse : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlMedCommonUse()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public long GetMedBseInfo(string p_strMedSort, string p_strMedShape, out System.Data.DataTable p_outdtResult)
        {
            p_outdtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc));
            long lngRes;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetMedBseInfo(p_strMedSort, p_strMedShape, out p_outdtResult);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }

        public long GetPrjBseInfo(string p_strMedSort, out System.Data.DataTable p_outdtResult)
        {
            p_outdtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc));
            long lngRes;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetPrjBseInfo(p_strMedSort, out p_outdtResult);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }

        public long GetMedSort(out System.Data.DataTable p_outdtResult)
        {
            p_outdtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc));
            long lngRes;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetMedSort(out p_outdtResult);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
        public long GetMedShape(out System.Data.DataTable p_outdtResult)
        {
            p_outdtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc));
            long lngRes;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP01()).Service.m_lngGetMedShape(out p_outdtResult);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
        public long GetMedCommonUseInfo(weCare.Core.Entity.clsLoginInfo p_loginInfo, out System.Data.DataTable p_outdtResult)
        {
            p_outdtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc));
            long lngRes;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP01()).Service.GetMedCommonUseInfo(p_loginInfo, out p_outdtResult);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
        //GetPrjCommonUseInfo

        public long GetPrjCommonUseInfo(weCare.Core.Entity.clsLoginInfo p_loginInfo, out System.Data.DataTable p_outdtResult)
        {
            p_outdtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc));
            long lngRes;
            try
            {
                lngRes = (new weCare.Proxy.ProxyOP01()).Service.GetPrjCommonUseInfo(p_loginInfo, out p_outdtResult);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return -1;
            }
            return lngRes;
        }
        public long SaveMedCommonUseInfo(System.Data.DataTable p_SrcDt, System.Data.DataTable p_DelDt, string strType)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc));
            long lngRes;
            lngRes = (new weCare.Proxy.ProxyOP01()).Service.SavePrjCommonUseInfo(p_SrcDt, p_DelDt, strType);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        public bool IsHasPrescriptionRight(string p_strEmpID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc objSvc = 
            //	(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedCommonUseSvc));
            bool bRes;
            try
            {
                bRes = (new weCare.Proxy.ProxyOP01()).Service.m_bIsHasPrescriptionRight(p_strEmpID);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return false;
            }
            return bRes;
        }
    }
}
