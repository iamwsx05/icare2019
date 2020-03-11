using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity; 
using System.Data;

namespace com.digitalwave.DiseaseTrackService
{

	/// <summary>
	/// 病程记录中间件的父类。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public abstract class clsDiseaseTrackService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		/// <summary>
		/// 获取病人的该记录时间列表
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
		/// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetRecordTimeList( string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr, out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            return 0;
        }
        /// <summary>
        /// 根据住院登记号获取病人的该记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreateDateArr">创建时间数组</param>
        /// <param name="p_strRecordDateArr">界面记录时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetRecordTimeList(  string p_strRegisterId,out string[] p_strCreateDateArr,out string[] p_strRecordDateArr)
        { 
            p_strCreateDateArr = null;
            p_strRecordDateArr = null;
            return 0;
        }

		/// <summary>
		/// 获取指定记录的内容
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_objRecordContent">返回的记录内容</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent( string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenDate,
			out clsTrackRecordContent p_objRecordContent)
		{
			p_objRecordContent = null; 
			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngGetRecordContentWithServ(p_strInPatientID,p_strInPatientDate,p_strOpenDate,objHRP,out p_objRecordContent);
        }
        #region 不用
        /// <summary>
        /// 获取指定记录的内容(茶山有摄入排出的护理表单用)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_strFormID">表单ID</param>
        /// <param name="p_objRecordContent">返回的记录内容</param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngGetRecordContent(
        //    string p_strInPatientID,
        //    string p_strInPatientDate,
        //    string p_strOpenDate,string p_strFormID,
        //    out clsTrackRecordContent p_objRecordContent)
        //{
        //    p_objRecordContent = null;
        //    com.digitalwave.security.clsPrivilegeHandleService obj = new com.digitalwave.security.clsPrivilegeHandleService();
        //    long lngCheckRes = obj.m_lngCheckCallPrivilege(p_objPrincipal, "clsDiseaseTrackService", "m_lngGetRecordContent");
        //    //obj.Dispose();
        //    if (lngCheckRes <= 0)
        //        return lngCheckRes;

        //    clsHRPTableService objHRP = new clsHRPTableService();
        //    return m_lngGetRecordContentWithServ(p_strInPatientID, p_strInPatientDate, p_strOpenDate,p_strFormID, objHRP, out p_objRecordContent);
        //}
        #endregion
        /// <summary>
        ///  根据住院登记号获取指定记录的内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCteatedDate">创建时间</param>
        /// <param name="p_objRecordContent">返回的记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordContent( string p_strRegisterId,
            string p_strCteatedDate,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null; 
            clsHRPTableService objHRP = new clsHRPTableService();
            return m_lngGetRecordContentWithServ(p_strRegisterId, p_strCteatedDate,out p_objRecordContent);
        }
        /// <summary>
        /// 根据住院登记号获取指定记录的内容。
        /// </summary>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCteatedDate">创建时间</param>
        /// <param name="p_objRecordContent">返回的记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetRecordContentWithServ(string p_strRegisterId, string p_strCteatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            return 0;
        }

		/// <summary>
		/// 获取指定记录的内容。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContent">返回的记录内容</param>
		/// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        { 
            p_objRecordContent = null;
            return 0;
        }
        #region 不用
        /// <summary>
        /// 获取指定记录的内容(茶山带摄入排出的护理表单用)。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_strFormID">表单ID</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">返回的记录内容</param>
        /// <returns></returns>
        //[AutoComplete]
        //protected virtual long m_lngGetRecordContentWithServ(string p_strInPatientID,
        //    string p_strInPatientDate,
        //    string p_strOpenDate,
        //     string p_strFormID,
        //    clsHRPTableService p_objHRPServ,
        //    out clsTrackRecordContent p_objRecordContent)
        //{
        //    p_objRecordContent = null;
        //    return 0;
        //}
        #endregion
        /// <summary>
		///  添加新记录。
		/// 1.生成 HRPServ。
		/// 2.添加新记录。(m_lngAddNewRecordWithServ)
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objModifyInfo">若存在相同的创建时间,返回该记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewRecord( clsTrackRecordContent p_objRecordContent,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo = null; 
			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngAddNewRecordWithServ(p_objRecordContent,objHRP,out p_objModifyInfo);
		}

		/// <summary>
		/// 添加新记录。
		/// 1.先查看是否有相同的记录时间。（m_lngCheckCreateDate）
		/// 2.添加记录到数据库。(m_lngAddNewRecord2DB)
		/// </summary>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">若存在相同记录,返回该记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngAddNewRecordWithServ(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;
			
			if(p_objRecordContent == null || p_objHRPServ == null)
				return -1; 
		
			long lngRes = m_lngCheckCreateDate(p_objRecordContent,p_objHRPServ,out p_objModifyInfo);           
		
			if(lngRes <= 0)
				return lngRes;     
		
			return m_lngAddNewRecord2DB(p_objRecordContent,p_objHRPServ);
			

		}	

		/// <summary>
		///  修改记录。
		/// 1.生成 HRPServ。
		/// 2.修改记录。(m_lngModifyRecordWithServ)
		/// </summary>
		/// <param name="p_objOldRecordContent">修改之前的原记录内容</param>
		/// <param name="p_objNewRecordContent">修改后的记录内容</param>
		/// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngModifyRecord( clsTrackRecordContent p_objOldRecordContent,
			clsTrackRecordContent p_objNewRecordContent,
			out clsPreModifyInfo p_objModifyInfo)
		{
			
			p_objModifyInfo=null; 
			if(p_objOldRecordContent == null || p_objNewRecordContent == null)
				return (long)enmOperationResult.Parameter_Error;

			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngModifyRecordWithServ(p_objOldRecordContent,p_objNewRecordContent,objHRP,out  p_objModifyInfo);
		}

		/// <summary>
		/// 修改记录。
		/// 1.查看当前记录是否最新的记录。（m_lngCheckLastModifyRecord）
		/// 2.把新修改的内容保存到数据库。(m_lngModifyRecord2DB)
		/// </summary>
		/// <param name="p_objOldRecordContent">修改之前的原记录内容</param>
		/// <param name="p_objNewRecordContent">修改后的记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngModifyRecordWithServ(clsTrackRecordContent p_objOldRecordContent,
			clsTrackRecordContent p_objNewRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{			
			p_objModifyInfo=null;
			
			if(p_objOldRecordContent == null || p_objNewRecordContent == null|| p_objHRPServ == null)
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = m_lngCheckLastModifyRecord(p_objOldRecordContent,p_objHRPServ,out p_objModifyInfo);           
		
			if(lngRes <= 0)
				return lngRes;                     
			
			return m_lngModifyRecord2DB(p_objNewRecordContent,p_objHRPServ);
		}

		/// <summary>
		/// 删除记录。
		/// 1.生成 HRPServ。
		/// 2.删除记录。(m_lngDeleteRecordWithServ)
		/// </summary>
		/// <param name="p_objRecordContent">当前要删除的记录</param>	
		/// <param name="p_objModifyInfo">若当前要删除的记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecord( clsTrackRecordContent p_objRecordContent,			
			out clsPreModifyInfo p_objModifyInfo)
		{			
			p_objModifyInfo=null; 
			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngDeleteRecordWithServ(p_objRecordContent,objHRP,out p_objModifyInfo);
		}

		/// <summary>
		///  删除记录。
		/// 1.查看当前记录是否最新的记录。（m_lngCheckLastModifyRecord）
		/// 2.把记录从数据中“删除”。(m_lngDeleteRecord2DB)	
		/// </summary>
		/// <param name="p_objRecordContent">当前要删除的记录</param>
		/// <param name="p_objHRPServ"></param>		
		/// <param name="p_objModifyInfo">若当前要删除的记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngDeleteRecordWithServ(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,			
			out clsPreModifyInfo p_objModifyInfo)
		{			
			p_objModifyInfo=null;

			if(p_objRecordContent == null || p_objHRPServ == null)
				return (long)enmOperationResult.Parameter_Error;
		
			long lngRes = m_lngCheckLastModifyRecord(p_objRecordContent,p_objHRPServ,out p_objModifyInfo);           
		
			if(lngRes <= 0)
				return lngRes;                     
		
			return m_lngDeleteRecord2DB(p_objRecordContent,p_objHRPServ);
		}

		/// <summary>
		/// 作废重做记录。
		/// 1.生成 HRPServ。
		/// 2.检查是否可以删除记录和添加记录。
		/// 3.删除记录。
		/// 4.添加记录。
		/// </summary>
		/// <param name="p_objDelRecord">要作废的记录</param>
		/// <param name="p_objAddNewRecord">新的记录</param>		
		/// <param name="p_objPreModifyInfo">若当前要作废的记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngReAddNewRecord( clsTrackRecordContent p_objDelRecord,
			clsTrackRecordContent p_objAddNewRecord,			
			out clsPreModifyInfo p_objPreModifyInfo)			
		{
			p_objPreModifyInfo = null; 
			clsHRPTableService objHRP = new clsHRPTableService();   
		
			long lngRes = m_lngCheckLastModifyRecord(p_objDelRecord,objHRP,out p_objPreModifyInfo); 
		
			if(lngRes <= 0)
				return lngRes;   
		
			lngRes = m_lngDeleteRecord2DB(p_objDelRecord,objHRP);  
		
			if(lngRes <= 0)
				return lngRes;   
		
			return m_lngAddNewRecord2DB(p_objAddNewRecord,objHRP); 
		}			

		/// <summary>
		/// 查看是否有相同的记录时间
		/// </summary>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objPreModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objPreModifyInfo);

		/// <summary>
		/// 保存记录到数据库。
		/// </summary>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ);

		/// <summary>
		/// 查看当前记录是否最新的记录。
		/// </summary>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>		
		/// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,			
			out clsPreModifyInfo p_objModifyInfo);

		/// <summary>
		/// 把新修改的内容保存到数据库。
		/// </summary>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ);

		/// <summary>
		/// 把记录从数据中“删除”。
		/// </summary>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ);

		/// <summary>
		/// 获取打印信息。
		/// 1.获取打印内容：如果输入参数p_dtmModifyDate不是最新的ModifyDate，输出变量 p_objContent
		///   会存放最新的内容；否则，输出变量为null。
		/// 2.获取打印时间：输出变量 p_dtmFirstPrintDate 存放首次打印时间。p_blnIsFirstPrint标记
		///   是否首次打印，如果是为true，客户端在打印后需要保存p_dtmFirstPrintDate到数据库。		
        /// </summary>
        /// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_dtmModifyDate">修改时间</param>
		/// <param name="p_objContent">记录内容</param>
		/// <param name="p_dtmFirstPrintDate">首次打印时间</param>
		/// <param name="p_blnIsFirstPrint">是否首次打印</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPrintInfo( string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenDate,
			DateTime p_dtmModifyDate,
			out clsTrackRecordContent p_objContent,
			out DateTime p_dtmFirstPrintDate,
			out bool p_blnIsFirstPrint)
		{
			p_objContent = null;
			p_dtmFirstPrintDate=DateTime.Now;
			p_blnIsFirstPrint=true; 
			//参数检查                     
			if(p_strInPatientID==null||p_strInPatientID==""||p_strInPatientDate==null||p_strInPatientDate==""||p_strOpenDate==null||p_strOpenDate=="")
				return (long)enmOperationResult.Parameter_Error;
		
			clsHRPTableService objHRP = new clsHRPTableService();
		
			//获取时间
			DateTime dtmModifyDate;
			string strFirstPrintDate;
			long lngRes = m_lngGetModifyDateAndFirstPrintDate(p_strInPatientID,p_strInPatientDate,p_strOpenDate,objHRP,out dtmModifyDate,out strFirstPrintDate);
		
			if(lngRes <= 0)
				return lngRes;
		
			//判断dtmModifyDate和p_dtmModifyDate是否一致
			//如果不一致
			if(p_dtmModifyDate!=dtmModifyDate)
			{
				lngRes = m_lngGetRecordContentWithServ(p_strInPatientID,p_strInPatientDate,p_strOpenDate,objHRP,out p_objContent);       
				if(lngRes <= 0)
					return lngRes;
			}
			//判断strFirstPrintDate是否为null
			//如果是
			if(strFirstPrintDate==null|| strFirstPrintDate.ToString().Trim().Length==0)
			{
				p_dtmFirstPrintDate = DateTime.Now;
				p_blnIsFirstPrint = true;
			}				
			else//如果不是
			{
				p_dtmFirstPrintDate = DateTime.Parse(strFirstPrintDate);
				p_blnIsFirstPrint = false;
			}
            //objHRP.Dispose();
			return lngRes;
		}
        /// <summary>
        /// 获取打印信息。
        /// 1.获取打印内容：如果输入参数p_dtmModifyDate不是最新的ModifyDate，输出变量 p_objContent
        ///   会存放最新的内容；否则，输出变量为null。
        /// 2.获取打印时间：输出变量 p_dtmFirstPrintDate 存放首次打印时间。p_blnIsFirstPrint标记
        ///   是否首次打印，如果是为true，客户端在打印后需要保存p_dtmFirstPrintDate到数据库。		
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreatedDate">创建日期</param>
        /// <param name="p_dtmModifyDate">修改时间</param>
        /// <param name="p_objContent">记录内容</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <param name="p_blnIsFirstPrint">是否首次打印</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrintInfo(  string p_strRegisterId,
            string p_strCreatedDate,
            DateTime p_dtmModifyDate,
            out clsTrackRecordContent p_objContent,
            out DateTime p_dtmFirstPrintDate,
            out bool p_blnIsFirstPrint)
        {
            p_objContent = null;
            p_dtmFirstPrintDate = DateTime.Now;
            p_blnIsFirstPrint = true; 
            //参数检查                     
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRP = new clsHRPTableService();

            //获取时间
            DateTime dtmModifyDate;
            string strFirstPrintDate;
            long lngRes = m_lngGetModifyDateAndFirstPrintDate(p_strRegisterId, p_strCreatedDate, out dtmModifyDate, out strFirstPrintDate);

            if (lngRes <= 0)
                return lngRes;

            //判断dtmModifyDate和p_dtmModifyDate是否一致
            //如果不一致
            if (p_dtmModifyDate != dtmModifyDate)
            {
                lngRes = m_lngGetRecordContentWithServ(p_strRegisterId, p_strCreatedDate, out p_objContent);
                if (lngRes <= 0)
                    return lngRes;
            }
            //判断strFirstPrintDate是否为null
            //如果是
            if (strFirstPrintDate == null || strFirstPrintDate.ToString().Trim().Length == 0)
            {
                p_dtmFirstPrintDate = DateTime.Now;
                p_blnIsFirstPrint = true;
            }
            else//如果不是
            {
                p_dtmFirstPrintDate = DateTime.Parse(strFirstPrintDate);
                p_blnIsFirstPrint = false;
            }
            //objHRP.Dispose();
            return lngRes;
        }
        /// <summary>
        /// 获取数据库中最新的修改时间和首次打印时间
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="dtmModifyDate">修改时间</param>
        /// <param name="strFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
		[AutoComplete]
        protected virtual long m_lngGetModifyDateAndFirstPrintDate(string p_strRegisterId, string p_strCreatedDate, out DateTime p_dtmModifyDate, out string p_strFirstPrintDate)
        {
            p_strFirstPrintDate = null;
            p_dtmModifyDate = DateTime.MinValue;
            return 0;
        }

		/// <summary>
		/// 获取数据库中最新的修改时间和首次打印时间
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_dtmModifyDate">修改时间</param>
		/// <param name="p_strFirstPrintDate">首次打印时间</param>
		/// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        { 
            p_strFirstPrintDate = null;
            p_dtmModifyDate = DateTime.MinValue;
            return 0;
        }

		/// <summary>
		/// 更新数据库中的首次打印时间。
		/// </summary>
        /// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenDate">记录时间</param>
		/// <param name="p_dtmFirstPrintDate">首次打印时间</param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            return 0;
        }
        /// <summary>
        ///  更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreatedDate">创建时间</param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
            string p_strCreatedDate,
            DateTime p_dtmFirstPrintDate)
        {
            return 0;
        }

		/// <summary>
		/// 获取病人的已经被某用户删除记录时间列表。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strDeleteUserID">删除操作者ID</param>
		/// <param name="p_strCreateRecordTimeArr">用户填写的记录时间数组</param>
		/// <param name="p_strOpenRecordTimeArr">系统生成的记录时间数组</param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            return 0;
        }
        /// <summary>
        /// 获取病人的已经被某用户删除记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strDeleteUserID">删除操作者ID</param>
        /// <param name="p_strRecordTimeArr">用户填写的记录时间数组</param>
        /// <param name="p_strCreatedDateArr">系统生成的记录时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeList(
            string p_strRegisterId,
            string p_strDeleteUserID,
            out string[] p_strRecordTimeArr,
            out string[] p_strCreatedDateArr)
        { 
            p_strRecordTimeArr = null;
            p_strCreatedDateArr = null;
            return 0;
        }

		/// <summary>
		/// 获取病人的已经被删除记录时间列表。
		/// </summary>
        /// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strCreateRecordTimeArr">入院日期</param>
		/// <param name="p_strOpenRecordTimeArr">系统生成的记录时间数组</param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            return 0;
        }
        /// <summary>
        /// 获取病人的已经被删除记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreateDateArr">创建时间</param>
        /// <param name="p_strRecordTimeArr">界面的记录时间</param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetDeleteRecordTimeListAll(
            string p_strRegisterId,
            out string[] p_strCreateDateArr,
            out string[] p_strRecordTimeArr)
        {
            p_strCreateDateArr = null;
            p_strRecordTimeArr = null;
            return 0;
        }


		/// <summary>
		/// 获取指定已经被删除记录的内容。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenRecordTime">记录时间</param>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetDeleteRecordContent( string p_strInPatientID,
			string p_strInPatientDate,
			string p_strOpenRecordTime,
			out clsTrackRecordContent p_objRecordContent)
		{
			p_objRecordContent = null; 
			clsHRPTableService objHRP = new clsHRPTableService();
			return m_lngGetDeleteRecordContentWithServ(p_strInPatientID,p_strInPatientDate,p_strOpenRecordTime,objHRP,out p_objRecordContent);
		}
        /// <summary>
        /// 获取指定已经被删除记录的内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreatedDate">创建时间</param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeleteRecordContent( string p_strRegisterId,
            string p_strCreatedDate,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null; 
            clsHRPTableService objHRP = new clsHRPTableService();
            return m_lngGetDeleteRecordContentWithServ(p_strRegisterId, p_strCreatedDate, out p_objRecordContent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreatedDate">创建时间</param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetDeleteRecordContentWithServ(string p_strRegisterId, string p_strCreatedDate, out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            return 0;
        }

		/// <summary>
		/// 获取指定已经被删除记录的内容。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_strOpenRecordTime">记录时间</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objRecordContent">记录内容</param>
		/// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenRecordTime,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        { 
            p_objRecordContent = null;
            return 0;
        }


        /// <summary>
        /// 获取一次住院全部作废记录
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public virtual long m_lngGetAllInactiveInfo(string p_strSQL,string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            p_objInactiveRecordInfoArr = null;
            if (string.IsNullOrEmpty(p_strInpatientId) || p_dtmInpatientDate == DateTime.MinValue || string.IsNullOrEmpty(p_strSQL)) return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                DataTable dtExist = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(p_strSQL, ref dtExist, objDPArr);
                if (lngRes > 0 && dtExist.Rows.Count > 0)
                {
                    p_objInactiveRecordInfoArr = new clsInactiveRecordInfo_VO[dtExist.Rows.Count];
                    DataRow objRow = null;
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtExist.Rows.Count; i++)
                    {
                        objRow = dtExist.Rows[i];
                        clsInactiveRecordInfo_VO objInfo = new clsInactiveRecordInfo_VO();
                        objInfo.m_StrInpatientId = p_strInpatientId;
                        objInfo.m_DtmInpatientDate = p_dtmInpatientDate;
                        if (DateTime.TryParse(objRow["createdate"].ToString(), out dtmTemp))
                            objInfo.m_DtmCreatedDate = dtmTemp;
                        if (DateTime.TryParse(objRow["opendate"].ToString(), out dtmTemp))
                            objInfo.m_DtmOpenDate = dtmTemp;
                        if (DateTime.TryParse(objRow["deactiveddate"].ToString(), out dtmTemp))
                            objInfo.m_DtmDeactiveDate = dtmTemp;
                        objInfo.m_StrCreatedUser = objRow["createdusername"].ToString();
                        objInfo.m_StrDeactiveUser = objRow["deactiveusername"].ToString();
                        p_objInactiveRecordInfoArr[i] = objInfo;
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }


	}// END CLASS DEFINITION clsDiseaseTrackService
}
