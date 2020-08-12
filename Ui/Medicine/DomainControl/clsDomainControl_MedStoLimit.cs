using System;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.DLL
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControl_MedStoLimit:���ݿ����� Create by Sam 2004-5-24
    /// </summary>
    public class clsDomainControl_MedStoLimit : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControl_MedStoLimit()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ��ȡ���еĲֿ�
        /// <summary>
        /// ��ȡ���еĲֿ�
        /// </summary>
        public long m_lngGetStorageList(out DataTable strStorageArr)
        {
            strStorageArr = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageList(out strStorageArr);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���еĲֿ�����
        /// <summary>
        /// ��ȡ���еĲֿ�����
        /// </summary>
        public long m_lngGetStorageTypeList(out DataTable strStorageTypeArr)
        {
            strStorageTypeArr = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetStorageTypeList(out strStorageTypeArr);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݲֿ�IDȡ��ҩƷ�޶�
        /// <summary>
        /// ���ݲֿ�IDȡ��ҩƷ�޶�
        /// </summary>
        public long m_lngGetLimitByStoID(string strStoID, string p_strStorageFlag, out DataTable p_dbtResultArr)
        {
            p_dbtResultArr = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindLimitByStoID(strStoID, p_strStorageFlag, out p_dbtResultArr);
            return lngRes;
        }
        #endregion

        #region �������ͻ�ȡҩƷ��������
        /// <summary>
        /// �������ͻ�ȡҩƷ��������
        /// </summary>
        public long m_lngGetMedListByType(string strMedTypeID, out DataTable dbtMedArr)
        {
            dbtMedArr = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedListByType(strMedTypeID, out dbtMedArr);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region Add�޶�
        /// <summary>
        /// Add�޶�
        /// </summary>
        public long m_lngAddLimit(DataTable AddRow)
        {
            //p_objResultArr = new clsStorageMedLimit_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewLimit(AddRow);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸��޶�
        /// <summary>
        /// �޸��޶�
        /// </summary>
        public long m_lngUpLimit(DataTable UpdataRow)
        {
            //p_objResultArr = new clsStorageMedLimit_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdLimitByKey(UpdataRow);
            return lngRes;
        }
        #endregion

        #region ɾ���޶�
        /// <summary>
        /// ɾ���޶�
        /// </summary>
        public long m_lngDelLimit(string strStorageID, string strMedID)
        {
            //p_objResultArr = new clsStorageMedLimit_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = objPrincipal;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDeleteLimitByKey(strStorageID, strMedID);
            //objSvc.Dispose();
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ�ֿ⾯�䱨��
        public long m_lngGetMedWatchRpt(string p_strStorageID, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageLimitSvc));
            try
            {
                lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMedWatchRpt(p_strStorageID, out dtResult);
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion ��ȡ�ֿ⾯�䱨��
    }
}
