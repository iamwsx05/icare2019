using System;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsDomainController_StatManage ��ժҪ˵����
    /// </summary>
    public class clsDomainController_StatManage : com.digitalwave.GUI_Base.clsDomainController_Base
    { 
        #region ���캯��
        public clsDomainController_StatManage()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ����������ѯѧ��ͳ�Ƶ���Ϣ ͯ�� 2005.01.17
        public long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo, string p_strAgeFrom, string p_strAgeTo,
            string p_strSex, clsLisScienceStatItemQueryCondition[] p_objRecordArr, out DataTable dtbHead, out DataTable dtbDetail)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetScienceStatByCondition(p_strDatFrom, p_strDatTo, p_strAgeFrom, p_strAgeTo, p_strSex,
                p_objRecordArr, out dtbHead, out dtbDetail);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����������ѯѧ��ͳ�Ƶ���Ϣ 2004.12.16
        public long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo, string p_strAgeFrom, string p_strAgeTo,
            string p_strSex, clsLisScienceStatItemQueryCondition[] p_objRecordArr, out DataTable dtbResult)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetScienceStatByCondition(p_strDatFrom, p_strDatTo, p_strAgeFrom, p_strAgeTo, p_strSex,
               p_objRecordArr, out dtbResult);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ����������ѯѧ��ͳ�Ƶ���Ϣ ͯ�� 2004.12.03
        public long m_lngGetScienceStatByCondition(string p_strDatFrom, string p_strDatTo, string p_strCheckItemID, string p_strResultFrom,
            string p_strResultTo, string p_strAgeFrom, string p_strAgeTo, string p_strSex, string p_strLowCompare, string p_strCondition,
            string p_strUpCompare, out DataTable p_DataTable)
        {
            long lngRes = 0;

            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetScienceStatByCondition(p_strDatFrom, p_strDatTo, p_strCheckItemID, p_strResultFrom, p_strResultTo,
                p_strAgeFrom, p_strAgeTo, p_strSex, p_strLowCompare, p_strCondition, p_strUpCompare, out p_DataTable);
            //			objSvc.Dispose();

            return lngRes;
        }
        #endregion

        #region ������û���ͳ�� ͯ�� 2004.09.23
        public long m_lngGetCheckPriceTotalReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetCheckPriceTotalReport(p_strDatFrom, p_strDatTo, p_strOprID, out p_dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������ϸͳ�� ͯ�� 2004.09.23
        public long m_lngGetCheckPriceDetailReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetCheckPriceDetailReport(p_strDatFrom, p_strDatTo, p_strOprID, out p_dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������ͳ����ϸ ͯ�� 2004.09.23
        public long m_lngGetStatDetailReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetStatDetailReport(p_strDatFrom, p_strDatTo, p_strOprID, out p_dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������ͳ�ƻ��� ͯ�� 2004.09.22
        public long m_lngGetStatTotalReport(string p_strDatFrom, string p_strDatTo, string p_strOprID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetStatTotalReport(p_strDatFrom, p_strDatTo, p_strOprID, out p_dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������ģ��

        #region ��ѯ���еĹ�������Ϣ ͯ�� 2004.09.17
        public long m_lngGetAllWorkGroupInfo(out DataTable p_dtbResult)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllWorkGroupInfo(out p_dtbResult);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ���еĹ�������Ϣ ͯ�� 2004.09.16
        public long m_lngGetAllWorkGroupInfo(out clsLisWorkGroup_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllWorkGroupInfo(out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���������� ͯ�� 2004.09.16
        public long m_lngAddNewWorkGroup(clsLisWorkGroup_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngAddNewWorkGroup(p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���¹�������Ϣ ͯ�� 2004.09.16
        public long m_lngModifyWorkGroup(clsLisWorkGroup_VO p_objRecord)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngModifyWorkGroup(p_objRecord);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ����������Ϣ ͯ�� 2004.09.16
        public long m_lngDelWorkGroup(string p_strWorkGroupID, string p_strStatus)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngDelWorkGroup(p_strWorkGroupID, p_strStatus);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #endregion

        #region ͳ����ģ��
        #region ��ȡ���е�ͳ������Ϣ ͯ�� 2004.09.17
        public long m_lngGetAllStatGroupInfo(out clsLisStatGroup_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllStatGroupInfo(out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���е�ͳ�������뵥Ԫ��Ϣ ͯ�� 2004.09.17
        public long m_lngGetAllStatGroupUnitInfo(out clsLisStatGroupUnit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetAllStatGroupUnitInfo(out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ͳ���鼰����ϸ ͯ�� 2004.09.20
        public long m_lngAddNewStatGroup(clsLisStatGroup_VO p_objStatGroup, clsLisStatGroupUnit_VO[] p_objStatGroupUnitArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngAddNewStatGroup(p_objStatGroup, p_objStatGroupUnitArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ͳ����ID��ȡ�����µ����뵥Ԫ��Ϣ ͯ�� 2004.09.20
        public long m_lngGetApplUnitByStatGroupID(string p_strStatGroupID, out clsApplUnit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngGetApplUnitByStatGroupID(p_strStatGroupID, out p_objResultArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ͳ���鼰��ϸ ͯ�� 2004.09.20
        public long m_lngModifyStatGroup(clsLisStatGroup_VO p_objStatGroup, clsLisStatGroupUnit_VO[] p_objStatGroupUnitArr)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngModifyStatGroup(p_objStatGroup, p_objStatGroupUnitArr);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��ͳ���� ͯ�� 2004.09.20
        public long m_lngDelStatGroup(string p_strStatGroupID)
        {
            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyLis02()).Service.m_lngDelStatGroup(p_strStatGroupID);
            //			objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #endregion

        #region ͳ������������
        /// <summary>
        /// ͳ������������
        /// </summary>
        /// <param name="p_datStart"></param>
        /// <param name="p_datEnd"></param>
        /// <param name="p_dtStatisResult"></param>
        public void m_mthGetDeviceCheckStatis(DateTime p_datStart, DateTime p_datEnd, out DataTable p_dtStatisResult)
        {
            (new weCare.Proxy.ProxyLis02()).Service.m_mthGetDeviceCheckStatis(p_datStart, p_datEnd, out p_dtStatisResult);
        }
        #endregion
    }
}
