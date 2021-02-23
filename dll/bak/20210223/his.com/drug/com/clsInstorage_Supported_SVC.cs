using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ҩ�����ҵ����
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsInstorage_Supported_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���ݲ�ѯ������ȡҩ�����������Ϣ
        /// <summary>
        /// ���ݲ�ѯ������ȡҩ�����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_plnCombine">�Ƿ�Ʒ�ֲ�ѯ</param>
        /// <param name="m_strBeginDate"></param>
        /// <param name="m_strEndDate"></param>
        /// <param name="m_strMakeOrderName"></param>
        /// <param name="m_strTypeCode"></param>
        /// <param name="m_intStatus"></param>
        /// <param name="m_strBorrowDeptID"></param>
        /// <param name="m_strBillID"></param>        
        /// <param name="p_strMedicineName"></param>
        /// <param name="m_dtInstorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInstoragenfoByconditions(bool p_blnCombine, string m_strBeginDate, string m_strEndDate, string m_strMakeOrderName, string m_strTypeCode, int m_intStatus, string m_strBorrowDeptID, string m_strBillID,string p_strMedicineID,
            out DataTable m_dtInstorage)
        {
            m_dtInstorage = null;
            long lngRes = 0;
            try
            {

                string strSQL =
       @"select distinct a.seriesid_int,
                a.indrugstoreid_vchr,
                b.medstorename_vchr,
                a.askid_vchr,
                a.outstorageid_vchr,
                a.drugstoreid_chr,
                a.comment_vchr,
                a.status as status_int,
                decode(a.status,
                       0,
                       'ɾ��',
                       1,
                       '����',
                       2,
                       '���',
                       3,
                       '����',
                       4,
                       '����') as status,
                formtype_int,
                a.typecode_vchr,
                a.borrowdept_chr,
                c.deptname_vchr,
                a.makeorder_dat,
                a.storageexamid_chr,
                d.lastname_vchr as storageexamname,
                a.drugstoreexamid_chr,
                e.lastname_vchr as drugstoreexamname_chr,
                a.inaccounterid_chr,
                f.lastname_vchr as inaccountername,
                a.makerid_chr,
                h.lastname_vchr as makername,
                i.typename_vchr,
                a.drugstoreexam_date,
                a.inaccount_dat,
                0 summoney
  from t_ds_instorage        a,
       t_bse_medstore        b,
       t_bse_deptdesc        c,
       t_bse_employee        d,
       t_bse_employee        e,
       t_bse_employee        f,
       t_bse_employee        h,
       t_aid_impexptype      i,
       t_ds_instorage_detail j,
       t_bse_medicine        k
 where a.drugstoreid_chr = b.deptid_chr(+)
   and a.borrowdept_chr = c.deptid_chr(+)
   and a.storageexamid_chr = d.empid_chr(+)
   and a.drugstoreexamid_chr = e.empid_chr(+)
   and a.inaccounterid_chr = f.empid_chr(+)
   and a.makerid_chr = h.empid_chr(+)
   and a.typecode_vchr = i.typecode_vchr(+)
   and (a.seriesid_int = j.seriesid2_int(+) and j.status = 1)
   and j.medicineid_chr = k.medicineid_chr(+)   
   and a.makeorder_dat between ? and ?";
                if (m_strMakeOrderName != string.Empty)
                {
                    strSQL += " and h.lastname_vchr like '" + m_strMakeOrderName + "%' ";
                }
                if (m_strTypeCode != string.Empty)
                {
                    strSQL += " and a.typecode_vchr=" + m_strTypeCode + " ";
                }

                if (m_intStatus != -1)
                {
                    strSQL += " and a.status=" + m_intStatus + " ";
                }
                if (m_strBorrowDeptID != string.Empty)
                {
                    strSQL += " and a.borrowdept_chr ='" + m_strBorrowDeptID + "' ";
                }
                if (m_strBillID != string.Empty)
                {
                    strSQL += " and a.indrugstoreid_vchr ='" + m_strBillID + "' ";
                }
                
                if (p_strMedicineID.Length > 0)
                {
                    if (p_blnCombine)
                    {
                        strSQL += " and k.assistcode_chr = '" + p_strMedicineID + "' ";
                    }
                    else
                    {
                        strSQL += " and j.medicineid_chr = '" + p_strMedicineID + "' ";
                    }
                    
                }
                strSQL += " order by a.indrugstoreid_vchr desc";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] m_objParaArr = null;
                objHRPServ.CreateDatabaseParameter(2, out m_objParaArr);
                m_objParaArr[0].Value = Convert.ToDateTime(m_strBeginDate);
                m_objParaArr[0].DbType = DbType.DateTime;
                m_objParaArr[1].Value = Convert.ToDateTime(m_strEndDate);
                m_objParaArr[1].DbType = DbType.DateTime;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref m_dtInstorage, m_objParaArr);                
                objHRPServ.Dispose();
                objHRPServ = null;
                DataView dv = m_dtInstorage.DefaultView;
                dv.RowFilter = "status_int<> 0 and formtype_int in (1,2,3,4,6)";
                m_dtInstorage = dv.ToTable();
                foreach (DataRow dr in m_dtInstorage.Rows)
                {
                    if (Convert.ToString(dr["inaccount_dat"]).Substring(0, 4) == "0001")
                    {
                        dr["inaccount_dat"] = DBNull.Value;
                    }
                    if (Convert.ToString(dr["drugstoreexam_date"]).Substring(0, 4) == "0001")
                    {
                        dr["drugstoreexam_date"] = DBNull.Value;
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
        #region ��ȡ����ҩ�����������Ϣ
        /// <summary>
        /// ��ȡ����ҩ�����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strBeginDate"></param>
        /// <param name="m_strEndDate"></param>
        /// <param name="m_dtInstorage"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCurrentDayInstoragenfo( string m_strBeginDate, string m_strEndDate, out DataTable m_dtInstorage)
        {
            m_dtInstorage = null;
            DataView dvTemp;
            long lngRes = 0;
            try
            {/*decode (a.formtype_int,
               1, 'ҩ����ⵥ',
               2, '���쵥',
               3, '������ҩ',
               4, 'ҩ�����',
               5, 'ҩ����ӯ'
              ) as formtype_int,*/
                string strSQL =
       @"select a.seriesid_int,
       a.indrugstoreid_vchr,
       b.deptname_vchr as medstorename_vchr,
       a.askid_vchr,
       a.outstorageid_vchr,
       a.drugstoreid_chr,
       a.comment_vchr,
       decode(a.status,
              0,
              'ɾ��',
              1,
              '����',
              2,
              '���',
              3,
              '����',
              4,
              '����') as status,
       formtype_int,a.status as status_int,
       a.typecode_vchr,
       a.borrowdept_chr,
       c.deptname_vchr,
       a.makeorder_dat,
       a.storageexamid_chr,
       d.lastname_vchr as storageexamname,
       a.drugstoreexamid_chr,
       e.lastname_vchr as drugstoreexamname_chr,
       a.inaccounterid_chr,
       f.lastname_vchr as inaccountername,
       a.makerid_chr,
       h.lastname_vchr as makername,
       i.typename_vchr,
       a.drugstoreexam_date,
       a.inaccount_dat,
       0 summoney
  from t_ds_instorage   a,
       t_bse_deptdesc   b,
       t_bse_deptdesc   c,
       t_bse_employee   d,
       t_bse_employee   e,
       t_bse_employee   f,
       t_bse_employee   h,
       t_aid_impexptype i
 where a.drugstoreid_chr = b.deptid_chr(+)
   and a.borrowdept_chr = c.deptid_chr(+)
   and a.storageexamid_chr = d.empid_chr(+)
   and a.drugstoreexamid_chr = e.empid_chr(+)
   and a.inaccounterid_chr = f.empid_chr(+)
   and a.makerid_chr = h.empid_chr(+)
   and a.typecode_vchr = i.typecode_vchr(+)
   and a.makeorder_dat between ? and ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] m_objParaArr = null;
                objHRPServ.CreateDatabaseParameter(2, out m_objParaArr);
                m_objParaArr[0].Value = Convert.ToDateTime(m_strBeginDate + " 00:00:00");
                m_objParaArr[0].DbType = DbType.DateTime;
                m_objParaArr[1].Value = Convert.ToDateTime(m_strEndDate + " 23:59:59");
                m_objParaArr[1].DbType = DbType.DateTime;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref m_dtInstorage, m_objParaArr);
                dvTemp = m_dtInstorage.DefaultView;
                dvTemp.RowFilter = "status_int<>0 and status_int<>1 and formtype_int in (2,3,4,6)";//���˷���������Ч����ⵥ
                m_dtInstorage = dvTemp.ToTable();
                //���Ƶ���ⵥ����ʱ�������
                strSQL = @"select a.seriesid_int,
       a.indrugstoreid_vchr,
       b.deptname_vchr as medstorename_vchr,
       a.askid_vchr,
       a.outstorageid_vchr,
       a.drugstoreid_chr,
       a.comment_vchr,
       decode(a.status,
              0,
              'ɾ��',
              1,
              '����',
              2,
              '���',
              3,
              '����',
              4,
              '����') as status,
       formtype_int,a.status as status_int,
       a.typecode_vchr,
       a.borrowdept_chr,
       c.deptname_vchr,
       a.makeorder_dat,
       a.storageexamid_chr,
       d.lastname_vchr as storageexamname,
       a.drugstoreexamid_chr,
       e.lastname_vchr as drugstoreexamname_chr,
       a.inaccounterid_chr,
       f.lastname_vchr as inaccountername,
       a.makerid_chr,
       h.lastname_vchr as makername,
       i.typename_vchr,
       a.drugstoreexam_date,
       a.inaccount_dat,
       0 summoney
  from t_ds_instorage   a,
       t_bse_deptdesc   b,
       t_bse_deptdesc   c,
       t_bse_employee   d,
       t_bse_employee   e,
       t_bse_employee   f,
       t_bse_employee   h,
       t_aid_impexptype i
 where a.drugstoreid_chr = b.deptid_chr(+)
   and a.borrowdept_chr = c.deptid_chr(+)
   and a.storageexamid_chr = d.empid_chr(+)
   and a.drugstoreexamid_chr = e.empid_chr(+)
   and a.inaccounterid_chr = f.empid_chr(+)
   and a.makerid_chr = h.empid_chr(+)
   and a.typecode_vchr = i.typecode_vchr(+)
   and a.status=1";
                DataTable dtTemp = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtTemp);
                dvTemp = dtTemp.DefaultView;
                dvTemp.RowFilter = "formtype_int in (2,3,4,6)";
                dtTemp = dvTemp.ToTable();
                if (m_dtInstorage != null && dtTemp != null && dtTemp.Rows.Count > 0)
                    m_dtInstorage.Merge(dtTemp, true);//�ϲ���ⵥ
                dvTemp = m_dtInstorage.DefaultView;//���а���ⵥ�ݺ�����
                dvTemp.Sort = "indrugstoreid_vchr desc";
                m_dtInstorage = dvTemp.ToTable();

                foreach (DataRow dr in m_dtInstorage.Rows)
                {
                    if (Convert.ToString(dr["inaccount_dat"]).Substring(0, 4) == "0001")
                    {
                        dr["inaccount_dat"] = DBNull.Value;
                    }
                    if (Convert.ToString(dr["drugstoreexam_date"]).Substring(0, 4) == "0001")
                    {
                        dr["drugstoreexam_date"] = DBNull.Value;
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
       
        #region ������ˮ�Ż�ȡҩ�������ϸ
        /// <summary>
        /// ������ˮ�Ż�ȡҩ�������ϸ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_blnIsHospital">�Ƿ�סԺҩ��</param>
        /// <param name="m_lngSeqid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInstorageDetailByID( bool p_blnIsHospital,long m_lngSeqid, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();
            try
            {
                string strSQL = "";
                if (p_blnIsHospital)
                {
                    strSQL = @"select a.seriesid_int,
       a.seriesid2_int,
       a.medicineid_chr,
       a.medspec_vchr,
       a.opamount_int,
       a.opunit_chr,
       a.ipamount_int,
       a.ipunit_chr,
       a.packqty_dec,
       a.opwholesaleprice_int,
       a.ipwholesaleprice_int,
       a.opretailprice_int,
       a.ipretailprice_int,
       decode(a.lotno_vchr, 'UNKNOWN', '', a.lotno_vchr) lotno_vchr,
       a.validperiod_dat,
       a.status,
       a.medicinename_vchr,
       b.assistcode_chr,
       b.medicinetypeid_chr,
       a.instoreid_vchr,
       a.instoragedate_dat,
       a.productorid_chr,
       0 as retailmoney,
       b.opchargeflg_int,
       b.ipchargeflg_int,
       decode(b.ipchargeflg_int, 0, a.opamount_int, a.ipamount_int) amount_int,
       decode(b.ipchargeflg_int, 0, a.opunit_chr, a.ipunit_chr) unit_chr,
       decode(b.ipchargeflg_int,
              0,
              a.opwholesaleprice_int,
              a.ipwholesaleprice_int) wholesaleprice_int,
       decode(b.ipchargeflg_int,
              0,
              a.opretailprice_int,
              a.ipretailprice_int) retailprice_int
  from t_ds_instorage_detail a, t_bse_medicine b
 where a.medicineid_chr = b.medicineid_chr(+)
   and a.status = 1
   and a.seriesid2_int = ?";
                }
                else
                {
                    strSQL = @"select a.seriesid_int,
       a.seriesid2_int,
       a.medicineid_chr,
       a.medspec_vchr,
       a.opamount_int,
       a.opunit_chr,
       a.ipamount_int,
       a.ipunit_chr,
       a.packqty_dec,
       a.opwholesaleprice_int,
       a.ipwholesaleprice_int,
       a.opretailprice_int,
       a.ipretailprice_int,
       decode(a.lotno_vchr, 'UNKNOWN', '', a.lotno_vchr) lotno_vchr,
       a.validperiod_dat,
       a.status,
       a.medicinename_vchr,
       b.assistcode_chr,
       b.medicinetypeid_chr,
       a.instoreid_vchr,
       a.instoragedate_dat,
       a.productorid_chr,
       0 as retailmoney,
       b.opchargeflg_int,
       b.ipchargeflg_int,
       decode(b.opchargeflg_int, 0, a.opamount_int, a.ipamount_int) amount_int,
       decode(b.opchargeflg_int, 0, a.opunit_chr, a.ipunit_chr) unit_chr,
       decode(b.opchargeflg_int,
              0,
              a.opwholesaleprice_int,
              a.ipwholesaleprice_int) wholesaleprice_int,
       decode(b.opchargeflg_int,
              0,
              a.opretailprice_int,
              a.ipretailprice_int) retailprice_int
  from t_ds_instorage_detail a, t_bse_medicine b
 where a.medicineid_chr = b.medicineid_chr(+)
   and a.status = 1
   and a.seriesid2_int = ?";
                }

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objDataParm = null;
                objHRPServ.CreateDatabaseParameter(1, out objDataParm);
                objDataParm[0].Value = m_lngSeqid;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDataParm);
                objHRPServ.Dispose();
                objHRPServ = null;
                dt.Columns["retailmoney"].Expression = "opretailprice_int*ipamount_int/packqty_dec";
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #region ����ҩ��id��ҩƷid�жϿ�������Ƿ��Ѵ��ڸ�ҩ
        /// <summary>
        /// ����ҩ��id��ҩƷid�жϿ�������Ƿ��Ѵ��ڸ�ҩ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMedicineID">ҩƷID</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_blnHasDetail">�Ƿ����</param>
        /// <param name="p_lngSeriesID">����ڣ��������к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckMedExistInStorage( string p_strMedicineID, string p_strStorageID, out bool p_blnHasDetail, out long p_lngSeriesID)
        {
            p_lngSeriesID = 0;
            p_blnHasDetail = false;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strMedicineID) || string.IsNullOrEmpty(p_strStorageID))
            {
                return -1;
            }
            try
            {
                string strSQL = @"select seriesid_int
                                  from t_ds_storage
                                  where medicineid_chr = ? and drugstoreid_chr= ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strMedicineID;
                objDPArr[1].Value = p_strStorageID;

                DataTable dtbValue = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (dtbValue != null && dtbValue.Rows.Count > 0)
                {
                    p_blnHasDetail = true;
                    p_lngSeriesID = Convert.ToInt64(dtbValue.Rows[0][0]);
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
       
        #region ���������ж��Ƿ������Ӧ��ҩƷ�����ϸ��Ϊ��⸺�����
        /// <summary>
        /// ���������ж��Ƿ������Ӧ��ҩƷ�����ϸ��Ϊ��⸺�����
        /// </summary>
        /// <param name="m_strDurgStoreid"></param>
        /// <param name="m_strLotNo"></param>
        /// <param name="m_strMedicineid"></param>
        /// <param name="m_dblOPAmount"></param>
        /// <param name="p_dblIPAmount"></param>
        /// <param name="m_blnExisted"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngJudgeMedicineExisted(string m_strDurgStoreid, string m_strLotNo, string m_strMedicineid, ref double m_dblOPAmount, ref double p_dblIPAmount, out bool m_blnExisted)
        {
            long lngRes = -1;
            m_blnExisted = false;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"select sum(a.iprealgross_int) iprealgross_int,
			 sum(round(a.iprealgross_int / a.packqty_dec, 2)) oprealgross_int,
			 b.opchargeflg_int
	from t_ds_storage_detail a
	left join t_bse_medicine b on b.medicineid_chr = a.medicineid_chr
  where a.medicineid_chr = ?
   and a.lotno_vchr = ?
   and a.drugstoreid_chr = ?
   and a.status = 1 group by b.opchargeflg_int";
            DataTable dt = new DataTable();
            objHRPServ.CreateDatabaseParameter(3, out objValues);
            objValues[0].Value = m_strMedicineid;
            objValues[1].Value = m_strLotNo;
            objValues[2].Value = m_strDurgStoreid;
            lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objValues);
            objHRPServ.Dispose();
            objHRPServ = null;
            if (lngRes > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    m_blnExisted = true;
                    //20080821 ȫ������С��λ���ж�
                    //if (Convert.ToInt16(dt.Rows[0]["opchargeflg_int"]) == 0)
                    //{
                    //    if (Convert.ToDouble(dt.Rows[0]["oprealgross_int"]) < Math.Abs(m_dblOPAmount))
                    //    {
                    //        m_dblOPAmount = 1;//��ֵ1��Ϊ���븺�������ϸ����ı�־
                    //    }
                    //}
                    //else
                    //{
                    if (Convert.ToDouble(dt.Rows[0]["iprealgross_int"]) < Math.Abs(p_dblIPAmount))
                    {
                        m_dblOPAmount = 1;//��ֵ1��Ϊ���븺�������ϸ����ı�־
                    }
                    //}

                }
                else
                {
                    m_blnExisted = false;
                }

            }
            return lngRes;

        }
        #endregion
        #region ���������ж��Ƿ�����㹻�Ŀ������
        /// <summary>
        /// ���������ж��Ƿ�����㹻�Ŀ������
        /// </summary>
        /// <param name="m_strDurgStoreid"></param>
        /// <param name="m_strLotNo"></param>
        /// <param name="m_strMedicineid"></param>
        /// <param name="m_dtmInstorage"></param>
        /// <param name="m_dblOPAmount"></param>
        /// <param name="m_blnEnough"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInstorageUnExamCheck(string m_strDurgStoreid, string m_strLotNo, string m_strMedicineid, DateTime m_dtmInstorage, double m_dblOPAmount, out bool m_blnEnough)
        {
            long lngRes = -1;
            m_blnEnough = false;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"select a.seriesid_int, a.iprealgross_int, round(a.iprealgross_int / a.packqty_dec, 2) oprealgross_int
  from t_ds_storage_detail a
  where a.medicineid_chr = ?
   and a.lotno_vchr = ?
   and a.drugstoreid_chr = ?
   and a.status = 1";
            DataTable dt = new DataTable();
            objHRPServ.CreateDatabaseParameter(3, out objValues);
            objValues[0].Value = m_strMedicineid;
            objValues[1].Value = m_strLotNo;
            objValues[2].Value = m_strDurgStoreid;
            lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objValues);
            objHRPServ.Dispose();
            objHRPServ = null;
            if (lngRes > 0)
            {
                if (dt.Rows.Count > 0)
                {

                    if (Convert.ToDouble(dt.Rows[0]["oprealgross_int"]) >= m_dblOPAmount)
                    {
                        m_blnEnough = true;
                    }
                }

            }
            return lngRes;

        }
        #endregion
        
        #region ����������ȡĳҩƷ��ǰ�������
        /// <summary>
        /// ����������ȡĳҩƷ��ǰ�������
        /// </summary>
        /// <param name="m_strDurgStoreid"></param>
        /// <param name="m_strLotNo"></param>
        /// <param name="m_strMedicineid"></param>
        /// <param name="m_strInstorageid"></param>
        /// <param name="m_dtmInstorage"></param>
        /// <param name="m_dblOPAmount"></param>
        /// <param name="m_dblIPAmount"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCurrentGrossByConditions(string m_strDurgStoreid, string m_strLotNo, string m_strMedicineid, DateTime m_dtmInstorage, ref double m_dblOPAmount, ref double m_dblIPAmount)
        {
            long lngRes = -1;
            string strSQL;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            System.Data.IDataParameter[] objValues = null;
            strSQL = @"select a.seriesid_int, a.iprealgross_int, round(a.iprealgross_int / a.packqty_dec, 2) oprealgross_int
  from t_ds_storage_detail a
  where a.medicineid_chr = ?
   and a.lotno_vchr = ?
   and a.drugstoreid_chr = ?
   and a.status = 1";
            DataTable dt = new DataTable();
            objHRPServ.CreateDatabaseParameter(3, out objValues);
            objValues[0].Value = m_strMedicineid;
            objValues[1].Value = m_strLotNo;
            objValues[2].Value = m_strDurgStoreid;
            lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objValues);
            objHRPServ.Dispose();
            objHRPServ = null;
            if (lngRes > 0)
            {
                if (dt != null)
                {
                    int m_intRowCount = dt.Rows.Count;
                    m_dblIPAmount = 0d;
                    m_dblOPAmount = 0d;
                    for (int i = 0; i < m_intRowCount; i++)
                    {
                        m_dblIPAmount += Convert.ToDouble(dt.Rows[i]["iprealgross_int"]);
                        m_dblOPAmount += Convert.ToDouble(dt.Rows[i]["oprealgross_int"]);
                    }

                }
            }
            return lngRes;

        }
        #endregion
       
        #region ���ݱ�����������ȡ����ID
        /// <summary>
        /// ���ݱ�����������ȡ����ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strTabName"></param>
        /// <param name="m_strColName"></param>
        /// <param name="m_datLike"></param>
        /// <param name="m_strID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetNewIdByName( string m_strTabName, string m_strColName, DateTime m_datLike, ref string m_strID)
        {
            m_strID = string.Empty;
            if (string.IsNullOrEmpty(m_strTabName))
            {
                return -1;
            }
            DataTable dtValue = new DataTable();
            long lngRes = 0;
            try
            {

                string strSQL = "select max(" + m_strColName + ") + 1 as nextid  from " + m_strTabName + " where " + m_strColName + " like ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objParm = null;
                objHRPServ.CreateDatabaseParameter(1, out objParm);
                objParm[0].Value = m_datLike.ToString("yyMMdd01") + "%";
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtValue, objParm);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (dtValue != null && dtValue.Rows.Count == 1)
                {
                    if (dtValue.Rows[0]["nextid"] != System.DBNull.Value)
                    {
                        m_strID = dtValue.Rows[0]["nextid"].ToString().Substring(dtValue.Rows[0]["nextid"].ToString().Length - 5, 5);
                    }
                    else
                    {
                        m_strID = "00001";
                    }
                }
                else
                {
                    m_strID = "00001";
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

        #region �û������������ţ�Ҫ��ϵͳ�Զ��ѿ��ӵ�����������ļ�¼,����Ч��
        /// <summary>
        /// ��ȡҩƷ��Ĭ�����ź���Ч��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDrugStoreID">ҩ��ID</param>
        /// <param name="p_strMedicineId">ҩƷID</param>
        /// <param name="p_dblOpRetailPrice">������λ�۸�</param>
        /// <param name="p_strLotno">����</param>
        /// <param name="p_datValidDate">��Ч��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDefaultLotno( string p_strDrugStoreID, string p_strMedicineId,double p_dblOpRetailPrice,out string p_strLotno, out DateTime p_datValidDate)
        {
            p_strLotno = string.Empty;
            p_datValidDate = DateTime.MinValue;
            DataTable dtValue = new DataTable();
            long lngRes = 0;
            try
            {

                string strSQL = @"select lotno_vchr,validperiod_dat
	from (select a.lotno_vchr,a.validperiod_dat 
					from t_ds_storage_detail a
				 where a.drugstoreid_chr = ?
					 and a.medicineid_chr = ?
                     and a.opretailprice_int = ?
					 and a.status = 1
				 order by a.iprealgross_int desc)
 where rownum = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objParm = null;
                objHRPServ.CreateDatabaseParameter(3, out objParm);
                objParm[0].Value = p_strDrugStoreID;
                objParm[1].Value = p_strMedicineId;
                objParm[2].Value = p_dblOpRetailPrice;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtValue, objParm);
                objHRPServ.Dispose();
                objHRPServ = null;
                if(dtValue != null && dtValue.Rows.Count > 0)
                {
                    if(Convert.ToString(dtValue.Rows[0]["lotno_vchr"]) != string.Empty)
                    {
                        p_strLotno = dtValue.Rows[0]["lotno_vchr"].ToString();
                    }
                    if(Convert.ToDateTime(dtValue.Rows[0]["validperiod_dat"]).ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        p_datValidDate = Convert.ToDateTime(dtValue.Rows[0]["validperiod_dat"]);
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

        #region ��ȡ�����Ӧ�����۽��
        /// <summary>
        /// ��ȡ�����Ӧ�����۽��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_lngSeriesID"></param>
        /// <param name="p_dblSummoney"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSumMoney( long p_lngSeriesID, out double p_dblSummoney)
        {
            p_dblSummoney = 0;
            DataTable dtbResult = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL =
       @"select a.ipamount_int, a.opretailprice_int, a.packqty_dec
	from t_ds_instorage_detail a, t_ds_instorage b
 where b.seriesid_int = a.seriesid2_int
	 and b.seriesid_int = ?
	 and a.status = 1 ";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] m_objParaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out m_objParaArr);
                m_objParaArr[0].Value = p_lngSeriesID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbResult, m_objParaArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    DataRow dr = null;
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        dr = dtbResult.Rows[i1];
                        p_dblSummoney += Math.Round(Convert.ToDouble(dr["ipamount_int"]) * Convert.ToDouble(dr["opretailprice_int"]) / Convert.ToDouble(dr["packqty_dec"]), 4);
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

        #region ��ȡ���ݺŻ�ȡ��ⵥ��Ϣ
        /// <summary>
        /// ��ȡ���ݺŻ�ȡ��ⵥ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strBillID">���ݺ�</param>
        /// <param name="p_objMain">������Ϣ</param>
        /// <param name="p_dtbSub">��ϸ����Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngLoadBill( bool p_blnIsHospital,string p_strBillID, out clsDS_Instorage_VO p_objMain, out DataTable p_dtbSub)
        {
            p_objMain = null;
            p_dtbSub = null;
            DataTable dtbMain = new DataTable();
            long lngRes = 0;
            try
            {
                string strSQL =
       @"select a.seriesid_int,
       a.indrugstoreid_vchr,
       b.deptname_vchr as medstorename_vchr,
       a.askid_vchr,
       a.outstorageid_vchr,
       a.drugstoreid_chr,
       a.comment_vchr,
       decode(a.status,
              0,
              'ɾ��',
              1,
              '����',
              2,
              '���',
              3,
              '����',
              4,
              '����') as status,
       formtype_int,a.status as status_int,
       a.typecode_vchr,
       a.borrowdept_chr,
       c.deptname_vchr,
       a.makeorder_dat,
       a.storageexamid_chr,
       d.lastname_vchr as storageexamname,
       a.drugstoreexamid_chr,
       e.lastname_vchr as drugstoreexamname_chr,
       a.inaccounterid_chr,
       f.lastname_vchr as inaccountername,
       a.makerid_chr,
       h.lastname_vchr as makername,
       i.typename_vchr,
       a.drugstoreexam_date,
       a.inaccount_dat,
       0 summoney
  from t_ds_instorage   a,
       t_bse_deptdesc   b,
       t_bse_deptdesc   c,
       t_bse_employee   d,
       t_bse_employee   e,
       t_bse_employee   f,
       t_bse_employee   h,
       t_aid_impexptype i
 where a.drugstoreid_chr = b.deptid_chr(+)
   and a.borrowdept_chr = c.deptid_chr(+)
   and a.storageexamid_chr = d.empid_chr(+)
   and a.drugstoreexamid_chr = e.empid_chr(+)
   and a.inaccounterid_chr = f.empid_chr(+)
   and a.makerid_chr = h.empid_chr(+)
   and a.typecode_vchr = i.typecode_vchr(+)
   and a.indrugstoreid_vchr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] m_objParaArr = null;
                objHRPServ.CreateDatabaseParameter(1, out m_objParaArr);
                m_objParaArr[0].Value = p_strBillID;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbMain, m_objParaArr);
                if (dtbMain != null && dtbMain.Rows.Count > 0)
                {
                    p_objMain = new clsDS_Instorage_VO();
                    p_objMain.m_datMAKEORDER_DAT = Convert.ToDateTime(dtbMain.Rows[0]["makeorder_dat"]);
                    p_objMain.m_strMakeName = dtbMain.Rows[0]["makername"].ToString();
                    p_objMain.m_strTYPENAME_VCHR = dtbMain.Rows[0]["typename_vchr"].ToString();
                    p_objMain.m_strBORROWDEPTName_CHR = dtbMain.Rows[0]["deptname_vchr"].ToString();
                    p_objMain.m_strDRUGSTOREName = dtbMain.Rows[0]["medstorename_vchr"].ToString();
                    p_objMain.m_strINDRUGSTOREID_VCHR = dtbMain.Rows[0]["indrugstoreid_vchr"].ToString();
                    p_objMain.m_strCOMMENT_VCHR = dtbMain.Rows[0]["comment_vchr"].ToString();

                    m_lngGetInstorageDetailByID(p_blnIsHospital, Convert.ToInt64(dtbMain.Rows[0]["seriesid_int"]), out p_dtbSub);
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

        #region ��ѯ��ⵥ��״̬
        /// <summary>
        /// ��ѯ��ⵥ��״̬
        /// </summary>
        /// <param name="p_strSeriesid"></param>
        /// <param name="p_strState"></param>
        /// <param name="intQueryStyle">��ѯ����:0-ֱ�Ӳ�ѯ����״̬,1-ͨ���ӱ��ѯ����״̬</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryInstorageState(string p_strSeriesid, int p_intQueryStyle, out string p_strState)
        {
            long lngRes = 0;
            DataTable dtResult = null;
            p_strState = null;
            string strSQL = null;
            if (p_intQueryStyle == 0)
            {
                strSQL = @"select a.status from t_ds_instorage a where a.seriesid_int = ?";
            }
            else
            {
                strSQL = @"select b.status
                                      from t_ds_instorage b
                                     where b.seriesid_int = (select a.seriesid2_int
                                                               from t_ds_instorage_detail a
                                                              where a.seriesid_int = ?)";
            }
            try {
                clsHRPTableService clsHrpSvc = new clsHRPTableService();

                IDataParameter[] objparams = null;
                clsHrpSvc.CreateDatabaseParameter(1,out objparams);
                objparams[0].Value = p_strSeriesid;

                lngRes = clsHrpSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objparams);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_strState = dtResult.Rows[0]["status"].ToString();
                }
                clsHrpSvc.Dispose();
                clsHrpSvc = null;
            }
            catch(Exception objex)
            {
                com.digitalwave.Utility.clsLogText clsError = new com.digitalwave.Utility.clsLogText();
                bool blnRes = clsError.LogError(objex.ToString());
            }
            return lngRes;
        }
#endregion

        #region ����Ƿ����ͬһ�����Ŷ����ͬ���ۼ�
        /// <summary>
        /// ����Ƿ����ͬһ�����Ŷ����ͬ���ۼ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDrugStoreID"></param>
        /// <param name="p_strMedicineID"></param>
        /// <param name="p_strLotno"></param>
        /// <param name="p_dblOpRetailPrice"></param>
        /// <param name="p_blnExist"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckDiffPrice( string p_strDrugStoreID, string p_strMedicineID, string p_strLotno, double p_dblOpRetailPrice, out bool p_blnExist)            
        {
            p_blnExist = false;
            DataTable dtValue = new DataTable();
            long lngRes = 0;
            try
            {

                string strSQL = @"select a.lotno_vchr
  from t_ds_storage_detail a
 where a.drugstoreid_chr = ?
   and a.medicineid_chr = ?
   and a.lotno_vchr = ?
   and a.opretailprice_int <> ?
   and a.status = 1";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                System.Data.IDataParameter[] objParm = null;
                objHRPServ.CreateDatabaseParameter(4, out objParm);
                objParm[0].Value = p_strDrugStoreID;
                objParm[1].Value = p_strMedicineID;
                objParm[2].Value = p_strLotno;
                objParm[3].Value = p_dblOpRetailPrice;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtValue, objParm);
                objHRPServ.Dispose();
                objHRPServ = null;
                if(dtValue != null && dtValue.Rows.Count > 0)
                {
                    p_blnExist = true;
                }
            }
            catch(Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
    }

}
