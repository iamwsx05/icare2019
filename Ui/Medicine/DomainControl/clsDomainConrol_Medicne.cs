using System;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HIS; //HIS_SVC.DLL
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDomainConrol_Medicne:���ݿ����� Create by Sam 2004-5-24
    /// </summary>
    public class clsDomainConrol_Medicne : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDomainConrol_Medicne()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ��������ҩ��ȱҩ��־
        /// <summary>
        /// ��������ҩ��ȱҩ��־
        /// </summary>
        /// <param name="p_strMedID">ҩƷID</param>
        /// <param name="p_intFlag">����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ</param>
        /// <returns></returns>
        public long m_lngSetCenterStorageFlag(string p_strMedID, int p_intFlag)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSetCenterStorageFlag(p_strMedID, p_intFlag);

            return lngRes;
        }
        #endregion

        #region ��ѯ���е�ҩƷ��ҩƷȱҩ����ģ�飩
        /// <summary>
        /// ��ѯ���е�ҩƷ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_intFlag">ָʾҩ����־ 0-ҩ�� 1-����ҩ��</param>
        /// <returns></returns>
        public long m_lngGetMetList(string[] MedTypeList, int p_intFlag, out System.Data.DataTable dt)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMetList(MedTypeList, p_intFlag, out dt);
            return lngRes;
        }
        #endregion

        #region ��ȡ���е�ҩƷ����
        /// <summary>
        /// ��ȡ���е�ҩƷ����
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedType(out DataTable dtbResult)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedType(out dtbResult);
            return lngRes;
        }
        #endregion


        #region ��ȡִ��ҽ����������
        /// <summary>
        /// ��ȡִ��ҽ����������
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetAllBihCate(out DataTable dtbResult)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllBihCate(out dtbResult);
            return lngRes;
        }
        #endregion

        #region ��ʾҩƷ���
        /// <summary>
        /// ��ȡ���е�ҩƷ����
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetMedStorage(string strMedID, out DataTable dt)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedStorage(strMedID, out dt);
            return lngRes;
        }
        #endregion

        #region ���ҩƷ�������
        /// <summary>
        /// ���ҩƷ�������
        /// </summary>
        /// <param name="ArrMedTypeName">ҩƷ��������</param>
        /// <returns></returns>
        public long m_lngGetMedTypeArr(out string[] ArrMedTypeName, string[] MedTypeList)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedTypeArr(out ArrMedTypeName, MedTypeList);
            return lngRes;
        }
        #endregion

        #region ����ҩƷ�Ƿ��Ѿ�ͬ�շ���Ŀͬ��
        /// <summary>
        /// ����ҩƷ�Ƿ��Ѿ�ͬ�շ���Ŀͬ��
        /// </summary>
        /// <param name="strMedid"></param>
        /// <param name="stritemID">������ڻ����շ���ĿID�������ڷ���NULL</param>
        /// <returns></returns>
        public long m_lngGetItemID(string strMedid, out string stritemID)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetItemID(strMedid, out stritemID);
            return lngRes;
        }
        #endregion


        #region ɾ��ҩƷ��Ӧ��ҩ��
        /// <summary>
        /// ɾ��ҩƷ��Ӧ��ҩ��
        /// </summary>
        /// <param name="strMedid"></param>
        /// <returns></returns>
        public long m_lngDeleteMedByID(string strMedid)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedByID(strMedid);
            return lngRes;
        }
        #endregion

        #region ��ѯ���е�ҩƷ(ҩƷ����ģ��)
        /// <summary>
        /// ��ѯ���е�ҩƷ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMetDgList(string[] medType, out System.Data.DataTable dt, bool p_blStop)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMetDgList(medType, out dt, p_blStop);
            return lngRes;
        }
        #endregion

        #region ����ҩƷȱҩ��־
        /// <summary>
        /// ͨ��ҩƷID����ҩƷ��ǰ�Ƿ��л�
        /// </summary>
        /// <param name="MedID">ҩƷID</param>
        /// <param name="p_ing">ȱҩ��־</param>
        /// <returns></returns>
        public long m_lngSetStorage(string MedID, int p_ing)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSetStorage(MedID, p_ing);

            return lngRes;
        }

        #endregion

        #region ��ѯ���е�ҩƷ
        /// <summary>
        /// ��ѯ���е�ҩƷ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedicine(out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedList(out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ѯ���е�ҩƷ
        /// <summary>
        /// ��ѯ���е�ҩƷ
        /// </summary>
        /// <returns></returns>
        public long m_lngGetMed(string strageID, out DataTable p_objResultArr)
        {
            p_objResultArr = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMed(strageID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ȡҽ������
        /// <summary>
        /// ��ȡҽ������
        /// </summary>
        /// <param name="dtMEDICARETYPE"></param>
        /// <returns></returns>
        public long m_lngGetMEDICARETYPE(out DataTable dtMEDICARETYPE)
        {
            dtMEDICARETYPE = null;
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMEDICARETYPE(out dtMEDICARETYPE);
            return lngRes;
        }
        #endregion

        #region ���ҩƷ�������Ƿ�������ҩƷ��ʹ��
        /// <summary>
        /// ���ҩƷ�������Ƿ�������ҩƷ��ʹ��
        /// </summary>
        /// <returns></returns>
        public long m_lngCheckIsUse(string helpCode, out DataTable dt)
        {
            dt = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngCheckIsUse(helpCode, out dt);
            return lngRes;
        }
        #endregion

        #region ����ҩƷ��Ϣ
        /// <summary>
        /// ����ҩƷ��Ϣ
        /// </summary>
        /// <returns></returns>
        public long m_lngSaveed(string p_strType, DataTable SaveRow, out string newID, int isInsertItem, string strEmpID, bool IsAuto, string strStorageID, DataTable p_dtbChargeItem)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSaveed(p_strType, SaveRow, out newID, isInsertItem, strEmpID, IsAuto, strStorageID, p_dtbChargeItem);
            return lngRes;
        }
        #endregion

        #region �޸Ķ�ӦID
        /// <summary>
        /// �޸Ķ�ӦID
        /// </summary>
        /// <param name="medID"></param>
        /// <param name="id"></param>
        /// <param name="IDname"></param>
        /// <returns></returns>
        public long m_lngModifyMEDICINESTDID(string medID, string id, string IDname)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngModifyMEDICINESTDID(medID, id, IDname);
            return lngRes;
        }
        #endregion

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        public long m_lngGetVendorName(string vendorID, out string vendorName)
        {
            long lngRes = 0;
            vendorName = null;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetVendorName(vendorID, out vendorName);

            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ�����ID
        public long getMedMaxID(out string strID)
        {
            strID = "1";
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedMaxID(out strID);

            return lngRes;
        }
        #endregion

        #region ����ID��ѯҩƷ
        /// <summary>
        /// ����ID��ѯҩƷ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedicineByID(string strMedID, out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicineByID(strMedID, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��������������ѯҩƷ
        /// <summary>
        /// ��������������ѯҩƷ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedicineByAll(string strSubSQL, out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedicineByAny(strSubSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ����ҩƷ��Ϣ
        /// <summary>
        /// ����ҩƷ��Ϣ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngNewMed(clsMedicine_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicine(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �޸�ҩƷ��Ϣ
        /// <summary>
        ///�޸�ҩƷ��Ϣ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpDoMed(clsMedicine_VO p_objResultArr, string OldID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicineByID(p_objResultArr, OldID);

            return lngRes;
        }
        #endregion

        #region ɾ��ҩƷ��Ϣ
        public long m_lngDeleteMedicineByID(string strID, bool isDeleItem, string strEmpID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedicineByID(strID, isDeleItem, strEmpID);
            return lngRes;
        }
        #endregion

        #region ��ȡ��λ��Ϣ
        public long m_lngGetUnitArr(out clsUnit_Vo[] p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetUnitArr(out p_objResultArr);

            return lngRes;
        }
        #endregion

        //ҩƷ���͡����͡���λ Create by Sam 2004-5-24

        #region ȡ�����е�ҩƷ����
        public long m_lngGetMedType(out clsMedicineType_VO[] objResultArr)
        {
            objResultArr = new clsMedicineType_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMedicineType(out objResultArr);

            return lngRes;
        }
        #endregion

        #region ȡ�����еļ���
        public long m_lngGetPrepType(out clsMedicinePrepType_VO[] objResultArr)
        {
            objResultArr = new clsMedicinePrepType_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMeicinePrep(out objResultArr);

            return lngRes;
        }
        #endregion

        #region ȡ�����еĵ�λ
        public long m_lngGetUnit(out clsUnit_VO[] objResultArr)
        {
            objResultArr = new clsUnit_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllUnit(out objResultArr);

            return lngRes;
        }
        #endregion

        #region ������λ
        /// <summary>
        /// ������λ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngNewUnit(clsUnit_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewUnit(p_objResultArr);
            return lngRes;
        }
        #endregion

        #region ����ҩƷ����
        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngNewMedType(clsMedicineType_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicineType(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ����ҩƷ����
        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngNewPrepType(clsMedicinePrepType_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewPrepType(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �޸ĵ�λ
        /// <summary>
        ///�޸ĵ�λ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpDoUnit(clsUnit_VO p_objResultArr, string OldID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdUnit(p_objResultArr, OldID);

            return lngRes;
        }
        #endregion

        #region �޸�ҩƷ����
        /// <summary>
        ///�޸�ҩƷ����
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpDoMedType(clsMedicineType_VO p_objResultArr, string OldID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicineTypeByID(p_objResultArr, OldID);

            return lngRes;
        }
        #endregion

        #region �޸ļ���
        /// <summary>
        ///�޸ļ���
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpDoPrepType(clsMedicinePrepType_VO p_objResultArr, string OldID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdPrepType(p_objResultArr, OldID);

            return lngRes;
        }
        #endregion

        #region ɾ����λ
        /// <summary>
        ///ɾ����λ
        /// </summary>
        public long m_lngDelUnit(string strUnitID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteUnit(strUnitID);

            return lngRes;
        }
        #endregion

        #region ɾ��ҩƷ����
        /// <summary>
        ///ɾ��ҩƷ����
        /// </summary>
        public long m_lngDelMedType(clsMedicineType_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedicineTypeByID(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ɾ������
        /// <summary>
        ///ɾ������
        /// </summary>
        public long m_lngDelPrepType(string strID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeletePrepType(strID);

            return lngRes;
        }
        #endregion

        #region ����ID��ѯ��Ŀ����(ҩƷ���͡����͡���λ)
        /// <summary>
        ///  ����ID��ѯ��Ŀ����
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetItemByID(string strID, byte sType, out string strName)
        {
            strName = "";
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetItemByID(sType, strID, out strName);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ����ID(ҩƷ���͡����͡���λ)
        public long getItemMaxID(byte sType, out string strID)
        {
            strID = "1";
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngMaxID(sType, out strID);

            return lngRes;
        }
        #endregion

        //ҩƷ�뵥λ�Ĺ�ϵ Create by Sam 2004-5-24

        #region ��ѯ���е�ҩƷ�뵥λ�Ĺ�ϵ
        public long m_lngGetMedAndUnit(out clsMedUnitAndUnit[] objResult)
        {
            objResult = new clsMedUnitAndUnit[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMedAndUnit(out objResult);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ID��ѯҩƷ�뵥λ�Ĺ�ϵ
        /// <summary>
        /// ����ID��ѯҩƷ�뵥λ�Ĺ�ϵ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedAndUnitByID(string strMedID, string strUnitID, out clsMedUnitAndUnit[] p_objResultArr)
        {
            p_objResultArr = new clsMedUnitAndUnit[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedAndUnitByID(strMedID, strUnitID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩƷID��ѯҩƷ�뵥λ�Ĺ�ϵ
        /// <summary>
        /// ����ҩƷID��ѯҩƷ�뵥λ�Ĺ�ϵ
        /// </summary>
        /// <param name="strMedID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedAndUnitByMedID(string strMedID, out clsMedUnitAndUnit[] p_objResultArr)
        {
            p_objResultArr = new clsMedUnitAndUnit[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindMedAndUnitByMedID(strMedID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩƷ�뵥λ�Ĺ�ϵ
        public long m_lngNewMedUnit(clsMedUnitAndUnit p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedUnitAndUnit(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �޸�ҩƷ�뵥λ�Ĺ�ϵ
        public long m_lngUpMedUnit(clsMedUnitAndUnit p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpMedUnitAndUnit(p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ɾ��ҩƷ�뵥λ�Ĺ�ϵ
        public long m_lngDelMedAndUnit(string strMedID, string strUnitID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedUnitAndUnit(strMedID, strUnitID);

            return lngRes;
        }
        #endregion

        #region �õ�ҩƷ�뵥λ��ϵ����󼶱�
        public long getLevelMaxID(string MedID, out string strID)
        {
            strID = "1";
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            (new weCare.Proxy.ProxyMedStore()).Service.GetMaxLeve(MedID, out strID);

            return lngRes;
        }
        #endregion

        //ҩƷ�۸��б� Create by Sam
        #region ��ѯ���е�ҩƷ�۸�
        /// <summary>
        /// ��ѯ���е�ҩƷ�۸�
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedPrice(out clsMedicinePrice_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicinePrice_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllCurPrice(out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ����ҩƷID��ѯҩƷ�۸�
        /// <summary>
        /// ����ҩƷID��ѯҩƷ�۸�
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedPriceByID(string strMedID, out clsMedicinePrice_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicinePrice_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindCurPriceByMedicineID(strMedID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ѯҩƷ��ʷ�۸�
        /// <summary>
        /// ��ѯҩƷ��ʷ�۸�
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedPriceHistory(string strMedID, out clsMedicinePrice_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicinePrice_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindPriceHistoryByMedicineID(strMedID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩƷ�۸�
        /// <summary>
        /// ����ҩƷ�۸�
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngAddMedPrice(clsMedicinePrice_VO p_objResultArr, out string ModifyDate)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewPrice(p_objResultArr, out ModifyDate);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩƷ�۸�
        /// <summary>
        /// ����ҩƷ�۸�
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUPMedPrice(clsMedicinePrice_VO p_objResultArr, out string ModifyDate)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdPriceByMedicineID(p_objResultArr, out ModifyDate);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��ҩƷ�۸�
        /// <summary>
        /// ɾ��ҩƷ�۸�
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDelMedPrice(clsMedicinePrice_VO p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicinePriceSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeletePriceByMedicineID(p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        //ҩƷ��ҩ��
        #region ����ҩƷ��ҩ��
        /// <summary>
        /// ����ҩƷ��ҩ��
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngAddMedAndSto(clsMedicineAndStorage p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoAddNewMedicineAndStorage(p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �޸�ҩƷ��ҩ���ϵ
        /// <summary>
        /// �޸�ҩƷ��ҩ���ϵ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngUpMedAndSto(clsMedicineAndStorage p_objResultArr, string OldMedID, string OldStoID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDoUpdMedicineAndStorage(p_objResultArr, OldMedID, OldStoID);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ɾ��ҩƷ��ҩ���ϵ
        /// <summary>
        /// ɾ��ҩƷ��ҩ���ϵ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngDelMedAndSto(clsMedicineAndStorage p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleteMedicineAndStorage(p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩ��ID��ѯ����ҩƷ
        /// <summary>
        /// ����ҩ��ID��ѯ����ҩƷ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindAllMedByStoID(string StoID, out clsMedicineAndStorage[] p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllMedicineByStorageID(StoID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ����ҩ��ID��ҩƷID��ѯҩƷ
        /// <summary>
        /// ����ҩ��ID��ҩƷID��ѯҩƷ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindMedByStoIDAndMedID(string StoID, string MedID, out clsMedicineAndStorage[] p_objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindItemByStoIDAndMedID(StoID, MedID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ȡ�òֿ����Ϣ
        public long m_lngGetStorage(out clsStorage_VO[] objStorage)
        {
            objStorage = new clsStorage_VO[0];
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetStorage(out objStorage);

            return lngRes;
        }
        #endregion

        #region ��ѯ���е�ҩƷ
        /// <summary>
        /// ��ѯ���е�ҩƷ
        /// </summary>
        /// <param name="p_strCategoryID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetMedNoIn(string StoID, out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedAndStorageSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedList(StoID, out p_objResultArr);

            //			objCheckItemSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������ҩƷ����
        public long m_lngGetMedicine(out DataTable dtbResult)
        {
            dtbResult = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedicine(out dtbResult);

            return lngRes;
        }

        #endregion

        #region  ҩ����ҩ����   2004-9-29

        #region ��ȡ���е����뵥��
        public long m_lngGetMedAppl(out clsStoreMedAppl_VO[] p_objResultArr, string STORAGEID, string strDate)
        {
            p_objResultArr = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedAppl(out p_objResultArr, STORAGEID, strDate);

            return lngRes;
        }

        #endregion

        #region ��ȡҩ��
        public long m_lngGetMedstroage(string medStroageID, out string medStroageName)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedstroage(medStroageID, out medStroageName);

            return lngRes;
        }

        #endregion

        #region �������뵥�Ż�ȡ���е�������ϸ
        /// <summary>
        /// �������뵥�Ż�ȡ���е�������ϸ
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dtbResult"></param>
        /// <param name="flat">false ��־�����뵥��û�����ð�װ����ҩƷ</param>
        /// <returns></returns>
        public long m_lngGetMedApplDeByID(string strID, out DataTable dtbResult, out bool flat)
        {
            dtbResult = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedApplDeByID(strID, out dtbResult, out flat);

            return lngRes;
        }

        #endregion

        #region ����ҩƷID��ѯ�����ϸ��
        public long m_lngGetDeTailByMedID(string MedID, out DataTable dtbResult)
        {
            dtbResult = null;

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetDeTailByMedID(MedID, out dtbResult);

            return lngRes;
        }
        #endregion

        #region ���������ɳ��ⵥ
        public long m_lngChangAndSave(string strMedApplId, clsMedStorageOrd_VO objResult, clsMedStorageOrdDe_VO[] objResultArr)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngChangAndSave(strMedApplId, objResult, objResultArr);
            return lngRes;
        }
        #endregion

        #endregion

        #region ҩƷ������Ϣ����
        /// <summary>
        /// ҩƷ������Ϣ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtUnit">���ص�λ��Ϣ</param>
        /// <param name="dtMeicinePrep">����ҩƷ�Ƽ�����</param>
        /// <param name="dtuse">�����÷�</param>
        /// <param name="dtvendor">���س���</param>
        ///<param name="dtmedtype">����ҩƷ������Ϣ</param>
        ///<param name="dtItemextype">����������Ŀ�������</param>
        ///<param name="dtItemextype1">����������Ŀ��Ʊ����</param>
        ///<param name="dtItemextype3">����סԺ��Ŀ��Ʊ����</param>
        ///<param name="dtItemextype4">����סԺ��Ŀ��Ʊ����</param>
        ///<param name="dtMEDICARETYPE">����ҽ����������</param>
        ///<param name="dtPharMatype">ҩ�����</param>
        ///<param name="Isuse">�Ƿ����ֱ����ҩƷ���������޸�ҩƷ�۸�</param>
        /// <returns></returns>
        public long m_lngFindAllBase(out DataTable dtUnit, out DataTable dtMeicinePrep, out DataTable dtuse, out DataTable dtFreq, out DataTable dtvendor, out DataTable dtmedtype, out DataTable dtItemextype, out DataTable dtItemextype1, out DataTable dtItemextype3, out DataTable dtItemextype4, out DataTable dtMEDICARETYPE, out DataTable dtItemextype5, out DataTable dtPharMatype, out bool Isuse, out DataTable dtCATEID1)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFindAllBase(out dtUnit, out dtMeicinePrep, out dtuse, out dtFreq, out dtvendor, out dtmedtype, out dtItemextype, out dtItemextype1, out dtItemextype3, out dtItemextype4, out dtMEDICARETYPE, out dtItemextype5, out dtPharMatype, out Isuse, out dtCATEID1);
            return lngRes;
        }

        #endregion

        #region �޸�ҩƷ��Ϣ
        /// <summary>
        /// �޸�ҩƷ��Ϣ
        /// </summary>
        /// <param name="ModifyRow"></param>
        /// <returns></returns>
        public long m_lngModify(DataTable ModifyRow, int isInsertItem, string strEmpID)
        {

            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngModify(ModifyRow, isInsertItem, strEmpID);
            return lngRes;
        }

        #endregion

        #region �ֿ�ҩƷά��

        #region ��ȡ���е�ҩƷ��Ϣ
        /// <summary>
        /// ��ȡ���е�ҩƷ��Ϣ
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="btStorage"></param>
        /// <returns></returns>
        public long m_lngGetAllMed(out DataTable bt, out DataTable btStorage)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllMed(out bt, out btStorage);
            return lngRes;
        }

        #endregion


        #region ����ҩ��IDȡ��ҩƷ��Ϣ
        /// <summary>
        /// ����ҩ��IDȡ��ҩƷ��Ϣ
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="btStorage"></param>
        /// <returns></returns>
        public long m_lngGetMedByStorageID(string strID, out DataTable tb)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetMedByStorageID(strID, out tb);
            return lngRes;
        }

        #endregion

        #region ��ҩƷ��ӵ��ֿ�(ȫ��ҩƷ��
        /// <summary>
        /// ��ҩƷ��ӵ��ֿ�(ȫ��ҩƷ��
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="storageIDb"></param>
        /// <returns></returns>
        public long m_lngAddMedToStorage(DataTable tb, DataTable tb1, string storageIDb)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddMedToStorage(tb, tb1, storageIDb);
            return lngRes;
        }

        #endregion

        #region ɾ��ָ���ֿ��ҩƷ
        /// <summary>
        /// ɾ��ָ���ֿ��ҩƷ
        /// </summary>
        /// <param name="storageID"></param>
        /// <param name="medID"></param>
        /// <returns></returns>
        public long m_lngDeleMedToStorage(string storageID, string medID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleMedToStorage(storageID, medID);
            return lngRes;
        }

        #endregion

        #region ��ҩƷ��ӵ��ֿ�(ĳһ����¼��
        /// <summary>
        /// ��ҩƷ��ӵ��ֿ�(ĳһ����¼��
        /// </summary>
        /// <param name="storageID"></param>
        /// <param name="strMedID"></param>
        /// <returns></returns>
        public long m_lngAddNoeMedToStorage(string storageID, string strMedID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngAddNoeMedToStorage(storageID, strMedID);
            return lngRes;
        }

        #endregion


        #endregion

        #region ҩ������ά��
        #region ��ȡ�ֿ����
        /// <summary>
        /// ��ȡ�ֿ����
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public long m_lngGetAllMedType(out DataTable tb)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllMedType(out tb);
            return lngRes;
        }
        #endregion
        #region ��ֿ���Ϣ
        /// <summary>
        /// ��ֿ���Ϣ
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public long m_lngGetAllstorage(out DataTable tb)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetAllstorage(out tb);
            return lngRes;
        }

        #endregion

        #region ����ֿ���Ϣ
        /// <summary>
        /// ����ֿ���Ϣ
        /// </summary>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public long m_lngInsertStorageData(string strStorageTypeID, string strStorageName, out string newID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngInsertStorageData(strStorageTypeID, strStorageName, out newID);
            return lngRes;
        }

        #endregion

        #region �޸Ĳֿ���Ϣ
        /// <summary>
        /// �޸Ĳֿ���Ϣ
        /// </summary>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public long m_lngModifyStorageData(string p_strStorageTypeID, string p_strStorageName, string p_strStorageID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngModifyStorageData(p_strStorageTypeID, p_strStorageName, p_strStorageID);
            return lngRes;
        }

        #endregion

        #region ɾ���ֿ���Ϣ
        /// <summary>
        /// ɾ���ֿ���Ϣ
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public long m_lngDeleStorageData(string strID)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngDeleStorageData(strID);
            return lngRes;
        }

        #endregion


        #endregion

        #region ҩ���¹�������ͳ��ģ��
        /// <summary>
        /// ҩ���¹�������ͳ��ģ��
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtord"></param>
        /// <returns></returns>
        public long m_lngGetReportDataOfMonth(out DataTable dtdein, out DataTable dtENAim, out DataTable dtENNoAim, out DataTable dtCHAim, out DataTable dtCHNoAim, out DataTable dtEHAim, out DataTable dtEHNoAim, out DataTable dtImportAim, out DataTable dtImportNoAim, System.Collections.Generic.List<string> ArrList, int statues)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfMonth(out dtdein, out dtENAim, out dtENNoAim, out dtCHAim, out dtCHNoAim, out dtEHAim, out dtEHNoAim, out dtImportAim, out dtImportNoAim, ArrList, statues);
            return lngRes;
        }

        #endregion

        #region ҩƷ�����汨��
        /// <summary>
        /// ҩƷ�����汨��
        /// </summary>
        /// <param name="arrPrID">Ҫͳ�ƵĲ������б�</param>
        /// <param name="strUpPr">��һ�ڵĲ�����</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetReportDataOfInAndOut(System.Collections.Generic.List<string> arrPrID, string strUpPr, out DataTable dt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfInAndOut(arrPrID, strUpPr, out dt);
            return lngRes;
        }

        #endregion

        #region ҩ���¹�������ͳ��ģ��
        /// <summary>
        /// ҩ���¹�������ͳ��ģ��
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtord"></param>
        /// <returns></returns>
        public long m_lngGetReportDataOfMonth1(out DataTable dtdein, out DataTable dtEN, out DataTable dtCH, out DataTable dtEH, out DataTable dtImport, string startDate, string EndDate, int statues)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfMonth(out dtdein, out dtEN, out dtCH, out dtEH, out dtImport, startDate, EndDate, statues);
            return lngRes;
        }

        #endregion

        #region �̿���ϸ��ͳ�Ʊ���
        /// <summary>
        /// �̿���ϸ��ͳ�Ʊ���
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="statues"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngCheckLoseDe(string startDate, string EndDate, int statues, out DataTable dt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsStorageCheckSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngCheckLoseDe(startDate, EndDate, statues, out dt);
            return lngRes;
        }

        #endregion

        #region ҩƷ����ͳ�Ʊ���
        /// <summary>
        /// ҩƷ����ͳ�Ʊ���
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtord"></param>
        /// <returns></returns>
        public long m_lngGetReportData(string date1, string date2, out DataTable dtDeIn, out DataTable dtDeOut)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportData(date1, date2, out dtDeIn, out dtDeOut);
            return lngRes;
        }

        #endregion

        #region ҩƷ�깺��ͳ�Ʊ���
        /// <summary>
        /// ҩƷ�깺��ͳ�Ʊ���
        /// </summary>
        /// <param name="arrPrID">һ���е����в�����</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetReportDataOfInAndOutYear(System.Collections.Generic.List<string> arrPrID, out DataTable dt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfInAndOutYear(arrPrID, out dt);
            return lngRes;
        }

        #endregion

        #region ҩƷ��������ϸ����
        /// <summary>
        /// ҩƷ��������ϸ����
        /// </summary>
        /// <param name="arrPrID">������</param>
        /// <param name="dt">ҩƷID</param>
        /// <returns></returns>
        public long m_lngGetReportDataOfInAndOutDe(System.Collections.Generic.List<string> arrPrID, System.Collections.Generic.List<string> arrUpPrID, string strMedID, int intMedType, out DataTable dt)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetReportDataOfInAndOutDe(arrPrID, arrUpPrID, strMedID, intMedType, out dt);
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ����ë����
        /// <summary>
        /// ��ȡҩƷ����ë����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineTypeID">ҩƷ����ID</param>
        /// <param name="p_douGrossprofitrate"></param>
        /// <returns></returns>
        public long m_lngGetGrossprofitrate(string p_strMedicineTypeID, out double p_douGrossprofitrate)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetGrossprofitrate(p_strMedicineTypeID, out p_douGrossprofitrate);
            return lngRes;
        }
        #endregion

        #region ��ȡ�������ۼ۷�ʽ
        /// <summary>
        /// ��ȡ�������ۼ۷�ʽ
        /// </summary>
        /// <param name="p_intRetailMethod">�������ۼ۷�ʽ</param>
        /// <returns></returns>
        internal long m_lngGetRetailMethod(out int p_intRetailMethod)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetSysSetting("5019", out p_intRetailMethod);
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ȡ���б������
        /// </summary>
        /// <param name="p_intYear">�б����</param>
        internal long m_lngGetStandardDate(out int p_intYear)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngGetSysSetting("5030", out p_intYear);
            return lngRes;
        }

        /// <summary>
        /// �����б����
        /// </summary>
        /// <param name="p_strMedicineID">ҩƷID</param>
        /// <param name="p_strReturn">���</param>
        internal long m_lngSaveStandardYear(string p_strMedicineID, string p_strYear)
        {
            long lngRes = 0;
            System.Security.Principal.IPrincipal p_objPrincipal = null;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSaveStandardYear(p_strMedicineID, p_strYear);
            return lngRes;
        }

        /// <summary>
        /// ������Ŀ������
        /// </summary>
        /// <param name="isAddNew"></param>
        /// <param name="objAlias_Vo"></param>
        public void m_mthSaveAlias(byte isAddNew, clsAlias_VO objAlias_Vo)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngSaveItemAlias(isAddNew, objAlias_Vo);
        }

        /// <summary>
        /// ��ȡ���ʲֿ�
        /// </summary>
        /// <param name="objStorageArr">���ʲֿ�</param>
        public void m_mthGetMaterialStorage(out clsStorage_VO[] objStorageArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_mthGetMaterialStorage(out objStorageArr);
        }

        internal long m_lngFillChargeItem(string p_MedicineID, DataTable p_dtbChargeItem)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsMedicineSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsMedicineSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore()).Service.m_lngFillChargeItem(p_MedicineID, out p_dtbChargeItem);
            return lngRes;
        }
    }
}
