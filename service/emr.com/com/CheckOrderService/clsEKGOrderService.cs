using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.CheckOrderService
{  

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEKGOrderService : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        //不允许有实例变量。所有函数的返回值都是long，传出值用out

        private const string m_strGetAllTimeInfoByPatient = @"select distinct createdate from ekgorder 
														  where inpatientid=? and status = '0' and inpatientdate=?";

        private const string m_strGetEKGOrderSQL_Oracle = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       createuserid,
       applicationdate,
       ekgnumber,
       clinicalimpression,
       hadotherdrug,
       comeek,
       requestersign,
       result,
       doctorsign,
       status,
       deactiveddate,
       deactivedoperatorid
  from (select a.inpatientid,
               a.inpatientdate,
               a.createdate,
               a.modifydate,
               a.createuserid,
               a.applicationdate,
               a.ekgnumber,
               a.clinicalimpression,
               a.hadotherdrug,
               a.comeek,
               a.requestersign,
               a.result,
               a.doctorsign,
               a.status,
               a.deactiveddate,
               a.deactivedoperatorid
          from ekgorder a
         where trim(a.inpatientid) = ?
           and a.status = '0'
           and a.createdate = ?
           and a.inpatientdate = ?
         order by modifydate desc)
 where rownum = 1";

        private const string m_strGetEKGOrderSQL_ODBC = @"select top 1 a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.modifydate,
       a.createuserid,
       a.applicationdate,
       a.ekgnumber,
       a.clinicalimpression,
       a.hadotherdrug,
       a.comeek,
       a.requestersign,
       a.result,
       a.doctorsign,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid
  from ekgorder a
 where inpatientid = ?
   and a.status = '0'
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc";

        private const string m_InsertRecordSQL = @"insert into ekgorder 
			(hadotherdrug,applicationdate,clinicalimpression,comeek,doctorsign,ekgnumber,
			requestersign,result,createdate,createuserid,inpatientdate,inpatientid,status,modifydate)
			values (?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        ///系统中采用的删除操作是通过更新操作，更改数据表的Status属性，将属性由0设为1
        /// </summary>
        private const string m_DeleteRecordSQL = @"update ekgorder set status=1,deactiveddate=?,deactivedoperatorid=? 
										  where inpatientid=? and createdate=? and inpatientdate=?";


        /// <summary>
        /// 获得所有Create Date
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strXml"></param>
        /// <param name="intRows"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTimeInfoOfAPatient(
                                            string p_strInPatientID, string p_strInPatientDate, ref string[] p_strDateInfo)
        {
            //			p_strDateInfo=new String[1];
            DataTable dtbValue = new DataTable();

            //			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsEKGOrderService","m_lngGetTimeInfoOfAPatient");
            //			////if (lngCheckRes <= 0)
            //				//return lngCheckRes;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);

            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

            try
            {
                long lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetAllTimeInfoByPatient, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strDateInfo = new String[dtbValue.Rows.Count];
                    for (int i1 = 0; i1 < dtbValue.Rows.Count; i1++)
                        p_strDateInfo[i1] = dtbValue.Rows[i1][0].ToString().Trim();

                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return (long)enmOperationResult.DB_Succeed;

        }


        /// <summary>
        /// 获取病人的EKG记录
        /// </summary>
        /// <param name="p_strInPatientID">病人住院ID</param>
        /// <param name="p_objEKGOrderArr">EKG记录对象</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEKGOrder(
                                    string p_strPatientID, string p_strInPatientDate,
                                    string p_strCreateDate, out clsEKGOrder p_objEKGOrder)
        {
            p_objEKGOrder = new clsEKGOrder();
            DataTable dtbValue = new DataTable();

            //			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsEKGOrderService","m_lngGetEKGOrder");
            //			////if (lngCheckRes <= 0)
            //				//return lngCheckRes;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);

            objDPArr[0].Value = p_strPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strCreateDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

            try
            {
                long lngRes;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetEKGOrderSQL_ODBC, ref dtbValue, objDPArr);
                else
                    lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetEKGOrderSQL_Oracle, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objEKGOrder = new clsEKGOrder();

                    p_objEKGOrder.strInPatientID = dtbValue.Rows[0]["INPATIENTID"].ToString();
                    p_objEKGOrder.m_blnHadOtherDrug = dtbValue.Rows[0]["HADOTHERDRUG"].ToString() == "0" ? false : true;
                    p_objEKGOrder.m_dteApplicationDate = dtbValue.Rows[0]["APPLICATIONDATE"].ToString().Trim();
                    p_objEKGOrder.m_strClinicalImpression = dtbValue.Rows[0]["CLINICALIMPRESSION"].ToString().Trim();
                    p_objEKGOrder.m_strComeEK = dtbValue.Rows[0]["COMEEK"].ToString().Trim();
                    p_objEKGOrder.m_strDoctorSign = dtbValue.Rows[0]["DOCTORSIGN"].ToString().Trim();
                    p_objEKGOrder.m_strEKGNumber = dtbValue.Rows[0]["EKGNUMBER"].ToString().Trim();
                    p_objEKGOrder.m_strRequesterSign = dtbValue.Rows[0]["REQUESTERSIGN"].ToString().Trim();
                    p_objEKGOrder.m_strResult = dtbValue.Rows[0]["RESULT"].ToString().Trim();
                    p_objEKGOrder.strCreateDate = dtbValue.Rows[0]["CREATEDATE"].ToString().Trim();
                    p_objEKGOrder.strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objEKGOrder.strInPatientDate = dtbValue.Rows[0]["INPATIENTDATE"].ToString().Trim();
                    p_objEKGOrder.strStatus = int.Parse(dtbValue.Rows[0]["STATUS"].ToString());

                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }
            return (long)enmOperationResult.DB_Succeed;

        }

        /// <summary>
        ///更新记录。包含新增，修改，删除操作
        /// </summary>
        /// <param name="p_objEKGOrder"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateEKGOrder(clsEKGOrder p_objEKGOrder, enmUpdateAction p_enmAction)
        {
            try
            {
                long lngAffectedRows = 0;
                long lngRe = 0;
                IDataParameter[] objDPArr = null;
                DataTable dtbValue = new DataTable();
                clsHRPTableService objHRPServ = new clsHRPTableService();

                switch (p_enmAction)
                {
                    case enmUpdateAction.enmAddNew:
                    case enmUpdateAction.enmEdit:
                        objHRPServ.CreateDatabaseParameter(14, out objDPArr);

                        objDPArr[0].Value = p_objEKGOrder.m_blnHadOtherDrug ? 1 : 0;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(DateTime.Parse(p_objEKGOrder.m_dteApplicationDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[2].Value = p_objEKGOrder.m_strClinicalImpression;
                        objDPArr[3].Value = p_objEKGOrder.m_strComeEK;
                        objDPArr[4].Value = p_objEKGOrder.m_strDoctorSign;
                        objDPArr[5].Value = p_objEKGOrder.m_strEKGNumber;
                        objDPArr[6].Value = p_objEKGOrder.m_strRequesterSign;
                        objDPArr[7].Value = p_objEKGOrder.m_strResult;
                        objDPArr[8].DbType = DbType.DateTime;
                        objDPArr[8].Value = DateTime.Parse(DateTime.Parse(p_objEKGOrder.strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[9].Value = p_objEKGOrder.strCreateUserID;
                        objDPArr[10].DbType = DbType.DateTime;
                        objDPArr[10].Value = DateTime.Parse(DateTime.Parse(p_objEKGOrder.strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[11].Value = p_objEKGOrder.strInPatientID;
                        objDPArr[12].Value = p_objEKGOrder.strStatus;
                        objDPArr[13].DbType = DbType.DateTime;
                        objDPArr[13].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        try
                        {
                            lngRe = objHRPServ.lngExecuteParameterSQL(m_InsertRecordSQL, ref lngAffectedRows, objDPArr);
                        }
                        catch (Exception objEx)
                        {
                            com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }


                        break;
                    case enmUpdateAction.enmDelete:
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = p_objEKGOrder.m_dtmDeActivedDate;
                        objDPArr[1].Value = p_objEKGOrder.m_strDeActivedOperatorID;
                        objDPArr[2].Value = p_objEKGOrder.strInPatientID;
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(DateTime.Parse(p_objEKGOrder.strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[4].DbType = DbType.DateTime;
                        objDPArr[4].Value = DateTime.Parse(DateTime.Parse(p_objEKGOrder.strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));

                        try
                        {
                            lngRe = objHRPServ.lngExecuteParameterSQL(m_DeleteRecordSQL, ref lngAffectedRows, objDPArr);
                        }
                        catch (Exception objEx)
                        {
                            com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                            bool blnRes = objLogger.LogError(objEx);
                        }


                        break;
                }
                //objHRPServ.Dispose();


                if (lngRe > 0 && lngAffectedRows > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }

            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
            }
        }

    }

}
