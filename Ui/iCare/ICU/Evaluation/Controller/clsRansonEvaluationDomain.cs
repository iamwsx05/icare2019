using System;
using System.Data;
using System.Xml;
using System.IO;
//using com.digitalwave.iCare.middletier.ICU;
using com.digitalwave.iCare.common;

namespace iCare.ICU.Evaluation
{
    /// <summary>
    /// Ranson评分的商业逻辑层
    /// </summary>
    public class clsRansonEvaluationDomain
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
        public clsRansonEvaluationDomain()
        {
            //
            // TODO: Add constructor logic here
            //            

            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//清空原来的字符
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);

        }


        //Load All Create Date

        public DateTime[] m_dtmGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate, string p_strFromDate, string p_strToDate)
        {
            if (p_strInPatientID == null || p_strInPatientID == "")
                return null;

            DateTime[] dtmCreateRecordDateArr = null;

            string strXml = "";
            int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetTimeInfoOfAPatient("ransonevaluation", p_strInPatientID, p_strInPatientDate, p_strFromDate, p_strToDate, ref strXml, ref intRows);

            if (lngRes > 0 && intRows > 0)
            {
                dtmCreateRecordDateArr = new DateTime[intRows];

                XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                int intIndex = 0;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                dtmCreateRecordDateArr[intIndex] = DateTime.Parse(objReader.GetAttribute("ACTIVITYTIME"));
                                intIndex++;
                            }
                            break;
                    }
                }
            }
            return dtmCreateRecordDateArr;
        }
        //END  Load All Create Date

        // Save********
        public long m_lngSave(clsEvalInfoOfRansonEvaluation p_objRansonEvaluationDB)
        {
            if (p_objRansonEvaluationDB == null)
                return -1;

            string strMainXml = m_mthGetMainXml(p_objRansonEvaluationDB);
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            return (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngAddNew("ransonevaluation", strMainXml);
        }

        private string m_mthGetMainXml(clsEvalInfoOfRansonEvaluation p_objRansonEvaluationDB)
        {
            if (p_objRansonEvaluationDB == null)
                return null;

            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RECORDMASTER");

            m_objXmlWriter.WriteAttributeString("INPATIENTNO", p_objRansonEvaluationDB.strPatientID.Replace('\'', 'き').Trim());
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", p_objRansonEvaluationDB.strInPatientDate.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("ACTIVITYTIME", p_objRansonEvaluationDB.strActivityTime.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("EVALDOCTORID", p_objRansonEvaluationDB.strEvalDoctorID.Replace('\'', 'き'));

            m_objXmlWriter.WriteAttributeString("KIND", p_objRansonEvaluationDB.strKind.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("AGE", p_objRansonEvaluationDB.strAge.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("BXB", p_objRansonEvaluationDB.strBXB.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("XT", p_objRansonEvaluationDB.strXT.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("RST", p_objRansonEvaluationDB.strRST.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("TDA", p_objRansonEvaluationDB.strTDA.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("XXB", p_objRansonEvaluationDB.strXXB.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("XNS", p_objRansonEvaluationDB.strXNS.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("XG", p_objRansonEvaluationDB.strXG.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("DMX", p_objRansonEvaluationDB.strDMX.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("JQS", p_objRansonEvaluationDB.strJQS.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("YTZ", p_objRansonEvaluationDB.strYTZ.Replace('\'', 'き'));


            m_objXmlWriter.WriteAttributeString("MORTALITY", p_objRansonEvaluationDB.strMortality.Replace('\'', 'き'));
            m_objXmlWriter.WriteAttributeString("STATUS", "0");

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }
        //END Save

        //读取信息**********
        public long m_lngGetRansonValue(string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out clsEvalInfoOfRansonEvaluation p_objRansonEvaluation)
        {
            p_objRansonEvaluation = null;

            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
                return -2;

            string strXml = "";
            int intRows = 0;
            //clsEvaluation_QuerySvc objSvc = (clsEvaluation_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_QuerySvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_QuerySvc_m_lngGetEvaluationData("ransonevaluation", p_strInPatientID, p_strInPatientDate, p_strCreateDate, ref strXml, ref intRows);

            if (lngRes <= 0)
                return -1;

            if (intRows <= 0)
                return 0;

            XmlTextReader objReader = new XmlTextReader(strXml, XmlNodeType.Element, m_objXmlParser);
            objReader.WhitespaceHandling = WhitespaceHandling.None;

            p_objRansonEvaluation = new clsEvalInfoOfRansonEvaluation();

            while (objReader.Read())
            {
                switch (objReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (objReader.HasAttributes)
                        {
                            p_objRansonEvaluation.strPatientID = objReader.GetAttribute("INPATIENTNO").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strActivityTime = objReader.GetAttribute("ACTIVITYTIME").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strEvalDoctorID = objReader.GetAttribute("EVALDOCTORID").ToString().Replace('き', '\'');

                            p_objRansonEvaluation.strKind = objReader.GetAttribute("KIND").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strAge = objReader.GetAttribute("AGE").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strBXB = objReader.GetAttribute("BXB").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strXT = objReader.GetAttribute("XT").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strRST = objReader.GetAttribute("RST").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strTDA = objReader.GetAttribute("TDA").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strXXB = objReader.GetAttribute("XXB").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strXNS = objReader.GetAttribute("XNS").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strXG = objReader.GetAttribute("XG").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strDMX = objReader.GetAttribute("DMX").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strJQS = objReader.GetAttribute("JQS").ToString().Replace('き', '\'');
                            p_objRansonEvaluation.strYTZ = objReader.GetAttribute("YTZ").ToString().Replace('き', '\'');

                            p_objRansonEvaluation.strMortality = objReader.GetAttribute("MORTALITY").ToString().Replace('き', '\'');
                        }
                        break;
                }
            }
            return 1;
        }
        //END 读取信息

        //Delete  ************
        public long m_lngDeactive(string p_strDeactiveUserID, string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate)
        {
            string strDeactiveXML = "<Deactive Status='1' DeActivedOperatorID='" + p_strDeactiveUserID + "' InPatientNO='" + p_strInPatientID + "'" + " InPatientDate='" + p_strInPatientDate + "'" + " ActivityTime='" + p_strCreateDate + "'" + " />";
            //clsEvaluation_UpdataSvc objSvc = (clsEvaluation_UpdataSvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsEvaluation_UpdataSvc));
            long lngRes = (new weCare.Proxy.ProxyEmr07()).Service.clsEvaluation_UpdataSvc_m_lngDeActiveRecord("ransonevaluation", strDeactiveXML);
            if (lngRes <= 0)
                return -1;

            return 1;
        }
        //End Delete


    }


    public class clsEvalInfoOfRansonEvaluation
    {
        public string strPatientID;
        public string strInPatientDate;
        public string strModifyDate;
        public string strActivityTime;

        public string strEvalDoctorID;
        public string strEvalDoctorName;

        public string strKind;
        public string strAge;
        public string strBXB;           //白细胞
        public string strXT;            //血糖
        public string strRST;           //乳酸脱氢酶
        public string strTDA;           //天冬氨酸转氨酶
        public string strXXB;           //血细胞比容下降
        public string strXNS;           //血尿素氮升高
        public string strXG;            //血钙
        public string strDMX;           //动脉血养分压
        public string strJQS;           //碱缺失
        public string strYTZ;           //液体潴留

        public string strMortality;//病死率
    }

}