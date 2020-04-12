using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.CheckOrderService
{
	/// <summary>
	/// Summary description for clsPSGOrderService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsPSGOrderService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		//不允许有实例变量。所有函数的返回值都是long，传出值用out
	

		private const string m_strGetAllTimeInfoByPatient = @"select distinct createdate from psgorder 
			where inpatientid=? and status = '0' and inpatientdate=?";

        private const string m_strGetPSGOrderSQL_Oracle = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       createuserid,
       psgnumber,
       applicationdate,
       address,
       paytype,
       checkitem,
       clinicalimpression,
       assay,
       clinicaldiagnose,
       requestersign,
       status,
       deactiveddate,
       deactivedoperatorid
  from (select a.inpatientid,
               a.inpatientdate,
               a.createdate,
               a.modifydate,
               a.createuserid,
               a.psgnumber,
               a.applicationdate,
               a.address,
               a.paytype,
               a.checkitem,
               a.clinicalimpression,
               a.assay,
               a.clinicaldiagnose,
               a.requestersign,
               a.status,
               a.deactiveddate,
               a.deactivedoperatorid
          from psgorder a
         where trim(a.inpatientid) = ?
           and a.status = '0'
           and a.createdate = ?
           and a.inpatientdate = ?
         order by modifydate desc)
 where rownum = 1";

        private const string m_strGetPSGOrderSQL_ODBC = @"select top 1 inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       createuserid,
       psgnumber,
       applicationdate,
       address,
       paytype,
       checkitem,
       clinicalimpression,
       assay,
       clinicaldiagnose,
       requestersign,
       status,
       deactiveddate,
       deactivedoperatorid
  from psgorder a
 where inpatientid = ?
   and a.status = '0'
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc";

		private const string m_InsertRecordSQL = @"insert into psgorder 
			(inpatientid,inpatientdate,createdate,modifydate,createuserid,psgnumber,
			applicationdate,address,paytype,checkitem,clinicalimpression,assay,clinicaldiagnose,requestersign,status)
			values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		///系统中采用的删除操作是通过更新操作，更改数据表的Status属性，将属性由0设为1
		/// </summary>
		private const string m_DeleteRecordSQL = @"update psgorder set status=1,deactiveddate=?,deactivedoperatorid=? 
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
			string p_strInPatientID,string p_strInPatientDate,ref string[]  p_strDateInfo)
		{
			//			p_strDateInfo=new String[1];
			DataTable dtbValue = new DataTable();

//			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPSGOrderService","m_lngGetTimeInfoOfAPatient");
//			////if (lngCheckRes <= 0)
//				//return lngCheckRes;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);

            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
			objDPArr[1].Value=DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));

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
		/// 获取病人的PSG记录
		/// </summary>
		/// <param name="p_strInPatientID">病人住院ID</param>
		/// <param name="p_objPSGOrderArr">PSG记录对象</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPSGOrder(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,out clsPSGOrder p_objPSGOrder)
		{		
			p_objPSGOrder = new clsPSGOrder();
			DataTable dtbValue = new DataTable();

//			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPSGOrderService","m_lngGetPSGOrder");
//			////if (lngCheckRes <= 0)
//				//return lngCheckRes;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);

            objDPArr[0].Value = p_strPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(DateTime.Parse(p_strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[2].DbType = DbType.DateTime;
			objDPArr[2].Value=DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));

             try
            {
                long lngRes = -1;

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetPSGOrderSQL_ODBC, ref dtbValue, objDPArr);
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetPSGOrderSQL_Oracle, ref dtbValue, objDPArr);
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetPSGOrderSQL_Oracle, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objPSGOrder = new clsPSGOrder();

                    p_objPSGOrder.strCreateDate = dtbValue.Rows[0]["CREATEDATE"].ToString();
                    p_objPSGOrder.strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objPSGOrder.strInPatientDate = dtbValue.Rows[0]["INPATIENTDATE"].ToString().Trim();
                    p_objPSGOrder.strInPatientID = dtbValue.Rows[0]["INPATIENTID"].ToString().Trim();
                    p_objPSGOrder.strStatus = int.Parse(dtbValue.Rows[0]["STATUS"].ToString());


                    switch (int.Parse(dtbValue.Rows[0]["PAYTYPE"].ToString().Trim()))
                    {
                        case 0:
                            p_objPSGOrder.m_enmPayType = enmPayType.enmUnknow;
                            break;
                        case 1:
                            p_objPSGOrder.m_enmPayType = enmPayType.enmPublic;
                            break;

                        case 2:
                            p_objPSGOrder.m_enmPayType = enmPayType.enmCompany;
                            break;
                        case 3:
                            p_objPSGOrder.m_enmPayType = enmPayType.enmPrivate;
                            break;
                    }

                    p_objPSGOrder.m_strAddress = dtbValue.Rows[0]["ADDRESS"].ToString().Trim();
                    p_objPSGOrder.m_strApplicationDate = dtbValue.Rows[0]["APPLICATIONDATE"].ToString().Trim();
                    p_objPSGOrder.m_strAssay = dtbValue.Rows[0]["ASSAY"].ToString().Trim();
                    p_objPSGOrder.m_strCheckItem = dtbValue.Rows[0]["CHECKITEM"].ToString().Trim();
                    p_objPSGOrder.m_strClinicalDiagnose = dtbValue.Rows[0]["CLINICALDIAGNOSE"].ToString();
                    p_objPSGOrder.m_strClinicalImpression = dtbValue.Rows[0]["CLINICALIMPRESSION"].ToString().Trim();
                    p_objPSGOrder.m_strPSGNumber = dtbValue.Rows[0]["PSGNUMBER"].ToString();
                    p_objPSGOrder.m_strRequesterSign = dtbValue.Rows[0]["REQUESTERSIGN"].ToString();


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
		/// <param name="p_objPSGOrder"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdatePSGOrder(clsPSGOrder p_objPSGOrder,enmUpdateAction p_enmAction)
		{
		
			long lngAffectedRows=0;
			long lngRes=0;
			IDataParameter[] objDPArr;
            clsHRPTableService objHRPServ = new clsHRPTableService(); ;
	        DataTable dtbValue = new DataTable();
	        try
			{
				switch(p_enmAction)
				{
					case enmUpdateAction.enmAddNew:
					case enmUpdateAction.enmEdit:
 						objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(15, out objDPArr);
                        objDPArr[0].Value = p_objPSGOrder.strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(DateTime.Parse(p_objPSGOrder.strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(DateTime.Parse(p_objPSGOrder.strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[3].DbType = DbType.DateTime;
						objDPArr[3].Value=DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
						objDPArr[4].Value=p_objPSGOrder.strCreateUserID ;
                        objDPArr[5].Value = p_objPSGOrder.m_strPSGNumber;
                        objDPArr[6].DbType = DbType.DateTime;
						objDPArr[6].Value=DateTime.Parse(DateTime.Parse(p_objPSGOrder.m_strApplicationDate).ToString("yyyy-MM-dd HH:mm:ss")) ;
						objDPArr[7].Value=p_objPSGOrder.m_strAddress ;

					switch(p_objPSGOrder.m_enmPayType)
					{
						case enmPayType.enmUnknow:
							objDPArr[8].Value= 0;
							break;
						case enmPayType.enmPublic :
							objDPArr[8].Value=1;
							break;

						case enmPayType.enmCompany :
							objDPArr[8].Value=2;
							break;
						case enmPayType.enmPrivate :
							objDPArr[8].Value=3;
							break;
					}
						objDPArr[9].Value=p_objPSGOrder.m_strCheckItem ;
						objDPArr[10].Value=p_objPSGOrder.m_strClinicalImpression;
						objDPArr[11].Value=p_objPSGOrder.m_strAssay  ;
						objDPArr[12].Value=p_objPSGOrder.m_strClinicalDiagnose;
						objDPArr[13].Value=p_objPSGOrder.m_strRequesterSign;
						objDPArr[14].Value=p_objPSGOrder.strStatus;


                        lngRes = objHRPServ.lngExecuteParameterSQL(m_InsertRecordSQL, ref lngAffectedRows, objDPArr);

						break;
					case enmUpdateAction.enmDelete:
 						objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);

                        objDPArr[0].DbType = DbType.DateTime;
						objDPArr[0].Value=p_objPSGOrder.m_dtmDeActivedDate  ;
						objDPArr[1].Value=p_objPSGOrder.m_strDeActivedOperatorID;
                        objDPArr[2].Value = p_objPSGOrder.strInPatientID;
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(DateTime.Parse(p_objPSGOrder.strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[4].DbType = DbType.DateTime;
						objDPArr[4].Value=DateTime.Parse(DateTime.Parse(p_objPSGOrder.strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss")) ;


                        lngRes = objHRPServ.lngExecuteParameterSQL(m_DeleteRecordSQL, ref lngAffectedRows, objDPArr);

						break;
				}
					
				
				if (lngRes>0 && lngAffectedRows>0)
				{
					return 1;
				}
				else
				{
					return 0;
				}

			}
		
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
				return 0;	
			}
	        finally
	        {
	          //objHRPServ.Dispose();

	        }
            return lngRes;
		}

	}

}
