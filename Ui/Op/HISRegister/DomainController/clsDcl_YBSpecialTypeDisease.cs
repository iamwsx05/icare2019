using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using System.Collections;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ҽ�����ֲ�ά������Ʋ�
    /// </summary>
    public class clsDcl_YBSpecialTypeDisease : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_YBSpecialTypeDisease()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region �������ֲ�ID��ȡ���ֲ���Ϣ
        /// <summary>
        /// �������ֲ�ID��ȡ���ֲ���Ϣ
        /// </summary>
        /// <param name="m_strDieaseCode">��������</param>
        /// <param name="m_objYBSpeTypeDiease_VO"></param>
        /// <returns></returns>
        public long m_mthGetYBSpecialTypeDiseaseByID(string m_strDieaseCode, out clsYBSpecialTypeDisease_VO m_objYBSpeTypeDiease_VO)
        {
            long lngres = 0;
            m_objYBSpeTypeDiease_VO = null;
            try
            {

                //com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC));

                lngres = (new weCare.Proxy.ProxyOP02()).Service.m_mthGetYBSpecialTypeDiseaseByID(m_strDieaseCode, out m_objYBSpeTypeDiease_VO);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return 0;
            }
            return lngres;

        }
        #endregion
        #region ��ȡ���ֲ���Ϣ
        /// <summary>
        /// ��ȡ���ֲ���Ϣ
        /// </summary>
        /// <param name="m_strDieaseCode">��������</param>
        /// <param name="m_objYBSpeTypeDiease_VO"></param>
        /// <returns></returns>
        public long m_mthGetTableForYBSpecialTypeDisease(out DataTable m_objTable)
        {
            long lngres = 0;
            m_objTable = null;
            try
            {

                //com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC));

                lngres = (new weCare.Proxy.ProxyOP02()).Service.m_mthGetTableForYBSpecialTypeDisease(out m_objTable);
                //objSvc.Dispose();
                //objSvc = null;

            }
            catch
            {
                return 0;
            }
            return lngres;

        }
        #endregion
        #region ɾ�����ֲ���Ϣ
        /// <summary>
        /// ͨ����������ɾ�����ֲ���Ϣ
        /// </summary>
        /// <param name="m_strDiseCode"></param>
        /// <returns></returns>
        public long m_mthDelectYBSpecialTypeDiseaseByDiseaseCode(string m_strDiseCode)
        {
            long lngres = 0;
            try
            {

                //com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC));

                lngres = (new weCare.Proxy.ProxyOP02()).Service.m_mthDelectYBSpecialTypeDiseaseByDiseaseCode(m_strDiseCode);
                //objSvc.Dispose();
                //objSvc = null;

            }
            catch (Exception e)
            {

                return 0;
            }
            return lngres;

        }
        #endregion
        #region ���ӻ����ҽ�����ֲ���Ϣ
        /// <summary>
        /// ���ӻ����ҽ�����ֲ���Ϣ
        /// </summary>
        /// <param name="m_objYBSpeTypeDise_VO"></param>
        /// <returns></returns>
        public long m_mthModifyYBSpecialTypeDiseaseInfo(clsYBSpecialTypeDisease_VO m_objYBSpeTypeDise_VO)
        {
            long lngres = 0;
            try
            {

                //com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC));

                lngres = (new weCare.Proxy.ProxyOP02()).Service.m_mthModifyYBSpecialTypeDiseaseInfo(m_objYBSpeTypeDise_VO);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return 0;
            }
            return lngres;

        }
        #endregion

        #region  ���ݲ�ѯ�����Ͳ������ݻ�ȡ���ֲ���Ϣ
        /// <summary>
        /// ���ݲ�ѯ�����Ͳ������ݻ�ȡ���ֲ���Ϣ,����ҽ�����ֱ�
        /// </summary>
        /// <param name="m_objTable"></param>
        ///  <param name="m_intCondition"></param>
        ///  <param name="m_strContent"></param>
        /// <returns></returns>
        public long m_mthGetTableForYBSpeTypeDiseByCondition(int m_intCondition, string m_strContent, out DataTable m_objTable)
        {
            long lngres = 0;
            m_objTable = null;
            try
            {

                //com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC objSvc = (com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsYBSpecialTypeDisease_SVC));

                lngres = (new weCare.Proxy.ProxyOP02()).Service.m_mthGetTableForYBSpeTypeDiseByCondition(m_intCondition, m_strContent, out m_objTable);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                return 0;
            }
            return lngres;

        }
        #endregion
    }
}
