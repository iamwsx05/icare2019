using System;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// Summary description for clsImproveGlasgowComaEvalutionDomain.
	/// </summary>
	public class clsImproveGlasgowComaEvalutionDomain
	{
		#region Member
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

		#endregion

		#region Constructor
		public clsImproveGlasgowComaEvalutionDomain()
		{   
			m_objXmlMemStream = new MemoryStream(300);

			m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();//清空原来的字符
			m_objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);

		}
		#endregion

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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("improveglasgowcomaevluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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
		public long m_lngSave(clsImproveGlasgowComaEvaluation p_objImproveGlasgowEvaluation)
		{
			if(p_objImproveGlasgowEvaluation == null)
				return -1;

			string strMainXml = m_mthGetMainXml(p_objImproveGlasgowEvaluation);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("improveglasgowcomaevluation", strMainXml);
		}

		private string m_mthGetMainXml(clsImproveGlasgowComaEvaluation p_objImproveGlasgowEvaluation)
		{
			if(p_objImproveGlasgowEvaluation == null)
				return null;

			m_objXmlMemStream.SetLength(0);
			
			try
			{
				m_objXmlWriter.WriteStartDocument();
				m_objXmlWriter.WriteStartElement("RECORDMASTER");

                m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objImproveGlasgowEvaluation.m_strInPatientNO.Replace('\'', 'き').Trim());
				m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objImproveGlasgowEvaluation.m_strInPatientDate.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objImproveGlasgowEvaluation.m_strActivityTime.Replace('\'','き'));

				m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objImproveGlasgowEvaluation.m_strEvalDoctorID.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("AGEGROUP", p_objImproveGlasgowEvaluation.m_strAgeGroup.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("OPENEYEU1", p_objImproveGlasgowEvaluation.m_strOpenEyeU1.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("OPENEYEO1", p_objImproveGlasgowEvaluation.m_strOpenEyeO1.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("SPORTFEEDBACKU1", p_objImproveGlasgowEvaluation.m_strSportFeedbackU1.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("SPORTFEEDBACKO1", p_objImproveGlasgowEvaluation.m_strSportFeedbackO1.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("TALKFEEDBACKU2", p_objImproveGlasgowEvaluation.m_strTalkFeedbackU2.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("TALKFEEDBACKU5", p_objImproveGlasgowEvaluation.m_strTalkFeedbackU5.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("TALKFEEDBACKO5", p_objImproveGlasgowEvaluation.m_strTalkFeedbackO5.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("PUPILLIGHTFEEDBACK", p_objImproveGlasgowEvaluation.m_strPupilLightFeedback.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("BRAINREFLECT", p_objImproveGlasgowEvaluation.m_strBrainReflect.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("TWITCH", p_objImproveGlasgowEvaluation.m_strTwitch.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("SPONTANEITYBREATH", p_objImproveGlasgowEvaluation.m_strSpontaneityBreath.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("OPENEYEEVAL", p_objImproveGlasgowEvaluation.m_strOpenEyeEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("SPORTFEEDBACKEVAL", p_objImproveGlasgowEvaluation.m_strSportFeedbackEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("TALKFEEDBACKEVAL", p_objImproveGlasgowEvaluation.m_strTalkFeedbackEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("PUPILLIGHTFEEDBACKEVAL", p_objImproveGlasgowEvaluation.m_strPupilLightFeedbackEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("BRAINREFLECTEVAL", p_objImproveGlasgowEvaluation.m_strBrainReflectEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("TWITCHEVAL", p_objImproveGlasgowEvaluation.m_strTwitchEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("SPONTANEITYBREATHEVAL", p_objImproveGlasgowEvaluation.m_strSpontaneityBreathEval.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("TOTALEVAL", p_objImproveGlasgowEvaluation.m_strTotalEval.Replace('\'','き'));
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

		#region 读取信息
		public long m_lngGetImproveGlasgowComaValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsImproveGlasgowComaEvaluation p_objImproveGlasgowEvaluation)
		{
			p_objImproveGlasgowEvaluation = null;

			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -2;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("improveglasgowcomaevluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

			if(lngRes <= 0)
				return -1;

			if(intRows <= 0)
				return 0;

			XmlTextReader objReader = new XmlTextReader(strXml,XmlNodeType.Element,m_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;

			p_objImproveGlasgowEvaluation = new clsImproveGlasgowComaEvaluation();
			
			while(objReader.Read())
			{
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element:
						if(objReader.HasAttributes)
						{
							p_objImproveGlasgowEvaluation.m_strInPatientNO =  objReader.GetAttribute("INPATIENTNO").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strInPatientDate =  objReader.GetAttribute("INPATIENTDATE").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strActivityTime =  objReader.GetAttribute("ACTIVITYTIME").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strModifyDate =  objReader.GetAttribute("MODIFYDATE").ToString().Replace ('き','\'') ;

							p_objImproveGlasgowEvaluation.m_strEvalDoctorID =  objReader.GetAttribute("EVALDOCTORID").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strAgeGroup =  objReader.GetAttribute("AGEGROUP").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strOpenEyeU1 =  objReader.GetAttribute("OPENEYEU1").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strOpenEyeO1 =  objReader.GetAttribute("OPENEYEO1").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strSportFeedbackU1 =  objReader.GetAttribute("SPORTFEEDBACKU1").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strSportFeedbackO1 =  objReader.GetAttribute("SPORTFEEDBACKO1").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strTalkFeedbackU2 =  objReader.GetAttribute("TALKFEEDBACKU2").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strTalkFeedbackU5 =  objReader.GetAttribute("TALKFEEDBACKU5").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strTalkFeedbackO5 =  objReader.GetAttribute("TALKFEEDBACKO5").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strPupilLightFeedback =  objReader.GetAttribute("PUPILLIGHTFEEDBACK").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strBrainReflect =  objReader.GetAttribute("BRAINREFLECT").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strTwitch =  objReader.GetAttribute("TWITCH").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strSpontaneityBreath =  objReader.GetAttribute("SPONTANEITYBREATH").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strOpenEyeEval =  objReader.GetAttribute("OPENEYEEVAL").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strSportFeedbackEval =  objReader.GetAttribute("SPORTFEEDBACKEVAL").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strTalkFeedbackEval =  objReader.GetAttribute("TALKFEEDBACKEVAL").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strPupilLightFeedbackEval =  objReader.GetAttribute("PUPILLIGHTFEEDBACKEVAL").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strBrainReflectEval =  objReader.GetAttribute("BRAINREFLECTEVAL").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strTwitchEval =  objReader.GetAttribute("TWITCHEVAL").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strSpontaneityBreathEval =  objReader.GetAttribute("SPONTANEITYBREATHEVAL").ToString().Replace ('き','\'') ;
							p_objImproveGlasgowEvaluation.m_strTotalEval =  objReader.GetAttribute("TOTALEVAL").ToString().Replace ('き','\'') ;

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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("improveglasgowcomaevluation", strDeactiveXML);			
			if(lngRes <= 0)
				return -1;

			return 1;
		}
		#endregion
	}

	public class clsImproveGlasgowComaEvaluation
	{
		public string m_strInPatientNO;
		public string m_strInPatientDate;
		public string m_strActivityTime;
		public string m_strModifyDate;

		public string m_strEvalDoctorID;
		public string m_strAgeGroup;
		public string m_strOpenEyeU1;
		public string m_strOpenEyeO1;
		public string m_strSportFeedbackU1;
		public string m_strSportFeedbackO1;
		public string m_strTalkFeedbackU2;
		public string m_strTalkFeedbackU5;
		public string m_strTalkFeedbackO5;
		public string m_strPupilLightFeedback;
		public string m_strBrainReflect;
		public string m_strTwitch;
		public string m_strSpontaneityBreath;
		public string m_strOpenEyeEval;
		public string m_strSportFeedbackEval;
		public string m_strTalkFeedbackEval;
		public string m_strPupilLightFeedbackEval;
		public string m_strBrainReflectEval;
		public string m_strTwitchEval;
		public string m_strSpontaneityBreathEval;
		public string m_strTotalEval;
		
		public string m_strStatus;
		public string m_strDeactivatedDate;
		public string m_strDeActivedOperatorID;
	}
}
