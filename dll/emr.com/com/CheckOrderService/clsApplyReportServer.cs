using System;
using System.Collections;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.ApplyReportServer
{
    /// <summary>
    /// 申请报告单中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsApplyReportServer : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        private const string m_strGetRecordSql = @"select f.formclsname,
       f.formdesc,
       f.typestatus,
       f.formstatus,
       f.levelindex,
       ar.recordid,
       ar.formclsname,
       ar.patientid,
       ar.opendate,
       ar.ceateddate,
       ar.createduserid,
       ar.delstatus,
       ar.deluserid,
       ar.tempstatus,
       ar.sendstatus,
       ar.rela_recordid,
       ar.deldate,
       ar.readstatus,
       ar.modifyuserid,
       ar.firstprintdate,
       ar.otherstatus,
       c.ctl_content as patientname,
       d.ctl_content as checkpart,
       p1.ctl_content as clinicdiagnose,
       p2.ctl_content as sex,
       p3.ctl_content as age,
       p4.ctl_content as inpatientid,
       p5.ctl_content as bed,
       p11.ctl_content as areaname,
       p6.ctl_content as applydate,
       p7.ctl_content as doctor,
       p8.ctl_content as dept,
       p9.ctl_content as cardid,
       p10.ctl_content as pathologyid
  from ar_form f
 inner join ar_apply_report ar on f.formclsname = ar.formclsname
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtName') c on ar.recordid = c.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtCheckPart') d on d.recordid =
                                                       ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtClinicDiagnose') p1 on p1.recordid =
                                                             ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_cboSex') p2 on p2.recordid = ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtAge') p3 on p3.recordid = ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtInPatientID') p4 on p4.recordid =
                                                          ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtBed') p5 on p5.recordid = ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_dtpApplyDate') p6 on p6.recordid =
                                                        ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtApplyDoctor') p7 on p7.recordid =
                                                          ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtDept') p8 on p8.recordid =
                                                   ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtCardID') p9 on p9.recordid =
                                                     ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtPathologyID') p10 on p10.recordid =
                                                           ar.recordid
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtArea') p11 on p11.recordid =
                                                    ar.recordid";
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="p_arlSQL"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveData(System.Collections.Generic.List<string> p_arlSQL)
        {
            long lngRes = -1;

            clsHRPTableService objHRPServer = new clsHRPTableService();

            for (int i = 0; i < p_arlSQL.Count; i++)
            {
                if (p_arlSQL[i].Trim().Length > 0)
                {
                    try
                    {
                        lngRes = objHRPServer.DoExcute(p_arlSQL[i]);
                    }
                    catch (Exception objEx)
                    {
                        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (lngRes <= 0)
                    {
                        System.EnterpriseServices.ContextUtil.SetAbort();
                        return lngRes;
                    }
                }
            }
            ////objHRPServer.Dispose();
            return lngRes;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_objPictureBoxValue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveDataToPic(string p_strSQL, clsPictureBoxValue[] p_objPictureBoxValue)
        {
            long lngRet = -1;
            long lngEff = 0;

            if (p_strSQL.Trim().Length <= 0)
                return lngRet;

            if (p_objPictureBoxValue == null)
                return lngRet;

            clsHRPTableService objHRPServer = new clsHRPTableService();
            IDataParameter[] objDPArr3 = null;

            objHRPServer.CreateDatabaseParameter(4, out objDPArr3);


            for (int i = 0; i < p_objPictureBoxValue.Length; i++)
            {
                //按顺序给IDataParameter赋值
                //				for(int j=0;j<objDPArr3.Length;j++)
                //					objDPArr3[j]=new OracleParameter();

                objDPArr3[0].Value = i.ToString();
                objDPArr3[1].DbType = DbType.Binary;
                if (p_objPictureBoxValue[i].m_bytImage != null)
                    objDPArr3[1].Value = p_objPictureBoxValue[i].m_bytImage;
                else
                    objDPArr3[1].Value = DBNull.Value;
                objDPArr3[2].Value = p_objPictureBoxValue[i].intHeight;
                objDPArr3[3].Value = p_objPictureBoxValue[i].intWidth;

                lngEff = 0;

                try
                {
                    lngRet = objHRPServer.lngExecuteParameterSQL(p_strSQL, ref lngEff, objDPArr3);
                }
                catch (Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                if (lngRet <= 0)
                {
                    ////objHRPServer.Dispose();
                    return lngRet;
                }
            }
            ////objHRPServer.Dispose();
            return 1;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtRecords"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetData(string p_strSQL, ref DataTable p_dtRecords)
        {
            p_dtRecords = null;
            long lngRes = -1;

            if (p_strSQL.Trim().Length <= 0)
                return lngRes;

            clsHRPTableService objHRPServer = new clsHRPTableService();

            try
            {
                lngRes = objHRPServer.lngGetDataTableWithoutParameters(p_strSQL, ref p_dtRecords);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                ////objHRPServer.Dispose();
            }

            return lngRes;
        }

        #region 获取表单和数据
        /// <summary>
        /// 通过SQL获取记录
        /// </summary>
        /// <param name="p_strSql"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordBySQL(string p_strSql, out clsApplyReportList_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (p_strSql == null || p_strSql == string.Empty)
                return -1;
            p_objRecordArr = m_objGetRecord(p_strSql);
            if (p_objRecordArr != null)
                return 1;
            return 0;
        }
        /// <summary>
        /// 根据记录ID获取记录信息
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordByRecID(string p_strRecordID, out clsApplyReportList_VO p_objRecord)
        {
            p_objRecord = null;
            if (p_strRecordID == null || p_strRecordID == string.Empty)
                return -1;
            string strSql = m_strGetRecordSql + @" where ar.recordid = '" + p_strRecordID.Trim() + "' and ar.readstatus  = '0' and ar.delstatus =0 order by ar.recordid";
            clsApplyReportList_VO[] objRecordArr = m_objGetRecord(strSql);
            if (objRecordArr != null)
            {
                p_objRecord = objRecordArr[0];
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// 获取某张单所有记录
        /// </summary>
        /// <param name="p_strFormClsName"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordByForm(string p_strFormClsName, out clsApplyReportList_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (p_strFormClsName == null || p_strFormClsName == string.Empty)
                return -1;
            string strSql = m_strGetRecordSql + @" where ar.formclsname = '" + p_strFormClsName + "' and ar.readstatus  = '0' and ar.delstatus =0  order by ar.recordid";
            p_objRecordArr = m_objGetRecord(strSql);
            if (p_objRecordArr != null)
                return 1;
            return 0;
        }
        /// <summary>
        ///  根据住院号某张单最新记录
        /// </summary>
        /// <param name="p_strFormClsName"></param>
        /// <param name="p_strInpatientID"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordByFormAndInpID(string p_strFormClsName, string p_strInpatientID, out clsApplyReportList_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (p_strFormClsName == null || p_strFormClsName == string.Empty || p_strInpatientID == null)
                return -1;
            string strSql = m_strGetRecordSql + @" where ar.recordid = (select max(b.recordid) from ar_content a  inner join  ar_apply_report b on a.recordid = b.recordid
 where controlid = 'm_txtInPatientID'and b.readstatus  = '0' and b.delstatus =0
  and b.formclsname = '" + p_strFormClsName + "' and a.ctl_content = '" + p_strInpatientID + "')";
            p_objRecordArr = m_objGetRecord(strSql);
            if (p_objRecordArr != null)
                return 1;
            return 0;
        }

        /// <summary>
        /// 根据日期段获取某张单所有记录
        /// </summary>
        /// <param name="p_strFormClsName"></param>
        /// <param name="p_strFirstTime"></param>
        /// <param name="p_strLastTime"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordByFormTimeslice(string p_strFormClsName, string p_strFirstTime, string p_strLastTime, out clsApplyReportList_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (p_strFormClsName == null || p_strFormClsName == string.Empty || p_strFirstTime == null || p_strLastTime == null)
                return -1;
            string strSql = m_strGetRecordSql + @" where ar.formclsname = '" + p_strFormClsName + "' and ar.readstatus  = '0' and ar.delstatus =0 and ar.ceateddate between " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strFirstTime) + " and "
                + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strLastTime) + "  order by ar.ceateddate";
            p_objRecordArr = m_objGetRecord(strSql);
            if (p_objRecordArr != null)
                return 1;
            return 0;
        }
        /// <summary>
        /// 获取某病人某张单的记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_strFormClsName"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordByPatientForm(string p_strPatientID, string p_strFormClsName, out clsApplyReportList_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (p_strPatientID == null || p_strPatientID == string.Empty || p_strFormClsName == null || p_strFormClsName == string.Empty)
                return -1;
            string strSql = m_strGetRecordSql + @" where ar.patientid = '" + p_strPatientID + "' and f.formclsname = '" + p_strFormClsName + "' and ar.readstatus  = '0' and ar.delstatus =0  order by ar.ceateddate";
            p_objRecordArr = m_objGetRecord(strSql);
            if (p_objRecordArr != null)
                return 1;
            return 0;
        }
        /// <summary>
        /// 获取某病人所有的记录
        /// </summary>
        /// <param name="p_strPatientID"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordByPatient(string p_strPatientID, out clsApplyReportList_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            if (p_strPatientID == null || p_strPatientID == string.Empty)
                return -1;
            string strSql = m_strGetRecordSql + @" where ar.patientid = '" + p_strPatientID + "' and ar.readstatus  = '0' and ar.delstatus =0   order by ar.ceateddate";
            p_objRecordArr = m_objGetRecord(strSql);
            if (p_objRecordArr != null)
                return 1;
            return 0;
        }
        /// <summary>
        /// 获取申请单或报告单的所有记录
        /// </summary>
        /// <param name="p_intTypeStatus"></param>
        /// <param name="p_objRecordArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordByType(int p_intTypeStatus, out clsApplyReportList_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            string strSql = m_strGetRecordSql + @" where ar.readstatus  = '0' and ar.delstatus =0";
            if (p_intTypeStatus >= 0 && p_intTypeStatus <= 2)
                strSql += " and f.typestatus = '" + p_intTypeStatus.ToString() + "'";
            p_objRecordArr = m_objGetRecord(strSql);
            if (p_objRecordArr != null)
                return 1;
            return 0;
        }
        /// <summary>
        /// 根据日期段获取申请单或报告单的所有记录
        /// </summary>
        /// <param name="p_intTypeStatus">0－申请单；1－报告单</param>
        /// <param name="p_strFirstTime">开始时间</param>
        /// <param name="p_strLastTime">结束时间</param>
        /// <param name="p_objRecordArr">记录列表</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARRecordByTimeslice(int p_intTypeStatus, string p_strFirstTime, string p_strLastTime, out clsApplyReportList_VO[] p_objRecordArr)
        {
            p_objRecordArr = null;
            string strSql = m_strGetRecordSql + @" where ar.readstatus  = '0' and ar.delstatus =0 and ar.ceateddate between " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strFirstTime) + " and " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strLastTime);
            if (p_intTypeStatus >= 0 && p_intTypeStatus <= 2)
                strSql += " and f.typestatus = '" + p_intTypeStatus.ToString() + "'";
            p_objRecordArr = m_objGetRecord(strSql);
            if (p_objRecordArr != null)
                return 1;
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strSql"></param>
        /// <returns></returns>
        [AutoComplete]
        private clsApplyReportList_VO[] m_objGetRecord(string p_strSql)
        {
            if (p_strSql == string.Empty || p_strSql == null)
                return null;
            DataTable dtResult = new DataTable();
            clsHRPTableService objHRPServer = new clsHRPTableService();
            long lngRes = -1;

            try
            {
                lngRes = objHRPServer.DoGetDataTable(p_strSql, ref dtResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            ////objHRPServer.Dispose();
            if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                return null;
            clsApplyReportList_VO[] objContentArr = new clsApplyReportList_VO[dtResult.Rows.Count];
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                objContentArr[i] = new clsApplyReportList_VO();
                objContentArr[i].m_strRecordID = dtResult.Rows[i]["RECORDID"].ToString().Trim();
                objContentArr[i].m_objAR_Form_VO = new clsAR_Form_VO();
                objContentArr[i].m_objAR_Form_VO.m_strAR_FormClsName = dtResult.Rows[i]["FORMCLSNAME"].ToString().Trim();
                objContentArr[i].m_objAR_Form_VO.m_strAR_FormDesc = dtResult.Rows[i]["FORMDESC"].ToString().Trim();
                objContentArr[i].m_objAR_Form_VO.m_strAR_FormType = dtResult.Rows[i]["TYPESTATUS"].ToString().Trim();
                objContentArr[i].m_strCreateDate = dtResult.Rows[i]["CEATEDDATE"].ToString().Trim();
                objContentArr[i].m_StrPatientID = dtResult.Rows[i]["PATIENTID"].ToString().Trim();
                objContentArr[i].m_StrPatientName = dtResult.Rows[i]["PatientName"].ToString().Trim();//用保存的控件值，没有为空
                objContentArr[i].m_strSendStatus = dtResult.Rows[i]["SENDSTATUS"].ToString().Trim();
                objContentArr[i].m_strAR_FormType = dtResult.Rows[i]["TYPESTATUS"].ToString().Trim();
                objContentArr[i].m_strCheckPart = dtResult.Rows[i]["CHECKPART"].ToString().Trim();

                if (dtResult.Rows[i]["AREANAME"] == DBNull.Value)
                    objContentArr[i].m_StrAreaName = "";//dtResult.Rows[i]["TYPESTATUS"].ToString().Trim();
                else
                    objContentArr[i].m_StrAreaName = dtResult.Rows[i]["AREANAME"].ToString().Trim();
                objContentArr[i].m_StrBedName = dtResult.Rows[i]["BED"].ToString().Trim();
                objContentArr[i].m_StrClinicDiagnose = dtResult.Rows[i]["CLINICDIAGNOSE"].ToString().Trim();
                objContentArr[i].m_StrDeptName = dtResult.Rows[i]["DEPT"].ToString().Trim();
                try
                {
                    objContentArr[i].m_DtmAppryDate = DateTime.Parse(dtResult.Rows[i]["APPLYDATE"].ToString());
                }
                catch { objContentArr[i].m_DtmAppryDate = DateTime.Now; }
                objContentArr[i].m_StrDoctor = dtResult.Rows[i]["DOCTOR"].ToString().Trim();
                objContentArr[i].m_StrInPatientID = dtResult.Rows[i]["INPATIENTID"].ToString().Trim();
                objContentArr[i].m_StrPatientAge = dtResult.Rows[i]["AGE"].ToString().Trim();
                objContentArr[i].m_StrPatientCardID = dtResult.Rows[i]["CARDID"].ToString().Trim();
                objContentArr[i].m_StrPatientSex = dtResult.Rows[i]["SEX"].ToString().Trim();
                objContentArr[i].m_StrPathologyID = dtResult.Rows[i]["PATHOLOGYID"].ToString().Trim();

                objContentArr[i].m_objRelaFormArr = m_objGetRelaForm(objContentArr[i].m_strRecordID);
            }
            ////objHRPServer.Dispose();
            return objContentArr;
        }
        /// <summary>
        /// 获取关联的报告单
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <returns></returns>
        [AutoComplete]
        private clsAR_RelaForm_VO[] m_objGetRelaForm(string p_strRecordID)
        {
            if (p_strRecordID == null || p_strRecordID == string.Empty)
                return null;
            string strSql = @"select f.formclsname,
       f.formdesc,
       f.typestatus,
       f.formstatus,
       f.levelindex,
       ar.recordid,
       c.ctl_content as patientname
  from ar_form f
 inner join ar_apply_report ar on f.formclsname = ar.formclsname
  left join (select recordid, controlid, ctl_content, ctl_content_xml
               from ar_content
              where controlid = 'm_txtName') c on ar.recordid = c.recordid
 where ar.rela_recordid = '1'
   and f.typeStatus = 1
   and ar.readStatus = 0
   and ar.delStatus = 0";
            DataTable dtResult = new DataTable();
            clsHRPTableService objHRPServer = new clsHRPTableService();

            long lngRes = -1;
            try
            {
                lngRes = objHRPServer.DoGetDataTable(strSql, ref dtResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            ////objHRPServer.Dispose();
            if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                return null;
            clsAR_RelaForm_VO[] objContentArr = new clsAR_RelaForm_VO[dtResult.Rows.Count];
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                objContentArr[i] = new clsAR_RelaForm_VO();
                objContentArr[i].m_strRela_RecordID = dtResult.Rows[i]["RECORDID"].ToString().Trim();
                objContentArr[i].m_objAR_Form_VO = new clsAR_Form_VO();
                objContentArr[i].m_objAR_Form_VO.m_strAR_FormClsName = dtResult.Rows[i]["FORMCLSNAME"].ToString().Trim();
                objContentArr[i].m_objAR_Form_VO.m_strAR_FormDesc = dtResult.Rows[i]["FORMDESC"].ToString().Trim();
                objContentArr[i].m_objAR_Form_VO.m_strAR_FormType = "1";
            }
            return objContentArr;
        }
        /// <summary>
        /// 获取表单
        /// </summary>
        /// <param name="p_intTypeStatus">0－申请单；1－报告单；2－其它</param>
        /// <param name="p_objApplyFormArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetARForm(int p_intTypeStatus, out clsAR_Form_VO[] p_objApplyFormArr)
        {
            p_objApplyFormArr = null;
            long lngRes = -1;
            string strSql = @"select formclsname, formdesc, typestatus, formstatus, levelindex from AR_FORM  where formStatus =0";
            if (p_intTypeStatus >= 0 && p_intTypeStatus <= 2)
                strSql += " and typestatus = '" + p_intTypeStatus.ToString() + "'";
            strSql += "order by levelindex";
            DataTable dtResult = new DataTable();
            clsHRPTableService objHRPServer = new clsHRPTableService();

            try
            {
                lngRes = objHRPServer.DoGetDataTable(strSql, ref dtResult);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            ////objHRPServer.Dispose();
            if (lngRes <= 0 || dtResult.Rows.Count <= 0)
                return 0;
            p_objApplyFormArr = new clsAR_Form_VO[dtResult.Rows.Count];
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                p_objApplyFormArr[i] = new clsAR_Form_VO();
                p_objApplyFormArr[i].m_strAR_FormClsName = dtResult.Rows[i]["FORMCLSNAME"].ToString().Trim();
                p_objApplyFormArr[i].m_strAR_FormDesc = dtResult.Rows[i]["FORMDESC"].ToString().Trim();
                p_objApplyFormArr[i].m_strAR_FormType = dtResult.Rows[i]["TYPESTATUS"].ToString().Trim();
                p_objApplyFormArr[i].m_strAR_FormIndex = dtResult.Rows[i]["LEVELINDEX"].ToString().Trim();
            }
            return lngRes;
        }
        /// <summary>
        /// 获取某记录的图片
        /// </summary>
        /// <param name="p_strRecordID"></param>
        /// <param name="p_objPicArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAR_PicValue(string p_strRecordID, out clsPictureBoxValue[] p_objPicArr)
        {
            p_objPicArr = null;
            long lngRes = -1;
            if (p_strRecordID == null || p_strRecordID == string.Empty)
                return -1;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            string SQL = "select recordid,controlid,imageid,imagecontent,height,width from ar_image where recordid=?";
            DataTable dtRecords = null;
            IDataParameter[] objDPArr = null;
            objHRPServer.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strRecordID.Trim();

            try
            {
                lngRes = objHRPServer.lngGetDataTableWithParameters(SQL, ref dtRecords, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            ////objHRPServer.Dispose();
            if (lngRes <= 0 || dtRecords == null || dtRecords.Rows.Count == 0)
                return 0;
            p_objPicArr = new clsPictureBoxValue[dtRecords.Rows.Count];
            for (int i = 0; i < dtRecords.Rows.Count; i++)
            {
                p_objPicArr[i] = new clsPictureBoxValue();
                try
                {
                    System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])(dtRecords.Rows[i]["IMAGECONTENT"]));
                    p_objPicArr[i].m_imgBack = new System.Drawing.Bitmap(objStream);
                }
                catch { p_objPicArr[i].m_imgBack = new System.Drawing.Bitmap(32, 32); }
                try
                {
                    p_objPicArr[i].intWidth = Convert.ToInt32(dtRecords.Rows[i]["WIDTH"]);
                }
                catch { p_objPicArr[i].intWidth = 100; }
                try
                {
                    p_objPicArr[i].intHeight = Convert.ToInt32(dtRecords.Rows[i]["HEIGHT"]);
                }
                catch { p_objPicArr[i].intHeight = 100; }

            }
            return lngRes;
        }
        #endregion 获取表单和数据

        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="p_strRecordID">记录ID</param>
        [AutoComplete]
        public long m_lngSetReferStatus(string p_strRecordID)
        {
            long lngRes = -1;
            if (p_strRecordID == null || p_strRecordID == string.Empty)
                return -1;
            string strSql = @"update ar_apply_report set sendstatus =1 where recordid = ?";
            clsHRPTableService objHRPServer = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServer.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strRecordID.Trim();

            try
            {
                long lngEff = -1;
                lngRes = objHRPServer.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                ////objHRPServer.Dispose();
            }

            return lngRes;
        }
        /// <summary>
        /// 设置表单状态为已读
        /// </summary>
        /// <param name="p_strRecordID">记录ID</param>
        [AutoComplete]
        public long m_lngSetReadStatus(string p_strRecordID)
        {
            if (p_strRecordID == null || p_strRecordID == string.Empty)
                return -1;
            string strSql = @"update ar_apply_report set readstatus =1 where recordid = ?";
            clsHRPTableService objHRPServer = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServer.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strRecordID.Trim();

            long lngRes = -1;
            try
            {
                long lngEff = -1;
                lngRes = objHRPServer.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                ////objHRPServer.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 判断表单是否已经初始化过
        /// </summary>
        /// <param name="p_strFormClsName"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnIsInitForm(string p_strFormClsName)
        {
            if (p_strFormClsName == null || p_strFormClsName == string.Empty)
                return false;
            string strSql = @"select count(t.formclsname) as count from ar_control_desc t where t.formclsname = ?";

            clsHRPTableService objHRPServer = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServer.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strFormClsName.Trim();

            DataTable dtRecords = null;
            long lngRes = -1;

            try
            {
                lngRes = objHRPServer.lngGetDataTableWithParameters(strSql, ref dtRecords, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                ////objHRPServer.Dispose();
            }

            if (lngRes > 0 && dtRecords != null)
            {
                if (dtRecords.Rows[0]["COUNT"].ToString().Trim() != "0")
                    return true;
            }
            return false;
        }


    }
}
