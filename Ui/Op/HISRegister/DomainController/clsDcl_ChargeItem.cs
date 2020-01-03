using System;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsDcl_ChargeItem ��ժҪ˵����
    /// </summary>
    public class clsDcl_ChargeItem : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        public clsDcl_ChargeItem()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

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

        #region ��ѯ�շ���Ŀ��������
        public long m_mthFindCat(out clsCharegeItemCat_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.clsChargeItemSvc_m_lngFindChargeItemCatList(out p_objResultArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡҽ������
        /// <summary>
        /// ��ȡҽ������
        /// </summary>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        public long m_mthSelectOrderCate(out DataTable m_objTable)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthSelectOrderCate(out m_objTable);
            //objSvc.Dispose();
            return lngRes;

        }
        #endregion
        #region ��ȡִ��ҽ����������
        /// <summary>
        /// ��ȡִ��ҽ����������
        /// </summary>
        /// <param name="p_objResultdt"></param>
        /// <returns></returns>
        public long m_lngGetAllBihCate(out DataTable p_objResultdt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngGetAllBihCate(out p_objResultdt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡ��ҩƵ��
        /// <summary>
        /// ��ȡ��ҩƵ��
        /// </summary>
        /// <param name="m_strFindText"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindRecipeFreq(string m_strFindText, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindRecipeFreq(m_strFindText, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡִ�з���
        /// <summary>
        /// ��ȡִ�з���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strFindText"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindExeType(string m_strFindText, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //  (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindExeType(m_strFindText, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡҽ������
        /// <summary>
        /// ��ȡҽ������
        /// </summary>
        /// <param name="m_strFindText"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindOrderType(string m_strFindText, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindOrderType(m_strFindText, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ѯ�շ���Ŀ

        public long m_mthFindChargeItem(string strCatID, string strType, string strContent, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            long lngRes = proxy.Service.clsChargeItemSvc_m_mthFindChargeItem(strCatID, strType, strContent, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ѯ�շ���Ŀ

        public long m_mthFindChargeItem1(string strCatID, string strType, string strContent, out DataTable dt)
        {
            dt = null;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            long lngRes = proxy.Service.m_mthFindChargeItem1(strCatID, strType, strContent, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ѯ���е�λ
        public long m_mthGetUnit(out clsUnit_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsUnit_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngFindAllUnit(out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ѯ�����÷�
        public long m_mthGetUsage(out clsUsageType_VO[] objResult, string strEx)
        {
            long lngRes = 0;
            objResult = new clsUsageType_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngFindAllUsage(out objResult, strEx);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ���е���t_bse_nurseorder
        public long m_lngFindAllORDERIDFromT_bse_nurseorder(out clsUsageType_VO[] objResult)
        {
            long lngRes = 0;
            objResult = new clsUsageType_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsGetBase objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsGetBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsGetBase));
            lngRes = proxy.Service.m_lngFindAllORDERIDFromT_bse_nurseorder(out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����շ��ر����
        public long m_mthEXType(string strFlag, out clsChargeItemEXType_VO[] objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngFindChargeItemEXTypeListByFlag(strFlag, out objResult);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region �����շ���Ŀ
        public long m_mthDoAddNewChargeItem(clsChargeItem_VO p_objResultArr, out string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoAddNewChargeItem(p_objResultArr, out strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡ���뵥����
        public long m_mthFindApplyType(out DataTable dt, string p_strEx)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindApplyType(out dt, p_strEx);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡ��ҩƵ��
        /// <summary>
        ///  ��ȡ��ҩƵ��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindRecipeFreq(out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindRecipeFreq(out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region �޸��շ���Ŀ
        public long m_mthDoUpdChargeItem(clsChargeItem_VO p_objResultArr, string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDoUpdChargeItemByID(p_objResultArr, strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡҽ������
        /// <summary>
        /// ��ȡҽ������
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_getMEDICARETYPE(out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_getMEDICARETYPE(out dt);
            return lngRes;
        }
        #endregion
        #region ɾ���շ���Ŀ
        public long m_mthDelChargeItem(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDeleteChargeItemByID(strID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ɾ���շ���Ŀ
        public long m_mthChangeCat(string strID, string strType)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthChangeCat(strID, strType);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �ж��Ƿ�ռ�ñ��
        public long m_mthItemIsUsed(string strCode, string strItemID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthItemIsUsed(strCode, strItemID);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ���շ���Ŀͬ����������Ŀ
        /// <summary>
        /// ���շ���Ŀͬ����������Ŀ
        /// </summary>
        /// <param name="m_objData"></param>
        /// <returns></returns>
        public long m_mthChargeItemSynOrderDic(clsChargeItemSynToOrderDic[] m_objDataArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthChargeItemSynOrderDic(m_objDataArr);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region �༭�շ���Ŀ�������
        /// <summary>
        /// ��ȡ�Ѿ���������Ŀ
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_getSUBCHARGEITEM(string itemID, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_getSUBCHARGEITEM(itemID, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngSaveSunItem(string itemID, DataTable dt, DataTable updt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngSaveSunItem(itemID, dt, updt);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ɾ��������Ŀ
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="sumItemID"></param>
        /// <returns></returns>
        public long m_lngDeleteSunItem(string itemID, string sumItemID, bool isDeleAll)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_lngDeleteSunItem(itemID, sumItemID, isDeleAll);
            //objSvc.Dispose();
            return lngRes;
        }
        /// <summary>
        /// ��ѯ�շ���Ŀ
        /// </summary>
        /// <param name="p_strFindString"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_mthFindMedicineByID(out DataTable p_dtResult, string strFindfild, string strFind)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindMedicineByID(out p_dtResult, strFindfild, strFind);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �����÷���λƵ������������
        /// <summary>
        /// ��ȡ�����÷���λƵ������������
        /// </summary>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_dbleQTY">����	{if(p_intType==1) һ������; if(p_intType==2) ҽ���µļ���;}</param>
        /// <param name="p_intType">{1=������λ;2=������λ}</param>
        /// <param name="p_dblUnitDosage">��λ����	{ֻ��p_intType==2���˲�����������}</param>
        /// <param name="p_dblGet">��λƵ������������	[out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=����*(ҽ���µļ���/��λ����)��}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public long m_lngGetMeasureClinicUsage(int p_intTIMES, double p_dbleQTY, int p_intType, double p_dblUnitDosage, out double p_dblGet)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetMeasureClinicUsage(p_intTIMES, p_dbleQTY, p_intType, p_dblUnitDosage, out p_dblGet);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region �����÷��շ�
        /// <summary>
        /// ��ȡ�����÷��շ�
        /// </summary>
        /// <param name="p_dblPrice">�۸�</param>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_dbleQTY">����	{if(p_intType==1) һ������; if(p_intType==2) ҽ���µļ���;}</param>
        /// <param name="p_intType">{1=������λ;2=������λ}</param>
        /// <param name="p_dblUnitDosage">��λ����	{ֻ��p_intType==2���˲�����������}</param>
        /// <param name="p_dblMoney">��λƵ�������ܼ�	[out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=����*(ҽ���µļ���/��λ����)}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public long m_lngGetChargeClinicUsage(double p_dblPrice, int p_intTIMES, double p_dbleQTY, int p_intType, double p_dblUnitDosage, out double p_dblMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetChargeClinicUsage(p_dblPrice, p_intTIMES, p_dbleQTY, p_intType, p_dblUnitDosage, out p_dblMoney);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region סԺ�÷���λƵ������������
        /// <summary>
        /// ��ȡסԺ�÷���λƵ������������
        /// </summary>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_dbleQTY">����	{if(p_intType==1) һ������; if(p_intType==2) ҽ���µļ���;}</param>
        /// <param name="p_intType">{1=������λ;2=������λ}</param>
        /// <param name="p_dblUnitDosage">��λ����	{ֻ��p_intType==2���˲�����������}</param>
        /// <param name="p_dblGet">��λƵ������������	[out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=����*(ҽ���µļ���/��λ����)��}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public long m_lngGetMeasureBIHUsage(int p_intTIMES, double p_dbleQTY, int p_intType, double p_dblUnitDosage, out double p_dblGet)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetMeasureBIHUsage(p_intTIMES, p_dbleQTY, p_intType, p_dblUnitDosage, out p_dblGet);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region סԺ�÷��շ�
        /// <summary>
        /// ��ȡסԺ�÷��շ�
        /// </summary>
        /// <param name="p_dblPrice">�۸�</param>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_dbleQTY">����	{if(p_intType==1) һ������; if(p_intType==2) ҽ���µļ���;}</param>
        /// <param name="p_intType">{1=������λ;2=������λ}</param>
        /// <param name="p_dblUnitDosage">��λ����	{ֻ��p_intType==2���˲�����������}</param>
        /// <param name="p_dblMoney">��λƵ�������ܼ�	[out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=����*(ҽ���µļ���/��λ����)��}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public long m_lngGetChargeBIHUsage(double p_dblPrice, int p_intTIMES, double p_dbleQTY, int p_intType, double p_dblUnitDosage, out double p_dblMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetChargeBIHUsage(p_dblPrice, p_intTIMES, p_dbleQTY, p_intType, p_dblUnitDosage, out p_dblMoney);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡ�����ܼۼ�סԺ�ܼ�
        /// <summary>
        /// ��ȡ�����ܼۼ�סԺ�ܼ�
        /// </summary>
        /// <param name="strITEMID_CHR">��ĿID</param>
        /// <param name="intType">1-�����ܼۣ�2-סԺ�ܼ�</param>
        /// <param name="dblQTY">����</param>
        /// <param name="intNuit">1-��ҩ��λ��2-������λ</param>
        /// <param name="dblTotailMoney">�����ܽ��</param>
        /// <returns></returns>
        public long m_lngGetChargeUsageTotailMoney(string strITEMID_CHR, int intType, double dblQTY, int intNuit, out double dblTotailMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = proxy.Service.m_lngGetChargeUsageTotailMoney(strITEMID_CHR, intType, dblQTY, intNuit, out dblTotailMoney);
            return lngRes;
        }
        #endregion
        #region ���ط���
        public long m_mthLoadCheckType(out DataTable dt, string strEx)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthLoadCheckType(out dt, strEx);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ���ط���
        public long m_lngUpdateOrderDicByChargeItemId(clsChargeItem_VO clsVO)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));

            string strSetStatus = "0";

            lngRes = proxy.Service.GetSysSetting("1031", out strSetStatus);

            if (lngRes > 0 && strSetStatus == "1")
            {
                lngRes = proxy.Service.m_lngUpdateOrderDicByChargeItemId(clsVO);
            }
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion
        #region ��ȡ�շ���Ŀִ�з���
        /// <summary>
        /// ��ȡ�շ���Ŀִ�з���
        /// </summary>
        /// <param name="m_strFindText"></param>
        /// <param name="m_intFlag"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthFindExeType(string m_strFindText, int m_intFlag, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));
            lngRes = proxy.Service.m_mthFindExeType(m_strFindText, m_intFlag, out dt);
            //objSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region GetItemChildPriceHis
        /// <summary>
        /// GetItemChildPriceHis
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public DataTable GetItemChildPriceHis(string itemId)
        {
            //using (clsChargeItemSvc svc = (clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsChargeItemSvc)))
            //{
            return proxy.Service.GetItemChildPriceHis(itemId);
            //}
        }
        #endregion
    }
}
