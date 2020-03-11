using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base;


namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// ø������Ŀ����Domain��
    /// </summary>
    public class clsDomainController_MK3ItemSetManage : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ��ȡ���еļ�����Ŀ
        /// <summary>
        /// ��ȡ���еļ�����Ŀ
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngGetAllCheckItem(string p_strDeviceModelID, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;

            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetAllCheckItem(p_strDeviceModelID, out p_dtResult);
            return lngRes;
        }
        #endregion

        #region ��ȡ�����Զ�����Ŀ����Ϣ
        /// <summary>
        /// ��ȡ�����Զ�����Ŀ����Ϣ
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngGetAllCheckItemCustomInfo(out clsLisCheckItemCustom[] p_objCheckItemCustomVO, out DataTable p_dtResult)
        {
            p_objCheckItemCustomVO = null;
            p_dtResult = null;
            long lngRes = 0;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetAllCheckItemCustomInfo(out p_objCheckItemCustomVO, out p_dtResult);
            return lngRes;
        }
        #endregion

        #region �޸��Զ�����Ŀ
        /// <summary>
        /// �޸��Զ�����Ŀ
        /// </summary>
        /// <param name="p_objCheckItemCustomVO"></param>
        /// <returns></returns>
        public long m_lngUpdateCheckItemCustom(clsLisCheckItemCustom p_objCheckItemCustomVO)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateCheckItemCustom(p_objCheckItemCustomVO);
            return lngRes;
        }
        #endregion

        #region ɾ���Զ�����Ŀ
        /// <summary>
        /// ɾ���Զ�����Ŀ
        /// </summary>
        /// <param name="p_strCheckItemID"></param>
        /// <returns></returns>
        public long m_lngDelteCheckItemCustom(string p_strCheckItemID)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngDelteCheckItemCustom(p_strCheckItemID);
            return lngRes;
        }
        #endregion

        #region ����Զ�����Ŀ
        /// <summary>
        /// ����Զ�����Ŀ
        /// </summary>
        /// <param name="p_objCheckItemCustomVO"></param>
        /// <returns></returns>
        public long m_lngInsertCheckItemCustom(clsLisCheckItemCustom p_objCheckItemCustomVO)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertCheckItemCustom(p_objCheckItemCustomVO);
            return lngRes;
        }
        #endregion

        #region ��ȡ�Զ�����Ŀ�Ľ���ж�
        /// <summary>
        /// ��ȡ�Զ�����Ŀ�Ľ���ж�
        /// </summary>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objCheckItemCustomRes"></param>
        /// <returns></returns>
        public long m_lngQueryCheckItemCustomRes(string p_strCheckItemID, out clsLisCheckItemCustomRes[] p_objCheckItemCustomRes)
        {
            long lngRes = 0;
            p_objCheckItemCustomRes = null;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryCheckItemCustomRes(p_strCheckItemID, out p_objCheckItemCustomRes);
            return lngRes;
        }
        #endregion

        #region ����Զ������ж�
        /// <summary>
        /// ����Զ������ж�
        /// </summary>
        /// <param name="p_objCheckItemCustomResVO"></param>
        /// <returns></returns>
        public long m_lngInsertCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertCheckItemCustomRes(p_objCheckItemCustomResVO);
            return lngRes;
        }
        #endregion

        #region �޸��Զ������ж�
        /// <summary>
        /// �޸��Զ������ж�
        /// </summary>
        /// <param name="p_objCheckItemCustomResVO"></param>
        /// <returns></returns>
        public long m_lngUpdateCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateCheckItemCustomRes(p_objCheckItemCustomResVO);
            return lngRes;
        }
        #endregion

        #region ɾ���Զ������ж�
        /// <summary>
        /// ɾ���Զ������ж�
        /// </summary>
        /// <param name="p_objCheckItemCustomResVO"></param>
        /// <returns></returns>
        public long m_lngDeleteCheckItemCustomRes(clsLisCheckItemCustomRes p_objCheckItemCustomResVO)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngDeleteCheckItemCustomRes(p_objCheckItemCustomResVO);
            return lngRes;
        }
        #endregion

        #region ���Ӽ����������, ������
        /// <summary>
        /// ���Ӽ����������, ������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        public long lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(p_objResultArr, true, out p_objOutResultArr);
            return lngRes;
        }
        #endregion

        #region ���Ӽ����������
        /// <summary>
        /// ���Ӽ����������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        public long lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(p_objResultArr, out p_objOutResultArr);
            return lngRes;
        }
        #endregion

        #region ��ȡ������Ŀ����
        /// <summary>
        /// ��ȡ������Ŀ����
        /// </summary>
        /// <param name="p_strDevicModelID"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQueryDevceCheckItem(string p_strDevicModelID, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryDevceCheckItem(p_strDevicModelID, out p_dtResult);
            return lngRes;
        }
        #endregion

        #region ��ȡ�������ƺͲ�����Ϣ
        /// <summary>
        /// ��ȡ�������ƺͲ�����Ϣ
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <param name="p_dtLayoutInfo"></param>
        /// <returns></returns>
        public long m_lngGetAllItemLayout(out DataTable p_dtResult, out DataTable p_dtLayoutInfo)
        {
            p_dtLayoutInfo = null;
            p_dtLayoutInfo = null;
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //   (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetAllItemLayout(out p_dtResult, out p_dtLayoutInfo);
            return lngRes;
        }
        #endregion

        #region �����µĲ���
        /// <summary>
        /// �����µĲ���
        /// </summary>
        /// <param name="p_objLisItemLayoutVO"></param>
        /// <returns></returns>
        public long m_lngAddItemLayout(clslisItemLayout[] p_objLisItemLayoutVO)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngAddItemLayout(p_objLisItemLayoutVO);
            return lngRes;
        }
        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="p_strLayoutName"></param>
        /// <returns></returns>
        public long m_lngDeleteItemLayout(string p_strLayoutName)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngDeleteItemLayout(p_strLayoutName);
            return lngRes;
        }
        #endregion

        #region ���ݰ��ӽ�����Ʋ�ѯ��ذ��ӽ������
        /// <summary>
        /// ���ݰ��ӽ�����Ʋ�ѯ��ذ��ӽ������
        /// </summary>
        /// <param name="p_strPlateName"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQueryPlateName(string p_strPlateName, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryPlateName(p_strPlateName, p_strStartDate, p_strEndDate, out p_dtResult);
            return lngRes;
        }
        #endregion

        #region ���ݰ�������ID��ѯ���ӽ��
        /// <summary>
        /// ���ݰ�������ID��ѯ���ӽ��
        /// </summary>
        /// <param name="p_strPlateNameID"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQueryPlateResult(string p_strPlateNameID, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryPlateResult(p_strPlateNameID, out p_dtResult);
            return lngRes;
        }
        #endregion

        #region ������Ӳ��ֽ��
        /// <summary>
        /// ������Ӳ��ֽ��
        /// </summary>
        /// <param name="p_strPlateName"></param>
        /// <param name="p_objPlateResultArr"></param>
        /// <returns></returns>
        public long m_lngInsertPlateResult(string p_strPlateName, string p_strPlateChName, clslisPlateResult[] p_objPlateResultArr, out string p_strPlateResultID)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertPlateResult(p_strPlateName, p_strPlateChName, p_objPlateResultArr, out p_strPlateResultID);
            return lngRes;
        }
        #endregion

        #region ɾ�����ӽ��
        /// <summary>
        /// ɾ�����ӽ��
        /// </summary>
        /// <param name="p_strPlateNameID"></param>
        /// <returns></returns>
        public long m_lngDeletePlateResult(string p_strPlateNameID)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngDeletePlateResult(p_strPlateNameID);
            return lngRes;
        }
        #endregion

        #region ��ȡ���а��ӽ��
        /// <summary>
        /// ��ȡ���а��ӽ��
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngGetAllPlateResult(out DataTable p_dtResult)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngGetAllPlateResult(out p_dtResult);
            return lngRes;
        }
        #endregion

        #region ���������
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngInsertDeviceResult(clsLIS_Device_Test_ResultVO[] p_objResultArr)
        {
            long lngRes = 0;
            //clsItemSetSvc objSvc =
            //    (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertDeviceResult(p_objResultArr);
            return lngRes;
        }
        #endregion


        #region ��ȡ�Զ�����Ŀ�Ľ���жϹ�ʽ   
        /// <summary>
        /// ��ȡ�Զ�����Ŀ�Ľ���ж�
        /// </summary>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objCheckItemCustomRes"></param>
        /// <returns></returns>
        public long m_lngQueryCheckItemCustomRes(string p_strCheckItemID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = null;
            //clsItemSetSvc objSvc = (clsItemSetSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsItemSetSvc));
            lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngQueryCheckItemCustomRes(p_strCheckItemID, out p_dtResult);
            return lngRes;
        }
        #endregion
    }
}
