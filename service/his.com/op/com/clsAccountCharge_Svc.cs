using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// �շ�
    /// �����ˣ�	2005-03-18
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAccountCharge_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        public clsAccountCharge_Svc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        //�÷��շ�
        #region �����÷���λƵ������������
        /// <summary>
        /// ��ȡ�����÷���λƵ������������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
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
        [AutoComplete]
        public long m_lngGetMeasureClinicUsage(int p_intTIMES, double p_dblQTY, int p_intType, double p_dblUnitDosage, out double p_dblGet)
        {
            long lngRes = 0;
            p_dblGet = 0;

            p_dblGet = 0;
            if (p_intType == 2)//������λ
            {
                double dblUse = p_dblQTY / p_dblUnitDosage;
                p_dblGet = dblUse * p_intTIMES; //����*����
            }
            else if (p_intType == 1)//������λ
            {
                p_dblGet = p_dblQTY * p_intTIMES;
            }
            return lngRes;
        }
        #endregion
        #region �����÷��շ�
        /// <summary>
        /// ��ȡ�����÷��շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strITEMID_CHR">�շ���ĿID</param>
        /// <param name="strUSAGEID_CHR">�÷�ID</param>
        /// <param name="p_dblMoney">�շ�	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeClinicUsage(string strITEMID_CHR, string strUSAGEID_CHR, out double p_dblMoney)
        {
            long lngRes = 0;
            p_dblMoney = 0;

            clsChargeItem_VO[] objItemArr;
            lngRes = new clsChargeItemSvc().m_GetItemByUsageIDAndItemID(strITEMID_CHR, strUSAGEID_CHR, out objItemArr);
            if (lngRes > 0)
            {
                //סԺ����								itemprice_mny
                //decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice
                double dblPrice = 0;
                try
                {
                    //סԺ�շѵ�λ 0 ��������λ 1����С��λ
                    if (objItemArr[0].m_intOPCHARGEFLG_INT == 0)//�����շѵ�λ 0 ��������λ 1����С��λ
                        dblPrice = double.Parse(objItemArr[0].m_fltItemPrice.ToString());
                    else
                    {
                        double dblItemPrice = double.Parse(objItemArr[0].m_fltItemPrice.ToString());
                        double dblPACKQTY_DEC = double.Parse(objItemArr[0].m_decPACKQTY_DEC.ToString());
                        dblPrice = double.Parse((dblItemPrice / dblPACKQTY_DEC).ToString("0.0000"));
                    }
                }
                catch { }
                //��������
                double dblQTY_DEC = 0;
                try
                {
                    dblQTY_DEC = double.Parse(objItemArr[0].m_strUNITPRICE.ToString());
                }
                catch { }
                //ҽ���µļ���
                double dblDosage = 0;
                try
                {
                    dblDosage = double.Parse(objItemArr[0].m_strDosage.ToString());
                }
                catch { }
                lngRes = m_lngGetChargeClinicUsage(dblPrice, 1, dblQTY_DEC, objItemArr[0].m_intCLINICTYPE_INT, dblDosage, out p_dblMoney);
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡ�����÷��շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
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
        ///		if(TYPE_INT==2[������λ]) then {=����*(ҽ���µļ���/��λ����)}
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		���磺����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        [AutoComplete]
        public long m_lngGetChargeClinicUsage(double p_dblPrice, int p_intTIMES, double p_dblQTY, int p_intType, double p_dblUnitDosage, out double p_dblMoney)
        {
            long lngRes = 0;
            p_dblMoney = 0;

            double dblGet = 0;
            lngRes = m_lngGetMeasureClinicUsage(p_intTIMES, p_dblQTY, p_intType, p_dblUnitDosage, out dblGet);
            p_dblMoney = p_dblPrice * dblGet;
            return lngRes;
        }
        #endregion
        #region סԺ�÷���λƵ������������
        /// <summary>
        /// ��ȡסԺ�÷���λƵ������������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
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
        [AutoComplete]
        public long m_lngGetMeasureBIHUsage(int p_intTIMES, double p_dblQTY, int p_intType, double p_dblUnitDosage, out double p_dblGet)
        {
            long lngRes = 0;
            p_dblGet = 0;

            p_dblGet = 0;
            if (p_intType == 2)//������λ
            {
                double dblUse = p_dblQTY / p_dblUnitDosage;
                p_dblGet = dblUse * p_intTIMES; //����*����
            }
            else if (p_intType == 1)//������λ
            {
                p_dblGet = p_dblQTY * p_intTIMES;
            }
            return 1;
        }
        #endregion
        #region ��ȡ�����ܼۼ�סԺ�ܼ�
        /// <summary>
        /// ��ȡ�����ܼۼ�סԺ�ܼ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strITEMID_CHR">��ĿID</param>
        /// <param name="intType">1-�����ܼۣ�2-סԺ�ܼ�</param>
        /// <param name="dblQTY">����</param>
        /// <param name="intNuit">1-��ҩ��λ��2-������λ</param>
        /// <param name="dblTotailMoney">�����ܽ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeUsageTotailMoney(string strITEMID_CHR, int intType, double dblQTY, int intNuit, out double dblTotailMoney)
        {
            long lngRes = 0;
            dblTotailMoney = 0;
            string strSQL = @"select ITEMPRICE_MNY,ITEMOPUNIT_CHR,ITEMIPUNIT_CHR,DOSAGE_DEC,DOSAGEUNIT_CHR,PACKQTY_DEC,OPCHARGEFLG_INT,IPCHARGEFLG_INT from t_bse_chargeitem where ITEMID_CHR='" + strITEMID_CHR + "'";
            DataTable p_dt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //��С��λ����
            double littlePrice = 0.00;
            try
            {
                littlePrice = Double.Parse(p_dt.Rows[0]["ITEMPRICE_MNY"].ToString()) / int.Parse(p_dt.Rows[0]["PACKQTY_DEC"].ToString());
            }
            catch
            {
            }

            if (intType == 1)
            {
                if (intNuit == 1)
                {
                    if (p_dt.Rows[0]["OPCHARGEFLG_INT"].ToString() == "0")
                    {
                        dblTotailMoney = Double.Parse(p_dt.Rows[0]["ITEMPRICE_MNY"].ToString()) * dblQTY;
                    }
                    else
                    {
                        dblTotailMoney = littlePrice * dblQTY;
                    }
                }
                else
                {
                    if (p_dt.Rows[0]["DOSAGE_DEC"] != System.DBNull.Value && p_dt.Rows[0]["DOSAGE_DEC"].ToString() != "")
                    {
                        dblTotailMoney = dblQTY / Double.Parse(p_dt.Rows[0]["DOSAGE_DEC"].ToString()) * littlePrice;
                    }
                }
            }
            else
            {
                if (intNuit == 1)
                {
                    if (p_dt.Rows[0]["IPCHARGEFLG_INT"].ToString() == "0")
                    {
                        dblTotailMoney = Double.Parse(p_dt.Rows[0]["ITEMPRICE_MNY"].ToString()) * dblQTY;
                    }
                    else
                    {
                        dblTotailMoney = littlePrice * dblQTY;
                    }
                }
                else
                {
                    if (p_dt.Rows[0]["DOSAGE_DEC"] != System.DBNull.Value && p_dt.Rows[0]["DOSAGE_DEC"].ToString() != "")
                    {
                        dblTotailMoney = dblQTY / Double.Parse(p_dt.Rows[0]["DOSAGE_DEC"].ToString()) * littlePrice;
                    }
                }

            }
            return lngRes;
        }

        #endregion
        #region סԺ�÷��շ�
        /// <summary>
        /// ��ȡסԺ�÷��շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strITEMID_CHR">�շ���ĿID</param>
        /// <param name="strUSAGEID_CHR">�÷�ID</param>
        /// <param name="p_dblMoney">�շ�	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeBIHUsage(string strITEMID_CHR, string strUSAGEID_CHR, out double p_dblMoney)
        {
            long lngRes = 0;
            p_dblMoney = 0;

            clsChargeItem_VO[] objItemArr;
            lngRes = new clsChargeItemSvc().m_GetItemByUsageIDAndItemID(strITEMID_CHR, strUSAGEID_CHR, out objItemArr);
            if (lngRes > 0)
            {
                //סԺ����								
                //decode(a.IPCHARGEFLG_INT,1,Round(a.ItemPrice_Mny/a.PackQty_Dec,4),0,a.ItemPrice_Mny,Round(a.ItemPrice_Mny/a.PackQty_Dec,4)) MinPrice
                double dblPrice = 0;
                try
                {
                    //סԺ�շѵ�λ 0 ��������λ 1����С��λ
                    if (objItemArr[0].m_intOPCHARGEFLG_INT == 0)//�����շѵ�λ 0 ��������λ 1����С��λ
                        dblPrice = double.Parse(objItemArr[0].m_fltItemPrice.ToString());
                    else
                    {
                        double dblItemPrice = double.Parse(objItemArr[0].m_fltItemPrice.ToString());
                        double dblPACKQTY_DEC = double.Parse(objItemArr[0].m_decPACKQTY_DEC.ToString());
                        dblPrice = double.Parse((dblItemPrice / dblPACKQTY_DEC).ToString("0.0000"));
                    }
                }
                catch { }
                //סԺ����
                double dblQTY_DEC = 0;
                try
                {
                    dblQTY_DEC = objItemArr[0].m_dblBIHQTY_DEC;
                }
                catch { }
                //ҽ���µļ���
                double dblDosage = 0;
                try
                {
                    dblDosage = double.Parse(objItemArr[0].m_strDosage.ToString());
                }
                catch { }
                lngRes = m_lngGetChargeClinicUsage(dblPrice, 1, dblQTY_DEC, objItemArr[0].m_intBIHTYPE_INT, dblDosage, out p_dblMoney);
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡסԺ�÷��շ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
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
        [AutoComplete]
        public long m_lngGetChargeBIHUsage(double p_dblPrice, int p_intTIMES, double p_dblQTY, int p_intType, double p_dblUnitDosage, out double p_dblMoney)
        {
            long lngRes = 0;
            p_dblMoney = 0;

            double dblGet = 0;
            lngRes = m_lngGetMeasureBIHUsage(p_intTIMES, p_dblQTY, p_intType, p_dblUnitDosage, out dblGet);
            p_dblMoney = p_dblPrice * dblGet;
            return lngRes;
        }
        #endregion
    }
}
