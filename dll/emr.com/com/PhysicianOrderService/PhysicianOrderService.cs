using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.PhysicianOrderService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsPhysicianOrderService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

 

		#region 提交医嘱表，多条医嘱,刘颖源,2003-6-30 16:02:28
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPhysicianOrderBaseValueArr"></param>
		/// <param name="p_objPhysicianOrderContentValueArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCommitPhysicianOrder(clsPhysicianOrderBaseValue [] p_objPhysicianOrderBaseValueArr,clsPhysicianOrderContentValue [] p_objPhysicianOrderContentValueArr )
		{
			if(p_objPhysicianOrderBaseValueArr ==null || p_objPhysicianOrderContentValueArr ==null)return(0);
			long lngRes=1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                #region 保存主医嘱表 PhysicianOrderBase,刘颖源,2003-6-27 11:25:52
                if (p_objPhysicianOrderBaseValueArr != null && p_objPhysicianOrderBaseValueArr.Length > 0)
                {
                    for (int i = 0; i < p_objPhysicianOrderBaseValueArr.Length; i++)
                    {
                        clsPhysicianOrderBaseValue objPhysicianOrderBaseValue = p_objPhysicianOrderBaseValueArr[i];
                        if (objPhysicianOrderBaseValue != null)
                        {
                            string strSQL = @"insert into physicianorderbase(
                                inpatientid, inpatientdate, opendate, orderid, startdate, startuserid, status, enddate, enduserid, ifconfirm, ifcancel, confirmuserid, orderflag, ifend,  canceldate, confirmdate, actualenddate, actualenduserid, ifperformed, canceluserid 
                                ) values( 
                                ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? 
                                )";

                            #region 防止null,刘颖源,2003-7-2 15:38:20
                            objPhysicianOrderBaseValue.m_strInPatientID = objPhysicianOrderBaseValue.m_strInPatientID == null ? "" : objPhysicianOrderBaseValue.m_strInPatientID;
                            objPhysicianOrderBaseValue.m_strInPatientDate = objPhysicianOrderBaseValue.m_strInPatientDate == null ? "" : objPhysicianOrderBaseValue.m_strInPatientDate;
                            objPhysicianOrderBaseValue.m_strOpenDate = objPhysicianOrderBaseValue.m_strOpenDate == null ? "" : objPhysicianOrderBaseValue.m_strOpenDate;
                            objPhysicianOrderBaseValue.m_strOrderID = objPhysicianOrderBaseValue.m_strOrderID == null ? "" : objPhysicianOrderBaseValue.m_strOrderID;
                            objPhysicianOrderBaseValue.m_strStartDate = objPhysicianOrderBaseValue.m_strStartDate == null ? "" : objPhysicianOrderBaseValue.m_strStartDate;
                            objPhysicianOrderBaseValue.m_strStartUserID = objPhysicianOrderBaseValue.m_strStartUserID == null ? "" : objPhysicianOrderBaseValue.m_strStartUserID;
                            objPhysicianOrderBaseValue.m_strStatus = objPhysicianOrderBaseValue.m_strStatus == null ? "" : objPhysicianOrderBaseValue.m_strStatus;
                            objPhysicianOrderBaseValue.m_strEndDate = objPhysicianOrderBaseValue.m_strEndDate == null ? "" : objPhysicianOrderBaseValue.m_strEndDate;
                            objPhysicianOrderBaseValue.m_strEndUserID = objPhysicianOrderBaseValue.m_strEndUserID == null ? "" : objPhysicianOrderBaseValue.m_strEndUserID;
                            objPhysicianOrderBaseValue.m_strIfConfirm = objPhysicianOrderBaseValue.m_strIfConfirm == null ? "" : objPhysicianOrderBaseValue.m_strIfConfirm;
                            objPhysicianOrderBaseValue.m_strIfCancel = objPhysicianOrderBaseValue.m_strIfCancel == null ? "" : objPhysicianOrderBaseValue.m_strIfCancel;
                            objPhysicianOrderBaseValue.m_strConfirmUserID = objPhysicianOrderBaseValue.m_strConfirmUserID == null ? "" : objPhysicianOrderBaseValue.m_strConfirmUserID;
                            objPhysicianOrderBaseValue.m_strOrderFlag = objPhysicianOrderBaseValue.m_strOrderFlag == null ? "" : objPhysicianOrderBaseValue.m_strOrderFlag;
                            objPhysicianOrderBaseValue.m_strIfEnd = objPhysicianOrderBaseValue.m_strIfEnd == null ? "" : objPhysicianOrderBaseValue.m_strIfEnd;
                            objPhysicianOrderBaseValue.m_strCancelDate = objPhysicianOrderBaseValue.m_strCancelDate == null ? "" : objPhysicianOrderBaseValue.m_strCancelDate;
                            objPhysicianOrderBaseValue.m_strConfirmDate = objPhysicianOrderBaseValue.m_strConfirmDate == null ? "" : objPhysicianOrderBaseValue.m_strConfirmDate;
                            objPhysicianOrderBaseValue.m_strActualEndDate = objPhysicianOrderBaseValue.m_strActualEndDate == null ? "" : objPhysicianOrderBaseValue.m_strActualEndDate;
                            objPhysicianOrderBaseValue.m_strActualEndUserID = objPhysicianOrderBaseValue.m_strActualEndUserID == null ? "" : objPhysicianOrderBaseValue.m_strActualEndUserID;
                            objPhysicianOrderBaseValue.m_strIfPerformed = objPhysicianOrderBaseValue.m_strIfPerformed == null ? "" : objPhysicianOrderBaseValue.m_strIfPerformed;
                            objPhysicianOrderBaseValue.m_strCancelUserID = objPhysicianOrderBaseValue.m_strCancelUserID == null ? "" : objPhysicianOrderBaseValue.m_strCancelUserID;
                            #endregion

                            #region old
                            //														IDataParameter objParameterInPatientID = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterInPatientID.Value = objPhysicianOrderBaseValue.m_strInPatientID;
                            //							IDataParameter objParameterInPatientDate = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterInPatientDate.Value = objPhysicianOrderBaseValue.m_strInPatientDate;
                            //							IDataParameter objParameterOpenDate = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterOpenDate.Value = strCurrentDate;
                            //							IDataParameter objParameterOrderID = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterOrderID.Value = objPhysicianOrderBaseValue.m_strOrderID ;
                            //							IDataParameter objParameterStartDate = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterStartDate.Value = objPhysicianOrderBaseValue.m_strStartDate;
                            //							IDataParameter objParameterStartUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterStartUserID.Value = objPhysicianOrderBaseValue.m_strStartUserID;
                            //							IDataParameter objParameterStatus = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterStatus.Value = "0";
                            //							IDataParameter objParameterEndDate = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterEndDate.Value = objPhysicianOrderBaseValue.m_strEndDate;
                            //							IDataParameter objParameterEndUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterEndUserID.Value = "";
                            //							IDataParameter objParameterIfConfirm = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterIfConfirm.Value = "0";
                            //							IDataParameter objParameterIfCancel = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterIfCancel.Value = "0";
                            //							IDataParameter objParameterConfirmUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterConfirmUserID.Value ="";
                            //							IDataParameter objParameterOrderFlag = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterOrderFlag.Value = objPhysicianOrderBaseValue.m_strOrderFlag;
                            //							IDataParameter objParameterIfEnd = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterIfEnd.Value = "0";
                            //							IDataParameter objParameterCancelDate = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterCancelDate.Value = "1900-1-1";
                            //							IDataParameter objParameterConfirmDate = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterConfirmDate.Value = "1900-1-1";
                            //							IDataParameter objParameterActualEndDate = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterActualEndDate.Value = "1900-1-1";
                            //							IDataParameter objParameterActualEndUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterActualEndUserID.Value = "";
                            //							IDataParameter objParameterIfPerformed = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterIfPerformed.Value = "0";
                            //							IDataParameter objParameterCancelUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //							objParameterCancelUserID.Value = "";
                            //
                            //							IDataParameter[] objPhysicianOrderBaseArr =null; //new IDataParameter[20];
                            //							objPhysicianOrderBaseArr[0]  =  objParameterInPatientID;
                            //							objPhysicianOrderBaseArr[1]  =  objParameterInPatientDate;
                            //							objPhysicianOrderBaseArr[2]  =  objParameterOpenDate;
                            //							objPhysicianOrderBaseArr[3]  =  objParameterOrderID;
                            //							objPhysicianOrderBaseArr[4]  =  objParameterStartDate;
                            //							objPhysicianOrderBaseArr[5]  =  objParameterStartUserID;
                            //							objPhysicianOrderBaseArr[6]  =  objParameterStatus;
                            //							objPhysicianOrderBaseArr[7]  =  objParameterEndDate;
                            //							objPhysicianOrderBaseArr[8]  =  objParameterEndUserID;
                            //							objPhysicianOrderBaseArr[9]  =  objParameterIfConfirm;
                            //							objPhysicianOrderBaseArr[10]  =  objParameterIfCancel;
                            //							objPhysicianOrderBaseArr[11]  =  objParameterConfirmUserID;
                            //							objPhysicianOrderBaseArr[12]  =  objParameterOrderFlag;
                            //							objPhysicianOrderBaseArr[13]  =  objParameterIfEnd;
                            //							objPhysicianOrderBaseArr[14]  =  objParameterCancelDate;
                            //							objPhysicianOrderBaseArr[15]  =  objParameterConfirmDate;
                            //							objPhysicianOrderBaseArr[16]  =  objParameterActualEndDate;
                            //							objPhysicianOrderBaseArr[17]  =  objParameterActualEndUserID;
                            //							objPhysicianOrderBaseArr[18]  =  objParameterIfPerformed;
                            //							objPhysicianOrderBaseArr[19]  =  objParameterCancelUserID;

                            #endregion
                            #region new

                            //定义

                            IDataParameter[] objPhysicianOrderBaseArr = null;//new Oracle.DataAccess.Client.OracleParameter[20];
                            objHRPServ.CreateDatabaseParameter(20, out objPhysicianOrderBaseArr);
                            //赋值
                            objPhysicianOrderBaseArr[0].Value = objPhysicianOrderBaseValue.m_strInPatientID;
                            objPhysicianOrderBaseArr[1].DbType = DbType.DateTime;
                            objPhysicianOrderBaseArr[1].Value = DateTime.Parse(objPhysicianOrderBaseValue.m_strInPatientDate);
                            objPhysicianOrderBaseArr[2].DbType = DbType.DateTime;
                            objPhysicianOrderBaseArr[2].Value = DateTime.Parse(strCurrentDate);
                            objPhysicianOrderBaseArr[3].Value = objPhysicianOrderBaseValue.m_strOrderID;
                            objPhysicianOrderBaseArr[4].Value = objPhysicianOrderBaseValue.m_strStartDate; ;
                            objPhysicianOrderBaseArr[5].Value = objPhysicianOrderBaseValue.m_strStartUserID; ;
                            objPhysicianOrderBaseArr[6].Value = "0";
                            objPhysicianOrderBaseArr[7].DbType = DbType.DateTime;
                            objPhysicianOrderBaseArr[7].Value = DateTime.Parse(objPhysicianOrderBaseValue.m_strEndDate);
                            objPhysicianOrderBaseArr[8].Value = "";
                            objPhysicianOrderBaseArr[9].Value = "0";
                            objPhysicianOrderBaseArr[10].Value = "0";
                            objPhysicianOrderBaseArr[11].Value = "";
                            objPhysicianOrderBaseArr[12].Value = objPhysicianOrderBaseValue.m_strOrderFlag;
                            objPhysicianOrderBaseArr[13].Value = "0";
                            objPhysicianOrderBaseArr[14].DbType = DbType.DateTime;
                            objPhysicianOrderBaseArr[14].Value = DateTime.Parse("1900-1-1");
                            objPhysicianOrderBaseArr[15].DbType = DbType.DateTime;
                            objPhysicianOrderBaseArr[15].Value = DateTime.Parse("1900-1-1");
                            objPhysicianOrderBaseArr[16].DbType = DbType.DateTime;
                            objPhysicianOrderBaseArr[16].Value = DateTime.Parse("1900-1-1");
                            objPhysicianOrderBaseArr[17].Value = "";
                            objPhysicianOrderBaseArr[18].Value = "0";
                            objPhysicianOrderBaseArr[19].Value = "";

                            #endregion

                            long lngEff = 0;
                            lngRes *= objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objPhysicianOrderBaseArr);
                        }
                    }
                }
                #endregion

                #region 保存医嘱内容表 PhysicianOrderContent,刘颖源,2003-6-27 11:34:06
                if (p_objPhysicianOrderContentValueArr != null && p_objPhysicianOrderContentValueArr.Length > 0)
                {
                    for (int i = 0; i < p_objPhysicianOrderContentValueArr.Length; i++)
                    {
                        clsPhysicianOrderContentValue objPhysicianOrderContentValue = p_objPhysicianOrderContentValueArr[i];
                        #region 保存医嘱内容
                        if (objPhysicianOrderContentValue != null)
                        {
                            string strSQL = @"insert into physicianordercontent(
                                inpatientid, inpatientdate, opendate, orderid, suborderid, itemid,  ordertypeid, usageid, itemtypeid, itemdosage, itemstandardid, itemunitid, drugstoreid,  frequencyid, documenttype, documentid, detailid, remark 
                                ) values( 
                                ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? 
                                )";

                            #region 防止null,刘颖源,2003-7-2 15:38:58
                            objPhysicianOrderContentValue.m_strInPatientID = objPhysicianOrderContentValue.m_strInPatientID == null ? "" : objPhysicianOrderContentValue.m_strInPatientID;
                            objPhysicianOrderContentValue.m_strInPatientDate = objPhysicianOrderContentValue.m_strInPatientDate == null ? "" : objPhysicianOrderContentValue.m_strInPatientDate;
                            objPhysicianOrderContentValue.m_strOpenDate = objPhysicianOrderContentValue.m_strOpenDate == null ? "" : objPhysicianOrderContentValue.m_strOpenDate;
                            objPhysicianOrderContentValue.m_strOrderID = objPhysicianOrderContentValue.m_strOrderID == null ? "" : objPhysicianOrderContentValue.m_strOrderID;
                            objPhysicianOrderContentValue.m_strSubOrderID = objPhysicianOrderContentValue.m_strSubOrderID == null ? "" : objPhysicianOrderContentValue.m_strSubOrderID;
                            objPhysicianOrderContentValue.m_strOrderTypeID = objPhysicianOrderContentValue.m_strOrderTypeID == null ? "" : objPhysicianOrderContentValue.m_strOrderTypeID;
                            objPhysicianOrderContentValue.m_strUsageID = objPhysicianOrderContentValue.m_strUsageID == null ? "" : objPhysicianOrderContentValue.m_strUsageID;
                            objPhysicianOrderContentValue.m_strItemID = objPhysicianOrderContentValue.m_strItemID == null ? "" : objPhysicianOrderContentValue.m_strItemID;
                            objPhysicianOrderContentValue.m_strItemTypeID = objPhysicianOrderContentValue.m_strItemTypeID == null ? "" : objPhysicianOrderContentValue.m_strItemTypeID;
                            objPhysicianOrderContentValue.m_strRemark = objPhysicianOrderContentValue.m_strRemark == null ? "" : objPhysicianOrderContentValue.m_strRemark;
                            objPhysicianOrderContentValue.m_strItemDosage = objPhysicianOrderContentValue.m_strItemDosage == null ? "" : objPhysicianOrderContentValue.m_strItemDosage;
                            objPhysicianOrderContentValue.m_strItemStandardID = objPhysicianOrderContentValue.m_strItemStandardID == null ? "" : objPhysicianOrderContentValue.m_strItemStandardID;
                            objPhysicianOrderContentValue.m_strItemUnitID = objPhysicianOrderContentValue.m_strItemUnitID == null ? "" : objPhysicianOrderContentValue.m_strItemUnitID;
                            objPhysicianOrderContentValue.m_strDrugStoreID = objPhysicianOrderContentValue.m_strDrugStoreID == null ? "" : objPhysicianOrderContentValue.m_strDrugStoreID;
                            objPhysicianOrderContentValue.m_strFrequencyID = objPhysicianOrderContentValue.m_strFrequencyID == null ? "" : objPhysicianOrderContentValue.m_strFrequencyID;
                            objPhysicianOrderContentValue.m_strDocumentType = objPhysicianOrderContentValue.m_strDocumentType == null ? "" : objPhysicianOrderContentValue.m_strDocumentType;
                            objPhysicianOrderContentValue.m_strDocumentID = objPhysicianOrderContentValue.m_strDocumentID == null ? "" : objPhysicianOrderContentValue.m_strDocumentID;
                            objPhysicianOrderContentValue.m_strDetailID = objPhysicianOrderContentValue.m_strDetailID == null ? "" : objPhysicianOrderContentValue.m_strDetailID;
                            #endregion
                            if (objPhysicianOrderContentValue.m_strOrderTypeID == "007")			//药
                            {
                                objPhysicianOrderContentValue.m_strDetailID = "00001";
                                objPhysicianOrderContentValue.m_strDocumentID = "";
                                objPhysicianOrderContentValue.m_strDocumentType = "";
                            }
                            else																//非药类
                            {
                                objPhysicianOrderContentValue.m_strItemID = "";					//清除有关药的ID	
                                objPhysicianOrderContentValue.m_strItemTypeID = "";
                                objPhysicianOrderContentValue.m_strItemDosage = "";
                                objPhysicianOrderContentValue.m_strItemUnitID = "";
                            }
                            //如果没有选择频次(如护理)，默认空
                            objPhysicianOrderContentValue.m_strFrequencyID = objPhysicianOrderContentValue.m_strFrequencyID == "" ? "000" : objPhysicianOrderContentValue.m_strFrequencyID;
                            //如果没有用法，默认为空
                            objPhysicianOrderContentValue.m_strUsageID = objPhysicianOrderContentValue.m_strUsageID == "" ? "000" : objPhysicianOrderContentValue.m_strUsageID;
                            #region old
                            //								IDataParameter objParameterInPatientID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterInPatientID.Value = objPhysicianOrderContentValue.m_strInPatientID;
                            //								IDataParameter objParameterInPatientDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterInPatientDate.Value = objPhysicianOrderContentValue.m_strInPatientDate;
                            //								IDataParameter objParameterOpenDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterOpenDate.Value = strCurrentDate ;
                            //								IDataParameter objParameterOrderID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterOrderID.Value = objPhysicianOrderContentValue.m_strOrderID;
                            //								IDataParameter objParameterSubOrderID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterSubOrderID.Value = i.ToString ("00");
                            //								IDataParameter objParameterItemID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterItemID.Value = objPhysicianOrderContentValue.m_strItemID;
                            //								IDataParameter objParameterOrderTypeID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterOrderTypeID.Value = objPhysicianOrderContentValue.m_strOrderTypeID;
                            //								IDataParameter objParameterUsageID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterUsageID.Value = objPhysicianOrderContentValue.m_strUsageID;
                            //								IDataParameter objParameterItemTypeID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterItemTypeID.Value = objPhysicianOrderContentValue.m_strItemTypeID;
                            //								IDataParameter objParameterItemDosage = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterItemDosage.Value = objPhysicianOrderContentValue.m_strItemDosage;
                            //								IDataParameter objParameterItemStandardID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterItemStandardID.Value = objPhysicianOrderContentValue.m_strItemStandardID;
                            //								IDataParameter objParameterItemUnitID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterItemUnitID.Value = objPhysicianOrderContentValue.m_strItemUnitID;
                            //								IDataParameter objParameterDrugStoreID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterDrugStoreID.Value = objPhysicianOrderContentValue.m_strDrugStoreID;
                            //								IDataParameter objParameterFrequencyID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterFrequencyID.Value = objPhysicianOrderContentValue.m_strFrequencyID;
                            //								IDataParameter objParameterDocumentType = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterDocumentType.Value = objPhysicianOrderContentValue.m_strDocumentType;
                            //								IDataParameter objParameterDocumentID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterDocumentID.Value = objPhysicianOrderContentValue.m_strDocumentID;
                            //								IDataParameter objParameterDetailID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterDetailID.Value = objPhysicianOrderContentValue.m_strDetailID;
                            //								IDataParameter objParameterRemark = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterRemark.Value = objPhysicianOrderContentValue.m_strRemark;
                            //									
                            //					
                            //								IDataParameter[] objPhysicianOrderContentArr = new IDataParameter[18];
                            //								objPhysicianOrderContentArr[0]  =  objParameterInPatientID;
                            //								objPhysicianOrderContentArr[1]  =  objParameterInPatientDate;
                            //								objPhysicianOrderContentArr[2]  =  objParameterOpenDate;
                            //								objPhysicianOrderContentArr[3]  =  objParameterOrderID;
                            //								objPhysicianOrderContentArr[4]  =  objParameterSubOrderID;
                            //								objPhysicianOrderContentArr[5]  =  objParameterItemID;
                            //								objPhysicianOrderContentArr[6]  =  objParameterOrderTypeID;
                            //								objPhysicianOrderContentArr[7]  =  objParameterUsageID;
                            //								objPhysicianOrderContentArr[8]  =  objParameterItemTypeID;
                            //								objPhysicianOrderContentArr[9]  =  objParameterItemDosage;
                            //								objPhysicianOrderContentArr[10]  =  objParameterItemStandardID;
                            //								objPhysicianOrderContentArr[11]  =  objParameterItemUnitID;
                            //								objPhysicianOrderContentArr[12]  =  objParameterDrugStoreID;
                            //								objPhysicianOrderContentArr[13]  =  objParameterFrequencyID;
                            //								objPhysicianOrderContentArr[14]  =  objParameterDocumentType;
                            //								objPhysicianOrderContentArr[15]  =  objParameterDocumentID;
                            //								objPhysicianOrderContentArr[16]  =  objParameterDetailID;
                            //								objPhysicianOrderContentArr[17]  =  objParameterRemark;

                            #endregion
                            #region new
                            //定义

                            IDataParameter[] objPhysicianOrderContentArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                            objHRPServ.CreateDatabaseParameter(18, out objPhysicianOrderContentArr);
                            //赋值
                            objPhysicianOrderContentArr[0].Value = objPhysicianOrderContentValue.m_strInPatientID;
                            objPhysicianOrderContentArr[1].DbType = DbType.DateTime;
                            objPhysicianOrderContentArr[1].Value = DateTime.Parse(objPhysicianOrderContentValue.m_strInPatientDate);
                            objPhysicianOrderContentArr[2].DbType = DbType.DateTime;
                            objPhysicianOrderContentArr[2].Value = DateTime.Parse(strCurrentDate);
                            objPhysicianOrderContentArr[3].Value = objPhysicianOrderContentValue.m_strOrderID;
                            objPhysicianOrderContentArr[4].Value = i.ToString("00");
                            objPhysicianOrderContentArr[5].Value = objPhysicianOrderContentValue.m_strItemID;
                            objPhysicianOrderContentArr[6].Value = objPhysicianOrderContentValue.m_strOrderTypeID;
                            objPhysicianOrderContentArr[7].Value = objPhysicianOrderContentValue.m_strUsageID;
                            objPhysicianOrderContentArr[8].Value = objPhysicianOrderContentValue.m_strItemTypeID;
                            objPhysicianOrderContentArr[9].Value = objPhysicianOrderContentValue.m_strItemDosage;
                            objPhysicianOrderContentArr[10].Value = objPhysicianOrderContentValue.m_strItemStandardID;
                            objPhysicianOrderContentArr[11].Value = objPhysicianOrderContentValue.m_strItemUnitID;
                            objPhysicianOrderContentArr[12].Value = objPhysicianOrderContentValue.m_strDrugStoreID;
                            objPhysicianOrderContentArr[13].Value = objPhysicianOrderContentValue.m_strFrequencyID;
                            objPhysicianOrderContentArr[14].Value = objPhysicianOrderContentValue.m_strDocumentType;
                            objPhysicianOrderContentArr[15].Value = objPhysicianOrderContentValue.m_strDocumentID;
                            objPhysicianOrderContentArr[16].Value = objPhysicianOrderContentValue.m_strDetailID;
                            objPhysicianOrderContentArr[17].Value = objPhysicianOrderContentValue.m_strRemark;

                            #endregion

                            long lngEff = 0;
                            lngRes *= objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objPhysicianOrderContentArr);
                        }
                        #endregion
                    }
                }
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
			
		}

		#endregion

		#region 作废一条医嘱,刘颖源,2003-6-30 16:02:28
		/// <summary>
		/// 作废一条医嘱
		/// </summary>
		/// <param name="p_strCancelUserID"></param>
		/// <returns></returns>
		[AutoComplete] 
		public long m_lngCancelPhysicianOrder(string p_strInPatientID,string p_strInPatientDate, string p_strOpenDate,string p_strOrderID, string p_strCancelUserID)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_strInPatientID == null || p_strInPatientID.Trim() == "" || p_strInPatientDate == null || p_strInPatientDate.Trim() == "") return (0);

                string strSQL = @"update physicianorderbase 
                    	set ifcancel=1,canceldate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(DateTime.Now) + ",canceluserid='" + p_strCancelUserID + "' " +
                    " where inpatientid='" + p_strInPatientID + "' and inpatientdate='" + p_strInPatientDate + "' and opendate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strOpenDate) + " and orderid='" + p_strOrderID + "' ";
                lngRes = objHRPServ.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}
		#endregion

		#region 审核多条医嘱,刘颖源,2003-6-30 16:02:28
		/// <summary>
		/// 校对医嘱
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strOrderID"></param>
		/// <param name="p_strConfirmUserID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngConfirmPhysicianOrder(string p_strInPatientID,string p_strInPatientDate, string p_strOpenDate,string p_strOrderID, string p_strConfirmUserID,clsPhysicianOrderAddInValue [] p_objPhysicianOrderAddInValueArr)
		{			
			if(p_strInPatientID ==null || p_strInPatientID.Trim ()=="" || p_strInPatientDate ==null || p_strInPatientDate.Trim ()=="")return(0); 
			long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                #region 保存表 PhysicianOrderAddIn,刘颖源,2003-6-30 11:34:29
                if (p_objPhysicianOrderAddInValueArr != null && p_objPhysicianOrderAddInValueArr.Length > 0)
                {
                    for (int i = 0; i < p_objPhysicianOrderAddInValueArr.Length; i++)
                    {
                        clsPhysicianOrderAddInValue objPhysicianOrderAddInValue = p_objPhysicianOrderAddInValueArr[i];

                        #region 防止null,刘颖源,2003-7-2 15:44:19
                        objPhysicianOrderAddInValue.m_strInPatientID = objPhysicianOrderAddInValue.m_strInPatientID == null ? "" : objPhysicianOrderAddInValue.m_strInPatientID;
                        objPhysicianOrderAddInValue.m_strInPatientDate = objPhysicianOrderAddInValue.m_strInPatientDate == null ? "" : objPhysicianOrderAddInValue.m_strInPatientDate;
                        objPhysicianOrderAddInValue.m_strOpenDate = objPhysicianOrderAddInValue.m_strOpenDate == null ? "" : objPhysicianOrderAddInValue.m_strOpenDate;
                        objPhysicianOrderAddInValue.m_strOrderID = objPhysicianOrderAddInValue.m_strOrderID == null ? "" : objPhysicianOrderAddInValue.m_strOrderID;
                        objPhysicianOrderAddInValue.m_strItemID = objPhysicianOrderAddInValue.m_strItemID == null ? "" : objPhysicianOrderAddInValue.m_strItemID;
                        objPhysicianOrderAddInValue.m_strCreateDate = objPhysicianOrderAddInValue.m_strCreateDate == null ? "" : objPhysicianOrderAddInValue.m_strCreateDate;
                        objPhysicianOrderAddInValue.m_strCreateUserID = objPhysicianOrderAddInValue.m_strCreateUserID == null ? "" : objPhysicianOrderAddInValue.m_strCreateUserID;
                        objPhysicianOrderAddInValue.m_strStatus = objPhysicianOrderAddInValue.m_strStatus == null ? "" : objPhysicianOrderAddInValue.m_strStatus;
                        objPhysicianOrderAddInValue.m_strDeActivedDate = objPhysicianOrderAddInValue.m_strDeActivedDate == null ? "" : objPhysicianOrderAddInValue.m_strDeActivedDate;
                        objPhysicianOrderAddInValue.m_strDeActivedUserID = objPhysicianOrderAddInValue.m_strDeActivedUserID == null ? "" : objPhysicianOrderAddInValue.m_strDeActivedUserID;
                        objPhysicianOrderAddInValue.m_strDescription = objPhysicianOrderAddInValue.m_strDescription == null ? "" : objPhysicianOrderAddInValue.m_strDescription;
                        objPhysicianOrderAddInValue.m_strNumber = objPhysicianOrderAddInValue.m_strNumber == null ? "" : objPhysicianOrderAddInValue.m_strNumber;
                        objPhysicianOrderAddInValue.m_strOpenDate = objPhysicianOrderAddInValue.m_strOpenDate == "" ? p_strOpenDate : objPhysicianOrderAddInValue.m_strOpenDate;
                        #endregion

                        #region 更新主表
                        string strSQL = @"update physicianorderbase 
                            	set ifconfirm=1,confirmdate='" + strCurrentDate + "',confirmuserid='" + p_strConfirmUserID + "' " +
                            " where inpatientid='" + objPhysicianOrderAddInValue.m_strInPatientID + "' and inpatientdate='" + objPhysicianOrderAddInValue.m_strInPatientDate + "' and OpenDate='" + objPhysicianOrderAddInValue.m_strOpenDate + "' and orderid='" + objPhysicianOrderAddInValue.m_strOrderID + "' ";
                        lngRes *= objHRPServ.DoExcute(strSQL);
                        #endregion

                        #region 添加附加物
                        if (objPhysicianOrderAddInValue != null)
                        {
                            strSQL = @"insert into physicianorderaddin(
                                inpatientid, inpatientdate, itemid, opendate, orderid, createdate,  createuserid, status, deactiveddate, deactiveduserid, description, number 
                                ) values( 
                                ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? 
                                )";

                            #region old
                            //											IDataParameter objParameterInPatientID = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterInPatientID.Value = objPhysicianOrderAddInValue.m_strInPatientID;
                            //						IDataParameter objParameterInPatientDate = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterInPatientDate.Value = objPhysicianOrderAddInValue.m_strInPatientDate;
                            //						IDataParameter objParameterItemID = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterItemID.Value = objPhysicianOrderAddInValue.m_strItemID;
                            //						IDataParameter objParameterOpenDate = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterOpenDate.Value = objPhysicianOrderAddInValue.m_strOpenDate;
                            //						IDataParameter objParameterOrderID = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterOrderID.Value = objPhysicianOrderAddInValue.m_strOrderID;
                            //						IDataParameter objParameterCreateDate = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterCreateDate.Value = strCurrentDate;
                            //						IDataParameter objParameterCreateUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterCreateUserID.Value = objPhysicianOrderAddInValue.m_strCreateUserID;
                            //						IDataParameter objParameterStatus = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterStatus.Value = "0";
                            //						IDataParameter objParameterDeActivedDate = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterDeActivedDate.Value = DBNull.Value ;
                            //						IDataParameter objParameterDeActivedUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterDeActivedUserID.Value = objPhysicianOrderAddInValue.m_strDeActivedUserID;
                            //						IDataParameter objParameterDescription = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterDescription.Value = objPhysicianOrderAddInValue.m_strDescription;
                            //						IDataParameter objParameterNumber = new Oracle.DataAccess.Client.OracleParameter();
                            //						objParameterNumber.Value = objPhysicianOrderAddInValue.m_strNumber;
                            //							
                            //			
                            //						IDataParameter[] objPhysicianOrderAddInArr = new IDataParameter[12];
                            //						objPhysicianOrderAddInArr[0]  =  objParameterInPatientID;
                            //						objPhysicianOrderAddInArr[1]  =  objParameterInPatientDate;
                            //						objPhysicianOrderAddInArr[2]  =  objParameterItemID;
                            //						objPhysicianOrderAddInArr[3]  =  objParameterOpenDate;
                            //						objPhysicianOrderAddInArr[4]  =  objParameterOrderID;
                            //						objPhysicianOrderAddInArr[5]  =  objParameterCreateDate;
                            //						objPhysicianOrderAddInArr[6]  =  objParameterCreateUserID;
                            //						objPhysicianOrderAddInArr[7]  =  objParameterStatus;
                            //						objPhysicianOrderAddInArr[8]  =  objParameterDeActivedDate;
                            //						objPhysicianOrderAddInArr[9]  =  objParameterDeActivedUserID;
                            //						objPhysicianOrderAddInArr[10]  =  objParameterDescription;
                            //						objPhysicianOrderAddInArr[11]  =  objParameterNumber;


                            #endregion
                            #region new
                            //定义

                            IDataParameter[] objPhysicianOrderAddInArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                            objHRPServ.CreateDatabaseParameter(12, out objPhysicianOrderAddInArr);
                            //赋值
                            objPhysicianOrderAddInArr[0].Value = objPhysicianOrderAddInValue.m_strInPatientID;
                            objPhysicianOrderAddInArr[1].DbType = DbType.DateTime;
                            objPhysicianOrderAddInArr[1].Value = DateTime.Parse(objPhysicianOrderAddInValue.m_strInPatientDate);
                            objPhysicianOrderAddInArr[2].Value = objPhysicianOrderAddInValue.m_strItemID;
                            objPhysicianOrderAddInArr[3].DbType = DbType.DateTime;
                            objPhysicianOrderAddInArr[3].Value = DateTime.Parse(objPhysicianOrderAddInValue.m_strOpenDate);
                            objPhysicianOrderAddInArr[4].Value = objPhysicianOrderAddInValue.m_strOrderID;
                            objPhysicianOrderAddInArr[5].DbType = DbType.DateTime;
                            objPhysicianOrderAddInArr[5].Value = DateTime.Parse(strCurrentDate);
                            objPhysicianOrderAddInArr[6].Value = objPhysicianOrderAddInValue.m_strCreateUserID;
                            objPhysicianOrderAddInArr[7].Value = "0";
                            objPhysicianOrderAddInArr[8].Value = DBNull.Value;
                            objPhysicianOrderAddInArr[9].Value = objPhysicianOrderAddInValue.m_strDeActivedUserID;
                            objPhysicianOrderAddInArr[10].Value = objPhysicianOrderAddInValue.m_strDescription;
                            objPhysicianOrderAddInArr[11].Value = objPhysicianOrderAddInValue.m_strNumber;
                            #endregion
                            long lngEff = 0;
                            lngRes *= objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objPhysicianOrderAddInArr);
                        }
                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		}
		#endregion

		#region 停止一条医嘱,刘颖源,2003-6-30 16:02:28
		/// <summary>
		/// 停止医嘱
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strOrderID"></param>
		/// <param name="p_strConfirmUserID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngStopPhysicianOrder(string p_strInPatientID,string p_strInPatientDate, string p_strOpenDate,string p_strOrderID, string p_strEndUserID)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_strInPatientID == null || p_strInPatientID.Trim() == "" || p_strInPatientDate == null || p_strInPatientDate.Trim() == "") return (0);
                string strSQL = @"update physicianorderbase 
                	set ifend=1,actualenddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',actualenduserid='" + p_strEndUserID + "' " +
                " where inpatientid='" + p_strInPatientID + "' and inpatientdate='" + p_strInPatientDate + "' and opendate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strOpenDate) + " and orderid='" + p_strOrderID + "' ";
                lngRes = objHRPServ.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}
		#endregion

		#region 获得所有的医嘱类型：如药物，检验，护理等,刘颖源,2003-6-30 16:02:28
		/// <summary>
		/// 获得所有的医嘱类型：如药物，检验，护理等
		/// </summary>
		/// <param name="p_objPhysicianOrderTypeValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllPhysicianOrderType(out clsPhysicianOrderTypeValue[] p_objPhysicianOrderTypeValue)
		{	
			long lngRes = 0;
			p_objPhysicianOrderTypeValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderType,Service层,刘颖源,2003-6-26 16:30:25
                string strSQL = @"select ordertypeid,
       ordertypename,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactiveduserid
  from physicianordertype
 where status = 0
   and ordertypeid > '000'";
                p_objPhysicianOrderTypeValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderTypeValue = new clsPhysicianOrderTypeValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderTypeValue.Length; i++)
                    {
                        p_objPhysicianOrderTypeValue[i] = new clsPhysicianOrderTypeValue();
                        p_objPhysicianOrderTypeValue[i].m_strOrderTypeID = objDataTableResult.Rows[i]["ORDERTYPEID"].ToString();
                        p_objPhysicianOrderTypeValue[i].m_strOrderTypeName = objDataTableResult.Rows[i]["ORDERTYPENAME"].ToString();
                        p_objPhysicianOrderTypeValue[i].m_strCreateDate = objDataTableResult.Rows[i]["CREATEDATE"].ToString();
                        p_objPhysicianOrderTypeValue[i].m_strCreateUserID = objDataTableResult.Rows[i]["CREATEUSERID"].ToString();
                        p_objPhysicianOrderTypeValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objPhysicianOrderTypeValue[i].m_strDeActivedDate = objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
                        p_objPhysicianOrderTypeValue[i].m_strDeActivedUserID = objDataTableResult.Rows[i]["DEACTIVEDUSERID"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}
		#endregion

		#region 获得所有的用法：如滴注，口服,刘颖源,2003-6-30 16:02:28
		/// <summary>
		/// 获得所有的用法：如滴注，口服
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllPhysicianOrderUsage(out clsPhysicianOrderUsageValue [] p_objPhysicianOrderUsageValue)
		{
			long lngRes = 0;
			p_objPhysicianOrderUsageValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderUsage,Service层,刘颖源,2003-6-27 10:45:15
                string strSQL = @"select usageid,
       usagename,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactiveduserid
  from physicianorderusage
 where status = 0
   and usageid > '000'";
                p_objPhysicianOrderUsageValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderUsageValue = new clsPhysicianOrderUsageValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderUsageValue.Length; i++)
                    {
                        p_objPhysicianOrderUsageValue[i] = new clsPhysicianOrderUsageValue();
                        p_objPhysicianOrderUsageValue[i].m_strUsageID = objDataTableResult.Rows[i]["USAGEID"].ToString();
                        p_objPhysicianOrderUsageValue[i].m_strUsageName = objDataTableResult.Rows[i]["USAGENAME"].ToString();
                        p_objPhysicianOrderUsageValue[i].m_strCreateDate = objDataTableResult.Rows[i]["CREATEDATE"].ToString();
                        p_objPhysicianOrderUsageValue[i].m_strCreateUserID = objDataTableResult.Rows[i]["CREATEUSERID"].ToString();
                        p_objPhysicianOrderUsageValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objPhysicianOrderUsageValue[i].m_strDeActivedDate = objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
                        p_objPhysicianOrderUsageValue[i].m_strDeActivedUserID = objDataTableResult.Rows[i]["DEACTIVEDUSERID"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}
		#endregion
		/*
		 *		因为工作需要请允许先屏蔽一下
				#region 获得所有的附加物记录,如针头，导管,刘颖源,2003-6-30 16:02:28
				/// <summary>
				/// 获得所有的附加物记录,如针头，导管
				/// </summary>
				/// <param name="p_objPhysicianOrderAddInInfoValue"></param>
				/// <returns></returns>
				public long m_lngGetAllPhysicianOrderAddInInfo(out clsPhysicianOrderAddInInfoValue [] p_objPhysicianOrderAddInInfoValue)
				{
			
					#region 获得所有表PhysicianOrderAddInInfo,Service层,刘颖源,2003-6-27 10:47:37
					string strSQL = "select * from PhysicianOrderAddInInfo  where status=0 and ItemID>'0000000'";
					p_objPhysicianOrderAddInInfoValue=null;
					DataTable objDataTableResult=new DataTable ();
					long lngRes = objHRPServ.DoGetDataTable (strSQL, ref objDataTableResult);
					if(lngRes > 0 && objDataTableResult.Rows.Count >= 1)
					{
						p_objPhysicianOrderAddInInfoValue = new clsPhysicianOrderAddInInfoValue [objDataTableResult.Rows.Count];
						for(int i=0;i<p_objPhysicianOrderAddInInfoValue.Length ;i++)
						{
							p_objPhysicianOrderAddInInfoValue[i]=new clsPhysicianOrderAddInInfoValue ();
							p_objPhysicianOrderAddInInfoValue[i].m_strItemID=objDataTableResult.Rows[i]["ITEMID"].ToString();
							p_objPhysicianOrderAddInInfoValue[i].m_strItemName=objDataTableResult.Rows[i]["ITEMNAME"].ToString();
							p_objPhysicianOrderAddInInfoValue[i].m_strCreateDate=objDataTableResult.Rows[i]["CREATEDATE"].ToString();
							p_objPhysicianOrderAddInInfoValue[i].m_strCreateUserID=objDataTableResult.Rows[i]["CREATEUSERID"].ToString();
							p_objPhysicianOrderAddInInfoValue[i].m_strStatus=objDataTableResult.Rows[i]["STATUS"].ToString();
							p_objPhysicianOrderAddInInfoValue[i].m_strDeActivedDate=objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
							p_objPhysicianOrderAddInInfoValue[i].m_strDeActiveUserID=objDataTableResult.Rows[i]["DEACTIVEUSERID"].ToString();
						
						}
					}
					#endregion
					return(lngRes );
				}
				#endregion
		*/
		#region 获得频次,刘颖源,2003-6-30 16:02:28
		/// <summary>
		/// 获得频次
		/// </summary>
		/// <param name="p_objPhysicianOrderFrequencyInfoValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllPhysicianOrderFrequencyInfo(out clsPhysicianOrderFrequencyInfoValue[] p_objPhysicianOrderFrequencyInfoValue)
		{
			long lngRes = 0;
			p_objPhysicianOrderFrequencyInfoValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderFrequencyInfo,Service层,刘颖源,2003-6-27 10:51:57
                string strSQL = @"select frequencyid,
       frequencyname,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactiveduserid,
       frequencyperday
  from physicianorderfrequencyinfo
 where status = 0
   and frequencyid > '000'";
                p_objPhysicianOrderFrequencyInfoValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderFrequencyInfoValue = new clsPhysicianOrderFrequencyInfoValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderFrequencyInfoValue.Length; i++)
                    {
                        p_objPhysicianOrderFrequencyInfoValue[i] = new clsPhysicianOrderFrequencyInfoValue();
                        p_objPhysicianOrderFrequencyInfoValue[i].m_strFrequencyID = objDataTableResult.Rows[i]["FREQUENCYID"].ToString();
                        p_objPhysicianOrderFrequencyInfoValue[i].m_strFrequencyName = objDataTableResult.Rows[i]["FREQUENCYNAME"].ToString();
                        p_objPhysicianOrderFrequencyInfoValue[i].m_strCreateDate = objDataTableResult.Rows[i]["CREATEDATE"].ToString();
                        p_objPhysicianOrderFrequencyInfoValue[i].m_strCreateUserID = objDataTableResult.Rows[i]["CREATEUSERID"].ToString();
                        p_objPhysicianOrderFrequencyInfoValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objPhysicianOrderFrequencyInfoValue[i].m_strDeActivedDate = objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
                        p_objPhysicianOrderFrequencyInfoValue[i].m_strDeActivedUserID = objDataTableResult.Rows[i]["DEACTIVEDUSERID"].ToString();
                        p_objPhysicianOrderFrequencyInfoValue[i].m_strFrequencyPerDay = objDataTableResult.Rows[i]["FREQUENCYPERDAY"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}
		#endregion

		#region 获得所有详细信息,刘颖源,2003-6-30 16:02:28
		/// <summary>
		/// 获得所有详细信息
		/// </summary>
		/// <param name="p_enmPhysicianOrderTypeDetail"></param>
		/// <param name="p_objPhysicianOrderTypeDetailValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPhysicianOrderTypeDetail(string p_strOrderTypeID,out clsPhysicianOrderTypeDetailValue [] p_objPhysicianOrderTypeDetailValue)
		{
            string strSQL = @"select detailid,
       ordertypeid,
       detailname,
       detailprice,
       createdate,
       createuserid,
       status,
       deactivedate,
       deactiveuserid
  from physicianordertypedetail where ordertypeid ='" + p_strOrderTypeID + "'";
			long lngRes = 0;
			p_objPhysicianOrderTypeDetailValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderTypeDetail,Service层,刘颖源,2003-7-1 10:03:36
                p_objPhysicianOrderTypeDetailValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderTypeDetailValue = new clsPhysicianOrderTypeDetailValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderTypeDetailValue.Length; i++)
                    {
                        p_objPhysicianOrderTypeDetailValue[i] = new clsPhysicianOrderTypeDetailValue();
                        p_objPhysicianOrderTypeDetailValue[i].m_strDetailID = objDataTableResult.Rows[i]["DETAILID"].ToString();
                        p_objPhysicianOrderTypeDetailValue[i].m_strOrderTypeID = objDataTableResult.Rows[i]["ORDERTYPEID"].ToString();
                        p_objPhysicianOrderTypeDetailValue[i].m_strDetailName = objDataTableResult.Rows[i]["DETAILNAME"].ToString();
                        p_objPhysicianOrderTypeDetailValue[i].m_strDetailPrice = objDataTableResult.Rows[i]["DETAILPRICE"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}


		#endregion

		#region 获得没有经过审核校对的列表(附加物由模板出),刘颖源,2003-6-30 16:07:09
		/// <summary>
		/// 获得没有经过审核校对的列表
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_objPhysicianOrderDefaultListValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPhysicainOrderUnconfirmList(string p_strInPatientID,string p_strInPatientDate,out clsPhysicianOrderDetailListValue  [] p_objPhysicianOrderDetailListValue)
		{
			string strSQL="";
			strSQL = 
				@" select a.inpatientid,a.inpatientdate,a.opendate,a.orderid,a.startdate,a.startuserid,a.enddate,a.enduserid,a.ifconfirm,a.ifcancel,a.orderflag,a.ifperformed,a.actualenddate,a.actualenduserid, 
				 b.suborderid,b.itemid,b.ordertypeid,b.usageid,b.itemtypeid,b.itemstandardid,b.itemdosage,b.itemunitid,b.drugstoreid,b.frequencyid,b.remark,b.documenttype,b.documentid,   
				 c.ordertypename,   
				 d.usagename,   
				 e.frequencyname, 
				 f.itemid as addinitemid, f.number,   
				 g.itemname ,  
				 h.detailname,h.detailprice,   
				 i.medicinename,i.medicineid, 
				 j.medicineoftypeid,   
				 k.medicineoftypename,k.shortname,   
				 l.unitid,l.standardid,l.standardname,    
				 m.unitname, a.status,a.ifend 
				 from physicianorderbase a  
				 left join physicianordercontent b on(a.inpatientid=b.inpatientid and a.inpatientdate=b.inpatientdate and a.opendate=b.opendate and a.orderid=b.orderid) 
				 left join physicianordertype c on (c.status=0 and b.ordertypeid=c.ordertypeid)  
				 left join physicianorderusage d on (d.status=0 and b.usageid=d.usageid) 
				 left join physicianorderfrequencyinfo e on(e.status=0 and b.frequencyid=e.frequencyid)  
				 left join physicianorderusageaddin f on(f.status=0 and d.usageid=f.usageid) 
				 left join physicianorderaddininfo g on(g.status=0 and f.itemid=g.itemid) 
				 left join physicianordertypedetail h on(h.status=0 and b.ordertypeid=h.ordertypeid and b.detailid=h.detailid)  
				 left join medicinemaster1 i on(i.status=0 and b.itemid=i.medicineid)  
				 left join medicineandmedicinetype j on (j.status=0 and i.medicineid=j.medicineid and b.itemtypeid=j.medicineoftypeid)  
				 left join medicineoftype k on (k.status=0 and j.medicineoftypeid=k.medicineoftypeid) 
				 left join medicineofstandard1 l on(l.status=0 and i.medicineid=l.medicineid and j.medicineoftypeid=k.medicineoftypeid and b.itemid=l.medicineid and b.itemtypeid=l.medicineoftypeid and  b.itemstandardid=l.standardid)  
				 left join unitbase m on(b.itemunitid=m.unitid)
				 where a.ifconfirm=0 and a.status=0 and a.inpatientid='" + p_strInPatientID + "' and a.inpatientdate='" + p_strInPatientDate + "' " + 
				" order by a.inpatientid,a.inpatientdate,a.opendate,a.orderid,b.suborderid " ;

			long lngRes = 0;
			p_objPhysicianOrderDetailListValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderDetailList,Service层,刘颖源,2003-7-3 14:29:01
                p_objPhysicianOrderDetailListValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderDetailListValue = new clsPhysicianOrderDetailListValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderDetailListValue.Length; i++)
                    {
                        p_objPhysicianOrderDetailListValue[i] = new clsPhysicianOrderDetailListValue();
                        p_objPhysicianOrderDetailListValue[i].m_strInPatientID = objDataTableResult.Rows[i]["INPATIENTID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strInPatientDate = objDataTableResult.Rows[i]["INPATIENTDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOpenDate = objDataTableResult.Rows[i]["OPENDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderID = objDataTableResult.Rows[i]["ORDERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStartDate = objDataTableResult.Rows[i]["STARTDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStartUserID = objDataTableResult.Rows[i]["STARTUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strEndDate = objDataTableResult.Rows[i]["ENDDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strEndUserID = objDataTableResult.Rows[i]["ENDUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfConfirm = objDataTableResult.Rows[i]["IFCONFIRM"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfCancel = objDataTableResult.Rows[i]["IFCANCEL"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderFlag = objDataTableResult.Rows[i]["ORDERFLAG"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfPerformed = objDataTableResult.Rows[i]["IFPERFORMED"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strActualEndDate = objDataTableResult.Rows[i]["ACTUALENDDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strActualEndUserID = objDataTableResult.Rows[i]["ACTUALENDUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strSubOrderID = objDataTableResult.Rows[i]["SUBORDERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemID = objDataTableResult.Rows[i]["ITEMID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderTypeID = objDataTableResult.Rows[i]["ORDERTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUsageID = objDataTableResult.Rows[i]["USAGEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemTypeID = objDataTableResult.Rows[i]["ITEMTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemStandardID = objDataTableResult.Rows[i]["ITEMSTANDARDID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemDosage = objDataTableResult.Rows[i]["ITEMDOSAGE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemUnitID = objDataTableResult.Rows[i]["ITEMUNITID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDrugStoreID = objDataTableResult.Rows[i]["DRUGSTOREID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strFrequencyID = objDataTableResult.Rows[i]["FREQUENCYID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strRemark = objDataTableResult.Rows[i]["REMARK"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDocumentType = objDataTableResult.Rows[i]["DOCUMENTTYPE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDocumentID = objDataTableResult.Rows[i]["DOCUMENTID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderTypeName = objDataTableResult.Rows[i]["ORDERTYPENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUsageName = objDataTableResult.Rows[i]["USAGENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strFrequencyName = objDataTableResult.Rows[i]["FREQUENCYNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strAddInItemID = objDataTableResult.Rows[i]["ADDINITEMID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strNumber = objDataTableResult.Rows[i]["NUMBER"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemName = objDataTableResult.Rows[i]["ITEMNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDetailName = objDataTableResult.Rows[i]["DETAILNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDetailPrice = objDataTableResult.Rows[i]["DETAILPRICE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineName = objDataTableResult.Rows[i]["MEDICINENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineOfTypeID = objDataTableResult.Rows[i]["MEDICINEOFTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineOfTypeName = objDataTableResult.Rows[i]["MEDICINEOFTYPENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strShortName = objDataTableResult.Rows[i]["SHORTNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUnitID = objDataTableResult.Rows[i]["UNITID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStandardID = objDataTableResult.Rows[i]["STANDARDID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStandardName = objDataTableResult.Rows[i]["STANDARDNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUnitName = objDataTableResult.Rows[i]["UNITNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfEnd = objDataTableResult.Rows[i]["IFEND"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
		}
		#endregion

		#region 得到任意组合的医嘱列表,刘颖源,2003-6-30 16:39:16
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_intOrderFlag"></param>
		/// <param name="p_intIfConfirm"></param>
		/// <param name="p_intIfCancel"></param>
		/// <param name="p_intIfEnd"></param>
		/// <param name="p_intIfPerformed"></param>
		/// <param name="p_strFromDate"></param>
		/// <param name="p_strToDate"></param>
		/// <param name="p_objPhysicianOrderDetailListValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllCombinationPhysicianOrder(string p_strInPatientID,string p_strInPatientDate,
			int p_intOrderFlag,int p_intIfConfirm,int p_intIfCancel,int p_intIfEnd,int p_intIfPerformed, 
			string p_strFromDate,string p_strToDate,
			out clsPhysicianOrderDetailListValue  [] p_objPhysicianOrderDetailListValue)
		{
			string strOrderType=p_intOrderFlag ==4?"": " and OrderFlag=" + p_intOrderFlag.ToString ();
			string strIfConfirm=p_intIfConfirm ==2?"": " and IfConfirm=" + p_intIfConfirm.ToString ();
			string strIfCancel=p_intIfCancel ==2?"":" and IfCancel=" + p_intIfCancel.ToString ();
			string strIfEnd=p_intIfEnd ==2?"":" and IfEnd=" + p_intIfEnd.ToString ();
			string strIfPerform=p_intIfPerformed ==2?"":" and IfPerformed=" + p_intIfPerformed.ToString ();

			string strSQL="";
			strSQL = @"select a.inpatientid,a.inpatientdate,a.opendate,a.orderid,a.startdate,a.startuserid,a.enddate,a.enduserid,a.ifconfirm,a.ifcancel,a.orderflag,a.ifperformed,a.actualenddate,a.actualenduserid,  
			b.suborderid,b.itemid,b.ordertypeid,b.usageid,b.itemtypeid,b.itemstandardid,b.itemdosage,b.itemunitid,b.drugstoreid,b.frequencyid,b.remark,b.documenttype,b.documentid, 
			c.ordertypename,  
			d.usagename,  
			e.frequencyname,
			f.itemid as addinitemid, f.number,  
			g.itemname , 
			h.detailname,h.detailprice,  
			i.medicinename,i.medicineid,
			j.medicineoftypeid,  
			k.medicineoftypename,k.shortname,  
			l.unitid,l.standardid,l.standardname,
			m.unitname, a.status,a.ifend
			from physicianorderbase a  
			left join physicianordercontent b on(a.inpatientid=b.inpatientid and a.inpatientdate=b.inpatientdate and a.opendate=b.opendate and a.orderid=b.orderid) 
			left join physicianordertype c on (c.status=0 and b.ordertypeid=c.ordertypeid) 
			left join physicianorderusage d on (d.status=0 and b.usageid=d.usageid)  
			left join physicianorderfrequencyinfo e on(e.status=0 and b.frequencyid=e.frequencyid)  
			left join physicianorderaddin f on(a.inpatientid=f.inpatientid and a.inpatientdate=f.inpatientdate and a.opendate=f.opendate and a.orderid=f.orderid)  
			left join physicianorderaddininfo g on(g.status=0 and f.itemid=g.itemid)  
			left join physicianordertypedetail h on(h.status=0 and b.ordertypeid=h.ordertypeid and b.detailid=h.detailid)  
			left join medicinemaster1 i on(i.status=0 and b.itemid=i.medicineid) 
			left join medicineandmedicinetype j on (j.status=0 and i.medicineid=j.medicineid and b.itemtypeid=j.medicineoftypeid)  
			left join medicineoftype k on (k.status=0 and j.medicineoftypeid=k.medicineoftypeid)  
			left join medicineofstandard1 l on(l.status=0 and i.medicineid=l.medicineid and j.medicineoftypeid=k.medicineoftypeid and b.itemid=l.medicineid and b.itemtypeid=l.medicineoftypeid and  b.itemstandardid=l.standardid)
			left join unitbase m on(b.itemunitid=m.unitid)" +
				" where a.status=0 and a.inpatientid='" + p_strInPatientID + "' and a.inpatientdate='" + p_strInPatientDate + "' and a.startdate between '" + p_strFromDate + "' and '" + p_strToDate + "' " +  
				strOrderType + strIfConfirm + strIfCancel + strIfEnd + strIfPerform +
				" order by a.inpatientid,a.inpatientdate,a.opendate,a.orderid,b.suborderid " + 
				" ";
			long lngRes = 0;
			p_objPhysicianOrderDetailListValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderDetailList,Service层,刘颖源,2003-7-3 14:29:01
                p_objPhysicianOrderDetailListValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderDetailListValue = new clsPhysicianOrderDetailListValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderDetailListValue.Length; i++)
                    {
                        p_objPhysicianOrderDetailListValue[i] = new clsPhysicianOrderDetailListValue();
                        p_objPhysicianOrderDetailListValue[i].m_strInPatientID = objDataTableResult.Rows[i]["INPATIENTID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strInPatientDate = objDataTableResult.Rows[i]["INPATIENTDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOpenDate = objDataTableResult.Rows[i]["OPENDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderID = objDataTableResult.Rows[i]["ORDERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStartDate = objDataTableResult.Rows[i]["STARTDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStartUserID = objDataTableResult.Rows[i]["STARTUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strEndDate = objDataTableResult.Rows[i]["ENDDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strEndUserID = objDataTableResult.Rows[i]["ENDUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfConfirm = objDataTableResult.Rows[i]["IFCONFIRM"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfCancel = objDataTableResult.Rows[i]["IFCANCEL"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderFlag = objDataTableResult.Rows[i]["ORDERFLAG"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfPerformed = objDataTableResult.Rows[i]["IFPERFORMED"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strActualEndDate = objDataTableResult.Rows[i]["ACTUALENDDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strActualEndUserID = objDataTableResult.Rows[i]["ACTUALENDUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strSubOrderID = objDataTableResult.Rows[i]["SUBORDERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemID = objDataTableResult.Rows[i]["ITEMID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderTypeID = objDataTableResult.Rows[i]["ORDERTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUsageID = objDataTableResult.Rows[i]["USAGEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemTypeID = objDataTableResult.Rows[i]["ITEMTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemStandardID = objDataTableResult.Rows[i]["ITEMSTANDARDID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemDosage = objDataTableResult.Rows[i]["ITEMDOSAGE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemUnitID = objDataTableResult.Rows[i]["ITEMUNITID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDrugStoreID = objDataTableResult.Rows[i]["DRUGSTOREID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strFrequencyID = objDataTableResult.Rows[i]["FREQUENCYID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strRemark = objDataTableResult.Rows[i]["REMARK"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDocumentType = objDataTableResult.Rows[i]["DOCUMENTTYPE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDocumentID = objDataTableResult.Rows[i]["DOCUMENTID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderTypeName = objDataTableResult.Rows[i]["ORDERTYPENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUsageName = objDataTableResult.Rows[i]["USAGENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strFrequencyName = objDataTableResult.Rows[i]["FREQUENCYNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strAddInItemID = objDataTableResult.Rows[i]["ADDINITEMID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strNumber = objDataTableResult.Rows[i]["NUMBER"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemName = objDataTableResult.Rows[i]["ITEMNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDetailName = objDataTableResult.Rows[i]["DETAILNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDetailPrice = objDataTableResult.Rows[i]["DETAILPRICE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineName = objDataTableResult.Rows[i]["MEDICINENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineOfTypeID = objDataTableResult.Rows[i]["MEDICINEOFTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineOfTypeName = objDataTableResult.Rows[i]["MEDICINEOFTYPENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strShortName = objDataTableResult.Rows[i]["SHORTNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUnitID = objDataTableResult.Rows[i]["UNITID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStandardID = objDataTableResult.Rows[i]["STANDARDID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStandardName = objDataTableResult.Rows[i]["STANDARDNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUnitName = objDataTableResult.Rows[i]["UNITNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfEnd = objDataTableResult.Rows[i]["IFEND"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }		//返回
			return lngRes;
		}
		#endregion

		#region 执行医嘱,刘颖源,2003-6-30 16:07:09
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPhysicianOrderPerformedListArr"></param>
		/// <param name="p_objPhysicianOrderPerformedAddInArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngPerformPhysicianOrders(clsPhysicianOrderPerformedListValue [] p_objPhysicianOrderPerformedListArr,clsPhysicianOrderPerformedAddInValue [] p_objPhysicianOrderPerformedAddInArr)
		{
			long lngRes=1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                #region 更新医嘱主表和医嘱执行表
                if (p_objPhysicianOrderPerformedListArr != null && p_objPhysicianOrderPerformedListArr.Length > 0)
                {

                    for (int i = 0; i < p_objPhysicianOrderPerformedListArr.Length; i++)
                    {
                        clsPhysicianOrderPerformedListValue objPhysicianOrderPerformedListValue = p_objPhysicianOrderPerformedListArr[i];
                        #region 更新主表
                        string strSQL = "update physicianorderbase " +
                            "	set ifperformed=1 " +
                            " where inpatientid='" + objPhysicianOrderPerformedListValue.m_strInPatientID + "' and inpatientdate='" + objPhysicianOrderPerformedListValue.m_strInPatientDate + "' and opendate='" + objPhysicianOrderPerformedListValue.m_strOpenDate + "' and orderid='" + objPhysicianOrderPerformedListValue.m_strOrderID + "' ";

                        lngRes *= objHRPServ.DoExcute(strSQL);
                        #endregion

                        #region 保存医嘱表 PhysicianOrderPerformedList,刘颖源,2003-7-2 11:00:52
                        if (objPhysicianOrderPerformedListValue != null)
                        {
                            strSQL = @"insert into physicianorderperformedlist(
                                inpatientid, inpatientdate, opendate, orderid, activedate, performdate, performuserid 
                                 ) values( 
                                ?, ?, ?, ?, ?, ?, ? 
                                )";
                            #region old
                            //								IDataParameter objParameterInPatientID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterInPatientID.Value = objPhysicianOrderPerformedListValue.m_strInPatientID;
                            //								IDataParameter objParameterInPatientDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterInPatientDate.Value = objPhysicianOrderPerformedListValue.m_strInPatientDate;
                            //								IDataParameter objParameterOpenDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterOpenDate.Value = objPhysicianOrderPerformedListValue.m_strOpenDate;
                            //								IDataParameter objParameterOrderID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterOrderID.Value = objPhysicianOrderPerformedListValue.m_strOrderID;
                            //								IDataParameter objParameterActiveDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterActiveDate.Value =  strCurrentDate ;
                            //								IDataParameter objParameterPerformDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterPerformDate.Value = objPhysicianOrderPerformedListValue.m_strPerformDate;
                            //								IDataParameter objParameterPerformUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterPerformUserID.Value = objPhysicianOrderPerformedListValue.m_strPerformUserID;
                            //								
                            //					
                            //								IDataParameter[] objPhysicianOrderPerformedListArr = new IDataParameter[7];
                            //								objPhysicianOrderPerformedListArr[0]  =  objParameterInPatientID;
                            //								objPhysicianOrderPerformedListArr[1]  =  objParameterInPatientDate;
                            //								objPhysicianOrderPerformedListArr[2]  =  objParameterOpenDate;
                            //								objPhysicianOrderPerformedListArr[3]  =  objParameterOrderID;
                            //								objPhysicianOrderPerformedListArr[4]  =  objParameterActiveDate;
                            //								objPhysicianOrderPerformedListArr[5]  =  objParameterPerformDate;
                            //								objPhysicianOrderPerformedListArr[6]  =  objParameterPerformUserID;
                            #endregion
                            #region new

                            IDataParameter[] objPhysicianOrderPerformedListArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                            objHRPServ.CreateDatabaseParameter(7, out objPhysicianOrderPerformedListArr);
                            objPhysicianOrderPerformedListArr[0].Value = objPhysicianOrderPerformedListValue.m_strInPatientID;
                            objPhysicianOrderPerformedListArr[1].DbType = DbType.DateTime;
                            objPhysicianOrderPerformedListArr[1].Value = DateTime.Parse(objPhysicianOrderPerformedListValue.m_strInPatientDate);
                            objPhysicianOrderPerformedListArr[2].DbType = DbType.DateTime;
                            objPhysicianOrderPerformedListArr[2].Value = DateTime.Parse(objPhysicianOrderPerformedListValue.m_strOpenDate);
                            objPhysicianOrderPerformedListArr[3].Value = objPhysicianOrderPerformedListValue.m_strOrderID;
                            objPhysicianOrderPerformedListArr[4].DbType = DbType.DateTime;
                            objPhysicianOrderPerformedListArr[4].Value = DateTime.Parse(strCurrentDate);
                            objPhysicianOrderPerformedListArr[5].DbType = DbType.DateTime;
                            objPhysicianOrderPerformedListArr[5].Value = DateTime.Parse(objPhysicianOrderPerformedListValue.m_strPerformDate);
                            objPhysicianOrderPerformedListArr[6].Value = objPhysicianOrderPerformedListValue.m_strPerformUserID;
                            #endregion


                            long lngEff = 0;
                            lngRes *= objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objPhysicianOrderPerformedListArr);
                        }
                        #endregion

                    }

                }
                #endregion

                #region	更新医嘱执行附加物详细表
                if (p_objPhysicianOrderPerformedAddInArr != null)
                {
                    for (int i = 0; i < p_objPhysicianOrderPerformedAddInArr.Length; i++)
                    {
                        clsPhysicianOrderPerformedAddInValue objPhysicianOrderPerformedAddInValue = p_objPhysicianOrderPerformedAddInArr[i];
                        #region 保存表 PhysicianOrderPerformedAddIn,刘颖源,2003-7-3 15:15:26
                        if (objPhysicianOrderPerformedAddInValue != null)
                        {
                            string strSQL = @"insert into physicianorderperformedaddin(
                                inpatientid, inpatientdate, opendate, orderid, activedate, itemid,  createdate, createuserid, status, deactiveddate, deactiveuserid, number 
                               ) values(
                                ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?
                                )";
                            #region old
                            //	IDataParameter objParameterInPatientID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterInPatientID.Value = objPhysicianOrderPerformedAddInValue.m_strInPatientID;
                            //								IDataParameter objParameterInPatientDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterInPatientDate.Value = objPhysicianOrderPerformedAddInValue.m_strInPatientDate;
                            //								IDataParameter objParameterOpenDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterOpenDate.Value = objPhysicianOrderPerformedAddInValue.m_strOpenDate;
                            //								IDataParameter objParameterOrderID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterOrderID.Value = objPhysicianOrderPerformedAddInValue.m_strOrderID;
                            //								IDataParameter objParameterActiveDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterActiveDate.Value = strCurrentDate ;
                            //								IDataParameter objParameterItemID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterItemID.Value = objPhysicianOrderPerformedAddInValue.m_strItemID;
                            //								IDataParameter objParameterCreateDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterCreateDate.Value = strCurrentDate;
                            //								IDataParameter objParameterCreateUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterCreateUserID.Value = objPhysicianOrderPerformedAddInValue.m_strCreateUserID;
                            //								IDataParameter objParameterStatus = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterStatus.Value = "0";
                            //								IDataParameter objParameterDeActivedDate = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterDeActivedDate.Value = DBNull.Value ;
                            //								IDataParameter objParameterDeActiveUserID = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterDeActiveUserID.Value = objPhysicianOrderPerformedAddInValue.m_strDeActiveUserID;
                            //								IDataParameter objParameterNumber = new Oracle.DataAccess.Client.OracleParameter();
                            //								objParameterNumber.Value = objPhysicianOrderPerformedAddInValue.m_strNumber;
                            //									
                            //					
                            //								IDataParameter[] objPhysicianOrderPerformedAddInArr = new IDataParameter[12];
                            //								objPhysicianOrderPerformedAddInArr[0]  =  objParameterInPatientID;
                            //								objPhysicianOrderPerformedAddInArr[1]  =  objParameterInPatientDate;
                            //								objPhysicianOrderPerformedAddInArr[2]  =  objParameterOpenDate;
                            //								objPhysicianOrderPerformedAddInArr[3]  =  objParameterOrderID;
                            //								objPhysicianOrderPerformedAddInArr[4]  =  objParameterActiveDate;
                            //								objPhysicianOrderPerformedAddInArr[5]  =  objParameterItemID;
                            //								objPhysicianOrderPerformedAddInArr[6]  =  objParameterCreateDate;
                            //								objPhysicianOrderPerformedAddInArr[7]  =  objParameterCreateUserID;
                            //								objPhysicianOrderPerformedAddInArr[8]  =  objParameterStatus;
                            //								objPhysicianOrderPerformedAddInArr[9]  =  objParameterDeActivedDate;
                            //								objPhysicianOrderPerformedAddInArr[10]  =  objParameterDeActiveUserID;
                            //								objPhysicianOrderPerformedAddInArr[11]  =  objParameterNumber;
                            #endregion
                            #region new
                            //定义

                            IDataParameter[] objPhysicianOrderPerformedAddInArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                            objHRPServ.CreateDatabaseParameter(12, out objPhysicianOrderPerformedAddInArr);
                            //赋值
                            objPhysicianOrderPerformedAddInArr[0].Value = objPhysicianOrderPerformedAddInValue.m_strInPatientID;
                            objPhysicianOrderPerformedAddInArr[1].Value = objPhysicianOrderPerformedAddInValue.m_strInPatientDate;
                            objPhysicianOrderPerformedAddInArr[2].Value = objPhysicianOrderPerformedAddInValue.m_strOpenDate;
                            objPhysicianOrderPerformedAddInArr[3].Value = objPhysicianOrderPerformedAddInValue.m_strOrderID;
                            objPhysicianOrderPerformedAddInArr[4].Value = strCurrentDate;
                            objPhysicianOrderPerformedAddInArr[5].Value = objPhysicianOrderPerformedAddInValue.m_strItemID;
                            objPhysicianOrderPerformedAddInArr[6].Value = strCurrentDate;
                            objPhysicianOrderPerformedAddInArr[7].Value = objPhysicianOrderPerformedAddInValue.m_strCreateUserID;
                            objPhysicianOrderPerformedAddInArr[8].Value = "0";
                            objPhysicianOrderPerformedAddInArr[9].Value = DBNull.Value;
                            objPhysicianOrderPerformedAddInArr[10].Value = objPhysicianOrderPerformedAddInValue.m_strDeActiveUserID;
                            objPhysicianOrderPerformedAddInArr[11].Value = objPhysicianOrderPerformedAddInValue.m_strNumber;
                            #endregion

                            long lngEff = 0;
                            lngRes *= objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objPhysicianOrderPerformedAddInArr);
                        }
                        #endregion
                    }
                }
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
		
		}
		#endregion
		
		#region 由拼音码获得药名称,刘颖源,2003-7-1 12:17:58
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strPingYinCode"></param>
		/// <param name="p_objPhysicianOrderMedicineNameValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedicineNameByPingYinCode(string p_strPingYinCode, out clsPhysicianOrderMedicineNameValue  [] p_objPhysicianOrderMedicineNameValue)
		{
			long lngRes = 0;
			p_objPhysicianOrderMedicineNameValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderMedicineName,Service层,刘颖源,2003-7-1 12:17:40
                string strSQL = @"select medicineid, medicinename, pycode, latinname, englishname  
                     from medicinemaster1 
                     where status=0 and pycode like '" + p_strPingYinCode + "%' order by medicineid";

                p_objPhysicianOrderMedicineNameValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderMedicineNameValue = new clsPhysicianOrderMedicineNameValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderMedicineNameValue.Length; i++)
                    {
                        p_objPhysicianOrderMedicineNameValue[i] = new clsPhysicianOrderMedicineNameValue();
                        p_objPhysicianOrderMedicineNameValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strMedicineName = objDataTableResult.Rows[i]["MEDICINENAME"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strPYCode = objDataTableResult.Rows[i]["PYCODE"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strLatinName = objDataTableResult.Rows[i]["LATINNAME"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strEnglishName = objDataTableResult.Rows[i]["ENGLISHNAME"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
			

		}
		#endregion

		#region 由拉丁码获得药名称,刘颖源,2003-7-2 12:17:58
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strCode"></param>
		/// <param name="p_objPhysicianOrderMedicineNameValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedicineNameByLatinCode(string p_strCode, out clsPhysicianOrderMedicineNameValue  [] p_objPhysicianOrderMedicineNameValue)
		{
			long lngRes = 0;
			p_objPhysicianOrderMedicineNameValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderMedicineName,Service层,刘颖源,2003-7-1 12:17:40
                string strSQL = @"select medicineid, medicinename, pycode, latinname, englishname  
                     from medicinemaster1 
                     where status=0 and latinname like '" + p_strCode + "%' order by medicineid";

                p_objPhysicianOrderMedicineNameValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderMedicineNameValue = new clsPhysicianOrderMedicineNameValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderMedicineNameValue.Length; i++)
                    {
                        p_objPhysicianOrderMedicineNameValue[i] = new clsPhysicianOrderMedicineNameValue();
                        p_objPhysicianOrderMedicineNameValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strMedicineName = objDataTableResult.Rows[i]["MEDICINENAME"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strPYCode = objDataTableResult.Rows[i]["PYCODE"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strLatinName = objDataTableResult.Rows[i]["LATINNAME"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strEnglishName = objDataTableResult.Rows[i]["ENGLISHNAME"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		

		}
		#endregion

		#region 由英语获得药名称,刘颖源,2003-7-2 12:17:58
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strCode"></param>
		/// <param name="p_objPhysicianOrderMedicineNameValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedicineNameByEnglishCode(string p_strCode, out clsPhysicianOrderMedicineNameValue  [] p_objPhysicianOrderMedicineNameValue)
		{
			long lngRes = 0;
			p_objPhysicianOrderMedicineNameValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderMedicineName,Service层,刘颖源,2003-7-1 12:17:40
                string strSQL = @"select medicineid, medicinename, pycode, latinname, englishname +
                     from medicinemaster1 
                     where status=0 and englishname like '" + p_strCode + "%' order by medicineid";

                p_objPhysicianOrderMedicineNameValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderMedicineNameValue = new clsPhysicianOrderMedicineNameValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderMedicineNameValue.Length; i++)
                    {
                        p_objPhysicianOrderMedicineNameValue[i] = new clsPhysicianOrderMedicineNameValue();
                        p_objPhysicianOrderMedicineNameValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strMedicineName = objDataTableResult.Rows[i]["MEDICINENAME"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strPYCode = objDataTableResult.Rows[i]["PYCODE"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strLatinName = objDataTableResult.Rows[i]["LATINNAME"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strEnglishName = objDataTableResult.Rows[i]["ENGLISHNAME"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			

		}
		#endregion

		#region 由药ID获得药名称,刘颖源,2003-7-2 12:17:58
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strCode"></param>
		/// <param name="p_objPhysicianOrderMedicineNameValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedicineNameByMedicineID(string p_strCode, out clsPhysicianOrderMedicineNameValue  [] p_objPhysicianOrderMedicineNameValue)
		{
			long lngRes = 0;
			p_objPhysicianOrderMedicineNameValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderMedicineName,Service层,刘颖源,2003-7-1 12:17:40
                string strSQL = @"select medicineid, medicinename, pycode, latinname, englishname 
                     from medicinemaster1 
                     where status=0 and medicineid like '" + p_strCode + "%' order by medicineid";

                p_objPhysicianOrderMedicineNameValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderMedicineNameValue = new clsPhysicianOrderMedicineNameValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderMedicineNameValue.Length; i++)
                    {
                        p_objPhysicianOrderMedicineNameValue[i] = new clsPhysicianOrderMedicineNameValue();
                        p_objPhysicianOrderMedicineNameValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strMedicineName = objDataTableResult.Rows[i]["MEDICINENAME"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strPYCode = objDataTableResult.Rows[i]["PYCODE"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strLatinName = objDataTableResult.Rows[i]["LATINNAME"].ToString();
                        p_objPhysicianOrderMedicineNameValue[i].m_strEnglishName = objDataTableResult.Rows[i]["ENGLISHNAME"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		

		}
		#endregion

		#region 获得药剂型,刘颖源,2003-7-1 13:17:12
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strMedicineID"></param>
		/// <param name="p_objPhysicianOrderMedicineTypeValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedicineType(string p_strMedicineID, out clsPhysicianOrderMedicineTypeValue [] p_objPhysicianOrderMedicineTypeValue)
		{
			long lngRes = 0;
			p_objPhysicianOrderMedicineTypeValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderMedicineType,Service层,刘颖源,2003-7-1 13:18:30
                string strSQL = @"select medicineandmedicinetype.medicineid,  
                          medicineandmedicinetype.medicineoftypeid,  
                          medicineoftype.medicineoftypename, medicineoftype.shortname 
                     from medicineandmedicinetype inner join 
                          medicineoftype on  
                           medicineandmedicinetype.medicineoftypeid = medicineoftype.medicineoftypeid 
                     where (medicineoftype.status = 0 and medicineandmedicinetype.medicineid='" + p_strMedicineID + "')";
                p_objPhysicianOrderMedicineTypeValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderMedicineTypeValue = new clsPhysicianOrderMedicineTypeValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderMedicineTypeValue.Length; i++)
                    {
                        p_objPhysicianOrderMedicineTypeValue[i] = new clsPhysicianOrderMedicineTypeValue();
                        p_objPhysicianOrderMedicineTypeValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderMedicineTypeValue[i].m_strMedicineOfTypeID = objDataTableResult.Rows[i]["MEDICINEOFTYPEID"].ToString();
                        p_objPhysicianOrderMedicineTypeValue[i].m_strMedicineOfTypeName = objDataTableResult.Rows[i]["MEDICINEOFTYPENAME"].ToString();
                        p_objPhysicianOrderMedicineTypeValue[i].m_strShortName = objDataTableResult.Rows[i]["SHORTNAME"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			

		}
		#endregion

		#region 获得药的规格,刘颖源,2003-7-1 13:22:33
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strMedicineID"></param>
		/// <param name="p_strMedicineTypeID"></param>
		/// <param name="p_objPhysicianOrderMedicineStandardValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedicineStandard(string p_strMedicineID,string p_strMedicineTypeID, out clsPhysicianOrderMedicineStandardValue [] p_objPhysicianOrderMedicineStandardValue)
		{
			long lngRes = 0;
			p_objPhysicianOrderMedicineStandardValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderMedicineStandard,Service层,刘颖源,2003-7-1 13:23:43
                string strSQL = @"select medicineofstandard1.medicineid,  
                           medicineofstandard1.medicineoftypeid, medicineofstandard1.standardid,   
                           medicineofstandard1.standardname, medicineofstandard1.unitid,  
                           unitbase.unitname 
                     from medicineofstandard1 inner join 
                           unitbase on medicineofstandard1.unitid = unitbase.unitid 
                     where medicineofstandard1.medicineid='" + p_strMedicineID + "' and medicineofstandard1.medicineoftypeid='" + p_strMedicineTypeID + "'";
                p_objPhysicianOrderMedicineStandardValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderMedicineStandardValue = new clsPhysicianOrderMedicineStandardValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderMedicineStandardValue.Length; i++)
                    {
                        p_objPhysicianOrderMedicineStandardValue[i] = new clsPhysicianOrderMedicineStandardValue();
                        p_objPhysicianOrderMedicineStandardValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderMedicineStandardValue[i].m_strMedicineOfTypeID = objDataTableResult.Rows[i]["MEDICINEOFTYPEID"].ToString();
                        p_objPhysicianOrderMedicineStandardValue[i].m_strStandardID = objDataTableResult.Rows[i]["STANDARDID"].ToString();
                        p_objPhysicianOrderMedicineStandardValue[i].m_strStandardName = objDataTableResult.Rows[i]["STANDARDNAME"].ToString();
                        p_objPhysicianOrderMedicineStandardValue[i].m_strUnitID = objDataTableResult.Rows[i]["UNITID"].ToString();
                        p_objPhysicianOrderMedicineStandardValue[i].m_strUnitName = objDataTableResult.Rows[i]["UNITNAME"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}
		#endregion

		#region 获得所有待执行的医嘱,刘颖源,2003-7-2 16:26:27
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objPhysicianOrderDetailListValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetWaitToPerformOrders(string p_strInPatientID,string p_strInPatientDate,out clsPhysicianOrderDetailListValue  [] p_objPhysicianOrderDetailListValue)
		{
			string strSQL="";
			strSQL =@"select a.inpatientid,a.inpatientdate,a.opendate,a.orderid,a.startdate,a.startuserid,a.enddate,a.enduserid,a.ifconfirm,a.ifcancel,a.orderflag,a.ifperformed,a.actualenddate,a.actualenduserid,  
					b.suborderid,b.itemid,b.ordertypeid,b.usageid,b.itemtypeid,b.itemstandardid,b.itemdosage,b.itemunitid,b.drugstoreid,b.frequencyid,b.remark,b.documenttype,b.documentid, 
					c.ordertypename,  
					d.usagename,  
					e.frequencyname,
					f.itemid as addinitemid, f.number,  
					g.itemname , 
					h.detailname,h.detailprice,  
					i.medicinename,i.medicineid,
					j.medicineoftypeid,  
					k.medicineoftypename,k.shortname,  
					l.unitid,l.standardid,l.standardname,
					m.unitname, a.status,a.ifend
					from physicianorderbase a  
					left join physicianordercontent b on(a.inpatientid=b.inpatientid and a.inpatientdate=b.inpatientdate and a.opendate=b.opendate and a.orderid=b.orderid) 
					left join physicianordertype c on (c.status=0 and b.ordertypeid=c.ordertypeid) 
					left join physicianorderusage d on (d.status=0 and b.usageid=d.usageid)  
					left join physicianorderfrequencyinfo e on(e.status=0 and b.frequencyid=e.frequencyid)  
					left join physicianorderaddin f on(a.inpatientid=f.inpatientid and a.inpatientdate=f.inpatientdate and a.opendate=f.opendate and a.orderid=f.orderid)  
					left join physicianorderaddininfo g on(g.status=0 and f.itemid=g.itemid)  
					left join physicianordertypedetail h on(h.status=0 and b.ordertypeid=h.ordertypeid and b.detailid=h.detailid)  
					left join medicinemaster1 i on(i.status=0 and b.itemid=i.medicineid) 
					left join medicineandmedicinetype j on (j.status=0 and i.medicineid=j.medicineid and b.itemtypeid=j.medicineoftypeid)  
					left join medicineoftype k on (k.status=0 and j.medicineoftypeid=k.medicineoftypeid)  
					left join medicineofstandard1 l on(l.status=0 and i.medicineid=l.medicineid and j.medicineoftypeid=k.medicineoftypeid and b.itemid=l.medicineid and b.itemtypeid=l.medicineoftypeid and  b.itemstandardid=l.standardid)
					left join unitbase m on(b.itemunitid=m.unitid)
					where a.status=0 and 
					a.inpatientid='" + p_strInPatientID +  @"' and 
					a.inpatientdate='" + p_strInPatientDate + @"' and a.ifconfirm=1 and 
					(
					(a.ifperformed=0 and a.ifcancel=0 and a.ifend=0) or	--所有没有执行的医嘱
					(a.ifperformed=1 and a.ifcancel=0 and a.ifend=0 and (a.orderflag=0 or a.orderflag=2))		--所有已执行过一次，但没有停的长期医嘱

					)
					order by a.inpatientid,a.inpatientdate,a.opendate,a.orderid,b.suborderid";

			long lngRes = 0;
			p_objPhysicianOrderDetailListValue=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderDetailList,Service层,刘颖源,2003-7-3 14:29:01
                p_objPhysicianOrderDetailListValue = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderDetailListValue = new clsPhysicianOrderDetailListValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderDetailListValue.Length; i++)
                    {
                        p_objPhysicianOrderDetailListValue[i] = new clsPhysicianOrderDetailListValue();
                        p_objPhysicianOrderDetailListValue[i].m_strInPatientID = objDataTableResult.Rows[i]["INPATIENTID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strInPatientDate = objDataTableResult.Rows[i]["INPATIENTDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOpenDate = objDataTableResult.Rows[i]["OPENDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderID = objDataTableResult.Rows[i]["ORDERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStartDate = objDataTableResult.Rows[i]["STARTDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStartUserID = objDataTableResult.Rows[i]["STARTUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strEndDate = objDataTableResult.Rows[i]["ENDDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strEndUserID = objDataTableResult.Rows[i]["ENDUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfConfirm = objDataTableResult.Rows[i]["IFCONFIRM"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfCancel = objDataTableResult.Rows[i]["IFCANCEL"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderFlag = objDataTableResult.Rows[i]["ORDERFLAG"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfPerformed = objDataTableResult.Rows[i]["IFPERFORMED"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strActualEndDate = objDataTableResult.Rows[i]["ACTUALENDDATE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strActualEndUserID = objDataTableResult.Rows[i]["ACTUALENDUSERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strSubOrderID = objDataTableResult.Rows[i]["SUBORDERID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemID = objDataTableResult.Rows[i]["ITEMID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderTypeID = objDataTableResult.Rows[i]["ORDERTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUsageID = objDataTableResult.Rows[i]["USAGEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemTypeID = objDataTableResult.Rows[i]["ITEMTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemStandardID = objDataTableResult.Rows[i]["ITEMSTANDARDID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemDosage = objDataTableResult.Rows[i]["ITEMDOSAGE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemUnitID = objDataTableResult.Rows[i]["ITEMUNITID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDrugStoreID = objDataTableResult.Rows[i]["DRUGSTOREID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strFrequencyID = objDataTableResult.Rows[i]["FREQUENCYID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strRemark = objDataTableResult.Rows[i]["REMARK"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDocumentType = objDataTableResult.Rows[i]["DOCUMENTTYPE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDocumentID = objDataTableResult.Rows[i]["DOCUMENTID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strOrderTypeName = objDataTableResult.Rows[i]["ORDERTYPENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUsageName = objDataTableResult.Rows[i]["USAGENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strFrequencyName = objDataTableResult.Rows[i]["FREQUENCYNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strAddInItemID = objDataTableResult.Rows[i]["ADDINITEMID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strNumber = objDataTableResult.Rows[i]["NUMBER"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strItemName = objDataTableResult.Rows[i]["ITEMNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDetailName = objDataTableResult.Rows[i]["DETAILNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strDetailPrice = objDataTableResult.Rows[i]["DETAILPRICE"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineName = objDataTableResult.Rows[i]["MEDICINENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineID = objDataTableResult.Rows[i]["MEDICINEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineOfTypeID = objDataTableResult.Rows[i]["MEDICINEOFTYPEID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strMedicineOfTypeName = objDataTableResult.Rows[i]["MEDICINEOFTYPENAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strShortName = objDataTableResult.Rows[i]["SHORTNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUnitID = objDataTableResult.Rows[i]["UNITID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStandardID = objDataTableResult.Rows[i]["STANDARDID"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStandardName = objDataTableResult.Rows[i]["STANDARDNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strUnitName = objDataTableResult.Rows[i]["UNITNAME"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objPhysicianOrderDetailListValue[i].m_strIfEnd = objDataTableResult.Rows[i]["IFEND"].ToString();

                    }
                }
                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}
		#endregion




		//-------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strUsageID"></param>
		/// <param name="p_strItemID"></param>
		/// <param name="p_objPOUAIV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetUsageAddIn ( string p_strUsageID, string p_strItemID, out clsPhysicianOrderUsageAddInValue[] p_objPOUAIV )
		{
			p_objPOUAIV = null;
			string strComm = "select * from physicianorderusageaddin where status = 0 ";
			if ( p_strUsageID != null )
				strComm += " and usageid = '" + p_strUsageID + "'";
			if ( p_strItemID != null )
				strComm += " and itemid = '" + p_strItemID + "'";
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable objDT = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strComm, ref objDT);
                if (lngRes > 0 && objDT.Rows.Count >= 1)
                {
                    p_objPOUAIV = new clsPhysicianOrderUsageAddInValue[objDT.Rows.Count];
                    for (int i = 0; i < objDT.Rows.Count; i++)
                    {
                        p_objPOUAIV[i] = new clsPhysicianOrderUsageAddInValue();
                        p_objPOUAIV[i].m_strUsageID = objDT.Rows[i]["USAGEID"].ToString();
                        p_objPOUAIV[i].m_strItemID = objDT.Rows[i]["ITEMID"].ToString();
                        p_objPOUAIV[i].m_strCreateDate = objDT.Rows[i]["CREATEDATE"].ToString();
                        p_objPOUAIV[i].m_strCreateUserID = objDT.Rows[i]["CREATEUSERID"].ToString();
                        p_objPOUAIV[i].m_strDeActivedDate = objDT.Rows[i]["DEACTIVEDDATE"].ToString();
                        p_objPOUAIV[i].m_strDeActivedUserID = objDT.Rows[i]["DEACTIVEDUSERID"].ToString();
                        p_objPOUAIV[i].m_strNumber = objDT.Rows[i]["NUMBER"].ToString();
                        p_objPOUAIV[i].m_strDescription = objDT.Rows[i]["DESCRIPTION"].ToString();

                    }
                    return objDT.Rows.Count;
                }
                return 0;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPOUAIV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngInsertUsageAddIn ( clsPhysicianOrderUsageAddInValue p_objPOUAIV )
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                p_objPOUAIV.m_strCreateDate = DateTime.Now.ToString();

                string strComm = @" insert into physicianorderusageaddin ( usageid, itemid, createdate, createuserid, status, deactiveddate, deactiveduserid, number, description ) values ( '" + p_objPOUAIV.m_strUsageID + "', '" + p_objPOUAIV.m_strItemID + "', '" + DateTime.Now.ToString() + "', '" + p_objPOUAIV.m_strCreateUserID + "', " + "0" + ", '" + p_objPOUAIV.m_strDeActivedDate + "', '" + p_objPOUAIV.m_strDeActivedUserID + "', '" + p_objPOUAIV.m_strNumber + "', '" + p_objPOUAIV.m_strDescription + "' )";

                lngRes = objHRPServ.DoExcute(strComm);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPOUAIV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteUsageAddIn ( clsPhysicianOrderUsageAddInValue p_objPOUAIV )
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strComm = " update physicianorderusageaddin set status = '1', deactiveddate = '" + DateTime.Now.ToString() + "' where usageid = '" + p_objPOUAIV.m_strUsageID + "' and " + " itemid = '" + p_objPOUAIV.m_strItemID + "'";

                lngRes = objHRPServ.DoExcute(strComm);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPOUAIV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAlterUsageAddIn ( clsPhysicianOrderUsageAddInValue p_objPOUAIV )
		{
			if ( m_lngDeleteUsageAddIn( p_objPOUAIV ) == 0 )
				return 0;
			if ( m_lngInsertUsageAddIn ( p_objPOUAIV ) == 0 )
				return 0;
			return 1;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPOUArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllOrderUsage ( out clsPhysicianOrderUsageValue[] p_objPOUArr )
		{
			p_objPOUArr = null;

            string strComm = @"select usageid,
       usagename,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactiveduserid
  from physicianorderusage
 where status = 0";
			DataTable objDT = new DataTable();
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.DoGetDataTable(strComm, ref objDT);
                if (lngRes > 0 && objDT.Rows.Count >= 1)
                {
                    p_objPOUArr = new clsPhysicianOrderUsageValue[objDT.Rows.Count];
                    for (int i = 0; i < objDT.Rows.Count; i++)
                    {
                        p_objPOUArr[i] = new clsPhysicianOrderUsageValue();
                        p_objPOUArr[i].m_strUsageID = objDT.Rows[i]["USAGEID"].ToString();
                        p_objPOUArr[i].m_strUsageName = objDT.Rows[i]["USAGENAME"].ToString();
                        p_objPOUArr[i].m_strCreateDate = objDT.Rows[i]["CREATEDATE"].ToString();
                        p_objPOUArr[i].m_strCreateUserID = objDT.Rows[i]["CREATEUSERID"].ToString();
                    }
                    return objDT.Rows.Count;
                }
                return 0;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}

		#region 写的和roy的重复了，先隐藏， EGO
        [AutoComplete]
		public long m_lngGetAllPhysicianOrderAddInInfo ( out clsPhysicianOrderAddInInfoValue[] p_objPOAArr )
		{
			p_objPOAArr = null;
			string strComm = @"select itemid,
       itemname,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactiveuserid
  from physicianorderaddininfo
 where status = 0";
			DataTable objDT = new DataTable();


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                 lngRes = objHRPServ.DoGetDataTable(strComm, ref objDT);
                if (lngRes > 0 && objDT.Rows.Count >= 1)
                {
                    p_objPOAArr = new clsPhysicianOrderAddInInfoValue[objDT.Rows.Count];
                    for (int i = 0; i < objDT.Rows.Count; i++)
                    {
                        p_objPOAArr[i] = new clsPhysicianOrderAddInInfoValue();
                        p_objPOAArr[i].m_strItemID = objDT.Rows[i]["ITEMID"].ToString();
                        p_objPOAArr[i].m_strItemName = objDT.Rows[i]["ITEMNAME"].ToString();
                        p_objPOAArr[i].m_strCreateDate = objDT.Rows[i]["CREATEDATE"].ToString();
                        p_objPOAArr[i].m_strCreateUserID = objDT.Rows[i]["CREATEUSERID"].ToString();
                    }
                    return objDT.Rows.Count;
                } 
            }
            finally
            {
                //objHRPServ.Dispose();

            }

            return lngRes;
		}
		#endregion



		/// <summary>
		/// //////////////////////////////////  here we go ///////
		/// </summary>
		/// <param name="p_objPOBM"></param>
		/// <param name="p_objPOCMArr"></param>
		/// <returns></returns>




		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objUBVArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllUnitBase ( out clsUnitBaseValue [] p_objUBVArr )
		{

			long lngRes = 0;
			p_objUBVArr=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表UnitBase,Service层,刘颖源,2003-8-7 9:10:58
                string strSQL = @"select unitid,
       unitname,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactiveuserid
  from unitbase ";
                p_objUBVArr = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objUBVArr = new clsUnitBaseValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objUBVArr.Length; i++)
                    {
                        p_objUBVArr[i] = new clsUnitBaseValue();
                        p_objUBVArr[i].m_strUnitID = objDataTableResult.Rows[i]["UNITID"].ToString();
                        p_objUBVArr[i].m_strUnitName = objDataTableResult.Rows[i]["UNITNAME"].ToString();
                        p_objUBVArr[i].m_strCreateDate = objDataTableResult.Rows[i]["CREATEDATE"].ToString();
                        p_objUBVArr[i].m_strCreateUserID = objDataTableResult.Rows[i]["CREATEUSERID"].ToString();
                        p_objUBVArr[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();
                        p_objUBVArr[i].m_strDeActivedDate = objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
                        p_objUBVArr[i].m_strDeActiveUserID = objDataTableResult.Rows[i]["DEACTIVEUSERID"].ToString();

                    }
                }
                #endregion

                lngRes = p_objUBVArr.Length;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPOTSVC"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngAddPhysicianOrderTemplateSetCase( clsPhysicianOrderTemplateSetValueCase p_objPOTSVC )
		{
			if ( ( p_objPOTSVC == null ) || ( p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr == null ) || ( p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[0] == null ) || ( p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[0].objPhysicianOrderTemplateContentValueArr == null ) || ( p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[0].objPhysicianOrderTemplateContentValueArr[0] == null ) )
				return 0;
			if  ( p_objPOTSVC == null )
				return 0;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                int count = 0;
                new clsHRPTableService().lngGenerateID(7, "Set_ID", "PhysicianOrderTemplateSet", out p_objPOTSVC.m_strSet_ID);
                p_objPOTSVC.m_strActiveDate = DateTime.Now.ToString();
                p_objPOTSVC.m_strStatus = ((int)0).ToString();

                if ((m_lngAddPhysicianOrderTemplateSet(p_objPOTSVC)) == 0)
                    return 0;
                if ((p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr != null))
                {
                    for (int i = 0; i < p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr.Length; i++)
                    {
                        if (p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i] != null)
                        {
                            p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strSet_ID = p_objPOTSVC.m_strSet_ID;
                            p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strActiveDate = p_objPOTSVC.m_strActiveDate;
                            p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strOrderID = i.ToString();
                            if ((m_lngAddPhysicianOrderTemplate(p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i])) == 0)
                                return 0;
                            if (p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr != null)
                            {
                                for (int j = 0; j < p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr.Length; j++)
                                {
                                    if (p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j] != null)
                                    {
                                        p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j].m_strSet_ID = p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strSet_ID;
                                        p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j].m_strActiveDate = p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strActiveDate;
                                        p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j].m_strOrderID = p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strOrderID;
                                        p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j].m_strSubOrderID = j.ToString();
                                        if ((m_lngAddPhysicianOrderTemplateContent(p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j])) == 0)
                                            return 0;
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
                lngRes = count;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPhysicianOrderTemplateSetValueCase"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeletePhysicianOrderTemplateSetCase( clsPhysicianOrderTemplateSetValueCase p_objPhysicianOrderTemplateSetValueCase )
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strComm = "update physicianordertemplateset set status = 1 where set_id = '" + p_objPhysicianOrderTemplateSetValueCase.m_strSet_ID + "' and activedate = '" + p_objPhysicianOrderTemplateSetValueCase.m_strActiveDate + "'";
                lngRes = objHRPServ.DoExcute(strComm);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPOTSVC"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdatePhysicianOrderTemplateSetCase( clsPhysicianOrderTemplateSetValueCase p_objPOTSVC	)
		{
			if ( p_objPOTSVC == null )
				return 0;

			int count = 0;
			
			p_objPOTSVC.m_strActiveDate = DateTime.Now.ToString();
			p_objPOTSVC.m_strStatus = ( (int) 0 ).ToString();
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if ((m_lngAddPhysicianOrderTemplateSet(p_objPOTSVC)) == 0)
                    return 0;
                if (p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr != null)
                {
                    for (int i = 0; i < p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr.Length; i++)
                    {
                        if ((p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i] != null))
                        {
                            p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strSet_ID = p_objPOTSVC.m_strSet_ID;
                            p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strActiveDate = p_objPOTSVC.m_strActiveDate;
                            p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strOrderID = i.ToString();
                            if ((m_lngAddPhysicianOrderTemplate(p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i])) == 0)
                                return 0;
                            if (p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr != null)
                            {
                                for (int j = 0; j < p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr.Length; j++)
                                {
                                    if (p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j] != null)
                                    {
                                        p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j].m_strSet_ID = p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strSet_ID;
                                        p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j].m_strActiveDate = p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strActiveDate;
                                        p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j].m_strOrderID = p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].m_strOrderID;
                                        p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j].m_strSubOrderID = j.ToString();
                                        if ((m_lngAddPhysicianOrderTemplateContent(p_objPOTSVC.objPhysicianOrderTemplateValueCaseArr[i].objPhysicianOrderTemplateContentValueArr[j])) == 0)
                                            return 0;
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
                lngRes = count;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPhysicianOrderTemplayeSetValueCaseArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllPhysicianOrderTemplateSetCase( out clsPhysicianOrderTemplateSetValueCase[] p_objPhysicianOrderTemplayeSetValueCaseArr )
		{
			p_objPhysicianOrderTemplayeSetValueCaseArr = null;
			long lngRes = 0;
             try
            {
                clsPhysicianOrderTemplateSetValue[] objPhysicianOrderTemplayeSetValueArr;

                lngRes = m_lngGetAllPhysicianOrderTemplateSet(out objPhysicianOrderTemplayeSetValueArr);

                if ((lngRes == 0) || (objPhysicianOrderTemplayeSetValueArr == null))
                    return 0;

                // get OrderTemplate
                #region give the value from objPhysicianOrderTemplayeSetValue to p_objPhysicianOrderTemplayeSetValueCaseArr

                p_objPhysicianOrderTemplayeSetValueCaseArr = new clsPhysicianOrderTemplateSetValueCase[objPhysicianOrderTemplayeSetValueArr.Length];

                for (int i = 0; i < objPhysicianOrderTemplayeSetValueArr.Length; i++)
                {
                    p_objPhysicianOrderTemplayeSetValueCaseArr[i] = new clsPhysicianOrderTemplateSetValueCase();

                    p_objPhysicianOrderTemplayeSetValueCaseArr[i].m_strActiveDate = objPhysicianOrderTemplayeSetValueArr[i].m_strActiveDate;
                    p_objPhysicianOrderTemplayeSetValueCaseArr[i].m_strBeginDate = objPhysicianOrderTemplayeSetValueArr[i].m_strBeginDate;
                    p_objPhysicianOrderTemplayeSetValueCaseArr[i].m_strCreatorID = objPhysicianOrderTemplayeSetValueArr[i].m_strCreatorID;
                    p_objPhysicianOrderTemplayeSetValueCaseArr[i].m_strEndDate = objPhysicianOrderTemplayeSetValueArr[i].m_strEndDate;
                    p_objPhysicianOrderTemplayeSetValueCaseArr[i].m_strName = objPhysicianOrderTemplayeSetValueArr[i].m_strName;
                    p_objPhysicianOrderTemplayeSetValueCaseArr[i].m_strSet_ID = objPhysicianOrderTemplayeSetValueArr[i].m_strSet_ID;
                    p_objPhysicianOrderTemplayeSetValueCaseArr[i].m_strStatus = objPhysicianOrderTemplayeSetValueArr[i].m_strStatus;
                }
                #endregion

                int count = 0;

                #region now set each objPhysicianOrderTemplateValueCaseArr in p_objPhysicianOrderTemplayeSetValueCaseArr
                for (int j = 0; j < p_objPhysicianOrderTemplayeSetValueCaseArr.Length; j++)
                {
                    if (p_objPhysicianOrderTemplayeSetValueCaseArr[j] != null)
                    {
                        m_lngFill_PhysicianOrderTemplateSetValueCase_WithAll_PhysicianOrderTemplateValueCase(ref p_objPhysicianOrderTemplayeSetValueCaseArr[j]);
                        if (p_objPhysicianOrderTemplayeSetValueCaseArr[j].objPhysicianOrderTemplateValueCaseArr != null)
                        {
                            for (int k = 0; k < p_objPhysicianOrderTemplayeSetValueCaseArr[j].objPhysicianOrderTemplateValueCaseArr.Length; k++)
                            {
                                if (p_objPhysicianOrderTemplayeSetValueCaseArr[j].objPhysicianOrderTemplateValueCaseArr[k] != null)
                                {
                                    m_lngFill_PhysicianOrderTemplateValueCase_WithAll_PhysicianOrderTemplateContent(ref p_objPhysicianOrderTemplayeSetValueCaseArr[j].objPhysicianOrderTemplateValueCaseArr[k]);

                                    if (p_objPhysicianOrderTemplayeSetValueCaseArr[j].objPhysicianOrderTemplateValueCaseArr[k].objPhysicianOrderTemplateContentValueArr != null)
                                    {
                                        count += p_objPhysicianOrderTemplayeSetValueCaseArr[j].objPhysicianOrderTemplateValueCaseArr[k].objPhysicianOrderTemplateContentValueArr.Length;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                lngRes = count;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
 			//返回
			return lngRes;

			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPhysicianOrderTemplayeSetValueCase"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngFill_PhysicianOrderTemplateSetValueCase_WithAll_PhysicianOrderTemplateValueCase ( ref clsPhysicianOrderTemplateSetValueCase p_objPhysicianOrderTemplayeSetValueCase)
		{
			clsPhysicianOrderTemplateSetValue objPOTSV = new clsPhysicianOrderTemplateSetValue();
			objPOTSV.m_strSet_ID = p_objPhysicianOrderTemplayeSetValueCase.m_strSet_ID;
			objPOTSV.m_strActiveDate = p_objPhysicianOrderTemplayeSetValueCase.m_strActiveDate;
			long lngRes = 0;
			try
			{
				clsPhysicianOrderTemplateValue[] objPOTVArr;

				long eff = m_lngGetPhysicianOrderTemplate( objPOTSV, out objPOTVArr );
				if ( ( eff == 0 ) || ( objPOTVArr == null ))
					return 0;
				
				#region give the value from objPOTVArr to p_objPhysicianOrderTemplayeSetValueCase.objPhysicianOrderTemplateValueCaseArr 
				if ( objPOTVArr.Length != 0 )
				{
					p_objPhysicianOrderTemplayeSetValueCase.objPhysicianOrderTemplateValueCaseArr = new clsPhysicianOrderTemplateValueCase [ objPOTVArr.Length ];
					for (int i = 0 ; i < objPOTVArr.Length ; i ++ )
					{
						p_objPhysicianOrderTemplayeSetValueCase.objPhysicianOrderTemplateValueCaseArr[i] = new clsPhysicianOrderTemplateValueCase();
						p_objPhysicianOrderTemplayeSetValueCase.objPhysicianOrderTemplateValueCaseArr[i].m_strActiveDate = objPOTVArr[i].m_strActiveDate;
						p_objPhysicianOrderTemplayeSetValueCase.objPhysicianOrderTemplateValueCaseArr[i].m_strOrderFlag = objPOTVArr[i].m_strOrderFlag;
						p_objPhysicianOrderTemplayeSetValueCase.objPhysicianOrderTemplateValueCaseArr[i].m_strOrderID = objPOTVArr[i].m_strOrderID;
						p_objPhysicianOrderTemplayeSetValueCase.objPhysicianOrderTemplateValueCaseArr[i].m_strSet_ID = objPOTVArr[i].m_strSet_ID;
					}
				}
				lngRes= objPOTVArr.Length ;
				#endregion

			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPhysicianOrderTemplateValueCase"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngFill_PhysicianOrderTemplateValueCase_WithAll_PhysicianOrderTemplateContent ( ref clsPhysicianOrderTemplateValueCase p_objPhysicianOrderTemplateValueCase  )
		{
			clsPhysicianOrderTemplateValue objPOTV = new clsPhysicianOrderTemplateValue();
			objPOTV.m_strActiveDate = p_objPhysicianOrderTemplateValueCase.m_strActiveDate;
			objPOTV.m_strOrderID = p_objPhysicianOrderTemplateValueCase.m_strOrderID;
			objPOTV.m_strSet_ID = p_objPhysicianOrderTemplateValueCase.m_strSet_ID;
			long lngRes = 0;
			try
			{
				clsPhysicianOrderTemplateContentValue[] objPOTCVArr ;
				long eff = m_lngGetPhysicianOrderTemplateContent( objPOTV, out objPOTCVArr );
				
				if ( ( eff == 0 ) || ( objPOTCVArr == null ) )
					return 0;

				#region fill p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr with value
				p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr =  new clsPhysicianOrderTemplateContentValue [objPOTCVArr.Length];
				if ( objPOTCVArr.Length != 0 )
				{
					for ( int i = 0; i < objPOTCVArr.Length ; i ++ )
					{
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i] = new clsPhysicianOrderTemplateContentValue();
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strActiveDate = objPOTCVArr[i].m_strActiveDate;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strDetailID = objPOTCVArr[i].m_strDetailID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strDocumentID = objPOTCVArr[i].m_strDocumentID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strDocumentType = objPOTCVArr[i].m_strDocumentType;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strFrequencyID = objPOTCVArr[i].m_strFrequencyID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strItemDosage = objPOTCVArr[i].m_strItemDosage;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strItemID = objPOTCVArr[i].m_strItemID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strItemStandardID = objPOTCVArr[i].m_strItemStandardID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strItemTypeID = objPOTCVArr[i].m_strItemTypeID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strOrderID = objPOTCVArr[i].m_strOrderID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strOrderTypeID = objPOTCVArr[i].m_strOrderTypeID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strRemark = objPOTCVArr[i].m_strRemark;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strSet_ID = objPOTCVArr[i].m_strSet_ID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strSubOrderID = objPOTCVArr[i].m_strSubOrderID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strItemUnitID = objPOTCVArr[i].m_strItemUnitID;
						p_objPhysicianOrderTemplateValueCase.objPhysicianOrderTemplateContentValueArr[i].m_strUsageID = objPOTCVArr[i].m_strUsageID;
					}
				}
				#endregion

				lngRes= objPOTCVArr.Length;
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
			
		}

		//     child functions
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objPhysicianOrderTemplayeSetValueArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllPhysicianOrderTemplateSet ( out clsPhysicianOrderTemplateSetValue[] objPhysicianOrderTemplayeSetValueArr )
		{
			long lngRes = 0;
			objPhysicianOrderTemplayeSetValueArr=null;
			objPhysicianOrderTemplayeSetValueArr=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderTemplateSet,Service层,刘颖源,2003-8-12 13:32:55
                string strSQL = @"select a.set_id,
       a.activedate,
       a.name,
       a.begindate,
       a.enddate,
       a.creatorid,
       a.status
  from physicianordertemplateset a,
       (select set_id, max(activedate) activedate
          from physicianordertemplateset
         group by set_id) b
 where a.activedate = b.activedate
   and a.set_id = b.set_id
   and a.status = 0";
                objPhysicianOrderTemplayeSetValueArr = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    objPhysicianOrderTemplayeSetValueArr = new clsPhysicianOrderTemplateSetValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < objPhysicianOrderTemplayeSetValueArr.Length; i++)
                    {
                        objPhysicianOrderTemplayeSetValueArr[i] = new clsPhysicianOrderTemplateSetValue();
                        objPhysicianOrderTemplayeSetValueArr[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        objPhysicianOrderTemplayeSetValueArr[i].m_strActiveDate = objDataTableResult.Rows[i]["ACTIVEDATE"].ToString();
                        objPhysicianOrderTemplayeSetValueArr[i].m_strName = objDataTableResult.Rows[i]["NAME"].ToString();
                        objPhysicianOrderTemplayeSetValueArr[i].m_strBeginDate = objDataTableResult.Rows[i]["BEGINDATE"].ToString();
                        objPhysicianOrderTemplayeSetValueArr[i].m_strEndDate = objDataTableResult.Rows[i]["ENDDATE"].ToString();
                        objPhysicianOrderTemplayeSetValueArr[i].m_strCreatorID = objDataTableResult.Rows[i]["CREATORID"].ToString();
                        objPhysicianOrderTemplayeSetValueArr[i].m_strStatus = objDataTableResult.Rows[i]["STATUS"].ToString();

                    }
                    lngRes = objPhysicianOrderTemplayeSetValueArr.Length;
                }
                #endregion

                lngRes= 0;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPhysicianOrderTemplayeSetValue"></param>
		/// <param name="p_objPhysicianOrderTemplayeValueCaseArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPhysicianOrderTemplate ( clsPhysicianOrderTemplateSetValue p_objPhysicianOrderTemplayeSetValue, out clsPhysicianOrderTemplateValue[] p_objPhysicianOrderTemplayeValueCaseArr )
		{
			long lngRes = 0;
			p_objPhysicianOrderTemplayeValueCaseArr=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderTemplate,Service层,刘颖源,2003-8-12 13:36:41
                string strSQL = @"select set_id, activedate, orderid, orderflag from physicianordertemplate ";
                strSQL += " where set_id = '" + p_objPhysicianOrderTemplayeSetValue.m_strSet_ID + "'  and activedate = '" + p_objPhysicianOrderTemplayeSetValue.m_strActiveDate + "' ";
                p_objPhysicianOrderTemplayeValueCaseArr = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderTemplayeValueCaseArr = new clsPhysicianOrderTemplateValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderTemplayeValueCaseArr.Length; i++)
                    {
                        p_objPhysicianOrderTemplayeValueCaseArr[i] = new clsPhysicianOrderTemplateValue();
                        p_objPhysicianOrderTemplayeValueCaseArr[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objPhysicianOrderTemplayeValueCaseArr[i].m_strActiveDate = objDataTableResult.Rows[i]["ACTIVEDATE"].ToString();
                        p_objPhysicianOrderTemplayeValueCaseArr[i].m_strOrderID = objDataTableResult.Rows[i]["ORDERID"].ToString();
                        p_objPhysicianOrderTemplayeValueCaseArr[i].m_strOrderFlag = objDataTableResult.Rows[i]["ORDERFLAG"].ToString();

                    }
                    return p_objPhysicianOrderTemplayeValueCaseArr.Length;
                }
                #endregion

                lngRes= 0;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPhysicianOrderTemplateValue"></param>
		/// <param name="p_objPhysicianOrderTemplateContentValueArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPhysicianOrderTemplateContent ( clsPhysicianOrderTemplateValue p_objPhysicianOrderTemplateValue,out clsPhysicianOrderTemplateContentValue[] p_objPhysicianOrderTemplateContentValueArr )
		{
			long lngRes = 0;
			p_objPhysicianOrderTemplateContentValueArr=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 获得所有表PhysicianOrderTemplateContent,Service层,刘颖源,2003-8-12 13:40:44
                string strSQL = @"select set_id,
       activedate,
       suborderid,
       orderid,
       ordertypeid,
       detailid,
       usageid,
       itemid,
       frequencyid,
       itemstandardid,
       itemtypeid,
       itemunitid,
       itemdosage,
       remark,
       documenttype,
       documentid
  from physicianordertemplatecontent";
                strSQL += " where set_id = '" + p_objPhysicianOrderTemplateValue.m_strSet_ID + "' and activedate = '" + p_objPhysicianOrderTemplateValue.m_strActiveDate + "' and orderid = '" + p_objPhysicianOrderTemplateValue.m_strOrderID + "' ";

                p_objPhysicianOrderTemplateContentValueArr = null;
                DataTable objDataTableResult = new DataTable();
                lngRes = objHRPServ.DoGetDataTable(strSQL, ref objDataTableResult);
                if (lngRes > 0 && objDataTableResult.Rows.Count >= 1)
                {
                    p_objPhysicianOrderTemplateContentValueArr = new clsPhysicianOrderTemplateContentValue[objDataTableResult.Rows.Count];
                    for (int i = 0; i < p_objPhysicianOrderTemplateContentValueArr.Length; i++)
                    {
                        p_objPhysicianOrderTemplateContentValueArr[i] = new clsPhysicianOrderTemplateContentValue();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strSet_ID = objDataTableResult.Rows[i]["SET_ID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strActiveDate = objDataTableResult.Rows[i]["ACTIVEDATE"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strSubOrderID = objDataTableResult.Rows[i]["SUBORDERID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strOrderID = objDataTableResult.Rows[i]["ORDERID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strOrderTypeID = objDataTableResult.Rows[i]["ORDERTYPEID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strDetailID = objDataTableResult.Rows[i]["DETAILID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strUsageID = objDataTableResult.Rows[i]["USAGEID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strItemID = objDataTableResult.Rows[i]["ITEMID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strFrequencyID = objDataTableResult.Rows[i]["FREQUENCYID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strItemStandardID = objDataTableResult.Rows[i]["ITEMSTANDARDID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strItemTypeID = objDataTableResult.Rows[i]["ITEMTYPEID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strItemUnitID = objDataTableResult.Rows[i]["ITEMUNITID"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strItemDosage = objDataTableResult.Rows[i]["ITEMDOSAGE"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strRemark = objDataTableResult.Rows[i]["REMARK"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strDocumentType = objDataTableResult.Rows[i]["DOCUMENTTYPE"].ToString();
                        p_objPhysicianOrderTemplateContentValueArr[i].m_strDocumentID = objDataTableResult.Rows[i]["DOCUMENTID"].ToString();

                    }
                    return p_objPhysicianOrderTemplateContentValueArr.Length;
                }
                #endregion

                return 0;
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
		
		}


		#region Add group
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objPOTCV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddPhysicianOrderTemplateContent ( clsPhysicianOrderTemplateContentValue objPOTCV )
		{
			clsPhysicianOrderTemplateContentValue p_objPhysicianOrderTemplateContentValue = objPOTCV;
			long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 保存表 PhysicianOrderTemplateContent,刘颖源,2003-8-12 17:17:37
                //注意逗号,刘颖源,2003-8-12 17:17:37
                if (p_objPhysicianOrderTemplateContentValue != null)
                {
                    string strSQL = @"insert into physicianordertemplatecontent(
                        set_id, activedate, suborderid, orderid, ordertypeid, detailid, usageid,    itemid, frequencyid, itemstandardid, itemtypeid, itemunitid, itemdosage, remark, documenttype,    documentid 
                        ) values(
                        ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ? 
                        )";
                    //				IDataParameter objParameterSet_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterSet_ID.Value = p_objPhysicianOrderTemplateContentValue.m_strSet_ID;
                    //				IDataParameter objParameterActiveDate = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterActiveDate.Value = p_objPhysicianOrderTemplateContentValue.m_strActiveDate;
                    //				IDataParameter objParameterSubOrderID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterSubOrderID.Value = p_objPhysicianOrderTemplateContentValue.m_strSubOrderID;
                    //				IDataParameter objParameterOrderID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterOrderID.Value = p_objPhysicianOrderTemplateContentValue.m_strOrderID;
                    //				IDataParameter objParameterOrderTypeID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterOrderTypeID.Value = p_objPhysicianOrderTemplateContentValue.m_strOrderTypeID;
                    //				IDataParameter objParameterDetailID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterDetailID.Value = p_objPhysicianOrderTemplateContentValue.m_strDetailID;
                    //				IDataParameter objParameterUsageID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterUsageID.Value = p_objPhysicianOrderTemplateContentValue.m_strUsageID;
                    //				IDataParameter objParameterItemID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterItemID.Value = p_objPhysicianOrderTemplateContentValue.m_strItemID;
                    //				IDataParameter objParameterFrequencyID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterFrequencyID.Value = p_objPhysicianOrderTemplateContentValue.m_strFrequencyID;
                    //				IDataParameter objParameterItemStandardID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterItemStandardID.Value = p_objPhysicianOrderTemplateContentValue.m_strItemStandardID;
                    //				IDataParameter objParameterItemTypeID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterItemTypeID.Value = p_objPhysicianOrderTemplateContentValue.m_strItemTypeID;
                    //				IDataParameter objParameterItemUnitID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterItemUnitID.Value = p_objPhysicianOrderTemplateContentValue.m_strItemUnitID;
                    //				IDataParameter objParameterItemDosage = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterItemDosage.Value = p_objPhysicianOrderTemplateContentValue.m_strItemDosage;
                    //				IDataParameter objParameterRemark = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterRemark.Value = p_objPhysicianOrderTemplateContentValue.m_strRemark;
                    //				IDataParameter objParameterDocumentType = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterDocumentType.Value = p_objPhysicianOrderTemplateContentValue.m_strDocumentType;
                    //				IDataParameter objParameterDocumentID = new Oracle.DataAccess.Client.OracleParameter();
                    //				objParameterDocumentID.Value = p_objPhysicianOrderTemplateContentValue.m_strDocumentID;
                    //							
                    //			
                    //				IDataParameter[] objPhysicianOrderTemplateContentArr = new IDataParameter[16];
                    //				objPhysicianOrderTemplateContentArr[0]  =  objParameterSet_ID;
                    //				objPhysicianOrderTemplateContentArr[1]  =  objParameterActiveDate;
                    //				objPhysicianOrderTemplateContentArr[2]  =  objParameterSubOrderID;
                    //				objPhysicianOrderTemplateContentArr[3]  =  objParameterOrderID;
                    //				objPhysicianOrderTemplateContentArr[4]  =  objParameterOrderTypeID;
                    //				objPhysicianOrderTemplateContentArr[5]  =  objParameterDetailID;
                    //				objPhysicianOrderTemplateContentArr[6]  =  objParameterUsageID;
                    //				objPhysicianOrderTemplateContentArr[7]  =  objParameterItemID;
                    //				objPhysicianOrderTemplateContentArr[8]  =  objParameterFrequencyID;
                    //				objPhysicianOrderTemplateContentArr[9]  =  objParameterItemStandardID;
                    //				objPhysicianOrderTemplateContentArr[10]  =  objParameterItemTypeID;
                    //				objPhysicianOrderTemplateContentArr[11]  =  objParameterItemUnitID;
                    //				objPhysicianOrderTemplateContentArr[12]  =  objParameterItemDosage;
                    //				objPhysicianOrderTemplateContentArr[13]  =  objParameterRemark;
                    //				objPhysicianOrderTemplateContentArr[14]  =  objParameterDocumentType;
                    //				objPhysicianOrderTemplateContentArr[15]  =  objParameterDocumentID;


                    IDataParameter[] objPhysicianOrderTemplateContentArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                    objHRPServ.CreateDatabaseParameter(16, out objPhysicianOrderTemplateContentArr);
                    objPhysicianOrderTemplateContentArr[0].Value = p_objPhysicianOrderTemplateContentValue.m_strSet_ID;
                    objPhysicianOrderTemplateContentArr[1].Value = p_objPhysicianOrderTemplateContentValue.m_strActiveDate;
                    objPhysicianOrderTemplateContentArr[2].Value = p_objPhysicianOrderTemplateContentValue.m_strSubOrderID;
                    objPhysicianOrderTemplateContentArr[3].Value = p_objPhysicianOrderTemplateContentValue.m_strOrderID;
                    objPhysicianOrderTemplateContentArr[4].Value = p_objPhysicianOrderTemplateContentValue.m_strOrderTypeID;
                    objPhysicianOrderTemplateContentArr[5].Value = p_objPhysicianOrderTemplateContentValue.m_strDetailID;
                    objPhysicianOrderTemplateContentArr[6].Value = p_objPhysicianOrderTemplateContentValue.m_strUsageID;
                    objPhysicianOrderTemplateContentArr[7].Value = p_objPhysicianOrderTemplateContentValue.m_strItemID;
                    objPhysicianOrderTemplateContentArr[8].Value = p_objPhysicianOrderTemplateContentValue.m_strFrequencyID;
                    objPhysicianOrderTemplateContentArr[9].Value = p_objPhysicianOrderTemplateContentValue.m_strItemStandardID;
                    objPhysicianOrderTemplateContentArr[10].Value = p_objPhysicianOrderTemplateContentValue.m_strItemTypeID;
                    objPhysicianOrderTemplateContentArr[11].Value = p_objPhysicianOrderTemplateContentValue.m_strItemUnitID;
                    objPhysicianOrderTemplateContentArr[12].Value = p_objPhysicianOrderTemplateContentValue.m_strItemDosage;
                    objPhysicianOrderTemplateContentArr[13].Value = p_objPhysicianOrderTemplateContentValue.m_strRemark;
                    objPhysicianOrderTemplateContentArr[14].Value = p_objPhysicianOrderTemplateContentValue.m_strDocumentType;
                    objPhysicianOrderTemplateContentArr[15].Value = p_objPhysicianOrderTemplateContentValue.m_strDocumentID;



                    objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objPhysicianOrderTemplateContentArr);
                }
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngEff;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objPOTV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddPhysicianOrderTemplate ( clsPhysicianOrderTemplateValue objPOTV )
		{
			clsPhysicianOrderTemplateValue p_objPhysicianOrderTemplateValue = objPOTV;
			
			long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 保存表 PhysicianOrderTemplate,刘颖源,2003-8-12 17:15:59
                //注意逗号,刘颖源,2003-8-12 17:15:59
                if (p_objPhysicianOrderTemplateValue != null)
                {
                    string strSQL = @"insert into physicianordertemplate(
                        set_id, activedate, orderid, orderflag 
                        ) values(
                        ?, ?, ?, ? 
                        )";
                    //					IDataParameter objParameterSet_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_ID.Value = p_objPhysicianOrderTemplateValue.m_strSet_ID;
                    //					IDataParameter objParameterActiveDate = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterActiveDate.Value = p_objPhysicianOrderTemplateValue.m_strActiveDate;
                    //					IDataParameter objParameterOrderID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterOrderID.Value = p_objPhysicianOrderTemplateValue.m_strOrderID;
                    //					IDataParameter objParameterOrderFlag = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterOrderFlag.Value = p_objPhysicianOrderTemplateValue.m_strOrderFlag;
                    //								
                    //				
                    //					IDataParameter[] objPhysicianOrderTemplateArr = new IDataParameter[4];

                    IDataParameter[] objPhysicianOrderTemplateArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                    objHRPServ.CreateDatabaseParameter(4, out objPhysicianOrderTemplateArr);

                    objPhysicianOrderTemplateArr[0].Value = p_objPhysicianOrderTemplateValue.m_strSet_ID; ;
                    objPhysicianOrderTemplateArr[1].Value = p_objPhysicianOrderTemplateValue.m_strActiveDate; ;
                    objPhysicianOrderTemplateArr[2].Value = p_objPhysicianOrderTemplateValue.m_strOrderID; ;
                    objPhysicianOrderTemplateArr[3].Value = p_objPhysicianOrderTemplateValue.m_strOrderFlag; ;


                    objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objPhysicianOrderTemplateArr);
                }
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngEff;

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objPOTSV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddPhysicianOrderTemplateSet ( clsPhysicianOrderTemplateSetValue objPOTSV )
		{
			clsPhysicianOrderTemplateSetValue p_objPhysicianOrderTemplateSetValue = objPOTSV;
			long lngEff = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                #region 保存表 PhysicianOrderTemplateSet,刘颖源,2003-8-12 17:13:17
                //注意逗号,刘颖源,2003-8-12 17:13:17

                if (p_objPhysicianOrderTemplateSetValue != null)
                {
                    string strSQL = @"insert into physicianordertemplateset(
                        set_id, activedate, name, begindate, enddate, creatorid, status 
                        ) values(
                        ?, ?, ?, ?, ?, ?, ? 
                        )";

                    //					IDataParameter objParameterSet_ID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterSet_ID.Value = p_objPhysicianOrderTemplateSetValue.m_strSet_ID;
                    //					IDataParameter objParameterActiveDate = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterActiveDate.Value = p_objPhysicianOrderTemplateSetValue.m_strActiveDate;
                    //					IDataParameter objParameterName = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterName.Value = p_objPhysicianOrderTemplateSetValue.m_strName;
                    //					IDataParameter objParameterBeginDate = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterBeginDate.Value = p_objPhysicianOrderTemplateSetValue.m_strBeginDate;
                    //					IDataParameter objParameterEndDate = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterEndDate.Value = p_objPhysicianOrderTemplateSetValue.m_strEndDate;
                    //					IDataParameter objParameterCreatorID = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterCreatorID.Value = p_objPhysicianOrderTemplateSetValue.m_strCreatorID;
                    //					IDataParameter objParameterStatus = new Oracle.DataAccess.Client.OracleParameter();
                    //					objParameterStatus.Value = p_objPhysicianOrderTemplateSetValue.m_strStatus;
                    //								
                    //				
                    //					IDataParameter[] objPhysicianOrderTemplateSetArr = new IDataParameter[7];


                    IDataParameter[] objPhysicianOrderTemplateSetArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                    objHRPServ.CreateDatabaseParameter(7, out objPhysicianOrderTemplateSetArr);

                    objPhysicianOrderTemplateSetArr[0].Value = p_objPhysicianOrderTemplateSetValue.m_strSet_ID; ;
                    objPhysicianOrderTemplateSetArr[1].Value = p_objPhysicianOrderTemplateSetValue.m_strActiveDate; ;
                    objPhysicianOrderTemplateSetArr[2].Value = p_objPhysicianOrderTemplateSetValue.m_strName; ;
                    objPhysicianOrderTemplateSetArr[3].Value = p_objPhysicianOrderTemplateSetValue.m_strBeginDate; ;
                    objPhysicianOrderTemplateSetArr[4].Value = p_objPhysicianOrderTemplateSetValue.m_strEndDate; ;
                    objPhysicianOrderTemplateSetArr[5].Value = p_objPhysicianOrderTemplateSetValue.m_strCreatorID; ;
                    objPhysicianOrderTemplateSetArr[6].Value = p_objPhysicianOrderTemplateSetValue.m_strStatus; ;

                    objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objPhysicianOrderTemplateSetArr);
                }
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
			return lngEff;

		}

		#endregion

		#region Delete group
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objPOTCV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeletePhysicianOrderTemplateContent ( clsPhysicianOrderTemplateContentValue objPOTCV )
		{
			return 0 ;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objPOTV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeletePhysicianOrderTemplate ( clsPhysicianOrderTemplateValue objPOTV )
		{
			return 0;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objPOTSV"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeletePhysicianOrderTemplateSet ( clsPhysicianOrderTemplateSetValue objPOTSV )
		{
			
			return 0;
		}
		#endregion

	}
}
