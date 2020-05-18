using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService ;
using System.Data;
using weCare.Core.Entity;
using System.Collections; 
namespace com.digitalwave.AssistantToolService.LabResultSvc
{
	/// <summary>
	/// clsLabResult 的摘要说明。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsLabResultSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsLabResultSvc()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		#region 旧
        /// <summary>
        /// 获得整个表的所有内容并且按照需求排列m_lngGetResultInfo
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_objLibResults"></param>
        /// <returns></returns>
		[AutoComplete]
        public long m_lngGetResultInfo(string p_strPatientID, out clsLabCheckResultDate_VO[] p_objLibResults)
		{
			
           p_objLibResults=null;

		   ArrayList arlModifyData = new ArrayList();
           string strSQL = @"SELECT to_date(to_char(t.CHECKDATE_DAT,'yyyy-mm-dd'),'yyyy-mm-dd') as OrderDate,t.*
  FROM T_OPR_EMR_CHECKLAB t
 WHERE t.registerid_chr = ?
 order by OrderDate desc";
			IDataParameter[] objDPArr = null;
			//给参数赋值
			clsHRPTableService objHRPServ =new clsHRPTableService();
			objHRPServ.CreateDatabaseParameter(1,out objDPArr);
            objDPArr[0].DbType = DbType.String;
            objDPArr[0].Value = p_strPatientID.Trim();
			//生成DataTable
			DataTable dtbValue = new DataTable();
			//执行查询，填充结果到DataTable
			long lngRes = 0;
            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //查看DataTable.Rows.Count
                if (lngRes < 0)
                {
                    return (long)enmOperationResult.DB_Fail;//失败
                }
                if (lngRes > 0 && dtbValue.Rows.Count >= 1)
                {
                    DataTable dtDate = dtbValue.DefaultView.ToTable(true, new string[] { "OrderDate" });
                    DataTable dt = dtbValue.DefaultView.ToTable(true, new string[] { "OrderDate", "orderid_vchr", "checkdate_dat", "checkpurpose_vchr", "checkadvice_vchr" });
                    if (dtDate.Rows.Count > 0)
                    {
                        p_objLibResults = new clsLabCheckResultDate_VO[dtDate.Rows.Count];
                        for (int k = 0 ; k < dtDate.Rows.Count ; k++)
                        {
                            p_objLibResults[k] = new clsLabCheckResultDate_VO();
                            p_objLibResults[k].m_StrOrderDate = dtDate.Rows[k]["OrderDate"].ToString();

                            DataRow[] rowGroups = dt.Select("OrderDate = '" + p_objLibResults[k].m_StrOrderDate + "'", "checkdate_dat desc");
                            if (rowGroups.Length > 0)
                            {
                                #region Groups
                                clsLabCheckResultGroup_VO[] ObjGroups = new clsLabCheckResultGroup_VO[rowGroups.Length];
                                int i = 0;
                                foreach (DataRow row in rowGroups)
                                {
                                    int index = i++;
                                    ObjGroups[index] = new clsLabCheckResultGroup_VO();
                                    ObjGroups[index].m_StrOrderid = row["orderid_vchr"].ToString();
                                    ObjGroups[index].m_StrOrderDate = row["checkdate_dat"].ToString();
                                    ObjGroups[index].m_StrOrderName = row["checkpurpose_vchr"].ToString();
                                    ObjGroups[index].m_StrAdvice = row["checkadvice_vchr"].ToString();
                                    strSQL = null;
                                    strSQL = @"orderid_vchr = '" + ObjGroups[index].m_StrOrderid.Trim() + "'";
                                    DataRow[] rowItems = dtbValue.Select(strSQL);
                                    if (rowItems.Length > 0)
                                    {
                                        #region Item
                                        ObjGroups[index].m_ObjItems = new clsLabCheckResultItem_VO[rowItems.Length];
                                        for (int j = 0 ; j < rowItems.Length ; j++)
                                        {
                                            ObjGroups[index].m_ObjItems[j] = new clsLabCheckResultItem_VO();
                                            ObjGroups[index].m_ObjItems[j].m_StrItemId = rowItems[j]["itemid_vchr"].ToString();
                                            ObjGroups[index].m_ObjItems[j].m_StrItemName = rowItems[j]["checkitem_vchr"].ToString();
                                            ObjGroups[index].m_ObjItems[j].m_StrCheckResult = rowItems[j]["checkresult_vchr"].ToString();
                                            ObjGroups[index].m_ObjItems[j].m_StrItemUnit = rowItems[j]["unit_vchr"].ToString();
                                            ObjGroups[index].m_ObjItems[j].m_StrReferRange = rowItems[j]["referrange_vchr"].ToString();
                                            ObjGroups[index].m_ObjItems[j].m_StrAbnormalFlag = rowItems[j]["abnormalflag"].ToString();
                                            ObjGroups[index].m_ObjItems[j].m_StrBacteria = rowItems[j]["bacteria_vchr"].ToString();
                                        }
                                        #endregion Item
                                    }
                                }
                                #endregion Groups
                                p_objLibResults[k].m_ObjGroups = ObjGroups;
                            }

                        }
                    }
                }
            }

            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
        }

        #endregion

        #region 新

        /// <summary>
		/// 获取检验主表信息
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_objLibResults"></param>
		/// <returns></returns>
		[AutoComplete]
        public long m_lngGetLabMainInfo(string p_strPatientID, out clsLabCheckResultMain_VO[] p_objLibMainArr)
		{
			
           p_objLibMainArr=null;
           if (string.IsNullOrEmpty(p_strPatientID)) return -1;

		   ArrayList arlModifyData = new ArrayList();
           string strSQL = @"select seq_lisid_int,
       inpatientid_vchr,
       applyid_vchr,
       checkitems_vchr,
       applydate_dt,
       checkdoc_vchr,
       applydoc_vchr,
       comment_vchr,
       reportdate_dt,
       autidor_vchr,
       checktype_vchr,
       recorddate_dt,
       status_int
  from t_syn_emr_lislabmain
 where inpatientid_vchr = ?
 order by applydate_dt desc";
			IDataParameter[] objDPArr = null;
			//给参数赋值
			clsHRPTableService objHRPServ =new clsHRPTableService();
			objHRPServ.CreateDatabaseParameter(1,out objDPArr);
            objDPArr[0].Value = p_strPatientID.Trim();
			//生成DataTable
			DataTable dtbValue = new DataTable();
			//执行查询，填充结果到DataTable
			long lngRes = 0;
            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //查看DataTable.Rows.Count
                if (lngRes < 0)
                {
                    return (long)enmOperationResult.DB_Fail;//失败
                }
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount >= 1)
                {
                    DataRow objRow = null;
                    p_objLibMainArr = new clsLabCheckResultMain_VO[intRowCount];
                    clsLabCheckResultMain_VO objValue = null;
                    int intTmp = 0;
                    DateTime dtmTmp = DateTime.MinValue;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbValue.Rows[i];
                        objValue = new clsLabCheckResultMain_VO();
                        if(int.TryParse(objRow["status_int"].ToString(),out intTmp))
                            objValue.m_intSTATUS_INT = intTmp;
                        else
                            objValue.m_intSTATUS_INT = -1;
                        if (DateTime.TryParse(objRow["applydate_dt"].ToString(), out dtmTmp))
                            objValue.m_dtmApplyDate = dtmTmp;
                        objValue.m_strApplyDoc = objRow["applydoc_vchr"].ToString();
                        objValue.m_strApplyId = objRow["applyid_vchr"].ToString();
                        objValue.m_strAutidor = objRow["autidor_vchr"].ToString();
                        objValue.m_strCheckDoc = objRow["checkdoc_vchr"].ToString();
                        objValue.m_strCheckItems = objRow["checkitems_vchr"].ToString();
                        objValue.m_strCheckType = objRow["checktype_vchr"].ToString();
                        objValue.m_strComment = objRow["comment_vchr"].ToString();
                        objValue.m_strId = objRow["seq_lisid_int"].ToString();
                        objValue.m_strInpatientId = p_strPatientID;
                        if (DateTime.TryParse(objRow["recorddate_dt"].ToString(), out dtmTmp))
                            objValue.m_dtmRecordDate = dtmTmp;
                        if (DateTime.TryParse(objRow["reportdate_dt"].ToString(), out dtmTmp))
                            objValue.m_dtmReportDate = dtmTmp;
                        p_objLibMainArr[i] = objValue;
                        objValue = null;
                    }
                }
            }

            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
			return lngRes;
        }

        /// <summary>
        /// 获取检验主表信息
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_objLibResults"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLabItemInfo(string p_strApplyId, out clsLabCheckResultSub_VO[] p_objLibMSubArr)
        {

            p_objLibMSubArr = null;
            if (string.IsNullOrEmpty(p_strApplyId)) return -1;

            ArrayList arlModifyData = new ArrayList();
            string strSQL = @"select applyid_vchr,
       itemid_vchr,
       itemname_vchr,
       checkresult_vchr,
       unit_vchr,
       referrange_vchr,
       abnormalflag_int,
       '' senibility_vchr,
       '' identity_vchr,
       0 itemtype
  from t_syn_emr_lisiterms
 where applyid_vchr = ?
   and status_int = 1
union all
select applyid_vchr,
       itemid_vchr,
       itemname_vchr,
       checkresult_vchr,
       '' unit_vchr,
       '' referrange_vchr,
       2 abnormalflag_int,
       senibility_vchr,
       identity_vchr,
       1 itemtype
  from t_syn_emr_lisbacteria
 where applyid_vchr = ?
   and status_int = 1";
            IDataParameter[] objDPArr = null;
            //给参数赋值
            clsHRPTableService objHRPServ = new clsHRPTableService();
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strApplyId.Trim();
            objDPArr[1].Value = p_strApplyId.Trim();
            //生成DataTable
            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable
            long lngRes = 0;
            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                //查看DataTable.Rows.Count
                if (lngRes < 0)
                {
                    return (long)enmOperationResult.DB_Fail;//失败
                }
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes > 0 && intRowCount >= 1)
                {
                    DataRow objRow = null;
                    p_objLibMSubArr = new clsLabCheckResultSub_VO[intRowCount];
                    int intTmp = 0;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objRow = dtbValue.Rows[i];
                        int intType = -1;
                        if (int.TryParse(objRow["itemtype"].ToString(), out intTmp))
                            intType = intTmp;
                        else
                            intType = -1;
                        if (intType == 0)
                        {
                            clsLabCheckResultItems_VO objValue = new clsLabCheckResultItems_VO();
                            objValue.m_intSTATUS_INT = 1;
                            objValue.m_intType = intType;
                            objValue.m_strAPPLYID_VCHR = p_strApplyId.Trim();
                            objValue.m_strCHECKRESULT_VCHR = objRow["checkresult_vchr"].ToString();
                            objValue.m_strITEMID_VCHR = objRow["itemid_vchr"].ToString();
                            objValue.m_strITEMNAME_VCHR = objRow["itemname_vchr"].ToString();
                            objValue.m_strREFERRANGE_VCHR = objRow["referrange_vchr"].ToString();
                            if (int.TryParse(objRow["abnormalflag_int"].ToString(), out intTmp))
                                objValue.m_intABNORMALFLAG_INT = intTmp;
                            else
                                objValue.m_intABNORMALFLAG_INT = -1;
                            objValue.m_strUNIT_VCHR = objRow["unit_vchr"].ToString();
                            p_objLibMSubArr[i] = objValue;
                            objValue = null;
                        }
                        else if (intType == 1)
                        {
                            clsLabCheckResultBacteria_VO objValue2 = new clsLabCheckResultBacteria_VO();
                            objValue2.m_intSTATUS_INT = 1;
                            objValue2.m_intType = intType;
                            objValue2.m_strAPPLYID_VCHR = p_strApplyId.Trim();
                            objValue2.m_strCHECKRESULT_VCHR = objRow["checkresult_vchr"].ToString();
                            objValue2.m_strITEMID_VCHR = objRow["itemid_vchr"].ToString();
                            objValue2.m_strITEMNAME_VCHR = objRow["itemname_vchr"].ToString();
                            objValue2.m_strSENIBILITY_VCHR = objRow["senibility_vchr"].ToString();
                            objValue2.m_strIDENTITY_VCHR = objRow["identity_vchr"].ToString();
                            p_objLibMSubArr[i] = objValue2;
                            objValue2 = null;
                        }
                    }
                }
            }

            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
		#endregion
	}
}
