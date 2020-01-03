using System;
using System.IO;
using System.Xml;
using weCare.Core.Entity;


namespace iCare
{
    /// <summary>
    /// Summary description for clsOperationEqipmentQtyDomain.
    /// </summary>
    public class clsOperationEqipmentQtyDomain
    {
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
        //private com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService  m_objServ;

        public clsOperationEqipmentQtyDomain()
        {
            //
            // TODO: Add constructor logic here
            //
            //m_objServ=new com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService ();
            m_objXmlMemStream = new MemoryStream(300);

            m_objXmlWriter = new XmlTextWriter(m_objXmlMemStream, System.Text.Encoding.Unicode);
            m_objXmlWriter.Flush();//���ԭ�����ַ�
            m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, System.Text.Encoding.Unicode);
        }

        /// <summary>
        /// ����¼�¼
        /// </summary>
        /// <param name="objOperationEquipmentPackage"></param>
        /// <returns></returns>
        public long lngAddNewRecord(clsOperationEquipmentPackage objOperationEquipmentPackage)
        {
            if (objOperationEquipmentPackage == null || objOperationEquipmentPackage.m_objOperationEqipmentQtyContent == null
                 || objOperationEquipmentPackage.m_objOperationEqipmentQtyXML == null)
                return -1;

            string strMasterXML;
            string strSubXML;
            string[] strNurseXML;

            //ƴ����XML
            strMasterXML = m_strGetMasterXMLInsert(objOperationEquipmentPackage.m_objOperationEqipmentQtyXML, true);

            //ƴ�ӱ�XML
            strSubXML = m_strGetContentXML(objOperationEquipmentPackage.m_objOperationEqipmentQtyContent);

            //ƴ��ʿXML
            if (objOperationEquipmentPackage.m_objOperationNurse != null)
            {
                strNurseXML = new string[objOperationEquipmentPackage.m_objOperationNurse.Length];
                for (int i = 0; i < objOperationEquipmentPackage.m_objOperationNurse.Length; i++)
                {
                    strNurseXML[i] = m_strGetNurseXML(objOperationEquipmentPackage.m_objOperationNurse[i]);
                }
            }
            else
                strNurseXML = null;
            long lngSucceed = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.m_lngAddNewRecord(strMasterXML, strSubXML, strNurseXML);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngSucceed;
        }

        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        /// <param name="objOperationEquipmentPackage"></param>
        /// <param name="strInPatientID"></param>
        /// <param name="strInPatientDate"></param>
        /// <param name="strOpenDate"></param>
        /// <returns></returns>
        public long lngModify(clsOperationEquipmentPackage objOperationEquipmentPackage, string strInPatientID, string strInPatientDate, string strOpenDate)
        {
            if (objOperationEquipmentPackage == null || objOperationEquipmentPackage.m_objOperationEqipmentQtyContent == null
                || objOperationEquipmentPackage.m_objOperationEqipmentQtyXML == null)
                return -1;

            string strMasterXML;
            string strSubXML;
            string[] strNurseXML;

            //ƴ����XML
            strMasterXML = m_strGetMasterXMLInsert(objOperationEquipmentPackage.m_objOperationEqipmentQtyXML, false);

            //ƴ�ӱ�XML
            strSubXML = m_strGetContentXML(objOperationEquipmentPackage.m_objOperationEqipmentQtyContent);

            //ƴ��ʿXML
            if (objOperationEquipmentPackage.m_objOperationNurse != null)
            {
                strNurseXML = new string[objOperationEquipmentPackage.m_objOperationNurse.Length];
                for (int i = 0; i < objOperationEquipmentPackage.m_objOperationNurse.Length; i++)
                {
                    strNurseXML[i] = m_strGetNurseXML(objOperationEquipmentPackage.m_objOperationNurse[i]);
                }
            }
            else
                strNurseXML = null;

            long lngSucceed = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.lngModify(strMasterXML, strSubXML, strNurseXML, strInPatientID, strInPatientDate, strOpenDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngSucceed;
        }



        #region ƴ����XML
        /// <summary>
        /// ƴ����XML
        /// </summary>
        /// <param name="objGeneralTendRecordInfo"></param>
        /// <returns></returns>
        private string m_strGetMasterXMLInsert(clsOperationEqipmentQtyXML objclsOperationEqipmentQtyXMLInsert, bool bolIfSave)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordMaster");

            if (bolIfSave == true)
            {
                m_objXmlWriter.WriteAttributeString("STATUS", objclsOperationEqipmentQtyXMLInsert.strStatus);
                m_objXmlWriter.WriteAttributeString("IFCONFIRM", objclsOperationEqipmentQtyXMLInsert.strIfConfirm);
            }

            m_objXmlWriter.WriteAttributeString("INPATIENTID", objclsOperationEqipmentQtyXMLInsert.strInPatientID);
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", objclsOperationEqipmentQtyXMLInsert.strInPatientDate);
            m_objXmlWriter.WriteAttributeString("OPENDATE", objclsOperationEqipmentQtyXMLInsert.strOpenDate);

            m_objXmlWriter.WriteAttributeString("CREATEDATE", objclsOperationEqipmentQtyXMLInsert.strCreateDate);
            m_objXmlWriter.WriteAttributeString("CREATEUSERID", objclsOperationEqipmentQtyXMLInsert.strCreateUserID);

            //			m_objXmlWriter.WriteAttributeString("DEACTIVEDDATE", objclsOperationEqipmentQtyXML.strDeActivedDate.Replace('\'','��'));
            //			m_objXmlWriter.WriteAttributeString("DEACTIVEDOPERATORID", objclsOperationEqipmentQtyXML.strDeActivedOperatorID.Replace('\'','��'));
            //			m_objXmlWriter.WriteAttributeString("CONFIRMREASON", objclsOperationEqipmentQtyXML.strConfirmReason.Replace('\'','��'));
            //			m_objXmlWriter.WriteAttributeString("CONFIRMREASONXML", objclsOperationEqipmentQtyXML.strConfirmReasonXML.Replace('\'','��'));
            //			m_objXmlWriter.WriteAttributeString("OPERATIONIDXML", objclsOperationEqipmentQtyXML.strOperationIDXML.Replace('\'','��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONNAMEXML", objclsOperationEqipmentQtyXMLInsert.strOperationNameXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WENZHI125XML", objclsOperationEqipmentQtyXMLInsert.strWenZhi125XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WENZHI125AFTERXML", objclsOperationEqipmentQtyXMLInsert.strWenZhi125AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WENZHI125BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWenZhi125BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WENWAN125XML", objclsOperationEqipmentQtyXMLInsert.strWenWan125XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WENWAN125AFTERXML", objclsOperationEqipmentQtyXMLInsert.strWenWan125AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WENWAN125BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWenWan125BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIAOZHI14XML", objclsOperationEqipmentQtyXMLInsert.strXiaoZhi14XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAOZHI14AFTERXML", objclsOperationEqipmentQtyXMLInsert.strXiaoZhi14AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAOZHI14BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXiaoZhi14BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIAOWAN14XML", objclsOperationEqipmentQtyXMLInsert.strXiaoWan14XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAOWAN14AFTERXML", objclsOperationEqipmentQtyXMLInsert.strXiaoWan14AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAOWAN14BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXiaoWan14BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHONGZHI16XML", objclsOperationEqipmentQtyXMLInsert.strZhongZhi16XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGZHI16AFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhongZhi16AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGZHI16BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhongZhi16BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHONGWAN16XML", objclsOperationEqipmentQtyXMLInsert.strZhongWan16XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGWAN16AFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhongWan16AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGWAN16BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhongWan16BeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("PIQIANXML", objclsOperationEqipmentQtyXMLInsert.strPiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strPiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strPiQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIYOUCHIXUEGUANQIANXML", objclsOperationEqipmentQtyXMLInsert.strZhiYouChiXueGuanQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIYOUCHIXUEGUANQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhiYouChiXueGuanQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIYOUCHIXUEGUANQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhiYouChiXueGuanQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("QUANQIANXML", objclsOperationEqipmentQtyXMLInsert.strQuanQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUANQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strQuanQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUANQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strQuanQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JINQIANXML", objclsOperationEqipmentQtyXMLInsert.strJinQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strJinQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strJinQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHIZHENQIAN18XML", objclsOperationEqipmentQtyXMLInsert.strChiZhenQian18XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIZHENQIAN18AFTERXML", objclsOperationEqipmentQtyXMLInsert.strChiZhenQian18AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIZHENQIAN18BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChiZhenQian18BeforeXML.Replace('\'', '��'));



            m_objXmlWriter.WriteAttributeString("YOUCHINIEXML", objclsOperationEqipmentQtyXMLInsert.strYouChiNieXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUCHINIEAFTERXML", objclsOperationEqipmentQtyXMLInsert.strYouChiNieAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUCHINIEBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strYouChiNieBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WUCHINIEXML", objclsOperationEqipmentQtyXMLInsert.strWuChiNieXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUCHINIEAFTERXML", objclsOperationEqipmentQtyXMLInsert.strWuChiNieAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUCHINIEBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWuChiNieBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGYABANXML", objclsOperationEqipmentQtyXMLInsert.strChangYaBanXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGYABANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strChangYaBanAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGYABANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChangYaBanBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOBING3XML", objclsOperationEqipmentQtyXMLInsert.strDaoBing3XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING3AFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaoBing3AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING3BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaoBing3BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOBING4XML", objclsOperationEqipmentQtyXMLInsert.strDaoBing4XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING4AFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaoBing4AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING4BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaoBing4BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOBING7XML", objclsOperationEqipmentQtyXMLInsert.strDaoBing7XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING7AFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaoBing7AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING7BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaoBing7BeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("WANZHUZHIJIANXML", objclsOperationEqipmentQtyXMLInsert.strWanZhuZhiJianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANZHUZHIJIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strWanZhuZhiJianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANZHUZHIJIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWanZhuZhiJianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIZHUZHIJIANXML", objclsOperationEqipmentQtyXMLInsert.strZhiZhuZhiJianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIZHUZHIJIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhiZhuZhiJianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIZHUZHIJIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhiZhuZhiJianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("BIANTAOXIANJIANXML", objclsOperationEqipmentQtyXMLInsert.strBianTaoXianJianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIANTAOXIANJIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strBianTaoXianJianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIANTAOXIANJIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strBianTaoXianJianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIONGQIANGJIANXML", objclsOperationEqipmentQtyXMLInsert.strXiongQiangJianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIONGQIANGJIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXiongQiangJianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIONGQIANGJIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXiongQiangJianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIJIAOXIAOLAGOUXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoXiaoLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOXIAOLAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoXiaoLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOXIAOLAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoXiaoLaGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LANWEILAGOUXML", objclsOperationEqipmentQtyXMLInsert.strLanWeiLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LANWEILAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strLanWeiLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LANWEILAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strLanWeiLaGouBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("ZHONGFUGOUXML", objclsOperationEqipmentQtyXMLInsert.strZhongFuGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGFUGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhongFuGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGFUGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhongFuGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGYAGOUXML", objclsOperationEqipmentQtyXMLInsert.strChangYaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGYAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strChangYaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGYAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChangYaGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIJIAOGOUXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIAFUBUQIANKAIQIXML", objclsOperationEqipmentQtyXMLInsert.strXiaFuBuQianKaiQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAFUBUQIANKAIQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXiaFuBuQianKaiQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAFUBUQIANKAIQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXiaFuBuQianKaiQiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("TONGQUANXML", objclsOperationEqipmentQtyXMLInsert.strTongQuanXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TONGQUANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strTongQuanAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TONGQUANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strTongQuanBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIYEGUANXML", objclsOperationEqipmentQtyXMLInsert.strXiYeGuanXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIYEGUANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXiYeGuanAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIYEGUANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXiYeGuanBeforeXML.Replace('\'', '��'));



            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN18XML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian18XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN18AFTERXML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian18AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN18BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian18BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN20XML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian20XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN20AFTERXML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian20AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN20BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian20BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN22XML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian22XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN22AFTERXML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian22AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN22BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian22BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("NIANMOQIANXML", objclsOperationEqipmentQtyXMLInsert.strNianMoQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NIANMOQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strNianMoQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NIANMOQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strNianMoQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHALIQIANXML", objclsOperationEqipmentQtyXMLInsert.strShaLiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHALIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strShaLiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHALIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strShaLiQianBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("FENGZHENXML", objclsOperationEqipmentQtyXMLInsert.strFengZhenXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FENGZHENAFTERXML", objclsOperationEqipmentQtyXMLInsert.strFengZhenAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FENGZHENBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strFengZhenBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOPIANXML", objclsOperationEqipmentQtyXMLInsert.strDaoPianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOPIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaoPianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOPIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaoPianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGCHIZHENQIAN25XML", objclsOperationEqipmentQtyXMLInsert.strChangChiZhenQian25XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGCHIZHENQIAN25AFTERXML", objclsOperationEqipmentQtyXMLInsert.strChangChiZhenQian25AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGCHIZHENQIAN25BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChangChiZhenQian25BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIJIAOQIANXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhiJiaoQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAZHIQIANXML", objclsOperationEqipmentQtyXMLInsert.strDaZhiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAZHIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaZhiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAZHIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaZhiQianBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN25XML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian25XML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN25AFTERXML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian25AfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN25BEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWanXueGuanQian25BeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAWANXUEGUANQIANXML", objclsOperationEqipmentQtyXMLInsert.strDaWanXueGuanQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAWANXUEGUANQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaWanXueGuanQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAWANXUEGUANQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaWanXueGuanQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHENDIQIANXML", objclsOperationEqipmentQtyXMLInsert.strShenDiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENDIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strShenDiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENDIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strShenDiQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGQIANZHIXML", objclsOperationEqipmentQtyXMLInsert.strChangQianZhiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANZHIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strChangQianZhiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANZHIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChangQianZhiBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("CHANGQIANWANXML", objclsOperationEqipmentQtyXMLInsert.strChangQianWanXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANWANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strChangQianWanAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANWANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChangQianWanBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHIQIANXML", objclsOperationEqipmentQtyXMLInsert.strShiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strShiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strShiQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WEIQIANXML", objclsOperationEqipmentQtyXMLInsert.strWeiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WEIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strWeiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WEIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWeiQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XINERQIANXML", objclsOperationEqipmentQtyXMLInsert.strXinErQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINERQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXinErQiaAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINERQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXinErQiaBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ERYANHOUCHONGXIQIXML", objclsOperationEqipmentQtyXMLInsert.strErYanHouChongXiQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ERYANHOUCHONGXIQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strErYanHouChongXiQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ERYANHOUCHONGXIQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strErYanHouChongXiQiBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("TANZHENCHUXML", objclsOperationEqipmentQtyXMLInsert.strTanZhenChuXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TANZHENCHUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strTanZhenChuAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TANZHENCHUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strTanZhenChuBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("TANZHENXIXML", objclsOperationEqipmentQtyXMLInsert.strTanZhenXiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TANZHENXIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strTanZhenXiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TANZHENXIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strTanZhenXiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DANDAOTANTIAOXML", objclsOperationEqipmentQtyXMLInsert.strDanDaoTanTiaoXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DANDAOTANTIAOAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDanDaoTanTiaoAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DANDAOTANTIAOBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDanDaoTanTiaoBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LEIGUQIANKAIQIXML", objclsOperationEqipmentQtyXMLInsert.strLeiGuQianKaiQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEIGUQIANKAIQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strLeiGuQianKaiQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEIGUQIANKAIQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strLeiGuQianKaiQiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("HELONGQIXML", objclsOperationEqipmentQtyXMLInsert.strHeLongQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HELONGQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strHeLongQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HELONGQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strHeLongQiBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("JIANJIAGULAGOUXML", objclsOperationEqipmentQtyXMLInsert.strJianJiaGuLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JIANJIAGULAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strJianJiaGuLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JIANJIAGULAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strJianJiaGuLaGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LEIGUQIZIXML", objclsOperationEqipmentQtyXMLInsert.strLeiGuQiZiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEIGUQIZIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strLeiGuQiZiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEIGUQIZIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strLeiGuQiZiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAGUJIANXML", objclsOperationEqipmentQtyXMLInsert.strDaGuJianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAGUJIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaGuJianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAGUJIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaGuJianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DIERLEIGUJIANXML", objclsOperationEqipmentQtyXMLInsert.strDiErLeiGuJianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIERLEIGUJIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDiErLeiGuJianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIERLEIGUJIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDiErLeiGuJianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FANGTOUYAOGUQIANXML", objclsOperationEqipmentQtyXMLInsert.strFangTouYaoGuQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FANGTOUYAOGUQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strFangTouYaoGuQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FANGTOUYAOGUQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strFangTouYaoGuQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YAOGUQIANXML", objclsOperationEqipmentQtyXMLInsert.strYaoGuQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YAOGUQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strYaoGuQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YAOGUQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strYaoGuQianBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("GUMOBOLIQIXML", objclsOperationEqipmentQtyXMLInsert.strGuMoBoLiQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUMOBOLIQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strGuMoBoLiQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUMOBOLIQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strGuMoBoLiQiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GUDAOXML", objclsOperationEqipmentQtyXMLInsert.strGuDaoXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUDAOAFTERXML", objclsOperationEqipmentQtyXMLInsert.strGuDaoAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUDAOBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strGuDaoBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GUZAOXML", objclsOperationEqipmentQtyXMLInsert.strGuZaoXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUZAOAFTERXML", objclsOperationEqipmentQtyXMLInsert.strGuZaoAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUZAOBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strGuZaoBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KUOSHIXML", objclsOperationEqipmentQtyXMLInsert.strKuoShiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KUOSHIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strKuoShiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KUOSHIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strKuoShiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GUCHUIXML", objclsOperationEqipmentQtyXMLInsert.strGuChuiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUCHUIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strGuChuiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUCHUIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strGuChuiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHIGUQIANXML", objclsOperationEqipmentQtyXMLInsert.strChiGuQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIGUQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strChiGuQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIGUQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChiGuQianBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("JINGGUQIZIXML", objclsOperationEqipmentQtyXMLInsert.strJingGuQiZiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINGGUQIZIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strJingGuQiZiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINGGUQIZIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strJingGuQiZiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DANCHILAGOUXML", objclsOperationEqipmentQtyXMLInsert.strDanChiLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DANCHILAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDanChiLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DANCHILAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDanChiLaGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LAOHUQIANXML", objclsOperationEqipmentQtyXMLInsert.strLaoHuQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LAOHUQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strLaoHuQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LAOHUQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strLaoHuQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("PINGHENGFUWEIQIANXML", objclsOperationEqipmentQtyXMLInsert.strPingHengFuWeiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PINGHENGFUWEIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strPingHengFuWeiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PINGHENGFUWEIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strPingHengFuWeiQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LUOSIQIZIXML", objclsOperationEqipmentQtyXMLInsert.strLuoSiQiZiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LUOSIQIZIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strLuoSiQiZiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LUOSIQIZIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strLuoSiQiZiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOXIANGQIXML", objclsOperationEqipmentQtyXMLInsert.strDaoXiangQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOXIANGQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaoXiangQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOXIANGQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaoXiangQiBeforeXML.Replace('\'', '��'));



            m_objXmlWriter.WriteAttributeString("ZHUIBANYAOGUQIANXML", objclsOperationEqipmentQtyXMLInsert.strZhuiBanYaoGuQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUIBANYAOGUQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhuiBanYaoGuQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUIBANYAOGUQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhuiBanYaoGuQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHUIHEQIANXML", objclsOperationEqipmentQtyXMLInsert.strShuiHeQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHUIHEQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strShuiHeQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHUIHEQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strShuiHeQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JINGTUJIANXML", objclsOperationEqipmentQtyXMLInsert.strJingTuJianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINGTUJIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strJingTuJianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINGTUJIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strJingTuJianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHUIBANBOLIQIXML", objclsOperationEqipmentQtyXMLInsert.strZhuiBanBoLiQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUIBANBOLIQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhuiBanBoLiQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUIBANBOLIQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhuiBanBoLiQiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JIANBOLIZIXML", objclsOperationEqipmentQtyXMLInsert.strJianBoLiZiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JIANBOLIZIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strJianBoLiZiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JIANBOLIZIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strJianBoLiZiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("QIANGZHUANGNIEXML", objclsOperationEqipmentQtyXMLInsert.strQiangZhuangNieXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QIANGZHUANGNIEAFTERXML", objclsOperationEqipmentQtyXMLInsert.strQiangZhuangNieAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QIANGZHUANGNIEBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strQiangZhuangNieBeforeXML.Replace('\'', '��'));



            m_objXmlWriter.WriteAttributeString("BAISHIQIANKAIQIXML", objclsOperationEqipmentQtyXMLInsert.strBaiShiQianKaiQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BAISHIQIANKAIQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strBaiShiQianKaiQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BAISHIQIANKAIQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strBaiShiQianKaiQiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KAILUZHUANXML", objclsOperationEqipmentQtyXMLInsert.strKaiLuZhuanXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KAILUZHUANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strKaiLuZhuanAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KAILUZHUANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strKaiLuZhuanBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("TOUPIJIANQIANXML", objclsOperationEqipmentQtyXMLInsert.strTouPiJianQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOUPIJIANQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strTouPiJianQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOUPIJIANQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strTouPiJianQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIANJUDAOYINZIXML", objclsOperationEqipmentQtyXMLInsert.strXianJuDaoYinZiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIANJUDAOYINZIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXianJuDaoYinZiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIANJUDAOYINZIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXianJuDaoYinZiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XINERLAGOUXML", objclsOperationEqipmentQtyXMLInsert.strXinErLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINERLAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXinErLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINERLAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXinErLaGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHIBANQIANXML", objclsOperationEqipmentQtyXMLInsert.strChiBanQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIBANQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strChiBanQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIBANQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChiBanQianBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("XINFANGLAGOUXML", objclsOperationEqipmentQtyXMLInsert.strXinFangLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINFANGLAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXinFangLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINFANGLAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXinFangLaGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("NAOMOGOUXML", objclsOperationEqipmentQtyXMLInsert.strNaoMoGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NAOMOGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strNaoMoGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NAOMOGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strNaoMoGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHUANCIZHENXML", objclsOperationEqipmentQtyXMLInsert.strChuanCiZhenXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHUANCIZHENAFTERXML", objclsOperationEqipmentQtyXMLInsert.strChuanCiZhenAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHUANCIZHENBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChuanCiZhenBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YINDINGQIANXML", objclsOperationEqipmentQtyXMLInsert.strYinDingQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YINDINGQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strYinDingQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YINDINGQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strYinDingQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FEIYEDANGBANXML", objclsOperationEqipmentQtyXMLInsert.strFeiYeDangBanXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FEIYEDANGBANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strFeiYeDangBanAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FEIYEDANGBANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strFeiYeDangBanBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHUAZHUDUANQIANXML", objclsOperationEqipmentQtyXMLInsert.strZhuAZhuDuanQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUAZHUDUANQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhuAZhuDuanQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUAZHUDUANQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhuAZhuDuanQianBeforeXML.Replace('\'', '��'));



            m_objXmlWriter.WriteAttributeString("ZHUAYOULIQIANXML", objclsOperationEqipmentQtyXMLInsert.strZhuAYouLiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUAYOULIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhuAYouLiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUAYOULIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhuAYouLiQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHUACEBIQIANXML", objclsOperationEqipmentQtyXMLInsert.strZhuACeBiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUACEBIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strZhuAYouLiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUACEBIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strZhuACeBiQiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ERJIANBANKUOZHANGQIXML", objclsOperationEqipmentQtyXMLInsert.strErJianBanKuoZhangQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ERJIANBANKUOZHANGQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strErJianBanKuoZhangQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ERJIANBANKUOZHANGQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strErJianBanKuoZhangQiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CEBANQIXML", objclsOperationEqipmentQtyXMLInsert.strCeBanQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CEBANQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strCeBanQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CEBANQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strCeBanQiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XINNEIZHIJIAOLAGOUXML", objclsOperationEqipmentQtyXMLInsert.strXinNeiZhiJiaoLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINNEIZHIJIAOLAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXinNeiZhiJiaoLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINNEIZHIJIAOLAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXinNeiZhiJiaoLaGouBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("DAOXIANGOUXML", objclsOperationEqipmentQtyXMLInsert.strDaoXianGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOXIANGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strDaoXianGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOXIANGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strDaoXianGouBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("WUCHUANGNIEXML", objclsOperationEqipmentQtyXMLInsert.strWuChuangNieXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUCHUANGNIEAFTERXML", objclsOperationEqipmentQtyXMLInsert.strWuChuangNieAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUCHUANGNIEBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWuChuangNieBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KACHIXML", objclsOperationEqipmentQtyXMLInsert.strKaChiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KACHIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strKaChiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KACHIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strKaChiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHENJINGLAGOUXML", objclsOperationEqipmentQtyXMLInsert.strShenJingLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENJINGLAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strShenJingLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENJINGLAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strShenJingLaGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XUEGUANJIAXML", objclsOperationEqipmentQtyXMLInsert.strXueGuanJiaXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XUEGUANJIAAFTERXML", objclsOperationEqipmentQtyXMLInsert.strXueGuanJiaAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XUEGUANJIABEFOREXML", objclsOperationEqipmentQtyXMLInsert.strXueGuanJiaBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FUKUIXML", objclsOperationEqipmentQtyXMLInsert.strFuKuiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUKUIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strFuKuiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUKUIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strFuKuiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GONGCHIXML", objclsOperationEqipmentQtyXMLInsert.strGongChiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGCHIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strGongChiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGCHIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strGongChiBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("GONGGUASHIXML", objclsOperationEqipmentQtyXMLInsert.strGongGuaShiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGGUASHIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strGongGuaShiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGGUASHIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strGongGuaShiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GONGJINGQIANXML", objclsOperationEqipmentQtyXMLInsert.strGongJingQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGJINGQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strGongJingQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGJINGQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strGongJingQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YINDAOLAGOUXML", objclsOperationEqipmentQtyXMLInsert.strYinDaoLaGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YINDAOLAGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strYinDaoLaGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YINDAOLAGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strYinDaoLaGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("RENDAIQIANXML", objclsOperationEqipmentQtyXMLInsert.strRenDaiQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RENDAIQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strRenDaiQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RENDAIQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strRenDaiQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JILIUBOLIZIXML", objclsOperationEqipmentQtyXMLInsert.strJiLiuBoLiZiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JILIUBOLIZIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strJiLiuBoLiZiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JILIUBOLIZIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strJiLiuBoLiZiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KUOGONGQIXML", objclsOperationEqipmentQtyXMLInsert.strKuoGongQiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KUOGONGQIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strKuoGongQiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KUOGONGQIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strKuoGongQiBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("JINSHUNIAOGOUXML", objclsOperationEqipmentQtyXMLInsert.strJinShuNiaoGouXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINSHUNIAOGOUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strJinShuNiaoGouAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINSHUNIAOGOUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strJinShuNiaoGouBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FUGUOQIANXML", objclsOperationEqipmentQtyXMLInsert.strFuGuoQianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUGUOQIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strFuGuoQianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUGUOQIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strFuGuoQianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YOUDAIFANGDIANXML", objclsOperationEqipmentQtyXMLInsert.strYouDaiFangDianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUDAIFANGDIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strYouDaiFangDianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUDAIFANGDIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strYouDaiFangDianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WUDAIFANGDIANXML", objclsOperationEqipmentQtyXMLInsert.strWuDaiFangDianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUDAIFANGDIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strWuDaiFangDianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUDAIFANGDIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWuDaiFangDianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YOUDAICHANGDIANXML", objclsOperationEqipmentQtyXMLInsert.strYouDaiChangDianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUDAICHANGDIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strYouDaiChangDianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUDAICHANGDIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strYouDaiChangDianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WUDAICHANGDIANXML", objclsOperationEqipmentQtyXMLInsert.strWuDaiChangDianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUDAICHANGDIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strWuDaiChangDianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUDAICHANGDIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWuDaiChangDianBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("FUNIEYINLIUXML", objclsOperationEqipmentQtyXMLInsert.strFuNieYinLiuXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUNIEYINLIUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strFuNieYinLiuAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUNIEYINLIUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strFuNieYinLiuBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KAILUMIANXML", objclsOperationEqipmentQtyXMLInsert.strKaiLuMianXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KAILUMIANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strKaiLuMianAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KAILUMIANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strKaiLuMianBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("QUANGONGSHAXML", objclsOperationEqipmentQtyXMLInsert.strQuanGongShaXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUANGONGSHAAFTERXML", objclsOperationEqipmentQtyXMLInsert.strQuanGongShaAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUANGONGSHABEFOREXML", objclsOperationEqipmentQtyXMLInsert.strQuanGongShaBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WANGSHAXML", objclsOperationEqipmentQtyXMLInsert.strWangShaXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANGSHAAFTERXML", objclsOperationEqipmentQtyXMLInsert.strWangShaAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANGSHABEFOREXML", objclsOperationEqipmentQtyXMLInsert.strWangShaBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHAKUAIXML", objclsOperationEqipmentQtyXMLInsert.strShaKuaiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHAKUAIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strShaKuaiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHAKUAIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strShaKuaiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHAQIUXML", objclsOperationEqipmentQtyXMLInsert.strShaQiuXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHAQIUAFTERXML", objclsOperationEqipmentQtyXMLInsert.strShaQiuAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHAQIUBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strShaQiuBeforeXML.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("BIANDAIXML", objclsOperationEqipmentQtyXMLInsert.strBianDaiXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIANDAIAFTERXML", objclsOperationEqipmentQtyXMLInsert.strBianDaiAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIANDAIBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strBianDaiBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGQIANTAOXML", objclsOperationEqipmentQtyXMLInsert.strChangQianTaoXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANTAOAFTERXML", objclsOperationEqipmentQtyXMLInsert.strChangQianTaoAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANTAOBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strChangQianTaoBeforeXML.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("NIAOGUANXML", objclsOperationEqipmentQtyXMLInsert.strNiaoGuanXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NIAOGUANAFTERXML", objclsOperationEqipmentQtyXMLInsert.strNiaoGuanAfterXML.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NIAOGUANBEFOREXML", objclsOperationEqipmentQtyXMLInsert.strNiaoGuanBeforeXML.Replace('\'', '��'));










            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);

        }


        #endregion

        #region ƴ�ӱ��XML
        /// <summary>
        /// ƴ�ӱ��XML
        /// </summary>
        /// <param name="objclsOperationEqipmentQtyXML"></param>
        /// <returns></returns>
        private string m_strGetContentXML(clsOperationEqipmentQtyContent objclsOperationEqipmentQtyContent)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordMaster");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", objclsOperationEqipmentQtyContent.strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", objclsOperationEqipmentQtyContent.strInPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPENDATE", objclsOperationEqipmentQtyContent.strOpenDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MODIFYUSERID", objclsOperationEqipmentQtyContent.strModifyUserID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("MODIFYDATE", objclsOperationEqipmentQtyContent.strModifyDate.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("OPERATIONID", objclsOperationEqipmentQtyContent.strOperationID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPERATIONNAME", objclsOperationEqipmentQtyContent.strOperationName.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WENZHI125", objclsOperationEqipmentQtyContent.strWenZhi125.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WENZHI125AFTER", objclsOperationEqipmentQtyContent.strWenZhi125After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WENZHI125BEFORE", objclsOperationEqipmentQtyContent.strWenZhi125Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WENWAN125", objclsOperationEqipmentQtyContent.strWenWan125.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WENWAN125AFTER", objclsOperationEqipmentQtyContent.strWenWan125After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WENWAN125BEFORE", objclsOperationEqipmentQtyContent.strWenWan125Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIAOZHI14", objclsOperationEqipmentQtyContent.strXiaoZhi14.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAOZHI14AFTER", objclsOperationEqipmentQtyContent.strXiaoZhi14After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAOZHI14BEFORE", objclsOperationEqipmentQtyContent.strXiaoZhi14Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIAOWAN14", objclsOperationEqipmentQtyContent.strXiaoWan14.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAOWAN14AFTER", objclsOperationEqipmentQtyContent.strXiaoWan14After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAOWAN14BEFORE", objclsOperationEqipmentQtyContent.strXiaoWan14Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHONGZHI16", objclsOperationEqipmentQtyContent.strZhongZhi16.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGZHI16AFTER", objclsOperationEqipmentQtyContent.strZhongZhi16After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGZHI16BEFORE", objclsOperationEqipmentQtyContent.strZhongZhi16Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHONGWAN16", objclsOperationEqipmentQtyContent.strZhongWan16.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGWAN16AFTER", objclsOperationEqipmentQtyContent.strZhongWan16After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGWAN16BEFORE", objclsOperationEqipmentQtyContent.strZhongWan16Before.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("PIQIAN", objclsOperationEqipmentQtyContent.strPiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PIQIANAFTER", objclsOperationEqipmentQtyContent.strPiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PIQIANBEFORE", objclsOperationEqipmentQtyContent.strPiQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIYOUCHIXUEGUANQIAN", objclsOperationEqipmentQtyContent.strZhiYouChiXueGuanQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIYOUCHIXUEGUANQIANAFTER", objclsOperationEqipmentQtyContent.strZhiYouChiXueGuanQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIYOUCHIXUEGUANQIANBEFORE", objclsOperationEqipmentQtyContent.strZhiYouChiXueGuanQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("QUANQIAN", objclsOperationEqipmentQtyContent.strQuanQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUANQIANAFTER", objclsOperationEqipmentQtyContent.strQuanQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUANQIANBEFORE", objclsOperationEqipmentQtyContent.strQuanQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JINQIAN", objclsOperationEqipmentQtyContent.strJinQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINQIANAFTER", objclsOperationEqipmentQtyContent.strJinQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINQIANBEFORE", objclsOperationEqipmentQtyContent.strJinQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHIZHENQIAN18", objclsOperationEqipmentQtyContent.strChiZhenQian18.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIZHENQIAN18AFTER", objclsOperationEqipmentQtyContent.strChiZhenQian18After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIZHENQIAN18BEFORE", objclsOperationEqipmentQtyContent.strChiZhenQian18Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YOUCHINIE", objclsOperationEqipmentQtyContent.strYouChiNie.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUCHINIEAFTER", objclsOperationEqipmentQtyContent.strYouChiNieAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUCHINIEBEFORE", objclsOperationEqipmentQtyContent.strYouChiNieBefore.Replace('\'', '��'));



            m_objXmlWriter.WriteAttributeString("WUCHINIE", objclsOperationEqipmentQtyContent.strWuChiNie.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUCHINIEAFTER", objclsOperationEqipmentQtyContent.strWuChiNieAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUCHINIEBEFORE", objclsOperationEqipmentQtyContent.strWuChiNieBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGYABAN", objclsOperationEqipmentQtyContent.strChangYaBan.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGYABANAFTER", objclsOperationEqipmentQtyContent.strChangYaBanAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGYABANBEFORE", objclsOperationEqipmentQtyContent.strChangYaBanBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOBING3", objclsOperationEqipmentQtyContent.strDaoBing3.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING3AFTER", objclsOperationEqipmentQtyContent.strDaoBing3After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING3BEFORE", objclsOperationEqipmentQtyContent.strDaoBing3Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOBING4", objclsOperationEqipmentQtyContent.strDaoBing4.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING4AFTER", objclsOperationEqipmentQtyContent.strDaoBing4After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING4BEFORE", objclsOperationEqipmentQtyContent.strDaoBing4Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOBING7", objclsOperationEqipmentQtyContent.strDaoBing7.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING7AFTER", objclsOperationEqipmentQtyContent.strDaoBing7After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOBING7BEFORE", objclsOperationEqipmentQtyContent.strDaoBing7Before.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("WANZHUZHIJIAN", objclsOperationEqipmentQtyContent.strWanZhuZhiJian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANZHUZHIJIANAFTER", objclsOperationEqipmentQtyContent.strWanZhuZhiJianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANZHUZHIJIANBEFORE", objclsOperationEqipmentQtyContent.strWanZhuZhiJianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIZHUZHIJIAN", objclsOperationEqipmentQtyContent.strZhiZhuZhiJian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIZHUZHIJIANAFTER", objclsOperationEqipmentQtyContent.strZhiZhuZhiJianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIZHUZHIJIANBEFORE", objclsOperationEqipmentQtyContent.strZhiZhuZhiJianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("BIANTAOXIANJIAN", objclsOperationEqipmentQtyContent.strBianTaoXianJian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIANTAOXIANJIANAFTER", objclsOperationEqipmentQtyContent.strBianTaoXianJianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIANTAOXIANJIANBEFORE", objclsOperationEqipmentQtyContent.strBianTaoXianJianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIONGQIANGJIAN", objclsOperationEqipmentQtyContent.strXiongQiangJian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIONGQIANGJIANAFTER", objclsOperationEqipmentQtyContent.strXiongQiangJianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIONGQIANGJIANBEFORE", objclsOperationEqipmentQtyContent.strXiongQiangJianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIJIAOXIAOLAGOU", objclsOperationEqipmentQtyContent.strZhiJiaoXiaoLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOXIAOLAGOUAFTER", objclsOperationEqipmentQtyContent.strZhiJiaoXiaoLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOXIAOLAGOUBEFORE", objclsOperationEqipmentQtyContent.strZhiJiaoXiaoLaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LANWEILAGOU", objclsOperationEqipmentQtyContent.strLanWeiLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LANWEILAGOUAFTER", objclsOperationEqipmentQtyContent.strLanWeiLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LANWEILAGOUBEFORE", objclsOperationEqipmentQtyContent.strLanWeiLaGouBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("ZHONGFUGOU", objclsOperationEqipmentQtyContent.strZhongFuGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGFUGOUAFTER", objclsOperationEqipmentQtyContent.strZhongFuGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHONGFUGOUBEFORE", objclsOperationEqipmentQtyContent.strZhongFuGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGYAGOU", objclsOperationEqipmentQtyContent.strChangYaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGYAGOUAFTER", objclsOperationEqipmentQtyContent.strChangYaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGYAGOUBEFORE", objclsOperationEqipmentQtyContent.strChangYaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIJIAOGOU", objclsOperationEqipmentQtyContent.strZhiJiaoGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOGOUAFTER", objclsOperationEqipmentQtyContent.strZhiJiaoGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOGOUBEFORE", objclsOperationEqipmentQtyContent.strZhiJiaoGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIAFUBUQIANKAIQI", objclsOperationEqipmentQtyContent.strXiaFuBuQianKaiQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAFUBUQIANKAIQIAFTER", objclsOperationEqipmentQtyContent.strXiaFuBuQianKaiQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIAFUBUQIANKAIQIBEFORE", objclsOperationEqipmentQtyContent.strXiaFuBuQianKaiQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("TONGQUAN", objclsOperationEqipmentQtyContent.strTongQuan.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TONGQUANAFTER", objclsOperationEqipmentQtyContent.strTongQuanAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TONGQUANBEFORE", objclsOperationEqipmentQtyContent.strTongQuanBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIYEGUAN", objclsOperationEqipmentQtyContent.strXiYeGuan.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIYEGUANAFTER", objclsOperationEqipmentQtyContent.strXiYeGuanAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIYEGUANBEFORE", objclsOperationEqipmentQtyContent.strXiYeGuanBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHIJIAOQIAN", objclsOperationEqipmentQtyContent.strZhiJiaoQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOQIANAFTER", objclsOperationEqipmentQtyContent.strZhiJiaoQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHIJIAOQIANBEFORE", objclsOperationEqipmentQtyContent.strZhiJiaoQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAZHIQIAN", objclsOperationEqipmentQtyContent.strDaZhiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAZHIQIANAFTER", objclsOperationEqipmentQtyContent.strDaZhiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAZHIQIANBEFORE", objclsOperationEqipmentQtyContent.strDaZhiQianBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("FENGZHEN", objclsOperationEqipmentQtyContent.strFengZhen.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FENGZHENAFTER", objclsOperationEqipmentQtyContent.strFengZhenAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FENGZHENBEFORE", objclsOperationEqipmentQtyContent.strFengZhenBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOPIAN", objclsOperationEqipmentQtyContent.strDaoPian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOPIANAFTER", objclsOperationEqipmentQtyContent.strDaoPianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOPIANBEFORE", objclsOperationEqipmentQtyContent.strDaoPianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN18", objclsOperationEqipmentQtyContent.strWanXueGuanQian18.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN18AFTER", objclsOperationEqipmentQtyContent.strWanXueGuanQian18After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN18BEFORE", objclsOperationEqipmentQtyContent.strWanXueGuanQian18Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN20", objclsOperationEqipmentQtyContent.strWanXueGuanQian20.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN20AFTER", objclsOperationEqipmentQtyContent.strWanXueGuanQian20After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN20BEFORE", objclsOperationEqipmentQtyContent.strWanXueGuanQian20Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN22", objclsOperationEqipmentQtyContent.strWanXueGuanQian22.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN22AFTER", objclsOperationEqipmentQtyContent.strWanXueGuanQian22After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN22BEFORE", objclsOperationEqipmentQtyContent.strWanXueGuanQian22Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN25", objclsOperationEqipmentQtyContent.strWanXueGuanQian25.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN25AFTER", objclsOperationEqipmentQtyContent.strWanXueGuanQian25After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANXUEGUANQIAN25BEFORE", objclsOperationEqipmentQtyContent.strWanXueGuanQian25Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGCHIZHENQIAN25", objclsOperationEqipmentQtyContent.strChangChiZhenQian25.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGCHIZHENQIAN25AFTER", objclsOperationEqipmentQtyContent.strChangChiZhenQian25After.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGCHIZHENQIAN25BEFORE", objclsOperationEqipmentQtyContent.strChangChiZhenQian25Before.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("NIANMOQIAN", objclsOperationEqipmentQtyContent.strNianMoQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NIANMOQIANAFTER", objclsOperationEqipmentQtyContent.strNianMoQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NIANMOQIANBEFORE", objclsOperationEqipmentQtyContent.strNianMoQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHALIQIAN", objclsOperationEqipmentQtyContent.strShaLiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHALIQIANAFTER", objclsOperationEqipmentQtyContent.strShaLiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHALIQIANBEFORE", objclsOperationEqipmentQtyContent.strShaLiQianBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("DAWANXUEGUANQIAN", objclsOperationEqipmentQtyContent.strDaWanXueGuanQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAWANXUEGUANQIANAFTER", objclsOperationEqipmentQtyContent.strDaWanXueGuanQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAWANXUEGUANQIANBEFORE", objclsOperationEqipmentQtyContent.strDaWanXueGuanQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHENDIQIAN", objclsOperationEqipmentQtyContent.strShenDiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENDIQIANAFTER", objclsOperationEqipmentQtyContent.strShenDiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENDIQIANBEFORE", objclsOperationEqipmentQtyContent.strShenDiQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGQIANZHI", objclsOperationEqipmentQtyContent.strChangQianZhi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANZHIAFTER", objclsOperationEqipmentQtyContent.strChangQianZhiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANZHIBEFORE", objclsOperationEqipmentQtyContent.strChangQianZhiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGQIANWAN", objclsOperationEqipmentQtyContent.strChangQianWan.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANWANAFTER", objclsOperationEqipmentQtyContent.strChangQianWanAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANWANBEFORE", objclsOperationEqipmentQtyContent.strChangQianWanBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHIQIAN", objclsOperationEqipmentQtyContent.strShiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHIQIANAFTER", objclsOperationEqipmentQtyContent.strShiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHIQIANBEFORE", objclsOperationEqipmentQtyContent.strShiQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WEIQIAN", objclsOperationEqipmentQtyContent.strWeiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WEIQIANAFTER", objclsOperationEqipmentQtyContent.strWeiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WEIQIANBEFORE", objclsOperationEqipmentQtyContent.strWeiQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XINERQIAN", objclsOperationEqipmentQtyContent.strXinErQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINERQIANAFTER", objclsOperationEqipmentQtyContent.strXinErQiaAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINERQIANBEFORE", objclsOperationEqipmentQtyContent.strXinErQiaBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ERYANHOUCHONGXIQI", objclsOperationEqipmentQtyContent.strErYanHouChongXiQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ERYANHOUCHONGXIQIAFTER", objclsOperationEqipmentQtyContent.strErYanHouChongXiQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ERYANHOUCHONGXIQIBEFORE", objclsOperationEqipmentQtyContent.strErYanHouChongXiQiBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("TANZHENCHU", objclsOperationEqipmentQtyContent.strTanZhenChu.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TANZHENCHUAFTER", objclsOperationEqipmentQtyContent.strTanZhenChuAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TANZHENCHUBEFORE", objclsOperationEqipmentQtyContent.strTanZhenChuBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("TANZHENXI", objclsOperationEqipmentQtyContent.strTanZhenXi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TANZHENXIAFTER", objclsOperationEqipmentQtyContent.strTanZhenXiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TANZHENXIBEFORE", objclsOperationEqipmentQtyContent.strTanZhenXiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DANDAOTANTIAO", objclsOperationEqipmentQtyContent.strDanDaoTanTiao.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DANDAOTANTIAOAFTER", objclsOperationEqipmentQtyContent.strDanDaoTanTiaoAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DANDAOTANTIAOBEFORE", objclsOperationEqipmentQtyContent.strDanDaoTanTiaoBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LEIGUQIANKAIQI", objclsOperationEqipmentQtyContent.strLeiGuQianKaiQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEIGUQIANKAIQIAFTER", objclsOperationEqipmentQtyContent.strLeiGuQianKaiQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEIGUQIANKAIQIBEFORE", objclsOperationEqipmentQtyContent.strLeiGuQianKaiQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("HELONGQI", objclsOperationEqipmentQtyContent.strHeLongQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HELONGQIAFTER", objclsOperationEqipmentQtyContent.strHeLongQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("HELONGQIBEFORE", objclsOperationEqipmentQtyContent.strHeLongQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JIANJIAGULAGOU", objclsOperationEqipmentQtyContent.strJianJiaGuLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JIANJIAGULAGOUAFTER", objclsOperationEqipmentQtyContent.strJianJiaGuLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JIANJIAGULAGOUBEFORE", objclsOperationEqipmentQtyContent.strJianJiaGuLaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LEIGUQIZI", objclsOperationEqipmentQtyContent.strLeiGuQiZi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEIGUQIZIAFTER", objclsOperationEqipmentQtyContent.strLeiGuQiZiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LEIGUQIZIBEFORE", objclsOperationEqipmentQtyContent.strLeiGuQiZiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAGUJIAN", objclsOperationEqipmentQtyContent.strDaGuJian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAGUJIANAFTER", objclsOperationEqipmentQtyContent.strDaGuJianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAGUJIANBEFORE", objclsOperationEqipmentQtyContent.strDaGuJianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DIERLEIGUJIAN", objclsOperationEqipmentQtyContent.strDiErLeiGuJian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIERLEIGUJIANAFTER", objclsOperationEqipmentQtyContent.strDiErLeiGuJianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DIERLEIGUJIANBEFORE", objclsOperationEqipmentQtyContent.strDiErLeiGuJianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FANGTOUYAOGUQIAN", objclsOperationEqipmentQtyContent.strFangTouYaoGuQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FANGTOUYAOGUQIANAFTER", objclsOperationEqipmentQtyContent.strFangTouYaoGuQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FANGTOUYAOGUQIANBEFORE", objclsOperationEqipmentQtyContent.strFangTouYaoGuQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YAOGUQIAN", objclsOperationEqipmentQtyContent.strYaoGuQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YAOGUQIANAFTER", objclsOperationEqipmentQtyContent.strYaoGuQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YAOGUQIANBEFORE", objclsOperationEqipmentQtyContent.strYaoGuQianBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("GUMOBOLIQI", objclsOperationEqipmentQtyContent.strGuMoBoLiQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUMOBOLIQIAFTER", objclsOperationEqipmentQtyContent.strGuMoBoLiQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUMOBOLIQIBEFORE", objclsOperationEqipmentQtyContent.strGuMoBoLiQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GUDAO", objclsOperationEqipmentQtyContent.strGuDao.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUDAOAFTER", objclsOperationEqipmentQtyContent.strGuDaoAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUDAOBEFORE", objclsOperationEqipmentQtyContent.strGuDaoBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GUZAO", objclsOperationEqipmentQtyContent.strGuZao.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUZAOAFTER", objclsOperationEqipmentQtyContent.strGuZaoAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUZAOBEFORE", objclsOperationEqipmentQtyContent.strGuZaoBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KUOSHI", objclsOperationEqipmentQtyContent.strKuoShi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KUOSHIAFTER", objclsOperationEqipmentQtyContent.strKuoShiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KUOSHIBEFORE", objclsOperationEqipmentQtyContent.strKuoShiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GUCHUI", objclsOperationEqipmentQtyContent.strGuChui.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUCHUIAFTER", objclsOperationEqipmentQtyContent.strGuChuiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GUCHUIBEFORE", objclsOperationEqipmentQtyContent.strGuChuiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHIGUQIAN", objclsOperationEqipmentQtyContent.strChiGuQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIGUQIANAFTER", objclsOperationEqipmentQtyContent.strChiGuQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIGUQIANBEFORE", objclsOperationEqipmentQtyContent.strChiGuQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JINGGUQIZI", objclsOperationEqipmentQtyContent.strJingGuQiZi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINGGUQIZIAFTER", objclsOperationEqipmentQtyContent.strJingGuQiZiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINGGUQIZIBEFORE", objclsOperationEqipmentQtyContent.strJingGuQiZiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DANCHILAGOU", objclsOperationEqipmentQtyContent.strDanChiLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DANCHILAGOUAFTER", objclsOperationEqipmentQtyContent.strDanChiLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DANCHILAGOUBEFORE", objclsOperationEqipmentQtyContent.strDanChiLaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LAOHUQIAN", objclsOperationEqipmentQtyContent.strLaoHuQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LAOHUQIANAFTER", objclsOperationEqipmentQtyContent.strLaoHuQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LAOHUQIANBEFORE", objclsOperationEqipmentQtyContent.strLaoHuQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("PINGHENGFUWEIQIAN", objclsOperationEqipmentQtyContent.strPingHengFuWeiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PINGHENGFUWEIQIANAFTER", objclsOperationEqipmentQtyContent.strPingHengFuWeiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("PINGHENGFUWEIQIANBEFORE", objclsOperationEqipmentQtyContent.strPingHengFuWeiQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("LUOSIQIZI", objclsOperationEqipmentQtyContent.strLuoSiQiZi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LUOSIQIZIAFTER", objclsOperationEqipmentQtyContent.strLuoSiQiZiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("LUOSIQIZIBEFORE", objclsOperationEqipmentQtyContent.strLuoSiQiZiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOXIANGQI", objclsOperationEqipmentQtyContent.strDaoXiangQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOXIANGQIAFTER", objclsOperationEqipmentQtyContent.strDaoXiangQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOXIANGQIBEFORE", objclsOperationEqipmentQtyContent.strDaoXiangQiBefore.Replace('\'', '��'));



            m_objXmlWriter.WriteAttributeString("ZHUIBANYAOGUQIAN", objclsOperationEqipmentQtyContent.strZhuiBanYaoGuQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUIBANYAOGUQIANAFTER", objclsOperationEqipmentQtyContent.strZhuiBanYaoGuQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUIBANYAOGUQIANBEFORE", objclsOperationEqipmentQtyContent.strZhuiBanYaoGuQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHUIHEQIAN", objclsOperationEqipmentQtyContent.strShuiHeQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHUIHEQIANAFTER", objclsOperationEqipmentQtyContent.strShuiHeQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHUIHEQIANBEFORE", objclsOperationEqipmentQtyContent.strShuiHeQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JINGTUJIAN", objclsOperationEqipmentQtyContent.strJingTuJian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINGTUJIANAFTER", objclsOperationEqipmentQtyContent.strJingTuJianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINGTUJIANBEFORE", objclsOperationEqipmentQtyContent.strJingTuJianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHUIBANBOLIQI", objclsOperationEqipmentQtyContent.strZhuiBanBoLiQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUIBANBOLIQIAFTER", objclsOperationEqipmentQtyContent.strZhuiBanBoLiQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUIBANBOLIQIBEFORE", objclsOperationEqipmentQtyContent.strZhuiBanBoLiQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JIANBOLIZI", objclsOperationEqipmentQtyContent.strJianBoLiZi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JIANBOLIZIAFTER", objclsOperationEqipmentQtyContent.strJianBoLiZiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JIANBOLIZIBEFORE", objclsOperationEqipmentQtyContent.strJianBoLiZiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("QIANGZHUANGNIE", objclsOperationEqipmentQtyContent.strQiangZhuangNie.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QIANGZHUANGNIEAFTER", objclsOperationEqipmentQtyContent.strQiangZhuangNieAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QIANGZHUANGNIEBEFORE", objclsOperationEqipmentQtyContent.strQiangZhuangNieBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("BAISHIQIANKAIQI", objclsOperationEqipmentQtyContent.strBaiShiQianKaiQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BAISHIQIANKAIQIAFTER", objclsOperationEqipmentQtyContent.strBaiShiQianKaiQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BAISHIQIANKAIQIBEFORE", objclsOperationEqipmentQtyContent.strBaiShiQianKaiQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KAILUZHUAN", objclsOperationEqipmentQtyContent.strKaiLuZhuan.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KAILUZHUANAFTER", objclsOperationEqipmentQtyContent.strKaiLuZhuanAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KAILUZHUANBEFORE", objclsOperationEqipmentQtyContent.strKaiLuZhuanBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("TOUPIJIANQIAN", objclsOperationEqipmentQtyContent.strTouPiJianQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOUPIJIANQIANAFTER", objclsOperationEqipmentQtyContent.strTouPiJianQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("TOUPIJIANQIANBEFORE", objclsOperationEqipmentQtyContent.strTouPiJianQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XIANJUDAOYINZI", objclsOperationEqipmentQtyContent.strXianJuDaoYinZi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIANJUDAOYINZIAFTER", objclsOperationEqipmentQtyContent.strXianJuDaoYinZiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XIANJUDAOYINZIBEFORE", objclsOperationEqipmentQtyContent.strXianJuDaoYinZiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XINERLAGOU", objclsOperationEqipmentQtyContent.strXinErLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINERLAGOUAFTER", objclsOperationEqipmentQtyContent.strXinErLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINERLAGOUBEFORE", objclsOperationEqipmentQtyContent.strXinErLaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHIBANQIAN", objclsOperationEqipmentQtyContent.strChiBanQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIBANQIANAFTER", objclsOperationEqipmentQtyContent.strChiBanQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHIBANQIANBEFORE", objclsOperationEqipmentQtyContent.strChiBanQianBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("XINFANGLAGOU", objclsOperationEqipmentQtyContent.strXinFangLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINFANGLAGOUAFTER", objclsOperationEqipmentQtyContent.strXinFangLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINFANGLAGOUBEFORE", objclsOperationEqipmentQtyContent.strXinFangLaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("NAOMOGOU", objclsOperationEqipmentQtyContent.strNaoMoGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NAOMOGOUAFTER", objclsOperationEqipmentQtyContent.strNaoMoGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NAOMOGOUBEFORE", objclsOperationEqipmentQtyContent.strNaoMoGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHUANCIZHEN", objclsOperationEqipmentQtyContent.strChuanCiZhen.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHUANCIZHENAFTER", objclsOperationEqipmentQtyContent.strChuanCiZhenAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHUANCIZHENBEFORE", objclsOperationEqipmentQtyContent.strChuanCiZhenBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YINDINGQIAN", objclsOperationEqipmentQtyContent.strYinDingQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YINDINGQIANAFTER", objclsOperationEqipmentQtyContent.strYinDingQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YINDINGQIANBEFORE", objclsOperationEqipmentQtyContent.strYinDingQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FEIYEDANGBAN", objclsOperationEqipmentQtyContent.strFeiYeDangBan.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FEIYEDANGBANAFTER", objclsOperationEqipmentQtyContent.strFeiYeDangBanAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FEIYEDANGBANBEFORE", objclsOperationEqipmentQtyContent.strFeiYeDangBanBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHUAZHUDUANQIAN", objclsOperationEqipmentQtyContent.strZhuAZhuDuanQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUAZHUDUANQIANAFTER", objclsOperationEqipmentQtyContent.strZhuAZhuDuanQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUAZHUDUANQIANBEFORE", objclsOperationEqipmentQtyContent.strZhuAZhuDuanQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHUAYOULIQIAN", objclsOperationEqipmentQtyContent.strZhuAYouLiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUAYOULIQIANAFTER", objclsOperationEqipmentQtyContent.strZhuAYouLiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUAYOULIQIANBEFORE", objclsOperationEqipmentQtyContent.strZhuAYouLiQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ZHUACEBIQIAN", objclsOperationEqipmentQtyContent.strZhuACeBiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUACEBIQIANAFTER", objclsOperationEqipmentQtyContent.strZhuACeBiQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ZHUACEBIQIANBEFORE", objclsOperationEqipmentQtyContent.strZhuACeBiQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("ERJIANBANKUOZHANGQI", objclsOperationEqipmentQtyContent.strErJianBanKuoZhangQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ERJIANBANKUOZHANGQIAFTER", objclsOperationEqipmentQtyContent.strErJianBanKuoZhangQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("ERJIANBANKUOZHANGQIBEFORE", objclsOperationEqipmentQtyContent.strErJianBanKuoZhangQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CEBANQI", objclsOperationEqipmentQtyContent.strCeBanQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CEBANQIAFTER", objclsOperationEqipmentQtyContent.strCeBanQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CEBANQIBEFORE", objclsOperationEqipmentQtyContent.strCeBanQiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XINNEIZHIJIAOLAGOU", objclsOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINNEIZHIJIAOLAGOUAFTER", objclsOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XINNEIZHIJIAOLAGOUBEFORE", objclsOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("DAOXIANGOU", objclsOperationEqipmentQtyContent.strDaoXianGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOXIANGOUAFTER", objclsOperationEqipmentQtyContent.strDaoXianGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("DAOXIANGOUBEFORE", objclsOperationEqipmentQtyContent.strDaoXianGouBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("WUCHUANGNIE", objclsOperationEqipmentQtyContent.strWuChuangNie.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUCHUANGNIEAFTER", objclsOperationEqipmentQtyContent.strWuChuangNieAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUCHUANGNIEBEFORE", objclsOperationEqipmentQtyContent.strWuChuangNieBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KACHI", objclsOperationEqipmentQtyContent.strKaChi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KACHIAFTER", objclsOperationEqipmentQtyContent.strKaChiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KACHIBEFORE", objclsOperationEqipmentQtyContent.strKaChiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHENJINGLAGOU", objclsOperationEqipmentQtyContent.strShenJingLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENJINGLAGOUAFTER", objclsOperationEqipmentQtyContent.strShenJingLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHENJINGLAGOUBEFORE", objclsOperationEqipmentQtyContent.strShenJingLaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("XUEGUANJIA", objclsOperationEqipmentQtyContent.strXueGuanJia.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XUEGUANJIAAFTER", objclsOperationEqipmentQtyContent.strXueGuanJiaAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("XUEGUANJIABEFORE", objclsOperationEqipmentQtyContent.strXueGuanJiaBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FUKUI", objclsOperationEqipmentQtyContent.strFuKui.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUKUIAFTER", objclsOperationEqipmentQtyContent.strFuKuiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUKUIBEFORE", objclsOperationEqipmentQtyContent.strFuKuiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GONGCHI", objclsOperationEqipmentQtyContent.strGongChi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGCHIAFTER", objclsOperationEqipmentQtyContent.strGongChiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGCHIBEFORE", objclsOperationEqipmentQtyContent.strGongChiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GONGGUASHI", objclsOperationEqipmentQtyContent.strGongGuaShi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGGUASHIAFTER", objclsOperationEqipmentQtyContent.strGongGuaShiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGGUASHIBEFORE", objclsOperationEqipmentQtyContent.strGongGuaShiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("GONGJINGQIAN", objclsOperationEqipmentQtyContent.strGongJingQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGJINGQIANAFTER", objclsOperationEqipmentQtyContent.strGongJingQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("GONGJINGQIANBEFORE", objclsOperationEqipmentQtyContent.strGongJingQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YINDAOLAGOU", objclsOperationEqipmentQtyContent.strYinDaoLaGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YINDAOLAGOUAFTER", objclsOperationEqipmentQtyContent.strYinDaoLaGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YINDAOLAGOUBEFORE", objclsOperationEqipmentQtyContent.strYinDaoLaGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("RENDAIQIAN", objclsOperationEqipmentQtyContent.strRenDaiQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RENDAIQIANAFTER", objclsOperationEqipmentQtyContent.strRenDaiQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("RENDAIQIANBEFORE", objclsOperationEqipmentQtyContent.strRenDaiQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("JILIUBOLIZI", objclsOperationEqipmentQtyContent.strJiLiuBoLiZi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JILIUBOLIZIAFTER", objclsOperationEqipmentQtyContent.strJiLiuBoLiZiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JILIUBOLIZIBEFORE", objclsOperationEqipmentQtyContent.strJiLiuBoLiZiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KUOGONGQI", objclsOperationEqipmentQtyContent.strKuoGongQi.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KUOGONGQIAFTER", objclsOperationEqipmentQtyContent.strKuoGongQiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KUOGONGQIBEFORE", objclsOperationEqipmentQtyContent.strKuoGongQiBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("JINSHUNIAOGOU", objclsOperationEqipmentQtyContent.strJinShuNiaoGou.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINSHUNIAOGOUAFTER", objclsOperationEqipmentQtyContent.strJinShuNiaoGouAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("JINSHUNIAOGOUBEFORE", objclsOperationEqipmentQtyContent.strJinShuNiaoGouBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FUGUOQIAN", objclsOperationEqipmentQtyContent.strFuGuoQian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUGUOQIANAFTER", objclsOperationEqipmentQtyContent.strFuGuoQianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUGUOQIANBEFORE", objclsOperationEqipmentQtyContent.strFuGuoQianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YOUDAIFANGDIAN", objclsOperationEqipmentQtyContent.strYouDaiFangDian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUDAIFANGDIANAFTER", objclsOperationEqipmentQtyContent.strYouDaiFangDianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUDAIFANGDIANBEFORE", objclsOperationEqipmentQtyContent.strYouDaiFangDianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WUDAIFANGDIAN", objclsOperationEqipmentQtyContent.strWuDaiFangDian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUDAIFANGDIANAFTER", objclsOperationEqipmentQtyContent.strWuDaiFangDianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUDAIFANGDIANBEFORE", objclsOperationEqipmentQtyContent.strWuDaiFangDianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("YOUDAICHANGDIAN", objclsOperationEqipmentQtyContent.strYouDaiChangDian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUDAICHANGDIANAFTER", objclsOperationEqipmentQtyContent.strYouDaiChangDianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("YOUDAICHANGDIANBEFORE", objclsOperationEqipmentQtyContent.strYouDaiChangDianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WUDAICHANGDIAN", objclsOperationEqipmentQtyContent.strWuDaiChangDian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUDAICHANGDIANAFTER", objclsOperationEqipmentQtyContent.strWuDaiChangDianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WUDAICHANGDIANBEFORE", objclsOperationEqipmentQtyContent.strWuDaiChangDianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("FUNIEYINLIU", objclsOperationEqipmentQtyContent.strFuNieYinLiu.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUNIEYINLIUAFTER", objclsOperationEqipmentQtyContent.strFuNieYinLiuAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("FUNIEYINLIUBEFORE", objclsOperationEqipmentQtyContent.strFuNieYinLiuBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("KAILUMIAN", objclsOperationEqipmentQtyContent.strKaiLuMian.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KAILUMIANAFTER", objclsOperationEqipmentQtyContent.strKaiLuMianAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("KAILUMIANBEFORE", objclsOperationEqipmentQtyContent.strKaiLuMianBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("QUANGONGSHA", objclsOperationEqipmentQtyContent.strQuanGongSha.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUANGONGSHAAFTER", objclsOperationEqipmentQtyContent.strQuanGongShaAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("QUANGONGSHABEFORE", objclsOperationEqipmentQtyContent.strQuanGongShaBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("WANGSHA", objclsOperationEqipmentQtyContent.strWangSha.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANGSHAAFTER", objclsOperationEqipmentQtyContent.strWangShaAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("WANGSHABEFORE", objclsOperationEqipmentQtyContent.strWangShaBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHAKUAI", objclsOperationEqipmentQtyContent.strShaKuai.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHAKUAIAFTER", objclsOperationEqipmentQtyContent.strShaKuaiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHAKUAIBEFORE", objclsOperationEqipmentQtyContent.strShaKuaiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("SHAQIU", objclsOperationEqipmentQtyContent.strShaQiu.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHAQIUAFTER", objclsOperationEqipmentQtyContent.strShaQiuAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("SHAQIUBEFORE", objclsOperationEqipmentQtyContent.strShaQiuBefore.Replace('\'', '��'));


            m_objXmlWriter.WriteAttributeString("BIANDAI", objclsOperationEqipmentQtyContent.strBianDai.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIANDAIAFTER", objclsOperationEqipmentQtyContent.strBianDaiAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("BIANDAIBEFORE", objclsOperationEqipmentQtyContent.strBianDaiBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("CHANGQIANTAO", objclsOperationEqipmentQtyContent.strChangQianTao.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANTAOAFTER", objclsOperationEqipmentQtyContent.strChangQianTaoAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("CHANGQIANTAOBEFORE", objclsOperationEqipmentQtyContent.strChangQianTaoBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteAttributeString("NIAOGUAN", objclsOperationEqipmentQtyContent.strNiaoGuan.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NIAOGUANAFTER", objclsOperationEqipmentQtyContent.strNiaoGuanAfter.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NIAOGUANBEFORE", objclsOperationEqipmentQtyContent.strNiaoGuanBefore.Replace('\'', '��'));

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();

            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);

        }

        #endregion

        /// <summary>
        /// ƴ��ʿ
        /// </summary>
        /// <param name="objclsOperationEqipmentQtyContent"></param>
        /// <returns></returns>
        private string m_strGetNurseXML(clsOperationNurse objclsOperationNurse)
        {
            m_objXmlMemStream.SetLength(0);

            m_objXmlWriter.WriteStartDocument();
            m_objXmlWriter.WriteStartElement("RecordMaster");

            m_objXmlWriter.WriteAttributeString("INPATIENTID", objclsOperationNurse.strInPatientID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("INPATIENTDATE", objclsOperationNurse.strInPatientDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("OPENDATE", objclsOperationNurse.strOpenDate.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSEID", objclsOperationNurse.strNurseID.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("NURSEFLAG", objclsOperationNurse.strNurseFlag.Replace('\'', '��'));
            m_objXmlWriter.WriteAttributeString("STATUS", objclsOperationNurse.strStatus.Replace('\'', '��'));

            m_objXmlWriter.WriteEndElement();
            m_objXmlWriter.WriteEndDocument();
            m_objXmlWriter.Flush();
            return System.Text.Encoding.Unicode.GetString(m_objXmlMemStream.ToArray(), 39 * 2, (int)m_objXmlMemStream.Length - 39 * 2);
        }


        /// <summary>
        /// ��ʾ
        /// </summary>
        /// <param name="strInPatientID"></param>
        /// <param name="strInPatientDate"></param>
        /// <param name="strOpenDate"></param>
        /// <param name="strReceivedXML"></param>
        /// <param name="intReturnRows"></param>
        /// <returns></returns>
        public long lngSelectDisply(string strInPatientID, string strInPatientDate, string strOpenDate, out clsOperationEquipmentPackage objclsOperationEquipmentPackage)
        {
            objclsOperationEquipmentPackage = null;
            #region �������
            string m_strReceivedXML = "";
            int m_intReturnRows = 0;
            //����Package
            clsOperationEquipmentPackage m_objPackage = new clsOperationEquipmentPackage();

            //��������
            clsOperationEqipmentQtyXML objOperationEqipmentQtyXML = new clsOperationEqipmentQtyXML();

            //�����ӱ�
            clsOperationEqipmentQtyContent objOperationEqipmentQtyContent = new clsOperationEqipmentQtyContent();
            #endregion

            long lngSucceed = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetCurrentRecordXMLTable(strInPatientID, strInPatientDate, strOpenDate, ref m_strReceivedXML, ref m_intReturnRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (m_intReturnRows > 0)
            {
                XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {

                                #region �ӱ�
                                objOperationEqipmentQtyContent.strInPatientID = objReader.GetAttribute("INPATIENTID").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strOpenDate = objReader.GetAttribute("OPENDATE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strModifyUserID = objReader.GetAttribute("MODIFYUSERID").ToString().Replace('��', '\'');

                                //								objOperationEqipmentQtyContent.strOperationID=objReader.GetAttribute("INPATIENTID").ToString().Replace ('��','\'');
                                objOperationEqipmentQtyContent.strOperationName = objReader.GetAttribute("OPERATIONNAME").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWenWan125 = objReader.GetAttribute("WENWAN125").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWenWan125After = objReader.GetAttribute("WENWAN125AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWenWan125Before = objReader.GetAttribute("WENWAN125BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWenZhi125 = objReader.GetAttribute("WENZHI125").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWenZhi125After = objReader.GetAttribute("WENZHI125AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWenZhi125Before = objReader.GetAttribute("WENZHI125BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiaoWan14 = objReader.GetAttribute("XIAOWAN14").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaoWan14After = objReader.GetAttribute("XIAOWAN14AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaoWan14Before = objReader.GetAttribute("XIAOWAN14BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiaoZhi14 = objReader.GetAttribute("XIAOZHI14").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaoZhi14After = objReader.GetAttribute("XIAOZHI14AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaoZhi14Before = objReader.GetAttribute("XIAOZHI14BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhongWan16 = objReader.GetAttribute("ZHONGWAN16").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongWan16After = objReader.GetAttribute("ZHONGWAN16AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongWan16Before = objReader.GetAttribute("ZHONGWAN16BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhongZhi16 = objReader.GetAttribute("ZHONGZHI16").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongZhi16After = objReader.GetAttribute("ZHONGZHI16AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongZhi16Before = objReader.GetAttribute("ZHONGZHI16BEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strPiQian = objReader.GetAttribute("PIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strPiQianAfter = objReader.GetAttribute("PIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strPiQianBefore = objReader.GetAttribute("PIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhiYouChiXueGuanQian = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiYouChiXueGuanQianAfter = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiYouChiXueGuanQianBefore = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strQuanQian = objReader.GetAttribute("QUANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQuanQianAfter = objReader.GetAttribute("QUANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQuanQianBefore = objReader.GetAttribute("QUANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJinQian = objReader.GetAttribute("JINQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJinQianAfter = objReader.GetAttribute("JINQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJinQianBefore = objReader.GetAttribute("JINQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChiZhenQian18 = objReader.GetAttribute("CHIZHENQIAN18").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiZhenQian18After = objReader.GetAttribute("CHIZHENQIAN18AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiZhenQian18Before = objReader.GetAttribute("CHIZHENQIAN18BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYouChiNie = objReader.GetAttribute("YOUCHINIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouChiNieAfter = objReader.GetAttribute("YOUCHINIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouChiNieBefore = objReader.GetAttribute("YOUCHINIEBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strWuChiNie = objReader.GetAttribute("WUCHINIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChiNieAfter = objReader.GetAttribute("WUCHINIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChiNieBefore = objReader.GetAttribute("WUCHINIEBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangYaBan = objReader.GetAttribute("CHANGYABAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangYaBanAfter = objReader.GetAttribute("CHANGYABANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangYaBanBefore = objReader.GetAttribute("CHANGYABANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoBing3 = objReader.GetAttribute("DAOBING3").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing3After = objReader.GetAttribute("DAOBING3AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing3Before = objReader.GetAttribute("DAOBING3BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoBing4 = objReader.GetAttribute("DAOBING4").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing4After = objReader.GetAttribute("DAOBING4AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing4Before = objReader.GetAttribute("DAOBING4BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoBing7 = objReader.GetAttribute("DAOBING7").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing7After = objReader.GetAttribute("DAOBING7AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing7Before = objReader.GetAttribute("DAOBING7BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWuChiNie = objReader.GetAttribute("WUCHINIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChiNieAfter = objReader.GetAttribute("WUCHINIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChiNieBefore = objReader.GetAttribute("WUCHINIEBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strZhiZhuZhiJian = objReader.GetAttribute("ZHIZHUZHIJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiZhuZhiJianAfter = objReader.GetAttribute("ZHIZHUZHIJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiZhuZhiJianBefore = objReader.GetAttribute("ZHIZHUZHIJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanZhuZhiJian = objReader.GetAttribute("WANZHUZHIJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanZhuZhiJianAfter = objReader.GetAttribute("WANZHUZHIJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanZhuZhiJianBefore = objReader.GetAttribute("WANZHUZHIJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strBianTaoXianJian = objReader.GetAttribute("BIANTAOXIANJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBianTaoXianJianAfter = objReader.GetAttribute("BIANTAOXIANJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBianTaoXianJianBefore = objReader.GetAttribute("BIANTAOXIANJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiongQiangJian = objReader.GetAttribute("XIONGQIANGJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiongQiangJianAfter = objReader.GetAttribute("XIONGQIANGJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiongQiangJianBefore = objReader.GetAttribute("XIONGQIANGJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhiJiaoXiaoLaGou = objReader.GetAttribute("ZHIJIAOXIAOLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoXiaoLaGouAfter = objReader.GetAttribute("ZHIJIAOXIAOLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoXiaoLaGouBefore = objReader.GetAttribute("ZHIJIAOXIAOLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLanWeiLaGou = objReader.GetAttribute("LANWEILAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLanWeiLaGouAfter = objReader.GetAttribute("LANWEILAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLanWeiLaGouBefore = objReader.GetAttribute("LANWEILAGOUBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strZhongFuGou = objReader.GetAttribute("ZHONGFUGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongFuGouAfter = objReader.GetAttribute("ZHONGFUGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongFuGouBefore = objReader.GetAttribute("ZHONGFUGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangYaGou = objReader.GetAttribute("CHANGYAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangYaGouAfter = objReader.GetAttribute("CHANGYAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangYaGouBefore = objReader.GetAttribute("CHANGYAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhiJiaoQian = objReader.GetAttribute("ZHIJIAOQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoQianAfter = objReader.GetAttribute("ZHIJIAOQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoQianBefore = objReader.GetAttribute("ZHIJIAOQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiaFuBuQianKaiQi = objReader.GetAttribute("XIAFUBUQIANKAIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaFuBuQianKaiQiAfter = objReader.GetAttribute("XIAFUBUQIANKAIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaFuBuQianKaiQiBefore = objReader.GetAttribute("XIAFUBUQIANKAIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strTongQuan = objReader.GetAttribute("TONGQUAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTongQuanAfter = objReader.GetAttribute("TONGQUANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTongQuanBefore = objReader.GetAttribute("TONGQUANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiYeGuan = objReader.GetAttribute("XIYEGUAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiYeGuanAfter = objReader.GetAttribute("XIYEGUANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiYeGuanBefore = objReader.GetAttribute("XIYEGUANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaZhiQian = objReader.GetAttribute("DAZHIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaZhiQianAfter = objReader.GetAttribute("DAZHIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaZhiQianBefore = objReader.GetAttribute("DAZHIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhiJiaoGou = objReader.GetAttribute("ZHIJIAOGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoGouAfter = objReader.GetAttribute("ZHIJIAOGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoGouBefore = objReader.GetAttribute("ZHIJIAOGOUBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strDaoPian = objReader.GetAttribute("DAOPIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoPianAfter = objReader.GetAttribute("DAOPIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoPianBefore = objReader.GetAttribute("DAOPIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanXueGuanQian18 = objReader.GetAttribute("WANXUEGUANQIAN18").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian18After = objReader.GetAttribute("WANXUEGUANQIAN18AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian18Before = objReader.GetAttribute("WANXUEGUANQIAN18BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanXueGuanQian22 = objReader.GetAttribute("WANXUEGUANQIAN22").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian22After = objReader.GetAttribute("WANXUEGUANQIAN22AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian22Before = objReader.GetAttribute("WANXUEGUANQIAN22BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangChiZhenQian25 = objReader.GetAttribute("CHANGCHIZHENQIAN25").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangChiZhenQian25After = objReader.GetAttribute("CHANGCHIZHENQIAN25AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangChiZhenQian25Before = objReader.GetAttribute("CHANGCHIZHENQIAN25BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strFengZhen = objReader.GetAttribute("FENGZHEN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFengZhenAfter = objReader.GetAttribute("FENGZHENAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFengZhenBefore = objReader.GetAttribute("FENGZHENBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strNianMoQian = objReader.GetAttribute("NIANMOQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNianMoQianAfter = objReader.GetAttribute("NIANMOQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNianMoQianBefore = objReader.GetAttribute("NIANMOQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShaLiQian = objReader.GetAttribute("SHALIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaLiQianAfter = objReader.GetAttribute("SHALIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaLiQianBefore = objReader.GetAttribute("SHALIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShaLiQian = objReader.GetAttribute("SHALIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaLiQianAfter = objReader.GetAttribute("SHALIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaLiQianBefore = objReader.GetAttribute("SHALIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanXueGuanQian20 = objReader.GetAttribute("WANXUEGUANQIAN20").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian20After = objReader.GetAttribute("WANXUEGUANQIAN20AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian20Before = objReader.GetAttribute("WANXUEGUANQIAN20BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanXueGuanQian25 = objReader.GetAttribute("WANXUEGUANQIAN25").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian25After = objReader.GetAttribute("WANXUEGUANQIAN25AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian25Before = objReader.GetAttribute("WANXUEGUANQIAN25BEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strChangQianWan = objReader.GetAttribute("CHANGQIANWAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianWanAfter = objReader.GetAttribute("CHANGQIANWANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianWanBefore = objReader.GetAttribute("CHANGQIANWANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangQianZhi = objReader.GetAttribute("CHANGQIANZHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianZhiAfter = objReader.GetAttribute("CHANGQIANZHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianZhiBefore = objReader.GetAttribute("CHANGQIANZHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaWanXueGuanQian = objReader.GetAttribute("DAWANXUEGUANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaWanXueGuanQianAfter = objReader.GetAttribute("DAWANXUEGUANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaWanXueGuanQianBefore = objReader.GetAttribute("DAWANXUEGUANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strErYanHouChongXiQi = objReader.GetAttribute("ERYANHOUCHONGXIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strErYanHouChongXiQiAfter = objReader.GetAttribute("ERYANHOUCHONGXIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strErYanHouChongXiQiBefore = objReader.GetAttribute("ERYANHOUCHONGXIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShenDiQian = objReader.GetAttribute("SHENDIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShenDiQianAfter = objReader.GetAttribute("SHENDIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShenDiQianBefore = objReader.GetAttribute("SHENDIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShiQian = objReader.GetAttribute("SHIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShiQianAfter = objReader.GetAttribute("SHIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShiQianBefore = objReader.GetAttribute("SHIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWeiQian = objReader.GetAttribute("WEIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWeiQianAfter = objReader.GetAttribute("WEIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWeiQianBefore = objReader.GetAttribute("WEIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXinErQian = objReader.GetAttribute("XINERQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinErQiaAfter = objReader.GetAttribute("XINERQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinErQiaBefore = objReader.GetAttribute("XINERQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strDaGuJian = objReader.GetAttribute("DAGUJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaGuJianAfter = objReader.GetAttribute("DAGUJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaGuJianBefore = objReader.GetAttribute("DAGUJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDanDaoTanTiao = objReader.GetAttribute("DANDAOTANTIAO").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDanDaoTanTiaoAfter = objReader.GetAttribute("DANDAOTANTIAOAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDanDaoTanTiaoBefore = objReader.GetAttribute("DANDAOTANTIAOBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDiErLeiGuJian = objReader.GetAttribute("DIERLEIGUJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDiErLeiGuJianAfter = objReader.GetAttribute("DIERLEIGUJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDiErLeiGuJianBefore = objReader.GetAttribute("DIERLEIGUJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strFangTouYaoGuQian = objReader.GetAttribute("FANGTOUYAOGUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFangTouYaoGuQianAfter = objReader.GetAttribute("FANGTOUYAOGUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFangTouYaoGuQianBefore = objReader.GetAttribute("FANGTOUYAOGUQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strHeLongQi = objReader.GetAttribute("HELONGQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strHeLongQiAfter = objReader.GetAttribute("HELONGQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strHeLongQiBefore = objReader.GetAttribute("HELONGQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJianJiaGuLaGou = objReader.GetAttribute("JIANJIAGULAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJianJiaGuLaGouAfter = objReader.GetAttribute("JIANJIAGULAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJianJiaGuLaGouBefore = objReader.GetAttribute("JIANJIAGULAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLeiGuQianKaiQi = objReader.GetAttribute("LEIGUQIANKAIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQianKaiQiAfter = objReader.GetAttribute("LEIGUQIANKAIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQianKaiQiBefore = objReader.GetAttribute("LEIGUQIANKAIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLeiGuQiZi = objReader.GetAttribute("LEIGUQIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQiZiAfter = objReader.GetAttribute("LEIGUQIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQiZiBefore = objReader.GetAttribute("LEIGUQIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strTanZhenChu = objReader.GetAttribute("TANZHENCHU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTanZhenChuAfter = objReader.GetAttribute("TANZHENCHUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTanZhenChuBefore = objReader.GetAttribute("TANZHENCHUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strTanZhenXi = objReader.GetAttribute("TANZHENXI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTanZhenXiAfter = objReader.GetAttribute("TANZHENXIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTanZhenXiBefore = objReader.GetAttribute("TANZHENXIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYaoGuQian = objReader.GetAttribute("YAOGUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYaoGuQianAfter = objReader.GetAttribute("YAOGUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYaoGuQianBefore = objReader.GetAttribute("YAOGUQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strChiGuQian = objReader.GetAttribute("CHIGUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiGuQianAfter = objReader.GetAttribute("CHIGUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiGuQianBefore = objReader.GetAttribute("CHIGUQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDanChiLaGou = objReader.GetAttribute("DANCHILAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDanChiLaGouAfter = objReader.GetAttribute("DANCHILAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDanChiLaGouBefore = objReader.GetAttribute("DANCHILAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoXiangQi = objReader.GetAttribute("DAOXIANGQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoXiangQiAfter = objReader.GetAttribute("DAOXIANGQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoXiangQiBefore = objReader.GetAttribute("DAOXIANGQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGuChui = objReader.GetAttribute("GUCHUI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuChuiAfter = objReader.GetAttribute("GUCHUIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuChuiBefore = objReader.GetAttribute("GUCHUIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGuDao = objReader.GetAttribute("GUDAO").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuDaoAfter = objReader.GetAttribute("GUDAOAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuDaoBefore = objReader.GetAttribute("GUDAOBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGuZao = objReader.GetAttribute("GUZAO").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuZaoAfter = objReader.GetAttribute("GUZAOAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuZaoBefore = objReader.GetAttribute("GUZAOBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGuMoBoLiQi = objReader.GetAttribute("GUMOBOLIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuMoBoLiQiAfter = objReader.GetAttribute("GUMOBOLIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuMoBoLiQiBefore = objReader.GetAttribute("GUMOBOLIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJingGuQiZi = objReader.GetAttribute("JINGGUQIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJingGuQiZiAfter = objReader.GetAttribute("JINGGUQIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJingGuQiZiBefore = objReader.GetAttribute("JINGGUQIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKuoShi = objReader.GetAttribute("KUOSHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKuoShiAfter = objReader.GetAttribute("KUOSHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKuoShiBefore = objReader.GetAttribute("KUOSHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLeiGuQiZi = objReader.GetAttribute("LEIGUQIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQiZiAfter = objReader.GetAttribute("LEIGUQIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQiZiBefore = objReader.GetAttribute("LEIGUQIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLaoHuQian = objReader.GetAttribute("LAOHUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLaoHuQianAfter = objReader.GetAttribute("LAOHUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLaoHuQianBefore = objReader.GetAttribute("LAOHUQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLuoSiQiZi = objReader.GetAttribute("LUOSIQIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLuoSiQiZiAfter = objReader.GetAttribute("LUOSIQIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLuoSiQiZiBefore = objReader.GetAttribute("LUOSIQIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strPingHengFuWeiQian = objReader.GetAttribute("PINGHENGFUWEIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strPingHengFuWeiQianAfter = objReader.GetAttribute("PINGHENGFUWEIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strPingHengFuWeiQianBefore = objReader.GetAttribute("PINGHENGFUWEIQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strBaiShiQianKaiQi = objReader.GetAttribute("BAISHIQIANKAIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBaiShiQianKaiQiAfter = objReader.GetAttribute("BAISHIQIANKAIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBaiShiQianKaiQiBefore = objReader.GetAttribute("BAISHIQIANKAIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChiBanQian = objReader.GetAttribute("CHIBANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiBanQianAfter = objReader.GetAttribute("CHIBANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiBanQianBefore = objReader.GetAttribute("CHIBANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJianBoLiZi = objReader.GetAttribute("JIANBOLIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJianBoLiZiAfter = objReader.GetAttribute("JIANBOLIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJianBoLiZiBefore = objReader.GetAttribute("JIANBOLIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJingTuJian = objReader.GetAttribute("JINGTUJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJingTuJianAfter = objReader.GetAttribute("JINGTUJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJingTuJianBefore = objReader.GetAttribute("JINGTUJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKaiLuZhuan = objReader.GetAttribute("KAILUZHUAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaiLuZhuanAfter = objReader.GetAttribute("KAILUZHUANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaiLuZhuanBefore = objReader.GetAttribute("KAILUZHUANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strQiangZhuangNie = objReader.GetAttribute("QIANGZHUANGNIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQiangZhuangNieAfter = objReader.GetAttribute("QIANGZHUANGNIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQiangZhuangNieBefore = objReader.GetAttribute("QIANGZHUANGNIEBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShuiHeQian = objReader.GetAttribute("SHUIHEQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShuiHeQianAfter = objReader.GetAttribute("SHUIHEQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShuiHeQianBefore = objReader.GetAttribute("SHUIHEQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strTouPiJianQian = objReader.GetAttribute("TOUPIJIANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTouPiJianQianAfter = objReader.GetAttribute("TOUPIJIANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTouPiJianQianBefore = objReader.GetAttribute("TOUPIJIANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXianJuDaoYinZi = objReader.GetAttribute("XIANJUDAOYINZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXianJuDaoYinZiAfter = objReader.GetAttribute("XIANJUDAOYINZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXianJuDaoYinZiBefore = objReader.GetAttribute("XIANJUDAOYINZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXinErLaGou = objReader.GetAttribute("XINERLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinErLaGouAfter = objReader.GetAttribute("XINERLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinErLaGouBefore = objReader.GetAttribute("XINERLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuiBanBoLiQi = objReader.GetAttribute("ZHUIBANBOLIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuiBanBoLiQiAfter = objReader.GetAttribute("ZHUIBANBOLIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuiBanBoLiQiBefore = objReader.GetAttribute("ZHUIBANBOLIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuiBanYaoGuQian = objReader.GetAttribute("ZHUIBANYAOGUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuiBanYaoGuQianAfter = objReader.GetAttribute("ZHUIBANYAOGUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuiBanYaoGuQianBefore = objReader.GetAttribute("ZHUIBANYAOGUQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strCeBanQi = objReader.GetAttribute("CEBANQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strCeBanQiAfter = objReader.GetAttribute("CEBANQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strCeBanQiBefore = objReader.GetAttribute("CEBANQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChuanCiZhen = objReader.GetAttribute("CHUANCIZHEN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChuanCiZhenAfter = objReader.GetAttribute("CHUANCIZHENAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChuanCiZhenBefore = objReader.GetAttribute("CHUANCIZHENBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoXianGou = objReader.GetAttribute("DAOXIANGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoXianGouAfter = objReader.GetAttribute("DAOXIANGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoXianGouBefore = objReader.GetAttribute("DAOXIANGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strErJianBanKuoZhangQi = objReader.GetAttribute("ERJIANBANKUOZHANGQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strErJianBanKuoZhangQiAfter = objReader.GetAttribute("ERJIANBANKUOZHANGQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strErJianBanKuoZhangQiBefore = objReader.GetAttribute("ERJIANBANKUOZHANGQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strFeiYeDangBan = objReader.GetAttribute("FEIYEDANGBAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFeiYeDangBanAfter = objReader.GetAttribute("FEIYEDANGBANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFeiYeDangBanBefore = objReader.GetAttribute("FEIYEDANGBANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strNaoMoGou = objReader.GetAttribute("NAOMOGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNaoMoGouAfter = objReader.GetAttribute("NAOMOGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNaoMoGouBefore = objReader.GetAttribute("NAOMOGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXinFangLaGou = objReader.GetAttribute("XINFANGLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinFangLaGouAfter = objReader.GetAttribute("XINFANGLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinFangLaGouBefore = objReader.GetAttribute("XINFANGLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGou = objReader.GetAttribute("XINNEIZHIJIAOLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGouAfter = objReader.GetAttribute("XINNEIZHIJIAOLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGouBefore = objReader.GetAttribute("XINNEIZHIJIAOLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYinDingQian = objReader.GetAttribute("YINDINGQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYinDingQianAfter = objReader.GetAttribute("YINDINGQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYinDingQianBefore = objReader.GetAttribute("YINDINGQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuACeBiQian = objReader.GetAttribute("ZHUACEBIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuACeBiQiAfter = objReader.GetAttribute("ZHUACEBIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuACeBiQiBefore = objReader.GetAttribute("ZHUACEBIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuAYouLiQian = objReader.GetAttribute("ZHUAYOULIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuAYouLiQianAfter = objReader.GetAttribute("ZHUAYOULIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuAYouLiQianBefore = objReader.GetAttribute("ZHUAYOULIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuAZhuDuanQian = objReader.GetAttribute("ZHUAZHUDUANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuAZhuDuanQianAfter = objReader.GetAttribute("ZHUAZHUDUANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuAZhuDuanQianBefore = objReader.GetAttribute("ZHUAZHUDUANQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strFuKui = objReader.GetAttribute("FUKUI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuKuiAfter = objReader.GetAttribute("FUKUIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuKuiBefore = objReader.GetAttribute("FUKUIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGongChi = objReader.GetAttribute("GONGCHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongChiAfter = objReader.GetAttribute("GONGCHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongChiBefore = objReader.GetAttribute("GONGCHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGongGuaShi = objReader.GetAttribute("GONGGUASHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongGuaShiAfter = objReader.GetAttribute("GONGGUASHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongGuaShiBefore = objReader.GetAttribute("GONGGUASHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGongJingQian = objReader.GetAttribute("GONGJINGQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongJingQianAfter = objReader.GetAttribute("GONGJINGQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongJingQianBefore = objReader.GetAttribute("GONGJINGQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJiLiuBoLiZi = objReader.GetAttribute("JILIUBOLIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJiLiuBoLiZiAfter = objReader.GetAttribute("JILIUBOLIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJiLiuBoLiZiBefore = objReader.GetAttribute("JILIUBOLIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKaChi = objReader.GetAttribute("KACHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaChiAfter = objReader.GetAttribute("KACHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaChiBefore = objReader.GetAttribute("KACHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKuoGongQi = objReader.GetAttribute("KUOGONGQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKuoGongQiAfter = objReader.GetAttribute("KUOGONGQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKuoGongQiBefore = objReader.GetAttribute("KUOGONGQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strRenDaiQian = objReader.GetAttribute("RENDAIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strRenDaiQianAfter = objReader.GetAttribute("RENDAIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strRenDaiQianBefore = objReader.GetAttribute("RENDAIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShenJingLaGou = objReader.GetAttribute("SHENJINGLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShenJingLaGouAfter = objReader.GetAttribute("SHENJINGLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShenJingLaGouBefore = objReader.GetAttribute("SHENJINGLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWuChuangNie = objReader.GetAttribute("WUCHUANGNIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChuangNieAfter = objReader.GetAttribute("WUCHUANGNIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChuangNieBefore = objReader.GetAttribute("WUCHUANGNIEBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXueGuanJia = objReader.GetAttribute("XUEGUANJIA").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXueGuanJiaAfter = objReader.GetAttribute("XUEGUANJIAAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXueGuanJiaBefore = objReader.GetAttribute("XUEGUANJIABEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYinDaoLaGou = objReader.GetAttribute("YINDAOLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYinDaoLaGouAfter = objReader.GetAttribute("YINDAOLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYinDaoLaGouBefore = objReader.GetAttribute("YINDAOLAGOUBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strFuGuoQian = objReader.GetAttribute("FUGUOQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuGuoQianAfter = objReader.GetAttribute("FUGUOQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuGuoQianBefore = objReader.GetAttribute("FUGUOQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strFuNieYinLiu = objReader.GetAttribute("FUNIEYINLIU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuNieYinLiuAfter = objReader.GetAttribute("FUNIEYINLIUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuNieYinLiuBefore = objReader.GetAttribute("FUNIEYINLIUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJinShuNiaoGou = objReader.GetAttribute("JINSHUNIAOGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJinShuNiaoGouAfter = objReader.GetAttribute("JINSHUNIAOGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJinShuNiaoGouBefore = objReader.GetAttribute("JINSHUNIAOGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKaiLuMian = objReader.GetAttribute("KAILUMIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaiLuMianAfter = objReader.GetAttribute("KAILUMIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaiLuMianBefore = objReader.GetAttribute("KAILUMIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strQuanGongSha = objReader.GetAttribute("QUANGONGSHA").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQuanGongShaAfter = objReader.GetAttribute("QUANGONGSHAAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQuanGongShaBefore = objReader.GetAttribute("QUANGONGSHABEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShaKuai = objReader.GetAttribute("SHAKUAI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaKuaiAfter = objReader.GetAttribute("SHAKUAIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaKuaiBefore = objReader.GetAttribute("SHAKUAIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShaQiu = objReader.GetAttribute("SHAQIU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaQiuAfter = objReader.GetAttribute("SHAQIUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaQiuBefore = objReader.GetAttribute("SHAQIUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWangSha = objReader.GetAttribute("WANGSHA").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWangShaAfter = objReader.GetAttribute("WANGSHAAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWangShaBefore = objReader.GetAttribute("WANGSHABEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWuDaiChangDian = objReader.GetAttribute("WUDAICHANGDIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuDaiChangDianAfter = objReader.GetAttribute("WUDAICHANGDIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuDaiChangDianBefore = objReader.GetAttribute("WUDAICHANGDIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWuDaiFangDian = objReader.GetAttribute("WUDAIFANGDIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuDaiFangDianAfter = objReader.GetAttribute("WUDAIFANGDIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuDaiFangDianBefore = objReader.GetAttribute("WUDAIFANGDIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYouDaiChangDian = objReader.GetAttribute("YOUDAICHANGDIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouDaiChangDianAfter = objReader.GetAttribute("YOUDAICHANGDIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouDaiChangDianBefore = objReader.GetAttribute("YOUDAICHANGDIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYouDaiFangDian = objReader.GetAttribute("YOUDAIFANGDIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouDaiFangDianAfter = objReader.GetAttribute("YOUDAIFANGDIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouDaiFangDianBefore = objReader.GetAttribute("YOUDAIFANGDIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strBianDai = objReader.GetAttribute("BIANDAI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBianDaiAfter = objReader.GetAttribute("BIANDAIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBianDaiBefore = objReader.GetAttribute("BIANDAIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangQianTao = objReader.GetAttribute("CHANGQIANTAO").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianTaoAfter = objReader.GetAttribute("CHANGQIANTAOAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianTaoBefore = objReader.GetAttribute("CHANGQIANTAOBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strNiaoGuan = objReader.GetAttribute("NIAOGUAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNiaoGuanAfter = objReader.GetAttribute("NIAOGUANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNiaoGuanBefore = objReader.GetAttribute("NIAOGUANBEFORE").ToString().Replace('��', '\'');


                                m_objPackage.m_objOperationEqipmentQtyContent = objOperationEqipmentQtyContent;

                                #endregion

                                #region ����
                                objOperationEqipmentQtyXML.strInPatientID = objReader.GetAttribute("INPATIENTID");
                                objOperationEqipmentQtyXML.strInPatientDate = objReader.GetAttribute("INPATIENTDATE");
                                objOperationEqipmentQtyXML.strOpenDate = objReader.GetAttribute("OPENDATE");

                                objOperationEqipmentQtyXML.strCreateDate = objReader.GetAttribute("CREATEDATE");
                                objOperationEqipmentQtyXML.strCreateUserID = objReader.GetAttribute("CREATEUSERID");

                                //								objOperationEqipmentQtyContentInsert.strOperationID=objReader.GetAttribute("INPATIENTID").ToString().Replace ('��','\'');
                                objOperationEqipmentQtyXML.strOperationNameXML = objReader.GetAttribute("OPERATIONNAMEXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWenWan125XML = objReader.GetAttribute("WENWAN125XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWenWan125AfterXML = objReader.GetAttribute("WENWAN125AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWenWan125BeforeXML = objReader.GetAttribute("WENWAN125BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWenZhi125XML = objReader.GetAttribute("WENZHI125XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWenZhi125AfterXML = objReader.GetAttribute("WENZHI125AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWenZhi125BeforeXML = objReader.GetAttribute("WENZHI125BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiaoWan14XML = objReader.GetAttribute("XIAOWAN14XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaoWan14AfterXML = objReader.GetAttribute("XIAOWAN14AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaoWan14BeforeXML = objReader.GetAttribute("XIAOWAN14BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiaoZhi14XML = objReader.GetAttribute("XIAOZHI14XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaoZhi14AfterXML = objReader.GetAttribute("XIAOZHI14AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaoZhi14BeforeXML = objReader.GetAttribute("XIAOZHI14BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhongWan16XML = objReader.GetAttribute("ZHONGWAN16XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongWan16AfterXML = objReader.GetAttribute("ZHONGWAN16AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongWan16BeforeXML = objReader.GetAttribute("ZHONGWAN16BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhongZhi16XML = objReader.GetAttribute("ZHONGZHI16XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongZhi16AfterXML = objReader.GetAttribute("ZHONGZHI16AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongZhi16BeforeXML = objReader.GetAttribute("ZHONGZHI16BEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChiZhenQian18XML = objReader.GetAttribute("CHIZHENQIAN18XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiZhenQian18AfterXML = objReader.GetAttribute("CHIZHENQIAN18AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiZhenQian18BeforeXML = objReader.GetAttribute("CHIZHENQIAN18BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJinQianAfterXML = objReader.GetAttribute("JINQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJinQianBeforeXML = objReader.GetAttribute("JINQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJinQianXML = objReader.GetAttribute("JINQIANXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strPiQianAfterXML = objReader.GetAttribute("PIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strPiQianBeforeXML = objReader.GetAttribute("PIQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strPiQianXML = objReader.GetAttribute("PIQIANXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strQuanQianAfterXML = objReader.GetAttribute("QUANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQuanQianBeforeXML = objReader.GetAttribute("QUANQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQuanQianXML = objReader.GetAttribute("QUANQIANXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhiYouChiXueGuanQianAfterXML = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiYouChiXueGuanQianBeforeXML = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiYouChiXueGuanQianXML = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANXML").ToString().Replace('��', '\'');



                                objOperationEqipmentQtyXML.strYouChiNieXML = objReader.GetAttribute("YOUCHINIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouChiNieBeforeXML = objReader.GetAttribute("YOUCHINIEBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouChiNieAfterXML = objReader.GetAttribute("YOUCHINIEAFTERXML").ToString().Replace('��', '\'');


                                objOperationEqipmentQtyXML.strPingHengFuWeiQianXML = objReader.GetAttribute("PINGHENGFUWEIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strPingHengFuWeiQianBeforeXML = objReader.GetAttribute("PINGHENGFUWEIQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strPingHengFuWeiQianAfterXML = objReader.GetAttribute("PINGHENGFUWEIQIANAFTERXML").ToString().Replace('��', '\'');

                                //************************************************
                                objOperationEqipmentQtyXML.strWuChiNieXML = objReader.GetAttribute("WUCHINIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChiNieAfterXML = objReader.GetAttribute("WUCHINIEAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChiNieBeforeXML = objReader.GetAttribute("WUCHINIEBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strChangYaBanXML = objReader.GetAttribute("CHANGYABANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangYaBanAfterXML = objReader.GetAttribute("CHANGYABANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangYaBanBeforeXML = objReader.GetAttribute("CHANGYABANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoBing3XML = objReader.GetAttribute("DAOBING3XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing3AfterXML = objReader.GetAttribute("DAOBING3AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing3BeforeXML = objReader.GetAttribute("DAOBING3BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoBing4XML = objReader.GetAttribute("DAOBING4XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing4AfterXML = objReader.GetAttribute("DAOBING4AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing4BeforeXML = objReader.GetAttribute("DAOBING4BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoBing7XML = objReader.GetAttribute("DAOBING7XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing7AfterXML = objReader.GetAttribute("DAOBING7AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing7BeforeXML = objReader.GetAttribute("DAOBING7BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWuChiNieXML = objReader.GetAttribute("WUCHINIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChiNieAfterXML = objReader.GetAttribute("WUCHINIEAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChiNieBeforeXML = objReader.GetAttribute("WUCHINIEBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strZhiZhuZhiJianXML = objReader.GetAttribute("ZHIZHUZHIJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiZhuZhiJianBeforeXML = objReader.GetAttribute("ZHIZHUZHIJIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiZhuZhiJianAfterXML = objReader.GetAttribute("ZHIZHUZHIJIANAFTERXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhiJiaoXiaoLaGouXML = objReader.GetAttribute("ZHIJIAOXIAOLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoXiaoLaGouBeforeXML = objReader.GetAttribute("ZHIJIAOXIAOLAGOUBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoXiaoLaGouAfterXML = objReader.GetAttribute("ZHIJIAOXIAOLAGOUAFTERXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiongQiangJianXML = objReader.GetAttribute("XIONGQIANGJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiongQiangJianAfterXML = objReader.GetAttribute("XIONGQIANGJIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiongQiangJianBeforeXML = objReader.GetAttribute("XIONGQIANGJIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanZhuZhiJianXML = objReader.GetAttribute("WANZHUZHIJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanZhuZhiJianBeforeXML = objReader.GetAttribute("WANZHUZHIJIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanZhuZhiJianAfterXML = objReader.GetAttribute("WANZHUZHIJIANAFTERXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLanWeiLaGouXML = objReader.GetAttribute("LANWEILAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLanWeiLaGouAfterXML = objReader.GetAttribute("LANWEILAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLanWeiLaGouBeforeXML = objReader.GetAttribute("LANWEILAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strBianTaoXianJianXML = objReader.GetAttribute("BIANTAOXIANJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBianTaoXianJianBeforeXML = objReader.GetAttribute("BIANTAOXIANJIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBianTaoXianJianAfterXML = objReader.GetAttribute("BIANTAOXIANJIANAFTERXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChangYaGouXML = objReader.GetAttribute("CHANGYAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangYaGouAfterXML = objReader.GetAttribute("CHANGYAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangYaGouBeforeXML = objReader.GetAttribute("CHANGYAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strTongQuanXML = objReader.GetAttribute("TONGQUANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTongQuanAfterXML = objReader.GetAttribute("TONGQUANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTongQuanBeforeXML = objReader.GetAttribute("TONGQUANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiXML = objReader.GetAttribute("XIAFUBUQIANKAIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiAfterXML = objReader.GetAttribute("XIAFUBUQIANKAIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiBeforeXML = objReader.GetAttribute("XIAFUBUQIANKAIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiXML = objReader.GetAttribute("XIAFUBUQIANKAIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiAfterXML = objReader.GetAttribute("XIAFUBUQIANKAIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiBeforeXML = objReader.GetAttribute("XIAFUBUQIANKAIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiYeGuanXML = objReader.GetAttribute("XIYEGUANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiYeGuanAfterXML = objReader.GetAttribute("XIYEGUANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiYeGuanBeforeXML = objReader.GetAttribute("XIYEGUANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhiJiaoGouXML = objReader.GetAttribute("ZHIJIAOGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoGouAfterXML = objReader.GetAttribute("ZHIJIAOGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoGouBeforeXML = objReader.GetAttribute("ZHIJIAOGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhongFuGouXML = objReader.GetAttribute("ZHONGFUGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongFuGouAfterXML = objReader.GetAttribute("ZHONGFUGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongFuGouBeforeXML = objReader.GetAttribute("ZHONGFUGOUBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strNianMoQianXML = objReader.GetAttribute("NIANMOQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNianMoQianAfterXML = objReader.GetAttribute("NIANMOQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNianMoQianBeforeXML = objReader.GetAttribute("NIANMOQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShaLiQianXML = objReader.GetAttribute("SHALIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaLiQianAfterXML = objReader.GetAttribute("SHALIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaLiQianBeforeXML = objReader.GetAttribute("SHALIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanXueGuanQian18XML = objReader.GetAttribute("WANXUEGUANQIAN18XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian18AfterXML = objReader.GetAttribute("WANXUEGUANQIAN18AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian18BeforeXML = objReader.GetAttribute("WANXUEGUANQIAN18BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanXueGuanQian20XML = objReader.GetAttribute("WANXUEGUANQIAN20XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian20AfterXML = objReader.GetAttribute("WANXUEGUANQIAN20AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian20BeforeXML = objReader.GetAttribute("WANXUEGUANQIAN20BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanXueGuanQian22XML = objReader.GetAttribute("WANXUEGUANQIAN22XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian22AfterXML = objReader.GetAttribute("WANXUEGUANQIAN22AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian22BeforeXML = objReader.GetAttribute("WANXUEGUANQIAN22BEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChangChiZhenQian25XML = objReader.GetAttribute("CHANGCHIZHENQIAN25XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangChiZhenQian25AfterXML = objReader.GetAttribute("CHANGCHIZHENQIAN25AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangChiZhenQian25BeforeXML = objReader.GetAttribute("CHANGCHIZHENQIAN25BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoPianXML = objReader.GetAttribute("DAOPIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoPianAfterXML = objReader.GetAttribute("DAOPIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoPianBeforeXML = objReader.GetAttribute("DAOPIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaZhiQianXML = objReader.GetAttribute("DAZHIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaZhiQianAfterXML = objReader.GetAttribute("DAZHIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaZhiQianBeforeXML = objReader.GetAttribute("DAZHIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strFengZhenXML = objReader.GetAttribute("FENGZHENXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFengZhenAfterXML = objReader.GetAttribute("FENGZHENAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFengZhenBeforeXML = objReader.GetAttribute("FENGZHENBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhiJiaoQianXML = objReader.GetAttribute("ZHIJIAOQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoQianAfterXML = objReader.GetAttribute("ZHIJIAOQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoQianBeforeXML = objReader.GetAttribute("ZHIJIAOQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChangQianZhiXML = objReader.GetAttribute("CHANGQIANZHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianZhiAfterXML = objReader.GetAttribute("CHANGQIANZHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianZhiBeforeXML = objReader.GetAttribute("CHANGQIANZHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaWanXueGuanQianXML = objReader.GetAttribute("DAWANXUEGUANQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaWanXueGuanQianAfterXML = objReader.GetAttribute("DAWANXUEGUANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaWanXueGuanQianBeforeXML = objReader.GetAttribute("DAWANXUEGUANQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShenDiQianXML = objReader.GetAttribute("SHENDIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShenDiQianAfterXML = objReader.GetAttribute("SHENDIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShenDiQianBeforeXML = objReader.GetAttribute("SHENDIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanXueGuanQian25XML = objReader.GetAttribute("WANXUEGUANQIAN25XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian25AfterXML = objReader.GetAttribute("WANXUEGUANQIAN25AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian25BeforeXML = objReader.GetAttribute("WANXUEGUANQIAN25BEFOREXML").ToString().Replace('��', '\'');

                                //************************************************
                                objOperationEqipmentQtyXML.strChangQianWanXML = objReader.GetAttribute("CHANGQIANWANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianWanAfterXML = objReader.GetAttribute("CHANGQIANWANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianWanBeforeXML = objReader.GetAttribute("CHANGQIANWANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strErYanHouChongXiQiXML = objReader.GetAttribute("ERYANHOUCHONGXIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strErYanHouChongXiQiAfterXML = objReader.GetAttribute("ERYANHOUCHONGXIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strErYanHouChongXiQiBeforeXML = objReader.GetAttribute("ERYANHOUCHONGXIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShiQianXML = objReader.GetAttribute("SHIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShiQianAfterXML = objReader.GetAttribute("SHIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShiQianBeforeXML = objReader.GetAttribute("SHIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWeiQianXML = objReader.GetAttribute("WEIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWeiQianAfterXML = objReader.GetAttribute("WEIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWeiQianBeforeXML = objReader.GetAttribute("WEIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXinErQianXML = objReader.GetAttribute("XINERQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinErQiaAfterXML = objReader.GetAttribute("XINERQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinErQiaBeforeXML = objReader.GetAttribute("XINERQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strDanDaoTanTiaoXML = objReader.GetAttribute("DANDAOTANTIAOXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDanDaoTanTiaoAfterXML = objReader.GetAttribute("DANDAOTANTIAOAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDanDaoTanTiaoBeforeXML = objReader.GetAttribute("DANDAOTANTIAOBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strHeLongQiXML = objReader.GetAttribute("HELONGQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strHeLongQiAfterXML = objReader.GetAttribute("HELONGQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strHeLongQiBeforeXML = objReader.GetAttribute("HELONGQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLeiGuQianKaiQiXML = objReader.GetAttribute("LEIGUQIANKAIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQianKaiQiAfterXML = objReader.GetAttribute("LEIGUQIANKAIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQianKaiQiBeforeXML = objReader.GetAttribute("LEIGUQIANKAIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strTanZhenChuXML = objReader.GetAttribute("TANZHENCHUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTanZhenChuAfterXML = objReader.GetAttribute("TANZHENCHUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTanZhenChuBeforeXML = objReader.GetAttribute("TANZHENCHUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strTanZhenXiXML = objReader.GetAttribute("TANZHENXIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTanZhenXiAfterXML = objReader.GetAttribute("TANZHENXIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTanZhenXiBeforeXML = objReader.GetAttribute("TANZHENXIBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strDaGuJianXML = objReader.GetAttribute("DAGUJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaGuJianAfterXML = objReader.GetAttribute("DAGUJIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaGuJianBeforeXML = objReader.GetAttribute("DAGUJIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDiErLeiGuJianXML = objReader.GetAttribute("DIERLEIGUJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDiErLeiGuJianAfterXML = objReader.GetAttribute("DIERLEIGUJIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDiErLeiGuJianBeforeXML = objReader.GetAttribute("DIERLEIGUJIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strFangTouYaoGuQianXML = objReader.GetAttribute("FANGTOUYAOGUQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFangTouYaoGuQianAfterXML = objReader.GetAttribute("FANGTOUYAOGUQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFangTouYaoGuQianBeforeXML = objReader.GetAttribute("FANGTOUYAOGUQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJianJiaGuLaGouXML = objReader.GetAttribute("JIANJIAGULAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJianJiaGuLaGouAfterXML = objReader.GetAttribute("JIANJIAGULAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJianJiaGuLaGouBeforeXML = objReader.GetAttribute("JIANJIAGULAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLeiGuQiZiXML = objReader.GetAttribute("LEIGUQIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQiZiAfterXML = objReader.GetAttribute("LEIGUQIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQiZiBeforeXML = objReader.GetAttribute("LEIGUQIZIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYaoGuQianXML = objReader.GetAttribute("YAOGUQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYaoGuQianAfterXML = objReader.GetAttribute("YAOGUQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYaoGuQianBeforeXML = objReader.GetAttribute("YAOGUQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChiGuQianXML = objReader.GetAttribute("CHIGUQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiGuQianAfterXML = objReader.GetAttribute("CHIGUQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiGuQianBeforeXML = objReader.GetAttribute("CHIGUQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGuChuiXML = objReader.GetAttribute("GUCHUIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuChuiAfterXML = objReader.GetAttribute("GUCHUIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuChuiBeforeXML = objReader.GetAttribute("GUCHUIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGuDaoXML = objReader.GetAttribute("GUDAOXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuDaoAfterXML = objReader.GetAttribute("GUDAOAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuDaoBeforeXML = objReader.GetAttribute("GUDAOBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGuMoBoLiQiXML = objReader.GetAttribute("GUMOBOLIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuMoBoLiQiAfterXML = objReader.GetAttribute("GUMOBOLIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuMoBoLiQiBeforeXML = objReader.GetAttribute("GUMOBOLIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGuZaoXML = objReader.GetAttribute("GUZAOXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuZaoAfterXML = objReader.GetAttribute("GUZAOAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuZaoBeforeXML = objReader.GetAttribute("GUZAOBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKuoShiXML = objReader.GetAttribute("KUOSHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKuoShiAfterXML = objReader.GetAttribute("KUOSHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKuoShiBeforeXML = objReader.GetAttribute("KUOSHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLeiGuQiZiXML = objReader.GetAttribute("LEIGUQIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQiZiAfterXML = objReader.GetAttribute("LEIGUQIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQiZiBeforeXML = objReader.GetAttribute("LEIGUQIZIBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strDanChiLaGouXML = objReader.GetAttribute("DANCHILAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDanChiLaGouAfterXML = objReader.GetAttribute("DANCHILAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDanChiLaGouBeforeXML = objReader.GetAttribute("DANCHILAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoXiangQiXML = objReader.GetAttribute("DAOXIANGQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoXiangQiAfterXML = objReader.GetAttribute("DAOXIANGQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoXiangQiBeforeXML = objReader.GetAttribute("DAOXIANGQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJingGuQiZiXML = objReader.GetAttribute("JINGGUQIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJingGuQiZiAfterXML = objReader.GetAttribute("JINGGUQIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJingGuQiZiBeforeXML = objReader.GetAttribute("JINGGUQIZIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLaoHuQianXML = objReader.GetAttribute("LAOHUQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLaoHuQianAfterXML = objReader.GetAttribute("LAOHUQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLaoHuQianBeforeXML = objReader.GetAttribute("LAOHUQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLuoSiQiZiXML = objReader.GetAttribute("LUOSIQIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLuoSiQiZiAfterXML = objReader.GetAttribute("LUOSIQIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLuoSiQiZiBeforeXML = objReader.GetAttribute("LUOSIQIZIBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strJianBoLiZiXML = objReader.GetAttribute("JIANBOLIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJianBoLiZiAfterXML = objReader.GetAttribute("JIANBOLIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJianBoLiZiBeforeXML = objReader.GetAttribute("JIANBOLIZIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJingTuJianXML = objReader.GetAttribute("JINGTUJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJingTuJianAfterXML = objReader.GetAttribute("JINGTUJIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJingTuJianBeforeXML = objReader.GetAttribute("JINGTUJIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strQiangZhuangNieXML = objReader.GetAttribute("QIANGZHUANGNIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQiangZhuangNieAfterXML = objReader.GetAttribute("QIANGZHUANGNIEAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQiangZhuangNieBeforeXML = objReader.GetAttribute("QIANGZHUANGNIEBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShuiHeQianXML = objReader.GetAttribute("SHUIHEQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShuiHeQianAfterXML = objReader.GetAttribute("SHUIHEQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShuiHeQianBeforeXML = objReader.GetAttribute("SHUIHEQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhuiBanBoLiQiXML = objReader.GetAttribute("ZHUIBANBOLIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuiBanBoLiQiAfterXML = objReader.GetAttribute("ZHUIBANBOLIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuiBanBoLiQiBeforeXML = objReader.GetAttribute("ZHUIBANBOLIQIBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strBaiShiQianKaiQiXML = objReader.GetAttribute("BAISHIQIANKAIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBaiShiQianKaiQiAfterXML = objReader.GetAttribute("BAISHIQIANKAIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBaiShiQianKaiQiBeforeXML = objReader.GetAttribute("BAISHIQIANKAIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strChiBanQianXML = objReader.GetAttribute("CHIBANQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiBanQianAfterXML = objReader.GetAttribute("CHIBANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiBanQianBeforeXML = objReader.GetAttribute("CHIBANQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKaiLuZhuanXML = objReader.GetAttribute("KAILUZHUANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaiLuZhuanAfterXML = objReader.GetAttribute("KAILUZHUANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaiLuZhuanBeforeXML = objReader.GetAttribute("KAILUZHUANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strTouPiJianQianXML = objReader.GetAttribute("TOUPIJIANQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTouPiJianQianAfterXML = objReader.GetAttribute("TOUPIJIANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTouPiJianQianBeforeXML = objReader.GetAttribute("TOUPIJIANQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXianJuDaoYinZiXML = objReader.GetAttribute("XIANJUDAOYINZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXianJuDaoYinZiAfterXML = objReader.GetAttribute("XIANJUDAOYINZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXianJuDaoYinZiBeforeXML = objReader.GetAttribute("XIANJUDAOYINZIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXinErLaGouXML = objReader.GetAttribute("XINERLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinErLaGouAfterXML = objReader.GetAttribute("XINERLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinErLaGouBeforeXML = objReader.GetAttribute("XINERLAGOUBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChuanCiZhenXML = objReader.GetAttribute("CHUANCIZHENXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChuanCiZhenAfterXML = objReader.GetAttribute("CHUANCIZHENAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChuanCiZhenBeforeXML = objReader.GetAttribute("CHUANCIZHENBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strFeiYeDangBanXML = objReader.GetAttribute("FEIYEDANGBANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFeiYeDangBanAfterXML = objReader.GetAttribute("FEIYEDANGBANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFeiYeDangBanBeforeXML = objReader.GetAttribute("FEIYEDANGBANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strNaoMoGouXML = objReader.GetAttribute("NAOMOGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNaoMoGouAfterXML = objReader.GetAttribute("NAOMOGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNaoMoGouBeforeXML = objReader.GetAttribute("NAOMOGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXinFangLaGouXML = objReader.GetAttribute("XINFANGLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinFangLaGouAfterXML = objReader.GetAttribute("XINFANGLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinFangLaGouBeforeXML = objReader.GetAttribute("XINFANGLAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYinDingQianXML = objReader.GetAttribute("YINDINGQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYinDingQianAfterXML = objReader.GetAttribute("YINDINGQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYinDingQianBeforeXML = objReader.GetAttribute("YINDINGQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhuAZhuDuanQianXML = objReader.GetAttribute("ZHUAZHUDUANQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuAZhuDuanQianAfterXML = objReader.GetAttribute("ZHUAZHUDUANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuAZhuDuanQianBeforeXML = objReader.GetAttribute("ZHUAZHUDUANQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strCeBanQiXML = objReader.GetAttribute("CEBANQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strCeBanQiAfterXML = objReader.GetAttribute("CEBANQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strCeBanQiBeforeXML = objReader.GetAttribute("CEBANQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoXianGouXML = objReader.GetAttribute("DAOXIANGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoXianGouAfterXML = objReader.GetAttribute("DAOXIANGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoXianGouBeforeXML = objReader.GetAttribute("DAOXIANGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strErJianBanKuoZhangQiXML = objReader.GetAttribute("ERJIANBANKUOZHANGQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strErJianBanKuoZhangQiAfterXML = objReader.GetAttribute("ERJIANBANKUOZHANGQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strErJianBanKuoZhangQiBeforeXML = objReader.GetAttribute("ERJIANBANKUOZHANGQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXinNeiZhiJiaoLaGouXML = objReader.GetAttribute("XINNEIZHIJIAOLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinNeiZhiJiaoLaGouAfterXML = objReader.GetAttribute("XINNEIZHIJIAOLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinNeiZhiJiaoLaGouBeforeXML = objReader.GetAttribute("XINNEIZHIJIAOLAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhuACeBiQianXML = objReader.GetAttribute("ZHUACEBIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuACeBiQiAfterXML = objReader.GetAttribute("ZHUACEBIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuACeBiQiBeforeXML = objReader.GetAttribute("ZHUACEBIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhuAYouLiQianXML = objReader.GetAttribute("ZHUAYOULIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuAYouLiQianAfterXML = objReader.GetAttribute("ZHUAYOULIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuAYouLiQianBeforeXML = objReader.GetAttribute("ZHUAYOULIQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strFuKuiXML = objReader.GetAttribute("FUKUIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuKuiAfterXML = objReader.GetAttribute("FUKUIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuKuiBeforeXML = objReader.GetAttribute("FUKUIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGongChiXML = objReader.GetAttribute("GONGCHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongChiAfterXML = objReader.GetAttribute("GONGCHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongChiBeforeXML = objReader.GetAttribute("GONGCHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKaChiXML = objReader.GetAttribute("KACHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaChiAfterXML = objReader.GetAttribute("KACHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaChiBeforeXML = objReader.GetAttribute("KACHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShenJingLaGouXML = objReader.GetAttribute("SHENJINGLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShenJingLaGouAfterXML = objReader.GetAttribute("SHENJINGLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShenJingLaGouBeforeXML = objReader.GetAttribute("SHENJINGLAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWuChuangNieXML = objReader.GetAttribute("WUCHUANGNIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChuangNieAfterXML = objReader.GetAttribute("WUCHUANGNIEAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChuangNieBeforeXML = objReader.GetAttribute("WUCHUANGNIEBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXueGuanJiaXML = objReader.GetAttribute("XUEGUANJIAXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXueGuanJiaAfterXML = objReader.GetAttribute("XUEGUANJIAAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXueGuanJiaBeforeXML = objReader.GetAttribute("XUEGUANJIABEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strGongGuaShiXML = objReader.GetAttribute("GONGGUASHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongGuaShiAfterXML = objReader.GetAttribute("GONGGUASHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongGuaShiBeforeXML = objReader.GetAttribute("GONGGUASHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGongJingQianXML = objReader.GetAttribute("GONGJINGQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongJingQianAfterXML = objReader.GetAttribute("GONGJINGQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongJingQianBeforeXML = objReader.GetAttribute("GONGJINGQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJiLiuBoLiZiXML = objReader.GetAttribute("JINSHUNIAOGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJiLiuBoLiZiAfterXML = objReader.GetAttribute("JINSHUNIAOGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJiLiuBoLiZiBeforeXML = objReader.GetAttribute("JINSHUNIAOGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKuoGongQiXML = objReader.GetAttribute("KUOGONGQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKuoGongQiAfterXML = objReader.GetAttribute("KUOGONGQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKuoGongQiBeforeXML = objReader.GetAttribute("KUOGONGQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strRenDaiQianXML = objReader.GetAttribute("RENDAIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strRenDaiQianAfterXML = objReader.GetAttribute("RENDAIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strRenDaiQianBeforeXML = objReader.GetAttribute("RENDAIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYinDaoLaGouXML = objReader.GetAttribute("YINDAOLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYinDaoLaGouAfterXML = objReader.GetAttribute("YINDAOLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYinDaoLaGouBeforeXML = objReader.GetAttribute("YINDAOLAGOUBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strFuGuoQianXML = objReader.GetAttribute("FUGUOQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuGuoQianAfterXML = objReader.GetAttribute("FUGUOQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuGuoQianBeforeXML = objReader.GetAttribute("FUGUOQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJinShuNiaoGouXML = objReader.GetAttribute("JINSHUNIAOGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJinShuNiaoGouAfterXML = objReader.GetAttribute("JINSHUNIAOGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJinShuNiaoGouBeforeXML = objReader.GetAttribute("JINSHUNIAOGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWuDaiChangDianXML = objReader.GetAttribute("WUDAICHANGDIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuDaiChangDianAfterXML = objReader.GetAttribute("WUDAICHANGDIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuDaiChangDianBeforeXML = objReader.GetAttribute("WUDAICHANGDIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWuDaiFangDianXML = objReader.GetAttribute("WUDAIFANGDIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuDaiFangDianAfterXML = objReader.GetAttribute("WUDAIFANGDIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuDaiFangDianBeforeXML = objReader.GetAttribute("WUDAIFANGDIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYouDaiChangDianXML = objReader.GetAttribute("YOUDAICHANGDIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouDaiChangDianAfterXML = objReader.GetAttribute("YOUDAICHANGDIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouDaiChangDianBeforeXML = objReader.GetAttribute("YOUDAICHANGDIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYouDaiFangDianXML = objReader.GetAttribute("YOUDAIFANGDIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouDaiFangDianAfterXML = objReader.GetAttribute("YOUDAIFANGDIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouDaiFangDianBeforeXML = objReader.GetAttribute("YOUDAIFANGDIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strFuNieYinLiuXML = objReader.GetAttribute("FUNIEYINLIUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuNieYinLiuAfterXML = objReader.GetAttribute("FUNIEYINLIUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuNieYinLiuBeforeXML = objReader.GetAttribute("FUNIEYINLIUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKaiLuMianXML = objReader.GetAttribute("KAILUMIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaiLuMianAfterXML = objReader.GetAttribute("KAILUMIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaiLuMianBeforeXML = objReader.GetAttribute("KAILUMIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strQuanGongShaXML = objReader.GetAttribute("QUANGONGSHAXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQuanGongShaAfterXML = objReader.GetAttribute("QUANGONGSHAAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQuanGongShaBeforeXML = objReader.GetAttribute("QUANGONGSHABEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShaKuaiXML = objReader.GetAttribute("SHAKUAIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaKuaiAfterXML = objReader.GetAttribute("SHAKUAIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaKuaiBeforeXML = objReader.GetAttribute("SHAKUAIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShaQiuXML = objReader.GetAttribute("SHAQIUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaQiuAfterXML = objReader.GetAttribute("SHAQIUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaQiuBeforeXML = objReader.GetAttribute("SHAQIUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWangShaXML = objReader.GetAttribute("WANGSHAXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWangShaAfterXML = objReader.GetAttribute("WANGSHAAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWangShaBeforeXML = objReader.GetAttribute("WANGSHABEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strBianDaiXML = objReader.GetAttribute("BIANDAIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBianDaiAfterXML = objReader.GetAttribute("BIANDAIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBianDaiBeforeXML = objReader.GetAttribute("BIANDAIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strChangQianTaoXML = objReader.GetAttribute("CHANGQIANTAOXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianTaoAfterXML = objReader.GetAttribute("CHANGQIANTAOAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianTaoBeforeXML = objReader.GetAttribute("CHANGQIANTAOBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strNiaoGuanXML = objReader.GetAttribute("NIAOGUANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNiaoGuanAfterXML = objReader.GetAttribute("NIAOGUANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNiaoGuanBeforeXML = objReader.GetAttribute("NIAOGUANBEFOREXML").ToString().Replace('��', '\'');

                                m_objPackage.m_objOperationEqipmentQtyXML = objOperationEqipmentQtyXML;


                                #endregion

                            }
                            break;
                    }
                }
                objclsOperationEquipmentPackage = m_objPackage;
            }
            return m_intReturnRows;
        }

        public long lngSelectDeletedDisply(string strInPatientID, string strInPatientDate, string strOpenDate, out clsOperationEquipmentPackage objclsOperationEquipmentPackage)
        {
            objclsOperationEquipmentPackage = null;
            #region �������
            string m_strReceivedXML = "";
            int m_intReturnRows = 0;
            //����Package
            clsOperationEquipmentPackage m_objPackage = new clsOperationEquipmentPackage();

            //��������
            clsOperationEqipmentQtyXML objOperationEqipmentQtyXML = new clsOperationEqipmentQtyXML();

            //�����ӱ�
            clsOperationEqipmentQtyContent objOperationEqipmentQtyContent = new clsOperationEqipmentQtyContent();
            #endregion

            long lngSucceed = 0;
            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetDeletedRecordXMLTable(strInPatientID, strInPatientDate, strOpenDate, ref m_strReceivedXML, ref m_intReturnRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (m_intReturnRows > 0)
            {
                XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {

                                #region �ӱ�
                                objOperationEqipmentQtyContent.strInPatientID = objReader.GetAttribute("INPATIENTID").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strInPatientDate = objReader.GetAttribute("INPATIENTDATE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strOpenDate = objReader.GetAttribute("OPENDATE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strModifyDate = objReader.GetAttribute("MODIFYDATE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strModifyUserID = objReader.GetAttribute("MODIFYUSERID").ToString().Replace('��', '\'');

                                //								objOperationEqipmentQtyContent.strOperationID=objReader.GetAttribute("INPATIENTID").ToString().Replace ('��','\'');
                                objOperationEqipmentQtyContent.strOperationName = objReader.GetAttribute("OPERATIONNAME").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWenWan125 = objReader.GetAttribute("WENWAN125").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWenWan125After = objReader.GetAttribute("WENWAN125AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWenWan125Before = objReader.GetAttribute("WENWAN125BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWenZhi125 = objReader.GetAttribute("WENZHI125").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWenZhi125After = objReader.GetAttribute("WENZHI125AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWenZhi125Before = objReader.GetAttribute("WENZHI125BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiaoWan14 = objReader.GetAttribute("XIAOWAN14").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaoWan14After = objReader.GetAttribute("XIAOWAN14AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaoWan14Before = objReader.GetAttribute("XIAOWAN14BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiaoZhi14 = objReader.GetAttribute("XIAOZHI14").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaoZhi14After = objReader.GetAttribute("XIAOZHI14AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaoZhi14Before = objReader.GetAttribute("XIAOZHI14BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhongWan16 = objReader.GetAttribute("ZHONGWAN16").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongWan16After = objReader.GetAttribute("ZHONGWAN16AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongWan16Before = objReader.GetAttribute("ZHONGWAN16BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhongZhi16 = objReader.GetAttribute("ZHONGZHI16").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongZhi16After = objReader.GetAttribute("ZHONGZHI16AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongZhi16Before = objReader.GetAttribute("ZHONGZHI16BEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strPiQian = objReader.GetAttribute("PIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strPiQianAfter = objReader.GetAttribute("PIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strPiQianBefore = objReader.GetAttribute("PIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhiYouChiXueGuanQian = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiYouChiXueGuanQianAfter = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiYouChiXueGuanQianBefore = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strQuanQian = objReader.GetAttribute("QUANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQuanQianAfter = objReader.GetAttribute("QUANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQuanQianBefore = objReader.GetAttribute("QUANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJinQian = objReader.GetAttribute("JINQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJinQianAfter = objReader.GetAttribute("JINQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJinQianBefore = objReader.GetAttribute("JINQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChiZhenQian18 = objReader.GetAttribute("CHIZHENQIAN18").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiZhenQian18After = objReader.GetAttribute("CHIZHENQIAN18AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiZhenQian18Before = objReader.GetAttribute("CHIZHENQIAN18BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYouChiNie = objReader.GetAttribute("YOUCHINIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouChiNieAfter = objReader.GetAttribute("YOUCHINIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouChiNieBefore = objReader.GetAttribute("YOUCHINIEBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strWuChiNie = objReader.GetAttribute("WUCHINIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChiNieAfter = objReader.GetAttribute("WUCHINIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChiNieBefore = objReader.GetAttribute("WUCHINIEBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangYaBan = objReader.GetAttribute("CHANGYABAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangYaBanAfter = objReader.GetAttribute("CHANGYABANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangYaBanBefore = objReader.GetAttribute("CHANGYABANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoBing3 = objReader.GetAttribute("DAOBING3").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing3After = objReader.GetAttribute("DAOBING3AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing3Before = objReader.GetAttribute("DAOBING3BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoBing4 = objReader.GetAttribute("DAOBING4").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing4After = objReader.GetAttribute("DAOBING4AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing4Before = objReader.GetAttribute("DAOBING4BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoBing7 = objReader.GetAttribute("DAOBING7").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing7After = objReader.GetAttribute("DAOBING7AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoBing7Before = objReader.GetAttribute("DAOBING7BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWuChiNie = objReader.GetAttribute("WUCHINIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChiNieAfter = objReader.GetAttribute("WUCHINIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChiNieBefore = objReader.GetAttribute("WUCHINIEBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strZhiZhuZhiJian = objReader.GetAttribute("ZHIZHUZHIJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiZhuZhiJianAfter = objReader.GetAttribute("ZHIZHUZHIJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiZhuZhiJianBefore = objReader.GetAttribute("ZHIZHUZHIJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanZhuZhiJian = objReader.GetAttribute("WANZHUZHIJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanZhuZhiJianAfter = objReader.GetAttribute("WANZHUZHIJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanZhuZhiJianBefore = objReader.GetAttribute("WANZHUZHIJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strBianTaoXianJian = objReader.GetAttribute("BIANTAOXIANJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBianTaoXianJianAfter = objReader.GetAttribute("BIANTAOXIANJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBianTaoXianJianBefore = objReader.GetAttribute("BIANTAOXIANJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiongQiangJian = objReader.GetAttribute("XIONGQIANGJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiongQiangJianAfter = objReader.GetAttribute("XIONGQIANGJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiongQiangJianBefore = objReader.GetAttribute("XIONGQIANGJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhiJiaoXiaoLaGou = objReader.GetAttribute("ZHIJIAOXIAOLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoXiaoLaGouAfter = objReader.GetAttribute("ZHIJIAOXIAOLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoXiaoLaGouBefore = objReader.GetAttribute("ZHIJIAOXIAOLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLanWeiLaGou = objReader.GetAttribute("LANWEILAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLanWeiLaGouAfter = objReader.GetAttribute("LANWEILAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLanWeiLaGouBefore = objReader.GetAttribute("LANWEILAGOUBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strZhongFuGou = objReader.GetAttribute("ZHONGFUGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongFuGouAfter = objReader.GetAttribute("ZHONGFUGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhongFuGouBefore = objReader.GetAttribute("ZHONGFUGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangYaGou = objReader.GetAttribute("CHANGYAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangYaGouAfter = objReader.GetAttribute("CHANGYAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangYaGouBefore = objReader.GetAttribute("CHANGYAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhiJiaoQian = objReader.GetAttribute("ZHIJIAOQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoQianAfter = objReader.GetAttribute("ZHIJIAOQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoQianBefore = objReader.GetAttribute("ZHIJIAOQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiaFuBuQianKaiQi = objReader.GetAttribute("XIAFUBUQIANKAIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaFuBuQianKaiQiAfter = objReader.GetAttribute("XIAFUBUQIANKAIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiaFuBuQianKaiQiBefore = objReader.GetAttribute("XIAFUBUQIANKAIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strTongQuan = objReader.GetAttribute("TONGQUAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTongQuanAfter = objReader.GetAttribute("TONGQUANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTongQuanBefore = objReader.GetAttribute("TONGQUANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXiYeGuan = objReader.GetAttribute("XIYEGUAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiYeGuanAfter = objReader.GetAttribute("XIYEGUANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXiYeGuanBefore = objReader.GetAttribute("XIYEGUANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaZhiQian = objReader.GetAttribute("DAZHIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaZhiQianAfter = objReader.GetAttribute("DAZHIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaZhiQianBefore = objReader.GetAttribute("DAZHIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhiJiaoGou = objReader.GetAttribute("ZHIJIAOGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoGouAfter = objReader.GetAttribute("ZHIJIAOGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhiJiaoGouBefore = objReader.GetAttribute("ZHIJIAOGOUBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strDaoPian = objReader.GetAttribute("DAOPIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoPianAfter = objReader.GetAttribute("DAOPIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoPianBefore = objReader.GetAttribute("DAOPIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanXueGuanQian18 = objReader.GetAttribute("WANXUEGUANQIAN18").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian18After = objReader.GetAttribute("WANXUEGUANQIAN18AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian18Before = objReader.GetAttribute("WANXUEGUANQIAN18BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanXueGuanQian22 = objReader.GetAttribute("WANXUEGUANQIAN22").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian22After = objReader.GetAttribute("WANXUEGUANQIAN22AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian22Before = objReader.GetAttribute("WANXUEGUANQIAN22BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangChiZhenQian25 = objReader.GetAttribute("CHANGCHIZHENQIAN25").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangChiZhenQian25After = objReader.GetAttribute("CHANGCHIZHENQIAN25AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangChiZhenQian25Before = objReader.GetAttribute("CHANGCHIZHENQIAN25BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strFengZhen = objReader.GetAttribute("FENGZHEN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFengZhenAfter = objReader.GetAttribute("FENGZHENAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFengZhenBefore = objReader.GetAttribute("FENGZHENBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strNianMoQian = objReader.GetAttribute("NIANMOQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNianMoQianAfter = objReader.GetAttribute("NIANMOQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNianMoQianBefore = objReader.GetAttribute("NIANMOQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShaLiQian = objReader.GetAttribute("SHALIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaLiQianAfter = objReader.GetAttribute("SHALIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaLiQianBefore = objReader.GetAttribute("SHALIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShaLiQian = objReader.GetAttribute("SHALIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaLiQianAfter = objReader.GetAttribute("SHALIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaLiQianBefore = objReader.GetAttribute("SHALIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanXueGuanQian20 = objReader.GetAttribute("WANXUEGUANQIAN20").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian20After = objReader.GetAttribute("WANXUEGUANQIAN20AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian20Before = objReader.GetAttribute("WANXUEGUANQIAN20BEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWanXueGuanQian25 = objReader.GetAttribute("WANXUEGUANQIAN25").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian25After = objReader.GetAttribute("WANXUEGUANQIAN25AFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWanXueGuanQian25Before = objReader.GetAttribute("WANXUEGUANQIAN25BEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strChangQianWan = objReader.GetAttribute("CHANGQIANWAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianWanAfter = objReader.GetAttribute("CHANGQIANWANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianWanBefore = objReader.GetAttribute("CHANGQIANWANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangQianZhi = objReader.GetAttribute("CHANGQIANZHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianZhiAfter = objReader.GetAttribute("CHANGQIANZHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianZhiBefore = objReader.GetAttribute("CHANGQIANZHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaWanXueGuanQian = objReader.GetAttribute("DAWANXUEGUANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaWanXueGuanQianAfter = objReader.GetAttribute("DAWANXUEGUANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaWanXueGuanQianBefore = objReader.GetAttribute("DAWANXUEGUANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strErYanHouChongXiQi = objReader.GetAttribute("ERYANHOUCHONGXIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strErYanHouChongXiQiAfter = objReader.GetAttribute("ERYANHOUCHONGXIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strErYanHouChongXiQiBefore = objReader.GetAttribute("ERYANHOUCHONGXIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShenDiQian = objReader.GetAttribute("SHENDIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShenDiQianAfter = objReader.GetAttribute("SHENDIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShenDiQianBefore = objReader.GetAttribute("SHENDIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShiQian = objReader.GetAttribute("SHIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShiQianAfter = objReader.GetAttribute("SHIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShiQianBefore = objReader.GetAttribute("SHIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWeiQian = objReader.GetAttribute("WEIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWeiQianAfter = objReader.GetAttribute("WEIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWeiQianBefore = objReader.GetAttribute("WEIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXinErQian = objReader.GetAttribute("XINERQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinErQiaAfter = objReader.GetAttribute("XINERQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinErQiaBefore = objReader.GetAttribute("XINERQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strDaGuJian = objReader.GetAttribute("DAGUJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaGuJianAfter = objReader.GetAttribute("DAGUJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaGuJianBefore = objReader.GetAttribute("DAGUJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDanDaoTanTiao = objReader.GetAttribute("DANDAOTANTIAO").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDanDaoTanTiaoAfter = objReader.GetAttribute("DANDAOTANTIAOAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDanDaoTanTiaoBefore = objReader.GetAttribute("DANDAOTANTIAOBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDiErLeiGuJian = objReader.GetAttribute("DIERLEIGUJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDiErLeiGuJianAfter = objReader.GetAttribute("DIERLEIGUJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDiErLeiGuJianBefore = objReader.GetAttribute("DIERLEIGUJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strFangTouYaoGuQian = objReader.GetAttribute("FANGTOUYAOGUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFangTouYaoGuQianAfter = objReader.GetAttribute("FANGTOUYAOGUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFangTouYaoGuQianBefore = objReader.GetAttribute("FANGTOUYAOGUQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strHeLongQi = objReader.GetAttribute("HELONGQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strHeLongQiAfter = objReader.GetAttribute("HELONGQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strHeLongQiBefore = objReader.GetAttribute("HELONGQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJianJiaGuLaGou = objReader.GetAttribute("JIANJIAGULAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJianJiaGuLaGouAfter = objReader.GetAttribute("JIANJIAGULAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJianJiaGuLaGouBefore = objReader.GetAttribute("JIANJIAGULAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLeiGuQianKaiQi = objReader.GetAttribute("LEIGUQIANKAIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQianKaiQiAfter = objReader.GetAttribute("LEIGUQIANKAIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQianKaiQiBefore = objReader.GetAttribute("LEIGUQIANKAIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLeiGuQiZi = objReader.GetAttribute("LEIGUQIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQiZiAfter = objReader.GetAttribute("LEIGUQIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQiZiBefore = objReader.GetAttribute("LEIGUQIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strTanZhenChu = objReader.GetAttribute("TANZHENCHU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTanZhenChuAfter = objReader.GetAttribute("TANZHENCHUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTanZhenChuBefore = objReader.GetAttribute("TANZHENCHUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strTanZhenXi = objReader.GetAttribute("TANZHENXI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTanZhenXiAfter = objReader.GetAttribute("TANZHENXIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTanZhenXiBefore = objReader.GetAttribute("TANZHENXIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYaoGuQian = objReader.GetAttribute("YAOGUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYaoGuQianAfter = objReader.GetAttribute("YAOGUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYaoGuQianBefore = objReader.GetAttribute("YAOGUQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strChiGuQian = objReader.GetAttribute("CHIGUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiGuQianAfter = objReader.GetAttribute("CHIGUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiGuQianBefore = objReader.GetAttribute("CHIGUQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDanChiLaGou = objReader.GetAttribute("DANCHILAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDanChiLaGouAfter = objReader.GetAttribute("DANCHILAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDanChiLaGouBefore = objReader.GetAttribute("DANCHILAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoXiangQi = objReader.GetAttribute("DAOXIANGQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoXiangQiAfter = objReader.GetAttribute("DAOXIANGQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoXiangQiBefore = objReader.GetAttribute("DAOXIANGQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGuChui = objReader.GetAttribute("GUCHUI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuChuiAfter = objReader.GetAttribute("GUCHUIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuChuiBefore = objReader.GetAttribute("GUCHUIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGuDao = objReader.GetAttribute("GUDAO").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuDaoAfter = objReader.GetAttribute("GUDAOAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuDaoBefore = objReader.GetAttribute("GUDAOBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGuZao = objReader.GetAttribute("GUZAO").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuZaoAfter = objReader.GetAttribute("GUZAOAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuZaoBefore = objReader.GetAttribute("GUZAOBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGuMoBoLiQi = objReader.GetAttribute("GUMOBOLIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuMoBoLiQiAfter = objReader.GetAttribute("GUMOBOLIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGuMoBoLiQiBefore = objReader.GetAttribute("GUMOBOLIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJingGuQiZi = objReader.GetAttribute("JINGGUQIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJingGuQiZiAfter = objReader.GetAttribute("JINGGUQIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJingGuQiZiBefore = objReader.GetAttribute("JINGGUQIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKuoShi = objReader.GetAttribute("KUOSHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKuoShiAfter = objReader.GetAttribute("KUOSHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKuoShiBefore = objReader.GetAttribute("KUOSHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLeiGuQiZi = objReader.GetAttribute("LEIGUQIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQiZiAfter = objReader.GetAttribute("LEIGUQIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLeiGuQiZiBefore = objReader.GetAttribute("LEIGUQIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLaoHuQian = objReader.GetAttribute("LAOHUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLaoHuQianAfter = objReader.GetAttribute("LAOHUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLaoHuQianBefore = objReader.GetAttribute("LAOHUQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strLuoSiQiZi = objReader.GetAttribute("LUOSIQIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLuoSiQiZiAfter = objReader.GetAttribute("LUOSIQIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strLuoSiQiZiBefore = objReader.GetAttribute("LUOSIQIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strPingHengFuWeiQian = objReader.GetAttribute("PINGHENGFUWEIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strPingHengFuWeiQianAfter = objReader.GetAttribute("PINGHENGFUWEIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strPingHengFuWeiQianBefore = objReader.GetAttribute("PINGHENGFUWEIQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strBaiShiQianKaiQi = objReader.GetAttribute("BAISHIQIANKAIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBaiShiQianKaiQiAfter = objReader.GetAttribute("BAISHIQIANKAIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBaiShiQianKaiQiBefore = objReader.GetAttribute("BAISHIQIANKAIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChiBanQian = objReader.GetAttribute("CHIBANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiBanQianAfter = objReader.GetAttribute("CHIBANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChiBanQianBefore = objReader.GetAttribute("CHIBANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJianBoLiZi = objReader.GetAttribute("JIANBOLIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJianBoLiZiAfter = objReader.GetAttribute("JIANBOLIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJianBoLiZiBefore = objReader.GetAttribute("JIANBOLIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJingTuJian = objReader.GetAttribute("JINGTUJIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJingTuJianAfter = objReader.GetAttribute("JINGTUJIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJingTuJianBefore = objReader.GetAttribute("JINGTUJIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKaiLuZhuan = objReader.GetAttribute("KAILUZHUAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaiLuZhuanAfter = objReader.GetAttribute("KAILUZHUANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaiLuZhuanBefore = objReader.GetAttribute("KAILUZHUANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strQiangZhuangNie = objReader.GetAttribute("QIANGZHUANGNIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQiangZhuangNieAfter = objReader.GetAttribute("QIANGZHUANGNIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQiangZhuangNieBefore = objReader.GetAttribute("QIANGZHUANGNIEBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShuiHeQian = objReader.GetAttribute("SHUIHEQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShuiHeQianAfter = objReader.GetAttribute("SHUIHEQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShuiHeQianBefore = objReader.GetAttribute("SHUIHEQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strTouPiJianQian = objReader.GetAttribute("TOUPIJIANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTouPiJianQianAfter = objReader.GetAttribute("TOUPIJIANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strTouPiJianQianBefore = objReader.GetAttribute("TOUPIJIANQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXianJuDaoYinZi = objReader.GetAttribute("XIANJUDAOYINZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXianJuDaoYinZiAfter = objReader.GetAttribute("XIANJUDAOYINZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXianJuDaoYinZiBefore = objReader.GetAttribute("XIANJUDAOYINZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXinErLaGou = objReader.GetAttribute("XINERLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinErLaGouAfter = objReader.GetAttribute("XINERLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinErLaGouBefore = objReader.GetAttribute("XINERLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuiBanBoLiQi = objReader.GetAttribute("ZHUIBANBOLIQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuiBanBoLiQiAfter = objReader.GetAttribute("ZHUIBANBOLIQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuiBanBoLiQiBefore = objReader.GetAttribute("ZHUIBANBOLIQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuiBanYaoGuQian = objReader.GetAttribute("ZHUIBANYAOGUQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuiBanYaoGuQianAfter = objReader.GetAttribute("ZHUIBANYAOGUQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuiBanYaoGuQianBefore = objReader.GetAttribute("ZHUIBANYAOGUQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strCeBanQi = objReader.GetAttribute("CEBANQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strCeBanQiAfter = objReader.GetAttribute("CEBANQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strCeBanQiBefore = objReader.GetAttribute("CEBANQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChuanCiZhen = objReader.GetAttribute("CHUANCIZHEN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChuanCiZhenAfter = objReader.GetAttribute("CHUANCIZHENAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChuanCiZhenBefore = objReader.GetAttribute("CHUANCIZHENBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strDaoXianGou = objReader.GetAttribute("DAOXIANGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoXianGouAfter = objReader.GetAttribute("DAOXIANGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strDaoXianGouBefore = objReader.GetAttribute("DAOXIANGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strErJianBanKuoZhangQi = objReader.GetAttribute("ERJIANBANKUOZHANGQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strErJianBanKuoZhangQiAfter = objReader.GetAttribute("ERJIANBANKUOZHANGQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strErJianBanKuoZhangQiBefore = objReader.GetAttribute("ERJIANBANKUOZHANGQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strFeiYeDangBan = objReader.GetAttribute("FEIYEDANGBAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFeiYeDangBanAfter = objReader.GetAttribute("FEIYEDANGBANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFeiYeDangBanBefore = objReader.GetAttribute("FEIYEDANGBANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strNaoMoGou = objReader.GetAttribute("NAOMOGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNaoMoGouAfter = objReader.GetAttribute("NAOMOGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNaoMoGouBefore = objReader.GetAttribute("NAOMOGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXinFangLaGou = objReader.GetAttribute("XINFANGLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinFangLaGouAfter = objReader.GetAttribute("XINFANGLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinFangLaGouBefore = objReader.GetAttribute("XINFANGLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGou = objReader.GetAttribute("XINNEIZHIJIAOLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGouAfter = objReader.GetAttribute("XINNEIZHIJIAOLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXinNeiZhiJiaoLaGouBefore = objReader.GetAttribute("XINNEIZHIJIAOLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYinDingQian = objReader.GetAttribute("YINDINGQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYinDingQianAfter = objReader.GetAttribute("YINDINGQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYinDingQianBefore = objReader.GetAttribute("YINDINGQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuACeBiQian = objReader.GetAttribute("ZHUACEBIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuACeBiQiAfter = objReader.GetAttribute("ZHUACEBIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuACeBiQiBefore = objReader.GetAttribute("ZHUACEBIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuAYouLiQian = objReader.GetAttribute("ZHUAYOULIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuAYouLiQianAfter = objReader.GetAttribute("ZHUAYOULIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuAYouLiQianBefore = objReader.GetAttribute("ZHUAYOULIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strZhuAZhuDuanQian = objReader.GetAttribute("ZHUAZHUDUANQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuAZhuDuanQianAfter = objReader.GetAttribute("ZHUAZHUDUANQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strZhuAZhuDuanQianBefore = objReader.GetAttribute("ZHUAZHUDUANQIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strFuKui = objReader.GetAttribute("FUKUI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuKuiAfter = objReader.GetAttribute("FUKUIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuKuiBefore = objReader.GetAttribute("FUKUIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGongChi = objReader.GetAttribute("GONGCHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongChiAfter = objReader.GetAttribute("GONGCHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongChiBefore = objReader.GetAttribute("GONGCHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGongGuaShi = objReader.GetAttribute("GONGGUASHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongGuaShiAfter = objReader.GetAttribute("GONGGUASHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongGuaShiBefore = objReader.GetAttribute("GONGGUASHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strGongJingQian = objReader.GetAttribute("GONGJINGQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongJingQianAfter = objReader.GetAttribute("GONGJINGQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strGongJingQianBefore = objReader.GetAttribute("GONGJINGQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJiLiuBoLiZi = objReader.GetAttribute("JILIUBOLIZI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJiLiuBoLiZiAfter = objReader.GetAttribute("JILIUBOLIZIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJiLiuBoLiZiBefore = objReader.GetAttribute("JILIUBOLIZIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKaChi = objReader.GetAttribute("KACHI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaChiAfter = objReader.GetAttribute("KACHIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaChiBefore = objReader.GetAttribute("KACHIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKuoGongQi = objReader.GetAttribute("KUOGONGQI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKuoGongQiAfter = objReader.GetAttribute("KUOGONGQIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKuoGongQiBefore = objReader.GetAttribute("KUOGONGQIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strRenDaiQian = objReader.GetAttribute("RENDAIQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strRenDaiQianAfter = objReader.GetAttribute("RENDAIQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strRenDaiQianBefore = objReader.GetAttribute("RENDAIQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShenJingLaGou = objReader.GetAttribute("SHENJINGLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShenJingLaGouAfter = objReader.GetAttribute("SHENJINGLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShenJingLaGouBefore = objReader.GetAttribute("SHENJINGLAGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWuChuangNie = objReader.GetAttribute("WUCHUANGNIE").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChuangNieAfter = objReader.GetAttribute("WUCHUANGNIEAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuChuangNieBefore = objReader.GetAttribute("WUCHUANGNIEBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strXueGuanJia = objReader.GetAttribute("XUEGUANJIA").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXueGuanJiaAfter = objReader.GetAttribute("XUEGUANJIAAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strXueGuanJiaBefore = objReader.GetAttribute("XUEGUANJIABEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYinDaoLaGou = objReader.GetAttribute("YINDAOLAGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYinDaoLaGouAfter = objReader.GetAttribute("YINDAOLAGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYinDaoLaGouBefore = objReader.GetAttribute("YINDAOLAGOUBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strFuGuoQian = objReader.GetAttribute("FUGUOQIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuGuoQianAfter = objReader.GetAttribute("FUGUOQIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuGuoQianBefore = objReader.GetAttribute("FUGUOQIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strFuNieYinLiu = objReader.GetAttribute("FUNIEYINLIU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuNieYinLiuAfter = objReader.GetAttribute("FUNIEYINLIUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strFuNieYinLiuBefore = objReader.GetAttribute("FUNIEYINLIUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strJinShuNiaoGou = objReader.GetAttribute("JINSHUNIAOGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJinShuNiaoGouAfter = objReader.GetAttribute("JINSHUNIAOGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strJinShuNiaoGouBefore = objReader.GetAttribute("JINSHUNIAOGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strKaiLuMian = objReader.GetAttribute("KAILUMIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaiLuMianAfter = objReader.GetAttribute("KAILUMIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strKaiLuMianBefore = objReader.GetAttribute("KAILUMIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strQuanGongSha = objReader.GetAttribute("QUANGONGSHA").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQuanGongShaAfter = objReader.GetAttribute("QUANGONGSHAAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strQuanGongShaBefore = objReader.GetAttribute("QUANGONGSHABEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShaKuai = objReader.GetAttribute("SHAKUAI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaKuaiAfter = objReader.GetAttribute("SHAKUAIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaKuaiBefore = objReader.GetAttribute("SHAKUAIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strShaQiu = objReader.GetAttribute("SHAQIU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaQiuAfter = objReader.GetAttribute("SHAQIUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strShaQiuBefore = objReader.GetAttribute("SHAQIUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWangSha = objReader.GetAttribute("WANGSHA").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWangShaAfter = objReader.GetAttribute("WANGSHAAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWangShaBefore = objReader.GetAttribute("WANGSHABEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWuDaiChangDian = objReader.GetAttribute("WUDAICHANGDIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuDaiChangDianAfter = objReader.GetAttribute("WUDAICHANGDIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuDaiChangDianBefore = objReader.GetAttribute("WUDAICHANGDIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strWuDaiFangDian = objReader.GetAttribute("WUDAIFANGDIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuDaiFangDianAfter = objReader.GetAttribute("WUDAIFANGDIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strWuDaiFangDianBefore = objReader.GetAttribute("WUDAIFANGDIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYouDaiChangDian = objReader.GetAttribute("YOUDAICHANGDIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouDaiChangDianAfter = objReader.GetAttribute("YOUDAICHANGDIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouDaiChangDianBefore = objReader.GetAttribute("YOUDAICHANGDIANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strYouDaiFangDian = objReader.GetAttribute("YOUDAIFANGDIAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouDaiFangDianAfter = objReader.GetAttribute("YOUDAIFANGDIANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strYouDaiFangDianBefore = objReader.GetAttribute("YOUDAIFANGDIANBEFORE").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyContent.strBianDai = objReader.GetAttribute("BIANDAI").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBianDaiAfter = objReader.GetAttribute("BIANDAIAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strBianDaiBefore = objReader.GetAttribute("BIANDAIBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strChangQianTao = objReader.GetAttribute("CHANGQIANTAO").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianTaoAfter = objReader.GetAttribute("CHANGQIANTAOAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strChangQianTaoBefore = objReader.GetAttribute("CHANGQIANTAOBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyContent.strNiaoGuan = objReader.GetAttribute("NIAOGUAN").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNiaoGuanAfter = objReader.GetAttribute("NIAOGUANAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyContent.strNiaoGuanBefore = objReader.GetAttribute("NIAOGUANBEFORE").ToString().Replace('��', '\'');


                                m_objPackage.m_objOperationEqipmentQtyContent = objOperationEqipmentQtyContent;

                                #endregion

                                #region ����
                                objOperationEqipmentQtyXML.strInPatientID = objReader.GetAttribute("INPATIENTID");
                                objOperationEqipmentQtyXML.strInPatientDate = objReader.GetAttribute("INPATIENTDATE");
                                objOperationEqipmentQtyXML.strOpenDate = objReader.GetAttribute("OPENDATE");

                                objOperationEqipmentQtyXML.strCreateDate = objReader.GetAttribute("CREATEDATE");
                                objOperationEqipmentQtyXML.strCreateUserID = objReader.GetAttribute("CREATEUSERID");

                                //								objOperationEqipmentQtyContentInsert.strOperationID=objReader.GetAttribute("INPATIENTID").ToString().Replace ('��','\'');
                                objOperationEqipmentQtyXML.strOperationNameXML = objReader.GetAttribute("OPERATIONNAMEXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWenWan125XML = objReader.GetAttribute("WENWAN125XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWenWan125AfterXML = objReader.GetAttribute("WENWAN125AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWenWan125BeforeXML = objReader.GetAttribute("WENWAN125BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWenZhi125XML = objReader.GetAttribute("WENZHI125XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWenZhi125AfterXML = objReader.GetAttribute("WENZHI125AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWenZhi125BeforeXML = objReader.GetAttribute("WENZHI125BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiaoWan14XML = objReader.GetAttribute("XIAOWAN14XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaoWan14AfterXML = objReader.GetAttribute("XIAOWAN14AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaoWan14BeforeXML = objReader.GetAttribute("XIAOWAN14BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiaoZhi14XML = objReader.GetAttribute("XIAOZHI14XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaoZhi14AfterXML = objReader.GetAttribute("XIAOZHI14AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaoZhi14BeforeXML = objReader.GetAttribute("XIAOZHI14BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhongWan16XML = objReader.GetAttribute("ZHONGWAN16XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongWan16AfterXML = objReader.GetAttribute("ZHONGWAN16AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongWan16BeforeXML = objReader.GetAttribute("ZHONGWAN16BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhongZhi16XML = objReader.GetAttribute("ZHONGZHI16XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongZhi16AfterXML = objReader.GetAttribute("ZHONGZHI16AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongZhi16BeforeXML = objReader.GetAttribute("ZHONGZHI16BEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChiZhenQian18XML = objReader.GetAttribute("CHIZHENQIAN18XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiZhenQian18AfterXML = objReader.GetAttribute("CHIZHENQIAN18AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiZhenQian18BeforeXML = objReader.GetAttribute("CHIZHENQIAN18BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJinQianAfterXML = objReader.GetAttribute("JINQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJinQianBeforeXML = objReader.GetAttribute("JINQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJinQianXML = objReader.GetAttribute("JINQIANXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strPiQianAfterXML = objReader.GetAttribute("PIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strPiQianBeforeXML = objReader.GetAttribute("PIQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strPiQianXML = objReader.GetAttribute("PIQIANXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strQuanQianAfterXML = objReader.GetAttribute("QUANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQuanQianBeforeXML = objReader.GetAttribute("QUANQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQuanQianXML = objReader.GetAttribute("QUANQIANXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhiYouChiXueGuanQianAfterXML = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiYouChiXueGuanQianBeforeXML = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiYouChiXueGuanQianXML = objReader.GetAttribute("ZHIYOUCHIXUEGUANQIANXML").ToString().Replace('��', '\'');



                                objOperationEqipmentQtyXML.strYouChiNieXML = objReader.GetAttribute("YOUCHINIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouChiNieBeforeXML = objReader.GetAttribute("YOUCHINIEBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouChiNieAfterXML = objReader.GetAttribute("YOUCHINIEAFTERXML").ToString().Replace('��', '\'');


                                objOperationEqipmentQtyXML.strPingHengFuWeiQianXML = objReader.GetAttribute("PINGHENGFUWEIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strPingHengFuWeiQianBeforeXML = objReader.GetAttribute("PINGHENGFUWEIQIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strPingHengFuWeiQianAfterXML = objReader.GetAttribute("PINGHENGFUWEIQIANAFTERXML").ToString().Replace('��', '\'');

                                //************************************************
                                objOperationEqipmentQtyXML.strWuChiNieXML = objReader.GetAttribute("WUCHINIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChiNieAfterXML = objReader.GetAttribute("WUCHINIEAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChiNieBeforeXML = objReader.GetAttribute("WUCHINIEBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strChangYaBanXML = objReader.GetAttribute("CHANGYABANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangYaBanAfterXML = objReader.GetAttribute("CHANGYABANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangYaBanBeforeXML = objReader.GetAttribute("CHANGYABANBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoBing3XML = objReader.GetAttribute("DAOBING3XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing3AfterXML = objReader.GetAttribute("DAOBING3AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing3BeforeXML = objReader.GetAttribute("DAOBING3BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoBing4XML = objReader.GetAttribute("DAOBING4XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing4AfterXML = objReader.GetAttribute("DAOBING4AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing4BeforeXML = objReader.GetAttribute("DAOBING4BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoBing7XML = objReader.GetAttribute("DAOBING7XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing7AfterXML = objReader.GetAttribute("DAOBING7AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoBing7BeforeXML = objReader.GetAttribute("DAOBING7BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWuChiNieXML = objReader.GetAttribute("WUCHINIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChiNieAfterXML = objReader.GetAttribute("WUCHINIEAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChiNieBeforeXML = objReader.GetAttribute("WUCHINIEBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strZhiZhuZhiJianXML = objReader.GetAttribute("ZHIZHUZHIJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiZhuZhiJianBeforeXML = objReader.GetAttribute("ZHIZHUZHIJIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiZhuZhiJianAfterXML = objReader.GetAttribute("ZHIZHUZHIJIANAFTERXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhiJiaoXiaoLaGouXML = objReader.GetAttribute("ZHIJIAOXIAOLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoXiaoLaGouBeforeXML = objReader.GetAttribute("ZHIJIAOXIAOLAGOUBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoXiaoLaGouAfterXML = objReader.GetAttribute("ZHIJIAOXIAOLAGOUAFTERXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiongQiangJianXML = objReader.GetAttribute("XIONGQIANGJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiongQiangJianAfterXML = objReader.GetAttribute("XIONGQIANGJIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiongQiangJianBeforeXML = objReader.GetAttribute("XIONGQIANGJIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanZhuZhiJianXML = objReader.GetAttribute("WANZHUZHIJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanZhuZhiJianBeforeXML = objReader.GetAttribute("WANZHUZHIJIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanZhuZhiJianAfterXML = objReader.GetAttribute("WANZHUZHIJIANAFTERXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLanWeiLaGouXML = objReader.GetAttribute("LANWEILAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLanWeiLaGouAfterXML = objReader.GetAttribute("LANWEILAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLanWeiLaGouBeforeXML = objReader.GetAttribute("LANWEILAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strBianTaoXianJianXML = objReader.GetAttribute("BIANTAOXIANJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBianTaoXianJianBeforeXML = objReader.GetAttribute("BIANTAOXIANJIANBEFOREXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBianTaoXianJianAfterXML = objReader.GetAttribute("BIANTAOXIANJIANAFTERXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChangYaGouXML = objReader.GetAttribute("CHANGYAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangYaGouAfterXML = objReader.GetAttribute("CHANGYAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangYaGouBeforeXML = objReader.GetAttribute("CHANGYAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strTongQuanXML = objReader.GetAttribute("TONGQUANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTongQuanAfterXML = objReader.GetAttribute("TONGQUANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTongQuanBeforeXML = objReader.GetAttribute("TONGQUANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiXML = objReader.GetAttribute("XIAFUBUQIANKAIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiAfterXML = objReader.GetAttribute("XIAFUBUQIANKAIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiBeforeXML = objReader.GetAttribute("XIAFUBUQIANKAIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiXML = objReader.GetAttribute("XIAFUBUQIANKAIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiAfterXML = objReader.GetAttribute("XIAFUBUQIANKAIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiaFuBuQianKaiQiBeforeXML = objReader.GetAttribute("XIAFUBUQIANKAIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXiYeGuanXML = objReader.GetAttribute("XIYEGUANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiYeGuanAfterXML = objReader.GetAttribute("XIYEGUANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXiYeGuanBeforeXML = objReader.GetAttribute("XIYEGUANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhiJiaoGouXML = objReader.GetAttribute("ZHIJIAOGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoGouAfterXML = objReader.GetAttribute("ZHIJIAOGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoGouBeforeXML = objReader.GetAttribute("ZHIJIAOGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhongFuGouXML = objReader.GetAttribute("ZHONGFUGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongFuGouAfterXML = objReader.GetAttribute("ZHONGFUGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhongFuGouBeforeXML = objReader.GetAttribute("ZHONGFUGOUBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strNianMoQianXML = objReader.GetAttribute("NIANMOQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNianMoQianAfterXML = objReader.GetAttribute("NIANMOQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNianMoQianBeforeXML = objReader.GetAttribute("NIANMOQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShaLiQianXML = objReader.GetAttribute("SHALIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaLiQianAfterXML = objReader.GetAttribute("SHALIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaLiQianBeforeXML = objReader.GetAttribute("SHALIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanXueGuanQian18XML = objReader.GetAttribute("WANXUEGUANQIAN18XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian18AfterXML = objReader.GetAttribute("WANXUEGUANQIAN18AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian18BeforeXML = objReader.GetAttribute("WANXUEGUANQIAN18BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanXueGuanQian20XML = objReader.GetAttribute("WANXUEGUANQIAN20XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian20AfterXML = objReader.GetAttribute("WANXUEGUANQIAN20AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian20BeforeXML = objReader.GetAttribute("WANXUEGUANQIAN20BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanXueGuanQian22XML = objReader.GetAttribute("WANXUEGUANQIAN22XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian22AfterXML = objReader.GetAttribute("WANXUEGUANQIAN22AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian22BeforeXML = objReader.GetAttribute("WANXUEGUANQIAN22BEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChangChiZhenQian25XML = objReader.GetAttribute("CHANGCHIZHENQIAN25XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangChiZhenQian25AfterXML = objReader.GetAttribute("CHANGCHIZHENQIAN25AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangChiZhenQian25BeforeXML = objReader.GetAttribute("CHANGCHIZHENQIAN25BEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoPianXML = objReader.GetAttribute("DAOPIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoPianAfterXML = objReader.GetAttribute("DAOPIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoPianBeforeXML = objReader.GetAttribute("DAOPIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaZhiQianXML = objReader.GetAttribute("DAZHIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaZhiQianAfterXML = objReader.GetAttribute("DAZHIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaZhiQianBeforeXML = objReader.GetAttribute("DAZHIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strFengZhenXML = objReader.GetAttribute("FENGZHENXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFengZhenAfterXML = objReader.GetAttribute("FENGZHENAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFengZhenBeforeXML = objReader.GetAttribute("FENGZHENBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhiJiaoQianXML = objReader.GetAttribute("ZHIJIAOQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoQianAfterXML = objReader.GetAttribute("ZHIJIAOQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhiJiaoQianBeforeXML = objReader.GetAttribute("ZHIJIAOQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChangQianZhiXML = objReader.GetAttribute("CHANGQIANZHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianZhiAfterXML = objReader.GetAttribute("CHANGQIANZHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianZhiBeforeXML = objReader.GetAttribute("CHANGQIANZHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaWanXueGuanQianXML = objReader.GetAttribute("DAWANXUEGUANQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaWanXueGuanQianAfterXML = objReader.GetAttribute("DAWANXUEGUANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaWanXueGuanQianBeforeXML = objReader.GetAttribute("DAWANXUEGUANQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShenDiQianXML = objReader.GetAttribute("SHENDIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShenDiQianAfterXML = objReader.GetAttribute("SHENDIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShenDiQianBeforeXML = objReader.GetAttribute("SHENDIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWanXueGuanQian25XML = objReader.GetAttribute("WANXUEGUANQIAN25XML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian25AfterXML = objReader.GetAttribute("WANXUEGUANQIAN25AFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWanXueGuanQian25BeforeXML = objReader.GetAttribute("WANXUEGUANQIAN25BEFOREXML").ToString().Replace('��', '\'');

                                //************************************************
                                objOperationEqipmentQtyXML.strChangQianWanXML = objReader.GetAttribute("CHANGQIANWANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianWanAfterXML = objReader.GetAttribute("CHANGQIANWANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianWanBeforeXML = objReader.GetAttribute("CHANGQIANWANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strErYanHouChongXiQiXML = objReader.GetAttribute("ERYANHOUCHONGXIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strErYanHouChongXiQiAfterXML = objReader.GetAttribute("ERYANHOUCHONGXIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strErYanHouChongXiQiBeforeXML = objReader.GetAttribute("ERYANHOUCHONGXIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShiQianXML = objReader.GetAttribute("SHIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShiQianAfterXML = objReader.GetAttribute("SHIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShiQianBeforeXML = objReader.GetAttribute("SHIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWeiQianXML = objReader.GetAttribute("WEIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWeiQianAfterXML = objReader.GetAttribute("WEIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWeiQianBeforeXML = objReader.GetAttribute("WEIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXinErQianXML = objReader.GetAttribute("XINERQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinErQiaAfterXML = objReader.GetAttribute("XINERQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinErQiaBeforeXML = objReader.GetAttribute("XINERQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strDanDaoTanTiaoXML = objReader.GetAttribute("DANDAOTANTIAOXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDanDaoTanTiaoAfterXML = objReader.GetAttribute("DANDAOTANTIAOAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDanDaoTanTiaoBeforeXML = objReader.GetAttribute("DANDAOTANTIAOBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strHeLongQiXML = objReader.GetAttribute("HELONGQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strHeLongQiAfterXML = objReader.GetAttribute("HELONGQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strHeLongQiBeforeXML = objReader.GetAttribute("HELONGQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLeiGuQianKaiQiXML = objReader.GetAttribute("LEIGUQIANKAIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQianKaiQiAfterXML = objReader.GetAttribute("LEIGUQIANKAIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQianKaiQiBeforeXML = objReader.GetAttribute("LEIGUQIANKAIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strTanZhenChuXML = objReader.GetAttribute("TANZHENCHUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTanZhenChuAfterXML = objReader.GetAttribute("TANZHENCHUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTanZhenChuBeforeXML = objReader.GetAttribute("TANZHENCHUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strTanZhenXiXML = objReader.GetAttribute("TANZHENXIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTanZhenXiAfterXML = objReader.GetAttribute("TANZHENXIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTanZhenXiBeforeXML = objReader.GetAttribute("TANZHENXIBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strDaGuJianXML = objReader.GetAttribute("DAGUJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaGuJianAfterXML = objReader.GetAttribute("DAGUJIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaGuJianBeforeXML = objReader.GetAttribute("DAGUJIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDiErLeiGuJianXML = objReader.GetAttribute("DIERLEIGUJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDiErLeiGuJianAfterXML = objReader.GetAttribute("DIERLEIGUJIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDiErLeiGuJianBeforeXML = objReader.GetAttribute("DIERLEIGUJIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strFangTouYaoGuQianXML = objReader.GetAttribute("FANGTOUYAOGUQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFangTouYaoGuQianAfterXML = objReader.GetAttribute("FANGTOUYAOGUQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFangTouYaoGuQianBeforeXML = objReader.GetAttribute("FANGTOUYAOGUQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJianJiaGuLaGouXML = objReader.GetAttribute("JIANJIAGULAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJianJiaGuLaGouAfterXML = objReader.GetAttribute("JIANJIAGULAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJianJiaGuLaGouBeforeXML = objReader.GetAttribute("JIANJIAGULAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLeiGuQiZiXML = objReader.GetAttribute("LEIGUQIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQiZiAfterXML = objReader.GetAttribute("LEIGUQIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQiZiBeforeXML = objReader.GetAttribute("LEIGUQIZIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYaoGuQianXML = objReader.GetAttribute("YAOGUQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYaoGuQianAfterXML = objReader.GetAttribute("YAOGUQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYaoGuQianBeforeXML = objReader.GetAttribute("YAOGUQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChiGuQianXML = objReader.GetAttribute("CHIGUQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiGuQianAfterXML = objReader.GetAttribute("CHIGUQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiGuQianBeforeXML = objReader.GetAttribute("CHIGUQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGuChuiXML = objReader.GetAttribute("GUCHUIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuChuiAfterXML = objReader.GetAttribute("GUCHUIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuChuiBeforeXML = objReader.GetAttribute("GUCHUIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGuDaoXML = objReader.GetAttribute("GUDAOXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuDaoAfterXML = objReader.GetAttribute("GUDAOAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuDaoBeforeXML = objReader.GetAttribute("GUDAOBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGuMoBoLiQiXML = objReader.GetAttribute("GUMOBOLIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuMoBoLiQiAfterXML = objReader.GetAttribute("GUMOBOLIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuMoBoLiQiBeforeXML = objReader.GetAttribute("GUMOBOLIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGuZaoXML = objReader.GetAttribute("GUZAOXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuZaoAfterXML = objReader.GetAttribute("GUZAOAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGuZaoBeforeXML = objReader.GetAttribute("GUZAOBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKuoShiXML = objReader.GetAttribute("KUOSHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKuoShiAfterXML = objReader.GetAttribute("KUOSHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKuoShiBeforeXML = objReader.GetAttribute("KUOSHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLeiGuQiZiXML = objReader.GetAttribute("LEIGUQIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQiZiAfterXML = objReader.GetAttribute("LEIGUQIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLeiGuQiZiBeforeXML = objReader.GetAttribute("LEIGUQIZIBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strDanChiLaGouXML = objReader.GetAttribute("DANCHILAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDanChiLaGouAfterXML = objReader.GetAttribute("DANCHILAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDanChiLaGouBeforeXML = objReader.GetAttribute("DANCHILAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoXiangQiXML = objReader.GetAttribute("DAOXIANGQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoXiangQiAfterXML = objReader.GetAttribute("DAOXIANGQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoXiangQiBeforeXML = objReader.GetAttribute("DAOXIANGQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJingGuQiZiXML = objReader.GetAttribute("JINGGUQIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJingGuQiZiAfterXML = objReader.GetAttribute("JINGGUQIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJingGuQiZiBeforeXML = objReader.GetAttribute("JINGGUQIZIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLaoHuQianXML = objReader.GetAttribute("LAOHUQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLaoHuQianAfterXML = objReader.GetAttribute("LAOHUQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLaoHuQianBeforeXML = objReader.GetAttribute("LAOHUQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strLuoSiQiZiXML = objReader.GetAttribute("LUOSIQIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLuoSiQiZiAfterXML = objReader.GetAttribute("LUOSIQIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strLuoSiQiZiBeforeXML = objReader.GetAttribute("LUOSIQIZIBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strJianBoLiZiXML = objReader.GetAttribute("JIANBOLIZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJianBoLiZiAfterXML = objReader.GetAttribute("JIANBOLIZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJianBoLiZiBeforeXML = objReader.GetAttribute("JIANBOLIZIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJingTuJianXML = objReader.GetAttribute("JINGTUJIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJingTuJianAfterXML = objReader.GetAttribute("JINGTUJIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJingTuJianBeforeXML = objReader.GetAttribute("JINGTUJIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strQiangZhuangNieXML = objReader.GetAttribute("QIANGZHUANGNIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQiangZhuangNieAfterXML = objReader.GetAttribute("QIANGZHUANGNIEAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQiangZhuangNieBeforeXML = objReader.GetAttribute("QIANGZHUANGNIEBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShuiHeQianXML = objReader.GetAttribute("SHUIHEQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShuiHeQianAfterXML = objReader.GetAttribute("SHUIHEQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShuiHeQianBeforeXML = objReader.GetAttribute("SHUIHEQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhuiBanBoLiQiXML = objReader.GetAttribute("ZHUIBANBOLIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuiBanBoLiQiAfterXML = objReader.GetAttribute("ZHUIBANBOLIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuiBanBoLiQiBeforeXML = objReader.GetAttribute("ZHUIBANBOLIQIBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strBaiShiQianKaiQiXML = objReader.GetAttribute("BAISHIQIANKAIQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBaiShiQianKaiQiAfterXML = objReader.GetAttribute("BAISHIQIANKAIQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBaiShiQianKaiQiBeforeXML = objReader.GetAttribute("BAISHIQIANKAIQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strChiBanQianXML = objReader.GetAttribute("CHIBANQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiBanQianAfterXML = objReader.GetAttribute("CHIBANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChiBanQianBeforeXML = objReader.GetAttribute("CHIBANQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKaiLuZhuanXML = objReader.GetAttribute("KAILUZHUANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaiLuZhuanAfterXML = objReader.GetAttribute("KAILUZHUANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaiLuZhuanBeforeXML = objReader.GetAttribute("KAILUZHUANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strTouPiJianQianXML = objReader.GetAttribute("TOUPIJIANQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTouPiJianQianAfterXML = objReader.GetAttribute("TOUPIJIANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strTouPiJianQianBeforeXML = objReader.GetAttribute("TOUPIJIANQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXianJuDaoYinZiXML = objReader.GetAttribute("XIANJUDAOYINZIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXianJuDaoYinZiAfterXML = objReader.GetAttribute("XIANJUDAOYINZIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXianJuDaoYinZiBeforeXML = objReader.GetAttribute("XIANJUDAOYINZIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXinErLaGouXML = objReader.GetAttribute("XINERLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinErLaGouAfterXML = objReader.GetAttribute("XINERLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinErLaGouBeforeXML = objReader.GetAttribute("XINERLAGOUBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strChuanCiZhenXML = objReader.GetAttribute("CHUANCIZHENXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChuanCiZhenAfterXML = objReader.GetAttribute("CHUANCIZHENAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChuanCiZhenBeforeXML = objReader.GetAttribute("CHUANCIZHENBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strFeiYeDangBanXML = objReader.GetAttribute("FEIYEDANGBANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFeiYeDangBanAfterXML = objReader.GetAttribute("FEIYEDANGBANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFeiYeDangBanBeforeXML = objReader.GetAttribute("FEIYEDANGBANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strNaoMoGouXML = objReader.GetAttribute("NAOMOGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNaoMoGouAfterXML = objReader.GetAttribute("NAOMOGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNaoMoGouBeforeXML = objReader.GetAttribute("NAOMOGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXinFangLaGouXML = objReader.GetAttribute("XINFANGLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinFangLaGouAfterXML = objReader.GetAttribute("XINFANGLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinFangLaGouBeforeXML = objReader.GetAttribute("XINFANGLAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYinDingQianXML = objReader.GetAttribute("YINDINGQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYinDingQianAfterXML = objReader.GetAttribute("YINDINGQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYinDingQianBeforeXML = objReader.GetAttribute("YINDINGQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhuAZhuDuanQianXML = objReader.GetAttribute("ZHUAZHUDUANQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuAZhuDuanQianAfterXML = objReader.GetAttribute("ZHUAZHUDUANQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuAZhuDuanQianBeforeXML = objReader.GetAttribute("ZHUAZHUDUANQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strCeBanQiXML = objReader.GetAttribute("CEBANQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strCeBanQiAfterXML = objReader.GetAttribute("CEBANQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strCeBanQiBeforeXML = objReader.GetAttribute("CEBANQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strDaoXianGouXML = objReader.GetAttribute("DAOXIANGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoXianGouAfterXML = objReader.GetAttribute("DAOXIANGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strDaoXianGouBeforeXML = objReader.GetAttribute("DAOXIANGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strErJianBanKuoZhangQiXML = objReader.GetAttribute("ERJIANBANKUOZHANGQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strErJianBanKuoZhangQiAfterXML = objReader.GetAttribute("ERJIANBANKUOZHANGQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strErJianBanKuoZhangQiBeforeXML = objReader.GetAttribute("ERJIANBANKUOZHANGQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXinNeiZhiJiaoLaGouXML = objReader.GetAttribute("XINNEIZHIJIAOLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinNeiZhiJiaoLaGouAfterXML = objReader.GetAttribute("XINNEIZHIJIAOLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXinNeiZhiJiaoLaGouBeforeXML = objReader.GetAttribute("XINNEIZHIJIAOLAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhuACeBiQianXML = objReader.GetAttribute("ZHUACEBIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuACeBiQiAfterXML = objReader.GetAttribute("ZHUACEBIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuACeBiQiBeforeXML = objReader.GetAttribute("ZHUACEBIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strZhuAYouLiQianXML = objReader.GetAttribute("ZHUAYOULIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuAYouLiQianAfterXML = objReader.GetAttribute("ZHUAYOULIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strZhuAYouLiQianBeforeXML = objReader.GetAttribute("ZHUAYOULIQIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strFuKuiXML = objReader.GetAttribute("FUKUIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuKuiAfterXML = objReader.GetAttribute("FUKUIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuKuiBeforeXML = objReader.GetAttribute("FUKUIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGongChiXML = objReader.GetAttribute("GONGCHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongChiAfterXML = objReader.GetAttribute("GONGCHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongChiBeforeXML = objReader.GetAttribute("GONGCHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKaChiXML = objReader.GetAttribute("KACHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaChiAfterXML = objReader.GetAttribute("KACHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaChiBeforeXML = objReader.GetAttribute("KACHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShenJingLaGouXML = objReader.GetAttribute("SHENJINGLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShenJingLaGouAfterXML = objReader.GetAttribute("SHENJINGLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShenJingLaGouBeforeXML = objReader.GetAttribute("SHENJINGLAGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWuChuangNieXML = objReader.GetAttribute("WUCHUANGNIEXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChuangNieAfterXML = objReader.GetAttribute("WUCHUANGNIEAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuChuangNieBeforeXML = objReader.GetAttribute("WUCHUANGNIEBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strXueGuanJiaXML = objReader.GetAttribute("XUEGUANJIAXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXueGuanJiaAfterXML = objReader.GetAttribute("XUEGUANJIAAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strXueGuanJiaBeforeXML = objReader.GetAttribute("XUEGUANJIABEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strGongGuaShiXML = objReader.GetAttribute("GONGGUASHIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongGuaShiAfterXML = objReader.GetAttribute("GONGGUASHIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongGuaShiBeforeXML = objReader.GetAttribute("GONGGUASHIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strGongJingQianXML = objReader.GetAttribute("GONGJINGQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongJingQianAfterXML = objReader.GetAttribute("GONGJINGQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strGongJingQianBeforeXML = objReader.GetAttribute("GONGJINGQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJiLiuBoLiZiXML = objReader.GetAttribute("JINSHUNIAOGOU").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJiLiuBoLiZiAfterXML = objReader.GetAttribute("JINSHUNIAOGOUAFTER").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJiLiuBoLiZiBeforeXML = objReader.GetAttribute("JINSHUNIAOGOUBEFORE").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKuoGongQiXML = objReader.GetAttribute("KUOGONGQIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKuoGongQiAfterXML = objReader.GetAttribute("KUOGONGQIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKuoGongQiBeforeXML = objReader.GetAttribute("KUOGONGQIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strRenDaiQianXML = objReader.GetAttribute("RENDAIQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strRenDaiQianAfterXML = objReader.GetAttribute("RENDAIQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strRenDaiQianBeforeXML = objReader.GetAttribute("RENDAIQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYinDaoLaGouXML = objReader.GetAttribute("YINDAOLAGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYinDaoLaGouAfterXML = objReader.GetAttribute("YINDAOLAGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYinDaoLaGouBeforeXML = objReader.GetAttribute("YINDAOLAGOUBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strFuGuoQianXML = objReader.GetAttribute("FUGUOQIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuGuoQianAfterXML = objReader.GetAttribute("FUGUOQIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuGuoQianBeforeXML = objReader.GetAttribute("FUGUOQIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strJinShuNiaoGouXML = objReader.GetAttribute("JINSHUNIAOGOUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJinShuNiaoGouAfterXML = objReader.GetAttribute("JINSHUNIAOGOUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strJinShuNiaoGouBeforeXML = objReader.GetAttribute("JINSHUNIAOGOUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWuDaiChangDianXML = objReader.GetAttribute("WUDAICHANGDIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuDaiChangDianAfterXML = objReader.GetAttribute("WUDAICHANGDIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuDaiChangDianBeforeXML = objReader.GetAttribute("WUDAICHANGDIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWuDaiFangDianXML = objReader.GetAttribute("WUDAIFANGDIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuDaiFangDianAfterXML = objReader.GetAttribute("WUDAIFANGDIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWuDaiFangDianBeforeXML = objReader.GetAttribute("WUDAIFANGDIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYouDaiChangDianXML = objReader.GetAttribute("YOUDAICHANGDIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouDaiChangDianAfterXML = objReader.GetAttribute("YOUDAICHANGDIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouDaiChangDianBeforeXML = objReader.GetAttribute("YOUDAICHANGDIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strYouDaiFangDianXML = objReader.GetAttribute("YOUDAIFANGDIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouDaiFangDianAfterXML = objReader.GetAttribute("YOUDAIFANGDIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strYouDaiFangDianBeforeXML = objReader.GetAttribute("YOUDAIFANGDIANBEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strFuNieYinLiuXML = objReader.GetAttribute("FUNIEYINLIUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuNieYinLiuAfterXML = objReader.GetAttribute("FUNIEYINLIUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strFuNieYinLiuBeforeXML = objReader.GetAttribute("FUNIEYINLIUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strKaiLuMianXML = objReader.GetAttribute("KAILUMIANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaiLuMianAfterXML = objReader.GetAttribute("KAILUMIANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strKaiLuMianBeforeXML = objReader.GetAttribute("KAILUMIANBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strQuanGongShaXML = objReader.GetAttribute("QUANGONGSHAXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQuanGongShaAfterXML = objReader.GetAttribute("QUANGONGSHAAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strQuanGongShaBeforeXML = objReader.GetAttribute("QUANGONGSHABEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShaKuaiXML = objReader.GetAttribute("SHAKUAIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaKuaiAfterXML = objReader.GetAttribute("SHAKUAIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaKuaiBeforeXML = objReader.GetAttribute("SHAKUAIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strShaQiuXML = objReader.GetAttribute("SHAQIUXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaQiuAfterXML = objReader.GetAttribute("SHAQIUAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strShaQiuBeforeXML = objReader.GetAttribute("SHAQIUBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strWangShaXML = objReader.GetAttribute("WANGSHAXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWangShaAfterXML = objReader.GetAttribute("WANGSHAAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strWangShaBeforeXML = objReader.GetAttribute("WANGSHABEFOREXML").ToString().Replace('��', '\'');


                                //************************************************
                                objOperationEqipmentQtyXML.strBianDaiXML = objReader.GetAttribute("BIANDAIXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBianDaiAfterXML = objReader.GetAttribute("BIANDAIAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strBianDaiBeforeXML = objReader.GetAttribute("BIANDAIBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strChangQianTaoXML = objReader.GetAttribute("CHANGQIANTAOXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianTaoAfterXML = objReader.GetAttribute("CHANGQIANTAOAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strChangQianTaoBeforeXML = objReader.GetAttribute("CHANGQIANTAOBEFOREXML").ToString().Replace('��', '\'');

                                objOperationEqipmentQtyXML.strNiaoGuanXML = objReader.GetAttribute("NIAOGUANXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNiaoGuanAfterXML = objReader.GetAttribute("NIAOGUANAFTERXML").ToString().Replace('��', '\'');
                                objOperationEqipmentQtyXML.strNiaoGuanBeforeXML = objReader.GetAttribute("NIAOGUANBEFOREXML").ToString().Replace('��', '\'');

                                m_objPackage.m_objOperationEqipmentQtyXML = objOperationEqipmentQtyXML;


                                #endregion

                            }
                            break;
                    }
                }
                objclsOperationEquipmentPackage = m_objPackage;
            }
            return m_intReturnRows;
        }

        /// <summary>
        /// ���ǰ�ж��û����������ʱ���Ƿ��ظ�
        /// </summary>
        /// <param name="strInPatientID"></param>
        /// <param name="strInPatientDate"></param>
        /// <param name="strCreateDate">�û����������ʱ��</param>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public long lngSelectBeforSave(string strInPatientID, string strInPatientDate, string strCreateDate, out int Rows)
        {
            int m_intRows = 0;
            long lngSucceed = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.m_lngSelectBeforeSave(strInPatientID, strInPatientDate, strCreateDate, out m_intRows);
                Rows = m_intRows;
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngSucceed;

        }
        /// <summary>
        /// ���еĵ������ʱ��
        /// </summary>		
        public long m_lngGetTimeInfoOfAPatientArr(string p_strInPatientID, string p_strInPatientDate, out DateTime[] dtmOpenDateArr, out DateTime[] dtmCreateDateArr)
        {
            dtmOpenDateArr = null;
            dtmCreateDateArr = null;
            if (p_strInPatientID == null || p_strInPatientID == "")
                return -1;

            string strXml = "";
            int intRows = 0;
            long lngRes = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr()).Service.clsOperationEquipmentQtyService_m_lngGetTimeInfoOfAPatient(p_strInPatientID, p_strInPatientDate, ref strXml, ref intRows);

                if (lngRes > 0 && intRows > 0)
                {
                    dtmOpenDateArr = new DateTime[intRows];
                    dtmCreateDateArr = new DateTime[intRows];

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
                                    dtmOpenDateArr[intIndex] = DateTime.Parse(objReader.GetAttribute("OPENDATE"));
                                    dtmCreateDateArr[intIndex] = DateTime.Parse(objReader.GetAttribute("CREATEDATE"));
                                    intIndex++;
                                }
                                break;
                        }
                    }
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngRes;
        }

        /// <summary>
        /// ���еĻ�ʿ
        /// </summary>
        /// <param name="strInPatientID"></param>
        /// <param name="strInPatientDate"></param>
        /// <param name="strOpenDate"></param>
        /// <param name="Rows"></param>
        /// <returns></returns>

        public long m_lngGetOperation_Nurse(string strInPatientID, string strInPatientDate, string strOpenDate, out clsOperationNurse[] objclsOperationNurseArr)
        {
            objclsOperationNurseArr = null;
            string m_strReceivedXML = "";
            int m_intReturnRows = 0;
            long lngSucceed = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.clsOperationEquipmentQtyService_m_lngGetOperation_Nurse(strInPatientID, strInPatientDate, strOpenDate, ref m_strReceivedXML, ref m_intReturnRows);
                if (m_intReturnRows > 0)
                {
                    XmlTextReader objReader = new XmlTextReader(m_strReceivedXML, XmlNodeType.Element, m_objXmlParser);
                    objReader.WhitespaceHandling = WhitespaceHandling.None;
                    objclsOperationNurseArr = new clsOperationNurse[m_intReturnRows];
                    int intIndex = 0;
                    while (objReader.Read())
                    {
                        switch (objReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (objReader.HasAttributes)
                                {
                                    objclsOperationNurseArr[intIndex] = new clsOperationNurse();
                                    objclsOperationNurseArr[intIndex].strNurseID = objReader.GetAttribute("NURSEID");
                                    objclsOperationNurseArr[intIndex].strNurseName = objReader.GetAttribute("NURSENAME").Trim();
                                    objclsOperationNurseArr[intIndex].strNurseFlag = objReader.GetAttribute("NURSEFLAG");
                                    intIndex++;
                                }
                                break;
                        }
                    }
                }
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngSucceed;

        }

        /// <summary>
        /// ģ������ҽ��ID��
        /// </summary>
        /// <param name="strOperator"></param>
        /// <param name="bolSuccess"></param>
        /// <returns></returns>
        public System.Windows.Forms.ListViewItem[] m_lviGetEmployee(string strOperator, ref bool bolSuccess)
        {

            System.Windows.Forms.ListViewItem[] item1 = null;
            string strSetXML = "";
            int intRows = 0;

            long lngSucceed = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.clsOperationEquipmentQtyService_lngXMLLikeQuery_Doctor(strOperator, ref strSetXML, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (intRows > 0)
            {
                item1 = new System.Windows.Forms.ListViewItem[intRows];
                XmlTextReader objReader = new XmlTextReader(strSetXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;
                int intIndex = 0;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                item1[intIndex] = new System.Windows.Forms.ListViewItem(objReader.GetAttribute("EMPLOYEEID").ToString().Replace('��', '\''));
                                item1[intIndex].SubItems.Add(objReader.GetAttribute("FIRSTNAME").Trim().Replace('��', '\''));
                                intIndex++;
                            }
                            break;
                    }

                }
                bolSuccess = true;

            }
            else
                bolSuccess = false;

            return item1;

        }

        public long m_lngGetEmployee(string strOperatorLike, out clsEmployeeIDName[] objclsEmployeeIDNameArr)
        {
            objclsEmployeeIDNameArr = null;
            string strSetXML = "";
            int intRows = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                long lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.clsOperationEquipmentQtyService_lngXMLLikeQuery_Doctor(strOperatorLike, ref strSetXML, ref intRows);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            if (intRows > 0)
            {
                objclsEmployeeIDNameArr = new clsEmployeeIDName[intRows];
                XmlTextReader objReader = new XmlTextReader(strSetXML, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;
                int intIndex = 0;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.HasAttributes)
                            {
                                objclsEmployeeIDNameArr[intIndex] = new clsEmployeeIDName();
                                objclsEmployeeIDNameArr[intIndex].strEmployeeID = objReader.GetAttribute("EMPLOYEEID");
                                objclsEmployeeIDNameArr[intIndex].strEmployeeName = objReader.GetAttribute("FIRSTNAME").Trim().Replace('��', '\'');
                                intIndex++;
                            }
                            break;
                    }
                }

            }

            return intRows;
        }


        public long m_lngDelete(string p_strDeActivedOperatorID, string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            long lngSucceed = 0;

            //com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService m_objServ =
            //    (com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.OperationEquipmentQtyService.clsOperationEquipmentQtyService));

            try
            {
                lngSucceed = (new weCare.Proxy.ProxyEmr()).Service.clsOperationEquipmentQtyService_m_lngDelete(p_strDeActivedOperatorID, p_strInPatientID, p_strInPatientDate, p_strOpenDate);
            }
            finally
            {
                //m_objServ.Dispose();
            }
            return lngSucceed;
        }
    }

    //	#region �������
    //	/// <summary>
    //	/// �������
    //	/// </summary>	
    //	[Serializable]
    //	public class clsOperationEqipmentQtyXML
    //	{
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //		public string strCreateDate;//�û�����Ĵ���ʱ��
    //		public string strCreateUserID;
    //		public string strStatus;
    //		public string strIfConfirm;
    //		public string strDeActivedDate;
    //		public string strDeActivedOperatorID;
    //		public string strConfirmReason;
    //		public string strConfirmReasonXML;
    //
    //		public string strOperationIDXML;
    //		public string strOperationNameXML;
    //
    //		public string strWenZhi125XML;
    //		public string strWenZhi125BeforeXML;
    //		public string strWenZhi125AfterXML;
    //
    //		public string strWenWan125XML;
    //		public string strWenWan125BeforeXML;
    //		public string strWenWan125AfterXML;
    //
    //		public string strXiaoZhi14XML;
    //		public string strXiaoZhi14BeforeXML;
    //		public string strXiaoZhi14AfterXML;
    //
    //		public string strXiaoWan14XML;
    //		public string strXiaoWan14BeforeXML;
    //		public string strXiaoWan14AfterXML;
    //
    //		public string strZhongZhi16XML;
    //		public string strZhongZhi16BeforeXML;
    //		public string strZhongZhi16AfterXML;
    //
    //		public string strZhongWan16XML;
    //		public string strZhongWan16BeforeXML;
    //		public string strZhongWan16AfterXML;
    //	
    //		public string strChiZhenQian18XML;
    //		public string strChiZhenQian18BeforeXML;
    //		public string strChiZhenQian18AfterXML;
    //
    //		public string strJinQianXML;
    //		public string strJinQianBeforeXML;
    //		public string strJinQianAfterXML;
    //
    //		public string strQuanQianXML;
    //		public string strQuanQianBeforeXML;
    //		public string strQuanQianAfterXML;
    //
    //		public string strZhiYouChiXueGuanQianXML;
    //		public string strZhiYouChiXueGuanQianBeforeXML;
    //		public string strZhiYouChiXueGuanQianAfterXML;
    //
    //		public string strPiQianXML;
    //		public string strPiQianBeforeXML;
    //		public string strPiQianAfterXML;
    //	
    //
    //		public string strYouChiNieXML;
    //		public string strYouChiNieBeforeXML;
    //		public string strYouChiNieAfterXML;
    //
    //		public string strDaoBing7XML;
    //		public string strDaoBing7BeforeXML;
    //		public string strDaoBing7AfterXML;
    //
    //		public string strDaoBing4XML;
    //		public string strDaoBing4BeforeXML;
    //		public string strDaoBing4AfterXML;
    //
    //		public string strDaoBing3XML;
    //		public string strDaoBing3BeforeXML;
    //		public string strDaoBing3AfterXML;
    //
    //		public string strChangYaBanXML;
    //		public string strChangYaBanBeforeXML;
    //		public string strChangYaBanAfterXML;
    //
    //		public string strWuChiNieXML;
    //		public string strWuChiNieBeforeXML;
    //		public string strWuChiNieAfterXML;
    //
    //	
    //
    //		public string strLanWeiLaGouXML;
    //		public string strLanWeiLaGouBeforeXML;
    //		public string strLanWeiLaGouAfterXML;
    //
    //		public string strZhiJiaoXiaoLaGouXML;
    //		public string strZhiJiaoXiaoLaGouBeforeXML;
    //		public string strZhiJiaoXiaoLaGouAfterXML;
    //
    //		public string strXiongQiangJianXML;
    //		public string strXiongQiangJianBeforeXML;
    //		public string strXiongQiangJianAfterXML;
    //
    //		public string strBianTaoXianJianXML;
    //		public string strBianTaoXianJianBeforeXML;
    //		public string strBianTaoXianJianAfterXML;
    //
    //		public string strZhiZhuZhiJianXML;
    //		public string strZhiZhuZhiJianBeforeXML;
    //		public string strZhiZhuZhiJianAfterXML;
    //
    //		public string strWanZhuZhiJianXML;
    //		public string strWanZhuZhiJianBeforeXML;
    //		public string strWanZhuZhiJianAfterXML;
    //	
    //
    //		public string strXiYeGuanXML;
    //		public string strXiYeGuanBeforeXML;
    //		public string strXiYeGuanAfterXML;
    //
    //		public string strTongQuanXML;
    //		public string strTongQuanBeforeXML;
    //		public string strTongQuanAfterXML;
    //
    //		public string strXiaFuBuQianKaiQiXML;
    //		public string strXiaFuBuQianKaiQiBeforeXML;
    //		public string strXiaFuBuQianKaiQiAfterXML;
    //
    //		public string strZhiJiaoGouXML;
    //		public string strZhiJiaoGouBeforeXML;
    //		public string strZhiJiaoGouAfterXML;
    //
    //		public string strChangYaGouXML;
    //		public string strChangYaGouBeforeXML;
    //		public string strChangYaGouAfterXML;
    //
    //		public string strZhongFuGouXML;
    //		public string strZhongFuGouBeforeXML;
    //		public string strZhongFuGouAfterXML;
    //	
    //
    //		public string strWanXueGuanQian22XML;
    //		public string strWanXueGuanQian22BeforeXML;
    //		public string strWanXueGuanQian22AfterXML;
    //
    //		public string strWanXueGuanQian20XML;
    //		public string strWanXueGuanQian20BeforeXML;
    //		public string strWanXueGuanQian20AfterXML;
    //
    //		public string strWanXueGuanQian18XML;
    //		public string strWanXueGuanQian18BeforeXML;
    //		public string strWanXueGuanQian18AfterXML;
    //
    //		public string strNianMoQianXML;
    //		public string strNianMoQianBeforeXML;
    //		public string strNianMoQianAfterXML;
    //
    //		public string strShaLiQianXML;
    //		public string strShaLiQianBeforeXML;
    //		public string strShaLiQianAfterXML;
    //	
    //
    //		public string strDaoPianXML;
    //		public string strDaoPianBeforeXML;
    //		public string strDaoPianAfterXML;
    //
    //		public string strFengZhenXML;
    //		public string strFengZhenBeforeXML;
    //		public string strFengZhenAfterXML;
    //
    //		public string strChangChiZhenQian25XML;
    //		public string strChangChiZhenQian25BeforeXML;
    //		public string strChangChiZhenQian25AfterXML;
    //
    //		public string strZhiJiaoQianXML;
    //		public string strZhiJiaoQianBeforeXML;
    //		public string strZhiJiaoQianAfterXML;
    //
    //		public string strDaZhiQianXML;
    //		public string strDaZhiQianBeforeXML;
    //		public string strDaZhiQianAfterXML;
    //
    //	
    //
    //		public string strWanXueGuanQian25XML;
    //		public string strWanXueGuanQian25BeforeXML;
    //		public string strWanXueGuanQian25AfterXML;
    //
    //		public string strDaWanXueGuanQianXML;
    //		public string strDaWanXueGuanQianBeforeXML;
    //		public string strDaWanXueGuanQianAfterXML;
    //
    //		public string strShenDiQianXML;
    //		public string strShenDiQianBeforeXML;
    //		public string strShenDiQianAfterXML;
    //
    //		public string strChangQianZhiXML;
    //		public string strChangQianZhiBeforeXML;
    //		public string strChangQianZhiAfterXML;
    //
    //	
    //
    //		public string strChangQianWanXML;
    //		public string strChangQianWanBeforeXML;
    //		public string strChangQianWanAfterXML;
    //
    //		public string strShiQianXML;
    //		public string strShiQianBeforeXML;
    //		public string strShiQianAfterXML;
    //
    //		public string strWeiQianXML;
    //		public string strWeiQianBeforeXML;
    //		public string strWeiQianAfterXML;
    //
    //		public string strXinErQianXML;
    //		public string strXinErQiaBeforeXML;
    //		public string strXinErQiaAfterXML;
    //
    //		public string strErYanHouChongXiQiXML;
    //		public string strErYanHouChongXiQiBeforeXML;
    //		public string strErYanHouChongXiQiAfterXML;
    //
    //	
    //
    //		public string strTanZhenChuXML;
    //		public string strTanZhenChuBeforeXML;
    //		public string strTanZhenChuAfterXML;
    //
    //		public string strTanZhenXiXML;
    //		public string strTanZhenXiBeforeXML;
    //		public string strTanZhenXiAfterXML;
    //
    //		public string strDanDaoTanTiaoXML;
    //		public string strDanDaoTanTiaoBeforeXML;
    //		public string strDanDaoTanTiaoAfterXML;
    //
    //		public string strLeiGuQianKaiQiXML;
    //		public string strLeiGuQianKaiQiBeforeXML;
    //		public string strLeiGuQianKaiQiAfterXML;
    //
    //		public string strHeLongQiXML;
    //		public string strHeLongQiBeforeXML;
    //		public string strHeLongQiAfterXML;
    //
    //	
    //
    //		public string strJianJiaGuLaGouXML;
    //		public string strJianJiaGuLaGouBeforeXML;
    //		public string strJianJiaGuLaGouAfterXML;
    //
    //		public string strLeiGuQiZiXML;
    //		public string strLeiGuQiZiBeforeXML;
    //		public string strLeiGuQiZiAfterXML;
    //
    //		public string strDaGuJianXML;
    //		public string strDaGuJianBeforeXML;
    //		public string strDaGuJianAfterXML;
    //
    //		public string strDiErLeiGuJianXML;
    //		public string strDiErLeiGuJianBeforeXML;
    //		public string strDiErLeiGuJianAfterXML;
    //
    //		public string strFangTouYaoGuQianXML;
    //		public string strFangTouYaoGuQianBeforeXML;
    //		public string strFangTouYaoGuQianAfterXML;
    //
    //		public string strYaoGuQianXML;
    //		public string strYaoGuQianBeforeXML;
    //		public string strYaoGuQianAfterXML;
    //	
    //
    //		public string strGuMoBoLiQiXML;
    //		public string strGuMoBoLiQiBeforeXML;
    //		public string strGuMoBoLiQiAfterXML;
    //
    //		public string strGuDaoXML;
    //		public string strGuDaoBeforeXML;
    //		public string strGuDaoAfterXML;
    //
    //		public string strGuZaoXML;
    //		public string strGuZaoBeforeXML;
    //		public string strGuZaoAfterXML;
    //
    //		public string strKuoShiXML;
    //		public string strKuoShiBeforeXML;
    //		public string strKuoShiAfterXML;
    //
    //		public string strGuChuiXML;
    //		public string strGuChuiBeforeXML;
    //		public string strGuChuiAfterXML;
    //
    //		public string strChiGuQianXML;
    //		public string strChiGuQianBeforeXML;
    //		public string strChiGuQianAfterXML;
    //
    //		
    //
    //		public string strJingGuQiZiXML;
    //		public string strJingGuQiZiBeforeXML;
    //		public string strJingGuQiZiAfterXML;
    //
    //		public string strDanChiLaGouXML;
    //		public string strDanChiLaGouBeforeXML;
    //		public string strDanChiLaGouAfterXML;
    //
    //		public string strLaoHuQianXML;
    //		public string strLaoHuQianBeforeXML;
    //		public string strLaoHuQianAfterXML;
    //
    //		public string strPingHengFuWeiQianXML;
    //		public string strPingHengFuWeiQianBeforeXML;
    //		public string strPingHengFuWeiQianAfterXML;
    //
    //		public string strLuoSiQiZiXML;
    //		public string strLuoSiQiZiBeforeXML;
    //		public string strLuoSiQiZiAfterXML;
    //
    //		public string strDaoXiangQiXML;
    //		public string strDaoXiangQiBeforeXML;
    //		public string strDaoXiangQiAfterXML;
    //
    //	
    //
    //		public string strZhuiBanYaoGuQianXML;
    //		public string strZhuiBanYaoGuQianBeforeXML;
    //		public string strZhuiBanYaoGuQianAfterXML;
    //
    //		public string strShuiHeQianXML;
    //		public string strShuiHeQianBeforeXML;
    //		public string strShuiHeQianAfterXML;
    //
    //		public string strJingTuJianXML;
    //		public string strJingTuJianBeforeXML;
    //		public string strJingTuJianAfterXML;
    //
    //		public string strZhuiBanBoLiQiXML;
    //		public string strZhuiBanBoLiQiBeforeXML;
    //		public string strZhuiBanBoLiQiAfterXML;
    //
    //		public string strJianBoLiZiXML;
    //		public string strJianBoLiZiBeforeXML;
    //		public string strJianBoLiZiAfterXML;
    //
    //		public string strQiangZhuangNieXML;
    //		public string strQiangZhuangNieBeforeXML;
    //		public string strQiangZhuangNieAfterXML;
    //
    //	
    //
    //		public string strBaiShiQianKaiQiXML;
    //		public string strBaiShiQianKaiQiBeforeXML;
    //		public string strBaiShiQianKaiQiAfterXML;
    //
    //		public string strKaiLuZhuanXML;
    //		public string strKaiLuZhuanBeforeXML;
    //		public string strKaiLuZhuanAfterXML;
    //
    //		public string strTouPiJianQianXML;
    //		public string strTouPiJianQianBeforeXML;
    //		public string strTouPiJianQianAfterXML;
    //
    //		public string strXianJuDaoYinZiXML;
    //		public string strXianJuDaoYinZiBeforeXML;
    //		public string strXianJuDaoYinZiAfterXML;
    //
    //		public string strXinErLaGouXML;
    //		public string strXinErLaGouBeforeXML;
    //		public string strXinErLaGouAfterXML;
    //
    //		public string strChiBanQianXML;
    //		public string strChiBanQianBeforeXML;
    //		public string strChiBanQianAfterXML;
    //
    //	
    //
    //		public string strXinFangLaGouXML;
    //		public string strXinFangLaGouBeforeXML;
    //		public string strXinFangLaGouAfterXML;
    //
    //		public string strNaoMoGouXML;
    //		public string strNaoMoGouBeforeXML;
    //		public string strNaoMoGouAfterXML;
    //
    //		public string strChuanCiZhenXML;
    //		public string strChuanCiZhenBeforeXML;
    //		public string strChuanCiZhenAfterXML;
    //
    //		public string strYinDingQianXML;
    //		public string strYinDingQianBeforeXML;
    //		public string strYinDingQianAfterXML;
    //
    //		public string strFeiYeDangBanXML;
    //		public string strFeiYeDangBanBeforeXML;
    //		public string strFeiYeDangBanAfterXML;
    //
    //		public string strZhuAZhuDuanQianXML;
    //		public string strZhuAZhuDuanQianBeforeXML;
    //		public string strZhuAZhuDuanQianAfterXML;
    //
    //
    //	
    //
    //		public string strZhuAYouLiQianXML;
    //		public string strZhuAYouLiQianBeforeXML;
    //		public string strZhuAYouLiQianAfterXML;
    //
    //		public string strZhuACeBiQianXML;
    //		public string strZhuACeBiQiBeforeXML;
    //		public string strZhuACeBiQiAfterXML;
    //
    //		public string strErJianBanKuoZhangQiXML;
    //		public string strErJianBanKuoZhangQiBeforeXML;
    //		public string strErJianBanKuoZhangQiAfterXML;
    //
    //		public string strCeBanQiXML;
    //		public string strCeBanQiBeforeXML;
    //		public string strCeBanQiAfterXML;
    //
    //		public string strXinNeiZhiJiaoLaGouXML;
    //		public string strXinNeiZhiJiaoLaGouBeforeXML;
    //		public string strXinNeiZhiJiaoLaGouAfterXML;
    //
    //		public string strDaoXianGouXML;
    //		public string strDaoXianGouBeforeXML;
    //		public string strDaoXianGouAfterXML;
    //	
    //		public string strWuChuangNieXML;
    //		public string strWuChuangNieAfterXML;
    //		public string strWuChuangNieBeforeXML;
    //
    //		public string strKaChiXML;
    //		public string strKaChiAfterXML;
    //		public string strKaChiBeforeXML;
    //
    //		public string strShenJingLaGouXML;
    //		public string strShenJingLaGouAfterXML;
    //		public string strShenJingLaGouBeforeXML;
    //
    //		public string strXueGuanJiaXML;
    //		public string strXueGuanJiaAfterXML;
    //		public string strXueGuanJiaBeforeXML;
    //
    //		public string strFuKuiXML;
    //		public string strFuKuiAfterXML;
    //		public string strFuKuiBeforeXML;
    //
    //		public string strGongChiXML;
    //		public string strGongChiAfterXML;
    //		public string strGongChiBeforeXML;
    //	
    //
    //		public string strGongGuaShiXML;
    //		public string strGongGuaShiAfterXML;
    //		public string strGongGuaShiBeforeXML;
    //
    //		public string strGongJingQianXML;
    //		public string strGongJingQianAfterXML;
    //		public string strGongJingQianBeforeXML;
    //
    //		public string strYinDaoLaGouXML;
    //		public string strYinDaoLaGouAfterXML;
    //		public string strYinDaoLaGouBeforeXML;
    //
    //		public string strRenDaiQianXML;
    //		public string strRenDaiQianAfterXML;
    //		public string strRenDaiQianBeforeXML;
    //
    //		public string strJiLiuBoLiZiXML;
    //		public string strJiLiuBoLiZiAfterXML;
    //		public string strJiLiuBoLiZiBeforeXML;
    //
    //		public string strKuoGongQiXML;
    //		public string strKuoGongQiAfterXML;
    //		public string strKuoGongQiBeforeXML;
    //	
    //		public string strFuGuoQianXML;
    //		public string strFuGuoQianAfterXML;
    //		public string strFuGuoQianBeforeXML;
    //
    //		public string strJinShuNiaoGouXML;
    //		public string strJinShuNiaoGouAfterXML;
    //		public string strJinShuNiaoGouBeforeXML;
    //
    //		public string strYouDaiFangDianXML;
    //		public string strYouDaiFangDianAfterXML;
    //		public string strYouDaiFangDianBeforeXML;
    //
    //		public string strWuDaiFangDianXML;
    //		public string strWuDaiFangDianAfterXML;
    //		public string strWuDaiFangDianBeforeXML;
    //
    //		public string strYouDaiChangDianXML;
    //		public string strYouDaiChangDianAfterXML;
    //		public string strYouDaiChangDianBeforeXML;
    //
    //		public string strWuDaiChangDianXML;
    //		public string strWuDaiChangDianAfterXML;
    //		public string strWuDaiChangDianBeforeXML;
    //	
    //
    //		public string strFuNieYinLiuXML;
    //		public string strFuNieYinLiuAfterXML;
    //		public string strFuNieYinLiuBeforeXML;
    //
    //		public string strKaiLuMianXML;
    //		public string strKaiLuMianAfterXML;
    //		public string strKaiLuMianBeforeXML;
    //
    //		public string strQuanGongShaXML;
    //		public string strQuanGongShaAfterXML;
    //		public string strQuanGongShaBeforeXML;
    //
    //		public string strWangShaXML;
    //		public string strWangShaAfterXML;
    //		public string strWangShaBeforeXML;
    //
    //		public string strShaKuaiXML;
    //		public string strShaKuaiAfterXML;
    //		public string strShaKuaiBeforeXML;
    //
    //		public string strShaQiuXML;
    //		public string strShaQiuAfterXML;
    //		public string strShaQiuBeforeXML;
    //	
    //
    //		public string strBianDaiXML;
    //		public string strBianDaiAfterXML;
    //		public string strBianDaiBeforeXML;
    //
    //		public string strChangQianTaoXML;
    //		public string strChangQianTaoAfterXML;
    //		public string strChangQianTaoBeforeXML;
    //
    //		public string strNiaoGuanXML;
    //		public string strNiaoGuanAfterXML;
    //		public string strNiaoGuanBeforeXML;
    //
    //	}
    //	
    //	
    //	#endregion
    //
    //	#region �ӱ����
    //	/// <summary>
    //	/// �ӱ����1
    //	/// </summary>
    //	[Serializable]
    //	public class clsOperationEqipmentQtyContent
    //	{
    //		public string strWenZhi125;
    //		public string strWenZhi125Before;
    //		public string strWenZhi125After;
    //
    //		public string strWenWan125;
    //		public string strWenWan125Before;
    //		public string strWenWan125After;
    //
    //		public string strXiaoZhi14;
    //		public string strXiaoZhi14Before;
    //		public string strXiaoZhi14After;
    //
    //		public string strXiaoWan14;
    //		public string strXiaoWan14Before;
    //		public string strXiaoWan14After;
    //
    //		public string strZhongZhi16;
    //		public string strZhongZhi16Before;
    //		public string strZhongZhi16After;
    //
    //		public string strZhongWan16;
    //		public string strZhongWan16Before;
    //		public string strZhongWan16After;
    //
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //		public string strModifyDate;
    //		public string strModifyUserID;
    //		public string strOperationID;
    //		public string strOperationName;
    //		
    //	
    //
    //		public string strYouChiNie;
    //		public string strYouChiNieBefore;
    //		public string strYouChiNieAfter;
    //
    //		public string strChiZhenQian18;
    //		public string strChiZhenQian18Before;
    //		public string strChiZhenQian18After;
    //
    //		public string strJinQian;
    //		public string strJinQianBefore;
    //		public string strJinQianAfter;
    //
    //		public string strQuanQian;
    //		public string strQuanQianBefore;
    //		public string strQuanQianAfter;
    //
    //		public string strZhiYouChiXueGuanQian;
    //		public string strZhiYouChiXueGuanQianBefore;
    //		public string strZhiYouChiXueGuanQianAfter;
    //
    //		public string strPiQian;
    //		public string strPiQianBefore;
    //		public string strPiQianAfter;
    //
    //		
    //	
    //
    //		public string strDaoBing7;
    //		public string strDaoBing7Before;
    //		public string strDaoBing7After;
    //
    //		public string strDaoBing4;
    //		public string strDaoBing4Before;
    //		public string strDaoBing4After;
    //
    //		public string strDaoBing3;
    //		public string strDaoBing3Before;
    //		public string strDaoBing3After;
    //
    //		public string strChangYaBan;
    //		public string strChangYaBanBefore;
    //		public string strChangYaBanAfter;
    //
    //		public string strWuChiNie;
    //		public string strWuChiNieBefore;
    //		public string strWuChiNieAfter;
    //	
    //
    //		public string strLanWeiLaGou;
    //		public string strLanWeiLaGouBefore;
    //		public string strLanWeiLaGouAfter;
    //
    //		public string strZhiJiaoXiaoLaGou;
    //		public string strZhiJiaoXiaoLaGouBefore;
    //		public string strZhiJiaoXiaoLaGouAfter;
    //
    //		public string strXiongQiangJian;
    //		public string strXiongQiangJianBefore;
    //		public string strXiongQiangJianAfter;
    //
    //		public string strBianTaoXianJian;
    //		public string strBianTaoXianJianBefore;
    //		public string strBianTaoXianJianAfter;
    //
    //		public string strZhiZhuZhiJian;
    //		public string strZhiZhuZhiJianBefore;
    //		public string strZhiZhuZhiJianAfter;
    //
    //		public string strWanZhuZhiJian;
    //		public string strWanZhuZhiJianBefore;
    //		public string strWanZhuZhiJianAfter;
    //
    //	
    //
    //		public string strXiYeGuan;
    //		public string strXiYeGuanBefore;
    //		public string strXiYeGuanAfter;
    //
    //		public string strTongQuan;
    //		public string strTongQuanBefore;
    //		public string strTongQuanAfter;
    //
    //		public string strXiaFuBuQianKaiQi;
    //		public string strXiaFuBuQianKaiQiBefore;
    //		public string strXiaFuBuQianKaiQiAfter;
    //
    //		public string strZhiJiaoGou;
    //		public string strZhiJiaoGouBefore;
    //		public string strZhiJiaoGouAfter;
    //
    //		public string strChangYaGou;
    //		public string strChangYaGouBefore;
    //		public string strChangYaGouAfter;
    //
    //		public string strZhongFuGou;
    //		public string strZhongFuGouBefore;
    //		public string strZhongFuGouAfter;
    //
    //		public string strZhiJiaoQian;
    //		public string strZhiJiaoQianBefore;
    //		public string strZhiJiaoQianAfter;
    //
    //		public string strDaZhiQian;
    //		public string strDaZhiQianBefore;
    //		public string strDaZhiQianAfter;
    //	
    //		public string strFengZhen;
    //		public string strFengZhenBefore;
    //		public string strFengZhenAfter;
    //
    //		public string strChangChiZhenQian25;
    //		public string strChangChiZhenQian25Before;
    //		public string strChangChiZhenQian25After;
    //
    //		public string strWanXueGuanQian25;
    //		public string strWanXueGuanQian25Before;
    //		public string strWanXueGuanQian25After;
    //
    //		public string strWanXueGuanQian22;
    //		public string strWanXueGuanQian22Before;
    //		public string strWanXueGuanQian22After;
    //
    //		public string strWanXueGuanQian20;
    //		public string strWanXueGuanQian20Before;
    //		public string strWanXueGuanQian20After;
    //
    //		public string strWanXueGuanQian18;
    //		public string strWanXueGuanQian18Before;
    //		public string strWanXueGuanQian18After;
    //
    //		public string strDaoPian;
    //		public string strDaoPianBefore;
    //		public string strDaoPianAfter;
    //
    //		public string strNianMoQian;
    //		public string strNianMoQianBefore;
    //		public string strNianMoQianAfter;
    //
    //		public string strShaLiQian;
    //		public string strShaLiQianBefore;
    //		public string strShaLiQianAfter;
    //	
    //
    //		public string strDaWanXueGuanQian;
    //		public string strDaWanXueGuanQianBefore;
    //		public string strDaWanXueGuanQianAfter;
    //
    //		public string strShenDiQian;
    //		public string strShenDiQianBefore;
    //		public string strShenDiQianAfter;
    //
    //		public string strChangQianZhi;
    //		public string strChangQianZhiBefore;
    //		public string strChangQianZhiAfter;
    //
    //		public string strChangQianWan;
    //		public string strChangQianWanBefore;
    //		public string strChangQianWanAfter;
    //
    //		public string strShiQian;
    //		public string strShiQianBefore;
    //		public string strShiQianAfter;
    //
    //		public string strWeiQian;
    //		public string strWeiQianBefore;
    //		public string strWeiQianAfter;
    //
    //		public string strXinErQian;
    //		public string strXinErQiaBefore;
    //		public string strXinErQiaAfter;
    //
    //		public string strErYanHouChongXiQi;
    //		public string strErYanHouChongXiQiBefore;
    //		public string strErYanHouChongXiQiAfter;
    //
    //	
    //		public string strTanZhenChu;
    //		public string strTanZhenChuBefore;
    //		public string strTanZhenChuAfter;
    //
    //		public string strTanZhenXi;
    //		public string strTanZhenXiBefore;
    //		public string strTanZhenXiAfter;
    //
    //		public string strDanDaoTanTiao;
    //		public string strDanDaoTanTiaoBefore;
    //		public string strDanDaoTanTiaoAfter;
    //
    //		public string strLeiGuQianKaiQi;
    //		public string strLeiGuQianKaiQiBefore;
    //		public string strLeiGuQianKaiQiAfter;
    //
    //		public string strHeLongQi;
    //		public string strHeLongQiBefore;
    //		public string strHeLongQiAfter;
    //
    //		public string strJianJiaGuLaGou;
    //		public string strJianJiaGuLaGouBefore;
    //		public string strJianJiaGuLaGouAfter;
    //
    //		public string strLeiGuQiZi;
    //		public string strLeiGuQiZiBefore;
    //		public string strLeiGuQiZiAfter;
    //
    //		public string strDaGuJian;
    //		public string strDaGuJianBefore;
    //		public string strDaGuJianAfter;
    //
    //		public string strDiErLeiGuJian;
    //		public string strDiErLeiGuJianBefore;
    //		public string strDiErLeiGuJianAfter;
    //
    //		public string strFangTouYaoGuQian;
    //		public string strFangTouYaoGuQianBefore;
    //		public string strFangTouYaoGuQianAfter;
    //
    //		public string strYaoGuQian;
    //		public string strYaoGuQianBefore;
    //		public string strYaoGuQianAfter;
    //	
    //
    //		public string strGuMoBoLiQi;
    //		public string strGuMoBoLiQiBefore;
    //		public string strGuMoBoLiQiAfter;
    //
    //		public string strGuDao;
    //		public string strGuDaoBefore;
    //		public string strGuDaoAfter;
    //
    //		public string strGuZao;
    //		public string strGuZaoBefore;
    //		public string strGuZaoAfter;
    //
    //		public string strKuoShi;
    //		public string strKuoShiBefore;
    //		public string strKuoShiAfter;
    //
    //		public string strGuChui;
    //		public string strGuChuiBefore;
    //		public string strGuChuiAfter;
    //
    //		public string strChiGuQian;
    //		public string strChiGuQianBefore;
    //		public string strChiGuQianAfter;
    //
    //		
    //		public string strJingGuQiZi;
    //		public string strJingGuQiZiBefore;
    //		public string strJingGuQiZiAfter;
    //
    //		public string strDanChiLaGou;
    //		public string strDanChiLaGouBefore;
    //		public string strDanChiLaGouAfter;
    //
    //		public string strLaoHuQian;
    //		public string strLaoHuQianBefore;
    //		public string strLaoHuQianAfter;
    //
    //		public string strPingHengFuWeiQian;
    //		public string strPingHengFuWeiQianBefore;
    //		public string strPingHengFuWeiQianAfter;
    //
    //		public string strLuoSiQiZi;
    //		public string strLuoSiQiZiBefore;
    //		public string strLuoSiQiZiAfter;
    //
    //		public string strDaoXiangQi;
    //		public string strDaoXiangQiBefore;
    //		public string strDaoXiangQiAfter;
    //	
    //
    //		public string strZhuiBanYaoGuQian;
    //		public string strZhuiBanYaoGuQianBefore;
    //		public string strZhuiBanYaoGuQianAfter;
    //
    //		public string strShuiHeQian;
    //		public string strShuiHeQianBefore;
    //		public string strShuiHeQianAfter;
    //
    //		public string strJingTuJian;
    //		public string strJingTuJianBefore;
    //		public string strJingTuJianAfter;
    //
    //		public string strZhuiBanBoLiQi;
    //		public string strZhuiBanBoLiQiBefore;
    //		public string strZhuiBanBoLiQiAfter;
    //
    //		public string strJianBoLiZi;
    //		public string strJianBoLiZiBefore;
    //		public string strJianBoLiZiAfter;
    //
    //		public string strQiangZhuangNie;
    //		public string strQiangZhuangNieBefore;
    //		public string strQiangZhuangNieAfter;
    //
    //		public string strBaiShiQianKaiQi;
    //		public string strBaiShiQianKaiQiBefore;
    //		public string strBaiShiQianKaiQiAfter;
    //
    //		public string strKaiLuZhuan;
    //		public string strKaiLuZhuanBefore;
    //		public string strKaiLuZhuanAfter;
    //
    //		public string strTouPiJianQian;
    //		public string strTouPiJianQianBefore;
    //		public string strTouPiJianQianAfter;
    //
    //		public string strXianJuDaoYinZi;
    //		public string strXianJuDaoYinZiBefore;
    //		public string strXianJuDaoYinZiAfter;
    //
    //		public string strXinErLaGou;
    //		public string strXinErLaGouBefore;
    //		public string strXinErLaGouAfter;
    //
    //		public string strChiBanQian;
    //		public string strChiBanQianBefore;
    //		public string strChiBanQianAfter;
    //	
    //		public string strXinFangLaGou;
    //		public string strXinFangLaGouBefore;
    //		public string strXinFangLaGouAfter;
    //
    //		public string strNaoMoGou;
    //		public string strNaoMoGouBefore;
    //		public string strNaoMoGouAfter;
    //
    //		public string strChuanCiZhen;
    //		public string strChuanCiZhenBefore;
    //		public string strChuanCiZhenAfter;
    //
    //		public string strYinDingQian;
    //		public string strYinDingQianBefore;
    //		public string strYinDingQianAfter;
    //
    //		public string strFeiYeDangBan;
    //		public string strFeiYeDangBanBefore;
    //		public string strFeiYeDangBanAfter;
    //
    //		public string strZhuAZhuDuanQian;
    //		public string strZhuAZhuDuanQianBefore;
    //		public string strZhuAZhuDuanQianAfter;
    //
    //		public string strZhuAYouLiQian;
    //		public string strZhuAYouLiQianBefore;
    //		public string strZhuAYouLiQianAfter;
    //
    //		public string strZhuACeBiQian;
    //		public string strZhuACeBiQiBefore;
    //		public string strZhuACeBiQiAfter;
    //
    //		public string strErJianBanKuoZhangQi;
    //		public string strErJianBanKuoZhangQiBefore;
    //		public string strErJianBanKuoZhangQiAfter;
    //
    //		public string strCeBanQi;
    //		public string strCeBanQiBefore;
    //		public string strCeBanQiAfter;
    //
    //		public string strXinNeiZhiJiaoLaGou;
    //		public string strXinNeiZhiJiaoLaGouBefore;
    //		public string strXinNeiZhiJiaoLaGouAfter;
    //
    //		public string strDaoXianGou;
    //		public string strDaoXianGouBefore;
    //		public string strDaoXianGouAfter;
    //
    //	
    //
    //		public string strWuChuangNie;
    //		public string strWuChuangNieAfter;
    //		public string strWuChuangNieBefore;
    //
    //		public string strKaChi;
    //		public string strKaChiAfter;
    //		public string strKaChiBefore;
    //
    //		public string strShenJingLaGou;
    //		public string strShenJingLaGouAfter;
    //		public string strShenJingLaGouBefore;
    //
    //		public string strXueGuanJia;
    //		public string strXueGuanJiaAfter;
    //		public string strXueGuanJiaBefore;
    //
    //		public string strFuKui;
    //		public string strFuKuiAfter;
    //		public string strFuKuiBefore;
    //
    //		public string strGongChi;
    //		public string strGongChiAfter;
    //		public string strGongChiBefore;
    //
    //		public string strGongGuaShi;
    //		public string strGongGuaShiAfter;
    //		public string strGongGuaShiBefore;
    //
    //		public string strGongJingQian;
    //		public string strGongJingQianAfter;
    //		public string strGongJingQianBefore;
    //
    //		public string strYinDaoLaGou;
    //		public string strYinDaoLaGouAfter;
    //		public string strYinDaoLaGouBefore;
    //
    //		public string strRenDaiQian;
    //		public string strRenDaiQianAfter;
    //		public string strRenDaiQianBefore;
    //
    //		public string strJiLiuBoLiZi;
    //		public string strJiLiuBoLiZiAfter;
    //		public string strJiLiuBoLiZiBefore;
    //
    //		public string strKuoGongQi;
    //		public string strKuoGongQiAfter;
    //		public string strKuoGongQiBefore;
    //
    //	
    //		public string strFuGuoQian;
    //		public string strFuGuoQianAfter;
    //		public string strFuGuoQianBefore;
    //
    //		public string strJinShuNiaoGou;
    //		public string strJinShuNiaoGouAfter;
    //		public string strJinShuNiaoGouBefore;
    //
    //		public string strYouDaiFangDian;
    //		public string strYouDaiFangDianAfter;
    //		public string strYouDaiFangDianBefore;
    //
    //		public string strWuDaiFangDian;
    //		public string strWuDaiFangDianAfter;
    //		public string strWuDaiFangDianBefore;
    //
    //		public string strYouDaiChangDian;
    //		public string strYouDaiChangDianAfter;
    //		public string strYouDaiChangDianBefore;
    //
    //		public string strWuDaiChangDian;
    //		public string strWuDaiChangDianAfter;
    //		public string strWuDaiChangDianBefore;
    //
    //		public string strFuNieYinLiu;
    //		public string strFuNieYinLiuAfter;
    //		public string strFuNieYinLiuBefore;
    //
    //		public string strKaiLuMian;
    //		public string strKaiLuMianAfter;
    //		public string strKaiLuMianBefore;
    //
    //		public string strQuanGongSha;
    //		public string strQuanGongShaAfter;
    //		public string strQuanGongShaBefore;
    //
    //		public string strWangSha;
    //		public string strWangShaAfter;
    //		public string strWangShaBefore;
    //
    //		public string strShaKuai;
    //		public string strShaKuaiAfter;
    //		public string strShaKuaiBefore;
    //
    //		public string strShaQiu;
    //		public string strShaQiuAfter;
    //		public string strShaQiuBefore;
    //
    //	
    //
    //		public string strBianDai;
    //		public string strBianDaiAfter;
    //		public string strBianDaiBefore;
    //
    //		public string strChangQianTao;
    //		public string strChangQianTaoAfter;
    //		public string strChangQianTaoBefore;
    //
    //		public string strNiaoGuan;
    //		public string strNiaoGuanAfter;
    //		public string strNiaoGuanBefore;
    //
    //	}
    //
    //
    //
    //
    //	#endregion
    //
    //	/// <summary>                                                
    //	/// ��ʿǩ��
    //	/// </summary>
    //	[Serializable]
    //	public class clsOperationNurse
    //	{
    //		public string strInPatientID;
    //		public string strInPatientDate;
    //		public string strOpenDate;
    //		public string strNurseID;
    //		/// <summary>
    //		/// ��ʿ���ƣ����ڶ���ʱ��ֵ
    //		/// </summary>
    //		public string strNurseName;
    //		public string strNurseFlag;
    //		public string strStatus;
    //		public string strDeActivedDate;
    //		public string strDeActivedOperatorID;
    //	}

    public class clsEmployeeIDName
    {
        public string strEmployeeID;
        public string strEmployeeName;
    }


    //	[Serializable]
    //	public class clsOperationEquipmentPackage
    //	{
    //		//����
    //		public clsOperationEqipmentQtyXML m_objOperationEqipmentQtyXML;
    //		
    //		//�ӱ�
    //		public clsOperationEqipmentQtyContent m_objOperationEqipmentQtyContent;
    //		
    //		//��ʿ
    //		public clsOperationNurse [] m_objOperationNurse;
    //	}
}
