using System;
using com.digitalwave.iCare.common;
using com.digitalwave.Utility;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// Summary description for clsController_SampleTypeInfo.
    /// </summary>
    public class clsController_SampleTypeInfo : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsController_SampleTypeInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        public long AddNewSample(string strSampleType, string strPyCode, string strWbCode, int intHasBarCode, out string strSampleTypeID)
        {
            long lngRes = 0;
            clsSampleType_VO objSampleTypeVO = new clsSampleType_VO();
            objSampleTypeVO.m_strPyCode = strPyCode;
            objSampleTypeVO.m_strSample_Type_Desc = strSampleType;
            objSampleTypeVO.m_strWbCode = strWbCode;
            objSampleTypeVO.m_intHasBarCode = intHasBarCode;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddSampleType(ref objSampleTypeVO);
            strSampleTypeID = objSampleTypeVO.m_strSample_Type_ID;
            //			objSampleTypeSvc.Dispose();
            return lngRes;
        }

        public long DelSampleType(string strSampleTypeID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelSampleTypeBySampleTypeID(strSampleTypeID);
            //			objSampleTypeSvc.Dispose();
            return lngRes;
        }

        public long SetSampleTypeDetail(string strSampleType, string strPyCode, string strWbCode, string strSampleTypeID, int intHasCode)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetSampleTypeDetailBySampleTypeID(strSampleType, strPyCode, strWbCode, strSampleTypeID, intHasCode);
            //			objSampleTypeSvc.Dispose();
            return lngRes;
        }

        //查询某一样品类别的所有样品性状
        public long QryAllSampleCharacter(out System.Data.DataTable dtbAllSampleCharacter, string strSampleTypeID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetAllSampleCharacter(strSampleTypeID, out dtbAllSampleCharacter);
            //			objSampleCharacterSvc.Dispose();
            return lngRes;
        }

        //查询所有的样品类别
        public long QryAllSampleType(out System.Data.DataTable dtbAllSampleType)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllSampleType(out dtbAllSampleType);
            //			objSampleCharacterSvc.Dispose();
            return lngRes;
        }

        //新增样本性状记录
        public long AddNewSampleCharacter(string strSampleCharacter, string strPyCode, string strWbCode, string strSampleTypeID, ref string strSEQ)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            clsSampleCharacter_VO objSampleCharacterVO = new clsSampleCharacter_VO();
            objSampleCharacterVO.m_strCharacter_Desc = strSampleCharacter;
            objSampleCharacterVO.m_strPyCode = strPyCode;
            objSampleCharacterVO.m_strSample_Type_ID = strSampleTypeID;
            objSampleCharacterVO.m_strwbCode = strWbCode;
            objSampleCharacterVO.m_strCharacter_Ord = strSEQ;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddSampleCharacterBySampleTypeID(ref objSampleCharacterVO);
            //			objSampleCharacterSvc.Dispose();
            strSEQ = objSampleCharacterVO.m_strCharacter_Ord;
            return lngRes;
        }

        //更样本性状记录
        public long SetSampleCharacter(string strSampleCharacter, string strPyCode, string strWbCode, string strSampleTypeID, string strSEQ)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngSetSampleCharacterBySampleTypeIDAndSEQ(strSampleTypeID, strSEQ, strSampleCharacter,
               strPyCode, strWbCode);
            //			objSampleCharacterSvc.Dispose();
            return lngRes;
        }

        //删除样本性状记录
        public long DelSampleCharacter(string strSEQ, string strSampleTypeID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelSampleCharacterBySampleTypeIDAndSEQ(strSampleTypeID, strSEQ);
            //			objSampleCharacterSvc.Dispose();
            return lngRes;
        }
    }
}
