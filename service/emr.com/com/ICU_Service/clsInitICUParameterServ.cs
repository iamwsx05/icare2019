using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Text;
using System.Data;

namespace com.digitalwave.InitICUParameterServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsInitICUParameterServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsInitICUParameterServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		/// <summary>
		/// 提取所有设备类型
		/// </summary>
		[AutoComplete]
		public long lngGetAllEquipmentTypes(out string strXML,out int intRows)
		{
			strXML = null;
			intRows =0;	
			long res = -1;
			string strCommand =@"SELECT EquipmentTypeID,EquipmentTypeName from EquipmentType  "+			
				" WHERE Status =0 ";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                res = objTabService.lngGetXMLTable(strCommand, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return res;
		}

		[AutoComplete]
		public long lngGetEquipmentModelNameArr(string strEquipmentTypeID,out string strXML,out int intRows)
		{				
			strXML = null;
			intRows =0;
			string strCommand = null;
			long res = -1;
			strCommand ="SELECT DISTINCT EquipmentModelID,EquipmentModelName  from EquipmentModel "+			
				" WHERE EquipmentTypeID = '"+strEquipmentTypeID+"' AND Status =0 ";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                res = objTabService.lngGetXMLTable(strCommand, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return res;

		}

		[AutoComplete]
		public long lngGetEquipmentFunationNameArr(string strEquipmentID,out string strXML,out int intRows)
		{				
			strXML = null;
			intRows =0;
			long res = -1;
			string strCommand = null;
            clsHRPTableService objTabService = new clsHRPTableService();
			strCommand ="select * from EQUIPMENTANDFUNFORCHKLAB ee, FunationForLab ff where ee.funationid=ff.funationid and equipmentid='"+strEquipmentID+"'";
            try
            {
                res = objTabService.lngGetXMLTable(strCommand, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return res;

		}

		[AutoComplete]
		public long lngGetEquipmentNameArr(string strEquipmentTypeID,out string strXML,out int intRows)
		{				
			strXML = null;
			intRows =0;
			string strCommand = null;
			long res = -1;
			strCommand ="SELECT DISTINCT EquipmentModelID,EquipmentModelName,EquipmentID  from EquipmentModel "+			
				" WHERE EquipmentTypeID = '"+strEquipmentTypeID+"' AND Status =0 ";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                res = objTabService.lngGetXMLTable(strCommand, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return res;

		}

		[AutoComplete]
		public long lngGetICUParameterInfoArr(string strEquipmentTypeID,string strEquipmentModelID,out string strXML,out int intRows)
		{				
			strXML = null;
			intRows =0;
			string strCommand = null;
			long res = -1;
			strCommand ="SELECT  * from ICUParameterInfo  "+			
				" WHERE EquipmentTypeID = '"+strEquipmentTypeID+"' AND EquipmentModelID='"+strEquipmentModelID+"'AND Status =0 ";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                res = objTabService.lngGetXMLTable(strCommand, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return res;

		}

		[AutoComplete]
		public long lngSave( string strXML, bool p_blnIsAddNew,out string strNewID)
		{
			strNewID="";
			long res = -1;
			if(strXML==null || strXML=="")return -1;				
			
			clsHRPTableService hs=new clsHRPTableService();

            try
            {
                if (p_blnIsAddNew == true)//添加记录
                {

                    hs.lngGenerateID(7, "ParameterID", "ICUParameterInfo", out strNewID);

                    StringBuilder temp = new StringBuilder(strXML);
                    temp.Insert(strXML.IndexOf(" "), " ParameterID='" + strNewID + "' ");
                    strXML = temp.ToString();

                    res = hs.add_new_record("ICUParameterInfo", strXML);
                }
                else //修改记录
                {
                    res = hs.modify_record("ICUParameterInfo", strXML, "ParameterID");

                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                hs.Dispose();
            }
			return res;
		}

		[AutoComplete]
		public long lngDelete( string strID)
		{			
			if(strID==null || strID=="")return -1;				
			
			clsHRPTableService hs=new clsHRPTableService();
			string strCommand = null;				

			
			string strSQL="select * from ICUParameterInfo  WHERE ParameterID = '"+strID+"' ";
			string strXML="";
			int intRows=0;	
			long res = -1;
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                res = objTabService.lngGetXMLTable(strSQL, ref strXML, ref intRows);
                if (intRows > 0)
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(strXML);
                    string strEquipmentTypeID = doc.FirstChild.ChildNodes[0].Attributes["EQUIPMENTTYPEID"].Value;
                    string strEquipmentModelID = doc.FirstChild.ChildNodes[0].Attributes["EQUIPMENTMODELID"].Value;
                    string strStandardParam = doc.FirstChild.ChildNodes[0].Attributes["STANDARDPARAM"].Value;

                    strCommand = "DELETE  ICUParameterSetting  " +		//删除子表,added by jacky-2002-11-29
                        " WHERE EquipmentTypeID = '" + strEquipmentTypeID + "'  and EquipmentModelID ='" + strEquipmentModelID + "' and  StandardParam='" + strStandardParam + "' and status=0";
                    hs.DoExcute(strCommand);

                    strCommand = "DELETE  ICUParameterInfo  " +		// 因为主键不变,如果只更新Status,将不能再添加该主键值,modified by jacky-2002-11-29
                        " WHERE ParameterID = '" + strID + "' ";
                    res = hs.DoExcute(strCommand);
                }
                else
                {
                    strCommand = "DELETE  ICUParameterInfo  " +		// 因为主键不变,如果只更新Status,将不能再添加该主键值,modified by jacky-2002-11-29
                        " WHERE ParameterID = '" + strID + "' ";
                    res = hs.DoExcute(strCommand);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
                hs.Dispose();
            }
			return res;			 	
			
		}

		//liyi 2002-12-2 获取所有标准参数
		[AutoComplete]
		public long lngGetAllStandardParam(out string strXML,out int intRows)
		{
			strXML="";
			intRows=0;
			long res = -1;

			clsHRPTableService hs=new clsHRPTableService();
			string strSQL="select * from ICUStandardParam where status='0'";

            try
            {
                res =hs.lngGetXMLTable(strSQL, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                hs.Dispose();
            }
			return res;		
		}

		/// <summary>
		/// 获取所有标准参数 HB - 2004-11-18
		/// </summary>
		/// <param name="p_dtbValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long lngGetAllStandardParam(out DataTable p_dtbValue)
		{
			p_dtbValue = new DataTable();

			clsHRPTableService hs=new clsHRPTableService();
			string strSQL="select * from ICUStandardParam where status='0'";
			long res = -1;

            try
            {
                res = hs.DoGetDataTable(strSQL, ref p_dtbValue);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                hs.Dispose();
            }
			return res;		
		}

		[AutoComplete]
		public long lngGetStandardParam(out string strXML,out int intRows)
		{
			strXML="";
			intRows=0;
			long res = -1;

			clsHRPTableService hs=new clsHRPTableService();
			string strSQL="select * from ICUStandardParam where Status=0and ParamFlag=0 ";

            try
            {
                res = hs.lngGetXMLTable(strSQL, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                hs.Dispose();
            }
			return res;		
		}

		[AutoComplete]
		public long lngGetStandardParam(string strStandardParam,out string strXML,out int intRows)
		{
			 strXML="";
			 intRows=0;
			long res = -1;

			clsHRPTableService hs=new clsHRPTableService();
			string strSQL="select * from ICUStandardParam where StandardParam ='" + strStandardParam + "' and status='0'";

            try
            {
                res = hs.lngGetXMLTable(strSQL, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                hs.Dispose();
            }
			return res;
			
			
		}

		/// <summary>
		/// 得到所有对应的 ICUDataInfo,Jacky-2002-12-2
		/// </summary>
		/// <param name="strEquipmentTypeID"></param>
		/// <param name="strEquipmentModelID"></param>
		/// <param name="strXML"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long lngGetICUDataInfoArr(string strEquipmentTypeID,string strEquipmentModelID,out string strXML,out int intRows)
		{				
			strXML = null;
			intRows =0;
			long res = -1;
			string strCommand = null;
			strCommand ="SELECT  * from ICUDataInfo  "+			
				" WHERE EquipmentTypeID = '"+strEquipmentTypeID+"' AND EquipmentModelID='"+strEquipmentModelID+"'  ";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                res = objTabService.lngGetXMLTable(strCommand, ref strXML, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objTabService.Dispose();
            }
			return res;

		}
	}
}
