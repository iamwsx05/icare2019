using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
//using weCare.Core.Entity;
//using com.digitalwave.iCare.middletier.PatientSvc;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// סԺ�Ź���
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBeINpatientNOSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        //����סԺ��

        #region ��ͨ��Ժ�����жϡ���ʷ����סԺ�ż�¼�������Ƿ����סԺ�ţ����У�ȡ��С��һ�����룬����ȡ��ȫ���ֶΡ�������+1������6λסԺ��
        /// <summary>
        /// ��ͨ��Ժ�������סԺ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strHead">ͷ�ַ�</param>
        /// <param name="m_strMaxNo">�������ֵ</param>
        /// <param name="m_intSour">����1,���ֵ��Դ����ʷ��¼��2��Դ�����ֵ,0����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBigPatientIDNor(out string m_strHead, out string m_strMaxNo, out int m_intSour)
        {
            m_strHead = "";
            m_strMaxNo = "";
            m_intSour = 0;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;

                //��ȡ��ǰ ��ͨסԺ��ͷ��ʶ
                string strSQL = @" select a.firstflag_vchr from t_opr_bih_inpatientnomax a where a.flag_int=? ";
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = 1;
                bool m_blNull = false;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {

                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    if (dtbResult.Rows[0]["firstflag_vchr"] == System.DBNull.Value)
                        m_blNull = true;
                }

                //��ȡ��ǰ ����ʷ����סԺ�ż�¼�� �� �Ƿ��� ��ͨסԺ�� ��ȡ��Сһ�����롣����6λסԺ��
                if (m_blNull)
                {
                    strSQL = @"select a.firstflag_vchr,a.maxinpatientno_vchr maxno from t_opr_bih_inpatientnohis a where a.firstflag_vchr is null order by to_number(a.maxinpatientno_vchr) asc ";
                }
                else
                {
                    strSQL = @"select a.firstflag_vchr,a.maxinpatientno_vchr maxno from t_opr_bih_inpatientnohis a where trim(a.firstflag_vchr)=? order by to_number(a.maxinpatientno_vchr) asc ";

                }
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strHead.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    m_strMaxNo = m_strHead.Trim() + dtbResult.Rows[0]["maxno"].ToString().Trim();
                    m_intSour = 1;
                    /**********/
                    /***********/
                    return lngRes;
                }

                //����ȡ��ȫ���ֶΡ�������+1������6λסԺ��
                // strSQL = @"select  a.firstflag_vchr,lpad(a.maxinpatientno_vchr+1,length(a.maxinpatientno_vchr),'0') maxno,length(a.maxinpatientno_vchr) maxLen FROM t_opr_bih_inpatientnomax a where a.flag_int=1 ";
                strSQL = @"select  a.firstflag_vchr,
                (case when
                length(to_char(a.maxinpatientno_vchr+1))>length(to_char(a.maxinpatientno_vchr))
                then
                lpad(a.maxinpatientno_vchr+1, length(a.maxinpatientno_vchr)+1,'0')
                else
                lpad(a.maxinpatientno_vchr+1, length(a.maxinpatientno_vchr),'0')
                end) maxno
                FROM t_opr_bih_inpatientnomax a where a.flag_int=1 ";

                lngRes = 0;
                dtbResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    string m_strMaxtemp = "";

                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    m_strMaxNo = m_strHead + dtbResult.Rows[0]["maxno"].ToString().Trim();
                    m_strMaxtemp = dtbResult.Rows[0]["maxno"].ToString().Trim();
                    m_intSour = 2;


                    /*��ֵ���е�������*/
                    bool m_blHave = false;
                    m_lngCheckInputNoHave(m_strMaxNo, out m_blHave);
                    while (m_blHave)
                    {
                        int m_IntLeng = m_strMaxtemp.Length;
                        m_strMaxtemp = Convert.ToString(int.Parse(m_strMaxtemp) + 1);
                        if (m_strMaxtemp.Length > m_IntLeng)
                        {
                            m_strMaxtemp = m_strMaxtemp.PadLeft(m_strMaxtemp.Length, "0".ToCharArray()[0]);
                        }
                        else
                        {
                            m_strMaxtemp = m_strMaxtemp.PadLeft(m_IntLeng, "0".ToCharArray()[0]);
                        }
                        m_lngCheckInputNoHave(m_strHead + m_strMaxtemp, out m_blHave);
                        // m_strMaxNo = Convert.ToString(int.Parse(dtbResult.Rows[0]["maxno"].ToString().Trim()) + 1);
                        m_strMaxNo = m_strHead + m_strMaxtemp;
                        // m_lngGetBigPatientIDNor( ref m_strHead, ref m_strMaxNo, out m_intSour);
                    }
                    /*<--------------------*/

                    return lngRes;
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

        #region ����ͨ��Ժ(����)�����жϡ���ʷ����סԺ�ż�¼�������Ƿ����סԺ�ţ����У�ȡ����ĸ����С��һ�����룬����ȡ�����ֶΡ�������+1������6λסԺ��
        /// <summary>
        /// ����ͨ��Ժ�������סԺ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strHead">ͷ��ĸ</param>
        /// <param name="m_strMaxNo">�������ֵ</param>
        /// <param name="m_intSour">����1,���ֵ��Դ����ʷ��¼��2��Դ�����ֵ,0����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBigPatientIDOth(out string m_strHead, out string m_strMaxNo, out int m_intSour)
        {

            m_strMaxNo = "";
            m_intSour = 0;
            m_strHead = "";
            long lngRes = 0;
            try
            {


                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                bool m_blNull = false;
                //��ȡ��ǰ ��ͨסԺ��ͷ��ʶ
                string strSQL = @" select a.firstflag_vchr from t_opr_bih_inpatientnomax a where a.flag_int=? ";
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = 2;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    if (dtbResult.Rows[0]["firstflag_vchr"] == System.DBNull.Value)
                        m_blNull = true;
                }

                //��ȡ��ǰ ����ʷ����סԺ�ż�¼�� �� �Ƿ��� ��ͨסԺ�� ��ȡ��Сһ�����롣����6λסԺ��
                if (m_blNull)
                {
                    strSQL = @"select a.firstflag_vchr,a.maxinpatientno_vchr maxno from t_opr_bih_inpatientnohis a where  a.firstflag_vchr is null order by to_number(a.maxinpatientno_vchr) asc ";
                }
                else
                {
                    strSQL = @"select a.firstflag_vchr,a.maxinpatientno_vchr maxno from t_opr_bih_inpatientnohis a where trim(a.firstflag_vchr)=?  order by to_number(a.maxinpatientno_vchr) asc ";
                }
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strHead.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    m_strMaxNo = m_strHead.Trim() + dtbResult.Rows[0]["maxno"].ToString().Trim();
                    m_intSour = 1;
                    /**********/
                    /***********/
                    return lngRes;
                }

                //����ȡ��ȫ���ֶΡ�������+1������6λסԺ��
                //strSQL = @" select a.firstflag_vchr,lpad(a.maxinpatientno_vchr+1,length(a.maxinpatientno_vchr),'0')  maxno FROM t_opr_bih_inpatientnomax a where a.flag_int=2 ";
                strSQL = @"select  a.firstflag_vchr,
                (case when
                length(to_char(a.maxinpatientno_vchr+1))>length(to_char(a.maxinpatientno_vchr))
                then
                lpad(a.maxinpatientno_vchr+1, length(a.maxinpatientno_vchr)+1,'0')
                else
                lpad(a.maxinpatientno_vchr+1, length(a.maxinpatientno_vchr),'0')
                end) maxno
                FROM t_opr_bih_inpatientnomax a where a.flag_int=2 ";

                lngRes = 0;
                dtbResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {

                    string m_strMaxtemp = "";
                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    m_strMaxNo = m_strHead + dtbResult.Rows[0]["maxno"].ToString().Trim();
                    m_strMaxtemp = dtbResult.Rows[0]["maxno"].ToString().Trim();
                    m_intSour = 2;


                    /*��ֵ���е�������*/
                    bool m_blHave = false;
                    m_lngCheckInputNoHave(m_strMaxNo, out m_blHave);
                    while (m_blHave)
                    {
                        int m_IntLeng = m_strMaxtemp.Length;
                        m_strMaxtemp = Convert.ToString(int.Parse(m_strMaxtemp) + 1);
                        //m_strMaxtemp = m_strMaxtemp.PadLeft(m_IntLeng, "0".ToCharArray()[0]);
                        if (m_strMaxtemp.Length > m_IntLeng)
                        {
                            m_strMaxtemp = m_strMaxtemp.PadLeft(m_strMaxtemp.Length, "0".ToCharArray()[0]);
                        }
                        else
                        {
                            m_strMaxtemp = m_strMaxtemp.PadLeft(m_IntLeng, "0".ToCharArray()[0]);
                        }
                        m_lngCheckInputNoHave(m_strHead + m_strMaxtemp, out m_blHave);
                        // m_strMaxNo = Convert.ToString(int.Parse(dtbResult.Rows[0]["maxno"].ToString().Trim()) + 1);
                        m_strMaxNo = m_strHead + m_strMaxtemp;
                        // m_lngGetBigPatientIDNor( ref m_strHead, ref m_strMaxNo, out m_intSour);
                    }
                    /*<--------------------*/

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

        //����סԺ��

        #region ����˹������סԺ�Ŵ�������ʷ����ȡ��ΪסԺ��
        /// <summary>
        ///����˹������סԺ�Ŵ�������ʷ����ȡ��ΪסԺ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strHead">ͷ�ַ�</param>
        /// <param name="m_strMaxNo">�������ֵ</param>
        /// <param name="m_intSour">����1,���ֵ��Դ����ʷ��¼��2��Դ�����ֵ,0����</param>
        /// <param name="inpatientno_vchr">�˹������סԺ��</param>
        /// <param name="flag_int">1������2����</param>
        /// <param name="count">��ʶ(0-û���ҵ�,1-��ʷ�����ҵ���¼)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBigPatientIDFree(out string m_strHead, out string m_strMaxNo, out int m_intSour, string inpatientno_vchr, int flag_int, out int count)
        {
            m_strHead = "";
            m_strMaxNo = "";
            m_intSour = 0;
            count = 0;
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                DataTable dtbResult = new DataTable();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;

                //��ȡ��ǰ ��ͨסԺ��ͷ��ʶ
                string strSQL = @" select a.firstflag_vchr from t_opr_bih_inpatientnomax a where a.flag_int=? ";
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = flag_int;
                bool m_blNull = false;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {

                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    if (dtbResult.Rows[0]["firstflag_vchr"] == System.DBNull.Value)
                        m_blNull = true;
                }
                if (inpatientno_vchr.Length > m_strHead.Length)
                {
                    m_strMaxNo = inpatientno_vchr.Substring(m_strHead.Length);
                }
                //��ȡ��ǰ ����ʷ����סԺ�ż�¼�� �� �Ƿ��� ���������Ժ���Ƿ���ͬ��

                strSQL = @"select a.firstflag_vchr,a.maxinpatientno_vchr maxno from t_opr_bih_inpatientnohis a where (trim(a.firstflag_vchr)=? or a.firstflag_vchr is null) and a.maxinpatientno_vchr=? ";

                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strHead.Trim();
                objLisAddItemRefArr[1].Value = m_strMaxNo.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    m_strMaxNo = m_strHead.Trim() + dtbResult.Rows[0]["maxno"].ToString().Trim();
                    m_intSour = 1;
                    count = 1;
                    /**********/
                    /***********/
                    return lngRes;
                }
                else if (lngRes > 0 && dtbResult.Rows.Count == 0)
                {
                    m_intSour = 0;
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


        #region ����/����סԺ�ŵ����ֵ��
        /// <summary>
        /// ����/����סԺ�ŵ����ֵ��(������)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="inpatientnotype_int">1-������2-����</param>
        /// <param name="m_strMain">��������</param>
        /// <returns></returns>  
        [AutoComplete]
        public long m_lngAddBigIDTableMax(int inpatientnotype_int, string m_strMain)
        {

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtbResult = new DataTable();
                string strSQL = "";

                //�������ֵ
                // strSQL = @"insert into  t_opr_bih_inpatientnohis (seqid_int,firstflag_vchr,maxinpatientno_vchr) values(seq_public.nextval,'1','000012') ";
                if (inpatientnotype_int == 1)
                {
                    strSQL = @"update  t_opr_bih_inpatientnomax  set  maxinpatientno_vchr=? where flag_int=1 ";
                }
                else if (inpatientnotype_int == 2)
                {
                    strSQL = @"update  t_opr_bih_inpatientnomax set  maxinpatientno_vchr=? where flag_int=2 ";
                }
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                //objLisAddItemRefArr[0].Value = m_strHead.Trim();
                objLisAddItemRefArr[0].Value = m_strMain.Trim();

                //objLisAddItemRefArr[1].Value = seqid_int;
                dtbResult = new DataTable();
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
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

        /// <summary>
        /// ����/����סԺ�ŵ����ֵ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="inpatientnotype_int">�Ǽ�����(1-����,2-����)</param>
        /// <param name="m_strHead">סԺ��ͷ��ʶ</param>
        /// <param name="m_strMain">סԺ�����ֲ���</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddBigIDTableMax(int inpatientnotype_int, string m_strHead, string m_strMain)
        {

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtbResult = new DataTable();
                string strSQL = "";

                //�������ֵ
                // strSQL = @"insert into  t_opr_bih_inpatientnohis (seqid_int,firstflag_vchr,maxinpatientno_vchr) values(seq_public.nextval,'1','000012') ";
                if (inpatientnotype_int == 1)
                {
                    strSQL = @"update  t_opr_bih_inpatientnomax  set  maxinpatientno_vchr=? where flag_int=1 ";
                }
                else if (inpatientnotype_int == 2)
                {
                    strSQL = @"update  t_opr_bih_inpatientnomax set  maxinpatientno_vchr=? where flag_int=2 ";
                }
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                //objLisAddItemRefArr[0].Value = m_strHead.Trim();
                if (m_strHead.Trim().Equals(""))
                {
                    objLisAddItemRefArr[0].Value = m_strMain.Trim();
                }
                else
                {
                    objLisAddItemRefArr[0].Value = m_strMain.Trim().Replace(m_strHead.Trim(), "");
                }

                //objLisAddItemRefArr[1].Value = seqid_int;
                dtbResult = new DataTable();
                objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
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

        /// <summary>
        /// ����/����סԺ�ŵ����ֵ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="inpatientnotype_int">�Ǽ�����(1-����,2-����)</param>
        /// <param name="m_strHead">סԺ��ͷ��ʶ</param>
        /// <param name="m_strMain">סԺ�����ֲ���</param>
        /// <param name="m_intSour">1,���ֵ��Դ����ʷ��¼��2��Դ�����ֵ,0����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddBigIDTableMax(int inpatientnotype_int, string m_strHead, string m_strMain, int m_intSour)
        {

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtbResult = new DataTable();
                string strSQL = "";


                if (m_intSour == 2) //�������ֵ
                {
                    if (inpatientnotype_int == 1)
                    {
                        strSQL = @"update  t_opr_bih_inpatientnomax  set  maxinpatientno_vchr=? where flag_int=1 ";
                    }
                    else if (inpatientnotype_int == 2)
                    {
                        strSQL = @"update  t_opr_bih_inpatientnomax set  maxinpatientno_vchr=? where flag_int=2 ";
                    }

                    objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                    /*
                    if (m_strHead.Trim().Equals(""))
                    {
                        objLisAddItemRefArr[0].Value = m_strMain.Trim();
                    }
                    else
                    {
                        objLisAddItemRefArr[0].Value = m_strMain.Trim().Replace(m_strHead.Trim(), "");
                    }
                     */
                    objLisAddItemRefArr[0].Value = m_strMain.Trim().Substring(m_strHead.Trim().Length);


                    //objLisAddItemRefArr[1].Value = seqid_int;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
                }
                else if (m_intSour == 1)//������ʷ��
                {
                    lngRes = m_lngDelInpatientNohis( m_strHead, m_strMain);
                }
                else
                {
                    lngRes = 1;
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

        #region ɾ��"��Դ����ʷ��¼"�ٴ����õ�סԺ��
        /// <summary>
        /// ɾ��"��Դ����ʷ��¼"�ٴ����õ�סԺ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strHead">ͷ��ĸ</param>
        /// <param name="m_strMain">��������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelInpatientNohis(string m_strHead, string m_strMain)
        {
            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                string strSQL = "";
                //��ǰ�Ƿ���ڵ�ǰ��ĸ��ͷ�����סԺ�ţ�������и��£������½�
                strSQL = @" delete  FROM t_opr_bih_inpatientnohis a where (a.firstflag_vchr=? or a.firstflag_vchr is null) and  a.maxinpatientno_vchr=? ";
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strHead.Trim();
                if (!m_strHead.Trim().Equals(""))
                {
                    objLisAddItemRefArr[1].Value = m_strMain.Replace(m_strHead, "").Trim();
                }
                else
                {
                    objLisAddItemRefArr[1].Value = m_strMain.Trim();
                }

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);

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

        //����סԺ��
        #region ������Ժ������סԺ�š����롰��ʷ����סԺ�ż�¼�����Ա��Ժ���
        /// <summary>
        /// ����סԺ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="inpatientnotype_int">1-������2-����</param>
        /// <param name="m_strInpatientid_chr">סԺ��</param>
        /// <returns></returns>
        public long m_lngAddBigIDTableHis(int inpatientnotype_int, string m_strInpatientid_chr)
        {

            long lngRes = 0;

            string m_strHead = "";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtbResult = new DataTable();
                //��������������־
                string strSQL = @" select a.firstflag_vchr from t_opr_bih_inpatientnomax a where a.flag_int=?";
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = inpatientnotype_int;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_strHead = dtbResult.Rows[0]["firstflag_vchr"].ToString().Trim();
                    string m_strMain = m_strInpatientid_chr.Trim();
                    if (!m_strHead.Trim().Equals(""))
                    {
                        m_strMain = m_strMain.Replace(m_strHead.Trim(), "");
                    }
                    try
                    {
                        //((char[])m_strMain.ToCharArray()).is
                        int k = int.Parse(m_strMain);
                    }
                    catch
                    {
                        return lngRes;
                    }
                    //����סԺ�ǼǱ� ��סԺ�ż���*
                    //strSQL = @" update t_opr_bih_register a set a.inpatientid_chr='*'||a.inpatientid_chr where a.inpatientid_chr=?  ";
                    // objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                    //objLisAddItemRefArr[0].Value = m_strInpatientid_chr.Trim();
                    //lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
                    //if (lngRes > 0 && dtbResult.Rows.Count > 0)
                    //{

                    strSQL = @"insert into  t_opr_bih_inpatientnohis  (SEQID_INT,firstflag_vchr,maxinpatientno_vchr) values(seq_public.nextval,?,?)";
                    objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_strHead;
                    objLisAddItemRefArr[1].Value = m_strMain;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);



                    //}
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

        #region ���ۺ�תסԺ�� / ���ۺ�-->ת���ۺ�
        /// <summary>
        /// ���ۺ�--��סԺ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strRegisterid_chr">ԭ�Ǽ���ˮ��</param>
        /// <param name="oldinpatientnotype_int">�ɵǼ�����(1-����,2-����)</param>
        /// <param name="newinpatientnotype_int">�µǼ�����(1-����,2-����)</param>
        /// <param name="m_strHead">��סԺ��ͷ��ʶ</param>
        /// <param name="m_strMain">��סԺ�����ֲ���</param>
        /// <param name="m_intSour">1,�����ֵ��Դ����ʷ��¼��2��Դ�����ֵ,0����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangePatientIDNor(string m_strRegisterid_chr, string oldInpatientid_chr, int oldinpatientnotype_int, int newinpatientnotype_int, string m_strHead, string m_strMain, int m_intSour)
        {

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtbResult = new DataTable();
                //���µ�ǰ�����ۺű��סԺ�ţ����޸�INPATIENTNOTYPE_INT��ʶ
                string strSQL = "";
                strSQL = @"update t_opr_bih_register set  inpatientid_chr=?, INPATIENTNOTYPE_INT=? where registerid_chr=? ";

                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strHead.Trim() + m_strMain.Trim();
                objLisAddItemRefArr[1].Value = newinpatientnotype_int;
                objLisAddItemRefArr[2].Value = m_strRegisterid_chr.Trim();


                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
                if (lngRes > 0)
                {
                    if (m_intSour == 2) //�������ֵ
                    {
                        if (newinpatientnotype_int == 1)
                        {
                            strSQL = @"update  t_opr_bih_inpatientnomax  set  maxinpatientno_vchr=? where flag_int=1 ";
                        }
                        else if (newinpatientnotype_int == 2)
                        {
                            strSQL = @"update  t_opr_bih_inpatientnomax set  maxinpatientno_vchr=? where flag_int=2 ";
                        }

                        objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                        //objLisAddItemRefArr[0].Value = m_strHead.Trim();
                        if (m_strHead.Trim().Equals(""))
                        {
                            objLisAddItemRefArr[0].Value = m_strMain.Trim();
                        }
                        else
                        {
                            objLisAddItemRefArr[0].Value = m_strMain.Trim().Replace(m_strHead.Trim(), "");
                        }
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);

                        //select  a.inpatientid_chr from t_bse_patient a 
                    }
                    else if (m_intSour == 1)//������ʷ��
                    {
                        lngRes = m_lngDelInpatientNohis( m_strHead, m_strMain);
                    }
                    else
                    {
                        lngRes = 1;
                    }

                    if (lngRes > 0)
                    {
                        //ͬ�����˻�����Ϣ��
                        strSQL = @"update t_bse_patient a set a.inpatientid_chr=? where a.patientid_chr=(select  patientid_chr from  t_opr_bih_register where  registerid_chr=?)  ";
                        objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                        if (m_strHead.Trim().Equals(""))
                        {
                            objLisAddItemRefArr[0].Value = m_strMain.Trim();
                            objLisAddItemRefArr[1].Value = m_strRegisterid_chr.Trim();
                        }
                        else
                        {
                            objLisAddItemRefArr[0].Value = m_strMain.Trim().Replace(m_strHead.Trim(), "");
                            objLisAddItemRefArr[1].Value = m_strRegisterid_chr.Trim();
                        }
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
                        /*<---------------------------------------------------------------*/

                    }
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

        #region סԺ��ת���ۺ� / סԺ��-->תסԺ��
        /// <summary>
        /// סԺ��-->ת���ۺ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strRegisterid_chr">ԭ�Ǽ���ˮ��</param>
        /// <param name="oldInpatientid_chr">ԭסԺ��</param>
        /// <param name="oldinpatientnotype_int">�ɵǼ�����(1-����,2-����)</param>
        /// <param name="newinpatientnotype_int">�µǼ�����(1-����,2-����)</param>
        /// <param name="m_strHead">��סԺ��ͷ��ʶ</param>
        /// <param name="m_strMain">��סԺ�����ֲ���</param>
        /// <param name="m_intSour">1,�����ֵ��Դ����ʷ��¼��2��Դ�����ֵ,0����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChangePatientIDOth(string m_strRegisterid_chr, string oldInpatientid_chr, int oldinpatientnotype_int, int newinpatientnotype_int, string m_strHead, string m_strMain, int m_intSour)
        {

            long lngRes = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtbResult = new DataTable();
                //������Ժ������סԺ�š����롰��ʷ����סԺ�ż�¼�����Ա��Ժ���
                lngRes = m_lngAddBigIDTableHis( oldinpatientnotype_int, oldInpatientid_chr);
                /*<---------------------------------------------------------------*/
                if (lngRes > 0)
                {
                    //���µ�ǰ��סԺ�ű�����ۺţ����޸�INPATIENTNOTYPE_INT��ʶ
                    string strSQL = "";

                    strSQL = @"update t_opr_bih_register set  inpatientid_chr=?, INPATIENTNOTYPE_INT=? where registerid_chr=? ";
                    objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_strHead.Trim() + m_strMain.Trim();
                    objLisAddItemRefArr[1].Value = newinpatientnotype_int;
                    objLisAddItemRefArr[2].Value = m_strRegisterid_chr.Trim();
                    lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
                    if (lngRes > 0)
                    {
                        if (m_intSour == 2) //�������ֵ
                        {
                            if (newinpatientnotype_int == 1)
                            {
                                strSQL = @"update  t_opr_bih_inpatientnomax  set  maxinpatientno_vchr=? where flag_int=1 ";
                            }
                            else if (newinpatientnotype_int == 2)
                            {
                                strSQL = @"update  t_opr_bih_inpatientnomax set  maxinpatientno_vchr=? where flag_int=2 ";
                            }

                            objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                            //objLisAddItemRefArr[0].Value = m_strHead.Trim();
                            if (m_strHead.Trim().Equals(""))
                            {
                                objLisAddItemRefArr[0].Value = m_strMain.Trim();
                            }
                            else
                            {
                                objLisAddItemRefArr[0].Value = m_strMain.Trim().Replace(m_strHead.Trim(), "");
                            }

                            //objLisAddItemRefArr[1].Value = seqid_int;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
                        }
                        else if (m_intSour == 1)//������ʷ��
                        {
                            lngRes = m_lngDelInpatientNohis( m_strHead, m_strMain);
                        }
                        else
                        {
                            lngRes = 1;
                        }
                        if (lngRes > 0)
                        {
                            //ͬ�����˻�����Ϣ��
                            strSQL = @"update t_bse_patient a set a.inpatientid_chr=? where a.patientid_chr=(select  patientid_chr from  t_opr_bih_register where  registerid_chr=?)  ";
                            objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                            if (m_strHead.Trim().Equals(""))
                            {
                                objLisAddItemRefArr[0].Value = m_strMain.Trim();
                                objLisAddItemRefArr[1].Value = m_strRegisterid_chr.Trim();
                            }
                            else
                            {
                                objLisAddItemRefArr[0].Value = m_strMain.Trim().Replace(m_strHead.Trim(), "");
                                objLisAddItemRefArr[1].Value = m_strRegisterid_chr.Trim();
                            }
                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRes, objLisAddItemRefArr);
                            /*<---------------------------------------------------------------*/

                        }
                    }
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

        #region �жϿ���סԺ�Ų��ж�¼�뿪��
        /// <summary>
        /// ҽ��¼��Ȩ�޷������޿���,����0-�أ�1-����(������)
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckInputNoOpen(out bool m_blnOpen)
        {
            m_blnOpen = false;
            long lngRes = -1;
            string strSQL = "select a.setstatus_int from t_sys_setting a where a.setid_chr='1004'";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtbResult.Rows.Count > 0 && dtbResult.Rows[0][0] != System.DBNull.Value)
                {
                    m_blnOpen = (Int32.Parse(dtbResult.Rows[0][0].ToString()) == 1) ? true : false;
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

        #region ����סԺ������ʽ
        /// <summary>
        /// ����סԺ������ʽ   ^[A-Za-z]{1}[0-9]{5}$
        /// </summary>
        /// <param name="m_strRegex"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_strInputNoRegex(out string m_strRegexNor, out string m_strRegexOth)
        {

            long lngRes = -1;
            m_strRegexNor = @"^[A-Z]{1}[0-9]{5}$";
            m_strRegexOth = @"^[0-9]{6}$";
            //string strSQL = "select a.setstatus_int from t_sys_setting a where a.setid_chr='1004'";
            //try
            //{
            //    DataTable dtbResult = new DataTable();
            //    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            //    objHRPSvc.Dispose();
            //    if (lngRes > 0 && dtbResult.Rows.Count > 0 && dtbResult.Rows[0][0] != System.DBNull.Value)
            //    {
            //        m_blnOpen = (Int32.Parse(dtbResult.Rows[0][0].ToString()) == 1) ? true : false;
            //    }
            //}
            //catch (Exception objEx)
            //{
            //    string strTmp = objEx.Message;
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);
            //}
            return lngRes;
        }
        #endregion

        #region �жϵ�ǰ�ǼǱ��Ƿ��Ѵ�����ͬ��סԺ��
        /// <summary>
        ///  �жϵ�ǰ�ǼǱ��Ƿ��Ѵ�����ͬ��סԺ��
        /// </summary>
        /// <param name="inpatientid_chr"></param>
        /// <param name="m_blnHave">true-���ڣ�false-������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckInputNoHave(string inpatientid_chr, out bool m_blnHave)
        {
            m_blnHave = true;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngRes = 0;
            try
            {


                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtbResult = new DataTable();
                string strSQL = @"select  a.inpatientid_chr from t_opr_bih_register a where a.inpatientid_chr=? ";
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = inpatientid_chr.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    m_blnHave = true;
                }
                else
                {
                    m_blnHave = false;
                }
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

        #region �жϵ�ǰ�Ƿ���Ժϲ���ǰ������סԺ����
        /// <summary>
        /// �жϵ�ǰ�Ƿ���Ժϲ���ǰ������סԺ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="oldInpatientid_chr">��סԺ��</param>
        /// <param name="newInpatientid_chr">��סԺ��</param>
        /// <param name="m_blnHave">�Ƿ���Ժϲ�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckInputNoCombo(string oldInpatientid_chr, string newInpatientid_chr, out bool m_blnHave)
        {
            m_blnHave = false;
            long lngRes = 0;
            string name1 = "", name2 = "", sex1 = "", sex2 = "", modify_dat1 = "", modify_dat2 = "";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                DataTable dtbResult = new DataTable();
                string strSQL = @"select b.lastname_vchr,
                b.sex_chr,
                (select max(c.MODIFY_DAT)
                from t_opr_bih_transfer c, t_opr_bih_register a
                where c.registerid_chr = a.registerid_chr
                and c.type_int = 6
                and c.registerid_chr=?
                ) MODIFY_DAT
                from t_opr_bih_register a, t_bse_patient b
                where a.patientid_chr = b.patientid_chr
                and a.registerid_chr=? ";
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = oldInpatientid_chr.Trim();
                objLisAddItemRefArr[1].Value = oldInpatientid_chr.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    name1 = dtbResult.Rows[0]["lastname_vchr"].ToString().Trim();
                    sex1 = dtbResult.Rows[0]["sex_chr"].ToString().Trim();
                    modify_dat1 = dtbResult.Rows[0]["MODIFY_DAT"].ToString().Trim();
                }

                strSQL = @"select b.lastname_vchr,
                b.sex_chr,
                a.INPATIENT_DAT 
                from t_opr_bih_register a, t_bse_patient b
                where a.patientid_chr = b.patientid_chr
                and a.registerid_chr=? ";
                objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = newInpatientid_chr.Trim();
                objLisAddItemRefArr[1].Value = newInpatientid_chr.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    name2 = dtbResult.Rows[0]["lastname_vchr"].ToString().Trim();
                    sex2 = dtbResult.Rows[0]["sex_chr"].ToString().Trim();
                    modify_dat2 = dtbResult.Rows[0]["INPATIENT_DAT"].ToString().Trim();
                }


                if (sex1.Equals(sex2) && name1.Equals(name2))
                {
                    try
                    {

                        if (Convert.ToDateTime(modify_dat1).CompareTo(Convert.ToDateTime(modify_dat2)) > 0)
                        {

                            m_blnHave = true;
                        }
                    }
                    catch
                    {
                    }
                }
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

        #region 

        [AutoComplete]
        public long m_lngGetNativeplace(string m_strFindCode, out DataTable m_dtResult)
        {

            long lngRes = 0;
            m_dtResult = new DataTable();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;

                //��ȡ��ǰ ��ͨסԺ��ͷ��ʶ
                string strSQL = @" 
                SELECT   a.dictname_vchr, a.pycode_chr, a.wbcode_chr
                FROM t_aid_dict a
                WHERE dictid_chr != '0' AND dictkind_chr = '3'
                and (a.dictname_vchr like ? or a.pycode_chr like ? or wbcode_chr like ?)
                ORDER BY TO_NUMBER (dictid_chr)
                ";
                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(3, out arrParams);
                arrParams[0].Value = m_strFindCode.Trim() + "%";
                arrParams[1].Value = m_strFindCode.Trim() + "%";
                arrParams[2].Value = m_strFindCode.Trim() + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtResult, arrParams);
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
