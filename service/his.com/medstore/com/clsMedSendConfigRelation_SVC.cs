using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҩ���������ù�ϵ
    /// Create by xgpeng 2006-02-15
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMedSendConfigRelation_SVC : clsMiddleTierBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsMedSendConfigRelation_SVC()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ���ҩ����Ϣ(����)   
        /// <summary>
        /// ���ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreInfo(out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = new DataTable();
            string strSQL = @"select e.medstoreid_chr,e.medstorename_vchr from t_bse_medstore e    where  e.medstoretype_int in (1, 3)";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtable);
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

        #region ����ҩ��ID��ȡҩ��������Ϣ   
        /// <summary>
        /// ����ҩ��ID��ȡҩ��������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_TypeID">����ID</param>
        /// <param name="flage">0-��ҩ���� ; 1-��ҩ����</param>
        /// <param name="p_objResArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedWindowInfo(string p_TypeID, int flage, out clsOPMedStoreWin_VO[] p_objResArr)
        {
            long lngRes = 0;
            p_objResArr = new clsOPMedStoreWin_VO[0];
            DataTable p_dtRes = new DataTable();
            string strSQL = @"select a.windowid_chr,a.windowname_vchr,a.medstoreid_chr,a.windowtype_int,a.workstatus_int,b.medstorename_vchr from T_BSE_MEDSTOREWIN a,T_bse_medStore b
            where a.medstoreid_chr=b.medstoreid_chr and a.windowtype_int=? and b.medstoreid_chr=? order by a.medstoreid_chr,a.windowname_vchr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = flage;
                paramArr[1].Value = p_TypeID.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtRes, paramArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && p_dtRes != null)
                {
                    int intRow = p_dtRes.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResArr = new clsOPMedStoreWin_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResArr[i] = new clsOPMedStoreWin_VO();
                            p_objResArr[i].m_strWindowID = p_dtRes.Rows[i]["windowid_chr"].ToString().Trim();
                            p_objResArr[i].m_strWindowName = p_dtRes.Rows[i]["windowname_vchr"].ToString().Trim();
                            p_objResArr[i].m_intWindowType = Convert.ToInt32(p_dtRes.Rows[i]["windowtype_int"].ToString().Trim());
                            p_objResArr[i].m_intWorkStatus = Convert.ToInt32(p_dtRes.Rows[i]["workstatus_int"].ToString().Trim());
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

        #region ������ҩ����ID��ȡ��ҩ����->��ҩ���� ��Ϣ   
        /// <summary>
        /// ������ҩ����ID��ȡ��ҩ����->��ҩ���� ��Ϣ 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_WinID">��ҩ����ID</param>
        /// <param name="p_objResArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedWinByID(string p_WinID, out clsMedSendConfig_VO[] p_objResArr)
        {
            long lngRes = 0;
            p_objResArr = new clsMedSendConfig_VO[0];
            DataTable p_dtRes = new DataTable();
            string strSQL = @" select c.seq_int,c.treatwinid_chr,c.givewinid_chr,c.order_int,d.windowname_vchr  
 from T_OPR_MEDSTOREWINRLT c ,T_BSE_MEDSTOREWIN d
 where c.givewinid_chr=d.windowid_chr(+) and c.treatwinid_chr='" + p_WinID.Trim() + "' order by  c.order_int";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtRes);
                objHRPSvc.Dispose();
                if (lngRes > 0 && p_dtRes != null)
                {
                    int intRow = p_dtRes.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResArr = new clsMedSendConfig_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResArr[i] = new clsMedSendConfig_VO();
                            p_objResArr[i].m_intSeq = Convert.ToInt32(p_dtRes.Rows[i]["seq_int"].ToString().Trim());
                            p_objResArr[i].m_TreatWinID_chr = p_dtRes.Rows[i]["treatwinid_chr"].ToString().Trim();
                            p_objResArr[i].m_GiveWinID_chr = p_dtRes.Rows[i]["givewinid_chr"].ToString().Trim();
                            p_objResArr[i].m_intOrder = Convert.ToInt32(p_dtRes.Rows[i]["order_int"].ToString().Trim());
                            p_objResArr[i].m_strGiveWinName = p_dtRes.Rows[i]["windowname_vchr"].ToString().Trim();
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

        #region ���� ��ҩ����->��ҩ���� ��ϵ
        /// <summary>
        /// ���� ��ҩ����->��ҩ���� ��ϵ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intSeq"></param>
        /// <param name="p_objWinArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddMedSendGiveRelation(out int p_intSeq, clsMedSendConfig_VO p_objWinArr)
        {
            long lngRes = 0;
            p_intSeq = 0;//��ˮ��

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_intSeq = Convert.ToInt32(objHRPSvc.m_strGetNewID("T_OPR_MEDSTOREWINRLT", "SEQ_INT", 6));

                string strSQL = @"INSERT INTO T_OPR_MEDSTOREWINRLT
										  (SEQ_INT, TREATWINID_CHR, GIVEWINID_CHR,
										   ORDER_INT
										  )
								  VALUES (?,?,?,?)";


                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                paramArr[0].Value = p_intSeq;
                paramArr[1].Value = p_objWinArr.m_TreatWinID_chr;
                paramArr[2].Value = p_objWinArr.m_GiveWinID_chr;
                paramArr[3].Value = p_objWinArr.m_intOrder;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);


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

        #region ɾ�� ��ҩ����->��ҩ���� ��ϵ
        /// <summary>
        /// ɾ��ҩ���Ű���Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_intID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_thDelMedSendGiveRelation(int p_intID)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                string strSQL = @"delete T_OPR_MEDSTOREWINRLT  where SEQ_INT=" + p_intID + "";
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

        #region ���� �����ƶ���¼
        /// <summary>
        /// ���� �����ƶ���¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objWinArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateMovRecord(clsMedSendConfig_VO[] p_objWinArr)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                for (int i1 = 0; i1 < p_objWinArr.Length; ++i1)
                {
                    string strSQL = @"update T_OPR_MEDSTOREWINRLT set ORDER_INT=? where SEQ_INT=? ";

                    System.Data.IDataParameter[] paramArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                    paramArr[0].Value = p_objWinArr[i1].m_intOrder;
                    paramArr[1].Value = p_objWinArr[i1].m_intSeq;
                    long lngRecordsAffected = -1;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
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
        #endregion

        #region ��ô��ڶ�����Ϣ������ҩ��ID�Լ��������ͣ� 2006-2-20
        /// <summary>
        /// ��ô��ڶ�����Ϣ������ҩ��ID�Լ��������ͣ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">ҩ��ID</param>
        /// <param name="flage">�������� 0-��ҩ���� 1-��ҩ����,</param>
        /// <param name="p_DataTableQueue"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetWinQueueByMedStoreID(string p_strID, int flage, DateTime p_dtimeBegin, DateTime p_dtimeEnd, out DataTable p_DataTableQueue)
        {
            long lngRes = 0;
            p_DataTableQueue = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                string strSQL;

                strSQL = @"SELECT distinct  g.seq_int, f.windowid_chr, f.windowname_vchr, g.lastname_vchr,
         g.sex_chr, g.order_int
    FROM 
      (  select  b.seq_int,b.windowid_chr,b.medstoreid_chr,b.order_int,t.lastname_vchr,t.sex_chr
      from t_opr_outpatientrecipeinv h, t_bse_patient t,t_opr_outpatientrecipe a,t_opr_medstorewinque b
      where  t.patientid_chr = a.patientid_chr and  h.outpatrecipeid_chr=b.outpatrecipeid_chr and b.outpatrecipeid_chr=a.outpatrecipeid_chr  and h.recorddate_dat between to_Date(?,'yyyy-MM-dd hh24:mi:ss') and to_Date(?,'yyyy-MM-dd hh24:mi:ss') ) g,(SELECT   e.windowid_chr, e.windowname_vchr,
                 CASE
                    WHEN e.windowname_vchr LIKE 'һ%'
                       THEN 1
                    WHEN e.windowname_vchr LIKE '��%'
                       THEN 2
                    WHEN e.windowname_vchr LIKE '��%'
                       THEN 3
                    WHEN e.windowname_vchr LIKE '��%'
                       THEN 4
                    WHEN e.windowname_vchr LIKE '��%'
                       THEN 5
                    WHEN e.windowname_vchr LIKE '��%'
                       THEN 6
                    WHEN e.windowname_vchr LIKE '��%'
                       THEN 7
                    WHEN e.windowname_vchr LIKE '��%'
                       THEN 8
                    WHEN e.windowname_vchr LIKE '��%'
                       THEN 9
                    WHEN e.windowname_vchr LIKE 'ʮ%'
                       THEN 10
                 END AS ord
            FROM  t_bse_medstorewin e 
           WHERE e.medstoreid_chr=? and e.windowtype_int=?) f  WHERE  g.windowid_chr(+) = f.windowid_chr ORDER BY f.ord, g.order_int";




                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                paramArr[0].Value = p_dtimeBegin.ToString("yyyy-MM-dd 00:00:00");
                paramArr[1].Value = p_dtimeEnd.ToString("yyyy-MM-dd 23:59:59");
                paramArr[2].Value = p_strID;
                paramArr[3].Value = flage;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_DataTableQueue, paramArr);

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

        #region  �ж��Ƿ�ɵ�����
        /// <summary>
        /// �ж��Ƿ�ɵ�����
        /// </summary>
        /// <param name="p_Seq">��ˮ��</param>
        /// <param name="p_WinStyle">����״̬��0-��ҩ���ڣ�1����ҩ����</param>
        /// <param name="p_Status">״̬ 1-�½� 2-����ҩ 3-�ѷ�ҩ -1-�˻�</param>
        /// <param name="p_objPrincipal"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_thJudgeIsOldData(int p_Seq, int p_WinStyle, out int p_Status)
        {
            long lngRes = 0;
            p_Status = -2;
            DataTable dtable = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                string strSQL = "";
                if (p_WinStyle == 1)
                    strSQL = @" select a.pstatus_int from t_opr_medRecipesend a,T_OPR_MEDSTOREWINQUE b
                            where b.seq_int=" + p_Seq + "and a.outpatrecipeid_chr=b.outpatrecipeid_chr and a.windowid_chr=b.windowid_chr";
                else if (p_WinStyle == 0)
                    strSQL = @" select a.pstatus_int from t_opr_medRecipesend a,T_OPR_MEDSTOREWINQUE b
                            where b.seq_int=" + p_Seq + "and a.outpatrecipeid_chr=b.outpatrecipeid_chr and a.sendemp_chr=b.windowid_chr";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtable);
                if (lngRes > 0 && dtable.Rows.Count > 0)
                {
                    p_Status = Convert.ToInt32(dtable.Rows[0][0].ToString());
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

        #endregion
        #region ���洹ֱ�϶���¼ 2006-2-22
        /// <summary>
        /// ���洹ֱ�϶���¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_Seq"></param>
        /// <param name="p_Order"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngVerichDropRecord(int p_Seq, int p_Order)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                string strSQL = @" update T_OPR_MEDSTOREWINQUE set  ORDER_INT=? where SEQ_INT=?";

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                paramArr[0].Value = p_Order;
                paramArr[1].Value = p_Seq;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
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

        #region ����ˮƽ��б���϶���¼ 2006-2-22
        /// <summary>
        /// ����ˮƽ��б���϶���¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_Seq"></param>
        /// <param name="p_WinID"></param>
        /// <param name="p_WinType"></param>
        /// <param name="p_Order"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngHorDropRecord(int p_Seq, string p_WinID, int p_WinType, int p_Order)
        {
            long lngRes = 0;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                string strSQL = @"  update T_OPR_MEDSTOREWINQUE a set  a.ORDER_INT=?,a.WINDOWID_CHR=?  where a.SEQ_INT=?";


                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = p_Order;
                paramArr[1].Value = p_WinID;
                paramArr[2].Value = p_Seq;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
                string p_strSQL = "";
                if (p_WinType == 0)
                {
                    p_strSQL = @"  update t_opr_medrecipesend b set  b.SENDWINDOWID=? where b.OUTPATRECIPEID_CHR in (select a.OUTPATRECIPEID_CHR from T_OPR_MEDSTOREWINQUE a where a.SEQ_INT=? )";
                    paramArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                    paramArr[0].Value = p_WinID;
                    paramArr[1].Value = p_Seq;
                    lngRecordsAffected = -1;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(p_strSQL, ref lngRecordsAffected, paramArr);

                }
                else if (p_WinType == 1)
                {
                    paramArr = null;
                    objHRPSvc.CreateDatabaseParameter(2, out paramArr);
                    paramArr[0].Value = p_WinID;
                    paramArr[1].Value = p_Seq;
                    lngRecordsAffected = -1;
                    p_strSQL = @"  update t_opr_medrecipesend b set  b.WINDOWID_CHR=? where b.OUTPATRECIPEID_CHR=(select a.outpatrecipeid_chr from T_OPR_MEDSTOREWINQUE a where a.seq_int=?)";
                    lngRes = objHRPSvc.lngExecuteParameterSQL(p_strSQL, ref lngRecordsAffected, paramArr);
                    p_strSQL = @"  update t_opr_outpatientpwmrecipede b set  b.WINDOWID_CHR=? where b.OUTPATRECIPEID_CHR=(select a.outpatrecipeid_chr from T_OPR_MEDSTOREWINQUE a where a.seq_int=?)";
                    lngRes = objHRPSvc.lngExecuteParameterSQL(p_strSQL, ref lngRecordsAffected, paramArr);
                    p_strSQL = @"  update T_OPR_OUTPATIENTCMRECIPEDE b set  b.WINDOWID_CHR=? where b.OUTPATRECIPEID_CHR=(select a.outpatrecipeid_chr from T_OPR_MEDSTOREWINQUE a where a.seq_int=?)";
                    lngRes = objHRPSvc.lngExecuteParameterSQL(p_strSQL, ref lngRecordsAffected, paramArr);

                    p_strSQL = @"  update T_OPR_OUTPATIENTCHKRECIPEDE b set  b.WINDOWID_CHR=? where b.OUTPATRECIPEID_CHR=(select a.outpatrecipeid_chr from T_OPR_MEDSTOREWINQUE a where a.seq_int=?)";
                    lngRes = objHRPSvc.lngExecuteParameterSQL(p_strSQL, ref lngRecordsAffected, paramArr);

                    p_strSQL = @"  update T_OPR_OUTPATIENTTESTRECIPEDE b set  b.WINDOWID_CHR=? where b.OUTPATRECIPEID_CHR=(select a.outpatrecipeid_chr from T_OPR_MEDSTOREWINQUE a where a.seq_int=?)";
                    lngRes = objHRPSvc.lngExecuteParameterSQL(p_strSQL, ref lngRecordsAffected, paramArr);

                    p_strSQL = @"  update T_OPR_OUTPATIENTOPSRECIPEDE b set  b.WINDOWID_CHR=? where b.OUTPATRECIPEID_CHR=(select a.outpatrecipeid_chr from T_OPR_MEDSTOREWINQUE a where a.seq_int=?)";
                    lngRes = objHRPSvc.lngExecuteParameterSQL(p_strSQL, ref lngRecordsAffected, paramArr);




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

        #endregion

    }
}
