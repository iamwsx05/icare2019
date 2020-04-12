using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using com.digitalwave.Utility;

namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// ѪҺ������¼��
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsBloodCleanseRecord_MainService : clsRecordsService
	{
        
		#region �������ݿ��е��״δ�ӡʱ��
		/// <summary>
		/// �������ݿ��е��״δ�ӡʱ�䡣
		/// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">סԺ�ǼǺ�</param>
		/// <param name="p_intRecordTypeArr">��¼����</param>
		/// <param name="p_dtmOpenDateArr">��¼ʱ��(���¼���ͼ���λ��һһ��Ӧ)</param>
		/// <param name="p_dtmFirstPrintDate">�״δ�ӡʱ��</param>
		/// <returns></returns>
		[AutoComplete]
		public override long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
			int[] p_intRecordTypeArr,
            DateTime[] p_dtmCreatedDateArr,
			DateTime p_dtmFirstPrintDate)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordMainService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	
			

			//������
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmFirstPrintDate == DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
            string strSql = @"update t_emr_bloodcleanserecord_all
   set firstprintdate_dat = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";
			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0 ; i < p_dtmCreatedDateArr.Length-1 ; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = p_dtmFirstPrintDate;
                        objDPArr[1].Value = p_strRegisterId;
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = p_dtmCreatedDateArr[i];
                        //ִ��SQL
                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                        //if (lngRes <= 0) return lngRes;
                    }
                }
                else
                {
                    //objDPArr = null;
                    //objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    DbType[] dbTypes = new DbType[] { DbType.Date, DbType.String, DbType.Date };
                    string[][] strValues = new string[3][];
                    //ArrayList arlItems = new ArrayList(p_objItems.Length);
                    //for (int i1 = 0 ; i1 < p_dtmCreatedDateArr.Length ; i1++)
                    //{
                    //    arlItems.Add(p_objItems[i1]);
                    //}
                    if (p_dtmCreatedDateArr.Length > 0)
                    {
                        for (int j = 0 ; j < strValues.Length ; j++)
                        {
                            strValues[j] = new string[p_dtmCreatedDateArr.Length-1];
                        }
                        for (int k1 = 0 ; k1 < p_dtmCreatedDateArr.Length-1 ; k1++)
                        {
                            strValues[0][k1] = p_dtmFirstPrintDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        for (int k2 = 0 ; k2 < p_dtmCreatedDateArr.Length - 1 ; k2++)
                        {
                            strValues[1][k2] = p_strRegisterId;
                        }
                        for (int k3 = 0 ; k3 < p_dtmCreatedDateArr.Length - 1 ; k3++)
                        {
                            strValues[2][k3] = p_dtmCreatedDateArr[k3].ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, strValues, dbTypes);
                    }
                }
                strSql = @"update t_emr_bloodcleansesub_all
   set firstprintdate_dat = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterId;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmCreatedDateArr[p_dtmCreatedDateArr.Length - 1];
                //ִ��SQL
                long lngEff2 = 0;
                long lngRes2 = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff2, objDPArr);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex,true);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
 
			return (long)enmOperationResult.DB_Succeed;
		}
		#endregion 

		#region ��ȡָ����¼������
		/// <summary>
		/// ��ȡָ����¼�����ݡ�
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objGeneralNurseRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
        protected override long m_lngGetTransDataInfoArrWithServ(string p_strRegisterId, int p_intStatus, out clsTransDataInfo[] p_objTansDataInfoArr)
		{
            p_objTansDataInfoArr = null;
			long lngRes = -1;
			//������
            if (string.IsNullOrEmpty(p_strRegisterId))
				return (long)enmOperationResult.Parameter_Error;
            string strSql = @"select a.touxiya_chr,
       a.jingmaiya_chr,
       a.gansuliang_chr,
       a.xueliuliang_chr,
       a.tiwen_chr,
       a.maibo_chr,
       a.xueya_chr,
       a.huxi_chr,
       a.faleng_chr,
       a.fare_chr,
       a.toutong_chr,
       a.tuoshuiliang_chr,
       a.outu_chr,
       a.chouchu_chr,
       a.xinyi_chr,
       a.nanongdu_chr,
       a.chuli_chr,
       a.chuli_chrxml,
       a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_int,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.firstprintdate_dat,
       a.recordusername_vchr,
       a.recorddate_dat,
       b.modifydate,
       b.modifyuserid,
       b.touxiya_chr_right,
       b.jingmaiya_chr_right,
       b.gansuliang_chr_right,
       b.xueliuliang_chr_right,
       b.tiwen_chr_right,
       b.maibo_chr_right,
       b.xueya_chr_right,
       b.huxi_chr_right,
       b.faleng_chr_right,
       b.fare_chr_right,
       b.toutong_chr_right,
       b.tuoshuiliang_chr_right,
       b.outu_chr_right,
       b.chouchu_chr_right,
       b.xinyi_chr_right,
       b.nanongdu_chr_right,
       b.chuli_chr_right
  from t_emr_bloodcleanserecord_all a
 inner join  t_emr_bloodcleanserecord_right b on a.registerid_chr = b.registerid_chr
                                    and a.createdate_dat = b.createdate_dat
 where a.status_int = 1
   and b.status_int = 1
   and a.registerid_chr = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                DataTable dtbValue = new DataTable();//͸�����̲����¼��¼����  

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    ArrayList arlTransDataSub = new ArrayList();
                    ArrayList arlTransData = new ArrayList();
                    DateTime dtmOpenDate = DateTime.MinValue;
                    DataRow objRow = null;
                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
                    {
                        objRow = dtbValue.Rows[j];
                        #region
                        //��ȡ��ǰDataTable��¼��OpenDate����¼��dtmOpenDate
                        dtmOpenDate = DateTime.Parse(objRow["RECORDDATE_DAT"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(objRow["RECORDDATE_DAT"].ToString()).Date == dtmOpenDate)
                        {
                            objRow = dtbValue.Rows[j];
                            #region ��DataTable.Rows�л�ȡ���

                            clsDialyseRecord_Value objRecordContent = new clsDialyseRecord_Value();
                            objRecordContent.m_strRegisterID = p_strRegisterId;
                            DateTime dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(objRow["CREATEDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmCreateDate = dtmTemp;

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(objRow["RECORDDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmRecordDate = dtmTemp;

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(objRow["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                            objRecordContent.m_strCreateUserID = objRow["CREATEUSERID_CHR"].ToString();
                            objRecordContent.m_strRECORDUSERNAME_CHR = objRow["RECORDUSERNAME_VCHR"].ToString();
                            objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(objRow["MODIFYDATE"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmModifyDate = dtmTemp;


                            int intTemp = 0;
                            int.TryParse(objRow["IFCONFIRM_INT"].ToString(), out intTemp);
                            objRecordContent.m_bytIfConfirm = intTemp;
                            intTemp = 0;
                            int.TryParse(objRow["STATUS_INT"].ToString(), out intTemp);
                            objRecordContent.m_bytStatus = intTemp;

                            objRecordContent.m_strTOUXIYA_CHR = objRow["touxiya_chr"].ToString();
                            objRecordContent.m_strJINGMAI_CHR = objRow["jingmaiya_chr"].ToString();
                            objRecordContent.m_strGANSULIANG_CHR = objRow["gansuliang_chr"].ToString();
                            objRecordContent.m_strXUELIULIANG_CHR = objRow["xueliuliang_chr"].ToString();
                            objRecordContent.m_strTIWEN_CHR = objRow["tiwen_chr"].ToString();
                            objRecordContent.m_strMAIBO_CHR = objRow["maibo_chr"].ToString();
                            objRecordContent.m_strXUEYA_CHR = objRow["xueya_chr"].ToString();
                            objRecordContent.m_strHUXI_CHR = objRow["huxi_chr"].ToString();
                            objRecordContent.m_strFALENG_CHR = objRow["faleng_chr"].ToString();
                            objRecordContent.m_strFARE_CHR = objRow["fare_chr"].ToString();
                            objRecordContent.m_strTOUTONG_CHR = objRow["toutong_chr"].ToString();
                            objRecordContent.m_strTUOSHUILIANG_CHR = objRow["tuoshuiliang_chr"].ToString();
                            objRecordContent.m_strOUTU_CHR = objRow["outu_chr"].ToString();
                            objRecordContent.m_strCHOUCHU_CHR = objRow["chouchu_chr"].ToString();
                            objRecordContent.m_strXINYI_CHR = objRow["xinyi_chr"].ToString();
                            objRecordContent.m_strNANONGDU_CHR = objRow["nanongdu_chr"].ToString();
                            objRecordContent.m_strCHULI_CHR = objRow["chuli_chr"].ToString();
                            objRecordContent.m_strCHULI_CHR_RIGHT = objRow["chuli_chr_right"].ToString();
                            objRecordContent.m_strCHULI_CHRXML = objRow["chuli_chrxml"].ToString();

                            j++;
                            arlTransDataSub.Add(objRecordContent);
                            #endregion
                        }
                        j--;
                        #endregion
                        clsDialyseRecordArr objdateInfo = new clsDialyseRecordArr();
                        objdateInfo.m_objRecordArr = (clsDialyseRecord_Value[])arlTransDataSub.ToArray(typeof(clsDialyseRecord_Value));
                        objdateInfo.m_objRecordContent = objdateInfo.m_objRecordArr[objdateInfo.m_objRecordArr.Length - 1];
                        arlTransData.Add(objdateInfo);
                        arlTransDataSub.Clear();
                    }
                    clsBloodCleanRecordValueContentDataInfo[] objTansDataInfoArr = new clsBloodCleanRecordValueContentDataInfo[1];
                    objTansDataInfoArr[0] = new clsBloodCleanRecordValueContentDataInfo();
                    objTansDataInfoArr[0].m_objDialyseRecordValues = (clsDialyseRecordArr[])arlTransData.ToArray(typeof(clsDialyseRecordArr));
                    p_objTansDataInfoArr = objTansDataInfoArr;
                }
                clsBloodCleanseBaseRecord_Value objBloodCleanseBaseRecord = null;
                lngRes = m_lngGetRecord(  p_strRegisterId, out objBloodCleanseBaseRecord);
                if (lngRes > 0 && objBloodCleanseBaseRecord != null)
                {
                    clsBloodCleanRecordValueContentDataInfo objjTansDataInfo = null;
                    if (p_objTansDataInfoArr != null && p_objTansDataInfoArr.Length > 0)
                        objjTansDataInfo = (clsBloodCleanRecordValueContentDataInfo)p_objTansDataInfoArr[0];
                    else
                        objjTansDataInfo = new clsBloodCleanRecordValueContentDataInfo();
                    objjTansDataInfo.m_objBloodCleanseBaseRecord = objBloodCleanseBaseRecord;

                    p_objTansDataInfoArr = new clsTransDataInfo[] { objjTansDataInfo };
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
                return 0;
            }
            finally
            {
                objHRPServ = null;
                //objHRPServ.Dispose();

            }
			return (long)enmOperationResult.DB_Succeed;
		}
		#endregion

		#region �鿴��ǰ��¼�Ƿ����µļ�¼
		/// <summary>
		/// �鿴��ǰ��¼�Ƿ����µļ�¼��
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngCheckLastModifyRecord(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;

			//������          
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

            string strSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_bloodcleanserecord_all t1, t_emr_bloodcleanserecord_right t2
 where t1.registerid_chr = t2.registerid_chr
   and t1.createdate_dat = t2.createdate_dat
   and t1.status_int = 1
   and t2.status_int = 1
   and t1.registerid_chr = ?
   and t1.createdate_dat = ? ";

				
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
                //ʹ��strSQL����DataTable
                DataTable dtbValue = new DataTable();
                //ִ�в�ѯ���������DataTable            
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                //���DataTable.Rows.Count����0��������ҵ������Ѿ���ɾ��������Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    strSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_chr
  from t_emr_bloodcleanserecord_all t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 0";
                    objDPArr = null;
                    p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID_CHR"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE_DAT"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //��DataTable�л�ȡModifyDate��ʹ֮��p_objRecordContent.m_dtmModifyDate�Ƚ�
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //�����ͬ������DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //���򣬷���Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
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
                objHRPServ = null;
            } 
            return lngRes;	
			
		}
		#endregion

		#region �Ѽ�¼�������С�ɾ������
		/// <summary>
		/// �Ѽ�¼�������С�ɾ������
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngDeleteRecord2DB(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//������
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            string strSql = @"update t_emr_bloodcleanserecord_all t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

                strSql = @"update t_emr_bloodcleansesub_all t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";
                objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;

                //ִ��SQL
                lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            } 
            return lngRes;
		}
		#endregion

        #region �������ݵ�����޸�ɾ������

        #region Get
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecord( string p_strRegisterId, out clsBloodCleanseBaseRecord_Value p_objRecordContent)
        {
            p_objRecordContent = null;
            //������                              
            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"select a.registerid_chr,
       a.createdate_dat,
       a.status_int,
       a.touxihao_vchr,
       a.DIJICITOUXI_VCHR,
       a.touxiriqi_dat,
       a.zhenduan_vchr,
       a.zhenduanxml_vchr,
       a.pangdaofangshi_vchr,
       a.youwuganran_vchr,
       a.gansuhua_vchr,
       a.gansu_vchr,
       a.yujingdanbai_vchr,
       a.touxishijian_xiaoshi_vchr,
       a.touxishijian_fenzhong_vchr,
       a.touxiyepeifang_vchr,
       a.touxizhuangzhixinghao_vchr,
       a.tizhong_tian_vchr,
       a.tizhong_hou_vchr,
       a.hulijilu_vchr,
       a.hulijiluxml_vchr,
       a.sequence_int,
       a.createuserid_chr,
       a.firstprintdate_dat,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.ifconfirm_int,
       b.modifydate_dat,
       b.modifyuserid_chr,
       b.touxiriqiright_dat,
       b.zhenduanright_vchr,
       b.pangdaofangshiright_vchr,
       b.youwuganranright_vchr,
       b.gansuhuaright_vchr,
       b.gansuright_vchr,
       b.yujingdanbairight_vchr,
       b.touxishijianright_xiaoshi_int,
       b.touxishijianright_fenzhong_int,
       b.touxiyepeifangright_vchr,
       b.xinghaoright_vchr,
       b.tizhongright_tian_vchr,
       b.tizhongright_hou_vchr,
       b.hulijiluright_vchr
  from t_emr_bloodcleansesub_all a
 inner join t_emr_bloodcleansesub_r b on a.registerid_chr = b.registerid_chr
                                    and a.createdate_dat = b.createdate_dat
 where a.status_int = 1
   and b.status_int = 1
   and a.registerid_chr = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtbValue = new DataTable();
                DataRow objRow = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objRecordContent = new clsBloodCleanseBaseRecord_Value();
                    objRow = dtbValue.Rows[0];
                    #region 
                    p_objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(objRow["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    p_objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(objRow["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    p_objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    p_objRecordContent.m_strCreateUserID = objRow["CREATEUSERID_CHR"].ToString();
                    p_objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID_CHR"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(objRow["MODIFYDATE_DAT"].ToString(), out dtmTemp);
                    p_objRecordContent.m_dtmModifyDate = dtmTemp;

                    int intTemp = 0;
                    int.TryParse(objRow["IFCONFIRM_INT"].ToString(), out intTemp);
                    p_objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(objRow["STATUS_INT"].ToString(), out intTemp);
                    p_objRecordContent.m_bytStatus = intTemp;

                    p_objRecordContent.m_strTOUXIHAO = objRow["touxihao_vchr"].ToString();
                    p_objRecordContent.m_strTOTALBLOODNUM_CHR = objRow["DIJICITOUXI_VCHR"].ToString();
                    p_objRecordContent.m_strTOUXIRIQI_CHR = objRow["touxiriqi_dat"].ToString();

                    p_objRecordContent.m_strZHENDUAN_CHR = objRow["zhenduan_vchr"].ToString();
                    p_objRecordContent.m_strZHENDUAN_CHR_RIGHT = objRow["zhenduanright_vchr"].ToString();
                    p_objRecordContent.m_strZHENDUAN_CHRXML = objRow["zhenduanxml_vchr"].ToString();

                    p_objRecordContent.m_strPANGDAOFANGSHI_CHR = objRow["pangdaofangshi_vchr"].ToString();
                    p_objRecordContent.m_strYOUWUGANRAN_CHR = objRow["youwuganran_vchr"].ToString();
                    p_objRecordContent.m_strGANSUHUA_CHR = objRow["gansuhua_vchr"].ToString();


                    p_objRecordContent.m_strGANSU_CHR = objRow["gansu_vchr"].ToString();
                    p_objRecordContent.m_strYUJINGDANBAI_CHR = objRow["yujingdanbai_vchr"].ToString();
                    p_objRecordContent.m_strTOUXISHIJIAN_SHI_CHR = objRow["touxishijian_xiaoshi_vchr"].ToString();

                    p_objRecordContent.m_strTOUXISHIJIAN_FEN_CHR = objRow["touxishijian_fenzhong_vchr"].ToString();
                    p_objRecordContent.m_strTOUXIYEPEIFANG_CHR = objRow["touxiyepeifang_vchr"].ToString();
                    p_objRecordContent.m_strZHUANGZHIXINGHAO_CHR = objRow["touxizhuangzhixinghao_vchr"].ToString();
                    p_objRecordContent.m_strTIZHONG_QIAN_CHR = objRow["tizhong_tian_vchr"].ToString();
                    p_objRecordContent.m_strTIZHONG_HOU_CHR = objRow["tizhong_hou_vchr"].ToString();

                    p_objRecordContent.m_strHULIJILU_CHR = objRow["hulijilu_vchr"].ToString();
                    p_objRecordContent.m_strHULIJILU_CHR_RIGHT = objRow["hulijiluright_vchr"].ToString();
                    p_objRecordContent.m_strHULIJILU_CHRXML = objRow["hulijiluxml_vchr"].ToString();

                    //��ȡǩ������
                    long lngS = 0;
                    if (long.TryParse(objRow["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign2 = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign2.m_lngGetSign(lngS, out p_objRecordContent.objSignerArr);

                        //�ͷ�
                        objSign2 = null;
                    }
                    #endregion 
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
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion Get

        #region ���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewRecord( clsBloodCleanseBaseRecord_Value p_objRecordContent)
        {
            //������                              
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"insert into t_emr_bloodcleansesub_all
       (registerid_chr,
       createdate_dat,
       status_int,
       touxihao_vchr,
       DIJICITOUXI_VCHR,
       touxiriqi_dat,
       zhenduan_vchr,
       zhenduanxml_vchr,
       pangdaofangshi_vchr,
       youwuganran_vchr,
       gansuhua_vchr,
       gansu_vchr,
       yujingdanbai_vchr,
       touxishijian_xiaoshi_vchr,
       touxishijian_fenzhong_vchr,
       touxiyepeifang_vchr,
       touxizhuangzhixinghao_vchr,
       tizhong_tian_vchr,
       tizhong_hou_vchr,
       hulijilu_vchr,
       hulijiluxml_vchr,
       sequence_int,
       createuserid_chr,
       ifconfirm_int)
values
  (?,?,1,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,0)";//22
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region ��ֵ
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(22, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.Date;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
                objDPArr[2].Value = p_objRecordContent.m_strTOUXIHAO;
                objDPArr[3].Value = p_objRecordContent.m_strTOTALBLOODNUM_CHR;
                objDPArr[4].DbType = DbType.Date;
                objDPArr[4].Value = DateTime.Parse(p_objRecordContent.m_strTOUXIRIQI_CHR);
                objDPArr[5].Value = p_objRecordContent.m_strZHENDUAN_CHR;
                objDPArr[6].Value = p_objRecordContent.m_strZHENDUAN_CHRXML;
                objDPArr[7].Value = p_objRecordContent.m_strPANGDAOFANGSHI_CHR;
                objDPArr[8].Value = p_objRecordContent.m_strYOUWUGANRAN_CHR;

                objDPArr[9].Value = p_objRecordContent.m_strGANSUHUA_CHR;
                objDPArr[10].Value = p_objRecordContent.m_strGANSU_CHR;
                objDPArr[11].Value = p_objRecordContent.m_strYUJINGDANBAI_CHR;
                objDPArr[12].Value = p_objRecordContent.m_strTOUXISHIJIAN_SHI_CHR;
                objDPArr[13].Value = p_objRecordContent.m_strTOUXISHIJIAN_FEN_CHR;
                objDPArr[14].Value = p_objRecordContent.m_strTOUXIYEPEIFANG_CHR;
                objDPArr[15].Value = p_objRecordContent.m_strZHUANGZHIXINGHAO_CHR;
                objDPArr[16].Value = p_objRecordContent.m_strTIZHONG_QIAN_CHR;
                objDPArr[17].Value = p_objRecordContent.m_strTIZHONG_HOU_CHR;
                objDPArr[18].Value = p_objRecordContent.m_strHULIJILU_CHR;
                objDPArr[19].Value = p_objRecordContent.m_strHULIJILU_CHRXML;

                objDPArr[20].Value = lngSequence;
                objDPArr[21].Value = p_objRecordContent.m_strCreateUserID;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);

                lngRes = m_lngAddRecordRight(p_objRecordContent);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddRecordRight(clsBloodCleanseBaseRecord_Value p_objRecordContent)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strSql = @"insert into t_emr_bloodcleansesub_r
      (registerid_chr,
       createdate_dat,
       status_int,
       modifydate_dat,
       modifyuserid_chr,
       touxihaoright_vchr,
       dijicitouxiright_int,
       touxiriqiright_dat,
       zhenduanright_vchr,
       pangdaofangshiright_vchr,
       youwuganranright_vchr,
       gansuhuaright_vchr,
       gansuright_vchr,
       yujingdanbairight_vchr,
       touxishijianright_xiaoshi_int,
       touxishijianright_fenzhong_int,
       touxiyepeifangright_vchr,
       xinghaoright_vchr,
       tizhongright_tian_vchr,
       tizhongright_hou_vchr,
       hulijiluright_vchr)
values
  (?,?,1,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";//20
            try
            {
                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(20, out objDPArr2);
                objDPArr2[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.Date;
                objDPArr2[1].Value = p_objRecordContent.m_dtmCreateDate;
                objDPArr2[2].DbType = DbType.Date;
                objDPArr2[2].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr2[3].Value = p_objRecordContent.m_strModifyUserID;
                objDPArr2[4].Value = p_objRecordContent.m_strTOUXIHAO;
                objDPArr2[5].Value = p_objRecordContent.m_strTOTALBLOODNUM_CHR;
                objDPArr2[6].DbType = DbType.Date;
                objDPArr2[6].Value = DateTime.Parse(p_objRecordContent.m_strTOUXIRIQI_CHR);
                objDPArr2[7].Value = p_objRecordContent.m_strZHENDUAN_CHR_RIGHT;
                objDPArr2[8].Value = p_objRecordContent.m_strPANGDAOFANGSHI_CHR;
                objDPArr2[9].Value = p_objRecordContent.m_strYOUWUGANRAN_CHR;
                objDPArr2[10].Value = p_objRecordContent.m_strGANSUHUA_CHR;
                objDPArr2[11].Value = p_objRecordContent.m_strGANSU_CHR;
                objDPArr2[12].Value = p_objRecordContent.m_strYUJINGDANBAI_CHR;
                objDPArr2[13].Value = p_objRecordContent.m_strTOUXISHIJIAN_SHI_CHR;
                objDPArr2[14].Value = p_objRecordContent.m_strTOUXISHIJIAN_FEN_CHR;
                objDPArr2[15].Value = p_objRecordContent.m_strTOUXIYEPEIFANG_CHR;
                objDPArr2[16].Value = p_objRecordContent.m_strZHUANGZHIXINGHAO_CHR;
                objDPArr2[17].Value = p_objRecordContent.m_strTIZHONG_QIAN_CHR;
                objDPArr2[18].Value = p_objRecordContent.m_strTIZHONG_HOU_CHR;
                objDPArr2[19].Value = p_objRecordContent.m_strHULIJILU_CHR_RIGHT;

                #endregion
                long lngEff = 0;
                //ִ��SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr2);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion ���

        #region �޸�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyRecord( clsBloodCleanseBaseRecord_Value p_objRecordContent)
        {
            //������
            if (p_objRecordContent == null || p_objRecordContent.m_dtmCreateDate == DateTime.MinValue || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"update t_emr_bloodcleansesub_all
   set touxihao_vchr = ?,
       DIJICITOUXI_VCHR = ?,
       touxiriqi_dat = ?,
       zhenduan_vchr = ?,
       zhenduanxml_vchr = ?,
       pangdaofangshi_vchr = ?,
       youwuganran_vchr = ?,
       gansuhua_vchr = ?,
       gansu_vchr = ?,
       yujingdanbai_vchr = ?,
       touxishijian_xiaoshi_vchr = ?,
       touxishijian_fenzhong_vchr = ?,
       touxiyepeifang_vchr = ?,
       touxizhuangzhixinghao_vchr = ?,
       tizhong_tian_vchr = ?,
       tizhong_hou_vchr = ?,
       hulijilu_vchr = ?,
       hulijiluxml_vchr = ?,
       sequence_int = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//21
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡǩ����ˮ��
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region set value
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(21, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strTOUXIHAO;
                objDPArr[1].Value = p_objRecordContent.m_strTOTALBLOODNUM_CHR;
                objDPArr[2].DbType = DbType.Date;
                objDPArr[2].Value = DateTime.Parse(p_objRecordContent.m_strTOUXIRIQI_CHR);
                objDPArr[3].Value = p_objRecordContent.m_strZHENDUAN_CHR;
                objDPArr[4].Value = p_objRecordContent.m_strZHENDUAN_CHRXML;
                objDPArr[5].Value = p_objRecordContent.m_strPANGDAOFANGSHI_CHR;
                objDPArr[6].Value = p_objRecordContent.m_strYOUWUGANRAN_CHR;
                objDPArr[7].Value = p_objRecordContent.m_strGANSUHUA_CHR;
                objDPArr[8].Value = p_objRecordContent.m_strGANSU_CHR;
                objDPArr[9].Value = p_objRecordContent.m_strYUJINGDANBAI_CHR;
                objDPArr[10].Value = p_objRecordContent.m_strTOUXISHIJIAN_SHI_CHR;
                objDPArr[11].Value = p_objRecordContent.m_strTOUXISHIJIAN_FEN_CHR;
                objDPArr[12].Value = p_objRecordContent.m_strTOUXIYEPEIFANG_CHR;
                objDPArr[13].Value = p_objRecordContent.m_strZHUANGZHIXINGHAO_CHR;
                objDPArr[14].Value = p_objRecordContent.m_strTIZHONG_QIAN_CHR;
                objDPArr[15].Value = p_objRecordContent.m_strTIZHONG_HOU_CHR;
                objDPArr[16].Value = p_objRecordContent.m_strHULIJILU_CHR;
                objDPArr[17].Value = p_objRecordContent.m_strHULIJILU_CHRXML;
                objDPArr[18].Value = lngSequence;
                objDPArr[19].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[20].DbType = DbType.Date;
                objDPArr[20].Value = p_objRecordContent.m_dtmCreateDate;

                #endregion
                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //����ǩ������
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);

                #region set value
                //�þɼ�¼״̬Ϊ0
                lngRes = m_lngDeleteContentInfo(p_objRecordContent.m_strRegisterID, p_objRecordContent.m_dtmCreateDate);
                //�����¼�¼
                lngRes = m_lngAddRecordRight(p_objRecordContent);

                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
        }
        /// <summary>
        /// �þɼ�¼״̬Ϊ0
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteContentInfo(string p_strRegisterId, DateTime p_dtmCreatedDate)
        {
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmCreatedDate == DateTime.MaxValue)
                return -1;
            string strSql = @" update t_emr_bloodcleansesub_r t set t.status_int = 0
 where t.registerid_chr = ? and t.createdate_dat = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmCreatedDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion �޸�

        #region ɾ��
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord( clsBloodCleanseBaseRecord_Value p_objRecordContent)
        {
            //������
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"update t_emr_bloodcleansesub_all t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";
             long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //��ȡIDataParameter����
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;

                //ִ��SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;

            } return lngRes;
        }
        #endregion ɾ��

        #endregion ��ע���ݵ�����޸�ɾ������
    }
}
