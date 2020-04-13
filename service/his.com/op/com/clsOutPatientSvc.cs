using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.HIS
{
    #region �����շѺ��������ɱ���ҵ�����
    /// <summary>	
    /// �����շѺ��������ɱ���ҵ�����
    /// Create ��ΰ�� by 2005-09-13
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsOutPatientSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsOutPatientSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region �м�������������շѺ��������ɱ���ҵ�����

        #region ���ϵͳʱ��
        /// <summary>
        /// ��ȡ���ݿ������ʱ��
        /// </summary>
        /// <returns>DateTime</returns>
        [AutoComplete]
        public DateTime m_dtmGetServerDate()
        {
            long lngRes = 0;
            System.DateTime datResult = System.DateTime.Now;

            string strSQL = @"SELECT sysdate
							  FROM dual";
            System.Data.DataTable dtbResult = new System.Data.DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    datResult = System.DateTime.Parse(dtbResult.Rows[0]["sysdate"].ToString());

                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return datResult;
        }
        #endregion

        #region ��ʱ��ͳ���շ� 
        /// <summary>
        /// ��ʱ��ͳ���շ�
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_dtm1">��ʼʱ��</param>
        /// <param name="p_dtm2">����ʱ��</param>
        /// <param name="p_dtb">��ѯ�õ��Ľ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStatiticsData(string p_dtm1, string p_dtm2, out DataTable p_dtb)
        {
            p_dtb = new DataTable();
            long lngRes = 0;

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT t1.*, t2.typename_vchr
									FROM (SELECT   b.itemcatid_chr, SUM (b.tolfee_mny) AS totalmoney
											 FROM t_opr_outpatientrecipesumde b, t_opr_outpatientrecipeinv a
											 WHERE b.seqid_chr = a.seqid_chr
													 AND a.recorddate_dat BETWEEN to_date('" + p_dtm1 + " 00:00:00" + "','yyyy-mm-dd hh24:mi:ss')";
            strSQL += "	AND to_date('" + p_dtm2 + " 00:00:00" + "','yyyy-mm-dd hh24:mi:ss')";
            strSQL += " AND a.balanceflag_int = 1";
            strSQL += " GROUP BY b.itemcatid_chr) t1,";
            strSQL += " t_bse_chargeitemextype t2";
            strSQL += " WHERE t1.itemcatid_chr = t2.typeid_chr";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_dtb);
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

        #endregion

    }
    #endregion

}
