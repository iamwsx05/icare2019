using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService; 

namespace com.digitalwave.clsDocVueSyncService
{
	/// <summary>
	/// Summary description for clsDocVueSyncService.
	/// 同步DocVue数据
	/// Alex 2003-8-7
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsDocVueSyncService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 添加记录到HRPGZNO1的SQL
		/// </summary>
		private const string c_strAddNewRecordSQL = @"insert into v_trend2 values(?,?,?,?,?,?,?)";

		/// <summary>
		/// 获取数值(SDN格式)
		/// </summary>
		/// <param name="p_intBaseValue">原始数据</param>
		/// <returns></returns>
		
		/// <summary>
		/// 获取结果信息
		/// </summary>
		/// <param name="p_bytBaseValueArr">结果数组</param>
		/// <param name="p_intResultIndex">当前结果索引</param>
		/// <param name="p_intMFC_ID">结果类型，如果返回0，代表没有结果</param>
		/// <param name="p_fltResult">结果值</param>
		[AutoComplete]
		private void m_mthTransNum(byte[] p_bytBaseValueArr,int p_intResultIndex,out int p_intMFC_ID,out float p_fltResult)
		{
			byte bytMFC_IDFirst = p_bytBaseValueArr[p_intResultIndex*4];
			byte bytMFC_IDSecond = p_bytBaseValueArr[p_intResultIndex*4+1];
			p_intMFC_ID = bytMFC_IDFirst+bytMFC_IDSecond*256;

			byte bytFirst = p_bytBaseValueArr[p_intResultIndex*4+3];
			byte bytSecond = p_bytBaseValueArr[p_intResultIndex*4+2];
			
			p_fltResult = 0;
			float fltRes=0;
			if((bytFirst/8)%2 == 1)
				return;

			switch((bytFirst/16)%8)
			{
				case 0:
					fltRes = ((bytFirst%8)*256+bytSecond)*0.0001f;
					break;
				case 1:
					fltRes = (bytFirst%8)*256+bytSecond;
					break;
				case 2:
					fltRes = ((bytFirst%8)*256+bytSecond)*0.01f;
					break;
				case 3:
					fltRes = ((bytFirst%8)*256+bytSecond)*100;
					break;
				case 4:
					fltRes = ((bytFirst%8)*256+bytSecond)*0.001f;
					break;
				case 5:
					fltRes = ((bytFirst%8)*256+bytSecond)*10;
					break;
				case 6:
					fltRes = ((bytFirst%8)*256+bytSecond)*0.1f;
					break;
				case 7:
					fltRes = ((bytFirst%8)*256+bytSecond)*1000;
					break;
			}

			if(((bytFirst/128)%2)==1)
			{
				fltRes = -1*fltRes;
			}

			p_fltResult = fltRes;
		}
		

		/// <summary>
		/// 数据同步
		/// </summary>
		/// <param name="p_lngEffRecords">操作的条数</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSyncDocVueData(out long p_lngEffRecords)
		{            
			p_lngEffRecords = 0;
            return 0;
            //		clsHRPTableService objHRPService = new clsHRPTableService();
            //		clsTrendDBService objTrendDBService = new clsTrendDBService();
            //		long lngRes = 1;
            //           try
            //           {
            //               //查找BedNO
            //               string strCommand = @"select t2.bedno,t1.inpatientid,t1.inpatientdate 
            //				from indeptinfo t1,docvue_bed_hrp t2
            //				where t1.bed_id = t2.bed_id and t1.inbedenddate = ?
            //				order by bedno";
            //               DataTable dtbBaseResult = new DataTable();
            //               IDataParameter[] objDPArr = null;
            //               objHRPService.CreateDatabaseParameter(1, out objDPArr);
            //               objDPArr[0].DbType = DbType.DateTime;
            //               objDPArr[0].Value = new DateTime(1900,1,1);

            //               long lngRess = objHRPService.lngGetDataTableWithParameters(strCommand, ref dtbBaseResult, objDPArr);
            //               if (lngRess <= 0 || dtbBaseResult.Rows.Count <= 0)
            //                   return lngRes;

            //               for (int i = 0; i < dtbBaseResult.Rows.Count; i++)
            //               {
            //                   string strDocVueBedNO = dtbBaseResult.Rows[i][0].ToString();
            //                   string strInPatientID = dtbBaseResult.Rows[i][1].ToString();
            //                   string strInPatientDate = dtbBaseResult.Rows[i][2].ToString();

            //                   //获取最后时间
            //                   strCommand = @"select max(storedate) as storedate
            // from v_trend2
            //where (inpatientdate = ?)
            //  and (inpatientid = ?)";

            //                   DataTable dtbMaxDateResult = new DataTable();
            //                   objHRPService.CreateDatabaseParameter(2, out objDPArr);
            //                   objDPArr[0].DbType = DbType.DateTime;
            //                   objDPArr[0].Value = DateTime.Parse(strInPatientDate);
            //                   objDPArr[1].Value = strInPatientID;

            //                   long lngMaxDateRes = objHRPService.lngGetDataTableWithParameters(strCommand, ref dtbMaxDateResult, objDPArr);

            //                   if (lngMaxDateRes <= 0)
            //                       return lngMaxDateRes;

            //                   string strMaxStoreDate = dtbMaxDateResult.Rows[0][0].ToString();

            //                   //获取最新记录
            //                   strCommand = @"select trend.*
            //								from docvue.v_bedlist bedlist,docvue.da_hightrend as trend
            //								where bedlist.caseid = trend.caseid
            //								and bedlist.bedid = '" + strDocVueBedNO + "' ";

            //                   if (strMaxStoreDate != "")
            //                       strCommand += "and trend.storedate > '" + strMaxStoreDate + "' ";

            //                   strCommand += "order by trend.storedate";

            //                   DataTable dtbTrendResult = new DataTable();
            //                   long lngTrendRes = objTrendDBService.DoGetDataTable(strCommand, ref dtbTrendResult);

            //                   if (lngTrendRes <= 0)
            //                       return lngTrendRes;

            //                   for (int j2 = 0; j2 < dtbTrendResult.Rows.Count; j2++)
            //                   {
            //                       for (int k3 = 0; k3 < 28; k3++)
            //                       {
            //                           int intMFCID;
            //                           float fltResult;

            //                           m_mthTransNum((byte[])dtbTrendResult.Rows[j2]["MFC_VALUE"], k3, out intMFCID, out fltResult);

            //                           if (intMFCID == 0)
            //                               continue;

            //                           //将结果插入HRPGZNO1数据库中的表内
            //                           long lngEff = 0;

            //                           //							IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[7];
            //                           //							for(int l4=0;l4<objDPArr.Length;l4++)
            //                           //							{
            //                           //								objDPArr[l4]=new Oracle.DataAccess.Client.OracleParameter();
            //                           //							}
            //                           objHRPService.CreateDatabaseParameter(7, out objDPArr);
            //                           objDPArr[0].Value = strInPatientID;//InPatientID
            //                           objDPArr[1].DbType = DbType.DateTime;
            //                           objDPArr[1].Value = DateTime.Parse(strInPatientDate);//InPatientDate
            //                           objDPArr[2].Value = dtbTrendResult.Rows[j2]["CASEID"];//CaseId
            //                           objDPArr[3].Value = dtbTrendResult.Rows[j2]["SELINDEX"];//Selindex
            //                           objDPArr[4].Value = dtbTrendResult.Rows[j2]["STOREDATE"];//Storedate
            //                           objDPArr[5].Value = intMFCID;//EMFC_ID
            //                           objDPArr[6].Value = fltResult;//Result

            //                           long lngMoveRes = objHRPService.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);

            //                           if (lngMoveRes <= 0)
            //                               return lngMoveRes;

            //                           p_lngEffRecords += lngEff;
            //                       }
            //                   }
            //               }

            //           }
            //           catch (Exception objEx)
            //           {
            //               string strTmp = objEx.Message;
            //               com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
            //               bool blnRes = objLogger.LogError(objEx);
            //           }
            //           finally
            //           {
            //               //objHRPService.Dispose();
            //           }
            //		//返回
            //		return lngRes;

            #region old
            /*
			string strTemp = "";

			DataRow dtrNew;
			for(int i1=0;i1<dtbBaseResult.Rows.Count;i1++)
			{
				dtrNew = dtbBaseResult.Rows[i1];
				strTemp += dtrNew[1].ToString() + ",";
			}
			strTemp = strTemp.Substring(0,strTemp.Length - 1);
			//添加一列，用于存放CaseID
			dtbBaseResult.Columns.Add();

			//根据BedNo查找CaseID
			strCommand = "Select CaseID,BedID from docvue.V_BedList WHERE BedID IN("+strTemp+")";

			
			DataTable dtbResult = new DataTable();
			lngRes = objTrendDBService.DoGetDataTable(strCommand,ref dtbResult);	
			if(lngRes <= 0 || dtbResult.Rows.Count <=0)
				return lngRes;
			strTemp = "";
			Object [] objTempArr = new Object[dtbBaseResult.Columns.Count];
			for(int i1=0;i1<dtbBaseResult.Columns.Count;i1++)
			{
				objTempArr[i1] = new object();
			}
			for(int i1=0;i1<dtbResult.Rows.Count;i1++)
			{
				dtrNew = dtbResult.Rows[i1];
				strTemp += dtrNew[0].ToString() + ",";
				//查找dtbBaseResult中的数据，找到该CaseID相对应的BedID
				for(int j2=0;j2<dtbBaseResult.Rows.Count;j2++)
				{
					if(dtbBaseResult.Rows[j2].ItemArray[1].ToString() == dtrNew[1].ToString())
					{
						for(int k3=0;k3<dtbBaseResult.Columns.Count-1;k3++)
						{
							objTempArr[k3] = dtbBaseResult.Rows[j2].ItemArray[k3];
						}
						objTempArr[4] = dtrNew[0];
						dtbBaseResult.Rows[j2].ItemArray = objTempArr;
						break;
					}
				}
			}


			strTemp = strTemp.Substring(0,strTemp.Length - 1);
			//根据CaseID查找每一个CaseID的最后更新时间。
			strCommand = "SELECT CaseId,MAX(Storedate) AS Storedate FROM V_Trend2 "+
					"GROUP BY CaseId HAVING CaseId IN ("+strTemp+") ";

			lngRes = objHRPService.DoGetDataTable(strCommand,ref dtbResult);
			if(lngRes <= 0 || dtbResult.Rows.Count <0)
				return lngRes;
			strCommand = "";
			for(int i1=0;i1<dtbResult.Rows.Count;i1++)
			{
				dtrNew = dtbResult.Rows[i1];
				strCommand += "Select * from docvue.V_Trend2 WHERE CaseId = "+dtrNew[0].ToString()+" and Storedate > '"+dtrNew[1].ToString()+"' UNION ";
			}
			strCommand = strCommand.Substring(0,strCommand.Length - 6);
			//查找每一个CaseID中还没有更新过的数据
			lngRes = objTrendDBService.DoGetDataTable(strCommand,ref dtbResult);
			p_lngEffRecords = dtbResult.Rows.Count;
			if(lngRes <= 0 || dtbResult.Rows.Count <=0)
				return lngRes;
			long lngEff=0;
			IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[dtbResult.Columns.Count + 2];
			for(int i1=0;i1<dtbResult.Rows.Count;i1++)
			{
				dtrNew = dtbResult.Rows[i1];
				//按顺序给IDataParameter赋值
				for(int j2=0;j2<objDPArr.Length;j2++)
				{
					objDPArr[j2]=new Oracle.DataAccess.Client.OracleParameter();
					if(j2 > 1)
					{
						objDPArr[j2].Value=dtrNew[j2-2];
						continue;
					}
					if(j2 == 0)//添加InPatientID的数据信息
					{
						for(int k3=0;k3<dtbBaseResult.Rows.Count;k3++)
						{
							if(dtbBaseResult.Rows[k3].ItemArray[4].ToString() == dtrNew[0].ToString())
							{
								objDPArr[j2].Value = dtbBaseResult.Rows[k3].ItemArray[2];
								break;
							}
						}
						continue;
					}
					if(j2 == 1)//添加InPatientDate的数据信息
					{
						for(int k3=0;k3<dtbBaseResult.Rows.Count;k3++)
						{
							if(dtbBaseResult.Rows[k3].ItemArray[4].ToString() == dtrNew[0].ToString())
							{
								objDPArr[j2].Value = dtbBaseResult.Rows[k3].ItemArray[3];
								break;
							}
						}
						continue;
					}
				}
				//将结果插入HRPGZNO1数据库中的表内
				lngRes = objHRPService.lngExecuteParameterSQL(c_strAddNewRecordSQL,ref lngEff,objDPArr);

				if(lngRes<=0)
					return lngRes;
			}
			
			return lngRes;
			*/
            #endregion


        }
		
	}
}
