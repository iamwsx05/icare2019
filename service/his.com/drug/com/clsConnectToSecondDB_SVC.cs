using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.iCare.middletier.MedicineStoreService
{
    /// <summary>
    /// ҩ���м��������
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.NotSupported)]
    [ObjectPooling(true)]
    public class clsConnectToSecondDB_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region �����ڶ������ݿ������
        /// <summary>
        /// �����ڶ������ݿ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDBConfig">�����ַ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngEstablishConnection(string p_strDBConfig)
        {
            long lngRes = 0;

            try
            {   
                //20090421:��ȡ�ڶ������ݿ��Ƿ��ѽ�������
                if (p_strDBConfig.Length > 0)
                {
                    DataTable dtbTemp = new DataTable();

                    string strSQL = @"select count(*)
  from dba_db_links a
 where a.db_link = 'SECONDMSCONFIG.REGRESS.RDBMS.DEV.US.ORACLE.COM'
   and a.owner = 'PUBLIC'";

                    clsHRPTableService objHRPServ = new clsHRPTableService();
                    objHRPServ.lngGetDataTableWithoutParameters(strSQL, ref dtbTemp);

                    if (dtbTemp != null && dtbTemp.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtbTemp.Rows[0][0]) > 0)
                        {
                            //ɾ������
                            strSQL = @"DROP PUBLIC DATABASE LINK SECONDMSCONFIG";
                            objHRPServ.DoExcute(strSQL);
                        }
                    }

                    objHRPServ.DoExcute(p_strDBConfig);
                    objHRPServ.Dispose();
                    objHRPServ = null;
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
