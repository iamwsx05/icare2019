using System.EnterpriseServices;
using System;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.BaseCaseHistorySevice;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Drawing;
using System.Collections;

namespace com.digitalwave.InPatientCaseHistoryServ
{
    /// <summary>
    /// 新生儿入室记录
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsNewBabyInRoomRecordService : clsBaseCaseHistorySevice
    {
        public clsNewBabyInRoomRecordService()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private const string c_strGetTimeListSQL = "select inpatientdate,createdate,opendate from newbabyinroomrecord where inpatientid = ?  and status=0 order by opendate desc";

        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.babysex,
       a.birthtime,
       a.childbearing,
       a.pregnanttime,
       a.reaction,
       a.musclestrain,
       a.cryvoice,
       a.dropsy,
       a.skincolor,
       a.elasticity,
       a.icterus,
       a.pigment,
       a.petechia,
       a.birthburl,
       a.haematoma,
       a.skullsoft,
       a.bonesew,
       a.fontanel,
       a.headround,
       a.facialfeatures,
       a.mouth,
       a.heart,
       a.lung,
       a.chest,
       a.vein,
       a.liver,
       a.spleen,
       a.hilum,
       a.activity,
       a.arthrosis,
       a.abnormality,
       a.genitalia,
       a.otherrecord,
       a.otherrecordxml,
       a.checkdate,
       a.inhospitaldays,
       a.weight,
       a.head,
       a.skin,
       a.heart_outhospital,
       a.lung_outhospital,
       a.genitalia_outhospital,
       a.abdomen,
       a.umbilicalcordlefttime,
       a.buttocks,
       a.limb,
       a.normalcircs,
       a.lactation,
       a.bcgvaccine,
       a.bliverbacterin,
       a.othercheck,
       a.othercheckxml,
       a.outhospitaladvice,
       a.outhospitaladvicexml,
       a.dealwith,
       a.dealwithxml,
       a.inroomcheckdocid,
       a.recordsigndocid,
       a.modifydate,
       a.modifyuserid,
       a.inroomcheckdocname,
       a.recordsigndocname,
        a.sequence_int,
        a.checkedchange,
        a.outhospitaldate,
       tbe.lastname_vchr
  from newbabyinroomrecord a, t_bse_employee tbe
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.createuserid = tbe.empno_chr";

        private const string c_strGetDeletedRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.babysex,
       a.birthtime,
       a.childbearing,
       a.pregnanttime,
       a.reaction,
       a.musclestrain,
       a.cryvoice,
       a.dropsy,
       a.skincolor,
       a.elasticity,
       a.icterus,
       a.pigment,
       a.petechia,
       a.birthburl,
       a.haematoma,
       a.skullsoft,
       a.bonesew,
       a.fontanel,
       a.headround,
       a.facialfeatures,
       a.mouth,
       a.heart,
       a.lung,
       a.chest,
       a.vein,
       a.liver,
       a.spleen,
       a.hilum,
       a.activity,
       a.arthrosis,
       a.abnormality,
       a.genitalia,
       a.otherrecord,
       a.otherrecordxml,
       a.checkdate,
       a.inhospitaldays,
       a.weight,
       a.head,
       a.skin,
       a.heart_outhospital,
       a.lung_outhospital,
       a.genitalia_outhospital,
       a.abdomen,
       a.umbilicalcordlefttime,
       a.buttocks,
       a.limb,
       a.normalcircs,
       a.lactation,
       a.bcgvaccine,
       a.bliverbacterin,
       a.othercheck,
       a.othercheckxml,
       a.outhospitaladvice,
       a.outhospitaladvicexml,
       a.dealwith,
       a.dealwithxml,
       a.inroomcheckdocid,
       a.recordsigndocid,
       a.modifydate,
       a.modifyuserid,
       a.inroomcheckdocname,
       a.recordsigndocname,a.sequence_int,a.checkedchange,a.outhospitaldate,
       tbe.lastname_vchr
  from newbabyinroomrecord a, t_bse_employee tbe
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 1
   and a.createuserid = tbe.empno_chr";


        private const string c_strCheckCreateDateSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate,
       babysex,
       birthtime,
       childbearing,
       pregnanttime,
       reaction,
       musclestrain,
       cryvoice,
       dropsy,
       skincolor,
       elasticity,
       icterus,
       pigment,
       petechia,
       birthburl,
       haematoma,
       skullsoft,
       bonesew,
       fontanel,
       headround,
       facialfeatures,
       mouth,
       heart,
       lung,
       chest,
       vein,
       liver,
       spleen,
       hilum,
       activity,
       arthrosis,
       abnormality,
       genitalia,
       otherrecord,
       otherrecordxml,
       checkdate,
       inhospitaldays,
       weight,
       head,
       skin,
       heart_outhospital,
       lung_outhospital,
       genitalia_outhospital,
       abdomen,
       umbilicalcordlefttime,
       buttocks,
       limb,
       normalcircs,
       lactation,
       bcgvaccine,
       bliverbacterin,
       othercheck,
       othercheckxml,
       outhospitaladvice,
       outhospitaladvicexml,
       dealwith,
       dealwithxml,
       inroomcheckdocid,
       recordsigndocid,
       modifydate,
       modifyuserid,
       inroomcheckdocname,
       recordsigndocname,sequence_int,
  from newbabyinroomrecord
 where inpatientid = ?
   and inpatientdate = ?
   and status = 0";


        /// <summary>
        /// 更新GeneralDiseaseRecord中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = "update  newbabyinroomrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        private const string c_strGetDeleteRecordSQL = "";

        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select firstprintdate,modifydate
																			from newbabyinroomrecord
																			where inpatientid = ?
																			and inpatientdate = ?
																			and status = 0";

        private const string c_strModifyRecordSQL = @"update newbabyinroomrecord set modifydate=?,modifyuserid=?, babysex=?,birthtime=?,childbearing=?,
			pregnanttime=?,reaction=?,musclestrain=?,cryvoice=?,dropsy=?,skincolor=?,elasticity=?,icterus=?,pigment=?,petechia=?,
			birthburl=?,haematoma=?,skullsoft=?,bonesew=?,fontanel=?,headround=?,facialfeatures=?,mouth=?,heart=?,lung=?,chest=?,
			vein=?,liver=?,spleen=?,hilum=?,activity=?,arthrosis=?,abnormality=?,genitalia=?,otherrecord=?,otherrecordxml=?,checkdate=?,
			inhospitaldays=?,weight=?,head=?,skin=?,heart_outhospital=?,lung_outhospital=?,genitalia_outhospital=?,abdomen=?,
			umbilicalcordlefttime=?,buttocks=?,limb=?,normalcircs=?,lactation=?,bcgvaccine=?,bliverbacterin=?,othercheck=?,othercheckxml=?,
			outhospitaladvice=?,outhospitaladvicexml=?,dealwith=?,dealwithxml=?,inroomcheckdocid=?,recordsigndocid=? ,inroomcheckdocname=?,recordsigndocname=?,sequence_int=?,checkedchange=?,outhospitaldate=?
			where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strDeleteRecordSQL = "update newbabyinroomrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strAddNewRecordSQL = @"insert into newbabyinroomrecord (inpatientid,inpatientdate,opendate,createdate,
			createuserid,status,modifydate,modifyuserid,babysex,birthtime,childbearing,pregnanttime,
			reaction,musclestrain,cryvoice,dropsy,skincolor,elasticity,icterus,pigment,petechia,birthburl,haematoma,skullsoft,
			bonesew,fontanel,headround,facialfeatures,mouth,heart,lung,chest,vein,liver,spleen,hilum,activity,arthrosis,
			abnormality,genitalia,otherrecord,otherrecordxml,checkdate,inhospitaldays,weight,head,skin,heart_outhospital,
			lung_outhospital,genitalia_outhospital,abdomen,umbilicalcordlefttime,buttocks,limb,normalcircs,lactation,bcgvaccine,
			bliverbacterin,othercheck,othercheckxml,outhospitaladvice,outhospitaladvicexml,dealwith,dealwithxml,
			inroomcheckdocid,recordsigndocid,inroomcheckdocname,recordsigndocname,sequence_int,checkedchange,outhospitaldate) 
			values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
					?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
					?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        ///  获取病人的该记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDateArr"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strInPatientID,
            out string[] p_strInPatientDateArr,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strInPatientDateArr = null;
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngGetRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "")
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strInPatientDateArr = new string[dtbValue.Rows.Count];
                    p_strCreateRecordTimeArr = new string[dtbValue.Rows.Count];
                    p_strOpenRecordTimeArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strInPatientDateArr[i] = dtbValue.Rows[i]["INPATIENTDATE"].ToString();
                        p_strCreateRecordTimeArr[i] = dtbValue.Rows[i]["CREATEDATE"].ToString();
                        p_strOpenRecordTimeArr[i] = dtbValue.Rows[i]["OPENDATE"].ToString();
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

            }       //返回
            return lngRes;


        }

        /// <summary>
        ///  更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数                              
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return (long)enmOperationResult.Parameter_Error;
            //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

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
        ///  获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strDeleteUserID"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngGetDeleteRecordTimeList");
                //if(lngCheckRes <= 0)
                //return lngCheckRes;	
                lngRes = (long)enmOperationResult.DB_Succeed;

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
        ///  获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientCaseHistoryServ","m_lngGetDeleteRecordTimeListAll");
                //if(lngCheckRes <= 0)
                //return lngCheckRes;	

                lngRes = (long)enmOperationResult.DB_Succeed;

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
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(
            string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsBaseCaseHistoryInfo p_objRecordContent,
            out clsPictureBoxValue[] p_objPicValueArr)
        {
            p_objRecordContent = null;
            p_objPicValueArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsNewBabyInRoomRecord p_objContent = new clsNewBabyInRoomRecord();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        #region 获取结果
                        p_objContent.m_strInPatientID = p_strInPatientID;
                        p_objContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                        p_objContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        p_objContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();

                        if (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            p_objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else p_objContent.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[i]["FIRSTPRINTDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            p_objContent.m_bytStatus = 0;
                        else p_objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        p_objContent.m_strBABYSEX = dtbValue.Rows[i]["BABYSEX"].ToString();
                        p_objContent.m_dtmBIRTHTIME = Convert.ToDateTime(dtbValue.Rows[i]["BIRTHTIME"]);
                        p_objContent.m_strCHILDBEARING = dtbValue.Rows[i]["CHILDBEARING"].ToString();
                        p_objContent.m_strPREGNANTTIME = dtbValue.Rows[i]["PREGNANTTIME"].ToString();
                        p_objContent.m_strREACTION = dtbValue.Rows[i]["REACTION"].ToString();
                        p_objContent.m_strMUSCLESTRAIN = dtbValue.Rows[i]["MUSCLESTRAIN"].ToString();
                        p_objContent.m_strCRYVOICE = dtbValue.Rows[i]["CRYVOICE"].ToString();
                        p_objContent.m_strDROPSY = dtbValue.Rows[i]["DROPSY"].ToString();
                        p_objContent.m_strSKINCOLOR = dtbValue.Rows[i]["SKINCOLOR"].ToString();
                        p_objContent.m_strELASTICITY = dtbValue.Rows[i]["ELASTICITY"].ToString();
                        p_objContent.m_strICTERUS = dtbValue.Rows[i]["ICTERUS"].ToString();
                        p_objContent.m_strPIGMENT = dtbValue.Rows[i]["PIGMENT"].ToString();
                        p_objContent.m_strPETECHIA = dtbValue.Rows[i]["PETECHIA"].ToString();
                        p_objContent.m_strBIRTHBURL = dtbValue.Rows[i]["BIRTHBURL"].ToString();
                        p_objContent.m_strHAEMATOMA = dtbValue.Rows[i]["HAEMATOMA"].ToString();
                        p_objContent.m_strSKULLSOFT = dtbValue.Rows[i]["SKULLSOFT"].ToString();
                        p_objContent.m_strBONESEW = dtbValue.Rows[i]["BONESEW"].ToString();
                        p_objContent.m_strFONTANEL = dtbValue.Rows[i]["FONTANEL"].ToString();
                        p_objContent.m_strHEADROUND = dtbValue.Rows[i]["HEADROUND"].ToString();
                        p_objContent.m_strFACIALFEATURES = dtbValue.Rows[i]["FACIALFEATURES"].ToString();
                        p_objContent.m_strMOUTH = dtbValue.Rows[i]["MOUTH"].ToString();
                        p_objContent.m_strHEART = dtbValue.Rows[i]["HEART"].ToString();
                        p_objContent.m_strLUNG = dtbValue.Rows[i]["LUNG"].ToString();
                        p_objContent.m_strCHEST = dtbValue.Rows[i]["CHEST"].ToString();
                        p_objContent.m_strVEIN = dtbValue.Rows[i]["VEIN"].ToString();
                        p_objContent.m_strLIVER = dtbValue.Rows[i]["LIVER"].ToString();
                        p_objContent.m_strSPLEEN = dtbValue.Rows[i]["SPLEEN"].ToString();
                        p_objContent.m_strHILUM = dtbValue.Rows[i]["HILUM"].ToString();
                        p_objContent.m_strACTIVITY = dtbValue.Rows[i]["ACTIVITY"].ToString();
                        p_objContent.m_strARTHROSIS = dtbValue.Rows[i]["ARTHROSIS"].ToString();
                        p_objContent.m_strABNORMALITY = dtbValue.Rows[i]["ABNORMALITY"].ToString();
                        p_objContent.m_strGENITALIA = dtbValue.Rows[i]["GENITALIA"].ToString();
                        p_objContent.m_strOTHERRECORD = dtbValue.Rows[i]["OTHERRECORD"].ToString();
                        p_objContent.m_strOTHERRECORDXML = dtbValue.Rows[i]["OTHERRECORDXML"].ToString();
                        p_objContent.m_dtmCHECKDATE = Convert.ToDateTime(dtbValue.Rows[i]["CHECKDATE"]);
                        p_objContent.m_strINHOSPITALDAYS = dtbValue.Rows[i]["INHOSPITALDAYS"].ToString();
                        p_objContent.m_strWEIGHT = dtbValue.Rows[i]["WEIGHT"].ToString();
                        p_objContent.m_strHEAD = dtbValue.Rows[i]["HEAD"].ToString();
                        p_objContent.m_strSKIN = dtbValue.Rows[i]["SKIN"].ToString();
                        p_objContent.m_strHEART_OUTHOSPITAL = dtbValue.Rows[i]["HEART_OUTHOSPITAL"].ToString();
                        p_objContent.m_strLUNG_OUTHOSPITAL = dtbValue.Rows[i]["LUNG_OUTHOSPITAL"].ToString();
                        p_objContent.m_strGENITALIA_OUTHOSPITAL = dtbValue.Rows[i]["GENITALIA_OUTHOSPITAL"].ToString();
                        p_objContent.m_strABDOMEN = dtbValue.Rows[i]["ABDOMEN"].ToString();
                        p_objContent.m_dtmUMBILICALCORDLEFTTIME = dtbValue.Rows[i]["UMBILICALCORDLEFTTIME"].ToString();
                        p_objContent.m_strBUTTOCKS = dtbValue.Rows[i]["BUTTOCKS"].ToString();
                        p_objContent.m_strLIMB = dtbValue.Rows[i]["LIMB"].ToString();
                        p_objContent.m_strNORMALCIRCS = dtbValue.Rows[i]["NORMALCIRCS"].ToString();
                        p_objContent.m_strLACTATION = dtbValue.Rows[i]["LACTATION"].ToString();
                        p_objContent.m_strBCGVACCINE = dtbValue.Rows[i]["BCGVACCINE"].ToString();
                        p_objContent.m_strBLIVERBACTERIN = dtbValue.Rows[i]["BLIVERBACTERIN"].ToString();
                        p_objContent.m_strOTHERCHECK = dtbValue.Rows[i]["OTHERCHECK"].ToString();
                        p_objContent.m_strOTHERCHECKXML = dtbValue.Rows[i]["OTHERCHECKXML"].ToString();
                        p_objContent.m_strOUTHOSPITALADVICE = dtbValue.Rows[i]["OUTHOSPITALADVICE"].ToString();
                        p_objContent.m_strOUTHOSPITALADVICEXML = dtbValue.Rows[i]["OUTHOSPITALADVICEXML"].ToString();
                        p_objContent.m_strDEALWITH = dtbValue.Rows[i]["DEALWITH"].ToString();
                        p_objContent.m_strDEALWITHXML = dtbValue.Rows[i]["DEALWITHXML"].ToString();
                        p_objContent.m_strINROOMCHECKDOCID = dtbValue.Rows[i]["INROOMCHECKDOCID"].ToString();
                        p_objContent.m_strRECORDSIGNDOCID = dtbValue.Rows[i]["RECORDSIGNDOCID"].ToString();
                        p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        p_objContent.m_strINROOMCHECKDOCName = dtbValue.Rows[i]["INROOMCHECKDOCNAME"].ToString();
                        p_objContent.m_strRECORDSIGNDOCName = dtbValue.Rows[i]["RECORDSIGNDOCNAME"].ToString();
                        p_objContent.m_strCHECKEDCHANGE = dtbValue.Rows[i]["CHECKEDCHANGE"].ToString();
                        if (dtbValue.Rows[i]["OUTHOSPITALDATE"] != DBNull.Value)
                        {
                            p_objContent.m_dtOUTHOSPITALDATE = Convert.ToDateTime(dtbValue.Rows[i]["OUTHOSPITALDATE"]);
                        }
                        else
                        {
                            p_objContent.m_dtOUTHOSPITALDATE = DateTime.Parse("1900-01-01 00:00:00");

                        }
                        if (dtbValue.Rows[i]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out p_objContent.objSignerArr);

                            //释放
                            objSign = null;
                        }
                        #endregion
                    }
                    p_objRecordContent = p_objContent;
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

            }           //返回
            return lngRes;

        }

        /// <summary>
        /// 查看是否有相同的记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objPreModifyInfo)
        {
            p_objPreModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objPreModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_objPreModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString());
                        p_objPreModifyInfo.m_strActionUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
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

        /// <summary>
        /// 查看是否最新记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            string c_strCheckLastModifyRecordSQL = null;
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                c_strCheckLastModifyRecordSQL = "select top 1 modifydate,modifyuserid from newbabyinroomrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=0 order by modifydate desc";
            }
            else
            {
                c_strCheckLastModifyRecordSQL = @"select modifydate, modifyuserid
  from (select modifydate, modifyuserid
          from newbabyinroomrecord
         where inpatientid = ?
           and inpatientdate = ?
           and opendate = ?
           and status = 0
         order by modifydate desc)
 where rownum = 1";
            }


            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果,
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["ModifyDate"].ToString());
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[i]["ModifyUserID"].ToString();
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

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <param name="p_strDiseaseID"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;

            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
            clsNewBabyInRoomRecord m_objContent = (clsNewBabyInRoomRecord)p_objRecordContent;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(71, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = m_objContent.m_strInPatientID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr[2].DbType = DbType.DateTime;
                objLisAddItemRefArr[2].Value = m_objContent.m_dtmOpenDate;
                objLisAddItemRefArr[3].DbType = DbType.DateTime;
                objLisAddItemRefArr[3].Value = m_objContent.m_dtmCreateDate;
                objLisAddItemRefArr[4].Value = m_objContent.m_strCreateUserID;
                objLisAddItemRefArr[5].Value = 0;
                objLisAddItemRefArr[6].DbType = DbType.DateTime;
                objLisAddItemRefArr[6].Value = m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr[7].Value = m_objContent.m_strModifyUserID;
                objLisAddItemRefArr[8].Value = m_objContent.m_strBABYSEX;
                objLisAddItemRefArr[9].DbType = DbType.DateTime;
                objLisAddItemRefArr[9].Value = m_objContent.m_dtmBIRTHTIME;
                objLisAddItemRefArr[10].Value = m_objContent.m_strCHILDBEARING;
                objLisAddItemRefArr[11].Value = m_objContent.m_strPREGNANTTIME;
                objLisAddItemRefArr[12].Value = m_objContent.m_strREACTION;
                objLisAddItemRefArr[13].Value = m_objContent.m_strMUSCLESTRAIN;
                objLisAddItemRefArr[14].Value = m_objContent.m_strCRYVOICE;
                objLisAddItemRefArr[15].Value = m_objContent.m_strDROPSY;
                objLisAddItemRefArr[16].Value = m_objContent.m_strSKINCOLOR;
                objLisAddItemRefArr[17].Value = m_objContent.m_strELASTICITY;
                objLisAddItemRefArr[18].Value = m_objContent.m_strICTERUS;
                objLisAddItemRefArr[19].Value = m_objContent.m_strPIGMENT;
                objLisAddItemRefArr[20].Value = m_objContent.m_strPETECHIA;
                objLisAddItemRefArr[21].Value = m_objContent.m_strBIRTHBURL;
                objLisAddItemRefArr[22].Value = m_objContent.m_strHAEMATOMA;
                objLisAddItemRefArr[23].Value = m_objContent.m_strSKULLSOFT;
                objLisAddItemRefArr[24].Value = m_objContent.m_strBONESEW;
                objLisAddItemRefArr[25].Value = m_objContent.m_strFONTANEL;
                objLisAddItemRefArr[26].Value = m_objContent.m_strHEADROUND;
                objLisAddItemRefArr[27].Value = m_objContent.m_strFACIALFEATURES;
                objLisAddItemRefArr[28].Value = m_objContent.m_strMOUTH;
                objLisAddItemRefArr[29].Value = m_objContent.m_strHEART;
                objLisAddItemRefArr[30].Value = m_objContent.m_strLUNG;
                objLisAddItemRefArr[31].Value = m_objContent.m_strCHEST;
                objLisAddItemRefArr[32].Value = m_objContent.m_strVEIN;
                objLisAddItemRefArr[33].Value = m_objContent.m_strLIVER;
                objLisAddItemRefArr[34].Value = m_objContent.m_strSPLEEN;
                objLisAddItemRefArr[35].Value = m_objContent.m_strHILUM;
                objLisAddItemRefArr[36].Value = m_objContent.m_strACTIVITY;
                objLisAddItemRefArr[37].Value = m_objContent.m_strARTHROSIS;
                objLisAddItemRefArr[38].Value = m_objContent.m_strABNORMALITY;
                objLisAddItemRefArr[39].Value = m_objContent.m_strGENITALIA;
                objLisAddItemRefArr[40].Value = m_objContent.m_strOTHERRECORD;
                objLisAddItemRefArr[41].Value = m_objContent.m_strOTHERRECORDXML;
                objLisAddItemRefArr[42].DbType = DbType.DateTime;
                objLisAddItemRefArr[42].Value = m_objContent.m_dtmCHECKDATE;
                objLisAddItemRefArr[43].Value = m_objContent.m_strINHOSPITALDAYS;
                objLisAddItemRefArr[44].Value = m_objContent.m_strWEIGHT;
                objLisAddItemRefArr[45].Value = m_objContent.m_strHEAD;
                objLisAddItemRefArr[46].Value = m_objContent.m_strSKIN;
                objLisAddItemRefArr[47].Value = m_objContent.m_strHEART_OUTHOSPITAL;
                objLisAddItemRefArr[48].Value = m_objContent.m_strLUNG_OUTHOSPITAL;
                objLisAddItemRefArr[49].Value = m_objContent.m_strGENITALIA_OUTHOSPITAL;
                objLisAddItemRefArr[50].Value = m_objContent.m_strABDOMEN;
                objLisAddItemRefArr[51].Value = m_objContent.m_dtmUMBILICALCORDLEFTTIME;
                objLisAddItemRefArr[52].Value = m_objContent.m_strBUTTOCKS;
                objLisAddItemRefArr[53].Value = m_objContent.m_strLIMB;
                objLisAddItemRefArr[54].Value = m_objContent.m_strNORMALCIRCS;
                objLisAddItemRefArr[55].Value = m_objContent.m_strLACTATION;
                objLisAddItemRefArr[56].Value = m_objContent.m_strBCGVACCINE;
                objLisAddItemRefArr[57].Value = m_objContent.m_strBLIVERBACTERIN;
                objLisAddItemRefArr[58].Value = m_objContent.m_strOTHERCHECK;
                objLisAddItemRefArr[59].Value = m_objContent.m_strOTHERCHECKXML;
                objLisAddItemRefArr[60].Value = m_objContent.m_strOUTHOSPITALADVICE;
                objLisAddItemRefArr[61].Value = m_objContent.m_strOUTHOSPITALADVICEXML;
                objLisAddItemRefArr[62].Value = m_objContent.m_strDEALWITH;
                objLisAddItemRefArr[63].Value = m_objContent.m_strDEALWITHXML;
                objLisAddItemRefArr[64].Value = m_objContent.m_strINROOMCHECKDOCID;
                objLisAddItemRefArr[65].Value = m_objContent.m_strRECORDSIGNDOCID;
                objLisAddItemRefArr[66].Value = m_objContent.m_strINROOMCHECKDOCName;
                objLisAddItemRefArr[67].Value = m_objContent.m_strRECORDSIGNDOCName;
                objLisAddItemRefArr[68].Value = lngSequence;
                objLisAddItemRefArr[69].Value = m_objContent.m_strCHECKEDCHANGE;
                objLisAddItemRefArr[70].DbType = DbType.DateTime;
                objLisAddItemRefArr[70].Value = m_objContent.m_dtOUTHOSPITALDATE;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngRecEff, objLisAddItemRefArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(m_objContent.objSignerArr, lngSequence);
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
        /// 修改内容
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <param name="p_strDiseaseID"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;

            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
            clsNewBabyInRoomRecord m_objContent = (clsNewBabyInRoomRecord)p_objRecordContent;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(68, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr[1].Value = m_objContent.m_strModifyUserID;
                objLisAddItemRefArr[2].Value = m_objContent.m_strBABYSEX;
                objLisAddItemRefArr[3].DbType = DbType.DateTime;
                objLisAddItemRefArr[3].Value = m_objContent.m_dtmBIRTHTIME;
                objLisAddItemRefArr[4].Value = m_objContent.m_strCHILDBEARING;
                objLisAddItemRefArr[5].Value = m_objContent.m_strPREGNANTTIME;
                objLisAddItemRefArr[6].Value = m_objContent.m_strREACTION;
                objLisAddItemRefArr[7].Value = m_objContent.m_strMUSCLESTRAIN;
                objLisAddItemRefArr[8].Value = m_objContent.m_strCRYVOICE;
                objLisAddItemRefArr[9].Value = m_objContent.m_strDROPSY;
                objLisAddItemRefArr[10].Value = m_objContent.m_strSKINCOLOR;
                objLisAddItemRefArr[11].Value = m_objContent.m_strELASTICITY;
                objLisAddItemRefArr[12].Value = m_objContent.m_strICTERUS;
                objLisAddItemRefArr[13].Value = m_objContent.m_strPIGMENT;
                objLisAddItemRefArr[14].Value = m_objContent.m_strPETECHIA;
                objLisAddItemRefArr[15].Value = m_objContent.m_strBIRTHBURL;
                objLisAddItemRefArr[16].Value = m_objContent.m_strHAEMATOMA;
                objLisAddItemRefArr[17].Value = m_objContent.m_strSKULLSOFT;
                objLisAddItemRefArr[18].Value = m_objContent.m_strBONESEW;
                objLisAddItemRefArr[19].Value = m_objContent.m_strFONTANEL;
                objLisAddItemRefArr[20].Value = m_objContent.m_strHEADROUND;
                objLisAddItemRefArr[21].Value = m_objContent.m_strFACIALFEATURES;
                objLisAddItemRefArr[22].Value = m_objContent.m_strMOUTH;
                objLisAddItemRefArr[23].Value = m_objContent.m_strHEART;
                objLisAddItemRefArr[24].Value = m_objContent.m_strLUNG;
                objLisAddItemRefArr[25].Value = m_objContent.m_strCHEST;
                objLisAddItemRefArr[26].Value = m_objContent.m_strVEIN;
                objLisAddItemRefArr[27].Value = m_objContent.m_strLIVER;
                objLisAddItemRefArr[28].Value = m_objContent.m_strSPLEEN;
                objLisAddItemRefArr[29].Value = m_objContent.m_strHILUM;
                objLisAddItemRefArr[30].Value = m_objContent.m_strACTIVITY;
                objLisAddItemRefArr[31].Value = m_objContent.m_strARTHROSIS;
                objLisAddItemRefArr[32].Value = m_objContent.m_strABNORMALITY;
                objLisAddItemRefArr[33].Value = m_objContent.m_strGENITALIA;
                objLisAddItemRefArr[34].Value = m_objContent.m_strOTHERRECORD;
                objLisAddItemRefArr[35].Value = m_objContent.m_strOTHERRECORDXML;
                objLisAddItemRefArr[36].DbType = DbType.DateTime;
                objLisAddItemRefArr[36].Value = m_objContent.m_dtmCHECKDATE;
                objLisAddItemRefArr[37].Value = m_objContent.m_strINHOSPITALDAYS;
                objLisAddItemRefArr[38].Value = m_objContent.m_strWEIGHT;
                objLisAddItemRefArr[39].Value = m_objContent.m_strHEAD;
                objLisAddItemRefArr[40].Value = m_objContent.m_strSKIN;
                objLisAddItemRefArr[41].Value = m_objContent.m_strHEART_OUTHOSPITAL;
                objLisAddItemRefArr[42].Value = m_objContent.m_strLUNG_OUTHOSPITAL;
                objLisAddItemRefArr[43].Value = m_objContent.m_strGENITALIA_OUTHOSPITAL;
                objLisAddItemRefArr[44].Value = m_objContent.m_strABDOMEN;
                objLisAddItemRefArr[45].Value = m_objContent.m_dtmUMBILICALCORDLEFTTIME;
                objLisAddItemRefArr[46].Value = m_objContent.m_strBUTTOCKS;
                objLisAddItemRefArr[47].Value = m_objContent.m_strLIMB;
                objLisAddItemRefArr[48].Value = m_objContent.m_strNORMALCIRCS;
                objLisAddItemRefArr[49].Value = m_objContent.m_strLACTATION;
                objLisAddItemRefArr[50].Value = m_objContent.m_strBCGVACCINE;
                objLisAddItemRefArr[51].Value = m_objContent.m_strBLIVERBACTERIN;
                objLisAddItemRefArr[52].Value = m_objContent.m_strOTHERCHECK;
                objLisAddItemRefArr[53].Value = m_objContent.m_strOTHERCHECKXML;
                objLisAddItemRefArr[54].Value = m_objContent.m_strOUTHOSPITALADVICE;
                objLisAddItemRefArr[55].Value = m_objContent.m_strOUTHOSPITALADVICEXML;
                objLisAddItemRefArr[56].Value = m_objContent.m_strDEALWITH;
                objLisAddItemRefArr[57].Value = m_objContent.m_strDEALWITHXML;
                objLisAddItemRefArr[58].Value = m_objContent.m_strINROOMCHECKDOCID;
                objLisAddItemRefArr[59].Value = m_objContent.m_strRECORDSIGNDOCID;
                objLisAddItemRefArr[60].Value = m_objContent.m_strINROOMCHECKDOCName;
                objLisAddItemRefArr[61].Value = m_objContent.m_strRECORDSIGNDOCName;
                objLisAddItemRefArr[62].Value = lngSequence;
                objLisAddItemRefArr[63].Value = m_objContent.m_strCHECKEDCHANGE;
                objLisAddItemRefArr[64].DbType = DbType.DateTime;
                objLisAddItemRefArr[64].Value = m_objContent.m_dtOUTHOSPITALDATE;
                objLisAddItemRefArr[65].Value = m_objContent.m_strInPatientID;
                objLisAddItemRefArr[66].DbType = DbType.DateTime;
                objLisAddItemRefArr[66].Value = m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr[67].DbType = DbType.DateTime;
                objLisAddItemRefArr[67].Value = m_objContent.m_dtmOpenDate;

                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objLisAddItemRefArr);
                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(m_objContent.objSignerArr, lngSequence);
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
        /// 删除记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;


            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
                if (lngEff < 0) return -1;
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
        /// 获取首次打印时间及修改时间
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_dtmModifyDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate
            (string p_strInPatientID,
            string p_strInPatientDate, clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.MinValue;
            p_strFirstPrintDate = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //			objDPArr[2].Value=DateTime.Parse(p_strOpenRecordTime);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count >= 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
                //返回
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }   //返回
            return lngRes;
        }


        /// <summary>
        /// 获取指定的已删除记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenRecordTime"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenRecordTime,
            clsHRPTableService p_objHRPServ,
            out clsBaseCaseHistoryInfo p_objRecordContent)
        {
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);


                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenRecordTime);

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeletedRecordContentSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsNewBabyInRoomRecord p_objContent = new clsNewBabyInRoomRecord();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        #region 获取结果
                        p_objContent.m_strInPatientID = p_strInPatientID;
                        p_objContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                        p_objContent.m_dtmDeActivedDate = Convert.ToDateTime(dtbValue.Rows[i]["DEACTIVEDDATE"]);
                        p_objContent.m_strDeActivedOperatorID = dtbValue.Rows[i]["DEACTIVEDOPERATORID"].ToString();
                        p_objContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        p_objContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();

                        if (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            p_objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else p_objContent.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[i]["FIRSTPRINTDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            p_objContent.m_bytStatus = 0;
                        else p_objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        p_objContent.m_strBABYSEX = dtbValue.Rows[i]["BABYSEX"].ToString();
                        p_objContent.m_dtmBIRTHTIME = Convert.ToDateTime(dtbValue.Rows[i]["BIRTHTIME"]);
                        p_objContent.m_strCHILDBEARING = dtbValue.Rows[i]["CHILDBEARING"].ToString();
                        p_objContent.m_strPREGNANTTIME = dtbValue.Rows[i]["PREGNANTTIME"].ToString();
                        p_objContent.m_strREACTION = dtbValue.Rows[i]["REACTION"].ToString();
                        p_objContent.m_strMUSCLESTRAIN = dtbValue.Rows[i]["MUSCLESTRAIN"].ToString();
                        p_objContent.m_strCRYVOICE = dtbValue.Rows[i]["CRYVOICE"].ToString();
                        p_objContent.m_strDROPSY = dtbValue.Rows[i]["DROPSY"].ToString();
                        p_objContent.m_strSKINCOLOR = dtbValue.Rows[i]["SKINCOLOR"].ToString();
                        p_objContent.m_strELASTICITY = dtbValue.Rows[i]["ELASTICITY"].ToString();
                        p_objContent.m_strICTERUS = dtbValue.Rows[i]["ICTERUS"].ToString();
                        p_objContent.m_strPIGMENT = dtbValue.Rows[i]["PIGMENT"].ToString();
                        p_objContent.m_strPETECHIA = dtbValue.Rows[i]["PETECHIA"].ToString();
                        p_objContent.m_strBIRTHBURL = dtbValue.Rows[i]["BIRTHBURL"].ToString();
                        p_objContent.m_strHAEMATOMA = dtbValue.Rows[i]["HAEMATOMA"].ToString();
                        p_objContent.m_strSKULLSOFT = dtbValue.Rows[i]["SKULLSOFT"].ToString();
                        p_objContent.m_strBONESEW = dtbValue.Rows[i]["BONESEW"].ToString();
                        p_objContent.m_strFONTANEL = dtbValue.Rows[i]["FONTANEL"].ToString();
                        p_objContent.m_strHEADROUND = dtbValue.Rows[i]["HEADROUND"].ToString();
                        p_objContent.m_strFACIALFEATURES = dtbValue.Rows[i]["FACIALFEATURES"].ToString();
                        p_objContent.m_strMOUTH = dtbValue.Rows[i]["MOUTH"].ToString();
                        p_objContent.m_strHEART = dtbValue.Rows[i]["HEART"].ToString();
                        p_objContent.m_strLUNG = dtbValue.Rows[i]["LUNG"].ToString();
                        p_objContent.m_strCHEST = dtbValue.Rows[i]["CHEST"].ToString();
                        p_objContent.m_strVEIN = dtbValue.Rows[i]["VEIN"].ToString();
                        p_objContent.m_strLIVER = dtbValue.Rows[i]["LIVER"].ToString();
                        p_objContent.m_strSPLEEN = dtbValue.Rows[i]["SPLEEN"].ToString();
                        p_objContent.m_strHILUM = dtbValue.Rows[i]["HILUM"].ToString();
                        p_objContent.m_strACTIVITY = dtbValue.Rows[i]["ACTIVITY"].ToString();
                        p_objContent.m_strARTHROSIS = dtbValue.Rows[i]["ARTHROSIS"].ToString();
                        p_objContent.m_strABNORMALITY = dtbValue.Rows[i]["ABNORMALITY"].ToString();
                        p_objContent.m_strGENITALIA = dtbValue.Rows[i]["GENITALIA"].ToString();
                        p_objContent.m_strOTHERRECORD = dtbValue.Rows[i]["OTHERRECORD"].ToString();
                        p_objContent.m_strOTHERRECORDXML = dtbValue.Rows[i]["OTHERRECORDXML"].ToString();
                        p_objContent.m_dtmCHECKDATE = Convert.ToDateTime(dtbValue.Rows[i]["CHECKDATE"]);
                        p_objContent.m_strINHOSPITALDAYS = dtbValue.Rows[i]["INHOSPITALDAYS"].ToString();
                        p_objContent.m_strWEIGHT = dtbValue.Rows[i]["WEIGHT"].ToString();
                        p_objContent.m_strHEAD = dtbValue.Rows[i]["HEAD"].ToString();
                        p_objContent.m_strSKIN = dtbValue.Rows[i]["SKIN"].ToString();
                        p_objContent.m_strHEART_OUTHOSPITAL = dtbValue.Rows[i]["HEART_OUTHOSPITAL"].ToString();
                        p_objContent.m_strLUNG_OUTHOSPITAL = dtbValue.Rows[i]["LUNG_OUTHOSPITAL"].ToString();
                        p_objContent.m_strGENITALIA_OUTHOSPITAL = dtbValue.Rows[i]["GENITALIA_OUTHOSPITAL"].ToString();
                        p_objContent.m_strABDOMEN = dtbValue.Rows[i]["ABDOMEN"].ToString();
                        p_objContent.m_dtmUMBILICALCORDLEFTTIME = dtbValue.Rows[i]["UMBILICALCORDLEFTTIME"].ToString();
                        p_objContent.m_strBUTTOCKS = dtbValue.Rows[i]["BUTTOCKS"].ToString();
                        p_objContent.m_strLIMB = dtbValue.Rows[i]["LIMB"].ToString();
                        p_objContent.m_strNORMALCIRCS = dtbValue.Rows[i]["NORMALCIRCS"].ToString();
                        p_objContent.m_strLACTATION = dtbValue.Rows[i]["LACTATION"].ToString();
                        p_objContent.m_strBCGVACCINE = dtbValue.Rows[i]["BCGVACCINE"].ToString();
                        p_objContent.m_strBLIVERBACTERIN = dtbValue.Rows[i]["BLIVERBACTERIN"].ToString();
                        p_objContent.m_strOTHERCHECK = dtbValue.Rows[i]["OTHERCHECK"].ToString();
                        p_objContent.m_strOTHERCHECKXML = dtbValue.Rows[i]["OTHERCHECKXML"].ToString();
                        p_objContent.m_strOUTHOSPITALADVICE = dtbValue.Rows[i]["OUTHOSPITALADVICE"].ToString();
                        p_objContent.m_strOUTHOSPITALADVICEXML = dtbValue.Rows[i]["OUTHOSPITALADVICEXML"].ToString();
                        p_objContent.m_strDEALWITH = dtbValue.Rows[i]["DEALWITH"].ToString();
                        p_objContent.m_strDEALWITHXML = dtbValue.Rows[i]["DEALWITHXML"].ToString();
                        p_objContent.m_strINROOMCHECKDOCID = dtbValue.Rows[i]["INROOMCHECKDOCID"].ToString();
                        p_objContent.m_strRECORDSIGNDOCID = dtbValue.Rows[i]["RECORDSIGNDOCID"].ToString();
                        p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        p_objContent.m_strINROOMCHECKDOCName = dtbValue.Rows[i]["INROOMCHECKDOCNAME"].ToString();
                        p_objContent.m_strRECORDSIGNDOCName = dtbValue.Rows[i]["RECORDSIGNDOCNAME"].ToString();
                        p_objContent.m_strCHECKEDCHANGE = dtbValue.Rows[i]["CHECKEDCHANGE"].ToString().Trim();
                        p_objContent.m_dtOUTHOSPITALDATE = Convert.ToDateTime(dtbValue.Rows[i]["OUTHOSPITALDATE"]);
                        //获取签名集合
                        if (dtbValue.Rows[i]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out p_objContent.objSignerArr);

                            //释放
                            objSign = null;
                        }
                        #endregion
                    }

                    p_objRecordContent = p_objContent;
                }

                //返回
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }       //返回
            return lngRes;

        }

    }

}
