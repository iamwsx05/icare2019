using System;
using System.Data;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// MortalityRate评分的商业逻辑层
	/// </summary>
	public class clsMortalityRateEvaluationDomain
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
		
		public clsMortalityRateEvaluationDomain()
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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("mortalityrateevaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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
		public long		m_lngSave(clsEvalInfoOfMortalityRateEvaluation p_objMortalityRateEvaluationDB)
		{
			if(p_objMortalityRateEvaluationDB == null)
				return -1;

			string strMainXml = m_mthGetMainXml(p_objMortalityRateEvaluationDB);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
			return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("mortalityrateevaluation",strMainXml);
		}

		private string	m_mthGetMainXml(clsEvalInfoOfMortalityRateEvaluation p_objMortalityRateEvaluationDB)
		{
			if(p_objMortalityRateEvaluationDB == null)
				return null;

			m_objXmlMemStream.SetLength(0);
			
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("RECORDMASTER");

            m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objMortalityRateEvaluationDB.strPatientID.Replace('\'', 'き').Trim());
			m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objMortalityRateEvaluationDB.strInPatientDate.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objMortalityRateEvaluationDB.strActivityTime.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objMortalityRateEvaluationDB.strEvalDoctorID.Replace('\'','き'));

			m_objXmlWriter.WriteAttributeString("HM", p_objMortalityRateEvaluationDB.strHM.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("JZ", p_objMortalityRateEvaluationDB.strJZ.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("CANCER", p_objMortalityRateEvaluationDB.strCancer.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("INFECT", p_objMortalityRateEvaluationDB.strInfect.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("SJ", p_objMortalityRateEvaluationDB.strSJ.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("SBP", p_objMortalityRateEvaluationDB.strSBP.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("SBP2", p_objMortalityRateEvaluationDB.strSBP2.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("MORTALITY", p_objMortalityRateEvaluationDB.strMortality.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("STATUS", "0");

			m_objXmlWriter.WriteEndElement();          
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();			
			return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(),39*2,(int)m_objXmlMemStream.Length-39*2);
		}
		//END Save

		//读取信息**********
		public long m_lngGetMortalityRateValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsEvalInfoOfMortalityRateEvaluation p_objMortalityRateEvaluation)
		{
			p_objMortalityRateEvaluation = null;

			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -2;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("mortalityrateevaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

			if(lngRes <= 0)
				return -1;

			if(intRows <= 0)
				return 0;

			XmlTextReader objReader = new XmlTextReader(strXml,XmlNodeType.Element,m_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;

			p_objMortalityRateEvaluation = new clsEvalInfoOfMortalityRateEvaluation();

			while(objReader.Read())
			{
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element:
						if(objReader.HasAttributes)
						{
							p_objMortalityRateEvaluation.strPatientID			= objReader.GetAttribute("INPATIENTNO").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strInPatientDate		= objReader.GetAttribute("INPATIENTDATE").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strActivityTime		= objReader.GetAttribute("ACTIVITYTIME").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strModifyDate			= objReader.GetAttribute("MODIFYDATE").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strEvalDoctorID		= objReader.GetAttribute("EVALDOCTORID").ToString().Replace ('き','\'');
							
							p_objMortalityRateEvaluation.strHM				= objReader.GetAttribute("HM").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strJZ				= objReader.GetAttribute("JZ").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strCancer			= objReader.GetAttribute("CANCER").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strSJ				= objReader.GetAttribute("SJ").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strInfect			= objReader.GetAttribute("INFECT").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strSBP				= objReader.GetAttribute("SBP").ToString().Replace ('き','\'');
							p_objMortalityRateEvaluation.strSBP2			= objReader.GetAttribute("SBP2").ToString().Replace ('き','\'');

							p_objMortalityRateEvaluation.strMortality		= objReader.GetAttribute("MORTALITY").ToString().Replace ('き','\'');
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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("mortalityrateevaluation", strDeactiveXML);			

			if(lngRes <= 0)
				return -1;

			return 1;
		}
		//End Delete
		

	}


	public class clsEvalInfoOfMortalityRateEvaluation
	{
		public string strPatientID;
		public string strInPatientDate;
		public string strModifyDate;
		public string strActivityTime;

		public string strEvalDoctorID;
		public string strEvalDoctorName;

		public string strHM;
		public string strJZ;
		public string strCancer;
		public string strInfect;
		public string strSJ;
		public string strSBP;
		public string strSBP2;

		public string strMortality;
	}

}