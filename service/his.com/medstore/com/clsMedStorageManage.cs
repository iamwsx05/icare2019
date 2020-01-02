using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMedStorageManage : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public long m_lngAccountAdjustPriceInfo(DataSet ds)
        {
            return 1;
        }

        [AutoComplete]
        public long m_lngAddNewAdjustPriceInfo(DataSet ds, DataSet p_DelDs, out string strBillID)
        {
            string str;
            clsHRPTableService service;
            long num;
            int num2;
            Exception exception;
            clsLogText text;
            DataRow[] rowArray;
            int num3;
            string str2;
            long num4;
            bool flag;
            string[] strArray;
            str = "";
            strBillID = "";
            service = new clsHRPTableService();
            num = 1L;
            if (p_DelDs == null)
            {
                goto Label_01A3;
            }
            num2 = 0;
            goto Label_00EE;
        Label_002C:
            str = "DELETE FROM t_opr_medicinepricechgappl WHERE MEDICINEPRICECHGAPPLID_CHR='" + p_DelDs.Tables["t_opr_medicinepricechgappl"].Rows[num2]["MEDICINEPRICECHGAPPLID_CHR"].ToString() + "'";
        Label_0067:
            try
            {
                num = service.DoExcute(str);
                goto Label_008A;
            }
            catch (Exception exception1)
            {
            Label_0073:
                exception = exception1;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_008A;
            }
        Label_008A:
            str = "DELETE FROM t_opr_medicinepricechgapplde WHERE MEDICINEPRICECHGAPPLID_CHR='" + p_DelDs.Tables["t_opr_medicinepricechgappl"].Rows[num2]["MEDICINEPRICECHGAPPLID_CHR"].ToString() + "'";
        Label_00C5:
            try
            {
                num = service.DoExcute(str);
                goto Label_00E8;
            }
            catch (Exception exception2)
            {
            Label_00D1:
                exception = exception2;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_00E8;
            }
        Label_00E8:
            num2 += 1;
        Label_00EE:
            if (num2 < p_DelDs.Tables["t_opr_medicinepricechgappl"].Rows.Count)
            {
                goto Label_002C;
            }
            num2 = 0;
            goto Label_017C;
        Label_0118:
            str = "DELETE FROM t_opr_medicinepricechgapplde WHERE MEDICINEPRICECHGAPPLID_CHR='" + p_DelDs.Tables["t_opr_medicinepricechgapplde"].Rows[num2]["MEDICINEPRICECHGAPPLID_CHR"].ToString() + "'";
        Label_0153:
            try
            {
                num = service.DoExcute(str);
                goto Label_0176;
            }
            catch (Exception exception3)
            {
            Label_015F:
                exception = exception3;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0176;
            }
        Label_0176:
            num2 += 1;
        Label_017C:
            if (num2 < p_DelDs.Tables["t_opr_medicinepricechgapplde"].Rows.Count)
            {
                goto Label_0118;
            }
        Label_01A3:
            num2 = 0;
            goto Label_060F;
        Label_01AA:
            if (ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["PSTATUS_INT"].ToString() != "2" && ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["PSTATUS_INT"].ToString() != "3")
            {
                goto Label_0227;
            }
            goto Label_060B;
        Label_0227:
            str = "DELETE FROM t_opr_medicinepricechgappl WHERE MEDICINEPRICECHGAPPLID_CHR='" + ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["MEDICINEPRICECHGAPPLID_CHR"].ToString() + "'";
        Label_0261:
            try
            {
                num = service.DoExcute(str);
                goto Label_0284;
            }
            catch (Exception exception4)
            {
            Label_026D:
                exception = exception4;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0284;
            }
        Label_0284:
            str = "DELETE FROM t_opr_medicinepricechgapplde WHERE MEDICINEPRICECHGAPPLID_CHR='" + ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["MEDICINEPRICECHGAPPLID_CHR"].ToString() + "'";
        Label_02BF:
            try
            {
                num = service.DoExcute(str);
                goto Label_02E2;
            }
            catch (Exception exception5)
            {
            Label_02CB:
                exception = exception5;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_02E2;
            }
        Label_02E2:
            strBillID = service.m_strGetNewID("t_opr_medicinepricechgappl", "MEDICINEPRICECHGAPPLID_CHR", 10);
            str = "INSERT INTO t_opr_medicinepricechgappl(MEDICINEPRICECHGAPPLID_CHR,MEDICINEPRICECHGAPPLNO_CHR,PERIODID_CHR,APPLDATE_DAT, PSTATUS_INT,MEMO_VCHR,CREATORID_CHR,CREATEDATE_DAT) VALUES('" + strBillID + "','" + ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["MEDICINEPRICECHGAPPLNO_CHR"].ToString() + "','" + ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["PERIODID_CHR"].ToString() + "',to_date('" + ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["APPLDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),1,'" + ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["MEMO_VCHR"].ToString() + "','" + ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["CREATORID_CHR"].ToString() + "',sysdate)";
        Label_0437:
            try
            {
                num = service.DoExcute(str);
                goto Label_045A;
            }
            catch (Exception exception6)
            {
            Label_0443:
                exception = exception6;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_045A;
            }
        Label_045A:
            rowArray = ds.Tables["t_opr_medicinepricechgapplde"].Select("MEDICINEPRICECHGAPPLID_CHR='" + ds.Tables["t_opr_medicinepricechgappl"].Rows[num2]["MEDICINEPRICECHGAPPLID_CHR"].ToString() + "'");
            num3 = 0;
            goto Label_05F9;
        Label_04B3:
            str2 = service.m_strGetNewID("t_opr_medicinepricechgapplde", "MEDICINEPRICECHGAPPLDEID_CHR", 10);
            str = "INSERT INTO t_opr_medicinepricechgapplde(MEDICINEPRICECHGAPPLDEID_CHR,ROWNO_CHR,MEDICINEID_CHR,UNITID_CHR, CURPRICE_MNY,CHANGEPRICE_MNY,MEDICINEPRICECHGAPPLID_CHR,TYPEID_CHR)  VALUES('" + str2 + "','" + rowArray[num3]["ROWNO_CHR"].ToString() + "','" + rowArray[num3]["MEDICINEID_CHR"].ToString() + "','" + rowArray[num3]["UNITID_CHR"].ToString() + "'," + rowArray[num3]["CURPRICE_MNY"].ToString() + "," + rowArray[num3]["CHANGEPRICE_MNY"].ToString() + ",'" + strBillID + "','" + rowArray[num3]["TYPEID_CHR"].ToString() + "')";
        Label_05CE:
            try
            {
                num = service.DoExcute(str);
                goto Label_05F1;
            }
            catch (Exception exception7)
            {
            Label_05DA:
                exception = exception7;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_05F1;
            }
        Label_05F1:
            num3 += 1;
        Label_05F9:
            if (num3 < ((int)rowArray.Length))
            {
                goto Label_04B3;
            }
        Label_060B:
            num2 += 1;
        Label_060F:
            if (num2 < ds.Tables["t_opr_medicinepricechgappl"].Rows.Count)
            {
                goto Label_01AA;
            }
            num4 = num;
        Label_063A:
            return num4;
        }

        [AutoComplete]
        public long m_lngAuditAdjustPriceInfo(clsPriceChgeAppl objApplVO, clsPriceChgeApplDe[] objApplDeArr)
        {
            string str;
            clsHRPTableService service;
            long num;
            Exception exception;
            clsLogText text;
            DataTable table;
            int num2;
            double num3;
            int num4;
            DataTable table2;
            string str2;
            long num5;
            string[] strArray;
            bool flag;
            object[] objArray;
            str = "";
            service = new clsHRPTableService();
            num = 1L;
            str = "UPDATE t_opr_medicinepricechgappl SET PSTATUS_INT = 2,ADUITEMP_CHR = '" + objApplVO.m_strADUITEMP_CHR + "',ADUITDATE_DAT=sysdate WHERE MEDICINEPRICECHGAPPLID_CHR='" + objApplVO.m_strMEDICINEPRICECHGAPPLID_CHR + "'";
        Label_004F:
            try
            {
                num = service.DoExcute(str);
                goto Label_0070;
            }
            catch (Exception exception1)
            {
            Label_005B:
                exception = exception1;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0070;
            }
        Label_0070:
            table = new DataTable();
            if ((int)objApplDeArr.Length <= 0)
            {
                goto Label_07A6;
            }
            num2 = 0;
            goto Label_0793;
        Label_0093:
            str = "select * from T_OPR_STORAGEMEDDETAIL where MEDICINEID_CHR='" + objApplDeArr[num2].m_strMEDICINEID_CHR + "'";
        Label_00AD:
            try
            {
                num = service.DoGetDataTable(str, ref table);
                goto Label_00D0;
            }
            catch (Exception exception2)
            {
            Label_00BB:
                exception = exception2;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_00D0;
            }
        Label_00D0:
            num3 = 0.0;
            if (table.Rows.Count <= 0)
            {
                goto Label_0560;
            }
            num4 = 0;
            goto Label_0546;
        Label_0100:
            if (table.Rows[num4]["FLAG_INT"].ToString() != "1")
            {
                goto Label_033C;
            }
            num3 = (objApplDeArr[num2].m_dblCHANGEPRICE_MNY - double.Parse(objApplDeArr[num2].m_dblCURPRICE_MNY.ToString().Trim())) * double.Parse(table.Rows[num4]["CURQTY_DEC"].ToString());
            str = string.Concat(new object[] {
            "insert into t_opr_storemedpricepl(MEDICINEPRICECHGAPPLID_CHR,MEDSTOREID_CHR,MEDICINEID_CHR,CURQTY_DEC,UNITID_CHR,PREPRICE_MNY,CURPRICE_MNY,BALANCEPRICE_MNY,SYSLOTNO_CHR,LOTNO_VCHR) values ('", objApplVO.m_strMEDICINEPRICECHGAPPLID_CHR.Trim(), "','", table.Rows[num4]["STORAGEID_CHR"].ToString().Trim(), "','", objApplDeArr[num2].m_strMEDICINEID_CHR.Trim(), "',", table.Rows[num4]["CURQTY_DEC"].ToString().Trim(), ",'", table.Rows[num4]["UNITID_CHR"].ToString().Trim(), "',", objApplDeArr[num2].m_dblCURPRICE_MNY.ToString().Trim(), ",", objApplDeArr[num2].m_dblCHANGEPRICE_MNY.ToString().Trim(), ",", (double)num3,
            ",'", table.Rows[num4]["SYSLOTNO_CHR"].ToString().Trim(), "','", table.Rows[num4]["LOTNO_VCHR"].ToString().Trim(), "')"
         });
        Label_0314:
            try
            {
                num = service.DoExcute(str);
                goto Label_0335;
            }
            catch (Exception exception3)
            {
            Label_0320:
                exception = exception3;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0335;
            }
        Label_0335:
            goto Label_053F;
        Label_033C:
            num3 = (objApplDeArr[num2].m_dblCHANGEPRICE_MNY - double.Parse(objApplDeArr[num2].m_dblCURPRICE_MNY.ToString().Trim())) * double.Parse(table.Rows[num4]["CURQTY_DEC"].ToString());
            str = string.Concat(new object[] {
            "insert into T_OPR_STORAGEMEDPRICEPL(MEDICINEPRICECHGAPPLID_CHR,STORAGEID_CHR,MEDICINEID_CHR,CURQTY_DEC,UNITID_CHR,PREPRICE_MNY,CURPRICE_MNY,BALANCEPRICE_MNY,SYSLOTNO_CHR,LOTNO_VCHR) values ('", objApplVO.m_strMEDICINEPRICECHGAPPLID_CHR.Trim(), "','", table.Rows[num4]["STORAGEID_CHR"].ToString().Trim(), "','", objApplDeArr[num2].m_strMEDICINEID_CHR.Trim(), "',", table.Rows[num4]["CURQTY_DEC"].ToString().Trim(), ",'", table.Rows[num4]["UNITID_CHR"].ToString().Trim(), "',", objApplDeArr[num2].m_dblCURPRICE_MNY.ToString().Trim(), ",", objApplDeArr[num2].m_dblCHANGEPRICE_MNY.ToString().Trim(), ",", (double)num3,
            ",'", table.Rows[num4]["SYSLOTNO_CHR"].ToString().Trim(), "','", table.Rows[num4]["LOTNO_VCHR"].ToString().Trim(), "')"
         });
        Label_051C:
            try
            {
                num = service.DoExcute(str);
                goto Label_053D;
            }
            catch (Exception exception4)
            {
            Label_0528:
                exception = exception4;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_053D;
            }
        Label_053D:;
        Label_053F:
            num4 += 1;
        Label_0546:
            if (num4 < table.Rows.Count)
            {
                goto Label_0100;
            }
        Label_0560:;
            str = string.Concat(new object[] { "update T_BSE_MEDICINE set UNITPRICE_MNY=", (double)objApplDeArr[num2].m_dblCHANGEPRICE_MNY, " where MEDICINEID_CHR='", objApplDeArr[num2].m_strMEDICINEID_CHR, "'" });
        Label_05AA:
            try
            {
                num = service.DoExcute(str);
                goto Label_05CB;
            }
            catch (Exception exception5)
            {
            Label_05B6:
                exception = exception5;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_05CB;
            }
        Label_05CB:;
            str = string.Concat(new object[] { "update T_BSE_CHARGEITEM set ITEMPRICE_MNY=", (double)objApplDeArr[num2].m_dblCHANGEPRICE_MNY, " where ITEMSRCID_VCHR='", objApplDeArr[num2].m_strMEDICINEID_CHR, "'" });
        Label_0616:
            try
            {
                num = service.DoExcute(str);
                goto Label_0637;
            }
            catch (Exception exception6)
            {
            Label_0622:
                exception = exception6;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0637;
            }
        Label_0637:
            str = "select ITEMID_CHR from T_BSE_CHARGEITEM where ITEMSRCID_VCHR='" + objApplDeArr[num2].m_strMEDICINEID_CHR + "'";
            table2 = new DataTable();
        Label_0658:
            try
            {
                num = service.DoGetDataTable(str, ref table2);
                goto Label_067B;
            }
            catch (Exception exception7)
            {
            Label_0666:
                exception = exception7;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_067B;
            }
        Label_067B:
            if (table2.Rows.Count <= 0)
            {
                goto Label_078C;
            }
            str2 = "";
            service.lngGenerateID(20, "SEQID_CHR", "t_opr_chargeitempricehis", out str2);
            str = string.Concat(new object[] { "insert into t_opr_chargeitempricehis(SEQID_CHR,CHARGEORDERID_CHR,ITEMID_CHR,EFFECT_DAT,PREPRICE_MNY,CURPRICE_MNY,UNIT_VCHR,PSTATUS_INT) values('", str2, "','", objApplVO.m_strMEDICINEPRICECHGAPPLID_CHR, "','", table2.Rows[0]["ITEMID_CHR"].ToString(), "',sysDate,", (double)objApplDeArr[num2].m_dblCURPRICE_MNY, ",", (double)objApplDeArr[num2].m_dblCHANGEPRICE_MNY, ",'", objApplDeArr[num2].m_strUNITID_CHR, "',1)" });
        Label_0769:
            try
            {
                num = service.DoExcute(str);
                goto Label_078A;
            }
            catch (Exception exception8)
            {
            Label_0775:
                exception = exception8;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_078A;
            }
        Label_078A:;
        Label_078C:
            num2 += 1;
        Label_0793:
            if (num2 < ((int)objApplDeArr.Length))
            {
                goto Label_0093;
            }
            goto Label_07AC;
        Label_07A6:
            num5 = num;
            goto Label_07B1;
        Label_07AC:
            num5 = num;
        Label_07B1:
            return num5;
        }

        public long m_lngGetAdjustPriceInfo(string p_strAdjustBillID, out DataSet ds)
        {
            long num;
            num = this.m_lngGetAdjustPriceInfo(p_strAdjustBillID, "", "", "", "", "", "", "", "", "", "", out ds);
        Label_003E:
            return num;
        }

        public long m_lngGetAdjustPriceInfo(string p_strAdjustBillID, string p_strAdjustAppID, out DataSet ds)
        {
            long num;
            num = this.m_lngGetAdjustPriceInfo(p_strAdjustBillID, p_strAdjustAppID, "", "", "", "", "", "", "", "", "", out ds);
        Label_003A:
            return num;
        }

        [AutoComplete]
        public long m_lngGetAdjustPriceInfo(string p_strAdjustBillID, string p_strAdjustAppID, string p_strPeriodID, string p_strAppDate, string p_strState, string p_strCteatorID, string p_strCreateDate, string p_strAuditorID, string p_strAuditDate, string p_strAccountID, string p_strAccountDate, out DataSet p_outDs)
        {
            string str;
            string str2;
            DataTable table;
            clsHRPTableService service;
            long num;
            DataTable table2;
            long num2;
            bool flag;
            str = "SELECT t.*,e1.LASTNAME_VCHR || e1.FIRSTNAME_VCHR as creatorName,e2.LASTNAME_VCHR || e2.FIRSTNAME_VCHR as ADUITName,e3.LASTNAME_VCHR || e3.FIRSTNAME_VCHR as AccountName  FROM t_opr_medicinepricechgappl t,t_bse_employee e1,t_bse_employee e2,t_bse_employee e3";
            str2 = " where t.CREATORID_CHR = e1.EMPID_CHR and t.ADUITEMP_CHR = e2.empid_chr(+) and t.ACCTEMP_CHR = e3.empid_chr(+)";
            if (p_strAdjustBillID != null && p_strAdjustBillID == "")
            {
                goto Label_0067;
            }
            if (str2 != "")
            {
                goto Label_0046;
            }
            str2 = " where ";
            goto Label_0054;
        Label_0046:
            str2 = str2 + " and ";
        Label_0054:
            str2 = str2 + " t.MEDICINEPRICECHGAPPLID_CHR ='" + p_strAdjustBillID + "'";
        Label_0067:
            if (p_strAdjustAppID != null && p_strAdjustAppID == "")
            {
                goto Label_00C1;
            }
            if (str2 != "")
            {
                goto Label_00A0;
            }
            str2 = " where ";
            goto Label_00AE;
        Label_00A0:
            str2 = str2 + " and ";
        Label_00AE:
            str2 = str2 + " t.MEDICINEPRICECHGAPPLNO_CHR='" + p_strAdjustAppID + "'";
        Label_00C1:
            if (p_strAccountDate != null && p_strAccountDate == "")
            {
                goto Label_011E;
            }
            if (str2 != "")
            {
                goto Label_00FC;
            }
            str2 = " where ";
            goto Label_010A;
        Label_00FC:
            str2 = str2 + " and ";
        Label_010A:
            str2 = str2 + " to_char(t.ACCTDATE_DAT,'yyyy-mm-dd')='" + p_strAccountDate + "'";
        Label_011E:
            if (p_strPeriodID != null && p_strPeriodID == "")
            {
                goto Label_0178;
            }
            if (str2 != "")
            {
                goto Label_0157;
            }
            str2 = " where ";
            goto Label_0165;
        Label_0157:
            str2 = str2 + " and ";
        Label_0165:
            str2 = str2 + " t.PERIODID_CHR='" + p_strPeriodID + "'";
        Label_0178:
            if (p_strAppDate != null && p_strAppDate == "")
            {
                goto Label_01D5;
            }
            if (str2 != "")
            {
                goto Label_01B3;
            }
            str2 = " where ";
            goto Label_01C1;
        Label_01B3:
            str2 = str2 + " and ";
        Label_01C1:
            str2 = str2 + " to_char(t.APPLDATE_DAT,'yyyy-mm-dd') ='" + p_strAppDate + "'";
        Label_01D5:
            if (p_strState != null && p_strState == "")
            {
                goto Label_0232;
            }
            if (str2 != "")
            {
                goto Label_0210;
            }
            str2 = " where ";
            goto Label_021E;
        Label_0210:
            str2 = str2 + " and ";
        Label_021E:
            str2 = str2 + " t.PSTATUS_INT=" + p_strState + "";
        Label_0232:
            if (p_strCteatorID != null && p_strCteatorID == "")
            {
                goto Label_028F;
            }
            if (str2 != "")
            {
                goto Label_026D;
            }
            str2 = " where ";
            goto Label_027B;
        Label_026D:
            str2 = str2 + " and ";
        Label_027B:
            str2 = str2 + " t.CREATORID_CHR='" + p_strCteatorID + "'";
        Label_028F:
            if (p_strCreateDate != null && p_strCreateDate == "")
            {
                goto Label_02EC;
            }
            if (str2 != "")
            {
                goto Label_02CA;
            }
            str2 = " where ";
            goto Label_02D8;
        Label_02CA:
            str2 = str2 + " and ";
        Label_02D8:
            str2 = str2 + " to_char(t.CREATEDATE_DAT,'yyyy-mm-dd')='" + p_strCreateDate + "'";
        Label_02EC:
            if (p_strAuditorID != null && p_strAuditorID == "")
            {
                goto Label_0349;
            }
            if (str2 != "")
            {
                goto Label_0327;
            }
            str2 = " where ";
            goto Label_0335;
        Label_0327:
            str2 = str2 + " and ";
        Label_0335:
            str2 = str2 + " t.ADUITEMP_CHR='" + p_strAuditorID + "'";
        Label_0349:
            if (p_strAuditDate != null && p_strAuditDate == "")
            {
                goto Label_03A6;
            }
            if (str2 != "")
            {
                goto Label_0384;
            }
            str2 = " where ";
            goto Label_0392;
        Label_0384:
            str2 = str2 + " and ";
        Label_0392:
            str2 = str2 + " to_char(t.ADUITDATE_DAT,'yyyy-mm-dd')='" + p_strAuditDate + "'";
        Label_03A6:
            if (p_strAccountID != null && p_strAccountID == "")
            {
                goto Label_0403;
            }
            if (str2 != "")
            {
                goto Label_03E1;
            }
            str2 = " where ";
            goto Label_03EF;
        Label_03E1:
            str2 = str2 + " and ";
        Label_03EF:
            str2 = str2 + " t.ACCTEMP_CHR='" + p_strAccountID + "'";
        Label_0403:
            str = str + str2;
            p_outDs = new DataSet();
            table = new DataTable("t_opr_medicinepricechgappl");
            service = new clsHRPTableService();
            num = service.lngGetDataTableWithoutParameters(str, ref table);
            table.TableName = "t_opr_medicinepricechgappl";
            p_outDs.Tables.Add(table);
            if (num >= 0L)
            {
                goto Label_0460;
            }
            num2 = num;
            goto Label_04C1;
        Label_0460:
            str = "select a.MEDICINEID_CHR, a.medicinepricechgappldeid_chr,a.ROWNO_CHR, a.medicineid_chr,a.unitid_chr,a.curprice_mny, a.changeprice_mny,a.medicinepricechgapplid_chr,b.assistcode_chr, a.typeid_chr,d.typename_chr,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,sum(c.AMOUNT_DEC) as AMOUNT_DEC,'' as Balance from t_opr_medicinepricechgapplde a,t_bse_medicine b ,t_bse_storagemedicine c, t_bse_medchagetype d where a.MEDICINEID_CHR = b.MEDICINEID_CHR  and a.medicineid_chr = c.medicineid_chr  and a.typeid_chr = d.typeid_chr and  MEDICINEPRICECHGAPPLID_CHR in (SELECT t.MEDICINEPRICECHGAPPLID_CHR FROM t_opr_medicinepricechgappl t,t_bse_employee e1,t_bse_employee e2,t_bse_employee e3" + str2 + ") group by a.MEDICINEID_CHR,a.medicinepricechgappldeid_chr, a.medicinepricechgappldeid_chr, a.medicineid_chr,a.unitid_chr,a.curprice_mny, a.changeprice_mny,a.medicinepricechgapplid_chr, a.typeid_chr,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,d.typename_chr,a.ROWNO_CHR,b.assistcode_chr";
            table2 = new DataTable("t_opr_medicinepricechgapplde");
            num = service.lngGetDataTableWithoutParameters(str, ref table2);
            table2.TableName = "t_opr_medicinepricechgapplde";
            p_outDs.Tables.Add(table2);
            if (num >= 0L)
            {
                goto Label_04BB;
            }
            num2 = num;
            goto Label_04C1;
        Label_04BB:
            num2 = num;
        Label_04C1:
            return num2;
        }

        [AutoComplete]
        public long m_lnghMedInit(string p_strMedID, string p_strBatchNO, string p_strStorageID, double p_intMedQty, string p_strUnitID, double p_dBuyunitprice_mny, double p_dSaleunitprice_mny, double p_dWPrice, string p_dateUsefullife_dat, string p_strVendor, string sysNO)
        {
            string str;
            long num;
            str = "";
            num = this.m_lnghMedInit(p_strMedID, p_strBatchNO, p_strStorageID, p_intMedQty, p_strUnitID, p_dBuyunitprice_mny, p_dSaleunitprice_mny, p_dWPrice, p_dateUsefullife_dat, p_strVendor, "0", sysNO, out str);
        Label_002C:
            return num;
        }

        [AutoComplete]
        public long m_lnghMedInit(string p_strMedID, string p_strBatchNO, string p_strStorageID, double p_intMedQty, string p_strUnitID, double p_dBuyunitprice_mny, double p_dSaleunitprice_mny, double p_dWPrice, string p_dateUsefullife_dat, string p_strVendor, out string newSysNO)
        {
            long num;
            newSysNO = "";
            num = this.m_lnghMedInit(p_strMedID, p_strBatchNO, p_strStorageID, p_intMedQty, p_strUnitID, p_dBuyunitprice_mny, p_dSaleunitprice_mny, p_dWPrice, p_dateUsefullife_dat, p_strVendor, "0", null, out newSysNO);
        Label_002D:
            return num;
        }

        [AutoComplete]
        public long m_lnghMedInit(string p_strMedID, string p_strBatchNO, string p_strStorageID, double p_intMedQty, string p_strUnitID, double p_dBuyunitprice_mny, double p_dSaleunitprice_mny, double p_dWPrice, string p_dateUsefullife_dat, string p_strVendor, string flag, string sysNO)
        {
            string str;
            long num;
            str = "";
            num = this.m_lnghMedInit(p_strMedID, p_strBatchNO, p_strStorageID, p_intMedQty, p_strUnitID, p_dBuyunitprice_mny, p_dSaleunitprice_mny, p_dWPrice, p_dateUsefullife_dat, p_strVendor, flag, sysNO, out str);
        Label_0029:
            return num;
        }

        public long m_lnghMedInit(string p_strMedID, string p_strBatchNO, string p_strStorageID, double p_intMedQty, string p_strUnitID, double p_dBuyunitprice_mny, double p_dSaleunitprice_mny, double p_dWPrice, string p_dateUsefullife_dat, string p_strVendor, string flag, string sysNO, out string strSyslotno)
        {
            long num;
            float num2;
            string str;
            double num3;
            clsHRPTableService service;
            DataTable table;
            DataTable table2;
            string str2;
            long num4;
            string str3;
            DataTable table3;
            long num5;
            long num6;
            int num7;
            int num8;
            Exception exception;
            clsLogText text;
            long num9;
            bool flag2;
            string[] strArray;
            object[] objArray;
            if (flag == null || flag.Length == 0)
                flag = "0";
            else
                goto Label_0024;
            Label_0024:
            num = 0L;
            num2 = 1f;
            num3 = 0.0;
            strSyslotno = "";
            service = new clsHRPTableService();
            table = new DataTable();
            table2 = new DataTable();
            str2 = "select medicineid_chr, medicinename_vchr, medicinetypeid_chr, medspec_vchr, medicinestdid_chr, pycode_chr, wbcode_chr, medicinepreptype_chr, productorid_chr, isanaesthesia_chr, ischlorpromazine_chr, iscostly_chr, isself_chr, isimport_chr, isselfpay_chr, medicineengname_vchr, assistcode_chr, dosage_dec, dosageunit_chr, opunit_chr, ipunit_chr, packqty_dec, noqtyflag_int, tradeprice_mny, unitprice_mny, mindosage_dec, maxdosage_dec, nmldosage_dec, adultdosage_dec, childdosage_dec, ipnoqtyflag_int, poflag_int, usageid_chr, opchargeflg_int, ipchargeflg_int, ifstop_int, insuranceid_vchr, standard_int from t_bse_medicine where medicineid_chr = '" + p_strMedID.Trim() + "'";
            service.lngGetDataTableWithoutParameters(str2, ref table2);
            if (table2 != null && table2.Rows.Count > 0)
            {
                goto Label_00A0;
            }
            throw new Exception("无此药品信息");
        Label_00A0:
            if (flag != "0")
            {
                goto Label_00CC;
            }
            str2 = "select storageid_chr, storagetypeid_chr, storagename_vchr, storagegrossprofit_dec, initflag_int, outflag_int from t_bse_storage where storageid_chr = '" + p_strStorageID + "'";
            goto Label_00E1;
        Label_00CC:
            str2 = "select medstoreid_chr as storageid_chr, a.medstorename_vchr as storagename_vchr from t_bse_medstore a where a.medstoreid_chr = '" + p_strStorageID + "'";
        Label_00E1:
            num = service.lngGetDataTableWithoutParameters(str2, ref table);
            if (table != null && table.Rows.Count > 0)
            {
                goto Label_0116;
            }
            throw new Exception("无此仓库");
        Label_0116:
            if (flag != "0")
            {
                goto Label_0151;
            }
            str = table2.Rows[0]["opunit_chr"].ToString().Trim();
            goto Label_01CD;
        Label_0151:
            if (table2.Rows[0]["OPCHARGEFLG_INT"].ToString().Trim() != "1")
            {
                goto Label_01AA;
            }
            str = table2.Rows[0]["ipunit_chr"].ToString().Trim();
            goto Label_01CC;
        Label_01AA:
            str = table2.Rows[0]["opunit_chr"].ToString().Trim();
        Label_01CC:;
        Label_01CD:
            if (str.Trim() == p_strUnitID.Trim())
            {
                goto Label_0260;
            }
        Label_01E9:
            try
            {
                num2 = float.Parse(table2.Rows[0]["packqty_dec"].ToString());
                if (p_strUnitID.Trim() != table2.Rows[0]["ipunit_chr"].ToString().Trim())
                {
                    goto Label_024C;
                }
                num3 = p_intMedQty / ((double)num2);
                goto Label_0254;
            Label_024C:
                num3 = p_intMedQty * ((double)num2);
            Label_0254:
                goto Label_025C;
            }
            catch
            {
            Label_0257:
                goto Label_025C;
            }
        Label_025C:
            goto Label_0291;
        Label_0260:;
        Label_0261:
            try
            {
                num2 = float.Parse(table2.Rows[0]["packqty_dec"].ToString());
                num3 = p_intMedQty;
                goto Label_028F;
            }
            catch
            {
            Label_028A:
                goto Label_028F;
            }
        Label_028F:;
        Label_0291:
            if (p_dateUsefullife_dat != null)
            {
                goto Label_02B6;
            }
            p_dateUsefullife_dat = "";
        Label_02B6:
            num4 = 0L;
            if (sysNO != null && sysNO.Trim() != "")
            {
                goto Label_03DA;
            }
            str3 = "select nvl(max(a.syslotno_chr),0) as syslotno from t_opr_storagemeddetail a where a.medicineid_chr = '" + p_strMedID + "' and FLAG_INT=" + flag;
            table3 = new DataTable();
            service.lngGetDataTableWithoutParameters(str3, ref table3);
            if (table3 == null || table3.Rows.Count == 0)
            {
                goto Label_03B1;
            }
        Label_0312:
            try
            {
                num6 = 0L;
                if (table3.Rows[0].ItemArray[0].Equals(DBNull.Value) != null)
                {
                    goto Label_035B;
                }
                num6 = Convert.ToInt64(table3.Rows[0].ItemArray[0].ToString());
            Label_035B:
                strSyslotno = Convert.ToString(num6 + 1L);
                goto Label_0378;
            }
            catch
            {
            Label_036B:
                strSyslotno = "";
                goto Label_0378;
            }
        Label_0378:
            num7 = 6 - strSyslotno.Length;
            num8 = 0;
            goto Label_03A4;
        Label_038A:
            strSyslotno = ((char)0x30) + strSyslotno;
            num8 += 1;
        Label_03A4:
            if (num8 < num7)
            {
                goto Label_038A;
            }
        Label_03B1:
            try
            {
                num = service.DoExcute(str2);
                goto Label_03D6;
            }
            catch (Exception exception1)
            {
            Label_03BF:
                exception = exception1;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_03D6;
            }
        Label_03D6:
            goto Label_03E6;
        Label_03DA:
            strSyslotno = sysNO.Trim();
        Label_03E6:
            if (flag != "0")
            {
                goto Label_075A;
            }
            if (sysNO != null && sysNO.Trim() != "")
            {
                goto Label_0561;
            }
            str2 = "insert into t_opr_storagemeddetail (storageid_chr, medicineid_chr,  lotno_vchr, syslotno_chr, productorid_chr, curqty_dec, unitid_chr, usefullife_dat, buyunitprice_mny, saleunitprice_mny,WHOLESALEUNITPRICE_MNY,USEFULSTATUS_INT,PACKQTY_DEC,FLAG_INT) values ('" + p_strStorageID.Trim() + "', '" + p_strMedID.Trim() + "','" + p_strBatchNO + "','" + strSyslotno + "',  '" + p_strVendor + "', " + num3.ToString() + ", '" + str + "', to_date('" + p_dateUsefullife_dat.Trim() + "','yyyy-mm-dd hh24:mi:ss'), " + p_dBuyunitprice_mny.ToString() + ", " + p_dSaleunitprice_mny.ToString() + "," + p_dWPrice.ToString() + ",1," + num2.ToString() + "," + flag + ")";
        Label_0535:
            try
            {
                num = service.DoExcute(str2);
                goto Label_055A;
            }
            catch (Exception exception2)
            {
            Label_0543:
                exception = exception2;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_055A;
            }
        Label_055A:
            goto Label_0754;
        Label_0561:
            num4 = 0L;
            str2 = string.Concat(new object[] { "update t_opr_storagemeddetail set CURQTY_DEC=CURQTY_DEC+", (double)num3, " where STORAGEID_CHR='", p_strStorageID.Trim(), "' and MEDICINEID_CHR='", p_strMedID.Trim(), "' and SYSLOTNO_CHR='", strSyslotno, "' and LOTNO_VCHR='", p_strBatchNO, "' and FLAG_INT=0" });
        Label_05DB:
            try
            {
                num = service.DoExcuteForDelete(str2, ref num4);
                goto Label_0602;
            }
            catch (Exception exception3)
            {
            Label_05EB:
                exception = exception3;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0602;
            }
        Label_0602:
            if (num4 != 0L)
            {
                goto Label_0753;
            }
            str2 = "insert into t_opr_storagemeddetail (storageid_chr, medicineid_chr,  lotno_vchr, syslotno_chr, productorid_chr, curqty_dec, unitid_chr, usefullife_dat, buyunitprice_mny, saleunitprice_mny,WHOLESALEUNITPRICE_MNY,USEFULSTATUS_INT,PACKQTY_DEC,FLAG_INT) values ('" + p_strStorageID.Trim() + "', '" + p_strMedID.Trim() + "','" + p_strBatchNO + "','" + strSyslotno + "',  '" + p_strVendor + "', " + num3.ToString() + ", '" + str + "', to_date('" + p_dateUsefullife_dat.Trim() + "','yyyy-mm-dd hh24:mi:ss'), " + p_dBuyunitprice_mny.ToString() + ", " + p_dSaleunitprice_mny.ToString() + "," + p_dWPrice.ToString() + ",1," + num2.ToString() + "," + flag + ")";
        Label_072C:
            try
            {
                num = service.DoExcute(str2);
                goto Label_0751;
            }
            catch (Exception exception4)
            {
            Label_073A:
                exception = exception4;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0751;
            }
        Label_0751:;
        Label_0753:;
        Label_0754:
            goto Label_0954;
        Label_075A:
            num4 = 0L;
            str2 = string.Concat(new object[] { "update t_opr_storagemeddetail set CURQTY_DEC=CURQTY_DEC+", (double)num3, " where STORAGEID_CHR='", p_strStorageID.Trim(), "' and MEDICINEID_CHR='", p_strMedID.Trim(), "' and SYSLOTNO_CHR='", strSyslotno, "' and LOTNO_VCHR='", p_strBatchNO, "' and FLAG_INT=", flag });
        Label_07DB:
            try
            {
                num = service.DoExcuteForDelete(str2, ref num4);
                goto Label_0802;
            }
            catch (Exception exception5)
            {
            Label_07EB:
                exception = exception5;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0802;
            }
        Label_0802:
            if (num4 != 0L)
            {
                goto Label_0953;
            }
            str2 = "insert into t_opr_storagemeddetail (storageid_chr, medicineid_chr,  lotno_vchr, syslotno_chr, productorid_chr, curqty_dec, unitid_chr, usefullife_dat, buyunitprice_mny, saleunitprice_mny,WHOLESALEUNITPRICE_MNY,USEFULSTATUS_INT,PACKQTY_DEC,FLAG_INT) values ('" + p_strStorageID.Trim() + "', '" + p_strMedID.Trim() + "','" + p_strBatchNO + "','" + strSyslotno + "',  '" + p_strVendor + "', " + num3.ToString() + ", '" + str + "', to_date('" + p_dateUsefullife_dat.Trim() + "','yyyy-mm-dd hh24:mi:ss'), " + p_dBuyunitprice_mny.ToString() + ", " + p_dSaleunitprice_mny.ToString() + "," + p_dWPrice.ToString() + ",1," + num2.ToString() + "," + flag + ")";
        Label_092C:
            try
            {
                num = service.DoExcute(str2);
                goto Label_0951;
            }
            catch (Exception exception6)
            {
            Label_093A:
                exception = exception6;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0951;
            }
        Label_0951:;
        Label_0953:;
        Label_0954:;
            str2 = "update t_bse_storagemedicine set amount_dec = amount_dec +" + num3.ToString() + " where storageid_chr = '" + p_strStorageID.Trim() + "' and FLAG_INT = " + flag + " and medicineid_chr = '" + p_strMedID.Trim() + "'";
        Label_09B9:
            try
            {
                num = service.DoExcuteForDelete(str2, ref num4);
                goto Label_09E0;
            }
            catch (Exception exception7)
            {
            Label_09C9:
                exception = exception7;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_09E0;
            }
        Label_09E0:
            if (num4 != 0L)
            {
                goto Label_0AC1;
            }
            if (flag != "1")
            {
                goto Label_0A0F;
            }
            num2 = 1f;
        Label_0A0F:;
            str2 = " insert into t_bse_storagemedicine (storageid_chr, medicineid_chr, amount_dec,unitid_chr,PACKQTY_DEC,FLAG_INT) values ('" + p_strStorageID.Trim() + "', '" + p_strMedID.Trim() + "'," + num3.ToString() + ",'" + str + "'," + num2.ToString() + "," + flag + ")";
        Label_0A9A:
            try
            {
                num = service.DoExcute(str2);
                goto Label_0ABF;
            }
            catch (Exception exception8)
            {
            Label_0AA8:
                exception = exception8;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0ABF;
            }
        Label_0ABF:;
        Label_0AC1:
            num9 = num;
        Label_0AC6:
            return num9;
        }

        [AutoComplete]
        public long m_lngIncreaseMedQty(string p_strMedID, string p_strBatchNO, string p_strStorageID, double p_intMedQty, string p_strUnitID, int status)
        {
            long num;
            long num2;
            bool flag;
            num = 0L;
        Label_002E:
            num2 = this.m_lngUpdateMed(p_strMedID, p_strBatchNO, "", p_strStorageID, "0", p_intMedQty, p_strUnitID, true, false, status);
        Label_004D:
            return num2;
        }

        [AutoComplete]
        public long m_lngIncreaseMedQty(string p_strMedID, string p_strBatchNO, string p_strStorageID, string p_strStorageFlag, double p_intMedQty, string p_strUnitID, int status)
        {
            long num;
            long num2;
            bool flag;
            num = 0L;
        Label_002E:
            num2 = this.m_lngUpdateMed(p_strMedID, p_strBatchNO, "", p_strStorageID, p_strStorageFlag, p_intMedQty, p_strUnitID, true, false, status);
        Label_004A:
            return num2;
        }

        [AutoComplete]
        public long m_lngReduceMedQty(string p_strMedID, string p_strBatchNO, string p_strSysNO, string p_strStorageID, double p_intMedQty, string p_strUnitID, int status)
        {
            long num;
            long num2;
            bool flag;
            num = 0L;
        Label_002D:
            num2 = this.m_lngUpdateMed(p_strMedID, p_strBatchNO, p_strSysNO, p_strStorageID, "0", p_intMedQty, p_strUnitID, false, true, status);
        Label_0049:
            return num2;
        }

        [AutoComplete]
        public long m_lngReduceMedQty(string p_strMedID, string p_strBatchNO, string p_strSysNO, string p_strStorageID, string p_strStorageFlag, double p_intMedQty, string p_strUnitID, int status)
        {
            long num;
            long num2;
            bool flag;
            num = 0L;
        Label_002D:
            num2 = this.m_lngUpdateMed(p_strMedID, p_strBatchNO, p_strSysNO, p_strStorageID, p_strStorageFlag, p_intMedQty, p_strUnitID, false, true, status);
        Label_0046:
            return num2;
        }

        [AutoComplete]
        public long m_lngReducMedQty(string p_strMedID, string p_strStorageID, string p_strStorageFlag, double p_intMedQty, string p_strUnitID)
        {
            long num;
            float num2;
            string str;
            double num3;
            double num4;
            clsHRPTableService service;
            DataTable table;
            DataTable table2;
            string str2;
            int num5;
            Exception exception;
            clsLogText text;
            long num6;
            bool flag;
            string[] strArray;
            string str3;
            num = 0L;
            num2 = 1f;
            num3 = 0.0;
            num4 = 0.0;
            service = new clsHRPTableService();
            table = new DataTable();
            table2 = new DataTable();
            str2 = "select medicineid_chr, medicinename_vchr, medicinetypeid_chr, medspec_vchr, medicinestdid_chr, pycode_chr, wbcode_chr, medicinepreptype_chr, productorid_chr, isanaesthesia_chr, ischlorpromazine_chr, iscostly_chr, isself_chr, isimport_chr, isselfpay_chr, medicineengname_vchr, assistcode_chr, dosage_dec, dosageunit_chr, opunit_chr, ipunit_chr, packqty_dec, noqtyflag_int, tradeprice_mny, unitprice_mny, mindosage_dec, maxdosage_dec, nmldosage_dec, adultdosage_dec, childdosage_dec, ipnoqtyflag_int, poflag_int, usageid_chr, opchargeflg_int, ipchargeflg_int, ifstop_int, insuranceid_vchr, standard_int from t_bse_medicine where medicineid_chr = '" + p_strMedID.Trim() + "'";
            service.lngGetDataTableWithoutParameters(str2, ref table2);
            if (table2 != null && table2.Rows.Count > 0)
            {
                goto Label_0080;
            }
            throw new Exception("无此药品信息");
        Label_0080:
            if (table2.Rows[0]["opunit_chr"].ToString().Trim() != p_strUnitID)
            {
                goto Label_00DC;
            }
            num2 = float.Parse(table2.Rows[0]["packqty_dec"].ToString());
            num3 = p_intMedQty;
            goto Label_0110;
        Label_00DC:;
        Label_00DD:
            try
            {
                num2 = float.Parse(table2.Rows[0]["packqty_dec"].ToString());
                num3 = p_intMedQty / ((double)num2);
                goto Label_010E;
            }
            catch
            {
            Label_0109:
                goto Label_010E;
            }
        Label_010E:;
        Label_0110:;
            str2 = "select storageid_chr, medicineid_chr, syslotno_chr, lotno_vchr, productorid_chr, curqty_dec, unitid_chr, usefulstatus_int, usefullife_dat, buyunitprice_mny, saleunitprice_mny, wholesaleunitprice_mny from t_opr_storagemeddetail where sotrageid_chr = '" + p_strStorageID.Trim() + "' and FLAG_INT = " + p_strStorageFlag + " and medicineid_chr = '" + p_strMedID.Trim() + "'";
            str3 = table.Rows[0]["outflag_int"].ToString().Trim();
            if (str3 == null)
            {
                goto Label_01DB;
            }
            if (str3 == "0")
            {
                goto Label_01B2;
            }
            if (str3 == "1")
            {
                goto Label_01C2;
            }
            if (str3 == "2")
            {
                goto Label_01D2;
            }
            goto Label_01DB;
        Label_01B2:
            str2 = str2 + " order by SYSLOTNO_CHR";
            goto Label_01DB;
        Label_01C2:
            str2 = str2 + " order by SYSLOTNO_CHR";
            goto Label_01DB;
        Label_01D2:
            str2 = " order by usefullife_dat";
        Label_01DB:
            num = service.lngGetDataTableWithoutParameters(str2, ref table2);
            num4 = num3;
            if (table2.Rows.Count >= 1)
            {
                goto Label_020C;
            }
            num6 = -1L;
            goto Label_0616;
        Label_020C:
            num5 = 0;
            goto Label_0476;
        Label_0214:
            if (double.Parse(table2.Rows[num5]["curqty_dec"].ToString()) <= num3)
            {
                goto Label_0337;
            }
            str2 = "update t_opr_storagemeddetail set curqty_dec = curqty_dec-" + num3.ToString() + " where medicineid_chr = '" + p_strMedID.Trim() + "'  and storageid_chr = '" + p_strStorageID.Trim() + "'  and FLAG_INT = " + p_strStorageFlag + " and trim(lotno_vchr) = '" + table2.Rows[num5]["lotno_vchr"].ToString() + "' and trim(syslotno_chr)='" + table2.Rows[num5]["syslotno_chr"].ToString() + "'";
            num = service.DoExcute(str2);
            num3 -= double.Parse(table2.Rows[num5]["curqty_dec"].ToString());
            goto Label_048F;
        Label_0337:
            if (double.Parse(table2.Rows[num5]["curqty_dec"].ToString()) > 0 && double.Parse(table2.Rows[num5]["curqty_dec"].ToString()) >= num3)
            {
                goto Label_046F;
            }
            str2 = "update t_opr_storagemeddetail set curqty_dec = 0 where trim(medicineid_chr) = '" + p_strMedID.Trim() + "' and trim(storageid_chr) = '" + p_strStorageID.Trim() + "' and FLAG_INT = " + p_strStorageFlag + " and trim(lotno_vchr) = '" + table2.Rows[num5]["lotno_vchr"].ToString() + "' and trim(syslotno_chr)='" + table2.Rows[num5]["syslotno_chr"].ToString() + "'";
            num = service.DoExcute(str2);
            num3 -= double.Parse(table2.Rows[num5]["curqty_dec"].ToString());
        Label_046F:
            num5 += 1;
        Label_0476:
            if (num5 < table2.Rows.Count)
            {
                goto Label_0214;
            }
        Label_048F:
            if (num3 <= 0.0)
            {
                goto Label_0587;
            }
            str2 = "update t_opr_storagemeddetail set curqty_dec = curqty_dec-" + num3.ToString() + " where medicineid_chr = '" + p_strMedID.Trim() + "' and storageid_chr = '" + p_strStorageID.Trim() + "' and FLAG_INT = " + p_strStorageFlag + " and trim(lotno_vchr) = '" + table2.Rows[table2.Rows.Count - 1]["lotno_vchr"].ToString() + "' and trim(syslotno_chr)='" + table2.Rows[table2.Rows.Count - 1]["syslotno_chr"].ToString() + "'";
            num = service.DoExcute(str2);
        Label_0587:;
            str2 = "update t_bse_storagemedicine set amount_dec = amount_dec -" + num3.ToString() + " where trim(storageid_chr) = '" + p_strStorageID.Trim() + "' and FLAG_INT = " + p_strStorageFlag + " and trim(medicineid_chr)= '" + p_strMedID.Trim() + "'";
        Label_05EB:
            try
            {
                num = service.DoExcute(str2);
                goto Label_0610;
            }
            catch (Exception exception1)
            {
            Label_05F9:
                exception = exception1;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0610;
            }
        Label_0610:
            num6 = num;
        Label_0616:
            return num6;
        }

        [AutoComplete]
        private long m_lngUpdateMed(string p_strMedID, string p_strBatchNO, string p_strSysNO, string p_strStorageID, string p_strStorageFlag, double p_intMedQty, string p_strUnitID, bool addFlag, bool p_bBatch, int status)
        {
            long num;
            float num2;
            string str;
            double num3;
            clsHRPTableService service;
            DataTable table;
            DataTable table2;
            string str2;
            string str3;
            DataTable table3;
            Exception exception;
            clsLogText text;
            long num4;
            long num5;
            bool flag;
            string[] strArray;
            num = 0L;
            num2 = 1f;
            num3 = 0.0;
            service = new clsHRPTableService();
            table = new DataTable();
            table2 = new DataTable();
            str2 = "select medicineid_chr, medicinename_vchr, medicinetypeid_chr, medspec_vchr, medicinestdid_chr, pycode_chr, wbcode_chr, medicinepreptype_chr, productorid_chr, isanaesthesia_chr, ischlorpromazine_chr, iscostly_chr, isself_chr, isimport_chr, isselfpay_chr, medicineengname_vchr, assistcode_chr, dosage_dec, dosageunit_chr, opunit_chr, ipunit_chr, packqty_dec, noqtyflag_int, tradeprice_mny, unitprice_mny, mindosage_dec, maxdosage_dec, nmldosage_dec, adultdosage_dec, childdosage_dec, ipnoqtyflag_int, poflag_int, usageid_chr, opchargeflg_int, ipchargeflg_int, ifstop_int, insuranceid_vchr, standard_int from t_bse_medicine where medicineid_chr = '" + p_strMedID.Trim() + "'";
            num = service.lngGetDataTableWithoutParameters(str2, ref table2);
            if (num > 0 && table2.Rows.Count > 0)
            {
                goto Label_0075;
            }
            throw new Exception("无此药品信息");
        Label_0075:
            if (table2.Rows[0]["opunit_chr"].ToString().Trim() != p_strUnitID)
            {
                goto Label_00B5;
            }
            num2 = 1f;
            num3 = p_intMedQty;
            goto Label_00E9;
        Label_00B5:;
        Label_00B6:
            try
            {
                num2 = float.Parse(table2.Rows[0]["packqty_dec"].ToString());
                num3 = p_intMedQty / ((double)num2);
                goto Label_00E7;
            }
            catch
            {
            Label_00E2:
                goto Label_00E7;
            }
        Label_00E7:;
        Label_00E9:;
            str3 = ("SELECT a.storageid_chr, a.medicineid_chr, a.syslotno_chr, a.lotno_vchr, a.productorid_chr, a.curqty_dec, a.unitid_chr, a.usefulstatus_int, a.usefullife_dat, a.buyunitprice_mny, a.saleunitprice_mny, a.wholesaleunitprice_mny, a.flag_int, a.packqty_dec FROM t_opr_storagemeddetail a where trim(a.storageid_chr) ='" + p_strStorageID.Trim() + "'and a.FLAG_INT = " + p_strStorageFlag + " and trim(a.medicineid_chr) = '" + p_strMedID.Trim()) + "' and trim(a.SYSLOTNO_CHR) = '" + p_strSysNO.Trim() + "'";
            table3 = new DataTable();
        Label_0150:
            try
            {
                service.lngGetDataTableWithoutParameters(str3, ref table3);
                goto Label_0177;
            }
            catch (Exception exception1)
            {
            Label_0160:
                exception = exception1;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0177;
            }
        Label_0177:
            if (table3.Rows.Count < 1)
            {
                throw new Exception("无此批号的药品");
            }
            else
            {
                goto Label_019C;
            }
        Label_019C:
            if (status != 0)
            {
                goto Label_0207;
            }
            if (double.Parse(table3.Rows[0]["curqty_dec"].ToString()) >= num3)
            {
                goto Label_0206;
            }
            throw new Exception(table2.Rows[0]["medicinename_vchr"].ToString() + p_strSysNO + "批次的库存不足");
        Label_0206:;
        Label_0207:
            if (addFlag == true)
            {
                goto Label_02F6;
            }
            num5 = this.m_lnghMedInit(p_strMedID.Trim(), p_strBatchNO.Trim(), p_strStorageID.Trim(), num3, p_strUnitID.Trim(), double.Parse(table3.Rows[0]["buyunitprice_mny"].ToString()), double.Parse(table3.Rows[0]["saleunitprice"].ToString()), double.Parse(table3.Rows[0]["wholesaleunitprice_mny"].ToString()), table3.Rows[0]["usefulstatus_int"].ToString(), table3.Rows[0]["productorid_chr"].ToString(), table3.Rows[0]["SYSLOTNO_CHR"].ToString());
            goto Label_0476;
        Label_02F6:;
            str2 = "update t_opr_storagemeddetail set curqty_dec = curqty_dec-" + num3.ToString() + " where trim(medicineid_chr) = '" + p_strMedID.Trim() + "' and trim(storageid_chr) = '" + p_strStorageID.Trim() + "' and FLAG_INT = " + p_strStorageFlag + " and trim(SYSLOTNO_CHR) = '" + p_strSysNO + "'";
        Label_036B:
            try
            {
                num4 = 0L;
                num = service.DoExcuteForDelete(str2, ref num4);
                if (num4 != 0L)
                {
                    goto Label_0397;
                }
                throw new Exception("出错了，库存中没有此批号的药品");
            Label_0397:
                goto Label_03B1;
            }
            catch (Exception exception2)
            {
            Label_039A:
                exception = exception2;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_03B1;
            }
        Label_03B1:
            if (num >= 0L)
            {
                goto Label_03CC;
            }
            throw new Exception("更新库存明细失败");
        Label_03CC:;
            str2 = "update t_bse_storagemedicine set amount_dec = amount_dec -" + num3.ToString() + " where trim(storageid_chr) = '" + p_strStorageID.Trim() + "' and FLAG_INT = " + p_strStorageFlag + " and trim(medicineid_chr) = '" + p_strMedID.Trim() + "'";
        Label_0431:
            try
            {
                num = service.DoExcute(str2);
                goto Label_0456;
            }
            catch (Exception exception3)
            {
            Label_043F:
                exception = exception3;
                text = new clsLogText();
                text.LogError(exception);
                goto Label_0456;
            }
        Label_0456:
            if (num >= 0L)
            {
                goto Label_0471;
            }
            throw new Exception("更新库存总表失败");
        Label_0471:
            num5 = num;
        Label_0476:
            return num5;
        }

        [AutoComplete]
        public void m_mthGetMedBatchQty(string p_strMedID, string p_strBatchNO, string p_strStorageID, out double p_intMedQty)
        {
            long num;
            string str;
            clsHRPTableService service2;
            DataTable table;
            bool flag;
            string[] strArray;
            num = 0L;
            p_intMedQty = 0.0;
        Label_003C:;
            str = "select a.storageid_chr, a.medicineid_chr, a.syslotno_chr, a.lotno_vchr, a.productorid_chr, a.curqty_dec, a.unitid_chr,b.unitname_chr, a.usefulstatus_int, a.usefullife_dat, a.buyunitprice_mny, a.saleunitprice_mny, a.wholesaleunitprice_mny from t_opr_storagemeddetail a,t_aid_unit b where a.unitid_chr = b.unitid_chr(+) and trim(a.storageid_chr)='" + p_strStorageID.Trim() + "' and a.medicineid_chr='" + p_strMedID.Trim() + "'";
            service2 = new clsHRPTableService();
            table = new DataTable();
            service2.lngGetDataTableWithoutParameters(str, ref table);
            if (table == null || table.Rows.Count == 0)
            {
                goto Label_00F2;
            }
        Label_00B4:
            try
            {
                p_intMedQty = (double)int.Parse(table.Rows[0]["curqty_dec"].ToString());
                goto Label_00EE;
            }
            catch
            {
            Label_00DD:
                p_intMedQty = 0.0;
                goto Label_00EE;
            }
        Label_00EE:
            goto Label_0100;
        Label_00F2:
            p_intMedQty = 0.0;
        Label_0100:
            return;
        }

        [AutoComplete]
        public void m_mthGetMedQty(string p_strMedID, string p_strStorageID, out double p_intMedQty)
        {
            long num;
            string str;
            clsHRPTableService service2;
            DataTable table;
            bool flag;
            string[] strArray;
            num = 0L;
            p_intMedQty = 0.0;
        Label_003C:;
            str = "select s.amount_dec from t_bse_storagemedicine s where s.storageid_chr = '" + p_strStorageID.Trim() + "' and s.medicineid_chr = '" + p_strMedID.Trim() + "'";
            service2 = new clsHRPTableService();
            table = new DataTable();
            service2.lngGetDataTableWithoutParameters(str, ref table);
            if (table == null || table.Rows.Count == 0)
            {
                goto Label_00ED;
            }
        Label_00B3:
            try
            {
                p_intMedQty = (double)int.Parse(table.Rows[0][0].ToString());
                goto Label_00E9;
            }
            catch
            {
            Label_00D8:
                p_intMedQty = 0.0;
                goto Label_00E9;
            }
        Label_00E9:
            goto Label_00FB;
        Label_00ED:
            p_intMedQty = 0.0;
        Label_00FB:
            return;
        }

        public void m_mthMedStorageInit(object[] obj1)
        {
            return;
        }
    }
}
