using System;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;
using System.Windows.Forms;
//using iCare.Common;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// TISS-28d评分的领域层
	/// </summary>
	public class clsTISSValuationDomain
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
		public clsTISSValuationDomain()
		{
			//
			// TODO: Add constructor logic here
			//            
			m_objXmlMemStream = new MemoryStream(300);

			m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream,System.Text.Encoding.Unicode);
			m_objXmlWriter.Flush();//清空原来的字符
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
        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate, string p_strFromDate, string p_strToDate)
		{
			if(p_strInPatientID ==null||p_strInPatientID =="")
				return null;

			DateTime[] dtmCreateRecordDateArr = null;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("tissvaluation",p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

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
		public long m_lngSave(clsTISSValuationInfo p_objTISSValuationInfo)
		{
			if(p_objTISSValuationInfo == null)
				return -1;

			string strMainXml = m_mthGetMainXml(p_objTISSValuationInfo);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
			return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("tissvaluation",strMainXml);
		}

		private string m_mthGetMainXml(clsTISSValuationInfo p_objTISSValuationInfo)
		{
			if(p_objTISSValuationInfo == null)
				return null;

			m_objXmlMemStream.SetLength(0);
			
			try
			{
				m_objXmlWriter.WriteStartDocument();
				m_objXmlWriter.WriteStartElement("RECORDMASTER");

                m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objTISSValuationInfo.strInHospitalNO.Replace('\'', 'き').Trim());
				m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objTISSValuationInfo.strInPatientDate.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objTISSValuationInfo.strActivityTime.Replace('\'','き'));

				m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objTISSValuationInfo.strEvalDoctorID.Replace('\'','き'));

				m_objXmlWriter.WriteAttributeString("ITEM1", p_objTISSValuationInfo.blnItem1.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM2", p_objTISSValuationInfo.blnItem2.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM3", p_objTISSValuationInfo.blnItem3.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM4", p_objTISSValuationInfo.blnItem4.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM5", p_objTISSValuationInfo.blnItem5.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM6", p_objTISSValuationInfo.blnItem6.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM7", p_objTISSValuationInfo.blnItem7.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM8", p_objTISSValuationInfo.blnItem8.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM9", p_objTISSValuationInfo.blnItem9.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM10", p_objTISSValuationInfo.blnItem10.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM11", p_objTISSValuationInfo.blnItem11.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM12", p_objTISSValuationInfo.blnItem12.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM13", p_objTISSValuationInfo.blnItem13.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM14", p_objTISSValuationInfo.blnItem14.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM15", p_objTISSValuationInfo.blnItem15.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM16", p_objTISSValuationInfo.blnItem16.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM17", p_objTISSValuationInfo.blnItem17.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM18", p_objTISSValuationInfo.blnItem18.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM19", p_objTISSValuationInfo.blnItem19.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM20", p_objTISSValuationInfo.blnItem20.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM21", p_objTISSValuationInfo.blnItem21.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM22", p_objTISSValuationInfo.blnItem22.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM23", p_objTISSValuationInfo.blnItem23.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM24", p_objTISSValuationInfo.blnItem24.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM25", p_objTISSValuationInfo.blnItem25.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM26", p_objTISSValuationInfo.blnItem26.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM27", p_objTISSValuationInfo.blnItem27.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("ITEM28", p_objTISSValuationInfo.blnItem28.Replace('\'','き'));
				m_objXmlWriter.WriteAttributeString("RESULT", p_objTISSValuationInfo.strResult.Replace('\'','き'));
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
		public long m_lngGetTISSValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsTISSValuationInfo p_objTISSValuationInfo)
		{
			p_objTISSValuationInfo = null;

			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -2;

			string strXml = "";
			int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("tissvaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

			if(lngRes <= 0)
				return -1;

			if(intRows <= 0)
				return 0;

			XmlTextReader objReader = new XmlTextReader(strXml,XmlNodeType.Element,m_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;

			p_objTISSValuationInfo = new clsTISSValuationInfo();
			
			while(objReader.Read())
			{
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element:
						if(objReader.HasAttributes)
						{
							p_objTISSValuationInfo.strInHospitalNO = objReader.GetAttribute("INPATIENTNO").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.strActivityTime = objReader.GetAttribute("ACTIVITYTIME").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace ('き','\'');

							p_objTISSValuationInfo.strEvalDoctorID = objReader.GetAttribute("EVALDOCTORID").ToString().Replace ('き','\'');

							p_objTISSValuationInfo.strResult = objReader.GetAttribute("RESULT").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem1 = objReader.GetAttribute("ITEM1").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem2 = objReader.GetAttribute("ITEM2").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem3 = objReader.GetAttribute("ITEM3").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem4 = objReader.GetAttribute("ITEM4").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem5 = objReader.GetAttribute("ITEM5").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem6 = objReader.GetAttribute("ITEM6").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem7 = objReader.GetAttribute("ITEM7").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem8 = objReader.GetAttribute("ITEM8").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem9 = objReader.GetAttribute("ITEM9").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem10 = objReader.GetAttribute("ITEM10").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem11 = objReader.GetAttribute("ITEM11").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem12 = objReader.GetAttribute("ITEM12").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem13 = objReader.GetAttribute("ITEM13").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem14 = objReader.GetAttribute("ITEM14").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem15 = objReader.GetAttribute("ITEM15").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem16 = objReader.GetAttribute("ITEM16").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem17 = objReader.GetAttribute("ITEM17").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem18 = objReader.GetAttribute("ITEM18").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem19 = objReader.GetAttribute("ITEM19").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem20 = objReader.GetAttribute("ITEM20").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem21 = objReader.GetAttribute("ITEM21").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem22 = objReader.GetAttribute("ITEM22").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem23 = objReader.GetAttribute("ITEM23").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem24 = objReader.GetAttribute("ITEM24").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem25 = objReader.GetAttribute("ITEM25").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem26 = objReader.GetAttribute("ITEM26").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem27 = objReader.GetAttribute("ITEM27").ToString().Replace ('き','\'');
							p_objTISSValuationInfo.blnItem28 = objReader.GetAttribute("ITEM28").ToString().Replace ('き','\'');

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
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("tissvaluation", strDeactiveXML);			
			if(lngRes <= 0)
				return -1;

			return 1;
		}
		#endregion
		#endregion
	}
	/// <summary>
	/// 存界面控件属性值的类
	/// </summary>
	public class clsTISSValuationInfo
	{		
		public string strInHospitalNO;
		public string strInPatientDate;
		public string strActivityTime;
		public string strModifyDate;

		public string strEvalDoctorID;



		//基础项目
		public string	  blnItem1;
		public string	  blnItem2;
		public string	  blnItem3;
		public string	  blnItem4;
		public string	  blnItem5;
		public string	  blnItem6;
		public string	  blnItem7;
		//通气支持
		public string	  blnItem8;
		public string	  blnItem9;
		public string	  blnItem10;
		public string	  blnItem11;
		//心血管支持
		public string	  blnItem12;
		public string	  blnItem13;
		public string	  blnItem14;
		public string	  blnItem15;
		public string	  blnItem16;
		public string	  blnItem17;
		public string	  blnItem18;
		//肾脏支持
		public string	  blnItem19;
		public string	  blnItem20;
		public string	  blnItem21;
		//神经系统支持
		public string	  blnItem22;
		//代谢支持
		public string	  blnItem23;
		public string	  blnItem24;
		public string	  blnItem25;
		//特殊干预措施
		public string	  blnItem26;
		public string	  blnItem27;
		public string	  blnItem28;
		//结果
		public string strResult;
	}

}
