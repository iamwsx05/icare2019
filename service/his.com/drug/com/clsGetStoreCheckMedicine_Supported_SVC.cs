using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ��ȡ�̵�ҩƷ
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsGetStoreCheckMedicine_Supported_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ͨ��˳��Ż�ȡҩƷ

        /// <summary>
        /// ͨ��˳��Ż�ȡҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSortBegin">˳��Ŷο�ʼ����</param>
        /// <param name="p_strSortEnd">˳��Ŷν�������</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_dtbMedicine">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineBySortNum( string p_strSortBegin, string p_strSortEnd, string p_strStorageID, bool p_blnIsHospital,out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;

            long lngRes = -1;
            try
            {
                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and d.checkmedicineorder_chr between ? and ?
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and d.checkmedicineorder_chr between ? and ?
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strSortBegin;
                objDPArr[1].Value = p_strSortEnd;
                objDPArr[2].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                p_dtbMedicine = m_dtbGetMedicineWithoutLastCheckZero( p_strStorageID, p_dtbMedicine);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ҩƷ�����ȡҩƷ
        /// <summary>
        /// ����ҩƷ�����ȡҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineCodeBegin">ҩƷ����ο�ʼ����</param>
        /// <param name="p_strMedicineCodeEnd">ҩƷ����ν�������</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_dtbMedicine">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineByMedicineCode( string p_strMedicineCodeBegin, string p_strMedicineCodeEnd, string p_strStorageID, bool p_blnIsHospital,out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;
            long lngRes = 0;
            try
            {
                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and b.assistcode_chr between ? and ?
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and b.assistcode_chr between ? and ?
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strMedicineCodeBegin;
                objDPArr[1].Value = p_strMedicineCodeEnd;
                objDPArr[2].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                p_dtbMedicine = m_dtbGetMedicineWithoutLastCheckZero( p_strStorageID, p_dtbMedicine);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ҩƷ���ͻ�ȡҩƷ
        /// <summary>
        /// ����ҩƷ���ͻ�ȡҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicinePreptypeID">ҩƷ����ID</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_dtbMedicine">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineByMedicinePreptype( string p_strMedicinePreptypeID, string p_strStorageID, bool p_blnIsHospital,out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;

            long lngRes = 0;
            try
            {
                string strType = string.Empty;

                if (!string.IsNullOrEmpty(p_strMedicinePreptypeID))
                {
                    strType = @"
   and c.medicinepreptype_chr = ?";
                }

                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ? " + strType + @"
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ? " + strType + @"
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;

                if (!string.IsNullOrEmpty(p_strMedicinePreptypeID))
                {
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strStorageID;
                    objDPArr[1].Value = p_strMedicinePreptypeID;
                }
                else
                {
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = p_strStorageID;
                }

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                p_dtbMedicine = m_dtbGetMedicineWithoutLastCheckZero( p_strStorageID, p_dtbMedicine);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ����ҩƷ���ͻ�ȡҩƷ
        /// <summary>
        /// ����ҩƷ���ͻ�ȡҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineTypeID">ҩƷ����ID</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_dtbMedicine">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineByMedicineType( string p_strMedicineTypeID, string p_strStorageID, bool p_blnIsHospital,out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;

            long lngRes = 0;
            try
            {
                string strType = string.Empty;

                if (!string.IsNullOrEmpty(p_strMedicineTypeID))
                {
                    strType = @"
   and b.medicinetypeid_chr = ?";
                }

                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ? " + strType + @"
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ? " + strType + @"
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;

                if (!string.IsNullOrEmpty(p_strMedicineTypeID))
                {
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strStorageID;
                    objDPArr[1].Value = p_strMedicineTypeID;
                }
                else
                {
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = p_strStorageID;
                }

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                p_dtbMedicine = m_dtbGetMedicineWithoutLastCheckZero( p_strStorageID, p_dtbMedicine);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// ����ҩƷ���ͻ�ȡҩƷ
        /// </summary>
        /// <param name="p_objPrincipal">ҩƷ����ID����</param>
        /// <param name="p_strMedicineTypeIDArr">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_strStorageID">ҩƷ����</param>
        /// <param name="p_dtbMedicine"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineByMedicineTypeList(List<string> p_strMedicineTypeIDArr, string p_strStorageID,bool p_blnIsHospital, out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;

            long lngRes = 0;
            try
            {
                string strType = string.Empty;

                if (p_strMedicineTypeIDArr.Count > 0)
                {
                    strType = @"
   and b.medicinetypeid_chr in(";
                    System.Text.StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < p_strMedicineTypeIDArr.Count; i++)
                    {
                        sb.Append("? ,");
                    }
                    strType = strType + sb.Remove(sb.Length - 2, 2).ToString() + ")";
                }
                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ? " + strType + @"
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ? " + strType + @"
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;

                if (p_strMedicineTypeIDArr.Count > 0)
                {
                    objHRPServ.CreateDatabaseParameter(p_strMedicineTypeIDArr.Count + 1, out objDPArr);
                    objDPArr[0].Value = p_strStorageID;
                    for (int i2 = 0; i2 < p_strMedicineTypeIDArr.Count; i2++)
                    {
                        objDPArr[i2 + 1].Value = p_strMedicineTypeIDArr[i2];
                    }
                }
                else
                {
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = p_strStorageID;
                }

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                p_dtbMedicine = m_dtbGetMedicineWithoutLastCheckZero( p_strStorageID, p_dtbMedicine);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���ݻ��ܺŻ�ȡҩƷ��Ϣ

        /// <summary>
        /// ���ݻ��ܺŻ�ȡҩƷ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRackNOBegin">���ܺ���ο�ʼ����</param>
        /// <param name="p_strRackNOEnd">���ܺ���ν�������</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_dtbMedicine">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedicineByMedicineRackNO( string p_strRackNOBegin, string p_strRackNOEnd, string p_strStorageID, bool p_blnIsHospital,out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;
            long lngRes = 0;
            try
            {
                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and e.storagerackcode_vchr between ? and ?
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and e.storagerackcode_vchr between ? and ?
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strRackNOBegin;
                objDPArr[1].Value = p_strRackNOEnd;
                objDPArr[2].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                p_dtbMedicine = m_dtbGetMedicineWithoutLastCheckZero( p_strStorageID, p_dtbMedicine);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡȫ��ҩƷ
        /// <summary>
        /// ��ȡȫ��ҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_dtbMedicine">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMedicine( string p_strStorageID, bool p_blnIsHospital,out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;

            long lngRes = -1;
            try
            {
                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
                b.productorid_chr,
                a.packqty_dec,
                b.opchargeflg_int,
                b.ipchargeflg_int,
                decode(b.ipchargeflg_int, 0, a.opunit_chr, a.ipunit_chr) unit_chr,
                a.opunit_chr,
                a.ipunit_chr,
                0 realgross_int,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
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
                e.storagerackcode_vchr,
                b.productorid_chr,
                a.packqty_dec,
                b.opchargeflg_int,
                b.ipchargeflg_int,
                decode(b.opchargeflg_int, 0, a.opunit_chr, a.ipunit_chr) unit_chr,
                a.opunit_chr,
                a.ipunit_chr,
                0 realgross_int,
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
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          a.opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                p_dtbMedicine = m_dtbGetMedicineWithoutLastCheckZero( p_strStorageID, p_dtbMedicine);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ��һ�ο��Ϊ���ҩƷ
        /// <summary>
        /// ��ȡ��һ�ο��Ϊ���ҩƷ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_hstMedicine">�̵�ҩƷ��Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetLastZeroStorageCheckMedicine( string p_strStorageID, out Hashtable p_hstMedicine)
        {
            p_hstMedicine = null;
            long lngRes = 0;
            try
            {
                string strSQL = string.Empty;

                if (clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    strSQL = @"select seriesid_int
  from (select a.seriesid_int
          from t_ds_drugstorecheck a
         where a.status_int <> 0
           and a.drugstoreid_chr = ?
         order by a.seriesid_int desc)
 where rownum = 1";
                }
                else if (clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytODBC
                    || clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytSQL_Server)
                {
                    strSQL = @"select top 1 a.seriesid_int
  from t_ds_drugstorecheck a
 where a.status_int <> 0
   and a.drugstoreid_chr = ?
 order by a.seriesid_int desc";
                }

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strStorageID;

                DataTable dtbMainSEQ = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbMainSEQ, objDPArr);

                if (dtbMainSEQ == null || dtbMainSEQ.Rows.Count == 0)
                {
                    objHRPServ.Dispose();
                    objHRPServ = null;
                    return -1;
                }

                long lngMainSEQ = Convert.ToInt64(dtbMainSEQ.Rows[0][0]);

                strSQL = @"select b.medicineid_chr,        case
        when b.lotno_vchr = 'UNKNOWN' then
         ''
        else
            b.lotno_vchr
        end lotno_vchr, b.indrugstoreid_vchr 
  from t_ds_drugstorecheck_detail b
 where b.seriesid2_int = ?
   and b.iszero_int = 0
   and b.status_int = 1";

                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = lngMainSEQ;

                DataTable dtbCheck = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbCheck, objDPArr);
                if (dtbCheck != null)
                {
                    int intRowsCount = dtbCheck.Rows.Count;
                    if (intRowsCount > 0)
                    {
                        DataRow drTemp = null;
                        p_hstMedicine = new Hashtable();
                        for (int iRow = 0; iRow < intRowsCount; iRow++)
                        {
                            drTemp = dtbCheck.Rows[iRow];
                            string strKey = drTemp["medicineid_chr"].ToString().PadLeft(10, '0') + drTemp["lotno_vchr"].ToString().PadLeft(10, '0') + drTemp["indrugstoreid_vchr"].ToString().PadLeft(13, '0');
                            if (!p_hstMedicine.ContainsKey(strKey))
                                p_hstMedicine.Add(strKey, drTemp["medicineid_chr"].ToString());
                        }
                    }
                }
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

        #region ��ȡ�ų��ϴ��̵�ʱ���Ϊ�㣬����ҲΪ���ҩƷ�������
        /// <summary>
        /// ��ȡ�ų��ϴ��̵�ʱ���Ϊ�㣬����ҲΪ���ҩƷ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_dtbMedicine">���ڿ������</param>
        /// <returns></returns>
        [AutoComplete]
        private DataTable m_dtbGetMedicineWithoutLastCheckZero( string p_strStorageID, DataTable p_dtbMedicine)
        {
            if (p_dtbMedicine == null || p_dtbMedicine.Rows.Count == 0)
            {
                return null;
            }

            Hashtable hstLastIsZero = null;
            long lngRes = m_lngGetLastZeroStorageCheckMedicine( p_strStorageID, out hstLastIsZero);

            if (hstLastIsZero == null || hstLastIsZero.Count == 0)
            {
                return p_dtbMedicine;
            }

            DataRow[] drIsZero = p_dtbMedicine.Select("realgross_int = 0");

            if (drIsZero == null || drIsZero.Length == 0)
            {
                return p_dtbMedicine;
            }

            List<DataRow> lstZero = new List<DataRow>();
            for (int iRow = 0; iRow < drIsZero.Length; iRow++)
            {
                string strKey = drIsZero[iRow]["medicineid_chr"].ToString().PadLeft(10, '0') + drIsZero[iRow]["lotno_vchr"].ToString().PadLeft(10, '0') + drIsZero[iRow]["indrugstoreid_vchr"].ToString().PadLeft(13, '0');
                if (hstLastIsZero.Contains(strKey))
                {
                    lstZero.Add(drIsZero[iRow]);
                }
            }

            if (lstZero.Count > 0)
            {
                foreach (DataRow dr in lstZero)
                {
                    p_dtbMedicine.Rows.Remove(dr);
                }
            }
            return p_dtbMedicine;
        }
        #endregion

        #region ��ȡҩƷ����
        /// <summary>
        /// ��ȡҩƷ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtMedType"></param>
        [AutoComplete]
        public long m_lngGetMedType( out DataTable dtMedType)
        {
            long lngRes = 0;
            dtMedType = new DataTable(); 
            string strSQL = @"select medicinetypeid_chr, medicinetypename_vchr
                                from t_aid_medicinetype";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtMedType);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ѯ����ҩƷ������Ϣ

        /// <summary>
        /// ��ѯ����ҩƷ������Ϣ

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        public long m_lngFindAllMedicineType(
            
            out clsMedicineType_VO[] p_objResultArr)
        {
            p_objResultArr = new clsMedicineType_VO[0];
            long lngRes = 0; 

            string strSQL = @"select medicinetypeid_chr, medicinetypename_vchr
                                from t_aid_medicinetype";
            lngRes = m_getMedTypeResult(strSQL, out p_objResultArr);
            return lngRes;
        }
        #endregion

        #region ���ҩƷ���Ĳ�ѯ���

        /// <summary>
        /// ���ҩƷ���Ĳ�ѯ���

        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="p_objResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_getMedTypeResult(string strSQL, out clsMedicineType_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedicineType_VO[0];
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                objHRPSvc.Dispose();
                objHRPSvc = null;
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    p_objResultArr = new clsMedicineType_VO[dtbResult.Rows.Count];

                    for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
                    {
                        p_objResultArr[i1] = new clsMedicineType_VO();
                        p_objResultArr[i1].m_strMedicineTypeID = dtbResult.Rows[i1]["medicinetypeid_chr"].ToString().Trim();
                        p_objResultArr[i1].m_strMedicineTypeName = dtbResult.Rows[i1]["medicinetypename_vchr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡҩƷ����

        /// <summary>
        /// ��ȡҩƷ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMTVO">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStorageMedicineType( out clsMS_MedicineType_VO[] p_objMTVO)
        {
            p_objMTVO = null;
            long lngRes = 0;

            try
            {
                string strSQL = @"select distinct a.medicinetypeid_chr, a.medicinetypename_vchr
  from t_aid_medicinetype a order by a.medicinetypeid_chr";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                DataTable dtbValue = null;

                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbValue);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (lngRes > 0 && dtbValue != null)
                {
                    int intRowsCount = dtbValue.Rows.Count;
                    if (intRowsCount == 0)
                    {
                        return 0;
                    }

                    p_objMTVO = new clsMS_MedicineType_VO[intRowsCount];
                    DataRow drTemp = null;
                    for (int iRow = 0; iRow < intRowsCount; iRow++)
                    {
                        p_objMTVO[iRow] = new clsMS_MedicineType_VO();
                        drTemp = dtbValue.Rows[iRow];
                        p_objMTVO[iRow].m_strMedicineTypeID_CHR = drTemp["medicinetypeid_chr"].ToString();
                        p_objMTVO[iRow].m_strMedicineTypeName_VCHR = drTemp["medicinetypename_vchr"].ToString();
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

        #region ��ȡȫ��ҩƷ(���ʱʹ�ã�����seriesid_int)
        /// <summary>
        /// ��ȡȫ��ҩƷ(���ʱʹ�ã�����seriesid_int)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺ��λ</param>
        /// <param name="p_dtbMedicine">ҩƷ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllMedicineForCommit( string p_strStorageID, bool p_blnIsHospital, out DataTable p_dtbMedicine)
        {
            p_dtbMedicine = null;

            long lngRes = -1;
            try
            {
                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
                b.assistcode_chr,
                b.medicinetypeid_chr,
                a.medicineid_chr,
                a.medicinename_vchr,
                a.medspec_vchr,
                a.oprealgross_int,
                round(a.opretailprice_int,4) opretailprice_int,
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
                e.storagerackcode_vchr,
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
                '' checkreason_vchr,
                a.seriesid_int,
                a.opavailablegross_num,
                a.ipavailablegross_num
  from t_ds_storage_detail a
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          opretailprice_int,
          lotno_vchr desc";
                }
                else
                {
                    strSQL = @"select distinct d.checkmedicineorder_chr,
                b.assistcode_chr,
                b.medicinetypeid_chr,
                a.medicineid_chr,
                a.medicinename_vchr,
                a.medspec_vchr,
                a.oprealgross_int,
                round(a.opretailprice_int,4) opretailprice_int,
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
                e.storagerackcode_vchr,
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
                '' checkreason_vchr,
                a.seriesid_int,
                a.opavailablegross_num,
                a.ipavailablegross_num
  from t_ds_storage_detail a
  left join t_bse_medicine b on a.medicineid_chr = b.medicineid_chr
  left join t_aid_medicinepreptype c on c.medicinepreptype_chr =
                                        b.medicinepreptype_chr
  left join t_ms_checkmedicineorder d on d.medicineid_chr =
                                         a.medicineid_chr
                                     and a.drugstoreid_chr =
                                         d.storageid_chr
  left join t_ms_storagerackset e on e.storagerackid_chr =
                                     d.storagerackid_chr
                                 and e.storageid_chr = d.storageid_chr
 inner join t_aid_medicinetype h on h.medicinetypeid_chr =
                                    b.medicinetypeid_chr
 where a.status = 1
   and a.drugstoreid_chr = ?
 order by d.checkmedicineorder_chr,
          b.assistcode_chr,
          a.medicineid_chr,
          opretailprice_int,
          lotno_vchr desc";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strStorageID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbMedicine, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                p_dtbMedicine = m_dtbGetMedicineWithoutLastCheckZero( p_strStorageID, p_dtbMedicine);
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
