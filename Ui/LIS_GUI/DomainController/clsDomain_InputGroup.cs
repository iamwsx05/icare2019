using System;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// 
    /// </summary>
    public class clsDomain_InputGroup : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        weCare.Proxy.ProxyLis proxy
        {
            get
            {
                return new weCare.Proxy.ProxyLis();
            }
        }

        #region ���캯��
        public clsDomain_InputGroup()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ��ȡɸѡ�����Ŀ�б�
        public long m_lngGetFiltedItems(string[] p_strApplyUnitIDArr, string[] p_strInputGroupIDArr, out string[] p_strItemResultArr)
        {
            long lngRes = 0;
            p_strItemResultArr = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetFiltedItems(p_strApplyUnitIDArr, p_strInputGroupIDArr, out p_strItemResultArr);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion

        #region ��ȡָ�����뵥Ԫ���������б�
        public long m_lngGetApplyUnitInfo(string[] p_strApplyUnitIDArr, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetApplyUnitInfo(p_strApplyUnitIDArr, out p_dtbResult);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion


        #region ��ȡָ�����뵥Ԫ�¿��õ�¼�����
        public long m_lngGetInputGroupsByUnit(string[] p_strApplyUnitIDArr, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetInputGroupsByUnit(p_strApplyUnitIDArr, out p_dtbResult);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion


        #region ��ȡ�������-���뵥Ԫ-¼����ϵĵ�������Ϣ
        public long m_lngGetUnitedInputGroupInfo(out clsInputGroupUnited_VO[] p_objResults)
        {
            long lngRes = 0;
            p_objResults = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetUnitedInputGroupInfo(out p_objResults);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion

        #region ��ȡ���뵥Ԫ��Ŀ��ϸ
        public long m_lngGetApplyUnitItems(string p_strApplyUnitID, out clsCheckItemSimple_VO[] p_objResults)
        {
            long lngRes = 0;
            p_objResults = null;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetApplyUnitItems(p_strApplyUnitID, out p_objResults);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion

        #region ��ȡ¼����ϼ���ϸ
        public long m_lngGetInputGroupInfo(string p_strInputGroupID, out clsInputGroupBaseInfo_VO p_objBaseInfo, out clsInputGroupDetail_VO[] p_objResults)
        {
            long lngRes = 0;
            p_objResults = null;
            p_objBaseInfo = null;

            try
            {
                lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetInputGroupInfo(p_strInputGroupID, out p_objBaseInfo, out p_objResults);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion

        #region ����¼�����
        public long m_lngAddNewInputGroup(clsInputGroupBaseInfo_VO p_objBaseInfo, clsInputGroupDetail_VO[] p_objResults, out string strID)
        {
            long lngRes = 0;
            strID = null;

            try
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngAddNewInputGroup(p_objBaseInfo, p_objResults, out strID);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion

        #region ����¼�����
        public long m_lngUpdateInputGroup(clsInputGroupBaseInfo_VO p_objBaseInfo, clsInputGroupDetail_VO[] p_objResults)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngUpdateInputGroup(p_objBaseInfo, p_objResults);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion

        #region ɾ��¼�����
        public long m_lngDeleteInputGroup(string strGroupID)
        {
            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyLis01()).Service.m_lngDeleteInputGroup(strGroupID);
            }
            catch
            {

            }

            return lngRes;
        }
        #endregion

    }

}