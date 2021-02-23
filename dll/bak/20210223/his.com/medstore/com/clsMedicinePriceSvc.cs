using System;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Data;
namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 药品价格中间键 Create by Sam 2004-5-24
    /// </summary>
    /// 

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMedicinePriceSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 添加药品价格信息
        /// <summary>
        /// 添加药品价格信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedPrice"></param>
        /// <param name="ModifyDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoAddNewPrice(clsMedicinePrice_VO p_objMedPrice, out string ModifyDate)
        {
            //取得当前服务器的时间
            string strDate = DateTime.Now.ToShortDateString();
            ModifyDate = strDate;//返回增加后的时间
            long lngRes = 0;
            clsHisBase hisbase = new clsHisBase();
            strDate = hisbase.s_GetServerDate().ToString();
            ModifyDate = strDate;//返回增加后的时间
            string strSQL = "Insert into T_BSE_MEDICINEPRICE(MODIFY_DAT,START_DAT,END_DATE,UNITID_CHR,LOWINPRICE_MNY,HIGHINPRICE_MNY,LOWOUTPRICE_MNY, " +
                "HIGHOUTPRICE_MNY,CURINPRICE_MNY,CUROUTPRICE_MNY,OPREATORID_CHR,STATUS_INT,MEDICINEID_CHR)" +
                " Values (to_date('" + strDate + "','yyyy-mm-dd hh24:mi:ss'),to_date('" + p_objMedPrice.m_strStartDate + "','yyyy-mm-dd')," +
                " to_date('" + p_objMedPrice.m_strEndDate + "','yyyy-mm-dd'),'" + p_objMedPrice.m_objUnit.m_strUnitID + "', " +
                " '" + p_objMedPrice.m_fltLowInPrice + "','" + p_objMedPrice.m_fltHighInPrice + "'," +
                " '" + p_objMedPrice.m_fltLowOutPrice + "','" + p_objMedPrice.m_fltHighOutPrice + "', " +
                "'" + p_objMedPrice.m_fltCurInPrice + "','" + p_objMedPrice.m_fltCurOutPrice + "'," +
                "'" + p_objMedPrice.m_strOpreatorID + "','" + p_objMedPrice.m_intStatus + "'," +
                "'" + p_objMedPrice.m_objMedicineID.m_strMedicineID + "')";
            try
            {
                //定义一数据执行类
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {

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

        #region 更新药品价格信息
        /// <summary>
        /// 更新药品价格信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedPrice"></param>
        /// <param name="ModifyDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdPriceByMedicineID(clsMedicinePrice_VO p_objMedPrice, out string ModifyDate)
        {
            //取得当前服务器的时间
            string strDate = DateTime.Now.ToShortDateString();
            ModifyDate = strDate;
            long lngRes = 0;
            string strSQL = "";

            lngRes = this.m_lngDoAddNewPrice(p_objMedPrice, out ModifyDate);
            if (lngRes > 0)
                return 1;
            clsHisBase hisbase = new clsHisBase();
            strDate = hisbase.s_GetServerDate().ToString();
            //ModifyDate=strDate;
            strSQL = "Update T_BSE_MEDICINEPRICE Set STATUS_INT=-1,MODIFY_DAT=to_date('" + strDate + "','yyyy-mm-dd hh24:mi:ss') " +
                " Where MEDICINEID_CHR='" + p_objMedPrice.m_objMedicineID.m_strMedicineID + "' " +
                " And MODIFY_DAT=to_date('" + p_objMedPrice.m_strModifyDate + "','yyyy-mm-dd hh24:mi:ss') ";

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

        #region 删除药品价格信息
        /// <summary>
        /// 删除药品价格信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMedPrice"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDeletePriceByMedicineID(clsMedicinePrice_VO p_objMedPrice)
        {
            long lngRes = 0;
            //取得当前服务器的时间
            string strDate = DateTime.Now.ToShortDateString();
            clsHisBase hisbase = new clsHisBase();
            strDate = hisbase.s_GetServerDate().ToString();
            //更新状态为删除
            string strSQL = "Update T_BSE_MEDICINEPRICE Set STATUS_INT=0,MODIFY_DAT=to_date('" + strDate + "','yyyy-mm-dd hh24:mi:ss') " +
                " Where MEDICINEID_CHR='" + p_objMedPrice.m_objMedicineID.m_strMedicineID + "' " +
                " And MODIFY_DAT=to_date('" + p_objMedPrice.m_strModifyDate + "','yyyy-mm-dd hh24:mi:ss') ";

            //			string strSQL="Delete T_BSE_MEDICINEPRICE " +
            //				" Where MEDICINEID_CHR='"+p_objMedPrice.m_objMedicineID.m_strMedicineID+"' " ;//+
            //			//" And MODIFY_DAT='"+p_objMedPrice.m_strModifyDate+"' ";
            try
            {
                //定义一数据执行类
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                if (lngRes > 0)
                {

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

        #region 返回当前药品价格信息（不包括历史）
        /// <summary>
        /// 返回当前药品价格信息（不包括历史）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAllCurPrice(out clsMedicinePrice_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedicinePrice_VO[0];
            string strSQL = "Select a.*,b.UNITNAME_CHR,c.MEDICINENAME_VCHR From T_BSE_MEDICINEPRICE a " +
                " Left Outer Join T_AID_UNIT b On a.UNITID_CHR=b.UNITID_CHR " +
                " Left Outer Join T_BSE_MEDICINE c On a.MEDICINEID_CHR=c.MEDICINEID_CHR " +
                " Where a.STATUS_INT=1 ";
            lngRes = this.m_lngResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 根据药品ID和修改时间查看当前药品价格信息
        /// <summary>
        /// 根据药品ID和修改时间查看当前药品价格信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strMedID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindCurPriceByMedicineID(string strMedID, out clsMedicinePrice_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedicinePrice_VO[0];
            string strSQL = "Select a.*,b.UNITNAME_CHR,c.MEDICINENAME_VCHR From T_BSE_MEDICINEPRICE a " +
                " Left Outer Join T_AID_UNIT b On a.UNITID_CHR=b.UNITID_CHR " +
                " Left Outer Join T_BSE_MEDICINE c On a.MEDICINEID_CHR=c.MEDICINEID_CHR " +
                " Where a.MEDICINEID_CHR='" + strMedID + "' " +
                " And a.STATUS_INT=1 ";
            lngRes = this.m_lngResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 查看药品价格调整历史信息
        /// <summary>
        /// 查看药品价格调整历史信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedID"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindPriceHistoryByMedicineID(string MedID, out clsMedicinePrice_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedicinePrice_VO[0];
            string strSQL = "Select a.*,b.UNITNAME_CHR,c.MEDICINENAME_VCHR From T_BSE_MEDICINEPRICE a " +
                " Left Outer Join T_AID_UNIT b On a.UNITID_CHR=b.UNITID_CHR " +
                " Left Outer Join T_BSE_MEDICINE c On a.MEDICINEID_CHR=c.MEDICINEID_CHR " +
                " Where a.MEDICINEID_CHR='" + MedID + "'  And a.STATUS_INT<>1 ";
            lngRes = this.m_lngResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region 输出查询结果
        //把结果填充到VO中
        /// <summary>
        /// 输出查询结果
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        private long m_lngResult(string strSQL, out clsMedicinePrice_VO[] p_objResultArr)
        {

            long lngRes = 0;
            p_objResultArr = new clsMedicinePrice_VO[0];
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicinePrice_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicinePrice_VO();
                        p_objResultArr[i1].m_fltCurInPrice = float.Parse(dtbResult.Rows[i1]["CURINPRICE_MNY"].ToString().Trim());
                        p_objResultArr[i1].m_fltCurOutPrice = float.Parse(dtbResult.Rows[i1]["CUROUTPRICE_MNY"].ToString().Trim());
                        p_objResultArr[i1].m_fltHighInPrice = float.Parse(dtbResult.Rows[i1]["HIGHINPRICE_MNY"].ToString().Trim());
                        p_objResultArr[i1].m_fltHighOutPrice = float.Parse(dtbResult.Rows[i1]["HIGHOUTPRICE_MNY"].ToString().Trim());
                        p_objResultArr[i1].m_fltLowInPrice = float.Parse(dtbResult.Rows[i1]["LOWINPRICE_MNY"].ToString().Trim());
                        p_objResultArr[i1].m_fltLowOutPrice = float.Parse(dtbResult.Rows[i1]["LOWOUTPRICE_MNY"].ToString().Trim());
                        p_objResultArr[i1].m_intStatus = int.Parse(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
                        p_objResultArr[i1].m_objMedicineID = new clsMedicine_VO();
                        p_objResultArr[i1].m_objMedicineID.m_strMedicineID = dtbResult.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objMedicineID.m_strMedicineName = dtbResult.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
                        p_objResultArr[i1].m_objUnit = new clsUnit_VO();
                        p_objResultArr[i1].m_objUnit.m_strUnitID = dtbResult.Rows[i1]["UNITID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_objUnit.m_strUnitName = dtbResult.Rows[i1]["UNITNAME_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strEndDate = dtbResult.Rows[i1]["END_DATE"].ToString().Trim();
                        p_objResultArr[i1].m_strModifyDate = dtbResult.Rows[i1]["MODIFY_DAT"].ToString().Trim();
                        p_objResultArr[i1].m_strOpreatorID = dtbResult.Rows[i1]["OPREATORID_CHR"].ToString().Trim();
                        p_objResultArr[i1].m_strStartDate = dtbResult.Rows[i1]["START_DAT"].ToString().Trim();
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

    }// END CLASS DEFINITION clsMedicinePriceSvc

} // 药库管理
