using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ����DOMIAN��
    /// </summary>
    public class clsDcl_Charge : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsDcl_Charge()
        {
        }

        weCare.Proxy.ProxyIP01 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyIP01();
            }
        }
        #endregion

        #region ����Ա��ID��ȡ��������Ϣ
        /// <summary>
        /// ����Ա��ID��ȡ��������Ϣ
        /// </summary>
        /// <param name="EmpID">Ա��ID</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetGroupEmp(string EmpID, out DataTable dt)
        {
            return (new weCare.Proxy.ProxyBase()).Service.m_lngGetGroupEmp(EmpID, out dt);
        }
        #endregion

        #region ���ݷ��෶Χ��ȡ���÷���(������㡢��Ʊ��סԺ���㡢��Ʊ)������Ϣ
        /// <summary>
        /// ���ݷ��෶Χ��ȡ���÷���(������㡢��Ʊ��סԺ���㡢��Ʊ)������Ϣ
        /// </summary>
        /// <param name="Scope">��Χ: 1 ������� 2 ���﷢Ʊ 3 סԺ���� 4 סԺ��Ʊ</param>
        /// <param name="Status">% ȫ�� 0 ͣ�� 1 ����</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetDefChargeCat(string Scope, string Status, out DataTable dt)
        {
            return proxy.Service.m_lngGetDefChargeCat(Scope, Status, out dt);
        }
        #endregion

        #region ��ȡ���(�ѱ�)��Ϣ
        /// <summary>
        /// ��ȡ���(�ѱ�)��Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPayTypeInfo(out DataTable dt)
        {
            return proxy.Service.m_lngGetPayTypeInfo(out dt);
        }
        #endregion

        #region ��ȡԱ�������
        /// <summary>
        /// ��ȡԱ�������
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetEmployee(out DataTable dt)
        {
            return proxy.Service.m_lngGetEmployee(out dt);
        }
        /// <summary>
        /// ��ȡԱ�������
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="empTypeId"></param>
        /// <returns></returns>
        public long m_lngGetEmployee(out DataTable dt, int empTypeId)
        {
            return proxy.Service.m_lngGetEmployee(out dt, empTypeId);
        }
        #endregion

        #region ����Ա�����Ż�ȡID������
        /// <summary>
        /// ����Ա�����Ż�ȡID������
        /// </summary>
        /// <param name="EmpCode"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        public long m_lngGetEmployee(string EmpCode, out string EmpID, out string EmpName)
        {
            return proxy.Service.m_lngGetEmployee(EmpCode, out EmpID, out EmpName);
        }
        #endregion

        #region �����û�ID��ȡ������ɫ�б�
        /// <summary>
        /// �����û�ID��ȡ������ɫ�б�
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetEmpRole(string EmpID, out DataTable dt)
        {
            return proxy.Service.m_lngGetEmpRole(EmpID, out dt);
        }
        #endregion

        #region ��ò�����Ϣ
        /// <summary>
        /// ��ò�����Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Flag">1 ���� 2 ����</param>
        /// <returns></returns>
        public long m_lngGetDeptArea(out DataTable dt, int Flag)
        {
            return proxy.Service.m_lngGetDeptArea(out dt, Flag);
        }
        #endregion

        #region ���ݲ���ID�ʹ���ID(��CODE)��ȡסԺ��
        /// <summary>
        /// ���ݲ���ID�ʹ���ID(��CODE)��ȡסԺ��
        /// </summary>
        /// <param name="AreaID">����ID</param>
        /// <param name="BedID">����ID(��CODE)</param>          
        /// <returns></returns>        
        public string m_strGetZyhByAreaAndBedID(string AreaID, string BedID)
        {
            return proxy.Service.m_strGetZyhByAreaAndBedID(AreaID, BedID);
        }
        #endregion

        #region ����סԺ�Ż����ƿ��Ż�ȡ������Ϣ
        /// <summary>
        /// ����סԺ�Ż����ƿ��Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="no"></param>
        /// <param name="dt"></param>
        /// <param name="flag">0 ���� 1 ��Ժ 2 ��Ժ 3 ����</param>
        /// <param name="type">0 ���ƿ��Ż�סԺ�� 1 ���ƿ���  2 סԺ�� </param>
        /// <returns></returns>
        public long m_lngGetPatientinfoByNO(string no, out DataTable dt, int flag, int type)
        {
            return proxy.Service.m_lngGetPatientinfoByNO(no, out dt, flag, type);
        }
        #endregion

        #region  ���ҳ�Ժ���˴���
        /// <summary>
        /// ���ҳ�Ժ���˴���
        /// </summary>
        /// <param name="no"></param>
        /// <param name="type"></param>
        /// <param name="p_strBedNo"></param>
        /// <returns></returns>
        public long m_lngGetDedNo(string no, ref string p_strBedNo)
        {
            return proxy.Service.m_lngGetDedNo(no, ref p_strBedNo);
        }
        #endregion

        #region ����סԺ�Ǽ���ˮ�Ų��Ҳ�������������Ϣ
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�Ų��Ҳ�������������Ϣ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetPatientDayaccountsByRegID(string RegID, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientDayaccountsByRegID(RegID, out dt);
        }
        #endregion

        #region ��ȡ��������Ч������Ϣ
        /// <summary>
        /// ��ȡ��������Ч������Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type">1 ������Ժ�Ǽ�ID 2 ��������ID </param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeInfoByID(string ID, int type, out DataTable dt)
        {
            return proxy.Service.m_lngGetChargeInfoByID(ID, type, out dt);
        }
        #endregion

        #region ĸӤ�ϲ��������һ���ѯucpatien�ؼ�ʹ��
        /// <summary>
        /// ĸӤ�ϲ��������һ���ѯucpatien�ؼ�ʹ��
        /// </summary>
        /// <param name="p_strRegisterID">����registerId</param>
        /// <param name="p_dtbCharge"></param>
        /// <returns></returns>
        public long m_lngGetChargeInfoByIDForBaby(string p_strRegisterID, out DataTable p_dtbCharge)
        {
            return proxy.Service.m_lngGetChargeInfoByIDForBaby(p_strRegisterID, out p_dtbCharge);
        }
        #endregion

        #region ��ȡ��Ŀ������Ϣ(������ࡢ��Ʊ����)
        /// <summary>
        /// ��ȡ��Ŀ������Ϣ(������ࡢ��Ʊ����)
        /// </summary>
        /// <param name="flag">�������ͣ�1 ������� 2 ���﷢Ʊ 3 סԺ���� 4 סԺ��Ʊ 5 ��������</param>
        /// <param name="dt"></param>
        /// <returns></returns>              
        public long m_lngGetChargeItemCat(int flag, out DataTable dt)
        {
            return proxy.Service.m_lngGetChargeItemCat(flag, out dt);
        }
        #endregion

        #region ����סԺ�Ǽ���ˮ�ż����Ŀ״̬
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�ż����Ŀ״̬
        /// </summary>
        /// <param name="RegID">סԺ�Ǽ���ˮ��</param>
        /// <param name="status">0=��ȷ��;1=����;2=����;3=����;4=ֱ��</param>
        /// <returns></returns>
        public bool m_blnCheckChargeItemStatus(string RegID, int status)
        {
            return proxy.Service.m_blnCheckChargeItemStatus(RegID, status);
        }
        #endregion

        #region ����סԺ�Ǽ���ˮ�����ɸ�״̬��Ŀ�ܷ���
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�����ɸ�״̬��Ŀ�ܷ���
        /// </summary>
        /// <param name="RegID">סԺ�Ǽ���ˮ��</param>
        /// <param name="status">0 ��ȷ�� 1 ���� 2 ���� 3 ���� 4 ֱ�� 9 ��������</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetChargeItemFee(string RegID, int status, out DataTable dt)
        {
            return proxy.Service.m_lngGetChargeItemFee(RegID, status, out dt);
        }
        #endregion

        #region ��鷢Ʊ���Ƿ��ظ�
        /// <summary>
        /// ��鷢Ʊ���Ƿ��ظ�
        /// </summary>
        /// <param name="CurrNo"></param>
        /// <returns></returns>
        public bool m_blnCheckInvoiceNo(string CurrNo)
        {
            return proxy.Service.m_blnCheckInvoiceNo(CurrNo);
        }
        #endregion

        #region ������Ժ�Ǽ���ˮID��ȡ�����𡢴�λ������ʱ��
        /// <summary>
        /// ������Ժ�Ǽ���ˮID��ȡ�����𡢴�λ������ʱ��
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="FinallyDate"></param>
        /// <returns></returns>
        public long m_lngGetFinallyDiagFeeDateByRegID(string RegID, out string FinallyDate)
        {
            return proxy.Service.m_lngGetFinallyDiagFeeDateByRegID(RegID, out FinallyDate);
        }
        #endregion

        #region ������Ժ�Ǽ���ˮ�Ż�ȡԤ��Ժʱ��
        /// <summary>
        /// ������Ժ�Ǽ���ˮ�Ż�ȡԤ��Ժʱ��
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="PrepDate"></param>
        /// <returns></returns>
        public long m_lngGetPrepLHDateByRegID(string RegID, out string PrepDate)
        {
            return proxy.Service.m_lngGetPrepLHDateByRegID(RegID, out PrepDate);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="DayChrgType">���ʽ������ͣ�1 ���� 2 ��ϸ</param>
        /// <param name="DayAccountsArr"></param>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeCatArr"></param>
        /// <param name="Invoice_VO"></param>
        /// <param name="InvoCatArr"></param>
        /// <param name="PaymentArr"></param>
        /// <param name="PrePayDeal">Ԥ������ 0 ������ 1 �˻� 2 ת����</param> 
        /// <param name="PrePayIDArr"></param>
        /// <param name="ChrgType">�������ͣ�1 ��;���� 2 ��Ժ���� 3 ���ʽ���</param>
        /// <returns></returns>
        public long m_lngReckoning(DataTable dtSource, int DayChrgType, List<clsBihDayAccounts_VO> DayAccountsArr, clsBihCharge_VO Charge_VO, List<clsBihChargeCat_VO> ChargeCatArr, clsBihInvoice_VO Invoice_VO, List<clsBihInvoiceCat_VO> InvoCatArr, List<clsBihPayment_VO> PaymentArr, int PrePayDeal, List<string> PrePayIDArr, int ChargeType, clsBihConfirm_VO Confirm_VO, out string ChargeNo)
        {
            //return proxy.Service.m_lngReckoning(dtSource, DayChrgType, DayAccountsArr, Charge_VO, ChargeCatArr, Invoice_VO, InvoCatArr, PaymentArr, PrePayDeal, PrePayIDArr, ChargeType, Confirm_VO, out ChargeNo);
            return proxy.Service.m_lngReckoning(weCare.Core.Utils.Compression.Zip(dtSource), DayChrgType, DayAccountsArr, Charge_VO, ChargeCatArr, Invoice_VO, InvoCatArr, PaymentArr, PrePayDeal, PrePayIDArr, ChargeType, Confirm_VO, out ChargeNo);
        }
        #endregion

        #region �˿�
        /// <summary>
        /// �˿�
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="Invono"></param>
        /// <param name="EmpID"></param>
        /// <param name="ChargeType">�������ͣ�1 ��;���� 2 ��Ժ���� 3 ���ʽ��� 4 ֱ�� 5 ȷ���շ� 6 ���ʲ��������</param>
        /// <returns></returns>
        public long m_lngRefundment(string ChargeNo, string Invono, string EmpID, int ChargeType, int PayMode)
        {
            return proxy.Service.m_lngRefundment(ChargeNo, Invono, EmpID, ChargeType, PayMode);
        }
        #endregion

        #region ���ݽ���Ż�ȡ��Ʊ��ϸ��Ϣ
        /// <summary>
        /// ���ݽ���Ż�ȡ��Ʊ��ϸ��Ϣ
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPrepay"></param>
        /// <returns></returns>
        public long m_lngGetInvoiceByChargeNo(string ChargeNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPrepay, out DataTable dtPayMode, out DataTable dtItemDate)
        {
            return proxy.Service.m_lngGetInvoiceByChargeNo(ChargeNo, out dtMain, out dtDet, out dtPrepay, out dtPayMode, out dtItemDate);
        }
        #endregion

        #region ������Ժ�Ǽ���ˮID��ȡ��Ʊ����Ϣ
        /// <summary>
        /// ������Ժ�Ǽ���ˮID��ȡ��Ʊ����Ϣ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="Type">��Ʊ���ͷ�Χ��1 ���� 2 ����+�ش�</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetInvoiceInfoByRegID(string RegID, int Type, out DataTable dt)
        {
            return proxy.Service.m_lngGetInvoiceByRegID(RegID, Type, out dt);
        }
        #endregion

        #region ��ѯ�շ���Ŀ
        /// <summary>
        /// ��ѯ�շ���Ŀ
        /// </summary>
        /// <param name="FindStr"></param>
        /// <param name="PatType">�������</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngFindChargeItem(string FindStr, string PatType, out DataTable dt, bool isChildPrice)
        {
            return proxy.Service.m_lngFindChargeItem(FindStr, PatType, out dt, isChildPrice);
        }

        /// <summary>
        /// ������ĿID�����շ���Ŀ
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngFindChargeItem(string ItemID, out DataTable dt, bool isChildPrice)
        {
            return proxy.Service.m_lngFindChargeItem(ItemID, out dt, isChildPrice);
        }
        #endregion

        #region ֱ���շ����ɷ�����ϸ
        /// <summary>
        /// ֱ���շ����ɷ�����ϸ
        /// </summary>
        /// <param name="OrderDicArr">��������Ŀ����</param>
        /// <param name="PatientChargeArr">������ϸ����</param>
        /// <param name="Type">8 ֱ�� 9 ������</param>
        /// <param name="OrderID">���صķ���ID��(��ѽҽ�����ֶ�)</param>
        /// <returns></returns>        
        public long m_lngGenPatientChargeByDir(List<clsBihOrderDic_VO> OrderDicArr, List<clsBihPatientCharge_VO> PatientChargeArr, int Type, ref string OrderID)
        {
            return proxy.Service.m_lngGenPatientChargeByDir(OrderDicArr, PatientChargeArr, Type, ref OrderID);
        }
        #endregion

        #region ����סԺ�Ǽ���ˮ�Ż�ȡ����Ч����(����״̬)������Ϣ
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�Ż�ȡ����Ч����(����״̬)������Ϣ
        /// </summary>
        /// <param name="RegID">סԺ�Ǽ���ˮ��</param>
        /// <param name="ActiveType">��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�;888=����״̬����;999=ȫ��}</param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetFeeItemByActiveType(string RegID, int ActiveType, string Pstatus, string AreaID, string BeginDate, string EndDate, out DataTable dt)
        {
            long l = proxy.Service.m_lngGetFeeItemByActiveType(RegID, ActiveType, Pstatus, AreaID, BeginDate, EndDate, out dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                #region Lis
                DataRow[] drr = null;
                DataTable dtRpt = proxy.Service.GetFeeItemByActiveTypeLis(dt);
                if (dtRpt != null && dtRpt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRpt.Rows)
                    {
                        drr = dt.Select("orderid_chr = '" + dr["orderId"].ToString() + "'");
                        if (drr != null && drr.Length > 0)
                        {
                            foreach (DataRow dr1 in drr)
                            {
                                dr1["rptStatus"] = 1;
                            }
                        }
                    }
                    dt.AcceptChanges();
                }
                #endregion

                #region Pacs
                try
                {
                    List<string> lstOrderId = proxy.Service.GetFeeItemByActiveTypePacs(dt);
                    if (lstOrderId.Count > 0)
                    {
                        foreach (string orderId in lstOrderId)
                        {
                            drr = dt.Select("orderid_chr = '" + orderId + "'");
                            if (drr != null && drr.Length > 0)
                            {
                                foreach (DataRow dr in drr)
                                {
                                    dr["rptStatus"] = 1;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("����Ӱ�����ݿ�(PACS)ʧ�ܣ������жϼ�����״̬��" + Environment.NewLine + ex.Message);
                }
                #endregion
            }

            return l;
        }
        #endregion

        #region ����ҽ��ID(ֱ�շ���ID)��ȡ������ϸ
        /// <summary>
        /// ����ҽ��ID(ֱ�շ���ID)��ȡ������ϸ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientChargeByID(string ID, out DataTable dt)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngGetPatientChargeByID(ID, out dt);
        }
        #endregion

        #region ��ȡ�շ���ĿĬ��ִ�еص�
        /// <summary>
        /// ��ȡ�շ���ĿĬ��ִ�еص�
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ApplyAreaID"></param>
        /// <returns></returns>        
        public string m_strGetChargeItemDefaultExecAreaID(string ItemID, string ApplyAreaID, out string ExecAreaName)
        {
            return proxy.Service.m_strGetChargeItemDefaultExecAreaID(ItemID, ApplyAreaID, out ExecAreaName);
        }
        #endregion

        #region �ύ�����ʷ�����ϸ
        /// <summary>
        /// �ύ�����ʷ�����ϸ
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>        
        public long m_lngCommitPatchCharge(string OrderID, string RegID, string OperID, int Type)
        {
            return proxy.Service.m_lngCommitPatchCharge(OrderID, RegID, OperID, Type);
        }
        #endregion

        #region �����ڹ���
        /// <summary>
        /// �����ڹ���
        /// </summary>
        /// <param name="ExecDate">����ʱ��(��ʽ��yyy-mm-dd hh:mm:ss</param>
        /// <param name="FeeDate">����ʱ��(��ʽ��yyy-mm-dd hh:mm:ss</param>
        /// <param name="OperID">����ԱID</param>   
        /// <param name="RegID">���˹���ʱ����Ժ�Ǽ�ID</param>  
        /// <param name="ExecType">1 ����ҹ�� 2 ��Ժ����</param>
        public long AutoCharge(string ExecDate, string FeeDate, string OperID, string RegID, int ExecType)
        {
            //com.digitalwave.iCare.middletier.HIS.clsAutoCharge objSvc =
            //                                               (com.digitalwave.iCare.middletier.HIS.clsAutoCharge)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAutoCharge));            

            //long l = proxy.Service.AutoCharge(ExecDate, OperID);
            //proxy.Service.Dispose();

            //com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS objSvc =
            //                                               (com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS));

            com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS objSvc = new com.digitalwave.iCare.middletier.HIS.clsAutoCharge_CS();
            long l = objSvc.AutoCharge(ExecDate, FeeDate, OperID, clsPublic.m_intGetSysParm("1013"), clsPublic.m_intGetSysParm("1014"), RegID, ExecType);
            objSvc = null;

            return l;
        }
        #endregion

        #region ��Ժ��ȡ�����Է���
        /// <summary>
        /// ��Ժ��ȡ�����Է���
        /// </summary>
        /// <param name="FeeDate">����ʱ��(��ʽ��yyy-mm-dd hh:mm:ss)</param>
        /// <param name="OperID">����ԱID</param>
        /// <param name="RegID">��Ժ�Ǽ�ID</param>
        /// <returns></returns>
        public long AutoChargeContinueItem(string FeeDate, string OperID, string RegID)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.AutoChargeContinueItem(FeeDate, OperID, RegID);
        }
        #endregion

        #region ��ȡ����������Ϣ
        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long GetDayAccountsInfo(out DataTable dt)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.GetDayAccountsInfo(out dt);
        }
        #endregion

        #region ��ȡ�����������ʱ��
        /// <summary>
        /// ��ȡ�����������ʱ��
        /// </summary>
        /// <param name="RegID">��Ժ�Ǽ�ID</param>
        /// <returns></returns>        
        public string GetDayAccountsMaxDate(string RegID)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.GetDayAccountsMaxDate(RegID);
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="DayAccounts_VO">����VO</param>
        /// <param name="EmpID">����ԱID</param>       
        /// <param name="ChargeType">0 ��ͨ���� 1 ��Ժ���� 2 ��Ժ����</param>     
        /// <returns></returns>        
        public long m_lngBuildDayAccounts(clsBihDayAccounts_VO DayAccounts_VO, string EmpID, int ChargeType)
        {
            return proxy.Service.m_lngBuildDayAccounts(DayAccounts_VO, EmpID, ChargeType);
        }
        #endregion

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="PatientChargeArr">������ϸ����</param>        
        /// <param name="DayAccountID">����ID</param>                
        /// <returns></returns>        
        public long m_lngPatchDayAccount(List<clsBihPatientCharge_VO> PatientChargeArr, string DayAccountID)
        {
            return proxy.Service.m_lngPatchDayAccount(PatientChargeArr, DayAccountID);
        }
        #endregion

        #region ���淢Ʊ��ӡ����
        /// <summary>
        /// ���淢Ʊ��ӡ����
        /// </summary>
        /// <param name="ChargeItemCatArr">���÷�������VO</param>
        /// <param name="Scope">��Χ: 1 ������� 2 ���﷢Ʊ 3 סԺ���� 4 סԺ��Ʊ</param>
        /// <returns></returns>        
        public long m_lngSaveInvoiceSet(List<clsBihChargeItemCat_VO> ChargeItemCatArr, string Scope)
        {
            return proxy.Service.m_lngSaveInvoiceSet(ChargeItemCatArr, Scope);
        }
        #endregion

        #region �տ�Ա�ս�(��Ʊ+����)
        /// <summary>
        /// �տ�Ա�ս�(��Ʊ+����)
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <param name="RecType">0 ȫ�� 1 ��Ʊ 2 ����</param>
        /// <returns></returns>        
        public long m_lngDayReckoningUnion(string EmpID, string RecDate, string RemarkInfo, int RecType)
        {
            return proxy.Service.m_lngDayReckoningUnion(EmpID, RecDate, RemarkInfo, RecType);
        }
        #endregion

        #region �տ�Ա�ս�(��Ʊ)
        /// <summary>
        /// �տ�Ա�ս�(��Ʊ)
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngDayReckoning(string EmpID, string RecDate, string RemarkInfo)
        {
            return proxy.Service.m_lngDayReckoning(EmpID, RecDate, RemarkInfo);
        }
        #endregion

        #region �տ�Ա�ս�(����)
        /// <summary>
        /// �տ�Ա�ս�(����)
        /// </summary>
        /// <param name="EmpID">�տ�ԱID</param>
        /// <param name="RecDate"></param>  
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngDayReckoningPre(string EmpID, string RecDate, string RemarkInfo)
        {
            return proxy.Service.m_lngDayReckoningPre(EmpID, RecDate, RemarkInfo);
        }
        #endregion

        #region ��ȡ�տ�Ա�ս�ʱ���б�
        /// <summary>
        /// ��ȡ�տ�Ա�ս�ʱ���б�
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetDayReckoningTime(string EmpID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngGetDayReckoningTime(EmpID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region (ҽ��)����סԺ�շ����ݵ�ҽ��ǰ�û�
        /// <summary>
        /// (ҽ��)����סԺ�շ����ݵ�ҽ��ǰ�û�
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngSendybdata(string DSN, List<clsYB_VO> objYBArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsZyYB objSvc =
            //                                                   (com.digitalwave.iCare.middletier.HIS.clsZyYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsZyYB));

            //clsZyYB objSvc = new clsZyYB();

            return (new weCare.Proxy.ProxyIP02()).Service.m_lngSendybdata(DSN, objYBArr);
        }

        /// <summary>
        /// (ҽ��)����סԺ�շ����ݵ�ҽ��ǰ�û�
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngSendybdata(string DSN, DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsZyYB objSvc =
            //                                             (com.digitalwave.iCare.middletier.HIS.clsZyYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsZyYB));

            //clsZyYB objSvc = new clsZyYB();

            return (new weCare.Proxy.ProxyIP02()).Service.m_lngSendybdata(DSN, dt);
        }
        #endregion

        #region (ҽ��)��ѯ�����շ���Ŀ�Ƿ�ɹ�
        /// <summary>
        /// (ҽ��)��ѯ�����շ���Ŀ�Ƿ�ɹ�
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="Hospcode"></param>
        /// <param name="ZYNo"></param>
        /// <param name="ZYSno"></param>
        /// <returns></returns>
        public bool m_blnCheckSendRes(string DSN, string Hospcode, string ZYNo, string ZYSno)
        {
            //com.digitalwave.iCare.middletier.HIS.clsZyYB objSvc =
            //                                                   (com.digitalwave.iCare.middletier.HIS.clsZyYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsZyYB));

            //clsZyYB objSvc = new clsZyYB();

            return (new weCare.Proxy.ProxyIP02()).Service.m_blnCheckSendRes(DSN, Hospcode, ZYNo, ZYSno);
        }
        #endregion

        #region (ҽ��)����ʱHIS����ʧ�ܣ��ֹ�ɾ����������
        /// <summary>
        /// (ҽ��)����ʱHIS����ʧ�ܣ��ֹ�ɾ����������
        /// </summary>
        /// <param name="billno"></param>
        /// <returns></returns>
        public long m_lngDelybdata(string DSN, string ZYNo, string ZYSno)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngDelybdata(DSN, ZYNo, ZYSno);
        }
        #endregion

        #region (ҽ��)��ȡҽ��������ϸ
        /// <summary>
        /// (ҽ��)��ȡҽ��������ϸ
        /// </summary>
        /// <param name="DSN"></param>        
        /// <param name="Hospcode"></param>        
        /// <param name="ZYNo"></param>
        /// <param name="ZYSno"></param>
        /// <param name="YbType">1 ��ͨ 2 ����Ա</param>
        /// <returns></returns>      
        public long m_lngGetybjsmx(string DSN, string Hospcode, string ZYNo, string ZYSno, out DataTable dtRecord, out int YbType)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngGetybjsmx(DSN, Hospcode, ZYNo, ZYSno, out dtRecord, out YbType);
        }
        #endregion

        #region (ҽ������)��������
        /// <summary>
        /// (ҽ������)��������
        /// </summary>
        /// <param name="HospCode"></param>
        /// <param name="RegID"></param>
        /// <param name="Mode">1 ģʽһ��ȫ��δ����Ŀ 2 ģʽ����ָ����Ŀ</param>
        /// <returns></returns>        
        public long m_lngBudgetSendData(string HospCode, string RegID, int Mode)
        {
            return proxy.Service.m_lngBudgetSendData(HospCode, RegID, Mode);
        }
        #endregion

        #region (ҽ������)��������
        /// <summary>
        /// (ҽ������)��������
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <returns></returns>        
        public long m_lngBudgetGetData(string RegID, out DataTable dtMain, out DataTable dtDet)
        {
            return proxy.Service.m_lngBudgetGetData(RegID, out dtMain, out dtDet);
        }
        #endregion

        #region (ҽ��)����ҽ��ǰ�û�����
        /// <summary>
        /// (ҽ��)����ҽ��ǰ�û�����
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="Hospcode"></param>
        /// <param name="ZYNo"></param>
        /// <param name="ZYSno"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngDownloadYBData(string DSN, string Hospcode, string ZYNo, string ZYSno, out DataTable dt)
        {
            //com.digitalwave.iCare.middletier.HIS.clsZyYB objSvc =
            //                                             (com.digitalwave.iCare.middletier.HIS.clsZyYB)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsZyYB));

            //clsZyYB objSvc = new clsZyYB();

            return (new weCare.Proxy.ProxyIP02()).Service.m_lngDownloadYBData(DSN, Hospcode, ZYNo, ZYSno, out dt);
        }
        #endregion

        #region (ҽ��)����ҽ��ǰ�û�����->���ɵ�����
        /// <summary>
        /// (ҽ��)����ҽ��ǰ�û�����->���ɵ�����
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngDownloadYBData(DataTable dt)
        {
            return proxy.Service.m_lngDownloadYBData(dt);
        }
        #endregion

        #region (ҽ��)ɾ��������ҽ��ǰ�û�����
        /// <summary>
        /// (ҽ��)ɾ��������ҽ��ǰ�û�����
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <returns></returns>        
        public long m_lngDelDownloadYBData(string Zyh, int Zycs)
        {
            return proxy.Service.m_lngDelDownloadYBData(Zyh, Zycs);
        }
        #endregion

        #region (ҽ��)��ȡ������ҽ��ǰ�û�����
        /// <summary>
        /// (ҽ��)��ȡ������ҽ��ǰ�û�����
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetDownloadYBData(string Zyh, int Zycs, out DataTable dt)
        {
            return proxy.Service.m_lngGetDownloadYBData(Zyh, Zycs, out dt);
        }
        #endregion

        #region ҽ������
        /// <summary>
        /// ҽ������
        /// </summary>
        /// <param name="HospCode"></param>
        /// <param name="RegID"></param>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <param name="TotalMoney"></param>
        /// <param name="InsuredMoney"></param>
        /// <param name="OutErrMsg"></param>
        /// <returns></returns>        
        public long m_lngYBBudget(string HospCode, string RegID, string Zyh, int Zycs, out decimal TotalMoney, out decimal InsuredMoney, out string OutErrMsg)
        {
            long lngRes = proxy.Service.m_lngYBBudget(HospCode, RegID, Zyh, Zycs, out TotalMoney, out InsuredMoney, out OutErrMsg);
            return proxy.Service.m_lngZYSS(HospCode, Zyh, Zycs, out TotalMoney, out InsuredMoney, out OutErrMsg);
        }
        #endregion

        #region (��ɽҽ��)����DBF�ļ�
        /// <summary>
        /// (��ɽҽ��)����DBF�ļ�
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>  
        public long m_lngCreateDBF(string DSN, string DbfName, ArrayList objYBArr)
        {
            com.digitalwave.iCare.middletier.HIS.clsChaShan objSvc = new middletier.HIS.clsChaShan();
            //(com.digitalwave.iCare.middletier.HIS.clsChaShan)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChaShan));

            return objSvc.m_lngCreateDbf(DSN, DbfName, objYBArr);
        }
        #endregion

        #region (ҽ��)��ȡ���
        /// <summary>
        /// (ҽ��)��ȡ���
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetResult(string DSN, string DbfName, out DataTable dtRecord)
        {
            com.digitalwave.iCare.middletier.HIS.clsChaShan objSvc = new middletier.HIS.clsChaShan();
            //(com.digitalwave.iCare.middletier.HIS.clsChaShan)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChaShan));

            return objSvc.m_lngGetResult(DSN, DbfName, out dtRecord);
        }
        #endregion

        #region ����������Ŀ
        /// <summary>
        /// ����������Ŀ
        /// </summary>
        /// <param name="ID"></param>        
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngFindOrderByID(string ID, out DataTable dt, bool isChildPrice)
        {
            return proxy.Service.m_lngFindOrderByID(ID, out dt, isChildPrice);
        }
        #endregion

        #region ����������Ŀ��ȡ�շ���Ŀ
        /// <summary>
        /// ����������Ŀ��ȡ�շ���Ŀ
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="PayTypeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetChargeItemByOrderID(string OrderID, string PayTypeID, out DataTable dt, bool isChildPrice)
        {
            return proxy.Service.m_lngGetChargeItemByOrderID(OrderID, PayTypeID, out dt, isChildPrice);
        }
        #endregion

        #region �ж�������Ŀ(���)�Ƿ��������
        /// <summary>
        /// �ж�������Ŀ(���)�Ƿ��������
        /// </summary>
        /// <param name="OrderID">������ĿID</param>
        /// <param name="InvoCatArr">��Ʊ��������</param>
        /// <param name="SysType">ϵͳ 1 ���� 2 סԺ</param>
        /// <param name="ItemNums">��Ŀ����</param>
        /// <returns></returns>        
        public bool m_blnCheckOrderDiscount(string OrderID, List<string> InvoCatArr, int SysType, int ItemNums)
        {
            return proxy.Service.m_blnCheckOrderDiscount(OrderID, InvoCatArr, SysType, ItemNums);
        }
        #endregion

        #region ��ȡ�����ʡ�ֱ�շ���(������Ŀ)�����¼
        /// <summary>
        /// ��ȡ�����ʡ�ֱ�շ���(������Ŀ)�����¼
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetOrderDic(string OrderID, out DataTable dt)
        {
            return proxy.Service.m_lngGetOrderDic(OrderID, out dt);
        }
        #endregion

        #region (ҽ��)����ҽ��ͳ�����
        /// <summary>
        /// (ҽ��)����ҽ��ͳ�����
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="InsuredSum"></param>
        /// <returns></returns>        
        public long m_lngUpdateInsuredSum(string RegID, decimal InsuredSum)
        {
            return proxy.Service.m_lngUpdateInsuredSum(RegID, InsuredSum);
        }
        #endregion

        #region ��Ժ�����������ڳ���λ
        /// <summary>
        /// ��Ժ�����������ڳ���λ
        /// </summary>
        /// <param name="RegID"></param>
        /// <returns></returns>        
        public long m_lngClearBed(string RegID)
        {
            return proxy.Service.m_lngClearBed(RegID);
        }
        #endregion

        #region ��¼��ǰ�շ�Աʹ��֮��Ʊ��
        /// <summary>
        /// ��¼��ǰ�շ�Աʹ��֮��Ʊ��
        /// </summary>
        /// <param name="OperID"></param>
        /// <param name="InvoNo"></param>
        /// <param name="Type">���ͣ� 1 סԺ��Ʊ 2 Ѻ�� 3 ���﷢Ʊ</param>
        /// <returns></returns>        
        public long m_lngRegOperInvoNO(string OperID, string InvoNo, int Type)
        {
            return proxy.Service.m_lngRegOperInvoNO(OperID, InvoNo, Type);
        }
        #endregion

        #region ��ȡ��ǰ�շ�Աʹ��֮��Ʊ��
        /// <summary>
        /// ��ȡ��ǰ�շ�Աʹ��֮��Ʊ��
        /// </summary>
        /// <param name="OperID"></param>
        /// <param name="Type">���ͣ� 1 סԺ��Ʊ 2 Ѻ�� 3 ���﷢Ʊ</param>
        /// <param name="InvoNo"></param>
        /// <returns></returns>        
        public long m_lngGetOperInvoNO(string OperID, int Type, out string InvoNo)
        {
            return proxy.Service.m_lngGetOperInvoNO(OperID, Type, out InvoNo);
        }
        #endregion

        #region ���Ӥ����
        /// <summary>
        /// ���Ӥ����
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngCheckBaby(string Zyh, out DataTable dt)
        {
            return proxy.Service.m_lngCheckBaby(Zyh, out dt);
        }
        #endregion

        #region �������˷�
        /// <summary>
        /// �������˷�
        /// </summary>
        /// <param name="ChargeIDArr"></param>
        /// <param name="EmpID"></param>
        /// <returns></returns>        
        public long m_lngPatchRefundment(List<clsBihRefCharge_VO> ChargeIDArr, string EmpID)
        {
            return proxy.Service.m_lngPatchRefundment(ChargeIDArr, EmpID);
        }
        #endregion

        #region �޸ı�ע��Ϣ
        /// <summary>
        /// �޸ı�ע��Ϣ
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngUpdateDayRecRemark(string EmpID, string RecDate, string RemarkInfo)
        {
            return proxy.Service.m_lngUpdateDayRecRemark(EmpID, RecDate, RemarkInfo);
        }
        #endregion

        #region ��ȡ��ͬ�ѱ�ķ�����ϸ
        /// <summary>
        /// ��ȡ��ͬ�ѱ�ķ�����ϸ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="PayTypeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientFeeDetByPayType(string RegID, string PayTypeID, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientFeeDetByPayType(RegID, PayTypeID, out dt);
        }
        #endregion

        #region ������ϸ��Ӧ֮������Ŀ
        /// <summary>
        /// ������ϸ��Ӧ֮������Ŀ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="ActiveType">��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�;888=����״̬����;999=ȫ��}</param>
        /// <param name="Pstatus"></param>
        /// <param name="AreaID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetFeeDiagItem(string RegID, int ActiveType, string Pstatus, string AreaID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngGetFeeDiagItem(RegID, ActiveType, Pstatus, AreaID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region ���ݷ�����ϸID���ҷ�����Ϣ
        /// <summary>
        /// ���ݷ�����ϸID���ҷ�����Ϣ
        /// </summary>
        /// <param name="DiagArr"></param>
        /// <param name="dtNormal"></param>
        /// <param name="dtRefundment"></param>
        /// <returns></returns>        
        public long m_lngGetFeeItemByActiveType(List<clsParmDiagItem_VO> DiagArr, out DataTable dtNormal, out DataTable dtRefundment)
        {
            return proxy.Service.m_lngGetFeeItemByActiveType(DiagArr, out dtNormal, out dtRefundment);
        }
        #endregion

        #region ���������ͻ�ȡ���˷��÷���
        /// <summary>
        /// ���������ͻ�ȡ���˷��÷���
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="DeptClass">1 ִ�п��� 2 �������� 3 ���ڲ���</param>
        /// <param name="Status">0 δ���ʽ��� 1 �Ѵ��ʽ���</param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetFeeCatByDeptClass(string RegID, int DeptClass, int Status, out DataTable dt)
        {
            return proxy.Service.m_lngGetFeeCatByDeptClass(RegID, DeptClass, Status, out dt);
        }
        #endregion

        #region ���������ͻ�ȡ���˷��÷���ĸӤ�ϲ�����ʹ�� by yibing.zheng 09-07-04

        /// <summary>
        /// ���������ͻ�ȡ���˷��÷���ĸӤ�ϲ�����ʹ��
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="DeptClass">1 ִ�п��� 2 �������� 3 ���ڲ���</param>
        /// <param name="Status">0 δ���ʽ��� 1 �Ѵ��ʽ���</param>
        /// <param name="dt"></param>
        /// <returns></returns>

        public long m_lngGetFeeCatByDeptClassForMortherBaby(string RegID, int DeptClass, int Status, out DataTable dt)
        {
            return proxy.Service.m_lngGetFeeCatByDeptClassForMortherBaby(RegID, DeptClass, Status, out dt);
        }
        #endregion

        #region ���ʽ���
        /// <summary>
        /// ���ʽ���
        /// </summary>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeCatArr"></param>
        /// <param name="FactTotalMoney">ʵ��δ�ᡢδ���ܽ��</param>
        /// <param name="FactPreMoney">ʵ�ʷ�̯�ܽ��</param>
        /// <param name="DiffValDeptID">��ֵ�����ID</param>
        /// <param name="DiffValCatID">��ֵ��������ID</param>
        /// <param name="IsHavePrepayMoney">�Ƿ���Ԥ���� (true �� false ��)</param>
        /// <param name="ChargeNo"></param>
        /// <returns></returns>        
        public long m_lngBadCharge(clsBihCharge_VO Charge_VO, List<clsBihChargeCat_VO> ChargeCatArr, clsBihInvoice_VO Invoice_VO, List<clsBihInvoiceCat_VO> InvoCatArr, List<clsBihPayment_VO> PaymentArr, decimal FactTotalMoney, decimal FactPreMoney, string DiffValDeptID, string DiffValCatID, bool IsHavePrepayMoney, out string ChargeNo)
        {
            return proxy.Service.m_lngBadCharge(Charge_VO, ChargeCatArr, Invoice_VO, InvoCatArr, PaymentArr, FactTotalMoney, FactPreMoney, DiffValDeptID, DiffValCatID, IsHavePrepayMoney, out ChargeNo);
        }
        #endregion

        #region ��ȡ���ʲ���δ�����
        /// <summary>
        /// ��ȡ���ʲ���δ�����
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetBadChargeFeeInfo(string RegID, out DataTable dt)
        {
            return proxy.Service.m_lngGetBadChargeFeeInfo(RegID, out dt);
        }

        /// <summary>
        /// ��ȡ���ʲ���δ�����(ĸӤ�ϲ��������)
        /// </summary>
        /// <param name="RegID">����ID</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetBadChargeFeeInfoMotherBaby(string RegID, out DataTable dt)
        {
            return proxy.Service.m_lngGetBadChargeFeeInfoMotherBaby(RegID, out dt);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeNo"></param>
        /// <returns></returns>        
        public long m_lngReckoning(clsBihCharge_VO Charge_VO, out string ChargeNo)
        {
            return proxy.Service.m_lngReckoning(Charge_VO, out ChargeNo);
        }
        #endregion

        #region ��ȡ�������д���ID
        /// <summary>
        /// ��ȡ�������д���ID
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngMzGetAcctRecipeID(string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngMzGetAcctRecipeID(BeginDate, EndDate, out dt);
        }
        #endregion

        #region �������շѴ���ID��ȡ�������
        /// <summary>
        /// �������շѴ���ID��ȡ�������
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngMzGetRecipeCat(string RecipeID, out DataTable dt)
        {
            return proxy.Service.m_lngMzGetRecipeCat(RecipeID, out dt);
        }
        #endregion

        #region ����SEQID��ȡ�������
        /// <summary>
        /// ����SEQID��ȡ�������
        /// </summary>
        /// <param name="SeqID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngMzGetChargeCat(string SeqID, out DataTable dt)
        {
            return proxy.Service.m_lngMzGetChargeCat(SeqID, out dt);
        }
        #endregion

        #region ����SEQID��CatID���º������
        /// <summary>
        /// ����SEQID��CatID���º������
        /// </summary>
        /// <param name="SeqID"></param>
        /// <param name="CatIDArr"></param>
        /// <param name="CatSumArr"></param>
        /// <returns></returns>        
        public long m_lngMzUpdateChargeCat(string SeqID, List<string> CatIDArr, List<decimal> CatSumArr, string PStatus)
        {
            return proxy.Service.m_lngMzUpdateChargeCat(SeqID, CatIDArr, CatSumArr, PStatus);
        }
        #endregion

        #region ���ݴ���ID��ȡSEQID�б�
        /// <summary>
        /// ���ݴ���ID��ȡSEQID�б�
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngMzGetSeqIDList(string RecipeID, out DataTable dt)
        {
            return proxy.Service.m_lngMzGetSeqIDList(RecipeID, out dt);
        }
        #endregion

        #region <����>��Ʊ��Ϣ
        /// <summary>
        /// (����)��Ʊ��Ϣ 
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns>        
        public long m_lngGetOPInvoiceByChargeNo(string ChargeNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            return proxy.Service.m_lngGetOPInvoiceByChargeNo(ChargeNo, out dtMain, out dtDet, out dtPayMode);
        }

        /// <summary>
        /// ���﷢Ʊ��Ϣ 
        /// </summary>
        /// <param name="InvoNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns> 
        public long m_lngGetOPInvoiceByInvoNo(string InvoNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            return proxy.Service.m_lngGetOPInvoiceByInvoNo(InvoNo, out dtMain, out dtDet, out dtPayMode);
        }

        /// <summary>
        /// ���﷢Ʊ��Ϣ(for ��Ʊ)
        /// </summary>
        /// <param name="mode">ģʽ(������ʶ��) 0-��Ʊ</param>
        /// <param name="InvoNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns>
        public long m_lngGetOPInvoiceByInvoNo(int mode, string InvoNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            return proxy.Service.m_lngGetOPInvoiceByInvoNo(mode, InvoNo, out dtMain, out dtDet, out dtPayMode);
        }
        #endregion

        #region ����̨ɽҽ������
        /// <summary>
        /// ���������ϸ
        /// </summary>
        /// <param name="p_strlsh0"></param>
        /// <param name="p_inpatientid"></param>
        /// <returns></returns>
        public long m_lngInsertRegisterCharge(string p_strlsh0, string p_inpatientid)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngInsertRegisterCharge(p_strlsh0, p_inpatientid);
        }
        /// <summary>
        /// ���벡����Ϣ
        /// </summary>
        /// <param name="p_strlsh0"></param>
        /// <param name="p_inpatientid"></param>
        /// <returns></returns>
        public long m_lngInsertRegister(string p_strlsh0, string p_inpatientid)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngInsertRegister(p_strlsh0, p_inpatientid);
        }
        /// <summary>
        /// ��ȡҽ��֧���Ľ��
        /// </summary>
        /// <param name="p_strlsh0"></param>
        /// <param name="p_strYBpay"></param>
        /// <returns></returns>
        public long m_lngGetYBpay(string p_strlsh0, out string p_strMedno, out string p_strYBpay)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngGetYBpay(p_strlsh0, out p_strMedno, out p_strYBpay);
        }
        /// <summary>
        /// ɾ����HIS�ϴ���Ϣ
        /// </summary>
        /// <param name="p_registerid"></param>
        public long m_lngDelYBInfo(string p_strlsh0)
        {
            return (new weCare.Proxy.ProxyIP02()).Service.m_lngDelYBInfo(p_strlsh0);
        }
        #endregion

        #region ���Ĳ��˷��ú˶�״̬
        /// <summary>
        /// ���Ĳ��˷��ú˶�״̬
        /// </summary>
        /// <param name="RegisterID"></param>
        /// <param name="CheckStatus"></param>
        public long m_lngUpdatePatientChargeCheckStatus(string RegisterID, string CheckStatus)
        {
            return proxy.Service.m_lngUpdatePatientChargeCheckStatus(RegisterID, CheckStatus);
        }
        #endregion

        #region ����Ӥ��δ�����
        /// <summary>
        /// ����Ӥ��δ�����
        /// </summary>
        /// <param name="p_strRegisterId">Ӥ��ID</param>
        /// <param name="p_dtbResult">���ؽ��</param>
        /// <returns></returns>
        public long m_lngCheckBabyNoPayCharge(string p_strRegisterId, out DataTable p_dtbResult)
        {
            return proxy.Service.m_lngCheckBabyNoPayCharge(p_strRegisterId, out p_dtbResult);
        }
        #endregion

        #region ����ĸ��ID��ȡӤ����Ϣ

        /// <summary>
        /// ����ĸ��ID��ȡӤ����Ϣ
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtbBabyInfo"></param>
        /// <returns></returns>
        public long m_lngGetBabyRegisterId(string p_strRegisterId, out DataTable p_dtbBabyInfo)
        {
            return proxy.Service.m_lngGetBabyRegisterId(p_strRegisterId, out p_dtbBabyInfo);
        }
        #endregion

        #region ��ȡ�����б�
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetDepts(out DataTable dtbResult)
        {
            return proxy.Service.m_lngGetDepts(out dtbResult);
        }
        #endregion

        #region ��ɽҽ���ϴ� ��Ӧ֢
        public long m_lngCheckChangeSFLB(Dictionary<string, int> p_gdicItemIDs, out Dictionary<string, string> p_gdicItemIDResult)
        {
            return proxy.Service.m_lngCheckChangeSFLB(p_gdicItemIDs, out p_gdicItemIDResult);
        }


        public long m_lngGetSFLB_ForZjwsy(out DataTable dtResult)
        {
            return proxy.Service.m_lngGetSFLB_ForZjwsy(out dtResult);
        }

        public long m_lngGetPatientChargeSFLB(List<string> m_glstPChargeID,
                                              out Dictionary<string, string> p_gdicItemIDResult,
                                              out Dictionary<string, decimal> p_gdicPatchAmount,
                                              out Dictionary<string, List<string>> p_gdicPatchList)
        {
            return proxy.Service.m_lngGetPatientChargeSFLB(m_glstPChargeID, out p_gdicItemIDResult, out p_gdicPatchAmount, out p_gdicPatchList);
        }



        public long m_lngSetChargeSFLB_Zjwsy(List<clsSFLB_log> m_glstSFLB, string p_strEmpID, string p_strEmpName)
        {
            return proxy.Service.m_lngSetChargeSFLB_Zjwsy(m_glstSFLB, p_strEmpID, p_strEmpName);
        }


        public long m_lngGetPatientPayTypeSFLBBH(string p_strPayType, out string p_strPayNo)
        {
            return proxy.Service.m_lngGetPatientPayTypeSFLBBH(p_strPayType, out p_strPayNo);
        }

        #endregion

        #region ��Ŀ��Ӧ֢
        ///// <summary>
        ///// ��Ŀ��Ӧ֢
        ///// </summary>
        ///// <param name="strRegID"></param>
        ///// <param name="dtResult"></param>
        ///// <returns></returns>
        //public long m_lngGetItemShiying(string strRegID, out DataTable dtResult)
        //{
        //    com.digitalwave.iCare.middletier.HIS.clsCommonQuery objSvc =
        //                                                    (com.digitalwave.iCare.middletier.HIS.clsCommonQuery)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsCommonQuery));

        //    long l = proxy.Service.m_lngGetItemShiying(strRegID, out dtResult);
        //    proxy.Service.Dispose();

        //    return l;
        //}
        #endregion

        #region ͨ����ˮ�Ų�ѯ����������Ĳ�����˼�¼
        /// <summary>
        /// ͨ����ˮ�Ų�ѯ����������Ĳ�����˼�¼
        /// </summary>
        /// <param name="p_strIpno"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQueryOpExtraChargeByRgno(string p_strIpno, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = null;

            #region �м������ 
            try
            {
                lngRes = proxy.Service.m_lngQueryOpExtraChargeByRgno(p_strIpno, out p_dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ͨ����ˮ�Ų�ѯ������������Ϣ
        /// <summary>
        /// ͨ����ˮ�Ų�ѯ������������Ϣ
        /// </summary>
        /// <param name="p_strIpno"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        public long m_lngQuerySMDetailByRgno(string p_strRgno, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = null;

            #region �м������ 
            try
            {
                lngRes = proxy.Service.m_lngQuerySMDetailByRgno(p_strRgno, out p_dtResult);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region �����������뵥�޸ı�
        /// <summary>
        /// �����������뵥�޸ı�
        /// </summary>
        /// <param name="p_strRgno"></param>
        /// <param name="p_strOpreationName"></param>
        /// <param name="p_strANAName"></param>
        /// <param name="p_strANADate"></param>
        /// <param name="p_strEmployID"></param>
        /// <param name="p_strEmployName"></param>
        /// <returns></returns>
        public long m_lngUpdateRequisitionMR(string p_strRgno, string p_strOpreationName, string p_strANAName, string p_strANADate, string p_strEmployID, string p_strEmployName)
        {
            long lngRes = 0;

            #region �м������ 
            try
            {
                lngRes = proxy.Service.m_lngUpdateRequisitionMR(p_strRgno, p_strOpreationName, p_strANAName, p_strANADate, p_strEmployID, p_strEmployName);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
            }
            #endregion
            return lngRes;
        }
        #endregion

        #region ����籣�����籣����û�гɹ��Ͳ�����HIS����
        /// <summary>
        /// ����籣�����籣����û�гɹ��Ͳ�����HIS����
        /// </summary>
        /// <param name="p_registerID"></param>
        /// <returns></returns>
        public bool m_blnCheckYBChargeSuccessFull(string p_registerID)
        {
            bool blnSucc = false;

            #region �м������ 
            try
            {
                blnSucc = proxy.Service.m_blnCheckYBChargeSuccessFull(p_registerID);
            }
            catch (Exception exp)
            {
                Utility.clsLogText objLogger = new Utility.clsLogText();
                objLogger.LogError("�����м�������쳣��" + exp.Message);
            }
            finally
            {
            }
            #endregion
            return blnSucc;
        }
        #endregion

        #region ��˷���

        #region ��ȡ���߷��������Ϣ
        /// <summary>
        /// ��ȡ���߷��������Ϣ
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public DataTable GetPatientCheckFee(string registerId)
        {
            return proxy.Service.GetPatientCheckFee(registerId);
        }
        #endregion

        #region ���滼�߷��������Ϣ
        /// <summary>
        /// ���滼�߷��������Ϣ
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        public int SavePatientCheckFee(string registerId, string operId)
        {
            return proxy.Service.SavePatientCheckFee(registerId, operId);
        }
        #endregion

        #region ȡ�����߷��������Ϣ
        /// <summary>
        /// ȡ�����߷��������Ϣ
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        public int CancelPatientCheckFee(string registerId, string operId)
        {
            return proxy.Service.CancelPatientCheckFee(registerId, operId);
        }
        #endregion

        #endregion

        #region �ѷ�(��)�ڷ�ҩ�������˷�
        /// <summary>
        /// �ѷ�(��)�ڷ�ҩ�������˷�
        /// </summary>
        /// <param name="pchargeId"></param>
        /// <returns></returns>
        public bool IsCanRefundment(string pchargeId)
        {
            return proxy.Service.IsCanRefundment(pchargeId);
        }
        #endregion

        #region ������ɫ����

        #region GetEmpInfo
        /// <summary>
        /// GetEmpInfo
        /// </summary>
        /// <param name="empNo"></param>
        /// <returns></returns>
        public DataTable GetEmpInfo(string empNo)
        {
            return proxy.Service.GetEmpInfo(empNo);
        }
        #endregion

        #region AddCaseRole
        /// <summary>
        /// AddCaseRole
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public int AddCaseRole(EntityLogSetCaseRole vo)
        {
            return proxy.Service.AddCaseRole(vo);
        }
        #endregion

        #region DelCaseRole
        /// <summary>
        /// DelCaseRole
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public int DelCaseRole(EntityLogSetCaseRole vo)
        {
            return proxy.Service.DelCaseRole(vo);
        }
        #endregion

        #region QueryCaseRole
        /// <summary>
        /// QueryCaseRole
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        public DataTable QueryCaseRole(string startDate, string endDate, string doctCode)
        {
            return proxy.Service.QueryCaseRole(startDate, endDate, doctCode);
        }
        #endregion

        #region ��ѯ��Ʊ�˿�ԭ��ģ��
        /// <summary>
        /// ��ѯ��Ʊ�˿�ԭ��ģ��
        /// </summary>
        /// <param name="flagId"></param>
        /// <returns></returns>
        public DataTable GetRefundReasonList(int flagId)
        {
            return proxy.Service.GetRefundReasonList(flagId);
        }
        #endregion

        #region ��ѯ��Ʊ�˿�ԭ��
        /// <summary>
        /// ��ѯ��Ʊ�˿�ԭ��
        /// </summary>
        /// <param name="flagId"></param>
        /// <param name="invoNo"></param>
        /// <returns></returns>
        public EntityInvoiceRefundReason GetInvoiceRefundReason(int flagId, string invoNo)
        {
            return proxy.Service.GetInvoiceRefundReason(flagId, invoNo);
        }
        #endregion

        #region ���淢Ʊ�˿�ԭ��
        /// <summary>
        /// ���淢Ʊ�˿�ԭ��
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public int SaveInvoiceRefundReason(EntityInvoiceRefundReason vo)
        {
            return proxy.Service.SaveInvoiceRefundReason(vo);
        }
        #endregion

        #endregion
    }
}
