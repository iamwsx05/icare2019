using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
	/// <summary>
	/// 药品总库存的服务层
	/// </summary>
	/// 

	[Transaction(TransactionOption.Required)]
	[ObjectPooling(Enabled=true)]
	public class clsStorageMedicineSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase  //MiddleTierBase
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public clsStorageMedicineSvc()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 获取仓库记录
		/// <summary>
		/// 获取仓库记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllStorage( out System.Data.DataTable dt)
		{
			dt = null;
			long lngRes = 0; 

			string strSQL = @"SELECT  storagename_vchr,storageid_chr,STORAGEGROSSPROFIT_DEC 
							    FROM T_BSE_STORAGE";		

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dt);							
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;			
		}
		#endregion	获取仓库记录

		#region 增加在药品总库存表中增加记录
		/// <summary>
		/// 在药品总库存表中增加记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objMedStorage"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewMedicineStorage( clsStorageMedicine_VO p_objMedStorage)
		{
			long lngRes = 0; 

			string strSQL = @"INSERT INTO t_bse_storagemedicine(storageid_chr,medicineid_chr,amount_dec) VALUES('"+
				p_objMedStorage.m_objStorage.m_strStroageID + "','" + p_objMedStorage.m_objMedicine.m_strMedicineID + "'," + 
				p_objMedStorage.m_fltAmount + ")";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}

			return lngRes;			
		}
		#endregion

		#region 更改药品总库存表的记录
		/// <summary>
		/// 更改在药品总库存表的记录
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objMedStorage"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateMedicineStorage( clsStorageMedicine_VO p_objMedStorage)
		{
			long lngRes = 0; 

			string strSQL = @"UPDATE t_bse_storagemedicine
							 SET amount_dec = " + p_objMedStorage.m_fltAmount + 
							" WHERE medicineid_chr = '" + p_objMedStorage.m_objMedicine.m_strMedicineID + "'";
			
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}

			return lngRes;			
		}
		#endregion

		#region 检查药品是否已经在库存中有记录　欧阳孔伟　2004-05-25
		/// <summary>
		/// 检查药品是否已经存在库存表中，返回值1为不存在，-1为存在。
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strStorage"></param>
		/// <param name="p_strMedID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckMedIsInSto( string p_strStorage,string p_strMedID)
		{
			long lngRes =0; string strMedTmp = "";

			System.Data.DataTable dtbResult = new System.Data.DataTable();
 

			string strSQL = @"SELECT medicineid_chr
							  FROM t_bse_storagemedicine
							  WHERE medicineid_chr = '" + p_strMedID + "' and storageid_chr = '" + p_strStorage + "'";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
				

				if (lngRes >0 && dtbResult != null)
				{
					strMedTmp = dtbResult.Rows[0]["medicineid_chr"].ToString();
				}
				if (strMedTmp != "")
				{
					return -1;
				}


			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}
			return lngRes;

		}
		#endregion

		#region 检查库存　欧阳孔伟　2004-05-14
		/// <summary>
		/// 检查库存
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strMedicineID"></param>
		/// <param name="p_fltMedStorage"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngCheckMdStorage( string p_strMedicineID,out float p_fltMedStorage)
		{
			long lngRes = 0;

			System.Data.DataTable dtbResult = new System.Data.DataTable(); 
			p_fltMedStorage = 0;
			 

			string strSQL = @"SELECT sum(amount_dec) as amount
							  FROM t_bse_storagemedicine
							  WHERE medicineid_chr = '" + p_strMedicineID + "'";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
				

				if (lngRes >0 && dtbResult != null)
				{
					if(dtbResult.Rows.Count > 0)
					{
						string strAmount = null;
						strAmount = dtbResult.Rows[0]["amount"].ToString();
						if(strAmount == "")
						{
							strAmount = "0";
						}
						p_fltMedStorage = float.Parse(strAmount);
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}
			return lngRes;
		}
		#endregion

		#region 将DataTable数据传递到VO上　欧阳孔伟　2004-06-17
		private void CopyDataTableToVO(System.Data.DataTable dtbSource,out clsStorageMedicine_VO[] objItem)
		{
			objItem = new clsStorageMedicine_VO[0];

			try
			{
				if(dtbSource != null)
				{
					int intRow = dtbSource.Rows.Count;
					if(intRow>0)
					{
						objItem = new clsStorageMedicine_VO[0];
						for(int i=0;i<intRow;i++)
						{
							objItem[i] = new clsStorageMedicine_VO();
							objItem[i].m_objMedicine = new clsMedicine_VO();
							objItem[i].m_objStorage = new clsStorage_VO();
							objItem[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["storageid_chr"].ToString().Trim();
							objItem[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["storagename_vchr"].ToString().Trim();
							objItem[i].m_objMedicine.m_strMedicineID = dtbSource.Rows[i]["medicineid_chr"].ToString().Trim();
							objItem[i].m_objMedicine.m_strMedicineName = dtbSource.Rows[i]["medicinename_vchr"].ToString().Trim();
							objItem[i].m_objMedicine.m_strMedSpec = dtbSource.Rows[i]["medspec_vchr"].ToString().Trim();
							objItem[i].m_objMedicine.m_strMedicineEngName = dtbSource.Rows[i]["medicineengname_vchr"].ToString().Trim();
							objItem[i].m_objMedicine.m_strPYCode = dtbSource.Rows[i]["pycode_chr"].ToString().Trim();
							objItem[i].m_objMedicine.m_strWBCode = dtbSource.Rows[i]["wbcode_chr"].ToString().Trim();

							if(dtbSource.Rows[i]["amount_dec"].ToString().Trim() != null)
							{
								objItem[i].m_fltAmount = float.Parse(dtbSource.Rows[i]["amount_dec"].ToString().Trim());
							}
						}
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}

		}
		#endregion

		#region 根据库房ID查找该库房中所有药品的库存　欧阳孔伟　2004-05-25
		/// <summary>
		/// 根据库房ID查找该库房下所有药品的库存
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strStorageID"></param>
		/// <param name="p_objResult"></param>
		/// <returns></returns>
		public long m_lngFindMedStoByStoID( string p_strStorageID,out clsStorageMedicine_VO[] p_objResult)
		{
			long lngRes = 0; 
			p_objResult = new clsStorageMedicine_VO[0];
			 

			string strSQL = @"SELECT a.*, b.storagename_vchr, c.medicinename_vchr, c.medspec_vchr,
									 c.medicineengname_vchr, c.pycode_chr, c.wbcode_chr
								FROM t_bse_storagemedicine a, t_bse_storage b, t_bse_medicine c
							   WHERE a.storageid_chr = b.storageid_chr AND a.medicineid_chr = c.medicineid_chr(+)
								 AND a.storageid_chr = '" + p_strStorageID + "'";
			System.Data.DataTable dtbResult = new System.Data.DataTable();
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
				

				if(lngRes>0 && dtbResult != null)
				{
					CopyDataTableToVO(dtbResult,out p_objResult);
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}
			return lngRes;
		}
		#endregion

		#region 获取药品库存
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strStorageID"></param>
		/// <param name="p_strStorageFlag"></param>
		/// <param name="p_bZero">是零库存也显示</param>
		/// <param name="p_outDataset"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedStock( string p_strStorageID,string p_strStorageFlag,bool p_bZero,out System.Data.DataSet p_outDataset)
		{
			long lngRes = 0;
			p_outDataset = new System.Data.DataSet();
			if(p_strStorageFlag == null || p_strStorageFlag == "")
			{
				p_strStorageFlag = "0";
			} 

			string strSQL = @"select a.medicineid_chr,b.assistcode_chr,b.medicinename_vchr,b.PYCODE_CHR,b.WBCODE_CHR,b.medspec_vchr, a.unitid_chr,
							sum(a.curqty_dec) as curqt, 
							sum(a.curqty_dec* a.buyunitprice_mny) as buymny,
							sum(a.curqty_dec*a.saleunitprice_mny) as salemny,
							sum(a.curqty_dec*a.SALEUNITPRICE_MNY) as wholemny,e.VENDORNAME_VCHR
							from t_opr_storagemeddetail a,t_bse_medicine b,t_opr_storageordde c,t_opr_storageord d,T_BSE_VENDOR e
							where a.FLAG_INT=" + p_strStorageFlag + " and a.MEDICINEID_CHR=c.MEDICINEID_CHR and a.SYSLOTNO_CHR=c.SYSLOTNO_CHR and c.SIGN_INT=1 and d.PSTATUS_INT=2 and  c.STORAGEORDID_CHR=d.STORAGEORDID_CHR and d.VENDORID_CHR=e.VENDORID_CHR";
			if( !p_bZero)
			{
				strSQL +=" and a.curqty_dec > 0 ";
			}
			strSQL +=" and a.storageid_chr = '"+p_strStorageID+@"'
						and a.medicineid_chr = b.medicineid_chr
						group by a.storageid_chr,a.medicineid_chr,b.medicinename_vchr,b.medspec_vchr,b.PYCODE_CHR,b.WBCODE_CHR,b.assistcode_chr,a.unitid_chr,e.VENDORNAME_VCHR order by b.assistcode_chr";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
				System.Data.DataTable dtMedStock = new System.Data.DataTable();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtMedStock);	
				dtMedStock.TableName = "MedStock";
				p_outDataset.Tables.Add(dtMedStock);

				strSQL = @"select a.medicineid_chr,b.assistcode_chr,b.medicinename_vchr,
						b.medspec_vchr,a.usefullife_dat,a.syslotno_chr,a.lotno_vchr,a.unitid_chr,a.curqty_dec, 
						a.buyunitprice_mny,
						b.UNITPRICE_MNY as saleunitprice_mny,
						b.TRADEPRICE_MNY as wholesaleunitprice_mny,e.VENDORNAME_VCHR
						from t_opr_storagemeddetail a,t_bse_medicine b,t_opr_storageordde c,t_opr_storageord d,T_BSE_VENDOR e
						where a.medicineid_chr = b.medicineid_chr
						and a.storageid_chr = '" + p_strStorageID+@"' 
						and a.FLAG_INT = "+p_strStorageFlag+@"
						and a.medicineid_chr = b.medicineid_chr and a.MEDICINEID_CHR=c.MEDICINEID_CHR and a.SYSLOTNO_CHR=c.SYSLOTNO_CHR and c.STORAGEORDID_CHR=d.STORAGEORDID_CHR and d.VENDORID_CHR=e.VENDORID_CHR  order by a.syslotno_chr";
				System.Data.DataTable dtMedStockDetail = new System.Data.DataTable();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtMedStockDetail);	
				dtMedStockDetail.TableName = "MedStockDetail";
				p_outDataset.Tables.Add(dtMedStockDetail);
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx); 
			}
			return lngRes;
		}
		#endregion 


        #region 查询药品的出入库记录
        [AutoComplete]
        public long m_lngOutInReMark( string MedID,System.Collections.Generic.List<string> List, string strStatus, out System.Data.DataTable dtbResult,bool isMed)
        {
            long lngRes = 0;
            dtbResult = new System.Data.DataTable(); 

            string strSQL = @"";
            string strWhere = "";
            if (strStatus!="0")
            {
                strWhere = " and a.SIGN_INT=" + strStatus;
            }
            if (List.Count > 0)
            {
                for (int i1 = 0; i1 < List.Count; i1++)
                {
                    if (i1 == 0)
                    {
                        strWhere += @" and (PERIODID_CHR='" + (string)List[i1] + "'";
                    }
                    else
                    {
                        strWhere += @" or PERIODID_CHR='" + (string)List[i1] + "'";
                    }
                }
                strWhere += @" )";
            }
            if (isMed)
            {
                strSQL = @"SELECT   b.medicinename_vchr, b.medspec_vchr,
         a.unitid_chr, a.buyunitprice_mny, a.unitid_chr, a.syslotno_chr,
         d.inord_dat, a.lotno_vchr, a.qty_dec, a.buyunitprice_mny,a.qty_dec*a.buyunitprice_mny as money,a.qty_dec*a.saleunitprice_mny as salemoney,
         a.saleunitprice_mny, a.aimunit_int, e.vendorname_vchr||f.MEDSTORENAME_VCHR as vendorname_vchr, d.docid_vchr,
         d.aduitdate_dat,a.SIGN_INT
    FROM t_opr_storageordde a,
         t_bse_medicine b,
         t_opr_storageord d,
         t_bse_vendor e,
         t_bse_medstore f
   WHERE a.medicineid_chr = b.medicineid_chr
     AND a.storageordid_chr = d.storageordid_chr
     AND d.pstatus_int = 2
     AND d.vendorid_chr = e.vendorid_chr(+) and d.DEPTID_CHR=f.MEDSTOREID_CHR(+)  and a.medicineid_chr='" + MedID + "'" + strWhere + @"  ORDER BY a.ord_dat";
            }
            else
            {
                strSQL = @"SELECT   b.medicinename_vchr, b.medspec_vchr,
         d.unitid_chr, d.buyunitprice_mny, d.unitid_chr, d.syslotno_chr,
         a.inord_dat, d.lotno_vchr, d.qty_dec, d.buyunitprice_mny,d.qty_dec*d.buyunitprice_mny as money,d.qty_dec*d.saleunitprice_mny as salemoney,
         d.saleunitprice_mny, d.aimunit_int, e.vendorname_vchr, a.docid_vchr,
         a.aduitdate_dat,d.SIGN_INT
    FROM t_opr_storageord a,
         t_opr_storageordde d,
         t_bse_medicine b,
         t_bse_vendor e
   WHERE a.storageordid_chr=d.storageordid_chr
     AND d.medicineid_chr = b.medicineid_chr
     AND a.pstatus_int = 2
     AND a.VENDORID_CHR='" + MedID + "' and a.vendorid_chr = e.vendorid_chr " + strWhere + @"  ORDER BY a.INORD_DAT";
            }
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

        #region 获取供应商信息
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtvendor"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllVendor( out System.Data.DataTable dtvendor)
        {
            long lngRes = 0;
            dtvendor = new System.Data.DataTable(); 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select USERCODE_CHR,VENDORNAME_VCHR,VENDORID_CHR,PYCODE_CHR,WBCODE_CHR from t_bse_vendor where VENDORTYPE_INT<>2 and PRODUCTTYPE_INT=1 order by USERCODE_CHR";
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

            return lngRes;
        }

        #endregion
	}
}
