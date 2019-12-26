using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// ����ͨѶ�����м��
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMK3DeviceCommunications : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        const string strClassName = "com.digitalwave.iCare.middletier.LIS.clsMK3DeviceCommunications";

        #region ��ȡ�Զ�����Ŀ�ķ�������
        /// <summary>
        /// ��ȡ�Զ�����Ŀ�ķ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <param name="p_objCheckItemCustomOrderArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryChcekItemCustomOrder(string p_strCheckItemID, out clsLisCheckItemCustomOrder p_objCheckItemCustomOrder)
        {
            p_objCheckItemCustomOrder = null;
            long lngRes = 0;
            string strSQL = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"select t.air_blank_chr,
       t.jin_plate_way_chr,
       t.shock_speed_chr,
       t.shock_time_chr,
       t.wavelength_chr,
       t.filter_chr,
       t.deputy_filter_chr
  from t_bse_lis_check_item_cutomorde t
 where t.check_item_id_chr = ?
";
                IDataParameter[] objDPArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strCheckItemID;
                DataTable dtResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    DataRow drTemp = dtResult.Rows[0];
                    p_objCheckItemCustomOrder = new clsLisCheckItemCustomOrder();
                    p_objCheckItemCustomOrder.m_strAir_blank = drTemp["air_blank_chr"].ToString().Trim();
                    p_objCheckItemCustomOrder.m_strJin_plate_way_chr = drTemp["jin_plate_way_chr"].ToString().Trim();
                    p_objCheckItemCustomOrder.m_strShock_speed_chr = drTemp["shock_speed_chr"].ToString().Trim();
                    p_objCheckItemCustomOrder.m_strShock_time_chr = drTemp["shock_time_chr"].ToString().Trim();
                    p_objCheckItemCustomOrder.m_strWavelength_chr = drTemp["wavelength_chr"].ToString().Trim();
                    p_objCheckItemCustomOrder.m_strFilter_chr = drTemp["filter_chr"].ToString().Trim();
                    p_objCheckItemCustomOrder.m_strDeputy_filter_chr = drTemp["deputy_filter_chr"].ToString().Trim();
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
            }
            return lngRes;
        }
        #endregion

        #region �޸��Զ�����Ŀ���͸�����������
        /// <summary>
        /// �޸��Զ�����Ŀ���͸�����������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckItemCustomOrderVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateChcekItemCustomOrder(clsLisCheckItemCustomOrder p_objCheckItemCustomOrderVO)
        {
            long lngRes = 0;
            string strSQL = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"update t_bse_lis_check_item_cutomorde t
   set t.air_blank_chr     = ?,
       t.jin_plate_way_chr = ?,
       t.shock_speed_chr   = ?,
       t.shock_time_chr    = ?,
       t.wavelength_chr    = ?,
       t.filter_chr        = ?,
       t.deputy_filter_chr = ?
 where t.check_item_id_chr = ?

";
                IDataParameter[] objDPArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(8, out objDPArr);
                objDPArr[0].Value = p_objCheckItemCustomOrderVO.m_strAir_blank;
                objDPArr[1].Value = p_objCheckItemCustomOrderVO.m_strJin_plate_way_chr;
                objDPArr[2].Value = p_objCheckItemCustomOrderVO.m_strShock_speed_chr;
                objDPArr[3].Value = p_objCheckItemCustomOrderVO.m_strShock_time_chr;
                objDPArr[4].Value = p_objCheckItemCustomOrderVO.m_strWavelength_chr;
                objDPArr[5].Value = p_objCheckItemCustomOrderVO.m_strFilter_chr;
                objDPArr[6].Value = p_objCheckItemCustomOrderVO.m_strDeputy_filter_chr;
                objDPArr[7].Value = p_objCheckItemCustomOrderVO.m_strCheckItemID;
                long lngEff = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
            }
            return lngRes;
        }
        #endregion

        #region ����Զ�����Ŀ�ķ�������
        /// <summary>
        /// ����Զ�����Ŀ�ķ�������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objCheckItemCustomOrderVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertCheckItemCustomOrder(clsLisCheckItemCustomOrder p_objCheckItemCustomOrderVO)
        {
            long lngRes = 0;
            string strSQL = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"insert into t_bse_lis_check_item_cutomorde
  (check_item_id_chr,
   air_blank_chr,
   jin_plate_way_chr,
   shock_speed_chr,
   shock_time_chr,
   wavelength_chr,
   filter_chr,
   deputy_filter_chr)
values
  (?, ?, ?, ?, ?, ?, ?, ?)
";
                IDataParameter[] objDPArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(8, out objDPArr);
                objDPArr[0].Value = p_objCheckItemCustomOrderVO.m_strCheckItemID;
                objDPArr[1].Value = p_objCheckItemCustomOrderVO.m_strAir_blank;
                objDPArr[2].Value = p_objCheckItemCustomOrderVO.m_strJin_plate_way_chr;
                objDPArr[3].Value = p_objCheckItemCustomOrderVO.m_strShock_speed_chr;
                objDPArr[4].Value = p_objCheckItemCustomOrderVO.m_strShock_time_chr;
                objDPArr[5].Value = p_objCheckItemCustomOrderVO.m_strWavelength_chr;
                objDPArr[6].Value = p_objCheckItemCustomOrderVO.m_strFilter_chr;
                objDPArr[7].Value = p_objCheckItemCustomOrderVO.m_strDeputy_filter_chr;
                long lngEff = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
            }
            return lngRes;
        }
        #endregion

        #region ɾ���Զ�����Ŀ������
        /// <summary>
        /// ɾ���Զ�����Ŀ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strCheckItemID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteCheckItemCustomOrder(string p_strCheckItemID)
        {
            long lngRes = 0;
            string strSQL = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"delete t_bse_lis_check_item_cutomorde t where t.check_item_id_chr = ?";
                IDataParameter[] objDPArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strCheckItemID;
                long lngEff = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
            }
            return lngRes;
        }
        #endregion

    }
}
