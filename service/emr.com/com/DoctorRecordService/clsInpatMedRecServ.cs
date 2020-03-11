using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using System.Drawing; 

namespace com.digitalwave.DoctorRecordService
{
    /// <summary>
    /// 专科住院病历中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsInpatMedRecServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region Sql
        private const string c_strAddNewMain = @"insert into inpatmedrec
      (typeid, inpatientid, inpatientdate, opendate, createdate, createuserid, ifconfirm, 
      firstprintdate, credibility, representor, status, deactiveddate, deactivedoperatorid,sequence_int,recorddate)
values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?)";//15

        private const string c_strAddNewItem = @"insert into inpatmedrec_item
      (typeid, inpatientid, inpatientdate, opendate, itemid, itemcontent, itemcontentxml)
values (?, ?, ?, ?, ?, ?, ?)";

        private const string c_strAddNewPic = @"insert into inpatmedrec_pic(typeid,inpatientid,inpatientdate,opendate,picid,frontimage,backcolor,width,height,pictureboxname) 
		values(?,?,?,?,?,?,?,?,?,?)";

        private const string c_strAddNewPatient_Disease = @"insert into patient_associate
      (inpatientid, inpatientdate,associateid)
values (?,?,?)";

        private const string c_strGetTimeListSQL = "select inpatientdate,createdate,opendate from inpatmedrec where typeid = ? and　inpatientid　 = ?  and status=0 order by opendate desc";
        private const string c_strGetTimeListSQL2 = "select createdate,opendate from inpatmedrec where typeid = ? and　inpatientid　 = ?  and inpatientdate = ? and status=0 order by opendate desc";

        private const string c_strGetMainRecord = @"select typeid,
       inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       ifconfirm,
       firstprintdate,
       credibility,
       representor,
       status,
       deactiveddate,
       deactivedoperatorid,
       sequence_int,
       markstatus_int,
       recorddate
from inpatmedrec
where typeid = ? and inpatientid = ? and inpatientdate= ? and status=0";

        private const string c_strGetMainRecord2 = @"select typeid,
       inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       ifconfirm,
       firstprintdate,
       credibility,
       representor,
       status,
       deactiveddate,
       deactivedoperatorid,
       sequence_int,
       markstatus_int,
       recorddate
from inpatmedrec
where typeid = ? and inpatientid = ? and inpatientdate= ? and opendate = ? and status=0";

        private const string c_strGetItemRecord = @"select a.itemid, a.itemcontent, a.itemcontentxml, b.itemname
  from inpatmedrec_item a
  left outer join inpatmedrec_type_item b on a.typeid = b.typeid　
                                         and a.itemid = b.itemid 　
 where a.typeid = ?
   and a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?";

        private const string c_strGetPics = @"select  typeid,
       inpatientid,
       inpatientdate,
       opendate,
       picid,
       backimage,
       frontimage,
       backcolor,
       width,
       height,
       pictureboxname
from inpatmedrec_pic
where typeid = ? and inpatientid = ? and inpatientdate = ? and opendate = ?";

        private const string c_strModifyMainRecord = @"update inpatmedrec
set credibility = ?, representor = ?,sequence_int = ?,markstatus_int = ?,recorddate=?
where typeid = ? and inpatientid = ? and inpatientdate = ? and opendate = ?";//9

        private const string c_strModifyItem = @"update inpatmedrec_item
set itemcontent = ?, itemcontentxml = ?
where (　typeid　 = ?) and (　inpatientid　 = ?) and (inpatientdate = ?) and (opendate = ?) and 
      (　itemid　 = ?)";

        private const string c_strDeletePics = @"delete from inpatmedrec_pic
where (　typeid　 = ?) and (　inpatientid　 = ?) and (inpatientdate = ?) and (opendate = ?)";

        private const string c_strUpdateFirstPrintDateSQL = @"update  inpatmedrec set firstprintdate= ? where typeid = ? and inpatientid = ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        private const string c_strDeleteMainRecord = @"update inpatmedrec set status = 1,deactiveddate=?,deactivedoperatorid=? 
where (　typeid　 = ?) and (　inpatientid　 = ?) and (inpatientdate = ?) and (opendate = ?)";

        private const string c_strGetDeactiveRecord = @"select typeid,
       inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       ifconfirm,
       firstprintdate,
       credibility,
       representor,
       status,
       deactiveddate,
       deactivedoperatorid,
       sequence_int,
       markstatus_int,
       recorddate
from inpatmedrec
where typeid = ? and inpatientid = ? and opendate = ? and status=1";


        private const string c_strGetAllformID = @"select typeid, typename from inpatmedrec_type";
        #endregion

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewRecord(
            clsInpatMedRecContent p_objContent)
        {
            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsBaseCaseHistorySevice","m_lngAddNewRecord");
                //			if(lngCheckRes <= 0)
                //				//return lngCheckRes;	
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("seq_emr_sign", out lngSequence);

                #region 插入主表
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(15, out objDPArr);

                objDPArr[0].Value = p_objContent.m_strTypeID;
                objDPArr[1].Value = p_objContent.m_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objContent.m_dtmInPatientDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objContent.m_dtmOpenDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objContent.m_dtmCreateDate;
                objDPArr[5].Value = p_objContent.m_strCreateUserID.Trim();
                objDPArr[6].Value = p_objContent.m_bytIfConfirm;
                objDPArr[7].DbType = DbType.DateTime;
                if (p_objContent.m_dtmFirstPrintDate == DateTime.MinValue)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = p_objContent.m_dtmFirstPrintDate;
                objDPArr[8].Value = (p_objContent.m_strCredibility == null ? "" : p_objContent.m_strCredibility);
                objDPArr[9].Value = (p_objContent.m_strRepresentor == null ? "" : p_objContent.m_strRepresentor);
                objDPArr[10].Value = p_objContent.m_bytStatus;
                objDPArr[11].DbType = DbType.DateTime;
                if (p_objContent.m_dtmDeActivedDate == DateTime.MinValue)
                    objDPArr[11].Value = DBNull.Value;
                else
                    objDPArr[11].Value = p_objContent.m_dtmDeActivedDate;
                objDPArr[12].Value = (p_objContent.m_strDeActivedOperatorID == null ? "" : p_objContent.m_strDeActivedOperatorID);
                objDPArr[13].Value = lngSequence;
                objDPArr[14].DbType = DbType.DateTime;
                objDPArr[14].Value = p_objContent.m_dtmRecordDate;
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewMain, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objContent.objSignerArr, lngSequence);
                #endregion

                #region 插入项目子表
                lngRes = m_lngAddItemRecord(p_objContent);
                if (lngRes <= 0)
                    return lngRes;
                #endregion

                #region 保存图片

                if (p_objContent.m_objPics != null && p_objContent.m_objPics.Length > 0)
                {
                    for (int j = 0 ; j < p_objContent.m_objPics.Length ; j++)
                    {
                        IDataParameter[] objDPAr9 = null;
                        objHRPServ.CreateDatabaseParameter(10, out objDPAr9);
                        objDPAr9[0].Value = p_objContent.m_strTypeID;
                        objDPAr9[1].Value = p_objContent.m_strInPatientID;
                        objDPAr9[2].DbType = DbType.DateTime;
                        objDPAr9[2].Value = p_objContent.m_dtmInPatientDate;
                        objDPAr9[3].DbType = DbType.DateTime;
                        objDPAr9[3].Value = p_objContent.m_dtmOpenDate;

                        objDPAr9[4].Value = j + 1;
                        objDPAr9[5].DbType = DbType.Binary;
                        if (p_objContent.m_objPics[j].m_bytImage != null)
                            objDPAr9[5].Value = p_objContent.m_objPics[j].m_bytImage;
                        else
                            objDPAr9[5].Value = System.DBNull.Value;

                        objDPAr9[6].Value = p_objContent.m_objPics[j].clrBack.ToArgb();

                        objDPAr9[7].Value = p_objContent.m_objPics[j].intWidth;
                        objDPAr9[8].Value = p_objContent.m_objPics[j].intHeight;
                        objDPAr9[9].Value = p_objContent.m_objPics[j].m_StrPictureBoxName;

                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewPic, ref lngEff, objDPAr9);
                        if (lngRes <= 0)
                            return lngRes;
                    }
                }
                #endregion

                #region 保存病名
                if (!string.IsNullOrEmpty(p_objContent.m_strDiseaseID))//套装模板与病名挂勾
                {
                    lngRes = m_lngSavePatient_Disease(p_objContent.m_strInPatientID, p_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objContent.m_strDiseaseID, p_objContent.m_strDeptID);
                    if (lngRes <= 0)
                        return lngRes;
                }
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

            }
            //返回
            return lngRes;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddItemRecord(clsInpatMedRecContent p_objContent)
        {
            if (p_objContent == null || p_objContent.m_objItemContents == null)
                return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0 ; i < p_objContent.m_objItemContents.Length ; i++)
                    {
                        IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                        objHRPServ.CreateDatabaseParameter(7, out objDPArr2);
                        objDPArr2[0].Value = p_objContent.m_strTypeID;
                        objDPArr2[1].Value = p_objContent.m_strInPatientID;
                        objDPArr2[2].DbType = DbType.DateTime;
                        objDPArr2[2].Value = p_objContent.m_dtmInPatientDate;
                        objDPArr2[3].DbType = DbType.DateTime;
                        objDPArr2[3].Value = p_objContent.m_dtmOpenDate;
                        objDPArr2[4].Value = p_objContent.m_objItemContents[i].m_strItemID;
                        objDPArr2[5].Value = p_objContent.m_objItemContents[i].m_strItemContent;
                        objDPArr2[6].Value = (p_objContent.m_objItemContents[i].m_strItemContentXml == null ? "" : p_objContent.m_objItemContents[i].m_strItemContentXml);

                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewItem, ref lngEff, objDPArr2);

                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.Date, DbType.Date, DbType.String, DbType.String, DbType.String };
                    object[][] objValues = new object[7][];
                    if (p_objContent.m_objItemContents.Length > 0)
                    {
                        for (int j = 0 ; j < objValues.Length ; j++)
                        {
                            objValues[j] = new object[p_objContent.m_objItemContents.Length];//初始化
                        }

                        for (int k1 = 0 ; k1 < p_objContent.m_objItemContents.Length ; k1++)
                        {
                            objValues[0][k1] = p_objContent.m_strTypeID;
                            objValues[1][k1] = p_objContent.m_strInPatientID;
                            objValues[2][k1] = p_objContent.m_dtmInPatientDate;
                            objValues[3][k1] = p_objContent.m_dtmOpenDate;
                            objValues[4][k1] = p_objContent.m_objItemContents[k1].m_strItemID;
                            objValues[5][k1] = p_objContent.m_objItemContents[k1].m_strItemContent;
                            objValues[6][k1] = p_objContent.m_objItemContents[k1].m_strItemContentXml;
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(c_strAddNewItem, objValues, dbTypes);
                    }
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
            //返回
            return lngRes;

        }

        /// <summary>
        /// 获取病人的该记录时间列表
        /// </summary>
        [AutoComplete]
        public long m_lngGetRecordTimeList(
            string p_strTypeID, string p_strInPatientID,
            out string[] p_strInPatientDateArr,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strInPatientDateArr = null;
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;

            //			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientCaseHistoryServ","m_lngGetRecordTimeList");
            //			if(lngCheckRes <= 0)
            //				//return lngCheckRes;	

            //检查参数
            clsHRPTableService objHRPServ = new clsHRPTableService();
            if (p_strInPatientID == null || p_strInPatientID == "")
                return (long)enmOperationResult.Parameter_Error;

            IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strTypeID;
            objDPArr[1].Value = p_strInPatientID.Trim();
            long lngRes = 0;

            try
            {
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                int intRowCount = dtbValue.Rows.Count;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strInPatientDateArr = new string[intRowCount];
                    p_strCreateRecordTimeArr = new string[intRowCount];
                    p_strOpenRecordTimeArr = new string[intRowCount];
                    DataRow objSelectRow = null;
                    for (int i = 0 ; i < intRowCount ; i++)
                    {
                        objSelectRow = dtbValue.Rows[i];
                        //设置结果
                        p_strInPatientDateArr[i] = objSelectRow["INPATIENTDATE"].ToString();
                        p_strCreateRecordTimeArr[i] = objSelectRow["CREATEDATE"].ToString();
                        p_strOpenRecordTimeArr[i] = objSelectRow["OPENDATE"].ToString();
                    }
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
            //返回
            return lngRes;

        }

        /// <summary>
        /// 获取病人一次入院的记录时间列表
        /// </summary>
        [AutoComplete]
        public long m_lngGetRecordTimeList(
            string p_strTypeID,
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            //out string[] p_strRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;

            //检查参数
            clsHRPTableService objHRPServ = new clsHRPTableService();
            if (p_strInPatientID == null || p_strInPatientID == "")
                return (long)enmOperationResult.Parameter_Error;

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strTypeID;
            objDPArr[1].Value = p_strInPatientID.Trim();
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strInPatientDate.Trim());
            long lngRes = 0;

            try
            {
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL2, ref dtbValue, objDPArr);
                int intRowCount = dtbValue.Rows.Count;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && intRowCount > 0)
                {
                    p_strCreateRecordTimeArr = new string[intRowCount];
                    p_strOpenRecordTimeArr = new string[intRowCount];
                    DataRow objSelectRow = null;
                    for (int i = 0; i < intRowCount; i++)
                    {
                        objSelectRow = dtbValue.Rows[i];
                        //设置结果
                        p_strCreateRecordTimeArr[i] = objSelectRow["CREATEDATE"].ToString();
                        p_strOpenRecordTimeArr[i] = objSelectRow["OPENDATE"].ToString();
                    }
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
            //返回
            return lngRes;

        }

        /// <summary>
        /// 获取指定记录内容
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordContent(
            string p_strTypeID, string p_strInPatientID, string p_strInPatientDate,
            out clsInpatMedRecContent p_objContent)
        {
            //参数判断
            p_objContent = null;
            long lngRes = 1;
            try
            {
                #region 主表信息
                lngRes = m_lngGetMainRecContent(p_strTypeID, p_strInPatientID, p_strInPatientDate, out p_objContent, false);
                if (lngRes <= 0)
                    return lngRes;
                #endregion

                #region 项目信息
                lngRes = m_lngGetItemRecord(  p_strTypeID, p_strInPatientID, p_strInPatientDate, p_objContent.m_dtmOpenDate, out p_objContent.m_objItemContents);
                if (lngRes <= 0)
                    return lngRes;
                #endregion

                #region 图片信息
                lngRes = m_lngGetPicContent(p_strTypeID, p_strInPatientID, p_strInPatientDate, ref  p_objContent);
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }

        /// <summary>
        /// 获取指定记录内容
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecordContent(
            string p_strTypeID, string p_strInPatientID, string p_strInPatientDate,DateTime p_dtmOpenDate,
            out clsInpatMedRecContent p_objContent)
        {
            //参数判断
            p_objContent = null;
            long lngRes = 1;
            try
            {
                #region 主表信息
                lngRes = m_lngGetMainRecContent(p_strTypeID, p_strInPatientID, p_strInPatientDate, p_dtmOpenDate,out p_objContent, false);
                if (lngRes <= 0)
                    return lngRes;
                #endregion

                #region 项目信息
                lngRes = m_lngGetItemRecord(  p_strTypeID, p_strInPatientID, p_strInPatientDate, p_objContent.m_dtmOpenDate, out p_objContent.m_objItemContents);
                if (lngRes <= 0)
                    return lngRes;
                #endregion

                #region 图片信息
                lngRes = m_lngGetPicContent(p_strTypeID, p_strInPatientID, p_strInPatientDate, ref  p_objContent);
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }

        /// <summary>
        /// 图片信息
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngGetPicContent(string p_strTypeID, string p_strInPatientID, string p_strInPatientDate,
            ref clsInpatMedRecContent p_objContent)
        {
            clsHRPTableService objHRPServ = new clsHRPTableService();

            IDataParameter[] objDPArr3 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
            objHRPServ.CreateDatabaseParameter(4, out objDPArr3);
            objDPArr3[0].Value = p_strTypeID.Trim();
            objDPArr3[1].Value = p_strInPatientID.Trim();
            objDPArr3[2].DbType = DbType.DateTime;
            objDPArr3[2].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr3[3].DbType = DbType.DateTime;
            objDPArr3[3].Value = p_objContent.m_dtmOpenDate;
            long lngRes = 0;

            try
            {
                DataTable dtbValue3 = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetPics, ref dtbValue3, objDPArr3);
                if (lngRes <= 0)
                    return lngRes;

                ArrayList arlPic = new ArrayList();
                int intRowCount = dtbValue3.Rows.Count;
                DataRow objSelectRow = null;
                for (int i = 0 ; i < intRowCount ; i++)
                {
                    objSelectRow = dtbValue3.Rows[i];
                    try
                    {
                        clsPictureBoxValue objPicValue = new clsPictureBoxValue();
                        objPicValue.m_bytImage = (byte[])(objSelectRow["FRONTIMAGE"]);
                        objPicValue.intWidth = Convert.ToInt32(objSelectRow["WIDTH"]);
                        objPicValue.intHeight = Convert.ToInt32(objSelectRow["HEIGHT"]);
                        objPicValue.clrBack = Color.FromArgb(Convert.ToInt32(objSelectRow["BACKCOLOR"]));
                        objPicValue.m_StrPictureBoxName = objSelectRow["PICTUREBOXNAME"].ToString();
                        arlPic.Add(objPicValue);
                    }
                    catch { continue; }
                }

                p_objContent.m_objPics = (clsPictureBoxValue[])arlPic.ToArray(typeof(clsPictureBoxValue));
                arlPic.Clear();
                return lngRes;
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
            //返回
            return lngRes;

        }
        #region 修改记录
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyRecord(
            clsInpatMedRecContent p_objContent)
        {
            //检查参数                              
            if (p_objContent == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region 更新主表
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(9, out objDPArr);

                objDPArr[0].Value = p_objContent.m_strCredibility;
                objDPArr[1].Value =  p_objContent.m_strRepresentor;
                objDPArr[2].Value = lngSequence;
                objDPArr[3].Value = p_objContent.m_intMarkStatus;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objContent.m_dtmRecordDate;
                objDPArr[5].Value =  p_objContent.m_strTypeID;
                objDPArr[6].Value =  p_objContent.m_strInPatientID;
                objDPArr[7].DbType = DbType.DateTime;
                objDPArr[7].Value = p_objContent.m_dtmInPatientDate;
                objDPArr[8].DbType = DbType.DateTime;
                objDPArr[8].Value = p_objContent.m_dtmOpenDate;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyMainRecord, ref lngEff, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objContent.objSignerArr, lngSequence);
                #endregion

                #region 更新项目信息

                ArrayList arlInsert = new ArrayList(10);
                ArrayList arlModify = new ArrayList(10);
                ArrayList arlDelete = new ArrayList(3);
                clsInpatMedRec_Item objItem = null;
                for (int i = 0; i < p_objContent.m_objItemContents.Length;i++ )
                {
                    objItem = p_objContent.m_objItemContents[i];
                    if (objItem.m_enmTextStatus == EnmItemTextStatus.NEW)
                        arlInsert.Add(objItem);
                    else if (objItem.m_enmTextStatus == EnmItemTextStatus.MODIFY)
                        arlModify.Add(objItem);
                    else if (objItem.m_enmTextStatus == EnmItemTextStatus.DELETE)
                        arlDelete.Add(objItem);
                }
                clsInpatMedRecContent objContent = new clsInpatMedRecContent();
                objContent.m_strTypeID = p_objContent.m_strTypeID;
                objContent.m_strInPatientID = p_objContent.m_strInPatientID;
                objContent.m_dtmInPatientDate = p_objContent.m_dtmInPatientDate;
                objContent.m_dtmOpenDate = p_objContent.m_dtmOpenDate;
                if (lngRes > 0)
                {
                    if (arlInsert.Count > 0)
                    {
                        objContent.m_objItemContents = (clsInpatMedRec_Item[])arlInsert.ToArray(typeof(clsInpatMedRec_Item));
                        lngRes = m_lngAddItemRecord(objContent);
                    }
                    if (lngRes > 0)
                    {
                        if (arlModify.Count > 0)
                        {
                            objContent.m_objItemContents = (clsInpatMedRec_Item[])arlModify.ToArray(typeof(clsInpatMedRec_Item));
                            lngRes = m_lngModifyItems(objContent);
                        }
                        if (lngRes > 0 && arlDelete.Count > 0)
                        {
                            objContent.m_objItemContents = (clsInpatMedRec_Item[])arlDelete.ToArray(typeof(clsInpatMedRec_Item));
                            lngRes = m_lngDeleteItems(objContent);
                            if (lngRes <= 0)
                                throw new Exception("m_lngDeleteItems:删除专科病历项目出错");
                        }
                        else if(lngRes <= 0)
                            throw new Exception("m_lngModifyItems:修改专科病历项目出错");
                    }
                    else
                        throw new Exception("m_lngAddItemRecord:添加专科病历项目出错");
                }
                objContent = null;
                #endregion

                #region 更新图片信息
                IDataParameter[] objDPArr3 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr3);

                objDPArr3[0].Value = p_objContent.m_strTypeID;
                objDPArr3[1].Value = p_objContent.m_strInPatientID;
                objDPArr3[2].DbType = DbType.DateTime;
                objDPArr3[2].Value = p_objContent.m_dtmInPatientDate;
                objDPArr3[3].DbType = DbType.DateTime;
                objDPArr3[3].Value = p_objContent.m_dtmOpenDate;

                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeletePics, ref lngEff, objDPArr3);
                if (lngRes <= 0)
                    throw new Exception("删除旧图片出错");

                //添加新记录
                if (p_objContent.m_objPics != null && p_objContent.m_objPics.Length > 0)
                {
                    IDataParameter[] objDPArr4 = null;//new Oracle.DataAccess.Client.OracleParameter[4];


                    for (int j = 0 ; j < p_objContent.m_objPics.Length ; j++)
                    {
                        //						//按顺序给IDataParameter赋值
                        //						for(int i=0;i<objDPArr4.Length;i++)
                        //							objDPArr4[i]=new Oracle.DataAccess.Client.OracleParameter();
                        objHRPServ.CreateDatabaseParameter(10, out objDPArr4);
                        objDPArr4[0].Value = p_objContent.m_strTypeID;
                        objDPArr4[1].Value = p_objContent.m_strInPatientID;
                        objDPArr4[2].DbType = DbType.DateTime;
                        objDPArr4[2].Value = p_objContent.m_dtmInPatientDate;
                        objDPArr4[3].DbType = DbType.DateTime;
                        objDPArr4[3].Value = p_objContent.m_dtmOpenDate;

                        objDPArr4[4].Value = j + 1;
                        objDPArr4[5].DbType = DbType.Binary;
                        if (p_objContent.m_objPics[j].m_bytImage != null)
                            objDPArr4[5].Value = p_objContent.m_objPics[j].m_bytImage;
                        else
                            objDPArr4[5].Value = System.DBNull.Value;

                        objDPArr4[6].Value = p_objContent.m_objPics[j].clrBack.ToArgb();

                        objDPArr4[7].Value = p_objContent.m_objPics[j].intWidth;
                        objDPArr4[8].Value = p_objContent.m_objPics[j].intHeight;
                        objDPArr4[9].Value = p_objContent.m_objPics[j].m_StrPictureBoxName;

                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewPic, ref lngEff, objDPArr4);
                        if (lngRes <= 0)
                            throw new Exception("添加图片出错");
                    }
                }
                #endregion

                #region 更新病名
                //if (p_objContent.m_strDiseaseID != "")//套装模板与病名挂勾
                //{
                //    lngRes = m_lngSavePatient_Disease(p_objContent.m_strInPatientID, p_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objContent.m_strDiseaseID, p_objContent.m_strDeptID);
                //    if (lngRes <= 0)
                //        return lngRes;
                //}
                #endregion
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
            }
            finally
            {
                //objHRPServ.Dispose();
            }
            return lngRes;
        }
        /// <summary>
        /// 删除子项目
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteItems(clsInpatMedRecContent p_objContent)
        {
            if (p_objContent == null || p_objContent.m_objItemContents.Length == 0) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strDeleteItems = @"delete from inpatmedrec_item 
                                        where typeid　 = ? and inpatientid　 = ? 
                                        and inpatientdate = ? and opendate = ? and itemid = ?";

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_objContent.m_objItemContents.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr2);
                        objDPArr2[0].Value = p_objContent.m_strTypeID;
                        objDPArr2[1].Value = p_objContent.m_strInPatientID;
                        objDPArr2[2].DbType = DbType.DateTime;
                        objDPArr2[2].Value = p_objContent.m_dtmInPatientDate;
                        objDPArr2[3].DbType = DbType.DateTime;
                        objDPArr2[3].Value = p_objContent.m_dtmOpenDate;
                        objDPArr2[4].Value = p_objContent.m_objItemContents[i].m_strItemID;

                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strDeleteItems, ref lngEff, objDPArr2);

                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.Date, DbType.Date, DbType.String};
                    object[][] objValues = new object[5][];
                    if (p_objContent.m_objItemContents.Length > 0)
                    {
                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[p_objContent.m_objItemContents.Length];//初始化
                        }

                        for (int k1 = 0; k1 < p_objContent.m_objItemContents.Length; k1++)
                        {
                            objValues[0][k1] = p_objContent.m_strTypeID;
                            objValues[1][k1] = p_objContent.m_strInPatientID;
                            objValues[2][k1] = p_objContent.m_dtmInPatientDate;
                            objValues[3][k1] = p_objContent.m_dtmOpenDate;
                            objValues[4][k1] = p_objContent.m_objItemContents[k1].m_strItemID;
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strDeleteItems, objValues, dbTypes);
                    }
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
            //返回
            return lngRes;

        }

        [AutoComplete]
        private long m_lngModifyItems(clsInpatMedRecContent p_objContent)
        {
            if (p_objContent == null || p_objContent.m_objItemContents.Length == 0) return -1;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strUpdateItems = @"update inpatmedrec_item
   set itemcontent = ?, itemcontentxml = ?
 where typeid = ?
   and inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and itemid = ?";

                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_objContent.m_objItemContents.Length; i++)
                    {
                        IDataParameter[] objDPArr2 = null;
                        objHRPServ.CreateDatabaseParameter(7, out objDPArr2);
                        objDPArr2[0].Value = p_objContent.m_objItemContents[i].m_strItemContent;
                        objDPArr2[1].Value = p_objContent.m_objItemContents[i].m_strItemContentXml;
                        objDPArr2[2].Value = p_objContent.m_strTypeID;
                        objDPArr2[3].Value = p_objContent.m_strInPatientID;
                        objDPArr2[4].DbType = DbType.DateTime;
                        objDPArr2[4].Value = p_objContent.m_dtmInPatientDate;
                        objDPArr2[5].DbType = DbType.DateTime;
                        objDPArr2[5].Value = p_objContent.m_dtmOpenDate;
                        objDPArr2[6].Value = p_objContent.m_objItemContents[i].m_strItemID;

                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strUpdateItems, ref lngEff, objDPArr2);

                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.String, DbType.Date, DbType.Date, DbType.String };
                    object[][] objValues = new object[7][];
                    if (p_objContent.m_objItemContents.Length > 0)
                    {
                        for (int j = 0; j < objValues.Length; j++)
                        {
                            objValues[j] = new object[p_objContent.m_objItemContents.Length];//初始化
                        }

                        for (int k1 = 0; k1 < p_objContent.m_objItemContents.Length; k1++)
                        {
                            objValues[0][k1] = p_objContent.m_objItemContents[k1].m_strItemContent;
                            objValues[1][k1] = p_objContent.m_objItemContents[k1].m_strItemContentXml;
                            objValues[2][k1] = p_objContent.m_strTypeID;
                            objValues[3][k1] = p_objContent.m_strInPatientID;
                            objValues[4][k1] = p_objContent.m_dtmInPatientDate;
                            objValues[5][k1] = p_objContent.m_dtmOpenDate;
                            objValues[6][k1] = p_objContent.m_objItemContents[k1].m_strItemID;
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strUpdateItems, objValues, dbTypes);
                    }
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
            //返回
            return lngRes;

        }
        #endregion 修改记录

        #region 私有函数
        //保存套装模板的病名
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strPatID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strDiseaseID"></param>
        /// <param name="p_strDeptID"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngSavePatient_Disease(string p_strPatID, string p_strInPatientDate, string p_strDiseaseID, string p_strDeptID)
        {
            string strSql = @"select a.inpatientid, a.inpatientdate, a.associateid, 
                b.associateid,
               b.deptid,
               b.formname,
               b.templatesetid,
               b.associatename,
               b.type from patient_associate a
							inner join templateset_associate b on 　a.associateid)　 = 　b.associateid　
							where 　inpatientid　 = ? 
							and inpatientdate = ?
							and b.type = '0' and 　b.deptid　 = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtExist = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeptID;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtExist, objDPArr);
                if (lngRes <= 0) return lngRes;
                if (dtExist.Rows.Count > 0)
                {

                    strSql = @"delete patient_associate
								where 
								　inpatientid　 = ? 
								and inpatientdate = ?
								and 　associateid　 in
								(select b.associateid from patient_associate a
								inner join templateset_associate b on 　a.associateid　 =　b.associateid　
								where 　inpatientid　 = ? 
								and inpatientdate = ?
								and b.type = '0' and 　b.deptid　 = ?)";

                    objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                    objDPArr[0].Value = p_strPatID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].Value = p_strPatID;
                    objDPArr[3].DbType = DbType.DateTime;
                    objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[4].Value = p_strDeptID;

                    long lngEff = -1;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                }
                if (p_strDiseaseID.Trim() != "")
                    lngRes = m_lngAddPatient_Disease(p_strPatID, p_strInPatientDate, p_strDiseaseID);

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
            //返回
            return lngRes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strPatID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strDiseaseID"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddPatient_Disease(string p_strPatID, string p_strInPatientDate, string p_strDiseaseID)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDiseaseID;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewPatient_Disease, ref lngEff, objDPArr);
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
            //返回
            return lngRes;

        }
        #endregion

        /// <summary>
        /// 住院病历主表信息
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objContent"></param>
        /// <param name="p_intStatus"></param>
        [AutoComplete]
        private long m_lngGetMainRecContent(string p_strTypeID, string p_strInPatientID, string p_strPrimaryDate,
            out clsInpatMedRecContent p_objContent, bool p_blnIsDeactiveRec)
        {
            p_objContent = null;
            //			string strGetMainRecord = @"select * from InpatMedRec
            //where TypeID = '"+p_strTypeID+"' and trim(InPatientID) = '"+ p_strInPatientID+"' and InPatientDate= "+ clsHRPTableService.s_strOracleDateTime(p_strPrimaryDate)+" and Status=0";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strTypeID.Trim();
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strPrimaryDate);

                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                if (p_blnIsDeactiveRec == false)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetMainRecord, ref dtbValue, objDPArr);
                else
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeactiveRecord, ref dtbValue, objDPArr);

                int intRowCount = dtbValue.Rows.Count;
                DataRow objSelectedRow = null;
                if (lngRes <= 0 || intRowCount <= 0)
                    return 0;

                //if (dtbValue.Rows.Count > 0)
                //{
                objSelectedRow = dtbValue.Rows[0];
                p_objContent = new clsInpatMedRecContent();
                p_objContent.m_strTypeID = objSelectedRow["TYPEID"].ToString();
                p_objContent.m_strInPatientID = objSelectedRow["INPATIENTID"].ToString();
                try
                {
                    p_objContent.m_dtmInPatientDate = DateTime.Parse(objSelectedRow["INPATIENTDATE"].ToString());
                    p_objContent.m_dtmOpenDate = DateTime.Parse(objSelectedRow["OPENDATE"].ToString());
                    p_objContent.m_dtmCreateDate = DateTime.Parse(objSelectedRow["CREATEDATE"].ToString());
                    p_objContent.m_dtmRecordDate = DateTime.Parse(objSelectedRow["RECORDDATE"].ToString());
                }
                catch { return 0; }
                p_objContent.m_strCreateUserID = objSelectedRow["CREATEUSERID"].ToString().TrimEnd();
                try
                {
                    p_objContent.m_bytIfConfirm = byte.Parse(objSelectedRow["IFCONFIRM"].ToString());
                }
                catch { p_objContent.m_bytIfConfirm = byte.Parse("0"); }
                try
                {
                    p_objContent.m_dtmFirstPrintDate = objSelectedRow["FIRSTPRINTDATE"] == System.DBNull.Value ? DateTime.MinValue : DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                }
                catch { p_objContent.m_dtmFirstPrintDate = DateTime.MinValue; }
                p_objContent.m_strCredibility = objSelectedRow["CREDIBILITY"].ToString();
                p_objContent.m_strRepresentor = objSelectedRow["REPRESENTOR"].ToString();
                try
                {
                    p_objContent.m_bytStatus = byte.Parse(objSelectedRow["STATUS"].ToString());
                }
                catch { p_objContent.m_bytStatus = byte.Parse("0"); }
                try
                {
                    p_objContent.m_dtmDeActivedDate = objSelectedRow["DEACTIVEDDATE"] == System.DBNull.Value ? DateTime.MinValue : DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                }
                catch { p_objContent.m_dtmDeActivedDate = DateTime.MinValue; }
                int intTemp = 0;
                if (int.TryParse(objSelectedRow["MARKSTATUS_INT"].ToString(), out intTemp))
                {
                    p_objContent.m_intMarkStatus = intTemp;
                }
                //获取签名集合
                if (objSelectedRow["SEQUENCE_INT"] != DBNull.Value)
                {
                    long lngS = 0;
                    if (long.TryParse(objSelectedRow["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out p_objContent.objSignerArr);

                        //释放
                        objSign = null;
                    }
                }
                //}
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
            //返回
            return lngRes;
        }

        /// <summary>
        /// 住院病历主表信息
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objContent"></param>
        /// <param name="p_intStatus"></param>
        [AutoComplete]
        private long m_lngGetMainRecContent(string p_strTypeID, string p_strInPatientID, string p_strPrimaryDate,DateTime p_dtmOpenDate,
            out clsInpatMedRecContent p_objContent, bool p_blnIsDeactiveRec)
        {
            p_objContent = null;
            //			string strGetMainRecord = @"select * from InpatMedRec
            //where TypeID = '"+p_strTypeID+"' and trim(InPatientID) = '"+ p_strInPatientID+"' and InPatientDate= "+ clsHRPTableService.s_strOracleDateTime(p_strPrimaryDate)+" and Status=0";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strTypeID.Trim();
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strPrimaryDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmOpenDate;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr1);
                objDPArr1[0].Value = p_strTypeID.Trim();
                objDPArr1[1].Value = p_strInPatientID.Trim();
                objDPArr1[2].DbType = DbType.DateTime;
                objDPArr1[2].Value = DateTime.Parse(p_strPrimaryDate);
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                if (p_blnIsDeactiveRec == false)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetMainRecord2, ref dtbValue, objDPArr);
                else
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeactiveRecord, ref dtbValue, objDPArr1);

                int intRowCount = dtbValue.Rows.Count;
                DataRow objSelectedRow = null;
                if (lngRes <= 0 || intRowCount <= 0)
                    return 0;

                //if (dtbValue.Rows.Count > 0)
                //{
                objSelectedRow = dtbValue.Rows[0];
                p_objContent = new clsInpatMedRecContent();
                p_objContent.m_strTypeID = objSelectedRow["TYPEID"].ToString();
                p_objContent.m_strInPatientID = objSelectedRow["INPATIENTID"].ToString();
                try
                {
                    p_objContent.m_dtmInPatientDate = DateTime.Parse(objSelectedRow["INPATIENTDATE"].ToString());
                    p_objContent.m_dtmOpenDate = DateTime.Parse(objSelectedRow["OPENDATE"].ToString());
                    p_objContent.m_dtmCreateDate = DateTime.Parse(objSelectedRow["CREATEDATE"].ToString());
                    p_objContent.m_dtmRecordDate = DateTime.Parse(objSelectedRow["RECORDDATE"].ToString());
                }
                catch { return 0; }
                p_objContent.m_strCreateUserID = objSelectedRow["CREATEUSERID"].ToString().TrimEnd();
                try
                {
                    p_objContent.m_bytIfConfirm = byte.Parse(objSelectedRow["IFCONFIRM"].ToString());
                }
                catch { p_objContent.m_bytIfConfirm = byte.Parse("0"); }
                try
                {
                    p_objContent.m_dtmFirstPrintDate = objSelectedRow["FIRSTPRINTDATE"] == System.DBNull.Value ? DateTime.MinValue : DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());
                }
                catch { p_objContent.m_dtmFirstPrintDate = DateTime.MinValue; }
                p_objContent.m_strCredibility = objSelectedRow["CREDIBILITY"].ToString();
                p_objContent.m_strRepresentor = objSelectedRow["REPRESENTOR"].ToString();
                try
                {
                    p_objContent.m_bytStatus = byte.Parse(objSelectedRow["STATUS"].ToString());
                }
                catch { p_objContent.m_bytStatus = byte.Parse("0"); }
                try
                {
                    p_objContent.m_dtmDeActivedDate = objSelectedRow["DEACTIVEDDATE"] == System.DBNull.Value ? DateTime.MinValue : DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                }
                catch { p_objContent.m_dtmDeActivedDate = DateTime.MinValue; }
                int intTemp = 0;
                if (int.TryParse(objSelectedRow["MARKSTATUS_INT"].ToString(), out intTemp))
                {
                    p_objContent.m_intMarkStatus = intTemp;
                }
                //获取签名集合
                if (objSelectedRow["SEQUENCE_INT"] != DBNull.Value)
                {
                    long lngS = 0;
                    if (long.TryParse(objSelectedRow["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign.m_lngGetSign(lngS, out p_objContent.objSignerArr);

                        //释放
                        objSign = null;
                    }
                }
                //}
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
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取一个专科病历的项目信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objMainContent"></param>
        /// <param name="p_objItemContentArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetItemRecord( string p_strTypeID, string p_strInPatientID, string p_strInPatientDate, DateTime p_dtmOpenDate, out clsInpatMedRec_Item[] p_objItemContentArr)
        {
            p_objItemContentArr = null;
            //参数判断
            if (p_strTypeID == null || p_strInPatientID == null || p_strInPatientDate == null)
                return (long)enmOperationResult.Parameter_Error;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr2 = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr2);
                objDPArr2[0].Value = p_strTypeID.Trim();
                objDPArr2[1].Value = p_strInPatientID.Trim();
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = p_dtmOpenDate;

                DataTable dtbValue2 = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetItemRecord, ref dtbValue2, objDPArr2);
                int intRowCount = dtbValue2.Rows.Count;
                if (lngRes <= 0 || intRowCount == 0)
                    return lngRes;

                p_objItemContentArr = new clsInpatMedRec_Item[dtbValue2.Rows.Count];
                DataRow objSelectedRow = null;
                for (int i = 0 ; i < intRowCount ; i++)
                {
                    p_objItemContentArr[i] = new clsInpatMedRec_Item();
                    objSelectedRow = dtbValue2.Rows[i];
                    p_objItemContentArr[i].m_strItemID = objSelectedRow["ITEMID"].ToString();
                    p_objItemContentArr[i].m_strItemContent = objSelectedRow["ITEMCONTENT"].ToString();
                    p_objItemContentArr[i].m_strItemContentXml = objSelectedRow["ITEMCONTENTXML"].ToString();
                    p_objItemContentArr[i].m_strItemName = objSelectedRow["ITEMNAME"].ToString();
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
            //返回
            return lngRes;

        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord(
            clsInpatMedRecContent p_objContent)
        {
            //检查参数                              
            if (p_objContent == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(6, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = (p_objContent.m_strTypeID == null ? "" : p_objContent.m_strTypeID.Trim());
                objDPArr[3].Value = (p_objContent.m_strInPatientID == null ? "" : p_objContent.m_strInPatientID.Trim());
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objContent.m_dtmInPatientDate;
                objDPArr[5].DbType = DbType.DateTime;
                objDPArr[5].Value = p_objContent.m_dtmOpenDate;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteMainRecord, ref lngEff, objDPArr);
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
            //返回
            return lngRes;
        }


        #region  打印

        /// <summary>
        /// 获取打印内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_objPrintContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrintInfo( string p_strTypeID, ref clsPrintInfo_InpatMedRec p_objPrintContent)
        {
            //参数检查                     

            p_objPrintContent.m_objContent = null;
            p_objPrintContent.m_dtmFirstPrintDate = DateTime.MinValue;
            p_objPrintContent.m_blnIsFirstPrint = false;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strFirstPrintDate;
                //眼科护理记录做成专科表单，但是需要写多次记录，用opendate区分--wf20080202
                if (p_strTypeID == "frmIMR_EyeTakecare")
                    lngRes = m_lngGetFirstPrintDate(p_strTypeID, p_objPrintContent.m_strInPatientID, p_objPrintContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objPrintContent.m_dtmOpenDate,out strFirstPrintDate);
                else
                    lngRes = m_lngGetFirstPrintDate(p_strTypeID, p_objPrintContent.m_strInPatientID, p_objPrintContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out strFirstPrintDate);

                if (lngRes <= 0)
                    return lngRes;

                //判断strFirstPrintDate是否为null或者不为null但为空值
                if (strFirstPrintDate == null || strFirstPrintDate == "")
                {//如果是
                    p_objPrintContent.m_dtmFirstPrintDate = DateTime.Now;
                    p_objPrintContent.m_blnIsFirstPrint = true;
                }
                else
                {//如果不是
                    try
                    {
                        p_objPrintContent.m_dtmFirstPrintDate = DateTime.Parse(strFirstPrintDate);
                    }
                    catch { p_objPrintContent.m_dtmFirstPrintDate = DateTime.Now; }
                    p_objPrintContent.m_blnIsFirstPrint = false;
                }
                clsInpatMedRecContent objContent = null;
                //眼科护理记录做成专科表单，但是需要写多次记录，用opendate区分--wf20080202
                if (p_strTypeID == "frmIMR_EyeTakecare")
                    lngRes = m_lngGetRecordContent(  p_strTypeID, p_objPrintContent.m_strInPatientID, p_objPrintContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objPrintContent.m_dtmOpenDate,out objContent);
                else
                    lngRes = m_lngGetRecordContent( p_strTypeID, p_objPrintContent.m_strInPatientID, p_objPrintContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);

                if (lngRes > 0 && objContent != null)
                {
                    p_objPrintContent.m_objContent = objContent;
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
            //返回
            return lngRes;

        }

        /// <summary>
        /// 获取数据库中首次打印时间
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        protected long m_lngGetFirstPrintDate(string p_strTypeID, string p_strInPatientID, string p_strInPatientDate, out string p_strFirstPrintDate)
        {
            p_strFirstPrintDate = null;

            string strGetFirstPrintDateSQL = @"select firstprintdate from inpatmedrec where typeid = ? and 　inpatientid　 = ? and inpatientdate = ? and status=0";;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strTypeID;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
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
            //返回
            return lngRes;

        }

        /// <summary>
        /// 获取数据库中首次打印时间,适用于专科表单但是又需要写多次记录--wf20080202
        /// </summary>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        protected long m_lngGetFirstPrintDate(string p_strTypeID, string p_strInPatientID, string p_strInPatientDate, DateTime p_dtmOpenDate,out string p_strFirstPrintDate)
        {
            p_strFirstPrintDate = null;
            //用opendate区分同一个专科表单同一次入院写的多次记录
            string strGetFirstPrintDateSQL =@"select firstprintdate from inpatmedrec where typeid = ? and 　inpatientid　 = ? and inpatientdate = ? and opendate = ? and status=0";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strTypeID;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_dtmOpenDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
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
            //返回
            return lngRes;

        }

        // 更新数据库中的首次打印时间。
        [AutoComplete]
        public long m_lngUpdateFirstPrintDate( string p_strTypeID, clsPrintInfo_InpatMedRec p_objPrintContent)
        {
            //检查参数                              
            if (p_strTypeID == null || p_strTypeID == "" || p_objPrintContent.m_strInPatientID == null
                || p_objPrintContent.m_strInPatientID == "" || p_objPrintContent == null)
                return (long)enmOperationResult.Parameter_Error;
            //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objPrintContent.m_dtmFirstPrintDate;
                objDPArr[1].Value = p_strTypeID.Trim();
                objDPArr[2].Value = p_objPrintContent.m_strInPatientID.Trim();
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objPrintContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objPrintContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

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
            //返回
            return lngRes;

        }

        #endregion

        /// <summary>
        /// 获取控件描述
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetType_ItemRecord( string p_strTypeID, out clsInpatMedRec_Type_Item[] p_objContentArr)
        {
            string strSql = "";
            long lngRes = 0;
            p_objContentArr = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();

                p_objContentArr = null;

                //检查参数                              
                if (p_strTypeID == null)
                {
                    //				return (long)enmOperationResult.Parameter_Error;
                    strSql = @"select typeid, itemid, itemtype, itemname, itemtabindex from inpatmedrec_type_item order by typeid,itemid";
                    //执行查询，填充结果到DataTable
                    lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSql, ref dtbValue);
                }
                else
                {
                    strSql = @"select typeid, itemid, itemtype, itemname, itemtabindex from inpatmedrec_type_item where typeid = ?";
                    //					IDataParameter[] objDPArr = new Oracle.DataAccess.Client.OracleParameter[1];

                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    //objHRPServ.Dispose();

                    //					objDPArr[0]=new Oracle.DataAccess.Client.OracleParameter();
                    objDPArr[0].Value = p_strTypeID;
                    objHRPServ = new clsHRPTableService();
                    //执行查询，填充结果到DataTable
                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                }
                if (lngRes <= 0)
                    return lngRes;
                int intRowCount  = dtbValue.Rows.Count;
                DataRow objSelectedRow = null;
                if (intRowCount > 0)
                {
                    p_objContentArr = new clsInpatMedRec_Type_Item[intRowCount];
                    for (int i = 0 ; i < intRowCount ; i++)
                    {
                        objSelectedRow = dtbValue.Rows[i];
                        p_objContentArr[i] = new clsInpatMedRec_Type_Item();
                        p_objContentArr[i].m_strTypeID = objSelectedRow["TYPEID"].ToString().Trim();
                        p_objContentArr[i].m_strItemID = objSelectedRow["ITEMID"].ToString().Trim();
                        p_objContentArr[i].m_strItemName = objSelectedRow["ITEMNAME"].ToString().Trim();
                        p_objContentArr[i].m_strItemType = objSelectedRow["ITEMTYPE"].ToString().Trim();
                    }
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTypeIDArr"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public clsInpatMedRec_Type_Item[] m_objGetType_ItemRecordByTypeIDArr( string p_strTypeIDArr)
        {
            string strSql = "";
            long lngRes = 0;
            clsInpatMedRec_Type_Item[] objContentArr = null;
            if (p_strTypeIDArr == null || p_strTypeIDArr == "")
                return null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();

                strSql = @"select typeid, itemid, itemtype, itemname, itemtabindex from inpatmedrec_type_item where typeid in (" + p_strTypeIDArr + ")";

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.DoGetDataTable(strSql, ref dtbValue);

                if (lngRes <= 0)
                    return null;

                int intRowCount = dtbValue.Rows.Count;
                DataRow objSelectedRow = null;
                if (intRowCount > 0)
                {
                    objContentArr = new clsInpatMedRec_Type_Item[intRowCount];
                    for (int i = 0 ; i < intRowCount ; i++)
                    {
                        objSelectedRow = dtbValue.Rows[i];
                        objContentArr[i] = new clsInpatMedRec_Type_Item();
                        objContentArr[i].m_strTypeID = objSelectedRow["TYPEID"].ToString().Trim();
                        objContentArr[i].m_strItemID = objSelectedRow["ITEMID"].ToString().Trim();
                        objContentArr[i].m_strItemName = objSelectedRow["ITEMNAME"].ToString().Trim();
                        objContentArr[i].m_strItemType = objSelectedRow["ITEMTYPE"].ToString().Trim();
                    }
                }

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return objContentArr;
        }

        /// <summary>
        /// 提取一个项目的内容
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_strItemID"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOneItemValue( string p_strTypeID, string p_strInPatientID, string p_strInPatientDate, string p_strItemID, out clsInpatMedRec_Item p_objContent)
        {
            p_objContent = null;
            //检查参数                              
            if (p_strTypeID == null || p_strInPatientID == null || p_strInPatientDate == null || p_strItemID == null)
                return (long)enmOperationResult.Parameter_Error;

            string strSql = @"select a.itemid,a.itemcontent,a.itemcontentxml
				from inpatmedrec_item a inner join inpatmedrec b on 　a.typeid　 = 　b.typeid　 and 　a.inpatientid　 = 　b.inpatientid　 and a.inpatientdate = b.inpatientdate and a.opendate = b.opendate 
				where 　a.typeid = ? and a.inpatientid = ? and a.inpatientdate = ? and a.itemid = ? and b.status = 0";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strTypeID.Trim();
                objDPArr[1].Value = p_strInPatientID.Trim();
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].Value = p_strItemID.Trim();

                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                int intRowCount = dtbValue.Rows.Count;
                DataRow objSelectedRow = null;
                if (intRowCount == 1)
                {
                    objSelectedRow = dtbValue.Rows[0];
                    p_objContent = new clsInpatMedRec_Item();
                    p_objContent.m_strItemID = objSelectedRow["ITEMID"].ToString();
                    p_objContent.m_strItemContent = objSelectedRow["ITEMCONTENT"].ToString();
                    p_objContent.m_strItemContentXml = objSelectedRow["ITEMCONTENTXML"].ToString();
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
            //返回
            return lngRes;

        }

        /// <summary>
        /// 获取病人所有可用主表信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objContentArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientAllMainRecord( string p_strInPatientID, string p_strInPatientDate, out clsInpatMedRecContent[] p_objContentArr)
        {
            p_objContentArr = null;
            //检查参数                              
            if (p_strInPatientID == null || p_strInPatientDate == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strSql = @"select a.typeid,
       a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.firstprintdate,
       a.credibility,
       a.representor,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.sequence_int,
       a.markstatus_int,
       a.recorddate,b.typename from inpatmedrec a inner join inpatmedrec_type b
on a.typeid = b.typeid
where inpatientid = ? and inpatientdate= ? and status=0";
            IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[4];
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID.Trim();
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

            DataTable dtbValue = new DataTable();
            //执行查询，填充结果到DataTable

            long lngRes = 0;

            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            int intRowCount = dtbValue.Rows.Count;
            DataRow objSelectedRow = null;
            if (lngRes <= 0 || intRowCount < 1)
                return lngRes;

            p_objContentArr = new clsInpatMedRecContent[intRowCount];
            for (int i = 0 ; i < intRowCount ; i++)
            {
                objSelectedRow = dtbValue.Rows[i];
                p_objContentArr[i] = new clsInpatMedRecContent();
                p_objContentArr[i].m_strTypeID = objSelectedRow["TYPEID"].ToString();
                p_objContentArr[i].m_strTypeName = objSelectedRow["TYPENAME"].ToString();
                p_objContentArr[i].m_strInPatientID = objSelectedRow["INPATIENTID"].ToString();
                try
                {
                    p_objContentArr[i].m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                }
                catch { p_objContentArr[i].m_dtmInPatientDate = DateTime.MinValue; }
                try
                {
                    p_objContentArr[i].m_dtmOpenDate = DateTime.Parse(objSelectedRow["OPENDATE"].ToString());
                }
                catch { p_objContentArr[i].m_dtmOpenDate = DateTime.Now; }
                try
                {
                    p_objContentArr[i].m_dtmCreateDate = DateTime.Parse(objSelectedRow["CREATEDATE"].ToString());
                }
                catch { p_objContentArr[i].m_dtmCreateDate = DateTime.Now; }
                p_objContentArr[i].m_strCreateUserID = objSelectedRow["CREATEUSERID"].ToString().TrimEnd();
                try
                {
                    p_objContentArr[i].m_bytIfConfirm = int.Parse(objSelectedRow["IFCONFIRM"].ToString());
                }
                catch { p_objContentArr[i].m_bytIfConfirm = 0; }
                try
                {
                    p_objContentArr[i].m_dtmFirstPrintDate = objSelectedRow["FIRSTPRINTDATE"] == System.DBNull.Value ? DateTime.MinValue : DateTime.Parse(dtbValue.Rows[i]["FirstPrintDate"].ToString());
                }
                catch { p_objContentArr[i].m_dtmFirstPrintDate = DateTime.MinValue; }
                p_objContentArr[i].m_strCredibility = objSelectedRow["CREDIBILITY"].ToString();
                p_objContentArr[i].m_strRepresentor = objSelectedRow["REPRESENTOR"].ToString();
                try
                {
                    p_objContentArr[i].m_bytStatus = int.Parse(objSelectedRow["STATUS"].ToString());
                }
                catch { p_objContentArr[i].m_bytStatus = 0; }

            }
            return lngRes;
        }

        /// <summary>
        /// 获取窗体名称
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTypeName( ref clsInpatMedRec_Type p_objContent)
        {
            //检查参数                              
            if (p_objContent == null || p_objContent.m_strTypeID == null)
                return (long)enmOperationResult.Parameter_Error;

            string strSql = @"select typeid, typename from inpatmedrec_type where typeid = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_objContent.m_strTypeID;

                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                if (dtbValue.Rows.Count > 0 && dtbValue.Rows.Count == 1)
                    p_objContent.m_strTypeName = dtbValue.Rows[0]["TYPENAME"].ToString().Trim();
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
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取删除记录
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strTypeID"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_objContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeactiveRecInfo(
            string p_strTypeID, string p_strInPatientID, string p_strOpenDate,
            out clsInpatMedRecContent p_objContent)
        {
            //参数判断
            p_objContent = null;
            if (p_strTypeID == null || p_strInPatientID == null)
                return -1;
            long lngRes = 0;
            try
            {

                #region 主表信息
                lngRes = m_lngGetMainRecContent(p_strTypeID, p_strInPatientID, p_strOpenDate, out p_objContent, true);
                if (lngRes <= 0)
                    return lngRes;
                #endregion

                #region 项目信息
                lngRes = m_lngGetItemRecord(  p_strTypeID, p_strInPatientID, p_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), p_objContent.m_dtmOpenDate, out p_objContent.m_objItemContents);
                if (lngRes <= 0)
                    return lngRes;
                #endregion

                #region 图片信息
                lngRes = m_lngGetPicContent(p_strTypeID, p_strInPatientID, p_objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd hh:mm:ss"), ref  p_objContent);
                #endregion

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取所有窗体
        /// </summary>
        /// <param name="p_objTypeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllFormID(out clsInpatMedRec_Type[] p_objTypeArr)
        {
            p_objTypeArr = null;
            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(c_strGetAllformID, ref dtbValue);
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes <= 0 || intRowCount <= 0)
                    return lngRes;
                
                DataRow objSelectedRow = null;
                p_objTypeArr = new clsInpatMedRec_Type[intRowCount];
                for (int i = 0 ; i < intRowCount ; i++)
                {
                    objSelectedRow = dtbValue.Rows[i];
                    p_objTypeArr[i] = new clsInpatMedRec_Type();
                    p_objTypeArr[i].m_strTypeID = objSelectedRow["TYPEID"].ToString().Trim();
                    p_objTypeArr[i].m_strTypeName = objSelectedRow["TYPENAME"].ToString().Trim();
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

            }			//返回
            return lngRes;
        }/// <summary>
        /// 获取所有窗体
        /// </summary>
        /// <param name="p_objTypeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public clsInpatMedRec_Type[] m_objGetAllFormID()
        {
            clsInpatMedRec_Type[] p_objTypeArr = null;
            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(c_strGetAllformID, ref dtbValue);
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes <= 0 || intRowCount <= 0)
                    return null;
                DataRow objSelectedRow = null;
                p_objTypeArr = new clsInpatMedRec_Type[intRowCount];
                for (int i = 0 ; i < intRowCount ; i++)
                {
                    objSelectedRow = dtbValue.Rows[i];
                    p_objTypeArr[i] = new clsInpatMedRec_Type();
                    p_objTypeArr[i].m_strTypeID = objSelectedRow["TYPEID"].ToString().Trim();
                    p_objTypeArr[i].m_strTypeName = objSelectedRow["TYPENAME"].ToString().Trim();
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

            }			//返回
            return p_objTypeArr;
        }
        /// <summary>
        /// 获取相应科室可使用的电子病历
        /// </summary>
        /// <param name="p_strDeptIDArr"></param>
        /// <param name="p_objTypeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFormByChargeDept(string[] p_strDeptIDArr, out clsInpatMedRec_Type[] p_objTypeArr)
        {
            p_objTypeArr = null;
            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_strDeptIDArr == null || p_strDeptIDArr.Length == 0) return -1;
                string strSql = @"select distinct b.typeid, b.typename from inpatmedrec_type_dept a inner join
      inpatmedrec_type b on a.typeid = b.typeid where [DEPTIDARR]";
                string str = " a.deptid = '" + p_strDeptIDArr[0] + "'";
                for (int i = 1 ; i < p_strDeptIDArr.Length ; i++)
                {
                    str += " or a.deptid = '" + p_strDeptIDArr[i] + "'";
                }
                strSql = strSql.Replace("[DEPTIDARR]", str);

                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSql, ref dtbValue);
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes <= 0 || intRowCount <= 0)
                    return lngRes;
                DataRow objSelectedRow = null;
                p_objTypeArr = new clsInpatMedRec_Type[intRowCount];
                for (int i = 0 ; i < intRowCount ; i++)
                {
                    objSelectedRow = dtbValue.Rows[i];
                    p_objTypeArr[i] = new clsInpatMedRec_Type();
                    p_objTypeArr[i].m_strTypeID = objSelectedRow["TYPEID"].ToString().Trim();
                    p_objTypeArr[i].m_strTypeName = objSelectedRow["TYPENAME"].ToString().Trim();
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

            }			//返回
            return lngRes;
        }
        /// <summary>
        /// 获取相应科室可使用的电子病历
        /// </summary>
        /// <param name="p_strDeptIDArr"></param>
        /// <param name="p_objTypeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public clsInpatMedRec_Type[] m_objGetFormByChargeDept(string[] p_strDeptIDArr)
        {
            clsInpatMedRec_Type[] p_objTypeArr = null;
            long lngRes = 1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_strDeptIDArr == null || p_strDeptIDArr.Length == 0) return null;
                string strSql = @"select distinct b.typeid, b.typename from inpatmedrec_type_dept a inner join
      inpatmedrec_type b on a.typeid = b.typeid where [DEPTIDARR]";
                string str = " a.deptid = '" + p_strDeptIDArr[0] + "'";
                for (int i = 1 ; i < p_strDeptIDArr.Length ; i++)
                {
                    str += " or a.deptid = '" + p_strDeptIDArr[i] + "'";
                }
                strSql = strSql.Replace("[DEPTIDARR]", str);

                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithoutParameters(strSql, ref dtbValue);
                int intRowCount = dtbValue.Rows.Count;
                if (lngRes <= 0 || intRowCount <= 0)
                    return null;
                DataRow objSelectedRow = null;
                p_objTypeArr = new clsInpatMedRec_Type[intRowCount];
                for (int i = 0 ; i < intRowCount ; i++)
                {
                    objSelectedRow = dtbValue.Rows[i];
                    p_objTypeArr[i] = new clsInpatMedRec_Type();
                    p_objTypeArr[i].m_strTypeID = objSelectedRow["TYPEID"].ToString().Trim();
                    p_objTypeArr[i].m_strTypeName = objSelectedRow["TYPENAME"].ToString().Trim();
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

            }			//返回
            return p_objTypeArr;
        }

        [AutoComplete]
        public long m_lngGetAllInactiveInfo(string p_strType,string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            p_objInactiveRecordInfoArr = null;
            if (string.IsNullOrEmpty(p_strInpatientId) || p_dtmInpatientDate == DateTime.MinValue || string.IsNullOrEmpty(p_strType)) return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSql = @"select t.createdate,
       t.opendate,
       t.deactiveddate,
       e2.lastname_vchr createdusername,
       e3.lastname_vchr deactiveusername
  from inpatmedrec t, t_bse_employee e2, t_bse_employee e3
 where t.createuserid = e2.empno_chr
   and t.deactivedoperatorid = e3.empno_chr
   and t.inpatientid = ?
   and t.inpatientdate = ?
   and t.typeid = ?
   and t.status = 1
 order by t.opendate desc";
                DataTable dtExist = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;
                objDPArr[2].Value = p_strType;

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtExist, objDPArr);
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

    }
}
