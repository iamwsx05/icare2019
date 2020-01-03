using System;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;

namespace iCare.ICU.Evaluation
{
	public class APACHEIIIValuationDB
	{
		public string strInHospitalNO;
		public string strActivityTime;
		public string strInPatientDate;
		public string strModifyDate;

		public string strAgeGroup;
		public string strEvalDoctorID;
		public string strOperSel;
		public string strOpenEyeSel;
		public string strLanguageSel;
		public string strHR;
		public string strAdvArteryPress;
		public string strTemperature;
		public string strBreath;
		public string strPao2;
		public string strDo2;
		public string strBloodCorpuscle;
		public string strAmountLeucocyte;
		public string strBloodFlesh;
		public string strHypercholesterolemia;
		public string strPH;
		public string strPCO2;
		public string strUrineAmount;
		public string strHematuria;
		public string strBloodNa;
		public string strProteid;
		public string strBloodGallbladder;
		public string strFiO2;
		public string strMachineAerateChk;
		public string strKidneyWaneChk;
		public string strAIDSChk;
		public string strLiverWaneChk;
		public string strLimphomaChk;
		public string strMetastaticTumorChk;
		public string strLeukaemiaChk;
		public string strImmunityChk;
		public string strHepatocirrhosisChk;
		public string strAccordingChk;
		public string strPositionAcheChk;
		public string strBodyBendAndVerticalChk;
		public string strBrainUnreactionChk;
		public string strAspVal;
		public string strPHAndPCO2Val;
		public string strAgeAndHealthVal;
		public string strNeuroneVal;
		public string strTotalVal;
		public string strPaCO2;

	}
	/// <summary>
	/// Summary description for APACHEIIIValuationDomain.
	/// </summary>
	public class APACHEIIIValuationDomain
	{
		#region Member		
		/// <summary>
		/// 伏撹Xml議産喝
		/// </summary>
		private MemoryStream m_objXmlMemStream;
		/// <summary>
		/// 伏撹Xml議垢醤
		/// </summary>
		private XmlTextWriter m_objXmlWriter;
		///  <summary>
		/// 響函Xml垢醤補秘歌方		
		/// </summary>
		private XmlParserContext m_objXmlParser;
		#endregion

		#region Constructor
		public APACHEIIIValuationDomain()
		{

			//
			// TODO: Add constructor logic here
			//                       

			m_objXmlMemStream = new MemoryStream(300);

			m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();//賠腎圻栖議忖憲
			m_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);

		}
		#endregion
		
		#region New

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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("apacheiiivaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objAPACHEIIValuationDB"></param>
		/// <returns></returns>
		public long m_lngSave(APACHEIIIValuationDB p_objAPACHEIIIValuationDB)
		{
			if(p_objAPACHEIIIValuationDB == null)
				return -1;

			string strMainXml = m_mthGetMainXml(p_objAPACHEIIIValuationDB);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("apacheiiivaluation", strMainXml);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objAPACHEIIValuationDB"></param>
		/// <returns></returns>
		private string m_mthGetMainXml(APACHEIIIValuationDB p_objAPACHEIIIValuationDB)
		{
			if(p_objAPACHEIIIValuationDB == null)
				return null;

			m_objXmlMemStream.SetLength(0);
			
			m_objXmlWriter.WriteStartDocument();
			m_objXmlWriter.WriteStartElement("RECORDMASTER");

            m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objAPACHEIIIValuationDB.strInHospitalNO.Replace('\'', 'き').Trim());
			m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objAPACHEIIIValuationDB.strInPatientDate.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objAPACHEIIIValuationDB.strActivityTime.Replace('\'','き'));

			m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objAPACHEIIIValuationDB.strEvalDoctorID.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("AGEGROUP", p_objAPACHEIIIValuationDB.strAgeGroup.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("OPERSEL", p_objAPACHEIIIValuationDB.strOperSel.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("OPENEYESEL", p_objAPACHEIIIValuationDB.strOpenEyeSel.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("LANGUAGESEL", p_objAPACHEIIIValuationDB.strLanguageSel.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("HR", p_objAPACHEIIIValuationDB.strHR.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("ADVARTERYPRESS", p_objAPACHEIIIValuationDB.strAdvArteryPress.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("TEMPERATURE", p_objAPACHEIIIValuationDB.strTemperature.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("BREATH", p_objAPACHEIIIValuationDB.strBreath.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("PAO2", p_objAPACHEIIIValuationDB.strPao2.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("DO2", p_objAPACHEIIIValuationDB.strDo2.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("BLOODCORPUSCLE", p_objAPACHEIIIValuationDB.strBloodCorpuscle.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("AMOUNTLEUCOCYTE", p_objAPACHEIIIValuationDB.strAmountLeucocyte.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("BLOODFLESH", p_objAPACHEIIIValuationDB.strBloodFlesh.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("HYPERCHOLESTEROLEMIA", p_objAPACHEIIIValuationDB.strHypercholesterolemia.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("PH", p_objAPACHEIIIValuationDB.strPH.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("PCO2", p_objAPACHEIIIValuationDB.strPCO2.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("URINEAMOUNT", p_objAPACHEIIIValuationDB.strUrineAmount.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("HEMATURIA", p_objAPACHEIIIValuationDB.strHematuria.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("BLOODNA", p_objAPACHEIIIValuationDB.strBloodNa.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("PROTEID", p_objAPACHEIIIValuationDB.strProteid.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("BLOODGALLBLADDER", p_objAPACHEIIIValuationDB.strBloodGallbladder.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("FIO2", p_objAPACHEIIIValuationDB.strFiO2.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("MACHINEAERATECHK", p_objAPACHEIIIValuationDB.strMachineAerateChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("KIDNEYWANECHK", p_objAPACHEIIIValuationDB.strKidneyWaneChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("AIDSCHK", p_objAPACHEIIIValuationDB.strAIDSChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("LIVERWANECHK", p_objAPACHEIIIValuationDB.strLiverWaneChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("LIMPHOMACHK", p_objAPACHEIIIValuationDB.strLimphomaChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("METASTATICTUMORCHK", p_objAPACHEIIIValuationDB.strMetastaticTumorChk.Replace('\'','き'));

			m_objXmlWriter.WriteAttributeString("LEUKAEMIACHK", p_objAPACHEIIIValuationDB.strLeukaemiaChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("IMMUNITYCHK", p_objAPACHEIIIValuationDB.strImmunityChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("HEPATOCIRRHOSISCHK", p_objAPACHEIIIValuationDB.strHepatocirrhosisChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("ACCORDINGCHK", p_objAPACHEIIIValuationDB.strAccordingChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("POSITIONACHECHK", p_objAPACHEIIIValuationDB.strPositionAcheChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("BODYBENDANDVERTICALCHK", p_objAPACHEIIIValuationDB.strBodyBendAndVerticalChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("BRAINUNREACTIONCHK", p_objAPACHEIIIValuationDB.strBrainUnreactionChk.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("ASPVAL", p_objAPACHEIIIValuationDB.strAspVal.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("PHANDPCO2VAL", p_objAPACHEIIIValuationDB.strPHAndPCO2Val.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("AGEANDHEALTHVAL", p_objAPACHEIIIValuationDB.strAgeAndHealthVal.Replace('\'','き'));

			m_objXmlWriter.WriteAttributeString("NEURONEVAL", p_objAPACHEIIIValuationDB.strNeuroneVal.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("TOTALVAL", p_objAPACHEIIIValuationDB.strTotalVal.Replace('\'','き'));
			m_objXmlWriter.WriteAttributeString("STATUS", "0");
			m_objXmlWriter.WriteAttributeString("PACO2", p_objAPACHEIIIValuationDB.strPaCO2.Replace('\'','き'));

			m_objXmlWriter.WriteEndElement();          
			m_objXmlWriter.WriteEndDocument();
			m_objXmlWriter.Flush();			
			return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(),39*2,(int)m_objXmlMemStream.Length-39*2);
		}
		#endregion

		#region 響函佚連
		public long m_lngGetApacheIIIValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out APACHEIIIValuationDB p_objApacheIIIValuation)
		{
			p_objApacheIIIValuation = null;

			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -2;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("apacheiiivaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

			if(lngRes <= 0)
				return -1;

			if(intRows <= 0)
				return 0;

			XmlTextReader objReader = new XmlTextReader(strXml,XmlNodeType.Element,m_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;

			p_objApacheIIIValuation = new APACHEIIIValuationDB();
			
			while(objReader.Read())
			{
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element:
						if(objReader.HasAttributes)
						{
							p_objApacheIIIValuation.strActivityTime = objReader.GetAttribute("ACTIVITYTIME").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace ('き','\'') ;

							p_objApacheIIIValuation.strEvalDoctorID =  objReader.GetAttribute("EVALDOCTORID").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strAgeGroup =  objReader.GetAttribute("AGEGROUP").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strOperSel= objReader.GetAttribute("OPERSEL").ToString().Replace ('き','\'');
							p_objApacheIIIValuation.strOpenEyeSel= objReader.GetAttribute("OPENEYESEL").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strLanguageSel= objReader.GetAttribute("LANGUAGESEL").ToString().Replace ('き','\'');
							p_objApacheIIIValuation.strHR =  objReader.GetAttribute("HR").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strAdvArteryPress =   objReader.GetAttribute("ADVARTERYPRESS").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strTemperature= objReader.GetAttribute("TEMPERATURE").ToString().Replace ('き','\'');
							p_objApacheIIIValuation.strBreath= objReader.GetAttribute("BREATH").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strPao2 =  objReader.GetAttribute("PAO2").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strDo2=  objReader.GetAttribute("DO2").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strBloodCorpuscle =  objReader.GetAttribute("BLOODCORPUSCLE").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strAmountLeucocyte= objReader.GetAttribute("AMOUNTLEUCOCYTE").ToString().Replace ('き','\'');
							p_objApacheIIIValuation.strBloodFlesh= objReader.GetAttribute("BLOODFLESH").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strHypercholesterolemia= objReader.GetAttribute("HYPERCHOLESTEROLEMIA").ToString().Replace ('き','\'');
							p_objApacheIIIValuation.strPH=	 objReader.GetAttribute("PH").ToString().Replace ('き','\'');							
							p_objApacheIIIValuation.strPCO2=  objReader.GetAttribute("PCO2").ToString().Replace ('き','\'') ;

							p_objApacheIIIValuation.strUrineAmount =  objReader.GetAttribute("URINEAMOUNT").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strHematuria=  objReader.GetAttribute("HEMATURIA").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strBloodNa =  objReader.GetAttribute("BLOODNA").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strProteid=  objReader.GetAttribute("PROTEID").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strBloodGallbladder =  objReader.GetAttribute("BLOODGALLBLADDER").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strFiO2 =  objReader.GetAttribute("FIO2").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strPaCO2 =  objReader.GetAttribute("PACO2").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strMachineAerateChk=  objReader.GetAttribute("MACHINEAERATECHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strKidneyWaneChk =  objReader.GetAttribute("KIDNEYWANECHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strAIDSChk =  objReader.GetAttribute("AIDSCHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strLiverWaneChk=  objReader.GetAttribute("LIVERWANECHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strLimphomaChk =  objReader.GetAttribute("LIMPHOMACHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strMetastaticTumorChk =  objReader.GetAttribute("METASTATICTUMORCHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strLeukaemiaChk =  objReader.GetAttribute("LEUKAEMIACHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strImmunityChk =  objReader.GetAttribute("IMMUNITYCHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strHepatocirrhosisChk=  objReader.GetAttribute("HEPATOCIRRHOSISCHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strAccordingChk=  objReader.GetAttribute("ACCORDINGCHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strPositionAcheChk =  objReader.GetAttribute("POSITIONACHECHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strBodyBendAndVerticalChk =  objReader.GetAttribute("BODYBENDANDVERTICALCHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strBrainUnreactionChk =  objReader.GetAttribute("BRAINUNREACTIONCHK").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strAspVal=  objReader.GetAttribute("ASPVAL").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strPHAndPCO2Val=  objReader.GetAttribute("PHANDPCO2VAL").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strAgeAndHealthVal =  objReader.GetAttribute("AGEANDHEALTHVAL").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strNeuroneVal=  objReader.GetAttribute("NEURONEVAL").ToString().Replace ('き','\'') ;
							p_objApacheIIIValuation.strTotalVal =  objReader.GetAttribute("TOTALVAL").ToString().Replace ('き','\'') ;
						}
						break;
				}
			}
			return 1;
		}
		#endregion

		#region Delete
		public long m_lngDeactive(string p_strDeactiveUserID,string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate)
		{
			string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='"+p_strDeactiveUserID+"' InPatientNO='"+p_strInPatientID +"'"+" InPatientDate='"+p_strInPatientDate+ "'" +" ActivityTime='"+p_strCreateDate+"'" +" />";
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("apacheiiivaluation", strDeactiveXML);

			if(lngRes <= 0)
				return -1;

			return 1;
		}
		#endregion

		#endregion
	}
}
