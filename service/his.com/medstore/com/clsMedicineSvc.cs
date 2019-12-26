using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 药品信息的服务层 Create By Sam 2004-5-24
    /// </summary>
    /// 

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    // 药品管理中间件
    public class clsMedicineSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase //MiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsMedicineSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 设置中心药房缺药标志

        /// <summary>
        /// 设置中心药房缺药标志
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedID">药品ID</param>
        /// <param name="p_intFlag">中心药房缺药标志 0-有药 1－缺药</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetCenterStorageFlag(string p_strMedID, int p_intFlag)
        {
            long lngRes = 0;
            #region SQL
            string strSQL = "Update T_BSE_MEDICINE SET IPNOQTYFLAG_INT=" + p_intFlag + " WHERE MEDICINEID_CHR='" + p_strMedID + "'";
            #endregion

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngRes = 0;
                lngRes = objHRP.DoExcute(strSQL);
                objHRP.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objEx);
            }
            return lngRes;
        }

        #endregion

        #region 获得药品基本信息列表
        /// <summary>
        /// 获得药品基本信息列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedList(out clsMedicine_VO[] p_objResultArr)
        {

            long lngRes = 0;
            string strSQL = "SELECT a.*,b.MEDICINETYPENAME_VCHR,c.MEDICINEPREPTYPENAME_VCHR,d.VENDORNAME_VCHR,e.UNITNAME_CHR as DOSAGEUNAME,f.UNITNAME_CHR as OPUNITNAME,g.UNITNAME_CHR as IPUNITNAME, a.medbagunit, a.highriskflag, a.isproducedrugs  FROM T_BSE_MEDICINE a ,T_AID_MEDICINETYPE b,T_AID_MEDICINEPREPTYPE c,T_BSE_VENDOR d,T_AID_UNIT e,T_AID_UNIT f,T_AID_UNIT g  where  a.MEDICINEPREPTYPE_CHR=c.MEDICINEPREPTYPE_CHR(+)  and a.PRODUCTORID_CHR=d.VENDORID_CHR(+) and  a.MEDICINETYPEID_CHR=b.MEDICINETYPEID_CHR(+)  and  a.DOSAGEUNIT_CHR=e.UNITID_CHR(+)  and  a.OPUNIT_CHR=f.UNITID_CHR(+)  and  a.IPUNIT_CHR=g.UNITID_CHR(+)";

            lngRes = m_getResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 根据仓库ID获得药品基本信息列表
        /// <summary>
        /// 根据仓库ID获得药品基本信息列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="storageID">仓库ID</param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMed(string storageID, out DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = 0;
            string strSQL = "select a.MEDICINEID_CHR,b.ASSISTCODE_CHR,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.OPUNIT_CHR,b.UNITPRICE_MNY,b.PRODUCTORID_CHR,b.TRADEPRICE_MNY,b.PYCODE_CHR,b.WBCODE_CHR,d.AMOUNT_DEC, b.medbagunit, b.highriskflag, b.isproducedrugs, b.transno, b.varietycode from  T_BSE_StorageAndMedicine a,T_BSE_MEDICINE b,t_bse_storagemedicine d  where a.STORAGEID_CHR='" + storageID + "' and a.MEDICINEID_CHR=b.MEDICINEID_CHR  and a.STORAGEID_CHR=d.STORAGEID_CHR(+) and a.MEDICINEID_CHR=d.MEDICINEID_CHR(+) order by ASSISTCODE_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
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

        #region 获取供应商资料
        /// <summary>
        /// 获取供应商资料
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="VendorVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetVendor(out clsVendor_VO[] VendorVO)
        {
            long lngRes = 0;
            VendorVO = null;
            DataTable dtbResult = new DataTable();
            string strSQL = @"select USERCODE_CHR,VENDORNAME_VCHR,VENDORID_CHR,PYCODE_CHR,WBCODE_CHR from t_bse_vendor where VENDORTYPE_INT<>2 and PRODUCTTYPE_INT=1 order by USERCODE_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                VendorVO = new clsVendor_VO[dtbResult.Rows.Count];
                for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                {
                    VendorVO[i1] = new clsVendor_VO();
                    VendorVO[i1].m_strVendorID = dtbResult.Rows[i1]["VENDORID_CHR"].ToString();
                    VendorVO[i1].m_strVendorName = dtbResult.Rows[i1]["VENDORNAME_VCHR"].ToString();
                    VendorVO[i1].m_strWBCode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString();
                    VendorVO[i1].m_strPYCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString();
                }
            }
            return lngRes;
        }
        #endregion

        #region 获取厂家名称
        /// <summary>
        /// 获取厂家名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="vendorID"></param>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetVendorName(string vendorID, out string vendorName)
        {
            long lngRes = 0;
            vendorName = "";
            DataTable dtbResult = new DataTable();
            string strSQL = @"select VENDORNAME_VCHR from t_bse_vendor where VENDORID_CHR='" + vendorID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                vendorName = dtbResult.Rows[0]["VENDORNAME_VCHR"].ToString();
            }
            return lngRes;
        }
        #endregion

        #region 获得药品类别
        /// <summary>
        /// 获得药品类别
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedType(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = new DataTable();
            string strSQL = @"SELECT * from t_aid_medicinetype";

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

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

        #region 获得药品类别名称
        /// <summary>
        /// 获得药品类别名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ArrMedTypeName">药品类型名称</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedTypeArr(out string[] ArrMedTypeName, string[] MedTypeList)
        {
            long lngRes = 0;
            ArrMedTypeName = new string[MedTypeList.Length];
            string strSQL = "";
            DataTable dt = new DataTable();
            for (int i1 = 0; i1 < MedTypeList.Length; i1++)
            {
                strSQL = @"select MEDICINETYPENAME_VCHR  from T_AID_MEDICINETYPE where MEDICINETYPEID_CHR='" + MedTypeList[i1].ToString() + "'";
                try
                {

                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt.Rows.Count > 0)
                {
                    ArrMedTypeName[i1] = dt.Rows[0]["MEDICINETYPENAME_VCHR"].ToString();
                }
                else
                {
                    ArrMedTypeName[i1] = "";
                }
            }
            return lngRes;
        }
        #endregion

        #region 显示药品库存
        /// <summary>
        /// 显示药品库存
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strMedID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStorage(string strMedID, out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = "";
            strSQL = @"select sum(CURQTY_DEC) as AMOUNT_DEC,UNITID_CHR,storageName,FLAG_INT from (select a.CURQTY_DEC,a.UNITID_CHR,a.FLAG_INT,case when a.FLAG_INT=0 then (select b.STORAGENAME_VCHR from t_bse_storage b  where a.STORAGEID_CHR=b.STORAGEID_CHR) when a.FLAG_INT=1 then (select b.MEDSTORENAME_VCHR from t_bse_medstore b  where a.STORAGEID_CHR=b.MEDSTOREID_CHR and MEDSTORETYPE_INT=1) end as storageName from t_opr_storagemeddetail a where MEDICINEID_CHR='" + strMedID + "') group by storageName,UNITID_CHR,FLAG_INT";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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

        #region 药品查询信息
        /// <summary>
        /// 药品查询信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedTypeList">传入药品类型</param>
        /// <param name="objResultArr"></param>
        /// <param name="p_intFlag">指示药房标志 0-药房 1-中心药房</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMetList(string[] MedTypeList, int p_intFlag, out DataTable objResultArr)
        {
            long lngRes = 0;
            DataTable dtbResult = new DataTable();
            objResultArr = new DataTable();
            string strSQL;
            string strWhere = "";
            if (MedTypeList.Length > 0)
            {
                strWhere = " and (";
                for (int i1 = 0; i1 < MedTypeList.Length; i1++)
                {
                    if (i1 == 0)
                        strWhere += " a.MEDICINETYPEID_CHR='" + MedTypeList[i1].ToString() + "' ";
                    else
                        strWhere += " or  a.MEDICINETYPEID_CHR='" + MedTypeList[i1].ToString() + "' ";
                }
                strWhere += ")";
            }
            strSQL = @"SELECT a.*,case when OPCHARGEFLG_INT=0 then a.OPUNIT_CHR when a.OPCHARGEFLG_INT=1 then a.IPUNIT_CHR end as 门诊单位,case when a.OPCHARGEFLG_INT=0 then round(a.UNITPRICE_MNY,4) when a.OPCHARGEFLG_INT=1 then Round(a.UNITPRICE_MNY/a.PACKQTY_DEC,4) end as 门诊单价,case when IPCHARGEFLG_INT=0 then a.OPUNIT_CHR when a.IPCHARGEFLG_INT=1 then a.IPUNIT_CHR end as 住院单位,case when a.IPCHARGEFLG_INT=0 then round(a.UNITPRICE_MNY,4) when a.IPCHARGEFLG_INT=1 then Round(a.UNITPRICE_MNY/a.PACKQTY_DEC,4) end as 住院单价,b.VENDORNAME_VCHR,f.MEDICINETYPENAME_VCHR,g.MEDICINEPREPTYPENAME_VCHR 
						 FROM T_BSE_MEDICINE a,
							  T_BSE_VENDOR b,
							  T_AID_MEDICINETYPE f,
                              T_AID_MEDICINEPREPTYPE g 
                        WHERE a.PRODUCTORID_CHR=b.VENDORID_CHR(+) 
						  AND a.MEDICINETYPEID_CHR=f.MEDICINETYPEID_CHR(+) 
                          AND a.MEDICINEPREPTYPE_CHR=g.MEDICINEPREPTYPE_CHR(+)
						  AND a.IFSTOP_INT=0" + strWhere +
                        @" ORDER BY a.ASSISTCODE_CHR";


            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    objResultArr.Columns.Add("助记码");
                    objResultArr.Columns.Add("药品名称");
                    objResultArr.Columns.Add("药品通用名");
                    objResultArr.Columns.Add("英文名");
                    objResultArr.Columns.Add("类别");
                    objResultArr.Columns.Add("制剂");
                    objResultArr.Columns.Add("规格");
                    objResultArr.Columns.Add("批发价");
                    objResultArr.Columns.Add("单价");
                    objResultArr.Columns.Add("生产厂家");
                    objResultArr.Columns.Add("剂量");
                    objResultArr.Columns.Add("剂量单位");
                    objResultArr.Columns.Add("门诊单位");
                    objResultArr.Columns.Add("住院单位");
                    objResultArr.Columns.Add("包装量");
                    objResultArr.Columns.Add("缺药");
                    objResultArr.Columns.Add("麻醉药品");
                    objResultArr.Columns.Add("精神药品");
                    objResultArr.Columns.Add("贵重药品");
                    objResultArr.Columns.Add("院内制剂");
                    objResultArr.Columns.Add("进口药品");
                    objResultArr.Columns.Add("自费药品");
                    objResultArr.Columns.Add("药品代码");
                    objResultArr.Columns.Add("五笔码");
                    objResultArr.Columns.Add("拼音码");
                    objResultArr.Columns.Add("药品类型ID");
                    objResultArr.Columns.Add("制剂类型ID");
                    objResultArr.Columns.Add("MEDICINEID_CHR");
                    objResultArr.Columns.Add("门诊单价");
                    objResultArr.Columns.Add("住院单价");
                    string strFlag = "";
                    if (p_intFlag == 0)
                    {
                        strFlag = "NOQTYFLAG_INT";
                    }
                    else
                    {
                        strFlag = "IPNOQTYFLAG_INT";
                    }
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        DataRow objRow = objResultArr.NewRow();
                        objRow["MEDICINEID_CHR"] = dtbResult.Rows[i1]["MEDICINEID_CHR"];
                        objRow["药品通用名"] = dtbResult.Rows[i1]["MEDNORMALNAME_VCHR"];
                        objRow["助记码"] = dtbResult.Rows[i1]["ASSISTCODE_CHR"];
                        objRow["药品名称"] = dtbResult.Rows[i1]["MEDICINENAME_VCHR"];
                        objRow["英文名"] = dtbResult.Rows[i1]["MEDICINEENGNAME_VCHR"];
                        objRow["类别"] = dtbResult.Rows[i1]["MEDICINETYPENAME_VCHR"];
                        objRow["制剂"] = dtbResult.Rows[i1]["MEDICINEPREPTYPENAME_VCHR"];
                        objRow["规格"] = dtbResult.Rows[i1]["MEDSPEC_VCHR"];
                        if (dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim() != string.Empty)
                        {
                            objRow["批发价"] = dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString();
                        }
                        else
                        {
                            objRow["批发价"] = "0";
                        }
                        if (dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString().Trim() != string.Empty)
                        {
                            objRow["单价"] = dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString();
                        }
                        else
                        {
                            objRow["单价"] = "0";
                        }
                        objRow["生产厂家"] = dtbResult.Rows[i1]["PRODUCTORID_CHR"];
                        objRow["剂量"] = dtbResult.Rows[i1]["DOSAGE_DEC"];
                        objRow["剂量单位"] = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"];
                        objRow["门诊单位"] = dtbResult.Rows[i1]["门诊单位"];
                        objRow["住院单位"] = dtbResult.Rows[i1]["住院单位"];
                        objRow["包装量"] = dtbResult.Rows[i1]["PACKQTY_DEC"];
                        objRow["五笔码"] = dtbResult.Rows[i1]["WBCODE_CHR"];
                        objRow["拼音码"] = dtbResult.Rows[i1]["PYCODE_CHR"];
                        objRow["药品类型ID"] = dtbResult.Rows[i1]["MEDICINETYPEID_CHR"];
                        objRow["制剂类型ID"] = dtbResult.Rows[i1]["MEDICINEPREPTYPE_CHR"];
                        if (dtbResult.Rows[i1]["门诊单价"].ToString().Trim() != string.Empty)
                        {
                            objRow["门诊单价"] = dtbResult.Rows[i1]["门诊单价"].ToString();
                        }
                        else
                        {
                            objRow["门诊单价"] = "0";
                        }
                        if (dtbResult.Rows[i1]["住院单价"].ToString().Trim() != string.Empty)
                        {
                            objRow["住院单价"] = dtbResult.Rows[i1]["住院单价"].ToString();
                        }
                        else
                        {
                            objRow["住院单价"] = "0";
                        }
                        if (dtbResult.Rows[i1][strFlag].ToString() == "1")
                        {
                            objRow["缺药"] = "√";
                        }
                        else
                        {
                            objRow["缺药"] = "×";
                        }

                        if (dtbResult.Rows[i1]["ISANAESTHESIA_CHR"].ToString() == "T")
                        {
                            objRow["麻醉药品"] = "√";
                        }
                        else
                        {
                            objRow["麻醉药品"] = "×";
                        }

                        if (dtbResult.Rows[i1]["ISCHLORPROMAZINE_CHR"].ToString() == "T")
                        {
                            objRow["精神药品"] = "√";
                        }
                        else
                        {
                            objRow["精神药品"] = "×";
                        }

                        if (dtbResult.Rows[i1]["ISCOSTLY_CHR"].ToString() == "T")
                        {
                            objRow["贵重药品"] = "√";
                        }
                        else
                        {
                            objRow["贵重药品"] = "×";
                        }

                        if (dtbResult.Rows[i1]["ISSELF_CHR"].ToString() == "T")
                        {
                            objRow["院内制剂"] = "√";
                        }
                        else
                        {
                            objRow["院内制剂"] = "×";
                        }
                        if (dtbResult.Rows[i1]["ISIMPORT_CHR"].ToString() == "T")
                        {
                            objRow["进口药品"] = "进口";
                        }
                        else if (dtbResult.Rows[i1]["ISIMPORT_CHR"].ToString() == "F")
                        {
                            objRow["进口药品"] = "国产";
                        }
                        else if (dtbResult.Rows[i1]["ISIMPORT_CHR"].ToString() == "H")
                        {
                            objRow["进口药品"] = "合资";
                        }
                        if (dtbResult.Rows[i1]["ISSELFPAY_CHR"].ToString() == "T")
                        {
                            objRow["自费药品"] = "√";
                        }
                        else
                        {
                            objRow["自费药品"] = "×";
                        }
                        objRow["药品代码"] = dtbResult.Rows[i1]["MEDICINEID_CHR"];
                        objResultArr.Rows.Add(objRow);
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

        #region 药品药品的查询结果
        //输出药品信息的查询结果
        [AutoComplete]
        private long m_getResult(string strSQL, out clsMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedicine_VO[0];
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicine_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicine_VO();
                        p_objResultArr[i1].m_strMedicineID = dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMedicineName = dtbResult.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objMedicineType = new clsMedicineType_VO();
                        p_objResultArr[i1].m_objMedicineType.m_strMedicineTypeID = dtbResult.Rows[i1]["MEDICINETYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objMedicineType.m_strMedicineTypeName = dtbResult.Rows[i1]["MEDICINETYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMedSpec = dtbResult.Rows[i1]["MEDSPEC_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objMedicineStd = dtbResult.Rows[i1]["MEDICINESTDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPYCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strWBCode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objMedicinePrepType = new clsMedicinePrepType_VO();
                        p_objResultArr[i1].m_objMedicinePrepType.m_strMedicinePrepTypeID = dtbResult.Rows[i1]["MEDICINEPREPTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objMedicinePrepType.m_strMedicinePrepTypeName = dtbResult.Rows[i1]["MEDICINEPREPTYPENAME_VCHR"].ToString().Trim();

                        p_objResultArr[i1].m_strMedicineEngName = dtbResult.Rows[i1]["MEDICINEENGNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objProduct = new clsVendor_VO();
                        p_objResultArr[i1].m_objProduct.m_strVendorID = dtbResult.Rows[i1]["PRODUCTORID_CHR"].ToString();
                        p_objResultArr[i1].m_objProduct.m_strVendorName = dtbResult.Rows[i1]["VENDORNAME_VCHR"].ToString();
                        p_objResultArr[i1].m_strMedicineEngName = dtbResult.Rows[i1]["MEDICINEENGNAME_VCHR"].ToString();
                        p_objResultArr[i1].m_strIsAnaesthesia = dtbResult.Rows[i1]["ISANAESTHESIA_CHR"].ToString();
                        p_objResultArr[i1].m_strIsChlorpromzine = dtbResult.Rows[i1]["ISCHLORPROMAZINE_CHR"].ToString();
                        p_objResultArr[i1].m_strIsCostly = dtbResult.Rows[i1]["ISCOSTLY_CHR"].ToString();
                        p_objResultArr[i1].m_strIsSelf = dtbResult.Rows[i1]["ISSELF_CHR"].ToString();
                        p_objResultArr[i1].m_strIsImport = dtbResult.Rows[i1]["ISIMPORT_CHR"].ToString();
                        p_objResultArr[i1].m_strIsSelfPay = dtbResult.Rows[i1]["ISSELFPAY_CHR"].ToString();
                        p_objResultArr[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["ASSISTCODE_CHR"].ToString();
                        p_objResultArr[i1].m_strDOSAGEUNIT_CHR = dtbResult.Rows[i1]["DOSAGEUNIT_CHR"].ToString();
                        //						p_objResultArr[i1].m_strDOSAGEUNITNAME_CHR=dtbResult.Rows[i1]["DOSAGEUNAME"].ToString();
                        p_objResultArr[i1].m_strIPUNIT_CHR = dtbResult.Rows[i1]["IPUNIT_CHR"].ToString();
                        //						p_objResultArr[i1].m_strIPUNITNAME_CHR=dtbResult.Rows[i1]["IPUNITNAME"].ToString();
                        p_objResultArr[i1].m_strOPUNIT_CHR = dtbResult.Rows[i1]["OPUNIT_CHR"].ToString();
                        //						p_objResultArr[i1].m_strOPUNITNAME_CHR=dtbResult.Rows[i1]["OPUNITNAME"].ToString();

                        try
                        {
                            p_objResultArr[i1].m_dblPACKQTY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["PACKQTY_DEC"].ToString());
                            p_objResultArr[i1].m_intNOQTYFLAG_INT = Convert.ToInt32(dtbResult.Rows[i1]["NOQTYFLAG_INT"].ToString());
                            p_objResultArr[i1].m_dblDOSAGE_DEC = Convert.ToDouble(dtbResult.Rows[i1]["DOSAGE_DEC"].ToString());
                        }
                        catch
                        {
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

        #region 设置药品是否有货
        /// <summary>
        /// 通过药品ID更改药品当前是否有货
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetStorage(string MedID, int p_ing)
        {
            long lngReg = 0;
            string strSQL = "Update T_BSE_MEDICINE SET NOQTYFLAG_INT=" + p_ing + " WHERE MEDICINEID_CHR='" + MedID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }

        #endregion

        #region 检测药品助记码是否有其它药品在使用
        /// <summary>
        /// 检测药品助记码是否有其它药品在使用
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckIsUse(string helpCode, out DataTable dt)
        {
            dt = new DataTable();
            long lngReg = 0;
            string strSQL = "select MEDICINEID_CHR,MEDICINENAME_VCHR from  T_BSE_MEDICINE where  ASSISTCODE_CHR='" + helpCode + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoGetDataTable(strSQL, ref dt);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }

        #endregion

        #region 药品查询信息
        /// <summary>
        /// 药品查询信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="medType"></param>
        /// <param name="dtbResult"></param>
        /// <param name="p_blSTOP">false,不显示停用药</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMetDgList(string[] medType, out DataTable dtbResult, bool p_blSTOP)
        {
            long lngRes = 0;
            dtbResult = new DataTable();
            string strTypeWhere = " and (";
            if (medType.Length > 0)
            {
                for (int i1 = 0; i1 < medType.Length; i1++)
                {
                    if (i1 == 0)
                        strTypeWhere += "a.MEDICINETYPEID_CHR='" + medType[i1].Trim() + "'";
                    else
                        strTypeWhere += " or a.MEDICINETYPEID_CHR='" + medType[i1].Trim() + "'";
                }
            }
            strTypeWhere += ")";
            string strWhere = "";
            if (p_blSTOP == false)
                strWhere = @" a.IFSTOP_INT=0 and ";
            string strSQL = @"SELECT a.*,h.itemopcode_chr,case when DEPTPREP_INT=1 then '是' else '否' end as DEPTPREP,case when a.ISPOISON_CHR ='T' then '√' when a.ISPOISON_CHR ='F' then '×' end as ISPOISON,case when a.ISCHLORPROMAZINE2_CHR ='T' then '√' when a.ISCHLORPROMAZINE2_CHR ='F' then '×' end as ISCHLORPROMAZINE2,case when a.ISANAESTHESIA_CHR ='T' then '√' when a.ISANAESTHESIA_CHR ='F' then '×' end as ISANAESTHESIA,case when a.ISCHLORPROMAZINE_CHR='T' then '√' when a.ISCHLORPROMAZINE_CHR='F'then '×' end as ISCHLORPROMAZIN,case  when a.ISCOSTLY_CHR='T' then '√' when a.ISCOSTLY_CHR='F' then '×' end as ISCOSTLY,case when a.ISSELF_CHR='T' then '√' when a.ISSELF_CHR='F'then '×' end as ISSELF,case when a.ISIMPORT_CHR='T' then '进口' when a.ISIMPORT_CHR='F' then '国产'
 when a.isimport_chr = 'H' then '合资' end as ISIMPORT,case when a.ISSELFPAY_CHR='T' then '√' when a.ISSELFPAY_CHR ='F' then '×' end as ISSELFPAY,case when a.STANDARD_INT=1 then '√' when a.STANDARD_INT=0 then '×' end as isSTANDARD,case when a.POFLAG_INT=1 then '是' when a.POFLAG_INT=0 then '否' end as POFLAG,b.USAGENAME_VCHR,case when a.OPCHARGEFLG_INT=0 then '基本单位' when a.OPCHARGEFLG_INT=1 then '最小单位' end as OPCHARGEFLG,case when a.IPCHARGEFLG_INT=0 then '基本单位' when a.IPCHARGEFLG_INT=1 then '最小单位' end as IPCHARGEFLG,case when a.IFSTOP_INT=1 then '是(停用)' when a.IFSTOP_INT=0 then '否(正常)' end as IFSTOP,f.MEDICINETYPENAME_VCHR,g.MEDICINEPREPTYPENAME_VCHR,h.ITEMOPCALCTYPE_CHR,h.ITEMIPCALCTYPE_CHR,h.ITEMOPINVTYPE_CHR,h.ITEMIPINVTYPE_CHR,j.TYPENAME_VCHR,v.TYPENAME_VCHR as TYPENAME_VCHR1,h.ITEMBIHCTYPE_CHR,k.PHARMANAME_VCHR,a.PHARMAID_CHR,U.ORDERCATENAME_VCHR,p.FREQNAME_CHR,q.NAME_CHR,case when a.PUTMEDTYPE_INT=0 then '非摆药类' when  a.PUTMEDTYPE_INT=1 then '摆药类'  else '' end as PUTMEDTYPEName, a.standarddate,a.requestpackqty_dec,a.requestunit_chr, a.medbagunit, a.highriskflag, a.isproducedrugs, a.transno, a.varietycode  FROM T_BSE_MEDICINE a,t_bse_usagetype b,T_AID_MEDICINETYPE f,T_AID_MEDICINEPREPTYPE g,t_bse_chargeitem h,T_AID_MEDICARETYPE j,t_bse_pharmatype k,t_aid_bih_orderperformcate U,t_aid_recipefreq p,T_AID_MEDICARETYPE v,T_AID_BIH_ORDERCATE q  WHERE" + strWhere + "  a.MEDICINETYPEID_CHR=f.MEDICINETYPEID_CHR(+) AND a.MEDICINEPREPTYPE_CHR=g.MEDICINEPREPTYPE_CHR(+) and a.USAGEID_CHR=b.USAGEID_CHR(+) " + strTypeWhere + @" and  a.MEDICINEID_CHR=h.ITEMSRCID_VCHR(+) and a.INSURANCETYPE_VCHR=j.TYPEID_CHR(+)  and a.INPINSURANCETYPE_VCHR=v.TYPEID_CHR(+) and a.PHARMAID_CHR=k.PHARMAID_CHR(+) and a.ORDERCATEID_CHR=U.ORDERCATEID_CHR(+) and a.FREQID_CHR=p.FREQID_CHR(+) and a.ORDERCATEID1_CHR=q.ORDERCATEID_CHR(+) ORDER BY a.MEDICINETYPEID_CHR,a.ASSISTCODE_CHR";

            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


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

        #region 新增药品信息数据
        /// <summary>
        /// 新增药品信息数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strType">类型，0代表药库，1代表物资仓库</param>
        /// <param name="SaveRow"></param>
        /// <param name="newID"></param>
        ///  <param name="isInsertIntoChangPrice">是否要插入调价单</param>
        /// <param name="isInsertItem">1,把该药品同时插入到收费项目，0不插入</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveed(string p_strType, DataTable SaveDtb, out string newID, int isInsertItem, string strEmpID, bool isInsertIntoChangPrice, string strStorageID, DataTable p_dtbChargeItem)
        {
            newID = string.Empty;
            if (SaveDtb == null || SaveDtb.Rows.Count != 1)
            {
                return -1;
            }
            DataRow SaveRow = SaveDtb.Rows[0];

            long lngReg = 0;
            newID = "";
            clsRecordMark_VO Markvo = new clsRecordMark_VO();
            //clsRecordMark recordMark = new clsRecordMark();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (string.IsNullOrEmpty(SaveRow["medicineid_chr"].ToString()))
            {
                newID = objHRPSvc.m_strGetNewID("t_bse_medicine", "MEDICINEID_CHR", 10);
            }
            else
            {
                newID = SaveRow["medicineid_chr"].ToString();
            }

            string strSQL = @"insert into t_bse_medicine
  (medicineid_chr,
   medicinename_vchr,
   medicinetypeid_chr,
   medspec_vchr,
   medicinestdid_chr,
   pycode_chr,
   wbcode_chr,
   medicinepreptype_chr,
   isanaesthesia_chr,
   ischlorpromazine_chr,
   iscostly_chr,
   isself_chr,
   isimport_chr,
   isselfpay_chr,
   medicineengname_vchr,
   assistcode_chr,
   dosage_dec,
   dosageunit_chr,
   opunit_chr,
   ipunit_chr,
   packqty_dec,
   tradeprice_mny,
   unitprice_mny,
   mindosage_dec,
   maxdosage_dec,
   productorid_chr,
   poflag_int,
   usageid_chr,
   opchargeflg_int,
   ipchargeflg_int,
   ifstop_int,
   insuranceid_vchr,
   standard_int,
   nmldosage_dec,
   noqtyflag_int,
   ipnoqtyflag_int,
   adultdosage_dec,
   childdosage_dec,
   medicinestdesc_vchr,
   hype_int,
   insurancetype_vchr,
   mednormalname_vchr,
   permitno_vchr,
   pharmaid_chr,
   medicnetype_int,
   ispoison_chr,
   ischlorpromazine2_chr,
   ordercateid_chr,
   limitunitprice_mny,
   freqid_chr,
   inpinsurancetype_vchr,
   ordercateid1_chr,
   putmedtype_int,
   ipunitprice_mny,
   standarddate,requestunit_chr,requestpackqty_dec,
   expenselimit_mny,
   diffprice_mny,
   medbagunit,
   highriskflag,
   isproducedrugs,
   transno,
   varietycode )
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
   ?, ?, ?, ?)";
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(64, out objDPArr);
                objDPArr[0].Value = newID;
                objDPArr[1].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                objDPArr[2].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();
                objDPArr[3].Value = SaveRow["MEDSPEC_VCHR"].ToString();
                objDPArr[4].Value = SaveRow["MEDICINESTDID_CHR"].ToString();
                objDPArr[5].Value = SaveRow["PYCODE_CHR"].ToString();
                objDPArr[6].Value = SaveRow["WBCODE_CHR"].ToString();
                objDPArr[7].Value = SaveRow["MEDICINEPREPTYPE_CHR"].ToString();
                objDPArr[8].Value = SaveRow["ISANAESTHESIA_CHR"].ToString();
                objDPArr[9].Value = SaveRow["ISCHLORPROMAZINE_CHR"].ToString();
                objDPArr[10].Value = SaveRow["ISCOSTLY_CHR"].ToString();
                objDPArr[11].Value = SaveRow["ISSELF_CHR"].ToString();
                objDPArr[12].Value = SaveRow["ISIMPORT_CHR"].ToString();
                objDPArr[13].Value = SaveRow["ISSELFPAY_CHR"].ToString();
                objDPArr[14].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                objDPArr[15].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                objDPArr[16].Value = SaveRow["DOSAGE_DEC"].ToString();
                objDPArr[17].Value = SaveRow["DOSAGEUNIT_CHR"].ToString();
                objDPArr[18].Value = SaveRow["OPUNIT_CHR"].ToString();
                objDPArr[19].Value = SaveRow["IPUNIT_CHR"].ToString();
                objDPArr[20].Value = SaveRow["PACKQTY_DEC"].ToString();
                objDPArr[21].Value = SaveRow["TRADEPRICE_MNY"].ToString();
                objDPArr[22].Value = SaveRow["UNITPRICE_MNY"].ToString();
                objDPArr[23].Value = SaveRow["MINDOSAGE_DEC"].ToString();
                objDPArr[24].Value = SaveRow["MAXDOSAGE_DEC"].ToString();
                objDPArr[25].Value = SaveRow["PRODUCTORID_CHR"].ToString();
                objDPArr[26].Value = SaveRow["POFLAG_INT"].ToString();
                objDPArr[27].Value = SaveRow["USAGEID_CHR"].ToString();
                objDPArr[28].Value = SaveRow["OPCHARGEFLG_INT"].ToString();
                objDPArr[29].Value = SaveRow["IPCHARGEFLG_INT"].ToString();
                objDPArr[30].Value = SaveRow["IFSTOP_INT"].ToString();
                objDPArr[31].Value = SaveRow["INSURANCEID_VCHR"].ToString();
                objDPArr[32].Value = SaveRow["STANDARD_INT"].ToString();
                objDPArr[33].Value = SaveRow["NMLDOSAGE_DEC"].ToString();
                objDPArr[34].Value = 1;
                objDPArr[35].Value = 1;
                objDPArr[36].Value = SaveRow["ADULTDOSAGE_DEC"].ToString();
                objDPArr[37].Value = SaveRow["CHILDDOSAGE_DEC"].ToString();
                objDPArr[38].Value = SaveRow["MEDICINESTDESC_VCHR"].ToString();
                objDPArr[39].Value = SaveRow["HYPE_INT"].ToString();
                objDPArr[40].Value = SaveRow["INSURANCETYPE_VCHR"].ToString();
                objDPArr[41].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                objDPArr[42].Value = SaveRow["PERMITNO_VCHR"].ToString();
                objDPArr[43].Value = SaveRow["pharmaid_chr"].ToString();
                objDPArr[44].Value = SaveRow["MEDICNETYPE_INT"].ToString();
                objDPArr[45].Value = SaveRow["ISPOISON_CHR"].ToString();
                objDPArr[46].Value = SaveRow["ISCHLORPROMAZINE2_CHR"].ToString();
                objDPArr[47].Value = SaveRow["ORDERCATEID_CHR"].ToString();
                objDPArr[48].Value = SaveRow["LIMITUNITPRICE_MNY"].ToString();
                objDPArr[49].Value = SaveRow["FREQID_CHR"].ToString();
                objDPArr[50].Value = SaveRow["INPINSURANCETYPE_VCHR"].ToString();
                objDPArr[51].Value = SaveRow["ORDERCATEID1_CHR"].ToString();
                objDPArr[52].Value = SaveRow["PUTMEDTYPE_INT"].ToString();
                objDPArr[53].Value = SaveRow["IPUNITPRICE_MNY"].ToString();
                objDPArr[54].Value = SaveRow["STANDARDDATE"].ToString();
                objDPArr[55].Value = SaveRow["REQUESTUNIT_CHR"].ToString();
                objDPArr[56].Value = SaveRow["REQUESTPACKQTY_DEC"].ToString();
                objDPArr[57].Value = SaveRow["EXPENSELIMIT_MNY"].ToString();
                objDPArr[58].Value = SaveRow["DIFFPRICE_MNY"].ToString();//Added by: 吴汉明 2014-12-9 药品让利
                objDPArr[59].Value = SaveRow["medbagunit"].ToString();
                objDPArr[60].Value = SaveRow["highriskflag"].ToString();
                objDPArr[61].Value = SaveRow["isproducedrugs"].ToString();
                objDPArr[62].Value = SaveRow["transno"].ToString();
                objDPArr[63].Value = SaveRow["varietycode"].ToString();
                long lngEff = -1;
                lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                #region 写入痕迹记录
                string strSQLForMark = @"insert into t_bse_medicine(MEDICINEID_CHR,MEDICINENAME_VCHR,MEDICINETYPEID_CHR,MEDSPEC_VCHR,MEDICINESTDID_CHR,PYCODE_CHR,WBCODE_CHR,MEDICINEPREPTYPE_CHR,ISANAESTHESIA_CHR,ISCHLORPROMAZINE_CHR,ISCOSTLY_CHR,ISSELF_CHR,ISIMPORT_CHR,ISSELFPAY_CHR,MEDICINEENGNAME_VCHR,ASSISTCODE_CHR,DOSAGE_DEC,DOSAGEUNIT_CHR,OPUNIT_CHR,IPUNIT_CHR,PACKQTY_DEC,TRADEPRICE_MNY,UNITPRICE_MNY,MINDOSAGE_DEC,MAXDOSAGE_DEC,PRODUCTORID_CHR,POFLAG_INT,USAGEID_CHR,OPCHARGEFLG_INT,IPCHARGEFLG_INT,IFSTOP_INT,INSURANCEID_VCHR,STANDARD_INT,NMLDOSAGE_DEC,NOQTYFLAG_INT,IPNOQTYFLAG_INT,ADULTDOSAGE_DEC,CHILDDOSAGE_DEC,MEDICINESTDESC_VCHR,HYPE_INT,INSURANCETYPE_VCHR,MEDNORMALNAME_VCHR,PERMITNO_VCHR,pharmaid_chr,MEDICNETYPE_INT,ISPOISON_CHR,ISCHLORPROMAZINE2_CHR,ORDERCATEID_CHR,LIMITUNITPRICE_MNY,FREQID_CHR,INPINSURANCETYPE_VCHR,ORDERCATEID1_CHR,PUTMEDTYPE_INT,standarddate,REQUESTUNIT_CHR,REQUESTPACKQTY_DEC,DIFFPRICE_MNY, medbagunit, highriskflag, isproducedrugs, transno, varietycode) values ('" + newID + "','" + SaveRow["MEDICINENAME_VCHR"].ToString() + "','" + SaveRow["MEDICINETYPEID_CHR"].ToString() + "','" + SaveRow["MEDSPEC_VCHR"].ToString() + "','" + SaveRow["MEDICINESTDID_CHR"].ToString() + "','" + SaveRow["PYCODE_CHR"].ToString() + "','" + SaveRow["WBCODE_CHR"].ToString() + "','" + SaveRow["MEDICINEPREPTYPE_CHR"].ToString() + "','" + SaveRow["ISANAESTHESIA_CHR"].ToString() + "','" + SaveRow["ISCHLORPROMAZINE_CHR"].ToString() + "','" + SaveRow["ISCOSTLY_CHR"].ToString() + "','" + SaveRow["ISSELF_CHR"].ToString() + "','" + SaveRow["ISIMPORT_CHR"].ToString() + "','" + SaveRow["ISSELFPAY_CHR"].ToString() + "','" + SaveRow["MEDICINEENGNAME_VCHR"].ToString() + "','" + SaveRow["ASSISTCODE_CHR"].ToString() + "'," + SaveRow["DOSAGE_DEC"].ToString() + ",'" + SaveRow["DOSAGEUNIT_CHR"].ToString() + "','" + SaveRow["OPUNIT_CHR"].ToString() + "','" + SaveRow["IPUNIT_CHR"].ToString() + "'," + SaveRow["PACKQTY_DEC"].ToString() + "," + SaveRow["TRADEPRICE_MNY"].ToString() + "," + SaveRow["UNITPRICE_MNY"].ToString() + "," + SaveRow["MINDOSAGE_DEC"].ToString() + "," + SaveRow["MAXDOSAGE_DEC"].ToString() + ",'" + SaveRow["PRODUCTORID_CHR"].ToString() + "'," + SaveRow["POFLAG_INT"].ToString() + ",'" + SaveRow["USAGEID_CHR"].ToString() + "'," + SaveRow["OPCHARGEFLG_INT"].ToString() + "," + SaveRow["IPCHARGEFLG_INT"] + "," + SaveRow["IFSTOP_INT"].ToString();
                strSQLForMark += @",'" + SaveRow["INSURANCEID_VCHR"].ToString() + "'," + SaveRow["STANDARD_INT"].ToString() + ",'" + SaveRow["NMLDOSAGE_DEC"].ToString() + "',1,1," + SaveRow["ADULTDOSAGE_DEC"].ToString() + "," + SaveRow["CHILDDOSAGE_DEC"].ToString() + ",'" + SaveRow["MEDICINESTDESC_VCHR"].ToString() + "'," + SaveRow["HYPE_INT"].ToString() + ",'" + SaveRow["INSURANCETYPE_VCHR"].ToString() + "','" + SaveRow["MEDNORMALNAME_VCHR"].ToString() + "','" + SaveRow["PERMITNO_VCHR"].ToString() + "','" + SaveRow["pharmaid_chr"].ToString() + "'," + SaveRow["MEDICNETYPE_INT"].ToString() + ",'" + SaveRow["ISPOISON_CHR"].ToString() + "','" + SaveRow["ISCHLORPROMAZINE2_CHR"].ToString() + "','" + SaveRow["ORDERCATEID_CHR"].ToString() + "'," + SaveRow["LIMITUNITPRICE_MNY"].ToString() + ",'" + SaveRow["FREQID_CHR"].ToString() + "','" + SaveRow["INPINSURANCETYPE_VCHR"].ToString() + "','" + SaveRow["ORDERCATEID1_CHR"].ToString() + "','" + SaveRow["PUTMEDTYPE_INT"].ToString() + "','" + SaveRow["STANDARDDATE"].ToString() + "','" + SaveRow["REQUESTUNIT_CHR"].ToString() + "','" + SaveRow["REQUESTPACKQTY_DEC"].ToString() + "','" + SaveRow["DIFFPRICE_MNY"].ToString() + "','" + SaveRow["medbagunit"].ToString() + "', " + SaveRow["highriskflag"].ToString() + ", " + SaveRow["isproducedrugs"].ToString() + ", '" + SaveRow["transno"].ToString() + "', '" + SaveRow["varietycode"].ToString() + "')";

                Markvo.m_strOPERATORID_CHR = strEmpID;
                Markvo.m_strTABLESEQID_CHR = "1";
                Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                //recordMark.m_mthAddNewRecord(Markvo);
                #endregion

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (strStorageID.Trim() != "" && p_strType == "0")
            {
                strSQL = @"insert into t_bse_storageandmedicine(storageid_chr,medicineid_chr) values (?,?)";
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = strStorageID;
                    objDPArr[1].Value = newID;

                    long lngEff = -1;
                    lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else if (strStorageID.Trim() != "" && p_strType == "1")
            {
                strSQL = @"insert into t_bse_storageandmaterial(storageid_chr,materialid_chr) values (?,?)";
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = strStorageID;
                    objDPArr[1].Value = newID;

                    long lngEff = -1;
                    lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }

            if (isInsertItem == 1)
            {
                if (isInsertIntoChangPrice == true)
                {
                    clsPriceChgeAppl objApplAuto = new clsPriceChgeAppl();
                    clsPriceChgeApplDe[] objArrDe = new clsPriceChgeApplDe[1];
                    string strDate = DateTime.Now.ToString("yyyyMMdd");
                    strSQL = @"select max(medicinepricechgapplno_chr) from t_opr_medicinepricechgappl where medicinepricechgapplno_chr like ?";
                    DataTable dt = new DataTable();
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = strDate + "%";

                        lngReg = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            double maxNO = double.Parse(dt.Rows[0][0].ToString()) + 1;
                            objApplAuto.m_strMEDICINEPRICECHGAPPLNO_CHR = maxNO.ToString();
                        }
                        catch
                        {
                            objApplAuto.m_strMEDICINEPRICECHGAPPLNO_CHR = strDate + "0001";
                        }

                    }
                    else
                    {
                        objApplAuto.m_strMEDICINEPRICECHGAPPLNO_CHR = strDate + "0001";
                    }
                    objApplAuto.m_strCREATEDATE_DAT = DateTime.Now.ToString();
                    objApplAuto.m_strMEMO_VCHR = "药品基本信息维护界面自动添加的调价单";
                    objApplAuto.m_intPSTATUS_INT = 1;
                    strSQL = @"select periodid_chr,startdate_dat,enddate_dat  from t_bse_period";
                    DataTable dt1 = new DataTable();
                    try
                    {
                        lngReg = objHRPSvc.DoGetDataTable(strSQL, ref dt1);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (dt1.Rows.Count > 0)
                    {
                        for (int k1 = 0; k1 < dt1.Rows.Count; k1++)
                        {
                            if (DateTime.Parse(dt1.Rows[k1]["STARTDATE_DAT"].ToString()) <= DateTime.Now && DateTime.Parse(dt1.Rows[k1]["ENDDATE_DAT"].ToString()) >= DateTime.Now)
                            {
                                objApplAuto.m_strPERIODID_CHR = dt1.Rows[k1]["PERIODID_CHR"].ToString();
                                break;
                            }
                        }

                    }
                    else
                    {
                        objApplAuto.m_strPERIODID_CHR = "0001";
                    }
                    objApplAuto.m_strCREATORID_CHR = strEmpID;
                    objArrDe[0] = new clsPriceChgeApplDe();
                    objArrDe[0].m_dblCHANGEPRICE_MNY = double.Parse(SaveRow["UNITPRICE_MNY"].ToString());
                    objArrDe[0].m_dblCURPRICE_MNY = 0;
                    objArrDe[0].m_dblODDSDE_MNY = 0;
                    objArrDe[0].m_dblQTY_DEC = 0;
                    objArrDe[0].m_strASSISTCODE_CHR = SaveRow["ASSISTCODE_CHR"].ToString();
                    objArrDe[0].m_strMEDICINEID_CHR = newID;
                    objArrDe[0].m_strtypeid = "01";

                    if (SaveRow["OPCHARGEFLG_INT"].ToString() == "0")
                        objArrDe[0].m_strUNITID_CHR = SaveRow["OPUNIT_CHR"].ToString();
                    else
                        objArrDe[0].m_strUNITID_CHR = SaveRow["IPUNIT_CHR"].ToString();
                    clsChangPrice objPrice = new clsChangPrice();
                    string strID = "";
                    objPrice.m_lngSaveChangPriceData(objApplAuto, objArrDe, out strID);
                }
                strSQL = @"select itemcatid_chr from t_aid_chargemderla where medicinetypeid_chr=?";
                DataTable tbCHARGEMDERLA = new DataTable();
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();

                    lngReg = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref tbCHARGEMDERLA, objDPArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                string strItemType = "";
                if (tbCHARGEMDERLA.Rows.Count > 0)
                {
                    strItemType = tbCHARGEMDERLA.Rows[0]["ITEMCATID_CHR"].ToString();
                }
                if (SaveRow["ISCOSTLY_CHR"].ToString() == "T")
                    SaveRow["ISCOSTLY_CHR"] = "1";
                else
                    SaveRow["ISCOSTLY_CHR"] = "0";
                #region 插入收费项目数据
                string newItemID = objHRPSvc.m_strGetNewID("t_bse_chargeitem", "ITEMID_CHR", 10);
                strSQL = @"insert into t_bse_chargeitem
  (ITEMID_CHR,
   ITEMNAME_VCHR,
   ITEMCODE_VCHR,
   ITEMPYCODE_CHR,
   ITEMWBCODE_CHR,
   ITEMSRCID_VCHR,
   ITEMSRCTYPE_INT,
   ITEMSPEC_VCHR,
   ITEMPRICE_MNY,
   ITEMOPUNIT_CHR,
   ITEMIPUNIT_CHR,
   DOSAGE_DEC,
   DOSAGEUNIT_CHR,
   ISGROUPITEM_INT,
   USAGEID_CHR,
   INSURANCEID_CHR,
   PACKQTY_DEC,
   TRADEPRICE_MNY,
   POFLAG_INT,
   ISRICH_INT,
   OPCHARGEFLG_INT,
   ITEMENGNAME_VCHR,
   IFSTOP_INT,
   PDCAREA_VCHR,
   IPCHARGEFLG_INT,
   ITEMCATID_CHR,
   ITEMOPCALCTYPE_CHR,
   ITEMIPCALCTYPE_CHR,
   ITEMOPINVTYPE_CHR,
   ITEMIPINVTYPE_CHR,
   INSURANCETYPE_VCHR,
   ITEMBIHCTYPE_CHR,
   ITEMCOMMNAME_VCHR,
   ORDERCATEID_CHR,
   FREQID_CHR,
   INPINSURANCETYPE_VCHR,
   ORDERCATEID1_CHR,
   ISSELFPAY_CHR,
   DIFFPRICE_MNY
)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(39, out objDPArr);
                    objDPArr[0].Value = newItemID;
                    objDPArr[1].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                    objDPArr[2].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                    objDPArr[3].Value = SaveRow["PYCODE_CHR"].ToString();
                    objDPArr[4].Value = SaveRow["WBCODE_CHR"].ToString();
                    objDPArr[5].Value = newID;
                    objDPArr[6].Value = 1;
                    objDPArr[7].Value = SaveRow["MEDSPEC_VCHR"].ToString();
                    objDPArr[8].Value = SaveRow["UNITPRICE_MNY"].ToString();
                    objDPArr[9].Value = SaveRow["OPUNIT_CHR"].ToString();
                    objDPArr[10].Value = SaveRow["IPUNIT_CHR"].ToString();
                    objDPArr[11].Value = SaveRow["DOSAGE_DEC"].ToString();
                    objDPArr[12].Value = SaveRow["DOSAGEUNIT_CHR"].ToString();
                    objDPArr[13].Value = 0;
                    objDPArr[14].Value = SaveRow["USAGEID_CHR"].ToString();
                    objDPArr[15].Value = SaveRow["INSURANCEID_VCHR"].ToString();
                    objDPArr[16].Value = SaveRow["PACKQTY_DEC"].ToString();
                    objDPArr[17].Value = SaveRow["TRADEPRICE_MNY"].ToString();
                    objDPArr[18].Value = SaveRow["POFLAG_INT"].ToString();
                    objDPArr[19].Value = SaveRow["ISCOSTLY_CHR"].ToString();
                    objDPArr[20].Value = SaveRow["OPCHARGEFLG_INT"].ToString();
                    objDPArr[21].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                    objDPArr[22].Value = SaveRow["IFSTOP_INT"].ToString();
                    objDPArr[23].Value = SaveRow["PRODUCTORID_CHR"].ToString();
                    objDPArr[24].Value = SaveRow["IPCHARGEFLG_INT"].ToString();
                    objDPArr[25].Value = strItemType;
                    objDPArr[26].Value = SaveRow["ITEMOPCALCTYPE_CHR"].ToString();
                    objDPArr[27].Value = SaveRow["ITEMIPCALCTYPE_CHR"].ToString();
                    objDPArr[28].Value = SaveRow["ITEMOPINVTYPE_CHR"].ToString();
                    objDPArr[29].Value = SaveRow["ITEMIPINVTYPE_CHR"].ToString();
                    objDPArr[30].Value = SaveRow["INSURANCETYPE_VCHR"].ToString();
                    objDPArr[31].Value = SaveRow["ITEMBIHCTYPE_CHR"].ToString();
                    objDPArr[32].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                    objDPArr[33].Value = SaveRow["ORDERCATEID_CHR"].ToString();
                    objDPArr[34].Value = SaveRow["FREQID_CHR"].ToString();
                    objDPArr[35].Value = SaveRow["INPINSURANCETYPE_VCHR"].ToString();
                    objDPArr[36].Value = SaveRow["ORDERCATEID1_CHR"].ToString();
                    objDPArr[37].Value = SaveRow["ISSELFPAY_CHR"].ToString();
                    objDPArr[38].Value = SaveRow["DIFFPRICE_MNY"].ToString();

                    long lngEff = -1;
                    lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    #region 写入痕迹记录
                    string strSQLForMark = @"insert into t_bse_chargeitem(ITEMID_CHR,ITEMNAME_VCHR,ITEMCODE_VCHR,ITEMPYCODE_CHR,ITEMWBCODE_CHR,ITEMSRCID_VCHR,ITEMSRCTYPE_INT,ITEMSPEC_VCHR,ITEMPRICE_MNY,ITEMOPUNIT_CHR,ITEMIPUNIT_CHR,DOSAGE_DEC,DOSAGEUNIT_CHR,ISGROUPITEM_INT,USAGEID_CHR,INSURANCEID_CHR,PACKQTY_DEC,TRADEPRICE_MNY,POFLAG_INT,ISRICH_INT,OPCHARGEFLG_INT,ITEMENGNAME_VCHR,IFSTOP_INT,PDCAREA_VCHR,IPCHARGEFLG_INT,ITEMCATID_CHR,ITEMOPCALCTYPE_CHR,ITEMIPCALCTYPE_CHR,ITEMOPINVTYPE_CHR,ITEMIPINVTYPE_CHR,INSURANCETYPE_VCHR,ITEMBIHCTYPE_CHR,ITEMCOMMNAME_VCHR,ORDERCATEID_CHR,FREQID_CHR,INPINSURANCETYPE_VCHR,ORDERCATEID1_CHR,ISSELFPAY_CHR,DIFFPRICE_MNY) values('" + newItemID + "','" + SaveRow["MEDICINENAME_VCHR"].ToString() + "','" + SaveRow["ASSISTCODE_CHR"].ToString() + "','" + SaveRow["PYCODE_CHR"].ToString() + "','" + SaveRow["WBCODE_CHR"].ToString() + "','" + newID + "',1,'" + SaveRow["MEDSPEC_VCHR"].ToString() + "'," + SaveRow["UNITPRICE_MNY"].ToString() + ",'" + SaveRow["OPUNIT_CHR"].ToString() + "','" + SaveRow["IPUNIT_CHR"].ToString() + "'," + SaveRow["DOSAGE_DEC"].ToString() + ",'" + SaveRow["DOSAGEUNIT_CHR"].ToString() + "',0,'" + SaveRow["USAGEID_CHR"].ToString() + "','" + SaveRow["INSURANCEID_VCHR"].ToString() + "'," + SaveRow["PACKQTY_DEC"].ToString() + "," + SaveRow["TRADEPRICE_MNY"].ToString() + "," + SaveRow["POFLAG_INT"].ToString() + "," + SaveRow["ISCOSTLY_CHR"].ToString() + "," + SaveRow["OPCHARGEFLG_INT"].ToString() + ",'" + SaveRow["MEDICINEENGNAME_VCHR"].ToString() + "'," + SaveRow["IFSTOP_INT"].ToString() + ",'" + SaveRow["PRODUCTORID_CHR"].ToString() + "'," + SaveRow["IPCHARGEFLG_INT"].ToString() + ",'" + strItemType + "','" + SaveRow["ITEMOPCALCTYPE_CHR"].ToString() + "','" + SaveRow["ITEMIPCALCTYPE_CHR"].ToString() + "','" + SaveRow["ITEMOPINVTYPE_CHR"].ToString() + "','" + SaveRow["ITEMIPINVTYPE_CHR"].ToString() + "','" + SaveRow["INSURANCETYPE_VCHR"].ToString() + "','" + SaveRow["ITEMBIHCTYPE_CHR"].ToString() + "','" + SaveRow["MEDNORMALNAME_VCHR"].ToString() + "','" + SaveRow["ORDERCATEID_CHR"].ToString() + "','" + SaveRow["FREQID_CHR"].ToString() + "','" + SaveRow["INPINSURANCETYPE_VCHR"].ToString() + "','" + SaveRow["ORDERCATEID1_CHR"].ToString() + "','" + SaveRow["ISSELFPAY_CHR"].ToString() + "','" + SaveRow["DIFFPRICE_MNY"].ToString() + "')";//Added by: 吴汉明 2014-12-9 药品让利Log
                    Markvo.m_strOPERATORID_CHR = strEmpID;
                    Markvo.m_strTABLESEQID_CHR = "1";
                    Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                    //recordMark.m_mthAddNewRecord(Markvo);
                    #endregion
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                #endregion

                #region 插入诊疗项目数据

                string newbihID = "";
                strSQL = @"select lpad(seq_ORDERDICID.NEXTVAL,10,'0') p_strRecordID   from dual";
                DataTable dtbih = new DataTable();
                try
                {
                    lngReg = objHRPSvc.DoGetDataTable(strSQL, ref dtbih);

                    if (dtbih.Rows.Count > 0)
                    {
                        newbihID = dtbih.Rows[0]["p_strRecordID"].ToString().Trim();
                    }

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                strSQL = @"select a.ordercateid_chr
  from t_bse_chgcatevsordercate a, t_aid_chargemderla b
 where b.itemcatid_chr = a.itemcatid_chr
   and b.medicinetypeid_chr = ?";
                dtbih = new DataTable();
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();

                    lngReg = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbih, objDPArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dtbih.Rows.Count > 0)
                {
                    SaveRow["MEDICINETYPEID_CHR"] = dtbih.Rows[0]["ORDERCATEID_CHR"].ToString().Trim();
                }
                else
                {
                    SaveRow["MEDICINETYPEID_CHR"] = "";
                }
                strSQL = @"insert into t_bse_bih_orderdic
  (orderdicid_chr,
   name_chr,
   des_vchr,
   usercode_chr,
   wbcode_chr,
   pycode_chr,
   ordercateid_chr,
   itemid_chr,
   nullitemdosageunit_chr,
   nullitemdosetypeid_chr,
   engname_vchr,
   commname_vchr,
   status_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?)";
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(13, out objDPArr);
                    objDPArr[0].Value = newbihID;
                    objDPArr[1].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                    objDPArr[2].Value = "药库自己生成";
                    objDPArr[3].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                    objDPArr[4].Value = SaveRow["WBCODE_CHR"].ToString();
                    objDPArr[5].Value = SaveRow["PYCODE_CHR"].ToString();
                    objDPArr[6].Value = SaveRow["ORDERCATEID1_CHR"].ToString();
                    objDPArr[7].Value = newItemID;
                    objDPArr[8].Value = SaveRow["DOSAGEUNIT_CHR"].ToString();
                    objDPArr[9].Value = SaveRow["USAGEID_CHR"].ToString();
                    objDPArr[10].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                    objDPArr[11].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                    objDPArr[12].Value = SaveRow["IFSTOP_INT"].ToString().Trim() == "0" ? 1 : 0;

                    long lngEff = -1;
                    lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    #region 写入痕迹记录
                    string strSQLForMark = @"insert into t_bse_bih_orderdic(ORDERDICID_CHR,NAME_CHR,DES_VCHR,USERCODE_CHR,WBCODE_CHR,PYCODE_CHR,ORDERCATEID_CHR,ITEMID_CHR,NULLITEMDOSAGEUNIT_CHR,NULLITEMDOSETYPEID_CHR,ENGNAME_VCHR,COMMNAME_VCHR,STATUS_INT) values('" + newbihID + "','" + SaveRow["MEDICINENAME_VCHR"].ToString() + "','药库自己生成','" + SaveRow["ASSISTCODE_CHR"].ToString() + "','" + SaveRow["WBCODE_CHR"].ToString() + "','" + SaveRow["PYCODE_CHR"].ToString() + "','" + SaveRow["MEDICINETYPEID_CHR"].ToString() + "','" + newItemID + "','" + SaveRow["DOSAGEUNIT_CHR"].ToString() + "','" + SaveRow["USAGEID_CHR"].ToString() + "','" + SaveRow["MEDICINEENGNAME_VCHR"].ToString() + "','" + SaveRow["MEDNORMALNAME_VCHR"].ToString() + "'," + (SaveRow["IFSTOP_INT"].ToString().Trim() == "0" ? 1 : 0) + ")";

                    Markvo.m_strOPERATORID_CHR = strEmpID;
                    Markvo.m_strTABLESEQID_CHR = "1";
                    Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                    //recordMark.m_mthAddNewRecord(Markvo);
                    #endregion
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                // string newbihDeID = objHRPSvc.m_strGetNewID("t_aid_bih_orderdic_charge", "OCMAPID_CHR", 18);
                string newbihDeID = "";
                strSQL = @"select lpad(seq_ocmapid.NEXTVAL,18,'0') p_strRecordID  from dual";
                DataTable m_objTable = new DataTable();
                try
                {
                    lngReg = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
                    if (m_objTable.Rows.Count > 0)
                    {
                        newbihDeID = m_objTable.Rows[0][0].ToString();
                    }
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }



                strSQL = @"insert into t_aid_bih_orderdic_charge
            (ocmapid_chr, orderdicid_chr, itemid_chr, qty_int, type_int
            ) values(?,?,?,?,?)";
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRPSvc.CreateDatabaseParameter(5, out objDPArr);
                    objDPArr[0].Value = newbihDeID;
                    objDPArr[1].Value = newbihID;
                    objDPArr[2].Value = newItemID;
                    objDPArr[3].Value = 1;
                    objDPArr[4].Value = SaveRow["IPCHARGEFLG_INT"].ToString();

                    long lngEff = -1;
                    lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    #region 写入痕迹记录
                    string strSQLMark = @"INSERT INTO t_aid_bih_orderdic_charge
            (ocmapid_chr, orderdicid_chr, itemid_chr, qty_int, type_int
            ) VALUES('" + newbihDeID + "','" + newbihID + "','" + newItemID + "',1," + SaveRow["IPCHARGEFLG_INT"] + ")";

                    Markvo.m_strOPERATORID_CHR = strEmpID;
                    Markvo.m_strTABLESEQID_CHR = "1";
                    Markvo.m_strRECORDDETAIL_VCHR = strSQLMark;
                    //recordMark.m_mthAddNewRecord(Markvo);
                    #endregion
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                #endregion

                //新增药品时亦可操作比例
                if (p_dtbChargeItem == null || p_dtbChargeItem.Rows.Count == 0)
                {
                    strSQL = @"select 100                decdiscount,
			 b.paytypeid_chr    copayid_chr,
			 b.paytypename_vchr catname
	from t_bse_patientpaytype b
 where b.isusing_num = 1
 order by b.paytypeid_chr";

                    try
                    {
                        lngReg = objHRPSvc.DoGetDataTable(strSQL, ref p_dtbChargeItem);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }
                if (p_dtbChargeItem != null && p_dtbChargeItem.Rows.Count > 0)
                {
                    long lngEff = -1;
                    IDataParameter[] objDPArr = null;
                    for (int k1 = 0; k1 < p_dtbChargeItem.Rows.Count; k1++)
                    {
                        strSQL = @"insert into t_aid_inschargeitem(precent_dec,itemid_chr,copayid_chr) values(?,?,?)";
                        try
                        {
                            objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                            objDPArr[0].Value = p_dtbChargeItem.Rows[k1]["decdiscount"].ToString(); ;
                            objDPArr[1].Value = newItemID;
                            objDPArr[2].Value = p_dtbChargeItem.Rows[k1]["copayid_chr"].ToString();

                            lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }
                    }
                }

            }
            return lngReg;
        }
        #endregion

        #region 检查该药品是否已经同收费项目同步
        /// <summary>
        /// 检查该药品是否已经同收费项目同步
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strMedid"></param>
        /// <param name="stritemID">如果存在还回收费项目ID，不存在返回NULL</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetItemID(string strMedid, out string stritemID)
        {
            long lngRes = 0;
            stritemID = null;
            string strSQL = @"select ITEMID_CHR from T_BSE_CHARGEITEM where ITEMSRCID_VCHR='" + strMedid + "'";
            DataTable dtbResult = new DataTable();
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                stritemID = dtbResult.Rows[0]["ITEMID_CHR"].ToString();
            }
            return lngRes;
        }
        #endregion


        #region 获取医保类型
        /// <summary>
        /// 获取医保类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtMEDICARETYPE">返回医保类型数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMEDICARETYPE(out DataTable dtMEDICARETYPE)
        {
            long lngRes = 0;
            dtMEDICARETYPE = null;
            string strSQL = @"select * from T_AID_MEDICARETYPE";
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtMEDICARETYPE);

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

        #region 修改药品信息
        /// <summary>
        /// 修改药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="SaveRow"></param>
        /// <param name="isInsertItem">1,把该药品同时插入到收费项目，0不插入</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModify(DataTable dtbSave, int isInsertItem, string strEmpID)
        {
            if (dtbSave == null || dtbSave.Rows.Count != 1)
            {
                return -1;
            }
            DataRow SaveRow = dtbSave.Rows[0];
            long lngReg = 0;
            clsRecordMark_VO Markvo = new clsRecordMark_VO();
            //clsRecordMark recordMark = new clsRecordMark();

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
            string strSQL = @"update t_bse_medicine
   set medicinename_vchr     = ?,
       medicinetypeid_chr    = ?,
       medspec_vchr          = ?,
       medicinestdid_chr     = ?,
       pycode_chr            = ?,
       wbcode_chr            = ?,
       medicinepreptype_chr  = ?,
       isanaesthesia_chr     = ?,
       ischlorpromazine_chr  = ?,
       iscostly_chr          = ?,
       isself_chr            = ?,
       isimport_chr          = ?,
       isselfpay_chr         = ?,
       medicineengname_vchr  = ?,
       assistcode_chr        = ?,
       dosage_dec            = ?,
       dosageunit_chr        = ?,
       opunit_chr            = ?,
       ipunit_chr            = ?,
       packqty_dec           = ?,
       tradeprice_mny        = ?,
       unitprice_mny         = ?,
       mindosage_dec         = ?,
       maxdosage_dec         = ?,
       productorid_chr       = ?,
       poflag_int            = ?,
       usageid_chr           = ?,
       opchargeflg_int       = ?,
       ipchargeflg_int       = ?,
       ifstop_int            = ?,
       insuranceid_vchr      = ?,
       standard_int          = ?,
       nmldosage_dec         = ?,
       adultdosage_dec       = ?,
       childdosage_dec       = ?,
       medicinestdesc_vchr   = ?,
       hype_int              = ?,
       insurancetype_vchr    = ?,
       mednormalname_vchr    = ?,
       permitno_vchr         = ?,
       pharmaid_chr          = ?,
       medicnetype_int       = ?,
       ispoison_chr          = ?,
       ischlorpromazine2_chr = ?,
       deptprep_int          = ?,
       ordercateid_chr       = ?,
       limitunitprice_mny    = ?,
       freqid_chr            = ?,
       inpinsurancetype_vchr = ?,
       ordercateid1_chr      = ?,
       putmedtype_int        = ?,
       ipunitprice_mny       = ?,
       standarddate          = ?,
       requestunit_chr       = ?,
       requestpackqty_dec    = ?,
       expenselimit_mny      = ?,
       diffprice_mny      = ?,
       medbagunit            = ?,
       highriskflag         = ?,
       isproducedrugs       = ?,
       transno              = ?,
       varietycode          = ?    
 where medicineid_chr = ?";

            #region 写入痕迹记录
            string strSQLForMark = @"update t_bse_medicine set MEDICINENAME_VCHR='" + SaveRow["MEDICINENAME_VCHR"].ToString() + "',MEDICINETYPEID_CHR='" + SaveRow["MEDICINETYPEID_CHR"].ToString() + "',MEDSPEC_VCHR='" + SaveRow["MEDSPEC_VCHR"].ToString() + "',MEDICINESTDID_CHR='" + SaveRow["MEDICINESTDID_CHR"].ToString() + "',PYCODE_CHR='" + SaveRow["PYCODE_CHR"].ToString() + "',WBCODE_CHR='" + SaveRow["WBCODE_CHR"].ToString() + "',MEDICINEPREPTYPE_CHR='" + SaveRow["MEDICINEPREPTYPE_CHR"].ToString() + "',ISANAESTHESIA_CHR='" + SaveRow["ISANAESTHESIA_CHR"].ToString() + "',ISCHLORPROMAZINE_CHR='" + SaveRow["ISCHLORPROMAZINE_CHR"].ToString() + "',ISCOSTLY_CHR='" + SaveRow["ISCOSTLY_CHR"].ToString() + "',ISSELF_CHR='" + SaveRow["ISSELF_CHR"].ToString() + "',ISIMPORT_CHR='" + SaveRow["ISIMPORT_CHR"].ToString() + "',ISSELFPAY_CHR='" + SaveRow["ISSELFPAY_CHR"].ToString() + "',MEDICINEENGNAME_VCHR='" + SaveRow["MEDICINEENGNAME_VCHR"].ToString() + "',ASSISTCODE_CHR='" + SaveRow["ASSISTCODE_CHR"].ToString() + "',DOSAGE_DEC=" + SaveRow["DOSAGE_DEC"].ToString() + ",DOSAGEUNIT_CHR='" + SaveRow["DOSAGEUNIT_CHR"].ToString() + "',OPUNIT_CHR='" + SaveRow["OPUNIT_CHR"].ToString() + "',IPUNIT_CHR='" + SaveRow["IPUNIT_CHR"].ToString() + "',PACKQTY_DEC=" + SaveRow["PACKQTY_DEC"].ToString() + ",TRADEPRICE_MNY=" + SaveRow["TRADEPRICE_MNY"].ToString() + ",UNITPRICE_MNY=" + SaveRow["UNITPRICE_MNY"].ToString() + ",MINDOSAGE_DEC=" + SaveRow["MINDOSAGE_DEC"].ToString() + ",MAXDOSAGE_DEC=" + SaveRow["MAXDOSAGE_DEC"].ToString() + ",PRODUCTORID_CHR='" + SaveRow["PRODUCTORID_CHR"].ToString() + "',POFLAG_INT=" + SaveRow["POFLAG_INT"].ToString() + ",USAGEID_CHR='" + SaveRow["USAGEID_CHR"].ToString() + "',OPCHARGEFLG_INT=" + SaveRow["OPCHARGEFLG_INT"].ToString() + ",IPCHARGEFLG_INT=" + SaveRow["IPCHARGEFLG_INT"].ToString() + ",IFSTOP_INT=" + SaveRow["IFSTOP_INT"].ToString() + ",INSURANCEID_VCHR='" + SaveRow["INSURANCEID_VCHR"].ToString() + "',STANDARD_INT=" + SaveRow["STANDARD_INT"].ToString() + ",NMLDOSAGE_DEC='" + SaveRow["NMLDOSAGE_DEC"].ToString() + "',ADULTDOSAGE_DEC=" + SaveRow["ADULTDOSAGE_DEC"].ToString() + ",";
            strSQLForMark += @"CHILDDOSAGE_DEC=" + SaveRow["CHILDDOSAGE_DEC"].ToString() + ",MEDICINESTDESC_VCHR='" + SaveRow["MEDICINESTDESC_VCHR"].ToString() + "',HYPE_INT=" + SaveRow["HYPE_INT"].ToString() + ",INSURANCETYPE_VCHR='" + SaveRow["INSURANCETYPE_VCHR"].ToString() + "',MEDNORMALNAME_VCHR='" + SaveRow["MEDNORMALNAME_VCHR"].ToString() + "',PERMITNO_VCHR='" + SaveRow["PERMITNO_VCHR"].ToString() + "',pharmaid_chr='" + SaveRow["pharmaid_chr"].ToString() + "',MEDICNETYPE_INT=" + SaveRow["MEDICNETYPE_INT"].ToString() + ", ISPOISON_CHR='" + SaveRow["ISPOISON_CHR"].ToString() + "',ISCHLORPROMAZINE2_CHR='" + SaveRow["ISCHLORPROMAZINE2_CHR"].ToString() + "',DEPTPREP_INT=" + SaveRow["DEPTPREP_INT"].ToString() + ",ORDERCATEID_CHR='" + SaveRow["ORDERCATEID_CHR"].ToString() + "',LIMITUNITPRICE_MNY='" + SaveRow["LIMITUNITPRICE_MNY"].ToString() + "',FREQID_CHR='" + SaveRow["FREQID_CHR"].ToString() + "',INPINSURANCETYPE_VCHR='" + SaveRow["INPINSURANCETYPE_VCHR"].ToString() + "',ORDERCATEID1_CHR='" + SaveRow["ORDERCATEID1_CHR"].ToString() + "',PUTMEDTYPE_INT='" + SaveRow["PUTMEDTYPE_INT"].ToString() + "',standarddate='" + SaveRow["STANDARDDATE"].ToString() + "',REQUESTUNIT_CHR='" + SaveRow["REQUESTUNIT_CHR"].ToString() + "',REQUESTPACKQTY_DEC='" + SaveRow["REQUESTPACKQTY_DEC"].ToString() + "',DIFFPRICE_MNY='" + SaveRow["DIFFPRICE_MNY"].ToString() + "' where MEDICINEID_CHR='" + SaveRow["MEDICINEID_CHR"].ToString() + "'";//Added by: 吴汉明 2014-12-9 修改LOG

            Markvo.m_strOPERATORID_CHR = strEmpID;
            Markvo.m_strTABLESEQID_CHR = "1";
            Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
            //recordMark.m_mthAddNewRecord(Markvo);
            #endregion
            try
            {
                IDataParameter[] objDPArr = null;
                objHRP.CreateDatabaseParameter(63, out objDPArr);
                objDPArr[0].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                objDPArr[1].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();
                objDPArr[2].Value = SaveRow["MEDSPEC_VCHR"].ToString();
                objDPArr[3].Value = SaveRow["MEDICINESTDID_CHR"].ToString();
                objDPArr[4].Value = SaveRow["PYCODE_CHR"].ToString();
                objDPArr[5].Value = SaveRow["WBCODE_CHR"].ToString();
                objDPArr[6].Value = SaveRow["MEDICINEPREPTYPE_CHR"].ToString();
                objDPArr[7].Value = SaveRow["ISANAESTHESIA_CHR"].ToString();
                objDPArr[8].Value = SaveRow["ISCHLORPROMAZINE_CHR"].ToString();
                objDPArr[9].Value = SaveRow["ISCOSTLY_CHR"].ToString();
                objDPArr[10].Value = SaveRow["ISSELF_CHR"].ToString();
                objDPArr[11].Value = SaveRow["ISIMPORT_CHR"].ToString();
                objDPArr[12].Value = SaveRow["ISSELFPAY_CHR"].ToString();
                objDPArr[13].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                objDPArr[14].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                objDPArr[15].Value = SaveRow["DOSAGE_DEC"].ToString();
                objDPArr[16].Value = SaveRow["DOSAGEUNIT_CHR"].ToString();
                objDPArr[17].Value = SaveRow["OPUNIT_CHR"].ToString();
                objDPArr[18].Value = SaveRow["IPUNIT_CHR"].ToString();
                objDPArr[19].Value = SaveRow["PACKQTY_DEC"].ToString();
                objDPArr[20].Value = SaveRow["TRADEPRICE_MNY"].ToString();
                objDPArr[21].Value = SaveRow["UNITPRICE_MNY"].ToString();
                objDPArr[22].Value = SaveRow["MINDOSAGE_DEC"].ToString();
                objDPArr[23].Value = SaveRow["MAXDOSAGE_DEC"].ToString();
                objDPArr[24].Value = SaveRow["PRODUCTORID_CHR"].ToString();
                objDPArr[25].Value = SaveRow["POFLAG_INT"].ToString();
                objDPArr[26].Value = SaveRow["USAGEID_CHR"].ToString();
                objDPArr[27].Value = SaveRow["OPCHARGEFLG_INT"].ToString();
                objDPArr[28].Value = SaveRow["IPCHARGEFLG_INT"].ToString();
                objDPArr[29].Value = SaveRow["IFSTOP_INT"].ToString();
                objDPArr[30].Value = SaveRow["INSURANCEID_VCHR"].ToString();
                objDPArr[31].Value = SaveRow["STANDARD_INT"].ToString();
                objDPArr[32].Value = SaveRow["NMLDOSAGE_DEC"].ToString();
                objDPArr[33].Value = SaveRow["ADULTDOSAGE_DEC"].ToString();
                objDPArr[34].Value = SaveRow["CHILDDOSAGE_DEC"].ToString();
                objDPArr[35].Value = SaveRow["MEDICINESTDESC_VCHR"].ToString();
                objDPArr[36].Value = SaveRow["HYPE_INT"].ToString();
                objDPArr[37].Value = SaveRow["INSURANCETYPE_VCHR"].ToString();
                objDPArr[38].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                objDPArr[39].Value = SaveRow["PERMITNO_VCHR"].ToString();
                objDPArr[40].Value = SaveRow["pharmaid_chr"].ToString();
                objDPArr[41].Value = SaveRow["MEDICNETYPE_INT"].ToString();
                objDPArr[42].Value = SaveRow["ISPOISON_CHR"].ToString();
                objDPArr[43].Value = SaveRow["ISCHLORPROMAZINE2_CHR"].ToString();
                objDPArr[44].Value = SaveRow["DEPTPREP_INT"].ToString();
                objDPArr[45].Value = SaveRow["ORDERCATEID_CHR"].ToString();
                objDPArr[46].Value = SaveRow["LIMITUNITPRICE_MNY"].ToString();
                objDPArr[47].Value = SaveRow["FREQID_CHR"].ToString();
                objDPArr[48].Value = SaveRow["INPINSURANCETYPE_VCHR"].ToString();
                objDPArr[49].Value = SaveRow["ORDERCATEID1_CHR"].ToString();
                objDPArr[50].Value = SaveRow["PUTMEDTYPE_INT"].ToString();
                objDPArr[51].Value = SaveRow["ipunitprice_mny"].ToString();
                objDPArr[52].Value = SaveRow["STANDARDDATE"].ToString();
                objDPArr[53].Value = SaveRow["REQUESTUNIT_CHR"].ToString();
                objDPArr[54].Value = SaveRow["REQUESTPACKQTY_DEC"].ToString();
                objDPArr[55].Value = SaveRow["EXPENSELIMIT_MNY"].ToString();
                objDPArr[56].Value = SaveRow["DIFFPRICE_MNY"].ToString();//Added by: 吴汉明 2014-12-9
                objDPArr[57].Value = SaveRow["medbagunit"].ToString();
                objDPArr[58].Value = SaveRow["highriskflag"].ToString();
                objDPArr[59].Value = SaveRow["isproducedrugs"].ToString();
                objDPArr[60].Value = SaveRow["transno"].ToString();
                objDPArr[61].Value = SaveRow["varietycode"].ToString();
                objDPArr[62].Value = SaveRow["MEDICINEID_CHR"].ToString();

                long lngEff = -1;
                lngReg = objHRP.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                if (lngReg > 0)
                {
                    strSQL = @"insert into t_bse_medicine_history
  (medicineid_chr,
   modifydate_dat,
   modifyuserid_chr,
   packqty_dec,
   tradeprice_mny,
   unitprice_mny,
   ipunitprice_mny,
   opchargeflg_int,
   ipchargeflg_int,
   opunit_chr,
   ipunit_chr)
  select t.medicineid_chr,
         ?,
         ?,
         t.packqty_dec,
         t.tradeprice_mny,
         t.unitprice_mny,
         t.ipunitprice_mny,
         t.opchargeflg_int,
         t.ipchargeflg_int,
         t.opunit_chr,
         t.ipunit_chr
    from t_bse_medicine t
   where t.medicineid_chr = ?";

                    objHRP.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[1].Value = strEmpID;
                    objDPArr[2].Value = SaveRow["MEDICINEID_CHR"].ToString();

                    lngEff = -1;
                    lngReg = objHRP.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (isInsertItem == 1)
            {
                strSQL = @"select itemid_chr from t_bse_chargeitem where  itemsrcid_vchr=? and itemsrctype_int=1";
                DataTable tbIs = new DataTable();
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRP.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = SaveRow["MEDICINEID_CHR"].ToString();

                    lngReg = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref tbIs, objDPArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                if (SaveRow["ISCOSTLY_CHR"].ToString() == "F")
                    SaveRow["ISCOSTLY_CHR"] = "0";
                else
                    SaveRow["ISCOSTLY_CHR"] = "1";

                if (tbIs.Rows.Count == 0)//如果收费项目中没有此记录则插入一件数据
                {
                    strSQL = @"select itemcatid_chr from t_aid_chargemderla where medicinetypeid_chr=?";
                    DataTable tbCHARGEMDERLA = new DataTable();
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();

                        lngReg = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref tbCHARGEMDERLA, objDPArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (tbCHARGEMDERLA.Rows.Count > 0)
                    {
                        SaveRow["MEDICINETYPEID_CHR"] = tbCHARGEMDERLA.Rows[0]["ITEMCATID_CHR"].ToString();
                    }
                    else
                    {
                        SaveRow["MEDICINETYPEID_CHR"] = "";
                    }
                    string newItemID = objHRPSvc.m_strGetNewID("t_bse_chargeitem", "ITEMID_CHR", 10);
                    strSQL = @"insert into t_bse_chargeitem
  (itemid_chr,
   itemname_vchr,
   itemcode_vchr,
   itempycode_chr,
   itemwbcode_chr,
   itemsrcid_vchr,
   itemsrctype_int,
   itemspec_vchr,
   itemprice_mny,
   itemopunit_chr,
   itemipunit_chr,
   dosage_dec,
   dosageunit_chr,
   isgroupitem_int,
   usageid_chr,
   insuranceid_chr,
   packqty_dec,
   tradeprice_mny,
   poflag_int,
   isrich_int,
   opchargeflg_int,
   itemengname_vchr,
   ifstop_int,
   pdcarea_vchr,
   ipchargeflg_int,
   itemcatid_chr,
   insurancetype_vchr,
   itembihctype_chr,
   itemcommname_vchr,
   ordercateid_chr,
   freqid_chr,
   inpinsurancetype_vchr,
   ordercateid1_chr,
   isselfpay_chr,
   diffprice_mny
)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?)";
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPSvc.CreateDatabaseParameter(35, out objDPArr);
                        objDPArr[0].Value = newItemID;
                        objDPArr[1].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                        objDPArr[2].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                        objDPArr[3].Value = SaveRow["PYCODE_CHR"].ToString();
                        objDPArr[4].Value = SaveRow["WBCODE_CHR"].ToString();
                        objDPArr[5].Value = SaveRow["MEDICINEID_CHR"].ToString();
                        objDPArr[6].Value = 1;
                        objDPArr[7].Value = SaveRow["MEDSPEC_VCHR"].ToString();
                        objDPArr[8].Value = SaveRow["UNITPRICE_MNY"].ToString();
                        objDPArr[9].Value = SaveRow["OPUNIT_CHR"].ToString();
                        objDPArr[10].Value = SaveRow["IPUNIT_CHR"].ToString();
                        objDPArr[11].Value = SaveRow["DOSAGE_DEC"].ToString();
                        objDPArr[12].Value = SaveRow["DOSAGEUNIT_CHR"].ToString();
                        objDPArr[13].Value = 0;
                        objDPArr[14].Value = SaveRow["USAGEID_CHR"].ToString();
                        objDPArr[15].Value = SaveRow["INSURANCEID_VCHR"].ToString();
                        objDPArr[16].Value = SaveRow["PACKQTY_DEC"].ToString();
                        objDPArr[17].Value = SaveRow["TRADEPRICE_MNY"].ToString();
                        objDPArr[18].Value = SaveRow["POFLAG_INT"].ToString();
                        objDPArr[19].Value = SaveRow["ISCOSTLY_CHR"].ToString();
                        objDPArr[20].Value = SaveRow["OPCHARGEFLG_INT"].ToString();
                        objDPArr[21].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                        objDPArr[22].Value = SaveRow["IFSTOP_INT"].ToString();
                        objDPArr[23].Value = SaveRow["PRODUCTORID_CHR"].ToString();
                        objDPArr[24].Value = SaveRow["IPCHARGEFLG_INT"].ToString();
                        objDPArr[25].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();
                        objDPArr[26].Value = SaveRow["INSURANCETYPE_VCHR"].ToString();
                        objDPArr[27].Value = SaveRow["ITEMBIHCTYPE_CHR"].ToString();
                        objDPArr[28].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                        objDPArr[29].Value = SaveRow["ORDERCATEID_CHR"].ToString();
                        objDPArr[30].Value = SaveRow["FREQID_CHR"].ToString();
                        objDPArr[31].Value = SaveRow["INPINSURANCETYPE_VCHR"].ToString();
                        objDPArr[32].Value = SaveRow["ORDERCATEID1_CHR"].ToString();
                        objDPArr[33].Value = SaveRow["ISSELFPAY_CHR"].ToString();
                        objDPArr[34].Value = SaveRow["DIFFPRICE_MNY"].ToString();

                        long lngEff = -1;
                        lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                        #region 写入痕迹记录
                        strSQLForMark = @"insert into t_bse_chargeitem(ITEMID_CHR,ITEMNAME_VCHR,ITEMCODE_VCHR,ITEMPYCODE_CHR,ITEMWBCODE_CHR,ITEMSRCID_VCHR,ITEMSRCTYPE_INT,ITEMSPEC_VCHR,ITEMPRICE_MNY,ITEMOPUNIT_CHR,ITEMIPUNIT_CHR,DOSAGE_DEC,DOSAGEUNIT_CHR,ISGROUPITEM_INT,USAGEID_CHR,INSURANCEID_CHR,PACKQTY_DEC,TRADEPRICE_MNY,POFLAG_INT,ISRICH_INT,OPCHARGEFLG_INT,ITEMENGNAME_VCHR,IFSTOP_INT,PDCAREA_VCHR,IPCHARGEFLG_INT,ITEMCATID_CHR,INSURANCETYPE_VCHR,ITEMBIHCTYPE_CHR,ITEMCOMMNAME_VCHR,ORDERCATEID_CHR,FREQID_CHR,INPINSURANCETYPE_VCHR,ORDERCATEID1_CHR,ISSELFPAY_CHR,DIFFPRICE_MNY) values('" + newItemID + "','" + SaveRow["MEDICINENAME_VCHR"].ToString() + "','" + SaveRow["ASSISTCODE_CHR"].ToString() + "','" + SaveRow["PYCODE_CHR"].ToString() + "','" + SaveRow["WBCODE_CHR"].ToString() + "','" + SaveRow["MEDICINEID_CHR"].ToString() + "',1,'" + SaveRow["MEDSPEC_VCHR"].ToString() + "'," + SaveRow["UNITPRICE_MNY"].ToString() + ",'" + SaveRow["OPUNIT_CHR"].ToString() + "','" + SaveRow["IPUNIT_CHR"].ToString() + "'," + SaveRow["DOSAGE_DEC"].ToString() + ",'" + SaveRow["DOSAGEUNIT_CHR"].ToString() + "',0,'" + SaveRow["USAGEID_CHR"].ToString() + "','" + SaveRow["INSURANCEID_VCHR"].ToString() + "'," + SaveRow["PACKQTY_DEC"].ToString() + "," + SaveRow["TRADEPRICE_MNY"].ToString() + "," + SaveRow["POFLAG_INT"].ToString() + "," + SaveRow["ISCOSTLY_CHR"].ToString() + "," + SaveRow["OPCHARGEFLG_INT"].ToString() + ",'" + SaveRow["MEDICINEENGNAME_VCHR"].ToString() + "'," + SaveRow["IFSTOP_INT"].ToString() + ",'" + SaveRow["PRODUCTORID_CHR"].ToString() + "'," + SaveRow["IPCHARGEFLG_INT"].ToString() + ",'" + SaveRow["MEDICINETYPEID_CHR"].ToString() + "','" + SaveRow["INSURANCETYPE_VCHR"].ToString() + "','" + SaveRow["ITEMBIHCTYPE_CHR"].ToString() + "','" + SaveRow["MEDNORMALNAME_VCHR"].ToString() + "','" + SaveRow["ORDERCATEID_CHR"].ToString() + "','" + SaveRow["FREQID_CHR"].ToString() + "','" + SaveRow["INPINSURANCETYPE_VCHR"].ToString() + "','" + SaveRow["ORDERCATEID1_CHR"].ToString() + "','" + SaveRow["ISSELFPAY_CHR"].ToString() + "','" + SaveRow["DIFFPRICE_MNY "].ToString() + "')";
                        Markvo.m_strOPERATORID_CHR = strEmpID;
                        Markvo.m_strTABLESEQID_CHR = "1";
                        Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                        //recordMark.m_mthAddNewRecord(Markvo);
                        #endregion

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    #region 插入诊疗项目数据

                    string newbihID = "";
                    strSQL = @"select lpad(seq_ORDERDICID.NEXTVAL,10,'0') p_strRecordID   from dual";
                    DataTable dtbih = new DataTable();
                    try
                    {
                        lngReg = objHRPSvc.DoGetDataTable(strSQL, ref dtbih);

                        if (dtbih.Rows.Count > 0)
                        {
                            newbihID = dtbih.Rows[0]["p_strRecordID"].ToString().Trim();
                        }

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                    strSQL = @"select a.ordercateid_chr from t_bse_chgcatevsordercate a,t_aid_chargemderla
 b where b.itemcatid_chr=a.itemcatid_chr and b.medicinetypeid_chr=?";
                    dtbih = new DataTable();
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();

                        lngReg = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbih, objDPArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (dtbih.Rows.Count > 0)
                    {
                        SaveRow["MEDICINETYPEID_CHR"] = dtbih.Rows[0]["ORDERCATEID_CHR"].ToString().Trim();
                    }
                    else
                    {
                        SaveRow["MEDICINETYPEID_CHR"] = "";
                    }
                    strSQL = @"insert into t_bse_bih_orderdic
  (orderdicid_chr,
   name_chr,
   des_vchr,
   usercode_chr,
   wbcode_chr,
   pycode_chr,
   ordercateid_chr,
   itemid_chr,
   nullitemdosageunit_chr,
   nullitemdosetypeid_chr,
   engname_vchr,
   commname_vchr,
   status_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?)";
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPSvc.CreateDatabaseParameter(13, out objDPArr);
                        objDPArr[0].Value = newbihID;
                        objDPArr[1].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                        objDPArr[2].Value = "药库自己生成";
                        objDPArr[3].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                        objDPArr[4].Value = SaveRow["WBCODE_CHR"].ToString();
                        objDPArr[5].Value = SaveRow["PYCODE_CHR"].ToString();
                        objDPArr[6].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();
                        objDPArr[7].Value = newItemID;
                        objDPArr[8].Value = SaveRow["DOSAGEUNIT_CHR"].ToString();
                        objDPArr[9].Value = SaveRow["USAGEID_CHR"].ToString();
                        objDPArr[10].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                        objDPArr[11].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                        objDPArr[12].Value = SaveRow["IFSTOP_INT"].ToString().Trim() == "0" ? 1 : 0;

                        long lngEff = -1;
                        lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                        #region 写入痕迹记录
                        strSQLForMark = @"insert into t_bse_bih_orderdic(ORDERDICID_CHR,NAME_CHR,DES_VCHR,USERCODE_CHR,WBCODE_CHR,PYCODE_CHR,ORDERCATEID_CHR,ITEMID_CHR,NULLITEMDOSAGEUNIT_CHR,NULLITEMDOSETYPEID_CHR,ENGNAME_VCHR,COMMNAME_VCHR,STATUS_INT) values('" + newbihID + "','" + SaveRow["MEDICINENAME_VCHR"].ToString() + "','药库自己生成','" + SaveRow["ASSISTCODE_CHR"].ToString() + "','" + SaveRow["WBCODE_CHR"].ToString() + "','" + SaveRow["PYCODE_CHR"].ToString() + "','" + SaveRow["MEDICINETYPEID_CHR"].ToString() + "','" + newItemID + "','" + SaveRow["DOSAGEUNIT_CHR"].ToString() + "','" + SaveRow["USAGEID_CHR"].ToString() + "','" + SaveRow["MEDICINEENGNAME_VCHR"].ToString() + "','" + SaveRow["MEDNORMALNAME_VCHR"].ToString() + "'," + (SaveRow["IFSTOP_INT"].ToString().Trim() == "0" ? 1 : 0) + ")";
                        Markvo.m_strOPERATORID_CHR = strEmpID;
                        Markvo.m_strTABLESEQID_CHR = "1";
                        Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                        //recordMark.m_mthAddNewRecord(Markvo);
                        #endregion
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }

                    // string newbihDeID = objHRPSvc.m_strGetNewID("t_aid_bih_orderdic_charge", "OCMAPID_CHR", 18);
                    string newbihDeID = "";
                    strSQL = @"select lpad(seq_ocmapid.NEXTVAL,18,'0') p_strRecordID  from dual";
                    DataTable m_objTable = new DataTable();
                    try
                    {
                        lngReg = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
                        if (m_objTable.Rows.Count > 0)
                        {
                            newbihDeID = m_objTable.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }



                    strSQL = @"insert into t_aid_bih_orderdic_charge
            (ocmapid_chr, orderdicid_chr, itemid_chr, qty_int, type_int
            ) values(?,?,?,?,?)";
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPSvc.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = newbihDeID;
                        objDPArr[1].Value = newbihID;
                        objDPArr[2].Value = newItemID;
                        objDPArr[3].Value = 1;
                        objDPArr[4].Value = SaveRow["IPCHARGEFLG_INT"].ToString();

                        long lngEff = -1;
                        lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                        #region 写入痕迹记录
                        strSQLForMark = @"INSERT INTO t_aid_bih_orderdic_charge
            (ocmapid_chr, orderdicid_chr, itemid_chr, qty_int, type_int
            ) VALUES('" + newbihDeID + "','" + newbihID + "','" + newItemID + "',1," + SaveRow["IPCHARGEFLG_INT"] + ")";

                        Markvo.m_strOPERATORID_CHR = strEmpID;
                        Markvo.m_strTABLESEQID_CHR = "1";
                        Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                        //recordMark.m_mthAddNewRecord(Markvo);
                        #endregion
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    #endregion

                }
                else//存在修改
                {

                    strSQL = @"update t_bse_chargeitem
   set itemname_vchr         = ?,
       itempycode_chr        = ?,
       itemwbcode_chr        = ?,
       itemspec_vchr         = ?,
       itemprice_mny         = ?,
       itemopunit_chr        = ?,
       itemipunit_chr        = ?,
       usageid_chr           = ?,
       insuranceid_chr       = ?,
       packqty_dec           = ?,
       poflag_int            = ?,
       isrich_int            = ?,
       opchargeflg_int       = ?,
       itemsrcname_vchr      = ?,
       itemengname_vchr      = ?,
       pdcarea_vchr          = ?,
       ipchargeflg_int       = ?,
       itemcode_vchr         = ?,
       tradeprice_mny        = ?,
       dosage_dec            = ?,
       dosageunit_chr        = ?,
       itemopcalctype_chr    = ?,
       itemipcalctype_chr    = ?,
       itemopinvtype_chr     = ?,
       itemipinvtype_chr     = ?,
       ifstop_int            = ?,
       insurancetype_vchr    = ?,
       itembihctype_chr      = ?,
       itemcommname_vchr     = ?,
       ordercateid_chr       = ?,
       freqid_chr            = ?,
       inpinsurancetype_vchr = ?,
       ordercateid1_chr      = ?,
       isselfpay_chr         = ?,
       diffprice_mny         = ?
 where itemsrcid_vchr = ?
   and itemsrctype_int = 1";
                    #region 写入痕迹记录
                    strSQLForMark = @"update t_bse_chargeitem set ITEMNAME_VCHR='" + SaveRow["MEDICINENAME_VCHR"].ToString() + "',ITEMPYCODE_CHR='" + SaveRow["PYCODE_CHR"].ToString() + "',ITEMWBCODE_CHR='" + SaveRow["WBCODE_CHR"].ToString() + "',ITEMSPEC_VCHR='" + SaveRow["MEDSPEC_VCHR"].ToString() + "',ITEMPRICE_MNY=" + SaveRow["UNITPRICE_MNY"].ToString() + ",ITEMOPUNIT_CHR='" + SaveRow["OPUNIT_CHR"].ToString() + "',ITEMIPUNIT_CHR='" + SaveRow["IPUNIT_CHR"].ToString() + "',USAGEID_CHR='" + SaveRow["USAGEID_CHR"].ToString() + "',INSURANCEID_CHR='" + SaveRow["INSURANCEID_VCHR"].ToString() + "',PACKQTY_DEC=" + SaveRow["PACKQTY_DEC"].ToString() + ",POFLAG_INT=" + SaveRow["POFLAG_INT"].ToString() + ",ISRICH_INT=" + SaveRow["ISCOSTLY_CHR"].ToString() + ",OPCHARGEFLG_INT=" + SaveRow["OPCHARGEFLG_INT"].ToString() + ",ITEMSRCNAME_VCHR='" + SaveRow["MEDICINENAME_VCHR"].ToString() + "',ITEMENGNAME_VCHR='" + SaveRow["MEDICINEENGNAME_VCHR"].ToString() + "',PDCAREA_VCHR='" + SaveRow["PRODUCTORID_CHR"].ToString() + "',IPCHARGEFLG_INT=" + SaveRow["IPCHARGEFLG_INT"].ToString() + ",ITEMCODE_VCHR='" + SaveRow["ASSISTCODE_CHR"].ToString() + "',TRADEPRICE_MNY=" + SaveRow["TRADEPRICE_MNY"].ToString() + ",DOSAGE_DEC=" + SaveRow["DOSAGE_DEC"].ToString() + ",DOSAGEUNIT_CHR='" + SaveRow["DOSAGEUNIT_CHR"].ToString() + "',ITEMOPCALCTYPE_CHR='" + SaveRow["ITEMOPCALCTYPE_CHR"].ToString() + "',ITEMIPCALCTYPE_CHR='" + SaveRow["ITEMIPCALCTYPE_CHR"].ToString() + "',ITEMOPINVTYPE_CHR='" + SaveRow["ITEMOPINVTYPE_CHR"].ToString() + "',ITEMIPINVTYPE_CHR='" + SaveRow["ITEMIPINVTYPE_CHR"].ToString() + "',IFSTOP_INT=" + SaveRow["IFSTOP_INT"].ToString() + ",INSURANCETYPE_VCHR='" + SaveRow["INSURANCETYPE_VCHR"].ToString() + "',ITEMBIHCTYPE_CHR='" + SaveRow["ITEMBIHCTYPE_CHR"].ToString() + "',ITEMCOMMNAME_VCHR='" + SaveRow["MEDNORMALNAME_VCHR"].ToString() + "',ORDERCATEID_CHR='" + SaveRow["ORDERCATEID_CHR"].ToString() + "',FREQID_CHR ='" + SaveRow["FREQID_CHR"].ToString() + "',INPINSURANCETYPE_VCHR='" + SaveRow["INPINSURANCETYPE_VCHR"].ToString() + "',ORDERCATEID1_CHR='" + SaveRow["ORDERCATEID1_CHR"].ToString() + "',ISSELFPAY_CHR='" + SaveRow["ISSELFPAY_CHR"].ToString() + "',DIFFPRICE_MNY='" + SaveRow["DIFFPRICE_MNY"].ToString() + "' where ITEMSRCID_VCHR='" + SaveRow["MEDICINEID_CHR"].ToString() + "' and ITEMSRCTYPE_INT=1"; ;
                    Markvo.m_strOPERATORID_CHR = strEmpID;
                    Markvo.m_strTABLESEQID_CHR = "1";
                    Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                    //recordMark.m_mthAddNewRecord(Markvo);
                    #endregion
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRP.CreateDatabaseParameter(36, out objDPArr);
                        objDPArr[0].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                        objDPArr[1].Value = SaveRow["PYCODE_CHR"].ToString();
                        objDPArr[2].Value = SaveRow["WBCODE_CHR"].ToString();
                        objDPArr[3].Value = SaveRow["MEDSPEC_VCHR"].ToString();
                        objDPArr[4].Value = SaveRow["UNITPRICE_MNY"].ToString();
                        objDPArr[5].Value = SaveRow["OPUNIT_CHR"].ToString();
                        objDPArr[6].Value = SaveRow["IPUNIT_CHR"].ToString();
                        objDPArr[7].Value = SaveRow["USAGEID_CHR"].ToString();
                        objDPArr[8].Value = SaveRow["INSURANCEID_VCHR"].ToString();
                        objDPArr[9].Value = SaveRow["PACKQTY_DEC"].ToString();
                        objDPArr[10].Value = SaveRow["POFLAG_INT"].ToString();
                        objDPArr[11].Value = SaveRow["ISCOSTLY_CHR"].ToString();
                        objDPArr[12].Value = SaveRow["OPCHARGEFLG_INT"].ToString();
                        objDPArr[13].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                        objDPArr[14].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                        objDPArr[15].Value = SaveRow["PRODUCTORID_CHR"].ToString();
                        objDPArr[16].Value = SaveRow["IPCHARGEFLG_INT"].ToString();
                        objDPArr[17].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                        objDPArr[18].Value = SaveRow["TRADEPRICE_MNY"].ToString();
                        objDPArr[19].Value = SaveRow["DOSAGE_DEC"].ToString();
                        objDPArr[20].Value = SaveRow["DOSAGEUNIT_CHR"].ToString();
                        objDPArr[21].Value = SaveRow["ITEMOPCALCTYPE_CHR"].ToString();
                        objDPArr[22].Value = SaveRow["ITEMIPCALCTYPE_CHR"].ToString();
                        objDPArr[23].Value = SaveRow["ITEMOPINVTYPE_CHR"].ToString();
                        objDPArr[24].Value = SaveRow["ITEMIPINVTYPE_CHR"].ToString();
                        objDPArr[25].Value = SaveRow["IFSTOP_INT"].ToString();
                        objDPArr[26].Value = SaveRow["INSURANCETYPE_VCHR"].ToString();
                        objDPArr[27].Value = SaveRow["ITEMBIHCTYPE_CHR"].ToString();
                        objDPArr[28].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                        objDPArr[29].Value = SaveRow["ORDERCATEID_CHR"].ToString();
                        objDPArr[30].Value = SaveRow["FREQID_CHR"].ToString();
                        objDPArr[31].Value = SaveRow["INPINSURANCETYPE_VCHR"].ToString();
                        objDPArr[32].Value = SaveRow["ORDERCATEID1_CHR"].ToString();
                        objDPArr[33].Value = SaveRow["ISSELFPAY_CHR"].ToString();
                        objDPArr[34].Value = SaveRow["DIFFPRICE_MNY"].ToString();//Added by: 吴汉明 2014-12-9
                        objDPArr[35].Value = SaveRow["MEDICINEID_CHR"].ToString();

                        long lngEff = -1;
                        lngReg = objHRP.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    strSQL = @"select orderdicid_chr
  from t_bse_bih_orderdic
 where itemid_chr in (select itemid_chr
                        from t_bse_chargeitem
                       where itemsrcid_vchr = ?
                         and itemsrctype_int = 1)";
                    DataTable m_objTable = new DataTable();
                    try
                    {
                        IDataParameter[] objDPArr = null;
                        objHRP.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = SaveRow["MEDICINEID_CHR"].ToString();

                        lngReg = objHRP.lngGetDataTableWithParameters(strSQL, ref m_objTable, objDPArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (lngReg > 0 && m_objTable.Rows.Count > 0)
                    {
                        strSQL = @"update t_bse_bih_orderdic a
   set a.name_chr        = ?,
       a.usercode_chr    = ?,
       a.wbcode_chr      = ?,
       a.pycode_chr      = ?,
       a.ordercateid_chr = ?,
       a.engname_vchr    = ?,
       a.commname_vchr   = ?,
       a.status_int      = ?
 where a.itemid_chr in (select c.itemid_chr
                          from t_bse_medicine b, t_bse_chargeitem c
                         where b.medicineid_chr = c.itemsrcid_vchr
                           and c.itemsrctype_int = 1
                           and b.medicineid_chr = ?)";
                        try
                        {
                            IDataParameter[] objDPArr = null;
                            objHRP.CreateDatabaseParameter(9, out objDPArr);
                            objDPArr[0].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                            objDPArr[1].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                            objDPArr[2].Value = SaveRow["WBCODE_CHR"].ToString();
                            objDPArr[3].Value = SaveRow["PYCODE_CHR"].ToString();
                            objDPArr[4].Value = SaveRow["ORDERCATEID1_CHR"].ToString();
                            objDPArr[5].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                            objDPArr[6].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                            objDPArr[7].Value = SaveRow["IFSTOP_INT"].ToString().Trim() == "0" ? 1 : 0;
                            objDPArr[8].Value = SaveRow["MEDICINEID_CHR"].ToString();

                            long lngEff = -1;
                            lngReg = objHRP.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }
                    }
                    else if (lngReg > 0 && m_objTable.Rows.Count == 0)
                    {
                        for (int i = 0; i < tbIs.Rows.Count; i++)
                        {
                            #region 插入诊疗项目数据

                            string newbihID = "";
                            strSQL = @"select lpad(seq_ORDERDICID.NEXTVAL,10,'0') p_strRecordID   from dual";
                            DataTable dtbih = new DataTable();
                            try
                            {
                                lngReg = objHRPSvc.DoGetDataTable(strSQL, ref dtbih);

                                if (dtbih.Rows.Count > 0)
                                {
                                    newbihID = dtbih.Rows[0]["p_strRecordID"].ToString().Trim();
                                }

                            }
                            catch (Exception objEx)
                            {
                                string strTmp = objEx.Message;
                                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                bool blnRes = objLogger.LogError(objEx);
                            }

                            strSQL = @"select a.ordercateid_chr from t_bse_chgcatevsordercate a,t_aid_chargemderla
 b where b.itemcatid_chr=a.itemcatid_chr and b.medicinetypeid_chr=?";
                            dtbih = new DataTable();
                            try
                            {
                                IDataParameter[] objDPArr = null;
                                objHRP.CreateDatabaseParameter(1, out objDPArr);
                                objDPArr[0].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();

                                lngReg = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbih, objDPArr);

                            }
                            catch (Exception objEx)
                            {
                                string strTmp = objEx.Message;
                                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                bool blnRes = objLogger.LogError(objEx);
                            }
                            if (dtbih.Rows.Count > 0)
                            {
                                SaveRow["MEDICINETYPEID_CHR"] = dtbih.Rows[0]["ORDERCATEID_CHR"].ToString().Trim();
                            }
                            else
                            {
                                SaveRow["MEDICINETYPEID_CHR"] = "";
                            }
                            strSQL = @"insert into t_bse_bih_orderdic
  (orderdicid_chr,
   name_chr,
   des_vchr,
   usercode_chr,
   wbcode_chr,
   pycode_chr,
   ordercateid_chr,
   itemid_chr,
   nullitemdosageunit_chr,
   nullitemdosetypeid_chr,
   engname_vchr,
   commname_vchr,
   status_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
   ?, ?, ?)";
                            try
                            {
                                IDataParameter[] objDPArr = null;
                                objHRP.CreateDatabaseParameter(13, out objDPArr);
                                objDPArr[0].Value = newbihID;
                                objDPArr[1].Value = SaveRow["MEDICINENAME_VCHR"].ToString();
                                objDPArr[2].Value = "药库自己生成";
                                objDPArr[3].Value = SaveRow["ASSISTCODE_CHR"].ToString();
                                objDPArr[4].Value = SaveRow["WBCODE_CHR"].ToString();
                                objDPArr[5].Value = SaveRow["PYCODE_CHR"].ToString();
                                objDPArr[6].Value = SaveRow["MEDICINETYPEID_CHR"].ToString();
                                objDPArr[7].Value = tbIs.Rows[0][0].ToString();
                                objDPArr[8].Value = SaveRow["DOSAGEUNIT_CHR"].ToString();
                                objDPArr[9].Value = SaveRow["USAGEID_CHR"].ToString();
                                objDPArr[10].Value = SaveRow["MEDICINEENGNAME_VCHR"].ToString();
                                objDPArr[11].Value = SaveRow["MEDNORMALNAME_VCHR"].ToString();
                                objDPArr[12].Value = SaveRow["IFSTOP_INT"].ToString().Trim() == "0" ? 1 : 0;

                                long lngEff = -1;
                                lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                                #region 写入痕迹记录
                                strSQLForMark = @"insert into t_bse_bih_orderdic(ORDERDICID_CHR,NAME_CHR,DES_VCHR,USERCODE_CHR,WBCODE_CHR,PYCODE_CHR,ORDERCATEID_CHR,ITEMID_CHR,NULLITEMDOSAGEUNIT_CHR,NULLITEMDOSETYPEID_CHR,ENGNAME_VCHR,COMMNAME_VCHR,STATUS_INT) values('" + newbihID + "','" + SaveRow["MEDICINENAME_VCHR"].ToString() + "','药库自己生成','" + SaveRow["ASSISTCODE_CHR"].ToString() + "','" + SaveRow["WBCODE_CHR"].ToString() + "','" + SaveRow["PYCODE_CHR"].ToString() + "','" + SaveRow["MEDICINETYPEID_CHR"].ToString() + "','" + tbIs.Rows[0][0].ToString() + "','" + SaveRow["DOSAGEUNIT_CHR"].ToString() + "','" + SaveRow["USAGEID_CHR"].ToString() + "','" + SaveRow["MEDICINEENGNAME_VCHR"].ToString() + "','" + SaveRow["MEDNORMALNAME_VCHR"].ToString() + "'," + (SaveRow["IFSTOP_INT"].ToString().Trim() == "0" ? 1 : 0) + ")";
                                Markvo.m_strOPERATORID_CHR = strEmpID;
                                Markvo.m_strTABLESEQID_CHR = "1";
                                Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                                //recordMark.m_mthAddNewRecord(Markvo);
                                #endregion
                            }
                            catch (Exception objEx)
                            {
                                string strTmp = objEx.Message;
                                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                bool blnRes = objLogger.LogError(objEx);
                            }

                            // string newbihDeID = objHRPSvc.m_strGetNewID("t_aid_bih_orderdic_charge", "OCMAPID_CHR", 18);
                            string newbihDeID = "";
                            strSQL = @"select lpad(seq_ocmapid.NEXTVAL,18,'0') p_strRecordID  from dual";
                            m_objTable = new DataTable();
                            try
                            {
                                lngReg = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
                                if (m_objTable.Rows.Count > 0)
                                {
                                    newbihDeID = m_objTable.Rows[0][0].ToString();
                                }
                            }
                            catch (Exception objEx)
                            {
                                string strTmp = objEx.Message;
                                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                bool blnRes = objLogger.LogError(objEx);
                            }



                            strSQL = @"insert into t_aid_bih_orderdic_charge
            (ocmapid_chr, orderdicid_chr, itemid_chr, qty_int, type_int
            ) values(?,?,?,?,?)";
                            try
                            {
                                IDataParameter[] objDPArr = null;
                                objHRP.CreateDatabaseParameter(5, out objDPArr);
                                objDPArr[0].Value = newbihDeID;
                                objDPArr[1].Value = newbihID;
                                objDPArr[2].Value = tbIs.Rows[0][0].ToString();
                                objDPArr[3].Value = 1;
                                objDPArr[4].Value = SaveRow["IPCHARGEFLG_INT"].ToString();

                                long lngEff = -1;
                                lngReg = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                                #region 写入痕迹记录
                                strSQLForMark = @"INSERT INTO t_aid_bih_orderdic_charge
            (ocmapid_chr, orderdicid_chr, itemid_chr, qty_int, type_int
            ) VALUES('" + newbihDeID + "','" + newbihID + "','" + tbIs.Rows[0][0].ToString() + "',1," + SaveRow["IPCHARGEFLG_INT"] + ")";

                                Markvo.m_strOPERATORID_CHR = strEmpID;
                                Markvo.m_strTABLESEQID_CHR = "1";
                                Markvo.m_strRECORDDETAIL_VCHR = strSQLForMark;
                                //recordMark.m_mthAddNewRecord(Markvo);
                                #endregion
                            }
                            catch (Exception objEx)
                            {
                                string strTmp = objEx.Message;
                                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                bool blnRes = objLogger.LogError(objEx);
                            }
                            #endregion

                        }
                    }

                }
            }
            return lngReg;
        }
        #endregion

        #region 输出药品类别的查询结果
        //输出药品信息的查询结果
        [AutoComplete]
        private long m_getMedTypeResult(string strSQL, out clsMedicineType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedicineType_VO[0];
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicineType_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicineType_VO();
                        p_objResultArr[i1].m_strMedicineTypeID = dtbResult.Rows[i1]["MEDICINETYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMedicineTypeName = dtbResult.Rows[i1]["MEDICINETYPENAME_VCHR"].ToString().Trim();
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

        #region 仓库药品维护

        #region 获取所有的药品信息
        /// <summary>
        /// 获取所有的药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="tb">返回药品信息</param>
        /// <param name="tbStorage">返回仓库信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMed(out DataTable tb, out DataTable tbStorage)
        {
            long lngRes = 0;
            tbStorage = new DataTable();
            tb = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.MEDICINEID_CHR,a.ASSISTCODE_CHR,a.MEDICINENAME_VCHR,b.MEDICINETYPENAME_VCHR from T_BSE_MEDICINE a,T_AID_MEDICINETYPE b" +
                " where a.MEDICINETYPEID_CHR=b.MEDICINETYPEID_CHR" +
                " order by a.ASSISTCODE_CHR";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref tb);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select * from t_bse_storage";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref tbStorage);
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

        #region 根据药库ID取出药品信息
        /// <summary>
        /// 根据药库ID取出药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedByStorageID(string strID, out DataTable tb)
        {
            long lngRes = 0;
            tb = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.STORAGEID_CHR,a.MEDICINEID_CHR,b.MEDICINENAME_VCHR,b.ASSISTCODE_CHR,c.MEDICINETYPENAME_VCHR " +
                " from T_BSE_storageandmedicine a,t_bse_medicine b,T_AID_MEDICINETYPE c " +
                " where a.MEDICINEID_CHR=b.MEDICINEID_CHR " +
                " and b.MEDICINETYPEID_CHR=c.MEDICINETYPEID_CHR " +
                " and a.STORAGEID_CHR='" + strID + "'" +
                " order by b.ASSISTCODE_CHR";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref tb);
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

        #region 把药品添加到仓库(全部药品）
        /// <summary>
        /// 把药品添加到仓库(全部药品）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="tb"></param>
        /// <param name="tb1"></param>
        /// <param name="storageID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddMedToStorage(DataTable tb, DataTable tb1, string storageID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (tb1.Rows.Count != 0)
            {
                for (int i1 = 0; i1 < tb.Rows.Count; i1++)
                {
                    for (int f2 = 0; f2 < tb1.Rows.Count; f2++)
                    {
                        if (tb.Rows[i1]["MEDICINEID_CHR"].ToString().Trim() == tb1.Rows[f2]["MEDICINEID_CHR"].ToString().Trim())
                        {
                            break;
                        }
                        if (f2 == tb1.Rows.Count - 1)
                        {
                            string strSQL = @"insert into t_bse_storageandmedicine(STORAGEID_CHR,MEDICINEID_CHR)values('" + storageID + "','" + tb.Rows[i1]["MEDICINEID_CHR"].ToString().Trim() + "')";
                            try
                            {

                                lngRes = objHRPSvc.DoExcute(strSQL);
                            }
                            catch (Exception objEx)
                            {
                                string strTmp = objEx.Message;
                                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                bool blnRes = objLogger.LogError(objEx);
                            }

                        }
                    }

                }
            }
            else
            {
                for (int i1 = 0; i1 < tb.Rows.Count; i1++)
                {
                    string strSQL = @"insert into t_bse_storageandmedicine(STORAGEID_CHR,MEDICINEID_CHR)values('" + storageID + "','" + tb.Rows[i1]["MEDICINEID_CHR"].ToString().Trim() + "')";
                    try
                    {

                        lngRes = objHRPSvc.DoExcute(strSQL);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }

            }

            return lngRes;
        }

        #endregion


        #region 把药品添加到仓库(某一条记录）
        /// <summary>
        /// 把药品添加到仓库(某一条记录）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="storageID"></param>
        /// <param name="strMedID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNoeMedToStorage(string storageID, string strMedID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = @"insert into t_bse_storageandmedicine(STORAGEID_CHR,MEDICINEID_CHR)values('" + storageID + "','" + strMedID + "')";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region 删除指定仓库的药品
        /// <summary>
        /// 删除指定仓库的药品
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="storageID"></param>
        /// <param name="medID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleMedToStorage(string storageID, string medID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (medID == null)
            {
                string strSQL = @"delete from t_bse_storageandmedicine where STORAGEID_CHR='" + storageID + "'";
                try
                {

                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else
            {
                string strSQL = @"delete from t_bse_storageandmedicine where STORAGEID_CHR='" + storageID + "' and MEDICINEID_CHR='" + medID + "'";
                try
                {

                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

            }

            return lngRes;
        }

        #endregion

        #endregion

        #region 输出药品的单位的对应关系
        //输出药品与单位的对应关系
        [AutoComplete]
        private long m_getMedAndUnit(string strSQL, out clsMedUnitAndUnit[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedUnitAndUnit[0];
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedUnitAndUnit[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedUnitAndUnit();
                        p_objResultArr[i1].m_fltBigUnitQty = Convert.ToSingle(dtbResult.Rows[i1]["BIGUNITQTY_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_fltSmallUnit = Convert.ToSingle(dtbResult.Rows[i1]["SMALLUNITQTY_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_intLevel = Convert.ToInt32(dtbResult.Rows[i1]["LEVEL_INT"].ToString().Trim());
                        p_objResultArr[i1].m_objMedicine = new clsMedicine_VO();
                        p_objResultArr[i1].m_objMedicine.m_strMedicineID = dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objMedicine.m_strMedicineName = dtbResult.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objBigUnit = new clsUnit_VO();
                        p_objResultArr[i1].m_objBigUnit.m_strUnitID = dtbResult.Rows[i1]["BIGUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objBigUnit.m_strUnitName = dtbResult.Rows[i1]["BigName"].ToString().Trim();
                        p_objResultArr[i1].m_objSmallUnit = new clsUnit_VO();
                        p_objResultArr[i1].m_objSmallUnit.m_strUnitID = dtbResult.Rows[i1]["SMALLUNIT_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objSmallUnit.m_strUnitName = dtbResult.Rows[i1]["SmallName"].ToString().Trim();
                        p_objResultArr[i1].m_strUsedFlag = dtbResult.Rows[i1]["USEDFLAG_INT"].ToString().Trim();
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

        #region 添加新药品信息
        /*/////////////////////////////////
		 * Sam
		 * 2004-05-18
		*////////////////////////////////////
          /// <summary>
          /// 添加新药品信息
          /// </summary>
          /// <param name="p_objPrincipal"></param>
          /// <param name="p_objMed"></param>
          /// <returns></returns>
          /// 
        [AutoComplete]
        public long m_lngDoAddNewMedicine(clsMedicine_VO p_objMed)
        {
            long lngRes = 0;
            //取得最大的ID 
            //			  string MaxID="12";
            //              lngRes=m_lngMaxID("T_BSE_MEDICINE","MEDICINEID_CHR",out MaxID);
            //              if (lngRes<0||Convert.ToInt32(MaxID)<=0)
            //				  return -1;
            //			  p_objMed.m_strMedicineID=MaxID;
            //			  MedID=MaxID;
            string strSQL = "Insert into T_BSE_MEDICINE(MEDICINEID_CHR,MEDICINENAME_VCHR,MEDICINETYPEID_CHR,MEDSPEC_VCHR,MEDICINESTDID_CHR," +
                "PYCODE_CHR,WBCODE_CHR,MEDICINEPREPTYPE_CHR,PRODUCTORID_CHR,ISANAESTHESIA_CHR,ISCHLORPROMAZINE_CHR,ISCOSTLY_CHR,ISSELF_CHR," +
                "ISIMPORT_CHR,ISSELFPAY_CHR,MEDICINEENGNAME_VCHR,ASSISTCODE_CHR,DOSAGE_DEC,DOSAGEUNIT_CHR,OPUNIT_CHR,IPUNIT_CHR,PACKQTY_DEC,NOQTYFLAG_INT,TRADEPRICE_MNY,UNITPRICE_MNY, medbagunit, highriskflag, isproducedrugs, transno, varietycode) Values ('" + p_objMed.m_strMedicineID + "','" + p_objMed.m_strMedicineName + "'," +
                " '" + p_objMed.m_objMedicineType.m_strMedicineTypeID + "','" + p_objMed.m_strMedSpec + "','" + p_objMed.m_objMedicineStd + "','" +
                p_objMed.m_strPYCode + "','" + p_objMed.m_strWBCode + "','" + p_objMed.m_objMedicinePrepType.m_strMedicinePrepTypeID + "','" +
                p_objMed.m_objProduct.m_strVendorID + "','" + p_objMed.m_strIsAnaesthesia + "','" + p_objMed.m_strIsChlorpromzine + "','" +
                p_objMed.m_strIsCostly + "','" + p_objMed.m_strIsSelf + "','" + p_objMed.m_strIsImport + "','" + p_objMed.m_strIsSelfPay +
                "','" + p_objMed.m_strMedicineEngName + "','" + p_objMed.m_strASSISTCODE_CHR + "'," + p_objMed.m_dblDOSAGE_DEC + ",'" + p_objMed.m_strDOSAGEUNIT_CHR + "','" + p_objMed.m_strOPUNIT_CHR + "','" + p_objMed.m_strIPUNIT_CHR + "'," + p_objMed.m_dblPACKQTY_DEC + "," + p_objMed.m_intNOQTYFLAG_INT + "," + p_objMed.m_dblTRADEPRICE_MNY + "," + p_objMed.m_dblUNITPRICE_MNY + ", '" + p_objMed.MedBagUnit + "', " + p_objMed.HighRiskFlag + ", " + p_objMed.IsProduceDrugs + ",'" + p_objMed.TransNo + "', '" + p_objMed.VarietyCode + "'" + ")";
            try
            {
                //定义一数据执行类
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

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

        #region 更新药品信息 
        //Create by Sam 2004-5-18
        /// <summary>
        /// 通过药品ID更新药品信息
        /// </summary>
        /// <param name="obj_Principal"></param>
        /// <param name="objMed"></param>
        /// <param name="OldID"></param>
        /// <returns></returns>
        // 通过药品ID更新药品信息
        [AutoComplete]
        public long m_lngDoUpdMedicineByID(clsMedicine_VO objMed, string OldID)
        {
            long lngReg = 0;
            string strSQL = "UPdate T_BSE_MEDICINE Set MEDICINENAME_VCHR='" + objMed.m_strMedicineName + "',MEDICINETYPEID_CHR='" + objMed.m_objMedicineType.m_strMedicineTypeID + "'," +
                "MEDSPEC_VCHR='" + objMed.m_strMedSpec + "',MEDICINESTDID_CHR='" + objMed.m_objMedicineStd + "',PYCODE_CHR='" + objMed.m_strPYCode + "'," +
                "WBCODE_CHR='" + objMed.m_strWBCode + "',MEDICINEPREPTYPE_CHR='" + objMed.m_objMedicinePrepType.m_strMedicinePrepTypeID + "', " +
                "PRODUCTORID_CHR='" + objMed.m_objProduct.m_strVendorID + "',ISANAESTHESIA_CHR='" +
                objMed.m_strIsAnaesthesia + "',ISCHLORPROMAZINE_CHR='" + objMed.m_strIsChlorpromzine + "',ISCOSTLY_CHR='" + objMed.m_strIsCostly +
                "',ISSELF_CHR='" + objMed.m_strIsSelf + "',ISIMPORT_CHR='" + objMed.m_strIsImport + "',ISSELFPAY_CHR='" + objMed.m_strIsImport +
                "',MEDICINEENGNAME_VCHR='" + objMed.m_strMedicineEngName + "',ASSISTCODE_CHR='" + objMed.m_strASSISTCODE_CHR + "',DOSAGE_DEC='" +
                objMed.m_dblDOSAGE_DEC + "',DOSAGEUNIT_CHR='" + objMed.m_strDOSAGEUNIT_CHR + "',OPUNIT_CHR='" + objMed.m_strOPUNIT_CHR + "',IPUNIT_CHR='" +
                objMed.m_strIPUNIT_CHR + "',PACKQTY_DEC=" + objMed.m_dblPACKQTY_DEC + ",NOQTYFLAG_INT=" + objMed.m_intNOQTYFLAG_INT + ",TRADEPRICE_MNY=" +
                objMed.m_dblTRADEPRICE_MNY + ",UNITPRICE_MNY=" + objMed.m_dblUNITPRICE_MNY + ", medbagunit = '" + objMed.MedBagUnit + "', highriskflag = " + objMed.HighRiskFlag + ", isproducedrugs = " + objMed.IsProduceDrugs + ", " +
                "transno = '" + objMed.TransNo + "', varietycode = '" + objMed.VarietyCode + "' " +
                " Where MEDICINEID_CHR='" + OldID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion

        #region 删除药品信息
        //Create by Sam 2004-5-18
        /// <summary>
        /// 通过药品ID删除药品信息
        /// </summary>
        /// <returns></returns>
        // 通过药品ID删除药品信息
        [AutoComplete]
        public long m_lngDeleteMedicineByID(string strID, bool isDeleItem, string strEmpID)
        {
            long lngReg = 0;
            clsRecordMark_VO Markvo = new clsRecordMark_VO();
            //clsRecordMark recordMark = new clsRecordMark();
            string strSQL = "Delete T_BSE_MEDICINE Where MEDICINEID_CHR='" + strID + "'";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                lngReg = objHRP.DoExcute(strSQL);
                #region 写入痕迹记录
                Markvo.m_strOPERATORID_CHR = strEmpID;
                Markvo.m_strTABLESEQID_CHR = "1";
                Markvo.m_strRECORDDETAIL_VCHR = strSQL;
                //recordMark.m_mthAddNewRecord(Markvo);
                #endregion
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            if (isDeleItem)
            {
                strSQL = @"Delete t_aid_inschargeitem Where ITEMID_CHR = (SELECT ITEMID_CHR FROM T_BSE_CHARGEITEM WHERE ITEMSRCID_VCHR='" + strID + "')";

                try
                {
                    lngReg = objHRP.DoExcute(strSQL);
                }
                catch (Exception objTxt)
                {
                    string strTmp = objTxt.Message;
                    com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                    bool blnReg = objLog.LogError(objTxt);
                }
                strSQL = @"Delete T_BSE_CHARGEITEM Where ITEMSRCID_VCHR='" + strID + "' and ITEMSRCTYPE_INT=1";

                try
                {
                    lngReg = objHRP.DoExcute(strSQL);
                    #region 写入痕迹记录

                    Markvo.m_strOPERATORID_CHR = strEmpID;
                    Markvo.m_strTABLESEQID_CHR = "1";
                    Markvo.m_strRECORDDETAIL_VCHR = strSQL;
                    //recordMark.m_mthAddNewRecord(Markvo);
                    #endregion
                }
                catch (Exception objTxt)
                {
                    string strTmp = objTxt.Message;
                    com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                    bool blnReg = objLog.LogError(objTxt);
                }

            }
            return lngReg;
        }
        #endregion

        #region 删除药品对应的药典
        /// <summary>
        /// 删除药品对应的药典
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedByID(string strID)
        {
            long lngReg = 0;
            string strSQL = "update T_BSE_MEDICINE set MEDICINESTDID_CHR='',MEDICINESTDESC_VCHR=''  Where MEDICINEID_CHR='" + strID + "'";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                lngReg = objHRP.DoExcute(strSQL);

            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion

        #region 通过ID查询药品信息
        /// <summary>
        /// 通过ID查询药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strMedID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedicineByID(string strMedID, out clsMedicine_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicine_VO[0];
            long lngRes = 0;
            string strSQL = "SELECT a.*,b.MEDICINETYPENAME_VCHR,c.MEDICINEPREPTYPENAME_VCHR,d.VENDORNAME_VCHR FROM T_BSE_MEDICINE a " +
                " Left Outer Join T_AID_MEDICINETYPE b On b.MEDICINETYPEID_CHR=a.MEDICINETYPEID_CHR Left " +
                " Outer Join T_AID_MEDICINEPREPTYPE c On c.MEDICINEPREPTYPE_CHR=a.MEDICINEPREPTYPE_CHR " +
                " Left Outer Join T_BSE_VENDOR d On d.VENDORID_CHR=a.PRODUCTORID_CHR" +
                " Where MEDICINEID_CHR='" + strMedID + "'";
            lngRes = m_getResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region  通过各种查询条件查找药品信息，可以模糊查询(添加Where语句)
        /// <summary>
        /// 通过各种查询条件查找药品信息，可以模糊查询
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strSubSQL"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedicineByAny(string strSubSQL, out clsMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedicine_VO[0];
            string strSQL = "SELECT a.*,b.MEDICINETYPENAME_VCHR,c.MEDICINEPREPTYPENAME_VCHR,d.VENDORNAME_VCHR FROM T_BSE_MEDICINE a " +
                " Left Outer Join T_AID_MEDICINETYPE b On b.MEDICINETYPEID_CHR=a.MEDICINETYPEID_CHR Left " +
                " Outer Join T_AID_MEDICINEPREPTYPE c On c.MEDICINEPREPTYPE_CHR=a.MEDICINEPREPTYPE_CHR " +
                "Left Outer Join T_BSE_VENDOR d On d.VENDORID_CHR=a.PRODUCTORID_CHR ";
            strSQL = strSQL + strSubSQL;
            lngRes = m_getResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        //药品类型
        #region 添加药品类型信息
        //Create By Sam 2004-5-18
        /// <summary>
        ///   添加药品类型信息
        /// </summary>
        /// <returns></returns>
        // 添加药品类型信息
        [AutoComplete]
        public long m_lngDoAddNewMedicineType(clsMedicineType_VO objMedType)
        {
            long lngReg = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                //				objMedType.m_strMedicineTypeID=objHRP.m_strGetNewID("T_AID_MEDICINETYPE","MEDICINETYPEID_CHR");
                string strSQL = "Insert Into T_AID_MEDICINETYPE (MEDICINETYPEID_CHR,MEDICINETYPENAME_VCHR)Values('" + objMedType.m_strMedicineTypeID + "','" + objMedType.m_strMedicineTypeName + "')";
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool bnlRec = objLog.LogError(objEx);
            }
            return lngReg;
        }
        #endregion

        #region 更新药品类型信息
        //Create by Sam 2004-5-18
        /// <summary>
        /// 通过药品ID更新药品类型信息
        /// </summary>
        /// <param name="obj_Principal"></param>
        /// <param name="objMed"></param>
        /// <param name="OldID"></param>
        /// <returns></returns>
        // 通过药品ID更新药品类型信息
        [AutoComplete]
        public long m_lngDoUpdMedicineTypeByID(clsMedicineType_VO objMed, string OldID)
        {
            long lngReg = 0;
            string strSQL = "UPdate T_Aid_MedicineType Set MEDICINETYPEID_CHR='" + objMed.m_strMedicineTypeID + "',MEDICINETYPENAME_VCHR='" + objMed.m_strMedicineTypeName + "' " +
                " Where MEDICINETYPEID_CHR='" + OldID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion


        #region 通过药品类型ID删除药品类型信息
        //Create by Sam 2004-5-18
        /// <summary>
        /// 通过药品类型ID删除药品类型信息
        /// </summary>
        /// <param name="obj_Principal"></param>
        /// <param name="objMed"></param>
        /// <returns></returns>
        // 通过药品类型ID删除药品类型信息
        [AutoComplete]
        public long m_lngDeleteMedicineTypeByID(clsMedicineType_VO objMed)
        {
            long lngReg = 0;
            string strSQL = "Delete T_AID_MEDICINETYPE Where MEDICINETYPEID_CHR='" + objMed.m_strMedicineTypeID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
                objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion


        #region 通过药品类型ID查询药品类型信息
        /// <summary>
        /// 通过药品类型ID查询药品类型信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strMedTypeID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindMedicineTypeByID(string strMedTypeID, out clsMedicineType_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicineType_VO[0];
            long lngRes = 0;
            string strSQL = "SELECT * FROM T_Aid_MedicineType Where MEDICINETYPEID_CHR='" + strMedTypeID + "'";
            lngRes = m_getMedTypeResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 查询所有药品类型信息
        /// <summary>
        /// 查询所有药品类型信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllMedicineType(out clsMedicineType_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicineType_VO[0];
            long lngRes = 0;
            string strSQL = "SELECT * FROM T_Aid_MedicineType ";
            lngRes = m_getMedTypeResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        // 新增药品与单位关系
        #region 新增药品与单位关系
        /*/////////////////////////////////
		 * Sam
		 * 2004-05-18
		*////////////////////////////////////
          /// <summary>
          /// 新增药品与单位关系
          /// </summary>
          /// <param name="p_objPrincipal"></param>
          /// <param name="p_objMedUnit"></param>
          /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAddNewMedUnitAndUnit(clsMedUnitAndUnit p_objMedUnit)
        {
            long lngRes = 0;
            string strSQL = " Insert Into T_BSE_MEDUNITANDUNIT(BIGUNIT_CHR,SMALLUNIT_CHR,BIGUNITQTY_DEC,SMALLUNITQTY_DEC,MEDICINEID_CHR,LEVEL_INT,USEDFLAG_INT) " +
                "Values('" + p_objMedUnit.m_objBigUnit.m_strUnitID + "','" + p_objMedUnit.m_objSmallUnit.m_strUnitID + "','" + p_objMedUnit.m_fltBigUnitQty + "'," +
                "'" + p_objMedUnit.m_fltSmallUnit + "','" + p_objMedUnit.m_objMedicine.m_strMedicineID + "','" + p_objMedUnit.m_intLevel + "','" + p_objMedUnit.m_strUsedFlag + "')";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

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

        #region 更新药品与单位关系
        /*/////////////////////////////////
		 * Sam
		 * 2004-05-18
		*////////////////////////////////////
          /// <summary>
          /// 更新药品与单位关系
          /// </summary>
          /// <param name="p_objPrincipal"></param>
          /// <param name="p_objMedUnit"></param>
          /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpMedUnitAndUnit(clsMedUnitAndUnit p_objMedUnit)
        {
            long lngRes = 0;
            string strSQL = "Update T_BSE_MEDUNITANDUNIT Set BIGUNIT_CHR='" + p_objMedUnit.m_objBigUnit.m_strUnitID + "', " +
                "SMALLUNIT_CHR='" + p_objMedUnit.m_objSmallUnit.m_strUnitID + "',BIGUNITQTY_DEC='" + p_objMedUnit.m_fltBigUnitQty + "', " +
                "SMALLUNITQTY_DEC='" + p_objMedUnit.m_fltSmallUnit + "',MEDICINEID_CHR='" + p_objMedUnit.m_objMedicine.m_strMedicineID + "'," +
                "LEVEL_INT='" + p_objMedUnit.m_intLevel + "',USEDFLAG_INT='" + p_objMedUnit.m_strUsedFlag + "' " +
                " Where MEDICINEID_CHR='" + p_objMedUnit.m_objMedicine.m_strMedicineID + "' And " +
                " BIGUNIT_CHR='" + p_objMedUnit.m_objBigUnit.m_strUnitID + "' ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region 通过药品ID删除药品与单位的关系
        //Create by Sam 2004-5-18
        /// <summary>
        /// 通过药品ID删除药品与单位的关系
        /// </summary>
        /// <param name="obj_Principal"></param>
        /// <param name="strMedID"></param>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        // 通过药品ID删除药品与单位的关系
        [AutoComplete]
        public long m_lngDeleteMedUnitAndUnit(string strMedID, string UnitID)
        {
            long lngReg = 0;
            string strSQL = "Delete T_BSE_MEDUNITANDUNIT Where MEDICINEID_CHR='" + strMedID + "' And BIGUNIT_CHR='" + UnitID + "' ";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
                objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion

        #region 查询所有药品与单位的关系
        /// <summary>
        /// 查询所有药品与单位的关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllMedAndUnit(out clsMedUnitAndUnit[] p_objResultArr)
        {
            p_objResultArr = new clsMedUnitAndUnit[0];
            long lngRes = 0;
            string strSQL = "SELECT a.*,b.UNITNAME_CHR BigName,c.UNITNAME_CHR SmallName,d.MEDICINENAME_VCHR FROM T_BSE_MEDUNITANDUNIT a " +
                " Left Outer Join T_Aid_Unit b On a.BIGUNIT_CHR=b.UNITID_CHR Left " +
                " Outer Join T_Aid_Unit c On a.SmallUNIT_CHR=c.UNITID_CHR " +
                " Left Outer Join T_BSE_MEDICINE d On a.MEDICINEID_CHR=d.MEDICINEID_CHR ";
            lngRes = m_getMedAndUnit(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 根据药品ID和单位ID查询药品与单位的关系
        /// <summary>
        /// 根据药品ID和单位ID查询药品与单位的关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedID"></param>
        /// <param name="BigUnitID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedAndUnitByID(string MedID, string BigUnitID, out clsMedUnitAndUnit[] p_objResultArr)
        {
            p_objResultArr = new clsMedUnitAndUnit[0];
            long lngRes = 0;
            string strSQL = "SELECT a.*,b.UNITNAME_CHR BigName,c.UNITNAME_CHR SmallName,d.MEDICINENAME_VCHR FROM T_BSE_MEDUNITANDUNIT a " +
                " Left Outer Join T_Aid_Unit b On a.BIGUNIT_CHR=b.UNITID_CHR Left " +
                " Outer Join T_Aid_Unit c On a.SmallUNIT_CHR=c.UNITID_CHR " +
                " Left Outer Join T_BSE_MEDICINE d On a.MEDICINEID_CHR=d.MEDICINEID_CHR " +
                " Where MEDICINEID_CHR='" + MedID + "' And BIGUNIT_CHR='" + BigUnitID + "' ";
            lngRes = m_getMedAndUnit(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 根据药品ID药品与单位的关系
        /// <summary>
        /// 根据药品ID查询药品与单位的关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedAndUnitByMedID(string MedID, out clsMedUnitAndUnit[] p_objResultArr)
        {
            p_objResultArr = new clsMedUnitAndUnit[0];
            long lngRes = 0;

            string strSQL = "SELECT a.*,b.UNITNAME_CHR BigName,c.UNITNAME_CHR SmallName,d.MEDICINENAME_VCHR FROM T_BSE_MEDUNITANDUNIT a " +
                " Left Outer Join T_Aid_Unit b On a.BIGUNIT_CHR=b.UNITID_CHR Left " +
                " Outer Join T_Aid_Unit c On a.SmallUNIT_CHR=c.UNITID_CHR " +
                " Left Outer Join T_BSE_MEDICINE d On a.MEDICINEID_CHR=d.MEDICINEID_CHR " +
                " Where MEDICINEID_CHR='" + MedID + "' ";
            lngRes = m_getMedAndUnit(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        //单位
        #region 添加药品单位信息
        //Create By Sam 2004-5-18
        /// <summary>
        ///   添加药品单位信息
        /// </summary>
        /// <returns></returns>
        // 添加药品单位信息
        [AutoComplete]
        public long m_lngDoAddNewUnit(clsUnit_VO objUnit)
        {
            long lngReg = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                string strSQL = "Insert Into T_AID_UNIT (UNITID_CHR,UNITNAME_CHR)Values('" + objUnit.m_strUnitID + "','" + objUnit.m_strUnitName + "')";
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool bnlRec = objLog.LogError(objEx);
            }
            return lngReg;
        }
        #endregion

        #region 更新药品单位信息
        //Create by Sam 2004-5-18
        /// <summary>
        /// 更新药品单位信息
        /// </summary>
        /// <param name="obj_Principal"></param>
        /// <param name="objUnit"></param>
        /// <param name="OldID"></param>
        /// <returns></returns>
        // 更新药品单位信息
        [AutoComplete]
        public long m_lngDoUpdUnit(clsUnit_VO objUnit, string OldID)
        {
            long lngReg = 0;
            string strSQL = "UPdate T_AID_UNIT Set UNITID_CHR='" + objUnit.m_strUnitID + "',UNITNAME_CHR='" + objUnit.m_strUnitName + "' " +
                " Where UNITID_CHR='" + OldID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion


        #region 通过单位ID删除单位
        //Create by Sam 2004-5-18
        /// <summary>
        /// 通过单位ID删除单位
        /// </summary>
        /// <param name="obj_Principal"></param>
        /// <param name="strUnitID"></param>
        /// <returns></returns>
        // 通过单位ID删除单位
        [AutoComplete]
        public long m_lngDeleteUnit(string strUnitID)
        {
            long lngReg = 0;
            string strSQL = "Delete T_AID_UNIT Where UNITID_CHR='" + strUnitID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion

        //剂型
        #region 添加药品剂型
        //Create By Sam 2004-5-18
        /// <summary>
        ///   添加药品剂型
        /// </summary>
        /// <returns></returns>
        // 添加药品剂型
        [AutoComplete]
        public long m_lngDoAddNewPrepType(clsMedicinePrepType_VO p_objMed)
        {
            long lngReg = 0;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                //				string PrepTypeID=objHRP.m_strGetNewID("T_AID_MEDICINEPREPTYPE","MEDICINEPREPTYPE_CHR");
                string strSQL = "Insert Into T_AID_MEDICINEPREPTYPE (MEDICINEPREPTYPE_CHR,MEDICINEPREPTYPENAME_VCHR,FLAGA_INT)Values('" + p_objMed.m_strMedicinePrepTypeID + "','" + p_objMed.m_strMedicinePrepTypeName + "'," + p_objMed.m_intDoseType + ")";
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool bnlRec = objLog.LogError(objEx);
            }
            return lngReg;
        }
        #endregion

        #region 更新药品剂型
        //Create by Sam 2004-5-18
        /// <summary>
        /// 更新药品剂型
        /// </summary>
        /// <param name="obj_Principal"></param>
        /// <param name="objType"></param>
        /// <param name="OldID"></param>
        /// <returns></returns>
        // 更新药品剂型
        [AutoComplete]
        public long m_lngDoUpdPrepType(clsMedicinePrepType_VO objType, string OldID)
        {
            long lngReg = 0;

            string strSQL = "UPdate T_AID_MEDICINEPREPTYPE Set MEDICINEPREPTYPE_CHR='" + objType.m_strMedicinePrepTypeID + "',MEDICINEPREPTYPENAME_VCHR='" + objType.m_strMedicinePrepTypeName + "' ,FLAGA_INT=" + objType.m_intDoseType + "" +
                " Where MEDICINEPREPTYPE_CHR='" + OldID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion


        #region 通过ID删除剂型
        //Create by Sam 2004-5-18
        /// <summary>
        /// 通过ID删除剂型
        /// </summary>
        /// <param name="obj_Principal"></param>
        /// <param name="strPrepTypeID"></param>
        /// <returns></returns>
        // 通过ID删除剂型
        [AutoComplete]
        public long m_lngDeletePrepType(string strPrepTypeID)
        {
            long lngReg = 0;
            string strSQL = "Delete T_AID_MEDICINEPREPTYPE Where MEDICINEPREPTYPE_CHR='" + strPrepTypeID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
                if (lngReg > 0)
                    objHRP.Dispose();
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion

        #region 查询所有剂型
        /// <summary>
        /// 查询所有剂型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllMeicinePrep(out clsMedicinePrepType_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicinePrepType_VO[0];
            long lngRes = 0;
            DataTable dtbResult = new DataTable();
            string strSQL = "SELECT * FROM T_AID_MEDICINEPREPTYPE ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicinePrepType_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicinePrepType_VO();
                        p_objResultArr[i1].m_strMedicinePrepTypeID = dtbResult.Rows[i1]["MEDICINEPREPTYPE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMedicinePrepTypeName = dtbResult.Rows[i1]["MEDICINEPREPTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intDoseType = int.Parse(dtbResult.Rows[i1]["FLAGA_INT"].ToString().Trim());
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

        #region 查询所有单位
        /// <summary>
        /// 查询所有单位
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllUnit(out clsUnit_VO[] p_objResultArr)
        {
            p_objResultArr = new clsUnit_VO[0];
            long lngRes = 0;
            DataTable dtbResult = new DataTable();
            string strSQL = "SELECT * FROM T_AID_UNIT ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsUnit_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsUnit_VO();
                        p_objResultArr[i1].m_strUnitID = dtbResult.Rows[i1]["UNITID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUnitName = dtbResult.Rows[i1]["UNITNAME_CHR"].ToString().Trim();
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


        #region 获得药品最大的ID
        /// <summary>
        /// 获得药品最大的ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strItemID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedMaxID(out string p_strItemID)
        {
            p_strItemID = null;
            long lngRes = 0;
            try
            {
                this.GetMaxID("T_BSE_MEDICINE", "MEDICINEID_CHR", out p_strItemID);
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

        #region 获得最大的ID sType:1-药品类型 2-单位 3-剂型
        /// <summary>
        /// 获得最大的ID sType:1-单位 2-药品类型 3-剂型 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="sType"></param>
        /// <param name="p_strItemID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMaxID(byte sType, out string p_strItemID)
        {
            p_strItemID = null;
            long lngRes = 0;
            string strTable = null;
            string strField = null;
            switch (sType)
            {
                case 2: //药品类型
                    strTable = "T_AID_MEDICINETYPE";
                    strField = "MEDICINETYPEID_CHR";
                    break;
                case 1: //单位
                    strTable = "T_AID_UNIT";
                    strField = "UNITID_CHR";
                    break;
                case 3: //剂型
                    strTable = "T_AID_MEDICINEPREPTYPE";
                    strField = "MEDICINEPREPTYPE_CHR";
                    break;

            }
            //string strSQL = "SELECT MAX ("+ strField +") AS MaxID FROM "+strTable +" ";
            try
            {
                //				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP=new clsHRPTableService();
                //				p_strItemID =objHRP.m_strGetNewID(strTable,strField);
                this.GetMaxID(strTable, strField, out p_strItemID);
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

        #region 根据ID取回相应的Name,sType:1-单位 2-药品类型 3-剂型 
        /// <summary>
        /// 根据ID取回相应的Name,sType:1-单位 2-药品类型 3-剂型 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="sType"></param>
        /// <param name="strID"></param>
        /// <param name="p_strItemName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetItemByID(byte sType, string strID, out string p_strItemName)
        {
            p_strItemName = "";
            long lngRes = 0;
            string strTable = null;
            string strFieldID = null;
            string strFieldName = null;
            switch (sType)
            {
                case 2: //药品类型
                    strTable = "T_AID_MEDICINETYPE";
                    strFieldID = "MEDICINETYPEID_CHR";
                    strFieldName = "MEDICINETYPENAME_VCHR";
                    break;
                case 1: //单位
                    strTable = "T_AID_UNIT";
                    strFieldID = "UNITID_CHR";
                    strFieldName = "UNITNAME_CHR";
                    break;
                case 3: //剂型
                    strTable = "T_AID_MEDICINEPREPTYPE";
                    strFieldID = "MEDICINEPREPTYPE_CHR";
                    strFieldName = "MEDICINEPREPTYPENAME_VCHR";
                    break;

            }
            string strSQL = "SELECT " + strFieldName + " AS ItemName FROM " + strTable + " Where " + strFieldID + "=" + strID + "";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                DataTable objResult = new DataTable();
                lngRes = objHRP.lngGetDataTableWithoutParameters(strSQL, ref objResult);
                if (lngRes <= 0)
                    return -1;
                if (objResult.Rows.Count > 0)
                    p_strItemName = objResult.Rows[0][0].ToString().Trim();

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

        #region 取回当前药品与单位关系的级别
        /// <summary>
        /// 取回当前药品与单位关系的级别
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedID"></param>
        /// <param name="LevelID"></param>
        [AutoComplete]
        public void GetMaxLeve(string MedID, out string LevelID)
        {
            LevelID = "1";
            long lngRes = 0;
            try
            {
                string strID = null;
                string strSQL = "SELECT MAX (LEVEL_INT) AS MaxID FROM T_BSE_MEDUNITANDUNIT Where MEDICINEID_CHR='" + MedID + "' ";
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                DataTable objResult = new DataTable();
                lngRes = objHRP.lngGetDataTableWithoutParameters(strSQL, ref objResult);
                if (lngRes <= 0)
                    return;
                if (objResult.Rows.Count > 0)
                    strID = objResult.Rows[0][0].ToString().Trim();
                if (strID != "")
                    LevelID = Convert.ToString((int.Parse(strID.ToString()) + 1));
                else
                    LevelID = "1";
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion

        #region 返回最大值
        private long GetMaxID(string strTable, string strField, out string strID)
        {
            strID = "1";
            string strSQL = "SELECT MAX (TO_NUMBER((" + strField + "))) AS MaxID FROM " + strTable + " ";
            long lngRes = 0;
            try
            {
                DataTable objResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngRes = objHRP.lngGetDataTableWithoutParameters(strSQL, ref objResult);
                if (lngRes <= 0)
                    return -1;
                if (objResult.Rows.Count > 0)
                    strID = objResult.Rows[0][0].ToString().Trim();
                if (strID == "")
                    strID = "1";
                else
                    strID = Convert.ToString((int.Parse(strID.ToString()) + 1));
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

        #region 获取单位信息
        [AutoComplete]
        public long m_lngGetUnitArr(out clsUnit_Vo[] p_objResultArr)
        {
            p_objResultArr = new clsUnit_Vo[0];
            long lngRes = 0;
            string strSQL = @"SELECT * FROM T_AID_UNIT";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsUnit_Vo[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsUnit_Vo();
                        p_objResultArr[i1].m_strUNITID_CHR = dtbResult.Rows[i1]["UNITID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUNITNAME_CHR = dtbResult.Rows[i1]["UNITNAME_CHR"].ToString().Trim();
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

        #region 药品基本资料
        [AutoComplete]
        public long m_lngGetMedicine(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = @"SELECT MEDICINEID_CHR,MEDICINENAME_VCHR,MEDSPEC_VCHR,OPUNIT_CHR,UNITPRICE_MNY FROM T_BSE_MEDICINE";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


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

        #region 药库配药管理   2004-9-29
        #region  获取所有的申请单据(根据日期）
        [AutoComplete]
        public long m_lngGetMedAppl(out clsStoreMedAppl_VO[] p_objResultArr, string STORAGEID, string strDate)
        {
            p_objResultArr = new clsStoreMedAppl_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT a.*,b.MEDSTORENAME_VCHR,c.LASTNAME_VCHR FROM T_OPR_MEDSTOREMEDAPPL a,T_BSE_MEDSTORE b,T_BSE_EMPLOYEE c WHERE a.APPLMEDSTOREID_CHR=b.MEDSTOREID_CHR(+) AND a.CREATOR_CHR=c.EMPID_CHR(+) and a.MEDSTORAGEID_CHR='" + STORAGEID + "' and APPLDATE_DAT between to_date('" + strDate + " 00:00:00','yyyy-mm-dd hh24:mi:ss') and to_date('" + strDate + " 23:59:59','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsStoreMedAppl_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsStoreMedAppl_VO();
                        p_objResultArr[i1].m_strMEDAPPLID_CHR = dtbResult.Rows[i1]["MEDAPPLID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strAPPLMEDSTOREID_CHR = dtbResult.Rows[i1]["APPLMEDSTOREID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAPPLMEDSTORENAME_CHR = dtbResult.Rows[i1]["MEDSTORENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strAPPLDATE_DAT = dtbResult.Rows[i1]["APPLDATE_DAT"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATOR_CHR = dtbResult.Rows[i1]["CREATOR_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATORNAME_CHR = dtbResult.Rows[i1]["LASTNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEMO_VCHR = dtbResult.Rows[i1]["MEMO_VCHR"].ToString().Trim();
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

        #region  根据申请单号获取所有的申请明细
        /// <summary>
        ///根据申请单号获取所有的申请明细 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <param name="dtbResult"></param>
        /// <param name="flat">false 标志此申请单有没有设置包装量的药品</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedApplDeByID(string strID, out DataTable dtbResult, out bool flat)
        {
            long lngRes = 0;
            flat = true;
            dtbResult = null;
            string strSQL = @"SELECT a.*,b.MEDICINENAME_VCHR,b.OPUNIT_CHR,b.PACKQTY_DEC,b.OPCHARGEFLG_INT FROM  T_OPR_MEDSTOREMEDAPPLDE a,T_BSE_MEDICINE b WHERE a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.MEDAPPLID_CHR='" + strID + "'";
            try
            {
                dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                {
                    if (dtbResult.Rows[i1]["OPCHARGEFLG_INT"].ToString() == "1")
                    {
                        if (dtbResult.Rows[i1]["PACKQTY_DEC"].ToString() != "" && dtbResult.Rows[i1]["PACKQTY_DEC"].ToString() != "0")
                        {
                            dtbResult.Rows[i1]["UNITID_CHR"] = dtbResult.Rows[i1]["OPUNIT_CHR"];
                            dtbResult.Rows[i1]["QTY_DEC"] = (double)dtbResult.Rows[i1]["QTY_DEC"] / (double)dtbResult.Rows[i1]["PACKQTY_DEC"];
                        }
                        else
                        {
                            dtbResult.Rows[i1]["UNITID_CHR"] = dtbResult.Rows[i1]["OPUNIT_CHR"];
                            dtbResult.Rows[i1]["QTY_DEC"] = 0;
                            dtbResult.Rows[i1]["PACKQTY_DEC"] = 0;
                            flat = false;
                        }
                    }
                }
            }
            return lngRes;

        }
        #endregion

        #region 根据药品ID查询库存明细表
        [AutoComplete]
        public long m_lngGetDeTailByMedID(string MedID, out DataTable p_objResultArr)
        {
            p_objResultArr = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT a.STORAGEID_CHR,a.PRODUCTORID_CHR,a.MEDICINEID_CHR,a.SYSLOTNO_CHR,a.CURQTY_DEC,a.UNITID_CHR,a.USEFULLIFE_DAT,a.BUYUNITPRICE_MNY,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,c.VENDORNAME_VCHR,d.STORAGENAME_VCHR
                             FROM T_OPR_STORAGEMEDDETAIL a ,t_bse_medicine b,t_bse_vendor c,T_BSE_STORAGE d where a.STORAGEID_CHR=d.storageid_chr(+) and  a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.PRODUCTORID_CHR=c.VENDORID_CHR(+) and a.USEFULSTATUS_INT=1 and a.MEDICINEID_CHR='" + MedID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_objResultArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            for (int i1 = 0; i1 < p_objResultArr.Rows.Count; i1++)
            {
                string SysRow = p_objResultArr.Rows[i1]["SYSLOTNO_CHR"].ToString();
                strSQL = @"select qty_dec from t_opr_storageordde where SYSLOTNO_CHR='" + SysRow + "' and  storageordid_chr in (select STORAGEORDID_CHR from t_opr_storageord where PSTATUS_INT=1 and STORAGEORDTYPEID_CHR in (select STORAGEORDTYPEID_CHR from t_aid_storageordtype where SIGN_INT=2))";
                DataTable p_objResult = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_objResult);
                if (lngRes > 0 && p_objResult != null)
                {

                    for (int j2 = 0; j2 < p_objResult.Rows.Count; j2++)
                    {
                        if (p_objResult.Rows[j2]["qty_dec"].ToString() != "")
                        {
                            p_objResultArr.Rows[i1]["CURQTY_DEC"] = Convert.ToInt32(p_objResultArr.Rows[i1]["CURQTY_DEC"]) - Convert.ToInt32(p_objResult.Rows[j2]["qty_dec"]);

                        }
                    }
                }

            }

            return lngRes;

        }
        #endregion

        #region 保存与生成出库单
        /// <summary>
        /// 保存与生成出库单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strMedApplId"></param>
        /// <param name="objResult"></param>
        /// <param name="objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangAndSave(string strMedApplId, clsMedStorageOrd_VO objResult, clsMedStorageOrdDe_VO[] objResultArr)
        {
            long lngReg = 0;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
            string stroageID = "";
            objHRP.m_lngGenerateNewID("T_OPR_STORAGEORD", "STORAGEORDID_CHR", out stroageID);
            objResult.m_strSTORAGEORDID_CHR = stroageID;
            strSQL = @"Insert Into T_OPR_STORAGEORD(STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,STORAGEID_CHR,PERIODID_CHR,INORD_DAT,TOLMNY_MNY,PSTATUS_INT,CREATORID_CHR,CREATEDATE_DAT,MEMO_VCHR,DOCID_VCHR,SIGN_INT,DEPTID_CHR)"
                + " VALUES('" + objResult.m_strSTORAGEORDID_CHR + "','" + objResult.m_strSTORAGEORDTYPEID_CHR + "','" + objResult.m_strSTORAGEID_CHR + "','" + objResult.m_strPERIODID_CHR + "',To_Date('" + objResult.m_strINORD_DAT + "','yyyy-mm-dd hh24:mi:ss'),"
                + objResult.m_dblTOLMNY_MNY + "," + objResult.m_intPSTATUS_INT + ",'" + objResult.m_strCREATORID_CHR + "',To_Date('" + objResult.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss'),'" + objResult.m_strMEMO_VCHR + "','" + objResult.m_strDOCID_VCHR + "',2,'" + objResult.m_strDEPTID_CHR + "')";//插入出库单
            try
            {
                objHRP.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool bnlRec = objLog.LogError(objEx);
            }
            strSQL = @"Update t_opr_medstoremedappl set MEDSTORAGEID_CHR='" + objResult.m_strSTORAGEID_CHR.Trim() + "',PSTATUS_INT=2 where MEDAPPLID_CHR='" + strMedApplId + "'";
            try
            {
                objHRP.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool bnlRec = objLog.LogError(objEx);
            }
            for (int i1 = 0; i1 < objResultArr.Length; i1++)//插入出库明细
            {
                string newID = "";
                objHRP.m_lngGenerateNewID("T_OPR_STORAGEORDDE", "STORAGEORDDEID_CHR", out newID);
                objResultArr[i1].m_strSTORAGEORDDEID_CHR = newID;
                objResultArr[i1].m_strSTORAGEORDID_CHR = stroageID;
                strSQL = @"Insert Into T_OPR_STORAGEORDDE(STORAGEORDDEID_CHR,STORAGEORDID_CHR,MEDICINEID_CHR,SYSLOTNO_CHR,ORD_DAT,ROWNO_CHR,UNITID_CHR,PRODCUTORID_CHR,QTY_DEC,SALEUNITPRICE_MNY,BUYTOLPRICE_MNY,USEFULLIFE_DAT)"
                                                     + " values('" + objResultArr[i1].m_strSTORAGEORDDEID_CHR + "','" + objResultArr[i1].m_strSTORAGEORDID_CHR + "','" + objResultArr[i1].m_strMEDICINEID_CHR + "','"
                                                     + objResultArr[i1].m_strSYSLOTNO_CHR + "',To_Date('" + objResultArr[i1].m_strORD_DAT + "','yyyy-mm-dd hh24:mi:ss'),'" + objResultArr[i1].m_strROWNO_CHR + "','"
                                                     + objResultArr[i1].m_strUNITID_CHR + "','" + objResultArr[i1].m_strPRODCUTORID_CHR + "'," + objResultArr[i1].m_dblQTY_DEC + "," + objResultArr[i1].m_dblSALEUNITPRICE_MNY + "," + objResultArr[i1].m_dblBUYTOLPRICE_MNY + ",To_Date('" + objResultArr[i1].m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss'))";
                try
                {
                    lngReg = objHRP.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                    bool bnlRec = objLog.LogError(objEx);
                }
            }
            objHRP.Dispose();
            return lngReg;

        }


        #endregion

        #region 获取药库的信息
        [AutoComplete]
        public long m_lngGetMedstroage(string medStroageID, out string medStroageName)
        {
            medStroageName = null;
            long lngRes = 0;
            string strSQL = @"select STORAGENAME_VCHR from T_BSE_STORAGE where STORAGEID_CHR='" + medStroageID + "'";
            DataTable dtbResult = new DataTable();
            try
            {

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                medStroageName = dtbResult.Rows[0]["STORAGENAME_VCHR"].ToString();
            }
            else
            {
                medStroageName = null;
            }
            return lngRes;

        }

        #endregion

        #endregion


        #region 药品基本信息管理
        /// <summary>
        /// 药品基本信息管理
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtUnit">返回单位信息</param>
        /// <param name="dtMeicinePrep">返回药品制剂类型</param>
        /// <param name="dtuse">返回用法</param>
        /// <param name="dtvendor">返回厂家</param>
        ///<param name="dtmedtype">返回药品类型信息</param>
        ///<param name="dtItemextype">返回门诊核算类型</param>
        ///<param name="dtItemextype1">返回门诊发票类型</param>
        ///<param name="dtItemextype3">返回住院核算类型</param>
        ///<param name="dtItemextype4">返回住院发票类型</param>
        ///<param name="dtMEDICARETYPE">返回医保类型数据</param>
        ///<param name="dtItemextype5">病案核算分类</param>
        ///<param name="dtPharMatype">药理分类</param>
        ///<param name="Isuse">是否可以直接在药品基本界面修改药品价格</param>
        /// <param name="dtCATEID1">医嘱分类</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllBase(out DataTable dtUnit, out DataTable dtMeicinePrep, out DataTable dtuse, out DataTable dtFreq, out DataTable dtvendor, out DataTable dtmedtype, out DataTable dtItemextype, out DataTable dtItemextype1, out DataTable dtItemextype3, out DataTable dtItemextype4, out DataTable dtMEDICARETYPE, out DataTable dtItemextype5, out DataTable dtPharMatype, out bool Isuse, out DataTable dtCATEID1)
        {
            Isuse = false;
            dtmedtype = new DataTable();
            dtUnit = new DataTable();
            dtMeicinePrep = new DataTable();
            dtuse = new DataTable();
            dtvendor = new DataTable();
            dtItemextype = new DataTable();
            dtItemextype1 = new DataTable();
            dtItemextype3 = new DataTable();
            dtItemextype4 = new DataTable();
            dtMEDICARETYPE = new DataTable();
            dtItemextype5 = new DataTable();
            dtPharMatype = new DataTable();
            dtFreq = new DataTable();
            dtCATEID1 = new DataTable();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "SELECT * FROM T_AID_UNIT ";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtUnit);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = "SELECT ORDERCATEID_CHR,NAME_CHR FROM T_AID_BIH_ORDERCATE order by ORDERCATEID_CHR";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtCATEID1);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = "SELECT * FROM T_AID_MEDICINEPREPTYPE ";

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtMeicinePrep);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = "SELECT ASSISTCODE_VCHR,PHARMANAME_VCHR,PYCODE_VCHR,WBCODE_VCHR,PHARMAID_CHR, PARENTID_CHR FROM t_bse_pharmatype ";

            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtPharMatype);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"select USERCODE_CHR,USAGENAME_VCHR,USAGEID_CHR from t_bse_usagetype";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtuse);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"Select USERCODE_CHR,FREQNAME_CHR,FREQID_CHR From t_aid_recipefreq  ORDER BY USERCODE_CHR
";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtFreq);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"select USERCODE_CHR,VENDORNAME_VCHR,VENDORID_CHR,PYCODE_CHR,WBCODE_CHR from t_bse_vendor where PRODUCTTYPE_INT=1 and VENDORTYPE_INT!=1";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtvendor);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select * from T_AID_MEDICINETYPE";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtmedtype);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"Select USERCODE_CHR,TYPENAME_VCHR,TYPEID_CHR From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtItemextype);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"Select USERCODE_CHR,TYPENAME_VCHR,TYPEID_CHR From t_bse_chargeitemextype  Where flag_int='2' order by SORTCODE_INT";

            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtItemextype1);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"Select USERCODE_CHR,TYPENAME_VCHR,TYPEID_CHR From t_bse_chargeitemextype  Where flag_int='3' order by SORTCODE_INT";

            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtItemextype3);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"Select USERCODE_CHR,TYPENAME_VCHR,TYPEID_CHR From t_bse_chargeitemextype  Where flag_int='4' order by SORTCODE_INT";

            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtItemextype4);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select USERCODE_CHR,TYPENAME_VCHR,TYPEID_CHR from T_AID_MEDICARETYPE";

            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtMEDICARETYPE);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"Select USERCODE_CHR,TYPENAME_VCHR,TYPEID_CHR  From t_bse_chargeitemextype  Where flag_int='5' order by SORTCODE_INT";

            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtItemextype5);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"Select SETSTATUS_INT From t_sys_setting  Where SETID_CHR='0031'";
            DataTable dt1 = new DataTable();
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt1);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["SETSTATUS_INT"].ToString() == "0")
                {
                    Isuse = false;
                }
                else
                {
                    Isuse = true;
                }
            }
            return lngRes;
        }
        #endregion

        #region 获取执行医嘱分类名称
        /// <summary>
        /// 获取执行医嘱分类名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllBihCate(out DataTable tb)
        {
            long lngRes = 0;
            tb = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select * from t_aid_bih_orderperformcate order by SORT_INT";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref tb);
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


        #region 仓库药品维护

        #region 获取所有的药品信息
        /// <summary>
        /// 获取所有的药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMed(out DataTable tb)
        {
            long lngRes = 0;
            tb = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.MEDICINEID_CHR,a.ASSISTCODE_CHR,a.MEDICINENAME_VCHR,b.MEDICINETYPENAME_VCHR from T_BSE_MEDICINE a,T_AID_MEDICINETYPE b where a.MEDICINETYPEID_CHR=b.MEDICINETYPEID_CHR";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref tb);
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

        #region 修改对应ID
        /// <summary>
        ///  修改对应ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="medID"></param>
        /// <param name="id"></param>
        /// <param name="IDname"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyMEDICINESTDID(string medID, string id, string IDname)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"update t_bse_medicine set MEDICINESTDID_CHR='" + id + "',MEDICINESTDESC_VCHR='" + IDname + "' where MEDICINEID_CHR='" + medID + "'";
            try
            {

                lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region 药库数据维护
        #region 获取仓库类别
        /// <summary>
        /// 获取仓库类别
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMedType(out DataTable tb)
        {
            long lngRes = 0;
            tb = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select * from t_aid_storagetype";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref tb);
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
        #region 获仓库信息
        /// <summary>
        /// 获仓库信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllstorage(out DataTable tb)
        {
            long lngRes = 0;
            tb = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.*,b.STORAGETYPENAME_VCHR  from t_bse_storage a,t_aid_storagetype b where a.STORAGETYPEID_CHR=b.STORAGETYPEID_CHR";
            try
            {

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref tb);
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

        #region 插入仓库信息
        /// <summary>
        /// 插入仓库信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertStorageData(string strStorageTypeID, string strStorageName, out string newID)
        {
            newID = string.Empty;
            long lngRes = 0;
            newID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            newID = objHRPSvc.m_strGetNewID("t_bse_storage", "STORAGEID_CHR", 4);
            string strSQL = @"insert into t_bse_storage(storageid_chr,storagetypeid_chr,storagename_vchr) values(?,?,?)";
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = newID;
                objDPArr[1].Value = strStorageTypeID;
                objDPArr[2].Value = strStorageName;

                long lngEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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

        #region 修改仓库信息
        /// <summary>
        /// 修改仓库信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyStorageData(string p_strStorageTypeID, string p_strStorageName, string p_strStorageID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"update t_bse_storage set storagetypeid_chr=?,storagename_vchr=? where storageid_chr=?";
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strStorageTypeID;
                objDPArr[1].Value = p_strStorageName;
                objDPArr[2].Value = p_strStorageID;

                long lngEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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

        #region 删除仓库信息
        /// <summary>
        /// 删除仓库信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleStorageData(string strID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"delete t_bse_storage where STORAGEID_CHR='" + strID + "'";
            try
            {

                lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region 药库月购进报表统计模块
        /// <summary>
        /// 药库月购进报表统计模块
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="dtVendor"></param>
        /// <param name="dtord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetVendorData(string date1, string date2, out DataTable dtord, string strIsBreak)
        {
            dtord = new DataTable();
            long lngRes = 0;
            string strWhere = "";
            if (strIsBreak == "2")
            {
                strWhere = @" and a.SIGN_INT=2";
            }
            else
            {
                strWhere = @" and a.SIGN_INT=1";
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.VENDORID_CHR,c.VENDORNAME_VCHR,c.USERCODE_CHR,sum(b.QTY_DEC*b.BUYUNITPRICE_MNY) as TotailMoney  from t_opr_storageord a,t_opr_storageordde b,t_bse_vendor c  where a.STORAGEORDID_CHR=b.STORAGEORDID_CHR and  a.INORD_DAT between to_date('" + date1 + " 00:00:00" + "','yyyy-mm-dd hh24:mi:ss')  and to_date('" + date2 + " 23:59:59" + "','yyyy-mm-dd hh24:mi:ss')  and a.PSTATUS_INT=2 and a.VENDORID_CHR=c.VENDORID_CHR " + strWhere + " group by a.VENDORID_CHR,c.VENDORNAME_VCHR,c.USERCODE_CHR";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtord);
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

        #region 财务统计报表

        [AutoComplete]
        public long m_lngGetReportData(string date1, string date2, out DataTable dtdein, out DataTable dtDeout)
        {
            dtdein = new DataTable();
            dtDeout = new DataTable();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT   a.medicineid_chr, b.medicinename_vchr, a.syslotno_chr,
         a.buyunitprice_mny, b.medspec_vchr,
         a.aimunit_int
              , a.sign_int, SUM (a.qty_dec) AS buyqty,
         SUM (a.buyunitprice_mny * a.qty_dec) AS buymoney,b.OPUNIT_CHR
    FROM t_opr_storageordde a, t_bse_medicine b
   WHERE a.medicineid_chr = b.medicineid_chr
     AND a.ord_dat BETWEEN TO_DATE ('" + date1 + @" 00:00:00',
                                    'yyyy-mm-dd hh24:mi:ss'
                                   )
                       AND TO_DATE ('" + date2 + @" 23:59:59',
                                    'yyyy-mm-dd hh24:mi:ss'
                                   ) AND (sign_int = 1 or sign_int = 3)
GROUP BY a.medicineid_chr,
         b.medicinename_vchr,
         a.syslotno_chr,
         a.buyunitprice_mny,
         b.medspec_vchr,
         a.aimunit_int,
         a.sign_int,
         b.OPUNIT_CHR";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtdein);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT   a.medicineid_chr, b.medicinename_vchr, a.syslotno_chr,
         a.saleunitprice_mny, b.medspec_vchr,
         a.aimunit_int 
               ,a.sign_int, SUM (a.qty_dec) AS saleqty,
         SUM (a.qty_dec * a.saleunitprice_mny) salemoney,b.OPUNIT_CHR
    FROM t_opr_storageordde a, t_bse_medicine b
   WHERE a.medicineid_chr = b.medicineid_chr
     AND a.ord_dat BETWEEN TO_DATE ('" + date1 + @" 00:00:00',
                                    'yyyy-mm-dd hh24:mi:ss'
                                   )
                       AND TO_DATE ('" + date2 + @" 23:59:59',
                                    'yyyy-mm-dd hh24:mi:ss'
                                   ) AND (sign_int = 2 or sign_int = 4)
GROUP BY a.medicineid_chr,
         b.medicinename_vchr,
         a.syslotno_chr,
         a.saleunitprice_mny,
         b.medspec_vchr,
         a.aimunit_int,
         a.sign_int,
         b.OPUNIT_CHR";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtDeout);
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

        #region 药品月购进报表
        /// <summary>
        /// 药品月购进报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtdein">供应商信息</param>
        /// <param name="dtEN">西药费用</param>
        /// <param name="dtCH">中药费用</param>
        /// <param name="dtEH">中成药费用</param>
        /// <param name="dtImport">进口药费用</param>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportDataOfMonth(out DataTable dtdein, out DataTable dtEN, out DataTable dtCH, out DataTable dtEH, out DataTable dtImport, string startDate, string EndDate, int statusINT)
        {
            dtdein = new DataTable();
            dtEN = new DataTable();
            dtCH = new DataTable();
            dtEH = new DataTable();
            dtImport = new DataTable();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strWhere = "";

            if (statusINT == 1)
            {
                strWhere = @" and  a.SIGN_INT=1";
            }
            else if (statusINT == 2)
            {
                strWhere = @" and a.SIGN_INT=2";
            }
            string strSQL = @"select DISTINCT(c.VENDORNAME_VCHR) from  t_opr_storageord a,t_bse_vendor c,t_aid_storageordtype f where a.VENDORID_CHR=c.vendorid_chr and a.INORD_DAT BETWEEN to_date('" + startDate + @" 00:00:00','yyyy-mm-dd hh24:mi:ss') and  to_date('" + EndDate + @" 23:59:59','yyyy-mm-dd hh24:mi:ss') and a.STORAGEORDTYPEID_CHR=f.STORAGEORDTYPEID_CHR" + strWhere;

















            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtdein);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"SELECT   c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Imbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Imsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ImLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' and INORD_DAT BETWEEN to_date('" + startDate + @" 00:00:00','yyyy-mm-dd hh24:mi:ss') and  to_date('" + EndDate + @" 23:59:59','yyyy-mm-dd hh24:mi:ss')" + strWhere + "  GROUP BY c.vendorname_vchr";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtImport);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"SELECT   c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) buymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) salmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EnLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 and INORD_DAT BETWEEN to_date('" + startDate + @" 00:00:00','yyyy-mm-dd hh24:mi:ss') and  to_date('" + EndDate + @" 23:59:59','yyyy-mm-dd hh24:mi:ss')" + strWhere + "  GROUP BY c.vendorname_vchr";

            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEN);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT   c.vendorname_vchr,

                 SUM (b.buyunitprice_mny * b.qty_dec) Chbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Chsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ChLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        and INORD_DAT BETWEEN to_date('" + startDate + @" 00:00:00','yyyy-mm-dd hh24:mi:ss') and  to_date('" + EndDate + @" 23:59:59','yyyy-mm-dd hh24:mi:ss')  GROUP BY c.vendorname_vchr";

            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCH);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"SELECT   c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Ehbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Ehsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        and INORD_DAT BETWEEN to_date('" + startDate + @" 00:00:00','yyyy-mm-dd hh24:mi:ss') and  to_date('" + EndDate + @" 23:59:59','yyyy-mm-dd hh24:mi:ss')  GROUP BY c.vendorname_vchr";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEH);
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
        #region 药品月购进报表
        /// <summary>
        /// 药品月购进报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtdein">供应商信息</param>
        /// <param name="dtENAim">西药中标费用</param>
        /// <param name="dtENNoAim">西药非中标费用</param>
        /// <param name="dtCHAim">中药中标费用</param>
        /// <param name="dtCHNoAim">中药非中标费用</param>
        /// <param name="dtEHAim">中成中标药费用</param>
        /// <param name="dtEHNoAim">中成非中标药费用</param>
        /// <param name="dtImportAim">进口中标药费用</param>
        /// <param name="dtImportNoAim">进口非中标药费用</param>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportDataOfMonth(out DataTable dtdein, out DataTable dtENAim, out DataTable dtENNoAim, out DataTable dtCHAim, out DataTable dtCHNoAim, out DataTable dtEHAim, out DataTable dtEHNoAim, out DataTable dtImportAim, out DataTable dtImportNoAim, System.Collections.Generic.List<string> arrList, int statusINT)
        {
            dtdein = new DataTable();
            dtENAim = new DataTable();
            dtENNoAim = new DataTable();
            dtCHAim = new DataTable();
            dtCHNoAim = new DataTable();


            dtEHAim = new DataTable();
            dtEHNoAim = new DataTable();
            dtImportAim = new DataTable();
            dtImportNoAim = new DataTable();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            string strWhere = "";
            if (statusINT == 5)
            {
                strWhere = " and (a.SIGN_INT=1 or a.SIGN_INT=4)";
            }
            else
            {
                strWhere = " and a.SIGN_INT=" + statusINT.ToString();
            }
            string strWherePeriod = "";
            if (arrList.Count > 0)
            {
                for (int i1 = 0; i1 < arrList.Count; i1++)
                {

                    if (i1 == 0)
                    {
                        strWherePeriod += @" and (a.PERIODID_CHR='" + (string)arrList[i1] + "'";
                    }
                    else
                    {
                        strWherePeriod += @" or a.PERIODID_CHR='" + (string)arrList[i1] + "'";
                    }
                }
                strWherePeriod += @" )";
            }
            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                strSQL = @"select DISTINCT(c.VENDORNAME_VCHR) from  t_opr_storageord a,t_bse_vendor c where a.VENDORID_CHR=c.vendorid_chr " + strWherePeriod + @"  and a.PSTATUS_INT=2 " + strWhere;
            }
            else
            {
                strSQL = @"select DISTINCT(c.MEDSTORENAME_VCHR) as VENDORNAME_VCHR from  t_opr_storageord a,t_bse_medstore c  where a.DEPTID_CHR=c.MEDSTOREID_CHR " + strWherePeriod + "  and  a.PSTATUS_INT=2" + strWhere;
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtdein);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                if (statusINT == 5)
                {
                    strSQL = @"select vendorname_vchr,sum(AimImbuymoney1) as AimImbuymoney,sum(AimImsalmoney1) as AimImsalmoney,sum(ImLIMITmoney1) as ImLIMITmoney from ((SELECT c.vendorname_vchr,
                 sum(b.buyunitprice_mny * b.qty_dec) AimImbuymoney1,
                sum(b.SALEUNITPRICE_MNY * b.qty_dec) AimImsalmoney1,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec)ImLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' " + strWherePeriod + @" and AIMUNIT_INT=1 and a.SIGN_INT=1 GROUP BY c.vendorname_vchr)
            UNION (SELECT c.vendorname_vchr,
                 sum(0-b.buyunitprice_mny * b.qty_dec) AimImbuymoney1,
                 sum(0-b.SALEUNITPRICE_MNY * b.qty_dec) AimImsalmoney1,
sum(0-b.LIMITUNITPRICE_MNY* b.qty_dec) ImLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' " + strWherePeriod + @" and AIMUNIT_INT=1 and a.SIGN_INT=4 GROUP BY c.vendorname_vchr))group by vendorname_vchr";
                }
                else
                {
                    strSQL = @"SELECT a.SIGN_INT, c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) AimImbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) AimImsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ImLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' " + strWherePeriod + @" and AIMUNIT_INT=1 " + strWhere + " GROUP BY c.vendorname_vchr,a.SIGN_INT";
                }
            }
            else
            {
                strSQL = @"SELECT  c.MEDSTORENAME_VCHR  as vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) AimImbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) AimImsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ImLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_medstore c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.DEPTID_CHR = c.MEDSTOREID_CHR
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' " + strWherePeriod + " and AIMUNIT_INT=1 " + strWhere + " GROUP BY c.MEDSTORENAME_VCHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtImportAim);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                if (statusINT == 5)
                {
                    strSQL = @" select vendorname_vchr ,sum(Imbuymoney1) as Imbuymoney,sum(Imsalmoney1) as Imsalmoney,sum(ImLIMITmoney1) as ImLIMITmoney from((SELECT c.vendorname_vchr,
                 sum(0-b.buyunitprice_mny * b.qty_dec) Imbuymoney1,
                sum(0-b.SALEUNITPRICE_MNY * b.qty_dec) Imsalmoney1,
sum(0-b.LIMITUNITPRICE_MNY* b.qty_dec) ImLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' " + strWherePeriod + @"  and (AIMUNIT_INT=0 or AIMUNIT_INT is null) and a.SIGN_INT=4 GROUP BY c.vendorname_vchr) UNION (SELECT c.vendorname_vchr,
                 sum(b.buyunitprice_mny * b.qty_dec) Imbuymoney1,
                sum(b.SALEUNITPRICE_MNY * b.qty_dec) Imsalmoney1,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ImLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' " + strWherePeriod + @"  and (AIMUNIT_INT=0 or AIMUNIT_INT is null) and a.SIGN_INT=1 GROUP BY c.vendorname_vchr)) group by vendorname_vchr";
                }
                else
                {
                    strSQL = @"SELECT  c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Imbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Imsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ImLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' " + strWherePeriod + @"  and (AIMUNIT_INT=0 or AIMUNIT_INT is null) " + strWhere + " GROUP BY c.vendorname_vchr";
                }
            }
            else
            {
                strSQL = @"SELECT  c.MEDSTORENAME_VCHR  as vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Imbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Imsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ImLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_medstore c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
              AND a.DEPTID_CHR = c.MEDSTOREID_CHR
             AND b.medicineid_chr = e.medicineid_chr
             AND e.ISIMPORT_CHR = 'T' " + strWherePeriod + @"  and (AIMUNIT_INT=0 or AIMUNIT_INT is null) " + strWhere + " GROUP BY c.MEDSTORENAME_VCHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtImportNoAim);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                if (statusINT == 5)
                {
                    strSQL = @"select vendorname_vchr,sum(Aimbuymoney1) as Aimbuymoney,sum(Aimsalmoney1) as Aimsalmoney,sum(EnLIMITmoney1) as EnLIMITmoney from ((SELECT   c.vendorname_vchr,
                 sum(b.buyunitprice_mny * b.qty_dec) Aimbuymoney1,
                 sum(b.SALEUNITPRICE_MNY * b.qty_dec) Aimsalmoney1,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec)  EnLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 " + strWherePeriod + @" and AIMUNIT_INT=1 and a.SIGN_INT=1 GROUP BY c.vendorname_vchr) UNION (SELECT   c.vendorname_vchr,
                 sum(0-b.buyunitprice_mny * b.qty_dec) Aimbuymoney1,
                 sum(0-b.SALEUNITPRICE_MNY * b.qty_dec) Aimsalmoney1,
sum(0-b.LIMITUNITPRICE_MNY* b.qty_dec)  EnLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 " + strWherePeriod + @" and AIMUNIT_INT=1 and a.SIGN_INT=4 GROUP BY c.vendorname_vchr))  GROUP BY vendorname_vchr";
                }
                else
                {
                    strSQL = @"SELECT   c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Aimbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Aimsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EnLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 " + strWherePeriod + @" and AIMUNIT_INT=1 " + strWhere + " GROUP BY c.vendorname_vchr";
                }
            }
            else
            {
                strSQL = @"SELECT   c.MEDSTORENAME_VCHR  as vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Aimbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Aimsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EnLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_medstore c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.DEPTID_CHR = c.MEDSTOREID_CHR
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 " + strWherePeriod + @" and AIMUNIT_INT=1 " + strWhere + " GROUP BY c.MEDSTORENAME_VCHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtENAim);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                if (statusINT == 5)
                {
                    strSQL = @"select vendorname_vchr,sum(buymoney1) buymoney,sum(salmoney1)  salmoney,sum(EnLIMITmoney1) EnLIMITmoney from((SELECT c.vendorname_vchr,
                 sum(0-b.buyunitprice_mny * b.qty_dec) buymoney1,
                 sum(0-b.SALEUNITPRICE_MNY * b.qty_dec) salmoney1,
sum(0-b.LIMITUNITPRICE_MNY* b.qty_dec) EnLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 " + strWherePeriod + @" and (AIMUNIT_INT=0 or AIMUNIT_INT is null) and a.SIGN_INT=4 GROUP BY c.vendorname_vchr) UNION (SELECT c.vendorname_vchr,
                 sum(b.buyunitprice_mny * b.qty_dec) buymoney1,
                 sum(b.SALEUNITPRICE_MNY * b.qty_dec) salmoney1,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EnLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 " + strWherePeriod + @" and (AIMUNIT_INT=0 or AIMUNIT_INT is null) and a.SIGN_INT=1 GROUP BY c.vendorname_vchr)) GROUP BY vendorname_vchr";

                }
                else
                {
                    strSQL = @"SELECT   c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) buymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) salmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EnLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 " + strWherePeriod + @" and (AIMUNIT_INT=0 or AIMUNIT_INT is null) " + strWhere + " GROUP BY c.vendorname_vchr";
                }
            }
            else
            {
                strSQL = @"SELECT   c.MEDSTORENAME_VCHR  as vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) buymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) salmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EnLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_medstore c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.DEPTID_CHR = c.MEDSTOREID_CHR
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 1 " + strWherePeriod + @" and (AIMUNIT_INT=0 or AIMUNIT_INT is null) " + strWhere + " GROUP BY c.MEDSTORENAME_VCHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtENNoAim);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                if (statusINT == 5)
                {
                    strSQL = @"select vendorname_vchr,sum(nvl(AimChbuymoney1,0))  AimChbuymoney,sum(nvl(AimChsalmoney1,0)) as  AimChsalmoney,sum(nvl(ChLIMITmoney1,0)) as  ChLIMITmoney from((SELECT c.vendorname_vchr,
                 b.buyunitprice_mny * b.qty_dec AimChbuymoney1,
                 b.SALEUNITPRICE_MNY * b.qty_dec AimChsalmoney1,
b.LIMITUNITPRICE_MNY* b.qty_dec ChLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        " + strWherePeriod + @"  and AIMUNIT_INT=1 and a.SIGN_INT=1) UNION (SELECT c.vendorname_vchr,
                 0-b.buyunitprice_mny * b.qty_dec AimChbuymoney1,
                 0-b.SALEUNITPRICE_MNY * b.qty_dec AimChsalmoney1,
0-b.LIMITUNITPRICE_MNY* b.qty_dec ChLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        " + strWherePeriod + "  and AIMUNIT_INT=1 and a.SIGN_INT=4))GROUP BY vendorname_vchr";

                }
                else
                {
                    strSQL = @"SELECT  c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) AimChbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) AimChsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ChLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        " + strWherePeriod + @"  and AIMUNIT_INT=1 and a.SIGN_INT=" + statusINT.ToString() + " GROUP BY c.vendorname_vchr";
                }
            }
            else
            {
                strSQL = @"SELECT  c.MEDSTORENAME_VCHR  as vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) AimChbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) AimChsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ChLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_medstore c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.DEPTID_CHR = c.MEDSTOREID_CHR
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        " + strWherePeriod + @"  and AIMUNIT_INT=1 " + strWhere + " GROUP BY c.MEDSTORENAME_VCHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCHAim);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                if (statusINT == 5)
                {
                    strSQL = @"select vendorname_vchr,sum(Chbuymoney1) as   Chbuymoney,sum(Chsalmoney1) as  Chsalmoney,sum(ChLIMITmoney1) as  ChLIMITmoney from((SELECT c.vendorname_vchr,
                 sum(b.buyunitprice_mny * b.qty_dec) Chbuymoney1,
                sum(b.SALEUNITPRICE_MNY * b.qty_dec) Chsalmoney1,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ChLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        " + strWherePeriod + @"  and (AIMUNIT_INT=0 or AIMUNIT_INT is null) and a.SIGN_INT=1 GROUP BY c.vendorname_vchr) UNION (SELECT c.vendorname_vchr,
                sum(0- b.buyunitprice_mny * b.qty_dec) Chbuymoney1,
                sum(0-b.SALEUNITPRICE_MNY * b.qty_dec) Chsalmoney1,
sum(0-b.LIMITUNITPRICE_MNY* b.qty_dec) ChLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        " + strWherePeriod + @" and a.SIGN_INT=4 GROUP BY c.vendorname_vchr)) GROUP BY vendorname_vchr";

                }
                else
                {
                    strSQL = @"SELECT  c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Chbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Chsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ChLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        " + strWherePeriod + @"  and (AIMUNIT_INT=0 or AIMUNIT_INT is null) " + strWhere + " GROUP BY c.vendorname_vchr";
                }
            }
            else
            {
                strSQL = @"SELECT  c.MEDSTORENAME_VCHR  as vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Chbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Chsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) ChLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_medstore c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.DEPTID_CHR = c.MEDSTOREID_CHR
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 2
        " + strWherePeriod + @" and (AIMUNIT_INT=0 or AIMUNIT_INT is null) " + strWhere + " GROUP BY c.MEDSTORENAME_VCHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCHNoAim);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                if (statusINT == 5)
                {
                    strSQL = @"select vendorname_vchr,sum(AimEhbuymoney1) as  AimEhbuymoney,sum(AimEhsalmoney1) as  AimEhsalmoney,sum(EhLIMITmoney1) as  EhLIMITmoney from((SELECT   c.vendorname_vchr,
                 sum(b.buyunitprice_mny * b.qty_dec) AimEhbuymoney1,
                sum(b.SALEUNITPRICE_MNY * b.qty_dec) AimEhsalmoney1,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        " + strWherePeriod + @" and  AIMUNIT_INT=1 and a.SIGN_INT=1 GROUP BY c.vendorname_vchr) UNION (SELECT   c.vendorname_vchr,
                 sum(0-b.buyunitprice_mny * b.qty_dec) AimEhbuymoney1,
                sum(0-b.SALEUNITPRICE_MNY * b.qty_dec) AimEhsalmoney1,
sum(0-b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        " + strWherePeriod + @" and  AIMUNIT_INT=1 and a.SIGN_INT=4 GROUP BY c.vendorname_vchr)) GROUP BY vendorname_vchr";

                }
                else
                {
                    strSQL = @"SELECT   c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) AimEhbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) AimEhsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        " + strWherePeriod + @" and  AIMUNIT_INT=1  " + strWhere + " GROUP BY c.vendorname_vchr";
                }
            }
            else
            {
                strSQL = @"SELECT c.MEDSTORENAME_VCHR  as vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) AimEhbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) AimEhsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_medstore c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.DEPTID_CHR = c.MEDSTOREID_CHR
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        " + strWherePeriod + @" and  AIMUNIT_INT=1  " + strWhere + " GROUP BY c.MEDSTORENAME_VCHR";

            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEHAim);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (statusINT == 1 || statusINT == 4 || statusINT == 5)
            {
                if (statusINT == 5)
                {
                    strSQL = @"select vendorname_vchr,sum(Ehbuymoney1) as  Ehbuymoney,sum(Ehsalmoney1) as  Ehsalmoney,sum(EhLIMITmoney1) as  EhLIMITmoney from((SELECT c.vendorname_vchr,
                 sum(b.buyunitprice_mny * b.qty_dec) Ehbuymoney1,
                 sum(b.SALEUNITPRICE_MNY * b.qty_dec) Ehsalmoney1,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        " + strWherePeriod + @" and  (AIMUNIT_INT=0 or AIMUNIT_INT is null) and a.SIGN_INT=1 GROUP BY c.vendorname_vchr) UNION (SELECT c.vendorname_vchr,
                 sum(0-b.buyunitprice_mny * b.qty_dec) Ehbuymoney1,
                 sum(0-b.SALEUNITPRICE_MNY * b.qty_dec) Ehsalmoney1,
sum(0-b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney1
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        " + strWherePeriod + @" and  (AIMUNIT_INT=0 or AIMUNIT_INT is null) and a.SIGN_INT=4 GROUP BY c.vendorname_vchr)) GROUP BY vendorname_vchr";

                }
                else
                {
                    strSQL = @"SELECT   c.vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Ehbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Ehsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_vendor c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.vendorid_chr = c.vendorid_chr
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        " + strWherePeriod + @" and  (AIMUNIT_INT=0 or AIMUNIT_INT is null) " + strWhere + " GROUP BY c.vendorname_vchr";
                }
            }
            else
            {
                strSQL = @"SELECT   c.MEDSTORENAME_VCHR  as vendorname_vchr,
                 SUM (b.buyunitprice_mny * b.qty_dec) Ehbuymoney,
                 SUM (b.SALEUNITPRICE_MNY * b.qty_dec) Ehsalmoney,
sum(b.LIMITUNITPRICE_MNY* b.qty_dec) EhLIMITmoney
            FROM t_opr_storageord a,
                 t_opr_storageordde b,
                 t_bse_medstore c,
                 t_bse_medicine e
           WHERE b.storageordid_chr = a.storageordid_chr
             AND a.DEPTID_CHR = c.MEDSTOREID_CHR
             AND b.medicineid_chr = e.medicineid_chr
             AND e.medicinetypeid_chr = 3
        " + strWherePeriod + @" and  (AIMUNIT_INT=0 or AIMUNIT_INT is null) " + strWhere + " GROUP BY c.MEDSTORENAME_VCHR";
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEHNoAim);
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


        #region 药品进销存报表
        /// <summary>
        /// 药品进销存报表(新)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="arrPrID">要统计的财务期列表</param>
        /// <param name="strUpPr">上一期的财务期</param>
        /// <param name="dt">返回数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportDataOfInAndOut(System.Collections.Generic.List<string> arrPrID, string strUpPr, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            string strWhere = "";
            string strWhere2 = "";
            string strWhere1 = " and a.PERIODID_CHR='" + strUpPr + "'";
            if (arrPrID.Count > 0)
            {
                for (int i1 = 0; i1 < arrPrID.Count; i1++)
                {
                    strSQL = arrPrID[i1];
                    if (i1 == 0)
                    {
                        strWhere = "  and (a.PERIODID_CHR='" + strSQL + "'";
                        if (arrPrID.Count == 1)
                        {
                            strWhere += ")";
                        }
                    }
                    else
                    {
                        if (i1 == arrPrID.Count - 1)
                        {
                            strWhere += ")";
                        }
                        else
                        {
                            strWhere += " or a.PERIODID_CHR='" + strSQL + "'";
                        }
                    }
                    if (i1 == arrPrID.Count - 1)
                    {
                        strWhere2 = @" and a.PERIODID_CHR='" + strSQL + "'";
                    }
                }
            }

            strSQL = @"select k.*,f.*,h.* from(SELECT  '上期结存' as RowName, sum(b.realqty_dec * b.BUYPRICE_MNY) AS WestMedinmoney,
       sum(b.realqty_dec * b.SALEUNITPRICE_MNY) AS WestMedsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
    and  PSTATUS_INT=1 and FLAG_INT=0
   AND c.medicinetypeid_chr = 1" + strWhere1 + @")k,
(SELECT sum(b.realqty_dec *  b.BUYPRICE_MNY) AS WCinmoney,
       sum(b.realqty_dec *  b.SALEUNITPRICE_MNY) AS WCsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   and  PSTATUS_INT=1 and FLAG_INT=0
   AND c.medicinetypeid_chr = 3" + strWhere1 + @")f,
(SELECT sum(b.realqty_dec *  b.BUYPRICE_MNY) AS CHinmoney,
       sum(b.realqty_dec *  b.SALEUNITPRICE_MNY) AS CHsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   and  PSTATUS_INT=1 and FLAG_INT=0
   AND c.medicinetypeid_chr = 2" + strWhere1 + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select k.*,f.*,h.* from(SELECT '本期购入' as RowName,sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WestMedinmoney,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS WestMedsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=1
   AND c.medicinetypeid_chr = 1" + strWhere + @")k,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WCinmoney,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS WCsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=1
   AND c.medicinetypeid_chr = 3" + strWhere + @")f,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS CHinmoney,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS CHsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=1
   AND c.medicinetypeid_chr = 2" + strWhere + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt1);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select k.*,f.*,h.* from(SELECT '本期退库' as RowName,sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WestMedinmoney,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS WestMedsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=3
   AND c.medicinetypeid_chr = 1" + strWhere + @")k,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WCinmoney,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS WCsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=3
   AND c.medicinetypeid_chr = 3" + strWhere + @")f,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS CHinmoney,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS CHsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=3
   AND c.medicinetypeid_chr = 2" + strWhere + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt2);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select k.*,f.*,h.* from(SELECT '本期发出' as RowName,0 AS WestMedinmoney,
       sum(b.QTY_DEC *b.SALEUNITPRICE_MNY) AS WestMedsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=2
   AND c.medicinetypeid_chr = 1" + strWhere + @")k,
(SELECT 0 AS WCinmoney,
       sum(b.QTY_DEC *b.SALEUNITPRICE_MNY) AS WCsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=2
   AND c.medicinetypeid_chr = 3" + strWhere + @")f,
(SELECT 0 AS CHinmoney,
       sum(b.QTY_DEC *b.SALEUNITPRICE_MNY) AS CHsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=2
   AND c.medicinetypeid_chr = 2" + strWhere + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt3);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select k.*,f.*,h.* from(SELECT '本期退货' as RowName,sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WestMedinmoney,
       sum(b.QTY_DEC *b.SALEUNITPRICE_MNY) AS WestMedsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
    and a.PSTATUS_INT=2
   AND a.sign_int=4
   AND c.medicinetypeid_chr = 1" + strWhere + @")k,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WCinmoney,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS WCsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
    and a.PSTATUS_INT=2
   AND a.sign_int=4
   AND c.medicinetypeid_chr = 3" + strWhere + @")f,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS CHinmoney,
       sum(b.QTY_DEC *b.SALEUNITPRICE_MNY) AS CHsalemoney
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
    and a.PSTATUS_INT=2
   AND a.sign_int=4
   AND c.medicinetypeid_chr = 2" + strWhere + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt4);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"select k.*,f.*,h.* from(SELECT '本期调价盈亏' as RowName, 0 AS WestMedinmoney,
       sum((b.CHANGEPRICE_MNY-b.CURPRICE_MNY) * b.CURQTY_DEC) AS WestMedsalemoney
  FROM t_opr_medicinepricechgappl a, t_opr_medicinepricechgapplde b, t_bse_medicine c
 WHERE a.MEDICINEPRICECHGAPPLID_CHR = b.MEDICINEPRICECHGAPPLID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr = 1" + strWhere + @")k,
(SELECT 0 AS WCinmoney,
       sum((b.CHANGEPRICE_MNY-b.CURPRICE_MNY) * b.CURQTY_DEC)  AS WCsalemoney
  FROM t_opr_medicinepricechgappl a, t_opr_medicinepricechgapplde b, t_bse_medicine c
 WHERE a.MEDICINEPRICECHGAPPLID_CHR = b.MEDICINEPRICECHGAPPLID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr = 3" + strWhere + @")f,
(SELECT 0 AS CHinmoney,
       sum((b.CHANGEPRICE_MNY-b.CURPRICE_MNY) * b.CURQTY_DEC) AS CHsalemoney
  FROM t_opr_medicinepricechgappl a, t_opr_medicinepricechgapplde b, t_bse_medicine c
 WHERE a.MEDICINEPRICECHGAPPLID_CHR = b.MEDICINEPRICECHGAPPLID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND c.medicinetypeid_chr = 2" + strWhere + @")h";

            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt5);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"select k.*,f.*,h.* from(SELECT  '本期结存' as RowName, sum(b.realqty_dec * b.BUYPRICE_MNY) AS WestMedinmoney,
       sum(b.realqty_dec * b.SALEUNITPRICE_MNY) AS WestMedsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
    and  PSTATUS_INT=1 and FLAG_INT=0
   AND c.medicinetypeid_chr = 1" + strWhere2 + @")k,
(SELECT sum(b.realqty_dec *  b.BUYPRICE_MNY) AS WCinmoney,
       sum(b.realqty_dec *  b.SALEUNITPRICE_MNY) AS WCsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   and  PSTATUS_INT=1 and FLAG_INT=0
   AND c.medicinetypeid_chr = 3" + strWhere2 + @")f,
(SELECT sum(b.realqty_dec *  b.BUYPRICE_MNY) AS CHinmoney,
       sum(b.realqty_dec *  b.SALEUNITPRICE_MNY) AS CHsalemoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr
   and  PSTATUS_INT=1 and FLAG_INT=0
   AND c.medicinetypeid_chr = 2" + strWhere2 + @")h";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt6);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            dt.ImportRow(dt1.Rows[0]);
            dt.ImportRow(dt2.Rows[0]);
            dt.ImportRow(dt3.Rows[0]);
            dt.ImportRow(dt4.Rows[0]);
            dt.ImportRow(dt5.Rows[0]);
            dt.ImportRow(dt6.Rows[0]);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("Totailinmoney");
                dt.Columns.Add("Totailsalemoney");
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    double Totailinmoney = 0;
                    double Totailsalemoney = 0;
                    for (int f3 = 1; f3 < dt.Columns.Count - 2; f3++)
                    {
                        if (f3 % 2 == 0)
                        {
                            if (dt.Rows[i1][f3].ToString() != "")
                                Totailsalemoney += double.Parse(dt.Rows[i1][f3].ToString());
                        }
                        else
                        {
                            if (dt.Rows[i1][f3].ToString() != "")
                                Totailinmoney += double.Parse(dt.Rows[i1][f3].ToString());
                        }
                    }
                    dt.Rows[i1]["Totailinmoney"] = Totailinmoney;
                    dt.Rows[i1]["Totailsalemoney"] = Totailsalemoney;
                }
            }
            return lngRes;
        }

        #endregion

        #region 药品年购进统计报表
        /// <summary>
        /// 药品年购进统计报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="arrPrID">一年中的所有财务期</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportDataOfInAndOutYear(System.Collections.Generic.List<string> arrPrID, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            DataTable dt4 = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            dt.Columns.Add("ROWNAME");
            dt.Columns.Add("WESTMEDINMONEY");
            dt.Columns.Add("WESTMEDSALEMONEY");
            dt.Columns.Add("WCINMONEY");
            dt.Columns.Add("WCSALEMONEY");
            dt.Columns.Add("CHINMONEY");
            dt.Columns.Add("CHSALEMONEY");
            dt.Columns.Add("Totailinmoney");
            dt.Columns.Add("Totailsalemoney");
            if (arrPrID.Count > 0)
            {
                for (int i1 = 0; i1 < arrPrID.Count; i1++)
                {
                    strSQL = @"select g.RowName,(nvl(g.WestMedinmoney1,0)-nvl(r.WestMedinmoney1,0)) as WestMedinmoney,(nvl(g.WESTMEDSALEMONEY1,0)-nvl(r.WESTMEDSALEMONEY1,0)) as WESTMEDSALEMONEY,(nvl(g.WCINMONEY1,0)-nvl(r.WCINMONEY1,0)) as WCINMONEY,(nvl(g.WCSALEMONEY1,0)-nvl(r.WCSALEMONEY1,0)) as WCSALEMONEY,(nvl(g.CHINMONEY1,0)-nvl(r.CHINMONEY1,0)) as CHINMONEY ,(nvl(g.CHSALEMONEY1,0)-nvl(r.CHSALEMONEY1,0)) as CHSALEMONEY from(select k.*,f.*,h.* from(SELECT '" + (string)arrPrID[i1] + @"' as RowName,sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WestMedinmoney1,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS WestMedsalemoney1
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=1
   AND c.medicinetypeid_chr = 1 and a.PERIODID_CHR='" + (string)arrPrID[i1] + @"')k,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WCinmoney1,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS WCsalemoney1
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=1
   AND c.medicinetypeid_chr = 3 and a.PERIODID_CHR='" + (string)arrPrID[i1] + @"')f,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS CHinmoney1,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS CHsalemoney1
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=1
   AND c.medicinetypeid_chr = 2 and a.PERIODID_CHR='" + (string)arrPrID[i1] + @"')h) g,(select k.*,f.*,h.* from(SELECT '" + (string)arrPrID[i1] + @"' as RowName,sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WestMedinmoney1,
       sum(b.QTY_DEC *b.SALEUNITPRICE_MNY) AS WestMedsalemoney1
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=4
   AND c.medicinetypeid_chr = 1 and a.PERIODID_CHR='" + (string)arrPrID[i1] + @"')k,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS WCinmoney1,
       sum(b.QTY_DEC * b.SALEUNITPRICE_MNY) AS WCsalemoney1
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=4
   AND c.medicinetypeid_chr = 3 and a.PERIODID_CHR='" + (string)arrPrID[i1] + @"')f,
(SELECT sum(b.BUYUNITPRICE_MNY * b.QTY_DEC) AS CHinmoney1,
       sum(b.QTY_DEC *b.SALEUNITPRICE_MNY) AS CHsalemoney1
  FROM t_opr_storageord a, t_opr_storageordde b, t_bse_medicine c
 WHERE a.STORAGEORDID_CHR = b.STORAGEORDID_CHR
   AND b.medicineid_chr = c.medicineid_chr
   and a.PSTATUS_INT=2
   AND a.sign_int=4
   AND c.medicinetypeid_chr = 2 and a.PERIODID_CHR='" + (string)arrPrID[i1] + @"')h) r";
                    try
                    {

                        lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt4);
                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (dt4.Rows.Count > 0)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["ROWNAME"] = dt4.Rows[0]["ROWNAME"];
                        newRow["WESTMEDINMONEY"] = dt4.Rows[0]["WESTMEDINMONEY"];
                        newRow["WESTMEDSALEMONEY"] = dt4.Rows[0]["WESTMEDSALEMONEY"];
                        newRow["WCINMONEY"] = dt4.Rows[0]["WCINMONEY"];
                        newRow["WCSALEMONEY"] = dt4.Rows[0]["WCSALEMONEY"];
                        newRow["CHINMONEY"] = dt4.Rows[0]["CHINMONEY"];
                        newRow["CHSALEMONEY"] = dt4.Rows[0]["CHSALEMONEY"];
                        if (newRow["WESTMEDINMONEY"] == System.DBNull.Value || newRow["WESTMEDINMONEY"].ToString() == "")
                        {
                            newRow["WESTMEDINMONEY"] = 0;
                        }
                        if (newRow["WCINMONEY"] == System.DBNull.Value || newRow["WCINMONEY"].ToString() == "")
                        {
                            newRow["WCINMONEY"] = 0;
                        }
                        if (newRow["CHINMONEY"] == System.DBNull.Value || newRow["CHINMONEY"].ToString() == "")
                        {
                            newRow["CHINMONEY"] = 0;
                        }
                        newRow["Totailinmoney"] = decimal.Parse(newRow["WESTMEDINMONEY"].ToString()) + decimal.Parse(newRow["WCINMONEY"].ToString()) + decimal.Parse(newRow["CHINMONEY"].ToString());
                        if (newRow["WESTMEDSALEMONEY"] == System.DBNull.Value || newRow["WESTMEDSALEMONEY"].ToString() == "")
                        {
                            newRow["WESTMEDSALEMONEY"] = 0;
                        }
                        if (newRow["WCSALEMONEY"] == System.DBNull.Value || newRow["WCSALEMONEY"].ToString() == "")
                        {
                            newRow["WCSALEMONEY"] = 0;
                        }
                        if (newRow["CHSALEMONEY"] == System.DBNull.Value || newRow["CHSALEMONEY"].ToString() == "")
                        {
                            newRow["CHSALEMONEY"] = 0;
                        }
                        newRow["Totailsalemoney"] = decimal.Parse(newRow["WESTMEDSALEMONEY"].ToString()) + decimal.Parse(newRow["WCSALEMONEY"].ToString()) + decimal.Parse(newRow["CHSALEMONEY"].ToString());
                        dt.Rows.Add(newRow);
                    }
                }
            }
            return lngRes;
        }

        #endregion



        #region 药品进销存明细报表
        /// <summary>
        /// 药品进销存明细报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="arrPrID">要统计的财务期</param>
        /// <param name="strMedID">要统计的药品ID</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReportDataOfInAndOutDe(System.Collections.Generic.List<string> arrPrID, System.Collections.Generic.List<string> arrUpPrID, string strMedID, int intMedType, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            DataTable dt4 = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strWhere = "";
            string strWhere1 = "";
            if (arrPrID.Count > 0)
            {
                for (int i1 = 0; i1 < arrPrID.Count; i1++)
                {

                    if (i1 == 0)
                    {
                        strWhere1 = "and (a.periodid_chr='" + (string)arrUpPrID[i1];
                        strWhere = " and (a.periodid_chr='" + (string)arrPrID[i1];
                        if (arrPrID.Count == 1)
                        {
                            strWhere += "')";
                            strWhere1 += "')";
                        }
                    }
                    else
                    {
                        strWhere1 += "' or a.periodid_chr='" + (string)arrUpPrID[i1];
                        strWhere += "' or a.periodid_chr='" + (string)arrPrID[i1];
                        if (i1 == arrPrID.Count - 1)
                        {
                            strWhere += "')";
                            strWhere1 += "')";
                        }
                    }
                }
            }
            if (intMedType > 0)
            {

                strWhere += " and c.MEDICINETYPEID_CHR='" + intMedType.ToString() + "'";
                strWhere1 += " and c.MEDICINETYPEID_CHR='" + intMedType.ToString() + "'";
            }
            else
            {
                strWhere += " and c.medicineid_chr='" + strMedID + "'";
                strWhere1 += " and c.medicineid_chr='" + strMedID + "'";
            }
            string strSQL = @"SELECT a.PERIODID_CHR  period,'1' as groupCol, a.check_dat AS dat, '上期结余' AS rowname, a.storagecheckid_chr as DOCID_VCHR,b.LOTNO_VCHR,
       c.productorid_chr, 0 AS qty,
       0 AS inmoney,
       0 AS salemoney,0  outqty,0 outmoney,0 outsalemoney,0 changqty,
       0 changmoney,0 changsalemoney,c.UNITPRICE_MNY,c.MEDICINENAME_VCHR,b.realqty_dec as checkMuber,b.BUYPRICE_MNY as checkpre,b.SALEUNITPRICE_MNY as checkSalepre,b.SALEUNITPRICE_MNY*b.realqty_dec as checkMoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr and b.realqty_dec!=0
   AND pstatus_int = 1 " + strWhere1 + @"
union  all
SELECT period, '2' as groupCol,  dat, '(购入)' || vendorname_vchr  AS rowname,DOCID_VCHR,LOTNO_VCHR,PRODUCTORID_CHR,qty,Inmoney,
       salemoney,0  outqty,0 outmoney,0 outsalemoney,0 changqty,
       0 changmoney,0 changsalemoney,UNITPRICE_MNY,MEDICINENAME_VCHR,0 as checkMuber,
0 as checkpre,0 as checkSalepre,0 as checkMoney
  FROM (SELECT a.periodid_chr  as period,a.INORD_DAT as dat, v.vendorname_vchr,a.DOCID_VCHR,c.PRODUCTORID_CHR,b.qty_dec as qty,
               b.buyunitprice_mny  AS Inmoney,b.LOTNO_VCHR,
               b.saleunitprice_mny AS salemoney,c.UNITPRICE_MNY,c.MEDICINENAME_VCHR
          FROM t_opr_storageord a,
               t_opr_storageordde b,
               t_bse_medicine c,
               t_bse_vendor v
         WHERE a.storageordid_chr = b.storageordid_chr
           AND b.medicineid_chr = c.medicineid_chr
           AND a.vendorid_chr = v.vendorid_chr
           AND a.pstatus_int = 2
           AND a.sign_int = 1"
           + strWhere + @" order by a.inord_dat)
union all
SELECT period,'2' as groupCol, dat,'(退库)' || medstorename_vchr  AS rowname, docid_vchr,LOTNO_VCHR,
       productorid_chr,qty,Inmoney,salemoney,0  outqty,0 outmoney,0 outsalemoney,0 changqty,
       0 changmoney,0 changsalemoney,UNITPRICE_MNY,MEDICINENAME_VCHR,0 as checkMuber,
0 as checkpre,0 as checkSalepre,0 as checkMoney
  FROM (SELECT a.periodid_chr as period,a.INORD_DAT AS dat, v.medstorename_vchr, a.docid_vchr,b.LOTNO_VCHR,
               c.productorid_chr, b.qty_dec AS qty,
               b.buyunitprice_mny AS Inmoney,
               b.saleunitprice_mny AS salemoney,c.UNITPRICE_MNY,c.MEDICINENAME_VCHR
          FROM t_opr_storageord a,
               t_opr_storageordde b,
               t_bse_medicine c,
               t_bse_medstore v
         WHERE a.storageordid_chr = b.storageordid_chr
           AND b.medicineid_chr = c.medicineid_chr
           AND a.deptid_chr = v.medstoreid_chr
           AND a.pstatus_int = 2
           AND a.sign_int = 3
           " + strWhere + @"  order by a.inord_dat)
union all
SELECT period,'3' as groupCol,dat, '(出库)' || medstorename_vchr  AS rowname, docid_vchr,LOTNO_VCHR,
       productorid_chr,0 qty,0 Inmoney,0 salemoney, outqty, outmoney, outsalemoney,0 changqty,
       0 changmoney,0 changsalemoney,UNITPRICE_MNY,MEDICINENAME_VCHR,0 as checkMuber,
0 as checkpre,0 as checkSalepre,0 as checkMoney
  FROM (SELECT a.periodid_chr  as period,a.INORD_DAT AS dat, v.medstorename_vchr, a.docid_vchr,b.LOTNO_VCHR,
               c.productorid_chr, b.qty_dec AS outqty,
               0 outmoney,
               b.saleunitprice_mny AS outsalemoney,c.UNITPRICE_MNY,c.MEDICINENAME_VCHR
          FROM t_opr_storageord a,
               t_opr_storageordde b,
               t_bse_medicine c,
               t_bse_medstore v
         WHERE a.storageordid_chr = b.storageordid_chr
           AND b.medicineid_chr = c.medicineid_chr
           AND a.deptid_chr = v.medstoreid_chr
           AND a.pstatus_int = 2
           AND a.sign_int = 2
           " + strWhere + @"  order by a.inord_dat)
union all
SELECT period,'3 'as groupCol,dat, '(退货)' || vendorname_vchr  AS rowname, docid_vchr,LOTNO_VCHR,
       productorid_chr,0 qty,0 Inmoney,0 salemoney, outqty, outmoney, outsalemoney ,0 changqty,
       0 changmoney,0 changsalemoney,UNITPRICE_MNY,MEDICINENAME_VCHR,0 as checkMuber,
0 as checkpre,0 as checkSalepre,0 as checkMoney
  FROM (SELECT a.periodid_chr  as period,a.INORD_DAT AS dat, v.vendorname_vchr, a.docid_vchr,b.LOTNO_VCHR,
               c.productorid_chr, b.qty_dec AS outqty,
               0 AS outmoney,
               b.saleunitprice_mny AS outsalemoney,c.UNITPRICE_MNY,c.MEDICINENAME_VCHR
          FROM t_opr_storageord a,
               t_opr_storageordde b,
               t_bse_medicine c,
               t_bse_vendor v
         WHERE a.storageordid_chr = b.storageordid_chr
           AND b.medicineid_chr = c.medicineid_chr
           AND a.vendorid_chr = v.vendorid_chr
           AND a.pstatus_int = 2
           AND a.sign_int = 4
           " + strWhere + @"  order by a.inord_dat)
union all
SELECT period,'4' as groupCol, dat, '(调价)'|| memo_vchr  AS rowname,
       medicinepricechgapplno_chr as docid_vchr,'' LOTNO_VCHR, productorid_chr, 0 qty,
       0 inmoney, 0 salemoney, 0 outqty, 0 outmoney, 0 outsalemoney, changqty,
       changmoney, changsalemoney,UNITPRICE_MNY,MEDICINENAME_VCHR,0 as checkMuber,
0 as checkpre,0 as checkSalepre,0 as checkMoney
  FROM (SELECT a.periodid_chr  as period,a.APPLDATE_DAT AS dat, a.memo_vchr,
               a.medicinepricechgapplno_chr, c.productorid_chr,
               b.CURQTY_DEC AS changqty, curprice_mny AS changmoney,
               changeprice_mny AS changsalemoney,c.UNITPRICE_MNY,c.MEDICINENAME_VCHR
          FROM t_opr_medicinepricechgappl a,
               t_opr_medicinepricechgapplde b,
               t_bse_medicine c
         WHERE a.medicinepricechgapplid_chr = b.medicinepricechgapplid_chr
           AND b.medicineid_chr = c.medicineid_chr
           AND a.pstatus_int = 2
           " + strWhere + @"  order by a.appldate_dat)
union all 
SELECT a.PERIODID_CHR  period,'5' as groupCol, a.check_dat AS dat, '本期结余' AS rowname, a.storagecheckid_chr as DOCID_VCHR,b.LOTNO_VCHR,
       c.productorid_chr, 0 qty,
       0 inmoney,
       0 salemoney,0  outqty,0 outmoney,0 outsalemoney,0 changqty,
       0 changmoney,0 changsalemoney,c.UNITPRICE_MNY,c.MEDICINENAME_VCHR,b.realqty_dec as checkMuber,b.BUYPRICE_MNY as checkpre,b.SALEUNITPRICE_MNY as checkSalepre,b.SALEUNITPRICE_MNY*b.realqty_dec as checkMoney
  FROM t_opr_storagecheck a, t_opr_storagecheckdetail b, t_bse_medicine c
 WHERE a.storagecheckid_chr = b.storagecheckid_chr
   AND b.medicineid_chr = c.medicineid_chr and b.realqty_dec!=0
   AND pstatus_int = 1 " + strWhere;

            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
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

        #region 获取药品类型毛利率
        /// <summary>
        /// 获取药品类型毛利率
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineTypeID">药品类型ID</param>
        /// <param name="p_douGrossprofitrate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGrossprofitrate(string p_strMedicineTypeID, out double p_douGrossprofitrate)
        {
            p_douGrossprofitrate = 0;
            DataTable p_dtbMedicine = null;
            if (string.IsNullOrEmpty(p_strMedicineTypeID))
            {
                return -1;
            }

            long lngRes = 0;

            try
            {
                string strSQL = @" select grossprofitrate
   from t_ms_grossprofitrateset
  where medicinetypeid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strMedicineTypeID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                if (p_dtbMedicine != null && p_dtbMedicine.Rows.Count > 0)
                {
                    p_douGrossprofitrate = Convert.ToDouble(p_dtbMedicine.Rows[0]["grossprofitrate"]);
                }
                else
                {
                    p_douGrossprofitrate = 0;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取系统设置
        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSetID">设置ID</param>
        /// <param name="p_intSet">设置代码</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSysSetting(string p_strSetID, out int p_intSet)
        {
            p_intSet = 0;
            if (p_strSetID == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"select setstatus_int from t_sys_setting where setid_chr = ?";

                DataTable dtbValue = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSetID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_intSet = Convert.ToInt32(dtbValue.Rows[0][0]);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 将药品设置为无效
        /// <summary>
        /// 将药品设置为无效
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID">药品ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetMedicineInvalid(string p_strMedicineID)
        {
            if (string.IsNullOrEmpty(p_strMedicineID))
            {
                return -1;
            }

            long lngRes = -1;
            try
            {
                string strSQL = @"update t_bse_medicine set status_int = 0 where medicineid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strMedicineID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 保存中标年份
        /// <summary>
        /// 保存中标年份
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID">药品ID</param>
        /// <param name="p_strYear">中标年份</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveStandardYear(string p_strMedicineID, string p_strYear)
        {
            long lngReg = 0;
            string strSQL = "update t_bse_medicine set standarddate ='" + p_strYear + "' where  medicineid_chr = '" + p_strMedicineID + "'";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRP = new clsHRPTableService();
                lngReg = objHRP.DoExcute(strSQL);
            }
            catch (Exception objTxt)
            {
                string strTmp = objTxt.Message;
                com.digitalwave.Utility.clsLogText objLog = new clsLogText();
                bool blnReg = objLog.LogError(objTxt);
            }
            return lngReg;
        }
        #endregion

        #region 修改项目别名表
        /// <summary>
        /// 修改项目别名表
        /// </summary>
        /// <param name="isAddNew">0-false;1-true; other-unknow</param>
        /// <param name="objAlias_Vo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveItemAlias(byte isAddNew, clsAlias_VO objAlias_Vo)
        {
            long lngRes = -1;
            string strSQL = @"";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                DataTable dtTmp = new DataTable();
                IDataParameter[] objParamArr = null;
                if (isAddNew != 0 || isAddNew != 1)
                {
                    strSQL = @"select itemname_vchr
                                 from t_bse_itemalias_drug
                                where itemid_chr = ?
                                  and itemname_vchr = ?
                                   or itemname_vchr = ?
                                  and flag_int = 1";
                    objHRPSvc.CreateDatabaseParameter(3, out objParamArr);
                    objParamArr[0].Value = objAlias_Vo.m_strMedicineId;
                    objParamArr[1].Value = objAlias_Vo.m_strAliasName;
                    objParamArr[2].Value = objAlias_Vo.m_strOldAliasName;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTmp, objParamArr);
                    if (lngRes > 0)
                    {
                        if (dtTmp.Rows.Count == 1)
                        { isAddNew = 0; }
                        else if (dtTmp.Rows.Count == 0)
                        { isAddNew = 1; }
                        else
                        { return 1; }
                    }
                }
                this.m_mthGenerateSQL(isAddNew, ref strSQL);
                if (isAddNew == 1)
                {
                    objHRPSvc.CreateDatabaseParameter(7, out objParamArr);
                    objParamArr[0].Value = objAlias_Vo.m_strAliasCode;
                    objParamArr[1].Value = objAlias_Vo.m_strAliasName;
                    objParamArr[2].Value = objAlias_Vo.m_strPyCode;
                    objParamArr[3].Value = objAlias_Vo.m_strWbCode;
                    objParamArr[4].Value = objAlias_Vo.m_strUserCode;
                    objParamArr[5].Value = objAlias_Vo.m_strOpCode;
                    objParamArr[6].Value = objAlias_Vo.m_strMedicineId;
                }
                else
                {
                    objHRPSvc.CreateDatabaseParameter(8, out objParamArr);
                    objParamArr[0].Value = objAlias_Vo.m_strAliasCode;
                    objParamArr[1].Value = objAlias_Vo.m_strAliasName;
                    objParamArr[2].Value = objAlias_Vo.m_strPyCode;
                    objParamArr[3].Value = objAlias_Vo.m_strWbCode;
                    objParamArr[4].Value = objAlias_Vo.m_strUserCode;
                    objParamArr[5].Value = objAlias_Vo.m_strOpCode;
                    objParamArr[6].Value = objAlias_Vo.m_strMedicineId;
                    objParamArr[7].Value = objAlias_Vo.m_strOldAliasName;
                }
                long lngReceff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngReceff, objParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 生成别名表SQL语句
        /// </summary>
        /// <param name="isAddNew"></param>
        /// <param name="strSQL"></param>
        private void m_mthGenerateSQL(byte isAddNew, ref string strSQL)
        {
            if (isAddNew == 1)
            {
                strSQL = @"insert into t_bse_itemalias_drug
                                       (itemcode_vchr, itemname_vchr, pycode_vchr, wbcode_vchr,
                                        usercode_vchr, opcode_vchr, itemid_chr)
                                values (?, ?, ?, ?, ?, ?, ?)";
            }
            else if (isAddNew == 0)
            {
                strSQL = @"update t_bse_itemalias_drug
                              set itemcode_vchr = ?,
                                  itemname_vchr = ?,
                                  pycode_vchr = ?,
                                  wbcode_vchr = ?,
                                  usercode_vchr = ?,
                                  opcode_vchr = ?
                            where itemid_chr = ?
                              and itemname_vchr = ?
                              and flag_int = 1";
            }
        }
        #endregion

        #region 获取物资仓库
        /// <summary>
        /// 获取物资仓库
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objStorageArr">物资仓库</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetMaterialStorage(out clsStorage_VO[] objStorageArr)
        {
            objStorageArr = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct t.materialroomid, t.materialroomname 
  from t_ma_materialstoreroomset t";

                DataTable dtbValue = null;
                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbValue);

                if (lngRes > 0 && dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    objStorageArr = new clsStorage_VO[dtbValue.Rows.Count];
                    for (int i1 = 0; i1 < dtbValue.Rows.Count; i1++)
                    {
                        objStorageArr[i1] = new clsStorage_VO();
                        objStorageArr[i1].m_strStroageID = dtbValue.Rows[i1]["materialroomid"].ToString();
                        objStorageArr[i1].m_strStroageName = dtbValue.Rows[i1]["materialroomname"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取药品的比例
        /// <summary>
        /// 获取药品的比例
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID"></param>
        /// <param name="p_dtbChargeItem"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFillChargeItem(string p_strMedicineID, out DataTable p_dtbChargeItem)
        {
            p_dtbChargeItem = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.precent_dec decdiscount, a.copayid_chr, b.paytypename_vchr catname
	from t_aid_inschargeitem a, t_bse_patientpaytype b
 where a.copayid_chr = b.paytypeid_chr(+)
	 and itemid_chr = '" + p_strMedicineID + @"'
	 and b.isusing_num = 1
 order by b.paytypeid_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtbChargeItem);
                objHRPServ.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

    }


    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageBaseSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region m_lngFindAllStroage
        /// <summary>
        /// m_lngFindAllStroage
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllStroage(out clsStorage_VO[] p_objResultArr)
        {
            string sub = " ORDER BY storageid_chr ";
            return this.m_lngFindStroageByAny(sub, out p_objResultArr);
        }
        #endregion

        #region m_lngFindStroageByAny
        /// <summary>
        /// m_lngFindStroageByAny
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindStroageByAny(string p_strSQL, out clsStorage_VO[] p_objResultArr)
        {
            long rec = 0;
            string Sql = string.Empty;
            DataTable dt = null;
            p_objResultArr = null;
            clsHRPTableService svc = new clsHRPTableService();

            Sql = @"SELECT a.*, b.STORAGETYPENAME_VCHR FROM T_BSE_STORAGE a LEFT OUTER JOIN T_AID_STORAGETYPE b ON b.STORAGETYPEID_CHR = a.STORAGETYPEID_CHR " + p_strSQL;
            rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.CopyDataToVO(dt, out p_objResultArr);
            }
            return rec;
        }
        #endregion

        #region CopyDataToVO
        /// <summary>
        /// CopyDataToVO
        /// </summary>
        /// <param name="dtbSource"></param>
        /// <param name="objResultArr"></param>
        private void CopyDataToVO(DataTable dtbSource, out clsStorage_VO[] objResultArr)
        {
            objResultArr = null;

            if (dtbSource != null && dtbSource.Rows.Count > 0)
            {
                objResultArr = new clsStorage_VO[dtbSource.Rows.Count];

                DataRow dr = null;
                for (int i = 0; i < dtbSource.Rows.Count; i++)
                {
                    dr = dtbSource.Rows[i];

                    objResultArr[i] = new clsStorage_VO();
                    objResultArr[i].m_objStroageType = new clsStorageType_VO();
                    objResultArr[i].m_strStroageID = dr["STORAGEID_CHR"].ToString().Trim();
                    objResultArr[i].m_objStroageType.m_strStroageTypeID = dr["STORAGETYPEID_CHR"].ToString().Trim();
                    objResultArr[i].m_objStroageType.m_strStroageTypeName = dr["STORAGETYPENAME_VCHR"].ToString().Trim();
                    objResultArr[i].m_strStroageName = dr["STORAGENAME_VCHR"].ToString().Trim();
                    objResultArr[i].m_intOutType = int.Parse(dr["OUTFLAG_INT"].ToString().Trim());
                    objResultArr[i].m_decStorageGrossProfit = (dr["STORAGEGROSSPROFIT_DEC"] == DBNull.Value || dr["STORAGEGROSSPROFIT_DEC"].ToString().Trim() == "") ? Convert.ToDecimal("0.15") : Convert.ToDecimal(dr["STORAGEGROSSPROFIT_DEC"]);
                    objResultArr[i].m_intInitFlag = (dr["INITFLAG_INT"] == DBNull.Value || dr["INITFLAG_INT"].ToString().Trim() == "") ? 0 : Convert.ToInt32(dr["INITFLAG_INT"]);
                }
            }

        }
        #endregion

        #region m_lngFindVendorByAny
        /// <summary>
        /// m_lngFindVendorByAny
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindVendorByAny(string p_strSQL, out DataTable p_dtbResult)
        {
            long rec = 0;
            string Sql = string.Empty;
            p_dtbResult = null;
            clsHRPTableService svc = new clsHRPTableService();

            Sql = @"SELECT * FROM t_bse_vendor " + p_strSQL + " order by USERCODE_CHR";
            rec = svc.lngGetDataTableWithoutParameters(Sql, ref p_dtbResult);

            return rec;
        }
        #endregion

        #region m_lngGetStorageInitFlag
        /// <summary>
        /// m_lngGetStorageInitFlag
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="p_intInitFlag"></param>
        /// <returns></returns>
        public long m_lngGetStorageInitFlag(string p_strID, out int p_intInitFlag)
        {
            long rec = 0;
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            p_intInitFlag = 0;

            Sql = "SELECT initflag_int FROM t_bse_storage WHERE storageid_chr = '" + p_strID.Trim() + "'";
            rec = svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["initflag_int"] != DBNull.Value)
                {
                    p_intInitFlag = Convert.ToInt32(dt.Rows[0]["initflag_int"]);
                }
            }
            return rec;
        }
        #endregion

    }
}
