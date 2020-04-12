using System;
using System.Data;
using weCare.Core.Entity;
using System.Collections;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;

namespace iCare.CustomFromService
{
	/// <summary>
	/// 最小元素集中间件 creat by gphuang 2004-11-25
    /// 优化by tfzhang 2006-03-07
    /// 1、去除rtrim、ltrim函数
    /// 2、使用参数传值
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsMinElementColServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		#region 保存值
		[AutoComplete]
		public long m_lngSaveTemplateData(clsTextTemplate p_objTextTemplate)
		{
			if(p_objTextTemplate==null) return 0;
			if(p_objTextTemplate.m_objTmpCtlValueArr == null || p_objTextTemplate.m_objTmpCtlValueArr.Length <= 0)
				return 0;
			long lngRes;
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            try
            {
                //Delete Old
                string strSql = @"delete min_elementcol_value where gui_id=? and inpatientid=? and inpatientdate=?";
                
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objTextTemplate.m_strGUI_ID;
                objDPArr[1].Value = p_objTextTemplate.m_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value =p_objTextTemplate.m_dtInPatientDate;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);


                string strSql2 = @"insert into min_elementcol_value(gui_id,control_id,control_value,inpatientid,inpatientdate,opendate)
								values(?',?,?,?,?,?)";
                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr2);
                objDPArr2[0].Value = p_objTextTemplate.m_strGUI_ID;
                objDPArr2[3].Value = p_objTextTemplate.m_strInPatientID;
                objDPArr2[4].DbType = DbType.DateTime;
                objDPArr2[4].Value = p_objTextTemplate.m_dtInPatientDate;
                objDPArr2[5].DbType = DbType.DateTime;
                objDPArr2[5].Value = DateTime.Now;
                
                //strSql2 = strSql2.Replace("[PatID]", p_objTextTemplate.m_strInPatientID);
                //strSql2 = strSql2.Replace("[PatDate]", clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objTextTemplate.m_dtInPatientDate));
                //strSql2 = strSql2.Replace("[OpenDate]", clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(DateTime.Now));
                //strSql2 = strSql2.Replace("[TempID]", p_objTextTemplate.m_strGUI_ID);

                for (int i = 0; i < p_objTextTemplate.m_objTmpCtlValueArr.Length; i++)
                {
                    objDPArr2[1].Value = p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_ID;
                    objDPArr2[2].Value = p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_VALUE;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSql2, ref lngAff, objDPArr2);
                }

            }
            finally
            {
                //objHRPServ.Dispose();
            }
			return 1;
		}
		/// <summary>
		/// 保存主表信息
		/// </summary>
		/// <param name="p_objTextTemplate"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveApplyInfo(clsTextTemplate p_objTextTemplate)
		{
			if(p_objTextTemplate==null) 
				return 0;
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
             try
            {
			string strSql = @"delete from  min_element_apply where gui_id=? and ?form_id=? and control_id=?";
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_objTextTemplate.m_strGUI_ID;
            objDPArr[1].Value = p_objTextTemplate.m_strFORM_ID;
            objDPArr[2].Value =p_objTextTemplate.m_strCONTROL_ID;
            long lngAff = 0;
            lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr);

            strSql = @"insert into min_element_apply(form_id,control_id,gui_id,status,doctor_id)
								values(?,?,?,?,?)";
            IDataParameter[] objDPArr2 = null;
            objHRPServ.CreateDatabaseParameter(5, out objDPArr2);
            objDPArr2[0].Value = p_objTextTemplate.m_strFORM_ID;
            objDPArr2[1].Value = p_objTextTemplate.m_strCONTROL_ID;
            objDPArr2[2].Value = p_objTextTemplate.m_strGUI_ID;
            objDPArr2[3].Value = 0;
            objDPArr2[4].Value = p_objTextTemplate.m_strDoctor_ID;
            lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngAff, objDPArr2);
             }
            finally
            {
                //objHRPServ.Dispose();

            }

			return lngRes;
		}

		#endregion

		#region 保存模板信息
		[AutoComplete]
		public long m_lngSaveTemplate(clsTemplateInfo objTIVO,out string strID)
		{
			strID="";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.lngGenerateID(7, "TEMPLATE_ID", "MIN_ELEMENTCOL_GUI", out strID);
                if (lngRes > 0)
                {
                    string strSQL = "insert into min_elementcol_gui(template_id,template_name,template_xml)values(?,?,?) ";

                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = strID;
                    objDPArr[1].Value = objTIVO.m_strTEMPLATE_NAME;
                    objDPArr[2].Value = System.Text.Encoding.Unicode.GetBytes(objTIVO.m_strTEMPLATE_XML);
                    long lngAff = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr);
                } 
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}
		#endregion

		#region 更新模板
		[AutoComplete]
		public long m_lngUpdateTemplate(clsTemplateInfo objTIVO)
		{
            long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSQL="update min_elementcol_gui set template_name =?,template_xml =? where template_id =?";

			
            try
            {
                IDataParameter[] objDPArr=null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = objTIVO.m_strTEMPLATE_NAME;
                objDPArr[1].Value = System.Text.Encoding.Unicode.GetBytes(objTIVO.m_strTEMPLATE_XML);
                objDPArr[2].Value = objTIVO.m_strTEMPLATE_ID;
                long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr); 
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}
		#endregion

		#region 停用模板 
		[AutoComplete]
		public long m_lngHaltTemplate(string p_strTemplate_ID)
		{
            long lngRes = 0;
						
			clsHRPTableService objHRPServ =new clsHRPTableService();
			string strSQL="update min_element_apply set status = 1 where gui_id = ?";
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strTemplate_ID;
                 long lngAff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAff, objDPArr); 
             }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}
		#endregion


		#region 根据窗口ID，控件ID查找模板。
		/// <summary>
		/// 获取指定控件得相关模板
		/// </summary>
		/// <param name="strFormID"></param>
		/// <param name="strControlID"></param>
		/// <param name="arrTemplate"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTemplates(string strFormID,string strControlID,out clsTemplateInfo[] arrTemplate)
		{
			arrTemplate=null;
			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
            string strSQL = @"select min_elementcol_gui.template_id,
       min_elementcol_gui.template_name,
       min_elementcol_gui.template_xml,
       min_elementcol_gui.visblelevel,
       min_element_apply.doctor_id
  from min_element_apply
 inner join min_elementcol_gui on min_element_apply.gui_id =
                                  min_elementcol_gui.template_id
 where min_element_apply.form_id = ?
   and min_element_apply.control_id = ?
   and min_element_apply.status = ?";
	
			DataTable dt =new DataTable();

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = strFormID;
                objDPArr[1].Value = strControlID;
                objDPArr[2].Value = 0;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr); 

                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    arrTemplate = new clsTemplateInfo[dt.Rows.Count];
                    for (int i = 0; i < arrTemplate.Length; i++)
                    {
                        arrTemplate[i] = new clsTemplateInfo();
                        arrTemplate[i].m_strTEMPLATE_ID = dt.Rows[i]["TEMPLATE_ID"].ToString().Trim();
                        arrTemplate[i].m_strTEMPLATE_NAME = dt.Rows[i]["TEMPLATE_NAME"].ToString().Trim();
                        arrTemplate[i].m_strTEMPLATE_XML = System.Text.Encoding.Unicode.GetString((byte[])(dt.Rows[i]["TEMPLATE_XML"]));
                        arrTemplate[i].m_strDoctor_ID = dt.Rows[i]["DOCTOR_ID"].ToString().Trim();
                    }
                } 
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return lngRes;
		}


		/// <summary>
		///  获取模板有描述的控件
		/// </summary>
		/// <param name="strTemplateID"></param>
		/// <param name="arrItems"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTemplateControls(string strTemplateID,out clsTemplateControlValue[] arrItems)
		{
			arrItems=null;
            string strSQL = @"select control_id,control_desc  from min_element_desc where template_id=?";

			DataTable dt=new DataTable();
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strTemplateID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr); 
 			    if(lngRes>0 && dt!=null)
			    {
				    arrItems=new clsTemplateControlValue[dt.Rows.Count];
				    for(int i=0;i<arrItems.Length;i++)
				    {
					    arrItems[i]=new clsTemplateControlValue();
					    arrItems[i].m_strGUI_ID=strTemplateID;
					    arrItems[i].m_strCONTROL_ID= ToStr(dt.Rows[i]["CONTROL_ID"]).Trim();
					    arrItems[i].m_strCONTROL_VALUE="";
					    arrItems[i].m_strCONTROL_DESC= ToStr(dt.Rows[i]["CONTROL_DESC"]).Trim();
				    }
			    } 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}

		/// <summary>
		/// 保存控件描述
		/// </summary>
		/// <param name="p_objTextTemplate"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveTemplateDesc(clsTextTemplate p_objTextTemplate)
		{
			if(p_objTextTemplate == null)
				return 0;
			if(p_objTextTemplate.m_strGUI_ID == null || p_objTextTemplate.m_objTmpCtlValueArr == null)
				return 0;
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
			long lngRes = 0;
			string strDelete = @"delete from min_element_desc where template_id = ?";
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objTextTemplate.m_strGUI_ID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strDelete, ref lngEff,objDPArr); 
			    string strSql = @"insert into min_element_desc (template_id, control_id, control_desc) values (?, ?, ? )";
			    for(int i=0;i<p_objTextTemplate.m_objTmpCtlValueArr.Length;i++)
			    {
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_objTextTemplate.m_strGUI_ID;
                    objDPArr[1].Value = p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_ID;
                    objDPArr[2].Value = p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_DESC;
                    //string str = strSql.Replace("[CTOL_ID]",p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_ID);
                    //str = str.Replace("[CTOL_DESC]",p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_DESC);
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
			    }  
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			return 1;
		}


		#endregion

		#region  temp
		[AutoComplete]
		public long m_lngFindPatientTemplateText(string[] arrGuiID,string[] arrCtrlName,string[] arrCtrlValue,out clsTextTemplate [] arrValue)
		{
			arrValue=new clsTextTemplate[0];
			if(arrGuiID==null || arrGuiID.Length<=0) return 1;
			
			string strSql=strGetSql(arrGuiID[0],arrCtrlName[0],arrCtrlValue[0],0);
			for(int i=1;i<arrGuiID.Length;i++)
			{
				//				strSql=strSql + " intersect " + strGetSql(arrGuiID[i],arrCtrlName[i],arrCtrlValue[i]);
				string strIndexF = "i"+Convert.ToString(i-1);
				string strIndexN = "i"+i.ToString();
				strSql=strSql + " and exists ( " + strGetSql(arrGuiID[i],arrCtrlName[i],arrCtrlValue[i],i) + " and "+strIndexF
					+"gui_id = "+strIndexN+".gui_id and "+strIndexF+".inpatientid = "+strIndexN+".inpatientid and "+strIndexF+".inpatientdate = "+strIndexN+".inpatientdate)";
			}

			DataTable objDT=new DataTable();
            long lngRes = 0;
           clsHRPTableService objHRPServ = new clsHRPTableService();
		    try
            {
                lngRes = objHRPServ.DoGetDataTable(strSql, ref objDT);
                if ((lngRes > 0) && (objDT != null))
                {
                    arrValue = new clsTextTemplate[objDT.Rows.Count];
                    for (int i = 0; i < arrValue.Length; i++)
                    {
                        arrValue[i] = new clsTextTemplate();
                        arrValue[i].m_strInPatientID = ToStr(objDT.Rows[i]["INPATIENTID"]);
                        arrValue[i].m_dtInPatientDate = ToDT(objDT.Rows[i]["INPATIENTDATE"]);
                        arrValue[i].m_dtOpenDate = ToDT(objDT.Rows[i]["OPENDATE"]);
                    }
                    lngRes=1;
                }
                else
                {
                    arrValue = null;
                   lngRes= 0;
                }
            }
	        finally
	        {
	          //objHRPServ.Dispose();

	        }
            return lngRes;
        
		}

		private string strGetSql(string strGuiID,string strCtrlName,string strCtrlValue,int p_intIndex)
		{
			string strIndex = "i"+p_intIndex.ToString();
			string strSql=" select "+strIndex+".InPatientID,"+strIndex+".InPatientDate,"+strIndex+".OpenDate from Min_ElementCol_Value "+strIndex
				+@" where  "+strIndex+".GUI_ID ='[GUIID]' and  "+strIndex+".CONTROL_ID ='[CTRLNAME]'  and "+strIndex+".CONTROL_VALUE like '%[CTRLVALUE]%' ";
			strSql=strSql.Replace("[GUIID]",strGuiID);
			strSql=strSql.Replace("[CTRLNAME]",strCtrlName);
			strSql=strSql.Replace("[CTRLVALUE]",strCtrlValue);
			return strSql;
		}


		private string ToStr(object objValue)
		{
			try
			{
				if(objValue==null)
					return "";
				else
					return Convert.ToString(objValue);
			}
			catch(Exception)
			{
				return "";
			}
		}

		private DateTime ToDT(object objValue)
		{
			try
			{
				return Convert.ToDateTime(objValue);
			}
			catch(Exception)
			{
				return DateTime.Parse("1900-1-1");
			}
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objTextTemplate"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetValue(ref clsTextTemplate p_objTextTemplate)
		{
			if(p_objTextTemplate.m_strInPatientID == null || p_objTextTemplate.m_strGUI_ID == null)
				return 0;


            string strSql = @"select a.gui_id,
       a.control_value,
       a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.control_id,
       b.control_desc
  from min_elementcol_value a
 inner join min_element_desc b on a.gui_id = b.template_id
                              and a.control_id = b.control_id
 where a.gui_id = ?
   and inpatientid = ?
   and a.inpatientdate = ?";


			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
		        DataTable dtValue=new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objTextTemplate.m_strGUI_ID.Trim();
                objDPArr[1].Value = p_objTextTemplate.m_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.Time;
                objDPArr[2].Value = p_objTextTemplate.m_dtInPatientDate;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtValue, objDPArr); 
			    if(lngRes>0 && dtValue.Rows.Count > 0)
			    {
				    p_objTextTemplate.m_objTmpCtlValueArr = new clsTemplateControlValue[dtValue.Rows.Count];
				    p_objTextTemplate.m_dtOpenDate = DateTime.Parse(dtValue.Rows[0]["OPENDATE"].ToString().Trim());
				    for(int i=0;i<dtValue.Rows.Count;i++)
				    {
					    p_objTextTemplate.m_objTmpCtlValueArr[i] = new clsTemplateControlValue();
					    p_objTextTemplate.m_objTmpCtlValueArr[i].m_strGUI_ID = dtValue.Rows[i]["GUI_ID"].ToString().Trim();
					    p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_ID = dtValue.Rows[i]["CONTROL_ID"].ToString().Trim();
					    p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_VALUE = dtValue.Rows[i]["CONTROL_VALUE"].ToString().Trim();
					    p_objTextTemplate.m_objTmpCtlValueArr[i].m_strCONTROL_DESC = dtValue.Rows[i]["CONTROL_DESC"].ToString().Trim();
				    }
			    } 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
		/// <summary>
		/// 获取指定控件的相关模板，不包含模板的XML信息
		/// </summary>
		/// <param name="strFormID"></param>
		/// <param name="strControlID"></param>
		/// <param name="arrTemplate"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTemplateName(string strFormID,string strControlID,out clsTemplateInfo[] arrTemplate)
		{
			arrTemplate=null;

 			string strSQL=@"select mg.template_id,mg.template_name 
                        from min_element_apply ma inner join min_elementcol_gui mg
                        on ma.gui_id = mg.template_id where ma.form_id = ?
                        and ma.control_id = ? and ma.status =?";
	
			DataTable dt =new DataTable();

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtValue = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = strFormID;
                objDPArr[1].Value = strControlID;
                objDPArr[2].Value = 0;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr); 
                    if (lngRes > 0 && dt.Rows.Count > 0)
                    {
                        arrTemplate = new clsTemplateInfo[dt.Rows.Count];
                        for (int i = 0; i < arrTemplate.Length; i++)
                        {
                            arrTemplate[i] = new clsTemplateInfo();
                            arrTemplate[i].m_strTEMPLATE_ID = dt.Rows[i]["TEMPLATE_ID"].ToString().Trim();
                            arrTemplate[i].m_strTEMPLATE_NAME = dt.Rows[i]["TEMPLATE_NAME"].ToString().Trim();
                        }
                    } 
            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
		}
	}
}
