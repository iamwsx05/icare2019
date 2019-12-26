using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ҽ��¼��	�߼����Ʋ�
    /// ���ߣ� ����
    /// ����ʱ�䣺 2004-12-23
    /// </summary>
    public class clsDcl_InputOrder : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ���캯��
        public clsDcl_InputOrder()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        //��ȡ����
        #region ������Ŀ��
        /// <summary>
        /// ��ȡ������Ŀ��	����������ĿID
        /// </summary>
        /// <param name="p_strID">������ĿID</param>
        /// <returns></returns>
        public string m_strGetOrderdicNameByID(string p_strID)
        {
            clsT_bse_bih_orderdic_VO objReslut = new clsT_bse_bih_orderdic_VO();
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            long lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetOrderdicByOrdericid(p_strID, out objReslut);
            //objSvc.Dispose();
            //objSvc = null;
            if (lngRes > 0 && objReslut != null)
                return objReslut.m_strNAME_CHR;
            else
                return "";
        }
        #endregion
        #region ִ��Ƶ��
        /// <summary>
        /// ��ȡִ��Ƶ��	����ִ��Ƶ��ID
        /// </summary>
        /// <param name="p_strID">ִ��Ƶ��ID</param>
        /// <returns></returns>
        public string m_strGetFreqNameByID(string p_strID)
        {
            clsT_aid_recipefreq_VO objReslut = new clsT_aid_recipefreq_VO();
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            long lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetAidRecipefreqByID(p_strID, out objReslut);
            //objSvc.Dispose();
            //objSvc = null;
            if (lngRes > 0 && objReslut != null)
                return objReslut.m_strFREQNAME_CHR;
            else
                return "";
        }
        #endregion
        #region ��ѯ��ҩƵ��
        /// <summary>
        /// ��ѯ��ҩƵ��
        /// </summary>
        /// <param name="p_strID">��ҩƵ��ID</param>
        /// <param name="p_objResult">��ҩƵ�ʶ���</param>
        /// <returns></returns>
        public long m_lngGetAidRecipefreqByID(string p_strID, out clsT_aid_recipefreq_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetAidRecipefreqByID(p_strID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ������
        /// <summary>
        /// ��ȡҽ������
        /// </summary>
        /// <param name="p_objResultArr">ҽ�����Ͷ���</param>
        /// <returns></returns>
        public long m_lngGetAidOrderCate(out clsT_aid_bih_ordercate_VO[] p_objResultArr)
        {
            p_objResultArr = new clsT_aid_bih_ordercate_VO[0];
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            long lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetAidOrderCate("", out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ѯסԺ�������ñ�VO

        internal long m_lngAddGetSPECORDERCATE(out clsSPECORDERCATE m_objSpecateVo)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            //    (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddGetSPECORDERCATE(out m_objSpecateVo);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��ѯ��ǰҽ���Ƿ��д���Ȩ

        internal long m_lngAddGetAccessPower(string m_strEmpID, out bool m_blAcess)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            //    (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddGetAccessPower(m_strEmpID, out m_blAcess);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��ҩ��ʽ
        /// <summary>
        /// ��ȡ��ҩ��ʽ	�����÷�ID
        /// </summary>
        /// <param name="p_strID">�÷�ID</param>
        /// <returns></returns>
        public string m_strGetUsageTypeNameByID(string p_strID)
        {
            clsBSEUsageType objReslut = new clsBSEUsageType();
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetUsageTypeByID(p_strID, out objReslut);
            //objSvc.Dispose();
            //objSvc = null;
            if (lngRes > 0 && objReslut != null)
                return objReslut.m_strUsageName;
            else
                return "";
        }
        #endregion
        #region ��ȡ��ҩ��ʽ
        /// <summary>
        /// ��ȡ��ҩ��ʽ����
        /// </summary>
        /// <param name="p_strFindString">��ѯ�ַ���</param>
        /// <param name="p_objResultArr">��ҩ��ʽ����</param>
        /// <returns></returns>
        public long m_lngGetUsageType(string p_strFindString, out clsBSEUsageType[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetUsageType(p_strFindString, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ����ҽ�����
        //��ȡҽ��
        /// <summary>
        /// ��ȡ����������ҽ��	������Ժ�Ǽ�ID	
        /// ����:	
        ///		1��״̬Ϊ3-ֹͣ;6-���ֹͣ�ĳ���ҽ��;
        ///		2��ִ��״̬����ʱҽ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
        /// <param name="p_objResultArr">[out ����]</param>
        /// <returns></returns>
        public long m_lngGetCanReformingOrder(string p_strRegisterID, out clsBIHOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCanReformingOrder(p_strRegisterID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p_objItem">ҽ������</param>
        /// <param name="p_strDoctorID">����ҽ��ID</param>
        /// <param name="p_strDoctorName">����ҽ������</param>
        /// <param name="p_blnInfectSon">�Ƿ�������ҽ��</param>
        /// <returns></returns>
        public long m_lngRetractOrder(clsBIHOrder p_objItem, string p_strDoctorID, string p_strDoctorName, bool p_blnInfectSon, bool isChildPrice)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngRetractOrder(p_objItem, p_strDoctorID, p_strDoctorName, p_blnInfectSon, isChildPrice);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ֹͣ
        /// </summary>
        /// <param name="p_objItem">ҽ������</param>
        /// <param name="p_strDoctorID">����ҽ��ID</param>
        /// <param name="p_strDoctorName">����ҽ������</param>
        /// <param name="p_blnInfectSon">�Ƿ�ֹͣ��ҽ��</param>
        /// <returns></returns>
        public long m_lngStopOrder(clsBIHOrder p_objItem, string p_strDoctorID, string p_strDoctorName, bool p_blnInfectSon, bool isChildPrice)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngStopOrder(p_objItem, p_strDoctorID, p_strDoctorName, p_blnInfectSon, DateTime.MinValue, isChildPrice);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ��ֹͣ��ҽ��
        //��ȡҽ��
        /// <summary>
        /// ��ȡ����ֹͣ��ҽ��	������Ժ�Ǽ�ID	
        /// ҵ��˵��:	ֻ��ִֹͣ��״̬�ĳ���ҽ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
        /// <param name="p_objResultArr">[out ����]</param>
        /// <returns></returns>
        public long m_lngGetCanStopOrder(string p_strRegisterID, out clsBIHOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCanStopOrder(p_strRegisterID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region �޸�ҽ��
        /// <summary>
        /// ҽ���޸�δ�ύҽ��������
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns></returns>
        public long m_lngModifyOrder(clsBIHOrder objOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyOrder(objOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ����ǰҽ����Ϊ�¿�ҽ�����޸�(��Ҫ����޸ķ��ż���ҽ����־��Ϊ�� -- �÷�������ʹ��20180403
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns></returns>
        public long m_lngModifyNewRecipenNoOrder(clsBIHOrder objOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyNewRecipenNoOrder(objOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ҽ���޸�δ�ύҽ��������	������ҽ������ҽ��ֻ����Ƶ�ʺ��÷�
        /// ע�⣺�����Ǹ�����
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns></returns>
        public long m_lngModifyOrderWithSon(clsBIHOrder objOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyOrderWithSon(objOrder);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch { }
            return lngRes;
        }

        #endregion
        /// <summary>
        /// ��ȡҽ����Ϣ	����������ĿID
        /// </summary>
        /// <param name="p_strOrderdicID">������ĿID</param>
        /// <param name="p_dtbResult">[out ����]</param>
        /// <returns></returns>
        public long m_lngGetMedicareByOrderdicID(string p_strOrderdicID, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetMedicareByOrderdicID(p_strOrderdicID, out p_dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��ȡҽ����Ϣ	����������Ŀ����
        /// </summary>
        /// <param name="p_strUserCode">������Ŀ����</param>
        /// <param name="p_dtbResult">[out ����]</param>
        /// <returns></returns>
        public long m_lngGetMedicareByUserCode(string p_strUserCode, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetMedicareByUserCode(p_strUserCode, out p_dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /** @add by xzf (05-09-21 */
        /// <summary>
        /// ��ȡ�û�����,����������Ŀid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getOrderDicUserCodeById(string id)
        {
            string userCode = "";
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            userCode = (new weCare.Proxy.ProxyIP()).Service.getOrderDicUserCodeById(id);
            //objSvc.Dispose();
            //objSvc = null;
            return userCode;

        }
        //�շ�
        #region �����շ���Ŀ�Ʒ�
        /// <summary>
        /// ��������շ���Ŀ����
        /// </summary>
        /// <param name="p_objRecipeFreq">��ҩƵ�ʸ��������</param>
        /// <param name="p_objOrderdicCharge">������Ŀ|�շ���Ŀ����</param>
        /// <param name="p_dmlGet">��λƵ������������	[out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=�㷨�������շ���Ŀ}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public decimal m_dmlGetChargeNotMainItem(string p_strFreqID, clsT_aid_bih_orderdic_charge_VO p_objOrderdicCharge)
        {
            decimal dmlGet = 0;
            clsT_aid_recipefreq_VO objItem = new clsT_aid_recipefreq_VO();
            long lngRes = m_lngGetAidRecipefreqByID(p_strFreqID, out objItem);
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetChargeNotMainItem(objItem, p_objOrderdicCharge, out dmlGet);
            //objSvc.Dispose();
            //objSvc = null;
            return dmlGet;
        }
        /// <summary>
        /// ��������շ���Ŀ����
        /// </summary>
        /// <param name="p_objRecipeFreq">��ҩƵ�ʸ��������</param>
        /// <param name="p_objOrderdicCharge">������Ŀ|�շ���Ŀ����</param>
        /// <param name="p_dmlGet">��λƵ������������	[out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=�㷨�������շ���Ŀ}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public decimal m_dmlGetChargeNotMainItem(clsT_aid_recipefreq_VO p_objRecipeFreq, clsT_aid_bih_orderdic_charge_VO p_objOrderdicCharge)
        {
            decimal dmlGet = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            long lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetChargeNotMainItem(p_objRecipeFreq, p_objOrderdicCharge, out dmlGet);
            //objSvc.Dispose();
            //objSvc = null;
            return dmlGet;
        }
        /// <summary>
        /// ��������շ���Ŀ����
        /// </summary>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_objOrderdicCharge">������Ŀ|�շ���Ŀ����</param>
        /// <param name="p_dmlGet">��λƵ������������	[out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=�㷨�������շ���Ŀ}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public decimal m_lngGetChargeNotMainItem(int p_intTIMES, clsT_aid_bih_orderdic_charge_VO p_objOrderdicCharge)
        {
            decimal dmlGet = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            long lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetChargeNotMainItem(p_intTIMES, p_objOrderdicCharge, out dmlGet);
            //objSvc.Dispose();
            //objSvc = null;
            return dmlGet;
        }
        /// <summary>
        /// ��������շ���Ŀ����
        /// </summary>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="m_intQTY">����</param>
        /// <param name="p_intType">{1=������λ;2=������λ}</param>
        /// <param name="p_dmlDosage">ҽ���µļ���</param>
        /// <param name="p_dmlUnitDosage">������λ</param>
        /// <param name="p_dmlGet">��λƵ������������	[out ����]</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=�㷨�������շ���Ŀ��}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public decimal m_lngGetChargeNotMainItem(int p_intTIMES, int p_intQTY, int p_intType, decimal p_dmlDosage, decimal p_dmlUnitDosage)
        {
            decimal dmlGet = 0;
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            long lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetChargeNotMainItem(p_intTIMES, p_intQTY, p_intType, p_dmlDosage, p_dmlUnitDosage, out dmlGet);
            //objSvc.Dispose();
            //objSvc = null;
            return dmlGet;
        }
        #endregion
        #region סԺ�÷���λƵ������������
        /// <summary>
        /// ��ȡסԺ�÷���λƵ������������
        /// </summary>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_dblQTY">����	{if(p_intType==1) һ������; if(p_intType==2) ҽ���µļ���;}</param>
        /// <param name="p_intType">{1=������λ;2=������λ}</param>
        /// <param name="p_dblUnitDosage">��λ����	{ֻ��p_intType==2���˲�����������}</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=����*(ҽ���µļ���/��λ����)��}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public double m_dblGetMeasureBIHUsage(int p_intTIMES, double p_dblQTY, int p_intType, double p_dblUnitDosage)
        {
            double dblGet = 0;
            long lngRes = m_lngGetMeasureBIHUsage(p_intTIMES, p_dblQTY, p_intType, p_dblUnitDosage, out dblGet);
            return dblGet;
        }
        /// <summary>
        /// ��ȡסԺ�÷���λƵ������������
        /// </summary>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_dblQTY">����	{if(p_intType==1) һ������; if(p_intType==2) ҽ���µļ���;}</param>
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
        public long m_lngGetMeasureBIHUsage(int p_intTIMES, double p_dblQTY, int p_intType, double p_dblUnitDosage, out double p_dblGet)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc =(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            //lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetMeasureBIHUsage(p_intTIMES,p_dblQTY,p_intType,p_dblUnitDosage,out p_dblGet);
            p_dblGet = 0;
            if (p_intType == 2)//������λ
            {
                double dblUse = p_dblQTY / p_dblUnitDosage;
                p_dblGet = dblUse * p_intTIMES;	//����*����
            }
            else if (p_intType == 1)//������λ
            {
                p_dblGet = p_dblQTY * p_intTIMES;
            }
            return lngRes;
        }
        #endregion
        #region סԺ�÷��շ�
        /// <summary>
        /// ��ȡסԺ�÷��շ�
        /// </summary>
        /// <param name="strITEMID_CHR">�շ���ĿID</param>
        /// <param name="strUSAGEID_CHR">�÷�ID</param>
        /// <returns></returns>
        public double m_dblGetChargeBIHUsage(string strITEMID_CHR, string strUSAGEID_CHR)
        {
            double dblMoney = 0;
            long lngRes = m_lngGetChargeBIHUsage(strITEMID_CHR, strUSAGEID_CHR, out dblMoney);
            return dblMoney;
        }
        /// <summary>
        /// ��ȡסԺ�÷��շ�
        /// </summary>
        /// <param name="p_dblPrice">�۸�</param>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_dblQTY">����	{if(p_intType==1) һ������; if(p_intType==2) ҽ���µļ���;}</param>
        /// <param name="p_intType">{1=������λ;2=������λ}</param>
        /// <param name="p_dblUnitDosage">��λ����	{ֻ��p_intType==2���˲�����������}</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��������
        ///		if(TYPE_INT==1[������λ]) then {=����*����}
        ///		if(TYPE_INT==2[������λ]) then {=����*(ҽ���µļ���/��λ����)��}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        public double m_dblGetChargeBIHUsage(double p_dblPrice, int p_intTIMES, double p_dblQTY, int p_intType, double p_dblUnitDosage)
        {
            double dblMoney = 0;
            long lngRes = m_lngGetChargeBIHUsage(p_dblPrice, p_intTIMES, p_dblQTY, p_intType, p_dblUnitDosage, out dblMoney);
            return dblMoney;
        }
        /// <summary>
        /// ��ȡסԺ�÷��շ�
        /// </summary>
        /// <param name="p_dblPrice">�۸�</param>
        /// <param name="p_intTIMES">������ҩ����</param>
        /// <param name="p_dblQTY">����	{if(p_intType==1) һ������; if(p_intType==2) ҽ���µļ���;}</param>
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
        public long m_lngGetChargeBIHUsage(double p_dblPrice, int p_intTIMES, double p_dblQTY, int p_intType, double p_dblUnitDosage, out double p_dblMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetChargeBIHUsage(p_dblPrice, p_intTIMES, p_dblQTY, p_intType, p_dblUnitDosage, out p_dblMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��ȡסԺ�÷��շ�
        /// </summary>
        /// <param name="strITEMID_CHR">�շ���ĿID</param>
        /// <param name="strUSAGEID_CHR">�÷�ID</param>
        /// <param name="p_dblMoney">�շ�	[out ����]</param>
        /// <returns></returns>
        public long m_lngGetChargeBIHUsage(string strITEMID_CHR, string strUSAGEID_CHR, out double p_dblMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc objSvc = (com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAccountCharge_Svc));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetChargeBIHUsage(strITEMID_CHR, strUSAGEID_CHR, out p_dblMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ�÷���Ӧ����Ŀ
        /// <summary>
        /// ��ȡ�÷���Ӧ����Ŀ	�����÷�ID
        /// </summary>
        /// <param name="p_strUsageID">�÷�ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_GetItemByUsageID(string p_strOrderID, out clsChargeItem_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderChangeService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderChangeService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderChangeService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_GetItemByUsageID(p_strOrderID, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region סԺ�շ�
        /// <summary>
        /// ��ʾ������Ϣ	{ҩƷ���á��÷�����}
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_intIsSonOrder">�Ƿ��Ӽ�ҽ��	{0=���Ӽ�ҽ��;1=�Ӽ�ҽ��}</param>
        /// <param name="p_dblDraw">���շ���Ŀ������</param>
        /// <param name="p_strFreqID">ִ��Ƶ��ID</param>
        /// <param name="p_strUsageID">�÷�ID</param>
        /// <param name="p_objResultArr">[out ����] ������������</param>
        /// <returns></returns>
        public long m_lngGetBIHCharge(string p_strOrderID, int p_intIsSonOrder, double p_dblDraw, string p_strFreqID, string p_strUsageID, out clsChargeForDisplay[] p_objResultArr, bool isChildPrice)
        {
            long lngRes = 0;
            p_objResultArr = new clsChargeForDisplay[0];
            ArrayList alItem = new ArrayList();
            clsChargeForDisplay objItem;

            //�Ƿ�������ҽ��
            bool blnIsConOrder = false;
            string strConfreqID = new clsDcl_ExecuteOrder().m_strGetConfreqID();
            if (strConfreqID.Trim() == p_strFreqID) blnIsConOrder = true;

            //��ȡ��ҩƵ�ʶ���
            clsT_aid_recipefreq_VO objRecipeFreq = new clsT_aid_recipefreq_VO();
            lngRes = m_lngGetAidRecipefreqByID(p_strFreqID, out objRecipeFreq);
            if (lngRes <= 0) return 0;

            //��ȡ������Ŀ-�շ���Ŀ����	{�Է�|���е�ҽ������ҩƷ����}
            clsBIHOrder objBIHOrder;
            lngRes = m_lngGetOrderByOrderID(p_strOrderID, out objBIHOrder);
            if (lngRes > 0 && objBIHOrder != null && objBIHOrder.RateType != 1 /* && objBIHOrder.m_intRateType != 2*/)
            {
                clsT_aid_bih_orderdic_charge_VO[] objMedicineItemArr;
                lngRes = new clsDcl_CommitOrder().m_lngGetOrderdicChargeByOrderID(p_strOrderID, out objMedicineItemArr);
                #region ҩƷ����
                if (lngRes > 0 && objMedicineItemArr != null && objMedicineItemArr.Length > 0)
                {
                    for (int i1 = 0; i1 < objMedicineItemArr.Length; i1++)
                    {
                        objItem = new clsChargeForDisplay();
                        objItem.m_strChargeID = objMedicineItemArr[i1].m_strITEMID_CHR;
                        //�շ���Ŀ����
                        objItem.m_strName = objMedicineItemArr[i1].m_strItemName;
                        double dblNum = 0;
                        if (objMedicineItemArr[i1].m_objChargeItem.m_strITEMID_CHR.Trim() == objMedicineItemArr[i1].m_strChiefItemID.Trim())//�Ƿ����շ���Ŀ
                        {
                            dblNum = p_dblDraw;
                            //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                            objItem.m_intType = 2;
                        }
                        else
                        {
                            dblNum = System.Convert.ToDouble(m_dmlGetChargeNotMainItem(objRecipeFreq, objMedicineItemArr[i1]));
                            //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                            objItem.m_intType = 1;
                        }
                        //����
                        objItem.m_dblPrice = objMedicineItemArr[i1].m_objChargeItem.m_dblMinPrice;
                        //   �޸ĵ��ۼ�������Դ

                        objItem.m_dblPrice = (double)objMedicineItemArr[i1].m_objORDERCHARGEDEPT_VO.m_decUnitprice_dec;
                        dblNum = (double)objMedicineItemArr[i1].m_objORDERCHARGEDEPT_VO.m_decAmount_dec;
                        /*<---------------------------------*/
                        //����
                        objItem.m_dblDrawAmount = dblNum;

                        //�ϼƽ��
                        objItem.m_dblMoney = objMedicineItemArr[i1].m_objChargeItem.m_dblMinPrice * dblNum;
                        //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                        objItem.m_intCONTINUEUSETYPE_INT = -1;
                        //�Ƿ�������ҽ��	{0=��1=��} ������ҽ������ʾҩƷ������Ϣ��
                        objItem.m_intIsContinueOrder = (blnIsConOrder) ? (1) : (0);
                        //�Ƿ�ȱҩ
                        objItem.m_strNoqtyFLag = objMedicineItemArr[i1].m_strNoqtyFLag;
                        // ���Ͽ�������
                        objItem.m_strClacarea_chr = objMedicineItemArr[i1].m_objChargeItem.m_strITEMPYCODE_CHR;
                        objItem.m_strClacareaName_chr = objMedicineItemArr[i1].m_objChargeItem.m_strITEMWBCODE_CHR;
                        //�ݴ�סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��
                        objItem.m_strSeq_int = objMedicineItemArr[i1].m_strOCMAPID_CHR;

                        // סԺ������Ŀ�շ���Ŀִ�пͻ���VO
                        objItem.m_objORDERCHARGEDEPT_VO = objMedicineItemArr[i1].m_objORDERCHARGEDEPT_VO;

                        alItem.Add(objItem);
                    }
                }
                #endregion
            }

            //��ȡ�÷���Ӧ����Ŀ����
            if (p_intIsSonOrder != 1 && p_strUsageID != null && p_strUsageID.Trim() != "")
            {
                clsChargeItem_VO[] objUsageResultArr;
                //lngRes =m_GetItemByUsageID(p_strUsageID,out objUsageResultArr);
                lngRes = m_GetItemByUsageID(p_strOrderID, out objUsageResultArr);

                #region �÷�����
                if (lngRes > 0 && objUsageResultArr != null && objUsageResultArr.Length > 0)
                {
                    for (int i1 = 0; i1 < objUsageResultArr.Length; i1++)
                    {
                        objItem = new clsChargeForDisplay();
                        objItem.m_strChargeID = objUsageResultArr[i1].m_strItemID;
                        //�շ���Ŀ����
                        objItem.m_strName = objUsageResultArr[i1].m_strItemName;
                        //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                        objItem.m_intType = 3;
                        //���� decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice
                        double dblPrice = 0;
                        try
                        {
                            //סԺ�շѵ�λ 0 ��������λ 1����С��λ
                            if (objUsageResultArr[i1].m_intOPCHARGEFLG_INT == 0)//�����շѵ�λ 0 ��������λ 1����С��λ
                                dblPrice = double.Parse(objUsageResultArr[i1].m_fltItemPrice.ToString());
                            else
                            {
                                double dblItemPrice = double.Parse(objUsageResultArr[i1].m_fltItemPrice.ToString());
                                double dblPACKQTY_DEC = double.Parse(objUsageResultArr[i1].m_decPACKQTY_DEC.ToString());
                                dblPrice = double.Parse((dblItemPrice / dblPACKQTY_DEC).ToString("0.0000"));
                            }
                        }
                        catch { }
                        objItem.m_dblPrice = dblPrice;
                        //ҽ���µļ���
                        double dblDosage = 0;
                        try
                        {
                            dblDosage = double.Parse(objUsageResultArr[i1].m_strDosage.ToString());
                        }
                        catch { }
                        //����					
                        double dblNum = 0;
                        dblNum = m_dblGetMeasureBIHUsage(objRecipeFreq.m_intTIMES_INT, objUsageResultArr[i1].m_dblBIHQTY_DEC, objUsageResultArr[i1].m_intBIHTYPE_INT, dblDosage);
                        // �޸ĵ��ۼ�������Դ

                        objItem.m_dblPrice = (double)objUsageResultArr[i1].m_objORDERCHARGEDEPT_VO.m_decUnitprice_dec;
                        dblNum = (double)objUsageResultArr[i1].m_objORDERCHARGEDEPT_VO.m_decAmount_dec;
                        /*<---------------------------------*/
                        objItem.m_dblDrawAmount = dblNum;
                        //�ϼƽ��
                        objItem.m_dblMoney = dblPrice * dblNum;
                        //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                        objItem.m_intCONTINUEUSETYPE_INT = objUsageResultArr[i1].m_intCONTINUEUSETYPE_INT;
                        //�Ƿ�������ҽ��	{0=��1=��} ������ҽ������ʾҩƷ������Ϣ��
                        objItem.m_intIsContinueOrder = (blnIsConOrder) ? (1) : (0);
                        // ���Ͽ�������
                        objItem.m_strClacarea_chr = objUsageResultArr[i1].m_strItemPYCode;
                        objItem.m_strClacareaName_chr = objUsageResultArr[i1].m_strItemWBCode;
                        /*<----------------------------*/
                        //�ݴ�סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��
                        objItem.m_strSeq_int = objUsageResultArr[i1].m_strItemCode;
                        // סԺ������Ŀ�շ���Ŀִ�пͻ���VO
                        objItem.m_objORDERCHARGEDEPT_VO = objUsageResultArr[i1].m_objORDERCHARGEDEPT_VO;
                        alItem.Add(objItem);
                    }
                }
                #endregion
            }
            p_objResultArr = (clsChargeForDisplay[])(alItem.ToArray(typeof(clsChargeForDisplay)));
            return lngRes;
        }
        #endregion

        #region ҽ���շ���Ϣ(��Դ��T_OPR_BIH_ORDERCHARGEDEPTסԺ������Ŀ�շ���Ŀִ�пͻ���)

        public long m_lngGetBIHChargeFromDEPT(string m_strOrderid_chr, out DataTable m_dtChargeList)
        {
            long lngRes;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetBIHChargeFromDEPT(m_strOrderid_chr, out m_dtChargeList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ʾ������Ϣ
        /// <summary>
        /// ��ʾ������Ϣ	{ҩƷ���á��÷�����}
        /// </summary>
        /// <param name="objItemArr">������������</param>
        /// <param name="p_dgDataGrid"></param>
        public void DisplayCharge(clsChargeForDisplay[] objItemArr, com.digitalwave.controls.datagrid.ctlDataGrid p_dgDataGrid)
        {
            ////ҩƷ������ɫ
            //System.Drawing.Color clMedicineBackColor =System.Drawing.SystemColors.Window;
            //System.Drawing.Color clMedicineForeColor =System.Drawing.SystemColors.WindowText;
            ////�÷�������ɫ
            //System.Drawing.Color clUsageBackColor =System.Drawing.Color.LightGreen;
            //System.Drawing.Color clUsageForeColor =System.Drawing.SystemColors.WindowText;

            p_dgDataGrid.BeginUpdate();
            p_dgDataGrid.m_mthDeleteAllRow();
            p_dgDataGrid.m_mthFormatReset();

            if (objItemArr != null && objItemArr.Length > 0)
            {
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    DataRow objRow = p_dgDataGrid.NewRow();
                    //���
                    objRow[0] = (i1 + 1).ToString();
                    //�շ���Ŀ����
                    objRow[1] = objItemArr[i1].m_strName;
                    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    switch (objItemArr[i1].m_intType)
                    {
                        case 1:
                            objRow[2] = "�����շ�";
                            break;
                        case 2:
                            objRow[2] = "���շ�";
                            break;
                        case 3:
                            objRow[2] = "�÷��շ�";
                            break;
                        default:
                            objRow[2] = "";
                            break;
                    }
                    //����		
                    if (!double.IsInfinity(objItemArr[i1].m_dblPrice))
                        objRow[3] = objItemArr[i1].m_dblPrice.ToString("0.0000");
                    else
                        objRow[3] = "0.0000";
                    if (objItemArr[i1].m_intIsContinueOrder == 1 && objItemArr[i1].m_intType != 3)
                    {
                        //����					
                        objRow[4] = "-";
                        //�ϼƽ��
                        objRow[5] = "-";
                    }
                    else
                    {
                        //����					
                        objRow[4] = objItemArr[i1].m_dblDrawAmount.ToString();
                        //�ϼƽ��
                        if (!double.IsInfinity(objItemArr[i1].m_dblMoney))
                            objRow[5] = objItemArr[i1].m_dblMoney.ToString("0.00");
                        else
                            objRow[5] = "0.00";
                    }
                    //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                    switch (objItemArr[i1].m_intCONTINUEUSETYPE_INT)
                    {
                        case -1:
                            objRow[6] = "-";
                            break;
                        case 0:
                            objRow[6] = "������";
                            break;
                        case 1:
                            objRow[6] = "ȫ������";
                            break;
                        case 2:
                            objRow[6] = "��������";
                            break;
                        default:
                            objRow[6] = "";
                            break;
                    }
                    // ����ִ�п���
                    objRow[7] = objItemArr[i1].m_strClacareaName_chr;
                    /*<---------------------------*/
                    //�ݴ�סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��
                    objRow["seq_int"] = objItemArr[i1].m_strSeq_int;
                    /*<------------------------------------*/
                    // �շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    objRow["m_intType"] = objItemArr[i1].m_intType.ToString();
                    /*<---------------------------*/
                    p_dgDataGrid.m_mthAppendRow(objRow);
                    //�����ɫ
                    #region �����ɫ
                    switch (objItemArr[i1].m_intType)//{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    {
                        case 1:
                            //for(int intCol=0;intCol<p_dgDataGrid.Columns.Count;intCol++)
                            //{
                            //	p_dgDataGrid.m_mthFormatCell(p_dgDataGrid.RowCount-1,intCol,p_dgDataGrid.Font,clMedicineBackColor,clMedicineForeColor);
                            //}
                            break;
                        case 2:
                            //for(int intCol=0;intCol<p_dgDataGrid.Columns.Count;intCol++)
                            //{
                            //	p_dgDataGrid.m_mthFormatCell(p_dgDataGrid.RowCount-1,intCol,p_dgDataGrid.Font,clMedicineBackColor,clMedicineForeColor);
                            //}
                            break;
                        case 3:
                            //for(int intCol=0;intCol<p_dgDataGrid.Columns.Count;intCol++)
                            //{
                            //	p_dgDataGrid.m_mthFormatCell(p_dgDataGrid.RowCount-1,intCol,p_dgDataGrid.Font,clUsageBackColor,clUsageForeColor);
                            //}
                            break;
                    }
                    #endregion
                }
            }
            p_dgDataGrid.EndUpdate();
        }
        /// <summary>
        /// ��ʾ������Ϣ	{ҩƷ���á��÷�����}
        /// </summary>
        /// <param name="objItemArr">������������</param>
        /// <param name="p_dgDataGrid"></param>
        public void DisplayCharge(clsChargeForDisplay[] objItemArr, ListView p_lsvListView)
        {
            //ҩƷ������ɫ
            System.Drawing.Color clMedicineBackColor = System.Drawing.SystemColors.Window;
            System.Drawing.Color clMedicineForeColor = System.Drawing.SystemColors.WindowText;
            //�÷�������ɫ
            System.Drawing.Color clUsageBackColor = System.Drawing.Color.LightGreen;
            System.Drawing.Color clUsageForeColor = System.Drawing.SystemColors.WindowText;
            p_lsvListView.Items.Clear();

            if (objItemArr != null && objItemArr.Length > 0)
            {
                ListViewItem lviTemp = null;
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    //���
                    lviTemp = new ListViewItem((i1 + 1).ToString());
                    //�շ���Ŀ����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strITEMCODE_VCHR);
                    //�շ���Ŀ����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strName);
                    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    switch (objItemArr[i1].m_intType)
                    {
                        case 0:
                            lviTemp.SubItems.Add("����Ŀ");
                            break;
                        case 1:
                            lviTemp.SubItems.Add("������Ŀ");
                            break;
                        case 2:
                            lviTemp.SubItems.Add("�÷�����");
                            break;
                        case 3:
                            lviTemp.SubItems.Add("����¼����Ŀ");
                            break;
                        default:
                            lviTemp.SubItems.Add("");
                            break;
                    }
                    //����			
                    if (!double.IsInfinity(objItemArr[i1].m_dblPrice))
                        lviTemp.SubItems.Add(objItemArr[i1].m_dblPrice.ToString("0.00"));
                    else
                        lviTemp.SubItems.Add("0.00");
                    //if(objItemArr[i1].m_intIsContinueOrder==1 && objItemArr[i1].m_intType!=3)
                    //{
                    //    //����					
                    //    lviTemp.SubItems.Add("-");
                    //    //�ϼƽ��
                    //    lviTemp.SubItems.Add("-");
                    //}
                    //else
                    //{
                    //    //����					
                    //    lviTemp.SubItems.Add(objItemArr[i1].m_dblDrawAmount.ToString());
                    //    //�ϼƽ��
                    //    if(!double.IsInfinity(objItemArr[i1].m_dblMoney))
                    //        lviTemp.SubItems.Add(objItemArr[i1].m_dblMoney.ToString("0.00"));
                    //    else
                    //        lviTemp.SubItems.Add("0.00");
                    //}
                    //����					
                    lviTemp.SubItems.Add(objItemArr[i1].m_dblDrawAmount.ToString() + "(" + objItemArr[i1].m_strUNIT_VCHR + ")");
                    //�ϼƽ��
                    if (!double.IsInfinity(objItemArr[i1].m_dblMoney))
                        lviTemp.SubItems.Add(objItemArr[i1].m_dblMoney.ToString("0.00"));
                    else
                        lviTemp.SubItems.Add("0.00");
                    //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                    //switch(objItemArr[i1].m_intCONTINUEUSETYPE_INT)
                    //{
                    //    case -1:
                    //        lviTemp.SubItems.Add("-");
                    //        break;
                    //    case 0:
                    //        lviTemp.SubItems.Add("������");
                    //        break;
                    //    case 1:
                    //        lviTemp.SubItems.Add("ȫ������");
                    //        break;
                    //    case 2:
                    //        lviTemp.SubItems.Add("��������");
                    //        break;
                    //    default:
                    //        lviTemp.SubItems.Add("");
                    //        break;
                    //}
                    switch (objItemArr[i1].m_intCONTINUEUSETYPE_INT)
                    {
                        case 1:
                            lviTemp.SubItems.Add("  �״��á�");
                            break;
                        case 0:
                            lviTemp.SubItems.Add("  �����á�");
                            break;
                        default:
                            lviTemp.SubItems.Add(" -- ");
                            break;
                    }
                    // ����ִ�п���
                    lviTemp.SubItems.Add(objItemArr[i1].m_strClacareaName_chr);

                    lviTemp.SubItems.Add(objItemArr[i1].m_strYBClass);
                    lviTemp.Tag = objItemArr[i1];
                    #region �����ɫ
                    switch (objItemArr[i1].m_intType)//{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    {
                        case 1:
                            //lviTemp.BackColor =clMedicineBackColor;
                            //lviTemp.ForeColor =clMedicineForeColor;
                            break;
                        case 2:
                            //lviTemp.BackColor =clMedicineBackColor;
                            //lviTemp.ForeColor =clMedicineForeColor;
                            break;
                        case 3:
                            //lviTemp.BackColor =clUsageBackColor;
                            //lviTemp.ForeColor =clUsageForeColor;
                            break;
                    }
                    #endregion

                    #region ͻ����ʾȱҩ��Ŀ	glzhang	2005.10.14
                    if (objItemArr[i1].m_strNoqtyFLag == "1")
                    {
                        lviTemp.ForeColor = System.Drawing.Color.Red;
                        lviTemp.SubItems.Add("ȱҩ");
                    }
                    #endregion

                    p_lsvListView.Items.Add(lviTemp);
                    //�����ɫ
                }
            }
        }
        #endregion
        #region ��ȡ����������
        /// <summary>
        /// ��ȡ����������
        /// ҵ��˵����	�Ʒ�(������λ:Сʱ):
        ///				a.��һ��Ʒ�,���� = ��ʼʱ�� -- {����ʱ��};
        ///				b.�ڼ�Ʒ�,���� = {��һ�ι���ʱ��} -- {����ʱ��)	(ʱ��:�ڹ���ʱ);
        ///				c.ֹͣ�Ʒ�,���� = {��һ�ι���ʱ��} -- {ֹͣʱ��}	(ʱ��:�����ֹͣʱ)
        ///				����ʱ�̣�	23:59:59
        /// </summary>
        /// <param name="p_strOrderID"></param>
        /// <param name="p_dtStartTime">��ʼʱ��</param>
        /// <param name="p_dtBalanceTime">����ʱ��</param>
        /// <param name="p_intReturnType">��������{1=����;2Сʱ;Ĭ��ΪСʱ}</param>
        /// <returns>������������</returns>
        public int m_intGetMeasureForConOrder(DateTime p_dtStartTime, DateTime p_dtBalanceTime, int p_intReturnType)
        {
            int intGetMeasure = 0;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetMeasureForConOrder(p_dtStartTime, p_dtBalanceTime, p_intReturnType, out intGetMeasure);
            //objSvc.Dispose();
            //objSvc = null;
            return intGetMeasure;
        }
        #endregion

        #region ���ҽ����ֹͣʱ��
        /// <summary>
        /// ���ҽ����ֹͣʱ��
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_dtStopTime">ֹͣʱ��</param>
        /// <returns></returns>
        public long m_lngFillConOrderStopTime(string p_strOrderID, DateTime p_dtStopTime)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFillConOrderStopTime(p_strOrderID, p_dtStopTime);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡҽ�����֣ͣ�
        /// <summary>
        /// ��ѯҽ�����Ͷ���	����ҽ������ID
        /// </summary>
        /// <param name="p_strID">ҽ������ID</param>
        /// <param name="p_objResult">ҽ�����Ͷ���</param>
        /// <returns></returns>
        public long m_lngGetAidOrderCateByID(string p_strID, out clsT_aid_bih_ordercate_VO p_objResult)
        {
            //long lngRes=0;
            //com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc objSvc =(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsOrderdicChargeSvc));
            //lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetAidOrderCateByID(p_strID,out p_objResult);
            //return lngRes;
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetAidOrderCateByID(p_strID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #endregion
        #region �Ƿ���ڸ��ӵ���
        /// <summary>
        /// �Ƿ���ڸ��ӵ���	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <returns></returns>
        /// <remarks>
        /// ����ֻ�Ǹ��ݸ��ӵ���Ӱ������жϣ�Ӱ����������С�
        /// </remarks>
        public bool m_blnExistAttchOrder(string p_strOrderID)
        {
            long lngRes = 0;
            bool blnExist = false;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngExistAttchOrder(p_strOrderID, out blnExist);
            //objSvc.Dispose();
            //objSvc = null;
            return blnExist;
        }
        #endregion

        #region ���Ҳ���
        /// <summary>
        /// ���Ҳ���	���������ַ���
        /// </summary>
        /// <param name="strCode">�����ַ���</param>
        /// <param name="p_objResultArr">��������	[out ����]</param>
        public long m_lngFindArea(string strCode, out clsBIHArea[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(strCode, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ���Ҳ���
        /// <summary>
        /// ����ռ���������䲡����Ϣ	���ݲ���ID
        /// </summary>
        /// <param name="p_strID">����ID</param>
        /// <returns></returns>
        public long m_lngGetBedInfoByAreaID(string p_strAreaID, out clsT_Bse_Bed_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBedManageSvc objSvc =(com.digitalwave.iCare.middletier.HIS.clsBedManageSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBedManageSvc));

            //����״̬(Ϊ������Ϊ��ѯ������������ö��ŷָ�����: ��1,2,3��) {1=�մ�;2=ռ��;3=ԤԼռ��;4=����ռ��}
            /* �Ż���λ��ѯ*/
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderBedManager objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderBedManager)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderBedManager));

            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetBedInfoByAreaID(p_strAreaID, "2", out p_objResultArr, true);
            //objSvc.Dispose();
            //objSvc = null;
            /*<===============================================*/
            return lngRes;
        }
        #endregion
        #region ���Ҳ��˿���
        /// <summary>
        /// ��ȡ���˿��� ���ݲ���ID
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <returns></returns>
        public string m_strGetCardIDByID(string p_strPatientID)
        {
            string strCardID = "";
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCardIDByID(p_strPatientID, out strCardID);
            //objSvc.Dispose();
            //objSvc = null;
            return strCardID;
        }
        #endregion

        #region ����Ȩ��
        /// <summary>
        /// ���˳���Ȩ�޵Ĳ���
        /// </summary>
        /// <param name="p_objArea">��������</param>
        /// <param name="p_ilUsableAreaID">��Ȩ���ʵĲ���ID����</param>
        /// <returns>��Ȩ���ʵĲ������󼯺�</returns>
        public ArrayList GetUsableAreaObject(clsBIHArea[] p_objArea, IList p_ilUsableAreaID)
        {
            ArrayList ilRes = new ArrayList();
            if (p_objArea == null || p_objArea.Length <= 0) return ilRes;

            //ȫ���Ŀɷ��ʵĲ�������
            for (int i1 = 0; i1 < p_objArea.Length; i1++)
            {
                if (p_objArea[i1] == null) continue;
                if (p_ilUsableAreaID.Contains(p_objArea[i1].m_strAreaID.Trim()))
                {
                    if (!(ilRes.Contains(p_objArea[i1])))
                        ilRes.Add(p_objArea[i1]);
                }
            }
            return ilRes;
        }

        /// <summary>
        /// ���˳���Ȩ�޵�סԺ��
        /// </summary>
        /// <param name="p_objItemArr">��Ժ�ǼǶ���	[����]</param>
        /// <param name="p_ilUsableAreaID">��Ȩ���ʵĲ���ID����</param>
        /// <returns>��Ȩ���ʵ���Ժ�ǼǶ��󼯺�</returns>
        public ArrayList GetUsableRegisterObject(clsT_Opr_Bih_Register_VO[] p_objItemArr, IList p_ilUsableAreaID)
        {
            ArrayList ilRes = new ArrayList();
            if (p_objItemArr == null || p_objItemArr.Length <= 0) return ilRes;

            //ȫ���Ŀɷ��ʵĲ�������
            for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
            {
                if (p_objItemArr[i1] == null) continue;
                if (p_ilUsableAreaID.Contains(p_objItemArr[i1].m_strAREAID_CHR.Trim()))
                {
                    if (!(ilRes.Contains(p_objItemArr[i1])))
                        ilRes.Add(p_objItemArr[i1]);
                }
            }
            return ilRes;
        }
        #endregion

        //T_Opr_Bih_Order (ҽ����)
        #region ����
        /// <summary>
        /// ��ȡҽ����¼Vo		����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_objResult">ҽ����¼Vo	[out ����]</param>
        /// <returns></returns>
        public long m_lngGetOrderByID(string p_strOrderID, out clsBIHOrder p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByID(p_strOrderID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ	������ԺID
        /// </summary>
        /// <param name="registerid">��ԺID</param>
        /// <param name="dtbResult">DataTable </param>
        /// <returns></returns>
        public long m_lngGetPatientInfoByRegisterid(string registerid, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBedManageSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsBedManageSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBedManageSvc));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPatientInfoByRegisterid(registerid, out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ���߻�������
        public long m_lngGetPatientCareInfo(string p_strResgisterID, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBedManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsBedManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBedManageSvc));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPatientCareInfo(p_strResgisterID, out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ������	����ҽ��ID
        /// <summary>
        /// ��ȡ��Чҽ��	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <returns></returns>
        public long m_lngGetOrderByOrderID(string p_strOrderID, out clsBIHOrder p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByOrderID(p_strOrderID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region
        public long m_lngGetSpChargeItemIDType(out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBedManageSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsBedManageSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBedManageSvc));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSpChargeItemIDType(out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ��ϸ��Ϣ	glzhang	2005.10.13
        /// <summary>
        /// ��ȡҩƷ��ϸ��Ϣ	glzhang	2005.10.13
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="strText"></param>
        public void m_mthGetMedicineInfo(string ID, out string strText)
        {
            strText = "";
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            (new weCare.Proxy.ProxyOP()).Service.m_mthGetMedicineInfo(ID, out strText);
            //objSvc.Dispose();
            //objSvc = null;
        }
        #endregion



        #region ��������ҽ��
        /// <summary>
        /// ��������ҽ��(blnIsSameNO-trueͬ��,false ��ͬ��)
        /// </summary>
        internal long SaveTheGroup(out string[] strRecordIDArr, clsBIHOrder[] arrOrder, bool blnIsSameNO, bool isChildPrice)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            //    (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrder(out strRecordIDArr, arrOrder, blnIsSameNO, isChildPrice);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        #region ��������ҽ�����£�
        /// <summary>
        /// ��������ҽ��(blnIsSameNO-trueͬ��,false ��ͬ��)
        /// </summary>
        internal long m_lngAddNewOrderByGroup(out string[] p_strRecordIDArr, System.Collections.Generic.List<clsBIHOrder> p_RecordArr, bool isChildPrice)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            //    (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrderByGroup(out p_strRecordIDArr, p_RecordArr, isChildPrice);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }
        #endregion

        internal long m_lngGetOrderGroupByID(string m_strGroupID, out clsT_aid_bih_ordergroup_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService objSvc =
            //  (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderGroupByID(m_strGroupID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngGetOrderGroupDetailByGroupID(string m_strGroupID, out DataTable m_dtOrderGroupDetail)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService objSvc =
            // (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderGroupDetailByGroupID(m_strGroupID, out m_dtOrderGroupDetail, true);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngDellORDERCHARGEDEPT(string m_strSeq_int)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHORDERCHARGEDService objSvc =
            // (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHORDERCHARGEDService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHORDERCHARGEDService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDellORDERCHARGEDEPT(m_strSeq_int);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #region ���ܿ������������Ƿ������޸�ҽ��������Ŀ���� 1017
        /// <summary>
        /// ���ܿ������������Ƿ������޸�ҽ��������Ŀ���� 1017
        /// </summary>
        /// <param name="m_blOpen"></param>
        /// <returns></returns>
        public long m_lngGetBihOrderNameControl(out bool m_blOpen)
        {

            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetBihOrderNameControl(out m_blOpen);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        internal long m_lngFindSendArea(string m_strAreaID, out DataTable m_dtItem)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFindSendArea(m_strAreaID, out m_dtItem);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ����ǰҽ����ĳ��ҽ�����޸�
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns></returns>
        internal long m_lngModifyCurrentSubOrder(clsBIHOrder objOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyCurrentSubOrder(objOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ���ܿ������������Ƿ������޸�ҽ������ 1017
        /// </summary>
        /// <param name="m_blOpen"></param>
        /// <returns></returns>
        internal long m_lngGetm_cmdBlankOutControl(out bool m_blOpen)
        {
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetm_cmdBlankOutControl(out m_blOpen);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }


        #region ������ĿID�����������
        /// <summary>
        /// ������ĿID�����������
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_mthGetApplyTypeByID(string strID, out DataTable dt)
        {
            long ret = 0;
            //com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //    (com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            ret = (new weCare.Proxy.ProxyIP()).Service.m_mthGetApplyTypeByID(strID, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return ret;
        }
        #endregion

        internal long m_lngGetOrderDicChargeByCode(string strFindCode, int m_intClass, string m_strORDERCATEID_CHR, bool m_blLessMedControl, string p_strMedDeptId, out clsBIHOrderDic[] arrDic, out DataSet m_dsDicChargeSet, bool isChildPrice)
        {
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderDicChargeByCode(strFindCode, m_intClass, m_strORDERCATEID_CHR, m_blLessMedControl, p_strMedDeptId, out arrDic, out m_dsDicChargeSet, isChildPrice);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngGetRecipeFreq(string strFindCode, out clsAIDRecipeFreq[] arrFreq)
        {
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetRecipeFreq(strFindCode, out arrFreq);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngModifyOrder(clsBIHOrder objOrder, clsBIHOrder[] arrOrder, bool isChildPrice)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyOrder(objOrder, arrOrder, isChildPrice);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngGetChargeListByGroupDetail(clsBIHOrder order, out List<clsORDERCHARGEDEPT_VO> m_arrChargeList)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService objSvc =
            // (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetChargeListByGroupDetail(order, out m_arrChargeList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        public long m_lngGetFeelListbyOrderDic(List<string> m_arrOrderDic, out List<string> m_arrFeelList)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService objSvc =
            // (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetFeelListbyOrderDic(m_arrOrderDic, out m_arrFeelList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }


        #region ����ͳ�Ʒ���
        /// <summary>
        /// ��ǰͬ��ҽ�������ܼ�
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        internal decimal GetTheSameChargeSum(clsBIHCanExecOrder order, DataTable m_dtChargeList)
        {
            decimal m_decSum = 0;
            if (m_dtChargeList != null && m_dtChargeList.Rows.Count <= 0)
            {
                return m_decSum;
            }
            DataView myDataView = new DataView(m_dtChargeList);
            myDataView.RowFilter = "REGISTERID_CHR='" + order.m_strRegisterID + "' and RECIPENO_INT=" + order.m_intRecipenNo.ToString();
            if (myDataView.Count <= 0)
            {
                return m_decSum;
            }
            for (int i = 0; i < myDataView.Count; i++)
            {
                DataRowView row = myDataView[i];
                m_decSum += clsConverter.ToDecimal(row["UNITPRICE_DEC"].ToString()) * clsConverter.ToDecimal(row["AMOUNT_DEC"].ToString());
            }
            return m_decSum;
        }

        //��ǰҽ�����úϼ�
        internal decimal GettheChargeSum(clsBIHCanExecOrder order, DataTable m_dtChargeList)
        {
            decimal m_decSum = 0;
            if (m_dtChargeList != null && m_dtChargeList.Rows.Count <= 0)
            {
                return m_decSum;
            }
            DataView myDataView = new DataView(m_dtChargeList);
            myDataView.RowFilter = "orderid_chr='" + order.m_strOrderID + "'";
            myDataView.Sort = "FLAG_INT";
            if (myDataView.Count <= 0)
            {
                return m_decSum;
            }
            for (int i = 0; i < myDataView.Count; i++)
            {
                DataRowView row = myDataView[i];
                m_decSum += clsConverter.ToDecimal(row["UNITPRICE_DEC"].ToString()) * clsConverter.ToDecimal(row["AMOUNT_DEC"].ToString());
            }
            return m_decSum;
        }
        #endregion

        //ԭ��ʹ��ArrayList��ȷ�Ĵ���
        //internal long GetTheHisControl(ArrayList m_arrControl, out DataTable dtbResult)
        //{
        //    long lngRes = 0;
        //    com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
        //    lngRes = (new weCare.Proxy.ProxyIP()).Service.GetTheHisControl(m_arrControl, out  dtbResult);
        //    return lngRes;
        //}
        //����ʹ��List<T>�Ĵ���
        internal long GetTheHisControl(System.Collections.Generic.List<string> m_arrControl, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.GetTheHisControl(m_arrControl, out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngFindGroup(string m_strFindCode, string m_strEmpId, string m_strInpatientAreaID, int m_intClass, out clsBIHOrderGroup[] arrGroup)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService objSvc =
            // (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderGroupService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFindGroup(m_strFindCode, m_strEmpId, m_strInpatientAreaID, m_intClass, out arrGroup);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngGetOrderStopSignByRegisterId(string m_strRegisterID, out DataTable m_dtOrderSign)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            // (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderStopSignByRegisterId(m_strRegisterID, out m_dtOrderSign);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngGetOrderStopByRECIPENO_INT(List<string> m_arrRecipenNo, string m_strRegisterID, out clsBIHOrder[] arrOrder, out DataTable m_dtChargeList)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            // (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderStopByRECIPENO_INT(m_arrRecipenNo, m_strRegisterID, out arrOrder, out m_dtChargeList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngStopOrder(string[] p_strBlankOutOrderIDArr, string strDoctorID, string strDoctorName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngStopOrder(p_strBlankOutOrderIDArr, strDoctorID, strDoctorName);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngDeleteOrder(string[] p_strDeleteOrderIDArr, string[] p_strDeleteContinueIDArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrder(p_strDeleteOrderIDArr, p_strDeleteContinueIDArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngGetSignByEmpID(string m_strEmpID, ref byte[] m_objSign)
        {
            long lngRes = 0;
            //clsBIHOrderService objSvc = (clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSignByEmpID(m_strEmpID, ref m_objSign);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
    }
    #region ����������
    /// <summary>
    /// ����������	{������ʾ������Ϣ}	 
    /// </summary>
    public class clsChargeForDisplay
    {
        public clsChargeForDisplay()
        { }
        /// <summary>
        /// �շ���Ŀ���ڵ�ҽ��ID
        /// </summary>
        public string strOrderID = "";
        /// <summary>
        /// ��ˮ��--��Ӧ(סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��-T_OPR_BIH_ORDERCHARGEDEPT)
        /// </summary>
        public string m_strSeq_int = "";
        /// <summary>
        /// �շ���ĿID
        /// </summary>
        public string m_strChargeID = "";
        /// <summary>
        /// �շ���Ŀ����
        /// </summary>
        public string m_strITEMCODE_VCHR = "";
        /// <summary>
        /// �շ���Ŀ����
        /// </summary>
        public string m_strName = "";
        /// <summary>
        /// �շ���� 0-����Ŀ 1-������Ŀ 2-�÷����� 3-����¼����Ŀ
        /// </summary>
        public int m_intType = 1;
        /// <summary>
        /// ����
        /// </summary>
        public double m_dblPrice = 0;
        /// <summary>
        /// ����
        /// </summary>
        public double m_dblDrawAmount = 0;
        /// <summary>
        /// �ϼƽ��
        /// </summary>
        public double m_dblMoney = 0;
        /// <summary>
        /// �������� 
        /// </summary>
        public double m_dblTradePrice = 0;
        /// <summary>
        /// �ϼ�ҩƷ������� 
        /// </summary>
        public double m_dblDiffCostMoney = 0;
        /// <summary>
        /// �������� �������� {0=������;1=�״���}
        /// </summary>
        public int m_intCONTINUEUSETYPE_INT = -1;
        /// <summary>
        /// �Ƿ�������ҽ��	{0=��1=��} ������ҽ������ʾҩƷ������Ϣ��
        /// </summary>
        public int m_intIsContinueOrder = 0;
        /// <summary>
        /// ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
        /// </summary>
        public string m_strNoqtyFLag = "";
        /// <summary>
        /// �Ƿ�ҽ����0/1)
        /// </summary>
        public int m_strIsYB = 0;
        /// <summary>
        /// ҽ������
        /// </summary>
        public string m_strYBClass = "";
        /// <summary>
        /// ִ�����ID
        /// </summary>
        public string m_strOrdercateid_chr = "";
        /// <summary>
        /// ִ�п���ID
        /// </summary>
        public string m_strClacarea_chr = "";
        /// <summary>
        /// ִ�п�������
        /// </summary>
        public string m_strClacareaName_chr = "";
        /// <summary>
        /// סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// </summary>
        public clsORDERCHARGEDEPT_VO m_objORDERCHARGEDEPT_VO;
        /// <summary>
        /// ��Ŀ��Դ��������������Դ����ȷ��ֵ��Χ���ڲ�ʹ�á�����1��ҩƷ��2�����ϱ�ȡ�
        /// </summary>
        public int m_intITEMSRCTYPE_INT;
        /// <summary>
        /// ����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
        /// </summary>
        public int m_intIPNOQTYFLAG_INT;
        /// <summary>
        /// ���{=this.get������Ŀ.get�շ���Ŀ.���}
        /// </summary>
        public string m_strSPEC_VCHR = "";
        /// <summary>
        /// ������λ
        /// </summary>
        public string m_strUNIT_VCHR = "";
        /* <<======================================= */
        /// <summary>
        /// ͣ�ñ�־ 1-ͣ�� 0-����
        /// </summary>
        public int m_intIFSTOP_INT = 0;

    }
    #endregion
}
