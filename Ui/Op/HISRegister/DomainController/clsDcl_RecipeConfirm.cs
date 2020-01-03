using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsDcl_RecipeConfirm : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region GetProxy 
        /// <summary>
        /// GetProxy
        /// </summary>
        weCare.Proxy.ProxyOP proxy
        {
            get
            {
                return new weCare.Proxy.ProxyOP();
            }
        }
        #endregion 

        #region ���ݿ��Ų�ѯ������Ϣ
        public long m_lngGetPatientInfo(string strCardno, out DataTable dteResult)
        {
            dteResult = new DataTable(); ;
            //com.digitalwave.iCare.middletier.HIS.clsHisBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
            long lngRes = (new weCare.Proxy.ProxyHisBase()).Service.m_mthGetPatientInfoByCardID(strCardno, out dteResult, false);
            return lngRes;
        }
        #endregion

        #region ���ش���
        /// <summary>
        /// ���ش���
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="objRI_VO"></param>
        /// <returns></returns>
        public long m_mthLoadRecipeNo(string strID, out clsRecipeInfo_VO[] objRI_VO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc));
            long l = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetPatientInfoByCardID(strID, out objRI_VO);
            return l;
        }
        ///
        #endregion

        #region ��ѯ���顢��顢��������Ϣ
        /// <summary>
        /// ��ѯ���顢��顢��������Ϣ
        /// </summary>
        /// <param name="p_RecipeNo"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_mthFindRecipeDetailOrder(string p_RecipeNo, out DataTable dtResult)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc objSvc =
            //   (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc));
            long l = proxy.Service.m_mthFindRecipeDetailOrder(p_RecipeNo, out dtResult);
            return l;
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
                //clsRecipeConfirmQuerySvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc));
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

        #region
        public long m_mthFindRecipeDetail6(string ID, out DataTable dt, bool flag)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_mthFindRecipeDetail6(ID, out dt, flag);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������뵥��״̬
        /// <summary>
        /// �������뵥��״̬
        /// </summary>
        /// <param name="p_reicpeNO"></param>
        /// <param name="p_listApp"></param>
        /// <returns></returns>
        public long m_lngModiffyAppStatus(clsOutpatientRecipe_VO[] objRecipeVO)
        {
            //com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmOpSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmOpSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmOpSvc));
            long lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_lngModiffyAppStatus(objRecipeVO);
            //objSvc.Dispose();
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

            bool ret = proxy.Service.m_blnChkopsitem(chrgitemcode);
            //objSvc.Dispose();

            return ret;
        }
        #endregion

        #region ��ѯ������ϸ
        public long m_mthRecipeDetail(string p_strRecipeNO, out DataTable dtResult)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc objSvc =
            //  (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc));
            long l = (new weCare.Proxy.ProxyOP02()).Service.m_mthRecipeDetail(p_strRecipeNO, out dtResult);
            return l;
        }
        #endregion

        #region ��ѯ��Ŀ��ϸ
        /// <summary>
        /// ��ѯ��Ŀ��ϸ
        /// </summary>
        /// <param name="p_strRecipeNO"></param>
        /// <param name="p_strPatientID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetItemDetails(string p_strRecipeNO, string p_strPatientID, string p_strItemID, string p_strType, out DataTable dtResult)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc objSvc =
            //  (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc));
            long l = (new weCare.Proxy.ProxyOP02()).Service.m_lngGetItemDetails(p_strRecipeNO, p_strPatientID, p_strItemID, p_strType, out dtResult);
            return l;
        }
        #endregion

        #region ����ȡ��ȷ��
        /// <summary>
        /// ����ȡ��ȷ��
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public long m_lngItemsCancel(string p_strItemid, string p_strEmpid)
        {
            long lngRes = -1;
            //com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmOpSvc objSvc =
            //  (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmOpSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmOpSvc));
            long l = (new weCare.Proxy.ProxyOP02()).Service.m_lngItemsCancel(p_strItemid, p_strEmpid);
            return l;
        }
        #endregion

        #region Ȩ���ж�
        /// <summary>
        /// Ȩ���ж�
        /// </summary>
        /// <param name="p_strEmpid"></param>
        /// <returns></returns>
        public bool m_blnCompetence(string p_strEmpid)
        {
            bool lngRes = false;
            //com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc objSvc =
            //  (com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsRecipeConfirmQuerySvc));
            lngRes = (new weCare.Proxy.ProxyOP02()).Service.m_blnCompetence(p_strEmpid);
            return lngRes;
        }
        #endregion
    }
}
