using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HIS;
using com.digitalwave.Utility;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDcl_OPSelectChargeItem : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ���ݴ�����ѯ�Ƿ��Ѿ���ҩ���
        /// <summary>
        /// ���ݴ�����ѯ�Ƿ��Ѿ���ҩ���
        /// </summary>
        /// <param name="p_strRecNo">������</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngQuerySendMed(string p_strRecNo, out DataTable p_dtbResult)
        {
            long lngRes = -1;
            p_dtbResult = null;
            try
            {
                //clsOPSelectChargeItemSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOPSelectChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPSelectChargeItemSvc));
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngQuerySendMed(p_strRecNo, out p_dtbResult);

                //objSvc.Dispose();
                //objSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError("m_lngQuerySendMed�����м������" + objEx.Message);
                objLogger = null;
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ������Ŀ�Ƿ��Ѿ�����
        /// <summary>
        /// ��ѯ������Ŀ�Ƿ��Ѿ�����
        /// </summary>
        /// <param name="p_strRecNo">������</param>
        /// <param name="p_strOrderDicId">������Ŀid</param>
        /// <param name="p_intType">1-����� 2-����</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngQueryDiagnosisItemStatus(string p_strRecNo, string p_strOrderDicId, int p_intType, out DataTable p_dtbResult)
        {
            long lngRes = -1;
            p_dtbResult = null;
            try
            {
                //clsOPSelectChargeItemSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOPSelectChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOPSelectChargeItemSvc));
                lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngQueryDiagnosisItemStatus(p_strRecNo, p_strOrderDicId, p_intType, out p_dtbResult);

                //objSvc.Dispose();
                //objSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError("m_lngQueryDiagnosisItemStatus�����м������" + objEx.Message);
                objLogger = null;
            }
            return lngRes;
        }
        #endregion

        #region �����շ�ID�ж��Ƿ�������������
        /// <summary>
        /// �����շ�ID�ж��Ƿ�������������
        /// </summary>
        /// <param name="chrgitemcode"></param>
        /// <returns></returns>
        public bool m_blnChkopsitem(string chrgitemcode)
        {
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //                                         (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));

            bool ret = (new weCare.Proxy.ProxyOP()).Service.m_blnChkopsitem(chrgitemcode);
            //objSvc.Dispose();

            return ret;
        }
        #endregion
    }
}
