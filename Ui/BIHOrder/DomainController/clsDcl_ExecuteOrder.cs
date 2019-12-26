using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using System.Collections.Generic;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using System.Collections;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ҽ��ִ��	�߼����Ʋ� 
    /// </summary>
    public class clsDcl_ExecuteOrder : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ���캯��
        public clsDcl_ExecuteOrder()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        //ִ��ҽ��
        #region ��ȡҽ��-ȫ��
        /// <summary>
        /// ��ȡҽ�� [����=������1:����ִ��   2:Ƶ�ʲ���ִ��   3:��ִ��	4:��ֹͣ��]	
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����ID [out ���� ����]</param>
        /// <returns></returns>
        public long m_lngGetCanExecuteOrder(string p_strAreaID, string p_strBedIDs, DateTime dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCanExecuteOrder(p_strAreaID, p_strBedIDs, dtExecuteDate, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��ȡҽ�� [����=������1:����ִ��   2:Ƶ�ʲ���ִ��   3:��ִ��	4:��ֹͣ��]	
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_strOrderType">�ö��ŷָ���ҽ������	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_strOrderStatus">�ö��ŷָ���ִ��״̬	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_strOrderCate">�ö��ŷָ���������Ŀ����	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_blnNeedFeel">����Ƥ��</param>
        /// <param name="p_blnTakeMedicine">��Ժ��ҩ</param>
        /// <param name="p_blnOnlyToday">���Ե���	{�ύʱ��}</param>
        /// <param name="strCreatorID">������ID</param>
        /// <param name="dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����ID [out ���� ����]</param>
        /// <returns></returns>
        public long m_lngGetCanExecuteOrder(string p_strAreaID, string p_strBedIDs, string p_strOrderType, string p_strOrderStatus, string p_strOrderCate, bool p_blnNeedFeel, bool p_blnTakeMedicine, bool p_blnOnlyToday, string strCreatorID, DateTime dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCanExecuteOrder(p_strAreaID, p_strBedIDs, p_strOrderType, p_strOrderStatus, p_strOrderCate, p_blnNeedFeel, p_blnOnlyToday, p_blnTakeMedicine, strCreatorID, dtExecuteDate, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ��-ִ��
        /// <summary>
        /// ��ȡҽ�� [����=������1:����ִ�С�]
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����ID [out ���� ����]</param>
        /// <returns></returns>
        public long m_lngGetCanExecuteOrderOnlyCan(string p_strAreaID, string p_strBedIDs, DateTime dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCanExecuteOrderOnlyCan(p_strAreaID, p_strBedIDs, dtExecuteDate, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��ȡҽ�� [����=������1:����ִ�С�]
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_strOrderType">�ö��ŷָ���ҽ������	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_strOrderStatus">�ö��ŷָ���ִ��״̬	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_strOrderCate">�ö��ŷָ���������Ŀ����	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_blnNeedFeel">����Ƥ��</param>
        /// <param name="p_blnTakeMedicine">��Ժ��ҩ</param>
        /// <param name="p_blnOnlyToday">���Ե���	{�ύʱ��}</param>
        /// <param name="strCreatorID">������ID</param>
        /// <param name="dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����ID [out ���� ����]</param>
        /// <returns></returns>
        public long m_lngGetCanExecuteOrderOnlyCan(string p_strAreaID, string p_strBedIDs, string p_strOrderType, string p_strOrderStatus, string p_strOrderCate, bool p_blnNeedFeel, bool p_blnTakeMedicine, bool p_blnOnlyToday, string strCreatorID, DateTime dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCanExecuteOrderOnlyCan(p_strAreaID, p_strBedIDs, p_strOrderType, p_strOrderStatus, p_strOrderCate, p_blnNeedFeel, p_blnOnlyToday, p_blnTakeMedicine, strCreatorID, dtExecuteDate, out p_objResultArr);

            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ��-����ύ
        /// <summary>
        /// ��ȡҽ��-����ύ	{ִ��״̬=1-�ύ;}
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����Vo[out ���� ����]</param>
        /// <returns></returns>
        public long m_lngGetOrderForAuditingExecute(string p_strAreaID, string p_strBedIDs, DateTime p_dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderForAuditingExecute(p_strAreaID, p_strBedIDs, p_dtExecuteDate, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ��-����ύ(�жϵ�ǰ�����Ƿ�������ύ��ҽ��)
        /// <summary>
        /// ��ȡҽ��-����ύ	{ִ��״̬=1-�ύ;}
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����Vo[out ���� ����]</param>
        /// <returns></returns>
        public long m_lngGetOrderForAuditingExecute(string p_strAreaID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));

            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderForAuditingExecute(p_strAreaID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ��-���ֹͣ
        /// <summary>
        /// ��ȡҽ��-���ֹͣ	{ִ��״̬=3-ֹͣ;}
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����Vo[out ���� ����]</param>
        /// <returns></returns>
        public long m_lngGetOrderForAuditingStop(string p_strAreaID, string p_strBedIDs, DateTime p_dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderForAuditingStop(p_strAreaID, p_strBedIDs, p_dtExecuteDate, out p_objResultArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ִ   �С�����ύ�����ֹͣ
        /// <summary>
        /// ִ��ҽ��	[����ҽ��]
        /// </summary>
        /// <param name="strOrderID">ҽ��ID</param>
        /// <param name="strOrderExecID">ִ�е���¼ID [out ������������ض��ID���ж��š������ָΪ��ִ��ʧ�ܡ�]</param>
        /// <param name="blnIsRecruit">ָ���Ƿ񲹴�(��ִ������)</param>
        /// <param name="strEmpID">ִ��ҽ����ˮ��</param>
        /// <param name="strEmpName">ִ��ҽ������</param>
        /// <param name="dtExecDate">ִ������</param>
        /// <returns></returns>
        /// <remarks>
        ///		1����ΪCom+�������ִ��ҽ��ʧ�ܣ��򱨴�ҽ��ִ�д��󣡡������ع���
        ///		2������������ע��Ҫ���쳣����
        ///	</remarks>
        public long m_lngExecuteOrder(string strOrderID, out string strOrderExecID, bool blnIsRecruit, string strEmpID, string strEmpName, DateTime dtExecDate)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngExecuteOrder(strOrderID, out strOrderExecID, blnIsRecruit, strEmpID, strEmpName, dtExecDate);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        ///  ִ��ҽ��	[����ҽ��]	����
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <param name="p_strOrderExecIDArr">ִ�е���¼ID [����] [out ������������ض��ID���ж��š������ָΪ��ִ��ʧ�ܡ�]</param>
        /// <param name="p_blnIsRecruitArr">ָ���Ƿ񲹴�(��ִ������) [����] </param>
        /// <param name="p_strEmpID">ִ��ҽ����ˮ�� [����] </param>
        /// <param name="p_strEmpName">ִ��ҽ������ [����] </param>
        /// <param name="p_dtExecDate">ִ������ [����] </param>
        /// <param name="p_strParentIDArr">����ҽ��ID [����] </param>
        /// <returns>����{-1=������ִ�в�������0=ִ�г�����1=�ɹ���}</returns>
        public long m_lngExecuteOrder(string[] p_strOrderIDArr, out string[] p_strOrderExecIDArr, bool[] p_blnIsRecruitArr, string p_strEmpID, string p_strEmpName, DateTime p_dtExecDate, string[] p_strParentIDArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngExecuteOrder(p_strOrderIDArr, out p_strOrderExecIDArr, p_blnIsRecruitArr, p_strEmpID, p_strEmpName, p_dtExecDate, p_strParentIDArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        ///  ִ��ҽ��	[����ҽ��]	����
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <param name="p_strRegisterIDArr">��Ժ�Ǽ�ID [����]</param>
        /// <param name="p_intRecipenNoArr">���� [����]</param>
        /// <param name="p_strOrderExecIDArr">ִ�е���¼ID [����] [out ������������ض��ID���ж��š������ָΪ��ִ��ʧ�ܡ�]</param>
        /// <param name="p_blnIsRecruitArr">ָ���Ƿ񲹴�(��ִ������) [����] </param>
        /// <param name="p_strEmpID">ִ��ҽ����ˮ�� [����] </param>
        /// <param name="p_strEmpName">ִ��ҽ������ [����] </param>
        /// <param name="p_dtExecDate">ִ������ [����] </param>
        /// <param name="p_strParentIDArr">����ҽ��ID [����] </param>
        /// <returns>����{-1=������ִ�в�������0=ִ�г�����1=�ɹ���}</returns>
        public long m_lngExecuteOrder(string[] p_strOrderIDArr, string[] p_strRegisterIDArr, int[] p_intRecipenNoArr, out string[] p_strOrderExecIDArr, bool[] p_blnIsRecruitArr, string p_strEmpID, string p_strEmpName, DateTime p_dtExecDate, string[] p_strParentIDArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngExecuteOrder(p_strOrderIDArr, p_strRegisterIDArr, p_intRecipenNoArr, out p_strOrderExecIDArr, p_blnIsRecruitArr, p_strEmpID, p_strEmpName, p_dtExecDate, p_strParentIDArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ����ύ
        /// </summary>
        /// <param name="p_strOrderIDArr">[����]	ҽ��ID</param>
        /// <param name="p_strHandlersID">������ID</param>
        /// <param name="p_strHandlers">����������</param>
        /// <returns></returns>
        public long m_lngAuditingForExecute(string[] p_strOrderIDArr, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAuditingForExecute(p_strOrderIDArr, p_strHandlersID, p_strHandlers);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// �˻�ҽ��	
        /// ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}
        /// ҵ��˵����	ֻ���״̬��2-ִ�С�5-������ύ
        /// </summary>
        /// <param name="p_strReturnOrderID">�˻�ҽ��ID</param>
        /// <param name="p_strReturnReason">ԭ��</param>
        /// <param name="strDoctorID">������ID</param>
        /// <param name="strDoctorName">����������</param>
        /// <returns></returns>
        public long m_lngReturnOrder(string p_strReturnOrderID, string p_strReturnReason, string strDoctorID, string strDoctorName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngReturnOrder(p_strReturnOrderID, p_strReturnReason, strDoctorID, strDoctorName);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// �˻�ҽ��	
        /// ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}
        /// ҵ��˵����	ֻ���״̬��2-ִ�С�5-������ύ
        /// </summary>
        /// <param name="p_strReturnOrderID">�˻�ҽ��ID</param>
        /// <param name="p_strReturnReason">ԭ��</param>
        /// <param name="strDoctorID">������ID</param>
        /// <param name="strDoctorName">����������</param>
        /// <param name="p_blnInfectSon">�Ƿ��ݹ��˻��Ӽ�ҽ��</param>
        /// <returns></returns>
        public long m_lngReturnOrder(string p_strReturnOrderID, string p_strReturnReason, string strDoctorID, string strDoctorName, bool p_blnInfectSon, bool isChildPrice)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngReturnOrder(p_strReturnOrderID, p_strReturnReason, strDoctorID, strDoctorName, p_blnInfectSon, isChildPrice);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ���ֹͣ
        /// </summary>
        /// <param name="p_strOrderIDArr">[����]	ҽ��ID</param>
        /// <param name="p_strHandlersID">������ID</param>
        /// <param name="p_strHandlers">����������</param>
        /// <returns></returns>
        public long m_lngAuditingForStop(string[] p_strOrderIDArr, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAuditingForStop(p_strOrderIDArr, p_strHandlersID, p_strHandlers);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch { }
            return lngRes;
        }
        #endregion
        #region �ж��Ƿ����ִ��ҽ��
        /// <summary>
        /// �ж��Ƿ����ִ��ҽ��
        /// ҵ��˵����	�Ӽ�ҽ�����ܵ���ִ�У�������丸��ҽ��һ��ִ��
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <param name="p_strParentIDArr">����ҽ��ID [����]</param>
        /// <param name="p_blnIsCanExecute">�Ƿ����ִ��ҽ����[out ����]</param>
        /// <returns></returns>
        public long m_lngCheckIsCanExecute(string[] p_strOrderIDArr, string[] p_strParentIDArr, out bool p_blnIsCanExecute)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngCheckIsCanExecute(p_strOrderIDArr, p_strParentIDArr, out p_blnIsCanExecute);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ��	[����-�ύ��]
        /// <summary>
        /// ����ҽ��ID	[����]
        /// ����: ��֤ͬ���ŵ�ҽ��һ�����	{�ύҽ����ֹͣҽ��������ҽ��������ҽ��}
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <param name="p_intStatus">ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;}</param>
        /// <returns></returns>
        public string[] GetOrderIDSameRecipeNOForCommit(string[] p_strOrderIDArr, int p_intStatus)
        {
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            return (new weCare.Proxy.ProxyIP()).Service.GetOrderIDSameRecipeNOForCommit(p_strOrderIDArr, p_intStatus);
        }
        #endregion
        #region ��ȡҽ��	[����-ִ����]
        /// <summary>
        /// ����ҽ��ID	����
        /// ����: �Ա�ȷ��ͬ���ŵ�ҽ��ִ��һ��ִ�У�
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <param name="p_intStatus">ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;}</param>
        /// <returns></returns>
        public string[] GetOrderIDSameRecipeNOForExecute(string[] p_strOrderIDArr)
        {
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            return (new weCare.Proxy.ProxyIP()).Service.GetOrderIDSameRecipeNOForExecute(p_strOrderIDArr);
        }
        #endregion

        //T_Opr_Bih_OrderFeel(ҽ��Ƥ�Խ��)
        #region ����
        /// <summary>
        /// ����Ƥ�Խ��	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult">ҽ��Ƥ�Խ��Vo����	[out ����]</param>
        /// <returns></returns>
        public long m_lngGetOrderFeelByID(string p_strID, out clsT_Opr_Bih_OrderFeel_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderFeelByID(p_strID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ����Ƥ�Խ��	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_objResult">ҽ��Ƥ�Խ��Vo����	[out ����]</param>
        /// <returns></returns>
        public long m_lngGetOrderFeelByOrderID(string p_strOrderID, out clsT_Opr_Bih_OrderFeel_VO p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderFeelByOrderID(p_strOrderID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ����
        /// <summary>
        /// ����Ƥ�Խ��
        /// </summary>
        /// <param name="p_strRecordID">��ˮ��	[out ����]</param>
        /// <param name="p_objRecord">ҽ��Ƥ�Խ��Vo����</param>
        /// <returns></returns>
        public long m_lngAddNewOrderFeel(out string p_strRecordID, clsT_Opr_Bih_OrderFeel_VO p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrderFeel(out p_strRecordID, p_objRecord);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region �޸�
        /// <summary>
        /// �޸�Ƥ�Խ��
        /// </summary>
        /// <param name="p_strOrderFeelID">��ˮ��</param>
        /// <param name="p_objRecord">ҽ��Ƥ�Խ��Vo����</param>
        /// <returns></returns>
        public long m_lngModifyOrderFeel(clsT_Opr_Bih_OrderFeel_VO p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyOrderFeel(p_objRecord);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        //���ӵ���Ӱ��
        #region ���Ӹ��ӵ���Ӱ��
        /// <summary>
        /// ���Ӹ��ӵ���Ӱ��
        /// </summary>
        /// <param name="strOrderID">ҽ��ID</param>
        /// <param name="strAttachID">ҽ�����ӵ���ID</param>
        /// <returns></returns>
        public long m_lngAddAttachOrder(string strOrderID, string strAttachID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddAttachOrder(strOrderID, strAttachID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #endregion
        #region ɾ�����ӵ���Ӱ��
        /// <summary>
        /// ɾ�����ӵ���Ӱ��
        /// </summary>
        /// <param name="strID">���ӵ���Ӱ��ID</param>
        /// <returns></returns>
        public long m_lngDeleteAttachOrder(string strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteAttachOrder(strID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #endregion
        #region ��ȡ���˻�����Ϣ
        /// <summary>
        /// ��ȡ���˻�����Ϣ	���ݲ���ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngGetPatientInfoByPatientID(string p_strPatientID, out clsPatient_VO p_objResult)
        {
            p_objResult = null;
            long lngRes = 0;
            //string strSQL = @"select a.* FROM t_bse_patient a where Trim(a.PATIENTID_CHR) = '" + p_strPatientID.Trim() + "'";
            //try
            //{
            //    DataTable dtbResult = new DataTable();
            //    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            //    objHRPSvc.Dispose();
            //    if (lngRes > 0 && dtbResult.Rows.Count > 0)
            //    {
            //        p_objResult = new clsPatient_VO();
            //        p_objResult.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
            //        p_objResult.m_strNAME_VCHR = dtbResult.Rows[0]["NAME_VCHR"].ToString().Trim();
            //        p_objResult.m_strSEX_CHR = dtbResult.Rows[0]["SEX_CHR"].ToString().Trim();
            //        p_objResult.m_strINPATIENTID_CHR = dtbResult.Rows[0]["INPATIENTID_CHR"].ToString().Trim();
            //        p_objResult.m_strIDCARD_CHR = dtbResult.Rows[0]["IDCARD_CHR"].ToString().Trim();
            //    }

            //}
            //catch (Exception objEx)
            //{
            //    string strTmp = objEx.Message;
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);
            //}
            return lngRes;
        }
        #endregion
        #region ��ȡҽ�����ͣɣ�
        /// <summary>
        /// ��ȡҽ�����ͣɣ�	����ҽ���ɣ�
        /// </summary>
        /// <param name="p_strOrderID">ҽ���ɣ�</param>
        /// <returns>ҽ�����ͣɣ�</returns>
        public string m_strGetOrderCateIDByOrderID(string p_strOrderID)
        {
            string strOrderCateID = "";
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderCateIDByOrderID(p_strOrderID, out strOrderCateID);
            //objSvc.Dispose();
            //objSvc = null;
            return strOrderCateID;
        }
        #endregion

        //T_Opr_Bih_OrderAttach_Transfer(ҽ�����ӵ���-ת��)
        #region ����
        /// <summary>
        /// ��ȡҽ�����ӵ���-ת��	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetOrderAttachTransferByID(string p_strID, out clsT_Opr_Bih_OrderAttach_Transfer_Vo p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderAttachTransferByID(p_strID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ����
        /// <summary>
        /// ����ҽ�����ӵ���-ת��
        /// </summary>
        /// <param name="p_strRecordID">��ˮ�� [out ����]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngAddNewOrderAttachTransfer(out string p_strRecordID, clsT_Opr_Bih_OrderAttach_Transfer_Vo p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrderAttachTransfer(out p_strRecordID, p_objRecord);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region �޸�
        /// <summary>
        /// �޸�ҽ�����ӵ���-ת��
        /// </summary>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngModifyOrderAttachTransfer(clsT_Opr_Bih_OrderAttach_Transfer_Vo p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyOrderAttachTransfer(p_objRecord);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// �޸�ת�����ӵ���״̬	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_intStatus">״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}</param>
        /// <returns></returns>
        public long m_lngChangeOrderAttachTransferState(string p_strID, int p_intStatus)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngChangeOrderAttachTransferState(p_strID, p_intStatus);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��Чת�����ӵ���״̬	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strActiveEmpID">��Ч��ID</param>
        /// <returns></returns>
        public long m_lngBecomeEffectiveOrderAttachTransfer(string p_strID, string p_strActiveEmpID, clsT_Opr_Bih_Transfer_VO p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngBecomeEffectiveOrderAttachTransfer(p_strID, p_strActiveEmpID, p_objRecord);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }
        #endregion
        #region ɾ��
        /// <summary>
        /// ɾ��ҽ�����ӵ���-ת��
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <returns></returns>
        public long m_lngDeleteOrderAttachTransfer(string p_strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrderAttachTransfer(p_strID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        //T_Opr_Bih_OrderAttach_Leave(ҽ�����ӵ���-��Ժ)
        #region ����
        /// <summary>
        /// ��ȡ��Ժ���ӵ���	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        public long m_lngGetOrderAttachLeaveByID(string p_strID, out clsT_Opr_Bih_OrderAttach_Leave_Vo p_objResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderAttachLeaveByID(p_strID, out p_objResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ����
        /// <summary>
        /// ���ӳ�Ժ���ӵ���
        /// </summary>
        /// <param name="p_strRecordID">��ˮ��	[out ����]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngAddNewOrderAttachLeave(out string p_strRecordID, clsT_Opr_Bih_OrderAttach_Leave_Vo p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrderAttachLeave(out p_strRecordID, p_objRecord);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region �޸�
        /// <summary>
        /// �޸ĳ�Ժ���ӵ���
        /// </summary>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        public long m_lngModifyOrderAttachLeave(clsT_Opr_Bih_OrderAttach_Leave_Vo p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyOrderAttachLeave(p_objRecord);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// �޸ĳ�Ժ���ӵ���״̬	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_intStatus">״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}</param>
        /// <returns></returns>
        public long m_lngChangeOrderAttachLeaveState(string p_strID, int p_intStatus)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngChangeOrderAttachLeaveState(p_strID, p_intStatus);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��Чת�����ӵ���״̬	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strActiveEmpID">��Ч��ID</param>
        /// <returns></returns>
        public long m_lngBecomeEffectiveOrderAttachLeave(string p_strID, string p_strActiveEmpID, clsT_Opr_Bih_Leave_VO p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngBecomeEffectiveOrderAttachLeave(p_strID, p_strActiveEmpID, p_objRecord);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }
        #endregion
        #region ɾ��
        /// <summary>
        /// ɾ����Ժ���ӵ���	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <returns></returns>
        public long m_lngDeleteOrderAttachLeave(string p_strID)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrderAttachLeave(p_strID);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        //��ȡ���˷�����Ϣ
        #region ��ȡ�ۼƷ���
        /// <summary>
        /// ��ȡ�ۼƷ���	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <returns></returns>
        public double m_dblGetSumMoneyByRegisterID(string p_strRegisterID)
        {
            long lngRes = 0;
            double dblSumMoney = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc));
            lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetSumMoneyByRegisterID(p_strRegisterID, out dblSumMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return dblSumMoney;
        }
        #endregion
        #region ��ȡ�������
        /// <summary>
        /// ��ȡ�������	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <returns></returns>
        public double m_dblGetBalanceMoneyByRegisterID(string p_strRegisterID)
        {
            long lngRes = 0;
            double dblBalanceMoney = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc));
            lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetBalanceMoneyByRegisterID(p_strRegisterID, out dblBalanceMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return dblBalanceMoney;
        }
        /// <summary>
        /// ��ȡ��������	������Ժ�Ǽ���ˮ��
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <returns></returns>
        public double m_dblGetLowerLimitMoneyByRegisterID(string p_strRegisterID)
        {
            long lngRes = 0;
            double dblLowerLimitMoney = 0;
            //com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsBIHChargeSvc));
            lngRes = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetLowerLimitMoneyByRegisterID(p_strRegisterID, out dblLowerLimitMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return dblLowerLimitMoney;
        }
        #endregion

        //������ҽ������	
        #region ������ҽ������
        /// <summary>
        /// ������ҽ������
        /// ҵ��˵��: 
        ///		1������ʱ���Ѿ�ִ�й���
        ///		2������ʱ�̻�û��ֹͣ��
        ///		3�����ȫ�����˵�ȫ��������ҽ����
        ///		4�������ǹ���ʱ�̵ĵ���ķ��ã�
        ///		5����һ������
        /// </summary>
        /// <param name="p_strHandlers">����������</param>
        /// <param name="p_strHandlersID">������ID</param>
        /// <param name="p_dtAuToExecDataTime">����ʱ��</param>
        /// <returns>{��=ʧ��;������=�ɹ�{999��û����Ҫ���ѵ�����ҽ�����Ѿ���������}}</returns>
        public long m_lngAuToCumulateMoneyForContinuousOrder(string p_strHandlers, string p_strHandlersID, DateTime p_dtAuToExecDataTime)
        {
            return 0;
            //string textsucceed = "	������ҽ���ֹ����ʳɹ�	����ʱ��{" + p_dtAuToExecDataTime.ToString("yyyy-MM-dd HH:mm:ss") + "}";
            //string strNoRecord = "	û��Ҫ���ѵ�������ҽ��	����ʱ��{" + p_dtAuToExecDataTime.ToString("yyyy-MM-dd HH:mm:ss") + "}";
            //string textfailure = "	������ҽ���ֹ�����ʧ��	����ʱ��{" + p_dtAuToExecDataTime.ToString("yyyy-MM-dd HH:mm:ss") + "}";
            //long lngRes = 0;
            //digitalwaveHisAutoCharge.digitalwaveHisAutoCharge objSvc = (digitalwaveHisAutoCharge.digitalwaveHisAutoCharge)clsObjectGenerator.objCreatorObjectByType(typeof(digitalwaveHisAutoCharge.digitalwaveHisAutoCharge));
            //try
            //{
            //    lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAuToCumulateMoneyForContinuousOrder( p_strHandlers, p_strHandlersID, p_dtAuToExecDataTime);
            //    //objSvc.Dispose();
            //    //objSvc = null;
            //}
            //catch
            //{
            //    try
            //    {
            //        new digitalwaveHisAutoCharge.AppLog().WriteFile(textfailure);
            //    }
            //    catch { }
            //    return lngRes;
            //}
            //try
            //{
            //    if (lngRes == 999)
            //        new digitalwaveHisAutoCharge.AppLog().WriteFile(strNoRecord);
            //    else
            //        new digitalwaveHisAutoCharge.AppLog().WriteFile(textsucceed);
            //}
            //catch { }
            //return lngRes;
        }

        #endregion

        //����
        #region �޸�ҽ�����ӵ���-ת����״̬
        /// <summary>
        /// �ύҽ�����ӵ���-��Ժ	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_intPStatus">״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}</param>
        /// <returns></returns>
        public long m_lngCommitAttachOrder_Transfer(string p_strOrderID, int p_intStatus)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngCommitAttachOrder_Transfer(p_strOrderID, p_intStatus);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion
        #region �޸�ҽ�����ӵ���-��Ժ��״̬
        /// <summary>
        /// �ύҽ�����ӵ���-��Ժ	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_intPStatus">״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}</param>
        /// <returns></returns>
        public long m_lngCommitAttachOrder_Leave(string p_strOrderID, int p_intPStatus)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngCommitAttachOrder_Leave(p_strOrderID, p_intPStatus);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion

        //�ۺ�
        #region	��ȡҽ����Ϣ��������Ϣ��סԺ��Ϣ	����ҽ��ID
        /// <summary>
        /// ��ȡҽ����Ϣ��������Ϣ��סԺ��Ϣ	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_dtResult">DataTable	[out ����]</param>
        /// <returns></returns>
        public long lngGetOrderPatientBIHInfo(string p_strOrderID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.lngGetOrderPatientBIHInfo(p_strOrderID, out p_dtResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡ��ǰʱ�����ִ�е�ҽ��
        /// <summary>
        /// ��ȡ��ǰʱ�����ִ�е�ҽ��	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_strOrderIDArr">��ִ��ҽ��ID [out ���� ����]</param>
        /// <returns></returns>
        public long m_lngGetCanExecuteOrderByOrderID(string p_strOrderID, out string[] p_strOrderIDArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCanExecuteOrderByOrderID(p_strOrderID, out p_strOrderIDArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #region ��ȡҽ������-��Ժ
        /// <summary>
        /// ��ȡҽ������-��Ժ
        /// </summary>
        /// <returns>����ҽ������-��Ժ</returns>
        public string m_strGetORDERCATEID_LEAVE_CHR()
        {
            DataTable dtbResult = new DataTable();
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSpChargeItemIDType(out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                return dtbResult.Rows[0]["ORDERCATEID_LEAVE_CHR"].ToString();
            }
            return "";
        }
        #endregion
        #region ��ȡҽ������-ת��
        /// <summary>
        /// ��ȡҽ������-ת��
        /// </summary>
        /// <returns>����ҽ������-ת��</returns>
        public string m_strGetORDERCATEID_TRANSFER_CHR()
        {
            DataTable dtbResult = new DataTable();
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSpChargeItemIDType(out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                return dtbResult.Rows[0]["ORDERCATEID_TRANSFER_CHR"].ToString();
            }
            return "";
        }
        #endregion
        #region ��ȡ��ʱҽ������ID
        /// <summary>
        /// ��ȡ��ʱҽ������ID
        ///		ע�⣺�����ֶ���Ϊ��FREQID_CHR��
        /// </summary>
        /// <returns></returns>
        public string m_strGetTemOrderRecipefreqID()
        {
            DataTable dtbResult = new DataTable();
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSpChargeItemIDType(out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                return dtbResult.Rows[0]["FREQID_CHR"].ToString();
            }
            return "";
        }
        #endregion
        #region ��ȡҽ������ID {ҩƷ}
        /// <summary>
        /// ��ȡҽ������ID {ҩƷ}
        ///		ע�⣺�����ֶ���Ϊ��ORDERCATEID_MEDICINE_CHR��
        /// </summary>
        /// <returns></returns>
        public string m_strGetMedicineOrderTypeID()
        {
            DataTable dtbResult = new DataTable();
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSpChargeItemIDType(out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                return dtbResult.Rows[0]["ORDERCATEID_MEDICINE_CHR"].ToString();
            }
            return "";
        }
        #endregion
        #region ��ȡ������ҽ��Ƶ��ID
        /// <summary>
        /// ��ȡҽ������ID {ҩƷ}
        ///		ע�⣺�����ֶ���Ϊ��CONFREQID_CHR��
        /// </summary>
        /// <returns></returns>
        public string m_strGetConfreqID()
        {
            DataTable dtbResult = new DataTable();
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSpChargeItemIDType(out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                return dtbResult.Rows[0]["CONFREQID_CHR"].ToString();
            }
            return "";
        }
        #endregion
        #region	�ж�ҽ���Ƿ��ų�
        #region Old
        /// <summary>
        /// �ж�ҽ��(����)���Ƿ�����ų�	�������ų�ҽ��ID
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="blnIsExclude">�Ƿ��ų�	[out ����]</param>
        /// <param name="p_strExcludeOrderID">�ų�ҽ��ID	[out ����]</param>
        /// <returns></returns>
        public long m_lngIsExcludeOrder(string[] p_strOrderIDArr, int p_intActiveType, out bool blnIsExclude, out string[] p_strExcludeOrderIDArr)
        {
            long lngRes = 0;
            blnIsExclude = false;
            p_strExcludeOrderIDArr = null;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngIsExcludeOrder(p_strOrderIDArr, p_intActiveType, out blnIsExclude, out p_strExcludeOrderIDArr);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        /// <summary>
        /// �ж�ҽ���Ƿ�����ų�
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_strOrderIDBaseArr">�����жϵ�ҽ��</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="blnIsExclude">�Ƿ��ų�	[out ����]</param>
        /// <param name="p_strExcludeOrderID">�ų�ҽ��ID	[out ����]</param>
        /// <returns></returns>
        public long m_lngIsExcludeOrder(string p_strOrderID, string[] p_strOrderIDBaseArr, int p_intActiveType, out bool blnIsExclude)
        {
            long lngRes = 0;
            blnIsExclude = false;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngIsExcludeOrder(p_strOrderID, p_strOrderIDBaseArr, p_intActiveType, out blnIsExclude);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        /// <summary>
        /// �ж�ҽ��(����)���Ƿ�����ų�	[����]	�������ų�ҽ��ID
        /// ע��: ��Ҫ�жϵ�ҽ���������ڡ������жϵ�ҽ�����ڣ����򷵻�ִ��ʧ�ܣ�
        /// </summary>
        /// <param name="p_strOrderIDAimArr">Ҫ�жϵ�ҽ��</param>
        /// <param name="p_strOrderIDBaseArr">�����жϵ�ҽ��</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="blnIsExclude">�Ƿ��ų�	[out ����]</param>
        /// <param name="p_strExcludeOrderIDArr">�ų�ҽ��ID	[out ����]</param>
        /// <returns></returns>
        public long m_lngIsExcludeOrder(string[] p_strOrderIDAimArr, string[] p_strOrderIDBaseArr, int p_intActiveType, out bool blnIsExclude, out string[] p_strExcludeOrderIDArr)
        {
            long lngRes = 0;
            blnIsExclude = false;
            p_strExcludeOrderIDArr = null;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            try
            {
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngIsExcludeOrder(p_strOrderIDAimArr, p_strOrderIDBaseArr, p_intActiveType, out blnIsExclude, out p_strExcludeOrderIDArr);
                //objSvc.Dispose();
                //objSvc = null;
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion
        #region New	����	2005-02-18
        /// <summary>
        /// �ж�ҽ���Ƿ�����ų�
        /// ע�⣺	
        ///		1���ж����ȼ�	[ȫ�ų�(ȫ��������)-��ͨ�ų�]��
        /// </summary>
        /// <param name="p_strOrderIDArr">Ҫ�жϵ�ҽ��	[����]</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="p_intExcludeType">[out ����]	�ų�����{0=û�ų⣻1=ȫ�ų���ʱҽ����2=ȫ�ųⳤ��ҽ����3=ȫ�ų��ٳ�ҽ����4=��ͨ�ų⣻}</param>
        /// <param name="p_strExcludeOrderIDArr">[out ����]	��Ŀ��ҽ�����д����ų��ҽ��ID</param>
        /// <param name="p_strExcludeOrderIDArr">[out ����]	��Ŀ��ҽ�����д����ų��ҽ������</param>
        /// <returns></returns>
        public long m_lngJudgeExcludeOrder(string[] p_strOrderIDArr, int p_intActiveType, out int p_intExcludeType, out string[] p_strExcludeOrderIDArr, out string[] p_strExcludeOrderNameArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngJudgeExcludeOrder(p_strOrderIDArr, p_intActiveType, out p_intExcludeType, out p_strExcludeOrderIDArr, out p_strExcludeOrderNameArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// �ж�ҽ���Ƿ�����ų�
        /// ע�⣺	
        ///		1���ж����ȼ�	[ȫ�ų�(ȫ��������)-��ͨ�ų�]��
        ///		2������û���ж�Ŀ��ҽ��������ų⣻
        /// </summary>
        /// <param name="p_strOrderIDAimArr">Ҫ�жϵġ�Ŀ��ҽ����</param>
        /// <param name="p_strOrderIDBaseArr">�����жϵġ�����ҽ����</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="p_intExcludeType">[out ����]	�ų�����{0=û�ų⣻1=ȫ�ų���ʱҽ����2=ȫ�ųⳤ��ҽ����3=ȫ�ų��ٳ�ҽ����4=��ͨ�ų⣻}</param>
        /// <param name="p_strExcludeOrderIDArr">[out ����]	��Ŀ��ҽ�����д����ų��ҽ��ID</param>
        /// <param name="p_strExcludeOrderIDArr">[out ����]	��Ŀ��ҽ�����д����ų��ҽ������</param>
        /// <returns></returns>
        public long m_lngJudgeExcludeOrder(string[] p_strOrderIDAimArr, string[] p_strOrderIDBaseArr, int p_intActiveType, out int p_intExcludeType, out string[] p_strExcludeOrderIDArr, out string[] p_strExcludeOrderNameArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngJudgeExcludeOrder(p_strOrderIDAimArr, p_strOrderIDBaseArr, p_intActiveType, out p_intExcludeType, out p_strExcludeOrderIDArr, out p_strExcludeOrderNameArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
        #endregion
        #region ��鲡���Ƿ����
        /// <summary>
        /// ��鲡���Ƿ������ٵ�
        /// </summary>
        /// <param name="p_strRegisterIDs">��Ժ�Ǽ�ID	{���,�ö��ŷָ�.��: ��'00001','0002','0006'��}</param>
        /// <param name="p_blnIsLeave">[out����]	���</param>
        /// <returns></returns>
        public long m_lngCheckPatientIsLeave(string p_strRegisterIDs, out bool p_blnIsLeave)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngCheckPatientIsLeave(p_strRegisterIDs, out p_blnIsLeave);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����ҽ�����ӵ���	����ID
        /// </summary>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult">ҽ�����ӵ���Vo����	[out ����]</param>
        /// <returns></returns>
        public long m_lngGetTemfororderByID(string p_strID, out clsT_Opr_Bih_Temfororder_VO p_objResult)
        {
            p_objResult = null;
            long lngRes = 0;
            //string strSQL = @"SELECT * FROM T_opr_bih_temfororder WHERE ID_CHR = '" + p_strID + "'";
            //try
            //{
            //    DataTable dtbResult = new DataTable();
            //    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            //    objHRPSvc.Dispose();
            //    if (lngRes > 0 && dtbResult.Rows.Count > 0)
            //    {
            //        p_objResult = new clsT_Opr_Bih_Temfororder_VO();
            //        p_objResult.m_strID_CHR = dtbResult.Rows[0]["ID_CHR"].ToString().Trim();
            //        p_objResult = new clsT_Opr_Bih_Temfororder_VO();
            //        p_objResult.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
            //        p_objResult.m_strPATIENTNAME_CHR = dtbResult.Rows[0]["PATIENTNAME_CHR"].ToString().Trim();
            //        p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
            //        p_objResult.m_strMAZUI_CHR = dtbResult.Rows[0]["MAZUI_CHR"].ToString().Trim();
            //        p_objResult.m_fltPSTATUS_CHR = Convert.ToInt32(dtbResult.Rows[0]["PSTATUS_CHR"].ToString().Trim());
            //        p_objResult.m_strDESC_VCHR = dtbResult.Rows[0]["DESC_VCHR"].ToString().Trim();
            //    }
            //}
            //catch (Exception objEx)
            //{
            //    string strTmp = objEx.Message;
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);
            //}
            return lngRes;
        }
        /// <summary>
        /// ����ҽ�����ӵ���	����ID
        /// </summary>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
        /// <param name="p_objResult">ҽ�����ӵ���Vo����	[out ����]</param>
        /// <returns></returns>
        public long m_lngGetTemfororderByPatientIDRegisterID(string p_strPatientID, string p_strRegisterID, out clsT_Opr_Bih_Temfororder_VO p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_Temfororder_VO();
            long lngRes = 0;
            //string strSQL = @"SELECT * FROM T_opr_bih_temfororder WHERE Trim(PATIENTID_CHR) = '" + p_strPatientID.Trim() + "' and Trim(REGISTERID_CHR) = '" + p_strRegisterID.Trim() + "'";
            //try
            //{
            //    DataTable dtbResult = new DataTable();
            //    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            //    objHRPSvc.Dispose();
            //    if (lngRes > 0 && dtbResult.Rows.Count > 0)
            //    {
            //        p_objResult.m_strID_CHR = dtbResult.Rows[0]["ID_CHR"].ToString().Trim();
            //        p_objResult = new clsT_Opr_Bih_Temfororder_VO();
            //        p_objResult.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
            //        p_objResult.m_strPATIENTNAME_CHR = dtbResult.Rows[0]["PATIENTNAME_CHR"].ToString().Trim();
            //        p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
            //        p_objResult.m_strMAZUI_CHR = dtbResult.Rows[0]["MAZUI_CHR"].ToString().Trim();
            //        p_objResult.m_fltPSTATUS_CHR = Convert.ToInt32(dtbResult.Rows[0]["PSTATUS_CHR"].ToString().Trim());
            //        p_objResult.m_strDESC_VCHR = dtbResult.Rows[0]["DESC_VCHR"].ToString().Trim();
            //    }
            //}
            //catch (Exception objEx)
            //{
            //    string strTmp = objEx.Message;
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);
            //}
            return lngRes;
        }
        #endregion
        #region ����
        /// <summary>
        /// ����ҽ�����ӵ���
        /// </summary>
        /// <param name="p_strRecordID">��ˮ��	[out ����]</param>
        /// <param name="p_objRecord">ҽ�����ӵ���Vo����</param>
        /// <returns></returns>
        public long m_lngAddNewTemfororder(out string p_strRecordID, clsT_Opr_Bih_Temfororder_VO p_objRecord)
        {
            p_strRecordID = string.Empty;
            return -1;
            //long lngRes = 0;
            //p_strRecordID = "";

            ////com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //lngRes = objHRPSvc.lngGenerateID(8, "ID_CHR", "T_opr_bih_temfororder", out p_strRecordID);
            //if (lngRes < 0)
            //    return lngRes;
            //string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string strSQL = "INSERT INTO T_opr_bih_temfororder (PATIENTID_CHR,PATIENTNAME_CHR,REGISTERID_CHR,MAZUI_CHR,ID_CHR,PSTATUS_CHR,DESC_VCHR) VALUES (?,?,?,?,?,?,?)";
            //try
            //{
            //    System.Data.IDataParameter[] objLisAddItemRefArr = null;
            //    objHRPSvc.CreateDatabaseParameter(7, out objLisAddItemRefArr);
            //    //Please change the datetime and reocrdid 
            //    objLisAddItemRefArr[0].Value = p_objRecord.m_strPATIENTID_CHR;
            //    objLisAddItemRefArr[1].Value = p_objRecord.m_strPATIENTNAME_CHR;
            //    objLisAddItemRefArr[2].Value = p_objRecord.m_strREGISTERID_CHR;
            //    objLisAddItemRefArr[3].Value = p_objRecord.m_strMAZUI_CHR;
            //    objLisAddItemRefArr[4].Value = p_strRecordID;
            //    objLisAddItemRefArr[5].Value = p_objRecord.m_fltPSTATUS_CHR;
            //    objLisAddItemRefArr[6].Value = p_objRecord.m_strDESC_VCHR;
            //    long lngRecEff = -1;
            //    //�������Ӽ�¼
            //    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
            //    objHRPSvc.Dispose();
            //}
            //catch (Exception objEx)
            //{
            //    string strTmp = objEx.Message;
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);
            //}
            //return lngRes;
        }
        #endregion
        #region �޸�
        /// <summary>
        /// �޸�ҽ�����ӵ���
        /// </summary>
        /// <param name="p_objRecord">ҽ�����ӵ���Vo</param>
        /// <returns></returns>
        public long m_lngModifyTemfororder(clsT_Opr_Bih_Temfororder_VO p_objRecord)
        {
            long lngRes = 0;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_OPR_BIH_TEMFORORDER A";
            strSQL += " SET";
            strSQL += "    A.PATIENTID_CHR ='" + p_objRecord.m_strPATIENTID_CHR.Trim() + "'";
            strSQL += "  , A.PATIENTNAME_CHR ='" + p_objRecord.m_strPATIENTNAME_CHR.Trim() + "'";
            strSQL += "  , A.REGISTERID_CHR ='" + p_objRecord.m_strREGISTERID_CHR.Trim() + "'";
            strSQL += "  , A.MAZUI_CHR = '" + p_objRecord.m_strMAZUI_CHR.Trim() + "'";
            strSQL += "  , A.PSTATUS_CHR = '" + p_objRecord.m_fltPSTATUS_CHR.ToString().Trim() + "'";
            strSQL += "  , A.DESC_VCHR = '" + p_objRecord.m_strDESC_VCHR.Trim() + "'";
            strSQL += " Where A.ID_CHR = '" + p_objRecord.m_strID_CHR.Trim() + "'";
            try
            {
                //com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = (new weCare.Proxy.ProxyHisBase()).Service.m_lngDoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ɾ��
        /// <summary>
        /// ɾ��ҽ�����ӵ���
        /// </summary>
        /// <param name="strID">ҽ�����ӵ���ID</param>
        /// <returns></returns>
        public long m_lngDeleteTemfororder(string strID)
        {
            long lngRes = 0;
            string strSQL = "Delete T_OPR_BIH_TEMFORORDER where Trim(ID_CHR) = '" + strID.Trim() + "'";
            try
            {
                //com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = (new weCare.Proxy.ProxyHisBase()).Service.m_lngDoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region �ύ
        /// <summary>
        /// �ύҽ�����ӵ���	���ݸ��ӵ���ID
        /// </summary>
        /// <param name="strID">ҽ�����ӵ���ID</param>
        /// <returns></returns>
        public long m_lngCommitTemfororder(string strID)
        {
            long lngRes = 0;
            string strSQL = "UPDATE T_OPR_BIH_TEMFORORDER A SET A.PSTATUS_CHR = '1' Where A.PSTATUS_CHR<>'1' and A.ID_CHR = '" + strID.Trim() + "'";
            try
            {
                //com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = (new weCare.Proxy.ProxyHisBase()).Service.m_lngDoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// �ύҽ�����ӵ���	[����]	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <returns></returns>
        /// <remarks>
        /// ��Ϊһ�����ﴦ��
        /// </remarks>
        public long m_lngCommitAttachOrder(string p_strOrderID)
        {
            long lngRes = 0;
            string[] strAttachIDArr;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objTem = new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService();
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetAttachOrder(p_strOrderID, out strAttachIDArr);
            for (int i1 = 0; i1 < strAttachIDArr.Length; i1++)
            {
                if (lngRes > 0) lngRes = m_lngCommitTemfororder(strAttachIDArr[i1]);
            }
            if (lngRes <= 0)
            {
                throw (new System.Exception("���ӵ����ύʧ�ܣ�"));
            }
            return lngRes;
        }
        #endregion

        #region ��ȡҽ��-���ݲ���ID

        public long m_lngGetOrderByArea(string m_strAreaid_chr, int m_intState, out DataTable m_dtExecOrder, out DataTable m_dtPatients, out DataTable m_dtChargeList)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByArea(m_strAreaid_chr, m_intState, out m_dtExecOrder, out m_dtPatients, out m_dtChargeList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        public long m_lngGetOrderByArea(string m_strAreaid_chr, int m_intState, out DataTable m_dtExecOrder, out DataTable m_dtChargeList)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByArea(m_strAreaid_chr, m_intState, out m_dtExecOrder, out m_dtChargeList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        public long m_lngGetPatientInfoVo(string registerid_chr, out DataTable m_dtPatients)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPatientInfoVo(registerid_chr, out m_dtPatients);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngUpdateBihOrderConfirmer(List<EntityConfirmOrder> lstOrder, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateBihOrderConfirmer(lstOrder, CONFIRMERID_CHR, CONFIRMER_VCHR);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngUpdateBihOrderRedraw(List<string> m_strORDERID_Arr, string RETRACTORID_CHR, string RETRACTOR_CHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateBihOrderRedraw(m_strORDERID_Arr, RETRACTORID_CHR, RETRACTOR_CHR);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngUpdateBihOrderBack(string m_strORDERID_Arr, string SENDBACKID_CHR, string SENDBACKER_CHR, string m_strBACKREASON)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateBihOrderBack(m_strORDERID_Arr, SENDBACKID_CHR, SENDBACKER_CHR, m_strBACKREASON);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ��Ժҽ�����¹�����
        /// </summary>
        /// <param name="m_strOrderID"></param>
        /// <param name="confirmerid"></param>
        /// <param name="confirmer"></param>
        /// <returns></returns>
        internal long m_lngUpdateLeaveConfiemer(string m_strOrderID, string confirmerid, string confirmer)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateLeaveConfiemer(m_strOrderID, confirmerid, confirmer);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #region ��ȡҽ��-���ݲ����ɣ�

        public long m_lngGetExecOrderByArea(string m_strAreaid_chr, int m_intState, out DataTable m_dtExecOrder, out DataTable m_dtPatients, out DataTable m_dtChargeList, out DataTable m_dtChargeSum, out DataTable m_dtChargeMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetExecOrderByArea(m_strAreaid_chr, out m_dtExecOrder, out m_dtPatients, out m_dtChargeList, out m_dtChargeSum, out m_dtChargeMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ҫ����ȷ�ϼ��ʵ�����

        public long m_lngGetComfirmChargeData(string m_strRegisterID, out DataTable m_dtOrderExecute, out DataTable m_dtChargeList)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetComfirmChargeData(m_strRegisterID, out m_dtOrderExecute, out m_dtChargeList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡҽ��-���ݲ����ɣ� ����λID

        public long m_lngGetExecOrderByAreaBed(string m_strAreaid_chr, string m_strRegisterID, out DataTable m_dtExecOrder, out DataTable m_dtPatients, out DataTable m_dtChargeList, out DataTable m_dtChargeSum, out DataTable m_dtChargeMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetExecOrderByAreaBed(m_strAreaid_chr, m_strRegisterID, out m_dtExecOrder, out m_dtPatients, out m_dtChargeList, out m_dtChargeSum, out m_dtChargeMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion


        //internal long m_lngUpdateBihOrderExecConfirmer(ArrayList m_strORDERID_Arr, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        //{
        //    long lngRes = 0;
        //    //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
        //    lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateBihOrderExecConfirmer(m_strORDERID_Arr, CONFIRMERID_CHR, CONFIRMER_VCHR);
        //    //objSvc.Dispose();
        //    //objSvc = null;
        //    return lngRes;
        //}

        internal long m_lngGetPersonListByArea(string m_strAreaid_chr, out DataTable m_dtPatients)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPersonListByArea(m_strAreaid_chr, out m_dtPatients);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ҽ��ִ�е���ȷ�Ϸ���ȷ�ϲ�����
        /// </summary>
        /// <param name="m_strOrderExecuteID_Arr"></param>
        /// <param name="CONFIRMERID_CHR"></param>
        /// <param name="CONFIRMER_VCHR"></param>
        /// <returns></returns>
        internal long m_lngBihOrderExecuteChargeConfirmer(string m_strOrderExecuteID_Arr, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngBihOrderExecuteChargeConfirmer(m_strOrderExecuteID_Arr, CONFIRMERID_CHR, CONFIRMER_VCHR);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ҽ��ִ�е���ȷ�Ϸ�������
        /// </summary>
        /// <param name="m_strOrderExecuteID_Arr"></param>
        /// <param name="CONFIRMERID_CHR"></param>
        /// <param name="CONFIRMER_VCHR"></param>
        /// <returns></returns>
        internal long m_lngBihOrderExecuteDenableConfirmer(string m_strOrderExecuteID_Arr, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngBihOrderExecuteDenableConfirmer(m_strOrderExecuteID_Arr, CONFIRMERID_CHR, CONFIRMER_VCHR);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// /��鵱ǰ�Ƿ��л�û����ȷ���Ƿ���Ҫ������� 0-�� 1-�ǵĵ�����˵���û�з��͵�ҽ�����뵥
        /// </summary>
        /// <param name="m_strCurrentRegisterID"></param>
        /// <param name="m_blhave"></param>
        /// <returns></returns>
        internal long m_lngCheckTheExecuteBill(string m_strCurrentRegisterID, out bool m_blhave)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngCheckTheExecuteBill(m_strCurrentRegisterID, out m_blhave);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }


        /// <summary>
        /// �������޸��շ���Ŀʱ�����ύ�շ�����
        /// </summary>
        /// <param name="p"></param>
        /// <param name="m_intState"></param>
        /// <param name="m_dtChargeList"></param>
        /// <returns></returns>
        internal long m_lngrefreshTheChargeDate(string m_strAreaid_chr, int m_intState, out DataTable m_dtChargeList)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngrefreshTheChargeDate(m_strAreaid_chr, m_intState, out m_dtChargeList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngGetComfirmThChargeData(string m_strRegisterID, out DataTable m_dtOrderExecute, out DataTable m_dtChargeList)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetComfirmThChargeData(m_strRegisterID, out m_dtOrderExecute, out m_dtChargeList);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }


        internal long m_lngCheckTheChanged(string m_strAreaid_chr, int m_intState, out DataTable m_dtExecOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngCheckTheChanged(m_strAreaid_chr, m_intState, out m_dtExecOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ����ҽ��ִ��ԤԼ���¼(������뵥)
        /// </summary>
        /// <param name="m_OrderBookingVO">ҽ��ִ��ԤԼ��</param>
        /// <returns></returns>
        internal long m_lngUpdateOrderBookingArr(clsOrderBooking[] m_OrderBookingVO)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateOrderBookingArr(m_OrderBookingVO);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// �����������뵥��Ӧ��(������뵥)
        /// </summary>
        /// <param name="m_OrderBookingVO">�������뵥��Ӧ��</param>
        /// <returns></returns>
        internal long m_lngUpdateOrderAttachRelation(clsATTACHRELATION_VO[] m_arATTACHRELATION)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateOrderAttachRelation(m_arATTACHRELATION);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_mthGetCheckByID2(string m_strOrderDicID, out bool isHave)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_mthGetCheckByID2(m_strOrderDicID, out isHave);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_mthGetApplyTypeByID(string m_strChargeITEMID_CHR, out DataTable dt)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_mthGetApplyTypeByID(m_strChargeITEMID_CHR, out dt);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #region ��ȡҽ��-���ݲ����ɣ�  ���߳�����
        /// <summary>
        /// ͨ������ID���ҳ��ò�����Ҫִ�е�ҽ��
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <returns></returns>
        public long m_lngGetExecOrderDTByArea(string m_strAreaid_chr, out DataTable m_dtExecOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetExecOrderDTByArea(m_strAreaid_chr, out m_dtExecOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ҽ������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <returns></returns>
        public long m_lngGetExecOrderDTByAreaBed(string m_strAreaid_chr, string m_strRegisterID, out DataTable m_dtExecOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetExecOrderDTByAreaBed(m_strAreaid_chr, m_strRegisterID, out m_dtExecOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtPatients"></param>
        /// <returns></returns>

        public long m_lngGetPatientDTByArea(string m_strAreaid_chr, out DataTable m_dtPatients)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPatientDTByArea(m_strAreaid_chr, out m_dtPatients);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtPatients"></param>
        /// <returns></returns>
        /// ������һ��������õ�û���ظ�������ˮ�ŵĲ��˺����飬��ȡ���������Ϣ����Щ���˾���ҽ��������Ҫִ��
        //ԭ����ȷ�Ĵ���,��Ҫ���� public long m_lngGetPatientDTByArea(ArrayList m_arrRegisterid_chr, out DataTable m_dtPatients)
        public long m_lngGetPatientDTByArea(System.Collections.Generic.List<string> m_glstRegisterid_chr, out DataTable m_dtPatients)
        {
            long lngRes = 0;

            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPatientDTByArea(m_glstRegisterid_chr, out m_dtPatients);
            //objSvc.Dispose();
            //objSvc = null;

            return lngRes;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtPatients"></param>
        /// <returns></returns>
        public long m_lngGetPatientDTByAreaBed(string m_strAreaid_chr, string m_strRegisterID, out DataTable m_dtPatients)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetPatientDTByAreaBed(m_strAreaid_chr, m_strRegisterID, out m_dtPatients);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeSum"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <returns></returns>
        public long m_lngGetChargeByArea(string m_strAreaid_chr, System.Collections.Generic.List<string> m_glstRegisterid_chr, out DataTable m_dtChargeList, out DataTable m_dtChargeMoney, out DataTable m_dtPrepay)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetChargeByArea(m_strAreaid_chr, m_glstRegisterid_chr, out m_dtChargeList, out m_dtChargeMoney, out m_dtPrepay);
            return lngRes;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeSum"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <returns></returns>
        //ԭ������ȷ����,��Ҫ���� public long m_lngGetChargeByRegisterids(ArrayList m_arrRegisterid_chr, out DataTable m_dtChargeMoney, out DataTable m_dtPrepay)
        public long m_lngGetChargeByRegisterids(System.Collections.Generic.List<string> m_glstRegisterid_chr, out DataTable m_dtChargeMoney, out DataTable m_dtPrepay)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetChargeByRegisterids(m_glstRegisterid_chr, out m_dtChargeMoney, out m_dtPrepay);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeSum"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <returns></returns>
        public long m_lngGetChargeByArea(string m_strAreaid_chr, out DataTable m_dtChargeList, out DataTable m_dtChargeMoney, out DataTable m_dtPrepay)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetChargeByArea(m_strAreaid_chr, out m_dtChargeList, out m_dtChargeMoney, out m_dtPrepay);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeSum"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <returns></returns>
        public long m_lngGetChargeByAreaBed(string m_strAreaid_chr, string m_strRegisterID, out DataTable m_dtChargeList, out DataTable m_dtChargeMoney)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetChargeByAreaBed(m_strAreaid_chr, m_strRegisterID, out m_dtChargeList, out m_dtChargeMoney);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion

        internal long m_lngGetBihExecOrderControls(out decimal m_dmlMedOCMin, out decimal m_dmlNoMedOCMin, out decimal m_dmlMedICMin, out decimal m_dmlNoMedICMin, out int m_intMoneyControl)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetBihExecOrderControls(out m_dmlMedOCMin, out m_dmlNoMedOCMin, out m_dmlMedICMin, out m_dmlNoMedICMin, out m_intMoneyControl);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ҽ��ִ�У�����ִ�е��������շ���ϸ
        /// </summary>
        /// <param name="m_arrExecOrder"></param>
        /// <param name="m_objCare">�ȼ�������</param>
        /// <param name="m_objEat">��ʳ����</param>
        /// <returns></returns>
        internal long m_lngUpdateBihOrderExecConfirmer(System.Collections.Generic.List<clsExecOrderVO> m_glstExecutablePhysicianOrderList, List<clsPatientNurseVO> glstNurseVO, List<EntityCureMed> lstCureMed, List<EntityCureSubStock> lstSubStock, out List<clsT_Bih_Opr_Putmeddetail_VO> lstPutMedCfkl, out string error)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateBihOrderExecConfirmer(m_glstExecutablePhysicianOrderList, glstNurseVO, lstCureMed, lstSubStock, out lstPutMedCfkl, out error);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long GetTheComfirmControl(out int m_intNeedConfirm, out int m_intExeConfirm)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.GetTheComfirmControl(out m_intNeedConfirm, out m_intExeConfirm);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngFindArea(string strFindCode, out clsBIHArea[] objItemArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsInPatientQuerySvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsInPatientQuerySvc)
            //     com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsInPatientQuerySvc));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(strFindCode, out objItemArr);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        internal long m_lngGetBihBedByArea(string strAreaID, string strInputCode, out clsBIHBed[] arrBed)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.HIS.clsReport_d_bihSvc objSvc = (com.digitalwave.iCare.middletier.HIS.clsReport_d_bihSvc)
            //  com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsReport_d_bihSvc));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetBihBedByArea(strAreaID, strInputCode, out arrBed);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;


        }

        internal long m_lngModifyOrderFeelEnd(clsT_Opr_Bih_OrderFeel_VO p_objRecord)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngModifyOrderFeelEnd(p_objRecord);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngStopBihOrderConfirmer(System.Collections.Generic.List<string> m_strORDERID_Arr, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngStopBihOrderConfirmer(m_strORDERID_Arr, CONFIRMERID_CHR, CONFIRMER_VCHR);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngExecDrawOrderByOrderID(List<clsBIHCanExecOrder> m_arrExecOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngExecDrawOrderByOrderID(m_arrExecOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngGetOrderMessageByTimer(string m_strAreaid_chr, out DataTable m_dtExecOrder)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderMessageByTimer(m_strAreaid_chr, out m_dtExecOrder);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        /*ԭ������ȷ���룬��Ҫ����
        internal long GetTheHisControl(ArrayList m_arrControl, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.GetTheHisControl(m_arrControl, out  dtbResult);
            return lngRes;
        }
        */
        //�������´���
        internal long GetTheHisControl(System.Collections.Generic.List<string> m_glstControl, out DataTable dtbResult)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.GetTheHisControl(m_glstControl, out dtbResult);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        //�������´���
        internal long m_lngSaveTheEntrust(string m_strRegisterID, int m_intRecipenNo, string m_strEntrust)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngSaveTheEntrust(m_strRegisterID, m_intRecipenNo, m_strEntrust);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        internal long m_lngSaveTheATTACHTIMES_INT(string m_strRegisterID, int m_intRecipenNo, int ATTACHTIMES_INT)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngSaveTheATTACHTIMES_INT(m_strRegisterID, m_intRecipenNo, ATTACHTIMES_INT);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        //����ҽ��ִ�б�t_opr_bih_orderexecute�����¼��ÿ��ҽ������ִ�д���
        internal long m_lngGetReExecute(string m_strAreaid_chr, out DataTable m_dtReExecute)
        {
            long lngRes = 0;
            ////com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetReExecute(m_strAreaid_chr, out m_dtReExecute);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        internal long IsAllPatSend(string m_strAreaid_chr, out bool ifAll)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.IsAllPatSend(m_strAreaid_chr, out ifAll);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        internal long LoadThePARMVALUE(string PARMCODE_CHR, out string m_strPARMVALUE_VCHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.LoadThePARMVALUE(PARMCODE_CHR, out m_strPARMVALUE_VCHR);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        internal long LoadThePARMVALUE(List<string> PARMCODE_CHR, out DataTable m_dtPARMVALUE_VCHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.LoadThePARMVALUE(PARMCODE_CHR, out m_dtPARMVALUE_VCHR);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ֱ�Ӳ��ϵ�ȷ��
        /// </summary>
        /// <param name="m_arrPCHARGEID_CHR"></param>
        /// <param name="p"></param>
        /// <param name="p_3"></param>
        /// <returns></returns>
        internal long m_lngBihOrderExecuteChargeConfirmerTh(List<string> m_arrPCHARGEID_CHR, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            ////com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngBihOrderExecuteChargeConfirmerTh(m_arrPCHARGEID_CHR, CONFIRMERID_CHR, CONFIRMER_VCHR);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        //ԭ���Ĵ���
        internal long m_lngConfirmCurrentOrder(string[] m_arrORDERID, out DataTable m_dtOrder)
        {
            long lngRes = 0;
            ////com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngConfirmCurrentOrder(m_arrORDERID, out m_dtOrder);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        public long m_lngGetOrderStopSign(string[] m_arrOrders, out DataTable m_dtOrderSign)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderStopSign(m_arrOrders, out m_dtOrderSign);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ��ȡ���ʶ�ţ�m_arrREGISTERID_CHR��m_arrORDERID_CHRֻ����һ��������Ŀ>0��
        /// </summary>
        /// <param name="m_arrREGISTERID_CHR">������ˮ������</param>
        /// <param name="m_arrORDERID_CHR">ҽ����ˮ������</param>
        /// <param name="motion_id_int">���β�����ʶ</param>
        /// <returns></returns>
        public long m_lngGetMotionID(List<string> m_arrREGISTERID_CHR, List<string> m_arrORDERID_CHR, out long motion_id_int)
        {
            long lngRes = 0;
            motion_id_int = 0;
            if (m_arrREGISTERID_CHR == null)
            {
                m_arrREGISTERID_CHR = new List<string>();
            }
            if (m_arrORDERID_CHR == null)
            {
                m_arrORDERID_CHR = new List<string>();
            }
            if (m_arrREGISTERID_CHR.Count <= 0 && m_arrORDERID_CHR.Count <= 0)
            {
                return lngRes;
            }
            ////com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetMotionID(m_arrREGISTERID_CHR, m_arrORDERID_CHR, out motion_id_int);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        internal long m_lngGetOrderLisSign(string[] m_arrOrders, out DataTable m_dtOrderSign)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderLisSign(m_arrOrders, out m_dtOrderSign);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        #region Ƥ�Է��õ���ȡ
        /// <summary>
        /// Ƥ�Է��õ���ȡ
        /// </summary>
        /// <param name="strOrderID"></param>
        /// <param name="strChargeItemID"></param>
        /// <param name="ExecuteType_Int"></param>
        /// <param name="CURAREAID_CHR"></param>
        /// <param name="CURBEDID_CHR"></param>
        /// <param name="CONFIRMERID_CHR"></param>
        /// <returns></returns>
        public long m_lngFeelCharge(string strOrderID, string strChargeItemID, int ExecuteType_Int, string CURAREAID_CHR, string CURBEDID_CHR, string CONFIRMERID_CHR, bool isChildPrice)
        {
            long lngRes = 0;
            ////com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngFeelCharge(strOrderID, strChargeItemID, ExecuteType_Int, CURAREAID_CHR, CURBEDID_CHR, CONFIRMERID_CHR, isChildPrice);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        /// <summary>
        /// ɾ��Ƥ�Է���
        /// </summary>
        /// <param name="strOrderID"></param>
        /// <returns></returns>
        public long m_lngDeleteFeelCharge(string strOrderID)
        {
            long lngRes = 0;
            ////com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteFeelCharge(strOrderID);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }
        #endregion

        #region ���������ձ�
        /// <summary>
        /// ���������ձ�
        /// </summary>
        /// <param name="objTmp"></param>
        /// <returns></returns>
        internal long m_lngGetAPPLY_RLT(out System.Collections.Generic.Dictionary<string, string> objTmp)
        {
            long lngRes = 0;
            // com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc objSvc =
            //(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsDoctorWorkStationSvc));
            lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetAPPLY_RLT(out objTmp);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ�����
        /// <summary>
        /// ��ȡҩƷ�����
        /// </summary>
        /// <param name="p_dtnMedID">ҩƷID (ҩ��ID,(ҩƷID))</param>
        /// <param name="p_dtnKCL">ҩƷ�����(ҩ��ID*ҩƷID,�����)</param>

        /// <returns></returns>
        public long m_lngGetMedicineKC(System.Collections.Generic.Dictionary<string, List<string>> p_dtnMedID, out System.Collections.Generic.Dictionary<string, double> p_dtnKCL, out clsDsStorageVO[] p_objDsStorageVOArr)
        {
            ////com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService));
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetMedicineKC(p_dtnMedID, out p_dtnKCL, out p_objDsStorageVOArr);
            ////objSvc.Dispose();
            ////objSvc = null;
            return lngRes;
        }

        #endregion

        #region �Ƿ����ö�ͯ�۸�
        /// <summary>
        /// �Ƿ����ö�ͯ�۸�
        /// </summary>
        /// <returns></returns>
        public bool IsUseChildPrice()
        {
            //using (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService svc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)))
            //{
            return (new weCare.Proxy.ProxyIP()).Service.IsUseChildPrice();
            //}
        }
        #endregion

        #region ���Ҷ�ͯ�۸��Ƿ������;����
        /// <summary>
        /// ���Ҷ�ͯ�۸��Ƿ������;����
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public DateTime? GetMiddChargeDate(string registerId)
        {
            //using (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService svc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderExecuteService)))
            //{
            return (new weCare.Proxy.ProxyIP()).Service.GetMiddChargeDate(registerId);
            //}
        }
        #endregion

    }
    //T_opr_bih_temfororder(ҽ�����ӵ���)��ʱ��
    #region	T_opr_bih_temfororder(ҽ�����ӵ�����ˮ��)
    /// <summary>
    ///	clsT_Opr_Bih_Temfororder_VO(ҽ�����ӵ�����ˮ��)
    /// </summary>
    public class clsT_Opr_Bih_Temfororder_VO : BaseDataEntity
    {
        /// <summary>
        ///	��ˮ��
        /// </summary>
        public string m_strID_CHR;
        /// <summary>
        ///	����ID
        /// </summary>
        public string m_strPATIENTID_CHR;
        /// <summary>
        ///	��������
        /// </summary>
        public string m_strPATIENTNAME_CHR;
        /// <summary>
        ///	��Ժ�Ǽ���ˮ��
        /// </summary>
        public string m_strREGISTERID_CHR;
        /// <summary>
        ///	������
        /// </summary>
        public string m_strMAZUI_CHR;
        /// <summary>
        ///	״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}
        /// </summary>
        public float m_fltPSTATUS_CHR;
        /// <summary>
        ///	��ע
        /// </summary>
        public string m_strDESC_VCHR;
    }
    #endregion
}
