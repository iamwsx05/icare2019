using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using System.Security;
using weCare.Core.Entity;


namespace com.digitalwave.emr.EMR_SynchronousCaseServ
{
    /// <summary>
    /// 病案同步2009新版中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_SynchronousCaseServ_2009_Modify : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 新增病案同步记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strContentXML">内容</param>
        /// <param name="intType">类型0未同步1已同步</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewCaseRecord(string p_strRegisterID, string p_strContentXML, int intType)
        {
            if (string.IsNullOrEmpty(p_strRegisterID) || string.IsNullOrEmpty(p_strContentXML))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                string strSQL = @"insert into t_emr_synchronouscaserecord
  (registerid_chr, caserecord, status)
values
  (?, ?, ?)";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strRegisterID;
                objDPArr[1].Value = p_strContentXML;
                objDPArr[2].Value = intType;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 修改病案同步记录
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strContentXML">内容</param>
        /// <param name="intType">类型0未同步1已同步</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyCaseRecord(string p_strRegisterID, string p_strContentXML, int intType)
        {
            if (string.IsNullOrEmpty(p_strRegisterID) || string.IsNullOrEmpty(p_strContentXML))
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_emr_synchronouscaserecord
   set caserecord = ?, status = ?
 where registerid_chr = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strContentXML;
                objDPArr[1].Value = intType;
                objDPArr[2].Value = p_strRegisterID;

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 新增病案同步记录
        /// </summary>
        /// <param name="p_objVOArr">病案同步记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewCaseRecordArr(clsEMR_SynchronousCase2009_VO[] p_objVOArr)
        {
            if (p_objVOArr == null || p_objVOArr.Length == 0)
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                string strSQL = @"insert into t_emr_synchronouscaserecord
  (registerid_chr, caserecord, status)
values
  (?, ?, ?)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.Int32 };
                object[][] objValues = new object[3][];
                for (int iV = 0; iV < objValues.Length; iV++)
                {
                    objValues[iV] = new object[p_objVOArr.Length];
                }
                for (int i = 0; i < p_objVOArr.Length; i++)
                {
                    objValues[0][i] = p_objVOArr[i].m_strRegisterID;
                    objValues[1][i] = p_objVOArr[i].m_strContentXML;
                    objValues[2][i] = p_objVOArr[i].m_intType;
                }
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 修改病案同步记录
        /// </summary>
        /// <param name="p_objVOArr">病案同步记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyCaseRecordArr(clsEMR_SynchronousCase2009_VO[] p_objVOArr)
        {
            if (p_objVOArr == null || p_objVOArr.Length == 0)
            {
                return -1;
            }
            long lngRes = 0;
            try
            {
                string strSQL = @"update t_emr_synchronouscaserecord
   set caserecord = ?, status = ?
 where registerid_chr = ?";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                DbType[] dbTypes = new DbType[] { DbType.String, DbType.Int32, DbType.String };
                object[][] objValues = new object[3][];
                for (int iV = 0; iV < objValues.Length; iV++)
                {
                    objValues[iV] = new object[p_objVOArr.Length];
                }
                for (int i = 0; i < p_objVOArr.Length; i++)
                {
                    objValues[0][i] = p_objVOArr[i].m_strContentXML;
                    objValues[1][i] = p_objVOArr[i].m_intType;
                    objValues[2][i] = p_objVOArr[i].m_strRegisterID;
                }
                lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 同步内容到HIS_BA1
        /// </summary>
        /// <param name="p_dtbBA1">HIS_BA1内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToHIS_BA1(DataTable p_dtbBA1)
        {                                 
            if (p_dtbBA1 == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {

               string strSQL = @"insert into his_ba1
(
    fifinput,fprn,ftimes,ficdversion,fzyid,fage,fname,fsexbh,fsex,
	fbirthday,fbirthplace,fidcard,fcountrybh,fcountry,fnationalitybh,fnationality,fjob,fstatusbh,fstatus,
	fdwname,fdwaddr,fdwtele,fdwpost,fhkaddr,fhkpost,flxname,frelate,flxaddr,flxtele,
    fascard1,frydate,frytime,frytykh,frydept,frybs,fcydate,fcytime,fcytykh,fcydept,   
    fcybs,fdays,fmzzdbh,fmzzd,fmzdoctbh,fmzdoct,fphzd,fgmyw,frycyaccobh,frycyacco,
    flcblaccobh,flcblacco,fqjtimes,fqjsuctimes,fkzrbh,fkzr,fzrdoctbh,fzrdoctor,fzzdoctbh,fzzdoct,
    fzydoctbh,fzydoct,fjxdoctbh,fjxdoct,fsxdoctbh,fsxdoct,fbmybh,fbmy,fzlrbh,fzlr,
    fqualitybh,fquality,fzkdoctbh,fzkdoct,fzknursebh,fzknurse,fzkrq,fsum1,fxyf,fzyf,
    fzchyf,fzcyf,fqtf,fbodybh,fbody,fbloodbh,fblood,frhbh,frh,fbabynum,
    ftwill,fzktykh,fzkdept,fzkdate,fzktime,fsrybh,fsry,fworkrq,fjbfxbh,fjbfx,
    ffhgdbh,ffhgd,fsourcebh,fsource,fifss,fiffyk,fyngr,fextend1,fextend2,fextend3,
    fextend4,fextend5,fextend6,fextend7,fextend8,fextend9,fextend10,fextend11,fextend12,fextend13,
    fextend14,fextend15,fnative,fcurraddr,fcurrtele,fcurrpost,fjobbh,fcstz,frytz,frytjbh,
    frytj,fycljbh,fyclj,fphzdbh,fphzdnum,fifgmywbh,fifgmyw,fnursebh,fnurse,flyfsbh,
    flyfs,fyzouthostital,fsqouthostital,fisagainrybh,fisagainry,fisagainrymd,fryqhmdays,fryqhmhours,fryqhmmins,fryqhmcounts,
    fryhmdays,fryhmhours,fryhmmins,fryhmcounts,ffbbhnew,ffbnew,fzfje,fzhfwlylf,fzhfwlczf,fzhfwlhlf,
    fzhfwlqtf,fzdlblf,fzdlsssf,fzdlyxf,fzdllcf,fzllffssf,fzllfwlzwlf,fzllfssf,fzllfmzf,fzllfsszlf,
    fkflkff,fzylzf,fxylgjf,fxylxf,fxylbqbf,fxylqdbf,fxylyxyzf,fxylxbyzf,fhclcjf,fhclzlf,
    fhclssf,fzhfwlylf01,fzhfwlylf02,fzylzdf,fzylzlf,fzylzlf01,fzylzlf02,fzylzlf03,fzylzlf04,fzylzlf05,
    fzylzlf06,fzylqtf,fzylqtf01,fzylqtf02,fzcljgzjf)
values
(
    ?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?,?,?,?,?,?,
    ?,?,?,?,?
)";

                DataRow drValue = p_dtbBA1.Rows[0];
                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(194, out objDPArr);
                int intIndex = 0;
                #region 参数赋值
                objDPArr[intIndex++].Value = Convert.ToBoolean(drValue["FIFINPUT"]);
                objDPArr[intIndex++].Value = drValue["FPRN"].ToString();
                objDPArr[intIndex++].Value = drValue["FTIMES"].ToString();
                objDPArr[intIndex++].Value = drValue["FICDVERSION"].ToString();
                objDPArr[intIndex++].Value = drValue["FZYID"].ToString();
                objDPArr[intIndex++].Value = drValue["FAGE"].ToString();
                objDPArr[intIndex++].Value = drValue["FNAME"].ToString();
                objDPArr[intIndex++].Value = drValue["FSEXBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FSEX"].ToString();
                /***************************  10 ******************************/
                objDPArr[intIndex].DbType = DbType.DateTime;
                objDPArr[intIndex].Value = Convert.ToDateTime(drValue["FBIRTHDAY"]);
                intIndex++;
                objDPArr[intIndex++].Value = drValue["FBIRTHPLACE"].ToString();
                objDPArr[intIndex++].Value = drValue["FIDCARD"].ToString();
                objDPArr[intIndex++].Value = drValue["FCOUNTRYBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FCOUNTRY"].ToString();
                objDPArr[intIndex++].Value = drValue["FNATIONALITYBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FNATIONALITY"].ToString();
                objDPArr[intIndex++].Value = drValue["FJOB"].ToString();
                objDPArr[intIndex++].Value = drValue["FSTATUSBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FSTATUS"].ToString();
                /***************************  20 ******************************/
                objDPArr[intIndex++].Value = drValue["FDWNAME"].ToString();
                objDPArr[intIndex++].Value = drValue["FDWADDR"].ToString();
                objDPArr[intIndex++].Value = drValue["FDWTELE"].ToString();
                objDPArr[intIndex++].Value = drValue["FDWPOST"].ToString();
                objDPArr[intIndex++].Value = drValue["FHKADDR"].ToString();
                objDPArr[intIndex++].Value = drValue["FHKPOST"].ToString();
                objDPArr[intIndex++].Value = drValue["FLXNAME"].ToString();
                objDPArr[intIndex++].Value = drValue["FRELATE"].ToString();
                objDPArr[intIndex++].Value = drValue["FLXADDR"].ToString();
                objDPArr[intIndex++].Value = drValue["FLXTELE"].ToString();
                /***************************  30 ******************************/
                objDPArr[intIndex++].Value = drValue["FASCARD1"].ToString();
                objDPArr[intIndex++].Value = drValue["FRYDATE"].ToString();
                objDPArr[intIndex++].Value = drValue["FRYTIME"].ToString();
                objDPArr[intIndex++].Value = drValue["FRYTYKH"].ToString();
                objDPArr[intIndex++].Value = drValue["FRYDEPT"].ToString();
                objDPArr[intIndex++].Value = drValue["FRYBS"].ToString();
                objDPArr[intIndex++].Value = drValue["FCYDATE"].ToString();
                objDPArr[intIndex++].Value = drValue["FCYTIME"].ToString();
                objDPArr[intIndex++].Value = drValue["FCYTYKH"].ToString();
                objDPArr[intIndex++].Value = drValue["FCYDEPT"].ToString();
                /***************************  40 ******************************/
                objDPArr[intIndex++].Value = drValue["FCYBS"].ToString();
                objDPArr[intIndex++].Value = drValue["FDAYS"].ToString();
                objDPArr[intIndex++].Value = drValue["FMZZDBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FMZZD"].ToString();
                objDPArr[intIndex++].Value = drValue["FMZDOCTBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FMZDOCT"].ToString();
                objDPArr[intIndex++].Value = drValue["FPHZD"].ToString();
                objDPArr[intIndex++].Value = drValue["FGMYW"].ToString();
                objDPArr[intIndex++].Value = drValue["FRYCYACCOBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FRYCYACCO"].ToString();
                /***************************  50 ******************************/
                objDPArr[intIndex++].Value = drValue["FLCBLACCOBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FLCBLACCO"].ToString();
                int IntResult = 0;
                if (int.TryParse(drValue["FQJTIMES"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = drValue["FQJTIMES"].ToString();
                else
                    objDPArr[intIndex++].Value = 0;
                if (int.TryParse(drValue["FQJSUCTIMES"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = drValue["FQJSUCTIMES"].ToString();
                else
                    objDPArr[intIndex++].Value = 0;
                objDPArr[intIndex++].Value = drValue["FKZRBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FKZR"].ToString();
                objDPArr[intIndex++].Value = drValue["FZRDOCTBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FZRDOCTOR"].ToString();
                objDPArr[intIndex++].Value = drValue["FZZDOCTBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FZZDOCT"].ToString();
                /***************************  60 ******************************/
                objDPArr[intIndex++].Value = drValue["FZYDOCTBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FZYDOCT"].ToString();
                objDPArr[intIndex++].Value = drValue["FJXDOCTBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FJXDOCT"].ToString();
                objDPArr[intIndex++].Value = drValue["FSXDOCTBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FSXDOCT"].ToString();
                objDPArr[intIndex++].Value = drValue["FBMYBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FBMY"].ToString();
                objDPArr[intIndex++].Value = drValue["FZLRBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FZLR"].ToString();
                /***************************  70 ******************************/
                objDPArr[intIndex++].Value = drValue["FQUALITYBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FQUALITY"].ToString();
                objDPArr[intIndex++].Value = drValue["FZKDOCTBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FZKDOCT"].ToString();
                objDPArr[intIndex++].Value = drValue["FZKNURSEBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FZKNURSE"].ToString();
                objDPArr[intIndex].DbType = DbType.DateTime;
                objDPArr[intIndex].Value = Convert.ToDateTime(drValue["FZKRQ"]);
                intIndex++;
                double dbResult = 0.0;
                if (double.TryParse(drValue["FSUM1"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (double.TryParse(drValue["FXYF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (double.TryParse(drValue["FZYF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                /***************************  80 ******************************/
                if (double.TryParse(drValue["FZCHYF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (double.TryParse(drValue["FZCYF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (double.TryParse(drValue["FQTF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                objDPArr[intIndex++].Value = drValue["FBODYBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FBODY"].ToString();
                objDPArr[intIndex++].Value = drValue["FBLOODBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FBLOOD"].ToString();
                objDPArr[intIndex++].Value = drValue["FRHBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FRH"].ToString();
                objDPArr[intIndex++].Value = drValue["FBABYNUM"].ToString();
                /***************************  90 ******************************/
                if (drValue["FTWILL"] != DBNull.Value)
                {
                    objDPArr[intIndex++].Value = Convert.ToBoolean(drValue["FTWILL"]);
                }
                else
                {
                    objDPArr[intIndex++].Value = DBNull.Value;
                }
                objDPArr[intIndex++].Value = drValue["FZKTYKH"].ToString();
                objDPArr[intIndex++].Value = drValue["FZKDEPT"].ToString();
                if (drValue["FZKDATE"] != DBNull.Value)
                {
                    objDPArr[intIndex].DbType = DbType.DateTime;
                    objDPArr[intIndex].Value = Convert.ToDateTime(drValue["FZKDATE"]);
                    intIndex++;
                }
                else
                {
                    objDPArr[intIndex++].Value = DBNull.Value;
                }
                objDPArr[intIndex++].Value = drValue["FZKTIME"].ToString();
                objDPArr[intIndex++].Value = drValue["FSRYBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FSRY"].ToString();
                objDPArr[intIndex].DbType = DbType.DateTime;
                objDPArr[intIndex].Value = DateTime.Now;
                intIndex++;
                objDPArr[intIndex++].Value = drValue["FJBFXBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FJBFX"].ToString();
                /***************************  100 ******************************/
                objDPArr[intIndex++].Value = drValue["FFHGDBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FFHGD"].ToString();
                objDPArr[intIndex++].Value = drValue["FSOURCEBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FSOURCE"].ToString();
                objDPArr[intIndex++].Value = Convert.ToBoolean(drValue["FIFSS"]);
                objDPArr[intIndex++].Value = Convert.ToBoolean(drValue["FIFFYK"]);
                //objDPArr[intIndex++].Value = drValue["FBFZ"].ToString();
                if (int.TryParse(drValue["FYNGR"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                objDPArr[intIndex++].Value = drValue["FEXTEND1"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND2"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND3"].ToString();
                /***************************  110 ******************************/
                objDPArr[intIndex++].Value = drValue["FEXTEND4"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND5"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND6"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND7"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND8"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND9"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND10"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND11"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND12"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND13"].ToString();
                /***************************  120 ******************************/
                objDPArr[intIndex++].Value = drValue["FEXTEND14"].ToString();
                objDPArr[intIndex++].Value = drValue["FEXTEND15"].ToString();
                objDPArr[intIndex++].Value = drValue["FNATIVE"].ToString();
                objDPArr[intIndex++].Value = drValue["FCURRADDR"].ToString();
                objDPArr[intIndex++].Value = drValue["FCURRTELE"].ToString();
                objDPArr[intIndex++].Value = drValue["FCURRPOST"].ToString();
                objDPArr[intIndex++].Value = drValue["FJOBBH"].ToString();
                if (double.TryParse(drValue["FCSTZ"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (double.TryParse(drValue["FRYTZ"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                objDPArr[intIndex++].Value = drValue["FRYTJBH"].ToString();
                /***************************  130 ******************************/
                objDPArr[intIndex++].Value = drValue["FRYTJ"].ToString();
                objDPArr[intIndex++].Value = drValue["FYCLJBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FYCLJ"].ToString();
                objDPArr[intIndex++].Value = drValue["FPHZDBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FPHZDNUM"].ToString();
                objDPArr[intIndex++].Value = drValue["FIFGMYWBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FIFGMYW"].ToString();
                objDPArr[intIndex++].Value = drValue["FNURSEBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FNURSE"].ToString();
                objDPArr[intIndex++].Value = drValue["FLYFSBH"].ToString();
                /***************************  140 ******************************/
                objDPArr[intIndex++].Value = drValue["FLYFS"].ToString();
                objDPArr[intIndex++].Value = drValue["FYZOUTHOSTITAL"].ToString();
                objDPArr[intIndex++].Value = drValue["FSQOUTHOSTITAL"].ToString();
                objDPArr[intIndex++].Value = drValue["FISAGAINRYBH"].ToString();
                objDPArr[intIndex++].Value = drValue["FISAGAINRY"].ToString();
                objDPArr[intIndex++].Value = drValue["FISAGAINRYMD"].ToString();
                if (int.TryParse(drValue["FRYQHMDAYS"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (int.TryParse(drValue["FRYQHMHOURS"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (int.TryParse(drValue["FRYQHMMINS"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (int.TryParse(drValue["FRYQHMCOUNTS"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                /***************************  150 ******************************/
                if (int.TryParse(drValue["FRYHMDAYS"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (int.TryParse(drValue["FRYHMHOURS"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (int.TryParse(drValue["FRYHMMINS"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                if (int.TryParse(drValue["FRYHMCOUNTS"].ToString(), out IntResult))
                    objDPArr[intIndex++].Value = IntResult;
                else
                    objDPArr[intIndex++].Value = DBNull.Value;
                objDPArr[intIndex++].Value = drValue["FFBBHNEW"].ToString();
                objDPArr[intIndex++].Value = drValue["FFBNEW"].ToString();
                if (double.TryParse(drValue["FZFJE"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZHFWLYLF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZHFWLCZF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZHFWLHLF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                /***************************  160 ******************************/
                if (double.TryParse(drValue["FZHFWLQTF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZDLBLF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZDLSSSF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZDLYXF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZDLLCF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZLLFFSSF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZLLFWLZWLF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZLLFSSF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZLLFMZF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZLLFSSZLF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                /***************************  170 ******************************/
                if (double.TryParse(drValue["FKFLKFF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FZYLZF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FXYLGJF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FXYLXF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FXYLBQBF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FXYLQDBF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FXYLYXYZF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FXYLXBYZF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FHCLCJF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                if (double.TryParse(drValue["FHCLZLF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                /***************************  180 ******************************/
                if (double.TryParse(drValue["FHCLSSF"].ToString(), out dbResult))
                    objDPArr[intIndex++].Value = dbResult;
                else
                    objDPArr[intIndex++].Value = 0.0;
                //中医类
                objDPArr[intIndex++].Value = DBNull.Value;// drValue["FZHFWLYLF01"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZHFWLYLF02"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZDF"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF01"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF02"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF03"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF04"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF05"].ToString();
                /***************************  190 ******************************/
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF06"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLQTF"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLQTF01"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLQTF02"].ToString();
                objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZCLJGZJF"].ToString();
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF02"].ToString();
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF03"].ToString();
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF04"].ToString();
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF05"].ToString();
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLZLF06"].ToString();
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLQTF"].ToString();               
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLQTF01"].ToString();
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZYLQTF02"].ToString();
                //objDPArr[intIndex++].Value = DBNull.Value;//drValue["FZCLJGZJF"].ToString();
               
                #endregion

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
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
        /// 同步内容到HIS_BA2
        /// </summary>
        /// <param name="p_dtbBA2">HIS_BA2内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToHIS_BA2(DataTable p_dtbBA2)
        {
            if (p_dtbBA2 == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into his_ba2
(
	fprn,
	ftimes,
	fzktykh,
	fzkdept,
	fzkdate,
	fzktime
)
values
(
	?, ?, ?, ?,	?, ?
)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);
                IDataParameter[] objDPArr = null;

                DataRow drValue = null;
                long lngEff = -1;
                for (int iRow = 0; iRow < p_dtbBA2.Rows.Count; iRow++)
                {
                    drValue = p_dtbBA2.Rows[iRow];
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = drValue["fprn"].ToString();
                    objDPArr[1].Value = drValue["ftimes"].ToString();
                    objDPArr[2].Value = drValue["fzktykh"].ToString();
                    objDPArr[3].Value = drValue["fzkdept"].ToString();
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = Convert.ToDateTime(drValue["fzkdate"]);
                    objDPArr[5].Value = drValue["fzktime"].ToString();

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
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
        /// 同步内容到HIS_BA3
        /// </summary>
        /// <param name="p_dtbBA3">HIS_BA3内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToHIS_BA3(DataTable p_dtbBA3)
        {
            if (p_dtbBA3 == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into his_ba3
(
	fprn,
	ftimes,
	fzdlx,
	ficdversion,
	ficdm,
	fjbname,
	frybqbh,
	frybq
)
values
(
	?,?,?,?,?,?,?,?
)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);
                IDataParameter[] objDPArr = null;

                DataRow drValue = null;
                long lngEff = -1;
                for (int iRow = 0; iRow < p_dtbBA3.Rows.Count; iRow++)
                {
                    drValue = p_dtbBA3.Rows[iRow];
                    objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                    objDPArr[0].Value = drValue["fprn"].ToString();
                    objDPArr[1].Value = drValue["ftimes"].ToString();
                    objDPArr[2].Value = drValue["fzdlx"].ToString();
                    objDPArr[3].Value = drValue["ficdversion"].ToString();
                    objDPArr[4].Value = drValue["ficdm"].ToString();
                    objDPArr[5].Value = drValue["fjbname"].ToString();
                    objDPArr[6].Value = drValue["frybqbh"].ToString();
                    objDPArr[7].Value = drValue["frybq"].ToString();

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
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
        /// 同步内容到HIS_BA4
        /// </summary>
        /// <param name="p_dtbBA4">HIS_BA4内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToHIS_BA4(DataTable p_dtbBA4)
        {
            if (p_dtbBA4 == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into his_ba4
(
	fprn,
	ftimes,
	fname,
	foptimes,
	fopcode,
	fop,
	fopdate,
	fqiekoubh,
	fqiekou,
	fyuhebh,
	fyuhe,
	fdocbh,
	fdocname,
	fmazuibh,
	fmazui,
	fiffsop,
	fopdoct1bh,
	fopdoct1,
	fopdoct2bh,
	fopdoct2,
	fmzdoctbh,
	fmzdoct,
    FZQSSBH,
    FZQSS,
    FSSJBBH,
    FSSJB,
    FOPKSNAME,
    FOPTYKH
    
)
values
(
	?,?,?,?,?,?,?,?,?,?,
	?,?,?,?,?,?,?,?,?,?,
	?,?,?,?,?,?,?,?
)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);
                IDataParameter[] objDPArr = null;

                DataRow drValue = null;
                long lngEff = -1;
                for (int iRow = 0; iRow < p_dtbBA4.Rows.Count; iRow++)
                {
                    drValue = p_dtbBA4.Rows[iRow];
                    objHRPServ.CreateDatabaseParameter(28, out objDPArr);
                    objDPArr[0].Value = drValue["fprn"].ToString();
                    objDPArr[1].Value = drValue["ftimes"].ToString();
                    objDPArr[2].Value = drValue["fname"].ToString();
                    objDPArr[3].Value = drValue["foptimes"].ToString();
                    objDPArr[4].Value = drValue["fopcode"].ToString();
                    objDPArr[5].Value = drValue["fop"].ToString();
                    objDPArr[6].DbType = DbType.DateTime;
                    objDPArr[6].Value = Convert.ToDateTime(drValue["fopdate"]);
                    objDPArr[7].Value = drValue["fqiekoubh"].ToString();
                    objDPArr[8].Value = drValue["fqiekou"].ToString();
                    objDPArr[9].Value = drValue["fyuhebh"].ToString();
                    objDPArr[10].Value = drValue["fyuhe"].ToString();
                    objDPArr[11].Value = drValue["fdocbh"].ToString();
                    objDPArr[12].Value = drValue["fdocname"].ToString();
                    objDPArr[13].Value = drValue["fmazuibh"].ToString();
                    objDPArr[14].Value = drValue["fmazui"].ToString();
                    objDPArr[15].Value = Convert.ToBoolean(drValue["fiffsop"]);
                    objDPArr[16].Value = drValue["fopdoct1bh"].ToString();
                    objDPArr[17].Value = drValue["fopdoct1"].ToString();
                    objDPArr[18].Value = drValue["fopdoct2bh"].ToString();
                    objDPArr[19].Value = drValue["fopdoct2"].ToString();
                    objDPArr[20].Value = drValue["fmzdoctbh"].ToString();
                    objDPArr[21].Value = drValue["fmzdoct"].ToString();
                    objDPArr[22].Value = drValue["FZQSSBH"].ToString();
                    objDPArr[23].Value = drValue["FZQSS"].ToString();
                    objDPArr[24].Value = drValue["FSSJBBH"].ToString();
                    objDPArr[25].Value = drValue["FSSJB"].ToString();
                    objDPArr[26].Value = drValue["FOPKSNAME"].ToString();
                    objDPArr[27].Value = drValue["FOPTYKH"].ToString();

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
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
        /// 同步内容到HIS_BA5
        /// </summary>
        /// <param name="p_dtbBA5">HIS_BA5内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToHIS_BA5(DataTable p_dtbBA5)
        {
            if (p_dtbBA5 == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into his_ba5
(
	fprn,
	ftimes,
	fbabynum,
	fname,
	fbabysexbh,
	fbabysex,
	ftz,
	fresultbh,
	fresult,
	fzgbh,
	fzg,
    fbabyqj,
	fbabysuc,
	fhxbh,
	fhx
)
values
(
	?,?,?,?,?,?,?,?,?,?,
	?,?,?,?,?
)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);
                IDataParameter[] objDPArr = null;

                DataRow drValue = null;
                long lngEff = -1;
                for (int iRow = 0; iRow < p_dtbBA5.Rows.Count; iRow++)
                {
                    drValue = p_dtbBA5.Rows[iRow];
                    objHRPServ.CreateDatabaseParameter(15, out objDPArr);
                    objDPArr[0].Value = drValue["fprn"].ToString();
                    objDPArr[1].Value = drValue["ftimes"].ToString();
                    objDPArr[2].Value = drValue["fbabynum"].ToString();
                    objDPArr[3].Value = drValue["fname"].ToString();
                    objDPArr[4].Value = drValue["fbabysexbh"].ToString();
                    objDPArr[5].Value = drValue["fbabysex"].ToString();
                    objDPArr[6].Value = drValue["ftz"].ToString();
                    objDPArr[7].Value = drValue["fresultbh"].ToString();
                    objDPArr[8].Value = drValue["fresult"].ToString();
                    objDPArr[9].Value = drValue["fzgbh"].ToString();
                    objDPArr[10].Value = drValue["fzg"].ToString();
                    //objDPArr[11].Value = drValue["fgricd10"].ToString();
                    //objDPArr[12].Value = drValue["fgrname"].ToString();
                    //objDPArr[13].Value = drValue["fbabygr"].ToString();
                    objDPArr[11].Value = drValue["fbabyqj"].ToString();
                    objDPArr[12].Value = drValue["fbabysuc"].ToString();
                    objDPArr[13].Value = drValue["fhxbh"].ToString();
                    objDPArr[14].Value = drValue["fhx"].ToString();

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
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
        /// 同步内容到HIS_BA6
        /// </summary>
        /// <param name="p_dtbBA6">HIS_BA6内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToHIS_BA6(DataTable p_dtbBA6)
        {
            if (p_dtbBA6 == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into his_ba6
(
	fprn,
	ftimes,
	fflfsbh,
	fflfs,
	fflcxbh,
	fflcx,
	fflzzbh,
	fflzz,
	fyjy,
	fycs,
	fyts,
	fyrq1,
	fyrq2,
	fqjy,
	fqcs,
	fqts,
	fqrq1,
	fqrq2,
	fzname,
	fzjy,
	fzcs,
	fzts,
	fzrq1,
	fzrq2,
	fhlfsbh,
	fhlfs,
	fhlffbh,
	fhlff
)
values
(
	?,?,?,?,?,?,?,?,?,?,
	?,?,?,?,?,?,?,?,?,?,
	?,?,?,?,?,?,?,?
)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);
                IDataParameter[] objDPArr = null;

                DataRow drValue = null;
                long lngEff = -1;
                for (int iRow = 0; iRow < p_dtbBA6.Rows.Count; iRow++)
                {
                    drValue = p_dtbBA6.Rows[iRow];
                    objHRPServ.CreateDatabaseParameter(28, out objDPArr);
                    objDPArr[0].Value = drValue["fprn"].ToString();
                    objDPArr[1].Value = drValue["ftimes"].ToString();
                    objDPArr[2].Value = drValue["fflfsbh"].ToString();
                    objDPArr[3].Value = drValue["FFLFS"].ToString();
                    objDPArr[4].Value = drValue["FFLCXBH"].ToString();
                    objDPArr[5].Value = drValue["FFLCX"].ToString();
                    objDPArr[6].Value = drValue["FFLZZBH"].ToString();
                    objDPArr[7].Value = drValue["FFLZZ"].ToString();
                    objDPArr[8].Value = drValue["FYJY"].ToString();
                    objDPArr[9].Value = drValue["FYCS"].ToString();
                    objDPArr[10].Value = drValue["FYTS"].ToString();
                    if (drValue["FYRQ1"] != DBNull.Value)
                    {
                        objDPArr[11].DbType = DbType.DateTime;
                        objDPArr[11].Value = Convert.ToDateTime(drValue["FYRQ1"]);
                    }
                    else
                    {
                        objDPArr[11].Value = DBNull.Value;
                    }
                    if (drValue["FYRQ2"] != DBNull.Value)
                    {
                        objDPArr[12].DbType = DbType.DateTime;
                        objDPArr[12].Value = Convert.ToDateTime(drValue["FYRQ2"]);
                    }
                    else
                    {
                        objDPArr[12].Value = DBNull.Value;
                    }
                    objDPArr[13].Value = drValue["FQJY"].ToString();
                    objDPArr[14].Value = drValue["FQCS"].ToString();
                    objDPArr[15].Value = drValue["FQTS"].ToString();
                    if (drValue["FQRQ1"] != DBNull.Value)
                    {
                        objDPArr[16].DbType = DbType.DateTime;
                        objDPArr[16].Value = Convert.ToDateTime(drValue["FQRQ1"]);
                    }
                    else
                    {
                        objDPArr[16].Value = DBNull.Value;
                    }
                    if (drValue["FQRQ2"] != DBNull.Value)
                    {
                        objDPArr[17].DbType = DbType.DateTime;
                        objDPArr[17].Value = Convert.ToDateTime(drValue["FQRQ2"]);
                    }
                    else
                    {
                        objDPArr[17].Value = DBNull.Value;
                    }
                    objDPArr[18].Value = drValue["FZNAME"].ToString();
                    objDPArr[19].Value = drValue["FZJY"].ToString();
                    objDPArr[20].Value = drValue["FZCS"].ToString();
                    objDPArr[21].Value = drValue["FZTS"].ToString();
                    if (drValue["FZRQ1"] != DBNull.Value)
                    {
                        objDPArr[22].DbType = DbType.DateTime;
                        objDPArr[22].Value = Convert.ToDateTime(drValue["FZRQ1"]);
                    }
                    else
                    {
                        objDPArr[22].Value = DBNull.Value;
                    }
                    if (drValue["FZRQ2"] != DBNull.Value)
                    {
                        objDPArr[23].DbType = DbType.DateTime;
                        objDPArr[23].Value = Convert.ToDateTime(drValue["FZRQ2"]);
                    }
                    else
                    {
                        objDPArr[23].Value = DBNull.Value;
                    }
                    objDPArr[24].Value = drValue["FHLFSBH"].ToString();
                    objDPArr[25].Value = drValue["FHLFS"].ToString();
                    objDPArr[26].Value = drValue["FHLFFBH"].ToString();
                    objDPArr[27].Value = drValue["FHLFF"].ToString();

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
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
        /// 同步内容到HIS_BA7
        /// </summary>
        /// <param name="p_dtbBA7">HIS_BA7内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitToHIS_BA7(DataTable p_dtbBA7)
        {
            if (p_dtbBA7 == null)
            {
                return -1;
            }

            long lngRes = 0;
            try
            {
                string strSQL = @"insert into his_ba7
(
	fprn,
	ftimes,
	fhlrq1,
	fhlrq2,
	fhldrug,
	fhlproc,
	fhllxbh,
	fhllx
)
values
(
	?,?,?,?,?,?,?,?
)";

                clsHRPTableService objHRPServ = new clsHRPTableService();
                objHRPServ.m_mthSetDataBase_Selector(clsHRPTableService.enumDatabase_Selector.bytSQL_Server, clsHRPTableService.enumDatabase.bytGDCASE);
                IDataParameter[] objDPArr = null;

                DataRow drValue = null;
                long lngEff = -1;
                for (int iRow = 0; iRow < p_dtbBA7.Rows.Count; iRow++)
                {
                    drValue = p_dtbBA7.Rows[iRow];
                    objHRPServ.CreateDatabaseParameter(8, out objDPArr);
                    objDPArr[0].Value = drValue["fprn"].ToString();
                    objDPArr[1].Value = drValue["ftimes"].ToString();
                    objDPArr[2].Value = drValue["fhlrq1"].ToString();
                    objDPArr[3].Value = drValue["fhlrq2"].ToString();
                    objDPArr[4].Value = drValue["fhldrug"].ToString();
                    objDPArr[5].Value = drValue["fhlproc"].ToString();
                    objDPArr[6].Value = drValue["fhllxbh"].ToString();
                    objDPArr[7].Value = drValue["fhllx"].ToString();

                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
                }
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
