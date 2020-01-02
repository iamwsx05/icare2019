using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ��������Domain��
    /// </summary>
    public class clsDcl_Report : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsDcl_Report()
        {
        }

        weCare.Proxy.ProxyIP02 proxy
        {
            get
            {
                return new weCare.Proxy.ProxyIP02();
            }
        }
        #endregion

        #region ���ݱ����Ż�ȡסԺ�������
        /// <summary>
        /// ���ݱ����Ż�ȡסԺ�������
        /// </summary>
        /// <param name="RptID">�Զ��屨��ID</param>
        /// <param name="Flag">3 סԺ������� 4 סԺ��Ʊ����</param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetCatIDByRptID(string RptID, int Flag, out DataTable dt)
        {
            return proxy.Service.m_lngGetCatIDByRptID(RptID, Flag, out dt);
        }
        #endregion

        #region ÿ���嵥
        /// <summary>
        /// ÿ���嵥(����Ϣ)
        /// </summary>
        /// <param name="AreaID">����ID</param>
        /// <param name="RegID">סԺ�Ǽ���ˮID</param>
        /// <param name="BillDate">�嵥����</param> 
        /// <param name="objEveryDayBill"></param> 
        /// <returns></returns>
        public long m_lngRptEveryDayBill(string RegID, string BillDate, out clsBihEveryDayBill_VO objEveryDayBill)
        {
            return proxy.Service.m_lngRptEveryDayBill(RegID, BillDate, out objEveryDayBill);
        }

        /// <summary>
        /// ÿ���嵥(�������)
        /// </summary> 
        /// <param name="ID">1-����ID 2-סԺ�Ǽ���ˮID 3-סԺ��</param>        
        /// <param name="BillDate">�嵥����</param>
        /// <param name="Type">���ͣ�1 ������ 2 ����λ 3 ��סԺ��</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngRptEveryDayBillFee(string ID, string BillDate, int Type, out DataTable dt)
        {
            return proxy.Service.m_lngRptEveryDayBillFee(ID, BillDate, Type, out dt);
        }

        /// <summary>
        /// ÿ���嵥 ----�����
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="BillDate"></param>
        /// <param name="Type"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngRptEveryDayBillCate(string ID, string BillDate, int Type, int ItemCodeType, out DataTable dt)
        {
            return proxy.Service.m_lngRptEveryDayBillCate(ID, BillDate, Type, ItemCodeType, out dt);
        }

        /// <summary>
        /// ÿ���嵥(������ϸ)
        /// </summary>
        /// <param name="ID">1-����ID 2-סԺ�Ǽ���ˮID 3-סԺ��</param>        
        /// <param name="BillDate">�嵥����</param>
        /// <param name="Type">���ͣ�1 ������ 2 ����λ 3 ��סԺ��</param>
        /// <param name="dt"></param>
        /// <param name="ItemCodeType">��Ŀ����ʹ������ 0 �����շѱ��� 1 ��Ŀ����</param>
        /// <returns></returns>
        public long m_lngRptEveryDayBillEntry(string ID, string BillDate, int Type, int ItemCodeType, out DataTable dt)
        {
            return proxy.Service.m_lngRptEveryDayBillEntry(ID, BillDate, Type, ItemCodeType, out dt);
        }

        public long m_lngRptGetBednoByAreaid(string p_strAreaid, out DataTable dtResult)
        {
            return proxy.Service.m_lngRptGetBednoByAreaid(p_strAreaid, out dtResult);
        }

        #endregion

        #region ȫԺ�ɿ�(��Ʊ)���౨��
        /// <summary>
        /// ȫԺ�ɿ�(��Ʊ)���౨��
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="RepType">0 �ɿ���� 1 ��Ʊ����</param>
        /// <param name="StatType">0 ���� 1 ���ܣ�����</param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <returns></returns>        
        public long m_lngRptIncomeClass(string BeginDate, string EndDate, int RepType, int StatType, out DataTable dtMain, out DataTable dtDet)
        {
            return proxy.Service.m_lngRptIncomeClass(BeginDate, EndDate, RepType, StatType, out dtMain, out dtDet);
        }
        #endregion

        #region �տ�Ա�ɿ��
        /// <summary>
        /// �տ�Ա�ɿ��
        /// </summary>
        /// <param name="EmpID">�շ�ԱID</param>
        /// <param name="IsRec">�Ƿ��ѽ���</param>
        /// <param name="RecTime">����ʱ��</param>
        /// <param name="dtCharge"></param>
        /// <param name="dtInvoice"></param>
        /// <param name="dtPayment"></param>
        /// <returns></returns>        
        public long m_lngRptReckoningEmp(string EmpID, bool IsRec, string RecTime, out DataTable dtCharge, out DataTable dtInvoice, out DataTable dtPayment, out DataTable dtPrepayChargeNo, out string RemarkInfo)
        {
            return proxy.Service.m_lngRptReckoningEmp(EmpID, IsRec, RecTime, out dtCharge, out dtInvoice, out dtPayment, out dtPrepayChargeNo, out RemarkInfo);
        }
        #endregion

        #region �տ�Ա�ɿ��(����)
        /// <summary>
        /// �տ�Ա�ɿ��(����)
        /// </summary>
        /// <param name="EmpID">�շ�ԱID</param>
        /// <param name="IsRec">�Ƿ��ѽ���</param>
        /// <param name="RecTime">����ʱ��</param>
        /// <param name="dtPrepay"></param>
        /// <param name="dtPrepayRepNo"></param>
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngRptReckoningEmpPre(string EmpID, bool IsRec, string RecTime, out DataTable dtPrepay, out DataTable dtPrepayRepNo, out string RemarkInfo)
        {
            return proxy.Service.m_lngRptReckoningEmpPre(EmpID, IsRec, RecTime, out dtPrepay, out dtPrepayRepNo, out RemarkInfo);
        }
        #endregion

        #region �շѴ��ɿ��
        /// <summary>
        /// �շѴ��ɿ��
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dtCharge"></param>
        /// <param name="dtPayment"></param>
        /// <param name="dtRemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngRptReckoningDept(string BeginDate, string EndDate, out DataTable dtCharge, out DataTable dtPayment, out DataTable dtRemarkInfo, out DataTable dtChargeno)
        {
            return proxy.Service.m_lngRptReckoningDept(BeginDate, EndDate, out dtCharge, out dtPayment, out dtRemarkInfo, out dtChargeno);
        }
        #endregion

        #region ��Ʊ��ϸ����
        /// <summary>
        /// ��Ʊ��ϸ����
        /// </summary>
        /// <param name="InvoiceNO">��Ʊ��</param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptInvoiceEntry(string InvoiceNO, out DataTable dt)
        {
            return proxy.Service.m_lngRptInvoiceEntry(InvoiceNO, out dt);
        }
        #endregion

        #region ��ȡסԺ�·�Ʊͳ������
        /// <summary>
        /// ��ȡסԺ�·�Ʊͳ������
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        public long m_lngGetBIHInvoiceStatData(string p_opratorId, string p_beginDate, string p_endDate, out DataTable p_dtbStat)
        {
            return proxy.Service.m_lngGetBIHInvoiceStatData(p_opratorId, p_beginDate, p_endDate, out p_dtbStat);
        }

        /// <summary>
        /// ��ȡסԺ��Ʊ�ش�����
        /// </summary>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtbRePrint"></param>
        /// <returns></returns>
        public long m_lngGetBillRePrintData(string p_strOperatorId,
                                            string p_strStartDate,
                                            string p_strEndDate,
                                            out DataTable p_dtbRePrint)
        {
            return proxy.Service.m_lngGetBIHBillReprintByDate(p_strOperatorId, p_strStartDate, p_strEndDate, out p_dtbRePrint);
        }

        #region ������н���Ա����
        public long m_lngGetRecEmp(out DataTable p_dtbRecEmp)
        {
            return proxy.Service.m_lngGetRecEmp(out p_dtbRecEmp);
        }
        #endregion

        #endregion //��ȡ�Һŷ�Ʊͳ������

        #region ʵ����ϸ��־(��Ʊ��ϸ)
        /// <summary>
        /// ʵ����ϸ��־(��Ʊ��ϸ)
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OperCode"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dtInvoice"></param>
        /// <param name="dtPayment"></param>
        /// <returns></returns>        
        public long m_lngRptInvoiceSum(string BeginDate, string EndDate, string OperCode, List<string> DeptIDArr, out DataTable dtInvoice, out DataTable dtPayment)
        {
            return proxy.Service.m_lngRptInvoiceSum(BeginDate, EndDate, OperCode, DeptIDArr, out dtInvoice, out dtPayment);
        }
        #endregion

        #region ʵ����ϸ��־(��Ʊ)
        /// <summary>
        /// ʵ����ϸ��־(��Ʊ)
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OperCode"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dtInvoice"></param>
        /// <param name="dtPayment"></param>
        /// <returns></returns>        
        public long m_lngRptInvoiceRefundment(string BeginDate, string EndDate, string OperCode, List<string> DeptIDArr, out DataTable dtInvoice, out DataTable dtPayment)
        {
            return proxy.Service.m_lngRptInvoiceRefundment(BeginDate, EndDate, OperCode, DeptIDArr, out dtInvoice, out dtPayment);
        }
        #endregion

        #region ����ʵ�ձ���
        /// <summary>
        /// ����ʵ�ձ���
        /// </summary>
        /// <param name="Type">1 ��������ʵ�� 2 ִ�п���ʵ��</param>
        /// <param name="RptID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptIncome(int Type, string RptID, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptIncome(Type, RptID, BeginDate, EndDate, DeptIDArr, out dt);
        }
        #endregion

        #region ҽ��������ϸ����
        /// <summary>
        /// ҽ��������ϸ����
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptYBEntry(string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            return proxy.Service.m_lngRptYBEntry(BeginDate, EndDate, DeptIDArr, out dt);
        }
        #endregion

        #region ����Ԥ������ϸ����
        /// <summary>
        /// ����Ԥ������ϸ����
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptPrePayClear(string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptPrePayClear(BeginDate, EndDate, out dt);
        }
        #endregion

        #region ����ʵ����ϸ����
        /// <summary>
        /// ����ʵ����ϸ����
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptIncomeEntry(string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptIncomeEntry(BeginDate, EndDate, DeptIDArr, out dt);
        }
        #endregion

        #region ��������־����
        /// <summary>
        /// ��������־����
        /// </summary>
        /// <param name="rptType"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngRptLogBuShou(int rptType, string operCode, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            return proxy.Service.m_lngRptLogBuShou(rptType, operCode, BeginDate, EndDate, DeptIDArr, out dt);
        }

        #endregion

        #region ��Ժ������־����
        /// <summary>
        /// ��Ժ������־����
        /// </summary>
        /// <param name="OperCode"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngRptLogSettleAccount(string OperCode, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            return proxy.Service.m_lngRptLogSettleAccount(OperCode, BeginDate, EndDate, DeptIDArr, out dt);
        }
        #endregion

        #region ��Ŀͳ�Ʒ�����ϸ����
        /// <summary>
        /// ��Ŀͳ�Ʒ�����ϸ����
        /// </summary>
        /// <param name="CodeNo"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngRptItemDetailStat(string CodeNo, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            return proxy.Service.m_lngRptItemDetailStat(CodeNo, BeginDate, EndDate, DeptIDArr, out dt);
        }
        #endregion

        #region ҩƷ������(������)
        /// <summary>
        /// ҩƷ������(������)
        /// </summary>
        /// <param name="CodeNo"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngRptDragUsedStat(string CodeNo, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            return proxy.Service.m_lngRptDragUsedStat(CodeNo, BeginDate, EndDate, DeptIDArr, out dt);
        }
        #endregion

        #region ˳��������ͨҽ����סԺҽ��������ϸͳ��
        /// <summary>
        /// ˳��������ͨҽ����סԺҽ��������ϸͳ��
        /// </summary>
        /// <param name="Type">1 ���� 2 סԺ</param>
        /// <param name="NO"></param>     
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptSDYBFeeDetail(int Type, string NO, out DataTable dt)
        {
            return proxy.Service.m_lngSDYBFeeDetail(Type, NO, out dt);
        }
        #endregion

        #region ��ͨ����ҽ����סԺҽ��
        /// <summary>
        /// ��ͨ����ҽ����סԺҽ��
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="NO"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngSDZYYB(string DSN, string NO, DataTable dt)
        {
            return 0;
            //com.digitalwave.iCare.middletier.HIS.clsLunJiao objSvc =
            //                                                (com.digitalwave.iCare.middletier.HIS.clsLunJiao)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsLunJiao));

            //return proxy.Service.m_lngSDZYYB(DSN, NO, dt);
        }
        #endregion

        #region ����סԺ��
        /// <summary>
        /// ����סԺ��
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="OldNo"></param>
        /// <param name="NewNo"></param>
        /// <returns></returns>
        public long m_lngSDYBModifyZyh(string DSN, string OldNo, string NewNo)
        {
            return 0;
            //com.digitalwave.iCare.middletier.HIS.clsLunJiao objSvc =
            //                                                (com.digitalwave.iCare.middletier.HIS.clsLunJiao)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsLunJiao));

            //return proxy.Service.m_lngModifyZyh(DSN, OldNo, NewNo);
        }
        #endregion

        #region ��Ժ�Ǽ��������˼�¼
        /// <summary>
        /// ��Ժ�Ǽ��������˼�¼
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="Patient_VO"></param>
        /// <param name="Register_VO"></param>
        /// <returns></returns>
        public long m_lngSDYBBihRegister(string DSN, clsPatient_VO Patient_VO, clsT_Opr_Bih_Register_VO Register_VO)
        {
            return 0;
            //com.digitalwave.iCare.middletier.HIS.clsLunJiao objSvc =
            //                                               (com.digitalwave.iCare.middletier.HIS.clsLunJiao)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsLunJiao));

            //return proxy.Service.m_lngBihRegister(DSN, Patient_VO, Register_VO);
        }
        #endregion

        #region סԺҽ����Чͳ�Ʊ���
        /// <summary>
        /// סԺҽ����Чͳ�Ʊ���
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="MedCatArr"></param>
        /// <param name="dtDoct"></param>
        /// <param name="dtDept"></param>
        /// <param name="dtPersonNums"></param>
        /// <param name="dtBedDays"></param>
        /// <param name="dtFeeSum"></param>
        /// <param name="dtMedSum"></param>
        /// <param name="dtNonMedSum"></param>
        /// <returns></returns>        
        public long m_lngRptDoctorPerformance(string BeginDate, string EndDate, string DoctIDArr, string MedCatArr, int FeeType, out DataTable dtDoct, out DataTable dtDept,
                                              out DataTable dtPersonNums, out DataTable dtBedDays, out DataTable dtFeeSum, out DataTable dtMedSum, out DataTable dtNonMedSum)
        {
            return proxy.Service.m_lngRptDoctorPerformance(BeginDate, EndDate, DoctIDArr, MedCatArr, FeeType, out dtDoct, out dtDept, out dtPersonNums, out dtBedDays, out dtFeeSum, out dtMedSum, out dtNonMedSum);
        }

        /// <summary>
        /// 1����������
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="dtDoct"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_Doct(string BeginDate, string EndDate, string DoctIDArr, int FeeType, out DataTable dtDoct)
        {
            return proxy.Service.m_lngRptDoctorPerformance_Doct(BeginDate, EndDate, DoctIDArr, FeeType, out dtDoct);
        }

        /// <summary>
        /// 2��Ĭ�Ͽ���
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="dtDept"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_Dept(string BeginDate, string EndDate, string DoctIDArr, out DataTable dtDept)
        {
            return proxy.Service.m_lngRptDoctorPerformance_Dept(BeginDate, EndDate, DoctIDArr, out dtDept);
        }

        /// <summary>
        /// 3����ס�˴�
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="dtPersonNums"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_PersonNums(string BeginDate, string EndDate, string DoctIDArr, out DataTable dtPersonNums)
        {
            return proxy.Service.m_lngRptDoctorPerformance_PersonNums(BeginDate, EndDate, DoctIDArr, out dtPersonNums);
        }

        /// <summary>
        /// 4����Ժ�˴� ռ��������
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="dtBedDays"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_BedDays(string BeginDate, string EndDate, string DoctIDArr, out DataTable dtBedDays)
        {
            return proxy.Service.m_lngRptDoctorPerformance_BedDays(BeginDate, EndDate, DoctIDArr, out dtBedDays);
        }

        /// <summary>
        /// 5����Ժ��(��Ԥ��Ժ)�ܷ���
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="dtFeeSum"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_FeeSum(string BeginDate, string EndDate, string DoctIDArr, int FeeType, out DataTable dtFeeSum)
        {
            return proxy.Service.m_lngRptDoctorPerformance_FeeSum(BeginDate, EndDate, DoctIDArr, FeeType, out dtFeeSum);
        }

        /// <summary>
        /// 6����Ժ��(��Ԥ��Ժ)ҩ��
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="MedCatArr"></param>
        /// <param name="dtMedSum"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_MedSum(string BeginDate, string EndDate, string DoctIDArr, string MedCatArr, int FeeType, out DataTable dtMedSum)
        {
            return proxy.Service.m_lngRptDoctorPerformance_MedSum(BeginDate, EndDate, DoctIDArr, MedCatArr, FeeType, out dtMedSum);
        }

        /// <summary>
        /// 7����Ժ��(��Ԥ��Ժ)��ҩ��
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="MedCatArr"></param>
        /// <param name="dtNonMedSum"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_NonMedSum(string BeginDate, string EndDate, string DoctIDArr, string MedCatArr, int FeeType, out DataTable dtNonMedSum)
        {
            return proxy.Service.m_lngRptDoctorPerformance_NonMedSum(BeginDate, EndDate, DoctIDArr, MedCatArr, FeeType, out dtNonMedSum);
        }

        /// <summary>
        /// ���Ϸ�
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="MedCatArr"></param>
        /// <param name="FeeType"></param>
        /// <param name="dtNonMedSum"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_MateSum(string BeginDate, string EndDate, string DoctIDArr, string MateCatArr, int FeeType, out DataTable dtMateSum)
        {
            return proxy.Service.m_lngRptDoctorPerformance_MateSum(BeginDate, EndDate, DoctIDArr, MateCatArr, FeeType, out dtMateSum);
        }

        /// <summary>
        /// ��Ч
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DoctIDArr"></param>
        /// <param name="FeeType"></param>
        /// <param name="RptID"></param>
        /// <param name="m_objGroup"></param>
        /// <param name="m_objResult"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_Effects(string BeginDate, string EndDate, string DoctIDArr, int FeeType,
                                              string RptID, Dictionary<string, decimal> m_objGroup, out Dictionary<string, decimal> m_objResult)
        {
            return proxy.Service.m_lngRptDoctorPerformance_Effects(BeginDate, EndDate, DoctIDArr, FeeType, RptID, m_objGroup, out m_objResult);
        }
        /// <summary>
        /// ����ҩ����
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="FeeType"></param>
        /// <param name="m_objAntiseptic"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_Antiseptic(string BeginDate, string EndDate, string DoctIDArr, string KangJunArr, int FeeType, out DataTable dtAntiseptic)
        {
            return proxy.Service.m_lngRptDoctorPerformance_Antiseptic(BeginDate, EndDate, DoctIDArr, KangJunArr, FeeType, out dtAntiseptic);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="FeeType"></param>
        /// <param name="m_objAntiseptic"></param>
        /// <returns></returns>
        public long m_lngRptDoctorPerformance_Essential(string BeginDate, string EndDate, string DoctIDArr, string JiBenArr, int FeeType, out DataTable dtEssential)
        {
            return proxy.Service.m_lngRptDoctorPerformance_Essential(BeginDate, EndDate, DoctIDArr, JiBenArr, FeeType, out dtEssential);
        }

        #endregion

        #region ȫԺ(���סԺ)�����㵥λʵ�ձ���
        /// <summary>
        /// ȫԺ(���סԺ)�����㵥λʵ�ձ���
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Type">1 ��Ʊʱ�� 2 �ս�ʱ��</param>
        /// <param name="dtGroup"></param>
        /// <param name="dtRecNums"></param>
        /// <param name="dtMz"></param>
        /// <param name="dtZy"></param>
        /// <returns></returns>       
        public long m_lngRptAllDeptIncome(string BeginDate, string EndDate, int Type, out DataTable dtGroup, out DataTable dtRecNums, out DataTable dtMz, out DataTable dtZy)
        {
            return proxy.Service.m_lngRptAllDeptIncome(BeginDate, EndDate, Type, out dtGroup, out dtRecNums, out dtMz, out dtZy);
        }
        #endregion

        #region ��ȡ���ܿ���רҵ�����ͳ������
        /// <summary>
        /// ��ȡ���ܿ���רҵ�����ͳ������
        /// </summary>
        /// <param name="p_objData"></param>
        /// <returns></returns>
        public long m_lngGetGroupInComeByDoctor(ref clsGroupInComeByDoctorOrArea_VO objvalue_Param, out DataTable dtbResult)
        {
            return proxy.Service.m_lngGetGroupInComeByDoctor(ref objvalue_Param, out dtbResult);
        }
        #endregion

        #region ��ȡ���ܿ��Һ���ʵ��ͳ������-����ҽ��
        /// <summary>
        /// ��ȡ���ܿ��Һ���ʵ��ͳ������
        /// </summary>
        /// <param name="p_objData"></param>
        /// <returns></returns>
        public long m_lngGetGroupInComeByArea(ref clsGroupInComeByDoctorOrArea_VO objvalue_Param, out DataTable dtbResult)
        {
            return proxy.Service.m_lngGetGroupInComeByDoctor(ref objvalue_Param, out dtbResult);
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="strFindCode"></param>
        /// <param name="objItemArr"></param>
        /// <returns></returns>
        internal long m_lngFindArea(string strFindCode, out clsBIHArea[] objItemArr)
        {
            return (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(strFindCode, out objItemArr);
        }

        #endregion

        #region Ԥ��Ժδ����ͳ��-�õ���Ա�б�
        /// <summary>
        /// �õ���Ա�б�
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="p_strSections"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetOutNoChargePatientList(string BeginDate, string EndDate, string p_strSections, out DataTable dt)
        {
            return proxy.Service.m_lngGetOutNoChargePatientList(BeginDate, EndDate, p_strSections, out dt);
        }
        #endregion

        #region ����REGID��ȡ��������Ԥ����
        /// <summary>
        /// ����REGID��ȡ��������Ԥ����
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPrepayStatusSumByRegID(string RegID, out DataTable dt)
        {
            return proxy.Service.m_lngGetPrepayStatusSumByRegID(RegID, out dt);
        }
        #endregion

        #region ����REGID��ȡ�������и�״̬������Ϣ
        /// <summary>
        /// ����REGID��ȡ�������и�״̬������Ϣ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngGetPatientStatusSumByRegID(string RegID, out DataTable dt)
        {
            return proxy.Service.m_lngGetPatientStatusSumByRegID(RegID, out dt);
        }
        #endregion

        #region ����������־
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>       
        public long m_lngRptDeptWorkLog_Dept(out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_Dept(out dt);
        }

        /// <summary>
        /// ������Ժ����
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_InNums(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_InNums(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// �����Ժ����
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_OutNums(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_OutNums(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ��Ժ��������
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_OutDeadNums(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_OutDeadNums(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ��Ժ��������(24Сʱ)
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_OutDead24Nums(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_OutDead24Nums(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ������Ժ����
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>      
        public long m_lngRptDeptWorkLog_OnNums(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_OnNums(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ������Ժ��������
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>      
        public long m_lngRptDeptWorkLog_FmNums(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_FMNums(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ����ת������
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_TransOutNums(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_TransOutNums(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ����ת������
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_TransInNums(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_TransInNums(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ��Ժ�����嵥
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_InPatList(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_InPatList(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ת�벡���嵥
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_TransInPatList(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_TransInPatList(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ת�������嵥
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_TransOutPatList(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_TransOutPatList(AreaID, CurrDate, out dt);
        }

        /// <summary>
        /// ��Ժ�����嵥
        /// </summary>
        /// <param name="AreaID"></param>
        /// <param name="CurrDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngRptDeptWorkLog_OutPatList(string AreaID, string CurrDate, out DataTable dt)
        {
            return proxy.Service.m_lngRptDeptWorkLog_OutPatList(AreaID, CurrDate, out dt);
        }
        #endregion

        #region ����ҽ��
        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLPatientInfo(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLPatientInfo(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region �����ϼ�
        #region �����ϼ�1-��ҩ
        /// <summary>
        /// �����ϼ�1-��ҩ
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLRecipeSum1(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLRecipeSum1(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region �����ϼ�2-��ҩ
        /// <summary>
        /// �����ϼ�2-��ҩ
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLRecipeSum2(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLRecipeSum2(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion
        #endregion

        #region ������ϸ
        #region ������ϸ1-��ҩ
        /// <summary>
        /// ������ϸ1-��ҩ
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLRecipeEntry1(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLRecipeEntry1(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region ������ϸ2-��ҩ
        /// <summary>
        /// ������ϸ2-��ҩ
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLRecipeEntry2(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLRecipeEntry2(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region ������ϸ3-����
        /// <summary>
        /// ������ϸ3-����
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLRecipeEntry3(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLRecipeEntry3(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region ������ϸ4-���
        /// <summary>
        /// ������ϸ4-���
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLRecipeEntry4(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLRecipeEntry4(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region ������ϸ5-����
        /// <summary>
        /// ������ϸ5-����
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLRecipeEntry5(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLRecipeEntry5(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion

        #region ������ϸ6-����
        /// <summary>
        /// ������ϸ6-����
        /// </summary>
        /// <param name="PayTypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>        
        public long m_lngHZYLRecipeEntry6(string PayTypeID, string BeginDate, string EndDate, out DataTable dt)
        {
            return proxy.Service.m_lngHZYLRecipeEntry6(PayTypeID, BeginDate, EndDate, out dt);
        }
        #endregion

        #endregion

        #region ����DBF
        #region �������ϱ�(grzl)
        /// <summary>
        /// �������ϱ�(grzl)
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngCreateHZYLDbf_PatInfo(string DSN, string DbfName, ArrayList objYBArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsLunJiao objSvc =
            //                                                (com.digitalwave.iCare.middletier.HIS.clsLunJiao)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsLunJiao));

            //long l = proxy.Service.m_lngCreateHZYLDbf_PatInfo(DSN, DbfName, objYBArr);

            return 1;
        }
        #endregion

        #region ��������(zycf)
        /// <summary>
        /// ��������(zycf)
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngCreateHZYLDbf_RecipeSum(string DSN, string DbfName, ArrayList objYBArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsLunJiao objSvc =
            //                                                (com.digitalwave.iCare.middletier.HIS.clsLunJiao)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsLunJiao));

            //long l = proxy.Service.m_lngCreateHZYLDbf_RecipeSum(DSN, DbfName, objYBArr);

            return 1;
        }
        #endregion

        #region �������ϱ�(cfzl)
        /// <summary>
        /// �������ϱ�(cfzl)
        /// </summary>
        /// <param name="DSN"></param>
        /// <param name="DbfName"></param>
        /// <param name="objYBArr"></param>
        /// <returns></returns>
        public long m_lngCreateHZYLDbf_RecipeEntry(string DSN, string DbfName, ArrayList objYBArr)
        {
            //com.digitalwave.iCare.middletier.HIS.clsLunJiao objSvc =
            //                                                (com.digitalwave.iCare.middletier.HIS.clsLunJiao)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsLunJiao));

            //long l = proxy.Service.m_lngCreateHZYLDbf_RecipeEntry(DSN, DbfName, objYBArr);

            return 1;
        }
        #endregion
        #endregion
        #endregion

        #region ҽ�����
        /// <summary>
        /// ҽ�����
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long m_lngGetALLYBType(out DataTable dt)
        {
            return proxy.Service.m_lngGetALLYBType(out dt);
        }
        #endregion

        #region ����ҽ��-HIS��Ӧ��Ŀ
        /// <summary>
        /// ����ҽ��-HIS��Ӧ��Ŀ
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long m_lngGetShiying(string strQuery, out DataTable dtResult)
        {
            return proxy.Service.m_lngGetShiying(strQuery, out dtResult);
        }
        #endregion

        #region ������Ӧ֢
        /// <summary>
        /// ������Ӧ֢
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long m_lngSaveShiying(clsShiyingVO objVO)
        {
            return proxy.Service.m_lngSaveShiying(objVO);
        }
        #endregion

        #region ɾ����Ӧ֢
        /// <summary>
        /// ɾ����Ӧ֢
        /// </summary>
        /// <param name="objVO"></param>
        /// <returns></returns>
        public long m_lngDelShiying(clsShiyingVO objVO)
        {
            return proxy.Service.m_lngDelShiying(objVO);
        }
        #endregion

        #region �շѴ��ɿ��ͳ���������
        /// <summary>
        /// �շѴ��ɿ��ͳ���������
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dtCharge"></param>
        /// <param name="dtPayment"></param>
        /// <param name="dtRemarkInfo"></param>
        /// <returns></returns>        
        public long m_lngRptTotaldiffcostmoney(string m_strChargeno, string EmpID, out DataTable dttodiffsum)
        {
            return proxy.Service.m_lngRptTotaldiffcostmoney(m_strChargeno, EmpID, out dttodiffsum);
        }
        #endregion

        #region �籣�Ǽ���־
        /// <summary>
        /// �籣�Ǽ���־
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptIdArr"></param>
        /// <returns></returns>
        public DataTable GetRptSbRegister(string beginDate, string endDate, string deptIdArr)
        {
            return proxy.Service.GetRptSbRegister(beginDate, endDate, deptIdArr);
        }
        #endregion

        #region ����סԺ�˷�ԭ��
        /// <summary>
        /// ����סԺ�˷�ԭ��
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetRptInvoiceRefundReason(int flagId, string beginDate, string endDate)
        {
            return proxy.Service.GetRptInvoiceRefundReason(flagId, beginDate, endDate);
        }
        #endregion

        #region ��ȡ���͵�λ
        /// <summary>
        /// ��ȡ���͵�λ
        /// </summary>
        /// <returns></returns>
        public DataTable GetOutsideUnit()
        {
            return proxy.Service.GetOutsideUnit();
        }
        #endregion

        #region ��ȡ���ͷ�����ϸ
        /// <summary>
        /// ��ȡ���ͷ�����ϸ
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetOutsideChargeItem(string beginDate, string endDate)
        {
            return proxy.Service.GetOutsideChargeItem(beginDate, endDate);
        }
        #endregion

    }
}
