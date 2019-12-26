using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDcl_CommonFind : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsDcl_CommonFind()
        {
        }

        weCare.Proxy.ProxyIP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyIP01();
            }
        }
        #endregion              

        #region ���ݲ���ID��ȡ�ò�����λ��Ϣ
        /// <summary>
        /// ���ݲ���ID��ȡ�ò�����λ��Ϣ
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="status"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetBedinfo(string AreaID, int status, out DataTable dt)
        {
            return proxy.Service.m_lngGetBedinfo(AreaID, status, out dt);
        }
        #endregion

        #region ����סԺ�Ż����ƿ��Ż�ȡ��ǰ��Ժ������Ϣ
        /// <summary>
        /// ����סԺ�Ż����ƿ��Ż�ȡ��ǰ��Ժ������Ϣ
        /// </summary>
        /// <param name="no"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetPatientinfoByZyh(string no, out DataTable dt, int type)
        {
            return proxy.Service.m_lngGetPatientinfoByZyh(no, out dt, type);
        }
        #endregion

        #region ͨ�ò��Ҵ�����
        /// <summary>
        /// ͨ�ò��Ҵ�����
        /// </summary>
        /// <param name="SqlWhereZY"></param>
        /// <param name="Status">0 ȫ�� 1 ��Ժ 2 ��Ժ</param>
        /// <param name="IsIncludeMZ"></param>
        /// <param name="SqlWhereMZ"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientinfo(string SqlWhereZY, int Status, bool IsIncludeMZ, string SqlWhereMZ, clsCommonQueryDate_VO CommonQueryDate_VO, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientinfo(SqlWhereZY, Status, IsIncludeMZ, SqlWhereMZ, CommonQueryDate_VO, out dt);
        }
        #endregion

        #region ��ȡƵ���б�
        /// <summary>
        /// ��ȡƵ���б�
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>       
        public long m_lngGetUsageInfo(out DataTable dt)
        {
            return proxy.Service.m_lngGetUsageInfo(out dt);
        }
        #endregion

        #region ��ȡ�÷������շ���Ŀ��Ϣ
        /// <summary>
        /// ��ȡ�÷������շ���Ŀ��Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PatType">�������</param>
        /// <param name="ApplyAreaID">��������ID</param>
        /// <returns></returns>        
        public long m_lngGetUsageAddItem(out DataTable dt, string PatType, string ApplyAreaID)
        {
            return proxy.Service.m_lngGetUsageAddItem(out dt, PatType, ApplyAreaID);
        }
        #endregion

        #region ��ȡ�շ��������Ϣ
        /// <summary>
        /// ��ȡ�շ��������Ϣ
        /// </summary>
        /// <param name="OperID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetItemGroup(string OperID, out DataTable dt)
        {
            return proxy.Service.m_lngGetItemGroup(OperID, out dt);
        }
        #endregion

        #region ��ȡ�շ������ϸ
        /// <summary>
        /// ��ȡ�շ������ϸ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PatType"></param>
        /// <param name="ApplyAreaID"></param>
        /// <returns></returns>        
        public long m_lngGetItemGroupDet(out DataTable dt, string PatType, string ApplyAreaID)
        {
            return proxy.Service.m_lngGetItemGroupDet(out dt, PatType, ApplyAreaID);
        }
        #endregion

        #region ��ȡȫԺ��ǰ��Ժ�����嵥
        /// <summary>
        /// ��ȡȫԺ��ǰ��Ժ�����嵥
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetBihPatient(out DataTable dt)
        {
            return proxy.Service.m_lngGetBihPatient(out dt);
        }
        #endregion

        #region ����סԺ�Ż�ȡ���˻�������
        /// <summary>
        /// ����סԺ�Ż�ȡ���˻�������
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientinfoByZyh(string Zyh, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientinfoByZyh(Zyh, out dt);
        }
        #endregion

        #region ��ȡҽ������
        /// <summary>
        /// ��ȡҽ������
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetOrderCate(out DataTable dt)
        {
            return proxy.Service.m_lngGetOrderCate(out dt);
        }
        #endregion

        #region ͨ����ҩID��ѯ������¼ҽ���Ƿ�����ȡҩ�����ü�ҽ����������
        /// <summary>
        /// ͨ����ҩID��ѯ������¼ҽ���Ƿ�����ȡҩ�����ü�ҽ����������
        /// </summary>
        /// <param name="p_strPutMedDetailID"></param>
        /// <param name="p_blnIfCharge"></param>
        /// <returns></returns>
        public long m_lngQueryIfChargeMedBag(string p_strPutMedDetailID, ref bool p_blnIfCharge, ref string p_strOrderCreateAreaID)
        {
            return proxy.Service.m_lngQueryIfChargeMedBag(p_strPutMedDetailID, ref p_blnIfCharge, ref p_strOrderCreateAreaID);
        }
        #endregion

        #region ���°�ҩ��ϸ����ָ����¼��ҩ��������ȡ״̬
        /// <summary>
        /// ���°�ҩ��ϸ����ָ����¼��ҩ��������ȡ״̬
        /// </summary>
        /// <param name="p_strPutMedDetailID"></param>
        /// <param name="p_intStatus"></param>
        /// <returns></returns>
        public long m_lngUpdateIfChargeMedBag(string p_strPutMedDetailID, int p_intStatus)
        {
            return proxy.Service.m_lngUpdateIfChargeMedBag(p_strPutMedDetailID, p_intStatus);
        }
        #endregion

        #region ��ѯ����ҩ���շѼ�¼�������Ϣ
        /// <summary>
        /// ��ѯ����ҩ���շѼ�¼�������Ϣ
        /// </summary>
        /// <param name="p_strOrderDicID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtOrderInfo"></param>
        /// <param name="p_dtPatientInfo"></param>
        /// <returns></returns>
        public long m_lngQueryInfoForChargeMedBag(string p_strOrderDicID, string p_strInPatientID, out DataTable p_dtOrderInfo, out DataTable p_dtPatientInfo)
        {
            return proxy.Service.m_lngQueryInfoForChargeMedBag(p_strOrderDicID, p_strInPatientID, out p_dtOrderInfo, out p_dtPatientInfo);
        }
        #endregion
    }
}
