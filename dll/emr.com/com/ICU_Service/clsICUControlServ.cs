using System;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;

namespace com.digitalwave.clsICUControlServ
{
	/// <summary>
	/// Summary description for clsICUControlServ.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsICUControlServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsICUControlServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// �õ�ICUControl��Ϣ: "�Ƿ����","���ȼ�",�� "��ǰ�û����ȼ�" ,Jacky-2002-11-30
		/// </summary>
		/// <param name="strInHospitalID"></param>
		/// <param name="strEID"></param>
		/// <param name="strBedID"></param>
		/// <param name="strEmployeeID">��ǰ�û�ID</param>
		[AutoComplete]		
		public long lngGetICUControlInfo(string strInHospitalID,string strEmployeeID, out string strXml,out int intRows)
		{
//			strXml="";
//			intRows=0;
//			long res = -1;
//			string strCommand = "SELECT TOP 1 i.IsLocked,i.Priority as Priority1 , p.Prioryity as Priority2 FROM ICUControlInfo i, dbo.ICUControlPriority p  "+
//				" WHERE  InHospitalID='" +strInHospitalID +"' and (ToDate='1900-1-1' or ToDate=NULL) "+				
//				"	and FromDate= "+
//				"	("+
//				"		select Max(FromDate) as  FromDate2 from ICUControlInfo "+
//				"		where InHospitalID='" +strInHospitalID +"' "+
//				"		and (ToDate='1900-1-1' or ToDate=NULL) 	"+
//				"	)"+
//				"	and p.EmployeeID='" +strEmployeeID +"' and p.Status=0 and p.Begin_Emp_Duty_Date = (select Max(Begin_Emp_Duty_Date) from ICUControlPriority where EmployeeID='" +strEmployeeID +"' and Status=0 )";
//			try
//			{
//				res =new clsHRPTableService().lngGetXMLTable(strCommand,ref strXml,ref intRows);
//			}
//			catch(Exception objEx)
//			{
//				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
//				bool blnRes = objLogger.LogError(objEx);					
//			}
//			return res;
			strXml = "";
			intRows = -1;
			return 0;
		}


		/// <summary>
		/// ����ICUControl��Ϣ: �ȸ��¾ɼ�¼(�������),�ٲ���һ���¼�¼,Jacky-2002-11-30
		/// </summary>
		/// <param name="strInHospitalID"></param>
		/// <param name="strEID"></param>
		/// <param name="strBedID"></param>
		/// <param name="strEmployeeID">��ǰ�û�ID</param>
		/// <returns></returns>
		[AutoComplete]
		public long lngUpdateICUControlInfo(string strInHospitalID,string strBedID,bool blnIsLocked,string strEmployeeID)
		{
			DateTime strNow=DateTime.Now;
			long res = -1;
			string strCommand = //��ʼ����
				"	Update	ICUControlInfo set ToDate='"+strNow+"',OperateID='"+strEmployeeID+"' "+
				"	where InHospitalID='"+strInHospitalID+"' and  BedID='"+strBedID+"' "+ 
				"	and FromDate = "+
				"	("+
				"		select Max(FromDate) as  FromDate2 from ICUControlInfo "+
				"		where InHospitalID='" +strInHospitalID +"'  and  BedID='" +strBedID +"' "+
				"		and (ToDate='1900-1-1' or ToDate=NULL) 	"+
				"	)"+			

				"	declare @intPriority as int "+  //��ʼ����
				"	set @intPriority= "+
				"	( "+
				"		select  Prioryity from ICUControlPriority where EmployeeID='"+strEmployeeID+"' and Status=0 "+
				"		and Begin_Emp_Duty_Date = (select Max(Begin_Emp_Duty_Date) from ICUControlPriority where EmployeeID='" +strEmployeeID +"' and Status=0 ) "+
				"	) "+
				"	Insert into ICUControlInfo(InHospitalID,BedID,FromDate,ToDate,IsLocked,Priority,OperateID) "+
				"	values('"+strInHospitalID+"','"+strBedID+"','"+strNow+"','1900-1-1','"+( blnIsLocked==true? 1:0) +"',@intPriority,'"+strEmployeeID+"')";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                res = objTabService.DoExcute(strCommand);
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

		/// <summary>
		/// ɾ��ICUControl��Ϣ,(���¾ɼ�¼),Jacky-2002-11-30
		/// </summary>
		/// <param name="strInHospitalID"></param>
		/// <param name="strEID"></param>
		/// <param name="strBedID"></param>
		/// <param name="strEmployeeID">��ǰ�û�ID</param>
		/// <returns></returns>
		[AutoComplete]
		public long lngDeleteICUControlInfo(string strInHospitalID,string strBedID,string strEmployeeID)
		{
			DateTime strNow=DateTime.Now;
			long res = -1;
			string strCommand = //��ʼ����
				"	Update	ICUControlInfo set ToDate='"+strNow+"',OperateID='"+strEmployeeID+"' "+
				"	where InHospitalID='"+strInHospitalID+"' and  BedID='"+strBedID+"' "+ 
				"	and FromDate = "+
				"	("+
				"		select Max(FromDate) as  FromDate2 from ICUControlInfo "+
				"		where InHospitalID='" +strInHospitalID +"'  and  BedID='" +strBedID +"' "+
				"		and (ToDate='1900-1-1' or ToDate=NULL) 	"+
				"	)";
            clsHRPTableService objTabService = new clsHRPTableService();
            try
            {
                res = objTabService.DoExcute(strCommand);
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
