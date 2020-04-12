using System.EnterpriseServices;
using System;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.BaseCaseHistorySevice;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Drawing;
using System.Collections;

namespace com.digitalwave.InPatientCaseHistoryServ
{
    /// <summary>
    /// 产时记录
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsBrothRecords_F2Service : clsBaseCaseHistorySevice
    {
        public clsBrothRecords_F2Service()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private const string c_strGetTimeListSQL = "select inpatientdate,createdate,opendate from brothrecords_f2 where inpatientid = ?  and status=0 order by opendate desc";

        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.dtpgongsuotime,
       a.dtppomotime,
       a.rdbmopo,
       a.dtpgongkoukaitime,
       a.cbotaici,
       a.cboyunqi,
       a.cbotaiwei,
       a.dtpbrothtime,
       a.dtpbrothtype,
       a.dtptaipantime,
       a.rdbtaipantype,
       a.cboxingzhuang,
       a.cbozhongliang,
       a.cbodaxiao,
       a.cbowanzheng,
       a.cboqidai,
       a.cboqichang,
       a.cbozzhiliu,
       a.rdbhuiyintype,
       a.rdbhuiyinqiekaitype,
       a.cboneifeng,
       a.cbowaifeng,
       a.cboshixue,
       a.cbogongdigao,
       a.cbogongjingqingkuang,
       a.cboxueya1,
       a.cboxueya2,
       a.cbohuxi,
       a.cbomaibo,
       a.rdbyinger,
       a.cbosiwangyuanyin,
       a.rdbhuxitype,
       a.cbotizhong,
       a.cboshenchang,
       a.cboshuangdingjing,
       a.cbozhenjing,
       a.cboxin,
       a.cbofei,
       a.cbojixing,
       a.cboyichancheng,
       a.cboerchancheng,
       a.cbosanchancheng,
       a.cboquancheng,
       a.txtjieshen,
       a.xtzhuli,
       a.txthuli,
       a.txtzhidao,
       a.jieshenid,
       a.txtzhuliid,
       a.txthuliid,
       a.txtzhidaoid,
        a.airenname,
        a.age,
        a.jiguan,
        a.zhiye,
        a.renzhi,
        a.zhuzhi,
        a.babyid,
       a.modifydate,
       a.modifyuserid,
       tbe.lastname_vchr
  from brothrecords_f2 a, t_bse_employee tbe
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.createuserid = tbe.empno_chr";

        private const string c_strGetDeletedRecordContentSQL = @"select a.inpatientid,
        a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.dtpgongsuotime,
       a.dtppomotime,
       a.rdbmopo,
       a.dtpgongkoukaitime,
       a.cbotaici,
       a.cboyunqi,
       a.cbotaiwei,
       a.dtpbrothtime,
       a.dtpbrothtype,
       a.dtptaipantime,
       a.rdbtaipantype,
       a.cboxingzhuang,
       a.cbozhongliang,
       a.cbodaxiao,
       a.cbowanzheng,
       a.cboqidai,
       a.cboqichang,
       a.cbozzhiliu,
       a.rdbhuiyintype,
       a.rdbhuiyinqiekaitype,
       a.cboneifeng,
       a.cbowaifeng,
       a.cboshixue,
       a.cbogongdigao,
       a.cbogongjingqingkuang,
       a.cboxueya1,
       a.cboxueya2,
       a.cbohuxi,
       a.cbomaibo,
       a.rdbyinger,
       a.cbosiwangyuanyin,
       a.rdbhuxitype,
       a.cbotizhong,
       a.cboshenchang,
       a.cboshuangdingjing,
       a.cbozhenjing,
       a.cboxin,
       a.cbofei,
       a.cbojixing,
       a.cboyichancheng,
       a.cboerchancheng,
       a.cbosanchancheng,
       a.cboquancheng,
       a.txtjieshen,
       a.xtzhuli,
       a.txthuli,
       a.txtzhidao,
       a.jieshenid,
       a.txtzhuliid,
       a.txthuliid,
       a.txtzhidaoid,
       a.airenname,
        a.age,
        a.jiguan,
        a.zhiye,
        a.renzhi,
        a.zhuzhi,
        a.babyid,
       a.modifydate,
       a.modifyuserid,
       tbe.lastname_vchr
  from brothrecords_f2 a, t_bse_employee tbe
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.createuserid = tbe.empno_chr";


        private const string c_strCheckCreateDateSQL = @"select inpatientid,
        a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.dtpgongsuotime,
       a.dtppomotime,
       a.rdbmopo,
       a.dtpgongkoukaitime,
       a.cbotaici,
       a.cboyunqi,
       a.cbotaiwei,
       a.dtpbrothtime,
       a.dtpbrothtype,
       a.dtptaipantime,
       a.rdbtaipantype,
       a.cboxingzhuang,
       a.cbozhongliang,
       a.cbodaxiao,
       a.cbowanzheng,
       a.cboqidai,
       a.cboqichang,
       a.cbozzhiliu,
       a.rdbhuiyintype,
       a.rdbhuiyinqiekaitype,
       a.cboneifeng,
       a.cbowaifeng,
       a.cboshixue,
       a.cbogongdigao,
       a.cbogongjingqingkuang,
       a.cboxueya1,
       a.cboxueya2,
       a.cbohuxi,
       a.cbomaibo,
       a.rdbyinger,
       a.cbosiwangyuanyin,
       a.rdbhuxitype,
       a.cbotizhong,
       a.cboshenchang,
       a.cboshuangdingjing,
       a.cbozhenjing,
       a.cboxin,
       a.cbofei,
       a.cbojixing,
       a.cboyichancheng,
       a.cboerchancheng,
       a.cbosanchancheng,
       a.cboquancheng,
       a.txtjieshen,
       a.xtzhuli,
       a.txthuli,
       a.txtzhidao,
       a.jieshenid,
       a.txtzhuliid,
       a.txthuliid,
       a.txtzhidaoid,
       a.airenname,
        a.age,
        a.jiguan,
        a.zhiye,
        a.renzhi,
        a.zhuzhi,
        a.babyid,
       a.modifydate,
       a.modifyuserid
  from brothrecords_f2 a
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0";


        /// <summary>
        /// 更新GeneralDiseaseRecord中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = "update  brothrecords_f2 set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        private const string c_strGetDeleteRecordSQL = "";

        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select firstprintdate,modifydate
																			from brothrecords_f2
																			where inpatientid = ?
																			and inpatientdate = ?
																			and status = 0";

        private const string c_strModifyRecordSQL = @"update brothrecords_f2 set modifydate=?,modifyuserid=?, dtpgongsuotime=?,dtppomotime=?,rdbmopo=?,
			dtpgongkoukaitime=?,cbotaici=?,cboyunqi=?,cbotaiwei=?,dtpbrothtime=?,dtpbrothtype=?,dtptaipantime=?,rdbtaipantype=?,cboxingzhuang=?,cbozhongliang=?,
			cbodaxiao=?,cbowanzheng=?,cboqidai=?,cboqichang=?,cbozzhiliu=?,rdbhuiyintype=?,rdbhuiyinqiekaitype=?,cboneifeng=?,cbowaifeng=?,cboshixue=?,cbogongdigao=?,
			cbogongjingqingkuang=?,cboxueya1=?,cboxueya2=?,cbohuxi=?,cbomaibo=?,rdbyinger=?,cbosiwangyuanyin=?,rdbhuxitype=?,cbotizhong=?,cboshenchang=?,cboshuangdingjing=?,
			cbozhenjing=?,cboxin=?,cbofei=?,cbojixing=?,cboyichancheng=?,cboerchancheng=?,cbosanchancheng=?,cboquancheng=?,
			txtjieshen=?,xtzhuli=?,txthuli=?,txtzhidao=?,jieshenid=?,txtzhuliid=?,txthuliid=?,txtzhidaoid=?,
            airenname=?,age=?,jiguan=?,zhiye=?,renzhi=?,zhuzhi=?,babyid=? 
			where inpatientid=? and inpatientdate=? and opendate=? and status=0";  
        
        
        private const string c_strDeleteRecordSQL = "update brothrecords_f2 set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strAddNewRecordSQL = @"insert into brothrecords_f2 (inpatientid,inpatientdate,opendate,createdate,
			createuserid,status,modifydate,modifyuserid,dtpgongsuotime,dtppomotime,rdbmopo,
			dtpgongkoukaitime,cbotaici,cboyunqi,cbotaiwei,dtpbrothtime,dtpbrothtype,dtptaipantime,rdbtaipantype,cboxingzhuang,cbozhongliang,
			cbodaxiao,cbowanzheng,cboqidai,cboqichang,cbozzhiliu,rdbhuiyintype,rdbhuiyinqiekaitype,cboneifeng,cbowaifeng,cboshixue,cbogongdigao,
			cbogongjingqingkuang,cboxueya1,cboxueya2,cbohuxi,cbomaibo,rdbyinger,cbosiwangyuanyin,rdbhuxitype,cbotizhong,cboshenchang,cboshuangdingjing,
			cbozhenjing,cboxin,cbofei,cbojixing,cboyichancheng,cboerchancheng,cbosanchancheng,cboquancheng,
			txtjieshen,xtzhuli,txthuli,txtzhidao,jieshenid,txtzhuliid,txthuliid,txtzhidaoid,
            airenname,age,jiguan,zhiye,renzhi,zhuzhi,babyid) 
			values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
					?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,
					?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        ///  获取病人的该记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDateArr"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strInPatientID,
            out string[] p_strInPatientDateArr,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strInPatientDateArr = null;
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngGetRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "")
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strInPatientDateArr = new string[dtbValue.Rows.Count];
                    p_strCreateRecordTimeArr = new string[dtbValue.Rows.Count];
                    p_strOpenRecordTimeArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strInPatientDateArr[i] = dtbValue.Rows[i]["INPATIENTDATE"].ToString();
                        p_strCreateRecordTimeArr[i] = dtbValue.Rows[i]["CREATEDATE"].ToString();
                        p_strOpenRecordTimeArr[i] = dtbValue.Rows[i]["OPENDATE"].ToString();
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

            }		//返回
            return lngRes;


        }

        /// <summary>
        ///  更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数                              
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return (long)enmOperationResult.Parameter_Error;
            //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

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

        /// <summary>
        ///  获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strDeleteUserID"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngGetDeleteRecordTimeList");
                //if(lngCheckRes <= 0)
                //return lngCheckRes;	
                lngRes = (long)enmOperationResult.DB_Succeed;

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
        ///  获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientCaseHistoryServ","m_lngGetDeleteRecordTimeListAll");
                //if(lngCheckRes <= 0)
                //return lngCheckRes;	

                lngRes = (long)enmOperationResult.DB_Succeed;

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
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(
            string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsBaseCaseHistoryInfo p_objRecordContent,
            out clsPictureBoxValue[] p_objPicValueArr)
        {
            p_objRecordContent = null;
            p_objPicValueArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsBrothRecords_F2 p_objContent = new clsBrothRecords_F2();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        #region 获取结果
                        p_objContent.m_strInPatientID = p_strInPatientID;
                        p_objContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                        p_objContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        p_objContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();

                        if (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            p_objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else p_objContent.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[i]["FIRSTPRINTDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            p_objContent.m_bytStatus = 0;
                        else p_objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        p_objContent.M_DTPGONGSUOTIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPGONGSUOTIME"]);
                        p_objContent.M_DTPPOMOTIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPPOMOTIME"]);
                        p_objContent.M_RDBMOPO = dtbValue.Rows[i]["RDBMOPO"].ToString();
                        p_objContent.M_DTPGONGKOUKAITIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPGONGKOUKAITIME"]);
                        p_objContent.M_CBOTAICI = dtbValue.Rows[i]["CBOTAICI"].ToString();
                        p_objContent.M_CBOYUNQI = dtbValue.Rows[i]["CBOYUNQI"].ToString();
                        p_objContent.M_CBOTAIWEI = dtbValue.Rows[i]["CBOTAIWEI"].ToString();
                        p_objContent.M_DTPBROTHTIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPBROTHTIME"]);
                        p_objContent.M_DTPBROTHTYPE = dtbValue.Rows[i]["DTPBROTHTYPE"].ToString();
                        p_objContent.M_DTPTAIPANTIME =Convert.ToDateTime( dtbValue.Rows[i]["DTPTAIPANTIME"]);
                        p_objContent.M_RDBTAIPANTYPE = dtbValue.Rows[i]["RDBTAIPANTYPE"].ToString();
                        p_objContent.M_CBOXINGZHUANG = dtbValue.Rows[i]["CBOXINGZHUANG"].ToString();
                        p_objContent.M_CBOZHONGLIANG = dtbValue.Rows[i]["CBOZHONGLIANG"].ToString();
                        p_objContent.M_CBODAXIAO = dtbValue.Rows[i]["CBODAXIAO"].ToString();
                        p_objContent.M_CBOWANZHENG = dtbValue.Rows[i]["CBOWANZHENG"].ToString();
                        p_objContent.M_CBOQIDAI = dtbValue.Rows[i]["CBOQIDAI"].ToString();
                        p_objContent.M_CBOQICHANG = dtbValue.Rows[i]["CBOQICHANG"].ToString();
                        p_objContent.M_CBOZZHILIU = dtbValue.Rows[i]["CBOZZHILIU"].ToString();
                        p_objContent.M_RDBHUIYINTYPE = dtbValue.Rows[i]["RDBHUIYINTYPE"].ToString();
                        p_objContent.M_RDBHUIYINQIEKAITYPE = dtbValue.Rows[i]["RDBHUIYINQIEKAITYPE"].ToString();
                        p_objContent.M_CBONEIFENG = dtbValue.Rows[i]["CBONEIFENG"].ToString();
                        p_objContent.M_CBOWAIFENG = dtbValue.Rows[i]["CBOWAIFENG"].ToString();
                        p_objContent.M_CBOSHIXUE = dtbValue.Rows[i]["CBOSHIXUE"].ToString();
                        p_objContent.M_CBOGONGDIGAO = dtbValue.Rows[i]["CBOGONGDIGAO"].ToString();
                        p_objContent.M_CBOGONGJINGQINGKUANG = dtbValue.Rows[i]["CBOGONGJINGQINGKUANG"].ToString();
                        p_objContent.M_CBOXUEYA1 = dtbValue.Rows[i]["CBOXUEYA1"].ToString();
                        p_objContent.M_CBOXUEYA2 = dtbValue.Rows[i]["CBOXUEYA2"].ToString();
                        p_objContent.M_CBOHUXI = dtbValue.Rows[i]["CBOHUXI"].ToString();
                        p_objContent.M_CBOMAIBO = dtbValue.Rows[i]["CBOMAIBO"].ToString();
                        p_objContent.M_RDBYINGER = dtbValue.Rows[i]["RDBYINGER"].ToString();
                        p_objContent.M_CBOSIWANGYUANYIN = dtbValue.Rows[i]["CBOSIWANGYUANYIN"].ToString();
                        p_objContent.M_RDBHUXITYPE = dtbValue.Rows[i]["RDBHUXITYPE"].ToString();
                        p_objContent.M_CBOTIZHONG = dtbValue.Rows[i]["CBOTIZHONG"].ToString();
                        p_objContent.M_CBOSHENCHANG = dtbValue.Rows[i]["CBOSHENCHANG"].ToString();
                        p_objContent.M_CBOSHUANGDINGJING = dtbValue.Rows[i]["CBOSHUANGDINGJING"].ToString(); ;
                        p_objContent.M_CBOZHENJING = dtbValue.Rows[i]["CBOZHENJING"].ToString();
                        p_objContent.M_CBOXIN= dtbValue.Rows[i]["CBOXIN"].ToString();
                        p_objContent.M_CBOFEI = dtbValue.Rows[i]["CBOFEI"].ToString();
                        p_objContent.M_CBOJIXING = dtbValue.Rows[i]["CBOJIXING"].ToString();
                        p_objContent.M_CBOYICHANCHENG = dtbValue.Rows[i]["CBOYICHANCHENG"].ToString();
                        p_objContent.M_CBOERCHANCHENG = dtbValue.Rows[i]["CBOERCHANCHENG"].ToString();
                        p_objContent.M_CBOSANCHANCHENG = dtbValue.Rows[i]["CBOSANCHANCHENG"].ToString();
                        p_objContent.M_CBOQUANCHENG = dtbValue.Rows[i]["CBOQUANCHENG"].ToString();
   
                        p_objContent.M_TXTJIESHEN = dtbValue.Rows[i]["TXTJIESHEN"].ToString();
                        p_objContent.M_TXTZHULI = dtbValue.Rows[i]["XTZHULI"].ToString();
                        p_objContent.M_TXTHULI = dtbValue.Rows[i]["TXTHULI"].ToString();
                        p_objContent.M_TXTZHIDAO = dtbValue.Rows[i]["TXTZHIDAO"].ToString();
                        p_objContent.M_TXTJIESHENID = dtbValue.Rows[i]["JIESHENID"].ToString();
                        p_objContent.M_TXTZHULIID = dtbValue.Rows[i]["TXTZHULIID"].ToString();
                        p_objContent.M_TXTHULIID= dtbValue.Rows[i]["TXTHULIID"].ToString();
                        p_objContent.M_TXTZHIDAOID = dtbValue.Rows[i]["TXTZHIDAOID"].ToString();

                        p_objContent.M_TXTAIRENNAME = dtbValue.Rows[i]["AIRENNAME"].ToString();
                        p_objContent.M_TXTAGE = dtbValue.Rows[i]["AGE"].ToString();
                        p_objContent.M_TXTJIGUAN = dtbValue.Rows[i]["JIGUAN"].ToString();
                        p_objContent.M_TXTZHIYE = dtbValue.Rows[i]["ZHIYE"].ToString();
                        p_objContent.M_TXTRENZHI = dtbValue.Rows[i]["RENZHI"].ToString();
                        p_objContent.M_TXTZHUZHI = dtbValue.Rows[i]["ZHUZHI"].ToString();
                        p_objContent.M_TXTBABYID = dtbValue.Rows[i]["BABYID"].ToString();                       

                        p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        
                        #endregion
                    }
                    p_objRecordContent = p_objContent;
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
        /// 查看是否有相同的记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objPreModifyInfo)
        {
            p_objPreModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objPreModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_objPreModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString());
                        p_objPreModifyInfo.m_strActionUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
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
        /// 查看是否最新记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            string c_strCheckLastModifyRecordSQL = null;
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                c_strCheckLastModifyRecordSQL = "select top 1 modifydate,modifyuserid from brothrecords_f2 where inpatientid = ? and inpatientdate= ? and opendate= ? and status=0 order by modifydate desc";
            }
            else
            {
                c_strCheckLastModifyRecordSQL = @"select modifydate, modifyuserid
  from (select modifydate, modifyuserid
          from brothrecords_f2
         where inpatientid = ?
           and inpatientdate = ?
           and opendate = ?
           and status = 0
         order by modifydate desc)
 where rownum = 1";
            }


            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果,
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["ModifyDate"].ToString());
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[i]["ModifyUserID"].ToString();
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
        /// 添加记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <param name="p_strDiseaseID"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;

            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;

            clsBrothRecords_F2 m_objContent = (clsBrothRecords_F2)p_objRecordContent;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(66, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = m_objContent.m_strInPatientID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr[2].DbType = DbType.DateTime;
                objLisAddItemRefArr[2].Value = m_objContent.m_dtmOpenDate;
                objLisAddItemRefArr[3].DbType = DbType.DateTime;
                objLisAddItemRefArr[3].Value = m_objContent.m_dtmCreateDate;
                objLisAddItemRefArr[4].Value = m_objContent.m_strCreateUserID;
                objLisAddItemRefArr[5].Value = 0;
                objLisAddItemRefArr[6].DbType = DbType.DateTime;
                objLisAddItemRefArr[6].Value = m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr[7].Value = m_objContent.m_strModifyUserID;
                objLisAddItemRefArr[8].DbType = DbType.DateTime;
                objLisAddItemRefArr[8].Value = m_objContent.M_DTPGONGSUOTIME;
                objLisAddItemRefArr[9].DbType = DbType.DateTime;
                objLisAddItemRefArr[9].Value = m_objContent.M_DTPPOMOTIME;
                objLisAddItemRefArr[10].Value = m_objContent.M_RDBMOPO;
                objLisAddItemRefArr[11].DbType = DbType.DateTime;
                objLisAddItemRefArr[11].Value = m_objContent.M_DTPGONGKOUKAITIME;
                objLisAddItemRefArr[12].Value = m_objContent.M_CBOTAICI;
                objLisAddItemRefArr[13].Value = m_objContent.M_CBOYUNQI;
                objLisAddItemRefArr[14].Value = m_objContent.M_CBOTAIWEI;
                objLisAddItemRefArr[15].DbType = DbType.DateTime;
                objLisAddItemRefArr[15].Value = m_objContent.M_DTPBROTHTIME;
                objLisAddItemRefArr[16].Value = m_objContent.M_DTPBROTHTYPE;
                objLisAddItemRefArr[17].DbType = DbType.DateTime;
                objLisAddItemRefArr[17].Value = m_objContent.M_DTPTAIPANTIME;
                objLisAddItemRefArr[18].Value = m_objContent.M_RDBTAIPANTYPE;
                objLisAddItemRefArr[19].Value = m_objContent.M_CBOXINGZHUANG;
                objLisAddItemRefArr[20].Value = m_objContent.M_CBOZHONGLIANG;
                objLisAddItemRefArr[21].Value = m_objContent.M_CBODAXIAO;
                objLisAddItemRefArr[22].Value = m_objContent.M_CBOWANZHENG;
                objLisAddItemRefArr[23].Value = m_objContent.M_CBOQIDAI;
                objLisAddItemRefArr[24].Value = m_objContent.M_CBOQICHANG;
                objLisAddItemRefArr[25].Value = m_objContent.M_CBOZZHILIU;
                objLisAddItemRefArr[26].Value = m_objContent.M_RDBHUIYINTYPE;
                objLisAddItemRefArr[27].Value = m_objContent.M_RDBHUIYINQIEKAITYPE;
                objLisAddItemRefArr[28].Value = m_objContent.M_CBONEIFENG;
                objLisAddItemRefArr[29].Value = m_objContent.M_CBOWAIFENG;
                objLisAddItemRefArr[30].Value = m_objContent.M_CBOSHIXUE;
                objLisAddItemRefArr[31].Value = m_objContent.M_CBOGONGDIGAO;
                objLisAddItemRefArr[32].Value = m_objContent.M_CBOGONGJINGQINGKUANG;
                objLisAddItemRefArr[33].Value = m_objContent.M_CBOXUEYA1;
                objLisAddItemRefArr[34].Value = m_objContent.M_CBOXUEYA2;
                objLisAddItemRefArr[35].Value = m_objContent.M_CBOHUXI;
                objLisAddItemRefArr[36].Value = m_objContent.M_CBOMAIBO;
                objLisAddItemRefArr[37].Value = m_objContent.M_RDBYINGER;
                objLisAddItemRefArr[38].Value = m_objContent.M_CBOSIWANGYUANYIN;
                objLisAddItemRefArr[39].Value = m_objContent.M_RDBHUXITYPE;
                objLisAddItemRefArr[40].Value = m_objContent.M_CBOTIZHONG;
                objLisAddItemRefArr[41].Value = m_objContent.M_CBOSHENCHANG;
           
                objLisAddItemRefArr[42].Value = m_objContent.M_CBOSHUANGDINGJING;
                objLisAddItemRefArr[43].Value = m_objContent.M_CBOZHENJING;
                objLisAddItemRefArr[44].Value = m_objContent.M_CBOXIN;
                objLisAddItemRefArr[45].Value = m_objContent.M_CBOFEI;
                objLisAddItemRefArr[46].Value = m_objContent.M_CBOJIXING;
                objLisAddItemRefArr[47].Value = m_objContent.M_CBOYICHANCHENG;
                objLisAddItemRefArr[48].Value = m_objContent.M_CBOERCHANCHENG;
                objLisAddItemRefArr[49].Value = m_objContent.M_CBOSANCHANCHENG;
                objLisAddItemRefArr[50].Value = m_objContent.M_CBOQUANCHENG;

                objLisAddItemRefArr[51].Value = m_objContent.M_TXTJIESHEN;
                objLisAddItemRefArr[52].Value = m_objContent.M_TXTZHULI;
                objLisAddItemRefArr[53].Value = m_objContent.M_TXTHULI;
                objLisAddItemRefArr[54].Value = m_objContent.M_TXTZHIDAO;
                objLisAddItemRefArr[55].Value = m_objContent.M_TXTJIESHENID;
                objLisAddItemRefArr[56].Value = m_objContent.M_TXTZHULIID;
                objLisAddItemRefArr[57].Value = m_objContent.M_TXTHULIID;
                objLisAddItemRefArr[58].Value = m_objContent.M_TXTZHIDAOID;

                objLisAddItemRefArr[59].Value = m_objContent.M_TXTAIRENNAME;
                objLisAddItemRefArr[60].Value = m_objContent.M_TXTAGE;
                objLisAddItemRefArr[61].Value = m_objContent.M_TXTJIGUAN;
                objLisAddItemRefArr[62].Value = m_objContent.M_TXTZHIYE;
                objLisAddItemRefArr[63].Value = m_objContent.M_TXTRENZHI;
                objLisAddItemRefArr[64].Value = m_objContent.M_TXTZHUZHI;
                objLisAddItemRefArr[65].Value = m_objContent.M_TXTBABYID;


                //objLisAddItemRefArr[59].Value = m_objContent.m_strOTHERCHECKXML;
                //objLisAddItemRefArr[60].Value = m_objContent.m_strOUTHOSPITALADVICE;
                //objLisAddItemRefArr[61].Value = m_objContent.m_strOUTHOSPITALADVICEXML;
                //objLisAddItemRefArr[62].Value = m_objContent.m_strDEALWITH;
                //objLisAddItemRefArr[63].Value = m_objContent.m_strDEALWITHXML;
                //objLisAddItemRefArr[64].Value = m_objContent.m_strINROOMCHECKDOCID;
                //objLisAddItemRefArr[65].Value = m_objContent.m_strRECORDSIGNDOCID;
                //objLisAddItemRefArr[66].Value = m_objContent.m_strINROOMCHECKDOCName;
                //objLisAddItemRefArr[67].Value = m_objContent.m_strRECORDSIGNDOCName;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngRecEff, objLisAddItemRefArr);
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
        /// 修改内容
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <param name="p_strDiseaseID"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;

            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;

            clsBrothRecords_F2 m_objContent = (clsBrothRecords_F2)p_objRecordContent;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(63, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr[1].Value = m_objContent.m_strModifyUserID;
                objLisAddItemRefArr[2].DbType = DbType.DateTime;
                objLisAddItemRefArr[2].Value = m_objContent.M_DTPGONGSUOTIME;
                objLisAddItemRefArr[3].DbType = DbType.DateTime;
                objLisAddItemRefArr[3].Value = m_objContent.M_DTPPOMOTIME;
                objLisAddItemRefArr[4].Value = m_objContent.M_RDBMOPO;
                objLisAddItemRefArr[5].DbType = DbType.DateTime;
                objLisAddItemRefArr[5].Value = m_objContent.M_DTPGONGKOUKAITIME;
                objLisAddItemRefArr[6].Value = m_objContent.M_CBOTAICI;
                objLisAddItemRefArr[7].Value = m_objContent.M_CBOYUNQI;
                objLisAddItemRefArr[8].Value = m_objContent.M_CBOTAIWEI;
                objLisAddItemRefArr[9].DbType = DbType.DateTime;
                objLisAddItemRefArr[9].Value = m_objContent.M_DTPBROTHTIME;
                objLisAddItemRefArr[10].Value = m_objContent.M_DTPBROTHTYPE;
                objLisAddItemRefArr[11].DbType = DbType.DateTime;
                objLisAddItemRefArr[11].Value = m_objContent.M_DTPTAIPANTIME;
                objLisAddItemRefArr[12].Value = m_objContent.M_RDBTAIPANTYPE;
                objLisAddItemRefArr[13].Value = m_objContent.M_CBOXINGZHUANG;
                objLisAddItemRefArr[14].Value = m_objContent.M_CBOZHONGLIANG;
                objLisAddItemRefArr[15].Value = m_objContent.M_CBODAXIAO;
                objLisAddItemRefArr[16].Value = m_objContent.M_CBOWANZHENG;
                objLisAddItemRefArr[17].Value = m_objContent.M_CBOQIDAI;
                objLisAddItemRefArr[18].Value = m_objContent.M_CBOQICHANG;
                objLisAddItemRefArr[19].Value = m_objContent.M_CBOZZHILIU;
                objLisAddItemRefArr[20].Value = m_objContent.M_RDBHUIYINTYPE;
                objLisAddItemRefArr[21].Value = m_objContent.M_RDBHUIYINQIEKAITYPE;
                objLisAddItemRefArr[22].Value = m_objContent.M_CBONEIFENG;
                objLisAddItemRefArr[23].Value = m_objContent.M_CBOWAIFENG;
                objLisAddItemRefArr[24].Value = m_objContent.M_CBOSHIXUE;
                objLisAddItemRefArr[25].Value = m_objContent.M_CBOGONGDIGAO;
                objLisAddItemRefArr[26].Value = m_objContent.M_CBOGONGJINGQINGKUANG;
                objLisAddItemRefArr[27].Value = m_objContent.M_CBOXUEYA1;
                objLisAddItemRefArr[28].Value = m_objContent.M_CBOXUEYA2;
                objLisAddItemRefArr[29].Value = m_objContent.M_CBOHUXI;
                objLisAddItemRefArr[30].Value = m_objContent.M_CBOMAIBO;
                objLisAddItemRefArr[31].Value = m_objContent.M_RDBYINGER;
                objLisAddItemRefArr[32].Value = m_objContent.M_CBOSIWANGYUANYIN;
                objLisAddItemRefArr[33].Value = m_objContent.M_RDBHUXITYPE;
                objLisAddItemRefArr[34].Value = m_objContent.M_CBOTIZHONG;
                objLisAddItemRefArr[35].Value = m_objContent.M_CBOSHENCHANG;

                objLisAddItemRefArr[36].Value = m_objContent.M_CBOSHUANGDINGJING;
                objLisAddItemRefArr[37].Value = m_objContent.M_CBOZHENJING;
                objLisAddItemRefArr[38].Value = m_objContent.M_CBOXIN;
                objLisAddItemRefArr[39].Value = m_objContent.M_CBOFEI;
                objLisAddItemRefArr[40].Value = m_objContent.M_CBOJIXING;
                objLisAddItemRefArr[41].Value = m_objContent.M_CBOYICHANCHENG;
                objLisAddItemRefArr[42].Value = m_objContent.M_CBOERCHANCHENG;
                objLisAddItemRefArr[43].Value = m_objContent.M_CBOSANCHANCHENG;
                objLisAddItemRefArr[44].Value = m_objContent.M_CBOQUANCHENG;

                objLisAddItemRefArr[45].Value = m_objContent.M_TXTJIESHEN;
                objLisAddItemRefArr[46].Value = m_objContent.M_TXTZHULI;
                objLisAddItemRefArr[47].Value = m_objContent.M_TXTHULI;
                objLisAddItemRefArr[48].Value = m_objContent.M_TXTZHIDAO;
                objLisAddItemRefArr[49].Value = m_objContent.M_TXTJIESHENID;
                objLisAddItemRefArr[50].Value = m_objContent.M_TXTZHULIID;
                objLisAddItemRefArr[51].Value = m_objContent.M_TXTHULIID;
                objLisAddItemRefArr[52].Value = m_objContent.M_TXTZHIDAOID;

                objLisAddItemRefArr[53].Value = m_objContent.M_TXTAIRENNAME;
                objLisAddItemRefArr[54].Value = m_objContent.M_TXTAGE;
                objLisAddItemRefArr[55].Value = m_objContent.M_TXTJIGUAN;
                objLisAddItemRefArr[56].Value = m_objContent.M_TXTZHIYE;
                objLisAddItemRefArr[57].Value = m_objContent.M_TXTRENZHI;
                objLisAddItemRefArr[58].Value = m_objContent.M_TXTZHUZHI;
                objLisAddItemRefArr[59].Value = m_objContent.M_TXTBABYID;


                objLisAddItemRefArr[60].Value = m_objContent.m_strInPatientID;
                objLisAddItemRefArr[61].DbType = DbType.DateTime;
                objLisAddItemRefArr[61].Value = m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr[62].DbType = DbType.DateTime;
                objLisAddItemRefArr[62].Value = m_objContent.m_dtmOpenDate;

                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objLisAddItemRefArr);
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
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;


            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
                if (lngEff < 0) return -1;
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
        /// 获取首次打印时间及修改时间
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_dtmModifyDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate
            (string p_strInPatientID,
            string p_strInPatientDate, clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.MinValue;
            p_strFirstPrintDate = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //			objDPArr[2].Value=DateTime.Parse(p_strOpenRecordTime);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
                //返回
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

            }	//返回
            return lngRes;
        }


        /// <summary>
        /// 获取指定的已删除记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenRecordTime"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenRecordTime,
            clsHRPTableService p_objHRPServ,
            out clsBaseCaseHistoryInfo p_objRecordContent)
        {
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);


                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenRecordTime);

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeletedRecordContentSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsBrothRecords_F2 p_objContent = new clsBrothRecords_F2();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        #region 获取结果
                        p_objContent.m_strInPatientID = p_strInPatientID;
                        p_objContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                        p_objContent.m_dtmDeActivedDate = Convert.ToDateTime(dtbValue.Rows[i]["DEACTIVEDDATE"]);
                        p_objContent.m_strDeActivedOperatorID = dtbValue.Rows[i]["DEACTIVEDOPERATORID"].ToString();
                        p_objContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        p_objContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();

                        if (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            p_objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else p_objContent.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[i]["FIRSTPRINTDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            p_objContent.m_bytStatus = 0;
                        else p_objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        p_objContent.M_DTPGONGSUOTIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPGONGSUOTIME"]);
                        p_objContent.M_DTPPOMOTIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPPOMOTIME"]);
                        p_objContent.M_RDBMOPO = dtbValue.Rows[i]["RDBMOPO"].ToString();
                        p_objContent.M_DTPGONGKOUKAITIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPGONGKOUKAITIME"]);
                        p_objContent.M_CBOTAICI = dtbValue.Rows[i]["CBOTAICI"].ToString();
                        p_objContent.M_CBOYUNQI = dtbValue.Rows[i]["CBOYUNQI"].ToString();
                        p_objContent.M_CBOTAIWEI = dtbValue.Rows[i]["CBOTAIWEI"].ToString();
                        p_objContent.M_DTPBROTHTIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPBROTHTIME"]);
                        p_objContent.M_DTPBROTHTYPE = dtbValue.Rows[i]["DTPBROTHTYPE"].ToString();
                        p_objContent.M_DTPTAIPANTIME = Convert.ToDateTime(dtbValue.Rows[i]["DTPTAIPANTIME"]);
                        p_objContent.M_RDBTAIPANTYPE = dtbValue.Rows[i]["RDBTAIPANTYPE"].ToString();
                        p_objContent.M_CBOXINGZHUANG = dtbValue.Rows[i]["CBOXINGZHUANG"].ToString();
                        p_objContent.M_CBOZHONGLIANG = dtbValue.Rows[i]["CBOZHONGLIANG"].ToString();
                        p_objContent.M_CBODAXIAO = dtbValue.Rows[i]["CBODAXIAO"].ToString();
                        p_objContent.M_CBOWANZHENG = dtbValue.Rows[i]["CBOWANZHENG"].ToString();
                        p_objContent.M_CBOQIDAI = dtbValue.Rows[i]["CBOQIDAI"].ToString();
                        p_objContent.M_CBOQICHANG = dtbValue.Rows[i]["CBOQICHANG"].ToString();
                        p_objContent.M_CBOZZHILIU = dtbValue.Rows[i]["CBOZZHILIU"].ToString();
                        p_objContent.M_RDBHUIYINTYPE = dtbValue.Rows[i]["RDBHUIYINTYPE"].ToString();
                        p_objContent.M_RDBHUIYINQIEKAITYPE = dtbValue.Rows[i]["RDBHUIYINQIEKAITYPE"].ToString();
                        p_objContent.M_CBONEIFENG = dtbValue.Rows[i]["CBONEIFENG"].ToString();
                        p_objContent.M_CBOWAIFENG = dtbValue.Rows[i]["CBOWAIFENG"].ToString();
                        p_objContent.M_CBOSHIXUE = dtbValue.Rows[i]["CBOSHIXUE"].ToString();
                        p_objContent.M_CBOGONGDIGAO = dtbValue.Rows[i]["CBOGONGDIGAO"].ToString();
                        p_objContent.M_CBOGONGJINGQINGKUANG = dtbValue.Rows[i]["CBOGONGJINGQINGKUANG"].ToString();
                        p_objContent.M_CBOXUEYA1 = dtbValue.Rows[i]["CBOXUEYA1"].ToString();
                        p_objContent.M_CBOXUEYA2 = dtbValue.Rows[i]["CBOXUEYA2"].ToString();
                        p_objContent.M_CBOHUXI = dtbValue.Rows[i]["CBOHUXI"].ToString();
                        p_objContent.M_CBOMAIBO = dtbValue.Rows[i]["CBOMAIBO"].ToString();
                        p_objContent.M_RDBYINGER = dtbValue.Rows[i]["RDBYINGER"].ToString();
                        p_objContent.M_CBOSIWANGYUANYIN = dtbValue.Rows[i]["CBOSIWANGYUANYIN"].ToString();
                        p_objContent.M_RDBHUXITYPE = dtbValue.Rows[i]["RDBHUXITYPE"].ToString();
                        p_objContent.M_CBOTIZHONG = dtbValue.Rows[i]["CBOTIZHONG"].ToString();
                        p_objContent.M_CBOSHENCHANG = dtbValue.Rows[i]["CBOSHENCHANG"].ToString();
                        p_objContent.M_CBOSHUANGDINGJING = dtbValue.Rows[i]["CBOSHUANGDINGJING"].ToString(); ;
                        p_objContent.M_CBOZHENJING = dtbValue.Rows[i]["CBOZHENJING"].ToString();
                        p_objContent.M_CBOXIN = dtbValue.Rows[i]["CBOXIN"].ToString();
                        p_objContent.M_CBOFEI = dtbValue.Rows[i]["CBOFEI"].ToString();
                        p_objContent.M_CBOJIXING = dtbValue.Rows[i]["CBOJIXING"].ToString();
                        p_objContent.M_CBOYICHANCHENG = dtbValue.Rows[i]["CBOYICHANCHENG"].ToString();
                        p_objContent.M_CBOERCHANCHENG = dtbValue.Rows[i]["CBOERCHANCHENG"].ToString();
                        p_objContent.M_CBOSANCHANCHENG = dtbValue.Rows[i]["CBOSANCHANCHENG"].ToString();
                        p_objContent.M_CBOQUANCHENG = dtbValue.Rows[i]["CBOQUANCHENG"].ToString();

                        p_objContent.M_TXTJIESHEN = dtbValue.Rows[i]["TXTJIESHEN"].ToString();
                        p_objContent.M_TXTZHULI = dtbValue.Rows[i]["XTZHULI"].ToString();
                        p_objContent.M_TXTHULI = dtbValue.Rows[i]["TXTHULI"].ToString();
                        p_objContent.M_TXTZHIDAO = dtbValue.Rows[i]["TXTZHIDAO"].ToString();
                        p_objContent.M_TXTJIESHENID = dtbValue.Rows[i]["JIESHENID"].ToString();
                        p_objContent.M_TXTZHULIID = dtbValue.Rows[i]["TXTZHULIID"].ToString();
                        p_objContent.M_TXTHULIID = dtbValue.Rows[i]["TXTHULIID"].ToString();
                        p_objContent.M_TXTZHIDAOID = dtbValue.Rows[i]["TXTZHIDAOID"].ToString();

                        p_objContent.M_TXTAIRENNAME = dtbValue.Rows[i]["AIRENNAME"].ToString();
                        p_objContent.M_TXTAGE = dtbValue.Rows[i]["AGE"].ToString();
                        p_objContent.M_TXTJIGUAN = dtbValue.Rows[i]["JIGUAN"].ToString();
                        p_objContent.M_TXTZHIYE = dtbValue.Rows[i]["ZHIYE"].ToString();
                        p_objContent.M_TXTRENZHI = dtbValue.Rows[i]["RENZHI"].ToString();
                        p_objContent.M_TXTZHUZHI = dtbValue.Rows[i]["ZHUZHI"].ToString();
                        p_objContent.M_TXTBABYID = dtbValue.Rows[i]["BABYID"].ToString();  

                        p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        #endregion
                    }

                    p_objRecordContent = p_objContent;
                }

                //返回
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

            }		//返回
            return lngRes;

        }

    }

}
