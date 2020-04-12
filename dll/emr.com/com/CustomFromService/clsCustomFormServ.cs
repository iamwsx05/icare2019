using System;
using System.EnterpriseServices;
using System.Runtime.CompilerServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave;
using System.Text;
using System.Xml;
using com.digitalwave.Utility.SQLConvert;

namespace iCare.CustomFromService
{
	/// <summary>
	/// 自定义表单中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsCustomFormServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		#region Sql
		/// <summary>
		/// 新添未提交表单
		/// </summary>
		private const string c_strAddUnSubmitForm = @"INSERT INTO CUSTOMFORM_UNSUBMITFORM (FORM_ID, EMPLOYEEID, FORMNAME) VALUES (?,?,?)";
		/// <summary>
		/// 新添未提交表单配置文件
		/// </summary>
		private const string c_strAddUnSubmitPages = @"INSERT INTO CUSTOMFORM_UNSUBMIT_CONF (FORM_ID, PAGE, CONFIGURATION) VALUES (?,?,?)";
		/// <summary>
		/// 查找所有本员工未提交的表单
		/// </summary>
		private const string c_strGetUnSubmitForms = @"SELECT * FROM CUSTOMFORM_UNSUBMITFORM where EmployeeID = ?";
		/// <summary>
		/// 查找未提交表单配置文件
		/// </summary>
        private const string c_strGetUnSubmitPages = @"select form_id, page,configuration  from customform_unsubmit_conf where form_id = ? order by page";
		/// <summary>
		/// 查找提交表单配置文件
		/// </summary>
        private const string c_strGetSubmitPages = @"select form_id,page, configuration  from customform_conf where form_id = ?  order by page";

		/// <summary>
		/// 删除未提交表单
		/// </summary>
		private const string c_strDelUnSubmitForms = @"DELETE FROM CUSTOMFORM_UNSUBMITFORM WHERE EMPLOYEEID=? AND FORM_ID = ?";
		/// <summary>
		/// 更新表单
		/// </summary>
		private const string c_strUpdateUnSubmitForms = @"Update CUSTOMFORM_UNSUBMITFORM set FORMNAME=? where EmployeeID=? AND FORM_ID = ?";
		/// <summary>
		/// 新添提交表单配置文件
		/// </summary>
		private const string c_strAddSubmitPages = @"INSERT INTO customform_conf (form_id, page, configuration) VALUES (?,?,?)";
		/// <summary>
		/// 添加目的同步字段
		/// </summary>
		private const string c_strAddTarSync = @"INSERT INTO customform_sync_tarcnfg (form_id, target_form_id,config)  VALUES (?,?,?)";
		/// <summary>
		/// 修改目的同步字段
		/// </summary>
		private const string c_strUpdateTarSync = @"update customform_sync_tarcnfg set config =? where form_id = ? and target_form_id = ?";
		/// <summary>
		/// 添加源同步字段
		/// </summary>
		private const string c_strAddSouSync = @"INSERT INTO customform_sync_soucnfg (form_id, source_form_id,config)  VALUES (?,?,?)";
		/// <summary>
		/// 修改源同步字段
		/// </summary>
		private const string c_strUpdateSouSync = @"update customform_sync_soucnfg set config =? where form_id = ? and source_form_id = ?";
		/// <summary>
		/// 添加同步内容
		/// </summary>
		private const string c_strAddSync_data = @"INSERT INTO customform_sync_data (record_id,sync_id, content,status)  VALUES (?,?,?,'0')";
		/// <summary>
		/// 修改同步内容
		/// </summary>
		private const string c_strUpdateSync_data = @"update customform_sync_data set content = ? where record_id = ? and sync_id = ?";	
		/// <summary>
		/// 提交表单
		/// </summary>
		private const string c_strSubmitNewForm = @"INSERT INTO CustomForm
      (form_id,type_id,form_name,position,employeeid,visible_level,status)
VALUES (?,?,?,?,?,?,'0')";


		/// <summary>
		/// 保存模板需要的界面信息
		/// </summary>
		private const string c_strSaveTemplate_GUI_Info = @"INSERT INTO GUI_Info
      (Form_ID, Form_Desc)
VALUES (?,?)";

		/// <summary>
		/// 保存模板需要的界面控件信息
		/// </summary>
		private const string c_strSaveTemplate_GUI_Info_Detail = @"INSERT INTO GUI_Info_Detail
      (Form_ID, Control_ID, Control_Desc)
VALUES (?, ?, ?)";

		#endregion

		private const string c_strFormBaseName = "frmCustomForm_";

		/// <summary>
		/// 添加新的未提交表单
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddUnSubmitForm(ref clsCustom_UnSubmitValue p_objContent)
		{
			if(p_objContent == null || p_objContent.m_objPagesArr == null)
				return -1;
			
			long lngRef = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {
                int intID = objHRPServ.intGetNewNumericID("FORM_ID", "CUSTOMFORM_UNSUBMITFORM");
                if (intID == 0) intID = 1;
                if (intID > 0)
                {
                    p_objContent.m_intFormID = intID;
                    lngRef = m_lngAddUnSubmitFormValue(p_objContent);
                }
                if (lngRef > 0)
                {
                    for (int i = 0; i < p_objContent.m_objPagesArr.Length; i++)
                    {
                        lngRef = m_lngAddPageAndConfig(c_strAddUnSubmitPages, intID, p_objContent.m_objPagesArr[i]);
                        if (lngRef <= 0)
                            break;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRef;
		}
		/// <summary>
		/// 添加新的未提交表单主表
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngAddUnSubmitFormValue(clsCustom_UnSubmitValue p_objContent)
		{
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
			
			objHRPServ.CreateDatabaseParameter(3,out objDPArr);
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
			objDPArr[0].Value=p_objContent.m_intFormID;
			objDPArr[1].Value=p_objContent.m_strEmployeeID;
			objDPArr[2].Value=p_objContent.m_strFormName;
			
			long lngAff = 0 ;
			long lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddUnSubmitForm,ref lngAff,objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 添加新的未提交表单的配置文件
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <param name="p_intFormID"></param>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngAddPageAndConfig(string p_strSql,int p_intFormID,clsCustom_PagesValue p_objContent)
		{
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[3];
			
			objHRPServ.CreateDatabaseParameter(3,out objDPArr);
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
			objDPArr[0].Value=p_intFormID;
			objDPArr[1].Value=p_objContent.m_intPages;
			objDPArr[2].Value=System.Text.Encoding.Unicode.GetBytes(p_objContent.m_strConfiguration);
			
			long lngAff = 0 ;
            long lngRes = objHRPServ.lngExecuteParameterSQL(p_strSql, ref lngAff, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 查询未提交的表单
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetUnSubmitForm(string p_strEmployeeID,out clsCustom_UnSubmitValue[] p_objContent)
		{
			p_objContent = null;
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
            try
            {
                DataTable dtResult = new DataTable();
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[1];

                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strEmployeeID.Trim();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetUnSubmitForms, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    p_objContent = new clsCustom_UnSubmitValue[dtResult.Rows.Count];
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        p_objContent[i] = new clsCustom_UnSubmitValue();
                        p_objContent[i].m_strEmployeeID = dtResult.Rows[i]["EMPLOYEEID"].ToString().Trim();
                        p_objContent[i].m_strFormName = dtResult.Rows[i]["FORMNAME"].ToString().Trim();
                        try
                        {
                            p_objContent[i].m_intFormID = Convert.ToInt32(dtResult.Rows[i]["FORM_ID"]);
                        }
                        catch { return 0; }
                        p_objContent[i].m_objPagesArr = m_objGetPageAndConfig(c_strGetUnSubmitPages, p_objContent[i].m_intFormID);
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}
		/// <summary>
		/// 查询未提交的表单的配置文件
		/// </summary>
		/// <param name="p_strSqlType"></param>
		/// <param name="p_intFormID"></param>
		/// <returns></returns>
		[AutoComplete]
		private clsCustom_PagesValue[] m_objGetPageAndConfig(string p_strSqlType,int p_intFormID)
		{
			if(p_intFormID <= 0 || p_strSqlType == null || p_strSqlType == string.Empty)
				return null;
			
		    clsHRPTableService objHRPServ =new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            objHRPServ.CreateDatabaseParameter(1, out arrParams);
            arrParams[0].Value = p_intFormID.ToString();

			clsCustom_PagesValue[] p_objContent = null;
 
			DataTable dtResult = new DataTable();
            long lngRes = objHRPServ.lngGetDataTableWithParameters(p_strSqlType, ref dtResult, arrParams);
            //objHRPServ.Dispose();
			if(lngRes > 0 && dtResult.Rows.Count > 0)
			{
				p_objContent = new clsCustom_PagesValue[dtResult.Rows.Count];
				for(int i = 0; i < dtResult.Rows.Count; i++)
				{
					p_objContent[i] = new clsCustom_PagesValue();
					try
					{
						p_objContent[i].m_intPages = int.Parse(dtResult.Rows[i]["PAGE"].ToString().Trim());
					}
					catch{p_objContent[i].m_intPages = i;}
					p_objContent[i].m_strConfiguration = System.Text.Encoding.Unicode.GetString((byte[])(dtResult.Rows[i]["CONFIGURATION"]));
				}
			}
			return p_objContent;
		}
		/// <summary>
		/// 删除未提交表单
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDelUnSubmitForm(string p_strEmployeeID,int p_intFormID)
		{
			if(p_strEmployeeID=="" && p_intFormID <= 0)
				return -1;
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
            try
            {
                long lngRef = m_lngDelUnSubmitPages(p_intFormID);
                if (lngRef <= 0)
                    return lngRef;
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //			for(int i=0;i<objDPArr.Length;i++)
                //				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strEmployeeID;
                objDPArr[1].Value = p_intFormID;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDelUnSubmitForms, ref lngAff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;
		}
		/// <summary>
		/// 删除未提交表单的配置文件
		/// </summary>
		/// <param name="p_intFormID"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngDelUnSubmitPages(int p_intFormID)
		{
			if(p_intFormID <= 0)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"Delete FROM CUSTOMFORM_UNSUBMIT_CONF WHERE FORM_ID='"+p_intFormID.ToString()+"'";

			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 停用已提交表单
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngHaltSubmitForm(int p_intFormID,string p_strDeactivedid,DateTime p_dtmDeactivedTime)
        {
            #region 旧的部分
        //    if (p_intFormID <= 0)
        //        return -1;
			
        //clsHRPTableService objHRPServ =new clsHRPTableService();
        //string strHaltSubmitForms = @"Update customform set Status=1 where form_id='" + p_intFormID.ToString() + "' and deactivedid ='"
        //        +p_strDeactivedid+"'and deactiveddate ="+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmDeactivedTime);
        //    long lngRes = objHRPServ.DoExcute(strHaltSubmitForms);
        //    //objHRPServ.Dispose();
        //    return lngRes;
            #endregion 

            #region 修改部分
            if (p_intFormID <= 0)
                return -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strHaltSubmitForms = @"update customform
   set status = 1, deactivedid = ?, deactiveddate = ?
 where form_id = ?
   and status = 0";

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strDeactivedid;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = p_dtmDeactivedTime;
            objDPArr[2].Value = p_intFormID;

            long lngEff = -1;
            long lngRes = objHRPServ.lngExecuteParameterSQL(strHaltSubmitForms, ref lngEff, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
            #endregion 
        }
		
		/// <summary>
		/// 更新未提交表单
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateForm(clsCustom_UnSubmitValue p_objContent)
		{
			if(p_objContent==null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			IDataParameter[] objDPArr=null;//new Oracle.DataAccess.Client.OracleParameter[3];
			
			objHRPServ.CreateDatabaseParameter(3,out objDPArr);
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
			objDPArr[0].Value=p_objContent.m_strFormName;
			objDPArr[1].Value=p_objContent.m_strEmployeeID;
			objDPArr[2].Value=p_objContent.m_intFormID;
			long lngAff = 0;
			long lngRef = objHRPServ.lngExecuteParameterSQL(c_strUpdateUnSubmitForms,ref lngAff,objDPArr);
            //objHRPServ.Dispose();
			if(lngRef <=0)
				return lngRef;
			lngRef = m_lngDelUnSubmitPages(p_objContent.m_intFormID);
			if(lngRef <=0)
				return lngRef;
			for(int j2=0;j2<p_objContent.m_objPagesArr.Length;j2++)
			{
				lngRef = m_lngAddPageAndConfig(c_strAddUnSubmitPages,p_objContent.m_intFormID,p_objContent.m_objPagesArr[j2]);
				if(lngRef <=0)
					return lngRef;
			}
			return lngRef;
		}
		/// <summary>
		/// 更新已提交表单(页面及模板关联)
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateSubmitForm(clsCustom_SubmitValue p_objContent)
		{
			if(p_objContent==null)
				return -1;
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            clsGetAllRuningFormsServ2 objGetAllRuningFormsServ = new clsGetAllRuningFormsServ2();
			long lngRef = 0;
            try
            {
                lngRef = m_lngUpdateSunmitFormName(p_objContent.m_strFormName, p_objContent.m_intFormID);
                if (lngRef > 0)
                {
                    lngRef = m_lngDelSubmitPages(p_objContent.m_intFormID);
                }
                if (lngRef > 0)
                {
                    for (int j2 = 0; j2 < p_objContent.m_objPagesArr.Length; j2++)
                    {
                        lngRef = m_lngAddPageAndConfig(c_strAddSubmitPages, p_objContent.m_intFormID, p_objContent.m_objPagesArr[j2]);
                        if (lngRef <= 0)
                            break;
                    }
                }
                if (lngRef > 0)
                {
                    lngRef = objGetAllRuningFormsServ.m_lngDelGUI_Info_Detail(c_strFormBaseName + p_objContent.m_intFormID.ToString());
                }
                if (lngRef > 0)
                {
                    lngRef = objGetAllRuningFormsServ.m_lngDelGUI_Info(c_strFormBaseName + p_objContent.m_intFormID.ToString());
                }
                if (lngRef > 0)
                {
                    lngRef = m_lngSaveTemplate_GUI_Info(c_strFormBaseName + p_objContent.m_intFormID.ToString(), p_objContent.m_strFormName, p_objContent.m_objPagesArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objGetAllRuningFormsServ.Dispose();
            }
			return lngRef;
		}
		/// <summary>
		/// 更新已提交表单名称
		/// </summary>
		/// <param name="p_strFormName"></param>
		/// <param name="p_intFormID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateSunmitFormName(string p_strFormName,int p_intFormID)
		{
			if(p_strFormName == null || p_strFormName == string.Empty || p_intFormID <= 0)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"update CustomForm set form_name = '"+p_strFormName+"' where form_id = '" +p_intFormID.ToString() +"'";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 更新已提交表单分类及位置
		/// </summary>
		/// <param name="p_intNewTypeID"></param>
		/// <param name="p_intNewPos"></param>
		/// <param name="p_intFormID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateSubmitFormTypeAndPosision(int p_intNewTypeID,int p_intNewPos,int p_intFormID)
		{
			if(p_intNewTypeID <= 0 || p_intNewPos < 0 || p_intFormID <= 0)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"update CustomForm set type_id = '"+p_intNewTypeID.ToString()+"' , position = '"+p_intNewPos.ToString()+"' where form_id = '" +p_intFormID.ToString() +"'";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 删除已提交表单页面
		/// </summary>
		/// <param name="p_intFormID"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngDelSubmitPages(int p_intFormID)
		{
			if(p_intFormID <= 0)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"DELETE FROM customform_conf WHERE form_id = '"+p_intFormID.ToString()+"'";
            long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 提交新表单
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSubmitNewForm(ref clsCustom_SubmitValue p_objContent)
		{
			if(p_objContent==null)
				return -1;
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes =0;
            try
            {

                //分类
                lngRes = m_lngAddType(ref p_objContent.m_objType);
                int intID = 0;
                if (lngRes > 0)
                {
                    intID = objHRPServ.intGetNewNumericID("FORM_ID", "CUSTOMFORM");
                    if (intID == 0) intID = 1;
                }
                if (intID > 0)
                {
                    //主表
                    p_objContent.m_intFormID = intID;
                    lngRes = m_lngAddSubmitForm(p_objContent);
                }
                if (lngRes > 0)
                {
                    for (int i = 0; i < p_objContent.m_objPagesArr.Length; i++)
                    {
                        lngRes = m_lngAddPageAndConfig(c_strAddSubmitPages, intID, p_objContent.m_objPagesArr[i]);
                        if (lngRes <= 0)
                            break;
                    }
                }
                if (lngRes > 0)
                {
                    //可见科室
                    if (p_objContent.m_intVisible_Level == 2 && p_objContent.m_strDepts != null)
                        lngRes = m_lngAddDeptVisible(intID, p_objContent.m_strDepts);
                }
                if (lngRes > 0)
                {
                    lngRes = m_lngSaveTemplate_GUI_Info(c_strFormBaseName + p_objContent.m_intFormID.ToString(), p_objContent.m_strFormName, p_objContent.m_objPagesArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRes;

		}
		/// <summary>
		/// 添加表单类型
		/// </summary>
		/// <param name="p_objType"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddType(ref clsCustom_Type p_objType)
		{
			int intID = 0;
			if(p_objType == null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"select type_id from customform_type where parent_id = '"+p_objType.m_intParentID+"' and type_name = '"+p_objType.m_strTypeName+"'";
			DataTable dtResult = new DataTable();
			long lngRef = objHRPServ.DoGetDataTable(strSql,ref dtResult);
			if(lngRef<=0 || dtResult.Rows.Count <= 0)
			{
				intID = objHRPServ.intGetNewNumericID("TYPE_ID","CUSTOMFORM_TYPE");
				if(intID == 0) intID = 1;
			}
			else
			{
                try
                {
                    p_objType.m_intTypeID = Convert.ToInt32(dtResult.Rows[0]["TYPE_ID"]);
                }
                catch
                {
                    //objHRPServ.Dispose();
                    return 0;
                }
                //objHRPServ.Dispose();
				return 1;
			}
			p_objType.m_intTypeID = intID;
			strSql = @"INSERT INTO customform_type(type_id, parent_id, type_name,employeeid) VALUES ('"+intID+"', '"+p_objType.m_intParentID+"', '"+p_objType.m_strTypeName+"', '"+p_objType.m_strCreateID+"')";

			long lngRes =  objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
			return lngRes;
		}
		/// <summary>
		/// 修改分类
		/// </summary>
		/// <param name="p_objType"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateType(clsCustom_Type p_objType)
		{
			if(p_objType == null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"update customform_type set type_name = '"+p_objType.m_strTypeName+"',parent_id = '"+p_objType.m_intParentID.ToString()+"' where type_id = '"+p_objType.m_intTypeID.ToString()+"'";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
			return lngRes;
		}
		/// <summary>
		/// 修改分类
		/// </summary>
		/// <param name="p_intNewParentID"></param>
		/// <param name="p_objType"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateType(int p_intNewParentID,clsCustom_Type p_objType)
		{
			if(p_objType == null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"update customform_type set parent_id = '"+p_intNewParentID.ToString()+"' where parent_id = '"+p_objType.m_intParentID.ToString()+"' and type_id = '"+p_objType.m_intTypeID.ToString()+"'";
			long lngRes =  objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
			return lngRes;
		}
		/// <summary>
		/// 删除分类
		/// </summary>
		/// <param name="p_intType_ID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteTypeAndSetChild(int p_intType_ID)
		{
			if(p_intType_ID <= 0)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = m_lngSetParentType(p_intType_ID);
			if(lngRes <= 0)
				return lngRes;
			string strSql = @"delete from customform_type where type_id = '"+p_intType_ID.ToString()+"'";
			lngRes =  objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
			return lngRes;
		}
		/// <summary>
		/// 删除分类
		/// </summary>
		/// <param name="p_intType_ID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteType(int p_intType_ID)
		{
			if(p_intType_ID <= 0)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"delete from customform_type where type_id = '"+p_intType_ID.ToString()+"'";
			long lngRes =  objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
			return lngRes;
		}
		/// <summary>
		/// 在删除分类之前把子类设置为根类
		/// </summary>
		/// <param name="p_intParentID"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngSetParentType(int p_intParentID)
		{
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"update customform_type set parent_id = '0' where parent_id = '"+p_intParentID.ToString()+"'";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
			return lngRes;
		}
		/// <summary>
		/// 添加提交表单主表
		/// </summary>
		/// <param name="p_objContent"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngAddSubmitForm(clsCustom_SubmitValue p_objContent)
		{
			if(p_objContent == null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			IDataParameter[] objDPArr=null;//new Oracle.DataAccess.Client.OracleParameter[6];
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
			
			objHRPServ.CreateDatabaseParameter(6,out objDPArr);
			objDPArr[0].Value=p_objContent.m_intFormID;
			objDPArr[1].Value=p_objContent.m_objType.m_intTypeID;
			objDPArr[2].Value=p_objContent.m_strFormName;
			objDPArr[3].Value=p_objContent.m_intPosition;
			objDPArr[4].Value=p_objContent.m_strEmployeeID;
			objDPArr[5].Value=p_objContent.m_intVisible_Level;
			long lngAff = 0;
            long lngRes = objHRPServ.lngExecuteParameterSQL(c_strSubmitNewForm, ref lngAff, objDPArr);
            //objHRPServ.Dispose();
			return lngRes;
		}
		/// <summary>
		/// 添加需要传送的字段信息
		/// </summary>
		/// <param name="p_objField"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddTarSyncField(ref clsCustom_Sync p_objField)
		{
			return m_lngAddSyncField(c_strAddTarSync,ref p_objField);
		}
		/// <summary>
		/// 添加需要传送的字段信息
		/// </summary>
		/// <param name="p_objField"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddSouSyncField(ref clsCustom_Sync p_objField)
		{
			return m_lngAddSyncField(c_strAddSouSync,ref p_objField);
		}
		/// <summary>
		/// 添加需要传送的字段信息
		/// </summary>
		/// <param name="p_objField"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngAddSyncField(string p_strSql,ref clsCustom_Sync p_objField)
		{
			if(p_objField == null || p_strSql == null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			IDataParameter[] objDPArr=null;//new Oracle.DataAccess.Client.OracleParameter[3];
			
			objHRPServ.CreateDatabaseParameter(3,out objDPArr);
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i] = new Oracle.DataAccess.Client.OracleParameter();
			objDPArr[0].Value = p_objField.m_intForm_ID;
			objDPArr[1].Value = p_objField.m_strTarget_Form_ID;
			objDPArr[2].Value = System.Text.Encoding.Unicode.GetBytes(p_objField.m_strSyncField);
			long lngAff = 0;
			long lngRes = objHRPServ.lngExecuteParameterSQL(p_strSql,ref lngAff,objDPArr);
            
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 修改需要传送的字段信息
		/// </summary>
		/// <param name="p_objField"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateTarSyncField(clsCustom_Sync p_objField)
		{
			return m_lngUpdateSyncField( c_strUpdateTarSync,p_objField);
		}
		/// <summary>
		/// 修改需要传送的字段信息
		/// </summary>
		/// <param name="p_objField"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateSouSyncField(clsCustom_Sync p_objField)
		{
			return m_lngUpdateSyncField( c_strUpdateSouSync,p_objField);
		}
		/// <summary>
		/// 修改需要传送的字段信息
		/// </summary>
		/// <param name="p_objField"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngUpdateSyncField( string p_strSql,clsCustom_Sync p_objField)
		{
			if(p_objField == null || p_strSql == null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			IDataParameter[] objDPArr=null;//new Oracle.DataAccess.Client.OracleParameter[3];
			
			objHRPServ.CreateDatabaseParameter(3,out objDPArr);
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i] = new Oracle.DataAccess.Client.OracleParameter();
			objDPArr[0].Value = System.Text.Encoding.Unicode.GetBytes(p_objField.m_strSyncField);
			objDPArr[1].Value = p_objField.m_intForm_ID;
			objDPArr[2].Value = p_objField.m_strTarget_Form_ID;
			long lngAff = 0;
			long lngRes = objHRPServ.lngExecuteParameterSQL(p_strSql,ref lngAff,objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 删除需要传送的字段信息
		/// </summary>
		/// <param name="p_intSync_ID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteTarSync(int p_intForm_ID,string p_strTargetFormID)
		{
			if(p_intForm_ID <= 0 || p_strTargetFormID == null || p_strTargetFormID == string.Empty)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"delete from customform_sync_tarcnfg where form_id = '"+p_intForm_ID.ToString()+"'and target_form_id = '"+p_strTargetFormID+"'";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 删除需要传送的字段信息
		/// </summary>
		/// <param name="p_intSync_ID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteSouSync(int p_intForm_ID,string p_strSourceFormID)
		{
			if(p_intForm_ID <= 0 || p_strSourceFormID == null || p_strSourceFormID == string.Empty)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"delete from customform_sync_soucnfg where form_id = '"+p_intForm_ID.ToString()+"'and source_form_id = '"+p_strSourceFormID+"'";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 获取传送的字段
		/// </summary>
		/// <param name="p_intForm_ID"></param>
		/// <param name="p_objSyncField"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTarSyncField(int p_intForm_ID,string p_strTargetFormID,out clsCustom_Sync p_objSyncField)
		{
			p_objSyncField = new clsCustom_Sync();
			if(p_intForm_ID <=0 || p_strTargetFormID == null || p_strTargetFormID == string.Empty)
				return -1;

			p_objSyncField.m_intForm_ID = p_intForm_ID;
			p_objSyncField.m_strTarget_Form_ID = p_strTargetFormID;
			string strSql = @"SELECT CONFIG FROM customform_sync_tarcnfg WHERE form_id = '"+p_intForm_ID+"' and target_form_id = '"+p_strTargetFormID+"'";
			p_objSyncField.m_strSyncField = m_strGetSyncField(strSql);
			if(p_objSyncField.m_strSyncField == null)
				return 0;
			return 1;
		}
		/// <summary>
		/// 获取传送的字段
		/// </summary>
		/// <param name="p_intForm_ID"></param>
		/// <param name="p_objSyncField"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetSouSyncField(int p_intForm_ID,string p_strSourceFormID,out clsCustom_Sync p_objSyncField)
		{
			p_objSyncField = new clsCustom_Sync();
			if(p_intForm_ID <=0 || p_strSourceFormID == null || p_strSourceFormID == string.Empty)
				return -1;
			p_objSyncField.m_intForm_ID = p_intForm_ID;
			p_objSyncField.m_strTarget_Form_ID = p_strSourceFormID;
			string strSql = @"SELECT CONFIG FROM customform_sync_soucnfg WHERE form_id = '"+p_intForm_ID+"' and source_form_id = '"+p_strSourceFormID+"'";
			p_objSyncField.m_strSyncField = m_strGetSyncField(strSql);
			if(p_objSyncField.m_strSyncField == null)
				return 0;
			return 1;
		}
		/// <summary>
		/// 获取传送字段设置
		/// </summary>
		/// <param name="p_intFormID"></param>
		/// <returns></returns>
		[AutoComplete]
		private string m_strGetSyncField(string p_strSql)
		{
		clsHRPTableService objHRPServ =new clsHRPTableService();
			DataTable dtResult = new DataTable();
			long lngRef = objHRPServ.DoGetDataTable(p_strSql,ref dtResult);
            //objHRPServ.Dispose();
			if(lngRef <= 0 || dtResult.Rows.Count != 1)
				return null;
			return System.Text.Encoding.Unicode.GetString((byte[])(dtResult.Rows[0]["CONFIG"]));
		}
		/// <summary>
		/// 添加可见科室信息
		/// </summary>
		/// <param name="p_intFormID"></param>
		/// <param name="p_strDeptIDArr"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngAddDeptVisible(int p_intFormID,string[] p_strDeptIDArr)
		{
			if(p_intFormID <=0 || p_strDeptIDArr == null || p_strDeptIDArr.Length == 0)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
        //objHRPServ.Dispose();
			int intResult = 0;
			for(int i=0;i<p_strDeptIDArr.Length;i++)
			{
				string strSql = @"INSERT INTO CUSTOMFORM_DEPT (form_id, deptid)  VALUES ('"+p_intFormID+"', '"+p_strDeptIDArr[i]+"')";
				if(objHRPServ.DoExcute(strSql) > 0)
					intResult = 1;
			}
			return intResult;
		}
		/// <summary>
		/// 获取已提交的并且该用户可使用的表单（单个科室）
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_objContents"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetSubmitForms(string p_strEmployeeID,string p_strDeptID,out clsCustom_SubmitValue[] p_objContents)
		{
			p_objContents = null;
			if(p_strDeptID == null || p_strDeptID == string.Empty)
				return -1;
			string strDept = @"deptid = '"+p_strDeptID+"'";
			return m_lngGetSubmitFormsByEmpCanUser(p_strEmployeeID,strDept,out p_objContents);
		}
		/// <summary>
		/// 获取已提交的并且该用户可使用的表单（多个科室）
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strDeptIDArr"></param>
		/// <param name="p_objContents"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetSubmitForms(string p_strEmployeeID,string[] p_strDeptIDArr,out clsCustom_SubmitValue[] p_objContents)
		{
			p_objContents = null;
			if(p_strDeptIDArr == null || p_strDeptIDArr.Length <= 0)
				return -1;
			string strDept = "";
			foreach(string str in p_strDeptIDArr)
			{
				strDept += "deptid = '"+str+"' or ";
			}
			strDept = strDept.Substring(0,strDept.Length-4);
			return m_lngGetSubmitFormsByEmpCanUser(p_strEmployeeID,strDept,out p_objContents);
		}
		/// <summary>
		///  获取已提交的并且该用户可用的表单
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_strDeptSQL"></param>
		/// <param name="p_objContents"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetSubmitFormsByEmpCanUser(string p_strEmployeeID,string p_strDeptSQL,out clsCustom_SubmitValue[] p_objContents)
		{
			
			p_objContents = null;

			#region sql
			string strSql = @"SELECT a.*, t.parent_id, t.type_name
FROM customform a INNER JOIN customform_type t ON a.type_id = t.type_id
WHERE ( a.visible_level = 1 OR (a.visible_level = 0 AND a.employeeid = '"+p_strEmployeeID+@"')
OR ( a.visible_level =2 
AND (SELECT COUNT (*) FROM CUSTOMFORM_DEPT WHERE form_id = a.form_id AND (<DEPTIDARR>)) >0)) and a.status = 0 ";
			#endregion
			
			strSql = strSql.Replace("<DEPTIDARR>",p_strDeptSQL);
			return m_lngSubGetSubmitForms(p_strEmployeeID,strSql,out p_objContents);
		}
		/// <summary>
		///  获取已提交的并且该用户可修改的表单
		/// </summary>
		/// <param name="p_strEmployeeID"></param>
		/// <param name="p_objContents"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetSubmitForms(string p_strEmployeeID,out clsCustom_SubmitValue[] p_objContents)
		{
			p_objContents = null;

			#region sql
			string strSql = @"SELECT a.*, t.parent_id, t.type_name
FROM customform a INNER JOIN customform_type t ON a.type_id = t.type_id
WHERE a.employeeid = '"+p_strEmployeeID+@"' and a.status = '0'";
			#endregion
			return m_lngSubGetSubmitForms(p_strEmployeeID,strSql,out p_objContents);
		}
		/// <summary>
		/// 获取已提交的表单
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngSubGetSubmitForms(string p_strEmployeeID,string p_strSQL,out clsCustom_SubmitValue[] p_objContents)
		{
			p_objContents = null;

			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			DataTable dtResult = new DataTable();
			long lngRes = objHRPServ.DoGetDataTable(p_strSQL,ref dtResult);
            //objHRPServ.Dispose();
			if(lngRes  <= 0)
				return lngRes;
			if(dtResult.Rows.Count > 0)
			{
				p_objContents = new clsCustom_SubmitValue[dtResult.Rows.Count];
				for(int i = 0; i < dtResult.Rows.Count; i++)
				{
					p_objContents[i] = new clsCustom_SubmitValue();
					try
					{
						p_objContents[i].m_intFormID = Convert.ToInt32(dtResult.Rows[i]["FORM_ID"]);}
					catch{return 0;}
					p_objContents[i].m_strFormName = dtResult.Rows[i]["FORM_NAME"].ToString();
					try
					{
						p_objContents[i].m_intPosition = Convert.ToInt32(dtResult.Rows[i]["POSITION"]);}
					catch{p_objContents[i].m_intPosition = 1;}
					try
					{
						p_objContents[i].m_intStatus = Convert.ToInt32(dtResult.Rows[i]["STATUS"]);}
					catch{p_objContents[i].m_intStatus = 0;}
					try
					{
						p_objContents[i].m_intVisible_Level = int.Parse(dtResult.Rows[i]["VISIBLE_LEVEL"].ToString());}
					catch{p_objContents[i].m_intVisible_Level = 0;}
					p_objContents[i].m_objPagesArr = m_objGetPageAndConfig(c_strGetSubmitPages,p_objContents[i].m_intFormID);
					p_objContents[i].m_objType = new clsCustom_Type();
					try
					{
						p_objContents[i].m_objType.m_intTypeID = Convert.ToInt32(dtResult.Rows[i]["TYPE_ID"]);}
					catch{return 0;}
					try
					{
						p_objContents[i].m_objType.m_intParentID = Convert.ToInt32(dtResult.Rows[i]["PARENT_ID"]);}
					catch{return 0;}
					p_objContents[i].m_objType.m_strTypeName = dtResult.Rows[i]["TYPE_NAME"].ToString().Trim();
//					p_objContents[i].m_objSyncField = m_objGetSyncField(p_objContents[i].m_intFormID);
					p_objContents[i].m_strEmployeeID = p_strEmployeeID;
				}
			}
			return lngRes;
		}
			

		/// <summary>
		/// 保存模板需要的界面信息
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngSaveTemplate_GUI_Info(string p_strFormID,
			string p_strFormDesc,clsCustom_PagesValue[] p_objPages)
		{
			if(p_strFormID == null || p_objPages == null || p_objPages.Length == 0)
				return -1;

		clsHRPTableService objHRPServ =new clsHRPTableService();
			int intResult = 1;
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[2];

                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                //				for(int j=0;j<objDPArr.Length;j++)
                //					objDPArr[j]=new Oracle.DataAccess.Client.OracleParameter();
                objDPArr[0].Value = p_strFormID;
                objDPArr[1].Value = p_strFormDesc;
                long lngAff = 0;
                long lngRes = objHRPServ.lngExecuteParameterSQL(c_strSaveTemplate_GUI_Info, ref lngAff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                for (int k2 = 0; k2 < p_objPages.Length; k2++)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(p_objPages[k2].m_strConfiguration);
                    XmlElement root = doc.DocumentElement;
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        if ((root.ChildNodes[i].Attributes["TYPE"].Value == "ctlRichTextBox" && root.ChildNodes[i].Attributes["DESCRIPTION"].Value.ToString().Trim() != "") ||
                            (root.ChildNodes[i].Attributes["TYPE"].Value == "ctlComboBox" && root.ChildNodes[i].Attributes["DESCRIPTION"].Value.ToString().Trim() != ""))
                        {
                            IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[3];
                            //							for(int j=0;j<objDPArr2.Length;j++)
                            //								objDPArr2[j]=new Oracle.DataAccess.Client.OracleParameter();
                            objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                            objDPArr2[0].Value = p_strFormID;
                            objDPArr2[1].Value = root.ChildNodes[i].Attributes["NAME"].Value;
                            objDPArr2[2].Value = root.ChildNodes[i].Attributes["DESCRIPTION"].Value;
                            lngRes = objHRPServ.lngExecuteParameterSQL(c_strSaveTemplate_GUI_Info_Detail, ref lngAff, objDPArr2);
                            if (lngRes <= 0)
                                intResult = 0;
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return intResult;			
		}
		/// <summary>
		///获取所有分类
		/// </summary>
		/// <param name="p_objTypeArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngLoadAllType(out clsCustom_Type[] p_objTypeArr)
		{
			p_objTypeArr = null;
			string strSql = @"select * from customform_type";
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			DataTable dtbValues = new DataTable();
			long lngRef = objHRPServ.DoGetDataTable(strSql,ref dtbValues);
            //objHRPServ.Dispose();
			if(lngRef <=0 || dtbValues.Rows.Count <= 0)
				return 0;
			p_objTypeArr = new clsCustom_Type[dtbValues.Rows.Count];
			for(int i=0;i<dtbValues.Rows.Count;i++)
			{
				p_objTypeArr[i] = new clsCustom_Type();
				try
				{
					p_objTypeArr[i].m_intTypeID = Convert.ToInt32(dtbValues.Rows[i]["TYPE_ID"]);
				}
				catch{}
				try
				{
					p_objTypeArr[i].m_intParentID = Convert.ToInt32(dtbValues.Rows[i]["PARENT_ID"]);
				}
				catch{}
				p_objTypeArr[i].m_strTypeName = dtbValues.Rows[i]["TYPE_NAME"].ToString().Trim();
				p_objTypeArr[i].m_strCreateID = dtbValues.Rows[i]["EMPLOYEEID"].ToString().Trim();
			}
			return lngRef;
		}
		/// <summary>
		/// 检查是否有子类
		/// </summary>
		/// <param name="p_intType_ID"></param>
		/// <param name="p_intAmount"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckChildType(int p_intType_ID,out int p_intTypeAmount,out int p_intSubmitAmount)
		{
			p_intTypeAmount = 0;
			p_intSubmitAmount = 0;
			if(p_intType_ID <= 0)
				return 0;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"select (select count(*) from customform_type where parent_id = '"+p_intType_ID.ToString()+@"') as typeamount, (select count(*) from customform a inner join customform_type b on a.type_id = b.type_id 
where b.parent_id = '"+p_intType_ID.ToString()+@"') as submitamount ";
			if(clsHRPTableService.bytDatabase_Selector == 2)
				strSql += " from dual";
			DataTable dtResult = new DataTable();
			long lngRes = objHRPServ.DoGetDataTable(strSql,ref dtResult);
            //objHRPServ.Dispose();
			if(lngRes <= 0 || dtResult.Rows.Count != 1)
				return 0;
			try
			{
				p_intTypeAmount = Convert.ToInt32(dtResult.Rows[0]["TYPEAMOUNT"]);}
			catch{return 0;}
			try
			{
				p_intSubmitAmount = Convert.ToInt32(dtResult.Rows[0]["SUBMITAMOUNT"]);}
			catch{return 0;}
			return lngRes;

		}
		/// <summary>
		/// 检查表单名称
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <returns>存在返回“true”</returns>
		[AutoComplete]
		private bool m_blnCheckName(string p_strSql)
		{
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			DataTable dtbValue = new DataTable();
			long lngRef = objHRPServ.DoGetDataTable(p_strSql,ref dtbValue);
            //objHRPServ.Dispose();
			if(lngRef <= 0 || dtbValue.Rows.Count<= 0)
				return false;
			return dtbValue.Rows[0][0].ToString().Trim() != "0";
		}
		/// <summary>
		/// 检查未提交表单名称
		/// </summary>
		/// <param name="p_strName"></param>
		/// <returns></returns>
		[AutoComplete]
		public bool m_blnCheckUnSubmitName(string p_strName)
		{
			if(p_strName == null)
				return false;
			string strSQL = @"select count(*) num from CUSTOMFORM_UNSUBMITFORM where formname ='"+p_strName+"'";
			return m_blnCheckName(strSQL);
		}
		/// <summary>
		/// 检查已提交表单名称
		/// </summary>
		/// <param name="p_strParentID"></param>
		/// <param name="p_strName"></param>
		/// <returns></returns>
		[AutoComplete]
		public bool m_blnCheckSubmitName(int p_intParentID,string p_strName)
		{
			if(p_intParentID <= 0 || p_strName == null)
				return false;
			string strSQL = @"select count(a.form_name) num from CustomForm a inner join customform_type b on a.type_id = b.type_id where b.parent_id = '"+p_intParentID.ToString()+"' and a.form_name ='"+p_strName+"'";
			return m_blnCheckName(strSQL);
		}
		/// <summary>
		/// 更新表单位置
		/// </summary>
		/// <param name="p_intNewPos"></param>
		/// <param name="p_intForm_ID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyFormPosition(int p_intNewPos,int p_intForm_ID)
		{
			if(p_intNewPos < 0 || p_intForm_ID <= 0)
				return 0;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"update customform  set position = '"+p_intNewPos+"' where form_id = '"+p_intForm_ID+"'";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
            return lngRes;
		}

		#region 记录内容的操作数据库
		/// <summary>
		/// 添加新记录
		/// </summary>	
		[AutoComplete]
		public long m_lngAddNewRecord(ref clsCustom_Data p_objDataValue)
		{	
			if(p_objDataValue == null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			int intID = 0;
			long lngRef = 0;
            try
            {
                lngRef = objHRPServ.m_lngGetSequences("seq_emr_minelement", out intID);
                if (intID > 0)
                {
                    p_objDataValue.m_intRecord_ID = intID;
                    string strSql = @"insert into customform_data
  (record_id,
   appform_id,
   patientid,
   inpatientid,
   inpatientdate,
   opendate,
   createdate,
   createuserid,
   content,
   form_id,
   patientcardid,
   status,
   syncstatus,
   registerid_chr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '0', '0', ?)";
                    IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[11];

                    objHRPServ.CreateDatabaseParameter(12, out objDPArr);
                    //					for(int i=0;i<objDPArr.Length;i++)
                    //						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    objDPArr[0].Value = p_objDataValue.m_intRecord_ID;
                    objDPArr[1].Value = p_objDataValue.m_strAppForm_ID;
                    objDPArr[2].Value = p_objDataValue.m_strPatientID;
                    objDPArr[3].Value = p_objDataValue.m_strInPatientID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = p_objDataValue.m_dtmInPatientDate;
                    objDPArr[5].DbType = DbType.DateTime;
                    objDPArr[5].Value = p_objDataValue.m_dtmOpenDate;
                    objDPArr[6].DbType = DbType.DateTime;
                    objDPArr[6].Value = p_objDataValue.m_dtmCreateDate;
                    objDPArr[7].Value = p_objDataValue.m_strCreateUserID;
                    objDPArr[8].Value = System.Text.Encoding.Unicode.GetBytes(p_objDataValue.m_strContent);
                    objDPArr[9].Value = p_objDataValue.m_intForm_ID;
                    objDPArr[10].Value = p_objDataValue.m_strPatientCardID;
                    objDPArr[11].Value = p_objDataValue.m_strRegisterId;
                    long lngAff = 0;
                    lngRef = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return lngRef;
		}

		/// <summary>
		/// 根据记录ID获取记录
		/// </summary>
		/// <param name="p_strRecord_ID"></param>
		/// <param name="p_objDataArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecByRecord_ID(string p_strRecord_ID,out clsCustom_Data p_objData)
		{
			p_objData = null;
			if(p_strRecord_ID == null || p_strRecord_ID == string.Empty)
				return -1;
			string strSql = @"select * from customform_data where record_id = '"+p_strRecord_ID+"' and status = '0'";
			clsCustom_Data[] p_objDataArr = null;
			long lngRes = m_lngGetAllRecordContents(strSql,out  p_objDataArr);
			if(lngRes > 0 && p_objDataArr != null)
			{
				p_objData = p_objDataArr[0];
			}
			return lngRes;
		}
		/// <summary>
		/// 根据病人ID获取记录
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strAppFormID"></param>
		/// <param name="p_objDataArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecByPatientID(string p_strPatientID,string p_strAppFormID,out clsCustom_Data[] p_objDataArr)
		{
			p_objDataArr = null;
			if(p_strPatientID == null || p_strPatientID == string.Empty || p_strAppFormID == null || p_strAppFormID == string.Empty)
				return -1;
			string strSql = @"select * from customform_data where PATIENTID = '"+p_strPatientID+"' and APPFORM_ID ='"+p_strAppFormID+"' and status = '0'";
			return m_lngGetAllRecordContents(strSql,out  p_objDataArr);
		}
		/// <summary>
		/// 根据住院ID和住院日期获取记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_dtmInPatientDate"></param>
		/// <param name="p_strAppFormID"></param>
		/// <param name="p_objDataArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecByInPatientInfo(string p_strInPatientID,DateTime p_dtmInPatientDate,string p_strAppFormID,out clsCustom_Data[] p_objDataArr)
		{
			p_objDataArr = null;
			if(p_strInPatientID == null || p_strInPatientID == string.Empty || p_dtmInPatientDate == DateTime.MinValue || p_strAppFormID == null || p_strAppFormID == string.Empty)
				return -1;
			string strSql = @"select * from customform_data where INPATIENTID = '"+p_strInPatientID+"' and INPATIENTDATE = "+ clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmInPatientDate) + " and APPFORM_ID = '"+p_strAppFormID+"' and status = '0'";
			return m_lngGetAllRecordContents(strSql,out  p_objDataArr);
		}
		/// <summary>
		/// 获取所有住院记录内容
		/// </summary>
		/// <param name="p_objDataArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllRecords(out clsCustom_Data[] p_objDataArr)
		{
			p_objDataArr = null;
			string strSql = @"select * from customform_data";
			return m_lngGetAllRecordContents(strSql,out  p_objDataArr);
		}
        /// <summary>
        /// 获取所有住院记录内容
        /// </summary>
        /// <param name="p_objDataArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllForSeachRecords(out clsCustom_Data[] p_objDataArr)
        {
            p_objDataArr = null;
            string strSql = @"select record_id,
       appform_id,
       patientid,
       inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       content,
       status,
       deactiveddate,
       deactiveoperatorid,
       form_id,
       syncstatus,
       patientcardid,
       registerid_chr
  from customform_data 
 where registerid_chr is not null";
            return m_lngGetAllRecordContents(strSql, out  p_objDataArr);
        }
		/// <summary>
		/// 获取所有未停用记录内容
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetAllRecordContents(string p_strSql,out clsCustom_Data[] p_objDataArr)
		{
			p_objDataArr = null;
			if(p_strSql == null || p_strSql == "")
				return -1;

		clsHRPTableService objHRPServ =new clsHRPTableService();
			DataTable dtbValues = new DataTable();
			long lngRes = objHRPServ.DoGetDataTable(p_strSql,ref dtbValues);
            int intRowCount = dtbValues.Rows.Count;
            if (lngRes <= 0 || intRowCount <= 0)
				return 0;
            p_objDataArr = new clsCustom_Data[intRowCount];
            DataRow objRow = null;
            for (int i = 0; i < intRowCount; i++)
			{
                objRow = dtbValues.Rows[i];
                clsCustom_Data objData = new clsCustom_Data();
				try
				{
                    objData.m_intRecord_ID = int.Parse(objRow["RECORD_ID"].ToString());
                }
				catch{return 0;}
				try
				{
					objData.m_intForm_ID = int.Parse(objRow["FORM_ID"].ToString());}
				catch{return 0;}
				try
				{
					objData.m_intStatus = int.Parse(objRow["STATUS"].ToString());}
				catch{return 0;}
				objData.m_strAppForm_ID = objRow["APPFORM_ID"].ToString();
				objData.m_strPatientID = (objRow["PATIENTID"] == DBNull.Value?"":objRow["PATIENTID"].ToString().Trim());
				objData.m_strPatientCardID = (objRow["PATIENTCARDID"] == DBNull.Value?"":objRow["PATIENTCARDID"].ToString().Trim());
				objData.m_strInPatientID = (objRow["INPATIENTID"] == DBNull.Value?"":objRow["INPATIENTID"].ToString());
				try
				{
					objData.m_dtmInPatientDate = DateTime.Parse(objRow["INPATIENTDATE"].ToString());}
				catch{objData.m_dtmInPatientDate = DateTime.MinValue;}
				try
				{
					objData.m_dtmOpenDate = DateTime.Parse(objRow["OPENDATE"].ToString());}
				catch{objData.m_dtmOpenDate = DateTime.MinValue;}
				try
				{
					objData.m_dtmCreateDate = DateTime.Parse(objRow["CREATEDATE"].ToString());}
				catch{objData.m_dtmCreateDate = DateTime.MinValue;}
				objData.m_strCreateUserID = objRow["CREATEUSERID"].ToString().Trim();
                objData.m_strContent = System.Text.Encoding.Unicode.GetString((byte[])(objRow["CONTENT"]));
                objData.m_strRegisterId = objRow["registerid_chr"].ToString().Trim();
                p_objDataArr[i] = objData;
			}
			return lngRes;
		}

		/// <summary>
		/// 修改记录内容
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyRecord( clsCustom_Data p_objDataValue)
		{
			
			if(p_objDataValue == null)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"update customform_data set OPENDATE = ?,CONTENT = ? where RECORD_ID = ?";
			IDataParameter[] objDPArr=null;//new Oracle.DataAccess.Client.OracleParameter[3];
			
			objHRPServ.CreateDatabaseParameter(3,out objDPArr);
//			for(int i=0;i<objDPArr.Length;i++)
//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

            objDPArr[0].DbType = DbType.DateTime;
			objDPArr[0].Value = p_objDataValue.m_dtmOpenDate;
			objDPArr[1].Value = System.Text.Encoding.Unicode.GetBytes(p_objDataValue.m_strContent);
			objDPArr[2].Value = p_objDataValue.m_intRecord_ID;
			long lngAff = 0;
			long lngRef = objHRPServ.lngExecuteParameterSQL(strSql,ref lngAff,objDPArr);
            //objHRPServ.Dispose();
			return lngRef;
		}

		/// <summary>
		/// 删除记录
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecord(int p_intRecordID,string p_strOperatorID)
		{
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql= "UPDATE CUSTOMFORM_DATA SET STATUS = '1', DEACTIVEDDATE = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(DateTime.Now)+","+
				"DEACTIVEOPERATORID = '"+p_strOperatorID+"' Where RECORD_ID = '"+p_intRecordID+"' ";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
//			if(lngRes > 0)
//			{
//				lngRes = m_lngDeleteSync_Data(p_intRecordID);
//			}
			return lngRes;
		}
			
			
		#endregion

		#region 门诊
		/// <summary>
		/// 根据病区模糊查找病人
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_strLikeString"></param>
		/// <param name="p_objOutPatientInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientLikeIDByArea(string p_strAreaID,string p_strLikeString,out clsOutpatientInfo[] p_objOutPatientInfoArr)
		{
			p_objOutPatientInfoArr = null;
			if(p_strLikeString == null || p_strLikeString == string.Empty || p_strAreaID == null || p_strAreaID == string.Empty)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"select t2.*,<AGE> as age from t_opr_bih_register t1 inner join t_bse_patient t2
on t1.patientid_chr = t2.patientid_chr where t1.areaid_chr = '"+p_strAreaID+"' and t1.patientid_chr like '"+p_strLikeString+@"%'";
			if(clsHRPTableService.bytDatabase_Selector == 2)
				strSql.Replace("<AGE>","(to_number(To_Char(sysdate,'YYYY'))-to_number(To_Char(t2.birth_dat,'YYYY')))");
			else 
				strSql.Replace("<AGE>","DATEDIFF ( year , t2.birth_dat,getdate())");

			DataTable dtResult = new DataTable();
			long lngRes = objHRPServ.DoGetDataTable(strSql,ref dtResult);
            //objHRPServ.Dispose();
			if(lngRes <= 0 || dtResult.Rows.Count <= 0)
				return 0;
			p_objOutPatientInfoArr = new clsOutpatientInfo[dtResult.Rows.Count];
			for(int i=0;i<dtResult.Rows.Count;i++)
			{
				p_objOutPatientInfoArr[i] = new clsOutpatientInfo();
				p_objOutPatientInfoArr[i].m_StrPatientCardID = dtResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
				p_objOutPatientInfoArr[i].m_StrPatientName = dtResult.Rows[0]["FIRSTNAME_VCHR"].ToString().Trim();
				try
				{
					p_objOutPatientInfoArr[i].m_IntAge = Convert.ToInt32(dtResult.Rows[0]["AGE"]);
				}
				catch{p_objOutPatientInfoArr[i].m_IntAge = 100;}
				p_objOutPatientInfoArr[i].m_StrSex = dtResult.Rows[0]["SEX_CHR"].ToString().Trim();
			}
			return lngRes;
		}
		/// <summary>
		/// 获取本病区所有病人ID
		/// </summary>
		/// <param name="p_strAreaID"></param>
		/// <param name="p_strPatientIDArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllPatientIDByArea(string p_strAreaID,out string[] p_strPatientIDArr)
		{
			p_strPatientIDArr = null;
			if(p_strAreaID == null || p_strAreaID == string.Empty)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"select patientid_chr from t_opr_bih_register where areaid_chr = '"+p_strAreaID+"'";
			DataTable dtResult = new DataTable();
			long lngRes = objHRPServ.DoGetDataTable(strSql,ref dtResult);
            //objHRPServ.Dispose();
			if(lngRes <= 0 || dtResult.Rows.Count <= 0)
				return 0;
			p_strPatientIDArr = new string[dtResult.Rows.Count];
			for(int i=0;i<dtResult.Rows.Count;i++)
			{
				p_strPatientIDArr[i] = dtResult.Rows[i]["PATIENTID_CHR"].ToString().Trim();
			}
			return lngRes;
		}
		/// <summary>
		/// 根据病人卡号ID获取病人资料
		/// </summary>
		/// <param name="p_strPatientID">就诊卡号</param>
		/// <param name="p_objOutPatientInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPatientInfoByID(string p_strPatientCardID,out clsOutpatientInfo p_objOutPatientInfo)
		{
			p_objOutPatientInfo = null;
			if(p_strPatientCardID == null || p_strPatientCardID == string.Empty)
				return -1;
			string strSql = @"SELECT t1.*, t2.patientcardid_chr,<AGE> AS age
  FROM t_bse_patient t1 INNER JOIN t_bse_patientcard t2 ON t1.patientid_chr = t2.patientid_chr WHERE t2.patientcardid_chr = '"+p_strPatientCardID+"'";
			if(clsHRPTableService.bytDatabase_Selector == 2)
				strSql.Replace("<AGE>","(TO_NUMBER (TO_CHAR (SYSDATE, 'YYYY')) - TO_NUMBER (TO_CHAR (t1.birth_dat, 'YYYY')))");
			else 
				strSql.Replace("<AGE>","DATEDIFF ( year , t1.birth_dat,getdate())");
			DataTable dtResult = new DataTable();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            long lngRes = objHRPServ.DoGetDataTable(strSql, ref dtResult);
            //objHRPServ.Dispose();
			if(lngRes <= 0 || dtResult.Rows.Count != 1)
				return 0;
			p_objOutPatientInfo = new clsOutpatientInfo();
			p_objOutPatientInfo.m_StrPatientCardID = p_strPatientCardID;
			p_objOutPatientInfo.m_StrPatientID = dtResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
			p_objOutPatientInfo.m_StrPatientName = dtResult.Rows[0]["FIRSTNAME_VCHR"].ToString().Trim();
			try
			{
				p_objOutPatientInfo.m_IntAge = Convert.ToInt32(dtResult.Rows[0]["AGE"]);
			}
			catch{p_objOutPatientInfo.m_IntAge = 100;}
			p_objOutPatientInfo.m_StrSex = dtResult.Rows[0]["SEX_CHR"].ToString().Trim();
			return lngRes;
		}
		#endregion

		#region 同步接口
		/// <summary>
		/// 获取同步内容（住院）
		/// </summary>
		/// <param name="p_objSyncInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllSyncInfoForInPatient(out clsCustom_SyncInfo[] p_objSyncInfoArr)
		{
			p_objSyncInfoArr = null;
			string strSql = @"where d.inpatientid is not null";
			return m_lngSubGetAllSyncInfo(strSql,out p_objSyncInfoArr);
		}
		/// <summary>
		/// 获取同步内容（门诊）
		/// </summary>
		/// <param name="p_objSyncInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllSyncInfo(out clsCustom_SyncInfo[] p_objSyncInfoArr)
		{
			p_objSyncInfoArr = null;
			string strSql = @"where d.patientid is not null";
			return m_lngSubGetAllSyncInfo(strSql,out p_objSyncInfoArr);
		}
		/// <summary>
		/// 根据日期段获取同步内容（门诊）
		/// </summary>
		/// <param name="p_objSyncInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllSyncInfoByDate(DateTime p_dtmBeginDate,DateTime p_dtmEndDate,out clsCustom_SyncInfo[] p_objSyncInfoArr)
		{
			p_objSyncInfoArr = null;
			string strSql = @"where d.patientid is not null and (d.createdate between "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmBeginDate)+" and "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_dtmEndDate)+") ";
			return m_lngSubGetAllSyncInfo(strSql,out p_objSyncInfoArr);
		}
		/// <summary>
		/// 获取同步内容
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <param name="p_objSyncInfoArr"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngSubGetAllSyncInfo(string p_strSql,out clsCustom_SyncInfo[] p_objSyncInfoArr)
		{
			p_objSyncInfoArr = null;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"select d.*,c.CONFIG,c.TARGET_FORM_ID from CUSTOMFORM_DATA d inner join customform_sync_tarcnfg c on d.form_id = c.FORM_ID " + p_strSql+ " and d.STATUS = '0' and d.syncstatus = '0'";
			DataTable dtbValues = new DataTable();
			long lngRef = objHRPServ.DoGetDataTable(strSql,ref dtbValues);
            //objHRPServ.Dispose();
			if(lngRef <=0 || dtbValues.Rows.Count <= 0)
				return 0;
			p_objSyncInfoArr = new clsCustom_SyncInfo[dtbValues.Rows.Count];
			for(int i=0;i<dtbValues.Rows.Count;i++)
			{
				p_objSyncInfoArr[i] = new clsCustom_SyncInfo();
				try
				{
					p_objSyncInfoArr[i].m_intRecordID = Convert.ToInt32(dtbValues.Rows[i]["RECORD_ID"]);}
				catch{};
				p_objSyncInfoArr[i].m_strPatientID = dtbValues.Rows[i]["PATIENTID"].ToString();
				p_objSyncInfoArr[i].m_strPatientCardID = dtbValues.Rows[i]["PATIENTCARDID"].ToString();
				p_objSyncInfoArr[i].m_strInPatientID = dtbValues.Rows[i]["INPATIENTID"].ToString();
				p_objSyncInfoArr[i].m_dtmCreatedDate = DateTime.Parse(dtbValues.Rows[i]["CREATEDATE"].ToString());
				p_objSyncInfoArr[i].m_strSyncData = System.Text.Encoding.Unicode.GetString((byte[])(dtbValues.Rows[i]["CONTENT"]));
				p_objSyncInfoArr[i].m_strSyncField = System.Text.Encoding.Unicode.GetString((byte[])(dtbValues.Rows[i]["CONFIG"]));
				m_lngGetTarget_GUIByID(@"select * from customform_target_gui where target_form_id = '"+dtbValues.Rows[i]["TARGET_FORM_ID"].ToString().Trim()+"' order by control_desc",out p_objSyncInfoArr[i].m_objTarget_GUI,true);
			}
			return lngRef;
		}
		/// <summary>
		/// 更新同步数据状态
		/// </summary>
		/// <param name="p_intStatus"></param>
		/// <param name="p_intRecord_ID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateStatus(int p_intStatus,int p_intRecord_ID)
		{
			if(p_intStatus < 0 || p_intRecord_ID<= 0)
				return -1;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSql = @"update CUSTOMFORM_DATA set SYNCSTATUS = '"+p_intStatus.ToString()+"' where record_id = '"+p_intRecord_ID.ToString()+"'";
			long lngRes = objHRPServ.DoExcute(strSql);
            //objHRPServ.Dispose();
            return lngRes;
		}
		/// <summary>
		/// 获取工作流目的表单信息
		/// </summary>
		/// <param name="p_objTargetArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllSource_GUI(out clsCustom_Target_GUI[] p_objTargetArr)
		{
			p_objTargetArr = null;
			string strSql = @"SELECT DISTINCT source_form_id  FROM customform_source_gui";
			string strChildSql = @"select * from customform_source_gui where source_form_id = '<SOURCE_FORM_ID>'";
			return m_lngGetAllGUIInfo(strSql,strChildSql,out p_objTargetArr,false);
		}
		/// <summary>
		/// 获取工作流外部表单信息
		/// </summary>
		/// <param name="p_objTargetArr"></param>
		/// <returns></returns>
		[AutoComplete]
		private long m_lngGetAllGUIInfo(string p_strSql,string p_strChildSql,out clsCustom_Target_GUI[] p_objTargetArr,bool p_blnIsTargetForm)
		{
			p_objTargetArr = null;
			
		clsHRPTableService objHRPServ =new clsHRPTableService();
			DataTable dtbValues = new DataTable();
			long lngRef = objHRPServ.DoGetDataTable(p_strSql,ref dtbValues);
            //objHRPServ.Dispose();
			if(lngRef <=0 || dtbValues.Rows.Count <= 0)
				return 0;
			p_objTargetArr = new clsCustom_Target_GUI[dtbValues.Rows.Count];
			for(int i=0;i<dtbValues.Rows.Count;i++)
			{
				string strSql = p_strChildSql;
				if(p_blnIsTargetForm)
					strSql = strSql.Replace("<TARGET_FORM_ID>",dtbValues.Rows[i]["TARGET_FORM_ID"].ToString().Trim());
				else
					strSql = strSql.Replace("<SOURCE_FORM_ID>",dtbValues.Rows[i]["SOURCE_FORM_ID"].ToString().Trim());
				this.m_lngGetTarget_GUIByID(strSql,out p_objTargetArr[i],p_blnIsTargetForm);
			}
			return lngRef;
		}
		/// <summary>
		/// 获取工作流目的表单信息
		/// </summary>
		/// <param name="p_objTargetArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllTarget_GUI(out clsCustom_Target_GUI[] p_objTargetArr)
		{
			p_objTargetArr = null;
			string strSql = @"SELECT distinct target_form_id FROM customform_target_gui";
			string strChildSql = @"select * from customform_target_gui where target_form_id = '<TARGET_FORM_ID>' order by control_desc";
			return m_lngGetAllGUIInfo(strSql,strChildSql,out p_objTargetArr,true);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strChildSql"></param>
		/// <param name="p_objTarget"></param>
		/// <param name="p_blnIsTargetForm"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTarget_GUIByID(string p_strChildSql,out clsCustom_Target_GUI p_objTarget,bool p_blnIsTargetForm)
		{
			p_objTarget = null;
			if(p_strChildSql == null || p_strChildSql == string.Empty)
				return 0;

		clsHRPTableService objHRPServ =new clsHRPTableService();
			DataTable dtbValues = new DataTable();
			long lngRef = objHRPServ.DoGetDataTable(p_strChildSql,ref dtbValues);
            //objHRPServ.Dispose();
			if(lngRef <=0 || dtbValues.Rows.Count <= 0)
				return 0;
			p_objTarget = new clsCustom_Target_GUI();

			string strFormIDField,strFormDescField,strChildIDField,strChildDescField,strChildTypeField;
			strFormIDField = (p_blnIsTargetForm?"TARGET_FORM_ID":"SOURCE_FORM_ID");
			strFormDescField = (p_blnIsTargetForm?"TARGET_FORM_DESC":"SOURCE_FORM_DESC");
			strChildIDField = (p_blnIsTargetForm?"CONTROL_ID":"ATTRIBUTE_ID");
			strChildDescField = (p_blnIsTargetForm?"CONTROL_DESC":"ATTRIBUTE_DESC");
			strChildTypeField = (p_blnIsTargetForm?"CONTROL_TYPE":"ATTRIBUTE_TYPE");

			p_objTarget.m_strTarget_Form_ID = dtbValues.Rows[0][strFormIDField].ToString().Trim();
			p_objTarget.m_strTarget_Form_Desc = dtbValues.Rows[0][strFormDescField].ToString().Trim();
			p_objTarget.m_objControlArr = new clsCustom_Target_Control[dtbValues.Rows.Count];
			for(int i=0;i<dtbValues.Rows.Count;i++)
			{
				p_objTarget.m_objControlArr[i] = new clsCustom_Target_Control();
				p_objTarget.m_objControlArr[i].m_strControl_ID = dtbValues.Rows[i][strChildIDField].ToString().Trim();
				p_objTarget.m_objControlArr[i].m_strControl_Desc = dtbValues.Rows[i][strChildDescField].ToString().Trim();
				p_objTarget.m_objControlArr[i].m_strControl_Type = dtbValues.Rows[i][strChildTypeField].ToString().Trim();
			}
			return lngRef;
		}
		#endregion

	}
}
