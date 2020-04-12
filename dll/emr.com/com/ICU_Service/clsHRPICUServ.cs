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
		/// ��ȡICU����������
		/// </summary>
		/// <param name="p_strStartTime">���ƿ�ʼʱ��</param>
		/// <param name="p_strEndTime">���ƽ���ʱ��</param>
		/// <param name="p_lngSamplyTime">���Ʋ���ʱ�䣬����Ϊ��λ</param>
		/// <param name="p_strInHospitalID">סԺ��</param>
		/// <param name="p_strTableName">���ݱ�����</param>
		/// <param name="p_strXMLSet">���ؽ����Xml</param>
		/// <param name="p_intRows">���ؽ������������</param>
		/// <returns>
		/// -1����������
		/// 0����ѯʧ��
		/// 1���ɹ���ѯ��
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
		/// ��ȡ�໤�ǵ���������
		/// </summary>
		/// <param name="p_strStartTime">���ƿ�ʼʱ��</param>
		/// <param name="p_strEndTime">���ƽ���ʱ��</param>
		/// <param name="p_lngSamplyTime">���Ʋ���ʱ�䣬����Ϊ��λ</param>
		/// <param name="p_strInHospitalID">סԺ��</param>
		/// <param name="p_strXMLSet">���ؽ����Xml</param>
		/// <param name="p_intRows">���ؽ������������</param>
		/// <returns>
		/// -1����������
		/// 0����ѯʧ��
		/// 1���ɹ���ѯ��
		/// </returns>
		[AutoComplete]
		public long m_lngGetCMSTrendData(string p_strStartTime,string p_strEndTime,long p_lngSamplyTime,string p_strInHospitalID,out string p_strXMLSet,out int p_intRows)
		{
			return m_lngGetTrendData(p_strStartTime,p_strEndTime,p_lngSamplyTime,p_strInHospitalID,"CMSData",out p_strXMLSet,out p_intRows);
		}

		/// <summary>
		/// ��ȡ����������������
		/// </summary>
		/// <param name="p_strStartTime">���ƿ�ʼʱ��</param>
		/// <param name="p_strEndTime">���ƽ���ʱ��</param>
		/// <param name="p_lngSamplyTime">���Ʋ���ʱ�䣬����Ϊ��λ</param>
		/// <param name="p_strInHospitalID">סԺ��</param>
		/// <param name="p_strXMLSet">���ؽ����Xml</param>
		/// <param name="p_intRows">���ؽ������������</param>
		/// <returns>
		/// -1����������
		/// 0����ѯʧ��
		/// 1���ɹ���ѯ��
		/// </returns>
		[AutoComplete]
		public long m_lngGetVentilatorTrendData(string p_strStartTime,string p_strEndTime,long p_lngSamplyTime,string p_strInHospitalID,out string p_strXMLSet,out int p_intRows)
		{
			return m_lngGetTrendData(p_strStartTime,p_strEndTime,p_lngSamplyTime,p_strInHospitalID,"VentilatorData",out p_strXMLSet,out p_intRows);
		}		

		/// <summary>
		/// ��ȡ����ICU��������������
		/// </summary>
		/// <param name="p_strStartTime">���ƿ�ʼʱ��</param>
		/// <param name="p_strEndTime">���ƽ���ʱ��</param>
		/// <param name="p_lngSamplyTime">���Ʋ���ʱ�䣬����Ϊ��λ</param>
		/// <param name="p_strInHospitalID">סԺ��</param>
		/// <param name="p_strCMSXMLSet">���ؼ໤�ǽ����Xml</param>
		/// <param name="p_intCMSRows">���ؼ໤�ǽ������������</param>
		/// <param name="p_strVentilatorXMLSet">���غ����������Xml</param>
		/// <param name="p_intVentilatorRows">���غ������������������</param>
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
        /// ��ȡGE�໤�ǲɼ��Ĳ���.
        /// </summary>
        /// <param name="p_strStartTime">��ʼʱ��</param>
        /// <param name="p_strEndTime">����ʱ��</param>
        /// <param name="p_lngSamplyTime">���ʱ��</param>
        /// <param name="p_strGEIP">�໤��IP</param>
        /// <param name="p_set">������</param>
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
		/// �õ�GE���������ݴ����ʱ��
		/// </summary>
		/// <param name="p_strInHospitalID">����ID</param>
		/// <param name="p_strCollectTime">Ҫ���ҵ����ݵ�ʱ�䷶Χ</param>
		/// <param name="p_strInHospitalTime">סԺ����</param>
		/// <param name="p_dtGECMS">�������ݵĽ����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetICUGEDataByTime(string p_strInHospitalID,string p_strCollectTime,string p_strInHospitalTime,ref DataTable p_dtGECMS)
		{
			/*
			 * ע�⣬ֻҪ������ȷ������ֵ����1����Ҫ�ӷ���������ѯ�Ƿ��м�¼��
			 */
			#region Old
//			p_dtGECMS=null;
//
//			if(p_strCollectTime == null)
//				return -1;
//
//			//��ȡ�໤������
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
		/// ��������IP��ʱ���ȡGE������Ϣ
		/// </summary>
		/// <param name="p_strMONITOR_IP">����IP</param>
		/// <param name="p_strCollectTime">��ȡ��¼���¼���Χ ��ӽ���ʱ��ļ�¼</param>
		/// <param name="p_dtGECMS">����ֵ</param>
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
		/// ��ȡ��ӽ�����ʱ���ICU��ֵ��
		/// </summary>
		/// <param name="p_strInHospitalID">סԺ��</param>
		/// <param name="p_strBedID">��λ��</param>
		/// <param name="p_strCollectTime">�ɼ�ʱ�䡣����ʱ��֮�󣬵�������ӽ�����ֵ��</param>
		/// <param name="p_strCMSXml">���ؼ໤�����ݵ�XML</param>
		/// <param name="p_intCMSRows">���ؼ໤�����ݵ�����</param>
		/// <param name="p_strVenXml">���غ��������ݵ�XML</param>
		/// <param name="p_intVenRows">���غ��������ݵ�����</param>
		/// <returns>
		/// -1����������
		/// 0����ѯʧ��
		/// 1���ɹ���ѯ��
		/// </returns>
		[AutoComplete]
		public long m_lngGetICUDataByTime(string p_strInHospitalID,string p_strBedID,string p_strCollectTime,out string p_strCMSXml,out int p_intCMSRows,out string p_strVenXml,out int p_intVenRows)
		{
			/*
			 * ע�⣬ֻҪ������ȷ������ֵ����1����Ҫ�ӷ���������ѯ�Ƿ��м�¼��
			 */

			p_strCMSXml = "";
			p_intCMSRows = 0;
			p_strVenXml = "";
			p_intVenRows = 0;

			if(p_strBedID == null ||p_strCollectTime == null)
				return -1;

			//��ȡ�໤������
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

                //��ȡ����������
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
		/// ���ICU�豸����Ϣ
		/// </summary>
		/// <param name="p_strInHospitalID">סԺ��</param>
		/// <param name="p_strXML">�����豸��Ϣ��XML</param>
		/// <param name="p_intRows">�����豸��Ϣ������</param>
		/// <returns>
		/// -1����������
		/// 0����ѯʧ��
		/// 1���ɹ���ѯ��
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
		/// ��ȡ����ʹ�ù���������
		/// </summary>
		/// <param name="p_strInHospitalID">סԺ��</param>
		/// <param name="p_strXML">�����豸��Ϣ��XML</param>
		/// <param name="p_intRows">�����豸��Ϣ������</param>
		/// <returns>
		/// -1����������
		/// 0����ѯʧ��
		/// 1���ɹ���ѯ��
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
		/// ����ʱ��ȡ�ô˴�סԺ�и���ָ����ICU�໤�ǣ��������Ĳ�������������
		/// </summary>
		/// <param name="p_strInHospitalID">����סԺ��</param>
		/// <param name="p_strBedID">����</param>
		/// <param name="p_strInHospitalTime">סԺʱ��</param>
		/// <param name="p_strCollectTime">ָ��ȡֵ��ʱ�䣨Ϊ�վ�ȡ��ǰʱ�䣩</param>
		/// <param name="p_strTypeArry">��ȡ����������(��HEARTRATE�����ɣ���PULSERATE����������NPB���޴�Ѫѹ����NPBSYSTOLIC���޴�����ѹ����NPB_DIASTOLIC���޴�����ѹ����SPO21��Ѫ�����Ͷȣ���TEMP1�����£���RESPRATE������Ƶ�ʣ���O2CONCENTRATION������ENDEXPPRESSURE������EXPTIDALVOLUME������PEAKPRESSURE������BLOODNUM1������RESPDETECTNUM���໤�Ǻ�������)</param>
		/// <param name="p_dtCMS">���صķ����ռ໤�ǵ����ݼ�</param>
		/// <param name="p_dtVen">���ص������Ӻ����������ݼ�</param>
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


                #region ��ȡ�����ռ໤������
                //"select max(DATACOLLECTEDTIME) from cmsdata where Length(rtrim(" + strFieldName + "))!=0 AND eid=b.eid";

                for (int i = 0; i < p_strTypeArry.Length; i++)
                {
                    SQLTable_2 = "";
                    switch (p_strTypeArry[i].ToUpper())
                    {
                        case "HEARTRATE"://����
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
                        case "PULSERATE"://����
                            strFieldName = "PULSERATE";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "NPB"://�޴�Ѫѹ
                            strFieldName = "NPB";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "NPBSYSTOLIC"://�޴�����ѹ
                            strFieldName = "NPBSYSTOLIC";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "NPB_DIASTOLIC"://�޴�����ѹ
                            strFieldName = "NPB_DIASTOLIC";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "SPO21"://Ѫ�����Ͷ�
                            strFieldName = "SPO21";
                            strCMSSQLWhere = "(select DATACOLLECTEDTIME from (select DATACOLLECTEDTIME,EID from cmsdata where Length(rtrim(" + strFieldName + ")) != 0 and eid in " + strEID + " order by DATACOLLECTEDTIME desc) where rownum = 1)";
                            SQLTable_2 = "select '" + strFieldName + "' ID," + strFieldName + " F_Value from " +
                                "CMSDATA a" + " where a.eid in " + strEID + " and DATACOLLECTEDTIME= " + strCMSSQLWhere + "";
                            break;
                        case "TEMP1"://����
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
                #endregion ��ȡ�����ռ໤������

                #region ��ȡ�����Ӻ���������
                SQLTable_2 = ""; SQL = ""; strFieldName = "";
                arlSQL.Clear();

                for (int i = 0; i < p_strTypeArry.Length; i++)
                {
                    SQLTable_2 = "";
                    switch (p_strTypeArry[i].ToUpper())
                    {
                        case "RESPRATE"://����Ƶ��
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
                #endregion ��ȡ�����Ӻ���������
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
		/// �洢ICU���Ʒ����û��趨����ɫ�����ԡ�
		/// </summary>
		/// <param name="p_strFormName">���Ʒ����Ĵ�������</param>
		/// <param name="p_intMonitorColor">��ʾҪ�������ɫ����������һ���֣�0:��ʾ�໤�ǣ�1:��ʾ��������</param>
		/// <param name="p_strVenColor">�����ɫ���Ե�XML��ʽ������</param>
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
		/// �õ��洢��ICU���Ʒ�������ɫ��������
		/// </summary>
		/// <param name="p_strFormName">ICU���Ʒ����Ĵ�������</param>
		/// <param name="p_dtRecord">�������ݵĽ����</param>
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
		/// ��ȡICU��GE�ؼ��Ĳ��ε���ʽ��ɫ����Ϣ��
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
		/// ��ȡICU��GE�ؼ��Ĳ��ε���ʽ��ɫ����Ϣ��
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
