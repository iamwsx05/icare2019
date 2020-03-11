using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.PartogramService
{
    /// <summary>
    /// 产程记录的中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPartogramService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        DateTime m_dtmInvalidDate = new DateTime(1900, 1, 1);
        /// <summary>
        /// 添加主记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMain"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMain( clsPartogramMain_VO p_objMain, clsPartogramContent_VO p_objContent)
        {
            if (p_objMain == null || p_objContent == null) return -1;

            long lngRes = 0;
                //Delete Old
                string strSql = @"insert into t_emr_partogrammain
  (registerid_chr,
   createuserid_chr,
   createdate_dat,
   recorduserid_vchr,
   recorddate_dat,
   status_int,
   ifconfirm_int,
   sequence_int,
   childbearingway_vchr,
   childbearingway_xml_vchr,
   firstpartogram_vchr,
   firstpartogram_xml_vchr,
   secondpartogram_vchr,
   secondpartogram_xml_vchr,
   thirdpartogram_vchr,
   thirdpartogram_xml_vchr,
   allpartogram_vchr,
   allpartogram_xml_vchr,
   aiduser_vchr,
   aiduser_xml_vchr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?)";//20

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(20, out objDPArr);
                objDPArr[0].Value = p_objMain.m_strREGISTERID_CHR;
                objDPArr[1].Value = p_objMain.m_strCREATEUSERID_CHR;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objMain.m_dtmCREATEDATE_DAT;
                objDPArr[3].Value = p_objMain.m_strRECORDUSERID_VCHR;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objMain.m_dtmRECORDDATE_DAT;
                objDPArr[5].Value = 1;
                objDPArr[6].Value = 0;
                objDPArr[7].Value = lngSequence;
                objDPArr[8].Value = p_objMain.m_strCHILDBEARINGWAY_VCHR;
                objDPArr[9].Value = p_objMain.m_strCHILDBEARINGWAY_XML_VCHR;
                objDPArr[10].Value = p_objMain.m_strFIRSTPARTOGRAM_VCHR;
                objDPArr[11].Value = p_objMain.m_strFIRSTPARTOGRAM_XML_VCHR;
                objDPArr[12].Value = p_objMain.m_strSECONDPARTOGRAM_VCHR;
                objDPArr[13].Value = p_objMain.m_strSECONDPARTOGRAM_XML_VCHR;
                objDPArr[14].Value = p_objMain.m_strTHIRDPARTOGRAM_VCHR;
                objDPArr[15].Value = p_objMain.m_strTHIRDPARTOGRAM_XML_VCHR;
                objDPArr[16].Value = p_objMain.m_strALLPARTOGRAM_VCHR;
                objDPArr[17].Value = p_objMain.m_strALLPARTOGRAM_XML_VCHR;
                objDPArr[18].Value = p_objMain.m_strAIDUSER_VCHR;
                objDPArr[19].Value = p_objMain.m_strAIDUSER_XML_VCHR;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;
                //保存签名集合
                objSign.m_lngAddSign(p_objMain.objSignerArr, lngSequence);

                lngRes = m_lngAddMainContent(p_objContent);

            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 添加子记录
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddMainContent(clsPartogramContent_VO p_objContent)
        {
            if (p_objContent == null) return -1;

            long lngRes = 0;
            
                string strSql2 = @"insert into t_emr_partogramcontent
  (registerid_chr,
   createdate_dat,
   status_int,
   modifydate_dat,
   modifyuserid_chr,
   lastmenses_dat,
   edc_dat,
   breaktime_dat,
   givebirthtime_dat,
   sex_vchr,
   weight_int,
   height_int,
   childbearingway_r_vchr,
   firstpartogram_r_vchr,
   secondpartogram_r_vchr,
   thirdpartogram_r_vchr,
   allpartogram_r_vchr,
   graviditycount_int,
   borncount_int,
   aiduser_r_vchr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?)";//20

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(20, out objDPArr2);
                objDPArr2[0].Value = p_objContent.m_strREGISTERID_CHR;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_objContent.m_dtmCREATEDATE_DAT;
                objDPArr2[2].Value = 1;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = p_objContent.m_dtmMODIFYDATE_DAT;
                objDPArr2[4].Value = p_objContent.m_strMODIFYUSERID_CHR;
                objDPArr2[5].DbType = DbType.DateTime;
                objDPArr2[5].Value = p_objContent.m_dtmLASTMENSES_DAT;
                objDPArr2[6].DbType = DbType.DateTime;
                objDPArr2[6].Value = p_objContent.m_dtmEDC_DAT;
                objDPArr2[7].DbType = DbType.DateTime;
                objDPArr2[7].Value = p_objContent.m_dtmBREAKTIME_DAT;
                objDPArr2[8].DbType = DbType.DateTime;
                objDPArr2[8].Value = p_objContent.m_dtmGIVEBIRTHTIME_DAT;
                objDPArr2[9].Value = p_objContent.m_strSEX_VCHR;
                if (p_objContent.m_intWEIGHT_INT != 0)
                    objDPArr2[10].Value = p_objContent.m_intWEIGHT_INT;
                else
                    objDPArr2[10].Value = DBNull.Value;
                if (p_objContent.m_intHEIGHT_INT != 0)
                    objDPArr2[11].Value = p_objContent.m_intHEIGHT_INT;
                else
                    objDPArr2[11].Value = DBNull.Value;
                objDPArr2[12].Value = p_objContent.m_strCHILDBEARINGWAY_R_VCHR;
                objDPArr2[13].Value = p_objContent.m_strFIRSTPARTOGRAM_R_VCHR;
                objDPArr2[14].Value = p_objContent.m_strSECONDPARTOGRAM_R_VCHR;
                objDPArr2[15].Value = p_objContent.m_strTHIRDPARTOGRAM_R_VCHR;
                objDPArr2[16].Value = p_objContent.m_strALLPARTOGRAM_R_VCHR;
                objDPArr2[17].Value = p_objContent.m_intGRAVIDITYCOUNT_INT;
                objDPArr2[18].Value = p_objContent.m_intBORNCOUNT_INT;
                objDPArr2[19].Value = p_objContent.m_strAIDUSER_R_VCHR;

                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql2, ref lngAff, objDPArr2);

            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 修改主记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMain"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyMain( clsPartogramMain_VO p_objMain, clsPartogramContent_VO p_objContent)
        {
            if (p_objMain == null || p_objContent == null) return -1;

            long lngRes = 0;
            //Delete Old
            string strSql = @"update t_emr_partogrammain
   set recorduserid_vchr = ?,
       recorddate_dat = ?,
       sequence_int = ?,
       childbearingway_vchr = ?,
       childbearingway_xml_vchr = ?,
       firstpartogram_vchr = ?,
       firstpartogram_xml_vchr = ?,
       secondpartogram_vchr = ?,
       secondpartogram_xml_vchr = ?,
       thirdpartogram_vchr = ?,
       thirdpartogram_xml_vchr = ?,
       allpartogram_vchr = ?,
       allpartogram_xml_vchr = ?,
       aiduser_vchr = ?,
   aiduser_xml_vchr = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and  status_int = 1";//17

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(17, out objDPArr);
                objDPArr[0].Value = p_objMain.m_strRECORDUSERID_VCHR;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objMain.m_dtmRECORDDATE_DAT;
                objDPArr[2].Value = lngSequence;
                objDPArr[3].Value = p_objMain.m_strCHILDBEARINGWAY_VCHR;
                objDPArr[4].Value = p_objMain.m_strCHILDBEARINGWAY_XML_VCHR;
                objDPArr[5].Value = p_objMain.m_strFIRSTPARTOGRAM_VCHR;
                objDPArr[6].Value = p_objMain.m_strFIRSTPARTOGRAM_XML_VCHR;
                objDPArr[7].Value = p_objMain.m_strSECONDPARTOGRAM_VCHR;
                objDPArr[8].Value = p_objMain.m_strSECONDPARTOGRAM_XML_VCHR;
                objDPArr[9].Value = p_objMain.m_strTHIRDPARTOGRAM_VCHR;
                objDPArr[10].Value = p_objMain.m_strTHIRDPARTOGRAM_XML_VCHR;
                objDPArr[11].Value = p_objMain.m_strALLPARTOGRAM_VCHR;
                objDPArr[12].Value = p_objMain.m_strALLPARTOGRAM_XML_VCHR;
                objDPArr[13].Value = p_objMain.m_strAIDUSER_VCHR;
                objDPArr[14].Value = p_objMain.m_strAIDUSER_XML_VCHR;
                objDPArr[15].Value = p_objMain.m_strREGISTERID_CHR;
                objDPArr[16].DbType = DbType.DateTime;
                objDPArr[16].Value = p_objMain.m_dtmCREATEDATE_DAT;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;
                //保存签名集合
                objSign.m_lngAddSign(p_objMain.objSignerArr, lngSequence);

                string strSql2 = @"update t_emr_partogramcontent
   set status_int = 2
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//3
                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                objDPArr2[0].Value = p_objContent.m_strREGISTERID_CHR;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_objContent.m_dtmCREATEDATE_DAT;

                lngRes = objHRPServ.lngExecuteParameterSQL(strSql2, ref lngAff, objDPArr2);
                if (lngRes > 0)
                {
                    lngRes = m_lngAddMainContent(p_objContent);
                }

            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <param name="p_strEmpId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteMain(string p_strRegisterId, DateTime p_dtmCreatedDate,string p_strEmpId)
        {
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmCreatedDate == DateTime.MinValue) return -1;

            long lngRes = 0;

            string strSql = @"update t_emr_partogrammain
   set status_int = 0,
       deactiveddate_dat = ?,
       deactivedoperatorid_chr = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//4

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr2[1].Value = p_strEmpId;
                objDPArr2[2].Value = p_strRegisterId;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = p_dtmCreatedDate;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr2);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetValues( string p_strRegisterId, out clsPartogramAll_VO p_objContent)
        {
            p_objContent = null;
            if (string.IsNullOrEmpty(p_strRegisterId)) return -1;

            long lngRes = 0;

            string strSql = @"select registerid_chr,
       createuserid_chr,
       createdate_dat,
       recorduserid_vchr,
       recorddate_dat,
       status_int,
       ifconfirm_int,
       sequence_int,
       firstprintdate_dat,
       deactiveddate_dat,
       deactivedoperatorid_chr,
       childbearingway_vchr,
       childbearingway_xml_vchr,
       firstpartogram_vchr,
       firstpartogram_xml_vchr,
       secondpartogram_vchr,
       secondpartogram_xml_vchr,
       thirdpartogram_vchr,
       thirdpartogram_xml_vchr,
       allpartogram_vchr,
       allpartogram_xml_vchr,
       aiduser_vchr,
       aiduser_xml_vchr
  from t_emr_partogrammain t
 where t.registerid_chr = ?
   and t.status_int = 1";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;

                #region Main
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtb = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtb, objDPArr);
                int intRowCount = dtb.Rows.Count;
                DataRow objRow = null;
                p_objContent = new clsPartogramAll_VO();
                DateTime dtmTemp = m_dtmInvalidDate;
                int intTemp = 0;
                if (lngRes > 0 && intRowCount == 1)
                {

                    objRow = dtb.Rows[0];
                    clsPartogramMain_VO objMain = new clsPartogramMain_VO();
                    objMain.m_strREGISTERID_CHR = p_strRegisterId;
                    DateTime.TryParse(objRow["createdate_dat"].ToString(), out dtmTemp);
                    objMain.m_dtmCREATEDATE_DAT = dtmTemp;
                    dtmTemp = m_dtmInvalidDate;
                    DateTime.TryParse(objRow["firstprintdate_dat"].ToString(), out dtmTemp);
                    objMain.m_dtmFIRSTPRINTDATE_DAT = dtmTemp;
                    dtmTemp = m_dtmInvalidDate;
                    DateTime.TryParse(objRow["recorddate_dat"].ToString(), out dtmTemp);
                    objMain.m_dtmRECORDDATE_DAT = dtmTemp;
                    int.TryParse(objRow["ifconfirm_int"].ToString(), out intTemp);
                    objMain.m_intIFCONFIRM_INT = intTemp;
                    objMain.m_intMarkStatus = -1;
                    objMain.m_intSTATUS_INT = 1;
                    objMain.m_strALLPARTOGRAM_VCHR = objRow["allpartogram_vchr"].ToString();
                    objMain.m_strALLPARTOGRAM_XML_VCHR = objRow["allpartogram_xml_vchr"].ToString();
                    objMain.m_strCHILDBEARINGWAY_VCHR = objRow["childbearingway_vchr"].ToString();
                    objMain.m_strCHILDBEARINGWAY_XML_VCHR = objRow["childbearingway_xml_vchr"].ToString();
                    objMain.m_strCREATEUSERID_CHR = objRow["createuserid_chr"].ToString();
                    objMain.m_strFIRSTPARTOGRAM_VCHR = objRow["firstpartogram_vchr"].ToString();
                    objMain.m_strFIRSTPARTOGRAM_XML_VCHR = objRow["firstpartogram_xml_vchr"].ToString();
                    objMain.m_strRECORDUSERID_VCHR = objRow["recorduserid_vchr"].ToString();
                    objMain.m_strSECONDPARTOGRAM_VCHR = objRow["secondpartogram_vchr"].ToString();
                    objMain.m_strSECONDPARTOGRAM_XML_VCHR = objRow["secondpartogram_xml_vchr"].ToString();
                    objMain.m_strTHIRDPARTOGRAM_VCHR = objRow["thirdpartogram_vchr"].ToString();
                    objMain.m_strTHIRDPARTOGRAM_XML_VCHR = objRow["thirdpartogram_xml_vchr"].ToString();
                    objMain.m_strAIDUSER_VCHR = objRow["AIDUSER_VCHR"].ToString();
                    objMain.m_strAIDUSER_XML_VCHR = objRow["AIDUSER_XML_VCHR"].ToString();
                    if (objRow["sequence_int"] != DBNull.Value)
                    {
                        long lngS = 0;
                        if (long.TryParse(objRow["SEQUENCE_INT"].ToString(), out lngS))
                        {
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out objMain.objSignerArr);

                            //释放
                            objSign = null;
                        }
                    }
                    p_objContent.m_objPartogramMain = objMain;
                }
                #endregion Main

                #region Content
                if (p_objContent.m_objPartogramMain != null)
                {
                    strSql = @"select registerid_chr,
       createdate_dat,
       status_int,
       modifydate_dat,
       modifyuserid_chr,
       lastmenses_dat,
       edc_dat,
       breaktime_dat,
       givebirthtime_dat,
       sex_vchr,
       weight_int,
       height_int,
       childbearingway_r_vchr,
       firstpartogram_r_vchr,
       secondpartogram_r_vchr,
       thirdpartogram_r_vchr,
       allpartogram_r_vchr,
       graviditycount_int,
       borncount_int,
       aiduser_r_vchr
  from t_emr_partogramcontent
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strRegisterId;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objContent.m_objPartogramMain.m_dtmCREATEDATE_DAT;

                    dtb = new DataTable();
                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtb, objDPArr);
                    intRowCount = dtb.Rows.Count;
                    if (lngRes <= 0 || intRowCount != 1) return lngRes;
                    objRow = dtb.Rows[0];
                    clsPartogramContent_VO objContent = new clsPartogramContent_VO();
                    objContent.m_strREGISTERID_CHR = p_strRegisterId;
                    objContent.m_dtmCREATEDATE_DAT = p_objContent.m_objPartogramMain.m_dtmCREATEDATE_DAT;
                    dtmTemp = m_dtmInvalidDate;
                    DateTime.TryParse(objRow["breaktime_dat"].ToString(), out dtmTemp);
                    objContent.m_dtmBREAKTIME_DAT = dtmTemp;
                    dtmTemp = m_dtmInvalidDate;
                    DateTime.TryParse(objRow["edc_dat"].ToString(), out dtmTemp);
                    objContent.m_dtmEDC_DAT = dtmTemp;
                    dtmTemp = m_dtmInvalidDate;
                    DateTime.TryParse(objRow["givebirthtime_dat"].ToString(), out dtmTemp);
                    objContent.m_dtmGIVEBIRTHTIME_DAT = dtmTemp;
                    dtmTemp = m_dtmInvalidDate;
                    DateTime.TryParse(objRow["lastmenses_dat"].ToString(), out dtmTemp);
                    objContent.m_dtmLASTMENSES_DAT = dtmTemp;
                    dtmTemp = m_dtmInvalidDate;
                    DateTime.TryParse(objRow["modifydate_dat"].ToString(), out dtmTemp);
                    objContent.m_dtmMODIFYDATE_DAT = dtmTemp;
                    intTemp = 0;
                    int.TryParse(objRow["borncount_int"].ToString(), out intTemp);
                    objContent.m_intBORNCOUNT_INT = intTemp;
                    intTemp = 0;
                    int.TryParse(objRow["graviditycount_int"].ToString(), out intTemp);
                    objContent.m_intGRAVIDITYCOUNT_INT = intTemp;
                    intTemp = 0;
                    int.TryParse(objRow["height_int"].ToString(), out intTemp);
                    objContent.m_intHEIGHT_INT = intTemp;
                    objContent.m_intSTATUS_INT = 1;
                    intTemp = 0;
                    int.TryParse(objRow["weight_int"].ToString(), out intTemp);
                    objContent.m_intWEIGHT_INT = intTemp;
                    objContent.m_strALLPARTOGRAM_R_VCHR = objRow["allpartogram_r_vchr"].ToString();
                    objContent.m_strCHILDBEARINGWAY_R_VCHR = objRow["childbearingway_r_vchr"].ToString();
                    objContent.m_strFIRSTPARTOGRAM_R_VCHR = objRow["firstpartogram_r_vchr"].ToString();
                    objContent.m_strMODIFYUSERID_CHR = objRow["modifyuserid_chr"].ToString();
                    objContent.m_strSECONDPARTOGRAM_R_VCHR = objRow["secondpartogram_r_vchr"].ToString();
                    objContent.m_strSEX_VCHR = objRow["sex_vchr"].ToString();
                    objContent.m_strTHIRDPARTOGRAM_R_VCHR = objRow["thirdpartogram_r_vchr"].ToString();
                    objContent.m_strAIDUSER_R_VCHR = objRow["AIDUSER_R_VCHR"].ToString();
                    p_objContent.m_objPartogramContent = objContent;
                }
                #endregion Content

                #region SubMain
                strSql = @"select a.registerid_chr,
       a.createuserid_chr,
       a.createdate_dat,
       a.modifydate_dat,
       a.modifyuserid_chr,
       a.partogram_int,
       a.systolicpressure_int,
       a.diastolicpressure_int,
       a.uterinecontraction_int,
       a.checkdate_dat,
       a.process_vchr,
       a.process_r_vchr,
       a.process_xml_vchr,
       a.sequence_int,
       a.fetalrhythm_int,
       a.uterinecontractionmin_int,
       a.markstatus
  from t_emr_partogram a
 where a.registerid_chr = ?
   and a.status_int = 1";
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                dtb = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtb, objDPArr);
                intRowCount = dtb.Rows.Count;
                if (lngRes <= 0 || intRowCount <= 0) return lngRes;

                #region Get Point
                strSql = @"select b.createdate_dat,
       b.registerid_chr,
       b.partogram_int,
       b.modifydate_dat,
       b.modifyuserid_chr,
       b.pointmin_int,
       b.pointvalue_int,
       b.pointid_int,
       b.poingtype_int,
       b.checkdate_dat,
       b.childbearingpoint_int,
       emp2.lastname_vchr
  from t_emr_partogram_point b
 inner join t_bse_employee emp2 on b.modifyuserid_chr = emp2.empid_chr
 where b.registerid_chr = ?
   and b.status_int = 1";

                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtbPoints = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbPoints, objDPArr);

                #endregion Get Point

                clsPartogram_VO[] obj_VOArr = null;
                obj_VOArr = new clsPartogram_VO[intRowCount];
                for (int i = 0; i < intRowCount; i++)
                {
                    objRow = dtb.Rows[i];
                    obj_VOArr[i] = m_objGetPartogram_VO(objRow);
                    obj_VOArr[i].m_strREGISTERID_CHR = p_strRegisterId;

                    DataRow[] rows = dtbPoints.Select("partogram_int = " + obj_VOArr[i].m_intPARTOGRAM_INT);
                    if (rows.Length > 0)
                    {
                        obj_VOArr[i].m_ObjPointArr = new clsPartogram_Point[rows.Length];
                        for (int j = 0; j < rows.Length; j++)
                        {
                            obj_VOArr[i].m_ObjPointArr[j] = m_objGetPoint_VO(rows[j]);
                            obj_VOArr[i].m_ObjPointArr[j].m_intPARTOGRAM_INT = obj_VOArr[i].m_intPARTOGRAM_INT;
                            obj_VOArr[i].m_ObjPointArr[j].m_strREGISTERID_CHR = p_strRegisterId;
                        }
                    }

                }
                p_objContent.m_ObjPartogramArr = obj_VOArr;
                if (p_objContent.m_ObjPartogramArr != null)
                    p_objContent.m_dtmFirstSave = p_objContent.m_ObjPartogramArr[0].m_dtmCHECKDATE_DAT;
                #endregion SubMain
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                if (p_objContent != null)
                {
                    if (p_objContent.m_objPartogramMain == null && p_objContent.m_ObjPartogramArr == null)
                        p_objContent = null;
                }
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 返回一个小时的记录
        /// </summary>
        /// <param name="p_objRow"></param>
        /// <returns></returns>
        [AutoComplete]
        private clsPartogram_VO m_objGetPartogram_VO(DataRow p_objRow)
        {
            clsPartogram_VO obj_VO = new clsPartogram_VO();
            DateTime dtmTemp = m_dtmInvalidDate;
            DateTime.TryParse(p_objRow["checkdate_dat"].ToString(), out dtmTemp);
            obj_VO.m_dtmCHECKDATE_DAT = dtmTemp;
            dtmTemp = m_dtmInvalidDate;
            DateTime.TryParse(p_objRow["createdate_dat"].ToString(), out dtmTemp);
            obj_VO.m_dtmCREATEDATE_DAT = dtmTemp;
            dtmTemp = m_dtmInvalidDate;
            DateTime.TryParse(p_objRow["modifydate_dat"].ToString(), out dtmTemp);
            obj_VO.m_dtmMODIFYDATE_DAT = dtmTemp;
            int intTemp = 0;
            int.TryParse(p_objRow["diastolicpressure_int"].ToString(), out intTemp);
            obj_VO.m_intDIASTOLICPRESSURE_INT = intTemp;
            intTemp = 0;
            int.TryParse(p_objRow["fetalrhythm_int"].ToString(), out intTemp);
            obj_VO.m_intFETALRHYTHM_INT = intTemp;
            intTemp = 0;
            int.TryParse(p_objRow["partogram_int"].ToString(), out intTemp);
            obj_VO.m_intPARTOGRAM_INT = intTemp;
            obj_VO.m_intSTATUS_INT = 1;
            intTemp = 0;
            int.TryParse(p_objRow["systolicpressure_int"].ToString(), out intTemp);
            obj_VO.m_intSYSTOLICPRESSURE_INT = intTemp;
            intTemp = 0;
            int.TryParse(p_objRow["uterinecontraction_int"].ToString(), out intTemp);
            obj_VO.m_intUTERINECONTRACTION_INT = intTemp;
            intTemp = 0;
            int.TryParse(p_objRow["MARKSTATUS"].ToString(), out intTemp);
            obj_VO.m_intMarkStatus = intTemp;
            intTemp = 0;
            int.TryParse(p_objRow["uterinecontractionmin_int"].ToString(), out intTemp);
            obj_VO.m_intUTERINECONTRACTIONMIN_INT = intTemp;
            obj_VO.m_strMODIFYUSERID_CHR = p_objRow["modifyuserid_chr"].ToString();
            obj_VO.m_strPROCESS_R_VCHR = p_objRow["process_r_vchr"].ToString();
            obj_VO.m_strCREATEUSERID_CHR = p_objRow["CREATEUSERID_CHR"].ToString();
            obj_VO.m_strPROCESS_VCHR = p_objRow["process_vchr"].ToString();
            obj_VO.m_strPROCESS_XML_VCHR = p_objRow["process_xml_vchr"].ToString();

            if (p_objRow["sequence_int"] != DBNull.Value)
            {
                long lngS = 0;
                if (long.TryParse(p_objRow["SEQUENCE_INT"].ToString(), out lngS))
                {
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    long lngTemp = objSign.m_lngGetSign(lngS, out obj_VO.objSignerArr);
                    if (lngTemp > 0 && obj_VO.objSignerArr != null)
                    {
                        if (obj_VO.objSignerArr.Length > 0)
                        {
                            obj_VO.m_strMODIFYUSERNAME_VCHR = obj_VO.objSignerArr[0].objEmployee.m_strLASTNAME_VCHR;
                            for (int i = 1 ; i < obj_VO.objSignerArr.Length ; i++)
                            {
                                obj_VO.m_strMODIFYUSERNAME_VCHR += ","+obj_VO.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR;
                            }
                        }
                    }
                    //释放
                    objSign = null;
                }
            }
            return obj_VO;
        }
        /// <summary>
        /// 返回一个点的记录
        /// </summary>
        /// <param name="p_objRow"></param>
        /// <returns></returns>
        [AutoComplete]
        private clsPartogram_Point m_objGetPoint_VO(DataRow p_objRow)
        {
            clsPartogram_Point ObjPoint = new clsPartogram_Point();
            DateTime dtmTemp = m_dtmInvalidDate;
            DateTime.TryParse(p_objRow["createdate_dat"].ToString(), out dtmTemp);
            ObjPoint.m_dtmCREATEDATE_DAT = dtmTemp;
            dtmTemp = m_dtmInvalidDate;
            DateTime.TryParse(p_objRow["modifydate_dat"].ToString(), out dtmTemp);
            ObjPoint.m_dtmMODIFYDATE_DAT = dtmTemp;
            dtmTemp = m_dtmInvalidDate;
            DateTime.TryParse(p_objRow["CHECKDATE_DAT"].ToString(), out dtmTemp);
            ObjPoint.m_dtmCheckDate = dtmTemp;
            float fltTtemp = 0;
            float.TryParse(p_objRow["pointvalue_int"].ToString(), out fltTtemp);
            ObjPoint.m_fltPointValue_INT = fltTtemp;
            //ObjPoint.m_intPARTOGRAM_INT = obj_VOArr[i].m_intPARTOGRAM_INT;
            int intTemp = 0;
            int.TryParse(p_objRow["pointid_int"].ToString(), out intTemp);
            ObjPoint.m_intPointID_INT = intTemp;
            intTemp = 0;
            int.TryParse(p_objRow["pointmin_int"].ToString(), out intTemp);
            ObjPoint.m_intPointMin_INT = intTemp;
            intTemp = 0;
            int.TryParse(p_objRow["poingtype_int"].ToString(), out intTemp);
            ObjPoint.m_intPointType_INT = intTemp;
            ObjPoint.m_intSTATUS_INT = 1;
            ObjPoint.m_strMODIFYUSERID_CHR = p_objRow["modifyuserid_chr"].ToString();
            ObjPoint.m_strMODIFYUSERNAME_VCHR = p_objRow["lastname_vchr"].ToString();
            intTemp = 0;
            int.TryParse(p_objRow["childbearingpoint_int"].ToString(), out intTemp);
            ObjPoint.m_intChildbearingPoint = intTemp;
            return ObjPoint;
        }
        /// <summary>
        /// 获取全部的小时记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllHourValues( string p_strRegisterId, out clsPartogram_VO[] p_objContentArr)
        {
            p_objContentArr = null;
            if (string.IsNullOrEmpty(p_strRegisterId)) return -1;

            long lngRes = 0;

            string strSql = @"select a.registerid_chr,
       a.createuserid_chr,
       a.createdate_dat,
       a.modifydate_dat,
       a.modifyuserid_chr,
       a.partogram_int,
       a.systolicpressure_int,
       a.diastolicpressure_int,
       a.uterinecontraction_int,
       a.checkdate_dat,
       a.process_vchr,
       a.process_r_vchr,
       a.process_xml_vchr,
       a.sequence_int,
       a.fetalrhythm_int,
       a.uterinecontractionmin_int,
       a.markstatus
  from t_emr_partogram a
 where a.registerid_chr = ?
   and a.status_int = 1";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                DataTable dtb = new DataTable(); ;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtb, objDPArr);
                int intRowCount = dtb.Rows.Count;
                if (lngRes <= 0 || intRowCount == 0) return lngRes;

                strSql = @"select b.createdate_dat,
       b.registerid_chr,
       b.partogram_int,
       b.modifydate_dat,
       b.modifyuserid_chr,
       b.pointmin_int,
       b.pointvalue_int,
       b.pointid_int,
       b.poingtype_int,
       b.checkdate_dat,
       b.childbearingpoint_int,
       emp2.lastname_vchr
  from t_emr_partogram_point b
 inner join t_bse_employee emp2 on b.modifyuserid_chr = emp2.empid_chr
 where b.registerid_chr = ?
   and b.status_int = 1";

                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtbPoints = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbPoints, objDPArr);
                DataRow objRow = null;
                p_objContentArr = new clsPartogram_VO[intRowCount];
                for (int j2 = 0; j2 < intRowCount; j2++)
                {
                    objRow = dtb.Rows[j2];
                    clsPartogram_VO objPartogram = m_objGetPartogram_VO(objRow);
                    objPartogram.m_strREGISTERID_CHR = p_strRegisterId;

                    #region Get Point
                    if (dtbPoints.Rows.Count > 0)
                    {
                        int intPartogram = objPartogram.m_intPARTOGRAM_INT;
                        DataRow[] rowPoints = dtbPoints.Select("partogram_int = '" + intPartogram + "'");
                        if (rowPoints.Length > 0)
                        {
                            objPartogram.m_ObjPointArr = new clsPartogram_Point[rowPoints.Length];
                            for (int i = 0; i < rowPoints.Length; i++)
                            {
                                objPartogram.m_ObjPointArr[i] = m_objGetPoint_VO(rowPoints[i]);
                                objPartogram.m_ObjPointArr[i].m_strREGISTERID_CHR = p_strRegisterId;
                                objPartogram.m_ObjPointArr[i].m_intPARTOGRAM_INT = intPartogram;
                            }
                        }
                    }
                    #endregion Get Point
                    p_objContentArr[j2] = objPartogram;
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 获取一个小时的记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <param name="p_intSelectedHour"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOneHourValues( string p_strRegisterId,int p_intSelectedHour, out clsPartogram_VO p_objContent)
        {
            p_objContent = null;
            if (string.IsNullOrEmpty(p_strRegisterId) || p_intSelectedHour < 0) return -1;

            long lngRes = 0;

            string strSql = @"select a.registerid_chr,
       a.createuserid_chr,
       a.createdate_dat,
       a.modifydate_dat,
       a.modifyuserid_chr,
       a.partogram_int,
       a.systolicpressure_int,
       a.diastolicpressure_int,
       a.uterinecontraction_int,
       a.checkdate_dat,
       a.process_vchr,
       a.process_r_vchr,
       a.process_xml_vchr,
       a.sequence_int,
       a.fetalrhythm_int,
       a.uterinecontractionmin_int,
       a.markstatus
  from t_emr_partogram a
 where a.registerid_chr = ?
   and a.status_int = 1
   and a.partogram_int = ?";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                //objDPArr[1].DbType = DbType.DateTime;
                //objDPArr[1].Value = p_dtmCreatedDate;
                objDPArr[1].Value = p_intSelectedHour;
                DataTable dtb = new DataTable(); ;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtb, objDPArr);
                if (lngRes <= 0 || dtb.Rows.Count == 0) return lngRes;

                p_objContent = m_objGetPartogram_VO(dtb.Rows[0]);
                p_objContent.m_strREGISTERID_CHR = p_strRegisterId;

                #region Get Point
                strSql = @"select b.createdate_dat,
       b.registerid_chr,
       b.partogram_int,
       b.modifydate_dat,
       b.modifyuserid_chr,
       b.pointmin_int,
       b.pointvalue_int,
       b.pointid_int,
       b.poingtype_int,
       b.checkdate_dat,
       b.childbearingpoint_int,
       emp2.lastname_vchr
  from t_emr_partogram_point b
 inner join t_bse_employee emp2 on b.modifyuserid_chr = emp2.empid_chr
 where b.registerid_chr = ?
   and b.status_int = 1
and b.partogram_int = ?";

                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].Value = p_intSelectedHour;

                DataTable dtbPoints = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbPoints, objDPArr);

                int intRowCount = dtbPoints.Rows.Count;
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_objContent.m_ObjPointArr = new clsPartogram_Point[intRowCount];
                    for (int i = 0 ; i < intRowCount ; i++)
                    {
                        p_objContent.m_ObjPointArr[i] = m_objGetPoint_VO(dtbPoints.Rows[i]);
                        p_objContent.m_ObjPointArr[i].m_strREGISTERID_CHR = p_strRegisterId;
                        p_objContent.m_ObjPointArr[i].m_intPARTOGRAM_INT = p_objContent.m_intPARTOGRAM_INT;
                    }
                }
                #endregion Get Point
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 返回一个小时的点记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_intSelectedHour"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOneHourPointValues( string p_strRegisterId, int p_intSelectedHour, out clsPartogram_Point[] p_objContentArr)
        {
            p_objContentArr = null;
            if (string.IsNullOrEmpty(p_strRegisterId) ||  p_intSelectedHour < 0) return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSql = @"select b.createdate_dat,
       b.registerid_chr,
       b.partogram_int,
       b.modifydate_dat,
       b.modifyuserid_chr,
       b.pointmin_int,
       b.pointvalue_int,
       b.pointid_int,
       b.poingtype_int,
       b.checkdate_dat,
       b.childbearingpoint_int,
       emp2.lastname_vchr
  from t_emr_partogram_point b
 inner join t_bse_employee emp2 on b.modifyuserid_chr = emp2.empid_chr
 where b.registerid_chr = ?
   and b.status_int = 1
and b.partogram_int = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].Value = p_intSelectedHour;

                DataTable dtbPoints = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbPoints, objDPArr);
                int intRowCount = dtbPoints.Rows.Count;
                if (lngRes <= 0 || intRowCount == 0) return 0;
                p_objContentArr = new clsPartogram_Point[intRowCount];
                for (int i = 0 ; i < intRowCount ; i++)
                {
                    p_objContentArr[i] = m_objGetPoint_VO(dtbPoints.Rows[i]);
                    p_objContentArr[i].m_strREGISTERID_CHR = p_strRegisterId;
                    p_objContentArr[i].m_intPARTOGRAM_INT = p_intSelectedHour;
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 添加一个小时的记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSub"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewSub( clsPartogram_VO p_objSub)
        {
            if (p_objSub == null) return -1;

            long lngRes = 0;
            //Delete Old
            string strSql = @"insert into t_emr_partogram
  (registerid_chr,
   createdate_dat,
   status_int,
   modifydate_dat,
   modifyuserid_chr,
   partogram_int,
   systolicpressure_int,
   diastolicpressure_int,
   uterinecontraction_int,
   checkdate_dat,
   process_vchr,
   process_r_vchr,
   process_xml_vchr,
   sequence_int,
   fetalrhythm_int,
   uterinecontractionmin_int,
   markstatus,
   createuserid_chr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?)";//17

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(18, out objDPArr);
                objDPArr[0].Value = p_objSub.m_strREGISTERID_CHR;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objSub.m_dtmCREATEDATE_DAT;
                objDPArr[2].Value = 1;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objSub.m_dtmMODIFYDATE_DAT;
                objDPArr[4].Value = p_objSub.m_strMODIFYUSERID_CHR;
                objDPArr[5].Value = p_objSub.m_intPARTOGRAM_INT;
                objDPArr[6].Value = p_objSub.m_intSYSTOLICPRESSURE_INT;
                objDPArr[7].Value = p_objSub.m_intDIASTOLICPRESSURE_INT;
                objDPArr[8].Value = p_objSub.m_intUTERINECONTRACTION_INT;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = p_objSub.m_dtmCHECKDATE_DAT;
                objDPArr[10].Value = p_objSub.m_strPROCESS_VCHR;
                objDPArr[11].Value = p_objSub.m_strPROCESS_R_VCHR;
                objDPArr[12].Value = p_objSub.m_strPROCESS_XML_VCHR;
                objDPArr[13].Value = lngSequence;
                objDPArr[14].Value = p_objSub.m_intFETALRHYTHM_INT;
                objDPArr[15].Value = p_objSub.m_intUTERINECONTRACTIONMIN_INT;
                objDPArr[16].Value = p_objSub.m_intMarkStatus;
                objDPArr[17].Value = p_objSub.m_strCREATEUSERID_CHR;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;
                //保存签名集合
                objSign.m_lngAddSign(p_objSub.objSignerArr, lngSequence);

                if (p_objSub.m_ObjPointArr != null)
                {
                    for (int i = 0 ; i < p_objSub.m_ObjPointArr.Length ; i++)
                    {
                        if(p_objSub.m_ObjPointArr[i].m_intSTATUS_INT == 0)
                            m_lngDeletePoint(p_objSub.m_ObjPointArr[i],0);
                        else if (p_objSub.m_ObjPointArr[i].m_intSTATUS_INT == 2)
                        { 
                            m_lngDeletePoint(p_objSub.m_ObjPointArr[i],2);
                            m_lngAddNewPoint(p_objSub.m_ObjPointArr[i]);
                        }
                        else if (p_objSub.m_ObjPointArr[i].m_intPointID_INT == -1)
                            m_lngAddNewPoint(p_objSub.m_ObjPointArr[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 修改一个小时的记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSub"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifySub( clsPartogram_VO p_objSub,int p_intPartogarm)
        {
            if (p_objSub == null || p_intPartogarm < 0 || p_intPartogarm > 24) return -1;

            long lngRes = 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngDeleteSubMain(p_objSub,p_intPartogarm);
                if (lngRes <= 0)
                    return lngRes;
                
                //保存签名集合
                lngRes = m_lngAddNewSub( p_objSub);

            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 删除一个点
        /// </summary>
        /// <param name="p_objPoint"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeletePoint(clsPartogram_Point p_objPoint,int p_intStatus)
        {
            if (p_objPoint == null) return -1;
            long lngRes = 0;
            string strSql = @"update t_emr_partogram_point
   set status_int = ?,
       deactiveddate_dat = ?,
       deactivedoperatorid_chr = ?
 where pointid_int = ?";//4
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_intStatus;
                objDPArr[1].DbType = DbType.DateTime;
                if (p_intStatus == 2)
                {
                    objDPArr[1].Value = DBNull.Value;
                    objDPArr[2].Value = DBNull.Value;
                }
                else
                { 
                    objDPArr[1].Value = p_objPoint.m_dtmDEACTIVEDDATE_DAT;
                    objDPArr[2].Value = p_objPoint.m_strDEACTIVEDOPERATORID_CHR;
                }
                objDPArr[3].Value = p_objPoint.m_intPointID_INT;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);

            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 添加一个点
        /// </summary>
        /// <param name="p_objPoint"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewPoint(clsPartogram_Point p_objPoint)
        {
            if (p_objPoint == null) return -1;
            long lngRes = 0;
            string strSql = @"insert into t_emr_partogram_point
  (registerid_chr,
   createdate_dat,
   status_int,
   modifydate_dat,
   modifyuserid_chr,
   partogram_int,
   pointmin_int,
   pointvalue_int,
   pointid_int,
   poingtype_int,
   checkdate_dat,
   childbearingpoint_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?)";//12
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR", out lngSequence);

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(12, out objDPArr);
                objDPArr[0].Value = p_objPoint.m_strREGISTERID_CHR;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objPoint.m_dtmCREATEDATE_DAT;
                objDPArr[2].Value = 1;
                objDPArr[3].Value = p_objPoint.m_dtmMODIFYDATE_DAT;
                objDPArr[4].Value = p_objPoint.m_strMODIFYUSERID_CHR;
                objDPArr[5].Value = p_objPoint.m_intPARTOGRAM_INT;
                objDPArr[6].Value = p_objPoint.m_intPointMin_INT;
                objDPArr[7].Value = p_objPoint.m_fltPointValue_INT;
                objDPArr[8].Value = lngSequence;
                objDPArr[9].Value = p_objPoint.m_intPointType_INT;
                objDPArr[10].DbType = DbType.DateTime;
                objDPArr[10].Value = p_objPoint.m_dtmCheckDate;
                objDPArr[11].Value = p_objPoint.m_intChildbearingPoint;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);

            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        ///将一个小时的记录置为历史记录
        /// </summary>
        /// <param name="p_objSub"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteSubMain(clsPartogram_VO p_objSub, int p_intPartogarm)
        {
            if (p_objSub == null) return -1;
            long lngRes = 0;
            string strSql = @"update t_emr_partogram
   set status_int = 2
 where registerid_chr = ?
   and partogram_int = ?
   and status_int = 1";//2
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objSub.m_strREGISTERID_CHR;
                objDPArr[1].Value = p_intPartogarm;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 作废一个小时的记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <param name="p_intHour"></param>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_dtmDeactiveDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteHour( 
            string p_strRegisterId, DateTime p_dtmCreatedDate, int p_intHour, string p_strEmpId, DateTime p_dtmDeactiveDate)
        {
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmCreatedDate == DateTime.MinValue || p_intHour < 0) return -1;
            long lngRes = 0;
            string strSql = @"update t_emr_partogram
   set status_int = 0,
   deactiveddate_dat = ?,
   deactivedoperatorid_chr = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and partogram_int = ?
   and status_int = 1";//5
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmDeactiveDate;
                objDPArr[1].Value = p_strEmpId;
                objDPArr[2].Value = p_strRegisterId;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmCreatedDate;
                objDPArr[4].Value = p_intHour;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                if (lngRes > 0)
                {
                    strSql = @"update t_emr_partogram_point
   set status_int = 0,
       deactiveddate_dat = ?,
       deactivedoperatorid_chr = ?
 where registerid_chr = ?
   and partogram_int = ?
   and status_int = 1"; 
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = p_dtmDeactiveDate;
                    objDPArr[1].Value = p_strEmpId;
                    objDPArr[2].Value = p_strRegisterId;
                    objDPArr[3].Value = p_intHour;
                    lngAff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 对一组点记录做处理
        /// m_intSTATUS_INT ＝0为删除
        /// m_intSTATUS_INT ＝ 1 为增加
        /// m_intSTATUS_INT  ＝ 2 为修改（先作废再添加）
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_objPointArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetPointToDb( string p_strEmpId,clsPartogram_Point[] p_objPointArr)
        {
            if (p_objPointArr == null || p_objPointArr.Length ==0) return -1;

            try
            {
                for (int i = 0 ; i < p_objPointArr.Length ; i++)
                {
                    if (p_objPointArr[i].m_intSTATUS_INT == 0)
                        m_lngDeletePoint(p_objPointArr[i],0);
                    else if (p_objPointArr[i].m_intSTATUS_INT == 2)
                    {
                        m_lngDeletePoint(p_objPointArr[i], 2);
                        m_lngAddNewPoint(p_objPointArr[i]);
                    }
                    else if(p_objPointArr[i].m_intPointID_INT == -1)
                        m_lngAddNewPoint(p_objPointArr[i]);
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }

            return 1;
        }
        /// <summary>
        /// 更新第一次打印时间
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateFirstPrintDate( string p_strRegisterId,DateTime p_dtmFirstPrintDate)
        {
            if (string.IsNullOrEmpty(p_strRegisterId)) return -1;
            long lngRes = 0;
            string strSql = @"update t_emr_partogrammain
   set firstprintdate_dat = ?
 where registerid_chr = ?
   and status_int = 1
   and firstprintdate_dat is null";//1
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterId;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);

            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }

            return 1;
        }
    }
}
