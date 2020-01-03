using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;

namespace iCare
{
    public class clsEMR_OPInstrumentDomain
    {
        #region �����Ŀ���ֵ��
        /// <summary>
        /// �����Ŀ���ֵ��
        /// </summary>
        /// <param name="p_strItemName">��Ŀ����</param>
        /// <returns></returns>
        public long m_lngAddNewToDict(string p_strItemName)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngAddNewToDict(p_strItemName);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region �޸��ֵ����Ŀ
        /// <summary>
        /// �޸��ֵ����Ŀ
        /// </summary>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <param name="p_strOPInstrumentName">��Ŀ����</param>
        /// <returns></returns>
        public long m_lngModifyToDisc(int p_intOPInstrumentID, string p_strOPInstrumentName)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngModifyToDisc(p_intOPInstrumentID, p_strOPInstrumentName);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region ͣ���ֵ����Ŀ
        /// <summary>
        /// ͣ���ֵ����Ŀ
        /// </summary>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <returns></returns>
        public long m_lngDeActiveItemFromDict(int p_intOPInstrumentID)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngDeActiveItemFromDict(p_intOPInstrumentID);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region �����ֵ����Ŀ
        /// <summary>
        /// �����ֵ����Ŀ
        /// </summary>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <returns></returns>
        public long m_lngActiveItemFromDict(int p_intOPInstrumentID)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngActiveItemFromDict(p_intOPInstrumentID);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region ������������Ŀ˳��
        /// <summary>
        /// ������������Ŀ˳��
        /// </summary>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <param name="p_intOrderID">˳���</param>
        /// <returns></returns>
        public long m_lngUpdateOrderID(int p_intOPInstrumentID, int p_intOrderID)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngUpdateOrderID(p_intOPInstrumentID, p_intOrderID);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ�����ֵ����Ŀ
        /// <summary>
        /// ��ȡ�����ֵ����Ŀ
        /// </summary>
        /// <param name="p_obDictItems">������Ŀ</param>
        /// <returns></returns>
        public long m_lngGetAllItemsFromDict(out clsEMR_OPInstrument_Dict[] p_obDictItems)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetAllItemsFromDict(out p_obDictItems);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ�������ֵ����Ŀ
        /// <summary>
        /// ��ȡ�������ֵ����Ŀ
        /// </summary>
        /// <param name="p_obDictItems">��������Ŀ</param>
        /// <returns></returns>
        public long m_lngGetActiveItemsFromDict(out clsEMR_OPInstrument_Dict[] p_obDictItems)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetActiveItemsFromDict(out p_obDictItems);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region ����ֵ���Ƿ����и���Ŀ
        /// <summary>
        /// ����ֵ���Ƿ����и���Ŀ
        /// </summary>
        /// <param name="p_strOPInstrumentName">��Ŀ����</param>
        /// <param name="p_intOPInstrumentID">��ĿID</param>
        /// <returns></returns>
        public long m_lngCheckSameItemID(string p_strOPInstrumentName, out int p_intOPInstrumentID)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngCheckSameItemID(p_strOPInstrumentName, out p_intOPInstrumentID);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region ��ȡָ����¼������
        /// <summary>
        /// ��ȡָ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objRecordContent">���صļ�¼����</param>
        /// <returns></returns>
        public long m_lngGetRecordContent(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            out clsTrackRecordContent p_objRecordContent)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyEmr07()).Service.m_lngGetRecordContent_factory( enmDiseaseTrackType.OPInstrumentQty, p_strInPatientID, p_strInPatientDate, p_strOpenDate, out p_objRecordContent);
            //objService = null;
            return lngRes;
        }
        #endregion

        #region �������ݿ��е��״δ�ӡʱ��
        /// <summary>
        /// �������ݿ��е��״δ�ӡʱ�䡣
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
        /// <returns></returns>
        public long m_lngUpdateFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            //clsEMR_OPInstrumentService objService =
            //    (clsEMR_OPInstrumentService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_OPInstrumentService));

            long lngRes = 0;
            lngRes = (new weCare.Proxy.ProxyEmr05()).Service.clsEMR_OPInstrumentService_m_lngUpdateFirstPrintDate(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_dtmFirstPrintDate);
            //objService = null;
            return lngRes;
        }
        #endregion
    }
}
