using System;
using System.Collections;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using weCare.Core.Entity;

namespace com.digitalwave.GLS_WS.GlsMiddleTierBase
{
	/// <summary>
	/// clsGlspub 的摘要说明。
	/// </summary>	
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsGlspub : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsGlspub()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 返回当前最大RecordID(+1)
		/// <summary>
		/// 返回当前最大RecordID(+1)
		/// </summary>		
		/// <returns></returns>
		[AutoComplete]
		public int m_GetMaxRecordID()
		{
			int maxid = 0;
			long lngRes = 0;
			DataTable dtRecord = new DataTable();
			clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
			string SQL = "select nvl(max(RecordID),0) + 1 from ar_apply_report";
						
			try
			{
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
				lngRes = 0;
			}

			if(lngRes>0 && dtRecord.Rows.Count==1)
			{
				maxid = int.Parse(dtRecord.Rows[0][0].ToString());
			}		

			return maxid;
		}
		#endregion

		#region 根据编号(病理号、X线号等) 查找RecordID
		/// <summary>
		/// 根据编号(病理号、X线号等) 查找RecordID
		/// </summary>
		/// <param name="CtlID"></param>
		/// <param name="PNO"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_GetRecordID(string CtlID, string PNO)
		{
			long ReocrdID = 0;
			long lngRes = 0;
			DataTable dtRecord = new DataTable();
			clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
            string SQL = @"select b.recordid
  from ar_content a, ar_apply_report b
 where a.recordid = b.recordid
   and b.delstatus = 0
   and lower (a.controlid) = lower ('" + CtlID + @"')
   and trim (a.ctl_content) = trim ('" + PNO + "')";
			try
			{
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
				lngRes = 0;
			}

			if(lngRes>0 && dtRecord.Rows.Count==1)
			{
				ReocrdID = int.Parse(dtRecord.Rows[0][0].ToString());
			}
		
			return ReocrdID;
		}
		#endregion

		#region 查询满足编号(病理号、X线号等)的记录数
		/// <summary>
		/// 查询满足编号(病理号、X线号等)的记录数
		/// </summary>		
		/// <param name="CtlID"></param>
		/// <param name="PNO"></param>
		/// <returns></returns>
		[AutoComplete]
		public int m_GetRowsbyno(string CtlID, string PNO)
		{
			int rows = 0;
			long lngRes = 0;
			DataTable dtRecord = new DataTable();
			clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
            string SQL = @"select count (*) as nums
  from ar_content
 where lower (controlid) = '" + CtlID + @"'
   and trim (ctl_content) = '" + PNO + "'";
			try
			{
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRecord);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
				lngRes = 0;
			}

			if(lngRes>0 && dtRecord.Rows.Count>0)
			{
				rows = int.Parse(dtRecord.Rows[0]["nums"].ToString());
			}
		
			return rows;
		}
		#endregion	

		#region 保存全部数据
		/// <summary>
		/// 保存全部数据
		/// </summary>
		/// <param name="objMR"></param>
		/// <param name="objDRArr"></param>
		/// <param name="objPicArr"></param>
		/// <param name="DOFLAG"></param>
		/// <returns></returns>
		[AutoComplete]
		public bool m_blnSave(clsGLS_MainRecordInfo_VO objMR, clsGLS_DetailReocrdInfo_VO[] objDRArr,  clsPictureBoxValue[] objPicArr, int DOFLAG)
		{
			long ret = 0;
			bool b = true;
			try
			{				
				ret = m_lngSaveMainRecord(objMR, DOFLAG);	
				if(ret < 0) b = false;

				ret = m_lngSaveDetailRecord(objDRArr, DOFLAG);
				if(ret < 0) b = false;

				if(objPicArr!=null)
				{
					ret = m_lngSaveImageRecord(objPicArr);
					if(ret < 0) b = false;
				}

				if(objMR.ApplyID != 0)
				{
					ret = m_lngUpdateApplybillStatus(objMR.ApplyID.ToString());
					if(ret < 0) b = false;
				}
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
				b = false;
			}
		
			return b;
		}

		/// <summary>
		/// 保存主记录信息
		/// </summary>
		private long m_lngSaveMainRecord(clsGLS_MainRecordInfo_VO objMR, int DOFLAG)
        {
            long lngRes = 0;
            long lngAffected = 0;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
			string SQL = "";
			if(DOFLAG==0)
			{
                SQL = @"insert into ar_apply_report
            (recordid, formclsname, patientid, opendate, ceateddate,
             createduserid, delstatus, tempstatus, modifyuserid, sendstatus
            )
           values (?, ?, ?, ?, ?,
             ?, ?, ?, ?, ?
            )";

                try
                {   
                   
                    System.Data.IDataParameter[] m_objDataParmArr = null;
                    objHRPSvc.CreateDatabaseParameter(10, out m_objDataParmArr);
                    m_objDataParmArr[0].Value = objMR.RecordID;
                    m_objDataParmArr[1].Value=objMR.FormClsname;
                      m_objDataParmArr[2].Value=objMR.PatientID;
                      m_objDataParmArr[3].Value=Convert.ToDateTime(objMR.OpenDate);
                      m_objDataParmArr[4].Value=Convert.ToDateTime(objMR.CreatedDate);
                      m_objDataParmArr[5].Value=objMR.CreatedUserID;
                      m_objDataParmArr[6].Value=objMR.DelStatus;
                      m_objDataParmArr[7].Value=objMR.TempStatus;
                      m_objDataParmArr[8].Value=objMR.ModifyUserID;
                      m_objDataParmArr[9].Value = objMR.SendStatus;
                      lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffected, m_objDataParmArr);
                      objHRPSvc.Dispose();
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(ex);
                }
			}
			else if(DOFLAG==1)
			{
                SQL = @"update ar_apply_report
   set opendate = ?,
       ceateddate = ?,
       createduserid = ?,
       modifyuserid = ?
 where recordid = ?";

                try
                {

                    System.Data.IDataParameter[] m_objDataParmArr = null;
                    objHRPSvc.CreateDatabaseParameter(5, out m_objDataParmArr);
                    m_objDataParmArr[0].Value = Convert.ToDateTime(objMR.OpenDate);
                    m_objDataParmArr[1].Value = Convert.ToDateTime(objMR.CreatedDate);
                    m_objDataParmArr[2].Value = objMR.CreatedUserID;
                    m_objDataParmArr[3].Value = objMR.ModifyUserID;
                    m_objDataParmArr[4].Value = objMR.RecordID.Trim();
            
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffected, m_objDataParmArr);
                    objHRPSvc.Dispose();
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(ex);
                }
			}

			return lngRes;
		}

		/// <summary>
		/// 保存明细记录信息
		/// </summary>
		/// <param name="RecordID"></param>
		/// <param name="objDRArr"></param>
		/// <param name="DOFLAG"></param>
		private long m_lngSaveDetailRecord(clsGLS_DetailReocrdInfo_VO[] objDRArr, int DOFLAG)
		{
			long lngRes = 0;
			string SQL = "";
            IDataParameter[] ParamArr = null;
			clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
            long lngAffected=-1;
			if(DOFLAG==0)
			{
				try
				{
					for(int i=0; i<objDRArr.Length; i++)
					{
                        SQL = @"insert into ar_content
            (recordid, controlid, ctl_content, ctl_content_xml
            )
            values (?, ?, ?, ?
            )";
                        ParamArr = null;
                        objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                        ParamArr[0].Value = objDRArr[i].RecordID;
                        ParamArr[1].Value = objDRArr[i].ControlID;
                        ParamArr[2].Value = objDRArr[i].Ctl_Content;
                        ParamArr[3].Value = objDRArr[i].Ctl_Content_XML;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffected, ParamArr);
					}
				}
				catch(Exception ex)
				{
					com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
					bool blnRes = objLogger.LogError(ex);
				}			
			}
			else if(DOFLAG==1)
			{
				try
				{
					for(int i=0; i<objDRArr.Length; i++)
					{
						SQL = @"update ar_content 
									set ctl_content = ?,
										ctl_content_xml = ?
								where recordid = ? 
								  and controlid =?";

                        ParamArr = null;
                        objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                        ParamArr[0].Value = objDRArr[i].Ctl_Content;
                        ParamArr[1].Value = objDRArr[i].Ctl_Content_XML;
                        ParamArr[2].Value = objDRArr[i].RecordID;
                        ParamArr[3].Value = objDRArr[i].ControlID;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffected, ParamArr);
        
					}
				}
				catch(Exception ex)
				{
					com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
					bool blnRes = objLogger.LogError(ex);
				}
			}
            objHRPSvc.Dispose();
			return lngRes;
		}

		/// <summary>
		/// 保存图像信息
		/// </summary>
		/// <param name="RecordID"></param>
		/// <param name="objPicArr"></param>		
        [AutoComplete]
		private long m_lngSaveImageRecord( clsPictureBoxValue[] objPicArr)
		{
			long lngRes = 0;
			long lngEff = 0;
			string SQL = "";			

			clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			IDataParameter[] objDPArr = null;

			try
			{
				SQL = "delete from ar_image where recordid = ?";
                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = objPicArr[0].m_strRecordID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr);

				for(int i=0; i<objPicArr.Length; i++)
				{
					SQL = @"insert into ar_image (recordid, controlid, imageid, imagecontent, height, width) 
							values (?, '0', ?, ?, ?, ?)";

					objHRPSvc.CreateDatabaseParameter(5, out objDPArr);

					objDPArr[0].Value = objPicArr[i].m_strRecordID;
					objDPArr[1].Value = objPicArr[i].intImgID.ToString();						
					objDPArr[2].Value = objPicArr[i].m_bytImage;					
					objDPArr[3].Value = objPicArr[i].intHeight;
					objDPArr[4].Value = objPicArr[i].intWidth;

					lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr);
					objDPArr = null;
				}
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
			}			
			
			return lngRes;
		}

		/// <summary>
		/// 更新申请单状态为： 已完成
		/// </summary>
		/// <param name="ApplyID"></param>
		/// <returns></returns>
		private long m_lngUpdateApplybillStatus(string ApplyID)
		{
			long lngRes = 0;
        
			string SQL = "update ar_common_apply set status_int = 2 where applyid = ?";			

			try
			{
                long lngEff = -1;
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = ApplyID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
			}
	
			return lngRes;		
		}

		#endregion

		#region 伪删除记录数据
		/// <summary>
		/// 伪删除记录数据
		/// </summary>
		/// <param name="RecordID"></param>
		/// <param name="EmpID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDelete(string RecordID, string EmpID)
		{
			long lngRes = 0;
			string SQL = "update ar_apply_report set delstatus = 1, deluserid = ?, deldate = sysdate where recordid =?";

			try
			{
                long lngEff = -1;
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = EmpID;
                objDPArr[1].Value = RecordID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
			}
			
			return lngRes;
		}
		#endregion

		#region 删除图像
		/// <summary>
		/// 删除图像
		/// </summary>
		/// <param name="RecordID"></param>
		/// <param name="ImgID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDelImage(string RecordID, string ImgID)
		{
			long lngRes = 0;
			string SQL = "delete from ar_image where recordid = ? and imageid =?";			


			try
			{
                long lngEff = -1;
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = RecordID;
                objDPArr[1].Value = ImgID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngEff, objDPArr);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
			}
			
			return lngRes;
		}
		#endregion 

		#region 检索明细数据
		/// <summary>
		/// 检索明细数据
		/// </summary>
		/// <param name="RecordID"></param>
		/// <param name="dtRecord"></param>
		[AutoComplete]
		public long m_lngGetDetailData(string RecordID, out DataTable dtRecord)
		{		
			long lngRes = 0;
			dtRecord = null;
			


			string SQL = @"select a.controlid, a.ctl_content, a.ctl_content_xml
							 from ar_content a,
								  ar_apply_report b
							where a.recordid = b.recordid
							  and b.delstatus = 0
							  and b.recordid = ?";							 
			try
			{
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = RecordID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, objDPArr);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
				lngRes = 0;
			}
		
			return lngRes;
		}
		#endregion

		#region 检索图像数据
		/// <summary>
		/// 检索图像数据
		/// </summary>
		/// <param name="RecordID"></param>
		/// <param name="dtImgrecord"></param>
		[AutoComplete]
		public long m_lngGetImageDate(string RecordID, string Picscope, out DataTable dtImgrecord)
		{
			long lngRes = 0;
			string SQL = "";
			dtImgrecord = null;
			clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
			//Picscope: 图片打印范围， ""代表全部打印； 否则按选择范围打印
			if(Picscope.Trim()=="")
			{
				SQL = @"select controlid, imageid, imagecontent, height, width, remark_vchr
						  from ar_image
						 where recordid = ? order by imageid";
                try
                {
                    System.Data.IDataParameter[] m_objDataParaArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out m_objDataParaArr);
                    m_objDataParaArr[0].Value = RecordID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtImgrecord,m_objDataParaArr);
                    objHRPSvc.Dispose();
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(ex);
                    lngRes = 0;
                }
			}
			else
			{
				SQL = @"select controlid, imageid, imagecontent, height, width, remark_vchr
						  from ar_image
						 where recordid = ? and imageid in (" + Picscope + ") order by imageid";
                try
                {
                    System.Data.IDataParameter[] m_objDataParaArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out m_objDataParaArr);
                    m_objDataParaArr[0].Value = RecordID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtImgrecord, m_objDataParaArr);
                    objHRPSvc.Dispose();
                }
                catch (Exception ex)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(ex);
                    lngRes = 0;
                }		 
			}

			return lngRes;
		}
		#endregion

		#region 查找
		/// <summary>
		/// 查找
		/// </summary>
		/// <param name="SQL"></param>
		/// <param name="dtRecord"></param>
		[AutoComplete]
		public long m_lngFind(string sqlwhere, out DataTable dtRecordID, out DataTable dtRecord)
		{
			long lngRes = 0;
			dtRecordID = null;
			dtRecord = null;
			
			clsHRPTableService objHRPSvc = new clsHRPTableService();
            objHRPSvc.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			
			string SQL1 = @"select distinct recordid from ar_content where " + sqlwhere;

            string SQL2 = @"select a.recordid,a.controlid,a.ctl_content,a.ctl_content_xml
							from  ar_content a, 
								  ar_apply_report b
							where a.recordid = b.recordid 
							  and b.delstatus = 0 
							  and b.recordid in (
													select distinct recordid 
													from  ar_content 
													where " + sqlwhere + ")";

			try
			{
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL1, ref dtRecordID);
				lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL2, ref dtRecord);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
				lngRes = 0;
			}

			return lngRes;
		}
		#endregion

		#region 检索申请信息(根据申请类型ID)
		/// <summary>
		/// 检索申请信息(根据申请类型ID)
		/// </summary>
		/// <param name="TypeID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetApplybill(string TypeID, out DataTable dtRecord)
		{
			long lngRes = 0;
			dtRecord = null;
			
			clsHRPTableService objHRPSvc = new clsHRPTableService();

			string SQL = @"select applyid, name, sex, age, department, to_char(applydate,'yyyy/mm/dd hh24:mm:ss') as appdate, status_int
							 from ar_common_apply
						    where typeid =? 
							  and deleted = 0 
							  and submitted = 1 
							  and (status_int<>2 or (status_int=2 and to_char(applydate,'yyyy/mm/dd')=to_char(sysdate,'yyyy/mm/dd')))";										 
			try
			{
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out m_objDataParaArr);
                m_objDataParaArr[0].Value = TypeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, m_objDataParaArr);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
				lngRes = 0;
			}
		
			return lngRes;
		}
		#endregion

		#region 检索申请单记录信息(根据申请单号)
		/// <summary>
		/// 检索申请单记录信息(根据申请单号)
		/// </summary>
		/// <param name="TypeID"></param>
		/// <param name="dtRecord"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetApplybillrecord(string ApplyID, out DataTable dtRecord)
		{
			long lngRes = 0;
			dtRecord = null;
			
			clsHRPTableService objHRPSvc = new clsHRPTableService();

            string SQL = @"select applyid, reportid, deposit, balance, applydate, checkno, clinicno,
       bihno, name, sex, age, department, area, bedno, tel, address, summary,
       diagnose, diagnosepart, diagnoseaim, doctorname, doctorno, finishdate,
       chargedetail, extrano, cardno, deleted, whodeletes, deleteddate,
       applytitle, typeid, submitted, chargestatus_int, deptid_chr,
       areaid_chr, doctorid_chr, status_int
  from ar_common_apply
 where applyid = ?
";										 
			try
			{
                System.Data.IDataParameter[] m_objDataParaArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out m_objDataParaArr);
                m_objDataParaArr[0].Value = ApplyID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtRecord, m_objDataParaArr);
                objHRPSvc.Dispose();
			}
			catch(Exception ex)
			{
				com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
				bool blnRes = objLogger.LogError(ex);
				lngRes = 0;
			}
		
			return lngRes;
		}
		#endregion
	}
}
