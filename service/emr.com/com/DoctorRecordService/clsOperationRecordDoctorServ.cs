using System;
using System.Text ;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.PublicMiddleTier;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.OperationRecordDoctorServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsOperationRecordDoctorServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		#region Sql
		/// <summary>
		/// 获取画图信息
		/// </summary>
        private const string c_strGetRecordContent_PictureSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       picid,
       backimage,
       frontimage,
       backcolor,
       width,
       height
  from operationrecorddoctor_picture
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?";
		#endregion


		

		#region 保存单据数据
		/// <summary>
		/// 添加信息
		/// </summary>		
		[AutoComplete]		
		public long m_lngAddNew(
			string p_strMainXml,string p_strSubXml,
			string[] p_strOperationXmlArr,string[] p_strNurseXMLArr,
			clsOperationRecordDoctorSign objDoctorSign,
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,clsPictureBoxValue[] p_objPics)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                //添加记录时，主从表同时添加一条记录			
                if (p_strMainXml == "" || p_strSubXml == "")
                    return -1;

                lngRes = objHRPServ.add_new_record("OperationRecordDoctor", p_strMainXml);

                if (lngRes == 1)
                    lngRes = objHRPServ.add_new_record("OperationRecordContenDoctor", p_strSubXml);
                //不需要保存id，直接保存名字 tfzhang 2005-7-9 16:08:56

                //				if( p_strOperationXmlArr !=null)
                //					for(int i=0;i<p_strOperationXmlArr.Length && lngRes==1;i++)
                //					{
                //						lngRes = objHRPServ.add_new_record("OprRecDoctor_OperationID",p_strOperationXmlArr[i]);
                //					}
                //
                //				if(lngRes == 1)
                //					lngRes = m_lngAddNewSign2DB(objDoctorSign,objHRPServ);
                //
                //				if(lngRes == 1)
                //					lngRes = m_lngAddNurse(p_strNurseXMLArr);

                if (lngRes == 1)
                    lngRes = m_lngAddPics(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_objPics);


            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objOperationRecord">主表内容</param>
        /// <param name="p_objOperationRecordContent">子表内容</param>
        /// <param name="p_objPics">图片信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNew( clsOperationRecordDoctor p_objOperationRecord,clsOperationRecordContentDoctor p_objOperationRecordContent, clsPictureBoxValue[] p_objPics)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngAddNewRecordDoctor(p_objOperationRecord);

                if (lngRes == 1)
                    lngRes = m_lngAddNewRecordContentDoctor(p_objOperationRecordContent);

                if (lngRes == 1)
                    lngRes = m_lngAddPics(p_objOperationRecord.m_strInPatientID, p_objOperationRecord.m_strInPatientDate, p_objOperationRecord.m_strOpenDate, p_objPics);
                
                //保存签名
                if (lngRes == 1)
                {
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    lngRes = objSign.m_lngAddSign(p_objOperationRecord.objSignerArr, p_objOperationRecord.int_Sequence);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 修改信息
        /// 适用多签名方式
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_objOperationRecord">主表内容</param>
        /// <param name="p_objOperationRecordContent">子表内容</param>
        /// <param name="p_objPics">图片</param>
        /// <returns>返回值</returns>
        [AutoComplete]
        public long m_lngModify(
           clsOperationRecordDoctor p_objOperationRecord, clsOperationRecordContentDoctor p_objOperationRecordContent, clsPictureBoxValue[] p_objPics)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = m_lngModifyRecordDoctor(p_objOperationRecord);
                if (lngRes == 1)
                    lngRes = m_lngAddNewRecordContentDoctor(p_objOperationRecordContent);

                if (lngRes == 1)
                    lngRes = m_lngModifyPics(p_objOperationRecord.m_strInPatientID, p_objOperationRecord.m_strInPatientDate, p_objOperationRecord.m_strOpenDate, p_objPics);

                //保存签名
                if (lngRes == 1)
                {
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    lngRes = objSign.m_lngAddSign(p_objOperationRecord.objSignerArr, p_objOperationRecord.int_Sequence);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 添加信息
        /// 适应多签名方式
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_strMainXml">主记录xml</param>
        /// <param name="p_strSubXml">子记录xml</param>
        /// <param name="p_strOperationXmlArr">手术记录xml</param>
        /// <param name="p_objOperationRecord">主记录</param>
        /// <param name="p_objPics">图片</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNew(
            string p_strMainXml, string p_strSubXml,
            string[] p_strOperationXmlArr, clsOperationRecordDoctor p_objOperationRecord, clsPictureBoxValue[] p_objPics)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;
                //添加记录时，主从表同时添加一条记录			
                if (p_strMainXml == "" || p_strSubXml == "")
                    return -1;

                lngRes = objHRPServ.add_new_record("OperationRecordDoctor", p_strMainXml);

                if (lngRes == 1)
                    lngRes = objHRPServ.add_new_record("OperationRecordContenDoctor", p_strSubXml);
 
                if (lngRes == 1)
                    lngRes = m_lngAddPics(p_objOperationRecord.m_strInPatientID, p_objOperationRecord.m_strInPatientDate, p_objOperationRecord.m_strOpenDate, p_objPics);
                //保存签名
                if (lngRes == 1)
                {
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    lngRes = objSign.m_lngAddSign(p_objOperationRecord.objSignerArr, p_objOperationRecord.int_Sequence);
                 }



            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }
		[AutoComplete]
		private long m_lngAddNurse(string[] p_strNurseXMLArr)
		{
			if(p_strNurseXMLArr == null || p_strNurseXMLArr.Length <=0)
				return 1;
			long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                for (int i1 = 0; i1 < p_strNurseXMLArr.Length; i1++)
                {
                    lngRes = objHRPServ.add_new_record("OperationRecordDoctor_Operator", p_strNurseXMLArr[i1]);
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
			return lngRes;

			

		}
		#endregion

		#region 修改单据数据
		/// <summary>
		/// 修改信息
		/// </summary>		
		[AutoComplete]		
		public long m_lngModify(
			string p_strMainXml,string p_strSubXml,string[] p_strOperationXmlArr,string[] p_strNurseXMLArr,clsOperationRecordDoctorSign objDoctorSign,
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,clsPictureBoxValue[] p_objPics)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngModify");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                //修改记录时，主表更新原来记录中的相关XML记录，同时从表添加一条记录	
                if (p_strMainXml == "" || p_strSubXml == "")
                    return -1;

                lngRes = objHRPServ.modify_record("OperationRecordDoctor", p_strMainXml, "INPATIENTID", "INPATIENTDATE", "OPENDATE");
                if (lngRes == 1)
                    lngRes = objHRPServ.add_new_record("OperationRecordContenDoctor", p_strSubXml);

                if (p_strOperationXmlArr != null)
                    for (int i = 0; i < p_strOperationXmlArr.Length && lngRes == 1; i++)
                    {
                        lngRes = objHRPServ.add_new_record("OprRecDoctor_OperationID", p_strOperationXmlArr[i]);
                    }

                if (lngRes == 1)
                    lngRes = m_lngAddNewSign2DB(objDoctorSign, objHRPServ);

                if (lngRes == 1)
                    lngRes = m_lngAddNurse(p_strNurseXMLArr);

                if (lngRes == 1)
                    lngRes = m_lngModifyPics(p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_objPics);


            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
        /// <summary>
        /// 修改信息
        /// 适用多签名方式
        /// </summary>
        /// <param name="p_objPrincipal">权限</param>
        /// <param name="p_strMainXml">主记录xml</param>
        /// <param name="p_strSubXml">子记录xml</param>
        /// <param name="p_strOperationXmlArr">手术记录xml</param>
        /// <param name="p_objOperationRecord">主记录</param>
        /// <param name="p_objPics">图片</param>
        /// <returns>返回值</returns>
	
        [AutoComplete]
        public long m_lngModify(
            string p_strMainXml, string p_strSubXml, string[] p_strOperationXmlArr, clsOperationRecordDoctor p_objOperationRecord, clsPictureBoxValue[] p_objPics)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngModify");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;
                //修改记录时，主表更新原来记录中的相关XML记录，同时从表添加一条记录	
                if (p_strMainXml == "" || p_strSubXml == "")
                    return -1;

                lngRes = objHRPServ.modify_record("OperationRecordDoctor", p_strMainXml, "INPATIENTID", "INPATIENTDATE", "OPENDATE");
                if (lngRes == 1)
                    lngRes = objHRPServ.add_new_record("OperationRecordContenDoctor", p_strSubXml);

                if (p_strOperationXmlArr != null)
                    for (int i = 0; i < p_strOperationXmlArr.Length && lngRes == 1; i++)
                    {
                        lngRes = objHRPServ.add_new_record("OprRecDoctor_OperationID", p_strOperationXmlArr[i]);
                    }
                if (lngRes == 1)
                    lngRes = m_lngModifyPics(p_objOperationRecord.m_strInPatientID, p_objOperationRecord.m_strInPatientDate,p_objOperationRecord.m_strOpenDate, p_objPics);

                //保存签名
                if (lngRes == 1)
                {
                    com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                    lngRes = objSign.m_lngAddSign(p_objOperationRecord.objSignerArr, p_objOperationRecord.int_Sequence);
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
            return lngRes;

        }

		#endregion

		#region 读取初始化信息
		/// <summary>
		/// 获得所有Create Date
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]		
		public long m_lngGetTimeInfoOfAPatient(
			string p_strInPatientID,string p_strInPatientDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetTimeInfoOfAPatient");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = @"select createdate,opendate from operationrecorddoctor where inpatientid=? and status =0 and inpatientdate=? order by createdate desc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;

            }			//返回
			return lngRes;


		}

		/// <summary>
		/// 获得手术名称和ID
		/// </summary>
		/// <param name="p_strXML"></param>
		/// <param name="p_intRows"></param>
		/// <returns></returns>
		[AutoComplete]		
		public long m_lngGetOperationIDName(
			out string p_strXML,out int p_intRows)
		{
			p_strXML = null;
			p_intRows = 0;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetOperationIDName");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                string m_strCommand = @"select operationid,
       operationname,
       status,
       deactiveddate,
       deactivedoperatorid
  from operation
 where status = 0";
                lngRes = objHRPServ.lngGetXMLTable(m_strCommand, ref p_strXML, ref p_intRows);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion

		#region 读取显示信息
		/// <summary>
		/// 获得主表的记录
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngGetOperationRecord(
			string p_strPatientID,string p_strInPatientDate,
			string p_strOpenDate,ref string  p_strXml,ref int  intRows)
		{

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetOperationRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.operationnamexml,
       a.anaesthesiabeforeoperationxml,
       a.anaesthesiainoperationxml,
       a.anaesthesiacategorydosagexml,
       a.diagnosebeforeoperationxml,
       a.diagnoseafteroperationxml,
       a.operationprocessxml,
       a.pathologyxml,
       a.sampleorextrarecordxml,
       a.summaryafteroperationxml,
       a.inliquidxml,
       a.outflowxml,
       a.xraynumberxml,
       a.firstprintdate,
       a.sequence_int
  from operationrecorddoctor a
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate );
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}

		/// <summary>
		/// 获得从表的记录
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngGetOperationRecordContent(
			string p_strPatientID,string p_strInPatientDate,
			string p_strOpenDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strCommand = "";
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetOperationRecordContent");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strCommand = @"select top 1 a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.lastmodifydate,
       a.lastmodifyuserid,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.operationname,
       a.diagnosebeforeoperation,
       a.diagnoseafteroperation,
       a.operationbegindate,
       a.operationenddate,
       a.anaesthesiabeforeoperation,
       a.anaesthesiainoperation,
       a.anaesthesiacategorydosage,
       a.anaesthesiabegindate,
       a.anaesthesiaenddate,
       a.operationprocess,
       a.pathology,
       a.inliquid,
       a.outflow,
       a.sampleorextrarecord,
       a.summaryafteroperation,
       a.xraynumber,
       a.anaesther,
       a.doctor1,
       a.doctor2,
       a.operationdoctorid,
       a.operationdoctorname,
       a.firstassistantid,
       a.firstassistantname,
       a.secondassistantid,
       a.secondassistantname,
       a.nurse,
       a.assistant,
       a.operationdoctor
  from operationrecordcontendoctor a
 where a.inpatientid = ?
   and a.status = 0
   and a.opendate = ?
   and a.inpatientdate = ?
 order by a.lastmodifydate desc";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       operationname,
       diagnosebeforeoperation,
       diagnoseafteroperation,
       operationbegindate,
       operationenddate,
       anaesthesiabeforeoperation,
       anaesthesiainoperation,
       anaesthesiacategorydosage,
       anaesthesiabegindate,
       anaesthesiaenddate,
       operationprocess,
       pathology,
       inliquid,
       outflow,
       sampleorextrarecord,
       summaryafteroperation,
       xraynumber,
       anaesther,
       doctor1,
       doctor2,
       operationdoctorid,
       operationdoctorname,
       firstassistantid,
       firstassistantname,
       secondassistantid,
       secondassistantname,
       nurse,
       assistant,
       operationdoctor
  from (select a.inpatientid,
               a.inpatientdate,
               a.opendate,
               a.lastmodifydate,
               a.lastmodifyuserid,
               a.deactiveddate,
               a.deactivedoperatorid,
               a.status,
               a.operationname,
               a.diagnosebeforeoperation,
               a.diagnoseafteroperation,
               a.operationbegindate,
               a.operationenddate,
               a.anaesthesiabeforeoperation,
               a.anaesthesiainoperation,
               a.anaesthesiacategorydosage,
               a.anaesthesiabegindate,
               a.anaesthesiaenddate,
               a.operationprocess,
               a.pathology,
               a.inliquid,
               a.outflow,
               a.sampleorextrarecord,
               a.summaryafteroperation,
               a.xraynumber,
               a.anaesther,
               a.doctor1,
               a.doctor2,
               a.operationdoctorid,
               a.operationdoctorname,
               a.firstassistantid,
               a.firstassistantname,
               a.secondassistantid,
               a.secondassistantname,
               a.nurse,
               a.assistant,
               a.operationdoctor
          from operationrecordcontendoctor a
         where a.inpatientid = ?
           and a.status = 0
           and a.opendate = ?
           and a.inpatientdate = ?
         order by a.lastmodifydate desc)
 where rownum = 1";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strCommand = @" select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.lastmodifydate,
       a.lastmodifyuserid,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.operationname,
       a.diagnosebeforeoperation,
       a.diagnoseafteroperation,
       a.operationbegindate,
       a.operationenddate,
       a.anaesthesiabeforeoperation,
       a.anaesthesiainoperation,
       a.anaesthesiacategorydosage,
       a.anaesthesiabegindate,
       a.anaesthesiaenddate,
       a.operationprocess,
       a.pathology,
       a.inliquid,
       a.outflow,
       a.sampleorextrarecord,
       a.summaryafteroperation,
       a.xraynumber,
       a.anaesther,
       a.doctor1,
       a.doctor2,
       a.operationdoctorid,
       a.operationdoctorname,
       a.firstassistantid,
       a.firstassistantname,
       a.secondassistantid,
       a.secondassistantname,
       a.nurse,
       a.assistant,
       a.operationdoctor
  from operationrecordcontendoctor a
 where a.inpatientid = ?
   and a.status = 0
   and a.opendate = ?
   and a.inpatientdate = ?
 order by a.lastmodifydate desc fetch first 1 row only";

                }

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
			return lngRes;


		}

		/// <summary>
		/// 获得最后的手术名称ID
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strXML"></param>
		/// <param name="p_intRows"></param>
		/// <returns></returns>
		[AutoComplete]		
		public long m_lngGetLastestOperationIDArr(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
		{
			p_strXML = null;
			p_intRows = 0;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetLastestOperationIDArr");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string m_strCommand = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.operationid,
       a.modifydate,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid
  from oprrecdoctor_operationid a
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 0
   and modifydate = (select max(modifydate)
                       from oprrecdoctor_operationid
                      where inpatientid = a.inpatientid
                        and inpatientdate = a.inpatientdate
                        and opendate = ?)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(m_strCommand, ref p_strXML, ref p_intRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
			return lngRes;

		}

		/// <summary>
		/// 刘荣国增加 获取手术医生，护士的信息
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetOperation_Nurse(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,ref string  strXml,ref int  intRows)
		{
			string strCommand="";
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetOperation_Nurse");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                strCommand = @"select ordo.inpatientid,
       ordo.inpatientdate,
       ordo.opendate,
       ordo.lastmodifydate,
       ordo.opertorid,
       ordo.operatorflag,
       ordo.status,
       ordo.deactiveddate,
       ordo.deactivedoperatorid,
       ebi.lastname_vchr as firstname
  from operationrecorddoctor_operator ordo, t_bse_employee ebi
 where ordo.inpatientid = ?
   and ordo.inpatientdate = ?
   and ordo.opendate = ?
   and ordo.lastmodifydate = (select max(lastmodifydate)
                                from operationrecorddoctor_operator
                               where inpatientid = ?
                                 and inpatientdate = ?
                                 and opendate = ?)
   and ordo.opertorid = ebi.empno_chr
 order by ordo.operatorflag asc";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref strXml, ref intRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;


		}
		#endregion

		#region 读取已经被删除的信息
		/// <summary>
		/// 获得主表的记录
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngGetDeletedOperationRecord(
			string p_strPatientID,string p_strInPatientDate,
			string p_strOpenDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetDeletedOperationRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.operationnamexml,
       a.anaesthesiabeforeoperationxml,
       a.anaesthesiainoperationxml,
       a.anaesthesiacategorydosagexml,
       a.diagnosebeforeoperationxml,
       a.diagnoseafteroperationxml,
       a.operationprocessxml,
       a.pathologyxml,
       a.sampleorextrarecordxml,
       a.summaryafteroperationxml,
       a.inliquidxml,
       a.outflowxml,
       a.xraynumberxml,
       a.firstprintdate,
       a.sequence_int
  from operationrecorddoctor a
 where a.inpatientid = ?
   and a.status = 1
   and a.opendate = ?
   and a.inpatientdate = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);


            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			
            //返回
			return lngRes;

		}

		/// <summary>
		/// 获得从表的记录
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngGetDeletedOperationRecordContent(
			string p_strPatientID,string p_strInPatientDate,
			string p_strOpenDate,ref string  p_strXml,ref int  intRows)
		{

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strCommand = "";
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetDeletedOperationRecordContent");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strCommand = @"select top 1 b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.operationname,
       b.diagnosebeforeoperation,
       b.diagnoseafteroperation,
       b.operationbegindate,
       b.operationenddate,
       b.anaesthesiabeforeoperation,
       b.anaesthesiainoperation,
       b.anaesthesiacategorydosage,
       b.anaesthesiabegindate,
       b.anaesthesiaenddate,
       b.operationprocess,
       b.pathology,
       b.inliquid,
       b.outflow,
       b.sampleorextrarecord,
       b.summaryafteroperation,
       b.xraynumber,
       b.anaesther,
       b.doctor1,
       b.doctor2,
       b.operationdoctorid,
       b.operationdoctorname,
       b.firstassistantid,
       b.firstassistantname,
       b.secondassistantid,
       b.secondassistantname,
       b.nurse,
       b.assistant,
       b.operationdoctor
  from operationrecordcontendoctor b, operationrecorddoctor a
 where a.inpatientid = ?
   and a.status = 1
   and a.opendate = ?
   and a.inpatientdate = ?
   and a.inpatientid = b.inpatientid
   and a.opendate = b.opendate
   and a.inpatientdate = b.inpatientdate
 order by b.lastmodifydate desc";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       lastmodifyuserid,
       deactiveddate,
       deactivedoperatorid,
       status,
       operationname,
       diagnosebeforeoperation,
       diagnoseafteroperation,
       operationbegindate,
       operationenddate,
       anaesthesiabeforeoperation,
       anaesthesiainoperation,
       anaesthesiacategorydosage,
       anaesthesiabegindate,
       anaesthesiaenddate,
       operationprocess,
       pathology,
       inliquid,
       outflow,
       sampleorextrarecord,
       summaryafteroperation,
       xraynumber,
       anaesther,
       doctor1,
       doctor2,
       operationdoctorid,
       operationdoctorname,
       firstassistantid,
       firstassistantname,
       secondassistantid,
       secondassistantname,
       nurse,
       assistant,
       operationdoctor
  from (select b.inpatientid,
               b.inpatientdate,
               b.opendate,
               b.lastmodifydate,
               b.lastmodifyuserid,
               b.deactiveddate,
               b.deactivedoperatorid,
               b.status,
               b.operationname,
               b.diagnosebeforeoperation,
               b.diagnoseafteroperation,
               b.operationbegindate,
               b.operationenddate,
               b.anaesthesiabeforeoperation,
               b.anaesthesiainoperation,
               b.anaesthesiacategorydosage,
               b.anaesthesiabegindate,
               b.anaesthesiaenddate,
               b.operationprocess,
               b.pathology,
               b.inliquid,
               b.outflow,
               b.sampleorextrarecord,
               b.summaryafteroperation,
               b.xraynumber,
               b.anaesther,
               b.doctor1,
               b.doctor2,
               b.operationdoctorid,
               b.operationdoctorname,
               b.firstassistantid,
               b.firstassistantname,
               b.secondassistantid,
               b.secondassistantname,
               b.nurse,
               b.assistant,
               b.operationdoctor
          from operationrecordcontendoctor b, operationrecorddoctor a
         where a.inpatientid = ?
           and a.status = 1
           and a.opendate = ?
           and a.inpatientdate = ?
           and a.inpatientid = b.inpatientid
           and a.opendate = b.opendate
           and a.inpatientdate = b.inpatientdate
         order by b.lastmodifydate desc)
 where rownum = 1";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strCommand = @"select b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.operationname,
       b.diagnosebeforeoperation,
       b.diagnoseafteroperation,
       b.operationbegindate,
       b.operationenddate,
       b.anaesthesiabeforeoperation,
       b.anaesthesiainoperation,
       b.anaesthesiacategorydosage,
       b.anaesthesiabegindate,
       b.anaesthesiaenddate,
       b.operationprocess,
       b.pathology,
       b.inliquid,
       b.outflow,
       b.sampleorextrarecord,
       b.summaryafteroperation,
       b.xraynumber,
       b.anaesther,
       b.doctor1,
       b.doctor2,
       b.operationdoctorid,
       b.operationdoctorname,
       b.firstassistantid,
       b.firstassistantname,
       b.secondassistantid,
       b.secondassistantname,
       b.nurse,
       b.assistant,
       b.operationdoctor
  from operationrecordcontendoctor b, operationrecorddoctor a
 where a.inpatientid = ?
   and a.status = 1
   and a.opendate = ?
   and a.inpatientdate = ?
   and a.inpatientid = b.inpatientid
   and a.opendate = b.opendate
   and a.inpatientdate = b.inpatientdate
 order by b.lastmodifydate desc fetch first 1 row only";

                }

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}

		/// <summary>
		/// 获得最后的手术名称ID
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strXML"></param>
		/// <param name="p_intRows"></param>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngGetDeletedLastestOperationIDArr(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
		{
			p_strXML = null;
			p_intRows = 0;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetDeletedLastestOperationIDArr");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return -1;
                string m_strCommand = @"select b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.operationid,
       b.modifydate,
       b.status,
       b.deactiveddate,
       b.deactivedoperatorid
  from oprrecdoctor_operationid b, operationrecorddoctor a
 where a.inpatientid = ?
   and a.status = 1
   and a.opendate = ?
   and a.inpatientdate = ?
   and a.inpatientid = b.inpatientid
   and a.opendate = b.opendate
   and a.inpatientdate = b.inpatientdate
   and b.modifydate = (select max(modifydate)
                         from oprrecdoctor_operationid
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(m_strCommand, ref p_strXML, ref p_intRows, objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;

		}		
		#endregion

		#region Delete
		[AutoComplete]	
		public long m_lngDelete(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate ,string p_strDeActiveID)
		{
			long lngRes = 0;
            clsPublicMiddleTier objHRPServ = new clsPublicMiddleTier();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngDelete");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                lngRes = objHRPServ.m_lngDeleteRecord(  "OperationRecordDoctor", p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_strDeActiveID);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion

		/// <summary>
		/// 添加记录时查询用户输入的时间有无重复
		/// </summary>		
		[AutoComplete]	
		public long m_lngRecordExist(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out bool p_blnExist)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出CreateDate为指定时间的记录
			p_blnExist=false;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngRecordExist");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       status,
       deactiveddate,
       deactivedoperatorid,
       operationnamexml,
       anaesthesiabeforeoperationxml,
       anaesthesiainoperationxml,
       anaesthesiacategorydosagexml,
       diagnosebeforeoperationxml,
       diagnoseafteroperationxml,
       operationprocessxml,
       pathologyxml,
       sampleorextrarecordxml,
       summaryafteroperationxml,
       inliquidxml,
       outflowxml,
       xraynumberxml,
       firstprintdate,
       sequence_int
  from operationrecorddoctor
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and status = 0";
                string strXML = "";
                int intRows = 0;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref strXML, ref intRows, objDPArr);
                if (lngRes > 0 && intRows > 0) p_blnExist = true;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;


		}

		#region FirstPrint
		[AutoComplete]	
		public long m_lngUpdateFirstPrintDate(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,string p_strFirstPrintDate)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //更新第一次打印时间	
                string strCommand = "update operationrecorddoctor set firstprintdate=? where firstprintdate is null and status=0 and inpatientid=? and inpatientdate=? and opendate=?";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Parse(p_strFirstPrintDate);
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strCommand,ref lngEff,objDPArr);

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}		

		/// <summary>
		/// 查找该条记录的第一次打印时间
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strFirstPrintDate"></param>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngGetFirstPrintDate(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strFirstPrintDate)
		{
			p_strFirstPrintDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;
                if (p_strInPatientDate == null || p_strInPatientDate == "")
                    return -1;
                if (p_strOpenDate == null || p_strOpenDate == "")
                    return -1;

                DataTable m_dtbResult = new DataTable();
                string m_strCommand = @"select firstprintdate from operationrecorddoctor  where 
                    inpatientid = ? and inpatientdate = ? and opendate=?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strCommand, ref m_dtbResult, objDPArr);
                if (lngRes > 0)
                {
                    if (m_dtbResult.Rows[0][0] == null || m_dtbResult.Rows[0][0].ToString() == "")
                        return lngRes;
                    p_strFirstPrintDate = m_dtbResult.Rows[0][0].ToString();
                }
                //				else 
                //					return m_lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }		
            //返回
			return lngRes;
			
		}
		#endregion 

		/// <summary>
		/// 获得最后修改时间
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_strLastModifyDate"></param>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngGetOperationRecordLastModifyDate(
			string p_strPatientID,string p_strInPatientDate,
			string p_strOpenDate,out string  p_strLastModifyDate)
		{
			p_strLastModifyDate = "";
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationRecordDoctorServ", "m_lngGetOperationRecordLastModifyDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strCommand = @"select top 1 lastmodifydate from operationrecordcontendoctor a 
                          where a.inpatientid=?  and a.status =0 and a.opendate= ? and a.inpatientdate=? order by a.lastmodifydate desc ";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strCommand = @"select lastmodifydate
  from (select lastmodifydate
          from operationrecordcontendoctor a
         where a.inpatientid = ?
           and a.status = 0
           and a.opendate = ?
           and a.inpatientdate = ?
         order by a.lastmodifydate desc)
 where rownum = 1";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strCommand = @"select lastmodifydate from operationrecordcontendoctor a 
                          where a.inpatientid=? and a.status =0 and a.opendate=? and a.inpatientdate=? order by a.lastmodifydate desc fetch first 1 row only ";

                }

                DataTable m_dtbResult = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strOpenDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref m_dtbResult, objDPArr);
                if (lngRes > 0 && m_dtbResult.Rows.Count > 0)
                {
                    p_strLastModifyDate = m_dtbResult.Rows[0][0].ToString();
                }


            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;

		}

		// 添加记录到DeadCaseDiscussRecordNoter
		private const string c_strAddNewOperationDoctorSQL =@"insert into  oprrecdoctor_operationdoctor(inpatientid,inpatientdate,opendate,modifydate,operationdoctorid) 
								values(?,?,?,?,?)";
		// 添加记录到DeadCaseDiscussRecordNoter
		private const string c_strAddNewAssistantSQL =@"insert into  oprrecdoctor_assistant(inpatientid,inpatientdate,opendate,modifydate,assistantid) 
								values(?,?,?,?,?)";
		// 添加记录到DeadCaseDiscussRecordNoter
		private const string c_strAddNewNurseSQL =@"insert into  operationrecorddoctor_nurse(inpatientid,inpatientdate,opendate,modifydate,nurseid) 
								values(?,?,?,?,?)";
		/// <summary>
		/// 保存签名到数据库。
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngAddNewSign2DB(clsOperationRecordDoctorSign p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			long lngRes =1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                long lngEff = 0;

                if (p_objRecordContent.m_strOperationDoctorIDArr != null)
                {
 
                    for (
                        int j = 0; j < p_objRecordContent.m_strOperationDoctorIDArr.Length; j++)
                    {
 
                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objRecordContent.m_strInPatientDate);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objRecordContent.m_strOpenDate);
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_objRecordContent.m_strModifyDate);

                        objDPArr[4].Value = p_objRecordContent.m_strOperationDoctorIDArr[j];

                        //执行SQL			
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewOperationDoctorSQL, ref lngEff, objDPArr);
                        if (lngRes <= 0) return lngRes;
                    }
                }

                if (p_objRecordContent.m_strAssistantIDArr != null)
                {



                    for (int j = 0; j < p_objRecordContent.m_strAssistantIDArr.Length; j++)
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objRecordContent.m_strInPatientDate);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objRecordContent.m_strOpenDate);
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_objRecordContent.m_strModifyDate);

                        objDPArr[4].Value = p_objRecordContent.m_strAssistantIDArr[j];

                        //执行SQL			
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewAssistantSQL, ref lngEff, objDPArr);
                        if (lngRes <= 0) return lngRes;
                    }
                }

                if (p_objRecordContent.m_strNurseIDArr != null)
                {



                    for (int j = 0; j < p_objRecordContent.m_strNurseIDArr.Length; j++)
                    {
                        IDataParameter[] objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(p_objRecordContent.m_strInPatientDate);
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(p_objRecordContent.m_strOpenDate);
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(p_objRecordContent.m_strModifyDate);

                        objDPArr[4].Value = p_objRecordContent.m_strNurseIDArr[j];

                        //执行SQL			
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewNurseSQL, ref lngEff, objDPArr);
                        if (lngRes <= 0) return lngRes;
                    }
                }

 
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;

		}

		// 在OprRecDoctor_OperationDoctor中获取指定表单和ModifyDate的手术医师。
		private const string c_strGetOperationDoctorSQL= @"select od.operationdoctorid, ebi.lastname_vchr as operationdoctorname
															from operationrecorddoctor        ord,
																oprrecdoctor_operationdoctor od,
																t_bse_employee             ebi
															where ord.inpatientid = ?
															and ord.inpatientdate = ?
															and ord.opendate = ?
															and ord.status = 0
															and od.operationdoctorid = ebi.empno_chr
															and od.inpatientid = ord.inpatientid
															and od.inpatientdate = ord.inpatientdate
															and od.opendate = ord.opendate
															and od.modifydate = (select max(modifydate)
																					from oprrecdoctor_operationdoctor
																					where inpatientid = ord.inpatientid
																					and inpatientdate = ord.inpatientdate
																					and opendate = ord.opendate) ";
		// 在OprRecDoctor_Assistant中获取指定表单和ModifyDate的助手。
		private const string c_strGetAssistantSQL= @"select a.assistantid, ebi.lastname_vchr as assistantname
														from operationrecorddoctor  ord,
															oprrecdoctor_assistant a,
															t_bse_employee       ebi
														where ord.inpatientid = ?
														and ord.inpatientdate = ?
														and ord.opendate = ?
														and ord.status = 0
														and a.assistantid = ebi.empno_chr
														and a.inpatientid = ord.inpatientid
														and a.inpatientdate = ord.inpatientdate
														and a.opendate = ord.opendate
														and a.modifydate = (select max(modifydate)
																				from oprrecdoctor_assistant
																				where inpatientid = ord.inpatientid
																				and inpatientdate = ord.inpatientdate
																				and opendate = ord.opendate) ";
		// 在OperationRecordDoctor_Nurse中获取指定表单和ModifyDate的护士。
		private const string c_strGetNurseSQL= @"select n.nurseid, ebi.lastname_vchr as nursename
												from operationrecorddoctor       ord,
													operationrecorddoctor_nurse n,
													t_bse_employee            ebi
												where ord.inpatientid = ?
												and ord.inpatientdate = ?
												and ord.opendate = ?
												and ord.status = 0
												and n.nurseid = ebi.empno_chr
												and n.inpatientid = ord.inpatientid
												and n.inpatientdate = ord.inpatientdate
												and n.opendate = ord.opendate
												and n.modifydate = (select max(modifydate)
																		from operationrecorddoctor_nurse
																		where inpatientid = ord.inpatientid
																		and inpatientdate = ord.inpatientdate
																		and opendate = ord.opendate) ";
		[AutoComplete]	
		public long m_lngGetDoctorSign(string p_strInPatientID,string p_strInPatientDate,
			string p_strOpenDate,out clsOperationRecordDoctorSign p_objRecordContent)
		{
			p_objRecordContent=null;

			clsOperationRecordDoctorSign objRecordConent = new clsOperationRecordDoctorSign();
            	
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                //				//获取IDataParameter数组
                //				IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[3];
                //				//按顺序给IDataParameter赋值
                //				for(int i=0;i<objDPArr.Length;i++)
                //					objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetOperationDoctorSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    objRecordConent.m_strOperationDoctorIDArr = new string[dtbValue.Rows.Count];
                    objRecordConent.m_strOperationDoctorNameArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordConent.m_strOperationDoctorIDArr[i] = dtbValue.Rows[i]["OPERATIONDOCTORID"].ToString();
                        objRecordConent.m_strOperationDoctorNameArr[i] = dtbValue.Rows[i]["OPERATIONDOCTORNAME"].ToString().Trim();
                    }
                }

                dtbValue = new DataTable();
                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                objDPArr2[0].Value = p_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = DateTime.Parse(p_strOpenDate);
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetAssistantSQL, ref dtbValue, objDPArr2);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    objRecordConent.m_strAssistantIDArr = new string[dtbValue.Rows.Count];
                    objRecordConent.m_strAssistantNameArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordConent.m_strAssistantIDArr[i] = dtbValue.Rows[i]["ASSISTANTID"].ToString();
                        objRecordConent.m_strAssistantNameArr[i] = dtbValue.Rows[i]["ASSISTANTNAME"].ToString().Trim();
                    }
                }

                dtbValue = new DataTable();
                IDataParameter[] objDPArr3 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr3);
                objDPArr3[0].Value = p_strInPatientID;
                objDPArr3[1].DbType = DbType.DateTime;
                objDPArr3[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr3[2].DbType = DbType.DateTime;
                objDPArr3[2].Value = DateTime.Parse(p_strOpenDate);
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetNurseSQL, ref dtbValue, objDPArr3);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    objRecordConent.m_strNurseIDArr = new string[dtbValue.Rows.Count];
                    objRecordConent.m_strNurseNameArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        objRecordConent.m_strNurseIDArr[i] = dtbValue.Rows[i]["NURSEID"].ToString();
                        objRecordConent.m_strNurseNameArr[i] = dtbValue.Rows[i]["NURSENAME"].ToString().Trim();
                    }
                }

                p_objRecordContent = objRecordConent;
                //返回

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
		
		}

		#region 图片信息
		/// <summary>
		/// 添加图片信息
		/// </summary>
		/// <returns></returns>
		[AutoComplete]	
		private long m_lngAddPics(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,clsPictureBoxValue[] p_objPics)
		{
			if(p_objPics == null || p_objPics.Length == 0)
				return 1;
			long lngRes = 0;
			try
			{
				clsHRPTableService objHRPServ =new clsHRPTableService();
				
				
				for(int j=0;j<p_objPics.Length;j++)
				{
					//按顺序给IDataParameter赋值
					//				for(int i=0;i<objDPArr0.Length;i++)
					//					objDPArr0[i]=new Oracle.DataAccess.Client.OracleParameter();
                    IDataParameter[] objDPArr0 = null;
                    objHRPServ.CreateDatabaseParameter(8, out objDPArr0);
                    objDPArr0[0].Value = p_strInPatientID;
                    objDPArr0[1].DbType = DbType.DateTime;
                    objDPArr0[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr0[2].DbType = DbType.DateTime;
					objDPArr0[2].Value=DateTime.Parse(p_strOpenDate);

					objDPArr0[3].Value=j+1;
                    objDPArr0[4].DbType = DbType.Binary;		
					if(p_objPics[j].m_bytImage!=null)
						objDPArr0[4].Value= p_objPics[j].m_bytImage;
					else
						objDPArr0[4].Value= System.DBNull.Value;
					objDPArr0[5].Value=p_objPics[j].clrBack.ToArgb();

					objDPArr0[6].Value= p_objPics[j].intWidth;
					objDPArr0[7].Value= p_objPics[j].intHeight;

					string strAddNewRecord_PictureSQL = @"insert into operationrecorddoctor_picture(inpatientid,inpatientdate,opendate,picid,frontimage,backcolor,width,height) 
			values(?,?,?,?,?,?,?,?)";

					long lngEff=0;
					
					lngRes =objHRPServ.lngExecuteParameterSQL(strAddNewRecord_PictureSQL,ref lngEff,objDPArr0);
					if(lngRes<=0)	
						return lngRes;
				}

				lngRes= 1;
				
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}

		/// <summary>
		/// 修改图片信息
		/// </summary>
		/// <returns></returns>
		[AutoComplete]	
		private long m_lngModifyPics(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,clsPictureBoxValue[] p_objPics)
		{
			#region 先删除旧的记录
			long lngRes = 1;
			try
			{
				//				IDataParameter[] objDPArr00 = new Oracle.DataAccess.Client.OracleParameter[3];
				//				for(int i=0;i<objDPArr00.Length;i++)
				//					objDPArr00[i]=new Oracle.DataAccess.Client.OracleParameter();
				clsHRPTableService objHRPServ =new clsHRPTableService();
				IDataParameter[] objDPArr00 = null;
				objHRPServ.CreateDatabaseParameter(3,out objDPArr00);

                objDPArr00[0].Value = p_strInPatientID;
                objDPArr00[1].DbType = DbType.DateTime;
                objDPArr00[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr00[2].DbType = DbType.DateTime;
				objDPArr00[2].Value=DateTime.Parse(p_strOpenDate);

				string strDeleteRecord_PictureSQL = "delete operationrecorddoctor_picture where inpatientid=? and inpatientdate=? and opendate=?" ;

				long lngEff=0;
				lngRes = objHRPServ.lngExecuteParameterSQL(strDeleteRecord_PictureSQL,ref lngEff,objDPArr00);
				if(lngRes<=0)	
					return lngRes;
				#endregion 先删除旧的记录

				//再添加新的记录
				lngRes= m_lngAddPics(p_strInPatientID,p_strInPatientDate,p_strOpenDate,p_objPics);
				
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;

		}		

		/// <summary>
		/// 获取画图信息
		/// </summary>
		/// <returns></returns>
		[AutoComplete]	
		public long m_lngGetPics(string p_strInPatientID,string p_strInPatientDate,
			string p_strOpenDate,out clsPictureBoxValue[] p_objPics)
		{
			long lngRes = 0;
			p_objPics=null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr1);

                objDPArr1[0].Value = p_strInPatientID;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = DateTime.Parse(p_strOpenDate);

                //生成DataTable
                DataTable dtbValue1 = new DataTable();

                //执行查询，填充结果到DataTable
                long lngRes1 = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetRecordContent_PictureSQL, ref dtbValue1, objDPArr1);

                System.Collections.ArrayList arlPic = new System.Collections.ArrayList();

                //从DataTable.Rows中获取结果
                if (lngRes1 > 0 && dtbValue1.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbValue1.Rows.Count; i++)
                    {
                        clsPictureBoxValue objPicValue = new clsPictureBoxValue();
                        objPicValue.m_bytImage = (byte[])(dtbValue1.Rows[i]["FRONTIMAGE"]);
                        objPicValue.intWidth = Convert.ToInt32(dtbValue1.Rows[i]["WIDTH"]);
                        objPicValue.intHeight = Convert.ToInt32(dtbValue1.Rows[i]["HEIGHT"]);
                        objPicValue.clrBack = System.Drawing.Color.FromArgb(Convert.ToInt32(dtbValue1.Rows[i]["BACKCOLOR"]));
                        arlPic.Add(objPicValue);
                    }
                }
                p_objPics = (clsPictureBoxValue[])arlPic.ToArray(typeof(clsPictureBoxValue));
                arlPic.Clear();

                lngRes = lngRes1;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion

        /// <summary>
        /// 保存主表
        /// </summary>
        /// <param name="p_objOperationRecord">主表内容</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewRecordDoctor(clsOperationRecordDoctor p_objOperationRecord)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            if (p_objOperationRecord == null)
            {
                return -1;
            }

            try
            {
                string strSQL = @"insert into operationrecorddoctor (inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,status,
operationnamexml,anaesthesiabeforeoperationxml,anaesthesiainoperationxml,anaesthesiacategorydosagexml,diagnosebeforeoperationxml,
diagnoseafteroperationxml,operationprocessxml,pathologyxml,sampleorextrarecordxml,summaryafteroperationxml,inliquidxml,outflowxml,
xraynumberxml,sequence_int) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(21, out objDPArr);

                objDPArr[0].Value = p_objOperationRecord.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objOperationRecord.m_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_objOperationRecord.m_strOpenDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_objOperationRecord.m_strCreateDate);
                objDPArr[4].Value = p_objOperationRecord.m_strCreateUserID;
                objDPArr[5].Value = 1;
                objDPArr[6].Value = 0;
                objDPArr[7].Value = p_objOperationRecord.m_strOperationNameXML;
                objDPArr[8].Value = p_objOperationRecord.m_strAnaesthesiaBeforeOperationXML;
                objDPArr[9].Value = p_objOperationRecord.m_strAnaesthesiaInOperationXML;
                objDPArr[10].Value = p_objOperationRecord.m_strAnaesthesiaCategoryDosageXML;
                objDPArr[11].Value = p_objOperationRecord.m_strDiagnoseBeforeOperationXML;
                objDPArr[12].Value = p_objOperationRecord.m_strDiagnoseAfterOperationXML;
                objDPArr[13].Value = p_objOperationRecord.m_strOperationProcessXML;
                objDPArr[14].Value = p_objOperationRecord.m_strPathologyXML;
                objDPArr[15].Value = p_objOperationRecord.m_strSampleOrExtraRecordXML;
                objDPArr[16].Value = p_objOperationRecord.m_strSummaryAfterOperationXML;
                objDPArr[17].Value = p_objOperationRecord.m_strInLiquidXML;
                objDPArr[18].Value = p_objOperationRecord.m_strOutFlowXML;
                objDPArr[19].Value = p_objOperationRecord.m_strXRayNumberXML;
                objDPArr[20].Value = p_objOperationRecord.int_Sequence;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 保存子表
        /// </summary>
        /// <param name="p_objOperationRecordContent">子表内容</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddNewRecordContentDoctor(clsOperationRecordContentDoctor p_objOperationRecordContent)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            if (p_objOperationRecordContent == null)
            {
                return -1;
            }

            try
            {
                string strSQL = @"insert into operationrecordcontendoctor (inpatientid,inpatientdate,opendate,lastmodifydate,lastmodifyuserid,status,
operationname,diagnosebeforeoperation,diagnoseafteroperation,operationbegindate,operationenddate,anaesthesiabeforeoperation,anaesthesiainoperation,
anaesthesiacategorydosage,anaesthesiabegindate,anaesthesiaenddate,operationprocess,pathology,inliquid,outflow,sampleorextrarecord,summaryafteroperation,
xraynumber,anaesther,doctor1,doctor2,operationdoctorid,operationdoctorname,firstassistantid,firstassistantname,secondassistantid,secondassistantname,
nurse,assistant,operationdoctor) 
values (?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?,?,?,?,?,?,
        ?,?,?,?,?)";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(35, out objDPArr);                

                objDPArr[0].Value = p_objOperationRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_objOperationRecordContent.m_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_objOperationRecordContent.m_strOpenDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_objOperationRecordContent.m_strLastModifyDate);
                objDPArr[4].Value = p_objOperationRecordContent.m_strLastModifyUserID;
                objDPArr[5].Value = 0;
                objDPArr[6].Value = p_objOperationRecordContent.m_strOperationName;
                objDPArr[7].Value = p_objOperationRecordContent.m_strDiagnoseBeforeOperation;
                objDPArr[8].Value = p_objOperationRecordContent.m_strDiagnoseAfterOperation;
                objDPArr[9].DbType = DbType.DateTime;
                objDPArr[9].Value = DateTime.Parse(p_objOperationRecordContent.m_strOperationBeginDate);
                objDPArr[10].DbType = DbType.DateTime;
                objDPArr[10].Value = DateTime.Parse(p_objOperationRecordContent.m_strOperationEndDate);
                objDPArr[11].Value = p_objOperationRecordContent.m_strAnaesthesiaBeforeOperation;
                objDPArr[12].Value = p_objOperationRecordContent.m_strAnaesthesiaInOperation;
                objDPArr[13].Value = p_objOperationRecordContent.m_strAnaesthesiaCategoryDosage;
                objDPArr[14].DbType = DbType.DateTime;
                objDPArr[14].Value = DateTime.Parse(p_objOperationRecordContent.m_strAnaesthesiaBeginDate);
                objDPArr[15].DbType = DbType.DateTime;
                objDPArr[15].Value = DateTime.Parse(p_objOperationRecordContent.m_strAnaesthesiaEndDate);
                objDPArr[16].Value = p_objOperationRecordContent.m_strOperationProcess;
                objDPArr[17].Value = p_objOperationRecordContent.m_strPathology;
                objDPArr[18].Value = p_objOperationRecordContent.m_strInLiquid;
                objDPArr[19].Value = p_objOperationRecordContent.m_strOutFlow;
                objDPArr[20].Value = p_objOperationRecordContent.m_strSampleOrExtraRecord;
                objDPArr[21].Value = p_objOperationRecordContent.m_strSummaryAfterOperation;
                objDPArr[22].Value = p_objOperationRecordContent.m_strXRayNumber;
                objDPArr[23].Value = p_objOperationRecordContent.m_strAnaesther;
                objDPArr[24].Value = p_objOperationRecordContent.m_strDoctor1;
                objDPArr[25].Value = p_objOperationRecordContent.m_strDoctor2;
                objDPArr[26].Value = p_objOperationRecordContent.m_strOperationDoctorID;
                objDPArr[27].Value = p_objOperationRecordContent.m_strOperationDoctorName;
                objDPArr[28].Value = p_objOperationRecordContent.m_strFirstAssistantID;
                objDPArr[29].Value = p_objOperationRecordContent.m_strFirstAssistantName;
                objDPArr[30].Value = p_objOperationRecordContent.m_strSecondAssistantID;
                objDPArr[31].Value = p_objOperationRecordContent.m_strSecondAssistantName;
                objDPArr[32].Value = p_objOperationRecordContent.m_strNurse;
                objDPArr[33].Value = p_objOperationRecordContent.m_strAssistant;
                objDPArr[34].Value = p_objOperationRecordContent.m_strOperationDoctor;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 修改主表
        /// </summary>
        /// <param name="p_objOperationRecord">主表内容</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngModifyRecordDoctor(clsOperationRecordDoctor p_objOperationRecord)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();

            if (p_objOperationRecord == null)
            {
                return -1;
            }

            try
            {
                string strSQL = @"update operationrecorddoctor set 
operationnamexml = ?,anaesthesiabeforeoperationxml = ?,anaesthesiainoperationxml = ?,anaesthesiacategorydosagexml = ?,diagnosebeforeoperationxml = ?,
diagnoseafteroperationxml = ?,operationprocessxml = ?,pathologyxml = ?,sampleorextrarecordxml = ?,summaryafteroperationxml = ?,inliquidxml = ?,outflowxml = ?,
xraynumberxml = ?,sequence_int = ?
 where inpatientid = ?
 and inpatientdate = ?
 and opendate = ? and status = 0";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(17, out objDPArr);

                objDPArr[0].Value = p_objOperationRecord.m_strOperationNameXML;
                objDPArr[1].Value = p_objOperationRecord.m_strAnaesthesiaBeforeOperationXML;
                objDPArr[2].Value = p_objOperationRecord.m_strAnaesthesiaInOperationXML;
                objDPArr[3].Value = p_objOperationRecord.m_strAnaesthesiaCategoryDosageXML;
                objDPArr[4].Value = p_objOperationRecord.m_strDiagnoseBeforeOperationXML;
                objDPArr[5].Value = p_objOperationRecord.m_strDiagnoseAfterOperationXML;
                objDPArr[6].Value = p_objOperationRecord.m_strOperationProcessXML;
                objDPArr[7].Value = p_objOperationRecord.m_strPathologyXML;
                objDPArr[8].Value = p_objOperationRecord.m_strSampleOrExtraRecordXML;
                objDPArr[9].Value = p_objOperationRecord.m_strSummaryAfterOperationXML;
                objDPArr[10].Value = p_objOperationRecord.m_strInLiquidXML;
                objDPArr[11].Value = p_objOperationRecord.m_strOutFlowXML;
                objDPArr[12].Value = p_objOperationRecord.m_strXRayNumberXML;
                objDPArr[13].Value = p_objOperationRecord.int_Sequence;
                objDPArr[14].Value = p_objOperationRecord.m_strInPatientID;
                objDPArr[15].DbType = DbType.DateTime;
                objDPArr[15].Value = DateTime.Parse(p_objOperationRecord.m_strInPatientDate);
                objDPArr[16].DbType = DbType.DateTime;
                objDPArr[16].Value = DateTime.Parse(p_objOperationRecord.m_strOpenDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
	}
}