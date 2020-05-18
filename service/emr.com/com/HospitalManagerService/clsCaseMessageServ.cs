using System;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;

namespace com.digitalwave.emr.HospitalManagerService
{
	/// <summary>
	/// clsCaseMessage ��ժҪ˵����
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsCaseMessageServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ��
        /// <summary>
        /// ����Ƿ��Ѿ��������ѵ����ݣ��������ڴ�����������
        /// </summary>
        /// <param name="p_strDeptOrAreaID">���һ��߲���id</param>
        /// <param name="p_intType">��1�����󷵻أ�0���������ڴ���1���Ѿ��������ݣ���δ����8Сʱ��2��û�����ݻ��������Ѿ�����8Сʱ�������´���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckMessageExist(string p_strDeptOrAreaID, ref int p_intType)
        {
            //			p_intType = -1;
            if (string.IsNullOrEmpty(p_strDeptOrAreaID)) return -1;
            long lngRes = 0;
            try
            {
                string strSql = @"select messageid_int,
       deptid_chr,
       registerid_chr,
       createdtime_dat,
       status_int
  from t_emr_message_all
 where deptid_chr = ?
   and registerid_chr is null
   and status_int = -1";
                clsHRPTableService objHrpServ = new clsHRPTableService();
                IDataParameter[] arrParam = null;
                objHrpServ.CreateDatabaseParameter(1, out arrParam);
                arrParam[0].Value = p_strDeptOrAreaID;
                DataTable dtResult = new DataTable();
                lngRes = objHrpServ.lngGetDataTableWithParameters(strSql, ref dtResult, arrParam);
                if (lngRes > 0)//��ѯʧ�ܣ����أ�1��ʾ��Ҫ���²�ѯ
                {
                    if (dtResult.Rows.Count == 0)
                    {
                        strSql = @"select max(createdtime_dat)
  from t_emr_message_all
 where deptid_chr = ?
   and status_int = 1";
                        arrParam = null;
                        objHrpServ.CreateDatabaseParameter(1, out arrParam);
                        arrParam[0].Value = p_strDeptOrAreaID;
                        dtResult = new DataTable();
                        lngRes = objHrpServ.lngGetDataTableWithParameters(strSql, ref dtResult, arrParam);
                        if (lngRes <= 0 || dtResult.Rows.Count == 0) //��ѯʧ�ܻ���û�����ݣ�����2��ʾ��Ҫ���²�ѯ
                            p_intType = 2;
                        else
                        {
                            if (dtResult.Rows[0][0] == DBNull.Value)
                                p_intType = 2; //û�����ݣ�����2��ʾ��Ҫ���²�ѯ
                            else
                            {
                                DateTime dtmResult = DateTime.Parse(dtResult.Rows[0][0].ToString());
                                if (dtmResult.AddHours(8) <= DateTime.Now)
                                    p_intType = 2;//����8Сʱ������2��ʾ��Ҫ���²�ѯ
                                else
                                    p_intType = 1;
                            }
                        }
                    }
                    else
                    {
                        p_intType = 0;//�������߳����ڴ���ͬһ�����ҵ����ݣ�����1��ʾҪ�ȴ��������
                    }
                }
                if (p_intType != 0 && p_intType != 1)
                    lngRes = m_lngClearOldMessage(p_strDeptOrAreaID);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            return 1;
        }
        /// <summary>
        /// �������Ѳ���������
        /// </summary>
        /// <param name="p_strDeptOrAreaID"></param>
        /// <param name="p_blnIsProcessDepartmentNow"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTodayMessage(string p_strDeptOrAreaID, bool p_blnIsProcessDepartmentNow, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strDeptOrAreaID)) return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHrpServ = new clsHRPTableService();

                if (!p_blnIsProcessDepartmentNow)
                {
                    if (clsHRPTableService.bytDatabase_Selector == 2)
                    {
                        IDataParameter[] arrParam = new IDataParameter[1];
                        objHrpServ.m_mthCreateParameterWithDbType("strDeptID", "VARCHAR", -1, out arrParam[0]);
                        arrParam[0].Value = p_strDeptOrAreaID;
                        lngRes = objHrpServ.lngExecuteProc("MESSAGEPACK.ProcessDepartmentNow", arrParam);
                    }
                    else
                    {
                        IDataParameter[] arrParam = new IDataParameter[1];
                        objHrpServ.m_mthCreateParameterWithDbType("@strDeptID", "VARCHAR", 20, out arrParam[0]);
                        arrParam[0].Value = p_strDeptOrAreaID;
                        lngRes = objHrpServ.lngExecuteProc("ProcessDepartmentNow", arrParam);

                    }
                }

                string strSql = @"select a.registerid_chr,
       a.createdtime_dat,
       re.extendid_vchr,
       b.cueid_int,
       b.messageid_int,
       b.cuetypeid_int,
       b.patientname_vchr,
       b.chargedocname_vchr,
       b.bedid_chr,
       b.bedname_vchr,
       b.cuestate_int,
       b.surplushour_int,
       b.timerangebegin_dat,
       b.timerangeend_dat,
       b.cuemessage_vchr
  from t_emr_message_all a
 inner join t_emr_messagecue b on a.messageid_int = b.messageid_int
 inner join t_opr_bih_register re on a.registerid_chr = re.registerid_chr
 where (a.status_int = 1)
   and a.deptid_chr = ?";
                IDataParameter[] arrParam2 = null;
                objHrpServ.CreateDatabaseParameter(1, out arrParam2);
                arrParam2[0].Value = p_strDeptOrAreaID;

                lngRes = objHrpServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, arrParam2);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            return lngRes;
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="p_strDeptOrAreaID"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngClearOldMessage(string p_strDeptOrAreaID)
        {
            if (p_strDeptOrAreaID == null || p_strDeptOrAreaID == "") return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHrpServ = new clsHRPTableService();
                if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    IDataParameter[] arrParam = new IDataParameter[1];
                    objHrpServ.m_mthCreateParameterWithDbType("strDeptID", "VARCHAR", -1, out arrParam[0]);
                    arrParam[0].Value = p_strDeptOrAreaID;
                    lngRes = objHrpServ.lngExecuteProc("MESSAGEPACK.ClearTable", arrParam);
                }
                else
                {
                    IDataParameter[] arrParam = new IDataParameter[1];
                    objHrpServ.m_mthCreateParameterWithDbType("@strDeptID", "VARCHAR", 20, out arrParam[0]);
                    arrParam[0].Value = p_strDeptOrAreaID;
                    lngRes = objHrpServ.lngExecuteProc("ClearTable", arrParam);

                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex,true);
            }
            return lngRes;
        }
        /// <summary>
        /// ��������֪ͨ�Ĳ��˲���������¼���ѱ�
        /// </summary>
        /// <param name="p_strDeptId"></param>
        /// <param name="p_dtbDeadOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetDeadRecordMessage(string p_strDeptId, DataTable p_dtbDeadOrder)
        {
            if (string.IsNullOrEmpty(p_strDeptId) || p_dtbDeadOrder == null || p_dtbDeadOrder.Rows.Count == 0) return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHrpServ = new clsHRPTableService();

                string strSql = @"delete from t_emr_messagecue_dead where deptid_chr = ?";//1

                IDataParameter[] arrParam = null;
                objHrpServ.CreateDatabaseParameter(1,out arrParam);
                arrParam[0].Value = p_strDeptId;
                long lngRff = 0;
                lngRes = objHrpServ.lngExecuteParameterSQL(strSql,ref lngRff,arrParam);
                if (lngRes > 0)
                {
                    strSql = @"insert into t_emr_messagecue_dead
     (deadcueid_int,deptid_chr,status_int)
   values (seq_emr_messagecue.nextval, ?, -1)";//2
                    arrParam = null;
                    objHrpServ.CreateDatabaseParameter(1, out arrParam);
                    arrParam[0].Value = p_strDeptId;
                    lngRff = 0;
                    lngRes = objHrpServ.lngExecuteParameterSQL(strSql, ref lngRff, arrParam);
                    {
                        strSql = @"insert into t_emr_messagecue_dead
     (deadcueid_int,
      deptid_chr,
      status_int,
      extendid_vchr,
      deadorderdate_dat)
   values
     (seq_emr_messagecue.nextval, ?, 1, ?, ?)";//4
                        DataRow objRow = null;
                        int intRowCount = p_dtbDeadOrder.Rows.Count;
                        if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                        {
                            for (int i = 0; i < intRowCount; i++)
                            {
                                objRow = p_dtbDeadOrder.Rows[i];
                                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                                objHrpServ.CreateDatabaseParameter(3, out objDPArr2);
                                objDPArr2[0].Value = p_strDeptId;
                                objDPArr2[1].Value = objRow["patient_id"].ToString() + "_" + objRow["visit_id"].ToString();
                                objDPArr2[2].DbType = DbType.DateTime;
                                objDPArr2[2].Value = DateTime.Parse(objRow["enter_date_time"].ToString());

                                lngRff = 0;
                                lngRes = objHrpServ.lngExecuteParameterSQL(strSql, ref lngRff, objDPArr2);

                            }
                        }
                        else
                        {
                            if (intRowCount > 0)
                            {
                                DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.DateTime };
                                object[][] objValues = new object[3][];
                                for (int j = 0; j < objValues.Length; j++)
                                {
                                    objValues[j] = new object[intRowCount];//��ʼ��
                                }

                                for (int k1 = 0; k1 < intRowCount; k1++)
                                {
                                    objRow = p_dtbDeadOrder.Rows[k1];
                                    objValues[0][k1] = p_strDeptId;
                                    objValues[1][k1] = objRow["patient_id"].ToString() + "_" + objRow["visit_id"].ToString();
                                    objValues[2][k1] = DateTime.Parse(objRow["enter_date_time"].ToString());
                                }
                                lngRes = objHrpServ.m_lngSaveArrayWithParameters(strSql, objValues, dbTypes);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex,true);
            }
            return lngRes;
        }
        /// <summary>
        /// ����������¼����
        /// </summary>
        /// <param name="p_strDeptOrAreaID"></param>
        /// <param name="p_blnIsProcessDepartmentNow"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngProcessCueDeadTable(string p_strDeptOrAreaID, bool p_blnIsProcessDepartmentNow, out DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (string.IsNullOrEmpty(p_strDeptOrAreaID)) return -1;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHrpServ = new clsHRPTableService();

                if (!p_blnIsProcessDepartmentNow)
                {
                    if (clsHRPTableService.bytDatabase_Selector == 2)
                    {
                        IDataParameter[] arrParam = new IDataParameter[1];
                        objHrpServ.m_mthCreateParameterWithDbType("strDeptID", "VARCHAR", -1, out arrParam[0]);
                        arrParam[0].Value = p_strDeptOrAreaID;
                        lngRes = objHrpServ.lngExecuteProc("MESSAGEPACK.ProcessCueDeadTable", arrParam);
                    }
                    else
                    {
                        IDataParameter[] arrParam = new IDataParameter[1];
                        objHrpServ.m_mthCreateParameterWithDbType("@strDeptID", "VARCHAR", 20, out arrParam[0]);
                        arrParam[0].Value = p_strDeptOrAreaID;
                        lngRes = objHrpServ.lngExecuteProc("ProcessCueDeadTable", arrParam);

                    }
                }

                string strSql = @"select deadcueid_int,
       deptid_chr,
       registerid_chr,
       patientname_vchr,
       chargedocname_vchr,
       bedid_chr,
       bedname_vchr,
       cuemessage_vchr,
       status_int,
       extendid_vchr,
       deadorderdate_dat
  from t_emr_messagecue_dead
 where deptid_chr = ?
   and status_int = 1";
                IDataParameter[] arrParam2 = null;
                objHrpServ.CreateDatabaseParameter(1, out arrParam2);
                arrParam2[0].Value = p_strDeptOrAreaID;

                lngRes = objHrpServ.lngGetDataTableWithParameters(strSql, ref p_dtbResult, arrParam2);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex);
            }
            return lngRes;
        }
        #endregion ��
    }
}
