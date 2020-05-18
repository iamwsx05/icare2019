using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.LabCheckItemAdminService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsLabCheckItemAdminService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		
//		/// <summary>
//		/// 获取所有检验项目的名称和ID Sql
//		/// </summary>
//		private const string c_strGetLabCheckItemsSQL = @"SELECT LID.Item_ID, LID.Item_Name FROM LabCheck_Item_Desc LID,LabCheck_Item LI
//														WHERE LID.Item_ID = LI.Item_ID AND LI.End_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" AND LID.End_Name_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";


//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strGetLabCheckGroupsSQL = @"SELECT LGD.Group_ID,Group_Name FROM LabCheck_Group_Desc LGD, LabCheck_Group LG
//														WHERE LGD.Group_ID = LG.Group_ID AND LGD.End_Name_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" AND LG.End_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strGetMaxGroupIDSQL = @"SELECT Max(Group_ID) AS Group_ID FROM LabCheck_Group";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strGetLabCheckGroupItem = @"SELECT LID.Item_ID,LID.Item_Name FROM LabCheck_Item_Desc LID,LabCheck_Group_Item LGI
//														WHERE LID.Item_ID = LGI.Item_ID AND LID.End_Name_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" AND LGI.End_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" AND LGI.Group_ID = ?";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strGetUnGroupLabCheckItem = @"SELECT LID.Item_ID,LID.Item_Name FROM LabCheck_Item_Desc LID
//														WHERE LID.Item_ID not in (SELECT DISTINCT LGI.Item_ID FROM LabCheck_Group_Item LGI WHERE End_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@")
//														And LID.End_Name_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";

//		private const string c_strGetLabCheckItemsSpecialSQL = @"SELECT Item_ID,Item_Name FROM LabCheck_Item_Desc WHERE End_Name_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" AND Item_ID = ?";

//		private const string c_strGetLabCheckGroupSpecialSQL = @"SELECT Group_ID,Group_Name FROM LabCheck_Group_Desc WHERE End_Name_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@" AND Group_ID = ?";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strAddNewGroupSQL = @"INSERT INTO LabCheck_Group (Group_ID,Begin_Date,End_Date) Values (?,?,?)";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strAddNewGroupDescSQL = @"INSERT INTO LabCheck_Group_Desc (Group_ID,Begin_Name_Date,Group_Name,End_Name_Date) Values (?,?,?,?)";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strAddNewGroupItemSQL = @"INSERT INTO LabCheck_Group_Item (Group_ID,Item_ID,Begin_Date,End_Date) Values (?,?,?,?)";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strModifyGroupItemSQL = @"UPDATE LabCheck_Group_Item SET End_Date = ? WHERE Group_ID = ? AND End_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strModifyGroupSQL = @"UPDATE LabCheck_Group SET End_Date = ? WHERE Group_ID = ? AND End_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";

//		/// <summary>
//		/// 
//		/// </summary>
//		private const string c_strModifyGroupDescSQL = @"UPDATE LabCheck_Group_Desc SET End_Name_Date = ? WHERE Group_ID = ? AND End_Name_Date = "+clsDatabaseSQLConvert.s_strGetSQLInvalidDateFormat()+@"";
				

		/// <summary>
		/// 获取所有检验项目的名称和ID
		/// </summary>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLabCheckItems(
			out clsLabCheckItem[] p_objRecordContentArr)
		{
			p_objRecordContentArr = null;

			string c_strGetLabCheckItemsSQL = @"select lid.item_id, lid.item_name from labcheck_item_desc lid,labcheck_item li
														where lid.item_id = li.item_id and li.end_date = ? and lid.end_name_date = ?";
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsLabCheckItemAdminService", "m_lngGetLabCheckItems");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                DataTable dtbValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServer.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = new DateTime(1900, 1, 1);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);

                long lngRes = objHRPServer.lngGetDataTableWithParameters(c_strGetLabCheckItemsSQL, ref dtbValue, objDPArr);

                if (lngRes < 0 || dtbValue.Rows.Count == 0)
                    return 0;

                p_objRecordContentArr = new clsLabCheckItem[dtbValue.Rows.Count];

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objRecordContentArr[i] = new clsLabCheckItem();

                    p_objRecordContentArr[i].m_strLabItemID = dtbValue.Rows[i]["ITEM_ID"].ToString();
                    p_objRecordContentArr[i].m_strLabItemDesc = dtbValue.Rows[i]["ITEM_NAME"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
			
			
			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objRecordContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLabCheckItemsSpecial(
			string p_strItemID, out clsLabCheckItem p_objRecordContent)
		{

			p_objRecordContent = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngGetLabCheckItemsSpecial");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strItemID == null || p_strItemID == "")
				return (long)enmOperationResult.Parameter_Error;
 
			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(2,out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = new DateTime(1900, 1, 1);
			objDPArr[1].Value = p_strItemID;

			DataTable dtbValue = new DataTable();

			string c_strGetLabCheckItemsSpecialSQL = @"select item_id,item_name from labcheck_item_desc where end_name_date = ? and item_id = ?";

            try
            {
                long lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetLabCheckItemsSpecialSQL, ref dtbValue, objDPArr);

                if (lngRes < 0 || dtbValue.Rows.Count == 0)
                    return 0;

                p_objRecordContent = new clsLabCheckItem();

                if (dtbValue.Rows.Count == 1)
                {
                    p_objRecordContent.m_strLabItemID = dtbValue.Rows[0]["ITEM_ID"].ToString();
                    p_objRecordContent.m_strLabItemDesc = dtbValue.Rows[0]["ITEM_NAME"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
			
			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strItemID"></param>
		/// <param name="p_objRecordContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLabCheckGroupSpecial(
			string p_strGroupID, out clsLabCheckGroup p_objRecordContent)
		{

			p_objRecordContent = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngGetLabCheckGroupSpecial");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strGroupID == null || p_strGroupID == "")
				return (long)enmOperationResult.Parameter_Error;
 
			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(2,out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = new DateTime(1900, 1, 1);
			objDPArr[1].Value = p_strGroupID;

			DataTable dtbValue = new DataTable();

			string c_strGetLabCheckGroupSpecialSQL = @"select group_id,group_name from labcheck_group_desc where end_name_date = ? and group_id = ?";

            try
            {
                long lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetLabCheckGroupSpecialSQL, ref dtbValue, objDPArr);

                if (lngRes < 0 || dtbValue.Rows.Count == 0)
                    return 0;

                p_objRecordContent = new clsLabCheckGroup();

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objRecordContent.m_strLabGroupID = dtbValue.Rows[i]["GROUP_ID"].ToString();
                    p_objRecordContent.m_strLabGroupName = dtbValue.Rows[i]["GROUP_NAME"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
			
			return 1;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objRecordContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetUnGroupLabCheckItems(
			out clsLabCheckItem[] p_objRecordContentArr)
		{
			//c_strGetUnGroupLabCheckItem
			p_objRecordContentArr = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngGetUnGroupLabCheckItems");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			DataTable dtbValue = new DataTable();
            clsHRPTableService objHRPServer = new clsHRPTableService();
			string c_strGetUnGroupLabCheckItem = @"select lid.item_id,lid.item_name from labcheck_item_desc lid
														where lid.item_id not in (select distinct lgi.item_id from labcheck_group_item lgi where end_date = ?)
														and lid.end_name_date = ?";

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServer.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = new DateTime(1900, 1, 1);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);

                long lngRes = objHRPServer.lngGetDataTableWithParameters(c_strGetUnGroupLabCheckItem, ref dtbValue, objDPArr);

                if (lngRes < 0 || dtbValue.Rows.Count == 0)
                    return 0;

                p_objRecordContentArr = new clsLabCheckItem[dtbValue.Rows.Count];

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objRecordContentArr[i] = new clsLabCheckItem();

                    p_objRecordContentArr[i].m_strLabItemID = dtbValue.Rows[i]["ITEM_ID"].ToString();
                    p_objRecordContentArr[i].m_strLabItemDesc = dtbValue.Rows[i]["ITEM_NAME"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
			
			
			return 1;
		}

		/// <summary>
		/// 获取所有检验项目分组
		/// </summary>
		/// <param name="p_objRecordContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLabCheckGroups(
			out clsLabCheckGroup[] p_objRecordContentArr)
		{
			p_objRecordContentArr = null;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsLabCheckItemAdminService", "m_lngGetLabCheckGroups");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                DataTable dtbValue = new DataTable();

                string c_strGetLabCheckGroupsSQL = @"select lgd.group_id,group_name from labcheck_group_desc lgd, labcheck_group lg
															where lgd.group_id = lg.group_id and lgd.end_name_date = ? and lg.end_date = ?";

                IDataParameter[] objDPArr = null;
                objHRPServer.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = new DateTime(1900, 1, 1);
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = new DateTime(1900, 1, 1);

                long lngRes = objHRPServer.lngGetDataTableWithParameters(c_strGetLabCheckGroupsSQL, ref dtbValue, objDPArr);

                if (lngRes < 0 || dtbValue.Rows.Count == 0)
                    return 0;

                p_objRecordContentArr = new clsLabCheckGroup[dtbValue.Rows.Count];

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objRecordContentArr[i] = new clsLabCheckGroup();

                    p_objRecordContentArr[i].m_strLabGroupID = dtbValue.Rows[i]["GROUP_ID"].ToString();
                    p_objRecordContentArr[i].m_strLabGroupName = dtbValue.Rows[i]["GROUP_NAME"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
			
			
			return 1;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strGroupID"></param>
		/// <param name="p_objRecordContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLabCheckGroupItem(
			string p_strGroupID,out clsLabCheckItem[] p_objRecordContentArr)
		{
			p_objRecordContentArr = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngGetLabCheckGroupItem");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strGroupID == null || p_strGroupID == "")
				return (long)enmOperationResult.Parameter_Error;

			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(3,out objDPArr);
//			objDPArr[0] = new Oracle.DataAccess.Client.OracleParameter();
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = new DateTime(1900, 1, 1);
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = new DateTime(1900, 1, 1);
			objDPArr[2].Value = p_strGroupID;


			DataTable dtbValue = new DataTable();

			string c_strGetLabCheckGroupItem = @"select lid.item_id,lid.item_name from labcheck_item_desc lid,labcheck_group_item lgi
														where lid.item_id = lgi.item_id and lid.end_name_date = ? and lgi.end_date = ? and lgi.group_id = ?";

            try
            {
                long lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetLabCheckGroupItem, ref dtbValue, objDPArr);

                if (lngRes < 0 || dtbValue.Rows.Count == 0)
                    return 0;

                p_objRecordContentArr = new clsLabCheckItem[dtbValue.Rows.Count];

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objRecordContentArr[i] = new clsLabCheckItem();

                    p_objRecordContentArr[i].m_strLabItemID = dtbValue.Rows[i]["ITEM_ID"].ToString();
                    p_objRecordContentArr[i].m_strLabItemDesc = dtbValue.Rows[i]["ITEM_NAME"].ToString();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }
			
			
			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strGroupID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMaxGroupID(
			out string p_strGroupID)
		{
			p_strGroupID = null;
             clsHRPTableService objHRPServer = new clsHRPTableService();
             try
             {
                 //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsLabCheckItemAdminService", "m_lngGetMaxGroupID");
                 //if (lngCheckRes <= 0)
                     //return lngCheckRes;

                 DataTable dtbValue = new DataTable();

                 string c_strGetMaxGroupIDSQL = @"select max(group_id) as group_id from labcheck_group";

                 long lngRes = objHRPServer.DoGetDataTable(c_strGetMaxGroupIDSQL, ref dtbValue);

                 if (lngRes < 0 || dtbValue.Rows.Count != 1)
                     return 0;

                 p_strGroupID = (dtbValue.Rows[0]["GROUP_ID"].ToString() == "") ? "00000" : dtbValue.Rows[0]["GROUP_ID"].ToString();
             }
             catch (Exception objEx)
             {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                 bool blnRes = objLogger.LogError(objEx);
             }
             finally
             {
                 //objHRPServer.Dispose();
             }

			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objLabCheckGroup"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewGroup(
			clsLabCheckGroup p_objLabCheckGroup)
		{
			string c_strAddNewGroupSQL = @"insert into labcheck_group (group_id,begin_date,end_date) values (?,?,?)";

			string c_strAddNewGroupDescSQL = @"insert into labcheck_group_desc (group_id,begin_name_date,group_name,end_name_date) values (?,?,?,?)";

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngAddNewGroup");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_objLabCheckGroup == null)
				return (long)enmOperationResult.Parameter_Error;

			//
			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(3,out objDPArr); 

//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

            objDPArr[0].Value = p_objLabCheckGroup.m_strLabGroupID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[2].DbType = DbType.DateTime;
			objDPArr[2].Value = DateTime.Parse("1900-01-01 00:00:00");

			//
			IDataParameter[] objDPArr2 = null;
			objHRPServer.CreateDatabaseParameter(4,out objDPArr2);

//			for(int i=0;i<objDPArr2.Length;i++)
//				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();

            objDPArr2[0].Value = p_objLabCheckGroup.m_strLabGroupID;
            objDPArr2[1].DbType = DbType.DateTime;
			objDPArr2[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr2[2].Value = p_objLabCheckGroup.m_strLabGroupName;
            objDPArr2[3].DbType = DbType.DateTime;
			objDPArr2[3].Value = DateTime.Parse("1900-01-01 00:00:00");

			long lngEff = 0;
			long lngRes = -1;
            try
            {
                lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strAddNewGroupSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strAddNewGroupDescSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServer.Dispose();
            }
			return lngRes;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objLabCheckGroupItemArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewGroupItem(
			clsLabCheckGroupItem[] p_objLabCheckGroupItemArr)
		{
			string c_strAddNewGroupItemSQL = @"insert into labcheck_group_item (group_id,item_id,begin_date,end_date) values (?,?,?,?)";
            clsHRPTableService objHRPServer=new clsHRPTableService();
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngAddNewGroupItem");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_objLabCheckGroupItemArr == null)
				return (long)enmOperationResult.Parameter_Error;

			for(int i = 0; i < p_objLabCheckGroupItemArr.Length; i++)
			{
				
				IDataParameter[] objDPArr = null;
				objHRPServer.CreateDatabaseParameter(4,out objDPArr);
				
//				for(int j=0;j<objDPArr.Length;j++)
//					objDPArr[j]=new Oracle.DataAccess.Client.OracleParameter();

				objDPArr[0].Value = p_objLabCheckGroupItemArr[i].m_objLabCheckGroup.m_strLabGroupID;
                objDPArr[1].Value = p_objLabCheckGroupItemArr[i].m_objLabCheckItem.m_strLabItemID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value = DateTime.Parse("1900-1-1 00:00:00");

				long lngEff = 0;
				try
				{
					long lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strAddNewGroupItemSQL,ref lngEff,objDPArr);
					if(lngRes<=0)return lngRes;
				}
				catch(Exception objEx)
				{
					com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
					bool blnRes = objLogger.LogError(objEx);					
				}
				
			}
          
            //objHRPServer.Dispose();
			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strGroupID"></param>
		/// <param name="p_objLabCheckGroupItemArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyGroupItem(
			string p_strGroupID)
		{
			string c_strModifyGroupItemSQL = @"update labcheck_group_item set end_date = ? where group_id = ? and end_date = ?";

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngModifyGroupItem");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strGroupID == null || p_strGroupID == "")
				return (long)enmOperationResult.Parameter_Error;

			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(3,out objDPArr);

//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

            objDPArr[0].DbType = DbType.DateTime;
			objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[1].Value = p_strGroupID;
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = new DateTime(1900, 1, 1);
			
			long lngEff = 0;
            try
            {
                long lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strModifyGroupItemSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServer.Dispose();
            }

			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strGroupID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteGroupItem(
			string p_strGroupID)
		{
			string c_strModifyGroupItemSQL = @"update labcheck_group_item set end_date = ? where group_id = ? and end_date = ?";

			string c_strModifyGroupSQL = @"update labcheck_group set end_date = ? where group_id = ? and end_date = ?";

			string c_strModifyGroupDescSQL = @"update labcheck_group_desc set end_name_date = ? where group_id = ? and end_name_date = ?";

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngDeleteGroupItem");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strGroupID == null || p_strGroupID == "")
				return (long)enmOperationResult.Parameter_Error;

			//
			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(3,out objDPArr);
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();


            objDPArr[0].DbType = DbType.DateTime;
			objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[1].Value = p_strGroupID;
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = new DateTime(1900, 1, 1);

			//
			IDataParameter[] objDPArr2 = null;
			objHRPServer.CreateDatabaseParameter(3,out objDPArr2);
//			for(int i=0;i<objDPArr2.Length;i++)
//				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();


            objDPArr2[0].DbType = DbType.DateTime;
			objDPArr2[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr2[1].Value = p_strGroupID;
            objDPArr2[2].DbType = DbType.DateTime;
            objDPArr2[2].Value = new DateTime(1900, 1, 1);

			//
			IDataParameter[] objDPArr3 = null;
			objHRPServer.CreateDatabaseParameter(3,out objDPArr3);
//			for(int i=0;i<objDPArr3.Length;i++)
//				objDPArr3[i]=new Oracle.DataAccess.Client.OracleParameter();
            
            objDPArr3[0].DbType = DbType.DateTime;
			objDPArr3[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr3[1].Value = p_strGroupID;
            objDPArr3[2].DbType = DbType.DateTime;
            objDPArr3[2].Value = new DateTime(1900, 1, 1);

			long lngEff = 0;

            try
            {
                long lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strModifyGroupItemSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strModifyGroupDescSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strModifyGroupSQL, ref lngEff, objDPArr3);
                if (lngRes <= 0) return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServer.Dispose();
            }
			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strGroup_ID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyGroupDesc(
			string p_strGroupID,clsLabCheckGroup p_objLabCheckGroup)
		{
			string c_strAddNewGroupDescSQL = @"insert into labcheck_group_desc (group_id,begin_name_date,group_name,end_name_date) values (?,?,?,?)";

			string c_strModifyGroupDescSQL = @"update labcheck_group_desc set end_name_date = ? where group_id = ? and end_name_date = ?";

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabCheckItemAdminService","m_lngModifyGroupDesc");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strGroupID == null || p_strGroupID == "")
				return (long)enmOperationResult.Parameter_Error;

			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr = null;
			objHRPServer.CreateDatabaseParameter(3,out objDPArr);
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

            objDPArr[0].DbType = DbType.DateTime;
			objDPArr[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			objDPArr[1].Value = p_strGroupID;
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = new DateTime(1900, 1, 1);

			IDataParameter[] objDPArr2 = null;
			objHRPServer.CreateDatabaseParameter(4,out objDPArr2);

//			for(int i=0;i<objDPArr2.Length;i++)
//				objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();

            objDPArr2[0].Value = p_objLabCheckGroup.m_strLabGroupID;
            objDPArr2[1].DbType = DbType.DateTime;
			objDPArr2[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr2[2].Value = p_objLabCheckGroup.m_strLabGroupName;
            objDPArr2[3].DbType = DbType.DateTime;
			objDPArr2[3].Value = DateTime.Parse("1900-1-1 00:00:00");

			long lngEff = 0;
            try
            {
                long lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strModifyGroupDescSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                lngRes = new clsHRPTableService().lngExecuteParameterSQL(c_strAddNewGroupDescSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServer.Dispose();
            }
			return 1;
		}

		
	}
}
