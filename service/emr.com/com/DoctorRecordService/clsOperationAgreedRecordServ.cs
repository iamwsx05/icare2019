using System.EnterpriseServices;
using System;
using System.Text ;
using System.Xml;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.OperationAgreedRecordServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsOperationAgreedRecordServ : com.digitalwave.iCare.middletier.clsMiddleTierBase 
	{

[AutoComplete]
		public long m_lngAddNewRecord(
			string strXML)
		{
			long lngRes = 0;
			
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationAgreedRecordServ","m_lngAddNewRecord");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;

				if(strXML == "" || strXML == null)
					return -1;

				string strCurrentTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				StringBuilder sbdTempl=null;

                clsHRPTableService objHRPServ = new clsHRPTableService();
                try
                {
                    sbdTempl = new StringBuilder(strXML);
                    int intIndex = strXML.IndexOf(" ");
                    sbdTempl.Insert(intIndex, " ModifyDate =  '" + strCurrentTime + "' ");
                    strXML = sbdTempl.ToString();

                    lngRes = objHRPServ.add_new_record("OperationRecordAgreed", strXML);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
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
	/// 
	/// </summary>
	/// <param name="p_objPrincipal"></param>
	/// <param name="strInPatientID"></param>
	/// <param name="strInPatientDate"></param>
	/// <param name="strCreateDate"></param>
	/// <param name="strReceivedXML"></param>
	/// <param name="intReturnRows"></param>
	/// <returns></returns>
		[AutoComplete]
		public long lngSelectNewRecord(
			string strInPatientID, string strInPatientDate,string strCreateDate,ref string strReceivedXML,ref int intReturnRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationAgreedRecordServ", "lngSelectNewRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       createuserid,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       status,
       beforedisgone,
       operationname,
       auris_1,
       auris_2,
       auris_3,
       auris_4,
       auris_5,
       auris_6,
       auris_7,
       auris_8,
       auris_9,
       auris_10,
       auris_11,
       auris_12,
       auris_13,
       auris_14,
       auris_15,
       auris_16,
       auris_17,
       auris_18,
       auris_19,
       auris_20,
       auris_21,
       auris_22,
       auris_23,
       auris_24,
       auris_25,
       auris_26,
       auris_27,
       auris_28,
       auris_29,
       auris_30,
       auris_31,
       auris_32,
       auris_33,
       auris_34,
       auris_35,
       auris_36,
       auris_37,
       auris_38,
       nose_1,
       nose_2,
       nose_3,
       nose_4,
       nose_5,
       nose_6,
       nose_7,
       nose_8,
       nose_9,
       nose_10,
       nose_11,
       nose_12,
       nose_13,
       nose_14,
       nose_15,
       nose_16,
       nose_17,
       nose_18,
       nose_19,
       nose_20,
       nose_21,
       nose_22,
       nose_23,
       nose_24,
       nose_25,
       nose_26,
       nose_27,
       nose_28,
       nose_29,
       nose_30,
       nose_31,
       fauces_1,
       fauces_2,
       fauces_3,
       fauces_4,
       fauces_5,
       fauces_6,
       fauces_7,
       fauces_8,
       fauces_9,
       fauces_10,
       fauces_11,
       fauces_12,
       fauces_13,
       fauces_14,
       fauces_15,
       fauces_16,
       fauces_17,
       fauces_18,
       fauces_19,
       fauces_20,
       fauces_21,
       fauces_22,
       fauces_23,
       fauces_24,
       head_1,
       head_2,
       head_3,
       head_4,
       head_5,
       head_6,
       head_7,
       head_8,
       head_9,
       head_10,
       head_11,
       head_12,
       head_13,
       head_14,
       head_15,
       head_16,
       head_17,
       head_18,
       larynxgullet_1,
       larynxgullet_2,
       larynxgullet_3,
       larynxgullet_4,
       larynxgullet_5,
       larynxgullet_6,
       larynxgullet_7,
       larynxgullet_8,
       larynxgullet_9,
       larynxgullet_10,
       larynxgullet_11,
       larynxgullet_12,
       larynxgullet_13,
       larynxgullet_14,
       larynxgullet_15,
       larynxgullet_16,
       larynxgullet_17,
       larynxgullet_18,
       larynxgullet_19,
       larynxgullet_20,
       larynxgullet_21,
       relation,
       writer,
       phone,
       talkdoc,
       relationsufferer,
       relationid
  from (select inpatientid,
               inpatientdate,
               createdate,
               modifydate,
               createuserid,
               ifconfirm,
               confirmreason,
               confirmreasonxml,
               firstprintdate,
               deactiveddate,
               deactivedoperatorid,
               status,
               beforedisgone,
               operationname,
               auris_1,
               auris_2,
               auris_3,
               auris_4,
               auris_5,
               auris_6,
               auris_7,
               auris_8,
               auris_9,
               auris_10,
               auris_11,
               auris_12,
               auris_13,
               auris_14,
               auris_15,
               auris_16,
               auris_17,
               auris_18,
               auris_19,
               auris_20,
               auris_21,
               auris_22,
               auris_23,
               auris_24,
               auris_25,
               auris_26,
               auris_27,
               auris_28,
               auris_29,
               auris_30,
               auris_31,
               auris_32,
               auris_33,
               auris_34,
               auris_35,
               auris_36,
               auris_37,
               auris_38,
               nose_1,
               nose_2,
               nose_3,
               nose_4,
               nose_5,
               nose_6,
               nose_7,
               nose_8,
               nose_9,
               nose_10,
               nose_11,
               nose_12,
               nose_13,
               nose_14,
               nose_15,
               nose_16,
               nose_17,
               nose_18,
               nose_19,
               nose_20,
               nose_21,
               nose_22,
               nose_23,
               nose_24,
               nose_25,
               nose_26,
               nose_27,
               nose_28,
               nose_29,
               nose_30,
               nose_31,
               fauces_1,
               fauces_2,
               fauces_3,
               fauces_4,
               fauces_5,
               fauces_6,
               fauces_7,
               fauces_8,
               fauces_9,
               fauces_10,
               fauces_11,
               fauces_12,
               fauces_13,
               fauces_14,
               fauces_15,
               fauces_16,
               fauces_17,
               fauces_18,
               fauces_19,
               fauces_20,
               fauces_21,
               fauces_22,
               fauces_23,
               fauces_24,
               head_1,
               head_2,
               head_3,
               head_4,
               head_5,
               head_6,
               head_7,
               head_8,
               head_9,
               head_10,
               head_11,
               head_12,
               head_13,
               head_14,
               head_15,
               head_16,
               head_17,
               head_18,
               larynxgullet_1,
               larynxgullet_2,
               larynxgullet_3,
               larynxgullet_4,
               larynxgullet_5,
               larynxgullet_6,
               larynxgullet_7,
               larynxgullet_8,
               larynxgullet_9,
               larynxgullet_10,
               larynxgullet_11,
               larynxgullet_12,
               larynxgullet_13,
               larynxgullet_14,
               larynxgullet_15,
               larynxgullet_16,
               larynxgullet_17,
               larynxgullet_18,
               larynxgullet_19,
               larynxgullet_20,
               larynxgullet_21,
               relation,
               writer,
               phone,
               talkdoc,
               relationsufferer,
               relationid
          from operationrecordagreed
         where inpatientid = ?
           and inpatientdate = ?
           and createdate = ?
           and status = 0
         order by modifydate desc)
 where rownum = 1";

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                objDPArr2[0].Value = strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(strInPatientDate);
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = DateTime.Parse(strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref strReceivedXML, ref intReturnRows, objDPArr2);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="strInPatientID"></param>
		/// <param name="strInPatientDate"></param>
		/// <param name="strCreateDate"></param>
		/// <param name="strReceivedXML"></param>
		/// <param name="intReturnRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long lngGetDeletedRecord(
			string strInPatientID, string strInPatientDate,string strCreateDate,ref string strReceivedXML,ref int intReturnRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationAgreedRecordServ", "lngGetDeletedRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       createuserid,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       status,
       beforedisgone,
       operationname,
       auris_1,
       auris_2,
       auris_3,
       auris_4,
       auris_5,
       auris_6,
       auris_7,
       auris_8,
       auris_9,
       auris_10,
       auris_11,
       auris_12,
       auris_13,
       auris_14,
       auris_15,
       auris_16,
       auris_17,
       auris_18,
       auris_19,
       auris_20,
       auris_21,
       auris_22,
       auris_23,
       auris_24,
       auris_25,
       auris_26,
       auris_27,
       auris_28,
       auris_29,
       auris_30,
       auris_31,
       auris_32,
       auris_33,
       auris_34,
       auris_35,
       auris_36,
       auris_37,
       auris_38,
       nose_1,
       nose_2,
       nose_3,
       nose_4,
       nose_5,
       nose_6,
       nose_7,
       nose_8,
       nose_9,
       nose_10,
       nose_11,
       nose_12,
       nose_13,
       nose_14,
       nose_15,
       nose_16,
       nose_17,
       nose_18,
       nose_19,
       nose_20,
       nose_21,
       nose_22,
       nose_23,
       nose_24,
       nose_25,
       nose_26,
       nose_27,
       nose_28,
       nose_29,
       nose_30,
       nose_31,
       fauces_1,
       fauces_2,
       fauces_3,
       fauces_4,
       fauces_5,
       fauces_6,
       fauces_7,
       fauces_8,
       fauces_9,
       fauces_10,
       fauces_11,
       fauces_12,
       fauces_13,
       fauces_14,
       fauces_15,
       fauces_16,
       fauces_17,
       fauces_18,
       fauces_19,
       fauces_20,
       fauces_21,
       fauces_22,
       fauces_23,
       fauces_24,
       head_1,
       head_2,
       head_3,
       head_4,
       head_5,
       head_6,
       head_7,
       head_8,
       head_9,
       head_10,
       head_11,
       head_12,
       head_13,
       head_14,
       head_15,
       head_16,
       head_17,
       head_18,
       larynxgullet_1,
       larynxgullet_2,
       larynxgullet_3,
       larynxgullet_4,
       larynxgullet_5,
       larynxgullet_6,
       larynxgullet_7,
       larynxgullet_8,
       larynxgullet_9,
       larynxgullet_10,
       larynxgullet_11,
       larynxgullet_12,
       larynxgullet_13,
       larynxgullet_14,
       larynxgullet_15,
       larynxgullet_16,
       larynxgullet_17,
       larynxgullet_18,
       larynxgullet_19,
       larynxgullet_20,
       larynxgullet_21,
       relation,
       writer,
       phone,
       talkdoc,
       relationsufferer,
       relationid
  from (select inpatientid,
               inpatientdate,
               createdate,
               modifydate,
               createuserid,
               ifconfirm,
               confirmreason,
               confirmreasonxml,
               firstprintdate,
               deactiveddate,
               deactivedoperatorid,
               status,
               beforedisgone,
               operationname,
               auris_1,
               auris_2,
               auris_3,
               auris_4,
               auris_5,
               auris_6,
               auris_7,
               auris_8,
               auris_9,
               auris_10,
               auris_11,
               auris_12,
               auris_13,
               auris_14,
               auris_15,
               auris_16,
               auris_17,
               auris_18,
               auris_19,
               auris_20,
               auris_21,
               auris_22,
               auris_23,
               auris_24,
               auris_25,
               auris_26,
               auris_27,
               auris_28,
               auris_29,
               auris_30,
               auris_31,
               auris_32,
               auris_33,
               auris_34,
               auris_35,
               auris_36,
               auris_37,
               auris_38,
               nose_1,
               nose_2,
               nose_3,
               nose_4,
               nose_5,
               nose_6,
               nose_7,
               nose_8,
               nose_9,
               nose_10,
               nose_11,
               nose_12,
               nose_13,
               nose_14,
               nose_15,
               nose_16,
               nose_17,
               nose_18,
               nose_19,
               nose_20,
               nose_21,
               nose_22,
               nose_23,
               nose_24,
               nose_25,
               nose_26,
               nose_27,
               nose_28,
               nose_29,
               nose_30,
               nose_31,
               fauces_1,
               fauces_2,
               fauces_3,
               fauces_4,
               fauces_5,
               fauces_6,
               fauces_7,
               fauces_8,
               fauces_9,
               fauces_10,
               fauces_11,
               fauces_12,
               fauces_13,
               fauces_14,
               fauces_15,
               fauces_16,
               fauces_17,
               fauces_18,
               fauces_19,
               fauces_20,
               fauces_21,
               fauces_22,
               fauces_23,
               fauces_24,
               head_1,
               head_2,
               head_3,
               head_4,
               head_5,
               head_6,
               head_7,
               head_8,
               head_9,
               head_10,
               head_11,
               head_12,
               head_13,
               head_14,
               head_15,
               head_16,
               head_17,
               head_18,
               larynxgullet_1,
               larynxgullet_2,
               larynxgullet_3,
               larynxgullet_4,
               larynxgullet_5,
               larynxgullet_6,
               larynxgullet_7,
               larynxgullet_8,
               larynxgullet_9,
               larynxgullet_10,
               larynxgullet_11,
               larynxgullet_12,
               larynxgullet_13,
               larynxgullet_14,
               larynxgullet_15,
               larynxgullet_16,
               larynxgullet_17,
               larynxgullet_18,
               larynxgullet_19,
               larynxgullet_20,
               larynxgullet_21,
               relation,
               writer,
               phone,
               talkdoc,
               relationsufferer,
               relationid
          from operationrecordagreed
         where inpatientid = ?
           and inpatientdate = ?
           and createdate = ?
           and status = 1
         order by modifydate desc)
 where rownum = 1";

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
                objDPArr2[0].Value = strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(strInPatientDate);
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = DateTime.Parse(strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref strReceivedXML, ref intReturnRows, objDPArr2);

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
		/// 所有的同意书时间
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOperationAgreedRecordServ", "m_lngGetTimeInfoOfAPatient");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = @"select base.createdate as createdate  from operationrecordagreed ct, 
                     (select max(modifydate) as modifydate,createdate,inpatientid,inpatientdate,status  
                     from operationrecordagreed 
                     group by createdate,inpatientid,inpatientdate,status )base 
                     where ct.inpatientid = base.inpatientid  and base.status = '0' 
                     and ct.inpatientdate = base.inpatientdate 
                     and ct.createdate = base.createdate 
                     and ct.modifydate = base.modifydate 
                     and ct.inpatientid = ?
                     and ct.inpatientdate = ?" ;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr2);
                objDPArr2[0].Value = p_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr2);

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
		/// 新增手术（麻醉，介入治疗）前签字同意书
		/// </summary>
		/// <param name="p_objContent">对象</param>
		/// <returns>成功标志</returns>
		[AutoComplete]	
		public  long m_lngAddItemRecord(clsOpraAnaSignAgree p_objContent)
		{
			if(p_objContent == null )
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string m_strTempSql = @"insert into opraanasignagree
										(inpatientid, inpatientdate, opendate, createdate, createuserid,status,
												stateofillness, action,badfactor,syndrome,
												relationsign,relationsigndate,leadsign,leadsigndate,doctorsign,doctorsigndate ,directorsign ,directorsigndate)
									values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?,?,?,?,?,?,?)";


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(18, out objDPArr2);
                objDPArr2[0].Value = p_objContent.m_strInpatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_objContent.m_dtInpatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = p_objContent.m_dtOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = p_objContent.m_dtCreateDate;
                objDPArr2[4].Value = p_objContent.m_strCreateUserID;
                objDPArr2[5].Value = p_objContent.m_strStatus;
                objDPArr2[6].Value = p_objContent.m_strStateOfIllness;
                objDPArr2[7].Value = p_objContent.m_strAction;
                objDPArr2[8].Value = p_objContent.m_strBadFactor;
                objDPArr2[9].Value = p_objContent.m_strSyndrome;
                objDPArr2[10].Value = p_objContent.m_strRelationSign;
                objDPArr2[11].DbType = DbType.DateTime;
                objDPArr2[11].Value = p_objContent.m_dtRelationSignDate;
                objDPArr2[12].Value = p_objContent.m_strLeadsign;
                objDPArr2[13].DbType = DbType.DateTime;
                objDPArr2[13].Value = p_objContent.m_dtLeadSignDate;
                objDPArr2[14].Value = p_objContent.m_strDoctorSign;
                objDPArr2[15].DbType = DbType.DateTime;
                objDPArr2[15].Value = p_objContent.m_strDoctorSignDate;
                objDPArr2[16].Value = p_objContent.m_strDirectorSign;
                objDPArr2[17].DbType = DbType.DateTime;
                objDPArr2[17].Value = p_objContent.m_dtDirectorSignDate;
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strTempSql, ref lngEff, objDPArr2);

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
		/// 修改手术（麻醉，介入治疗）前签字同意书
		/// </summary>
		/// <param name="p_objContent">对象</param>
		/// <returns>成功标志</returns>
		[AutoComplete]		
		public  long m_lngUpateItemRecord(clsOpraAnaSignAgree p_objContent)
		{
			if(p_objContent == null )
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string m_strTempSql = @"update opraanasignagree set
			opendate=?, modifydate=?, modifyuserid=?,
				stateofillness=?, action=?,badfactor=?,syndrome=?, 
				relationsign=?,relationsigndate=?,leadsign=?,leadsigndate=?,doctorsign=?,doctorsigndate=? ,directorsign=? ,directorsigndate=?
				where inpatientid=? and createdate=?  and status=0";

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(17, out objDPArr2);

                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = p_objContent.m_dtOpenDate;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_objContent.m_dtModifyDate;
                objDPArr2[2].Value = p_objContent.m_strModifyUserID;
                objDPArr2[3].Value = p_objContent.m_strStateOfIllness;
                objDPArr2[4].Value = p_objContent.m_strAction;
                objDPArr2[5].Value = p_objContent.m_strBadFactor;
                objDPArr2[6].Value = p_objContent.m_strSyndrome;
                objDPArr2[7].Value = p_objContent.m_strRelationSign;
                objDPArr2[8].DbType = DbType.DateTime;
                objDPArr2[8].Value = p_objContent.m_dtRelationSignDate;
                objDPArr2[9].Value = p_objContent.m_strLeadsign;
                objDPArr2[10].DbType = DbType.DateTime;
                objDPArr2[10].Value = p_objContent.m_dtLeadSignDate;
                objDPArr2[11].Value = p_objContent.m_strDoctorSign;
                objDPArr2[12].DbType = DbType.DateTime;
                objDPArr2[12].Value = p_objContent.m_strDoctorSignDate;
                objDPArr2[13].Value = p_objContent.m_strDirectorSign;
                objDPArr2[14].DbType = DbType.DateTime;
                objDPArr2[14].Value = p_objContent.m_dtDirectorSignDate;
                objDPArr2[15].Value = p_objContent.m_strInpatientID;
                objDPArr2[16].DbType = DbType.DateTime;
                objDPArr2[16].Value = p_objContent.m_dtCreateDate;
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strTempSql, ref lngEff, objDPArr2);


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
		/// 删除手术（麻醉，介入治疗）前签字同意书
		/// </summary>
		/// <param name="p_strInPatientID">对象id</param>
		/// <returns>成功标志</returns>
		[AutoComplete]		
		public  long m_lngDeleteItemRecord(string p_strInPatientID,string p_strDeleteUserID,
			DateTime p_dtCreateDate)
		{
			if(p_strInPatientID == null||p_strInPatientID==""  )
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string m_strTempSql = "";
                //				if (clsHRPTableService.bytDatabase_Selector==0)
                //				{
                m_strTempSql = @"update opraanasignagree set status='1',deactiveddate =?,deactivedoperatorid = ? where inpatientid=? and createdate=?";
                //				}
                //				else
                //				{
                //					m_strTempSql = @"update opraanasignagree set Status='1',DeActivedDate = timestamp'"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"',DeActivedOperatorID = '"+p_strDeleteUserID+"' where InPatientID=? and CreateDate=?";
                //				}

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr2);
                objDPArr2[0].DbType = DbType.DateTime;
                objDPArr2[0].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr2[1].Value = p_strDeleteUserID;
                objDPArr2[2].Value = p_strInPatientID;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = p_dtCreateDate;
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(m_strTempSql, ref lngEff, objDPArr2);


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
		/// 获取手术（麻醉，介入治疗）前签字同意书
		/// </summary>
		/// <param name="p_strInPatientID">对象id</param>
		/// <returns>成功标志</returns>
		[AutoComplete]		
		public  long m_lngGetItemRecord(string p_strInPatientID,
			DateTime p_dtCreateDate,out clsOpraAnaSignAgree p_objContent)
		{
			//初始化返回参数
			p_objContent=null;	
			if(p_strInPatientID == null||p_strInPatientID==""  )
				return -1;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string m_strTempSql = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       stateofillness,
       action,
       badfactor,
       syndrome,
       relationsign,
       relationsigndate,
       leadsign,
       leadsigndate,
       doctorsign,
       doctorsigndate,
       directorsign,
       directorsigndate,
       modifyuserid,
       modifydate,
       lastprintdate,
       f_getempnamebyno_1stofall(o.doctorsign) doctorname,
       f_getempnamebyno_1stofall(o.directorsign) directorname
  from opraanasignagree o
 where InPatientID = ?
   and CreateDate = ?
   and Status = 0";

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr2);

                objDPArr2[0].Value = p_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = p_dtCreateDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strTempSql, ref dtbValue, objDPArr2);
              
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    clsOpraAnaSignAgree objRecordContent = new clsOpraAnaSignAgree();
                    objRecordContent.m_strInpatientID = p_strInPatientID;
                    objRecordContent.m_dtInpatientDate = DateTime.Parse(dtbValue.Rows[0]["INPATIENTDATE"].ToString());
                    objRecordContent.m_strStateOfIllness = dtbValue.Rows[0]["STATEOFILLNESS"].ToString();
                    objRecordContent.m_strAction = dtbValue.Rows[0]["ACTION"].ToString();
                    objRecordContent.m_strBadFactor = dtbValue.Rows[0]["BADFACTOR"].ToString();
                    objRecordContent.m_strSyndrome = dtbValue.Rows[0]["SYNDROME"].ToString();
                    objRecordContent.m_strRelationSign = dtbValue.Rows[0]["RELATIONSIGN"].ToString();
                    objRecordContent.m_dtRelationSignDate = DateTime.Parse(dtbValue.Rows[0]["RELATIONSIGNDATE"].ToString());
                    objRecordContent.m_strLeadsign = dtbValue.Rows[0]["LEADSIGN"].ToString();
                    objRecordContent.m_dtLeadSignDate = DateTime.Parse(dtbValue.Rows[0]["LEADSIGNDATE"].ToString());
                    objRecordContent.m_strDoctorSign = dtbValue.Rows[0]["DOCTORSIGN"].ToString();
                    objRecordContent.m_strDoctorName = dtbValue.Rows[0]["DOCTORNAME"].ToString();
                    objRecordContent.m_strDoctorSignDate = DateTime.Parse(dtbValue.Rows[0]["DOCTORSIGNDATE"].ToString());
                    objRecordContent.m_strDirectorSign = dtbValue.Rows[0]["DIRECTORSIGN"].ToString();
                    objRecordContent.m_strDirectorName = dtbValue.Rows[0]["DIRECTORNAME"].ToString();
                    objRecordContent.m_dtDirectorSignDate = DateTime.Parse(dtbValue.Rows[0]["DIRECTORSIGNDATE"].ToString());
                    objRecordContent.m_strStatus = dtbValue.Rows[0]["STATUS"].ToString();

                    p_objContent = objRecordContent;
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

		/// <summary>
		/// 获取病人的该记录时间列表。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
		/// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
		/// <returns></returns>
		[AutoComplete]		
		public  long m_lngGetRecordTimeList(
			string p_strInPatientID,
			DateTime p_strInPatientDate,
			out string[] p_strCreateDateArr,
			out string[] p_strOpenDateArr)
		{
			p_strCreateDateArr=null;
			p_strOpenDateArr=null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string m_strTempSql = @"select createdate,opendate  from  opraanasignagree where inpatientid=?  and inpatientdate=? and status=0";

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "")
                    return -1;

                 IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_strInPatientDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(m_strTempSql, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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

            }			//返回
			return lngRes;

		}
	}
}
