using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.middletier.BIHOrderServer
{
    /// <summary>
    /// ҽ��ִ��
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBIHOrderExecuteService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ��ȡ����
        /// <summary>
        /// ͨ������,���ŷ�Χ��ȡ����
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strAreaID"></param>
        /// <param name="strBedBegin"></param>
        /// <param name="strBedEnd"></param>
        /// <returns></returns>
        [AutoComplete]
        private string m_strGetPatientsSQL(string strTableName, string strAreaID, string strBedBegin, string strBedEnd)
        {
            return new clsBIHOrderService().m_strGetPatientsSQL(strTableName, strAreaID, strBedBegin, strBedEnd);
        }
        #endregion
        #region ��ȡҽ��-ȫ��
        /// <summary>
        /// ��ȡҽ�� [����=������1:����ִ��   2:Ƶ�ʲ���ִ��   3:��ִ��	4:��ֹͣ��]	
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="dtExecuteDate">ִ��ʱ��</param>
        /// <param name="arrOrder">��ִ��ҽ����ID [out ���� ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCanExecuteOrder(string p_strAreaID, string p_strBedIDs, DateTime dtExecuteDate, out clsBIHCanExecOrder[] arrOrder)
        {
            string str1 = "";
            string strSql = @"select   t1.orderid_chr,
       t1.orderdicid_chr,
       t1.registerid_chr,
       t1.patientid_chr,
       t1.executetype_int,
       t1.recipeno_int,
       t1.name_vchr,
       t1.spec_vchr,
       t1.execfreqid_chr,
       t1.dosage_dec,
       t1.execfreqname_chr,
       t1.dosageunit_chr,
       t1.get_dec,
       t1.useunit_chr,
       t1.getunit_chr,
       t1.dosetypeid_chr,
       t1.dosetypename_chr,
       t1.startdate_dat,
       t1.finishdate_dat,
       t1.execdeptid_chr,
       t1.execdeptname_chr,
       t1.entrust_vchr,
       t1.parentid_chr,
       t1.status_int,
       t1.creatorid_chr,
       t1.creator_chr,
       t1.createdate_dat,
       t1.posterid_chr,
       t1.poster_chr,
       t1.postdate_dat,
       t1.executorid_chr,
       t1.executor_chr,
       t1.executedate_dat,
       t1.stoperid_chr,
       t1.stoper_chr,
       t1.stopdate_dat,
       t1.retractorid_chr,
       t1.retractor_chr,
       t1.retractdate_dat,
       t1.isrich_int,
       t1.ratetype_int,
       t1.isrepare_int,
       t1.use_dec,
       t1.isneedfeel,
       t1.outgetmeddays_int,
       t1.assessoridforexec_chr,
       t1.assessorforexec_chr,
       t1.assessorforexec_dat,
       t1.assessoridforstop_chr,
       t1.assessorforstop_chr,
       t1.assessorforstop_dat,
       t1.backreason,
       t1.sendbackid_chr,
       t1.sendbacker_chr,
       t1.sendback_dat,
       t1.isyb_int,
       t1.sampleid_vchr,
       t1.lisappid_vchr,
       t1.partid_vchr,
       t1.createareaid_chr,
       t1.createareaname_vchr,
       t1.ifparentid_int,
       t1.confirmerid_chr,
       t1.confirmer_vchr,
       t1.confirm_dat,
       t1.attachtimes_int,
       t1.doctorid_chr,
       t1.doctor_vchr,
       t1.curareaid_chr,
       t1.curbedid_chr,
       t1.doctorgroupid_chr,
       t1.deleterid_chr,
       t1.deletername_vchr,
       t1.delete_dat,
       t1.sign_int,
       t1.operation_int,
       t1.remark_vchr,
       t1.recipeno2_int,
       t1.feelresult_vchr,
       t1.feel_int,
       t1.type_int,
       t1.charge_int,
       t1.singleamount_dec,
       t1.sourcetype_int,
       t1.chargedoctorgroupid_chr,
       t1.itemchargetype_vchr, t7.recipeno_int || ' ' || t7.name_vchr as parentname,
         t3.lastname_vchr as patientname, t3.sex_chr as patientsex,
         t2.areaid_chr, t2.bedid_chr as bedid, t4.code_chr,t4.areaid_chr,
         bihpack.canexecute (t1.orderid_chr,
                             t1.executetype_int,
                             t1.status_int,
                             t1.finishdate_dat,
                             TO_DATE ('" + dtExecuteDate.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                                      'yyyy-mm-dd hh24:mi:ss'
                                     )
                            ) execflag,
         decode (t6.ipchargeflg_int,
                 1, round (t6.itemprice_mny / t6.packqty_dec, 4),
                 0, t6.itemprice_mny,
                 round (t6.itemprice_mny / t6.packqty_dec, 4)
                ) itemprice,
         t5.ordercateid_chr, t8.attarelaid_chr
    from t_opr_bih_order t1,
         t_opr_bih_register t2,
         t_bse_patient t3,
         t_bse_bed t4,
         t_bse_bih_orderdic t5,
         t_bse_chargeitem t6,
         t_opr_bih_order t7,
         t_opr_attachrelation t8
   where t1.registerid_chr = t2.registerid_chr
     and t2.patientid_chr = t3.patientid_chr
     and t2.registerid_chr = t4.bihregisterid_chr
     and t1.orderdicid_chr = t5.orderdicid_chr(+)
     and t5.itemid_chr = t6.itemid_chr(+)
     and t1.parentid_chr = t7.orderid_chr(+)
     and t1.orderid_chr = t8.sourceitemid_vchr(+)
     and t2.pstatus_int <> 3
     and t1.status_int in (1, 2, 3, 5, 6)
     [patientcondition]
order by t4.bedid_chr, t1.patientid_chr, t1.recipeno_int, t1.createdate_dat";

            if (p_strAreaID.Trim() != "")
            {
                str1 += " AND t2.areaid_chr='" + p_strAreaID.Trim() + "' ";
                if (p_strBedIDs.Trim() != "")
                {
                    str1 += @"
        AND t2.bedid_chr IN (" + p_strBedIDs.Trim() + ")";
                }
            }
            strSql = strSql.Replace("[PatientCondition]", str1);
            long lngRes = -1;
            arrOrder = null;
            try
            {
                DataTable objDT = new DataTable();
                lngRes = 0;
                lngRes = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                if (lngRes > 0 && (objDT != null) && objDT.Rows.Count > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngGetCanExeOrderArrFromDataTable(objDT, out arrOrder);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                arrOrder = null;
            }
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
        [AutoComplete]
        public long m_lngGetCanExecuteOrder(string p_strAreaID, string p_strBedIDs, string p_strOrderType, string p_strOrderStatus, string p_strOrderCate, bool p_blnNeedFeel, bool p_blnTakeMedicine, bool p_blnOnlyToday, string strCreatorID, DateTime dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            string str1 = "";
            string strSql = @"
				select a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr,
                       a.executetype_int, a.recipeno_int, a.name_vchr, a.spec_vchr,
                       a.execfreqid_chr, a.dosage_dec, a.execfreqname_chr, a.dosageunit_chr,
                       a.get_dec, a.useunit_chr, a.getunit_chr, a.dosetypeid_chr,
                       a.dosetypename_chr, a.startdate_dat, a.finishdate_dat,
                       a.execdeptid_chr, a.execdeptname_chr, a.entrust_vchr, a.parentid_chr,
                       a.status_int, a.creatorid_chr, a.creator_chr, a.createdate_dat,
                       a.posterid_chr, a.poster_chr, a.postdate_dat, a.executorid_chr,
                       a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                       a.stopdate_dat, a.retractorid_chr, a.retractor_chr, a.retractdate_dat,
                       a.isrich_int, a.ratetype_int, a.isrepare_int, a.use_dec, a.isneedfeel,
                       a.outgetmeddays_int, a.assessoridforexec_chr, a.assessorforexec_chr,
                       a.assessorforexec_dat, a.assessoridforstop_chr, a.assessorforstop_chr,
                       a.assessorforstop_dat, a.backreason, a.sendbackid_chr,
                       a.sendbacker_chr, a.sendback_dat, a.isyb_int, a.sampleid_vchr,
                       a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                       a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr,
                       a.confirmer_vchr, a.confirm_dat, a.attachtimes_int, a.doctorid_chr,
                       a.doctor_vchr, a.curareaid_chr, a.curbedid_chr, a.doctorgroupid_chr,
                       a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                       a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr,
                       a.feel_int, a.charge_int, a.type_int, a.singleamount_dec,
                       a.sourcetype_int  
 					,(select REPLACE(trim(RECIPENO_INT || ' ' || NAME_VCHR),' ',' - ') from T_Opr_Bih_Order where Trim(T_Opr_Bih_Order.ORDERID_CHR)=Trim(A.PARENTID_CHR)) ParentName 	
 					,(select Name_Vchr from T_BSE_Patient where Trim(T_BSE_Patient.PatientID_Chr)=Trim(A.PatientID_Chr)) PatientName 	
 					,(select Sex_Chr from T_BSE_Patient where Trim(T_BSE_Patient.PatientID_Chr)=Trim(A.PatientID_Chr)) PatientSex 		
 					,(select AREAID_CHR from T_Opr_Bih_Register where T_Opr_Bih_Register.RegisterID_Chr=A.RegisterID_Chr ) AREAID_CHR 
 					,(select BedID_Chr from T_Opr_Bih_Register where T_Opr_Bih_Register.RegisterID_Chr=A.RegisterID_Chr ) BedID
 					,(select a1.Code_Chr from ([GetBedNOTable])a1 where a1.RegisterID_Chr=a.RegisterID_Chr) CODE_CHR 
 					,BIHPack.CanExecute(A.OrderID_Chr,A.ExecuteType_Int,A.Status_Int,A.FinishDate_Dat,[ExecuteDateValue]) ExecFlag
 					,(select a2.ItemPrice from ([GetItemPriceTable])a2 where a2.OrderDicID_Chr=a.OrderDicID_Chr) ItemPrice
					,(select ordercateid_chr from t_bse_bih_orderdic where t_bse_bih_orderdic.orderdicid_chr= a.orderdicid_chr) ordercateid_chr
				from T_Opr_Bih_Order A
				where 
					(A.Status_Int=1 or A.Status_Int=2 or A.Status_Int=3 or A.Status_Int=5 or A.Status_Int=6)
					and A.RegisterID_Chr in(SELECT registerid_chr FROM t_opr_bih_register where pstatus_int<>3)
					[ORDERTYPECONDITION]
					[ORDERSTATUSCONDITION]
					[TAKEMEDICINECONDITION]
					[ONLYTODAYCONDITION]					
				order by registerid_chr,a.parentid_chr desc			
				";
            #region ����
            //��ȡ���� [��Ժ�Ǽ���ˮ�á�����]	=>[GetBedNOTable]
            str1 = "select RegisterID_Chr ,(select CODE_CHR from T_BSE_Bed where T_BSE_Bed.BedID_Chr=T_Opr_Bih_Register.BedID_Chr) Code_Chr from T_Opr_Bih_Register";
            strSql = strSql.Replace("[GetBedNOTable]", str1);
            //��ȡסԺ���� [������Ŀ��ˮ�š�סԺ����]	=>[GetItemPriceTable]	
            str1 = @"
					select OrderDicID_Chr,
						(select decode(IPCHARGEFLG_INT,1,Round(itemprice_mny/PackQty_Dec,4),0,itemprice_mny,Round(itemprice_mny/PackQty_Dec,4)) ItemPrice 
						from T_BSE_ChargeItem 
						where T_BSE_ChargeItem.itemid_chr=T_BSE_BIH_OrderDic.itemid_chr
						)ItemPrice 
					from T_BSE_BIH_OrderDic
				 ";

            strSql = strSql.Replace("[GetItemPriceTable]", str1);

            strSql = @"select AA.* from (" + strSql + ") AA " + " where [CreatorCondition] [PatientCondition] [NeedFeelCondition] [ORDERCATECONDITION] order by AA.BedID,AA.patientid_chr,AA.recipeno_int,AA.createdate_dat ";
            //��ѯ����
            if (strCreatorID.Trim() != "")
                strSql = strSql.Replace("[CreatorCondition]", " AA.CreatorID_Chr = '" + strCreatorID.Trim() + "' ");
            else
                strSql = strSql.Replace("[CreatorCondition]", " 1=1 ");
            str1 = "";
            if (p_strAreaID.Trim() != "")
            {
                str1 += " AND TRIM(AA.areaid_chr)='" + p_strAreaID.Trim() + "' ";
                if (p_strBedIDs.Trim() != "")
                {
                    str1 += " AND TRIM(AA.BedID) IN (" + p_strBedIDs.Trim() + ")";
                }
            }
            strSql = strSql.Replace("[PatientCondition]", str1);
            strSql = strSql.Replace("[ExecuteDateValue]", "to_date('" + dtExecuteDate.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')");
            //ҽ������	=>[ORDERTYPECONDITION]
            if (p_strOrderType.Trim() != "")
                strSql = strSql.Replace("[ORDERTYPECONDITION]", "  and A.EXECUTETYPE_INT in (" + p_strOrderType + ") ");
            else
                strSql = strSql.Replace("[ORDERTYPECONDITION]", "");
            //ִ��״̬	=>[ORDERSTATUSCONDITION]
            if (p_strOrderStatus.Trim() != "")
                strSql = strSql.Replace("[ORDERSTATUSCONDITION]", "  and A.STATUS_INT in (" + p_strOrderStatus + ") ");
            else
                strSql = strSql.Replace("[ORDERSTATUSCONDITION]", "");
            //Ƥ��	=>[NeedFeelCondition]
            if (p_blnNeedFeel)
                strSql = strSql.Replace("[NeedFeelCondition]", "  and AA.IsNeedFeeL=1 ");
            else
                strSql = strSql.Replace("[NeedFeelCondition]", "");
            //��Ժ��ҩ	=>[TAKEMEDICINECONDITION]
            if (p_blnTakeMedicine)
                strSql = strSql.Replace("[TAKEMEDICINECONDITION]", "  and A.EXECUTETYPE_INT=2 and A.RATETYPE_INT=4 ");
            else
                strSql = strSql.Replace("[TAKEMEDICINECONDITION]", "");
            //���Ե���	=>[ONLYTODAYCONDITION]
            if (p_blnTakeMedicine)
                strSql = strSql.Replace("[ONLYTODAYCONDITION]", "  and trunc(A.POSTDATE_DAT)=to_date(sysdate,'yyyy-mm-dd')");
            else
                strSql = strSql.Replace("[ONLYTODAYCONDITION]", "");
            //������Ŀ����	=>[ORDERCATECONDITION]
            if (p_strOrderCate.Trim() != "")
                strSql = strSql.Replace("[ORDERCATECONDITION]", "  and AA.ordercateid_chr in (" + p_strOrderCate + ") ");
            else
                strSql = strSql.Replace("[ORDERCATECONDITION]", "");
            #endregion

            long lngRes = -1;
            p_objResultArr = null;
            try
            {
                DataTable objDT = new DataTable();
                lngRes = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                if (lngRes > 0 && (objDT != null) && objDT.Rows.Count > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngGetCanExeOrderArrFromDataTable(objDT, out p_objResultArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                p_objResultArr = null;
            }
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
        /// <param name="arrOrder">��ִ��ҽ����Vo[out ���� ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCanExecuteOrderOnlyCan(string p_strAreaID, string p_strBedIDs, DateTime dtExecuteDate, out clsBIHCanExecOrder[] arrOrder)
        {
            string str1 = "";
            //��start
            string strSql = @"select *
  from (select   t1.orderid_chr,
       t1.orderdicid_chr,
       t1.registerid_chr,
       t1.patientid_chr,
       t1.executetype_int,
       t1.recipeno_int,
       t1.name_vchr,
       t1.spec_vchr,
       t1.execfreqid_chr,
       t1.dosage_dec,
       t1.execfreqname_chr,
       t1.dosageunit_chr,
       t1.get_dec,
       t1.useunit_chr,
       t1.getunit_chr,
       t1.dosetypeid_chr,
       t1.dosetypename_chr,
       t1.startdate_dat,
       t1.finishdate_dat,
       t1.execdeptid_chr,
       t1.execdeptname_chr,
       t1.entrust_vchr,
       t1.parentid_chr,
       t1.status_int,
       t1.creatorid_chr,
       t1.creator_chr,
       t1.createdate_dat,
       t1.posterid_chr,
       t1.poster_chr,
       t1.postdate_dat,
       t1.executorid_chr,
       t1.executor_chr,
       t1.executedate_dat,
       t1.stoperid_chr,
       t1.stoper_chr,
       t1.stopdate_dat,
       t1.retractorid_chr,
       t1.retractor_chr,
       t1.retractdate_dat,
       t1.isrich_int,
       t1.ratetype_int,
       t1.isrepare_int,
       t1.use_dec,
       t1.isneedfeel,
       t1.outgetmeddays_int,
       t1.assessoridforexec_chr,
       t1.assessorforexec_chr,
       t1.assessorforexec_dat,
       t1.assessoridforstop_chr,
       t1.assessorforstop_chr,
       t1.assessorforstop_dat,
       t1.backreason,
       t1.sendbackid_chr,
       t1.sendbacker_chr,
       t1.sendback_dat,
       t1.isyb_int,
       t1.sampleid_vchr,
       t1.lisappid_vchr,
       t1.partid_vchr,
       t1.createareaid_chr,
       t1.createareaname_vchr,
       t1.ifparentid_int,
       t1.confirmerid_chr,
       t1.confirmer_vchr,
       t1.confirm_dat,
       t1.attachtimes_int,
       t1.doctorid_chr,
       t1.doctor_vchr,
       t1.curareaid_chr,
       t1.curbedid_chr,
       t1.doctorgroupid_chr,
       t1.deleterid_chr,
       t1.deletername_vchr,
       t1.delete_dat,
       t1.sign_int,
       t1.operation_int,
       t1.remark_vchr,
       t1.recipeno2_int,
       t1.feelresult_vchr,
       t1.feel_int,
       t1.type_int,
       t1.charge_int,
       t1.singleamount_dec,
       t1.sourcetype_int,
       t1.chargedoctorgroupid_chr,
       t1.itemchargetype_vchr, t7.recipeno_int || '  ' || t7.name_vchr as parentname,
                 t3.lastname_vchr as patientname, t3.sex_chr as patientsex,
                 t2.areaid_chr, t2.bedid_chr as bedid, t4.code_chr,
                 bihpack.canexecute
                                   (t1.orderid_chr,
                                    t1.executetype_int,
                                    t1.status_int,
                                    t1.finishdate_dat,
                                    TO_DATE ('" + dtExecuteDate.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                                             'yyyy-mm-dd hh24:mi:ss'
                                            )
                                   ) execflag,
                 decode (t6.ipchargeflg_int,
                         1, round (t6.itemprice_mny / t6.packqty_dec, 4),
                         0, t6.itemprice_mny,
                         round (t6.itemprice_mny / t6.packqty_dec, 4)
                        ) itemprice,
                 t5.ordercateid_chr, t8.attarelaid_chr
            from t_opr_bih_order t1,
                 t_opr_bih_register t2,
                 t_bse_patient t3,
                 t_bse_bed t4,
                 t_bse_bih_orderdic t5,
                 t_bse_chargeitem t6,
                 t_opr_bih_order t7,
                 t_opr_attachrelation t8
           where t1.registerid_chr = t2.registerid_chr
             and t2.patientid_chr = t3.patientid_chr
             and t2.registerid_chr = t4.bihregisterid_chr
             and t1.orderdicid_chr = t5.orderdicid_chr(+)
             and t5.itemid_chr = t6.itemid_chr(+)
             and t1.parentid_chr = t7.orderid_chr(+)
             and t1.orderid_chr = t8.sourceitemid_vchr(+)
             and t2.pstatus_int <> 3
             [PatientCondition]
        order by t4.bedid_chr,
                 t1.patientid_chr,
                 t1.recipeno_int,
                 t1.createdate_dat,t1.orderid_chr) t
 where t.execflag = 1";

            if (p_strAreaID.Trim() != "")
            {
                str1 += " AND t2.areaid_chr='" + p_strAreaID.Trim() + "' ";
                if (p_strBedIDs.Trim() != "")
                {
                    str1 += @"
        AND t2.bedid_chr IN (" + p_strBedIDs.Trim() + ")";
                }
            }
            strSql = strSql.Replace("[PatientCondition]", str1);
            long lngRes = -1;
            arrOrder = null;
            try
            {
                DataTable objDT = new DataTable();
                lngRes = 0;
                lngRes = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                if (lngRes > 0 && (objDT != null) && objDT.Rows.Count > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngGetCanExeOrderArrFromDataTable(objDT, out arrOrder);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                arrOrder = null;
            }
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
        [AutoComplete]
        public long m_lngGetCanExecuteOrderOnlyCan(string p_strAreaID, string p_strBedIDs, string p_strOrderType, string p_strOrderStatus, string p_strOrderCate, bool p_blnNeedFeel, bool p_blnTakeMedicine, bool p_blnOnlyToday, string strCreatorID, DateTime dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            string str1 = "";
            //��start
            string strSql = @"
				select /*+all_rows*/ a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr,
                       a.executetype_int, a.recipeno_int, a.name_vchr, a.spec_vchr,
                       a.execfreqid_chr, a.dosage_dec, a.execfreqname_chr, a.dosageunit_chr,
                       a.get_dec, a.useunit_chr, a.getunit_chr, a.dosetypeid_chr,
                       a.dosetypename_chr, a.startdate_dat, a.finishdate_dat,
                       a.execdeptid_chr, a.execdeptname_chr, a.entrust_vchr, a.parentid_chr,
                       a.status_int, a.creatorid_chr, a.creator_chr, a.createdate_dat,
                       a.posterid_chr, a.poster_chr, a.postdate_dat, a.executorid_chr,
                       a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                       a.stopdate_dat, a.retractorid_chr, a.retractor_chr, a.retractdate_dat,
                       a.isrich_int, a.ratetype_int, a.isrepare_int, a.use_dec, a.isneedfeel,
                       a.outgetmeddays_int, a.assessoridforexec_chr, a.assessorforexec_chr,
                       a.assessorforexec_dat, a.assessoridforstop_chr, a.assessorforstop_chr,
                       a.assessorforstop_dat, a.backreason, a.sendbackid_chr,
                       a.sendbacker_chr, a.sendback_dat, a.isyb_int, a.sampleid_vchr,
                       a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                       a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr,
                       a.confirmer_vchr, a.confirm_dat, a.attachtimes_int, a.doctorid_chr,
                       a.doctor_vchr, a.curareaid_chr, a.curbedid_chr, a.doctorgroupid_chr,
                       a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                       a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr,
                       a.feel_int, a.charge_int, a.type_int, a.singleamount_dec,
                       a.sourcetype_int  
 					,(select REPLACE(trim(RECIPENO_INT || ' ' || NAME_VCHR),' ',' - ') from T_Opr_Bih_Order where Trim(T_Opr_Bih_Order.ORDERID_CHR)=Trim(A.PARENTID_CHR)) ParentName 	
 					,(select Name_Vchr from T_BSE_Patient where Trim(T_BSE_Patient.PatientID_Chr)=Trim(A.PatientID_Chr)) PatientName 	
 					,(select Sex_Chr from T_BSE_Patient where Trim(T_BSE_Patient.PatientID_Chr)=Trim(A.PatientID_Chr)) PatientSex 		
 					,(select AREAID_CHR from T_Opr_Bih_Register where T_Opr_Bih_Register.RegisterID_Chr=A.RegisterID_Chr ) AREAID_CHR 
 					,(select BedID_Chr from T_Opr_Bih_Register where T_Opr_Bih_Register.RegisterID_Chr=A.RegisterID_Chr ) BedID
 					,(select a1.Code_Chr from ([GetBedNOTable])a1 where a1.RegisterID_Chr=a.RegisterID_Chr) CODE_CHR 
 					,BIHPack.CanExecute(A.OrderID_Chr,A.ExecuteType_Int,A.Status_Int,A.FinishDate_Dat,[ExecuteDateValue]) ExecFlag
 					,(select a2.ItemPrice from ([GetItemPriceTable])a2 where a2.OrderDicID_Chr=a.OrderDicID_Chr) ItemPrice
					,(select ordercateid_chr from t_bse_bih_orderdic where t_bse_bih_orderdic.orderdicid_chr= a.orderdicid_chr) ordercateid_chr
				from T_Opr_Bih_Order A
				where 
					BIHPack.CanExecute(A.OrderID_Chr,A.ExecuteType_Int,A.Status_Int,A.FinishDate_Dat,[ExecuteDateValue])=1	
					and A.RegisterID_Chr in(SELECT registerid_chr FROM t_opr_bih_register where pstatus_int<>3)
					[ORDERTYPECONDITION]
					[ORDERSTATUSCONDITION]
					[TAKEMEDICINECONDITION]
					[ONLYTODAYCONDITION]
				order by registerid_chr,a.parentid_chr desc			
				";
            #region ����
            //��ȡ���� [��Ժ�Ǽ���ˮ�á�����]	=>[GetBedNOTable]
            str1 = "select RegisterID_Chr ,(select CODE_CHR from T_BSE_Bed where T_BSE_Bed.BedID_Chr=T_Opr_Bih_Register.BedID_Chr) Code_Chr from T_Opr_Bih_Register";
            strSql = strSql.Replace("[GetBedNOTable]", str1);
            //��ȡסԺ���� [������Ŀ��ˮ�š�סԺ����]	=>[GetItemPriceTable]	
            str1 = @"
					select OrderDicID_Chr,
						(select decode(IPCHARGEFLG_INT,1,Round(itemprice_mny/PackQty_Dec,4),0,itemprice_mny,Round(itemprice_mny/PackQty_Dec,4)) ItemPrice 
						from T_BSE_ChargeItem 
						where T_BSE_ChargeItem.itemid_chr=T_BSE_BIH_OrderDic.itemid_chr
						)ItemPrice 
					from T_BSE_BIH_OrderDic
				 ";

            strSql = strSql.Replace("[GetItemPriceTable]", str1);

            strSql = @"select AA.* from (" + strSql + ") AA " + " where [CreatorCondition] [PatientCondition] [NeedFeelCondition] [ORDERCATECONDITION] order by AA.BedID,AA.patientid_chr,AA.recipeno_int,AA.createdate_dat ";
            //��ѯ����
            if (strCreatorID.Trim() != "")
                strSql = strSql.Replace("[CreatorCondition]", " AA.CreatorID_Chr = '" + strCreatorID.Trim() + "' ");
            else
                strSql = strSql.Replace("[CreatorCondition]", " 1=1 ");
            str1 = "";
            if (p_strAreaID.Trim() != "")
            {
                str1 += " AND TRIM(AA.areaid_chr)='" + p_strAreaID.Trim() + "' ";
                if (p_strBedIDs.Trim() != "")
                {
                    str1 += " AND TRIM(AA.BedID) IN (" + p_strBedIDs.Trim() + ")";
                }
            }
            strSql = strSql.Replace("[PatientCondition]", str1);
            strSql = strSql.Replace("[ExecuteDateValue]", "to_date('" + dtExecuteDate.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')");
            //ҽ������	=>[ORDERTYPECONDITION]
            if (p_strOrderType.Trim() != "")
                strSql = strSql.Replace("[ORDERTYPECONDITION]", "  and A.EXECUTETYPE_INT in (" + p_strOrderType + ") ");
            else
                strSql = strSql.Replace("[ORDERTYPECONDITION]", "");
            //ִ��״̬	=>[ORDERSTATUSCONDITION]
            if (p_strOrderStatus.Trim() != "")
                strSql = strSql.Replace("[ORDERSTATUSCONDITION]", "  and A.STATUS_INT in (" + p_strOrderStatus + ") ");
            else
                strSql = strSql.Replace("[ORDERSTATUSCONDITION]", "");
            //Ƥ��
            if (p_blnNeedFeel)
                strSql = strSql.Replace("[NeedFeelCondition]", "  and AA.IsNeedFeeL=1 ");
            else
                strSql = strSql.Replace("[NeedFeelCondition]", "");
            //��Ժ��ҩ	=>[TAKEMEDICINECONDITION]
            if (p_blnTakeMedicine)
                strSql = strSql.Replace("[TAKEMEDICINECONDITION]", "  and A.EXECUTETYPE_INT=2 and A.RATETYPE_INT=4 ");
            else
                strSql = strSql.Replace("[TAKEMEDICINECONDITION]", "");
            //���Ե���	=>[ONLYTODAYCONDITION]
            if (p_blnTakeMedicine)
                strSql = strSql.Replace("[ONLYTODAYCONDITION]", "  and trunc(A.POSTDATE_DAT)=to_date(sysdate,'yyyy-mm-dd')");
            else
                strSql = strSql.Replace("[ONLYTODAYCONDITION]", "");
            //������Ŀ����	=>[ORDERCATECONDITION]
            if (p_strOrderCate.Trim() != "")
                strSql = strSql.Replace("[ORDERCATECONDITION]", "  and AA.ordercateid_chr in (" + p_strOrderCate + ") ");
            else
                strSql = strSql.Replace("[ORDERCATECONDITION]", "");
            #endregion

            long lngRes = -1;
            p_objResultArr = null;
            try
            {
                DataTable objDT = new DataTable();
                lngRes = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                if (lngRes > 0 && (objDT != null) && objDT.Rows.Count > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngGetCanExeOrderArrFromDataTable(objDT, out p_objResultArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                p_objResultArr = null;
            }
            return lngRes;
        }

        #endregion
        #region ��ȡҽ��-ִ��
        /// <summary>
        /// ��ȡ��ǰʱ�����ִ�е�ҽ��	����ҽ��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_strOrderIDArr">��ִ��ҽ��ID [out ���� ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCanExecuteOrderByOrderID(string p_strOrderID, out string[] p_strOrderIDArr)
        {
            p_strOrderIDArr = null;
            long lngRes = 0;

            string strSql = @"select orderid_chr from t_opr_bih_order"
                     + " where registerId_chr=(select registerid_chr from t_opr_bih_order where orderid_chr=[ORDERIDCONDITION])"
                     + " and (STATUS_INT=0 or STATUS_INT=1 or STATUS_INT=2)";
            strSql = strSql.Replace("[ORDERIDCONDITION]", "'" + p_strOrderID.Trim() + "' ");
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_strOrderIDArr = new string[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        p_strOrderIDArr[i1] = dtbResult.Rows[i1]["orderid_chr"].ToString();
                    }
                }
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
        /// ��ȡ��ִ��ҽ�� [����]
        /// </summary>
        /// <param name="objDT"></param>
        /// <param name="arrOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetCanExeOrderArrFromDataTable(DataTable objDT, out clsBIHCanExecOrder[] arrOrder)
        {
            arrOrder = null;
            if (objDT == null)
                return 0;

            arrOrder = new clsBIHCanExecOrder[objDT.Rows.Count];
            for (int i = 0; i < arrOrder.Length; i++)
            {
                arrOrder[i] = new clsBIHCanExecOrder();

                arrOrder[i].m_intExecFlag = clsConverter.ToInt(objDT.Rows[i]["ExecFlag"]);

                arrOrder[i].m_strOrderID = clsConverter.ToString(objDT.Rows[i]["Orderid_Chr"]).Trim();
                arrOrder[i].m_strOrderDicID = clsConverter.ToString(objDT.Rows[i]["Orderdicid_Chr"]).Trim();
                arrOrder[i].m_strRegisterID = clsConverter.ToString(objDT.Rows[i]["Registerid_Chr"]).Trim();
                arrOrder[i].m_strPatientID = clsConverter.ToString(objDT.Rows[i]["Patientid_Chr"]).Trim();
                arrOrder[i].m_intExecuteType = clsConverter.ToInt(objDT.Rows[i]["Executetype_Int"]);
                arrOrder[i].m_intRecipenNo = clsConverter.ToInt(objDT.Rows[i]["Recipeno_Int"]);

                arrOrder[i].m_strName = clsConverter.ToString(objDT.Rows[i]["Name_Vchr"]).Trim();
                arrOrder[i].m_strSpec = clsConverter.ToString(objDT.Rows[i]["Spec_Vchr"]).Trim();
                arrOrder[i].m_strExecFreqID = clsConverter.ToString(objDT.Rows[i]["Execfreqid_Chr"]).Trim();
                arrOrder[i].m_strExecFreqName = clsConverter.ToString(objDT.Rows[i]["Execfreqname_Chr"]).Trim();
                arrOrder[i].m_dmlDosage = clsConverter.ToDecimal(objDT.Rows[i]["Dosage_Dec"]);
                arrOrder[i].m_strDosageUnit = clsConverter.ToString(objDT.Rows[i]["Dosageunit_Chr"]).Trim();

                arrOrder[i].m_dmlUse = clsConverter.ToDecimal(objDT.Rows[i]["Use_Dec"]);
                arrOrder[i].m_strUseunit = clsConverter.ToString(objDT.Rows[i]["Useunit_Chr"]).Trim();
                arrOrder[i].m_dmlGet = clsConverter.ToDecimal(objDT.Rows[i]["Get_Dec"]);
                arrOrder[i].m_strGetunit = clsConverter.ToString(objDT.Rows[i]["Getunit_Chr"]).Trim();
                arrOrder[i].m_strDosetypeID = clsConverter.ToString(objDT.Rows[i]["Dosetypeid_Chr"]).Trim();
                arrOrder[i].m_strDosetypeName = clsConverter.ToString(objDT.Rows[i]["Dosetypename_Chr"]).Trim();


                arrOrder[i].m_dtStartDate = clsConverter.ToDateTime(objDT.Rows[i]["Startdate_Dat"]);
                arrOrder[i].m_dtFinishDate = clsConverter.ToDateTime(objDT.Rows[i]["Finishdate_Dat"]);
                arrOrder[i].m_strExecDeptID = clsConverter.ToString(objDT.Rows[i]["Execdeptid_Chr"]).Trim();
                arrOrder[i].m_strExecDeptName = clsConverter.ToString(objDT.Rows[i]["Execdeptname_Chr"]).Trim();
                arrOrder[i].m_strEntrust = clsConverter.ToString(objDT.Rows[i]["Entrust_Vchr"]).Trim();
                arrOrder[i].m_strParentID = clsConverter.ToString(objDT.Rows[i]["Parentid_Chr"]).Trim();
                arrOrder[i].m_strParentName = clsConverter.ToString(objDT.Rows[i]["ParentName"]).Trim();//����ҽ������

                arrOrder[i].m_intStatus = clsConverter.ToInt(objDT.Rows[i]["Status_Int"]);
                arrOrder[i].m_intIsRich = clsConverter.ToInt(objDT.Rows[i]["Isrich_Int"]);
                arrOrder[i].RateType = clsConverter.ToInt(objDT.Rows[i]["Ratetype_Int"]);
                arrOrder[i].m_intIsRepare = clsConverter.ToInt(objDT.Rows[i]["Isrepare_Int"]);

                arrOrder[i].m_strCreatorID = clsConverter.ToString(objDT.Rows[i]["Creatorid_Chr"]).Trim();
                arrOrder[i].m_strCreator = clsConverter.ToString(objDT.Rows[i]["Creator_Chr"]).Trim();
                arrOrder[i].m_dtCreatedate = clsConverter.ToDateTime(objDT.Rows[i]["Createdate_Dat"]);

                arrOrder[i].m_strPosterId = clsConverter.ToString(objDT.Rows[i]["Posterid_Chr"]).Trim();
                arrOrder[i].m_strPoster = clsConverter.ToString(objDT.Rows[i]["Poster_Chr"]).Trim();
                arrOrder[i].m_dtPostdate = clsConverter.ToDateTime(objDT.Rows[i]["Postdate_Dat"]);

                arrOrder[i].m_strExecutorID = clsConverter.ToString(objDT.Rows[i]["Executorid_Chr"]).Trim();
                arrOrder[i].m_strExecutor = clsConverter.ToString(objDT.Rows[i]["Executor_Chr"]).Trim();
                arrOrder[i].m_dtExecutedate = clsConverter.ToDateTime(objDT.Rows[i]["Executedate_Dat"]);

                arrOrder[i].m_strStoperID = clsConverter.ToString(objDT.Rows[i]["Stoperid_Chr"]).Trim();
                arrOrder[i].m_strStoper = clsConverter.ToString(objDT.Rows[i]["Stoper_Chr"]).Trim();
                arrOrder[i].m_dtStopdate = clsConverter.ToDateTime(objDT.Rows[i]["Stopdate_Dat"]);

                arrOrder[i].m_strRetractorID = clsConverter.ToString(objDT.Rows[i]["Retractorid_Chr"]).Trim();
                arrOrder[i].m_strRetractor = clsConverter.ToString(objDT.Rows[i]["Retractor_Chr"]).Trim();
                arrOrder[i].m_dtRetractdate = clsConverter.ToDateTime(objDT.Rows[i]["Retractdate_Dat"]);

                arrOrder[i].m_dmlPrice = clsConverter.ToDecimal(objDT.Rows[i]["ItemPrice"]);
                //arrOrder[i].m_dmlDosageRate=clsConverter.ToDecimal(objDT.Rows[i]["DosageRate"]);
                arrOrder[i].m_strPatientName = clsConverter.ToString(objDT.Rows[i]["PatientName"]).Trim();//��������
                arrOrder[i].m_strPatientSex = clsConverter.ToString(objDT.Rows[i]["PatientSex"]).Trim();
                arrOrder[i].m_strBedID = clsConverter.ToString(objDT.Rows[i]["BedID"]).Trim();
                arrOrder[i].m_strBedName = clsConverter.ToString(objDT.Rows[i]["CODE_CHR"]).Trim();

                arrOrder[i].m_intISNEEDFEEL = clsConverter.ToInt(objDT.Rows[i]["ISNEEDFEEL"]);
                arrOrder[i].m_strOrderDicCateID = clsConverter.ToString(objDT.Rows[i]["ordercateid_chr"]);
                /* ���ϼ������뵥ID*/
                arrOrder[i].m_strLISAPPID_VCHR = clsConverter.ToString(objDT.Rows[i]["LISAPPID_VCHR"]).ToString().Trim();
                /*<======================================*/
                if (objDT.Columns.Contains("AREAID_CHR"))
                {
                    arrOrder[i].m_strCREATEAREA_ID = clsConverter.ToString(objDT.Rows[i]["AREAID_CHR"]).ToString().Trim();
                }
                //������ύ
                arrOrder[i].m_strASSESSORIDFOREXEC_CHR = clsConverter.ToString(objDT.Rows[i]["ASSESSORIDFOREXEC_CHR"]).Trim();
                arrOrder[i].m_strASSESSORFOREXEC_CHR = clsConverter.ToString(objDT.Rows[i]["ASSESSORFOREXEC_CHR"]).Trim();
                if (objDT.Rows[i]["ASSESSORFOREXEC_DAT"] != System.DBNull.Value)
                {
                    try
                    {
                        arrOrder[i].m_strASSESSORFOREXEC_DAT = Convert.ToDateTime(objDT.Rows[i]["ASSESSORFOREXEC_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch
                    {
                    }
                }
                //���ֹͣ
                arrOrder[i].m_strASSESSORIDFORSTOP_CHR = clsConverter.ToString(objDT.Rows[i]["ASSESSORIDFORSTOP_CHR"]).Trim();
                arrOrder[i].m_strASSESSORFORSTOP_CHR = clsConverter.ToString(objDT.Rows[i]["ASSESSORFORSTOP_CHR"]).Trim();
                if (objDT.Rows[i]["ASSESSORFORSTOP_DAT"] != System.DBNull.Value)
                {
                    try
                    {
                        arrOrder[i].m_strASSESSORFORSTOP_DAT = Convert.ToDateTime(objDT.Rows[i]["ASSESSORFORSTOP_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch
                    {
                    }
                }
                try
                {
                    arrOrder[i].m_strATTARELAID_CHR = objDT.Rows[i]["attarelaid_chr"].ToString().Trim();
                }
                catch
                {
                }
            }

            return 1;
        }
        #endregion
        #region ��ȡҽ��-������ύ
        /// <summary>
        /// ��ȡҽ��-������ύ	{ִ��״̬=1-�ύ;}
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����Vo[out ���� ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderForAuditingExecute(string p_strAreaID, string p_strBedIDs, DateTime p_dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            string str1 = "";
            string strSql = @"SELECT   t1.orderid_chr, t1.orderdicid_chr, t1.registerid_chr, t1.patientid_chr,
                                       t1.executetype_int, t1.recipeno_int, t1.name_vchr, t1.spec_vchr,
                                       t1.execfreqid_chr, t1.dosage_dec, t1.execfreqname_chr, t1.dosageunit_chr,
                                       t1.get_dec, t1.useunit_chr, t1.getunit_chr, t1.dosetypeid_chr,
                                       t1.dosetypename_chr, t1.startdate_dat, t1.finishdate_dat,
                                       t1.execdeptid_chr, t1.execdeptname_chr, t1.entrust_vchr, t1.parentid_chr,
                                       t1.status_int, t1.creatorid_chr, t1.creator_chr, t1.createdate_dat,
                                       t1.posterid_chr, t1.poster_chr, t1.postdate_dat, t1.executorid_chr,
                                       t1.executor_chr, t1.executedate_dat, t1.stoperid_chr, t1.stoper_chr,
                                       t1.stopdate_dat, t1.retractorid_chr, t1.retractor_chr, t1.retractdate_dat,
                                       t1.isrich_int, t1.ratetype_int, t1.isrepare_int, t1.use_dec, t1.isneedfeel,
                                       t1.outgetmeddays_int, t1.assessoridforexec_chr, t1.assessorforexec_chr,
                                       t1.assessorforexec_dat, t1.assessoridforstop_chr, t1.assessorforstop_chr,
                                       t1.assessorforstop_dat, t1.backreason, t1.sendbackid_chr,
                                       t1.sendbacker_chr, t1.sendback_dat, t1.isyb_int, t1.sampleid_vchr,
                                       t1.lisappid_vchr, t1.partid_vchr, t1.createareaid_chr,
                                       t1.createareaname_vchr, t1.ifparentid_int, t1.confirmerid_chr,
                                       t1.confirmer_vchr, t1.confirm_dat, t1.attachtimes_int, t1.doctorid_chr,
                                       t1.doctor_vchr, t1.curareaid_chr, t1.curbedid_chr, t1.doctorgroupid_chr,
                                       t1.deleterid_chr, t1.deletername_vchr, t1.delete_dat, t1.sign_int,
                                       t1.operation_int, t1.remark_vchr, t1.recipeno2_int, t1.feelresult_vchr,
                                       t1.feel_int, t1.charge_int, t1.type_int, t1.singleamount_dec,
                                       t1.sourcetype_int, 
         t7.recipeno_int || ' ' || t7.name_vchr AS parentname,
         t3.lastname_vchr AS patientname, t3.sex_chr AS patientsex,
         t2.areaid_chr, t2.bedid_chr AS bedid, t4.code_chr,
         bihpack.canexecute (t1.orderid_chr,
                             t1.executetype_int,
                             t1.status_int,
                             t1.finishdate_dat,
                             TO_DATE ('" + p_dtExecuteDate.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                                      'yyyy-mm-dd hh24:mi:ss'
                                     )
                            ) execflag,
         DECODE (t6.ipchargeflg_int,
                 1, ROUND (t6.itemprice_mny / t6.packqty_dec, 4),
                 0, t6.itemprice_mny,
                 ROUND (t6.itemprice_mny / t6.packqty_dec, 4)
                ) itemprice,
         t5.ordercateid_chr, t8.attarelaid_chr
    FROM t_opr_bih_order t1,
         t_opr_bih_register t2,
         t_bse_patient t3,
         t_bse_bed t4,
         t_bse_bih_orderdic t5,
         t_bse_chargeitem t6,
         t_opr_bih_order t7,
         t_opr_attachrelation t8
   WHERE t1.registerid_chr = t2.registerid_chr
     AND t2.patientid_chr = t3.patientid_chr
     AND t2.bedid_chr = t4.bedid_chr
     AND t1.orderdicid_chr = t5.orderdicid_chr(+)
     AND t5.itemid_chr = t6.itemid_chr(+)
     AND t1.parentid_chr = t7.orderid_chr(+)
     AND t1.orderid_chr = t8.sourceitemid_vchr(+)
     AND t2.pstatus_int <> 3
     AND t1.status_int = 1
     [PatientCondition]
ORDER BY t4.bedid_chr, t1.patientid_chr, t1.recipeno_int, t1.createdate_dat";

            if (p_strAreaID.Trim() != "")
            {
                str1 += " AND t2.areaid_chr='" + p_strAreaID.Trim() + "' ";
                if (p_strBedIDs.Trim() != "")
                {
                    str1 += @"
        AND t2.bedid_chr IN (" + p_strBedIDs.Trim() + ")";
                }
            }
            strSql = strSql.Replace("[PatientCondition]", str1);

            long lngRes = -1;
            p_objResultArr = null;
            try
            {
                DataTable objDT = new DataTable();
                lngRes = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                if (lngRes > 0 && (objDT != null) && objDT.Rows.Count > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngGetCanExeOrderArrFromDataTable(objDT, out p_objResultArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                p_objResultArr = null;
            }
            return lngRes;
        }
        #endregion

        #region ��ȡҽ��-������ύ(�ж��Ƿ����)
        /// <summary>
        /// ��ȡҽ��-������ύ	{ִ��״̬=1-�ύ;}
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>

        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderForAuditingExecute(string p_strAreaID)
        {

            DataTable p_dtbOrderCate = new DataTable();


            string strSQL = @"	select 
                a.orderid_chr
               	from T_Opr_Bih_Order a,T_Opr_Bih_Register b 
				where a.registerid_chr=b.registerid_chr and  a.STATUS_INT=1
					
			    AND b.areaid_chr='[p_strAreaID]'";
            strSQL = strSQL.Replace("[p_strAreaID]", p_strAreaID.Trim());
            long lngRes = new clsHRPTableService().DoGetDataTable(strSQL, ref p_dtbOrderCate);

            if (lngRes >= 0)
            {
                if (p_dtbOrderCate.Rows.Count > 0)
                    return 1;
                else
                    return 0;
            }

            return -1;

        }
        #endregion
        #region ��ȡҽ��-���ֹͣ
        /// <summary>
        /// ��ȡҽ��-���ֹͣ	{ҽ������=������ִ��״̬=3-ֹͣ;}
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="p_dtExecuteDate">ִ��ʱ��</param>
        /// <param name="p_objResultArr">��ִ��ҽ����Vo[out ���� ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderForAuditingStop(string p_strAreaID, string p_strBedIDs, DateTime p_dtExecuteDate, out clsBIHCanExecOrder[] p_objResultArr)
        {
            string str1 = "";
            string strSql = @"SELECT   t1.orderid_chr, t1.orderdicid_chr, t1.registerid_chr, t1.patientid_chr,
                                       t1.executetype_int, t1.recipeno_int, t1.name_vchr, t1.spec_vchr,
                                       t1.execfreqid_chr, t1.dosage_dec, t1.execfreqname_chr, t1.dosageunit_chr,
                                       t1.get_dec, t1.useunit_chr, t1.getunit_chr, t1.dosetypeid_chr,
                                       t1.dosetypename_chr, t1.startdate_dat, t1.finishdate_dat,
                                       t1.execdeptid_chr, t1.execdeptname_chr, t1.entrust_vchr, t1.parentid_chr,
                                       t1.status_int, t1.creatorid_chr, t1.creator_chr, t1.createdate_dat,
                                       t1.posterid_chr, t1.poster_chr, t1.postdate_dat, t1.executorid_chr,
                                       t1.executor_chr, t1.executedate_dat, t1.stoperid_chr, t1.stoper_chr,
                                       t1.stopdate_dat, t1.retractorid_chr, t1.retractor_chr, t1.retractdate_dat,
                                       t1.isrich_int, t1.ratetype_int, t1.isrepare_int, t1.use_dec, t1.isneedfeel,
                                       t1.outgetmeddays_int, t1.assessoridforexec_chr, t1.assessorforexec_chr,
                                       t1.assessorforexec_dat, t1.assessoridforstop_chr, t1.assessorforstop_chr,
                                       t1.assessorforstop_dat, t1.backreason, t1.sendbackid_chr,
                                       t1.sendbacker_chr, t1.sendback_dat, t1.isyb_int, t1.sampleid_vchr,
                                       t1.lisappid_vchr, t1.partid_vchr, t1.createareaid_chr,
                                       t1.createareaname_vchr, t1.ifparentid_int, t1.confirmerid_chr,
                                       t1.confirmer_vchr, t1.confirm_dat, t1.attachtimes_int, t1.doctorid_chr,
                                       t1.doctor_vchr, t1.curareaid_chr, t1.curbedid_chr, t1.doctorgroupid_chr,
                                       t1.deleterid_chr, t1.deletername_vchr, t1.delete_dat, t1.sign_int,
                                       t1.operation_int, t1.remark_vchr, t1.recipeno2_int, t1.feelresult_vchr,
                                       t1.feel_int, t1.charge_int, t1.type_int, t1.singleamount_dec,
                                       t1.sourcetype_int, 
         t7.recipeno_int || '  ' || t7.name_vchr AS parentname,
         t3.lastname_vchr AS patientname, t3.sex_chr AS patientsex,
         t2.areaid_chr, t2.bedid_chr AS bedid, t4.code_chr,
         bihpack.canexecute (t1.orderid_chr,
                             t1.executetype_int,
                             t1.status_int,
                             t1.finishdate_dat,
                             TO_DATE ('" + p_dtExecuteDate.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                                      'yyyy-mm-dd hh24:mi:ss'
                                     )
                            ) execflag,
         DECODE (t6.ipchargeflg_int,
                 1, ROUND (t6.itemprice_mny / t6.packqty_dec, 4),
                 0, t6.itemprice_mny,
                 ROUND (t6.itemprice_mny / t6.packqty_dec, 4)
                ) itemprice,
         t5.ordercateid_chr, t8.attarelaid_chr
    FROM t_opr_bih_order t1,
         t_opr_bih_register t2,
         t_bse_patient t3,
         t_bse_bed t4,
         t_bse_bih_orderdic t5,
         t_bse_chargeitem t6,
         t_opr_bih_order t7,
         t_opr_attachrelation t8
   WHERE t1.registerid_chr = t2.registerid_chr
     AND t2.patientid_chr = t3.patientid_chr
     AND t2.bedid_chr = t4.bedid_chr
     AND t1.orderdicid_chr = t5.orderdicid_chr(+)
     AND t5.itemid_chr = t6.itemid_chr(+)
     AND t1.parentid_chr = t7.orderid_chr(+)
     AND t1.orderid_chr = t8.sourceitemid_vchr(+)
     AND t1.status_int = 3
     AND t1.executetype_int = 1
     [PatientCondition]
ORDER BY t4.bedid_chr, t1.patientid_chr, t1.recipeno_int, t1.createdate_dat";

            if (p_strAreaID.Trim() != "")
            {
                str1 += " AND t2.areaid_chr='" + p_strAreaID.Trim() + "' ";
                if (p_strBedIDs.Trim() != "")
                {
                    str1 += @"
        AND t2.bedid_chr IN (" + p_strBedIDs.Trim() + ")";
                }
            }
            strSql = strSql.Replace("[PatientCondition]", str1);

            long lngRes = -1;
            p_objResultArr = null;
            try
            {
                DataTable objDT = new DataTable();
                lngRes = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                if (lngRes > 0 && (objDT != null) && objDT.Rows.Count > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngGetCanExeOrderArrFromDataTable(objDT, out p_objResultArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                p_objResultArr = null;
            }
            return lngRes;
        }
        #endregion
        #region ִ��ҽ��
        #region Old ����
        /// <summary>
        /// ִ��ҽ��	[����ҽ��]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
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
        [AutoComplete]
        public long m_lngExecuteOrder(string strOrderID, out string strOrderExecID, bool blnIsRecruit, string strEmpID, string strEmpName, DateTime dtExecDate)
        {
            long lngRes = 0;
            strOrderExecID = "";
            #region ��������
            clsSQLParamDefinitionVO[] arrParams = new clsSQLParamDefinitionVO[6];
            arrParams[0] = new clsSQLParamDefinitionVO();
            arrParams[0].strParameter_Name = "strOrderID";
            arrParams[0].strParameter_Type = "Varchar2";
            arrParams[0].strParameter_Direction = "Input";
            arrParams[0].objParameter_Value = strOrderID;

            arrParams[1] = new clsSQLParamDefinitionVO();
            arrParams[1].strParameter_Name = "intIsRecruit";
            arrParams[1].strParameter_Type = "Int32";
            arrParams[1].strParameter_Direction = "Input";
            arrParams[1].objParameter_Value = (blnIsRecruit ? 1 : 0);

            arrParams[2] = new clsSQLParamDefinitionVO();
            arrParams[2].strParameter_Name = "strExecutor";
            arrParams[2].strParameter_Type = "Varchar2";
            arrParams[2].strParameter_Direction = "Input";
            arrParams[2].objParameter_Value = strEmpName;

            arrParams[3] = new clsSQLParamDefinitionVO();
            arrParams[3].strParameter_Name = "strExecutorID";
            arrParams[3].strParameter_Type = "Varchar2";
            arrParams[3].strParameter_Direction = "Input";
            arrParams[3].objParameter_Value = strEmpID;

            arrParams[4] = new clsSQLParamDefinitionVO();
            arrParams[4].strParameter_Name = "dtExecuteDate";
            arrParams[4].strParameter_Type = "Date";
            arrParams[4].strParameter_Direction = "Input";
            arrParams[4].objParameter_Value = dtExecDate;

            arrParams[5] = new clsSQLParamDefinitionVO();
            arrParams[5].strParameter_Name = "strExecOrderID";
            arrParams[5].strParameter_Type = "Varchar2";
            arrParams[5].strParameter_Direction = "InputOutput";
            arrParams[5].objParameter_Value = "";
            #endregion
            try
            {
                lngRes = 0;

                lngRes = new clsHRPTableService().lngExecuteParameterProc("BIHPack.ExecOrder", arrParams);
                if (lngRes > 0)
                {
                    strOrderExecID = arrParams[5].objParameter_Value.ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (lngRes <= 0 || strOrderExecID.Trim() == "")
            {
                throw (new Exception("ҽ��ִ�д���"));
            }
            return lngRes;
        }
        /// <summary>
        ///  ִ��ҽ��	[����ҽ��]	����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <param name="p_strOrderExecIDArr">ִ�е���¼ID [����] [out ������������ض��ID���ж��š������ָΪ��ִ��ʧ�ܡ�]</param>
        /// <param name="p_blnIsRecruitArr">ָ���Ƿ񲹴�(��ִ������) [����] </param>
        /// <param name="p_strEmpID">ִ��ҽ����ˮ�� [����] </param>
        /// <param name="p_strEmpName">ִ��ҽ������ [����] </param>
        /// <param name="p_dtExecDate">ִ������ [����] </param>
        /// <param name="p_strParentIDArr">����ҽ��ID [����] </param>
        /// <returns>����{-1=������ִ�в�������0=ִ�г�����1=�ɹ���}</returns>
        [AutoComplete]
        public long m_lngExecuteOrder(string[] p_strOrderIDArr, out string[] p_strOrderExecIDArr, bool[] p_blnIsRecruitArr, string p_strEmpID, string p_strEmpName, DateTime p_dtExecDate, string[] p_strParentIDArr)
        {
            long lngRes = 0;
            p_strOrderExecIDArr = new string[p_strOrderIDArr.Length];

            //����Ƿ����ִ��
            bool blnIsCanExecute = false;
            lngRes = 0;
            lngRes = m_lngCheckIsCanExecute(p_strOrderIDArr, p_strParentIDArr, out blnIsCanExecute);
            if (lngRes <= 0 || !blnIsCanExecute)
                return lngRes;

            //����ִ��ҽ��
            lngRes = 1;
            for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
            {
                if (lngRes > 0)
                {
                    lngRes = 0;

                    lngRes = m_lngExecuteOrder(p_strOrderIDArr[i1], out p_strOrderExecIDArr[i1], p_blnIsRecruitArr[i1], p_strEmpID, p_strEmpName, p_dtExecDate);

                }
            }
            if (lngRes <= 0)
            {
                throw (new System.Exception("ִ��ҽ������"));
            }
            return lngRes;
        }


        #endregion

        #region ִ��ҽ��
        /// <summary>
        /// ִ��ҽ��	[����ҽ��]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strOrderID">ҽ��ID</param>
        /// <param name="strOrderExecID">ִ�е���¼ID [out ������������ض��ID���ж��š������ָΪ��ִ��ʧ�ܡ�]</param>
        /// <param name="blnIsRecruit">ָ���Ƿ񲹴�(��ִ������)</param>
        /// <param name="strEmpID">ִ��ҽ����ˮ��</param>
        /// <param name="strEmpName">ִ��ҽ������</param>
        /// <param name="dtExecDate">ִ������</param>
        /// <param name="p_blnIsCharge">�÷��Ƿ��շ�</param>
        /// <returns></returns>
        /// <remarks>
        ///		1����ΪCom+�������ִ��ҽ��ʧ�ܣ��򱨴�ҽ��ִ�д��󣡡������ع���
        ///		2������������ע��Ҫ���쳣����
        ///	</remarks>
        [AutoComplete]
        public long m_lngExecuteOrder(string strOrderID, out string strOrderExecID, bool blnIsRecruit, string strEmpID, string strEmpName, DateTime dtExecDate, bool p_blnIsCharge)
        {
            long lngRes = 0;
            strOrderExecID = "";
            #region ��������
            clsSQLParamDefinitionVO[] arrParams = new clsSQLParamDefinitionVO[7];
            arrParams[0] = new clsSQLParamDefinitionVO();
            arrParams[0].strParameter_Name = "strOrderID";
            arrParams[0].strParameter_Type = "Varchar2";
            arrParams[0].strParameter_Direction = "Input";
            arrParams[0].objParameter_Value = strOrderID;

            arrParams[1] = new clsSQLParamDefinitionVO();
            arrParams[1].strParameter_Name = "intIsRecruit";
            arrParams[1].strParameter_Type = "Int32";
            arrParams[1].strParameter_Direction = "Input";
            arrParams[1].objParameter_Value = (blnIsRecruit ? 1 : 0);

            arrParams[2] = new clsSQLParamDefinitionVO();
            arrParams[2].strParameter_Name = "strExecutor";
            arrParams[2].strParameter_Type = "Varchar2";
            arrParams[2].strParameter_Direction = "Input";
            arrParams[2].objParameter_Value = strEmpName;

            arrParams[3] = new clsSQLParamDefinitionVO();
            arrParams[3].strParameter_Name = "strExecutorID";
            arrParams[3].strParameter_Type = "Varchar2";
            arrParams[3].strParameter_Direction = "Input";
            arrParams[3].objParameter_Value = strEmpID;

            arrParams[4] = new clsSQLParamDefinitionVO();
            arrParams[4].strParameter_Name = "dtExecuteDate";
            arrParams[4].strParameter_Type = "Date";
            arrParams[4].strParameter_Direction = "Input";
            arrParams[4].objParameter_Value = dtExecDate;

            arrParams[5] = new clsSQLParamDefinitionVO();
            arrParams[5].strParameter_Name = "intChargeForUsage";
            arrParams[5].strParameter_Type = "Int32";
            arrParams[5].strParameter_Direction = "Input";
            arrParams[5].objParameter_Value = (p_blnIsCharge) ? 1 : 0;//{1=�շѣ�0=���շ�}	

            arrParams[6] = new clsSQLParamDefinitionVO();
            arrParams[6].strParameter_Name = "strExecOrderID";
            arrParams[6].strParameter_Type = "Varchar2";
            arrParams[6].strParameter_Direction = "InputOutput";
            arrParams[6].objParameter_Value = "";

            #endregion
            try
            {
                lngRes = 0;
                lngRes = new clsHRPTableService().lngExecuteParameterProc("BIHPack.ExecOrder", arrParams);
                if (lngRes > 0)
                {
                    strOrderExecID = arrParams[6].objParameter_Value.ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (lngRes <= 0 || strOrderExecID.Trim() == "")
            {
                throw (new Exception("ҽ��ִ�д���"));
            }
            return lngRes;
        }
        /// <summary>
        ///  ִ��ҽ��	[����ҽ��]	����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
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
        [AutoComplete]
        public long m_lngExecuteOrder(string[] p_strOrderIDArr, string[] p_strRegisterIDArr, int[] p_intRecipenNoArr, out string[] p_strOrderExecIDArr, bool[] p_blnIsRecruitArr, string p_strEmpID, string p_strEmpName, DateTime p_dtExecDate, string[] p_strParentIDArr)
        {
            long lngRes = 0;
            ArrayList alHaveCharge = new ArrayList();//�Ѿ��շѵ�ҽ��	{��ʽ����Ժ�Ǽ�ID-���ţ����硰00000001-12��}
            p_strOrderExecIDArr = new string[p_strOrderIDArr.Length];

            //����Ƿ����ִ��
            #region ����Ƿ����ִ��
            bool blnIsCanExecute = false;
            lngRes = 0;
            lngRes = m_lngCheckIsCanExecute(p_strOrderIDArr, p_strParentIDArr, out blnIsCanExecute);
            if (lngRes <= 0 || !blnIsCanExecute)
                return lngRes;
            #endregion

            //����ִ��ҽ��
            lngRes = 1;
            bool blnIsCharge = true;
            for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
            {
                blnIsCharge = blnIsChargeUse(alHaveCharge, p_strRegisterIDArr[i1], p_intRecipenNoArr[i1]);
                if (lngRes > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngExecuteOrder(p_strOrderIDArr[i1], out p_strOrderExecIDArr[i1], p_blnIsRecruitArr[i1], p_strEmpID, p_strEmpName, p_dtExecDate, blnIsCharge, true);
                    alHaveCharge.Add(p_strRegisterIDArr[i1].ToString().Trim() + "-" + p_intRecipenNoArr[i1].ToString().Trim());
                }
            }
            if (lngRes <= 0)
            {
                throw (new System.Exception("ִ��ҽ������"));
            }
            return lngRes;
        }

        #endregion

        /// <summary>
        /// �ж��Ƿ����ִ��ҽ��
        /// ҵ��˵����	�Ӽ�ҽ�����ܵ���ִ�У�������丸��ҽ��һ��ִ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <param name="p_strParentIDArr">����ҽ��ID [����]</param>
        /// <param name="p_blnIsCanExecute">�Ƿ����ִ��ҽ����[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckIsCanExecute(string[] p_strOrderIDArr, string[] p_strParentIDArr, out bool p_blnIsCanExecute)
        {
            long lngRes = 0;
            p_blnIsCanExecute = false;
            if (p_strOrderIDArr.Length != p_strParentIDArr.Length)
            {
                p_blnIsCanExecute = false;
                return lngRes;
            }

            p_blnIsCanExecute = true;
            int i1, i2;
            bool blnExistFatherOrder;
            lngRes = 1;
            try
            {
                for (i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
                {
                    if (p_strParentIDArr[i1] != null && p_strParentIDArr[i1] != string.Empty)
                    {
                        blnExistFatherOrder = false;
                        for (i2 = 0; i2 < p_strOrderIDArr.Length; i2++)
                        {
                            if (p_strOrderIDArr[i2].Trim() == p_strParentIDArr[i1].Trim())
                            {
                                blnExistFatherOrder = true;
                                break;
                            }
                        }
                        if (!blnExistFatherOrder)
                        {
                            p_blnIsCanExecute = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                lngRes = 0;
            }
            return lngRes;
        }
        /// <summary>
        /// ����ҽ��ID	����
        /// ����: �Ա�ȷ��ͬ���ŵ�ҽ��ִ��һ��ִ�У�
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <param name="p_intStatus">ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}</param>
        /// <returns></returns>
        [AutoComplete]
        public string[] GetOrderIDSameRecipeNOForExecute(string[] p_strOrderIDArr)
        {
            string[] strReturnOrderIDArr = new string[0];
            if (p_strOrderIDArr.Length <= 0)
                return strReturnOrderIDArr;
            ArrayList myAL = new ArrayList();
            #region ��ȡҽ��ID
            long lngRes = 0;
            string strSQL = "";
            string strSQLTem = @"select a.orderid_chr
                                  from t_opr_bih_order a
                                 where a.registerid_chr = (select a1.registerid_chr
                                                             from t_opr_bih_order a1
                                                            where a1.orderid_chr = '[ORDERIDCONDITION]')
                                   and a.recipeno_int = (select a1.recipeno_int
                                                           from t_opr_bih_order a1
                                                          where a1.orderid_chr = '[ORDERIDCONDITION]')
                                   and bihpack.canexecute (a.orderid_chr,
                                                           a.executetype_int,
                                                           a.status_int,
                                                           a.finishdate_dat,
                                                           sysdate
                                                          ) = 1";
            for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
            {
                strSQL = strSQLTem;
                strSQL = strSQL.Replace("[ORDERIDCONDITION]", p_strOrderIDArr[i1].Trim());
                try
                {
                    DataTable dtbResult = new DataTable();
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = 0;
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                    objHRPSvc.Dispose();
                    if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                    {
                        for (int i2 = 0; i2 < dtbResult.Rows.Count; i2++)
                        {
                            bool blnHaveSame = false;
                            for (int i3 = 0; i3 < myAL.Count; i3++)
                            {
                                if (myAL[i3].ToString().Trim() == dtbResult.Rows[i2]["orderid_chr"].ToString().Trim())
                                {
                                    blnHaveSame = true;
                                    break;
                                }
                            }
                            if (!blnHaveSame)
                                myAL.Add(dtbResult.Rows[i2]["orderid_chr"].ToString().Trim());
                        }
                    }
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            #endregion

            strReturnOrderIDArr = new string[myAL.Count];
            for (int i1 = 0; i1 < myAL.Count; i1++)
                strReturnOrderIDArr[i1] = myAL[i1].ToString().Trim();
            return strReturnOrderIDArr;
        }

        /// <summary>
        /// ��ȡҽ���Ƿ�Ҫ�÷��շ�	�����ͬ���ŵ�ҽ�����÷�ֻ�շ�һ�Σ�
        /// </summary>
        /// <param name="p_myAL">�Ѿ��շѵ�ArrayList����	{��ʽ����Ժ�Ǽ�ID-���ţ����硰00000001-12��}</param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ�ID</param>
        /// <param name="p_intRecipenNo">����</param>
        /// <returns></returns>
        [AutoComplete]
        private bool blnIsChargeUse(ArrayList p_myAL, string p_strRegisterID, int p_intRecipenNo)
        {
            string[] strTemArr = new string[2];
            for (int i1 = 0; i1 < p_myAL.Count; i1++)
            {
                strTemArr = p_myAL[i1].ToString().Split(new Char[] { '-' });
                if (strTemArr[0].Trim() == p_strRegisterID && strTemArr[1].Trim() == p_intRecipenNo.ToString().Trim())
                    return false;
            }
            return true;
        }
        #endregion
        #region ������ύ
        /// <summary>
        /// ������ύ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">[����]	ҽ��ID</param>
        /// <param name="p_strHandlersID">������ID</param>
        /// <param name="p_strHandlers">����������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAuditingForExecute(string[] p_strOrderIDArr, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;
            string strOrderIDs = "";
            for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
            {
                if (p_strOrderIDArr[i1] != null && p_strOrderIDArr[i1].Trim() != "")
                {
                    strOrderIDs += (strOrderIDs.Trim() == "") ? (p_strOrderIDArr[i1]) : ("," + p_strOrderIDArr[i1]);
                }
            }
            if (strOrderIDs.Trim() == "")
                strOrderIDs = "''";
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += "UPDATE t_opr_bih_order A SET ";
            strSQL += "   A.STATUS_INT =5";     // ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}
            strSQL += "   ,A.ASSESSORIDFOREXEC_CHR ='" + p_strHandlersID.Trim() + "'";
            strSQL += "   ,A.ASSESSORFOREXEC_CHR ='" + p_strHandlers.Trim() + "'";
            strSQL += "   ,A.ASSESSORFOREXEC_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "	Where A.STATUS_INT=1 and Trim(A.ORDERID_CHR) in (" + strOrderIDs.Trim() + ")";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
        #region ��ȡҽ��ִ�е���Ϣ
        /// <summary>
        /// ��ȡҽ��ִ�е���Ϣ	����ִ�е�ID[����]
        /// </summary>
        /// <param name="arrOrderExecID">ִ�е�ID[����]</param>
        /// <param name="arrExecOrder">ִ�е�Vo����[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExecuteOrder(string[] arrOrderExecID, out clsBIHExecOrder[] arrExecOrder)
        {
            string strSql = @"select A.orderexecid_chr OrderExecID,A.creatorid_chr CreatorID,A.creator_chr Creator,A.createdate_dat CreateDate,
				A.executetime_int ExecuteTimes,A.executedays_int ExecuteDays ,A.executedate_vchr ExecuteDate,
				A.ischarge_int IsCharge,A.isincept_int IsIncept,A.isfirst_int IsFirst,A.isrecruit_int IsRecruit,A.status_int Status,
				A.operatorid_chr OperatorID,A.operator_chr OperatorName,A.deactivatorid_chr DeactivatorID,A.deactivator_chr Deactivator,A.deactivate_dat DeactivateDate,
				B.* ,TC.PatientName,TC.PatientSex,TC.BedID,TC.BedName
				from 
				( select orderexecid_chr, orderid_chr, creatorid_chr, creator_chr, createdate_dat, executetime_int, executedate_vchr, ischarge_int, isincept_int, 
isfirst_int, isrecruit_int, status_int, operatorid_chr, operator_chr, deactivatorid_chr, deactivator_chr, deactivate_dat, executedays_int, 
needconfirm_int, confirmerid_chr, confirmer_vchr, confirm_dat, print_date, exeareaid_chr, exebedid_chr, repare_int, autoid_vchr 
 from T_opr_Bih_OrderExecute where ORDEREXECID_CHR in ([ArrOrderExecIDValue])
				) A,
				(
					select  TB.orderid_chr, TB.orderdicid_chr, TB.registerid_chr, TB.patientid_chr, TB.executetype_int,
 TB.recipeno_int, TB.name_vchr, TB.spec_vchr, TB.execfreqid_chr, TB.dosage_dec, TB.execfreqname_chr,
  TB.dosageunit_chr, TB.get_dec, TB.useunit_chr, TB.getunit_chr, TB.dosetypeid_chr, TB.dosetypename_chr,
   TB.startdate_dat, TB.finishdate_dat, TB.execdeptid_chr, TB.execdeptname_chr, TB.entrust_vchr, TB.parentid_chr,
    TB.status_int, TB.creatorid_chr, TB.creator_chr, TB.createdate_dat, TB.posterid_chr, TB.poster_chr, TB.postdate_dat,
     TB.executorid_chr, TB.executor_chr, TB.executedate_dat, TB.stoperid_chr, TB.stoper_chr, TB.stopdate_dat, TB.retractorid_chr,
      TB.retractor_chr, TB.retractdate_dat, TB.isrich_int, TB.ratetype_int, TB.isrepare_int, TB.use_dec, TB.isneedfeel, TB.outgetmeddays_int,
       TB.assessoridforexec_chr, TB.assessorforexec_chr, TB.assessorforexec_dat, TB.assessoridforstop_chr, TB.assessorforstop_chr, TB.assessorforstop_dat,
        TB.backreason, TB.sendbackid_chr, TB.sendbacker_chr, TB.sendback_dat, TB.isyb_int, TB.sampleid_vchr, TB.lisappid_vchr, TB.partid_vchr, TB.createareaid_chr,
         TB.createareaname_vchr, TB.ifparentid_int, TB.confirmerid_chr, TB.confirmer_vchr, TB.confirm_dat, TB.attachtimes_int, TB.doctorid_chr, TB.doctor_vchr,
          TB.curareaid_chr, TB.curbedid_chr, TB.doctorgroupid_chr, TB.deleterid_chr, TB.deletername_vchr, TB.delete_dat, TB.sign_int, TB.operation_int, TB.remark_vchr,
           TB.recipeno2_int, TB.feelresult_vchr, TB.feel_int, TB.type_int, TB.charge_int, TB.singleamount_dec, TB.sourcetype_int, TB.chargedoctorgroupid_chr, TB.itemchargetype_vchr,TD.ItemPrice
					from T_Opr_Bih_Order TB,
					(
						select TA.OrderDicID_Chr,TB.Dosage_Dec DosageRate,TA.OrderCateID_Chr
						,decode(TB.IPCHARGEFLG_INT,1,Round(tb.itemprice_mny/TB.PackQty_Dec,4),0,tb.itemprice_mny,Round(tb.itemprice_mny/TB.PackQty_Dec,4)) ItemPrice 
						from T_BSE_BIH_OrderDic TA,T_BSE_ChargeItem TB
						where TA.ItemID_chr=TB.itemid_chr(+)
					) TD
					where TB.OrderDicID_Chr=TD.OrderDicID_Chr(+)
				) B,
				(
			     		select A.registerid_chr RegisterID,A.patientid_chr PatientID,
						B.name_vchr PatientName, B.sex_chr PatientSex,
						C.bedid_chr BedID,C.code_chr BedName
						from T_Opr_Bih_Register A,
						T_BSE_Patient B,
						T_BSE_Bed C
						where A.patientid_chr = B.patientid_chr(+)
						and A.areaid_chr=C.areaid_chr and A.bedid_chr=C.bedid_chr(+)
				) TC
				where A.orderid_chr=B.orderid_chr and B.RegisterID_Chr=TC.RegisterID(+)
				order by  B.RegisterID_Chr,B.RecipeNo_INT
				";

            string strArrOrderExecID = "";
            if (arrOrderExecID != null)
            {
                for (int i = 0; i < arrOrderExecID.Length; i++)
                {
                    if (i == 0)
                        strArrOrderExecID += "'" + arrOrderExecID[i] + "'";
                    else
                        strArrOrderExecID += ",'" + arrOrderExecID[i] + "'";
                }
            }
            if (strArrOrderExecID.Trim() == "")
                strArrOrderExecID = "''";
            strSql = strSql.Replace("[ArrOrderExecIDValue]", strArrOrderExecID);

            DataTable objDT = new DataTable();
            long ret = 0;
            try
            {
                ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if ((ret > 0) && (objDT != null))
            {
                m_lngGetExeOrderArrFromDataTable(objDT, out arrExecOrder);
                return 1;
            }
            else
            {
                arrExecOrder = null;
                return 0;
            }
        }

        /// <summary>
        /// ��ȡҽ��ִ�е�	����ҽ��ID	[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">ҽ��ִ�е�����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExecuteOrderByOrderID(string[] p_strOrderIDArr, out clsBIHExecOrder[] p_objResultArr)
        {
            p_objResultArr = new clsBIHExecOrder[0];
            long lngRes = 0;
            #region strSql
            string strSql = @"select A.orderexecid_chr OrderExecID,A.creatorid_chr CreatorID,A.creator_chr Creator,A.createdate_dat CreateDate,
				A.executetime_int ExecuteTimes,A.executedays_int ExecuteDays ,A.executedate_vchr ExecuteDate,
				A.ischarge_int IsCharge,A.isincept_int IsIncept,A.isfirst_int IsFirst,A.isrecruit_int IsRecruit,A.status_int Status,
				A.operatorid_chr OperatorID,A.operator_chr OperatorName,A.deactivatorid_chr DeactivatorID,A.deactivator_chr Deactivator,A.deactivate_dat DeactivateDate,
				B.* ,TC.PatientName,TC.PatientSex,TC.BedID,TC.BedName
				from 
				( select orderexecid_chr, orderid_chr, creatorid_chr, creator_chr, createdate_dat, executetime_int, executedate_vchr, ischarge_int, isincept_int, 
isfirst_int, isrecruit_int, status_int, operatorid_chr, operator_chr, deactivatorid_chr, deactivator_chr, deactivate_dat, executedays_int, 
needconfirm_int, confirmerid_chr, confirmer_vchr, confirm_dat, print_date, exeareaid_chr, exebedid_chr, repare_int, autoid_vchr 
 from T_opr_Bih_OrderExecute where ORDERID_CHR in ([GETORDERID])
				) A,
				( select TB.orderid_chr, TB.orderdicid_chr, TB.registerid_chr, TB.patientid_chr, TB.executetype_int,
 TB.recipeno_int, TB.name_vchr, TB.spec_vchr, TB.execfreqid_chr, TB.dosage_dec, TB.execfreqname_chr,
  TB.dosageunit_chr, TB.get_dec, TB.useunit_chr, TB.getunit_chr, TB.dosetypeid_chr, TB.dosetypename_chr,
   TB.startdate_dat, TB.finishdate_dat, TB.execdeptid_chr, TB.execdeptname_chr, TB.entrust_vchr, TB.parentid_chr,
    TB.status_int, TB.creatorid_chr, TB.creator_chr, TB.createdate_dat, TB.posterid_chr, TB.poster_chr, TB.postdate_dat,
     TB.executorid_chr, TB.executor_chr, TB.executedate_dat, TB.stoperid_chr, TB.stoper_chr, TB.stopdate_dat, TB.retractorid_chr,
      TB.retractor_chr, TB.retractdate_dat, TB.isrich_int, TB.ratetype_int, TB.isrepare_int, TB.use_dec, TB.isneedfeel, TB.outgetmeddays_int,
       TB.assessoridforexec_chr, TB.assessorforexec_chr, TB.assessorforexec_dat, TB.assessoridforstop_chr, TB.assessorforstop_chr, TB.assessorforstop_dat,
        TB.backreason, TB.sendbackid_chr, TB.sendbacker_chr, TB.sendback_dat, TB.isyb_int, TB.sampleid_vchr, TB.lisappid_vchr, TB.partid_vchr, TB.createareaid_chr,
         TB.createareaname_vchr, TB.ifparentid_int, TB.confirmerid_chr, TB.confirmer_vchr, TB.confirm_dat, TB.attachtimes_int, TB.doctorid_chr, TB.doctor_vchr,
          TB.curareaid_chr, TB.curbedid_chr, TB.doctorgroupid_chr, TB.deleterid_chr, TB.deletername_vchr, TB.delete_dat, TB.sign_int, TB.operation_int, TB.remark_vchr,
           TB.recipeno2_int, TB.feelresult_vchr, TB.feel_int, TB.type_int, TB.charge_int, TB.singleamount_dec, TB.sourcetype_int, TB.chargedoctorgroupid_chr, TB.itemchargetype_vchr,TD.ItemPrice
					from T_Opr_Bih_Order TB,
					( select TA.OrderDicID_Chr,TB.Dosage_Dec DosageRate,TA.OrderCateID_Chr
						,decode(TB.IPCHARGEFLG_INT,1,Round(tb.itemprice_mny/TB.PackQty_Dec,4),0,tb.itemprice_mny,Round(tb.itemprice_mny/TB.PackQty_Dec,4)) ItemPrice 
						from T_BSE_BIH_OrderDic TA,T_BSE_ChargeItem TB
						where TA.ItemID_chr=TB.itemid_chr(+)
					) TD
					where TB.OrderDicID_Chr=TD.OrderDicID_Chr(+)
				) B,
				( select A.registerid_chr RegisterID,A.patientid_chr PatientID,
						B.name_vchr PatientName, B.sex_chr PatientSex,
						C.bedid_chr BedID,C.code_chr BedName
						from T_Opr_Bih_Register A,
						T_BSE_Patient B,
						T_BSE_Bed C
						where A.patientid_chr = B.patientid_chr(+)
						and A.areaid_chr=C.areaid_chr and A.bedid_chr=C.bedid_chr(+)
				) TC
				where A.orderid_chr=B.orderid_chr and B.RegisterID_Chr=TC.RegisterID(+)
				order by  B.RegisterID_Chr,B.RecipeNo_INT
				";

            string strOrderID = "";
            if (p_strOrderIDArr != null)
            {
                for (int i = 0; i < p_strOrderIDArr.Length; i++)
                {
                    if (i == 0)
                        strOrderID += "'" + p_strOrderIDArr[i].Trim() + "'";
                    else
                        strOrderID += ",'" + p_strOrderIDArr[i].Trim() + "'";
                }
            }
            if (strOrderID.Trim() == "")
                strOrderID = "''";
            strSql = strSql.Replace("[GETORDERID]", strOrderID);
            #endregion

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_lngGetExeOrderArrFromDataTable(dtbResult, out p_objResultArr);
                }
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
        ///  ��ȡҽ��ִ�е���Ϣ	���ݲ�ѯ����
        /// </summary>
        /// <param name="strAreaID">����ID</param>
        /// <param name="strBedBegin">��ʼ����ID</param>
        /// <param name="strBedEnd">��������ID</param>
        /// <param name="dtExecute">ִ��ʱ��</param>
        /// <param name="strCreatorID">ִ����ID</param>
        /// <param name="arrExecOrder">ִ�е�Vo����[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExecuteOrder(string strAreaID, string strBedBegin, string strBedEnd, DateTime dtExecute, string strCreatorID, out clsBIHExecOrder[] arrExecOrder)
        {
            string strSql = @"
				select	A.orderexecid_chr OrderExecID,A.creatorid_chr CreatorID,A.creator_chr Creator,A.createdate_dat CreateDate,
						A.executetime_int ExecuteTimes,A.executedays_int ExecuteDays ,A.executedate_vchr ExecuteDate,
						A.ischarge_int IsCharge,A.isincept_int IsIncept,A.isfirst_int IsFirst,A.isrecruit_int IsRecruit,A.status_int Status,
						A.operatorid_chr OperatorID,A.operator_chr OperatorName,A.deactivatorid_chr DeactivatorID,A.deactivator_chr Deactivator,A.deactivate_dat DeactivateDate,
						B.* ,C.PatientName,C.PatientSex,C.BedID,C.BedName
						from T_opr_Bih_OrderExecute A,
						(
							select B1.orderid_chr, B1.orderdicid_chr, B1.registerid_chr, B1.patientid_chr, B1.executetype_int,
 B1.recipeno_int, B1.name_vchr, B1.spec_vchr, B1.execfreqid_chr, B1.dosage_dec, B1.execfreqname_chr,
  B1.dosageunit_chr, B1.get_dec, B1.useunit_chr, B1.getunit_chr, B1.dosetypeid_chr, B1.dosetypename_chr,
   B1.startdate_dat, B1.finishdate_dat, B1.execdeptid_chr, B1.execdeptname_chr, B1.entrust_vchr, B1.parentid_chr,
    B1.status_int, B1.creatorid_chr, B1.creator_chr, B1.createdate_dat, B1.posterid_chr, B1.poster_chr, B1.postdate_dat,
     B1.executorid_chr, B1.executor_chr, B1.executedate_dat, B1.stoperid_chr, B1.stoper_chr, B1.stopdate_dat, B1.retractorid_chr,
      B1.retractor_chr, B1.retractdate_dat, B1.isrich_int, B1.ratetype_int, B1.isrepare_int, B1.use_dec, B1.isneedfeel, B1.outgetmeddays_int,
       B1.assessoridforexec_chr, B1.assessorforexec_chr, B1.assessorforexec_dat, B1.assessoridforstop_chr, B1.assessorforstop_chr, B1.assessorforstop_dat,
        B1.backreason, B1.sendbackid_chr, B1.sendbacker_chr, B1.sendback_dat, B1.isyb_int, B1.sampleid_vchr, B1.lisappid_vchr, B1.partid_vchr, B1.createareaid_chr,
         B1.createareaname_vchr, B1.ifparentid_int, B1.confirmerid_chr, B1.confirmer_vchr, B1.confirm_dat, B1.attachtimes_int, B1.doctorid_chr, B1.doctor_vchr,
          B1.curareaid_chr, B1.curbedid_chr, B1.doctorgroupid_chr, B1.deleterid_chr, B1.deletername_vchr, B1.delete_dat, B1.sign_int, B1.operation_int, B1.remark_vchr,
           B1.recipeno2_int, B1.feelresult_vchr, B1.feel_int, B1.type_int, B1.charge_int, B1.singleamount_dec, B1.sourcetype_int, B1.chargedoctorgroupid_chr, B1.itemchargetype_vchr,B2.ItemPrice
							from T_Opr_Bih_Order B1,
							(
								select B11.OrderDicID_Chr ,B12.Dosage_Dec DosageRate,B11.OrderCateID_Chr
										,decode(B12.IPCHARGEFLG_INT,1,Round(b12.itemprice_mny/B12.PackQty_Dec,4),0,b12.itemprice_mny,Round(b12.itemprice_mny/B12.PackQty_Dec,4)) ItemPrice
								from T_BSE_BIH_OrderDic B11,T_BSE_ChargeItem B12
								where B11.ItemID_chr=B12.itemid_chr(+)
							) B2
							where B1.OrderDicID_Chr=B2.OrderDicID_Chr(+)
						) B,
						(
			     				select	C1.registerid_chr RegisterID,C1.patientid_chr PatientID,
										C2.name_vchr PatientName, C2.sex_chr PatientSex,
										C3.bedid_chr BedID,C3.code_chr BedName
								from T_Opr_Bih_Register C1,T_BSE_Patient C2,T_BSE_Bed C3
								where C1.patientid_chr = C2.patientid_chr
								and C1.areaid_chr=C3.areaid_chr and C1.bedid_chr=C3.bedid_chr
								[PatientCondition]								
						) C
						where	A.orderid_chr=B.orderid_chr and B.RegisterID_Chr =C.RegisterID
								and BIHPack.GetDate(A.CreateDate_Dat) = to_date('[CreateDateValue]','yyyy-mm-dd')
								[CreatorCondition]
						order by  B.RegisterID_Chr,B.RecipeNo_INT
			";

            string strPatientCondition = m_strGetPatientsSQL("C3", strAreaID, strBedBegin, strBedEnd);
            strSql = strSql.Replace("[PatientCondition]", strPatientCondition);
            strSql = strSql.Replace("[CreateDateValue]", dtExecute.ToString("yyyy-MM-dd"));
            if (strCreatorID.Trim() != "")
                strSql = strSql.Replace("[CreatorCondition]", " and A.CreatorID_Chr = '" + strCreatorID.Trim() + "' ");
            else
                strSql = strSql.Replace("[CreatorCondition]", "");

            DataTable objDT = new DataTable();
            long ret = 0;
            try
            {
                ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if ((ret > 0) && (objDT != null))
            {
                m_lngGetExeOrderArrFromDataTable(objDT, out arrExecOrder);
                return 1;
            }
            else
            {
                arrExecOrder = null;
                return 0;
            }
        }
        /// <summary>
        ///  ��ȡҽ��ִ�е���Ϣ	���ݲ�ѯ����
        /// </summary>
        /// <param name="p_strAreaID">����ID</param>
        /// <param name="p_strBedIDs">�ö��ŷָ��Ĳ���ID	{���Ϊ������Ϊ��ѯ����}</param>
        /// <param name="dtExecute">ִ��ʱ��</param>
        /// <param name="strCreatorID">ִ����ID</param>
        /// <param name="arrExecOrder">ִ�е�Vo����[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExecuteOrder(string p_strAreaID, string p_strBedIDs, DateTime dtExecute, string strCreatorID, out clsBIHExecOrder[] arrExecOrder)
        {

            string strSql = @"
				select	A.orderexecid_chr OrderExecID,A.creatorid_chr CreatorID,A.creator_chr Creator,A.createdate_dat CreateDate,
						A.executetime_int ExecuteTimes,A.executedays_int ExecuteDays ,A.executedate_vchr ExecuteDate,
						A.ischarge_int IsCharge,A.isincept_int IsIncept,A.isfirst_int IsFirst,A.isrecruit_int IsRecruit,A.status_int Status,
						A.operatorid_chr OperatorID,A.operator_chr OperatorName,A.deactivatorid_chr DeactivatorID,A.deactivator_chr Deactivator,A.deactivate_dat DeactivateDate,
						B.* ,C.PatientName,C.PatientSex,C.BedID,C.BedName,   D.CLACAREA_CHR,D.CREATEAREA_CHR,E.DEPTNAME_VCHR
						from T_opr_Bih_OrderExecute A,
						(
							select B1.orderid_chr, B1.orderdicid_chr, B1.registerid_chr, B1.patientid_chr, B1.executetype_int,
 B1.recipeno_int, B1.name_vchr, B1.spec_vchr, B1.execfreqid_chr, B1.dosage_dec, B1.execfreqname_chr,
  B1.dosageunit_chr, B1.get_dec, B1.useunit_chr, B1.getunit_chr, B1.dosetypeid_chr, B1.dosetypename_chr,
   B1.startdate_dat, B1.finishdate_dat, B1.execdeptid_chr, B1.execdeptname_chr, B1.entrust_vchr, B1.parentid_chr,
    B1.status_int, B1.creatorid_chr, B1.creator_chr, B1.createdate_dat, B1.posterid_chr, B1.poster_chr, B1.postdate_dat,
     B1.executorid_chr, B1.executor_chr, B1.executedate_dat, B1.stoperid_chr, B1.stoper_chr, B1.stopdate_dat, B1.retractorid_chr,
      B1.retractor_chr, B1.retractdate_dat, B1.isrich_int, B1.ratetype_int, B1.isrepare_int, B1.use_dec, B1.isneedfeel, B1.outgetmeddays_int,
       B1.assessoridforexec_chr, B1.assessorforexec_chr, B1.assessorforexec_dat, B1.assessoridforstop_chr, B1.assessorforstop_chr, B1.assessorforstop_dat,
        B1.backreason, B1.sendbackid_chr, B1.sendbacker_chr, B1.sendback_dat, B1.isyb_int, B1.sampleid_vchr, B1.lisappid_vchr, B1.partid_vchr, B1.createareaid_chr,
         B1.createareaname_vchr, B1.ifparentid_int, B1.confirmerid_chr, B1.confirmer_vchr, B1.confirm_dat, B1.attachtimes_int, B1.doctorid_chr, B1.doctor_vchr,
          B1.curareaid_chr, B1.curbedid_chr, B1.doctorgroupid_chr, B1.deleterid_chr, B1.deletername_vchr, B1.delete_dat, B1.sign_int, B1.operation_int, B1.remark_vchr,
           B1.recipeno2_int, B1.feelresult_vchr, B1.feel_int, B1.type_int, B1.charge_int, B1.singleamount_dec, B1.sourcetype_int, B1.chargedoctorgroupid_chr, B1.itemchargetype_vchr,B2.ItemPrice   ,B2.Itemid_Chr
							from T_Opr_Bih_Order B1,
							(
								select B11.OrderDicID_Chr ,B12.Dosage_Dec DosageRate,B11.OrderCateID_Chr
										,decode(B12.IPCHARGEFLG_INT,1,Round(b12.itemprice_mny/B12.PackQty_Dec,4),0,b12.itemprice_mny,Round(b12.itemprice_mny/B12.PackQty_Dec,4)) ItemPrice
                                        ,B12.Itemid_Chr
								from T_BSE_BIH_OrderDic B11,T_BSE_ChargeItem B12
								where B11.ItemID_chr=B12.itemid_chr(+)
							) B2
							where B1.OrderDicID_Chr=B2.OrderDicID_Chr(+)
						) B,
						(
			     				select	C1.registerid_chr RegisterID,C1.patientid_chr PatientID,
										C2.name_vchr PatientName, C2.sex_chr PatientSex,
										C3.bedid_chr BedID,C3.code_chr BedName
								from T_Opr_Bih_Register C1,T_BSE_Patient C2,T_BSE_Bed C3
								where C1.patientid_chr = C2.patientid_chr
								and C1.areaid_chr=C3.areaid_chr and C1.bedid_chr=C3.bedid_chr and C1.PSTATUS_INT<>3 
								[PatientCondition]								
						) C

                        ,
                        T_OPR_BIH_ORDERCHARGEDEPT D,T_BSE_DEPTDESC E

						where	A.orderid_chr=B.orderid_chr

                                and A.orderid_chr=D.ORDERID_CHR(+)
                                and D.Chargeitemid_Chr=B.Itemid_Chr(+)
                                and D.CLACAREA_CHR=E.DEPTID_CHR(+)

                                and B.RegisterID_Chr =C.RegisterID
								and BIHPack.GetDate(A.CreateDate_Dat) = to_date('[CreateDateValue]','yyyy-mm-dd')
								[CreatorCondition]
						order by  B.RegisterID_Chr,B.RecipeNo_INT
			";

            string str1 = "";
            if (p_strAreaID.Trim() != "")
            {
                str1 += " AND TRIM(C3.areaid_chr)='" + p_strAreaID.Trim() + "' ";
                if (p_strBedIDs.Trim() != "")
                {
                    str1 += " AND TRIM(C3.bedid_chr) IN (" + p_strBedIDs.Trim() + ")";
                }
            }
            strSql = strSql.Replace("[PatientCondition]", str1);
            strSql = strSql.Replace("[CreateDateValue]", dtExecute.ToString("yyyy-MM-dd"));
            if (strCreatorID.Trim() != "")
                strSql = strSql.Replace("[CreatorCondition]", " and A.CreatorID_Chr = '" + strCreatorID.Trim() + "' ");
            else
                strSql = strSql.Replace("[CreatorCondition]", "");

            DataTable objDT = new DataTable();
            long ret = 0;
            try
            {
                ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if ((ret > 0) && (objDT != null))
            {
                m_lngGetExeOrderArrFromDataTable(objDT, out arrExecOrder);
                return 1;
            }
            else
            {
                arrExecOrder = null;
                return 0;
            }
        }
        /// <summary>
        /// ��ȡҽ��ִ�е���Ϣ	����DataTable��
        /// </summary>
        /// <param name="objDT">DataTable��</param>
        /// <param name="arrOrder">ִ�е�Vo����[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetExeOrderArrFromDataTable(DataTable objDT, out clsBIHExecOrder[] arrOrder)
        {
            arrOrder = null;
            if (objDT == null)
                return 0;

            arrOrder = new clsBIHExecOrder[objDT.Rows.Count];
            for (int i = 0; i < arrOrder.Length; i++)
            {
                arrOrder[i] = new clsBIHExecOrder();

                arrOrder[i].m_strEOrderExecID = clsConverter.ToString(objDT.Rows[i]["OrderExecID"]).Trim();

                arrOrder[i].m_strECreator = clsConverter.ToString(objDT.Rows[i]["Creator"]).Trim();
                arrOrder[i].m_strECreatorID = clsConverter.ToString(objDT.Rows[i]["CreatorID"]).Trim();
                arrOrder[i].m_dtECreateDate = clsConverter.ToDateTime(objDT.Rows[i]["CreateDate"]);

                arrOrder[i].m_intEExecuteTimes = clsConverter.ToInt(objDT.Rows[i]["ExecuteTimes"]);
                arrOrder[i].m_intEExecuteDays = clsConverter.ToInt(objDT.Rows[i]["ExecuteDays"]);
                arrOrder[i].m_strEExecuteDate = clsConverter.ToString(objDT.Rows[i]["ExecuteDate"]).Trim();

                arrOrder[i].m_intEIsCharge = clsConverter.ToInt(objDT.Rows[i]["IsCharge"]);
                arrOrder[i].m_intEIsIncept = clsConverter.ToInt(objDT.Rows[i]["IsIncept"]);
                arrOrder[i].m_intEIsRecruit = clsConverter.ToInt(objDT.Rows[i]["IsRecruit"]);
                arrOrder[i].m_intEStatus = clsConverter.ToInt(objDT.Rows[i]["Status"]);

                arrOrder[i].m_strEOperator = clsConverter.ToString(objDT.Rows[i]["OperatorName"]).Trim();
                arrOrder[i].m_strEOperatorID = clsConverter.ToString(objDT.Rows[i]["OperatorID"]).Trim();

                arrOrder[i].m_strEDeactivator = clsConverter.ToString(objDT.Rows[i]["Deactivator"]).Trim();
                arrOrder[i].m_strEDeactivatorID = clsConverter.ToString(objDT.Rows[i]["DeactivatorID"]).Trim();
                arrOrder[i].m_dtEDeactivateDate = clsConverter.ToDateTime(objDT.Rows[i]["DeactivateDate"]);

                #region CanExecOrder Part

                arrOrder[i].m_strOrderID = clsConverter.ToString(objDT.Rows[i]["Orderid_Chr"]).Trim();
                arrOrder[i].m_strOrderDicID = clsConverter.ToString(objDT.Rows[i]["Orderdicid_Chr"]).Trim();
                arrOrder[i].m_strRegisterID = clsConverter.ToString(objDT.Rows[i]["Registerid_Chr"]).Trim();
                arrOrder[i].m_strPatientID = clsConverter.ToString(objDT.Rows[i]["Patientid_Chr"]).Trim();
                arrOrder[i].m_intExecuteType = clsConverter.ToInt(objDT.Rows[i]["Executetype_Int"]);
                arrOrder[i].m_intRecipenNo = clsConverter.ToInt(objDT.Rows[i]["Recipeno_Int"]);

                arrOrder[i].m_strName = clsConverter.ToString(objDT.Rows[i]["Name_Vchr"]).Trim();
                arrOrder[i].m_strSpec = clsConverter.ToString(objDT.Rows[i]["Spec_Vchr"]).Trim();
                arrOrder[i].m_strExecFreqID = clsConverter.ToString(objDT.Rows[i]["Execfreqid_Chr"]).Trim();
                arrOrder[i].m_strExecFreqName = clsConverter.ToString(objDT.Rows[i]["Execfreqname_Chr"]).Trim();
                arrOrder[i].m_dmlDosage = clsConverter.ToDecimal(objDT.Rows[i]["Dosage_Dec"]);
                arrOrder[i].m_strDosageUnit = clsConverter.ToString(objDT.Rows[i]["Dosageunit_Chr"]).Trim();

                arrOrder[i].m_dmlUse = clsConverter.ToDecimal(objDT.Rows[i]["Use_Dec"]);
                arrOrder[i].m_strUseunit = clsConverter.ToString(objDT.Rows[i]["Useunit_Chr"]).Trim();
                arrOrder[i].m_dmlGet = clsConverter.ToDecimal(objDT.Rows[i]["Get_Dec"]);
                arrOrder[i].m_strGetunit = clsConverter.ToString(objDT.Rows[i]["Getunit_Chr"]).Trim();
                arrOrder[i].m_strDosetypeID = clsConverter.ToString(objDT.Rows[i]["Dosetypeid_Chr"]).Trim();
                arrOrder[i].m_strDosetypeName = clsConverter.ToString(objDT.Rows[i]["Dosetypename_Chr"]).Trim();


                arrOrder[i].m_dtStartDate = clsConverter.ToDateTime(objDT.Rows[i]["Startdate_Dat"]);
                arrOrder[i].m_dtFinishDate = clsConverter.ToDateTime(objDT.Rows[i]["Finishdate_Dat"]);
                arrOrder[i].m_strExecDeptID = clsConverter.ToString(objDT.Rows[i]["Execdeptid_Chr"]).Trim();
                arrOrder[i].m_strExecDeptName = clsConverter.ToString(objDT.Rows[i]["Execdeptname_Chr"]).Trim();
                arrOrder[i].m_strEntrust = clsConverter.ToString(objDT.Rows[i]["Entrust_Vchr"]).Trim();
                arrOrder[i].m_strParentID = clsConverter.ToString(objDT.Rows[i]["Parentid_Chr"]).Trim();

                arrOrder[i].m_intStatus = clsConverter.ToInt(objDT.Rows[i]["Status_Int"]);
                arrOrder[i].m_intIsRich = clsConverter.ToInt(objDT.Rows[i]["Isrich_Int"]);
                arrOrder[i].RateType = clsConverter.ToInt(objDT.Rows[i]["Ratetype_Int"]);
                arrOrder[i].m_intIsRepare = clsConverter.ToInt(objDT.Rows[i]["Isrepare_Int"]);

                arrOrder[i].m_strCreatorID = clsConverter.ToString(objDT.Rows[i]["Creatorid_Chr"]).Trim();
                arrOrder[i].m_strCreator = clsConverter.ToString(objDT.Rows[i]["Creator_Chr"]).Trim();
                arrOrder[i].m_dtCreatedate = clsConverter.ToDateTime(objDT.Rows[i]["Createdate_Dat"]);

                arrOrder[i].m_strPosterId = clsConverter.ToString(objDT.Rows[i]["Posterid_Chr"]).Trim();
                arrOrder[i].m_strPoster = clsConverter.ToString(objDT.Rows[i]["Poster_Chr"]).Trim();
                arrOrder[i].m_dtPostdate = clsConverter.ToDateTime(objDT.Rows[i]["Postdate_Dat"]);

                arrOrder[i].m_strExecutorID = clsConverter.ToString(objDT.Rows[i]["Executorid_Chr"]).Trim();
                arrOrder[i].m_strExecutor = clsConverter.ToString(objDT.Rows[i]["Executor_Chr"]).Trim();
                arrOrder[i].m_dtExecutedate = clsConverter.ToDateTime(objDT.Rows[i]["Executedate_Dat"]);

                arrOrder[i].m_strStoperID = clsConverter.ToString(objDT.Rows[i]["Stoperid_Chr"]).Trim();
                arrOrder[i].m_strStoper = clsConverter.ToString(objDT.Rows[i]["Stoper_Chr"]).Trim();
                arrOrder[i].m_dtStopdate = clsConverter.ToDateTime(objDT.Rows[i]["Stopdate_Dat"]);

                arrOrder[i].m_strRetractorID = clsConverter.ToString(objDT.Rows[i]["Retractorid_Chr"]).Trim();
                arrOrder[i].m_strRetractor = clsConverter.ToString(objDT.Rows[i]["Retractor_Chr"]).Trim();
                arrOrder[i].m_dtRetractdate = clsConverter.ToDateTime(objDT.Rows[i]["Retractdate_Dat"]);

                arrOrder[i].m_dmlPrice = clsConverter.ToDecimal(objDT.Rows[i]["ItemPrice"]);
                //arrOrder[i].m_dmlDosageRate=clsConverter.ToDecimal(objDT.Rows[i]["DosageRate"]);
                arrOrder[i].m_strPatientName = clsConverter.ToString(objDT.Rows[i]["PatientName"]).Trim();
                arrOrder[i].m_strPatientSex = clsConverter.ToString(objDT.Rows[i]["PatientSex"]).Trim();
                arrOrder[i].m_strBedID = clsConverter.ToString(objDT.Rows[i]["BedID"]).Trim();
                arrOrder[i].m_strBedName = clsConverter.ToString(objDT.Rows[i]["BedName"]).Trim();
                if ((arrOrder[i].m_intExecuteType == 1) && (arrOrder[i].m_intEIsRecruit == 1))
                    arrOrder[i].m_intEIsFirst = 3;
                else
                    arrOrder[i].m_intEIsFirst = clsConverter.ToInt(objDT.Rows[i]["Executetype_Int"]);
                arrOrder[i].m_intISFIRST_INT = clsConverter.ToInt(objDT.Rows[i]["IsFirst"]);

                if (objDT.Columns.Contains("CREATEAREA_CHR"))
                    arrOrder[i].m_strCREATEAREA_ID = clsConverter.ToString(objDT.Rows[i]["CREATEAREA_CHR"].ToString());
                if (objDT.Columns.Contains("CLACAREA_CHR"))
                    arrOrder[i].m_strExecDeptID = clsConverter.ToString(objDT.Rows[i]["CLACAREA_CHR"].ToString());
                if (objDT.Columns.Contains("DEPTNAME_VCHR"))
                    arrOrder[i].m_strExecDeptName = clsConverter.ToString(objDT.Rows[i]["DEPTNAME_VCHR"].ToString());
                #endregion
            }
            return 1;
        }
        #endregion
        #region  ��ȡ���˷�����ϸ
        /// <summary>
        /// ��ȡ���˷�����ϸ	����ִ�е�ID[����]
        /// </summary>
        /// <param name="arrOrderExecID">ִ�е�ID	[����]</param>
        /// <param name="arrCharge">���˷�����ϸ��Vo����	[����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeRecord(string[] arrOrderExecID, out clsBIHPatientCharge[] arrCharge)
        {
            string strSql = @"
				select ta.pchargeid_chr, ta.patientid_chr, ta.registerid_chr, ta.active_dat, ta.orderid_chr, ta.orderexectype_int, ta.orderexecid_chr, 
ta.clacarea_chr, ta.createarea_chr, ta.calccateid_chr, ta.invcateid_chr, ta.chargeitemid_chr, ta.chargeitemname_chr, ta.unit_vchr, 
ta.unitprice_dec, ta.amount_dec, ta.discount_dec, ta.ismepay_int, ta.des_vchr, ta.createtype_int, ta.creator_chr, ta.create_dat, 
ta.operator_chr, ta.modify_dat, ta.deactivator_chr, ta.deactivate_dat, ta.status_int, ta.pstatus_int, ta.chearaccount_dat, ta.dayaccountid_chr, 
ta.paymoneyid_chr, ta.activator_chr, ta.activatetype_int, ta.isrich_int, ta.isconfirmrefundment, ta.refundmentchecker, ta.refundmentdate, 
ta.bmstatus_int, ta.curareaid_chr, ta.curbedid_chr, ta.doctorid_chr, ta.doctor_vchr, ta.doctorgroupid_chr, ta.needconfirm_int, ta.confirmerid_chr, 
ta.confirmer_vchr, ta.confirm_dat, ta.chargeactive_dat, ta.insuracedesc_vchr, ta.spec_vchr, ta.totalmoney_dec, ta.acctmoney_dec, 
ta.newdiscount_dec, ta.patientnurse_int, ta.attachorderid_vchr, ta.attachorderbasenum_dec, ta.putmedicineflag_int, ta.chargedoctorid_chr, 
ta.chargedoctor_vchr, ta.pchargeidorg_chr, ta.chargedoctorgroupid_chr, ta.returnmedbillno, ta.manyreturnmedill_int, ta.itemchargetype_vchr,tb.patientname,tb.bedname ,d.deptname_vchr
				from t_opr_bih_patientcharge ta,
				(
			     	select a.registerid_chr registerid,a.patientid_chr patientid,
                    b.name_vchr patientname, b.sex_chr patientsex,
                    c.bedid_chr bedid,c.code_chr bedname
                    from t_opr_bih_register a,
                    t_bse_patient b,
                    t_bse_bed c
                    where a.patientid_chr = b.patientid_chr(+)
                    and a.areaid_chr=c.areaid_chr and a.bedid_chr=c.bedid_chr(+)
                ) tb
                ,t_bse_deptdesc d
                where ta.registerid_chr=tb.registerid(+) 
                and ta.clacarea_chr=d.deptid_chr(+)
                and ta.status_int=1
				and ta.orderexecid_chr in ([OrderExecIDArr])
				";
            arrCharge = new clsBIHPatientCharge[0];
            if (arrOrderExecID == null)
                return 1;

            string strIDArr = "";
            for (int i = 0; i < arrOrderExecID.Length; i++)
            {
                if (i == 0)
                    strIDArr += "'" + arrOrderExecID[i] + "'";
                else
                    strIDArr += ",'" + arrOrderExecID[i] + "'";
            }
            if (strIDArr.Trim() == "")
                strIDArr = "''";
            strSql = strSql.Replace("[OrderExecIDArr]", strIDArr);

            DataTable objDT = new DataTable();
            long ret = 0;
            try
            {
                ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if ((ret > 0) && (objDT != null))
            {
                m_lngGetPatientChargeFromDataTable(objDT, out arrCharge);
                return 1;
            }
            else
            {
                arrCharge = null;
                return 0;
            }
        }

        /// <summary>
        /// ��ȡ���˷�����ϸ	����DataTable��
        /// </summary>
        /// <param name="objDT">DataTable��</param>
        /// <param name="arrCharge">���˷�����ϸ��Vo����</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetPatientChargeFromDataTable(DataTable objDT, out clsBIHPatientCharge[] arrCharge)
        {
            arrCharge = null;
            if (objDT == null)
                return 0;
            arrCharge = new clsBIHPatientCharge[objDT.Rows.Count];
            for (int i = 0; i < arrCharge.Length; i++)
            {
                arrCharge[i] = new clsBIHPatientCharge();
                clsBIHPatientCharge objCharge = arrCharge[i];
                DataRow objRow = objDT.Rows[i];

                objCharge.m_strPChargeID = clsConverter.ToString(objRow["PChargeID_Chr"]);
                objCharge.m_strPatientID = clsConverter.ToString(objRow["PatientID_Chr"]).Trim();
                objCharge.m_strRegisterID = clsConverter.ToString(objRow["RegisterID_Chr"]).Trim();
                objCharge.m_strPatientName = clsConverter.ToString(objRow["PatientName"]).Trim();
                objCharge.m_strBedNo = clsConverter.ToString(objRow["BedName"]).Trim();

                objCharge.m_dtActiveDate = clsConverter.ToDateTime(objRow["Active_Dat"]);
                objCharge.m_strOrderID = clsConverter.ToString(objRow["OrderID_Chr"]).Trim();
                objCharge.m_intOrderExecType = clsConverter.ToInt(objRow["OrderExecType_Int"]);
                objCharge.m_strOrderExecID = clsConverter.ToString(objRow["OrderExecID_Chr"]).Trim();

                objCharge.m_strClacArea = clsConverter.ToString(objRow["ClacArea_Chr"]).Trim();
                objCharge.m_strCreateArea = clsConverter.ToString(objRow["CreateArea_Chr"]).Trim();

                objCharge.m_strCalcCateID = clsConverter.ToString(objRow["CalcCateID_Chr"]).Trim();
                objCharge.m_strInvCateID = clsConverter.ToString(objRow["InvCateID_Chr"]).Trim();
                objCharge.m_strChargeItemID = clsConverter.ToString(objRow["ChargeItemID_Chr"]).Trim();
                objCharge.m_strChargeItemName = clsConverter.ToString(objRow["ChargeItemName_Chr"]).Trim();
                objCharge.m_strUnit = clsConverter.ToString(objRow["Unit_VChr"]).Trim();
                objCharge.m_dmlUnitPrice = clsConverter.ToDecimal(objRow["UnitPrice_Dec"]);

                objCharge.m_dmlAmount = clsConverter.ToDecimal(objRow["Amount_Dec"]);
                objCharge.m_dmlDiscount = clsConverter.ToDecimal(objRow["Discount_Dec"]);
                objCharge.m_intIsMepay = clsConverter.ToInt(objRow["IsMepay_Int"]);
                objCharge.m_strDes = clsConverter.ToString(objRow["Des_VChr"]).Trim();
                objCharge.m_intCreateType = clsConverter.ToInt(objRow["CreateType_Int"]);

                objCharge.m_strCreator = clsConverter.ToString(objRow["Creator_Chr"]).Trim();
                objCharge.m_dtCreateDate = clsConverter.ToDateTime(objRow["Create_Dat"]);
                objCharge.m_strOperator = clsConverter.ToString(objRow["Operator_Chr"]).Trim();
                objCharge.m_dtModifyDate = clsConverter.ToDateTime(objRow["Modify_Dat"]);
                objCharge.m_strDeactivator = clsConverter.ToString(objRow["Deactivator_chr"]).Trim();
                objCharge.m_dtDeactivateDate = clsConverter.ToDateTime(objRow["Deactivate_Dat"]);

                objCharge.m_intStatus = clsConverter.ToInt(objRow["Status_Int"]);
                objCharge.m_intPStatus = clsConverter.ToInt(objRow["PStatus_Int"]);
                objCharge.m_dtChearAccountDate = clsConverter.ToDateTime(objRow["ChearAccount_Dat"]);
                objCharge.m_strDayAccountID = clsConverter.ToString(objRow["DayAccountID_Chr"]).Trim();
                objCharge.m_strPayMoneyID = clsConverter.ToString(objRow["PayMoneyID_Chr"]).Trim();

                // ��ִ�п���
                objCharge.m_strExecDeptName = clsConverter.ToString(objRow["DEPTNAME_VCHR"]).Trim();
            }
            return 1;
        }
        /// <summary>
        /// ��ȡ���˷�����ϸ	����ҽ��ID[����]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">ҽ��ID	[����]</param>
        /// <param name="p_objResultArr">���˷�����ϸ��Vo����	[����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByOrderID(string[] p_strOrderIDArr, out clsBIHPatientCharge[] p_objResultArr)
        {
            p_objResultArr = new clsBIHPatientCharge[0];
            long lngRes = 0;
            if (p_strOrderIDArr == null || p_strOrderIDArr.Length <= 0)
                return 1;

            #region SQL
            string strSQL = @"
				select ta.pchargeid_chr, ta.patientid_chr, ta.registerid_chr, ta.active_dat, ta.orderid_chr, ta.orderexectype_int, ta.orderexecid_chr, 
ta.clacarea_chr, ta.createarea_chr, ta.calccateid_chr, ta.invcateid_chr, ta.chargeitemid_chr, ta.chargeitemname_chr, ta.unit_vchr, 
ta.unitprice_dec, ta.amount_dec, ta.discount_dec, ta.ismepay_int, ta.des_vchr, ta.createtype_int, ta.creator_chr, ta.create_dat, 
ta.operator_chr, ta.modify_dat, ta.deactivator_chr, ta.deactivate_dat, ta.status_int, ta.pstatus_int, ta.chearaccount_dat, ta.dayaccountid_chr, 
ta.paymoneyid_chr, ta.activator_chr, ta.activatetype_int, ta.isrich_int, ta.isconfirmrefundment, ta.refundmentchecker, ta.refundmentdate, 
ta.bmstatus_int, ta.curareaid_chr, ta.curbedid_chr, ta.doctorid_chr, ta.doctor_vchr, ta.doctorgroupid_chr, ta.needconfirm_int, ta.confirmerid_chr, 
ta.confirmer_vchr, ta.confirm_dat, ta.chargeactive_dat, ta.insuracedesc_vchr, ta.spec_vchr, ta.totalmoney_dec, ta.acctmoney_dec, 
ta.newdiscount_dec, ta.patientnurse_int, ta.attachorderid_vchr, ta.attachorderbasenum_dec, ta.putmedicineflag_int, ta.chargedoctorid_chr, 
ta.chargedoctor_vchr, ta.pchargeidorg_chr, ta.chargedoctorgroupid_chr, ta.returnmedbillno, ta.manyreturnmedill_int, ta.itemchargetype_vchr,tb.patientname,tb.bedname
				from t_opr_bih_patientcharge ta,
				(
			     	select a.registerid_chr registerid,a.patientid_chr patientid,
                    b.name_vchr patientname, b.sex_chr patientsex,
                    c.bedid_chr bedid,c.code_chr bedname
                    from t_opr_bih_register a,
                    t_bse_patient b,
                    t_bse_bed c
                    where a.patientid_chr = b.patientid_chr(+)
                    and a.areaid_chr=c.areaid_chr and a.bedid_chr=c.bedid_chr(+)
                ) tb
                where ta.registerid_chr=tb.registerid(+) and ta.status_int=1
				and ta.orderid_chr in ([ORDERID_CHR])
				";
            string strIDArr = "";
            for (int i = 0; i < p_strOrderIDArr.Length; i++)
            {
                if (i == 0)
                    strIDArr += "'" + p_strOrderIDArr[i] + "'";
                else
                    strIDArr += ",'" + p_strOrderIDArr[i] + "'";
            }
            if (strIDArr.Trim() == "")
                strIDArr = "''";
            strSQL = strSQL.Replace("[ORDERID_CHR]", strIDArr);
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_lngGetPatientChargeFromDataTable(dtbResult, out p_objResultArr);
                }
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
        #region �޸�ִ�е��ѽ���״̬
        /// <summary>
        /// �޸�ִ�е��ѽ���״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intIsIncept">�Ƿ��ѽ���	{1/0}</param>
        /// <param name="p_strHandlersID">������ID</param>
        /// <param name="p_strHandlersName">����������</param>
        /// <param name="p_strID">ִ�е�ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderExecuteStatus(int p_intIsIncept, string p_strHandlersID, string p_strHandlersName, string p_strID)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE t_opr_bih_orderexecute A ";
            strSQL += " SET";
            strSQL += "    A.isincept_int =" + p_intIsIncept.ToString();
            strSQL += "  , A.OPERATORID_CHR = '" + p_strHandlersID.Trim() + "'";
            strSQL += "  , A.OPERATOR_CHR = '" + p_strHandlersName.Trim() + "'";
            strSQL += "  Where Trim(A.ORDEREXECID_CHR) ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            /** @add by xzf (05-10-20)
             * ��ִ�е���������ط��ñ��Ϊ"��ȷ��",ͬʱ����"��Чʱ��"
             */
            strSQL = "update t_opr_bih_patientCharge"
                + " set PSTATUS_INT=1,ACTIVE_DAT=SYSDATE"
                + " where ORDEREXECID_CHR='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            /* <<================================================== */
            return lngRes;
        }
        #endregion

        //�ۺ�
        #region	��ȡҽ����Ϣ��������Ϣ��סԺ��Ϣ	����ҽ��ID
        /// <summary>
        /// ��ȡҽ����Ϣ��������Ϣ��סԺ��Ϣ	����ҽ��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_dtResult">DataTable	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetOrderPatientBIHInfo(string p_strOrderID, out DataTable p_dtResult)
        {
            p_dtResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"
					SELECT a.registerid_chr,a.modify_dat,a.patientid_chr,a.isbooking_int,a.inpatientid_chr,a.inpatient_dat,
       a.deptid_chr,a.areaid_chr,a.bedid_chr,a.type_int,a.diagnose_vchr,a.limitrate_mny,a.inpatientcount_int,
       a.state_int,a.status_int,a.operatorid_chr,a.pstatus_int,a.casedoctor_chr,a.inpatientnotype_int,
       a.des_vchr,a.inareadate_dat,a.mzdoctor_chr,a.mzdiagnose_vchr,a.diagnoseid_chr,a.icd10diagid_vchr,
       a.icd10diagtext_vchr,a.isfromclinic,a.clinicsayprepay,a.paytypeid_chr,a.bornnum_int,
       a.relateregisterid_chr,a.feestatus_int,a.extendid_vchr,a.nursing_class,a.casedoctordept_chr,
       a.cancelerid_chr,a.cancel_dat,a.outdiagnose_vchr,a.insuredsum_mny,a.checkstatus_int,a.diseasetype_int,
       a.isshunchan,b.patientid_chr,b.inpatientid_chr,b.lastname_vchr,b.idcard_chr,b.married_chr,b.birthplace_vchr,
       b.homeaddress_vchr,b.sex_chr,b.nationality_vchr,b.firstname_vchr,b.birth_dat,b.race_vchr,
       b.nativeplace_vchr,b.occupation_vchr,b.name_vchr,b.homephone_vchr,b.officephone_vchr,b.insuranceid_vchr,
       b.mobile_chr,b.officeaddress_vchr,b.employer_vchr,b.officepc_vchr,b.homepc_chr,b.email_vchr,
       b.contactpersonfirstname_vchr,b.contactpersonlastname_vchr,b.contactpersonaddress_vchr,b.contactpersonphone_vchr,
       b.contactpersonpc_chr,b.patientrelation_vchr,b.firstdate_dat,b.isemployee_int,b.status_int,
       b.deactivate_dat,b.operatorid_chr,b.modify_dat,b.paytypeid_chr,b.optimes_int,b.govcard_chr,
       b.bloodtype_chr,b.ifallergic_int,b.allergicdesc_vchr,b.difficulty_vchr,b.extendid_vchr,
       b.inpatienttempid_vchr,b.modifytime_dat,b.modifyman_vchr,b.registertime_dat,b.registerman_vchr,
       b.patientsources_vchr,c.orderid_chr,c.orderdicid_chr,c.registerid_chr,c.patientid_chr,c.executetype_int,c.recipeno_int,
       c.name_vchr,c.spec_vchr,c.execfreqid_chr,c.dosage_dec,c.execfreqname_chr,c.dosageunit_chr,
       c.get_dec,c.useunit_chr,c.getunit_chr,c.dosetypeid_chr,c.dosetypename_chr,c.startdate_dat,
       c.finishdate_dat,c.execdeptid_chr,c.execdeptname_chr,c.entrust_vchr,c.parentid_chr,c.status_int,
       c.creatorid_chr,c.creator_chr,c.createdate_dat,c.posterid_chr,c.poster_chr,c.postdate_dat,
       c.executorid_chr,c.executor_chr,c.executedate_dat,c.stoperid_chr,c.stoper_chr,c.stopdate_dat,
       c.retractorid_chr,c.retractor_chr,c.retractdate_dat,c.isrich_int,c.ratetype_int,c.isrepare_int,
       c.use_dec,c.isneedfeel,c.outgetmeddays_int,c.assessoridforexec_chr,c.assessorforexec_chr,
       c.assessorforexec_dat,c.assessoridforstop_chr,c.assessorforstop_chr,c.assessorforstop_dat,
       c.backreason,c.sendbackid_chr,c.sendbacker_chr,c.sendback_dat,c.isyb_int,c.sampleid_vchr,
       c.lisappid_vchr,c.partid_vchr,c.createareaid_chr,c.createareaname_vchr,c.ifparentid_int,c.confirmerid_chr,
       c.confirmer_vchr,c.confirm_dat,c.attachtimes_int,c.doctorid_chr,c.doctor_vchr,c.curareaid_chr,
       c.curbedid_chr,c.doctorgroupid_chr,c.deleterid_chr,c.deletername_vchr,c.delete_dat,c.sign_int,
       c.operation_int,c.remark_vchr,c.recipeno2_int,c.feelresult_vchr,c.feel_int,c.type_int,c.charge_int,
       c.singleamount_dec,c.sourcetype_int,c.chargedoctorgroupid_chr,c.itemchargetype_vchr
						,TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(b.birth_dat,'YYYY') As Age
						,(select deptname_vchr from t_bse_deptdesc a1 where a1.deptid_chr=a.areaid_chr) AreaName
						,(select code_chr from t_bse_bed a2 where a2.areaid_chr=a.areaid_chr and a2.bedid_chr=a.bedid_chr) BedCode 
					FROM t_opr_bih_order c,t_opr_bih_register a,t_bse_patient b
					WHERE c.registerid_chr=a.registerid_chr and a.patientid_chr=b.patientid_chr and trim(c.ORDERID_CHR)='" + p_strOrderID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                objHRPSvc.Dispose();
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
        #region	�ж�ҽ���Ƿ��ų�
        #region	Old
        /// <summary>
        /// �ж�ҽ��(����)���Ƿ�����ų�	[����]	�������ų�ҽ��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">ҽ��ID</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="blnIsExclude">�Ƿ��ų�	[out ����]</param>
        /// <param name="p_strExcludeOrderID">�ų�ҽ��ID	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngIsExcludeOrder(string[] p_strOrderIDArr, int p_intActiveType, out bool blnIsExclude, out string[] p_strExcludeOrderIDArr)
        {
            blnIsExclude = false;
            p_strExcludeOrderIDArr = null;
            try
            {
                return m_lngIsExcludeOrder(p_strOrderIDArr, p_strOrderIDArr, p_intActiveType, out blnIsExclude, out p_strExcludeOrderIDArr);
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// �ж�ҽ���Ƿ�����ų�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_strOrderIDBaseArr">�����жϵ�ҽ��</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="blnIsExclude">�Ƿ��ų�	[out ����]</param>
        /// <param name="p_strExcludeOrderID">�ų�ҽ��ID	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngIsExcludeOrder(string p_strOrderID, string[] p_strOrderIDBaseArr, int p_intActiveType, out bool blnIsExclude)
        {
            string[] strExcludeOrderIDArr = null;
            blnIsExclude = false;
            try
            {
                return m_lngIsExcludeOrder(new string[] { p_strOrderID }, p_strOrderIDBaseArr, p_intActiveType, out blnIsExclude, out strExcludeOrderIDArr);
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// �ж�ҽ���Ƿ�����ų�	[����]	�������ų�ҽ��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDAimArr">Ҫ�жϵġ�Ŀ��ҽ����</param>
        /// <param name="p_strOrderIDBaseArr">�����жϵġ�����ҽ����</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="blnIsExclude">�Ƿ��ų�	[out ����]</param>
        /// <param name="p_strExcludeOrderIDArr">��Ŀ��ҽ�����д����ų��ҽ��ID	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngIsExcludeOrder(string[] p_strOrderIDAimArr, string[] p_strOrderIDBaseArr, int p_intActiveType, out bool blnIsExclude, out string[] p_strExcludeOrderIDArr)
        {
            long lngRes = 0;
            blnIsExclude = false;
            p_strExcludeOrderIDArr = null;
            if (p_strOrderIDAimArr.Length <= 0 || p_strOrderIDBaseArr.Length <= 0)
                return lngRes;

            //��ȡ������Ŀ
            #region ��ȡ������Ŀ
            string[] strOrderDicIDAimArr = new string[p_strOrderIDAimArr.Length];
            for (int i1 = 0; i1 < p_strOrderIDAimArr.Length; i1++)
            {
                strOrderDicIDAimArr[i1] = GetOrderDicIDByOrderID(p_strOrderIDAimArr[i1]);
            }
            string[] strOrderDicIDBaseArr = new string[p_strOrderIDBaseArr.Length];
            for (int i1 = 0; i1 < p_strOrderIDBaseArr.Length; i1++)
            {
                strOrderDicIDBaseArr[i1] = GetOrderDicIDByOrderID(p_strOrderIDBaseArr[i1]);
            }
            #endregion

            //�ж��ų�
            p_strExcludeOrderIDArr = new string[p_strOrderIDAimArr.Length];
            #region �ж�ȫ�ų�	{3=ȫ�ų�}
            int intAllExcludeType = 0;//ȫ�ų�����(1=�ų�����;2=�ųⳤ��;3=�ųⳤ����)
            for (int i1 = 0; i1 < p_strOrderIDAimArr.Length; i1++)
            {
                intAllExcludeType = GetAllExcludeType(p_strOrderIDAimArr[i1], p_intActiveType);
                switch (intAllExcludeType)
                {
                    case 0: //���ų�
                        break;
                    case 1: //�ų�����						
                        for (int i2 = 0; i2 < p_strOrderIDAimArr.Length; i2++)//Ŀ��ҽ��--Ŀ��ҽ��
                        {
                            if (i1 != i2 && GetOrderType(p_strOrderIDAimArr[i2]) == 2)
                            {
                                p_strExcludeOrderIDArr[i2] = p_strOrderIDAimArr[i2];
                            }
                        }
                        if (p_strExcludeOrderIDArr[i1] == null || p_strExcludeOrderIDArr[i1].Trim() == "")//Ŀ��ҽ��--����ҽ��
                        {
                            for (int i2 = 0; i2 < p_strOrderIDBaseArr.Length; i2++)
                            {
                                if (p_strOrderIDAimArr[i1].Trim() != p_strOrderIDBaseArr[i2].Trim() && GetOrderType(p_strOrderIDBaseArr[i2]) == 2)
                                {
                                    p_strExcludeOrderIDArr[i1] = p_strOrderIDAimArr[i1];
                                    break;
                                }
                            }
                        }
                        break;
                    case 2: //�ųⳤ��
                        for (int i2 = 0; i2 < p_strOrderIDAimArr.Length; i2++) //Ŀ��ҽ��--Ŀ��ҽ��
                        {
                            if (i1 != i2 && GetOrderType(p_strOrderIDAimArr[i2]) == 1)
                            {
                                p_strExcludeOrderIDArr[i2] = p_strOrderIDAimArr[i2];
                            }
                        }
                        if (p_strExcludeOrderIDArr[i1] == null || p_strExcludeOrderIDArr[i1].Trim() == "")//Ŀ��ҽ��--����ҽ��
                        {
                            for (int i2 = 0; i2 < p_strOrderIDBaseArr.Length; i2++)
                            {
                                if (p_strOrderIDAimArr[i1].Trim() != p_strOrderIDBaseArr[i2].Trim() && GetOrderType(p_strOrderIDBaseArr[i2]) == 1)
                                {
                                    p_strExcludeOrderIDArr[i1] = p_strOrderIDAimArr[i1];
                                    break;
                                }
                            }
                        }
                        break;
                    case 3: //�ųⳤ����
                        for (int i2 = 0; i2 < p_strOrderIDAimArr.Length; i2++)//Ŀ��ҽ��--Ŀ��ҽ��
                        {
                            if (i1 != i2 && (GetOrderType(p_strOrderIDBaseArr[i2]) == 1 || GetOrderType(p_strOrderIDBaseArr[i2]) == 2))
                                p_strExcludeOrderIDArr[i2] = p_strOrderIDAimArr[i2];
                        }
                        if (p_strExcludeOrderIDArr[i1] == null || p_strExcludeOrderIDArr[i1].Trim() == "")//Ŀ��ҽ��--����ҽ��
                        {
                            for (int i2 = 0; i2 < p_strOrderIDBaseArr.Length; i2++)
                            {
                                if (p_strOrderIDAimArr[i1].Trim() != p_strOrderIDBaseArr[i2].Trim() && (GetOrderType(p_strOrderIDBaseArr[i2]) == 1 || GetOrderType(p_strOrderIDBaseArr[i2]) == 2))
                                {
                                    p_strExcludeOrderIDArr[i1] = p_strOrderIDAimArr[i1];
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
            #endregion
            #region �ж�һ���ų�	{1=�����ų�;2=�����ų�;}
            lngRes = 1;
            bool blnResult = false;
            com.digitalwave.iCare.middletier.HIS.clsDoctorAdviceSvc objTem = new com.digitalwave.iCare.middletier.HIS.clsDoctorAdviceSvc();
            for (int i1 = 0; i1 < strOrderDicIDAimArr.Length; i1++)
            {
                blnResult = false;
                for (int i2 = 0; i2 < strOrderDicIDBaseArr.Length; i2++)
                {
                    if (p_strExcludeOrderIDArr[i1] != null && p_strExcludeOrderIDArr[i1].Trim() != "")
                        break;//�Ѿ��ж�Ϊ�ų��ˣ��Ͳ�����ִ���ж�
                    if (lngRes > 0 && strOrderDicIDAimArr[i1].Trim() != strOrderDicIDBaseArr[i2].Trim())
                    {
                        lngRes = 0;
                        lngRes = objTem.m_lngJudgeOrderExclude(strOrderDicIDAimArr[i1], strOrderDicIDBaseArr[i2], p_intActiveType, out blnResult);
                    }
                    if (lngRes > 0 && blnResult)
                    {
                        p_strExcludeOrderIDArr[i1] = p_strOrderIDAimArr[i1];
                        break;
                    }
                    else if (i2 == strOrderDicIDBaseArr.Length - 1 && p_strExcludeOrderIDArr[i1] == null)
                    {
                        p_strExcludeOrderIDArr[i1] = "";
                    }
                }
            }
            #endregion

            if (lngRes <= 0)
            {
                throw (new System.Exception("�ж�ҽ���ų�ʧ�ܣ�"));
            }
            blnIsExclude = false;
            for (int i1 = 0; i1 < p_strExcludeOrderIDArr.Length; i1++)
            {
                if (p_strExcludeOrderIDArr[i1] != null && p_strExcludeOrderIDArr[i1].Trim() != "")
                {
                    blnIsExclude = true;
                    break;
                }
            }
            return lngRes;
        }
        /// <summary>
        /// ��ȡ������ĿID	����ҽ��ID
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <returns></returns>
        [AutoComplete]
        private string GetOrderDicIDByOrderID(string p_strOrderID)
        {
            long lngRes = 0;
            string strSQL = @"
							SELECT a.orderdicid_chr
							FROM t_opr_bih_order a
							WHERE Trim(a.orderid_chr)='" + p_strOrderID.Trim() + "'";
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    return dtResult.Rows[0]["orderdicid_chr"].ToString();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return "";
        }
        /// <summary>
        /// ��ȡȫ�ų�����	{0=���ų�;1=�ų�����;2=�ųⳤ��;3=�ųⳤ����}
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч;����Ĭ��Ϊ��������}</param>
        /// <returns>����ȫ�ų�����{0=���ų�;1=�ų�����;2=�ųⳤ��;3=�ųⳤ����}</returns>
        [AutoComplete]
        private int GetAllExcludeType(string p_strOrderID, int p_intActiveType)
        {
            int intRes = 0;
            string strOrderDicID = GetOrderDicIDByOrderID(p_strOrderID);
            if (strOrderDicID.Trim() == "")
                return intRes;

            //����ж�����¼��ȡȫ�ų���������
            string strSQL = @"
							SELECT Max(a.type3excludetype_int)  FROM t_aid_bih_orderexclude a
							WHERE a.excludetype_int=3 and (
							Trim(a.orderdicid_chr)='" + strOrderDicID.Trim() + "' or Trim(a.exculdedicid_chr)='" + strOrderDicID.Trim() + "')";
            switch (p_intActiveType)
            {
                case 0:
                    break;
                case 1:
                    strSQL += " and ACTIVETYPE_INT=1";
                    break;
                case 2:
                    strSQL += " and ACTIVETYPE_INT=2";
                    break;
                default:
                    break;
            }
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                long lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    try
                    {
                        intRes = Int32.Parse(dtbResult.Rows[0][0].ToString());
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return intRes;
        }
        /// <summary>
        /// ��ȡҽ������	ִ������	{1=����;2=��ʱ}
        /// </summary>
        /// <param name="p_strOrderID">ҽ��</param>
        /// <returns>����ִ������{0=ҽ��������,����û��ֵ;1=����;2=��ʱ}</returns>
        [AutoComplete]
        private int GetOrderType(string p_strOrderID)
        {
            int intRes = 0;
            string strSQL = @"SELECT a.executetype_int FROM t_opr_bih_order a where Trim(a.orderid_chr)='" + p_strOrderID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                long lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    try
                    {
                        intRes = Int32.Parse(dtbResult.Rows[0]["executetype_int"].ToString());
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return intRes;
        }
        #endregion
        #region New	����	2005-02-18
        /// <summary>
        /// �ж�ҽ���Ƿ�����ų�
        /// ע�⣺	
        ///		1���ж����ȼ�	[ȫ�ų�(ȫ��������)-��ͨ�ų�]��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">Ҫ�жϵ�ҽ��	[����]</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="p_intExcludeType">[out ����]	�ų�����{0=û�ų⣻1=ȫ�ų���ʱҽ����2=ȫ�ųⳤ��ҽ����3=ȫ�ų��ٳ�ҽ����4=��ͨ�ų⣻}</param>
        /// <param name="p_strExcludeOrderIDArr">[out ����]	��Ŀ��ҽ�����д����ų��ҽ��ID</param>
        /// <param name="p_strExcludeOrderIDArr">[out ����]	��Ŀ��ҽ�����д����ų��ҽ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJudgeExcludeOrder(string[] p_strOrderIDArr, int p_intActiveType, out int p_intExcludeType, out string[] p_strExcludeOrderIDArr, out string[] p_strExcludeOrderNameArr)
        {
            return m_lngJudgeExcludeOrder(p_strOrderIDArr, p_strOrderIDArr, p_intActiveType, out p_intExcludeType, out p_strExcludeOrderIDArr, out p_strExcludeOrderNameArr);
        }
        /// <summary>
        /// �ж�ҽ���Ƿ�����ų�
        /// ע�⣺	
        ///		1���ж����ȼ�	[ȫ�ų�(ȫ��������)-��ͨ�ų�]��
        ///		2������û���ж�Ŀ��ҽ��������ų⣻
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDAimArr">Ҫ�жϵġ�Ŀ��ҽ����</param>
        /// <param name="p_strOrderIDBaseArr">�����жϵġ�����ҽ����</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="p_intExcludeType">[out ����]	�ų�����{0=û�ų⣻1=ȫ�ų���ʱҽ����2=ȫ�ųⳤ��ҽ����3=ȫ�ų��ٳ�ҽ����4=��ͨ�ų⣻}</param>
        /// <param name="p_strExcludeOrderIDArr">[out ����]	��Ŀ��ҽ�����д����ų��ҽ��ID</param>
        /// <param name="p_strExcludeOrderIDArr">[out ����]	��Ŀ��ҽ�����д����ų��ҽ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJudgeExcludeOrder(string[] p_strOrderIDAimArr, string[] p_strOrderIDBaseArr, int p_intActiveType, out int p_intExcludeType, out string[] p_strExcludeOrderIDArr, out string[] p_strExcludeOrderNameArr)
        {
            #region ����
            p_intExcludeType = 0;
            p_strExcludeOrderIDArr = new string[0];
            p_strExcludeOrderNameArr = new string[0];
            if (p_strOrderIDAimArr == null || p_strOrderIDBaseArr == null || p_strOrderIDAimArr.Length <= 0 || p_strOrderIDBaseArr.Length <= 0)
                return 1;

            long lngRes = 0;
            #endregion

            //��ȡҽ���������ų���Ϣ	(����ȫ�ų�)
            DataTable dtbExcludeInfo = new DataTable();
            lngRes = 0;
            lngRes = lngGetdtbExcludeInfo(p_strOrderIDAimArr, p_intActiveType, out dtbExcludeInfo);
            if (lngRes <= 0)
                return 0;
            if (dtbExcludeInfo.Rows.Count <= 0)
                return 1;

            //��ȡ����ҽ����Ϣ
            DataTable dtbBaseOrderInfo = new DataTable();
            lngRes = 0;
            lngRes = lngGetdtbBaseOrderInfo(p_strOrderIDBaseArr, out dtbBaseOrderInfo);
            if (lngRes <= 0)
                return 0;

            string strOrderID1, strOrderID2;
            //��֤ȫ�ų�	[ע�⣺���ų��Լ�]	{1=�ų�����;2=�ųⳤ��;3=�ųⳤ����}
            #region ��֤ȫ�ų�
            DataRow[] drArr;
            //3ȫ�ų���֤
            #region 3ȫ�ų���֤
            drArr = dtbExcludeInfo.Select("EXCLUDETYPE_INT=3 and TYPE3EXCLUDETYPE_INT=3");
            if (drArr.Length > 0)
            {
                //if ȫ�ųⲻ�Ǳ���Ļ� then �����ų�
                if (p_strOrderIDBaseArr.Length == 1 && p_strOrderIDAimArr.Length == 1 && p_strOrderIDBaseArr[0].Trim() == p_strOrderIDAimArr[0].Trim())
                {
                    return 1;
                }
                else
                {
                    for (int i1 = 0; i1 < drArr.Length; i1++)
                    {
                        p_intExcludeType = 3;
                        p_strExcludeOrderIDArr = new string[drArr.Length];
                        p_strExcludeOrderIDArr[i1] = drArr[i1]["orderid_chr"].ToString();
                        p_strExcludeOrderNameArr = new string[drArr.Length];
                        p_strExcludeOrderNameArr[i1] = drArr[i1]["OrderName"].ToString();
                    }
                    return 1;
                }
            }
            #endregion
            //2�ųⳤ����֤
            #region 2�ųⳤ����֤
            drArr = dtbExcludeInfo.Select("EXCLUDETYPE_INT=3 and TYPE3EXCLUDETYPE_INT=2");
            if (drArr.Length > 0)
            {
                int intExecuteType = 0;
                for (int i = 0; i < drArr.Length; i++)
                {
                    strOrderID1 = drArr[i]["orderid_chr"].ToString().Trim();
                    for (int i0 = 0; i0 < dtbBaseOrderInfo.Rows.Count; i0++)
                    {
                        strOrderID2 = dtbBaseOrderInfo.Rows[i0]["orderid_chr"].ToString().Trim();
                        try
                        {
                            intExecuteType = Int32.Parse(dtbBaseOrderInfo.Rows[i0]["executetype_int"].ToString());
                        }
                        catch
                        {
                        }
                        if (intExecuteType == 1 && strOrderID1 != strOrderID2)//{1=����;2=��ʱ}
                        {
                            for (int i1 = 0; i1 < drArr.Length; i1++)
                            {
                                p_intExcludeType = 2;
                                p_strExcludeOrderIDArr = new string[drArr.Length];
                                p_strExcludeOrderIDArr[i1] = drArr[i1]["orderid_chr"].ToString();
                                p_strExcludeOrderNameArr = new string[drArr.Length];
                                p_strExcludeOrderNameArr[i1] = drArr[i1]["OrderName"].ToString();
                            }
                            return 1;
                        }
                    }
                }
            }
            #endregion
            //1�ų�������֤
            #region 1�ų�������֤
            drArr = dtbExcludeInfo.Select("EXCLUDETYPE_INT=3 and TYPE3EXCLUDETYPE_INT=1");
            if (drArr.Length > 0)
            {
                int intExecuteType = 0;
                for (int i = 0; i < drArr.Length; i++)
                {
                    strOrderID1 = drArr[i]["orderid_chr"].ToString().Trim();
                    for (int i0 = 0; i0 < dtbBaseOrderInfo.Rows.Count; i0++)
                    {
                        strOrderID2 = dtbBaseOrderInfo.Rows[i0]["orderid_chr"].ToString().Trim();
                        try
                        {
                            intExecuteType = Int32.Parse(dtbBaseOrderInfo.Rows[i0]["executetype_int"].ToString());
                        }
                        catch
                        {
                        }
                        if (intExecuteType == 2 && strOrderID1 != strOrderID2)//{1=����;2=��ʱ}
                        {
                            for (int i1 = 0; i1 < drArr.Length; i1++)
                            {
                                p_intExcludeType = 1;//1=ȫ�ų���ʱҽ��
                                p_strExcludeOrderIDArr = new string[drArr.Length];
                                p_strExcludeOrderIDArr[i1] = drArr[i1]["orderid_chr"].ToString();
                                p_strExcludeOrderNameArr = new string[drArr.Length];
                                p_strExcludeOrderNameArr[i1] = drArr[i1]["OrderName"].ToString();
                            }
                            return 1;
                        }
                    }
                }
            }
            #endregion
            #endregion
            //��֤һ���ų�
            #region ��֤һ���ų�
            string strOrderdicID1, strOrderdicID2, strOrderName;
            ArrayList alExcludeOrderID = new ArrayList();
            ArrayList alExcludeOrderName = new ArrayList();
            for (int i1 = 0; i1 < dtbExcludeInfo.Rows.Count; i1++)
            {
                strOrderdicID1 = dtbExcludeInfo.Rows[i1]["orderdicid_chr"].ToString().Trim();
                strOrderID1 = dtbExcludeInfo.Rows[i1]["orderid_chr"].ToString().Trim();
                strOrderName = dtbExcludeInfo.Rows[i1]["OrderName"].ToString().Trim();
                /** @update by xzf (05-10-15) */
                IList list = this.getExcludeArr(strOrderdicID1);
                for (int i2 = 0; i2 < dtbBaseOrderInfo.Rows.Count; i2++)
                {
                    /** @update by xzf(05-10-14) */
                    //@ strOrderdicID2 =dtbBaseOrderInfo.Rows[i1]["orderdicid_chr"].ToString().Trim();
                    //@ strOrderID2 =dtbBaseOrderInfo.Rows[i1]["orderid_chr"].ToString().Trim();
                    strOrderdicID2 = dtbBaseOrderInfo.Rows[i2]["orderdicid_chr"].ToString().Trim();
                    strOrderID2 = dtbBaseOrderInfo.Rows[i2]["orderid_chr"].ToString().Trim();
                    string strOrderName2 = dtbBaseOrderInfo.Rows[i2]["NAME_VCHR"].ToString().Trim();
                    /* <<========================== */
                    //@ if(strOrderdicID1==strOrderdicID2 && strOrderID1!=strOrderID2)
                    if ((list != null) && list.Contains(strOrderdicID2))
                    {
                        alExcludeOrderID.Add(strOrderID1);
                        alExcludeOrderName.Add(strOrderName);
                        alExcludeOrderID.Add(strOrderID2);
                        alExcludeOrderName.Add(strOrderName2);
                        alExcludeOrderID.Add("");
                        alExcludeOrderName.Add("");
                    }
                }
                /* <<======================================================= */
            }
            if (alExcludeOrderID.Count > 0)
            {
                p_intExcludeType = 4;//4=��ͨ�ų�
                p_strExcludeOrderIDArr = new string[alExcludeOrderID.Count];
                p_strExcludeOrderNameArr = new string[alExcludeOrderID.Count];
                for (int i1 = 0; i1 < alExcludeOrderID.Count; i1++)
                {
                    p_strExcludeOrderIDArr[i1] = alExcludeOrderID[i1].ToString();
                    p_strExcludeOrderNameArr[i1] = alExcludeOrderName[i1].ToString();
                }
                return 1;
            }
            #endregion
            return 1;
        }
        /// <summary>
        ///  ��ȡҽ���������ų���Ϣ	(����ȫ�ų�)
        ///  �����ֶ�:	[ҽ��ID			ҽ������	�ų���Ϣ]
        ///				[orderid_chr	OrderName	........]
        /// </summary>
        /// <param name="p_strOrderIdArr">ҽ��ID	[����]</param>
        /// <param name="p_intActiveType">�ų���Ч����{0=��������;1=�ύʱ��Ч;2=ִ��ʱ��Ч}</param>
        /// <param name="dtResult">[out ����] ���ر�</param>
        /// <returns></returns>
        [AutoComplete]
        private long lngGetdtbExcludeInfo(string[] p_strOrderIdArr, int p_intActiveType, out DataTable dtResult)
        {
            dtResult = new DataTable();
            if (p_strOrderIdArr.Length <= 0)
                return 1;
            long lngRes = 0;
            string strSQL = "";
            #region SQL
            //��ȡ������ĿSQL
            string strOrderdicID = " (SELECT orderdicid_chr FROM t_opr_bih_order WHERE Trim(t_opr_bih_order.orderid_chr)='[ORDERID]') ";
            string strOrderName = " (SELECT name_vchr FROM t_opr_bih_order WHERE Trim(t_opr_bih_order.orderid_chr)='[ORDERID]') ";
            string strSQLTem = @" SELECT '[ORDERID]' orderid_chr,[ORDERNAME] OrderName,a.* FROM t_aid_bih_orderexclude a WHERE a.orderdicid_chr=[GETORDERDICID] ";
            strSQLTem = strSQLTem.Replace("[GETORDERDICID]", strOrderdicID);
            strSQLTem = strSQLTem.Replace("[ORDERNAME]", strOrderName);
            for (int i1 = 0; i1 < p_strOrderIdArr.Length; i1++)
            {
                if (i1 > 0)
                    strSQL += " UNION ";
                strSQL += strSQLTem;
                if (p_intActiveType != 0)
                    strSQL += " and a.activetype_int=" + p_intActiveType.ToString();//�ų���Ч��������
                strSQL = strSQL.Replace("[ORDERID]", p_strOrderIdArr[i1].Trim());	//�滻ҽ��ID
            }
            #endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                objHRPSvc.Dispose();
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
        /// ����ҽ����Ϣ
        /// </summary>
        /// <param name="p_strOrderIdArr">ҽ��ID	[����]</param>
        /// <param name="dtResult">[out ����] ���ر�</param>
        /// <returns></returns>
        [AutoComplete]
        private long lngGetdtbBaseOrderInfo(string[] p_strOrderIdArr, out DataTable dtResult)
        {
            dtResult = new DataTable();
            if (p_strOrderIdArr.Length <= 0)
                return 1;
            long lngRes = 0;
            string strSQL = @"select a.orderid_chr,a.orderdicid_chr,a.registerid_chr,a.patientid_chr,a.executetype_int,a.recipeno_int,
       a.name_vchr,a.spec_vchr,a.execfreqid_chr,a.dosage_dec,a.execfreqname_chr,a.dosageunit_chr,
       a.get_dec,a.useunit_chr,a.getunit_chr,a.dosetypeid_chr,a.dosetypename_chr,a.startdate_dat,
       a.finishdate_dat,a.execdeptid_chr,a.execdeptname_chr,a.entrust_vchr,a.parentid_chr,a.status_int,
       a.creatorid_chr,a.creator_chr,a.createdate_dat,a.posterid_chr,a.poster_chr,a.postdate_dat,
       a.executorid_chr,a.executor_chr,a.executedate_dat,a.stoperid_chr,a.stoper_chr,a.stopdate_dat,
       a.retractorid_chr,a.retractor_chr,a.retractdate_dat,a.isrich_int,a.ratetype_int,a.isrepare_int,
       a.use_dec,a.isneedfeel,a.outgetmeddays_int,a.assessoridforexec_chr,a.assessorforexec_chr,
       a.assessorforexec_dat,a.assessoridforstop_chr,a.assessorforstop_chr,a.assessorforstop_dat,
       a.backreason,a.sendbackid_chr,a.sendbacker_chr,a.sendback_dat,a.isyb_int,a.sampleid_vchr,
       a.lisappid_vchr,a.partid_vchr,a.createareaid_chr,a.createareaname_vchr,a.ifparentid_int,a.confirmerid_chr,
       a.confirmer_vchr,a.confirm_dat,a.attachtimes_int,a.doctorid_chr,a.doctor_vchr,a.curareaid_chr,
       a.curbedid_chr,a.doctorgroupid_chr,a.deleterid_chr,a.deletername_vchr,a.delete_dat,a.sign_int,
       a.operation_int,a.remark_vchr,a.recipeno2_int,a.feelresult_vchr,a.feel_int,a.type_int,a.charge_int,
       a.singleamount_dec,a.sourcetype_int,a.chargedoctorgroupid_chr,a.itemchargetype_vchr FROM t_opr_bih_order a WHERE Trim(a.orderid_chr) in ([ORDERIDARR])";
            string strTem = "";
            for (int i1 = 0; i1 < p_strOrderIdArr.Length; i1++)
            {
                if (i1 > 0)
                    strTem += ",";
                strTem += "'" + p_strOrderIdArr[i1].Trim() + "'";
            }
            strSQL = strSQL.Replace("[ORDERIDARR]", strTem);
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                objHRPSvc.Dispose();
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
        #endregion
        #region �ж��Ƿ���δ���ֹͣ������ҽ��
        /// <summary>
        /// �ж��Ƿ����δ���ֹͣ������ҽ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_blnExist">[out����] �Ƿ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJudgeExistNotCheckConfreqOrder(string p_strRegisterID, out bool p_blnExist)
        {
            p_blnExist = false;
            long lngRes = 0;
            string strSQL = @"select count (a.orderid_chr)
                                  from t_opr_bih_order a
                                 where (a.status_int = 3 or a.status_int = 2)
                                   and a.registerid_chr = '[REGISTERID]'
                                   and a.execfreqid_chr in (select confreqid_chr
                                                              from t_bse_bih_specordercate)";

            strSQL = strSQL.Replace("[REGISTERID]", p_strRegisterID.Trim());
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (Int32.Parse(dtbResult.Rows[0][0].ToString()) > 0)
                        p_blnExist = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                lngRes = 0;
            }
            return lngRes;
        }
        #endregion
        #region	��鲡���Ƿ����
        /// <summary>
        /// ��鲡���Ƿ������ٵ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterIDs">��Ժ�Ǽ�ID	{���,�ö��ŷָ�.��: ��'00001','0002','0006'��}</param>
        /// <param name="p_blnIsLeave">[out����]	���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckPatientIsLeave(string p_strRegisterIDs, out bool p_blnIsLeave)
        {
            p_blnIsLeave = false;
            long lngRes = 0;
            if (p_strRegisterIDs.Trim() == "")
                return lngRes;

            string strSQL = @"select count (a.registerid_chr)
                                  from t_opr_bih_register a
                                 where a.pstatus_int = 4 and a.registerid_chr in ([REGISTERID])";

            strSQL = strSQL.Replace("[REGISTERID]", p_strRegisterIDs);
            try
            {
                DataTable dtResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_blnIsLeave = (Convert.ToInt32(dtResult.Rows[0][0].ToString()) > 0) ? (true) : (false);
                }
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

        //T_Opr_Bih_OrderFeel(ҽ��Ƥ�Խ��)
        #region ����
        /// <summary>
        /// ����Ƥ�Խ��	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult">ҽ��Ƥ�Խ��Vo����	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderFeelByID(string p_strID, out clsT_Opr_Bih_OrderFeel_VO p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_OrderFeel_VO();
            long lngRes = 0;
            string strSQL = @"
							SELECT a.*,b.*
								,decode(a.resulttype_int,1,'����',2,'����','') ResultTypeName
							FROM t_opr_bih_orderfeel a,t_opr_bih_order b
							where a.orderid_chr =b.orderid_chr							
									AND a.orderfeelid_chr = '" + p_strID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Opr_Bih_OrderFeel_VO();
                    p_objResult.m_strORDERFEELID_CHR = dtbResult.Rows[0]["ORDERFEELID_CHR"].ToString().Trim();
                    p_objResult.m_strORDERID_CHR = dtbResult.Rows[0]["ORDERID_CHR"].ToString().Trim();
                    p_objResult.m_intRESULTTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["RESULTTYPE_INT"].ToString().Trim());
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_strResultTypeName = dtbResult.Rows[0]["ResultTypeName"].ToString().Trim();
                    p_objResult.m_strOrderName = dtbResult.Rows[0]["NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strRegisterID = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    p_objResult.m_strParentID = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
                }
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
        /// ����Ƥ�Խ��	����ҽ��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_objResult">ҽ��Ƥ�Խ��Vo����	[out ����]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderFeelByOrderID(string p_strOrderID, out clsT_Opr_Bih_OrderFeel_VO p_objResult)
        {
            p_objResult = new clsT_Opr_Bih_OrderFeel_VO();
            long lngRes = 0;
            string strSQL = @"
							SELECT a.*,b.*
								,decode(a.resulttype_int,1,'����',2,'����','') ResultTypeName
							FROM t_opr_bih_orderfeel a,t_opr_bih_order b
							where a.orderid_chr =b.orderid_chr							
									AND a.orderid_chr = '" + p_strOrderID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Opr_Bih_OrderFeel_VO();
                    p_objResult.m_strORDERFEELID_CHR = dtbResult.Rows[0]["ORDERFEELID_CHR"].ToString().Trim();
                    p_objResult.m_strORDERID_CHR = dtbResult.Rows[0]["ORDERID_CHR"].ToString().Trim();
                    p_objResult.m_intRESULTTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["RESULTTYPE_INT"].ToString().Trim());
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_strResultTypeName = dtbResult.Rows[0]["ResultTypeName"].ToString().Trim();
                    p_objResult.m_strOrderName = dtbResult.Rows[0]["NAME_VCHR"].ToString().Trim();
                    p_objResult.m_strRegisterID = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    p_objResult.m_strParentID = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
                }
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
        #region ����
        /// <summary>
        /// ����Ƥ�Խ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��	[out ����]</param>
        /// <param name="p_objRecord">ҽ��Ƥ�Խ��Vo����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderFeel(out string p_strRecordID, clsT_Opr_Bih_OrderFeel_VO p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(7, "ORDERFEELID_CHR", "t_opr_bih_orderfeel", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_OPR_BIH_ORDERFEEL(ORDERFEELID_CHR ,ORDERID_CHR ,RESULTTYPE_INT ,DES_VCHR) VALUES (?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strORDERFEELID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strORDERID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_intRESULTTYPE_INT;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strDES_VCHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
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
        #region �޸�
        /// <summary>
        /// �޸�Ƥ�Խ��	������ˮ���޸�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderFeel(clsT_Opr_Bih_OrderFeel_VO p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += "UPDATE T_OPR_BIH_ORDERFEEL A SET ";
            strSQL += "   A.ORDERID_CHR ='" + p_objRecord.m_strORDERID_CHR + "'";
            strSQL += " , A.RESULTTYPE_INT =" + p_objRecord.m_intRESULTTYPE_INT.ToString();
            strSQL += " , A.DES_VCHR = '" + p_objRecord.m_strDES_VCHR + "'";
            strSQL += "	Where Trim(A.ORDERFEELID_CHR)='" + p_objRecord.m_strORDERFEELID_CHR.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
        /// �޸�Ƥ�Խ��	������ˮ���޸�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderFeelEnd(clsT_Opr_Bih_OrderFeel_VO p_objRecord)
        {
            long lngRes = 0, lngAff = 0;
            string strSql = @"
             UPDATE t_opr_bih_order A SET A.FEEL_INT =?,A.FEELRESULT_VCHR =?
             Where  ORDERID_CHR=? ";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = p_objRecord.m_intRESULTTYPE_INT;
                arrParams[1].Value = p_objRecord.m_strDES_VCHR;
                arrParams[2].Value = p_objRecord.m_strORDERID_CHR;

                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAff, arrParams);
                objHRPSvc.Dispose();
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

        //T_Opr_Bih_OrderAttach_Transfer(ҽ�����ӵ���-ת��)
        #region ����
        /// <summary>
        /// ��ȡҽ�����ӵ���-ת��	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderAttachTransferByID(string p_strID, out clsT_Opr_Bih_OrderAttach_Transfer_Vo p_objResult)
        {
            p_objResult = null;
            long lngRes = 0;
            //״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;
            string strSQL = @"
				SELECT	a.transferid_chr, a.sourceareaid_chr, a.sourcebedid_chr, a.targetareaid_chr, a.registerid_chr, a.createdate_dat, a.status_int, a.isactive_chr, a.activeempid_chr, a.activedate_dat, a.des_vchr 
						,(select deptname_vchr from t_bse_deptdesc a1 where a1.deptid_chr=a.sourceareaid_chr) SourceAreaName
						,(select code_chr from t_bse_bed a2 where a2.areaid_chr=a.sourceareaid_chr and a2.bedid_chr=a.sourcebedid_chr) SourceBedNo
						,(select deptname_vchr from t_bse_deptdesc a3 where a3.deptid_chr=a.targetareaid_chr) TargetAreaName
						,decode(status_int,0,'δ����',1,'�ѷ���',2,'�Ѿ��н��','') StatusName
						,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activeempid_chr) ActiveEmpName
				FROM T_Opr_Bih_OrderAttach_Transfer a 
				WHERE Trim(TRANSFERID_CHR) = '" + p_strID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Opr_Bih_OrderAttach_Transfer_Vo();
                    p_objResult.m_strTRANSFERID_CHR = dtbResult.Rows[0]["TRANSFERID_CHR"].ToString().Trim();
                    p_objResult.m_strSOURCEAREAID_CHR = dtbResult.Rows[0]["SOURCEAREAID_CHR"].ToString().Trim();
                    p_objResult.m_strSOURCEBEDID_CHR = dtbResult.Rows[0]["SOURCEBEDID_CHR"].ToString().Trim();
                    p_objResult.m_strTARGETAREAID_CHR = dtbResult.Rows[0]["TARGETAREAID_CHR"].ToString().Trim();
                    p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    p_objResult.m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    p_objResult.m_intISACTIVE_CHR = Convert.ToInt32(dtbResult.Rows[0]["ISACTIVE_CHR"].ToString().Trim());
                    p_objResult.m_strACTIVEEMPID_CHR = dtbResult.Rows[0]["ACTIVEEMPID_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_strACTIVEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["ACTIVEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch
                    {
                    }
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    //���ֶ�
                    p_objResult.m_strSourceAreaName = dtbResult.Rows[0]["SourceAreaName"].ToString().Trim();
                    p_objResult.m_strSourceBedNo = dtbResult.Rows[0]["SourceBedNo"].ToString().Trim();
                    p_objResult.m_strTargetAreaName = dtbResult.Rows[0]["TargetAreaName"].ToString().Trim();
                    p_objResult.m_strStatusName = dtbResult.Rows[0]["StatusName"].ToString().Trim();
                    p_objResult.m_strActiveEmpName = dtbResult.Rows[0]["ActiveEmpName"].ToString().Trim();
                }
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
        #region ����
        /// <summary>
        /// ����ҽ�����ӵ���-ת��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ�� [out ����]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderAttachTransfer(out string p_strRecordID, clsT_Opr_Bih_OrderAttach_Transfer_Vo p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(7, "TRANSFERID_CHR", "T_Opr_Bih_OrderAttach_Transfer", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_Opr_Bih_OrderAttach_Transfer (TRANSFERID_CHR,SOURCEAREAID_CHR,SOURCEBEDID_CHR,TARGETAREAID_CHR,REGISTERID_CHR,CREATEDATE_DAT,STATUS_INT,ISACTIVE_CHR,ACTIVEEMPID_CHR,ACTIVEDATE_DAT,DES_VCHR) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(11, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strTRANSFERID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strSOURCEAREAID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strSOURCEBEDID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strTARGETAREAID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[5].Value = DateTime.Parse(strDateTime);//p_objRecord.m_strCREATEDATE_DAT
                objLisAddItemRefArr[6].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_intISACTIVE_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strACTIVEEMPID_CHR;
                try
                {
                    objLisAddItemRefArr[9].Value = DateTime.Parse(p_objRecord.m_strACTIVEDATE_DAT);
                }
                catch
                {
                    objLisAddItemRefArr[9].Value = null;
                }
                objLisAddItemRefArr[10].Value = p_objRecord.m_strDES_VCHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
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
        #region �޸�
        /// <summary>
        /// �޸�ҽ�����ӵ���-ת��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderAttachTransfer(clsT_Opr_Bih_OrderAttach_Transfer_Vo p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE T_OPR_BIH_ORDERATTACH_TRANSFER A";
            strSQL += " SET";
            strSQL += "    A.SOURCEAREAID_CHR ='" + p_objRecord.m_strSOURCEAREAID_CHR + "'";
            strSQL += "  , A.SOURCEBEDID_CHR ='" + p_objRecord.m_strSOURCEBEDID_CHR + "'";
            strSQL += "  , A.TARGETAREAID_CHR ='" + p_objRecord.m_strTARGETAREAID_CHR + "'";
            strSQL += "  , A.REGISTERID_CHR ='" + p_objRecord.m_strREGISTERID_CHR + "'";
            //strSQL +="  , A.CREATEDATE_DAT =TO_DATE('"+p_objRecord.m_strCREATEDATE_DAT+"','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "  , A.STATUS_INT ='" + p_objRecord.m_intSTATUS_INT.ToString() + "'";
            strSQL += "  , A.ISACTIVE_CHR ='" + p_objRecord.m_intISACTIVE_CHR.ToString() + "'";
            strSQL += "  , A.ACTIVEEMPID_CHR ='" + p_objRecord.m_strACTIVEEMPID_CHR + "'";
            if (p_objRecord.m_strACTIVEDATE_DAT != null && p_objRecord.m_strACTIVEDATE_DAT.Trim() != "")
            {
                strSQL += "  , A.ACTIVEDATE_DAT =TO_DATE('" + p_objRecord.m_strACTIVEDATE_DAT + "','YYYY-MM-DD hh24:mi:ss'))";
            }
            strSQL += "  , A.DES_VCHR ='" + p_objRecord.m_strDES_VCHR + "'";
            strSQL += " WHERE Trim(A.TRANSFERID_CHR) ='" + p_objRecord.m_strTRANSFERID_CHR.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
        /// �޸�ת�����ӵ���״̬	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_intStatus">״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}</param>
        /// <returns></returns>
        /// <remarks>
        [AutoComplete]
        public long m_lngChangeOrderAttachTransferState(string p_strID, int p_intStatus)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE  T_Opr_Bih_OrderAttach_Transfer A";
            strSQL += " SET";
            strSQL += " A.STATUS_INT = '" + p_intStatus.ToString() + "'";
            strSQL += " WHERE Trim(A.TRANSFERID_CHR) ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
        /// ��Чת�����ӵ���״̬	[����]	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strActiveEmpID">��Ч��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBecomeEffectiveOrderAttachTransfer(string p_strID, string p_strActiveEmpID, clsT_Opr_Bih_Transfer_VO p_objRecord)
        {
            long lngRes = 0;
            #region �޸ĸ��ӵ�������
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE  T_Opr_Bih_OrderAttach_Transfer A";
            strSQL += " SET";
            strSQL += "  A.ISACTIVE_CHR = '1'";
            strSQL += " ,A.ACTIVEEMPID_CHR = '" + p_strActiveEmpID.Trim() + "'";
            strSQL += " ,A.ACTIVEDATE_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += " WHERE Trim(A.TRANSFERID_CHR) ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            #endregion
            //ת����
            if (lngRes > 0)
            {
                com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc objTem = new com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc();
                lngRes = 0;
                lngRes = objTem.m_lngTransferInHospital(p_objRecord);
            }
            if (lngRes <= 0)
            {
                throw (new System.Exception("��Чʧ�ܣ�"));
            }
            return lngRes;
        }
        #endregion
        #region ɾ��
        /// <summary>
        /// ɾ��ҽ�����ӵ���-ת��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrderAttachTransfer(string p_strID)
        {
            long lngRes = 0;
            string strSQL = "DELETE FROM T_OPR_BIH_ORDERATTACH_TRANSFER WHERE Trim(TRANSFERID_CHR)='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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

        //T_Opr_Bih_OrderAttach_Leave(ҽ�����ӵ���-��Ժ)
        #region ����
        /// <summary>
        /// ��ȡ��Ժ���ӵ���	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderAttachLeaveByID(string p_strID, out clsT_Opr_Bih_OrderAttach_Leave_Vo p_objResult)
        {
            p_objResult = null;
            long lngRes = 0;
            string strSQL = @"
				SELECT	a.leaveid_chr, a.registerid_chr, a.type_int, a.outareaid_chr,
                       a.outbedid_chr, a.des_vchr, a.pstatus_int, a.status_int,
                       a.isactive_int, a.activeempid_chr, a.activedate_dat 
						,decode(a.type_int,1,'������Ժ',2,'תԺ',3,'����',4,'����','')TypeName
						,(select deptname_vchr from t_bse_deptdesc a1 where a1.deptid_chr=a.outareaid_chr)OutAreaName
						,(select code_chr from t_bse_bed a2 where a2.areaid_chr=a.outareaid_chr and a2.bedid_chr=a.outbedid_chr)OutBedNo
						,decode(a.pstatus_int,0,'Ԥ��Ժ',1,'ʵ�ʳ�Ժ','')PStatusName
						,decode(status_int,0,'δ����',1,'�ѷ���',2,'�Ѿ��н��','') StatusName
						,(SELECT lastname_vchr FROM t_bse_employee WHERE t_bse_employee.empid_chr=a.activeempid_chr) ActiveEmpName
				FROM T_Opr_Bih_OrderAttach_Leave a 
				WHERE Trim(a.LEAVEID_CHR) = '" + p_strID.Trim() + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResult = new clsT_Opr_Bih_OrderAttach_Leave_Vo();
                    p_objResult.m_strLEAVEID_CHR = dtbResult.Rows[0]["LEAVEID_CHR"].ToString().Trim();
                    p_objResult.m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                    p_objResult.m_intTYPE_INT = Convert.ToInt32(dtbResult.Rows[0]["TYPE_INT"].ToString().Trim());
                    p_objResult.m_strOUTAREAID_CHR = dtbResult.Rows[0]["OUTAREAID_CHR"].ToString().Trim();
                    p_objResult.m_strOUTBEDID_CHR = dtbResult.Rows[0]["OUTBEDID_CHR"].ToString().Trim();
                    p_objResult.m_strDES_VCHR = dtbResult.Rows[0]["DES_VCHR"].ToString().Trim();
                    p_objResult.m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["PSTATUS_INT"].ToString().Trim());
                    p_objResult.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
                    p_objResult.m_intISACTIVE_INT = Convert.ToInt32(dtbResult.Rows[0]["ISACTIVE_INT"].ToString().Trim());
                    p_objResult.m_strACTIVEEMPID_CHR = dtbResult.Rows[0]["ACTIVEEMPID_CHR"].ToString().Trim();
                    try
                    {
                        p_objResult.m_strACTIVEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["ACTIVEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                    }
                    catch
                    {
                    }
                    //���ֶ�
                    p_objResult.m_strTypeName = dtbResult.Rows[0]["TypeName"].ToString().Trim();
                    p_objResult.m_strOutAreaName = dtbResult.Rows[0]["OutAreaName"].ToString().Trim();
                    p_objResult.m_strOutBedNo = dtbResult.Rows[0]["OutBedNo"].ToString().Trim();
                    p_objResult.m_strPStatusName = dtbResult.Rows[0]["PStatusName"].ToString().Trim();
                    p_objResult.m_strStatusName = dtbResult.Rows[0]["StatusName"].ToString().Trim();
                    p_objResult.m_strActiveEmpName = dtbResult.Rows[0]["ActiveEmpName"].ToString().Trim();
                }
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
        #region ����
        /// <summary>
        /// ���ӳ�Ժ���ӵ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRecordID">��ˮ��	[out ����]</param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewOrderAttachLeave(out string p_strRecordID, clsT_Opr_Bih_OrderAttach_Leave_Vo p_objRecord)
        {
            long lngRes = 0;
            p_strRecordID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            lngRes = 0;
            lngRes = objHRPSvc.lngGenerateID(7, "LEAVEID_CHR", "T_Opr_Bih_OrderAttach_Leave", out p_strRecordID);
            if (lngRes < 0)
                return lngRes;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "INSERT INTO T_Opr_Bih_OrderAttach_Leave (LEAVEID_CHR,REGISTERID_CHR,TYPE_INT,OUTAREAID_CHR,OUTBEDID_CHR,DES_VCHR,PSTATUS_INT,STATUS_INT,ISACTIVE_INT,ACTIVEEMPID_CHR,ACTIVEDATE_DAT) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(11, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_strRecordID;//p_objRecord.m_strLEAVEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strREGISTERID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_intTYPE_INT;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strOUTAREAID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strOUTBEDID_CHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strDES_VCHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_intPSTATUS_INT;
                objLisAddItemRefArr[7].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[8].Value = p_objRecord.m_intISACTIVE_INT;
                objLisAddItemRefArr[9].Value = p_objRecord.m_strACTIVEEMPID_CHR;
                try
                {
                    objLisAddItemRefArr[10].Value = DateTime.Parse(p_objRecord.m_strACTIVEDATE_DAT);//strDateTime
                }
                catch
                {
                    objLisAddItemRefArr[10].Value = null;
                }
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
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
        #region �޸�
        /// <summary>
        /// �޸ĳ�Ժ���ӵ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyOrderAttachLeave(clsT_Opr_Bih_OrderAttach_Leave_Vo p_objRecord)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE  T_OPR_BIH_ORDERATTACH_LEAVE A";
            strSQL += " SET";
            strSQL += "    A.REGISTERID_CHR = '" + p_objRecord.m_strREGISTERID_CHR + "'";
            strSQL += "  , A.TYPE_INT = '" + p_objRecord.m_intTYPE_INT.ToString() + "'";
            strSQL += "  , A.OUTAREAID_CHR = '" + p_objRecord.m_strOUTAREAID_CHR + "'";
            strSQL += "  , A.OUTBEDID_CHR = '" + p_objRecord.m_strOUTBEDID_CHR + "'";
            strSQL += "  , A.DES_VCHR = '" + p_objRecord.m_strDES_VCHR + "'";
            strSQL += "  , A.PSTATUS_INT = '" + p_objRecord.m_intPSTATUS_INT.ToString() + "'";
            strSQL += "  , A.STATUS_INT = '" + p_objRecord.m_intSTATUS_INT.ToString() + "'";
            strSQL += "  , A.ISACTIVE_INT = '" + p_objRecord.m_intISACTIVE_INT.ToString() + "'";
            strSQL += "  , A.ACTIVEEMPID_CHR = '" + p_objRecord.m_strACTIVEEMPID_CHR + "'";
            if (p_objRecord.m_strACTIVEDATE_DAT != null && p_objRecord.m_strACTIVEDATE_DAT.Trim() != "")
            {
                strSQL += "  , A.ACTIVEDATE_DAT = TO_DATE('" + p_objRecord.m_strACTIVEDATE_DAT + "','YYYY-MM-DD hh24:mi:ss'))";//strDateTime
            }
            strSQL += " WHERE Trim(A.LEAVEID_CHR) ='" + p_objRecord.m_strLEAVEID_CHR.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
        /// �޸ĳ�Ժ���ӵ���״̬	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_intStatus">״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeOrderAttachLeaveState(string p_strID, int p_intStatus)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE  T_OPR_BIH_ORDERATTACH_LEAVE A";
            strSQL += " SET";
            strSQL += " A.STATUS_INT = '" + p_intStatus.ToString() + "'";
            strSQL += " WHERE Trim(A.LEAVEID_CHR) ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
        /// ��Ч��Ժ���ӵ���״̬	����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">��ˮ��</param>
        /// <param name="p_strActiveEmpID">��Ч��ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBecomeEffectiveOrderAttachLeave(string p_strID, string p_strActiveEmpID, clsT_Opr_Bih_Leave_VO p_objRecord)
        {
            long lngRes = 0;
            #region �޸ĸ��ӵ�������
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += " UPDATE  T_OPR_BIH_ORDERATTACH_LEAVE A";
            strSQL += " SET";
            strSQL += "  A.ISACTIVE_INT = '1'";
            strSQL += " ,A.ACTIVEEMPID_CHR = '" + p_strActiveEmpID.Trim() + "'";
            strSQL += " ,A.ACTIVEDATE_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += " WHERE Trim(A.LEAVEID_CHR) ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            #endregion
            //��Ժ
            if (lngRes > 0)
            {
                com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc objTem = new com.digitalwave.iCare.middletier.HIS.clsBihRegisterSvc();
                lngRes = 0;
                lngRes = objTem.m_lngLeaveHospital(p_objRecord);
            }
            if (lngRes <= 0)
            {
                throw (new System.Exception("��Чʧ�ܣ�"));
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
        [AutoComplete]
        public long m_lngDeleteOrderAttachLeave(string p_strID)
        {
            long lngRes = 0;
            string strSQL = "DELETE FROM T_OPR_BIH_ORDERATTACH_LEAVE WHERE Trim(LEAVEID_CHR) ='" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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

        //����
        #region �޸�ҽ�����ӵ���-ת����״̬
        /// <summary>
        /// �ύҽ�����ӵ���-��Ժ	[����]	����ҽ��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_intPStatus">״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitAttachOrder_Transfer(string p_strOrderID, int p_intStatus)
        {
            long lngRes = 0;
            string[] strAttachIDArr;
            clsBIHOrderService objTem = new clsBIHOrderService();
            lngRes = 0;
            lngRes = objTem.m_lngGetAttachOrder(p_strOrderID, out strAttachIDArr);
            for (int i1 = 0; i1 < strAttachIDArr.Length; i1++)
            {
                if (lngRes > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngChangeOrderAttachTransferState(strAttachIDArr[i1], p_intStatus);
                }
            }
            if (lngRes <= 0)
            {
                throw (new System.Exception("����ʧ�ܣ�"));
            }
            return lngRes;
        }
        #endregion
        #region �޸�ҽ�����ӵ���-��Ժ��״̬
        /// <summary>
        /// �ύҽ�����ӵ���-��Ժ	[����]	����ҽ��ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_intStatus">״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitAttachOrder_Leave(string p_strOrderID, int p_intStatus)
        {
            long lngRes = 0;
            string[] strAttachIDArr;
            clsBIHOrderService objTem = new clsBIHOrderService();
            lngRes = 0;
            lngRes = objTem.m_lngGetAttachOrder(p_strOrderID, out strAttachIDArr);
            for (int i1 = 0; i1 < strAttachIDArr.Length; i1++)
            {
                if (lngRes > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngChangeOrderAttachLeaveState(strAttachIDArr[i1], p_intStatus);
                }
            }
            if (lngRes <= 0)
            {
                throw (new System.Exception("����ʧ�ܣ�"));
            }
            return lngRes;
        }
        #endregion
        #region ���ֹͣ
        /// <summary>
        /// ���ֹͣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">[����]	ҽ��ID</param>
        /// <param name="p_strHandlersID">������ID</param>
        /// <param name="p_strHandlers">����������</param>
        /// <returns></returns>
        /// <remarks>
        /// �����������ҽ��,��Ʒ�	{ͬ���ţ��÷����Ʒ�}
        /// </remarks>
        [AutoComplete]
        public long m_lngAuditingForStop(string[] p_strOrderIDArr, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;

            IEnumerator ieItem;
            //�����������ҽ��,��Ʒ�
            if (lngRes > 0)
            {
                Hashtable htTableName = new Hashtable();
                string strOrderexecID = "";
                string strDosetypeID = "";
                string strOrderID = "";
                //��ȡ������ҽ��
                lngRes = 0;
                lngRes = m_lngGetConOrder(p_strOrderIDArr, out htTableName);

                //���������������λ	[һ����ȫ�����]
                #region ���������������λ
                if (lngRes > 0)
                {
                    ieItem = htTableName.Values.GetEnumerator();
                    ieItem.Reset();
                    while (ieItem.MoveNext())
                    {
                        DateTime dtStart = System.Convert.ToDateTime(((DataRow)(ieItem.Current))["startdate_dat"].ToString());
                        DateTime dtStop = System.Convert.ToDateTime(((DataRow)(ieItem.Current))["stopdate_dat"].ToString());
                        int intGet = 0;
                        if (lngRes > 0)
                        {
                            lngRes = 0;
                            lngRes = m_lngGetMeasureForConOrder(dtStart, dtStop, 2, out intGet);
                        }
                        strOrderID = ((DataRow)(ieItem.Current))["orderid_chr"].ToString().Trim();
                        if (lngRes > 0)
                        {
                            lngRes = 0;
                            lngRes = m_lngGetFillMeasureForStopOrder(strOrderID, intGet, "Сʱ");
                        }
                    }
                }
                #endregion

                #region �շ�-������Ŀ
                if (lngRes > 0)
                {
                    ieItem = htTableName.Values.GetEnumerator();
                    ieItem.Reset();
                    while (ieItem.MoveNext())
                    {
                        //1-�շ���Ŀ�Ʒ�;
                        strOrderexecID = ((DataRow)(ieItem.Current))["orderexecid_chr"].ToString().Trim();	//ִ�е�ID
                        if (lngRes > 0)
                        {
                            lngRes = 0;
                            lngRes = m_lngExecOrderToChargeItem(strOrderexecID, p_strHandlers, p_strHandlersID, System.DateTime.Now);
                        }
                    }
                }
                #endregion

                Hashtable htSameRecipeno = new Hashtable();
                #region �շ�-�÷�
                //��ȡ��ͬ���ŵ�ҽ��
                if (lngRes > 0)
                {
                    lngRes = 0;
                    lngRes = m_lngGetSameRecipenoRecord(p_strOrderIDArr, out htSameRecipeno);
                }
                if (lngRes > 0)
                {
                    if (htSameRecipeno.Count > 0)
                    {
                        ieItem = htSameRecipeno.Values.GetEnumerator();
                        ieItem.Reset();
                        while (ieItem.MoveNext())
                        {
                            strOrderID = ((DataRow)(ieItem.Current))["orderid_chr"].ToString().Trim();			//ҽ��ID
                            if (htTableName.Contains(strOrderID))
                            {
                                strOrderexecID = ((DataRow)(htTableName[strOrderID]))["orderexecid_chr"].ToString().Trim();	//ִ�е�ID
                                strDosetypeID = ((DataRow)(htTableName[strOrderID]))["dosetypeid_chr"].ToString().Trim();	//�÷�ID
                                //2-�÷��Ʒ�
                                if (lngRes > 0)
                                {
                                    lngRes = 0;
                                    lngRes = m_lngUsageToChargeItem(strDosetypeID, strOrderexecID, p_strHandlers, p_strHandlersID, System.DateTime.Now);
                                }
                            }
                        }
                    }

                }
                #endregion
            }

            //ֱ�����ֹͣ
            if (lngRes > 0)
            {
                lngAuditingForStop(p_strOrderIDArr, p_strHandlersID, p_strHandlers);
            }

            if (lngRes <= 0)
            {
                throw (new System.Exception("���ֹͣʧ��"));
            }
            return lngRes;
        }
        #region �ӷ���
        #region �жϻ�ȡ������ҽ��
        /// <summary>
        /// �жϻ�ȡ������ҽ��
        /// ע�⣺	���ع�ϣ��	{Key=ҽ��ID Value=DataRow{ҽ����,ִ�е������}}
        ///			��ͬҽ��ID�ĵ�һ����	Key=ҽ��ID��
        ///			�ڶ���Ϊ��				Key=ҽ��ID��+ "second"
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">[����] ҽ��ID</param>
        /// <param name="p_htOrderID">[out ����] Key=ҽ��ID Value=DataRow{ҽ����,ִ�е������}</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetConOrder(string[] p_strOrderIDArr, out Hashtable p_htOrderID)
        {
            p_htOrderID = new Hashtable();
            long lngRes = 0;
            if (p_strOrderIDArr == null || p_strOrderIDArr.Length <= 0)
                return 1;

            string strSQL = @"
					SELECT   a.orderid_chr,a.orderdicid_chr,a.registerid_chr,a.patientid_chr,a.executetype_int,a.recipeno_int,
       a.name_vchr,a.spec_vchr,a.execfreqid_chr,a.dosage_dec,a.execfreqname_chr,a.dosageunit_chr,
       a.get_dec,a.useunit_chr,a.getunit_chr,a.dosetypeid_chr,a.dosetypename_chr,a.startdate_dat,
       a.finishdate_dat,a.execdeptid_chr,a.execdeptname_chr,a.entrust_vchr,a.parentid_chr,a.status_int,
       a.creatorid_chr,a.creator_chr,a.createdate_dat,a.posterid_chr,a.poster_chr,a.postdate_dat,
       a.executorid_chr,a.executor_chr,a.executedate_dat,a.stoperid_chr,a.stoper_chr,a.stopdate_dat,
       a.retractorid_chr,a.retractor_chr,a.retractdate_dat,a.isrich_int,a.ratetype_int,a.isrepare_int,
       a.use_dec,a.isneedfeel,a.outgetmeddays_int,a.assessoridforexec_chr,a.assessorforexec_chr,
       a.assessorforexec_dat,a.assessoridforstop_chr,a.assessorforstop_chr,a.assessorforstop_dat,
       a.backreason,a.sendbackid_chr,a.sendbacker_chr,a.sendback_dat,a.isyb_int,a.sampleid_vchr,
       a.lisappid_vchr,a.partid_vchr,a.createareaid_chr,a.createareaname_vchr,a.ifparentid_int,a.confirmerid_chr,
       a.confirmer_vchr,a.confirm_dat,a.attachtimes_int,a.doctorid_chr,a.doctor_vchr,a.curareaid_chr,
       a.curbedid_chr,a.doctorgroupid_chr,a.deleterid_chr,a.deletername_vchr,a.delete_dat,a.sign_int,
       a.operation_int,a.remark_vchr,a.recipeno2_int,a.feelresult_vchr,a.feel_int,a.type_int,a.charge_int,
       a.singleamount_dec,a.sourcetype_int,a.chargedoctorgroupid_chr,a.itemchargetype_vchr,b.orderexecid_chr, b.orderid_chr, b.creatorid_chr, b.creator_chr, b.createdate_dat, b.executetime_int, 
	b.executedate_vchr, b.ischarge_int, b.isincept_int, b.isfirst_int, b.isrecruit_int, b.status_int, 
	b.operatorid_chr, b.operator_chr, b.deactivatorid_chr, b.deactivator_chr, b.deactivate_dat, 
	b.executedays_int, b.needconfirm_int, b.confirmerid_chr, b.confirmer_vchr, b.confirm_dat, 
	b.print_date, b.exeareaid_chr, b.exebedid_chr, b.repare_int, b.autoid_vchr                                              
						FROM t_opr_bih_order a, t_opr_bih_orderexecute b
					WHERE a.orderid_chr = b.orderid_chr(+)
						AND a.status_int=3
						AND a.execfreqid_chr IN (SELECT confreqid_chr FROM t_bse_bih_specordercate)
						AND a.orderid_chr in ([ORDERIDCONDITION])
					ORDER BY a.orderid_chr, b.orderexecid_chr";
            #region ����
            string strTem = "";
            //ҽ��ID
            for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
            {
                if (p_strOrderIDArr[i1] != null && p_strOrderIDArr[i1].Trim() != "")
                {
                    strTem += (strTem.Trim() == "") ? (p_strOrderIDArr[i1]) : ("," + p_strOrderIDArr[i1]);
                }
            }
            if (strTem.Trim() == "")
                strTem = "''";
            strSQL = strSQL.Replace("[ORDERIDCONDITION]", strTem);
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    //����ҽ��
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        for (int i2 = 0; i2 < p_strOrderIDArr.Length; i2++)
                        {
                            if (p_strOrderIDArr[i2] != null && p_strOrderIDArr[i2].Trim() != "")
                            {
                                if (dtbResult.Rows[i1]["orderid_chr"].ToString().Trim() == p_strOrderIDArr[i2].Trim())
                                {
                                    if (!p_htOrderID.Contains(p_strOrderIDArr[i2].Trim()))
                                    {
                                        p_htOrderID.Add(p_strOrderIDArr[i2].Trim(), dtbResult.Rows[i1]);
                                    }
                                    else if (!p_htOrderID.Contains(p_strOrderIDArr[i2].Trim() + "second"))
                                    {
                                        p_htOrderID.Add(p_strOrderIDArr[i2].Trim() + "second", dtbResult.Rows[i1]);
                                    }
                                }
                            }
                        }
                    }
                }
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
        #region ȷ��һ����Ժ�ǼǺŵ�ͬ���� ֻ����һ����¼
        /// <summary>
        /// ȷ��һ����Ժ�ǼǺŵ�ͬ���� ֻ����һ����¼	���������ֹͣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">[����] ҽ��ID</param>
        /// <param name="p_htOrderID">[out ����] Key=ҽ��ID Value=DataRow{ҽ��ID=orderid_chr����Ժ�Ǽ�ID=registerid_chr������=recipeno_int}</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetSameRecipenoRecord(string[] p_strOrderIDArr, out Hashtable p_htOrderID)
        {
            p_htOrderID = new Hashtable();
            long lngRes = 0;
            if (p_strOrderIDArr == null || p_strOrderIDArr.Length <= 0)
                return 1;

            string strSQL = @"
					SELECT   MAX (orderid_chr) orderid_chr, registerid_chr, recipeno_int
						FROM t_opr_bih_order
					WHERE status_int=3 AND orderid_chr in ([ORDERIDCONDITION])
					GROUP BY registerid_chr, recipeno_int";
            #region ����
            string strTem = "";
            //ҽ��ID
            for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
            {
                if (p_strOrderIDArr[i1] != null && p_strOrderIDArr[i1].Trim() != "")
                {
                    strTem += (strTem.Trim() == "") ? ("'" + p_strOrderIDArr[i1] + "'") : (",'" + p_strOrderIDArr[i1] + "'");
                }
            }
            if (strTem.Trim() == "")
                strTem = "''";
            strSQL = strSQL.Replace("[ORDERIDCONDITION]", strTem);
            #endregion
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    //����ҽ��
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        p_htOrderID.Add(dtbResult.Rows[i1]["orderid_chr"].ToString().Trim(), dtbResult.Rows[i1]);
                    }
                }
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
        #region ֱ�����ֹͣ
        /// <summary>
        /// ֱ�����ֹͣ	û�п�������������,ֱ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrderIDArr">[����]	ҽ��ID</param>
        /// <param name="p_strHandlersID">������ID</param>
        /// <param name="p_strHandlers">����������</param>
        /// <returns></returns>
        [AutoComplete]
        private long lngAuditingForStop(string[] p_strOrderIDArr, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;
            string strOrderIDs = "";
            for (int i1 = 0; i1 < p_strOrderIDArr.Length; i1++)
            {
                if (p_strOrderIDArr[i1] != null && p_strOrderIDArr[i1].Trim() != "")
                {
                    strOrderIDs += (strOrderIDs.Trim() == "") ? (p_strOrderIDArr[i1]) : ("," + p_strOrderIDArr[i1]);
                }
            }
            if (strOrderIDs.Trim() == "")
                strOrderIDs = "''";
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = "";
            strSQL += "UPDATE t_opr_bih_order A SET ";
            strSQL += "   A.STATUS_INT =6";//ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}
            strSQL += "   ,A.ASSESSORIDFORSTOP_CHR ='" + p_strHandlersID.Trim() + "'";
            strSQL += "   ,A.ASSESSORFORSTOP_CHR ='" + p_strHandlers.Trim() + "'";
            strSQL += "   ,A.ASSESSORFORSTOP_DAT =TO_DATE('" + strDateTime + "','YYYY-MM-DD hh24:mi:ss')";
            strSQL += "	Where A.STATUS_INT=3 and Trim(A.ORDERID_CHR) in (" + strOrderIDs.Trim() + ")";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
        #region �շ���Ŀ�Ʒ�
        /// <summary>
        /// �շ���Ŀ[������Ŀ]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strExecOrderID">ִ�е�ID</param>
        /// <param name="strExecutor">ִ��������</param>
        /// <param name="strExecutorID">ִ����ID</param>
        /// <param name="dtExecuteDate">ִ��ʱ��</param>
        /// <returns></returns>
        /// <remarks>    
        /// ҵ��˵����
        ///		1��if(���� & �Ʒ�) then �շ� else ���շѣ�
        ///		2��if(���� || �Դ�ҩ) then ���շ� else �շѣ�
        /// </remarks>
        [AutoComplete]
        public long m_lngExecOrderToChargeItem(string strExecOrderID, string strExecutor, string strExecutorID, DateTime dtExecuteDate)
        {
            long lngRes = 0;
            #region ��������
            clsSQLParamDefinitionVO[] arrParams = new clsSQLParamDefinitionVO[4];
            arrParams[0] = new clsSQLParamDefinitionVO();
            arrParams[0].strParameter_Name = "strExecOrderID";
            arrParams[0].strParameter_Type = "Varchar2";
            arrParams[0].strParameter_Direction = "Input";
            arrParams[0].objParameter_Value = strExecOrderID;

            arrParams[1] = new clsSQLParamDefinitionVO();
            arrParams[1].strParameter_Name = "strExecutor";
            arrParams[1].strParameter_Type = "Varchar2";
            arrParams[1].strParameter_Direction = "Input";
            arrParams[1].objParameter_Value = strExecutor;

            arrParams[2] = new clsSQLParamDefinitionVO();
            arrParams[2].strParameter_Name = "strExecutorID";
            arrParams[2].strParameter_Type = "Varchar2";
            arrParams[2].strParameter_Direction = "Input";
            arrParams[2].objParameter_Value = strExecutorID;

            arrParams[3] = new clsSQLParamDefinitionVO();
            arrParams[3].strParameter_Name = "dtExecuteDate";
            arrParams[3].strParameter_Type = "Date";
            arrParams[3].strParameter_Direction = "Input";
            arrParams[3].objParameter_Value = dtExecuteDate;

            #endregion
            try
            {
                lngRes = 0;
                lngRes = new clsHRPTableService().lngExecuteParameterProc("BIHPack.ExecOrderToChargeItem", arrParams);
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
        #region �÷��Ʒ�
        /// <summary>
        /// �շ���Ŀ[�÷�]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strUsageID">�÷�ID</param>
        /// <param name="strExecOrderID">ִ�е�ID</param>
        /// <param name="strExecutor">ִ��������</param>
        /// <param name="strExecutorID">ִ����ID</param>
        /// <param name="dtExecuteDate">ִ��ʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUsageToChargeItem(string strUsageID, string strExecOrderID, string strExecutor, string strExecutorID, DateTime dtExecuteDate)
        {
            long lngRes = 0;
            #region ��������
            clsSQLParamDefinitionVO[] arrParams = new clsSQLParamDefinitionVO[5];
            arrParams[0] = new clsSQLParamDefinitionVO();
            arrParams[0].strParameter_Name = "strUsageID";
            arrParams[0].strParameter_Type = "Varchar2";
            arrParams[0].strParameter_Direction = "Input";
            arrParams[0].objParameter_Value = strUsageID;

            arrParams[1] = new clsSQLParamDefinitionVO();
            arrParams[1].strParameter_Name = "strExecOrderID";
            arrParams[1].strParameter_Type = "Varchar2";
            arrParams[1].strParameter_Direction = "Input";
            arrParams[1].objParameter_Value = strExecOrderID;

            arrParams[2] = new clsSQLParamDefinitionVO();
            arrParams[2].strParameter_Name = "strExecutor";
            arrParams[2].strParameter_Type = "Varchar2";
            arrParams[2].strParameter_Direction = "Input";
            arrParams[2].objParameter_Value = strExecutor;

            arrParams[3] = new clsSQLParamDefinitionVO();
            arrParams[3].strParameter_Name = "strExecutorID";
            arrParams[3].strParameter_Type = "Varchar2";
            arrParams[3].strParameter_Direction = "Input";
            arrParams[3].objParameter_Value = strExecutorID;

            arrParams[4] = new clsSQLParamDefinitionVO();
            arrParams[4].strParameter_Name = "dtExecuteDate";
            arrParams[4].strParameter_Type = "Date";
            arrParams[4].strParameter_Direction = "Input";
            arrParams[4].objParameter_Value = dtExecuteDate;

            #endregion
            try
            {
                lngRes = 0;
                lngRes = new clsHRPTableService().lngExecuteParameterProc("BIHPack.UsageToChargeItem", arrParams);
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
        #region ��ȡ����������
        /// <summary>
        /// ��ȡ����������
        /// ҵ��˵����	�Ʒ�(������λ:Сʱ):
        ///				a.��һ��Ʒ�,���� = ��ʼʱ�� -- {����ʱ��};
        ///				b.�ڼ�Ʒ�,���� = {��һ�ι���ʱ��} -- {����ʱ��)	(ʱ��:�ڹ���ʱ);
        ///				c.ֹͣ�Ʒ�,���� = {��һ�ι���ʱ��} -- {ֹͣʱ��}	(ʱ��:�����ֹͣʱ)
        ///				����ʱ�̣�	23:59:59
        ///	ע������ÿ�춼����һ�ε�
        /// </summary>
        /// <param name="p_strOrderID"></param>
        /// <param name="p_dtStartTime">��ʼʱ��</param>
        /// <param name="p_dtBalanceTime">����ʱ��</param>
        /// <param name="p_intReturnType">��������{1=����;2Сʱ;Ĭ��ΪСʱ}</param>
        /// <param name="p_intGetMeasure">[out ����] ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMeasureForConOrder(DateTime p_dtStartTime, DateTime p_dtBalanceTime, int p_intReturnType, out int p_intGetMeasure)
        {
            p_intGetMeasure = 0;
            //if(p_dtBalanceTime<=p_dtStartTime) return 0;
            //if(p_dtBalanceTime==p_dtStartTime) return 1;
            if (p_dtBalanceTime <= p_dtStartTime)
                return 1;

            /*if(��ʼʱ��<�������ʱ��) ��ʼʱ��=�������ʱ��*/
            string strYesterdayBalanceDataTime = (p_dtBalanceTime.AddDays(-1)).ToString("yyyy-MM-dd") + " 23:59:59";
            DateTime dtYesterdayBalanceDataTime = System.Convert.ToDateTime(strYesterdayBalanceDataTime);
            if (p_dtStartTime < dtYesterdayBalanceDataTime)
            {
                p_dtStartTime = dtYesterdayBalanceDataTime;
            }

            TimeSpan dTs = p_dtBalanceTime.Subtract(p_dtStartTime);
            switch (p_intReturnType)
            {
                case 1://����
                    p_intGetMeasure = dTs.Days * 24 * 60 + dTs.Hours * 60 + dTs.Minutes + ((dTs.Seconds > 0) ? 1 : 0);
                    break;
                case 2://Сʱ��
                    p_intGetMeasure = dTs.Days * 24 + dTs.Hours + ((dTs.Minutes > 0) ? 1 : 0);
                    break;
                default://Сʱ��
                    p_intGetMeasure = dTs.Days * 24 + dTs.Hours + ((dTs.Minutes > 0) ? 1 : 0);
                    break;
            }
            return 1;
        }
        #endregion
        #region ���������������λ
        /// <summary>
        /// ���������������λ	Forֹͣҽ��
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_intGet">����</param>
        /// <param name="strUnitGet">������λ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFillMeasureForStopOrder(string p_strOrderID, int p_intGet, string strUnitGet)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQLMode = "UPDATE T_OPR_BIH_ORDER  SET GET_DEC=[GET],GETUNIT_CHR=[GETUNIT] WHERE Trim(ORDERID_CHR)=[ORDERID]";
            //����SQL���
            string strSQL = @"";
            strSQL += strSQLMode;
            strSQL = strSQL.Replace("[ORDERID]", "'" + p_strOrderID.Trim() + "'");
            strSQL = strSQL.Replace("[GET]", "'" + p_intGet.ToString().Trim() + "'");
            strSQL = strSQL.Replace("[GETUNIT]", "'" + strUnitGet.Trim() + "'");
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
        #endregion
        #endregion

        #region ֹͣ[���]
        /// <summary>
        /// ֹͣ[���] ��һ��������ִ��
        /// �ô�:	һ�������Զ�ֹͣҽ��
        /// </summary>
        /// <param name="p_objOrderArr">ҽ�����ݶ���</param>
        /// <param name="p_strHandlersID"></param>
        /// <param name="p_strHandlers"></param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngStopANDAuditingOrder(System.Security.Principal.IPrincipal clsBIHOrder[] p_objOrderArr, string p_strHandlersID, string p_strHandlers)
        //{
        //    long lngRes = 0;
        //    clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //    lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsLisDeviceSvc", "m_lngModifyOrderFeel");
        //    if (lngRes < 0)
        //    {
        //        return -1;
        //    }
        //    if (lngRes > 0)
        //    {
        //        lngRes = 0;
        //        DateTime m_dtStopTime = DateTime.MinValue;//��ֹͣʱȡϵͳʱ��
        //        lngRes = new clsBIHOrderService().m_lngStopOrder(p_objOrderArr, p_strHandlersID, p_strHandlers, m_dtStopTime);
        //    }
        //    if (lngRes > 0)
        //    {
        //        string[] strOrderIDArr = new string[p_objOrderArr.Length];
        //        for (int i1 = 0; i1 < strOrderIDArr.Length; i1++)
        //        {
        //            strOrderIDArr[i1] = p_objOrderArr[i1].m_strOrderID;
        //        }
        //        lngRes = 0;
        //        lngRes = m_lngAuditingForStop(strOrderIDArr, p_strHandlersID, p_strHandlers);
        //    }
        //    if (lngRes <= 0)
        //    {
        //        ContextUtil.SetAbort();
        //    }
        //    return lngRes;
        //}
        #endregion

        /// <summary>
        /// ����������Ŀ�ֵ�,�����������ų���Ŀ
        /// </summary>
        /// <param name="orderDicId"></param>
        /// <returns></returns>
        [AutoComplete]
        public IList getExcludeArr(string orderDicId)
        {
            long lngRes = 0;
            IList list = null;
            string strSql = "";
            strSql = "select EXCULDEDICID_CHR from t_aid_bih_orderexclude"
                 + " where ORDERDICID_CHR='" + orderDicId + "'";
            DataTable dt = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //���ֵ
            if (dt.Rows.Count > 0)
            {
                list = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    string dicId = dr[0].ToString().Trim();
                    list.Add(dicId);
                }
            }
            return list;
        }

        /* <<======================================== */

        #region ִ��ҽ��	[����ҽ��]  -�Ǵ洢����  -----> 2018.03.08����ȷ���÷�������
        /// <summary>
        /// ִ��ҽ��	[����ҽ��]
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strOrderID">ҽ��ID</param>
        /// <param name="strOrderExecID">ִ�е���¼ID [out ������������ض��ID���ж��š������ָΪ��ִ��ʧ�ܡ�]</param>
        /// <param name="blnIsRecruit">ָ���Ƿ񲹴�(��ִ������)</param>
        /// <param name="strEmpID">ִ��ҽ����ˮ��</param>
        /// <param name="strEmpName">ִ��ҽ������</param>
        /// <param name="dtExecDate">ִ������</param>
        /// <param name="p_blnIsCharge">�÷��Ƿ��շ�</param>
        /// <returns></returns>
        /// <remarks>
        ///		1����ΪCom+�������ִ��ҽ��ʧ�ܣ��򱨴�ҽ��ִ�д��󣡡������ع���
        ///		2������������ע��Ҫ���쳣����
        ///	</remarks>
        [AutoComplete]
        public long m_lngExecuteOrder(string strOrderID, out string strExecOrderID, bool blnIsRecruit, string strEmpID, string strEmpName, DateTime dtExecDate, bool p_blnIsCharge, bool m_blNewTag)
        {
            DataTable dtbResult = new DataTable();

            long lngRes = 0;
            long lngAff = 0;
            strExecOrderID = "";
            int intChargeForUsage = 0;//{�÷���1=�շѣ�0=���շ�} ͬ����ҽ��ֻ����һ���÷��շ�
            if (p_blnIsCharge)
            {
                intChargeForUsage = 1;
            }
            else
            {
                intChargeForUsage = 0;
            }
            int intIsRecruit; //ָ���Ƿ񲹴�(��ִ������)[1-���Σ�0-������]
            if (blnIsRecruit)
            {
                intIsRecruit = 1;
            }
            else
            {
                intIsRecruit = 0;
            }

            string strExecutor = strEmpName;//����������
            string strExecutorID = strEmpID;//������ID
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            /*
            ����˵��:
            ִ��ҽ��(ҽ��->ִ�е�)
            ����˵��:
            strOrderID       ҽ��ID
            intIsRecruit     ָ���Ƿ񲹴�(��ִ������)[1-���Σ�0-������]
            strExecutor      ִ��������
            strExecutorID    ִ����ID
            dtExecuteDate    ִ��ʱ��
            ����ֵ˵��:
            ����ִ�е�ID��������ض��ID�����ж��š������ָ��
            ҵ��˵��:
            �������һ�ȡҽ��ID��Ӧ���α�
            ��������Ƿ���Ҫִ�У�
            ��������һ��ҽ��ִ�е���¼��
            ��������շ���Ŀ��¼��
            ������ҩ��
            �����÷��Ƿ��շ�{1=�շѣ�0=���շ�}
            ��������ҽ��ID��Ӧ��ִ��״̬��־��
            ��ע˵����
            ��������������һ������
            �����������շ�����: ����ִ�е�,���Ʒѣ�����ҩ��
            */

            //--��ȡ������ҽ��Ƶ��id

            string strConfreqID = "";

            string strExecOrderID2 = "";
            //--��ҩ��ʽid    {��ҩ��ʽΪ�գ����÷����շѡ�}
            string strUsageID = "";
            string strSQL = "";
            strSQL = @"SELECT confreqid_chr  strConfreqID FROM t_bse_bih_specordercate WHERE rownum =1";
            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                strConfreqID = clsConverter.ToString(dtbResult.Rows[0]["strConfreqID"].ToString());
            }

            #region �������һ�ȡҽ��ID��Ӧ���α�
            int intType = 0;   //--����/������־ ִ���Ƿ���Ҫ����ҽ������־ 0����Ҫ    1����   2�����״�ִ��
            int intIsFirst = 0; //--�״�ִ�б�־ 
            strSQL = @"
               select a.orderid_chr,a.orderdicid_chr,a.registerid_chr,a.patientid_chr,a.executetype_int,a.recipeno_int,
       a.name_vchr,a.spec_vchr,a.execfreqid_chr,a.dosage_dec,a.execfreqname_chr,a.dosageunit_chr,
       a.get_dec,a.useunit_chr,a.getunit_chr,a.dosetypeid_chr,a.dosetypename_chr,a.startdate_dat,
       a.finishdate_dat,a.execdeptid_chr,a.execdeptname_chr,a.entrust_vchr,a.parentid_chr,a.status_int,
       a.creatorid_chr,a.creator_chr,a.createdate_dat,a.posterid_chr,a.poster_chr,a.postdate_dat,
       a.executorid_chr,a.executor_chr,a.executedate_dat,a.stoperid_chr,a.stoper_chr,a.stopdate_dat,
       a.retractorid_chr,a.retractor_chr,a.retractdate_dat,a.isrich_int,a.ratetype_int,a.isrepare_int,
       a.use_dec,a.isneedfeel,a.outgetmeddays_int,a.assessoridforexec_chr,a.assessorforexec_chr,
       a.assessorforexec_dat,a.assessoridforstop_chr,a.assessorforstop_chr,a.assessorforstop_dat,
       a.backreason,a.sendbackid_chr,a.sendbacker_chr,a.sendback_dat,a.isyb_int,a.sampleid_vchr,
       a.lisappid_vchr,a.partid_vchr,a.createareaid_chr,a.createareaname_vchr,a.ifparentid_int,a.confirmerid_chr,
       a.confirmer_vchr,a.confirm_dat,a.attachtimes_int,a.doctorid_chr,a.doctor_vchr,a.curareaid_chr,
       a.curbedid_chr,a.doctorgroupid_chr,a.deleterid_chr,a.deletername_vchr,a.delete_dat,a.sign_int,
       a.operation_int,a.remark_vchr,a.recipeno2_int,a.feelresult_vchr,a.feel_int,a.type_int,a.charge_int,
       a.singleamount_dec,a.sourcetype_int,a.chargedoctorgroupid_chr,a.itemchargetype_vchr,b.days_int,b.times_int,b.texectime_vchr exectime
                      from t_opr_bih_order a,t_aid_recipefreq b
                      where a.execfreqid_chr=b.freqid_chr and a.orderid_chr=? and rownum=1
               ";


            System.Data.IDataParameter[] arrParams = null;
            objHRPSvc.CreateDatabaseParameter(1, out arrParams);
            arrParams[0].Value = strOrderID;
            #endregion

            try
            {
                lngRes = 0;
                // ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        #region ��������Ƿ���Ҫִ��
                        /*
                        ��������Ƿ���Ҫִ�У�
                        1��if(����ʱҽ�� & �ǳ���ҽ������ִ��) then ��ִ��;
                        2��if(��ʱҽ�� & ִ��״̬Ϊ��5-���ִ�С�) then ִ�� else ��ִ��;
                        3��if(����ҽ�� & ִ��״̬Ϊ��5-���ִ�С�) then ִ��
                        else ��if(ִ��������ִ�й�) then ��ִ�� else ִ�У�;
                        ��ע��
                        i_cur.ExecuteType_Int [1=����;2=��ʱ]
                        i_cur.Status_Int      ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ���ִ��;6-���ֹͣ;}
                        */

                        //EXECUTETYPE_INT	NUMBER(1)	Y			ִ������	{1=����;2=��ʱ;}
                        //��ز���
                        string m_strOrderID_Chr = "";//ҽ��ID
                        /*<---------------------------*/

                        //STATUS_INT	NUMBER(1)	Y			
                        int STATUS_INT = 0;//ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}
                        //DAYS_INT	NUMBER(1)	Y			���� ������Ƶ�ʱ�
                        int DAYS_INT = 0;
                        int m_intTemp;
                        int Times_Int = 0;

                        string ExecTime = "";
                        DateTime dtExecuteDate = Convert.ToDateTime(dtExecDate.ToString("yyyy-MM-dd HH:mm:ss"));


                        STATUS_INT = clsConverter.ToInt(dtbResult.Rows[i]["STATUS_INT"].ToString());
                        DAYS_INT = clsConverter.ToInt(dtbResult.Rows[i]["DAYS_INT"].ToString());
                        Times_Int = clsConverter.ToInt(dtbResult.Rows[i]["Times_Int"].ToString());
                        ExecTime = clsConverter.ToString(dtbResult.Rows[i]["ExecTime"].ToString());
                        int ExecuteType_Int = clsConverter.ToInt(dtbResult.Rows[i]["ExecuteType_Int"].ToString());
                        //STARTDATE_DAT	DATE	Y			"��ʼʱ��"
                        DateTime StartDate_Dat = clsConverter.ToDateTime(dtbResult.Rows[i]["STARTDATE_DAT"].ToString());
                        string OrderID_Chr = clsConverter.ToString(dtbResult.Rows[i]["OrderID_Chr"].ToString());

                        if (ExecuteType_Int != 1 && ExecuteType_Int != 2)
                        {
                            strExecOrderID = "";
                            return -1;
                        }
                        if (ExecuteType_Int == 2)
                        {
                            if (STATUS_INT != 5)
                            {
                                strExecOrderID = "";
                                return -1;
                            }
                            else
                            {
                                intType = 1;   //����
                                intIsFirst = 1;//�״�ִ��
                            }
                        }
                        if (ExecuteType_Int == 1)
                        {
                            if (STATUS_INT == 5)
                            {
                                intType = 2;//�����״�ִ��
                                intIsFirst = 1;//�״�ִ��
                            }
                            else
                            {
                                if (DAYS_INT <= 0)
                                {
                                    m_intTemp = 1;
                                }
                                else
                                {
                                    m_intTemp = DAYS_INT;
                                }

                                TimeSpan m_tsDay = dtExecuteDate - StartDate_Dat;
                                if (m_tsDay.Days % m_intTemp == 0)
                                {
                                    intIsFirst = 0;//�����״�ִ��
                                    intType = 0;//�ǵ�һ��ִ��,���ø���ҽ�������ִ��״̬��
                                }
                                else
                                {
                                    strExecOrderID = "";
                                    return -1;//��ִ��
                                }
                            }
                        }
                        #endregion

                        #region ��������һ��ҽ��ִ�е���¼
                        /*
                        ��������һ��ҽ��ִ�е���¼��
                        1�������µ�ҽ��ִ�е���ˮ�ţ�����λ����
                        2������ҽ��ִ�е���¼��
                        ҵ��������
                        if(����ҽ�� & �״�ִ�� & ����״̬)
                        then ��������ִ�е���¼[1-������2-����]
                        else ����һ��ִ�е�[����]
                        */

                        //--�����µ�ҽ��ִ�е���ˮ�ţ�����λ����
                        strSQL = "   select lpad(SEQ_ORDEREXECID.Nextval,18,'0') OrderExecID_Chr   from dual ";
                        DataTable dtbResult2 = new DataTable();
                        objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
                        if (dtbResult.Rows.Count > 0)
                        {
                            //--����һ��ҽ��ִ�е���¼��[1-����]
                            strExecOrderID = dtbResult2.Rows[0]["OrderExecID_Chr"].ToString();
                            if (ExecuteType_Int == 1 && intType == 2 && intIsRecruit == 1)
                            {
                                strSQL = @"  insert into T_Opr_Bih_OrderExecute (OrderExecID_Chr,OrderID_Chr,CreatorID_Chr,Creator_Chr,CreateDate_Dat,
                        ExecuteTime_Int,ExecuteDays_Int,ExecuteDate_VChr,IsCharge_Int,IsIncept_int,IsFirst_int,IsRecruit_int,Status_Int)
                        values(?,?,?,?,?,?,?,?,0,0,?,0,1)
                        ";
                                arrParams = null;
                                objHRPSvc.CreateDatabaseParameter(9, out arrParams);
                                arrParams[0].Value = strExecOrderID;
                                arrParams[1].Value = OrderID_Chr;
                                arrParams[2].Value = strExecutorID;
                                arrParams[3].Value = strExecutor;
                                arrParams[4].DbType = DbType.Date;
                                arrParams[4].Value = dtExecuteDate;
                                arrParams[5].Value = Times_Int;
                                arrParams[6].Value = DAYS_INT;
                                arrParams[7].Value = ExecTime;
                                arrParams[8].Value = intIsFirst;

                                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);

                                // --����һ��ҽ��ִ�е���¼��[2-����]
                                strSQL = "   select lpad(SEQ_ORDEREXECID.Nextval,18,'0') OrderExecID_Chr   from dual ";
                                objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
                                if (dtbResult2.Rows.Count > 0)
                                {
                                    strExecOrderID2 = dtbResult2.Rows[0]["OrderExecID_Chr"].ToString();


                                    strSQL = @"  insert into T_Opr_Bih_OrderExecute (OrderExecID_Chr,OrderID_Chr,CreatorID_Chr,Creator_Chr,CreateDate_Dat,
                        ExecuteTime_Int,ExecuteDays_Int,ExecuteDate_VChr,IsCharge_Int,IsIncept_int,IsFirst_int,IsRecruit_int,Status_Int)
                        values(?,?,?,?,?,?,?,?,0,0,?,1,1)
                        ";
                                    arrParams = null;
                                    objHRPSvc.CreateDatabaseParameter(9, out arrParams);
                                    arrParams[0].Value = strExecOrderID2;
                                    arrParams[1].Value = OrderID_Chr;
                                    arrParams[2].Value = strExecutorID;
                                    arrParams[3].Value = strExecutor;
                                    arrParams[4].DbType = DbType.Date;
                                    arrParams[4].Value = dtExecuteDate;
                                    arrParams[5].Value = Times_Int;
                                    arrParams[6].Value = DAYS_INT;
                                    arrParams[7].Value = ExecTime;
                                    arrParams[8].Value = intIsFirst;

                                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);

                                }
                            }
                            else//--����һ��ҽ��ִ�е���¼��[����]
                            {
                                strSQL = @"  insert into T_Opr_Bih_OrderExecute (OrderExecID_Chr,OrderID_Chr,CreatorID_Chr,Creator_Chr,CreateDate_Dat,
                        ExecuteTime_Int,ExecuteDays_Int,ExecuteDate_VChr,IsCharge_Int,IsIncept_int,IsFirst_int,IsRecruit_int,Status_Int)
                        values(?,?,?,?,?,?,?,?,0,0,?,0,1)
                        ";
                                arrParams = null;
                                objHRPSvc.CreateDatabaseParameter(9, out arrParams);
                                arrParams[0].Value = strExecOrderID;
                                arrParams[1].Value = OrderID_Chr;
                                arrParams[2].Value = strExecutorID;
                                arrParams[3].Value = strExecutor;
                                arrParams[4].DbType = DbType.Date;
                                arrParams[4].Value = dtExecuteDate;
                                arrParams[5].Value = Times_Int;
                                arrParams[6].Value = DAYS_INT;
                                arrParams[7].Value = ExecTime;
                                arrParams[8].Value = intIsFirst;

                                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);

                            }
                        }
                        #endregion

                        #region �������շ�����
                        /*�������շ�����:
             *      ����ִ�е�,���Ʒѣ�����ҩ
             *      ��ʼʱ�䲻�ܸ��ǣ�ԭ����ֵ��
             */
                        //--��ȡִ��Ƶ��id
                        /*
           strExecFreqID:=i_cur.ExecFreqID_Chr;
           if((strConfreqID is not null) and (strExecFreqID=strConfreqID)) then
               --��������ҽ��ID��Ӧ��ִ��״̬��־��
               --ִ��״̬{0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;}
               if((intType=1) or (intType=2)) then
                    update T_Opr_Bih_Order
                    set ExecutorID_Chr=strExecutorID,Executor_Chr=strExecutor,ExecuteDate_Dat=sysdate,Status_Int=2
                    where OrderID_Chr=strOrderID;
               end if;
               --ȷ����������
               if(strExecOrderID2 is not null) then
                   return(strExecOrderID || ',' || strExecOrderID2);
               else
                   return(strExecOrderID);
               end if;
           end if;*/
                        string strExecFreqID = clsConverter.ToString(dtbResult.Rows[0]["ExecFreqID_Chr"].ToString());
                        if (strExecFreqID == strConfreqID)
                        {
                            if (intType == 1 || intType == 2)
                            {
                                strSQL = @"update T_Opr_Bih_Order
                                set ExecutorID_Chr=?,Executor_Chr=?,ExecuteDate_Dat=sysdate,Status_Int=2
                                where OrderID_Chr=?";
                                arrParams = null;
                                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                                arrParams[0].Value = strExecutorID;
                                arrParams[1].Value = strExecutor;
                                arrParams[2].Value = strOrderID;
                                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);

                            }
                            if (strExecOrderID2.Trim().Equals(""))
                            {
                                strExecOrderID = strExecOrderID + "," + strExecOrderID2;
                                return lngRes;
                            }
                            else
                            {
                                strExecOrderID = strExecOrderID;
                                return lngRes;
                            }

                        }
                        #endregion

                        //--��ҩ��ʽid    {��ҩ��ʽΪ�գ����÷����շѡ�}
                        strUsageID = clsConverter.ToString(dtbResult.Rows[i]["DoseTypeID_Chr"].ToString());
                        /*
                        ��������շ���Ŀ��¼��
                            1������շ���Ŀ��¼[������Ŀ]��
                            2������շ���Ŀ��¼[�÷�]��
                        ҵ��������(�����ں����д������ҵ��)
                            0��if���в��Σ�then �շ�����ҽ����¼[�����ġ����ε�]
                            1��if(���� & �Ʒ�) then �����շ� else �����շѣ�
                            2��if(���� || �Դ�ҩ) then ֻ���÷����� else �����շ�[��������Ŀ���÷����ַ���]��
                        */
                        /*
                        if(i_cur.ExecuteType_Int=1 and intType=2 and intIsRecruit=1) then
                            --1������շ���Ŀ��¼[������Ŀ]��
                            BIHPack.ExecOrderToChargeItem(strExecOrderID,strExecutor,strExecutorID,dtExecuteDate);
                            --2������շ���Ŀ��¼[�÷�]��
                            if(intChargeForUsage=1 and (strUsageID is not null)) then
                                BIHPack.UsageToChargeItem(strUsageID,strExecOrderID,strExecutor,strExecutorID,dtExecuteDate);
                            end if;
                            --1������շ���Ŀ��¼[������Ŀ]��
                            BIHPack.ExecOrderToChargeItem(strExecOrderID2,strExecutor,strExecutorID,dtExecuteDate);
                            --2������շ���Ŀ��¼[�÷�]��
                            if(intChargeForUsage=1 and (strUsageID is not null)) then
                                BIHPack.UsageToChargeItem(strUsageID,strExecOrderID2,strExecutor,strExecutorID,dtExecuteDate);
                            end if;
                        else
                            --1������շ���Ŀ��¼[������Ŀ]��
                            BIHPack.ExecOrderToChargeItem(strExecOrderID,strExecutor,strExecutorID,dtExecuteDate);
                            --2������շ���Ŀ��¼[�÷�]��
                            if(intChargeForUsage=1 and (strUsageID is not null)) then
                                BIHPack.UsageToChargeItem(strUsageID,strExecOrderID,strExecutor,strExecutorID,dtExecuteDate);
                            end if;
                        end if;
                         * */
                        /*----------------------------------->
                        if (ExecuteType_Int == 1 && intType == 2 && intIsRecruit==1)
                        {
                            //--1������շ���Ŀ��¼[������Ŀ]��
                            ExecOrderToChargeItem(strExecOrderID, strExecutor, strExecutorID, dtExecuteDate);
                            //--2������շ���Ŀ��¼[�÷�]��
                            if (intChargeForUsage == 1 && !strUsageID.Trim().Equals(""))
                            {
                                UsageToChargeItem(strUsageID, strExecOrderID, strExecutor, strExecutorID, dtExecuteDate);
                            }
                            //--1������շ���Ŀ��¼[������Ŀ]��
                            ExecOrderToChargeItem(strExecOrderID2, strExecutor, strExecutorID, dtExecuteDate);
                            //--2������շ���Ŀ��¼[�÷�]��
                            if (intChargeForUsage == 1 && !strUsageID.Trim().Equals(""))
                            {
                                UsageToChargeItem(strUsageID, strExecOrderID2, strExecutor, strExecutorID, dtExecuteDate);
                            }
                        }
                        else
                        {
                            //--1������շ���Ŀ��¼[������Ŀ]��
                            ExecOrderToChargeItem(strExecOrderID, strExecutor, strExecutorID, dtExecuteDate);
                            //--2������շ���Ŀ��¼[�÷�]��
                            if (intChargeForUsage == 1 && !strUsageID.Trim().Equals(""))
                            {
                                UsageToChargeItem(strUsageID, strExecOrderID, strExecutor, strExecutorID, dtExecuteDate);
                            }
                        }
                        */
                        if (ExecuteType_Int == 1 && intType == 2 && intIsRecruit == 1)
                        {
                            //--1������շ���Ŀ��¼[������Ŀ]��
                            //--2������շ���Ŀ��¼[�÷�]��
                            ExecFromORDERCHARGEDEPT(strExecOrderID, strExecutor, strExecutorID, dtExecuteDate);

                            //--1������շ���Ŀ��¼[������Ŀ]��
                            //--2������շ���Ŀ��¼[�÷�]��
                            ExecFromORDERCHARGEDEPT(strExecOrderID2, strExecutor, strExecutorID, dtExecuteDate);

                        }
                        else
                        {
                            //--1������շ���Ŀ��¼[������Ŀ]��
                            //--2������շ���Ŀ��¼[�÷�]��
                            ExecFromORDERCHARGEDEPT(strExecOrderID, strExecutor, strExecutorID, dtExecuteDate);

                        }

                        /*<=========================================================*/
                        /*
                        ������ҩ��
                        ҵ��������(�����ں����д������ҵ��)
                        0��if���в��Σ�then ��ҩ����ҽ����¼[�����ġ����ε�]
                        1��if(��ҩ�շ���Ŀ) then ��ҩ else ����ҩ;
                        */
                        /*
                        if(i_cur.ExecuteType_Int=1 and intType=2 and intIsRecruit=1) then
                        BIHPack.ProcessOrderMedicine(strExecOrderID,strExecutorID);
                        BIHPack.ProcessOrderMedicine(strExecOrderID2,strExecutorID);
                        else
                        BIHPack.ProcessOrderMedicine(strExecOrderID,strExecutorID);
                        end if;
                        */
                        if (ExecuteType_Int == 1 && intType == 2 && intIsRecruit == 1)
                        {
                            ProcessOrderMedicine(strExecOrderID, strExecutorID);
                            ProcessOrderMedicine(strExecOrderID2, strExecutorID);
                        }
                        else
                        {
                            ProcessOrderMedicine(strExecOrderID, strExecutorID);
                        }


                    }

                    //--��������ҽ��ID��Ӧ��ִ��״̬��־��
                    //--ִ��״̬{0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;}
                    /*
                    if((intType=1) or (intType=2)) then
                    update T_Opr_Bih_Order
                    set ExecutorID_Chr=strExecutorID,Executor_Chr=strExecutor,ExecuteDate_Dat=sysdate,StartDate_Dat=sysdate,Status_Int=2
                    where OrderID_Chr=strOrderID;
                    end if;
                    --ȷ����������
                    if(strExecOrderID2 is not null) then
                    return(strExecOrderID || ',' || strExecOrderID2);
                    else
                    return(strExecOrderID);
                    end if;
                     */
                    if ((intType == 1) || (intType == 2))
                    {
                        strSQL = @"
                             update T_Opr_Bih_Order
                             set ExecutorID_Chr=?,Executor_Chr=?,ExecuteDate_Dat=sysdate,StartDate_Dat=sysdate,Status_Int=2
                             where OrderID_Chr=?
                            ";
                        arrParams = null;
                        objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                        arrParams[0].Value = strExecutorID;
                        arrParams[1].Value = strExecutor;
                        arrParams[2].Value = strOrderID;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);

                        //--ȷ����������
                        if (!strExecOrderID2.Trim().Equals(""))
                        {
                            strExecOrderID = strExecOrderID + ',' + strExecOrderID2;
                            return lngRes;
                        }
                        else
                        {
                            strExecOrderID = strExecOrderID;
                            return lngRes;
                        }
                    }

                }



                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }


            if (lngRes <= 0 || strExecOrderID.Trim() == "")
            {
                throw (new Exception("ҽ��ִ�д���"));
            }
            return lngRes;
        }
        #endregion

        #region ͨ��ִ�е���¼����շ���Ŀ
        /// <summary>
        /// ͨ��ִ�е���¼����շ���Ŀ ҵ��˵���� 
        /// 1��if(���� & �Ʒ�) then �շ� else ���շѣ�
        /// 2��if(���� || �Դ�ҩ) then ���շ� else �շѣ�
        /// </summary>
        /// <param name="strExecOrderID">ִ�е�ID</param>
        /// <param name="strEmpName">ִ��������</param>
        /// <param name="strEmpID">ִ����ID</param>
        /// <param name="dtExecDate">ִ��ʱ��</param>
        [AutoComplete]
        private void ExecOrderToChargeItem(string strExecOrderID, string strEmpName, string strEmpID, DateTime dtExecDate)
        {
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            long lngAff = 0;

            string strOrderID = "";        //--ҽ����ˮ��
            string strDicID = "";          //--������Ŀ��ˮ��
            string strOrderCateID = "";    // --ҽ������ID
            string strChargeID = "";       //--
            string strDefaultItemID = "";  //--���շ���ĿID
            int dmlDefaultAmount = 0;  //-һ������
            int intID = 0;               //--���˷�����ϸ��ˮ��
            // --����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
            int intIsRepare = 0;
            //--��ȡ���ñ�־{1=�Ա�;2=����;3=����ҩ;4=��Ժ��ҩ};����ҩ:�����Ա�ҩ
            int intRateType = 0;
            int intCreateType = 0;        //-- 3 ����(ҽ��)    1�Զ�(ҽ��)
            int intIsRich = 0;                  //--�շ���Ŀ�Ĺ��ر�־
            int intChargeItemStatus = 0;        //--����״̬��0-��ȷ�ϣ�1-���ᣩ
            decimal dmlAmount = 0;
            string strPatientID = "";      //--����ID
            string strRegisterID = "";     //--��Ժ�Ǽ�ID
            int intExecuteType = 0;             //--ҽ��ִ������{1=����;2=����;3=�����¿���}
            string strCalcCateID = "";          //--��ĿסԺ�������
            string strINvCateID = "";           //--��ĿסԺ��Ʊ���
            decimal dmlPrice = 0;          //--סԺ����(=��Ŀ�۸�/��װ��)
            int intRecruit = 0;                 //--�Ƿ񲹴�{1/0}
            string strDeptID = "";              //--����ID [����ID]
            int intTimes = 0;                   //--��λƵ��ִ�еĴ���
            int intDosageView = 0;              //--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1

            /** @add by xzf (05-10-20) */
            int intCreateChargeType = 0; //-- ������Ч��ʽ{1=ִ��ҽ��ʱ��Ч;2=ִ�п��ҽ���ҽ��ʱ��Ч}

            /* <<====================== */

            /*
             --��ȡҽ����ˮ��
        select nvl(OrderID_Chr,'') into strOrderID from T_Opr_Bih_OrderExecute where OrderExecID_Chr=strExecOrderID;
        --if(strOrderID is null) then return; end if;
        --��ȡ������Ŀ��ˮ��
        select OrderDicID_Chr into strDicID from T_Opr_Bih_Order where Trim(OrderID_Chr)=Trim(strOrderID);
        --if(strDicID is null) then return; end if;
        --��ȡҽ������ID
        SELECT ordercateid_chr into strOrderCateID FROM t_bse_bih_orderdic WHERE trim(orderdicid_chr)=Trim(strDicID);
        if(strOrderCateID is null) then strOrderCateID:='';end if;
        --��ȡ���շ���ĿID
        select Trim(ItemID_Chr) into strDefaultItemID from T_Bse_Bih_OrderDic where OrderDicID_Chr=strDicID;
        --��ȡ�Ƿ񲹴�{1/0}
        select IsRecruit_Int into intRecruit from T_opr_Bih_OrderExecute where  OrderExecID_Chr=strExecOrderID;
      */
            /*
      ����ҽ��ִ������{1=����;2=����;3=�����¿���}
      ע��:T_Opr_Bih_Order�� ִ������{1=����;2=��ʱ}
      ҵ������:
          if������ & ����) then intExecuteType=3;
      */
            /*
        select ExecuteType_Int into intExecuteType from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        if(intExecuteType=1 and intRecruit=1) then intExecuteType:=3;end if;
        --��ȡ�Ƿ񲹵�:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
        select IsRepare_Int into intIsRepare from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        if(intExecuteType=2 and (intIsRepare is not null) and (intIsRepare=3 or intIsRepare=4)) then return;end if;
        --��ȡ���ñ�־{1=�Ա�;2=����;3=����ҩ;4=��Ժ��ҩ};
        select RateType_Int into intRateType from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        if(intRateType=1 or intRateType=2) then return;end if;
        --��ȡ������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1
        SELECT dosageviewtype into intDosageView from t_aid_bih_ordercate where trim(ordercateid_chr)=trim(strOrderCateID);
        if((intDosageView is not null) and intDosageView=2) then
            dmlDefaultAmount:=1;
        else
            --��ȡ����(�Ѿ������Ժ��ҩ�����)
            select Get_Dec into dmlDefaultAmount from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
            if(dmlDefaultAmount is null) then dmlDefaultAmount:=0;end if;
        end if;
        --select IsRich_Int into intIsRich from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        --��ȡ����ID
        select PatientID_Chr into strPatientID from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        --��ȡ��Ժ�Ǽ�id
        select substr(RegisterID_Chr,1,12) into strRegisterID from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        --��ȡ����ID
        select AreaID_Chr into strDeptID from T_Opr_BIH_Register where RegisterID_Chr=strRegisterID;
        --��λƵ��ִ�еĴ���
        SELECT times_int  into intTimes FROM t_aid_recipefreq WHERE t_aid_recipefreq.freqid_chr=(SELECT EXECFREQID_CHR FROM t_opr_bih_order WHERE orderid_chr=strOrderID);
        if(intTimes is null) then intTimes:=1;end if;
        */
            /** @add by xzf (05-10-20) */
            //select CreateChargeType into intCreateChargeType from t_aid_bih_orderCate where ORDERCATEID_CHR=strOrderCateID;
            /* <<============================= */

            string strSQL = "";
            strSQL = @"
            select
            a.orderid_chr,a.IsRecruit_Int,
            b.RateType_Int,b.IsRepare_Int,b.orderdicid_chr,b.ExecuteType_Int,b.Get_Dec,b.IsRich_Int,b.PatientID_Chr,b.RegisterID_Chr,
            c.ordercateid_chr,c.itemid_chr,
            d.AreaID_Chr,
            e.times_int,
            f.dosageviewtype,f.CreateChargeType
            from 
            T_Opr_Bih_OrderExecute a,
            T_Opr_Bih_Order b,
            t_bse_bih_orderdic c,
            T_Opr_BIH_Register d,
            t_aid_recipefreq e,
            t_aid_bih_ordercate f
            where 
            a.orderid_chr=b.orderid_chr
            and
            b.orderdicid_chr=c.orderdicid_chr
            and
            b.RegisterID_Chr=d.RegisterID_Chr
            and 
            b.EXECFREQID_CHR=e.freqid_chr(+)
            and 
            c.ordercateid_chr=f.ordercateid_chr(+)
            and
            a.OrderExecID_Chr=?

            ";
            objHRPSvc.CreateDatabaseParameter(1, out arrParams);
            arrParams[0].Value = strExecOrderID;
            lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                strOrderID = clsConverter.ToString(dtbResult.Rows[0]["OrderID_Chr"].ToString());
                strDicID = clsConverter.ToString(dtbResult.Rows[0]["orderdicid_chr"].ToString());
                strOrderCateID = clsConverter.ToString(dtbResult.Rows[0]["ordercateid_chr"].ToString());
                strDefaultItemID = clsConverter.ToString(dtbResult.Rows[0]["ItemID_Chr"].ToString());
                intRecruit = clsConverter.ToInt(dtbResult.Rows[0]["IsRecruit_Int"].ToString());
                intExecuteType = clsConverter.ToInt(dtbResult.Rows[0]["ExecuteType_Int"].ToString());
                intIsRepare = clsConverter.ToInt(dtbResult.Rows[0]["IsRepare_Int"].ToString());
                intRateType = clsConverter.ToInt(dtbResult.Rows[0]["RateType_Int"].ToString());
                intDosageView = clsConverter.ToInt(dtbResult.Rows[0]["dosageviewtype"].ToString());
                dmlDefaultAmount = clsConverter.ToInt(dtbResult.Rows[0]["Get_Dec"].ToString());
                intIsRich = clsConverter.ToInt(dtbResult.Rows[0]["IsRich_Int"].ToString());
                strPatientID = clsConverter.ToString(dtbResult.Rows[0]["PatientID_Chr"].ToString());
                strRegisterID = clsConverter.ToString(dtbResult.Rows[0]["RegisterID_Chr"].ToString());
                strDeptID = clsConverter.ToString(dtbResult.Rows[0]["AreaID_Chr"].ToString());
                intTimes = clsConverter.ToInt(dtbResult.Rows[0]["times_int"].ToString());
                intCreateChargeType = clsConverter.ToInt(dtbResult.Rows[0]["CreateChargeType"].ToString());
                //intExecuteType--ҽ��ִ������{1=����;2=����;3=�����¿���}
                //intRecruit--�Ƿ񲹴�{1/0}
                if (intExecuteType == 1 && intRecruit == 1)
                {
                    intExecuteType = 3;//if������ & ����)
                }
                // intIsRepare--����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                if (intExecuteType == 2 && (intIsRepare == 3 || intIsRepare == 4))
                {
                    return;
                }
                //intRateType--��ȡ���ñ�־{1=�Ա�;2=����;3=����ҩ;4=��Ժ��ҩ};����ҩ:�����Ա�ҩ
                if (intRateType == 1)  // || 20180404 intRateType == 2)
                {
                    return;
                }
                //--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1
                if (intDosageView == 2)
                {
                    dmlDefaultAmount = 1;
                }
                //--��λƵ��ִ�еĴ���
                if (intTimes == 0)
                {
                    intTimes = 1;
                }


                /* 
                  --Loop All Charge Item
             for i_item in ( select A.ItemID_Chr , A.QTY_INT ,A.type_int,B.ItemName_Vchr,B.ItemIPUnit_Chr
                                 ,decode(IPCHARGEFLG_INT,1,Round(B.ItemPrice_Mny/B.PackQty_Dec,4),0,B.ItemPrice_Mny,Round(B.ItemPrice_Mny/B.PackQty_Dec,4)) ItemPriceA
                                 ,B.ItemIPCalcType_Chr,B.ItemIpInvType_Chr,B.IsRich_Int,B.DOSAGE_DEC
                             from T_Aid_Bih_OrderDic_Charge A,T_Bse_ChargeItem B
                             where OrderDicID_Chr=strDicID and A.ItemID_Chr = B.ItemID_Chr
                             )
             loop
                     --��ȡ�շ���Ŀ�Ĺ��ر�־
                     intIsRich:=i_item.IsRich_Int;
                     --����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                     --����¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
                     if((intIsRepare is not null) and (intIsRepare=1 or intIsRepare=2)) then
                         intCreateType:=3;
                     else
                         intCreateType:=1;
                     end if;
                     --���÷���״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
                     */
                /** @update by xzf (05-10-20) */
                /** @remark
              if(intIsRich=1) then
                  intChargeItemStatus:=0;
              else
                  intChargeItemStatus:=1;
              end if;
                /* <<======== */
                /*
                    if(intIsRich=1 or intCreateChargeType=2) then
                        intChargeItemStatus:=0;
                    else
                        intChargeItemStatus:=1;
                    end if;
                    /* <<=============================== */
                /*
                --��������
                if(Trim(i_item.ItemID_Chr)=Trim(strDefaultItemID)) then
                    dmlAmount:=dmlDefaultAmount;--ס�շ���Ŀ
                else
                    --��������շ���Ŀ���շ�
                    /*
                     *ҵ��������
                     *    if(TYPE_INT==1[������λ]) then {=����*����}
                     *    if(TYPE_INT==2[������λ]) then {=�㷨�������շ���Ŀ��ҽ���µļ�����Ĭ�ϵ��Ǹ�}
                     *       ���� = ������ҩ���� * ����
                     *       ���� = ҽ���µļ���/��λ����
                     */
                /*
                         if(i_item.type_int=1) then
                            dmlAmount:=intTimes * i_item.Qty_Int;
                         else
                            dmlAmount:=intTimes * (i_item.Qty_Int/i_item.DOSAGE_DEC);
                         end if;
                    end if;
                    --������ĿסԺ�������
                    strCalcCateID:=i_item.ItemIPCalcType_Chr;
                    --������ĿסԺ��Ʊ���
                    strInvCateID:=i_item.ItemIpInvType_Chr;
                    --�����������˷�����ϸ��ˮ��
                    select to_number(max(PChargeID_Chr))+1 into intID from T_Opr_Bih_PatientCharge;
                    if(intID is null) then intID:= 1; end if;
                    strChargeID:= lpad(trim(to_char(intID)),18,'0');
                    --����סԺ����(=��Ŀ�۸�/��װ��)
                    dmlPrice:=i_item.ItemPriceA;
                    --���������ϸ��¼
                    insert into T_Opr_Bih_PatientCharge(
                        PChargeID_Chr,PatientID_Chr,RegisterID_Chr,Active_Dat,OrderID_Chr,
                        OrderExecType_Int,OrderExecID_Chr,CalCCateID_Chr,InvCateID_Chr,
                        ChargeItemID_Chr,ChargeItemName_Chr,Unit_Vchr,
                        UnitPrice_Dec,AMount_Dec,DisCount_Dec,IsMepay_Int,CreateType_Int,Creator_Chr,
                        Create_Dat,Status_Int,PStatus_Int,ClacArea_Chr,CreateArea_Chr,ISRICH_INT
                        )
                    values(
                        trim(strChargeID),trim(strPatientID),trim(strRegisterID),dtExecuteDate,trim(strOrderID),
                        intExecuteType,trim(strExecOrderID),trim(strCalcCateID),trim(strInvCateID),
                        trim(i_item.ItemID_Chr),trim(i_item.ItemName_Vchr),substr(i_item.ItemIPUnit_Chr,1,10),
                        dmlPrice,dmlAmount,1,1,intCreateType,trim(strExecutorID),
                        sysdate,1,intChargeItemStatus,trim(strDeptID),trim(strDeptID),intIsRich
                        );
            end loop;
                */
                strSQL = @"select A.ItemID_Chr , A.QTY_INT ,A.type_int,B.ItemName_Vchr,B.ItemIPUnit_Chr
                            ,decode(IPCHARGEFLG_INT,1,Round(b.itemprice_mny/B.PackQty_Dec,4),0,b.itemprice_mny,Round(b.itemprice_mny/B.PackQty_Dec,4)) ItemPriceA
                            ,B.ItemIPCalcType_Chr,B.ItemIpInvType_Chr,B.IsRich_Int,B.DOSAGE_DEC
                        from T_Aid_Bih_OrderDic_Charge A,T_Bse_ChargeItem B
                        where OrderDicID_Chr=? and A.ItemID_Chr = B.ItemID_Chr";

                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = strDicID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        // --��ȡ�շ���Ŀ�Ĺ��ر�־
                        intIsRich = clsConverter.ToInt(dtbResult.Rows[i]["IsRich_Int"].ToString());
                        string ItemID_Chr = clsConverter.ToString(dtbResult.Rows[i]["ItemID_Chr"].ToString());
                        string ItemName_Vchr = clsConverter.ToString(dtbResult.Rows[i]["ItemName_Vchr"].ToString());
                        string ItemIPUnit_Chr = clsConverter.ToString(dtbResult.Rows[i]["ItemIPUnit_Chr"].ToString());

                        int type_int = clsConverter.ToInt(dtbResult.Rows[i]["type_int"].ToString());
                        int Qty_Int = clsConverter.ToInt(dtbResult.Rows[i]["Qty_Int"].ToString());
                        decimal DOSAGE_DEC = clsConverter.ToDecimal(dtbResult.Rows[i]["DOSAGE_DEC"].ToString());
                        //     --������ĿסԺ�������
                        strCalcCateID = clsConverter.ToString(dtbResult.Rows[i]["ItemIPCalcType_Chr"].ToString());
                        //     --������ĿסԺ��Ʊ���
                        strINvCateID = clsConverter.ToString(dtbResult.Rows[i]["ItemIpInvType_Chr"].ToString());
                        //     --�����������˷�����ϸ��ˮ��
                        strSQL = "   select lpad(SEQ_PCHARGEID.Nextval,18,'0') PChargeID_Chr   from dual ";
                        DataTable dtbResult2 = new DataTable();
                        objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
                        strChargeID = clsConverter.ToString(dtbResult2.Rows[0]["PChargeID_Chr"].ToString());
                        //    --����סԺ����(=��Ŀ�۸�/��װ��)
                        dmlPrice = clsConverter.ToDecimal(dtbResult.Rows[i]["ItemPriceA"].ToString());

                        // --����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                        // --����¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
                        if (intIsRepare == 1 || intIsRepare == 2)
                        {
                            intCreateType = 3;
                        }
                        else
                        {
                            intCreateType = 1;
                        }

                        // --���÷���״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}

                        if (intIsRich == 1 || intCreateChargeType == 2)
                        {
                            intChargeItemStatus = 0;
                        }
                        else
                        {
                            intChargeItemStatus = 1;
                        }

                        //--��������
                        if (ItemID_Chr.Equals(strDefaultItemID))
                        {
                            dmlAmount = dmlDefaultAmount;//--ס�շ���Ŀ
                        }
                        else
                        {
                            //--��������շ���Ŀ���շ�
                            /*
                             *ҵ��������
                             *    if(TYPE_INT==1[������λ]) then {=����*����}
                             *    if(TYPE_INT==2[������λ]) then {=�㷨�������շ���Ŀ��ҽ���µļ�����Ĭ�ϵ��Ǹ�}
                             *       ���� = ������ҩ���� * ����
                             *       ���� = ҽ���µļ���/��λ����
                             */
                            if (type_int == 1)
                            {
                                dmlAmount = intTimes * Qty_Int;
                            }
                            else
                            {
                                dmlAmount = intTimes * (Qty_Int / DOSAGE_DEC);
                            }

                        }

                        // ��ȡ�շ���Ŀ��Ӧ������/ִ�п���
                        DataTable dtbResult3 = new DataTable();
                        string CREATEAREA_CHR = "";//�������
                        string CLACAREA_CHR = "";//ִ�п���
                        strSQL = @"select CREATEAREA_CHR,CLACAREA_CHR from T_OPR_BIH_ORDERCHARGEDEPT 
                                   where ORDERID_CHR=? and ORDERDICID_CHR=? and CHARGEITEMID_CHR=?";
                        objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                        arrParams[0].Value = strOrderID;
                        arrParams[1].Value = strDicID;
                        arrParams[2].Value = ItemID_Chr;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult3, arrParams);
                        if (lngRes > 0 && dtbResult3.Rows.Count > 0)
                        {
                            CREATEAREA_CHR = clsConverter.ToString(dtbResult3.Rows[0]["CREATEAREA_CHR"].ToString());
                            CLACAREA_CHR = clsConverter.ToString(dtbResult3.Rows[0]["CLACAREA_CHR"].ToString());
                        }
                        /*<----------------------------------------------------------*/


                        //    --���������ϸ��¼
                        strSQL = @" 
                    insert into T_Opr_Bih_PatientCharge(
                    PChargeID_Chr,    PatientID_Chr,  RegisterID_Chr,   Active_Dat,     OrderID_Chr,
                    OrderExecType_Int,OrderExecID_Chr,CalCCateID_Chr,   InvCateID_Chr,   ChargeItemID_Chr,
                    ChargeItemName_Chr,Unit_Vchr,      UnitPrice_Dec,   AMount_Dec,       DisCount_Dec,
                    IsMepay_Int,      CreateType_Int,  Creator_Chr,     Create_Dat,      Status_Int,
                    PStatus_Int,      ClacArea_Chr,    CreateArea_Chr,  ISRICH_INT
                    )
                    values(
                    ?,?,?,?,?,
                    ?,?,?,?,?,
                    ?,?,?,?,?,
                    ?,?,?,sysdate,?,
                    ?,?,?,?
                    )";
                        objHRPSvc.CreateDatabaseParameter(23, out arrParams);
                        arrParams[0].Value = strChargeID.Trim();//PChargeID_Chr
                        arrParams[1].Value = strPatientID.Trim();//PatientID_Chr
                        arrParams[2].Value = strRegisterID.Trim();//RegisterID_Chr
                        arrParams[3].Value = dtExecDate;//Active_Dat
                        arrParams[4].Value = strOrderID.Trim();//OrderID_Chr

                        arrParams[5].Value = intExecuteType;//OrderExecType_Int ҽ��ִ������{1=����;2=����;3=�����¿���}
                        arrParams[6].Value = strExecOrderID.Trim();//OrderExecID_Chr
                        arrParams[7].Value = strCalcCateID.Trim();//CalCCateID_Chr  ���ú������id {=�շ���Ŀ����.id}
                        arrParams[8].Value = strINvCateID.Trim();//InvCateID_Chr    ���÷�Ʊ���id {=�շ���Ŀ����.id}
                        arrParams[9].Value = ItemID_Chr.Trim();//ChargeItemID_Chr

                        arrParams[10].Value = ItemName_Vchr.Trim();//ChargeItemName_Chr
                        arrParams[11].Value = ItemIPUnit_Chr.Trim();//Unit_Vchr סԺ��λ{=�շ���Ŀ.סԺ��λ}
                        arrParams[12].Value = dmlPrice;//UnitPrice_Dec  סԺ����{=�շ���Ŀ.סԺ����}
                        arrParams[13].Value = dmlAmount;//AMount_Dec    ����
                        arrParams[14].Value = 1;//DisCount_Dec=1���ۿ۱���

                        arrParams[15].Value = 1;//IsMepay_Int=1�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
                        arrParams[16].Value = intCreateType;//CreateType_Int ¼������ {1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
                        arrParams[17].Value = strEmpID.Trim();//Creator_Chr
                        //Create_Dat
                        arrParams[18].Value = 1;// Status_Int


                        arrParams[19].Value = intChargeItemStatus;//PStatus_Int
                        ////arrParams[17].Value = strDeptID.Trim();
                        //// arrParams[18].Value = strDeptID.Trim();
                        arrParams[20].Value = CLACAREA_CHR.Trim();//ClacArea_Chr
                        arrParams[21].Value = CREATEAREA_CHR.Trim();//CreateArea_Chr
                        arrParams[22].Value = intIsRich;//ISRICH_INT
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);


                    }
                }


                objHRPSvc.Dispose();
            }
        }
        #endregion

        #region ����շ���Ŀ��¼�÷�
        /// <summary>
        /// ����շ���Ŀ��¼�÷�
        /// </summary>
        /// <param name="strUsageID"></param>
        /// <param name="strExecOrderID"></param>
        /// <param name="strExecutor"></param>
        /// <param name="strExecutorID"></param>
        /// <param name="dtExecuteDate"></param>
        [AutoComplete]
        private void UsageToChargeItem(string strUsageID, string strExecOrderID, string strExecutor, string strExecutorID, DateTime dtExecuteDate)
        {

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            long lngAff = 0;

            string strOrderID; //    --ҽ����ˮ��
            string strDicID;//     --������Ŀ��ˮ��
            // --����:{1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
            int intIsRepare;
            int intIsRich;       //       --�շ���Ŀ�Ĺ��ر�־
            string strPatientID; // --����ID
            string strRegisterID;// --��Ժ�Ǽ�ID
            int intExecuteType;  //       --ҽ��ִ������{1=����;2=����;3=�����¿���}
            string strChargeID;
            int intID;//           --������ϸ��ˮ��
            int intCreateType; //         --
            decimal dmlAmount;//     --��
            int intRecruit;//             --��ȡ�Ƿ񲹴�{1/0}
            string strCalcCateID;//      --��ĿסԺ�������
            string strInvCateID;//       --��ĿסԺ��Ʊ���
            int intChargeItemStatus;//    --����״̬��0-��ȷ�ϣ�1-���ᣩ
            string strDeptID;//          --����ID [����ID]
            int intTimes;//               --��λƵ��ִ�еĴ���
            int intTimesBak;//            --��λƵ��ִ�еĴ���
            int intCount; //              --��ʱ����
            decimal dblBihqty;
            decimal dblDosage;

            /*
              --��ȡҽ��ID
        select nvl(OrderID_Chr,'') into strOrderID from T_Opr_Bih_OrderExecute where OrderExecID_Chr=strExecOrderID;
        if(strOrderID is null) then return; end if;
       --��ȡ������ĿID
        select OrderDicID_Chr into strDicID from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        if(strDicID is null) then strDicID:=''; end if;
        --��ȡ�Ƿ񲹴�{1/0}
        select IsRecruit_Int into intRecruit from T_opr_Bih_OrderExecute where  OrderExecID_Chr=strExecOrderID;
             */
            /*
            ����ҽ��ִ������{1=����;2=����;3=�����¿���}
            ע��:T_Opr_Bih_Order�� ִ������{1=����;2=��ʱ}
            ҵ������:
                if������ & ����) then intExecuteType=3;
            */
            /*
        select ExecuteType_Int into intExecuteType from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        if(intExecuteType=1 and intRecruit=1) then intExecuteType:=3;end if;
        --����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
        select IsRepare_Int into intIsRepare from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        if(intExecuteType=2 and (intIsRepare is not null) and (intIsRepare=3 or intIsRepare=4)) then return;end if;
        --��ȡ����ID
        select PatientID_Chr into strPatientID from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        --��Ժ�Ǽ���ˮ��
        select substr(RegisterID_Chr,1,12) into strRegisterID from T_Opr_Bih_Order where OrderID_Chr=strOrderID;
        --��ȡ����ID [����ID]
        select AreaID_Chr into strDeptID from T_Opr_BIH_Register where RegisterID_Chr=strRegisterID;
        --��λƵ��ִ�еĴ���
        --2��if(����ҽ������λƵ�ʴ���ȡ1��
        SELECT times_int  into intTimes FROM t_aid_recipefreq WHERE t_aid_recipefreq.freqid_chr=(SELECT EXECFREQID_CHR FROM t_opr_bih_order WHERE orderid_chr=strOrderID);
        if(intTimes is null) then intTimes:=1;end if;
        ?SELECT COUNT (*) into intCount FROM t_bse_bih_specordercate WHERE confreqid_chr=(SELECT EXECFREQID_CHR FROM t_opr_bih_order WHERE orderid_chr=strOrderID);
        ?if(intCount>0) then intTimes:=1;end if;
             */
            string strSQL = "";
            strSQL = @"
            select
            a.OrderID_Chr,
            a.IsRecruit_Int,
            b.OrderDicID_Chr,
            b.ExecuteType_Int,
            b.IsRepare_Int,
            b.PatientID_Chr,
            b.RegisterID_Chr,
            c.AreaID_Chr,
            d.times_int,
            (select count(e.bedchargecate) from t_bse_bih_specordercate e where b.EXECFREQID_CHR=e.confreqid_chr) intCount
             from 
            T_Opr_Bih_OrderExecute a,
            T_Opr_Bih_Order b,
            T_Opr_BIH_Register c,
            t_aid_recipefreq d
            
            where 
            a.OrderID_Chr=b.OrderID_Chr
            and
            b.RegisterID_Chr=c.RegisterID_Chr
            and
            b.EXECFREQID_CHR=d.freqid_chr(+)
           
            
            and
            a.OrderExecID_Chr=?

            ";
            objHRPSvc.CreateDatabaseParameter(1, out arrParams);
            arrParams[0].Value = strExecOrderID;
            lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                strOrderID = clsConverter.ToString(dtbResult.Rows[0]["OrderID_Chr"].ToString());
                strDicID = clsConverter.ToString(dtbResult.Rows[0]["OrderDicID_Chr"].ToString());
                intRecruit = clsConverter.ToInt(dtbResult.Rows[0]["IsRecruit_Int"].ToString());
                intExecuteType = clsConverter.ToInt(dtbResult.Rows[0]["ExecuteType_Int"].ToString());
                intIsRepare = clsConverter.ToInt(dtbResult.Rows[0]["IsRepare_Int"].ToString());
                strPatientID = clsConverter.ToString(dtbResult.Rows[0]["PatientID_Chr"].ToString());
                strRegisterID = clsConverter.ToString(dtbResult.Rows[0]["RegisterID_Chr"].ToString());
                strDeptID = clsConverter.ToString(dtbResult.Rows[0]["AreaID_Chr"].ToString());
                intTimes = clsConverter.ToInt(dtbResult.Rows[0]["times_int"].ToString());
                intCount = clsConverter.ToInt(dtbResult.Rows[0]["intCount"].ToString());

                if (strOrderID.Trim().Equals(""))
                {
                    return;
                }

                if (intExecuteType == 1 && intRecruit == 1)
                {
                    intExecuteType = 3;
                }


                if ((intExecuteType == 2 && intIsRepare != 0) && (intIsRepare == 3 || intIsRepare == 4))
                {
                    return;
                }
                if (intTimes == 0)
                {
                    intTimes = 1;
                }

                /*
                 --Loop All Charge Item
           for i_item in ( select  A.ItemID_Chr , A.BIHQTY_DEC,A.BIHTYPE_INT,A.ContinueUseType_Int,B.ItemName_Vchr,B.ItemIPUnit_Chr,B.DOSAGE_DEC
                                   ,decode(IPCHARGEFLG_INT,0,Round(B.ItemPrice_Mny/B.PackQty_Dec,4),1,B.ItemPrice_Mny,Round(B.ItemPrice_Mny/B.PackQty_Dec,4)) ItemPriceA
                                   ,B.ItemIPCalcType_Chr,B.ItemIpInvType_Chr,B.IsRich_Int
                           from T_BSE_ChargeItemUsageGroup A,T_Bse_ChargeItem B
                           where A.UsageID_Chr=strUsageID and A.ItemID_Chr = B.ItemID_Chr
                           )
                 */
                strSQL = @"select  A.ItemID_Chr , A.BIHQTY_DEC,A.BIHTYPE_INT,A.ContinueUseType_Int,B.ItemName_Vchr,B.ItemIPUnit_Chr,B.DOSAGE_DEC
                                ,decode(IPCHARGEFLG_INT,0,Round(b.itemprice_mny/B.PackQty_Dec,4),1,b.itemprice_mny,Round(b.itemprice_mny/B.PackQty_Dec,4)) ItemPriceA
                                ,B.ItemIPCalcType_Chr,B.ItemIpInvType_Chr,B.IsRich_Int
                        from T_BSE_ChargeItemUsageGroup A,T_Bse_ChargeItem B
                        where A.UsageID_Chr=? and A.ItemID_Chr = B.ItemID_Chr ";

                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = strUsageID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                if (lngRes > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {

                        int ContinueUseType_Int = clsConverter.ToInt(dtbResult.Rows[i]["ContinueUseType_Int"].ToString());
                        string ItemID_Chr = clsConverter.ToString(dtbResult.Rows[i]["ItemID_Chr"].ToString());
                        decimal BIHQTY_DEC = clsConverter.ToDecimal(dtbResult.Rows[i]["BIHQTY_DEC"].ToString());
                        decimal DOSAGE_DEC = clsConverter.ToDecimal(dtbResult.Rows[i]["DOSAGE_DEC"].ToString());
                        int BIHTYPE_INT = clsConverter.ToInt(dtbResult.Rows[i]["BIHTYPE_INT"].ToString());
                        string ItemIpInvType_Chr = clsConverter.ToString(dtbResult.Rows[i]["ItemIpInvType_Chr"].ToString());
                        string ItemName_Vchr = clsConverter.ToString(dtbResult.Rows[i]["ItemName_Vchr"].ToString());
                        string ItemIPUnit_Chr = clsConverter.ToString(dtbResult.Rows[i]["ItemIPUnit_Chr"].ToString());
                        decimal ItemPriceA = clsConverter.ToDecimal(dtbResult.Rows[i]["ItemPriceA"].ToString());


                        /*
                        intTimesBak :=intTimes;
                   intIsRich:=i_item.IsRich_Int;
                   --�����������˷�����ϸ��ˮ��
                   select to_number(max(PChargeID_Chr))+1 into intID from T_Opr_Bih_PatientCharge;
                   if(intID is null) then intID:= 1; end if;
                   strChargeID:= lpad(trim(to_char(intID)),18,'0');
                   --����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                   --����¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
                   if((intIsRepare is not null) and (intIsRepare=1 or intIsRepare=2)) then
                       intCreateType:=3;
                   else
                       intCreateType:=1;
                   end if;
                         */
                        intIsRich = clsConverter.ToInt(dtbResult.Rows[i]["IsRich_Int"].ToString());
                        intTimesBak = intTimes;
                        strSQL = "   select lpad(SEQ_PCHARGEID.Nextval,18,'0') PChargeID_Chr   from dual ";
                        DataTable dtbResult2 = new DataTable();
                        objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
                        strChargeID = clsConverter.ToString(dtbResult2.Rows[0]["PChargeID_Chr"].ToString());
                        if (intIsRepare == 1 || intIsRepare == 2)
                        {
                            intCreateType = 3;
                        }
                        else
                        {
                            intCreateType = 1;
                        }

                        /*
                3�������շ��߼���   �������� {0=������;1=ȫ������;2-��������}
                if(������) �շ�;
                if(ȫ������)
                {
                    if(�չ���) continue(����)��else �շѣ�
                    ��λƵ�ʴ���=1��
                }
                if(����ҽ������)
                {
                    if(����ҽ��)
                    {
                        if(�չ���) continue(����)��else �շѣ�
                    }
                    ��λƵ�ʴ���=1��
                }
                */
                        /*
                   if((i_item.ContinueUseType_Int is not null) and (i_item.ContinueUseType_Int=1)) then
                       if(blnIsCharged(strRegisterID,strOrderID,i_item.ItemID_Chr)) then
                           GOTO label_name;--continue;
                       end if;
                       intTimes :=1;--����,��λƵ����1����;
                   end if;
                   if((i_item.ContinueUseType_Int is not null) and (i_item.ContinueUseType_Int=2)) then
                       if(intExecuteType=1 or intExecuteType=3) then
                           if(blnIsCharged(strRegisterID,strOrderID,i_item.ItemID_Chr)) then
                              GOTO label_name;--continue;
                           end if;
                           intTimes :=1;--����,��λƵ����1����;
                       end if;
                   end if;
                         */
                        if (ContinueUseType_Int == 1)
                        {
                            if (blnIsCharged(strRegisterID, strOrderID, ItemID_Chr))
                            {
                                continue;
                            }
                            intTimes = 1;
                        }
                        else if (ContinueUseType_Int == 2)
                        {
                            if (intExecuteType == 1 || intExecuteType == 3)
                            {
                                if (blnIsCharged(strRegisterID, strOrderID, ItemID_Chr))
                                {
                                    continue;
                                }
                                intTimes = 1;
                            }
                        }
                        // --�����÷�����
                        /*
                        *ҵ��������
                        *    if(TYPE_INT==1[������λ]) then {=����*����}
                        *    if(TYPE_INT==2[������λ]) then {=�㷨�������շ���Ŀ��ҽ���µļ�����Ĭ�ϵ��Ǹ�}
                        *       ���� = ������ҩ���� * ����
                        *       ���� = ҽ���µļ���/��λ����
                        */
                        /*
                   if(i_item.BIHQTY_DEC is null) then
                       dblBihqty:=1;
                   else
                       dblBihqty:=i_item.BIHQTY_DEC;
                   end if;
                   if(i_item.DOSAGE_DEC is null) then
                       dblDosage:=1;
                   else
                       dblDosage:=i_item.DOSAGE_DEC;
                   end if;
                   if(i_item.BIHTYPE_INT=1) then
                       dmlAmount:=intTimes * dblBihqty;
                   else
                       dmlAmount:=intTimes * (dblBihqty/dblDosage);
                   end if;
                        */
                        if (BIHQTY_DEC == 0)
                        {
                            dblBihqty = 1;
                        }
                        else
                        {
                            dblBihqty = BIHQTY_DEC;
                        }
                        if (DOSAGE_DEC == 0)
                        {
                            dblDosage = 1;
                        }
                        else
                        {
                            dblDosage = DOSAGE_DEC;
                        }
                        if (BIHTYPE_INT == 1)
                        {
                            dmlAmount = intTimes * dblBihqty;
                        }
                        else
                        {
                            dmlAmount = intTimes * (dblBihqty / dblDosage);
                        }

                        /*
                          --���÷���״̬	{0=��ȷ��;1=����;2=����;3=����;4=ֱ��}
                   if(intIsRich=1) then
                       intChargeItemStatus:=0;
                   else
                       intChargeItemStatus:=1;
                   end if;
                   --������ĿסԺ�������
                   strCalcCateID:=i_item.ItemIPCalcType_Chr;
                   --������ĿסԺ��Ʊ���
                   strInvCateID:=i_item.ItemIpInvType_Chr;
                    insert into T_Opr_Bih_PatientCharge(
                       PChargeID_Chr,PatientID_Chr,RegisterID_Chr,Active_Dat,OrderID_Chr,
                       OrderExecType_Int,OrderExecID_Chr,CalCCateID_Chr,InvCateID_Chr,
                       ChargeItemID_Chr,ChargeItemName_Chr,Unit_Vchr,
                       UnitPrice_Dec,AMount_Dec,DisCount_Dec,IsMepay_Int,CreateType_Int ,
                       Creator_Chr,Create_Dat,Status_Int,PStatus_Int,ClacArea_Chr,CreateArea_Chr
                       )
                   values(
                       trim(strChargeID),trim(strPatientID),trim(strRegisterID),dtExecuteDate,trim(strOrderID),
                       intExecuteType,trim(strExecOrderID),trim(strCalcCateID),trim(strInvCateID),
                       trim(i_item.ItemID_Chr),substr(i_item.ItemName_Vchr,0,20),substr(i_item.ItemIPUnit_Chr,0,10),
                       i_item.ItemPriceA,dmlAmount,1,1,intCreateType,
                       trim(strExecutorID),sysdate,1,intChargeItemStatus,trim(strDeptID),trim(strDeptID)
                       );
                   <<label_name>>
                   intTimes :=intTimesBak;
                         */
                        if (intIsRich == 1)
                        {
                            intChargeItemStatus = 0;
                        }
                        else
                        {
                            intChargeItemStatus = 1;
                        }
                        strCalcCateID = ItemIpInvType_Chr;
                        strInvCateID = ItemIpInvType_Chr;


                        // ��ȡ�շ���Ŀ��Ӧ������/ִ�п���
                        DataTable dtbResult3 = new DataTable();
                        string CREATEAREA_CHR = "";//�������
                        string CLACAREA_CHR = "";//ִ�п���
                        strSQL = @"select CREATEAREA_CHR,CLACAREA_CHR from T_OPR_BIH_ORDERCHARGEDEPT 
                                   where ORDERID_CHR=? and ORDERDICID_CHR=? and CHARGEITEMID_CHR=?";
                        objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                        arrParams[0].Value = strOrderID;
                        arrParams[1].Value = strDicID;
                        arrParams[2].Value = ItemID_Chr;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult3, arrParams);
                        if (lngRes > 0 && dtbResult3.Rows.Count > 0)
                        {
                            CREATEAREA_CHR = clsConverter.ToString(dtbResult3.Rows[0]["CREATEAREA_CHR"].ToString());
                            CLACAREA_CHR = clsConverter.ToString(dtbResult3.Rows[0]["CLACAREA_CHR"].ToString());
                        }
                        /*<----------------------------------------------------------*/



                        strSQL = @"  insert into T_Opr_Bih_PatientCharge(
                    PChargeID_Chr,PatientID_Chr,RegisterID_Chr,Active_Dat,OrderID_Chr,
                    OrderExecType_Int,OrderExecID_Chr,CalCCateID_Chr,InvCateID_Chr,
                    ChargeItemID_Chr,ChargeItemName_Chr,Unit_Vchr,
                    UnitPrice_Dec,AMount_Dec,DisCount_Dec,IsMepay_Int,CreateType_Int ,
                    Creator_Chr,Create_Dat,Status_Int,PStatus_Int,ClacArea_Chr,CreateArea_Chr
                    )
                    values(
                    ?,?,?,?,?,
                    ?,?,?,?,
                    ?,?,?,
                    ?,?,1,1,?,
                    ?,sysdate,1,?,?,?
                    )
                    ";

                        objHRPSvc.CreateDatabaseParameter(19, out arrParams);
                        arrParams[0].Value = strChargeID.Trim();
                        arrParams[1].Value = strPatientID.Trim();
                        arrParams[2].Value = strRegisterID.Trim();
                        arrParams[3].Value = dtExecuteDate;
                        arrParams[4].Value = strOrderID.Trim();
                        arrParams[5].Value = intExecuteType;
                        arrParams[6].Value = strExecOrderID.Trim();
                        arrParams[7].Value = strCalcCateID.Trim();
                        arrParams[8].Value = strInvCateID.Trim();
                        arrParams[9].Value = ItemID_Chr.Trim();
                        arrParams[10].Value = ItemName_Vchr.Trim();
                        arrParams[11].Value = ItemIPUnit_Chr.Trim();
                        arrParams[12].Value = ItemPriceA;
                        arrParams[13].Value = dmlAmount;
                        arrParams[14].Value = intCreateType;
                        arrParams[15].Value = strExecutorID.Trim();
                        arrParams[16].Value = intChargeItemStatus;
                        //  arrParams[17].Value = strDeptID.Trim();
                        // arrParams[18].Value = strDeptID.Trim();
                        arrParams[17].Value = CLACAREA_CHR.Trim();
                        arrParams[18].Value = CREATEAREA_CHR.Trim();
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
                    }
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region ����ҽ����Ӧ�շ���Ŀ�����Ƿ��Ѿ��չ�����
        /// <summary>
        /// ����ҽ����Ӧ�շ���Ŀ�����Ƿ��Ѿ��չ�����
        /// </summary>
        /// <param name="strRegisterID"></param>
        /// <param name="strOrderID"></param>
        /// <param name="strItemID"></param>
        /// <returns></returns>
        [AutoComplete]
        private Boolean blnIsCharged(string strRegisterID, string strOrderID, string strItemID)
        {
            /*
              ����˵����
        ����ҽ����Ӧ�շ���Ŀ�����Ƿ��Ѿ��չ�����
        �����÷��շѵ������߼�
        ����:{��Ժ�ǼǺš��շ���Ŀ���Ʒ�ʱ��=���졢��Դ=ҽ��}
    ����˵����
        strRegisterID   ��Ժ�ǼǺ�
        strOrderID      ҽ��ID
        strItemID       �շ���ĿID
    ��ע��
        --¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
            */
            /*
   function blnIsCharged(strRegisterID varchar2,strOrderID varchar2,strItemID varchar2) return Boolean
   is
       intCout int;                --����
   begin
       SELECT count(*) into intCout FROM t_opr_bih_patientcharge
        WHERE status_int = 1
           AND TRIM (registerid_chr) = TRIM (strRegisterID)
           AND TRIM (chargeitemid_chr) = TRIM (strItemID)
           AND TRUNC (create_dat) = TRUNC (sysdate)
           AND (createtype_int = 1 OR createtype_int = 3);

       if(intCout=0) then
           return(false);
       else
           return(true);
       end if;
   end blnIsCharged;
       */
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            int intCount = 0;
            string strSQL = @"select count (pchargeid_chr) intcount
                                  from t_opr_bih_patientcharge
                                 where status_int = 1
                                   and registerid_chr = ?
                                   and chargeitemid_chr = ?
                                   and trunc (create_dat) = trunc (sysdate)
                                   and (createtype_int = 1 or createtype_int = 3)";
            objHRPSvc.CreateDatabaseParameter(2, out arrParams);
            arrParams[0].Value = strRegisterID;
            arrParams[1].Value = strItemID;
            lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                intCount = clsConverter.ToInt(dtbResult.Rows[0]["intCount"].ToString());
            }

            if (intCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            return false;
        }
        #endregion

        #region ��ҩ -- ����ȷ���÷�������ʹ�� 20180320
        /// <summary>
        /// ��ҩ
        /// </summary>
        /// <param name="strExecOrderID"></param>
        /// <param name="strExecutorID"></param>
        [AutoComplete]
        private void ProcessOrderMedicine(string strExecOrderID, string strExecutorID)
        {
            /*
    ����˵����
        ��ҩ
    ����˵����
        strExecOrderID  ִ�е�ID
        strCreatorID    ִ����ID
    ҵ��˵����
        if(���շ���Ŀ����ҩƷ�շ���Ŀ) then ����ҽ������ҩ;
        if(�����շ���Ŀ����ҩƷ�շ���Ŀ) then this���ҩ;
        if(����:{2=�Ʒ�-����ҩ;4=���Ʒ�-����ҩ}) then ����ҩ --ֻ������ʱҽ��
        if(��ҩ��ʽ������) then ���շ�\����ҩ;
        if(����Ϊ��) then Ĭ��ȡ1�� ��
    */

            string strOrderID;  //      --ҽ����ˮ��
            string strPatientID;  //    --����ID
            string strRegisterID; //    --��Ժ�Ǽ�ID
            string strAreaID;      //    --����ID [����ID]
            int intExecuteType;          //   --ִ������{1=����;2=��ʱ}
            int intRecipeNo;             //   --����	���ڱ�����ʾ
            string strDicID;           //   --������Ŀ��ˮ��
            string strMainItemID;      //   --���շ���ĿID
            decimal dmlMainDosage;    // --һ�μ���
            string strMainDosageUnit;// --������λ	{=this.get������Ŀ.get�շ���Ŀ.������λ}
            decimal dmlDosage;
            string strDosageUnit;
            int intRecruit;                // --�Ƿ񲹴�{1/0}
            //--�շ���Ŀ��Դ���� ������Դ��ȷ��ֵ��Χ,����1��ҩƷ��2�����ϱ�ȡ�
            int intItemSrctype;
            //--����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
            int intIsRepare;
            string strDoseTypeID;         // --��ҩ��ʽid
            string strExecFreqID;         // --ִ��Ƶ��	{=this.getҽ��Ƶ��.name}
            int intExecuteTimes;           // --ִ�д���
            int intExecuteDays;            // --ִ������
            string strExecuteTime;   // --ִ��ʱ��	��: 08:00-14:00-��20:00
            int intID;
            string strID = "";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            strSQL = @"
                select 
                a.OrderID_Chr,a.IsRecruit_Int,a.ExecuteTime_Int,a.ExecuteDays_Int,a.ExecuteDate_VChr,
                b.PatientID_Chr,b.RegisterID_Chr,b.ExecuteType_Int,b.ISREPARE_INT,b.RecipeNo_Int,b.OrderDicID_Chr,b.Dosage_Dec,b.DosageUnit_Chr,b.DoseTypeID_Chr,b.ExecFreqID_Chr,
                c.AreaID_Chr,
                d.ItemID_Chr,
                e.itemsrctype_int,
                b.recipeno2_int 
                from 
                T_Opr_Bih_OrderExecute a,
                T_opr_bih_order b,
                T_Opr_Bih_Register c,
                T_Bse_Bih_OrderDic d,
                t_bse_chargeitem e
                where
                a.orderid_chr=b.orderid_chr
                and
                b.registerid_chr=c.registerid_chr(+)
                and
                b.orderdicid_chr=d.orderdicid_chr(+)
                and
                d.itemid_chr=e.itemid_chr(+)
                and
                a.orderexecid_chr=?";
            objHRPSvc.CreateDatabaseParameter(1, out arrParams);
            arrParams[0].Value = strExecOrderID;
            lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                strOrderID = clsConverter.ToString(dtbResult.Rows[0]["OrderID_Chr"].ToString());
                strPatientID = clsConverter.ToString(dtbResult.Rows[0]["PatientID_Chr"].ToString());
                strRegisterID = clsConverter.ToString(dtbResult.Rows[0]["RegisterID_Chr"].ToString());
                strAreaID = clsConverter.ToString(dtbResult.Rows[0]["AreaID_Chr"].ToString());
                intRecruit = clsConverter.ToInt(dtbResult.Rows[0]["IsRecruit_Int"].ToString());
                intExecuteType = clsConverter.ToInt(dtbResult.Rows[0]["ExecuteType_Int"].ToString());

                //--����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                if (intExecuteType == 1 && intRecruit == 1)
                {
                    intExecuteType = 3;
                }
                intIsRepare = clsConverter.ToInt(dtbResult.Rows[0]["ISREPARE_INT"].ToString());
                /*
            if(����:{2=�Ʒ�-����ҩ;4=���Ʒ�-����ҩ}) then ����ҩ --ֻ������ʱҽ��
            */
                if (intExecuteType == 2 && (intIsRepare == 2 || intIsRepare == 4))
                {
                    return;
                }
                //--��ȡ����	���ڱ�����ʾ
                //intRecipeNo = clsConverter.ToInt(dtbResult.Rows[0]["RecipeNo_Int"].ToString());
                intRecipeNo = clsConverter.ToInt(dtbResult.Rows[0]["recipeno2_int"].ToString());
                // --��ȡ������ĿID
                strDicID = clsConverter.ToString(dtbResult.Rows[0]["OrderDicID_Chr"].ToString());
                //--��ȡ���շ���ĿID
                strMainItemID = clsConverter.ToString(dtbResult.Rows[0]["ItemID_Chr"].ToString());
                //--��ȡ�շ���Ŀ��Դ���� ������Դ��ȷ��ֵ��Χ,����1��ҩƷ��2�����ϱ�ȡ�
                intItemSrctype = clsConverter.ToInt(dtbResult.Rows[0]["itemsrctype_int"].ToString());
                if (intItemSrctype != 1)
                {
                    return;
                }
                //--��ȡһ�μ���  {if(����Ϊ��) then Ĭ��ȡ1��}
                dmlMainDosage = clsConverter.ToDecimal(dtbResult.Rows[0]["Dosage_Dec"].ToString());
                // --��ȡ������λ	{=this.get������Ŀ.get�շ���Ŀ.������λ}
                strMainDosageUnit = clsConverter.ToString(dtbResult.Rows[0]["DosageUnit_Chr"].ToString());
                //--��ȡ��ҩ��ʽid    {��ҩ��ʽ������,��ô���շ�}
                strDoseTypeID = clsConverter.ToString(dtbResult.Rows[0]["DoseTypeID_Chr"].ToString());
                // --��ȡִ��Ƶ��	{=this.getҽ��Ƶ��.name}
                strExecFreqID = clsConverter.ToString(dtbResult.Rows[0]["ExecFreqID_Chr"].ToString());
                //  --��ȡִ�д���
                intExecuteTimes = clsConverter.ToInt(dtbResult.Rows[0]["ExecuteTime_Int"].ToString());
                //   --��ȡִ������
                intExecuteDays = clsConverter.ToInt(dtbResult.Rows[0]["ExecuteDays_Int"].ToString());
                //--��ȡִ��ʱ��	��: 08:00-14:00-��20:00
                strExecuteTime = clsConverter.ToString(dtbResult.Rows[0]["ExecuteDate_VChr"].ToString());

                strSQL = @"
            select A.PChargeID_Chr,A.ChargeItemID_Chr,A.Amount_Dec,A.UnitPrice_Dec,A.Unit_VChr ,
                             B.IsRich_Int,B.DOSAGE_DEC,B.dosageunit_chr,B.itemsrctype_int,C.MedicineID_Chr,C.MedicineName_VChr
                        from T_Opr_Bih_PatientCharge A,
                             T_Bse_ChargeItem B,
                             T_Bse_Medicine C
                        where A.ChargeItemID_Chr=B.ItemID_Chr
                             and Trim(B.ItemSrcID_VChr)=Trim(C.MedicineID_Chr)
                             and B.ItemSrcType_Int=1 and Trim(A.OrderExecID_Chr)=?
            ";
                objHRPSvc.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = strExecOrderID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {

                    //--ȷ����if(�����շ���Ŀ����ҩƷ�շ���Ŀ) then this���ҩ;
                    //--��ȡ�շ���Ŀ��Դ���� ������Դ��ȷ��ֵ��Χ,����1��ҩƷ��2�����ϱ�ȡ�
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        intItemSrctype = clsConverter.ToInt(dtbResult.Rows[i]["itemsrctype_int"].ToString());
                        string ChargeItemID_Chr = clsConverter.ToString(dtbResult.Rows[i]["ChargeItemID_Chr"].ToString());
                        decimal DOSAGE_DEC = clsConverter.ToDecimal(dtbResult.Rows[i]["DOSAGE_DEC"].ToString());
                        string dosageunit_chr = clsConverter.ToString(dtbResult.Rows[i]["dosageunit_chr"].ToString());
                        string MedicineID_Chr = clsConverter.ToString(dtbResult.Rows[i]["MedicineID_Chr"].ToString());
                        string MedicineName_VChr = clsConverter.ToString(dtbResult.Rows[i]["MedicineName_VChr"].ToString());
                        int IsRich_Int = clsConverter.ToInt(dtbResult.Rows[i]["IsRich_Int"].ToString());
                        decimal UnitPrice_Dec = clsConverter.ToDecimal(dtbResult.Rows[i]["UnitPrice_Dec"].ToString());
                        string Unit_VChr = clsConverter.ToString(dtbResult.Rows[i]["Unit_VChr"].ToString());
                        decimal Amount_Dec = clsConverter.ToDecimal(dtbResult.Rows[i]["Amount_Dec"].ToString());
                        string PChargeID_Chr = clsConverter.ToString(dtbResult.Rows[i]["PChargeID_Chr"].ToString());

                        if (intItemSrctype == 1)
                        {
                            if (ChargeItemID_Chr.Trim().Equals(strMainItemID.Trim()))
                            {
                                dmlDosage = dmlMainDosage;
                                strDosageUnit = strMainDosageUnit;
                            }
                            else
                            {
                                dmlDosage = DOSAGE_DEC;
                                strDosageUnit = dosageunit_chr;
                            }
                            // --��ȡ������ҩ��ϸ����ˮ��
                            strSQL = " select lpad(SEQ_PutMedDetailID.Nextval,18,'0') strID   from dual ";
                            DataTable dtbResult2 = new DataTable();
                            objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
                            if (dtbResult2.Rows.Count > 0)
                            {
                                strID = clsConverter.ToString(dtbResult2.Rows[0]["strID"].ToString());
                            }
                            strSQL = @"
                        Insert into T_Bih_Opr_PutMedDetail
             (PutMedDetailID_Chr,AreaID_Chr,PaientID_Chr,RegisterID_Chr,
                        OrderID_Chr,OrderExecID_Chr,OrderExecType_Int,RecipeNo_Int,
                        Dosage_Dec,DosageUnit_VChr,ChargeItemID_Chr,MedID_Chr,MedName_VChr,IsRich_Int,
                        DoseTypeID_Chr,ExecFreqID_Chr,ExecTimes_Int,ExecDays_Int,ExecTime_VChr,
                        UnitPrice_Mny,Unit_VChr,Get_Dec,PChargeID_Chr,
                        Creator_Chr,Create_Dat,IsPut_Int,PutType_Int,PutMedReqID_Chr)
            values(?,?,?,?,
                        ?,?,?,?,
                        ?,?,?,?,?,?,
                        ?,?,?,?,?,
                        ?,?,?,?,
                        ?,sysdate,0,0,'')
                        ";
                            arrParams = null;
                            objHRPSvc.CreateDatabaseParameter(24, out arrParams);
                            int n = 0;
                            arrParams[n].Value = strID.Trim();
                            n++;
                            arrParams[n].Value = strAreaID.Trim();
                            n++;
                            arrParams[n].Value = strPatientID.Trim();
                            n++;
                            arrParams[n].Value = strRegisterID.Trim();
                            n++;
                            arrParams[n].Value = strOrderID.Trim();
                            n++;
                            arrParams[n].Value = strExecOrderID.Trim();
                            n++;
                            arrParams[n].Value = intExecuteType;
                            n++;
                            arrParams[n].Value = intRecipeNo;
                            n++;
                            arrParams[n].Value = dmlDosage;
                            n++;
                            arrParams[n].Value = strDosageUnit.Trim();
                            n++;
                            arrParams[n].Value = ChargeItemID_Chr.Trim();
                            n++;
                            arrParams[n].Value = MedicineID_Chr.Trim();
                            n++;
                            arrParams[n].Value = MedicineName_VChr.Trim();
                            n++;
                            arrParams[n].Value = IsRich_Int;
                            n++;
                            arrParams[n].Value = strDoseTypeID.Trim();
                            n++;
                            arrParams[n].Value = strExecFreqID.Trim();
                            n++;

                            arrParams[n].Value = intExecuteTimes;
                            n++;
                            arrParams[n].Value = intExecuteDays;
                            n++;
                            arrParams[n].Value = strExecuteTime.Trim();
                            n++;
                            arrParams[n].Value = UnitPrice_Dec;
                            n++;
                            arrParams[n].Value = Unit_VChr.Trim();
                            n++;
                            arrParams[n].Value = Amount_Dec;
                            n++;
                            arrParams[n].Value = PChargeID_Chr.Trim();
                            n++;
                            arrParams[n].Value = strExecutorID.Trim();
                            n++;

                            long lngRecEff = -1;
                            //�������Ӽ�¼
                            lngRes = 0;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, arrParams);

                        }
                    }
                }
            }
        }
        #endregion

        #region ͨ��ִ�е���¼����շ���Ŀ(�շ���Ŀ��Դ��T_OPR_BIH_ORDERCHARGEDEPT��)
        /// <summary>
        /// ͨ��ִ�е���¼����շ���Ŀ ҵ��˵���� 
        /// 1��if(���� & �Ʒ�) then �շ� else ���շѣ�
        /// 2��if(���� || �Դ�ҩ) then ���շ� else �շѣ�
        /// </summary>
        /// <param name="strExecOrderID">ִ�е�ID</param>
        /// <param name="strEmpName">ִ��������</param>
        /// <param name="strEmpID">ִ����ID</param>
        /// <param name="dtExecDate">ִ��ʱ��</param>
        [AutoComplete]
        private void ExecFromORDERCHARGEDEPT(string strExecOrderID, string strEmpName, string strEmpID, DateTime dtExecDate)
        {
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            long lngAff = 0;

            string strOrderID = "";        //--ҽ����ˮ��
            string strDicID = "";          //--������Ŀ��ˮ��
            string strOrderCateID = "";    // --ҽ������ID
            string strChargeID = "";       //--
            string strDefaultItemID = "";  //--���շ���ĿID
            int dmlDefaultAmount = 0;  //-һ������
            int intID = 0;               //--���˷�����ϸ��ˮ��
            // --����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
            int intIsRepare = 0;
            //--��ȡ���ñ�־{1=�Ա�;2=����;3=����ҩ;4=��Ժ��ҩ};����ҩ:�����Ա�ҩ
            int intRateType = 0;
            int intCreateType = 0;        //-- 3 ����(ҽ��)    1�Զ�(ҽ��)
            int intIsRich = 0;                  //--�շ���Ŀ�Ĺ��ر�־
            int intChargeItemStatus = 0;        //--����״̬��0-��ȷ�ϣ�1-���ᣩ
            decimal dmlAmount = 0;
            string strPatientID = "";      //--����ID
            string strRegisterID = "";     //--��Ժ�Ǽ�ID
            int intExecuteType = 0;             //--ҽ��ִ������{1=����;2=����;3=�����¿���}
            string strCalcCateID = "";          //--��ĿסԺ�������
            string strINvCateID = "";           //--��ĿסԺ��Ʊ���
            decimal dmlPrice = 0;          //--סԺ����(=��Ŀ�۸�/��װ��)
            int intRecruit = 0;                 //--�Ƿ񲹴�{1/0}
            string strDeptID = "";              //--����ID [����ID]
            int intTimes = 0;                   //--��λƵ��ִ�еĴ���
            int intDosageView = 0;              //--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1

            /** @add by xzf (05-10-20) */
            int intCreateChargeType = 0; //-- ������Ч��ʽ{1=ִ��ҽ��ʱ��Ч;2=ִ�п��ҽ���ҽ��ʱ��Ч}

            /* <<====================== */
            string ItemID_Chr = "", ItemName_Vchr = "", ItemIPUnit_Chr = "", CLACAREA_CHR = "", CREATEAREA_CHR = "";
            /* <<============================= */

            string strSQL = "";
            strSQL = @"select A.ItemID_Chr , A.QTY_INT ,A.type_int,B.ItemName_Vchr,B.ItemIPUnit_Chr
                            ,decode(IPCHARGEFLG_INT,1,Round(B.ItemPrice_Mny/B.PackQty_Dec,4),0,B.ItemPrice_Mny,Round(B.ItemPrice_Mny/B.PackQty_Dec,4)) ItemPriceA
                            ,B.ItemIPCalcType_Chr,B.ItemIpInvType_Chr,B.IsRich_Int,B.DOSAGE_DEC
                        from T_Aid_Bih_OrderDic_Charge A,T_Bse_ChargeItem B
                        where OrderDicID_Chr=? and A.ItemID_Chr = B.ItemID_Chr";

            strSQL = @"
                            select 
                            a.orderid_chr,a.orderdicid_chr,a.chargeitemid_chr,a.clacarea_chr,
                            a.createarea_chr,a.chargeitemname_chr,a.spec_vchr,a.unit_vchr,
                            a.amount_dec,a.unitprice_dec,a.creatorid_chr,a.creator_vchr,
                            a.createdate_dat,a.flag_int,

                            c.RateType_Int,c.patientid_chr,c.registerid_chr,c.ExecuteType_Int,

                            d.ItemIPCalcType_Chr,d.ItemIpInvType_Chr,c.isrich_int,c.IsRepare_Int,
                             f.dosageviewtype,f.CreateChargeType

                            from
                            T_OPR_BIH_ORDERCHARGEDEPT a,
                            T_Opr_Bih_OrderExecute b,
                            T_Opr_Bih_Order c,
                            T_Bse_ChargeItem d,
                            t_aid_bih_ordercate f
                            where 
                            b.orderid_chr=c.orderid_chr
                            and
                            b.orderid_chr=a.orderid_chr
                            and
                            a.chargeitemid_chr=d.itemid_chr
                            and
                            d.ordercateid_chr=f.ORDERCATEID_CHR
                            and
                            b.orderexecid_chr=?
                        ";
            objHRPSvc.CreateDatabaseParameter(1, out arrParams);
            arrParams[0].Value = strExecOrderID;
            lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtbResult.Rows.Count; i++)
                {

                    strPatientID = clsConverter.ToString(dtbResult.Rows[i]["patientid_chr"].ToString());
                    strRegisterID = clsConverter.ToString(dtbResult.Rows[i]["registerid_chr"].ToString());
                    strOrderID = clsConverter.ToString(dtbResult.Rows[i]["orderid_chr"].ToString());
                    intExecuteType = clsConverter.ToInt(dtbResult.Rows[i]["ExecuteType_Int"].ToString());
                    strCalcCateID = clsConverter.ToString(dtbResult.Rows[i]["ItemIPCalcType_Chr"].ToString());
                    strINvCateID = clsConverter.ToString(dtbResult.Rows[i]["ItemIpInvType_Chr"].ToString());
                    ItemID_Chr = clsConverter.ToString(dtbResult.Rows[i]["chargeitemid_chr"].ToString());
                    ItemName_Vchr = clsConverter.ToString(dtbResult.Rows[i]["chargeitemname_chr"].ToString());
                    ItemIPUnit_Chr = clsConverter.ToString(dtbResult.Rows[i]["unit_vchr"].ToString());
                    dmlPrice = clsConverter.ToDecimal(dtbResult.Rows[i]["unitprice_dec"].ToString());
                    dmlAmount = clsConverter.ToDecimal(dtbResult.Rows[i]["amount_dec"].ToString());
                    CLACAREA_CHR = clsConverter.ToString(dtbResult.Rows[i]["clacarea_chr"].ToString());
                    CREATEAREA_CHR = clsConverter.ToString(dtbResult.Rows[i]["createarea_chr"].ToString());
                    intIsRich = clsConverter.ToInt(dtbResult.Rows[i]["isrich_int"].ToString());
                    intIsRepare = clsConverter.ToInt(dtbResult.Rows[i]["IsRepare_Int"].ToString());
                    intRateType = clsConverter.ToInt(dtbResult.Rows[i]["RateType_Int"].ToString());
                    intDosageView = clsConverter.ToInt(dtbResult.Rows[0]["dosageviewtype"].ToString());

                    //intExecuteType--ҽ��ִ������{1=����;2=����;3=�����¿���}
                    //intRecruit--�Ƿ񲹴�{1/0}
                    if (intExecuteType == 1 && intRecruit == 1)
                    {
                        intExecuteType = 3;//if������ & ����)
                    }
                    // intIsRepare--����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                    if (intExecuteType == 2 && (intIsRepare == 3 || intIsRepare == 4))
                    {
                        return;
                    }
                    //intRateType--��ȡ���ñ�־{1=�Ա�;2=����;3=����ҩ;4=��Ժ��ҩ};����ҩ:�����Ա�ҩ
                    if (intRateType == 1)   // 20180404 || intRateType == 2)
                    {
                        return;
                    }
                    //--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1
                    if (intDosageView == 2)
                    {
                        dmlDefaultAmount = 1;
                    }

                    // --����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                    // --����¼������	{1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
                    if (intIsRepare == 1 || intIsRepare == 2)
                    {
                        intCreateType = 3;
                    }
                    else
                    {
                        intCreateType = 1;
                    }

                    if (intIsRich == 1 || intCreateChargeType == 2)
                    {
                        intChargeItemStatus = 0;
                    }
                    else
                    {
                        intChargeItemStatus = 1;
                    }

                    //    --���������ϸ��¼
                    strSQL = @" 
                            insert into T_Opr_Bih_PatientCharge(
                            PChargeID_Chr,    PatientID_Chr,  RegisterID_Chr,   Active_Dat,     OrderID_Chr,
                            OrderExecType_Int,OrderExecID_Chr,CalCCateID_Chr,   InvCateID_Chr,   ChargeItemID_Chr,
                            ChargeItemName_Chr,Unit_Vchr,      UnitPrice_Dec,   AMount_Dec,       DisCount_Dec,
                            IsMepay_Int,      CreateType_Int,  Creator_Chr,     Create_Dat,      Status_Int,
                            PStatus_Int,      ClacArea_Chr,    CreateArea_Chr,  ISRICH_INT
                            )
                            values(
                            lpad(SEQ_PCHARGEID.Nextval,18,'0'),
                            ?,?,?,?,
                            ?,?,?,?,?,
                            ?,?,?,?,?,
                            ?,?,?,sysdate,?,
                            ?,?,?,?
                            )";
                    int n = -1;
                    objHRPSvc.CreateDatabaseParameter(22, out arrParams);
                    //n++; arrParams[0].Value = strChargeID.Trim();//PChargeID_Chr
                    n++;
                    arrParams[n].Value = strPatientID.Trim();//PatientID_Chr
                    n++;
                    arrParams[n].Value = strRegisterID.Trim();//RegisterID_Chr
                    n++;
                    arrParams[n].Value = dtExecDate;//Active_Dat
                    n++;
                    arrParams[n].Value = strOrderID.Trim();//OrderID_Chr

                    n++;
                    arrParams[n].Value = intExecuteType;//OrderExecType_Int ҽ��ִ������{1=����;2=����;3=�����¿���}
                    n++;
                    arrParams[n].Value = strExecOrderID.Trim();//OrderExecID_Chr
                    n++;
                    arrParams[n].Value = strCalcCateID.Trim();//CalCCateID_Chr  ���ú������id {=�շ���Ŀ����.id}
                    n++;
                    arrParams[n].Value = strINvCateID.Trim();//InvCateID_Chr    ���÷�Ʊ���id {=�շ���Ŀ����.id}
                    n++;
                    arrParams[n].Value = ItemID_Chr.Trim();//ChargeItemID_Chr

                    n++;
                    arrParams[n].Value = ItemName_Vchr.Trim();//ChargeItemName_Chr
                    n++;
                    arrParams[n].Value = ItemIPUnit_Chr.Trim();//Unit_Vchr סԺ��λ{=�շ���Ŀ.סԺ��λ}
                    n++;
                    arrParams[n].Value = dmlPrice;//UnitPrice_Dec  סԺ����{=�շ���Ŀ.סԺ����}
                    n++;
                    arrParams[n].Value = dmlAmount;//AMount_Dec    ����
                    n++;
                    arrParams[n].Value = 1;//DisCount_Dec=1���ۿ۱���

                    n++;
                    arrParams[n].Value = 1;//IsMepay_Int=1�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
                    n++;
                    arrParams[n].Value = intCreateType;//CreateType_Int ¼������ {1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
                    n++;
                    arrParams[n].Value = strEmpID.Trim();//Creator_Chr
                    //Create_Dat
                    n++;
                    arrParams[n].Value = 1;// Status_Int


                    n++;
                    arrParams[n].Value = intChargeItemStatus;//PStatus_Int

                    n++;
                    arrParams[n].Value = CLACAREA_CHR.Trim();//ClacArea_Chr
                    n++;
                    arrParams[n].Value = CREATEAREA_CHR.Trim();//CreateArea_Chr
                    n++;
                    arrParams[n].Value = intIsRich;//ISRICH_INT
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
                }
            }


            objHRPSvc.Dispose();

        }

        #endregion

        #region ��ȡҽ��	���ݵ�ǰ���� --ҽ�����������ʹ��
        /// <summary>
        /// ��ȡҽ��  ���ݵ�ǰ���� --ҽ�����������ʹ��  
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_intState"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <param name="m_dtPatients"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeSum"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <returns></returns>         
        [AutoComplete]
        public long m_lngGetOrderByArea(string m_strAreaid_chr, int m_intState, out DataTable m_dtExecOrder, out DataTable m_dtPatients, out DataTable m_dtChargeList)
        {
            long lngRes = -1;

            m_dtPatients = new DataTable();
            m_dtChargeList = new DataTable();
            m_dtExecOrder = new DataTable();

            string strSql = @"select   a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr,
                                     a.executetype_int, a.recipeno_int, a.name_vchr, a.spec_vchr,
                                     a.execfreqid_chr, a.dosage_dec, a.execfreqname_chr, a.dosageunit_chr,
                                     a.get_dec, a.useunit_chr, a.getunit_chr, a.dosetypeid_chr,
                                     a.dosetypename_chr, a.startdate_dat, a.finishdate_dat,
                                     a.execdeptid_chr, a.execdeptname_chr, a.entrust_vchr, a.parentid_chr,
                                     a.status_int, a.creatorid_chr, a.creator_chr, a.createdate_dat,
                                     a.posterid_chr, a.poster_chr, a.postdate_dat, a.executorid_chr,
                                     a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                                     a.stopdate_dat, a.retractorid_chr, a.retractor_chr,
                                     a.retractdate_dat, a.isrich_int, a.ratetype_int, a.isrepare_int,
                                     a.use_dec, a.isneedfeel, a.outgetmeddays_int,
                                     a.assessoridforexec_chr, a.assessorforexec_chr,
                                     a.assessorforexec_dat, a.assessoridforstop_chr,
                                     a.assessorforstop_chr, a.assessorforstop_dat, a.backreason,
                                     a.sendbackid_chr, a.sendbacker_chr, a.sendback_dat, a.isyb_int,
                                     a.sampleid_vchr, a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                                     a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr,
                                     a.confirmer_vchr, a.confirm_dat, a.attachtimes_int, a.doctorid_chr,
                                     a.doctor_vchr, a.curareaid_chr, a.curbedid_chr, a.doctorgroupid_chr,
                                     a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                                     a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr,
                                     a.feel_int, a.charge_int, a.type_int, a.singleamount_dec,
                                     a.sourcetype_int, b.patientid_chr, c.lastname_vchr, c.registerid_chr,
                                     c.sex_chr, d.bedid_chr, d.code_chr, e.ordercateid_chr,
                                     h.sample_type_desc_vchr, j.partname
                                from t_opr_bih_order a,
                                     t_opr_bih_register b,
                                     t_opr_bih_registerdetail c,
                                     t_bse_bed d,
                                     t_bse_bih_orderdic e,
                                     t_aid_lis_sampletype h,
                                     ar_apply_partlist j
                               where a.registerid_chr = b.registerid_chr
                                 and b.registerid_chr = c.registerid_chr
                                 and b.bedid_chr = d.bedid_chr(+)
                                 and a.orderdicid_chr = e.orderdicid_chr(+)
                                 and a.sampleid_vchr = h.sample_type_id_chr(+)
                                 and a.partid_vchr = j.partid(+)
                                 and {0}
                                 and (b.pstatus_int <> 2 and b.pstatus_int <> 3)
                                 and b.status_int = 1
                                 and (   (b.areaid_chr = ? and a.sourcetype_int = 0)
                                      or (a.createareaid_chr = ? and a.sourcetype_int = 1)
                                     )
                            order by d.code_chr, a.registerid_chr, a.recipeno_int, a.orderid_chr asc";

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = m_strAreaid_chr.Trim();
                arrParams[1].Value = m_strAreaid_chr.Trim();
                string status = GetCommitCate(m_intState);
                strSql = string.Format(strSql, status);
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtExecOrder, arrParams);
                if ((lngRes > 0) && (m_dtExecOrder != null) && m_dtExecOrder.Rows.Count > 0)
                {
                    //���˱�
                    strSql = @"select distinct a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                                        a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                                        a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                                        b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                                        c.code_chr bedname, d.deptname_vchr areaname, a.limitrate_mny,
                                        e.flgname_vchr as state, mzdiagnose_vchr,
                                        f.paytypename_vchr paytypename_vchr, sysdate today,
                                        g.remarkname_vchr, a.des_vchr
                                   from t_opr_bih_register a,
                                        t_opr_bih_registerdetail b,
                                        t_bse_bed c,
                                        t_bse_deptdesc d,
                                        (select flg_int, flgname_vchr
                                           from t_sys_flg_table
                                          where tablename_vchr = 't_opr_bih_register'
                                            and columnname_vchr = 'PSTATUS_INT') e,
                                        (select paytypeid_chr, paytypename_vchr
                                           from t_bse_patientpaytype tf
                                          where tf.isusing_num = 1 and tf.payflag_dec <> 1) f,
                                        t_opr_bih_patspecremark g,
                                        t_opr_bih_order h
                                  where a.registerid_chr = h.registerid_chr
                                    and a.registerid_chr = b.registerid_chr(+)
                                    and a.registerid_chr = c.bihregisterid_chr(+)
                                    and a.areaid_chr = d.deptid_chr(+)
                                    and a.pstatus_int = e.flg_int(+)
                                    and a.paytypeid_chr = f.paytypeid_chr(+)
                                    and a.registerid_chr = g.registerid_chr(+)
                                    and a.status_int = 1
                                    and [status_int]
                                    and (   (a.areaid_chr = ? and h.sourcetype_int = 0)
                                         or (h.createareaid_chr = ? and h.sourcetype_int = 1)
                                        )
                                    and (b.pstatus_int <> 2 and b.pstatus_int <> 3)";
                    System.Data.IDataParameter[] arrParams2 = null;
                    HRPService.CreateDatabaseParameter(2, out arrParams2);
                    arrParams2[0].Value = m_strAreaid_chr.Trim();
                    arrParams2[1].Value = m_strAreaid_chr.Trim();

                    strSql = strSql.Replace("[status_int]", status.Replace("a.", "h."));

                    lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtPatients, arrParams2);

                    if (lngRes > 0)
                    {
                        //������ϸ��
                        strSql = @"select distinct c.seq_int, c.orderid_chr, c.orderdicid_chr,
                                            c.chargeitemid_chr, c.clacarea_chr, c.createarea_chr,
                                            c.flag_int, c.chargeitemname_chr, c.spec_vchr, c.unit_vchr,
                                            c.amount_dec, c.unitprice_dec, c.creatorid_chr,
                                            c.creator_vchr, c.createdate_dat, c.remark,
                                            c.insuracedesc_vchr, c.ratetype_int, c.poflag_int,
                                            c.continueusetype_int, c.singleamount_dec,
                                            c.continuefreqid_chr, c.newdiscount_dec, a.orderid_chr,
                                            a.registerid_chr, a.recipeno_int, d.deptname_vchr,
                                            f.itemsrctype_int, f.itemcode_vchr, f.itemspec_vchr,
                                            g.ipnoqtyflag_int,g.medicnetype_int
                                       from t_opr_bih_order a,
                                            t_opr_bih_register b,
                                            t_opr_bih_orderchargedept c,
                                            t_bse_deptdesc d,
                                            t_bse_chargeitem f,
                                            t_bse_medicine g
                                      where a.orderid_chr = c.orderid_chr
                                        and a.registerid_chr = b.registerid_chr
                                        and c.chargeitemid_chr = f.itemid_chr
                                        and f.itemsrcid_vchr = g.medicineid_chr(+)
                                        and c.clacarea_chr = d.deptid_chr(+)
                                        and (   (b.areaid_chr = ? and a.sourcetype_int = 0)
                                             or (a.createareaid_chr = ? and a.sourcetype_int = 1)
                                            )
                                        and (b.pstatus_int <> 2 and b.pstatus_int <> 3)
                                        and {0} 
                                   order by c.orderid_chr, c.seq_int";

                        System.Data.IDataParameter[] arrParams4 = null;
                        HRPService.CreateDatabaseParameter(2, out arrParams4);
                        arrParams4[0].Value = m_strAreaid_chr.Trim();
                        arrParams4[1].Value = m_strAreaid_chr.Trim();
                        strSql = string.Format(strSql, status);

                        lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtChargeList, arrParams4);
                    }
                }
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
        /// ��ȡҽ��  ���ݵ�ǰ���� --ҽ�����������ʹ�� 
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_intState"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <param name="m_dtPatients"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeSum"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <returns></returns>         
        [AutoComplete]
        public long m_lngGetOrderByArea(string m_strAreaid_chr, int m_intState, out DataTable m_dtExecOrder, out DataTable m_dtChargeList)
        {
            long lngRes = -1;
            string Sql = "";

            m_dtChargeList = new DataTable();
            m_dtExecOrder = new DataTable();

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                DataTable dt2 = new DataTable();

                #region 1.1
                Sql = @"select a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr,
                               a.executetype_int, a.recipeno_int, a.name_vchr, a.spec_vchr,
                               a.execfreqid_chr, a.dosage_dec, a.execfreqname_chr, a.dosageunit_chr,
                               a.get_dec, a.useunit_chr, a.getunit_chr, a.dosetypeid_chr,
                               a.dosetypename_chr, a.startdate_dat, a.finishdate_dat,
                               a.execdeptid_chr, a.execdeptname_chr, a.entrust_vchr, a.parentid_chr,
                               a.status_int, a.creatorid_chr, a.creator_chr, a.createdate_dat,
                               a.posterid_chr, a.poster_chr, a.postdate_dat, a.executorid_chr,
                               a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                               a.stopdate_dat, a.retractorid_chr, a.retractor_chr, a.retractdate_dat,
                               a.isrich_int, a.ratetype_int, a.isrepare_int, a.use_dec, a.isneedfeel,
                               a.outgetmeddays_int, a.assessoridforexec_chr, a.assessorforexec_chr,
                               a.assessorforexec_dat, a.assessoridforstop_chr, a.assessorforstop_chr,
                               a.assessorforstop_dat, a.backreason, a.sendbackid_chr,
                               a.sendbacker_chr, a.sendback_dat, a.isyb_int, a.sampleid_vchr,
                               a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                               a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr,
                               a.confirmer_vchr, a.confirm_dat, a.attachtimes_int, a.doctorid_chr,
                               a.doctor_vchr, a.curareaid_chr, a.curbedid_chr, a.doctorgroupid_chr,
                               a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                               a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr,
                               a.feel_int, a.charge_int, a.type_int, a.singleamount_dec, a.sourcetype_int, 
                               b.patientid_chr, c.lastname_vchr, c.registerid_chr, c.sex_chr,
                               d.bedid_chr, d.code_chr, e.ordercateid_chr, h.sample_type_desc_vchr,
                               j.partname, a.chargedoctorgroupid_chr,b.inpatientid_chr 
                          from t_opr_bih_order a,
                               t_opr_bih_register b,
                               t_opr_bih_registerdetail c,
                               t_bse_bed d,
                               t_bse_bih_orderdic e,
                               t_aid_lis_sampletype h,
                               ar_apply_partlist j
                         where a.registerid_chr = b.registerid_chr
                           and b.registerid_chr = c.registerid_chr
                           and b.bedid_chr = d.bedid_chr(+)
                           and a.orderdicid_chr = e.orderdicid_chr(+)
                           and a.sampleid_vchr = h.sample_type_id_chr(+)
                           and a.partid_vchr = j.partid(+)
                           and {0}
                           and a.sourcetype_int = 0
                           and a.curareaid_chr = ?
                           and b.pstatus_int in (0, 1, 4)
                           and b.status_int = 1
                           and b.areaid_chr = ?";

                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = m_strAreaid_chr.Trim();
                arrParams[1].Value = m_strAreaid_chr.Trim();

                string status = GetCommitCate(m_intState);
                Sql = string.Format(Sql, status);
                lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref m_dtExecOrder, arrParams);

                #endregion 1.1

                #region 1.2
                Sql = @"select a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr,
                               a.executetype_int, a.recipeno_int, a.name_vchr, a.spec_vchr,
                               a.execfreqid_chr, a.dosage_dec, a.execfreqname_chr, a.dosageunit_chr,
                               a.get_dec, a.useunit_chr, a.getunit_chr, a.dosetypeid_chr,
                               a.dosetypename_chr, a.startdate_dat, a.finishdate_dat,
                               a.execdeptid_chr, a.execdeptname_chr, a.entrust_vchr, a.parentid_chr,
                               a.status_int, a.creatorid_chr, a.creator_chr, a.createdate_dat,
                               a.posterid_chr, a.poster_chr, a.postdate_dat, a.executorid_chr,
                               a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                               a.stopdate_dat, a.retractorid_chr, a.retractor_chr, a.retractdate_dat,
                               a.isrich_int, a.ratetype_int, a.isrepare_int, a.use_dec, a.isneedfeel,
                               a.outgetmeddays_int, a.assessoridforexec_chr, a.assessorforexec_chr,
                               a.assessorforexec_dat, a.assessoridforstop_chr, a.assessorforstop_chr,
                               a.assessorforstop_dat, a.backreason, a.sendbackid_chr,
                               a.sendbacker_chr, a.sendback_dat, a.isyb_int, a.sampleid_vchr,
                               a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                               a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr,
                               a.confirmer_vchr, a.confirm_dat, a.attachtimes_int, a.doctorid_chr,
                               a.doctor_vchr, a.curareaid_chr, a.curbedid_chr, a.doctorgroupid_chr,
                               a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                               a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr,
                               a.feel_int, a.charge_int, a.type_int, a.singleamount_dec, a.sourcetype_int,
                               b.patientid_chr, c.lastname_vchr, c.registerid_chr, c.sex_chr,
                               d.bedid_chr, d.code_chr, e.ordercateid_chr, h.sample_type_desc_vchr,
                               j.partname, a.chargedoctorgroupid_chr,b.inpatientid_chr 
                          from t_opr_bih_order a,
                               t_opr_bih_register b,
                               t_opr_bih_registerdetail c,
                               t_bse_bed d,
                               t_bse_bih_orderdic e,
                               t_aid_lis_sampletype h,
                               ar_apply_partlist j
                         where a.registerid_chr = b.registerid_chr
                           and b.registerid_chr = c.registerid_chr
                           and b.bedid_chr = d.bedid_chr(+)
                           and a.orderdicid_chr = e.orderdicid_chr(+)
                           and a.sampleid_vchr = h.sample_type_id_chr(+)
                           and a.partid_vchr = j.partid(+)
                           and {0} 
                           and b.pstatus_int in (0, 1, 4)
                           and b.status_int = 1
                           and a.sourcetype_int = 1
                           and a.createareaid_chr = ? ";

                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr.Trim();

                Sql = string.Format(Sql, status);
                lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dt2, arrParams);
                if (m_dtExecOrder != null && dt2 != null && dt2.Rows.Count > 0)
                {
                    m_dtExecOrder.Merge(dt2);
                }
                m_dtExecOrder.AcceptChanges();

                #endregion 1.2

                if (m_dtExecOrder.Rows.Count > 0)
                {
                    #region 2.1
                    Sql = @"select  distinct c.seq_int, c.orderid_chr, c.orderdicid_chr,
                                    c.chargeitemid_chr, c.clacarea_chr, c.createarea_chr,
                                    c.flag_int, c.chargeitemname_chr, c.spec_vchr, c.unit_vchr,
                                    c.amount_dec, c.unitprice_dec, c.creatorid_chr,
                                    c.creator_vchr, c.createdate_dat, c.remark,
                                    c.insuracedesc_vchr, c.ratetype_int, c.poflag_int,
                                    c.continueusetype_int, c.singleamount_dec,
                                    c.continuefreqid_chr, c.newdiscount_dec, a.orderid_chr,
                                    a.registerid_chr, a.recipeno_int, d.deptname_vchr,
                                    f.itemsrcid_vchr, f.isrich_int, f.isselfpay_chr,
                                    f.itemipcalctype_chr, f.itemipinvtype_chr, f.itemsrctype_int,
                                    f.ipchargeflg_int, f.itemprice_mny, f.packqty_dec,
                                    f.itemspec_vchr, f.itemcode_vchr, g.ipnoqtyflag_int,
                                    g.medicinetypeid_chr, g.putmedtype_int, a.chargedoctorgroupid_chr,
                                    f.tradeprice_mny, p.birth_dat, f.ischildprice  
                               from t_opr_bih_order a,
                                    t_opr_bih_register b,
                                    t_opr_bih_orderchargedept c,
                                    t_bse_deptdesc d,
                                    t_bse_chargeitem f,
                                    t_bse_medicine g,
                                    t_bse_patient p
                              where a.orderid_chr = c.orderid_chr
                                and a.registerid_chr = b.registerid_chr
                                and c.chargeitemid_chr = f.itemid_chr
                                and f.itemsrcid_vchr = g.medicineid_chr(+)
                                and c.clacarea_chr = d.deptid_chr(+)
                                and b.patientid_chr = p.patientid_chr 
                                and {0}
                                and a.sourcetype_int = 0
                                and a.curareaid_chr=?
                                and b.pstatus_int in (0, 1, 4)
                                and b.status_int = 1
                                and b.areaid_chr = ? ";

                    HRPService.CreateDatabaseParameter(2, out arrParams);
                    arrParams[0].Value = m_strAreaid_chr.Trim();
                    arrParams[1].Value = m_strAreaid_chr.Trim();
                    Sql = string.Format(Sql, status);
                    lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref m_dtChargeList, arrParams);

                    #endregion 2.1

                    #region 2.2
                    Sql = @"  select distinct c.seq_int, c.orderid_chr, c.orderdicid_chr,
                                        c.chargeitemid_chr, c.clacarea_chr, c.createarea_chr,
                                        c.flag_int, c.chargeitemname_chr, c.spec_vchr, c.unit_vchr,
                                        c.amount_dec, c.unitprice_dec, c.creatorid_chr,
                                        c.creator_vchr, c.createdate_dat, c.remark,
                                        c.insuracedesc_vchr, c.ratetype_int, c.poflag_int,
                                        c.continueusetype_int, c.singleamount_dec,
                                        c.continuefreqid_chr, c.newdiscount_dec, a.orderid_chr,
                                        a.registerid_chr, a.recipeno_int, d.deptname_vchr,
                                        f.itemsrcid_vchr, f.isrich_int, f.isselfpay_chr,
                                        f.itemipcalctype_chr, f.itemipinvtype_chr, f.itemsrctype_int,
                                        f.ipchargeflg_int, f.itemprice_mny, f.packqty_dec,
                                        f.itemspec_vchr, f.itemcode_vchr, g.ipnoqtyflag_int,
                                        g.medicinetypeid_chr, g.putmedtype_int, a.chargedoctorgroupid_chr,
                                        f.tradeprice_mny, p.birth_dat, f.ischildprice  
                                   from t_opr_bih_order a,
                                        t_opr_bih_register b,
                                        t_opr_bih_orderchargedept c,
                                        t_bse_deptdesc d,
                                        t_bse_chargeitem f,
                                        t_bse_medicine g,
                                        t_bse_patient p 
                                  where a.orderid_chr = c.orderid_chr
                                    and a.registerid_chr = b.registerid_chr
                                    and c.chargeitemid_chr = f.itemid_chr
                                    and f.itemsrcid_vchr = g.medicineid_chr(+)
                                    and c.clacarea_chr = d.deptid_chr(+)
                                    and b.patientid_chr = p.patientid_chr
                                    and {0} 
                                    and b.pstatus_int in (0, 1, 4)
                                    and b.status_int = 1
                                    and a.sourcetype_int = 1
                                    and a.createareaid_chr = ? ";

                    HRPService.CreateDatabaseParameter(1, out arrParams);
                    arrParams[0].Value = m_strAreaid_chr.Trim();
                    Sql = string.Format(Sql, status);
                    lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dt2, arrParams);
                    if (m_dtChargeList != null && dt2 != null && dt2.Rows.Count > 0)
                        m_dtChargeList.Merge(dt2);
                    m_dtChargeList.AcceptChanges();

                    if (m_dtChargeList != null && m_dtChargeList.Rows.Count > 0)
                    {
                        if (this.IsUseChildPrice())
                        {
                            clsBrithdayToAge calc = new clsBrithdayToAge();
                            foreach (DataRow dr in m_dtChargeList.Rows)
                            {
                                if (dr["ischildprice"] != DBNull.Value && Convert.ToInt32(dr["ischildprice"]) == 1)
                                {
                                    if (calc.IsChild(Convert.ToDateTime(dr["birth_dat"])))
                                    {
                                        dr["itemprice_mny"] = Convert.ToDecimal(dr["itemprice_mny"]) * EntityChildPrice.AddScale;
                                    }
                                }
                            }
                        }
                    }

                    #endregion 2.2
                }
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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="registerid_chr"></param>
        /// <param name="m_dtPatients"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoVo(string registerid_chr, out DataTable m_dtPatients)
        {
            long lngRes = -1;

            m_dtPatients = new DataTable();
            try
            {
                //���˱�
                string strSql = @"select distinct a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                                                a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                                                a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                                                b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                                                c.code_chr bedname, d.deptname_vchr areaname, a.limitrate_mny,
                                                e.flgname_vchr as state, mzdiagnose_vchr,
                                                f.paytypename_vchr paytypename_vchr, sysdate today,
                                                g.remarkname_vchr, a.des_vchr
                                           from t_opr_bih_register a,
                                                t_opr_bih_registerdetail b,
                                                t_bse_bed c,
                                                t_bse_deptdesc d,
                                                (select flg_int, flgname_vchr
                                                   from t_sys_flg_table
                                                  where tablename_vchr = 't_opr_bih_register'
                                                    and columnname_vchr = 'PSTATUS_INT') e,
                                                (select paytypeid_chr, paytypename_vchr
                                                   from t_bse_patientpaytype tf
                                                  where tf.isusing_num = 1 and tf.payflag_dec <> 1) f,
                                                t_opr_bih_patspecremark g
                                          where a.registerid_chr = b.registerid_chr(+)
                                            and a.registerid_chr = c.bihregisterid_chr(+)
                                            and a.areaid_chr = d.deptid_chr(+)
                                            and a.pstatus_int = e.flg_int(+)
                                            and a.paytypeid_chr = f.paytypeid_chr(+)
                                            and a.registerid_chr = g.registerid_chr(+)
                                            and a.status_int = 1
                                            and a.registerid_chr = ?";

                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams2 = null;
                HRPService.CreateDatabaseParameter(1, out arrParams2);
                arrParams2[0].Value = registerid_chr;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtPatients, arrParams2);

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
        /// �ύ��״̬����
        /// </summary>
        /// <param name="m_intState"></param>
        /// <returns></returns>
        private string GetCommitCate(int m_intState)
        {
            string status = string.Empty;
            switch (m_intState)
            {
                case 0:
                    status = @" (  a.status_int = 1
                                            or a.status_int = 5
                                            or (    a.executetype_int = 1
                                                and ((   a.status_int = 3
                                                      or (    a.status_int = 2
                                                          and trunc (a.finishdate_dat) > trunc (sysdate)
                                                         )
                                                     )
                                                    )
                                               )
                                            or (a.executetype_int = 1 and a.status_int = 6)
                                           )";

                    break;
                case 1://δ���
                    status = @" (  (a.status_int = 1)
                                            or (    a.executetype_int = 1
                                                and ((   a.status_int = 3
                                                      or (    a.status_int = 2
                                                          and a.finishdate_dat > a.startdate_dat
                                                          and trunc (a.finishdate_dat) < trunc (sysdate)
                                                         )
                                                     )
                                                    )
                                               )
                                           )";

                    break;
                case 2://�����
                    status = @" (   (a.status_int = 5)
                                            or (    a.executetype_int = 1
                                                and a.status_int = 6
                                                and trunc (a.assessorforstop_dat) = trunc (sysdate)
                                               )
                                           )";
                    break;
            }
            return status;
        }
        #endregion

        #region ��ȡ��ǰҽ��	���ݵ�ǰ���� --ҽ�����������ʹ�á����ܣ�����Ƿ�����µ�ҽ������
        /// <summary>
        /// ��ȡ��ǰҽ��	���ݵ�ǰ���� --ҽ�����������ʹ�á����ܣ�����Ƿ�����µ�ҽ������ 
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_intState"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <returns></returns>         
        [AutoComplete]
        public long m_lngCheckTheChanged(string m_strAreaid_chr, int m_intState, out DataTable m_dtExecOrder)
        {
            long lngRes = -1;

            m_dtExecOrder = new DataTable();

            string strSql = @"	
            select 
            a.* ,
            b.patientid_chr,
            c.LASTNAME_VCHR,
            c.registerid_chr,
            d.bedid_chr,
            d.code_chr,
            f.ordercateid_chr,
            f.viewname_vchr,
            h.sample_type_desc_vchr,
            j.partname
            from
            t_opr_bih_order  a,
            t_opr_bih_register b,
            t_opr_bih_registerdetail c ,
            T_BSE_Bed d,
            t_bse_bih_orderdic e,
            t_aid_bih_ordercate f,
            t_aid_lis_sampletype h,
            ar_apply_partlist j
           
            where 
            a.registerid_chr=b.registerid_chr
            and
            b.registerid_chr=c.registerid_chr
            and
            a.CURBEDID_CHR=d.bedid_chr(+)
            and
            a.orderdicid_chr=e.orderdicid_chr(+)
            and
            e.ordercateid_chr=f.ordercateid_chr(+)
            and
            a.sampleid_vchr=h.sample_type_id_chr(+)
            and
            a.partid_vchr=j.partid(+)
            and 
            a.status_int in ([status_int])
            and
            b.pstatus_int!=3
            and 
            a.CREATEAREAID_CHR='[areaid_chr]'
            order by d.code_chr,a.recipeno_int,a.PARENTID_CHR desc
    ";
            /*<====================================================================*/
            string m_strStatus_int = "";
            switch (m_intState)
            {
                case 0:
                    m_strStatus_int = "1,5";
                    break;
                case 1:
                    m_strStatus_int = "1";
                    break;
                case 2:
                    m_strStatus_int = "5";
                    break;
            }
            strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
            strSql = strSql.Replace("[status_int]", m_strStatus_int);
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                lngRes = 0;
                lngRes = HRPService.DoGetDataTable(strSql, ref m_dtExecOrder);
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

        #region �������޸��շ���Ŀʱ�����ύ�շ�����
        /// <summary>
        /// �������޸��շ���Ŀʱ�����ύ�շ�����
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_intState"></param>
        /// <param name="m_dtChargeList"></param>
        /// <returns></returns>         
        [AutoComplete]
        public long m_lngrefreshTheChargeDate(string m_strAreaid_chr, int m_intState, out DataTable m_dtChargeList)
        {
            #region old
            long lngRes = 0;
            m_dtChargeList = new DataTable();
            string status = GetCommitCate(m_intState);

            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                System.Data.IDataParameter[] parm = null;
                DataTable dtTemp = new DataTable();
                string Sql = @"select distinct c.seq_int,
                                            c.orderid_chr,
                                            c.orderdicid_chr,
                                            c.chargeitemid_chr,
                                            c.clacarea_chr,
                                            c.createarea_chr,
                                            c.flag_int,
                                            c.chargeitemname_chr,
                                            c.spec_vchr,
                                            c.unit_vchr,
                                            c.amount_dec,
                                            c.unitprice_dec,
                                            c.creatorid_chr,
                                            c.creator_vchr,
                                            c.createdate_dat,
                                            c.remark,
                                            c.insuracedesc_vchr,
                                            c.ratetype_int,
                                            c.poflag_int,
                                            c.continueusetype_int,
                                            c.singleamount_dec,
                                            c.continuefreqid_chr,
                                            c.newdiscount_dec,
                                            a.orderid_chr,
                                            a.registerid_chr,
                                            a.recipeno_int,
                                            d.deptname_vchr,
                                            f.itemsrcid_vchr,
                                            f.isrich_int,
                                            f.isselfpay_chr,
                                            f.itemipcalctype_chr,
                                            f.itemipinvtype_chr,
                                            f.itemsrctype_int,
                                            f.ipchargeflg_int,
                                            f.itemprice_mny,
                                            f.packqty_dec,
                                            f.itemspec_vchr,
                                            f.itemcode_vchr,
                                            g.ipnoqtyflag_int,
                                            g.medicnetype_int,
                                            g.putmedtype_int,
                                            k.patientcardid_chr,
                                            f.tradeprice_mny,
                                            g.medicinetypeid_chr,
                                            a.ratetype_int as medSource,
                                            p.birth_dat, f.ischildprice   
                              from t_opr_bih_order           a,
                                   t_opr_bih_register        b,
                                   t_opr_bih_orderchargedept c,
                                   t_bse_deptdesc            d,
                                   t_bse_chargeitem          f,
                                   t_bse_medicine            g,
                                   t_bse_patientcard         k,
                                   t_bse_patient p
                             where b.patientid_chr = k.patientid_chr(+)
                               and a.orderid_chr = c.orderid_chr
                               and a.registerid_chr = b.registerid_chr
                               and c.chargeitemid_chr = f.itemid_chr
                               and f.itemsrcid_vchr = g.medicineid_chr(+)
                               and c.clacarea_chr = d.deptid_chr(+)
                               and b.patientid_chr = p.patientid_chr
                               and ((b.areaid_chr = ? and a.sourcetype_int = 0))
                               and b.pstatus_int not in (2, 3)
                               and b.status_int = 1
                               and a.curareaid_chr = ?
                               and {0} ";

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = m_strAreaid_chr.Trim();
                parm[1].Value = m_strAreaid_chr.Trim();
                Sql = string.Format(Sql, status);
                lngRes = svc.lngGetDataTableWithParameters(Sql, ref dtTemp, parm);
                if (lngRes > 0 && dtTemp.Rows.Count > 0)
                {
                    m_dtChargeList.Merge(dtTemp);
                }

                Sql = @"select distinct c.seq_int,
                                        c.orderid_chr,
                                        c.orderdicid_chr,
                                        c.chargeitemid_chr,
                                        c.clacarea_chr,
                                        c.createarea_chr,
                                        c.flag_int,
                                        c.chargeitemname_chr,
                                        c.spec_vchr,
                                        c.unit_vchr,
                                        c.amount_dec,
                                        c.unitprice_dec,
                                        c.creatorid_chr,
                                        c.creator_vchr,
                                        c.createdate_dat,
                                        c.remark,
                                        c.insuracedesc_vchr,
                                        c.ratetype_int,
                                        c.poflag_int,
                                        c.continueusetype_int,
                                        c.singleamount_dec,
                                        c.continuefreqid_chr,
                                        c.newdiscount_dec,
                                        a.orderid_chr,
                                        a.registerid_chr,
                                        a.recipeno_int,
                                        d.deptname_vchr,
                                        f.itemsrcid_vchr,
                                        f.isrich_int,
                                        f.isselfpay_chr,
                                        f.itemipcalctype_chr,
                                        f.itemipinvtype_chr,
                                        f.itemsrctype_int,
                                        f.ipchargeflg_int,
                                        f.itemprice_mny,
                                        f.packqty_dec,
                                        f.itemspec_vchr,
                                        f.itemcode_vchr,
                                        g.ipnoqtyflag_int,
                                        g.medicnetype_int,
                                        g.putmedtype_int,
                                        k.patientcardid_chr,
                                        f.tradeprice_mny,
                                        a.ratetype_int as medSource,
                                        p.birth_dat, f.ischildprice   
                          from t_opr_bih_order           a,
                               t_opr_bih_register        b,
                               t_opr_bih_orderchargedept c,
                               t_bse_deptdesc            d,
                               t_bse_chargeitem          f,
                               t_bse_medicine            g,
                               t_bse_patientcard         k,
                               t_bse_patient p
                         where b.patientid_chr = k.patientid_chr(+)
                           and a.orderid_chr = c.orderid_chr
                           and a.registerid_chr = b.registerid_chr
                           and c.chargeitemid_chr = f.itemid_chr
                           and f.itemsrcid_vchr = g.medicineid_chr(+)
                           and c.clacarea_chr = d.deptid_chr(+)
                           and b.patientid_chr = p.patientid_chr
                           and ((a.createareaid_chr = ? and a.sourcetype_int = 1))
                           and b.pstatus_int not in (2, 3)
                           and b.status_int = 1
                           and a.curareaid_chr = ?
                           and {0} ";

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = m_strAreaid_chr.Trim();
                parm[1].Value = m_strAreaid_chr.Trim();
                Sql = string.Format(Sql, status);
                lngRes = svc.lngGetDataTableWithParameters(Sql, ref dtTemp, parm);
                if (lngRes > 0 && dtTemp.Rows.Count > 0)
                {
                    m_dtChargeList.Merge(dtTemp);
                }
                if (m_dtChargeList != null && m_dtChargeList.Rows.Count > 0)
                {
                    if (this.IsUseChildPrice())
                    {
                        clsBrithdayToAge calc = new clsBrithdayToAge();
                        foreach (DataRow dr in m_dtChargeList.Rows)
                        {
                            if (dr["ischildprice"] != DBNull.Value && Convert.ToInt32(dr["ischildprice"]) == 1)
                            {
                                if (calc.IsChild(Convert.ToDateTime(dr["birth_dat"])))
                                {
                                    dr["itemprice_mny"] = Convert.ToDecimal(dr["itemprice_mny"]) * EntityChildPrice.AddScale;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

            #endregion
        }
        #endregion

        #region ��Ժҽ�����¹�����
        /// <summary>
        /// ��Ժҽ�����¹�����
        /// </summary>
        /// <param name="m_strOrderID"></param>
        /// <param name="confirmerid"></param>
        /// <param name="confirmer"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateLeaveConfiemer(string m_strOrderID, string confirmerid, string confirmer)
        {
            long lngRes = -1;
            long lngAffter = -1;
            string strSQL = @"   update t_opr_bih_order a
                                       set a.status_int = 6,
                                           a.confirmerid_chr = ? ,
                                           a.confirmer_vchr = ? ,
                                           a.confirm_dat = sysdate
                                     where a.status_int = 6 and a.orderid_chr = ? ";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;

                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = confirmerid;
                param[1].Value = confirmer;
                param[2].Value = m_strOrderID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffter, param);
                objHRPSvc.Dispose();
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

        #region ����ύһ��ҽ��
        /// <summary>
        /// ����ύһ��ҽ��
        /// </summary>
        /// <param name="m_strORDERID_Arr">ҽ��ID(���)</param>
        /// <param name="CONFIRMERID_CHR">����ύ��ID</param>
        /// <param name="CONFIRMER_VCHR">����ύ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateBihOrderConfirmer(List<EntityConfirmOrder> lstOrder, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            int n = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                n = 0;
                string strSQL = @"update t_opr_bih_order a
                                       set a.status_int = 5,
                                           a.confirmerid_chr = ?,
                                           a.confirmer_vchr = ?,                            
                                           a.confirm_dat = sysdate,
                                           a.pretestdays = ? 
                                     where a.status_int = 1 and a.orderid_chr = ?";

                DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.Int32, DbType.String };
                object[][] objValues = new object[4][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[lstOrder.Count];//��ʼ��
                }

                for (int k1 = 0; k1 < lstOrder.Count; k1++)
                {
                    n = -1;
                    objValues[++n][k1] = CONFIRMERID_CHR.Trim();
                    objValues[++n][k1] = CONFIRMER_VCHR.Trim();
                    objValues[++n][k1] = lstOrder[k1].PretestDays;
                    objValues[++n][k1] = lstOrder[k1].OrderId;
                }
                if (lstOrder.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
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

        #region ���ֹͣһ��ҽ��
        /// <summary>
        /// ���ֹͣһ��ҽ��
        /// </summary>
        /// <param name="m_strORDERID_Arr">ҽ��ID(���)</param>
        /// <param name="CONFIRMERID_CHR">����ύ��ID</param>
        /// <param name="CONFIRMER_VCHR">����ύ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngStopBihOrderConfirmer(List<string> m_strORDERID_Arr, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            int n = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                n = 0;
                List<string> lstOrderId = new List<string>();
                string strSQL = @"update t_opr_bih_order a
                                       set a.status_int = 6,
                                           a.assessoridforstop_chr = ?,
                                           a.assessorforstop_chr = ?,
                                           a.assessorforstop_dat = sysdate
                                     where a.status_int <> 6 and a.orderid_chr = ?";

                DbType[] dbTypes = new DbType[] {
                        DbType.String,DbType.String,DbType.String

                        };
                object[][] objValues = new object[3][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_strORDERID_Arr.Count];//��ʼ��
                }
                for (int k1 = 0; k1 < m_strORDERID_Arr.Count; k1++)
                {
                    n = -1;
                    objValues[++n][k1] = CONFIRMERID_CHR.Trim();
                    objValues[++n][k1] = CONFIRMER_VCHR.Trim();
                    objValues[++n][k1] = m_strORDERID_Arr[k1].ToString();

                    lstOrderId.Add(m_strORDERID_Arr[k1].ToString());
                }
                if (m_strORDERID_Arr.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

                    // Ԥ��ҩ.ҽ��ͣ�������֮ǰ��ʿδִ�и������������������������Ԥ��ҩ������ʱ��ȡ���������Ԥ��ҩ�ķ��á�
                    GeneratePretestMedCharge(lstOrderId, CONFIRMERID_CHR);
                }
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

        #region Ԥ��ҩ.ҽ��ͣ�������֮ǰ��ʿδִ�и������������������������Ԥ��ҩ������ʱ��ȡ���������Ԥ��ҩ�ķ��á�
        /// <summary>
        /// Ԥ��ҩ.ҽ��ͣ�������֮ǰ��ʿδִ�и������������������������Ԥ��ҩ������ʱ��ȡ���������Ԥ��ҩ�ķ��á�
        /// </summary>
        /// <param name="lstOrderId"></param>
        /// <param name="confirmId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GeneratePretestMedCharge(List<string> lstOrderId, string confirmId)
        {
            long lngRes = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;
            try
            {
                DataTable dt = null;
                DateTime dtmNow = DateTime.Now;
                foreach (string orderId in lstOrderId)
                {
                    Sql = @"select a.orderid_chr,
                                   a.orderdicid_chr,
                                   a.name_vchr,
                                   b.orderexecid_chr,
                                   b.createdate_dat,
                                   c.putmeddetailid_chr
                              from t_opr_bih_order a
                             inner join t_opr_bih_orderexecute b
                                on a.orderid_chr = b.orderid_chr
                             inner join t_bih_opr_putmeddetail c
                                on a.orderid_chr = c.orderid_chr
                             where a.pretestdays > 0
                               and b.status_int = 1
                               and a.orderid_chr = ?
                             order by b.createdate_dat ";

                    svc.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = orderId;
                    svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[dt.Rows.Count - 1];
                        DateTime dtmExec = Convert.ToDateTime(dr["createdate_dat"].ToString());
                        if (Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 00:00:00") <= dtmExec && dtmExec <= Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 23:59:59"))
                        {
                            // ��ʿ��ִ��
                        }
                        else
                        {
                            dr = dt.Rows[0];

                            Sql = @"insert into t_opr_bih_patientcharge
                                      (pchargeid_chr,   patientid_chr,      registerid_chr,   chargeactive_dat,   orderid_chr,
                                       needconfirm_int,   orderexectype_int,   orderexecid_chr,   calccateid_chr,   invcateid_chr,
                                       chargeitemid_chr,   chargeitemname_chr,   unit_vchr,   unitprice_dec,   amount_dec,
                                       discount_dec,   ismepay_int,   createtype_int,   creator_chr,   create_dat,
                                       status_int,   pstatus_int,   clacarea_chr,   createarea_chr,   isrich_int,
                                       activatetype_int,   curareaid_chr,   curbedid_chr,   insuracedesc_vchr,   spec_vchr,
                                       doctorid_chr,   doctor_vchr,   doctorgroupid_chr,   patientnurse_int,   active_dat,
                                       activator_chr,   totalmoney_dec,   newdiscount_dec,   putmedicineflag_int,   chargedoctorid_chr,
                                       chargedoctor_vchr,   chargedoctorgroupid_chr,   itemchargetype_vchr,   totaldiffcostmoney_dec,   BuyPrice_dec, des_vchr )
                                    select lpad(seq_pchargeid.nextval, 18, '0'),  patientid_chr,  registerid_chr, sysdate,  orderid_chr,
                                       needconfirm_int,   9,   ?,   calccateid_chr,   invcateid_chr,
                                       chargeitemid_chr,   chargeitemname_chr,   unit_vchr,   unitprice_dec,   amount_dec,
                                       discount_dec,   ismepay_int,   createtype_int,   ?,   sysdate,
                                       1,   2,   clacarea_chr,   createarea_chr,   isrich_int,
                                       activatetype_int,   curareaid_chr,   curbedid_chr,   insuracedesc_vchr,   spec_vchr,
                                       doctorid_chr,   doctor_vchr,   doctorgroupid_chr,   patientnurse_int,   sysdate,
                                       ?,   totalmoney_dec,   newdiscount_dec,   putmedicineflag_int,   chargedoctorid_chr,
                                       chargedoctor_vchr,   chargedoctorgroupid_chr,   itemchargetype_vchr,   totaldiffcostmoney_dec,   BuyPrice_dec, 'Ԥ��ҩ����'   
                                    from t_opr_bih_patientcharge 
                                    where orderexecid_chr = ?";

                            svc.CreateDatabaseParameter(4, out parm);
                            parm[0].Value = dr["orderexecid_chr"].ToString();
                            parm[1].Value = confirmId;
                            parm[2].Value = confirmId;
                            parm[3].Value = dr["orderexecid_chr"].ToString();
                            svc.lngExecuteParameterSQL(Sql, ref lngRes, parm);

                            Sql = @"insert into t_opr_bih_orderdic
                                          (orderid_int,
                                           orderid_chr,
                                           type_int,
                                           orderque_int,
                                           orderdicid_chr,
                                           orderdicname_vchr,
                                           attachorderid_vchr,
                                           attachorderbasenum_dec)
                                        values
                                          (seq_recipeorderid.NEXTVAL, ?, ?, ?, ?, ?, ?, ?)";

                            svc.CreateDatabaseParameter(7, out parm);
                            parm[0].Value = orderId;
                            parm[1].Value = 1;
                            parm[2].Value = 1;
                            parm[3].Value = dr["orderdicid_chr"].ToString();
                            parm[4].Value = dr["name_vchr"].ToString();
                            parm[5].Value = "1->" + dr["orderdicid_chr"].ToString();
                            parm[6].Value = 1;
                            svc.lngExecuteParameterSQL(Sql, ref lngRes, parm);

                            Sql = @"update t_opr_bih_order set ispretestcharge = 1 where orderid_chr = ?";

                            svc.CreateDatabaseParameter(1, out parm);
                            parm[0].Value = orderId;
                            svc.lngExecuteParameterSQL(Sql, ref lngRes, parm);
                        }
                    }
                }
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

        #region �������һ��ҽ��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_strORDERID_Arr"></param>
        /// <param name="RETRACTORID_CHR"></param>
        /// <param name="RETRACTOR_CHR"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateBihOrderRedraw(List<string> m_strORDERID_Arr, string RETRACTORID_CHR, string RETRACTOR_CHR)
        {
            long lngRes = 0;
            //               com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //                System.Data.IDataParameter[] arrParams = null;
            //                string strSQL = "";

            //                strSQL = @"
            //                        update t_opr_bih_order a
            //                        set 
            //                        a.STATUS_INT=1
            //                        ,a.CONFIRMERID_CHR=null
            //                        ,a.CONFIRMER_VCHR=null
            //                        ,a.CONFIRM_DAT=null
            //                        ,a.RETRACTORID_CHR='[RETRACTORID_CHR]'
            //                        ,a.RETRACTOR_CHR='[RETRACTOR_CHR]'
            //                        ,a.RETRACTDATE_DAT=sysdate
            //                        where
            //                        a.STATUS_INT=5
            //                        AND
            //                        a.ORDERID_CHR in ([ORDERID_CHR])
            //                        ";
            //                strSQL = strSQL.Replace("[RETRACTORID_CHR]", RETRACTORID_CHR.Trim());
            //                strSQL = strSQL.Replace("[RETRACTOR_CHR]", RETRACTOR_CHR.Trim());
            //                strSQL = strSQL.Replace("[ORDERID_CHR]", m_strORDERID_Arr.Trim());

            int n = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                n = 0;
                string strSQL = @"  
                        update t_opr_bih_order a
                        set 
                        a.STATUS_INT=1
                        ,a.CONFIRMERID_CHR=null
                        ,a.CONFIRMER_VCHR=null
                        ,a.CONFIRM_DAT=null
                        ,a.RETRACTORID_CHR=?
                        ,a.RETRACTOR_CHR=?
                        ,a.RETRACTDATE_DAT=sysdate
                        where
                        a.STATUS_INT=5
                        AND
                        a.ORDERID_CHR=?";

                DbType[] dbTypes = new DbType[] {
                        DbType.String,DbType.String,DbType.String

                        };
                object[][] objValues = new object[3][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_strORDERID_Arr.Count];//��ʼ��
                }

                for (int k1 = 0; k1 < m_strORDERID_Arr.Count; k1++)
                {

                    n = -1;
                    objValues[++n][k1] = RETRACTORID_CHR.Trim();
                    objValues[++n][k1] = RETRACTOR_CHR.Trim();
                    objValues[++n][k1] = m_strORDERID_Arr[k1];


                }
                if (m_strORDERID_Arr.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                //lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region �˻�һ��ҽ��
        /// <summary>
        /// �˻�һ��ҽ��
        /// </summary>
        /// <param name="m_strORDERID_Arr">ҽ��ID(���)</param>
        /// <param name="m_strSENDBACKID_CHR">�˻���ID</param>
        /// <param name="m_strSENDBACKER_CHR">�˻���</param>
        /// <param name="m_strBACKREASON">�˻�ԭ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateBihOrderBack(string m_strORDERID_Arr, string SENDBACKID_CHR, string SENDBACKER_CHR, string m_strBACKREASON)
        {

            long lngAff = 0;
            long lngRes = 0;
            int n = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            string[] m_arrOrder = m_strORDERID_Arr.Split(",".ToCharArray());
            if (m_arrOrder.Length <= 0)
            {
                return lngRes;
            }
            try
            {
                strSQL = @"
                     update t_opr_bih_order a
                        set 
                        a.STATUS_INT=7
                        ,a.BACKREASON=?
                        ,a.SENDBACKID_CHR=?
                        ,a.SENDBACKER_CHR=?
                        ,a.CONFIRM_DAT=sysdate
                        where
                        a.STATUS_INT=1
                        AND
                        a.ORDERID_CHR =?
                        ";
                DbType[] dbTypes = new DbType[] {
                        DbType.String,DbType.String,DbType.String, DbType.String
                        };
                object[][] objValues = new object[4][];
                if (m_arrOrder.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[m_arrOrder.Length];//��ʼ��
                    }

                    for (int k1 = 0; k1 < m_arrOrder.Length; k1++)
                    {
                        n = -1;
                        objValues[++n][k1] = m_strBACKREASON.Trim();
                        objValues[++n][k1] = SENDBACKID_CHR.Trim();
                        objValues[++n][k1] = SENDBACKER_CHR.Trim();
                        objValues[++n][k1] = m_arrOrder[k1];

                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

                }
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

        //ҽ��ִ��
        #region ��ȡҽ��	���ݵ�ǰ���� --ҽ��ִ��������ʹ��
        /// <summary>
        /// ��ȡҽ��	���ݵ�ǰ���� --ҽ��ִ��������ʹ��  
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_intState"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <param name="m_dtPatients"></param>
        /// <param name="m_dtChargeList"></param>
        /// <returns></returns>         
        [AutoComplete]
        public long m_lngGetExecOrderByArea(string m_strAreaid_chr, out DataTable m_dtExecOrder, out DataTable m_dtPatients, out DataTable m_dtChargeList, out DataTable m_dtChargeSum, out DataTable m_dtChargeMoney)
        {
            long lngRes = -1;

            m_dtPatients = new DataTable();
            m_dtChargeList = new DataTable();
            m_dtExecOrder = new DataTable();
            m_dtChargeSum = new DataTable();
            m_dtChargeMoney = new DataTable();

            string strSql = @"	
            select 
            a.* ,
            b.patientid_chr,            
            c.LASTNAME_VCHR,
            c.registerid_chr,
            d.bedid_chr,
            d.code_chr,
            f.ordercateid_chr,
            f.viewname_vchr,
            sysdate today,
            g.Days_Int,
            decode(h.execCount,null,0,h.execCount) execCount,
            k.DEPTNAME_VCHR,
            m.sample_type_desc_vchr,
            n.partname, a.chargedoctorgroupid_chr 
            from
            t_opr_bih_order  a,
            t_opr_bih_register b,
            t_opr_bih_registerdetail c ,
            T_BSE_Bed d,
            t_bse_bih_orderdic e,
            t_aid_bih_ordercate f,
            T_Aid_RecipeFreq    g,
             (select 
            count(t.orderid_chr) execCount,t.orderid_chr
            from 
            T_Opr_Bih_OrderExecute t
            where
            trunc(t.createdate_dat)=trunc(sysdate)
            group by 
            t.orderid_chr) h,
            t_bse_deptdesc k,
            t_aid_lis_sampletype m,
            ar_apply_partlist n
            where 
            a.registerid_chr=b.registerid_chr
            and
            b.registerid_chr=c.registerid_chr
            and
            a.CURBEDID_CHR=d.bedid_chr(+)
            and
            a.orderdicid_chr=e.orderdicid_chr(+)
            and
            e.ordercateid_chr=f.ordercateid_chr(+)
            and
            a.ExecFreqID_Chr=g.FreqID_Chr(+)
            and
            a.orderid_chr=h.orderid_chr(+)
            and
            a.CURAREAID_CHR=k.DEPTID_CHR(+)
            and
            a.sampleid_vchr=m.sample_type_id_chr(+)
            and
            a.partid_vchr=n.partid(+)
            and 
            a.status_int in (2,5)
            and
            b.pstatus_int!=3
            and 
            a.CREATEAREAID_CHR='[areaid_chr]'
            order by d.code_chr,a.recipeno_int,a.PARENTID_CHR desc
    ";
            /*<====================================================================*/
            //string m_strStatus_int = "";
            //switch (m_intState)
            //{
            //    case 0:
            //        m_strStatus_int = "2,5";
            //        break;
            //    case 1:
            //        m_strStatus_int = "5";
            //        break;
            //    case 2:
            //        m_strStatus_int = "2";
            //        break;
            //}
            strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
            //strSql = strSql.Replace("[status_int]", m_strStatus_int);
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                lngRes = 0;
                lngRes = HRPService.DoGetDataTable(strSql, ref m_dtExecOrder);
                if ((lngRes > 0) && (m_dtExecOrder != null) && m_dtExecOrder.Rows.Count > 0)
                {
                    //���˱�
                    strSql = @"
                SELECT   a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                c.code_chr bedname, d.deptname_vchr areaname,
                a.limitrate_mny, e.flgname_vchr AS state, mzdiagnose_vchr,
                f.paytypename_vchr paytypename_vchr, SYSDATE today,
                g.remarkname_vchr, a.des_vchr
                FROM t_opr_bih_register a,
                t_opr_bih_registerdetail b,
                t_bse_bed c,
                t_bse_deptdesc d,
                (SELECT flg_int, flgname_vchr
                FROM t_sys_flg_table
                WHERE tablename_vchr = 't_opr_bih_register'
                AND columnname_vchr = 'PSTATUS_INT') e,
                (SELECT tf.*
                FROM t_bse_patientpaytype tf
                WHERE tf.isusing_num = 1 AND tf.payflag_dec != 1) f,
                t_opr_bih_patspecremark g,
                (select distinct a.registerid_chr from t_opr_bih_order a
                 where  a.status_int in (2,5)  and a.CREATEAREAID_CHR='[areaid_chr]'
                 ) h
                WHERE
                a.registerid_chr=h.registerid_chr
                and a.registerid_chr = b.registerid_chr(+)
                AND a.status_int = 1
               
                AND a.registerid_chr = c.bihregisterid_chr(+)
                AND a.areaid_chr = d.deptid_chr(+)
                AND a.pstatus_int = e.flg_int(+)
                AND a.paytypeid_chr = f.paytypeid_chr(+)
                AND a.registerid_chr = g.registerid_chr(+)
                and a.pstatus_int!=3
               -- and a.AREAID_CHR='[areaid_chr]'
               

                ";
                    strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
                    lngRes = HRPService.DoGetDataTable(strSql, ref m_dtPatients);
                    if (lngRes > 0)
                    {


                        strSql = @"
                    select k.registerid_chr,k1.VerticalMoney,  k2.clearmoney,k3.preusemoney,
                           k4.NotUsePreMoney,k5.WaitMoney,k6.WaitClearMoney
                    from 
                    (select distinct a.registerid_chr from t_opr_bih_order a
                    where  a.status_int in (2,5) and a.CREATEAREAID_CHR='[areaid_chr]'
                    ) k,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) VerticalMoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a
                    where a.pstatus_int = 4
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    group by a.registerid_chr
                    ) k1,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) clearmoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a
                    where a.pstatus_int = 3
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    group by a.registerid_chr
                    ) k2,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) preusemoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int!=0
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    group by a.registerid_chr
                    ) k3,
                    (SELECT sum(round(a.money_dec,2)) NotUsePreMoney,a.registerid_chr
                    FROM t_opr_bih_prepay a 
                    where a.isclear_int=0
                    group by a.registerid_chr
                    ) k4,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) WaitMoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int=1
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    group by a.registerid_chr
                    ) k5,
                   (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) WaitClearMoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int=2
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    group by a.registerid_chr
                    ) k6
                    where
                    k.registerid_chr=k1.registerid_chr(+)
                    and
                    k.registerid_chr=k2.registerid_chr(+)
                    and
                    k.registerid_chr=k3.registerid_chr(+)
                    and
                    k.registerid_chr=k4.registerid_chr(+)
                    and
                    k.registerid_chr=k5.registerid_chr(+)
                    and
                    k.registerid_chr=k6.registerid_chr(+)
                        ";
                        strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
                        lngRes = HRPService.DoGetDataTable(strSql, ref m_dtChargeMoney);

                    }
                    if (lngRes > 0)
                    {

                        //������ϸ��
                        strSql = @"
                        select 
                        c.* ,
                        a.orderid_chr,
                        d.deptname_vchr,
                        f.ITEMSRCTYPE_INT,
                        f.ISRICH_INT,
                        f.ISSELFPAY_CHR,
                        f.ItemIPCalcType_Chr,
                        f.ItemIpInvType_Chr,
                        g.IPNOQTYFLAG_INT,
                        from
                        t_opr_bih_order  a,
                        t_opr_bih_register b,

                        T_OPR_BIH_ORDERCHARGEDEPT c,
                        T_BSE_DeptDesc d,
                        t_bse_chargeitem f,
                        T_BSE_MEDICINE g,
                       (select distinct a.registerid_chr from t_opr_bih_order a
                         where  a.status_int in (2,5)  and a.CREATEAREAID_CHR='[areaid_chr]'
                       ) h
                        where 
                        a.registerid_chr=h.registerid_chr
                        and
                        a.orderid_chr=c.orderid_chr
                        and
                        a.registerid_chr=b.registerid_chr
                        and
                        c.CHARGEITEMID_CHR=f.ITEMID_CHR
                        and
                        f.ITEMSRCID_VCHR=g.medicineid_chr(+)
                        and
                        c.CLACAREA_CHR=d.deptid_chr(+)
                        and
                        a.CREATEAREAID_CHR='[areaid_chr]'
                        and
                        b.pstatus_int!=3
                        
                        order by c.orderid_chr,c.SEQ_INT  ";

                        strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
                        lngRes = HRPService.DoGetDataTable(strSql, ref m_dtChargeList);

                    }
                    //ҽ������ͳ��
                    strSql = @"
                        select 
                        sum(c.UNITPRICE_DEC*c.AMOUNT_DEC) chargeSum,c.orderid_chr
                       
                        from
                        t_opr_bih_order  a,
                        t_opr_bih_register b,
                        T_OPR_BIH_ORDERCHARGEDEPT c,   
                       (select distinct a.registerid_chr from t_opr_bih_order a
                         where  a.status_int in (2,5) and a.CREATEAREAID_CHR='[areaid_chr]'
                        ) h        
                        where 
                        a.registerid_chr=h.registerid_chr
                        and
                        a.orderid_chr=c.orderid_chr
                        and
                        a.registerid_chr=b.registerid_chr
                        and
                        a.CREATEAREAID_CHR='[areaid_chr]'
                        and
                        b.pstatus_int!=3
                        and
                        a.status_int in (2,5)
                        group by c.orderid_chr
                         ";
                    strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);

                    lngRes = HRPService.DoGetDataTable(strSql, ref m_dtChargeSum);


                }
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

        #region ��ȡҽ��	���ݵ�ǰ����������λ�� --ҽ��ִ��������ʹ��
        /// <summary>
        /// ��ȡҽ��	���ݵ�ǰ����������λ�� --ҽ��ִ��������ʹ�� 
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_strBedid_chr"></param>
        /// <param name="m_intState"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <param name="m_dtPatients"></param>
        /// <param name="m_dtChargeList"></param>
        /// <returns></returns>

        [AutoComplete]
        public long m_lngGetExecOrderByAreaBed(string m_strAreaid_chr, string m_strRegisterID, out DataTable m_dtExecOrder, out DataTable m_dtPatients, out DataTable m_dtChargeList, out DataTable m_dtChargeSum, out DataTable m_dtChargeMoney)
        {
            long lngRes = -1;

            m_dtPatients = new DataTable();
            m_dtChargeList = new DataTable();
            m_dtExecOrder = new DataTable();
            m_dtChargeSum = new DataTable();
            m_dtChargeMoney = new DataTable();

            string strSql = @"	

    select 
            a.* ,
            b.patientid_chr,            
            c.LASTNAME_VCHR,
            c.registerid_chr,
            d.bedid_chr,
            d.code_chr,
            f.ordercateid_chr,
            f.viewname_vchr,
            sysdate today,
            g.Days_Int,
            decode(h.execCount,null,0,h.execCount) execCount,
            k.DEPTNAME_VCHR,
            m.sample_type_desc_vchr,
            n.partname, a.chargedoctorgroupid_chr 
            from
            t_opr_bih_order  a,
            t_opr_bih_register b,
            t_opr_bih_registerdetail c ,
            T_BSE_Bed d,
            t_bse_bih_orderdic e,
            t_aid_bih_ordercate f,
            T_Aid_RecipeFreq    g,
             (select 
            count(t.orderid_chr) execCount,t.orderid_chr
            from 
            T_Opr_Bih_OrderExecute t
            where
            trunc(t.createdate_dat)=trunc(sysdate)
            group by 
            t.orderid_chr) h,
            t_bse_deptdesc k,
            t_aid_lis_sampletype m,
            ar_apply_partlist n
            where 
            a.registerid_chr=b.registerid_chr
            and
            b.registerid_chr=c.registerid_chr
            and
            a.CURBEDID_CHR=d.bedid_chr(+)
            and
            a.orderdicid_chr=e.orderdicid_chr(+)
            and
            e.ordercateid_chr=f.ordercateid_chr(+)
            and
            a.ExecFreqID_Chr=g.FreqID_Chr(+)
            and
            a.orderid_chr=h.orderid_chr(+)
            and
            a.CURAREAID_CHR=k.DEPTID_CHR(+)
            and
            a.sampleid_vchr=m.sample_type_id_chr(+)
            and
            a.partid_vchr=n.partid(+)
            and 
            a.status_int in (2,5)
            and
            b.pstatus_int!=3
            and 
            a.CREATEAREAID_CHR='[areaid_chr]'
            and 
            a.REGISTERID_CHR='[RegisterID]'
            order by d.code_chr,a.recipeno_int,a.PARENTID_CHR desc
    ";
            /*<====================================================================*/
            //string m_strStatus_int = "";
            //switch (m_intState)
            //{
            //    case 0:
            //        m_strStatus_int = "2,5";
            //        break;
            //    case 1:
            //        m_strStatus_int = "5";
            //        break;
            //    case 2:
            //        m_strStatus_int = "2";
            //        break;
            //}
            strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
            strSql = strSql.Replace("[RegisterID]", m_strRegisterID);
            //strSql = strSql.Replace("[status_int]", m_strStatus_int);
            //strSql = strSql.Replace("[bedid_chr]", m_strBedid_chr);
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                lngRes = 0;
                lngRes = HRPService.DoGetDataTable(strSql, ref m_dtExecOrder);
                if ((lngRes > 0) && (m_dtExecOrder != null) && m_dtExecOrder.Rows.Count > 0)
                {
                    //���˱�
                    strSql = @"
 SELECT         a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                c.code_chr bedname, d.deptname_vchr areaname,
                a.limitrate_mny, e.flgname_vchr AS state, mzdiagnose_vchr,
                f.paytypename_vchr paytypename_vchr, SYSDATE today,
                g.remarkname_vchr, a.des_vchr
                FROM t_opr_bih_register a,
                t_opr_bih_registerdetail b,
                t_bse_bed c,
                t_bse_deptdesc d,
                (SELECT flg_int, flgname_vchr
                FROM t_sys_flg_table
                WHERE tablename_vchr = 't_opr_bih_register'
                AND columnname_vchr = 'PSTATUS_INT') e,
                (SELECT tf.paytypeid_chr, tf.paytypename_vchr, tf.memo_vchr, tf.paylimit_mny, tf.payflag_dec, tf.paypercent_dec, tf.paytypeno_vchr, tf.isusing_num, tf.copayid_chr, tf.chargepercent_dec, tf.internalflag_int, tf.coalitionrecipeflag_int, tf.bihlimitrate_dec 
                FROM t_bse_patientpaytype tf
                WHERE tf.isusing_num = 1 AND tf.payflag_dec != 1) f,
                t_opr_bih_patspecremark g
                WHERE
                a.registerid_chr = b.registerid_chr(+)
                AND a.status_int = 1
               
                AND a.registerid_chr = c.bihregisterid_chr(+)
                AND a.areaid_chr = d.deptid_chr(+)
                AND a.pstatus_int = e.flg_int(+)
                AND a.paytypeid_chr = f.paytypeid_chr(+)
                AND a.registerid_chr = g.registerid_chr(+)
                and a.pstatus_int!=3
                and a.REGISTERID_CHR='[RegisterID]'
                ";
                    //strSql = strSql.Replace("[bedid_chr]", m_strBedid_chr);
                    strSql = strSql.Replace("[RegisterID]", m_strRegisterID);
                    lngRes = HRPService.DoGetDataTable(strSql, ref m_dtPatients);
                    if (lngRes > 0)
                    {
                        strSql = @"
                    select k.registerid_chr, k1.VerticalMoney,k2.clearmoney,k3.preusemoney,
                           k4.NotUsePreMoney,k5.WaitMoney,k6.WaitClearMoney
                    from 
                    (select distinct a.registerid_chr from t_opr_bih_order a
                    where  a.status_int in (2,5) and a.CREATEAREAID_CHR='[areaid_chr]'
                    ) k,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) VerticalMoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a
                    where a.pstatus_int = 4
                    group by a.registerid_chr
                    ) k1,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) clearmoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a
                    where a.pstatus_int = 3
                    group by a.registerid_chr
                    ) k2,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) preusemoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int!=0
                    group by a.registerid_chr
                    ) k3,
                    (SELECT sum(round(a.money_dec,2)) NotUsePreMoney,a.registerid_chr
                    FROM t_opr_bih_prepay a 
                    where a.isclear_int=0
                    group by a.registerid_chr
                    ) k4,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) WaitMoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int=1
                    group by a.registerid_chr
                    ) k5,
                   (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) WaitClearMoney,a.registerid_chr
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int=2
                    group by a.registerid_chr
                    ) k6
                    where
                    k.registerid_chr=k1.registerid_chr(+)
                    and
                    k.registerid_chr=k2.registerid_chr(+)
                    and
                    k.registerid_chr=k3.registerid_chr(+)
                    and
                    k.registerid_chr=k4.registerid_chr(+)
                    and
                    k.registerid_chr=k5.registerid_chr(+)
                    and
                    k.registerid_chr=k6.registerid_chr(+)
                        ";
                        strSql = @"
                    select k.registerid_chr, 
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) 
                    FROM t_opr_bih_patientcharge a
                    where a.pstatus_int = 3
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    and a.REGISTERID_CHR='[RegisterID]'
                    ) clearmoney,
                     (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) VerticalMoney
                     FROM t_opr_bih_patientcharge a
                    where a.pstatus_int = 4
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null              
                    and a.REGISTERID_CHR='[RegisterID]'
                    ) VerticalMoney,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2)) 
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int!=0
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    and a.REGISTERID_CHR='[RegisterID]'
                    ) preusemoney,
                    (SELECT sum(round(a.money_dec,2)) 
                    FROM t_opr_bih_prepay a 
                    where a.isclear_int=0
                    and a.REGISTERID_CHR='[RegisterID]'
                    ) NotUsePreMoney,
                    (SELECT sum(round(a.unitprice_dec*a.amount_dec,2))
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int=1
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    and a.REGISTERID_CHR='[RegisterID]'
                    ) WaitMoney,
                   (SELECT sum(round(a.unitprice_dec*a.amount_dec,2))
                    FROM t_opr_bih_patientcharge a 
                    where a.pstatus_int=2
                    and a.STATUS_INT=1 and a.chargeactive_dat is not null
                    and a.REGISTERID_CHR='[RegisterID]'
                    ) WaitClearMoney
                    from 
                    t_opr_bih_order k
                    where  k.status_int in (2,5) and k.REGISTERID_CHR='[RegisterID]' and rownum=1
                   
                        ";
                        //strSql = strSql.Replace("[bedid_chr]", m_strBedid_chr);
                        strSql = strSql.Replace("[RegisterID]", m_strRegisterID);
                        lngRes = HRPService.DoGetDataTable(strSql, ref m_dtChargeMoney);

                    }
                    if (lngRes > 0)
                    {
                        //������ϸ��
                        //������ϸ��
                        strSql = @"
                        select 
                         c.seq_int, c.orderid_chr, c.orderdicid_chr, c.chargeitemid_chr, c.clacarea_chr, c.createarea_chr, 
	c.flag_int, c.chargeitemname_chr, c.spec_vchr, c.unit_vchr, c.amount_dec, c.unitprice_dec, c.creatorid_chr, 
	c.creator_vchr, c.createdate_dat, c.remark, c.insuracedesc_vchr, c.ratetype_int, c.poflag_int, 
	c.continueusetype_int, c.singleamount_dec, c.newdiscount_dec, c.continuefreqid_chr, c.continuechargetype_int,c.itemchargetype_vchr,
                        a.orderid_chr,
                        d.deptname_vchr,
                        f.itemsrctype_int,
                        f.isrich_int,
                        f.isselfpay_chr,
                        f.itemipcalctype_chr,
                        f.itemipinvtype_chr,
                        g.ipnoqtyflag_int
                        from
                        t_opr_bih_order  a,
                        t_opr_bih_register b,

                        t_opr_bih_orderchargedept c,
                        t_bse_deptdesc d,
                        t_bse_chargeitem f,
                        t_bse_medicine g
                        where 
                        a.orderid_chr=c.orderid_chr
                        and
                        a.registerid_chr=b.registerid_chr
                        and
                        c.chargeitemid_chr=f.itemid_chr
                        and
                        f.itemsrcid_vchr=g.medicineid_chr(+)
                        and
                        c.clacarea_chr=d.deptid_chr(+)
                        and
                        a.registerid_chr='[RegisterID]'
                        and
                        b.pstatus_int!=3
                        
                        order by c.orderid_chr,c.seq_int  ";

                        //strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
                        //strSql = strSql.Replace("[bedid_chr]", m_strBedid_chr);
                        strSql = strSql.Replace("[RegisterID]", m_strRegisterID);
                        lngRes = HRPService.DoGetDataTable(strSql, ref m_dtChargeList);

                    }
                    //ҽ������ͳ��
                    strSql = @"
                        select 
                        sum(c.UNITPRICE_DEC*c.AMOUNT_DEC) chargeSum,c.orderid_chr
                       
                        from
                        t_opr_bih_order  a,
                        t_opr_bih_register b,
                        T_OPR_BIH_ORDERCHARGEDEPT c
                        where 
                       
                        a.orderid_chr=c.orderid_chr
                        and
                        a.registerid_chr=b.registerid_chr
                        and
                        b.pstatus_int!=3
                        and
                        a.status_int in (2,5)
                        and
                        a.REGISTERID_CHR='[RegisterID]'
                        group by c.orderid_chr
                         ";
                    //strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
                    //strSql = strSql.Replace("[bedid_chr]", m_strBedid_chr);
                    strSql = strSql.Replace("[RegisterID]", m_strRegisterID);
                    lngRes = HRPService.DoGetDataTable(strSql, ref m_dtChargeSum);


                }
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

        //        #region ���ִ��һ��ҽ��
        //        /// <summary>
        //        /// ����ύһ��ҽ��
        //        /// </summary>
        //        /// <param name="m_strORDERID_Arr">ҽ��ID(���)</param>
        //        /// <param name="CONFIRMERID_CHR">����ύ��ID</param>
        //        /// <param name="CONFIRMER_VCHR">����ύ������</param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngUpdateBihOrderExecConfirmer(ArrayList m_strORDERID_Arr, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        //        {
        //            int STATUS_INT = 0;//ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}
        //            int DAYS_INT = 0; //���� ������Ƶ�ʱ�
        //            int m_intTemp;
        //            int Times_Int = 0;//���� ������Ƶ�ʱ�
        //            string m_strOrderID_Chr = "";//ҽ��ID  
        //            string OrderID_Chr = "";//ҽ��ID
        //            string ExecTime = ""; //��ʱҽ��ִ��ʱ��   ������Ƶ�ʱ�     
        //            string m_strREGISTERID_CHR = "";//סԺ�Ǽ���ˮ��
        //            int ExecuteType_Int;//ִ������{1=����;2=��ʱ;3=��Ժ��ҩ} ��Դ��ҽ����
        //            DateTime StartDate_Dat;//"��ʼʱ��" ��Դ��ҽ����
        //            DateTime dtExecuteDate;//"����ִ��ʱ��"
        //            int intType = 1;   //������ʶ 1,�����״�ִ�� 2
        //            int intIsFirst = 1;//�״�ִ�б�ʶ
        //            int intIsRecruit; //ָ���Ƿ񲹴�(��ִ������)[1-���Σ�0-������]
        //            int intATTACHTIMES_INT; //���δ���
        //            int REMARK_INT = 0;//��ע״̬ 0-��Ч 1-��Ч
        //            int CHARGECTL_INT = 0;//��עǷ�ѿ��� 0-������ 1-����
        //            decimal m_dmlMedOCMin = 0;//Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        //            decimal m_dmlNoMedOCMin = 0;//Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        //            decimal m_dmlMedICMin = 0;//��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        //            decimal m_dmlNoMedICMin = 0;//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        //            decimal ChargeNoMedItemMoney = 0;//һ��ҽ�����շ�ҩƷ�շ���Ŀ���ܺ�              
        //            decimal ChargeMedItemMoney = 0;//һ��ҽ������ҩƷ�շ���Ŀ���ܺ�
        //            int m_intNEEDCONFIRM_INT = 0;//�Ƿ���Ҫ������� 0-�� 1-��  ҽ��ִ�е��ֶ�
        //            string CURAREAID_CHR = "";//�µ�ʱ�������ڲ���ID
        //            string CURBEDID_CHR = "";//�µ�ʱ�������ڲ���ID
        //            long lngAff = 0;
        //            long lngRes = 0;
        //            string strOrderID = "";
        //            string strExecOrderID = "";
        //            DataTable dtbResult = new DataTable();
        //            DataTable dtbResult2 = new DataTable();
        //            DataTable dtbResult3 = new DataTable();
        //            DataTable dtbMoney = new DataTable();//Ƿ�ѱ�
        //            DataTable dtbSysSet = new DataTable();//Ƿ�ѿ��Ʊ�
        //            string strShiying = string.Empty;//��Ӧ֢
        //            string strSQL = "";
        //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //            try
        //            {
        //                for (int k = 0; k < m_strORDERID_Arr.Count; k++)
        //                {
        //                    strSQL = @"
        //                      select 
        //                      sysdate dtExecuteDate,
        //                      a.orderid_chr,a.orderdicid_chr,a.registerid_chr,a.patientid_chr,a.executetype_int,a.recipeno_int,
        //       a.name_vchr,a.spec_vchr,a.execfreqid_chr,a.dosage_dec,a.execfreqname_chr,a.dosageunit_chr,
        //       a.get_dec,a.useunit_chr,a.getunit_chr,a.dosetypeid_chr,a.dosetypename_chr,a.startdate_dat,
        //       a.finishdate_dat,a.execdeptid_chr,a.execdeptname_chr,a.entrust_vchr,a.parentid_chr,a.status_int,
        //       a.creatorid_chr,a.creator_chr,a.createdate_dat,a.posterid_chr,a.poster_chr,a.postdate_dat,
        //       a.executorid_chr,a.executor_chr,a.executedate_dat,a.stoperid_chr,a.stoper_chr,a.stopdate_dat,
        //       a.retractorid_chr,a.retractor_chr,a.retractdate_dat,a.isrich_int,a.ratetype_int,a.isrepare_int,
        //       a.use_dec,a.isneedfeel,a.outgetmeddays_int,a.assessoridforexec_chr,a.assessorforexec_chr,
        //       a.assessorforexec_dat,a.assessoridforstop_chr,a.assessorforstop_chr,a.assessorforstop_dat,
        //       a.backreason,a.sendbackid_chr,a.sendbacker_chr,a.sendback_dat,a.isyb_int,a.sampleid_vchr,
        //       a.lisappid_vchr,a.partid_vchr,a.createareaid_chr,a.createareaname_vchr,a.ifparentid_int,a.confirmerid_chr,
        //       a.confirmer_vchr,a.confirm_dat,a.attachtimes_int,a.doctorid_chr,a.doctor_vchr,a.curareaid_chr,
        //       a.curbedid_chr,a.doctorgroupid_chr,a.deleterid_chr,a.deletername_vchr,a.delete_dat,a.sign_int,
        //       a.operation_int,a.remark_vchr,a.recipeno2_int,a.feelresult_vchr,a.feel_int,a.type_int,a.charge_int,
        //       a.singleamount_dec,a.sourcetype_int,a.chargedoctorgroupid_chr,a.itemchargetype_vchr,
        //                      b.days_int,b.times_int,b.texectime_vchr exectime,
        //                      c.status_int remark_int,c.chargectl_int
        //                      from t_opr_bih_order a,t_aid_recipefreq b,t_opr_bih_patspecremark c
        //                      where a.registerid_chr=c.registerid_chr(+) and a.execfreqid_chr=b.freqid_chr(+) 
        //                      and a.orderid_chr=? and rownum=1
        //                        ";

        //                    strOrderID = m_strORDERID_Arr[k].ToString();
        //                    System.Data.IDataParameter[] arrParams = null;
        //                    objHRPSvc.CreateDatabaseParameter(1, out arrParams);
        //                    arrParams[0].Value = strOrderID;

        //                    lngRes = 0;
        //                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
        //                    if (lngRes > 0 && dtbResult.Rows.Count > 0)
        //                    {

        //                        #region ��������Ƿ���Ҫִ��
        //                        /*
        //                                ��������Ƿ���Ҫִ�У�
        //                                1��if(����ʱҽ�� & �ǳ���ҽ������ִ��) then ��ִ��;
        //                                2��if(��ʱҽ�� & ִ��״̬Ϊ��5-���ִ�С�) then ִ�� else ��ִ��;
        //                                3��if(����ҽ�� & ִ��״̬Ϊ��5-���ִ�С�) then ִ��
        //                                else ��if(ִ��������ִ�й�) then ��ִ�� else ִ�У�;
        //                                ��ע��
        //                                i_cur.ExecuteType_Int [1=����;2=��ʱ]
        //                                i_cur.Status_Int      ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ���ִ��;6-���ֹͣ;}
        //                                */
        //                        //EXECUTETYPE_INT	NUMBER(1)	Y			ִ������{1=����;2=��ʱ;3=��Ժ��ҩ}

        //                        /*<---------------------------*/
        //                        STATUS_INT = clsConverter.ToInt(dtbResult.Rows[0]["STATUS_INT"].ToString());
        //                        DAYS_INT = clsConverter.ToInt(dtbResult.Rows[0]["DAYS_INT"].ToString());
        //                        Times_Int = clsConverter.ToInt(dtbResult.Rows[0]["Times_Int"].ToString());
        //                        ExecTime = clsConverter.ToString(dtbResult.Rows[0]["ExecTime"].ToString());
        //                        ExecuteType_Int = clsConverter.ToInt(dtbResult.Rows[0]["ExecuteType_Int"].ToString());

        //                        StartDate_Dat = clsConverter.ToDateTime(dtbResult.Rows[0]["STARTDATE_DAT"].ToString());
        //                        dtExecuteDate = clsConverter.ToDateTime(dtbResult.Rows[0]["dtExecuteDate"].ToString());
        //                        OrderID_Chr = clsConverter.ToString(dtbResult.Rows[0]["OrderID_Chr"].ToString());
        //                        intATTACHTIMES_INT = clsConverter.ToInt(dtbResult.Rows[0]["ATTACHTIMES_INT"].ToString());
        //                        m_strREGISTERID_CHR = clsConverter.ToString(dtbResult.Rows[0]["REGISTERID_CHR"].ToString());
        //                        REMARK_INT = clsConverter.ToInt(dtbResult.Rows[0]["REMARK_INT"].ToString());
        //                        CHARGECTL_INT = clsConverter.ToInt(dtbResult.Rows[0]["CHARGECTL_INT"].ToString());
        //                        CURAREAID_CHR = clsConverter.ToString(dtbResult.Rows[0]["CURAREAID_CHR"].ToString());//�µ�ʱ�������ڲ���ID
        //                        CURBEDID_CHR = clsConverter.ToString(dtbResult.Rows[0]["CURBEDID_CHR"].ToString());//�µ�ʱ�������ڲ���ID
        //                        if (ExecuteType_Int != 1 && ExecuteType_Int != 2 && ExecuteType_Int != 3)
        //                        {
        //                            strExecOrderID = "";
        //                            continue;
        //                        }

        //                        if (ExecuteType_Int == 3)
        //                        {
        //                            if (STATUS_INT != 5)
        //                            {
        //                                strExecOrderID = "";
        //                                continue;
        //                            }
        //                            else
        //                            {
        //                                intType = 4;   //��Ժ��ҩ
        //                                intIsFirst = 1;//�״�ִ��
        //                            }
        //                        }

        //                        if (ExecuteType_Int == 2)
        //                        {
        //                            if (STATUS_INT != 5)
        //                            {
        //                                strExecOrderID = "";
        //                                continue;
        //                            }
        //                            else
        //                            {
        //                                intType = 1;   //����
        //                                intIsFirst = 1;//�״�ִ��
        //                            }
        //                        }
        //                        if (ExecuteType_Int == 1)
        //                        {
        //                            if (STATUS_INT == 5)
        //                            {
        //                                intType = 2;//�����״�ִ��
        //                                intIsFirst = 1;//�״�ִ��
        //                            }
        //                            else if (STATUS_INT == 2)
        //                            {
        //                                if (DAYS_INT <= 0)
        //                                {
        //                                    m_intTemp = 1;
        //                                }
        //                                else
        //                                {
        //                                    m_intTemp = DAYS_INT;
        //                                }

        //                                TimeSpan m_tsDay = dtExecuteDate - StartDate_Dat;
        //                                if (m_tsDay.Days % m_intTemp == 0)
        //                                {
        //                                    intIsFirst = 0;//�����״�ִ��
        //                                    intType = 0;//�ǵ�һ��ִ��,���ø���ҽ�������ִ��״̬��
        //                                }
        //                                else
        //                                {
        //                                    strExecOrderID = "";
        //                                    continue;//��ִ��
        //                                }
        //                            }
        //                        }
        //                        #endregion
        //                        // ���ӵ���֤
        //                        // ������Ӧ��Ԥ����,���ý��״̬.
        //                        //--PreMoney Ԥ�����
        //                        //-- PreUseMoney ���ý��
        //                        //-- reMainMoney������=����Ԥ����(NotUsePreMoney)-�����(WaitMoney)-����� (WaitClearMoney)
        //                        //REMARK_INT = 0;//��ע״̬ 0-��Ч 1-��Ч ,CHARGECTL_INT��עǷ�ѿ��� 0-������ 1-����

        //                        strSQL = @"
        //                                select 
        //                                a.setid_chr,
        //                                a.setstatus_int 
        //                                from
        //                                t_sys_setting a
        //                                where
        //                                a.setid_chr='1018' 
        //                                or  
        //                                a.setid_chr='1019'
        //                                or
        //                                a.setid_chr='1020'
        //                                or 
        //                                a.setid_chr='1021'
        //                                ";
        //                        lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbSysSet);
        //                        if (lngRes > 0 && dtbSysSet.Rows.Count > 0)
        //                        {
        //                            for (int Sys = 0; Sys < dtbSysSet.Rows.Count; Sys++)
        //                            {
        //                                if (dtbSysSet.Rows[Sys]["setid_chr"].ToString().Trim().Equals("1018"))
        //                                {
        //                                    try
        //                                    {
        //                                        m_dmlMedOCMin = int.Parse(dtbSysSet.Rows[Sys]["setstatus_int"].ToString().Trim());//Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        //                                    }
        //                                    catch
        //                                    {
        //                                    }
        //                                }
        //                                if (dtbSysSet.Rows[Sys]["setid_chr"].ToString().Trim().Equals("1019"))
        //                                {
        //                                    try
        //                                    {
        //                                        m_dmlNoMedOCMin = int.Parse(dtbSysSet.Rows[Sys]["setstatus_int"].ToString().Trim());//Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        //                                    }
        //                                    catch
        //                                    {
        //                                    }
        //                                }
        //                                if (dtbSysSet.Rows[Sys]["setid_chr"].ToString().Trim().Equals("1020"))
        //                                {
        //                                    try
        //                                    {
        //                                        m_dmlMedICMin = int.Parse(dtbSysSet.Rows[Sys]["setstatus_int"].ToString().Trim());//��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        //                                    }
        //                                    catch
        //                                    {
        //                                    }
        //                                }
        //                                if (dtbSysSet.Rows[Sys]["setid_chr"].ToString().Trim().Equals("1021"))
        //                                {
        //                                    try
        //                                    {
        //                                        m_dmlNoMedICMin = int.Parse(dtbSysSet.Rows[Sys]["setstatus_int"].ToString().Trim());//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        //                                    }
        //                                    catch
        //                                    {
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        strSQL = @"
        //                                select * from 
        //                                (select sum(a.money_dec) PreMoney from t_opr_bih_prepay a where a.registerid_chr='[registerid]')a,
        //                                (select sum(a.money_dec) PreUseMoney from t_opr_bih_prepay a where a.registerid_chr='[registerid]' and a.isclear_int=1)b,
        //                                (select sum(a.money_dec) NotUsePreMoney from t_opr_bih_prepay a where a.registerid_chr='[registerid]' and a.isclear_int=0)c,
        //                                (select sum(a.unitprice_dec*a.amount_dec) WaitMoney  from t_opr_bih_patientcharge a where  a.registerid_chr='[registerid]' and a.pstatus_int=1) d,
        //                                (select sum(a.unitprice_dec*a.amount_dec) WaitClearMoney from t_opr_bih_patientcharge a where a.registerid_chr='[registerid]' and a.pstatus_int=2) e,
        //                                (select sum(a.AMOUNT_DEC*a.unitprice_dec) ChargeMedItemMoney from T_OPR_BIH_ORDERCHARGEDEPT a,t_bse_chargeitem b where a.CHARGEITEMID_CHR=b.ITEMID_CHR and a.ORDERID_CHR='[ORDERID_CHR]' and b.itemsrctype_int=1 and a.RATETYPE_INT=1) f,
        //                                (select sum(a.AMOUNT_DEC*a.unitprice_dec) ChargeNoMedItemMoney from T_OPR_BIH_ORDERCHARGEDEPT a,t_bse_chargeitem b where a.CHARGEITEMID_CHR=b.ITEMID_CHR and a.ORDERID_CHR='[ORDERID_CHR]' and b.itemsrctype_int!=1 and a.RATETYPE_INT=1) g
        //                                ";
        //                        strSQL = strSQL.Replace("[registerid]", m_strREGISTERID_CHR.Trim());
        //                        strSQL = strSQL.Replace("[ORDERID_CHR]", OrderID_Chr.Trim());

        //                        lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbMoney);
        //                        decimal reMainMoney = 0, PreMoney = 0, PreUseMoney = 0, NotUsePreMoney = 0, WaitMoney = 0, WaitClearMoney = 0;

        //                        if (dtbMoney.Rows.Count > 0)
        //                        {
        //                            if (!dtbMoney.Rows[0]["PreMoney"].ToString().Trim().Equals(""))
        //                            {
        //                                PreMoney = decimal.Parse(dtbMoney.Rows[0]["PreMoney"].ToString().Trim());
        //                            }
        //                            if (!dtbMoney.Rows[0]["PreUseMoney"].ToString().Trim().Equals(""))
        //                            {
        //                                PreUseMoney = decimal.Parse(dtbMoney.Rows[0]["PreUseMoney"].ToString().Trim());
        //                            }
        //                            if (!dtbMoney.Rows[0]["NotUsePreMoney"].ToString().Trim().Equals(""))
        //                            {
        //                                NotUsePreMoney = decimal.Parse(dtbMoney.Rows[0]["NotUsePreMoney"].ToString().Trim());
        //                            }
        //                            if (!dtbMoney.Rows[0]["WaitMoney"].ToString().Trim().Equals(""))
        //                            {
        //                                WaitMoney = decimal.Parse(dtbMoney.Rows[0]["WaitMoney"].ToString().Trim());
        //                            }
        //                            if (!dtbMoney.Rows[0]["WaitClearMoney"].ToString().Trim().Equals(""))
        //                            {
        //                                WaitClearMoney = decimal.Parse(dtbMoney.Rows[0]["WaitClearMoney"].ToString().Trim());
        //                            }
        //                            if (!dtbMoney.Rows[0]["ChargeNoMedItemMoney"].ToString().Trim().Equals(""))
        //                            {
        //                                ChargeNoMedItemMoney = decimal.Parse(dtbMoney.Rows[0]["ChargeNoMedItemMoney"].ToString().Trim());
        //                            }
        //                            if (!dtbMoney.Rows[0]["ChargeMedItemMoney"].ToString().Trim().Equals(""))
        //                            {
        //                                ChargeMedItemMoney = decimal.Parse(dtbMoney.Rows[0]["ChargeMedItemMoney"].ToString().Trim());
        //                            }

        //                            reMainMoney = NotUsePreMoney - WaitMoney - WaitClearMoney;//���ࣽ����Ԥ���𣭴��ᣭ����

        //                        }
        //                        /*<===============================================*/
        //                        /*
        //                        ����1018-1021ȷ�ϲ����ڵ�ǰ����ݺ���ע״̬�£��Ƿ���Ҫ������ˣ�
        //                        ������־д��(ҽ��ִ�е�)��NEEDCONFIRM_INT���ֶΡ������Ҫ������ˣ�
        //                        ����Ҫͨ��������ȷ�ϼ��ʽ�����ɲ�������Ϣ���������͵�����ҩ����ҩ��
        //                       ������Ҫ��˵�ҽ����������Ϣд��סԺ������ϸ��Ϣ��ʱ��PSTATUS_INT=0����־Ϊδȷ���շ���Ŀ�� 
        //                         */
        //                        // reMainMoney ûǷ��(>0)/Ƿ��(<0)

        //                        if (REMARK_INT == 1 && CHARGECTL_INT == 1)//��עǷ�ѿ��ƵĲ���
        //                        {
        //                            if (ChargeMedItemMoney > 0 && m_dmlMedOCMin != 0 && m_dmlMedOCMin + reMainMoney < ChargeMedItemMoney)//ҩƷ��Ŀ��Ǯ��>0&&Ƿ�Ѳ���ҩƷ��Ŀȷ����С�������<ҩƷ��Ŀ��Ǯ��
        //                                m_intNEEDCONFIRM_INT = 1;//Ҫ���
        //                            if (ChargeNoMedItemMoney > 0 && m_dmlNoMedOCMin != 0 && m_dmlNoMedOCMin + reMainMoney < ChargeNoMedItemMoney)//��ҩƷ��Ŀ��Ǯ��>0&&Ƿ�Ѳ���ҩƷ��Ŀȷ����С�������<��ҩƷ��Ŀ��Ǯ��
        //                                m_intNEEDCONFIRM_INT = 1;//Ҫ���
        //                        }
        //                        else//����עǷ�ѿ��ƵĲ���(��ͨ����)
        //                        {
        //                            if (ChargeMedItemMoney > 0 && m_dmlMedICMin != 0 && m_dmlMedICMin + reMainMoney < ChargeMedItemMoney)//ҩƷ��Ŀ��Ǯ��>0&&��ͨ����ҩƷ��Ŀȷ����С�������<ҩƷ��Ŀ��Ǯ��
        //                                m_intNEEDCONFIRM_INT = 1;//Ҫ���
        //                            if (ChargeNoMedItemMoney > 0 && m_dmlNoMedICMin != 0 && m_dmlNoMedICMin + reMainMoney < ChargeNoMedItemMoney)//��ҩƷ��Ŀ��Ǯ��>0&&��ͨ���˷�ҩƷ��Ŀȷ����С�������<��ҩƷ��Ŀ��Ǯ��
        //                                m_intNEEDCONFIRM_INT = 1;//Ҫ���
        //                        }

        //                        #region ��������һִ�е���¼���շ���ϸ��Ŀ
        //                        /*
        //                                ��������һ��ҽ��ִ�е���¼��
        //                                1�������µ�ҽ��ִ�е���ˮ�ţ�����λ����
        //                                2������ҽ��ִ�е���¼��
        //                                ҵ��������
        //                                if(����ҽ�� & �״�ִ�� & ����״̬)
        //                                then ��������ִ�е���¼[1-������2-����]
        //                                else ����һ��ִ�е�[����]
        //                                */


        //                        //--����һ��ҽ��ִ�е���¼��[1-����]
        //                        if (ExecuteType_Int == 1 && intType == 2 && intATTACHTIMES_INT >= 0)
        //                        {
        //                            for (int j = 0; j <= intATTACHTIMES_INT; j++)
        //                            {
        //                                #region ����ִ�е�
        //                                //--�����µ�ҽ��ִ�е���ˮ�ţ�����λ����
        //                                strSQL = "   select lpad(SEQ_ORDEREXECID.Nextval,18,'0') OrderExecID_Chr   from dual ";
        //                                dtbResult2 = new DataTable();
        //                                bool m_blUpdate = false;//����ҽ��ִ�е���־(true- ����Ϊ����Ҫ������ˣ�false-������;
        //                                objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
        //                                strExecOrderID = dtbResult2.Rows[0]["OrderExecID_Chr"].ToString();

        //                                strSQL = @"  insert into T_Opr_Bih_OrderExecute (OrderExecID_Chr,OrderID_Chr,CreatorID_Chr,Creator_Chr,CreateDate_Dat,
        //                                            ExecuteTime_Int,ExecuteDays_Int,ExecuteDate_VChr,IsCharge_Int,IsIncept_int,IsFirst_int,IsRecruit_int,Status_Int,NEEDCONFIRM_INT)
        //                                            values(?,?,?,?,?,?,?,?,?,?,?,?,?,?)
        //                                            ";
        //                                System.Data.IDataParameter[] arrParams2 = null;
        //                                objHRPSvc.CreateDatabaseParameter(14, out arrParams2);
        //                                int n = -1;
        //                                n++;
        //                                arrParams2[n].Value = strExecOrderID;//OrderExecID_Chr ��ˮ��
        //                                n++;
        //                                arrParams2[n].Value = OrderID_Chr;//OrderID_Chr ҽ����id	{=ҽ����.id}
        //                                n++;
        //                                arrParams2[n].Value = CONFIRMERID_CHR;//CreatorID_Chr ����ִ�е���{=��Ա.Id}
        //                                n++;
        //                                arrParams2[n].Value = CONFIRMER_VCHR;//Creator_Chr ����ִ�е���{=��Ա.name}
        //                                n++;
        //                                arrParams2[n].Value = dtExecuteDate;//CreateDate_Dat ����ִ�е�ʱ��  ȡ��ǰϵͳʱ��
        //                                n++;
        //                                arrParams2[n].Value = Times_Int;//ExecuteTime_Int  ִ�д���  Ƶ�ʱ�Ĵ���
        //                                n++;
        //                                arrParams2[n].Value = DAYS_INT;//ExecuteDays_Int   ִ������  ���� ������Ƶ�ʱ�
        //                                n++;
        //                                arrParams2[n].Value = ExecTime;//ExecuteDate_VChr  ִ��ʱ�� ��: 08:00-14:00-��20:00 ������Ƶ�ʱ�
        //                                n++;
        //                                arrParams2[n].Value = 0;//IsCharge_Int=0  �Ƿ��ѼƷ�{1-��/0-��}
        //                                n++;
        //                                arrParams2[n].Value = 0;//IsIncept_int=0   �Ƿ��ѷ���{1-��/0-��}
        //                                n++;
        //                                arrParams2[n].Value = intIsFirst;//IsFirst_int �Ƿ��״�ִ��{1-��/0-��}
        //                                if (j > 0)//�Ƿ񲹴�{1/0}
        //                                {
        //                                    n++;
        //                                    arrParams2[n].Value = 1;
        //                                }
        //                                else
        //                                {
        //                                    n++;
        //                                    arrParams2[n].Value = 0;
        //                                }
        //                                n++;
        //                                arrParams2[n].Value = 1;//Status_Int=1    STATUS_INT ��Ч��־	{1=��Ч;0=ɾ��;-1=��ʷ}
        //                                n++;
        //                                arrParams2[n].Value = m_intNEEDCONFIRM_INT;//�Ƿ���Ҫ������� 0-�� 1-��
        //                                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams2);
        //                                #endregion

        //                                #region �����շ���Ŀ��ϸ��
        //                                //�����շ���Ŀ��ϸ��
        //                                strSQL = @"
        //                                                select 
        //                                                a.orderid_chr,a.orderdicid_chr,a.chargeitemid_chr,a.clacarea_chr,
        //                                                a.createarea_chr,a.chargeitemname_chr,a.spec_vchr,a.unit_vchr,
        //                                                a.amount_dec,a.unitprice_dec,a.creatorid_chr,a.creator_vchr,
        //                                                a.createdate_dat,a.flag_int,
        //
        //                                                c.ratetype_int,c.patientid_chr,c.registerid_chr,c.executetype_int,
        //
        //                                                d.itemipcalctype_chr,d.itemipinvtype_chr,d.isrich_int,d.isselfpay_chr,c.isrepare_int,
        //                                                 f.dosageviewtype,f.createchargetype,a.itemchargetype_vchr
        //
        //                                                from
        //                                                t_opr_bih_orderchargedept a,
        //                                                t_opr_bih_orderexecute b,
        //                                                t_opr_bih_order c,
        //                                                t_bse_chargeitem d,
        //                                                t_aid_bih_ordercate f
        //                                                where 
        //                                                b.orderid_chr=c.orderid_chr
        //                                                and
        //                                                b.orderid_chr=a.orderid_chr
        //                                                and
        //                                                a.chargeitemid_chr=d.itemid_chr
        //                                                and
        //                                                d.ordercateid_chr=f.ordercateid_chr(+)
        //                                                and
        //                                                b.orderexecid_chr=?
        //                                            ";

        //                                System.Data.IDataParameter[] arrParams3 = null;
        //                                objHRPSvc.CreateDatabaseParameter(1, out arrParams3);
        //                                arrParams3[0].Value = strExecOrderID;
        //                                dtbResult3 = new DataTable();
        //                                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult3, arrParams3);
        //                                if (lngRes > 0 && dtbResult3.Rows.Count > 0)
        //                                {
        //                                    string strDicID = "";          //--������Ŀ��ˮ��
        //                                    string strOrderCateID = "";    // --ҽ������ID
        //                                    string strChargeID = "";       //--
        //                                    string strDefaultItemID = "";  //--���շ���ĿID

        //                                    int dmlDefaultAmount = 0;  //-һ������

        //                                    int intRateType = 0;//*���ñ�־	{1=ȫ�Ʒ�;2=�÷��Ʒ�;3=���Ʒ�}
        //                                    int intCreateType = 0;        //-- 3 ����(ҽ��)    1�Զ�(ҽ��)
        //                                    int intIsRich = 0;                  //*--�շ���Ŀ�Ĺ��ر�־
        //                                    int intChargeItemStatus = 0;        //*--����״̬��0-��ȷ�ϣ�1-���ᣩ
        //                                    int intRATETYPE_INT = 0;//�Ƿ�Ʒ� 0-���Ʒ� 1-�Ʒ�
        //                                    decimal dmlAmount = 0;//*
        //                                    string strPatientID = "";      //--����ID*
        //                                    string strRegisterID = "";     //--��Ժ�Ǽ�ID*
        //                                    int intExecuteType = 0;             //*--ҽ��ִ������{ִ������{1=����;2=��ʱ;3=��Ժ��ҩ}}
        //                                    int m_intOrderExecType_Int = 0;      //*--���벡�˷�����ϸ���ORDEREXECTYPE_INT��ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}��

        //                                    string strCalcCateID = "";          //*--��ĿסԺ�������
        //                                    string strINvCateID = "";           //*--��ĿסԺ��Ʊ���
        //                                    decimal dmlPrice = 0;          //*--סԺ����(=��Ŀ�۸�/��װ��)
        //                                    int intDosageView = 0;              //*--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1

        //                                    int intCreateChargeType = 0; //-- ������Ч��ʽ{1=ִ��ҽ��ʱ��Ч;2=ִ�п��ҽ���ҽ��ʱ��Ч}
        //                                    int ACTIVATETYPE_INT = 1;//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�}
        //                                    string ItemID_Chr = "";
        //                                    string ItemName_Vchr = "";  //�շ���Ŀ
        //                                    string ItemIPUnit_Chr = "";
        //                                    string CLACAREA_CHR = "";
        //                                    string CREATEAREA_CHR = "";
        //                                    string ISSELFPAY_CHR = "";//�Ƿ��Է���Ŀ("T","F")
        //                                    /* <<============================= */

        //                                    for (int m = 0; m < dtbResult3.Rows.Count; m++)
        //                                    {

        //                                        strPatientID = clsConverter.ToString(dtbResult3.Rows[m]["patientid_chr"].ToString());
        //                                        strRegisterID = clsConverter.ToString(dtbResult3.Rows[m]["registerid_chr"].ToString());
        //                                        strOrderID = clsConverter.ToString(dtbResult3.Rows[m]["orderid_chr"].ToString());
        //                                        intExecuteType = clsConverter.ToInt(dtbResult3.Rows[m]["ExecuteType_Int"].ToString());
        //                                        strCalcCateID = clsConverter.ToString(dtbResult3.Rows[m]["ItemIPCalcType_Chr"].ToString());
        //                                        strINvCateID = clsConverter.ToString(dtbResult3.Rows[m]["ItemIpInvType_Chr"].ToString());
        //                                        ItemID_Chr = clsConverter.ToString(dtbResult3.Rows[m]["chargeitemid_chr"].ToString());
        //                                        ItemName_Vchr = clsConverter.ToString(dtbResult3.Rows[m]["chargeitemname_chr"].ToString());
        //                                        ItemIPUnit_Chr = clsConverter.ToString(dtbResult3.Rows[m]["unit_vchr"].ToString());
        //                                        dmlPrice = clsConverter.ToDecimal(dtbResult3.Rows[m]["unitprice_dec"].ToString());
        //                                        dmlAmount = clsConverter.ToDecimal(dtbResult3.Rows[m]["amount_dec"].ToString());
        //                                        CLACAREA_CHR = clsConverter.ToString(dtbResult3.Rows[m]["clacarea_chr"].ToString());
        //                                        CREATEAREA_CHR = clsConverter.ToString(dtbResult3.Rows[m]["createarea_chr"].ToString());
        //                                        intIsRich = clsConverter.ToInt(dtbResult3.Rows[m]["isrich_int"].ToString());
        //                                        //intIsRepare = clsConverter.ToInt(dtbResult3.Rows[i]["IsRepare_Int"].ToString());
        //                                        intRateType = clsConverter.ToInt(dtbResult3.Rows[m]["RateType_Int"].ToString());
        //                                        intDosageView = clsConverter.ToInt(dtbResult3.Rows[m]["dosageviewtype"].ToString());
        //                                        intRATETYPE_INT = clsConverter.ToInt(dtbResult3.Rows[m]["RATETYPE_INT"].ToString());
        //                                        ISSELFPAY_CHR = clsConverter.ToString(dtbResult3.Rows[m]["ISSELFPAY_CHR"].ToString().Trim());
        //                                        strShiying = clsConverter.ToString(dtbResult3.Rows[m]["itemchargetype_vchr"].ToString().Trim());
        //                                        // intCreateChargeType = clsConverter.ToInt(dtbResult.Rows[0]["CreateChargeType"].ToString());

        //                                        //intRateType--��ȡ�ѷ��ñ�־	{1=ȫ�Ʒ�;2=�÷��Ʒ�;3=���Ʒ�}
        //                                        //if (intRateType == 1 || intRateType == 2)
        //                                        //{
        //                                        //    return;
        //                                        //}
        //                                        if (j > 0)
        //                                        {
        //                                            m_intOrderExecType_Int = 3;//*--���벡�˷�����ϸ���ORDEREXECTYPE_INT��ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}��
        //                                        }
        //                                        else
        //                                        {
        //                                            m_intOrderExecType_Int = 1;
        //                                        }
        //                                        //--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1
        //                                        if (intDosageView == 2)
        //                                        {
        //                                            dmlDefaultAmount = 1;
        //                                        }
        //                                        if (m_intNEEDCONFIRM_INT == 1)//ҽ��ִ�е����Ƿ���Ҫȷ�ϱ�־
        //                                        {
        //                                            intChargeItemStatus = 0;
        //                                        }
        //                                        //*--����״̬��0-��ȷ�ϣ�1-���ᣩ
        //                                        if (intIsRich == 1 || ISSELFPAY_CHR.ToUpper().Equals("T"))
        //                                        {
        //                                            intChargeItemStatus = 0;

        //                                        }
        //                                        if (m_intNEEDCONFIRM_INT == 0 && intIsRich == 0 && !ISSELFPAY_CHR.ToUpper().Equals("T"))
        //                                        {
        //                                            intChargeItemStatus = 1;
        //                                        }
        //                                        //
        //                                        if (intChargeItemStatus == 0)
        //                                        {
        //                                            //ACTIVATETYPE_INT = 3;
        //                                            m_blUpdate = true;//����ҽ��ִ�е���־(true- ����Ϊ����Ҫ������ˣ�false-������;
        //                                        }
        //                                        else if (intChargeItemStatus == 1)
        //                                        {
        //                                            //ACTIVATETYPE_INT = 4;

        //                                        }
        //                                        /*<==============================*/
        //                                        //����ҽ��ִ�к���Ч����Ϊ3=ȷ�ϼ���
        //                                        if (intIsRich == 1)
        //                                        {
        //                                            ACTIVATETYPE_INT = 3;
        //                                        }
        //                                        else
        //                                        {
        //                                            ACTIVATETYPE_INT = 1;
        //                                        }
        //                                        //�Է�ҽ��ִ�к���Ч����Ҳ��4=ȷ���շ�
        //                                        if (ISSELFPAY_CHR.Trim().Equals("T"))
        //                                        {
        //                                            ACTIVATETYPE_INT = 4;
        //                                        }

        //                                        //    --���������ϸ��¼
        //                                        strSQL = @" 
        //                                                    insert into t_opr_bih_patientcharge(
        //                                                    pchargeid_chr,  
        //                                                    patientid_chr,  registerid_chr,   chargeactive_dat,     orderid_chr, needconfirm_int,
        //                                                    orderexectype_int,orderexecid_chr,calccateid_chr,   invcateid_chr,   chargeitemid_chr,
        //                                                    chargeitemname_chr,unit_vchr,      unitprice_dec,   amount_dec,       discount_dec,
        //                                                    ismepay_int,      createtype_int,  creator_chr,     create_dat,      status_int,
        //                                                    pstatus_int,      clacarea_chr,    createarea_chr,  isrich_int,      activatetype_int,
        //                                                    curareaid_chr,    curbedid_chr,itemchargetype_vchr
        //                                                    )
        //                                                    values(
        //                                                    lpad(seq_pchargeid.nextval,18,'0'),
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?
        //                                                    )";
        //                                        n = -1;
        //                                        System.Data.IDataParameter[] arrParams4 = null;
        //                                        objHRPSvc.CreateDatabaseParameter(28, out arrParams4);
        //                                        //n++; arrParams[0].Value = strChargeID.Trim();//PChargeID_Chr
        //                                        n++;
        //                                        arrParams4[n].Value = strPatientID.Trim();//PatientID_Chr
        //                                        n++;
        //                                        arrParams4[n].Value = strRegisterID.Trim();//RegisterID_Chr
        //                                        if (intChargeItemStatus == 0)
        //                                        {
        //                                            n++;
        //                                            arrParams4[n].Value = null;////CHARGEACTIVE_DAT ������Ч����
        //                                        }
        //                                        else
        //                                        {
        //                                            n++;
        //                                            arrParams4[n].Value = dtExecuteDate;////CHARGEACTIVE_DAT ������Ч����
        //                                        }
        //                                        n++;
        //                                        arrParams4[n].Value = strOrderID.Trim();//OrderID_Chr
        //                                        if (intChargeItemStatus == 0)
        //                                        {
        //                                            n++;
        //                                            arrParams4[n].Value = 1;//NEEDCONFIRM_INT�Ƿ���Ҫ������� 0-�� 1-��
        //                                        }
        //                                        else
        //                                        {
        //                                            n++;
        //                                            arrParams4[n].Value = 0;//NEEDCONFIRM_INT�Ƿ���Ҫ������� 0-�� 1-��
        //                                        }
        //                                        n++;
        //                                        arrParams4[n].Value = m_intOrderExecType_Int;//OrderExecType_Int ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}
        //                                        n++;
        //                                        arrParams4[n].Value = strExecOrderID.Trim();//OrderExecID_Chr
        //                                        n++;
        //                                        arrParams4[n].Value = strCalcCateID.Trim();//CalCCateID_Chr  ���ú������id {=�շ���Ŀ����.id}
        //                                        n++;
        //                                        arrParams4[n].Value = strINvCateID.Trim();//InvCateID_Chr    ���÷�Ʊ���id {=�շ���Ŀ����.id}
        //                                        n++;
        //                                        arrParams4[n].Value = ItemID_Chr.Trim();//ChargeItemID_Chr

        //                                        n++;
        //                                        arrParams4[n].Value = ItemName_Vchr.Trim();//ChargeItemName_Chr
        //                                        n++;
        //                                        arrParams4[n].Value = ItemIPUnit_Chr.Trim();//Unit_Vchr סԺ��λ{=�շ���Ŀ.סԺ��λ}
        //                                        n++;
        //                                        arrParams4[n].Value = dmlPrice;//UnitPrice_Dec  סԺ����{=�շ���Ŀ.סԺ����}
        //                                        n++;
        //                                        arrParams4[n].Value = dmlAmount;//AMount_Dec    ����
        //                                        n++;
        //                                        arrParams4[n].Value = 1;//DisCount_Dec=1���ۿ۱���


        //                                        if (ISSELFPAY_CHR.ToUpper().Equals("T"))
        //                                        {
        //                                            n++;
        //                                            arrParams4[n].Value = 1;//IsMepay_Int=1�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
        //                                        }
        //                                        else
        //                                        {
        //                                            n++;
        //                                            arrParams4[n].Value = 0;//IsMepay_Int=0�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
        //                                        }
        //                                        n++;
        //                                        arrParams4[n].Value = intCreateType;//CreateType_Int ¼������ {1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
        //                                        n++;
        //                                        arrParams4[n].Value = CONFIRMERID_CHR;//Creator_Chr
        //                                        n++;
        //                                        arrParams4[n].Value = dtExecuteDate;//Create_Dat
        //                                        n++;
        //                                        arrParams4[n].Value = 1;// Status_Int��Ч״̬{1=��Ч;0=��Ч;-1=��ʷ}


        //                                        n++;
        //                                        arrParams4[n].Value = intChargeItemStatus;//PStatus_Int
        //                                        n++;
        //                                        arrParams4[n].Value = CLACAREA_CHR.Trim();//ClacArea_Chr
        //                                        n++;
        //                                        arrParams4[n].Value = CREATEAREA_CHR.Trim();//CreateArea_Chr
        //                                        n++;
        //                                        arrParams4[n].Value = intIsRich;//ISRICH_INT
        //                                        n++;
        //                                        arrParams4[n].Value = ACTIVATETYPE_INT;//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�}
        //                                        n++;
        //                                        arrParams4[n].Value = CURAREAID_CHR;
        //                                        n++;
        //                                        arrParams4[n].Value = CURBEDID_CHR;
        //                                        n++;
        //                                        arrParams4[n].Value = strShiying;
        //                                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams4);
        //                                    }
        //                                }
        //                                /*<===========================================*/
        //                                #endregion

        //                                #region ����ҽ��ִ�е���־(true- ����Ϊ����Ҫ������ˣ�false-������);
        //                                if (m_blUpdate == true)
        //                                {
        //                                    strSQL = " update T_Opr_Bih_OrderExecute set NEEDCONFIRM_INT=1 where ORDEREXECID_CHR='" + strExecOrderID + "' ";
        //                                    lngRes = objHRPSvc.DoExcute(strSQL);

        //                                }
        //                                #endregion
        //                            }
        //                        }
        //                        else//--����һ��ҽ��ִ�е���¼��[����]
        //                        {
        //                            #region ����ִ�е�
        //                            //--�����µ�ҽ��ִ�е���ˮ�ţ�����λ����
        //                            strSQL = "   select lpad(SEQ_ORDEREXECID.Nextval,18,'0') OrderExecID_Chr   from dual ";
        //                            dtbResult2 = new DataTable();
        //                            bool m_blUpdate = false;//����ҽ��ִ�е���־(true- ����Ϊ����Ҫ������ˣ�false-������;)

        //                            objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult2);
        //                            strExecOrderID = dtbResult2.Rows[0]["OrderExecID_Chr"].ToString();

        //                            strSQL = @"  insert into T_Opr_Bih_OrderExecute (OrderExecID_Chr,OrderID_Chr,CreatorID_Chr,Creator_Chr,CreateDate_Dat,
        //                                            ExecuteTime_Int,ExecuteDays_Int,ExecuteDate_VChr,IsCharge_Int,IsIncept_int,IsFirst_int,IsRecruit_int,Status_Int,NEEDCONFIRM_INT)
        //                                            values(?,?,?,?,?,?,?,?,?,?,?,?,?,?)
        //                                            ";
        //                            System.Data.IDataParameter[] arrParams2 = null;
        //                            objHRPSvc.CreateDatabaseParameter(14, out arrParams2);
        //                            int n = -1;
        //                            n++;
        //                            arrParams2[n].Value = strExecOrderID;//OrderExecID_Chr ��ˮ��
        //                            n++;
        //                            arrParams2[n].Value = OrderID_Chr;//OrderID_Chr ҽ����id	{=ҽ����.id}
        //                            n++;
        //                            arrParams2[n].Value = CONFIRMERID_CHR;//CreatorID_Chr ����ִ�е���{=��Ա.Id}
        //                            n++;
        //                            arrParams2[n].Value = CONFIRMER_VCHR;//Creator_Chr ����ִ�е���{=��Ա.name}
        //                            n++;
        //                            arrParams2[n].Value = dtExecuteDate;//CreateDate_Dat ����ִ�е�ʱ��  ȡ��ǰϵͳʱ��
        //                            n++;
        //                            arrParams2[n].Value = Times_Int;//ExecuteTime_Int  ִ�д���  Ƶ�ʱ�Ĵ���
        //                            n++;
        //                            arrParams2[n].Value = DAYS_INT;//ExecuteDays_Int   ִ������  ���� ������Ƶ�ʱ�
        //                            n++;
        //                            arrParams2[n].Value = ExecTime;//ExecuteDate_VChr  ִ��ʱ�� ��: 08:00-14:00-��20:00 ������Ƶ�ʱ�
        //                            n++;
        //                            arrParams2[n].Value = 0;//IsCharge_Int=0  �Ƿ��ѼƷ�{1-��/0-��}
        //                            n++;
        //                            arrParams2[n].Value = 0;//IsIncept_int=0   �Ƿ��ѷ���{1-��/0-��}
        //                            n++;
        //                            arrParams2[n].Value = intIsFirst;//IsFirst_int �Ƿ��״�ִ��{1-��/0-��}
        //                            n++;
        //                            arrParams2[n].Value = 0;
        //                            n++;
        //                            arrParams2[n].Value = 1;//Status_Int=1    STATUS_INT ��Ч��־	{1=��Ч;0=ɾ��;-1=��ʷ}
        //                            n++;
        //                            arrParams2[n].Value = m_intNEEDCONFIRM_INT;//�Ƿ���Ҫ������� 0-�� 1-��
        //                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams2);
        //                            #endregion

        //                            #region �����շ���Ŀ��ϸ��
        //                            //�����շ���Ŀ��ϸ��
        //                            strSQL = @"
        //                                                select 
        //                                                a.orderid_chr,a.orderdicid_chr,a.chargeitemid_chr,a.clacarea_chr,
        //                                                a.createarea_chr,a.chargeitemname_chr,a.spec_vchr,a.unit_vchr,
        //                                                a.amount_dec,a.unitprice_dec,a.creatorid_chr,a.creator_vchr,
        //                                                a.createdate_dat,a.flag_int
        //                                                ,a.continueusetype_int
        //
        //                                                ,c.ratetype_int,c.patientid_chr,c.registerid_chr,c.executetype_int,
        //
        //                                                d.itemipcalctype_chr,d.itemipinvtype_chr,d.isrich_int,d.isselfpay_chr,c.isrepare_int,
        //                                                 f.dosageviewtype,f.createchargetype,a.itemchargetype_vchr
        //
        //                                                from
        //                                                t_opr_bih_orderchargedept a,
        //                                                t_opr_bih_orderexecute b,
        //                                                t_opr_bih_order c,
        //                                                t_bse_chargeitem d,
        //                                                t_aid_bih_ordercate f
        //                                                where 
        //                                                b.orderid_chr=c.orderid_chr
        //                                                and
        //                                                b.orderid_chr=a.orderid_chr
        //                                                and
        //                                                a.chargeitemid_chr=d.itemid_chr
        //                                                and
        //                                                d.ordercateid_chr=f.ordercateid_chr(+)
        //                                                and
        //
        //                                                b.orderexecid_chr=?
        //                                            ";
        //                            System.Data.IDataParameter[] arrParams3 = null;
        //                            objHRPSvc.CreateDatabaseParameter(1, out arrParams3);
        //                            arrParams3[0].Value = strExecOrderID;
        //                            dtbResult3 = new DataTable();
        //                            lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult3, arrParams3);
        //                            if (lngRes > 0 && dtbResult3.Rows.Count > 0)
        //                            {
        //                                string strDicID = "";          //--������Ŀ��ˮ��
        //                                string strOrderCateID = "";    // --ҽ������ID
        //                                string strChargeID = "";       //--
        //                                string strDefaultItemID = "";  //--���շ���ĿID

        //                                int dmlDefaultAmount = 0;  //-һ������

        //                                int intRateType = 0;//*���ñ�־	{1=ȫ�Ʒ�;2=�÷��Ʒ�;3=���Ʒ�}
        //                                int intCreateType = 0;        //-- 3 ����(ҽ��)    1�Զ�(ҽ��)
        //                                int intIsRich = 0;                  //*--�շ���Ŀ�Ĺ��ر�־
        //                                int intChargeItemStatus = 0;        //*--����״̬��0-��ȷ�ϣ�1-���ᣩ
        //                                int intRATETYPE_INT = 0;//�Ƿ�Ʒ� 0-���Ʒ� 1-�Ʒ�
        //                                decimal dmlAmount = 0;//*
        //                                string strPatientID = "";      //--����ID*
        //                                string strRegisterID = "";     //--��Ժ�Ǽ�ID*
        //                                int intExecuteType = 0;             //*--ҽ��ִ������{ִ������{1=����;2=��ʱ;3=��Ժ��ҩ}}
        //                                int m_intOrderExecType_Int = 0;      //*--���벡�˷�����ϸ���ORDEREXECTYPE_INT��ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}��
        //                                string strCalcCateID = "";          //*--��ĿסԺ�������
        //                                string strINvCateID = "";           //*--��ĿסԺ��Ʊ���
        //                                decimal dmlPrice = 0;          //*--סԺ����(=��Ŀ�۸�/��װ��)
        //                                int intDosageView = 0;              //*--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1

        //                                int intCreateChargeType = 0; //-- ������Ч��ʽ{1=ִ��ҽ��ʱ��Ч;2=ִ�п��ҽ���ҽ��ʱ��Ч}
        //                                int ACTIVATETYPE_INT = 1;//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�}

        //                                string ItemID_Chr = "";
        //                                string ItemName_Vchr = "";  //�շ���Ŀ
        //                                string ItemIPUnit_Chr = "";
        //                                string CLACAREA_CHR = "";
        //                                string CREATEAREA_CHR = "";
        //                                /* <<============================= */
        //                                int CONTINUEUSETYPE_INT = 0;//�������� {0=������;1=ȫ������;2-��������}0������仯��1����������仯
        //                                string ISSELFPAY_CHR = "";//�Ƿ��Է���Ŀ("T","F")

        //                                for (int m = 0; m < dtbResult3.Rows.Count; m++)
        //                                {

        //                                    strPatientID = clsConverter.ToString(dtbResult3.Rows[m]["patientid_chr"].ToString());
        //                                    strRegisterID = clsConverter.ToString(dtbResult3.Rows[m]["registerid_chr"].ToString());
        //                                    strOrderID = clsConverter.ToString(dtbResult3.Rows[m]["orderid_chr"].ToString());
        //                                    intExecuteType = clsConverter.ToInt(dtbResult3.Rows[m]["ExecuteType_Int"].ToString());
        //                                    strCalcCateID = clsConverter.ToString(dtbResult3.Rows[m]["ItemIPCalcType_Chr"].ToString());
        //                                    strINvCateID = clsConverter.ToString(dtbResult3.Rows[m]["ItemIpInvType_Chr"].ToString());
        //                                    ItemID_Chr = clsConverter.ToString(dtbResult3.Rows[m]["chargeitemid_chr"].ToString());
        //                                    ItemName_Vchr = clsConverter.ToString(dtbResult3.Rows[m]["chargeitemname_chr"].ToString());
        //                                    ItemIPUnit_Chr = clsConverter.ToString(dtbResult3.Rows[m]["unit_vchr"].ToString());
        //                                    dmlPrice = clsConverter.ToDecimal(dtbResult3.Rows[m]["unitprice_dec"].ToString());
        //                                    dmlAmount = clsConverter.ToDecimal(dtbResult3.Rows[m]["amount_dec"].ToString());
        //                                    CLACAREA_CHR = clsConverter.ToString(dtbResult3.Rows[m]["clacarea_chr"].ToString());
        //                                    CREATEAREA_CHR = clsConverter.ToString(dtbResult3.Rows[m]["createarea_chr"].ToString());
        //                                    intIsRich = clsConverter.ToInt(dtbResult3.Rows[m]["isrich_int"].ToString());
        //                                    //intIsRepare = clsConverter.ToInt(dtbResult3.Rows[i]["IsRepare_Int"].ToString());
        //                                    intRateType = clsConverter.ToInt(dtbResult3.Rows[m]["RateType_Int"].ToString());
        //                                    intDosageView = clsConverter.ToInt(dtbResult3.Rows[m]["dosageviewtype"].ToString());
        //                                    intRATETYPE_INT = clsConverter.ToInt(dtbResult3.Rows[m]["RATETYPE_INT"].ToString());
        //                                    // intCreateChargeType = clsConverter.ToInt(dtbResult.Rows[0]["CreateChargeType"].ToString());
        //                                    ISSELFPAY_CHR = clsConverter.ToString(dtbResult3.Rows[m]["ISSELFPAY_CHR"].ToString().Trim());
        //                                    strShiying = clsConverter.ToString(dtbResult3.Rows[m]["itemchargetype_vchr"].ToString().Trim());
        //                                    if (!dtbResult3.Rows[m]["CONTINUEUSETYPE_INT"].ToString().Trim().Equals(""))
        //                                        CONTINUEUSETYPE_INT = clsConverter.ToInt(dtbResult3.Rows[m]["CONTINUEUSETYPE_INT"].ToString().Trim());
        //                                    //���ã�����
        //                                    if (CONTINUEUSETYPE_INT == 1 && ExecuteType_Int == 1)
        //                                    {
        //                                        continue;
        //                                    }
        //                                    /*<===========================*/
        //                                    //  intExecuteType--ҽ��ִ������{ִ������{1=����;2=��ʱ;3=��Ժ��ҩ}}
        //                                    //  m_intOrderExecType_Int--���벡�˷�����ϸ���ORDEREXECTYPE_INT��ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}��

        //                                    switch (intExecuteType)
        //                                    {
        //                                        case 1:
        //                                            m_intOrderExecType_Int = 1;
        //                                            break;
        //                                        case 2:
        //                                            m_intOrderExecType_Int = 2;
        //                                            break;
        //                                        case 3:
        //                                            m_intOrderExecType_Int = 4;
        //                                            break;

        //                                    }

        //                                    /*<===========================*/
        //                                    //intRateType--��ȡ�ѷ��ñ�־	{1=ȫ�Ʒ�;2=�÷��Ʒ�;3=���Ʒ�}
        //                                    //if (intRateType == 1 || intRateType == 2)
        //                                    //{
        //                                    //    return;
        //                                    //}
        //                                    //--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1
        //                                    if (intDosageView == 2)
        //                                    {
        //                                        dmlDefaultAmount = 1;
        //                                    }
        //                                    if (m_intNEEDCONFIRM_INT == 1)//ҽ��ִ�е����Ƿ���Ҫȷ�ϱ�־
        //                                    {
        //                                        intChargeItemStatus = 0;
        //                                    }
        //                                    //*--����״̬��0-��ȷ�ϣ�1-���ᣩ
        //                                    if (intIsRich == 1 || ISSELFPAY_CHR.ToUpper().Equals("T"))
        //                                    {
        //                                        intChargeItemStatus = 0;
        //                                    }
        //                                    if (m_intNEEDCONFIRM_INT == 0 && intIsRich == 0 && !ISSELFPAY_CHR.ToUpper().Equals("T"))
        //                                    {
        //                                        intChargeItemStatus = 1;
        //                                    }
        //                                    //
        //                                    if (intChargeItemStatus == 0)
        //                                    {
        //                                        //ACTIVATETYPE_INT = 3;
        //                                        m_blUpdate = true;//����ҽ��ִ�е���־(true- ����Ϊ����Ҫ������ˣ�false-������;

        //                                    }
        //                                    else if (intChargeItemStatus == 1)
        //                                    {
        //                                        //ACTIVATETYPE_INT = 4;
        //                                    }
        //                                    /*<==============================*/
        //                                    //����ҽ��ִ�к���Ч����Ϊ3=ȷ�ϼ���
        //                                    if (intIsRich == 1)
        //                                    {
        //                                        ACTIVATETYPE_INT = 3;
        //                                    }
        //                                    else
        //                                    {
        //                                        ACTIVATETYPE_INT = 1;
        //                                    }
        //                                    //�Է�ҽ��ִ�к���Ч����Ҳ��4=ȷ���շ�
        //                                    if (ISSELFPAY_CHR.Trim().Equals("T"))
        //                                    {
        //                                        ACTIVATETYPE_INT = 4;
        //                                    }
        //                                    //    --���������ϸ��¼
        //                                    strSQL = @" 
        //                                                    insert into T_Opr_Bih_PatientCharge(
        //                                                    PChargeID_Chr,  
        //                                                    PatientID_Chr,  RegisterID_Chr,   CHARGEACTIVE_DAT,     OrderID_Chr,       NEEDCONFIRM_INT,
        //                                                    OrderExecType_Int,OrderExecID_Chr,CalCCateID_Chr,   InvCateID_Chr,   ChargeItemID_Chr,
        //                                                    ChargeItemName_Chr,Unit_Vchr,      UnitPrice_Dec,   AMount_Dec,       DisCount_Dec,
        //                                                    IsMepay_Int,      CreateType_Int,  Creator_Chr,     Create_Dat,      Status_Int,
        //                                                    PStatus_Int,      ClacArea_Chr,    CreateArea_Chr,  ISRICH_INT,       ACTIVATETYPE_INT,
        //                                                    CURAREAID_CHR,CURBEDID_CHR,itemchargetype_vchr
        //                                                    )
        //                                                    values(
        //                                                    lpad(SEQ_PCHARGEID.Nextval,18,'0'),
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?,?,?,
        //                                                    ?,?,?,?,?,
        //                                                    ?,?
        //                                                    )";
        //                                    n = -1;
        //                                    System.Data.IDataParameter[] arrParams4 = null;
        //                                    objHRPSvc.CreateDatabaseParameter(28, out arrParams4);
        //                                    //n++; arrParams[0].Value = strChargeID.Trim();//PChargeID_Chr
        //                                    n++;
        //                                    arrParams4[n].Value = strPatientID.Trim();//PatientID_Chr
        //                                    n++;
        //                                    arrParams4[n].Value = strRegisterID.Trim();//RegisterID_Chr
        //                                    if (intChargeItemStatus == 0)
        //                                    {
        //                                        n++;
        //                                        arrParams4[n].Value = null;////CHARGEACTIVE_DAT ������Ч����
        //                                    }
        //                                    else
        //                                    {
        //                                        n++;
        //                                        arrParams4[n].Value = dtExecuteDate;////CHARGEACTIVE_DAT ������Ч����
        //                                    }
        //                                    n++;
        //                                    arrParams4[n].Value = strOrderID.Trim();//OrderID_Chr
        //                                    if (intChargeItemStatus == 0)
        //                                    {
        //                                        n++;
        //                                        arrParams4[n].Value = 1;//NEEDCONFIRM_INT�Ƿ���Ҫ������� 0-�� 1-��
        //                                    }
        //                                    else
        //                                    {
        //                                        n++;
        //                                        arrParams4[n].Value = 0;//NEEDCONFIRM_INT�Ƿ���Ҫ������� 0-�� 1-��
        //                                    }

        //                                    n++;
        //                                    arrParams4[n].Value = m_intOrderExecType_Int;//OrderExecType_Int ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}
        //                                    n++;
        //                                    arrParams4[n].Value = strExecOrderID.Trim();//OrderExecID_Chr
        //                                    n++;
        //                                    arrParams4[n].Value = strCalcCateID.Trim();//CalCCateID_Chr  ���ú������id {=�շ���Ŀ����.id}
        //                                    n++;
        //                                    arrParams4[n].Value = strINvCateID.Trim();//InvCateID_Chr    ���÷�Ʊ���id {=�շ���Ŀ����.id}
        //                                    n++;
        //                                    arrParams4[n].Value = ItemID_Chr.Trim();//ChargeItemID_Chr

        //                                    n++;
        //                                    arrParams4[n].Value = ItemName_Vchr.Trim();//ChargeItemName_Chr
        //                                    n++;
        //                                    arrParams4[n].Value = ItemIPUnit_Chr.Trim();//Unit_Vchr סԺ��λ{=�շ���Ŀ.סԺ��λ}
        //                                    n++;
        //                                    arrParams4[n].Value = dmlPrice;//UnitPrice_Dec  סԺ����{=�շ���Ŀ.סԺ����}
        //                                    n++;
        //                                    arrParams4[n].Value = dmlAmount;//AMount_Dec    ����
        //                                    n++;
        //                                    arrParams4[n].Value = 1;//DisCount_Dec=1���ۿ۱���

        //                                    if (ISSELFPAY_CHR.ToUpper().Equals("T"))
        //                                    {
        //                                        n++;
        //                                        arrParams4[n].Value = 1;//IsMepay_Int=1�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
        //                                    }
        //                                    else
        //                                    {
        //                                        n++;
        //                                        arrParams4[n].Value = 0;//IsMepay_Int=0�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
        //                                    }
        //                                    n++;
        //                                    arrParams4[n].Value = intCreateType;//CreateType_Int ¼������ {1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
        //                                    n++;
        //                                    arrParams4[n].Value = CONFIRMERID_CHR;//Creator_Chr
        //                                    n++;
        //                                    arrParams4[n].Value = dtExecuteDate;//Create_Dat
        //                                    n++;
        //                                    arrParams4[n].Value = 1;// Status_Int��Ч״̬{1=��Ч;0=��Ч;-1=��ʷ}


        //                                    n++;
        //                                    arrParams4[n].Value = intChargeItemStatus;//PStatus_Int
        //                                    n++;
        //                                    arrParams4[n].Value = CLACAREA_CHR.Trim();//ClacArea_Chr
        //                                    n++;
        //                                    arrParams4[n].Value = CREATEAREA_CHR.Trim();//CreateArea_Chr
        //                                    n++;
        //                                    arrParams4[n].Value = intIsRich;//ISRICH_INT
        //                                    n++;
        //                                    arrParams4[n].Value = ACTIVATETYPE_INT;//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�}
        //                                    n++;
        //                                    arrParams4[n].Value = CURAREAID_CHR;
        //                                    n++;
        //                                    arrParams4[n].Value = CURBEDID_CHR;
        //                                    n++;
        //                                    arrParams4[n].Value = strShiying;
        //                                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams4);
        //                                }
        //                            }
        //                            /*<===========================================*/
        //                            #endregion

        //                            #region ����ҽ��ִ�е���־(true- ����Ϊ����Ҫ������ˣ�false-������);
        //                            if (m_blUpdate == true)
        //                            {
        //                                strSQL = " update T_Opr_Bih_OrderExecute set NEEDCONFIRM_INT=1 where ORDEREXECID_CHR='" + strExecOrderID + "' ";
        //                                lngRes = objHRPSvc.DoExcute(strSQL);

        //                            }
        //                            #endregion
        //                        }

        //                        #endregion

        //                        #region 3.����ҽ��״̬
        //                        //3.����ҽ��״̬
        //                        //intType 1���������������������¿��ӣ�����Ժ��ҩ
        //                        if ((intType == 1) || (intType == 2) || (intType == 4))
        //                        {
        //                            strSQL = @"
        //                                     update T_Opr_Bih_Order
        //                                     set ExecutorID_Chr=?,Executor_Chr=?,ExecuteDate_Dat=sysdate,StartDate_Dat=sysdate,Status_Int=2
        //                                     where OrderID_Chr=?
        //                                    ";
        //                            arrParams = null;
        //                            objHRPSvc.CreateDatabaseParameter(3, out arrParams);
        //                            arrParams[0].Value = CONFIRMERID_CHR;
        //                            arrParams[1].Value = CONFIRMER_VCHR;
        //                            arrParams[2].Value = strOrderID;
        //                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
        //                        }
        //                        #endregion

        //                    }
        //                }
        //            }
        //            catch (Exception objEx)
        //            {
        //                string strTmp = objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;

        //        }
        //        #endregion

        #region ��ȡû�а�ҩ�Ĳ�����Ϣ
        /// <summary>
        /// ��ȡû�а�ҩ�Ĳ�����Ϣ
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtPatients"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPersonListByArea(string m_strAreaid_chr, out DataTable m_dtPatients)
        {
            long lngRes = -1;

            m_dtPatients = new DataTable();
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                //���˱�
                string strSql = @"
                SELECT   a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                c.code_chr bedname, d.deptname_vchr areaname,
                a.limitrate_mny, e.flgname_vchr AS state, mzdiagnose_vchr,
                f.paytypename_vchr paytypename_vchr, SYSDATE today,
                g.remarkname_vchr, a.des_vchr
                FROM
                (select 
                count(k2.registerid_chr),k2.registerid_chr
                from 
                t_opr_bih_orderexecute k1,
                t_opr_bih_order k2,
                t_opr_bih_patientcharge k3
                where
                k1.orderid_chr=k2.orderid_chr
                and
                k3.orderexecid_chr=k1.orderexecid_chr
                and
                k3.AMOUNT_DEC>0
                and
                k1.STATUS_INT=1
                and
                k1.ISINCEPT_INT=0
                and
                k1.NEEDCONFIRM_INT=0
                and
                k3.PSTATUS_INT = 1
                and
                k3.STATUS_INT = 1
                and 
                K2.CREATEAREAID_CHR='[areaid_chr]'
                group by 
                k2.registerid_chr) k,

                t_opr_bih_register a,
                t_opr_bih_registerdetail b,
                t_bse_bed c,
                t_bse_deptdesc d,
                (SELECT flg_int, flgname_vchr
                FROM t_sys_flg_table
                WHERE tablename_vchr = 't_opr_bih_register'
                AND columnname_vchr = 'PSTATUS_INT') e,
                (SELECT tf.paytypeid_chr, tf.paytypename_vchr, tf.memo_vchr, tf.paylimit_mny, tf.payflag_dec, tf.paypercent_dec, tf.paytypeno_vchr, tf.isusing_num, tf.copayid_chr, tf.chargepercent_dec, tf.internalflag_int, tf.coalitionrecipeflag_int, tf.bihlimitrate_dec
                FROM t_bse_patientpaytype tf
                WHERE tf.isusing_num = 1 AND tf.payflag_dec != 1) f,
                t_opr_bih_patspecremark g

                WHERE

                k.REGISTERID_CHR=a.registerid_chr
                and a.registerid_chr = b.registerid_chr(+)
                AND a.status_int = 1

                AND a.registerid_chr = c.bihregisterid_chr(+)
                AND a.areaid_chr = d.deptid_chr(+)
                AND a.pstatus_int = e.flg_int(+)
                AND a.paytypeid_chr = f.paytypeid_chr(+)
                AND a.registerid_chr = g.registerid_chr(+)

                and a.pstatus_int!=3
              
                order by c.code_chr
                ";
                strSql = strSql.Replace("[areaid_chr]", m_strAreaid_chr);
                lngRes = HRPService.DoGetDataTable(strSql, ref m_dtPatients);

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

        #region ��ȡ��Ҫ����ȷ�ϼ��ʵ�����
        /// <summary>
        /// ��ȡ��Ҫ����ȷ�ϼ��ʵ�����
        /// </summary>
        /// <param name="m_strRegisterID"></param>
        /// <param name="m_dtOrderExecute"></param>
        /// <param name="m_dtChargeList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetComfirmChargeData(string m_strRegisterID, out DataTable m_dtOrderExecute, out DataTable m_dtChargeList)
        {
            long lngRes = -1;

            m_dtOrderExecute = new DataTable();
            m_dtChargeList = new DataTable();

            string strSql = @"
                 select 
                a.name_vchr,
                a.recipeno_int,
                a.executetype_int,
                a.registerid_chr,
                a.doctor_vchr,
                b.orderexecid_chr, b.orderid_chr, b.creatorid_chr, b.creator_chr, b.createdate_dat, b.executetime_int, 
	b.executedate_vchr, b.ischarge_int, b.isincept_int, b.isfirst_int, b.isrecruit_int, b.status_int, 
	b.operatorid_chr, b.operator_chr, b.deactivatorid_chr, b.deactivator_chr, b.deactivate_dat, 
	b.executedays_int, b.needconfirm_int, b.confirmerid_chr, b.confirmer_vchr, b.confirm_dat, 
	b.print_date, b.exeareaid_chr, b.exebedid_chr, b.repare_int, b.autoid_vchr
                from 
                t_opr_bih_order a,
                t_opr_bih_orderexecute b,
                (select count(k.orderexecid_chr) richCount,orderexecid_chr  from t_opr_bih_patientcharge k where k.ACTIVATETYPE_INT=3 and k.registerid_chr=? group by k.orderexecid_chr) c
             
                where 
                b.orderid_chr=a.orderid_chr
                and
                b.orderexecid_chr=c.orderexecid_chr
               
                --and
                --b.STATUS_INT=1
                and
                b.NEEDCONFIRM_INT=1
                and
                c.richCount>0
                and
                a.registerid_chr=?
               
                order by b.createdate_dat
                ";
            /*<====================================================================*/

            //strSql = strSql.Replace("[registerid_chr]", m_strRegisterID);
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = m_strRegisterID;
                arrParams[1].Value = m_strRegisterID;
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtOrderExecute, arrParams);
                if ((lngRes > 0) && (m_dtOrderExecute != null) && m_dtOrderExecute.Rows.Count > 0)
                {


                    //������ϸ��
                    strSql = @"
                        select 
                        a.orderexecid_chr,
                        b.pchargeid_chr, b.patientid_chr, b.registerid_chr, b.active_dat, b.orderid_chr, b.orderexectype_int, 
	b.orderexecid_chr, b.clacarea_chr, b.createarea_chr, b.calccateid_chr, b.invcateid_chr, b.chargeitemid_chr, 
	b.chargeitemname_chr, b.unit_vchr, b.unitprice_dec, b.amount_dec, b.discount_dec, b.ismepay_int, b.des_vchr, 
	b.createtype_int, b.creator_chr, b.create_dat, b.operator_chr, b.modify_dat, b.deactivator_chr, b.deactivate_dat, 
	b.status_int, b.pstatus_int, b.chearaccount_dat, b.dayaccountid_chr, b.paymoneyid_chr, b.activator_chr, 
	b.activatetype_int, b.isrich_int, b.isconfirmrefundment, b.refundmentchecker, b.refundmentdate, b.bmstatus_int, 
	b.curareaid_chr, b.curbedid_chr, b.doctorid_chr, b.doctor_vchr, b.doctorgroupid_chr, b.needconfirm_int, 
	b.confirmerid_chr, b.confirmer_vchr, b.confirm_dat, b.chargeactive_dat, b.insuracedesc_vchr, b.spec_vchr, 
	b.totalmoney_dec, b.acctmoney_dec, b.newdiscount_dec, b.patientnurse_int, b.attachorderid_vchr, 
	b.attachorderbasenum_dec, b.putmedicineflag_int, b.chargedoctorid_chr, b.chargedoctor_vchr, b.pchargeidorg_chr, 
	b.chargedoctorgroupid_chr, b.returnmedbillno, b.manyreturnmedill_int, b.itemchargetype_vchr,
                        c.deptname_vchr clacarea_name ,
                         d.deptname_vchr   createarea_name    
                        from
                        t_opr_bih_orderexecute a,
                        t_opr_bih_patientcharge b,
                        t_bse_deptdesc c,
                        t_bse_deptdesc d
                        where 
                        a.orderexecid_chr=b.orderexecid_chr
                        and
                        b.clacarea_chr=c.deptid_chr(+)
                        and
                        b.createarea_chr=d.deptid_chr(+)
                        --and
                        --a.status_int=1
                        and
                        a.needconfirm_int=1
                        and 
                        b.activatetype_int=3
                        and
                        b.registerid_chr=?  ";
                    System.Data.IDataParameter[] arrParams2 = null;
                    HRPService.CreateDatabaseParameter(1, out arrParams2);
                    arrParams2[0].Value = m_strRegisterID;
                    lngRes = 0;
                    lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtChargeList, arrParams2);




                }
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

        #region ��ȡ��Ҫ����ȷ��ֱ���շѵ�����
        /// <summary>
        /// ��ȡ��Ҫ����ȷ��ֱ���շѵ����� 
        /// </summary>
        /// <param name="m_strRegisterID"></param>
        /// <param name="m_dtOrderExecute"></param>
        /// <param name="m_dtChargeList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetComfirmThChargeData(string m_strRegisterID, out DataTable m_dtOrderExecute, out DataTable m_dtChargeList)
        {
            long lngRes = -1;

            m_dtOrderExecute = new DataTable();
            m_dtChargeList = new DataTable();

            string strSql = @"
                 select 
                a.name_vchr,
                a.recipeno_int,
                a.executetype_int,
                a.registerid_chr,
                a.doctor_vchr,
                b.orderexecid_chr, b.orderid_chr, b.creatorid_chr, b.creator_chr, b.createdate_dat, b.executetime_int, 
	            b.executedate_vchr, b.ischarge_int, b.isincept_int, b.isfirst_int, b.isrecruit_int, b.status_int, 
	            b.operatorid_chr, b.operator_chr, b.deactivatorid_chr, b.deactivator_chr, b.deactivate_dat, 
	            b.executedays_int, b.needconfirm_int, b.confirmerid_chr, b.confirmer_vchr, b.confirm_dat, 
	            b.print_date, b.exeareaid_chr, b.exebedid_chr, b.repare_int, b.autoid_vchr
                from 
                t_opr_bih_order a,
                T_Opr_Bih_OrderExecute b,
                (select count(k.orderexecid_chr) richCount,orderexecid_chr  from t_opr_bih_patientcharge k where k.ACTIVATETYPE_INT=4 and k.registerid_chr=? group by k.orderexecid_chr) c
             
                where 
                b.orderid_chr=a.orderid_chr
                and
                b.orderexecid_chr=c.orderexecid_chr
                and
                b.NEEDCONFIRM_INT=1
                and
                c.richCount>0
                and
                a.registerid_chr=?
               
                order by b.createdate_dat
                ";
            /*<====================================================================*/

            //strSql = strSql.Replace("[registerid_chr]", m_strRegisterID);
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = m_strRegisterID;
                arrParams[1].Value = m_strRegisterID;
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtOrderExecute, arrParams);

                if ((lngRes > 0) && (m_dtOrderExecute != null) && m_dtOrderExecute.Rows.Count > 0)
                {


                    //������ϸ��
                    strSql = @"
                        select k.*, f.precent_dec
                        from (select b.pchargeid_chr, b.patientid_chr, b.registerid_chr, b.active_dat, b.orderid_chr, b.orderexectype_int, 
	b.orderexecid_chr, b.clacarea_chr, b.createarea_chr, b.calccateid_chr, b.invcateid_chr, b.chargeitemid_chr, 
	b.chargeitemname_chr, b.unit_vchr, b.unitprice_dec, b.amount_dec, b.discount_dec, b.ismepay_int, b.des_vchr, 
	b.createtype_int, b.creator_chr, b.create_dat, b.operator_chr, b.modify_dat, b.deactivator_chr, b.deactivate_dat, 
	b.status_int, b.pstatus_int, b.chearaccount_dat, b.dayaccountid_chr, b.paymoneyid_chr, b.activator_chr, 
	b.activatetype_int, b.isrich_int, b.isconfirmrefundment, b.refundmentchecker, b.refundmentdate, b.bmstatus_int, 
	b.curareaid_chr, b.curbedid_chr, b.doctorid_chr, b.doctor_vchr, b.doctorgroupid_chr, b.needconfirm_int, 
	b.confirmerid_chr, b.confirmer_vchr, b.confirm_dat, b.chargeactive_dat, b.insuracedesc_vchr, b.spec_vchr, 
	b.totalmoney_dec, b.acctmoney_dec, b.newdiscount_dec, b.patientnurse_int, b.attachorderid_vchr, 
	b.attachorderbasenum_dec, b.putmedicineflag_int, b.chargedoctorid_chr, b.chargedoctor_vchr, b.pchargeidorg_chr, 
	b.chargedoctorgroupid_chr, b.returnmedbillno, b.manyreturnmedill_int, b.itemchargetype_vchr, c.deptname_vchr clacarea_name,
                        d.deptname_vchr createarea_name, e.paytypeid_chr,f.flag_int
                        from t_opr_bih_orderexecute a,
                        t_opr_bih_patientcharge b,
                        t_bse_deptdesc c,
                        t_bse_deptdesc d,
                        t_opr_bih_register e,
                        t_opr_bih_orderchargedept f
                        where a.orderexecid_chr = b.orderexecid_chr
                        and
                        b.orderid_chr=f.orderid_chr
                        and
                        b.chargeitemid_chr=f.chargeitemid_chr
                        and b.clacarea_chr = c.deptid_chr(+)
                        and b.createarea_chr = d.deptid_chr(+)
                        and b.registerid_chr = e.registerid_chr
                        --and a.status_int = 1
                        and a.needconfirm_int = 1
                        and b.activatetype_int=4
                        and b.registerid_chr = ?) k,
                        t_aid_inschargeitem f
                        where k.chargeitemid_chr = f.itemid_chr(+) and k.paytypeid_chr = f.copayid_chr(+)

                       ";
                    // strSql = strSql.Replace("[registerid_chr]", m_strRegisterID);
                    //  lngRes = HRPService.DoGetDataTable(strSql, ref m_dtChargeList);
                    System.Data.IDataParameter[] arrParams2 = null;
                    HRPService.CreateDatabaseParameter(1, out arrParams2);
                    arrParams2[0].Value = m_strRegisterID;
                    lngRes = 0;
                    lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtChargeList, arrParams2);




                }
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

        #region ҽ��ִ�е���ȷ�Ϸ���ȷ�ϲ�����
        /// <summary>
        /// ҽ��ִ�е���ȷ�Ϸ���ȷ�ϲ�����
        /// </summary>
        /// <param name="m_strOrderExecuteID_Arr">ҽ��ID(���)</param>
        /// <param name="CONFIRMERID_CHR">����ύ��ID</param>
        /// <param name="CONFIRMER_VCHR">����ύ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBihOrderExecuteChargeConfirmer(string m_strOrderExecuteID_Arr, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";

            strSQL = @"
                update 
                T_OPR_BIH_ORDEREXECUTE
                set
                CONFIRMERID_CHR='[CONFIRMERID_CHR]',
                CONFIRMER_VCHR='[CONFIRMER_VCHR]',
                CONFIRM_DAT=sysdate
                where
                NEEDCONFIRM_INT=1
                and
                ORDEREXECID_CHR in ([ORDEREXECID_CHR])
                ";
            strSQL = strSQL.Replace("[CONFIRMERID_CHR]", CONFIRMERID_CHR.Trim());
            strSQL = strSQL.Replace("[CONFIRMER_VCHR]", CONFIRMER_VCHR.Trim());
            strSQL = strSQL.Replace("[ORDEREXECID_CHR]", m_strOrderExecuteID_Arr.Trim());


            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {
                    strSQL = @"
                   
                          update 
                T_OPR_BIH_PATIENTCHARGE
                set
                CONFIRMERID_CHR='[CONFIRMERID_CHR]',
                CONFIRMER_VCHR='[CONFIRMER_VCHR]',
                CONFIRM_DAT=sysdate,
                CHARGEACTIVE_DAT=sysdate,
                PSTATUS_INT=1
                where
                NEEDCONFIRM_INT=1
                and
                PSTATUS_INT=0
                and
                ACTIVATETYPE_INT=3
                and
                ORDEREXECID_CHR in ([ORDEREXECID_CHR])
                ";
                    strSQL = strSQL.Replace("[CONFIRMERID_CHR]", CONFIRMERID_CHR.Trim());
                    strSQL = strSQL.Replace("[CONFIRMER_VCHR]", CONFIRMER_VCHR.Trim());
                    strSQL = strSQL.Replace("[ORDEREXECID_CHR]", m_strOrderExecuteID_Arr.Trim());
                    lngRes = objHRPSvc.DoExcute(strSQL);

                }
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

        #region ҽ��ִ�е���ȷ�Ϸ���ȷ�ϲ�����
        /// <summary>
        /// ҽ��ִ�е���ȷ�Ϸ���ȷ�ϲ�����
        /// </summary>
        /// <param name="m_arrPCHARGEID_CHR">���ñ���ˮ������</param>
        /// <param name="CONFIRMERID_CHR">����ύ��ID</param>
        /// <param name="CONFIRMER_VCHR">����ύ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBihOrderExecuteChargeConfirmerTh(List<string> m_arrPCHARGEID_CHR, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            long lngAff = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            string m_strPCHARGEID_CHR = "";
            for (int i = 0; i < m_arrPCHARGEID_CHR.Count; i++)
            {
                m_strPCHARGEID_CHR += m_arrPCHARGEID_CHR[i] + ",";
            }
            m_strPCHARGEID_CHR = m_strPCHARGEID_CHR.TrimEnd(",".ToCharArray());
            try
            {

                strSQL = @"
                   
                          update 
                T_OPR_BIH_PATIENTCHARGE
                set
                CONFIRMERID_CHR=?,
                CONFIRMER_VCHR=?,
                CONFIRM_DAT=sysdate,
                CHARGEACTIVE_DAT=sysdate,
                PSTATUS_INT=1
                where
                NEEDCONFIRM_INT=1
                and
                PSTATUS_INT=0
                and
                ACTIVATETYPE_INT=3
                and
                PCHARGEID_CHR in ([m_strPCHARGEID_CHR])
                ";
                strSQL = strSQL.Replace("[m_strPCHARGEID_CHR]", m_strPCHARGEID_CHR.Trim());
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = CONFIRMERID_CHR;
                arrParams[1].Value = CONFIRMER_VCHR;

                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
                objHRPSvc.Dispose();


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

        #region ҽ��ִ�е���ȷ�Ϸ�������
        /// <summary>
        /// ҽ��ִ�е���ȷ�Ϸ�������
        /// </summary>
        /// <param name="m_strOrderExecuteID_Arr">ҽ��ID(���)</param>
        /// <param name="CONFIRMERID_CHR">����ύ��ID</param>
        /// <param name="CONFIRMER_VCHR">����ύ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBihOrderExecuteDenableConfirmer(string m_strOrderExecuteID_Arr, string CONFIRMERID_CHR, string CONFIRMER_VCHR)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";

            strSQL = @"
                update 
                T_OPR_BIH_ORDEREXECUTE
                set
                CONFIRMERID_CHR='[CONFIRMERID_CHR]',
                CONFIRMER_VCHR='[CONFIRMER_VCHR]',
                CONFIRM_DAT=sysdate,
                STATUS_INT=0
                where
                NEEDCONFIRM_INT=1
                and
                ORDEREXECID_CHR in ([ORDEREXECID_CHR])
                ";
            strSQL = strSQL.Replace("[CONFIRMERID_CHR]", CONFIRMERID_CHR.Trim());
            strSQL = strSQL.Replace("[CONFIRMER_VCHR]", CONFIRMER_VCHR.Trim());
            strSQL = strSQL.Replace("[ORDEREXECID_CHR]", m_strOrderExecuteID_Arr.Trim());


            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {
                    strSQL = @"
                   
                          update 
                T_OPR_BIH_PATIENTCHARGE
                set
                CONFIRMERID_CHR='[CONFIRMERID_CHR]',
                CONFIRMER_VCHR='[CONFIRMER_VCHR]',
                CONFIRM_DAT=sysdate,
                STATUS_INT=0
                where
                NEEDCONFIRM_INT=1
                and
                PSTATUS_INT=0
                
                and
                ORDEREXECID_CHR in ([ORDEREXECID_CHR])
                ";
                    strSQL = strSQL.Replace("[CONFIRMERID_CHR]", CONFIRMERID_CHR.Trim());
                    strSQL = strSQL.Replace("[CONFIRMER_VCHR]", CONFIRMER_VCHR.Trim());
                    strSQL = strSQL.Replace("[ORDEREXECID_CHR]", m_strOrderExecuteID_Arr.Trim());
                    lngRes = objHRPSvc.DoExcute(strSQL);

                }
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

        #region ��鵱ǰ�Ƿ��л�û����ȷ���Ƿ���Ҫ������� 0-�� 1-�ǵĵ�����˵���û�з��͵�ҽ�����뵥
        /// <summary>
        /// ��鵱ǰ�Ƿ��л�û����ȷ���Ƿ���Ҫ������� 0-�� 1-�ǵĵ�����˵���û�з��͵�ҽ�����뵥
        /// </summary>
        /// <param name="m_strCurrentRegisterID"></param>
        /// <param name="m_blhave"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckTheExecuteBill(string m_strCurrentRegisterID, out bool m_blhave)
        {
            long lngRes = -1;
            m_blhave = false;
            DataTable dtResult = new DataTable();
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                string strSql = @"
                   select 
            count(a.orderexecid_chr)  ExecCount      
                        from
                        T_Opr_Bih_OrderExecute a,
                        t_opr_bih_order b
                        where
                        a.orderid_chr=b.orderid_chr
                        and
                        a.NEEDCONFIRM_INT=1
                        and
                        a.ISINCEPT_INT=0
                        and
                        a.CONFIRM_DAT is not null
                        and 
                        b.registerid_chr='[registerid_chr]'
                ";
                strSql = strSql.Replace("[registerid_chr]", m_strCurrentRegisterID);
                lngRes = HRPService.DoGetDataTable(strSql, ref dtResult);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    int k = 0;
                    try
                    {
                        k = int.Parse(dtResult.Rows[0]["ExecCount"].ToString().Trim());
                    }
                    catch
                    {
                        k = 0;
                    }
                    if (k > 0)
                    {
                        m_blhave = true;
                    }

                }

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

        #region ��������ҽ��ִ��ԤԼ���¼(������뵥)
        /// <summary>
        /// ��������ҽ��ִ��ԤԼ���¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_OrderBookingVO">ҽ��ִ��ԤԼ��VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderBookingArr(clsOrderBooking[] m_arrOrderBooking)
        {
            long lngAff = 0;
            long lngRes = 0;
            string strSQL = "";

            System.Collections.Generic.List<clsOrderBooking> glstOrderBooking = new System.Collections.Generic.List<clsOrderBooking>();

            try
            {
                for (int i1 = 0; i1 < m_arrOrderBooking.Length; i1++)
                {
                    if (m_arrOrderBooking[i1].m_decBOOKID_INT != -1)
                    {
                        glstOrderBooking.Add(m_arrOrderBooking[i1]);
                    }
                }
                m_arrOrderBooking = null;

                strSQL = @"insert into t_opr_bih_order_booking
                                        (bookid_int, operate_dat, patientid_chr, registerid_chr,
                                         orderid_chr, ordername_vchr, chargeitemid_chr,
                                         chargeitemname_vchr, unit_vchr, unitprice_dec, amount_dec,
                                         createarea_chr, createrid_chr, senderid_chr, curareaid_chr,
                                         curbedid_chr, doctorid_chr, apply_type_int, bookstatus_int,
                                         applyid)
                                 values (seq_bookid.nextval, sysdate, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ? ) ";

                DbType[] dbTypes = new DbType[] {
                                               DbType.String,DbType.String,DbType.String,
                        DbType.String,DbType.String,DbType.String,DbType.String,DbType.Int32,
                        DbType.Int32,DbType.String,DbType.String,DbType.String,DbType.String,
                        DbType.String,DbType.String,DbType.String,DbType.Int32,DbType.String
                        };
                object[][] objValues = new object[18][];
                if (glstOrderBooking.Count > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[glstOrderBooking.Count];//��ʼ��
                    }
                    int n = 0;
                    for (int k1 = 0; k1 < glstOrderBooking.Count; k1++)
                    {
                        clsOrderBooking objTmp = glstOrderBooking[k1];
                        n = -1;
                        objValues[++n][k1] = objTmp.m_strPATIENTID_CHR;
                        objValues[++n][k1] = objTmp.m_strREGISTERID_CHR;
                        objValues[++n][k1] = objTmp.m_strORDERID_CHR;

                        objValues[++n][k1] = objTmp.m_strORDERNAME_VCHR;
                        objValues[++n][k1] = objTmp.m_strCHARGEITEMID_CHR;
                        objValues[++n][k1] = objTmp.m_strCHARGEITEMNAME_VCHR;
                        objValues[++n][k1] = objTmp.m_strUNIT_VCHR;
                        objValues[++n][k1] = objTmp.m_decUNITPRICE_DEC;

                        objValues[++n][k1] = objTmp.m_decAMOUNT_DEC;
                        objValues[++n][k1] = objTmp.m_strCREATEAREA_CHR;
                        objValues[++n][k1] = objTmp.m_strCREATERID_CHR;
                        objValues[++n][k1] = objTmp.m_strSENDERID_CHR;
                        objValues[++n][k1] = objTmp.m_strCURAREAID_CHR;

                        objValues[++n][k1] = objTmp.m_strCURBEDID_CHR;
                        objValues[++n][k1] = objTmp.m_strDOCTORID_CHR;
                        objValues[++n][k1] = objTmp.m_strAPPLY_TYPE_INT;
                        objValues[++n][k1] = objTmp.m_intBOOKSTATUS_INT;
                        objValues[++n][k1] = objTmp.m_strATTACHID_VCHR;

                    }
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    objHRPSvc.Dispose();
                }
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

        #region �����������뵥��Ӧ��(������뵥)
        /// <summary>
        /// �����������뵥��Ӧ��(������뵥)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_OrderBookingVO">�������뵥��Ӧ��VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderAttachRelation(clsATTACHRELATION_VO[] m_arATTACHRELATION)
        {
            long lngAff = 0;
            long lngRes = 0;
            string strSQL = "";
            System.Collections.Generic.List<clsATTACHRELATION_VO> glstRelation = new System.Collections.Generic.List<clsATTACHRELATION_VO>();

            try
            {
                for (int i1 = 0; i1 < m_arATTACHRELATION.Length; i1++)
                {
                    if (!string.IsNullOrEmpty(m_arATTACHRELATION[i1].strATTACHID_VCHR))
                    {
                        glstRelation.Add(m_arATTACHRELATION[i1]);
                    }
                }

                strSQL = @"insert into t_opr_attachrelation
                                        (attarelaid_chr, sysfrom_int, attachtype_int, sourceitemid_vchr,
                                         attachid_vchr, urgency_int, status_int, chargedetail_vchr,
                                         diagnosepart_vchr, diagnosepartid_int)
                                 values (seq_attachrelation.nextval, ?, ?, ?,
                                         ?, ?, 1, ?,
                                         ?, ?)";

                DbType[] dbTypes = new DbType[] {DbType.Int32,DbType.Int32,DbType.String,DbType.String,DbType.Int32,
                                                 DbType.String,DbType.String,DbType.Int32};
                object[][] objValues = new object[8][];
                if (glstRelation.Count > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[glstRelation.Count];//��ʼ��
                    }
                    int n = 0;
                    for (int k1 = 0; k1 < glstRelation.Count; k1++)
                    {
                        clsATTACHRELATION_VO objTmp = glstRelation[k1];
                        n = -1;

                        objValues[++n][k1] = int.Parse(objTmp.strSYSFROM_INT);
                        objValues[++n][k1] = int.Parse(objTmp.strATTACHTYPE_INT);
                        objValues[++n][k1] = objTmp.strSOURCEITEMID_VCHR;
                        objValues[++n][k1] = objTmp.strATTACHID_VCHR;
                        objValues[++n][k1] = int.Parse(objTmp.strURGENCY_INT);
                        objValues[++n][k1] = objTmp.strChargeDetail;
                        objValues[++n][k1] = objTmp.strDiagnosePart;
                        objValues[++n][k1] = objTmp.strDiagnosePartID == "" ? -1 : Convert.ToInt32(objTmp.strDiagnosePartID);
                    }

                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    objHRPSvc.Dispose();

                }
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

        #region ���ִ��һ��ҽ�� *** ҽ��ִ����ؼ�����20180320
        /// <summary>
        /// ���ִ��һ��ҽ�� *** ҽ��ִ����ؼ�����20180320
        /// </summary>
        /// <param name="m_glstExecutablePhysicianOrderList"></param>
        /// <param name="m_arrNurseVO"></param>
        /// <param name="lstPutMedCfkl">��ҩ.��ҩ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateBihOrderExecConfirmer(System.Collections.Generic.List<clsExecOrderVO> m_glstExecutablePhysicianOrderList, List<clsPatientNurseVO> m_arrNurseVO, List<EntityCureMed> lstCureMed, List<EntityCureSubStock> lstSubStock, out List<clsT_Bih_Opr_Putmeddetail_VO> lstPutMedCfkl, out string error)
        {
            long lngRes = 0;
            DateTime CreateDate = DateTime.MinValue;
            Hashtable m_htAidOrderNurse = new Hashtable();//����ҽ������ִ�е���ˮ�Ŷ�Ӧ
            ArrayList m_arrPutmeddetail_VO = new ArrayList();
            List<string> glstOrderID = new List<string>();//��ǰִ�е���ҽ���б�
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "", ORDERID_CHR = "";
            int intExecutablePhysicianOrderListCount = m_glstExecutablePhysicianOrderList.Count;
            clsExecOrderVO objOneExecutablePhysicianOrder = null;

            lstPutMedCfkl = new List<clsT_Bih_Opr_Putmeddetail_VO>();
            error = string.Empty;
            string Sql = string.Empty;
            IDataParameter[] parm = null;

            DataTable dtMedSub = new DataTable();   // �ۼ�������
            dtMedSub.Columns.Add("registerid_chr", typeof(string));
            dtMedSub.Columns.Add("drugStoreId", typeof(string));
            dtMedSub.Columns.Add("orderid_chr", typeof(string));
            dtMedSub.Columns.Add("medid_chr", typeof(string));
            dtMedSub.Columns.Add("medicinename_vchr", typeof(string));
            dtMedSub.Columns.Add("get_dec", typeof(decimal));
            dtMedSub.Columns.Add("packqty_dec", typeof(decimal));
            dtMedSub.Columns.Add("ipchargeflg_int", typeof(decimal));
            dtMedSub.Columns.Add("putmeddetailid_chr", typeof(string));

            for (int i = 0; i < intExecutablePhysicianOrderListCount; i++)
            {
                objOneExecutablePhysicianOrder = m_glstExecutablePhysicianOrderList[i];

                ORDERID_CHR = objOneExecutablePhysicianOrder.ORDERID_CHR;
                if (!glstOrderID.Contains(ORDERID_CHR))
                {
                    glstOrderID.Add(ORDERID_CHR);
                }
            }
            int intOrderID_Count = glstOrderID.Count;

            try
            {
                if (intOrderID_Count > 0)
                {
                    #region new
                    string SQL = @" select a.orderid_chr 
                                      from t_opr_bih_order a, t_opr_bih_orderexecute b
                                     where a.orderid_chr = b.orderid_chr
                                       and trunc (a.executedate_dat) = trunc (sysdate)
                                       and a.orderid_chr in ([m_strOrderids])";
                    string Tmp = "";
                    DataTable dt = new DataTable();
                    DataTable dtbOrderid = null;
                    System.Text.StringBuilder stbOrderID = new System.Text.StringBuilder(2150);//ÿ�η�100��orderid, orderid ��18���ַ�,�������źͶ��ţ�����21���ַ����ܹ�2100���ַ���������50���ַ����ܹ�2150���ַ�
                    for (int i = 0; i < intOrderID_Count; i++)
                    {
                        stbOrderID.Append("'" + glstOrderID[i] + "',");

                        if (i > 0 && i % 100 == 0)
                        {
                            stbOrderID.Remove(stbOrderID.Length - 1, 1);
                            Tmp = stbOrderID.ToString().Trim();
                            string s = SQL.Replace("[m_strOrderids]", Tmp);

                            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(s, ref dt);
                            if (dtbOrderid == null)
                            {
                                dtbOrderid = dt.Clone();
                            }
                            if (dtbOrderid != null && dt != null && dt.Rows.Count > 0)
                                dtbOrderid.Merge(dt);
                            dtbOrderid.AcceptChanges();

                            stbOrderID.Remove(0, stbOrderID.Length);
                        }
                    }
                    glstOrderID.Clear();
                    glstOrderID = null;

                    if (stbOrderID.Length > 0)
                    {
                        stbOrderID.Remove(stbOrderID.Length - 1, 1);
                        Tmp = stbOrderID.ToString().Trim();

                        string s = SQL.Replace("[m_strOrderids]", Tmp);
                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(s, ref dt);
                        if (dtbOrderid == null)
                        {
                            dtbOrderid = dt.Clone();
                        }
                        if (dtbOrderid != null && dt != null && dt.Rows.Count > 0)
                            dtbOrderid.Merge(dt);
                        dtbOrderid.AcceptChanges();
                    }
                    #endregion

                    #region ������ִ�й���ҽ������ִ��

                    System.Collections.Generic.List<string> glstAlreadyExecutedOrderID = null;

                    if (lngRes > 0 && dtbOrderid != null && dtbOrderid.Rows.Count > 0)
                    {
                        int intAlreadyExecutedOrders_Count = dtbOrderid.Rows.Count;
                        glstAlreadyExecutedOrderID = new System.Collections.Generic.List<string>(intAlreadyExecutedOrders_Count);
                        System.Data.DataRow objOneAlreadyExecutedOrderRow = null;
                        for (int i = 0; i < intAlreadyExecutedOrders_Count; i++)
                        {
                            objOneAlreadyExecutedOrderRow = dtbOrderid.Rows[i];
                            ORDERID_CHR = objOneAlreadyExecutedOrderRow["orderid_chr"].ToString();
                            if (!glstAlreadyExecutedOrderID.Contains(ORDERID_CHR))
                            {
                                glstAlreadyExecutedOrderID.Add(ORDERID_CHR);
                            }
                        }
                        glstAlreadyExecutedOrderID.TrimExcess();
                        if (glstAlreadyExecutedOrderID.Count > 0)//���ڽ�����ִ�й���ҽ�����ٽ���ִ��
                        {
                            intExecutablePhysicianOrderListCount = m_glstExecutablePhysicianOrderList.Count;
                            for (int i = 0; i < intExecutablePhysicianOrderListCount; i++)
                            {
                                ORDERID_CHR = m_glstExecutablePhysicianOrderList[i].ORDERID_CHR;
                                if (glstAlreadyExecutedOrderID.Contains(ORDERID_CHR))
                                {
                                    m_glstExecutablePhysicianOrderList.Remove(m_glstExecutablePhysicianOrderList[i]);
                                    i--;
                                    intExecutablePhysicianOrderListCount = m_glstExecutablePhysicianOrderList.Count;
                                }

                            }
                            for (int i = 0; i < m_arrNurseVO.Count; i++)
                            {
                                ORDERID_CHR = m_arrNurseVO[i].m_strORDERID_CHR;
                                if (glstAlreadyExecutedOrderID.Contains(ORDERID_CHR))
                                {
                                    m_arrNurseVO.Remove(m_arrNurseVO[i]);
                                    i--;
                                }
                            }
                        }
                    }
                    #endregion
                }

                #region ������ҽ��ִ�е���
                int n = 0;
                strSQL = @"insert into t_opr_bih_orderexecute
                                        (orderexecid_chr, orderid_chr, creatorid_chr, creator_chr,
                                         createdate_dat, executetime_int, executedays_int,
                                         executedate_vchr, ischarge_int, isincept_int, isfirst_int,
                                         isrecruit_int, status_int, needconfirm_int, exeareaid_chr,
                                         exebedid_chr, repare_int, autoid_vchr 
                                        )
                                 values (?, ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?  
                                        )";

                DbType[] dbTypes = new DbType[] {
                        DbType.String,DbType.String,DbType.String,DbType.String,DbType.Date,
                        DbType.Int32,DbType.Int32,DbType.String,DbType.Int32,DbType.Int32,
                        DbType.Int32,DbType.Int32,DbType.Int32,DbType.Int32,DbType.String,
                        DbType.String,DbType.Int32,DbType.String
                        };
                object[][] objValues = new object[18][];
                int lenCount = m_glstExecutablePhysicianOrderList.Count;
                if (lenCount > 0)
                {
                    #region ��ȡҽ��ִ�е���
                    //string strSQL2 = @"select getseq ('SEQ_ORDEREXECID', [rownum]) orderexecid_chr, sysdate createdate from dual";
                    //strSQL2 = strSQL2.Replace("[rownum]", lenCount.ToString());
                    //DataTable dtbResult2 = new DataTable();
                    //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL2, ref dtbResult2);
                    //long SEQ_ORDEREXECID = 0;
                    //string m_strSEQORDEREXECID = "";
                    //if (lngRes > 0)
                    //{
                    //    SEQ_ORDEREXECID = long.Parse(dtbResult2.Rows[0]["OrderExecID_Chr"].ToString().Trim()) - lenCount;
                    //    CreateDate = Convert.ToDateTime(dtbResult2.Rows[0]["CreateDate"].ToString().Trim());
                    //}
                    //lenCount = m_glstExecutablePhysicianOrderList.Count;
                    //for (int i = 0; i < lenCount; i++)
                    //{
                    //    SEQ_ORDEREXECID++;
                    //    m_strSEQORDEREXECID = SEQ_ORDEREXECID.ToString().PadLeft(18, '0');
                    //    m_glstExecutablePhysicianOrderList[i].ORDEREXECID_CHR = m_strSEQORDEREXECID;
                    //    if (!m_htAidOrderNurse.ContainsKey(m_glstExecutablePhysicianOrderList[i].ORDERID_CHR))
                    //    {
                    //        m_htAidOrderNurse.Add(m_glstExecutablePhysicianOrderList[i].ORDERID_CHR, m_strSEQORDEREXECID);
                    //    }
                    //    else
                    //    {
                    //        m_htAidOrderNurse[m_glstExecutablePhysicianOrderList[i].ORDERID_CHR] = m_strSEQORDEREXECID;
                    //    }
                    //}

                    CreateDate = DateTime.Now;
                    string Sql2 = @"select SEQ_ORDEREXECID.Nextval as seqId, sysdate as currTime from dual ";
                    DataTable dt2 = null;
                    weCare.Core.Dac.SqlHelper svc2 = new weCare.Core.Dac.SqlHelper(weCare.Core.Dac.EnumBiz.onlineDB);
                    for (int i = 0; i < lenCount; i++)
                    {
                        dt2 = svc2.GetDataTable(Sql2);
                        string execId = dt2.Rows[0]["seqId"].ToString().PadLeft(18, '0');
                        CreateDate = Convert.ToDateTime(dt2.Rows[0]["currTime"].ToString());
                        m_glstExecutablePhysicianOrderList[i].ORDEREXECID_CHR = execId;
                        if (!m_htAidOrderNurse.ContainsKey(m_glstExecutablePhysicianOrderList[i].ORDERID_CHR))
                        {
                            m_htAidOrderNurse.Add(m_glstExecutablePhysicianOrderList[i].ORDERID_CHR, execId);
                        }
                        else
                        {
                            m_htAidOrderNurse[m_glstExecutablePhysicianOrderList[i].ORDERID_CHR] = execId;
                        }
                    }

                    #endregion
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[lenCount];
                    }
                    clsExecOrderVO ExeOrder = null;
                    for (int k1 = 0; k1 < lenCount; k1++)
                    {
                        n = -1;
                        ExeOrder = m_glstExecutablePhysicianOrderList[k1];

                        objValues[++n][k1] = ExeOrder.ORDEREXECID_CHR;//��ˮ��
                        objValues[++n][k1] = ExeOrder.ORDERID_CHR;
                        objValues[++n][k1] = ExeOrder.CREATORID_CHR;
                        objValues[++n][k1] = ExeOrder.CREATOR_CHR;
                        objValues[++n][k1] = CreateDate;//��������

                        objValues[++n][k1] = ExeOrder.EXECUTETIME_INT;
                        objValues[++n][k1] = ExeOrder.EXECUTEDAYS_INT;
                        objValues[++n][k1] = ExeOrder.EXECUTEDATE_VCHR;
                        objValues[++n][k1] = ExeOrder.ISCHARGE_INT;
                        objValues[++n][k1] = ExeOrder.ISINCEPT_INT;

                        objValues[++n][k1] = ExeOrder.ISFIRST_INT;
                        objValues[++n][k1] = ExeOrder.ISRECRUIT_INT;
                        objValues[++n][k1] = ExeOrder.STATUS_INT;
                        objValues[++n][k1] = ExeOrder.NEEDCONFIRM_INT;
                        objValues[++n][k1] = ExeOrder.m_strEXEAREAID_CHR;

                        objValues[++n][k1] = ExeOrder.m_strEXEBEDID_CHR;
                        objValues[++n][k1] = ExeOrder.m_intRepair;
                        objValues[++n][k1] = Convert.ToString(CreateDate.Date.ToString() + ExeOrder.ORDERID_CHR.ToString() + ExeOrder.ISRECRUIT_INT.ToString() + ExeOrder.m_intRepair + ExeOrder.m_strAUTOID_VCHR).GetHashCode().ToString();
                    }
                }
                if (lenCount > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ������ҽ��������ϸ��

                #region ��ȡҽ����������ϸ��ˮ��
                lenCount = 0;
                intExecutablePhysicianOrderListCount = m_glstExecutablePhysicianOrderList.Count;

                for (int i = 0; i < intExecutablePhysicianOrderListCount; i++)
                {
                    if (m_glstExecutablePhysicianOrderList[i].m_arrPatientChareVO != null && m_glstExecutablePhysicianOrderList[i].m_arrPatientChareVO.Length > 0)
                    {
                        for (int j = 0; j < m_glstExecutablePhysicianOrderList[i].m_arrPatientChareVO.Length; j++)
                        {
                            lenCount++;
                        }
                    }
                }

                if (lenCount > 0)
                {
                    //string strSQL2 = @"select getseq ('SEQ_PCHARGEID', [rownum]) pchargeid_chr from dual";
                    //strSQL2 = strSQL2.Replace("[rownum]", lenCount.ToString());
                    //DataTable dtbResult2 = new DataTable();
                    //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL2, ref dtbResult2);
                    //long SEQ_PCHARGEID = 0;
                    //string m_strSEQ_PCHARGEID = "";
                    //if (lngRes > 0)
                    //{
                    //    SEQ_PCHARGEID = long.Parse(dtbResult2.Rows[0]["PChargeID_Chr"].ToString().Trim()) - lenCount;

                    //}

                    string Sql2 = @"select SEQ_PCHARGEID.Nextval from dual ";
                    DataTable dt2 = null;
                    weCare.Core.Dac.SqlHelper svc2 = new weCare.Core.Dac.SqlHelper(weCare.Core.Dac.EnumBiz.onlineDB);

                    intExecutablePhysicianOrderListCount = m_glstExecutablePhysicianOrderList.Count;
                    clsExecOrderVO ExecOrder = null;

                    for (int k = 0; k < intExecutablePhysicianOrderListCount; k++)
                    {
                        ExecOrder = m_glstExecutablePhysicianOrderList[k];

                        if (ExecOrder.m_arrPatientChareVO != null && ExecOrder.m_arrPatientChareVO.Length > 0)
                        {
                            for (int k1 = 0; k1 < ExecOrder.m_arrPatientChareVO.Length; k1++)
                            {
                                //SEQ_PCHARGEID++;
                                //m_strSEQ_PCHARGEID = SEQ_PCHARGEID.ToString().PadLeft(18, '0');
                                dt2 = svc2.GetDataTable(Sql2);                                
                                ExecOrder.m_arrPatientChareVO[k1].PchargeID = dt2.Rows[0][0].ToString().PadLeft(18, '0');
                                ExecOrder.m_arrPatientChareVO[k1].OrderExecID = ExecOrder.ORDEREXECID_CHR;
                                // ��ҩ��ֵ
                                if (ExecOrder.m_arrPutmeddetail_VO != null && ExecOrder.m_arrPutmeddetail_VO.Length > 0)
                                {
                                    for (int k2 = 0; k2 < ExecOrder.m_arrPutmeddetail_VO.Length; k2++)
                                    {
                                        if (((clsT_Bih_Opr_Putmeddetail_VO)ExecOrder.m_arrPutmeddetail_VO[k2]).m_strPUTMEDDETAILID_CHR.Equals(ExecOrder.m_arrPatientChareVO[k1].m_strPUTMEDREQID_CHR))
                                        {
                                            ((clsT_Bih_Opr_Putmeddetail_VO)ExecOrder.m_arrPutmeddetail_VO[k2]).m_strPCHARGEID_CHR = ExecOrder.m_arrPatientChareVO[k1].PchargeID;
                                            ((clsT_Bih_Opr_Putmeddetail_VO)ExecOrder.m_arrPutmeddetail_VO[k2]).m_strORDEREXECID_CHR = ExecOrder.m_arrPatientChareVO[k1].OrderExecID;
                                            m_arrPutmeddetail_VO.Add((clsT_Bih_Opr_Putmeddetail_VO)ExecOrder.m_arrPutmeddetail_VO[k2]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    n = 0;
                    strSQL = @"insert into t_opr_bih_patientcharge
                                        (pchargeid_chr, patientid_chr, registerid_chr, chargeactive_dat,
                                         orderid_chr, needconfirm_int, orderexectype_int,
                                         orderexecid_chr, calccateid_chr, invcateid_chr,
                                         chargeitemid_chr, chargeitemname_chr, unit_vchr, unitprice_dec,
                                         amount_dec, discount_dec, ismepay_int, createtype_int,
                                         creator_chr, create_dat, status_int, pstatus_int, clacarea_chr,
                                         createarea_chr, isrich_int, activatetype_int, curareaid_chr,
                                         curbedid_chr, insuracedesc_vchr, spec_vchr, doctorid_chr,
                                         doctor_vchr, doctorgroupid_chr, patientnurse_int, active_dat,
                                         activator_chr, totalmoney_dec, newdiscount_dec,
                                         putmedicineflag_int, chargedoctorid_chr, chargedoctor_vchr, chargedoctorgroupid_chr,itemchargetype_vchr,totaldiffcostmoney_dec, BuyPrice_dec    
                                        )
                                 values (?, ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?, ?,?,?, ? 
                                        )";

                    dbTypes = new DbType[] {
                        DbType.String,
                        DbType.String,DbType.String,DbType.Date,DbType.String,DbType.Int32,
                        DbType.Int32,DbType.String,DbType.String,DbType.String,DbType.String,
                        DbType.String,DbType.String,DbType.Decimal,DbType.Decimal,DbType.Decimal,
                        DbType.Int32,DbType.Int32,DbType.String,DbType.Date,DbType.Int32,
                        DbType.Int32,DbType.String,DbType.String,DbType.Int32,DbType.Int32,
                        DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,
                        DbType.String,DbType.String,DbType.Int32,DbType.Date,DbType.String,
                        DbType.Decimal,DbType.Decimal,DbType.Int32,DbType.String,DbType.String, DbType.String, DbType.String,
                        DbType.Decimal, DbType.Decimal  };
                    objValues = new object[45][];

                    if (lenCount > 0)
                    {
                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[lenCount];
                        }
                        int k2 = -1;
                        intExecutablePhysicianOrderListCount = m_glstExecutablePhysicianOrderList.Count;
                        DataTable dtShiying = new DataTable();
                        IDataParameter[] paraArr = null;
                        string strShiying = string.Empty;
                        string strSQLShiying = string.Empty;
                        for (int k = 0; k < intExecutablePhysicianOrderListCount; k++)
                        {
                            if (m_glstExecutablePhysicianOrderList[k].m_arrPatientChareVO != null && m_glstExecutablePhysicianOrderList[k].m_arrPatientChareVO.Length > 0)
                            {
                                for (int k1 = 0; k1 < m_glstExecutablePhysicianOrderList[k].m_arrPatientChareVO.Length; k1++)
                                {
                                    n = -1;
                                    clsBihPatientCharge_VO PatientCharge = (clsBihPatientCharge_VO)(m_glstExecutablePhysicianOrderList[k].m_arrPatientChareVO[k1]);
                                    //����ҽ����ID���շ���ĿIDȡ��Ӧ֢��
                                    strSQLShiying = @"select  t.itemchargetype_vchr from t_opr_bih_orderchargedept t where t.chargeitemid_chr = ? and t.orderid_chr = ? ";
                                    paraArr = null;
                                    objHRPSvc.CreateDatabaseParameter(2, out paraArr);
                                    paraArr[0].Value = PatientCharge.ChargeItemID;
                                    paraArr[1].Value = PatientCharge.OrderID;
                                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQLShiying, ref dtShiying, paraArr);
                                    if (lngRes > 0 & dtShiying.Rows.Count > 0)
                                    {
                                        strShiying = dtShiying.Rows[0][0].ToString();

                                    }
                                    k2++;
                                    //��ˮ��
                                    objValues[++n][k2] = PatientCharge.PchargeID;
                                    objValues[++n][k2] = PatientCharge.PatientID;
                                    objValues[++n][k2] = PatientCharge.RegisterID;
                                    if (PatientCharge.NeedConfirm == 1)
                                    {
                                        objValues[++n][k2] = null;  // CHARGEACTIVE_DAT ������Ч����
                                    }
                                    else
                                    {
                                        objValues[++n][k2] = CreateDate;    // CHARGEACTIVE_DAT ������Ч����
                                    }

                                    objValues[++n][k2] = PatientCharge.OrderID;
                                    objValues[++n][k2] = PatientCharge.NeedConfirm;

                                    objValues[++n][k2] = PatientCharge.OrderExecType;
                                    objValues[++n][k2] = PatientCharge.OrderExecID;
                                    objValues[++n][k2] = PatientCharge.CalcCateID;
                                    objValues[++n][k2] = PatientCharge.InvCateID;
                                    objValues[++n][k2] = PatientCharge.ChargeItemID;

                                    objValues[++n][k2] = PatientCharge.ChargeItemName;
                                    objValues[++n][k2] = PatientCharge.Unit;
                                    objValues[++n][k2] = PatientCharge.UnitPrice;
                                    objValues[++n][k2] = PatientCharge.Amount;
                                    objValues[++n][k2] = PatientCharge.Discount;

                                    objValues[++n][k2] = PatientCharge.Ismepay;
                                    objValues[++n][k2] = PatientCharge.CreateType;
                                    objValues[++n][k2] = PatientCharge.Creator;
                                    objValues[++n][k2] = CreateDate;
                                    objValues[++n][k2] = PatientCharge.Status;

                                    objValues[++n][k2] = PatientCharge.PStatus;
                                    objValues[++n][k2] = PatientCharge.ClacArea;
                                    objValues[++n][k2] = PatientCharge.CreateArea;
                                    objValues[++n][k2] = PatientCharge.IsRich;
                                    objValues[++n][k2] = PatientCharge.ActivateType;

                                    objValues[++n][k2] = PatientCharge.CurAreaID;
                                    objValues[++n][k2] = PatientCharge.CurBedID;
                                    objValues[++n][k2] = PatientCharge.INSURACEDESC_VCHR;
                                    objValues[++n][k2] = PatientCharge.SPEC_VCHR;
                                    objValues[++n][k2] = PatientCharge.DoctorID;

                                    objValues[++n][k2] = PatientCharge.Doctor;
                                    objValues[++n][k2] = PatientCharge.DoctorGroupID;
                                    objValues[++n][k2] = PatientCharge.PATIENTNURSE_INT;

                                    if (PatientCharge.NeedConfirm == 0)
                                    {
                                        objValues[++n][k2] = CreateDate;
                                        objValues[++n][k2] = PatientCharge.Activator;
                                    }
                                    else
                                    {
                                        objValues[++n][k2] = null;
                                        objValues[++n][k2] = null;
                                    }

                                    objValues[++n][k2] = decimal.Round(PatientCharge.UnitPrice * PatientCharge.Amount, 2);
                                    objValues[++n][k2] = PatientCharge.NEWDISCOUNT_DEC;
                                    objValues[++n][k2] = PatientCharge.m_intPUTMEDTYPE_INT;         // 0-����ҩ,1-��ҩ
                                    objValues[++n][k2] = PatientCharge.CHARGEDOCTORID_CHR;          // ����ҽ��ID
                                    objValues[++n][k2] = PatientCharge.CHARGEDOCTOR_VCHR;           // ����ҽ������
                                    objValues[++n][k2] = PatientCharge.CHARGEDOCTORGROUPID_CHR;     // ����ҽ������רҵ��
                                    objValues[++n][k2] = strShiying;
                                    objValues[++n][k2] = weCare.Core.Utils.Function.Round(PatientCharge.TotalDiffCostMoney_dec, 2);      // �������
                                    objValues[++n][k2] = (PatientCharge.TotalDiffCostMoney_dec == 0 ? 0 : PatientCharge.BuyPrice);
                                }
                            }
                        }
                    }
                }
                if (lenCount > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ��ҩ����
                dtMedSub.BeginLoadData();
                DataRow drMs = dtMedSub.NewRow();
                if (m_arrPutmeddetail_VO.Count > 0)
                {
                    strSQL = @"insert into t_bih_opr_putmeddetail
                                        (putmeddetailid_chr, areaid_chr, paientid_chr, registerid_chr,
                                         orderid_chr, orderexecid_chr, orderexectype_int, recipeno_int,
                                         dosage_dec, dosageunit_vchr, chargeitemid_chr, medid_chr,
                                         medname_vchr, isrich_int, dosetypeid_chr, execfreqid_chr,
                                         exectimes_int, execdays_int, unitprice_mny, unit_vchr, get_dec,
                                         pchargeid_chr, creator_chr, create_dat, isput_int, puttype_int,
                                         putmedreqid_chr, exectime_vchr, needconfirm_int,
                                         activatetype_int, isrecruit_int, outgetmeddays_int,
                                         medicnetype_int, putmedtype_int, bedid_chr,status_int, pretestdays, get_dec2 
                                        )
                                 values (?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?,
                                         ?, ?, ?, ?, ?,
                                         ?, ?, sysdate, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?,
                                         ?, ?, ?,1, ?, ?  
                                        )";

                    dbTypes = new DbType[] {
                            DbType.String, DbType.String, DbType.String, DbType.String, DbType.String,
                            DbType.String, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String,
                            DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.String,
                            DbType.String, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.String,
                            DbType.Decimal, DbType.String, DbType.String, DbType.Int32,
                            DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.Int32,
                            DbType.Int32, DbType.Int32,DbType.Int32,DbType.Int32,DbType.String,DbType.Int32, DbType.Decimal
                            };
                    objValues = new object[36][];
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[m_arrPutmeddetail_VO.Count];
                    }

                    // 0000260 ������ҩ��; 0000258 ������ҩ��
                    string Sql2 = @"select b.medicineid_chr as medid_chr,
                                           b.medicinename_vchr as medicinename_vchr,
                                           '' as putmeddetailid_chr,
                                           (case b.medicinetypeid_chr
                                             when '1' then
                                              '0000260'
                                             else
                                              '0000258'
                                           end) as drugStoreId,
                                           b.packqty_dec,
                                           b.ipchargeflg_int
                                      from t_bse_medicine b
                                     where b.medicineid_chr = ?";
                    DataTable dtTmpMed = null;

                    Sql = @"select lpad (seq_putmeddetail.nextval, 18, '0') from dual";
                    DataTable dtTmp = null;
                    for (int k1 = 0; k1 < m_arrPutmeddetail_VO.Count; k1++)
                    {
                        clsT_Bih_Opr_Putmeddetail_VO m_objPutmeddetail_VO = (clsT_Bih_Opr_Putmeddetail_VO)m_arrPutmeddetail_VO[k1];
                        n = -1;

                        objHRPSvc.lngGetDataTableWithoutParameters(Sql, ref dtTmp);
                        m_objPutmeddetail_VO.m_strPUTMEDDETAILID_CHR = dtTmp.Rows[0][0].ToString();
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strPUTMEDDETAILID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strAREAID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strPAIENTID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strREGISTERID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strORDERID_CHR;

                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strORDEREXECID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intORDEREXECTYPE_INT;
                        if (m_objPutmeddetail_VO.m_intRECIPENO_INT != 0)
                        {
                            objValues[++n][k1] = m_objPutmeddetail_VO.m_intRECIPENO_INT;
                        }
                        else
                        {
                            objValues[++n][k1] = null;
                        }
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_dblDOSAGE_DEC;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strDOSAGEUNIT_VCHR;

                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strCHARGEITEMID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strMEDID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strMEDNAME_VCHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intISRICH_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strDOSETYPEID_CHR;

                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strEXECFREQID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intEXECTIMES_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intEXECDAYS_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_dblUNITPRICE_MNY;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strUNIT_VCHR;

                        objValues[++n][k1] = m_objPutmeddetail_VO.m_dblGET_DEC;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strPCHARGEID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strCREATOR_CHR;
                        objValues[++n][k1] = 0; //p_objRecord.m_intISPUT_INT;

                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intPUTTYPE_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strPUTMEDREQID_CHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strEXECTIME_VCHR;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intNEEDCONFIRM_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intACTIVATETYPE_INT;

                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intISRECRUIT_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intOUTGETMEDDAYS_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intMEDICNETYPE_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_intPUTMEDTYPE_INT;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_strBedID;
                        objValues[++n][k1] = m_objPutmeddetail_VO.PretestDays;
                        objValues[++n][k1] = m_objPutmeddetail_VO.m_dblGET_DEC;

                        // ��ҩ && �䷽����
                        if (m_objPutmeddetail_VO.m_intMEDICNETYPE_INT == 2 && m_objPutmeddetail_VO.m_strMEDNAME_VCHR.IndexOf("�䷽����") >= 0)
                        {
                            lstPutMedCfkl.Add(m_objPutmeddetail_VO);
                        }

                        #region ҩƷ��Ϣ

                        objHRPSvc.CreateDatabaseParameter(1, out parm);
                        parm[0].Value = m_objPutmeddetail_VO.m_strMEDID_CHR;
                        // 2018-07-26 ��ͣ��ʿִ��ҽ��ʱ�ۼ����
                        //objHRPSvc.lngGetDataTableWithParameters(Sql2, ref dtTmpMed, parm);
                        if (dtTmpMed != null && dtTmpMed.Rows.Count > 0)
                        {
                            drMs["registerid_chr"] = m_objPutmeddetail_VO.m_strREGISTERID_CHR;
                            drMs["drugStoreId"] = dtTmpMed.Rows[0]["drugStoreId"].ToString();
                            drMs["orderid_chr"] = m_objPutmeddetail_VO.m_strORDERID_CHR;
                            drMs["medid_chr"] = dtTmpMed.Rows[0]["medid_chr"].ToString();
                            drMs["medicinename_vchr"] = dtTmpMed.Rows[0]["medicinename_vchr"].ToString();
                            drMs["get_dec"] = m_objPutmeddetail_VO.m_dblGET_DEC;
                            drMs["packqty_dec"] = dtTmpMed.Rows[0]["packqty_dec"].ToString();
                            drMs["ipchargeflg_int"] = dtTmpMed.Rows[0]["ipchargeflg_int"].ToString();
                            drMs["putmeddetailid_chr"] = m_objPutmeddetail_VO.m_strPUTMEDDETAILID_CHR;
                            dtMedSub.LoadDataRow(drMs.ItemArray, true);
                        }

                        #endregion
                    }

                    if (m_arrPutmeddetail_VO.Count > 0)
                    {
                        lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    }
                }
                dtMedSub.EndLoadData();
                #endregion

                #region ������ҽ����
                n = 0;
                strSQL = @"update t_opr_bih_order
                               set executorid_chr = ?,
                                   executor_chr = ?,
                                   executedate_dat = ?,
                                   status_int = 2
                             where orderid_chr = ?";

                dbTypes = new DbType[] {
                        DbType.String,DbType.String,DbType.Date, DbType.String

                        };
                objValues = new object[4][];
                intExecutablePhysicianOrderListCount = m_glstExecutablePhysicianOrderList.Count;
                System.Collections.Generic.List<clsExecOrderVO> glstExecOrdersToBeUpdated = new System.Collections.Generic.List<clsExecOrderVO>(intExecutablePhysicianOrderListCount);
                for (int i = 0; i < intExecutablePhysicianOrderListCount; i++)
                {
                    if (m_glstExecutablePhysicianOrderList[i].ISFIRST_INT != 1 && m_glstExecutablePhysicianOrderList[i].ISRECRUIT_INT == 0)
                    {
                        glstExecOrdersToBeUpdated.Add(m_glstExecutablePhysicianOrderList[i]);
                    }
                }

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[glstExecOrdersToBeUpdated.Count];//��ʼ��
                }

                clsExecOrderVO ExecOrder1 = null;
                for (int k1 = 0; k1 < glstExecOrdersToBeUpdated.Count; k1++)
                {
                    n = -1;
                    ExecOrder1 = glstExecOrdersToBeUpdated[k1];
                    //��ˮ��
                    objValues[++n][k1] = ExecOrder1.CREATORID_CHR;
                    objValues[++n][k1] = ExecOrder1.CREATOR_CHR;
                    objValues[++n][k1] = CreateDate;
                    objValues[++n][k1] = ExecOrder1.ORDERID_CHR;
                    //��������
                }

                if (glstExecOrdersToBeUpdated.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ������ҽ���� �״�ִ�е�
                n = 0;
                strSQL = @"update t_opr_bih_order
                               set executorid_chr = ?,
                                   executor_chr = ?,
                                   executedate_dat = ?,
                                   startdate_dat = ?,
                                   status_int = 2
                             where orderid_chr = ?";

                dbTypes = new DbType[] {
                        DbType.String,DbType.String,DbType.Date,DbType.Date,DbType.String

                        };
                objValues = new object[5][];
                glstExecOrdersToBeUpdated.Clear();
                intExecutablePhysicianOrderListCount = m_glstExecutablePhysicianOrderList.Count;

                for (int i = 0; i < intExecutablePhysicianOrderListCount; i++)
                {
                    if (m_glstExecutablePhysicianOrderList[i].ISFIRST_INT == 1 && m_glstExecutablePhysicianOrderList[i].ISRECRUIT_INT == 0)
                    {
                        glstExecOrdersToBeUpdated.Add(m_glstExecutablePhysicianOrderList[i]);
                    }
                }

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[glstExecOrdersToBeUpdated.Count];//��ʼ��
                }

                clsExecOrderVO ExecOrder2 = null;
                for (int k1 = 0; k1 < glstExecOrdersToBeUpdated.Count; k1++)
                {

                    n = -1;
                    ExecOrder2 = glstExecOrdersToBeUpdated[k1];
                    //��ˮ��
                    objValues[++n][k1] = ExecOrder2.CREATORID_CHR;
                    objValues[++n][k1] = ExecOrder2.CREATOR_CHR;
                    objValues[++n][k1] = CreateDate;
                    if (ExecOrder2.m_intOrderType == 1)
                    {
                        objValues[++n][k1] = ExecOrder2.CREATEDATE_DAT;//������ҽ�����������ֶα���ҽ�����ύʱ��
                    }
                    else
                    {
                        objValues[++n][k1] = CreateDate;
                    }
                    objValues[++n][k1] = ExecOrder2.ORDERID_CHR;
                }

                if (glstExecOrdersToBeUpdated.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region �����벡����ʳ������ʷ��¼��

                #region ����ԭ�ȼ����������
                ArrayList m_arrCare = new ArrayList();//�ȼ�����
                ArrayList m_arrCareOrder = new ArrayList();//�ȼ��������ˮ��
                ArrayList m_arrLastCareOrder = new ArrayList();//�����Ч�Ļ�������
                ArrayList m_arrEat = new ArrayList();//��ʳ����
                for (int i = 0; i < m_arrNurseVO.Count; i++)
                {
                    if (((clsPatientNurseVO)m_arrNurseVO[i]).m_intTYPE_INT == 1)
                    {
                        if (!m_arrCare.Contains(((clsPatientNurseVO)m_arrNurseVO[i]).m_strREGISTERID_CHR))
                        {
                            m_arrCare.Add(((clsPatientNurseVO)m_arrNurseVO[i]).m_strREGISTERID_CHR);
                        }
                        if (!m_arrLastCareOrder.Contains(((clsPatientNurseVO)m_arrNurseVO[i]).m_strREGISTERID_CHR))
                        {
                            if (((clsPatientNurseVO)m_arrNurseVO[i]).m_intACTIVE_INT == 1)
                            {
                                m_arrLastCareOrder.Add(((clsPatientNurseVO)m_arrNurseVO[i]));
                            }
                        }
                        m_arrCareOrder.Add(((clsPatientNurseVO)m_arrNurseVO[i]));
                    }
                    else if (((clsPatientNurseVO)m_arrNurseVO[i]).m_intTYPE_INT == 2)
                    {
                        if (!m_arrEat.Contains(((clsPatientNurseVO)m_arrNurseVO[i]).m_strREGISTERID_CHR))
                        {
                            m_arrEat.Add(((clsPatientNurseVO)m_arrNurseVO[i]).m_strREGISTERID_CHR);
                        }
                    }

                }
                n = 0;
                strSQL = @"update t_opr_bih_patientnurse
                               set active_int = 0
                             where registerid_chr = ? and type_int = 1";
                dbTypes = new DbType[] {
                              DbType.String
                             };
                objValues = new object[1][];

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrCare.Count];
                }

                for (int k1 = 0; k1 < m_arrCare.Count; k1++)
                {
                    n = -1;
                    objValues[++n][k1] = m_arrCare[k1].ToString();
                }
                if (m_arrCare.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ������ʳ����
                n = 0;
                strSQL = @"update t_opr_bih_patientnurse
                               set active_int = 0
                             where registerid_chr = ? and type_int = 2";
                dbTypes = new DbType[] {
                              DbType.String
                             };
                objValues = new object[1][];

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrEat.Count];//��ʼ��
                }

                for (int k1 = 0; k1 < m_arrEat.Count; k1++)
                {
                    n = -1;
                    objValues[++n][k1] = m_arrEat[k1].ToString();
                }
                if (m_arrEat.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ���벡����ʳ������ʷ��¼��
                n = 0;
                strSQL = @"insert into t_opr_bih_patientnurse
                                        (seqid_int, registerid_chr, type_int, orderdicid_chr, active_dat,
                                         active_int, operatorid_chr, orderexecid_chr
                                        )
                                 values (seq_patientstate.nextval, ?, ?, ?, ?,
                                         ?, ?, ?
                                        )";

                dbTypes = new DbType[] {
                         DbType.String,DbType.Int32,DbType.String,DbType.Date,DbType.Int32,DbType.String,DbType.String

                        };
                objValues = new object[7][];

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrNurseVO.Count];
                }

                for (int k1 = 0; k1 < m_arrNurseVO.Count; k1++)
                {

                    n = -1;
                    clsPatientNurseVO m_objNurseVO = (clsPatientNurseVO)m_arrNurseVO[k1];
                    m_objNurseVO.m_strORDEREXECID_CHR = m_htAidOrderNurse[m_objNurseVO.m_strORDERID_CHR].ToString();
                    //��ˮ��
                    objValues[++n][k1] = m_objNurseVO.m_strREGISTERID_CHR;
                    objValues[++n][k1] = m_objNurseVO.m_intTYPE_INT;
                    objValues[++n][k1] = m_objNurseVO.m_strORDERDICID_CHR;
                    objValues[++n][k1] = m_objNurseVO.m_dtACTIVE_DAT;
                    objValues[++n][k1] = m_objNurseVO.m_intACTIVE_INT;
                    objValues[++n][k1] = m_objNurseVO.m_strOPERATORID_CHR;
                    objValues[++n][k1] = m_objNurseVO.m_strORDEREXECID_CHR;
                }

                if (m_arrNurseVO.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ͬ����ˮ�ǼǺ�
                ArrayList m_arrCare2 = new ArrayList();
                for (int i = 0; i < m_arrNurseVO.Count; i++)
                {
                    if (((clsPatientNurseVO)m_arrNurseVO[i]).m_intTYPE_INT == 1)
                    {
                        m_arrCare2.Add(m_arrNurseVO[i]);
                    }
                }
                n = 0;
                strSQL = @"update t_opr_bih_register
                               set nursing_class = ?
                             where registerid_chr = ?";
                dbTypes = new DbType[] {
                              DbType.Int32,DbType.String
                             };
                objValues = new object[2][];

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrCare2.Count];
                }

                for (int k1 = 0; k1 < m_arrCare2.Count; k1++)
                {
                    n = -1;
                    objValues[++n][k1] = ((clsPatientNurseVO)m_arrCare2[k1]).m_intNURSING_CLASS;
                    objValues[++n][k1] = ((clsPatientNurseVO)m_arrCare2[k1]).m_strREGISTERID_CHR;
                }
                if (m_arrCare2.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }

                #endregion

                #region ���²��˷�����ϸ����������������Ϊ��Ч
                n = 0;
                strSQL = @"update t_opr_bih_patientcharge
                               set status_int = 0
                             where orderid_chr <> ?
                               and registerid_chr = ?
                               and trunc (create_dat) = trunc (sysdate)
                               and patientnurse_int = 1
                               and status_int = 1";
                dbTypes = new DbType[] {
                              DbType.String,DbType.String
                             };
                objValues = new object[2][];

                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrLastCareOrder.Count];
                }

                for (int k1 = 0; k1 < m_arrLastCareOrder.Count; k1++)
                {
                    n = -1;
                    objValues[++n][k1] = ((clsPatientNurseVO)m_arrLastCareOrder[k1]).m_strORDERID_CHR;
                    objValues[++n][k1] = ((clsPatientNurseVO)m_arrLastCareOrder[k1]).m_strREGISTERID_CHR;
                }
                if (m_arrCareOrder.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #endregion

                #region �Ƴ���ҩ
                if (lstCureMed != null && lstCureMed.Count > 0 && lstSubStock != null && lstSubStock.Count > 0)
                {
                    (new clsBIHOrderService()).SaveCureMedConfirm(lstCureMed, lstSubStock);
                }
                #endregion

                #region �ۼ����

                if (dtMedSub != null && dtMedSub.Rows.Count > 0)
                {
                    DataTable dtWmed = dtMedSub.Clone();
                    DataTable dtCmed = dtMedSub.Clone();
                    dtWmed.BeginLoadData();
                    dtCmed.BeginLoadData();
                    List<string> lstPutMedId = new List<string>();
                    Dictionary<string, string> dicMedstore = new Dictionary<string, string>();
                    foreach (DataRow dr3 in dtMedSub.Rows)
                    {
                        // 0000260 ������ҩ��; 0000258 ������ҩ��
                        if (drMs["drugStoreId"].ToString() == "0000258")
                        {
                            dtWmed.LoadDataRow(dr3.ItemArray, true);
                            dicMedstore.Add(dr3["putmeddetailid_chr"].ToString(), "0005");
                        }
                        else if (drMs["drugStoreId"].ToString() == "0000260")
                        {
                            dtCmed.LoadDataRow(dr3.ItemArray, true);
                            dicMedstore.Add(dr3["putmeddetailid_chr"].ToString(), "0002");
                        }
                        lstPutMedId.Add(dr3["putmeddetailid_chr"].ToString());
                    }
                    dtWmed.EndLoadData();
                    dtCmed.EndLoadData();
                    Hashtable hasMed = new Hashtable();
                    List<clsPutMedicineDetailGroup> lstCureMedSub = new List<clsPutMedicineDetailGroup>();
                    try
                    {
                        Dictionary<string, DataTable> dicMed = new Dictionary<string, DataTable>();
                        // ��ҩ
                        if (dtWmed.Rows.Count > 0) dicMed.Add("0000258", dtWmed);
                        // ��ҩ
                        if (dtCmed.Rows.Count > 0) dicMed.Add("0000260", dtCmed);
                        foreach (string storeid in dicMed.Keys)
                        {
                            if (JudgeHasEnoughStorage(storeid, dicMed[storeid], out error, out hasMed, out lstCureMedSub))
                            {
                                SubstractStorage(hasMed, lstCureMedSub);
                                GetPutMedicineDetailGroup(hasMed, storeid, dicMed[storeid], out lstCureMedSub);
                                AddDSRecipeAccountInfo(m_glstExecutablePhysicianOrderList[0].CREATORID_CHR, lstCureMedSub);
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        (new clsLogText()).LogError(ex);
                        return 0;
                    }
                    if (lstPutMedId.Count > 0)
                    {
                        Sql = @"update t_bih_opr_putmeddetail set medstoreid_chr = ?, isclinicsub = 1 where putmeddetailid_chr = ?";
                        dbTypes = new DbType[] { DbType.String, DbType.String };
                        objValues = new object[2][];

                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[lstPutMedId.Count];
                        }
                        for (int k1 = 0; k1 < lstPutMedId.Count; k1++)
                        {
                            objValues[0][k1] = dicMedstore[lstPutMedId[k1]];
                            objValues[1][k1] = lstPutMedId[k1];
                        }
                        objHRPSvc.m_lngSaveArrayWithParameters(Sql, objValues, dbTypes);
                    }
                }
                #endregion

                lngRes = 1;
            }
            catch (Exception ex)
            {
                string strTmp = ex.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
                lngRes = 0;
                error = ex.Message;
            }
            m_glstExecutablePhysicianOrderList = null;
            return lngRes;

        }
        #endregion

        #region JudgeHasEnoughStorage
        /// <summary>
        /// JudgeHasEnoughStorage
        /// </summary>
        /// <param name="drugStoreId"></param>
        /// <param name="dtPutMedDetail"></param>
        /// <param name="msg"></param>
        /// <param name="hasMed"></param>
        /// <param name="lstCureMedSub"></param>
        /// <returns></returns>
        [AutoComplete]
        bool JudgeHasEnoughStorage(string drugStoreId, DataTable dtPutMedDetail, out string msg, out Hashtable hasMed, out List<clsPutMedicineDetailGroup> lstCureMedSub)
        {
            //1.�԰�ҩ��ϸ��ҩƷid���з��飬ͳ�Ƹ���ҩƷʵ�ʿۼ����������
            //2.�ж��Ƿ����㹻�Ŀ����Խ��пۼ���
            long lngRes = 0;
            msg = string.Empty;
            DataTable dtTable = null;
            hasMed = new Hashtable();

            DataTable dtPatSub = new DataTable();   // �ۼ�������
            dtPatSub.Columns.Add("registerId", typeof(string));
            dtPatSub.Columns.Add("storeId", typeof(string));
            dtPatSub.Columns.Add("orderId", typeof(string));
            dtPatSub.Columns.Add("medId", typeof(string));
            dtPatSub.Columns.Add("ipAmount", typeof(double));
            dtPatSub.Columns.Add("opAmount", typeof(double));
            DataRow drPat = dtPatSub.NewRow();
            double opAmount = 0;
            double ipAmount = 0;
            lstCureMedSub = new List<clsPutMedicineDetailGroup>();

            DataRow[] drr = null;
            clsPutMedicineDetailGroup vo;
            dtPatSub.BeginLoadData();
            foreach (DataRow dr in dtPutMedDetail.Rows)
            {
                if (dr["ipchargeflg_int"].ToString() == "0")        // ������λ(��λ)
                {
                    opAmount = Convert.ToDouble(dr["get_dec"]);
                    ipAmount = Convert.ToDouble(dr["get_dec"]) * Convert.ToDouble(dr["packqty_dec"]);
                }
                else
                {
                    opAmount = Math.Round(Convert.ToDouble(dr["get_dec"]) / Convert.ToDouble(dr["packqty_dec"]), 4);
                    ipAmount = Convert.ToDouble(dr["get_dec"]);
                }

                if (hasMed.Contains(dr["medid_chr"]))
                {
                    vo = hasMed[dr["medid_chr"]] as clsPutMedicineDetailGroup;
                    vo.m_dblOPAmount += opAmount;
                    vo.m_dblIPAmount += ipAmount;
                }
                else
                {
                    vo = new clsPutMedicineDetailGroup();
                    vo.m_listSubStorageDetail = new List<clsPutMedicineDetailGroup>();
                    vo.m_strMedicineid_chr = dr["medid_chr"].ToString();
                    vo.m_strMedicineName = dr["medicinename_vchr"].ToString();
                    vo.m_strDrugStoreid = drugStoreId;
                    vo.m_dblPackage = Convert.ToDouble(dr["packqty_dec"]);
                    vo.m_dblOPAmount = opAmount;
                    vo.m_dblIPAmount = ipAmount;
                    vo.m_strPutMedDetaileId = dr["PUTMEDDETAILID_CHR"].ToString();
                    hasMed.Add(dr["medid_chr"], vo);
                }

                drPat["registerId"] = dr["registerid_chr"];
                drPat["storeId"] = drugStoreId; //dr["medstoreid_chr"]; dr["medstoreid_chr"] û��ֵ? ����
                drPat["orderId"] = dr["orderid_chr"];
                drPat["medId"] = dr["medid_chr"];
                drPat["ipAmount"] = ipAmount;
                drPat["opAmount"] = opAmount;
                string filter = "registerId = '{0}' and storeId = '{1}' and orderId = '{2}' and medId = '{3}'";
                string[] filterData = new string[4] { drPat["registerId"].ToString(), drPat["storeId"].ToString(), drPat["orderId"].ToString(), drPat["medId"].ToString() };
                drr = dtPatSub.Select(string.Format(filter, filterData));
                if (drr == null || drr.Length == 0)
                {
                    dtPatSub.LoadDataRow(drPat.ItemArray, true);
                }
                else
                {
                    drr[0]["ipAmount"] = Convert.ToDouble(drr[0]["ipAmount"]) + ipAmount;
                    drr[0]["opAmount"] = Convert.ToDouble(drr[0]["opAmount"]) + opAmount;
                    dtPatSub.AcceptChanges();
                }
            }
            dtPatSub.EndLoadData();

            try
            {
                string Sql = string.Empty;
                clsHRPTableService svc = new clsHRPTableService();
                System.Data.IDataParameter[] parm = null;

                Sql = @"select t.serno,
                               t.registerid,
                               t.orderid,
                               t.storeid,
                               t.medid,
                               t.seriesid,
                               t.ipamountreal,
                               t.opamountreal,
                               t.ipamountre,
                               t.opamountre,
                               b.ipchargeflg_int as ipchargeflag
                          from t_curemedsubtract t
                         inner join t_bse_medicine b
                            on t.medid = b.medicineid_chr
                         where t.registerid = ?
                           and t.storeid = ?
                           and t.orderid = ?
                           and t.medid = ?";

                DataTable dtPatMed = null;      // Ԥ����(�Ƴ���ҩ)
                DataTable dtPatMedTmp = null;
                foreach (DataRow dr in dtPatSub.Rows)
                {
                    svc.CreateDatabaseParameter(4, out parm);
                    parm[0].Value = dr["registerid"].ToString();
                    parm[1].Value = dr["storeid"].ToString();
                    parm[2].Value = dr["orderid"].ToString();
                    parm[3].Value = dr["medid"].ToString();
                    svc.lngGetDataTableWithParameters(Sql, ref dtPatMedTmp, parm);
                    if (dtPatMed == null) dtPatMed = dtPatMedTmp.Clone();
                    if (dtPatMed != null && dtPatMedTmp != null && dtPatMedTmp.Rows.Count > 0)
                        dtPatMed.Merge(dtPatMedTmp);
                }

                Sql = @"select a.seriesid_int,
                               a.iprealgross_int,
                               a.oprealgross_int,
                               b.assistcode_chr as medcode
                          from t_ds_storage_detail a
                         inner join t_bse_medicine b
                            on a.medicineid_chr = b.medicineid_chr
                         where a.drugstoreid_chr = ?
                           and a.medicineid_chr = ?
                         order by a.validperiod_dat";

                clsPutMedicineDetailGroup voTmp = null;
                foreach (DictionaryEntry de in hasMed)
                {
                    vo = de.Value as clsPutMedicineDetailGroup;
                    svc.CreateDatabaseParameter(2, out parm);
                    parm[0].Value = vo.m_strDrugStoreid;
                    parm[1].Value = vo.m_strMedicineid_chr;
                    lngRes = svc.lngGetDataTableWithParameters(Sql, ref dtTable, parm);
                    if (lngRes > 0 && dtTable != null && dtTable.Rows.Count > 0)
                    {
                        if (dtPatMed != null && dtPatMed.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTable.Rows)
                            {
                                drr = dtPatMed.Select("seriesid = " + dr["seriesid_int"].ToString());
                                if (drr != null && drr.Length > 0)
                                {
                                    dr["iprealgross_int"] = Convert.ToDouble(dr["iprealgross_int"]) + Convert.ToDouble(drr[0]["ipamountre"]);
                                    dr["oprealgross_int"] = Convert.ToDouble(dr["oprealgross_int"]) + Convert.ToDouble(drr[0]["opamountre"]);
                                }
                            }
                        }
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(dtTable.Rows[i]["iprealgross_int"]) < vo.m_dblIPAmount)
                            {
                                if (i == dtTable.Rows.Count - 1)
                                {
                                    msg = string.Format("ҩƷ({0})û�г�����,���ܽ�����ҩ��", dtTable.Rows[i]["medcode"].ToString() + " " + vo.m_strMedicineName);
                                    return false;
                                }
                                if (Convert.ToDouble(dtTable.Rows[i]["iprealgross_int"]) <= 0)
                                {
                                    continue;
                                }
                                vo.m_dblIPAmount -= Convert.ToDouble(dtTable.Rows[i]["iprealgross_int"]);
                                voTmp = new clsPutMedicineDetailGroup();
                                voTmp.m_lngStorageSerial = Convert.ToInt32(dtTable.Rows[i]["seriesid_int"]);
                                voTmp.m_dblIPAmount = Convert.ToDouble(dtTable.Rows[i]["iprealgross_int"]);
                                voTmp.m_dblOPAmount = Math.Round(voTmp.m_dblIPAmount / vo.m_dblPackage, 4);
                                voTmp.m_strMedicineid_chr = vo.m_strMedicineid_chr;
                                voTmp.m_strDrugStoreid = vo.m_strDrugStoreid;
                                vo.m_listSubStorageDetail.Add(voTmp);
                            }
                            else
                            {
                                voTmp = new clsPutMedicineDetailGroup();
                                voTmp.m_lngStorageSerial = Convert.ToInt32(dtTable.Rows[i]["seriesid_int"]);
                                voTmp.m_dblIPAmount = Convert.ToDouble(vo.m_dblIPAmount);
                                voTmp.m_dblOPAmount = Math.Round(vo.m_dblIPAmount / vo.m_dblPackage, 4);
                                voTmp.m_strMedicineid_chr = vo.m_strMedicineid_chr;
                                voTmp.m_strDrugStoreid = vo.m_strDrugStoreid;
                                voTmp.m_strPutMedDetaileId = vo.m_strPutMedDetaileId;
                                vo.m_listSubStorageDetail.Add(voTmp);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (dtTable == null || dtTable.Rows.Count == 0)
                        {
                            msg = string.Format("ҩƷ({0})�������κο��,���ܽ�����ҩ��", vo.m_strMedicineName);
                            return false;
                        }
                        else
                        {
                            msg = "��ȡҩƷ������ݴ���";
                            return false;
                        }
                    }
                }

                if (dtPatMed != null && dtPatMed.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dtPatSub.Rows)          // �ۼ�������
                    {
                        ipAmount = Convert.ToDouble(dr1["ipAmount"]);
                        opAmount = Convert.ToDouble(dr1["opAmount"]);
                        foreach (DataRow dr2 in dtPatMed.Rows)      // Ԥ����(�Ƴ���ҩ)
                        {
                            if (dr1["registerid"].ToString() == dr2["registerid"].ToString() && dr1["storeid"].ToString() == dr2["storeid"].ToString() &&
                                dr1["orderid"].ToString() == dr2["orderid"].ToString() && dr1["medid"].ToString() == dr2["medid"].ToString())
                            {
                                voTmp = new clsPutMedicineDetailGroup();
                                voTmp.m_lngStorageSerial = Convert.ToInt32(dr2["seriesid"]);
                                voTmp.m_strMedicineid_chr = dr2["medid"].ToString();
                                voTmp.m_strDrugStoreid = dr2["storeid"].ToString();
                                voTmp.OrderId = dr2["orderid"].ToString();
                                voTmp.CuremedSubtractSerNo = Convert.ToDecimal(dr2["serno"].ToString());       // Ԥ��ҩID
                                voTmp.IpChargeFlag = dr2["ipchargeflag"] == DBNull.Value ? 1 : Convert.ToInt32(dr2["ipchargeflag"]);  // 0 ������λ; 1 ��С��λ
                                if (Convert.ToDouble(dr2["ipamountre"]) >= ipAmount)        // Ԥ���� >= ������
                                {
                                    voTmp.m_dblIPAmount = Convert.ToDouble(ipAmount);
                                    voTmp.m_dblOPAmount = Convert.ToDouble(opAmount);
                                    lstCureMedSub.Add(voTmp);
                                    break;
                                }
                                else
                                {
                                    voTmp.m_dblIPAmount = Convert.ToDouble(dr2["ipamountre"]);
                                    voTmp.m_dblOPAmount = Convert.ToDouble(dr2["opamountre"]);
                                    lstCureMedSub.Add(voTmp);

                                    ipAmount -= Convert.ToDouble(dr2["ipamountre"]);
                                    opAmount -= Convert.ToDouble(dr2["opamountre"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                msg = objEx.Message;
            }
            return true;
        }
        #endregion

        #region �ۼ����
        /// <summary>
        /// �ۼ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="hsTable"></param>
        /// <returns></returns>
        [AutoComplete]
        long SubstractStorage(Hashtable hsTable, List<clsPutMedicineDetailGroup> lstCureMedSub)
        {
            long lngRes = 0;
            List<clsPutMedicineDetailGroup> objList = new List<clsPutMedicineDetailGroup>();
            foreach (DictionaryEntry de in hsTable)
            {
                objList.AddRange(((clsPutMedicineDetailGroup)de.Value).m_listSubStorageDetail.ToArray());
            }
            if (lstCureMedSub != null && lstCureMedSub.Count > 0)
            {
                foreach (clsPutMedicineDetailGroup vo1 in lstCureMedSub)
                {
                    foreach (clsPutMedicineDetailGroup vo2 in objList)
                    {
                        if (vo1.m_lngStorageSerial == vo2.m_lngStorageSerial)
                        {
                            vo2.m_dblIPAmount -= vo1.m_dblIPAmount;
                            vo2.m_dblOPAmount -= vo1.m_dblOPAmount;
                        }
                    }
                }
            }

            try
            {
                string Sql = string.Empty;
                DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Double, DbType.Double, DbType.Int64 };
                object[][] objValues = new object[5][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[objList.Count];//��ʼ��
                }

                for (int k = 0; k < objList.Count; k++)
                {
                    objValues[0][k] = objList[k].m_dblIPAmount;
                    objValues[1][k] = objList[k].m_dblIPAmount;
                    objValues[2][k] = objList[k].m_dblOPAmount;
                    objValues[3][k] = objList[k].m_dblOPAmount;
                    objValues[4][k] = objList[k].m_lngStorageSerial;

                }
                Sql = @"update t_ds_storage_detail a
                               set a.iprealgross_int      = a.iprealgross_int - ?,
                                   a.ipavailablegross_num = a.ipavailablegross_num - ?,
                                   a.oprealgross_int      = a.oprealgross_int - ?,
                                   a.opavailablegross_num = a.opavailablegross_num - ?
                             where a.seriesid_int = ?";

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                long lngRecEff = -1;
                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                if (lngRecEff <= 0)
                    throw new Exception("�ۼ�������ӦҩƷ�����ϸ��");

                dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.String, DbType.String };
                objValues = new object[4][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[objList.Count];
                }
                for (int k = 0; k < objList.Count; k++)
                {
                    objValues[0][k] = objList[k].m_dblIPAmount;
                    objValues[1][k] = objList[k].m_dblOPAmount;
                    objValues[2][k] = objList[k].m_strDrugStoreid;
                    objValues[3][k] = objList[k].m_strMedicineid_chr;
                }

                Sql = @"update t_ds_storage a
                           set a.ipcurrentgross_num = a.ipcurrentgross_num - ?,
                               a.opcurrentgross_num = a.opcurrentgross_num - ?
                         where a.drugstoreid_chr = ?
                           and a.medicineid_chr = ?";

                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                if (lngRecEff <= 0)
                    throw new Exception("�ۼ�������ӦҩƷ�����棡");

                if (lstCureMedSub != null && lstCureMedSub.Count > 0)
                {
                    Sql = @"update t_curemedsubtract
                               set ipamountre = ipamountre - ?, opamountre = opamountre - ?
                             where serno = ?";

                    dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Decimal };
                    objValues = new object[3][];
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[lstCureMedSub.Count];
                    }
                    for (int k = 0; k < lstCureMedSub.Count; k++)
                    {
                        objValues[0][k] = lstCureMedSub[k].m_dblIPAmount;
                        objValues[1][k] = lstCureMedSub[k].m_dblOPAmount;
                        objValues[2][k] = lstCureMedSub[k].CuremedSubtractSerNo;
                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                    if (lngRecEff <= 0)
                        throw new Exception("����t_curemedsubtractʧ��");

                    Sql = @"update t_opr_bih_order 
                               set preamount2 = preamount2 - ? 
                             where orderid_chr = ?";

                    dbTypes = new DbType[] { DbType.Double, DbType.String };
                    objValues = new object[2][];
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[lstCureMedSub.Count];
                    }
                    for (int k = 0; k < lstCureMedSub.Count; k++)
                    {
                        objValues[0][k] = (lstCureMedSub[k].IpChargeFlag == 0 ? lstCureMedSub[k].m_dblOPAmount : lstCureMedSub[k].m_dblIPAmount);
                        objValues[1][k] = lstCureMedSub[k].OrderId;
                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                    if (lngRecEff <= 0)
                        throw new Exception("����t_opr_bih_orderʧ��");

                }
                objHRPSvc.Dispose();
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

        #region ��ȡ��ҩ��ϸ��Ϣ
        /// <summary>
        /// ��ȡ��ҩ��ϸ��Ϣ
        /// </summary>
        /// <param name="hsTable"></param>
        /// <param name="p_strDrugStoreid">ҩ��ID</param>
        /// <param name="p_dtbPutMed">��ҩ��Ϣ</param>
        /// <param name="objList">��ҩ��ϸ��ˮ��Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        long GetPutMedicineDetailGroup(Hashtable hsTable, string p_strDrugStoreid, DataTable p_dtbPutMed, out List<clsPutMedicineDetailGroup> objList)
        {
            long lngRes = 0;
            string strSeri = "";
            objList = new List<clsPutMedicineDetailGroup>();
            List<long> lstSeri = new List<long>();
            Dictionary<string, List<double>> p_dtnGross = new Dictionary<string, List<double>>();
            string strSQL = @"select a.seriesid_int, a.iprealgross_int, a.oprealgross_int,a.drugstoreid_chr,a.medicineid_chr
                                  from t_ds_storage_detail a
                                 where  a.seriesid_int in ([seri])
                                 order by a.validperiod_dat";
            clsHRPTableService objHRPSvc = null;

            try
            {
                int intRowscount = p_dtbPutMed.Rows.Count;
                clsPutMedicineDetailGroup objVo = null;
                DataRow dr2 = null;
                for (int i1 = 0; i1 < intRowscount; i1++)//����ҩ��ϸ��Ϣ��ֵ����ҩ��ˮ����ϸ����ϢVO
                {
                    dr2 = p_dtbPutMed.Rows[i1];
                    objVo = new clsPutMedicineDetailGroup();


                    objVo.m_strMedicineid_chr = dr2["medid_chr"].ToString();
                    objVo.m_strMedicineName = dr2["medicinename_vchr"].ToString();
                    objVo.m_strDrugStoreid = p_strDrugStoreid;
                    objVo.m_dblPackage = Convert.ToDouble(dr2["packqty_dec"]);
                    if (dr2["ipchargeflg_int"].ToString() == "0")
                    {
                        objVo.m_dblOPAmount = Convert.ToDouble(dr2["get_dec"]);
                        objVo.m_dblIPAmount = Convert.ToDouble(dr2["get_dec"]) * Convert.ToDouble(dr2["packqty_dec"]);
                    }
                    else
                    {
                        objVo.m_dblOPAmount = Math.Round(Convert.ToDouble(dr2["get_dec"]) / Convert.ToDouble(dr2["packqty_dec"]), 4);
                        objVo.m_dblIPAmount = Convert.ToDouble(dr2["get_dec"]);
                    }
                    objVo.m_strPutMedDetaileId = dr2["PUTMEDDETAILID_CHR"].ToString();

                    objList.Add(objVo);
                }
                foreach (DictionaryEntry de in hsTable)//��ȡ��Ҫ�ۼ����ҩƷ����������
                {
                    for (int i = 0; i < ((clsPutMedicineDetailGroup)de.Value).m_listSubStorageDetail.Count; i++)
                    {
                        if (!lstSeri.Contains(((clsPutMedicineDetailGroup)de.Value).m_listSubStorageDetail[i].m_lngStorageSerial))
                        {
                            lstSeri.Add(((clsPutMedicineDetailGroup)de.Value).m_listSubStorageDetail[i].m_lngStorageSerial);
                        }
                    }
                }
                if (lstSeri.Count > 1000)
                {
                    int intArrayCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lstSeri.Count / 1000.00)));
                    string[] strSeriArr = new string[intArrayCount];
                    for (int i3 = 0; i3 < lstSeri.Count; i3++)
                    {
                        strSeriArr[Convert.ToInt32(Math.Floor(Convert.ToDouble(i3 / 1000)))] += lstSeri[i3].ToString() + ",";
                    }
                    for (int i = 0; i < strSeriArr.Length; i++)
                    {
                        strSeriArr[i] = strSeriArr[i].TrimEnd(',');
                    }
                    string strTemp = string.Empty;
                    foreach (string str in strSeriArr)
                    {
                        strTemp += " a.seriesid_int in (" + str + ") or ";
                    }
                    strTemp = strTemp.Substring(0, strTemp.Length - 3);
                    strSQL = strSQL.Replace("a.seriesid_int in ([seri])", strTemp);
                }
                else
                {
                    for (int i3 = 0; i3 < lstSeri.Count; i3++)
                    {
                        strSeri += lstSeri[i3].ToString() + ",";
                    }
                    strSeri = strSeri.TrimEnd(',');
                    strSQL = strSQL.Replace("[seri]", strSeri);
                }
                DataTable dtbResult = new DataTable();
                objHRPSvc = new clsHRPTableService();

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)//����ÿ����ҩ��ϸҩƷ�ۼ������ʣ�µĿ�������Լ���¼����
                {
                    DataRow[] drArr = null;
                    int intlstCount = objList.Count;
                    for (int i4 = 0; i4 < intlstCount; i4++)
                    {
                        drArr = dtbResult.Select("medicineid_chr='" + objList[i4].m_strMedicineid_chr + "'");
                        double m_dblIPAmount = objList[i4].m_dblIPAmount;
                        double m_dblOPAmount = objList[i4].m_dblOPAmount;
                        for (int i5 = 0; i5 < drArr.Length; i5++)
                        {
                            double dblIpGross = Convert.ToDouble(drArr[i5]["iprealgross_int"].ToString());
                            double dblOpGross = Convert.ToDouble(drArr[i5]["oprealgross_int"].ToString());

                            if (m_dblIPAmount <= dblIpGross && m_dblIPAmount > 0)
                            {
                                drArr[i5]["iprealgross_int"] = Convert.ToString(dblIpGross - m_dblIPAmount);
                                drArr[i5]["oprealgross_int"] = Convert.ToString(dblOpGross - m_dblOPAmount);
                                objList[i4].m_dblIPDownGross = Convert.ToDouble(drArr[i5]["iprealgross_int"].ToString());
                                objList[i4].m_dblOPDownGross = Convert.ToDouble(drArr[i5]["oprealgross_int"].ToString());
                                objList[i4].m_lngStorageSerial = Convert.ToInt32(drArr[i5]["seriesid_int"].ToString());
                                break;
                            }
                            else if (m_dblIPAmount > dblIpGross && m_dblIPAmount > 0)
                            {
                                m_dblIPAmount = m_dblIPAmount - dblIpGross;
                                m_dblOPAmount = m_dblOPAmount - dblOpGross;
                                drArr[i5]["iprealgross_int"] = Convert.ToString(0);
                                drArr[i5]["oprealgross_int"] = Convert.ToString(0);
                                objList[i4].m_dblIPDownGross = Convert.ToDouble(drArr[i5]["iprealgross_int"].ToString());
                                objList[i4].m_dblOPDownGross = Convert.ToDouble(drArr[i5]["oprealgross_int"].ToString());
                                objList[i4].m_lngStorageSerial = Convert.ToInt32(drArr[i5]["seriesid_int"].ToString());
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
                strSQL = null;
            }
            return lngRes;
        }
        #endregion

        #region �����ҩ��ϸ�����ˮ��
        /// <summary>
        /// �����ҩ��ϸ�����ˮ��
        /// </summary>
        /// <param name="m_strOperatorid"></param>
        /// <param name="objList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long AddDSRecipeAccountInfo(string m_strOperatorid, List<clsPutMedicineDetailGroup> objList)
        {
            long lngRes = -1;
            string strSQL = string.Empty;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            strSQL = @" insert into t_ds_putmedaccount_detail a
                              (seriesid_int,
                               medicineid_chr,
                               medicinename_vchr,
                               medicinetypeid_chr,
                               medspec_vchr,
                               packqty_dec,
                               drugstoreid_int,
                               lotno_vchr,
                               validperiod_dat,
                               ipretailprice_int,
                               opretailprice_int,
                               ipunit_chr,
                               ipamount_int,
                               opamount_int,
                               opunit_chr,
                               ipoldgross_int,
                               opoldgross_int,
                               type_int,
                               state_int,
                               isend_int,
                               endipamount_int,
                               endopamount_int,
                               endipretailprice_int,
                               endopretailprice_int,
                               inaccountid_chr,
                               inaccountdate_dat,
                               accountid_chr,
                               productorid_chr,
                               operatedate_dat,
                               putmeddetailid_chr,
                               medseriesid_int,
                               operatorid_chr,
                               ipavaigross_int,
                               opavaigross_int,deptid_chr)
                              select seq_ds_putmedaccount_detail.nextval,
                                     b.medicineid_chr,
                                     b.medicinename_vchr,
                                     c.medicinetypeid_chr,
                                     b.medspec_vchr,
                                     b.packqty_dec,
                                     b.drugstoreid_chr,
                                     b.lotno_vchr,
                                     b.validperiod_dat,
                                     b.ipretailprice_int,
                                     b.opretailprice_int,
                                     b.ipunit_chr,
                                     ?,
                                     ?,
                                     b.opunit_chr,
                                     ?,
                                     ?,
                                     ?,
                                     1,
                                     0,
                                     null,
                                     null,
                                     null,
                                     null,
                                     ?,
                                     sysdate,
                                     null,
                                     b.productorid_chr,
                                     sysdate,
                                     ?,
                                     b.seriesid_int,
                                     ?,
                                     b.ipavailablegross_num,
                                     b.opavailablegross_num,?
                                from t_ds_storage_detail b, t_bse_medicine c
                               where b.seriesid_int = ?
                                 and b.medicineid_chr = c.medicineid_chr(+)";
            DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Double, DbType.Double, DbType.Int16, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int64 };
            object[][] objValuesArr = new object[10][];
            object[][] objValuesArr1 = new object[10][];
            int m_intCount = objList.Count;
            for (int j = 0; j < objValuesArr.Length; j++)//��ʼ������
            {
                objValuesArr[j] = new object[m_intCount];
            }
            clsPutMedicineDetailGroup m_objTempVo = null;
            clsPutMedicineDetailGroup m_objTempVo1 = null;
            long lngAffected = -1;
            long lngAffected1 = -1;
            // ���ORA-01795����
            if (m_intCount >= 1000)
            {
                List<clsPutMedicineDetailGroup> objList1 = new List<clsPutMedicineDetailGroup>();
                List<clsPutMedicineDetailGroup> objList2 = new List<clsPutMedicineDetailGroup>();
                for (int k = 0; k < m_intCount; k++)
                {
                    if (k < 1000)
                    {
                        objList1.Add(objList[k]);
                    }
                    else
                    {
                        objList2.Add(objList[k]);
                    }
                }
                for (int j = 0; j < objList1.Count; j++)
                {
                    m_objTempVo = objList1[j];
                    objValuesArr[0][j] = m_objTempVo.m_dblIPAmount;
                    objValuesArr[1][j] = m_objTempVo.m_dblOPAmount;
                    objValuesArr[2][j] = m_objTempVo.m_dblIPDownGross;
                    objValuesArr[3][j] = m_objTempVo.m_dblOPDownGross;
                    objValuesArr[4][j] = 1;
                    objValuesArr[5][j] = m_strOperatorid;
                    objValuesArr[6][j] = m_objTempVo.m_strPutMedDetaileId;
                    objValuesArr[7][j] = m_strOperatorid;
                    objValuesArr[8][j] = m_objTempVo.m_strDrugStoreid;
                    objValuesArr[9][j] = m_objTempVo.m_lngStorageSerial;
                }
                for (int m = 0; m < objList2.Count; m++)
                {
                    m_objTempVo1 = objList1[m];
                    objValuesArr1[0][m] = m_objTempVo1.m_dblIPAmount;
                    objValuesArr1[1][m] = m_objTempVo1.m_dblOPAmount;
                    objValuesArr1[2][m] = m_objTempVo1.m_dblIPDownGross;
                    objValuesArr1[3][m] = m_objTempVo1.m_dblOPDownGross;
                    objValuesArr1[4][m] = 1;
                    objValuesArr1[5][m] = m_strOperatorid;
                    objValuesArr1[6][m] = m_objTempVo1.m_strPutMedDetaileId;
                    objValuesArr1[7][m] = m_strOperatorid;
                    objValuesArr1[8][m] = m_objTempVo1.m_strDrugStoreid;
                    objValuesArr1[9][m] = m_objTempVo1.m_lngStorageSerial;
                }
                try
                {
                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValuesArr, ref lngAffected, dbTypes);
                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValuesArr1, ref lngAffected1, dbTypes);
                    if (lngAffected != objList1.Count && lngAffected1 != objList2.Count)
                    {
                        lngRes = -1;
                        ContextUtil.SetAbort();
                    }
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else
            {
                for (int i = 0; i < m_intCount; i++)
                {
                    m_objTempVo = objList[i];
                    objValuesArr[0][i] = m_objTempVo.m_dblIPAmount;
                    objValuesArr[1][i] = m_objTempVo.m_dblOPAmount;
                    objValuesArr[2][i] = m_objTempVo.m_dblIPDownGross;
                    objValuesArr[3][i] = m_objTempVo.m_dblOPDownGross;
                    objValuesArr[4][i] = 1;
                    objValuesArr[5][i] = m_strOperatorid;
                    objValuesArr[6][i] = m_objTempVo.m_strPutMedDetaileId;
                    objValuesArr[7][i] = m_strOperatorid;
                    objValuesArr[8][i] = m_objTempVo.m_strDrugStoreid;
                    objValuesArr[9][i] = m_objTempVo.m_lngStorageSerial;
                }
                try
                {
                    lngRes = objHRPServ.m_lngSaveArrayWithParametersWithAffected(strSQL, objValuesArr, ref lngAffected, dbTypes);
                    if (lngAffected != m_intCount)
                    {
                        lngRes = -1;
                        ContextUtil.SetAbort();
                    }
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            return lngRes;
        }
        #endregion

        #region ����ҽ��ִ��ԤԼ���¼(������뵥)
        /// <summary>
        /// ����ҽ��ִ��ԤԼ���¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_OrderBookingVO">ҽ��ִ��ԤԼ��VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderBooking(clsOrderBooking m_OrderBookingVO)
        {
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";

            strSQL = @"insert into t_opr_bih_order_booking
                                    (bookid_int, operate_dat, patientid_chr, registerid_chr,
                                     orderid_chr, ordername_vchr, chargeitemid_chr,
                                     chargeitemname_vchr, unit_vchr, unitprice_dec, amount_dec,
                                     createarea_chr, createrid_chr, senderid_chr, curareaid_chr,
                                     curbedid_chr, doctorid_chr, apply_type_int, bookstatus_int
                                    )
                             values (seq_bookid.nextval, sysdate, ?, ?,
                                     ?, ?, ?,
                                     ?, ?, ?, ?,
                                     ?, ?, ?, ?,
                                     ?, ?, ?, ?
                                    )";

            try
            {
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(17, out arrParams);
                int n = -1;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strPATIENTID_CHR;//��ˮ��
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strREGISTERID_CHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strORDERID_CHR;

                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strORDERNAME_VCHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strCHARGEITEMID_CHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strCHARGEITEMNAME_VCHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strUNIT_VCHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_decUNITPRICE_DEC;

                n++;
                arrParams[n].Value = m_OrderBookingVO.m_decAMOUNT_DEC;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strCREATEAREA_CHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strCREATERID_CHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strSENDERID_CHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strCURAREAID_CHR;

                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strCURBEDID_CHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strDOCTORID_CHR;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_strAPPLY_TYPE_INT;
                n++;
                arrParams[n].Value = m_OrderBookingVO.m_intBOOKSTATUS_INT;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);

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

        #region ��ȡҽ��	���ݵ�ǰ���� --ҽ��ִ��������ʹ�� �ֱ�õ�ҽ����Ϣ,������Ϣ,������Ϣ    --> 2018-03-08 �÷�������
        /// <summary>
        /// ͨ������ID���ҳ��ò�����Ҫִ�е�ҽ��
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExecOrderDTByArea(string m_strAreaid_chr, out DataTable m_dtExecOrder)
        {
            long lngRes = -1;
            string SQL = "";

            m_dtExecOrder = new DataTable();

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                #region 1
                SQL = @"select /*+ all_rows*/
                                a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr, a.executetype_int,
                                a.recipeno_int, a.name_vchr, a.spec_vchr, a.execfreqid_chr, a.dosage_dec,
                                a.execfreqname_chr, a.dosageunit_chr, a.get_dec, a.useunit_chr, a.getunit_chr,
                                a.dosetypeid_chr, a.dosetypename_chr, a.startdate_dat, a.finishdate_dat, a.execdeptid_chr,
                                a.execdeptname_chr, a.entrust_vchr, a.parentid_chr, a.status_int, a.creatorid_chr,
                                a.creator_chr, a.createdate_dat, a.posterid_chr, a.poster_chr, a.postdate_dat,
                                a.executorid_chr, a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                                a.stopdate_dat, a.retractorid_chr, a.retractor_chr, a.retractdate_dat, a.isrich_int,
                                a.ratetype_int, a.isrepare_int, a.use_dec, a.isneedfeel, a.outgetmeddays_int,
                                a.assessoridforexec_chr, a.assessorforexec_chr, a.assessorforexec_dat, a.assessoridforstop_chr, a.assessorforstop_chr,
                                a.assessorforstop_dat, a.backreason, a.sendbackid_chr, a.sendbacker_chr, a.sendback_dat,
                                a.isyb_int, a.sampleid_vchr, a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                                a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr, a.confirmer_vchr, a.confirm_dat,
                                a.attachtimes_int, a.doctorid_chr, a.doctor_vchr, a.curareaid_chr, a.curbedid_chr,
                                a.doctorgroupid_chr, a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                                a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr, a.feel_int,
                                a.charge_int, a.type_int, a.singleamount_dec, a.sourcetype_int, b.areaid_chr,
                                b.patientid_chr, c.lastname_vchr, c.registerid_chr, c.sex_chr, d.bedid_chr,
                                d.code_chr, f.ordercateid_chr, f.viewname_vchr, sysdate as today, g.days_int,
                                g.lexectime_vchr, g.texectime_vchr, g.times_int, e.lisapplyunitid_chr, e.applytypeid_chr,
                                k.deptname_vchr, m.sample_type_desc_vchr, n.partname, a.chargedoctorgroupid_chr, a.pretestdays,
                                a.preamount2, a.curedays, a.checkstate, p3.flaga_int as medproperty
                              from t_opr_bih_order a
                             inner join t_opr_bih_register b
                                on a.registerid_chr = b.registerid_chr
                             inner join t_opr_bih_registerdetail c
                                on b.registerid_chr = c.registerid_chr
                              left join t_bse_bed d
                                on b.bedid_chr = d.bedid_chr
                              left join t_bse_bih_orderdic e
                                on a.orderdicid_chr = e.orderdicid_chr
                              left join t_aid_bih_ordercate f
                                on e.ordercateid_chr = f.ordercateid_chr
                              left join t_aid_recipefreq g
                                on a.execfreqid_chr = g.freqid_chr
                              left join t_bse_deptdesc k
                                on a.curareaid_chr = k.deptid_chr
                              left join t_aid_lis_sampletype m
                                on a.sampleid_vchr = m.sample_type_id_chr
                              left join ar_apply_partlist n
                                on a.partid_vchr = n.partid
                              left join t_bse_chargeitem p1
                                on e.itemid_chr = p1.itemid_chr
                              left join t_bse_medicine p2
                                on p1.itemsrcid_vchr = p2.medicineid_chr
                              left join t_aid_medicinepreptype p3
                                on p2.medicinepreptype_chr = p3.medicinepreptype_chr
                             where (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                               and (a.executedate_dat < trunc(sysdate) or a.executedate_dat is null)
                               and (a.status_int = 5 or (a.status_int = 2 and a.executetype_int = 1))
                               and a.sourcetype_int = 0
                               and a.curareaid_chr = ?
                               and b.pstatus_int in (0, 1, 4)
                               and b.status_int = 1
                               and b.areaid_chr = ?
                            ";
                // -- and (a.curedays is null or a.curedays = 0 or (a.curedays > 0 and a.checkstate = 1))

                HRPService.CreateDatabaseParameter(2, out arrParams);
                arrParams[0].Value = m_strAreaid_chr;
                arrParams[1].Value = m_strAreaid_chr;

                lngRes = HRPService.lngGetDataTableWithParameters(SQL, ref m_dtExecOrder, arrParams); //new code

                #endregion

                #region 2
                SQL = @"select /*+ all_rows*/
                                a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr, a.executetype_int,
                                a.recipeno_int, a.name_vchr, a.spec_vchr, a.execfreqid_chr, a.dosage_dec,
                                a.execfreqname_chr, a.dosageunit_chr, a.get_dec, a.useunit_chr, a.getunit_chr,
                                a.dosetypeid_chr, a.dosetypename_chr, a.startdate_dat, a.finishdate_dat, a.execdeptid_chr,
                                a.execdeptname_chr, a.entrust_vchr, a.parentid_chr, a.status_int, a.creatorid_chr,
                                a.creator_chr, a.createdate_dat, a.posterid_chr, a.poster_chr, a.postdate_dat,
                                a.executorid_chr, a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                                a.stopdate_dat, a.retractorid_chr, a.retractor_chr, a.retractdate_dat, a.isrich_int,
                                a.ratetype_int, a.isrepare_int, a.use_dec, a.isneedfeel, a.outgetmeddays_int,
                                a.assessoridforexec_chr, a.assessorforexec_chr, a.assessorforexec_dat, a.assessoridforstop_chr, a.assessorforstop_chr,
                                a.assessorforstop_dat, a.backreason, a.sendbackid_chr, a.sendbacker_chr, a.sendback_dat,
                                a.isyb_int, a.sampleid_vchr, a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                                a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr, a.confirmer_vchr, a.confirm_dat,
                                a.attachtimes_int, a.doctorid_chr, a.doctor_vchr, a.curareaid_chr, a.curbedid_chr,
                                a.doctorgroupid_chr, a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                                a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr, a.feel_int,
                                a.charge_int, a.type_int, a.singleamount_dec, a.sourcetype_int, b.areaid_chr,
                                b.patientid_chr, c.lastname_vchr, c.registerid_chr, c.sex_chr, d.bedid_chr,
                                d.code_chr, f.ordercateid_chr, f.viewname_vchr, sysdate today, g.days_int,
                                g.lexectime_vchr, g.texectime_vchr, g.times_int, e.lisapplyunitid_chr, e.applytypeid_chr,
                                k.deptname_vchr, m.sample_type_desc_vchr, n.partname, a.chargedoctorgroupid_chr, a.pretestdays,
                                a.preamount2, a.curedays, a.checkstate, p3.flaga_int as medproperty
                              from t_opr_bih_order a
                             inner join t_opr_bih_register b
                                on a.registerid_chr = b.registerid_chr
                             inner join t_opr_bih_registerdetail c
                                on b.registerid_chr = c.registerid_chr
                              left join t_bse_bed d
                                on b.bedid_chr = d.bedid_chr
                              left join t_bse_bih_orderdic e
                                on a.orderdicid_chr = e.orderdicid_chr
                              left join t_aid_bih_ordercate f
                                on e.ordercateid_chr = f.ordercateid_chr
                              left join t_aid_recipefreq g
                                on a.execfreqid_chr = g.freqid_chr
                              left join t_bse_deptdesc k
                                on a.curareaid_chr = k.deptid_chr
                              left join t_aid_lis_sampletype m
                                on a.sampleid_vchr = m.sample_type_id_chr
                              left join ar_apply_partlist n
                                on a.partid_vchr = n.partid
                              left join t_bse_chargeitem p1
                                on e.itemid_chr = p1.itemid_chr
                              left join t_bse_medicine p2
                                on p1.itemsrcid_vchr = p2.medicineid_chr
                              left join t_aid_medicinepreptype p3
                                on p2.medicinepreptype_chr = p3.medicinepreptype_chr
                             where (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                               and (a.executedate_dat < trunc(sysdate) or a.executedate_dat is null)
                               and (a.status_int = 5 or (a.status_int = 2 and a.executetype_int = 1))
                               and b.pstatus_int in (0, 1, 4)
                               and b.status_int = 1
                               and a.sourcetype_int = 1
                               and a.createareaid_chr = ?                          
                             ";
                //   -- and (a.curedays is null or a.curedays = 0 or (a.curedays > 0 and a.checkstate = 1)) 

                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr;

                DataTable dt = new DataTable();
                lngRes = HRPService.lngGetDataTableWithParameters(SQL, ref dt, arrParams);
                if (m_dtExecOrder != null && dt != null && dt.Rows.Count > 0)
                    m_dtExecOrder.Merge(dt);
                m_dtExecOrder.AcceptChanges();
                dt = null;
                #endregion
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetPatientDTByArea(string m_strAreaid_chr, out DataTable m_dtPatients)
        {
            long lngRes = -1;

            m_dtPatients = new DataTable();

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                //���˱�
                string strSql = @"select distinct a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                                            a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                                            a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                                            b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                                            c.code_chr bedname, d.deptname_vchr areaname, a.limitrate_mny,
                                            e.flgname_vchr as state, mzdiagnose_vchr,
                                            f.paytypename_vchr paytypename_vchr, sysdate today,
                                            a1.remarkname_vchr, a.des_vchr, a1.status_int remark_int,
                                            a1.chargectl_int, a2.deptname_vchr deptname,
                                            a1.seq_int remark_no, a.insuredsum_mny, k.patientcardid_chr
                                       from t_opr_bih_register a,
                                            t_opr_bih_registerdetail b,
                                            t_bse_bed c,
                                            t_bse_deptdesc d,
                                            (select flg_int, flgname_vchr
                                               from t_sys_flg_table
                                              where tablename_vchr = 't_opr_bih_register'
                                                and columnname_vchr = 'PSTATUS_INT') e,
                                            (select tf.paytypename_vchr, tf.paytypeid_chr
                                               from t_bse_patientpaytype tf
                                              where tf.isusing_num = 1 and tf.payflag_dec <> 1) f,
                                            t_opr_bih_patspecremark a1,
                                            t_bse_deptdesc a2,
                                            (select patientid_chr, patientcardid_chr from t_bse_patientcard where status_int = 1 or status_int = 3) k  
                                      where a.registerid_chr = b.registerid_chr(+)
                                        and a.patientid_chr = k.patientid_chr(+)                                                          
                                        and a.registerid_chr = c.bihregisterid_chr(+)
                                        and a.areaid_chr = d.deptid_chr(+)
                                        and a.pstatus_int = e.flg_int(+)
                                        and a.paytypeid_chr = f.paytypeid_chr(+)
                                        and a.registerid_chr = a1.registerid_chr(+)
                                        and a.deptid_chr = a2.deptid_chr(+)
                                        and a.status_int = 1 
                                        and a.pstatus_int <> 3
                                        and a.areaid_chr = ?";

                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr;
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtPatients, arrParams);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        //������һ��������õ�û���ظ�������ˮ�ŵĲ��˺����飬��ȡ���������Ϣ����Щ���˾���ҽ��������Ҫִ��
        //ԭ���Ĵ��룬��Ҫ���� public long m_lngGetPatientDTByArea(ArrayList m_arrRegisterid_chr, out DataTable m_dtPatients)
        [AutoComplete]
        public long m_lngGetPatientDTByArea(System.Collections.Generic.List<string> m_glstRegisterid_chr, out DataTable m_dtPatients)
        {
            long lngRes = -1;
            string m_strRegisterid_chr = "";
            m_dtPatients = null;

            int intRegisterID_Count = 0;
            if (m_glstRegisterid_chr != null)
            {
                intRegisterID_Count = m_glstRegisterid_chr.Count;
            }
            if (intRegisterID_Count == 0)
            {
                return lngRes;
            }

            System.Text.StringBuilder stbRegisterID = new System.Text.StringBuilder(intRegisterID_Count * 15);

            for (int i = 0; i < intRegisterID_Count - 1; i++)
            {
                stbRegisterID.Append("'" + m_glstRegisterid_chr[i] + "',");
                //ԭ����ȷ�Ĵ��룬��Ҫ���� m_strRegisterid_chr += "'" + m_glstRegisterid_chr[i] + "',";

            }
            stbRegisterID.Append("'" + m_glstRegisterid_chr[intRegisterID_Count - 1] + "'");//���һ�������С������������ر���
            m_strRegisterid_chr = stbRegisterID.ToString().Trim();
            //m_strRegisterid_chr = m_strRegisterid_chr.TrimEnd(",".ToCharArray());
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                /*
                //���˱�
                string strSql = @"select distinct a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                                        a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                                        a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                                        b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                                        c.code_chr bedname, d.deptname_vchr areaname, a.limitrate_mny,
                                        e.flgname_vchr as state, mzdiagnose_vchr,
                                        f.paytypename_vchr paytypename_vchr, sysdate today,
                                        a1.remarkname_vchr, a.des_vchr, a1.status_int remark_int,
                                        a1.chargectl_int, a2.deptname_vchr deptname,
                                        a1.seq_int remark_no, a.insuredsum_mny, k.patientcardid_chr 
                                   from t_opr_bih_register a,
                                        t_opr_bih_registerdetail b,
                                        t_bse_bed c,
                                        t_bse_deptdesc d,
                                        (select flg_int, flgname_vchr
                                           from t_sys_flg_table
                                          where tablename_vchr = 't_opr_bih_register'
                                            and columnname_vchr = 'PSTATUS_INT') e,
                                        (select tf.paytypename_vchr, tf.paytypeid_chr 
                                           from t_bse_patientpaytype tf
                                          where tf.isusing_num = 1 and tf.payflag_dec <> 1) f,
                                        t_opr_bih_patspecremark a1,
                                        t_bse_deptdesc a2,
                                        (select patientid_chr, patientcardid_chr from t_bse_patientcard where status_int = 1 or status_int = 3) k  
                                  where a.registerid_chr = b.registerid_chr(+)
                                    and a.status_int = 1 
                                    and a.patientid_chr = k.patientid_chr(+)
                                    and a.registerid_chr = c.bihregisterid_chr(+)
                                    and a.areaid_chr = d.deptid_chr(+)
                                    and a.pstatus_int = e.flg_int(+)
                                    and a.paytypeid_chr = f.paytypeid_chr(+)
                                    and a.registerid_chr = a1.registerid_chr(+)
                                    and a.deptid_chr = a2.deptid_chr(+)
                                    and a.pstatus_int <> 3";
                                    //and a.registerid_chr in ([registerid_chr])";���뱣�����������룬��Ϊ�˲���

                //strSql = strSql.Replace("[registerid_chr]", m_strRegisterid_chr);���뱣�����������룬��Ϊ�˲���
                */

                //��SQL��䣬ȥ��distinct��in
                string strSql = @"select a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                                        a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                                        a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                                        b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                                        c.code_chr bedname, d.deptname_vchr areaname, a.limitrate_mny,
                                        e.flgname_vchr as state, mzdiagnose_vchr,
                                        f.paytypename_vchr paytypename_vchr, sysdate today,
                                        a1.remarkname_vchr, a.des_vchr, a1.status_int remark_int,
                                        a1.chargectl_int, a2.deptname_vchr deptname,
                                        a1.seq_int remark_no, a.insuredsum_mny, k.patientcardid_chr 
                                   from t_opr_bih_register a,
                                        t_opr_bih_registerdetail b,
                                        t_bse_bed c,
                                        t_bse_deptdesc d,
                                        (select flg_int, flgname_vchr
                                           from t_sys_flg_table
                                          where tablename_vchr = 't_opr_bih_register'
                                            and columnname_vchr = 'PSTATUS_INT') e,
                                        (select tf.paytypename_vchr, tf.paytypeid_chr 
                                           from t_bse_patientpaytype tf
                                          where tf.isusing_num = 1 and tf.payflag_dec <> 1) f,
                                        t_opr_bih_patspecremark a1,
                                        t_bse_deptdesc a2,
                                        (select patientid_chr, patientcardid_chr from t_bse_patientcard where status_int in (1, 3)) k  
                                  where a.registerid_chr = b.registerid_chr(+)
                                    and a.patientid_chr = k.patientid_chr(+)
                                    and a.registerid_chr = c.bihregisterid_chr(+)
                                    and a.areaid_chr = d.deptid_chr(+)
                                    and a.pstatus_int = e.flg_int(+)
                                    and a.paytypeid_chr = f.paytypeid_chr(+)
                                    and a.registerid_chr = a1.registerid_chr(+)
                                    and a.deptid_chr = a2.deptid_chr(+)
                                    and a.status_int = 1 
                                    and a.pstatus_int in (0,1,2,4)
                                    and a.registerid_chr in ([registerid_chr])";



                strSql = strSql.Replace("[registerid_chr]", m_strRegisterid_chr);
                lngRes = 0;
                DataTable dtTempTable = new DataTable();
                lngRes = HRPService.lngGetDataTableWithoutParameters(strSql, ref dtTempTable);
                com.digitalwave.Utility.DataSetHelper.clsDataSetHelper objDataSetHelper = new com.digitalwave.Utility.DataSetHelper.clsDataSetHelper();
                //string strFilterExpression = "registerid_chr IN (" + m_strRegisterid_chr + ")";
                string strFilterExpression = "";
                string[] colFieldNames_str = new string[29]{"registerid_chr", "patientid_chr", "inpatientid_chr",
                                        "inpatient_dat", "deptid_chr", "areaid_chr", "bedid_chr",
                                        "diagnose_vchr", "inpatientcount_int", "icd10diagtext_vchr",
                                        "name_vchr", "sex_chr", "birth_dat", "paytypeid_chr",
                                        "bedname", "areaname", "limitrate_mny",
                                        "state", "mzdiagnose_vchr",
                                        "paytypename_vchr", "today",
                                        "remarkname_vchr", "des_vchr", "remark_int",
                                        "chargectl_int", "deptname",
                                        "remark_no", "insuredsum_mny", "patientcardid_chr"};
                m_dtPatients = objDataSetHelper.SelectDistinct("PatientDataByArea", dtTempTable, strFilterExpression, colFieldNames_str);

                /*
                System.Data.DataRow[] objDataSubSet = null;
                //m_strRegisterid_chr = "'000000011422','000000011199'";//ʾ��,��ʽ������Ҫע�͵�
                //objDataSubSet = m_dtPatients.Select("registerid_chr IN ('000000011422','000000011199')");
                objDataSubSet = dtTempTable.Select("registerid_chr IN (" + m_strRegisterid_chr + ")");
                m_dtPatients = new DataTable();
                m_dtPatients = dtTempTable.Clone();
                
                if (objDataSubSet != null)
                {
                    int intJ = 0;
                    int intCount = objDataSubSet.Length;
                    System.Data.DataRow objOneRow = null;
                    //string strID = "";
                    for (intJ = 0; intJ < intCount; intJ++)
                    {
                        objOneRow = objDataSubSet[intJ];
                        //strID = strID + " " + objOneRow["registerid_chr"].ToString();
                        //m_dtPatients.Rows.Add(objOneRow.ItemArray);
                        m_dtPatients.LoadDataRow(objOneRow.ItemArray, true);
                    }
                    m_dtPatients.AcceptChanges();
                }
                */
                dtTempTable = null;

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
        /// ��ȡ������Ϣ(����in) 2007-5-8
        /// </summary>
        /// <param name="m_lngMotion_id_int"></param>
        /// <param name="m_dtPatients"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientDTByArea(long m_lngMotion_id_int, out DataTable m_dtPatients)
        {
            long lngRes = -1;
            m_dtPatients = null;

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                //���˱�
                string strSql = @"select distinct a.registerid_chr, a.patientid_chr, a.inpatientid_chr,
                                                a.inpatient_dat, a.deptid_chr, a.areaid_chr, a.bedid_chr,
                                                a.diagnose_vchr, a.inpatientcount_int, a.icd10diagtext_vchr,
                                                b.name_vchr, b.sex_chr, b.birth_dat, a.paytypeid_chr,
                                                c.code_chr bedname, d.deptname_vchr areaname, a.limitrate_mny,
                                                e.flgname_vchr as state, mzdiagnose_vchr,
                                                f.paytypename_vchr paytypename_vchr, SYSDATE today,
                                                a1.remarkname_vchr, a.des_vchr, a1.status_int remark_int,
                                                a1.chargectl_int, a2.deptname_vchr deptname,
                                                a1.seq_int remark_no, a.insuredsum_mny, k.patientcardid_chr 
                                           from t_opr_bih_register a,
                                                t_opr_bih_registerdetail b,
                                                t_bse_bed c,
                                                t_bse_deptdesc d,
                                                (select flg_int, flgname_vchr
                                                   from t_sys_flg_table
                                                  where tablename_vchr = 't_opr_bih_register'
                                                    and columnname_vchr = 'pstatus_int') e,
                                                (select tf.paytypename_vchr, tf.paytypeid_chr
                                                   from t_bse_patientpaytype tf
                                                  where tf.isusing_num = 1 and tf.payflag_dec <> 1) f,
                                                t_opr_bih_patspecremark a1,
                                                t_bse_deptdesc a2,
                                                t_aid_bih_motion mon,
                                                (select patientid_chr, patientcardid_chr from t_bse_patientcard where status_int = 1 or status_int = 3) k    
                                          where a.registerid_chr = b.registerid_chr(+)
                                            and a.status_int = 1
                                            and a.patientid_chr = k.patientid_chr(+)                                             
                                            and a.registerid_chr = c.bihregisterid_chr(+)
                                            and a.areaid_chr = d.deptid_chr(+)
                                            and a.pstatus_int = e.flg_int(+)
                                            and a.paytypeid_chr = f.paytypeid_chr(+)
                                            and a.registerid_chr = a1.registerid_chr(+)
                                            and a.deptid_chr = a2.deptid_chr(+)
                                            and a.pstatus_int <> 3
                                            and mon.registerid_chr = a.registerid_chr
                                            and motion_id_int = ?";

                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_lngMotion_id_int;
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtPatients, arrParams);

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
        /// 2018-03-08 ����ȷ�ϸ÷�������ʹ��
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <param name="m_dtPrepay"></param>
        /// <returns></returns> 
        [AutoComplete]
        public long m_lngGetChargeByArea(string m_strAreaid_chr, out DataTable m_dtChargeList, out DataTable m_dtChargeMoney, out DataTable m_dtPrepay)
        {
            long lngRes = -1;
            m_dtChargeList = new DataTable();
            m_dtChargeMoney = new DataTable();
            m_dtPrepay = new DataTable();
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                string strSql = @"select a.registerid_chr, b.unitprice_dec, b.amount_dec, b.pstatus_int
                                      from t_opr_bih_register a, t_opr_bih_patientcharge b
                                     where a.registerid_chr = b.registerid_chr
                                       and a.pstatus_int <> 3
                                       and a.status_int = 1
                                       and b.status_int = 1
                                       and b.chargeactive_dat is not null
                                       and a.areaid_chr = ?";
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr;
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtChargeMoney, arrParams);
                if (lngRes > 0)
                {
                    strSql = @"select b.money_dec, a.registerid_chr
                                  from t_opr_bih_register a, t_opr_bih_prepay b
                                 where a.registerid_chr = b.registerid_chr
                                   and a.pstatus_int <> 3
                                   and a.status_int = 1
                                   and b.isclear_int = 0
                                   and a.areaid_chr = ?";
                    System.Data.IDataParameter[] arrParams3 = null;
                    HRPService.CreateDatabaseParameter(1, out arrParams3);
                    arrParams3[0].Value = m_strAreaid_chr;
                    lngRes = 0;
                    lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtPrepay, arrParams3);
                }

                if (lngRes > 0)
                {

                    //�����м���ϸ��
                    strSql = @"select   c.seq_int, c.orderid_chr, c.orderdicid_chr, c.chargeitemid_chr,
                                     c.clacarea_chr, c.createarea_chr, c.flag_int, c.chargeitemname_chr,
                                     c.spec_vchr, c.unit_vchr, c.amount_dec, c.unitprice_dec,
                                     c.creatorid_chr, c.creator_vchr, c.createdate_dat, c.remark,
                                     c.insuracedesc_vchr, c.ratetype_int, c.poflag_int,
                                     c.continueusetype_int, c.singleamount_dec, c.continuefreqid_chr,
                                     c.newdiscount_dec, a.orderid_chr, a.registerid_chr, a.recipeno_int,
                                     d.deptname_vchr, f.itemsrcid_vchr, f.isrich_int, f.isselfpay_chr,
                                     f.itemipcalctype_chr, f.itemipinvtype_chr, f.itemsrctype_int,
                                     g.ipnoqtyflag_int, g.medicnetype_int, g.putmedtype_int, a.chargedoctorgroupid_chr,g.medicinetypeid_chr,
                                     f.tradeprice_mny
                                from t_opr_bih_order a,
                                     t_opr_bih_register b,
                                     t_opr_bih_orderchargedept c,
                                     t_bse_deptdesc d,
                                     t_bse_chargeitem f,
                                     t_bse_medicine g
                               where a.orderid_chr = c.orderid_chr
                                 and a.registerid_chr = b.registerid_chr
                                 and c.chargeitemid_chr = f.itemid_chr
                                 and f.itemsrcid_vchr = g.medicineid_chr(+)
                                 and c.clacarea_chr = d.deptid_chr(+)
                                 and (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                                 and (   trunc (a.executedate_dat) < trunc (sysdate)
                                      or a.executedate_dat is null
                                     )
                                 and (a.status_int = 5 or (a.status_int = 2 and a.executetype_int = 1))
                                 and b.areaid_chr = ?
                                 and b.status_int = 1
                                 and b.pstatus_int <> 3
                            order by c.orderid_chr, c.seq_int";

                    System.Data.IDataParameter[] arrParams2 = null;
                    HRPService.CreateDatabaseParameter(1, out arrParams2);
                    arrParams2[0].Value = m_strAreaid_chr;
                    lngRes = 0;
                    lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtChargeList, arrParams2);
                }
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
        /// 2018-03-08 �÷�������
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_glstRegisterid_chr"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <param name="m_dtPrepay"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeByArea(string areaId, List<string> lstRegisterid, out DataTable dtChargeList, out DataTable dtChargeMoney, out DataTable dtPrepay)
        {
            long lngRes = -1;
            dtChargeList = null;
            dtChargeMoney = null;
            dtPrepay = null;
            string registerId = "";

            int intRegisterID_Count = 0;
            if (lstRegisterid != null)
            {
                intRegisterID_Count = lstRegisterid.Count;
            }
            if (intRegisterID_Count == 0)
            {
                return lngRes;
            }

            System.Text.StringBuilder stbRegisterID = new System.Text.StringBuilder(intRegisterID_Count * 15);
            for (int i = 0; i < intRegisterID_Count - 1; i++)
            {
                stbRegisterID.Append("'" + lstRegisterid[i] + "',");
            }
            stbRegisterID.Append("'" + lstRegisterid[intRegisterID_Count - 1] + "'");
            registerId = stbRegisterID.ToString().Trim();

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                string Sql = string.Empty;
                dtChargeMoney = new DataTable();
                DataTable dtTmp = null;
                IDataParameter[] parm = null;

                foreach (string regId in lstRegisterid)
                {
                    Sql = @"select a.registerid_chr,
                                   a.pstatus_int,
                                   sum(round(a.unitprice_dec * a.amount_dec, 2)) as money
                              from t_opr_bih_patientcharge a
                             where a.status_int = 1
                               and a.chargeactive_dat is not null
                               and a.registerid_chr = ?
                             group by a.registerid_chr, a.pstatus_int
                            ";

                    HRPService.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = regId;
                    lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dtTmp, parm);
                    if (dtTmp != null && dtTmp.Rows.Count > 0)
                    {
                        dtChargeMoney.BeginLoadData();
                        dtChargeMoney.Merge(dtTmp);
                        dtChargeMoney.EndLoadData();
                        dtChargeMoney.AcceptChanges();
                    }
                }

                if (lngRes > 0)
                {
                    Sql = @"select b.money_dec, b.registerid_chr
                              from t_opr_bih_prepay b
                             where b.isclear_int = 0
                               and b.registerid_chr in ({0})";
                    Sql = string.Format(Sql, registerId);
                    lngRes = HRPService.lngGetDataTableWithoutParameters(Sql, ref dtPrepay);
                }

                if (lngRes > 0)
                {
                    #region 2.1
                    Sql = @"select /*+ all_rows*/ c.seq_int, c.orderid_chr, c.orderdicid_chr, c.chargeitemid_chr,
                                   c.clacarea_chr, c.createarea_chr, c.flag_int, c.chargeitemname_chr,
                                   c.spec_vchr, c.unit_vchr, c.amount_dec, c.unitprice_dec,
                                   c.creatorid_chr, c.creator_vchr, c.createdate_dat, c.remark,
                                   c.insuracedesc_vchr, c.ratetype_int, c.poflag_int,
                                   c.continueusetype_int, c.singleamount_dec, c.continuefreqid_chr,
                                   c.newdiscount_dec, a.orderid_chr, a.registerid_chr, a.recipeno_int,
                                   d.deptname_vchr, f.itemsrcid_vchr, f.isrich_int, f.isselfpay_chr,
                                   f.itemipcalctype_chr, f.itemipinvtype_chr, f.itemsrctype_int,
                                   f.ipchargeflg_int, f.itemprice_mny, f.packqty_dec, f.itemspec_vchr,
                                   f.dosage_dec, f.dosageunit_chr, g.ipnoqtyflag_int, g.medicnetype_int,
                                   g.putmedtype_int, a.chargedoctorgroupid_chr, f.tradeprice_mny,g.medicinetypeid_chr, a.ratetype_int as medSource, a.curedays, a.checkstate,
                                   p.birth_dat, f.ischildprice   
                              from t_opr_bih_order a,
                                   t_opr_bih_register b,
                                   t_opr_bih_orderchargedept c,
                                   t_bse_deptdesc d,
                                   t_bse_chargeitem f,
                                   t_bse_medicine g,
                                   t_bse_patient p
                             where a.orderid_chr = c.orderid_chr
                               and a.registerid_chr = b.registerid_chr
                               and c.chargeitemid_chr = f.itemid_chr
                               and f.itemsrcid_vchr = g.medicineid_chr(+)
                               and c.clacarea_chr = d.deptid_chr(+)
                               and (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                               and (trunc (a.executedate_dat) < trunc (sysdate)
                                    or a.executedate_dat is null
                                   )
                               and (a.status_int = 5 or (a.status_int = 2 and a.executetype_int = 1))
                               and a.sourcetype_int = 0
                               and b.patientid_chr = p.patientid_chr
                               -- and (a.curedays is null or a.curedays = 0 or (a.curedays > 0 and a.checkstate = 1))
                               and a.curareaid_chr=?
                               and b.pstatus_int in (0, 1, 4)
                               and b.status_int = 1
                               and b.areaid_chr = ?";

                    System.Data.IDataParameter[] arrParams = null;
                    HRPService.CreateDatabaseParameter(2, out arrParams);
                    arrParams[0].Value = areaId;
                    arrParams[1].Value = areaId;

                    dtChargeList = new DataTable();
                    lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dtChargeList, arrParams);

                    #endregion 2.1

                    #region 2.2
                    Sql = @"select c.seq_int, c.orderid_chr, c.orderdicid_chr, c.chargeitemid_chr,
                                   c.clacarea_chr, c.createarea_chr, c.flag_int, c.chargeitemname_chr,
                                   c.spec_vchr, c.unit_vchr, c.amount_dec, c.unitprice_dec,
                                   c.creatorid_chr, c.creator_vchr, c.createdate_dat, c.remark,
                                   c.insuracedesc_vchr, c.ratetype_int, c.poflag_int,
                                   c.continueusetype_int, c.singleamount_dec, c.continuefreqid_chr,
                                   c.newdiscount_dec, a.orderid_chr, a.registerid_chr, a.recipeno_int,
                                   d.deptname_vchr, f.itemsrcid_vchr, f.isrich_int, f.isselfpay_chr,
                                   f.itemipcalctype_chr, f.itemipinvtype_chr, f.itemsrctype_int,
                                   f.ipchargeflg_int, f.itemprice_mny, f.packqty_dec, f.itemspec_vchr,
                                   f.dosage_dec, f.dosageunit_chr, g.ipnoqtyflag_int, g.medicnetype_int,
                                   g.putmedtype_int, a.chargedoctorgroupid_chr, f.tradeprice_mny,g.medicinetypeid_chr, a.ratetype_int as medSource, a.curedays, a.checkstate,
                                   p.birth_dat, f.ischildprice     
                              from t_opr_bih_order a,
                                   t_opr_bih_register b,
                                   t_opr_bih_orderchargedept c,
                                   t_bse_deptdesc d,
                                   t_bse_chargeitem f,
                                   t_bse_medicine g,
                                   t_bse_patient p
                             where a.orderid_chr = c.orderid_chr
                               and a.registerid_chr = b.registerid_chr
                               and c.chargeitemid_chr = f.itemid_chr
                               and f.itemsrcid_vchr = g.medicineid_chr(+)
                               and c.clacarea_chr = d.deptid_chr(+)
                               and (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                               and (trunc (a.executedate_dat) < trunc (sysdate)
                                    or a.executedate_dat is null
                                   )
                               and (a.status_int = 5 or (a.status_int = 2 and a.executetype_int = 1))
                               and b.patientid_chr = p.patientid_chr
                               and b.status_int = 1
                               and b.pstatus_int in (0, 1, 4)
                               and a.sourcetype_int = 1
                               -- and (a.curedays is null or a.curedays = 0 or (a.curedays > 0 and a.checkstate = 1)) 
                               and a.createareaid_chr = ? ";

                    HRPService.CreateDatabaseParameter(1, out arrParams);
                    arrParams[0].Value = areaId;

                    DataTable dt = new DataTable();
                    lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dt, arrParams);

                    if (dtChargeList != null && dt != null && dt.Rows.Count > 0)
                        dtChargeList.Merge(dt);
                    dtChargeList.AcceptChanges();
                    dt = null;
                    #endregion 2.2

                    if (dtChargeList != null && dtChargeList.Rows.Count > 0)
                    {
                        if (this.IsUseChildPrice())
                        {
                            clsBrithdayToAge calc = new clsBrithdayToAge();
                            foreach (DataRow dr in dtChargeList.Rows)
                            {
                                if (dr["ischildprice"] != DBNull.Value && Convert.ToInt32(dr["ischildprice"]) == 1)
                                {
                                    if (calc.IsChild(Convert.ToDateTime(dr["birth_dat"])))
                                    {
                                        dr["itemprice_mny"] = Convert.ToDecimal(dr["itemprice_mny"]) * EntityChildPrice.AddScale;
                                    }
                                }
                            }
                        }

                    }
                }
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
        /// ��ȡ���˷�����Ϣ(����in) 2007-5-8  ---> 2018-03-08 ����ȷ�ϸ÷�������ʹ��
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_lngMotion_id_int"></param>
        /// <param name="m_dtChargeList"></param>
        /// <param name="m_dtChargeMoney"></param>
        /// <param name="m_dtPrepay"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeByArea(string m_strAreaid_chr, long m_lngMotion_id_int, out DataTable m_dtChargeList, out DataTable m_dtChargeMoney, out DataTable m_dtPrepay)
        {
            long lngRes = -1;
            m_dtChargeList = null;
            m_dtChargeMoney = null;
            m_dtPrepay = null;

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                string Sql = @"select a.registerid_chr,
                                       a.pstatus_int,
                                       sum(round(a.unitprice_dec * a.amount_dec, 2)) as money
                                  from t_opr_bih_patientcharge a, t_aid_bih_motion mon
                                 where a.status_int = 1
                                   and a.chargeactive_dat is not null
                                   and mon.registerid_chr = a.registerid_chr
                                   and motion_id_int = ?
                                 group by a.registerid_chr, a.pstatus_int
                                ";

                lngRes = 0;
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_lngMotion_id_int;
                lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref m_dtChargeMoney, arrParams);

                if (lngRes > 0)
                {
                    Sql = @"select b.money_dec, b.registerid_chr
                              from t_opr_bih_prepay b, t_aid_bih_motion mon
                             where b.isclear_int = 0
                               and mon.registerid_chr = b.registerid_chr
                               and motion_id_int = ? ";

                    System.Data.IDataParameter[] arrParams1 = null;
                    HRPService.CreateDatabaseParameter(1, out arrParams1);
                    arrParams1[0].Value = m_lngMotion_id_int;
                    lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref m_dtPrepay, arrParams1);
                }

                if (lngRes > 0)
                {
                    string SQL = "";
                    DataTable dt = new DataTable();

                    #region 2.1
                    SQL = @"select c.seq_int, c.orderid_chr, c.orderdicid_chr, c.chargeitemid_chr,
                                   c.clacarea_chr, c.createarea_chr, c.flag_int, c.chargeitemname_chr,
                                   c.spec_vchr, c.unit_vchr, c.amount_dec, c.unitprice_dec,
                                   c.creatorid_chr, c.creator_vchr, c.createdate_dat, c.remark,
                                   c.insuracedesc_vchr, c.ratetype_int, c.poflag_int,
                                   c.continueusetype_int, c.singleamount_dec, c.continuefreqid_chr,
                                   c.newdiscount_dec, a.orderid_chr, a.registerid_chr, a.recipeno_int,
                                   d.deptname_vchr, f.itemsrcid_vchr, f.isrich_int, f.isselfpay_chr,
                                   f.itemipcalctype_chr, f.itemipinvtype_chr, f.itemsrctype_int,
                                   f.ipchargeflg_int, f.itemprice_mny, f.packqty_dec, f.itemspec_vchr,
                                   f.dosage_dec, f.dosageunit_chr, g.ipnoqtyflag_int, g.medicnetype_int,
                                   g.putmedtype_int, a.chargedoctorgroupid_chr,f.tradeprice_mny
                              from t_opr_bih_order a,
                                   t_opr_bih_register b,
                                   t_opr_bih_orderchargedept c,
                                   t_bse_deptdesc d,
                                   t_bse_chargeitem f,
                                   t_bse_medicine g
                             where a.orderid_chr = c.orderid_chr
                               and a.registerid_chr = b.registerid_chr
                               and c.chargeitemid_chr = f.itemid_chr
                               and f.itemsrcid_vchr = g.medicineid_chr(+)
                               and c.clacarea_chr = d.deptid_chr(+)
                               and (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                               and (trunc (a.executedate_dat) < trunc (sysdate)
                                    or a.executedate_dat is null
                                   )
                               and (a.status_int = 5 or (a.status_int = 2 and a.executetype_int = 1))
                               and ((b.areaid_chr = ? and a.sourcetype_int = 0))
                               and b.status_int = 1
                               and (b.pstatus_int <> 2 or b.pstatus_int <> 3)";

                    HRPService.CreateDatabaseParameter(1, out arrParams);
                    arrParams[0].Value = m_strAreaid_chr;

                    lngRes = HRPService.lngGetDataTableWithParameters(SQL, ref dt, arrParams);

                    m_dtChargeList = dt.Clone();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        m_dtChargeList.Rows.Add(dt.Rows[i].ItemArray);
                    }
                    m_dtChargeList.AcceptChanges();
                    #endregion 2.1

                    #region 2.2
                    SQL = @"select c.seq_int, c.orderid_chr, c.orderdicid_chr, c.chargeitemid_chr,
                                   c.clacarea_chr, c.createarea_chr, c.flag_int, c.chargeitemname_chr,
                                   c.spec_vchr, c.unit_vchr, c.amount_dec, c.unitprice_dec,
                                   c.creatorid_chr, c.creator_vchr, c.createdate_dat, c.remark,
                                   c.insuracedesc_vchr, c.ratetype_int, c.poflag_int,
                                   c.continueusetype_int, c.singleamount_dec, c.continuefreqid_chr,
                                   c.newdiscount_dec, a.orderid_chr, a.registerid_chr, a.recipeno_int,
                                   d.deptname_vchr, f.itemsrcid_vchr, f.isrich_int, f.isselfpay_chr,
                                   f.itemipcalctype_chr, f.itemipinvtype_chr, f.itemsrctype_int,
                                   f.ipchargeflg_int, f.itemprice_mny, f.packqty_dec, f.itemspec_vchr,
                                   f.dosage_dec, f.dosageunit_chr, g.ipnoqtyflag_int, g.medicnetype_int,
                                   g.putmedtype_int, a.chargedoctorgroupid_chr,f.tradeprice_mny
                              from t_opr_bih_order a,
                                   t_opr_bih_register b,
                                   t_opr_bih_orderchargedept c,
                                   t_bse_deptdesc d,
                                   t_bse_chargeitem f,
                                   t_bse_medicine g
                             where a.orderid_chr = c.orderid_chr
                               and a.registerid_chr = b.registerid_chr
                               and c.chargeitemid_chr = f.itemid_chr
                               and f.itemsrcid_vchr = g.medicineid_chr(+)
                               and c.clacarea_chr = d.deptid_chr(+)
                               and (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                               and (trunc (a.executedate_dat) < trunc (sysdate)
                                    or a.executedate_dat is null
                                   )
                               and (a.status_int = 5 or (a.status_int = 2 and a.executetype_int = 1))
                               and ((a.createareaid_chr = ? and a.sourcetype_int = 1))
                               and b.status_int = 1
                               and (b.pstatus_int <> 2 or b.pstatus_int <> 3)";

                    HRPService.CreateDatabaseParameter(1, out arrParams);
                    arrParams[0].Value = m_strAreaid_chr;

                    lngRes = HRPService.lngGetDataTableWithParameters(SQL, ref dt, arrParams);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        m_dtChargeList.Rows.Add(dt.Rows[i].ItemArray);
                    }
                    m_dtChargeList.AcceptChanges();
                    #endregion 2.2

                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetChargeByRegisterids(List<string> m_glstRegisterid_chr, out DataTable m_dtChargeMoney, out DataTable m_dtPrepay)
        {
            long lngRes = -1;
            m_dtChargeMoney = null;
            m_dtPrepay = null;
            string m_strRegisterid_chr = "";
            int intRegisterID_Count = 0;
            if (m_glstRegisterid_chr != null)
            {
                intRegisterID_Count = m_glstRegisterid_chr.Count;
            }

            if (intRegisterID_Count == 0)
            {
                return lngRes;
            }
            System.Text.StringBuilder stbRegister = new System.Text.StringBuilder(intRegisterID_Count * 15);
            for (int i = 0; i < intRegisterID_Count - 1; i++)
            {
                stbRegister.Append("'" + m_glstRegisterid_chr[i] + "',");
            }
            stbRegister.Append("'" + m_glstRegisterid_chr[intRegisterID_Count - 1] + "'");
            m_strRegisterid_chr = stbRegister.ToString().Trim();

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                DataTable dt = null;
                string Sql = string.Empty;
                IDataParameter[] parm = null;
                m_dtChargeMoney = new DataTable();

                foreach (string strRegID in m_glstRegisterid_chr)
                {
                    Sql = @"select a.registerid_chr,
                                   a.pstatus_int,
                                   sum(round(a.unitprice_dec * a.amount_dec, 2)) as money
                              from t_opr_bih_patientcharge a
                             where a.status_int = 1
                               and a.chargeactive_dat is not null
                               and a.registerid_chr = ?
                             group by a.registerid_chr, a.pstatus_int
                            ";
                    dt = new DataTable();
                    HRPService.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = strRegID;
                    lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dt, parm);
                    if (dt.Rows.Count > 0)
                    {
                        m_dtChargeMoney.BeginLoadData();
                        m_dtChargeMoney.Merge(dt);
                        m_dtChargeMoney.EndLoadData();
                        m_dtChargeMoney.AcceptChanges();
                    }
                }
                if (lngRes > 0)
                {
                    Sql = @"select b.money_dec, b.registerid_chr
                              from t_opr_bih_prepay b
                             where b.isclear_int = 0
                               and b.registerid_chr in ({0})";
                    Sql = string.Format(Sql, m_strRegisterid_chr);
                    lngRes = HRPService.lngGetDataTableWithoutParameters(Sql, ref m_dtPrepay);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetChargeByRegisterids(long m_lngMotion_id_int, out DataTable m_dtChargeMoney, out DataTable m_dtPrepay)
        {
            long lngRes = -1;
            m_dtChargeMoney = null;
            m_dtPrepay = null;

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                string strSql = @"select a.registerid_chr, pstatus_int,
                                         sum (round (a.unitprice_dec * a.amount_dec, 2)) money
                                    from t_opr_bih_patientcharge a, t_aid_bih_motion mon
                                   where a.status_int = 1
                                     and mon.registerid_chr = a.registerid_chr
                                     and a.chargeactive_dat is not null
                                     and motion_id_int = ?
                                group by a.registerid_chr, pstatus_int";
                lngRes = 0;
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_lngMotion_id_int;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtChargeMoney, arrParams);
                if (lngRes > 0)
                {
                    strSql = @" select b.money_dec, b.registerid_chr
                                  from t_opr_bih_prepay b, t_aid_bih_motion mon
                                 where b.isclear_int = 0
                                   and mon.registerid_chr = b.registerid_chr
                                   and motion_id_int = ? ";
                    lngRes = 0;
                    System.Data.IDataParameter[] arrParams1 = null;
                    HRPService.CreateDatabaseParameter(1, out arrParams1);
                    arrParams1[0].Value = m_lngMotion_id_int;
                    lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtPrepay, arrParams1);
                }
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
        /// ִ�е�ִ��ҽ����ͳ��(��ִ�й��ĳ���)
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtReExecute"></param>
        /// <returns></returns>
        /// ����ҽ��ִ�б�t_opr_bih_orderexecute�����¼��ÿ��ҽ������ִ�д���
        [AutoComplete]
        public long m_lngGetReExecute(string m_strAreaid_chr, out DataTable m_dtReExecute)
        {
            long lngRes = -1;
            m_dtReExecute = new DataTable();
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                string strSQL01 = @"select c.orderid_chr
                                            from t_opr_bih_order a,
                                                 t_opr_bih_register b,
                                                 t_opr_bih_orderexecute c
                                           where a.registerid_chr = b.registerid_chr
                                             and a.orderid_chr = c.orderid_chr
                                             and (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                                             and a.executetype_int = 1
                                             and a.sourcetype_int = 0
                                             and a.status_int = 2
                                             and b.pstatus_int in (0, 1, 4)
                                             and b.status_int = 1
                                             and b.areaid_chr = ?";

                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr;

                lngRes = 0;

                System.Data.DataTable dtTemp = new DataTable();
                lngRes = HRPService.lngGetDataTableWithParameters(strSQL01, ref dtTemp, arrParams);

                string strSQL02 = @"select c.orderid_chr
                                            from t_opr_bih_order a,
                                                 t_opr_bih_register b,
                                                 t_opr_bih_orderexecute c
                                           where a.registerid_chr = b.registerid_chr
                                             and a.orderid_chr = c.orderid_chr
                                             and (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                                             and b.pstatus_int in (0, 1, 4)
                                             and b.status_int = 1
                                             and a.executetype_int = 1
                                             and a.status_int = 2
                                             and a.sourcetype_int = 1
                                             and a.createareaid_chr = ?";

                arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr;

                System.Data.DataTable dtTemp1 = new DataTable();
                lngRes = HRPService.lngGetDataTableWithParameters(strSQL02, ref dtTemp1, arrParams);

                if (dtTemp != null && dtTemp1 != null && dtTemp1.Rows.Count > 0)
                    dtTemp.Merge(dtTemp1);
                dtTemp.AcceptChanges();

                com.digitalwave.Utility.DataSetHelper.clsFieldInfo[] colSelectFields = new com.digitalwave.Utility.DataSetHelper.clsFieldInfo[2];
                colSelectFields[0] = new com.digitalwave.Utility.DataSetHelper.clsFieldInfo();
                colSelectFields[0].strFieldName = "orderid_chr";
                colSelectFields[0].strFieldAlias = "execounts";
                colSelectFields[0].strAggregate = "count";
                colSelectFields[0].strRelationName = "";

                colSelectFields[1] = new com.digitalwave.Utility.DataSetHelper.clsFieldInfo();
                colSelectFields[1].strFieldName = "orderid_chr";
                colSelectFields[1].strFieldAlias = "orderid_chr";
                colSelectFields[1].strAggregate = "";
                colSelectFields[1].strRelationName = "";

                com.digitalwave.Utility.DataSetHelper.clsFieldInfo[] colGroupByFields = new com.digitalwave.Utility.DataSetHelper.clsFieldInfo[1];
                colGroupByFields[0] = new com.digitalwave.Utility.DataSetHelper.clsFieldInfo();
                colGroupByFields[0].strFieldName = "orderid_chr";
                colGroupByFields[0].strFieldAlias = "orderid_chr";
                colGroupByFields[0].strAggregate = "";
                colGroupByFields[0].strRelationName = "";

                com.digitalwave.Utility.DataSetHelper.clsDataSetHelper objDSHelper = new com.digitalwave.Utility.DataSetHelper.clsDataSetHelper();
                objDSHelper.CreateGroupByTable("Order_Count", dtTemp, colSelectFields, ref m_dtReExecute);
                objDSHelper.InsertGroupByInto(ref m_dtReExecute, dtTemp, colSelectFields, "", colGroupByFields);
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

        #region ��ȡҽ��	���ݵ�ǰ����,���� --ҽ��ִ��������ʹ�� �ֱ�õ�ҽ����Ϣ,������Ϣ,������Ϣ   --> 2018-03-08 �÷�������ʹ��
        /// <summary>
        /// ��ȡҽ��	���ݵ�ǰ����
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_strRegisterID"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetExecOrderDTByAreaBed(string m_strAreaid_chr, string m_strRegisterID, out DataTable dt)
        {
            long lngRes = -1;
            dt = new DataTable();

            string Sql = @"select   a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr,
                                     a.executetype_int, a.recipeno_int, a.name_vchr, a.spec_vchr,
                                     a.execfreqid_chr, a.dosage_dec, a.execfreqname_chr, a.dosageunit_chr,
                                     a.get_dec, a.useunit_chr, a.getunit_chr, a.dosetypeid_chr,
                                     a.dosetypename_chr, a.startdate_dat, a.finishdate_dat,
                                     a.execdeptid_chr, a.execdeptname_chr, a.entrust_vchr, a.parentid_chr,
                                     a.status_int, a.creatorid_chr, a.creator_chr, a.createdate_dat,
                                     a.posterid_chr, a.poster_chr, a.postdate_dat, a.executorid_chr,
                                     a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                                     a.stopdate_dat, a.retractorid_chr, a.retractor_chr,
                                     a.retractdate_dat, a.isrich_int, a.ratetype_int, a.isrepare_int,
                                     a.use_dec, a.isneedfeel, a.outgetmeddays_int,
                                     a.assessoridforexec_chr, a.assessorforexec_chr,
                                     a.assessorforexec_dat, a.assessoridforstop_chr,
                                     a.assessorforstop_chr, a.assessorforstop_dat, a.backreason,
                                     a.sendbackid_chr, a.sendbacker_chr, a.sendback_dat, a.isyb_int,
                                     a.sampleid_vchr, a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                                     a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr,
                                     a.confirmer_vchr, a.confirm_dat, a.attachtimes_int, a.doctorid_chr,
                                     a.doctor_vchr, a.curareaid_chr, a.curbedid_chr, a.doctorgroupid_chr,
                                     a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                                     a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr,
                                     a.feel_int, a.charge_int, a.type_int, a.singleamount_dec,
                                     a.sourcetype_int, b.patientid_chr, c.lastname_vchr, c.registerid_chr,
                                     d.bedid_chr, d.code_chr, f.ordercateid_chr, f.viewname_vchr,
                                     sysdate today, g.days_int,
                                     decode (h.execcount, null, 0, h.execcount) execcount,
                                     g.lexectime_vchr, g.texectime_vchr, g.times_int, k.deptname_vchr,
                                     m.sample_type_desc_vchr, n.partname, a.chargedoctorgroupid_chr 
                                from t_opr_bih_order a,
                                     t_opr_bih_register b,
                                     t_opr_bih_registerdetail c,
                                     t_bse_bed d,
                                     t_bse_bih_orderdic e,
                                     t_aid_bih_ordercate f,
                                     t_aid_recipefreq g,
                                     (select   count (t.orderid_chr) execcount, t.orderid_chr
                                          from t_opr_bih_orderexecute t
                                         where trunc (t.createdate_dat) = trunc (sysdate)
                                      group by t.orderid_chr) h,
                                     t_bse_deptdesc k,
                                     t_aid_lis_sampletype m,
                                     ar_apply_partlist n
                               where a.registerid_chr = b.registerid_chr
                                 and b.registerid_chr = c.registerid_chr
                                 and a.curbedid_chr = d.bedid_chr(+)
                                 and a.orderdicid_chr = e.orderdicid_chr(+)
                                 and e.ordercateid_chr = f.ordercateid_chr(+)
                                 and a.execfreqid_chr = g.freqid_chr(+)
                                 and a.orderid_chr = h.orderid_chr(+)
                                 and a.curareaid_chr = k.deptid_chr(+)
                                 and a.sampleid_vchr = m.sample_type_id_chr(+)
                                 and a.partid_vchr = n.partid(+)
                                 and (a.stopdate_dat > sysdate or a.stopdate_dat is null)
                                 and (a.status_int = 2 or a.status_int = 5)
                                 and b.pstatus_int <> 3
                                 and b.status_int = 1
                                 and a.createareaid_chr = ?
                                 and a.registerid_chr = ?
                            order by d.code_chr, a.recipeno_int, a.parentid_chr desc";
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                IDataParameter[] parm = null;
                HRPService.CreateDatabaseParameter(2, out parm);
                parm[0].Value = m_strAreaid_chr;
                parm[1].Value = m_strRegisterID;
                lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dt, parm);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetPatientDTByAreaBed(string m_strAreaid_chr, string m_strRegisterID, out DataTable dt)
        {
            long lngRes = -1;
            dt = new DataTable();

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                //���˱�
                string Sql = @"SELECT a.registerid_chr,
                                       a.patientid_chr,
                                       a.inpatientid_chr,
                                       a.inpatient_dat,
                                       a.deptid_chr,
                                       a.areaid_chr,
                                       a.bedid_chr,
                                       a.diagnose_vchr,
                                       a.inpatientcount_int,
                                       a.icd10diagtext_vchr,
                                       b.name_vchr,
                                       b.sex_chr,
                                       b.birth_dat,
                                       a.paytypeid_chr,
                                       c.code_chr           as bedname,
                                       d.deptname_vchr      as areaname,
                                       a.limitrate_mny,
                                       e.flgname_vchr       as state,
                                       mzdiagnose_vchr,
                                       f.paytypename_vchr   as paytypename_vchr,
                                       SYSDATE              as today,
                                       g.remarkname_vchr,
                                       a.des_vchr,
                                       a1.STATUS_INT        as REMARK_INT,
                                       a1.CHARGECTL_INT,
                                       k.patientcardid_chr
                                  FROM t_opr_bih_register a,
                                       t_opr_bih_registerdetail b,
                                       t_bse_bed c,
                                       t_bse_deptdesc d,
                                       (SELECT flg_int, flgname_vchr
                                          FROM t_sys_flg_table
                                         WHERE tablename_vchr = 't_opr_bih_register'
                                           AND columnname_vchr = 'PSTATUS_INT') e,
                                       (SELECT tf.paytypeid_chr,
                                               tf.paytypename_vchr,
                                               tf.memo_vchr,
                                               tf.paylimit_mny,
                                               tf.payflag_dec,
                                               tf.paypercent_dec,
                                               tf.paytypeno_vchr,
                                               tf.isusing_num,
                                               tf.copayid_chr,
                                               tf.chargepercent_dec,
                                               tf.internalflag_int,
                                               tf.coalitionrecipeflag_int,
                                               tf.bihlimitrate_dec
                                          FROM t_bse_patientpaytype tf
                                         WHERE tf.isusing_num = 1
                                           AND tf.payflag_dec <> 1) f,
                                       t_opr_bih_patspecremark g,
                                       T_OPR_BIH_PATSPECREMARK a1,
                                       (select patientid_chr, patientcardid_chr
                                          from t_bse_patientcard
                                         where status_int = 1
                                            or status_int = 3) k
                                 WHERE a.registerid_chr = b.registerid_chr(+)
                                   AND a.status_int = 1
                                   and a.patientid_chr = k.patientid_chr
                                   AND a.registerid_chr = c.bihregisterid_chr(+)
                                   AND a.areaid_chr = d.deptid_chr(+)
                                   AND a.pstatus_int = e.flg_int(+)
                                   AND a.paytypeid_chr = f.paytypeid_chr(+)
                                   AND a.registerid_chr = g.registerid_chr(+)
                                   AND a.registerid_chr = a1.registerid_chr(+)
                                   and a.pstatus_int <> 3
                                   and a.REGISTERID_CHR = ?
                                ";

                IDataParameter[] parm = null;
                HRPService.CreateDatabaseParameter(1, out parm);
                parm[0].Value = m_strRegisterID;
                lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dt, parm);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngGetChargeByAreaBed(string m_strAreaid_chr, string m_strRegisterID, out DataTable dtChargeList, out DataTable dtChargeMoney)
        {
            long lngRes = -1;
            dtChargeList = new DataTable();
            dtChargeMoney = new DataTable();

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                string Sql = @"select k.registerid_chr,
                                   (SELECT sum(round(a.unitprice_dec * a.amount_dec, 2))
                                      FROM t_opr_bih_patientcharge a
                                     where a.pstatus_int = 3
                                       and a.STATUS_INT = 1
                                       and a.chargeactive_dat is not null
                                       and a.REGISTERID_CHR = ?) clearmoney,
                                   (SELECT sum(round(a.unitprice_dec * a.amount_dec, 2)) VerticalMoney
                                      FROM t_opr_bih_patientcharge a
                                     where a.pstatus_int = 4
                                       and a.STATUS_INT = 1
                                       and a.chargeactive_dat is not null
                                       and a.REGISTERID_CHR = ?) VerticalMoney,
                                   (SELECT sum(round(a.unitprice_dec * a.amount_dec, 2))
                                      FROM t_opr_bih_patientcharge a
                                     where a.pstatus_int != 0
                                       and a.STATUS_INT = 1
                                       and a.chargeactive_dat is not null
                                       and a.REGISTERID_CHR = ?) preusemoney,
                                   (SELECT sum(round(a.money_dec, 2))
                                      FROM t_opr_bih_prepay a
                                     where a.isclear_int = 0
                                       and a.REGISTERID_CHR = ?) NotUsePreMoney,
                                   (SELECT sum(round(a.unitprice_dec * a.amount_dec, 2))
                                      FROM t_opr_bih_patientcharge a
                                     where a.pstatus_int = 1
                                       and a.STATUS_INT = 1
                                       and a.chargeactive_dat is not null
                                       and a.REGISTERID_CHR = ?) WaitMoney,
                                   (SELECT sum(round(a.unitprice_dec * a.amount_dec, 2))
                                      FROM t_opr_bih_patientcharge a
                                     where a.pstatus_int = 2
                                       and a.STATUS_INT = 1
                                       and a.chargeactive_dat is not null
                                       and a.REGISTERID_CHR = ?) WaitClearMoney
                              from t_opr_bih_order k
                             where k.status_int in (2, 5)
                               and k.REGISTERID_CHR = ?
                               and rownum = 1
                            ";

                int n = -1;
                IDataParameter[] parm = null;
                HRPService.CreateDatabaseParameter(7, out parm);
                parm[++n].Value = m_strRegisterID;
                parm[++n].Value = m_strRegisterID;
                parm[++n].Value = m_strRegisterID;
                parm[++n].Value = m_strRegisterID;
                parm[++n].Value = m_strRegisterID;
                parm[++n].Value = m_strRegisterID;
                parm[++n].Value = m_strRegisterID;
                lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dtChargeMoney, parm);
                if (lngRes > 0)
                {

                    //������ϸ��
                    Sql = @"select c.seq_int,
                                   c.orderid_chr,
                                   c.orderdicid_chr,
                                   c.chargeitemid_chr,
                                   c.clacarea_chr,
                                   c.createarea_chr,
                                   c.flag_int,
                                   c.chargeitemname_chr,
                                   c.spec_vchr,
                                   c.unit_vchr,
                                   c.amount_dec,
                                   c.unitprice_dec,
                                   c.creatorid_chr,
                                   c.creator_vchr,
                                   c.createdate_dat,
                                   c.remark,
                                   c.insuracedesc_vchr,
                                   c.ratetype_int,
                                   c.poflag_int,
                                   c.continueusetype_int,
                                   c.singleamount_dec,
                                   c.newdiscount_dec,
                                   c.continuefreqid_chr,
                                   c.continuechargetype_int,
                                   c.itemchargetype_vchr,
                                   a.orderid_chr,
                                   a.registerid_chr,
                                   a.recipeno_int,
                                   d.deptname_vchr,
                                   f.itemsrctype_int,
                                   f.isrich_int,
                                   f.isselfpay_chr,
                                   f.itemipcalctype_chr,
                                   f.itemipinvtype_chr,
                                   f.itemsrctype_int,
                                   g.ipnoqtyflag_int, a.ratetype_int as medSource 
                              from t_opr_bih_order           a,
                                   t_opr_bih_register        b,
                                   t_opr_bih_orderchargedept c,
                                   t_bse_deptdesc            d,
                                   t_bse_chargeitem          f,
                                   t_bse_medicine            g
                             where a.orderid_chr = c.orderid_chr
                               and a.registerid_chr = b.registerid_chr
                               and c.chargeitemid_chr = f.itemid_chr
                               and f.itemsrcid_vchr = g.medicineid_chr(+)
                               and c.clacarea_chr = d.deptid_chr(+)
                               and a.registerid_chr = ?
                               and b.pstatus_int <> 3
                             order by c.orderid_chr, c.seq_int ";

                    HRPService.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = m_strRegisterID;
                    lngRes = HRPService.lngGetDataTableWithParameters(Sql, ref dtChargeList, parm);
                }
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

        #region סԺҽ��ִ�н���Ŀ���
        /// <summary>
        /// סԺҽ��ִ�н���Ŀ���
        /// </summary>
        /// <param name="m_dmlMedOCMin">Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�</param>
        /// <param name="m_dmlNoMedOCMin">Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�</param>
        /// <param name="m_dmlMedICMin">��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�</param>
        /// <param name="m_dmlNoMedICMin">��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�</param>
        /// <param name="m_intMoneyControl">'1030', '���ƻ�ʿִ��ģ���Ƿ�����Ƿ�Ѳ���ִ��ҽ��', '0-������ 1-����'</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBihExecOrderControls(out decimal m_dmlMedOCMin, out decimal m_dmlNoMedOCMin, out decimal m_dmlMedICMin, out decimal m_dmlNoMedICMin, out int m_intMoneyControl)
        {
            m_dmlMedOCMin = 0;//Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_dmlNoMedOCMin = 0;//Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_dmlMedICMin = 0;//��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_dmlNoMedICMin = 0;//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_intMoneyControl = 0;//'1030', '���ƻ�ʿִ��ģ���Ƿ�����Ƿ�Ѳ���ִ��ҽ��', '0-������ 1-����'

            long lngRes = -1;
            string strSQL = @"  select a.setid_chr, a.setstatus_int
                                  from t_sys_setting a
                                 where setid_chr in ('1018', '1019', '1020', '1021', '1030')";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        string m_strSetid_chr = dtbResult.Rows[i]["setid_chr"].ToString().Trim();
                        switch (m_strSetid_chr)
                        {
                            case "1018":
                                m_dmlMedOCMin = decimal.Parse(dtbResult.Rows[i]["setstatus_int"].ToString().Trim());//Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                                break;
                            case "1019":
                                m_dmlNoMedOCMin = decimal.Parse(dtbResult.Rows[i]["setstatus_int"].ToString().Trim());//Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                                break;
                            case "1020":
                                m_dmlMedICMin = decimal.Parse(dtbResult.Rows[i]["setstatus_int"].ToString().Trim());//��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                                break;
                            case "1021":
                                m_dmlNoMedICMin = decimal.Parse(dtbResult.Rows[i]["setstatus_int"].ToString().Trim());//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                                break;
                            case "1030":
                                m_intMoneyControl = int.Parse(dtbResult.Rows[i]["setstatus_int"].ToString().Trim());//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                                break;
                        }
                    }
                }
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
        /// �ύ��ִ���Ƿ���Ҫ����Ա���ſ���
        /// </summary>
        /// <param name="m_intNeedConfirm">�ύ0-����Ҫ 1-��Ҫ</param>
        /// <param name="m_intExeConfirm">ִ��0-����Ҫ 1-��Ҫ</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetTheComfirmControl(out int m_intNeedConfirm, out int m_intExeConfirm)
        {
            m_intNeedConfirm = -1;
            m_intExeConfirm = -1;
            long lngRes = -1;
            string strSQL = "select a.setstatus_int,a.setid_chr from t_sys_setting a where a.setid_chr in ('1028','1029')";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        switch (dtbResult.Rows[i]["setid_chr"].ToString().TrimEnd())
                        {
                            case "1028":
                                if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                                {
                                    m_intNeedConfirm = 0;
                                }
                                else
                                {
                                    m_intNeedConfirm = 1;
                                }
                                break;
                            case "1029":
                                if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                                {
                                    m_intExeConfirm = 0;
                                }
                                else
                                {
                                    m_intExeConfirm = 1;
                                }
                                break;


                        }
                    }
                }
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
        /// ��ȡ���أ����ã�
        /// </summary>
        /// <returns></returns>

        /*ԭ����ȷ���룬��Ҫ����
        public long GetTheHisControl(ArrayList m_arrControl, out DataTable dtbResult)
        {
            long lngRes = -1;
            dtbResult = new DataTable();
            string strSQL = "select a.setstatus_int,a.setid_chr from t_sys_setting a where a.setid_chr in ([control])";
            try
            {
                string m_strControl = "";
                for (int i = 0; i < m_arrControl.Count; i++)
                {
                    m_strControl += "'" + m_arrControl[i].ToString().Trim() + "'";
                    m_strControl += ",";
                }
                m_strControl = m_strControl.TrimEnd(",".ToCharArray());
                strSQL = strSQL.Replace("[control]", m_strControl);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        */
        //�������´���
        [AutoComplete]
        public long GetTheHisControl(System.Collections.Generic.List<string> m_glstControl, out DataTable dtbResult)
        {
            long lngRes = -1;
            dtbResult = new DataTable();
            string strSQL = "select a.setstatus_int,a.setid_chr from t_sys_setting a where a.setid_chr in ([control])";

            int intControl_Count = 0;
            if (m_glstControl != null)
            {
                intControl_Count = m_glstControl.Count;
            }
            if (intControl_Count == 0)
            {
                return lngRes;
            }

            try
            {
                string m_strControl = "";
                System.Text.StringBuilder stbControl = new System.Text.StringBuilder(intControl_Count * 7);
                for (int i = 0; i < intControl_Count - 1; i++)
                {
                    stbControl.Append("'" + m_glstControl[i] + "',");
                    //m_strControl += "'" + m_glstControl[i] + "'";
                    //m_strControl += ",";
                }
                stbControl.Append("'" + m_glstControl[intControl_Count - 1] + "'");//���һ�������С�����
                m_strControl = stbControl.ToString().Trim();
                //m_strControl = m_strControl.TrimEnd(",".ToCharArray());


                strSQL = strSQL.Replace("[control]", m_strControl);
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        //�������´���
        #endregion

        //ԭ���Ĵ���
        [AutoComplete]
        public long m_lngConfirmCurrentOrder(string[] m_arrORDERID, out DataTable m_dtOrder)
        {
            long lngRes = -1;
            m_dtOrder = null;
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                #region new
                string SQL = @"select a.orderid_chr, a.status_int, a.executedate_dat, sysdate,b.ordercateid_chr
  from t_opr_bih_order a, t_bse_bih_orderdic b
 where a.orderdicid_chr = b.orderdicid_chr and a.orderid_chr in ([orderid_chr])";
                string Tmp = "";
                DataTable dt = new DataTable();
                System.Text.StringBuilder stbOrderID = new System.Text.StringBuilder(2150);//ÿ�η�100��orderid, orderid ��18���ַ�,�������źͶ��ţ�����21���ַ����ܹ�2100���ַ���������50���ַ����ܹ�2150���ַ�
                //int intOrderID_Count = m_glstORDERID.Count;
                int intOrderID_Count = m_arrORDERID.Length;
                for (int i = 0; i < intOrderID_Count; i++)
                {
                    //ԭ���Ĵ���Tmp += "'" + m_arrORDERID[i].ToString() + "',";
                    //stbOrderID.Append("'" + m_glstORDERID[i] + "',");
                    stbOrderID.Append("'" + m_arrORDERID[i] + "',");
                    if (i > 0 && i % 100 == 0)
                    {
                        stbOrderID.Remove(stbOrderID.Length - 1, 1);
                        //ԭ���Ĵ���Tmp = Tmp.Substring(0, Tmp.Length - 1);
                        Tmp = stbOrderID.ToString().Trim();
                        string s = SQL.Replace("[orderid_chr]", Tmp);

                        lngRes = HRPService.lngGetDataTableWithoutParameters(s, ref dt);
                        if (m_dtOrder == null)
                        {
                            m_dtOrder = dt.Clone();
                        }
                        //�ɴ���***********************************
                        //for (int j = 0; j < dt.Rows.Count; j++)
                        //{
                        //    m_dtOrder.Rows.Add(dt.Rows[j].ItemArray);
                        //}
                        //*****************************************

                        if (m_dtOrder != null && dt != null && dt.Rows.Count > 0)
                            m_dtOrder.Merge(dt);
                        m_dtOrder.AcceptChanges();

                        //ԭ���Ĵ��� Tmp = "";
                        stbOrderID.Remove(0, stbOrderID.Length);
                    }
                }

                //ԭ���Ĵ���if (Tmp.Trim() != "")
                if (stbOrderID.Length > 0)
                {
                    stbOrderID.Remove(stbOrderID.Length - 1, 1);
                    Tmp = stbOrderID.ToString().Trim();
                    //ԭ���Ĵ��� Tmp = Tmp.Substring(0, Tmp.Length - 1);
                    string s = SQL.Replace("[orderid_chr]", Tmp);

                    lngRes = HRPService.lngGetDataTableWithoutParameters(s, ref dt);
                    if (m_dtOrder == null)
                    {
                        m_dtOrder = dt.Clone();
                    }
                    ////�ɴ���***********************************************
                    //for (int j = 0; j < dt.Rows.Count; j++)
                    //{
                    //    m_dtOrder.Rows.Add(dt.Rows[j].ItemArray);
                    //}
                    //********************************************************
                    if (m_dtOrder != null && dt != null && dt.Rows.Count > 0)
                        m_dtOrder.Merge(dt);
                    m_dtOrder.AcceptChanges();
                }
                #endregion

                #region old
                //                string strUnion = " union all ";
                //                string strSql2 = "";
                //                string strSql = @"select a.orderid_chr, a.status_int, a.executedate_dat, sysdate
                //                                      from t_opr_bih_order a
                //                                     where a.orderid_chr in ([orderid_chr]) ";

                //                string orderid_chr = "";
                //                for (int i = 0; i < m_arrORDERID.Count; i++)
                //                {
                //                    orderid_chr += "'" + m_arrORDERID[i].ToString().Trim() + "'";
                //                    orderid_chr += ",";
                //                    if (i > 0 && i % 100 == 0)
                //                    {
                //                        orderid_chr = orderid_chr.TrimEnd(",".ToCharArray());
                //                        strSql2 += strSql.Replace("[orderid_chr]", orderid_chr) + strUnion;
                //                        orderid_chr = "";
                //                    }
                //                }
                //                if (!orderid_chr.Trim().Equals(""))
                //                {
                //                    orderid_chr = orderid_chr.TrimEnd(",".ToCharArray());
                //                    strSql2 += strSql.Replace("[orderid_chr]", orderid_chr);
                //                }
                //                strSql2 = strSql2.TrimEnd(strUnion.ToCharArray());


                //                lngRes = 0;
                //                lngRes = HRPService.lngGetDataTableWithoutParameters(strSql2, ref m_dtOrder);
                #endregion
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
        /// (����in) 2007-5-8
        /// </summary>
        /// <param name="m_lngMotion_id_int"></param>
        /// <param name="m_dtOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngConfirmCurrentOrder(long m_lngMotion_id_int, out DataTable m_dtOrder)
        {
            long lngRes = -1;
            m_dtOrder = null;
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();

                string strSql = @"  select a.orderid_chr, a.status_int, a.executedate_dat, sysdate
                                      from t_opr_bih_order a, t_aid_bih_motion mon
                                     where mon.orderid_chr = a.orderid_chr and motion_id_int = ?";

                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_lngMotion_id_int;
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtOrder, arrParams);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }


        #region ���ݵ�ǰҽ��VO����ҽ����ִ��

        [AutoComplete]
        public long m_lngExecDrawOrderByOrderID(List<clsBIHCanExecOrder> m_arrExecOrder)
        {
            long lngRes = 0;
            DateTime CreateDate = DateTime.MinValue;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            try
            {
                #region ������ҽ��ִ�е���
                int n = 0;
                strSQL = @"delete from t_opr_bih_orderexecute
                                where orderid_chr = ? and createdate_dat = ? and isincept_int = 0";

                DbType[] dbTypes = new DbType[] {
                        DbType.String,DbType.Date
                        };
                object[][] objValues = new object[2][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrExecOrder.Count];//��ʼ��
                }
                for (int k1 = 0; k1 < m_arrExecOrder.Count; k1++)
                {

                    n = -1;
                    clsBIHCanExecOrder ExeOrder = m_arrExecOrder[k1];
                    //��ˮ��
                    objValues[++n][k1] = ExeOrder.m_strOrderID;
                    objValues[++n][k1] = ExeOrder.m_dtExecutedate;
                    //��������

                }

                if (m_arrExecOrder.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ������ҽ��������ϸ��
                n = 0;
                strSQL = @"delete from t_opr_bih_patientcharge
                                    where orderid_chr = ? and create_dat = ?";

                dbTypes = new DbType[] {
                        DbType.String,DbType.Date
                        };
                objValues = new object[2][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrExecOrder.Count];//��ʼ��
                }
                for (int k1 = 0; k1 < m_arrExecOrder.Count; k1++)
                {

                    n = -1;
                    clsBIHCanExecOrder ExeOrder = (clsBIHCanExecOrder)m_arrExecOrder[k1];
                    //��ˮ��
                    objValues[++n][k1] = ExeOrder.m_strOrderID;
                    objValues[++n][k1] = ExeOrder.m_dtExecutedate;
                    //��������

                }

                if (m_arrExecOrder.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ������ҽ���� ���״�ִ�е�ҽ��
                n = 0;
                strSQL = @"update t_opr_bih_order
                               set executorid_chr =
                                      (select creatorid_chr
                                         from (select   a.creatorid_chr
                                                   from t_opr_bih_orderexecute a
                                                  where a.orderid_chr = ?
                                               order by a.createdate_dat desc)
                                        where rownum = 1),
                                   executor_chr =
                                      (select creator_chr
                                         from (select   a.creator_chr
                                                   from t_opr_bih_orderexecute a
                                                  where a.orderid_chr = ?
                                               order by a.createdate_dat desc)
                                        where rownum = 1),
                                   executedate_dat =
                                      (select createdate_dat
                                         from (select   a.createdate_dat
                                                   from t_opr_bih_orderexecute a
                                                  where a.orderid_chr = ?
                                               order by a.createdate_dat desc)
                                        where rownum = 1)
                             where orderid_chr = ? and startdate_dat <> ? ";

                dbTypes = new DbType[] {
                        DbType.String,DbType.String,DbType.String,DbType.String,DbType.Date

                        };
                objValues = new object[5][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrExecOrder.Count];//��ʼ��
                }

                for (int k1 = 0; k1 < m_arrExecOrder.Count; k1++)
                {

                    n = -1;
                    clsBIHCanExecOrder ExeOrder = (clsBIHCanExecOrder)m_arrExecOrder[k1];
                    //��ˮ��
                    objValues[++n][k1] = ExeOrder.m_strOrderID;
                    objValues[++n][k1] = ExeOrder.m_strOrderID;
                    objValues[++n][k1] = ExeOrder.m_strOrderID;
                    objValues[++n][k1] = ExeOrder.m_strOrderID;
                    objValues[++n][k1] = ExeOrder.m_dtExecutedate;
                    ;
                    //��������


                }

                if (m_arrExecOrder.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion

                #region ������ҽ���� �״�ִ�е�ҽ��
                n = 0;
                strSQL = @"update t_opr_bih_order
                               set executorid_chr = null,
                                   executor_chr = null,
                                   executedate_dat = null,
                                   startdate_dat = null,
                                   status_int = 5
                             where orderid_chr = ? and executedate_dat = ? and startdate_dat = ?";

                dbTypes = new DbType[] {
                        DbType.String,DbType.Date,DbType.Date
                        };
                objValues = new object[3][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_arrExecOrder.Count];//��ʼ��
                }

                for (int k1 = 0; k1 < m_arrExecOrder.Count; k1++)
                {

                    n = -1;
                    clsBIHCanExecOrder ExeOrder = (clsBIHCanExecOrder)m_arrExecOrder[k1];
                    //��ˮ��
                    objValues[++n][k1] = ExeOrder.m_strOrderID;
                    objValues[++n][k1] = ExeOrder.m_dtExecutedate;
                    objValues[++n][k1] = ExeOrder.m_dtExecutedate;
                    //��������


                }

                if (m_arrExecOrder.Count > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                }
                #endregion
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            m_arrExecOrder = null;
            return lngRes;
        }
        #endregion

        #region ��ʱ��ȡ��ǰ��������˵�ҽ����Ϣ
        /// <summary>
        /// ��ʱ��ȡ��ǰ��������˵�ҽ����Ϣ
        /// </summary>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <returns></returns>         
        [AutoComplete]
        public long m_lngGetOrderMessageByTimer(string m_strAreaid_chr, out DataTable m_dtExecOrder)
        {
            long lngRes = -1;
            string SQL = "";

            m_dtExecOrder = new DataTable();

            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;

                DataTable dt = new DataTable();

                #region 1
                SQL = @"select a.orderid_chr, a.executetype_int, a.status_int, a.registerid_chr
                          from t_opr_bih_order a, t_opr_bih_register b
                         where a.registerid_chr = b.registerid_chr
                           and a.sourcetype_int = 0
                           and (   (a.status_int = 1)
                                or (    a.executetype_int = 1
                                    and (   a.status_int = 3
                                         or (    a.status_int = 2
                                             and trunc (a.finishdate_dat) > trunc (sysdate)
                                            )
                                        )
                                   )
                               )
                           and b.status_int = 1
                           and b.pstatus_int in (0, 1, 4)
                           and b.areaid_chr = ? ";

                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr.Trim();

                lngRes = HRPService.lngGetDataTableWithParameters(SQL, ref dt, arrParams);

                m_dtExecOrder = dt.Clone();
                if (m_dtExecOrder != null && dt != null && dt.Rows.Count > 0)
                    m_dtExecOrder.Merge(dt);
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    m_dtExecOrder.Rows.Add(dt.Rows[i].ItemArray);
                //}
                m_dtExecOrder.AcceptChanges();
                #endregion

                #region 2
                SQL = @"select a.orderid_chr, a.executetype_int, a.status_int, a.registerid_chr
                          from t_opr_bih_order a, t_opr_bih_register b
                         where a.registerid_chr = b.registerid_chr
                           and b.pstatus_int in (0, 1, 4)
                           and (   (a.status_int = 1)
                                or (    a.executetype_int = 1
                                    and (   a.status_int = 3
                                         or (    a.status_int = 2
                                             and trunc (a.finishdate_dat) > trunc (sysdate)
                                            )
                                        )
                                   )
                               )
                           and b.status_int = 1
                           and a.createareaid_chr = ?
                           and a.sourcetype_int = 1";

                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr.Trim();

                lngRes = HRPService.lngGetDataTableWithParameters(SQL, ref dt, arrParams);
                if (m_dtExecOrder != null && dt != null && dt.Rows.Count > 0)
                    m_dtExecOrder.Merge(dt);
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    m_dtExecOrder.Rows.Add(dt.Rows[i].ItemArray);
                //}
                m_dtExecOrder.AcceptChanges();
                #endregion


                HRPService.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;


            #region
            //            string strSql = @"select a.orderid_chr, a.executetype_int, a.status_int, a.registerid_chr
            //                                  from t_opr_bih_order a, t_opr_bih_register b
            //                                 where a.registerid_chr = b.registerid_chr
            //                                   and (b.pstatus_int <> 2 and b.pstatus_int <> 3)
            //                                   and ((b.areaid_chr = ? and a.sourcetype_int = 0))
            //                                   and [status_int]  
            //                                union all
            //                                select a.orderid_chr, a.executetype_int, a.status_int, a.registerid_chr
            //                                  from t_opr_bih_order a, t_opr_bih_register b
            //                                 where a.registerid_chr = b.registerid_chr
            //                                   and (b.pstatus_int <> 2 and b.pstatus_int <> 3)
            //                                   and ((a.createareaid_chr = ? and a.sourcetype_int = 1)) 
            //                                   and [status_int] ";

            //            try
            //            {
            //                clsHRPTableService HRPService = new clsHRPTableService();
            //                System.Data.IDataParameter[] arrParams = null;
            //                HRPService.CreateDatabaseParameter(2, out arrParams);
            //                arrParams[0].Value = m_strAreaid_chr.Trim();
            //                arrParams[1].Value = m_strAreaid_chr.Trim();
            //                string m_strStatus_int = "";
            //                m_strStatus_int = @" (   (a.status_int = 1)
            //                                        or (    a.executetype_int = 1
            //                                            and (   a.status_int = 3
            //                                                 or (    a.status_int = 2
            //                                                     and trunc (a.finishdate_dat) > trunc (sysdate)
            //                                                    )
            //                                                )
            //                                           )
            //                                       )";
            //                strSql = strSql.Replace("[status_int]", m_strStatus_int);

            //                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtExecOrder, arrParams);

            //                HRPService.Dispose();
            //            }
            //            catch (Exception objEx)
            //            {
            //                string strTmp = objEx.Message;
            //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //                bool blnRes = objLogger.LogError(objEx);
            //            }

            //            return lngRes;
            #endregion
        }
        #endregion

        #region ��ȡҽ��  ���ݵ�ǰ���� --ҽ�����������ʹ��
        /// <summary>
        /// ��ȡҽ��  ���ݵ�ǰ���� --ҽ�����������ʹ�� 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strAreaid_chr"></param>
        /// <param name="m_dtExecOrder"></param>
        /// <returns></returns>         
        [AutoComplete]
        public long m_lngGetFeelOrderByAreaID(string m_strAreaid_chr, out DataTable m_dtExecOrder)
        {
            long lngRes = -1;
            m_dtExecOrder = new DataTable();
            string strSql = @"select   a.orderid_chr, a.orderdicid_chr, a.registerid_chr, a.patientid_chr,
                                     a.executetype_int, a.recipeno_int, a.name_vchr, a.spec_vchr,
                                     a.execfreqid_chr, a.dosage_dec, a.execfreqname_chr, a.dosageunit_chr,
                                     a.get_dec, a.useunit_chr, a.getunit_chr, a.dosetypeid_chr,
                                     a.dosetypename_chr, a.startdate_dat, a.finishdate_dat,
                                     a.execdeptid_chr, a.execdeptname_chr, a.entrust_vchr, a.parentid_chr,
                                     a.status_int, a.creatorid_chr, a.creator_chr, a.createdate_dat,
                                     a.posterid_chr, a.poster_chr, a.postdate_dat, a.executorid_chr,
                                     a.executor_chr, a.executedate_dat, a.stoperid_chr, a.stoper_chr,
                                     a.stopdate_dat, a.retractorid_chr, a.retractor_chr,
                                     a.retractdate_dat, a.isrich_int, a.ratetype_int, a.isrepare_int,
                                     a.use_dec, a.isneedfeel, a.outgetmeddays_int,
                                     a.assessoridforexec_chr, a.assessorforexec_chr,
                                     a.assessorforexec_dat, a.assessoridforstop_chr,
                                     a.assessorforstop_chr, a.assessorforstop_dat, a.backreason,
                                     a.sendbackid_chr, a.sendbacker_chr, a.sendback_dat, a.isyb_int,
                                     a.sampleid_vchr, a.lisappid_vchr, a.partid_vchr, a.createareaid_chr,
                                     a.createareaname_vchr, a.ifparentid_int, a.confirmerid_chr,
                                     a.confirmer_vchr, a.confirm_dat, a.attachtimes_int, a.doctorid_chr,
                                     a.doctor_vchr, a.curareaid_chr, a.curbedid_chr, a.doctorgroupid_chr,
                                     a.deleterid_chr, a.deletername_vchr, a.delete_dat, a.sign_int,
                                     a.operation_int, a.remark_vchr, a.recipeno2_int, a.feelresult_vchr,
                                     a.feel_int, a.charge_int, a.type_int, a.singleamount_dec,
                                     a.sourcetype_int, b.patientid_chr, c.lastname_vchr, c.registerid_chr,
                                     c.sex_chr, d.bedid_chr, d.code_chr, e.ordercateid_chr,
                                     g.deptname_vchr curareaname, h.sample_type_desc_vchr, j.partname, b.inpatientid_chr, b.cancel_dat 
                                from t_opr_bih_order a,
                                     t_opr_bih_register b,
                                     t_opr_bih_registerdetail c,
                                     t_bse_bed d,
                                     t_bse_bih_orderdic e,
                                     t_bse_deptdesc g,
                                     t_aid_lis_sampletype h,
                                     ar_apply_partlist j
                               where a.registerid_chr = b.registerid_chr
                                 and b.registerid_chr = c.registerid_chr
                                 and b.bedid_chr = d.bedid_chr(+)
                                 and a.orderdicid_chr = e.orderdicid_chr(+)
                                 and a.curareaid_chr = g.deptid_chr(+)
                                 and a.sampleid_vchr = h.sample_type_id_chr(+)
                                 and a.partid_vchr = j.partid(+)
                                 and b.pstatus_int <> 3
                                 and a.createareaid_chr = ?
                                 and a.status_int = 1
                                 and a.isneedfeel = 1
                                 and a.feel_int = 0
                            order by a.curareaid_chr, d.code_chr, a.recipeno_int, a.orderid_chr asc";
            /*<====================================================================*/
            try
            {
                clsHRPTableService HRPService = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams = null;
                HRPService.CreateDatabaseParameter(1, out arrParams);
                arrParams[0].Value = m_strAreaid_chr.Trim();
                lngRes = 0;
                lngRes = HRPService.lngGetDataTableWithParameters(strSql, ref m_dtExecOrder, arrParams);
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

        #region �޸�ҽ������
        /// <summary>
        /// �޸�ҽ������
        /// </summary>
        /// <param name="m_strOrderID">ҽ����</param>
        /// <param name="m_strEntrust">����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveTheEntrust(string m_strRegisterID, int m_intRecipenNo, string m_strEntrust)
        {
            long lngRes = 0;
            long lngAff = 0;
            int n = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                n = 0;
                string strSQL = @"update t_opr_bih_order a
                                       set a.entrust_vchr = ?
                                     where a.registerid_chr = ? and a.recipeno_int = ?";

                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = m_strEntrust;
                arrParams[1].Value = m_strRegisterID;
                arrParams[2].Value = m_intRecipenNo;

                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
                objHRPSvc.Dispose();
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

        #region �޸�ҽ������
        /// <summary>
        /// �޸�ҽ������
        /// </summary>
        /// <param name="m_strRegisterID">������ˮ�ǼǺ�</param>
        /// <param name="m_intRecipenNo">ҽ������</param>
        /// <param name="ATTACHTIMES_INT">���δ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveTheATTACHTIMES_INT(string m_strRegisterID, int m_intRecipenNo, int ATTACHTIMES_INT)
        {
            long lngRes = 0;
            long lngAff = 0;
            int n = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                n = 0;
                string strSQL = @"update t_opr_bih_order a
                                       set a.attachtimes_int = ?
                                     where a.registerid_chr = ? and a.recipeno_int = ?";

                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = ATTACHTIMES_INT;
                arrParams[1].Value = m_strRegisterID;
                arrParams[2].Value = m_intRecipenNo;

                lngRes = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
                objHRPSvc.Dispose();
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

        #region
        /// <summary>
        /// ��ȡ���ʶ�ţ�m_arrREGISTERID_CHR��m_arrORDERID_CHRֻ����һ��������Ŀ>0��
        /// </summary>
        /// <param name="m_arrREGISTERID_CHR">������ˮ������</param>
        /// <param name="m_arrORDERID_CHR">ҽ����ˮ������</param>
        /// <param name="motion_id_int">���β�����ʶ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMotionID(List<string> m_arrREGISTERID_CHR, List<string> m_arrORDERID_CHR, out long seq_motion_id_int)
        {
            long lngAff = 0;
            long lngRes = 0;
            seq_motion_id_int = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            if (m_arrREGISTERID_CHR.Count <= 0 && m_arrORDERID_CHR.Count <= 0)
            {
                return 0;
            }
            List<string> m_arrList = new List<string>();
            int type = 1;//1-���²�����ˮ�ţ�2-���²���ҽ����
            if (m_arrREGISTERID_CHR.Count > 0)
            {
                type = 1;
                m_arrList = m_arrREGISTERID_CHR;
            }
            else if (m_arrORDERID_CHR.Count > 0)
            {
                type = 2;
                m_arrList = m_arrORDERID_CHR;
            }

            try
            {
                #region ��ȡ����к�
                //                    strSQL = @"  
                //                        select  seq_motion_id_int.NEXTVAL motion_id_int from dual
                //                        ";
                //                    DataTable dtbResult = new DataTable();
                //                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                //                    if (lngRes > 0)
                //                    {
                //                        seq_motion_id_int = long.Parse(dtbResult.Rows[0]["motion_id_int"].ToString().Trim());                      
                //                    }

                seq_motion_id_int = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssfffff"));

                #endregion

                strSQL = @"insert into t_aid_bih_motion (motion_id_int, registerid_chr, orderid_chr, createdate_dat)
                                                     values (?, ?, ?, sysdate)";

                DbType[] dbTypes = new DbType[] {

                        DbType.Int64,DbType.String,DbType.String
                        };
                object[][] objValues = new object[3][];
                if (m_arrList.Count > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[m_arrList.Count];//��ʼ��
                    }
                    int n = 0;
                    for (int k1 = 0; k1 < m_arrList.Count; k1++)
                    {
                        n = -1;

                        objValues[++n][k1] = seq_motion_id_int;
                        objValues[++n][k1] = type == 1 ? m_arrList[k1] : "";
                        objValues[++n][k1] = type == 2 ? m_arrList[k1] : "";
                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

                }



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


        #region ɾ��Ƥ�Է���
        /// <summary>
        /// ɾ��Ƥ�Է���
        /// </summary>
        /// <param name="strOrderID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteFeelCharge(string strOrderID)
        {
            long lngRes = -1;
            string strSQL = @" delete from t_opr_bih_patientcharge where (pstatus_int <3)
                                                                   and bmstatus_int = 9
                                                                   and orderid_chr = ? ";
            long lngAfter = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = strOrderID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAfter, param);
                objHRPSvc.Dispose();
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
        [AutoComplete]
        public long m_lngFeelCharge(string strOrderID, string strChargeItemID, int ExecuteType_Int, string CURAREAID_CHR, string CURBEDID_CHR, string CONFIRMERID_CHR, bool isChildPrice)
        {
            DataTable dtbResult3;
            long lngRes = -1;
            try
            {
                #region �����շ���Ŀ��ϸ��
                //�����շ���Ŀ��ϸ��
                string strSQL = @"  select a.orderid_chr, a.orderdicid_chr, a.chargeitemid_chr, a.clacarea_chr,
       a.createarea_chr, a.chargeitemname_chr, a.spec_vchr, a.unit_vchr,
       a.amount_dec, a.unitprice_dec, a.creatorid_chr, a.creator_vchr,
       a.createdate_dat, a.flag_int, a.continueusetype_int, c.ratetype_int,
       c.patientid_chr, c.registerid_chr, c.executetype_int,c.doctorid_chr,c.creatorid_chr as ordercreatorid_chr,c.creator_chr as ordercreator_chr,c.doctor_vchr,
       d.itemipcalctype_chr, d.itemipinvtype_chr, d.isrich_int,
       d.isselfpay_chr, c.isrepare_int, f.dosageviewtype, f.createchargetype
  from t_opr_bih_orderchargedept a, 
       t_opr_bih_order c,
       t_bse_chargeitem d,
       t_aid_bih_ordercate f
 where a.orderid_chr = c.orderid_chr 
   and a.chargeitemid_chr = d.itemid_chr
   and d.ordercateid_chr = f.ordercateid_chr(+)
   and d.itemid_chr in ([ItemID])
   and c.orderid_chr = ? ";
                strSQL = strSQL.Replace("[ItemID]", strChargeItemID);
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] arrParams3 = null;
                objHRPSvc.CreateDatabaseParameter(1, out arrParams3);
                arrParams3[0].Value = strOrderID;

                dtbResult3 = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult3, arrParams3);
                if (lngRes > 0 && dtbResult3.Rows.Count > 0)
                {
                    string strDicID = "";          //--������Ŀ��ˮ��
                    string strOrderCateID = "";    // --ҽ������ID
                    string strChargeID = "";       //--
                    string strDefaultItemID = "";  //--���շ���ĿID

                    int dmlDefaultAmount = 0;  //-һ������

                    int intRateType = 0;//*���ñ�־	{1=ȫ�Ʒ�;2=�÷��Ʒ�;3=���Ʒ�}
                    int intCreateType = 0;        //-- 3 ����(ҽ��)    1�Զ�(ҽ��)
                    int intIsRich = 0;                  //*--�շ���Ŀ�Ĺ��ر�־
                    int intChargeItemStatus = 0;        //*--����״̬��0-��ȷ�ϣ�1-���ᣩ
                    int intRATETYPE_INT = 0;//�Ƿ�Ʒ� 0-���Ʒ� 1-�Ʒ�
                    decimal dmlAmount = 0;//*
                    string strPatientID = "";      //--����ID*
                    string strRegisterID = "";     //--��Ժ�Ǽ�ID*
                    int intExecuteType = 0;             //*--ҽ��ִ������{ִ������{1=����;2=��ʱ;3=��Ժ��ҩ}}
                    int m_intOrderExecType_Int = 0;      //*--���벡�˷�����ϸ���ORDEREXECTYPE_INT��ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}��
                    string strCalcCateID = "";          //*--��ĿסԺ�������
                    string strINvCateID = "";           //*--��ĿסԺ��Ʊ���
                    decimal dmlPrice = 0;          //*--סԺ����(=��Ŀ�۸�/��װ��)
                    int intDosageView = 0;              //*--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1

                    int intCreateChargeType = 0; //-- ������Ч��ʽ{1=ִ��ҽ��ʱ��Ч;2=ִ�п��ҽ���ҽ��ʱ��Ч}
                    int ACTIVATETYPE_INT = 1;//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�}

                    string ItemID_Chr = "";
                    string ItemName_Vchr = "";  //�շ���Ŀ
                    string ItemIPUnit_Chr = "";
                    string CLACAREA_CHR = "";
                    string CREATEAREA_CHR = "";
                    string strCreatorId = "";//����ҽ��
                    string strDoctorId = "";//����ҽ��
                    string strCreator = "";//����ҽ������
                    string strDoctor = "";//����ҽ������
                    /* <<============================= */
                    int CONTINUEUSETYPE_INT = 0;//�������� {0=������;1=ȫ������;2-��������}0������仯��1����������仯
                    string ISSELFPAY_CHR = "";//�Ƿ��Է���Ŀ("T","F")
                    long lngAff = -1;
                    for (int m = 0; m < dtbResult3.Rows.Count; m++)
                    {
                        int m_intNEEDCONFIRM_INT = 0;
                        strPatientID = clsConverter.ToString(dtbResult3.Rows[m]["patientid_chr"].ToString());
                        strRegisterID = clsConverter.ToString(dtbResult3.Rows[m]["registerid_chr"].ToString());
                        strOrderID = clsConverter.ToString(dtbResult3.Rows[m]["orderid_chr"].ToString());
                        intExecuteType = clsConverter.ToInt(dtbResult3.Rows[m]["ExecuteType_Int"].ToString());
                        strCalcCateID = clsConverter.ToString(dtbResult3.Rows[m]["ItemIPCalcType_Chr"].ToString());
                        strINvCateID = clsConverter.ToString(dtbResult3.Rows[m]["ItemIpInvType_Chr"].ToString());
                        ItemID_Chr = clsConverter.ToString(dtbResult3.Rows[m]["chargeitemid_chr"].ToString());
                        ItemName_Vchr = clsConverter.ToString(dtbResult3.Rows[m]["chargeitemname_chr"].ToString());
                        ItemIPUnit_Chr = clsConverter.ToString(dtbResult3.Rows[m]["unit_vchr"].ToString());
                        dmlPrice = clsConverter.ToDecimal(dtbResult3.Rows[m]["unitprice_dec"].ToString());
                        dmlAmount = clsConverter.ToDecimal(dtbResult3.Rows[m]["amount_dec"].ToString());
                        CLACAREA_CHR = clsConverter.ToString(dtbResult3.Rows[m]["clacarea_chr"].ToString());
                        CREATEAREA_CHR = clsConverter.ToString(dtbResult3.Rows[m]["createarea_chr"].ToString());
                        intIsRich = clsConverter.ToInt(dtbResult3.Rows[m]["isrich_int"].ToString());
                        //intIsRepare = clsConverter.ToInt(dtbResult3.Rows[i]["IsRepare_Int"].ToString());
                        intRateType = clsConverter.ToInt(dtbResult3.Rows[m]["RateType_Int"].ToString());
                        intDosageView = clsConverter.ToInt(dtbResult3.Rows[m]["dosageviewtype"].ToString());
                        intRATETYPE_INT = clsConverter.ToInt(dtbResult3.Rows[m]["RATETYPE_INT"].ToString());
                        // intCreateChargeType = clsConverter.ToInt(dtbResult.Rows[0]["CreateChargeType"].ToString());
                        ISSELFPAY_CHR = clsConverter.ToString(dtbResult3.Rows[m]["ISSELFPAY_CHR"].ToString().Trim());
                        strCreatorId = clsConverter.ToString(dtbResult3.Rows[m]["ordercreatorid_chr"].ToString());
                        strDoctorId = clsConverter.ToString(dtbResult3.Rows[m]["doctorid_chr"].ToString());
                        strCreator = clsConverter.ToString(dtbResult3.Rows[m]["ordercreator_chr"].ToString());
                        ;
                        strDoctor = clsConverter.ToString(dtbResult3.Rows[m]["doctor_vchr"].ToString());
                        ;
                        if (!dtbResult3.Rows[m]["CONTINUEUSETYPE_INT"].ToString().Trim().Equals(""))
                            CONTINUEUSETYPE_INT = clsConverter.ToInt(dtbResult3.Rows[m]["CONTINUEUSETYPE_INT"].ToString().Trim());
                        //���ã�����
                        if (CONTINUEUSETYPE_INT == 1 && ExecuteType_Int == 1)
                        {
                            continue;
                        }
                        /*<===========================*/
                        //  intExecuteType--ҽ��ִ������{ִ������{1=����;2=��ʱ;3=��Ժ��ҩ}}
                        //  m_intOrderExecType_Int--���벡�˷�����ϸ���ORDEREXECTYPE_INT��ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}��

                        switch (intExecuteType)
                        {
                            case 1:
                                m_intOrderExecType_Int = 1;
                                break;
                            case 2:
                                m_intOrderExecType_Int = 2;
                                break;
                            case 3:
                                m_intOrderExecType_Int = 4;
                                break;

                        }

                        /*<===========================*/
                        //intRateType--��ȡ�ѷ��ñ�־	{1=ȫ�Ʒ�;2=�÷��Ʒ�;3=���Ʒ�}
                        //if (intRateType == 1 || intRateType == 2)
                        //{
                        //    return;
                        //}
                        //--������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1
                        if (intDosageView == 2)
                        {
                            dmlDefaultAmount = 1;
                        }
                        if (m_intNEEDCONFIRM_INT == 1)//ҽ��ִ�е����Ƿ���Ҫȷ�ϱ�־
                        {
                            intChargeItemStatus = 0;
                        }
                        //*--����״̬��0-��ȷ�ϣ�1-���ᣩ
                        if (intIsRich == 1 || ISSELFPAY_CHR.ToUpper().Equals("T"))
                        {
                            intChargeItemStatus = 0;
                        }
                        if (m_intNEEDCONFIRM_INT == 0 && intIsRich == 0 && !ISSELFPAY_CHR.ToUpper().Equals("T"))
                        {
                            intChargeItemStatus = 1;
                        }
                        //
                        if (intChargeItemStatus == 0)
                        {
                            //ACTIVATETYPE_INT = 3;
                            //m_blUpdate = true;//����ҽ��ִ�е���־(true- ����Ϊ����Ҫ������ˣ�false-������;

                        }
                        else if (intChargeItemStatus == 1)
                        {
                            //ACTIVATETYPE_INT = 4;
                        }
                        /*<==============================*/
                        //����ҽ��ִ�к���Ч����Ϊ3=ȷ�ϼ���
                        if (intIsRich == 1)
                        {
                            ACTIVATETYPE_INT = 3;
                        }
                        else
                        {
                            ACTIVATETYPE_INT = 1;
                        }
                        //�Է�ҽ��ִ�к���Ч����Ҳ��4=ȷ���շ�
                        if (ISSELFPAY_CHR.Trim().Equals("T"))
                        {
                            ACTIVATETYPE_INT = 4;
                        }
                        //    --���������ϸ��¼
                        int n;
                        strSQL = @" insert into t_opr_bih_patientcharge
            (pchargeid_chr, patientid_chr, registerid_chr, chargeactive_dat,
             orderid_chr, needconfirm_int, orderexectype_int,
             orderexecid_chr, calccateid_chr, invcateid_chr,
             chargeitemid_chr, chargeitemname_chr, unit_vchr, unitprice_dec,
             amount_dec, discount_dec, ismepay_int, createtype_int,
             creator_chr, create_dat, status_int, pstatus_int, clacarea_chr,
             createarea_chr, isrich_int, activatetype_int, curareaid_chr,
             curbedid_chr,bmstatus_int,chargedoctorid_chr,doctorid_chr,chargedoctor_vchr,doctor_vchr)
     values (lpad (seq_pchargeid.nextval, 18, '0'), ?, ?, ?,
             ?, ?, ?,
             ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?, ?,
             ?, ?, ?, ?,
             ? ,9,?,?,?,?) ";
                        n = -1;
                        System.Data.IDataParameter[] arrParams4 = null;
                        objHRPSvc.CreateDatabaseParameter(32, out arrParams4);
                        //n++; arrParams[0].Value = strChargeID.Trim();//PChargeID_Chr
                        n++;
                        arrParams4[n].Value = strPatientID.Trim();//PatientID_Chr
                        n++;
                        arrParams4[n].Value = strRegisterID.Trim();//RegisterID_Chr
                        //if (intChargeItemStatus == 0)
                        //{
                        //    n++; arrParams4[n].Value = null;////CHARGEACTIVE_DAT ������Ч����
                        //}
                        //else
                        //{
                        //    n++; arrParams4[n].Value = DateTime.Now; ;////CHARGEACTIVE_DAT ������Ч����
                        //}
                        n++;
                        arrParams4[n].Value = DateTime.Now;
                        ;////CHARGEACTIVE_DAT ������Ч����
                        n++;
                        arrParams4[n].Value = strOrderID.Trim();//OrderID_Chr
                        if (intChargeItemStatus == 0)
                        {
                            n++;
                            arrParams4[n].Value = 1;//NEEDCONFIRM_INT�Ƿ���Ҫ������� 0-�� 1-��
                        }
                        else
                        {
                            n++;
                            arrParams4[n].Value = 0;//NEEDCONFIRM_INT�Ƿ���Ҫ������� 0-�� 1-��
                        }

                        n++;
                        arrParams4[n].Value = m_intOrderExecType_Int;//OrderExecType_Int ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}
                        n++;
                        arrParams4[n].Value = strOrderID.Trim();//OrderExecID_Chr
                        n++;
                        arrParams4[n].Value = strCalcCateID.Trim();//CalCCateID_Chr  ���ú������id {=�շ���Ŀ����.id}
                        n++;
                        arrParams4[n].Value = strINvCateID.Trim();//InvCateID_Chr    ���÷�Ʊ���id {=�շ���Ŀ����.id}
                        n++;
                        arrParams4[n].Value = ItemID_Chr.Trim();//ChargeItemID_Chr

                        n++;
                        arrParams4[n].Value = ItemName_Vchr.Trim();//ChargeItemName_Chr
                        n++;
                        arrParams4[n].Value = ItemIPUnit_Chr.Trim();//Unit_Vchr סԺ��λ{=�շ���Ŀ.סԺ��λ}
                        n++;
                        arrParams4[n].Value = dmlPrice;//UnitPrice_Dec  סԺ����{=�շ���Ŀ.סԺ����}
                        n++;
                        arrParams4[n].Value = dmlAmount;//AMount_Dec    ����
                        n++;
                        arrParams4[n].Value = 1;//DisCount_Dec=1���ۿ۱���

                        if (ISSELFPAY_CHR.ToUpper().Equals("T"))
                        {
                            n++;
                            arrParams4[n].Value = 1;//IsMepay_Int=1�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
                        }
                        else
                        {
                            n++;
                            arrParams4[n].Value = 0;//IsMepay_Int=0�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
                        }
                        n++;
                        arrParams4[n].Value = intCreateType;//CreateType_Int ¼������ {1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
                        n++;
                        arrParams4[n].Value = CONFIRMERID_CHR;//Creator_Chr
                        n++;
                        arrParams4[n].Value = DateTime.Now;//Create_Dat
                        n++;
                        arrParams4[n].Value = 1;// Status_Int��Ч״̬{1=��Ч;0=��Ч;-1=��ʷ}


                        n++;
                        arrParams4[n].Value = intChargeItemStatus;//PStatus_Int
                        n++;
                        arrParams4[n].Value = CLACAREA_CHR.Trim();//ClacArea_Chr
                        n++;
                        arrParams4[n].Value = CREATEAREA_CHR.Trim();//CreateArea_Chr
                        n++;
                        arrParams4[n].Value = intIsRich;//ISRICH_INT
                        n++;
                        arrParams4[n].Value = ACTIVATETYPE_INT;//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�}
                        n++;
                        arrParams4[n].Value = CURAREAID_CHR;
                        n++;
                        arrParams4[n].Value = CURBEDID_CHR;
                        n++;
                        arrParams4[n].Value = strCreatorId;//����ҽ��  
                        n++;
                        arrParams4[n].Value = strDoctorId;//����ҽ��
                        n++;
                        arrParams4[n].Value = strCreator;//����ҽ��  ����  
                        n++;
                        arrParams4[n].Value = strDoctor;//����ҽ������ 
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams4);
                    }
                }
                #endregion
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

        #region ��ȡҩƷ�����
        /// <summary>
        /// ��ȡҩƷ�����
        /// </summary>
        /// <param name="p_dtnMedID">ҩƷID (ҩ��ID,(ҩƷID))</param>
        /// <param name="p_dtnKCL">ҩƷ�����(ҩ��ID*ҩƷID,�����)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineKC(Dictionary<string, List<string>> dicMedID, out Dictionary<string, double> dicKCL, out clsDsStorageVO[] dsStorageVOArr)
        {
            long ret = -1;
            dicKCL = new Dictionary<string, double>();
            dsStorageVOArr = null;
            if (dicMedID == null || dicMedID.Count <= 0) return ret;
            clsHRPTableService svc = null;
            try
            {
                DataTable dtResult = null;
                DataTable dtTemp = null;
                svc = new clsHRPTableService();

                string Sql = @"select distinct a.seriesid_int,
                                   a.medicineid_chr,
                                   a.packqty_dec,
                                   a.validperiod_dat,
                                   a.drugstoreid_chr,
                                   a.iprealgross_int,
                                   a.ipavailablegross_num,
                                   a.dsinstoragedate_dat,
                                   b.ipchargeflg_int,
                                   b.medicinename_vchr,
                                   a.oprealgross_int  
                              from t_ds_storage_detail a, t_bse_medicine b, t_ds_storage c
                             where a.medicineid_chr = b.medicineid_chr
                               and a.medicineid_chr = c.medicineid_chr
                               and a.drugstoreid_chr = c.drugstoreid_chr
                               and a.canprovide_int = 1
                               and c.noqtyflag_int = 0
                               and c.ifstop_int = 0                               
                               and a.medicineid_chr in ({0}) 
                               and a.drugstoreid_chr = '{1}'";

                int times = 0;
                int perCount = 100;     // 100ҩƷidһ��.in���
                int remainCount = 0;
                string medId = string.Empty;
                List<string> lstMedId = new List<string>();
                foreach (string pharmacyID in dicMedID.Keys)
                {
                    times = dicMedID[pharmacyID].Count / perCount;
                    for (int i = 0; i < times; i++)
                    {
                        medId = string.Empty;
                        for (int j = 0; j < perCount; j++)
                        {
                            medId += "'" + dicMedID[pharmacyID][j + i * perCount] + "',";
                        }
                        lstMedId.Add(medId.TrimEnd(','));
                    }
                    // ʣ��
                    remainCount = dicMedID[pharmacyID].Count - perCount * times;
                    if (remainCount > 0)
                    {
                        medId = string.Empty;
                        for (int i = 0; i < remainCount; i++)
                        {
                            medId += "'" + dicMedID[pharmacyID][i + times * perCount] + "',";
                        }
                        lstMedId.Add(medId.TrimEnd(','));
                    }
                    foreach (string item in lstMedId)
                    {
                        string Sql1 = string.Format(Sql, item.TrimEnd(','), pharmacyID);
                        svc.lngGetDataTableWithoutParameters(Sql1, ref dtTemp);
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            if (dtResult == null) dtResult = dtTemp.Clone();

                            if (dtResult != null && dtTemp != null && dtTemp.Rows.Count > 0)
                                dtResult.Merge(dtTemp);
                            dtResult.AcceptChanges();
                        }
                        dtTemp = null;
                    }
                    lstMedId.Clear();
                }
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    string key = string.Empty;
                    DataRow[] drr = null;
                    drr = dtResult.Select("iprealgross_int > 0.00 ", "validperiod_dat asc,dsinstoragedate_dat asc,seriesid_int asc,drugstoreid_chr asc,medicineid_chr asc");    // ��seriesid_int����
                    List<clsDsStorageVO> lstStorage = new List<clsDsStorageVO>();
                    clsDsStorageVO vo = null;
                    foreach (DataRow dr in drr)
                    {
                        vo = new clsDsStorageVO();
                        vo.m_intSeriesID = Convert.ToInt32(dr["seriesid_int"]);
                        vo.m_strMedicineID = dr["medicineid_chr"].ToString();
                        vo.m_strPharmacyID = dr["drugstoreid_chr"].ToString();
                        vo.m_dblPackqty = dr["packqty_dec"] == DBNull.Value ? 1 : Convert.ToDouble(dr["packqty_dec"]);
                        vo.m_dtmValidperiod = Convert.ToDateTime(dr["validperiod_dat"]);
                        vo.m_dblOriginalStock = Convert.ToDouble(dr["ipavailablegross_num"]);
                        vo.m_dblIpavailableGross = Convert.ToDouble(dr["ipavailablegross_num"]);
                        vo.m_dbIprealgross = Convert.ToDouble(dr["iprealgross_int"]);
                        vo.m_intIpChargeFlg = Convert.ToInt32(dr["ipchargeflg_int"]);
                        vo.medName = dr["medicinename_vchr"].ToString();
                        vo.m_dbOprealgross = Convert.ToDouble(dr["oprealgross_int"]);
                        lstStorage.Add(vo);

                        key = vo.m_strPharmacyID + "*" + vo.m_strMedicineID;    // ��ϼ�(ҩ��ID*ҩƷID)
                        if (dicKCL.ContainsKey(key))
                        {
                            dicKCL[key] += vo.m_dbIprealgross;
                        }
                        else
                        {
                            dicKCL.Add(key, vo.m_dbIprealgross);
                        }
                    }
                    if (lstStorage.Count > 0) dsStorageVOArr = lstStorage.ToArray();
                    dtResult.Dispose();
                    ret = lstStorage.Count;
                }
            }
            catch (Exception ex)
            {
                clsLogText log = new clsLogText();
                log.LogError(ex.Message);
            }
            return ret;
        }

        #endregion

        #region ʹ�����ö�ͯ�۸�
        /// <summary>
        /// ʹ�����ö�ͯ�۸�
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public bool IsUseChildPrice()
        {
            try
            {
                DataTable dt = null;
                string Sql = @"select t.parmvalue_vchr from t_bse_sysparm t where t.parmcode_chr = '9015' and t.status_int = 1";
                clsHRPTableService svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                svc = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    int val = 0;
                    int.TryParse(dt.Rows[0]["parmvalue_vchr"].ToString(), out val);
                    if (val == 1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }
            return false;
        }
        #endregion

        #region ���Ҷ�ͯ�۸��Ƿ������;����
        /// <summary>
        /// ���Ҷ�ͯ�۸��Ƿ������;����
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DateTime? GetMiddChargeDate(string registerId)
        {
            DateTime? dtmCharge = null;
            try
            {
                DataTable dt = null;
                string Sql = @"select t.operdate_dat
                                  from t_opr_bih_charge t
                                 where t.registerid_chr = ? 
                                   and t.class_int = 1
                                   and t.status_int = 1";
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = registerId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["operdate_dat"] != DBNull.Value)
                    {
                        dtmCharge = Convert.ToDateTime(dt.Rows[0]["operdate_dat"]);
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }
            return dtmCharge;
        }
        #endregion
    }
}

