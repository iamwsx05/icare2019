using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;
//using iCare.Common;

namespace iCare.ICU.Evaluation
{
	public class BabyInjuryCaseEvaluationDB
	{
		public string strPatientID;
		public string strInPatientDate;
		public string strActivityTime;
		public string strModifyDate;

		public string strIsNewBaby;
		public string strEvalDoctorID;
		public string strAgeGroup;
		public string strHeartRate;
		public string strBloodOrShrinkPressure;
		public string strBldOrShKSel;
		public string strBreath;
		public string strIsrhythmWrong;
		public string strPao2;
		public string strPao0kPaOrmmHgSel;
		public string strpH;
		public string strNaPlus;
		public string strKPlus;
		public string strCrOrBUN;
		public string strCrOrBUNSel;
		public string strRedCellComp;
		public string strHb;
		public string strHbSel;
		public string strStomachAndIntestines;
		public string strHeartRateEval;
		public string strBloodPressureOrShrinkPressureEval;
		public string strBreathEval;
		public string strPao2Eval;
		public string strpHEval;
		public string strNaPlusEval;
		public string strKPlusEval;
		public string strCrOrBUNEval;
		public string strRedCellCompOrHbEval;
		public string strStomachAndintestinesBehaveEval;
		public string strTotalEval;

	}
	/// <summary>
	/// Summary description for BabyInjuryCaseEvaluationDomain.
	/// </summary>
	public class BabyInjuryCaseEvaluationDomain
	{
		#region Member	

		/// <summary>
		/// ����Xml�Ļ���
		/// </summary>
		private MemoryStream m_objXmlMemStream;
		/// <summary>
		/// ����Xml�Ĺ���
		/// </summary>
		private XmlTextWriter m_objXmlWriter;
		///  <summary>
		/// ��ȡXml�����������		
		/// </summary>
		private XmlParserContext m_objXmlParser;

		#endregion

		#region Constructor
		public BabyInjuryCaseEvaluationDomain()
		{   
			m_objXmlMemStream = new MemoryStream(300);

			m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();//���ԭ�����ַ�
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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("babyinjurycaseevaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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

		#region ��ȡ��Ϣ
		public long m_lngGetBabyInjuryCaseValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out BabyInjuryCaseEvaluationDB p_objBabyInjuryCaseEvaluationDB)
		{
			p_objBabyInjuryCaseEvaluationDB = null;

			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -2;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));            
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("babyinjurycaseevaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

			if(lngRes <= 0)
				return -1;

			if(intRows <= 0)
				return 0;

			XmlTextReader objReader = new XmlTextReader(strXml,XmlNodeType.Element,m_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;

			p_objBabyInjuryCaseEvaluationDB = new BabyInjuryCaseEvaluationDB();
			
			while(objReader.Read())
			{
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element:
						if(objReader.HasAttributes)
						{
							p_objBabyInjuryCaseEvaluationDB.strPatientID =  objReader.GetAttribute("INPATIENTNO").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strInPatientDate =  objReader.GetAttribute("INPATIENTDATE").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strActivityTime =  objReader.GetAttribute("ACTIVITYTIME").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strModifyDate =  objReader.GetAttribute("MODIFYDATE").ToString().Replace ('��','\'') ;

							p_objBabyInjuryCaseEvaluationDB.strEvalDoctorID =  objReader.GetAttribute("EVALDOCTORID").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strHeartRate =  objReader.GetAttribute("HEARTRATE").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strBloodOrShrinkPressure= objReader.GetAttribute("BLOODORSHRINKPRESSURE").ToString().Replace ('��','\'');
							p_objBabyInjuryCaseEvaluationDB.strBldOrShKSel= objReader.GetAttribute("BLDORSHKSEL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strAgeGroup= objReader.GetAttribute("AGEGROUP").ToString().Replace ('��','\'');
							p_objBabyInjuryCaseEvaluationDB.strBreath =  objReader.GetAttribute("BREATH").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strIsrhythmWrong =   objReader.GetAttribute("ISRHYTHMWRONG").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strPao2= objReader.GetAttribute("PAO2").ToString().Replace ('��','\'');
							p_objBabyInjuryCaseEvaluationDB.strPao0kPaOrmmHgSel= objReader.GetAttribute("PAO0KPAORMMHGSEL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strpH =  objReader.GetAttribute("PH").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strNaPlus=  objReader.GetAttribute("NAPLUS").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strKPlus =  objReader.GetAttribute("KPLUS").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strCrOrBUN= objReader.GetAttribute("CRORBUN").ToString().Replace ('��','\'');
							p_objBabyInjuryCaseEvaluationDB.strCrOrBUNSel= objReader.GetAttribute("CRORBUNSEL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strHb= objReader.GetAttribute("HB").ToString().Replace ('��','\'');
							p_objBabyInjuryCaseEvaluationDB.strHbSel=	 objReader.GetAttribute("HBSEL").ToString().Replace ('��','\'');							
							p_objBabyInjuryCaseEvaluationDB.strStomachAndIntestines =  objReader.GetAttribute("STOMACHANDINTESTINES").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strHeartRateEval =  objReader.GetAttribute("HEARTRATEEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strBloodPressureOrShrinkPressureEval =  objReader.GetAttribute("BLOODPRESSORSHRINKPRESSEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strBreathEval =  objReader.GetAttribute("BREATHEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strPao2Eval =  objReader.GetAttribute("PAO2EVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strpHEval =  objReader.GetAttribute("PHEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strNaPlusEval =  objReader.GetAttribute("NAPLUSEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strKPlusEval =  objReader.GetAttribute("KPLUSEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strCrOrBUNEval =  objReader.GetAttribute("CRORBUNEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strRedCellCompOrHbEval =  objReader.GetAttribute("REDCELLCOMPORHBEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strStomachAndintestinesBehaveEval =  objReader.GetAttribute("STOMACHANDINTESTINESBEHAVEEVAL").ToString().Replace ('��','\'') ;
							p_objBabyInjuryCaseEvaluationDB.strTotalEval =  objReader.GetAttribute("TOTALEVAL").ToString().Replace ('��','\'') ;
						}
						break;
				}
			}
			return 1;
		}
		#endregion

		#region Save
		public long m_lngSave(BabyInjuryCaseEvaluationDB p_objBabyInjuryCaseEvaluationDB)
		{
			if(p_objBabyInjuryCaseEvaluationDB == null)
				return -1;

			string strMainXml = m_mthGetMainXml(p_objBabyInjuryCaseEvaluationDB);

            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("babyinjurycaseevaluation", strMainXml);
		}

		private string m_mthGetMainXml(BabyInjuryCaseEvaluationDB p_objBabyInjuryCaseEvaluationDB)
		{
			if(p_objBabyInjuryCaseEvaluationDB == null)
				return null;

			m_objXmlMemStream.SetLength(0);
			
			try
			{
				m_objXmlWriter.WriteStartDocument();
				m_objXmlWriter.WriteStartElement("RECORDMASTER");

                m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objBabyInjuryCaseEvaluationDB.strPatientID.Replace('\'', '��').Trim());
				m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objBabyInjuryCaseEvaluationDB.strInPatientDate.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objBabyInjuryCaseEvaluationDB.strActivityTime.Replace('\'','��'));

				m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objBabyInjuryCaseEvaluationDB.strEvalDoctorID.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("AGEGROUP", p_objBabyInjuryCaseEvaluationDB.strAgeGroup.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("HEARTRATE", p_objBabyInjuryCaseEvaluationDB.strHeartRate.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("BLOODORSHRINKPRESSURE", p_objBabyInjuryCaseEvaluationDB.strBloodOrShrinkPressure.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("BLDORSHKSEL", p_objBabyInjuryCaseEvaluationDB.strBldOrShKSel.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("BREATH", p_objBabyInjuryCaseEvaluationDB.strBreath.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("ISRHYTHMWRONG", p_objBabyInjuryCaseEvaluationDB.strIsrhythmWrong.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("PAO2", p_objBabyInjuryCaseEvaluationDB.strPao2.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("PAO0KPAORMMHGSEL", p_objBabyInjuryCaseEvaluationDB.strPao0kPaOrmmHgSel.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("PH", p_objBabyInjuryCaseEvaluationDB.strpH.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("NAPLUS", p_objBabyInjuryCaseEvaluationDB.strNaPlus.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("KPLUS", p_objBabyInjuryCaseEvaluationDB.strKPlus.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("CRORBUN", p_objBabyInjuryCaseEvaluationDB.strCrOrBUN.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("CRORBUNSEL", p_objBabyInjuryCaseEvaluationDB.strCrOrBUNSel.Replace('\'','��'));
//				m_objXmlWriter.WriteAttributeString("REDCELLCOMP", p_objBabyInjuryCaseEvaluationDB.strRedCellComp.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("HB", p_objBabyInjuryCaseEvaluationDB.strHb.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("HBSEL", p_objBabyInjuryCaseEvaluationDB.strHbSel.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("STOMACHANDINTESTINES", p_objBabyInjuryCaseEvaluationDB.strStomachAndIntestines.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("HEARTRATEEVAL", p_objBabyInjuryCaseEvaluationDB.strHeartRateEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("BLOODPRESSORSHRINKPRESSEVAL", p_objBabyInjuryCaseEvaluationDB.strBloodPressureOrShrinkPressureEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("BREATHEVAL", p_objBabyInjuryCaseEvaluationDB.strBreathEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("PAO2EVAL", p_objBabyInjuryCaseEvaluationDB.strPao2Eval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("PHEVAL", p_objBabyInjuryCaseEvaluationDB.strpHEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("NAPLUSEVAL", p_objBabyInjuryCaseEvaluationDB.strNaPlusEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("KPLUSEVAL", p_objBabyInjuryCaseEvaluationDB.strKPlusEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("CRORBUNEVAL", p_objBabyInjuryCaseEvaluationDB.strCrOrBUNEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("REDCELLCOMPORHBEVAL", p_objBabyInjuryCaseEvaluationDB.strRedCellCompOrHbEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("STOMACHANDINTESTINESBEHAVEEVAL", p_objBabyInjuryCaseEvaluationDB.strStomachAndintestinesBehaveEval.Replace('\'','��'));
				m_objXmlWriter.WriteAttributeString("TOTALEVAL", p_objBabyInjuryCaseEvaluationDB.strTotalEval.Replace('\'','��'));
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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("babyinjurycaseevaluation", strDeactiveXML);

			if(lngRes <= 0)
				return -1;

			return 1;
		}
		#endregion

		#endregion

	}
}
