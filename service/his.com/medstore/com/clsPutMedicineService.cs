using System;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.iCare.middletier.BIHOrderServer;
using com.digitalwave.Utility;//Utility.dll
using weCare.Core.Entity;
using System.EnterpriseServices; 

namespace com.digitalwave.iCare.middletier.PutMedicineService
{
    #region clsPutMedicineService
    /// <summary>
    /// Class1 的摘要说明。
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPutMedicineService :com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        #region  m_lngGetPutMedDetail	(DataTable)
        /// <summary>
        /// 获取摆药申请单的详细|汇总信息	根据摆药申请单
        /// </summary>
        /// <param name="strAreaID">病区ID</param>
        /// <param name="strBeginBed"></param>
        /// <param name="strEndBed"></param>
        /// <param name="intExecType">例如:[1,2,3]	{1长嘱	2临嘱	3长嘱新开加}</param>
        /// <param name="arrDoseTypeID">为Null所有类型</param>
        /// <param name="intIsRich">0非贵重	1贵重	2所有</param>
        /// <param name="intIsPut">0未摆药	1摆药	2所有</param>
        /// <param name="arrDetail"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetail(string strAreaID, string strBeginBed, string strEndBed, string strExecType, string[] arrDoseTypeID, int intIsRich, int intIsPut, out DataTable objDTDetail, out DataTable objDTSum)
        {

            #region Sql
            string strSql = @"
				select A.*,B.Name_VChr PatientName,B.Sex_Chr PatientSex,BIHPack.GetAge(B.Birth_Dat) PatientAge,C.BedName BedNo,D.UsageName_VChr MedUsage,E.MedSpec_VChr MedSpec,
				case A.OrderExecType_Int when 1 then '长期' when 2 then '临时' when 3 then '长嘱新开加' end ExecType,
				A.MedID_Chr MedNo,A.MedName_VChr MedName,F.FreqName_Chr MedFreq,A.UnitPrice_Mny Price,
				A.Get_Dec Get ,A.Unit_Vchr Unit,A.PutMedDetailID_Chr ID,0 NO,
				case nvl(A.IsRich_Int,0) when 0 then '' when 1 then '√' end IsRich,
				case nvl(A.IsPut_Int,0) when 0 then '' when 1 then '√' end IsPut,
				case nvl(A.PutType_Int,0) when 0 then '' when 1 then '按总量' when 2 then '按明细(一般药)' when 3 then '按明细(确认药)' end PutType

				from  T_bih_opr_putMedDetail A,
				T_BSE_Patient B,
					(
							select TA.RegisterID_Chr,TB.BedID_Chr,TB.Code_Chr BedName 
							from T_Opr_Bih_Register TA,T_BSE_Bed TB
							where TA.AreaID_Chr=TB.AreaID_Chr and TA.BedID_Chr=TB.BedID_Chr
							[PatientCondition]
					)C,
				T_Bse_UsageType D,
				T_bse_Medicine E,
				T_Aid_RecipeFreq F
				where A.PaientID_Chr=B.PatientID_Chr(+) and B.Status_Int=1
				and A.RegisterID_Chr=C.RegisterID_Chr
				and A.DoseTypeID_Chr=D.UsageID_Chr(+)
				and A.ExecFreqID_Chr=F.FreqID_Chr(+)
				and Trim(A.MedID_Chr)=Trim(E.MedicineID_Chr(+))
				[OrderExecTypeCondition]
				[DoseTypeCondition]
				[IsRichCondition]
				[AlreadyPutCondition]
				order by PutMedDetailID_Chr
				";

            #endregion

            string strPatientCondition = new clsBIHOrderService().m_strGetPatientsSQL("TB", strAreaID, strBeginBed, strEndBed);
            strSql = strSql.Replace("[PatientCondition]", strPatientCondition);

            #region Condition

            //
            if (strExecType.Trim() == "")
                strSql = strSql.Replace("[OrderExecTypeCondition]", "");
            else
                strSql = strSql.Replace("[OrderExecTypeCondition]", " and A.OrderExecType_Int in ( " + strExecType + " ) ");

            //
            if ((arrDoseTypeID == null) || (arrDoseTypeID.Length <= 0))
            {
                strSql = strSql.Replace("[DoseTypeCondition]", "");
            }
            else
            {
                string arrID = "";
                for (int i = 0; i < arrDoseTypeID.Length; i++)
                {
                    if (arrDoseTypeID[i].Trim() != "")
                    {
                        if (i > 0) arrID += ",";
                        arrID += "'" + arrDoseTypeID[i] + "'";
                    }
                }
                if (arrID.Trim() != "")
                    strSql = strSql.Replace("[DoseTypeCondition]", " and A.DoseTypeID_Chr in ( " + arrID + " )");
                else
                    strSql = strSql.Replace("[DoseTypeCondition]", "");
            }

            //
            if (intIsRich >= 2)
            {
                strSql = strSql.Replace("[IsRichCondition]", "");
            }
            else
            {
                strSql = strSql.Replace("[IsRichCondition]", " and A.IsRich_Int=" + intIsRich.ToString());
            }

            //
            if (intIsPut >= 2)
            {
                strSql = strSql.Replace("[AlreadyPutCondition]", "");
            }
            else
            {
                strSql = strSql.Replace("[AlreadyPutCondition]", " and A.IsPut_Int=" + intIsPut.ToString());
            }

            #endregion

            string strSql2 = @"
				select 0 NO,MedID_Chr MedNo,MedName,MedSpec,sum(Get_Dec) Get, Unit, Price,sum(Get_Dec)*UnitPrice_Mny GetMoney
				from ( [SQL1STRING] ) TA
				group by MedID_Chr,MedName,MedSpec,Unit_VChr,UnitPrice_Mny
				";
            strSql2 = strSql2.Replace("[SQL1STRING]", strSql);

            objDTDetail = null;
            objDTSum = null;

            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSql, ref objDTDetail);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc2 = new clsHRPTableService();
                    lngRes = objHRPSvc2.DoGetDataTable(strSql2, ref objDTSum);
                    objHRPSvc2.Dispose();
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
        /// 获取摆药申请单的详细|汇总信息	根据摆药申请单
        /// </summary>
        /// <param name="strMedReqID">摆药申请单id{=摆药申请单.id}</param>
        /// <param name="objDTDetail">明细表	[out参数]</param>
        /// <param name="objDTSum">汇总表	[out参数]</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPutMedDetail(string strMedReqID, out DataTable objDTDetail, out DataTable objDTSum)
        {

            #region Sql
            string strSql = @"
				select A.*,B.Name_VChr PatientName,B.Sex_Chr PatientSex,BIHPack.GetAge(B.Birth_Dat) PatientAge,C.BedName BedNo,D.UsageName_VChr MedUsage,E.MedSpec_VChr MedSpec,
				case A.OrderExecType_Int when 1 then '长期' when 2 then '临时' when 3 then '长嘱新开加' end ExecType,
				A.MedID_Chr MedNo,A.MedName_VChr MedName,F.FreqName_Chr MedFreq,A.UnitPrice_Mny Price,
				A.Get_Dec Get ,A.Unit_Vchr Unit,A.PutMedDetailID_Chr ID,0 NO,
				case nvl(A.IsRich_Int,0) when 0 then '' when 1 then '√' end IsRich,
				case nvl(A.IsPut_Int,0) when 0 then '' when 1 then '√' end IsPut,
				case nvl(A.PutType_Int,0) when 0 then '' when 1 then '按总量' when 2 then '按明细(一般药)' when 3 then '按明细(确认药)' end PutType

				from  T_bih_opr_putMedDetail A,
				T_BSE_Patient B,
					(
							select TA.RegisterID_Chr,TB.BedID_Chr,TB.Code_Chr BedName 
							from T_Opr_Bih_Register TA,T_BSE_Bed TB
							where TA.AreaID_Chr=TB.AreaID_Chr and TA.BedID_Chr=TB.BedID_Chr
						
					)C,
				T_Bse_UsageType D,
				T_bse_Medicine E,
				T_Aid_RecipeFreq F
				where A.PaientID_Chr=B.PatientID_Chr(+) and B.Status_Int=1
				and A.RegisterID_Chr=C.RegisterID_Chr
				and A.DoseTypeID_Chr=D.UsageID_Chr(+)
				and A.ExecFreqID_Chr=F.FreqID_Chr(+)
				and Trim(A.MedID_Chr)=Trim(E.MedicineID_Chr(+))
				and A.PutMedReqID_Chr='[MedReqID]'
				order by PutMedDetailID_Chr
				";

            #endregion

            strSql = strSql.Replace("[MedReqID]", strMedReqID);

            //
            string strSql2 = @"
				select 0 NO,MedID_Chr MedNo,MedName,MedSpec,sum(Get_Dec) Get, Unit, Price,sum(Get_Dec)*UnitPrice_Mny GetMoney
				from ( [SQL1STRING] ) TA
				group by MedID_Chr,MedName,MedSpec,Unit_VChr,UnitPrice_Mny
				";

            strSql2 = strSql2.Replace("[SQL1STRING]", strSql);
            //
            objDTDetail = null;
            objDTSum = null;

            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSql, ref objDTDetail);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc2 = new clsHRPTableService();
                    lngRes = objHRPSvc2.DoGetDataTable(strSql2, ref objDTSum);
                    objHRPSvc2.Dispose();
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

        #region m_lngGetPutMedDetail   Get ValueObject
        [AutoComplete]
        public long m_lngGetPutMedDetail(string strAreaID, string strBeginBed, string strEndBed, int intExecType, string[] arrDoseTypeID, int intIsRich, int intIsPut, out clsPutMedicineDetail[] arrDetail)
        {
            #region Sql
            string strSql = @"
				select A.*,B.Name_VChr,C.BedName,D.UsageName_VChr,E.MedSpec_VChr
				from  T_bih_opr_putMedDetail A,
				T_BSE_Patient B,
					(
							select TA.RegisterID_Chr,TB.BedID_Chr,TB.Code_Chr BedName 
							from T_Opr_Bih_Register TA,T_BSE_Bed TB
							where TA.AreaID_Chr=TB.AreaID_Chr and TA.BedID_Chr=TB.BedID_Chr
							[PatientCondition]
					)C,
				T_Bse_UsageType D,
				T_bse_Medicine E,
				T_Aid_RecipeFreq F
				where A.PaientID_Chr=B.PatientID_Chr(+) and B.Status_Int=1
				and A.RegisterID_Chr=C.RegisterID_Chr
				and A.DoseTypeID_Chr=D.UsageID_Chr(+)
				and A.ExecFreqID_Chr=F.FreqID_Chr(+)
				and Trim(A.MedID_Chr)=Trim(E.MedicineID_Chr)
				[OrderExecTypeCondition]
				[DoseTypeCondition]
				[IsRichCondition]
				[AlreadyPutCondition]
				order by PutMedDetailID_Chr
				";

            #endregion

            string strPatientCondition = new clsBIHOrderService().m_strGetPatientsSQL("TB", strAreaID, strBeginBed, strEndBed);
            strSql = strSql.Replace("[PatientCondition]", strPatientCondition);

            #region Condition
            //
            if (intExecType == 0)
                strSql = strSql.Replace("[OrderExecTypeCondition]", "");
            else
                strSql = strSql.Replace("[OrderExecTypeCondition]", " and A.OrderExecType_Int = " + intExecType.ToString());

            //
            if ((arrDoseTypeID == null) || (arrDoseTypeID.Length <= 0))
            {
                strSql = strSql.Replace("[DoseTypeCondition]", "");
            }
            else
            {
                string arrID = "";
                for (int i = 0; i < arrDoseTypeID.Length; i++)
                {
                    if (i > 0) arrID += ",";
                    arrID += "'" + arrDoseTypeID[i] + "'";
                }
                strSql = strSql.Replace("[DoseTypeCondition]", " and A.DoseTypeID in ( " + arrID + " )");
            }

            //
            if (intIsRich >= 2)
            {
                strSql = strSql.Replace("[IsRichCondition]", "");
            }
            else
            {
                strSql = strSql.Replace("[IsRichCondition]", " and A.IsRich_Int=" + intIsRich.ToString());
            }

            //
            if (intIsPut >= 2)
            {
                strSql = strSql.Replace("[AlreadyPutCondition]", "");
            }
            else
            {
                strSql = strSql.Replace("[AlreadyPutCondition]", " and A.IsPut_Int=" + intIsPut.ToString());
            }

            #endregion

            long lngRes = 0;
            arrDetail = null;
            try
            {
                DataTable dtReslut = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSql, ref dtReslut);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtReslut.Rows.Count > 0)
                {
                    arrDetail = new clsPutMedicineDetail[dtReslut.Rows.Count];
                    for (int i = 0; i < arrDetail.Length; i++)
                    {
                        clsPutMedicineDetail objPut;
                        DataRow objRow = dtReslut.Rows[i];
                        lngRes = m_lngDataRowToDetail(objRow, out objPut);
                        arrDetail[i] = objPut;
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


        [AutoComplete]
        public long m_lngDataRowToDetail(DataRow objRow, out clsPutMedicineDetail objDetail)
        {
            if (objRow == null)
            {
                objDetail = null;
                return 0;
            }

            clsPutMedicineDetail objPut = new clsPutMedicineDetail();

            objPut.m_strPutMedDetailID = clsConverter.ToString(objRow["PutMedDetailID_Chr"]).Trim();
            objPut.m_strAreaID = clsConverter.ToString(objRow["AreaID_Chr"]).Trim();
            objPut.m_strPatientID = clsConverter.ToString(objRow["PaientID_Chr"]).Trim();
            objPut.m_strRegisterID = clsConverter.ToString(objRow["RegisterID_Chr"]).Trim();
            objPut.m_strOrderID = clsConverter.ToString(objRow["OrderID_Chr"]).Trim();
            objPut.m_strOrderExecID = clsConverter.ToString(objRow["OrderExecID_Chr"]).Trim();
            objPut.m_intOrderExecType = clsConverter.ToInt(objRow["OrderExecType_Int"]);
            objPut.m_intRecipeNo = clsConverter.ToInt(objRow["RecipeNo_Int"]);
            objPut.m_dmlDosage = clsConverter.ToDecimal(objRow["Dosage_Dec"]);
            objPut.m_strDosageUnit = clsConverter.ToString(objRow["DosageUnit_VChr"]).Trim();

            objPut.m_strChargeItemID = clsConverter.ToString(objRow["ChargeItemID_Chr"]).Trim();
            objPut.m_strMedicineID = clsConverter.ToString(objRow["MedID_Chr"]).Trim();
            objPut.m_strMedicineName = clsConverter.ToString(objRow["MedName_VChr"]).Trim();
            objPut.m_intIsRich = clsConverter.ToInt(objRow["IsRich_Int"]);

            objPut.m_strDoseTypeID = clsConverter.ToString(objRow["DoseTypeID_Chr"]).Trim();
            objPut.m_strExecFreqID = clsConverter.ToString(objRow["ExecFreqID_Chr"]).Trim();
            objPut.m_intExecTimes = clsConverter.ToInt(objRow["ExecTimes_Int"]);
            objPut.m_intExecDays = clsConverter.ToInt(objRow["ExecDays_Int"]);
            objPut.m_strExecTime = clsConverter.ToString(objRow["ExecTime_VChr"]).Trim();
            objPut.m_dmlUnitPrice = clsConverter.ToDecimal(objRow["UnitPrice_Mny"]);
            objPut.m_strUnit = clsConverter.ToString(objRow["Unit_VChr"]).Trim();
            objPut.m_dmlGet = clsConverter.ToDecimal(objRow["Get_Dec"]);

            objPut.m_strPChargeID = clsConverter.ToString(objRow["PChargeID_Chr"]).Trim();
            objPut.m_strCreator = clsConverter.ToString(objRow["Creator_Chr"]).Trim();
            objPut.m_dtCreateDate = clsConverter.ToDateTime(objRow["Create_Dat"]);
            objPut.m_intIsPut = clsConverter.ToInt(objRow["IsPut_Int"]);
            objPut.m_intIsPut = clsConverter.ToInt(objRow["PutType_Int"]);
            objPut.m_strPutMedReqID = clsConverter.ToString(objRow["PutMedReqID_Chr"]).Trim();

            objPut.m_strPatientName = clsConverter.ToString(objRow["Name_VChr"]).Trim();
            objPut.m_strBedName = clsConverter.ToString(objRow["BedName"]).Trim();
            objPut.m_strUsageName = clsConverter.ToString(objRow["UsageName_VChr"]).Trim();
            objPut.m_strMedicineSpec = clsConverter.ToString(objRow["MedSpec_VChr"]).Trim();

            objDetail = objPut;
            return 1;
        }

        /// <summary>
        /// 查询摆药申请单
        /// </summary>
        /// <param name="strAreaID">病区</param>
        /// <param name="strCreatorID">创建人ID</param>
        /// <param name="dtBegin">创建时间的所在范围--起始时间</param>
        /// <param name="dtEnd">中止时间</param>
        /// <param name="intStatus">申请单状态:0未发送	1已发送		3已发药</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedReqList(string strAreaID, string strCreatorID, DateTime dtBegin, DateTime dtEnd, int[] arrStatus, out clsPutMedReq[] arrList)
        {
            string strSql = @" select * from T_BIH_Opr_PutMedReq
							where [StatusCondition]
							[AreaCondition]	[CreatorCondition]	[BeginDate]	[EndDate]
							";
            #region Condition
            //
            if ((arrStatus == null) || (arrStatus.Length <= 0))
            {
                arrList = new clsPutMedReq[0];
                return 1;
            }

            //
            if (arrStatus.Length == 1)
            {
                strSql = strSql.Replace("[StatusCondition]", " PStatus_Int = " + arrStatus[0].ToString());
            }
            else
            {
                string strStatus = "";
                for (int i = 0; i < arrStatus.Length; i++)
                {
                    if (i > 0) strStatus += ",";
                    strStatus += arrStatus[i].ToString();
                }
                strSql = strSql.Replace("[StatusCondition]", " PStatus_Int int ( " + strStatus + " ) ");
            }

            //
            if (strAreaID.Trim() == "")
            {
                strSql = strSql.Replace("[AreaCondition]", "");
            }
            else
            {
                strSql = strSql.Replace("[AreaCondition]", " and AreaID_Chr='" + strAreaID.Trim() + "' ");
            }

            //
            if (strCreatorID.Trim() == "")
            {
                strSql = strSql.Replace("[CreatorCondition]", "");
            }
            else
            {
                strSql = strSql.Replace("[CreatorCondition]", " and Creator_Chr='" + strCreatorID + "' ");
            }

            //
            if (dtBegin == DateTimeNull.Value)
            {
                strSql = strSql.Replace("[BeginDate]", "");
            }
            else
            {
                strSql = strSql.Replace("[BeginDate]", " and Create_Dat >= to_date('" + dtBegin.ToString("yyyy-MM-dd") + "')");
            }

            //
            if (dtEnd == DateTimeNull.Value)
            {
                strSql = strSql.Replace("[EndDate]", "");
            }
            else
            {
                strSql = strSql.Replace("[EndDate]", " and Create_Dat <= to_date('" + dtEnd.ToString("yyyy-MM-dd") + "')");
            }

            #endregion

            //
            DataTable objDt = new DataTable();
            long ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDt);
            if (ret > 0)
            {
                arrList = new clsPutMedReq[objDt.Rows.Count];
                for (int i = 0; i < arrList.Length; i++)
                {
                    clsPutMedReq objReq = new clsPutMedReq();
                    DataRow objRow = objDt.Rows[i];

                    #region DataRow to ValueObject
                    objReq.m_strPutMedReqID = clsConverter.ToString(objRow["PutMedReqID_Chr"]).Trim();
                    objReq.m_strAreaID = clsConverter.ToString(objRow["AreaID_Chr"]).Trim();
                    objReq.m_strDes = clsConverter.ToString(objRow["Des_VChr"]).Trim();
                    objReq.m_intPStatus = clsConverter.ToInt(objRow["PStatus_Int"]);
                    objReq.m_intStatus = clsConverter.ToInt(objRow["Status_Int"]);
                    objReq.m_strDeactivator = clsConverter.ToString(objRow["Deactivator_Chr"]).Trim();
                    try { objReq.m_dtDeactivateDate = clsConverter.ToDateTime(objRow["Deactivate_Dat"]); }
                    catch { }
                    objReq.m_strCreator = clsConverter.ToString(objRow["Creator_Chr"]).Trim();
                    objReq.m_dtCreateDate = clsConverter.ToDateTime(objRow["Create_Dat"]);
                    objReq.m_strRangeDes = clsConverter.ToString(objRow["RangeDes_VChr"]).Trim();
                    objReq.m_strProcessor = clsConverter.ToString(objRow["Processor_Chr"]).Trim();
                    try { objReq.m_dtFinishDate = clsConverter.ToDateTime(objRow["Finish_Dat"]); }
                    catch { }
                    #endregion
                }

                return 1;
            }
            else
            {
                arrList = null;
                return 0;
            }
        }

        [AutoComplete]
        public long m_lngGetMedReqList(bool blnOnlyCanProcess, out DataTable objDT)
        {
            //获取申请单列表

            string strSql = @" select A.putmedreqid_chr FlowNO,C.deptname_vchr Area,
							A.pstatus_int,B.lastname_vchr Creator,
							to_char(A.Create_Dat,'yyyy-mm-dd hh24:mi') CreateDate,
							case PStatus_Int when 1 then '' when 2 then '-' when 3 then '√' end PStatus
							from T_BIH_Opr_PutMedReq A,T_BSE_Employee B,T_BSE_DeptDesc C
							where A.Creator_Chr=B.EmpID_Chr(+)  and A.Status_Int=1
								and A.AreaID_Chr=C.DeptID_Chr(+) 
								[StatusCondition]
							order by A.PStatus_Int, A.PutMedReqID_Chr
						";

            if (blnOnlyCanProcess)//
            {
                strSql = strSql.Replace("[StatusCondition]", " and A.PStatus_Int=1 ");
            }
            else
            {
                strSql = strSql.Replace("[StatusCondition]", " and A.PStatus_Int>0 ");

            }

            //
            DataTable objDt = new DataTable();
            long ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDt);
            if (ret > 0)
            {
                objDt.Columns.Add("NO", typeof(int));
                objDT = objDt;
                return 1;
            }
            else
            {
                objDT = null;
                return 0;
            }
        }

        /// <summary>
        /// 申请单处理完毕( PStatus->3)
        ///		处理状态{0=未发放;1=已发送;2=部分发药;3=已发药};if(this==2) 不允许并发访问
        /// </summary>
        /// <param name="strPutMedReqID"></param>
        /// <param name="strEmpID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMedReqProcessed(string strPutMedReqID, string strEmpID)
        {
            long lngRes = 0;
            string strSql = @" Update T_Bih_Opr_PutMedReq
							set PStatus_Int=3,Finish_Dat=sysdate,Processor_Chr='[EmpID]'
							where PutMedReqID_Chr = '[ReqID]' ";
            strSql = strSql.Replace("[ReqID]", strPutMedReqID);
            strSql = strSql.Replace("[EmpID]", strEmpID);
            lngRes = new clsHRPTableService().DoExcute(strSql);
            return lngRes;
        }
        /// <summary>
        /// 摆药操作
        /// 业务说明：
        ///			1、修改摆药申请单：		３		处理状态{0=未发放;1=已发送;2=部分发药;3=已发药};if(this==2) 不允许并发访问
        ///			2、修改医嘱摆药明细单：	摆药申请单id、标志为摆药
        ///			3、修改医嘱执行单：		是否已接收	{1/0}
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long lngPutMedicine()
        {
            return 1;
        }
        [AutoComplete]
        public long m_lngGetMedReqByID(string strPutMedReqID, out clsPutMedReq objReq)
        {
            return new clsPutMedListReqService().m_lngGetMedReqByID(strPutMedReqID, out objReq);
        }
    }
    #endregion

    #region clsPutMedListReqService
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPutMedListReqService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        //创建申请单
        [AutoComplete]
        public long m_lngCreateMedReq(clsPutMedReq objReq)
        {
            string strReqID = "";
            long ret = new clsHRPTableService().lngGenerateID(12, "PutMedReqID_Chr", "T_BIH_OPR_PutMedReq", out strReqID);
            if (ret <= 0) return 0;

            string strSql = @" Insert into T_Bih_Opr_PutMedReq(PutMedReqID_Chr,AreaID_Chr,Des_VChr,PStatus_Int,Status_Int,Creator_Chr,Create_Dat,RangeDes_VChr,
							BedBegin_VChr,BedEnd_VChr,ExecType_VChr,RichType_VChr,UsageType_VChr, medTypeId)
							values ('[ReqID]','[AreaID]','[Des]',0,1,'[Creator]', sysdate ,'[RangeDes]',
							'[BedBegin]','[BedEnd]','[ExecType]','[RichType]','[UsageType]', 0 ) ";

            strSql = strSql.Replace("[ReqID]", strReqID);
            strSql = strSql.Replace("[AreaID]", objReq.m_strAreaID);
            strSql = strSql.Replace("[Des]", objReq.m_strDes.Replace("'", "''"));
            strSql = strSql.Replace("[Creator]", objReq.m_strCreator);
            strSql = strSql.Replace("[RangeDes]", objReq.m_strRangeDes.Replace("'", "''"));

            strSql = strSql.Replace("[BedBegin]", objReq.m_strBedBegin);
            strSql = strSql.Replace("[BedEnd]", objReq.m_strBedEnd);
            strSql = strSql.Replace("[ExecType]", objReq.m_strExecType);
            strSql = strSql.Replace("[RichType]", objReq.m_strRichType);
            strSql = strSql.Replace("[UsageType]", objReq.m_strUsageType);


            long ret2 = new clsHRPTableService().DoExcute(strSql);
            if (ret2 > 0)
            {
                objReq.m_strPutMedReqID = strReqID;
                return 1;
            }
            else
            {
                return 0;
            }
        }


        //修改申请单
        [AutoComplete]
        public long m_lngModifyMedReq(clsPutMedReq objReq)
        {
            string strSql = @" Update T_Bih_Opr_PutMedReq
							set AreaID_Chr= '[AreaID]', Des_VChr='[Des]',PStatus_Int=0,Status_int=1,Creator_Chr='[Creator]',RangeDes_VChr='[RangeDes]',
							BedBegin_VChr ='[BedBegin]',BedEnd_VChr= '[BedEnd]',ExecType_VChr= '[ExecType]',RichType_VChr ='[RichType]',UsageType_VChr='[UsageType]'
							
							where PutMedReqID_Chr = '[ReqID]'
							 ";

            strSql = strSql.Replace("[ReqID]", objReq.m_strPutMedReqID);
            strSql = strSql.Replace("[AreaID]", objReq.m_strAreaID);
            strSql = strSql.Replace("[Des]", objReq.m_strDes.Replace("'", "''"));
            strSql = strSql.Replace("[Creator]", objReq.m_strCreator);
            strSql = strSql.Replace("[RangeDes]", objReq.m_strRangeDes.Replace("'", "''"));

            strSql = strSql.Replace("[BedBegin]", objReq.m_strBedBegin);
            strSql = strSql.Replace("[BedEnd]", objReq.m_strBedEnd);
            strSql = strSql.Replace("[ExecType]", objReq.m_strExecType);
            strSql = strSql.Replace("[RichType]", objReq.m_strRichType);
            strSql = strSql.Replace("[UsageType]", objReq.m_strUsageType);


            long ret2 = new clsHRPTableService().DoExcute(strSql);
            if (ret2 > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //发送申请单(到药房)PStatus->1
        [AutoComplete]
        public long m_lngSendMedReq(string strPutMedReqID)
        {
            string strSql = @" Update T_Bih_Opr_PutMedReq
							set PStatus_Int=1
							where PutMedReqID_Chr = '[ReqID]' ";
            strSql = strSql.Replace("[ReqID]", strPutMedReqID);
            long ret = new clsHRPTableService().DoExcute(strSql);
            if (ret > 0) return 1;
            else return 0;
        }

        //删除申请单	Status->0
        [AutoComplete]
        public long m_lngDeleteMedReq(string strPutMedReqID, string strEmpID)
        {
            string strSql = @" Update T_Bih_Opr_PutMedReq
							set Status_Int=0,Deactivate_Dat=sysdate,Deactivator_Chr='[EmpID]'
							where PutMedReqID_Chr = '[ReqID]' ";

            strSql = strSql.Replace("[ReqID]", strPutMedReqID);
            strSql = strSql.Replace("[EmpID]", strEmpID);
            long ret = new clsHRPTableService().DoExcute(strSql);
            if (ret > 0) return 1;
            else return 0;

        }



        //获取申请单信息
        [AutoComplete]
        public long m_lngGetMedReqByID(string strPutMedReqID, out clsPutMedReq objReq)
        {
            string strSql = @"select A.* ,B.LastName_VChr,C.deptname_vchr
							from T_Bih_Opr_PutMedReq A, T_BSE_Employee B , T_BSE_DeptDesc C
							where A.Creator_Chr=B.EmpID_Chr(+) and A.AreaID_Chr=C.DeptID_Chr(+)
							and  PutMedReqID_Chr = '[ReqID]' ";
            strSql = strSql.Replace("[ReqID]", strPutMedReqID);

            DataTable objDt = new DataTable();
            long ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDt);
            if ((ret > 0) && (objDt != null) && (objDt.Rows.Count > 0))
            {
                objReq = new clsPutMedReq();
                m_lngDataRowToMedReq(objDt.Rows[0], ref objReq);
                return 1;
            }
            else
            {
                objReq = null;
                return 0;
            }
        }

        [AutoComplete]
        private long m_lngDataRowToMedReq(DataRow objRow, ref clsPutMedReq objReq)
        {
            if (objReq == null) objReq = new clsPutMedReq();

            #region DataRow to ValueObject
            objReq.m_strPutMedReqID = clsConverter.ToString(objRow["PutMedReqID_Chr"]).Trim();
            objReq.m_strAreaID = clsConverter.ToString(objRow["AreaID_Chr"]).Trim();
            objReq.m_strDes = clsConverter.ToString(objRow["Des_VChr"]).Trim();
            objReq.m_intPStatus = clsConverter.ToInt(objRow["PStatus_Int"]);
            objReq.m_intStatus = clsConverter.ToInt(objRow["Status_Int"]);
            objReq.m_strDeactivator = clsConverter.ToString(objRow["Deactivator_Chr"]).Trim();
            objReq.m_dtDeactivateDate = clsConverter.ToDateTime(objRow["Deactivate_Dat"]);
            objReq.m_strCreator = clsConverter.ToString(objRow["Creator_Chr"]).Trim();
            objReq.m_dtCreateDate = clsConverter.ToDateTime(objRow["Create_Dat"]);
            objReq.m_strRangeDes = clsConverter.ToString(objRow["RangeDes_VChr"]).Trim();
            objReq.m_strProcessor = clsConverter.ToString(objRow["Processor_Chr"]).Trim();
            try { objReq.m_dtFinishDate = clsConverter.ToDateTime(objRow["Finish_Dat"]); }
            catch { }

            objReq.m_strBedBegin = clsConverter.ToString(objRow["BedBegin_VChr"]).Trim();
            objReq.m_strBedEnd = clsConverter.ToString(objRow["BedEnd_VChr"]).Trim();
            objReq.m_strExecType = clsConverter.ToString(objRow["ExecType_VChr"]).Trim();
            objReq.m_strRichType = clsConverter.ToString(objRow["RichType_VChr"]).Trim();
            objReq.m_strUsageType = clsConverter.ToString(objRow["UsageType_VChr"]).Trim();

            objReq.m_strCreatorName = clsConverter.ToString(objRow["LastName_VChr"]).Trim();
            objReq.m_strAreaName = clsConverter.ToString(objRow["deptname_vchr"]).Trim();
            #endregion

            return 1;
        }

        /// <summary>
        /// 获取申请单列表
        /// </summary>
        /// <param name="strAreaID"></param>
        /// <param name="strCreatorID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="arrStatus"></param>
        /// <param name="objDT"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedReqList(string strAreaID, string strCreatorID, DateTime dtBegin, DateTime dtEnd, int[] arrStatus, out DataTable objDT)
        {
            string strSql = @" select  A.putmedreqid_chr FlowNO,C.deptname_vchr DeptName
							,Trim(A.bedend_vchr) BedNo,A.exectype_vchr ,A.richtype_vchr
							,A.pstatus_int,A.des_vchr Des,A.creator_chr CreatorID,B.lastname_vchr CreatorName,
							to_char(A.Create_Dat,'yyyy-mm-dd hh24:mi') CreateDate,'' ExecType,'' RichType,'' PStatus							  
							from T_BIH_Opr_PutMedReq A,T_BSE_Employee B,T_BSE_DeptDesc C
							where A.Creator_Chr=B.EmpID_Chr(+)  and A.Status_Int=1
								and A.AreaID_Chr=C.DeptID_Chr(+)
							[StatusCondition]
							[AreaCondition]	[CreatorCondition]	[BeginDate]	[EndDate]
							order by PutMedReqID_Chr
							";

            objDT = null;

            #region Condition
            //
            if ((arrStatus == null) || (arrStatus.Length <= 0))
            {
                return 1;
            }

            //
            if (arrStatus.Length == 1)
            {
                strSql = strSql.Replace("[StatusCondition]", " and PStatus_Int = " + arrStatus[0].ToString());
            }
            else
            {
                string strStatus = "";
                for (int i = 0; i < arrStatus.Length; i++)
                {
                    if (i > 0) strStatus += ",";
                    strStatus += arrStatus[i].ToString();
                }
                strSql = strSql.Replace("[StatusCondition]", " and PStatus_Int in ( " + strStatus + " ) ");
            }

            //
            if (strAreaID.Trim() == "")
            {
                strSql = strSql.Replace("[AreaCondition]", "");
            }
            else
            {
                strSql = strSql.Replace("[AreaCondition]", " and AreaID_Chr='" + strAreaID.Trim() + "' ");
            }

            //
            if (strCreatorID.Trim() == "")
            {
                strSql = strSql.Replace("[CreatorCondition]", "");
            }
            else
            {
                strSql = strSql.Replace("[CreatorCondition]", " and Creator_Chr='" + strCreatorID + "' ");
            }

            //
            if (dtBegin == DateTimeNull.Value)
            {
                strSql = strSql.Replace("[BeginDate]", "");
            }
            else
            {
                strSql = strSql.Replace("[BeginDate]", " and Create_Dat >= to_date('" + dtBegin.ToString("yyyy-MM-dd") + "','yyyy-mm-dd')");
            }

            //
            if (dtEnd == DateTimeNull.Value)
            {
                strSql = strSql.Replace("[EndDate]", "");
            }
            else
            {
                strSql = strSql.Replace("[EndDate]", " and Create_Dat <= to_date('" + dtEnd.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')");
            }

            #endregion

            //
            DataTable objDt = new DataTable();
            long ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDt);
            if (ret > 0)
            {
                objDt.Columns.Add("NO", typeof(int));
                objDT = objDt;
                return 1;
            }
            else
            {
                return 0;
            }
        }


    }
    #endregion

    #region clsDeptLock
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsDeptLock : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        private string m_strLockDeptID = "";
        public clsDeptLock()
        {
        }
        public clsDeptLock(string p_strDeptID)
        {
            m_strLockDeptID = p_strDeptID;
        }
        [AutoComplete]
        public long s_lngLock(string strDeptID, string strEmpID, string strEmpName)
        {
            long lngRes = 0;
            lngRes = s_lngUnLock();
            if (strDeptID.Trim() != "")
            {
                //lock
                lngRes = m_lngUnLockDept(strDeptID);
                lngRes = m_lngLockDept(strDeptID, strEmpID, strEmpName);
                m_strLockDeptID = strDeptID;
            }
            return lngRes;
        }
        public string s_strCurrentLock
        {
            get
            {
                return m_strLockDeptID;
            }
            set
            {
                m_strLockDeptID = value;
            }
        }
        [AutoComplete]
        public long s_lngUnLock()
        {
            return s_lngUnLock(m_strLockDeptID);
        }
        [AutoComplete]
        public long s_lngUnLock(string p_strLockDeptID)
        {
            long lngRes = 1;
            if (m_strLockDeptID.Trim() != "")
            {
                //unlock
                lngRes = m_lngUnLockDept(p_strLockDeptID);
            }
            return lngRes;
        }

        [AutoComplete]
        public bool s_blnIsLock(string strDeptID, out DateTime dtLockDate, out string strLockEmpID, out string strLockEmpName)
        {
            long lngRes = 0;
            DateTime dtLock;
            string strEmpID, strEmpName;
            lngRes = m_lngGetDeptInfo(strDeptID.Trim(), out dtLock, out strEmpID, out strEmpName);
            if (lngRes > 0 && dtLock != DateTime.MinValue)
            {
                TimeSpan ts = DateTime.Now - dtLock;    //超时检测
                if (ts.Hours >= 12)
                {
                    lngRes = m_lngUnLockDept(strDeptID);
                }
                else
                {
                    dtLockDate = dtLock;
                    strLockEmpID = strEmpID;
                    strLockEmpName = strEmpName;
                    return true;
                }
            }
            //is not lock
            dtLockDate = DateTime.MinValue;
            strLockEmpID = "";
            strLockEmpName = "";
            return false;
        }

        [AutoComplete]
        public long m_lngGetDeptInfo(string strDeptID, out DateTime dtLockDate, out string strEmpID, out string strEmpName)
        {
            long lngRes = 0;
            dtLockDate = DateTime.MinValue;
            strEmpID = "";
            strEmpName = "";
            string strSql = @"select * from T_Opr_Bih_DeptLock where Trim(DeptID)=Trim('[DeptID]')";
            strSql = strSql.Replace("[DeptID]", strDeptID);
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSql, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    dtLockDate = Convert.ToDateTime(dtbResult.Rows[0]["LockDate"]);
                    strEmpID = dtbResult.Rows[0]["LockEmpID"].ToString().Trim();
                    strEmpName = dtbResult.Rows[0]["LockEmpName"].ToString().Trim();
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
        [AutoComplete]
        public long m_lngUnLockDept(string strDeptID)
        {
            long lngRes = 0;
            string strSql = @"DELETE FROM T_Opr_Bih_DeptLock WHERE TRIM(DeptID)='[DeptID]'";
            strSql = strSql.Replace("[DeptID]", strDeptID.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSql);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        [AutoComplete]
        public long m_lngLockDept(string strDeptID, string strEmpID, string strEmpName)
        {
            long lngRes = 0;
            string strSql = @"
			insert into T_Opr_Bih_DeptLock(DeptID,LockDate,LockEmpID,LockEmpName)
			values('[DeptID]',sysdate,'[EmpID]','[EmpName]')
			";
            strSql = strSql.Replace("[DeptID]", strDeptID);
            strSql = strSql.Replace("[EmpID]", strEmpID);
            strSql = strSql.Replace("[EmpName]", strEmpName);
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSql);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #region 徐斌辉加
        /// <summary>
        /// 给病区解锁	根据锁定人ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strLockEmpID">锁定人ID</param>
        /// <param name="p_intHour">小时数</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUnLockByLockEmpID(string p_strLockEmpID, int p_intHour)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSQL = @"DELETE FROM t_opr_bih_deptlock WHERE Trim(lockempid)='[LOCKEMPIDVALUE]'";
            strSQL = strSQL.Replace("[LOCKEMPIDVALUE]", p_strLockEmpID.Trim());
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
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
    #endregion
}
