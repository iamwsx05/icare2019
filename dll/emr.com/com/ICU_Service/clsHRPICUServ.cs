using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Text;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.MiddleTiers
{
	/// <summary>
	/// Summary description for clsHRPICUServ.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsHRPICUServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 获取ICU的趋势数据
		/// </summary>
		/// <param name="p_strStartTime">趋势开始时间</param>
		/// <param name="p_strEndTime">趋势结束时间</param>
		/// <param name="p_lngSamplyTime">趋势采样时间，以秒为单位</param>
		/// <param name="p_strInHospitalID">住院号</param>
		/// <param name="p_strTableName">数据表名称</param>
		/// <param name="p_strXMLSet">返回结果的Xml</param>
		/// <param name="p_intRows">返回结果的数据条数</param>
		/// <returns>
		/// -1，参数错误。
		/// 0，查询失败
		/// 1，成功查询。
		/// </returns>
		[AutoComplete]
		private long m_lngGetTrendData(string p_strStartTime,string p_strEndTime,long p_lngSamplyTime,string p_strInHospitalID,string p_strTableName,out string p_strXMLSet,out int p_intRows)
		{
			p_strXMLSet = "";
			p_intRows = 0;		

			if(p_strStartTime == null || p_strStartTime =="" ||
				p_strEndTime == null || p_strEndTime =="" ||
				p_strInHospitalID == null || p_strInHospitalID.Length == 0 )
				return -1;
	
			string strSQLSel= "select * from "+p_strTableName+" where DataCollectedTime=";
			string strSQLAnd = " and EID in(select distinct EID from EquipmentBed where rtrim(InHospitalID) = '"+p_strInHospitalID+"')";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                StringBuilder sbdSQLComm = new StringBuilder(strSQLSel.Length + strSQLAnd.Length + 17);

                DateTime dtmStartTime = DateTime.Parse(p_strStartTime);
                DateTime dtmEndTime = DateTime.Parse(p_strEndTime);



                StringBuilder sbdOutXml = new StringBuilder(1000);
                sbdOutXml.Append("<Data>");

                string strTempXML = "";
                int intTempRows = 0;

                while (dtmStartTime <= dtmEndTime)
                {
                    sbdSQLComm.Append(string.Intern(strSQLSel));
                    sbdSQLComm.Append(clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(dtmStartTime));
                    sbdSQLComm.Append(string.Intern(strSQLAnd));

                    long lngRes = objHRPServ.lngGetXMLTable(sbdSQLComm.ToString(), ref strTempXML, ref intTempRows);

                    if (lngRes <= 0)
                    {
                        p_intRows = 0;
                        return 0;
                    }

                    p_intRows++;

                    sbdOutXml.Append(strTempXML);

                    sbdSQLComm.Length = 0;

                    dtmStartTime = dtmStartTime.AddSeconds(p_lngSamplyTime);
                }

                sbdOutXml.Append("</Data>");

                p_strXMLSet = sbdOutXml.ToString();
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

			return 1;
		}

		/// <summary>
		/// 获取监护仪的趋势数据
		/// </summary>
		/// <param name="p_strStartTime">趋势开始时间</param>
		/// <param name="p_strEndTime">趋势结束时间</param>
		/// <param name="p_lngSamplyTime">趋势采样时间，以秒为单位</param>
		/// <param name="p_strInHospitalID">住院号</param>
		/// <param name="p_strXMLSet">返回结果的Xml</param>
		/// <param name="p_intRows">返回结果的数据条数</param>
		/// <returns>
		/// -1，参数错误。
		/// 0，查询失败
		/// 1，成功查询。
		/// </returns>
		[AutoComplete]
		public long m_lngGetCMSTrendData(string p_strStartTime,string p_strEndTime,long p_lngSamplyTime,string p_strInHospitalID,out string p_strXMLSet,out int p_intRows)
		{
			return m_lngGetTrendData(p_strStartTime,p_strEndTime,p_lngSamplyTime,p_strInHospitalID,"CMSData",out p_strXMLSet,out p_intRows);
		}

		/// <summary>
		/// 获取呼吸机的趋势数据
		/// </summary>
		/// <param name="p_strStartTime">趋势开始时间</param>
		/// <param name="p_strEndTime">趋势结束时间</param>
		/// <param name="p_lngSamplyTime">趋势采样时间，以秒为单位</param>
		/// <param name="p_strInHospitalID">住院号</param>
		/// <param name="p_strXMLSet">返回结果的Xml</param>
		/// <param name="p_intRows">返回结果的数据条数</param>
		/// <returns>
		/// -1，参数错误。
		/// 0，查询失败
		/// 1，成功查询。
		/// </returns>
		[AutoComplete]
		public long m_lngGetVentilatorTrendData(string p_strStartTime,string p_strEndTime,long p_lngSamplyTime,string p_strInHospitalID,out string p_strXMLSet,out int p_intRows)
		{
			return m_lngGetTrendData(p_strStartTime,p_strEndTime,p_lngSamplyTime,p_strInHospitalID,"VentilatorData",out p_strXMLSet,out p_intRows);
		}		

		/// <summary>
		/// 获取所有ICU仪器的趋势数据
		/// </summary>
		/// <param name="p_strStartTime">趋势开始时间</param>
		/// <param name="p_strEndTime">趋势结束时间</param>
		/// <param name="p_lngSamplyTime">趋势采样时间，以秒为单位</param>
		/// <param name="p_strInHospitalID">住院号</param>
		/// <param name="p_strCMSXMLSet">返回监护仪结果的Xml</param>
		/// <param name="p_intCMSRows">返回监护仪结果的数据条数</param>
		/// <param name="p_strVentilatorXMLSet">返回呼吸机结果的Xml</param>
		/// <param name="p_intVentilatorRows">返回呼吸机结果的数据条数</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetICUTrendData(string p_strStartTime,string p_strEndTime,long p_lngSamplyTime,string p_strInHospitalID,out string p_strCMSXMLSet,out int p_intCMSRows,out string p_strVentilatorXMLSet,out int p_intVentilatorRows,string p_strGEIP,out DataSet p_set)
		{
			long lngRes = m_lngGetTrendData(p_strStartTime,p_strEndTime,p_lngSamplyTime,p_strInHospitalID,"CMSData",out p_strCMSXMLSet,out p_intCMSRows);
            p_set = null;
            if (p_strGEIP.Trim().Length != 0)
            {
                m_lngGetTrendData(p_strStartTime, p_strEndTime, p_lngSamplyTime, "GEPARAM", p_strGEIP, ref p_set);
            }

			if(lngRes<=0)
			{
				p_strVentilatorXMLSet = "";
				p_intVentilatorRows = 0;
				return lngRes;
			}

			return m_lngGetTrendData(p_strStartTime,p_strEndTime,p_lngSamplyTime,p_strInHospitalID,"VentilatorData",out p_strVentilatorXMLSet,out p_intVentilatorRows);
		}

        /// <summary>
        /// 获取GE监护仪采集的参数.
        /// </summary>
        /// <param name="p_strStartTime">开始时间</param>
        /// <param name="p_strEndTime">结束时间</param>
        /// <param name="p_lngSamplyTime">间隔时间</param>
        /// <param name="p_strGEIP">监护仪IP</param>
        /// <param name="p_set">输出结果</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetTrendData(string p_strStartTime, string p_strEndTime, long p_lngSamplyTime,string p_strTableName,string p_strGEIP,ref DataSet p_set)
        {
            if (p_strStartTime == null || p_strStartTime == "" ||
                p_strEndTime == null || p_strEndTime == "" ||
                p_strGEIP == null || p_strGEIP.Length == 0)
                return -1;

            //string strSQLSel = "select * from " + p_strTableName + " where PARAMDATE=(select max(paramdate) from geparam where paramdate<=";
            //string strSQLAnd = " and MONITOR_IP = '" + p_strGEIP + "') and MONITOR_IP = '" + p_strGEIP + "'";
            string strSQLSel = "select * from " + p_strTableName + " where PARAMDATE=";
            string strSQLAnd = "and MONITOR_IP = '" + p_strGEIP + "'";

            p_set = new DataSet();

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                StringBuilder sbdSQLComm = new StringBuilder(strSQLSel.Length + strSQLAnd.Length + 17);

                DateTime dtmStartTime = DateTime.Parse(p_strStartTime);
                DateTime dtmEndTime = DateTime.Parse(p_strEndTime);

                while (dtmStartTime <= dtmEndTime)
                {
                    sbdSQLComm.Append(string.Intern(strSQLSel));
                    sbdSQLComm.Append(clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(dtmStartTime));
                    sbdSQLComm.Append(string.Intern(strSQLAnd));

                    DataTable dt = null;

                    //long lngRes = objHRPServ.lngGetXMLTable(sbdSQLComm.ToString(), ref strTempXML, ref intTempRows);
                    long lngRes = objHRPServ.DoGetDataTable(sbdSQLComm.ToString(), ref dt);
                    p_set.Tables.Add(dt);

                    if (lngRes <= 0)
                    {
                        //p_intRows = 0;
                        return 0;
                    }


                    sbdSQLComm.Length = 0;

                    dtmStartTime = dtmStartTime.AddSeconds(p_lngSamplyTime);
                }

                //sbdOutXml.Append("</Data>");

                //p_strXMLSet = sbdOutXml.ToString();
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

            return 1;
        }
		
		/// <summary>
		/// 得到GE参数，根据传入的时间
		/// </summary>
		/// <param name="p_strInHospitalID">病人ID</param>
		/// <param name="p_strCollectTime">要查找的数据的时间范围</param>
		/// <param name="p_strInHospitalTime">住院日期</param>
		/// <param name="p_dtGECMS">返回数据的结果集</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetICUGEDataByTime(string p_strInHospitalID,string p_strCollectTime,string p_strInHospitalTime,ref DataTable p_dtGECMS)
		{
			/*
			 * 注意，只要参数正确，返回值都是1。需要从返回条数查询是否有记录。
			 */
			#region Old
//			p_dtGECMS=null;
//
//			if(p_strCollectTime == null)
//				return -1;
//
//			//获取监护仪数据
//			/*string strSQL = @"select * from CMSData 
//							where eid in 
//							(
//							select distinct EID from EquipmentBed EB 
//							where trim(InHospitalID) ='"+p_strInHospitalID+@"' and trim(BedID) = '"+p_strBedID+@"' and  ToDate is null
//							)
//							and DataCollectedTime = (select Min(DataCollectedTime) from CMSData where DataCollectedTime = "+clsHRPTableService.s_strOracleDateTime(p_strCollectTime);
//			*/
//			string strSQL="select * from ICUGEPARAM where PARAMDATE=(select Min(PARAMDATE) from ICUGEPARAM where PARAMDATE = "+clsHRPTableService.s_strOracleDateTime(p_strCollectTime) + ")";
//			clsHRPTableService objHRPServ = new clsHRPTableService();
//
//			objHRPServ.lngGetDataTableWithoutParameters(strSQL,ref p_dtGECMS);
//			return 1;
			#endregion Old

			string[] strParamIDArry = new string[] {"0000001","0000002","0000003","0000004",
													"0000005","0000006","0000007","0000008",
													"0000009","0000010","0000011","0000012",
													"0000013","0000014"};

			//string[] strParamIDArry = new string[] {"0000001"};

			string SQLTable="";
			long res = -1;

			System.Collections.ArrayList altSQLTable=new System.Collections.ArrayList();

			clsHRPTableService objHRPServ = new clsHRPTableService();

			string strWhere="",strSQL="";

            try
            {
                if (p_strCollectTime.Trim().Length != 0)
                {
                    //				strWhere="PARAMDATE<=to_date('" + p_strCollectTime +"','yyyy-mm-dd hh24:mi:ss')";
                    strWhere = "PARAMDATE<=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCollectTime);
                }
                else
                {
                    //				strWhere="PARAMDATE<=sysdate";
                    strWhere = "PARAMDATE<=" + clsDatabaseSQLConvert.s_StrGetServDateFuncName;
                }

                for (int i = 0; i < strParamIDArry.Length; i++)
                {
                    string strFieldName = strParamIDArry[i];
                    if (clsHRPTableService.bytDatabase_Selector == 0)
                        SQLTable = "select PARAM_ID, PARAM_VALUE from icugeparam where INPATIENTID='" + p_strInHospitalID.Replace("'", "''") + "' " +
                            "and INPATIENTDATE=CONVERT(DATETIME,'" + p_strInHospitalTime + "',20) " +
                            "and PARAMDATE=(select max(PARAMDATE) from icugeparam where INPATIENTID='" + p_strInHospitalID.Replace("'", "''") + "' " +
                            "and INPATIENTDATE=CONVERT(DATETIME,'" + p_strInHospitalTime + "',20) " +
                            "and " + strWhere + " and PARAM_ID='" + strFieldName + "' and STATUS=0) " +
                            "and PARAM_ID='" + strFieldName + "' and STATUS=0";
                    else
                        SQLTable = "select PARAM_ID, PARAM_VALUE from icugeparam where INPATIENTID='" + p_strInHospitalID.Replace("'", "''") + "' " +
                            "and INPATIENTDATE=to_date('" + p_strInHospitalTime + "','yyyy-mm-dd hh24:mi:ss') " +
                            "and PARAMDATE=(select max(PARAMDATE) from icugeparam where INPATIENTID='" + p_strInHospitalID.Replace("'", "''") + "' " +
                            "and INPATIENTDATE=to_date('" + p_strInHospitalTime + "','yyyy-mm-dd hh24:mi:ss') " +
                            "and " + strWhere + " and PARAM_ID='" + strFieldName + "' and STATUS=0) " +
                            "and PARAM_ID='" + strFieldName + "' and STATUS=0";
                    altSQLTable.Add(SQLTable);
                }

                for (int i = 0; i < altSQLTable.Count; i++)
                {
                    if (i == 0)
                        strSQL = altSQLTable[i].ToString();
                    else
                        strSQL = strSQL + " union all " + altSQLTable[i].ToString();
                }

                res = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtGECMS);
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
			
			return res;
		}

		/// <summary>
		/// 根据仪器IP和时间获取GE参数信息
		/// </summary>
		/// <param name="p_strMONITOR_IP">仪器IP</param>
		/// <param name="p_strCollectTime">获取记录的事件范围 最接近此时间的记录</param>
		/// <param name="p_dtGECMS">返回值</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetICUGEDataByTime(string p_strMONITOR_IP,string p_strCollectTime,ref DataTable p_dtGECMS)
		{
			string[] strParamIDArry = new string[] {"0000001","0000002","0000003","0000004",
													   "0000005","0000006","0000007","0000008",
													   "0000009","0000010","0000011","0000012",
													   "0000013","0000014"};

			//string[] strParamIDArry = new string[] {"0000001"};

			string SQLTable="";
			long res = -1;

			System.Collections.ArrayList altSQLTable=new System.Collections.ArrayList();

			clsHRPTableService objHRPServ = new clsHRPTableService();

			string strWhere="",strSQL="";
            try
            {

                if (p_strCollectTime.Trim().Length != 0)
                {
                    //				strWhere="PARAMDATE<=to_date('" + p_strCollectTime +"','yyyy-mm-dd hh24:mi:ss')";
                    strWhere = "PARAMDATE<=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCollectTime);
                }
                else
                {
                    //				strWhere="PARAMDATE<=sysdate";
                    strWhere = "PARAMDATE<=" + clsDatabaseSQLConvert.s_StrGetServDateFuncName;
                }

                for (int i = 0; i < strParamIDArry.Length; i++)
                {
                    string strFieldName = strParamIDArry[i];
                    SQLTable = "select PARAM_ID, PARAM_VALUE from GEPARAM where MONITOR_IP='" + p_strMONITOR_IP.Replace("'", "''") + "' " +
                        "and PARAMDATE=(select max(PARAMDATE) from GEPARAM where MONITOR_IP='" + p_strMONITOR_IP.Replace("'", "''") + "' " +
                        "and " + strWhere + " and PARAM_ID='" + strFieldName + "' and STATUS=0) " +
                        "and PARAM_ID='" + strFieldName + "' and STATUS=0";
                    altSQLTable.Add(SQLTable);
                }

                for (int i = 0; i < altSQLTable.Count; i++)
                {
                    if (i == 0)
                        strSQL = altSQLTable[i].ToString();
                    else
                        strSQL = strSQL + " union all " + altSQLTable[i].ToString();
                }

                res = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref p_dtGECMS);
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
			return res;
		}

		/// <summary>
		/// 获取最接近给定时间的ICU数值。
		/// </summary>
		/// <param name="p_strInHospitalID">住院号</param>
		/// <param name="p_strBedID">床位号</param>
		/// <param name="p_strCollectTime">采集时间。（该时间之后，当天内最接近的数值）</param>
		/// <param name="p_strCMSXml">返回监护仪数据的XML</param>
		/// <param name="p_intCMSRows">返回监护仪数据的条数</param>
		/// <param name="p_strVenXml">返回呼吸机数据的XML</param>
		/// <param name="p_intVenRows">返回呼吸机数据的条数</param>
		/// <returns>
		/// -1，参数错误。
		/// 0，查询失败
		/// 1，成功查询。
		/// </returns>
		[AutoComplete]
		public long m_lngGetICUDataByTime(string p_strInHospitalID,string p_strBedID,string p_strCollectTime,out string p_strCMSXml,out int p_intCMSRows,out string p_strVenXml,out int p_intVenRows)
		{
			/*
			 * 注意，只要参数正确，返回值都是1。需要从返回条数查询是否有记录。
			 */

			p_strCMSXml = "";
			p_intCMSRows = 0;
			p_strVenXml = "";
			p_intVenRows = 0;

			if(p_strBedID == null ||p_strCollectTime == null)
				return -1;

			//获取监护仪数据
			string strSQL = @"select * from CMSData 
							where eid in 
							(
							select distinct EID from EquipmentBed EB 
							where rtrim(InHospitalID) ='"+p_strInHospitalID+@"' and rtrim(BedID) = '"+p_strBedID+@"' and  ToDate is null
							)
							and DataCollectedTime = (select Min(DataCollectedTime) from CMSData where DataCollectedTime = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCollectTime)+")";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                objHRPServ.lngGetXMLTable(strSQL, ref p_strCMSXml, ref p_intCMSRows);

                //获取呼吸机数据
                strSQL = @"select * from VentilatorData 
					where eid in 
					(
					select distinct EID from EquipmentBed EB 
					where rtrim(InHospitalID) ='" + p_strInHospitalID + @"' and rtrim(BedID) = '" + p_strBedID + @"' and ToDate is null
					)
					and DataCollectedTime = (select Min(DataCollectedTime) from VentilatorData where DataCollectedTime = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCollectTime) + ")";

                objHRPServ.lngGetXMLTable(strSQL, ref p_strVenXml, ref p_intVenRows);
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
			
			return 1;
		}

		/// <summary>
		/// 获得ICU设备的信息
		/// </summary>
		/// <param name="p_strInHospitalID">住院号</param>
		/// <param name="p_strXML">返回设备信息的XML</param>
		/// <param name="p_intRows">返回设备信息的条数</param>
		/// <returns>
		/// -1，参数错误。
		/// 0，查询失败
		/// 1，成功查询。
		/// </returns>
		[AutoComplete]
		public long m_lngGetICUEquipmentInfo(string p_strInHospitalID,out string p_strXML,out int p_intRows)
		{
			long res = -1;
			p_intRows = 0;
			p_strXML = "";
			string strSQL = @"SELECT DISTINCT EBI.EquipmentID,EBI.EquipmentName,EM.EquipmentModelName,ET.EquipmentTypeName
							  FROM EquipmentBaseInfo EBI, EquipmentModel EM,EquipmentType ET
							  WHERE EBI.EquipmentID = EM.EquipmentID AND EBI.ModelID = EM.EquipmentModelID 
							  AND   EBI.TypeID = ET.EquipmentTypeID AND EBI.TypeID = EM.EquipmentTypeID
							  AND EBI.Status =0 AND EM.Status =0 AND ET.Status =0";
			clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                res = objHRPServ.lngGetXMLTable(strSQL, ref p_strXML, ref p_intRows);
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
			return res; 
		}

		/// <summary>
		/// 获取病人使用过的仪器。
		/// </summary>
		/// <param name="p_strInHospitalID">住院号</param>
		/// <param name="p_strXML">返回设备信息的XML</param>
		/// <param name="p_intRows">返回设备信息的条数</param>
		/// <returns>
		/// -1，参数错误。
		/// 0，查询失败
		/// 1，成功查询。
		/// </returns>
		[AutoComplete]
		public long m_lngGetUsedICUEquipment(string p_strInHospitalID,out string p_strXML,out int p_intRows)
		{
			p_intRows = 0;
			p_strXML = "";
			long res = -1;
			string strSQL = "select EID,FromDate,ToDate from EquipmentBed where rtrim(InHospitalID) = '"+p_strInHospitalID+"' order by FromDate";
			clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                res = objHRPServ.lngGetXMLTable(strSQL, ref p_strXML, ref p_intRows);
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
			return res;
		}

		/// <summary>
		/// 根据时间取得此次住院中各项指定的ICU监护仪，呼吸机的参数的最新数据
		/// </summary>
		/// <param name="p_strInHospitalID">病人住院号</param>
		/// <param name="p_strBedID">病床</param>
		/// <param name="p_strInHospitalTime">住院时间</param>
		/// <param name="p_strCollectTime">指定取值的时间（为空就取当前时间）</param>
		/// <param name="p_strTypeArry">获取参数的数组(【HEARTRATE】心律，【PULSERATE】脉搏，【NPB】无创血压，【NPBSYSTOLIC】无创收缩压，【NPB_DIASTOLIC】无创舒张压，【SPO21】血氧饱和度，【TEMP1】体温，【RESPRATE】呼吸频率，【O2CONCENTRATION】，【ENDEXPPRESSURE】，【EXPTIDALVOLUME】，【PEAKPRESSURE】，【BLOODNUM1】，【RESPDETECTNUM】监护仪呼吸数据)</param>
		/// <param name="p_dtCMS">返回的菲利普监护仪的数据集</param>
		/// <param name="p_dtVen">返回的西门子呼吸机的数据集</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetICUNumericParmat(string p_strInHospitalID,
											 string p_strBedID,
											 string p_strInHospitalTime,
											 string p_strCollectTime,
											 string[] p_strTypeArry,
											 ref DataTable p_dtCMS,
											 ref DataTable p_dtVen)
		{
			if (p_strTypeArry==null || p_strTypeArry.Length==0)
				return -1;
			
			string SQLTable="",SQLWhere_2="",SQLTable_2="",SQL="",strFieldName="";string strCMSSQLWhere = "";string strVenSQLWhere = "";
	        clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                System.Collections.ArrayList arlSQL = new System.Collections.ArrayList();


                if (p_strCollectTime.Trim().Length == 0 || p_strCollectTime == null)
                {
                    SQLTable = "select distinct EID from EquipmentBed " +
                        "where FROMDATE>=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInHospitalTime) +
                        "and FROMDATE<=" + clsDatabaseSQLConvert.s_StrGetServDateFuncName +
                        "and rtrim(InHospitalID) ='" + p_strInHospitalID + "' and rtrim(BedID) = '" + p_strBedID + "'";

                    SQLWhere_2 = "DATACOLLECTEDTIME>=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInHospitalTime) +
                        "and DATACOLLECTEDTIME<=" + clsDatabaseSQLConvert.s_StrGetServDateFuncName;

                    //				SQLTable="(select distinct EID from EquipmentBed " + 
                    //						"where FROMDATE>=to_date('" + p_strInHospitalTime + "','yyyy-mm-dd hh24:mi:ss') " +
                    //						"and FROMDATE<=sysdate "+
                    //						"and trim(InHospitalID) ='"+ p_strInHospitalID + "' and trim(BedID) = '" + p_strBedID + "')";
                    //
                    //				SQLWhere_2="DATACOLLECTEDTIME>=to_date('" + p_strInHospitalTime + "','yyyy-mm-dd hh24:mi:ss') " +
                    //						"and DATACOLLECTEDTIME<=sysdate";
                }
                else
                {
                    SQLTable = "select distinct EID from EquipmentBed " +
                        "where FROMDATE>=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInHospitalTime) +
                        "and FROMDATE<=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCollectTime) +
                        "and rtrim(InHospitalID) ='" + p_strInHospitalID + "' and rtrim(BedID) = '" + p_strBedID + "'";

                    SQLWhere_2 = "DATACOLLECTEDTIME>=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInHospitalTime) +
                        "and DATACOLLECTEDTIME<=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCollectTime);

                    //				SQLTable="(select distinct EID from EquipmentBed " + 
                    //						"where FROMDATE>=to_date('" + p_strInHospitalTime + "','yyyy-mm-dd hh24:mi:ss') " +
                    //						"and FROMDATE<=to_date('" + p_strCollectTime + "','yyyy-mm-dd hh24:mi:ss') " +
                    //						"and trim(InHospitalID) ='"+ p_strInHospitalID + "' and trim(BedID) = '" + p_strBedID + "')";
                    //
                    //				SQLWhere_2="DATACOLLECTEDTIME>=to_date('" + p_strInHospitalTime + "','yyyy-mm-dd hh24:mi:ss') " +
                    //						"and DATACOLLECTEDTIME<=to_date('" + p_strCollectTime + "','yyyy-mm-dd hh24:mi:ss')";

                }

                DataTable dtRecord = null;
                string strEID = "";

                objHRPServ.lngGetDataTableWithoutParameters(SQLTable, ref dtRecord);

                if (dtRecord != null)
                {
                    for (int i = 0; i < dtRecord.Rows.Count; i++)
                    {
                        strEID += "'" + dtRecord.Rows[i]["EID"].ToString() + "'" + ",";
                    }
                }

                if (strEID.Trim().Length == 0)
                    strEID = "''";
                else
                {
                    strEID = strEID.Substring(0, strEID.Length - 1);
                    strEID = "(" + strEID + ")";
                }


                #region 获取菲利普监护仪数据
                //"select max(DATACOLLECTEDTIME) from cmsdata where Length(rtrim(" + strFieldName + "))!=0 AND eid=b.eid";

                for (int i = 0; i < p_strTypeArry.Length; i++)
                {
                    SQLTable_2 = "";
                    switch (p_strTypeArry[i].ToUpper())
                    {
                        case "HEARTRATE"://心律
                            strFieldName = "HEARTRATE";
                            //						SQLField="(select '1' PK," + strFieldName + " from CMSDATA where EID in (" + SQLWhere_1 + ") " +
                            //							"and " + SQLWhere_2 + " " +
                            //							"and Length(trim(" + strFieldName + "))!=0 and rownum=1 order by DATACOLLECTEDTIME desc) " + strTableAlias + ""; 
                            //SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                            //	"CMSDATA a," + SQLTable + " b where a.eid=b.eid and DATACOLLECTEDTIME=(select max(DATACOLLECTEDTIME) from cmsdata where Length(rtrim(" + strFieldName + "))!=0 AND eid=b.eid)";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "PULSERATE"://脉搏
                            strFieldName = "PULSERATE";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "NPB"://无创血压
                            strFieldName = "NPB";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "NPBSYSTOLIC"://无创收缩压
                            strFieldName = "NPBSYSTOLIC";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "NPB_DIASTOLIC"://无创舒张压
                            strFieldName = "NPB_DIASTOLIC";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "SPO21"://血氧饱和度
                            strFieldName = "SPO21";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "TEMP1"://体温
                            strFieldName = "TEMP1";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "BLOODNUM1":
                            strFieldName = "BLOODNUM1";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "RESPDETECTNUM":
                            strFieldName = "RESPDETECTNUM";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;

                    }
                    if (SQLTable_2.Trim().Length > 0)
                    {
                        arlSQL.Add(SQLTable_2);
                    }
                }

                if (arlSQL.Count > 0)
                {
                    SQL = "";
                    for (int i = 0; i < arlSQL.Count; i++)
                    {
                        if (i == 0)
                        {
                            SQL = arlSQL[i].ToString();
                        }
                        else
                        {
                            SQL = SQL + " union all " + arlSQL[i];
                        }
                    }

                    if (SQL.Trim().Length > 0)
                        objHRPServ.lngGetDataTableWithoutParameters(SQL, ref p_dtCMS);
                }
                #endregion 获取菲利普监护仪数据

                #region 获取西门子呼吸机数据
                SQLTable_2 = ""; SQL = ""; strFieldName = "";
                arlSQL.Clear();

                for (int i = 0; i < p_strTypeArry.Length; i++)
                {
                    SQLTable_2 = "";
                    switch (p_strTypeArry[i].ToUpper())
                    {
                        case "RESPRATE"://呼吸频率
                            strFieldName = "RESPRATE";
                            strVenSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from VENTILATORDATA where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "VENTILATORDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME=" + strVenSQLWhere + "";
                            break;
                        case "O2CONCENTRATION"://
                            strFieldName = "O2CONCENTRATION";
                            strVenSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from VENTILATORDATA where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "VENTILATORDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME=" + strVenSQLWhere + "";
                            break;
                        case "ENDEXPPRESSURE"://
                            strFieldName = "ENDEXPPRESSURE";
                            strVenSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from VENTILATORDATA where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "VENTILATORDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME=" + strVenSQLWhere + "";
                            break;
                        case "EXPTIDALVOLUME"://
                            strFieldName = "EXPTIDALVOLUME";
                            strVenSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from VENTILATORDATA where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "VENTILATORDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME=" + strVenSQLWhere + "";
                            break;
                        case "PEAKPRESSURE"://
                            strFieldName = "PEAKPRESSURE";
                            strVenSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from VENTILATORDATA where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "VENTILATORDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME=" + strVenSQLWhere + "";
                            break;

                    }

                    if (SQLTable_2.Trim().Length > 0)
                    {
                        arlSQL.Add(SQLTable_2);
                    }
                }

                if (arlSQL.Count > 0)
                {
                    SQL = "";
                    for (int i = 0; i < arlSQL.Count; i++)
                    {
                        if (i == 0)
                        {
                            SQL = arlSQL[i].ToString();
                        }
                        else
                        {
                            SQL = SQL + " union all " + arlSQL[i];
                        }
                    }

                    if (SQL.Trim().Length > 0)
                        objHRPServ.lngGetDataTableWithoutParameters(SQL, ref p_dtVen);
                }
                #endregion 获取西门子呼吸机数据
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
			return 0;
		}

		/// <summary>
		/// 存储ICU趋势分析用户设定的颜色等属性。
		/// </summary>
		/// <param name="p_strFormName">趋势分析的窗体名称</param>
		/// <param name="p_intMonitorColor">表示要储存的颜色属性属于那一部分（0:表示监护仪，1:表示呼吸机）</param>
		/// <param name="p_strVenColor">存放颜色属性的XML形式的内容</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveICUTrendColorAttrib(string p_strFormName,int p_intMonitorColor,string p_strVenColor)
		{
			string SQL = "";
			long lngRet = 0;
			long lngRetCount = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                SQL = "delete from ICUCOLORPROPERTY where FORMNAME = ? and TYPEID = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strFormName.Replace("'", "''");
                objDPArr[1].Value = p_intMonitorColor;

                lngRet = objHRPServ.lngExecuteParameterSQL(SQL, ref lngRetCount, objDPArr);

                if (lngRet < 0)
                    return lngRet;

                //SQL = "insert into ICUCOLORPROPERTY (FORMNAME,TYPEID,XMLCONTENT) values ('" + p_strFormName.Replace("'","''") + "'," + p_intMonitorColor + ",'" + p_strVenColor.Replace("'","''") + "')";
                SQL = "insert into ICUCOLORPROPERTY (FORMNAME,TYPEID,XMLCONTENT) values (?,?,?)";

                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strFormName;
                objDPArr[1].Value = p_intMonitorColor;
                objDPArr[2].Value = p_strVenColor;


                lngRet = objHRPServ.lngExecuteParameterSQL(SQL, ref lngRetCount, objDPArr);
                return lngRet;

                //lngRet = objHRPServ.DoExcute(SQL);
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
			return lngRet;
		}

		/// <summary>
		/// 得到存储的ICU趋势分析的颜色配置内容
		/// </summary>
		/// <param name="p_strFormName">ICU趋势分析的窗体名称</param>
		/// <param name="p_dtRecord">返回数据的结果集</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetICUTrendColorAttrib(string p_strFormName,ref DataTable p_dtRecord)
		{
			string SQL ="select TYPEID,XMLCONTENT from ICUCOLORPROPERTY where FORMNAME = ?";
			long lngRet = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strFormName.Replace("'", "''");

                lngRet = objHRPServ.lngGetDataTableWithParameters(SQL, ref p_dtRecord, objDPArr);
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
			return lngRet;
		}

		/// <summary>
		/// 获取ICU中GE控件的波形的样式颜色等信息。
		/// </summary>
		/// <param name="p_dtRecord"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetICUGEWaveParam(ref DataTable p_dtRecord)
		{
			string SQL = "select * from icugewaveparam";
			long lngRet = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                lngRet = objHRPServ.DoGetDataTable(SQL, ref p_dtRecord);
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
			return lngRet;
		}
		/// <summary>
		/// 获取ICU中GE控件的波形的样式颜色等信息。
		/// </summary>
		/// <param name="p_dtRecord"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetICUGEParam(ref DataTable p_dtRecordT,ref DataTable p_dtRecordV)
		{
			string SQL = "select * from icugeparamtitle";
			long lngRet = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                lngRet = objHRPServ.DoGetDataTable(SQL, ref p_dtRecordT);
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
			return lngRet;
		}
	}
}
