using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// �ʹ���������¼
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_CesareanRecordServ : clsDiseaseTrackService
    {
        #region SQL���
        #region ��ȡָ�����˵�����û��ɾ����¼��ʱ��
        /// <summary>
        /// ��ȡָ�����˵�����û��ɾ����¼��ʱ��
        /// </summary>
        private const string c_strGetTimeListSQL = @"select createdate, opendate
                                                      from t_emr_cesareanrecord
                                                     where inpatientid = ?
                                                       and inpatientdate = ?
                                                       and status = 0";
        #endregion

        #region ����ָ��������Ϣ�����ұ�������
        /// <summary>
        /// ����ָ��������Ϣ�����ұ�������
        /// </summary>
        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.emr_seq,
       a.registerid_chr,
       a.recorddate,
       a.pregnanttimes,
       a.laytimes,
       a.opdate,
       a.diagnosisbeforeop,
       a.diagnosisbeforeopxml,
       a.opindication,
       a.opindicationxml,
       a.uterusheight,
       a.uterusheightxml,
       a.abdomenround,
       a.abdomenroundxml,
       a.presentation1,
       a.presentation1xml,
       a.linkup,
       a.fetusweight,
       a.fetusweightxml,
       a.ischialspine,
       a.coccyxradian,
       a.ischiumnotch,
       a.pubicarch,
       a.pubicarchxml,
       a.dc,
       a.dcxml,
       a.uterusora,
       a.uterusoraxml,
       a.amniocentesis1,
       a.presentation2,
       a.presentation2xml,
       a.fetusplace1,
       a.fetusplace1xml,
       a.presentationheitht,
       a.presentationheithtxml,
       a.skull,
       a.caputsuccedaneumsize,
       a.caputsuccedaneumsizexml,
       a.caputsuccedaneumplace1,
       a.caputsuccedaneumplace1xml,
       a.abdominalwall_v,
       a.abdominalwall_vxml,
       a.abdominalwall_h,
       a.abdominalwall_hxml,
       a.fascia,
       a.peritoneum,
       a.uterus,
       a.fetusplace2,
       a.fetusplace2xml,
       a.engagement,
       a.presentationexpulsion,
       a.expulsiontime,
       a.babysex,
       a.babyweight,
       a.babyweightxml,
       a.apgar,
       a.apgarxml,
       a.fetusfacies,
       a.fetusfaciesxml,
       a.caputsuccedaneumsizex,
       a.caputsuccedaneumsizexxml,
       a.caputsuccedaneumsizey,
       a.caputsuccedaneumsizeyxml,
       a.caputsuccedaneumplace2,
       a.caputsuccedaneumplace2xml,
       a.amniocentesis2,
       a.amniocentesisbulk,
       a.amniocentesisbulkxml,
       a.placentasizex,
       a.placentasizexxml,
       a.placentasizey,
       a.placentasizeyxml,
       a.placentasizez,
       a.placentasizezxml,
       a.placentasizeweight,
       a.placentasizeweightxml,
       a.placentacalcify,
       a.umbilicalcordlength,
       a.umbilicalcordlengthxml,
       a.umbilicalcordcircs,
       a.embryolemmacircs,
       a.oviductcircs,
       a.ovarycircs,
       a.sutureuterus,
       a.sutureuterusxml,
       a.sutureabdominalwall,
       a.sutureabdominalwallxml,
       a.oxytocin,
       a.oxytocinxml,
       a.othermedicine,
       a.othermedicinexml,
       a.piss,
       a.pissxml,
       a.bleeding,
       a.bleedingxml,
       a.transfuse,
       a.transfusexml,
       a.anatime,
       a.fetalheartsound,
       a.fetalheartsoundxml,
       a.bp,
       a.bpxml,
       a.placentacircsafterop,
       a.placentacircsafteropxml,
       a.diagnosisafterop,
       a.diagnosisafteropxml,
       a.anamode,
       a.anamodexml,
       a.opname,
       a.opnamexml,
       a.uncheckbeforeop,
       a.sequence_int,
       a.placenta,
       a.placentaxml,
       a.umbilicalcord,
       a.umbilicalcordxml,
       a.medicineafterop,
       a.sumary4,
       a.sumary4xml,
       a.assistant1,
       a.assistant2,
       a.optime_vchr,
       a.anatime_vchr,
       a.caputsuccedaneumsizey_yn,
       a.caputsuccedaneumsize_yn, 
       b.modifydate,
       b.modifyuserid,
       b.diagnosisbeforeop_right,
       b.opindication_right,
       b.diagnosisafterop_right,
       b.anamode_right,
       b.opname_right,
       b.uterusheight_right,
       b.abdomenround_right,
       b.presentation1_right,
       b.fetusweight_right,
       b.pubicarch_right,
       b.dc_right,
       b.uterusora_right,
       b.presentation2_right,
       b.fetusplace1_right,
       b.presentationheitht_right,
       b.caputsuccedaneumsize_right,
       b.caputsuccedaneumplace1_right,
       b.abdominalwall_v_right,
       b.abdominalwall_h_right,
       b.fetusplace2_right,
       b.babyweight_right,
       b.apgar_right,
       b.fetusfacies_right,
       b.caputsuccedaneumsizex_right,
       b.caputsuccedaneumsizey_right,
       b.caputsuccedaneumplace2_right,
       b.amniocentesisbulk_right,
       b.placentasizex_right,
       b.placentasizey_right,
       b.placentasizez_right,
       b.placentasizeweight_right,
       b.umbilicalcordlength_right,
       b.sutureuterus_right,
       b.sutureabdominalwall_right,
       b.oxytocin_right,
       b.othermedicine_right,
       b.piss_right,
       b.bleeding_right,
       b.transfuse_right,
       b.fetalheartsound_right,
       b.bp_right,
       b.placentacircsafterop_right,
       b.placenta_right,
       b.umbilicalcord_right,
       b.sumary4_right
  from t_emr_cesareanrecord a, t_emr_cesareanrecordcon b
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and a.emr_seq = b.emr_seq
   and b.status = 0";
        #endregion

        #region ��ȡָ��ʱ��ı�
        /// <summary>
        /// ��ȡָ��ʱ��ı�
        /// </summary>
        private const string c_strCheckCreateDateSQL = @"select createuserid, opendate
                                                          from t_emr_cesareanrecord
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and createdate = ?
                                                           and status = 0";
        #endregion

        #region ��ȡָ����������޸�ʱ��
        /// <summary>
        /// ��ȡָ����������޸�ʱ��
        /// </summary>
        private const string c_strCheckLastModifyRecordSQL = @"select b.modifydate, b.modifyuserid
                                                              from t_emr_cesareanrecordcon b
                                                             where emr_seq = ?
                                                               and b.status = 0";
        #endregion

        #region ��ȡɾ��������Ҫ��Ϣ
        /// <summary>
        /// ��ȡɾ��������Ҫ��Ϣ
        /// </summary>
        private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
                                                          from t_emr_cesareanrecord
                                                         where inpatientid = ?
                                                           and inpatientdate = ?
                                                           and opendate = ?
                                                           and status = 1 ";
        #endregion

        #region ��Ӽ�¼��T_EMR_CESAREANRECORD
        /// <summary>
        /// ��Ӽ�¼��T_EMR_CESAREANRECORD
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into t_emr_cesareanrecord (inpatientid,inpatientdate,opendate,createdate,
        createuserid,status,emr_seq,registerid_chr,recorddate,pregnanttimes,laytimes,opdate,diagnosisbeforeop,diagnosisbeforeopxml,
        opindication,opindicationxml,uterusheight,uterusheightxml,abdomenround,abdomenroundxml,presentation1,presentation1xml,linkup,
        fetusweight,fetusweightxml,ischialspine,coccyxradian,ischiumnotch,pubicarch,pubicarchxml,dc,dcxml,uterusora,uterusoraxml,
        amniocentesis1,presentation2,presentation2xml,fetusplace1,fetusplace1xml,presentationheitht,presentationheithtxml,skull,
        caputsuccedaneumsize,caputsuccedaneumsizexml,caputsuccedaneumplace1,caputsuccedaneumplace1xml,abdominalwall_v,abdominalwall_vxml,
        abdominalwall_h,abdominalwall_hxml,fascia,peritoneum,uterus,fetusplace2,fetusplace2xml,engagement,presentationexpulsion,
        expulsiontime,babysex,babyweight,babyweightxml,apgar,apgarxml,fetusfacies,fetusfaciesxml,caputsuccedaneumsizex,caputsuccedaneumsizexxml,
        caputsuccedaneumsizey,caputsuccedaneumsizeyxml,caputsuccedaneumplace2,caputsuccedaneumplace2xml,amniocentesis2,amniocentesisbulk,
        amniocentesisbulkxml,embryolemmacircs,oviductcircs,ovarycircs,sutureuterus,sutureuterusxml,sutureabdominalwall,sutureabdominalwallxml,
        oxytocin,oxytocinxml,caputsuccedaneumsize_yn,caputsuccedaneumsizey_yn,othermedicine,othermedicinexml,piss,pissxml,bleeding,bleedingxml,transfuse,transfusexml,anatime,
        fetalheartsound,fetalheartsoundxml,bp,bpxml,placentacircsafterop,placentacircsafteropxml,diagnosisafterop,diagnosisafteropxml,anamode,
        anamodexml,opname,opnamexml,uncheckbeforeop,sequence_int,placenta,placentaxml,umbilicalcord,umbilicalcordxml,medicineafterop,sumary4,
        sumary4xml,assistant1,assistant2,optime_vchr,anatime_vchr) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?)";
        #endregion

        #region ��Ӽ�¼��T_EMR_CESAREANRECORDCON
        /// <summary>
        /// ��Ӽ�¼��T_EMR_CESAREANRECORDCON
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into t_emr_cesareanrecordcon (inpatientid,inpatientdate,opendate,
        modifydate,modifyuserid,emr_seq,status,registerid_chr,diagnosisbeforeop_right,opindication_right,diagnosisafterop_right,
        anamode_right,opname_right,uterusheight_right,abdomenround_right,presentation1_right,fetusweight_right,pubicarch_right,
        dc_right,uterusora_right,presentation2_right,fetusplace1_right,presentationheitht_right,caputsuccedaneumsize_right,
        caputsuccedaneumplace1_right,abdominalwall_v_right,abdominalwall_h_right,fetusplace2_right,babyweight_right,apgar_right,
        fetusfacies_right,caputsuccedaneumsizex_right,caputsuccedaneumsizey_right,caputsuccedaneumplace2_right,amniocentesisbulk_right,
        sutureuterus_right,sutureabdominalwall_right,oxytocin_right,othermedicine_right,piss_right,bleeding_right,
        transfuse_right,fetalheartsound_right,bp_right,placentacircsafterop_right,placenta_right,umbilicalcord_right,sumary4_right) 
        values (?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?,?,?,
                ?,?,?,?,?,?,?,?)";
        #endregion

        #region �޸ļ�¼��T_EMR_CESAREANRECORD
        /// <summary>
        /// �޸ļ�¼��T_EMR_CESAREANRECORD
        /// </summary>
        private const string c_strModifyRecordSQL = @"update t_emr_cesareanrecord set recorddate = ?,pregnanttimes = ?,laytimes = ?,opdate = ?,diagnosisbeforeop = ?,diagnosisbeforeopxml = ?,
        opindication = ?,opindicationxml = ?,uterusheight = ?,uterusheightxml = ?,abdomenround = ?,abdomenroundxml = ?,presentation1 = ?,presentation1xml = ?,linkup = ?,
        fetusweight = ?,fetusweightxml = ?,ischialspine = ?,coccyxradian = ?,ischiumnotch = ?,pubicarch = ?,pubicarchxml = ?,dc = ?,dcxml = ?,uterusora = ?,uterusoraxml = ?,
        amniocentesis1 = ?,presentation2 = ?,presentation2xml = ?,fetusplace1 = ?,fetusplace1xml = ?,presentationheitht = ?,presentationheithtxml = ?,skull = ?,
        caputsuccedaneumsize = ?,caputsuccedaneumsizexml = ?,caputsuccedaneumplace1 = ?,caputsuccedaneumplace1xml = ?,abdominalwall_v = ?,abdominalwall_vxml = ?,
        abdominalwall_h = ?,abdominalwall_hxml = ?,fascia = ?,peritoneum = ?,uterus = ?,fetusplace2 = ?,fetusplace2xml = ?,engagement = ?,presentationexpulsion = ?,
        expulsiontime = ?,babysex = ?,babyweight = ?,babyweightxml = ?,apgar = ?,apgarxml = ?,fetusfacies = ?,fetusfaciesxml = ?,caputsuccedaneumsizex = ?,caputsuccedaneumsizexxml = ?,
        caputsuccedaneumsizey = ?,caputsuccedaneumsizeyxml = ?,caputsuccedaneumplace2 = ?,caputsuccedaneumplace2xml = ?,amniocentesis2 = ?,amniocentesisbulk = ?,
        amniocentesisbulkxml = ?,embryolemmacircs = ?,oviductcircs = ?,ovarycircs = ?,sutureuterus = ?,sutureuterusxml = ?,sutureabdominalwall = ?,sutureabdominalwallxml = ?,oxytocin = ?,
        oxytocinxml = ?,caputsuccedaneumsize_yn = ?,caputsuccedaneumsizey_yn = ?,othermedicine = ?,othermedicinexml = ?,piss = ?,pissxml = ?,bleeding = ?,bleedingxml = ?,transfuse = ?,transfusexml = ?,
        anatime = ?,fetalheartsound = ?,fetalheartsoundxml = ?,bp = ?,bpxml = ?,placentacircsafterop = ?,placentacircsafteropxml = ?,diagnosisafterop = ?,diagnosisafteropxml = ?,
        anamode = ?,anamodexml = ?,opname = ?,opnamexml  = ?, uncheckbeforeop = ?,placenta= ?,placentaxml= ?,umbilicalcord=?,umbilicalcordxml=?,medicineafterop=?,
        sumary4=?,sumary4xml=?,assistant1=?,assistant2=?,sequence_int = ?,optime_vchr = ?,anatime_vchr = ?
        where emr_seq = ? and status=0";
        #endregion

        #region �޸ļ�¼��T_EMR_CESAREANRECORDCON
        /// <summary>
        /// ����T_EMR_CESAREANRECORDCON�ɼ�¼״̬Ϊ2
        /// </summary>
        private const string c_strSetOldRecordSQL = @"update t_emr_cesareanrecordcon set status = 2 where emr_seq = ? and status = 0";

        /// <summary>
        /// �޸ļ�¼��T_EMR_CESAREANRECORDCON
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;
        #endregion

        #region ����T_EMR_CESAREANRECORD��ɾ����¼����Ϣ
        /// <summary>
        /// ����T_EMR_CESAREANRECORD��ɾ����¼����Ϣ
        /// </summary>
        private const string c_strDeleteRecordSQL = @"update t_emr_cesareanrecord
                                                       set status = 1, deactiveddate = ?, deactivedoperatorid = ?
                                                     where emr_seq = ?
                                                       and status = 0";
        #endregion

        #region ��ȡLastModifyDate��FirstPrintDate
        /// <summary>
        /// ��ȡLastModifyDate��FirstPrintDate
        /// </summary>
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate, b.modifydate
                                                                      from t_emr_cesareanrecord a, t_emr_cesareanrecordcon b
                                                                     where a.inpatientid = ?
                                                                       and a.inpatientdate = ?
                                                                       and a.opendate = ?
                                                                       and a.status = 0
                                                                       and a.emr_seq = b.emr_seq
                                                                       and b.status = 0";
        #endregion

        #region ����FirstPrintDate
        /// <summary>
        /// ����FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update  t_emr_cesareanrecord 
															set firstprintdate= ? 
															where inpatientid= ? 
															and inpatientdate= ? 
															and opendate=? 
															and firstprintdate is null 
															and status=0";
        #endregion

        #region ��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ��
        /// <summary>
        /// ��ȡָ�����˵�����ָ��ɾ����ɾ���ļ�¼ʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = @"select createdate,opendate 
																from t_emr_cesareanrecord 
																where inpatientid = ? 
																and inpatientdate= ? 
																and deactivedoperatorid= ? 
																and status=1";
        #endregion

        #region ��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ��
        /// <summary>
        /// ��ȡָ�����˵������Ѿ�ɾ���ļ�¼ʱ��
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = @"select createdate,opendate 
																from t_emr_cesareanrecord 
																where inpatientid = ? 
																and inpatientdate= ? and status=1";
        #endregion

        #region ��ȡ��ɾ����¼
        /// <summary>
        /// ��ȡ��ɾ����¼
        /// </summary>
        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.emr_seq,
       a.registerid_chr,
       a.recorddate,
       a.pregnanttimes,
       a.laytimes,
       a.opdate,
       a.diagnosisbeforeop,
       a.diagnosisbeforeopxml,
       a.opindication,
       a.opindicationxml,
       a.uterusheight,
       a.uterusheightxml,
       a.abdomenround,
       a.abdomenroundxml,
       a.presentation1,
       a.presentation1xml,
       a.linkup,
       a.fetusweight,
       a.fetusweightxml,
       a.ischialspine,
       a.coccyxradian,
       a.ischiumnotch,
       a.pubicarch,
       a.pubicarchxml,
       a.dc,
       a.dcxml,
       a.uterusora,
       a.uterusoraxml,
       a.amniocentesis1,
       a.presentation2,
       a.presentation2xml,
       a.fetusplace1,
       a.fetusplace1xml,
       a.presentationheitht,
       a.presentationheithtxml,
       a.skull,
       a.caputsuccedaneumsize,
       a.caputsuccedaneumsizexml,
       a.caputsuccedaneumplace1,
       a.caputsuccedaneumplace1xml,
       a.abdominalwall_v,
       a.abdominalwall_vxml,
       a.abdominalwall_h,
       a.abdominalwall_hxml,
       a.fascia,
       a.peritoneum,
       a.uterus,
       a.fetusplace2,
       a.fetusplace2xml,
       a.engagement,
       a.presentationexpulsion,
       a.expulsiontime,
       a.babysex,
       a.babyweight,
       a.babyweightxml,
       a.apgar,
       a.apgarxml,
       a.fetusfacies,
       a.fetusfaciesxml,
       a.caputsuccedaneumsizex,
       a.caputsuccedaneumsizexxml,
       a.caputsuccedaneumsizey,
       a.caputsuccedaneumsizeyxml,
       a.caputsuccedaneumplace2,
       a.caputsuccedaneumplace2xml,
       a.amniocentesis2,
       a.amniocentesisbulk,
       a.amniocentesisbulkxml,
       a.placentasizex,
       a.placentasizexxml,
       a.placentasizey,
       a.placentasizeyxml,
       a.placentasizez,
       a.placentasizezxml,
       a.placentasizeweight,
       a.placentasizeweightxml,
       a.placentacalcify,
       a.umbilicalcordlength,
       a.umbilicalcordlengthxml,
       a.umbilicalcordcircs,
       a.embryolemmacircs,
       a.oviductcircs,
       a.ovarycircs,
       a.sutureuterus,
       a.sutureuterusxml,
       a.sutureabdominalwall,
       a.sutureabdominalwallxml,
       a.oxytocin,
       a.oxytocinxml,
       a.othermedicine,
       a.othermedicinexml,
       a.piss,
       a.pissxml,
       a.bleeding,
       a.bleedingxml,
       a.transfuse,
       a.transfusexml,
       a.anatime,
       a.fetalheartsound,
       a.fetalheartsoundxml,
       a.bp,
       a.bpxml,
       a.placentacircsafterop,
       a.placentacircsafteropxml,
       a.diagnosisafterop,
       a.diagnosisafteropxml,
       a.anamode,
       a.anamodexml,
       a.opname,
       a.opnamexml,
       a.uncheckbeforeop,
       a.sequence_int,
       a.placenta,
       a.placentaxml,
       a.umbilicalcord,
       a.umbilicalcordxml,
       a.medicineafterop,
       a.sumary4,
       a.sumary4xml,
       a.assistant1,
       a.assistant2,
       a.optime_vchr,
       a.anatime_vchr,
       a.caputsuccedaneumsizey_yn,
       a.caputsuccedaneumsize_yn,
       b.modifydate,
       b.modifyuserid,
       b.diagnosisbeforeop_right,
       b.opindication_right,
       b.diagnosisafterop_right,
       b.anamode_right,
       b.opname_right,
       b.uterusheight_right,
       b.abdomenround_right,
       b.presentation1_right,
       b.fetusweight_right,
       b.pubicarch_right,
       b.dc_right,
       b.uterusora_right,
       b.presentation2_right,
       b.fetusplace1_right,
       b.presentationheitht_right,
       b.caputsuccedaneumsize_right,
       b.caputsuccedaneumplace1_right,
       b.abdominalwall_v_right,
       b.abdominalwall_h_right,
       b.fetusplace2_right,
       b.babyweight_right,
       b.apgar_right,
       b.fetusfacies_right,
       b.caputsuccedaneumsizex_right,
       b.caputsuccedaneumsizey_right,
       b.caputsuccedaneumplace2_right,
       b.amniocentesisbulk_right,
       b.placentasizex_right,
       b.placentasizey_right,
       b.placentasizez_right,
       b.placentasizeweight_right,
       b.umbilicalcordlength_right,
       b.sutureuterus_right,
       b.sutureabdominalwall_right,
       b.oxytocin_right,
       b.othermedicine_right,
       b.piss_right,
       b.bleeding_right,
       b.transfuse_right,
       b.fetalheartsound_right,
       b.bp_right,
       b.placentacircsafterop_right,
       b.placenta_right,
       b.umbilicalcord_right,
       b.sumary4_right
  from t_emr_cesareanrecord a, t_emr_cesareanrecordcon b
 where a.inpatientid = ''
   and a.inpatientdate = ''
   and a.opendate = ''
   and a.status = 1
   and a.emr_seq = b.emr_seq
   and b.status = 0";
        #endregion 
        #endregion

        #region ��ȡ���˵ĸü�¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵ĸü�¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
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
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }		//����
            return lngRes;
        }
        #endregion

        #region �������ݿ��е��״δ�ӡʱ��
        /// <summary>
        /// �������ݿ��е��״δ�ӡʱ�䡣
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
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
                //������                              
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate))
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }
        #endregion

        #region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strDeleteUserID">ɾ����ID</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID.Trim();
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
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
            //����
            return lngRes;
        }
        #endregion

        #region ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// <summary>
        /// ��ȡ���˵��Ѿ���ɾ����¼ʱ���б�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strCreateDateArr">�û���д�Ĵ���ʱ������</param>
        /// <param name="p_strOpenDateArr">ϵͳ���ɵĿ�ʼʱ������</param>
        /// <returns></returns>
        [AutoComplete]
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
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate))
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //���ý��
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
            //����
            return lngRes;
        }
        #endregion

        #region ��ȡָ����¼������
        /// <summary>
        /// ��ȡָ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID,
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
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    DataRow dtbValueRow = dtbValue.Rows[0];
                    clsEMR_CesareanRecordValue objRecordContent = new clsEMR_CesareanRecordValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValueRow["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValueRow["MODIFYDATE"].ToString());

                    if (dtbValueRow["FIRSTPRINTDATE"] == DBNull.Value)
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValueRow["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValueRow["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValueRow["MODIFYUSERID"].ToString();
                    if (dtbValueRow["STATUS"] == DBNull.Value)
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValueRow["STATUS"].ToString());
                    if (dtbValueRow["EMR_SEQ"] == DBNull.Value)
                        return -1;
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValueRow["EMR_SEQ"]);

                    if (dtbValueRow["PREGNANTTIMES"] == DBNull.Value)
                        objRecordContent.m_intPREGNANTTIMES = -1;
                    else
                        objRecordContent.m_intPREGNANTTIMES = Convert.ToInt32(dtbValueRow["PREGNANTTIMES"].ToString());
                    if (dtbValueRow["LAYTIMES"] == DBNull.Value)
                        objRecordContent.m_intLAYTIMES = -1;
                    else
                        objRecordContent.m_intLAYTIMES = Convert.ToInt32(dtbValueRow["LAYTIMES"].ToString());
                    objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValueRow["RECORDDATE"]);

                    objRecordContent.m_dtmOPDATE = Convert.ToDateTime(dtbValueRow["OPDATE"]);
                    objRecordContent.m_strDIAGNOSISBEFOREOP = dtbValueRow["DIAGNOSISBEFOREOP"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOPXML = dtbValueRow["DIAGNOSISBEFOREOPXML"].ToString();
                    objRecordContent.m_strOPINDICATION = dtbValueRow["OPINDICATION"].ToString();
                    objRecordContent.m_strOPINDICATIONXML = dtbValueRow["OPINDICATIONXML"].ToString();
                    objRecordContent.m_strUTERUSHEIGHT = dtbValueRow["UTERUSHEIGHT"].ToString();
                    objRecordContent.m_strUTERUSHEIGHTXML = dtbValueRow["UTERUSHEIGHTXML"].ToString();
                    objRecordContent.m_strABDOMENROUND = dtbValueRow["ABDOMENROUND"].ToString();
                    objRecordContent.m_strABDOMENROUNDXML = dtbValueRow["ABDOMENROUNDXML"].ToString();
                    objRecordContent.m_strPRESENTATION1 = dtbValueRow["PRESENTATION1"].ToString();
                    objRecordContent.m_strPRESENTATION1XML = dtbValueRow["PRESENTATION1XML"].ToString();
                    objRecordContent.m_strLINKUP = dtbValueRow["LINKUP"].ToString();
                    objRecordContent.m_strFETUSWEIGHT = dtbValueRow["FETUSWEIGHT"].ToString();
                    objRecordContent.m_strFETUSWEIGHTXML = dtbValueRow["FETUSWEIGHTXML"].ToString();
                    objRecordContent.m_strISCHIALSPINE = dtbValueRow["ISCHIALSPINE"].ToString();
                    objRecordContent.m_strCOCCYXRADIAN = dtbValueRow["COCCYXRADIAN"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH = dtbValueRow["ISCHIUMNOTCH"].ToString();
                    objRecordContent.m_strPUBICARCH = dtbValueRow["PUBICARCH"].ToString();
                    objRecordContent.m_strPUBICARCHXML = dtbValueRow["PUBICARCHXML"].ToString();
                    objRecordContent.m_strDC = dtbValueRow["DC"].ToString();
                    objRecordContent.m_strDCXML = dtbValueRow["DCXML"].ToString();
                    objRecordContent.m_strUTERUSORA = dtbValueRow["UTERUSORA"].ToString();
                    objRecordContent.m_strUTERUSORAXML = dtbValueRow["UTERUSORAXML"].ToString();
                    objRecordContent.m_strAMNIOCENTESIS1 = dtbValueRow["AMNIOCENTESIS1"].ToString();
                    objRecordContent.m_strPRESENTATION2 = dtbValueRow["PRESENTATION2"].ToString();
                    objRecordContent.m_strPRESENTATION2XML = dtbValueRow["PRESENTATION2XML"].ToString();
                    objRecordContent.m_strFETUSPLACE1 = dtbValueRow["FETUSPLACE1"].ToString();
                    objRecordContent.m_strFETUSPLACE1XML = dtbValueRow["FETUSPLACE1XML"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHT = dtbValueRow["PRESENTATIONHEITHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHTXML = dtbValueRow["PRESENTATIONHEITHTXML"].ToString();
                    objRecordContent.m_strSKULL = dtbValueRow["SKULL"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE = dtbValueRow["CAPUTSUCCEDANEUMSIZE"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEXML = dtbValueRow["CAPUTSUCCEDANEUMSIZEXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE1 = dtbValueRow["CAPUTSUCCEDANEUMPLACE1"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE1XML = dtbValueRow["CAPUTSUCCEDANEUMPLACE1XML"].ToString();
                    objRecordContent.m_strABDOMINALWALL_V = dtbValueRow["ABDOMINALWALL_V"].ToString();
                    objRecordContent.m_strABDOMINALWALL_VXML = dtbValueRow["ABDOMINALWALL_VXML"].ToString();
                    objRecordContent.m_strABDOMINALWALL_H = dtbValueRow["ABDOMINALWALL_H"].ToString();
                    objRecordContent.m_strABDOMINALWALL_HXML = dtbValueRow["ABDOMINALWALL_HXML"].ToString();
                    objRecordContent.m_strFASCIA = dtbValueRow["FASCIA"].ToString();
                    objRecordContent.m_strPERITONEUM = dtbValueRow["PERITONEUM"].ToString();
                    objRecordContent.m_strUTERUS = dtbValueRow["UTERUS"].ToString();
                    objRecordContent.m_strFETUSPLACE2 = dtbValueRow["FETUSPLACE2"].ToString();
                    objRecordContent.m_strFETUSPLACE2XML = dtbValueRow["FETUSPLACE2XML"].ToString();
                    objRecordContent.m_strENGAGEMENT = dtbValueRow["ENGAGEMENT"].ToString();
                    objRecordContent.m_strPRESENTATIONEXPULSION = dtbValueRow["PRESENTATIONEXPULSION"].ToString();
                    objRecordContent.m_dtmEXPULSIONTIME = Convert.ToDateTime(dtbValueRow["EXPULSIONTIME"]);
                    objRecordContent.m_strBABYSEX = dtbValueRow["BABYSEX"].ToString();
                    objRecordContent.m_strBABYWEIGHT = dtbValueRow["BABYWEIGHT"].ToString();
                    objRecordContent.m_strBABYWEIGHTXML = dtbValueRow["BABYWEIGHTXML"].ToString();
                    objRecordContent.m_strAPGAR = dtbValueRow["APGAR"].ToString();
                    objRecordContent.m_strAPGARXML = dtbValueRow["APGARXML"].ToString();
                    objRecordContent.m_strFETUSFACIES = dtbValueRow["FETUSFACIES"].ToString();
                    objRecordContent.m_strFETUSFACIESXML = dtbValueRow["FETUSFACIESXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEX = dtbValueRow["CAPUTSUCCEDANEUMSIZEX"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEXXML = dtbValueRow["CAPUTSUCCEDANEUMSIZEXXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEY = dtbValueRow["CAPUTSUCCEDANEUMSIZEY"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEYXML = dtbValueRow["CAPUTSUCCEDANEUMSIZEYXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE2 = dtbValueRow["CAPUTSUCCEDANEUMPLACE2"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE2XML = dtbValueRow["CAPUTSUCCEDANEUMPLACE2XML"].ToString();
                    objRecordContent.m_strAMNIOCENTESIS2 = dtbValueRow["AMNIOCENTESIS2"].ToString();
                    objRecordContent.m_strAMNIOCENTESISBULK = dtbValueRow["AMNIOCENTESISBULK"].ToString();
                    objRecordContent.m_strAMNIOCENTESISBULKXML = dtbValueRow["AMNIOCENTESISBULKXML"].ToString();
                    objRecordContent.m_strEMBRYOLEMMACIRCS = dtbValueRow["EMBRYOLEMMACIRCS"].ToString();
                    objRecordContent.m_strOVIDUCTCIRCS = dtbValueRow["OVIDUCTCIRCS"].ToString();
                    objRecordContent.m_strOVARYCIRCS = dtbValueRow["OVARYCIRCS"].ToString();
                    objRecordContent.m_strSUTUREUTERUS = dtbValueRow["SUTUREUTERUS"].ToString();
                    objRecordContent.m_strSUTUREUTERUSXML = dtbValueRow["SUTUREUTERUSXML"].ToString();
                    objRecordContent.m_strSUTUREABDOMINALWALL = dtbValueRow["SUTUREABDOMINALWALL"].ToString();
                    objRecordContent.m_strSUTUREABDOMINALWALLXML = dtbValueRow["SUTUREABDOMINALWALLXML"].ToString();
                    objRecordContent.m_strOXYTOCIN = dtbValueRow["OXYTOCIN"].ToString();
                    objRecordContent.m_strOXYTOCINXML = dtbValueRow["OXYTOCINXML"].ToString();
                    //objRecordContent.m_strIM = dtbValueRow["IM"].ToString();
                    //objRecordContent.m_strIMXML = dtbValueRow["IMXML"].ToString();
                    //objRecordContent.m_strIV = dtbValueRow["IV"].ToString();
                    //objRecordContent.m_strIVXML = dtbValueRow["IVXML"].ToString();
                    //��ǰ����>>����(����),��������>>����(����)
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE_YN = dtbValueRow["CAPUTSUCCEDANEUMSIZE_YN"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEY_YN = dtbValueRow["CAPUTSUCCEDANEUMSIZEY_YN"].ToString();
                    //--------------------------------------------------------------------
                    objRecordContent.m_strOTHERMEDICINE = dtbValueRow["OTHERMEDICINE"].ToString();
                    objRecordContent.m_strOTHERMEDICINEXML = dtbValueRow["OTHERMEDICINEXML"].ToString();
                    objRecordContent.m_strPISS = dtbValueRow["PISS"].ToString();
                    objRecordContent.m_strPISSXML = dtbValueRow["PISSXML"].ToString();
                    objRecordContent.m_strBLEEDING = dtbValueRow["BLEEDING"].ToString();
                    objRecordContent.m_strBLEEDINGXML = dtbValueRow["BLEEDINGXML"].ToString();
                    objRecordContent.m_strTRANSFUSE = dtbValueRow["TRANSFUSE"].ToString();
                    objRecordContent.m_strTRANSFUSEXML = dtbValueRow["TRANSFUSEXML"].ToString();
                    objRecordContent.m_strFETALHEARTSOUND = dtbValueRow["FETALHEARTSOUND"].ToString();
                    objRecordContent.m_strFETALHEARTSOUNDXML = dtbValueRow["FETALHEARTSOUNDXML"].ToString();
                    objRecordContent.m_strBP = dtbValueRow["BP"].ToString();
                    objRecordContent.m_strBPXML = dtbValueRow["BPXML"].ToString();
                    objRecordContent.m_strPLACENTACIRCSAFTEROP = dtbValueRow["PLACENTACIRCSAFTEROP"].ToString();
                    objRecordContent.m_strPLACENTACIRCSAFTEROPXML = dtbValueRow["PLACENTACIRCSAFTEROPXML"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROP = dtbValueRow["DIAGNOSISAFTEROP"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROPXML = dtbValueRow["DIAGNOSISAFTEROPXML"].ToString();
                    objRecordContent.m_strANAMODE = dtbValueRow["ANAMODE"].ToString();
                    objRecordContent.m_strANAMODEXML = dtbValueRow["ANAMODEXML"].ToString();
                    objRecordContent.m_strOPNAME = dtbValueRow["OPNAME"].ToString();
                    objRecordContent.m_strOPNAMEXML = dtbValueRow["OPNAMEXML"].ToString();
                    objRecordContent.m_strUnCheckBeforeOP = dtbValueRow["UNCHECKBEFOREOP"].ToString();
                    objRecordContent.m_strANATIME = dtbValueRow["ANATIME_VCHR"].ToString();
                    objRecordContent.m_strOPTime = dtbValueRow["OPTIME_VCHR"].ToString();

                    objRecordContent.m_strDIAGNOSISBEFOREOP_RIGHT = dtbValueRow["DIAGNOSISBEFOREOP_RIGHT"].ToString();
                    objRecordContent.m_strOPINDICATION_RIGHT = dtbValueRow["OPINDICATION_RIGHT"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROP_RIGHT = dtbValueRow["DIAGNOSISAFTEROP_RIGHT"].ToString();
                    objRecordContent.m_strANAMODE_RIGHT = dtbValueRow["ANAMODE_RIGHT"].ToString();
                    objRecordContent.m_strOPNAME_RIGHT = dtbValueRow["OPNAME_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSHEIGHT_RIGHT = dtbValueRow["UTERUSHEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strABDOMENROUND_RIGHT = dtbValueRow["ABDOMENROUND_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION1_RIGHT = dtbValueRow["PRESENTATION1_RIGHT"].ToString();
                    objRecordContent.m_strFETUSWEIGHT_RIGHT = dtbValueRow["FETUSWEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strPUBICARCH_RIGHT = dtbValueRow["PUBICARCH_RIGHT"].ToString();
                    objRecordContent.m_strDC_RIGHT = dtbValueRow["DC_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSORA_RIGHT = dtbValueRow["UTERUSORA_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION2_RIGHT = dtbValueRow["PRESENTATION2_RIGHT"].ToString();
                    objRecordContent.m_strFETUSPLACE1_RIGHT = dtbValueRow["FETUSPLACE1_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHT_RIGHT = dtbValueRow["PRESENTATIONHEITHT_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMSIZE_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE1_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMPLACE1_RIGHT"].ToString();
                    objRecordContent.m_strABDOMINALWALL_V_RIGHT = dtbValueRow["ABDOMINALWALL_V_RIGHT"].ToString();
                    objRecordContent.m_strABDOMINALWALL_H_RIGHT = dtbValueRow["ABDOMINALWALL_H_RIGHT"].ToString();
                    objRecordContent.m_strFETUSPLACE2_RIGHT = dtbValueRow["FETUSPLACE2_RIGHT"].ToString();
                    objRecordContent.m_strBABYWEIGHT_RIGHT = dtbValueRow["BABYWEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strAPGAR_RIGHT = dtbValueRow["APGAR_RIGHT"].ToString();
                    objRecordContent.m_strFETUSFACIES_RIGHT = dtbValueRow["FETUSFACIES_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEX_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMSIZEX_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEY_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMSIZEY_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE2_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMPLACE2_RIGHT"].ToString();
                    objRecordContent.m_strAMNIOCENTESISBULK_RIGHT = dtbValueRow["AMNIOCENTESISBULK_RIGHT"].ToString();
                    objRecordContent.m_strSUTUREUTERUS_RIGHT = dtbValueRow["SUTUREUTERUS_RIGHT"].ToString();
                    objRecordContent.m_strSUTUREABDOMINALWALL_RIGHT = dtbValueRow["SUTUREABDOMINALWALL_RIGHT"].ToString();
                    objRecordContent.m_strOXYTOCIN_RIGHT = dtbValueRow["OXYTOCIN_RIGHT"].ToString();
                    //objRecordContent.m_strIM_RIGHT = dtbValueRow["IM_RIGHT"].ToString();
                    //objRecordContent.m_strIV_RIGHT = dtbValueRow["IV_RIGHT"].ToString();
                    objRecordContent.m_strOTHERMEDICINE_RIGHT = dtbValueRow["OTHERMEDICINE_RIGHT"].ToString();
                    objRecordContent.m_strPISS_RIGHT = dtbValueRow["PISS_RIGHT"].ToString();
                    objRecordContent.m_strBLEEDING_RIGHT = dtbValueRow["BLEEDING_RIGHT"].ToString();
                    objRecordContent.m_strTRANSFUSE_RIGHT = dtbValueRow["TRANSFUSE_RIGHT"].ToString();
                    objRecordContent.m_strFETALHEARTSOUND_RIGHT = dtbValueRow["FETALHEARTSOUND_RIGHT"].ToString();
                    objRecordContent.m_strBP_RIGHT = dtbValueRow["BP_RIGHT"].ToString();
                    objRecordContent.m_strPLACENTACIRCSAFTEROP_RIGHT = dtbValueRow["PLACENTACIRCSAFTEROP_RIGHT"].ToString();

                    objRecordContent.m_strPLACENTASIZEX = dtbValueRow["PLACENTASIZEX"].ToString();
                    objRecordContent.m_strPLACENTASIZEXXML = dtbValueRow["PLACENTASIZEXXML"].ToString();
                    objRecordContent.m_strPLACENTASIZEY = dtbValueRow["PLACENTASIZEY"].ToString();
                    objRecordContent.m_strPLACENTASIZEYXML = dtbValueRow["PLACENTASIZEYXML"].ToString();
                    objRecordContent.m_strPLACENTASIZEZ = dtbValueRow["PLACENTASIZEZ"].ToString();
                    objRecordContent.m_strPLACENTASIZEZXML = dtbValueRow["PLACENTASIZEZXML"].ToString();
                    objRecordContent.m_strPLACENTASIZEWEIGHT = dtbValueRow["PLACENTASIZEWEIGHT"].ToString();
                    objRecordContent.m_strPLACENTASIZEWEIGHTXML = dtbValueRow["PLACENTASIZEWEIGHTXML"].ToString();
                    objRecordContent.m_strPLACENTACALCIFY = dtbValueRow["PLACENTACALCIFY"].ToString();
                    objRecordContent.m_strUMBILICALCORDLENGTH = dtbValueRow["UMBILICALCORDLENGTH"].ToString();
                    objRecordContent.m_strUMBILICALCORDLENGTHXML = dtbValueRow["UMBILICALCORDLENGTHXML"].ToString();
                    objRecordContent.m_strUMBILICALCORDCIRCS = dtbValueRow["UMBILICALCORDCIRCS"].ToString();

                    objRecordContent.m_strPLACENTASIZEX_RIGHT = dtbValueRow["PLACENTASIZEX_RIGHT"].ToString();
                    objRecordContent.m_strPLACENTASIZEY_RIGHT = dtbValueRow["PLACENTASIZEY_RIGHT"].ToString();
                    objRecordContent.m_strPLACENTASIZEZ_RIGHT = dtbValueRow["PLACENTASIZEZ_RIGHT"].ToString();
                    objRecordContent.m_strPLACENTASIZEWEIGHT_RIGHT = dtbValueRow["PLACENTASIZEWEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strUMBILICALCORDLENGTH_RIGHT = dtbValueRow["UMBILICALCORDLENGTH_RIGHT"].ToString();

                    objRecordContent.m_strPLACENTA = dtbValueRow["PLACENTA"].ToString();
                    objRecordContent.m_strPLACENTA_RIGHT = dtbValueRow["PLACENTA_RIGHT"].ToString();
                    objRecordContent.m_strPLACENTAXML = dtbValueRow["PLACENTAXML"].ToString();
                    objRecordContent.m_strUMBILICALCORD = dtbValueRow["UMBILICALCORD"].ToString();
                    objRecordContent.m_strUMBILICALCORD_RIGHT = dtbValueRow["UMBILICALCORD_RIGHT"].ToString();
                    objRecordContent.m_strUMBILICALCORDXML = dtbValueRow["UMBILICALCORDXML"].ToString();
                    objRecordContent.m_strMEDICINEAFTEROP = dtbValueRow["MEDICINEAFTEROP"].ToString();
                    objRecordContent.m_strSUMARY4 = dtbValueRow["SUMARY4"].ToString();
                    objRecordContent.m_strSUMARY4_RIGHT = dtbValueRow["SUMARY4_RIGHT"].ToString();
                    objRecordContent.m_strSUMARY4XML = dtbValueRow["SUMARY4XML"].ToString();
                    objRecordContent.m_strASSISTANT1 = dtbValueRow["ASSISTANT1"].ToString();
                    objRecordContent.m_strASSISTANT2 = dtbValueRow["ASSISTANT2"].ToString();
                    //��ȡǩ������
                    if (dtbValueRow["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValueRow["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
                    }

                    p_objRecordContent = objRecordContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }
        #endregion

        #region �鿴�Ƿ�����ͬ�ļ�¼ʱ��
        /// <summary>
        /// �鿴�Ƿ�����ͬ�ļ�¼ʱ��
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo">������ͬ��¼,���ظ���ͬ��¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //�鿴DataTable.Rows.Count
                //�������1����ʾ�Ѿ��и�CreateDate�����Ҳ���ɾ���ļ�¼��
                //��ȡ�ü�¼����Ϣ����ֵ��p_objModifyInfo�С�����ֵʹ��Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    DataRow dtbValueRow = dtbValue.Rows[0];
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValueRow["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValueRow["OPENDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
                }
                //����	
                return lngRes;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }
        #endregion

        #region �����¼�����ݿ⡣�������,����ӱ�.
        /// <summary>
        /// �����¼�����ݿ⡣�������,����ӱ�.
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ	
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                long lngSignSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);
                lngRes = objSign.m_lngGetSequenceValue("seq_emr", out lngSequence);

                clsEMR_CesareanRecordValue objContent = (clsEMR_CesareanRecordValue)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(119, out objDPArr);
                objDPArr[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = 0;
                objDPArr[6].Value = lngSequence;
                objDPArr[7].Value = objContent.m_strREGISTERID_CHR;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = objContent.m_dtmRECORDDATE;
                if (objContent.m_intPREGNANTTIMES == -1)
                    objDPArr[9].Value = null;
                else
                    objDPArr[9].Value = objContent.m_intPREGNANTTIMES;
                if (objContent.m_intLAYTIMES == -1)
                    objDPArr[10].Value = null;
                else
                    objDPArr[10].Value = objContent.m_intLAYTIMES;
                objDPArr[11].DbType = DbType.DateTime;
                objDPArr[11].Value = objContent.m_dtmOPDATE;
                objDPArr[12].Value = objContent.m_strDIAGNOSISBEFOREOP;
                objDPArr[13].Value = objContent.m_strDIAGNOSISBEFOREOPXML;
                objDPArr[14].Value = objContent.m_strOPINDICATION;
                objDPArr[15].Value = objContent.m_strOPINDICATIONXML;
                objDPArr[16].Value = objContent.m_strUTERUSHEIGHT;
                objDPArr[17].Value = objContent.m_strUTERUSHEIGHTXML;
                objDPArr[18].Value = objContent.m_strABDOMENROUND;
                objDPArr[19].Value = objContent.m_strABDOMENROUNDXML;
                objDPArr[20].Value = objContent.m_strPRESENTATION1;
                objDPArr[21].Value = objContent.m_strPRESENTATION1XML;
                objDPArr[22].Value = objContent.m_strLINKUP;
                objDPArr[23].Value = objContent.m_strFETUSWEIGHT;
                objDPArr[24].Value = objContent.m_strFETUSWEIGHTXML;
                objDPArr[25].Value = objContent.m_strISCHIALSPINE;
                objDPArr[26].Value = objContent.m_strCOCCYXRADIAN;
                objDPArr[27].Value = objContent.m_strISCHIUMNOTCH;
                objDPArr[28].Value = objContent.m_strPUBICARCH;
                objDPArr[29].Value = objContent.m_strPUBICARCHXML;
                objDPArr[30].Value = objContent.m_strDC;
                objDPArr[31].Value = objContent.m_strDCXML;
                objDPArr[32].Value = objContent.m_strUTERUSORA;
                objDPArr[33].Value = objContent.m_strUTERUSORAXML;
                objDPArr[34].Value = objContent.m_strAMNIOCENTESIS1;
                objDPArr[35].Value = objContent.m_strPRESENTATION2;
                objDPArr[36].Value = objContent.m_strPRESENTATION2XML;
                objDPArr[37].Value = objContent.m_strFETUSPLACE1;
                objDPArr[38].Value = objContent.m_strFETUSPLACE1XML;
                objDPArr[39].Value = objContent.m_strPRESENTATIONHEITHT;
                objDPArr[40].Value = objContent.m_strPRESENTATIONHEITHTXML;
                objDPArr[41].Value = objContent.m_strSKULL;
                objDPArr[42].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE;
                objDPArr[43].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEXML;
                objDPArr[44].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE1;
                objDPArr[45].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE1XML;
                objDPArr[46].Value = objContent.m_strABDOMINALWALL_V;
                objDPArr[47].Value = objContent.m_strABDOMINALWALL_VXML;
                objDPArr[48].Value = objContent.m_strABDOMINALWALL_H;
                objDPArr[49].Value = objContent.m_strABDOMINALWALL_HXML;
                objDPArr[50].Value = objContent.m_strFASCIA;
                objDPArr[51].Value = objContent.m_strPERITONEUM;
                objDPArr[52].Value = objContent.m_strUTERUS;
                objDPArr[53].Value = objContent.m_strFETUSPLACE2;
                objDPArr[54].Value = objContent.m_strFETUSPLACE2XML;
                objDPArr[55].Value = objContent.m_strENGAGEMENT;
                objDPArr[56].Value = objContent.m_strPRESENTATIONEXPULSION;
                objDPArr[57].DbType = DbType.DateTime;
                objDPArr[57].Value = objContent.m_dtmEXPULSIONTIME;
                objDPArr[58].Value = objContent.m_strBABYSEX;
                objDPArr[59].Value = objContent.m_strBABYWEIGHT;
                objDPArr[60].Value = objContent.m_strBABYWEIGHTXML;
                objDPArr[61].Value = objContent.m_strAPGAR;
                objDPArr[62].Value = objContent.m_strAPGARXML;
                objDPArr[63].Value = objContent.m_strFETUSFACIES;
                objDPArr[64].Value = objContent.m_strFETUSFACIESXML;
                objDPArr[65].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEX;
                objDPArr[66].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEXXML;
                objDPArr[67].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEY;
                objDPArr[68].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEYXML;
                objDPArr[69].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE2;
                objDPArr[70].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE2XML;
                objDPArr[71].Value = objContent.m_strAMNIOCENTESIS2;
                objDPArr[72].Value = objContent.m_strAMNIOCENTESISBULK;
                objDPArr[73].Value = objContent.m_strAMNIOCENTESISBULKXML;               
                objDPArr[74].Value = objContent.m_strEMBRYOLEMMACIRCS;
                objDPArr[75].Value = objContent.m_strOVIDUCTCIRCS;
                objDPArr[76].Value = objContent.m_strOVARYCIRCS;
                objDPArr[77].Value = objContent.m_strSUTUREUTERUS;
                objDPArr[78].Value = objContent.m_strSUTUREUTERUSXML;
                objDPArr[79].Value = objContent.m_strSUTUREABDOMINALWALL;
                objDPArr[80].Value = objContent.m_strSUTUREABDOMINALWALLXML;
                objDPArr[81].Value = objContent.m_strOXYTOCIN;
                objDPArr[82].Value = objContent.m_strOXYTOCINXML;
                //---------------------------------------------
                objDPArr[83].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE_YN;
                objDPArr[84].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEY_YN;
                //---------------------------------------------
                //objDPArr[83].Value = objContent.m_strIM;
                //objDPArr[84].Value = objContent.m_strIMXML;
                //objDPArr[85].Value = objContent.m_strIV;
                //objDPArr[86].Value = objContent.m_strIVXML;
                objDPArr[85].Value = objContent.m_strOTHERMEDICINE;
                objDPArr[86].Value = objContent.m_strOTHERMEDICINEXML;
                objDPArr[87].Value = objContent.m_strPISS;
                objDPArr[88].Value = objContent.m_strPISSXML;
                objDPArr[89].Value = objContent.m_strBLEEDING;
                objDPArr[90].Value = objContent.m_strBLEEDINGXML;
                objDPArr[91].Value = objContent.m_strTRANSFUSE;
                objDPArr[92].Value = objContent.m_strTRANSFUSEXML;
                objDPArr[93].Value = DBNull.Value;
                objDPArr[94].Value = objContent.m_strFETALHEARTSOUND;
                objDPArr[95].Value = objContent.m_strFETALHEARTSOUNDXML;
                objDPArr[96].Value = objContent.m_strBP;
                objDPArr[97].Value = objContent.m_strBPXML;
                objDPArr[98].Value = objContent.m_strPLACENTACIRCSAFTEROP;
                objDPArr[99].Value = objContent.m_strPLACENTACIRCSAFTEROPXML;
                objDPArr[100].Value = objContent.m_strDIAGNOSISAFTEROP;
                objDPArr[101].Value = objContent.m_strDIAGNOSISAFTEROPXML;
                objDPArr[102].Value = objContent.m_strANAMODE;
                objDPArr[103].Value = objContent.m_strANAMODEXML;
                objDPArr[104].Value = objContent.m_strOPNAME;
                objDPArr[105].Value = objContent.m_strOPNAMEXML;
                objDPArr[106].Value = objContent.m_strUnCheckBeforeOP;
                objDPArr[107].Value = lngSignSequence;
                objDPArr[108].Value = objContent.m_strPLACENTA;
                objDPArr[109].Value = objContent.m_strPLACENTAXML;
                objDPArr[110].Value = objContent.m_strUMBILICALCORD;
                objDPArr[111].Value = objContent.m_strUMBILICALCORDXML;
                objDPArr[112].Value = objContent.m_strMEDICINEAFTEROP;
                objDPArr[113].Value = objContent.m_strSUMARY4;
                objDPArr[114].Value = objContent.m_strSUMARY4XML;
                objDPArr[115].Value = objContent.m_strASSISTANT1;
                objDPArr[116].Value = objContent.m_strASSISTANT2;
                objDPArr[117].Value = objContent.m_strOPTime;
                objDPArr[118].Value = objContent.m_strANATIME;

                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;
                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSequence);


                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(48, out objDPArr2);
                objDPArr2[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = lngSequence;
                objDPArr2[6].Value = 0;
                objDPArr2[7].Value = objContent.m_strREGISTERID_CHR;
                objDPArr2[8].Value = objContent.m_strDIAGNOSISBEFOREOP_RIGHT;
                objDPArr2[9].Value = objContent.m_strOPINDICATION_RIGHT;
                objDPArr2[10].Value = objContent.m_strDIAGNOSISAFTEROP_RIGHT;
                objDPArr2[11].Value = objContent.m_strANAMODE_RIGHT;
                objDPArr2[12].Value = objContent.m_strOPNAME_RIGHT;
                objDPArr2[13].Value = objContent.m_strUTERUSHEIGHT_RIGHT;
                objDPArr2[14].Value = objContent.m_strABDOMENROUND_RIGHT;
                objDPArr2[15].Value = objContent.m_strPRESENTATION1_RIGHT;
                objDPArr2[16].Value = objContent.m_strFETUSWEIGHT_RIGHT;
                objDPArr2[17].Value = objContent.m_strPUBICARCH_RIGHT;
                objDPArr2[18].Value = objContent.m_strDC_RIGHT;
                objDPArr2[19].Value = objContent.m_strUTERUSORA_RIGHT;
                objDPArr2[20].Value = objContent.m_strPRESENTATION2_RIGHT;
                objDPArr2[21].Value = objContent.m_strFETUSPLACE1_RIGHT;
                objDPArr2[22].Value = objContent.m_strPRESENTATIONHEITHT_RIGHT;
                objDPArr2[23].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT;
                objDPArr2[24].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE1_RIGHT;
                objDPArr2[25].Value = objContent.m_strABDOMINALWALL_V_RIGHT;
                objDPArr2[26].Value = objContent.m_strABDOMINALWALL_H_RIGHT;
                objDPArr2[27].Value = objContent.m_strFETUSPLACE2_RIGHT;
                objDPArr2[28].Value = objContent.m_strBABYWEIGHT_RIGHT;
                objDPArr2[29].Value = objContent.m_strAPGAR_RIGHT;
                objDPArr2[30].Value = objContent.m_strFETUSFACIES_RIGHT;
                objDPArr2[31].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEX_RIGHT;
                objDPArr2[32].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEY_RIGHT;
                objDPArr2[33].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE2_RIGHT;
                objDPArr2[34].Value = objContent.m_strAMNIOCENTESISBULK_RIGHT;                
                objDPArr2[35].Value = objContent.m_strSUTUREUTERUS_RIGHT;
                objDPArr2[36].Value = objContent.m_strSUTUREABDOMINALWALL_RIGHT;
                objDPArr2[37].Value = objContent.m_strOXYTOCIN_RIGHT;
                //objDPArr2[38].Value = objContent.m_strIM_RIGHT;
                //objDPArr2[39].Value = objContent.m_strIV_RIGHT;
                objDPArr2[38].Value = objContent.m_strOTHERMEDICINE_RIGHT;
                objDPArr2[39].Value = objContent.m_strPISS_RIGHT;
                objDPArr2[40].Value = objContent.m_strBLEEDING_RIGHT;
                objDPArr2[41].Value = objContent.m_strTRANSFUSE_RIGHT;
                objDPArr2[42].Value = objContent.m_strFETALHEARTSOUND_RIGHT;
                objDPArr2[43].Value = objContent.m_strBP_RIGHT;
                objDPArr2[44].Value = objContent.m_strPLACENTACIRCSAFTEROP_RIGHT;
                objDPArr2[45].Value = objContent.m_strPLACENTA_RIGHT;
                objDPArr2[46].Value = objContent.m_strUMBILICALCORD_RIGHT;
                objDPArr2[47].Value = objContent.m_strSUMARY4_RIGHT;

                //ִ��SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);

                //�ͷ�
                objSign = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }
        #endregion

        #region �鿴��ǰ��¼�Ƿ����µļ�¼
        /// <summary>
        /// �鿴��ǰ��¼�Ƿ����µļ�¼��
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>		
        /// <param name="p_objModifyInfo">����ǰ��¼�������µļ�¼,���ظ����¼�¼�Ĳ�����Ϣ,����Ϊ��</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                clsEMR_CesareanRecordValue objContent = p_objRecordContent as clsEMR_CesareanRecordValue;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = objContent.m_lngEMR_SEQ;
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);


                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = objContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = objContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = objContent.m_dtmOpenDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        DataRow dtbValueRow = dtbValue.Rows[0];
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValueRow["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValueRow["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //��DataTable�л�ȡModifyDate��ʹ֮��p_objRecordContent.m_dtmModifyDate�Ƚ�
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    DataRow dtbValueRow = dtbValue.Rows[0];
                    //�����ͬ������DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValueRow["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //���򣬷���Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValueRow["MODIFYUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValueRow["MODIFYDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }
        #endregion

        #region �����޸ĵ����ݱ��浽���ݿ�
        /// <summary>
        /// �����޸ĵ����ݱ��浽���ݿ⡣��������,����ӱ�.
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //��p_objRecordContent����Ϊ��Ҫʹ�õ����࣬��ʹ�ö�Ӧ���ֶ�ֵ	
                //��ȡǩ����ˮ��
                long lngSignSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSignSequence);

                clsEMR_CesareanRecordValue objContent = (clsEMR_CesareanRecordValue)p_objRecordContent;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(112, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objContent.m_dtmRECORDDATE;
                if (objContent.m_intPREGNANTTIMES == -1)
                    objDPArr[1].Value = null;
                else
                    objDPArr[1].Value = objContent.m_intPREGNANTTIMES;
                if (objContent.m_intLAYTIMES == -1)
                    objDPArr[2].Value = null;
                else
                    objDPArr[2].Value = objContent.m_intLAYTIMES;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmOPDATE;
                objDPArr[4].Value = objContent.m_strDIAGNOSISBEFOREOP;
                objDPArr[5].Value = objContent.m_strDIAGNOSISBEFOREOPXML;
                objDPArr[6].Value = objContent.m_strOPINDICATION;
                objDPArr[7].Value = objContent.m_strOPINDICATIONXML;
                objDPArr[8].Value = objContent.m_strUTERUSHEIGHT;
                objDPArr[9].Value = objContent.m_strUTERUSHEIGHTXML;
                objDPArr[10].Value = objContent.m_strABDOMENROUND;
                objDPArr[11].Value = objContent.m_strABDOMENROUNDXML;
                objDPArr[12].Value = objContent.m_strPRESENTATION1;
                objDPArr[13].Value = objContent.m_strPRESENTATION1XML;
                objDPArr[14].Value = objContent.m_strLINKUP;
                objDPArr[15].Value = objContent.m_strFETUSWEIGHT;
                objDPArr[16].Value = objContent.m_strFETUSWEIGHTXML;
                objDPArr[17].Value = objContent.m_strISCHIALSPINE;
                objDPArr[18].Value = objContent.m_strCOCCYXRADIAN;
                objDPArr[19].Value = objContent.m_strISCHIUMNOTCH;
                objDPArr[20].Value = objContent.m_strPUBICARCH;
                objDPArr[21].Value = objContent.m_strPUBICARCHXML;
                objDPArr[22].Value = objContent.m_strDC;
                objDPArr[23].Value = objContent.m_strDCXML;
                objDPArr[24].Value = objContent.m_strUTERUSORA;
                objDPArr[25].Value = objContent.m_strUTERUSORAXML;
                objDPArr[26].Value = objContent.m_strAMNIOCENTESIS1;
                objDPArr[27].Value = objContent.m_strPRESENTATION2;
                objDPArr[28].Value = objContent.m_strPRESENTATION2XML;
                objDPArr[29].Value = objContent.m_strFETUSPLACE1;
                objDPArr[30].Value = objContent.m_strFETUSPLACE1XML;
                objDPArr[31].Value = objContent.m_strPRESENTATIONHEITHT;
                objDPArr[32].Value = objContent.m_strPRESENTATIONHEITHTXML;
                objDPArr[33].Value = objContent.m_strSKULL;
                objDPArr[34].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE;
                objDPArr[35].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEXML;
                objDPArr[36].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE1;
                objDPArr[37].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE1XML;
                objDPArr[38].Value = objContent.m_strABDOMINALWALL_V;
                objDPArr[39].Value = objContent.m_strABDOMINALWALL_VXML;
                objDPArr[40].Value = objContent.m_strABDOMINALWALL_H;
                objDPArr[41].Value = objContent.m_strABDOMINALWALL_HXML;
                objDPArr[42].Value = objContent.m_strFASCIA;
                objDPArr[43].Value = objContent.m_strPERITONEUM;
                objDPArr[44].Value = objContent.m_strUTERUS;
                objDPArr[45].Value = objContent.m_strFETUSPLACE2;
                objDPArr[46].Value = objContent.m_strFETUSPLACE2XML;
                objDPArr[47].Value = objContent.m_strENGAGEMENT;
                objDPArr[48].Value = objContent.m_strPRESENTATIONEXPULSION;
                objDPArr[49].DbType = DbType.DateTime;
                objDPArr[49].Value = objContent.m_dtmEXPULSIONTIME;
                objDPArr[50].Value = objContent.m_strBABYSEX;
                objDPArr[51].Value = objContent.m_strBABYWEIGHT;
                objDPArr[52].Value = objContent.m_strBABYWEIGHTXML;
                objDPArr[53].Value = objContent.m_strAPGAR;
                objDPArr[54].Value = objContent.m_strAPGARXML;
                objDPArr[55].Value = objContent.m_strFETUSFACIES;
                objDPArr[56].Value = objContent.m_strFETUSFACIESXML;
                objDPArr[57].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEX;
                objDPArr[58].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEXXML;
                objDPArr[59].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEY;
                objDPArr[60].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEYXML;
                objDPArr[61].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE2;
                objDPArr[62].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE2XML;
                objDPArr[63].Value = objContent.m_strAMNIOCENTESIS2;
                objDPArr[64].Value = objContent.m_strAMNIOCENTESISBULK;
                objDPArr[65].Value = objContent.m_strAMNIOCENTESISBULKXML;                
                objDPArr[66].Value = objContent.m_strEMBRYOLEMMACIRCS;
                objDPArr[67].Value = objContent.m_strOVIDUCTCIRCS;
                objDPArr[68].Value = objContent.m_strOVARYCIRCS;
                objDPArr[69].Value = objContent.m_strSUTUREUTERUS;
                objDPArr[70].Value = objContent.m_strSUTUREUTERUSXML;
                objDPArr[71].Value = objContent.m_strSUTUREABDOMINALWALL;
                objDPArr[72].Value = objContent.m_strSUTUREABDOMINALWALLXML;
                objDPArr[73].Value = objContent.m_strOXYTOCIN;
                objDPArr[74].Value = objContent.m_strOXYTOCINXML;
                //------------------------------------------------------
                objDPArr[75].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE_YN;
                objDPArr[76].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEY_YN;
                //objDPArr[75].Value = objContent.m_strIM;
                //objDPArr[76].Value = objContent.m_strIMXML;
                //objDPArr[77].Value = objContent.m_strIV;
                //objDPArr[78].Value = objContent.m_strIVXML;
                //------------------------------------------------------
                objDPArr[77].Value = objContent.m_strOTHERMEDICINE;
                objDPArr[78].Value = objContent.m_strOTHERMEDICINEXML;
                objDPArr[79].Value = objContent.m_strPISS;
                objDPArr[80].Value = objContent.m_strPISSXML;
                objDPArr[81].Value = objContent.m_strBLEEDING;
                objDPArr[82].Value = objContent.m_strBLEEDINGXML;
                objDPArr[83].Value = objContent.m_strTRANSFUSE;
                objDPArr[84].Value = objContent.m_strTRANSFUSEXML;
                objDPArr[85].Value = DBNull.Value;
                objDPArr[86].Value = objContent.m_strFETALHEARTSOUND;
                objDPArr[87].Value = objContent.m_strFETALHEARTSOUNDXML;
                objDPArr[88].Value = objContent.m_strBP;
                objDPArr[89].Value = objContent.m_strBPXML;
                objDPArr[90].Value = objContent.m_strPLACENTACIRCSAFTEROP;
                objDPArr[91].Value = objContent.m_strPLACENTACIRCSAFTEROPXML;
                objDPArr[92].Value = objContent.m_strDIAGNOSISAFTEROP;
                objDPArr[93].Value = objContent.m_strDIAGNOSISAFTEROPXML;
                objDPArr[94].Value = objContent.m_strANAMODE;
                objDPArr[95].Value = objContent.m_strANAMODEXML;
                objDPArr[96].Value = objContent.m_strOPNAME;
                objDPArr[97].Value = objContent.m_strOPNAMEXML;
                objDPArr[98].Value = objContent.m_strUnCheckBeforeOP;
                objDPArr[99].Value = objContent.m_strPLACENTA;
                objDPArr[100].Value = objContent.m_strPLACENTAXML;
                objDPArr[101].Value = objContent.m_strUMBILICALCORD;
                objDPArr[102].Value = objContent.m_strUMBILICALCORDXML;
                objDPArr[103].Value = objContent.m_strMEDICINEAFTEROP;
                objDPArr[104].Value = objContent.m_strSUMARY4;
                objDPArr[105].Value = objContent.m_strSUMARY4XML;
                objDPArr[106].Value = objContent.m_strASSISTANT1;
                objDPArr[107].Value = objContent.m_strASSISTANT2;
                objDPArr[108].Value = lngSignSequence;
                objDPArr[109].Value = objContent.m_strOPTime;
                objDPArr[110].Value = objContent.m_strANATIME;
                objDPArr[111].Value = objContent.m_lngEMR_SEQ;


                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(objContent.objSignerArr, lngSignSequence);

                //���þɼ�¼״̬Ϊ2
                IDataParameter[] objDPArrStatus = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArrStatus);
                objDPArrStatus[0].Value = objContent.m_lngEMR_SEQ;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strSetOldRecordSQL, ref lngEff, objDPArrStatus);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(48, out objDPArr2);

                objDPArr2[0].Value = objContent.m_strInPatientID.Trim();
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_lngEMR_SEQ;
                objDPArr2[6].Value = 0;
                objDPArr2[7].Value = objContent.m_strREGISTERID_CHR;
                objDPArr2[8].Value = objContent.m_strDIAGNOSISBEFOREOP_RIGHT;
                objDPArr2[9].Value = objContent.m_strOPINDICATION_RIGHT;
                objDPArr2[10].Value = objContent.m_strDIAGNOSISAFTEROP_RIGHT;
                objDPArr2[11].Value = objContent.m_strANAMODE_RIGHT;
                objDPArr2[12].Value = objContent.m_strOPNAME_RIGHT;
                objDPArr2[13].Value = objContent.m_strUTERUSHEIGHT_RIGHT;
                objDPArr2[14].Value = objContent.m_strABDOMENROUND_RIGHT;
                objDPArr2[15].Value = objContent.m_strPRESENTATION1_RIGHT;
                objDPArr2[16].Value = objContent.m_strFETUSWEIGHT_RIGHT;
                objDPArr2[17].Value = objContent.m_strPUBICARCH_RIGHT;
                objDPArr2[18].Value = objContent.m_strDC_RIGHT;
                objDPArr2[19].Value = objContent.m_strUTERUSORA_RIGHT;
                objDPArr2[20].Value = objContent.m_strPRESENTATION2_RIGHT;
                objDPArr2[21].Value = objContent.m_strFETUSPLACE1_RIGHT;
                objDPArr2[22].Value = objContent.m_strPRESENTATIONHEITHT_RIGHT;
                objDPArr2[23].Value = objContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT;
                objDPArr2[24].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE1_RIGHT;
                objDPArr2[25].Value = objContent.m_strABDOMINALWALL_V_RIGHT;
                objDPArr2[26].Value = objContent.m_strABDOMINALWALL_H_RIGHT;
                objDPArr2[27].Value = objContent.m_strFETUSPLACE2_RIGHT;
                objDPArr2[28].Value = objContent.m_strBABYWEIGHT_RIGHT;
                objDPArr2[29].Value = objContent.m_strAPGAR_RIGHT;
                objDPArr2[30].Value = objContent.m_strFETUSFACIES_RIGHT;
                objDPArr2[31].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEX_RIGHT;
                objDPArr2[32].Value = objContent.m_strCAPUTSUCCEDANEUMSIZEY_RIGHT;
                objDPArr2[33].Value = objContent.m_strCAPUTSUCCEDANEUMPLACE2_RIGHT;
                objDPArr2[34].Value = objContent.m_strAMNIOCENTESISBULK_RIGHT;
                objDPArr2[35].Value = objContent.m_strSUTUREUTERUS_RIGHT;
                objDPArr2[36].Value = objContent.m_strSUTUREABDOMINALWALL_RIGHT;
                objDPArr2[37].Value = objContent.m_strOXYTOCIN_RIGHT;
                //objDPArr2[38].Value = objContent.m_strIM_RIGHT;
                //objDPArr2[39].Value = objContent.m_strIV_RIGHT;
                objDPArr2[38].Value = objContent.m_strOTHERMEDICINE_RIGHT;
                objDPArr2[39].Value = objContent.m_strPISS_RIGHT;
                objDPArr2[40].Value = objContent.m_strBLEEDING_RIGHT;
                objDPArr2[41].Value = objContent.m_strTRANSFUSE_RIGHT;
                objDPArr2[42].Value = objContent.m_strFETALHEARTSOUND_RIGHT;
                objDPArr2[43].Value = objContent.m_strBP_RIGHT;
                objDPArr2[44].Value = objContent.m_strPLACENTACIRCSAFTEROP_RIGHT;
                objDPArr2[45].Value = objContent.m_strPLACENTA_RIGHT;
                objDPArr2[46].Value = objContent.m_strUMBILICALCORD_RIGHT;
                objDPArr2[47].Value = objContent.m_strSUMARY4_RIGHT;

                //ִ��SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);


                //�ͷ�
                objSign = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }
        #endregion

        #region �Ѽ�¼�������С�ɾ����
        /// <summary>
        /// �Ѽ�¼�������С�ɾ������
        /// </summary>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                clsEMR_CesareanRecordValue objContent = p_objRecordContent as clsEMR_CesareanRecordValue;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = objContent.m_dtmDeActivedDate;
                objDPArr[1].Value = objContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = objContent.m_lngEMR_SEQ;

                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }			//����
            return lngRes;
        }
        #endregion

        #region ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        /// <summary>
        /// ��ȡ���ݿ������µ��޸�ʱ����״δ�ӡʱ��
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_dtmModifyDate">�޸�ʱ��</param>
        /// <param name="p_strFirstPrintDate">�״δ�ӡʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.Now;
            p_strFirstPrintDate = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    DataRow dtbValueRow = dtbValue.Rows[0];
                    //���ý��
                    p_strFirstPrintDate = dtbValueRow["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValueRow["MODIFYDATE"].ToString());
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }
        #endregion

        #region ��ȡָ���Ѿ���ɾ����¼������
        /// <summary>
        /// ��ȡָ���Ѿ���ɾ����¼�����ݡ�
        /// </summary>
        /// <param name="p_strInPatientID">סԺ��</param>
        /// <param name="p_strInPatientDate">��Ժ����</param>
        /// <param name="p_strOpenDate">��¼ʱ��</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">��¼����</param>
        /// <returns></returns>
        [AutoComplete]
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
                //������
                if (string.IsNullOrEmpty(p_strInPatientID) || string.IsNullOrEmpty(p_strInPatientDate) || string.IsNullOrEmpty(p_strOpenDate) || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //��DataTable.Rows�л�ȡ���
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //���ý��
                    DataRow dtbValueRow = dtbValue.Rows[0];
                    clsEMR_CesareanRecordValue objRecordContent = new clsEMR_CesareanRecordValue();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValueRow["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValueRow["MODIFYDATE"].ToString());

                    if (dtbValueRow["FIRSTPRINTDATE"] == DBNull.Value)
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValueRow["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = dtbValueRow["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = dtbValueRow["MODIFYUSERID"].ToString();
                    if (dtbValueRow["STATUS"] == DBNull.Value)
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(dtbValueRow["STATUS"].ToString());
                    if (dtbValueRow["EMR_SEQ"] == DBNull.Value)
                        return -1;
                    objRecordContent.m_lngEMR_SEQ = Convert.ToInt64(dtbValueRow["EMR_SEQ"]);

                    if (dtbValueRow["PREGNANTTIMES"] == DBNull.Value)
                        objRecordContent.m_intPREGNANTTIMES = -1;
                    else
                        objRecordContent.m_intPREGNANTTIMES = Convert.ToInt32(dtbValueRow["PREGNANTTIMES"].ToString());
                    if (dtbValueRow["LAYTIMES"] == DBNull.Value)
                        objRecordContent.m_intLAYTIMES = -1;
                    else
                        objRecordContent.m_intLAYTIMES = Convert.ToInt32(dtbValueRow["LAYTIMES"].ToString());
                    objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(dtbValueRow["RECORDDATE"]);

                    objRecordContent.m_dtmOPDATE = Convert.ToDateTime(dtbValueRow["OPDATE"]);
                    objRecordContent.m_strDIAGNOSISBEFOREOP = dtbValueRow["DIAGNOSISBEFOREOP"].ToString();
                    objRecordContent.m_strDIAGNOSISBEFOREOPXML = dtbValueRow["DIAGNOSISBEFOREOPXML"].ToString();
                    objRecordContent.m_strOPINDICATION = dtbValueRow["OPINDICATION"].ToString();
                    objRecordContent.m_strOPINDICATIONXML = dtbValueRow["OPINDICATIONXML"].ToString();
                    objRecordContent.m_strUTERUSHEIGHT = dtbValueRow["UTERUSHEIGHT"].ToString();
                    objRecordContent.m_strUTERUSHEIGHTXML = dtbValueRow["UTERUSHEIGHTXML"].ToString();
                    objRecordContent.m_strABDOMENROUND = dtbValueRow["ABDOMENROUND"].ToString();
                    objRecordContent.m_strABDOMENROUNDXML = dtbValueRow["ABDOMENROUNDXML"].ToString();
                    objRecordContent.m_strPRESENTATION1 = dtbValueRow["PRESENTATION1"].ToString();
                    objRecordContent.m_strPRESENTATION1XML = dtbValueRow["PRESENTATION1XML"].ToString();
                    objRecordContent.m_strLINKUP = dtbValueRow["LINKUP"].ToString();
                    objRecordContent.m_strFETUSWEIGHT = dtbValueRow["FETUSWEIGHT"].ToString();
                    objRecordContent.m_strFETUSWEIGHTXML = dtbValueRow["FETUSWEIGHTXML"].ToString();
                    objRecordContent.m_strISCHIALSPINE = dtbValueRow["ISCHIALSPINE"].ToString();
                    objRecordContent.m_strCOCCYXRADIAN = dtbValueRow["COCCYXRADIAN"].ToString();
                    objRecordContent.m_strISCHIUMNOTCH = dtbValueRow["ISCHIUMNOTCH"].ToString();
                    objRecordContent.m_strPUBICARCH = dtbValueRow["PUBICARCH"].ToString();
                    objRecordContent.m_strPUBICARCHXML = dtbValueRow["PUBICARCHXML"].ToString();
                    objRecordContent.m_strDC = dtbValueRow["DC"].ToString();
                    objRecordContent.m_strDCXML = dtbValueRow["DCXML"].ToString();
                    objRecordContent.m_strUTERUSORA = dtbValueRow["UTERUSORA"].ToString();
                    objRecordContent.m_strUTERUSORAXML = dtbValueRow["UTERUSORAXML"].ToString();
                    objRecordContent.m_strAMNIOCENTESIS1 = dtbValueRow["AMNIOCENTESIS1"].ToString();
                    objRecordContent.m_strPRESENTATION2 = dtbValueRow["PRESENTATION2"].ToString();
                    objRecordContent.m_strPRESENTATION2XML = dtbValueRow["PRESENTATION2XML"].ToString();
                    objRecordContent.m_strFETUSPLACE1 = dtbValueRow["FETUSPLACE1"].ToString();
                    objRecordContent.m_strFETUSPLACE1XML = dtbValueRow["FETUSPLACE1XML"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHT = dtbValueRow["PRESENTATIONHEITHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHTXML = dtbValueRow["PRESENTATIONHEITHTXML"].ToString();
                    objRecordContent.m_strSKULL = dtbValueRow["SKULL"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE = dtbValueRow["CAPUTSUCCEDANEUMSIZE"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEXML = dtbValueRow["CAPUTSUCCEDANEUMSIZEXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE1 = dtbValueRow["CAPUTSUCCEDANEUMPLACE1"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE1XML = dtbValueRow["CAPUTSUCCEDANEUMPLACE1XML"].ToString();
                    objRecordContent.m_strABDOMINALWALL_V = dtbValueRow["ABDOMINALWALL_V"].ToString();
                    objRecordContent.m_strABDOMINALWALL_VXML = dtbValueRow["ABDOMINALWALL_VXML"].ToString();
                    objRecordContent.m_strABDOMINALWALL_H = dtbValueRow["ABDOMINALWALL_H"].ToString();
                    objRecordContent.m_strABDOMINALWALL_HXML = dtbValueRow["ABDOMINALWALL_HXML"].ToString();
                    objRecordContent.m_strFASCIA = dtbValueRow["FASCIA"].ToString();
                    objRecordContent.m_strPERITONEUM = dtbValueRow["PERITONEUM"].ToString();
                    objRecordContent.m_strUTERUS = dtbValueRow["UTERUS"].ToString();
                    objRecordContent.m_strFETUSPLACE2 = dtbValueRow["FETUSPLACE2"].ToString();
                    objRecordContent.m_strFETUSPLACE2XML = dtbValueRow["FETUSPLACE2XML"].ToString();
                    objRecordContent.m_strENGAGEMENT = dtbValueRow["ENGAGEMENT"].ToString();
                    objRecordContent.m_strPRESENTATIONEXPULSION = dtbValueRow["PRESENTATIONEXPULSION"].ToString();
                    objRecordContent.m_dtmEXPULSIONTIME = Convert.ToDateTime(dtbValueRow["EXPULSIONTIME"]);
                    objRecordContent.m_strBABYSEX = dtbValueRow["BABYSEX"].ToString();
                    objRecordContent.m_strBABYWEIGHT = dtbValueRow["BABYWEIGHT"].ToString();
                    objRecordContent.m_strBABYWEIGHTXML = dtbValueRow["BABYWEIGHTXML"].ToString();
                    objRecordContent.m_strAPGAR = dtbValueRow["APGAR"].ToString();
                    objRecordContent.m_strAPGARXML = dtbValueRow["APGARXML"].ToString();
                    objRecordContent.m_strFETUSFACIES = dtbValueRow["FETUSFACIES"].ToString();
                    objRecordContent.m_strFETUSFACIESXML = dtbValueRow["FETUSFACIESXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEX = dtbValueRow["CAPUTSUCCEDANEUMSIZEX"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEXXML = dtbValueRow["CAPUTSUCCEDANEUMSIZEXXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEY = dtbValueRow["CAPUTSUCCEDANEUMSIZEY"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEYXML = dtbValueRow["CAPUTSUCCEDANEUMSIZEYXML"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE2 = dtbValueRow["CAPUTSUCCEDANEUMPLACE2"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE2XML = dtbValueRow["CAPUTSUCCEDANEUMPLACE2XML"].ToString();
                    objRecordContent.m_strAMNIOCENTESIS2 = dtbValueRow["AMNIOCENTESIS2"].ToString();
                    objRecordContent.m_strAMNIOCENTESISBULK = dtbValueRow["AMNIOCENTESISBULK"].ToString();
                    objRecordContent.m_strAMNIOCENTESISBULKXML = dtbValueRow["AMNIOCENTESISBULKXML"].ToString();                    
                    objRecordContent.m_strEMBRYOLEMMACIRCS = dtbValueRow["EMBRYOLEMMACIRCS"].ToString();
                    objRecordContent.m_strOVIDUCTCIRCS = dtbValueRow["OVIDUCTCIRCS"].ToString();
                    objRecordContent.m_strOVARYCIRCS = dtbValueRow["OVARYCIRCS"].ToString();
                    objRecordContent.m_strSUTUREUTERUS = dtbValueRow["SUTUREUTERUS"].ToString();
                    objRecordContent.m_strSUTUREUTERUSXML = dtbValueRow["SUTUREUTERUSXML"].ToString();
                    objRecordContent.m_strSUTUREABDOMINALWALL = dtbValueRow["SUTUREABDOMINALWALL"].ToString();
                    objRecordContent.m_strSUTUREABDOMINALWALLXML = dtbValueRow["SUTUREABDOMINALWALLXML"].ToString();
                    objRecordContent.m_strOXYTOCIN = dtbValueRow["OXYTOCIN"].ToString();
                    objRecordContent.m_strOXYTOCINXML = dtbValueRow["OXYTOCINXML"].ToString();
                    //---------------------------------------------------------
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE_YN = dtbValueRow["CAPUTSUCCEDANEUMSIZE_YN"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEY_YN = dtbValueRow["CAPUTSUCCEDANEUMSIZEY_YN"].ToString();
                    //---------------------------------------------------------
                    //objRecordContent.m_strIM = dtbValueRow["IM"].ToString();
                    //objRecordContent.m_strIMXML = dtbValueRow["IMXML"].ToString();
                    //objRecordContent.m_strIV = dtbValueRow["IV"].ToString();
                    //objRecordContent.m_strIVXML = dtbValueRow["IVXML"].ToString();
                    objRecordContent.m_strOTHERMEDICINE = dtbValueRow["OTHERMEDICINE"].ToString();
                    objRecordContent.m_strOTHERMEDICINEXML = dtbValueRow["OTHERMEDICINEXML"].ToString();
                    objRecordContent.m_strPISS = dtbValueRow["PISS"].ToString();
                    objRecordContent.m_strPISSXML = dtbValueRow["PISSXML"].ToString();
                    objRecordContent.m_strBLEEDING = dtbValueRow["BLEEDING"].ToString();
                    objRecordContent.m_strBLEEDINGXML = dtbValueRow["BLEEDINGXML"].ToString();
                    objRecordContent.m_strTRANSFUSE = dtbValueRow["TRANSFUSE"].ToString();
                    objRecordContent.m_strTRANSFUSEXML = dtbValueRow["TRANSFUSEXML"].ToString();
                    objRecordContent.m_strFETALHEARTSOUND = dtbValueRow["FETALHEARTSOUND"].ToString();
                    objRecordContent.m_strFETALHEARTSOUNDXML = dtbValueRow["FETALHEARTSOUNDXML"].ToString();
                    objRecordContent.m_strBP = dtbValueRow["BP"].ToString();
                    objRecordContent.m_strBPXML = dtbValueRow["BPXML"].ToString();
                    objRecordContent.m_strPLACENTACIRCSAFTEROP = dtbValueRow["PLACENTACIRCSAFTEROP"].ToString();
                    objRecordContent.m_strPLACENTACIRCSAFTEROPXML = dtbValueRow["PLACENTACIRCSAFTEROPXML"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROP = dtbValueRow["DIAGNOSISAFTEROP"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROPXML = dtbValueRow["DIAGNOSISAFTEROPXML"].ToString();
                    objRecordContent.m_strANAMODE = dtbValueRow["ANAMODE"].ToString();
                    objRecordContent.m_strANAMODEXML = dtbValueRow["ANAMODEXML"].ToString();
                    objRecordContent.m_strOPNAME = dtbValueRow["OPNAME"].ToString();
                    objRecordContent.m_strOPNAMEXML = dtbValueRow["OPNAMEXML"].ToString();
                    objRecordContent.m_strUnCheckBeforeOP = dtbValueRow["UNCHECKBEFOREOP"].ToString();
                    objRecordContent.m_strANATIME = dtbValueRow["ANATIME_VCHR"].ToString();
                    objRecordContent.m_strOPTime = dtbValueRow["OPTIME_VCHR"].ToString();

                    objRecordContent.m_strDIAGNOSISBEFOREOP_RIGHT = dtbValueRow["DIAGNOSISBEFOREOP_RIGHT"].ToString();
                    objRecordContent.m_strOPINDICATION_RIGHT = dtbValueRow["OPINDICATION_RIGHT"].ToString();
                    objRecordContent.m_strDIAGNOSISAFTEROP_RIGHT = dtbValueRow["DIAGNOSISAFTEROP_RIGHT"].ToString();
                    objRecordContent.m_strANAMODE_RIGHT = dtbValueRow["ANAMODE_RIGHT"].ToString();
                    objRecordContent.m_strOPNAME_RIGHT = dtbValueRow["OPNAME_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSHEIGHT_RIGHT = dtbValueRow["UTERUSHEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strABDOMENROUND_RIGHT = dtbValueRow["ABDOMENROUND_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION1_RIGHT = dtbValueRow["PRESENTATION1_RIGHT"].ToString();
                    objRecordContent.m_strFETUSWEIGHT_RIGHT = dtbValueRow["FETUSWEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strPUBICARCH_RIGHT = dtbValueRow["PUBICARCH_RIGHT"].ToString();
                    objRecordContent.m_strDC_RIGHT = dtbValueRow["DC_RIGHT"].ToString();
                    objRecordContent.m_strUTERUSORA_RIGHT = dtbValueRow["UTERUSORA_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATION2_RIGHT = dtbValueRow["PRESENTATION2_RIGHT"].ToString();
                    objRecordContent.m_strFETUSPLACE1_RIGHT = dtbValueRow["FETUSPLACE1_RIGHT"].ToString();
                    objRecordContent.m_strPRESENTATIONHEITHT_RIGHT = dtbValueRow["PRESENTATIONHEITHT_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMSIZE_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE1_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMPLACE1_RIGHT"].ToString();
                    objRecordContent.m_strABDOMINALWALL_V_RIGHT = dtbValueRow["ABDOMINALWALL_V_RIGHT"].ToString();
                    objRecordContent.m_strABDOMINALWALL_H_RIGHT = dtbValueRow["ABDOMINALWALL_H_RIGHT"].ToString();
                    objRecordContent.m_strFETUSPLACE2_RIGHT = dtbValueRow["FETUSPLACE2_RIGHT"].ToString();
                    objRecordContent.m_strBABYWEIGHT_RIGHT = dtbValueRow["BABYWEIGHT_RIGHT"].ToString();
                    objRecordContent.m_strAPGAR_RIGHT = dtbValueRow["APGAR_RIGHT"].ToString();
                    objRecordContent.m_strFETUSFACIES_RIGHT = dtbValueRow["FETUSFACIES_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEX_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMSIZEX_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMSIZEY_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMSIZEY_RIGHT"].ToString();
                    objRecordContent.m_strCAPUTSUCCEDANEUMPLACE2_RIGHT = dtbValueRow["CAPUTSUCCEDANEUMPLACE2_RIGHT"].ToString();
                    objRecordContent.m_strAMNIOCENTESISBULK_RIGHT = dtbValueRow["AMNIOCENTESISBULK_RIGHT"].ToString();                    
                    objRecordContent.m_strSUTUREUTERUS_RIGHT = dtbValueRow["SUTUREUTERUS_RIGHT"].ToString();
                    objRecordContent.m_strSUTUREABDOMINALWALL_RIGHT = dtbValueRow["SUTUREABDOMINALWALL_RIGHT"].ToString();
                    objRecordContent.m_strOXYTOCIN_RIGHT = dtbValueRow["OXYTOCIN_RIGHT"].ToString();
                    //objRecordContent.m_strIM_RIGHT = dtbValueRow["IM_RIGHT"].ToString();
                    //objRecordContent.m_strIV_RIGHT = dtbValueRow["IV_RIGHT"].ToString();
                    objRecordContent.m_strOTHERMEDICINE_RIGHT = dtbValueRow["OTHERMEDICINE_RIGHT"].ToString();
                    objRecordContent.m_strPISS_RIGHT = dtbValueRow["PISS_RIGHT"].ToString();
                    objRecordContent.m_strBLEEDING_RIGHT = dtbValueRow["BLEEDING_RIGHT"].ToString();
                    objRecordContent.m_strTRANSFUSE_RIGHT = dtbValueRow["TRANSFUSE_RIGHT"].ToString();
                    objRecordContent.m_strFETALHEARTSOUND_RIGHT = dtbValueRow["FETALHEARTSOUND_RIGHT"].ToString();
                    objRecordContent.m_strBP_RIGHT = dtbValueRow["BP_RIGHT"].ToString();
                    objRecordContent.m_strPLACENTACIRCSAFTEROP_RIGHT = dtbValueRow["PLACENTACIRCSAFTEROP_RIGHT"].ToString();

                    objRecordContent.m_strPLACENTA = dtbValueRow["PLACENTA"].ToString();
                    objRecordContent.m_strPLACENTA_RIGHT = dtbValueRow["PLACENTA_RIGHT"].ToString();
                    objRecordContent.m_strPLACENTAXML = dtbValueRow["PLACENTAXML"].ToString();
                    objRecordContent.m_strUMBILICALCORD = dtbValueRow["UMBILICALCORD"].ToString();
                    objRecordContent.m_strUMBILICALCORD_RIGHT = dtbValueRow["UMBILICALCORD_RIGHT"].ToString();
                    objRecordContent.m_strUMBILICALCORDXML = dtbValueRow["UMBILICALCORDXML"].ToString();
                    objRecordContent.m_strMEDICINEAFTEROP = dtbValueRow["MEDICINEAFTEROP"].ToString();
                    objRecordContent.m_strSUMARY4 = dtbValueRow["SUMARY4"].ToString();
                    objRecordContent.m_strSUMARY4_RIGHT = dtbValueRow["SUMARY4_RIGHT"].ToString();
                    objRecordContent.m_strSUMARY4XML = dtbValueRow["SUMARY4XML"].ToString();
                    objRecordContent.m_strASSISTANT1 = dtbValueRow["ASSISTANT1"].ToString();
                    objRecordContent.m_strASSISTANT2 = dtbValueRow["ASSISTANT2"].ToString();

                    //��ȡǩ������
                    if (dtbValueRow["SEQUENCE_INT"] != DBNull.Value)
                    {
                        long lngS = long.Parse(dtbValueRow["SEQUENCE_INT"].ToString());
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign = null;
                    }

                    p_objRecordContent = objRecordContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //����
            return lngRes;
        }
        #endregion
    }
}