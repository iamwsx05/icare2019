using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 药品库存明细的服务层 Create kong by 2004-05-12
    /// </summary>
    /// 

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageMedDetailSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase //MiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsStorageMedDetailSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 增加药品库存明细表记录  欧阳孔伟  2004-06-04
        /// <summary>
        /// 增加药品库存明细记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedStorageVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMedStoDetail(clsStorageMedDetail_VO p_objMedStorageVo)
        {
            long lngRes = 0;

            string strSQL = @"INSERT INTO t_opr_storagemeddetail(storageid_chr,medicineid_chr,syslotno_chr,lotno_vchr,productorid_chr,curqty_dec,unitid_chr,usefulstatus_int,usefullife_dat,buyunitprice_mny,saleunitprice_mny,wholesaleunitprice_mny)
							 VALUES('" + p_objMedStorageVo.m_objStorage.m_strStroageID + "','" + p_objMedStorageVo.m_objMedicine.m_strMedicineID + "','" + p_objMedStorageVo.m_strSysLotNo + "','" + p_objMedStorageVo.m_strLotNo + "','" +
                             p_objMedStorageVo.m_objProduct.m_strVendorID + "'," + p_objMedStorageVo.m_fltCurQty.ToString() + ",'" + p_objMedStorageVo.m_objUnit.m_strUnitID + "'," + p_objMedStorageVo.m_intUsefulStatus.ToString() +
                             ",to_date('" + p_objMedStorageVo.m_strUsefulLife + "','yyyy-mm-dd hh24:mi:ss')," + p_objMedStorageVo.m_fltBuyUnitPrice.ToString() + "," + p_objMedStorageVo.m_fltSaleUnitPrice.ToString() +
                             "," + p_objMedStorageVo.m_fltWholesaleUnitPrice.ToString() + ")";

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

        #region 更改药品库存明细表数量  欧阳孔伟  2004-06-04
        /// <summary>
        /// 更改药品库存明细表数量
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedStorageVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMedStoDetailQty(clsStorageMedDetail_VO p_objMedStorageVo)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE t_opr_storagemeddetail
							 SET curqty_dec=" + p_objMedStorageVo.m_fltCurQty.ToString() + " " +
                            " WHERE STORAGEID_CHR='" + p_objMedStorageVo.m_objStorage.m_strStroageID +
                            "' AND MEDICINEID_CHR='" + p_objMedStorageVo.m_objMedicine.m_strMedicineID +
                            "' AND SYSLOTNO_CHR='" + p_objMedStorageVo.m_strSysLotNo + "' ";

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

        #region 报废药品  欧阳孔伟  2004-06-04
        /// <summary>
        /// 报废药品
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedStorageVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRejectMedStoDetail(clsStorageMedDetail_VO p_objMedStorageVo)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE t_opr_storagemeddetail
							 SET USEFULSTATUS_INT = 2
							 WHERE STORAGEID_CHR='" + p_objMedStorageVo.m_objStorage.m_strStroageID +
                            "' AND MEDICINEID_CHR='" + p_objMedStorageVo.m_objMedicine.m_strMedicineID +
                            "' AND SYSLOTNO_CHR='" + p_objMedStorageVo.m_strSysLotNo + "' ";

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

        #region 用完药品（即删除标志）  欧阳孔伟  2004-06-04
        /// <summary>
        /// 设置药品删除标志
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedStorageVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMedStoDetail(clsStorageMedDetail_VO p_objMedStorageVo)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE t_opr_storagemeddetail
							 SET USEFULSTATUS_INT = 3
							 WHERE STORAGEID_CHR='" + p_objMedStorageVo.m_objStorage.m_strStroageID +
                            "' AND MEDICINEID_CHR='" + p_objMedStorageVo.m_objMedicine.m_strMedicineID +
                            "' AND SYSLOTNO_CHR='" + p_objMedStorageVo.m_strSysLotNo + "' ";

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

        #region 将DataTable数据传递到VO　欧阳孔伟　2004-06-17
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsStorageMedDetail_VO[] objItem)
        {
            objItem = new clsStorageMedDetail_VO[0];

            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItem = new clsStorageMedDetail_VO[intRow];
                        for (int i1 = 0; i1 < intRow; i1++)
                        {
                            string strQty = "";
                            string strBuyPrice = "";
                            string strSalePrice = "";
                            string strWholeSalePrice = "";
                            string strUsefulStatus = "";

                            objItem[i1] = new clsStorageMedDetail_VO();
                            objItem[i1].m_objStorage = new clsStorage_VO();
                            objItem[i1].m_objStorage.m_strStroageID = dtbSource.Rows[i1]["storageid_chr"].ToString().Trim();
                            objItem[i1].m_objStorage.m_strStroageName = dtbSource.Rows[i1]["storagename_vchr"].ToString().Trim();
                            objItem[i1].m_objMedicine = new clsMedicine_VO();
                            objItem[i1].m_objMedicine.m_strMedicineID = dtbSource.Rows[i1]["medicineid_chr"].ToString().Trim();
                            objItem[i1].m_objMedicine.m_strMedicineName = dtbSource.Rows[i1]["medicinename_vchr"].ToString().Trim();
                            objItem[i1].m_objMedicine.m_strMedSpec = dtbSource.Rows[i1]["medspec_vchr"].ToString().Trim();
                            objItem[i1].m_strSysLotNo = dtbSource.Rows[i1]["syslotno_chr"].ToString().Trim();
                            objItem[i1].m_strLotNo = dtbSource.Rows[i1]["lotno_vchr"].ToString().Trim();
                            objItem[i1].m_objProduct = new clsVendor_VO();
                            objItem[i1].m_objProduct.m_strVendorID = dtbSource.Rows[i1]["productorid_chr"].ToString().Trim();
                            objItem[i1].m_objProduct.m_strVendorName = dtbSource.Rows[i1]["vendorname_vchr"].ToString().Trim();
                            objItem[i1].m_objUnit = new clsUnit_VO();
                            objItem[i1].m_objUnit.m_strUnitID = dtbSource.Rows[i1]["unitid_chr"].ToString().Trim();
                            objItem[i1].m_objUnit.m_strUnitName = dtbSource.Rows[i1]["unitname_chr"].ToString().Trim();
                            objItem[i1].m_strUsefulLife = dtbSource.Rows[i1]["usefullife_dat"].ToString().Trim();

                            strQty = dtbSource.Rows[i1]["curqty_dec"].ToString();
                            if (strQty == "")
                            {
                                strQty = "0";
                            }
                            objItem[i1].m_fltCurQty = float.Parse(strQty);

                            strBuyPrice = dtbSource.Rows[i1]["buyunitprice_mny"].ToString();
                            if (strBuyPrice == "")
                            {
                                strBuyPrice = "0";
                            }
                            objItem[i1].m_fltBuyUnitPrice = float.Parse(strBuyPrice);

                            strSalePrice = dtbSource.Rows[i1]["saleunitprice_mny"].ToString();
                            if (strSalePrice == "")
                            {
                                strSalePrice = "0";
                            }
                            objItem[i1].m_fltSaleUnitPrice = float.Parse(strSalePrice);

                            strWholeSalePrice = dtbSource.Rows[i1]["wholesaleunitprice_mny"].ToString();
                            if (strWholeSalePrice == "")
                            {
                                strWholeSalePrice = "0";
                            }
                            objItem[i1].m_fltWholesaleUnitPrice = float.Parse(strWholeSalePrice);

                            strUsefulStatus = dtbSource.Rows[i1]["usefulstatus_int"].ToString();
                            if (strUsefulStatus == "")
                            {
                                strUsefulStatus = "1";
                            }
                            objItem[i1].m_intUsefulStatus = int.Parse(strUsefulStatus);
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

        }
        #endregion

        #region 模糊查找库存药品明细  欧阳孔伟  2004-06-04
        /// <summary>
        /// 模糊查找库存药品明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedStoDetailByAny(string p_strSQL, out clsStorageMedDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageMedDetail_VO[0];

            string strSQL = @"SELECT *
								FROM v_opr_storagemeddetail
							" + p_strSQL;
            System.Data.DataTable dtbResult = new System.Data.DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (dtbResult != null)
                {
                    CopyDataTableToVO(dtbResult, out p_objResult);
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

        #region 按药品ID查找库存药品明细  欧阳孔伟  2004-06-04
        /// <summary>
        /// 按药品ID查找库存药品明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedID"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedStoDetailByMedID(string p_strMedID, out clsStorageMedDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageMedDetail_VO[0];

            string strSQL = "WHERE medicineid_chr='" + p_strMedID + "'";

            lngRes = m_lngFindMedStoDetailByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 按药品ID和仓库ID查找库存药品明细  欧阳孔伟  2004-06-04
        /// <summary>
        /// 按药品ID和仓库ID查找库存药品明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedID"></param>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedStoDetailByMedIDAndStorageID(string p_strMedID, string p_strStorageID, out clsStorageMedDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageMedDetail_VO[0];

            string strSQL = "WHERE medicineid_chr='" + p_strMedID + "' AND storageid_chr='" + p_strStorageID + "'";

            lngRes = m_lngFindMedStoDetailByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 按仓库ID查找库存药品明细  欧阳孔伟  2004-06-04
        /// <summary>
        /// 按仓库ID查找库存药品明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedStoDetailByStorageID(string p_strStorageID, out clsStorageMedDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageMedDetail_VO[0];

            string strSQL = "WHERE storageid_chr='" + p_strStorageID + "'";

            lngRes = m_lngFindMedStoDetailByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 按药品ID和可用状态查找库存药品明细  欧阳孔伟  2004-06-04
        /// <summary>
        /// 按药品ID和可用状态查找库存药品明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedID"></param>
        /// <param name="p_intUsefulStatus"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedStoDetailByMedIDAndUsefulStatus(string p_strMedID, int p_intUsefulStatus, out clsStorageMedDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageMedDetail_VO[0];

            string strSQL = "WHERE medicineid_chr='" + p_strMedID + "' AND USEFULSTATUS_INT=" + p_intUsefulStatus.ToString();

            lngRes = m_lngFindMedStoDetailByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 按可用状态查找库存药品明细  欧阳孔伟  2004-06-04
        /// <summary>
        /// 按可用状态查找库存药品明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intUsefulStatus"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedStoDetailByUsefulStatus(int p_intUsefulStatus, out clsStorageMedDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageMedDetail_VO[0];

            string strSQL = "WHERE usefulstatus_int=" + p_intUsefulStatus.ToString();

            lngRes = m_lngFindMedStoDetailByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 按生产厂家查找库存药品明细  欧阳孔伟  2004-06-04
        /// <summary>
        /// 按生产厂家查找库存药品明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strProduct"></param>
        /// <param name="p_objResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindMedStoDetailByProduct(string p_strProduct, out clsStorageMedDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageMedDetail_VO[0];

            string strSQL = "WHERE productid_chr='" + p_strProduct + "'";

            lngRes = m_lngFindMedStoDetailByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region 获取药品近期失效的药品信息
        /// <summary>
        /// 获取药品近期失效的药品信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_strDate"></param>
        /// <param name="p_outDt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUsefulMedInfo(string p_strStorageID, string p_strDate, out System.Data.DataTable p_outDt)
        {
            long lngRes = 0;
            p_outDt = null;
            string strSQL = @"select a.storageid_chr,a.medicineid_chr,a.syslotno_chr,a.lotno_vchr,
								a.curqty_dec,a.unitid_chr,a.usefulstatus_int,a.usefullife_dat,
								a.buyunitprice_mny,a.saleunitprice_mny,a.wholesaleunitprice_mny,b.assistcode_chr,b.medicinename_vchr,b.medspec_vchr,b.productorid_chr
								from t_opr_storagemeddetail a,t_bse_medicine b
								where a.usefullife_dat <= to_date('" + p_strDate + @"','yyyy-mm-dd')
								and a.storageid_chr = '" + p_strStorageID + @"'
								and a.medicineid_chr = b.medicineid_chr 
								and a.curqty_dec > 0";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_outDt);
            }
            catch (Exception ee)
            {
                string strTmp = ee.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ee);
            }
            return lngRes;
        }
        #endregion
    }
}
