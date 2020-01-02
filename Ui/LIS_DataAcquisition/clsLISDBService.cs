using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.DataService;
using System.Data;
using System.Data.OracleClient;

namespace com.digitalwave.iCare.LIS
{
    public class clsLISDBService
    {
        private string _strONLINE_MODULE_CHR;
        private string _strONLINE_DNS_VCHR;

        #region 联机方式
        /// <summary>
        /// 联机方式1=ORACLE,2=SQL,3=ADO,4=ODBC,5=TEXT
        /// </summary>
        public string M_strONLINE_MODULE_CHR
        {
            get
            {
                return _strONLINE_MODULE_CHR;
            }
            set
            {
                _strONLINE_MODULE_CHR = value;
            }
        } 
        #endregion

        #region 联机DNS
        /// <summary>
        /// 联机DNS
        /// </summary>
        public string M_strONLINE_DNS_VCHR
        {
            get
            {
                return _strONLINE_DNS_VCHR;
            }
            set
            {
                _strONLINE_DNS_VCHR = value;
            }
        } 
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsLISDBService()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_strONLINE_MODULE_CHR"></param>
        /// <param name="p_strONLINE_DNS_VCHR"></param>
        public clsLISDBService(string p_strONLINE_MODULE_CHR, string p_strONLINE_DNS_VCHR)
            : this()
        {
            _strONLINE_MODULE_CHR = p_strONLINE_MODULE_CHR;
            _strONLINE_DNS_VCHR = p_strONLINE_DNS_VCHR;
        } 
        #endregion

        #region 获取数据库具体操作类
        /// <summary>
        /// 获取数据库具体操作类
        /// </summary>
        /// <returns></returns>
        private IDataService GetDataServ()
        {
            if (string.IsNullOrEmpty(_strONLINE_MODULE_CHR))
                return null;

            IDataService objDataService = null;
            switch (_strONLINE_MODULE_CHR)
            {
                case "1":
                    objDataService = new clsDataService_Oracle();
                    break;
                case "2":
                    objDataService = new clsDataService_SQLServer();
                    break;
                case "3":
                    objDataService = null;//new clsDataService_ADO();
                    break;
                case "4":
                    objDataService = new clsDataService_ODBC();
                    break;
                default:
                    objDataService = null;
                    break;
            }
            return objDataService;
        } 
        #endregion


        #region Public Func

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="intParameterNum"></param>
        /// <param name="objDPArr"></param>
        public void CreateDatabaseParameter(int intParameterNum, out IDataParameter[] objDPArr)
        {
            objDPArr = null;

            switch (M_strONLINE_MODULE_CHR)
            {
                case "1":
                    objDPArr = new Oracle.DataAccess.Client.OracleParameter[intParameterNum];
                    for (int i = 0; i < intParameterNum; i++)
                    {
                        objDPArr[i] = new Oracle.DataAccess.Client.OracleParameter();
                    }
                    break;
                case "2":
                    objDPArr = new System.Data.SqlClient.SqlParameter[intParameterNum];
                    for (int i = 0; i < intParameterNum; i++)
                    { 
                        objDPArr[i] = new System.Data.SqlClient.SqlParameter(); 
                    }
                    break;
                case "3":
                    objDPArr = new System.Data.OleDb.OleDbParameter[intParameterNum];
                    for (int i = 0; i < intParameterNum; i++)
                    {
                        objDPArr[i] = new System.Data.OleDb.OleDbParameter();
                    }
                    break;
                case "4":
                    objDPArr = new Microsoft.Data.Odbc.OdbcParameter[intParameterNum];
                    for (int i = 0; i < intParameterNum; i++)
                    {
                        objDPArr[i] = new Microsoft.Data.Odbc.OdbcParameter();
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 获取数据,有参数
        /// </summary>
        /// <param name="strSQLCommand"></param>
        /// <param name="dtResult"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public long lngGetDataTableWithParameters(string strSQLCommand, ref DataTable dtResult, params IDataParameter[] Params)
        {
            if (string.IsNullOrEmpty(strSQLCommand))
                return -1;
            if (string.IsNullOrEmpty(M_strONLINE_DNS_VCHR))
                return -1;

            IDataService ds = GetDataServ();

            return ds.lngGetDataTableWithParameters(strSQLCommand, M_strONLINE_DNS_VCHR, true, ref dtResult, Params);
        }
        /// <summary>
        /// 获取数据, 无参数
        /// </summary>
        /// <param name="strSQLCommand"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long lngGetDataTableWithoutParameters(string strSQLCommand, ref DataTable dtResult)
        {
            if (string.IsNullOrEmpty(strSQLCommand))
                return -1;
            if (string.IsNullOrEmpty(M_strONLINE_DNS_VCHR))
                return -1;

            IDataService ds = GetDataServ();
            return ds.lngGetDataTable(strSQLCommand, M_strONLINE_DNS_VCHR, true, ref dtResult);
        }

        /// <summary>
        /// 执行SQL, 有参数
        /// </summary>
        /// <param name="strSQLCommand"></param>
        /// <param name="lngRecordsAffected"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public long lngExecuteParameterSQL(string strSQLCommand, ref long lngRecordsAffected, params IDataParameter[] Params)
        {
            if (string.IsNullOrEmpty(strSQLCommand))
                return -1;
            if (string.IsNullOrEmpty(M_strONLINE_DNS_VCHR))
                return -1;

            for (int i = 0; i < Params.Length; i++)
            {
                if (Params[i].Value == null)
                {
                    Params[i].Value = System.DBNull.Value;
                }
            }

            IDataService ds = GetDataServ();

            return ds.lngExecuteParameterSQL(strSQLCommand, M_strONLINE_DNS_VCHR, ref lngRecordsAffected, Params);
        }

        /// <summary>
        /// 执行SQL, 无参数
        /// </summary>
        /// <param name="strSQLCommand"></param>
        /// <returns></returns>
        public long lngExecuteSQL(string strSQLCommand)
        {
            if (string.IsNullOrEmpty(strSQLCommand))
                return -1;
            if (string.IsNullOrEmpty(M_strONLINE_DNS_VCHR))
                return -1;

            IDataService ds = GetDataServ();

            long aff = 0;
            long res = ds.lngExecuteSQL(strSQLCommand, M_strONLINE_DNS_VCHR, ref aff);
            return res;
        }

        #endregion
    }
}
