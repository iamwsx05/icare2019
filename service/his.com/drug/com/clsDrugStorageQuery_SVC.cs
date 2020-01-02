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
    /// ҩ������ѯ��
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsDrugStorageQuery_SVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        
        #region ����ҩ��ҩƷ����
        /// <summary>
        /// ����ҩ��ҩƷ����
        /// </summary>
        /// <param name="p_objPrincipal">Ȩ��</param>
        /// <param name="p_dicStorageRack">����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveStorageRack(Dictionary<string,string> p_dicStorageRack)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_storage_detail a set a.storagerackid_chr = ?
                                    where a.medicineid_chr = ? and a.lotno_vchr = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                DbType[] dbTypes = new DbType[] { DbType.String, DbType.String,DbType.String };

                object[][] objValues = new object[3][];

                int intItemCount = p_dicStorageRack.Count;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��
                }
                
                int iRow = 0;
                foreach(KeyValuePair<string,string> kvp in p_dicStorageRack)
                {
                    objValues[0][iRow] = kvp.Value;
                    objValues[1][iRow] = kvp.Key.Substring(0,10);
                    objValues[2][iRow] = kvp.Key.Substring(10);
                    iRow++;
                }
                    
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region ����ɹ���־
        /// <summary>
        /// ����ɹ���־
        /// </summary>
        /// <param name="p_objPrincipal">Ȩ��</param>
        /// <param name="p_dtbModify">��Ҫ�޸�����������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveProvide( DataTable p_dtbModify)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_storage_detail a set canprovide_int = ? where a.seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Int64 };

                object[][] objValues = new object[2][];

                int intItemCount = p_dtbModify.Rows.Count;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��
                }

                for (int i1 = 0; i1 < p_dtbModify.Rows.Count; i1++)
                {
                    objValues[0][i1] = Convert.ToDouble(p_dtbModify.Rows[i1]["canprovide_int"]);
                    objValues[1][i1] = Convert.ToInt64(p_dtbModify.Rows[i1]["seriesid_int"]);
                }

                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region ����ȱҩ��ͣ��,����
        /// <summary>
        /// ����ȱҩ��ͣ��
        /// </summary>
        /// <param name="p_objPrincipal">Ȩ��</param>
        /// <param name="p_dtbModify">��Ҫ�޸�ȱҩ��ͣ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveStorageSet( DataTable p_dtbModify)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_storage a
	 set a.noqtyflag_int = ?,a.ifstop_int = ?,a.storagerackid_chr = ?
 where a.seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                DbType[] dbTypes = new DbType[] { DbType.Int16, DbType.Int16,DbType.String, DbType.Int64 };

                object[][] objValues = new object[4][];

                int intItemCount = p_dtbModify.Rows.Count;
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[intItemCount];//��ʼ��
                }

                for (int i1 = 0; i1 < p_dtbModify.Rows.Count; i1++)
                {
                    objValues[0][i1] = Convert.ToInt16(p_dtbModify.Rows[i1]["noqtyflag_int"].ToString());
                    objValues[1][i1] = Convert.ToInt16(p_dtbModify.Rows[i1]["ifstop_int"].ToString());
                    objValues[2][i1] = Convert.ToString(p_dtbModify.Rows[i1]["storagerackid_chr"]);
                    objValues[3][i1] = Convert.ToInt64(p_dtbModify.Rows[i1]["seriesid_int"]);
                }

                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region ��������ϸ���޸ļ�¼
        /// <summary>
        /// ��������ϸ���޸ļ�¼
        /// </summary>
        /// <param name="p_objPrincipal">Ȩ��</param>
        /// <param name="p_dtbModify">��Ҫ�޸�����������</param>
        /// <param name="p_strMakerID">�޸���ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveAmount( clsDS_StorageHistory_VO p_objHistory)
        {
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_ds_storage_detail a
	 set a.opavailablegross_num = ?,a.ipavailablegross_num = ? where a.seriesid_int = ?";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_objHistory.m_dblNEWOPAVAILABLEGROSS_NUM;
                objDPArr[1].Value = p_objHistory.m_dblNEWIPAVAILABLEGROSS_NUM;
                objDPArr[2].Value = p_objHistory.m_lngSERIESID2_INT;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

                //�����޸Ŀ��ÿ���log
                if (lngRes > 0)
                {
                    strSQL = @"insert into t_ds_storagedetail_history a
  (a.seriesid_int,
   a.seriesid2_int,
   a.drugstoreid_chr,
   a.ipavailablegross_num,
   a.newipavailablegross_num,
   a.ipunit_chr,
   a.opavailablegross_num,
   a.newopavailablegross_num,
   a.opunit_chr,
   a.modifydate_dat,
   a.modifyuserid_chr,a.medicineid_chr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, sysdate, ?,?)";
                    clsDS_Public_Supported_SVC objPubSvc = new clsDS_Public_Supported_SVC();
                    //long[] lngSEQArr = null;
                    //objPubSvc.m_lngGetSequenceArr( "SEQ_T_DS_STORAGEDETAIL_HISTORY", 1, out lngSEQArr);
                    objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(11, out objDPArr);
                    objDPArr[0].Value = objPubSvc.GetSeqNextVal("SEQ_T_DS_STORAGEDETAIL_HISTORY"); //lngSEQArr[0];
                    objDPArr[1].Value = p_objHistory.m_lngSERIESID2_INT;
                    objDPArr[2].Value = p_objHistory.m_strDRUGSTOREID_CHR;
                    objDPArr[3].Value = p_objHistory.m_dblIPAVAILABLEGROSS_NUM;
                    objDPArr[4].Value = p_objHistory.m_dblNEWIPAVAILABLEGROSS_NUM;
                    objDPArr[5].Value = p_objHistory.m_strIPUNIT_CHR;
                    objDPArr[6].Value = p_objHistory.m_dblOPAVAILABLEGROSS_NUM;
                    objDPArr[7].Value = p_objHistory.m_dblNEWOPAVAILABLEGROSS_NUM;
                    objDPArr[8].Value = p_objHistory.m_strOPUNIT_CHR;
                    objDPArr[9].Value = p_objHistory.m_strMODIFYUSERID_CHR;
                    objDPArr[10].Value = p_objHistory.m_strMEDICINEID_CHR;

                    lngEff = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);

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
    }
}
