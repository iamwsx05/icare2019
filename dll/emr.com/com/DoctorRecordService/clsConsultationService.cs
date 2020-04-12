using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.DiseaseTrackService;
using System.Collections; 

namespace com.digitalwave.DiseaseTrackService
{
    /// <summary>
    /// 实现特殊记录的中间件。
    /// 病历资料－－会诊记录
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsConsultationService : clsDiseaseTrackService
    {
        /// <summary>
        /// 从ConsultationRecord获取指定病人的所有没有删除记录的时间。
        /// 用 InPatientID ,InPatientDate ,Status = 0 查得所有可用的CreateDate时间
        /// </summary>
        private const string c_strGetTimeListSQL = "select createdate,opendate from consultationrecord where inpatientid = ? and inpatientdate= ? and status=0";

        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.casehistory,
       a.casehistoryxml,
       a.consultationorder,
       a.consultationorderxml,
       a.consultationidea,
       a.consultationideaxml,
       a.otherhospitalxml,
       a.otherhospital,
       a.hasreplied,
       b.modifydate,
       b.modifyuserid,
       b.consultationtime,
       b.applyconsultationdeptid,
       b.askconsultationdeptid,
       b.consultationdeptid,
       b.casehistory_right,
       b.consultationorder_right,
       b.consultationidea_right,
       b.consultationdate,
       b.maindoctorid,
       b.otherhospital_right,
       c1.deptname_vchr as applydeptname,
       c2.deptname_vchr as askdeptname,
       c3.deptname_vchr as deptname,
       (select lastname_vchr
          from (select lastname_vchr, empid_chr, isemployee_int, empno_chr
                  from t_bse_employee
                 where status_int <> -1
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = b.maindoctorid
           and rownum = 1) as maindocname
  from consultationrecord a
 inner join consultationrecordcontent b on b.inpatientid = a.inpatientid
                                       and b.inpatientdate =
                                           a.inpatientdate
                                       and b.opendate = a.opendate
  left outer join t_bse_deptdesc c1 on b.applyconsultationdeptid =
                                       c1.deptid_chr
  left outer join t_bse_deptdesc c2 on b.askconsultationdeptid =
                                       c2.deptid_chr
  left outer join t_bse_deptdesc c3 on b.consultationdeptid = c3.deptid_chr
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 0
   and b.modifydate = (select max(modifydate)
                         from consultationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";


        /// <summary>
        /// 在ConsultationRecordDoctor中获取指定表单和ModifyDate的申请医师签名。
        /// </summary>
        private const string c_strGetDoctorContentSQL1 = @"select sub2.employeeid,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,consultationrecorddoctor c
                 where e.empno_chr = c.employeeid
                    and c.employeeflag <> 1
                   and e.status_int <> -1
                   and c.inpatientid = ?
                   and c.inpatientdate = ?
                   and c.opendate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = sub2.employeeid
           and rownum = 1) firstname
  from consultationrecord a, consultationrecorddoctor sub2
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and sub2.employeeflag <> 1
   and a.status = 0
   and sub2.inpatientid = a.inpatientid
   and sub2.inpatientdate = a.inpatientdate
   and sub2.opendate = a.opendate
   and sub2.modifydate = (select max(modifydate)
                            from consultationrecorddoctor
                           where inpatientid = a.inpatientid
                             and inpatientdate = a.inpatientdate
                             and opendate = a.opendate
                             and employeeflag <> 1)";
        /// <summary>
        /// 在ConsultationRecordDoctor中获取指定表单和ModifyDate的会诊医师签名。
        /// </summary>
        private const string c_strGetDoctorContentSQL2 = @"select sub2.employeeid,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,consultationrecorddoctor c
                 where e.empno_chr = c.employeeid
                   and e.status_int <> -1
                   and c.employeeflag <> 0
      and c.inpatientid = ?
   and c.inpatientdate = ?
   and c.opendate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = sub2.employeeid
           and rownum = 1) firstname
  from consultationrecord a, consultationrecorddoctor sub2
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and sub2.employeeflag <> 0
   and a.status = 0
   and sub2.inpatientid = a.inpatientid
   and sub2.inpatientdate = a.inpatientdate
   and sub2.opendate = a.opendate
   and sub2.modifydate = (select max(modifydate)
                            from consultationrecorddoctor
                           where inpatientid = a.inpatientid
                             and inpatientdate = a.inpatientdate
                             and opendate = a.opendate
                             and employeeflag <> 0)
";
        /// <summary>
        /// 在ConsultationRecordDoctor中获取指定删除表单和ModifyDate的申请医师签名。
        /// </summary>
        private const string c_strGetDeleteRecordDoctorSQL1 = @"select sub2.employeeid,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,consultationrecorddoctor c
                 where e.empno_chr = c.employeeid
                    and c.employeeflag <> 1
                   and e.status_int <> -1
                   and c.inpatientid = ?
                   and c.inpatientdate = ?
                   and c.opendate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = sub2.employeeid
           and rownum = 1) lastname_vchr
  from consultationrecord a, consultationrecorddoctor sub2
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and sub2.employeeflag = 0
   and a.status = 1
   and sub2.inpatientid = a.inpatientid
   and sub2.inpatientdate = a.inpatientdate
   and sub2.opendate = a.opendate
   and sub2.modifydate = (select max(modifydate)
                            from consultationrecorddoctor
                           where inpatientid = a.inpatientid
                             and inpatientdate = a.inpatientdate
                             and opendate = a.opendate
                             and employeeflag = 0)";
        /// <summary>
        /// 在ConsultationRecordDoctor中获取指定删除表单和ModifyDate的会诊医师签名。
        /// </summary>
        private const string c_strGetDeleteRecordDoctorSQL2 = @"select sub2.employeeid,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,consultationrecorddoctor c
                 where e.empno_chr = c.employeeid
                   and e.status_int <> -1
                   and c.employeeflag <> 0
      and c.inpatientid = ?
   and c.inpatientdate = ?
   and c.opendate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = sub2.employeeid
           and rownum = 1) firstname
  from consultationrecord a, consultationrecorddoctor sub2
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and sub2.employeeflag = 1
   and a.status = 1
   and sub2.inpatientid = a.inpatientid
   and sub2.inpatientdate = a.inpatientdate
   and sub2.opendate = a.opendate
   and sub2.modifydate = (select max(modifydate)
                            from consultationrecorddoctor
                           where inpatientid = a.inpatientid
                             and inpatientdate = a.inpatientdate
                             and opendate = a.opendate
                             and employeeflag = 1)";
        /// <summary>
        /// 从 ConsultationRecord中获取指定时间的表单
        /// InPatientID ,InPatientDate ,CreateDate,Status = 0
        /// </summary>
        private const string c_strCheckCreateDateSQL = "select createuserid,opendate from  consultationrecord where inpatientid = ? and inpatientdate= ? and createdate= ? and status=0";

        //		/// <summary>
        //		/// 从ConsultationRecord获取已经存在记录的主要信息,获取修改表单的主要信息
        //		/// </summary>
        //		private const string c_strGetExistInfoSQL= "";

        /// <summary>
        /// 从ConsultationRecordContent获取指定表单的最后修改时间。
        /// </summary>
        private const string c_strCheckLastModifyRecordSQL = @"select b.modifydate,b.modifyuserid from consultationrecord a,consultationrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from consultationrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

        //		/// <summary>
        //		/// 从ConsultationRecordContent获取修改表单的主要信息。
        //		/// </summary>
        //		private const string c_strGetModifyRecordSQL= "";

        /// <summary>
        /// 从ConsultationRecord获取删除表单的主要信息。
        /// </summary>
        private const string c_strGetDeleteRecordSQL = "select deactiveddate,deactivedoperatorid from consultationrecord where inpatientid = ? and inpatientdate= ? and opendate= ? and status=1 ";

        /// <summary>
        /// 添加记录到ConsultationRecord
        /// </summary>
        private const string c_strAddNewRecordSQL = @"insert into  consultationrecord(inpatientid,inpatientdate,opendate,createdate,createuserid,ifconfirm,confirmreason,confirmreasonxml,status,casehistory,casehistoryxml,consultationorder,consultationorderxml,consultationidea,consultationideaxml,otherhospital,otherhospitalxml,hasreplied) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 添加记录到ConsultationRecordContent
        /// </summary>
        private const string c_strAddNewRecordContentSQL = @"insert into  consultationrecordcontent(inpatientid,inpatientdate,opendate,modifydate,modifyuserid,consultationtime,applyconsultationdeptid,askconsultationdeptid,consultationdeptid,casehistory_right,consultationorder_right,consultationidea_right,consultationdate,maindoctorid,otherhospital_right) 
				values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

        /// <summary>
        /// 添加住院医师到ConsultationRecordDoctor
        /// </summary>
        private const string c_strAddNewRecordDoctorSQL1 = @"insert into  consultationrecorddoctor(inpatientid,inpatientdate,opendate,modifydate,employeeid,employeeflag) 
								values(?,?,?,?,?,0)";

        /// <summary>
        /// 添加会诊医师到ConsultationRecordDoctor
        /// </summary>
        private const string c_strAddNewRecordDoctorSQL2 = @"insert into  consultationrecorddoctor(inpatientid,inpatientdate,opendate,modifydate,employeeid,employeeflag) 
								values(?,?,?,?,?,1)";

        /// <summary>
        /// 添加既是申请医师又是会诊医师到ConsultationRecordDoctor
        /// </summary>
        private const string c_strAddNewRecordDoctorSQL3 = @"insert into  consultationrecorddoctor(inpatientid,inpatientdate,opendate,modifydate,employeeid,employeeflag) 
								values(?,?,?,?,?,2)";

        /// <summary>
        /// 修改记录到ConsultationRecord
        /// </summary>
        private const string c_strModifyRecordSQL = "update consultationrecord set casehistory=?,casehistoryxml=?,consultationorder=?,consultationorderxml=?,consultationidea=?,consultationideaxml=?,otherhospital=?,otherhospitalxml=?,hasreplied=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";//IfConfirm=? ,ConfirmReason=? ,ConfirmReasonXML=?

        /// <summary>
        /// 修改记录到ConsultationRecordContent
        /// </summary>
        private const string c_strModifyRecordContentSQL = c_strAddNewRecordContentSQL;

        /// <summary>
        /// 修改申请医师记录到ConsultationRecordRecordDoctor
        /// </summary>
        private const string c_strModifyRecordDoctorSQL1 = c_strAddNewRecordDoctorSQL1;

        /// <summary>
        /// 修改会诊医师记录到ConsultationRecordRecordDoctor
        /// </summary>
        private const string c_strModifyRecordDoctorSQL2 = c_strAddNewRecordDoctorSQL2;

        /// <summary>
        /// 修改既是申请医师又是会诊医师记录到ConsultationRecordRecordDoctor
        /// </summary>
        private const string c_strModifyRecordDoctorSQL3 = c_strAddNewRecordDoctorSQL3;

        /// <summary>
        /// 设置ConsultationRecord中删除记录的信息
        /// </summary>
        private const string c_strDeleteRecordSQL = "update consultationrecord set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        /// <summary>
        /// 从ConsultationRecord和ConsultationRecordContent获取ModifyDate和FirstPrintDate
        /// </summary>
        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select a.firstprintdate,b.modifydate from consultationrecord a,consultationrecordcontent b where a.inpatientid = ? and a.inpatientdate= ? and a.opendate= ? and a.status=0 and b.inpatientid=a.inpatientid and b.inpatientdate=a.inpatientdate and b.opendate=a.opendate and
						b.modifydate=(select max(modifydate) from consultationrecordcontent where inpatientid=a.inpatientid and inpatientdate=a.inpatientdate and opendate=a.opendate)";

        /// <summary>
        /// 更新ConsultationRecord中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = "update  consultationrecord set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        /// <summary>
        /// 从ConsultationRecord获取指定病人的所有指定删除者删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListSQL = "select createdate,opendate from consultationrecord where inpatientid = ? and inpatientdate= ? and deactivedoperatorid= ? and status=1";

        /// <summary>
        /// 从ConsultationRecord获取指定病人的所有已经删除的记录时间。
        /// 用 InPatientID ,InPatientDate ,Status = 1 查得所有可用的CreateDate和OpenDate时间
        /// </summary>
        private const string c_strGetDeleteRecordTimeListAllSQL = "select createdate,opendate from consultationrecord where inpatientid = ? and inpatientdate= ? and status=1";

        private const string c_strGetDeleteRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.casehistory,
       a.casehistoryxml,
       a.consultationorder,
       a.consultationorderxml,
       a.consultationidea,
       a.consultationideaxml,
       a.otherhospitalxml,
       a.otherhospital,
       a.hasreplied,
       b.modifydate,
       b.modifyuserid,
       b.consultationtime,
       b.applyconsultationdeptid,
       b.askconsultationdeptid,
       b.consultationdeptid,
       b.casehistory_right,
       b.consultationorder_right,
       b.consultationidea_right,
       b.consultationdate,
       b.maindoctorid,
       b.otherhospital_right,
       c1.deptname_vchr as applydeptname,
       c2.deptname_vchr as askdeptname,
       c3.deptname_vchr as deptname,
       (select lastname_vchr
          from (select lastname_vchr, empid_chr, isemployee_int, empno_chr
                  from t_bse_employee
                 where status_int <> -1
                 order by isemployee_int desc, empid_chr desc)
         where empno_chr = b.maindoctorid
           and rownum = 1) maindocname
  from consultationrecord a
 inner join consultationrecordcontent b on b.inpatientid = a.inpatientid
                                       and b.inpatientdate =
                                           a.inpatientdate
                                       and b.opendate = a.opendate
  left outer join t_bse_deptdesc c1 on b.applyconsultationdeptid =
                                       c1.deptid_chr
  left outer join t_bse_deptdesc c2 on b.askconsultationdeptid =
                                       c2.deptid_chr
  left outer join t_bse_deptdesc c3 on b.consultationdeptid = c3.deptid_chr
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.opendate = ?
   and a.status = 1
   and b.modifydate = (select max(modifydate)
                         from consultationrecordcontent
                        where inpatientid = a.inpatientid
                          and inpatientdate = a.inpatientdate
                          and opendate = a.opendate)";

//        private const string c_strGetUnSignContentSQL = @"select v2.* from consultationrecorddoctor t1 right outer join
//(select cr.*,crc.modifydate,crc.modifyuserid,crc.consultationtime,applyconsultationdeptid,crc.askconsultationdeptid,
//crc.consultationdeptid,crc.casehistory_right,crc.consultationorder_right,crc.consultationidea_right,crc.otherhospital_right,crc.consultationdate,
//crc.maindoctorid,c1.deptname as applydeptname,c2.deptname as askdeptname,c3.deptname as deptname,f_getempnamebyno(crc.maindoctorid) as maindocname
//from consultationrecord cr,
//consultationrecordcontent crc,dept_desc c1,dept_desc c2,dept_desc c3,
//(select cr.inpatientid,cr.inpatientdate,cr.opendate,cr.createdate,max(crc.modifydate)  as maxmodifydate from 
//consultationrecord cr,consultationrecordcontent crc
//where crc.applyconsultationdeptid=?
//and cr.inpatientid=crc.inpatientid
//and cr.inpatientdate=crc.inpatientdate
//and cr.opendate=crc.opendate
//and cr.status=0 group by cr.inpatientid,cr.inpatientdate,cr.opendate,cr.createdate) v1
//where
//crc.applyconsultationdeptid=?
//and cr.inpatientid=crc.inpatientid
//and cr.inpatientdate=crc.inpatientdate
//and cr.opendate=crc.opendate
//and cr.status=0
//and crc.applyconsultationdeptid = c1.deptid
//and crc.askconsultationdeptid = c2.deptid
//and crc.consultationdeptid = c3.deptid 
//and cr.inpatientid=v1.inpatientid
//and cr.inpatientdate=v1.inpatientdate
//and cr.opendate=v1.opendate
//and v1.maxmodifydate=crc.modifydate
//and crc.maindoctorid is not null) v2
//on 
//t1.inpatientid=v2.inpatientid
//and t1.inpatientdate=v2.inpatientdate
//and t1.opendate=v2.opendate
//and t1.modifydate=v2.modifydate
//and t1.employeeflag = '1'
//where t1.employeeid is null
//order by v2.askdeptname,v2.consultationdate";

        /// <summary>
        /// 获取病人的该记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsConsultationService", "m_lngGetRecordTimeList");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);


                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    DataRow objRow = dtbValue.Rows[0];
                    clsConsultationRecordContent objRecordContent = new clsConsultationRecordContent();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(objRow["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(objRow["MODIFYDATE"].ToString());

                    if (objRow["FIRSTPRINTDATE"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();
                    if (objRow["IFCONFIRM"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(objRow["IFCONFIRM"].ToString());
                    if (objRow["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(objRow["STATUS"].ToString());
                    objRecordContent.m_strConfirmReason = objRow["CONFIRMREASON"].ToString();
                    objRecordContent.m_strConfirmReasonXML = objRow["CONFIRMREASONXML"].ToString();

                    objRecordContent.m_intConsultationTime = int.Parse(objRow["CONSULTATIONTIME"].ToString());

                    objRecordContent.m_strApplyConsultationDeptID = objRow["APPLYCONSULTATIONDEPTID"].ToString();
                    objRecordContent.m_strApplyConsultationDeptName = objRow["APPLYDEPTNAME"].ToString().Trim();

                    objRecordContent.m_strAskConsultationDeptID = objRow["ASKCONSULTATIONDEPTID"].ToString();
                    objRecordContent.m_strAskConsultationDeptName = objRow["ASKDEPTNAME"].ToString().Trim();

                    objRecordContent.m_strConsultationDeptID = objRow["CONSULTATIONDEPTID"].ToString();
                    objRecordContent.m_strConsultationDeptName = objRow["DEPTNAME"].ToString().Trim();

                    objRecordContent.m_strCaseHistory_Right = objRow["CASEHISTORY_RIGHT"].ToString();
                    objRecordContent.m_strCaseHistory = objRow["CASEHISTORY"].ToString();
                    objRecordContent.m_strCaseHistoryXml = objRow["CASEHISTORYXML"].ToString();

                    objRecordContent.m_strConsultationOrder_Right = objRow["CONSULTATIONORDER_RIGHT"].ToString();
                    objRecordContent.m_strConsultationOrder = objRow["CONSULTATIONORDER"].ToString();
                    objRecordContent.m_strConsultationOrderXml = objRow["CONSULTATIONORDERXML"].ToString();

                    objRecordContent.m_strConsultationIdea_Right = objRow["CONSULTATIONIDEA_RIGHT"].ToString();
                    objRecordContent.m_strConsultationIdea = objRow["CONSULTATIONIDEA"].ToString();
                    objRecordContent.m_strConsultationIdeaXml = objRow["CONSULTATIONIDEAXML"].ToString();

                    objRecordContent.m_strOtherHospital = objRow["OTHERHOSPITAL"].ToString();
                    objRecordContent.m_strOtherHospital_RIGHT = objRow["OTHERHOSPITAL_RIGHT"].ToString();
                    objRecordContent.m_strOtherHospitalXML = objRow["OTHERHOSPITALXML"].ToString();

                    objRecordContent.m_dtmConsultationDate = DateTime.Parse(objRow["CONSULTATIONDATE"].ToString());

                    objRecordContent.m_strMainDoctorID = objRow["MAINDOCTORID"].ToString().Trim();
                    objRecordContent.m_strMainDoctorName = objRow["MainDocName"].ToString();

                    if (objRow["HASREPLIED"] != DBNull.Value)
                    {
                        objRecordContent.m_intHASREPLIED = Convert.ToInt32(objRow["HASREPLIED"]);
                    }
                    else
                    {
                        objRecordContent.m_intHASREPLIED = 0;
                    }

                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                    objDPArr[3].Value = p_strInPatientID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[5].DbType = DbType.DateTime;
                    objDPArr[5].Value = DateTime.Parse(p_strOpenDate);

                    long lngRes2 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL1, ref dtbValue, objDPArr);
                    //从DataTable.Rows中获取结果，赋给申请医师
                    if (lngRes2 > 0 && dtbValue.Rows.Count > 0)
                    {
                        objRecordContent.m_strRequestDoctorIDArr = new string[dtbValue.Rows.Count];
                        objRecordContent.m_strRequestDoctorNameArr = new string[dtbValue.Rows.Count];
                        for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                        {
                            objRecordContent.m_strRequestDoctorIDArr[i] = dtbValue.Rows[i]["EMPLOYEEID"].ToString();
                            objRecordContent.m_strRequestDoctorNameArr[i] = dtbValue.Rows[i]["FIRSTNAME"].ToString().Trim();
                        }
                    }

                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                    objDPArr[3].Value = p_strInPatientID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[5].DbType = DbType.DateTime;
                    objDPArr[5].Value = DateTime.Parse(p_strOpenDate);

                    long lngRes3 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL2, ref dtbValue, objDPArr);
                    //从DataTable.Rows中获取结果，赋给会诊医师
                    if (lngRes3 > 0 && dtbValue.Rows.Count > 0)
                    {
                        objRecordContent.m_strConsultationDoctorIDArr = new string[dtbValue.Rows.Count];
                        objRecordContent.m_strConsultationDoctorNameArr = new string[dtbValue.Rows.Count];
                        for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                        {
                            objRecordContent.m_strConsultationDoctorIDArr[i] = dtbValue.Rows[i]["EMPLOYEEID"].ToString();
                            objRecordContent.m_strConsultationDoctorNameArr[i] = dtbValue.Rows[i]["FIRSTNAME"].ToString().Trim();
                        }
                    }

                    p_objRecordContent = objRecordContent;
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
        /// 查看是否有相同的记录时间
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo">若有相同记录,返回该相同记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //查看DataTable.Rows.Count
                //如果等于1，表示已经有该CreateDate，并且不是删除的记录。
                //获取该记录的信息，赋值到p_objModifyInfo中。返回值使用Record_Already_Exist
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["OPENDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Exist;
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
        /// 保存记录到数据库。添加主表,添加子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数                              
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                clsConsultationRecordContent objContent = (clsConsultationRecordContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(18, out objDPArr);

                objDPArr[0].Value = objContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = objContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objContent.m_dtmOpenDate;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = objContent.m_dtmCreateDate;
                objDPArr[4].Value = objContent.m_strCreateUserID;
                objDPArr[5].Value = objContent.m_bytIfConfirm;
                if (objContent.m_strConfirmReason == null)
                    objDPArr[6].Value = DBNull.Value;
                else
                    objDPArr[6].Value = objContent.m_strConfirmReason;
                if (objContent.m_strConfirmReasonXML == null)
                    objDPArr[7].Value = DBNull.Value;
                else
                    objDPArr[7].Value = objContent.m_strConfirmReasonXML;
                objDPArr[8].Value = 0;
                objDPArr[9].Value = objContent.m_strCaseHistory;
                objDPArr[10].Value = objContent.m_strCaseHistoryXml;
                objDPArr[11].Value = objContent.m_strConsultationOrder;
                objDPArr[12].Value = objContent.m_strConsultationOrderXml;
                objDPArr[13].Value = objContent.m_strConsultationIdea;
                objDPArr[14].Value = objContent.m_strConsultationIdeaXml;
                objDPArr[15].Value = objContent.m_strOtherHospital;
                objDPArr[16].Value = objContent.m_strOtherHospitalXML;
                objDPArr[17].Value = objContent.m_intHASREPLIED;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(15, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_intConsultationTime;
                if (objContent.m_strApplyConsultationDeptID == null)
                    objDPArr2[6].Value = "";
                else
                    objDPArr2[6].Value = objContent.m_strApplyConsultationDeptID.Trim();
                if (objContent.m_strAskConsultationDeptID == null)
                    objDPArr2[7].Value = "";
                else
                    objDPArr2[7].Value = objContent.m_strAskConsultationDeptID.Trim();
                if (objContent.m_strConsultationDeptID == null)
                    objDPArr2[8].Value = "";
                else
                    objDPArr2[8].Value = objContent.m_strConsultationDeptID.Trim();
                objDPArr2[9].Value = objContent.m_strCaseHistory_Right;
                objDPArr2[10].Value = objContent.m_strConsultationOrder_Right;
                objDPArr2[11].Value = objContent.m_strConsultationIdea_Right;
                objDPArr2[12].DbType = DbType.DateTime;
                objDPArr2[12].Value = objContent.m_dtmConsultationDate;
                if (objContent.m_strMainDoctorID == null)
                    objDPArr2[13].Value = "";
                else
                    objDPArr2[13].Value = objContent.m_strMainDoctorID.Trim();
                objDPArr2[14].Value = objContent.m_strOtherHospital_RIGHT;


                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_strRequestDoctorIDArr != null)
                {


                    for (int j = 0 ; j < objContent.m_strRequestDoctorIDArr.Length ; j++)	//当j>=objContent.m_strRequestDoctorIDArr.Length时是不用执行的
                    {

                        IDataParameter[] objDPArr3 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr3[2].DbType = DbType.DateTime;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;

                        objDPArr3[4].Value = objContent.m_strRequestDoctorIDArr[j];

                        if (objContent.m_strConsultationDoctorIDArr != null)
                        {
                            bool blnIsEqual = false;
                            for (int k = 0 ; k < objContent.m_strConsultationDoctorIDArr.Length ; k++)
                            {

                                if (objContent.m_strRequestDoctorIDArr[j] == objContent.m_strConsultationDoctorIDArr[k])
                                {
                                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordDoctorSQL3, ref lngEff, objDPArr3);
                                    if (lngRes <= 0) return lngRes;
                                    blnIsEqual = true;
                                    break;
                                }
                            }

                            if (!blnIsEqual)
                            {

                                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordDoctorSQL1, ref lngEff, objDPArr3);
                                if (lngRes <= 0) return lngRes;
                            }
                        }
                        else
                        {
                            lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordDoctorSQL1, ref lngEff, objDPArr3);
                            if (lngRes <= 0) return lngRes;
                        }

                    }
                }

                if (objContent.m_strConsultationDoctorIDArr != null)
                {



                    for (int j = 0 ; j < objContent.m_strConsultationDoctorIDArr.Length ; j++)
                    {


                        IDataParameter[] objDPArr4 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr4);

                        objDPArr4[0].Value = objContent.m_strInPatientID;
                        objDPArr4[1].DbType = DbType.DateTime;
                        objDPArr4[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr4[2].DbType = DbType.DateTime;
                        objDPArr4[2].Value = objContent.m_dtmOpenDate;
                        objDPArr4[3].DbType = DbType.DateTime;
                        objDPArr4[3].Value = objContent.m_dtmModifyDate;

                        objDPArr4[4].Value = objContent.m_strConsultationDoctorIDArr[j];

                        bool blnIsEqual = false;

                        if (objContent.m_strRequestDoctorIDArr != null)
                        {

                            for (int k = 0 ; k < objContent.m_strRequestDoctorIDArr.Length ; k++)
                            {
                                if (objContent.m_strConsultationDoctorIDArr[j] == objContent.m_strRequestDoctorIDArr[k])
                                {
                                    blnIsEqual = true;
                                    break;
                                }
                            }
                            if (!blnIsEqual)
                            {
                                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordDoctorSQL2, ref lngEff, objDPArr4);
                                if (lngRes <= 0) return lngRes;

                            }
                        }

                        else
                        {
                            lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordDoctorSQL2, ref lngEff, objDPArr4);
                            if (lngRes <= 0) return lngRes;
                        }
                    }
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
        /// 查看当前记录是否最新的记录。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>		
        /// <param name="p_objModifyInfo">若当前记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //获取IDataParameter数组			
                //string strSQL = "select top 1 ModifyDate,ModifyUserID from ConsultationRecordContent Where InPatientID = ? and InPatientDate= ? and OpenDate= ? and Status=0 order by ModifyDate desc";


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

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
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

            }
            //返回
            return lngRes;

        }

        /// <summary>
        /// 把新修改的内容保存到数据库。更新主表,添加子表.
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			
                clsConsultationRecordContent objContent = (clsConsultationRecordContent)p_objRecordContent;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(12, out objDPArr);
                objDPArr[0].Value = objContent.m_strCaseHistory;
                objDPArr[1].Value = objContent.m_strCaseHistoryXml;
                objDPArr[2].Value = objContent.m_strConsultationOrder;
                objDPArr[3].Value = objContent.m_strConsultationOrderXml;
                objDPArr[4].Value = objContent.m_strConsultationIdea;
                objDPArr[5].Value = objContent.m_strConsultationIdeaXml;
                objDPArr[6].Value = objContent.m_strOtherHospital;
                objDPArr[7].Value = objContent.m_strOtherHospitalXML;
                objDPArr[8].Value = objContent.m_intHASREPLIED;
                objDPArr[9].Value = objContent.m_strInPatientID;
                objDPArr[10].DbType = DbType.DateTime;
                objDPArr[10].Value = objContent.m_dtmInPatientDate;
                objDPArr[11].DbType = DbType.DateTime;
                objDPArr[11].Value = objContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(15, out objDPArr2);


                objDPArr2[0].Value = objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = objContent.m_dtmModifyDate;
                objDPArr2[4].Value = objContent.m_strModifyUserID;
                objDPArr2[5].Value = objContent.m_intConsultationTime;
                objDPArr2[6].Value = objContent.m_strApplyConsultationDeptID;
                objDPArr2[7].Value = objContent.m_strAskConsultationDeptID;
                objDPArr2[8].Value = objContent.m_strConsultationDeptID;
                objDPArr2[9].Value = objContent.m_strCaseHistory_Right;
                objDPArr2[10].Value = objContent.m_strConsultationOrder_Right;
                objDPArr2[11].Value = objContent.m_strConsultationIdea_Right;
                objDPArr2[12].DbType = DbType.DateTime;
                objDPArr2[12].Value = objContent.m_dtmConsultationDate;
                objDPArr2[13].Value = objContent.m_strMainDoctorID;
                objDPArr2[14].Value = objContent.m_strOtherHospital_RIGHT;


                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordContentSQL, ref lngEff, objDPArr2);
                if (lngRes <= 0) return lngRes;

                if (objContent.m_strRequestDoctorIDArr != null)
                {

                    for (int j = 0 ; j < objContent.m_strRequestDoctorIDArr.Length ; j++)	//当j>=objContent.m_strRequestDoctorIDArr.Length时是不用执行的
                    {

                        IDataParameter[] objDPArr3 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr3);

                        objDPArr3[0].Value = objContent.m_strInPatientID;
                        objDPArr3[1].DbType = DbType.DateTime;
                        objDPArr3[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr2[2].DbType = DbType.DateTime;
                        objDPArr3[2].Value = objContent.m_dtmOpenDate;
                        objDPArr3[3].DbType = DbType.DateTime;
                        objDPArr3[3].Value = objContent.m_dtmModifyDate;
                        objDPArr3[4].Value = objContent.m_strRequestDoctorIDArr[j];

                        bool blnIsEqual = false;
                        if (objContent.m_strConsultationDoctorIDArr != null)
                        {
                            for (int k = 0 ; k < objContent.m_strConsultationDoctorIDArr.Length ; k++)
                            {
                                if (objContent.m_strRequestDoctorIDArr[j] == objContent.m_strConsultationDoctorIDArr[k])
                                {
                                    lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordDoctorSQL3, ref lngEff, objDPArr3);
                                    if (lngRes <= 0) return lngRes;
                                    blnIsEqual = true;
                                    break;
                                }
                            }
                            if (!blnIsEqual)
                            {
                                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordDoctorSQL1, ref lngEff, objDPArr3);
                                if (lngRes <= 0) return lngRes;
                            }
                        }
                        else
                        {
                            lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordDoctorSQL1, ref lngEff, objDPArr3);
                            if (lngRes <= 0) return lngRes;
                        }

                    }
                }

                if (objContent.m_strConsultationDoctorIDArr != null)
                {




                    for (int j = 0 ; j < objContent.m_strConsultationDoctorIDArr.Length ; j++)
                    {
                        IDataParameter[] objDPArr4 = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr4);

                        objDPArr4[0].Value = objContent.m_strInPatientID;
                        objDPArr4[1].DbType = DbType.DateTime;
                        objDPArr4[1].Value = objContent.m_dtmInPatientDate;
                        objDPArr4[2].DbType = DbType.DateTime;
                        objDPArr4[2].Value = objContent.m_dtmOpenDate;
                        objDPArr4[3].DbType = DbType.DateTime;
                        objDPArr4[3].Value = objContent.m_dtmModifyDate;
                        objDPArr4[4].Value = objContent.m_strConsultationDoctorIDArr[j];

                        bool blnIsEqual = false;
                        if (objContent.m_strRequestDoctorIDArr != null)
                        {

                            for (int k = 0 ; k < objContent.m_strRequestDoctorIDArr.Length ; k++)
                            {
                                if (objContent.m_strConsultationDoctorIDArr[j] == objContent.m_strRequestDoctorIDArr[k])
                                {
                                    blnIsEqual = true;
                                    break;
                                }
                            }

                            if (!blnIsEqual)
                            {
                                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordDoctorSQL2, ref lngEff, objDPArr4);
                                if (lngRes <= 0) return lngRes;
                            }
                        }
                        else
                        {
                            lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordDoctorSQL2, ref lngEff, objDPArr4);
                            if (lngRes <= 0) return lngRes;
                        }
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

            }			//返回
            return lngRes;

        }

        /// <summary>
        /// 把记录从数据中“删除”。
        /// </summary>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);

                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
        protected override long m_lngGetModifyDateAndFirstPrintDate(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.Now;
            p_strFirstPrintDate = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
        /// 更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsConsultationService", "m_lngGetDeleteRecordTimeList");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //检查参数                              
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                    return (long)enmOperationResult.Parameter_Error;
                //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID.Trim();
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
                string strTmp = objEx.Message;
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
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strDeleteUserID">删除者ID</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsConsultationService", "m_lngGetDeleteRecordTimeList");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strDeleteUserID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
        /// 获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strCreateDateArr">用户填写的创建时间数组</param>
        /// <param name="p_strOpenDateArr">系统生成的开始时间数组</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateDateArr,
            out string[] p_strOpenDateArr)
        {
            p_strCreateDateArr = null;
            p_strOpenDateArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsConsultationService", "m_lngGetDeleteRecordTimeListAll");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordTimeListAllSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strCreateDateArr = new string[dtbValue.Rows.Count];
                    p_strOpenDateArr = new string[dtbValue.Rows.Count];
                    for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                    {
                        //设置结果
                        p_strCreateDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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
        /// 获取指定已经被删除记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strOpenDate">记录时间</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            clsHRPTableService p_objHRPServ,
            out clsTrackRecordContent p_objRecordContent)
        {
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "" || p_objHRPServ == null)
                    return (long)enmOperationResult.Parameter_Error;


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordContentSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    DataRow objRow = dtbValue.Rows[0];
                    clsConsultationRecordContent objRecordContent = new clsConsultationRecordContent();
                    objRecordContent.m_strInPatientID = p_strInPatientID;
                    objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
                    objRecordContent.m_dtmOpenDate = DateTime.Parse(p_strOpenDate);
                    objRecordContent.m_dtmCreateDate = DateTime.Parse(objRow["CREATEDATE"].ToString());
                    objRecordContent.m_dtmModifyDate = DateTime.Parse(objRow["MODIFYDATE"].ToString());

                    if (objRow["FIRSTPRINTDATE"].ToString() == "")
                        objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                    else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString());
                    objRecordContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
                    objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();
                    if (objRow["IFCONFIRM"].ToString() == "")
                        objRecordContent.m_bytIfConfirm = 0;
                    else objRecordContent.m_bytIfConfirm = Byte.Parse(objRow["IFCONFIRM"].ToString());
                    if (objRow["STATUS"].ToString() == "")
                        objRecordContent.m_bytStatus = 0;
                    else objRecordContent.m_bytStatus = Byte.Parse(objRow["STATUS"].ToString());
                    objRecordContent.m_strConfirmReason = objRow["CONFIRMREASON"].ToString();
                    objRecordContent.m_strConfirmReasonXML = objRow["CONFIRMREASONXML"].ToString();

                    objRecordContent.m_intConsultationTime = int.Parse(objRow["CONSULTATIONTIME"].ToString());

                    objRecordContent.m_strApplyConsultationDeptID = objRow["APPLYCONSULTATIONDEPTID"].ToString();
                    objRecordContent.m_strApplyConsultationDeptName = objRow["APPLYDEPTNAME"].ToString().Trim();

                    objRecordContent.m_strAskConsultationDeptID = objRow["ASKCONSULTATIONDEPTID"].ToString();
                    objRecordContent.m_strAskConsultationDeptName = objRow["ASKDEPTNAME"].ToString().Trim();

                    objRecordContent.m_strConsultationDeptID = objRow["CONSULTATIONDEPTID"].ToString();
                    objRecordContent.m_strConsultationDeptName = objRow["DEPTNAME"].ToString().Trim();

                    objRecordContent.m_strCaseHistory_Right = objRow["CASEHISTORY_RIGHT"].ToString();
                    objRecordContent.m_strCaseHistory = objRow["CASEHISTORY"].ToString();
                    objRecordContent.m_strCaseHistoryXml = objRow["CASEHISTORYXML"].ToString();

                    objRecordContent.m_strConsultationOrder_Right = objRow["CONSULTATIONORDER_RIGHT"].ToString();
                    objRecordContent.m_strConsultationOrder = objRow["CONSULTATIONORDER"].ToString();
                    objRecordContent.m_strConsultationOrderXml = objRow["CONSULTATIONORDERXML"].ToString();

                    objRecordContent.m_strConsultationIdea_Right = objRow["CONSULTATIONIDEA_RIGHT"].ToString();
                    objRecordContent.m_strConsultationIdea = objRow["CONSULTATIONIDEA"].ToString();
                    objRecordContent.m_strConsultationIdeaXml = objRow["CONSULTATIONIDEAXML"].ToString();

                    objRecordContent.m_strOtherHospital_RIGHT = objRow["OTHERHOSPITAL_RIGHT"].ToString();
                    objRecordContent.m_strOtherHospital = objRow["OTHERHOSPITAL"].ToString();
                    objRecordContent.m_strOtherHospitalXML = objRow["OTHERHOSPITALXML"].ToString();

                    if (objRow["HASREPLIED"] != DBNull.Value)
                    {
                        objRecordContent.m_intHASREPLIED = Convert.ToInt32(objRow["HASREPLIED"]);
                    }
                    else
                    {
                        objRecordContent.m_intHASREPLIED = 0;
                    }

                    objRecordContent.m_dtmConsultationDate = DateTime.Parse(objRow["CONSULTATIONDATE"].ToString());

                    objRecordContent.m_strMainDoctorID = objRow["MAINDOCTORID"].ToString().Trim();
                    objRecordContent.m_strMainDoctorName = objRow["MainDocName"].ToString();



                    //按顺序给IDataParameter赋值
                    //					for(int i=0;i<objDPArr.Length;i++)
                    //						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                    long lngRes2 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL1, ref dtbValue, objDPArr);
                    //从DataTable.Rows中获取结果，赋给申请医师
                    if (lngRes2 > 0 && dtbValue.Rows.Count > 0)
                    {
                        objRecordContent.m_strRequestDoctorIDArr = new string[dtbValue.Rows.Count];
                        objRecordContent.m_strRequestDoctorNameArr = new string[dtbValue.Rows.Count];
                        for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                        {
                            objRecordContent.m_strRequestDoctorIDArr[i] = dtbValue.Rows[i]["EMPLOYEEID"].ToString();
                            objRecordContent.m_strRequestDoctorNameArr[i] = dtbValue.Rows[i]["FIRSTNAME"].ToString();
                        }
                    }

                    //按顺序给IDataParameter赋值
                    //					for(int i=0;i<objDPArr.Length;i++)
                    //						objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
                    objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strOpenDate);

                    long lngRes3 = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL2, ref dtbValue, objDPArr);
                    //从DataTable.Rows中获取结果，赋给会诊医师
                    if (lngRes3 > 0 && dtbValue.Rows.Count > 0)
                    {
                        objRecordContent.m_strConsultationDoctorIDArr = new string[dtbValue.Rows.Count];
                        objRecordContent.m_strConsultationDoctorNameArr = new string[dtbValue.Rows.Count];
                        for (int i = 0 ; i < dtbValue.Rows.Count ; i++)
                        {
                            objRecordContent.m_strConsultationDoctorIDArr[i] = dtbValue.Rows[i]["EMPLOYEEID"].ToString();
                            objRecordContent.m_strConsultationDoctorNameArr[i] = dtbValue.Rows[i]["FIRSTNAME"].ToString();
                        }
                    }

                    p_objRecordContent = objRecordContent;
                }

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
            return lngRes;

        }

        #region mark
        /// <summary>
        /// 获取还没会诊的记录内容
        /// </summary>
        /// <param name="p_strDeptID">部门ID</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">会诊类</param>
        /// <returns></returns>
//        [AutoComplete]
//        public long m_lngGetUnSignContent(string p_strDeptID,
//            out clsConsultationRecordContent[] p_objRecordContentArr)
//        {
//            p_objRecordContentArr = null;
//            long lngRes = 0;
//            clsHRPTableService objHRPServ = new clsHRPTableService();
//            try
//            {
//                //检查参数
//                if (p_strDeptID == null)
//                    return (long)enmOperationResult.Parameter_Error;

//                //				clsHRPTableService objHRPServ = new clsHRPTableService();

//                ArrayList arlContent = new ArrayList();


//                IDataParameter[] objDPArr = null;
//                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

//                objDPArr[0].Value = p_strDeptID;
//                objDPArr[1].Value = p_strDeptID;
//                //生成DataTable
//                DataTable dtbValue = new DataTable();
//                //执行查询，填充结果到DataTable
//                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetUnSignContentSQL, ref dtbValue, objDPArr);
//                //从DataTable.Rows中获取结果
//                if (lngRes > 0 && dtbValue.Rows.Count > 0)
//                {
//                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
//                    {
//                        string strInPatientID = dtbValue.Rows[j]["INPATIENTID"].ToString();
//                        string strInPatientDate = dtbValue.Rows[j]["INPATIENTDATE"].ToString();
//                        string strOpenDate = dtbValue.Rows[j]["OPENDATE"].ToString();
//                        clsConsultationRecordContent objRecordContent = new clsConsultationRecordContent();
//                        objRecordContent.m_strInPatientID = strInPatientID;
//                        objRecordContent.m_dtmInPatientDate = DateTime.Parse(strInPatientDate);
//                        objRecordContent.m_dtmOpenDate = DateTime.Parse(strOpenDate);
//                        objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
//                        objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());

//                        if (dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString() == "")
//                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
//                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
//                        objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID"].ToString();
//                        objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();
//                        if (dtbValue.Rows[j]["IFCONFIRM"].ToString() == "")
//                            objRecordContent.m_bytIfConfirm = 0;
//                        else objRecordContent.m_bytIfConfirm = Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
//                        if (dtbValue.Rows[j]["STATUS"].ToString() == "")
//                            objRecordContent.m_bytStatus = 0;
//                        else objRecordContent.m_bytStatus = Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());
//                        objRecordContent.m_strConfirmReason = dtbValue.Rows[j]["CONFIRMREASON"].ToString();
//                        objRecordContent.m_strConfirmReasonXML = dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();

//                        objRecordContent.m_intConsultationTime = int.Parse(dtbValue.Rows[j]["CONSULTATIONTIME"].ToString());

//                        objRecordContent.m_strApplyConsultationDeptID = dtbValue.Rows[j]["APPLYCONSULTATIONDEPTID"].ToString();
//                        objRecordContent.m_strApplyConsultationDeptName = dtbValue.Rows[j]["APPLYDEPTNAME"].ToString().Trim();

//                        objRecordContent.m_strAskConsultationDeptID = dtbValue.Rows[j]["ASKCONSULTATIONDEPTID"].ToString();
//                        objRecordContent.m_strAskConsultationDeptName = dtbValue.Rows[j]["ASKDEPTNAME"].ToString().Trim();

//                        objRecordContent.m_strConsultationDeptID = dtbValue.Rows[j]["CONSULTATIONDEPTID"].ToString();
//                        objRecordContent.m_strConsultationDeptName = dtbValue.Rows[j]["DEPTNAME"].ToString().Trim();

//                        objRecordContent.m_strCaseHistory_Right = dtbValue.Rows[j]["CASEHISTORY_RIGHT"].ToString();
//                        objRecordContent.m_strCaseHistory = dtbValue.Rows[j]["CASEHISTORY"].ToString();
//                        objRecordContent.m_strCaseHistoryXml = dtbValue.Rows[j]["CASEHISTORYXML"].ToString();

//                        objRecordContent.m_strConsultationOrder_Right = dtbValue.Rows[j]["CONSULTATIONORDER_RIGHT"].ToString();
//                        objRecordContent.m_strConsultationOrder = dtbValue.Rows[j]["CONSULTATIONORDER"].ToString();
//                        objRecordContent.m_strConsultationOrderXml = dtbValue.Rows[j]["CONSULTATIONORDERXML"].ToString();

//                        objRecordContent.m_strConsultationIdea_Right = dtbValue.Rows[j]["CONSULTATIONIDEA_RIGHT"].ToString();
//                        objRecordContent.m_strConsultationIdea = dtbValue.Rows[j]["CONSULTATIONIDEA"].ToString();
//                        objRecordContent.m_strConsultationIdeaXml = dtbValue.Rows[j]["CONSULTATIONIDEAXML"].ToString();

//                        objRecordContent.m_strOtherHospital_RIGHT = dtbValue.Rows[j]["OTHERHOSPITAL_RIGHT"].ToString();
//                        objRecordContent.m_strOtherHospital = dtbValue.Rows[j]["OTHERHOSPITAL"].ToString();
//                        objRecordContent.m_strOtherHospitalXML = dtbValue.Rows[j]["OTHERHOSPITALXML"].ToString();

//                        objRecordContent.m_dtmConsultationDate = DateTime.Parse(dtbValue.Rows[j]["CONSULTATIONDATE"].ToString());

//                        objRecordContent.m_strMainDoctorID = dtbValue.Rows[j]["MAINDOCTORID"].ToString().Trim();
//                        objRecordContent.m_strMainDoctorName = dtbValue.Rows[j]["MainDocName"].ToString();

//                        string strGetDoctorContentSQL = @" select sub2.EmployeeID, F_GETEMPNAMEBYNO(sub2.EmployeeID) as  FirstName,sub2.EmployeeFlag
//															from ConsultationRecord       a,
//																ConsultationRecordDoctor sub2
//															where a.InPatientID = ?
//															and a.InPatientDate = ?
//															and a.OpenDate = ?
//															and a.Status = 0
//															and sub2.InPatientID = a.InPatientID
//															and sub2.InPatientDate = a.InPatientDate
//															and sub2.OpenDate = a.OpenDate
//															and sub2.ModifyDate =
//																(select Max(ModifyDate)
//																	from ConsultationRecordDoctor
//																	Where InPatientID = a.InPatientID
//																	and InPatientDate = a.InPatientDate
//																	and OpenDate = a.OpenDate) ";

//                        IDataParameter[] objDPArr2 = null;
//                        objHRPServ.CreateDatabaseParameter(3, out objDPArr2);

//                        objDPArr2[0].Value = strInPatientID;
//                        objDPArr2[1].DbType = DbType.DateTime;
//                        objDPArr2[1].Value = DateTime.Parse(strInPatientDate);
//                        objDPArr2[2].DbType = DbType.DateTime;
//                        objDPArr2[2].Value = DateTime.Parse(strOpenDate);

//                        DataTable dtbValue2 = new DataTable();

//                        long lngRes2 = objHRPServ.lngGetDataTableWithParameters(strGetDoctorContentSQL, ref dtbValue2, objDPArr2);
//                        //从DataTable.Rows中获取结果，赋给申请医师
//                        if (lngRes2 > 0 && dtbValue2.Rows.Count > 0)
//                        {
//                            DataView dtDoc = new DataView(dtbValue2);

//                            dtDoc.RowFilter = "EmployeeFlag <> 1";
//                            if (dtDoc.Count > 0)
//                            {
//                                int ReqCount = dtDoc.Count;
//                                objRecordContent.m_strRequestDoctorIDArr = new string[ReqCount];
//                                objRecordContent.m_strRequestDoctorNameArr = new string[ReqCount];
//                                for (int i = 0; i < ReqCount; i++)
//                                {
//                                    objRecordContent.m_strRequestDoctorIDArr[i] = dtDoc[i]["EMPLOYEEID"].ToString();
//                                    objRecordContent.m_strRequestDoctorNameArr[i] = dtDoc[i]["FIRSTNAME"].ToString().Trim();
//                                }
//                            }

//                            dtDoc = new DataView(dtbValue2);
//                            dtDoc.RowFilter = "EmployeeFlag <> 0";
//                            if (dtDoc.Count > 0)
//                            {
//                                int ConCount = dtDoc.Count;
//                                objRecordContent.m_strConsultationDoctorIDArr = new string[ConCount];
//                                objRecordContent.m_strConsultationDoctorNameArr = new string[ConCount];
//                                for (int i = 0; i < ConCount; i++)
//                                {
//                                    objRecordContent.m_strConsultationDoctorIDArr[i] = dtDoc[i]["EMPLOYEEID"].ToString();
//                                    objRecordContent.m_strConsultationDoctorNameArr[i] = dtDoc[i]["FIRSTNAME"].ToString().Trim();
//                                }
//                            }
//                        }

//                        #region 旧的获取申请医师
//                        //						IDataParameter[] objDPArr2 = new Oracle.DataAccess.Client.OracleParameter[3];
//                        //						//按顺序给IDataParameter赋值
//                        //						for(int i=0;i<objDPArr2.Length;i++)
//                        //							objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
//                        //IDataParameter[] objDPArr2 = null;
//                        //objHRPServ.CreateDatabaseParameter(3, out objDPArr2);

//                        //objDPArr2[0].Value = strInPatientID;
//                        //objDPArr2[1].DbType = DbType.DateTime;
//                        //objDPArr2[1].Value = DateTime.Parse(strInPatientDate);
//                        //objDPArr2[2].DbType = DbType.DateTime;
//                        //objDPArr2[2].Value = DateTime.Parse(strOpenDate);

//                        //DataTable dtbValue2 = new DataTable();

//                        //long lngRes2 = objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL1, ref dtbValue2, objDPArr2);
//                        ////从DataTable.Rows中获取结果，赋给申请医师
//                        //if (lngRes2 > 0 && dtbValue2.Rows.Count > 0)
//                        //{
//                        //    objRecordContent.m_strRequestDoctorIDArr = new string[dtbValue2.Rows.Count];
//                        //    objRecordContent.m_strRequestDoctorNameArr = new string[dtbValue2.Rows.Count];
//                        //    for (int i = 0 ; i < dtbValue2.Rows.Count ; i++)
//                        //    {
//                        //        objRecordContent.m_strRequestDoctorIDArr[i] = dtbValue2.Rows[i]["EMPLOYEEID"].ToString();
//                        //        objRecordContent.m_strRequestDoctorNameArr[i] = dtbValue2.Rows[i]["FIRSTNAME"].ToString().Trim();
//                        //    }
//                        //}

//                        ////按顺序给IDataParameter赋值
//                        ////						for(int i=0;i<objDPArr2.Length;i++)
//                        ////							objDPArr2[i]=new Oracle.DataAccess.Client.OracleParameter();
//                        //objHRPServ.CreateDatabaseParameter(3, out objDPArr2);
//                        //objDPArr2[0].Value = strInPatientID;
//                        //objDPArr2[1].DbType = DbType.DateTime;
//                        //objDPArr2[1].Value = DateTime.Parse(strInPatientDate);
//                        //objDPArr2[2].DbType = DbType.DateTime;
//                        //objDPArr2[2].Value = DateTime.Parse(strOpenDate);

//                        //long lngRes3 = objHRPServ.lngGetDataTableWithParameters(c_strGetDoctorContentSQL2, ref dtbValue2, objDPArr2);
//                        ////从DataTable.Rows中获取结果，赋给会诊医师
//                        //if (lngRes3 > 0 && dtbValue2.Rows.Count > 0)
//                        //{
//                        //    objRecordContent.m_strConsultationDoctorIDArr = new string[dtbValue2.Rows.Count];
//                        //    objRecordContent.m_strConsultationDoctorNameArr = new string[dtbValue2.Rows.Count];
//                        //    for (int i = 0 ; i < dtbValue2.Rows.Count ; i++)
//                        //    {
//                        //        objRecordContent.m_strConsultationDoctorIDArr[i] = dtbValue2.Rows[i]["EMPLOYEEID"].ToString();
//                        //        objRecordContent.m_strConsultationDoctorNameArr[i] = dtbValue2.Rows[i]["FIRSTNAME"].ToString().Trim();
//                        //    }
//                        //} 
//                        #endregion

//                        arlContent.Add(objRecordContent);
//                    }

//                    //返回结果到p_objRecordContentArr
//                    p_objRecordContentArr = (clsConsultationRecordContent[])arlContent.ToArray(typeof(clsConsultationRecordContent));
//                }
//                //				//返回
//                //				return lngRes;				
//            }
//            catch (Exception objEx)
//            {
//                string strTmp = objEx.Message;
//                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
//                bool blnRes = objLogger.LogError(objEx);
//            }

//            finally
//            {
//                //objHRPServ.Dispose();

//            }			//返回
//            return lngRes;



        //        }
        #endregion

        /// <summary>
        /// 获取还没会诊的记录内容
        /// </summary>
        /// <param name="p_strDeptIDArr">部门ID</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent">会诊类</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUnSignContent(string[] p_strDeptIDArr,
            out clsConsultationRecordContent[] p_objRecordContentArr)
        {
            p_objRecordContentArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strDeptIDArr == null || p_strDeptIDArr.Length <= 0)
                    return (long)enmOperationResult.Parameter_Error;

                string DeptSQL = string.Empty;

                System.Text.StringBuilder stbDept = new System.Text.StringBuilder(100);
                stbDept.Append("crc.applyconsultationdeptid = ?");
                //stbDept.Append(p_strDeptIDArr[0]);
                //stbDept.Append("'");
                for (int i = 1 ; i < p_strDeptIDArr.Length ; i++)
                {
                    stbDept.Append(" or crc.applyconsultationdeptid = ?");
                    //stbDept.Append(p_strDeptIDArr[i]);
                    //stbDept.Append("'");
                }

                DeptSQL = stbDept.ToString();

                #region 获取会诊记录

                string strGetSQL = @"select cr.inpatientid,
       cr.inpatientdate,
       cr.opendate,
       cr.createdate,
       cr.createuserid,
       cr.ifconfirm,
       cr.confirmreason,
       cr.confirmreasonxml,
       cr.firstprintdate,
       cr.deactiveddate,
       cr.deactivedoperatorid,
       cr.status,
       cr.casehistory,
       cr.casehistoryxml,
       cr.consultationorder,
       cr.consultationorderxml,
       cr.consultationidea,
       cr.consultationideaxml,
       cr.otherhospitalxml,
       cr.otherhospital,
       cr.hasreplied,
       crc.modifydate,
       crc.modifyuserid,
       crc.consultationtime,
       crc.applyconsultationdeptid,
       crc.askconsultationdeptid,
       crc.consultationdeptid,
       crc.casehistory_right,
       crc.consultationorder_right,
       crc.consultationidea_right,
       crc.otherhospital_right,
       crc.consultationdate,
       crc.maindoctorid,
       c1.deptname_vchr as applydeptname,
       c2.deptname_vchr as askdeptname,
       c3.deptname_vchr as deptname,
       f_getempnamebyno(crc.maindoctorid) as maindocname
  from consultationrecord        cr,
       consultationrecordcontent crc,
       t_bse_deptdesc            c1,
       t_bse_deptdesc            c2,
       t_bse_deptdesc            c3
 where (" + DeptSQL + @")
   and cr.inpatientid = crc.inpatientid
   and cr.inpatientdate = crc.inpatientdate
   and cr.opendate = crc.opendate
   and cr.status = 0
   and crc.applyconsultationdeptid = c1.deptid_chr
   and crc.askconsultationdeptid = c2.deptid_chr
   and crc.consultationdeptid = c3.deptid_chr
   and crc.modifydate =
       (select max(crc1.modifydate)
          from consultationrecordcontent crc1
         where crc1.inpatientid = crc.inpatientid
           and crc1.inpatientdate = crc.inpatientdate
           and crc.opendate = crc1.opendate)
   and crc.maindoctorid is not null
   and cr.hasreplied = 0
 order by askdeptname, crc.consultationdate";

                string strSqlGetEmp = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       sub2.employeeid,
       sub2.employeeflag,
       em.lastname_vchr
  from consultationrecord        a,
       consultationrecorddoctor  sub2,
       consultationrecordcontent crc,
       t_bse_employee            em
 where a.status = 0
   and sub2.inpatientid = a.inpatientid
   and sub2.inpatientdate = a.inpatientdate
   and sub2.opendate = a.opendate
   and crc.inpatientid = a.inpatientid
   and crc.inpatientdate = a.inpatientdate
   and crc.opendate = a.opendate
   and em.empno_chr = sub2.employeeid
   and crc.maindoctorid is not null
   and a.hasreplied = 0
   and (" + DeptSQL + @")
   and sub2.modifydate =
       (select max(crc1.modifydate)
          from consultationrecordcontent crc1
         where crc1.inpatientid = sub2.inpatientid
           and crc1.inpatientdate = sub2.inpatientdate
           and sub2.opendate = crc1.opendate)";
                #endregion
                ArrayList arlContent = new ArrayList();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(p_strDeptIDArr.Length, out objDPArr);
                //return -99;
                for (int y = 0; y < p_strDeptIDArr.Length; y++)
                {
                    objDPArr[y].DbType = DbType.String;
                    objDPArr[y].Value = p_strDeptIDArr[y];
                }
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetSQL, ref dtbValue, objDPArr);
                //-----------------
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(p_strDeptIDArr.Length, out objDPArr);

                for (int y = 0; y < p_strDeptIDArr.Length; y++)
                {
                    objDPArr[y].DbType = DbType.String;
                    objDPArr[y].Value = p_strDeptIDArr[y];
                }
                //生成DataTable
                DataTable dtbEmpValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSqlGetEmp, ref dtbEmpValue, objDPArr);
                //-------------------

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    DataRow objRow = null;
                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
                    {
                        objRow = dtbValue.Rows[j];
                        string strInPatientID = objRow["INPATIENTID"].ToString();
                        string strInPatientDate = objRow["INPATIENTDATE"].ToString();
                        string strOpenDate = objRow["OPENDATE"].ToString();
                        clsConsultationRecordContent objRecordContent = new clsConsultationRecordContent();
                        objRecordContent.m_strInPatientID = strInPatientID;
                        objRecordContent.m_dtmInPatientDate = DateTime.Parse(strInPatientDate);
                        objRecordContent.m_dtmOpenDate = DateTime.Parse(strOpenDate);
                        objRecordContent.m_dtmCreateDate = DateTime.Parse(objRow["CREATEDATE"].ToString());
                        objRecordContent.m_dtmModifyDate = DateTime.Parse(objRow["MODIFYDATE"].ToString());

                        if (objRow["FIRSTPRINTDATE"].ToString() == "")
                            objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else objRecordContent.m_dtmFirstPrintDate = DateTime.Parse(objRow["FIRSTPRINTDATE"].ToString());
                        objRecordContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
                        objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();
                        if (objRow["IFCONFIRM"].ToString() == "")
                            objRecordContent.m_bytIfConfirm = 0;
                        else objRecordContent.m_bytIfConfirm = Byte.Parse(objRow["IFCONFIRM"].ToString());
                        if (objRow["STATUS"].ToString() == "")
                            objRecordContent.m_bytStatus = 0;
                        else objRecordContent.m_bytStatus = Byte.Parse(objRow["STATUS"].ToString());
                        objRecordContent.m_strConfirmReason = objRow["CONFIRMREASON"].ToString();
                        objRecordContent.m_strConfirmReasonXML = objRow["CONFIRMREASONXML"].ToString();

                        objRecordContent.m_intConsultationTime = int.Parse(objRow["CONSULTATIONTIME"].ToString());

                        objRecordContent.m_strApplyConsultationDeptID = objRow["APPLYCONSULTATIONDEPTID"].ToString();
                        objRecordContent.m_strApplyConsultationDeptName = objRow["APPLYDEPTNAME"].ToString().Trim();

                        objRecordContent.m_strAskConsultationDeptID = objRow["ASKCONSULTATIONDEPTID"].ToString();
                        objRecordContent.m_strAskConsultationDeptName = objRow["ASKDEPTNAME"].ToString().Trim();

                        objRecordContent.m_strConsultationDeptID = objRow["CONSULTATIONDEPTID"].ToString();
                        objRecordContent.m_strConsultationDeptName = objRow["DEPTNAME"].ToString().Trim();

                        objRecordContent.m_strCaseHistory_Right = objRow["CASEHISTORY_RIGHT"].ToString();
                        objRecordContent.m_strCaseHistory = objRow["CASEHISTORY"].ToString();
                        objRecordContent.m_strCaseHistoryXml = objRow["CASEHISTORYXML"].ToString();

                        objRecordContent.m_strConsultationOrder_Right = objRow["CONSULTATIONORDER_RIGHT"].ToString();
                        objRecordContent.m_strConsultationOrder = objRow["CONSULTATIONORDER"].ToString();
                        objRecordContent.m_strConsultationOrderXml = objRow["CONSULTATIONORDERXML"].ToString();

                        objRecordContent.m_strConsultationIdea_Right = objRow["CONSULTATIONIDEA_RIGHT"].ToString();
                        objRecordContent.m_strConsultationIdea = objRow["CONSULTATIONIDEA"].ToString();
                        objRecordContent.m_strConsultationIdeaXml = objRow["CONSULTATIONIDEAXML"].ToString();

                        objRecordContent.m_strOtherHospital_RIGHT = objRow["OTHERHOSPITAL_RIGHT"].ToString();
                        objRecordContent.m_strOtherHospital = objRow["OTHERHOSPITAL"].ToString();
                        objRecordContent.m_strOtherHospitalXML = objRow["OTHERHOSPITALXML"].ToString();

                        objRecordContent.m_dtmConsultationDate = DateTime.Parse(objRow["CONSULTATIONDATE"].ToString());

                        objRecordContent.m_strMainDoctorID = objRow["MAINDOCTORID"].ToString().Trim();
                        objRecordContent.m_strMainDoctorName = objRow["MainDocName"].ToString();

                        DataRow[] drArr = dtbEmpValue.Select("inpatientid = '"+objRecordContent.m_strInPatientID+"' and inpatientdate = '"+objRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss")+"' and opendate = '"+objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss")+"' and employeeflag = 0 ");
                        if (drArr.Length > 0)
                        {
                            int ReqCount = drArr.Length;
                            objRecordContent.m_strRequestDoctorIDArr = new string[ReqCount];
                            objRecordContent.m_strRequestDoctorNameArr = new string[ReqCount];
                            DataRow rowDoc = null;
                            for (int i = 0; i < ReqCount; i++)
                            {
                                rowDoc = drArr[i];
                                objRecordContent.m_strRequestDoctorIDArr[i] = rowDoc["employeeid"].ToString();
                                objRecordContent.m_strRequestDoctorNameArr[i] = rowDoc["lastname_vchr"].ToString();
                            }
                        }
                        drArr = dtbEmpValue.Select("inpatientid = '" + objRecordContent.m_strInPatientID + "' and inpatientdate = '" + objRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and opendate = '" + objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and employeeflag = 1 ");
                        if (drArr.Length > 0)
                        {
                            int ConCount = drArr.Length;
                            objRecordContent.m_strConsultationDoctorIDArr = new string[ConCount];
                            objRecordContent.m_strConsultationDoctorNameArr = new string[ConCount];
                            DataRow rowDoc = null;
                            for (int i = 0; i < ConCount; i++)
                            {
                                rowDoc = drArr[i];
                                objRecordContent.m_strConsultationDoctorIDArr[i] = rowDoc["employeeid"].ToString();
                                objRecordContent.m_strConsultationDoctorNameArr[i] = rowDoc["lastname_vchr"].ToString();
                            }
                        }
                        arlContent.Add(objRecordContent);
                    }

                    //返回结果到p_objRecordContentArr
                    p_objRecordContentArr = (clsConsultationRecordContent[])arlContent.ToArray(typeof(clsConsultationRecordContent));
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
            return lngRes;
        }

        [AutoComplete]
        public long m_lngUpdateRecord(clsConsultationRecordContent p_objRecord)
        {
            if (p_objRecord == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;

            string strUpdateRecord = @"update consultationrecord set consultationidea=?,consultationideaxml=?,hasreplied = ? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

            string strUpdateContent = @"update consultationrecordcontent a
                                       set a.consultationidea_right = ?,a.consultationdate=?
                                     where a.inpatientid = ?
                                       and a.inpatientdate = ?
                                       and a.opendate = ?
                                       and a.modifydate = (select max(modifydate)
                                                             from consultationrecordcontent
                                                            where a.inpatientid = inpatientid
                                                              and a.inpatientdate = inpatientdate
                                                              and a.opendate = opendate)";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                long lngEff = 0;
                for (int j = 0 ; j < p_objRecord.m_strConsultationDoctorIDArr.Length ; j++)
                {
                    IDataParameter[] objDPArr4 = null;
                    objHRPServ.CreateDatabaseParameter(5, out objDPArr4);

                    objDPArr4[0].Value = p_objRecord.m_strInPatientID.Trim();
                    objDPArr4[1].DbType = DbType.DateTime;
                    objDPArr4[1].Value = p_objRecord.m_dtmInPatientDate;
                    objDPArr4[2].DbType = DbType.DateTime;
                    objDPArr4[2].Value = p_objRecord.m_dtmOpenDate;
                    objDPArr4[3].DbType = DbType.DateTime;
                    objDPArr4[3].Value = p_objRecord.m_dtmModifyDate;
                    objDPArr4[4].Value = p_objRecord.m_strConsultationDoctorIDArr[j].Trim();

                    bool blnIsEqual = false;
                    if (p_objRecord.m_strRequestDoctorIDArr != null)
                    {

                        for (int k = 0 ; k < p_objRecord.m_strRequestDoctorIDArr.Length ; k++)
                        {
                            if (p_objRecord.m_strConsultationDoctorIDArr[j] == p_objRecord.m_strRequestDoctorIDArr[k])
                            {
                                blnIsEqual = true;
                                break;
                            }
                        }

                        if (!blnIsEqual)
                        {
                            lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordDoctorSQL2, ref lngEff, objDPArr4);
                            if (lngRes <= 0) return lngRes;
                        }
                    }
                    else
                    {
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordDoctorSQL2, ref lngEff, objDPArr4);
                        if (lngRes <= 0) return lngRes;
                    }
                }

                IDataParameter[] objDPArr0 = null;
                objHRPServ.CreateDatabaseParameter(6, out objDPArr0);

                objDPArr0[0].Value = p_objRecord.m_strConsultationIdea;
                objDPArr0[1].Value = p_objRecord.m_strConsultationIdeaXml;
                objDPArr0[2].Value = p_objRecord.m_intHASREPLIED;
                objDPArr0[3].Value = p_objRecord.m_strInPatientID.Trim();
                objDPArr0[4].DbType = DbType.DateTime;
                objDPArr0[4].Value = p_objRecord.m_dtmInPatientDate;
                objDPArr0[5].DbType = DbType.DateTime;
                objDPArr0[5].Value = p_objRecord.m_dtmOpenDate;

                lngRes = objHRPServ.lngExecuteParameterSQL(strUpdateRecord, ref lngEff, objDPArr0);
                if (lngRes <= 0) return lngRes;

                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr1);

                objDPArr1[0].Value = p_objRecord.m_strConsultationIdea_Right;
                objDPArr1[1].DbType = DbType.DateTime;
                objDPArr1[1].Value = p_objRecord.m_dtmConsultationDate;
                objDPArr1[2].Value = p_objRecord.m_strInPatientID.Trim();
                objDPArr1[3].DbType = DbType.DateTime;
                objDPArr1[3].Value = p_objRecord.m_dtmInPatientDate;
                objDPArr1[4].DbType = DbType.DateTime;
                objDPArr1[4].Value = p_objRecord.m_dtmOpenDate;

                lngRes = objHRPServ.lngExecuteParameterSQL(strUpdateContent, ref lngEff, objDPArr1);
                if (lngRes <= 0) return lngRes;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
        }

        /// <summary>
        /// 检查是否有未回复的会诊通知
        /// </summary>
        /// <param name="p_strDeptIDArr">部门ID</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckUnSignContent(string[] p_strDeptIDArr)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //检查参数
                if (p_strDeptIDArr == null || p_strDeptIDArr.Length <= 0)
                    return (long)enmOperationResult.Parameter_Error;

                string DeptSQL = "crc.applyconsultationdeptid = '" + p_strDeptIDArr[0] + "'";
                for (int i = 1 ; i < p_strDeptIDArr.Length ; i++)
                {
                    DeptSQL += " or crc.applyconsultationdeptid = '" + p_strDeptIDArr[i] + "'";
                }
                #region 获取会诊记录
                string strGetSQL = @"select cr.inpatientid,
       cr.inpatientdate,
       cr.opendate,
       cr.createdate,
       cr.createuserid,
       cr.ifconfirm,
       cr.confirmreason,
       cr.confirmreasonxml,
       cr.firstprintdate,
       cr.deactiveddate,
       cr.deactivedoperatorid,
       cr.status,
       cr.casehistory,
       cr.casehistoryxml,
       cr.consultationorder,
       cr.consultationorderxml,
       cr.consultationidea,
       cr.consultationideaxml,
       cr.otherhospitalxml,
       cr.otherhospital,
       cr.hasreplied,
      crc.modifydate,
      crc.modifyuserid,
      crc.consultationtime,
      crc.applyconsultationdeptid,
      crc.askconsultationdeptid,
      crc.consultationdeptid,
      crc.casehistory_right,
      crc.consultationorder_right,
      crc.consultationidea_right,
      crc.consultationdate,
      crc.maindoctorid,
      c1.deptname_vchr as applydeptname,
      c2.deptname_vchr as askdeptname,
      c3.deptname_vchr as deptname
 from consultationrecord cr,
      consultationrecordcontent crc,
      t_bse_deptdesc c1,
      t_bse_deptdesc c2,
      t_bse_deptdesc c3
  where (" + DeptSQL + @")
   and cr.inpatientid = crc.inpatientid
   and cr.inpatientdate = crc.inpatientdate
   and cr.opendate = crc.opendate
   and cr.status = 0
   and crc.applyconsultationdeptid = c1.deptid_chr
   and crc.askconsultationdeptid = c2.deptid_chr
   and crc.consultationdeptid = c3.deptid_chr
   and crc.modifydate =
       (select max(crc1.modifydate)
          from consultationrecordcontent crc1
         where crc1.inpatientid = crc.inpatientid
           and crc1.inpatientdate = crc.inpatientdate
           and crc.opendate = crc1.opendate)
   and crc.maindoctorid is not null
   and cr.hasreplied = 0
order by askdeptname, crc.consultationdate";
                #endregion
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(p_strDeptIDArr.Length, out objDPArr);

                for (int y = 0; y < p_strDeptIDArr.Length; y++)
                {
                    objDPArr[y].Value = p_strDeptIDArr[y];
                }
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetSQL, ref dtbValue, objDPArr);

                if (dtbValue.Rows.Count == 0)
                    return 0;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }            //返回
            return lngRes;
        }

        /// <summary>
        /// 获取指定科室的会诊情况
        /// </summary>
        /// <param name="p_strDeptID">科室ID</param>
        /// <param name="p_dtmStartTime">查询开始时间</param>
        /// <param name="p_dtmEndTime">查询结束时间</param>
        /// <param name="p_intSendOrReceive">发送或接收科室０：发送科室；１：接收科室</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSearchSpesifyDeptConsultationSituation(string p_strDeptID,DateTime p_dtmStartTime,
            DateTime p_dtmEndTime, int p_intSendOrReceive, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (string.IsNullOrEmpty(p_strDeptID))
                    return -1;
                string strDeptSQL = string.Empty;
                if (p_intSendOrReceive == 0)
                {
                    strDeptSQL = "and t.askconsultationdeptid = ?";
                }
                else if (p_intSendOrReceive == 1)
                {
                    strDeptSQL = "and t.applyconsultationdeptid = ?";
                }
                string strSQL = @"select t.inpatientid,
         t.inpatientdate,
         t.opendate,
         t.modifydate,
         t.modifyuserid,
         t.consultationtime,
         t.applyconsultationdeptid,
         t.askconsultationdeptid,
         t.consultationdeptid,
         t.casehistory_right,
         t.consultationorder_right,
         t.consultationidea_right,
         t.consultationdate,
         t.maindoctorid,
         t.otherhospital_right,
       re.hisinpatientid_chr,
       re.hisinpatientdate,
       pa.lastname_vchr,
       pa.sex_chr,
       c1.deptname_vchr applydeptname_vchr,
       c2.deptname_vchr askdeptname_vchr,
       emp.lastname_vchr maindocname
  from consultationrecordcontent t
 inner join consultationrecord t1 on t.inpatientid = t1.inpatientid
                                 and t.inpatientdate = t1.inpatientdate
                                 and t.opendate = t1.opendate
                                 and t1.status = 0
 inner join t_bse_hisemr_relation re on re.emrinpatientid = t.inpatientid
                                    and re.emrinpatientdate =
                                        t.inpatientdate
 inner join t_opr_bih_registerdetail pa on pa.registerid_chr =
                                           re.registerid_chr
 inner join t_bse_deptdesc c1 on c1.deptid_chr = t.applyconsultationdeptid
 inner join t_bse_deptdesc c2 on c2.deptid_chr = t.askconsultationdeptid
 inner join t_bse_employee emp on emp.empno_chr = t.maindoctorid
 where t.modifydate = (select max(modifydate)
                         from consultationrecordcontent
                        where inpatientid = t.inpatientid
                          and inpatientdate = t.inpatientdate
                          and opendate = t.opendate)
   " + strDeptSQL + @"
   and t.consultationdate between ? and ?
 order by t.inpatientid, t.inpatientdate, t.opendate, t.modifydate";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strDeptID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmStartTime;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmEndTime;

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
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
        /// 获取所有科室的会诊情况
        /// </summary>
        /// <param name="p_dtmStartTime">查询开始时间</param>
        /// <param name="p_dtmEndTime">查询结束时间</param>
        /// <param name="p_dtbResult">查询结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSearchAllDeptConsultationSituation(DateTime p_dtmStartTime,
            DateTime p_dtmEndTime, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"select t.inpatientid,
         t.inpatientdate,
         t.opendate,
         t.modifydate,
         t.modifyuserid,
         t.consultationtime,
         t.applyconsultationdeptid,
         t.askconsultationdeptid,
         t.consultationdeptid,
         t.casehistory_right,
         t.consultationorder_right,
         t.consultationidea_right,
         t.consultationdate,
         t.maindoctorid,
         t.otherhospital_right,
       re.hisinpatientid_chr,
       re.hisinpatientdate,
       pa.lastname_vchr,
       pa.sex_chr,
       c1.deptname_vchr applydeptname_vchr,
       c2.deptname_vchr askdeptname_vchr,
       emp.lastname_vchr maindocname
  from consultationrecordcontent t
 inner join consultationrecord t1 on t.inpatientid = t1.inpatientid
                                 and t.inpatientdate = t1.inpatientdate
                                 and t.opendate = t1.opendate
                                 and t1.status = 0
 inner join t_bse_hisemr_relation re on re.emrinpatientid = t.inpatientid
                                    and re.emrinpatientdate =
                                        t.inpatientdate
 inner join t_opr_bih_registerdetail pa on pa.registerid_chr =
                                           re.registerid_chr
 inner join t_bse_deptdesc c1 on c1.deptid_chr = t.applyconsultationdeptid
 inner join t_bse_deptdesc c2 on c2.deptid_chr = t.askconsultationdeptid
 inner join t_bse_employee emp on emp.empno_chr = t.maindoctorid
 where t.modifydate = (select max(modifydate)
                         from consultationrecordcontent
                        where inpatientid = t.inpatientid
                          and inpatientdate = t.inpatientdate
                          and opendate = t.opendate)
   and t.consultationdate between ? and ?
 order by askdeptname_vchr,t.inpatientid, t.inpatientdate, t.opendate, t.modifydate";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmStartTime;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmEndTime;

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objDPArr);
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
        /// 获取全部作废记录
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public long m_lngGetAllInactiveInfo(string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            p_objInactiveRecordInfoArr = null;
            if (string.IsNullOrEmpty(p_strInpatientId) || p_dtmInpatientDate == DateTime.MinValue) return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                string strSql = @" select a.createdate,
	a.opendate,
	a.deactiveddate,
	b.lastname_vchr as createdusername,
	c.lastname_vchr as deactiveusername
from consultationrecord a,t_bse_employee b,t_bse_employee c
where trim(a.createuserid) = b.empno_chr
   and trim(a.deactivedoperatorid) = c.empno_chr
   and a.inpatientid = ?
   and a.inpatientdate = ?
  and a.status = 1
order by a.opendate desc";
                DataTable dtExist = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

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
