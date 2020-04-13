using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using System.Xml;

namespace com.digitalwave.iCare.middletier.HIS
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsRecordMark : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        [AutoComplete]
        public long m_mthAddNewRecord(clsRecordMark_VO p_objVO)
        {
            long num;
            clsHRPTableService service;
            string str;
            int num2;
            int num3;
            string str2;
            string str3;
            string str4;
            string str5;
            char[] chArray;
            string[] strArray;
            string[] strArray2;
            string str6;
            int num4;
            string str7;
            string str8;
            string[] strArray3;
            string str9;
            string str10;
            string str11;
            string str12;
            string str13;
            string[] strArray4;
            string str14;
            string str15;
            string str16;
            int num5;
            string str17;
            DataTable table;
            Exception exception;
            string str18;
            clsLogText text;
            bool flag;
            string str19;
            string[] strArray5;
            string str20;
            IDataParameter[] parameterArray;
            long num6;
            long num7;
            bool flag2;
            string str21;
            string[] strArray6;
            DateTime time;
            num = 0L;
            service = new clsHRPTableService();
            if (!string.IsNullOrEmpty(p_objVO.m_strRECORDDETAIL_VCHR))
            {
                goto Label_0853;
            }
            str = p_objVO.m_strRECORDDETAIL_VCHR.Trim().ToUpper();
            if (!str.StartsWith("INSERT"))
            {
                goto Label_01DF;
            }
            num2 = str.IndexOf("INTO");
            num3 = str.IndexOf("(");
            p_objVO.m_strTABLENAME_VCHR = str.Substring(num2 + 4, (num3 - num2) - 4).Trim();
            p_objVO.m_intTYPE_INT = 0;
            num2 = str.IndexOf("(");
            num3 = str.IndexOf(")");
            str2 = str.Substring(num2 + 1, (num3 - num2) - 1).Trim();
            str3 = str.Substring(num3 + 1);
            num2 = str3.IndexOf("(");
            num3 = str3.Length - 1;
            str4 = str3.Substring(num2 + 1, (num3 - num2) - 1).Trim();
            str5 = ",";
            chArray = str5.ToCharArray();
            strArray = str2.Split(chArray);
            strArray2 = str4.Split(chArray);
            if (strArray.Length != strArray2.Length)
            {
                goto Label_01DE;
            }
            str6 = "<INSERT>";
            num4 = 0;
            goto Label_01B9;
        Label_0148:
            str21 = str6;
            str6 = str21 + "<" + strArray[num4].Trim() + ">" + strArray2[num4].Trim() + "</" + strArray[num4].Trim() + ">";
            num4 += 1;
        Label_01B9:
            if (num4 < strArray.Length)
            {
                goto Label_0148;
            }
            str7 = "</INSERT>";
            p_objVO.m_strRECORDDETAIL_VCHR = str6 + str7;
        Label_01DE:;
        Label_01DF:
            if (!str.StartsWith("UPDATE"))
            {
                goto Label_0648;
            }
            p_objVO.m_intTYPE_INT = 1;
            num2 = str.IndexOf("UPDATE");
            num3 = str.IndexOf("SET");
            p_objVO.m_strTABLENAME_VCHR = str.Substring(num2 + 6, (num3 - num2) - 6).Trim();
            num2 = str.IndexOf("SET");
            num3 = str.IndexOf("WHERE");
            str8 = str.Substring(num2 + 3, (num3 - num2) - 3).Trim();
            str5 = "=,";
            chArray = str5.ToCharArray();
            strArray3 = str8.Split(chArray);
            str6 = "<UPDATE><SET>";
            str9 = "</SET>";
            str10 = "<WHERE>";
            str11 = str.Substring(num3 + 5).Trim();
            str12 = str11;
            str13 = "[^=,!=,>,<,>=,<=]";
            str11 = str11 = str11.Replace("AND", ",");
            str11 = str11 = str11.Replace(str13, "=");
            str5 = "=,";
            chArray = str5.ToCharArray();
            strArray4 = str11.Split(chArray);
            str14 = "";
            str16 = "";
            num5 = 0;
            goto Label_0492;
        Label_0306:
            num2 = str12.IndexOf(strArray4[num5].Trim());
            num3 = str12.IndexOf(strArray4[num5 + 1].Trim());
            if (num3 > (num2 + strArray4[num5].Length))
            {
                goto Label_0363;
            }
            str15 = str12.Substring(num2 + strArray4[num5].Trim().Length, 1);
            goto Label_0389;
        Label_0363:
            str15 = str12.Substring(num2 + strArray4[num5].Length, num3 - (num2 + strArray4[num5].Length));
        Label_0389:
            str21 = str14;
            str14 = str21 + "<CONDITION>" + str15 + "</CONDITION><" + strArray4[num5].Trim() + ">" + strArray4[num5 + 1].Trim() + "</" + strArray4[num5].Trim() + ">";
            if (str16 != "")
            {
                goto Label_043D;
            }
            str16 = str16 + strArray4[num5].Trim() + str15 + strArray4[num5 + 1].Trim();
            goto Label_0485;
        Label_043D:
            str21 = str16;
            str16 = str21 + " and " + strArray4[num5].Trim() + str15 + strArray4[num5 + 1].Trim();
        Label_0485:
            num5 += 1;
            num5 += 1;
        Label_0492:
            if (num5 < strArray4.Length)
            {
                goto Label_0306;
            }
            str17 = "select * from " + p_objVO.m_strTABLENAME_VCHR + " where " + str16;
            table = new DataTable();
        Label_04C3:
            try
            {
                num = service.DoGetDataTable(str17, ref table);
                goto Label_04F3;
            }
            catch (Exception exception1)
            {
            Label_04D2:
                exception = exception1;
                str18 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_04F3;
            }
        Label_04F3:
            num4 = 0;
            goto Label_05FC;
        Label_04FC:
            num5 = 0;
            goto Label_05D6;
        Label_0505:
            if (table.Columns[num5].ColumnName.Trim() != strArray3[num4].Trim())
            {
                goto Label_05CF;
            }
            str21 = str6;
            str6 = str21 + "<" + strArray3[num4].Trim() + ">" + strArray3[num4 + 1].Trim() + "," + table.Rows[0][num5].ToString() + "</" + strArray3[num4].Trim() + ">";
            goto Label_05EF;
        Label_05CF:
            num5 += 1;
        Label_05D6:
            if (num5 < table.Columns.Count)
            {
                goto Label_0505;
            }
        Label_05EF:
            num4 += 1;
            num4 += 1;
        Label_05FC:
            if (num4 < strArray3.Length)
            {
                goto Label_04FC;
            }
            str7 = "</WHERE></UPDATE>";
            p_objVO.m_strRECORDDETAIL_VCHR = str6 + str9 + str10 + str14 + str7;
        Label_0648:
            if (!str.StartsWith("DELETE"))
            {
                goto Label_0852;
            }
            p_objVO.m_intTYPE_INT = 2;
            str19 = str;
            str19 = str19.Replace("FROM", "");
            num3 = str19.IndexOf("WHERE");
            p_objVO.m_strTABLENAME_VCHR = str19.Substring(6, num3 - 6).Trim();
            num3 = str.IndexOf("WHERE");
            str11 = str.Substring(num3 + 5).Trim();
            str12 = str11;
            str13 = "[^=,!=,>,<,>=,<=]";
            str11 = str11 = str11.Replace("AND", ",");
            str11 = str11 = str11.Replace(str13, "=");
            str5 = "=,";
            chArray = str5.ToCharArray();
            strArray5 = str11.Split(chArray);
            str6 = "<DELETE><WHERE>";
            num4 = 0;
            goto Label_082A;
        Label_071E:
            num2 = str12.IndexOf(strArray5[num4].Trim());
            num3 = str12.IndexOf(strArray5[num4 + 1].Trim());
            if (num3 > (num2 + strArray5[num4].Length))
            {
                goto Label_077B;
            }
            str15 = str12.Substring(num2 + strArray5[num4].Trim().Length, 1);
            goto Label_07A1;
        Label_077B:
            str15 = str12.Substring(num2 + strArray5[num4].Length, num3 - (num2 + strArray5[num4].Length));
        Label_07A1:
            str21 = str6;
            str6 = str21 + "<CONDITION>" + str15 + "</CONDITION><" + strArray5[num4].Trim() + ">" + strArray5[num4 + 1].Trim() + "</" + strArray5[num4].Trim() + ">";
            num4 += 1;
            num4 += 1;
        Label_082A:
            if (num4 < strArray5.Length)
            {
                goto Label_071E;
            }
            str7 = "</WHERE></DELETE>";
            p_objVO.m_strRECORDDETAIL_VCHR = str6 + str7;
        Label_0852:;
        Label_0853:
            p_objVO.m_strOPERATE_DAT = DateTime.Now.ToString();
            str20 = "insert into t_aid_recordmark (seq_int,tablename_vchr,tableseqid_chr,recorddetail_vchr,operatorid_chr,operate_dat,type_int) values (seq_remark.nextval,?,?,?,?,?,?)";
        Label_0874:
            try
            {
                parameterArray = null;
                service.CreateDatabaseParameter(6, out parameterArray);
                parameterArray[0].Value = p_objVO.m_strTABLENAME_VCHR;
                parameterArray[1].Value = p_objVO.m_strTABLESEQID_CHR;
                parameterArray[2].Value = Encoding.Default.GetBytes(p_objVO.m_strRECORDDETAIL_VCHR);
                parameterArray[3].Value = p_objVO.m_strOPERATORID_CHR;
                parameterArray[4].Value = (DateTime)DateTime.Parse(p_objVO.m_strOPERATE_DAT);
                parameterArray[5].Value = (int)p_objVO.m_intTYPE_INT;
                num6 = -1L;
                num = service.lngExecuteParameterSQL(str20, ref num6, parameterArray);
                goto Label_0930;
            }
            catch (Exception exception2)
            {
            Label_090F:
                exception = exception2;
                str18 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0930;
            }
        Label_0930:
            num7 = num;
        Label_0936:
            return num7;
        }

        public void m_mthAnalyseXML(string strXML, out clsRecordMarkFied_VO[] ValuesVO, out clsRecordMarkWhere_VO[] WhereVO, DataTable dtcomments)
        {
            DataTable table;
            DataTable table2;
            XmlDocument document;
            string str;
            XmlNode node;
            int num;
            DataRow row;
            int num2;
            string str2;
            char[] chArray;
            string[] strArray;
            int num3;
            int num4;
            bool flag;
            string str3;
            ValuesVO = null;
            WhereVO = null;
            table = new DataTable();
            table.Columns.Add("table_fied");
            table.Columns.Add("values");
            table.Columns.Add("valuesOld");
            table2 = new DataTable();
            table2.Columns.Add("table_wherefied");
            table2.Columns.Add("CONDITION");
            table2.Columns.Add("wherevalues");
            if (!string.IsNullOrEmpty(strXML))
            {
                goto Label_0098;
            }
            goto Label_0933;
        Label_0098:
            document = new XmlDocument();
            document.LoadXml(strXML);
        Label_00A6:
            try
            {
                if (document.HasChildNodes)
                {
                    goto Label_00B8;
                }
                goto Label_0933;
            Label_00B8:
                goto Label_00C2;
            }
            catch
            {
            Label_00BB:
                goto Label_0933;
            }
        Label_00C2:
            str = document.FirstChild.Name;
            node = document.FirstChild;
            if (node.HasChildNodes == false)
            {
                goto Label_065B;
            }
            str3 = node.Name;
            if (str3 == null)
            {
                goto Label_065A;
            }
            if (str3 == "INSERT")
            {
                goto Label_0130;
            }
            if (str3 == "UPDATE")
            {
                goto Label_01B0;
            }
            if (str3 == "DELETE")
            {
                goto Label_04A2;
            }
            goto Label_065A;
        Label_0130:
            table2 = null;
            num = 0;
            goto Label_0195;
        Label_0137:
            row = table.NewRow();
            row["table_fied"] = node.ChildNodes[num].Name;
            row["values"] = node.ChildNodes[num].InnerText;
            table.Rows.Add(row);
            num += 1;
        Label_0195:
            if (num < node.ChildNodes.Count)
            {
                goto Label_0137;
            }
            goto Label_065A;
        Label_01B0:
            num = 0;
            goto Label_0484;
        Label_01B8:
            if (node.ChildNodes[num].Name != "SET")
            {
                goto Label_02F2;
            }
            if (node.ChildNodes[num].HasChildNodes == false)
            {
                goto Label_02F1;
            }
            num2 = 0;
            goto Label_02CB;
        Label_020B:
            row = table.NewRow();
            row["table_fied"] = node.ChildNodes[num].ChildNodes[num2].Name;
            str2 = ",";
            chArray = str2.ToCharArray();
            strArray = node.ChildNodes[num].ChildNodes[num2].InnerText.Split(chArray);
            row["values"] = strArray[0];
        Label_0289:
            try
            {
                row["valuesOld"] = strArray[1];
                goto Label_02B5;
            }
            catch
            {
            Label_029E:
                row["valuesOld"] = "";
                goto Label_02B5;
            }
        Label_02B5:
            table.Rows.Add(row);
            num2 += 1;
        Label_02CB:
            if (num2 < node.ChildNodes[num].ChildNodes.Count)
            {
                goto Label_020B;
            }
        Label_02F1:;
        Label_02F2:
            if (node.ChildNodes[num].Name != "WHERE")
            {
                goto Label_047D;
            }
            if (node.ChildNodes[num].HasChildNodes == false)
            {
                goto Label_047C;
            }
            num3 = 0;
            goto Label_0456;
        Label_0344:
            row = table2.NewRow();
            num4 = 0;
            goto Label_0433;
        Label_0355:
            if (node.ChildNodes[num].ChildNodes[num3].Name != "CONDITION")
            {
                goto Label_03B8;
            }
            row["CONDITION"] = node.ChildNodes[num].ChildNodes[num3].InnerText;
            goto Label_0412;
        Label_03B8:
            row["table_wherefied"] = node.ChildNodes[num].ChildNodes[num3].Name;
            row["wherevalues"] = node.ChildNodes[num].ChildNodes[num3].InnerText;
        Label_0412:
            if (num4 != 1)
            {
                goto Label_0424;
            }
            goto Label_042C;
        Label_0424:
            num3 += 1;
        Label_042C:
            num4 += 1;
        Label_0433:
            if (num4 < 2)
            {
                goto Label_0355;
            }
            table2.Rows.Add(row);
            num3 += 1;
        Label_0456:
            if (num3 < node.ChildNodes[num].ChildNodes.Count)
            {
                goto Label_0344;
            }
        Label_047C:;
        Label_047D:
            num += 1;
        Label_0484:
            if (num < node.ChildNodes.Count)
            {
                goto Label_01B8;
            }
            goto Label_065A;
        Label_04A2:
            table = null;
            num = 0;
            goto Label_063F;
        Label_04AC:
            if (node.ChildNodes[num].Name != "WHERE")
            {
                goto Label_0638;
            }
            if (node.ChildNodes[num].HasChildNodes == false)
            {
                goto Label_0637;
            }
            num3 = 0;
            goto Label_0611;
        Label_04FF:
            row = table2.NewRow();
            num4 = 0;
            goto Label_05EE;
        Label_0510:
            if (node.ChildNodes[num].ChildNodes[num3].Name != "CONDITION")
            {
                goto Label_0573;
            }
            row["CONDITION"] = node.ChildNodes[num].ChildNodes[num3].InnerText;
            goto Label_05CD;
        Label_0573:
            row["table_wherefied"] = node.ChildNodes[num].ChildNodes[num3].Name;
            row["wherevalues"] = node.ChildNodes[num].ChildNodes[num3].InnerText;
        Label_05CD:
            if (num4 != 1)
            {
                goto Label_05DF;
            }
            goto Label_05E7;
        Label_05DF:
            num3 += 1;
        Label_05E7:
            num4 += 1;
        Label_05EE:
            if (num4 < 2)
            {
                goto Label_0510;
            }
            table2.Rows.Add(row);
            num3 += 1;
        Label_0611:
            if (num3 < node.ChildNodes[num].ChildNodes.Count)
            {
                goto Label_04FF;
            }
        Label_0637:;
        Label_0638:
            num += 1;
        Label_063F:
            if (num < node.ChildNodes.Count)
            {
                goto Label_04AC;
            }
        Label_065A:;
        Label_065B:
            if (table != null && table.Rows.Count > 0)
            {
                ValuesVO = new clsRecordMarkFied_VO[table.Rows.Count];
                num = 0;
                goto Label_07AE;
            }
            else
            {
                goto Label_07C7;
            }
        Label_0696:
            ValuesVO[num] = new clsRecordMarkFied_VO();
            ValuesVO[num].m_strFiedName_VCHR = table.Rows[num]["table_fied"].ToString();
            ValuesVO[num].m_strFiedValues_CHR = table.Rows[num]["values"].ToString();
            ValuesVO[num].m_strFiedValuesOLD_CHR = table.Rows[num]["valuesOld"].ToString();
            num3 = 0;
            goto Label_078E;
        Label_0718:
            if (ValuesVO[num].m_strFiedName_VCHR.Trim() != dtcomments.Rows[num3]["COLUMN_NAME"].ToString().Trim())
            {
                goto Label_0787;
            }
            ValuesVO[num].m_strFiedComments_CHR = dtcomments.Rows[num3]["COMMENTS"].ToString().Trim();
            goto Label_07A7;
        Label_0787:
            num3 += 1;
        Label_078E:
            if (num3 < dtcomments.Rows.Count)
            {
                goto Label_0718;
            }
        Label_07A7:
            num += 1;
        Label_07AE:
            if (num < table.Rows.Count)
            {
                goto Label_0696;
            }
        Label_07C7:

            if (table2 != null && table2.Rows.Count > 0)
            {
                WhereVO = new clsRecordMarkWhere_VO[table2.Rows.Count];
                num = 0;
                goto Label_091A;
            }
            else
            {
                goto Label_0933;
            }
        Label_0802:
            WhereVO[num] = new clsRecordMarkWhere_VO();
            WhereVO[num].m_strFiedName_VCHR = table2.Rows[num]["table_wherefied"].ToString();
            WhereVO[num].m_strFiedValues_CHR = table2.Rows[num]["wherevalues"].ToString();
            WhereVO[num].m_strFiedCONDITION_CHR = table2.Rows[num]["CONDITION"].ToString();
            num3 = 0;
            goto Label_08FA;
        Label_0884:
            if (WhereVO[num].m_strFiedName_VCHR.Trim() != dtcomments.Rows[num3]["COLUMN_NAME"].ToString().Trim())
            {
                goto Label_08F3;
            }
            WhereVO[num].m_strFiedComments_CHR = dtcomments.Rows[num3]["COMMENTS"].ToString().Trim();
            goto Label_0913;
        Label_08F3:
            num3 += 1;
        Label_08FA:
            if (num3 < dtcomments.Rows.Count)
            {
                goto Label_0884;
            }
        Label_0913:
            num += 1;
        Label_091A:
            if (num < table2.Rows.Count)
            {
                goto Label_0802;
            }
        Label_0933:
            return;
        }

        [AutoComplete]
        public long m_objGetRecordMark(out clsRecordMark_VO[] p_objVO, string dateStar, string dateEnd)
        {
            long num;
            string str;
            DataTable table;
            clsHRPTableService service2;
            string str2;
            DataTable table2;
            int num2;
            Exception exception;
            string str3;
            clsLogText text;
            bool flag;
            long num3;
            DateTime time;
            bool flag2;
            string[] strArray;
            p_objVO = new clsRecordMark_VO[0];
            num = 0L;
        Label_000C:
            try
            {
                dateStar = DateTime.Parse(dateStar).ToShortDateString();
                dateEnd = DateTime.Parse(dateEnd).ToShortDateString();
                goto Label_0038;
            }
            catch
            {
            Label_0033:
                goto Label_0038;
            }
        Label_0038:
            goto Label_0069;
        Label_0069:;
            str = "SELECT a.*,b.LASTNAME_VCHR  FROM T_AID_RECORDMARK a,T_BSE_EMPLOYEE b WHERE  a.OPERATE_DAT between To_date('" + dateStar + " 00:00:00','yyyy-mm-dd hh24:mi:ss') and  To_date('" + dateEnd + " 23:59:59','yyyy-mm-dd hh24:mi:ss') and a.OPERATORID_CHR=b.EMPID_CHR(+)";
        Label_009F:
            try
            {
                table = new DataTable();
                service2 = new clsHRPTableService();
                num = service2.lngGetDataTableWithoutParameters(str, ref table);
                if (table != null && table.Rows.Count > 0)
                {
                    str2 = "";
                    p_objVO = new clsRecordMark_VO[table.Rows.Count];
                    table2 = new DataTable();
                    num2 = 0;
                    goto Label_02F7;
                }
                else
                {
                    goto Label_0309;
                }
            Label_0103:
                p_objVO[num2] = new clsRecordMark_VO();
                p_objVO[num2].m_strTABLENAME_VCHR = table.Rows[num2]["TABLENAME_VCHR"].ToString().Trim();
                p_objVO[num2].m_strTABLESEQID_CHR = table.Rows[num2]["TABLESEQID_CHR"].ToString().Trim();
                p_objVO[num2].m_strOPERATORID_CHR = table.Rows[num2]["OPERATORID_CHR"].ToString().Trim();
                p_objVO[num2].m_strOPERATORNAME_CHR = table.Rows[num2]["LASTNAME_VCHR"].ToString().Trim();
                p_objVO[num2].m_strOPERATE_DAT = Convert.ToDateTime(table.Rows[num2]["OPERATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                p_objVO[num2].m_intTYPE_INT = Convert.ToInt32(table.Rows[num2]["TYPE_INT"].ToString().Trim());
                p_objVO[num2].m_strRECORDDETAIL_VCHR = Encoding.Default.GetString((byte[])table.Rows[num2]["RECORDDETAIL_VCHR"]);
                if (p_objVO[num2].m_strTABLENAME_VCHR == str2)
                {
                    goto Label_02C9;
                }
                table2 = null;
                str = "SELECT *  FROM all_col_comments WHERE table_name = '" + p_objVO[num2].m_strTABLENAME_VCHR + "' AND owner = 'ICARE'";
            Label_028B:
                try
                {
                    num = service2.lngGetDataTableWithoutParameters(str, ref table2);
                    goto Label_02BB;
                }
                catch (Exception exception1)
                {
                Label_029A:
                    exception = exception1;
                    str3 = exception.Message;
                    text = new clsLogText();
                    flag = text.LogError(exception);
                    goto Label_02BB;
                }
            Label_02BB:
                str2 = p_objVO[num2].m_strTABLENAME_VCHR;
            Label_02C9:
                this.m_mthAnalyseXML(p_objVO[num2].m_strRECORDDETAIL_VCHR, out p_objVO[num2].m_objMarkFied, out p_objVO[num2].m_objMarkWhere, table2);
                num2 += 1;
            Label_02F7:
                if (num2 < p_objVO.Length)
                {
                    goto Label_0103;
                }
            Label_0309:
                goto Label_032D;
            }
            catch (Exception exception2)
            {
            Label_030C:
                exception = exception2;
                str3 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_032D;
            }
        Label_032D:
            num3 = num;
        Label_0333:
            return num3;
        }

        [AutoComplete]
        public long m_objGetRecordMark(out clsRecordMark_VO[] p_objVO, string dateStar, string dateEnd, string tableName)
        {
            string str;
            string[] strArray;
            int num;
            long num2;
            string str2;
            string str3;
            DataTable table;
            clsHRPTableService service2;
            DataTable table2;
            Exception exception;
            string str4;
            clsLogText text;
            bool flag;
            long num3;
            bool flag2;
            char[] chArray;
            DateTime time;
            string[] strArray2;
            str = "";
            if (string.IsNullOrEmpty(tableName))
            {
                goto Label_010E;
            }
            strArray = tableName.Split(new char[] { '+' });
            if (strArray.Length <= 0)
            {
                goto Label_010D;
            }
            num = 0;
            goto Label_00FD;
        Label_0052:
            if (num != 0)
            {
                goto Label_00AD;
            }
            if (num != strArray.Length - 1)
            {
                goto Label_008F;
            }
            str = str + " and  a.TABLENAME_VCHR='" + strArray[num].Trim() + "' ";
            goto Label_00AA;
        Label_008F:
            str = str + " and  (a.TABLENAME_VCHR='" + strArray[num].Trim() + "'";
        Label_00AA:
            goto Label_00F8;
        Label_00AD:
            if (num != strArray.Length - 1)
            {
                goto Label_00DC;
            }
            str = str + " or a.TABLENAME_VCHR='" + strArray[num].Trim() + "')";
            goto Label_00F7;
        Label_00DC:
            str = str + " or a.TABLENAME_VCHR='" + strArray[num].Trim() + "'";
        Label_00F7:;
        Label_00F8:
            num += 1;
        Label_00FD:
            if (num < ((int)strArray.Length))
            {
                goto Label_0052;
            }
        Label_010D:;
        Label_010E:
            p_objVO = new clsRecordMark_VO[0];
            num2 = 0L;
        Label_0119:
            try
            {
                dateStar = DateTime.Parse(dateStar).ToShortDateString();
                dateEnd = DateTime.Parse(dateEnd).ToShortDateString();
                goto Label_0145;
            }
            catch
            {
            Label_0140:
                goto Label_0145;
            }
        Label_0145:
            goto Label_0178;
        Label_0178:;
            str2 = "SELECT a.*,b.LASTNAME_VCHR  FROM T_AID_RECORDMARK a,T_BSE_EMPLOYEE b WHERE a.OPERATE_DAT between To_date('" + dateStar + " 00:00:00','yyyy-mm-dd hh24:mi:ss') and  To_date('" + dateEnd + " 23:59:59','yyyy-mm-dd hh24:mi:ss') and a.OPERATORID_CHR=b.EMPID_CHR(+) " + str;
        Label_01B4:
            try
            {
                str3 = "";
                table = new DataTable();
                service2 = new clsHRPTableService();
                num2 = service2.lngGetDataTableWithoutParameters(str2, ref table);
                if (table != null && table.Rows.Count > 0)
                {
                    p_objVO = new clsRecordMark_VO[table.Rows.Count];
                    table2 = new DataTable();
                    num = 0;
                    goto Label_0401;
                }
                else
                {
                    goto Label_0412;
                }

            Label_021B:
                p_objVO[num] = new clsRecordMark_VO();
                p_objVO[num].m_strTABLENAME_VCHR = table.Rows[num]["TABLENAME_VCHR"].ToString().Trim();
                p_objVO[num].m_strTABLESEQID_CHR = table.Rows[num]["TABLESEQID_CHR"].ToString().Trim();
                p_objVO[num].m_strOPERATORID_CHR = table.Rows[num]["OPERATORID_CHR"].ToString().Trim();
                p_objVO[num].m_strOPERATORNAME_CHR = table.Rows[num]["LASTNAME_VCHR"].ToString().Trim();
                p_objVO[num].m_strOPERATE_DAT = Convert.ToDateTime(table.Rows[num]["OPERATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                p_objVO[num].m_intTYPE_INT = Convert.ToInt32(table.Rows[num]["TYPE_INT"].ToString().Trim());
                p_objVO[num].m_strRECORDDETAIL_VCHR = Encoding.Default.GetString((byte[])table.Rows[num]["RECORDDETAIL_VCHR"]);
                if (p_objVO[num].m_strTABLENAME_VCHR == str3)
                {
                    goto Label_03D8;
                }
                table2 = null;
                str2 = "SELECT *  FROM all_col_comments WHERE table_name = '" + p_objVO[num].m_strTABLENAME_VCHR + "' AND owner = 'ICARE'";
            Label_039A:
                try
                {
                    num2 = service2.lngGetDataTableWithoutParameters(str2, ref table2);
                    goto Label_03CB;
                }
                catch (Exception exception1)
                {
                Label_03AA:
                    exception = exception1;
                    str4 = exception.Message;
                    text = new clsLogText();
                    flag = text.LogError(exception);
                    goto Label_03CB;
                }
            Label_03CB:
                str3 = p_objVO[num].m_strTABLENAME_VCHR;
            Label_03D8:
                this.m_mthAnalyseXML(p_objVO[num].m_strRECORDDETAIL_VCHR, out p_objVO[num].m_objMarkFied, out p_objVO[num].m_objMarkWhere, table2);
                num += 1;
            Label_0401:
                if (num < p_objVO.Length)
                {
                    goto Label_021B;
                }
            Label_0412:
                goto Label_0436;
            }
            catch (Exception exception2)
            {
            Label_0415:
                exception = exception2;
                str4 = exception.Message;
                text = new clsLogText();
                flag = text.LogError(exception);
                goto Label_0436;
            }
        Label_0436:
            num3 = num2;
        Label_043C:
            return num3;
        }
    }
}
