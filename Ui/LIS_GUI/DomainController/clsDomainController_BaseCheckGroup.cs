using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    #region ����������ĳ��󣬷�װ����������������߼� ���� 2004.05.07
    /// <summary>
    /// ����������ĳ��󣬷�װ����������������߼�
    /// ���� 2004.05.07
    /// </summary>
    public class clsDomainController_BaseCheckGroup : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        private string m_strBaseCheckGroupID;
        public clsDomainController_BaseCheckGroup(string p_strBaseCheckGroupID)
        {
            m_strBaseCheckGroupID = p_strBaseCheckGroupID;
        }

        #region ��������������ID��ѯ���п������Ļ���������
        /// <summary>
        /// ��������������ID��ѯ���п������Ļ���������
        /// </summary>
        /// <param name="p_strSampleTypeID"></param>
        /// <param name="p_dtbGroup"></param>
        /// <returns></returns>
        public static long s_lngGetBaseCheckGroupBySampleTypeID(string p_strSampleTypeID, out System.Data.DataTable p_dtbGroup)
        {
            p_dtbGroup = null;
            long lngRet = 0;
            System.Security.Principal.IPrincipal objPrinipal = null;
            try
            {
                lngRet = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetBaseCheckGroupBySampleTypeID(p_strSampleTypeID, out p_dtbGroup);
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return lngRet;
        }
        #endregion

        #region ���ݻ���������ID�������������������е���������ID 
        /// <summary>
        /// ���ݻ���������ID�������������������е���������ID ����
        /// </summary>
        /// <param name="p_objPrinipal"></param>
        /// <param name="p_strBaseCheckGroupID"></param>
        /// <param name="p_dtbDeviceModel"></param>
        /// <returns>
        /// DEVICE_MODEL_ID_CHR
        /// DEVICE_MODEL_DESC_VCHR
        /// </returns>
        public static long s_lngGetDeviceModelByBaseCheckGroupID(string p_strBaseCheckGroupID, out System.Data.DataTable p_dtbDeviceModel)
        {
            long lngRes = 0;
            p_dtbDeviceModel = null;
            System.Security.Principal.IPrincipal objPrinipal = null;

            try
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetDeviceModelByBaseCheckGroupID(p_strBaseCheckGroupID, out p_dtbDeviceModel);
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return lngRes;
        }
        public long m_lngGetDeviceModelByBaseCheckGroupID(out System.Data.DataTable p_dtbDeviceModel)
        {
            return clsDomainController_BaseCheckGroup.s_lngGetDeviceModelByBaseCheckGroupID(m_strBaseCheckGroupID, out p_dtbDeviceModel);
        }
        #endregion

    }
    #endregion

    public class clsDomainController_LisCheckGroupManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region ��ȡ���еı걾���б� ͯ�� 2004.05.24
        public long m_lngGetAllSampleGroup(out weCare.Core.Entity.clsSampleGroup_VO[] objSampleGroupVOList)
        {
            long lngRes = 0;
            objSampleGroupVOList = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllSampleGroup(ref objSampleGroupVOList);

            return lngRes;
        }
        #endregion

        #region ��ȡ���еı����鼰����ϸ���� ͯ�� 2004.05.25(go)
        public long m_lngGetAllReportGroup(out weCare.Core.Entity.clsReportGroup_VO[] objReportGroupVOList)
        {
            long lngRes = 0;
            objReportGroupVOList = null;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllReportGroup(ref objReportGroupVOList);

            return lngRes;
        }
        #endregion

        #region ��ȡĳһ�걾���µ����м�����Ŀ��Ϣ ͯ�� 2004.05.25(go)
        public long m_lngGetCheckItemBySampleGroupID(string strSampleGroupID, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            dtbCheckItem = null;
            lngRes = proxy.Service.m_lngGetCheckItemBySampleGroupID(strSampleGroupID, out dtbCheckItem);

            return lngRes;
        }
        #endregion

        #region ����걾�鼰����ϸ ͯ�� 2004.05.25(go)
        public long m_lngAddSampleGroup(ref clsSampleGroup_VO objSampleGroupVO, ref clsSampleGroupDetail_VO[] objSampleGroupDetailVOList)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddSampleGroupAndDetail(ref objSampleGroupVO, ref objSampleGroupDetailVOList);

            return lngRes;
        }
        #endregion

        #region ɾ���걾�鼰����ϸ ͯ�� 2004.05.25(go)
        public long m_lngDelSampleGroupAndDetail(string strSampleGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelSampleGroupAndDetail(strSampleGroupID);

            return lngRes;
        }
        #endregion

        #region ɾ����������ϸ ͯ�� 2004.05.25(go)
        public long m_lngDelSampleGroupDetail(string strSampleGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelSampleGroupDetail(strSampleGroupID);

            return lngRes;
        }
        #endregion

        #region ���汨���鼰����ϸ ͯ�� 2004.05.26(go)
        public long m_lngAddReportGroupAndDetail(ref clsReportGroup_VO objReportGroupVO, ref clsReportGroupDetail_VO[] objReportGroupDetailVOList)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddReportGroupAndDetail(ref objReportGroupVO, ref objReportGroupDetailVOList);

            return lngRes;
        }
        #endregion

        #region ɾ�������鼰����ϸ ͯ�� 2004.05.26(go)
        public long m_lngDelReportGroupAndDetail(string strReportGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelReportGroupAndDetail(strReportGroupID);

            return lngRes;
        }
        #endregion

        #region ɾ����������ϸ ͯ�� 2004.05.26(go)
        public long m_lngDelReportGroupDetail(string strReportGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDelReportGroupDetail(strReportGroupID);

            return lngRes;
        }
        #endregion

        #region ��ȡ���еı걾����ϸ�б� ͯ�� 2004.05.26(go)
        public long m_lngGetAllSampleGroupDetail(out clsSampleGroupDetail_VO[] objSampleGroupDetailVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllSampleGroupDetail(out objSampleGroupDetailVOList);

            return lngRes;
        }
        #endregion

        #region ��ȡ���еı�������ϸ�б� ͯ�� 2004.05.26(go)
        public long m_lngGetAllReportGroupDetail(out clsReportGroupDetail_VO[] objReportGroupDetailVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllReportGroupDetail(out objReportGroupDetailVOList);

            return lngRes;
        }
        #endregion

        #region ��ȡ���е����뵥Ԫ ͯ�� 2004.05.26
        public long m_lngGetAllApplUnit(out clsApplUnit_VO[] objApplUnitVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllApplUnit(out objApplUnitVOList);

            return lngRes;
        }
        #endregion

        #region ��ȡ���е��û��Զ����� ͯ�� 2004.05.26(ת��)
        public long m_lngGetAllUserGroup(out clsApplUserGroup_VO[] objApplUserGroupList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllUserGroup(out objApplUserGroupList);

            return lngRes;
        }
        #endregion

        #region �����û��Զ���ID��ѯ��������������뵥Ԫ ͯ�� 2004.05.26(��ת��)
        public long m_lngGetApplUnitByUserGroupID(string strUserGroupID, out clsApplUnit_VO[] objApplUnit)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetApplUnitByUserGroupID(strUserGroupID, out objApplUnit);

            return lngRes;
        }
        #endregion

        #region �����û��Զ���ID��ѯ��������������� ͯ�� 2004.05.26(��ת��)
        public long m_lngGetSubGroupByUserGroupID(string strUserGroupID, out clsApplUserGroup_VO[] objApplUserGroupVOList)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngGetChildGroupByUserGroupID(strUserGroupID, out objApplUserGroupVOList);

            return lngRes;
        }
        #endregion

        #region �������뵥ԪID��ѯ���еļ�����Ŀ��Ϣ ͯ�� 2004.05.26(��ת��)
        public long m_lngGetCheckItemByApplUnitID(string strApplUnitID, out DataTable dtbCheckItem)
        {
            long lngRes = 0;
            dtbCheckItem = null;
            lngRes = proxy.Service.m_lngGetCheckItemByApplUnitID(strApplUnitID, out dtbCheckItem);
            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���е����뵥Ԫ��ϸ ͯ�� 2004.05.27
        public long m_lngGetAllApplUnitDetail(out clsApplUnitDetail_VO[] objApplUnitDetailVOList)
        {
            long lngRes = 0;
            lngRes = proxy.Service.m_lngGetAllApplUnitDetail(out objApplUnitDetailVOList);

            return lngRes;
        }
        #endregion
    }

}
