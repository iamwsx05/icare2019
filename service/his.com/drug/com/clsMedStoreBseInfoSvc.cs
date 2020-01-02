using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.MedicineStoreService
{
	/// <summary>
	/// ҩ��������Ϣ
	/// Create by kong 2004-07-02
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(Enabled=true)]
	public class clsMedStoreBseInfoSvc : clsMiddleTierBase
	{
		#region ���캯��
		/// <summary>
		/// 
		/// </summary>
		public clsMedStoreBseInfoSvc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

		#region ҩ����Ϣ

        #region ����ҩ����Ϣ
        /// <summary>
        /// ����ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewMedStore(            clsMedStore_VO p_objItem)
        {
            long lngRes = 0; 

            string strSQL = @"INSERT INTO t_bse_medstore
										  (medstoreid_chr, medstorename_vchr, medstoretype_int,
										   medicnetype_int,URGENCE_INT, deptid_chr,shortname_chr
										  )
								  VALUES (?,?,?,?,?,?,?)";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(7, out paramArr);
                paramArr[0].Value = p_objItem.m_strMedStoreID.Trim();
                paramArr[1].Value = p_objItem.m_strMedStoreName.Trim();
                paramArr[2].Value = p_objItem.m_intMedStoreType.ToString().Trim();
                paramArr[3].Value = p_objItem.m_intMedicneType.ToString().Trim();
                paramArr[4].Value = p_objItem.m_intUrgency;
                paramArr[5].Value = p_objItem.m_strDeptid;
                paramArr[6].Value = p_objItem.m_strMedStoreShortName;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

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

        #region �޸�ҩ����Ϣ
        /// <summary>
        /// �޸�ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objItem">ҩ������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdMedStoreByID(            clsMedStore_VO p_objItem)
        {
            long lngRes = 0; 

            string strSQL = @"update t_bse_medstore
   set medstorename_vchr = ?,
       medstoretype_int  = ?,
       medicnetype_int   = ?,
       urgence_int       = ?,
       deptid_chr        = ?,
       shortname_chr     = ?
 where medstoreid_chr = ?";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(7, out paramArr);
                paramArr[0].Value = p_objItem.m_strMedStoreName.Trim();
                paramArr[1].Value = p_objItem.m_intMedStoreType.ToString().Trim();
                paramArr[2].Value = p_objItem.m_intMedicneType.ToString().Trim();
                paramArr[3].Value = p_objItem.m_intUrgency;
                paramArr[4].Value = p_objItem.m_strDeptid;
                paramArr[5].Value = p_objItem.m_strMedStoreShortName;
                paramArr[6].Value = p_objItem.m_strMedStoreID.Trim();
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

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

		#region ɾ��ҩ����Ϣ
		/// <summary>
		/// ɾ��ҩ����Ϣ
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteMedStoreByID(			string p_strID)
		{
			long lngRes = 0;	 

			string strSQL = @"DELETE      t_bse_medstore
									WHERE medstoreid_chr = '" + p_strID.Trim() + "'";
			string strSQL1 = @"DELETE      t_bse_deptduty
									WHERE DEPTID_VCHR = '" + p_strID.Trim() + "'";

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);
				lngRes = objHRPSvc.DoExcute(strSQL1);
	

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;	
		}
		#endregion

		#region ģ����ѯҩ����Ϣ
		/// <summary>
		/// ģ����ѯҩ����Ϣ
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strSQL">SQL���</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedStoreByAny(			string p_strSQL,out clsMedStore_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStore_VO[0]; 

			DataTable dtbResult = null;
			string strSQL = @"SELECT *
							    FROM t_bse_medstore
							 " + p_strSQL;

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
				objHRPSvc.Dispose();

				if(lngRes>0 && dtbResult != null)
				{
					int intRow = dtbResult.Rows.Count;
					if(intRow>0)
					{
						p_objResultArr = new clsMedStore_VO[intRow];
						for(int i=0;i<intRow;i++)
						{
							p_objResultArr[i] = new clsMedStore_VO();
							p_objResultArr[i].m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
							p_objResultArr[i].m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
							p_objResultArr[i].m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
							p_objResultArr[i].m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                            if(dtbResult.Rows[i]["URGENCE_INT"]!=System.DBNull.Value)
							p_objResultArr[i].m_intUrgency= Convert.ToInt32(dtbResult.Rows[i]["URGENCE_INT"].ToString().Trim());
						
						}
					}
				}
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;	
		}
		#endregion

		#region ��ҩ�������ѯҩ����Ϣ
		/// <summary>
		/// ��ҩ�������ѯҩ����Ϣ
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ������</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreByID(			string p_strID,out clsMedStore_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStore_VO[0]; 

			string strSQL = " WHERE medstoreid_chr='" + p_strID.Trim() + "'";
			lngRes = m_lngGetMedStoreByAny(strSQL,out p_objResultArr);

			return lngRes;	
		}
		#endregion

		#region ��ҩ�����Ͳ�ѯҩ����Ϣ
		/// <summary>
		/// ��ҩ�����Ͳ�ѯҩ����Ϣ
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_intID">ҩ�����ʹ��룬1������ҩ����2��סԺҩ��</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreByStoreType(			int p_intID,out clsMedStore_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStore_VO[0]; 

			string strSQL = " WHERE medstoretype_int=" + p_intID.ToString().Trim() + " ";
			lngRes = m_lngGetMedStoreByAny(strSQL,out p_objResultArr);

			return lngRes;	
		}
		#endregion

		#region ��ҩƷ���Ͳ�ѯҩ����Ϣ
		/// <summary>
		/// ��ҩƷ���Ͳ�ѯҩ����Ϣ
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_intID">ҩƷ���ͣ�1����ҩ��2����ҩ</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreByMedicineType(			int p_intID,out clsMedStore_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStore_VO[0]; 

			string strSQL = " WHERE medicnetype_int=" + p_intID.ToString().Trim() + " ";
			lngRes = m_lngGetMedStoreByAny(strSQL,out p_objResultArr);

			return lngRes;	
		}
		#endregion

        #region ��ѯ����ҩ����Ϣ
        /// <summary>
        /// ��ѯ����ҩ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_objResultArr">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreList(            out clsMedStore_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsMedStore_VO[0];
            string strSQL = @"select a.medstoreid_chr,
			 a.medstorename_vchr,
			 a.medstoretype_int,
			 a.medicnetype_int,
			 a.urgence_int,
			 a.deptid_chr,
			 b.deptname_vchr,
			 a.shortname_chr
	from t_bse_medstore a, t_bse_deptdesc b
 where a.deptid_chr = b.deptid_chr(+)";

            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_objResultArr = new clsMedStore_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_objResultArr[i] = new clsMedStore_VO();
                            p_objResultArr[i].m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
                            p_objResultArr[i].m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
                            p_objResultArr[i].m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
                            if (dtbResult.Rows[i]["URGENCE_INT"] != System.DBNull.Value)
                                p_objResultArr[i].m_intUrgency = Convert.ToInt32(dtbResult.Rows[i]["URGENCE_INT"].ToString().Trim());
                            p_objResultArr[i].m_strDeptid = dtbResult.Rows[i]["deptid_chr"].ToString().Trim();
                            p_objResultArr[i].m_strDeptName = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
                            p_objResultArr[i].m_strMedStoreShortName = dtbResult.Rows[i]["shortname_chr"].ToString().Trim();
                        }
                    }
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

		#region �������ҩ������
		/// <summary>
		/// �������ҩ������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedStoreID(			out string p_strID)
		{
			long lngRes = 0;	
			p_strID = null; 

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				p_strID = objHRPSvc.m_strGetNewID("t_bse_medstore","medstoreid_chr",4);
				objHRPSvc.Dispose();

				if(p_strID != null)
				{
					if(Convert.ToInt32(p_strID.Trim()) <=0)
					{
						int ID = Convert.ToInt32(p_strID.Trim()) +1;
						p_strID = ID.ToString("0000");
					}
				}
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;	
		}
		#endregion

		#endregion

		#region ������Ϣ

		#region ����ҩ������
		/// <summary>
		/// ����ҩ������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_objItem">ҩ����������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewMedStoreWin(			clsOPMedStoreWin_VO p_objItem)
		{
			long lngRes = 0;	 

			string strSQL = @"INSERT INTO t_bse_medstorewin
										  (windowid_chr, windowname_vchr, medstoreid_chr,WINDOWTYPE_INT,WORKSTATUS_INT, winproperty_int
										  )
								  VALUES (?,?,?,?,?,?)";
										 

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out paramArr);
                paramArr[0].Value = p_objItem.m_strWindowID.Trim();
                paramArr[1].Value = p_objItem.m_strWindowName.Trim();
                paramArr[2].Value = p_objItem.m_objMedStore.m_strMedStoreID.ToString().Trim();
                paramArr[3].Value = p_objItem.m_intWindowType;
                paramArr[4].Value = p_objItem.m_intWorkStatus;
                paramArr[5].Value = p_objItem.m_strWinprop;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;		
		}
		#endregion

		#region �޸�ҩ������
		/// <summary>
		/// �޸�ҩ������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_objItem">��������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdMedStoreWin(			clsOPMedStoreWin_VO p_objItem)
		{
			long lngRes = 0;	 
	
            string strSQL = @"update t_bse_medstorewin set windowname_vchr = ?,medstoreid_chr = ?,windowtype_int =?, workstatus_int = ?, winproperty_int = ? where windowid_chr =?";
            
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

                if (p_objItem.m_strWinprop == "0")
                {
                    string strDel = "DELETE FROM t_bse_medstorewindeptdef WHERE MEDSTOREID_CHR='" + p_objItem.m_objMedStore.m_strMedStoreID.Trim() + "' AND WINDOWID_CHR='" + p_objItem.m_strWindowID.Trim() + "'";
                    lngRes = objHRPSvc.DoExcute(strDel);
                }

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out paramArr);
                paramArr[0].Value = p_objItem.m_strWindowName.Trim();
                paramArr[1].Value = p_objItem.m_objMedStore.m_strMedStoreID.Trim();
                paramArr[2].Value = p_objItem.m_intWindowType;
                paramArr[3].Value = p_objItem.m_intWorkStatus;
                paramArr[4].Value = p_objItem.m_strWinprop;
                paramArr[5].Value = p_objItem.m_strWindowID.Trim();
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region ɾ��ҩ������
		/// <summary>
		/// ɾ��ҩ������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ�����ڴ���</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteMedStoreWin(			string p_strID)
		{
			long lngRes = 0;		 

			string strSQL = @"DELETE t_bse_medstorewin
								    WHERE windowid_chr = '" + p_strID.Trim() + "'";

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);	

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region ģ����ѯҩ��������Ϣ
		/// <summary>
		/// ģ����ѯҩ��������Ϣ
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strSQL">SQL���</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreWinByAny(			string p_strSQL,out clsOPMedStoreWin_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsOPMedStoreWin_VO[0]; 

			DataTable dtbResult = null;
			string strSQL = @"SELECT a.*, b.medstorename_vchr, b.medstoretype_int, b.medicnetype_int
							    FROM t_bse_medstorewin a, t_bse_medstore b
							   WHERE a.medstoreid_chr = b.medstoreid_chr 
							" + p_strSQL;

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
				objHRPSvc.Dispose();

				if(lngRes>0 && dtbResult != null)
				{
					int intRow = dtbResult.Rows.Count;
					if(intRow>0)
					{
						p_objResultArr = new clsOPMedStoreWin_VO[intRow];
						for(int i=0;i<intRow;i++)
						{
							p_objResultArr[i] = new clsOPMedStoreWin_VO();
							p_objResultArr[i].m_objMedStore = new clsMedStore_VO();
							p_objResultArr[i].m_strWindowID = dtbResult.Rows[i]["windowid_chr"].ToString().Trim();
							p_objResultArr[i].m_strWindowName = dtbResult.Rows[i]["windowname_vchr"].ToString().Trim();
							p_objResultArr[i].m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
							p_objResultArr[i].m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
							p_objResultArr[i].m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
							p_objResultArr[i].m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());
	                        p_objResultArr[i].m_intWindowType=Convert.ToInt32( dtbResult.Rows[i]["WINDOWTYPE_INT"].ToString().Trim());
							p_objResultArr[i].m_intWorkStatus=Convert.ToInt32( dtbResult.Rows[i]["WORKSTATUS_INT"].ToString().Trim());
                            p_objResultArr[i].m_strWinprop = dtbResult.Rows[i]["winproperty_int"].ToString();
						}
					}
				}
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region �����ںŲ�ѯ������Ϣ
		/// <summary>
		/// �����ںŲ�ѯ������Ϣ
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">���ں�</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreWinByID(			string p_strID,out clsOPMedStoreWin_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsOPMedStoreWin_VO[0]; 

			string strSQL = @" AND a.windowid_chr='" + p_strID.Trim() + "'";
			lngRes = m_lngGetMedStoreWinByAny(strSQL,out p_objResultArr);

			return lngRes;		
		}
		#endregion

		#region ��ҩ����ѯ����
		/// <summary>
		/// ��ҩ����ѯ����
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ������</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreWinByMedStoreID(			string p_strID,out clsOPMedStoreWin_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsOPMedStoreWin_VO[0]; 

			string strSQL = @" AND a.medstoreid_chr='" + p_strID.Trim() + "'";
			lngRes = m_lngGetMedStoreWinByAny(strSQL,out p_objResultArr);

			return lngRes;
		}
		#endregion

		#region ��������ѯ������Ϣ    xgpeng 2006-2-15
		/// <summary>
		/// ��������ѯ������Ϣ
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_Type">��������</param>
		/// <param name="p_objResultArr"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreWinList(int p_Type,			out clsOPMedStoreWin_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsOPMedStoreWin_VO[0]; 
            string strSQL = "";
            if(p_Type!=2)
			strSQL = @" and a.WINDOWTYPE_INT="+p_Type+" order by a.medstoreid_chr " ;
			lngRes = m_lngGetMedStoreWinByAny(strSQL,out p_objResultArr);

			return lngRes;
		}
		#endregion

		#region ��ѯ���еĴ���
		/// <summary>
		/// ��ѯ���еĴ���
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreWinList(			out clsOPMedStoreWin_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsOPMedStoreWin_VO[0]; 

			string strSQL = @" ";
			lngRes = m_lngGetMedStoreWinByAny(strSQL,out p_objResultArr);

			return lngRes;
		}
		#endregion

		#region �õ���ǰ���Ĵ��ں�
		/// <summary>
		/// �õ���ǰ���Ĵ��ں�
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">���ں�</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreWinID(			out string p_strID)
		{
			long lngRes = 0;	
			p_strID = null; 

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				p_strID = objHRPSvc.m_strGetNewID("t_bse_medstorewin","windowid_chr",4);
				objHRPSvc.Dispose();

				if(p_strID != null)
				{
					if(Convert.ToInt32(p_strID.Trim()) <=0)
					{
						int ID = Convert.ToInt32(p_strID.Trim()) +1;
						p_strID = ID.ToString("0000");
					}
				}
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;	
		}
		#endregion

		#endregion

		#region ҩ���޶�

		#region ���ҩƷ��Ϣ
		/// <summary>
		/// ���ҩƷ��Ϣ
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="dtResult"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMed(out DataTable dtResult)
		{
			long lngRes = 0;			
			dtResult=null; 
			string strSQL=@"select MEDICINEID_CHR,MEDICINENAME_VCHR,OPUNIT_CHR,PYCODE_CHR,WBCODE_CHR from t_bse_medicine";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.DoGetDataTable(strSQL,ref dtResult);	

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region ����ҩ���޶�
		/// <summary>
		/// ����ҩ���޶�
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_objItem">ҩ���޶�����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewMedStoreLimit(			DataRow p_objItem)
		{
			long lngRes = 0;	 

			string strSQL = @"INSERT INTO t_bse_medstorelimit
										  (medstoreid_chr, medicineid_chr, lowlimit_dec, highlimit_dec,
										   planqty_dec, planpercent_dec, unitid_chr
										  )
								   VALUES (?,?,?,?,?,?,?)";

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(7, out paramArr);
                paramArr[0].Value = p_objItem["MEDSTOREID_CHR"].ToString();
                paramArr[1].Value = p_objItem["MEDICINEID_CHR"].ToString();
                paramArr[2].Value = p_objItem["lowlimit_dec"].ToString();
                paramArr[3].Value = p_objItem["highlimit_dec"].ToString();
                paramArr[4].Value = p_objItem["planqty_dec"].ToString();
                paramArr[5].Value = p_objItem["planpercent_dec"].ToString();
                paramArr[6].Value = p_objItem["unitid_chr"].ToString();
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region �޸�ҩ���޶�
		/// <summary>
		/// �޸�ҩ���޶�
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_objItem">ҩ���޶�����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdMedStoreLimitByID(			DataRow p_objItem)
		{
			long lngRes = 0;	 

			string strSQL = @"UPDATE t_bse_medstorelimit
								 SET lowlimit_dec = ?, highlimit_dec = ?, planqty_dec = ?, planpercent_dec = ?
                                 WHERE TRIM(medstoreid_chr) = ? AND TRIM(medicineid_chr) = ?";

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
	

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out paramArr);
                paramArr[0].Value = p_objItem["LOWLIMIT_DEC"].ToString();
                paramArr[1].Value = p_objItem["HIGHLIMIT_DEC"].ToString();
                paramArr[2].Value = p_objItem["PLANQTY_DEC"].ToString();
                paramArr[3].Value = p_objItem["PLANPERCENT_DEC"].ToString();
                paramArr[4].Value = p_objItem["MEDSTOREID_CHR"].ToString().Trim();
                paramArr[5].Value = p_objItem["MEDICINEID_CHR"].ToString().Trim();
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region ɾ��ҩ���޶�
		/// <summary>
		/// ɾ��ҩ���޶�
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strMedStoreID">ҩ������</param>
		/// <param name="p_strMedicineID">ҩƷ����</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteMedStoreLimitByID(			string p_strMedStoreID,string p_strMedicineID)
		{
			long lngRes = 0;	 

			string strSQL = @"DELETE  t_bse_medstorelimit
								    WHERE TRIM (medstoreid_chr) = '" + p_strMedStoreID.Trim() + "' AND TRIM (medicineid_chr) = '" + p_strMedicineID.Trim() + "'";

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);	

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region ���ҩ���޶�
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public long m_lngCheckMedStoreLimit()
		{
			long lngRes = 0;
			return lngRes;
		
		}
		#endregion

		#region ����ҩ��ID���ҩƷ�޶�
		/// <summary>
		/// ����ҩ��ID���ҩƷ�޶�
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="WinID"></param>
		/// <param name="p_objResultArr"></param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreLimitByAnyWinID(			string WinID,out DataTable p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr=new DataTable(); 

			string strSQL = @"SELECT a.MEDSTOREID_CHR,a.MEDICINEID_CHR,a.LOWLIMIT_DEC,a.HIGHLIMIT_DEC,a.PLANQTY_DEC,a.PLANPERCENT_DEC,a.UNITID_CHR, b.medstorename_vchr, b.medstoretype_int, b.medicnetype_int,
								     c.medicinename_vchr, c.pycode_chr, c.wbcode_chr
							    FROM t_bse_medstorelimit a, t_bse_medstore b, t_bse_medicine c
							   WHERE a.medstoreid_chr = b.medstoreid_chr
							     AND a.medicineid_chr = c.medicineid_chr(+)
								 AND a.MEDSTOREID_CHR='"+WinID+"'";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_objResultArr);
				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region ģ����ѯҩ���޶�
		/// <summary>
		/// ģ����ѯҩ���޶�
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strSQL">SQL���</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreLimitByAny(			string p_strSQL,out clsMedStoreLimit_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStoreLimit_VO[0]; 
			DataTable dtbResult = null;
			string strSQL = @"SELECT a.*, b.medstorename_vchr, b.medstoretype_int, b.medicnetype_int,
								     c.medicinename_vchr, c.pycode_chr, c.wbcode_chr, d.unitname_chr
							    FROM t_bse_medstorelimit a, t_bse_medstore b, t_bse_medicine c,
								     t_aid_unit d
							   WHERE a.medstoreid_chr = b.medstoreid_chr
							     AND a.medicineid_chr = c.medicineid_chr(+)
								 AND a.unitid_chr = d.unitid_chr(+) 
							" + p_strSQL;

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
				objHRPSvc.Dispose();

				if(lngRes >0 && dtbResult != null)
				{
					int intRow = dtbResult.Rows.Count;
					if(intRow>0)
					{
						p_objResultArr = new clsMedStoreLimit_VO[intRow];
						for(int i=0;i<intRow;i++)
						{
							p_objResultArr[i] = new clsMedStoreLimit_VO();
							p_objResultArr[i].m_objMedStore = new clsMedStore_VO();
							p_objResultArr[i].m_objMedicine = new clsMedicine_VO();
							p_objResultArr[i].m_objUnit = new clsUnit_VO();
							
							p_objResultArr[i].m_objMedStore.m_strMedStoreID = dtbResult.Rows[i]["medstoreid_chr"].ToString().Trim();
							p_objResultArr[i].m_objMedStore.m_strMedStoreName = dtbResult.Rows[i]["medstorename_vchr"].ToString().Trim();
							p_objResultArr[i].m_objMedStore.m_intMedStoreType = Convert.ToInt32(dtbResult.Rows[i]["medstoretype_int"].ToString().Trim());
							p_objResultArr[i].m_objMedStore.m_intMedicneType = Convert.ToInt32(dtbResult.Rows[i]["medicnetype_int"].ToString().Trim());

                            p_objResultArr[i].m_objMedicine.m_strMedicineID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
							p_objResultArr[i].m_objMedicine.m_strMedicineName = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
							p_objResultArr[i].m_objMedicine.m_strPYCode = dtbResult.Rows[i]["pycode_chr"].ToString().Trim();
							p_objResultArr[i].m_objMedicine.m_strWBCode = dtbResult.Rows[i]["wbcode_chr"].ToString().Trim();

							p_objResultArr[i].m_objUnit.m_strUnitID = dtbResult.Rows[i]["unitid_chr"].ToString().Trim();
							p_objResultArr[i].m_objUnit.m_strUnitName = dtbResult.Rows[i]["unitname_chr"].ToString().Trim();

							p_objResultArr[i].m_decLowLimit = float.Parse(dtbResult.Rows[i]["lowlimit_dec"].ToString().Trim());
							p_objResultArr[i].m_decHighLimit = float.Parse(dtbResult.Rows[i]["highlimit_dec"].ToString().Trim());
							p_objResultArr[i].m_decPlanQty = float.Parse(dtbResult.Rows[i]["planqty_dec"].ToString().Trim());
							p_objResultArr[i].m_decPlanPercent = float.Parse(dtbResult.Rows[i]["planpercent_dec"].ToString().Trim());

							
						}
					}
				}
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region ��ҩ����ѯҩ���޶�
		/// <summary>
		/// ��ҩ����ѯҩ���޶�
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ������</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreLimitByMedStore(			string p_strID,out clsMedStoreLimit_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStoreLimit_VO[0]; 
			string strSQL = @" AND TRIM(a.medstoreid_chr)='" + p_strID.Trim() + "'";
			lngRes = m_lngGetMedStoreLimitByAny(strSQL,out p_objResultArr);


			return lngRes;
		}
		#endregion

		#endregion

		#region ҩ����������

		#region ����ҩ����������
		/// <summary>
		/// ����ҩ����������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_objItem">ҩ��������������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewMedStoreOrdType(			clsMedStoreOrdType_VO p_objItem)
		{
			long lngRes = 0;	 

			string strSQL = @"INSERT INTO t_aid_medstoreordtype
									      (medstoreordtypeid_chr, medstoreordtype_vchr, sign_int,BEGINSTR_CHR,STORAGESIGN_INT
										  )
								   VALUES ('" + p_objItem.m_strMedStoreOrdTypeID.Trim() + "', '" + p_objItem.m_strMedStoreOrdTypeName.Trim() + "','" + p_objItem.m_intSign.ToString() + "','"+p_objItem.m_strBEGINSTR_CHR+"','"+p_objItem.m_intSTORAGESIGN.ToString()+"')";

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);	

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region �޸�ҩ����������
		/// <summary>
		/// �޸�ҩ����������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_objItem">ҩ��������������</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdMedStoreOrdTypeByID(			clsMedStoreOrdType_VO p_objItem)
		{
			long lngRes = 0;	 
			string strSQL = @"UPDATE t_aid_medstoreordtype
								 SET medstoreordtype_vchr =?,sign_int = ? , BEGINSTR_CHR = ?, STORAGESIGN_INT =? WHERE TRIM(medstoreordtypeid_chr) =?";

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out paramArr);
                paramArr[0].Value = p_objItem.m_strMedStoreOrdTypeName.Trim();
                paramArr[1].Value = p_objItem.m_intSign.ToString();
                paramArr[2].Value = p_objItem.m_strBEGINSTR_CHR;
                paramArr[3].Value = p_objItem.m_intSTORAGESIGN.ToString();
                paramArr[4].Value = p_objItem.m_strMedStoreOrdTypeID.Trim();
              
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;		
		}
		#endregion

		#region ɾ��ҩ����������
		/// <summary>
		/// ɾ��ҩ����������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ���������ʹ���</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteMedStoreOrdType(			string p_strID)
		{
			long lngRes = 0;	 

			string strSQL = @"DELETE  t_aid_medstoreordtype
									WHERE TRIM(medstoreordtypeid_chr) = '" + p_strID.Trim() + "'";

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.DoExcute(strSQL);	

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;		
		}
		#endregion

		#region ģ����ѯҩ����������
		/// <summary>
		/// ģ����ѯҩ����������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ����</param>
		/// <param name="p_strSQL">SQL���</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreOrdTypeByAny(			string p_strSQL,out clsMedStoreOrdType_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStoreOrdType_VO[0]; 

			DataTable dtbResult = null;
			string strSQL = @"SELECT *
								FROM t_aid_medstoreordtype
								" + p_strSQL;

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref dtbResult);
				objHRPSvc.Dispose();

				if(lngRes>0 && dtbResult != null)
				{
					int intRow = dtbResult.Rows.Count;
					if(intRow>0)
					{
						p_objResultArr = new clsMedStoreOrdType_VO[intRow];
						for(int i=0;i<intRow;i++)
						{
							p_objResultArr[i] = new clsMedStoreOrdType_VO();
							p_objResultArr[i].m_strMedStoreOrdTypeID = dtbResult.Rows[i]["medstoreordtypeid_chr"].ToString().Trim();
							p_objResultArr[i].m_strMedStoreOrdTypeName = dtbResult.Rows[i]["medstoreordtype_vchr"].ToString().Trim();
							p_objResultArr[i].m_intSign = Convert.ToInt32(dtbResult.Rows[i]["sign_int"].ToString().Trim());

                            p_objResultArr[i].m_strBEGINSTR_CHR = dtbResult.Rows[i]["BEGINSTR_CHR"].ToString().Trim();
                            p_objResultArr[i].m_intSTORAGESIGN = Convert.ToInt32(dtbResult.Rows[i]["STORAGESIGN_INT"].ToString().Trim());
						}
					}
				}
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
		#endregion

		#region ��ҩ���������ʹ����ѯ
		/// <summary>
		/// ��ҩ���������ʹ����ѯ
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ���������ʹ���</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreOrdTypeByID(			string p_strID,out clsMedStoreOrdType_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStoreOrdType_VO[0]; 

			string strSQL = @" WHERE TRIM(medstoreordtypeid_chr)='" + p_strID.Trim() + "'";
			lngRes = m_lngGetMedStoreOrdTypeByAny(strSQL,out p_objResultArr);			

			return lngRes;
		}
		#endregion

		#region ����־��ѯҩ����������
		/// <summary>
		/// ����־��ѯҩ����������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_intSign">��־</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreOrdTypeBySign(			int p_intSign,out clsMedStoreOrdType_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStoreOrdType_VO[0]; 

			string strSQL = @" WHERE sign_int=" + p_intSign.ToString() + "";
			lngRes = m_lngGetMedStoreOrdTypeByAny(strSQL,out p_objResultArr);			

			return lngRes;
		}
		#endregion

		#region ��ѯ���е�ҩ����������
		/// <summary>
		/// ��ѯ���е�ҩ����������
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_objResultArr">�������</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreOrdTypeList(			out clsMedStoreOrdType_VO[] p_objResultArr)
		{
			long lngRes = 0;
			p_objResultArr = new clsMedStoreOrdType_VO[0]; 

			string strSQL = @" ";
			lngRes = m_lngGetMedStoreOrdTypeByAny(strSQL,out p_objResultArr);			

			return lngRes;
		}
		#endregion

		#region ��ȡ��ǰ����ҩ����������ID
		/// <summary>
		/// ��ȡ��ǰ����ҩ����������ID
		/// </summary>
		/// <param name="p_objPrincipal">��ȫ��ʶ</param>
		/// <param name="p_strID">ҩ����������ID</param>
		/// <returns></returns>
        [AutoComplete]
		public long m_lngGetMedStoreOrdTypeID(			out string p_strID)
		{
			long lngRes = 0;	
			p_strID = null; 

			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				p_strID = objHRPSvc.m_strGetNewID("t_aid_medstoreordtype","medstoreordtypeid_chr",4);
				objHRPSvc.Dispose();

				if(p_strID != null)
				{
					if(Convert.ToInt32(p_strID.Trim()) <=0)
					{
						int ID = Convert.ToInt32(p_strID.Trim()) +1;
						p_strID = ID.ToString("0000");
					}
				}
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;	
		}
		#endregion

		#endregion


		#region ���ҩ����Ϣ(����)   xigui.peng  2006-2-9
		/// <summary>
		/// ���ҩ����Ϣ
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_dtable"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMedStoreInfo(out DataTable p_dtable)
		{
			long lngRes = 0;
			p_dtable=new DataTable(); 
            string strSQL = @"select e.medstoreid_chr,e.medstorename_vchr,e.MEDICNETYPE_INT from t_bse_medstore e";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtable);
				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;	
		}
		#endregion

		#region ����ҩ��ID��ȡҩ���Ű���Ϣ   xigui.peng  2006-2-9
	/// <summary>
	/// ����ҩ��ID��ȡҩ���Ű���Ϣ
	/// </summary>
	/// <param name="p_objPrincipal"></param>
	/// <param name="p_TypeID"></param>
	/// <param name="p_objResArr"></param>
	/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDeptDutyInfo(string p_TypeID,out clsMedDeptDuty_VO[] p_objResArr)
		{
			long lngRes = 0;
			p_objResArr=new clsMedDeptDuty_VO[0];
			DataTable p_dtRes=new DataTable(); 
			string strSQL = @"select a.*,b.medstorename_vchr  from  T_bse_Deptduty  a,t_bse_medstore b
                              where a.objectdeptid_vchr=b.medstoreid_chr(+) and ";
                  strSQL+=@"  a.deptid_vchr='"+p_TypeID.Trim()+"' order by a.WEEKDAY_INT";
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref p_dtRes);
				objHRPSvc.Dispose();
				if(lngRes>0 && p_dtRes != null)
				{
					int intRow = p_dtRes.Rows.Count;
					if(intRow>0)
					{
						p_objResArr = new clsMedDeptDuty_VO[intRow];
						for(int i=0;i<intRow;i++)
						{
							p_objResArr[i] = new clsMedDeptDuty_VO();
							p_objResArr[i].m_strSeq = Convert.ToInt32(p_dtRes.Rows[i]["SEQ_INT"].ToString().Trim());
							p_objResArr[i].m_strDeptID = p_dtRes.Rows[i]["DEPTID_VCHR"].ToString().Trim();
							p_objResArr[i].m_intTypeID = Convert.ToInt32(p_dtRes.Rows[i]["TYPEID_INT"].ToString().Trim());
                           // p_objResArr[i].m_strDeptName = p_dtRes.Rows[i]["MEDSTORENAME_VCHR"].ToString().Trim();
							p_objResArr[i].m_strObjectDeptID = p_dtRes.Rows[i]["OBJECTDEPTID_VCHR"].ToString().Trim();
							p_objResArr[i].m_strObjectDeptName= p_dtRes.Rows[i]["MEDSTORENAME_VCHR"].ToString().Trim();
							p_objResArr[i].m_strWorkTime= p_dtRes.Rows[i]["WORKTIME_VCHR"].ToString().Trim();
							p_objResArr[i].m_intWeekDay= Convert.ToInt32(p_dtRes.Rows[i]["WEEKDAY_INT"].ToString().Trim());
							p_objResArr[i].m_strRemark= p_dtRes.Rows[i]["REMARK_VCHR"].ToString().Trim();

						}
					}
				}
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;	
		}
		#endregion

		#region ����ҩ���Ű���Ϣ xigui.peng  2006-2-9
		/// <summary>
		/// ����ҩ���Ű���Ϣ
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_intSeq"></param>
		/// <param name="p_objDuty"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddDeptDutyInfo(out int p_intSeq,			clsMedDeptDuty_VO p_objDuty)
		{
			long lngRes = 0;	
			p_intSeq=0;//��ˮ�� 
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				p_intSeq =Convert.ToInt32( objHRPSvc.m_strGetNewID("T_bse_Deptduty","SEQ_INT",4));
                string strSQL = @"INSERT INTO T_bse_Deptduty
										  (SEQ_INT, TYPEID_INT, DEPTID_VCHR,
										   WEEKDAY_INT,WORKTIME_VCHR,OBJECTDEPTID_VCHR,REMARK_VCHR
										  )
								  VALUES (?,?,?,?,?,?,?)";


                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(7, out paramArr);
                paramArr[0].Value = p_intSeq;
                paramArr[1].Value = p_objDuty.m_intTypeID;
                paramArr[2].Value = p_objDuty.m_strDeptID.ToString().Trim();
                paramArr[3].Value = p_objDuty.m_intWeekDay;
                paramArr[4].Value = p_objDuty.m_strWorkTime;
                paramArr[5].Value = p_objDuty.m_strObjectDeptID.Trim();
                paramArr[6].Value = p_objDuty.m_strRemark.Trim();
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;

		}
		#endregion

		#region �޸�ҩ���Ű���Ϣ xigui.peng  2006-2-9
		/// <summary>
		/// �޸�ҩ���Ű���Ϣ
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_objWorkDuty"></param>
		/// <returns></returns>
		[AutoComplete]			
		public long m_thUpdateDeptDutyInfo(clsMedDeptDuty_VO p_objWorkDuty)
		{
			long lngRes = 0;	 
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				string strSQL = @"update  T_bse_Deptduty 
										  set  TYPEID_INT="+p_objWorkDuty.m_intTypeID+",DEPTID_VCHR='"+p_objWorkDuty.m_strDeptID+"',";
		strSQL+=@"								   WEEKDAY_INT="+p_objWorkDuty.m_intWeekDay+",WORKTIME_VCHR='"+p_objWorkDuty.m_strWorkTime+"',OBJECTDEPTID_VCHR='"+p_objWorkDuty.m_strObjectDeptID+"',REMARK_VCHR='"
                                                   +p_objWorkDuty.m_strRemark+"'";
    	strSQL += @" where SEQ_INT="+p_objWorkDuty.m_strSeq;
        lngRes = objHRPSvc.DoExcute(strSQL);	

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;

		}
		#endregion

		#region ɾ��ҩ���Ű���Ϣ xigui.peng  2006-2-9
		/// <summary>
		/// ɾ��ҩ���Ű���Ϣ
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_intID"></param>
		/// <returns></returns>
		[AutoComplete]			
		public long m_thDelDeptDutyInfo(int p_intID)
		{
			long lngRes = 0;	 
			try
			{
				com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
				string strSQL = @"delete  T_bse_Deptduty"; 
															
				strSQL += @" where SEQ_INT="+p_intID;
				lngRes = objHRPSvc.DoExcute(strSQL);	

				objHRPSvc.Dispose();
			}		
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;

		}
		#endregion

        #region  ҩ��ר�ô�������Ҷ�Ӧ������
        #region ҩ��ר�ô�������Ҷ�Ӧ������
        /// <summary>
        /// ���ҩ��ר�ô�������Ҷ�Ӧ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreWinDeptDefInfo( string p_strMedStoreId, string p_strWindowId, out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = new DataTable(); 
            string strSQL = @"SELECT tc.code_vchr, tc.deptname_vchr, tc.deptid_chr, ta.medstoreid_chr,
                                     ta.windowid_chr
                                     FROM t_bse_medstorewindeptdef ta, t_bse_deptdesc tc
                                        WHERE ta.deptid_chr = tc.deptid_chr(+) AND ta.medstoreid_chr = '" + p_strMedStoreId + "'  AND ta.windowid_chr = '" + p_strWindowId + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtable);
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

        #region ȡ������
        /// <summary>
        /// ȡ������
        /// </summary>
        [AutoComplete]
        public long m_lngGeDataTableInfo(string p_sql, out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(p_sql, ref p_dtable);
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

        #region ɾ��ҩ��ר�ô�������Ҷ�Ӧ
        /// <summary>
        /// ɾ��ҩ��ר�ô�������Ҷ�Ӧ
        /// </summary>
        [AutoComplete]
        public long m_lngDeleteMEDSTOREWINDEPT(            clsMEDSTOREWINDEPTDEF_VO[] p_VO)
        {
            long lngRes = 0; 
            string strSQL = "";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                for (int i = 0; i < p_VO.Length; i++)
                {
                    strSQL = @"DELETE   T_BSE_MEDSTOREWINDEPTDEF    WHERE MEDSTOREID_CHR = '" + p_VO[i].m_strMEDSTOREID_CHR + "' and WINDOWID_CHR='" + p_VO[i].m_strWINDOWID_CHR + "' and DEPTID_CHR='" + p_VO[i].m_strDEPTID_CHR + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);
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

        #region ����ҩ��ר�ô�������Ҷ�Ӧ
        /// <summary>
        /// ����ҩ��ר�ô�������Ҷ�Ӧ
        /// </summary>
        [AutoComplete]
        public long m_lngInsertMEDSTOREWINDEPT(            clsMEDSTOREWINDEPTDEF_VO[] p_VO)
        {
            long lngRes = 0; 
            string strSQL = "";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                for (int i = 0; i < p_VO.Length; i++)
                {
                    if (p_VO[i] != null)
                    {
                        strSQL = @"INSERT INTO t_bse_medstorewindeptdef  (medstoreid_chr, windowid_chr, deptid_chr ) VALUES ('" + p_VO[i].m_strMEDSTOREID_CHR + "', '" + p_VO[i].m_strWINDOWID_CHR + "', '" + p_VO[i].m_strDEPTID_CHR + "')";
                        lngRes = objHRPSvc.DoExcute(strSQL);
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
        #endregion 
        #region ����ҩ�����ͻ��ҩ����Ϣ(����)
        /// <summary>
        /// ����ҩ�����ͻ��ҩ����Ϣ(����)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreInfoByMedStoreType( out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = new DataTable(); 
            string strSQL = @"select e.medstoreid_chr,e.medstorename_vchr from t_bse_medstore e where e.MEDSTORETYPE_INT=2 ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtable);
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
        #region ��ȡȫ��������Ϣ
        /// <summary>
        ///  ��ȡȫ��������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaInformation( out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = new DataTable(); 
            string strSQL = @"SELECT a.deptid_chr,a.deptname_vchr
                              FROM t_bse_deptdesc a, t_bse_deptattribute b
                             WHERE a.attributeid = b.ID AND (b.ID = '0000003' or a.putmed_int=1) ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtable);
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
        #region ��������ҩ��id��ȡ��Ӧ����������Ϣ
        /// <summary>
        /// ��������ҩ��id��ȡ��Ӧ����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="p_dtable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaInformationByMedStoreID(string m_strMedStoreID,out DataTable p_dtable)
        {
            long lngRes = 0;
            p_dtable = new DataTable(); 
            string strSQL = @"SELECT a.areaid_chr, b.deptname_vchr,a.ORDER_INT
                              FROM t_bse_area_medstore_rlt a, t_bse_deptdesc b
                              WHERE a.medstoreid_chr = '" + m_strMedStoreID + "' AND a.areaid_chr = b.deptid_chr(+) AND A.status_int=1 order by a.ORDER_INT";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtable);
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
        #region ��������ҩ��id������Ӧ����������Ϣ
        /// <summary>
        /// ��������ҩ��id������Ӧ����������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertMedStoreAreaRelation(clsMedStoreVsArea m_objData)
        {
            long lngRes = 0; 
            DataTable m_objTable = new DataTable();
            int m_intMaxsequence=0;
            string strGetMaxIDSQL = @"select max(A.seq_int)+1 as maxid from t_bse_area_medstore_rlt A ";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            try
            {
                lngRes=objHRPSvc.lngGetDataTableWithoutParameters(strGetMaxIDSQL, ref m_objTable);
                if (lngRes > 0)
                {  
                    if(m_objTable.Rows[0][0].ToString().Trim()!=string.Empty)
                    {
                        m_intMaxsequence = int.Parse(m_objTable.Rows[0][0].ToString().Trim());
                    }
                    else
                    {
                        m_intMaxsequence = 1;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            string strSQL = @"insert into t_bse_area_medstore_rlt(SEQ_INT,MEDSTOREID_CHR,AREAID_CHR,CREATORID_CHR,CREAT_DAT,STATUS_INT,CANCELERID_CHR,CANCEL_DAT,ORDER_INT)
                              values(" + m_intMaxsequence + ",'" + m_objData.m_strMEDSTOREID_CHR + "','" + m_objData.m_strAREAID_CHR + "','" + m_objData.m_strCreateID + "',to_date('"+m_objData.m_datCreateTime+"','yyyy-mm-dd hh24:mi:ss')," + m_objData.m_intStatusINT + ",'',to_date('','yyyy-mm-dd hh24:mi:ss')," + m_objData.m_intORDER_INT + ")";
            try
            {

                lngRes = objHRPSvc.DoExcute(strSQL);
           
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }

            return lngRes;
        }
        #endregion 

        #region ��������ҩ����Ӧ�����ļ�¼
        /// <summary>
        /// ��������ҩ����Ӧ�����ļ�¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long  m_lngUpdateMedStoreVsAreaInfo(clsMedStoreVsArea m_objVO)
        {
            long lngRes = 0; 
            string strSQL = @"update t_bse_area_medstore_rlt A set A.status_int="+m_objVO.m_intStatusINT+",A.cancelerid_chr='"+m_objVO.m_strCANCELERID_CHR+"',A.cancel_dat=to_date('"+m_objVO.m_datCANCEL_DAT.ToString()+"','yyyy-mm-dd hh24:mi:ss') where A.medstoreid_chr='"+m_objVO.m_strMEDSTOREID_CHR+"'and A.areaid_chr='"+m_objVO.m_strAREAID_CHR+"'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region ��������ҩ����Ӧ������˳���
        /// <summary>
        ///  ��������ҩ����Ӧ������˳���
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_objVOArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateOrderOfTable( clsMedStoreVsArea[] m_objVOArr)
        {
            long lngRes = 0; 
            string strSQL = "";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                for (int i = 0; i < m_objVOArr.Length; i++)
                {
                    strSQL = @"update t_bse_area_medstore_rlt A set A.order_int=" + m_objVOArr[i].m_intORDER_INT + " where A.medstoreid_chr='" + m_objVOArr[i].m_strMEDSTOREID_CHR + "'and A.areaid_chr='" + m_objVOArr[i].m_strAREAID_CHR + "'";
                    lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region ����ҩ���к�������Ϣ��
        /// <summary>
        /// ����ҩ���к�������Ϣ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strWindowID"></param>
        /// <param name="m_strCallContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsertMedStoreCallQue(string m_strMedStoreID,string m_strWindowID,string m_strCallContent)
        {
            long lngRes = 0; 
            DataTable m_objTable = new DataTable();
            int m_intMaxsequence = 0;
            string strGetMaxIDSQL = @"select max(A.seq_int)+1 as maxid from T_OPR_MEDSTORECALLQUE A ";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strGetMaxIDSQL, ref m_objTable);
                if (lngRes > 0)
                {
                    if (m_objTable.Rows[0][0].ToString().Trim() != string.Empty)
                    {
                        m_intMaxsequence = int.Parse(m_objTable.Rows[0][0].ToString().Trim());
                    }
                    else
                    {
                        m_intMaxsequence = 1;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            
            string strSQL = @"insert into T_OPR_MEDSTORECALLQUE(SEQ_INT,MEDSTOREID_CHR,WINDOWID_CHR,CALLDESC_VCHR)
                              values(" + m_intMaxsequence + ",'" + m_strMedStoreID + "','" + m_strWindowID + "','" + m_strCallContent+ "')";
            try
            {

                lngRes = objHRPSvc.DoExcute(strSQL);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }

            return lngRes;
        }
        #endregion
        #region ���ݲ���ҩ��id�ʹ���idɾ��ҩ���к�������Ϣ��
        /// <summary>
        /// ���ݲ���ҩ��id�ʹ���idɾ��ҩ���к�������Ϣ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strWindowsID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelMedStoreCallInfoByID(string m_strMedStoreID,string m_strWindowsID)
        {
            long lngRes = 0; 
            string strSQL = @"DELETE t_opr_medstorecallque a  WHERE a.medstoreid_chr = '"+m_strMedStoreID+"' AND a.windowid_chr = '"+m_strWindowsID+"'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);
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
        #region ���ݲ���ҩ��id�ʹ���id��ȡҩ���к�������Ϣ��
        /// <summary>
        ///  ���ݲ���ҩ��id�ʹ���id��ȡҩ���к�������Ϣ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strWindowsID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCallInfoByID( string m_strMedStoreID, string m_strWindowsID,out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = null; 
            string strSQL = @"select * from  t_opr_medstorecallque a  WHERE a.medstoreid_chr = '" + m_strMedStoreID + "' AND a.windowid_chr = '" + m_strWindowsID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL,ref m_objTable);
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
        #region ���ݲ���ҩ��id��ȡҩ���к�������Ϣ��
        /// <summary>
        /// ���ݲ���ҩ��id��ȡҩ���к�������Ϣ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreCallInfoByID( string m_strMedStoreID, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = null; 
            string strSQL = @"select * from  t_opr_medstorecallque a  WHERE a.medstoreid_chr = '" + m_strMedStoreID + "'order by A.seq_int";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_objTable);
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
        #region ���ݲ���ID��ȡ��ҩ�������δ��ҩ��Ϣ
        /// <summary>
        /// ���ݲ���ID��ȡ��ҩ�������δ��ҩ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strCurrentDataTime"></param>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_objTable"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStoreSendInfo( string m_strCurrentDataTime,string m_strMedStoreID, out DataTable m_objTable)
        {
            long lngRes = 0;
            m_objTable = null;
            string m_strFromDataTime=m_strCurrentDataTime+" 00:00:00";
            string m_strToDataTime=m_strCurrentDataTime+" 23:59:59"; 
            string strSQL = @"SELECT   a.sendwindowid_chr as sendwindowid, a.pstatus_int, a.senddate_dat,
         b.pstauts_int AS breakpstatus,
         a.windowid_chr, c.name_vchr,
         a.medstoreid_chr
    FROM t_opr_recipesend a,
         t_opr_recipesendentry i,
         t_opr_outpatientrecipe b,
         t_bse_patientidx c,
         t_opr_patientregister d,
         t_bse_patientcard e,
         t_bse_patientpaytype f,
         (SELECT c.recorddate_dat, c.invoiceno_vchr, c.outpatrecipeid_chr,
                 c.totalsum_mny, c.opremp_chr, c.status_int, c.split_int
            FROM (SELECT   MAX (seqid_chr) AS seqid_chr, outpatrecipeid_chr
                      FROM t_opr_outpatientrecipeinv
                     WHERE recorddate_dat
                              BETWEEN TO_DATE (?,
                                               'yyyy-mm-dd hh24:mi:ss'
                                              )
                                  AND TO_DATE (?,
                                               'yyyy-mm-dd hh24:mi:ss'
                                              )
                  GROUP BY outpatrecipeid_chr) b,
                 t_opr_outpatientrecipeinv c
           WHERE b.seqid_chr = c.seqid_chr) j,
         t_opr_reciperelation h,
         t_bse_employee g,
         t_bse_employee k,
         t_bse_employee m,
         t_bse_patient p,
         t_aid_recipetype r
   WHERE a.sid_int = i.sid_int
     AND i.outpatrecipeid_chr = b.outpatrecipeid_chr
     AND b.registerid_chr = d.registerid_chr(+)
     AND b.patientid_chr = c.patientid_chr
     AND b.deptmed_int = 0
     AND i.outpatrecipeid_chr = h.outpatrecipeid_chr
     AND h.seqid = j.outpatrecipeid_chr
     AND a.treatemp_chr = g.empid_chr(+)
     AND a.SENDEMP_CHR = m.empid_chr(+)
     AND j.opremp_chr = k.empid_chr
     AND b.type_int = r.type_int(+)
     AND b.patientid_chr = p.patientid_chr
     AND b.patientid_chr = e.patientid_chr(+)
     AND b.paytypeid_chr = f.paytypeid_chr
     AND b.pstauts_int != -2
     AND a.pstatus_int = 2 
     AND a.createdate_chr =?
     AND a.medstoreid_chr=? ORDER BY a.serno_chr DESC";
            
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = m_strFromDataTime;
                objLisAddItemRefArr[1].Value = m_strToDataTime;
                objLisAddItemRefArr[2].Value = Convert.ToDateTime(m_strCurrentDataTime).ToString("yyyy-MM-dd");
                objLisAddItemRefArr[3].Value = m_strMedStoreID.Trim();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_objTable, objLisAddItemRefArr);
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
