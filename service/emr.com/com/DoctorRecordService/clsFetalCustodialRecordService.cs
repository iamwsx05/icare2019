using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.DiseaseTrackService
{
    public class clsFetalCustodialRecordService : clsDiseaseTrackService
    {
        private const string c_strGetRecordContentSQL = @"select a.inpatientid_vchr,
       a.inpatientdate_dat,
       a.opendate_dat,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_num,
       a.confirmreason_vchr,
       a.confirmreasonxml_vchr,
       a.firstprintdate_dat,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.status_num,
       a.clinicaldiagnose_vchr,
       a.clinicaldiagnosexml_vchr,
       a.custodialindication_vchr,
       a.custodialindicationxml_vchr,
       a.ultrasonicscancue_vchr,
       a.ultrasonicscancuexml_vchr,
       a.scanertype_vchr,
       a.scanertypexml_vchr,
       a.amplitudevariation_chr,
       a.fetalheartrate_chr,
       a.periodicvariation_chr,
       a.acceleration_chr,       
       a.deceleration_chr,
       a.totalrate_vchr,
       a.suggestion_vchr,
       a.suggestionxml_vchr,
       a.oct_vchr,
       a.octxml_vchr,
       a.csf_vchr,
       a.csfxml_vchr,
       a.custodialrecord_vchr,
       a.custodialrecordxml_vchr,
       a.ostiumuteri_vchr,
       a.ostiumuterixml_vchr,
       a.parturienthour_vchr,
       a.parturienthourxml_vchr,
       a.parturientminute_vchr,
       a.parturientminutexml_vchr,
       a.signid1_vchr,       
       a.signtime1_dat,       
       a.nataltype_vchr,
       a.nataltypexml_vchr,
       a.birthprocesshour_vchr,
       a.birthprocesshourxml_vchr,
       a.birthprocessminute_vchr,
       a.birthprocessminutexml_vchr,
       a.evaluation_vchr,
       a.evaluationxml_vchr,
       a.fetalweight_vchr,
       a.fetalweightxml_vchr,
       a.fetallength_vchr,
       a.fetallengthxml_vchr,
       a.amnioticfluid_vchr,
       a.amnioticfluidxml_vchr,
       a.color_vchr,
       a.colorxml_vchr,
       a.placenta_vchr,
       a.placentaxml_vchr,
       a.umbilicalcord_vchr,
       a.umbilicalcordxml_vchr,
       a.remark_vchr,
       a.remarkxml_vchr,
       a.signid2_chr,       
       a.signtime2_dat,
       b.modifydate_dat,
       b.modifyuserid_chr,
       b.clinicaldiagnose_right_vchr,
       b.custodialindication_right_vchr,
       b.ultrasonicscancue_right_vchr,
       b.scanertype_right_vchr,
       b.suggestion_right_vchr,
       b.oct_right_vchr,
       b.csf_right_vchr,
       b.custodialrecord_right_vchr,       
       b.ostiumuteri_right_vchr,       
       b.parturienthour_right_vchr,       
       b.parturientminute_right_vchr,       
       b.birthprocesshour_right_vchr,       
       b.birthprocessminute_right_vchr,       
       b.nataltype_right_vchr,
       b.evaluation_right_vchr,
       b.fetalweight_right_vchr,
       b.fetallength_right_vchr,       
       b.amnioticfluid_right_vchr,
       b.color_right_vchr,       
       b.placenta_right_vchr,       
       b.umbilicalcord_right_vchr,
       b.signname1_vchr,
       b.remark_right_vchr,
       b.signname2_vchr
  from t_emr_fetalcustodialrecord a, t_emr_fetalrecordcontent b
  where a.inpatientid_vchr = ?
   and a.inpatientdate_dat = ?
   and a.opendate_dat = ?
   and a.status_num = 0
   and b.inpatientid_vchr = a.inpatientid_vchr
   and b.inpatientdate_dat = a.inpatientdate_dat
   and b.opendate_dat = a.opendate_dat
   and b.modifydate_dat = (select max(modifydate_dat)
                         from t_emr_fetalrecordcontent
                        where inpatientid_vchr = a.inpatientid_vchr
                          and inpatientdate_dat = a.inpatientdate_dat
                          and opendate_dat = a.opendate_dat)";

        // 从t_emr_fetalcustodialrecord获取指定病人的所有没有删除记录的时间。
        // 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
        private const string c_strGetTimeListSQL = @"select createdate_dat,opendate_dat 
													from t_emr_fetalcustodialrecord 
													where inpatientid_vchr = ?
													 and inpatientdate_dat= ?
													 and status_num=0";

        // 从t_emr_fetalcustodialrecord中获取指定时间的表单。
        // InPatientID ,InPatientDate ,CreateDate,Status = 0
        private const string c_strCheckCreateDateSQL = @"select createuserid_chr,opendate_dat
														from t_emr_fetalcustodialrecord
														where inpatientid_vchr = ? 
														and inpatientdate_dat= ?
														and createdate_dat= ? 
														and status_num=0";

        // 从t_emr_fetalrecordContent获取指定表单的最后修改时间。
        private const string c_strCheckLastModifyRecordSQL = @"select b.modifydate_dat,b.modifyuserid_chr 
															from t_emr_fetalcustodialrecord a,t_emr_fetalrecordcontent b 
															where a.inpatientid_vchr = ?
															and a.inpatientdate_dat= ?
															and a.opendate_dat= ?
															and a.status_num=0
                              and b.inpatientid_vchr=a.inpatientid_vchr
                              and b.inpatientdate_dat=a.inpatientdate_dat 
                              and b.opendate_dat=a.opendate_dat and
                              b.modifydate_dat=(select max(modifydate_dat) from t_emr_fetalrecordcontent 
                              where inpatientid_vchr=a.inpatientid_vchr
                              and inpatientdate_dat=a.inpatientdate_dat 
                              and opendate_dat=a.opendate_dat)";
        // 从t_emr_fetalcustodialrecord获取删除表单的主要信息。
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate_dat,deactivedoperatorid_chr 
                            from t_emr_fetalcustodialrecord 
                            where inpatientid_vchr = ?
                            and inpatientdate_dat= ?
                            and opendate_dat= ? 
                            and status_num=1";


        // 添加记录到t_emr_fetalcustodialrecord
        private const string c_strAddNewRecordSQL = @"insert into  t_emr_fetalcustodialrecord
													(inpatientid_vchr,inpatientdate_dat,opendate_dat,createdate_dat,createuserid_chr,ifconfirm_num,confirmreason_vchr,confirmreasonxml_vchr,status_num,clinicaldiagnose_vchr,clinicaldiagnosexml_vchr,custodialindication_vchr,custodialindicationxml_vchr,ultrasonicscancue_vchr,ultrasonicscancuexml_vchr, scanertype_vchr, scanertypexml_vchr, fetalheartrate_chr,amplitudevariation_chr, periodicvariation_chr,      deceleration_chr,acceleration_chr,totalrate_vchr,suggestion_vchr,suggestionxml_vchr,oct_vchr, octxml_vchr,   csf_vchr, csfxml_vchr, custodialrecord_vchr, custodialrecordxml_vchr,parturienthour_vchr, parturienthourxml_vchr,parturientminute_vchr, parturientminutexml_vchr,ostiumuteri_vchr, ostiumuterixml_vchr,signid1_vchr,     signtime1_dat,nataltype_vchr, nataltypexml_vchr,birthprocesshour_vchr,birthprocesshourxml_vchr,birthprocessminute_vchr,birthprocessminutexml_vchr,evaluation_vchr,evaluationxml_vchr,fetalweight_vchr,fetalweightxml_vchr,fetallength_vchr,fetallengthxml_vchr, amnioticfluid_vchr, amnioticfluidxml_vchr, color_vchr, colorxml_vchr, placenta_vchr,placentaxml_vchr,umbilicalcord_vchr,umbilicalcordxml_vchr,remark_vchr,remarkxml_vchr,signid2_chr,signtime2_dat) 
													values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        // 添加记录到t_emr_fetalrecordContent
        private const string c_strAddNewRecordContentSQL = @"insert into  t_emr_fetalrecordcontent
														(inpatientid_vchr, inpatientdate_dat,opendate_dat,modifydate_dat,
       modifyuserid_chr,clinicaldiagnose_right_vchr,custodialindication_right_vchr,ultrasonicscancue_right_vchr,scanertype_right_vchr, fetalheartrate_chr,amplitudevariation_chr,periodicvariation_chr,acceleration_chr,deceleration_chr,totalrate_vchr,  suggestion_right_vchr,oct_right_vchr,csf_right_vchr,custodialrecord_right_vchr,parturienthour_right_vchr,
parturientminute_right_vchr,ostiumuteri_right_vchr,  signid1_chr,signname1_vchr, signtime1_dat, nataltype_right_vchr,birthprocesshour_right_vchr,birthprocessminute_right_vchr,evaluation_right_vchr,fetalweight_right_vchr, fetallength_right_vchr,amnioticfluid_right_vchr, color_right_vchr,placenta_right_vchr,umbilicalcord_right_vchr,remark_right_vchr,signid2_chr,
       signname2_vchr,signtime2_dat) 
														values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 修改记录到t_emr_fetalcustodialrecord
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_fetalcustodialrecord 
													set clinicaldiagnose_vchr=?,clinicaldiagnosexml_vchr=?,custodialindication_vchr=?,custodialindicationxml_vchr=?, ultrasonicscancue_vchr=?,ultrasonicscancuexml_vchr=?, scanertype_vchr=?, scanertypexml_vchr=?,fetalheartrate_chr=?,amplitudevariation_chr=?, periodicvariation_chr=?, deceleration_chr=?,acceleration_chr=?,totalrate_vchr=?,suggestion_vchr=?,suggestionxml_vchr=?,oct_vchr=?,octxml_vchr=?,csf_vchr=?,csfxml_vchr=?,custodialrecord_vchr=?, custodialrecordxml_vchr=?,parturienthour_vchr=?,parturienthourxml_vchr=?,parturientminute_vchr=?,parturientminutexml_vchr=?,ostiumuteri_vchr=?,ostiumuterixml_vchr=?,signid1_vchr=?,  signtime1_dat=?,nataltype_vchr=?, nataltypexml_vchr=?, birthprocesshour_vchr=?,birthprocesshourxml_vchr=?,birthprocessminute_vchr=?,birthprocessminutexml_vchr=?,evaluation_vchr=?,evaluationxml_vchr=?,fetalweight_vchr=?,fetalweightxml_vchr=?,fetallength_vchr=?,fetallengthxml_vchr=?,amnioticfluid_vchr=?, amnioticfluidxml_vchr=?,color_vchr=?, colorxml_vchr=?, placenta_vchr=?,placentaxml_vchr=?,umbilicalcord_vchr=?,umbilicalcordxml_vchr=?,remark_vchr=?,remarkxml_vchr=?,signid2_chr=?,signtime2_dat=?
													where inpatientid_vchr=? 
													and inpatientdate_dat=? 
													and opendate_dat=?
													and status_num=0";


        // 修改记录到t_emr_fetalrecordContent
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        // 设置t_emr_fetalcustodialrecord中删除记录的信息
        private const string c_strDeleteRecordSQL = @"update t_emr_fetalcustodialrecord 
                          set status_num=1,deactiveddate_dat=?,deactivedoperatorid_chr=? 
                          where inpatientid_vchr=? 
                          and inpatientdate_dat=?
                          and opendate_dat=? 
                          and status_num=0";


        // 从t_emr_fetalcustodialrecord和t_emr_fetalrecordContent获取LastModifyDate和FirstPrintDate
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate_dat,b.modifydate_dat
                                  from t_emr_fetalcustodialrecord a,t_emr_fetalrecordcontent b 
                                  where a.inpatientid_vchr = ?
                                  and a.inpatientdate_dat= ?
                                  and a.opendate_dat= ? 
                                  and a.status_num=0
                                  and b.inpatientid_vchr=a.inpatientid_vchr 
                                  and b.inpatientdate_dat=a.inpatientdate_dat 
                                  and b.opendate_dat=a.opendate_dat 
                                  and b.modifydate_dat=(select max(modifydate_dat) 
                                  from t_emr_fetalrecordcontent 
                                  where inpatientid_vchr=a.inpatientid_vchr 
                                  and inpatientdate_dat=a.inpatientdate_dat 
                                  and opendate_dat=a.opendate_dat)";



        // 更新t_emr_fetalcustodialrecord中FirstPrintDate
        private const string c_strUpdateFirstPrintDateSQL = @"update  t_emr_fetalcustodialrecord 
															set firstprintdate_dat= ? 
															where inpatientid_vchr= ? 
															and inpatientdate_dat= ? 
															and opendate_dat=? 
															and firstprintdate_dat is null 
															and status_num=0";


        // 从t_emr_fetalcustodialrecord获取指定病人的所有指定删除者删除的记录时间。
        // 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate_dat,opendate_dat 
																from t_emr_fetalcustodialrecord 
																where inpatientid_vchr = ? 
																and inpatientdate_dat= ? 
																and deactivedoperatorid_chr= ? 
																and status_num=1";


        // 从t_emr_fetalcustodialrecord获取指定病人的所有已经删除的记录时间。
        // 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate_dat,opendate_dat 
																from t_emr_fetalcustodialrecord 
																where inpatientid_vchr = ? 
																and inpatientdate_dat= ? and status_num=1";


        // 在出院记录所有表中获取指定表单的信息。
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid_vchr,
       a.inpatientdate_dat,
       a.opendate_dat,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_num,
       a.confirmreason_vchr,
       a.confirmreasonxml_vchr,
       a.firstprintdate_dat,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.status_num,
       a.clinicaldiagnose_vchr,
       a.clinicaldiagnosexml_vchr,
       a.custodialindication_vchr,
       a.custodialindicationxml_vchr,
       a.ultrasonicscancue_vchr,
       a.ultrasonicscancuexml_vchr,
       a.scanertype_vchr,
       a.scanertypexml_vchr,
       a.amplitudevariation_chr,
       a.fetalheartrate_chr,
       a.periodicvariation_chr,
       a.acceleration_chr,       
       a.deceleration_chr,
       a.totalrate_vchr,
       a.suggestion_vchr,
       a.suggestionxml_vchr,
       a.oct_vchr,
       a.octxml_vchr,
       a.csf_vchr,
       a.csfxml_vchr,
       a.custodialrecord_vchr,
       a.custodialrecordxml_vchr,
       a.ostiumuteri_vchr,
       a.ostiumuterixml_vchr,
       a.parturienthour_vchr,
       a.parturienthourxml_vchr,
       a.parturientminute_vchr,
       a.parturientminutexml_vchr,
       a.signid1_vchr,       
       a.signtime1_dat,       
       a.nataltype_vchr,
       a.nataltypexml_vchr,
       a.birthprocesshour_vchr,
       a.birthprocesshourxml_vchr,
       a.birthprocessminute_vchr,
       a.birthprocessminutexml_vchr,
       a.evaluation_vchr,
       a.evaluationxml_vchr,
       a.fetalweight_vchr,
       a.fetalweightxml_vchr,
       a.fetallength_vchr,
       a.fetallengthxml_vchr,
       a.amnioticfluid_vchr,
       a.amnioticfluidxml_vchr,
       a.color_vchr,
       a.colorxml_vchr,
       a.placenta_vchr,
       a.placentaxml_vchr,
       a.umbilicalcord_vchr,
       a.umbilicalcordxml_vchr,
       a.remark_vchr,
       a.remarkxml_vchr,
       a.signid2_chr,       
       a.signtime2_dat,
       b.modifydate_dat,
       b.modifyuserid_chr,
       b.clinicaldiagnose_right_vchr,
       b.custodialindication_right_vchr,
       b.ultrasonicscancue_right_vchr,
       b.scanertype_right_vchr,
       b.suggestion_right_vchr,
       b.custodialrecord_right_vchr,       
       b.ostiumuteri_right_vchr,       
       b.parturienthour_right_vchr,       
       b.parturientminute_right_vchr,       
       b.birthprocesshour_right_vchr,       
       b.birthprocessminute_right_vchr,       
       b.nataltype_right_vchr,
       b.evaluation_right_vchr,
       b.fetalweight_right_vchr,
       b.fetallength_right_vchr,       
       b.amnioticfluid_right_vchr,
       b.color_right_vchr,       
       b.placenta_right_vchr,       
       b.umbilicalcord_right_vchr,
       b.signname1_vchr,
       b.remark_right_vchr,
       b.signname2_vchr
  from t_emr_fetalcustodialrecord a, t_emr_fetalcustodialrecordcontent b
 where a.inpatientid_vchr = ?
   and a.inpatientdate_dat = ?
   and a.opendate_dat = ?
   and a.status_num = 1
   and b.inpatientid_vchr = a.inpatientid_vchr
   and b.inpatientdate_dat = a.inpatientdate_dat
   and b.opendate_dat = a.opendate_dat
   and b.modifydate_dat = (select max(modifydate_adt)
                         from t_emr_fetalrecordcontent
                        where inpatientid_vchr = a.inpatientid_vchr
                          and inpatientdate_dat = a.inpatientdate_dat
                          and opendate_dat = a.opendate_dat)";

        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent, clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                clsFetalCustodialRecordContent objContent = (clsFetalCustodialRecordContent)p_objRecordContent;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(63, out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = objContent.m_bytIfConfirm;
                if (objContent.m_strConfirmReason == null)
                    objDPArr[6].Value = DBNull.Value;
                else
                    objDPArr[6].Value = objContent.m_strConfirmReason;
                if (objContent.m_strConfirmReasonXML == null)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = objContent.m_strConfirmReasonXML;
                objDPArr[8].Value = 0;

                objDPArr[9].Value = objContent.m_strClinicalDiagnose;
                objDPArr[10].Value = objContent.m_strClinicalDiagnoseXml;
                objDPArr[11].Value = objContent.m_strCustodialIndication;
                objDPArr[12].Value = objContent.m_strCustodialIndicationXml;
                objDPArr[13].Value = objContent.m_strUltraSonicscanCue;
                objDPArr[14].Value = objContent.m_strUltraSonicscanCueXml;
                objDPArr[15].Value = objContent.m_strUltraSonicscanerType;
                objDPArr[16].Value = objContent.m_strUltraSonicscanerTypeXml;
                objDPArr[17].Value = objContent.m_strFetalHeartRate;
                objDPArr[18].Value = objContent.m_strAmplitudeVariation;
                objDPArr[19].Value = objContent.m_strPeriodicVariation;
                objDPArr[20].Value = objContent.m_strAccerleration;
                objDPArr[21].Value = objContent.m_strDecerleration;
                objDPArr[22].Value = objContent.m_strTotalRate;
                objDPArr[23].Value = objContent.m_strManagementSuggestion;
                objDPArr[24].Value = objContent.m_strManagementSuggestionXml;
                objDPArr[25].Value = objContent.m_strOCT;
                objDPArr[26].Value = objContent.m_strOCTXml;
                objDPArr[27].Value = objContent.m_strCSF;
                objDPArr[28].Value = objContent.m_strCSFXml;
                objDPArr[29].Value = objContent.m_strCustodialRecord;
                objDPArr[30].Value = objContent.m_strCustodialRecordXml;
                objDPArr[31].Value = objContent.m_strAfterParturientHour;
                objDPArr[32].Value = objContent.m_strAfterParturientHourXml;
                objDPArr[33].Value = objContent.m_strAfterParturientMinute;
                objDPArr[34].Value = objContent.m_strAfterParturientMinuteXml;
                objDPArr[35].Value = objContent.m_strOstiumUteri;
                objDPArr[36].Value = objContent.m_strOstiumUteriXml;
                objDPArr[37].Value = objContent.m_strSignID1;
                objDPArr[38].Value = objContent.m_dtmSignTime1;
                objDPArr[38].DbType = DbType.DateTime;
                objDPArr[39].Value = objContent.m_strNatalType;
                objDPArr[40].Value = objContent.m_strNatalTypeXml;
                objDPArr[41].Value = objContent.m_strBirthProcessHour;
                objDPArr[42].Value = objContent.m_strBirthProcessHourXml;
                objDPArr[43].Value = objContent.m_strBirthProcessMinute;
                objDPArr[44].Value = objContent.m_strBirthProcessMinuteXml;
                objDPArr[45].Value = objContent.m_strEvaluation;
                objDPArr[46].Value = objContent.m_strEvaluationXml;
                objDPArr[47].Value = objContent.m_strFetalWeight;
                objDPArr[48].Value = objContent.m_strFetalWeightXml;
                objDPArr[49].Value = objContent.m_strFetalLength;
                objDPArr[50].Value = objContent.m_strFetalLengthXml;
                objDPArr[51].Value = objContent.m_strAmnioticFluid;
                objDPArr[52].Value = objContent.m_strAmnioticFluidXml;
                objDPArr[53].Value = objContent.m_strColor;
                objDPArr[54].Value = objContent.m_strColorXml;
                objDPArr[55].Value = objContent.m_strPlacenta;
                objDPArr[56].Value = objContent.m_strPlacentaXml;
                objDPArr[57].Value = objContent.m_strUmbilicalcord;
                objDPArr[58].Value = objContent.m_strUmbilicalcordXml;
                objDPArr[59].Value = objContent.m_strRemark;
                objDPArr[60].Value = objContent.m_strRemarkXml;
                objDPArr[61].Value = objContent.m_strSignID2;
                objDPArr[62].Value = objContent.m_dtmSignTime2;
                objDPArr[62].DbType = DbType.DateTime;
                

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(39, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strClinicalDiagnoseRight;
                objDPArr2[6].Value = objContent.m_strCustodialIndicationRight;
                objDPArr2[7].Value = objContent.m_strUltraSonicscanCueRight;                
                objDPArr2[8].Value = objContent.m_strUltraSonicscanerTypeRight;
                objDPArr2[9].Value = objContent.m_strFetalHeartRate;
                objDPArr2[10].Value = objContent.m_strAmplitudeVariation;
                objDPArr2[11].Value = objContent.m_strPeriodicVariation;
                objDPArr2[12].Value = objContent.m_strAccerleration;
                objDPArr2[13].Value = objContent.m_strDecerleration;
                objDPArr2[14].Value = objContent.m_strTotalRate;
                objDPArr2[15].Value = objContent.m_strManagementSuggestionRight;                
                objDPArr2[16].Value = objContent.m_strOCTRight;
                objDPArr2[17].Value = objContent.m_strCSFRight;
                objDPArr2[18].Value = objContent.m_strCustodialRecordRight;
                objDPArr2[19].Value = objContent.m_strAfterParturientHourRight;
                objDPArr2[20].Value = objContent.m_strAfterParturientMinuteRight;
                objDPArr2[21].Value = objContent.m_strOstiumUteriRight;
                objDPArr2[22].Value = objContent.m_strSignID1;
                objDPArr2[23].Value = objContent.m_strSignName1;
                objDPArr2[24].Value = objContent.m_dtmSignTime1;
                objDPArr2[24].DbType = DbType.DateTime;
                objDPArr2[25].Value = objContent.m_strNatalTypeRight;
                objDPArr2[26].Value = objContent.m_strBirthProcessHourRight;
                objDPArr2[27].Value = objContent.m_strBirthProcessMinuteRight;
                objDPArr2[28].Value = objContent.m_strEvaluationRight;
                objDPArr2[29].Value = objContent.m_strFetalWeightRight;
                objDPArr2[30].Value = objContent.m_strFetalLengthRight;
                objDPArr2[31].Value = objContent.m_strAmnioticFluidRight;
                objDPArr2[32].Value = objContent.m_strColorRight;
                objDPArr2[33].Value = objContent.m_strPlacentaRight;
                objDPArr2[34].Value = objContent.m_strUmbilicalcordRight;
                objDPArr2[35].Value = objContent.m_strRemarkRight;                
                objDPArr2[36].Value = objContent.m_strSignID2;
                objDPArr2[37].Value = objContent.m_strSignName2;
                objDPArr2[38].Value = objContent.m_dtmSignTime2;
                objDPArr2[38].DbType = DbType.DateTime;                

                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);


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

        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent, clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

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
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

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

        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent, clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                clsFetalCustodialRecordContent objContent = (clsFetalCustodialRecordContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(57, out objDPArr);
                objDPArr[0].Value = objContent.m_strClinicalDiagnose;
                objDPArr[1].Value = objContent.m_strClinicalDiagnoseXml;
                objDPArr[2].Value = objContent.m_strCustodialIndication;
                objDPArr[3].Value = objContent.m_strCustodialIndicationXml;
                objDPArr[4].Value = objContent.m_strUltraSonicscanCue;
                objDPArr[5].Value = objContent.m_strUltraSonicscanCueXml;
                objDPArr[6].Value = objContent.m_strUltraSonicscanerType;
                objDPArr[7].Value = objContent.m_strUltraSonicscanerTypeXml;
                objDPArr[8].Value = objContent.m_strFetalHeartRate;
                objDPArr[9].Value = objContent.m_strAmplitudeVariation;
                objDPArr[10].Value = objContent.m_strPeriodicVariation;
                objDPArr[11].Value = objContent.m_strAccerleration;
                objDPArr[12].Value = objContent.m_strDecerleration;
                objDPArr[13].Value = objContent.m_strTotalRate;
                objDPArr[14].Value = objContent.m_strManagementSuggestion;
                objDPArr[15].Value = objContent.m_strManagementSuggestionXml;
                objDPArr[16].Value = objContent.m_strOCT;
                objDPArr[17].Value = objContent.m_strOCTXml;
                objDPArr[18].Value = objContent.m_strCSF;
                objDPArr[19].Value = objContent.m_strCSFXml;
                objDPArr[20].Value = objContent.m_strCustodialRecord;
                objDPArr[21].Value = objContent.m_strCustodialRecordXml;
                objDPArr[22].Value = objContent.m_strAfterParturientHour;
                objDPArr[23].Value = objContent.m_strAfterParturientHourXml;
                objDPArr[24].Value = objContent.m_strAfterParturientMinute;
                objDPArr[25].Value = objContent.m_strAfterParturientMinuteXml;
                objDPArr[26].Value = objContent.m_strOstiumUteri;
                objDPArr[27].Value = objContent.m_strOstiumUteriXml;
                objDPArr[28].Value = objContent.m_strSignID1;
                objDPArr[29].Value = objContent.m_dtmSignTime1;
                objDPArr[29].DbType = DbType.DateTime;
                objDPArr[30].Value = objContent.m_strNatalType;
                objDPArr[31].Value = objContent.m_strNatalTypeXml;
                objDPArr[32].Value = objContent.m_strBirthProcessHour;
                objDPArr[33].Value = objContent.m_strBirthProcessHourXml;
                objDPArr[34].Value = objContent.m_strBirthProcessMinute;
                objDPArr[35].Value = objContent.m_strBirthProcessMinuteXml;
                objDPArr[36].Value = objContent.m_strEvaluation;
                objDPArr[37].Value = objContent.m_strEvaluationXml;
                objDPArr[38].Value = objContent.m_strFetalWeight;
                objDPArr[39].Value = objContent.m_strFetalWeightXml;
                objDPArr[40].Value = objContent.m_strFetalLength;
                objDPArr[41].Value = objContent.m_strFetalLengthXml;
                objDPArr[42].Value = objContent.m_strAmnioticFluid;
                objDPArr[43].Value = objContent.m_strAmnioticFluidXml;
                objDPArr[44].Value = objContent.m_strColor;
                objDPArr[45].Value = objContent.m_strColorXml;
                objDPArr[46].Value = objContent.m_strPlacenta;
                objDPArr[47].Value = objContent.m_strPlacentaXml;
                objDPArr[48].Value = objContent.m_strUmbilicalcord;
                objDPArr[49].Value = objContent.m_strUmbilicalcordXml;
                objDPArr[50].Value = objContent.m_strRemark;
                objDPArr[51].Value = objContent.m_strRemarkXml;
                objDPArr[52].Value = objContent.m_strSignID2;
                objDPArr[53].Value = objContent.m_dtmSignTime2;
                objDPArr[53].DbType = DbType.DateTime;
                objDPArr[54].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[55].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[56].Value = p_objRecordContent.m_dtmOpenDate;


                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(39, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_strClinicalDiagnoseRight;
                objDPArr2[6].Value = objContent.m_strCustodialIndicationRight;
                objDPArr2[7].Value = objContent.m_strUltraSonicscanCueRight;
                objDPArr2[8].Value = objContent.m_strUltraSonicscanerTypeRight;
                objDPArr2[9].Value = objContent.m_strFetalHeartRate;
                objDPArr2[10].Value = objContent.m_strAmplitudeVariation;
                objDPArr2[11].Value = objContent.m_strPeriodicVariation;
                objDPArr2[12].Value = objContent.m_strAccerleration;
                objDPArr2[13].Value = objContent.m_strDecerleration;
                objDPArr2[14].Value = objContent.m_strTotalRate;
                objDPArr2[15].Value = objContent.m_strManagementSuggestionRight;
                objDPArr2[16].Value = objContent.m_strOCTRight;
                objDPArr2[17].Value = objContent.m_strCSFRight;
                objDPArr2[18].Value = objContent.m_strCustodialRecordRight;
                objDPArr2[19].Value = objContent.m_strAfterParturientHourRight;
                objDPArr2[20].Value = objContent.m_strAfterParturientMinuteRight;
                objDPArr2[21].Value = objContent.m_strOstiumUteriRight;
                objDPArr2[22].Value = objContent.m_strSignID1;
                objDPArr2[23].Value = objContent.m_strSignName1;
                objDPArr2[24].Value = objContent.m_dtmSignTime1;
                objDPArr2[24].DbType = DbType.DateTime;
                objDPArr2[25].Value = objContent.m_strNatalTypeRight;
                objDPArr2[26].Value = objContent.m_strBirthProcessHourRight;
                objDPArr2[27].Value = objContent.m_strBirthProcessMinuteRight;
                objDPArr2[28].Value = objContent.m_strEvaluationRight;
                objDPArr2[29].Value = objContent.m_strFetalWeightRight;
                objDPArr2[30].Value = objContent.m_strFetalLengthRight;
                objDPArr2[31].Value = objContent.m_strAmnioticFluidRight;
                objDPArr2[32].Value = objContent.m_strColorRight;
                objDPArr2[33].Value = objContent.m_strPlacentaRight;
                objDPArr2[34].Value = objContent.m_strUmbilicalcordRight;
                objDPArr2[35].Value = objContent.m_strRemarkRight;
                objDPArr2[36].Value = objContent.m_strSignID2;
                objDPArr2[37].Value = objContent.m_strSignName2;
                objDPArr2[38].Value = objContent.m_dtmSignTime2;
                objDPArr2[38].DbType = DbType.DateTime; 


                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);


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

        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID_VCHR"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE_DAT"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
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

        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent, clsHRPTableService p_objHRPServ, out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    //string strSQL2 = "select DeActivedDate,DeActivedOperatorID from OutHospitalRecord Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=1 ";
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE_DAT"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID_CHR"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE_DAT"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
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

        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, clsHRPTableService p_objHRPServ, out DateTime p_dtmModifyDate, out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.Now;
            p_strFirstPrintDate = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE_DAT"].ToString());
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

            }			//返回
            return lngRes;
        }


        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate, clsHRPTableService p_objHRPServ, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    clsFetalCustodialRecordContent objContent = new clsFetalCustodialRecordContent();
                    objContent.m_strInPatientID = p_strInPatientID;
                    objContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE_DAT"].ToString());
                    objContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE_DAT"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString() == "")
                        objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString());
                    objContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID_CHR"].ToString();
                    objContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID_CHR"].ToString();
                    if (dtbValue.Rows[0]["IFCONFIRM_NUM"].ToString() == "")
                        objContent.m_bytIfConfirm = 0;
                    else objContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[0]["IFCONFIRM_NUM"].ToString());
                    if (dtbValue.Rows[0]["STATUS_NUM"].ToString() == "")
                        objContent.m_bytStatus = 0;
                    else objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS_NUM"].ToString());
                    
                    objContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON_VCHR"].ToString();
                    objContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML_VCHR"].ToString();
                    objContent.m_strClinicalDiagnose = dtbValue.Rows[0]["CLINICALDIAGNOSE_VCHR"].ToString();
                    objContent.m_strClinicalDiagnoseRight = dtbValue.Rows[0]["CLINICALDIAGNOSE_RIGHT_VCHR"].ToString();
                    objContent.m_strClinicalDiagnoseXml = dtbValue.Rows[0]["CONFIRMREASONXML_VCHR"].ToString();
                    objContent.m_strCustodialIndication = dtbValue.Rows[0]["CUSTODIALINDICATION_VCHR"].ToString();
                    objContent.m_strCustodialIndicationRight = dtbValue.Rows[0]["CUSTODIALINDICATION_RIGHT_VCHR"].ToString();
                    objContent.m_strCustodialIndicationXml = dtbValue.Rows[0]["CUSTODIALINDICATIONXML_VCHR"].ToString();
                    objContent.m_strUltraSonicscanCue = dtbValue.Rows[0]["ULTRASONICSCANCUE_VCHR"].ToString();
                    objContent.m_strUltraSonicscanCueRight = dtbValue.Rows[0]["ULTRASONICSCANCUE_RIGHT_VCHR"].ToString();
                    objContent.m_strUltraSonicscanCueXml = dtbValue.Rows[0]["ULTRASONICSCANCUEXML_VCHR"].ToString();
                    objContent.m_strUltraSonicscanerType = dtbValue.Rows[0]["SCANERTYPE_VCHR"].ToString();
                    objContent.m_strUltraSonicscanerTypeRight = dtbValue.Rows[0]["SCANERTYPE_RIGHT_VCHR"].ToString(); ;
                    objContent.m_strUltraSonicscanerTypeXml = dtbValue.Rows[0]["SCANERTYPEXML_VCHR"].ToString(); ;
                    objContent.m_strFetalHeartRate = dtbValue.Rows[0]["FETALHEARTRATE_CHR"].ToString();
                    objContent.m_strAmplitudeVariation = dtbValue.Rows[0]["AMPLITUDEVARIATION_CHR"].ToString();
                    objContent.m_strPeriodicVariation = dtbValue.Rows[0]["PERIODICVARIATION_CHR"].ToString();
                    objContent.m_strAccerleration = dtbValue.Rows[0]["ACCELERATION_CHR"].ToString();
                    objContent.m_strDecerleration = dtbValue.Rows[0]["DECELERATION_CHR"].ToString();
                    objContent.m_strTotalRate = dtbValue.Rows[0]["TOTALRATE_VCHR"].ToString();
                    objContent.m_strManagementSuggestion = dtbValue.Rows[0]["SUGGESTION_VCHR"].ToString();
                    objContent.m_strManagementSuggestionRight = dtbValue.Rows[0]["SUGGESTION_RIGHT_VCHR"].ToString();
                    objContent.m_strManagementSuggestionXml = dtbValue.Rows[0]["SUGGESTIONXML_VCHR"].ToString();
                    objContent.m_strOCT = dtbValue.Rows[0]["OCT_VCHR"].ToString();
                    objContent.m_strOCTRight = dtbValue.Rows[0]["OCT_RIGHT_VCHR"].ToString();
                    objContent.m_strOCTXml = dtbValue.Rows[0]["OCTXML_VCHR"].ToString();
                    objContent.m_strCSF = dtbValue.Rows[0]["CSF_VCHR"].ToString();
                    objContent.m_strCSFRight = dtbValue.Rows[0]["CSF_RIGHT_VCHR"].ToString();
                    objContent.m_strCSFXml = dtbValue.Rows[0]["CSFXML_VCHR"].ToString();
                    objContent.m_strCustodialRecord = dtbValue.Rows[0]["CUSTODIALRECORD_VCHR"].ToString();
                    objContent.m_strCustodialRecordRight = dtbValue.Rows[0]["CUSTODIALRECORD_RIGHT_VCHR"].ToString();
                    objContent.m_strCustodialRecordXml = dtbValue.Rows[0]["CUSTODIALRECORDXML_VCHR"].ToString(); 
                    objContent.m_strAfterParturientHour = dtbValue.Rows[0]["PARTURIENTHOUR_VCHR"].ToString();
                    objContent.m_strAfterParturientHourRight = dtbValue.Rows[0]["PARTURIENTHOUR_RIGHT_VCHR"].ToString();
                    objContent.m_strAfterParturientHourXml = dtbValue.Rows[0]["PARTURIENTHOURXML_VCHR"].ToString();
                    objContent.m_strAfterParturientMinute = dtbValue.Rows[0]["PARTURIENTMINUTE_VCHR"].ToString();
                    objContent.m_strAfterParturientMinuteRight = dtbValue.Rows[0]["PARTURIENTMINUTE_RIGHT_VCHR"].ToString();
                    objContent.m_strAfterParturientMinuteXml = dtbValue.Rows[0]["PARTURIENTMINUTEXML_VCHR"].ToString();
                    objContent.m_strOstiumUteri = dtbValue.Rows[0]["OSTIUMUTERI_VCHR"].ToString();
                    objContent.m_strOstiumUteriRight = dtbValue.Rows[0]["OSTIUMUTERI_RIGHT_VCHR"].ToString();
                    objContent.m_strOstiumUteriXml = dtbValue.Rows[0]["OSTIUMUTERIXML_VCHR"].ToString();
                    objContent.m_strSignID1 = dtbValue.Rows[0]["SIGNID1_VCHR"].ToString();
                    objContent.m_strSignName1 = dtbValue.Rows[0]["SIGNNAME1_VCHR"].ToString();
                    objContent.m_dtmSignTime1 = (DateTime)dtbValue.Rows[0]["SIGNTIME1_DAT"];
                    objContent.m_strNatalType = dtbValue.Rows[0]["NATALTYPE_VCHR"].ToString();
                    objContent.m_strNatalTypeRight = dtbValue.Rows[0]["NATALTYPE_RIGHT_VCHR"].ToString();
                    objContent.m_strNatalTypeXml = dtbValue.Rows[0]["NATALTYPEXML_VCHR"].ToString();
                    objContent.m_strBirthProcessHour = dtbValue.Rows[0]["BIRTHPROCESSHOUR_VCHR"].ToString();
                    objContent.m_strBirthProcessHourRight = dtbValue.Rows[0]["BIRTHPROCESSHOUR_RIGHT_VCHR"].ToString();
                    objContent.m_strBirthProcessHourXml = dtbValue.Rows[0]["BIRTHPROCESSHOURXML_VCHR"].ToString();
                    objContent.m_strBirthProcessMinute = dtbValue.Rows[0]["BIRTHPROCESSMINUTE_VCHR"].ToString();
                    objContent.m_strBirthProcessMinuteRight = dtbValue.Rows[0]["BIRTHPROCESSMINUTE_RIGHT_VCHR"].ToString();
                    objContent.m_strBirthProcessMinuteXml = dtbValue.Rows[0]["BIRTHPROCESSMINUTEXML_VCHR"].ToString();
                    objContent.m_strEvaluation = dtbValue.Rows[0]["EVALUATION_VCHR"].ToString();
                    objContent.m_strEvaluationRight = dtbValue.Rows[0]["EVALUATION_RIGHT_VCHR"].ToString();
                    objContent.m_strEvaluationXml = dtbValue.Rows[0]["EVALUATIONXML_VCHR"].ToString();
                    objContent.m_strFetalWeight = dtbValue.Rows[0]["FETALWEIGHT_VCHR"].ToString();
                    objContent.m_strFetalWeightRight = dtbValue.Rows[0]["FETALWEIGHT_RIGHT_VCHR"].ToString();
                    objContent.m_strFetalWeightXml = dtbValue.Rows[0]["FETALWEIGHTXML_VCHR"].ToString();
                    objContent.m_strFetalLength = dtbValue.Rows[0]["FETALLENGTH_VCHR"].ToString();
                    objContent.m_strFetalLengthRight = dtbValue.Rows[0]["FETALLENGTH_RIGHT_VCHR"].ToString();
                    objContent.m_strFetalLengthXml = dtbValue.Rows[0]["FETALLENGTHXML_VCHR"].ToString();
                    objContent.m_strAmnioticFluid = dtbValue.Rows[0]["AMNIOTICFLUID_VCHR"].ToString();
                    objContent.m_strAmnioticFluidRight = dtbValue.Rows[0]["AMNIOTICFLUID_RIGHT_VCHR"].ToString();
                    objContent.m_strAmnioticFluidXml = dtbValue.Rows[0]["AMNIOTICFLUIDXML_VCHR"].ToString();
                    objContent.m_strColor = dtbValue.Rows[0]["COLOR_VCHR"].ToString();
                    objContent.m_strColorRight = dtbValue.Rows[0]["COLOR_RIGHT_VCHR"].ToString();
                    objContent.m_strColorXml = dtbValue.Rows[0]["COLORXML_VCHR"].ToString();
                    objContent.m_strPlacenta = dtbValue.Rows[0]["PLACENTA_VCHR"].ToString();
                    objContent.m_strPlacentaRight = dtbValue.Rows[0]["PLACENTA_RIGHT_VCHR"].ToString();
                    objContent.m_strPlacentaXml = dtbValue.Rows[0]["PLACENTAXML_VCHR"].ToString();
                    objContent.m_strUmbilicalcord = dtbValue.Rows[0]["UMBILICALCORD_VCHR"].ToString();
                    objContent.m_strUmbilicalcordRight = dtbValue.Rows[0]["UMBILICALCORD_RIGHT_VCHR"].ToString();
                    objContent.m_strUmbilicalcordXml = dtbValue.Rows[0]["UMBILICALCORDXML_VCHR"].ToString();
                    objContent.m_strRemark = dtbValue.Rows[0]["REMARK_VCHR"].ToString();
                    objContent.m_strRemarkRight = dtbValue.Rows[0]["REMARK_RIGHT_VCHR"].ToString();
                    objContent.m_strRemarkXml = dtbValue.Rows[0]["REMARKXML_VCHR"].ToString();
                    objContent.m_strSignID2 = dtbValue.Rows[0]["SIGNID2_CHR"].ToString();
                    objContent.m_strSignName2 = dtbValue.Rows[0]["SIGNNAME2_VCHR"].ToString();
                    objContent.m_dtmSignTime2 = (DateTime)dtbValue.Rows[0]["SIGNTIME2_DAT"];

                    p_objRecordContent = objContent;
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

            }
            //返回
            return lngRes;
        }

        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOutHospitalService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;


                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE_DAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                //返回


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

        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOutHospitalService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = new clsHRPTableService().lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE_DAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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

        protected override long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    clsFetalCustodialRecordContent objContent = new clsFetalCustodialRecordContent();
                    objContent.m_strInPatientID = p_strInPatientID;
                    objContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[0]["CREATEDATE_DAT"].ToString());
                    objContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE_DAT"].ToString());

                    if (dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString() == "")
                        objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE_DAT"].ToString());
                    objContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID_CHR"].ToString();
                    objContent.m_strModifyUserID = dtbValue.Rows[0]["MODIFYUSERID_CHR"].ToString();
                    if (dtbValue.Rows[0]["IFCONFIRM_NUM"].ToString() == "")
                        objContent.m_bytIfConfirm = 0;
                    else objContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[0]["IFCONFIRM_NUM"].ToString());
                    if (dtbValue.Rows[0]["STATUS_NUM"].ToString() == "")
                        objContent.m_bytStatus = 0;
                    else objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[0]["STATUS_NUM"].ToString());
                    objContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON_VCHR"].ToString();
                    objContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML_VCHR"].ToString();

                    objContent.m_strConfirmReason = dtbValue.Rows[0]["CONFIRMREASON_VCHR"].ToString();
                    objContent.m_strConfirmReasonXML = dtbValue.Rows[0]["CONFIRMREASONXML_VCHR"].ToString();
                    objContent.m_strClinicalDiagnose = dtbValue.Rows[0]["CLINICALDIAGNOSE_VCHR"].ToString();
                    objContent.m_strClinicalDiagnoseRight = dtbValue.Rows[0]["CLINICALDIAGNOSE_RIGHT_VCHR"].ToString();
                    objContent.m_strClinicalDiagnoseXml = dtbValue.Rows[0]["CONFIRMREASONXML_VCHR"].ToString();
                    objContent.m_strCustodialIndication = dtbValue.Rows[0]["CUSTODIALINDICATION_VCHR"].ToString();
                    objContent.m_strCustodialIndicationRight = dtbValue.Rows[0]["CUSTODIALINDICATION_RIGHT_VCHR"].ToString();
                    objContent.m_strCustodialIndicationXml = dtbValue.Rows[0]["CUSTODIALINDICATIONXML_VCHR"].ToString();
                    objContent.m_strUltraSonicscanCue = dtbValue.Rows[0]["ULTRASONICSCANCUE_VCHR"].ToString();
                    objContent.m_strUltraSonicscanCueRight = dtbValue.Rows[0]["ULTRASONICSCANCUE_RIGHT_VCHR"].ToString();
                    objContent.m_strUltraSonicscanCueXml = dtbValue.Rows[0]["ULTRASONICSCANCUEXML_VCHR"].ToString();
                    objContent.m_strUltraSonicscanerType = dtbValue.Rows[0]["SCANERTYPE_VCHR"].ToString();
                    objContent.m_strUltraSonicscanerTypeRight = dtbValue.Rows[0]["SCANERTYPE_RIGHT_VCHR"].ToString(); ;
                    objContent.m_strUltraSonicscanerTypeXml = dtbValue.Rows[0]["SCANERTYPEXML_VCHR"].ToString(); ;
                    objContent.m_strFetalHeartRate = dtbValue.Rows[0]["FETALHEARTRATE_CHR"].ToString();
                    objContent.m_strAmplitudeVariation = dtbValue.Rows[0]["AMPLITUDEVARIATION_CHR"].ToString();
                    objContent.m_strPeriodicVariation = dtbValue.Rows[0]["PERIODICVARIATION_CHR"].ToString();
                    objContent.m_strAccerleration = dtbValue.Rows[0]["ACCELERATION_CHR"].ToString();
                    objContent.m_strDecerleration = dtbValue.Rows[0]["DECELERATION_CHR"].ToString();
                    objContent.m_strTotalRate = dtbValue.Rows[0]["TOTALRATE_VCHR"].ToString();
                    objContent.m_strManagementSuggestion = dtbValue.Rows[0]["SUGGESTION_VCHR"].ToString();
                    objContent.m_strManagementSuggestionRight = dtbValue.Rows[0]["SUGGESTION_RIGHT_VCHR"].ToString();
                    objContent.m_strManagementSuggestionXml = dtbValue.Rows[0]["SUGGESTIONXML_VCHR"].ToString();
                    objContent.m_strOCT = dtbValue.Rows[0]["OCT_VCHR"].ToString();
                    objContent.m_strOCTRight = dtbValue.Rows[0]["OCT_RIGHT_VCHR"].ToString();
                    objContent.m_strOCTXml = dtbValue.Rows[0]["OCTXML_VCHR"].ToString();
                    objContent.m_strCSF = dtbValue.Rows[0]["CSF_VCHR"].ToString();
                    objContent.m_strCSFRight = dtbValue.Rows[0]["CSF_RIGHT_VCHR"].ToString();
                    objContent.m_strCSFXml = dtbValue.Rows[0]["CSFXML_VCHR"].ToString();
                    objContent.m_strCustodialRecord = dtbValue.Rows[0]["CUSTODIALRECORD_VCHR"].ToString();
                    objContent.m_strCustodialRecordRight = dtbValue.Rows[0]["CUSTODIALRECORD_RIGHT_VCHR"].ToString();
                    objContent.m_strCustodialRecordXml = dtbValue.Rows[0]["CUSTODIALRECORDXML_VCHR"].ToString();
                    objContent.m_strAfterParturientHour = dtbValue.Rows[0]["PARTURIENTHOUR_VCHR"].ToString();
                    objContent.m_strAfterParturientHourRight = dtbValue.Rows[0]["PARTURIENTHOUR_RIGHT_VCHR"].ToString();
                    objContent.m_strAfterParturientHourXml = dtbValue.Rows[0]["PARTURIENTHOURXML_VCHR"].ToString();
                    objContent.m_strAfterParturientHour = dtbValue.Rows[0]["PARTURIENTMINUTE_VCHR"].ToString();
                    objContent.m_strAfterParturientMinuteRight = dtbValue.Rows[0]["PARTURIENTMINUTE_RIGHT_VCHR"].ToString();
                    objContent.m_strAfterParturientMinuteXml = dtbValue.Rows[0]["PARTURIENTMINUTEXML_VCHR"].ToString();
                    objContent.m_strOstiumUteri = dtbValue.Rows[0]["OSTIUMUTERI_VCHR"].ToString();
                    objContent.m_strOstiumUteriRight = dtbValue.Rows[0]["OSTIUMUTERI_RIGHT_VCHR"].ToString();
                    objContent.m_strOstiumUteriXml = dtbValue.Rows[0]["OSTIUMUTERIXML_VCHR"].ToString();
                    objContent.m_strSignID1 = dtbValue.Rows[0]["SIGNID1_VCHR"].ToString();
                    objContent.m_strSignName1 = dtbValue.Rows[0]["SIGNNAME1_VCHR"].ToString();
                    objContent.m_dtmSignTime1 = (DateTime)dtbValue.Rows[0]["SIGNTIME1_DAT"];
                    objContent.m_strNatalType = dtbValue.Rows[0]["NATALTYPE_VCHR"].ToString();
                    objContent.m_strNatalTypeRight = dtbValue.Rows[0]["NATALTYPE_RIGHT_VCHR"].ToString();
                    objContent.m_strNatalTypeXml = dtbValue.Rows[0]["NATALTYPEXML_VCHR"].ToString();
                    objContent.m_strBirthProcessHour = dtbValue.Rows[0]["BIRTHPROCESSHOUR_VCHR"].ToString();
                    objContent.m_strBirthProcessHourRight = dtbValue.Rows[0]["BBIRTHPROCESSHOUR_RIGHT_VCHR"].ToString();
                    objContent.m_strBirthProcessHourXml = dtbValue.Rows[0]["BIRTHPROCESSHOURXML_VCHR"].ToString();
                    objContent.m_strBirthProcessMinute = dtbValue.Rows[0]["BIRTHPROCESSMINUTE_VCHR"].ToString();
                    objContent.m_strBirthProcessMinuteRight = dtbValue.Rows[0]["BBIRTHPROCESSMINUTE_RIGHT_VCHR"].ToString();
                    objContent.m_strBirthProcessMinuteXml = dtbValue.Rows[0]["BIRTHPROCESSMINUTEXML_VCHR"].ToString();
                    objContent.m_strEvaluation = dtbValue.Rows[0]["EVALUATION_VCHR"].ToString();
                    objContent.m_strEvaluationRight = dtbValue.Rows[0]["EVALUATION_RIGHT_VCHR"].ToString();
                    objContent.m_strEvaluationXml = dtbValue.Rows[0]["EVALUATIONXML_VCHR"].ToString();
                    objContent.m_strFetalWeight = dtbValue.Rows[0]["FETALWEIGHT_VCHR"].ToString();
                    objContent.m_strFetalWeightRight = dtbValue.Rows[0]["FETALWEIGHT_RIGHT_VCHR"].ToString();
                    objContent.m_strFetalWeightXml = dtbValue.Rows[0]["FETALWEIGHTXML_VCHR"].ToString();
                    objContent.m_strFetalLength = dtbValue.Rows[0]["FETALLENGTH_VCHR"].ToString();
                    objContent.m_strFetalLengthRight = dtbValue.Rows[0]["FETALLENGTH_RIGHT_VCHR"].ToString();
                    objContent.m_strFetalLengthXml = dtbValue.Rows[0]["FETALLENGTHXML_VCHR"].ToString();
                    objContent.m_strAmnioticFluid = dtbValue.Rows[0]["AMNIOTICFLUID_VCHR"].ToString();
                    objContent.m_strAmnioticFluidRight = dtbValue.Rows[0]["AMNIOTICFLUID_RIGHT_VCHR"].ToString();
                    objContent.m_strAmnioticFluidXml = dtbValue.Rows[0]["AMNIOTICFLUIDXML_VCHR"].ToString();
                    objContent.m_strColor = dtbValue.Rows[0]["COLOR_VCHR"].ToString();
                    objContent.m_strColorRight = dtbValue.Rows[0]["COLOR_RIGHT_VCHR"].ToString();
                    objContent.m_strColorXml = dtbValue.Rows[0]["COLORXML_VCHR"].ToString();
                    objContent.m_strPlacenta = dtbValue.Rows[0]["PLACENTA_VCHR"].ToString();
                    objContent.m_strPlacentaRight = dtbValue.Rows[0]["PLACENTA_RIGHT_VCHR"].ToString();
                    objContent.m_strPlacentaXml = dtbValue.Rows[0]["PLACENTAXML_VCHR"].ToString();
                    objContent.m_strUmbilicalcord = dtbValue.Rows[0]["UMBILICALCORD_VCHR"].ToString();
                    objContent.m_strUmbilicalcordRight = dtbValue.Rows[0]["UMBILICALCORD_RIGHT_VCHR"].ToString();
                    objContent.m_strUmbilicalcordXml = dtbValue.Rows[0]["UMBILICALCORDXML_VCHR"].ToString();
                    objContent.m_strRemark = dtbValue.Rows[0]["REMARK_VCHR"].ToString();
                    objContent.m_strRemarkRight = dtbValue.Rows[0]["REMARK_RIGHT_VCHR"].ToString();
                    objContent.m_strRemarkXml = dtbValue.Rows[0]["REMARKXML_VCHR"].ToString();
                    objContent.m_strSignID2 = dtbValue.Rows[0]["SIGNID2_CHR"].ToString();
                    objContent.m_strSignName2 = dtbValue.Rows[0]["SIGNNAME2_VCHR"].ToString();
                    objContent.m_dtmSignTime2 = (DateTime)dtbValue.Rows[0]["SIGNTIME2_DAT"];

                    p_objRecordContent = objContent;
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

        /// <summary>
        /// 获取病人的该记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(  string p_strInPatientID,
                string p_strInPatientDate,
                out string[] p_strCreateDateArr,
                out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            { 

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE_DAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE_DAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                //返回


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
        /// 更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
                string p_strInPatientID,
                string p_strInPatientDate,
                string p_strOpenDate,
                DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsOutHospitalService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
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

    }
}
