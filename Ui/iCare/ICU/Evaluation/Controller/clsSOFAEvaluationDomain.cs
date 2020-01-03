using System;
using System.Data;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// SOFA评分的商业逻辑层
	/// </summary>
	public class clsSOFAEvaluationDomain
	{	
		/// <summary>
		/// 生成Xml的缓冲
		/// </summary>
		private MemoryStream				m_objXmlMemStream;
		/// <summary>
		/// 生成Xml的工具
		/// </summary>
		private XmlTextWriter				m_objXmlWriter;
		///  <summary>
		/// 读取Xml工具输入参数		
		/// </summary>
		private XmlParserContext			m_objXmlParser;
		public clsSOFAEvaluationDomain()
		{
			//
			// TODO: Add constructor logic here
			//
			m_objXmlMemStream = new MemoryStream(300);

			m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();//清空原来的字符
			m_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);

		}	

		
		//Load All Create Date

		public DateTime [] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID,string p_strInPatientDate,string p_strFromDate, string p_strToDate)
		{
			if(p_strInPatientID ==null||p_strInPatientID =="")
				return null;

			DateTime[] dtmCreateRecordDateArr = null;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("sofaevaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

			if(lngRes > 0 && intRows >0)
			{
				dtmCreateRecordDateArr = new DateTime [intRows];

				XmlTextReader objReader = new XmlTextReader(strXml,XmlNodeType.Element,m_objXmlParser);
				objReader.WhitespaceHandling = WhitespaceHandling.None;
			
				int intIndex = 0;
				while(objReader.Read())
				{
					switch (objReader.NodeType)
					{
						case XmlNodeType.Element:
							if(objReader.HasAttributes)
							{
								dtmCreateRecordDateArr[intIndex]=DateTime.Parse (objReader.GetAttribute("ACTIVITYTIME"));
								intIndex++;
							}
							break;
					}
				}
			}
			return dtmCreateRecordDateArr ;
		}
		//END  Load All Create Date

		// Save********
		public long		m_lngSave(clsEvalInfoOfSOFAEvaluation p_objSOFAEvaluationDB)
		{
			if(p_objSOFAEvaluationDB == null)
				return -1;

			string strMainXml = m_mthGetMainXml(p_objSOFAEvaluationDB);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("sofaevaluation", strMainXml);
		}

		private string	m_mthGetMainXml(clsEvalInfoOfSOFAEvaluation p_objSOFAEvaluationDB)
		{
			if(p_objSOFAEvaluationDB == null)
				return null;

			m_objXmlMemStream.SetLength(0);
			
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("RECORDMASTER");

            m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objSOFAEvaluationDB.strPatientID.Replace('\'', 'き').Trim());
			m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objSOFAEvaluationDB.strInPatientDate.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objSOFAEvaluationDB.strActivityTime.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objSOFAEvaluationDB.strEvalDoctorID.Replace('\'','き'));

			m_objXmlWriter.WriteAttributeString("PA02", p_objSOFAEvaluationDB.strPa02.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("FI02", p_objSOFAEvaluationDB.strFi02.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("XJG", p_objSOFAEvaluationDB.strXJG.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("DHS", p_objSOFAEvaluationDB.strDHS.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("DXY", p_objSOFAEvaluationDB.strDXY.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("XXB", p_objSOFAEvaluationDB.strXXB.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("OPENEYES", p_objSOFAEvaluationDB.strOpenEyes.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("SAY", p_objSOFAEvaluationDB.strSay.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("SPORT", p_objSOFAEvaluationDB.strSport.Replace('\'','き'));

			m_objXmlWriter.WriteAttributeString("BREATHEVAL", p_objSOFAEvaluationDB.strBreathEval.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("XJGEVAL", p_objSOFAEvaluationDB.strXJGEval.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("DHSEVAL", p_objSOFAEvaluationDB.strDHSEval.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("XXGEVAL", p_objSOFAEvaluationDB.strXXGEval.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("BLOODEVAL", p_objSOFAEvaluationDB.strBloodEval.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("NERVEEVAL", p_objSOFAEvaluationDB.strNerveEval.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("TOTALEVAL", p_objSOFAEvaluationDB.strTotalEval.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("STATUS", "0");

			m_objXmlWriter.WriteEndElement();          
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();			
			return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(),39*2,(int)m_objXmlMemStream.Length-39*2);
		}
		//END Save

		//读取信息**********
		public long m_lngGetSOFAValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsEvalInfoOfSOFAEvaluation p_objSOFAEvaluation)
		{
			p_objSOFAEvaluation = null;

			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -2;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("sofaevaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

			if(lngRes <= 0)
				return -1;

			if(intRows <= 0)
				return 0;

			XmlTextReader objReader = new XmlTextReader(strXml,XmlNodeType.Element,m_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;

			p_objSOFAEvaluation = new clsEvalInfoOfSOFAEvaluation();

			while(objReader.Read())
			{
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element:
						if(objReader.HasAttributes)
						{
							p_objSOFAEvaluation.strPatientID		= objReader.GetAttribute("INPATIENTNO").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strInPatientDate	= objReader.GetAttribute("INPATIENTDATE").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strActivityTime		= objReader.GetAttribute("ACTIVITYTIME").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strModifyDate		= objReader.GetAttribute("MODIFYDATE").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strEvalDoctorID		= objReader.GetAttribute("EVALDOCTORID").ToString().Replace ('き','\'');
							
							p_objSOFAEvaluation.strPa02				= objReader.GetAttribute("PA02").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strFi02				= objReader.GetAttribute("FI02").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strXJG				= objReader.GetAttribute("XJG").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strDHS				= objReader.GetAttribute("DHS").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strDXY				= objReader.GetAttribute("DXY").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strXXB				= objReader.GetAttribute("XXB").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strOpenEyes			= objReader.GetAttribute("OPENEYES").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strSay				= objReader.GetAttribute("SAY").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strSport			= objReader.GetAttribute("SPORT").ToString().Replace ('き','\'');

							p_objSOFAEvaluation.strBreathEval = objReader.GetAttribute("BREATHEVAL").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strXJGEval = objReader.GetAttribute("XJGEVAL").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strDHSEval = objReader.GetAttribute("DHSEVAL").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strXXGEval = objReader.GetAttribute("XXGEVAL").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strBloodEval = objReader.GetAttribute("BLOODEVAL").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strNerveEval = objReader.GetAttribute("NERVEEVAL").ToString().Replace ('き','\'');
							p_objSOFAEvaluation.strTotalEval		= objReader.GetAttribute("TOTALEVAL").ToString().Replace ('き','\'');
						}
						break;
				}
			}
			return 1;
		}
		//END 读取信息

		//Delete  ************
		public long m_lngDeactive(string p_strDeactiveUserID,string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate)
		{
			string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='"+p_strDeactiveUserID+"' InPatientNO='"+p_strInPatientID +"'"+" InPatientDate='"+p_strInPatientDate+ "'" +" ActivityTime='"+p_strCreateDate+"'" +" />";
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("sofaevaluation", strDeactiveXML);
			
			if(lngRes <= 0)
				return -1;

			return 1;
		}
		//End Delete
		

	}


	public class clsEvalInfoOfSOFAEvaluation
	{
		public string strPatientID;
		public string strInPatientDate;
		public string strModifyDate;
		public string strActivityTime;

		public string strEvalDoctorID;
		public string strEvalDoctorName;

		public string strPa02;//应为小写字母o而不是数字0
		public string strFi02;//应为小写字母o而不是数字0
		public string strXJG;//血肌酐
		public string strDHS;//胆红素
		public string strDXY;//低血压
		public string strXXB;//血小板
		public string strOpenEyes;
		public string strSay;
		public string strSport;

		public string strBreathEval;
		public string strXJGEval;
		public string strDHSEval;
		public string strXXGEval;
		public string strBloodEval;
		public string strNerveEval;
		public string strTotalEval;
	}

}