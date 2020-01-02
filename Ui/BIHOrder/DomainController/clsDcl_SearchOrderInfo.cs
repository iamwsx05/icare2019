using System;
using System.Data;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ��ѯҽ��	�߼����Ʋ�
    /// ���ߣ� ����
    /// ����ʱ�䣺 2005-02-22
    /// </summary>>
    public class clsDcl_SearchOrderInfo : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ���캯��
        public clsDcl_SearchOrderInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        //������Ϣ��
        #region ��ѯסԺ��
        /// <summary>
        /// ��ѯסԺ��	[����]
        /// </summary>
        /// <param name="p_strFindString">��ѯ�ַ���</param>
        /// <param name="p_strHospitalNoArr">סԺ��	[����]	[out����]</param>
        /// <returns></returns>
        public long m_lngFindHospitalNo(string p_strFindString, out string[] p_strHospitalNoArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFindHospitalNo(p_strFindString, out p_strHospitalNoArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ����סԺ��	[����]	
        /// ����(1=���ϴ�;2=Ԥ��Ժ;4=���)
        /// </summary>
        /// <param name="p_strCode">��ѯ�ַ���</param>
        /// <param name="p_objResultArr">��Ժ�ǼǶ��� [out ����]</param>
        /// <returns></returns>
        public long m_lngFindHospitalNo(string p_strFindString, out clsT_Opr_Bih_Register_VO[] p_strResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFindHospitalNo(p_strFindString, out p_strResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ����������
        /// <summary>
        /// ��ѯ����	[����]
        /// </summary>
        /// <param name="p_strFindString">��ѯ�ַ���</param>
        /// <param name="p_objResultArr">��������	[����]	[out����]</param>
        /// <returns></returns>
        public long m_lngFindArea(string p_strFindString, out clsBIHArea[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(p_strFindString, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��ѯ����	���ݲ���ID	[����]
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strFindString">��ѯ�ַ���</param>
        /// <param name="p_objResultArr">��������	[����]	[out����]</param>
        /// <returns></returns>
        public long m_lngGetBedByArea(string p_strAreaID, string p_strFindString, out weCare.Core.Entity.clsBIHBed[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetBedByArea(p_strAreaID, p_strFindString, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ���
        /// <summary>
        /// ��ȡ���	����סԺID
        /// </summary>
        /// <param name="p_strRegisterID">סԺID</param>
        /// <param name="p_dblBalanceMoney">���</param>
        /// <returns></returns>
        public long m_lngGetBalanceMoneyByRegisterID(string p_strRegisterID, out double p_dblBalanceMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc));
            lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetBalanceMoneyByRegisterID(p_strRegisterID, out p_dblBalanceMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ���˶���
        /// <summary>
        /// ��ȡ���˶���	����סԺ��
        /// </summary>
        /// <param name="p_strInHospitalNo">סԺ��</param>
        /// <param name="p_objResult">���˶���</param>
        /// <returns></returns>
        public long m_lngGetPatientByInHospitalNo(string p_strInHospitalNo, out clsBIHPatientInfo p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPatientByInHospitalNo(p_strInHospitalNo, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��ȡ���˶���	���ݲ���ID������ID
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedID">����ID</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetPatientByAreaBed(string p_strAreaID, string p_strBedID, out clsBIHPatientInfo p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPatientByAreaBed(p_strAreaID, p_strBedID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ȡ�ò���ҽ����Ӧ����
        public long m_lngGetYBPayTypeName(string strHisPayTypeId, out EntityYBMappingPayType retVal)
        {
            long lngRes = 0;
            retVal = null;
            //com.digitalwave.iCare.middletier.HIS.HISYBMapping.clsYBMappingPayTypeSVC objSvc =
            //	(com.digitalwave.iCare.middletier.HIS.HISYBMapping.clsYBMappingPayTypeSVC) 
            //		com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(
            //			typeof(com.digitalwave.iCare.middletier.HIS.HISYBMapping.clsYBMappingPayTypeSVC)); 
            return lngRes;
        }
        #endregion
        //ͳ����
        #region ��ѯҽ����Ϣ	��������
        /// <summary>
        /// ��ѯҽ����Ϣ	��������
        /// </summary>
        /// <param name="p_strCondition">�������ʽ[��������Where��]</param>
        /// <param name="p_objResultArr">ҽ����¼����	[����]</param>
        /// <returns></returns>
        public long m_lngGetOrderByCondition(string p_strCondition, out clsBIHOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByCondition(p_strCondition, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ��ִ�е�	����ҽ��ID
        /// <summary>
        /// ��ȡҽ��ִ�е�	����ҽ��ID	[����]
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">ҽ��ִ�е�����</param>
        /// <returns></returns>
        public long m_lngGetExecuteOrderByOrderID(string[] p_strOrderIDArr, out clsBIHExecOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetExecuteOrderByOrderID(p_strOrderIDArr, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ��ҩ��ϸ��	����ҽ��ID
        /// <summary>
        /// ���Ұ�ҩ��ϸ��	ҽ��ID	[����]
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">ҽ����ҩ��ϸ����</param>
        /// <returns></returns>
        public long m_lngGetPutMedDetailByOrderID(string[] p_strOrderIDArr, out clsT_Bih_Opr_Putmeddetail_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsPutMedicineSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsPutMedicineSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsPutMedicineSvc));
            lngRes = (new weCare.Proxy.ProxyMedStore01()).Service.m_lngGetPutMedDetailByOrderID(p_strOrderIDArr, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region  ��ȡ����������ϸ	����ҽ��ID
        /// <summary>
        /// ��ȡ����������ϸ	����ҽ��ID[����]
        /// </summary>
        /// <param name="p_strRegisterID">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">����������ϸ����</param>
        /// <returns></returns>
        public long m_lngGetPatientChargeByOrderID(string[] p_strOrderIDArr, out clsT_opr_bih_patientcharge_VO[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc));
            lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetPatientChargeByOrderID(p_strOrderIDArr, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ���ӵ������ݶ���	����ҽ��ID
        /// <summary>
        /// ��ȡ���ӵ������ݶ���	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">���ӵ������ݶ���</param>
        /// <returns></returns>
        public long m_lngGetAttachOrderByOrderID(string[] p_strOrderIDArr, out clsOrderAttach[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetAttachOrderByOrderID(p_strOrderIDArr, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        /// <summary>
        /// ����סԺ�ǼǺ�,������ʷסԺ��¼
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public DataTable getBihHistory(string registerId)
        {
            DataTable dt = null;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            dt = (new weCare.Proxy.ProxyIP()).Service.getBihHistory(registerId);
            //objSvc.Dispose();
            //objSvc = null;
            return dt;
        }
    }
}
