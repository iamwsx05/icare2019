using System.EnterpriseServices;
using System;
using System.Text ;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;

namespace com.digitalwave.EquipmentTypeService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsEquipmentTypeService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsEquipmentTypeService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="strXML"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewRecord(string strXML)
		{
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsEquipmentTypeService", "m_lngAddNewRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strXML == "" || strXML == null)
                    return -1;

                lngRes = objHRP.add_new_record("EquipmentType", strXML);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
			//返回
			return lngRes;
			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="strReceivedXML"></param>
		/// <param name="p_intReturnRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordCount(
			ref string strReceivedXML ,ref int p_intReturnRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsEquipmentTypeService", "m_lngGetRecordCount");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "select count(equipmenttypeid) as row from equipmenttype";

                lngRes = objHRP.lngGetXMLTable(strCommand, ref strReceivedXML, ref p_intReturnRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
			//返回
			return lngRes;
					}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="strReceivedXML"></param>
		/// <param name="p_intReturnRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetXMLTable(
			ref string strReceivedXML, ref int p_intReturnRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsEquipmentTypeService", "m_lngGetXMLTable");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = @"select equipmenttypeid,
       begin_naming_date,
       equipmenttypename,
       status,
       deactiveddate,
       operatorid,
       type_id,
       typename
  from equipmenttype
 where status <> '1'";

                lngRes = objHRP.lngGetXMLTable(strCommand, ref strReceivedXML, ref p_intReturnRows);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
			//返回
			return lngRes;
					}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="strEquipmentTypeID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecord(
			string strEquipmentTypeID)
		{
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsEquipmentTypeService", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strEquipmentTypeID == "" || strEquipmentTypeID == null)
                    return 0;

                string strCommand = "update equipmenttype set status =1 where equipmenttypeid=?";
                IDataParameter[] objDPArr = null;
                objHRP.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strEquipmentTypeID;

                long lngEff = -1;
                lngRes = objHRP.lngExecuteParameterSQL(strCommand,ref lngEff,objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
			//返回
			return lngRes;
			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="strEquipmentTypeID"></param>
		/// <returns></returns>
		[AutoComplete]
		public bool m_lngRecordExist(string strEquipmentTypeID)
		{
			bool blnRess =false;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsEquipmentTypeService", "m_lngRecordExist");
                //if (lngCheckRes <= 0)
                    return false;

                if (strEquipmentTypeID == "" || strEquipmentTypeID == null)
                    return false;

                blnRess = objHRP.bolRecordExist1Parameter("EQUIPMENTTYPE", strEquipmentTypeID, "EQUIPMENTTYPEID");

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
			//返回
			return blnRess;
					}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="strXML"></param>
		/// <param name="strEquipmentTypeID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyRecord(string strXML,string strEquipmentTypeID)
		{
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsEquipmentTypeService", "m_lngModifyRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strEquipmentTypeID == "" || strEquipmentTypeID == null)
                    return 0;

                lngRes = objHRP.modify_record("EquipmentType", strXML, "EQUIPMENTTYPEID");
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
			//返回
			return lngRes;
			
		}
	}
}
