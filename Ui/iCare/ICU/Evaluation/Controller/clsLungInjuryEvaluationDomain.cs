using System;
using com.digitalwave.Utility;
using System.Xml;
using System.Data;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// Summary description for clsLungInjuryEvaluationDomain.
	/// </summary>
	public class clsLungInjuryEvaluationDomain
	{		
		/// <summary>
		/// 生成Xml的缓冲
		/// </summary>
		private MemoryStream m_objXmlMemStream;
		/// <summary>
		/// 生成Xml的工具
		/// </summary>
		private XmlTextWriter m_objXmlWriter;
		///  <summary>
		/// 读取Xml工具输入参数		
		/// </summary>
		private XmlParserContext m_objXmlParser;

		public clsLungInjuryEvaluationDomain()
		{          
			m_objXmlMemStream = new MemoryStream(300);

			m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();//清空原来的字符
			m_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);

		}	
		#region new
		#region Load All Create Date
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strFromDate"></param>
		/// <param name="p_strToDate"></param>
		/// <returns></returns>
		public DateTime [] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID,string p_strInPatientDate,string p_strFromDate, string p_strToDate)
		{
			if(p_strInPatientID ==null||p_strInPatientID =="")
				return null;

			DateTime[] dtmCreateRecordDateArr = null;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("lunginjuryevalucation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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
		#endregion

		#region Save
		public long m_lngSave(clsEvalInfoOfclsLungInjuryEvaluation p_objLungInjuryEvaluation)
		{
			if(p_objLungInjuryEvaluation == null)
				return -1;

			string strMainXml = m_mthGetMainXml(p_objLungInjuryEvaluation);

            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("lunginjuryevalucation", strMainXml);
		}

		private string m_mthGetMainXml(clsEvalInfoOfclsLungInjuryEvaluation p_objLungInjuryEvaluation)
		{
			if(p_objLungInjuryEvaluation == null)
				return null;

			m_objXmlMemStream.SetLength(0);
			
			try
			{
				m_objXmlWriter.WriteStartDocument();
				m_objXmlWriter.WriteStartElement("RECORDMASTER");

                m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objLungInjuryEvaluation.strPatientID.Replace('\'', 'き').Trim());
				m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objLungInjuryEvaluation.strInPatientDate.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objLungInjuryEvaluation.strActivityTime.Replace('\'','き'));

				m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objLungInjuryEvaluation.strEvalDoctorID.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("LUNGXRAY", p_objLungInjuryEvaluation.strLungXRay.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("PAO2", p_objLungInjuryEvaluation.strPao2.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("FIO2", p_objLungInjuryEvaluation.strFio2.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("PAO2ANDFIO2SEL", p_objLungInjuryEvaluation.strPao2AndFio2Sel.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("LOWOXYGENBLOOD", p_objLungInjuryEvaluation.strLowOxygenBlood.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("PEEP", p_objLungInjuryEvaluation.strPEEP.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("VT", p_objLungInjuryEvaluation.strVt.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("PIP", p_objLungInjuryEvaluation.strPIP.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("LUNGSYSHUMOR", p_objLungInjuryEvaluation.strLungSysHumor.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("LUNGXRAYEVAL", p_objLungInjuryEvaluation.strLungXRayEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("LOWOXYGENBLOODEVAL", p_objLungInjuryEvaluation.strLowOxygenBloodEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("LUNGSYSHUMOREVAL", p_objLungInjuryEvaluation.strLungSysHumorEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("PEEPEVAL", p_objLungInjuryEvaluation.strPEEPEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("TOTALEVAL", p_objLungInjuryEvaluation.strTotalEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("STATUS", "0");

				m_objXmlWriter.WriteEndElement();          
				m_objXmlWriter.WriteEndDocument();
				m_objXmlWriter.Flush();			
			
				return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(),39*2,(int)m_objXmlMemStream.Length-39*2);
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				return null;
			}
		}
		#endregion

		#region Delete
		public long m_lngDeactive(string p_strDeactiveUserID,string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate)
		{
			string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='"+p_strDeactiveUserID+"' InPatientNO='"+p_strInPatientID +"'"+" InPatientDate='"+p_strInPatientDate+ "'" +" ActivityTime='"+p_strCreateDate+"'" +" />";
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("lunginjuryevalucation", strDeactiveXML);
			
			if(lngRes <= 0)
				return -1;

			return 1;
		}
		#endregion

		#region 读取信息
		public long m_lngGetLungInjuryValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsEvalInfoOfclsLungInjuryEvaluation p_objLungInjuryEvaluation)
		{
			p_objLungInjuryEvaluation = null;

			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -2;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("lunginjuryevalucation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

			if(lngRes <= 0)
				return -1;

			if(intRows <= 0)
				return 0;

			XmlTextReader objReader = new XmlTextReader(strXml,XmlNodeType.Element,m_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;

			p_objLungInjuryEvaluation = new clsEvalInfoOfclsLungInjuryEvaluation();
			
			while(objReader.Read())
			{
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element:
						if(objReader.HasAttributes)
						{
							p_objLungInjuryEvaluation.strPatientID= objReader.GetAttribute("INPATIENTNO").ToString().Replace ('き','\'');
							p_objLungInjuryEvaluation.strActivityTime= objReader.GetAttribute("ACTIVITYTIME").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strInPatientDate= objReader.GetAttribute("INPATIENTDATE").ToString().Replace ('き','\'');
							p_objLungInjuryEvaluation.strModifyDate= objReader.GetAttribute("MODIFYDATE").ToString().Replace ('き','\'');

							p_objLungInjuryEvaluation.strEvalDoctorID= objReader.GetAttribute("EVALDOCTORID").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strLungXRay= objReader.GetAttribute("LUNGXRAY").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strPao2= objReader.GetAttribute("PAO2").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strFio2= objReader.GetAttribute("FIO2").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strPao2AndFio2Sel= objReader.GetAttribute("PAO2ANDFIO2SEL").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strLowOxygenBlood= objReader.GetAttribute("LOWOXYGENBLOOD").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strPEEP= objReader.GetAttribute("PEEP").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strVt= objReader.GetAttribute("VT").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strPIP= objReader.GetAttribute("PIP").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strLungSysHumor= objReader.GetAttribute("LUNGSYSHUMOR").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strLungXRayEval= objReader.GetAttribute("LUNGXRAYEVAL").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strLowOxygenBloodEval= objReader.GetAttribute("LOWOXYGENBLOODEVAL").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strPEEPEval= objReader.GetAttribute("PEEPEVAL").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strLungSysHumorEval= objReader.GetAttribute("LUNGSYSHUMOREVAL").ToString().Replace ('き','\'') ;
							p_objLungInjuryEvaluation.strTotalEval= objReader.GetAttribute("TOTALEVAL").ToString().Replace ('き','\'');

						}
						break;
				}
			}
			return 1;
		}
		#endregion
		#endregion
	}
	public class clsEvalInfoOfclsLungInjuryEvaluation
	{
		public string strPatientID;
		public string strInPatientDate;
		public string strActivityTime;
		public string strModifyDate;

		public string strEvalDoctorID;
		public string strEvalDoctorName;
		public string strLungXRay;
		public string strPao2;
		public string strFio2;
		public string strPao2AndFio2Sel;
		public string strLowOxygenBlood;
		public string strPEEP;
		public string strVt;
		public string strPIP;
		public string strLungSysHumor;
		public string strLungXRayEval;
		public string strLowOxygenBloodEval;
		public string strPEEPEval;
		public string strLungSysHumorEval;
		public string strTotalEval;
	}
}
