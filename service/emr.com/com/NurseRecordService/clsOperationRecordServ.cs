using System.EnterpriseServices;
using System;
//using com.digitalwave.HRPService ;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.PublicMiddleTier;
using System.Text ;
using System.Data ;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.OperationRecord
{
	/// <summary>
	/// 手术护理记录的中间层
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsOperationRecordServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsOperationRecordServ()
		{}
		#region Read

		#region Read <OperationRecordContent>
		[AutoComplete]
		public long m_lngGetOperationRecordContent(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,out string  p_strXml,out int  intRows)
		{
			p_strXml="";
			intRows=0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetOperationRecordContent");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+ @" main.createdate,main.createuserid,main.ifconfirm,main.confirmreason,
main.confirmreasonxml,main.operationnamexml,main.anaesthesiamodexml,main.sensesxml,main.allergicxml,main.operationlocationxml,main.electknifexml,
main.doublepolexml,main.cathodelocationskinxml,main.cathodelocskinafoprxml,main.stypticrubberxml,
main.upxml,main.downxml,main.foleyxml,main.stomachxml,main.skinantisepsisxml,main.bloodxml,
main.inliquidqtyxml,main.peeoperatingqtyxml,main.outflowxml,main.fromheadtofootskinxml,main.fromheadtofootskinafoprxml,
main.samplexml,main.afteroperationsendxml,main.tendrecordxml,main.operationroomxml,main.operation_anaesthesiaxml,
main.firstprintdate,main.allergiccontentxml,main.otheroperationlocationxml,main.electknifemodelxml,
main.doublepolecontentxml,main.cathodelocationxml,main.stypticpressuremodexml,main.uppuffdatetimexml,
main.updeflatedatetimexml,main.uptotaldatetimexml,main.uppressxml,main.downpuffdatetimexml,
main.downdeflatedatetimexml,main.downtotaldatetimexml,main.downpressxml,main.foleyothercontentxml,
main.skinantisepsisothercontentxml,main.allbloodqtyxml,main.redcellqtyxml,main.bloodplasmqtyxml,
main.ownbloodqtyxml,main.bloodotherqtyxml,main.fromheadtofootskinbfoprcontxml,main.fromheadtofootskinafoprcontxml,
main.sampleothercontentxml,content.inpatientid,
       content.inpatientdate,
       content.opendate,
       content.lastmodifydate,
       content.lastmodifyuserid,
       content.deactiveddate,
       content.deactivedoperatorid,
       content.patientindate,
       content.sensesclearheaded,
       content.sensessleep,
       content.status,
       content.sensescoma,
       content.haveallergic,
       content.havenotallergic,
       content.allergiccontent,
       content.operationlocationonhisback,
       content.operationlocationside,
       content.operationlocationpa,
       content.operationlocationparaplegic,
       content.operationlocationhypothyroid,
       content.operationlocationother,
       content.otheroperationlocation,
       content.operationroom,
       content.operationbegindate,
       content.operationenddate,
       content.operationleavedate,
       content.havenotelectknife,
       content.haveusedelectknife,
       content.electknifemodel,
       content.havenotdoublepole,
       content.havedoublepole,
       content.doublepolecontent,
       content.cathodelocation,
       content.cathodelocskinbfoprmar,
       content.cathodelocskinbfoprfull,
       content.cathodelocskinafoprmar,
       content.cathodelocskinafoprfull,
       content.stypticrubber,
       content.stypticpressure,
       content.stypticpressuremode,
       content.upforearm,
       content.upthigh,
       content.upright,
       content.upleft,
       content.uppuffdatetime,
       content.updeflatedatetime,
       content.uptotaldatetime,
       content.uppress,
       content.downforearm,
       content.downthigh,
       content.downright,
       content.downleft,
       content.downpuffdatetime,
       content.downdeflatedatetime,
       content.downtotaldatetime,
       content.downpress,
       content.foleysickroom,
       content.foleyoperationroom,
       content.foleydoubleantrum,
       content.foleythreeantrum,
       content.foleyother,
       content.foleyothercontent,
       content.stomachsickroom,
       content.stomachoprationroom,
       content.skinantisepsis2,
       content.skinantisepsis75,
       content.skinantisepsisiodin,
       content.skinantisepsisiodinrare,
       content.skinantisepsisother,
       content.skinantisepsisothercontent,
       content.allblood,
       content.allbloodqty,
       content.redcell,
       content.redcellqty,
       content.bloodplasm,
       content.bloodplasmqty,
       content.ownblood,
       content.ownbloodqty,
       content.bloodother,
       content.bloodotherqty,
       content.inliquidqty,
       content.peeoperatingqty,
       content.haveoutflow,
       content.nothaveoutflow,
       content.fromheadtofootskinbfoprmar,
       content.fromheadtofootskinbfoprfull,
       content.fromheadtofootskinbfoprcont,
       content.fromheadtofootskinafoprmar,
       content.fromheadtofootskinafoprfull,
       content.fromheadtofootskinafoprcont,
       content.samplegeneral,
       content.sampleslice,
       content.samplebacilli,
       content.sampleother,
       content.sampleothercontent,
       content.afteroperationsendrenew,
       content.afteroperationsendicu,
       content.afteroperationsendsickroom,
       content.tendrecord,
       content.operationname,
       content.anaesthesiamode
from operationrecord main,operationrecordcontent content
where main.inpatientid = content.inpatientid
and main.inpatientdate = content.inpatientdate
and main.opendate = content.opendate
and main.inpatientid = ?
and main.inpatientdate = ?
and main.createdate =?
and main.status = '0'
order by content.lastmodifydate desc" + clsDatabaseSQLConvert.s_StrRownum;
			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strXml, ref intRows, objDPArr); 
 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;

		}

		[AutoComplete]
		public long m_lngGetDeleteOperationRecordContent(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,out string  p_strXml,out int  intRows)
		{
			p_strXml="";
			intRows=0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetDeleteOperationRecordContent");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
            string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @" main.createdate,
       main.createuserid,
       main.ifconfirm,
       main.confirmreason,
       main.confirmreasonxml,
       main.status,
       main.operationnamexml,
       main.anaesthesiamodexml,
       main.deactiveddate,
       main.deactivedoperatorid,
       main.sensesxml,
       main.allergicxml,
       main.operationlocationxml,
       main.electknifexml,
       main.doublepolexml,
       main.cathodelocationskinxml,
       main.cathodelocskinafoprxml,
       main.stypticrubberxml,
       main.upxml,
       main.downxml,
       main.foleyxml,
       main.stomachxml,
       main.skinantisepsisxml,
       main.bloodxml,
       main.inliquidqtyxml,
       main.peeoperatingqtyxml,
       main.outflowxml,
       main.fromheadtofootskinxml,
       main.fromheadtofootskinafoprxml,
       main.samplexml,
       main.afteroperationsendxml,
       main.tendrecordxml,
       main.operationroomxml,
       main.operation_anaesthesiaxml,
       main.firstprintdate,
       main.allergiccontentxml,
       main.otheroperationlocationxml,
       main.electknifemodelxml,
       main.doublepolecontentxml,
       main.cathodelocationxml,
       main.stypticpressuremodexml,
       main.uppuffdatetimexml,
       main.updeflatedatetimexml,
       main.uptotaldatetimexml,
       main.uppressxml,
       main.downpuffdatetimexml,
       main.downdeflatedatetimexml,
       main.downtotaldatetimexml,
       main.downpressxml,
       main.foleyothercontentxml,
       main.skinantisepsisothercontentxml,
       main.allbloodqtyxml,
       main.redcellqtyxml,
       main.bloodplasmqtyxml,
       main.ownbloodqtyxml,
       main.bloodotherqtyxml,
       main.fromheadtofootskinbfoprcontxml,
       main.fromheadtofootskinafoprcontxml,
       main.sampleothercontentxml,
       main.sequence_int,content.inpatientid,
       content.inpatientdate,
       content.opendate,
       content.lastmodifydate,
       content.lastmodifyuserid,
       content.deactiveddate,
       content.deactivedoperatorid,
       content.patientindate,
       content.sensesclearheaded,
       content.sensessleep,
       content.sensescoma,
       content.haveallergic,
       content.havenotallergic,
       content.allergiccontent,
       content.operationlocationonhisback,
       content.operationlocationside,
       content.operationlocationpa,
       content.operationlocationparaplegic,
       content.operationlocationhypothyroid,
       content.operationlocationother,
       content.otheroperationlocation,
       content.operationroom,
       content.operationbegindate,
       content.operationenddate,
       content.operationleavedate,
       content.havenotelectknife,
       content.haveusedelectknife,
       content.electknifemodel,
       content.havenotdoublepole,
       content.havedoublepole,
       content.doublepolecontent,
       content.cathodelocation,
       content.cathodelocskinbfoprmar,
       content.cathodelocskinbfoprfull,
       content.cathodelocskinafoprmar,
       content.cathodelocskinafoprfull,
       content.stypticrubber,
       content.stypticpressure,
       content.stypticpressuremode,
       content.upforearm,
       content.upthigh,
       content.upright,
       content.upleft,
       content.uppuffdatetime,
       content.updeflatedatetime,
       content.uptotaldatetime,
       content.uppress,
       content.downforearm,
       content.downthigh,
       content.downright,
       content.downleft,
       content.downpuffdatetime,
       content.downdeflatedatetime,
       content.downtotaldatetime,
       content.downpress,
       content.foleysickroom,
       content.foleyoperationroom,
       content.foleydoubleantrum,
       content.foleythreeantrum,
       content.foleyother,
       content.foleyothercontent,
       content.stomachsickroom,
       content.stomachoprationroom,
       content.skinantisepsis2,
       content.skinantisepsis75,
       content.skinantisepsisiodin,
       content.skinantisepsisiodinrare,
       content.skinantisepsisother,
       content.skinantisepsisothercontent,
       content.allblood,
       content.allbloodqty,
       content.redcell,
       content.redcellqty,
       content.bloodplasm,
       content.bloodplasmqty,
       content.ownblood,
       content.ownbloodqty,
       content.bloodother,
       content.bloodotherqty,
       content.inliquidqty,
       content.peeoperatingqty,
       content.haveoutflow,
       content.nothaveoutflow,
       content.fromheadtofootskinbfoprmar,
       content.fromheadtofootskinbfoprfull,
       content.fromheadtofootskinbfoprcont,
       content.fromheadtofootskinafoprmar,
       content.fromheadtofootskinafoprfull,
       content.fromheadtofootskinafoprcont,
       content.samplegeneral,
       content.sampleslice,
       content.samplebacilli,
       content.sampleother,
       content.sampleothercontent,
       content.afteroperationsendrenew,
       content.afteroperationsendicu,
       content.afteroperationsendsickroom,
       content.tendrecord,
       content.operationname,
       content.anaesthesiamode
								from operationrecord main,operationrecordcontent content
								where main.inpatientid = content.inpatientid
								and main.inpatientdate = content.inpatientdate
								and main.opendate = content.opendate
								and main.inpatientid = ? and main.inpatientdate = ? and main.opendate = ?
                                and main.status =1 order by content.lastmodifydate desc" + clsDatabaseSQLConvert.s_StrRownum;

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strXml, ref intRows, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}

		#endregion

		#region Read <OperationRecord>
		[AutoComplete]
		public long m_lngGetOperationRecord(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,out string  p_strXml,out int  intRows)
		{

			p_strXml="";
			intRows=0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetOperationRecord");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string strCommand="";
			
			strCommand=@"select main.inpatientid,
       main.inpatientdate,
       main.opendate,
       main.createdate,
       main.createuserid,
       main.ifconfirm,
       main.confirmreason,
       main.confirmreasonxml,
       main.status,
       main.operationnamexml,
       main.anaesthesiamodexml,
       main.deactiveddate,
       main.deactivedoperatorid,
       main.sensesxml,
       main.allergicxml,
       main.operationlocationxml,
       main.electknifexml,
       main.doublepolexml,
       main.cathodelocationskinxml,
       main.cathodelocskinafoprxml,
       main.stypticrubberxml,
       main.upxml,
       main.downxml,
       main.foleyxml,
       main.stomachxml,
       main.skinantisepsisxml,
       main.bloodxml,
       main.inliquidqtyxml,
       main.peeoperatingqtyxml,
       main.outflowxml,
       main.fromheadtofootskinxml,
       main.fromheadtofootskinafoprxml,
       main.samplexml,
       main.afteroperationsendxml,
       main.tendrecordxml,
       main.operationroomxml,
       main.operation_anaesthesiaxml,
       main.firstprintdate,
       main.allergiccontentxml,
       main.otheroperationlocationxml,
       main.electknifemodelxml,
       main.doublepolecontentxml,
       main.cathodelocationxml,
       main.stypticpressuremodexml,
       main.uppuffdatetimexml,
       main.updeflatedatetimexml,
       main.uptotaldatetimexml,
       main.uppressxml,
       main.downpuffdatetimexml,
       main.downdeflatedatetimexml,
       main.downtotaldatetimexml,
       main.downpressxml,
       main.foleyothercontentxml,
       main.skinantisepsisothercontentxml,
       main.allbloodqtyxml,
       main.redcellqtyxml,
       main.bloodplasmqtyxml,
       main.ownbloodqtyxml,
       main.bloodotherqtyxml,
       main.fromheadtofootskinbfoprcontxml,
       main.fromheadtofootskinafoprcontxml,
       main.sampleothercontentxml,
       main.sequence_int  from operationrecord main    where main.inpatientid= ?
and main.status =0 and main.createdate= ? and main.inpatientdate= ?";

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);

		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}

		[AutoComplete]
		public long m_lngGetDeleteOperationRecord(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,out string  p_strXml,out int  intRows)
		{

			p_strXml="";
			intRows=0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetDeleteOperationRecord");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string strCommand="";
			
			strCommand=@"select main.inpatientid,
       main.inpatientdate,
       main.opendate,
       main.createdate,
       main.createuserid,
       main.ifconfirm,
       main.confirmreason,
       main.confirmreasonxml,
       main.status,
       main.operationnamexml,
       main.anaesthesiamodexml,
       main.deactiveddate,
       main.deactivedoperatorid,
       main.sensesxml,
       main.allergicxml,
       main.operationlocationxml,
       main.electknifexml,
       main.doublepolexml,
       main.cathodelocationskinxml,
       main.cathodelocskinafoprxml,
       main.stypticrubberxml,
       main.upxml,
       main.downxml,
       main.foleyxml,
       main.stomachxml,
       main.skinantisepsisxml,
       main.bloodxml,
       main.inliquidqtyxml,
       main.peeoperatingqtyxml,
       main.outflowxml,
       main.fromheadtofootskinxml,
       main.fromheadtofootskinafoprxml,
       main.samplexml,
       main.afteroperationsendxml,
       main.tendrecordxml,
       main.operationroomxml,
       main.operation_anaesthesiaxml,
       main.firstprintdate,
       main.allergiccontentxml,
       main.otheroperationlocationxml,
       main.electknifemodelxml,
       main.doublepolecontentxml,
       main.cathodelocationxml,
       main.stypticpressuremodexml,
       main.uppuffdatetimexml,
       main.updeflatedatetimexml,
       main.uptotaldatetimexml,
       main.uppressxml,
       main.downpuffdatetimexml,
       main.downdeflatedatetimexml,
       main.downtotaldatetimexml,
       main.downpressxml,
       main.foleyothercontentxml,
       main.skinantisepsisothercontentxml,
       main.allbloodqtyxml,
       main.redcellqtyxml,
       main.bloodplasmqtyxml,
       main.ownbloodqtyxml,
       main.bloodotherqtyxml,
       main.fromheadtofootskinbfoprcontxml,
       main.fromheadtofootskinafoprcontxml,
       main.sampleothercontentxml,
       main.sequence_int  from operationrecord main  
                  where main.inpatientid=?
                 and main.status =1 and main.opendate= ? and main.inpatientdate=?";
			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr); 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;

		}

		#endregion

		#region Read<OperationRecord_OperationID>
		[AutoComplete]
		public long m_lngGetLastestOperationID(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strXML,out int p_intRows)
		{
			p_strXML = null;
			p_intRows = 0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetLastestOperationID");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -1;

            string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @" content.inpatientid,
       content.inpatientdate,
       content.opendate,
       content.operationid,
       content.lastmodifydate,
       content.status,
       content.deactiveddate,
       content.deactivedoperatorid,op.operationname
								from operationrecord main,operationrecord_operationid content,operation op
								where main.inpatientid = content.inpatientid
								and main.inpatientdate = content.inpatientdate
								and main.opendate = content.opendate
								and content.operationid = op.operationid and op.status=0 
								and main.inpatientid = ?
								and main.inpatientdate = ?
								and main.createdate =?
                                and main.status = '0'
								order by content.lastmodifydate desc" +clsDatabaseSQLConvert.s_StrRownum;
			
			
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
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strXML, ref p_intRows, objDPArr); 
 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
		
		#endregion

		#region Read<OperationRecord_Anaesthesia>
		
		[AutoComplete]
		public long m_lngGetLastestAnaesthesiaID(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strXML,out int p_intRows)
		{
			p_strXML = null;
			p_intRows = 0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetLastestAnaesthesiaID");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			if(p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strCreateDate == null || p_strCreateDate == "")
				return -1;

            string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @" content.inpatientid,
       content.inpatientdate,
       content.opendate,
       content.anaesthesiamodeid,
       content.lastmodifydate,
       content.status,
       content.deactiveddate,
       content.deactivedoperatorid,am.anaesthesiamodename
								from operationrecord main,operationrecord_anaesthesia content,anaesthesiamode am
								where main.inpatientid = content.inpatientid
								and main.inpatientdate = content.inpatientdate
								and main.opendate = content.opendate
								and content.anaesthesiamodeid = am.anaesthesiamodeid and am.status=0
								and main.inpatientid = ?
								and main.inpatientdate = ?
								and main.createdate =?
                                and main.status = '0'
								order by content.lastmodifydate desc" +clsDatabaseSQLConvert.s_StrRownum;
			

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
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strXML, ref p_intRows, objDPArr);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		}
		#endregion

		#region Read<OperationRecord_Operator>
		
		[AutoComplete]
		public long m_lngGetOperation_Nurse(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  strXml,ref int  intRows)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetOperation_Nurse");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

            string strSQL = @"select oro1.inpatientid,
       oro1.inpatientdate,
       oro1.opendate,
       oro1.lastmodifydate,
       oro1.opertorid,
       oro1.operatorflag,
       oro1.status,
       oro1.deactiveddate,
       oro1.deactivedoperatorid ,pbi.lastname_vchr as operatorname 
                              from operationrecord main ,operationrecord_operator oro1,t_bse_employee pbi
                              where main.inpatientid=oro1.inpatientid
                              and main.inpatientdate=oro1.inpatientdate
                              and main.opendate=oro1.opendate
							  and oro1.opertorid=pbi.empno_chr
                              and main.inpatientid=?
							  and main.inpatientdate=?
							  and main.createdate=?
							  and oro1.lastmodifydate = 
							  (select max(oro2.lastmodifydate) as maxlastmodifydate 
							  from operationrecord or2 ,operationrecord_operator oro2
							  where or2.inpatientid=oro2.inpatientid
							  and or2.inpatientdate=oro2.inpatientdate
							  and or2.opendate=oro2.opendate
							  and or2.inpatientid=?
							  and or2.inpatientdate=?
							  and or2.createdate=?
							  and or2.status='0'
							  and oro1.status=0) order by oro1.operatorflag asc "; 	
						
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref strXml, ref intRows, objDPArr); 
 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;

		}
		#endregion

		#region Read<OperationRecord_WoundThing>
		
		[AutoComplete]
		public long m_lngGetOperation_WoundThing(
			string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, ref string strXml,ref int  intRows)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetOperation_WoundThing");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
            string strSQL = @"select main.inpatientid,
       main.inpatientdate,
       main.opendate,
       main.createdate,
       main.createuserid,
       main.ifconfirm,
       main.confirmreason,
       main.confirmreasonxml,
       main.status,
       main.operationnamexml,
       main.anaesthesiamodexml,
       main.deactiveddate,
       main.deactivedoperatorid,
       main.sensesxml,
       main.allergicxml,
       main.operationlocationxml,
       main.electknifexml,
       main.doublepolexml,
       main.cathodelocationskinxml,
       main.cathodelocskinafoprxml,
       main.stypticrubberxml,
       main.upxml,
       main.downxml,
       main.foleyxml,
       main.stomachxml,
       main.skinantisepsisxml,
       main.bloodxml,
       main.inliquidqtyxml,
       main.peeoperatingqtyxml,
       main.outflowxml,
       main.fromheadtofootskinxml,
       main.fromheadtofootskinafoprxml,
       main.samplexml,
       main.afteroperationsendxml,
       main.tendrecordxml,
       main.operationroomxml,
       main.operation_anaesthesiaxml,
       main.firstprintdate,
       main.allergiccontentxml,
       main.otheroperationlocationxml,
       main.electknifemodelxml,
       main.doublepolecontentxml,
       main.cathodelocationxml,
       main.stypticpressuremodexml,
       main.uppuffdatetimexml,
       main.updeflatedatetimexml,
       main.uptotaldatetimexml,
       main.uppressxml,
       main.downpuffdatetimexml,
       main.downdeflatedatetimexml,
       main.downtotaldatetimexml,
       main.downpressxml,
       main.foleyothercontentxml,
       main.skinantisepsisothercontentxml,
       main.allbloodqtyxml,
       main.redcellqtyxml,
       main.bloodplasmqtyxml,
       main.ownbloodqtyxml,
       main.bloodotherqtyxml,
       main.fromheadtofootskinbfoprcontxml,
       main.fromheadtofootskinafoprcontxml,
       main.sampleothercontentxml,
       main.sequence_int ,
       oro1.lastmodifydate,
       oro1.woundthingid,
       oro1.quantity,
       oro1.status,
       oro1.deactiveddate,
       oro1.deactivedoperatorid ,w.woundthingname 
                              from operationrecord  main ,operationrecord_woundthing  oro1,woundthing w 
                              where main.inpatientid=oro1.inpatientid 
                              and main.inpatientdate=oro1.inpatientdate
                              and main.opendate=oro1.opendate and oro1.woundthingid=w.woundthingid 
                              and main.inpatientid=?
							  and main.inpatientdate=?
							  and main.createdate=?
							  and oro1.lastmodifydate = 
							  (select max(oro2.lastmodifydate) as maxlastmodifydate 
							  from operationrecord or2 ,operationrecord_woundthing oro2
							  where or2.inpatientid=oro2.inpatientid
							  and or2.inpatientdate=oro2.inpatientdate
							  and or2.opendate=oro2.opendate
							  and or2.inpatientid=?
							  and or2.inpatientdate=?
							  and or2.createdate=?
							  and or2.status='0'
							  and oro1.status=0) " ; 	
			


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[3].Value = p_strInPatientID;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref strXml, ref intRows, objDPArr);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		}
		#endregion

		/// <summary>
		/// 查出时间对应的主表信息，p_strInPatientDate的格式必须为"yyyy-MM-dd HH:mm:ss"
		/// </summary>
		[AutoComplete]		
		public long m_lngGetLatestRecord(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
		{//查询时除InHospitalNO和InHospitalDate基本信息外，只要查出OpenDate为指定时间的记录
			p_strXML="";
			p_intRows=0;	//Status=0条件多余	
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetLatestRecord");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
            string strCommand = @"select main.inpatientid,
       main.inpatientdate,
       main.opendate,
       main.createdate,
       main.createuserid,
       main.ifconfirm,
       main.confirmreason,
       main.confirmreasonxml,
       main.status,
       main.operationnamexml,
       main.anaesthesiamodexml,
       main.deactiveddate,
       main.deactivedoperatorid,
       main.sensesxml,
       main.allergicxml,
       main.operationlocationxml,
       main.electknifexml,
       main.doublepolexml,
       main.cathodelocationskinxml,
       main.cathodelocskinafoprxml,
       main.stypticrubberxml,
       main.upxml,
       main.downxml,
       main.foleyxml,
       main.stomachxml,
       main.skinantisepsisxml,
       main.bloodxml,
       main.inliquidqtyxml,
       main.peeoperatingqtyxml,
       main.outflowxml,
       main.fromheadtofootskinxml,
       main.fromheadtofootskinafoprxml,
       main.samplexml,
       main.afteroperationsendxml,
       main.tendrecordxml,
       main.operationroomxml,
       main.operation_anaesthesiaxml,
       main.firstprintdate,
       main.allergiccontentxml,
       main.otheroperationlocationxml,
       main.electknifemodelxml,
       main.doublepolecontentxml,
       main.cathodelocationxml,
       main.stypticpressuremodexml,
       main.uppuffdatetimexml,
       main.updeflatedatetimexml,
       main.uptotaldatetimexml,
       main.uppressxml,
       main.downpuffdatetimexml,
       main.downdeflatedatetimexml,
       main.downtotaldatetimexml,
       main.downpressxml,
       main.foleyothercontentxml,
       main.skinantisepsisothercontentxml,
       main.allbloodqtyxml,
       main.redcellqtyxml,
       main.bloodplasmqtyxml,
       main.ownbloodqtyxml,
       main.bloodotherqtyxml,
       main.fromheadtofootskinbfoprcontxml,
       main.fromheadtofootskinafoprcontxml,
       main.sampleothercontentxml,
       main.sequence_int  from operationrecord main where  main.inpatientid=? and main.inpatientdate=? and main.opendate=?";

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
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		}

		[AutoComplete]	
		public long m_lngGetOperationRecord_Anaesthesia(
			string p_strInPatientID,
			string p_strInPatientDate,string p_strOpenDate,out string  strXml,out int  intRows)
		{
			strXml="";
			intRows=0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetOperationRecord_Anaesthesia");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string strCommand=@"select inpatientid,
       inpatientdate,
       opendate,
       anaesthesiamodeid,
       lastmodifydate,
       status,
       deactiveddate,
       deactivedoperatorid  from operationrecord_anaesthesia where inpatientid=?  and status =0 and inpatientdate=? and opendate=? order by lastmodifydate asc ";

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
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref strXml, ref intRows, objDPArr);

 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
		
		[AutoComplete]
		public long m_lngGetOperationRecord_OperationID(
			string p_strInPatientID,
			string p_strInPatientDate,string p_strOpenDate,out string  strXml,out int  intRows)
		{
			strXml="";
			intRows=0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetOperationRecord_OperationID");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
            string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       operationid,
       lastmodifydate,
       status,
       deactiveddate,
       deactivedoperatorid  from operationrecord_operationid where inpatientid=?  
                 and status =0 and inpatientdate=? and opendate=?
				 order by lastmodifydate asc ";

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
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref strXml, ref intRows, objDPArr);

 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
		
		[AutoComplete]
		public long m_lngGetOperationRecord_Operator(
			string p_strInPatientID,
			string p_strInPatientDate,string p_strOpenDate,out string  strXml,out int  intRows)
		{
			strXml="";
			intRows=0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetOperationRecord_Operator");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
            string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       lastmodifydate,
       opertorid,
       operatorflag,
       status,
       deactiveddate,
       deactivedoperatorid  from  operationrecord_operator where inpatientid=? 
                 and inpatientdate=? and opendate=?
				 order by lastmodifydate ";

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
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref strXml, ref intRows, objDPArr);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
		
		#endregion

		#region checkModifyDate
				
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_blnCheckResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckLastModifyDate(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string strLastModifyDate ,out string strModifyUser)
		{
			strLastModifyDate="";
			strModifyUser="";

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngCheckLastModifyDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			
			string strSQL=@"select max(b.lastmodifydate) as lastmodifydate , b.lastmodifyuserid
                             from operationrecord a inner join
                             operationrecordcontent b on a.inpatientid = b.inpatientid and 
                             a.inpatientdate = b.inpatientdate and a.opendate = b.opendate
                             where (a.status =0) and (a.inpatientid = ?) and (a.inpatientdate=?) and 
                             (a.opendate=?) group by b.lastmodifyuserid ";
			DataTable dtResult = new DataTable();

			
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
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr); 
 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
 
			if(lngRes <= 0)
				return lngRes;
			else if(lngRes > 0 && dtResult.Rows.Count > 0)
			{
				strLastModifyDate=DateTime.Parse(dtResult.Rows[0][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
				strModifyUser=dtResult.Rows[0][1].ToString();
				return 1;
			}
			return lngRes;
		}

		#endregion 

		#region GetDeleteUser
		[AutoComplete]
		public long m_lngGetDeleteUser(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string strDeActiveDate ,out string strDeActiveUser)
		{
			strDeActiveDate="";
			strDeActiveUser="";
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetDeleteUser");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string strSQL=@"select deactiveddate,deactivedoperatorid 
                            from operationrecord 
                            where status =1 and inpatientid = ?
                            and inpatientdate=? 
                            and opendate=?";
			DataTable dtResult = new DataTable();


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
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
			if(lngRes <= 0)
				return lngRes;
			else
			{
				strDeActiveDate=DateTime.Parse(dtResult.Rows[0][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
				strDeActiveUser=dtResult.Rows[0][1].ToString();
				return 1;
			}
		}

		#endregion 

		#region checkCreateDate
		/// <summary>
		/// 检查是否已经有记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_blnCheckResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckNewCreateDate(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out bool p_blnIsAddNew)
		{
			p_blnIsAddNew = false;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngCheckNewCreateDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

            string strSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       status,
       operationnamexml,
       anaesthesiamodexml,
       deactiveddate,
       deactivedoperatorid,
       sensesxml,
       allergicxml,
       operationlocationxml,
       electknifexml,
       doublepolexml,
       cathodelocationskinxml,
       cathodelocskinafoprxml,
       stypticrubberxml,
       upxml,
       downxml,
       foleyxml,
       stomachxml,
       skinantisepsisxml,
       bloodxml,
       inliquidqtyxml,
       peeoperatingqtyxml,
       outflowxml,
       fromheadtofootskinxml,
       fromheadtofootskinafoprxml,
       samplexml,
       afteroperationsendxml,
       tendrecordxml,
       operationroomxml,
       operation_anaesthesiaxml,
       firstprintdate,
       allergiccontentxml,
       otheroperationlocationxml,
       electknifemodelxml,
       doublepolecontentxml,
       cathodelocationxml,
       stypticpressuremodexml,
       uppuffdatetimexml,
       updeflatedatetimexml,
       uptotaldatetimexml,
       uppressxml,
       downpuffdatetimexml,
       downdeflatedatetimexml,
       downtotaldatetimexml,
       downpressxml,
       foleyothercontentxml,
       skinantisepsisothercontentxml,
       allbloodqtyxml,
       redcellqtyxml,
       bloodplasmqtyxml,
       ownbloodqtyxml,
       bloodotherqtyxml,
       fromheadtofootskinbfoprcontxml,
       fromheadtofootskinafoprcontxml,
       sampleothercontentxml,
       sequence_int 
								from operationrecord
								where inpatientid=?
								and inpatientdate=?
								and createdate=?
								and status=0";

			DataTable dtResult = new DataTable();

			
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
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr); 
 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
             //return lngRes;

			p_blnIsAddNew = false;

			if(lngRes <= 0)
				return lngRes;
			else
			{
				if(dtResult.Rows.Count > 0)
				{
					p_blnIsAddNew = false;
				}
				else
				{
					p_blnIsAddNew = true;
				}

				return 1;
			}
		}
		
		#endregion
	
		#region 初始麻醉方式||引流物||手术编码||医生模糊查询列表
		#region  获得手术名称
		
		[AutoComplete]
		public long m_lngGetOperationIDName(
			out string p_strXML,out int p_intRows)
		{
			p_strXML = null;
			p_intRows = 0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetOperationIDName");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string m_strCommand = @"select operationid,
       operationname,
       status,
       deactiveddate,
       deactivedoperatorid from operation where status =0 ";

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.lngGetXMLTable(m_strCommand,ref p_strXML,ref p_intRows);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		}
		#endregion

		#region 模糊查询引流物
		
		[AutoComplete]
		public long m_lngGetWoundThingIDName(
			string p_strQuery,out string p_strXML,out int p_intRows)
		{
			p_strXML = null;
			p_intRows = 0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetWoundThingIDName");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string m_strCommand = @"select woundthingid,
       woundthingname,
       status,
       deactiveddate,
       deactivedoperatorid from woundthing where status ='0' and woundthingid like '"+p_strQuery+"%'";

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.lngGetXMLTable(m_strCommand,ref p_strXML,ref p_intRows);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		}
		#endregion

		[AutoComplete]
		public long lngXMLLikeQuery_Doctor(
			string txtID ,ref string strXML,ref int intRows)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","lngXMLLikeQuery_Doctor");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			//模糊查找医生ID号
			if(txtID=="" ||txtID==null)
				return -1;
			string strCommand = @"select t.empno_chr, t.lastname_vchr
  from t_bse_employee t
 where t.empno_chr like '"+txtID.Replace(" ","")+"%'";

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.lngGetXMLTable(strCommand,ref strXML,ref intRows);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		}
			
		[AutoComplete]
		public long m_lngGetAnaesthesiaModeID(
			ref string strXML,ref int intRows)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetAnaesthesiaModeID");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string strCommand=" select anaesthesiamodeid,anaesthesiamodename from anaesthesiamode where status=0 " ;

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=objHRPServ.lngGetXMLTable(strCommand ,ref strXML,ref intRows );
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;            
		}
		
		#endregion
		
		#region All Record Time
		[AutoComplete]
		public long m_lngGetTimeInfoOfAPatient(
			string p_strInPatientID,string p_strInPatientDate,ref string  p_strXml,ref int  intRows)
		{
			p_strXml="";
			intRows=0;
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngGetTimeInfoOfAPatient");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			string strSQL = @"select main.createdate
								from operationrecord main
								where main.inpatientid = ?
								and main.inpatientdate = ? and status=0 order by createdate desc ";


			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strXml, ref intRows, objDPArr);
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;
		}
		#endregion
		
		#region Add New Record	
		[AutoComplete]	
		public long m_lngAddNew(
			string p_strMainXml,string p_strSubXml,string[] p_strNurseXMLArr,string p_strOperationTableXML,string p_strAnaesthesiaTableXML, string[] p_strWoundThingTableXMLArr)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngAddNew");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//添加记录时，主从表同时添加一条记录			
			if(p_strMainXml=="" || p_strSubXml=="")return -1;
            //if(p_strOperationTableXML == null || p_strOperationTableXML == "")
            //    return -1;
            //if(p_strAnaesthesiaTableXML == null || p_strAnaesthesiaTableXML == "")
            //    return -1;

//			#region add OpenDate ModifyDate
//			string strCurrentTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//			StringBuilder sbdTemp=null;
//			sbdTemp=new StringBuilder(p_strMainXml);
//			int intIndex=p_strMainXml.IndexOf(" ");
//			sbdTemp.Insert(intIndex,"   OpenDate =  '"+strCurrentTime+"'   ");
//			p_strMainXml=sbdTemp.ToString ();
//
//			sbdTemp=new StringBuilder(p_strSubXml);
//			intIndex=p_strSubXml.IndexOf(" ");
//			sbdTemp.Insert(intIndex,"   OpenDate =  '"+strCurrentTime+"'  LastModifyDate =  '"+strCurrentTime+"' ");
//			p_strSubXml=sbdTemp.ToString ();
//
//			sbdTemp=new StringBuilder(p_strOperationTableXML);
//			intIndex=p_strOperationTableXML.IndexOf(" ");
//			sbdTemp.Insert(intIndex,"   OpenDate =  '"+strCurrentTime+"'  LastModifyDate =  '"+strCurrentTime+"' ");
//			p_strOperationTableXML=sbdTemp.ToString ();
//
//			sbdTemp=new StringBuilder(p_strAnaesthesiaTableXML);
//			intIndex=p_strAnaesthesiaTableXML.IndexOf(" ");
//			sbdTemp.Insert(intIndex,"   OpenDate =  '"+strCurrentTime+"'  LastModifyDate =  '"+strCurrentTime+"' ");
//			p_strAnaesthesiaTableXML=sbdTemp.ToString ();
//			#endregion 

			

			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
				lngRes=
		     lngRes= objHRPServ.add_new_record("OperationRecord",p_strMainXml); 	
			if(lngRes==1)
				lngRes= objHRPServ.add_new_record("OperationRecordContent",p_strSubXml);
            if (lngRes == 1 && !string.IsNullOrEmpty(p_strOperationTableXML))
				lngRes= objHRPServ.add_new_record("OperationRecord_OperationID",p_strOperationTableXML);
            if (lngRes == 1 && !string.IsNullOrEmpty(p_strOperationTableXML))
				lngRes= objHRPServ.add_new_record("OperationRecord_Anaesthesia",p_strAnaesthesiaTableXML);
            if (p_strNurseXMLArr != null && p_strNurseXMLArr.Length > 0)
            {
                lngRes = m_lngAddNurse(p_strNurseXMLArr);
            }
			lngRes = m_lngAddWoundThing(p_strWoundThingTableXMLArr); 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
 
			return lngRes;
		}


		[AutoComplete]
		private long m_lngAddNurse(string[] p_strNurseXMLArr)
		{
			if(p_strNurseXMLArr == null || p_strNurseXMLArr.Length <=0 )
				return 1;
			long lngRes=0;
			
 			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
 		    for(int i1=0;i1<p_strNurseXMLArr.Length;i1++)
			{
				lngRes= objHRPServ.add_new_record("OperationRecord_Operator",p_strNurseXMLArr[i1]);
			} 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
 			return lngRes;
		}

		[AutoComplete]
		private long m_lngAddWoundThing(string[] m_strWoundThingTableXMLArr)
		{
			if(m_strWoundThingTableXMLArr == null || m_strWoundThingTableXMLArr.Length <=0)
				return 1;
			long lngRes=0;
			
 			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
 		    
			for(int i1=0;i1<m_strWoundThingTableXMLArr.Length;i1++)
			{				
				lngRes= objHRPServ.add_new_record("OperationRecord_WoundThing",m_strWoundThingTableXMLArr[i1]);
			} 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
 			return lngRes;
		}

		#endregion

		#region Modify Exist Record
		[AutoComplete]
		public long m_lngModify(
			string p_strMainXml,string p_strSubXml,string[] p_strNurseXMLArr,string p_strOperationTableXML,string p_strAnaesthesiaTableXML, string[] p_strWoundThingTableXMLArr)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngModify");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;

			//修改记录时，主表更新原来记录中的相关XML记录，同时从表添加一条记录	
			if(p_strMainXml=="" || p_strSubXml=="")return -1;	
		
//			#region add LastModifyDate
//			string strCurrentTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//			StringBuilder sbdTemp=null;
//			
//			sbdTemp=new StringBuilder(p_strSubXml);
//			int intIndex=p_strSubXml.IndexOf(" ");
//			sbdTemp.Insert(intIndex,"   LastModifyDate =  '"+strCurrentTime+"'   ");
//			p_strSubXml=sbdTemp.ToString ();
//
//			sbdTemp=new StringBuilder(p_strOperationTableXML);
//			intIndex=p_strOperationTableXML.IndexOf(" ");
//			sbdTemp.Insert(intIndex,"   LastModifyDate =  '"+strCurrentTime+"'   ");
//			p_strOperationTableXML=sbdTemp.ToString ();
//
//			sbdTemp=new StringBuilder(p_strAnaesthesiaTableXML);
//			intIndex=p_strAnaesthesiaTableXML.IndexOf(" ");
//			sbdTemp.Insert(intIndex,"   LastModifyDate =  '"+strCurrentTime+"'   ");
//			p_strAnaesthesiaTableXML=sbdTemp.ToString ();
//			#endregion 

			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
 		      lngRes= objHRPServ.modify_record("OperationRecord",p_strMainXml,"INPATIENTID","INPATIENTDATE","OPENDATE"); 	
			if(lngRes==1)
				lngRes= objHRPServ.add_new_record("OperationRecordContent",p_strSubXml);
			if(lngRes==1)
				lngRes= objHRPServ.add_new_record("OperationRecord_OperationID",p_strOperationTableXML);
			if(lngRes==1)
				lngRes= objHRPServ.add_new_record("OperationRecord_Anaesthesia",p_strAnaesthesiaTableXML);
			lngRes = m_lngAddNurse(p_strNurseXMLArr);
			lngRes = m_lngAddWoundThing(p_strWoundThingTableXMLArr); 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
 
			return lngRes;
		}
		#endregion

		#region Delete
		
		[AutoComplete]
		public long m_lngDelete(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate ,string p_strDeActiveID)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngDelete");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;


			long lngRes = 0;
            clsPublicMiddleTier objHRPServ = new clsPublicMiddleTier();
			try
			{
                lngRes = objHRPServ.m_lngDeleteRecord(  "OperationRecord", p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_strDeActiveID);

 		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }
		#endregion

		#region FirstPrint
		[AutoComplete]
		public long m_lngUpdateFirstPrintDate(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,string p_strFirstPrintDate)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsOperationRecordServ","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			//更新第一次打印时间	
			string strCommand = "update operationrecord set firstprintdate=? where firstprintdate is null and status=0 and inpatientid=? and inpatientdate=? and opendate=?";

			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
			try
			{
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
				lngRes=objHRPServ.lngExecuteParameterSQL(strCommand,ref lngEff,objDPArr);		
 
		    }
			finally
			{
			  //objHRPServ.Dispose();
				
			}
			 return lngRes;		
        }		

		#endregion 

	}
}

