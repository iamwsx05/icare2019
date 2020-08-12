using System;
using System.Data;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.dll
using com.digitalwave.GUI_Base;//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainControlStorageAidInfo ��ժҪ˵����
    /// Create kong by 2004-06-06
    /// </summary>
    public class clsDomainControlStorageAidInfo : com.digitalwave.GUI_Base.clsDomainController_Base //GUI_Base.dll
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDomainControlStorageAidInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ��������

        #region ������������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="p_objItem">��������VO</param>
        /// <returns></returns>
        public long m_lngDoAddNewStorageOrdType(clsStorageOrdType_VO p_objItem)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewStorageOrdType(p_objItem);

            return lngRes;
        }
        #endregion

        #region �޸ĵ�������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// �޸ĵ�������
        /// </summary>
        /// <param name="p_objItem">��������VO</param>
        /// <returns></returns>
        public long m_lngDoUpdateStorageOrdType(clsStorageOrdType_VO p_objItem)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdStorageOrdTypeByID(p_objItem);

            return lngRes;
        }
        #endregion

        #region ɾ����������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="p_strStorageOrdTypeID">��������ID</param>
        /// <returns></returns>
        public long m_lngDoDeleteStorageOrdType(string p_strStorageOrdTypeID)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteStorageOrdTypeByID(p_strStorageOrdTypeID);

            return lngRes;
        }
        #endregion

        #region ��DataTable��ֵ���ݸ�VO  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ��DataTable��ֵ���ݸ�VO
        /// </summary>
        /// <param name="dtbSource"></param>
        /// <param name="p_objResultArr"></param>
        private void m_mthCopyTableToVO(DataTable dtbSource, out clsStorageOrdType_VO[] p_objResultArr)
        {
            int intRow = dtbSource.Rows.Count;
            p_objResultArr = new clsStorageOrdType_VO[intRow];
            for (int i1 = 0; i1 < intRow; i1++)
            {
                p_objResultArr[i1] = new clsStorageOrdType_VO();
                p_objResultArr[i1].m_strStorageOrdTypeID = dtbSource.Rows[i1]["STORAGEORDTYPEID_CHR"].ToString().Trim();
                p_objResultArr[i1].m_strStorageOrdTypeName = dtbSource.Rows[i1]["STORAGEORDTYPENAME_VCHR"].ToString().Trim();

                string strSign = dtbSource.Rows[i1]["SIGN_INT"].ToString().Trim();
                if (strSign == "")
                {
                    strSign = "1";
                }
                p_objResultArr[i1].m_intSign = int.Parse(strSign);

                string strDeptType = dtbSource.Rows[i1]["DEPTTYPE_INT"].ToString().Trim();
                if (strDeptType == "")
                {
                    strDeptType = "0";
                }
                p_objResultArr[i1].m_intDeptType = int.Parse(strDeptType);

                if (dtbSource.Rows[i1]["BEGINSTR_CHR"] != null)
                {
                    p_objResultArr[i1].m_strBEGINSTR_CHR = dtbSource.Rows[i1]["BEGINSTR_CHR"].ToString().Trim();
                }
                if (dtbSource.Rows[i1]["MEDSTORAGE_INT"] != null)
                {
                    p_objResultArr[i1].m_intMEDSTORAGE = int.Parse(dtbSource.Rows[i1]["MEDSTORAGE_INT"].ToString().Trim());
                }

            }
        }
        #endregion

        #region ģ�����ҵ�������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ģ�����ҵ�������
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindStorageOrdTypeByAny(string p_strSQL, out clsStorageOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOrdType_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdTypeByAny(p_strSQL, out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsStorageOrdType_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region ���ݵ�������ID���ҵ�������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ���ݵ�������ID����
        /// </summary>
        /// <param name="p_strStorageOrdTypeID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindStorageOrdTypeByID(string p_strStorageOrdTypeID, out clsStorageOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOrdType_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdTypeByID(p_strStorageOrdTypeID, out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsStorageOrdType_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region ���ݵ������ͳ����־����  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ���ݵ������ͳ����־����
        /// </summary>
        /// <param name="p_intSign"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindStorageOrdTypeBySign(int p_intSign, out clsStorageOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOrdType_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdTypeBySign(p_intSign, out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsStorageOrdType_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region ���ݵ������ͳ����־����  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ���ݵ������ͳ����־����
        /// </summary>
        /// <param name="p_strForwardID">ǰ�</param>
        /// <param name="p_intSign"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindStorageOrdTypeBySign(string p_strForwardID, int p_intSign, out clsStorageOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOrdType_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdTypeBySign(p_strForwardID, p_intSign, out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsStorageOrdType_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region ���ݵ������͵�Ժ�����־����  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ���ݵ������͵�Ժ�����־����
        /// </summary>
        /// <param name="p_intDeptType"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindStorageOrdTypeByDeptType(int p_intDeptType, out clsStorageOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOrdType_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindStorageOrdTypeByDeptType(p_intDeptType, out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsStorageOrdType_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region �������еĵ�������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// �������еĵ�������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindAllStorageOrdType(out clsStorageOrdType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOrdType_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindAllStorageOrdType(out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsStorageOrdType_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region ������ĵ�������ID
        /// <summary>
        /// ������ĵ�������ID
        /// </summary>
        /// <returns></returns>
        public string m_strGetMaxStorageOrdTypeID()
        {
            long lngRes = 0;
            string strResult = "";

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxStorageOrdTypeID(out strResult);

            int intID = 1;

            if (strResult == "")
            {
                intID = 1;
                strResult = intID.ToString("0000");
            }
            else
            {
                intID = int.Parse(strResult);
                if (intID > 0)
                {
                }
                else
                {
                    intID = 1;
                    strResult = intID.ToString("0000");
                }
            }

            return strResult;
        }
        #endregion

        #endregion

        #region ������

        #region ����������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        public long m_lngDoAddNewPeriod(clsPeriod_VO p_objItem)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewPeriod(p_objItem);

            return lngRes;
        }
        #endregion

        #region �޸�������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// �޸�������
        /// </summary>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        public long m_lngDoUpdatePeriod(clsPeriod_VO p_objItem)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdPeriodByID(p_objItem);

            return lngRes;
        }
        #endregion

        #region ɾ��������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ɾ��������
        /// </summary>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        public long m_lngDoDeletePeriod(string p_strPeriodID)
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeletePeriodByID(p_strPeriodID);

            return lngRes;
        }
        #endregion

        #region ɾ������������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ɾ������������
        /// </summary>
        /// <returns></returns>
        public long m_lngDoDeletePeriod()
        {
            long lngRes = 0;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeleteAllPeriod();

            return lngRes;
        }
        #endregion

        #region ��DataTable��ֵ���ݸ�VO  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ��DataTable��ֵ���ݵ�VO
        /// </summary>
        /// <param name="dtbSource"></param>
        /// <param name="p_objResultArr"></param>
        private void m_mthCopyTableToVO(DataTable dtbSource, out clsPeriod_VO[] p_objResultArr)
        {
            int intRow = dtbSource.Rows.Count;
            p_objResultArr = new clsPeriod_VO[intRow];
            for (int i1 = 0; i1 < intRow; i1++)
            {
                p_objResultArr[i1] = new clsPeriod_VO();
                p_objResultArr[i1].m_strPeriodID = dtbSource.Rows[i1]["PERIODID_CHR"].ToString().Trim();
                p_objResultArr[i1].m_strStartDate = dtbSource.Rows[i1]["STARTDATE"].ToString().Trim();
                p_objResultArr[i1].m_strEndDate = dtbSource.Rows[i1]["ENDDATE"].ToString().Trim();
            }
        }
        #endregion

        #region ģ������������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ģ������������
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindPeriodByAny(string p_strSQL, out clsPeriod_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsPeriod_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriodByAny(p_strSQL, out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsPeriod_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region ��������ID����  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ��������ID����
        /// </summary>
        /// <param name="p_strPeriodID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindPeriodByID(string p_strPeriodID, out clsPeriod_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsPeriod_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriodByID(p_strPeriodID, out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsPeriod_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ���е�������� created by weiling.huang at 2005-9-29
        /// <summary>
        /// ��ȡ���е�������� created by weiling.huang at 2005-9-29
        /// </summary>
        /// <param name="p_strPeriodID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngMaxValuePeriod(out string p_strResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngMaxValuePeriod(out p_strResult);
            return lngRes;
        }
        #endregion

        #region �������е�������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// �������е�������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindAllPeriod(out clsPeriod_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsPeriod_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindAllPeriod(out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsPeriod_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }

            return lngRes;
        }
        #endregion

        #region �������е�������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// �������е�������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindAllPeriod(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;

            p_dtbResult = null;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindAllPeriod(out p_dtbResult);

            return lngRes;
        }
        #endregion

        #region ���ҵ�ǰ������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ���ҵ�ǰ������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetCurrentPeriod(out clsPeriod_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsPeriod_VO[0];

            DataTable dtbResult = new DataTable();

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindCurrentPeriod(out dtbResult);

            if (lngRes > 0 && dtbResult != null)
            {
                int intRow = dtbResult.Rows.Count;
                if (intRow > 0)
                {
                    p_objResultArr = new clsPeriod_VO[intRow];
                    m_mthCopyTableToVO(dtbResult, out p_objResultArr);
                }
            }
            return lngRes;
        }
        #endregion

        #region ���ҵ�ǰ������  ŷ����ΰ  2004-06-06
        /// <summary>
        /// ���ҵ�ǰ������
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetCurrentPeriod(out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;

            p_dtbResult = null;

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindCurrentPeriod(out p_dtbResult);
            return lngRes;
        }
        #endregion

        #region ������������ID  ŷ����ΰ  2004-06-06
        /// <summary>
        /// �������������ID
        /// </summary>
        /// <returns></returns>
        public string m_strGetMaxPeriodID()
        {
            long lngRes = 0;
            string strResult = "";

            System.Security.Principal.IPrincipal objPrincipal = null;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetMaxPeriodID(out strResult);

            int intID = 1;

            if (strResult == "")
            {
                intID = 1;
                strResult = intID.ToString("0000");
            }
            else
            {
                intID = int.Parse(strResult);
                if (intID > 0)
                {
                }
                else
                {
                    intID = 1;
                    strResult = intID.ToString("0000");
                }
            }

            return strResult;
        }
        #endregion

        #region ��ѯ�����ת�������
        /// <summary>
        /// ��ѯ�����ת�������
        /// </summary>
        /// <param name="p_intRow">����</param>
        /// <returns></returns>
        public long m_lngGetPeriodRow(out int p_intRow)
        {
            long lngRes = 0;
            p_intRow = 0;

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetPeriodRow(out p_intRow);

            return lngRes;
        }
        #endregion

        #endregion

        #region �����ת

        #region ���������ת
        /// <summary>
        /// ���������ת
        /// </summary>
        /// <param name="p_objItem">�����ת����</param>
        /// <returns></returns>
        public long m_lngDoAddNewPeriodOperator(clsPeriodOperator_VO p_objItem)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoAddNewPeriodOperator(p_objItem);

            return lngRes;
        }
        #endregion

        #region �޸������ת
        /// <summary>
        /// �޸������ת
        /// </summary>
        /// <param name="p_objItem">�����ת����</param>
        /// <returns></returns>
        public long m_lngDoUpdatePeriodOperator(clsPeriodOperator_VO p_objItem)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoUpdatePeriodOperator(p_objItem);

            return lngRes;
        }
        #endregion

        #region ɾ�������ת
        /// <summary>
        /// ɾ�������ת
        /// </summary>
        /// <param name="p_strStorageID">�ⷿ����</param>
        /// <param name="p_strPeriodID">�����ڴ���</param>
        /// <returns></returns>
        public long m_lngDoDeletePeriodOperator(string p_strStorageID, string p_strPeriodID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngDoDeletePeriodOperator(p_strStorageID, p_strPeriodID);

            return lngRes;
        }
        #endregion

        #region ģ����ѯ�����ת
        /// <summary>
        /// ģ����ѯ�����ת
        /// </summary>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindPeriodOperatorByAny(string p_strSQL, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriodOperatorByAny(p_strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ���ⷿ��ѯ�����ת
        /// <summary>
        /// ���ⷿ��ѯ�����ת
        /// </summary>
        /// <param name="p_strID">�ⷿ����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindPeriodOperatorByStorage(string p_strID, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriodOperatorByStorage(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �������ڲ�ѯ�����ת
        /// <summary>
        /// �������ڲ�ѯ�����ת
        /// </summary>
        /// <param name="p_strID">�����ڴ���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindPeriodOperatorByPeriod(string p_strID, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriodOperatorByPeriod(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ���ⷿ�������ڲ�ѯ�����ת
        /// <summary>
        /// ���ⷿ�������ڲ�ѯ�����ת
        /// </summary>
        /// <param name="p_strStorageID">�ⷿ����</param>
        /// <param name="p_strPeriodID">�����ڴ���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindPeriodOperatorByStorageAndPeriod(string p_strStorageID, string p_strPeriodID,
            out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriodOperatorByStorageAndPeriod(p_strStorageID, p_strPeriodID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ����ת�����˲�ѯ�����ת
        /// <summary>
        /// ����ת�����˲�ѯ�����ת
        /// </summary>
        /// <param name="p_strID">����Ա����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindPeriodOperatorByOper(string p_strID, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriodOperatorByOper(p_strID, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ����תʱ��β�ѯ�����ת
        /// <summary>
        /// ����תʱ��β�ѯ�����ת
        /// </summary>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindPeriodOperatorByDate(string p_strStartDate, string p_strEndDate, out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindPeriodOperatorByDate(p_strStartDate, p_strEndDate, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ��ѯ���е������ת
        /// <summary>
        /// ��ѯ���е������ת
        /// </summary>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        public long m_lngFindAllPeriodOperator(out clsPeriodOperator_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsPeriodOperator_VO[0];

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngFindAllPeriodOperator(out p_objResult);

            return lngRes;
        }
        #endregion

        #region ��ѯ��ת�˼�ʱ��
        /// <summary>
        /// ��ѯ��ת�˼�ʱ��
        /// </summary>
        /// <param name="p_strStorageID">�ⷿ����</param>
        /// <param name="p_strPeriodID">�����ڴ���</param>
        /// <param name="p_strEmp">����Ա</param>
        /// <param name="p_strOperDate">����ʱ��</param>
        /// <returns></returns>
        public long m_lngPeriodOperatorReal(string p_strStorageID, string p_strPeriodID,
            out string p_strEmp, out string p_strOperDate)
        {
            long lngRes = 0;
            p_strEmp = "";
            p_strOperDate = "";

            //com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageAidInfoSvc));

            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngPeriodOperatorReal(p_strStorageID, p_strPeriodID, out p_strEmp, out p_strOperDate);

            return lngRes;

        }
        #endregion

        #endregion

    }
}
