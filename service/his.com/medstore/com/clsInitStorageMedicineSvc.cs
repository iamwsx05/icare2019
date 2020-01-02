using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;

namespace com.digitalwave.iCare.middletier.HIS
{
    #region 原始库存
    /// <summary>
    /// 原始库存
    /// </summary>
    /// 
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsInitStorageMedicineSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase //MiddleTierBase.dll
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsInitStorageMedicineSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region  新建原始库存记录  欧阳孔伟  2004-06-07
        /// <summary>
        /// 新建原始库存记录
        /// </summary>
        /// <param name="p_objPrincipal">安全符</param>
        /// <param name="p_objItem">原始库存符</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAddNewInitStorageMedicine(clsInitStorageMedicine_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO T_BSE_INITSTORAGEMEDICINE (STORAGEID_CHR,MEDICINEID_CHR,LOTNO_CHR,UNITID_CHR,USEFULLIFE_DAT,PRODUCTORID_CHR,QTY_DEC,BUYPRICE_MNY,UNITPRICE_MNY,WHOLESALEUNITPRICE_MNY) 
							VALUES('" + p_objItem.m_objStorage.m_strStroageID + "','" + p_objItem.m_objMedicine.m_strMedicineID + "','" + p_objItem.m_strLotNo + "','" + p_objItem.m_objUnit.m_strUnitID +
                            "',to_date('" + p_objItem.m_strUsefulLifeDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objItem.m_objProductor.m_strVendorID + "'," + p_objItem.m_fltQty.ToString() + "," +
                            p_objItem.m_fltBuyPrice.ToString() + "," + p_objItem.m_fltUnitPrice.ToString() + "," + p_objItem.m_fltWholeSaleUnitPrice + ") ";
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

        #region 更改原始库存记录  欧阳孔伟  2004-06-07
        /// <summary>
        /// 更改原始库存记录
        /// </summary>
        /// <param name="p_objPrincipal">安全</param>
        /// <param name="p_objItem">原始库存信息</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoUpdInitStorageMedicine(clsInitStorageMedicine_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE T_BSE_INITSTORAGEMEDICINE
							SET LOTNO_CHR='" + p_objItem.m_strLotNo + "',UNITID_CHR='" + p_objItem.m_objUnit.m_strUnitID +
                            "',USEFULLIFE_DAT=to_date('" + p_objItem.m_strUsefulLifeDate + "','yyyy-mm-dd hh24:mi:ss'),PRODUCTORID_CHR='" +
                            p_objItem.m_objProductor.m_strVendorID + "',QTY_DEC=" + p_objItem.m_fltQty.ToString() + ",BUYPRICE_MNY=" +
                            p_objItem.m_fltBuyPrice.ToString() + ",UNITPRICE_MNY=" + p_objItem.m_fltUnitPrice.ToString() +
                            ",WHOLESALEUNITPRICE_MNY=" + p_objItem.m_fltWholeSaleUnitPrice + " " +
                            " WHERE STORAGEID_CHR='" + p_objItem.m_objStorage.m_strStroageID + "' AND MEDICINEID_CHR='" + p_objItem.m_objMedicine.m_strMedicineID + "' ";
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

        #region 删除原始库存记录  欧阳孔伟  2004-06-07
        /// <summary>
        /// 删除原始库存记录
        /// </summary>
        /// <param name="p_objPrincipal">安全符</param>
        /// <param name="p_strStorageID">仓库</param>
        /// <param name="p_strMedicineID">药品</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDeleteInitStorageMedicine(string p_strStorageID, string p_strMedicineID)
        {
            long lngRes = 0;
            string strSQL = @"DELETE T_BSE_INITSTORAGEMEDICINE
							 WHERE STORAGEID_CHR='" + p_strStorageID + "' AND MEDICINEID_CHR='" + p_strMedicineID + "' ";
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

        #region 将DataTable数据传递到VO上去  欧阳孔伟  2004-06-07
        /// <summary>
        /// 将DataTable数据传递到VO
        /// </summary>
        /// <param name="dtbSource"></param>
        /// <param name="objItemArr"></param>
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsInitStorageMedicine_VO[] objItemArr)
        {
            objItemArr = new clsInitStorageMedicine_VO[0];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItemArr = new clsInitStorageMedicine_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItemArr[i] = new clsInitStorageMedicine_VO();
                            objItemArr[i].m_objStorage = new clsStorage_VO();
                            objItemArr[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["STORAGEID_CHR"].ToString().Trim();
                            objItemArr[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["STORAGENAME_VCHR"].ToString().Trim();
                            objItemArr[i].m_objMedicine = new clsMedicine_VO();
                            objItemArr[i].m_objMedicine.m_strMedicineID = dtbSource.Rows[i]["MEDICINEID_CHR"].ToString().Trim();
                            objItemArr[i].m_objMedicine.m_strMedicineName = dtbSource.Rows[i]["MEDICINENAME_VCHR"].ToString().Trim();
                            objItemArr[i].m_objMedicine.m_strMedSpec = dtbSource.Rows[i]["MEDSPEC_VCHR"].ToString().Trim();
                            objItemArr[i].m_objMedicine.m_strPYCode = dtbSource.Rows[i]["PYCODE_CHR"].ToString().Trim();
                            objItemArr[i].m_objMedicine.m_strWBCode = dtbSource.Rows[i]["WBCODE_CHR"].ToString().Trim();
                            objItemArr[i].m_strLotNo = dtbSource.Rows[i]["LOTNO_CHR"].ToString().Trim();
                            objItemArr[i].m_objUnit = new clsUnit_VO();
                            objItemArr[i].m_objUnit.m_strUnitID = dtbSource.Rows[i]["UNITID_CHR"].ToString().Trim();
                            objItemArr[i].m_objUnit.m_strUnitName = dtbSource.Rows[i]["UNITNAME_CHR"].ToString().Trim();
                            objItemArr[i].m_strUsefulLifeDate = dtbSource.Rows[i]["USEFULLIFE_DAT"].ToString().Trim();
                            objItemArr[i].m_objProductor = new clsVendor_VO();
                            objItemArr[i].m_objProductor.m_strVendorID = dtbSource.Rows[i]["PRODUCTORID_CHR"].ToString().Trim();
                            objItemArr[i].m_objProductor.m_strVendorName = dtbSource.Rows[i]["VENDORNAME_VCHR"].ToString().Trim();

                            string strQty = "0";
                            string strBuyPrice = "0";
                            string strUnitPrice = "0";
                            string strWholeSaleUnitPrice = "0";

                            strQty = dtbSource.Rows[i]["QTY_DEC"].ToString().Trim();
                            if (strQty == "" || strQty == " ")
                            {
                                strQty = "0";
                            }
                            strBuyPrice = dtbSource.Rows[i]["BUYPRICE_MNY"].ToString().Trim();
                            if (strBuyPrice == "" || strBuyPrice == " ")
                            {
                                strBuyPrice = "0";
                            }
                            strUnitPrice = dtbSource.Rows[i]["UNITPRICE_MNY"].ToString().Trim();
                            if (strUnitPrice == "" || strUnitPrice == " ")
                            {
                                strUnitPrice = "0";
                            }
                            strWholeSaleUnitPrice = dtbSource.Rows[i]["WHOLESALEUNITPRICE_MNY"].ToString().Trim();
                            if (strWholeSaleUnitPrice == "" || strWholeSaleUnitPrice == " ")
                            {
                                strWholeSaleUnitPrice = "0";
                            }

                            objItemArr[i].m_fltQty = float.Parse(strQty);
                            objItemArr[i].m_fltBuyPrice = float.Parse(strBuyPrice);
                            objItemArr[i].m_fltUnitPrice = float.Parse(strUnitPrice);
                            objItemArr[i].m_fltWholeSaleUnitPrice = float.Parse(strWholeSaleUnitPrice);
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

        #region 模糊查找原始库存记录  欧阳孔伟  2004-06-07
        /// <summary>
        /// 模糊查找原始库存记录
        /// </summary>
        /// <param name="p_objPrincipal">安全符</param>
        /// <param name="p_strSQL">SQL脚本</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindInitStorageMedicineByAny(string p_strSQL, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];
            System.Data.DataTable dtbResult = null;

            string strSQL = @"SELECT *
								FROM v_bse_initstoragemedicine
							 " + p_strSQL;
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
                CopyDataTableToVO(dtbResult, out p_objResultArr);
            }

            return lngRes;
        }
        #endregion

        #region 按仓库ID和药品ID查找原始库存  欧阳孔伟  2004-06-08
        /// <summary>
        /// 按仓库ID和药品ID查找原始库存
        /// </summary>
        /// <param name="p_objPrincipal">安全符</param>
        /// <param name="p_strStorageID">仓库</param>
        /// <param name="p_strMedicineID">药品</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindInitStorageMedicineByKey(string p_strStorageID, string p_strMedicineID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];
            string strSQL = " WHERE STORAGEID_CHR='" + p_strStorageID + "' AND MEDICINEID_CHR='" + p_strMedicineID + "' ";

            lngRes = m_lngFindInitStorageMedicineByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按仓库查找原始库存  欧阳孔伟  2004-06-08
        /// <summary>
        /// 按仓库ID查找原始库存
        /// </summary>
        /// <param name="p_objPrincipal">安全符</param>
        /// <param name="p_strStorageID">仓库</param>
        /// <param name="p_objResultArr">输出信息</param>
        /// <returns>返回信息</returns>
        [AutoComplete]
        public long m_lngFindInitStorageMedicineByStorageID(string p_strStorageID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];
            string strSQL = " WHERE STORAGEID_CHR='" + p_strStorageID + "'";

            lngRes = m_lngFindInitStorageMedicineByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 按药品查找对应的原始库存  欧阳孔伟  2004-06-08
        /// <summary>
        /// 按药品查找对应的原始库存
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindInitStorageMedicineByMedicineID(string p_strMedicineID, out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];
            string strSQL = " WHERE MEDICINEID_CHR='" + p_strMedicineID + "'";

            lngRes = m_lngFindInitStorageMedicineByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region  查找所有的原始库存信息  欧阳孔伟  2004-06-08
        /// <summary>
        /// 查找所有的原始库存信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllInitStorageMedicine(out clsInitStorageMedicine_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsInitStorageMedicine_VO[0];
            string strSQL = " ";

            lngRes = m_lngFindInitStorageMedicineByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region 确认原始库存，并更改库存  未完成
        /// <summary>
        /// 确认原始库存
        /// </summary>
        /// <param name="p_objPrincipal">安全符</param>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_intFlag">返回标识，1：成功，0：失败，-1：异常</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngConfirmInitStorage(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                //				objParams[0].objParameter_Value = p_strNo;
                //				objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                //				objParams[0].strParameter_Name = "ApplNo";
                //				
                //				objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                //				objParams[1].strParameter_Name = "Flag";
                //				objParams[1].strParameter_Direction = clsDirection.strOutput;

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);

                //				p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());


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

        #region 原始库存登帐  未完成
        /// <summary>
        /// 原始库存登帐
        /// </summary>
        /// <param name="p_objPrincipal">安全符</param>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_intFlag">返回标识，1：成功，0：失败，-1：异常</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngAccountInitStorage(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                //				objParams[0].objParameter_Value = p_strNo;
                //				objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                //				objParams[0].strParameter_Name = "ApplNo";
                //				
                //				objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                //				objParams[1].strParameter_Name = "Flag";
                //				objParams[1].strParameter_Direction = clsDirection.strOutput;

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);

                //				p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());


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

        #region 获取员工资料
        [AutoComplete]
        public long m_lngGetEmployee(out clsEmployeeVO[] EmployeeVO)
        {
            long lngRes = 0;
            EmployeeVO = null;
            //			//权限类
            //			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
            //			//检查是否有使用些函数的权限
            //			lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.HIS.clsStorageMedLimitSvc","m_lngGetEmployee");
            //			if(lngRes < 0) //没有使用的权限
            //			{
            //				return -1;
            //			}
            string strSQL = "SELECT EMPID_CHR,EMPNO_CHR,LASTNAME_VCHR,PYCODE_CHR from t_bse_employee where STATUS_INT=1 order by EMPNO_CHR";
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
                EmployeeVO = new clsEmployeeVO[dtbResult.Rows.Count];
                for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                {
                    EmployeeVO[i1] = new clsEmployeeVO();
                    EmployeeVO[i1].strEmpID = dtbResult.Rows[i1]["EMPID_CHR"].ToString().Trim();
                    EmployeeVO[i1].strEmpNO = dtbResult.Rows[i1]["EMPNO_CHR"].ToString().Trim();
                    EmployeeVO[i1].strLastName = dtbResult.Rows[i1]["LASTNAME_VCHR"].ToString().Trim();
                    EmployeeVO[i1].strPYCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                }

            }
            return lngRes;

        }


        #endregion

        #endregion

    }
}
