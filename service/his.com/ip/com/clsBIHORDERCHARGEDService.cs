using System;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using System.Collections;

namespace com.digitalwave.iCare.middletier.BIHOrderServer
{
    /// <summary>
    /// 
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBIHORDERCHARGEDService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {



        #region �����շ���Ŀ�� סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// <summary>
        ///�����շ���Ŀ�� סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// </summary>
        /// <param name="objCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddORDERCHARGEDEPT(clsORDERCHARGEDEPT_VO objCharge)
        {
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        insert into T_OPR_BIH_ORDERCHARGEDEPT(                 
                        SEQ_INT,     ORDERID_CHR,   ORDERDICID_CHR,    CHARGEITEMID_CHR,
                        CLACAREA_CHR,CREATEAREA_CHR,CHARGEITEMNAME_CHR,SPEC_VCHR,
                        UNIT_VCHR,   AMOUNT_DEC,    UNITPRICE_DEC,     CREATORID_CHR,
                        CREATOR_VCHR,CREATEDATE_DAT,FLAG_INT,REMARK 
                        ,INSURACEDESC_VCHR,CONTINUEUSETYPE_INT,SINGLEAMOUNT_DEC,POFLAG_INT)
                        values
                        (
                        SEQ_PUBLIC.NEXTVAL,?,?,?,
                        ?,?,?,?,
                        ?,?,?,?,
                        ?,?,?,?
                        ,?,?,?,0
                        )
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(18, out arrParams);
            n++; arrParams[n].Value = objCharge.m_strOrderid_chr;
            n++; arrParams[n].Value = objCharge.m_strOrderdicid_chr;
            n++; arrParams[n].Value = objCharge.m_strChargeitemid_chr;

            n++; arrParams[n].Value = objCharge.m_strClacarea_chr;
            n++; arrParams[n].Value = objCharge.m_strCreatearea_chr;
            n++; arrParams[n].Value = objCharge.m_strChargeitemname_chr;
            n++; arrParams[n].Value = objCharge.m_strSpec_vchr;

            n++; arrParams[n].Value = objCharge.m_strUnit_vchr;
            n++; arrParams[n].Value = objCharge.m_decAmount_dec;
            n++; arrParams[n].Value = objCharge.m_decUnitprice_dec;
            n++; arrParams[n].Value = objCharge.m_strCreatorid_chr;

            n++; arrParams[n].Value = objCharge.m_strCreator_vchr;
            n++; arrParams[n].Value = objCharge.m_strCreatedate_dat;
            n++; arrParams[n].Value = 3;//FLAG_INT ������־:�շ�����Դ��  0-����Ŀ 1-������Ŀ 2-�÷����� 3-����¼����Ŀ
            n++; arrParams[n].Value = objCharge.REMARK;
            n++; arrParams[n].Value = objCharge.m_strINSURACEDESC_VCHR;
            n++; arrParams[n].Value = objCharge.m_intCONTINUEUSETYPE_INT;
            n++; arrParams[n].Value = objCharge.m_decSINGLEAMOUNT_DEC;
            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
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

        #region �޸��շ���Ŀ�� סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// <summary>
        ///�޸��շ���Ŀ�� סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// </summary>
        /// <param name="objCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangeORDERCHARGEDEPT(clsORDERCHARGEDEPT_VO objCharge)
        {
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        update T_OPR_BIH_ORDERCHARGEDEPT                  
                        set   
                        CHARGEITEMID_CHR=?,CLACAREA_CHR=?,CREATEAREA_CHR=?,CHARGEITEMNAME_CHR=?,
                       SPEC_VCHR=?,UNIT_VCHR=?,   AMOUNT_DEC=?,    UNITPRICE_DEC=?,
                       REMARK=?,INSURACEDESC_VCHR=?,CONTINUEUSETYPE_INT=?,SINGLEAMOUNT_DEC=?
                        where
                        SEQ_INT=?
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(13, out arrParams);
            n++; arrParams[n].Value = objCharge.m_strChargeitemid_chr;
            n++; arrParams[n].Value = objCharge.m_strClacarea_chr;
            n++; arrParams[n].Value = objCharge.m_strCreatearea_chr;
            n++; arrParams[n].Value = objCharge.m_strChargeitemname_chr;

            n++; arrParams[n].Value = objCharge.m_strSpec_vchr;
            n++; arrParams[n].Value = objCharge.m_strUnit_vchr;
            n++; arrParams[n].Value = objCharge.m_decAmount_dec;
            n++; arrParams[n].Value = objCharge.m_decUnitprice_dec;

            n++; arrParams[n].Value = objCharge.REMARK;
            n++; arrParams[n].Value = objCharge.m_strINSURACEDESC_VCHR;
            n++; arrParams[n].Value = objCharge.m_intCONTINUEUSETYPE_INT;
            n++; arrParams[n].Value = objCharge.m_decSINGLEAMOUNT_DEC;
            n++; arrParams[n].Value = objCharge.m_strSeq_int;

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
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

        #region ɾ���շ���Ŀ סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// <summary>
        ///ɾ���շ���Ŀ סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// </summary>
        /// <param name="objCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDellORDERCHARGEDEPT(string m_strSeq_int)
        {
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        delete T_OPR_BIH_ORDERCHARGEDEPT                  
                        where
                        SEQ_INT=?
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(1, out arrParams);
            n++; arrParams[n].Value = m_strSeq_int;

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
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

        #region ��ѯ�շ���Ŀ סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// <summary>
        /// ��ѯ�շ���Ŀ סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// </summary>
        /// <param name="orderid_chr">ҽ��ID</param>
        /// <param name="objCharge">סԺ������Ŀ�շ���Ŀִ�пͻ���VO</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetORDERCHARGEDEPT(string orderid_chr, out clsORDERCHARGEDEPT_VO objCharge)
        {

            long lngRes = 0;
            objCharge = new clsORDERCHARGEDEPT_VO();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        select 
                         SEQ_INT,     ORDERID_CHR,   ORDERDICID_CHR,  CLACAREA_CHR,
                        CREATEAREA_CHR,CHARGEITEMNAME_CHR,SPEC_VCHR,  UNIT_VCHR,  
                         AMOUNT_DEC,    UNITPRICE_DEC,     CREATORID_CHR,CREATOR_VCHR,
                         CREATEDATE_DAT
                        from T_OPR_BIH_ORDERCHARGEDEPT                  
                        where
                        orderid_chr=?
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(1, out arrParams);
            n++; arrParams[n].Value = objCharge.m_strSeq_int;
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, arrParams);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {

                    objCharge.m_strSeq_int = clsConverter.ToString(dtbResult.Rows[0]["SEQ_INT"].ToString());
                    objCharge.m_strOrderid_chr = clsConverter.ToString(dtbResult.Rows[0]["ORDERID_CHR"].ToString());
                    objCharge.m_strOrderdicid_chr = clsConverter.ToString(dtbResult.Rows[0]["ORDERDICID_CHR"].ToString());
                    objCharge.m_strClacarea_chr = clsConverter.ToString(dtbResult.Rows[0]["CLACAREA_CHR"].ToString());

                    objCharge.m_strCreatearea_chr = clsConverter.ToString(dtbResult.Rows[0]["CREATEAREA_CHR"].ToString());
                    objCharge.m_strChargeitemname_chr = clsConverter.ToString(dtbResult.Rows[0]["CHARGEITEMNAME_CHR"].ToString());
                    objCharge.m_strSpec_vchr = clsConverter.ToString(dtbResult.Rows[0]["SPEC_VCHR"].ToString());
                    objCharge.m_strUnit_vchr = clsConverter.ToString(dtbResult.Rows[0]["UNIT_VCHR"].ToString());

                    objCharge.m_decAmount_dec = clsConverter.ToDecimal(dtbResult.Rows[0]["AMOUNT_DEC"].ToString());
                    objCharge.m_decUnitprice_dec = clsConverter.ToDecimal(dtbResult.Rows[0]["UNITPRICE_DEC"].ToString());
                    objCharge.m_strCreatorid_chr = clsConverter.ToString(dtbResult.Rows[0]["CREATORID_CHR"].ToString());
                    objCharge.m_strCreator_vchr = clsConverter.ToString(dtbResult.Rows[0]["CREATOR_VCHR"].ToString());

                    objCharge.m_strCreatedate_dat = clsConverter.ToDateTime(dtbResult.Rows[0]["CREATEDATE_DAT"].ToString());
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

        #region �޸Ĳ��˷������ޣ�ҽ��������棩
        /// <summary>
        ///�޸��շ���Ŀ�� סԺ������Ŀ�շ���Ŀִ�пͻ���
        /// </summary>
        /// <param name="objCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateLIMITRATE(clsBIHPatientInfo objPatient)
        {
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        update T_Opr_BIH_Register                  
                        set   
                        LIMITRATE_MNY=?
                        where
                        REGISTERID_CHR=?
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(2, out arrParams);
            n++; arrParams[n].Value = objPatient.m_dblLIMITRATE_MNY;
            n++; arrParams[n].Value = objPatient.m_strRegisterID;

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
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

        #region �޸�ҽ������ʱ�䣨ҽ��������棩
        /// <summary>
        ///�޸�ҽ������ʱ�䣨ҽ��������棩
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderBeginDate(clsBIHOrder order)
        {
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        update t_opr_bih_order                  
                        set   
                        PostDate_Dat = sysdate
                        where
                        REGISTERID_CHR=? and RECIPENO_INT=?
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(2, out arrParams);
            //n++; arrParams[n].Value = order.m_dtPostdate;
            n++; arrParams[n].Value = order.m_strRegisterID;
            n++; arrParams[n].Value = order.m_intRecipenNo;

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
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

        #region �޸�ҽ������ʱ�䣨ҽ���������-������
        /// <summary>
        ///�޸�ҽ������ʱ�䣨ҽ���������-������
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderBeginDate(clsBIHOrder[] m_arrOrder)
        {
            long lngRes = 0;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";

            ArrayList OrderATTACHList = new ArrayList();

            try
            {
                int n = 0;

                strSQL = @"
                    update t_opr_bih_order                  
                        set   
                        PostDate_Dat=?,
                        STARTDATE_DAT=?
                        where
                        STATUS_INT in (0,1,5,7) and
                        REGISTERID_CHR=? and RECIPENO_INT=?
                    ";
                //m_lngUpdateOrderBooking(m_arrOrderBooking[i]);
                DbType[] dbTypes = new DbType[] {

                        DbType.Date,DbType.Date,DbType.String,DbType.Int32
                        };
                object[][] objValues = new object[4][];
                if (m_arrOrder.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[m_arrOrder.Length];//��ʼ��
                    }

                    for (int k1 = 0; k1 < m_arrOrder.Length; k1++)
                    {
                        n = -1;

                        objValues[++n][k1] = m_arrOrder[k1].m_dtPostdate;
                        objValues[++n][k1] = m_arrOrder[k1].m_dtPostdate;
                        objValues[++n][k1] = m_arrOrder[k1].m_strRegisterID;
                        objValues[++n][k1] = m_arrOrder[k1].m_intRecipenNo;


                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

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

        #region �޸�ҽ������ʱ�䣨ҽ��������棩
        /// <summary>
        ///�޸�ҽ������ʱ�䣨ҽ��������棩
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderEndDate(clsBIHOrder order)
        {
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        update t_opr_bih_order                  
                        set   
                        FINISHDATE_DAT=?
                        where
                        REGISTERID_CHR=? and RECIPENO_INT=?
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(3, out arrParams);
            n++; arrParams[n].Value = order.m_dtFinishDate;
            n++; arrParams[n].Value = order.m_strRegisterID;
            n++; arrParams[n].Value = order.m_intRecipenNo;

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
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

        #region �޸�ҽ������ʱ�䣨ҽ���������-������
        /// <summary>
        ///�޸�ҽ������ʱ�䣨ҽ���������-������
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderEndDate(clsBIHOrder[] m_arrOrder)
        {
            long lngRes = 0;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";

            ArrayList OrderATTACHList = new ArrayList();

            try
            {
                int n = 0;

                strSQL = @"
                     update t_opr_bih_order                  
                        set   
                        FINISHDATE_DAT=?
                        where
                        REGISTERID_CHR=? and RECIPENO_INT=?
                    ";
                //m_lngUpdateOrderBooking(m_arrOrderBooking[i]);
                DbType[] dbTypes = new DbType[] {

                        DbType.Date,DbType.String,DbType.Int32
                        };
                object[][] objValues = new object[3][];
                if (m_arrOrder.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[m_arrOrder.Length];//��ʼ��
                    }

                    for (int k1 = 0; k1 < m_arrOrder.Length; k1++)
                    {
                        n = -1;

                        objValues[++n][k1] = m_arrOrder[k1].m_dtFinishDate;
                        objValues[++n][k1] = m_arrOrder[k1].m_strRegisterID;
                        objValues[++n][k1] = m_arrOrder[k1].m_intRecipenNo;


                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

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

        #region �޸�ҽ�����δ�����ҽ��������棩
        /// <summary>
        ///�޸�ҽ�����δ���
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderATTACHTIMES(clsBIHOrder order)
        {

            if (order.m_intStatus != 0)
            {
                System.Collections.Generic.List<string> arrOrderID = new System.Collections.Generic.List<string>();
                arrOrderID.Add(order.m_strOrderID);

                int[] intType = new int[] { 0, 1 };
                System.Collections.Generic.List<int> TypeList = new System.Collections.Generic.List<int>(intType);
                bool blnExist = false;
                long l = this.m_lngCheckOrderStatus(arrOrderID, TypeList, ref blnExist);
                if (l < 0 || blnExist == false)
                {
                    return -10;
                }
            }
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        update t_opr_bih_order                  
                        set   
                        ATTACHTIMES_INT=?
                        where
                        REGISTERID_CHR=?
                        and RECIPENO_INT=?
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(3, out arrParams);
            n++; arrParams[n].Value = order.m_intATTACHTIMES_INT;
            n++; arrParams[n].Value = order.m_strRegisterID;
            n++; arrParams[n].Value = order.m_intRecipenNo;
            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
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

        #region �޸�ҽ�����δ�����ҽ���������--���޸ģ�
        /// <summary>
        ///�޸�ҽ�����δ���
        /// </summary>
        /// <param name="m_arrOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderATTACHTIMES(clsBIHOrder[] m_arrOrder)
        {
            long lngRes = 0;
            System.Collections.Generic.List<string> OrderATTACHList = new System.Collections.Generic.List<string>();
            for (int k = 0; k < m_arrOrder.Length; k++)
            {
                if (m_arrOrder[k].m_intStatus != 0)
                {
                    OrderATTACHList.Add(m_arrOrder[k].m_strOrderID);
                }
            }
            if (OrderATTACHList.Count > 0)
            {
                int[] intType = new int[] { 0, 1 };
                System.Collections.Generic.List<int> TypeList = new System.Collections.Generic.List<int>(intType);
                bool blnExist = false;
                lngRes = this.m_lngCheckOrderStatus(OrderATTACHList, TypeList, ref blnExist);
                if (lngRes < 0 || blnExist == false)
                {
                    return -10;
                }
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";

            try
            {
                int n = 0;

                strSQL = @"
                     update t_opr_bih_order                  
                        set   
                        ATTACHTIMES_INT=?
                        where
                        REGISTERID_CHR=?
                        and RECIPENO_INT=?
                    ";
                //m_lngUpdateOrderBooking(m_arrOrderBooking[i]);
                DbType[] dbTypes = new DbType[] {

                        DbType.Int32,DbType.String,DbType.Int32
                        };
                object[][] objValues = new object[3][];
                if (m_arrOrder.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[m_arrOrder.Length];//��ʼ��
                    }

                    for (int k1 = 0; k1 < m_arrOrder.Length; k1++)
                    {
                        n = -1;

                        objValues[++n][k1] = m_arrOrder[k1].m_intATTACHTIMES_INT;
                        objValues[++n][k1] = m_arrOrder[k1].m_strRegisterID;
                        objValues[++n][k1] = m_arrOrder[k1].m_intRecipenNo;


                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

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

        #region �޸�ҽ��������ҽ���������--���޸ģ�
        /// <summary>
        ///�޸�ҽ��ҽ������
        /// </summary>
        /// <param name="m_arrOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderAmount(clsBIHOrder[] m_arrOrder)
        {
            long lngRes = 0;
            System.Collections.Generic.List<string> OrderATTACHList = new System.Collections.Generic.List<string>();
            for (int k = 0; k < m_arrOrder.Length; k++)
            {
                if (m_arrOrder[k].m_intStatus != 0)
                {
                    OrderATTACHList.Add(m_arrOrder[k].m_strOrderID);
                }
            }
            if (OrderATTACHList.Count > 0)
            {
                int[] intType = new int[] { 0, 1, 5 };//0�¿� 1 �ύ 2 ת��
                System.Collections.Generic.List<int> TypeList = new System.Collections.Generic.List<int>(intType);
                bool blnExist = false;
                lngRes = this.m_lngCheckOrderStatus(OrderATTACHList, TypeList, ref blnExist);
                if (lngRes < 1 || blnExist == false)
                {
                    return -10;
                }
            }
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = "";

            try
            {
                int n = 0;

                strSQL = @"
                     update t_opr_bih_order                  
                        set   
                        GET_DEC=?
                        where
                        REGISTERID_CHR=?
                        and RECIPENO_INT=?
                    ";
                //m_lngUpdateOrderBooking(m_arrOrderBooking[i]);
                DbType[] dbTypes = new DbType[] {

                        DbType.Decimal,DbType.String,DbType.Int32
                        };
                object[][] objValues = new object[3][];
                if (m_arrOrder.Length > 0)
                {
                    for (int j = 0; j < objValues.Length; j++)
                    {
                        objValues[j] = new object[m_arrOrder.Length];//��ʼ��
                    }

                    for (int k1 = 0; k1 < m_arrOrder.Length; k1++)
                    {
                        n = -1;

                        objValues[++n][k1] = m_arrOrder[k1].m_dmlGet;
                        objValues[++n][k1] = m_arrOrder[k1].m_strRegisterID;
                        objValues[++n][k1] = m_arrOrder[k1].m_intRecipenNo;
                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

                }

                strSQL = @"
                update T_OPR_BIH_ORDERCHARGEDEPT a 
                set a.amount_dec=(select b.get_dec from t_opr_bih_order b where b.orderid_chr=?) 
                where
                a.flag_int=0
                and
                ORDERID_CHR=?
                ";
                DbType[] dbTypes2 = new DbType[] {

                        DbType.String,DbType.String
                        };
                object[][] objValues2 = new object[2][];
                if (m_arrOrder.Length > 0)
                {
                    for (int j = 0; j < objValues2.Length; j++)
                    {
                        objValues2[j] = new object[m_arrOrder.Length];//��ʼ��
                    }

                    for (int k1 = 0; k1 < m_arrOrder.Length; k1++)
                    {
                        n = -1;

                        objValues2[++n][k1] = m_arrOrder[k1].m_strOrderID;
                        objValues2[++n][k1] = m_arrOrder[k1].m_strOrderID;

                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues2, dbTypes2);

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

        #region �޸�ҽ��˵������ST��ҽ��������棩
        /// <summary>
        ///�޸�ҽ��˵������ST��ҽ��������棩
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderREMARK_VCHR(clsBIHOrder order)
        {
            long lngAff = 0;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] arrParams = null;
            DataTable dtbResult = new DataTable();
            string strSQL = "";
            strSQL = @"
                        update t_opr_bih_order                  
                        set   
                        REMARK_VCHR=?
                        where
                        ORDERID_CHR=?
                        ";
            int n = -1;
            objHRPSvc.CreateDatabaseParameter(2, out arrParams);
            n++; arrParams[n].Value = order.m_strREMARK_VCHR;
            n++; arrParams[n].Value = order.m_strOrderID;

            try
            {
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAff, arrParams);
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

        #region  ��ѯҽ���Ƿ�����״̬
        /// <summary>
        /// ��ѯҽ���Ƿ�����״̬ //����ҽ���޸ķ��滹�з���������clsBIHOrderService���У��˷�����Copy��clsBIHOrderService�൱�С���Ҫ�޸ģ����ȵ����߶����޸�
        /// </summary>
        /// <param name="arrOrderID">OrderID����</param>
        /// <param name="ArrFlag">��Ҫ�����״̬</param>
        /// <param name="Exist"></param>
        /// <returns>���������ȫ�������Ƿ���false ȫ�������򷵻�True</returns>
        [AutoComplete]
        public long m_lngCheckOrderStatus(System.Collections.Generic.List<string> arrOrderID, System.Collections.Generic.List<int> ArrFlag, ref bool Exist)
        {
            long lngRes = -1;
            Exist = true;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                if (arrOrderID.Count == 1)
                {
                    sql = @"select a.status_int from t_opr_bih_order a where a.orderid_chr = ?";
                    IDataParameter[] param = null;
                    objHRPSvc.CreateDatabaseParameter(1, out param);
                    param[0].Value = arrOrderID[0] ;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dt, param);
                }
                else if (arrOrderID.Count > 0)
                {
                    sql = @"select a.status_int from t_opr_bih_order a where a.orderid_chr in ([arrOrderID])";
                    string strOrderid = "";
                    foreach (Object objTmp in arrOrderID)
                    {
                        strOrderid += "'" + arrOrderID + "',";
                    }
                    strOrderid = strOrderid.TrimEnd(',');
                    sql = sql.Replace("[arrOrderID]", strOrderid);

                    lngRes = objHRPSvc.DoGetDataTable(sql, ref dt);
                }
                objHRPSvc.Dispose();

                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    if (!ArrFlag.Contains(int.Parse(dt.Rows[i1][0].ToString())))
                    {
                        Exist = false;
                        return lngRes;
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

    }




}
