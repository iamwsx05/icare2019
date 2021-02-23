using System;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 入出库操作服务层
    /// kong 2004-05-14
    /// </summary>
    /// 

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageOrdSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase //MiddleTierBase.dll
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsStorageOrdSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 同时保存出入库记录和明细单 欧阳孔伟　2004-05-14
        /// <summary>
        /// 同步保存出入库记录和明细单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrd"></param>
        /// <param name="p_objOrdDeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoSaveOrdAndDe(clsStorageOrd_VO p_objOrd, clsStorageOrdDe_VO[] p_objOrdDeArr)
        {
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            long lngRes = 0;

            lngRes = m_lngCheckOrdID(p_objOrd.m_strStorageOrdID, p_objOrd.m_objStorageOrdType.m_strStorageOrdTypeID, p_objOrd.m_objStorage.m_strStroageID);

            if (lngRes < 1)
            {
                return lngRes;
            }

            //插入单据记录表
            lngRes = m_lngDoSaveOrd(p_objOrd);

            if (lngRes > 0)
            {
                int intRow = p_objOrdDeArr.Length;
                for (int i = 0; i < intRow; i++)
                {
                    lngRes = m_lngDoSaveOrdDe(p_objOrdDeArr[i]);
                    if (lngRes < 1)
                    {
                        break;
                    }
                }
            }
            //			string strSQL1 = @"INSERT INTO t_opr_StorageOrd(StorageOrdID_chr,StorageOrdTypeID_chr,InOrd_dat,TolMny_mny,PStatus_int,DocID_vchr,Memo_vchr,OfferID_chr,DeptID_chr,VendorID_chr,CreatorID_chr,CreateDate_dat,AduitEmp_chr,AduitDate_dat,PeriodID_chr,AcctEmp_chr,AcctDate_dat,storageid_chr) " +
            //				"VALUES('" + p_objOrd.m_strStorageOrdID + "','" + p_objOrd.m_objStorageOrdType.m_strStorageOrdTypeID + "',To_Date('" + p_objOrd.m_strCreateDate + "','yyyy-mm-dd hh24:mi:ss')," + p_objOrd.m_fltTolMny + "," + p_objOrd.m_intPStatus + ",'" + p_objOrd.m_strDocID + "','" +
            //				p_objOrd.m_strMemo + "','" + p_objOrd.m_objOffer.strEmpID + "','" + p_objOrd.m_objDept.strDeptID + "','" + p_objOrd.m_objVendor.m_strVendorID + "','" + p_objOrd.m_objCreator.strEmpID + "',To_Date('" + p_objOrd.m_strCreateDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objOrd.m_objAduitEmp.strEmpID +
            //				"',To_Date('" + p_objOrd.m_strAduitDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objOrd.m_objPeriod.m_strPeriodID + "','" + p_objOrd.m_objAcctEmp.strEmpID + "',To_Date('" + p_objOrd.m_strAcctDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objOrd.m_objStorage.m_strStroageID + "')";
            //
            //			try
            //			{
            //				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            //				lngRes = objHRPSvc.DoExcute(strSQL1);	
            //
            //				int intRow = p_objOrdDeArr.Length;
            //
            //				if (lngRes > 0 )
            //				{
            //					for (int i=0;i<intRow;i++)
            //					{
            //						//插入明细表
            //						string strSQL2 = @"INSERT INTO t_opr_StorageOrdDe(StorageOrdDeid_chr,StorageOrdID_chr,Ord_dat,RowNo_chr,MedicineID_chr,UnitID_chr,LotNo_vchr,UsefulLife_dat,Prodcutorid_chr,Qty_dec,BuyUnitPrice_mny,SaleUnitPrice_mny,WholeSaleUnitPrice_mny,Discount_dec,BuyTolPrice_mny,SysLotNo_chr) " +
            //							" VALUES('" + p_objOrdDeArr[i].m_strStorageOrdDetailID + "','" + p_objOrdDeArr[i].m_strStorageOrdID + "',To_Date('" + p_objOrdDeArr[i].m_strOrdDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objOrdDeArr[i].m_strRowNo + "','" + p_objOrdDeArr[i].m_objMedicine.m_strMedicineID +
            //							"','" + p_objOrdDeArr[i].m_objUnit.m_strUnitID + "','" + p_objOrdDeArr[i].m_strLotNo + "',to_date('" + p_objOrdDeArr[i].m_strUsefulLifeDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objOrdDeArr[i].m_strProductor + "'," + p_objOrdDeArr[i].m_fltQty + "," +
            //							p_objOrdDeArr[i].m_fltBuyUnitPrice + "," + p_objOrdDeArr[i].m_fltSaleUnitPrice + "," + p_objOrdDeArr[i].m_fltWholesaleUnitPrice + "," + p_objOrdDeArr[i].m_fltDiscount + "," + p_objOrdDeArr[i].m_fltBuyTolPrice + ",'" + p_objOrdDeArr[i].m_strSysLotNo + "')";
            //						lngRes = objHRPSvc.DoExcute(strSQL2);
            //
            //						if(lngRes < 1)
            //						{
            //							break;
            //						}
            //					}
            //				}
            //				
            //			}		
            //			catch(Exception objEx)
            //			{
            //				string strTmp=objEx.Message;
            //				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //				bool blnRes = objLogger.LogError(objEx);
            //			}
            return lngRes;
        }
        #endregion

        #region 保存记录表　欧阳孔伟　2004-05-26
        /// <summary>
        /// 保存记录表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoSaveOrd(clsStorageOrd_VO p_objOrd)
        {
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            long lngRes = 0;

            lngRes = m_lngCheckOrdID(p_objOrd.m_strStorageOrdID, p_objOrd.m_objStorageOrdType.m_strStorageOrdTypeID, p_objOrd.m_objStorage.m_strStroageID);

            if (lngRes < 1)
            {
                return lngRes;
            }
            //插入单据记录表
            string strSQL = @"INSERT INTO t_opr_StorageOrd(StorageOrdID_chr,StorageOrdTypeID_chr,storageid_chr,PeriodID_chr,InOrd_dat,TolMny_mny,PStatus_int,Docid_vchr,OfferID_chr,DeptID_chr,VendorID_chr,CreatorID_chr,CreateDate_dat,AduitEmp_chr,AduitDate_dat,AcctEmp_chr,AcctDate_dat,Memo_vchr) VALUES('" +
                p_objOrd.m_strStorageOrdID + "','" + p_objOrd.m_objStorageOrdType.m_strStorageOrdTypeID + "','" + p_objOrd.m_objStorage.m_strStroageID + "','" + p_objOrd.m_objPeriod.m_strPeriodID + "',To_Date('" + p_objOrd.m_strOrdDate + "','yyyy-mm-dd hh24:mi:ss')," + p_objOrd.m_fltTolMny +
                "," + p_objOrd.m_intPStatus + ",'" + p_objOrd.m_strDocID + "','" + p_objOrd.m_objOffer.strEmpID + "','" + p_objOrd.m_objDept.strDeptID + "','" + p_objOrd.m_objVendor.m_strVendorID + "','" + p_objOrd.m_objCreator.strEmpID + "',To_Date('" + p_objOrd.m_strCreateDate + "','yyyy-mm-dd hh24:mi:ss'),'" +
                p_objOrd.m_objAduitEmp.strEmpID + "',To_Date('" + p_objOrd.m_strAduitDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objOrd.m_objAcctEmp.strEmpID + "',To_Date('" + p_objOrd.m_strAcctDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objOrd.m_strMemo + "')";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
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

        #region 保存明细表　欧阳孔伟　2004-05-26
        /// <summary>
        /// 保存明细表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoSaveOrdDe(clsStorageOrdDe_VO p_objOrdDe)
        {
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            long lngRes = 0;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            //插入明细表
            objHRPSvc.m_lngGenerateNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", out p_objOrdDe.m_strStorageOrdDetailID);
            string strSQL = @"INSERT INTO t_opr_StorageOrdDe(StorageOrdDeid_chr,StorageOrdID_chr,StorageOrdTypeid_chr,MedicineID_chr,SysLotNo_chr,Ord_dat,RowNo_chr,UnitID_chr,LotNo_vchr,UsefulLife_dat,Prodcutorid_chr,Qty_dec,BuyUnitPrice_mny,SaleUnitPrice_mny,WholeSaleUnitPrice_mny,Discount_dec,BuyTolPrice_mny,invoiceno_vchr) VALUES('" +
                p_objOrdDe.m_strStorageOrdDetailID + "','" + p_objOrdDe.m_strStorageOrdID + "','" + p_objOrdDe.m_objStorageOrdType.m_strStorageOrdTypeID + "','" + p_objOrdDe.m_objMedicine.m_strMedicineID + "','" + p_objOrdDe.m_strSysLotNo + "',To_Date('" + p_objOrdDe.m_strOrdDate + "','yyyy-mm-dd hh24:mi:ss'),'" +
                p_objOrdDe.m_strRowNo + "','" + p_objOrdDe.m_objUnit.m_strUnitID + "','" + p_objOrdDe.m_strLotNo + "',To_Date('" + p_objOrdDe.m_strUsefulLifeDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objOrdDe.m_objProductor.m_strVendorID + "'," + p_objOrdDe.m_fltQty + "," + p_objOrdDe.m_fltBuyUnitPrice + "," +
                p_objOrdDe.m_fltSaleUnitPrice + "," + p_objOrdDe.m_fltWholesaleUnitPrice + "," + p_objOrdDe.m_fltDiscount + "," + p_objOrdDe.m_fltBuyTolPrice + ",'" + p_objOrdDe.m_strInvoiceNo.Trim() + "')";
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

        #region 获得最大单据号　欧阳孔伟　2004-05-14

        /// <summary>
        /// 获取最大单据号，参数p_intOrdType代表出入库类型，1为入库，2为出库
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdType"></param>
        /// <param name="p_strOrdID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetNewOrdID(string p_strOrdType, out string p_strOrdID)
        {
            long lngRes = 0;
            p_strOrdID = null;
            System.Data.DataTable dtbResult = new System.Data.DataTable();

            string strSQL = @"SELECT max(storageordid_chr) as maxid
							  FROM t_opr_storageord
							  WHERE storageordtypeid_chr ='" + p_strOrdType + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    if (dtbResult.Rows.Count > 0)
                    {
                        p_strOrdID = dtbResult.Rows[0]["MAXID"].ToString();
                    }

                }
                //如果还未对表进行操作，不知返回的dtbResult是否为null;
                //				else if(lngRes > 0)
                //				{
                //					p_strOrdID = "";
                //				}
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

        #region 检查单据号是否正确　欧阳孔伟　2004-05-14

        /// <summary>
        /// 检查单据号是否正确，正确返回1，错误返回0。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdID"></param>
        /// <param name="p_strOrdTypeID"></param>
        /// <param name="p_strStorageID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckOrdID(string p_strOrdID, string p_strOrdTypeID, string p_strStorageID)
        {
            long lngRes = 0;

            System.Data.DataTable dtbResult = null;

            string strSQL = @"SELECT storageordid_chr
							  FROM t_opr_storageord
							  WHERE storageordid_chr = '" + p_strOrdID + "' and storageordtypeid_chr = '" +
                p_strOrdTypeID + "' and storageid_chr = '" + p_strStorageID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0)
                {
                    if (dtbResult != null)
                    {

                        if (dtbResult.Rows.Count > 0)
                        {
                            lngRes = 0;
                        }
                    }
                }
                else
                {
                    lngRes = 1;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }



            return lngRes;
            /////////////////////////////////////////////////////////
        }
        #endregion

        #region 修改记录表　欧阳孔伟　2004-05-14
        /// <summary>
        /// 修改记录表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateOrdByID(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;

            //更新单据记录表
            string strSQL = @"UPDATE t_opr_StorageOrd SET createdate_dat = to_date('" + p_objOrd.m_strCreateDate + "','yyyy-mm-dd hh24:mi:ss'),Tolmny_mny = " + p_objOrd.m_fltTolMny + ",PStatus_int = " +
                p_objOrd.m_intPStatus + ",DocID_vchr = '" + p_objOrd.m_strDocID + "',deptid_chr='" + p_objOrd.m_objDept.strDeptID + "',Vendorid_chr='" + p_objOrd.m_objVendor.m_strVendorID +
                "',Memo_vchr = '" + p_objOrd.m_strMemo + "' WHERE StorageOrdID_chr = '" + p_objOrd.m_strStorageOrdID + "' AND StorageOrdTypeID_chr = '" +
                p_objOrd.m_objStorageOrdType.m_strStorageOrdTypeID + "' and PeriodID_chr = '" + p_objOrd.m_objPeriod.m_strPeriodID + "' and storageid_chr = '" + p_objOrd.m_objStorage.m_strStroageID + "'";
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

        #region 修改明细表　欧阳孔伟　2004-05-26
        /// <summary>
        /// 修改明细表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateOrdDeByDeID(clsStorageOrdDe_VO p_objOrdDe)
        {
            long lngRes = 0;

            //更新单据记录表
            string strSQL = @"UPDATE t_opr_StorageOrdDe SET MedicineId_chr='" + p_objOrdDe.m_objMedicine.m_strMedicineID + "',SysLotNo_chr='" +
                p_objOrdDe.m_strSysLotNo + "',RowNo_chr='" + p_objOrdDe.m_strRowNo + "',UnitID_chr='" + p_objOrdDe.m_objUnit.m_strUnitID +
                "',LotNo_vchr='" + p_objOrdDe.m_strLotNo + "',UsefulLife_dat=To_date('" + p_objOrdDe.m_strUsefulLifeDate + "','yyyy-mm-dd hh24:mi:ss')," +
                "Prodcutorid_chr='" + p_objOrdDe.m_objProductor.m_strVendorID + "',qty_dec=" + p_objOrdDe.m_fltQty + ",Buyunitprice_mny=" + p_objOrdDe.m_fltBuyUnitPrice +
                ",SaleUnitPrice_mny = " + p_objOrdDe.m_fltSaleUnitPrice + ",WholeSaleUnitPrice_mny=" + p_objOrdDe.m_fltWholesaleUnitPrice +
                ",Discount_dec = " + p_objOrdDe.m_fltDiscount + ",BuyTolPrice_mny = " + p_objOrdDe.m_fltBuyTolPrice + ",invoiceno_vchr='" + p_objOrdDe.m_strInvoiceNo.Trim() + "' ";
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

        #region 删除出入库记录单 欧阳孔伟  2004-05-14
        /// <summary>
        /// 删除出入库单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrdByID(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;
            //将状态改为删除标志
            string strSQL = @"UPDATE t_opr_StorageOrd SET PStatus_int = " + p_objOrd.m_intPStatus + " WHERE StorageOrdID_chr = '" + p_objOrd.m_strStorageOrdID +
                "' AND StorageOrdTypeID_chr = '" + p_objOrd.m_objStorageOrdType.m_strStorageOrdTypeID + "' AND PeriodID_chr = '" + p_objOrd.m_objPeriod.m_strPeriodID +
                "' and storageid_chr = '" + p_objOrd.m_objStorage.m_strStroageID + "'";

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

        #region 删除出入库单明细　欧阳孔伟　2004-05-26
        /// <summary>
        /// 删除出入库单明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdDeID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrdDeByDeID(string p_strOrdDeID)
        {
            long lngRes = 0;
            //删除
            string strSQL = @"DELETE t_opr_storageordde WHERE storageorddeid_chr = '" + p_strOrdDeID + "'";

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

        #region 将DataTable数据传递到VO上去

        #region 将DataTable数据传递到记录VO上
        /// <summary>
        /// 将DataTable数据传递到记录VO上
        /// </summary>
        /// <param name="dtbSource">数据源</param>
        /// <param name="objItems">记录单VO</param>
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsStorageOrd_VO[] objItems)
        {
            objItems = new clsStorageOrd_VO[0];

            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStorageOrd_VO[intRow];
                        for (int i1 = 0; i1 < intRow; i1++)
                        {
                            objItems[i1] = new clsStorageOrd_VO();
                            objItems[i1].m_objAcctEmp = new clsEmployeeVO();
                            objItems[i1].m_objAduitEmp = new clsEmployeeVO();
                            objItems[i1].m_objCreator = new clsEmployeeVO();
                            objItems[i1].m_objDept = new clsDepartmentVO();
                            objItems[i1].m_objOffer = new clsEmployeeVO();
                            objItems[i1].m_objPeriod = new clsPeriod_VO();
                            objItems[i1].m_objStorage = new clsStorage_VO();
                            objItems[i1].m_objStorageOrdType = new clsStorageOrdType_VO();
                            objItems[i1].m_objVendor = new clsVendor_VO();

                            objItems[i1].m_strStorageOrdID = dtbSource.Rows[i1]["storageordid_chr"].ToString().Trim();
                            objItems[i1].m_objStorage.m_strStroageID = dtbSource.Rows[i1]["storageid_chr"].ToString().Trim();
                            objItems[i1].m_objStorage.m_strStroageName = dtbSource.Rows[i1]["storagename_vchr"].ToString().Trim();
                            objItems[i1].m_objPeriod.m_strPeriodID = dtbSource.Rows[i1]["periodid_chr"].ToString().Trim();
                            objItems[i1].m_objPeriod.m_strStartDate = dtbSource.Rows[i1]["startdate_dat"].ToString().Trim();
                            objItems[i1].m_objPeriod.m_strEndDate = dtbSource.Rows[i1]["enddate_dat"].ToString().Trim();
                            objItems[i1].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i1]["storageordtypeid_chr"].ToString().Trim();
                            objItems[i1].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i1]["storageordtypename_vchr"].ToString().Trim();
                            objItems[i1].m_objStorageOrdType.m_intSign = int.Parse(dtbSource.Rows[i1]["sign_int"].ToString().Trim());
                            objItems[i1].m_objStorageOrdType.m_intDeptType = int.Parse(dtbSource.Rows[i1]["depttype_int"].ToString().Trim());

                            if (dtbSource.Rows[i1]["vendorid_chr"].ToString().Trim() != "")
                            {
                                objItems[i1].m_objVendor.m_strVendorID = dtbSource.Rows[i1]["vendorid_chr"].ToString().Trim();
                                objItems[i1].m_objVendor.m_strVendorName = dtbSource.Rows[i1]["vendorname_vchr"].ToString().Trim();
                            }
                            else
                            {
                                objItems[i1].m_objVendor.m_strVendorID = "";
                                objItems[i1].m_objVendor.m_strVendorName = "";
                            }


                            objItems[i1].m_strOrdDate = dtbSource.Rows[i1]["inord_dat"].ToString().Trim();
                            objItems[i1].m_fltTolMny = float.Parse(dtbSource.Rows[i1]["tolmny_mny"].ToString().Trim());
                            objItems[i1].m_intPStatus = int.Parse(dtbSource.Rows[i1]["pstatus_int"].ToString().Trim());
                            objItems[i1].m_strDocID = dtbSource.Rows[i1]["docid_vchr"].ToString().Trim();

                            if (dtbSource.Rows[i1]["deptid_chr"].ToString().Trim() != "")
                            {
                                objItems[i1].m_objDept.strDeptID = dtbSource.Rows[i1]["deptid_chr"].ToString().Trim();
                            }
                            else
                            {
                                objItems[i1].m_objDept.strDeptID = "";
                            }

                            objItems[i1].m_strMemo = dtbSource.Rows[i1]["memo_vchr"].ToString().Trim();

                            if (dtbSource.Rows[i1]["offerid_chr"].ToString().Trim() != "")
                            {
                                objItems[i1].m_objOffer.strEmpID = dtbSource.Rows[i1]["offerid_chr"].ToString().Trim();
                            }
                            else
                            {
                                objItems[i1].m_objOffer.strEmpID = "";
                                objItems[i1].m_objOffer.strLastName = "";
                            }

                            if (dtbSource.Rows[i1]["creatorid_chr"].ToString().Trim() != "")
                            {
                                objItems[i1].m_objCreator.strEmpID = dtbSource.Rows[i1]["creatorid_chr"].ToString().Trim();
                            }
                            else
                            {
                                objItems[i1].m_objCreator.strEmpID = "";
                                objItems[i1].m_objCreator.strLastName = "";
                            }
                            objItems[i1].m_strCreateDate = dtbSource.Rows[i1]["createdate_dat"].ToString().Trim();

                            if (dtbSource.Rows[i1]["aduitemp_chr"].ToString().Trim() != "")
                            {
                                objItems[i1].m_objAduitEmp.strEmpID = dtbSource.Rows[i1]["aduitemp_chr"].ToString().Trim();
                            }
                            else
                            {
                                objItems[i1].m_objAduitEmp.strEmpID = "";
                                objItems[i1].m_objAduitEmp.strLastName = "";
                            }
                            objItems[i1].m_strAduitDate = dtbSource.Rows[i1]["aduitdate_dat"].ToString().Trim();

                            if (dtbSource.Rows[i1]["acctemp_chr"].ToString().Trim() != "")
                            {
                                objItems[i1].m_objAcctEmp.strEmpID = dtbSource.Rows[i1]["acctemp_chr"].ToString().Trim();
                            }
                            else
                            {
                                objItems[i1].m_objAcctEmp.strEmpID = "";
                                objItems[i1].m_objAcctEmp.strLastName = "";
                            }
                            objItems[i1].m_strAcctDate = dtbSource.Rows[i1]["acctdate_dat"].ToString().Trim();
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

        #region 将DataTable数据传递到明细VO上
        /// <summary>
        /// 将DataTable数据传递到明细VO上
        /// </summary>
        /// <param name="dtbSource">数据源</param>
        /// <param name="objItems">明细VO</param>
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsStorageOrdDe_VO[] objItems)
        {
            objItems = new clsStorageOrdDe_VO[0];

            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStorageOrdDe_VO[intRow];
                        for (int i1 = 0; i1 < intRow; i1++)
                        {
                            objItems[i1] = new clsStorageOrdDe_VO();
                            objItems[i1].m_objStorageOrdType = new clsStorageOrdType_VO();
                            objItems[i1].m_objMedicine = new clsMedicine_VO();
                            objItems[i1].m_objUnit = new clsUnit_VO();
                            objItems[i1].m_objProductor = new clsVendor_VO();

                            objItems[i1].m_strStorageOrdDetailID = dtbSource.Rows[i1]["STORAGEORDDEID_CHR"].ToString().Trim();
                            objItems[i1].m_strStorageOrdID = dtbSource.Rows[i1]["STORAGEORDID_CHR"].ToString().Trim();

                            objItems[i1].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i1]["STORAGEORDTYPEID_CHR"].ToString().Trim();
                            objItems[i1].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i1]["storageordtypename_vchr"].ToString().Trim();
                            objItems[i1].m_objStorageOrdType.m_intSign = int.Parse(dtbSource.Rows[i1]["sign_int"].ToString().Trim());
                            objItems[i1].m_objStorageOrdType.m_intDeptType = int.Parse(dtbSource.Rows[i1]["depttype_int"].ToString().Trim());

                            objItems[i1].m_objMedicine.m_strMedicineID = dtbSource.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strMedicineName = dtbSource.Rows[i1]["medicinename_vchr"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strMedSpec = dtbSource.Rows[i1]["medspec_vchr"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strMedicineEngName = dtbSource.Rows[i1]["medicineengname_vchr"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strPYCode = dtbSource.Rows[i1]["pycode_chr"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strWBCode = dtbSource.Rows[i1]["wbcode_chr"].ToString().Trim();

                            objItems[i1].m_objUnit.m_strUnitID = dtbSource.Rows[i1]["unitid_chr"].ToString().Trim();
                            objItems[i1].m_objUnit.m_strUnitName = dtbSource.Rows[i1]["unitname_chr"].ToString().Trim();

                            if (dtbSource.Rows[i1]["PRODCUTORID_CHR"].ToString().Trim() != null)
                            {
                                objItems[i1].m_objProductor.m_strVendorID = dtbSource.Rows[i1]["PRODCUTORID_CHR"].ToString().Trim();
                                objItems[i1].m_objProductor.m_strVendorName = dtbSource.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim();
                            }
                            objItems[i1].m_strRowNo = dtbSource.Rows[i1]["ROWNO_CHR"].ToString().Trim();
                            objItems[i1].m_strSysLotNo = dtbSource.Rows[i1]["SYSLOTNO_CHR"].ToString().Trim();
                            objItems[i1].m_strLotNo = dtbSource.Rows[i1]["LOTNO_VCHR"].ToString().Trim();
                            objItems[i1].m_strOrdDate = dtbSource.Rows[i1]["ORD_DAT"].ToString().Trim();
                            objItems[i1].m_strUsefulLifeDate = dtbSource.Rows[i1]["USEFULLIFE_DAT"].ToString();

                            if (dtbSource.Rows[i1]["QTY_DEC"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltQty = float.Parse(dtbSource.Rows[i1]["QTY_DEC"].ToString().Trim());
                            }
                            if (dtbSource.Rows[i1]["BUYUNITPRICE_MNY"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltBuyUnitPrice = float.Parse(dtbSource.Rows[i1]["BUYUNITPRICE_MNY"].ToString().Trim());
                            }
                            if (dtbSource.Rows[i1]["SALEUNITPRICE_MNY"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltSaleUnitPrice = float.Parse(dtbSource.Rows[i1]["SALEUNITPRICE_MNY"].ToString().Trim());
                                objItems[i1].m_fltSaleTolPrice = objItems[i1].m_fltQty * objItems[i1].m_fltSaleUnitPrice;
                            }
                            if (dtbSource.Rows[i1]["WHOLESALEUNITPRICE_MNY"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltWholesaleUnitPrice = float.Parse(dtbSource.Rows[i1]["WHOLESALEUNITPRICE_MNY"].ToString().Trim());
                            }
                            if (dtbSource.Rows[i1]["DISCOUNT_DEC"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltDiscount = float.Parse(dtbSource.Rows[i1]["DISCOUNT_DEC"].ToString().Trim());
                            }
                            if (dtbSource.Rows[i1]["BUYTOLPRICE_MNY"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltBuyTolPrice = float.Parse(dtbSource.Rows[i1]["BUYTOLPRICE_MNY"].ToString().Trim());
                            }

                            objItems[i1].m_strInvoiceNo = dtbSource.Rows[i1]["INVOICENO_VCHR"].ToString().Trim();
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

        #endregion

        #region 模糊查询出入库记录单  欧阳孔伟  2004-05-14
        /// <summary>
        /// 模糊查询出入库记录单
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOrdByAny(string p_strSQL, out clsStorageOrd_VO[] p_objResult)
        {
            p_objResult = new clsStorageOrd_VO[0];
            long lngRes = 0;

            string strSQL = @"SELECT *
								FROM v_opr_storageord
							  " + p_strSQL;
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null)
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

        #region 按记录单号查询出入库记录单  欧阳孔伟  2004-05-14
        /// <summary>
        /// 按记录单查询出入库记录单
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strOrdID">单据号</param>
        /// <param name="p_strOrdType">单据类型</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOrdByID(string p_strOrdID, string p_strOrdType, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];
            string strWhere = @" WHERE storageordid_chr = '" + p_strOrdID + "' and storageordtypeid_chr = '" + p_strOrdType + "'";

            lngRes = m_lngFindOrdByAny(strWhere, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 按仓库查询出入库记录单　欧阳孔伟　2004-06-17
        /// <summary>
        /// 按仓库查询出入库记录单
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOrdByStorage(string p_strID, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];
            string strSQL = @" WHERE storageid_chr = '" + p_strID + "' ";

            lngRes = m_lngFindOrdByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 按帐务期查询出入库单　欧阳孔伟　2004-06-17
        /// <summary>
        /// 按帐务期查询出入库记录单
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOrdByPeriod(string p_strID, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];

            string strSQL = @" WHERE periodid_chr = '" + p_strID + "' ";

            lngRes = m_lngFindOrdByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 按时间段查询出入库单　欧阳孔伟　2004-06-17
        /// <summary>
        /// 按时间查询出入库记录单
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strStartDate">开始时间</param>
        /// <param name="p_strEndDate">结束时间</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        public long m_lngFindOrdByDate(string p_strStartDate, string p_strEndDate, out clsStorageOrd_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrd_VO[0];

            string strSQL = @" WHERE inord_dat >= TO_DATE('" + p_strStartDate + "','yyyy-mm-dd hh24:mi:ss') AND inord_dat <= TO_DATE('" + p_strEndDate + "','yyyy-mm-dd hh24:mi:ss')";

            lngRes = m_lngFindOrdByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 模糊查询出入库明细单　欧阳孔伟　2004-06-17
        /// <summary>
        /// 模糊查询出入库明细单
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOrdDeByAny(string p_strSQL, out clsStorageOrdDe_VO[] p_objResult)
        {
            p_objResult = new clsStorageOrdDe_VO[0];
            long lngRes = 0;


            string strSQL = @"SELECT *
								FROM v_opr_storageorddetail
							  " + p_strSQL;
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null)
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

        #region 按单据号查询出入库单明细　欧阳孔伟　2004-05-14
        /// <summary>
        ///  按单据号查询出入库单明细
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strOrdID">记录单ID</param>
        /// <param name="p_strOrdType">单据类型</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOrdDeByOrdID(string p_strOrdID, string p_strOrdType, out clsStorageOrdDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdDe_VO[0];

            string strSQL = @"WHERE storageordid_chr = '" + p_strOrdID + "' AND storageordtypeid_chr='" + p_strOrdType + "'";
            lngRes = m_lngFindOrdDeByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 按药品查询出入库单明细　欧阳孔伟　2004-06-17
        /// <summary>
        /// 按药品查询出入库单明细
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResult">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOrdDeByMedicine(string p_strID, out clsStorageOrdDe_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStorageOrdDe_VO[0];

            string strSQL = @"WHERE medicineid_chr = '" + p_strID + "'";
            lngRes = m_lngFindOrdDeByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region 审核单据　欧阳孔伟　2004-05-14
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAuditOrd(clsStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;

            //更新单据记录表
            string strSQL = @"UPDATE t_opr_StorageOrd SET aduitemp_chr='" + p_objOrd.m_objAduitEmp.strEmpID + "',aduitdate_dat=To_date('" + p_objOrd.m_strAduitDate + "','yyyy.mm.dd hh24:mi:ss') " +
                " WHERE storageordid_chr='" + p_objOrd.m_strStorageOrdID + "' and storageordtypeid_chr='" + p_objOrd.m_objStorageOrdType.m_strStorageOrdTypeID + "' and storageid_chr = '" + p_objOrd.m_objStorage.m_strStroageID + "'";
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

        #region 审核单据，更改库存　欧阳孔伟　2004-06-08
        /// <summary>
        /// 审核单据，更改库存
        /// </summary>
        /// <param name="p_objPrincipal">安全符</param>
        /// <param name="p_strStorageOrdID">单据号</param>
        /// <param name="p_strStorageOrdTypeID">单据类型号</param>
        /// <param name="p_intFlag">返回标识，1：成功  0：失败  -1：异常</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAuditOrdToChangeStorage(string p_strStorageOrdID, string p_strStorageOrdTypeID, out int p_intFlag)
        {
            long lngRes = 0;

            p_intFlag = 0;
            try
            {
                string strProcedure = "P_STORAGEORDADUIT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[3];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strStorageOrdID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "ordid";

                objParams[1].objParameter_Value = p_strStorageOrdTypeID;
                objParams[1].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[1].strParameter_Name = "typeid";

                objParams[2].strParameter_Type = clsOracleDbType.strInt32;
                objParams[2].strParameter_Direction = clsDirection.strOutput;
                objParams[2].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                p_intFlag = int.Parse(objParams[2].objParameter_Value.ToString().Trim());

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

        #region 检查价格　欧阳孔伟　2004-05-14
        /*////////////////////////////////////////////////
		 * 目前定义了接口，留待以后扩展
		 * 欧阳孔伟
		 * 2004-05-14
		 */
        /////////////////////////////////////////////////
        /// <summary>
        /// 检查价格，参数p_strPriceType为检查的价格类型：1为买入价，2为零售价，3为批发价
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID"></param>
        /// <param name="p_strPriceType"></param>
        /// <param name="p_fltPrice"></param>
        /// <returns></returns>
        public long m_lngCheckMdPrice(string p_strMedicineID, string p_strPriceType, float p_fltPrice)
        {
            long lngRes = 0;

            return lngRes;
        }
        #endregion

        #region 分配药品出库，此函数在申请的总数量大于该批时，系统根据设置进行默认的分配方案　欧阳孔伟　2004-05-14
        /*////////////////////////////////////////////////
		 * 目前定义了接口，留待以后扩展
		 * 欧阳孔伟
		 * 2004-05-14
		 */
        /////////////////////////////////////////////////
        /// <summary>
        /// 在出库时，对出库数大于选出的某批次药品时，对药品不同批次的分配
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID"></param>
        /// <param name="p_dtbMedicine"></param>
        /// <returns></returns>
        public long m_lngAllotMedicine(string p_strMedicineID, out System.Data.DataTable p_dtbMedicine)
        {
            long lngRes = 0;

            p_dtbMedicine = null;



            return lngRes;
        }
        #endregion

        #region 获取明细单ID　欧阳孔伟　2004-05-25
        /// <summary>
        /// 获取明细单ID，参数p_intLen为ID的长度
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intLen"></param>
        /// <param name="p_strOrdDeID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrdDeID(int p_intLen, out string p_strOrdDeID)
        {
            long lngRes = 0;
            p_strOrdDeID = null;

            System.Data.DataTable dtbResult = new System.Data.DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                p_strOrdDeID = objHRPSvc.m_strGetNewID("t_opr_storageordde", "storageorddeid_chr", p_intLen);

                if (p_strOrdDeID != null)
                {
                    lngRes = 1;
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

        #region 新系统的操作方法
        /// <summary>
        /// 插入入库单数据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objResult">输入单据数据</param>
        /// <param name="listItem">输入明细数据</param>
        /// <param name="newID">输出单号ID</param>
        /// <param name="isCheck">是否检查单据号是否存在，trun 检查，false-不检查</param>
        /// <param name="signint">1－入库 2－出库3-退库4-退库</param>
        /// <returns>返回-2表示单据号已经被占用</returns>
        [AutoComplete]
        public long m_lngInsertMetStorageOrd(clsMedStorageOrd_VO p_objResult, clsMedStorageOrdDe_VO[] listItem, out string newID, bool isCheck, int signint)
        {
            long lngRes = 0;
            newID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            #region 检查单据号是否己经被占用
            if (isCheck == true)
            {
                strSQL = @"select STORAGEORDID_CHR from t_opr_storageord where DOCID_VCHR='" + p_objResult.m_strDOCID_VCHR + "'";
                DataTable dt = new DataTable();
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
                if (dt.Rows.Count > 0)
                {
                    return -2;
                }
            }
            #endregion
            lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_storageord", "STORAGEORDID_CHR", out newID);
            strSQL = @"insert into t_opr_storageord(STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,STORAGEID_CHR,PERIODID_CHR,INORD_DAT,TOLMNY_MNY,PSTATUS_INT,DOCID_VCHR,OFFERID_CHR,DEPTID_CHR,VENDORID_CHR,CREATORID_CHR,CREATEDATE_DAT,MEMO_VCHR,sign_int) 
                            VALUES('" + newID + "','" + p_objResult.m_strSTORAGEORDTYPEID_CHR + "','" + p_objResult.m_strSTORAGEID_CHR + "','" + p_objResult.m_strPERIODID_CHR + "',To_Date('" + p_objResult.m_strINORD_DAT + "','yyyy-mm-dd hh24:mi:ss')" + "," + p_objResult.m_dblTOLMNY_MNY
                                   + "," + p_objResult.m_intPSTATUS_INT + ",'" + p_objResult.m_strDOCID_VCHR + "','" + p_objResult.m_strOFFERID_CHR + "','" + p_objResult.m_strDEPTID_CHR + "','" + p_objResult.m_strVENDORID_CHR + "','" + p_objResult.m_strCREATORID_CHR + "',To_Date('" + p_objResult.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                                 + ",'" + p_objResult.m_strMEMO_VCHR + "'," + signint.ToString() + ")";
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
            if (listItem.Length > 0)
            {
                string intStarNO = "";

                for (int i1 = 0; i1 < listItem.Length; i1++)
                {
                    objHRPSvc.m_lngGenerateNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", out intStarNO);
                    listItem[i1].m_strSTORAGEORDDEID_CHR = intStarNO;
                    strSQL = @"INSERT INTO t_opr_storageordde (STORAGEORDDEID_CHR,STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,MEDICINEID_CHR,ORD_DAT,
                           ROWNO_CHR,UNITID_CHR,LOTNO_VCHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,BUYUNITPRICE_MNY,WHOLESALEUNITPRICE_MNY,
                           DISCOUNT_DEC,BUYTOLPRICE_MNY,INVOICENO_VCHR,PACKAGEQTY_DEC,PACKAGEPRICE_MNY,SALEUNITPRICE_MNY,ORDERQTY_DEC,ORDERUNIT_VCHR,ORDERUNITPRICE_MNY,ORDERPKGQTY_DEC,SYSLOTNO_CHR,AIMUNITPRICE_MNY,LIMITUNITPRICE_MNY,AIMUNIT_INT,SIGN_INT) 
                          VALUES('" + listItem[i1].m_strSTORAGEORDDEID_CHR + "','" + newID + "','" + listItem[i1].m_strSTORAGEORDTYPEID_CHR
                        + "','" + listItem[i1].m_strMEDICINEID_CHR + "',sysdate,'" + listItem[i1].m_strROWNO_CHR + "','" + listItem[i1].m_strUNITID_CHR + "','" + listItem[i1].m_strLOTNO_VCHR + "',To_Date('" + listItem[i1].m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                        + ",'" + listItem[i1].m_strPRODCUTORNAME_CHR + "'," + listItem[i1].m_dblQTY_DEC + "," + listItem[i1].m_dblBUYUNITPRICE_MNY + "," + listItem[i1].m_dblWHOLESALEUNITPRICE_MNY
                        + "," + listItem[i1].m_fltDISCOUNT_DEC + "," + listItem[i1].m_dblBUYTOLPRICE_MNY + ",'" + listItem[i1].m_strINVOICENO_VCHR + "'," + listItem[i1].m_dblPACKAGEQTY_DEC
                        + "," + listItem[i1].m_dblPACKAGEPRICE_MNY + "," + listItem[i1].m_dblSALEUNITPRICE_MNY + "," + listItem[i1].m_strORDERQTY_DEC + ",'" + listItem[i1].m_strORDERUNIT_VCHR + "'," + listItem[i1].m_strORDERUNITPRICE_MNY + "," + listItem[i1].m_strORDERPKGQTY_DEC + ",'" + listItem[i1].m_strSYSLOTNO_CHR + "'," + listItem[i1].m_strAIMUNITPRICE_MNY + "," + listItem[i1].m_strLIMITUNITPRICE_MNY + "," + listItem[i1].m_intAIMUNIT_INT + ",1)";
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

        #region 刷新数据
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="OrdDe_VO"></param>
        /// <param name="OrderID">null-当前入库单为新增</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lonResetData(ref clsMedStorageOrdDe_VO[] OrdDe_VO, string OrderID)
        {
            long lngRes = 0;
            string strSQL;
            DataTable dt = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            for (int i1 = 0; i1 < OrdDe_VO.Length; i1++)
            {
                strSQL = @"select PRODUCTORID_CHR from T_BSE_MEDICINE where MEDICINEID_CHR='" + OrdDe_VO[i1].m_strMEDICINEID_CHR + "'";
                try
                {
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
                    if (OrderID != null)
                    {
                        strSQL = @"update t_opr_storageordde set  PRODCUTORID_CHR='" + dt.Rows[0]["PRODUCTORID_CHR"].ToString() + "' where STORAGEORDDEID_CHR='" + OrdDe_VO[i1].m_strSTORAGEORDDEID_CHR + "' and STORAGEORDID_CHR='" + OrderID + "'";
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
                    OrdDe_VO[i1].m_strPRODCUTORID_CHR = dt.Rows[0]["PRODUCTORID_CHR"].ToString();
                }
            }
            return lngRes;
        }
        #endregion

        #region 修改入库单的行号
        /// <summary>
        /// 修改入库单的行号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyRowNO(DataTable dt)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL;
            for (int i1 = 0; i1 < dt.Rows.Count; i1++)
            {
                strSQL = @"update t_opr_storageordde set ROWNO_CHR='" + dt.Rows[i1]["RowNO"].ToString() + "' where STORAGEORDDEID_CHR='" + dt.Rows[i1]["STORAGEORDDEID"].ToString() + "'";
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

        /// <summary>
        /// 插入入库单明细数据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objResult">输入数据</param>
        /// <param name="tolMoney">输出入库单总金额</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertMetStorageOrdDe(clsMedStorageOrdDe_VO p_objResult, out double tolMoney)
        {
            long lngRes = 0;
            tolMoney = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string p_strNewID = "";
            objHRPSvc.m_lngGenerateNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", out p_strNewID);
            p_objResult.m_strSTORAGEORDDEID_CHR = p_strNewID;
            string strSQL = @"INSERT INTO t_opr_storageordde (STORAGEORDDEID_CHR,STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,MEDICINEID_CHR,ORD_DAT,
                           ROWNO_CHR,UNITID_CHR,LOTNO_VCHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,BUYUNITPRICE_MNY,WHOLESALEUNITPRICE_MNY,
                           DISCOUNT_DEC,BUYTOLPRICE_MNY,INVOICENO_VCHR,PACKAGEQTY_DEC,PACKAGEPRICE_MNY,SALEUNITPRICE_MNY,ORDERQTY_DEC,ORDERUNIT_VCHR,ORDERUNITPRICE_MNY,ORDERPKGQTY_DEC,SYSLOTNO_CHR,AIMUNITPRICE_MNY,LIMITUNITPRICE_MNY,SIGN_INT) 
                          VALUES('" + p_objResult.m_strSTORAGEORDDEID_CHR + "','" + p_objResult.m_strSTORAGEORDID_CHR + "','" + p_objResult.m_strSTORAGEORDTYPEID_CHR
                + "','" + p_objResult.m_strMEDICINEID_CHR + "',To_Date('" + p_objResult.m_strORD_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                + ",'" + p_objResult.m_strROWNO_CHR + "','" + p_objResult.m_strUNITID_CHR + "','" + p_objResult.m_strLOTNO_VCHR + "',To_Date('" + p_objResult.m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                + ",'" + p_objResult.m_strPRODCUTORNAME_CHR + "'," + p_objResult.m_dblQTY_DEC + "," + p_objResult.m_dblBUYUNITPRICE_MNY + "," + p_objResult.m_dblWHOLESALEUNITPRICE_MNY
                + "," + p_objResult.m_fltDISCOUNT_DEC + "," + p_objResult.m_dblBUYTOLPRICE_MNY + ",'" + p_objResult.m_strINVOICENO_VCHR + "'," + p_objResult.m_dblPACKAGEQTY_DEC
                + "," + p_objResult.m_dblPACKAGEPRICE_MNY + "," + p_objResult.m_dblSALEUNITPRICE_MNY + "," + p_objResult.m_strORDERQTY_DEC + ",'" + p_objResult.m_strORDERUNIT_VCHR + "'," + p_objResult.m_strORDERUNITPRICE_MNY + "," + p_objResult.m_strORDERPKGQTY_DEC + ",'" + p_objResult.m_strSYSLOTNO_CHR + "'," + p_objResult.m_strAIMUNITPRICE_MNY + "," + p_objResult.m_strLIMITUNITPRICE_MNY + ",1)";
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


        #region 获取所有的药房
        [AutoComplete]
        public long m_lonGetStore(out DataTable dt)
        {
            dt = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT  MEDSTOREID_CHR,MEDSTORENAME_VCHR FROM t_bse_medstore order by MEDSTOREID_CHR";
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

        #region 获取上一次入库数据
        /// <summary>
        /// 获取上一次入库数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="medID"></param>
        /// <param name="ConsoltPrice"></param>
        /// <param name="ConsoltUnit"></param>
        /// <param name="ConsoltOrdPrice"></param>
        /// <param name="ConsoltORDERPKGQTY"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetConsoltPrice(string medID, out string ConsoltPrice, out string ConsoltUnit, out string ConsoltOrdPrice, out string ConsoltORDERPKGQTY, out string ConsoltAIMUNITPRICE, out string ConsoltLIMITUNITPRICE)
        {
            ConsoltPrice = "0.00";
            ConsoltUnit = "";
            ConsoltOrdPrice = "";
            ConsoltORDERPKGQTY = "1";
            ConsoltAIMUNITPRICE = "";
            ConsoltLIMITUNITPRICE = "";
            long lngRes = 0;
            string strSQL = @"select b.AIMUNITPRICE_MNY,b.LIMITUNITPRICE_MNY,BUYUNITPRICE_MNY,b.ORDERUNIT_VCHR,b.ORDERUNITPRICE_MNY,b.ORDERPKGQTY_DEC from (select Max(ORD_DAT) as MaxORD_DAT from t_opr_storageordde a,T_OPR_STORAGEORD b where a.MEDICINEID_CHR='" + medID + "' and a.STORAGEORDID_CHR=b.STORAGEORDID_CHR and b.SIGN_INT=1) a,t_opr_storageordde b where b.ORD_DAT=a.MaxORD_DAT";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (dtbResult.Rows.Count > 0)
                {
                    ConsoltPrice = dtbResult.Rows[0]["BUYUNITPRICE_MNY"].ToString();
                    ConsoltUnit = dtbResult.Rows[0]["ORDERUNIT_VCHR"].ToString();
                    ConsoltOrdPrice = dtbResult.Rows[0]["ORDERUNITPRICE_MNY"].ToString();
                    ConsoltORDERPKGQTY = dtbResult.Rows[0]["ORDERPKGQTY_DEC"].ToString();
                    ConsoltAIMUNITPRICE = dtbResult.Rows[0]["AIMUNITPRICE_MNY"].ToString();
                    ConsoltLIMITUNITPRICE = dtbResult.Rows[0]["LIMITUNITPRICE_MNY"].ToString();
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

        #region 获取出库单据类型
        /// <summary>
        /// 获取出库单据类型
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <param name="strSign">1-入库，2-出库</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutOrdType(out DataTable dtbResult, string strSign)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strSQL = @"SELECT *
							  FROM t_aid_storageordtype 
							    where SIGN_INT= " + strSign;
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

        #region 导数据
        /// <summary>
        /// 导数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strINordID">入库单ID</param>
        /// <param name="strOrdTypeID">出库单类型</param>
        /// <param name="strPERIODID">出库单的财务期</param>
        /// <param name="strOldDocID">旧的单据号</param>
        /// <param name="strNewDOCID">新的单据号</param>
        ///  <param name="strCREATOR">创建人</param>
        ///   <param name="strSIGN">出入标志 1－入库 2－出库</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGuideRope(string strINordID, string strOrdTypeID, string strPERIODID, string strOldDocID, string strNewDOCID, string strCREATOR, string strSIGN)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string newID = "";
            if (strSIGN == "1")
            {
                strOldDocID = "出库界面导入的单据，出库单据号为：" + strOldDocID;
            }
            else
            {
                strOldDocID = "入库界面导入的单据,入库单据号为：" + strOldDocID;

            }
            lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_storageord", "STORAGEORDID_CHR", out newID);
            string strSQL = @"insert into t_opr_storageord(STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,STORAGEID_CHR,PERIODID_CHR,INORD_DAT,TOLMNY_MNY,PSTATUS_INT,DOCID_VCHR,VENDORID_CHR,CREATORID_CHR,CREATEDATE_DAT,MEMO_VCHR,SIGN_INT)  select '" + newID + "','" + strOrdTypeID + "',STORAGEID_CHR,'" + strPERIODID + "',SysDate,TOLMNY_MNY,1,'" + strNewDOCID + "',VENDORID_CHR,'" + strCREATOR + "',sysDate,'" + strOldDocID + "'," + strSIGN + "  from t_opr_storageord where STORAGEORDID_CHR='" + strINordID + "'";

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
            DataTable dtResult = new DataTable();
            strSQL = @"select STORAGEORDDEID_CHR from  t_opr_storageordde where STORAGEORDID_CHR='" + strINordID + "'";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtResult.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtResult.Rows.Count; i1++)
                {
                    string newDeID = "";
                    if (strSIGN == "1")
                    {
                        lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", out newDeID);
                        strSQL = @"INSERT INTO t_opr_storageordde (STORAGEORDDEID_CHR,STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,MEDICINEID_CHR,ORD_DAT,
                           ROWNO_CHR,UNITID_CHR,LOTNO_VCHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,SALEUNITPRICE_MNY,BUYUNITPRICE_MNY,
                           BUYTOLPRICE_MNY,WHOLESALEUNITPRICE_MNY,INVOICENO_VCHR,PACKAGEQTY_DEC,PACKAGEPRICE_MNY,DEPTID_CHR,ORDERQTY_DEC,ORDERUNIT_VCHR,ORDERUNITPRICE_MNY,ORDERPKGQTY_DEC,SIGN_INT)select '" + newDeID + "','" + newID + "','" + strOrdTypeID + "',MEDICINEID_CHR,sysDate,ROWNO_CHR,UNITID_CHR,LOTNO_VCHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,SALEUNITPRICE_MNY,BUYUNITPRICE_MNY,BUYTOLPRICE_MNY,WHOLESALEUNITPRICE_MNY,INVOICENO_VCHR,PACKAGEQTY_DEC,PACKAGEPRICE_MNY,DEPTID_CHR,ORDERQTY_DEC,ORDERUNIT_VCHR,ORDERUNITPRICE_MNY,ORDERPKGQTY_DEC," + strSIGN + "  FROM t_opr_storageordde where STORAGEORDDEID_CHR='" + dtResult.Rows[i1]["STORAGEORDDEID_CHR"].ToString() + "' and STORAGEORDID_CHR='" + strINordID + "'";
                    }
                    else
                    {
                        lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", out newDeID);
                        strSQL = @"INSERT INTO t_opr_storageordde (STORAGEORDDEID_CHR,STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,MEDICINEID_CHR,SYSLOTNO_CHR,ORD_DAT,
                           ROWNO_CHR,UNITID_CHR,LOTNO_VCHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,SALEUNITPRICE_MNY,BUYUNITPRICE_MNY,
                           BUYTOLPRICE_MNY,WHOLESALEUNITPRICE_MNY,INVOICENO_VCHR,PACKAGEQTY_DEC,PACKAGEPRICE_MNY,DEPTID_CHR,ORDERQTY_DEC,ORDERUNIT_VCHR,ORDERUNITPRICE_MNY,ORDERPKGQTY_DEC,SIGN_INT)select '" + newDeID + "','" + newID + "','" + strOrdTypeID + "',MEDICINEID_CHR,SYSLOTNO_CHR,sysDate,ROWNO_CHR,UNITID_CHR,LOTNO_VCHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,SALEUNITPRICE_MNY,BUYUNITPRICE_MNY,BUYTOLPRICE_MNY,WHOLESALEUNITPRICE_MNY,INVOICENO_VCHR,PACKAGEQTY_DEC,PACKAGEPRICE_MNY,DEPTID_CHR,ORDERQTY_DEC,ORDERUNIT_VCHR,ORDERUNITPRICE_MNY,ORDERPKGQTY_DEC," + strSIGN + "  FROM t_opr_storageordde where STORAGEORDDEID_CHR='" + dtResult.Rows[i1]["STORAGEORDDEID_CHR"].ToString() + "' and STORAGEORDID_CHR='" + strINordID + "'";
                    }
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

        #region 获得所有入库单数据
        /// <summary>
        /// 获得所有入库单数据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objResultArr">返回数据</param>
        /// <param name="priod">财务期</param>
        /// <param name="ordTypeID">出入库类型ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageOrdList(out clsMedStorageOrd_VO[] p_objResultArr, string priod, string ordTypeID)
        {
            p_objResultArr = null;
            long lngRes = 0;
            string strSQL;
            string strWhere = "";
            if (priod != "")
            {
                strWhere = " and a.PERIODID_CHR='" + priod + "'";
            }
            strSQL = @"SELECT   a.*, b.storageordtypename_vchr, c.storagename_vchr,
         d.lastname_vchr AS offername, e.lastname_vchr AS creatname,
         f.lastname_vchr AS aduitname, j.vendorname_vchr, k.medstorename_vchr,h.tolmoney
    FROM t_opr_storageord a,
         t_aid_storageordtype b,
         t_bse_storage c,
         t_bse_employee d,
         t_bse_employee e,
         t_bse_employee f,
         t_bse_vendor j,
         t_bse_medstore k,
         (SELECT storageordid_chr, SUM (buyunitprice_mny * qty_dec) AS tolmoney
              FROM t_opr_storageordde where STORAGEORDTYPEID_CHR='" + ordTypeID + @"'
          GROUP BY storageordid_chr) h
   WHERE a.storageordtypeid_chr = b.storageordtypeid_chr
     and a.STORAGEORDID_CHR=h.STORAGEORDID_CHR(+)
     and a.STORAGEORDTYPEID_CHR='" + ordTypeID + @"' 
     AND a.storageid_chr = c.storageid_chr
     AND a.offerid_chr = d.empid_chr(+)
     AND a.creatorid_chr = e.empid_chr(+)
     AND a.aduitemp_chr = f.empid_chr(+)
     AND a.vendorid_chr = j.vendorid_chr(+)
     AND a.deptid_chr = k.medstoreid_chr(+)
     " + strWhere + @" 
ORDER BY TRIM (docid_vchr) DESC ";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedStorageOrd_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedStorageOrd_VO();
                        p_objResultArr[i1].m_strDOCID_VCHR = dtbResult.Rows[i1]["DOCID_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPTNAME_CHR = dtbResult.Rows[i1]["MEDSTORENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDTYPENAME_CHR = dtbResult.Rows[i1]["STORAGEORDTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGENAME_CHR = dtbResult.Rows[i1]["STORAGEORDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOFFERIDNAME_CHR = dtbResult.Rows[i1]["OfferName"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATORNAME_CHR = dtbResult.Rows[i1]["CreatName"].ToString().Trim();
                        p_objResultArr[i1].m_strADUITEMPNAME_CHR = dtbResult.Rows[i1]["AduitNAME"].ToString().Trim();
                        p_objResultArr[i1].m_strVENDORNAME_CHR = dtbResult.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDID_CHR = dtbResult.Rows[i1]["STORAGEORDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDTYPEID_CHR = dtbResult.Rows[i1]["STORAGEORDTYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEID_CHR = dtbResult.Rows[i1]["STORAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPERIODID_CHR = dtbResult.Rows[i1]["PERIODID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["INORD_DAT"].ToString() != "")
                            p_objResultArr[i1].m_strINORD_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["INORD_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        if (dtbResult.Rows[i1]["tolmoney"].ToString() != "")
                            p_objResultArr[i1].m_dblTOLMNY_MNY = Convert.ToDouble(dtbResult.Rows[i1]["tolmoney"].ToString().Trim());
                        if (dtbResult.Rows[i1]["PSTATUS_INT"].ToString() != "")
                            p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strDOCID_VCHR = dtbResult.Rows[i1]["DOCID_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strOFFERID_CHR = dtbResult.Rows[i1]["OFFERID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPTID_CHR = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strVENDORID_CHR = dtbResult.Rows[i1]["VENDORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATORID_CHR = dtbResult.Rows[i1]["CREATORID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["CREATEDATE_DAT"].ToString() != "")
                            p_objResultArr[i1].m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strADUITEMP_CHR = dtbResult.Rows[i1]["ADUITEMP_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ADUITDATE_DAT"].ToString() != "")
                        {
                            p_objResultArr[i1].m_strADUITDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ADUITDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
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

        #region 获得所有所有药房
        /// <summary>
        /// 获得所有所有药房
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorage(out clsMedStore_VO[] p_objResultArr)
        {
            p_objResultArr = null;
            long lngRes = 0;
            string strSQL = @"SELECT MEDSTOREID_CHR,MEDSTORENAME_VCHR from T_BSE_MEDSTORE WHERE MEDSTORETYPE_INT=1";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedStore_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedStore_VO();
                        p_objResultArr[i1].m_strMedStoreID = dtbResult.Rows[i1]["MEDSTOREID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMedStoreName = dtbResult.Rows[i1]["MEDSTORENAME_VCHR"].ToString().Trim();
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

        #region 根据单号获得明细数据
        /// <summary>
        /// 根据单号获得明细数据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="StorageID">单号</param>
        /// <param name="p_objResultArr">返回值</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStorageOrdDe(string StorageID, out clsMedStorageOrdDe_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedStorageOrdDe_VO[0];
            long lngRes = 0;

            //  添加distinct[YMH20061121]
            string strSQL = @"select distinct h.*,k.amount_dec from (SELECT a.*,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,b.UNITPRICE_MNY,b.TRADEPRICE_MNY,b.ASSISTCODE_CHR,c.STORAGEID_CHR FROM T_OPR_STORAGEORDDE a,t_opr_storageord c,t_bse_medicine b WHERE a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.storageordid_chr=c.storageordid_chr and a.STORAGEORDID_CHR ='" + StorageID + "') h,(select sum(CURQTY_DEC) as amount_dec,MEDICINEID_CHR,STORAGEID_CHR  from t_opr_storagemeddetail group by MEDICINEID_CHR,STORAGEID_CHR)  k where h.STORAGEID_CHR=k.STORAGEID_CHR(+) and h.MEDICINEID_CHR=k.MEDICINEID_CHR(+) order by h.ROWNO_CHR";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedStorageOrdDe_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedStorageOrdDe_VO();
                        p_objResultArr[i1].m_strspec = dtbResult.Rows[i1]["MEDSPEC_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString().Trim() != "")
                            p_objResultArr[i1].m_dblSALEUNITPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["UNITPRICE_MNY"].ToString().Trim());
                        else
                            p_objResultArr[i1].m_dblSALEUNITPRICE_MNY = 0.00;
                        if (dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim() != "")
                            p_objResultArr[i1].m_dblWHOLESALEUNITPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["TRADEPRICE_MNY"].ToString().Trim());
                        else
                            p_objResultArr[i1].m_dblWHOLESALEUNITPRICE_MNY = 0.00;
                        p_objResultArr[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["ASSISTCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDICINENAME_CHR = dtbResult.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPRODCUTORNAME_CHR = dtbResult.Rows[i1]["PRODCUTORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDDEID_CHR = dtbResult.Rows[i1]["STORAGEORDDEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDID_CHR = dtbResult.Rows[i1]["STORAGEORDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDICINEID_CHR = dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSYSLOTNO_CHR = dtbResult.Rows[i1]["SYSLOTNO_CHR"].ToString().Trim();

                        p_objResultArr[i1].m_strORDERPKGQTY_DEC = dtbResult.Rows[i1]["ORDERPKGQTY_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERQTY_DEC = dtbResult.Rows[i1]["ORDERQTY_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERUNIT_VCHR = dtbResult.Rows[i1]["ORDERUNIT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERUNITPRICE_MNY = dtbResult.Rows[i1]["ORDERUNITPRICE_MNY"].ToString().Trim();

                        p_objResultArr[i1].m_strLIMITUNITPRICE_MNY = dtbResult.Rows[i1]["LIMITUNITPRICE_MNY"].ToString().Trim();
                        p_objResultArr[i1].m_strAIMUNITPRICE_MNY = dtbResult.Rows[i1]["AIMUNITPRICE_MNY"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ORD_DAT"].ToString() != "")
                            p_objResultArr[i1].m_strORD_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ORD_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strROWNO_CHR = dtbResult.Rows[i1]["ROWNO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUNITID_CHR = dtbResult.Rows[i1]["UNITID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strLOTNO_VCHR = dtbResult.Rows[i1]["LOTNO_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["USEFULLIFE_DAT"].ToString() != "")
                            p_objResultArr[i1].m_strUSEFULLIFE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["USEFULLIFE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strPRODCUTORID_CHR = dtbResult.Rows[i1]["PRODCUTORID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["QTY_DEC"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblQTY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["QTY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["BUYUNITPRICE_MNY"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblBUYUNITPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["BUYUNITPRICE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["SALEUNITPRICE_MNY"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblSALEUNITPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["SALEUNITPRICE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["WHOLESALEUNITPRICE_MNY"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblWHOLESALEUNITPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["WHOLESALEUNITPRICE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString() != "")
                        {
                            p_objResultArr[i1].m_fltDISCOUNT_DEC = float.Parse(dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["BUYTOLPRICE_MNY"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblBUYTOLPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["BUYTOLPRICE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["PACKAGEQTY_DEC"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblPACKAGEQTY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["PACKAGEQTY_DEC"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strINVOICENO_VCHR = dtbResult.Rows[i1]["INVOICENO_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["AIMUNIT_INT"] == System.DBNull.Value || dtbResult.Rows[i1]["AIMUNIT_INT"].ToString() == "")
                        {
                            p_objResultArr[i1].m_intAIMUNIT_INT = 0;
                        }
                        else
                        {
                            p_objResultArr[i1].m_intAIMUNIT_INT = int.Parse(dtbResult.Rows[i1]["AIMUNIT_INT"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["PACKAGEPRICE_MNY"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblPACKAGEPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["PACKAGEPRICE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["AMOUNT_DEC"].ToString() != "")
                        {
                            p_objResultArr[i1].m_intStorage = Convert.ToDouble(dtbResult.Rows[i1]["AMOUNT_DEC"].ToString().Trim());
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

        #region 根据单号ID删除入库单
        /// <summary>
        /// 根据单号ID删除入库单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdDeID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleStorageOrd(string p_strOrdDeID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"DELETE t_opr_storageordde WHERE STORAGEORDID_CHR = '" + p_strOrdDeID + "'";
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

            if (lngRes > 0)
            {
                strSQL = @"DELETE t_opr_storageord WHERE STORAGEORDID_CHR = '" + p_strOrdDeID + "'";

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

        #region 删除出入库单明细
        /// <summary>
        /// 删除出入库单明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOrdDeID">明细ID</param>
        /// <param name="tolMoney">总金额</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteOrdDeBy(string p_strOrdDeID, out double tolMoney)
        {
            long lngRes = 0;
            string strSQL = "";
            tolMoney = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            strSQL = @"DELETE t_opr_storageordde WHERE STORAGEORDDEID_CHR = '" + p_strOrdDeID + "'";

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

        #region 修改明细表
        /// <summary>
        /// 修改明细表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateOrdDe(clsMedStorageOrdDe_VO p_objOrdDe)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_StorageOrdDe SET MEDICINEID_CHR='" + p_objOrdDe.m_strMEDICINEID_CHR +
                "',UNITID_CHR='" + p_objOrdDe.m_strUNITID_CHR +
                "',LOTNO_VCHR='" + p_objOrdDe.m_strLOTNO_VCHR + "',UsefulLife_dat=To_date('" + p_objOrdDe.m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss')," +
                "PRODCUTORID_CHR='" + p_objOrdDe.m_strPRODCUTORNAME_CHR + "',WHOLESALEUNITPRICE_MNY=" + p_objOrdDe.m_dblWHOLESALEUNITPRICE_MNY + ",QTY_DEC=" + p_objOrdDe.m_dblQTY_DEC + ",Buyunitprice_mny=" + p_objOrdDe.m_dblBUYUNITPRICE_MNY +
                ",Discount_dec = " + p_objOrdDe.m_fltDISCOUNT_DEC + ",BUYTOLPRICE_MNY = " + p_objOrdDe.m_dblBUYTOLPRICE_MNY + ",INVOICENO_VCHR='" + p_objOrdDe.m_strINVOICENO_VCHR.Trim() + "',PACKAGEQTY_DEC=" + p_objOrdDe.m_dblPACKAGEQTY_DEC + ",PACKAGEPRICE_MNY=" + p_objOrdDe.m_dblPACKAGEPRICE_MNY + ",SALEUNITPRICE_MNY=" + p_objOrdDe.m_dblSALEUNITPRICE_MNY + ",AIMUNIT_INT=" + p_objOrdDe.m_intAIMUNIT_INT + "   where STORAGEORDDEID_CHR='" + p_objOrdDe.m_strSTORAGEORDDEID_CHR + "'";
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

        #region 修改入库单
        /// <summary>
        /// 修改入库单
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrd">入库单数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateOrd(clsMedStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_StorageOrd SET STORAGEORDTYPEID_CHR='" + p_objOrd.m_strSTORAGEORDTYPEID_CHR +
                "',STORAGEID_CHR='" + p_objOrd.m_strSTORAGEID_CHR +
                "',TOLMNY_MNY=" + p_objOrd.m_dblTOLMNY_MNY +
                ",DOCID_VCHR='" + p_objOrd.m_strDOCID_VCHR + "',OFFERID_CHR='" + p_objOrd.m_strOFFERID_CHR + "',VENDORID_CHR='" + p_objOrd.m_strVENDORID_CHR + "',CREATEDATE_DAT=To_Date('" + p_objOrd.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss')" +
                ",MEMO_VCHR='" + p_objOrd.m_strMEMO_VCHR + "',DEPTID_CHR='" + p_objOrd.m_strDEPTID_CHR + "' where STORAGEORDID_CHR='" + p_objOrd.m_strSTORAGEORDID_CHR + "'";
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

        #region 修改入库单及明细
        /// <summary>
        /// 修改入库单及明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdID">入库单ID</param>
        /// <param name="p_objOrdDe">明细数据</param>
        /// <param name="tolMoney">新增明细金额</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateOrdDe(string p_objOrdID, clsMedStorageOrdDe_VO p_objOrdDe, out double tolMoney)
        {
            long lngRes = 0;
            tolMoney = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            strSQL = @"UPDATE t_opr_StorageOrdDe SET MEDICINEID_CHR='" + p_objOrdDe.m_strMEDICINEID_CHR +
                "',UNITID_CHR='" + p_objOrdDe.m_strUNITID_CHR +
                "',LOTNO_VCHR='" + p_objOrdDe.m_strLOTNO_VCHR + "',UsefulLife_dat=To_date('" + p_objOrdDe.m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss')," +
                "PRODCUTORID_CHR='" + p_objOrdDe.m_strPRODCUTORNAME_CHR + "',QTY_DEC=" + p_objOrdDe.m_dblQTY_DEC + ",Buyunitprice_mny=" + p_objOrdDe.m_dblBUYUNITPRICE_MNY +
                ",BUYTOLPRICE_MNY = " + p_objOrdDe.m_dblBUYTOLPRICE_MNY + ",INVOICENO_VCHR='" + p_objOrdDe.m_strINVOICENO_VCHR.Trim() + "',ORDERQTY_DEC=" + p_objOrdDe.m_strORDERQTY_DEC + ",ORDERUNIT_VCHR='" + p_objOrdDe.m_strORDERUNIT_VCHR + "',ORDERUNITPRICE_MNY=" + p_objOrdDe.m_strORDERUNITPRICE_MNY + ",ORDERPKGQTY_DEC=" + p_objOrdDe.m_strORDERPKGQTY_DEC + ",AIMUNITPRICE_MNY='" + p_objOrdDe.m_strAIMUNITPRICE_MNY + "',LIMITUNITPRICE_MNY='" + p_objOrdDe.m_strLIMITUNITPRICE_MNY + "',SALEUNITPRICE_MNY=" + p_objOrdDe.m_dblSALEUNITPRICE_MNY + ",AIMUNIT_INT=" + p_objOrdDe.m_intAIMUNIT_INT + " ,WHOLESALEUNITPRICE_MNY=" + p_objOrdDe.m_dblWHOLESALEUNITPRICE_MNY + "  where STORAGEORDDEID_CHR='" + p_objOrdDe.m_strSTORAGEORDDEID_CHR + "'";
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

        #region 审核单据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedOrdID">入库单ID</param>
        /// <param name="storageID">所属仓库ID</param>
        /// <param name="Empman">审核人</param>
        /// <param name="EmpDate">审核日期</param>
        /// <param name="p_objOrdDe">入库单明细数据</param>
        /// <param name="intDept">部门类型，1－院内，0－院外</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngEmpOrd(string MedOrdID, string storageID, string Empman, string EmpDate, clsMedStorageOrdDe_VO[] p_objOrdDe, int intDept)
        {
            long lngRes = 0;
            string strSQL;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            com.digitalwave.iCare.middletier.HIS.clsMedStorageManage mange = new clsMedStorageManage();
            string newSysNO = "";
            for (int i1 = 0; i1 < p_objOrdDe.Length; i1++)//更改总库存
            {
                mange.m_lnghMedInit(p_objOrdDe[i1].m_strMEDICINEID_CHR, p_objOrdDe[i1].m_strLOTNO_VCHR,
                storageID, p_objOrdDe[i1].m_dblQTY_DEC, p_objOrdDe[i1].m_strUNITID_CHR, p_objOrdDe[i1].m_dblBUYUNITPRICE_MNY,
                p_objOrdDe[i1].m_dblSALEUNITPRICE_MNY, p_objOrdDe[i1].m_dblWHOLESALEUNITPRICE_MNY, p_objOrdDe[i1].m_strUSEFULLIFE_DAT,
                p_objOrdDe[i1].m_strPRODCUTORID_CHR, out newSysNO);

                #region 把系统批号写回入库明细
                strSQL = @"update T_OPR_STORAGEORDDE set SYSLOTNO_CHR='" + newSysNO + "' where STORAGEORDDEID_CHR='" + p_objOrdDe[i1].m_strSTORAGEORDDEID_CHR + "'";
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

                #endregion
            }
            if (lngRes == 0)
            {
                return 0;
            }
            //修改入库单的状态
            strSQL = @"UPDATE t_opr_StorageOrd SET PSTATUS_INT=2,aduitemp_chr='" + Empman + "',aduitdate_dat=To_date('" + EmpDate + "','yyyy.mm.dd hh24:mi:ss') " +
                " WHERE storageordid_chr='" + MedOrdID + "'";
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

        #region 审核入库单据，更改库存(不用)
        /// <summary>
        /// 审核入库单据，更改库存
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_strMeDID"></param>
        /// <param name="TolNumber"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOrdToChangeStorage(string p_strStorageID, string p_strMeDID, double TolNumber)
        {
            long lngRes = 0;
            string strSQL;
            string strTolNumber = "0";
            strSQL = @"select AMOUNT_DEC  FROM T_BSE_STORAGEMEDICINE WHERE STORAGEID_CHR='" + p_strStorageID + "' AND MEDICINEID_CHR='" + p_strMeDID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    strTolNumber = dtbResult.Rows[0]["AMOUNT_DEC"].ToString();
                }
            }
            catch
            {
            }
            if (strTolNumber != "0")
            {
                TolNumber += Convert.ToDouble(strTolNumber);
                //更新单据记录表
                strSQL = @"UPDATE T_BSE_STORAGEMEDICINE  SET AMOUNT_DEC=" + TolNumber +
                    " WHERE STORAGEID_CHR='" + p_strStorageID + "' and MEDICINEID_CHR='" + p_strMeDID + "'";
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
            }
            else
            {
                strSQL = @"insert into  T_BSE_STORAGEMEDICINE(STORAGEID_CHR,MEDICINEID_CHR,AMOUNT_DEC) values('" + p_strStorageID + "','" + p_strMeDID + "'," + TolNumber + ")";
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

            }

            return lngRes;

        }
        #endregion

        #region 审核入库单据，更改库存明细表(不用)
        /// <summary>
        /// 审核入库单据，更改库存明细表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="storageID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOrdToChangeDetail(string storageID, clsMedStorageOrdDe_VO p_objResultArr)
        {
            long lngRes = 0;
            string strSQL = @"select CURQTY_DEC from t_opr_storagemeddetail where STORAGEID_CHR='" + storageID + "' and MEDICINEID_CHR='" + p_objResultArr.m_strMEDICINEID_CHR + "' and SYSLOTNO_CHR='" + p_objResultArr.m_strSYSLOTNO_CHR + "'";
            DataTable dtRuslt = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtRuslt);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0 && dtRuslt.Rows.Count > 0)
            {
                int CurQty = Convert.ToInt32(dtRuslt.Rows[0]["CURQTY_DEC"].ToString()) + Convert.ToInt32(p_objResultArr.m_dblQTY_DEC);
                strSQL = @"update t_opr_storagemeddetail set CURQTY_DEC=" + CurQty.ToString() + " where STORAGEID_CHR='" + storageID + "' and MEDICINEID_CHR='" + p_objResultArr.m_strMEDICINEID_CHR + "' and SYSLOTNO_CHR='" + p_objResultArr.m_strSYSLOTNO_CHR + "'";
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
            }
            else
            {
                strSQL = @"insert into  t_opr_storagemeddetail(STORAGEID_CHR,MEDICINEID_CHR,SYSLOTNO_CHR,LOTNO_VCHR,PRODUCTORID_CHR,CURQTY_DEC,UNITID_CHR,USEFULSTATUS_INT,
                    USEFULLIFE_DAT,BUYUNITPRICE_MNY) values('" + storageID + "','" + p_objResultArr.m_strMEDICINEID_CHR + "','" + p_objResultArr.m_strSYSLOTNO_CHR + "','" +
                    p_objResultArr.m_strLOTNO_VCHR + "','" + p_objResultArr.m_strPRODCUTORID_CHR + "'," + p_objResultArr.m_dblQTY_DEC + ",'" + p_objResultArr.m_strUNITID_CHR + "',1,To_Date('" + p_objResultArr.m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss')," + p_objResultArr.m_dblBUYUNITPRICE_MNY + ")";
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
            }
            return lngRes;
        }
        #endregion

        #region 根据仓库查询库存明细表
        /// <summary>
        /// 根据仓库查询库存明细表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="storageID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeTail(string storageID, out DataTable p_objResultArr)
        {
            p_objResultArr = new DataTable();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT a.MEDICINEID_CHR,a.SYSLOTNO_CHR,a.LOTNO_VCHR,a.PRODUCTORID_CHR,a.CURQTY_DEC,a.UNITID_CHR,a.USEFULLIFE_DAT,a.BUYUNITPRICE_MNY,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,c.VENDORNAME_VCHR,b.ASSISTCODE_CHR
                             FROM T_OPR_STORAGEMEDDETAIL a ,t_bse_medicine b,t_bse_vendor c where a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.PRODUCTORID_CHR=c.VENDORID_CHR(+) and a.USEFULSTATUS_INT=1 and a.CURQTY_DEC>0 and a.STORAGEID_CHR='" + storageID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
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

        #region 获取某仓库的最大单据号
        /// <summary>
        /// 获取某仓库的最大单据号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMaxDoc"></param>
        /// <param name="strdate"></param>
        /// <param name="SIGN"></param>
        /// <param name="STORAGEID">仓库ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxDoc(out string p_strMaxDoc, string strdate, string SIGN, string STORAGEID)
        {
            p_strMaxDoc = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select max(DOCID_VCHR) as MaxDoc from t_opr_storageord where  SIGN_INT=" + SIGN + " and DOCID_VCHR like '" + strdate + "%' and STORAGEID_CHR='" + STORAGEID + "'";
            DataTable dtbResult = new DataTable();
            try
            {
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
                p_strMaxDoc = dtbResult.Rows[0]["MaxDoc"].ToString();
            }
            return lngRes;
        }
        #endregion

        #region 获得所有出库单数据
        /// <summary>
        /// 获得所有出库单数据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objResultArr">返回数据</param>
        /// <param name="priod">财务期ID</param>
        /// <param name="strordType">出入库类型ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageOrdOut(out clsMedStorageOrd_VO[] p_objResultArr, string priod, string strordType)
        {
            p_objResultArr = null;
            long lngRes = 0;
            string strSQL;
            string strWhere = "";
            if (priod != "")
            {
                strWhere = " and a.PERIODID_CHR='" + priod + "'";
            }
            strSQL = @"SELECT   a.*, b.storageordtypename_vchr,b.DEPTTYPE_INT, c.storagename_vchr,
         d.lastname_vchr AS offername, e.lastname_vchr AS creatname,
         f.lastname_vchr AS aduitname, j.vendorname_vchr, k.medstorename_vchr,h.tolmoney
    FROM t_opr_storageord a,
         t_aid_storageordtype b,
         t_bse_storage c,
         t_bse_employee d,
         t_bse_employee e,
         t_bse_employee f,
         t_bse_vendor j,
         t_bse_medstore k,
         (SELECT storageordid_chr, SUM (buyunitprice_mny * qty_dec) AS tolmoney
              FROM t_opr_storageordde where STORAGEORDTYPEID_CHR='" + strordType + @"'
          GROUP BY storageordid_chr) h
   WHERE a.storageordtypeid_chr = b.storageordtypeid_chr
     and a.STORAGEORDID_CHR=h.STORAGEORDID_CHR(+)
     and a.STORAGEORDTYPEID_CHR='" + strordType + @"' 
     AND a.storageid_chr = c.storageid_chr
     AND a.offerid_chr = d.empid_chr(+)
     AND a.creatorid_chr = e.empid_chr(+)
     AND a.aduitemp_chr = f.empid_chr(+)
     AND a.vendorid_chr = j.vendorid_chr(+)
     AND a.deptid_chr = k.medstoreid_chr(+)
     " + strWhere + @" 
ORDER BY TRIM (inord_dat) DESC ";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                p_objResultArr = new clsMedStorageOrd_VO[dtbResult.Rows.Count];
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedStorageOrd_VO();
                        p_objResultArr[i1].m_strSTORAGEORDTYPENAME_CHR = dtbResult.Rows[i1]["STORAGEORDTYPENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGENAME_CHR = dtbResult.Rows[i1]["STORAGEORDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_intDEPTTYPE_INT = int.Parse(dtbResult.Rows[i1]["DEPTTYPE_INT"].ToString());
                        p_objResultArr[i1].m_strCREATORNAME_CHR = dtbResult.Rows[i1]["CreatName"].ToString().Trim();
                        p_objResultArr[i1].m_strADUITEMPNAME_CHR = dtbResult.Rows[i1]["AduitNAME"].ToString().Trim();
                        p_objResultArr[i1].m_strVENDORNAME_CHR = dtbResult.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDID_CHR = dtbResult.Rows[i1]["STORAGEORDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDTYPEID_CHR = dtbResult.Rows[i1]["STORAGEORDTYPEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEID_CHR = dtbResult.Rows[i1]["STORAGEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPERIODID_CHR = dtbResult.Rows[i1]["PERIODID_CHR"].ToString().Trim();

                        p_objResultArr[i1].m_strINORD_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["INORD_DAT"]).ToString("yyyy-MM-dd").Trim();
                        if (dtbResult.Rows[i1]["tolmoney"].ToString() != "")
                            p_objResultArr[i1].m_dblTOLMNY_MNY = Convert.ToDouble(dtbResult.Rows[i1]["tolmoney"].ToString().Trim());
                        p_objResultArr[i1].m_intPSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                        if (dtbResult.Rows[i1]["ADUITDATE_DAT"].ToString() != "")
                        {
                            p_objResultArr[i1].m_strADUITDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ADUITDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        }
                        if (dtbResult.Rows[i1]["CREATEDATE_DAT"].ToString() != "")
                            p_objResultArr[i1].m_strCREATEDATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CREATEDATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();

                        p_objResultArr[i1].m_strDOCID_VCHR = dtbResult.Rows[i1]["DOCID_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPTID_CHR = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strDEPTNAME_CHR = dtbResult.Rows[i1]["MEDSTORENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strVENDORID_CHR = dtbResult.Rows[i1]["VENDORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strCREATORID_CHR = dtbResult.Rows[i1]["CREATORID_CHR"].ToString().Trim();

                        p_objResultArr[i1].m_strADUITEMP_CHR = dtbResult.Rows[i1]["ADUITEMP_CHR"].ToString().Trim();

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

        #region 对数据库的操作

        /// <summary>
        /// 保存出库单数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResult">输入单据数据</param>
        /// <param name="p_objResultDe">输入明细数据</param>
        /// <param name="newID">输出单号ID</param>
        /// <param name="isCheck">是否检查单据号是否存在，trun 检查，false-不检查</param>
        /// <param name="signint">1－入库 2－出库3-退库4-退库</param>
        /// <returns>返回-2表示单据号已经被占用</returns>
        [AutoComplete]
        public long m_lngInsertMetStorageOrdOut(clsMedStorageOrd_VO p_objResult, clsMedStorageOrdDe_VO[] p_objResultDe, out string newID, bool isCheck, int signint)
        {
            long lngRes = 0;
            newID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            #region 检查单据号是否己经被占用
            if (isCheck == true)
            {
                strSQL = @"select STORAGEORDID_CHR from t_opr_storageord where DOCID_VCHR='" + p_objResult.m_strDOCID_VCHR + "'";
                DataTable dt = new DataTable();
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
                if (dt.Rows.Count > 0)
                {
                    return -2;
                }
            }
            #endregion
            lngRes = objHRPSvc.m_lngGenerateNewID("t_opr_storageord", "STORAGEORDID_CHR", out newID);
            strSQL = @"insert into t_opr_storageord(STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,STORAGEID_CHR,PERIODID_CHR,INORD_DAT,TOLMNY_MNY,PSTATUS_INT,DOCID_VCHR,DEPTID_CHR,VENDORID_CHR,CREATORID_CHR,CREATEDATE_DAT,MEMO_VCHR,SIGN_INT) 
                            VALUES('" + newID + "','" + p_objResult.m_strSTORAGEORDTYPEID_CHR + "','" + p_objResult.m_strSTORAGEID_CHR + "','" + p_objResult.m_strPERIODID_CHR + "',To_Date('" + p_objResult.m_strINORD_DAT + "','yyyy-mm-dd hh24:mi:ss')" + "," + p_objResult.m_dblTOLMNY_MNY
                 + "," + p_objResult.m_intPSTATUS_INT + ",'" + p_objResult.m_strDOCID_VCHR + "','" + p_objResult.m_strDEPTID_CHR + "','" + p_objResult.m_strVENDORID_CHR + "','" + p_objResult.m_strCREATORID_CHR + "',To_Date('" + p_objResult.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                 + ",'" + p_objResult.m_strMEMO_VCHR + "'," + signint.ToString() + ")";
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
            string newDeid = "";
            for (int i1 = 0; i1 < p_objResultDe.Length; i1++)
            {
                objHRPSvc.m_lngGenerateNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", out newDeid);
                strSQL = @"INSERT INTO t_opr_storageordde (STORAGEORDDEID_CHR,STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,MEDICINEID_CHR,SYSLOTNO_CHR,ORD_DAT,
                           ROWNO_CHR,UNITID_CHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,SALEUNITPRICE_MNY,BUYUNITPRICE_MNY,
                           BUYTOLPRICE_MNY,INVOICENO_VCHR,DEPTID_CHR,LOTNO_VCHR,ORDERQTY_DEC,ORDERUNIT_VCHR,ORDERUNITPRICE_MNY,ORDERPKGQTY_DEC,AIMUNIT_INT,LIMITUNITPRICE_MNY,AIMUNITPRICE_MNY,SIGN_INT) 
                          VALUES('" + newDeid + "','" + newID + "','" + p_objResult.m_strSTORAGEORDTYPEID_CHR
                     + "','" + p_objResultDe[i1].m_strMEDICINEID_CHR + "','" + p_objResultDe[i1].m_strSYSLOTNO_CHR + "',To_Date('" + p_objResultDe[i1].m_strORD_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                     + ",'" + p_objResultDe[i1].m_strROWNO_CHR + "','" + p_objResultDe[i1].m_strUNITID_CHR + "',To_Date('" + p_objResultDe[i1].m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                     + ",'" + p_objResultDe[i1].m_strPRODCUTORID_CHR + "'," + p_objResultDe[i1].m_dblQTY_DEC + "," + p_objResultDe[i1].m_dblSALEUNITPRICE_MNY + "," + p_objResultDe[i1].m_dblBUYUNITPRICE_MNY + ","
                     + p_objResultDe[i1].m_dblBUYTOLPRICE_MNY + ",'" + p_objResultDe[i1].m_strINVOICENO_VCHR + "','" + p_objResultDe[i1].m_strDEPTID_CHR + "','" + p_objResultDe[i1].m_strLOTNO_VCHR + "'," + p_objResultDe[i1].m_strORDERQTY_DEC + ",'" + p_objResultDe[i1].m_strORDERUNIT_VCHR + "','" + p_objResultDe[i1].m_strORDERUNITPRICE_MNY + "','" + p_objResultDe[i1].m_strORDERPKGQTY_DEC + "','" + p_objResultDe[i1].m_intAIMUNIT_INT.ToString() + "','" + p_objResultDe[i1].m_strLIMITUNITPRICE_MNY + "','" + p_objResultDe[i1].m_strAIMUNITPRICE_MNY + "',2)";
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

        /// <summary>
        /// 插入出库单明细数据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objResult">输入数据</param>
        /// <param name="tolMoney">输单据总金额</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertMetStorageOrdDeOut(clsMedStorageOrdDe_VO p_objResult, out double tolMoney)
        {
            long lngRes = 0;
            tolMoney = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string newDeid;
            objHRPSvc.m_lngGenerateNewID("t_opr_storageordde", "STORAGEORDDEID_CHR", out newDeid);
            string strSQL = @"INSERT INTO t_opr_storageordde (STORAGEORDDEID_CHR,STORAGEORDID_CHR,STORAGEORDTYPEID_CHR,MEDICINEID_CHR,SYSLOTNO_CHR,ORD_DAT,
                           ROWNO_CHR,UNITID_CHR,LOTNO_VCHR,USEFULLIFE_DAT,PRODCUTORID_CHR,QTY_DEC,SALEUNITPRICE_MNY,
                           DISCOUNT_DEC,BUYTOLPRICE_MNY,INVOICENO_VCHR,DEPTID_CHR,ORDERQTY_DEC,ORDERUNIT_VCHR,ORDERUNITPRICE_MNY,ORDERPKGQTY_DEC,BUYUNITPRICE_MNY,AIMUNIT_INT,LIMITUNITPRICE_MNY,AIMUNITPRICE_MNY,SIGN_INT) 
                          VALUES('" + newDeid + "','" + p_objResult.m_strSTORAGEORDID_CHR + "','" + p_objResult.m_strSTORAGEORDTYPEID_CHR
                + "','" + p_objResult.m_strMEDICINEID_CHR + "','" + p_objResult.m_strSYSLOTNO_CHR + "',To_Date('" + p_objResult.m_strORD_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                + ",'" + p_objResult.m_strROWNO_CHR + "','" + p_objResult.m_strUNITID_CHR + "','" + p_objResult.m_strLOTNO_VCHR + "',To_Date('" + p_objResult.m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss')"
                + ",'" + p_objResult.m_strPRODCUTORID_CHR + "'," + p_objResult.m_dblQTY_DEC + "," + p_objResult.m_dblSALEUNITPRICE_MNY + ","
                + p_objResult.m_fltDISCOUNT_DEC + "," + p_objResult.m_dblBUYTOLPRICE_MNY + ",'" + p_objResult.m_strINVOICENO_VCHR + "','" + p_objResult.m_strDEPTID_CHR + "'," + p_objResult.m_strORDERQTY_DEC + ",'" + p_objResult.m_strORDERUNIT_VCHR + "'," + p_objResult.m_strORDERUNITPRICE_MNY + "," + p_objResult.m_strORDERPKGQTY_DEC + "," + p_objResult.m_dblBUYUNITPRICE_MNY + "," + p_objResult.m_intAIMUNIT_INT.ToString() + "," + p_objResult.m_strLIMITUNITPRICE_MNY + "," + p_objResult.m_strAIMUNITPRICE_MNY + ",2)";
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

        #region 根据单号获得出库明细数据
        /// <summary>
        /// 根据单号获得出库明细数据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="StorageID">单号</param>
        /// <param name="p_objResultArr">返回值</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStorageOrdDeOut(string StorageID, out clsMedStorageOrdDe_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedStorageOrdDe_VO[0];
            long lngRes = 0;
            string strSQL = @"SELECT a.STORAGEORDDEID_CHR,a.STORAGEORDID_CHR,a.STORAGEORDTYPEID_CHR,a.MEDICINEID_CHR,a.SYSLOTNO_CHR,a.ORD_DAT,a.ROWNO_CHR,a.UNITID_CHR,
                           a.USEFULLIFE_DAT,a.WHOLESALEUNITPRICE_MNY,a.PRODCUTORID_CHR,a.QTY_DEC,a.SALEUNITPRICE_MNY,a.LOTNO_VCHR,a.DISCOUNT_DEC,a.BUYUNITPRICE_MNY,a.BUYTOLPRICE_MNY,a.INVOICENO_VCHR,a.DEPTID_CHR,a.ORDERQTY_DEC,a.ORDERUNIT_VCHR,a.ORDERUNITPRICE_MNY,a.ORDERPKGQTY_DEC,a.AIMUNIT_INT,a.LIMITUNITPRICE_MNY,a.AIMUNITPRICE_MNY,
                            b.MEDICINENAME_VCHR,b.ASSISTCODE_CHR,b.MEDSPEC_VCHR,d.CURQTY_DEC  FROM T_OPR_STORAGEORDDE a,t_bse_medicine b,t_opr_storagemeddetail d
                           WHERE a.MEDICINEID_CHR=b.MEDICINEID_CHR  and d.FLAG_INT=0 and  a.SYSLOTNO_CHR=d.SYSLOTNO_CHR and a.MEDICINEID_CHR=d.MEDICINEID_CHR  and a.STORAGEORDID_CHR ='" + StorageID + "' order by a.ROWNO_CHR";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedStorageOrdDe_VO[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedStorageOrdDe_VO();
                        try
                        {
                            p_objResultArr[i1].m_dblWHOLESALEUNITPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["WHOLESALEUNITPRICE_MNY"].ToString());
                        }
                        catch
                        {
                            p_objResultArr[i1].m_dblWHOLESALEUNITPRICE_MNY = 0;
                        }
                        try
                        {
                            p_objResultArr[i1].m_dblBUYUNITPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["BUYUNITPRICE_MNY"].ToString().Trim());
                        }
                        catch
                        {
                            p_objResultArr[i1].m_dblBUYUNITPRICE_MNY = 0;
                        }
                        p_objResultArr[i1].m_strORDERPKGQTY_DEC = dtbResult.Rows[i1]["ORDERPKGQTY_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERQTY_DEC = dtbResult.Rows[i1]["ORDERQTY_DEC"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERUNIT_VCHR = dtbResult.Rows[i1]["ORDERUNIT_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strORDERUNITPRICE_MNY = dtbResult.Rows[i1]["ORDERUNITPRICE_MNY"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDICINENAME_CHR = dtbResult.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strASSISTCODE_CHR = dtbResult.Rows[i1]["ASSISTCODE_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDID_CHR = dtbResult.Rows[i1]["STORAGEORDID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strPRODCUTORNAME_CHR = dtbResult.Rows[i1]["PRODCUTORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strspec = dtbResult.Rows[i1]["MEDSPEC_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_strSTORAGEORDDEID_CHR = dtbResult.Rows[i1]["STORAGEORDDEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strMEDICINEID_CHR = dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strLOTNO_VCHR = dtbResult.Rows[i1]["LOTNO_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_intStorage = Convert.ToDouble(dtbResult.Rows[i1]["CURQTY_DEC"].ToString().Trim());
                        p_objResultArr[i1].m_strSYSLOTNO_CHR = dtbResult.Rows[i1]["SYSLOTNO_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["ORD_DAT"].ToString() != "")
                            p_objResultArr[i1].m_strORD_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ORD_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strROWNO_CHR = dtbResult.Rows[i1]["ROWNO_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strUNITID_CHR = dtbResult.Rows[i1]["UNITID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["USEFULLIFE_DAT"].ToString() != "")
                            p_objResultArr[i1].m_strUSEFULLIFE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["USEFULLIFE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        p_objResultArr[i1].m_strPRODCUTORID_CHR = dtbResult.Rows[i1]["PRODCUTORID_CHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["QTY_DEC"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblQTY_DEC = Convert.ToDouble(dtbResult.Rows[i1]["QTY_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["SALEUNITPRICE_MNY"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblSALEUNITPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["SALEUNITPRICE_MNY"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString() != "")
                        {
                            p_objResultArr[i1].m_fltDISCOUNT_DEC = float.Parse(dtbResult.Rows[i1]["DISCOUNT_DEC"].ToString().Trim());
                        }
                        if (dtbResult.Rows[i1]["BUYTOLPRICE_MNY"].ToString() != "")
                        {
                            p_objResultArr[i1].m_dblBUYTOLPRICE_MNY = Convert.ToDouble(dtbResult.Rows[i1]["BUYTOLPRICE_MNY"].ToString().Trim());
                        }
                        p_objResultArr[i1].m_strINVOICENO_VCHR = dtbResult.Rows[i1]["INVOICENO_VCHR"].ToString().Trim();
                        if (dtbResult.Rows[i1]["AIMUNIT_INT"].ToString() != "")
                            p_objResultArr[i1].m_intAIMUNIT_INT = int.Parse(dtbResult.Rows[i1]["AIMUNIT_INT"].ToString().Trim());
                        p_objResultArr[i1].m_strAIMUNITPRICE_MNY = dtbResult.Rows[i1]["AIMUNITPRICE_MNY"].ToString().Trim();
                        p_objResultArr[i1].m_strLIMITUNITPRICE_MNY = dtbResult.Rows[i1]["LIMITUNITPRICE_MNY"].ToString().Trim();
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

        #region 修改明细表
        /// <summary>
        /// 修改明细表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrdDe"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateOutOrdDe(clsMedStorageOrdDe_VO p_objOrdDe, string strTotailMoney, string strOrdID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"UPDATE t_opr_StorageOrdDe SET MEDICINEID_CHR='" + p_objOrdDe.m_strMEDICINEID_CHR +
                "',UNITID_CHR='" + p_objOrdDe.m_strUNITID_CHR + "',UsefulLife_dat=To_date('" + p_objOrdDe.m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss')" +
                ",PRODCUTORID_CHR='" + p_objOrdDe.m_strPRODCUTORID_CHR + "',QTY_DEC=" + p_objOrdDe.m_dblQTY_DEC + ",BUYUNITPRICE_MNY=" + p_objOrdDe.m_dblBUYUNITPRICE_MNY +
                 ",BUYTOLPRICE_MNY = " + p_objOrdDe.m_dblBUYTOLPRICE_MNY + ",ORDERQTY_DEC=" + p_objOrdDe.m_strORDERQTY_DEC + ",ORDERUNIT_VCHR='" + p_objOrdDe.m_strORDERUNIT_VCHR + "',ORDERUNITPRICE_MNY=" + p_objOrdDe.m_strORDERUNITPRICE_MNY + ",ORDERPKGQTY_DEC=" + p_objOrdDe.m_strORDERPKGQTY_DEC + ",SYSLOTNO_CHR='" + p_objOrdDe.m_strSYSLOTNO_CHR + "',LOTNO_VCHR='" + p_objOrdDe.m_strLOTNO_VCHR + "',AIMUNIT_INT='" + p_objOrdDe.m_intAIMUNIT_INT.ToString() + "',LIMITUNITPRICE_MNY='" + p_objOrdDe.m_strLIMITUNITPRICE_MNY + "',AIMUNITPRICE_MNY='" + p_objOrdDe.m_strAIMUNITPRICE_MNY + "' ,WHOLESALEUNITPRICE_MNY=" + p_objOrdDe.m_dblWHOLESALEUNITPRICE_MNY + "  where STORAGEORDDEID_CHR='" + p_objOrdDe.m_strSTORAGEORDDEID_CHR + "'";
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

        #region 修改出库单表
        /// <summary>
        /// 修改出库单表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOrd"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateOutOrd(clsMedStorageOrd_VO p_objOrd)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSQL = @"UPDATE t_opr_StorageOrd SET STORAGEORDTYPEID_CHR='" + p_objOrd.m_strSTORAGEORDTYPEID_CHR +
                "',STORAGEID_CHR='" + p_objOrd.m_strSTORAGEID_CHR +
                "',TOLMNY_MNY=" + p_objOrd.m_dblTOLMNY_MNY + ",DEPTID_CHR='" + p_objOrd.m_strDEPTID_CHR +
                "',VENDORID_CHR='" + p_objOrd.m_strVENDORID_CHR + "',CREATEDATE_DAT=To_Date('" + p_objOrd.m_strCREATEDATE_DAT + "','yyyy-mm-dd hh24:mi:ss')" +
                ",MEMO_VCHR='" + p_objOrd.m_strMEMO_VCHR + "' where STORAGEORDID_CHR='" + p_objOrd.m_strSTORAGEORDID_CHR + "'";
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

        #region 审核出库单据
        /// <summary>
        ///审核出库单据 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objResultDeArr"></param>
        /// <param name="flag">true,自动生成药房入库数据</param>
        /// <param name="status">是否充许负库存0-不充许，1-允许</param>
        /// <returns>1,审核成功;-1,失败;-2,还没有设置该药品的包装量;-3,数据类型不正确;-4,药房还没有设置相应的入库单</returns>
        [AutoComplete]
        public long m_lngAduitOrdOut(clsMedStorageOrd_VO p_objResultArr, clsMedStorageOrdDe_VO[] p_objResultDeArr, bool flag, int status)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";
            if (flag == true)
            {
                #region 把药库出库的数据复制到药房出入库单表

                strSQL = @"select MEDSTOREORDTYPEID_CHR from t_aid_medstoreordtype where STORAGESIGN_INT=1 and SIGN_INT=1";
                DataTable dt = new DataTable();
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
                if (dt.Rows.Count == 0)
                {
                    return -4;
                }
                string newid = "";

                objHRPSvc.m_lngGenerateNewID("t_opr_medstoreord", "MEDSTOREORDID_CHR", out newid);
                strSQL = @"insert into t_opr_medstoreord(MEDSTOREORDID_CHR,MEDSTOREID_CHR,ORDDATE_DAT,TOLMNY_MNY,MEMO_VCHR,CREATOR_CHR," +
                    "CREATEDATE_DAT,MEDSTOREORDTYPEID_CHR,PSTATUS_INT,OUTFLAN_INT,PERIODID_CHR,SRCID_CHR,SRCTYPE_INT,STORERDOCNO_CHR) values('" + newid + "','"
                    + p_objResultArr.m_strDEPTID_CHR + "',sysdate," + p_objResultArr.m_dblTOLMNY_MNY + ",'药库自动生成的药房入库单','"
                    + p_objResultArr.m_strCREATORID_CHR + "',sysdate,'"
                    + dt.Rows[0]["MEDSTOREORDTYPEID_CHR"].ToString() + "',1,1,'" + p_objResultArr.m_strPERIODID_CHR + "','" + p_objResultArr.m_strSTORAGEID_CHR + "',3,'" + p_objResultArr.m_strDOCID_VCHR + "')";
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
                if (lngRes > 0 && p_objResultDeArr.Length > 0)
                {
                    string newDeid = "";
                    for (int i1 = 0; i1 < p_objResultDeArr.Length; i1++)
                    {
                        DataTable dtResult = new DataTable();
                        strSQL = @"select IPUNIT_CHR,OPUNIT_CHR,PACKQTY_DEC,OPCHARGEFLG_INT  from t_bse_medicine where MEDICINEID_CHR='" + p_objResultDeArr[i1].m_strMEDICINEID_CHR + "'";
                        try
                        {
                            lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtResult);
                        }
                        catch (Exception objEx)
                        {
                            string strTmp = objEx.Message;
                            com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }
                        int Row = i1 + 1;
                        double tolnuber = 0;
                        double saleunprice = 0;
                        string unit = "";
                        if (dtResult.Rows[0]["OPCHARGEFLG_INT"].ToString() == "1")
                        {
                            if (dtResult.Rows[0]["PACKQTY_DEC"].ToString() != "" && dtResult.Rows[0]["PACKQTY_DEC"].ToString() != "0")
                            {
                                try
                                {
                                    tolnuber = p_objResultDeArr[i1].m_dblQTY_DEC * Convert.ToInt32(dtResult.Rows[0]["PACKQTY_DEC"].ToString());
                                    saleunprice = p_objResultDeArr[i1].m_dblSALEUNITPRICE_MNY / Convert.ToInt32(dtResult.Rows[0]["PACKQTY_DEC"].ToString());
                                    unit = dtResult.Rows[0]["IPUNIT_CHR"].ToString();
                                }
                                catch (Exception objEx)
                                {
                                    System.EnterpriseServices.ContextUtil.SetAbort();
                                    lngRes = -3;
                                    string strTmp = objEx.Message;
                                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                    bool blnRes = objLogger.LogError(objEx);
                                }
                            }
                            else
                            {
                                System.EnterpriseServices.ContextUtil.SetAbort();
                                return -2;
                            }
                        }
                        else
                        {
                            tolnuber = p_objResultDeArr[i1].m_dblQTY_DEC;
                            saleunprice = p_objResultDeArr[i1].m_dblSALEUNITPRICE_MNY;
                            unit = dtResult.Rows[0]["OPUNIT_CHR"].ToString();
                        }

                        objHRPSvc.m_lngGenerateNewID("t_opr_medstoreordde", "MEDSTOREORDDEID_CHR", out newDeid);
                        strSQL = @"insert into t_opr_medstoreordde(MEDSTOREORDDEID_CHR,MEDSTOREORDID_CHR,MEDICINEID_CHR,ROWNO_CHR,QTY_DEC,SALEUNITPRICE_DEC,SALETOLPRICE_DEC,UNITID_CHR,USEFULLIFE_DAT,MEDNO_CHR,SYSLOTNO_CHR)"
                            + " values('" + newDeid + "','" + newid + "','" + p_objResultDeArr[i1].m_strMEDICINEID_CHR + "','"
                            + p_objResultDeArr[i1].m_strROWNO_CHR + "'," + tolnuber.ToString() + "," + saleunprice.ToString() + ","
                            + p_objResultDeArr[i1].m_dbltolmony.ToString() + ",'" + unit + "',To_Date('" + p_objResultDeArr[i1].m_strUSEFULLIFE_DAT + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objResultDeArr[i1].m_strLOTNO_VCHR + "','" + p_objResultDeArr[i1].m_strSYSLOTNO_CHR + "')";
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
                #endregion

            }
            com.digitalwave.iCare.middletier.HIS.clsMedStorageManage mange = new clsMedStorageManage();
            for (int i1 = 0; i1 < p_objResultDeArr.Length; i1++)
            {
                mange.m_lngReduceMedQty(null, p_objResultDeArr[i1].m_strMEDICINEID_CHR, p_objResultDeArr[i1].m_strLOTNO_VCHR, p_objResultDeArr[i1].m_strSYSLOTNO_CHR,
                    p_objResultArr.m_strSTORAGEID_CHR, p_objResultDeArr[i1].m_dblQTY_DEC, p_objResultDeArr[i1].m_strUNITID_CHR, status);
            }
            #region 把出库单标志为己审核
            strSQL = @"update t_opr_storageord set ADUITEMP_CHR='" + p_objResultArr.m_strADUITEMP_CHR + "',ADUITDATE_DAT=To_Date('" + p_objResultArr.m_strADUITDATE_DAT + "','yyyy-mm-dd hh24:mi:ss'),PSTATUS_INT=2  where STORAGEORDID_CHR='" + p_objResultArr.m_strSTORAGEORDID_CHR + "'";
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
            if (lngRes <= 0)
                System.EnterpriseServices.ContextUtil.SetAbort();
            #endregion
            return lngRes;
        }

        #endregion

        #region 审核出库单据，更改库存
        /// <summary>
        ///审核出库单据，更改库存 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID"></param>
        /// <param name="p_strMeDID"></param>
        /// <param name="TolNumber"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOrdToChangeStorageOut(string p_strStorageID, string p_strMeDID, double TolNumber)
        {
            long lngRes = 0;
            string strSQL;
            string strTolNumber = "0";
            strSQL = @"select AMOUNT_DEC  FROM T_BSE_STORAGEMEDICINE WHERE STORAGEID_CHR='" + p_strStorageID + "' AND MEDICINEID_CHR='" + p_strMeDID + "'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    strTolNumber = dtbResult.Rows[0]["AMOUNT_DEC"].ToString();
                }
            }
            catch
            {
            }
            TolNumber -= Convert.ToDouble(strTolNumber);
            //更新单据记录表
            strSQL = @"UPDATE T_BSE_STORAGEMEDICINE  SET AMOUNT_DEC=" + TolNumber +
                " WHERE STORAGEID_CHR='" + p_strStorageID + "' and MEDICINEID_CHR='" + p_strMeDID + "'";
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

        #region 审核出库单据，更改库存明细表
        /// <summary>
        ///审核出库单据，更改库存明细表 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="SysNO"></param>
        /// <param name="OutNumber"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngOrdToChangeDetailOut(string SysNO, double OutNumber)
        {
            long lngRes = 0;
            string strSQL;
            strSQL = @"select CURQTY_DEC from t_opr_storagemeddetail where syslotno_chr='" + SysNO + "'";
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
            if (lngRes > 0)
            {
                double NowNumber = Convert.ToDouble(dtbResult.Rows[0]["CURQTY_DEC"]) - OutNumber;
                strSQL = @"update t_opr_storagemeddetail set CURQTY_DEC=" + NowNumber + "  where syslotno_chr='" + SysNO + "'";
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

            }
            return lngRes;
        }
        #endregion

        #region 根据入库ID查找数据
        /// <summary>
        /// 根据入库ID查找数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="ordTypeID">单据类型ID,如果为""则以标专状态来查找相应的单据信息</param>
        /// <param name="oreType">返回单据信息</param>
        /// <param name="status_int">药房与药库数据同步标志0-药房申请药品使用单据，1-是药房退药使用单据，-1-不是同步单据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindOrdTypeNameByID(string ordTypeID, out clsStorageOrdType_VO oreType, string status_int)
        {
            oreType = new clsStorageOrdType_VO();
            long lngRes = 0;
            string strSQL = "";
            if (ordTypeID != "")
            {
                strSQL = @"select * from  t_aid_storageordtype  where STORAGEORDTYPEID_CHR='" + ordTypeID + "'";
            }
            else
            {
                strSQL = @"select * from  t_aid_storageordtype  where MEDSTORAGE_INT=" + status_int;
            }
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
                oreType.m_strStorageOrdTypeID = dtbResult.Rows[0]["STORAGEORDTYPEID_CHR"].ToString().Trim();
                oreType.m_strBEGINSTR_CHR = dtbResult.Rows[0]["BEGINSTR_CHR"].ToString().Trim();
                oreType.m_strStorageOrdTypeName = dtbResult.Rows[0]["STORAGEORDTYPENAME_VCHR"].ToString().Trim();
                oreType.m_strBEGINSTR_CHR = dtbResult.Rows[0]["BEGINSTR_CHR"].ToString().Trim();
                oreType.m_intSign = int.Parse(dtbResult.Rows[0]["SIGN_INT"].ToString().Trim());
                oreType.m_intDeptType = Convert.ToInt16(dtbResult.Rows[0]["DEPTTYPE_INT"].ToString().Trim());
            }
            return lngRes;
        }
        #endregion

        #region 判定财务期是否已经被盘点并审核
        /// <summary>
        /// 判定财务期是否已经被盘点并审核
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strPeriod">财务期</param>
        /// <param name="IsAppl">true-已经被盘点并审核</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngEstimatePeriod(string strPeriod, out bool IsAppl)
        {
            IsAppl = false;
            long lngRes = 0;
            string strSQL = @"select STORAGECHECKID_CHR from t_opr_storagecheck where PERIODID_CHR>='" + strPeriod + "' and PSTATUS_INT=1 and FLAG_INT=0";
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
                IsAppl = true;
            }
            return lngRes;
        }
        #endregion

        #endregion

    }
}
