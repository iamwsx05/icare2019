using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҩ���̵��м��
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsDrugStoreCheck_Supported_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region �����Ƶ�ʱ���ҩ��id��ȡҩ���̵�����
        /// <summary>
        /// �����Ƶ�ʱ���ҩ��id��ȡҩ���̵�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDrugStoreid"></param>
        /// <param name="m_datBeginTime"></param>
        /// <param name="m_datEndTime"></param>
        /// <param name="m_dtCheckMainInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDrugStoreCheckMainInfo( string m_strDrugStoreid, DateTime m_datBeginTime, DateTime m_datEndTime, out DataTable m_dtCheckMainInfo)
        {
            long lngRes = 0;
            m_dtCheckMainInfo = new DataTable(); 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.seriesid_int,
       a.drugstoreid_chr,
       a.checkid_chr,a.status_int as status,
       decode(a.status_int, 0, 'ɾ��', 1, '����', 2, '���', '����') as status_int,
       a.askdate_dat,
       a.examdate_dat,
       a.askerid_chr,
       b.lastname_vchr as askername,
       a.examerid_chr,
       c.lastname_vchr as examername,
       a.inaccountid_chr,
       d.lastname_vchr as inaccountname,
       a.inaccountdate_dat,
       a.checkdate_dat
  from t_ds_drugstorecheck a,
       t_bse_employee    b,
       t_bse_employee    c,
       t_bse_employee    d
 where a.askerid_chr = b.empid_chr(+)
   and a.examerid_chr = c.empid_chr(+)
   and a.inaccountid_chr = d.empid_chr(+)
   and a.drugstoreid_chr = ?
   and a.checkdate_dat between ? and ? and a.status_int <> 0 order by a.checkid_chr desc";

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = m_strDrugStoreid;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = m_datBeginTime;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = m_datEndTime;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckMainInfo, objDPArr);
                DataView dv = m_dtCheckMainInfo.DefaultView;
                dv.RowFilter = "status<> 0";
                m_dtCheckMainInfo = dv.ToTable();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region �������кŻ�ȡҩ���̵���ϸ����
        /// <summary>
        ///  �������кŻ�ȡҩ���̵���ϸ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strSerialID"></param>
        /// <param name="m_dtCheckDetailInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDrugStoreCheckDetailInfoById( string m_strSerialID,out DataTable m_dtCheckDetailInfo)
        {
            long lngRes = 0;
            m_dtCheckDetailInfo = new DataTable(); 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select b.seriesid_int,
       b.seriesid2_int,
       b.medicineid_chr,
       b.medicinename_vchr,
       b.medspec_vchr,
       b.unit_chr,
       b.lotno_vchr,
       b.validperiod_dat,
       b.currentgross_int,
       b.checkgross_int,
       b.productorid_chr,
       b.retailprice_int,
       b.callprice_int,
       b.wholesaleprice_int,
       b.checkreason_vchr,
       b.checkresult_int,
       b.iszero_int,
       b.modifier_chr,
       b.modifydate_dat,
       b.status_int,
       b.indrugstoreid_vchr,
       b.packqty_dec,
       b.opchargeflg_int,
       b.iprealgross_int,
       b.oprealgross_int,
       b.ipretailprice_int,
       b.opretailprice_int,
       b.ipcallprice_int,
       b.opcallprice_int,
       b.dsinstoragedate_dat,
       b.ipchargeflg_int,
       c.assistcode_chr,
       c.opunit_chr,
       c.ipunit_chr          
  from t_ds_drugstorecheck a, t_ds_drugstorecheck_detail b,t_bse_medicine c 
 where a.seriesid_int = b.seriesid2_int and b.medicineid_chr=c.medicineid_chr(+)
   and a.seriesid_int = ?
   ";

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = m_strSerialID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckDetailInfo, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
             #endregion

        #region ���ݲ�ѯ������ȡҩ���̵�����
        /// <summary>
        /// ���ݲ�ѯ������ȡҩ���̵�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strDrugStoreid"></param>
        /// <param name="m_datBeginTime"></param>
        /// <param name="m_datEndTime"></param>
        /// <param name="m_strCheckid"></param>
        /// <param name="m_strMakerid"></param>
        /// <param name="m_intStatus"></param>
        /// <param name="m_dtCheckMainInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDrugStoreCheckMainInfo( string m_strDrugStoreid, DateTime m_datBeginTime, DateTime m_datEndTime,string m_strCheckid,string m_strMakerid,Int16 m_intStatus, out DataTable m_dtCheckMainInfo)
        {
            long lngRes = 0;
            m_dtCheckMainInfo = new DataTable(); 
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.seriesid_int,
       a.drugstoreid_chr,
       a.checkid_chr,a.status_int as status,
       decode(a.status_int, 0, 'ɾ��', 1, '����', 2, '���', '����') as status_int,
       a.askdate_dat,
       a.examdate_dat,
       a.askerid_chr,
       b.lastname_vchr as askername,
       a.examerid_chr,
       c.lastname_vchr as examername,
       a.inaccountid_chr,
       d.lastname_vchr as inaccountname,
       a.inaccountdate_dat,
       a.checkdate_dat
  from t_ds_drugstorecheck a,
       t_bse_employee    b,
       t_bse_employee    c,
       t_bse_employee    d
 where a.askerid_chr = b.empid_chr(+)
   and a.examerid_chr = c.empid_chr(+)
   and a.inaccountid_chr = d.empid_chr(+)
   and a.drugstoreid_chr = ?
   and a.askerid_chr like ?
   and a.status_int = ?
   and a.checkid_chr like ?
   and a.checkdate_dat between ? and ?
   and a.status_int <> 0 
 order by a.checkid_chr desc";

            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = m_strDrugStoreid;
                objDPArr[1].Value = "%"+m_strMakerid+"%";
                objDPArr[2].Value = m_intStatus;
                objDPArr[3].Value = "%" + m_strCheckid + "%";
                objDPArr[4].Value = m_datBeginTime;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[5].Value = m_datEndTime;
                objDPArr[5].DbType = DbType.DateTime;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckMainInfo, objDPArr);
                DataView dv = m_dtCheckMainInfo.DefaultView;
                dv.RowFilter = "status<> 0";
                m_dtCheckMainInfo = dv.ToTable();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ȡ�̵�����
        /// <summary>
        /// ��ȡ�̵�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_dtmStarDate">��ʼʱ��</param>
        /// <param name="m_dtmEndDate">����ʱ��</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtbStorageCheck">�̵���������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageCheck( DateTime m_dtmStarDate, DateTime m_dtmEndDate, string p_strStorageID, out DataTable p_dtbStorageCheck)
        {
            p_dtbStorageCheck = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct  t.inaccountdate_dat,
                t.inaccountid_chr,
                t.examerid_chr,
                t.askerid_chr,
                t.examdate_dat,
                t.askdate_dat,
                t.status_int,
                t.checkid_chr,
                t.drugstoreid_chr,
                t.seriesid_int,
                to_char(t.checkdate_dat,'yyyy-mm-dd hh24:mi:ss') as checkdate_dat,
                case t.status_int
                  when 0 then
                   '����'
                  when 1 then
                   '����'
                  when 2 then
                   '���'
                  when 3 then
                   '����'
                end statusdesc,
                ea.lastname_vchr examername,
                eb.lastname_vchr askername
  from t_ds_drugstorecheck t
 inner join t_ds_drugstorecheck_detail de on t.seriesid_int =
                                           de.seriesid2_int
                                       and de.status_int = 1
  left outer join t_bse_employee ea on t.examerid_chr = ea.empid_chr
  left outer join t_bse_employee eb on t.askerid_chr = eb.empid_chr
 where t.status_int <> 0
   and t.checkdate_dat between ? and ?
   and t.drugstoreid_chr = ?
 order by t.checkid_chr desc";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = m_dtmStarDate;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = m_dtmEndDate;
                objDPArr[2].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbStorageCheck, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�̵���ϸ
        /// <summary>
        /// ��ȡ�̵���ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngMainSEQ">��������</param>
        /// <param name="p_intCheckMode">�̵�ģʽ��0ΪĬ�ϣ�1Ϊ��Ժ��ģʽ</param>
        /// <param name="p_blnIsHospital">�Ƿ�����ҩ��</param>
        /// <param name="p_dtbDetailTrue">δ�ϲ�����ϸ������</param>
        /// <param name="p_dtbStorageCheck_detail">�Ѻϲ���ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageCheck_detail( long p_lngMainSEQ,int p_intCheckMode,bool p_blnIsHospital,out DataTable p_dtbDetailTrue, out DataTable p_dtbStorageCheck_detail)
        {
            p_dtbDetailTrue = null;
            p_dtbStorageCheck_detail = null;
            long lngRes = 0;
            try
            {
                string strSQL = string.Empty;
                if (p_blnIsHospital)
                {
                    strSQL = @"select t.seriesid_int,
       t.seriesid2_int,
       t.medicineid_chr,
       t.medicinename_vchr,
       t.medspec_vchr,
       decode(t.ipchargeflg_int, 0, t.opunit_chr, t.ipunit_chr) unit_chr,
       t.opunit_chr,
       t.ipunit_chr,
       case
         when t.lotno_vchr = 'UNKNOWN' then
          ''
         else
          t.lotno_vchr
       end lotno_vchr,
       t.validperiod_dat,
       decode(t.ipchargeflg_int,
              0,
              t.opcheckgross_int +
              round(t.ipcheckgross_int / t.packqty_dec, 2),
              t.opcheckgross_int * t.packqty_dec + t.ipcheckgross_int) checkgross_int,
       t.opcheckgross_int,
       t.ipcheckgross_int,
       t.productorid_chr,
       decode(t.ipchargeflg_int,
              0,
              t.opretailprice_int,
              t.ipretailprice_int) retailprice_int,
       t.opretailprice_int,
       t.ipretailprice_int,
       decode(t.ipchargeflg_int, 0, t.opcallprice_int, t.ipcallprice_int) callprice_int,
       t.opcallprice_int,
       t.ipcallprice_int,
       t.checkreason_vchr,
       decode(t.ipchargeflg_int,
              0,
              t.opcheckresult_int +
              round(t.ipcheckresult_int / t.packqty_dec, 2),
              t.opcheckresult_int * t.packqty_dec + t.ipcheckresult_int) checkresult_int,
       t.opcheckresult_int,
       t.ipcheckresult_int,
       t.iszero_int,
       t.modifier_chr,
       t.modifydate_dat,
       t.status_int,
       t.indrugstoreid_vchr,
       t.packqty_dec,
       t.opchargeflg_int,
       decode(t.ipchargeflg_int,
              0,
              t.oprealgross_int + round(t.iprealgross_int / t.packqty_dec),
              t.oprealgross_int * t.packqty_dec + t.iprealgross_int) realgross_int,
       t.iprealgross_int,
       t.oprealgross_int,
       t.dsinstoragedate_dat,
       t.ipchargeflg_int,
       m.assistcode_chr,
       m.medicinetypeid_chr,
       c.medicinepreptype_chr,
       c.medicinepreptypename_vchr,
       h.medicinetypename_vchr,
       d.checkmedicineorder_chr,
       e.storagerackcode_vchr,
       round((t.opretailprice_int *
       (t.opcheckresult_int * t.packqty_dec + t.ipcheckresult_int) /
       t.packqty_dec),8) balance,
       round((t.opretailprice_int *
       (t.oprealgross_int * t.packqty_dec + t.iprealgross_int) /
       t.packqty_dec),8) retailmoney,
       round((t.opretailprice_int *
       (t.opcheckgross_int * t.packqty_dec + t.ipcheckgross_int) /
       t.packqty_dec),8) realmoney,
       round((t.opcallprice_int *
       (t.opcheckgross_int * t.packqty_dec + t.ipcheckgross_int) /
       t.packqty_dec),8) callmoney
  from t_ds_drugstorecheck_detail t
  left join t_ds_drugstorecheck f on t.seriesid2_int = f.seriesid_int
  left join t_bse_medicine m on t.medicineid_chr = m.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        m.medicinepreptype_chr
  left join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    m.medicinetypeid_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         t.medicineid_chr
                                     and f.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 where t.seriesid2_int = ?
   and f.status_int <> 0
   and t.status_int = 1";
                }
                else
                {
                    strSQL = @"select t.seriesid_int,
       t.seriesid2_int,
       t.medicineid_chr,
       t.medicinename_vchr,
       t.medspec_vchr,
       decode(t.opchargeflg_int, 0, t.opunit_chr, t.ipunit_chr) unit_chr,
       t.opunit_chr,
       t.ipunit_chr,
       case
         when t.lotno_vchr = 'UNKNOWN' then
          ''
         else
          t.lotno_vchr
       end lotno_vchr,
       t.validperiod_dat,
       decode(t.opchargeflg_int,
              0,
              t.opcheckgross_int +
              round(t.ipcheckgross_int / t.packqty_dec, 2),
              t.opcheckgross_int * t.packqty_dec + t.ipcheckgross_int) checkgross_int,
       t.opcheckgross_int,
       t.ipcheckgross_int,
       t.productorid_chr,
       decode(t.opchargeflg_int,
              0,
              t.opretailprice_int,
              t.ipretailprice_int) retailprice_int,
       t.opretailprice_int,
       t.ipretailprice_int,
       decode(t.opchargeflg_int, 0, t.opcallprice_int, t.ipcallprice_int) callprice_int,
       t.opcallprice_int,
       t.ipcallprice_int,
       t.checkreason_vchr,
       decode(t.opchargeflg_int,
              0,
              t.opcheckresult_int +
              round(t.ipcheckresult_int / t.packqty_dec, 2),
              t.opcheckresult_int * t.packqty_dec + t.ipcheckresult_int) checkresult_int,
       t.opcheckresult_int,
       t.ipcheckresult_int,
       t.iszero_int,
       t.modifier_chr,
       t.modifydate_dat,
       t.status_int,
       t.indrugstoreid_vchr,
       t.packqty_dec,
       t.opchargeflg_int,
       decode(t.opchargeflg_int,
              0,
              t.oprealgross_int + round(t.iprealgross_int / t.packqty_dec),
              t.oprealgross_int * t.packqty_dec + t.iprealgross_int) realgross_int,
       t.iprealgross_int,
       t.oprealgross_int,
       t.dsinstoragedate_dat,
       t.ipchargeflg_int,
       m.assistcode_chr,
       m.medicinetypeid_chr,
       c.medicinepreptype_chr,
       c.medicinepreptypename_vchr,
       h.medicinetypename_vchr,
       d.checkmedicineorder_chr,
       e.storagerackcode_vchr,
       round((t.opretailprice_int *
       (t.opcheckresult_int * t.packqty_dec + t.ipcheckresult_int) /
       t.packqty_dec),8) balance,
       round((t.opretailprice_int *
       (t.oprealgross_int * t.packqty_dec + t.iprealgross_int) /
       t.packqty_dec),8) retailmoney,
       round((t.opretailprice_int *
       (t.opcheckgross_int * t.packqty_dec + t.ipcheckgross_int) /
       t.packqty_dec),8) realmoney,
       round((t.opcallprice_int *
       (t.opcheckgross_int * t.packqty_dec + t.ipcheckgross_int) /
       t.packqty_dec),8) callmoney
  from t_ds_drugstorecheck_detail t
  left join t_ds_drugstorecheck f on t.seriesid2_int = f.seriesid_int
  left join t_bse_medicine m on t.medicineid_chr = m.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        m.medicinepreptype_chr
  left join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    m.medicinetypeid_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         t.medicineid_chr
                                     and f.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 where t.seriesid2_int = ?
   and f.status_int <> 0
   and t.status_int = 1";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngMainSEQ;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbDetailTrue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (p_dtbDetailTrue != null && p_dtbDetailTrue.Rows.Count > 0)
                {
                    p_dtbDetailTrue.DefaultView.Sort = "assistcode_chr";
                    p_dtbDetailTrue = p_dtbDetailTrue.DefaultView.ToTable();
                    if (p_intCheckMode == 0)
                    {
                        p_dtbStorageCheck_detail = p_dtbDetailTrue.Copy();
                    }
                    else
                    {
                        DataView dvResult = p_dtbDetailTrue.DefaultView;
                        dvResult.Sort = "seriesid_int";
                        p_dtbDetailTrue = dvResult.ToTable();

                        p_dtbStorageCheck_detail = p_dtbDetailTrue.Clone();
                        string strMedicineID = string.Empty;
                        DataRow dr = null;
                        DataRow drw = null;
                        double dblPackQty = 0d;
                        double dblOldOPGross = 0d;
                        double dblOldIPGross = 0d;

                        for (int i1 = 0; i1 < p_dtbDetailTrue.Rows.Count; i1++)
                        {
                            dr = p_dtbDetailTrue.Rows[i1];

                            if (p_dtbStorageCheck_detail.Rows.Count > 0)
                                drw = p_dtbStorageCheck_detail.Rows[p_dtbStorageCheck_detail.Rows.Count - 1];

                            dblPackQty = Convert.ToDouble(dr["packqty_dec"]);

                            

                            if (strMedicineID != dr["medicineid_chr"].ToString())
                            {
                                p_dtbStorageCheck_detail.Rows.Add(dr.ItemArray);
                                strMedicineID = dr["medicineid_chr"].ToString();
                                drw = p_dtbStorageCheck_detail.Rows[p_dtbStorageCheck_detail.Rows.Count - 1];
                                drw["lotno_vchr"] = "UNKNOWN";
                                drw["validperiod_dat"] = DBNull.Value;
                            }
                            else
                            {
                                dblOldOPGross = Convert.ToDouble(drw["oprealgross_int"]);
                                dblOldIPGross = Convert.ToDouble(drw["iprealgross_int"]);
                                drw["oprealgross_int"] = (int)((dblOldOPGross * dblPackQty + dblOldIPGross + Convert.ToDouble(dr["oprealgross_int"]) * dblPackQty + Convert.ToDouble(dr["iprealgross_int"])) / dblPackQty);
                                drw["iprealgross_int"] = (dblOldOPGross * dblPackQty + dblOldIPGross + Convert.ToDouble(dr["oprealgross_int"]) * dblPackQty + Convert.ToDouble(dr["iprealgross_int"])) % dblPackQty;
                                if (p_blnIsHospital)
                                {
                                    if (Convert.ToInt32(dr["ipchargeflg_int"]) == 0)
                                    {
                                        drw["realgross_int"] = Convert.ToDouble(drw["oprealgross_int"]) + Math.Round(Convert.ToDouble(drw["iprealgross_int"]) / dblPackQty, 2, MidpointRounding.AwayFromZero);                                        
                                    }
                                    else
                                    {
                                        drw["realgross_int"] = Convert.ToDouble(drw["iprealgross_int"]) + Convert.ToDouble(drw["oprealgross_int"]) * dblPackQty;
                                    }                                    
                                }
                                else
                                {
                                    if (Convert.ToInt32(dr["opchargeflg_int"]) == 0)
                                    {
                                        drw["realgross_int"] = Convert.ToDouble(drw["oprealgross_int"]) + Math.Round(Convert.ToDouble(drw["iprealgross_int"]) / dblPackQty, 2, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        drw["realgross_int"] = Convert.ToDouble(drw["iprealgross_int"]) + Convert.ToDouble(drw["oprealgross_int"]) * dblPackQty;
                                    }
                                }
                                
                                drw["opcheckgross_int"] = drw["oprealgross_int"];
                                drw["ipcheckgross_int"] = drw["iprealgross_int"];
                                drw["checkgross_int"] = drw["realgross_int"];

                                drw["balance"] = Convert.ToDouble(drw["balance"]) + Convert.ToDouble(dr["balance"]);
                                drw["retailmoney"] = Convert.ToDouble(drw["retailmoney"]) + Convert.ToDouble(dr["retailmoney"]);
                                drw["realmoney"] = Convert.ToDouble(drw["realmoney"]) + Convert.ToDouble(dr["realmoney"]);
                                drw["callmoney"] = Convert.ToDouble(drw["callmoney"]) + Convert.ToDouble(dr["callmoney"]);
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ���µ��̵��
        /// <summary>
        /// ���µ��̵��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strID">���ص��ݺ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetLatestCheckID( out string p_strID)
        {
            p_strID = string.Empty;

            long lngRes = -1;
            try
            {
                string strSQL = @"select max(t.checkid_chr)
  from t_ds_drugstorecheck t
 where t.checkid_chr like ?";

                DataTable dtbValue = null;
                DateTime dtmNow = DateTime.Now;
                clsDS_Public_Supported_SVC clsPub = new clsDS_Public_Supported_SVC();
                clsPub.m_lngGetCurrentDateTime(out dtmNow);
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = dtmNow.ToString("yyMMdd") + "06%";

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (dtbValue == null || dtbValue.Rows.Count == 0)
                {
                    p_strID = dtmNow.ToString("yyMMdd") + "0600001";
                }
                else
                {
                    string strTemp = dtbValue.Rows[0][0].ToString();
                    if (string.IsNullOrEmpty(strTemp))
                    {
                        p_strID = dtmNow.ToString("yyMMdd") + "0600001";
                    }
                    else
                    {
                        strTemp = strTemp.Substring(9, 4);
                        p_strID = dtmNow.ToString("yyMMdd") + "06" + (Convert.ToInt32(strTemp) + 1).ToString("00000");
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion      

        #region ��ȡ�г����¼����ӯҩƷ
        /// <summary>
        /// ��ȡ�г����¼����ӯҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckID">�̵�ID</param>
        /// <param name="p_dtbOut">����ҩƷ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHasOutCheckMedicine( string p_strCheckID, out DataTable p_dtbOut)
        {
            p_dtbOut = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct t.medicineid_chr,        case
        when t.lotno_vchr = 'UNKNOWN' then
         ''
        else
            t.lotno_vchr
        end lotno_vchr, s.outdrugstoreid_vchr
  from t_ds_outstorage_detail t
	left join t_ds_outstorage s on s.seriesid_int = t.seriesid2_int
 where s.outdrugstoreid_vchr = ?
   and t.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strCheckID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbOut, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�ѱ������������ӯ��¼
        /// <summary>
        /// ��ȡ�ѱ������������ӯ��¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckID">�̵�ID</param>
        /// <param name="p_dtbIn">���ҩƷ��¼</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInCheckMedicine( string p_strCheckID, out DataTable p_dtbIn)
        {
            p_dtbIn = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.medicineid_chr,
              case
        when t.lotno_vchr = 'UNKNOWN' then
         ''
        else
            t.lotno_vchr
        end lotno_vchr,
       t.indrugstoreid_vchr,
       t.seriesid_int,
       t.seriesid2_int,
       t.ruturnnum_int
  from t_ds_instorage_detail t, t_ds_instorage b
 where b.indrugstoreid_vchr = ?
   and t.status = 1
   and t.seriesid2_int = b.seriesid_int
   and (b.status = 1 or b.status = 2)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strCheckID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbIn, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�ѱ������������̿���¼
        /// <summary>
        /// ��ȡ�ѱ������������̿���¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckID">�̵�ID</param>
        /// <param name="p_dtbIn">����ҩƷ��¼</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutCheckMedicine( string p_strCheckID, out DataTable p_dtbIn)
        {
            p_dtbIn = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.medicineid_chr,
              case
        when t.lotno_vchr = 'UNKNOWN' then
         ''
        else
            t.lotno_vchr
        end lotno_vchr,
       b.outdrugstoreid_vchr,
       t.seriesid_int,
       t.seriesid2_int
  from t_ds_outstorage_detail t, t_ds_outstorage b
 where b.outdrugstoreid_vchr = ?
   and t.status = 1
   and t.seriesid2_int = b.seriesid_int
   and (b.status_int = 1 or b.status_int = 2)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strCheckID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbIn, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�̵�������Ϊ�������
        /// <summary>
        /// ��ȡ�̵�������Ϊ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckID">�̵�ID</param>
        /// <param name="p_dtbResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckResult( string p_strCheckID, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.medicineid_chr,
       case
         when a.lotno_vchr = 'UNKNOWN' then
          ''
         else
          a.lotno_vchr
       end lotno_vchr,
       a.indrugstoreid_vchr,
       a.ipcheckresult_int,
       a.opcheckresult_int,
       a.seriesid_int,
       a.validperiod_dat
  from t_ds_drugstorecheck_detail a, t_ds_drugstorecheck b
 where a.ipcheckresult_int <> 0
   and a.status_int = 1
   and a.seriesid2_int = b.seriesid_int
   and b.status_int <> 0
   and b.checkid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strCheckID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ��ȡ�̵�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSEQ">��ϸ������</param>
        /// <param name="p_dtbResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckResult( long p_lngSEQ, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            long lngRes = 0;
            try
            {
                string strSQL = @"select a.medicineid_chr,
       case
         when a.lotno_vchr = 'UNKNOWN' then
          ''
         else
          a.lotno_vchr
       end lotno_vchr,
       a.indrugstoreid_vchr,
       a.ipcheckresult_int,
       a.opcheckresult_int,
       a.validperiod_dat,
       a.ipcallprice_int,
       a.opcallprice_int
  from t_ds_drugstorecheck_detail a
 where a.medicineid_chr =
       (select b.medicineid_chr
          from t_ds_drugstorecheck_detail b
         where b.seriesid_int = ?)
   and a.seriesid2_int = (select c.seriesid2_int
                            from t_ds_drugstorecheck_detail c
                           where c.seriesid_int = ?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_lngSEQ;
                objDPArr[1].Value = p_lngSEQ;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ҩƷID��ȡҩƷ
        /// <summary>
        /// ����ҩƷID��ȡҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID">ҩƷID</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_dtbMedicine">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineByMedicineID( string p_strMedicineID, string p_strStorageID,bool p_blnIsHospital, out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;
            long lngRes = 0;
            try
            {
                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct '' checkmedicineorder_chr,
                b.assistcode_chr,
                b.medicinetypeid_chr,
                a.medicineid_chr,
                a.medicinename_vchr,
                a.medspec_vchr,
                a.oprealgross_int,
                a.opretailprice_int,
                a.iprealgross_int,
                a.ipretailprice_int,
                decode(b.ipchargeflg_int,
                       0,
                       a.oprealgross_int,
                       a.iprealgross_int) realgross_int,
                decode(b.ipchargeflg_int,
                       0,
                       a.opretailprice_int,
                       a.ipretailprice_int) retailprice_int,
                case
                  when a.lotno_vchr = 'UNKNOWN' then
                   ''
                  else
                   a.lotno_vchr
                end lotno_vchr,
                a.dsinstoreid_vchr,
                a.validperiod_dat,
                a.instoreid_vchr indrugstoreid_vchr,
                c.medicinepreptype_chr,
                c.medicinepreptypename_vchr,
                '' storagerackcode_vchr,
                b.productorid_chr,
                a.packqty_dec,
                b.opchargeflg_int,
                b.ipchargeflg_int,
                decode(b.ipchargeflg_int, 0, a.opunit_chr, a.ipunit_chr) unit_chr,
                a.opunit_chr,
                a.ipunit_chr,
                0 checkgross_int,
                0 balance,
                0 retailmoney,
                0 realmoney,0 callmoney,
                decode(b.ipchargeflg_int,
                       0,
                       a.opwholesaleprice_int,
                       a.ipwholesaleprice_int) callprice_int,
                a.opwholesaleprice_int opcallprice_int,
                a.ipwholesaleprice_int ipcallprice_int,
                a.dsinstoragedate_dat,
                h.medicinetypename_vchr,
                '' checkreason_vchr
  from t_ds_storage_detail a
 inner join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_bse_medstore d on d.deptid_chr = a.drugstoreid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.medicineid_chr = ?
   and d.medstoreid_chr = ?
 order by b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct '' checkmedicineorder_chr,
                b.assistcode_chr,
                b.medicinetypeid_chr,
                a.medicineid_chr,
                a.medicinename_vchr,
                a.medspec_vchr,
                a.oprealgross_int,
                a.opretailprice_int,
                a.iprealgross_int,
                a.ipretailprice_int,
                decode(b.opchargeflg_int,
                       0,
                       a.oprealgross_int,
                       a.iprealgross_int) realgross_int,
                decode(b.opchargeflg_int,
                       0,
                       a.opretailprice_int,
                       a.ipretailprice_int) retailprice_int,
                case
                  when a.lotno_vchr = 'UNKNOWN' then
                   ''
                  else
                   a.lotno_vchr
                end lotno_vchr,
                a.dsinstoreid_vchr,
                a.validperiod_dat,
                a.instoreid_vchr indrugstoreid_vchr,
                c.medicinepreptype_chr,
                c.medicinepreptypename_vchr,
                '' storagerackcode_vchr,
                b.productorid_chr,
                a.packqty_dec,
                b.opchargeflg_int,
                b.ipchargeflg_int,
                decode(b.opchargeflg_int, 0, a.opunit_chr, a.ipunit_chr) unit_chr,
                a.opunit_chr,
                a.ipunit_chr,
                0 checkgross_int,
                0 balance,
                0 retailmoney,
                0 realmoney,0 callmoney,
                decode(b.opchargeflg_int,
                       0,
                       a.opwholesaleprice_int,
                       a.ipwholesaleprice_int) callprice_int,
                a.opwholesaleprice_int opcallprice_int,
                a.ipwholesaleprice_int ipcallprice_int,
                a.dsinstoragedate_dat,
                h.medicinetypename_vchr,
                '' checkreason_vchr
  from t_ds_storage_detail a
 inner join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_bse_medstore d on d.deptid_chr = a.drugstoreid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.medicineid_chr = ?
   and d.medstoreid_chr = ?
 order by b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strMedicineID;
                objDPArr[1].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ�̵���ϸ(��ӡ)
        /// <summary>
        /// ��ȡ�̵���ϸ(��ӡ)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngMainSEQ">��������</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺҩ��</param>
        /// <param name="p_dtbStorageCheck_detail">��ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStoreCheck_DetailForPrint( long p_lngMainSEQ,bool p_blnIsHospital, out DataTable p_dtbStorageCheck_detail)
        {
            p_dtbStorageCheck_detail = null;
            long lngRes = 0;
            try
            {
                string strSQL = string.Empty;
                if (p_blnIsHospital)
                {
                    strSQL = @"select h.medicinetypename_vchr,
       m.assistcode_chr,
       t.medicinename_vchr medicinename_vch,
       t.medspec_vchr,
       decode(t.ipchargeflg_int, 0, t.opunit_chr, t.ipunit_chr) opunit_chr,
       t.productorid_chr,
       decode(t.ipchargeflg_int, 0, t.opcallprice_int, t.ipcallprice_int) callprice_int,
       decode(t.ipchargeflg_int,
              0,
              t.opretailprice_int,
              t.ipretailprice_int) retailprice_int,
       decode(t.ipchargeflg_int,
              0,
              t.oprealgross_int + round(t.iprealgross_int / t.packqty_dec),
              t.oprealgross_int * t.packqty_dec + t.iprealgross_int) currentgross_int,
       decode(t.ipchargeflg_int,
              0,
              t.opcheckgross_int +
              round(t.ipcheckgross_int / t.packqty_dec, 2),
              t.opcheckgross_int * t.packqty_dec + t.ipcheckgross_int) checkgross_int,
       decode(t.ipchargeflg_int,
              0,
              t.opcheckresult_int +
              round(t.ipcheckresult_int / t.packqty_dec, 2),
              t.opcheckresult_int * t.packqty_dec + t.ipcheckresult_int) checkresult_int,
       m.medicinetypeid_chr
  from t_ds_drugstorecheck_detail t
 inner join t_bse_medicine m on t.medicineid_chr = m.medicineid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    m.medicinetypeid_chr
 where t.status_int = 1
	 and t.seriesid2_int = ?
 order by m.assistcode_chr";
                }
                else
                {
                    strSQL = @"select h.medicinetypename_vchr,
       m.assistcode_chr,
       t.medicinename_vchr medicinename_vch,
       t.medspec_vchr,
       decode(t.opchargeflg_int, 0, t.opunit_chr, t.ipunit_chr) opunit_chr,
       t.productorid_chr,
       decode(t.opchargeflg_int, 0, t.opcallprice_int, t.ipcallprice_int) callprice_int,
       decode(t.opchargeflg_int,
              0,
              t.opretailprice_int,
              t.ipretailprice_int) retailprice_int,
       decode(t.opchargeflg_int,
              0,
              t.oprealgross_int + round(t.iprealgross_int / t.packqty_dec),
              t.oprealgross_int * t.packqty_dec + t.iprealgross_int) currentgross_int,
       decode(t.opchargeflg_int,
              0,
              t.opcheckgross_int +
              round(t.ipcheckgross_int / t.packqty_dec, 2),
              t.opcheckgross_int * t.packqty_dec + t.ipcheckgross_int) checkgross_int,
       decode(t.opchargeflg_int,
              0,
              t.opcheckresult_int +
              round(t.ipcheckresult_int / t.packqty_dec, 2),
              t.opcheckresult_int * t.packqty_dec + t.ipcheckresult_int) checkresult_int,
       m.medicinetypeid_chr
  from t_ds_drugstorecheck_detail t
 inner join t_bse_medicine m on t.medicineid_chr = m.medicineid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    m.medicinetypeid_chr
 where t.status_int = 1
	 and t.seriesid2_int = ?
 order by m.assistcode_chr";
                }

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngMainSEQ;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbStorageCheck_detail, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ��ȡ�̵���ϸ(��ӡ)��ƽ
        /// <summary>
        /// ��ȡ�̵���ϸ(��ӡ)��ƽ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngMainSEQ">��������</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺҩ��</param>
        /// <param name="p_dtbStorageCheck_detail">��ϸ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStoreCheck_DetailForPrintCP( long p_lngMainSEQ, bool p_blnIsHospital, out DataTable p_dtbStorageCheck_detail)
        {
            p_dtbStorageCheck_detail = null;
            long lngRes = 0;
            try
            {
                string strSQL = string.Empty;

                strSQL = @"select '' checkmedicineorder_chr,
       m.assistcode_chr,
       m.medicinetypeid_chr,
       t.medicineid_chr,
       t.medicinename_vchr medicinename_vch,
       t.medspec_vchr,
       t.opunit_chr,
       0 realgross_int,
       0 callprice_int,
       0 wholesaleprice_int,
       0 retailprice_int,
       case
         when t.lotno_vchr = 'UNKNOWN' then
          ''
         else
          t.lotno_vchr
       end lotno_vchr,
       '' instorageid_vchr,
       t.validperiod_dat,
       t.productorid_chr,
       '' vendorid_chr,
       c.medicinepreptype_chr,
       c.medicinepreptypename_vchr,
       '' storagerackcode_vchr,
       0 currentgross_int,
       0 checkgross_int,
       0 retailmoney,
       0 realmoney,
       0 checkresult_int,
       h.medicinetypename_vchr,
       t.checkreason_vchr,
       
       t.iszero_int,
       t.modifier_chr,
       t.modifydate_dat,
       t.status_int,
       
       t.seriesid_int,
       t.seriesid2_int,
       0 pausegross_int,
       0 rowno
  from t_ds_drugstorecheck_detail t
  left join t_ds_drugstorecheck f
    on t.seriesid2_int = f.seriesid_int
   and f.status_int <> 0
  left join t_bse_medicine m
    on t.medicineid_chr = m.medicineid_chr
  left join t_aid_medicinepreptype c
    on c.medicinepreptype_chr = m.medicinepreptype_chr
  left join t_aid_medicinetype h
    on h.medicinetypeid_chr = m.medicinetypeid_chr
 where t.status_int = 1
   and t.seriesid2_int = 0";
                

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_lngMainSEQ;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbStorageCheck_detail, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region δ���ҵ�񵥾�
        /// <summary>
        /// δ���ҵ�񵥾�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDrugStoreID">ҩ��ID</param>
        /// <param name="p_dtbIn">��ⵥ</param>
        /// <param name="p_dtbOut">���ⵥ</param>
        /// <param name="p_dtbAsk">���쵥</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckUnAuditData( string p_strDrugStoreID, out DataTable p_dtbIn, out DataTable p_dtbOut,out DataTable p_dtbAsk)
        {
            p_dtbIn = new DataTable();
            p_dtbOut = new DataTable();
            p_dtbAsk = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL = @"select distinct b.medicineid_chr
  from t_ds_instorage a, t_ds_instorage_detail b
 where a.status = 1 and b.status = 1
   and b.seriesid2_int = a.seriesid_int
   and a.drugstoreid_chr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objParaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objParaArr);
                objParaArr[0].Value = p_strDrugStoreID;
                objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbIn, objParaArr);
                if (p_dtbIn != null && p_dtbIn.Rows.Count > 0)
                {
                    p_dtbIn.PrimaryKey = new DataColumn[] { p_dtbIn.Columns["medicineid_chr"] };
                }

                strSQL = @" select distinct b.medicineid_chr
   from t_ds_outstorage a, t_ds_outstorage_detail b
  where a.status_int = 1 and b.status = 1
    and b.seriesid2_int = a.seriesid_int
    and a.drugstoreid_chr = ?";

                objHRPServ.CreateDatabaseParameter(1, out objParaArr);
                objParaArr[0].Value = p_strDrugStoreID;
                objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbOut, objParaArr);
                if (p_dtbOut != null && p_dtbOut.Rows.Count > 0)
                {
                    if (p_dtbOut != null && p_dtbOut.Rows.Count > 0)
                    {
                        p_dtbOut.PrimaryKey = new DataColumn[] { p_dtbOut.Columns["medicineid_chr"] };
                    }
                }

                //20090828:���쵥������û�д���ҩ������˶�ҩ��δ��˵ĵ��ݣ������쵥״̬��ҩ����˵ģ�
                strSQL = @"select distinct a.medicineid_chr
  from t_ms_outstorage_detail a
  left join t_ms_outstorage b
    on b.seriesid_int = a.seriesid2_int
  left join t_ds_ask c
    on c.askid_vchr = b.askid_vchr
 where b.status > 1
   and a.status = 1
   and c.status_int = 3
   and c.askdept_chr = ?";

                objHRPServ.CreateDatabaseParameter(1, out objParaArr);
                objParaArr[0].Value = p_strDrugStoreID;
                objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbAsk, objParaArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (p_dtbAsk != null && p_dtbAsk.Rows.Count > 0)
                {
                    if (p_dtbAsk != null && p_dtbAsk.Rows.Count > 0)
                    {
                        p_dtbAsk.PrimaryKey = new DataColumn[] { p_dtbAsk.Columns["medicineid_chr"] };
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region �Ƿ�����ݵ���ⲻΪ0
        /// <summary>
        /// �Ƿ�����ݵ���ⲻΪ0
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDrugStoreID">�ֿ�ID</param>
        /// <param name="p_dtmStartDate"></param>
        /// <param name="p_dtmEndDate"></param>
        /// <param name="p_blnExist"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckExistTempIn( string p_strDrugStoreID, DateTime p_dtmStartDate,DateTime p_dtmEndDate,out bool p_blnExist)
        {
            p_blnExist = false;
            DataTable p_dtbTemp = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL = @"select sum(round(b.ipamount_int * b.opretailprice_int / b.packqty_dec, 4))
  from t_ds_instorage a, t_ds_instorage_detail b
 where a.seriesid_int = b.seriesid2_int
   and a.typecode_vchr in (select z.typecode_vchr
                             from t_aid_impexptype z
                            where z.typename_vchr = '�ݵ����'
                              and z.storgeflag_int <> 0
                              and z.status_int = 1
                              and z.flag_int = 0)
   and a.drugstoreid_chr = ?
   and (a.status = 2 or a.status = 3)
   and a.drugstoreexam_date between ? and ?
   and b.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objParaArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objParaArr);
                objParaArr[0].Value = p_strDrugStoreID;
                objParaArr[1].DbType = DbType.DateTime;
                objParaArr[1].Value = p_dtmStartDate;
                objParaArr[2].DbType = DbType.DateTime;
                objParaArr[2].Value = p_dtmEndDate;
                objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbTemp, objParaArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (p_dtbTemp != null && p_dtbTemp.Rows.Count > 0)
                {
                    double dblSum = 0d;
                    if (double.TryParse(p_dtbTemp.Rows[0][0].ToString(),out dblSum))
                    {
                        if (dblSum != 0)
                        {
                            p_blnExist = true;
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ��ȡ��ǰ�����ڵ�����
        /// <summary>
        /// ��ȡ��ǰ�����ڵ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDrugStoreID">�ֿ�ID</param>
        /// <param name="p_dtmStartDate"></param>
        /// <param name="p_dtmEndDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCurrentAccountDate( string p_strDrugStoreID, out DateTime p_dtmStartDate, out DateTime p_dtmEndDate)
        {
            p_dtmStartDate = DateTime.MinValue;
            p_dtmEndDate = DateTime.MinValue;
            DataTable p_dtbTemp = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL = @"select t.endtime_dat
  from t_ds_accountperiod t
 where t.drugstoreid_chr = ?
 order by t.accountid_chr desc";

                clsDS_Public_Supported_SVC objService = new clsDS_Public_Supported_SVC();
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objParaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objParaArr);
                objParaArr[0].Value = p_strDrugStoreID;
                objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbTemp, objParaArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (p_dtbTemp != null && p_dtbTemp.Rows.Count > 0)
                {
                    p_dtmStartDate = Convert.ToDateTime(p_dtbTemp.Rows[0]["endtime_dat"]).AddSeconds(1);
                }
                else
                {
                    string strDate = string.Empty;                    
                    objService.m_lngGetSysParaByID("5001", out strDate);
                    DateTime.TryParse(strDate, out p_dtmStartDate);
                }
                objService.m_lngGetSystemDateTime(out p_dtmEndDate);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ��鵱ǰ�������Ƿ��ѿ��̵㵥
        /// <summary>
        /// ��鵱ǰ�������Ƿ��ѿ��̵㵥
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDrugStoreID">�ֿ�ID</param>
        /// <param name="p_dtmStartDate"></param>
        /// <param name="p_dtmEndDate"></param>
        /// <param name="p_blnExist"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckExistBill( string p_strDrugStoreID, DateTime p_dtmStartDate, DateTime p_dtmEndDate,string p_strCheckID, out bool p_blnExist)
        {
            p_blnExist = false;
            DataTable p_dtbTemp = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL = "";
                if (p_strCheckID.Length > 0)
                {
                    strSQL = @"select count(*)
  from t_ds_drugstorecheck a
 where a.drugstoreid_chr = ?
   and a.askdate_dat between ? and ?
   and a.status_int <> 0 and a.checkid_chr <> ?";
                }
                else
                {
                    strSQL = @"select count(*)
  from t_ds_drugstorecheck a
 where a.drugstoreid_chr = ?
   and a.askdate_dat between ? and ?
   and a.status_int <> 0";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objParaArr = null;
                if (p_strCheckID.Length > 0)
                {
                    objHRPServ.CreateDatabaseParameter(4, out objParaArr);
                    objParaArr[0].Value = p_strDrugStoreID;
                    objParaArr[1].DbType = DbType.DateTime;
                    objParaArr[1].Value = p_dtmStartDate;
                    objParaArr[2].DbType = DbType.DateTime;
                    objParaArr[2].Value = p_dtmEndDate;
                    objParaArr[3].Value = p_strCheckID;
                }
                else
                {
                    objHRPServ.CreateDatabaseParameter(3, out objParaArr);
                    objParaArr[0].Value = p_strDrugStoreID;
                    objParaArr[1].DbType = DbType.DateTime;
                    objParaArr[1].Value = p_dtmStartDate;
                    objParaArr[2].DbType = DbType.DateTime;
                    objParaArr[2].Value = p_dtmEndDate;
                }
                objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbTemp, objParaArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (p_dtbTemp != null && p_dtbTemp.Rows.Count > 0)
                {
                    double dblSum = 0d;
                    if (double.TryParse(p_dtbTemp.Rows[0][0].ToString(), out dblSum))
                    {
                        if (dblSum > 0)
                        {
                            p_blnExist = true;
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

    }
}
