using System;
using System.Data;
using System.EnterpriseServices;
using System.Security.Principal;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.Utility;//Utility.dll
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll 

namespace com.digitalwave.iCare.middletier.LIS
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsQueryStatSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        public clsQueryStatSvc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

        #region ����������ѯѧ��ͳ�Ƶ���Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDatFrom"></param>
        /// <param name="p_strDatTo"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_strResultFrom"></param>
        /// <param name="p_strResultTo"></param>
        /// <param name="p_strAgeFrom"></param>
        /// <param name="p_strAgeTo"></param>
        /// <param name="p_strSex"></param>
        /// <param name="p_strLowCompare">���ޱȽϷ�</param>
        /// <param name="p_strCondition">�Ƚ�����</param>
        /// <param name="p_strUpCompare">���ޱȽϷ�</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetScienceStatByCondition( string p_strDatFrom, string p_strDatTo,
            string p_strCheckItemID, string p_strResultFrom, string p_strResultTo, string p_strAgeFrom, string p_strAgeTo, string p_strSex,
            string p_strLowCompare, string p_strCondition, string p_strUpCompare, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null; 
            string strTableNumORChr = "";

            if (p_strLowCompare == "=" || p_strLowCompare == "<>")
            {
                strTableNumORChr = ",t_opr_lis_check_result t2";
            }
            else
            {
                strTableNumORChr = @", (SELECT *
										  FROM t_opr_lis_check_result
										 WHERE f_isnumber(result_vchr) = 1) t2";
            }

            string strSQL = @"SELECT t1.*, t2.*
								FROM t_opr_lis_sample t1" + strTableNumORChr + @"
							   WHERE t1.sample_id_chr = t2.sample_id_chr
								 AND t1.status_int = 6
								 AND t2.status_int > 0";

            strSQL += @" AND t2.MODIFY_DAT BETWEEN TO_DATE('" + p_strDatFrom + @"','yyyy-mm-dd hh24:mi:ss') 
											   AND TO_DATE('" + p_strDatTo + @"','yyyy-mm-dd hh24:mi:ss')";
            strSQL += @" AND t2.CHECK_ITEM_ID_CHR = '" + p_strCheckItemID + @"'";

            if (p_strSex != null && p_strSex != "")
            {
                strSQL += @" AND SEX_CHR = '" + p_strSex + @"'";
            }
            if (p_strResultFrom != null && p_strResultFrom != "" && (p_strResultTo == null || p_strResultTo == ""))
            {
                if (p_strLowCompare == ">")
                {
                    strSQL += @" AND TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @"";
                }
                if (p_strLowCompare == ">=")
                {
                    strSQL += @" AND TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @"";
                }
                if (p_strLowCompare == "=")
                {
                    strSQL += @" AND RESULT_VCHR = '" + p_strResultFrom + @"'";
                }
                if (p_strLowCompare == "<>")
                {
                    strSQL += @" AND RESULT_VCHR <> '" + p_strResultFrom + @"'";
                }
                if (p_strLowCompare == "IN" || p_strLowCompare == "NOT IN")
                {
                    //�滻��Ӣ�Ķ���
                    p_strResultFrom = p_strResultFrom.Replace("��", ",");
                    string[] strResultArr = p_strResultFrom.Split(new char[] { ',' });
                    if (p_strLowCompare == "IN")
                    {
                        strSQL += @" AND RESULT_VCHR IN (";
                    }
                    else if (p_strLowCompare == "NOT IN")
                    {
                        strSQL += @" AND RESULT_VCHR NOT IN (";
                    }
                    for (int i = 0; i < strResultArr.Length; i++)
                    {
                        if (i != strResultArr.Length - 1)
                        {
                            strSQL += strResultArr[i].Trim() + ",";
                        }
                        else
                        {
                            strSQL += strResultArr[i].Trim() + ")";
                        }
                    }
                }
            }

            if (p_strResultTo != null && p_strResultTo != "" && (p_strResultFrom == null || p_strResultFrom == ""))
            {
                if (p_strUpCompare == "<")
                {
                    strSQL += @" AND TO_NUMBER(RESULT_VCHR) < " + p_strResultTo + @"";
                }

                if (p_strUpCompare == "<=")
                {
                    strSQL += @" AND TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @"";
                }
            }

            if (p_strResultTo != null && p_strResultTo != "" && p_strResultFrom != null && p_strResultFrom != "")
            {
                if (p_strCondition == "��")
                {
                    if (p_strUpCompare == "<")
                    {
                        if (p_strLowCompare == ">")
                        {
                            strSQL += @" AND TO_NUMBER(RESULT_VCHR) < " + p_strResultTo + @" AND TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @"";
                        }
                        if (p_strLowCompare == ">=")
                        {
                            strSQL += @" AND TO_NUMBER(RESULT_VCHR) < '" + p_strResultTo + @"' AND TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @"";
                        }
                    }

                    if (p_strUpCompare == "<=")
                    {
                        if (p_strLowCompare == ">")
                        {
                            strSQL += @" AND TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @" AND TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @"";
                        }
                        if (p_strLowCompare == ">=")
                        {
                            strSQL += @" AND TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @" AND TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @"";
                        }
                    }
                }

                if (p_strCondition == "��")
                {
                    if (p_strUpCompare == "<")
                    {
                        if (p_strLowCompare == ">")
                        {
                            strSQL += @" AND (TO_NUMBER(RESULT_VCHR) < " + p_strResultTo + @" OR TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @")";
                        }
                        if (p_strLowCompare == ">=")
                        {
                            strSQL += @" AND (TO_NUMBER(RESULT_VCHR) < " + p_strResultTo + @" OR TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @")";
                        }
                    }

                    if (p_strUpCompare == "<=")
                    {
                        if (p_strLowCompare == ">")
                        {
                            strSQL += @" AND (TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @" OR TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @")";
                        }
                        if (p_strLowCompare == ">=")
                        {
                            strSQL += @" AND (TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @" OR TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @")";
                        }
                    }
                }
            }

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null)
                {
                    if (p_strAgeFrom != null && p_strAgeFrom != "")
                    {
                        string[] strRawAgeArr = p_strAgeFrom.Split(new char[] { ' ' });
                        int intAgeIdx = 0;
                        if (strRawAgeArr[1] == "Сʱ")
                        {
                            intAgeIdx = 0;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 1;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 2;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 3;
                        }
                        for (int i = 0; i < dtbResult.Rows.Count; i++)
                        {
                            string[] strResultAgeArr = dtbResult.Rows[i]["AGE_CHR"].ToString().Trim().Split(new char[] { ' ' });
                            if (strResultAgeArr.Length == 2)
                            {
                                int intResultAgeIdx = 0;
                                if (strResultAgeArr[1] == "Сʱ")
                                {
                                    intResultAgeIdx = 0;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 1;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 2;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 3;
                                }
                                if (intResultAgeIdx < intAgeIdx)
                                {
                                    dtbResult.Rows[i].Delete();
                                }
                                if (intResultAgeIdx == intAgeIdx)
                                {
                                    if (double.Parse(strResultAgeArr[0]) < double.Parse(strRawAgeArr[0]))
                                    {
                                        dtbResult.Rows[i].Delete();
                                    }
                                }
                            }
                            else
                            {
                                dtbResult.Rows[i].Delete();
                            }
                        }
                        dtbResult.AcceptChanges();
                    }
                    if (p_strAgeTo != null && p_strAgeTo != "")
                    {
                        string[] strRawAgeArr = p_strAgeTo.Split(new char[] { ' ' });
                        int intAgeIdx = 0;
                        if (strRawAgeArr[1] == "Сʱ")
                        {
                            intAgeIdx = 0;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 1;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 2;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 3;
                        }
                        for (int i = 0; i < dtbResult.Rows.Count; i++)
                        {
                            string[] strResultAgeArr = dtbResult.Rows[i]["AGE_CHR"].ToString().Trim().Split(new char[] { ' ' });
                            if (strResultAgeArr.Length == 2)
                            {
                                int intResultAgeIdx = 0;
                                if (strResultAgeArr[1] == "Сʱ")
                                {
                                    intResultAgeIdx = 0;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 1;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 2;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 3;
                                }
                                if (intResultAgeIdx > intAgeIdx)
                                {
                                    dtbResult.Rows[i].Delete();
                                }
                                if (intResultAgeIdx == intAgeIdx)
                                {
                                    if (double.Parse(strResultAgeArr[0]) > double.Parse(strRawAgeArr[0]))
                                    {
                                        dtbResult.Rows[i].Delete();
                                    }
                                }
                            }
                            else
                            {
                                dtbResult.Rows[i].Delete();
                            }
                        }
                        dtbResult.AcceptChanges();
                    }
                    //					p_objResultArr = new clsLisScienceStat_VO[dtbResult.Rows.Count];
                    //					for(int i=0;i<p_objResultArr.Length;i++)
                    //					{
                    //						p_objResultArr[i] = new clsLisScienceStat_VO();
                    //						p_objResultArr[i].m_strResult = dtbResult.Rows[i]["RESULT_VCHR"].ToString().Trim();
                    //						p_objResultArr[i].m_strAge = dtbResult.Rows[i]["AGE_CHR"].ToString().Trim();
                    //						p_objResultArr[i].m_strCheckDat = dtbResult.Rows[i]["CHECK_DATE_DAT"].ToString().Trim();
                    //						p_objResultArr[i].m_strPatientName = dtbResult.Rows[i]["PATIENT_NAME_VCHR"].ToString().Trim();
                    //						p_objResultArr[i].m_strSampleNO = dtbResult.Rows[i]["SAMPLE_ID_CHR"].ToString().Trim();
                    //						p_objResultArr[i].m_strSex = dtbResult.Rows[i]["SEX_CHR"].ToString().Trim();
                    //					}
                }
            }
            //			try
            //			{
            //				DataTable dtb = null;
            //				clsHRPTableService objSvc = new clsHRPTableService();
            //				objSvc.lngGetDataTableWithoutParameters(@"select * from v_lis_number_result where to_number(result_vchr) > 1 and modify_dat > to_date('2004-11-10 00:00:00','yyyy-mm-dd hh24:mi:ss') ",ref dtb);
            //			}
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����������ѯѧ��ͳ�Ƶ���Ϣ
        /// <summary>
        /// ����������ѯѧ��ͳ�Ƶ���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDatFrom"></param>
        /// <param name="p_strDatTo"></param>
        /// <param name="p_strAgeFrom"></param>
        /// <param name="p_strAgeTo"></param>
        /// <param name="p_strSex"></param>
        /// <param name="p_objRecordArr"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetScienceStatByCondition( string p_strDatFrom, string p_strDatTo,
            string p_strAgeFrom, string p_strAgeTo, string p_strSex, clsLisScienceStatItemQueryCondition[] p_objRecordArr, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null; 

            string[] strSQLItemArr = m_strConstructSQL(p_objRecordArr, p_strDatFrom, p_strDatTo);
            string strSQLItem = "";
            string strSQLCondition = "";

            for (int i = 0; i < strSQLItemArr.Length; i++)
            {
                string strTableName = "t" + ((int)(i + 2)).ToString();
                strSQLItem += ",(" + strSQLItemArr[i] + ") " + strTableName;
                strSQLCondition += " AND t1.sample_id_chr = " + strTableName + ".sample_id_chr";
            }

            string strSQL = @"SELECT t1.*, t2.*
								FROM t_opr_lis_sample t1" + strSQLItem + @"
							   WHERE t1.status_int = 6" + strSQLCondition;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null)
                {
                    if (p_strAgeFrom != null && p_strAgeFrom != "")
                    {
                        string[] strRawAgeArr = p_strAgeFrom.Split(new char[] { ' ' });
                        int intAgeIdx = 0;
                        if (strRawAgeArr[1] == "Сʱ")
                        {
                            intAgeIdx = 0;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 1;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 2;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 3;
                        }
                        for (int i = 0; i < dtbResult.Rows.Count; i++)
                        {
                            string[] strResultAgeArr = dtbResult.Rows[i]["AGE_CHR"].ToString().Trim().Split(new char[] { ' ' });
                            if (strResultAgeArr.Length == 2)
                            {
                                int intResultAgeIdx = 0;
                                if (strResultAgeArr[1] == "Сʱ")
                                {
                                    intResultAgeIdx = 0;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 1;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 2;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 3;
                                }
                                if (intResultAgeIdx < intAgeIdx)
                                {
                                    dtbResult.Rows[i].Delete();
                                }
                                if (intResultAgeIdx == intAgeIdx)
                                {
                                    if (double.Parse(strResultAgeArr[0]) < double.Parse(strRawAgeArr[0]))
                                    {
                                        dtbResult.Rows[i].Delete();
                                    }
                                }
                            }
                            else
                            {
                                dtbResult.Rows[i].Delete();
                            }
                        }
                        dtbResult.AcceptChanges();
                    }
                    if (p_strAgeTo != null && p_strAgeTo != "")
                    {
                        string[] strRawAgeArr = p_strAgeTo.Split(new char[] { ' ' });
                        int intAgeIdx = 0;
                        if (strRawAgeArr[1] == "Сʱ")
                        {
                            intAgeIdx = 0;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 1;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 2;
                        }
                        if (strRawAgeArr[1] == "��")
                        {
                            intAgeIdx = 3;
                        }
                        for (int i = 0; i < dtbResult.Rows.Count; i++)
                        {
                            string[] strResultAgeArr = dtbResult.Rows[i]["AGE_CHR"].ToString().Trim().Split(new char[] { ' ' });
                            if (strResultAgeArr.Length == 2)
                            {
                                int intResultAgeIdx = 0;
                                if (strResultAgeArr[1] == "Сʱ")
                                {
                                    intResultAgeIdx = 0;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 1;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 2;
                                }
                                if (strResultAgeArr[1] == "��")
                                {
                                    intResultAgeIdx = 3;
                                }
                                if (intResultAgeIdx > intAgeIdx)
                                {
                                    dtbResult.Rows[i].Delete();
                                }
                                if (intResultAgeIdx == intAgeIdx)
                                {
                                    if (double.Parse(strResultAgeArr[0]) > double.Parse(strRawAgeArr[0]))
                                    {
                                        dtbResult.Rows[i].Delete();
                                    }
                                }
                            }
                            else
                            {
                                dtbResult.Rows[i].Delete();
                            }
                        }
                        dtbResult.AcceptChanges();
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

        #region ����ѧ��ͳ�Ƽ�����Ŀ��SQL
        [AutoComplete]
        private string[] m_strConstructSQL(clsLisScienceStatItemQueryCondition[] p_objRecordArr, string p_strCheckDatFrom, string p_strCheckDatTo)
        {
            string[] strSQLArr = new string[p_objRecordArr.Length];
            for (int i = 0; i < p_objRecordArr.Length; i++)
            {
                string p_strResultFrom = p_objRecordArr[i].m_strLowResult;
                string p_strResultTo = p_objRecordArr[i].m_strUpResult;
                string p_strLowCompare = p_objRecordArr[i].m_strLowCondition;
                string p_strUpCompare = p_objRecordArr[i].m_strUpCondition;
                string p_strCondition = p_objRecordArr[i].m_strResultRelation;

                if (p_strLowCompare == "=" || p_strLowCompare == "<>")
                {
                    strSQLArr[i] = @"SELECT *
									   FROM t_opr_lis_check_result
                                      WHERE modify_dat BETWEEN TO_DATE ('" + p_strCheckDatFrom + @"',
																		'yyyy-mm-dd hh24:mi:ss'
																		)
															AND TO_DATE ('" + p_strCheckDatTo + @"',
																		'yyyy-mm-dd hh24:mi:ss'
																		)
										AND status_int > 0
										AND check_item_id_chr = '" + p_objRecordArr[i].m_strCheckItemID + @"'";
                }
                else
                {
                    strSQLArr[i] = @"SELECT *
										FROM (SELECT *
												FROM t_opr_lis_check_result
												WHERE f_isnumber (result_vchr) = 1)
										WHERE modify_dat BETWEEN TO_DATE ('" + p_strCheckDatFrom + @"',
																		'yyyy-mm-dd hh24:mi:ss'
																		)
															AND TO_DATE ('" + p_strCheckDatTo + @"',
																		'yyyy-mm-dd hh24:mi:ss'
																		)
										AND status_int > 0
										AND check_item_id_chr = '" + p_objRecordArr[i].m_strCheckItemID + @"'";
                }

                if (p_strResultFrom != null && p_strResultFrom != "" && (p_strResultTo == null || p_strResultTo == ""))
                {
                    if (p_strLowCompare == ">")
                    {
                        strSQLArr[i] += @" AND TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @"";
                    }
                    if (p_strLowCompare == ">=")
                    {
                        strSQLArr[i] += @" AND TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @"";
                    }
                    if (p_strLowCompare == "=")
                    {
                        strSQLArr[i] += @" AND RESULT_VCHR = '" + p_strResultFrom + @"'";
                    }
                    if (p_strLowCompare == "<>")
                    {
                        strSQLArr[i] += @" AND RESULT_VCHR <> '" + p_strResultFrom + @"'";
                    }
                    if (p_strLowCompare == "IN" || p_strLowCompare == "NOT IN")
                    {
                        //�滻��Ӣ�Ķ���
                        p_strResultFrom = p_strResultFrom.Replace("��", ",");
                        string[] strResultArr = p_strResultFrom.Split(new char[] { ',' });
                        if (p_strLowCompare == "IN")
                        {
                            strSQLArr[i] += @" AND RESULT_VCHR IN (";
                        }
                        else if (p_strLowCompare == "NOT IN")
                        {
                            strSQLArr[i] += @" AND RESULT_VCHR NOT IN (";
                        }
                        for (int j = 0; j < strResultArr.Length; j++)
                        {
                            if (i != strResultArr.Length - 1)
                            {
                                strSQLArr[j] += strResultArr[j].Trim() + ",";
                            }
                            else
                            {
                                strSQLArr[j] += strResultArr[j].Trim() + ")";
                            }
                        }
                    }
                }

                if (p_strResultTo != null && p_strResultTo != "" && (p_strResultFrom == null || p_strResultFrom == ""))
                {
                    if (p_strUpCompare == "<")
                    {
                        strSQLArr[i] += @" AND TO_NUMBER(RESULT_VCHR) < " + p_strResultTo + @"";
                    }

                    if (p_strUpCompare == "<=")
                    {
                        strSQLArr[i] += @" AND TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @"";
                    }
                }

                if (p_strResultTo != null && p_strResultTo != "" && p_strResultFrom != null && p_strResultFrom != "")
                {
                    if (p_strCondition == "��")
                    {
                        if (p_strUpCompare == "<")
                        {
                            if (p_strLowCompare == ">")
                            {
                                strSQLArr[i] += @" AND TO_NUMBER(RESULT_VCHR) < " + p_strResultTo + @" AND TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @"";
                            }
                            if (p_strLowCompare == ">=")
                            {
                                strSQLArr[i] += @" AND TO_NUMBER(RESULT_VCHR) < '" + p_strResultTo + @"' AND TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @"";
                            }
                        }

                        if (p_strUpCompare == "<=")
                        {
                            if (p_strLowCompare == ">")
                            {
                                strSQLArr[i] += @" AND TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @" AND TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @"";
                            }
                            if (p_strLowCompare == ">=")
                            {
                                strSQLArr[i] += @" AND TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @" AND TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @"";
                            }
                        }
                    }

                    if (p_strCondition == "��")
                    {
                        if (p_strUpCompare == "<")
                        {
                            if (p_strLowCompare == ">")
                            {
                                strSQLArr[i] += @" AND (TO_NUMBER(RESULT_VCHR) < " + p_strResultTo + @" OR TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @")";
                            }
                            if (p_strLowCompare == ">=")
                            {
                                strSQLArr[i] += @" AND (TO_NUMBER(RESULT_VCHR) < " + p_strResultTo + @" OR TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @")";
                            }
                        }

                        if (p_strUpCompare == "<=")
                        {
                            if (p_strLowCompare == ">")
                            {
                                strSQLArr[i] += @" AND (TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @" OR TO_NUMBER(RESULT_VCHR) > " + p_strResultFrom + @")";
                            }
                            if (p_strLowCompare == ">=")
                            {
                                strSQLArr[i] += @" AND (TO_NUMBER(RESULT_VCHR) <= " + p_strResultTo + @" OR TO_NUMBER(RESULT_VCHR) >= " + p_strResultFrom + @")";
                            }
                        }
                    }
                }
            }
            return strSQLArr;
        }
        #endregion

        #endregion

        #region ����������ѯѧ��ͳ�Ƶ���Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDatFrom"></param>
        /// <param name="p_strDatTo"></param>
        /// <param name="p_strAgeFrom"></param>
        /// <param name="p_strAgeTo"></param>
        /// <param name="p_strSex"></param>
        /// <param name="p_objRecordArr"></param>
        /// <param name="dtbHead"></param>
        /// <param name="dtbDetail"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetScienceStatByCondition( string p_strDatFrom, string p_strDatTo,
            string p_strAgeFrom, string p_strAgeTo, string p_strSex, clsLisScienceStatItemQueryCondition[] p_objRecordArr, out DataTable dtbHead,
            out DataTable dtbDetail)
        {
            long lngRes = 0;
            dtbHead = null;
            dtbDetail = null; 

            #region SQL-Result
            string strDetail_SQL = @"SELECT a.*
									   FROM t_opr_lis_check_result a
					                  WHERE a.status_int > 0";
            string strTable = "";
            string strCondition = "";
            string[] strResultArr = m_strConstructSQL(p_objRecordArr, p_strDatFrom, p_strDatTo);
            for (int i = 0; i < strResultArr.Length; i++)
            {
                strTable += "(" + strResultArr[i] + ") t" + i.ToString();
                if (i < strResultArr.Length - 1)
                {
                    strTable += ",";
                }
                if (i > 0)
                {
                    strCondition += " AND t" + i.ToString() + ".sample_id_chr = t" + (i - 1).ToString() + ".sample_id_chr";
                }
            }
            string strResult_SQL = @"SELECT /*+ USE_NL(t0) */t0.sample_id_chr FROM " + strTable;
            if (strResultArr.Length > 1)
            {
                strResult_SQL += " WHERE 1=1 " + strCondition;
            }
            strDetail_SQL += " AND a.sample_id_chr in ( " + strResult_SQL + ")";
            string strCheckItemID = "";
            for (int i = 0; i < p_objRecordArr.Length; i++)
            {
                strCheckItemID += p_objRecordArr[i].m_strCheckItemID;
                if (i < p_objRecordArr.Length - 1)
                {
                    strCheckItemID += ",";
                }
            }
            strDetail_SQL += " AND a.check_item_id_chr in (" + strCheckItemID + ")";
            #endregion

            #region SQL-Sample
            string strHead_SQL = @"SELECT a.*, c.deptname_vchr, d.lastname_vchr 
									 FROM t_opr_lis_sample a,(" + strResult_SQL + @") b, t_bse_deptdesc c, t_bse_employee d
                                    WHERE a.status_int = 6
									  AND a.MODIFY_DAT BETWEEN TO_DATE('" + p_strDatFrom + @"','yyyy-mm-dd hh24:mi:ss') 
											               AND TO_DATE('" + p_strDatTo + @"','yyyy-mm-dd hh24:mi:ss')";
            #region �Ա�
            if (p_strSex != null && p_strSex != "")
            {
                strHead_SQL += @" AND SEX_CHR = '" + p_strSex + @"'";
            }
            #endregion
            #region ����
            if ((p_strAgeFrom != null && p_strAgeFrom != "") && (p_strAgeTo != null && p_strAgeTo != ""))
            {
                string[] strAgeFromArr = p_strAgeFrom.Trim().Split(new char[] { ' ' });
                string[] strAgeToArr = p_strAgeTo.Trim().Split(new char[] { ' ' });
                int intAgeFromIdx = m_intReturnAgeIdx(strAgeFromArr[1]);
                int intAgeToIdx = m_intReturnAgeIdx(strAgeToArr[1]);
                strHead_SQL += " AND TRIM(SUBSTR(TRIM(a.age_chr),INSTR(TRIM(a.age_chr),' '))) in (" + m_strReturnAgeUnit(intAgeFromIdx, intAgeToIdx) + ")";
                strHead_SQL += " AND TRIM(SUBSTR(TRIM(a.age_chr),0,INSTR(TRIM(a.age_chr),' '))) > " + strAgeFromArr[0];
                strHead_SQL += " AND TRIM(SUBSTR(TRIM(a.age_chr),0,INSTR(TRIM(a.age_chr),' '))) < " + strAgeToArr[0];
            }
            else if (p_strAgeFrom != null && p_strAgeFrom != "")
            {
                string[] strAgeFromArr = p_strAgeFrom.Trim().Split(new char[] { ' ' });
                int intAgeFromIdx = m_intReturnAgeIdx(strAgeFromArr[1]);
                strHead_SQL += " AND TRIM(SUBSTR(TRIM(a.age_chr),INSTR(TRIM(a.age_chr),' '))) in (" + m_strReturnAgeUnit(intAgeFromIdx, 3) + ")";
                strHead_SQL += " AND TRIM(SUBSTR(TRIM(a.age_chr),0,INSTR(TRIM(a.age_chr),' '))) > " + strAgeFromArr[0];
            }
            else if (p_strAgeTo != null && p_strAgeTo != "")
            {
                string[] strAgeToArr = p_strAgeTo.Trim().Split(new char[] { ' ' });
                int intAgeToIdx = m_intReturnAgeIdx(strAgeToArr[1]);
                strHead_SQL += " AND TRIM(SUBSTR(TRIM(a.age_chr),INSTR(TRIM(a.age_chr),' '))) in (" + m_strReturnAgeUnit(0, intAgeToIdx) + ")";
                strHead_SQL += " AND TRIM(SUBSTR(TRIM(a.age_chr),0,INSTR(TRIM(a.age_chr),' '))) < " + strAgeToArr[0];
            }
            #endregion
            strHead_SQL += " AND a.sample_id_chr = b.sample_id_chr";
            strHead_SQL += " and a.appl_deptid_chr = c.deptid_chr(+)";
            strHead_SQL += " and a.appl_empid_chr = d.empid_chr(+)";
            #endregion
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strHead_SQL, ref dtbHead);
                if (lngRes > 0)
                {
                    lngRes = 0;
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strDetail_SQL, ref dtbDetail);
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

        #region �������䵥λ��Χ
        private string m_strReturnAgeUnit(int p_intFromIdx, int p_intToIdx)
        {
            string str = "";
            for (int i = p_intFromIdx; i <= p_intToIdx; i++)
            {
                if (i == 0)
                {
                    str += "'Сʱ'";
                }
                if (i == 1)
                {
                    str += "'��'";
                }
                if (i == 2)
                {
                    str += "'��'";
                }
                if (i == 3)
                {
                    str += "'��'";
                }
                if (i != p_intToIdx)
                {
                    str += ",";
                }
            }
            return str;
        }
        #endregion

        #region �滻���䵥λ
        private int m_intReturnAgeIdx(string p_str)
        {
            int intAgeIdx = 0;
            if (p_str == "Сʱ")
            {
                intAgeIdx = 0;
            }
            if (p_str == "��")
            {
                intAgeIdx = 1;
            }
            if (p_str == "��")
            {
                intAgeIdx = 2;
            }
            if (p_str == "��")
            {
                intAgeIdx = 3;
            }
            return intAgeIdx;
        }
        #endregion
        #endregion

        #region ������ģ��

        #region ��ѯ���еĹ�������Ϣ
        [AutoComplete]
        public long m_lngGetAllWorkGroupInfo( out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null; 

            string strSQL = @"SELECT * FROM T_BSE_LIS_WORK_GROUP WHERE status_int > 0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ���еĹ�������Ϣ
        [AutoComplete]
        public long m_lngGetAllWorkGroupInfo( out clsLisWorkGroup_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"SELECT * FROM T_BSE_LIS_WORK_GROUP WHERE status_int > 0";
            try
            {
                DataTable dtbResult = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisWorkGroup_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsLisWorkGroup_VO();
                        p_objResultArr[i1].m_strWORK_GROUP_ID_CHR = dtbResult.Rows[i1]["WORK_GROUP_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWORK_GROUP_NO_CHR = dtbResult.Rows[i1]["WORK_GROUP_NO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWORK_GROUP_NAME_VCHR = dtbResult.Rows[i1]["WORK_GROUP_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPRINT_NAME_VCHR = dtbResult.Rows[i1]["PRINT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPY_CODE_CHR = dtbResult.Rows[i1]["PY_CODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strASSIST_CODE01_CHR = dtbResult.Rows[i1]["ASSIST_CODE01_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWB_CODE_CHR = dtbResult.Rows[i1]["WB_CODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strASSIST_CODE02_CHR = dtbResult.Rows[i1]["ASSIST_CODE02_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strSUMMARY_VCHR = dtbResult.Rows[i1]["SUMMARY_VCHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #endregion

        #region ͳ����ģ��
        #region ��ȡ���е�ͳ������Ϣ
        [AutoComplete]
        public long m_lngGetAllStatGroupInfo( out clsLisStatGroup_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"SELECT * FROM T_BSE_LIS_STAT_GROUP WHERE STATUS_INT > 0 ORDER BY STAT_GROUP_ID_CHR";
            try
            {
                DataTable dtbResult = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisStatGroup_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsLisStatGroup_VO();
                        p_objResultArr[i1].m_strSTAT_GROUP_ID_CHR = dtbResult.Rows[i1]["STAT_GROUP_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTAT_GROUP_NAME_VCHR = dtbResult.Rows[i1]["STAT_GROUP_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPRINT_NAME_VCHR = dtbResult.Rows[i1]["PRINT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_fltWORK_COEFFICIENT_NUM = float.Parse(dtbResult.Rows[i1]["WORK_COEFFICIENT_NUM"].ToString().Trim());
                        p_objResultArr[i1].m_strPY_CODE_CHR = dtbResult.Rows[i1]["PY_CODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strASSIST_CODE01_CHR = dtbResult.Rows[i1]["ASSIST_CODE01_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWB_CODE_CHR = dtbResult.Rows[i1]["WB_CODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strASSIST_CODE02_CHR = dtbResult.Rows[i1]["ASSIST_CODE02_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strSUMMARY_VCHR = dtbResult.Rows[i1]["SUMMARY_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWORK_GROUP_ID_CHR = dtbResult.Rows[i1]["WORK_GROUP_ID_CHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���е�ͳ�������뵥Ԫ��Ϣ
        [AutoComplete]
        public long m_lngGetAllStatGroupUnitInfo( out clsLisStatGroupUnit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"SELECT b.*
							    FROM t_bse_lis_stat_group a,
									 t_bse_lis_stat_group_unit b
							   WHERE a.stat_group_id_chr = b.stat_group_id_chr 
								 AND a.status_int > 0
							ORDER BY apply_unit_id_chr";
            try
            {
                DataTable dtbResult = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsLisStatGroupUnit_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsLisStatGroupUnit_VO();
                        p_objResultArr[i1].m_strSTAT_GROUP_ID_CHR = dtbResult.Rows[i1]["STAT_GROUP_ID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAPPLY_UNIT_ID_CHR = dtbResult.Rows[i1]["APPLY_UNIT_ID_CHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ͳ����ID��ȡ�����µ����뵥Ԫ��Ϣ
        [AutoComplete]
        public long m_lngGetApplUnitByStatGroupID( string p_strStatGroupID,
            out clsApplUnit_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = null; 

            string strSQL = @"SELECT b.*
								FROM t_bse_lis_stat_group_unit a, 
									 t_aid_lis_apply_unit b
							   WHERE a.apply_unit_id_chr = b.apply_unit_id_chr 
								 AND a.stat_group_id_chr = '" + p_strStatGroupID + @"'
							ORDER BY a.apply_unit_id_chr";
            try
            {
                DataTable dtbResult = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsApplUnit_VO[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objResultArr[i] = new clsApplUnit_VO();
                        p_objResultArr[i].strApplUnitID = dtbResult.Rows[i]["APPLY_UNIT_ID_CHR"].ToString().Trim();
                        p_objResultArr[i].strApplUnitName = dtbResult.Rows[i]["APPLY_UNIT_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i].strOtherName = dtbResult.Rows[i]["OTHER_NAME_VCHR"].ToString().Trim();
                        p_objResultArr[i].strPYCode = dtbResult.Rows[i]["PY_CODE_CHR"].ToString().Trim();
                        p_objResultArr[i].strWBCode = dtbResult.Rows[i]["WB_CODE_CHR"].ToString().Trim();
                        p_objResultArr[i].strAssistCode01 = dtbResult.Rows[i]["ASSIST_CODE01_CHR"].ToString().Trim();
                        p_objResultArr[i].strAssistCode02 = dtbResult.Rows[i]["ASSIST_CODE02_CHR"].ToString().Trim();
                        p_objResultArr[i].strCheckCategoryID = dtbResult.Rows[i]["CHECK_CATEGORY_ID_CHR"].ToString().Trim();
                        p_objResultArr[i].strIsNoFoodRequired = dtbResult.Rows[i]["IS_NO_FOOD_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i].strIsPhysicsExamRequired = dtbResult.Rows[i]["IS_PHYSICAL_EXAM_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i].strIsReservationRequired = dtbResult.Rows[i]["IS_RESERVATION_REQUIRED_CHR"].ToString().Trim();
                        p_objResultArr[i].strPrice = dtbResult.Rows[i]["PRICE_NUM"].ToString().Trim();
                        p_objResultArr[i].strCost = dtbResult.Rows[i]["COST_NUM"].ToString().Trim();
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

        #endregion

        #region ������ͳ�ƻ���
        [AutoComplete]
        public long m_lngGetStatTotalReport( string p_strDatFrom, string p_strDatTo, string p_strOprID,
            out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null; 

            #region SQL
            //xing.chen modify for ��Ӳ�����Աid��ѯ����
            string strSQL = "";
            if (p_strOprID != null && p_strOprID.Trim() != "")
            {
                strSQL = @"SELECT n.total_work_count,
       m.work_group_name_vchr,
       m.print_name_vchr work_group_print_title,
       m.work_group_id_chr,
       n.total_raw_work_count
  FROM t_bse_lis_work_group m,
       (SELECT SUM((l.total_stat_group_count * j.work_coefficient_num)) total_work_count,
               k.work_group_id_chr,
               SUM(total_stat_group_count) total_raw_work_count
          FROM t_bse_lis_stat_group j,
               t_bse_lis_work_group k,
               (SELECT COUNT(i.stat_group_id_chr) total_stat_group_count,
                       i.stat_group_id_chr
                  FROM (SELECT DISTINCT d.application_id_chr,
                                        f.stat_group_id_chr
                          FROM t_opr_lis_app_apply_unit d,
                               t_aid_lis_sample_group_unit e,
                               t_bse_lis_stat_group_unit f,
                               t_bse_lis_stat_group g,
                               (SELECT a.application_id_chr,
                                       c.sample_group_id_chr
                                  FROM t_opr_lis_application a,
                                       (select DISTINCT report_group_id_chr,
                                                        application_id_chr
                                          from t_opr_lis_app_report
                                         where confirm_dat BETWEEN
                                               TO_DATE('" + p_strDatFrom + @"',
                                                       'yyyy-mm-dd hh24:mi:ss') AND
                                               TO_DATE('" + p_strDatTo + @"',
                                                       'yyyy-mm-dd hh24:mi:ss')
                                           and status_int = 2) b,
                                       t_opr_lis_app_sample c
                                 WHERE a.application_id_chr =
                                       b.application_id_chr
                                   AND b.report_group_id_chr =
                                       c.report_group_id_chr
                                   AND B.application_id_chr =
                                       c.application_id_chr
                                   AND a.pstatus_int > 0
								   AND a.operator_id_chr='" + p_strOprID + @"') h		
                         WHERE h.sample_group_id_chr = e.sample_group_id_chr
                           AND d.apply_unit_id_chr = e.apply_unit_id_chr
                           AND d.application_id_chr = h.application_id_chr
                           AND f.apply_unit_id_chr = d.apply_unit_id_chr
                           AND f.stat_group_id_chr = g.stat_group_id_chr
                           AND g.status_int > 0) i
                 GROUP BY i.stat_group_id_chr) l
         WHERE j.stat_group_id_chr = l.stat_group_id_chr
           AND j.work_group_id_chr = k.work_group_id_chr
           AND j.status_int > 0
           AND k.status_int > 0
         GROUP BY k.work_group_id_chr) n
 WHERE m.work_group_id_chr = n.work_group_id_chr
   AND m.status_int > 0";
            }
            else
            {
                strSQL = @"SELECT n.total_work_count,
       m.work_group_name_vchr,
       m.print_name_vchr work_group_print_title,
       m.work_group_id_chr,
       n.total_raw_work_count
  FROM t_bse_lis_work_group m,
       (SELECT SUM((l.total_stat_group_count * j.work_coefficient_num)) total_work_count,
               k.work_group_id_chr,
               SUM(total_stat_group_count) total_raw_work_count
          FROM t_bse_lis_stat_group j,
               t_bse_lis_work_group k,
               (SELECT COUNT(i.stat_group_id_chr) total_stat_group_count,
                       i.stat_group_id_chr
                  FROM (SELECT DISTINCT d.application_id_chr,
                                        f.stat_group_id_chr
                          FROM t_opr_lis_app_apply_unit d,
                               t_aid_lis_sample_group_unit e,
                               t_bse_lis_stat_group_unit f,
                               t_bse_lis_stat_group g,
                               (SELECT a.application_id_chr,
                                       c.sample_group_id_chr
                                  FROM t_opr_lis_application a,
                                       (select DISTINCT report_group_id_chr,
                                                        application_id_chr
                                          from t_opr_lis_app_report
                                         where confirm_dat BETWEEN
                                               TO_DATE('" + p_strDatFrom + @"',
                                                       'yyyy-mm-dd hh24:mi:ss') AND
                                               TO_DATE('" + p_strDatTo + @"',
                                                       'yyyy-mm-dd hh24:mi:ss')
                                           and status_int = 2) b,
                                       t_opr_lis_app_sample c
                                 WHERE a.application_id_chr =
                                       b.application_id_chr
                                   AND b.report_group_id_chr =
                                       c.report_group_id_chr
                                   AND B.application_id_chr =
                                       c.application_id_chr
                                   AND a.pstatus_int > 0) h		
                         WHERE h.sample_group_id_chr = e.sample_group_id_chr
                           AND d.apply_unit_id_chr = e.apply_unit_id_chr
                           AND d.application_id_chr = h.application_id_chr
                           AND f.apply_unit_id_chr = d.apply_unit_id_chr
                           AND f.stat_group_id_chr = g.stat_group_id_chr
                           AND g.status_int > 0) i
                 GROUP BY i.stat_group_id_chr) l
         WHERE j.stat_group_id_chr = l.stat_group_id_chr
           AND j.work_group_id_chr = k.work_group_id_chr
           AND j.status_int > 0
           AND k.status_int > 0
         GROUP BY k.work_group_id_chr) n
 WHERE m.work_group_id_chr = n.work_group_id_chr
   AND m.status_int > 0";
            }

            #endregion

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ������ͳ����ϸ
        [AutoComplete]
        public long m_lngGetStatDetailReport( string p_strDatFrom, string p_strDatTo, string p_strOprID,
            out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null; 

            #region SQL
           
            string strSQL = "";
            if (p_strOprID != null && p_strOprID.Trim() != "")
            {
                strSQL = @"SELECT l.total_stat_group_count,
       (l.total_stat_group_count * j.work_coefficient_num) total_work_count,
       j.stat_group_id_chr,
       j.stat_group_name_vchr,
       j.print_name_vchr stat_group_print_title,
       k.work_group_id_chr,
       k.work_group_name_vchr,
       k.print_name_vchr work_group_print_title
  FROM t_bse_lis_stat_group j,
       t_bse_lis_work_group k,
       (SELECT COUNT(i.stat_group_id_chr) total_stat_group_count,
               i.stat_group_id_chr
          FROM (SELECT DISTINCT d.application_id_chr, f.stat_group_id_chr
                  FROM t_opr_lis_app_apply_unit d,
                       t_aid_lis_sample_group_unit e,
                       t_bse_lis_stat_group_unit f,
                       t_bse_lis_stat_group g,
                       (SELECT a.application_id_chr, c.sample_group_id_chr
                          FROM t_opr_lis_application a,
                               (select DISTINCT report_group_id_chr,
                                                application_id_chr
                                  from t_opr_lis_app_report
                                 where confirm_dat BETWEEN
                                       TO_DATE('" + p_strDatFrom + @"',
                                               'yyyy-mm-dd hh24:mi:ss') AND
                                       TO_DATE('" + p_strDatTo + @"',
                                               'yyyy-mm-dd hh24:mi:ss')
                                   and status_int = 2) b,
                               t_opr_lis_app_sample c
                         WHERE a.application_id_chr = b.application_id_chr
                           AND b.report_group_id_chr = c.report_group_id_chr
                           AND B.application_id_chr = c.application_id_chr
                           AND a.pstatus_int > 0
						   AND a.operator_id_chr='" + p_strOprID + @"') h
                 WHERE h.sample_group_id_chr = e.sample_group_id_chr
                   AND d.apply_unit_id_chr = e.apply_unit_id_chr
                   AND d.application_id_chr = h.application_id_chr
                   AND f.apply_unit_id_chr = d.apply_unit_id_chr
                   AND f.stat_group_id_chr = g.stat_group_id_chr
                   AND g.status_int > 0) i
         GROUP BY i.stat_group_id_chr) l
 WHERE j.stat_group_id_chr = l.stat_group_id_chr
   AND j.work_group_id_chr = k.work_group_id_chr
   AND j.status_int > 0
   AND k.status_int > 0";
            }
            else
            {
                strSQL = @"SELECT l.total_stat_group_count,
       (l.total_stat_group_count * j.work_coefficient_num) total_work_count,
       j.stat_group_id_chr,
       j.stat_group_name_vchr,
       j.print_name_vchr stat_group_print_title,
       k.work_group_id_chr,
       k.work_group_name_vchr,
       k.print_name_vchr work_group_print_title
  FROM t_bse_lis_stat_group j,
       t_bse_lis_work_group k,
       (SELECT COUNT(i.stat_group_id_chr) total_stat_group_count,
               i.stat_group_id_chr
          FROM (SELECT DISTINCT d.application_id_chr, f.stat_group_id_chr
                  FROM t_opr_lis_app_apply_unit d,
                       t_aid_lis_sample_group_unit e,
                       t_bse_lis_stat_group_unit f,
                       t_bse_lis_stat_group g,
                       (SELECT a.application_id_chr, c.sample_group_id_chr
                          FROM t_opr_lis_application a,
                               (select DISTINCT report_group_id_chr,
                                                application_id_chr
                                  from t_opr_lis_app_report
                                 where confirm_dat BETWEEN
                                       TO_DATE('" + p_strDatFrom + @"',
                                               'yyyy-mm-dd hh24:mi:ss') AND
                                       TO_DATE('" + p_strDatTo + @"',
                                               'yyyy-mm-dd hh24:mi:ss')
                                   and status_int = 2) b,
                               t_opr_lis_app_sample c
                         WHERE a.application_id_chr = b.application_id_chr
                           AND b.report_group_id_chr = c.report_group_id_chr
                           AND B.application_id_chr = c.application_id_chr
                           AND a.pstatus_int > 0) h
                 WHERE h.sample_group_id_chr = e.sample_group_id_chr
                   AND d.apply_unit_id_chr = e.apply_unit_id_chr
                   AND d.application_id_chr = h.application_id_chr
                   AND f.apply_unit_id_chr = d.apply_unit_id_chr
                   AND f.stat_group_id_chr = g.stat_group_id_chr
                   AND g.status_int > 0) i
         GROUP BY i.stat_group_id_chr) l
 WHERE j.stat_group_id_chr = l.stat_group_id_chr
   AND j.work_group_id_chr = k.work_group_id_chr
   AND j.status_int > 0
   AND k.status_int > 0";
            }

            #endregion

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ������û���ͳ��
        [AutoComplete]
        public long m_lngGetCheckPriceTotalReport( string p_strDatFrom, string p_strDatTo, string p_strOprID,
            out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null; 

            #region SQL
          
            string strSQL = "";
            if (p_strOprID != null && p_strOprID.Trim() != "")
            {
                strSQL = @"SELECT m.work_group_id_chr,
       m.work_group_name_vchr,
       m.print_name_vchr work_group_print_title,
       n.total_price_count,
       n.total_cost_count,
       n.total_work_apply_count
  FROM t_bse_lis_work_group m,
       (SELECT SUM(l.total_apply_unit_count * j.cost_num) total_cost_count,
               SUM(l.total_apply_unit_count * j.price_num) total_price_count,
               l.work_group_id_chr,
               SUM(l.total_apply_unit_count) total_work_apply_count
          FROM t_aid_lis_apply_unit j,
               t_bse_lis_work_group k,
               (SELECT i.work_group_id_chr,
                       COUNT(d.apply_unit_id_chr) total_apply_unit_count,
                       d.apply_unit_id_chr
                  FROM t_opr_lis_app_apply_unit d,
                       t_aid_lis_sample_group_unit e,
                       t_bse_lis_stat_group_unit f,
                       t_bse_lis_stat_group g,
                       t_bse_lis_work_group i,
                       (SELECT a.application_id_chr, c.sample_group_id_chr
                          FROM t_opr_lis_application a,
                               (select DISTINCT report_group_id_chr,
                                                application_id_chr
                                  from t_opr_lis_app_report
                                 where confirm_dat BETWEEN
                                       TO_DATE('" + p_strDatFrom + @"',
                                               'yyyy-mm-dd hh24:mi:ss') AND
                                       TO_DATE('" + p_strDatTo + @"',
                                               'yyyy-mm-dd hh24:mi:ss')
                                   and status_int = 2) b,
                               t_opr_lis_app_sample c
                         WHERE a.application_id_chr = b.application_id_chr
                           AND b.report_group_id_chr = c.report_group_id_chr
                           AND b.application_id_chr = c.application_id_chr
                           AND a.pstatus_int > 0
						   AND a.operator_id_chr='" + p_strOprID + @"') h
                 WHERE h.sample_group_id_chr = e.sample_group_id_chr
                   AND d.apply_unit_id_chr = e.apply_unit_id_chr
                   AND d.application_id_chr = h.application_id_chr
                   AND f.apply_unit_id_chr = d.apply_unit_id_chr
                   AND f.stat_group_id_chr = g.stat_group_id_chr
                   AND g.work_group_id_chr = i.work_group_id_chr
                   AND i.status_int > 0
                   AND g.status_int > 0
                 GROUP BY i.work_group_id_chr, d.apply_unit_id_chr) l
         WHERE j.apply_unit_id_chr = l.apply_unit_id_chr
           AND l.work_group_id_chr = k.work_group_id_chr
           AND k.status_int > 0
         GROUP BY l.work_group_id_chr) n
 WHERE m.work_group_id_chr = n.work_group_id_chr
   AND m.status_int > 0";
            }
            else
            {
                strSQL = @"SELECT m.work_group_id_chr,
       m.work_group_name_vchr,
       m.print_name_vchr work_group_print_title,
       n.total_price_count,
       n.total_cost_count,
       n.total_work_apply_count
  FROM t_bse_lis_work_group m,
       (SELECT SUM(l.total_apply_unit_count * j.cost_num) total_cost_count,
               SUM(l.total_apply_unit_count * j.price_num) total_price_count,
               l.work_group_id_chr,
               SUM(l.total_apply_unit_count) total_work_apply_count
          FROM t_aid_lis_apply_unit j,
               t_bse_lis_work_group k,
               (SELECT i.work_group_id_chr,
                       COUNT(d.apply_unit_id_chr) total_apply_unit_count,
                       d.apply_unit_id_chr
                  FROM t_opr_lis_app_apply_unit d,
                       t_aid_lis_sample_group_unit e,
                       t_bse_lis_stat_group_unit f,
                       t_bse_lis_stat_group g,
                       t_bse_lis_work_group i,
                       (SELECT a.application_id_chr, c.sample_group_id_chr
                          FROM t_opr_lis_application a,
                               (select DISTINCT report_group_id_chr,
                                                application_id_chr
                                  from t_opr_lis_app_report
                                 where confirm_dat BETWEEN
                                       TO_DATE('" + p_strDatFrom + @"',
                                               'yyyy-mm-dd hh24:mi:ss') AND
                                       TO_DATE('" + p_strDatTo + @"',
                                               'yyyy-mm-dd hh24:mi:ss')
                                   and status_int = 2) b,
                               t_opr_lis_app_sample c
                         WHERE a.application_id_chr = b.application_id_chr
                           AND b.report_group_id_chr = c.report_group_id_chr
                           AND b.application_id_chr = c.application_id_chr
                           AND a.pstatus_int > 0) h
                 WHERE h.sample_group_id_chr = e.sample_group_id_chr
                   AND d.apply_unit_id_chr = e.apply_unit_id_chr
                   AND d.application_id_chr = h.application_id_chr
                   AND f.apply_unit_id_chr = d.apply_unit_id_chr
                   AND f.stat_group_id_chr = g.stat_group_id_chr
                   AND g.work_group_id_chr = i.work_group_id_chr
                   AND i.status_int > 0
                   AND g.status_int > 0
                 GROUP BY i.work_group_id_chr, d.apply_unit_id_chr) l
         WHERE j.apply_unit_id_chr = l.apply_unit_id_chr
           AND l.work_group_id_chr = k.work_group_id_chr
           AND k.status_int > 0
         GROUP BY l.work_group_id_chr) n
 WHERE m.work_group_id_chr = n.work_group_id_chr
   AND m.status_int > 0";
            }

            #endregion

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region �������ϸͳ��
        [AutoComplete]
        public long m_lngGetCheckPriceDetailReport( string p_strDatFrom, string p_strDatTo, string p_strOprID,
            out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null; 

            #region SQL
            string strSQL = "";
            if (p_strOprID != null && p_strOprID.Trim() != "")
            {
                strSQL = @"SELECT i.stat_group_id_chr,
       i.stat_group_name_vchr,
       i.print_name_vchr stat_group_print_title,
       j.work_group_id_chr,
       j.work_group_name_vchr,
       j.print_name_vchr work_group_print_title,
       k.cost_num,
       k.price_num,
       l.total_apply_unit_count,
       (k.cost_num * l.total_apply_unit_count) total_cost_count,
       (k.price_num * l.total_apply_unit_count) total_price_count,
       k.apply_unit_name_vchr,
       k.apply_unit_id_chr
  FROM t_bse_lis_stat_group i,
       t_bse_lis_work_group j,
       t_aid_lis_apply_unit k,
       (SELECT f.stat_group_id_chr,
               COUNT(d.apply_unit_id_chr) total_apply_unit_count,
               d.apply_unit_id_chr
          FROM t_opr_lis_app_apply_unit d,
               t_aid_lis_sample_group_unit e,
               t_bse_lis_stat_group_unit f,
               t_bse_lis_stat_group g,
               (SELECT a.application_id_chr, c.sample_group_id_chr
                  FROM t_opr_lis_application a,
                       (select distinct application_id_chr,
                                        report_group_id_chr
                          from t_opr_lis_app_report
                         where status_int = 2
                           and CONFIRM_DAT Between
                               TO_DATE('" + p_strDatFrom + @"',
                                       'yyyy-mm-dd hh24:mi:ss') and
                               TO_DATE('" + p_strDatTo + @"',
                                       'yyyy-mm-dd hh24:mi:ss')) b,
                       t_opr_lis_app_sample c
                 WHERE a.application_id_chr = b.application_id_chr
                   AND b.report_group_id_chr = c.report_group_id_chr
                   AND b.application_id_chr = c.application_id_chr
                   AND a.pstatus_int > 0
				   AND a.operator_id_chr='" + p_strOprID + @"') h
         WHERE h.sample_group_id_chr = e.sample_group_id_chr
           AND d.apply_unit_id_chr = e.apply_unit_id_chr
           AND d.application_id_chr = h.application_id_chr
           AND f.apply_unit_id_chr = d.apply_unit_id_chr
           AND f.stat_group_id_chr = g.stat_group_id_chr
           AND g.status_int > 0
         GROUP BY f.stat_group_id_chr, d.apply_unit_id_chr) l
 WHERE i.stat_group_id_chr = l.stat_group_id_chr
   AND i.work_group_id_chr = j.work_group_id_chr
   AND k.apply_unit_id_chr = l.apply_unit_id_chr
   AND i.status_int > 0
   AND j.status_int > 0";
            }
            else
            {
                strSQL = @"SELECT i.stat_group_id_chr,
       i.stat_group_name_vchr,
       i.print_name_vchr stat_group_print_title,
       j.work_group_id_chr,
       j.work_group_name_vchr,
       j.print_name_vchr work_group_print_title,
       k.cost_num,
       k.price_num,
       l.total_apply_unit_count,
       (k.cost_num * l.total_apply_unit_count) total_cost_count,
       (k.price_num * l.total_apply_unit_count) total_price_count,
       k.apply_unit_name_vchr,
       k.apply_unit_id_chr
  FROM t_bse_lis_stat_group i,
       t_bse_lis_work_group j,
       t_aid_lis_apply_unit k,
       (SELECT f.stat_group_id_chr,
               COUNT(d.apply_unit_id_chr) total_apply_unit_count,
               d.apply_unit_id_chr
          FROM t_opr_lis_app_apply_unit d,
               t_aid_lis_sample_group_unit e,
               t_bse_lis_stat_group_unit f,
               t_bse_lis_stat_group g,
               (SELECT a.application_id_chr, c.sample_group_id_chr
                  FROM t_opr_lis_application a,
                       (select distinct application_id_chr,
                                        report_group_id_chr
                          from t_opr_lis_app_report
                         where status_int = 2
                           and CONFIRM_DAT Between
                               TO_DATE('" + p_strDatFrom + @"',
                                       'yyyy-mm-dd hh24:mi:ss') and
                               TO_DATE('" + p_strDatTo + @"',
                                       'yyyy-mm-dd hh24:mi:ss')) b,
                       t_opr_lis_app_sample c
                 WHERE a.application_id_chr = b.application_id_chr
                   AND b.report_group_id_chr = c.report_group_id_chr
                   AND b.application_id_chr = c.application_id_chr
                   AND a.pstatus_int > 0) h
         WHERE h.sample_group_id_chr = e.sample_group_id_chr
           AND d.apply_unit_id_chr = e.apply_unit_id_chr
           AND d.application_id_chr = h.application_id_chr
           AND f.apply_unit_id_chr = d.apply_unit_id_chr
           AND f.stat_group_id_chr = g.stat_group_id_chr
           AND g.status_int > 0
         GROUP BY f.stat_group_id_chr, d.apply_unit_id_chr) l
 WHERE i.stat_group_id_chr = l.stat_group_id_chr
   AND i.work_group_id_chr = j.work_group_id_chr
   AND k.apply_unit_id_chr = l.apply_unit_id_chr
   AND i.status_int > 0
   AND j.status_int > 0";
            }

            #endregion

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����-�����걾�ͼ���
        /// <summary>
        /// �����걾�ͼ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSamplesCheckTotal( out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {
            #region ע��
            //            string strSQL = @"SELECT   dept.deptname_vchr AS deptname, APPLY.sample_type_vchr AS sampletype,
            //                                 COUNT (*) AS sampletypenum
            //                            FROM t_opr_lis_application APPLY JOIN t_bse_deptdesc dept ON APPLY.appl_deptid_chr =
            //                                                                                           dept.deptid_chr
            //                                 JOIN t_opr_lis_sample samples ON APPLY.application_id_chr =
            //                                                                            samples.application_id_chr
            //                           WHERE status_int = 6
            //                             AND application_dat >= TO_DATE ('{0}','yyyy-mm-dd hh24:mi:ss')
            //                             AND application_dat <= TO_DATE ('{1}','yyyy-mm-dd hh24:mi:ss')
            //                        GROUP BY dept.deptname_vchr, APPLY.sample_type_vchr
            //                        ORDER BY dept.deptname_vchr  ";
            #endregion

            // unit.assist_code02_chr = 'Germ'  --��ʾ��ϸ��������ҩ��
            string strSQL = @"SELECT   dept.deptname_vchr AS deptname, APPLY.sample_type_vchr AS sampletype,
                                 COUNT (APPLY.sample_type_vchr) AS sampletypenum
                                FROM t_opr_lis_application APPLY,
                                     t_bse_deptdesc dept,
                                     t_opr_lis_sample samples,
                                     t_opr_lis_app_apply_unit unitapply,
                                     t_aid_lis_apply_unit unit
                               WHERE APPLY.appl_deptid_chr = dept.deptid_chr
                                 AND APPLY.application_id_chr = samples.application_id_chr
                                 AND APPLY.application_id_chr = unitapply.application_id_chr
                                 AND unitapply.apply_unit_id_chr = unit.apply_unit_id_chr
                                 AND unit.assist_code02_chr = 'Germ'
                                 AND samples.status_int = 6
                                 AND application_dat >=
                                                    TO_DATE ('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                 AND application_dat <=
                                                    TO_DATE ('{1}', 'yyyy-mm-dd hh24:mi:ss')
                            GROUP BY dept.deptname_vchr, APPLY.sample_type_vchr
                            ORDER BY dept.deptname_vchr
                        ";
            strSQL = string.Format(strSQL, strDateFrom, strDateTo);
            return m_lngGetDataTableBySql( out p_dtbResult, strSQL);
        }
        #endregion

        #region ����-ϸ��������ͳ��
        /// <summary>
        /// �����걾�ͼ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGermOccurRate( out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {
            #region ע��
            //            string strSQL = @"SELECT   result_vchr germname, COUNT (*) germnum,
            //                             to_number(substr(to_char( COUNT (*)
            //                             / (SELECT COUNT (*) total
            //                                  FROM t_opr_lis_check_result
            //                                 WHERE sample_id_chr IN (SELECT sample_id_chr
            //                                                           FROM t_opr_lis_check_result
            //                                                          WHERE check_item_name_vchr = '�Ƿ��о�')
            //                                   AND check_item_name_vchr = '�������1'
            //                                   AND modify_dat >
            //                                           TO_DATE ('{0}', 'yyyy-mm-dd hh24:mi:ss')
            //                                   AND modify_dat <
            //                                           TO_DATE ('{1}', 'yyyy-mm-dd hh24:mi:ss'))),0,3))
            //                                                                                          AS RATE
            //                            FROM t_opr_lis_check_result
            //                           WHERE sample_id_chr IN (SELECT sample_id_chr
            //                                                     FROM t_opr_lis_check_result
            //                                                    WHERE check_item_name_vchr = '�Ƿ��о�')
            //                         AND check_item_name_vchr = '�������1'
            //                         AND modify_dat > TO_DATE ('{0}', 'yyyy-mm-dd hh24:mi:ss')
            //                         AND modify_dat < TO_DATE ('{1}', 'yyyy-mm-dd hh24:mi:ss')
            //                            GROUP BY result_vchr
            //                        "; 
            #endregion

            #region Sql���
            string strSQL = @"
                                SELECT   result_vchr GermName, COUNT (*) germnum,
                                 TO_NUMBER
                                    (SUBSTR
                                        (TO_CHAR
                                            (  COUNT (*)
                                             / (SELECT COUNT (*) germnum
                                                  FROM t_opr_lis_check_result RESULT, t_opr_lis_sample samples
                           WHERE check_item_id_chr =
                                                   (SELECT item.check_item_id_chr as check_item_id_chr
                                                  FROM t_bse_lis_check_item item, t_aid_lis_apply_unit unit,T_AID_LIS_APPLY_UNIT_DETAIL detail
                                                 WHERE item.assist_code02_chr = 'GermR'
                                                   AND unit.assist_code02_chr = 'Germ'
                                                   AND item.check_item_id_chr = detail.check_item_id_chr
                                                   and detail.apply_unit_id_chr = unit.apply_unit_id_chr  AND ROWNUM = 1)
                             AND RESULT.sample_id_chr IN (
                                    SELECT sample_id_chr
                                      FROM t_opr_lis_check_result
                                     WHERE check_item_id_chr =
                                                    (SELECT item.check_item_id_chr as check_item_id_chr
                                                  FROM t_bse_lis_check_item item, t_aid_lis_apply_unit unit,T_AID_LIS_APPLY_UNIT_DETAIL detail
                                                 WHERE item.assist_code02_chr = 'Germ'
                                                   AND unit.assist_code02_chr = 'Germ'
                                                   AND item.check_item_id_chr = detail.check_item_id_chr
                                                   and detail.apply_unit_id_chr = unit.apply_unit_id_chr  AND ROWNUM = 1)
                                       AND result_vchr = '��')
                             AND RESULT.sample_id_chr = samples.sample_id_chr
                             AND RESULT.status_int = 1
                             AND samples.status_int = 6
                             AND RESULT.modify_dat > TO_DATE ('{0}', 'yyyy-mm-dd hh24:mi:ss')
                             AND RESULT.modify_dat < TO_DATE ('{1}', 'yyyy-mm-dd hh24:mi:ss'))
                                            ),
                                         0,
                                         5
                                        )
                                    ) AS rate
                            FROM t_opr_lis_check_result RESULT, t_opr_lis_sample samples
                           WHERE check_item_id_chr =
                                                   (SELECT item.check_item_id_chr as check_item_id_chr
                                                  FROM t_bse_lis_check_item item, t_aid_lis_apply_unit unit,T_AID_LIS_APPLY_UNIT_DETAIL detail
                                                 WHERE item.assist_code02_chr = 'GermR'
                                                   AND unit.assist_code02_chr = 'Germ'
                                                   AND item.check_item_id_chr = detail.check_item_id_chr
                                                   and detail.apply_unit_id_chr = unit.apply_unit_id_chr  AND ROWNUM = 1)
                             AND RESULT.sample_id_chr IN (
                                    SELECT sample_id_chr
                                      FROM t_opr_lis_check_result
                                     WHERE check_item_id_chr =
                                                    (SELECT item.check_item_id_chr as check_item_id_chr
                                                  FROM t_bse_lis_check_item item, t_aid_lis_apply_unit unit,T_AID_LIS_APPLY_UNIT_DETAIL detail
                                                 WHERE item.assist_code02_chr = 'Germ'
                                                   AND unit.assist_code02_chr = 'Germ'
                                                   AND item.check_item_id_chr = detail.check_item_id_chr
                                                   and detail.apply_unit_id_chr = unit.apply_unit_id_chr  AND ROWNUM = 1)
                                       AND result_vchr = '��')
                             AND RESULT.sample_id_chr = samples.sample_id_chr
                             AND RESULT.status_int = 1
                             AND samples.status_int = 6
                             AND RESULT.modify_dat > TO_DATE ('{0}', 'yyyy-mm-dd hh24:mi:ss')
                             AND RESULT.modify_dat < TO_DATE ('{1}', 'yyyy-mm-dd hh24:mi:ss')
                        GROUP BY result_vchr
                            ";
            #endregion
            p_dtbResult = null;
            strSQL = string.Format(strSQL, strDateFrom, strDateTo);
            return m_lngGetDataTableBySql( out p_dtbResult, strSQL);
        }


        #endregion

        #region ����-ϸ���ֲ�����

        /// <summary>
        /// ϸ���ֲ����Ʊ���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult"></param>
        /// <param name="strDateFrom"></param>
        /// <param name="strDateTo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGermDistributeTrend( out DataTable p_dtbResult, string strDateFrom, string strDateTo)
        {

            string strSQL = @"SELECT   result_vchr germname, COUNT (*) germnum, RESULT.modify_dat AS yearmonth,
                                 NULL AS resulttype, NULL AS germtotal
                                        FROM t_opr_lis_check_result RESULT, t_opr_lis_sample samples
                                       WHERE check_item_id_chr =
                                                               (SELECT item.check_item_id_chr as check_item_id_chr
                                                  FROM t_bse_lis_check_item item, t_aid_lis_apply_unit unit,T_AID_LIS_APPLY_UNIT_DETAIL detail
                                                 WHERE item.assist_code02_chr = 'GermR'
                                                   AND unit.assist_code02_chr = 'Germ'
                                                   AND item.check_item_id_chr = detail.check_item_id_chr
                                                   and detail.apply_unit_id_chr = unit.apply_unit_id_chr  AND ROWNUM = 1)
                                         AND RESULT.sample_id_chr IN (
                                                SELECT sample_id_chr
                                                  FROM t_opr_lis_check_result
                                                 WHERE check_item_id_chr =
                                                                (SELECT item.check_item_id_chr as check_item_id_chr
                                                  FROM t_bse_lis_check_item item, t_aid_lis_apply_unit unit,T_AID_LIS_APPLY_UNIT_DETAIL detail
                                                 WHERE item.assist_code02_chr = 'Germ'
                                                   AND unit.assist_code02_chr = 'Germ'
                                                   AND item.check_item_id_chr = detail.check_item_id_chr
                                                   and detail.apply_unit_id_chr = unit.apply_unit_id_chr  AND ROWNUM = 1)
                                                   AND result_vchr = '��')
                                         AND RESULT.sample_id_chr = samples.sample_id_chr
                                         AND RESULT.status_int = 1
                                         AND samples.status_int = 6
                                         AND RESULT.modify_dat >
                                                           TO_DATE ('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                         AND RESULT.modify_dat <
                                                           TO_DATE ('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                    GROUP BY result_vchr, RESULT.modify_dat
                        ";

            p_dtbResult = null;
            strSQL = string.Format(strSQL, strDateFrom, strDateTo);

            long lngRes = m_lngGetDataTableBySql( out p_dtbResult, strSQL);

            System.Collections.ArrayList arlRows = new System.Collections.ArrayList();
            if (p_dtbResult != null)
            {
                System.Collections.Hashtable hasNum = new System.Collections.Hashtable();
                double total = 0.0f;
                //total:ͳ��ϸ��������
                foreach (DataRow row in p_dtbResult.Rows)
                {
                    string str = row["GermName"].ToString().Trim();
                    double num = double.Parse(row["GermNum"].ToString());

                    if (hasNum.ContainsKey(str))
                    {
                        hasNum[str] = Convert.ToDouble(hasNum[str]) + num;
                    }
                    else
                    {
                        hasNum.Add(str, row["GermNum"]);
                    }

                    total += num;
                }



                //���·�
                DateTime dtMin = Convert.ToDateTime(strDateFrom);
                DateTime dtMax = Convert.ToDateTime(strDateTo);


                //��� ϸ���ٷֱ� | ռȫ��ϸ���ٷֱ�%
                foreach (DataRow row in p_dtbResult.Rows)
                {
                    row["ResultType"] = "(��)";
                    string strName = row["GermName"].ToString().Trim();
                    double num = int.Parse(row["GermNum"].ToString());

                    DataRow dtr = p_dtbResult.NewRow();
                    dtr["GermName"] = row["GermName"];
                    dtr["YearMonth"] = row["YearMonth"];
                    dtr["ResultType"] = "ռ��ϸ���ٷֱ�(%)";
                    dtr["GermNum"] = num * 100 / Convert.ToDouble(hasNum[strName]);
                    dtr["GermTotal"] = num;
                    arlRows.Add(dtr);

                    dtr = p_dtbResult.NewRow();
                    dtr["GermName"] = row["GermName"];
                    dtr["YearMonth"] = row["YearMonth"];
                    dtr["ResultType"] = "ռȫ��ϸ���ٷֱ�(%)";
                    dtr["GermNum"] = num * 100 / total;


                    arlRows.Add(dtr);



                    for (DateTime dt = dtMin; dt <= dtMax; dt = dt.AddMonths(1))
                    {
                        bool isContainMonth = m_blnIsGermTrendContainMonthInfo(p_dtbResult, dt, row["GermName"].ToString());
                        if (!isContainMonth)
                        {
                            dtr = p_dtbResult.NewRow();
                            dtr["GermName"] = row["GermName"];
                            dtr["YearMonth"] = dt;
                            dtr["ResultType"] = "(��)";
                            dtr["GermNum"] = 0;

                            arlRows.Add(dtr);

                            dtr = p_dtbResult.NewRow();
                            dtr["GermName"] = row["GermName"];
                            dtr["YearMonth"] = dt;
                            dtr["ResultType"] = "ռ��ϸ���ٷֱ�(%)";
                            dtr["GermNum"] = 0;

                            arlRows.Add(dtr);

                            dtr = p_dtbResult.NewRow();
                            dtr["GermName"] = row["GermName"];
                            dtr["YearMonth"] = dt;
                            dtr["ResultType"] = "ռȫ��ϸ���ٷֱ�(%)";
                            dtr["GermNum"] = 0;

                            arlRows.Add(dtr);
                        }
                    }

                }


            }

            foreach (DataRow row in arlRows)
            {
                p_dtbResult.Rows.Add(row);
            }

            return lngRes;
        }

        /// <summary>
        /// �ж�ϸ�������Ƿ����ĳ���·ݵ�ͳ��
        /// </summary>
        /// <param name="p_GermTrend"></param>
        /// <param name="dt"></param>
        /// <param name="GermName"></param>
        /// <returns></returns>
        private bool m_blnIsGermTrendContainMonthInfo(DataTable p_GermTrend, DateTime dt, string GermName)
        {
            foreach (DataRow row in p_GermTrend.Rows)
            {
                if (row["GermName"].ToString() == GermName)
                {
                    DateTime dtRow = Convert.ToDateTime(row["YearMonth"]);
                    if (dtRow.Year == dt.Year && dtRow.Month == dt.Month)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region ����-΢��������ܱ�
        /// <summary>
        /// ΢���������
        /// </summary>
        /// <param name="p_objPrincipal">Ȩ����̽</param>
        /// <param name="p_dtbResult">����������DataTable</param>
        /// <param name="strDateFrom">��ʼ����</param>
        /// <param name="strDateTo">��ֹ����</param>
        /// <param name="listSamples">�걾����</param>
        /// <param name="listPatientArea">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAnimalculeCheckTotal( out DataTable p_dtbResult, string strDateFrom, string strDateTo, List<string> listSamples, List<string> listPatientArea)
        {
            #region Sql���
            string strSQL = @"
                         SELECT t1.sample_id_chr as sampleid,
		                        t1.Patient_name_vchr as PatientName,
		                        t1.sex_chr as PatientSex,
		                        t1.deptname_vchr as Dept,
                                t1.deptname_vchr  as PatientArea,
		                        t1.bedno_chr as BedNo,
		                        t1.sampletype_vchr as SampleType,
		                        t1.lastname_vchr   as DoctorName,
		                        t1.CONFIRM_DAT     as ReportTime,
                               CONCAT (t1.result_vchr, t2.result_vchr) AS CheckResult
                          FROM (SELECT samples.sampletype_vchr,
                                       samples.sample_id_chr, RESULT.result_vchr,
                                       samples.Patient_name_vchr,
                                       samples.sex_chr,
                                       samples.bedno_chr,
                                       samples.CONFIRM_DAT,
                                       RESULT.check_item_id_chr, 
			                           employee.lastname_vchr,
                                       dept.deptname_vchr
                                  FROM t_opr_lis_check_result RESULT,
                                       t_opr_lis_sample samples,
                                       t_bse_lis_check_item checkitem,
                                       t_bse_employee employee,
                                       t_bse_deptdesc dept,
                                            t_aid_lis_apply_unit unit,
                                            t_aid_lis_apply_unit_detail detail
                                 WHERE samples.status_int = 6
                                   AND samples.appl_empid_chr = employee.empid_chr(+)
                                   AND samples.appl_deptid_chr = dept.deptid_chr(+)
                                   AND RESULT.status_int = 1
                                        AND checkitem.assist_code02_chr = 'Germ'
                                        AND unit.assist_code02_chr = 'Germ'
                                        AND detail.apply_unit_id_chr = unit.apply_unit_id_chr
                                        AND checkitem.check_item_id_chr = detail.check_item_id_chr
                                        
                                   AND samples.sample_id_chr = RESULT.sample_id_chr
                                   AND checkitem.check_item_id_chr = RESULT.check_item_id_chr
                                   AND samples.confirm_dat >
                                                TO_DATE ('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                   AND samples.confirm_dat <
                                                TO_DATE ('{1}', 'yyyy-mm-dd hh24:mi:ss')) t1,
                               (SELECT samples.*,
                                       DECODE (RESULT.result_vchr,
                                               '-', 'ϸ������',
                                               CONCAT (RESULT.result_vchr,'����')
                                              ) AS result_vchr,
                                       RESULT.check_item_id_chr
                                  FROM t_opr_lis_check_result RESULT,
                                       t_opr_lis_sample samples,
                                       t_bse_lis_check_item checkitem,
                                            t_aid_lis_apply_unit unit,
                                            t_aid_lis_apply_unit_detail detail
                                 WHERE samples.status_int = 6
                                   AND RESULT.status_int = 1
                                           AND checkitem.assist_code02_chr = 'GermR'
                                           AND unit.assist_code02_chr = 'Germ'
                                           AND detail.apply_unit_id_chr = unit.apply_unit_id_chr
                                           AND checkitem.check_item_id_chr = detail.check_item_id_chr
                                   AND samples.sample_id_chr = RESULT.sample_id_chr
                                   AND checkitem.check_item_id_chr = RESULT.check_item_id_chr) t2
                         WHERE t1.sample_id_chr = t2.sample_id_chr
                               {2}
                            ";
            #endregion

            string txtWhere = m_strConstructSql(listSamples, listPatientArea);
            p_dtbResult = null;
            strSQL = string.Format(strSQL, strDateFrom, strDateTo, txtWhere);
            return m_lngGetDataTableBySql( out p_dtbResult, strSQL);
        }

        /// <summary>
        /// ���ݲ��ż��Ϻͱ걾���Ϲ����ѯ����
        /// </summary>
        /// <param name="listSamples"></param>
        /// <param name="listPatientArea"></param>
        /// <returns></returns>
        private static string m_strConstructSql(List<string> listSamples, List<string> listPatientArea)
        {
            StringBuilder sbSamples = new StringBuilder();
            if (listSamples.Count > 0)
            {

                sbSamples.Append(" 1=0 ");
                foreach (string str in listSamples)
                {
                    sbSamples.AppendFormat(" or t1.sampletype_vchr ='{0}' ", str);
                }

            }
            else
            {
                sbSamples.Append("1=1");
            }

            StringBuilder sbArea = new StringBuilder();
            if (listPatientArea.Count > 0)
            {
                sbArea.Append(" 1=0 ");
                foreach (string str in listPatientArea)
                {
                    sbArea.AppendFormat(" or t1.deptname_vchr ='{0}'", str);
                }
            }
            else
            {
                sbArea.Append("1=1");
            }

            return string.Format("And ({0}) And ({1})", sbSamples.ToString(), sbArea.ToString());
        }


        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <returns>�����б�ļ���DataTable</returns>
        [AutoComplete]
        public DataTable m_dtbGetSamplesList()
        {
            DataTable dtbResult = null;
            string sql = "select sample_type_id_chr,sample_type_desc_vchr from t_aid_lis_sampletype order by sample_type_id_chr";
            return m_lngGetDateTableBySql(sql, out dtbResult);
        }


        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <returns>�����б�ļ���DataTable</returns>
        [AutoComplete]
        public DataTable m_dtbGetDeptList()
        {
            DataTable dtbResult = null;
            string sql = "select DEPTID_CHR,DEPTNAME_VCHR from t_bse_deptdesc";
            return m_lngGetDateTableBySql(sql, out dtbResult);
        }

        #endregion

        #region ��������

        /// <summary>
        /// ͨ��sql�����datatable
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="dtbResult">�������</param>
        /// <returns>dataTable����</returns>
        public DataTable m_lngGetDateTableBySql(string sql, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(sql, ref dtbResult);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dtbResult;

        }

        /// <summary>
        /// ����Sql����ȡDataTable����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtbResult">DataTable����</param>
        /// <param name="p_strSql">sql���</param>
        /// <returns></returns>
        private long m_lngGetDataTableBySql(  out DataTable p_dtbResult, string p_strSql)
        {
            long lngRes = 0;
            p_dtbResult = null; 
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(p_strSql, ref p_dtbResult);
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

        #region ͳ������������
        /// <summary>
        /// ͳ������������
        /// baojian.mo add in 2007.12.01
        /// </summary>
        /// <param name="p_datStart"></param>
        /// <param name="p_datEnd"></param>
        /// <param name="p_dtStatisResult"></param>
        [AutoComplete]
        public void m_mthGetDeviceCheckStatis(DateTime p_datStart, DateTime p_datEnd, ref DataTable p_dtStatisResult)
        {
            long lngRes = 0;
            p_dtStatisResult = new DataTable();
            string strSQL = "";
            string strStartDate = p_datStart.ToString();
            string strEndDate = p_datEnd.ToString();
            try
            {
                strSQL = @"select t2.device_code_chr as devicecode_chr,
                                  t2.devicename_vchr as devicename_chr,
                                  count (t2.devicename_vchr) as reportsum_num, tb.reportitemtotal_num
                             from (select   trunc (a.check_dat), a.device_sampleid_chr, a.deviceid_chr
                                       from t_opr_lis_result_log a
                                      where a.check_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                   group by trunc (a.check_dat), a.device_sampleid_chr, a.deviceid_chr) ta,
                                  t_bse_lis_device t2,
                                  (select   t4.deviceid_chr,
                                            count (t4.deviceid_chr) as reportitemtotal_num
                                       from (select   b.deviceid_chr
                                                 from t_opr_lis_result b
                                                where b.check_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                             group by trunc (b.check_dat),
                                                      b.deviceid_chr,
                                                      b.device_sampleid_chr,
                                                      b.device_check_item_name_vchr) t4
                                   group by t4.deviceid_chr) tb
                            where ta.deviceid_chr = t2.deviceid_chr
                              and ta.deviceid_chr = tb.deviceid_chr
                         group by t2.devicename_vchr, t2.device_code_chr, tb.reportitemtotal_num
                         order by t2.device_code_chr";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objParamArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objParamArr);
                objParamArr[0].Value = strStartDate;
                objParamArr[1].Value = strEndDate;
                objParamArr[2].Value = strStartDate;
                objParamArr[3].Value = strEndDate;
                objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtStatisResult, objParamArr);
                //objHRPSvc.Dispose();

                #region Ѫ����(�����)��ͳ��
                //<==================================================== �������Ǵ��ڣ�������log��
                int intReportTotal = 0; //���浥����
                int intItemTotal = 0; //���浥����
                DataTable dtTmp = new DataTable();
                strSQL = @"select count (b.sample_id_chr) as reportsum_num
                             from (select a.sample_id_chr
                                     from t_opr_lis_check_result a
                                    where a.deviceid_chr = '002120' and a.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                 group by trunc (a.modify_dat), a.sample_id_chr, a.deviceid_chr) b";
                objParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objParamArr);
                objParamArr[0].Value = strStartDate;
                objParamArr[1].Value = strEndDate;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTmp, objParamArr);
                if (lngRes > 0)
                {
                    if (dtTmp.Rows.Count > 0)
                    {
                        intReportTotal = int.Parse(dtTmp.Rows[0][0].ToString());
                        //���浥����
                        strSQL = @"select count (b.sample_id_chr) as reportitemtotal_num
                                     from (select a.sample_id_chr
                                             from t_opr_lis_check_result a
                                            where a.deviceid_chr = '002120' and a.modify_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                         group by trunc (a.modify_dat),
                                                  a.sample_id_chr,
                                                  a.deviceid_chr,
                                                  a.check_item_id_chr) b";
                        objParamArr = null;
                        objHRPSvc.CreateDatabaseParameter(2, out objParamArr);
                        objParamArr[0].Value = strStartDate;
                        objParamArr[1].Value = strEndDate;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTmp, objParamArr);
                        if (lngRes > 0 && dtTmp.Rows.Count > 0)
                        {
                            intItemTotal = int.Parse(dtTmp.Rows[0][0].ToString());
                        }
                        DataRow dr = p_dtStatisResult.NewRow();
                        dr["devicecode_chr"] = "002120";
                        dr["devicename_chr"] = "Advia 2120";
                        dr["reportsum_num"] = intReportTotal;
                        dr["reportitemtotal_num"] = intItemTotal;
                        p_dtStatisResult.Rows.Add(dr);
                    }
                }
                objHRPSvc.Dispose();
                //============================================================>
                #endregion
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion

    }
}
