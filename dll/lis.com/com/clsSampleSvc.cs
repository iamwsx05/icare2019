using System;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.Utility; //Utility.dll
using Microsoft.VisualBasic;
using System.Text;
using System.Data; 

namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsSampleSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        //#region	���ݱ걾�Ų�ѯ�걾״̬
        //[AutoComplete]
        //public long m_lngFindStatusBySampleID( string p_strSampleID,out int p_intStatus)
        //{
        //    p_intStatus = 8;
        //    long lngRes = 0;
        //    clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //    lngRes = objPrivilege.m_lngCheckCallPrivilege(objPrincipal,"com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngFindStatusBySampleID");
        //    if(lngRes < 0)
        //    {
        //        return -1;
        //    }

        //    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

        //    string strSQL = @"select status_int from t_opr_lis_sample t where sample_id_chr = '" + p_strSampleID + "'";

        //    try
        //    {
        //        lngRes = 0;
        //        DataTable dtResult = new DataTable();
        //        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtResult);
        //        if( lngRes < 0 )
        //        {
        //            return -1;
        //        }

        //        if( dtResult != null && dtResult.Rows.Count > 0 )
        //        {
        //            string strStatus = dtResult.Rows[0][0].ToString();
        //            p_intStatus = int.Parse(strStatus);
        //        }
        //    }
        //    catch(Exception objEx)
        //    {
        //        string strTemp = objEx.Message;
        //        clsLogText objLogger = new clsLogText();
        //        bool blnRes = objLogger.LogError(objEx);
        //    }

        //    return lngRes;
        //}
        //#endregion

        #region [U] �޸ļ���汾��
        [AutoComplete]
        public long m_lngModifyBarCode( string strSampleID, string strAppID)
        {
            long lngRes = 0; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

            string strDelSQL = @"delete from t_opr_lis_sample where sample_id_chr = '" + strSampleID + "'";
            string strUpdateSQL = @"update t_opr_lis_app_sample set sample_id_chr ='' where application_id_chr = '" + strAppID + "'";

            try
            {
                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strDelSQL);
                if (lngRes < 0)
                {
                    return -1;
                }

                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strUpdateSQL);
                if (lngRes < 0)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ����������־λ
        /// <summary>
        /// ����������־λ
        ///  
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleIDArr"></param>
        /// <param name="p_intSourceStatus"></param>
        /// <param name="p_intTargetStatus"></param>
        /// <returns>
        /// С�ڵ���0:����;
        ///1:�ɹ���
        /// </returns>
        [AutoComplete]
        public long m_lngUpdateSampleFlag(
            string[] p_strSampleIDArr, int p_intSourceStatus, int p_intTargetStatus)
        {
            long lngRes = 0;
            long lngEff = 0;
            if (p_strSampleIDArr == null || p_strSampleIDArr.Length == 0)
                return 1;

            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < p_strSampleIDArr.Length; i++)
            {
                sb.Append("'");
                sb.Append(p_strSampleIDArr[i]);
                sb.Append("'");
                sb.Append(",");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            string strSampleIDs = sb.ToString();

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            //change by wjqin(07-4-20)
            //            string strSQL = @"UPDATE t_opr_lis_sample SET status_int = " + p_intTargetStatus + @" 
            //							WHERE  status_int = " + p_intSourceStatus.ToString() + " AND sample_id_chr IN(" + strSampleIDs + ")";
            //            try
            //            {
            //                lngRes = objHRPSvc.DoExcute(strSQL);
            //                objHRPSvc.Dispose();				
            //            }
            //            catch(Exception objEx)
            //            {
            //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            //                lngRes = 0;
            //            }
            //            return lngRes;

            /*====================================>*/
            string strSQL = @"update t_opr_lis_sample set status_int = ?
							where  status_int = ? and sample_id_chr =?  ";

            int n = 0;

            DbType[] dbTypes = new DbType[] { 
                        DbType.Int16,DbType.Int16,DbType.String
                       
                        };
            object[][] objValues = new object[3][];


            for (int j = 0; j < objValues.Length; j++)
            {
                objValues[j] = new object[p_strSampleIDArr.Length];//��ʼ��
            }
            for (int k1 = 0; k1 < p_strSampleIDArr.Length; k1++)
            {

                n = -1;
                //��ˮ��
                objValues[++n][k1] = p_intTargetStatus;
                objValues[++n][k1] = p_intSourceStatus;
                objValues[++n][k1] = p_strSampleIDArr[k1].ToString().PadRight(10, ' ');

                //��������


            }
            try
            {

                if (p_strSampleIDArr.Length > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    lngRes = 1;
                }

                /*<============================================*/




                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
                lngRes = 0;
            }
            return lngRes;

        }
        /// <summary>
        /// ����������־λ
        ///  ���� 2004.10.31
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleIDArr"></param>
        /// <param name="p_intSourceStatus"></param>
        /// <param name="p_intTargetStatus"></param>
        /// <returns>
        /// С�ڵ���0:����;
        ///1:�ɹ���
        /// </returns>
        [AutoComplete]
        public long m_lngUpdateSampleFlag(
            string[] p_strSampleIDArr, int p_intSourceStatus, int p_intTargetStatus, string p_strOriginDate)
        {
            long lngRes = 0;
            long lngEff = 0;
            if (p_strSampleIDArr == null || p_strSampleIDArr.Length == 0)
                return 1;

            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < p_strSampleIDArr.Length; i++)
            {
                sb.Append("'");
                sb.Append(p_strSampleIDArr[i]);
                sb.Append("'");
                sb.Append(",");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            string strSampleIDs = sb.ToString();

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            //change by wjqin(07-4-20)
            //            string strSQL = @"UPDATE t_opr_lis_sample SET status_int = " + p_intTargetStatus + @" 
            //							WHERE  status_int = " + p_intSourceStatus.ToString() + " AND sample_id_chr IN(" + strSampleIDs + ")";
            //            try
            //            {
            //                DateTime dtm = Convert.ToDateTime(p_strOriginDate);
            //                strSQL = strSQL + @" AND modify_dat >= to_date('" + p_strOriginDate + "','yyyy-mm-dd hh24:mi:ss')";
            //            }
            //            catch{}

            //try
            //{
            //    lngRes = objHRPSvc.DoExcute(strSQL);
            //    objHRPSvc.Dispose();
            //}
            //catch (Exception objEx)
            //{
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            //    lngRes = 0;
            //}
            //return lngRes;
            /*====================================>*/
            string strSQL = @"update t_opr_lis_sample set status_int = ?
							where  status_int = ? and sample_id_chr =? and modify_dat >=? ";

            int n = 0;

            DbType[] dbTypes = new DbType[] { 
                        DbType.Int16,DbType.Int16,DbType.String,DbType.DateTime
                       
                        };
            object[][] objValues = new object[4][];


            for (int j = 0; j < objValues.Length; j++)
            {
                objValues[j] = new object[p_strSampleIDArr.Length];//��ʼ��
            }
            DateTime m_dtTime;
            DateTime.TryParse(p_strOriginDate, out m_dtTime);
            for (int k1 = 0; k1 < p_strSampleIDArr.Length; k1++)
            {

                n = -1;
                //��ˮ��
                objValues[++n][k1] = p_intTargetStatus;
                objValues[++n][k1] = p_intSourceStatus;
                objValues[++n][k1] = p_strSampleIDArr[k1].ToString().PadRight(10, ' ');
                objValues[++n][k1] = m_dtTime;
                //��������


            }
            try
            {

                if (p_strSampleIDArr.Length > 0)
                {
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
                    lngRes = 1;
                }

                /*<============================================*/




                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
                lngRes = 0;
            }
            return lngRes;
        }
        #endregion

        #region �걾����
        //        #region ����BarCode��ѯ�����յ�������Ϣ
        //        /// <summary>
        //        /// ����BarCode��ѯ�����յ�������Ϣ
        //        /// </summary>
        //        [AutoComplete]
        //        public long m_mthGetUnReceivedSampleByBarCode(string p_strBarCode,
        //            out clsSampleReceive_VO p_objRecord)
        //        {
        //            long lngRes=0;
        //            p_objRecord = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_mthGetUnReceivedSampleByBarCode");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"SELECT a.appl_dat, a.barcode_vchr, a.patient_type_chr, a.sampletype_vchr, 
        //									 a.accept_dat,a.sample_id_chr, a.status_int, a.acceptor_id_chr,
        //									 b.patient_name_vchr,b.sex_chr,b.age_chr,
        //									 b.patientid_chr,b.patientcardid_chr,b.patient_inhospitalno_chr,b.patient_subno_chr,
        //									 b.appl_deptid_chr,b.appl_empid_chr,b.bedno_chr,b.application_id_chr,
        //									 b.check_content_vchr,  b.special_int, b.emergency_int,
        //									 c.lastname_vchr, e.dictname_vchr
        //								FROM t_opr_lis_sample a,
        //									 t_opr_lis_application b,
        //									 t_bse_employee c,
        //									 (SELECT d.dictid_chr, d.dictname_vchr
        //										FROM t_aid_dict d
        //									   WHERE TRIM (d.dictid_chr) <> 0 AND dictkind_chr = '61') e
        //							   WHERE a.application_id_chr = b.application_id_chr
        //								 AND a.patient_type_chr = e.dictid_chr(+)
        //								 AND b.pstatus_int > 0
        //								 AND a.status_int > 0
        //								 AND a.acceptor_id_chr = c.empid_chr(+)
        //								 AND a.barcode_vchr = '" + p_strBarCode + "' order by appl_dat desc";
        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    clsVOConstructor objConstructor = new clsVOConstructor();
        //                    p_objRecord = objConstructor.m_mthContructSampleReceiveVO(dtbResult.Rows[0]);
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����������ѯ�ѽ��յı걾��Ϣ
        //        [AutoComplete]
        //        public long m_lngGetReceivedSampleByCondition(string p_strDatFrom,string p_strDatTo,
        //            string p_strSampleType,string p_strAcceptEmp,string p_strPatientName,string p_strPatientCardID,string p_strBarCode,string p_strCheckCategory, out clsSampleReceive_VO[] p_objResultArr)
        //        {
        //            long lngRes=0;
        //            p_objResultArr = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetReceivedSampleByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"SELECT a.barcode_vchr, a.patient_type_chr, a.sampletype_vchr, 
        //									 a.accept_dat,a.sample_id_chr, a.status_int, a.acceptor_id_chr,
        //									 b.patient_name_vchr,b.sex_chr,b.age_chr,
        //									 b.patientid_chr,b.patientcardid_chr,b.patient_inhospitalno_chr,b.patient_subno_chr,
        //									 b.appl_deptid_chr,b.appl_empid_chr,b.bedno_chr,b.application_id_chr,
        //									 b.check_content_vchr,  b.special_int, b.emergency_int,
        //									 c.lastname_vchr, e.dictname_vchr
        //								FROM t_opr_lis_sample a, 
        //									 t_opr_lis_application b,
        //								     t_bse_employee c,
        //									 (SELECT d.dictid_chr, d.dictname_vchr
        //										FROM t_aid_dict d
        //									   WHERE TRIM (d.dictid_chr) <> 0 AND dictkind_chr = '61') e
        //							   WHERE a.application_id_chr = b.application_id_chr
        //								 AND b.pstatus_int > 0
        //								 AND a.patient_type_chr = e.dictid_chr(+)
        //								 AND a.acceptor_id_chr = c.empid_chr(+)
        //								 AND a.status_int > 2
        //								 AND a.status_int < 7
        //								 AND a.accept_dat between TO_DATE('"+p_strDatFrom+@"','yyyy-mm-dd hh24:mi:ss') 
        //													  and TO_DATE('"+p_strDatTo+@"','yyyy-mm-dd hh24:mi:ss')";

        //            if(p_strSampleType != null && p_strSampleType != "")
        //            {
        //                strSQL += " AND a.sampletype_vchr = '"+p_strSampleType+@"'";
        //            }

        //            if(p_strAcceptEmp != null && p_strAcceptEmp != "")
        //            {
        //                strSQL += " AND a.acceptor_id_chr = '"+p_strAcceptEmp+@"'";
        //            }

        //            if( p_strPatientName != null && p_strPatientName.Trim() != "" )
        //            {
        //                strSQL += " AND b.PATIENT_NAME_VCHR like '%" + p_strPatientName + "%'";
        //            }

        //            if( p_strPatientCardID != null && p_strPatientCardID.Trim() != "" )
        //            {
        //                strSQL += " AND b.PATIENTCARDID_CHR = '" + p_strPatientCardID + "'";
        //            }

        //            if( p_strBarCode != null && p_strBarCode.Trim() != "" )
        //            {
        //                strSQL += " AND a.BARCODE_VCHR = '" + p_strBarCode + "'";
        //            }

        //            if (p_strCheckCategory != null && p_strCheckCategory.Trim() != "")   //�������Զ����������ѯ  baojian.mo 2007.09.10 add
        //            {
        //                strSQL += @" and b.check_content_vchr in (select t2.apply_unit_name_vchr
        //                              from t_aid_lis_appuser_group_detail t1,
        //                                   t_aid_lis_apply_unit t2,
        //                                   (select a.user_group_id_chr, a.user_group_name_vchr
        //                                      from t_aid_lis_appuser_group a
        //                                     where not exists (
        //                                                     select child_user_group_id_chr
        //                                                       from t_aid_lis_appuser_group_relate b
        //                                                      where a.user_group_id_chr =
        //                                                                                 b.child_user_group_id_chr)
        //                                       and a.user_group_name_vchr = '" + p_strCheckCategory + @"') t3
        //                             where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //                               and t1.user_group_id_chr = t3.user_group_id_chr
        //                            union all
        //                            select t2.apply_unit_name_vchr
        //                              from t_aid_lis_appuser_group_detail t1,
        //                                   t_aid_lis_apply_unit t2,
        //                                   (select t4.child_user_group_id_chr
        //                                      from t_aid_lis_appuser_group_relate t4,
        //                                           (select a.user_group_id_chr, a.user_group_name_vchr
        //                                              from t_aid_lis_appuser_group a
        //                                             where not exists (
        //                                                      select child_user_group_id_chr
        //                                                        from t_aid_lis_appuser_group_relate b
        //                                                       where a.user_group_id_chr =
        //                                                                                 b.child_user_group_id_chr)) t5
        //                                     where t4.user_group_id_chr = t5.user_group_id_chr
        //                                       and t5.user_group_name_vchr = '" + p_strCheckCategory + @"') t3
        //                             where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //                               and t1.user_group_id_chr = t3.child_user_group_id_chr
        //                            union all
        //                            select t2.apply_unit_name_vchr
        //                              from t_aid_lis_appuser_group_detail t1,
        //                                   t_aid_lis_apply_unit t2,
        //                                   (select t4.child_user_group_id_chr
        //                                      from t_aid_lis_appuser_group_relate t4,
        //                                           (select a.user_group_id_chr, a.user_group_name_vchr
        //                                              from t_aid_lis_appuser_group a
        //                                             where not exists (
        //                                                      select child_user_group_id_chr
        //                                                        from t_aid_lis_appuser_group_relate b
        //                                                       where a.user_group_id_chr =
        //                                                                                 b.child_user_group_id_chr)) t5
        //                                     where t4.user_group_id_chr = t5.user_group_id_chr
        //                                       and t5.user_group_name_vchr = '" + p_strCheckCategory + @"') t3
        //                             where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //                               and t1.user_group_id_chr = t3.child_user_group_id_chr)";
        //            }

        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    clsVOConstructor objConstructor = new clsVOConstructor();
        //                    p_objResultArr = new clsSampleReceive_VO[dtbResult.Rows.Count];
        //                    for(int i=0;i<dtbResult.Rows.Count;i++)
        //                    {
        //                        p_objResultArr[i] = objConstructor.m_mthContructSampleReceiveVO(dtbResult.Rows[i]);
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����������ѯ�Ѳɼ���δ���յı걾��Ϣ
        //        /// <summary>
        //        /// ����������ѯ�Ѳɼ���δ���յı걾��Ϣ
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strDatFrom"></param>
        //        /// <param name="p_strDatTo"></param>
        //        /// <param name="p_strSampleType"></param>
        //        /// <param name="p_strConlectEmp"></param>
        //        /// <param name="p_strPatientName"></param>
        //        /// <param name="p_strPatientCardID"></param>
        //        /// <param name="p_strBarCode"></param>
        //        /// <param name="p_strCheckCategory"></param>
        //        /// <param name="p_objResultArr"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetUnReceivedSampleByCondition( string p_strDatFrom,
        //            string p_strDatTo, string p_strSampleType, string p_strConlectEmp, string p_strPatientName,
        //            string p_strPatientCardID, string p_strBarCode, string p_strCheckCategory, out clsSampleUnReceive_VO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege( "com.digitalwave.iCare.middletier.LIS.clsSampleSvc", "m_lngGetReceivedSampleByCondition");
        //            if (lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            #region SQL Builder
        //            string strSQL = @"select a.barcode_vchr,
        //       a.patient_type_chr,
        //       a.sampletype_vchr,
        //       a.sampling_date_dat,
        //       a.sample_id_chr,
        //       a.status_int,
        //       a.collector_id_chr,
        //       b.patient_name_vchr,
        //       b.check_content_vchr,
        //       b.special_int,
        //       b.emergency_int,
        //       c.lastname_vchr,
        //       e.dictname_vchr
        //  from t_opr_lis_sample a,
        //       t_opr_lis_application b,
        //       t_bse_employee c,
        //       (select d.dictid_chr, d.dictname_vchr
        //          from t_aid_dict d
        //         where trim(d.dictid_chr) <> 0
        //           and dictkind_chr = '61') e
        // where a.application_id_chr = b.application_id_chr
        //   and b.pstatus_int > 0
        //   and a.patient_type_chr = e.dictid_chr(+)
        //   and a.collector_id_chr = c.empid_chr(+)
        //   and a.status_int = 2
        //   and a.sampling_date_dat between ? and ?
        //";

        //            ArrayList alParams = new ArrayList();
        //            alParams.Add(Convert.ToDateTime(p_strDatFrom));
        //            alParams.Add(Convert.ToDateTime(p_strDatTo));

        //            if (p_strSampleType != null && p_strSampleType != "")
        //            {
        //                strSQL += " and a.sampletype_vchr = ?";
        //                alParams.Add(p_strSampleType);
        //            }

        //            if (p_strConlectEmp != null && p_strConlectEmp != "")
        //            {
        //                strSQL += " and a.collector_id_chr = ?";
        //                alParams.Add(p_strConlectEmp);
        //            }

        //            if (p_strPatientName != null && p_strPatientName.Trim() != "")
        //            {
        //                strSQL += " and b.patient_name_vchr like ?";
        //                alParams.Add("%" + p_strPatientName + "%");
        //            }

        //            if (p_strPatientCardID != null && p_strPatientCardID.Trim() != "")
        //            {
        //                strSQL += " and b.patientcardid_chr = ?";
        //                alParams.Add(p_strPatientCardID);
        //            }

        //            if (p_strBarCode != null && p_strBarCode.Trim() != "")
        //            {
        //                strSQL += " and a.barcode_vchr = ?";
        //                alParams.Add(p_strBarCode);
        //            }

        //            if (p_strCheckCategory != null && p_strCheckCategory.Trim() != "")   //�������Զ����������ѯ
        //            {
        //                strSQL += System.Environment.NewLine;
        //                strSQL += @"and b.check_content_vchr in
        //       (select t2.apply_unit_name_vchr
        //          from t_aid_lis_appuser_group_detail t1,
        //               t_aid_lis_apply_unit t2,
        //               (select a.user_group_id_chr, a.user_group_name_vchr
        //                  from t_aid_lis_appuser_group a
        //                 where not exists
        //                 (select child_user_group_id_chr
        //                          from t_aid_lis_appuser_group_relate b
        //                         where a.user_group_id_chr = b.child_user_group_id_chr)
        //                   and a.user_group_name_vchr = ?) t3
        //         where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //           and t1.user_group_id_chr = t3.user_group_id_chr
        //        union all
        //        select t2.apply_unit_name_vchr
        //          from t_aid_lis_appuser_group_detail t1,
        //               t_aid_lis_apply_unit t2,
        //               (select t4.child_user_group_id_chr
        //                  from t_aid_lis_appuser_group_relate t4,
        //                       (select a.user_group_id_chr, a.user_group_name_vchr
        //                          from t_aid_lis_appuser_group a
        //                         where not exists
        //                         (select child_user_group_id_chr
        //                                  from t_aid_lis_appuser_group_relate b
        //                                 where a.user_group_id_chr =
        //                                       b.child_user_group_id_chr)) t5
        //                 where t4.user_group_id_chr = t5.user_group_id_chr
        //                   and t5.user_group_name_vchr = ?) t3
        //         where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //           and t1.user_group_id_chr = t3.child_user_group_id_chr
        //        union all
        //        select t2.apply_unit_name_vchr
        //          from t_aid_lis_appuser_group_detail t1,
        //               t_aid_lis_apply_unit t2,
        //               (select t4.child_user_group_id_chr
        //                  from t_aid_lis_appuser_group_relate t4,
        //                       (select a.user_group_id_chr, a.user_group_name_vchr
        //                          from t_aid_lis_appuser_group a
        //                         where not exists
        //                         (select child_user_group_id_chr
        //                                  from t_aid_lis_appuser_group_relate b
        //                                 where a.user_group_id_chr =
        //                                       b.child_user_group_id_chr)) t5
        //                 where t4.user_group_id_chr = t5.user_group_id_chr
        //                   and t5.user_group_name_vchr = ?) t3
        //         where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //           and t1.user_group_id_chr = t3.child_user_group_id_chr)";

        //                alParams.Add(p_strCheckCategory);
        //                alParams.Add(p_strCheckCategory);
        //                alParams.Add(p_strCheckCategory);
        //            }

        //            strSQL += "    order by barcode_vchr";
        //            #endregion

        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                IDataParameter[] objDPArr = null;
        //                objHRPSvc.CreateDatabaseParameter(alParams.Count, out objDPArr);
        //                for (int index = 0; index < alParams.Count; index++)
        //                {
        //                    if (alParams[index].GetType().Name == "DateTime")
        //                    {
        //                        objDPArr[index].DbType = DbType.DateTime;
        //                    }
        //                    objDPArr[index].Value = alParams[index];
        //                }

        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
        //                objHRPSvc.Dispose();
        //                if (lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    clsVOConstructor objConstructor = new clsVOConstructor();
        //                    p_objResultArr = new clsSampleUnReceive_VO[dtbResult.Rows.Count];
        //                    for (int i = 0; i < dtbResult.Rows.Count; i++)
        //                    {
        //                        p_objResultArr[i] = objConstructor.m_mthContructSampleUnReceiveVO(dtbResult.Rows[i]);
        //                    }
        //                }
        //            }
        //            catch (Exception objEx)
        //            {
        //                string strTmp = objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region �����Զ������������뵥Ԫ
        //        /// <summary>
        //        /// �����Զ������������뵥Ԫ
        //        /// 
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strCheckCategory"></param>
        //        /// <param name="p_dtbDetail"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetAppuserGroupDetail( string p_strCheckCategory, out DataTable p_dtbDetail)
        //        {
        //            long lngRes = 0;
        //            p_dtbDetail = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege( "com.digitalwave.iCare.middletier.LIS.clsSampleSvc", "m_lngGetAppuserGroupDetail");
        //            try
        //            {
        //                string strSQL = @"select t2.apply_unit_name_vchr
        //                                    from t_aid_lis_appuser_group_detail t1,
        //                                         t_aid_lis_apply_unit t2,
        //                                         (select a.user_group_id_chr, a.user_group_name_vchr
        //                                            from t_aid_lis_appuser_group a
        //                                           where not exists (
        //                                                           select child_user_group_id_chr
        //                                                             from t_aid_lis_appuser_group_relate b
        //                                                            where a.user_group_id_chr =
        //                                                                                       b.child_user_group_id_chr)
        //                                             and trim(a.user_group_name_vchr) = ? ) t3
        //                                   where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //                                     and t1.user_group_id_chr = t3.user_group_id_chr
        //                                  union all
        //                                  select t2.apply_unit_name_vchr
        //                                    from t_aid_lis_appuser_group_detail t1,
        //                                         t_aid_lis_apply_unit t2,
        //                                         (select t4.child_user_group_id_chr
        //                                            from t_aid_lis_appuser_group_relate t4,
        //                                                 (select a.user_group_id_chr, a.user_group_name_vchr
        //                                                    from t_aid_lis_appuser_group a
        //                                                   where not exists (
        //                                                            select child_user_group_id_chr
        //                                                              from t_aid_lis_appuser_group_relate b
        //                                                             where a.user_group_id_chr =
        //                                                                                       b.child_user_group_id_chr)) t5
        //                                           where t4.user_group_id_chr = t5.user_group_id_chr
        //                                             and trim(t5.user_group_name_vchr) = ? ) t3
        //                                   where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //                                     and t1.user_group_id_chr = t3.child_user_group_id_chr
        //                                  union all
        //                                  select t2.apply_unit_name_vchr
        //                                    from t_aid_lis_appuser_group_detail t1,
        //                                         t_aid_lis_apply_unit t2,
        //                                         (select t4.child_user_group_id_chr
        //                                            from t_aid_lis_appuser_group_relate t4,
        //                                                 (select a.user_group_id_chr, a.user_group_name_vchr
        //                                                    from t_aid_lis_appuser_group a
        //                                                   where not exists (
        //                                                            select child_user_group_id_chr
        //                                                              from t_aid_lis_appuser_group_relate b
        //                                                             where a.user_group_id_chr =
        //                                                                                       b.child_user_group_id_chr)) t5
        //                                           where t4.user_group_id_chr = t5.user_group_id_chr
        //                                             and trim(t5.user_group_name_vchr) = ? ) t3
        //                                   where t1.apply_unit_id_chr = t2.apply_unit_id_chr
        //                                     and t1.user_group_id_chr = t3.child_user_group_id_chr";

        //                clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                IDataParameter[] objParam = null;
        //                objHRPSvc.CreateDatabaseParameter(3, out objParam);
        //                objParam[0].Value = p_strCheckCategory;
        //                objParam[1].Value = p_strCheckCategory;
        //                objParam[2].Value = p_strCheckCategory;
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbDetail, objParam);
        //                objHRPSvc.Dispose();
        //            }
        //            catch (Exception objEx)
        //            {
        //                string strTmp = objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region ���ձ걾���˻�
        /// <summary>
        /// ���ձ걾���˻�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intStatus">3-���� 7-�˻�</param>
        /// <param name="p_strSampleID"></param>
        /// <param name="p_strReceiveDat">��������</param>
        /// <param name="p_strReceiveEmp">����Ա��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngReceiveSample( int p_intStatus, string p_strSampleID, string p_strReceiveDat,
            string p_strReceiveEmp, string p_strSendPeopleID)
        {
            long lngRes = 0; 
            string strSQL = @"update t_opr_lis_sample
   set status_int           = " + p_intStatus + @",
       accept_dat           = to_date('" + p_strReceiveDat + @"',
                                      'yyyy-mm-dd hh24:mi:ss'),
       acceptor_id_chr      = '" + p_strReceiveEmp + @"',
       sendsample_empid_chr = '" + p_strSendPeopleID + @"',
       issampleback         = 0,
       sample_back_reason   = ''
 where sample_id_chr = '" + p_strSampleID + @"'
";
            string strSql1 = @"select a.outpatrecipeid_chr, a.orderid_int, d.barcode_vchr
                                  from t_opr_outpatient_orderdic a, t_opr_lis_sample d
                                 where a.attachid_vchr = d.application_id_chr
                                   and d.status_int > 0
                                   and d.sample_id_chr = ?";
            IDataParameter[] objParas = null;

            try
            {
                List<string> lstBarCode = new List<string>();
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);

                objHRPSvc.CreateDatabaseParameter(1, out objParas);
                objParas[0].Value = p_strSampleID;
                DataTable dtTemp = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql1, ref dtTemp, objParas);
                if (lngRes > 0 && dtTemp.Rows.Count > 0)
                {
                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        this.m_mthItemsConfirm(p_strReceiveEmp, dtTemp.Rows[k]);

                        if (dtTemp.Rows[k]["barcode_vchr"] != DBNull.Value && lstBarCode.IndexOf(dtTemp.Rows[k]["barcode_vchr"].ToString()) < 0)
                        {
                            lstBarCode.Add(dtTemp.Rows[k]["barcode_vchr"].ToString());
                        }
                    }
                }

                if (p_intStatus == 3)
                {
                    #region ����2��Ŀ 1) 0000041 - AU680

                    long affectRows = 0;
                    string Sql = string.Empty;
                    IDataParameter[] parm = null;
                    foreach (string barCode in lstBarCode)
                    {
                        Sql = @"select distinct d.device_check_item_id_chr, d.device_check_item_name_vchr
                                  from t_opr_lis_sample a
                                 inner join t_opr_lis_app_check_item b
                                    on b.application_id_chr = a.application_id_chr
                                 inner join t_bse_lis_check_item_dev_item c
                                    on c.check_item_id_chr = b.check_item_id_chr
                                 inner join t_bse_lis_device_check_item d
                                    on d.device_check_item_id_chr = c.device_check_item_id_chr
                                   and d.device_model_id_chr = c.device_model_id_chr
                                 where a.status_int >= 3
                                   and d.device_model_id_chr = '0000041'
                                   and a.barcode_vchr = ?
                                   and a.status_int > 0
                                 order by d.device_check_item_id_chr";

                        DataTable dtItem = null;
                        objHRPSvc.CreateDatabaseParameter(1, out parm);
                        parm[0].Value = barCode;
                        objHRPSvc.lngGetDataTableWithParameters(Sql, ref dtItem, parm);
                        if (dtItem != null && dtItem.Rows.Count > 0)
                        {
                            Sql = @"delete from t_opr_lis_barcode2item where barcode = ?";
                            objHRPSvc.CreateDatabaseParameter(1, out parm);
                            parm[0].Value = barCode;
                            objHRPSvc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                            Sql = @"insert into t_opr_lis_barcode2item
                                      (barcode, itemid, itemname, checktime)
                                    values
                                      (?, ?, ?, ?)";
                            DateTime dtmNow = DateTime.Now;
                            foreach (DataRow dr in dtItem.Rows)
                            {
                                objHRPSvc.CreateDatabaseParameter(4, out parm);
                                parm[0].Value = barCode;
                                parm[1].Value = dr["device_check_item_id_chr"].ToString();
                                parm[2].Value = dr["device_check_item_name_vchr"].ToString();
                                parm[3].Value = dtmNow;
                                objHRPSvc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                            }
                        }
                    }
                    #endregion
                }


                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        private void m_mthItemsConfirm(string p_strReceiveEmp, DataRow dr)
        {
            long lngEff = -1;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = null;
            string strSQL0 = @"select *
                              from t_opr_itemconfirm t
                             where t.outpatrecipedeid_chr = ?
                               and t.outpatrecipeid_chr = ?
                               and t.status_int = 1";
            string strSQL = @"insert into t_opr_itemconfirm
                                  (outpatrecipedeid_chr,
                                   outpatrecipeid_chr,
                                   empno_vchr,
                                   record_dat,
                                   status_int)
                                values
                                  (?, ?, ?, sysdate, 1)";
            IDataParameter[] objParas = null;
            try
            {
                DataTable dtbResult = new DataTable();
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out objParas);
                objParas[0].Value = dr["orderid_int"].ToString();
                objParas[1].Value = dr["outpatrecipeid_chr"].ToString();
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL0, ref dtbResult, objParas);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    return;
                }

                objParas = null;
                objHRPSvc.CreateDatabaseParameter(3, out objParas);
                objParas[0].Value = dr["orderid_int"].ToString();
                objParas[1].Value = dr["outpatrecipeid_chr"].ToString();
                objParas[2].Value = p_strReceiveEmp;
                objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objParas);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion
        #endregion

        #region ����������Ӵ��� ͯ��
        #region ����һ����¼
        [AutoComplete]
        public long m_lngAddNewSampleInterpose( clsLisSampleInterposeVO p_objRecord)
        {
            long lngRes = 0; 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "INSERT INTO T_AID_LIS_SAMPLE_INTERPOSE (DEVICEID_CHR,BEGIN_DEVICE_SAMPLE_ID_CHR,END_DEVICE_SAMPLE_ID_CHR,COMPLETE_FLAG_CHR) VALUES (?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strDEVICEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strBEGIN_DEVICE_SAMPLE_ID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strEND_DEVICE_SAMPLE_ID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strCOMPLETE_FLAG_CHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //        #region ��������ID��ѯ��Ӽ�¼
        //        [AutoComplete]
        //        public long m_lngGetSampleInterposeByDeviceID(string p_strDeviceID,
        //            out clsLisSampleInterposeVO p_objResult)
        //        {
        //            long lngRes=0;
        //            p_objResult = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetSampleInterposeByDeviceID");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"SELECT * 
        //							    FROM T_AID_LIS_SAMPLE_INTERPOSE 
        //							   WHERE DEVICEID_CHR = '"+p_strDeviceID+@"'";
        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResult = new clsLisSampleInterposeVO();
        //                    p_objResult.m_strBEGIN_DEVICE_SAMPLE_ID_CHR = dtbResult.Rows[0]["BEGIN_DEVICE_SAMPLE_ID_CHR"].ToString().Trim();
        //                    p_objResult.m_strCOMPLETE_FLAG_CHR = dtbResult.Rows[0]["COMPLETE_FLAG_CHR"].ToString().Trim();
        //                    p_objResult.m_strDEVICEID_CHR = dtbResult.Rows[0]["DEVICEID_CHR"].ToString().Trim();
        //                    p_objResult.m_strEND_DEVICE_SAMPLE_ID_CHR = dtbResult.Rows[0]["END_DEVICE_SAMPLE_ID_CHR"].ToString().Trim();
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region ��������ID���²�Ӽ�¼
        [AutoComplete]
        public long m_lngSetSampleInterpose( clsLisSampleInterposeVO p_objRecord)
        {
            long lngRes = 0; 
            string strSQL = @"UPDATE t_aid_lis_sample_interpose
								 SET complete_flag_chr = '" + p_objRecord.m_strCOMPLETE_FLAG_CHR + @"'
							   WHERE deviceid_chr = '" + p_objRecord.m_strDEVICEID_CHR + @"'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��Ӵ���
        [AutoComplete]
        public long m_lngSampleInterpose( clsLisSampleInterposeVO p_objRecord)
        {
            long lngRes = 0; 
            string strSQL = @"UPDATE t_aid_lis_sample_interpose
								 SET complete_flag_chr = ?,
									 BEGIN_DEVICE_SAMPLE_ID_CHR = ?,
									 END_DEVICE_SAMPLE_ID_CHR = ?
							   WHERE DEVICEID_CHR = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strCOMPLETE_FLAG_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strBEGIN_DEVICE_SAMPLE_ID_CHR;
                objLisAddItemRefArr[2].Value = p_objRecord.m_strEND_DEVICE_SAMPLE_ID_CHR;
                objLisAddItemRefArr[3].Value = p_objRecord.m_strDEVICEID_CHR;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
                if (lngRes > 0)
                {
                    if (lngRecEff == 0)
                    {
                        lngRes = m_lngAddNewSampleInterpose( p_objRecord);
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #endregion

        #region ���±�T_OPR_LIS_DEVICE_RELATION
        [AutoComplete]
        public long m_lngSetLisDeviceRelation( clsT_LIS_DeviceRelationVO p_objRecord)
        {
            long lngRes = 0; 
            string strSQL = @"UPDATE t_opr_lis_device_relation a
								 SET a.device_sampleid_chr = '" + p_objRecord.m_strDEVICE_SAMPLEID_CHR + @"',
									 a.check_dat = TO_DATE('" + p_objRecord.m_strCHECK_DAT + @"','yyyy-mm-dd hh24:mi:ss'),
									 a.status_int = '" + p_objRecord.m_intSTATUS_INT + @"',
									 a.IMPORT_REQ_INT = '" + p_objRecord.m_intIMPORT_REQ_INT + @"'
							   WHERE a.deviceid_chr = '" + p_objRecord.m_strDEVICEID_CHR + @"' 
								 AND a.seq_id_chr = '" + p_objRecord.m_strSEQ_ID_CHR + @"' 
								 AND a.reception_dat = TO_DATE('" + p_objRecord.m_strRECEPTION_DAT + @"','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        //        #region ����������ѯ����������֮��Ĺ�ϵ 
        //        [AutoComplete]
        //        public long m_lngGetDeviceRelationVOArrByCondition(string p_strDeviceID,string p_strReceptDatFrom,
        //            string p_strReceptDatTo,out clsT_LIS_DeviceRelationVO[] p_objResultArr)
        //        {
        //            p_objResultArr = null;
        //            long lngRes=0;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetDeviceRelationVOArrByCondition");

        //            #region SQL
        ////			string strSQL = @"SELECT a.*,b.status_int as sample_status,b.barcode_vchr
        ////								FROM t_opr_lis_device_relation a, t_opr_lis_sample b
        ////							   WHERE a.sample_id_chr = b.sample_id_chr(+)
        ////								 AND b.status_int >= 0";
        //            string strSQL = @"SELECT a.*, d.status_int AS sample_status, d.barcode_vchr,
        //									 d.application_form_no_chr
        //								FROM t_opr_lis_device_relation a,
        //									 (SELECT b.*, c.application_form_no_chr
        //										FROM t_opr_lis_sample b, t_opr_lis_application c
        //									   WHERE b.application_id_chr = c.application_id_chr
        //										 AND b.status_int >= 0
        //										 AND c.pstatus_int > 0) d
        //							   WHERE a.sample_id_chr = d.sample_id_chr(+)";
        //            string strSQL_ReceptDatFrom = " AND a.reception_dat >= ? ";
        //            string strSQL_ReceptDatTo = " AND a.reception_dat <= ? ";
        //            string strSQL_DeviceID = " AND a.deviceid_chr = ? ";
        //            #endregion

        //            ArrayList arlSQL = new ArrayList();
        //            ArrayList arlParm = new ArrayList();

        //            #region ����
        //            if(p_strDeviceID != null && p_strDeviceID.Trim() != "")
        //            {
        //                arlSQL.Add(strSQL_DeviceID);
        //                arlParm.Add(p_strDeviceID);
        //            }
        //            if(p_strReceptDatFrom != null && Microsoft.VisualBasic.Information.IsDate(p_strReceptDatFrom.Trim()))
        //            {
        //                arlSQL.Add(strSQL_ReceptDatFrom);
        //                arlParm.Add(DateTime.Parse(p_strReceptDatFrom.Trim()));
        //            }
        //            if(p_strReceptDatTo != null && Microsoft.VisualBasic.Information.IsDate(p_strReceptDatTo.Trim()))
        //            {
        //                arlSQL.Add(strSQL_ReceptDatTo);
        //                arlParm.Add(DateTime.Parse(p_strReceptDatTo.Trim()));
        //            }
        //            #endregion

        //            foreach(object obj in arlSQL)
        //            {
        //                strSQL += obj.ToString();
        //            }

        //            strSQL += " ORDER BY to_number(a.seq_id_device_chr)";

        //            int intParmCount = arlSQL.Count;

        //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //            IDataParameter[] objDPArr = null;
        //            objHRPSvc.CreateDatabaseParameter(intParmCount,out objDPArr);

        //            for(int i=0;i< intParmCount;i++)
        //            {
        //                objDPArr[i].Value = arlParm[i];
        //            }

        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                lngRes = 0;
        //                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult,objDPArr);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsT_LIS_DeviceRelationVO[dtbResult.Rows.Count];
        //                    for(int i1=0;i1<dtbResult.Rows.Count;i1++)
        //                    {
        //                        p_objResultArr[i1] = new clsT_LIS_DeviceRelationVO();
        //                        p_objResultArr[i1].m_strDEVICEID_CHR = dtbResult.Rows[i1]["DEVICEID_CHR"].ToString().Trim();
        //                        if(dtbResult.Rows[i1]["IMPORT_REQ_INT"].ToString().Trim() != "")
        //                        {
        //                            p_objResultArr[i1].m_intIMPORT_REQ_INT = int.Parse(dtbResult.Rows[i1]["IMPORT_REQ_INT"].ToString().Trim());
        //                        }
        //                        p_objResultArr[i1].m_strSEQ_ID_CHR = dtbResult.Rows[i1]["SEQ_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strRECEPTION_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["RECEPTION_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        p_objResultArr[i1].m_strDEVICE_SAMPLEID_CHR = dtbResult.Rows[i1]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        if(dtbResult.Rows[i1]["CHECK_DAT"].ToString().Trim() != "")
        //                        {
        //                            p_objResultArr[i1].m_strCHECK_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        else
        //                        {
        //                            p_objResultArr[i1].m_strCHECK_DAT = dtbResult.Rows[i1]["CHECK_DAT"].ToString().Trim();
        //                        }
        //                        p_objResultArr[i1].m_strSAMPLE_ID_CHR = dtbResult.Rows[i1]["SAMPLE_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPOSITIONID_CHR = dtbResult.Rows[i1]["POSITIONID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
        //                        p_objResultArr[i1].m_strSEQ_ID_DEVICE_CHR = dtbResult.Rows[i1]["SEQ_ID_DEVICE_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_intBIND_METHOD_INT = Convert.ToInt32(dtbResult.Rows[i1]["BIND_METHOD_INT"].ToString().Trim());
        //                        p_objResultArr[i1].m_intSAMPLE_STATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["sample_status"].ToString().Trim());
        //                        p_objResultArr[i1].m_strBARCODE_VCHR = dtbResult.Rows[i1]["barcode_vchr"].ToString().Trim();
        //                        p_objResultArr[i1].m_strCheckNO = dtbResult.Rows[i1]["application_form_no_chr"].ToString().Trim();
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region [U]����һ������,ͬʱ�޸�����������
        /// <summary>
        /// ����һ������,ͬʱ�޸�����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strReportID"></param>
        /// <param name="p_strSampleGroupID"></param>
        /// <param name="p_objRecordVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewSampleAndModifyAppSampleGroup(
            string p_strAppID, clsT_OPR_LIS_SAMPLE_VO p_objRecordVO)
        {
            long lngRes = 0;
            try
            { 
                clsT_OPR_LIS_SAMPLE_VO[] p_objRecordVOArr = new clsT_OPR_LIS_SAMPLE_VO[] { p_objRecordVO };
                lngRes = this.m_lngInsertSampleRecord( p_objRecordVOArr);
                if (lngRes == 1)
                {
                    lngRes = 0;
                    lngRes = new clsApplicationSvc().m_lngUpdateAppSampleGroupSampleID( p_strAppID, p_objRecordVO.m_strSAMPLE_ID_CHR);
                    if (lngRes <= 0)
                    {
                        throw new Exception("��������������ʧ��!");
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return lngRes;
        }
        #endregion

        #region [U]����һ������,ͬʱ�޸�����������
        /// <summary>
        /// ����һ������,ͬʱ�޸�����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppID"></param>
        /// <param name="p_strReportID"></param>
        /// <param name="p_strSampleGroupID"></param>
        /// <param name="p_objRecordVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewSampleAndModifyAppSampleGroup(
            string p_strAppID, ref clsT_OPR_LIS_SAMPLE_VO p_objRecordVO)
        {
            long lngRes = 0;
            try
            {
                clsT_OPR_LIS_SAMPLE_VO[] p_objRecordVOArr = new clsT_OPR_LIS_SAMPLE_VO[] { p_objRecordVO };
                lngRes = this.m_lngInsertSampleRecord( p_objRecordVOArr);
                if (lngRes == 1)
                {
                    lngRes = 0;
                    lngRes = new clsApplicationSvc().m_lngUpdateAppSampleGroupSampleID( p_strAppID, p_objRecordVO.m_strSAMPLE_ID_CHR);
                    if (lngRes <= 0)
                    {
                        throw new Exception("��������������ʧ��!");
                    }
                }
            }
            catch (Exception ex)
            {
                new clsLogText().LogError(ex);
            }
            return lngRes;
        }
        #endregion

        //        #region ���ݱ걾��BarCode��ѯ��Ӧ�ı걾���걾����Ϣ
        //        [AutoComplete]
        //        public long m_lngGetSampleInfoByBarCode(string p_strBarCode,
        //            out DataTable p_dtbResult)
        //        {
        //            long lngRes = 0;
        //            p_dtbResult = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetSampleGroupByBarCode");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }
        //            string strSQL = @"SELECT a.*,c.*, d.device_model_desc_vchr
        //								FROM t_opr_lis_sample a,
        //									 t_opr_lis_app_sample b,
        //									 t_aid_lis_sample_group c,
        //									 t_bse_lis_device_model d
        //							   WHERE a.sample_id_chr = b.sample_id_chr
        //								 AND b.sample_group_id_chr = c.sample_group_id_chr
        //								 AND c.device_model_id_chr = d.device_model_id_chr
        //								 AND a.status_int > 0
        //								 AND a.barcode_vchr = '"+p_strBarCode+"'";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbResult);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ��ȡ���еı걾������Ϣ 
        //        [AutoComplete]
        //        public long m_lngGetSampleTypeArr(out clsSampleType_VO[] p_objResultArr)
        //        {
        //            p_objResultArr = new clsSampleType_VO[0];
        //            long lngRes=0;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetSampleTypeArr");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }
        //            string strSQL = @"select sample_type_id_chr, sample_type_desc_vchr, pycode_chr, wbcode_chr,
        //       stdcode1_chr, stdcode2_chr, hasbarcode_int from t_aid_lis_sampletype order by sample_type_id_chr";
        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsSampleType_VO[dtbResult.Rows.Count];
        //                    for(int i1=0;i1<p_objResultArr.Length;i1++)
        //                    {
        //                        p_objResultArr[i1] = new clsSampleType_VO();
        //                        p_objResultArr[i1].m_strSample_Type_ID = dtbResult.Rows[i1]["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strSample_Type_Desc = dtbResult.Rows[i1]["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPyCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strWbCode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strStdCode1 = dtbResult.Rows[i1]["STDCODE1_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strStdCode2 = dtbResult.Rows[i1]["STDCODE2_CHR"].ToString().Trim();
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���ݱ걾ID��ѯ�걾�������걾�Ĺ�ϵVO 
        //        [AutoComplete]
        //        public long m_lngGetDeviceRelationVOArrBySampleID(string p_strSampleID,
        //            out clsT_LIS_DeviceRelationVO[] p_objResultArr)
        //        {
        //            long lngRes = 0;
        //            p_objResultArr = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetDeviceRelationVOArrBySampleID");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }
        //            string strCondition = "SAMPLE_ID_CHR = '"+p_strSampleID+"' AND STATUS_INT > 0";
        //            lngRes = m_lngGetDeviceRelationVOArrByCondition(strCondition,out p_objResultArr);
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���ݱ걾ID��ѯ�걾��Ϣ 
        //        [AutoComplete]
        //        public long m_lngGetSampleVOArrBySampleID(string p_strSampleID,out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr)
        //        {
        //            p_objResultArr = null;
        //            long lngRes = 0;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetSampleVOArrBySampleID");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }
        //            string strCondition = "SAMPLE_ID_CHR = '"+p_strSampleID+"' AND STATUS_INT > 0";
        //            lngRes = m_lngGetSampleVOArrByCondition(strCondition,out p_objResultArr);
        //            return lngRes;
        //        }
        //        #endregion

        //        #region *PROTECTED* m_lngGetSampleVOArrByCondition ��ϲ�ѯ,�õ��걾VOArr 
        //        /// <summary>
        //        /// ��ϲ�ѯ,�õ��걾VOArr ���� 2004.05.27
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strQueryCondition"></param>
        //        /// <param name="p_objResultArr"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        protected long m_lngGetSampleVOArrByCondition(string p_strQueryCondition, out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr)
        //        {
        //            p_objResultArr = new clsT_OPR_LIS_SAMPLE_VO[0];
        //            long lngRes=0;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetSampleVOArrByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"select appl_dat, sex_chr, patient_name_vchr, patient_subno_chr, age_chr,
        //                                   patient_type_chr, diagnose_vchr, sampletype_vchr, samplestate_vchr,
        //                                   bedno_chr, icd_vchr, patientcardid_chr, barcode_vchr, sample_id_chr,
        //                                   patientid_chr, sampling_date_dat, operator_id_chr, modify_dat,
        //                                   appl_empid_chr, appl_deptid_chr, status_int, sample_type_id_chr,
        //                                   qcsampleid_chr, samplekind_chr, check_date_dat, accept_dat,
        //                                   acceptor_id_chr, application_id_chr, patient_inhospitalno_chr,
        //                                   confirm_dat, confirmer_id_chr, collector_id_chr, checker_id_chr
        //                              from t_opr_lis_sample ";

        //            if(p_strQueryCondition != null && p_strQueryCondition.Trim() != "")
        //            {
        //                strSQL = strSQL + " WHERE " + p_strQueryCondition;
        //            }


        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsT_OPR_LIS_SAMPLE_VO[dtbResult.Rows.Count];
        //                    for(int i1=0;i1<p_objResultArr.Length;i1++)
        //                    {
        //                        p_objResultArr[i1] = new clsT_OPR_LIS_SAMPLE_VO();
        //                        if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[i1]["APPL_DAT"]))
        //                        {
        //                            p_objResultArr[i1].m_strAPPL_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["APPL_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        p_objResultArr[i1].m_strSEX_CHR = dtbResult.Rows[i1]["SEX_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPATIENT_NAME_VCHR = dtbResult.Rows[i1]["PATIENT_NAME_VCHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPATIENT_SUBNO_CHR = dtbResult.Rows[i1]["PATIENT_SUBNO_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strAGE_CHR = dtbResult.Rows[i1]["AGE_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPATIENT_TYPE_CHR = dtbResult.Rows[i1]["PATIENT_TYPE_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strDIAGNOSE_VCHR = dtbResult.Rows[i1]["DIAGNOSE_VCHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strSAMPLETYPE_VCHR = dtbResult.Rows[i1]["SAMPLETYPE_VCHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strSAMPLESTATE_VCHR = dtbResult.Rows[i1]["SAMPLESTATE_VCHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strBEDNO_CHR = dtbResult.Rows[i1]["BEDNO_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strICD_VCHR = dtbResult.Rows[i1]["ICD_VCHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPATIENTCARDID_CHR = dtbResult.Rows[i1]["PATIENTCARDID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strBARCODE_VCHR = dtbResult.Rows[i1]["BARCODE_VCHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strSAMPLE_ID_CHR = dtbResult.Rows[i1]["SAMPLE_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPATIENTID_CHR = dtbResult.Rows[i1]["PATIENTID_CHR"].ToString().Trim();
        //                        if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[i1]["SAMPLING_DATE_DAT"]))
        //                        {
        //                            p_objResultArr[i1].m_strSAMPLING_DATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["SAMPLING_DATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        p_objResultArr[i1].m_strOPERATOR_ID_CHR = dtbResult.Rows[i1]["OPERATOR_ID_CHR"].ToString().Trim();
        //                        if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[i1]["MODIFY_DAT"]))
        //                        {
        //                            p_objResultArr[i1].m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        p_objResultArr[i1].m_strAPPL_EMPID_CHR = dtbResult.Rows[i1]["APPL_EMPID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strAPPL_DEPTID_CHR = dtbResult.Rows[i1]["APPL_DEPTID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
        //                        p_objResultArr[i1].m_strSAMPLE_TYPE_ID_CHR = dtbResult.Rows[i1]["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strQCSAMPLEID_CHR = dtbResult.Rows[i1]["QCSAMPLEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strSAMPLEKIND_CHR = dtbResult.Rows[i1]["SAMPLEKIND_CHR"].ToString().Trim();
        //                        if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[i1]["CHECK_DATE_DAT"]))
        //                        {
        //                            p_objResultArr[i1].m_strCHECK_DATE_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHECK_DATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[i1]["ACCEPT_DAT"]))
        //                        {
        //                            p_objResultArr[i1].m_strACCEPT_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["ACCEPT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        p_objResultArr[i1].m_strACCEPTOR_ID_CHR = dtbResult.Rows[i1]["ACCEPTOR_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strAPPLICATION_ID_CHR = dtbResult.Rows[i1]["APPLICATION_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPATIENT_INHOSPITALNO_CHR = dtbResult.Rows[i1]["PATIENT_INHOSPITALNO_CHR"].ToString().Trim();
        //                        if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[i1]["CONFIRM_DAT"]))
        //                        {
        //                            p_objResultArr[i1].m_strCONFIRM_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CONFIRM_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        p_objResultArr[i1].m_strCONFIRMER_ID_CHR = dtbResult.Rows[i1]["CONFIRMER_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strCOLLECTOR_ID_CHR = dtbResult.Rows[i1]["COLLECTOR_ID_CHR"].ToString().Trim();
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region *PROTECTED* m_lngGetDeviceRelationVOArrByCondition ��ϲ�ѯ,�õ��걾VOArr
        //        /// <summary>
        //        /// ��ϲ�ѯ,�õ�����VOArr ���� 2004.05.27
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strQueryCondition"></param>
        //        /// <param name="p_objResultArr"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        protected long m_lngGetDeviceRelationVOArrByCondition(string p_strQueryCondition, out clsT_LIS_DeviceRelationVO[] p_objResultArr)
        //        {
        //            p_objResultArr = new clsT_LIS_DeviceRelationVO[0];
        //            long lngRes=0;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetDeviceRelationVOArrByCondition");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"SELECT * FROM T_OPR_LIS_DEVICE_RELATION ";

        //            if(p_strQueryCondition != null && p_strQueryCondition.Trim() != "")
        //            {
        //                strSQL = strSQL + " WHERE " + p_strQueryCondition;
        //            }


        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objResultArr = new clsT_LIS_DeviceRelationVO[dtbResult.Rows.Count];
        //                    for(int i1=0;i1<p_objResultArr.Length;i1++)
        //                    {
        //                        p_objResultArr[i1] = new clsT_LIS_DeviceRelationVO();
        //                        p_objResultArr[i1].m_strDEVICEID_CHR = dtbResult.Rows[i1]["DEVICEID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strSEQ_ID_CHR = dtbResult.Rows[i1]["SEQ_ID_CHR"].ToString().Trim();
        //                        if(dtbResult.Rows[i1]["IMPORT_REQ_INT"].ToString().Trim() != "")
        //                        {
        //                            p_objResultArr[i1].m_intIMPORT_REQ_INT = int.Parse(dtbResult.Rows[i1]["IMPORT_REQ_INT"].ToString().Trim());
        //                        }
        //                        if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[i1]["RECEPTION_DAT"]))
        //                        {
        //                            p_objResultArr[i1].m_strRECEPTION_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["RECEPTION_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        else
        //                        {
        //                            p_objResultArr[i1].m_strRECEPTION_DAT = null;
        //                        }
        //                        p_objResultArr[i1].m_strDEVICE_SAMPLEID_CHR = dtbResult.Rows[i1]["DEVICE_SAMPLEID_CHR"].ToString().Trim();
        //                        if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[i1]["CHECK_DAT"]))
        //                        {
        //                            p_objResultArr[i1].m_strCHECK_DAT = Convert.ToDateTime(dtbResult.Rows[i1]["CHECK_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                        }
        //                        else
        //                        {
        //                            p_objResultArr[i1].m_strCHECK_DAT = null;
        //                        }
        //                        p_objResultArr[i1].m_strSAMPLE_ID_CHR = dtbResult.Rows[i1]["SAMPLE_ID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_strPOSITIONID_CHR = dtbResult.Rows[i1]["POSITIONID_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[i1]["STATUS_INT"].ToString().Trim());
        //                        p_objResultArr[i1].m_strSEQ_ID_DEVICE_CHR = dtbResult.Rows[i1]["SEQ_ID_DEVICE_CHR"].ToString().Trim();
        //                        p_objResultArr[i1].m_intBIND_METHOD_INT = Convert.ToInt32(dtbResult.Rows[i1]["BIND_METHOD_INT"].ToString().Trim());
        //                    }
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region [U]m_lngInsertSampleRecord  Ϊ�� t_opr_lis_sample ����,�޸�,ɾ�� ��¼ʱ��

        /// <summary>
        /// Ϊ�� t_opr_lis_sample ����,�޸�,ɾ�� ��¼ʱ�� ;
        /// ���� 2004.05.26
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordVOArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertSampleRecord( clsT_OPR_LIS_SAMPLE_VO[] p_objRecordVOArr)
        {
            long lngRes = 0; 
            string strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                if (p_objRecordVOArr != null)
                {
                    for (int i = 0; i < p_objRecordVOArr.Length; i++)
                    {
                        if (p_objRecordVOArr[i] != null)
                        {
                            lngRes = 0;

                            #region ����
                            string strID = null;
                            if (p_objRecordVOArr[i].m_strSAMPLE_ID_CHR == null || p_objRecordVOArr[i].m_strSAMPLE_ID_CHR.Trim() == "")
                            {
                                objHRPSvc.m_lngGenerateNewID("T_OPR_LIS_SAMPLE", "SAMPLE_ID_CHR", out strID);
                                if (strID == null || strID == "")
                                {
                                    throw new Exception("���ܷ���걾ID");
                                }
                                else
                                {
                                    p_objRecordVOArr[i].m_strSAMPLE_ID_CHR = strID;
                                }
                            }
                            p_objRecordVOArr[i].m_strMODIFY_DAT = strDateTimeNow;
                            //�ʿر걾
                            if (p_objRecordVOArr[i].m_strSAMPLEKIND_CHR == "3")
                            {
                                p_objRecordVOArr[i].m_strQCSAMPLEID_CHR = p_objRecordVOArr[i].m_strSAMPLE_ID_CHR;
                            }
                            #endregion

                            string strSQL = "";

                            #region SQL
                            strSQL = @"insert into t_opr_lis_sample (appl_dat,sex_chr,patient_name_vchr,patient_subno_chr,age_chr,
                                patient_type_chr,diagnose_vchr,sampletype_vchr,samplestate_vchr,
                                bedno_chr,icd_vchr,patientcardid_chr,barcode_vchr,sample_id_chr,
                                patientid_chr,sampling_date_dat,operator_id_chr,modify_dat,
                                appl_empid_chr,appl_deptid_chr,status_int,sample_type_id_chr,
                                qcsampleid_chr,samplekind_chr,check_date_dat,accept_dat,
                                acceptor_id_chr,application_id_chr,patient_inhospitalno_chr,
                                confirm_dat,confirmer_id_chr,collector_id_chr,checker_id_chr,sendsample_empid_chr) 
                            values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                            #endregion

                            IDataParameter[] objDPArr = null;

                            #region �������
                            objHRPSvc.CreateDatabaseParameter(34, out objDPArr);
                            if (Microsoft.VisualBasic.Information.IsDate(p_objRecordVOArr[i].m_strAPPL_DAT))
                            {
                                objDPArr[0].Value = DateTime.Parse(p_objRecordVOArr[i].m_strAPPL_DAT);
                            }
                            objDPArr[1].Value = p_objRecordVOArr[i].m_strSEX_CHR;
                            objDPArr[2].Value = p_objRecordVOArr[i].m_strPATIENT_NAME_VCHR;
                            objDPArr[3].Value = p_objRecordVOArr[i].m_strPATIENT_SUBNO_CHR;
                            objDPArr[4].Value = p_objRecordVOArr[i].m_strAGE_CHR;
                            objDPArr[5].Value = p_objRecordVOArr[i].m_strPATIENT_TYPE_CHR;
                            objDPArr[6].Value = p_objRecordVOArr[i].m_strDIAGNOSE_VCHR;
                            objDPArr[7].Value = p_objRecordVOArr[i].m_strSAMPLETYPE_VCHR;
                            objDPArr[8].Value = p_objRecordVOArr[i].m_strSAMPLESTATE_VCHR;
                            objDPArr[9].Value = p_objRecordVOArr[i].m_strBEDNO_CHR;
                            objDPArr[10].Value = p_objRecordVOArr[i].m_strICD_VCHR;
                            objDPArr[11].Value = p_objRecordVOArr[i].m_strPATIENTCARDID_CHR;
                            //BarCode
                            objDPArr[12].Value = p_objRecordVOArr[i].m_strBARCODE_VCHR;
                            //����ID
                            objDPArr[13].Value = p_objRecordVOArr[i].m_strSAMPLE_ID_CHR;
                            objDPArr[14].Value = p_objRecordVOArr[i].m_strPATIENTID_CHR;
                            if (Microsoft.VisualBasic.Information.IsDate(p_objRecordVOArr[i].m_strSAMPLING_DATE_DAT))
                            {
                                objDPArr[15].Value = DateTime.Parse(p_objRecordVOArr[i].m_strSAMPLING_DATE_DAT);
                            }
                            objDPArr[16].Value = p_objRecordVOArr[i].m_strOPERATOR_ID_CHR;
                            if (Microsoft.VisualBasic.Information.IsDate(p_objRecordVOArr[i].m_strMODIFY_DAT))
                            {
                                objDPArr[17].Value = DateTime.Parse(p_objRecordVOArr[i].m_strMODIFY_DAT);
                            }
                            objDPArr[18].Value = p_objRecordVOArr[i].m_strAPPL_EMPID_CHR;
                            objDPArr[19].Value = p_objRecordVOArr[i].m_strAPPL_DEPTID_CHR;
                            //��¼��״̬��־
                            objDPArr[20].Value = p_objRecordVOArr[i].m_intSTATUS_INT;
                            objDPArr[21].Value = p_objRecordVOArr[i].m_strSAMPLE_TYPE_ID_CHR;
                            objDPArr[22].Value = p_objRecordVOArr[i].m_strQCSAMPLEID_CHR;
                            objDPArr[23].Value = p_objRecordVOArr[i].m_strSAMPLEKIND_CHR;
                            if (Microsoft.VisualBasic.Information.IsDate(p_objRecordVOArr[i].m_strCHECK_DATE_DAT))
                            {
                                objDPArr[24].Value = DateTime.Parse(p_objRecordVOArr[i].m_strCHECK_DATE_DAT);
                            }
                            if (Microsoft.VisualBasic.Information.IsDate(p_objRecordVOArr[i].m_strACCEPT_DAT))
                            {
                                objDPArr[25].Value = DateTime.Parse(p_objRecordVOArr[i].m_strACCEPT_DAT);
                            }
                            objDPArr[26].Value = p_objRecordVOArr[i].m_strACCEPTOR_ID_CHR;
                            objDPArr[27].Value = p_objRecordVOArr[i].m_strAPPLICATION_ID_CHR;
                            objDPArr[28].Value = p_objRecordVOArr[i].m_strPATIENT_INHOSPITALNO_CHR;
                            if (Microsoft.VisualBasic.Information.IsDate(p_objRecordVOArr[i].m_strCONFIRM_DAT))
                            {
                                objDPArr[29].Value = DateTime.Parse(p_objRecordVOArr[i].m_strCONFIRM_DAT);
                            }
                            objDPArr[30].Value = p_objRecordVOArr[i].m_strCONFIRMER_ID_CHR;
                            objDPArr[31].Value = p_objRecordVOArr[i].m_strCOLLECTOR_ID_CHR;
                            objDPArr[32].Value = p_objRecordVOArr[i].m_strCHECKER_ID_CHR;
                            objDPArr[33].Value = p_objRecordVOArr[i].m_strSendsample_empid_chr;
                            #endregion

                            long lngEff = 0;
                            lngRes = 0;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                            objHRPSvc.Dispose();
                            if (lngRes <= 0)
                            {
                                throw new Exception("����걾ʧ��!");
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region m_lngAddNewDeviceRelation  Ϊ�� t_opr_lis_device_relation  ���� ��¼ʱ��
        /// <summary>
        /// Ϊ�� t_opr_lis_device_relation  ���� ��¼ʱ�� 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecord"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewDeviceRelation( clsT_LIS_DeviceRelationVO p_objRecord)
        {
            long lngRes = 0; 

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            string strSeqID = null;
            string strSeqIDDevice = null;
            DataTable dtbSeq = null;

            string strSQL = @"SELECT MAX (TO_NUMBER (seq_id_device_chr)) + 1 AS strseqiddevice
						FROM t_opr_lis_device_relation
						WHERE deviceid_chr = '" + p_objRecord.m_strDEVICEID_CHR + "'";
            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSeq);
            if (lngRes > 0 && dtbSeq != null && dtbSeq.Rows.Count > 0)
            {
                strSeqIDDevice = dtbSeq.Rows[0]["strseqiddevice"].ToString().Trim();
                if (strSeqIDDevice == null || strSeqIDDevice.Trim() == "")
                {
                    strSeqIDDevice = "1";
                }
            }
            else if (lngRes > 0)
            {
                strSeqIDDevice = "1";
            }
            else
            {
                return lngRes;
            }

            lngRes = 0;
            dtbSeq = null;
            strSQL = @"SELECT MAX (TO_NUMBER (seq_id_chr)) + 1 AS strseqid
						FROM t_opr_lis_device_relation
						 WHERE deviceid_chr = '" + p_objRecord.m_strDEVICEID_CHR + @"' 
						 AND TRUNC (reception_dat) = trunc (to_date('" + p_objRecord.m_strRECEPTION_DAT + "','yyyy-mm-dd hh24:mi:ss'))";
            lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSeq);
            if (lngRes > 0 && dtbSeq != null && dtbSeq.Rows.Count > 0)
            {
                strSeqID = dtbSeq.Rows[0]["strseqid"].ToString().Trim();
                if (strSeqID == null || strSeqID.Trim() == "")
                {
                    strSeqID = "1";
                }
            }
            else if (lngRes > 0)
            {
                strSeqID = "1";
            }
            else
            {
                return lngRes;
            }

            p_objRecord.m_strSEQ_ID_CHR = strSeqID;
            p_objRecord.m_strSEQ_ID_DEVICE_CHR = strSeqIDDevice;

            lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strSQL = "INSERT INTO T_OPR_LIS_DEVICE_RELATION (DEVICEID_CHR,SEQ_ID_CHR,RECEPTION_DAT,DEVICE_SAMPLEID_CHR,CHECK_DAT,SAMPLE_ID_CHR,POSITIONID_CHR,STATUS_INT,SEQ_ID_DEVICE_CHR,BIND_METHOD_INT,IMPORT_REQ_INT) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(11, out objLisAddItemRefArr);
                //Please change the datetime and reocrdid 
                objLisAddItemRefArr[0].Value = p_objRecord.m_strDEVICEID_CHR;
                objLisAddItemRefArr[1].Value = p_objRecord.m_strSEQ_ID_CHR;
                if (p_objRecord.m_strRECEPTION_DAT == null || p_objRecord.m_strRECEPTION_DAT.Trim() == "")
                {
                    objLisAddItemRefArr[2].Value = null;
                }
                else
                {
                    objLisAddItemRefArr[2].Value = DateTime.Parse(p_objRecord.m_strRECEPTION_DAT);
                }
                objLisAddItemRefArr[3].Value = p_objRecord.m_strDEVICE_SAMPLEID_CHR;
                if (p_objRecord.m_strCHECK_DAT == null || p_objRecord.m_strCHECK_DAT.Trim() == "")
                {
                    objLisAddItemRefArr[4].Value = null;
                }
                else
                {
                    objLisAddItemRefArr[4].Value = DateTime.Parse(p_objRecord.m_strCHECK_DAT);
                }
                objLisAddItemRefArr[5].Value = p_objRecord.m_strSAMPLE_ID_CHR;
                objLisAddItemRefArr[6].Value = p_objRecord.m_strPOSITIONID_CHR;
                objLisAddItemRefArr[7].Value = p_objRecord.m_intSTATUS_INT;
                objLisAddItemRefArr[8].Value = p_objRecord.m_strSEQ_ID_DEVICE_CHR;
                objLisAddItemRefArr[9].Value = p_objRecord.m_intBIND_METHOD_INT;
                objLisAddItemRefArr[10].Value = p_objRecord.m_intIMPORT_REQ_INT;
                long lngRecEff = -1;
                //�������Ӽ�¼
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region m_lngDeleteDeviceRelation ɾ����������
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strRelationDate"></param>
        /// <param name="p_strSeq"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteDeviceRelation(
            string p_strDeviceID, string p_strRelationDate, string p_strSeq)
        {
            long lngRes = 0; 

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            //			string strSQL = @"UPDATE t_opr_lis_device_relation
            //								SET status_int = 0
            //								WHERE deviceid_chr = ? AND reception_dat = ? AND seq_id_chr = ?";
            string strSQL = @"UPDATE t_opr_lis_device_relation
								SET status_int = 0
								WHERE deviceid_chr = '" + p_strDeviceID + @"'
								AND reception_dat =
													TO_DATE ('" + p_strRelationDate + @"', 'yyyy-mm-dd hh24:mi:ss')
								AND seq_id_chr = '" + p_strSeq + "'";
            try
            {
                //				System.Data.IDataParameter[] objDPArr = null;
                //				objHRPSvc.CreateDatabaseParameter(3,out objDPArr);
                //
                //				DateTime dtmRelationDate;
                //				if(Microsoft.VisualBasic.Information.IsDate(p_strRelationDate))
                //				{
                //					dtmRelationDate = Convert.ToDateTime(p_strRelationDate);
                //				}
                //				else
                //				{
                //					dtmRelationDate = DateTime.Parse("1900-01-01 00:00:00");
                //				}
                //				objDPArr[0].Value = p_strDeviceID;
                //				objDPArr[1].Value = dtmRelationDate;
                //				objDPArr[2].Value = p_strSeq;
                //				
                //				long lngRecEff = -1;
                //				lngRes = 0;
                //				lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL,ref lngRecEff,objDPArr);

                lngRes = 0;
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region m_lngDeleteDeviceRelation ɾ����������
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRelation"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteDeviceRelation(
            clsT_LIS_DeviceRelationVO p_objRelation)
        {
            long lngRes = 0; 
            try
            {
                lngRes = 0;
                lngRes = this.m_lngDeleteDeviceRelation( p_objRelation.m_strDEVICEID_CHR, p_objRelation.m_strRECEPTION_DAT, p_objRelation.m_strSEQ_ID_CHR);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ͨ����ƷIDɾ����������
        /// <summary>
        /// ͨ����ƷIDɾ����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelDevicRelation( string p_strSampleID)
        {
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strSampleID))
                return lngRes;

            try
            {
                string strSQL = @"update t_opr_lis_device_relation
   set status_int = 0
 where sample_id_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;

                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSampleID;

                long lngAffect = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffect, objDPArr);
                objHRPServ = null;
                objDPArr = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                p_strSampleID = null;
            }
            return lngRes;
        }
        #endregion

        #region m_lngModifyBind ɾ������һ������������һ������
        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objSourceVO"></param>
        /// <param name="p_objTargetVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyBind(
            clsT_LIS_DeviceRelationVO p_objSourceVO, clsT_LIS_DeviceRelationVO p_objTargetVO)
        {
            long lngRes = 0; 

            try
            {
                lngRes = 0;
                lngRes = this.m_lngDeleteDeviceRelation( p_objSourceVO);
                if (lngRes == 1)
                {
                    lngRes = 0;
                    lngRes = this.m_lngAddNewDeviceRelation( p_objTargetVO);
                }
                if (lngRes == 0)
                {
                    System.EnterpriseServices.ContextUtil.SetAbort();
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion


        #region ���±� t_opr_lis_result_import_req
        //		public long m_mthUdateImportReqBindPointer(string p_strDeviceID,int p_intImportReq)
        //		{
        //
        //			long lngRes=0;
        //			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //			lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_mthUdateImportReqBindPointer");
        //			if(lngRes < 0)
        //			{
        //				return -1;
        //			}
        //
        //			com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //			
        //			string strSQL = @"SELECT MAX (TO_NUMBER (seq_id_device_chr)) + 1 AS strseqiddevice
        //						FROM t_opr_lis_device_relation
        //						WHERE deviceid_chr = '" + p_objRecord.m_strDEVICEID_CHR +"'";
        //
        //			System.Data.IDataParameter[] objDPArr1= null;
        //			objHRPSvc.CreateDatabaseParameter(2,out objDPArr1);
        //
        //			objDPArr1[0].Value=p_strDeviceID.Trim();
        //			objDPArr1[1].Value=p_strDeviceSampleID.Trim();
        //			if(Microsoft.VisualBasic.Information.IsDate(p_strCheckDate.Trim()))
        //			{
        //				objDPArr1[2].Value=DateTime.Parse(p_strCheckDate.Trim());
        //			}
        //			else
        //			{
        //				objDPArr1[2] = null;
        //			}
        //			objDPArr1[2].DbType = DbType.Date;
        //			objDPArr1[3].Value = p_intBeginIndex;
        //			lngRes = 0;
        //			lngRes=objHRPSvc.lngGetDataTableWithParameters(strSQL,ref dtbResult, objDPArr1);
        //		}
        #endregion


        //#region ����BarCode �õ�����VO 
        //[AutoComplete]
        //public long m_lngGetSampleVOByBarcode(string p_strBarCode,out clsT_OPR_LIS_SAMPLE_VO[] p_objResultArr)
        //{
        //    p_objResultArr = null;
        //    long lngRes = 0;
        //    clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //    lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetSampleVOByBarcode");
        //    if(lngRes < 0)
        //    {
        //        return -1;
        //    }
        //    string strCondition = "BARCODE_VCHR = '"+ p_strBarCode +"' AND STATUS_INT >= 0";
        //    lngRes = 0;
        //    lngRes = m_lngGetSampleVOArrByCondition(strCondition,out p_objResultArr);
        //    return lngRes;
        //}
        //#endregion







        //        #region ���ȫ������Ʒ������б� 
        //        /// <summary>
        //        /// ���ȫ������Ʒ������б� 
        //        /// 
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_dtbSampleType">
        //        /// SAMPLE_TYPE_ID_CHR, 
        //        /// SAMPLE_TYPE_DESC_VCHR, 
        //        /// PYCODE_CHR, 
        //        /// WBCODE_CHR, 
        //        /// STDCODE1_CHR, 
        //        /// STDCODE2_CHR, 
        //        /// </param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetSampleTypeList( out System.Data.DataTable p_dtbSampleType)
        //        {
        //            p_dtbSampleType = null;
        //            long lngRes = 0;
        //            com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsLisDeviceSvc","m_lngGetSampleTypeList");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"select sample_type_id_chr, sample_type_desc_vchr, pycode_chr, wbcode_chr,
        //       stdcode1_chr, stdcode2_chr from t_aid_lis_sampletype order by sample_type_id_chr";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbSampleType);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(System.Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ��ȡ���еļ������
        //        /// <summary>
        //        /// ��ȡ���еļ������
        //        /// 
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_dtbCheckCategory"></param>
        //        /// <returns></returns>
        //        public long m_lngGetCheckCategoryList( out System.Data.DataTable p_dtbCheckCategory)
        //        {
        //            p_dtbCheckCategory = null;
        //            long lngRes = 0;
        //            com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege( "com.digitalwave.iCare.middletier.LIS.clsLisDeviceSvc", "m_lngGetCheckCategoryList");
        //            if (lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = @"select trim(a.user_group_id_chr),a.user_group_name_vchr
        //                                from t_aid_lis_appuser_group a
        //                               where not exists (select child_user_group_id_chr
        //                                                   from t_aid_lis_appuser_group_relate b
        //                                                  where a.user_group_id_chr = b.child_user_group_id_chr)";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbCheckCategory);
        //                objHRPSvc.Dispose();
        //            }
        //            catch (System.Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes; 
        //        }
        //        #endregion

        //#region �õ����е�����״̬��Ϣ�б�

        ///// <summary>
        ///// �õ����е�����״̬��Ϣ�б�
        /////  
        ///// </summary>
        ///// <param name="p_objPrincipal"></param>
        ///// <param name="p_dtbSampleState">
        ///// table:t_aid_lis_sample_character
        ///// column:
        ///// character_desc_vchr
        ///// pycode_chr
        ///// wbcode_chr
        ///// sample_type_id_chr
        ///// </param>
        ///// <returns></returns>
        //[AutoComplete]
        //public long m_lngGetSampleState(out System.Data.DataTable p_dtbSampleState)
        //{

        //    p_dtbSampleState = null;
        //    long lngRes = 0;
        //    com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
        //    lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsLisDeviceSvc","m_lngGetSampleState");
        //    if(lngRes < 0)
        //    {
        //        return -1;
        //    }

        //    string strSQL = "SELECT character_desc_vchr, pycode_chr, wbcode_chr, sample_type_id_chr FROM t_aid_lis_sample_character";
        //    try
        //    {
        //        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbSampleState);
        //        objHRPSvc.Dispose();
        //    }
        //    catch(Exception objEx)
        //    {
        //        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //        bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //    }
        //    return lngRes;
        //}
        //#endregion

        //        #region ������Ʒ���ID��ѯ����״̬��Ϣ 

        //        /// <summary>
        //        /// ������Ʒ���ID��ѯ����״̬��Ϣ
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>
        //        /// <param name="p_strSampleTypeID"></param>
        //        /// <param name="p_dtbSampleState">
        //        /// character_desc_vchr
        //        /// </param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetSampleState(string p_strSampleTypeID,out System.Data.DataTable p_dtbSampleState)
        //        {

        //            p_dtbSampleState = null;
        //            long lngRes = 0;
        //            com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsLisDeviceSvc","m_lngGetSampleState");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }

        //            string strSQL = "SELECT character_desc_vchr FROM t_aid_lis_sample_character WHERE sample_type_id_chr = '"+ p_strSampleTypeID +"'";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbSampleState);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����BarCode�жϸñ걾�Ƿ��Ѿ����� 
        //        [AutoComplete]
        //        public long m_lngGetReceptedSampleInfoByBarCode(string p_strBarCode,
        //            out clsT_OPR_LIS_SAMPLE_VO p_objRecord)
        //        {
        //            long lngRes = 0;
        //            p_objRecord = null;
        //            clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
        //            lngRes = objPrivilege.m_lngCheckCallPrivilege("com.digitalwave.iCare.middletier.LIS.clsSampleSvc","m_lngGetReceptedSampleInfoByBarCode");
        //            if(lngRes < 0)
        //            {
        //                return -1;
        //            }
        //            string strSQL = @"SELECT *
        //								FROM t_opr_lis_sample a
        //							   WHERE a.status_int > 3
        //								 AND a.status_int < 6
        //								 AND a.samplekind_chr < 3
        //								 AND a.barcode_vchr = '"+p_strBarCode+"'";
        //            try
        //            {
        //                DataTable dtbResult = new DataTable();
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
        //                objHRPSvc.Dispose();
        //                if(lngRes > 0 && dtbResult.Rows.Count > 0)
        //                {
        //                    p_objRecord = new clsT_OPR_LIS_SAMPLE_VO();
        //                    if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[0]["APPL_DAT"]))
        //                    {
        //                        p_objRecord.m_strAPPL_DAT = Convert.ToDateTime(dtbResult.Rows[0]["APPL_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                    }
        //                    p_objRecord.m_strSEX_CHR = dtbResult.Rows[0]["SEX_CHR"].ToString().Trim();
        //                    p_objRecord.m_strPATIENT_NAME_VCHR = dtbResult.Rows[0]["PATIENT_NAME_VCHR"].ToString().Trim();
        //                    p_objRecord.m_strPATIENT_SUBNO_CHR = dtbResult.Rows[0]["PATIENT_SUBNO_CHR"].ToString().Trim();
        //                    p_objRecord.m_strAGE_CHR = dtbResult.Rows[0]["AGE_CHR"].ToString().Trim();
        //                    p_objRecord.m_strPATIENT_TYPE_CHR = dtbResult.Rows[0]["PATIENT_TYPE_CHR"].ToString().Trim();
        //                    p_objRecord.m_strDIAGNOSE_VCHR = dtbResult.Rows[0]["DIAGNOSE_VCHR"].ToString().Trim();
        //                    p_objRecord.m_strSAMPLETYPE_VCHR = dtbResult.Rows[0]["SAMPLETYPE_VCHR"].ToString().Trim();
        //                    p_objRecord.m_strSAMPLESTATE_VCHR = dtbResult.Rows[0]["SAMPLESTATE_VCHR"].ToString().Trim();
        //                    p_objRecord.m_strBEDNO_CHR = dtbResult.Rows[0]["BEDNO_CHR"].ToString().Trim();
        //                    p_objRecord.m_strICD_VCHR = dtbResult.Rows[0]["ICD_VCHR"].ToString().Trim();
        //                    p_objRecord.m_strPATIENTCARDID_CHR = dtbResult.Rows[0]["PATIENTCARDID_CHR"].ToString().Trim();
        //                    p_objRecord.m_strBARCODE_VCHR = dtbResult.Rows[0]["BARCODE_VCHR"].ToString().Trim();
        //                    p_objRecord.m_strSAMPLE_ID_CHR = dtbResult.Rows[0]["SAMPLE_ID_CHR"].ToString().Trim();
        //                    p_objRecord.m_strPATIENTID_CHR = dtbResult.Rows[0]["PATIENTID_CHR"].ToString().Trim();
        //                    if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[0]["SAMPLING_DATE_DAT"]))
        //                    {
        //                        p_objRecord.m_strSAMPLING_DATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["SAMPLING_DATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                    }
        //                    p_objRecord.m_strOPERATOR_ID_CHR = dtbResult.Rows[0]["OPERATOR_ID_CHR"].ToString().Trim();
        //                    if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[0]["MODIFY_DAT"]))
        //                    {
        //                        p_objRecord.m_strMODIFY_DAT = Convert.ToDateTime(dtbResult.Rows[0]["MODIFY_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                    }
        //                    p_objRecord.m_strAPPL_EMPID_CHR = dtbResult.Rows[0]["APPL_EMPID_CHR"].ToString().Trim();
        //                    p_objRecord.m_strAPPL_DEPTID_CHR = dtbResult.Rows[0]["APPL_DEPTID_CHR"].ToString().Trim();
        //                    p_objRecord.m_intSTATUS_INT = Convert.ToInt32(dtbResult.Rows[0]["STATUS_INT"].ToString().Trim());
        //                    p_objRecord.m_strSAMPLE_TYPE_ID_CHR = dtbResult.Rows[0]["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
        //                    p_objRecord.m_strQCSAMPLEID_CHR = dtbResult.Rows[0]["QCSAMPLEID_CHR"].ToString().Trim();
        //                    p_objRecord.m_strSAMPLEKIND_CHR = dtbResult.Rows[0]["SAMPLEKIND_CHR"].ToString().Trim();
        //                    if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[0]["CHECK_DATE_DAT"]))
        //                    {
        //                        p_objRecord.m_strCHECK_DATE_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CHECK_DATE_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                    }
        //                    if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[0]["ACCEPT_DAT"]))
        //                    {
        //                        p_objRecord.m_strACCEPT_DAT = Convert.ToDateTime(dtbResult.Rows[0]["ACCEPT_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                    }
        //                    p_objRecord.m_strACCEPTOR_ID_CHR = dtbResult.Rows[0]["ACCEPTOR_ID_CHR"].ToString().Trim();
        //                    p_objRecord.m_strAPPLICATION_ID_CHR = dtbResult.Rows[0]["APPLICATION_ID_CHR"].ToString().Trim();
        //                    p_objRecord.m_strPATIENT_INHOSPITALNO_CHR = dtbResult.Rows[0]["PATIENT_INHOSPITALNO_CHR"].ToString().Trim();
        //                    if(Microsoft.VisualBasic.Information.IsDate(dtbResult.Rows[0]["CONFIRM_DAT"]))
        //                    {
        //                        p_objRecord.m_strCONFIRM_DAT = Convert.ToDateTime(dtbResult.Rows[0]["CONFIRM_DAT"]).ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //                    }
        //                    p_objRecord.m_strCONFIRMER_ID_CHR = dtbResult.Rows[0]["CONFIRMER_ID_CHR"].ToString().Trim();
        //                    p_objRecord.m_strCOLLECTOR_ID_CHR = dtbResult.Rows[0]["COLLECTOR_ID_CHR"].ToString().Trim();
        //                }
        //            }
        //            catch(Exception objEx)
        //            {
        //                string strTmp=objEx.Message;
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);
        //            }
        //            return lngRes;
        //        }
        //        #endregion



        //        #region ��ѯ��ĳ��ʱ���ڲɼ��������뵫δ���յı걾 ͯ�� 
        //        [AutoComplete]
        //        public long m_lngGetAllNotReceptSample(string p_strFromDat,string p_strToDat,out DataTable p_dtbResult)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT t1.*,
        //									 t2.emergency_int,t2.special_int,t2.application_form_no_chr
        //								FROM t_opr_lis_sample t1, t_opr_lis_application t2
        //							   WHERE t1.APPLICATION_ID_CHR = t2.APPLICATION_ID_CHR
        //								 AND t1.status_int = 2
        //								 AND t2.pstatus_int > 0
        //								 AND t1.sampling_date_dat BETWEEN TO_DATE ('"+p_strFromDat+@"',
        //																		   'yyyy-mm-dd hh24:mi:ss'
        //																		   )
        //															   AND TO_DATE ('"+p_strToDat+@"',
        //																			'yyyy-mm-dd hh24:mi:ss'
        //																		   )";
        //            p_dtbResult = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbResult);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region �������ڷ�Χ��ѯ�Ѻ��յı걾
        //        [AutoComplete]
        //        public long m_lngGetReceptedSampleByDateRange(string p_strDeviceID,string p_strFromDat,
        //            string p_strToDat,out DataTable p_dtbResult)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT   t2.devicename_vchr, t3.barcode_vchr, t1.check_dat, t1.positionid_chr,
        //								       t3.samplekind_chr, t3.ACCEPT_DAT,t1.seq_id_chr
        //								 FROM t_opr_lis_device_relation t1,
        //									  t_bse_lis_device t2,
        //									  t_opr_lis_sample t3
        //								 WHERE t1.deviceid_chr = '"+p_strDeviceID+@"'
        //								 AND t1.status_int > 0
        //								 AND t1.deviceid_chr = t2.deviceid_chr
        //								 AND t1.sample_id_chr = t3.sample_id_chr
        //								 AND t3.status_int > 0
        //								 AND t3.SAMPLEKIND_CHR = 1
        //								 AND t1.reception_dat BETWEEN TO_DATE ('"+p_strFromDat+@"',
        //                                        'YYYY-MM-DD hh24:mi:ss'
        //                                       )
        //								 AND TO_DATE ('"+p_strToDat+@"',
        //                                        'YYYY-MM-DD hh24:mi:ss'
        //                                       )
        //							ORDER BY t3.ACCEPT_DAT";
        //            p_dtbResult = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbResult);  
        //                objHRPSvc.Dispose(); 
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region ����ʱ����������ı�־λ��Ϣ
        [AutoComplete]
        public long m_lngModifySampleInfoOnRecepting( clsT_OPR_LIS_SAMPLE_VO p_objRecord)
        {
            long lngRes = 0; 
            string strSQL = @"UPDATE t_opr_lis_sample
								 SET status_int = '" + p_objRecord.m_intSTATUS_INT + @"',
									 accept_dat = TO_DATE ('" + p_objRecord.m_strACCEPT_DAT + @"', 'yyyy-mm-dd hh24:mi:ss')
							   WHERE status_int > 0 AND sample_id_chr = '" + p_objRecord.m_strSAMPLE_ID_CHR + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

        #region ���������걾
        [AutoComplete]
        public long m_lngReceptSample( clsT_OPR_LIS_SAMPLE_VO p_objSampleVO,
            clsT_LIS_DeviceRelationVO p_objDeviceRelationVO)
        {
            long lngRes = 0; 

            p_objSampleVO.m_strACCEPT_DAT = System.DateTime.Now.ToString();
            p_objDeviceRelationVO.m_strRECEPTION_DAT = p_objSampleVO.m_strACCEPT_DAT;
            //1.����DeviceRelation
            lngRes = m_lngAddNewDeviceRelation( p_objDeviceRelationVO);
            if (lngRes > 0)
            {
                //2.���±걾״̬
                lngRes = m_lngModifySampleInfoOnRecepting( p_objSampleVO);
            }
            return lngRes;
        }
        #endregion







        //        #region ��ѯ����δ���յı걾����δ����ģ�
        //        /// <summary>
        //        /// ��ѯ����δ���յı걾����δ����ģ� ���� 2004.05.06
        //        /// </summary>
        //        /// <param name="p_objPrincipal"></param>		
        //        /// <param name="dtbAllNotReceptSample"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public long m_lngGetAllNotReceptSample(out DataTable dtbAllNotReceptSample)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT t1.*
        //								FROM t_opr_lis_sample t1
        //							   WHERE t1.status_int = '2'
        //								";
        //            dtbAllNotReceptSample = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
        //                objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbAllNotReceptSample);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        #region �������,���޸���ص����б�־λ
        /// <summary>
        /// �������,���޸���ص����б�־λ 
        /// ���� 2004.05.11
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSampleID"></param>
        /// <returns>
        /// С�ڵ���0,Ϊ���ݿ��������, 
        /// -10 : ָ��������������(������δ������,���Ѿ����), 
        /// 1�� �ɹ����, 
        /// 2���ɹ���ˣ����Ҹñ걾�����ļ��鵥�µ����б걾ҵ��ȫ�����, 
        /// 3���ɹ���ˣ����Ҹñ걾���������뵥�µ����б걾������ˡ�
        /// </returns>
        [AutoComplete]
        public long m_lngAuditingSample( string p_strSampleID)
        {
            long lngRes = 0;
            long lngEff = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            #region ���� Sample ��
            string strSQL = "UPDATE t_opr_lis_sample SET status_int = 6 WHERE  status_int = 5 AND sample_id_chr = ? ";

            try
            {
                IDataParameter[] objParamArr = clsIDataParameterCreator.m_objConstructIDataParameterArr(p_strSampleID);
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objParamArr);
                objHRPSvc.Dispose();
                if (lngEff == 0 && lngRes > 0)
                {
                    lngRes = -10;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
                lngRes = 0;
                lngEff = 0;
            }
            #endregion

            if (lngRes > 0 && lngEff > 0)
            {
                #region ���¼��鵥�ı�־
                //				strSQL = "";
                //
                //				try
                //				{
                //					IDataParameter objParamArr = clsIDataParameterCreator.m_objConstructIDataParameterArr("");
                //					lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL,lngEff,objParamArr);
                //				}
                //				catch(Exception objEx)
                //				{
                //					com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                //					bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
                //					lngRes = 0;
                //					logEff = 0;
                //				}
                #endregion
            }

            if (lngRes > 0 && lngEff > 0)
            {
                #region �������뵥�ı�־
                //				strSQL = "";
                //
                //				try
                //				{
                //					IDataParameter objParamArr = clsIDataParameterCreator.m_objConstructIDataParameterArr("");
                //					lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL,lngEff,objParamArr);
                //				}
                //				catch(Exception objEx)
                //				{
                //					com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                //					bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
                //					lngRes = 0;
                //					logEff = 0;
                //				}
                #endregion
            }

            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion











        //        #region ���ݼ��������źͼ����飨��һ�㣩��ѯ�Ѿ��ɼ��ĸ��걾���� 
        //        [AutoComplete]
        //        public long m_lngGetAllSampleCountByApplFormNoAndGroupID(string strApplFormNo,string strGroupID,out DataTable dtbGroupSampleCount)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT   COUNT (t1.sample_id_chr) as count, t2.sample_type_id_chr
        //								FROM t_opr_lis_applgrpsmp t1,
        //									 t_opr_lis_sample t2
        //							   WHERE t1.sample_id_chr = t2.sample_id_chr
        //								 AND t2.application_form_no_chr = '"+strApplFormNo+@"'
        //								 AND t1.groupid_chr = '"+strGroupID+@"'
        //							GROUP BY t2.sample_type_id_chr";
        //            dtbGroupSampleCount = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbGroupSampleCount);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion




        //        #region ��ѯt_opr_lis_application_detail�µ�Group�ı걾״̬ 
        //        [AutoComplete]
        //        public long m_lngGetSampleStatusByGroup(string strSampleID,string strApplFormNo,out DataTable dtbGroupSample)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT t1.sample_id_chr,t2.status_int,t1.groupid_chr
        //								FROM t_opr_lis_applgrpsmp t1,
        //									 t_opr_lis_sample t2
        //							   WHERE t1.groupid_chr = (SELECT groupid_chr
        //														 FROM t_opr_lis_applgrpsmp
        //													    WHERE sample_id_chr = '"+strSampleID+@"')
        //														  AND t1.application_form_no_chr = '"+strApplFormNo+@"'
        //														  AND t1.sample_id_chr = t2.sample_id_chr";
        //            dtbGroupSample = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbGroupSample);  
        //                objHRPSvc.Dispose(); 
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ���ݼ��������Ų�ѯ�Ѿ��ɼ��ĸ��걾���� 
        //        [AutoComplete]
        //        public long m_lngGetAllSampleCountByApplFormNo(string strApplFormNo,out DataTable dtbAllSampleCount)
        //        {
        //            long lngRes = 0;
        //            string strSQL = @"SELECT   COUNT (sample_type_id_chr) as count, sample_type_id_chr
        //								FROM t_opr_lis_sample
        //							  WHERE application_form_no_chr = '"+strApplFormNo
        //                                +"' GROUP BY sample_type_id_chr";
        //            dtbAllSampleCount = null;
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbAllSampleCount);
        //                objHRPSvc.Dispose();
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //            }
        //            return lngRes;
        //        }
        //        #endregion


        #region ����DeviceID��DeviceSampleID���ñ�t_opr_lis_device_relation�ı걾��¼״̬
        [AutoComplete]
        public long m_lngSetStatus( ref clsDeviceRelation_VO objDeviceRelationVO)
        {
            long lngRes = 0;
            string strDeviceSampleID = objDeviceRelationVO.m_strDevice_SampleID;
            string strDeviceID = objDeviceRelationVO.m_strDeviceID;
            string strCheckDat = objDeviceRelationVO.m_strCheck_dat;
            string strSQL = @"UPDATE t_opr_lis_device_relation
							      SET status_int = '0'
						      WHERE TRIM(device_sampleid_chr) = '" + strDeviceSampleID + "' AND deviceid_chr = '" + strDeviceID + "' AND check_dat=to_date('" + strCheckDat + "','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

        #region ����DeviceID��DeviceSampleID����t_opr_lis_device_relation�ı걾��λ��
        [AutoComplete]
        public long m_lngSetPositionANDSampleID( ref clsDeviceRelation_VO objDeviceRelationVO, ref clsSampleVO objSampleVO)
        {
            long lngRes = 0;
            string strDeviceSampleID = objDeviceRelationVO.m_strDevice_SampleID;
            string strDeviceID = objDeviceRelationVO.m_strDeviceID;
            string strPosition = objDeviceRelationVO.m_strPositionID;
            string strCheckDat = objDeviceRelationVO.m_strCheck_dat;
            string strSampleID = objSampleVO.m_strSAMPLE_ID;
            string strSQL = @"UPDATE t_opr_lis_device_relation SET positionid_chr = '" + strPosition + "', sample_id_chr = '" + strSampleID + "' ";
            strSQL += @" WHERE TRIM(device_sampleid_chr) = '" + strDeviceSampleID + "' AND deviceid_chr = '" + strDeviceID + "' AND check_dat = TO_DATE('" + strCheckDat + "','yyyy-mm-dd hh24:mi:ss') AND STATUS_INT > 0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��      
            }
            return lngRes;
        }
        #endregion

        //#region ���ݼ���������ϵĺŲ���������Ѿ��е���Ʒ��
        //[AutoComplete]
        //public long m_lngGetSampleInfoByFormId(
        //    string strFormNo, out System.Data.DataTable dtbSampleInfo)
        //{
        //    string strSQL = @"select SAMPLE_ID_CHR,MODIFY_DAT,sample_type_id_chr,sampletype_vchr,samplestate_vchr,barcode_vchr,sampling_date_dat,operator_id_chr from t_opr_lis_sample where application_form_no_chr='"+strFormNo+"'";

        //    long lngRes = 0;//Ϊ0��ʾû�гɹ�������0Ϊ�ɹ���
        //    dtbSampleInfo = null;
        //    try
        //    {
        //        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSampleInfo);
        //        objHRPSvc.Dispose();
        //    }
        //    catch(System.Exception objEx)
        //    {
        //        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //        bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
        //    }
        //    return lngRes;
        //}
        //#endregion


        #region ����һ���������������С�Old
        [AutoComplete]
        public long m_lngAddSample( ref clsSampleVO aSampleVO)
        {
            string strSQL = @"INSERT INTO t_opr_lis_sample(COLLECTOR_ID_CHR,SAMPLE_ID_CHR,samplekind_chr, 
                             appl_dat, sex_chr, patient_name_vchr, patient_subno_chr, age_chr, patient_type_chr,
                             diagnose_vchr, sampletype_vchr, samplestate_vchr, bedno_chr, icd_vchr,
                             patientcardid_chr, barcode_vchr, patientid_chr, sampling_date_dat, operator_id_chr,
                             appl_empid_chr, appl_deptid_chr,sample_type_id_chr,QCSampleID_chr,STATUS_INT,
							CHECK_DATE_DAT,ACCEPT_DAT,ACCEPTOR_ID_CHR,APPLICATION_ID_CHR,PATIENT_INHOSPITALNO_CHR,
							CONFIRM_DAT,CONFIRMER_ID_CHR,MODIFY_DAT)VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(25, out objDPArr);
                //if(Microsoft.VisualBasic.Information.IsDate(aSampleVO.m_strMODIFY_DAT))
                //	objDPArr[0].Value = aSampleVO.;
                objDPArr[24].Value = System.DateTime.Now;  //Parse(aSampleVO.m_strMODIFY_DAT);
                if (Microsoft.VisualBasic.Information.IsDate(aSampleVO.m_strAppl_Dat))
                    objDPArr[3].Value = System.DateTime.Now;//System.DateTime.Parse(aSampleVO.m_strAppl_Dat);
                objDPArr[4].Value = aSampleVO.m_strSex;
                objDPArr[5].Value = aSampleVO.m_strPatient_Name;
                objDPArr[6].Value = aSampleVO.m_strPatient_SubNO;
                objDPArr[7].Value = aSampleVO.m_strAge;
                objDPArr[8].Value = aSampleVO.m_strPatient_Type;
                objDPArr[9].Value = aSampleVO.m_strDiagnose;
                objDPArr[10].Value = aSampleVO.m_strSampleType;
                objDPArr[11].Value = aSampleVO.m_strSampleState;
                objDPArr[12].Value = aSampleVO.m_strBedNO;
                objDPArr[13].Value = aSampleVO.m_strIcd;
                objDPArr[14].Value = aSampleVO.m_strPatientCardID;
                objDPArr[15].Value = aSampleVO.m_strBarCode;
                objDPArr[16].Value = aSampleVO.m_strPatientID;
                //if (Microsoft.VisualBasic.Information.IsDate(aSampleVO.m_strSampling_Dat))
                aSampleVO.m_strSampling_Dat = objDPArr[24].Value.ToString();
                objDPArr[17].Value = objDPArr[24].Value;
                objDPArr[18].Value = aSampleVO.m_strOperator_ID;
                objDPArr[19].Value = aSampleVO.m_strAppl_EmpID;
                objDPArr[20].Value = aSampleVO.m_strAppl_DeptID;
                objDPArr[21].Value = aSampleVO.m_strSample_Type_Id;
                objDPArr[22].Value = aSampleVO.m_strQCSampleID;
                objDPArr[23].Value = aSampleVO.m_strStatus;
                objDPArr[2].Value = aSampleVO.m_strSampleKind;

                aSampleVO.m_strSAMPLE_ID = objHRPSvc.m_strGetNewID("t_opr_lis_sample", "SAMPLE_ID_CHR", 10);
                objDPArr[1].Value = aSampleVO.m_strSAMPLE_ID.Trim();

                long lngRecEff = -1;

                for (int i = 0; i < objDPArr.Length; i++)
                {
                    if (objDPArr[i].Value == null)
                    {
                        objDPArr[i].Value = System.DBNull.Value;
                    }
                }
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr);
                objHRPSvc.Dispose();
            }
            catch (System.Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion


        #region ����SampleBarCode���ñ걾��״̬
        [AutoComplete]
        public long m_lngSetSampleStatusBySampleBarCode( string p_strSampleBarCode, int p_intSampleStatus)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_lis_sample SET status_int=" + p_intSampleStatus.ToString() + @" 
					WHERE barcode_vchr='" + p_strSampleBarCode + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��      
            }
            return lngRes;
        }
        #endregion

        #region ����SampleId���ñ걾��״̬
        [AutoComplete]
        public long m_lngSetSampleStatusBySampleId( string p_strSampleId, int p_intSampleStatus)
        {
            long lngRes = 0;
            string strModifyDat = System.DateTime.Now.ToString().Trim();
            string strSQL = @"UPDATE t_opr_lis_sample SET status_int=" + p_intSampleStatus.ToString() + @", 
					MODIFY_DAT=TO_DATE('" + strModifyDat + "','YYYY-MM-DD hh24:mi:ss') WHERE SAMPLE_ID_CHR='" + p_strSampleId + "' AND status_int > 0";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��      
            }
            return lngRes;
        }
        #endregion

        //        #region ��ѯ���浥�ϵ�������Ϣ��һЩ���뵥��Ϣ(��Щ�ֶ���ID��Ҫ��ѯ��ر����ID��Ӧ��˵��) 
        //        [AutoComplete]
        //        public long  m_lngGetApplSampleInfo(string p_strSampleID,out System.Data.DataTable p_dtbSample)
        //        {
        //            long lngRes=0;
        //            p_dtbSample=null;
        //            string strSQL=@"SELECT t1.sample_id_chr,t1.sampletype_vchr, t1.samplestate_vchr,t2.patient_name_vchr,t3.deptname_vchr,t2.sex_chr,
        //							t2.patient_subno_chr,t4.lastname_vchr,t2.age_chr,t2.bedno_chr,t2.diagnose_vchr,t5.summary_vchr
        //							FROM t_opr_lis_sample t1,
        //								t_opr_lis_application t2,
        //								t_bse_deptdesc t3,
        //								t_bse_employee t4,
        //								t_opr_lis_application_detail t5,
        //								t_opr_lis_applgrpsmp t6      
        //							WHERE t1.sample_id_chr = '"+p_strSampleID+@"' and (t2.appl_deptid_chr=t3.deptid_chr(+))
        //							and (t2.appl_empid_chr=t4.empid_chr(+))
        //							AND t6.groupid_chr = t5.groupid_chr
        //							AND t6.sample_id_chr = t1.sample_id_chr
        //							AND t5.application_id_chr = t2.application_id_chr
        //							AND t2.modify_dat = t5.modify_dat
        //							AND t2.pstatus_int > 0
        //							and t1.application_form_no_chr=t2.application_form_no_chr";

        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbSample);  
        //                objHRPSvc.Dispose(); 
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��                 
        //            }

        //            return lngRes;
        //        }
        //        #endregion






        //        #region ����Application_ID���Ҹ����뵥�и��ּ������ĸ�����Ʒ������ 
        //        [AutoComplete]
        //        public long m_lngGetSampleTotalQtyByApplicationID(string p_strApplication_ID, out System.Data.DataTable p_dtbSampleQty)
        //        {
        //            long lngRes=0;
        //            p_dtbSampleQty = null;

        //            try
        //            {
        ////				System.Data.IDataParameter[] objDPArr = null;
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        ////				objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
        ////				objDPArr[0].Value = p_strApplication_ID;
        ////				lngRes = objHRPSvc.lngGetDataTableWithParameters(c_strGetSampleTotalQtyByApplicationID, ref p_dtbSampleQty, objDPArr);


        //                string strSQL = @"SELECT c.sample_type_id_chr, c.total, 
        //				d.SAMPLE_TYPE_DESC_VCHR FROM 
        //				(SELECT b.sample_type_id_chr, SUM(b.sample_qty_chr) AS total 
        //				FROM t_opr_lis_application_detail a, t_aid_lis_group_sample b
        //				WHERE a.STATUS_INT > 0 AND a.application_id_chr = '" + p_strApplication_ID + "' " + 
        //                @"AND a.groupid_chr = b.groupid_chr
        //				GROUP BY b.sample_type_id_chr) c, t_aid_lis_sampletype d
        //				WHERE c.sample_type_id_chr = d.sample_type_id_chr"; 
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbSampleQty);
        //                objHRPSvc.Dispose();
        //            }


        //            catch(System.Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;
        //        }

        //        #endregion

        //        #region ����DevID��ѯ��t_opr_lis_device_relation����ѯ�Ѻ��յ�,STATUS_INT=1�� 
        //        [AutoComplete]
        //        public long m_lngGetDevRelationInfo(string p_strDevID,out System.Data.DataTable p_dtbDevRelation)
        //        {
        //            long lngRes=0;
        //            p_dtbDevRelation=null;
        //            string strFromNow = System.DateTime.Now.ToShortDateString() + " 00:00:00";
        //            string strToNow = System.DateTime.Now.ToShortDateString() + " 23:59:59";
        //            string strSQL=@"SELECT t2.devicename_vchr, t3.barcode_vchr, t1.check_dat, t1.positionid_chr,t3.samplekind_chr
        //							FROM t_opr_lis_device_relation t1, t_bse_lis_device t2, t_opr_lis_sample t3
        //							WHERE t1.deviceid_chr = '"+p_strDevID+@"'
        //							AND t1.status_int = 1
        //							AND t1.deviceid_chr = t2.deviceid_chr
        //							AND t1.sample_id_chr = t3.sample_id_chr
        //							AND t3.modify_dat BETWEEN TO_DATE ('"+strFromNow+@"',
        //                                        'YYYY-MM-DD hh24:mi:ss'
        //                                       )
        //                           AND TO_DATE ('"+strToNow+@"',
        //                                        'YYYY-MM-DD hh24:mi:ss'
        //                                       )
        //							AND t3.status_int > 0 order by t1.SEQ_ID_CHR";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbDevRelation);  
        //                objHRPSvc.Dispose(); 
        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        //        #endregion

        //        #region ����ĳһ���뵥��ʹ��ĳһ����Ʒ�����м�����
        //        [AutoComplete]
        //        public long m_lngGetCheckGroupListByAppID_SampleType(string p_strApplication_ID, string p_strSampleTypeID, out System.Data.DataTable p_dtbCheckGroupList)
        //        {
        //            long lngRes=0;
        //            p_dtbCheckGroupList = null;

        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

        //                string strSQL = @"SELECT d.groupname_vchr, c.sample_type_desc_vchr, a.groupid_chr,
        //				b.sample_qty_chr, b.sample_valid_time ,a.status_int
        //				FROM t_opr_lis_application_detail a, t_aid_lis_group_sample b, t_aid_lis_sampletype c, 
        //				t_aid_lis_check_group d
        //				WHERE a.status_int > 0 AND a.groupid_chr = b.groupid_chr AND b.sample_type_id_chr = c.sample_type_id_chr
        //				AND a.groupid_chr = d.groupid_chr AND a.application_id_chr = '" + p_strApplication_ID + "' " +
        //                @"AND b.sample_type_id_chr = '" + p_strSampleTypeID + "'";


        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbCheckGroupList);
        //                objHRPSvc.Dispose();
        //            }

        //            catch(System.Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;

        //        }
        //        #endregion

        //        #region 
        //        [AutoComplete]
        //        public long m_lngGetSampleDetailByAppID_SampleType(
        //            string p_strApplication_ID, string p_strSampleTypeID, 
        //            out clsSampleVO[] colSampleList)
        //        {
        //            long lngRes=0;
        //            colSampleList = null;

        //            try
        //            {

        //                string strSQL = @"SELECT   c.*, g.sample_valid_time, h.sample_type_desc_vchr
        //				FROM (SELECT b.application_id_chr, a.application_form_no_chr,
        //				a.barcode_vchr, a.sample_id_chr, a.sampling_date_dat,
        //				a.operator_id_chr, a.sample_type_id_chr, a.samplestate_vchr
        //				FROM t_opr_lis_sample a, t_opr_lis_application b
        //				WHERE b.application_id_chr = '" + p_strApplication_ID + "' " +
        //                    @"AND a.application_form_no_chr = b.application_form_no_chr
        //				AND a.status_int > 0 AND b.pstatus_int > 0) c,
        //				(SELECT e.application_id_chr, f.sample_type_id_chr,
        //				MIN (f.sample_valid_time) AS sample_valid_time
        //				FROM t_opr_lis_application_detail e,
        //				t_aid_lis_group_sample f
        //				WHERE e.groupid_chr = f.groupid_chr
        //				AND e.status_int > 0
        //				AND e.application_id_chr = '" + p_strApplication_ID + "' " +
        //                    @"GROUP BY e.application_id_chr, f.sample_type_id_chr) g,
        //				t_aid_lis_sampletype h
        //				WHERE c.application_id_chr = g.application_id_chr
        //				AND c.sample_type_id_chr = g.sample_type_id_chr
        //				AND c.sample_type_id_chr = '" + p_strSampleTypeID + "' " +
        //                    @"AND c.sample_type_id_chr = h.sample_type_id_chr
        //				ORDER BY c.sampling_date_dat";

        //                System.Data.DataTable dtbSampleList = null;
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSampleList);
        //                objHRPSvc.Dispose();
        //                if(lngRes == 1)
        //                {
        //                    int intCount = dtbSampleList.Rows.Count;
        //                    if (intCount > 0)
        //                    {
        //                        colSampleList = new clsSampleVO[intCount];
        //                        for(int i=0; i<intCount; i++)
        //                        {
        //                            System.Data.DataRow objRow = dtbSampleList.Rows[i];
        //                            colSampleList[i] = new clsSampleVO();
        //                            ConstructSimpleSampleVO(objRow, ref colSampleList[i]);
        //                        }
        //                    }
        //                }
        //            }

        //            catch(System.Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 

        //            }
        //            return lngRes;

        //        }

        //        #endregion

        #region ����BarCode��ѯ���뵥����Ӧ������Group(������)
        //        public long m_lngGetGroupByBarCode( string p_strBarCode,out System.Data.DataTable p_dtbGroup)
        //        {
        //            long lngRes=0;
        //            p_dtbGroup=null;
        //            string strSQL=@"SELECT t4.groupid_chr, t4.groupname_vchr
        //							FROM t_opr_lis_sample t1,
        //								t_opr_lis_application t2,
        //								t_opr_lis_application_item t3,
        //								t_aid_lis_check_group t4,
        //								t_aid_lis_group_sample t5
        //							WHERE t1.barcode_vchr = '"+p_strBarCode+@"'
        //							AND t1.status_int > 0
        //							AND t1.application_form_no_chr = t2.application_form_no_chr
        //							AND t3.application_id_chr = t2.application_id_chr
        //							AND t2.pstatus_int > 0
        //							AND t3.groupid_chr = t4.groupid_chr
        //							AND t3.Modify_Dat = t2.Modify_Dat
        //							AND t4.groupid_chr = t5.groupid_chr
        //							AND t5.sample_type_id_chr = t1.sample_type_id_chr";
        //            try
        //            {
        //                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtbGroup);  
        //                objHRPSvc.Dispose(); 

        //            }
        //            catch(Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
        //            }
        //            return lngRes;
        //        }
        #endregion

        #region ���� SampleVO
        [AutoComplete]
        private void ConstructSimpleSampleVO(System.Data.DataRow objRow, ref clsSampleVO objSampleVO)
        {
            if (objRow["application_id_chr"] != System.DBNull.Value)
            { objSampleVO.m_strApplication_ID = objRow["application_id_chr"].ToString().Trim(); }

            if (objRow["application_form_no_chr"] != System.DBNull.Value)
            { objSampleVO.m_strApplication_Form_No = objRow["application_form_no_chr"].ToString().Trim(); }

            if (objRow["barcode_vchr"] != System.DBNull.Value)
            { objSampleVO.m_strBarCode = objRow["barcode_vchr"].ToString().Trim(); }

            if (objRow["sample_id_chr"] != System.DBNull.Value)
            { objSampleVO.m_strSAMPLE_ID = objRow["sample_id_chr"].ToString().Trim(); }

            if (objRow["sampling_date_dat"] != System.DBNull.Value)
            { objSampleVO.m_strSampling_Dat = objRow["sampling_date_dat"].ToString().Trim(); }

            if (objRow["operator_id_chr"] != System.DBNull.Value)
            { objSampleVO.m_strOperator_ID = objRow["operator_id_chr"].ToString().Trim(); }

            if (objRow["sample_type_id_chr"] != System.DBNull.Value)
            { objSampleVO.m_strSample_Type_Id = objRow["sample_type_id_chr"].ToString().Trim(); }

            if (objRow["sample_valid_time"] != System.DBNull.Value)
            { objSampleVO.m_strSample_Valid_Time = objRow["sample_valid_time"].ToString().Trim(); }

            if (objRow["sample_type_desc_vchr"] != System.DBNull.Value)
            { objSampleVO.m_strSampleType = objRow["sample_type_desc_vchr"].ToString().Trim(); }

            if (objRow["samplestate_vchr"] != System.DBNull.Value)
            {
                objSampleVO.m_strSampleState = objRow["samplestate_vchr"].ToString().Trim();
            }


        }
        #endregion
        [AutoComplete]
        public long m_lngGetSampleInfoByBarCode( string strBarCode, out clsSampleVO objSampleVO)
        {
            objSampleVO = null;
            return 0;
        }


        #region ����������������Ϣ
        /// <summary>
        /// ����������������Ϣ
        /// </summary>
        /// <param name="p_objSampleFeedBack"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertSampleFeedBack(clslissample_feedback p_objSampleFeedBack)
        {
            long lngRes = 0;
            string strSQL = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"update t_opr_lis_sample t
                               set t.status_int = 2, t.issampleback = 1, t.sample_back_reason = ? 
                             where t.sample_id_chr = ?
                               and t.status_int > 0
                            ";
                IDataParameter[] objDPArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objSampleFeedBack.m_strSample_Back_Reason_vchr;
                objDPArr[1].Value = p_objSampleFeedBack.m_strSample_id_chr;
                long lngEff = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                if (lngRes > 0)
                {
                    #region ɾ�������Ϣ
                    // ɾ�������Ϣ
                    string packInfo = string.Empty;
                    DataTable dt = null;
                    strSQL = @"select t.barcode_vchr from t_opr_lis_sample t where t.sample_id_chr = ? and t.status_int > 0";
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = p_objSampleFeedBack.m_strSample_id_chr;
                    objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
                    string barCode = dt.Rows[0]["barcode_vchr"].ToString();

                    strSQL = @"select t.packid, t.recorderid, t.recorddate from t_samplepack t where t.barcode = ?";
                    objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = barCode;
                    objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strSQL = @"delete from t_samplepack where barcode = ?";
                        objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = barCode;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                        strSQL = @"delete from t_samplepack_detail where barcode = ?";
                        objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                        objDPArr[0].Value = barCode;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                        packInfo = "���ID:" + dt.Rows[0]["packid"].ToString() + " �����:" + dt.Rows[0]["recorderid"].ToString() + " ���ʱ��:" + dt.Rows[0]["recorddate"].ToString();
                    }
                    #endregion

                    int intSampelFeedBackID = 0;
                    lngRes = objHRPSvc.m_lngGetSequences("seq_lis_sample_feed_back", out intSampelFeedBackID);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;

                    }
                    strSQL = @"insert into t_opr_lis_sample_feedback
                                  (samplebackid_chr,
                                   feedback_date_date,
                                   patient_name_vchr,
                                   patient_inhospitalno_vchr,
                                   bedno_chr,
                                   appl_empid_chr,
                                   sample_back_reason_vchr,
                                   back_empid_chr,
                                   sample_id_chr,
                                   packinfo)
                                values
                                  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
                                ";
                    p_objSampleFeedBack.m_strSampleBackid_chr = intSampelFeedBackID.ToString().PadLeft(6, '0');
                    objHRPSvc.CreateDatabaseParameter(10, out objDPArr);
                    DateTime dtcurrentTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr[0].Value = p_objSampleFeedBack.m_strSampleBackid_chr;
                    objDPArr[1].Value = dtcurrentTime;
                    objDPArr[2].Value = p_objSampleFeedBack.m_strPatient_Name_vchr;
                    objDPArr[3].Value = p_objSampleFeedBack.m_strPatient_Inhospitalno_vchr;
                    objDPArr[4].Value = p_objSampleFeedBack.m_strBedno_chr;
                    objDPArr[5].Value = p_objSampleFeedBack.m_strAppl_Empid_chr;
                    objDPArr[6].Value = p_objSampleFeedBack.m_strSample_Back_Reason_vchr;
                    objDPArr[7].Value = p_objSampleFeedBack.m_strBack_Empid_chr;
                    objDPArr[8].Value = p_objSampleFeedBack.m_strSample_id_chr;
                    objDPArr[9].Value = packInfo;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);



                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                strSQL = null;
                objHRPSvc = null;
                p_objSampleFeedBack = null;
            }
            return lngRes;
        }
        #endregion


        #region �������õ����
        /*

            #region SQL���
        private const string c_strAddNewReqCheck = @"INSERT INTO t_opr_lis_req_check(GROUPID_CHR, 
                                SAMPLE_ID_CHR,stepflag_chr,print_dat,seq_int)VALUES(?,?,?,?,?)";

        private const string c_strAddNewReqCheckDetail = @"INSERT INTO t_opr_lis_req_check_detail(groupid_chr, 
                                       check_item_id_chr,sample_id_chr)VALUES(?,?,?)";

        private const string c_strGetSampleTotalQtyByApplicationID = @"SELECT c.sample_type_id_chr, c.total, 
				d.SAMPLE_TYPE_DESC_VCHR FROM 
				(SELECT b.sample_type_id_chr, SUM(b.sample_qty_chr) AS total 
				FROM t_opr_lis_application_detail a, t_aid_lis_group_sample b
				WHERE a.STATUS_INT = 1 AND a.application_id_chr = ?  
				AND a.groupid_chr = b.groupid_chr
				GROUP BY b.sample_type_id_chr) c, t_aid_lis_sampletype d
				WHERE c.sample_type_id_chr = d.sample_type_id_chr ";

        private const string c_strGetApplicationGroupsByAppID_SampleType = @"SELECT d.groupname_vchr, 
				c.sample_type_desc_vchr, b.sample_qty_chr, b.sample_valid_time 
				FROM t_opr_lis_application_detail a, t_aid_lis_group_sample b, t_aid_lis_sampletype c, 
				t_aid_lis_check_group d
				WHERE a.status_int = 1 AND a.groupid_chr = b.groupid_chr AND b.sample_type_id_chr = c.sample_type_id_chr
				AND a.groupid_chr = d.groupid_chr AND a.application_id_chr = ? 
				AND b.sample_type_id_chr = ?";
        #endregion

            #region ����t_opr_lis_req_check����һ���¼�¼ л�ɺ� 2004.2.16

        [AutoComplete]
        public long m_mthAddNewReqCheck( clsReqCheck_VO p_objReqCheck_VO)
        {
            long lngRes = 0;
            try
            {
                System.Data.IDataParameter[] objReqCheckArr = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(5, out objReqCheckArr);

                objReqCheckArr[0].Value = p_objReqCheck_VO.m_strGroupID;
                objReqCheckArr[1].Value = p_objReqCheck_VO.m_strSample_ID;
                objReqCheckArr[2].Value = p_objReqCheck_VO.m_strStepFlag;
                if (Microsoft.VisualBasic.Information.IsDate(p_objReqCheck_VO.m_strPrint_Dat))
                { objReqCheckArr[3].Value = System.DateTime.Parse(p_objReqCheck_VO.m_strPrint_Dat); }
                int intSeq = objHRPSvc.intGetNewNumericID("seq_int", "t_opr_lis_req_check");
                objReqCheckArr[4].Value = intSeq;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddNewReqCheck, ref lngRecEff, objReqCheckArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

            #region ����t_opr_lis_reg_check_detail����һ���¼�¼ л�ɺ� 2004.2.16

        [AutoComplete]
        public long m_mthAddNewReqCheckDetail( clsReqCheckDetail_VO p_objReqCheckDetail_VO)
        {
            long lngRes = 0;
            try
            {
                System.Data.IDataParameter[] objReqCheckDetailArr = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(3, out objReqCheckDetailArr);

                objReqCheckDetailArr[0].Value = p_objReqCheckDetail_VO.m_strGroupID;
                objReqCheckDetailArr[1].Value = p_objReqCheckDetail_VO.m_strCheck_Item_ID;
                objReqCheckDetailArr[2].Value = p_objReqCheckDetail_VO.m_strSample_ID;

                long lngRecEff = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddNewReqCheckDetail, ref lngRecEff, objReqCheckDetailArr);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion

            #region ���ݸ�������ϲ�ѯ�õ������б� ���� 2004.05.10
        /// <summary>
        /// ���ݸ�������ϲ�ѯ�õ������б�
        /// ���� 2004.05.10
        /// </summary>
        /// <param name="p_objPrincipal">Ȩ�޶���</param>
        /// <param name="p_intDevice">
        /// 0: �ֹ�������, 
        /// 1������, 
        /// 2���ֹ�
        /// </param>
        /// <param name="p_strDeviceID">��Ϊ��</param>
        /// <param name="p_intAuditing">
        /// 0������˺�δ���, 
        /// 1��δ���, 
        /// 2�������
        /// </param>
        /// <param name="p_strCheckDate_Begin">��Ϊ��</param>
        /// <param name="p_strCheckDate_End">��Ϊ��</param>
        /// <param name="p_dtbSampleList">���ؽ������
        /// sample_id_chr, 
        /// barcode_vchr, 
        /// groupid_chr, 
        /// check_date_dat, 
        /// status_int, 
        /// application_form_no_chr, 
        /// patient_name_vchr, 
        /// sex_chr, 
        /// age_chr, 
        /// diagnose_vchr, 
        /// appl_empid_chr, 
        /// appl_deptid_chr
        /// </param>
        /// <returns>>0 ʱ��Ч</returns>
        [AutoComplete]
        public long m_lngGetSampleList( int p_intDevice, string p_strDeviceID, int p_intAuditing, string p_strCheckDate_Begin, string p_strCheckDate_End, out DataTable p_dtbSampleList)
        {
            p_dtbSampleList = null;
            long lngRes = 0;
            com.digitalwave.security.clsPrivilegeHandleService objPrivilege = new com.digitalwave.security.clsPrivilegeHandleService();
            lngRes = objPrivilege.m_lngCheckCallPrivilege( "com.digitalwave.iCare.middletier.LIS.clsLisDeviceSvc", "m_lngGetSampleList");
            if (lngRes > 0)
            {
                return -1;
            }

            string strSQL1 = @"SELECT t1.sample_id_chr, t1.barcode_vchr, t1.check_date_dat, t1.status_int, 
									 t1.sex_chr,t1.patient_name_vchr,t1.age_chr,t1.diagnose_vchr,t1.appl_empid_chr,t1.appl_deptid_chr,
									 t1.application_form_no_chr,
									 t2.groupid_chr 
								FROM t_opr_lis_sample t1,
								     t_opr_lis_req_check t2 ";
            string strSQL2 = @" , t_opr_lis_device_relation t3 ";
            string strSQL3 = @"WHERE t1.sample_id_chr = t2.sample_id_chr ";
            string strSQL4 = @"	AND t1.sample_id_chr = t3.sample_id_chr 
								AND t3.status_int = 1 
								AND t3.deviceid_chr = '" + p_strDeviceID + "' ";
            string strSQL5 = @"	AND (t1.status_int = 5 OR t1.status_int = 6) ";
            string strSQL51 = @"	AND (t1.status_int = 5 ) ";
            string strSQL52 = @"	AND (t1.status_int = 6) ";
            string strSQL6 = @"	AND (t1.samplekind_chr = 1 or t1.samplekind_chr = 2) ";
            string strSQL61 = @"	AND (t1.samplekind_chr = 1) ";
            string strSQL62 = @"	AND (t1.samplekind_chr = 2) ";
            string strSQL7 = @"	AND t1.check_date_dat >= to_date('" + p_strCheckDate_Begin + @"','yyyy/mm/dd hh24:mi:ss') 
								AND t1.check_date_dat <= to_date('" + p_strCheckDate_End + @"','yyyy/mm/dd hh24:mi:ss') 
								";
            #region ����ִ�

            string strSQL = strSQL1;
            if (p_intDevice == 1 && p_strDeviceID != null)
            {
                strSQL = strSQL + strSQL2;
            }
            strSQL = strSQL + strSQL3;
            if (p_intDevice == 1 && p_strDeviceID != null)
            {
                strSQL = strSQL + strSQL4;
            }
            if (p_intAuditing == 0)
            {
                strSQL = strSQL + strSQL5;
            }
            else if (p_intAuditing == 1)
            {
                strSQL = strSQL + strSQL51;
            }
            else if (p_intAuditing == 2)
            {
                strSQL = strSQL + strSQL52;
            }
            if (p_intDevice == 0)
            {
                strSQL = strSQL + strSQL6;
            }
            else if (p_intDevice == 1)
            {
                strSQL = strSQL + strSQL61;
            }
            else if (p_intDevice == 2)
            {
                strSQL = strSQL + strSQL62;
            }
            if (p_strCheckDate_Begin != null && p_strCheckDate_End != null)
            {
                strSQL = strSQL + strSQL7;
            }

            #endregion


            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbSampleList);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

            #region �������뵥�źͼ�����Ų�ѯĳһ�������µ�������״̬(stepflag_chr�ֶ�������) ͯ�� 2004.04.23
        [AutoComplete]
        public long m_lngQrySampleStatusByApplFormNoANDGroupID( string strGroupID, string strApplFromNo, out DataTable dtbSampleStatus)
        {
            long lngRes = 0;
            dtbSampleStatus = null;
            string strSQL = @"SELECT groupid_chr,stepflag_chr
								FROM t_opr_lis_req_check
							   WHERE sample_id_chr IN (
														SELECT sample_id_chr
														  FROM t_opr_lis_applgrpsmp
														 WHERE groupid_chr = '" + strGroupID + @"'
														   AND application_form_no_chr = '" + strApplFromNo + "')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbSampleStatus);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

            #region �������뵥�źͼ����������ĳһ�������µ�������״̬(stepflag_chr�ֶ�������) ͯ�� 2004.04.23
        [AutoComplete]
        public long m_lngSetSampleStatusByApplFormNoANDGroupID( string strApplFormNo, string strGroupID, string strFlag)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE t_opr_lis_req_check
								 SET stepflag_chr = '" + strFlag + @"'
							   WHERE sample_id_chr IN (
													   SELECT sample_id_chr
														 FROM t_opr_lis_applgrpsmp
														WHERE groupid_chr = '" + strGroupID + @"'
														  AND application_form_no_chr = '" + strApplFormNo + @"')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣��
            }
            return lngRes;
        }
        #endregion

            #region ����SampleID,GroupID���ñ�t_opr_lis_req_check�ֶ�stepflag_chr��״̬ л�ɺ� 2004.2.28
        [AutoComplete]
        public long m_lngSetReqCheckStepFlag( string p_strSampleID, string p_strGroupID, int p_intStepFlag)
        {
            long lngRes = 0;
            string strSQL = @" UPDATE t_opr_lis_req_check SET stepflag_chr = '" + p_intStepFlag.ToString() + @"'
							WHERE sample_id_chr = '" + p_strSampleID + "' and GROUPID_CHR='" + p_strGroupID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion


        #region ����SampleID,GroupID��ѯ��t_opr_lis_req_check л�ɺ� 2004.3.3
        [AutoComplete]
        public long m_lngQuerySampleStepFlag( string p_strSampleID, string p_strGroupID, out System.Data.DataTable p_dtbReqCheck)
        {
            long lngRes = 0;
            p_dtbReqCheck = null;
            string strSQL = @"select stepflag_chr
							from t_opr_lis_req_check where GROUPID_CHR='" + p_strGroupID + "' and SAMPLE_ID_CHR='" + p_strSampleID + @"' and stepflag_chr>0";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbReqCheck);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//Ҫ��LogError�������׳��쳣�� 
            }
            return lngRes;
        }
        #endregion
         * 
         * */
        #endregion


        #region �޸Ĳ�����Ա
        /// <summary>
        /// �޸Ĳ�����Ա
        /// </summary>
        /// <param name="p_objPrincil"></param>
        /// <param name="p_strEmpId"></param>
        /// <param name="p_strSampleId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertCollector( string p_strEmpId, string p_strSampleId, string p_strApplicationID)
        {
            long lngRes = 0; 
            clsHRPTableService objHRPSvc = null;
            DateTime dtcurrentTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                string strSQL = @"update t_opr_lis_sample t
                                   set t.collector_id_chr = ?, t.sampling_date_dat = ?
                                 where t.sample_id_chr = ?
                                   and t.status_int > 0
                                                                ";
                IDataParameter[] objDpArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(3, out objDpArr);
                objDpArr[0].Value = p_strEmpId;
                objDpArr[1].DbType = DbType.DateTime;
                objDpArr[1].Value = dtcurrentTime;
                objDpArr[2].Value = p_strSampleId;
                long lngEff = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDpArr);
                if (lngRes > 0)
                {
                    lngRes = m_lngUpdateApplPrint( p_strApplicationID, true);
                }
                objDpArr = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnEx = objLogger.LogError(objEx);
                throw objEx;
            }
            finally
            { 
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region ͨ�����뵥�Ÿ���t_opr_lis_application���ڵĴ�ӡ״̬
        /// <summary>
        /// ͨ�����뵥�Ÿ���t_opr_lis_application���ڵĴ�ӡ״̬
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strAppliID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateApplPrint( string p_strAppliID, bool blnPrint)
        {
            long lngRes = 0; 
            clsHRPTableService objHRPSvc = null;
            try
            {
                DateTime dtCunrentDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string strSQL = null;
                if (blnPrint)
                {
                    strSQL = @"update t_opr_lis_application t
                                   set t.printed_num = 1, t.printed_date = ?
                                 where t.application_id_chr = ?
                                   and t.pstatus_int = 2";
                }
                else
                {
                    strSQL = @"update t_opr_lis_application t
                                   set t.printed_num = 0, t.printed_date = ?
                                 where t.application_id_chr = ?
                                   and t.pstatus_int = 2
                                ";
                }
                IDataParameter[] objDPArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = dtCunrentDate;
                objDPArr[1].Value = p_strAppliID;
                long lngEff = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnEx = objLogger.LogError(objEx);
                throw objEx;
            }
            finally
            { 
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region �޸Ĳ���ʱ��
        /// <summary>
        /// �޸Ĳ���ʱ��
        /// </summary>
        /// <param name="lstBarCode"></param>
        /// <param name="collTime"></param>
        /// <returns></returns>
        [AutoComplete]
        public int UpdateCollectorTime(List<string> lstBarCode, DateTime collTime)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = null;
            try
            {
                string Sql = @"update t_opr_lis_sample t 
                                  set t.sampling_date_dat = ?
                                where t.barcode_vchr in ({0}) 
                                  and t.status_int >= 0 ";

                string barCode = string.Empty;
                foreach (string code in lstBarCode)
                {
                    barCode += "'" + code + "',";
                }
                Sql = string.Format(Sql, barCode.TrimEnd(','));
                IDataParameter[] objDpArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out objDpArr);
                objDpArr[0].DbType = DbType.DateTime;
                objDpArr[0].Value = collTime;
                objHRPSvc.lngExecuteParameterSQL(Sql, ref lngRes, objDpArr);
                objDpArr = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnEx = objLogger.LogError(objEx);
                throw objEx;
            }
            finally
            {
                objHRPSvc = null;
            }
            return (int)lngRes;
        }
        #endregion


    }
    #region ����IDataParameter ����ĸ�����
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsIDataParameterCreator
    {
        [AutoComplete]
        public static System.Data.IDataParameter[] m_objConstructIDataParameterArr(params object[] p_objParamArr)
        {
            if (p_objParamArr.Length == 0)
                return null;

            int intLength = p_objParamArr.Length;
            System.Data.IDataParameter[] objReqCheckDetailArr = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.CreateDatabaseParameter(intLength, out objReqCheckDetailArr);
            objHRPSvc.Dispose();

            for (int i = 0; i < intLength; i++)
            {
                objReqCheckDetailArr[i].Value = p_objParamArr[i];
            }

            return objReqCheckDetailArr;
        }

    }
    #endregion
}
