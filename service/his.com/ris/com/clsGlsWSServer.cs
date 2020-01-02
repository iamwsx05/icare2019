using System;
using System.Collections;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility; 
using System.Collections.Generic;
using weCare.Core.Entity;


namespace com.digitalwave.GLS_WS.GlsWSServer
{
	/// <summary>
	/// 病人管理类（针对全院）
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsGlsWSServer : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {

        [AutoComplete]
        public long m_lngSaveBultraSoundID(string p_strBultraSoundID, string p_strPatientID, string p_strTime)
        {
            string strSql = @"insert into ar_content
            (recordid,
             controlid, ctl_content, ctl_content_xml
            )
            values ((select recordid
                from ar_apply_report
               where formclsname = ?
                 and patientid = ?
                 and ceateddate = to_date (?, 'yyyy-mm-dd hh24:mi:ss')),
             ?, ?, ?
            )";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out objDPArr);
                objDPArr[0].Value = "frmBultrasoundWorkStation";
                objDPArr[1].Value = p_strPatientID;
                objDPArr[2].Value = p_strTime;
                objDPArr[3].Value = "m_txtBultraSoundID";
                objDPArr[4].Value = p_strBultraSoundID;
                objDPArr[5].Value = "";
                long lngAfft = 0;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSql, ref lngAfft, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            //返回
            return lngRes;
        }


        /// <summary>
        /// 更改申请单标志(暂时： Ar_Common_Apply.Submitted = 2 "正式登记信息标志")
        /// </summary>
        /// <param name="p_strApplyID"></param>
        [AutoComplete]
        public void m_mthUpdateApply(string p_strApplyID)
        {
            long lngAffected = -1;
            string strSQL = @"update ar_common_apply
   set submitted = 2
 where applyid = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = p_strApplyID;
                objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffected, objLisAddItemRefArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }

		#region 保存数据
		/// <summary>
		/// 保存数据
		/// </summary>
		/// <param name="p_arlSQL"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveData(List<string> p_arlSQL, string p_strTableName, string p_strColName, string p_flag)
		{
			long lngRes=-1;

			clsHRPTableService objHRPServer=new clsHRPTableService();
            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
			try
			{
				for(int i=0;i<p_arlSQL.Count;i++)
				{
					if (p_arlSQL[i].ToString().Trim().Length>0)
					{					
						lngRes=objHRPServer.DoExcute(p_arlSQL[i].ToString());					
						if (lngRes<=0)
						{
							System.EnterpriseServices.ContextUtil.SetAbort();
						}
					}
				}	
				if(p_flag.ToLower() == "add")
				{
					long l = m_mthUpdateMaxPathologyID(p_strTableName, p_strColName);
					if(l < 0)
					{
						System.EnterpriseServices.ContextUtil.SetAbort();
					}
				}
			}
			catch(Exception objEx)
			{
				string strTmp = objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
            objHRPServer.Dispose();
			return lngRes;
		}
		#endregion
        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="m_objList"></param>
        /// <param name="p_strTableName"></param>
        /// <param name="p_strColName"></param>
        /// <param name="p_flag"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveData(List<clsSQLAndParams> m_objList, string p_strTableName, string p_strColName, string p_flag)
        {
            long lngRes = -1;
            long lngAffected = -1;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
            System.Data.IDataParameter[] m_objDataParas = null;
            try
            {
                for (int i = 0; i < m_objList.Count; i++)
                {
                    objHRPServer.CreateDatabaseParameter(m_objList[i].m_objArr.Length, out m_objDataParas);
                    for (int j = 0; j < m_objList[i].m_objArr.Length; j++)
                    {
                        m_objDataParas[j].Value = m_objList[i].m_objArr[j];
                    }
                    //lngRes = objHRPServer.DoExcute(p_arlSQL[i].ToString());
                    lngRes = objHRPServer.lngExecuteParameterSQL(m_objList[i].m_strSQL, ref lngAffected, m_objDataParas);
                    if (lngRes <= 0)
                    {
                        System.EnterpriseServices.ContextUtil.SetAbort();
                    }

                }
                if (p_flag.ToLower() == "add")
                {
                    long l = m_mthUpdateMaxPathologyID(p_strTableName, p_strColName);
                    if (l < 0)
                    {
                        System.EnterpriseServices.ContextUtil.SetAbort();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPServer.Dispose();
            return lngRes;
        }
        #endregion
		

		
		#region 保存图像
		/// <summary>
		/// 保存图像
		/// </summary>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveImage(string m_strPathologyID,  clsPictureBoxValue[] p_objPictureBoxValue)
		{				
			string m_strRecordID = new clsGls_QuerySvc().m_strRecordID(m_strPathologyID);

			if(m_strRecordID.Trim() == "")
			{ 
				return -1; 
			}

			long lngEff = 0;
			long lngRet = 0;
			clsHRPTableService objHRPServer = new clsHRPTableService();
            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
			IDataParameter[] objDPArr3 = null;

			string SQL = @"insert into ar_image
            (recordid, controlid, imageid, imagecontent, height, width
            )
     values (?, '0', ?, ?, ?, ?
            )";	

			for (int i=0; i<p_objPictureBoxValue.Length; i++)
			{				
				objHRPServer.CreateDatabaseParameter(5, out objDPArr3);
                objDPArr3[0].Value = m_strRecordID;
				objDPArr3[1].Value= p_objPictureBoxValue[i].intImgID.ToString();
                if (p_objPictureBoxValue[i].m_bytImage != null)
                {
                    objDPArr3[2].Value = p_objPictureBoxValue[i].m_bytImage;
                }
                else
                {
                    objDPArr3[2].Value = DBNull.Value;
                }
				objDPArr3[3].Value = p_objPictureBoxValue[i].intHeight;
				objDPArr3[4].Value = p_objPictureBoxValue[i].intWidth;				

				try
				{
					lngRet = objHRPServer.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr3);
				}
				catch(Exception objEx)
				{
					string strTemp = objEx.Message;
					com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
					bool blnRes = objLogger.LogError(objEx);
				}
				if(lngRet <= 0)	
				{
					return lngRet;
				}
			}
            objHRPServer.Dispose();
			return 1;
		}

		[AutoComplete]
		public long m_lngSaveImage(string p_strPathologyID, string p_strImageID, byte[] p_bteImageArr, int p_intWidth, int p_intHeight)
		{
			long lngRet = 0;
			long lngEff = 0;
            string m_strRecordID = new clsGls_QuerySvc().m_strRecordID(p_strPathologyID);

			if(m_strRecordID.Trim() == "")
			{ 
				return -1; 
			}		

			string SQL = @"update ar_image
   set imagecontent = ?,
       width = ?,
       height = ?
 where recordid = ? and imageid = ?";
			clsHRPTableService objHRPServer = new clsHRPTableService();

            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			IDataParameter[] objDPArr = null;

			objHRPServer.CreateDatabaseParameter(5, out objDPArr);

			objDPArr[0].Value = p_bteImageArr;
			objDPArr[1].Value = p_intWidth;
			objDPArr[2].Value = p_intHeight;
			objDPArr[3].Value = m_strRecordID;			
			objDPArr[4].Value = p_strImageID;
			
			try
			{
				lngRet = objHRPServer.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr);
			}
			catch(Exception objEx)
			{
				string strTemp = objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			if(lngRet <= 0)	
			{
				return lngRet;
			}
            objHRPServer.Dispose();
			return 1;
		}

		
		#endregion (保存图像结束)
        #region 保存B超图像
        /// <summary>
        /// 保存B超图像
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveBultraImage(string m_strBultraSoundID,  clsPictureBoxValue[] p_objPictureBoxValue)
        {
            string m_strRecordID = new clsGls_QuerySvc().m_strRecordIDByBultraNo(m_strBultraSoundID);

            if (m_strRecordID.Trim() == "")
            {
                return -1;
            }

            long lngEff = 0;
            long lngRet = 0;
            clsHRPTableService objHRPServer = new clsHRPTableService();

            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            IDataParameter[] objDPArr3 = null;

            string SQL = @"insert into ar_image
            (recordid, controlid, imageid, imagecontent, height, width
            )
     values (?, '0', ?, ?, ?, ?
            )";

            for (int i = 0; i < p_objPictureBoxValue.Length; i++)
            {
                objHRPServer.CreateDatabaseParameter(5, out objDPArr3);
                objDPArr3[0].Value = m_strRecordID;
                objDPArr3[1].Value = p_objPictureBoxValue[i].intImgID.ToString();
                if (p_objPictureBoxValue[i].m_bytImage != null)
                    objDPArr3[2].Value = p_objPictureBoxValue[i].m_bytImage;
                else
                    objDPArr3[2].Value = DBNull.Value;
                objDPArr3[3].Value = p_objPictureBoxValue[i].intHeight;
                objDPArr3[4].Value = p_objPictureBoxValue[i].intWidth;

                try
                {
                    lngRet = objHRPServer.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr3);
                }
                catch (Exception objEx)
                {
                    string strTemp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (lngRet <= 0)
                {
                    return lngRet;
                }
            }
            objHRPServer.Dispose();
            return 1;
        }
        [AutoComplete]
        public long m_lngSaveBultraImage(string p_strBultraSoundID, string p_strImageID, byte[] p_bteImageArr, int p_intWidth, int p_intHeight)
        {
            long lngRet = 0;
            long lngEff = 0;
            string m_strRecordID = new clsGls_QuerySvc().m_strRecordIDByBultraNo(p_strBultraSoundID);

            if (m_strRecordID.Trim() == "")
            {
                return -1;
            }

            string SQL = @"update ar_image
   set imagecontent = ?,
       width = ?,
       height = ?
 where recordid = ? and imageid = ?";
            clsHRPTableService objHRPServer = new clsHRPTableService();

            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            IDataParameter[] objDPArr = null;

            objHRPServer.CreateDatabaseParameter(5, out objDPArr);

            objDPArr[0].Value = p_bteImageArr;
            objDPArr[1].Value = p_intWidth;
            objDPArr[2].Value = p_intHeight;
            objDPArr[3].Value = m_strRecordID;
            objDPArr[4].Value = p_strImageID;

            try
            {
                lngRet = objHRPServer.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr);
                objHRPServer.Dispose();
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRet <= 0)
            {
                return lngRet;
            }
            return 1;
        }
        #endregion (保存图像结束)

		#region 删除图像
		/// <summary>
		/// 删除图像
		/// </summary>
		/// <param name="p_strPathologyID"></param>
		/// <param name="p_strImageID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteImage(string p_strPathologyID, string p_strImageID)
		{
			long lngRet = 0;
            string m_strRecordID = new clsGls_QuerySvc().m_strRecordID(p_strPathologyID);

			if(m_strRecordID.Trim() == "")
			{ 
				return -1; 
			}

            string SQL = @"delete from ar_image
      where recordid = ? and imageid = ?";
            long lngAffected = -1;
			clsHRPTableService objHRPServer = new clsHRPTableService();	
			try
			{

                objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                IDataParameter[] objDPArr = null;
                objHRPServer.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = m_strRecordID;
                objDPArr[1].Value = p_strImageID;
                lngRet=objHRPServer.lngExecuteParameterSQL(SQL, ref lngAffected, objDPArr);
                objHRPServer.Dispose();
			}
			catch(Exception objEx)
			{
				string strTemp = objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
           
			if(lngRet <= 0)	
			{
				return lngRet;
			}

			return 1;
		}
		#endregion (删除图像结束)
        #region 删除B超图像
        /// <summary>
        /// 删除B超图像
        /// </summary>
        /// <param name="p_strPathologyID"></param>
        /// <param name="p_strImageID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteBultraSoundImage(string p_strBultraSoundID, string p_strImageID)
        {
            long lngRet = 0;
            string m_strRecordID = new clsGls_QuerySvc().m_strRecordIDByBultraNo(p_strBultraSoundID);

            if (m_strRecordID.Trim() == "")
            {
                return -1;
            }

            string SQL = @"delete from ar_image
      where recordid = ? and imageid = ?";
            long lngAffected = -1;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            try
            {

                objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
                IDataParameter[] objDPArr = null;
                objHRPServer.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = m_strRecordID;
                objDPArr[1].Value = p_strImageID;
                lngRet = objHRPServer.lngExecuteParameterSQL(SQL, ref lngAffected, objDPArr);
                objHRPServer.Dispose();
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
          
            if (lngRet <= 0)
            {
                return lngRet;
            }
            return 1;
        }
        #endregion (删除图像结束)

		#region 更新最大病理号
		/// <summary>
		/// 更新最大病理号
		/// </summary>
		/// <param name="strTableName"></param>
		/// <param name="strColName"></param>				
		public long m_mthUpdateMaxPathologyID(string strTableName, string strColName)
		{
			long lngRet = -1;
			string strCurrID = new clsGls_QuerySvc().m_strGetMaxPathologyID(strTableName, strColName);
			if(strCurrID == "编号重复")
			{
				return lngRet;
			}

			long lngID = 0;
			try
			{
				lngID = long.Parse(strCurrID) + 1;
			}
			catch{ return lngRet; }

            string SQL = @"update t_aid_table_sequence_id
                           set max_sequence_id_chr = ?
                           where lower (trim (table_name_vchr)) = ? and lower (trim (col_name_vchr)) = ?";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngAffected = -1;
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = lngID.ToString();
                objLisAddItemRefArr[1].Value = strTableName.Trim().ToLower();
                objLisAddItemRefArr[2].Value = strColName.Trim().ToLower();
                lngRet = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffected, objLisAddItemRefArr);
            }
			catch(Exception objEx)
			{
				string strTemp = objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);				
			}	
			return lngRet;
		}
		#endregion				
        #region 更新最大B超号
        /// <summary>
        /// 更新最大B超号
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strColName"></param>				
        public long m_mthUpdateMaxBultraSoundID(string strTableName, string strColName)
        {
            long lngRet = -1;
            string strCurrID = new clsGls_QuerySvc().m_strGetMaxBultralSoundID(strTableName, strColName);
            if (strCurrID == "编号重复")
            {
                return lngRet;
            }

            long lngID = 0;
            try
            {
                lngID = long.Parse(strCurrID) + 1;
            }
            catch { return lngRet; }

            string SQL = @"update t_aid_table_sequence_id
                           set max_sequence_id_chr = ?
                           where lower (trim (table_name_vchr)) = ? and lower (trim (col_name_vchr)) = ?";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            long lngAffected = -1;
            try
            {
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = lngID.ToString();
                objLisAddItemRefArr[1].Value = strTableName.Trim().ToLower();
                objLisAddItemRefArr[2].Value = strColName.Trim().ToLower();
                lngRet = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffected, objLisAddItemRefArr);
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRet;
        }
        #endregion				

		#region 复用图像
		/// <summary>
		/// 复用图像
		/// </summary>
		/// <param name="p_strOldRecordID"></param>
		/// <param name="p_strNewRecordID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngReUseImage(string p_strOldRecordID, string p_strNewRecordID)
		{
			long lngRet = 0;
			long lngAffected=0;
            string SQL = @"insert into ar_image
            (recordid, controlid, imageid, imagecontent, height, width)
   select ?, controlid, imageid, imagecontent, height, width
     from ar_image
    where recordid = ?";			
			clsHRPTableService objHRPServer = new clsHRPTableService();

            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            System.Data.IDataParameter[] m_objDataParaArr = null;
            objHRPServer.CreateDatabaseParameter(2, out m_objDataParaArr);
            m_objDataParaArr[0].Value = p_strNewRecordID;
            m_objDataParaArr[1].Value = p_strOldRecordID;
			try
			{
                lngRet = objHRPServer.lngExecuteParameterSQL(SQL, ref lngAffected, m_objDataParaArr);
                objHRPServer.Dispose();
			}
			catch(Exception objEx)
			{
				string strTemp = objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}				
			if(lngRet <= 0)	
			{
				return lngRet;
			}
			return 1;
		}
		#endregion (复用图像)

		#region 更新备注
		/// <summary>
		/// 更新备注
		/// </summary>
		/// <param name="strTableName"></param>
		/// <param name="strColName"></param>	
		[AutoComplete]	
		public long m_mthUpdateRemark_vchrbyID(string p_strRemark_vchr, string p_strRecordid,string p_strControlid,string p_strImageid)
		{

            string SQL = @"update ar_image
                           set remark_vchr = ?
                           where recordid = ? and controlid = ? and imageid = ?";
			long lngResult = 0;
            long lngAffected=0;
            clsHRPTableService objHRPServer = new clsHRPTableService();

             objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            System.Data.IDataParameter[] m_objDataParaArr = null;
            objHRPServer.CreateDatabaseParameter(4, out m_objDataParaArr);
            m_objDataParaArr[0].Value = p_strRemark_vchr;
            m_objDataParaArr[1].Value = p_strRecordid;
            m_objDataParaArr[2].Value = p_strControlid;
            m_objDataParaArr[3].Value = p_strImageid;
            try
            {
                lngResult = objHRPServer.lngExecuteParameterSQL(SQL, ref lngAffected, m_objDataParaArr);
                objHRPServer.Dispose();
            }
			catch(Exception objEx)
			{
				objHRPServer.Dispose();
				string strTemp = objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}	
			return lngResult;
		}
		#endregion				

		
        #region CB数据字典
        #region 删除
		/// <summary>
		/// 删除
		/// </summary>
		[AutoComplete]
		public long m_lngDeleteDictiionary(string p_strDictiionaryID)
		{
			long lngRet = 0;
			string SQL = @"delete from t_aid_cbdictionary where dictseqid_chr = ?";			
			clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strDictiionaryID.Trim().PadLeft(4, '0');
            try
            {
                long lngAfft = 0;
                lngRet = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAfft, objDPArr);                
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
			return lngRet;
		}
		#endregion 
        #region 新增
		/// <summary>
		/// 新增
		/// </summary>
		[AutoComplete]
		public long m_lngInsertDictiionary(string p_strsimplecode_chr,
            string p_strdictname_vchr,
            string p_strwbcode_chr,
            string p_strpycode_chr,
            string p_strdictkind_chr,
            out string p_strdictseqid_chr
            )
		{
			long lngRet = 0;
            p_strdictseqid_chr = "";
			string SQL = @"insert into t_aid_cbdictionary(dictseqid_chr,simplecode_chr,dictname_vchr,wbcode_chr,pycode_chr,dictkind_chr) values(?,?,?,?,?,?)";
			clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPSvc.CreateDatabaseParameter(6, out objDPArr);            
            try
            {
                objDPArr[0].Value = p_strdictseqid_chr;
                objDPArr[1].Value = p_strsimplecode_chr.Trim().PadLeft(4, '0');
                objDPArr[2].Value = p_strdictname_vchr;
                objDPArr[3].Value = p_strwbcode_chr.Trim();
                objDPArr[4].Value = p_strpycode_chr.Trim();
                objDPArr[5].Value = p_strdictkind_chr.Trim().PadLeft(4, '0');
                lngRet = objHRPSvc.lngGenerateID(10, "dictseqid_chr", "t_aid_cbdictionary", out p_strdictseqid_chr);
                if (lngRet > 0)
                {                    
                    long lngAfft = 0;
                    lngRet = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAfft, objDPArr);
                    objHRPSvc.Dispose();
                }
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
			return lngRet;
		}
		#endregion 

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        [AutoComplete]
        public long m_lngUpdateDictiionary(
            string strdictseqid_chr,
            string p_strsimplecode_chr,
            string p_strdictname_vchr,
            string p_strwbcode_chr,
            string p_strpycode_chr,
            string p_strdictkind_chr
            )
        {
            long lngRet = 0;
            string SQL = @"update t_aid_cbdictionary
                                set simplecode_chr = ?,
                                 dictname_vchr = ?,
                                 wbcode_chr = ?,
                                 pycode_chr = ?,
                                 dictkind_chr = ?
                                where dictseqid_chr=?";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objDPArr = null;            
            objDPArr[0].Value = p_strsimplecode_chr.Trim().PadLeft(4, '0');
            objDPArr[1].Value = p_strdictname_vchr;
            objDPArr[2].Value = p_strwbcode_chr.Trim();
            objDPArr[3].Value = p_strpycode_chr.Trim();
            objDPArr[4].Value = p_strdictkind_chr.Trim().PadLeft(4, '0');
            objDPArr[5].Value = strdictseqid_chr;
            objHRPSvc.CreateDatabaseParameter(6, out objDPArr);
            try
            {
                long lngAfft = 0;
                lngRet = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAfft, objDPArr);                
            }
            catch (Exception objEx)
            {
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngRet;
        }
        #endregion 

        #endregion
        #region 模版排序
        /// <summary>
        /// 模版排序
        /// </summary>
        /// <param name="m_strFormName"></param>
        /// <param name="m_objList"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthUpdateOrderNo(string m_strEmployeeID,string m_strFormName,List<string> m_objList)
        {
            long lngResult = -1;
            string SQL = @"update templateset a
            set a.order_no = ?
            where a.keyword = ? and a.form_id = ?";
            long lngAffected = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            System.Data.IDataParameter[] m_objDataParaArr = null;
            try
            {
                for (int i = 0; i < m_objList.Count; i++)
                {
                    m_objDataParaArr = null;
                    objHRPSvc.CreateDatabaseParameter(3, out m_objDataParaArr);
                    m_objDataParaArr[0].Value = i;
                    m_objDataParaArr[1].Value = m_objList[i];
                    m_objDataParaArr[2].Value = m_strFormName;
                    lngResult = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffected, m_objDataParaArr);
                }
            }

            catch (Exception objEx)
            {               
                string strTemp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
            }
            return lngResult;
        }
        #endregion				
        
        
	}
    /// <summary>
    /// 计算年龄
    /// </summary>
    public class clsConvertDateTime
    {
        #region 计算年龄
        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="dteBirth">出生日期</param>
        /// <returns></returns>
        public static string CalcAge(DateTime dteBirth)
        {
            int intAge = 0;
            string strAge = "";
            Age age = Age.Year;
            age = clsConvertDateTime.CalcAge(dteBirth, out intAge);
            switch (age)
            {
                case Age.Year:
                    strAge = intAge.ToString();
                    break;
                case Age.Month:
                    strAge = intAge.ToString() + "个月";
                    break;
                case Age.Day:
                    strAge = intAge.ToString() + "天";
                    break;
            }
            return strAge;
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public enum Age
        {
            /// <summary>
            /// 年
            /// </summary>
            Year,
            /// <summary>
            /// 月
            /// </summary>
            Month,
            /// <summary>
            /// 日
            /// </summary>
            Day
        }
        #region 计算年龄
        /// <summary>
        /// 计算年龄，根据返回的值得到是年，月或日
        /// </summary>
        /// <param name="dteBirth">出生日期</param>
        /// <param name="intAge">计算得到的年龄</param>
        /// <returns></returns>
        public static Age CalcAge(DateTime dteBirth, out int intAge)
        {
            Age age = Age.Year;
            intAge = 0;
            DateTime dteNow = DateTime.Now;
            int intYear = dteBirth.Year;
            int intMonth = dteBirth.Month;
            int intDay = dteBirth.Day;

            if ((dteNow.Year - intYear) > 0)
            {
                intAge = dteNow.Year - intYear;
                age = Age.Year;
            }
            else if ((dteNow.Month - intMonth) > 0)
            {
                intAge = dteNow.Month - intMonth;
                age = Age.Month;
            }
            else
            {
                intAge = dteNow.Day - intDay;
                age = Age.Day;
            }

            return age;

        }
        #endregion
        #endregion

    }
}
