using System;
using com.digitalwave.iCare.middletier;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using weCare.Core.Entity;
using System.Collections;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 药典管理中间件   2004-7-23 黄国平
    /// </summary>
    public class clsMedicineStdManager : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsMedicineStdManager()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取药典分类编号
        /// </summary>
        /// <returns>返回编号</returns>
        [AutoComplete]
        public string m_lngGetMedicineStdCatID()
        {
            string SQLstr = "SELECT Max(MEDICINESTDCATID_CHR) as ID FROM T_BSE_MEDICINESTDCAT";
            long lngRes = 0;
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQLstr, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0]["ID"].ToString().Trim() == "")
                    {
                        return "0000001";
                    }
                    int ID = int.Parse(dtbResult.Rows[0]["ID"].ToString()) + 1;
                    string retStr = ID.ToString().PadLeft(7, '0');
                    return retStr;
                }
                else
                {
                    return "0000001";
                }
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// 查找西药收费项目,
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strType"></param>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthFindWMedicineByID(string strUser, string ID, string strPatientTypeID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = "";

            if (strPatientTypeID.Trim() == "")
            {
                strSQL = @"select A.ITEMID_CHR,A.ITEMNAME_VCHR,A.ITEMSPEC_VCHR, A.ITEMENGNAME_VCHR ,a.itemopunit_chr, b.deptprep_int, 
									a.itemipunit_chr,A.DOSAGEUNIT_CHR,A.PACKQTY_DEC,Round(A.ITEMPRICE_MNY/A.PACKQTY_DEC,4) SubMoney,A.ITEMPRICE_MNY,
									''as type,B.NOQTYFLAG_INT,b.MINDOSAGE_DEC,b.maxdosage_dec,b.ADULTDOSAGE_DEC,
									b.CHILDDOSAGE_DEC,b.nmldosage_dec,A.opchargeflg_int,a.USAGEID_CHR,a.ITEMOPINVTYPE_CHR, h.freqid_chr as freqid, h.freqname_chr as freqname, h.times_int as freqtimes, h.days_int as freqdays, 
									c.usagename_vchr, a.dosage_dec,a.ITEMCODE_VCHR ,100 as precent_dec ,b.assistcode_chr,B.HYPE_INT,b.ISANAESTHESIA_CHR,b.ISCHLORPROMAZINE_CHR, b.ISCHLORPROMAZINE2_CHR, b.ispoison_chr
								from t_bse_chargeitem A ,T_BSE_MEDICINE B,
								T_BSE_USAGETYPE C, T_BSE_CHARGECATMAP D, t_aid_recipefreq h 
								where trim(A.ITEMSRCID_VCHR)=trim(B.MEDICINEID_CHR(+)) and a.IFSTOP_INT =0 
								and A.USAGEID_CHR=c.usageid_chr(+)  and a.ITEMOPINVTYPE_CHR=d.catid_chr(+) 
					and b.MEDICINESTDID_CHR = '" + ID + @"'
								and d.INTERNALFLAG_INT=0 and a.freqid_chr = h.freqid_chr(+) order by b.assistcode_chr";
            }
            else
            {
                strSQL = @"select A.ITEMID_CHR,A.ITEMNAME_VCHR,A.ITEMSPEC_VCHR, A.ITEMENGNAME_VCHR ,a.itemopunit_chr, b.deptprep_int, 
									a.itemipunit_chr,A.DOSAGEUNIT_CHR,A.PACKQTY_DEC,Round(A.ITEMPRICE_MNY/A.PACKQTY_DEC,4) SubMoney,A.ITEMPRICE_MNY,
									''as type,B.NOQTYFLAG_INT,b.MINDOSAGE_DEC,b.maxdosage_dec,b.ADULTDOSAGE_DEC,
									b.CHILDDOSAGE_DEC,b.nmldosage_dec,A.opchargeflg_int,a.USAGEID_CHR,a.ITEMOPINVTYPE_CHR, h.freqid_chr as freqid, h.freqname_chr as freqname, h.times_int as freqtimes, h.days_int as freqdays, 
									c.usagename_vchr, a.dosage_dec,a.ITEMCODE_VCHR ,f.precent_dec ,b.assistcode_chr,B.HYPE_INT,b.ISANAESTHESIA_CHR,b.ISCHLORPROMAZINE_CHR, b.ISCHLORPROMAZINE2_CHR, b.ispoison_chr
								from t_bse_chargeitem A ,T_BSE_MEDICINE B, t_aid_recipefreq h, 
								T_BSE_USAGETYPE C, T_BSE_CHARGECATMAP D, (SELECT * FROM t_aid_inschargeitem WHERE copayid_chr = '" + strPatientTypeID + @"') f
								where trim(A.ITEMSRCID_VCHR)=trim(B.MEDICINEID_CHR(+)) and a.IFSTOP_INT =0 
								and A.USAGEID_CHR=c.usageid_chr(+)  and a.ITEMOPINVTYPE_CHR=d.catid_chr(+) 
					            and b.MEDICINESTDID_CHR = '" + ID + @"'
								and d.INTERNALFLAG_INT=0 and a.itemid_chr = f.itemid_chr(+) and a.freqid_chr = h.freqid_chr(+) order by b.assistcode_chr";
            }


            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        /// <summary>
        /// 保存药典分类
        /// </summary>
        /// <param name="OBJ"></param>
        /// <returns>成功为1 否则 0</returns>
        [AutoComplete]
        public long m_lngSaveMedicineStdCat(clsMedicineSTDCAT OBJ)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = "INSERT INTO T_BSE_MEDICINESTDCAT (MEDICINESTDCATID_CHR,PMEDICINESTDCATID_CHR,MEDICINESTDCATNAME_VCHR,LEVEL_INT,MEDICINESTDCATENAME_VCHR) VALUES (?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = OBJ.m_strMEDICINESTDCATID_CHR;
                objLisAddItemRefArr[1].Value = OBJ.m_strPMEDICINESTDCATID_CHR;
                objLisAddItemRefArr[2].Value = OBJ.m_strMEDICINESTDCATNAME_VCHR;
                objLisAddItemRefArr[3].Value = OBJ.m_intLEVEL_INT;
                objLisAddItemRefArr[4].Value = OBJ.m_strMEDICINESTDCATENAME_VCHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        /// <summary>
        /// 返回节点数据
        /// </summary>
        /// <param name="level">父节点</param>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetNodes(string level, out clsMedicineSTDCAT[] p_objResultArr)
        {
            p_objResultArr = new clsMedicineSTDCAT[0];
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_BSE_MEDICINESTDCAT WHERE PMEDICINESTDCATID_CHR = '" + level + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicineSTDCAT[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicineSTDCAT();
                        p_objResultArr[i1].m_strMEDICINESTDCATID_CHR = dtbResult.Rows[i1]["MEDICINESTDCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPMEDICINESTDCATID_CHR = dtbResult.Rows[i1]["PMEDICINESTDCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDICINESTDCATNAME_VCHR = dtbResult.Rows[i1]["MEDICINESTDCATNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intLEVEL_INT = Convert.ToInt32(dtbResult.Rows[i1]["LEVEL_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strMEDICINESTDCATENAME_VCHR = dtbResult.Rows[i1]["MEDICINESTDCATENAME_VCHR"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        /// <summary>
        /// 返回药节点数据
        /// </summary>
        /// <param name="level">父节点</param>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineNodes(string level, out clsMedicineSTDCAT[] p_objResultArr)
        {
            p_objResultArr = new clsMedicineSTDCAT[0];
            long lngRes = 0;
            string strSQL = @"SELECT MEDICINESTDID_CHR,MEDICINESTDNAME_VCHR,MEDICINESTDCATID_CHR FROM T_BSE_MEDICINESTD WHERE MEDICINESTDCATID_CHR = '" + level + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicineSTDCAT[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicineSTDCAT();
                        p_objResultArr[i1].m_strMEDICINESTDCATID_CHR = dtbResult.Rows[i1]["MEDICINESTDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPMEDICINESTDCATID_CHR = dtbResult.Rows[i1]["MEDICINESTDCATID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDICINESTDCATNAME_VCHR = dtbResult.Rows[i1]["MEDICINESTDNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intLEVEL_INT = 3;
                        p_objResultArr[i1].m_strMEDICINESTDCATENAME_VCHR = "";
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        /// <summary>
        /// 更新节点名称
        /// </summary>
        /// <param name="ID">节点ID(不能修改)</param>
        /// <param name="p_objPrincipal"></param>
        /// <param name="newName">新分类名称</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateNode(string ID, string newName)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = "Update T_BSE_MEDICINESTDCAT Set MEDICINESTDCATNAME_VCHR = '" + newName + "' WHERE MEDICINESTDCATID_CHR ='" + ID + "'";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;

        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="ID">节点ID</param>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteNode(string ID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = "Update T_BSE_MEDICINESTDCAT Set PMEDICINESTDCATID_CHR = '-1' WHERE MEDICINESTDCATID_CHR ='" + ID + "'";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;

        }
        [AutoComplete]
        public long m_lngGetMedicineInfoByID(string ID, out clsMEDICINESTD p_objResultArr, out clsMedicineTabu[] objMTabu, out clsDiseaseTabu[] objDTabu)
        {
            p_objResultArr = new clsMEDICINESTD();
            objMTabu = new clsMedicineTabu[0];
            objDTabu = new clsDiseaseTabu[0];
            long lngRes = 0;
            string strSQL = @"SELECT  A.*,B.CONTEXT_VCHR,B.REMARK_VCHR  FROM T_BSE_MEDICINESTD A LEFT OUTER 
								JOIN T_BSE_MEDICINESTDDesc B
								ON A.MEDICINESTDID_CHR =B.MEDICINESTDID_CHR
								WHERE A.MEDICINESTDID_CHR = '" + ID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                //				#region 药品基本信息
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr.m_strMEDICINESTDID_CHR = dtbResult.Rows[0]["MEDICINESTDID_CHR"].ToString().Trim();
                    p_objResultArr.m_strMEDICINESTDNAME_VCHR = dtbResult.Rows[0]["MEDICINESTDNAME_VCHR"].ToString().Trim();
                    p_objResultArr.m_strMEDICINESTDENAME_VCHR = dtbResult.Rows[0]["MEDICINESTDENAME_VCHR"].ToString().Trim();
                    p_objResultArr.m_strMEDICINESTDCATID_CHR = dtbResult.Rows[0]["MEDICINESTDCATID_CHR"].ToString().Trim();
                    p_objResultArr.m_strALIASCN_VCHR = dtbResult.Rows[0]["ALIASCN_VCHR"].ToString().Trim();
                    p_objResultArr.m_strALIASEN_VCHR = dtbResult.Rows[0]["ALIASEN_VCHR"].ToString().Trim();
                    p_objResultArr.m_strUSERNO_CHR = dtbResult.Rows[0]["USERNO_CHR"].ToString().Trim();
                    p_objResultArr.m_strPYCODE_CHR = dtbResult.Rows[0]["PYCODE_CHR"].ToString().Trim();
                    p_objResultArr.m_strWBCODE_CHR = dtbResult.Rows[0]["WBCODE_CHR"].ToString().Trim();
                    p_objResultArr.m_strReMark = dtbResult.Rows[0]["REMARK_VCHR"].ToString().Trim();
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_objResultArr.m_strBasicInfo += dtbResult.Rows[i]["CONTEXT_VCHR"].ToString().Trim();
                    }
                    //					#endregion
                    //					#region 药品禁忌
                    strSQL = @"select A.*,B.MEDICINESTDNAME_VCHR from t_bse_MedStdRelation A LEFT OUTER JOIN t_bse_MedicineStd B ON
							A.refmedicinestdid_chr =B.MEDICINESTDID_CHR
							WHERE A.medicinestdid_chr ='" + ID + "'AND A.APPLICABILITYFLAG_INT =3";

                    long rt = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                    if (rt > 0 && dtbResult.Rows.Count > 0)
                    {
                        objMTabu = new clsMedicineTabu[dtbResult.Rows.Count];
                        for (int i = 0; i < dtbResult.Rows.Count; i++)
                        {
                            objMTabu[i] = new clsMedicineTabu();
                            objMTabu[i].m_intAPPLICABILITYFLAG_INT = int.Parse(dtbResult.Rows[i]["APPLICABILITYFLAG_INT"].ToString().Trim());
                            objMTabu[i].m_strMEDICINESTDID_CHR = dtbResult.Rows[i]["MEDICINESTDID_CHR"].ToString().Trim();
                            objMTabu[i].m_strMEMO_VCHR = dtbResult.Rows[i]["MEMO_VCHR"].ToString().Trim();
                            objMTabu[i].m_strREFMEDICINESTDID_CHR = dtbResult.Rows[i]["REFMEDICINESTDID_CHR"].ToString().Trim();
                            objMTabu[i].m_strREFMEDICINESTDNAME_CHR = dtbResult.Rows[i]["MEDICINESTDNAME_VCHR"].ToString().Trim();
                        }
                    }
                    //					#endregion
                    //					#region 病症禁忌
                    strSQL = @"SELECT  MEDICINESTDID_CHR,DISEASENAME_VCHR,APPLICABILITYFLAG_INT,MEMO_VCHR FROM t_bse_MedStdAndDisease
							WHERE MEDICINESTDID_CHR ='" + ID + "' AND APPLICABILITYFLAG_INT=0";

                    long rt2 = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                    objHRPSvc.Dispose();
                    if (rt2 > 0 && dtbResult.Rows.Count > 0)
                    {
                        objDTabu = new clsDiseaseTabu[dtbResult.Rows.Count];
                        for (int ii = 0; ii < dtbResult.Rows.Count; ii++)
                        {
                            objDTabu[ii] = new clsDiseaseTabu();
                            objDTabu[ii].m_intAPPLICABILITYFLAG_INT = int.Parse(dtbResult.Rows[ii]["APPLICABILITYFLAG_INT"].ToString().Trim());
                            objDTabu[ii].m_strMEDICINESTDID_CHR = dtbResult.Rows[ii]["MEDICINESTDID_CHR"].ToString().Trim();
                            objDTabu[ii].m_strMEMO_VCHR = dtbResult.Rows[ii]["MEMO_VCHR"].ToString().Trim();
                            objDTabu[ii].m_strDISEASENAME_VCHR = dtbResult.Rows[ii]["DISEASENAME_VCHR"].ToString().Trim();
                        }
                    }
                    //					#endregion
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }

            return lngRes;
        }
        /// <summary>
        /// 返回药品编号
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>返回ID</returns>
        [AutoComplete]
        public string m_lngAddNew()
        {
            string SQLstr = "SELECT Max(MEDICINESTDID_CHR) as ID FROM T_BSE_MEDICINESTD";
            long lngRes = 0;
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQLstr, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0]["ID"].ToString().Trim() == "")
                    {
                        return "0000001";
                    }
                    int ID = int.Parse(dtbResult.Rows[0]["ID"].ToString()) + 1;
                    string retStr = ID.ToString().PadLeft(7, '0');
                    return retStr;
                }
                else
                {
                    return "0000001";
                }
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 保存新药品数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertData(clsMEDICINESTD p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = "INSERT INTO T_BSE_MEDICINESTD (MEDICINESTDID_CHR,MEDICINESTDNAME_VCHR,MEDICINESTDENAME_VCHR,MEDICINESTDCATID_CHR,ALIASCN_VCHR,ALIASEN_VCHR,USERNO_CHR,PYCODE_CHR,WBCODE_CHR) VALUES (?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(9, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strMEDICINESTDID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strMEDICINESTDNAME_VCHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strMEDICINESTDENAME_VCHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strMEDICINESTDCATID_CHR;
                objLisAddItemRefArr[4].Value = p_objRecord.m_strALIASCN_VCHR;
                objLisAddItemRefArr[5].Value = p_objRecord.m_strALIASEN_VCHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strUSERNO_CHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_strPYCODE_CHR;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strWBCODE_CHR;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                if (p_objRecord.m_strBasicInfo.Trim() != "")
                {
                    //获得要分多少条记录
                    double Length = (double)p_objRecord.m_strBasicInfo.Trim().Length;
                    Length = Length * -1;
                    double count = Math.Floor(Length / 2000);
                    count = count * -1;
                    int LastSubStrLength = p_objRecord.m_strBasicInfo.Trim().Length % 2000;
                    string substring = "";
                    string strReMark = "";
                    for (int i = 0; i < count; i++)
                    {
                        if ((i + 1) == count)
                        {
                            substring = p_objRecord.m_strBasicInfo.Trim().Substring(1000 * i, LastSubStrLength);
                        }
                        else
                        {
                            substring = p_objRecord.m_strBasicInfo.Trim().Substring(1000 * i, 1000);
                        }
                        if (i == 0)
                        {
                            strReMark = p_objRecord.m_strReMark;
                        }
                        else
                        {
                            strReMark = " ";
                        }
                        string SQLstr = @"INSERT INTO T_BSE_MEDICINESTDDESC VALUES ('" + p_objRecord.m_strMEDICINESTDID_CHR + "','" + i + "','" + substring + "','" + strReMark + "')";
                        long retstr = objHRPSvc.DoExcute(SQLstr);
                    }
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        /// <summary>
        /// 更新药品记录
        /// </summary>
        /// <param name="IsInfoChange">药品描述改变</param>
        /// <param name="IsNoChange">药品编号改变</param>
        /// <param name="ID">药品编号</param>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateData(bool IsInfoChange, bool IsNoChange, string ID, clsMEDICINESTD p_objRecord)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            //			string strSQL = "UPDATE T_BSE_MEDICINESTD SET MEDICINESTDID_CHR=?, MEDICINESTDNAME_VCHR=?, MEDICINESTDENAME_VCHR=?, MEDICINESTDCATID_CHR=?, ALIASCN_VCHR=?,ALIASEN_VCHR=?, USERNO_CHR=?, PYCODE_CHR=?, WBCODE_CHR=? WHERE MEDICINESTDID_CHR =?";
            //MEDICINESTDENAME_VCHR=?, MEDICINESTDCATID_CHR=?, ALIASCN_VCHR=?,ALIASEN_VCHR=?, USERNO_CHR=?, PYCODE_CHR=?, WBCODE_CHR=? WHERE MEDICINESTDID_CHR =?";
            string strSQL2 = @"UPDATE T_BSE_MEDICINESTD SET MEDICINESTDID_CHR='" + p_objRecord.m_strMEDICINESTDID_CHR + "',MEDICINESTDNAME_VCHR ='" + p_objRecord.m_strMEDICINESTDNAME_VCHR +
                "',MEDICINESTDENAME_VCHR ='" + p_objRecord.m_strMEDICINESTDENAME_VCHR + "',MEDICINESTDCATID_CHR ='" + p_objRecord.m_strMEDICINESTDCATID_CHR +
                "',ALIASCN_VCHR ='" + p_objRecord.m_strALIASCN_VCHR + "',ALIASEN_VCHR ='" + p_objRecord.m_strALIASEN_VCHR + "',USERNO_CHR ='" + p_objRecord.m_strUSERNO_CHR +
                "',PYCODE_CHR ='" + p_objRecord.m_strPYCODE_CHR + "',WBCODE_CHR ='" + p_objRecord.m_strWBCODE_CHR + "' WHERE MEDICINESTDID_CHR ='" + ID + "'";
            try
            {
                //				System.Data.IDataParameter[] objLisAddItemRefArr = null;
                //				objHRPSvc.CreateDatabaseParameter(10,out objLisAddItemRefArr);
                //				//Please change the datetime and reocrdid 
                //				objLisAddItemRefArr[0].Value = p_objRecord.m_strMEDICINESTDID_CHR;
                //				objLisAddItemRefArr[1].Value = p_objRecord.m_strMEDICINESTDNAME_VCHR;
                //				objLisAddItemRefArr[2].Value = p_objRecord.m_strMEDICINESTDENAME_VCHR;
                //				objLisAddItemRefArr[3].Value = p_objRecord.m_strMEDICINESTDCATID_CHR;
                //				objLisAddItemRefArr[4].Value = p_objRecord.m_strALIASCN_VCHR;
                //				objLisAddItemRefArr[5].Value = p_objRecord.m_strALIASEN_VCHR;
                //				objLisAddItemRefArr[6].Value = p_objRecord.m_strUSERNO_CHR;
                //				objLisAddItemRefArr[7].Value = p_objRecord.m_strPYCODE_CHR;
                //				objLisAddItemRefArr[8].Value = p_objRecord.m_strWBCODE_CHR;
                //				objLisAddItemRefArr[9].Value = ID;
                //				long lngRecEff = -1;
                //往表增加记录
                //				lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL2,ref lngRecEff,objLisAddItemRefArr);
                lngRes = objHRPSvc.DoExcute(strSQL2);
                if (IsNoChange && IsInfoChange)
                {
                    this.m_lngDeleteData(ID);
                    this.m_lngInsetDataToDesc(p_objRecord.m_strMEDICINESTDID_CHR, p_objRecord.m_strBasicInfo.Trim(), p_objRecord.m_strReMark.Trim());
                }
                else
                {
                    if (IsNoChange)
                    {
                        string SQLstr = @"UPDATE T_BSE_MEDICINESTDDESC SET MEDICINESTDID_CHR ='" + ID + "'";
                        long retstr = objHRPSvc.DoExcute(SQLstr);
                        //只更新编号
                        return 1;
                    }
                    if (IsInfoChange)
                    {
                        this.m_lngDeleteData(ID);
                        this.m_lngInsetDataToDesc(p_objRecord.m_strMEDICINESTDID_CHR, p_objRecord.m_strBasicInfo.Trim(), p_objRecord.m_strReMark.Trim());
                    }
                }


                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        /// <summary>
        /// 删除详细信息记录
        /// </summary>
        /// <param name="ID"></param>
        private void m_lngDeleteData(string ID)
        {
            string SQLstr = @"DELETE T_BSE_MEDICINESTDDESC WHERE MEDICINESTDID_CHR ='" + ID + "'";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            long retstr = objHRPSvc.DoExcute(SQLstr);
        }
        /// <summary>
        /// 插入信息表
        /// </summary>
        /// <param name="data"></param>
        [AutoComplete]
        private void m_lngInsetDataToDesc(string ID, string data, string ReMark)
        {
            if (data != "")
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                //获得要分多少条记录
                double Length = (double)data.Length;
                Length = Length * -1;
                double count = Math.Floor(Length / 1000);
                count = count * -1;
                int LastSubStrLength = data.Length % 1000;
                string substring = "";
                for (int i = 0; i < count; i++)
                {

                    if ((i + 1) == count)
                    {
                        substring = data.Substring(1000 * i, LastSubStrLength);
                    }
                    else
                    {
                        substring = data.Substring(1000 * i, 1000);
                    }
                    if (i != 0)
                    {
                        ReMark = "";
                    }
                    string SQLstr = @"INSERT INTO T_BSE_MEDICINESTDDESC VALUES ('" + ID + "','" + i + "','" + substring + "','" + ReMark + "')";
                    long retstr = objHRPSvc.DoExcute(SQLstr);
                }
            }
        }

        /// <summary>
        /// 删除药品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedicineInfoByID(string ID)
        {
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string SQLstr = @"DELETE T_BSE_MEDICINESTD WHERE MEDICINESTDID_CHR ='" + ID + "'";
            long retstr = objHRPSvc.DoExcute(SQLstr);
            this.m_lngDeleteData(ID);
            return retstr;

        }
        /// <summary>
        /// 返回查找药品列表
        /// </summary>
        /// <param name="Type">查找类型</param>
        /// <param name="FindStr">查找内容</param>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedicineByID(string Type, string FindStr, out clsMedicineSTDCAT[] p_objResultArr)
        {
            p_objResultArr = null;

            long lngRes = 0;
            string CloumnStr = Type;
            if (Type == "MEDICINESTDNAME_VCHR")//如果是按药品查找就查出编号和药品
            {
                CloumnStr = "MEDICINESTDID_CHR";
            }
            string SQLstr = @"SELECT " + CloumnStr + ",MEDICINESTDNAME_VCHR,MEDICINESTDID_CHR  as AA FROM T_BSE_MEDICINESTD WHERE lower (" + Type + ")  LIKE '" + FindStr.ToLower() + "%'";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQLstr, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicineSTDCAT[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        //这里只是用ValueObject作为传值,实际上并没有对应关系.
                        p_objResultArr[i1] = new clsMedicineSTDCAT();
                        p_objResultArr[i1].m_strMEDICINESTDCATNAME_VCHR = dtbResult.Rows[i1]["MEDICINESTDNAME_VCHR"].ToString().Trim();//药名
                        p_objResultArr[i1].m_strMEDICINESTDCATID_CHR = dtbResult.Rows[i1][CloumnStr].ToString().Trim();//
                        p_objResultArr[i1].m_strMEDICINESTDCATENAME_VCHR = dtbResult.Rows[i1]["AA"].ToString().Trim();//记录药品编号
                    }
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }

        /// <summary>
        /// 插入药品禁忌
        /// </summary>
        /// <param name="p_objMedicineSTDCAT">调用药品分类中间件来传值</param>
        /// <param name="p_objPrincipal"></param>
        /// <returns>成功1 ,否则 0</returns>
        [AutoComplete]
        public long m_lngInsertMedicineTabu(clsMedicineSTDCAT p_objMedicineSTDCAT)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            try
            {
                string SQLstr = @"INSERT INTO t_bse_MedStdRelation VALUES ('" + p_objMedicineSTDCAT.m_strPMEDICINESTDCATID_CHR/*药品ID*/+ "','" + p_objMedicineSTDCAT.m_strMEDICINESTDCATID_CHR/*禁忌药物ID*/+ "','" + 3 + "','" + p_objMedicineSTDCAT.m_strMEDICINESTDCATENAME_VCHR/*备注*/+ "')";
                lngRes = objHRPSvc.DoExcute(SQLstr);
                objHRPSvc.Dispose();
            }

            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        /// <summary>
        /// 保存病症禁忌信息
        /// </summary>
        /// <param name="objDTabu"></param>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddDiseaseTabu(clsDiseaseTabu objDTabu)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            try
            {
                string SQLstr = @"INSERT INTO t_bse_MedStdAndDisease VALUES ('" + objDTabu.m_strMEDICINESTDID_CHR + "','" + objDTabu.m_intAPPLICABILITYFLAG_INT + "','" + objDTabu.m_strMEMO_VCHR + "','" + objDTabu.m_strDISEASENAME_VCHR + "')";
                lngRes = objHRPSvc.DoExcute(SQLstr);
                objHRPSvc.Dispose();
            }

            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        /// <summary>
        /// 删除药品禁忌
        /// </summary>
        /// <param name="ID">药典ID</param>
        /// <param name="ID2">禁忌药品ID</param>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedicineTabu(string ID, string ID2)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            try
            {
                string SQLstr = @"DELETE t_bse_MedStdRelation WHERE MEDICINESTDID_CHR ='" + ID + "' AND  REFMEDICINESTDID_CHR ='" + ID2 + "'";
                lngRes = objHRPSvc.DoExcute(SQLstr);
                objHRPSvc.Dispose();
            }

            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        /// <summary>
        /// 删除病症禁忌
        /// </summary>
        /// <param name="ID">药典ID</param>
        /// <param name="Name">疾病名称</param>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteDiseaseTabu(string ID, string Name)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            try
            {
                string SQLstr = @"DELETE t_bse_MedStdAndDisease WHERE MEDICINESTDID_CHR ='" + ID + "' AND  DISEASENAME_VCHR ='" + Name + "'";
                lngRes = objHRPSvc.DoExcute(SQLstr);
                objHRPSvc.Dispose();
            }

            catch (Exception objEx)
            {
                string strTmp = objEx.Message;

            }
            return lngRes;
        }
        #region 通过药典ID查找药典的父辈结点信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCodexID"></param>
        /// <param name="p_outArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCodex(string p_strCodexID, out System.Collections.Generic.List<string> p_outArr)
        {
            long lngRes = 0;
            p_outArr = new System.Collections.Generic.List<string>();
            System.Data.DataTable dt = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT  b.MEDICINESTDCATID_CHR FROM T_BSE_MEDICINESTD b
               where b.medicinestdid_chr = '" + p_strCodexID + "'";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception ee)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(ee);
            }
            if (dt == null || dt.Rows.Count < 1)
            {
                return -1;
            }
            p_outArr.Add(p_strCodexID);
            string strID = dt.Rows[0]["MEDICINESTDCATID_CHR"].ToString().Trim();
            p_outArr.Add(strID);
            while (true)
            {
                strSQL = "select a.medicinestdcatid_chr,a.pmedicinestdcatid_chr from t_bse_medicinestdcat a where a.medicinestdcatid_chr ='" + strID + "'";
                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                }
                catch (Exception ee)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    objLogger.LogError(ee);
                }
                if (dt == null || dt.Rows.Count < 1)
                {
                    return -1;
                }
                strID = dt.Rows[0]["pmedicinestdcatid_chr"].ToString().Trim();
                p_outArr.Add(strID);
                if (strID.Trim() == "1" || strID.Trim() == "2" || strID.Trim() == "3")
                {
                    break;
                }
            }
            return lngRes;
        }
        #endregion


        #region 通过药典ID查找药典的父辈结点信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCodexID"></param>
        /// <param name="p_outArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCodex(string p_strCodexID, out string panterName, out string strTypeName)
        {
            long lngRes = 0;
            strTypeName = "";
            panterName = "";
            System.Data.DataTable dt = null;
            System.Data.DataTable dt1 = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = @"SELECT MEDICINESTDCATID_CHR  FROM T_BSE_MEDICINESTD WHERE MEDICINESTDID_CHR = '" + p_strCodexID + "'";

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt1);
            }
            catch (Exception ee)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(ee);
            }

            strSQL = @"SELECT b.pmedicinestdcatid_chr,b.MEDICINESTDCATNAME_VCHR
      FROM t_bse_medicinestdcat b
CONNECT BY b.MEDICINESTDCATID_CHR= PRIOR   b.PMEDICINESTDCATID_CHR
               START WITH b.MEDICINESTDCATID_CHR = '" + dt1.Rows[0]["MEDICINESTDCATID_CHR"].ToString() + "'";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
            }
            catch (Exception ee)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogError(ee);
            }
            if (dt.Rows.Count > 0)
            {
                for (int i1 = dt.Rows.Count - 1; i1 >= 0; i1--)
                {
                    if (i1 == dt.Rows.Count - 1)
                    {
                        switch (dt.Rows[i1]["pmedicinestdcatid_chr"].ToString().Trim())
                        {
                            case "1":
                                strTypeName = "西药" + @"\" + dt.Rows[i1]["MEDICINESTDCATNAME_VCHR"].ToString().Trim();
                                panterName = "西药";
                                break;
                            case "2":
                                strTypeName = "中药" + @"\" + dt.Rows[i1]["MEDICINESTDCATNAME_VCHR"].ToString().Trim();
                                panterName = "中药";
                                break;
                            case "3":
                                strTypeName = "中成药" + @"\" + dt.Rows[i1]["MEDICINESTDCATNAME_VCHR"].ToString().Trim();
                                panterName = "中成药";
                                break;
                        }
                    }
                    else
                    {
                        strTypeName += @"\" + dt.Rows[i1]["MEDICINESTDCATNAME_VCHR"].ToString().Trim();
                    }
                }
            }
            return lngRes;
        }
        #endregion
    }

}
