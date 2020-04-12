using System;
using System.Collections;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;


namespace com.digitalwave.GLS_WS.ApplyReportServer
{
	/// <summary>
	/// 申请报告单中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsApplyReportServer : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		private const string m_strGetRecordSql = @"select f.*,ar.*,c.ctl_content as PatientName,d.ctl_content as checkpart,p1.ctl_content as ClinicDiagnose,
p2.ctl_content as Sex,p3.ctl_content as Age,p4.ctl_content as InPatientID,p5.ctl_content as Bed,p11.ctl_content as AREANAME,
p6.ctl_content as ApplyDate,p7.ctl_content as Doctor,p8.ctl_content as Dept,p9.ctl_content as CardID,p10.ctl_content as PathologyID,
p12.ctl_content as CheckID,p13.ctl_content as BUltrasonicID,p14.ctl_content as FilmID,p15.ctl_content as OperationID
from AR_FORM f inner join AR_APPLY_REPORT ar on f.formclsname = ar.FORMCLSNAME
left join (select * from ar_content where controlid = 'm_txtName') c on ar.recordid = c.recordid
left join (select * from ar_content where controlid = 'm_txtCheckPart') d on d.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtClinicDiagnose') p1 on p1.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_cboSex') p2 on p2.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtAge') p3 on p3.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtInPatientID') p4 on p4.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtBed') p5 on p5.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_dtpApplyDate') p6 on p6.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtApplyDoctor') p7 on p7.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtDept') p8 on p8.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtCardID') p9 on p9.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtPathologyID') p10 on p10.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtArea') p11 on p11.recordid = ar.recordid 
left join (select * from ar_content where controlid = 'm_txtCheckID') p12 on p12.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtBUltrasonicID') p13 on p13.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtFilmID') p14 on p14.recordid = ar.recordid
left join (select * from ar_content where controlid = 'm_txtOperationID') p15 on p15.recordid = ar.recordid";
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="p_arlSQL"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveData(ArrayList p_arlSQL)
		{
			long lngRes=-1;

			clsHRPTableService objHRPServer=new clsHRPTableService();

			for(int i=0;i<p_arlSQL.Count;i++)
			{
				if (p_arlSQL[i].ToString().Trim().Length>0)
				{
//					try
//					{
						lngRes=objHRPServer.DoExcute(p_arlSQL[i].ToString());
//					}
//					catch
//					{
//						com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
//						bool blnRes = objLogger.LogError(objEx);
//					
//					}
					if (lngRes<=0)
					{
						System.EnterpriseServices.ContextUtil.SetAbort();
						return lngRes;
					}
				}
			}
			return lngRes;
		}

		/// <summary>
		/// 保存图片
		/// </summary>
		/// <param name="p_strSQL"></param>
		/// <param name="p_objPictureBoxValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSaveDataToPic(string p_strSQL,clsPictureBoxValue[] p_objPictureBoxValue)
		{
			long lngRet=-1;
			long lngEff=0;

			if (p_strSQL.Trim().Length<=0)
				return lngRet;

			if (p_objPictureBoxValue==null)
				return lngRet;

			clsHRPTableService objHRPServer=new clsHRPTableService();
			IDataParameter[] objDPArr3 = null;

			


			for (int i=0;i<p_objPictureBoxValue.Length;i++)
			{
				//按顺序给IDataParameter赋值
//				for(int j=0;j<objDPArr3.Length;j++)
//					objDPArr3[j]=new OracleParameter();
				objHRPServer.CreateDatabaseParameter(4,out objDPArr3);
				objDPArr3[0].Value= i.ToString();
				
				if(p_objPictureBoxValue[i].m_bytImage!=null)
					objDPArr3[1].Value= p_objPictureBoxValue[i].m_bytImage;
				else
					objDPArr3[1].Value= DBNull.Value;
				objDPArr3[2].Value= p_objPictureBoxValue[i].intHeight;
				objDPArr3[3].Value= p_objPictureBoxValue[i].intWidth;

				lngEff=0;

				lngRet = objHRPServer.lngExecuteParameterSQL(p_strSQL,ref lngEff,objDPArr3);
				if(lngRet <= 0)	
				{
					return lngRet;
				}
			}
			return 1;
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="p_strSQL"></param>
		/// <param name="p_dtRecords"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetData(string p_strSQL,ref DataTable p_dtRecords)
		{
			p_dtRecords=null;
			long lngRes=-1;

			if (p_strSQL.Trim().Length<=0)
				return lngRes;

			clsHRPTableService objHRPServer=new clsHRPTableService();
            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
			lngRes=objHRPServer.lngGetDataTableWithoutParameters(p_strSQL,ref p_dtRecords);

			return lngRes;
		}

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="p_strSQL"></param>
        /// <param name="p_dtRecords"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDataBySQL(string p_strSQL, ref DataTable p_dtRecords)
        {
            p_dtRecords = null;
            long lngRes = -1;

            if (p_strSQL.Trim().Length <= 0)
                return lngRes;

            clsHRPTableService objHRPServer = new clsHRPTableService();
            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            lngRes = objHRPServer.lngGetDataTableWithoutParameters(p_strSQL, ref p_dtRecords);

            return lngRes;
        }
		#region 获取表单和数据
		/// <summary>
		/// 通过SQL获取记录
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordBySQL(string p_strSql,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			if(p_strSql == null || p_strSql == string.Empty)
				return -1;
			p_objRecordArr = m_objGetRecord(p_strSql);
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}
		/// <summary>
		/// 根据记录ID获取记录信息
		/// </summary>
		/// <param name="p_strRecordID"></param>
		/// <param name="p_objRecord"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordByRecID(string p_strRecordID,out clsApplyReportList_VO p_objRecord)
		{
			p_objRecord = null;
			if(p_strRecordID == null || p_strRecordID == string.Empty)
				return -1;
			string strSql = m_strGetRecordSql+@" where ar.RECORDID = '"+p_strRecordID.Trim()+"' and ar.READSTATUS  = '0' and ar.DELSTATUS = '0' order by ar.RECORDID";
			clsApplyReportList_VO[] objRecordArr = m_objGetRecord(strSql);
			if(objRecordArr != null)
			{
				p_objRecord = objRecordArr[0];
				return 1;
			}
			return 0;
		}
		/// <summary>
		/// 获取某张单所有记录
		/// </summary>
		/// <param name="p_strFormClsName"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordByForm(string p_strFormClsName,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			if(p_strFormClsName == null || p_strFormClsName == string.Empty)
				return -1;
			string strSql = m_strGetRecordSql+@" where ar.FORMCLSNAME = '"+p_strFormClsName+"' and ar.READSTATUS  = '0' and ar.DELSTATUS = '0'  order by ar.RECORDID";
			p_objRecordArr = m_objGetRecord(strSql);
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}
		/// <summary>
		///  根据住院号某张单最新记录
		/// </summary>
		/// <param name="p_strFormClsName"></param>
		/// <param name="p_strInpatientID"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordByFormAndInpID(string p_strFormClsName,string p_strInpatientID,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			if(p_strFormClsName == null || p_strFormClsName == string.Empty || p_strInpatientID == null)
				return -1;
			string strSql = m_strGetRecordSql+@" where ar.recordid = (select max(b.recordid) from ar_content a  inner join  ar_apply_report b on a.recordid = b.recordid
 where controlid = 'm_txtInPatientID'and b.READSTATUS  = '0' and b.DELSTATUS = '0'
  and b.formclsname = '"+p_strFormClsName+"' and a.ctl_content = '"+p_strInpatientID+"')";
			p_objRecordArr = m_objGetRecord(strSql);
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}

		/// <summary>
		///  获取最大病理号
		/// </summary>
		/// <param name="p_strFormClsName"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPathologyIDByForm(string p_strFormClsName,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			if(p_strFormClsName == null || p_strFormClsName == string.Empty)
				return -1;
			string strSql = @"select max(a.CTL_CONTENT) as PathologyID 
							from ar_content a inner join  ar_apply_report b on a.recordid = b.recordid
							where CONTROLID='m_txtPathologyID'
						    and b.formclsname = '"+p_strFormClsName+"'";
			
			DataTable dtResult = new DataTable();
			clsHRPTableService objHRPServer=new clsHRPTableService();
			long lngRes = objHRPServer.DoGetDataTable(strSql,ref dtResult);
            //objHRPServer.Dispose();
			if(lngRes <= 0 || dtResult.Rows.Count <=0)
				return 0;
			p_objRecordArr = new clsApplyReportList_VO[dtResult.Rows.Count];
			for(int i=0;i<dtResult.Rows.Count;i++)
			{
				p_objRecordArr[i] = new clsApplyReportList_VO();
				p_objRecordArr[i].m_StrPathologyID = dtResult.Rows[i]["PathologyID"].ToString().Trim();
			}
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}

		/// <summary>
		/// 根据日期段获取某张单所有记录
		/// </summary>
		/// <param name="p_strFormClsName"></param>
		/// <param name="p_strFirstTime"></param>
		/// <param name="p_strLastTime"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordByFormTimeslice(string p_strFormClsName,string p_strFirstTime,string p_strLastTime,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			if(p_strFormClsName == null || p_strFormClsName == string.Empty || p_strFirstTime == null || p_strLastTime == null)
				return -1;
			string strSql = m_strGetRecordSql+@" where ar.FORMCLSNAME = '"+p_strFormClsName+"' and ar.READSTATUS  = '0' and ar.DELSTATUS = '0' and ar.ceateddate between timestamp'"+p_strFirstTime+"' and timestamp'"
				+p_strLastTime+"'  order by ar.ceateddate";
			p_objRecordArr = m_objGetRecord(strSql);
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}
		/// <summary>
		/// 获取某病人某张单的记录
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strFormClsName"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordByPatientForm(string p_strPatientID,string p_strFormClsName,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			if(p_strPatientID == null || p_strPatientID == string.Empty || p_strFormClsName == null || p_strFormClsName == string.Empty)
				return -1;
			string strSql = m_strGetRecordSql+@" where ar.PATIENTID = '"+p_strPatientID+"' and f.FORMCLSNAME = '"+p_strFormClsName+"' and ar.READSTATUS  = '0' and ar.DELSTATUS = '0'  order by ar.ceateddate";
			p_objRecordArr = m_objGetRecord(strSql);
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}
		/// <summary>
		/// 获取某病人所有的记录
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordByPatient(string p_strPatientID,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			if(p_strPatientID == null || p_strPatientID == string.Empty)
				return -1;
			string strSql = m_strGetRecordSql+@" where ar.PATIENTID = '"+p_strPatientID+"' and ar.READSTATUS  = '0' and ar.DELSTATUS = '0'   order by ar.ceateddate";
			p_objRecordArr = m_objGetRecord(strSql);
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}
		/// <summary>
		/// 获取申请单或报告单的所有记录
		/// </summary>
		/// <param name="p_intTypeStatus"></param>
		/// <param name="p_objRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordByType(int p_intTypeStatus,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			string strSql =m_strGetRecordSql+ @" where ar.READSTATUS  = '0' and ar.DELSTATUS = '0'";
			if(p_intTypeStatus>=0 && p_intTypeStatus<=2)
				strSql += " and f.TYPESTATUS = '"+p_intTypeStatus.ToString()+"'";
			p_objRecordArr = m_objGetRecord(strSql);
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}
		/// <summary>
		/// 根据日期段获取申请单或报告单的所有记录
		/// </summary>
		/// <param name="p_intTypeStatus">0－申请单；1－报告单</param>
		/// <param name="p_strFirstTime">开始时间</param>
		/// <param name="p_strLastTime">结束时间</param>
		/// <param name="p_objRecordArr">记录列表</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARRecordByTimeslice(int p_intTypeStatus,string p_strFirstTime,string p_strLastTime,out clsApplyReportList_VO[] p_objRecordArr)
		{
			p_objRecordArr = null;
			string strSql = m_strGetRecordSql+@" where ar.READSTATUS  = '0' and ar.DELSTATUS = '0' and ar.ceateddate between timestamp'"+p_strFirstTime+"' and timestamp'"+p_strLastTime+"'";
			if(p_intTypeStatus>=0 && p_intTypeStatus<=2)
				strSql += " and f.TYPESTATUS = '"+p_intTypeStatus.ToString()+"'";
			p_objRecordArr = m_objGetRecord(strSql);
			if(p_objRecordArr != null)
				return 1;
			return 0;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strSql"></param>
		/// <returns></returns>
		[AutoComplete]
		private clsApplyReportList_VO[] m_objGetRecord(string p_strSql)
		{
			if(p_strSql == string.Empty || p_strSql == null)
				return null;
			DataTable dtResult = new DataTable();
			clsHRPTableService objHRPServer=new clsHRPTableService();
			long lngRes = objHRPServer.DoGetDataTable(p_strSql,ref dtResult);
            //objHRPServer.Dispose();
			if(lngRes <= 0 || dtResult.Rows.Count <=0)
				return null;
			clsApplyReportList_VO[] objContentArr = new clsApplyReportList_VO[dtResult.Rows.Count];
			for(int i=0;i<dtResult.Rows.Count;i++)
			{
				objContentArr[i] = new clsApplyReportList_VO();
				objContentArr[i].m_strRecordID = dtResult.Rows[i]["RECORDID"].ToString().Trim();
				objContentArr[i].m_objAR_Form_VO = new clsAR_Form_VO();
				objContentArr[i].m_objAR_Form_VO.m_strAR_FormClsName = dtResult.Rows[i]["FORMCLSNAME"].ToString().Trim();
				objContentArr[i].m_objAR_Form_VO.m_strAR_FormDesc = dtResult.Rows[i]["FORMDESC"].ToString().Trim();
				objContentArr[i].m_objAR_Form_VO.m_strAR_FormType = dtResult.Rows[i]["TYPESTATUS"].ToString().Trim();
				objContentArr[i].m_strCreateDate = dtResult.Rows[i]["CEATEDDATE"].ToString().Trim();
				objContentArr[i].m_StrPatientID = dtResult.Rows[i]["PATIENTID"].ToString().Trim();
				objContentArr[i].m_StrPatientName = dtResult.Rows[i]["PatientName"].ToString().Trim();//用保存的控件值，没有为空
				objContentArr[i].m_strSendStatus = dtResult.Rows[i]["SENDSTATUS"].ToString().Trim();
				objContentArr[i].m_strAR_FormType = dtResult.Rows[i]["TYPESTATUS"].ToString().Trim();
				objContentArr[i].m_strCheckPart = dtResult.Rows[i]["CHECKPART"].ToString().Trim();

				if(dtResult.Rows[i]["AREANAME"] == DBNull.Value)
					objContentArr[i].m_StrAreaName = "";//dtResult.Rows[i]["TYPESTATUS"].ToString().Trim();
				else
					objContentArr[i].m_StrAreaName = dtResult.Rows[i]["AREANAME"].ToString().Trim();
				objContentArr[i].m_StrBedName = dtResult.Rows[i]["BED"].ToString().Trim();
				objContentArr[i].m_StrClinicDiagnose = dtResult.Rows[i]["CLINICDIAGNOSE"].ToString().Trim();
				objContentArr[i].m_StrDeptName = dtResult.Rows[i]["DEPT"].ToString().Trim();
				try
				{
					objContentArr[i].m_DtmAppryDate = DateTime.Parse(dtResult.Rows[i]["APPLYDATE"].ToString());
				}
				catch{objContentArr[i].m_DtmAppryDate = DateTime.Now;}
				objContentArr[i].m_StrDoctor = dtResult.Rows[i]["DOCTOR"].ToString().Trim();
				objContentArr[i].m_StrInPatientID = dtResult.Rows[i]["INPATIENTID"].ToString().Trim();
				objContentArr[i].m_StrPatientAge = dtResult.Rows[i]["AGE"].ToString().Trim();
				objContentArr[i].m_StrPatientCardID = dtResult.Rows[i]["CARDID"].ToString().Trim();
				objContentArr[i].m_StrPatientSex = dtResult.Rows[i]["SEX"].ToString().Trim();
				objContentArr[i].m_StrPathologyID = dtResult.Rows[i]["PATHOLOGYID"].ToString().Trim();
				objContentArr[i].m_strCheckID = dtResult.Rows[i]["CheckID"].ToString().Trim();
				objContentArr[i].m_strBUltrasonicID = dtResult.Rows[i]["BUltrasonicID"].ToString().Trim();
				objContentArr[i].m_strFilmID = dtResult.Rows[i]["FilmID"].ToString().Trim();
				objContentArr[i].m_strOperationID = dtResult.Rows[i]["OperationID"].ToString().Trim();

				objContentArr[i].m_objRelaFormArr = m_objGetRelaForm(objContentArr[i].m_strRecordID);
			}
			return objContentArr;
		}
		/// <summary>
		/// 获取关联的报告单
		/// </summary>
		/// <param name="p_strRecordID"></param>
		/// <returns></returns>
		[AutoComplete]
		private clsAR_RelaForm_VO[] m_objGetRelaForm(string p_strRecordID)
		{
			if(p_strRecordID == null || p_strRecordID == string.Empty)
				return null;
            string strSql = @"select f.formclsname, f.formdesc, f.typestatus, f.formstatus, f.levelindex,
       ar.recordid, c.ctl_content as patientname
  from ar_form f inner join ar_apply_report ar on f.formclsname =
                                                                ar.formclsname
       left join (select h.recordid, h.controlid, h.ctl_content,
                         h.ctl_content_xml
                    from ar_content h
                   where controlid = 'm_txtName') c on ar.recordid =
                                                                    c.recordid
 where ar.rela_recordid = '1'
   and f.typestatus = '1'
   and ar.readstatus = '0'
   and ar.delstatus = '0'";
			DataTable dtResult = new DataTable();
			clsHRPTableService objHRPServer=new clsHRPTableService();
			long lngRes = objHRPServer.DoGetDataTable(strSql,ref dtResult);
            //objHRPServer.Dispose();
			if(lngRes <= 0 || dtResult.Rows.Count <=0)
				return null;
			clsAR_RelaForm_VO[] objContentArr = new clsAR_RelaForm_VO[dtResult.Rows.Count];
			for(int i=0;i<dtResult.Rows.Count;i++)
			{
				objContentArr[i] = new clsAR_RelaForm_VO();
				objContentArr[i].m_strRela_RecordID = dtResult.Rows[i]["RECORDID"].ToString().Trim();
				objContentArr[i].m_objAR_Form_VO = new clsAR_Form_VO();
				objContentArr[i].m_objAR_Form_VO.m_strAR_FormClsName = dtResult.Rows[i]["FORMCLSNAME"].ToString().Trim();
				objContentArr[i].m_objAR_Form_VO.m_strAR_FormDesc = dtResult.Rows[i]["FORMDESC"].ToString().Trim();
				objContentArr[i].m_objAR_Form_VO.m_strAR_FormType = "1";
			}
			return objContentArr;
		}
		/// <summary>
		/// 获取表单
		/// </summary>
		/// <param name="p_intTypeStatus">0－申请单；1－报告单；2－其它</param>
		/// <param name="p_objApplyFormArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetARForm(int p_intTypeStatus,out clsAR_Form_VO[] p_objApplyFormArr)
		{
			p_objApplyFormArr = null;
			string strSql = @"select * from AR_FORM  where formstatus = '0'";
			if(p_intTypeStatus>=0 && p_intTypeStatus<=2)
				strSql += " and TYPESTATUS = '"+p_intTypeStatus.ToString()+"'";
			strSql += "order by LEVELINDEX";
			DataTable dtResult = new DataTable();
			clsHRPTableService objHRPServer=new clsHRPTableService();
			long lngRes = objHRPServer.DoGetDataTable(strSql,ref dtResult);
            //objHRPServer.Dispose();
			if(lngRes <= 0 || dtResult.Rows.Count <=0)
				return 0;
			p_objApplyFormArr = new clsAR_Form_VO[dtResult.Rows.Count];
			for(int i=0;i<dtResult.Rows.Count;i++)
			{
				p_objApplyFormArr[i] = new clsAR_Form_VO();
				p_objApplyFormArr[i].m_strAR_FormClsName = dtResult.Rows[i]["FORMCLSNAME"].ToString().Trim();
				p_objApplyFormArr[i].m_strAR_FormDesc = dtResult.Rows[i]["FORMDESC"].ToString().Trim();
				p_objApplyFormArr[i].m_strAR_FormType = dtResult.Rows[i]["TYPESTATUS"].ToString().Trim();
				p_objApplyFormArr[i].m_strAR_FormIndex = dtResult.Rows[i]["LEVELINDEX"].ToString().Trim();
			}
			return lngRes;
		}
		/// <summary>
		/// 获取某记录的图片
		/// </summary>
		/// <param name="p_strRecordID"></param>
		/// <param name="p_objPicArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAR_PicValue(string p_strRecordID,out clsPictureBoxValue[] p_objPicArr)
		{
			p_objPicArr = null;
			if(p_strRecordID == null || p_strRecordID == string.Empty)
				return -1;
			clsHRPTableService objHRPServer=new clsHRPTableService();
			string SQL="select RECORDID,CONTROLID,IMAGEID,IMAGECONTENT,HEIGHT,WIDTH from AR_IMAGE where RECORDID=" + p_strRecordID + "";
			DataTable dtRecords=null;
			long lngRes = objHRPServer.DoGetDataTable(SQL,ref dtRecords);
            //objHRPServer.Dispose();
			if(lngRes <= 0 || dtRecords == null || dtRecords.Rows.Count == 0)
				return 0;
			p_objPicArr  = new clsPictureBoxValue[dtRecords.Rows.Count];
			for (int i=0;i<dtRecords.Rows.Count;i++)
			{
				p_objPicArr[i] = new clsPictureBoxValue();
				try
				{
					System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])(dtRecords.Rows[i]["IMAGECONTENT"]));
					p_objPicArr[i].m_imgBack = new System.Drawing.Bitmap(objStream);
                    p_objPicArr[i].m_bytImage = (byte[])dtRecords.Rows[i]["IMAGECONTENT"];
				}
				catch{p_objPicArr[i].m_imgBack = new System.Drawing.Bitmap(32,32);}
				try
				{
					p_objPicArr[i].intWidth = Convert.ToInt32(dtRecords.Rows[i]["WIDTH"]) ;}
				catch{p_objPicArr[i].intWidth = 100;}
				try
				{
					p_objPicArr[i].intHeight = Convert.ToInt32(dtRecords.Rows[i]["HEIGHT"]) ;}
				catch{p_objPicArr[i].intHeight = 100;}
				p_objPicArr[i].intImgID = Convert.ToInt32(dtRecords.Rows[i]["IMAGEID"]);
			}
			return lngRes;
		}
        public long m_lngGetARImages(string p_strRecordID, out clsPictureBoxValue[] p_objPicArr)
        {
            p_objPicArr = null;
            if (p_strRecordID == null || p_strRecordID == string.Empty)
                return -1;
            clsHRPTableService objHRPServer = new clsHRPTableService();
            objHRPServer.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
            string SQL = "select RECORDID,CONTROLID,IMAGEID,IMAGECONTENT,HEIGHT,WIDTH from AR_IMAGE where RECORDID=" + p_strRecordID + "";
            DataTable dtRecords = null;
            long lngRes = objHRPServer.DoGetDataTable(SQL, ref dtRecords);
            //objHRPServer.Dispose();
            if (lngRes <= 0 || dtRecords == null || dtRecords.Rows.Count == 0)
                return 0;
            p_objPicArr = new clsPictureBoxValue[dtRecords.Rows.Count];
            for (int i = 0; i < dtRecords.Rows.Count; i++)
            {
                p_objPicArr[i] = new clsPictureBoxValue();
                try
                {
                    System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])(dtRecords.Rows[i]["IMAGECONTENT"]));
                    p_objPicArr[i].m_imgBack = new System.Drawing.Bitmap(objStream);
                    p_objPicArr[i].m_bytImage = (byte[])dtRecords.Rows[i]["IMAGECONTENT"];
                }
                catch { p_objPicArr[i].m_imgBack = new System.Drawing.Bitmap(32, 32); }
                try
                {
                    p_objPicArr[i].intWidth = Convert.ToInt32(dtRecords.Rows[i]["WIDTH"]);
                }
                catch { p_objPicArr[i].intWidth = 100; }
                try
                {
                    p_objPicArr[i].intHeight = Convert.ToInt32(dtRecords.Rows[i]["HEIGHT"]);
                }
                catch { p_objPicArr[i].intHeight = 100; }
                p_objPicArr[i].intImgID = Convert.ToInt32(dtRecords.Rows[i]["IMAGEID"]);
            }
            return lngRes;
        }
		#endregion 获取表单和数据

		/// <summary>
		/// 删除指定图片
		/// </summary>
		/// <param name="p_strRecordID"></param>
		/// <param name="p_strImgID"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDelAR_Pic(string p_strRecordID,int p_strImgID)
		{
			if(p_strRecordID == null || p_strRecordID == string.Empty)
				return -1;
			clsHRPTableService objHRPServer=new clsHRPTableService();
			string SQL="DELETE AR_IMAGE where RECORDID=" + p_strRecordID + " AND IMAGEID="+ p_strImgID +"";
			long lngRes = objHRPServer.DoExcute(SQL);
            //objHRPServer.Dispose();
			
			return lngRes;
		}


		/// <summary>
		/// 提交表单
		/// </summary>
		/// <param name="p_strRecordID">记录ID</param>
		[AutoComplete]
		public long m_lngSetReferStatus(string p_strRecordID)
		{
			if(p_strRecordID == null || p_strRecordID == string.Empty)
				return -1;
			string strSql = @"update AR_APPLY_REPORT set SENDSTATUS = '1' where RECORDID = '"+p_strRecordID.Trim()+"'";
			clsHRPTableService objHRPServer=new clsHRPTableService();
			long lngRes =  objHRPServer.DoExcute(strSql);
			return lngRes;
		}
		/// <summary>
		/// 设置表单状态为已读
		/// </summary>
		/// <param name="p_strRecordID">记录ID</param>
		[AutoComplete]
		public long m_lngSetReadStatus(string p_strRecordID)
		{
			if(p_strRecordID == null || p_strRecordID == string.Empty)
				return -1;
			string strSql = @"update AR_APPLY_REPORT set READSTATUS = '1' where RECORDID = '"+p_strRecordID.Trim()+"'";
			clsHRPTableService objHRPServer=new clsHRPTableService();
			long lngRes =  objHRPServer.DoExcute(strSql);
			return lngRes;
		}
		/// <summary>
		/// 判断表单是否已经初始化过
		/// </summary>
		/// <param name="p_strFormClsName"></param>
		/// <returns></returns>
		[AutoComplete]
		public bool m_blnIsInitForm(string p_strFormClsName)
		{
			if(p_strFormClsName == null || p_strFormClsName == string.Empty)
				return false;
			string strSql =@"select count(*) as Count from ar_control_desc t where t.formclsname = '"+p_strFormClsName.Trim()+"'";
			clsHRPTableService objHRPServer=new clsHRPTableService();
			DataTable dtRecords=null;
			long lngRes = objHRPServer.DoGetDataTable(strSql,ref dtRecords);
			if(lngRes > 0 && dtRecords != null)
			{
				if(dtRecords.Rows[0]["COUNT"].ToString().Trim() != "0")
					return true;
			}
			return false;
		}


		/// <summary>
		/// 根据病人id获取指定图文工作站报告书的记录时间列表
		/// </summary>
		/// <param name="p_strPatientID">病人ID</param>
		/// <param name="strQueryDate">指定字段时间列表</param>
		/// <param name="strFormName">指定表单名称</param>
		/// <param name="strCreateDateArr">返回时间列表</param>
		/// <param name="strRecordIDArr">返回报告单ID</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetCreateDateByPatientID(string p_strPatientID,string strQueryDate,string strFormName,out string[] strCreateDateArr,out string[] strRecordIDArr)
		{
			long lngRes=0;
			strCreateDateArr=null;
			strRecordIDArr=null;
			string strSQl=string.Empty;
			strSQl="select Recordid, "+ strQueryDate +" from ar_apply_report where patientid='"+ p_strPatientID+"' and formclsname='"+strFormName.Trim() +"' and delstatus<>'1' order by ceateddate desc";
			try
			{
				DataTable dtResult = new DataTable();
                clsHRPTableService objHRPService = new clsHRPTableService();
                if (strFormName.Trim() == "frmBultrasoundWorkStation")
                    objHRPService.m_bytSetOtherDSN = (byte)com.digitalwave.iCare.middletier.HRPService.clsHRPTableService.enumDatabase.bytUltraSound;
				lngRes= objHRPService.DoGetDataTable(strSQl,ref dtResult);

				if(lngRes > 0 && dtResult.Rows.Count > 0)
				{
					strCreateDateArr=new string[dtResult.Rows.Count];
					strRecordIDArr=new string[dtResult.Rows.Count];
					for (int i=0;i<dtResult.Rows.Count;i++)
					{
						strCreateDateArr[i]=dtResult.Rows[i][strQueryDate].ToString().Trim();	
						strRecordIDArr[i]=dtResult.Rows[i]["RecordID"].ToString().Trim();
					}
				}
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;


		}
		/// <summary>
		/// 根据病人id获取指定图文工作站申请单的记录时间列表
		/// </summary>
		/// <param name="p_strPatientID">病人ID</param>
		/// <param name="strQueryDate">指定字段时间列表</param>
		/// <param name="strFormName">指定类型表单</param>
		/// <param name="strCreateDateArr">返回时间列表</param>
		/// <param name="strRecordIDArr">返回申请单ID</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetApplyDateByPatientID(string p_strPatientID,string strQueryDate,string strTypeID,out string[] strApplyDateArr,out string[] strApplyIDArr)
		{
			long lngRes=0;
			strApplyDateArr=null;
			strApplyIDArr=null;
			string strSQl=string.Empty;
			strSQl="select applyID, "+ strQueryDate +" from AR_COMMON_APPLY where cardNo='"+ p_strPatientID+"' and typeID='"+strTypeID.Trim() +"' and deleted<>'1' order by applydate desc";
			try
			{
				DataTable dtResult = new DataTable();
				lngRes= new clsHRPTableService().DoGetDataTable(strSQl,ref dtResult);

				if(lngRes > 0 && dtResult.Rows.Count > 0)
				{
					strApplyDateArr=new string[dtResult.Rows.Count];
					strApplyIDArr=new string[dtResult.Rows.Count];
					for (int i=0;i<dtResult.Rows.Count;i++)
					{
						strApplyDateArr[i]=dtResult.Rows[i][strQueryDate].ToString().Trim();	
						strApplyIDArr[i]=dtResult.Rows[i]["ApplyID"].ToString().Trim();
					}
				}
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;


        }

        #region 根据处方ID获取指定图文工作站申请单的记录时间列表(查询检查申请单)
        /// <summary>
        /// 根据处方ID查询检查申请单
        /// baojian.mo add in 2007.12.07
        /// </summary>
        /// <param name="p_strRecipeID"></param>
        /// <param name="p_strTypeID"></param>
        /// <param name="strApplyDateArr"></param>
        /// <param name="strApplyIDArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetApplyDateByRecipeID(string p_strRecipeID, string p_strTypeID, out string[] strApplyDateArr, out string[] strApplyIDArr)
        {
            long lngRes = 0;
            strApplyDateArr = null;
            strApplyIDArr = null;
            DataTable dtResult=new DataTable();
            string strSQL = "";
            try
            {
                strSQL = @"select a.applyid, a.applydate
                             from ar_common_apply a, t_opr_outpatient_orderdic b
                            where a.applyid = b.attachid_vchr
                              and a.typeid = ?
                              and a.deleted <> '1'
                              and b.tableindex_int = 4
                              and b.outpatrecipeid_chr = ?";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objParamArr);
                objParamArr[0].Value = p_strTypeID;
                objParamArr[1].Value = p_strRecipeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objParamArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dtResult.Rows.Count > 0)
                {
                    strApplyDateArr = new string[dtResult.Rows.Count];
                    strApplyIDArr = new string[dtResult.Rows.Count];
                    for (int i1 = 0; i1 < dtResult.Rows.Count; i1++)
                    {
                        strApplyIDArr[i1] = dtResult.Rows[i1]["applyid"].ToString();
                        strApplyDateArr[i1] = dtResult.Rows[i1]["applydate"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(strTmp);
            }
            return lngRes;
        }
        #endregion

        /// <summary>
		/// 将指定申请单状态设置为已完成
		/// </summary>
		/// <param name="p_strApplyID"></param>
		/// <param name="p_dtmFinishDate"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngSetApplyComplete(string p_strApplyID, out DateTime p_dtmFinishDate)
		{
			p_dtmFinishDate = DateTime.MinValue;
			if(p_strApplyID == null || p_strApplyID == string.Empty)
				return -1;
			long lngRes=0;
			
			string strSQl = string.Empty;

			strSQl = "update AR_COMMON_APPLY set STATUS_INT=2,FINISHDATE=? where APPLYID = ?";
			try
			{
				clsHRPTableService objServ = new clsHRPTableService();
				p_dtmFinishDate = DateTime.Now;
				IDataParameter[] objDPArr = null; 
				objServ.CreateDatabaseParameter(2,out objDPArr);
				objDPArr[0].Value = Convert.ToDateTime(p_dtmFinishDate.ToString("yyyy-MM-dd HH:mm:ss"));
				objDPArr[1].Value = p_strApplyID.Trim();

				long lngEff = 0;
				lngRes= objServ.lngExecuteParameterSQL(strSQl, ref lngEff, objDPArr);
				objServ = null;
				
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			return lngRes;
		}

        /// <summary>
        /// 插入新的申请单
        /// </summary>
        /// <param name="objArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddCommonApply(clsApplyRecord[] objArr)
        {
            long lngRes = 0;
            string strSQL = @"insert into ar_common_apply
                                            (chargestatus_int, deposit, balance, checkno, clinicno, bihno,
                                             name, sex, age, area, bedno, tel, address, summary, diagnose,
                                             doctorname, doctorno, extrano, cardno, department, chargedetail,
                                             finishdate, applydate, deleted, applytitle, diagnoseaim,
                                             diagnosepart, typeid, deptid_chr, areaid_chr, doctorid_chr,
                                             submitted, applyid, birthday_vchr)
                                     values (?, ?, ?, ?, ?, ?,
                                             ?, ?, ?, ?, ?, ?, ?, ?, ?,
                                             ?, ?, ?, ?, ?, ?,
                                             ?, ?, ?, ?, ?,
                                             ?, ?, ?, ?, ?,
                                             ?, ?, ?) ";
            try
            { 
                DbType[] objTypes = new DbType[]{DbType.Int16,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,
                                               DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,
                                               DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,DbType.String,
                                               DbType.DateTime,DbType.DateTime,DbType.Int16,DbType.String,DbType.String,
                                               DbType.String,DbType.Int16,DbType.String,DbType.String,DbType.String,
                                               DbType.Int16,DbType.String,DbType.String};

                object[][] objValues = new object[34][];
                for (int i = 0; i < objValues.Length; i++)
                {
                    objValues[i] = new object[objArr.Length];
                }
                for (int i = 0; i < objArr.Length; i++)
                {
                    int n = 0;

                    objValues[n++][i]=objArr[i].m_intChargeStatus;
                    objValues[n++][i]=objArr[i].m_strDeposit;
                    objValues[n++][i]=objArr[i].m_strBalance;
                    objValues[n++][i]=objArr[i].m_strCheckNO;
                    objValues[n++][i]=objArr[i].m_strClinicNO;
                    objValues[n++][i]=objArr[i].m_strBIHNO;
                    objValues[n++][i]=objArr[i].m_strName;
                    objValues[n++][i]=objArr[i].m_strSex;
                    objValues[n++][i]=objArr[i].m_strAge;
                    objValues[n++][i]=objArr[i].m_strArea;
                    objValues[n++][i]=objArr[i].m_strBedNO;
                    objValues[n++][i]=objArr[i].m_strTel;
                    objValues[n++][i]=objArr[i].m_strAddress;
                    objValues[n++][i]=objArr[i].m_strSummary;
                    objValues[n++][i]=objArr[i].m_strDiagnose;
                    objValues[n++][i]=objArr[i].m_strDoctorName;
                    objValues[n++][i]=objArr[i].m_strDoctorNO;
                    objValues[n++][i]=objArr[i].m_strExtraNO;
                    objValues[n++][i]=objArr[i].m_strCardNO;
                    objValues[n++][i]=objArr[i].m_strDepartment;
                    objValues[n++][i]=objArr[i].m_strChargeDetail;
                    objValues[n++][i]=objArr[i].m_datFinishDate;
                    objValues[n++][i]=objArr[i].m_datApplyDate;
                    objValues[n++][i]=objArr[i].m_intDeleted;
                    objValues[n++][i]=objArr[i].m_strApplyTitle;
                    objValues[n++][i]=objArr[i].m_strDiagnoseAim;
                    objValues[n++][i]=objArr[i].m_strDiagnosePart;
                    objValues[n++][i] = objArr[i].m_strTypeID;
                    objValues[n++][i] = objArr[i].m_strDeptID ;
                    objValues[n++][i] = objArr[i].m_strAreaID;
                    objValues[n++][i] = objArr[i].m_strDoctorID;
                    objValues[n++][i] = objArr[i].m_intSubmitted;
                    objValues[n++][i] = objArr[i].m_strApplyID;
                    objValues[n++][i] = objArr[i].BirthDay;
                }

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, objTypes);

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="strSqlType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetCommon_Apply(DataTable m_dtData, string strSqlType)
        {
            long lngEff = 0;
            long lngRes = 0;
            string strSQLSet = "";
            clsHRPTableService m_objHRPService = new clsHRPTableService();
            switch (strSqlType)
            {
                case "1":
                    strSQLSet = @"insert into ar_common_apply
                                            (chargestatus_int, deposit, balance, checkno, clinicno, bihno,
                                             name, sex, age, area, bedno, tel, address, summary, diagnose,
                                             doctorname, doctorno, extrano, cardno, department, chargedetail,
                                             finishdate, applydate, deleted, applytitle, diagnoseaim,
                                             diagnosepart, typeid, deptid_chr, areaid_chr, doctorid_chr,
                                             submitted, applyid)
                                     values (?, ?, ?, ?, ?, ?,
                                             ?, ?, ?, ?, ?, ?, ?, ?, ?,
                                             ?, ?, ?, ?, ?, ?,
                                             ?, ?, ?, ?, ?,
                                             ?, ?, ?, ?, ?,
                                             ?, ?) ";
                    break;
                case "2":
                    strSQLSet = @"update ar_common_apply
                                       set chargestatus_int = ?,
                                           deposit = ?,
                                           balance = ?,
                                           checkno = ?,
                                           clinicno = ?,
                                           bihno = ?,
                                           name = ?,
                                           sex = ?,
                                           age = ?,
                                           area = ?,
                                           bedno = ?,
                                           tel = ?,
                                           address = ?,
                                           summary = ?,
                                           diagnose = ?,
                                           doctorname = ?,
                                           doctorno = ?,
                                           extrano = ?,
                                           cardno = ?,
                                           department = ?,
                                           chargedetail = ?,
                                           finishdate = ?,
                                           applydate = ?,
                                           deleted = ?,
                                           applytitle = ?,
                                           diagnoseaim = ?,
                                           diagnosepart = ?,
                                           typeid = ?,
                                           deptid_chr = ?,
                                           areaid_chr = ?,
                                           doctorid_chr = ?,
                                           submitted = ?
                                     where applyid = ?";
                    break;
            }
            IDataParameter[] objDPArr = null;
            if (m_dtData.Rows.Count <=0)
            {
                return lngEff;
            }
            DataRow dr = m_dtData.Rows[0];
            m_objHRPService.CreateDatabaseParameter(33, out objDPArr);
            objDPArr[0].Value = dr["CHARGESTATUS_INT"].ToString();
            objDPArr[1].Value = dr["DEPOSIT"].ToString();
            objDPArr[2].Value = dr["BALANCE"].ToString();
            objDPArr[3].Value = dr["CHECKNO"].ToString();
            objDPArr[4].Value = dr["CLINICNO"].ToString();
            objDPArr[5].Value = dr["BIHNO"].ToString();
            objDPArr[6].Value = dr["NAME"].ToString();
            objDPArr[7].Value = dr["SEX"].ToString();
            objDPArr[8].Value = dr["AGE"].ToString();
            objDPArr[9].Value = dr["AREA"].ToString();
            objDPArr[10].Value = dr["BEDNO"].ToString();
            objDPArr[11].Value = dr["TEL"].ToString();
            objDPArr[12].Value = dr["ADDRESS"].ToString();
            objDPArr[13].Value = dr["SUMMARY"].ToString();
            objDPArr[14].Value = dr["DIAGNOSE"].ToString();
            objDPArr[15].Value = dr["DOCTORNAME"].ToString();
            objDPArr[16].Value = dr["DOCTORNO"].ToString();
            objDPArr[17].Value = dr["EXTRANO"].ToString();
            objDPArr[18].Value = dr["CARDNO"].ToString();
            objDPArr[19].Value = dr["DEPARTMENT"].ToString();
            objDPArr[20].Value = dr["CHARGEDETAIL"].ToString();
            objDPArr[21].Value = DateTime.Parse(dr["FINISHDATE"].ToString());
            objDPArr[22].Value = DateTime.Parse(dr["APPLYDATE"].ToString());
            objDPArr[23].Value = dr["Deleted"].ToString();
            objDPArr[24].Value = dr["ApplyTitle"].ToString();
            objDPArr[25].Value = dr["DIAGNOSEAIM"].ToString();
            objDPArr[26].Value = dr["DIAGNOSEPART"].ToString();
            objDPArr[27].Value = dr["TypeID"].ToString();
            objDPArr[28].Value = dr["DEPTID_CHR"].ToString();
            objDPArr[29].Value = dr["AREAID_CHR"].ToString();
            objDPArr[30].Value = dr["DOCTORID_CHR"].ToString();
            objDPArr[31].Value = dr["SUBMITTED"].ToString();
            objDPArr[32].Value = dr["ApplyID"].ToString();

           
            try
            {
                 lngRes = m_objHRPService.lngExecuteParameterSQL(strSQLSet, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

         
        }

		
	}
}
