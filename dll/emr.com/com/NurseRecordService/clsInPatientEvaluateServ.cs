using System;
using System.EnterpriseServices;
//using com.digitalwave.HRPService;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.InPatientEvaluateServ
{
	/// <summary>
	/// 入院病人评估
	///当参数中含有具体OpenDate时，说明此OpenDate的状态Status=0，故在SQL条件中若含有OpenDate可以不用考虑Status=0
	/// 否则，要考虑Status=0的情况
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsInPatientEvaluateServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsInPatientEvaluateServ()
		{}

		[AutoComplete]
		public long m_lngGetFirstPrintDate(
			string p_strInPatientID,string p_strInPatientDate,/*string p_strOpenDate,*/out string p_strFirstPrintDate)
		{		
			p_strFirstPrintDate="";

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientEvaluateServ","m_lngGetFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			long lngRes = 0;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//查询第一次打印时间				
				string strCommand = "select firstprintdate  from inpatientevaluate where status=0 and inpatientid=? and inpatientdate=?";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);

				System.Data.DataTable  dtbResult=new System.Data.DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref dtbResult, objDPArr);
				p_strFirstPrintDate="";
				if(lngRes>0 && dtbResult.Rows.Count>0)
					p_strFirstPrintDate=dtbResult.Rows[0][0].ToString();
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
                finally
                {

                    //objHRPServ.Dispose();
                }
			return lngRes;
		}

		[AutoComplete]
		public long m_lngUpdateFirstPrintDate(
			string p_strInPatientID,string p_strInPatientDate/*string p_strOpenDate,*/)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientEvaluateServ","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			//更新第一次打印时间				
			string strCommand = "update inpatientevaluate set firstprintdate=?  where firstprintdate is null and status=0 and inpatientid=? and inpatientdate=? ";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[1].Value = p_strInPatientID;
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngEff = -1;
            long lngRes = objHRPServ.lngExecuteParameterSQL(strCommand,ref lngEff,objDPArr);	
            //objHRPServ.Dispose();
            return lngRes;		
		}

		/// <summary>
		/// 获得主表中记录的住院时间（而非创建时间）
		/// </summary>
		[AutoComplete]		
		public long m_lngGetAllRecordDateArr(
			string p_strInPatientID,/*string p_strInPatientDate,*/out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO基本信息外，只要查出Status=0的记录
			p_strXML="";
			p_intRows=0;	
	
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientEvaluateServ","m_lngGetAllRecordDateArr");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       education,
       religion,
       religioncontent,
       datafrom,
       datafromothercontent,
       inpatientdiagnosexml,
       inpatientmode_tablexml,
       allergichistory_tablexml,
       allergicsourcexml,
       allergicsymptomxml,
       appetite_tablexml,
       weight_tablexml,
       howmuchweightxml,
       mouth_tablexml,
       chaw_tablexml,
       deglutition_tablexml,
       stomach_tablexml,
       stomachcolorxml,
       stomachcharacterxml,
       stomachtimesxml,
       stomachqtyxml,
       skin_tablexml,
       bodyxml,
       skinothercontentxml,
       chogh_tablexml,
       phlegm_tablexml,
       phlegmcolorxml,
       dejecta_tablexml,
       dejectatimesinonedayxml,
       dejectahowmanydaysoncexml,
       dejectacharacterxml,
       pee_tablexml,
       peeothercontentxml,
       canmyself_tablexml,
       breathurge_tablexml,
       limbactive_tablexml,
       paralysispartxml,
       sleep_tablexml,
       sleephoursxml,
       sleepothercontentxml,
       assistantsleep_tablexml,
       assistantsleepmedicinesxml,
       assistantsleepmodelxml,
       shout_tablexml,
       answer_tablexml,
       seeingbalk_tablexml,
       lefteyeothercontentxml,
       righteyeothercontentxml,
       botheyeothercontentxml,
       listenbalk_tablexml,
       leftlistenothercontentxml,
       rightlistenothercontentxml,
       bothlistenothercontentxml,
       ache_tablexml,
       achepartxml,
       language_tablexml,
       alwayslanguagexml,
       inhospitalfeel_tablexml,
       lookin_tablexml,
       economic_tablexml,
       illnessunderstand_tablexml,
       doctorsadvice_tablexml,
       doctorsadvicecontentxml,
       smoking_tablexml,
       smokingyearsxml,
       smokingqtyonedayxml,
       drink_tablexml,
       drinkyearsxml,
       drinkqtyonedayxml,
       freakout_tablexml,
       freakoutmedicinesxml,
       freakoutyearsxml,
       freakoutqtyonedayxml,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       status,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate
  from inpatientevaluate
 where status = 0
   and inpatientid = ?
 order by inpatientdate";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 查出时间对应的主表信息，p_strInPatientDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>	
		[AutoComplete]	
		private long m_lngGetLatestRecord(string p_strInPatientID,string p_strInPatientDate/*,string p_strOpenDate*/,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出OpenDate为指定时间的记录
			p_strXML="";
			p_intRows=0;	
			string strCommand = @"Select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       education,
       religion,
       religioncontent,
       datafrom,
       datafromothercontent,
       inpatientdiagnosexml,
       inpatientmode_tablexml,
       allergichistory_tablexml,
       allergicsourcexml,
       allergicsymptomxml,
       appetite_tablexml,
       weight_tablexml,
       howmuchweightxml,
       mouth_tablexml,
       chaw_tablexml,
       deglutition_tablexml,
       stomach_tablexml,
       stomachcolorxml,
       stomachcharacterxml,
       stomachtimesxml,
       stomachqtyxml,
       skin_tablexml,
       bodyxml,
       skinothercontentxml,
       chogh_tablexml,
       phlegm_tablexml,
       phlegmcolorxml,
       dejecta_tablexml,
       dejectatimesinonedayxml,
       dejectahowmanydaysoncexml,
       dejectacharacterxml,
       pee_tablexml,
       peeothercontentxml,
       canmyself_tablexml,
       breathurge_tablexml,
       limbactive_tablexml,
       paralysispartxml,
       sleep_tablexml,
       sleephoursxml,
       sleepothercontentxml,
       assistantsleep_tablexml,
       assistantsleepmedicinesxml,
       assistantsleepmodelxml,
       shout_tablexml,
       answer_tablexml,
       seeingbalk_tablexml,
       lefteyeothercontentxml,
       righteyeothercontentxml,
       botheyeothercontentxml,
       listenbalk_tablexml,
       leftlistenothercontentxml,
       rightlistenothercontentxml,
       bothlistenothercontentxml,
       ache_tablexml,
       achepartxml,
       language_tablexml,
       alwayslanguagexml,
       inhospitalfeel_tablexml,
       lookin_tablexml,
       economic_tablexml,
       illnessunderstand_tablexml,
       doctorsadvice_tablexml,
       doctorsadvicecontentxml,
       smoking_tablexml,
       smokingyearsxml,
       smokingqtyonedayxml,
       drink_tablexml,
       drinkyearsxml,
       drinkqtyonedayxml,
       freakout_tablexml,
       freakoutmedicinesxml,
       freakoutyearsxml,
       freakoutqtyonedayxml,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       status,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate
  from InPatientEvaluate
 where InPatientID = ?
   and InPatientDate = ?
   and Status = 0";//and OpenDate="+clsHRPTableService.s_strOracleDateTime(p_strOpenDate)+"";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 查出时间对应的从表信息,p_strInPatientDate和p_strOpenDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>	
		[AutoComplete]			
		private long m_lngGetLatestRecordContent(string p_strInPatientID,string p_strInPatientDate/*,string p_strOpenDate*/,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出OpenDate为指定时间的记录中,ModifyDate为最大修改时间的记录
			p_strXML="";
			p_intRows=0;	//无Status字段
            string strCommand = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.modifydate,
       a.modifyuserid,
       a.inpatientdiagnose,
       a.inpatientmode,
       a.allergichistory,
       a.allergicsource,
       a.allergicsymptom,
       a.appetite,
       a.weight,
       a.howmuchweight,
       a.mouth,
       a.chaw,
       a.deglutition,
       a.stomachnothing,
       a.stomachrise,
       a.stomachache,
       a.stomachnaupathia,
       a.stomachspew,
       a.stomachcolor,
       a.stomachcharacter,
       a.stomachtimes,
       a.stomachqty,
       a.skinfull,
       a.skinpallor,
       a.skinicterus,
       a.skinred,
       a.skincyanopathy,
       a.skindehydrate,
       a.skinanthema,
       a.skinblood,
       a.skinsore,
       a.skincut,
       a.skindropsy,
       a.body,
       a.upperlimbs,
       a.lowerlimbs,
       a.skinother,
       a.skinothercontent,
       a.chogh,
       a.havephlegm,
       a.phlegmeasy,
       a.phlegmchroma,
       a.phlegmcolor,
       a.dejectatimesinoneday,
       a.dejectahowmanydaysonce,
       a.dejectairretention,
       a.dejectafistula,
       a.dejectacharacter,
       a.peeirretention,
       a.peenatural,
       a.peeuraemia,
       a.peemuch,
       a.peeblood,
       a.peecyst,
       a.peepipe,
       a.peeother,
       a.peeothercontent,
       a.canmyself,
       a.breathurge,
       a.limbactive,
       a.paralysis,
       a.paralysispart,
       a.sleephours,
       a.sleepnothing,
       a.sleepdifficulty,
       a.sleepwakeeasy,
       a.sleepwakeearly,
       a.sleepdreammuch,
       a.sleepother,
       a.sleepothercontent,
       a.assistantsleep,
       a.assistantsleepmedicines,
       a.assistantsleepmodel,
       a.shout,
       a.answer,
       a.seeingbalk,
       a.lefteyedown,
       a.lefteyeblur,
       a.lefteyeagain,
       a.lefteyeother,
       a.lefteyeothercontent,
       a.righteyedown,
       a.righteyeblur,
       a.righteyeagain,
       a.righteyeother,
       a.righteyeothercontent,
       a.botheyedown,
       a.botheyeblur,
       a.botheyeagain,
       a.botheyeother,
       a.botheyeothercontent,
       a.listenbalk,
       a.leftlistendown,
       a.leftlistentinnitus,
       a.leftlistenagain,
       a.leftlistenother,
       a.leftlistenothercontent,
       a.rightlistendown,
       a.rightlistentinnitus,
       a.rightlistenagain,
       a.rightlistenother,
       a.rightlistenothercontent,
       a.bothlistendown,
       a.bothlistentinnitus,
       a.bothlistenagain,
       a.bothlistenother,
       a.bothlistenothercontent,
       a.ache,
       a.achepart,
       a.achetimes,
       a.alwayslanguage,
       a.mandarin,
       a.cantsay,
       a.inhospitalfeel,
       a.lookin,
       a.economic,
       a.illnessunderstand,
       a.doctorsadvice,
       a.doctorsadvicecontent,
       a.ifsmoking,
       a.smokingyears,
       a.smokingqtyoneday,
       a.ifdrink,
       a.drinkyears,
       a.drinkqtyoneday,
       a.freakout,
       a.freakoutmedicines,
       a.freakoutyears,
       a.freakoutqtyoneday,
       a.chargenurseid,
       a.abonrmaldesc,
       a.checktime
  from inpatientevaluatecontent a
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.modifydate =
       (select max(modifydate)
          from inpatientevaluatecontent
         where inpatientid = a.inpatientid
           and inpatientdate = a.inpatientdate)";
		
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]
		public long m_lngGetLatestRecord_All(
			string p_strInPatientID,string p_strInPatientDate,out string p_strXML,out int p_intRows,out string p_strContentXML,out int p_intContentRows)
		{
			p_strContentXML=p_strXML="";
			p_intContentRows=p_intRows=0;
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientEvaluateServ","m_lngGetLatestRecord_All");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			long lngRes=m_lngGetLatestRecord(p_strInPatientID,p_strInPatientDate,out p_strXML,out p_intRows);
			if(lngRes>0 && p_intRows>0)//因为主表需要能查询出没有被删除的记录，子表才能继续查询
				lngRes=m_lngGetLatestRecordContent(p_strInPatientID,p_strInPatientDate,out p_strContentXML,out p_intContentRows);
			return lngRes;
		}

		[AutoComplete]
		public long m_lngGetLatestDeleteRecord_All(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows,out string p_strContentXML,out int p_intContentRows)
		{
			p_strContentXML=p_strXML="";
			p_intContentRows=p_intRows=0;	
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientEvaluateServ","m_lngGetLatestDeleteRecord_All");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			long lngRes=m_lngGetLatestDeleteRecord(p_strInPatientID,p_strInPatientDate,p_strOpenDate,out p_strXML,out p_intRows);
			if(lngRes>0 && p_intRows>0)//因为主表需要能查询出有被删除的记录，子表才能继续查询
				lngRes=m_lngGetLatestDeleteRecordContent(p_strInPatientID,p_strInPatientDate,p_strOpenDate,out p_strContentXML,out p_intContentRows);
			return lngRes;
		}

		/// <summary>
		/// 查出时间对应的主表信息，p_strInPatientDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>	
		[AutoComplete]	
		private long m_lngGetLatestDeleteRecord(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出OpenDate为指定时间的记录
			p_strXML="";
			p_intRows=0;
            string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       education,
       religion,
       religioncontent,
       datafrom,
       datafromothercontent,
       inpatientdiagnosexml,
       inpatientmode_tablexml,
       allergichistory_tablexml,
       allergicsourcexml,
       allergicsymptomxml,
       appetite_tablexml,
       weight_tablexml,
       howmuchweightxml,
       mouth_tablexml,
       chaw_tablexml,
       deglutition_tablexml,
       stomach_tablexml,
       stomachcolorxml,
       stomachcharacterxml,
       stomachtimesxml,
       stomachqtyxml,
       skin_tablexml,
       bodyxml,
       skinothercontentxml,
       chogh_tablexml,
       phlegm_tablexml,
       phlegmcolorxml,
       dejecta_tablexml,
       dejectatimesinonedayxml,
       dejectahowmanydaysoncexml,
       dejectacharacterxml,
       pee_tablexml,
       peeothercontentxml,
       canmyself_tablexml,
       breathurge_tablexml,
       limbactive_tablexml,
       paralysispartxml,
       sleep_tablexml,
       sleephoursxml,
       sleepothercontentxml,
       assistantsleep_tablexml,
       assistantsleepmedicinesxml,
       assistantsleepmodelxml,
       shout_tablexml,
       answer_tablexml,
       seeingbalk_tablexml,
       lefteyeothercontentxml,
       righteyeothercontentxml,
       botheyeothercontentxml,
       listenbalk_tablexml,
       leftlistenothercontentxml,
       rightlistenothercontentxml,
       bothlistenothercontentxml,
       ache_tablexml,
       achepartxml,
       language_tablexml,
       alwayslanguagexml,
       inhospitalfeel_tablexml,
       lookin_tablexml,
       economic_tablexml,
       illnessunderstand_tablexml,
       doctorsadvice_tablexml,
       doctorsadvicecontentxml,
       smoking_tablexml,
       smokingyearsxml,
       smokingqtyonedayxml,
       drink_tablexml,
       drinkyearsxml,
       drinkqtyonedayxml,
       freakout_tablexml,
       freakoutmedicinesxml,
       freakoutyearsxml,
       freakoutqtyonedayxml,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       status,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate
  from inpatientevaluate
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1";//and OpenDate="+clsHRPTableService.s_strOracleDateTime(p_strOpenDate)+"";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 查出时间对应的从表信息,p_strInPatientDate和p_strOpenDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>
		[AutoComplete]
		private long m_lngGetLatestDeleteRecordContent(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出OpenDate为指定时间的记录中,ModifyDate为最大修改时间的记录
			p_strXML="";
			p_intRows=0;	//无Status字段
            string strCommand = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.modifydate,
       a.modifyuserid,
       a.inpatientdiagnose,
       a.inpatientmode,
       a.allergichistory,
       a.allergicsource,
       a.allergicsymptom,
       a.appetite,
       a.weight,
       a.howmuchweight,
       a.mouth,
       a.chaw,
       a.deglutition,
       a.stomachnothing,
       a.stomachrise,
       a.stomachache,
       a.stomachnaupathia,
       a.stomachspew,
       a.stomachcolor,
       a.stomachcharacter,
       a.stomachtimes,
       a.stomachqty,
       a.skinfull,
       a.skinpallor,
       a.skinicterus,
       a.skinred,
       a.skincyanopathy,
       a.skindehydrate,
       a.skinanthema,
       a.skinblood,
       a.skinsore,
       a.skincut,
       a.skindropsy,
       a.body,
       a.upperlimbs,
       a.lowerlimbs,
       a.skinother,
       a.skinothercontent,
       a.chogh,
       a.havephlegm,
       a.phlegmeasy,
       a.phlegmchroma,
       a.phlegmcolor,
       a.dejectatimesinoneday,
       a.dejectahowmanydaysonce,
       a.dejectairretention,
       a.dejectafistula,
       a.dejectacharacter,
       a.peeirretention,
       a.peenatural,
       a.peeuraemia,
       a.peemuch,
       a.peeblood,
       a.peecyst,
       a.peepipe,
       a.peeother,
       a.peeothercontent,
       a.canmyself,
       a.breathurge,
       a.limbactive,
       a.paralysis,
       a.paralysispart,
       a.sleephours,
       a.sleepnothing,
       a.sleepdifficulty,
       a.sleepwakeeasy,
       a.sleepwakeearly,
       a.sleepdreammuch,
       a.sleepother,
       a.sleepothercontent,
       a.assistantsleep,
       a.assistantsleepmedicines,
       a.assistantsleepmodel,
       a.shout,
       a.answer,
       a.seeingbalk,
       a.lefteyedown,
       a.lefteyeblur,
       a.lefteyeagain,
       a.lefteyeother,
       a.lefteyeothercontent,
       a.righteyedown,
       a.righteyeblur,
       a.righteyeagain,
       a.righteyeother,
       a.righteyeothercontent,
       a.botheyedown,
       a.botheyeblur,
       a.botheyeagain,
       a.botheyeother,
       a.botheyeothercontent,
       a.listenbalk,
       a.leftlistendown,
       a.leftlistentinnitus,
       a.leftlistenagain,
       a.leftlistenother,
       a.leftlistenothercontent,
       a.rightlistendown,
       a.rightlistentinnitus,
       a.rightlistenagain,
       a.rightlistenother,
       a.rightlistenothercontent,
       a.bothlistendown,
       a.bothlistentinnitus,
       a.bothlistenagain,
       a.bothlistenother,
       a.bothlistenothercontent,
       a.ache,
       a.achepart,
       a.achetimes,
       a.alwayslanguage,
       a.mandarin,
       a.cantsay,
       a.inhospitalfeel,
       a.lookin,
       a.economic,
       a.illnessunderstand,
       a.doctorsadvice,
       a.doctorsadvicecontent,
       a.ifsmoking,
       a.smokingyears,
       a.smokingqtyoneday,
       a.ifdrink,
       a.drinkyears,
       a.drinkqtyoneday,
       a.freakout,
       a.freakoutmedicines,
       a.freakoutyears,
       a.freakoutqtyoneday,
       a.chargenurseid,
       a.abonrmaldesc,
       a.checktime
  from inpatientevaluatecontent a
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.modifydate = (select max(modifydate)
                         from inpatientevaluatecontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and a.opendate = a.opendate)";
		
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 单独提取异常信息（用在一般护理记录中）
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strAbnormalInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAbnormalInfo(
			string p_strInPatientID,string p_strInPatientDate,out string p_strAbnormalInfo)
		{
			p_strAbnormalInfo="";
			
//			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientEvaluateServ","m_lngGetAbnormalInfo");
//			//if(lngCheckRes <= 0)
//				//return lngCheckRes;

			string strCommand = @"select b.abonrmaldesc from inpatientevaluate a,inpatientevaluatecontent b  
				 where  a.inpatientid=? and a.inpatientdate=? and a.status=0 
				 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and  b.modifydate=(select max(modifydate) from inpatientevaluatecontent where  inpatientid=a.inpatientid and inpatientdate=a.inpatientdate ) ";
		
			DataTable dtbResult=null;
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref dtbResult, objDPArr);
			if(lngRes>0 && dtbResult!=null && dtbResult.Rows.Count>0)
				p_strAbnormalInfo=dtbResult.Rows[0][0].ToString();
            //objHRPServ.Dispose();
			return lngRes;
		}
		#region Remark
//		/// <summary>
//		/// 查出创建时间OpenDate对应的所有信息（主从表），p_strInPatientDate和p_strOpenDate的格式必须为"yyyy-MM-dd HH:mm:ss"
//		/// </summary>		
//		public long m_lngGetRecordByOpenDate(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
//		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出OpenDate为指定时间的记录中,ModifyDate为最大修改时间的记录
//			p_strXML="";
//			p_intRows=0;	//无Status字段
//			string strCommand = "Select a.* ,b.* from InPatientEvaluate a,InPatientEvaluateContent b "+
//				" where  a.InPatientID='"+p_strInPatientID+"' and a.InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and a.OpenDate="+clsHRPTableService.s_strOracleDateTime(p_strOpenDate)+" "+
//				" and a.InPatientID=b.InPatientID and a.InPatientDate=b.InPatientDate and a.OpenDate=b.OpenDate "+
//				" and b.ModifyDate=(select Max(ModifyDate) from InPatientEvaluateContent where  InPatientID='"+p_strInPatientID+"' and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and OpenDate="+clsHRPTableService.s_strOracleDateTime(p_strOpenDate)+") ";
//		
//			return hrp.lngGetXMLTable(strCommand,ref p_strXML,ref p_intRows);
//		}

//		/// <summary>
//		/// 查出所有信息（主从表），p_strInPatientDate的格式必须为"yyyy-MM-dd HH:mm:ss"
//		/// </summary>		
//		public long m_lngGetAllRecord(string p_strInPatientID,string p_strInPatientDate,out string p_strXML,out int p_intRows)
//		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出OpenDate为指定时间的记录中,ModifyDate为最大修改时间的记录
//			p_strXML="";
//			p_intRows=0;	//无Status字段
//			string strCommand = "Select a.*,a.OpenDate as CDate ,b.* from InPatientEvaluate a,InPatientEvaluateContent b, "+
//				"( "+
//				" select Max(ModifyDate) as MDate,OpenDate from InPatientEvaluateContent "+
//				" Where trim(InPatientID)='"+p_strInPatientID+"' and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" "+
//				" Group by OpenDate "+
//				" )as Base "+
//				" where  a.InPatientID='"+p_strInPatientID+"' and a.InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" "+
//				" and a.InPatientID=b.InPatientID and a.InPatientDate=b.InPatientDate and a.OpenDate=b.OpenDate "+					
//				" and  b.ModifyDate = Base.MDate and b.OpenDate = Base.OpenDate "+
//				"Order by a.OpenDate";
//			return hrp.lngGetXMLTable(strCommand,ref p_strXML,ref p_intRows);
//		}
//
//		/// <summary>
//		/// 查出所有在p_strOpenDateFrom和p_strOpenDateTo日期之间的信息（主从表），p_strInPatientDate,p_strOpenDateFrom,p_strOpenDateTo的格式必须为"yyyy-MM-dd HH:mm:ss"或"yyyy-MM-dd"
//		/// </summary>		
//		public long m_lngGetAllRecordBetweenDates(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDateFrom,string p_strOpenDateTo,out string p_strXML,out int p_intRows)
//		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出OpenDate为指定时间的记录中,ModifyDate为最大修改时间的记录
//			p_strXML="";
//			p_intRows=0;	//无Status字段
//			string strCommand = "Select a.*,a.OpenDate as CDate ,b.* from InPatientEvaluate a,InPatientEvaluateContent b, "+
//				"( "+
//				" select Max(ModifyDate) as MDate,OpenDate from InPatientEvaluateContent "+
//				" Where trim(InPatientID)='"+p_strInPatientID+"' and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" and OpenDate between '"+p_strOpenDateFrom.Substring(0,10)+"' and '"+p_strOpenDateTo.Substring(0,10)+" 23:59:59' "+
//				" Group by OpenDate "+
//				" )as Base "+
//				" where  a.InPatientID='"+p_strInPatientID+"' and a.InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" "+
//				" and a.InPatientID=b.InPatientID and a.InPatientDate=b.InPatientDate and a.OpenDate=b.OpenDate "+					
//				" and  b.ModifyDate = Base.MDate and b.OpenDate = Base.OpenDate "+					
//				"Order by a.OpenDate";
//			return hrp.lngGetXMLTable(strCommand,ref p_strXML,ref p_intRows);
//		}
		#endregion
		/// <summary>
		/// 添加信息
		/// </summary>
		[AutoComplete]	
		public long m_lngAddNew(
			string p_strMainXml,string p_strSubXml)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMainGeneralNurseRecordService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			//添加记录时，主从表同时添加一条记录			
			if(p_strMainXml=="" || p_strSubXml=="")return -1;	
			clsHRPTableService objHRPServ =new clsHRPTableService();		
			long lngRes=objHRPServ.add_new_record("InPatientEvaluate",p_strMainXml); 	
			if(lngRes==1)
				lngRes=objHRPServ.add_new_record("InPatientEvaluateContent",p_strSubXml);
            //objHRPServ.Dispose();
			return lngRes;
		}

		/// <summary>
		/// 修改信息
		/// </summary>	
		[AutoComplete]	
		public long m_lngModify(
			string p_strMainXml,string p_strSubXml)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientEvaluateServ","m_lngModify");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			//修改记录时，主表更新原来记录中的相关XML记录，同时从表添加一条记录	
			if(p_strMainXml=="" || p_strSubXml=="")return -1;	
			clsHRPTableService objHRPServ =new clsHRPTableService();		
			long lngRes=objHRPServ.modify_record("InPatientEvaluate",p_strMainXml,"INPATIENTID","INPATIENTDATE","OPENDATE"); 	
			if(lngRes==1)
                lngRes = objHRPServ.add_new_record("InPatientEvaluateContent", p_strSubXml);
            //objHRPServ.Dispose();
			return lngRes;
		}

		/// <summary>
		/// 删除
		/// </summary>	
		[AutoComplete]		
		private long m_lngDeleteInMainTable(string p_strTableName,string p_strDeActivedOperatorID,string p_strInPatientID,string p_strInPatientDate)
		{
			string strSQL="update "+p_strTableName+" set status=1 , deactivedoperatorid=? , deactiveddate= ?"+
				" where inpatientid=? and inpatientdate=? and status=0" ;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(4, out objDPArr);
            objDPArr[0].Value = p_strDeActivedOperatorID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[2].Value = p_strInPatientID;
            objDPArr[3].DbType = DbType.DateTime;
            objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);

            long lngEff = -1;
            return objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
		}
	
		[AutoComplete]
		public long m_lngDelete(
			string p_strDeActivedOperatorID,string p_strInPatientID,string p_strInPatientDate)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientEvaluateServ","m_lngDelete");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			return m_lngDeleteInMainTable("InPatientEvaluate",p_strDeActivedOperatorID,p_strInPatientID,p_strInPatientDate);
		}


	}
}

