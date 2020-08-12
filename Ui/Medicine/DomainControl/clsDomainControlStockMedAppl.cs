using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �ɹ��ƻ���
    /// kong 2004-05-27
    /// </summary>
    public class clsDomainControlStockMedAppl : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainControlStockMedAppl()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����ɹ���¼�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ����ɹ���¼��
        /// </summary>
        /// <param name="p_objStockMedAppl"></param>
        /// <returns></returns>
        public long m_lngDoSaveStockMedAppl(clsStockMedApplication_VO p_objStockMedAppl)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStockAppl(p_objStockMedAppl);

            return lngRes;
        }
        #endregion

        #region ����ɹ���ϸ ŷ����ΰ 2004-05-31
        /// <summary>
        /// ����ɹ���ϸ
        /// </summary>
        /// <param name="p_objStockMedApplDetail"></param>
        /// <returns></returns>
        public long m_lngDoSaveStockMedApplDetail(clsStockMedApplDetail_VO p_objStockMedApplDetail)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStockApplDe(p_objStockMedApplDetail);

            return lngRes;
        }
        #endregion

        #region ���ļ�¼�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ���ļ�¼��
        /// </summary>
        /// <param name="p_objStockMedAppl"></param>
        /// <returns></returns>
        public long m_lngDoUpdateStockMedAppl(clsStockMedApplication_VO p_objStockMedAppl)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateStockApplByID(p_objStockMedAppl);

            return lngRes;
        }
        #endregion

        #region ������ϸ�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ������ϸ��
        /// </summary>
        /// <param name="p_objStockMedApplDetail"></param>
        /// <returns></returns>
        public long m_lngDoUpdateStockMedApplDetail(clsStockMedApplDetail_VO p_objStockMedApplDetail)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdateStockApplDeByID(p_objStockMedApplDetail);

            return lngRes;
        }
        #endregion

        #region ɾ����¼�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ɾ����¼��
        /// </summary>
        /// <param name="p_strId">��¼��ID</param>
        /// <returns></returns>
        public long m_lngDoDeleteStockMedApplID(string p_strId)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStockApplByID(p_strId);

            return lngRes;
        }
        #endregion

        #region ɾ����ϸ�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ɾ����ϸ��
        /// </summary>
        /// <param name="p_strID">��ϸ��ID</param>
        /// <returns></returns>
        public long m_lngDoDeleteStockMedApplDetailByID(string p_strID)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStockApplDeByID(p_strID);

            return lngRes;
        }
        #endregion

        #region ģ����ѯ��¼�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ģ����ѯ��¼��
        /// </summary>
        /// <param name="p_strSQL">SQL�ű����</param>
        /// <param name="p_objResult">���ֵ</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByAny(string p_strSQL, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ��ID���Ҽ�¼�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ��ID���Ҽ�¼����Ϣ
        /// </summary>
        /// <param name="p_strId">ID��</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByID(string p_strId, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByApplID(p_strId, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ���ݲɹ����Ų�ѯ�ɹ���¼��
        /// <summary>
        /// �Բɹ����Ų�ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_strNo">�ɹ�����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByNo(string p_strNo, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByApplNo(p_strNo, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �Կⷿ��ѯ�ɹ���¼��
        /// <summary>
        /// �Կⷿ��ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_strId">�ⷿ����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByStorage(string p_strId, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByStorage(p_strId, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �Թ�Ӧ�̲�ѯ�ɹ���¼��
        /// <summary>
        /// �Թ�Ӧ�̲�ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_strId">��Ӧ�̴���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByVendor(string p_strId, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByVendor(p_strId, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �Բ��Ų�ѯ�ɹ���¼��
        /// <summary>
        /// �Բ��Ų�ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_strId">���Ŵ���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByDept(string p_strId, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByDept(p_strId, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �Բɹ�ʱ��β�ѯ�ɹ���¼��
        /// <summary>
        /// �Բɹ�ʱ��β�ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplByDate(string p_strStartDate, string p_strEndDate, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplByDate(p_strStartDate, p_strEndDate, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ģ��������ϸ�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ģ��������ϸ��
        /// </summary>
        /// <param name="p_strSQL">SQL�ű����</param>
        /// <param name="p_objResult">���ֵ</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplDeByAny(string p_strSQL, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplDeByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �Բɹ���ID������ϸ�� ŷ����ΰ 2004-05-31
        /// <summary>
        /// �Բɹ���ID������ϸ��
        /// </summary>
        /// <param name="p_strID">�ɹ���¼��ID</param>
        /// <param name="p_dtbResult">�������</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplDeByID(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplDeByID(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ��ѯ�ɹ���ϸ��
        /// <summary>
        /// ��ҩƷ��ѯ�ɹ���ϸ��
        /// </summary>
        /// <param name="p_strID">ҩƷ����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindStockMedApplDeByMedicine(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStockMedApplDeByMedicine(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �������¼���� ŷ����ΰ 2004-05-31
        /// <summary>
        /// �������¼����
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetMaxStockMedApplNo(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxStockMedApplNo(out p_strResult);

            long lngNo = long.Parse(p_strResult);

            if (lngNo < 1)
            {
                lngNo = 1;
                p_strResult = lngNo.ToString("0000000000");
            }

            return lngRes;
        }
        #endregion

        #region �������¼��ID ŷ����ΰ 2004-05-31
        /// <summary>
        /// �������¼��ID
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetMaxStockMedApplID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxStockMedApplId(out p_strResult);

            long lngID = long.Parse(p_strResult);

            if (lngID < 1)
            {
                lngID = 1;
                p_strResult = lngID.ToString("0000000000");
            }

            return lngRes;
        }
        #endregion

        #region ��������ϸ���� ŷ����ΰ 2004-05-31
        /// <summary>
        /// ��������ϸ����
        /// </summary>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        public long m_lngGetMaxStockMedApplDeId(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxStockMedApplDeId(out p_strResult);

            long lngDeID = long.Parse(p_strResult);

            if (lngDeID < 1)
            {
                lngDeID = 1;
                p_strResult = lngDeID.ToString("0000000000");
            }

            return lngRes;
        }
        #endregion

        #region �Զ����ɲɹ�����ϸ ŷ����ΰ 2004-05-31
        /// <summary>
        /// �Զ����ɲɹ�����ϸ
        /// </summary>
        /// <param name="p_strID">�ⷿID��</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngDoAutoCalcStockMedApplDe(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAutoCalcStockMedAppl(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ��ϵͳ�ķ���

        #region ��ȡ���뵥��Ϣ
        public long m_lngGetApplCation(out DataTable dtbResult, string date, string p_strStorageID)
        {
            long lngRes = 0;
            dtbResult = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetApplCation(out dtbResult, date, p_strStorageID);
            return lngRes;

        }
        #endregion

        #region ��ȡ���벿��
        public long m_lngGetDept(out clsT_BSE_DEPTDESC_VO[] DEPTDESC_VO)
        {
            long lngRes = 0;
            DEPTDESC_VO = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetDept(out DEPTDESC_VO);
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ӧ��
        public long m_lngGetVendor(out clsVendor_VO[] VendorVO)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetVendor(out VendorVO);

            return lngRes;
        }
        #endregion

        #region ��ȡ����
        public long m_lngGetManufacturer(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetManufacturer(out dtbResult);

            return lngRes;
        }
        #endregion

        #region ��ȡ���ĵ��ݺ�
        public long m_lngGetMaxDoc(out string p_strMaxDoc, string strdate)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxDoc(out p_strMaxDoc, strdate);
            return lngRes;
        }
        #endregion

        #region �ɹ����
        /// <summary>
        /// �ɹ����
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objResultDeArr"></param>
        /// <param name="blnAuto"></param>
        /// <returns></returns>
        public long m_lngAutoCompleteApp(clsMedStorageOrd_VO p_objResultArr, clsMedStorageOrdDe_VO[] p_objResultDeArr, bool blnAuto)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngAutoCompleteApp(p_objResultArr, p_objResultDeArr, blnAuto);
            return lngRes;
        }
        #endregion

        #region ���ݵ���ID��õ�����ϸ
        public long m_lngGetApplDeByID(string strID, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetApplDeByID(strID, out dtbResult);
            return lngRes;
        }
        #endregion



        #region  ������ϸ
        public long m_lngInsertDe(DataTable newRow)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngInsertDe(newRow);

            return lngRes;
        }
        #endregion

        #region  �޸ĵ���
        public long m_lngModify(DataTable newRow, DataTable newRowDe)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngModify(newRow, newRowDe);

            return lngRes;
        }
        #endregion

        #region  ���浥��
        public long m_lngSaveData(DataTable newRow, DataTable newTableDe, out string p_strNewID)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngSaveData(newRow, newTableDe, out p_strNewID);

            return lngRes;
        }
        #endregion

        #region �޸ĵ���
        /// <summary>
        /// �޸ĵ���
        /// </summary>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public long m_lngModifyData(DataTable newRow)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngModifyData(newRow);


            return lngRes;
        }
        #endregion

        #region ���ݵ���ID��õ�����ϸ
        public long m_lngGetVen(string strMedID, out string venName)
        {
            long lngRes = 0;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetVen(strMedID, out venName);
            return lngRes;
        }
        #endregion

        #region ɾ����¼��
        /// <summary>
        /// ɾ����¼��
        /// </summary>
        /// <param name="p_strId">��¼��ID</param>
        /// <returns></returns>
        public long m_lngDoDelStockApplByID(string p_strId)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));


            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDelStockApplByID(p_strId);


            return lngRes;
        }
        #endregion

        #region ɾ����ϸ��
        /// <summary>
        /// ɾ����ϸ��
        /// </summary>
        /// <param name="p_strID">��ϸ��ID</param>
        /// <returns></returns>
        public long m_lngDoDelStockMedApplDetailByID(string p_strID)
        {
            long lngRes = 0;

            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStockApplDeByID(p_strID);


            return lngRes;
        }
        #endregion

        #region ��ȡ���ϲɹ�����������
        public long m_lngGetData(out DataTable objDataTable)
        {
            long lngRes = 0;
            objDataTable = null;
            //clsStockMedApplSvc objStockMed = (clsStockMedApplSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsStockMedApplSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetData(out objDataTable);

            return lngRes;
        }
        #endregion
        #endregion
    }
}
