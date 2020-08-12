using System;
using weCare.Core.Entity;
using System.Data;
using System.Collections.Generic;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDcl_YB : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_YB()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        weCare.Proxy.ProxyYB proxy
        {
            get
            {
                return new weCare.Proxy.ProxyYB();
            }
        }

        #region ����ҽ�����㷵����ϢSP3_2004
        /// <summary>
        /// ����ҽ�����㷵����ϢSP3_2004
        /// </summary>
        /// <param name="objDgmzjsfhVo"></param>
        /// <returns></returns>
        public long m_lngSaveYBChargeReturn(string strRegID, clsDGMzjsfh_VO objDgmzjsfhVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngSaveYBChargeReturn(strRegID, objDgmzjsfhVo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ��������贫�������
        /// <summary>
        /// ��ȡ��������贫�������
        /// </summary>
        /// <param name="strRecipeID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetDgmzjsdata(string strRecipeID, out DataTable dtResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetDgmzjsdata(strRecipeID, out dtResult);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ��ȡסԺҽ���Ǽ���������
        /// <summary>
        /// ��ȡסԺҽ���Ǽ���������
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetZYYBRegister(string strRegisterId, out DataTable dtResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetZYYBRegister(strRegisterId, out dtResult);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ����סԺҽ���ǼǷ�����Ϣ
        /// <summary>
        /// ����סԺҽ���ǼǷ�����Ϣ
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strJzjlh"></param>
        /// <returns></returns>
        public long m_lngSaveYBZYRegInfo(string strRegID, string strJzjlh, string jbr, clsDGZydj_VO objDgzydjVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngSaveYBZYRegInfo(strRegID, strJzjlh, jbr, objDgzydjVo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�סԺҽ���ǼǷ�����Ϣ
        /// <summary>
        /// �޸�סԺҽ���ǼǷ�����Ϣ
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strJzjlh"></param>
        /// <returns></returns>
        public long m_lngUpdateYBZYRegInfo(string strRegID, clsDGZydj_VO objDgzydjVo, clsDGExtra_VO objDgextraVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngUpdateYBZYRegInfo(strRegID, objDgzydjVo, objDgextraVo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡסԺҽ������������������
        /// <summary>
        /// ��ȡסԺҽ������������������
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="objDgzydyxsVo"></param>
        /// <returns></returns>
        public long m_lngGetZYYBDyxs(string strJslx, string strRegisterId, ref string strName, ref string strZyh, ref string strStatus, out clsDGZydyxs_VO objDgzydyxsVo, out clsDGZyjsfh_VO objDgzyjsfhVo, out decimal decZyfyze, out decimal decGrzfeije)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetZYYBDyxs(strJslx, strRegisterId, ref strName, ref strZyh, ref strStatus, out objDgzydyxsVo, out objDgzyjsfhVo, out decZyfyze, out decGrzfeije);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ��ȡסԺ������ϸ����
        /// <summary>
        /// ��ȡסԺ������ϸ����
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="lstDgzyxmcsVo"></param>
        /// <param name="p_blnDiffCostOn">��������</param>
        /// <returns></returns>
        public long m_lngGetDgzyxmcs(string strRegisterId, out List<clsDGZyxmcs_VO> lstDgzyxmcsVo, bool p_blnDiffCostOn, decimal decHISTotalSum, List<string> lstPChargeId)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetDgzyxmcs(strRegisterId, out lstDgzyxmcsVo, p_blnDiffCostOn, decHISTotalSum, lstPChargeId);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ��ȡסԺ������ϸ����
        /// <summary>
        /// ��ȡסԺ������ϸ����
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="lstDgzyxmcsVo"></param>
        /// <param name="p_blnDiffCostOn">��������</param>
        /// <returns></returns>
        public long m_lngGetDgzyxmcs(DateTime p_dateBegin, DateTime p_dateEnd, out List<clsDGZyxmcs_VO> lstDgzyxmcsVo, bool p_blnDiffCostOn)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetDgzyxmcs(p_dateBegin, p_dateEnd, out lstDgzyxmcsVo, p_blnDiffCostOn);
            //objSvc.Dispose();
            return l;
        }

        public long m_lngGetDgzyxmcs(DateTime p_dateBegin, DateTime p_dateEnd, out List<clsDGZyxmcs_VO> lstDgzyxmcsVo, bool p_blnDiffCostOn, string jzjlh)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetDgzyxmcs2(p_dateBegin, p_dateEnd, out lstDgzyxmcsVo, p_blnDiffCostOn, jzjlh);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ����סԺ������ϸ����
        /// <summary>
        /// ����סԺ������ϸ����
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public long m_lngUpdateDgzyxmcs(List<clsDGZyxmcs_VO> lstDgzyxmcsVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngUpdateDgzyxmcs(lstDgzyxmcsVo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����סԺ������ϸ����
        /// <summary>
        /// ����סԺ������ϸ����
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public long m_lngUpdateDgzyxmcs(string strRegID)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngUpdateDgzyxmcs(strRegID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡסԺҽ��������������
        /// <summary>
        /// ��ȡסԺҽ��������������
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="objDgzyjsVo"></param>
        /// <returns></returns>
        public long m_lngGetZYYBjs(string strJslb, string strInvNo, string strZDZMHM, decimal decTotal, string strRegisterId, out clsDGZyjs_VO objDgzyjsVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetZYYBjs(strJslb, strInvNo, strZDZMHM, decTotal, strRegisterId, out objDgzyjsVo);
            //objSvc.Dispose();
            return l;
        }

        /// <summary>
        /// ��ȡסԺҽ��������������
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="objDgzyjsVo"></param>
        /// <returns></returns>
        public long m_lngGetZYYBjs(string strJslb, string strInvNo, string strZDZMHM, decimal decTotal, string strRegisterId, out clsDGZyjs_VO objDgzyjsVo, bool p_blnDiffOn)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetZYYBjs(strJslb, strInvNo, strZDZMHM, decTotal, strRegisterId, out objDgzyjsVo, p_blnDiffOn);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ��ȡסԺҽ����Ժ�Ǽ���������
        /// <summary>
        /// ��ȡסԺҽ����Ժ�Ǽ���������
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="objDgzycydjVo"></param>
        /// <returns></returns>
        public long m_lngGetZYYBCydj(string strRegisterId, string strJZJLH, out clsDGZycydj_VO objDgzycydjVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc =
            //                                                          (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));

            long l = proxy.Service.m_lngGetZYYBCydj(strRegisterId, strJZJLH, out objDgzycydjVo);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ����סԺҽ�����㷵����Ϣ
        /// <summary>
        /// ����סԺҽ�����㷵����Ϣ
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="objDgzyjsfhVo"></param>
        /// <returns></returns>
        public long m_lngSaveYBChargeReturnZY(string strRegID, string strInvNo, clsDGZyjsfh_VO objDgzyjsfhVo, string strJSDYZDBY)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngSaveYBChargeReturnZY(strRegID, strInvNo, objDgzyjsfhVo, strJSDYZDBY);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����סԺ�ǼǱ�״̬
        /// <summary>
        /// ����סԺ�ǼǱ�״̬
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strStatusFlag"></param>
        /// <returns></returns>
        public long m_lngUpdateYBRegisterStatusZY(string strRegID, string strStatusFlag)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngUpdateYBRegisterStatusZY(strRegID, strStatusFlag);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����סԺ�����״̬
        /// <summary>
        /// ����סԺ�����״̬
        /// </summary>
        /// <param name="strRegID"></param>
        /// <param name="strStatusFlag"></param>
        /// <returns></returns>
        public long m_lngUpdateYBChargeStatusZY(string strSdywh, string strRegID, string strStatusFlag)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngUpdateYBChargeStatusZY(strSdywh, strRegID, strStatusFlag);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҽԺ������ע���¼
        /// <summary>
        /// ����ҽԺ������ע���¼
        /// </summary>
        /// <param name="m_clsDGYBjbrzcVo">����ע��VO</param>
        /// <returns></returns>
        public long m_lngSaveUserRegister(clsDGYBjbrzc_VO m_clsDGYBjbrzcVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc =
            //                                     (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngSaveUserRegister(m_clsDGYBjbrzcVo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ݷ�Ʊ��ȡ����ҽ��������Ϣ
        /// <summary>
        /// ���ݷ�Ʊ��ȡ����ҽ��������Ϣ
        /// </summary>
        /// <param name="strInvoiceNo"></param>
        /// <param name="dtPatientInfo"></param>
        /// <param name="dtFee"></param>
        /// <returns></returns>
        public long m_lngGetPatientInfoByInvoice(string strInvoiceNo, out DataTable dtPatientInfo, out DataTable dtFee)
        {
            long lngRes = -1;
            dtPatientInfo = null;
            dtFee = null;
            //com.digitalwave.iCare.middletier.HIS.clsYBTSQuerySVC objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsYBTSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBTSQuerySVC));
            //long lngRes =  objSvc.m_lngGetPatientInfoByInvoice(strInvoiceNo, out dtPatientInfo, out dtFee);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҽԺ������
        /// <summary>
        /// ����ҽԺ������
        /// </summary>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngGetUserRegister(out clsDGYBjbrzc_VO[] p_objResult)
        {
            p_objResult = new clsDGYBjbrzc_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));
            long lngRes = proxy.Service.m_lngGetUserRegister(out p_objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ���˵Ļ�����Ϣ
        /// <summary>
        /// ��ȡ���˵Ļ�����Ϣ
        /// </summary>
        /// <param name="p_InPatientID"></param>
        /// <returns></returns>
        public long m_lngGetPatientInfo(string p_InPatientID, string strFlag, out DataTable dtResult)
        {
            dtResult = new DataTable();
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));
            long lngRes = proxy.Service.m_lngGetPatientInfo(p_InPatientID, strFlag, out dtResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�ҽԺ��¼����
        /// <summary>
        /// �޸�ҽԺ��¼����
        /// </summary>
        /// <param name="p_strPwd">������</param>
        /// <param name="p_strYYBH">ҽԺ����</param>
        /// <returns></returns>
        public long m_lngHospitalUserLogin(string p_strPwd, string p_strYYBH)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngHospitalUserLogin(p_strPwd, p_strYYBH);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ��������˿�������Ϣ
        /// <summary>
        /// ��ȡ��������˿�������Ϣ
        /// </summary>
        /// <param name="p_strSywh"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public long m_lngCSYBChargeCancel(string p_strSywh, int flag, out clsDGExtra_VO objDgextraVo, out bool p_blRefund)
        {
            objDgextraVo = null;
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));
            long lngRes = proxy.Service.m_lngCSYBChargeCancel(p_strSywh, flag, out objDgextraVo, out p_blRefund);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ������������˿�������Ϣ
        /// <summary>
        /// ������������˿�������Ϣ
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public long m_lngUpdateCSYBChargeCancel(clsDGExtra_VO objDgextraVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngUpdateCSYBChargeCancel(objDgextraVo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ҽ�����㵥��ӡ��֤
        /// <summary>
        /// ҽ�����㵥��ӡ��֤
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public long m_lngGetYBChargeZY(string p_strRegisterId, out clsDGExtra_VO objDgextraVo)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));
            long lngRes = proxy.Service.m_lngGetYBChargeZY(p_strRegisterId, out objDgextraVo);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �ж��Ƿ����籣���˵Ǽ�
        /// <summary>
        /// �ж��Ƿ����籣���˵Ǽ�
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <returns></returns>
        public bool m_blGetIsYBReg(string p_strRegisterId)
        {
            bool blRes = false;
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));
            blRes = proxy.Service.m_lngGetPatientInfo(p_strRegisterId);
            //objSvc.Dispose();
            return blRes;
        }
        #endregion

        #region ���������Ϣ�޸�
        /// <summary>
        /// ���������Ϣ�޸�
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public long m_lngUpdateCSYBChargeInfo(clsDGExtra_VO objDgextraVo, string strMZorZY)
        {
            //clsYBCSRequiredSVC objSvc = (clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSRequiredSVC));
            long lngRes = proxy.Service.m_lngUpdateCSYBChargeInfo(objDgextraVo, strMZorZY);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ���ﲡ����Ϣ
        /// <summary>
        /// ��ѯ���ﲡ����Ϣ
        /// </summary>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        public long m_lngGetPatientInfo(string p_strID, out DataTable dtResult)
        {
            //clsYBCSQuerySVC objSvc = (clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSQuerySVC));
            long lngRes = proxy.Service.m_lngGetPatientInfo(p_strID, out dtResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����Ƿ�����סԺ���
        /// <summary>
        /// ����Ƿ�����סԺ���
        /// </summary>
        /// <param name="p_strCheckOutType"></param>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngCheckDiagnose2(string p_strCheckOutType, string p_strRegisterID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            #region �м������
            //clsYBCSQuerySVC objServ = null;
            try
            {
                //objServ = (clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSQuerySVC));
                lngRes = proxy.Service.m_lngCheckDiagnose2(p_strCheckOutType, p_strRegisterID, out p_dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
                //if (objServ != null)
                //{
                //    objServ.Dispose();
                //    objServ = null;
                //}
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ͨ��סԺ������ȡ�籣���˵ľ����¼��
        /// <summary>
        /// ͨ��סԺ������ȡ�籣���˵ľ����¼��
        /// </summary>
        /// <param name="strInpatientID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetJZJLHbyInpatientID(string strInpatientID, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = new DataTable();
            #region �м������
            //clsYBCSQuerySVC objServ = null;
            try
            {
                //objServ = (clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSQuerySVC));
                lngRes = proxy.Service.m_lngGetJZJLHbyInpatientID(strInpatientID, out dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
                //if (objServ != null)
                //{
                //    objServ.Dispose();
                //    objServ = null;
                //}
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ͨ�������¼������ȡ�籣���˵Ľ����
        /// <summary>
        /// ͨ�������¼������ȡ�籣���˵Ľ����
        /// </summary>
        /// <param name="strInpatientID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetJSHbyJZJLH(string strInpatientID, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = new DataTable();
            #region �м������
            //clsYBCSQuerySVC objServ = null;
            try
            {
                //objServ = (clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSQuerySVC));
                lngRes = proxy.Service.m_lngGetJSHbyJZJLH(strInpatientID, out dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
                //if (objServ != null)
                //{
                //    objServ.Dispose();
                //    objServ = null;
                //}
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ��ȡ�籣��ҽԺ���һ�����ݵ�����
        /// <summary>
        /// ��ȡ�籣��ҽԺ���һ�����ݵ�����
        /// </summary>
        /// <param name="intType"></param>
        /// <returns></returns>
        public long m_lngGetHosYBData(int intType, out DataTable dtHosp, out DataTable dtYBHospCorr)
        {
            long lngRes = 0;
            dtHosp = new DataTable();
            dtYBHospCorr = new DataTable();
            #region �м������
            //clsYBCSQuerySVC objServ = null;
            try
            {
                //objServ = (clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSQuerySVC));
                lngRes = proxy.Service.m_lngGetHosYBData(intType, out dtHosp, out dtYBHospCorr);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
                //if (objServ != null)
                //{
                //    objServ.Dispose();
                //    objServ = null;
                //}
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="intType">1:���ң�2:�������</param>
        /// <param name="strHospID"></param>
        /// <param name="strName"></param>
        /// <param name="strYBID"></param>
        /// <param name="strYBName"></param>
        /// <returns></returns>
        public long m_lngSaveData(int intType, string strHospID, string strName, string strYBID, string strYBName)
        {
            long lngRes = 0;
            #region �м������
            //clsYBCSRequiredSVC objServ = null;
            try
            {
                //objServ = (clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSRequiredSVC));
                lngRes = proxy.Service.m_lngSaveData(intType, strHospID, strName, strYBID, strYBName);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
                //if (objServ != null)
                //{
                //    objServ.Dispose();
                //    objServ = null;
                //}
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="strHospID"></param>
        /// <param name="strYBID"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public long m_lngDelData(string strHospID, string strYBID, int intType)
        {
            long lngRes = 0;
            #region �м������
            //clsYBCSRequiredSVC objServ = null;
            try
            {
                //objServ = (clsYBCSRequiredSVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSRequiredSVC));
                lngRes = proxy.Service.m_lngDelData(strHospID, strYBID, intType);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
                //if (objServ != null)
                //{
                //    objServ.Dispose();
                //    objServ = null;
                //}
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ���ݾ����¼������ȡ�籣����֤��Ϣ
        /// <summary>
        /// ���ݾ����¼������ȡ�籣����֤��Ϣ
        /// </summary>
        /// <param name="strJzjlh"></param>
        /// <param name="objDGExtraVO"></param>
        /// <returns></returns>
        public long m_lngGetYBPswCheckInfo(string strJzjlh, ref clsDGExtra_VO objDGExtraVO, out DateTime dtmFyrq)
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));
            long l = proxy.Service.m_lngGetYBPswCheckInfo(strJzjlh, ref objDGExtraVO, out dtmFyrq);
            //objSvc.Dispose();
            return l;
        }
        #endregion

        #region ICD10���
        /// <summary>
        /// ICD10���
        /// </summary>
        /// <returns></returns>
        public DataTable GetIcd10()
        {
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC svc = (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));
            DataTable dt = proxy.Service.GetIcd10();
            //svc.Dispose();
            return dt;
        }
        #endregion

        #region ���ý������(��ֹ)����
        /// <summary>
        /// ���ý������(��ֹ)����С����
        /// </summary>
        /// <returns></returns>
        public string GetFeeMaxDate(string regId)
        {
            string maxDate = string.Empty;
            string minDate = string.Empty;
            //com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC svc = (com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBCSQuerySVC));
            proxy.Service.m_lngGetZYFYSJ(regId, out maxDate, out minDate);
            //svc.Dispose();
            if (string.IsNullOrEmpty(maxDate))
                maxDate = DateTime.Now.ToString("yyyyMMdd");
            else
                maxDate = Convert.ToDateTime(maxDate).ToString("yyyyMMdd");
            return maxDate;
        }
        #endregion

        #region �����������
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdministrative()
        {
            //using (clsYBCSQuerySVC svc = (clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSQuerySVC)))
            //{
            return proxy.Service.GetAdministrative();
            //}
        }
        #endregion

        #region �Ƿ����ö�ͯ�۸�
        /// <summary>
        /// �Ƿ����ö�ͯ�۸�
        /// </summary>
        /// <returns></returns>
        public bool IsUseChildPrice()
        {
            //using (clsYBCSQuerySVC svc = (clsYBCSQuerySVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsYBCSQuerySVC)))
            //{
            return proxy.Service.IsUseChildPrice();
            //}
        }
        #endregion

    }
}
