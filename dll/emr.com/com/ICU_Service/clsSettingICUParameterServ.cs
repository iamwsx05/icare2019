using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Text;
using System.Xml;

namespace com.digitalwave.SettingICUParameterServ
{
	/// <summary>
	/// Summary description for SettingICUParameterServ.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsSettingICUParameterServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsSettingICUParameterServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[AutoComplete]
		public long lngGetAllDatesOfSetting(string strDoctorID,out string strXML,out int intRows)
		{
			strXML = null;
			intRows =0;	
			long res = -1;
			string strCommand ="SELECT DISTINCT DateOfSetting,SettingName,SamplingTime,SamplingUnit from ICUParameterSetting  "+			
				" WHERE DoctorID='"+strDoctorID+"' and Status =0 ";
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
		public long lngGetAllParameterInfoOfSetting(string strDoctorID,string strDate,out string strXML,out int intRows)
		{
			strXML = null;
			intRows =0;
			long res = -1;
			string strCommand ="SELECT i.StandardParam as StandardParam_one ,i.* ,ip.* from ICUStandardParam ip,ICUParameterInfo i,ICUParameterSetting  s "+			
				" WHERE s.DoctorID='"+strDoctorID+"' and s.DateOfSetting='"+strDate+"' and s.Status =0 and s.StandardParam=i.StandardParam "+
				" and s.EquipmentTypeID = i.EquipmentTypeID and s.EquipmentModelID=i.EquipmentModelID and  ip.StandardParam=i.StandardParam  ";
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
		public long lngGetEquipmentTypeName(string strEquipmentTypeID,out string strXML,out int intRows)
		{
			strXML = null;
			intRows =0;	
			long res = -1;
			string strCommand ="SELECT EquipmentTypeName as Name from EquipmentType  "+			
				" WHERE Status =0 and EquipmentTypeID='"+strEquipmentTypeID+"' ";
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
		public long lngGetEquipmentModelName(string strEquipmentModelID,out string strXML,out int intRows)
		{
			strXML = null;
			intRows =0;	
			long res = -1;
			string strCommand ="SELECT EquipmentModelName as Name from EquipmentModel  "+			
				" WHERE Status =0 and EquipmentModelID='"+strEquipmentModelID+"' ";
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
		public long lngSave( string[] strXMLArr, bool p_blnIsAddNew,out string strNewDate)
		{
			strNewDate="";
			if(strXMLArr==null || strXMLArr.Length==0)return -1;				
			
			clsHRPTableService hs=new clsHRPTableService();
			long lngRes=1;

            try
            {
                if (p_blnIsAddNew == true)//添加记录
                {


                    strNewDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    for (int i = 0; i < strXMLArr.Length && lngRes > 0; i++)
                    {
                        StringBuilder temp = new StringBuilder(strXMLArr[i]);
                        temp.Insert(strXMLArr[i].IndexOf(" "), " DateOfSetting='" + strNewDate + "' ");
                        strXMLArr[i] = temp.ToString();

                        lngRes = hs.add_new_record("ICUParameterSetting", strXMLArr[i]);
                    }
                }
                else //修改记录,注意:此处修改记录时在同一个表中有时要添加记录,故不能用modify_record()函数定位主键来修改
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(strXMLArr[0]);
                    XmlNode root = doc.FirstChild;

                    string strDoctorID = root.Attributes["DoctorID"].Value;
                    string strDate = root.Attributes["DateOfSetting"].Value;

                    string strCommand = "DELETE FROM ICUParameterSetting " +
                        " WHERE DoctorID='" + strDoctorID + "' and DateOfSetting='" + strDate + "' and Status =0 ";
                    hs.DoExcute(strCommand);//先删除所有的参数ID

                    for (int i1 = 0; i1 < strXMLArr.Length && lngRes > 0; i1++)
                        lngRes = hs.add_new_record("ICUParameterSetting", strXMLArr[i1]);

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
			return lngRes;
		}

		[AutoComplete]
		public long lngDelete(string strDoctorID,string strDateOfSetting)
		{			
			if(strDoctorID==null || strDoctorID==""||strDateOfSetting==null || strDateOfSetting=="")return -1;				
			
			clsHRPTableService hs=new clsHRPTableService();
			string strCommand = null;
			long res = -1;
			strCommand ="UPDATE  ICUParameterSetting SET Status = 1 "+			
				" WHERE DoctorID='"+strDoctorID+"' and DateOfSetting='"+strDateOfSetting+"' and Status =0 ";

            try
            {
                res = hs.DoExcute(strCommand);
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

	}
}
