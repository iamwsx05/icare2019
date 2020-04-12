using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.emr.AssistModuleSev.SQLHelper
{
    /// <summary>
    /// �������в�ѯ���ݿ��SQL���,������ʱ�����Oracle��PL/SQL
    /// </summary>
    internal class clsSqlStringHelper
    {
        /*��������-------------------------
         * 1��ԭ����ֻ�ܱ���Ŀʹ�ã�ʹ��internal�������η�
         * 2��ʹ��const�ؼ��ֿ���Ϊ����
         * 3����������"c_str"+"[�������]"+"[����������]"+"\[���ݿ��ʶ]"
         * [�������]����ѯ��"Query";���룽"Insert";���£�"Update"
         * [����������]:SQL���������Ҫ��һ�ű����ƣ��������ܷ�ӳ��Ҫҵ�����������,����Ǳ���,��ȫ����д
         * \[���ݿ��ʶ]��O��Oracle��S��SQL Server��D��DB2�������ͨ��SQL���Ӻ�׺��
         * ��c_strQueryT_bse_bed_O,c_strInsertT_bse_bed_S,c_strUpdateT_bse_bed_D
         ----------------------------------*/

        #region �������ͣ�������ҽԺ��
        private const string c_strQueryInHospitalMainRec = @"select n.condictionwhenin wzqk,
       n.salvetimes qjcs,
       n.salvesuccess qjts,
       n.infectiondiagnosis yngr,
       n.paytype fb,
       rowscount zdgs,
       p.aanaesthesiamodeid mzmf1,
       n.inpatientid,
       n.inpatientdate,
       dp.deptname_vchr,
       r.extendid_vchr,
       h.birth_dat birthday,
       sign
  from t_emr_inhospitalmainrec_gxcon n
 inner join t_opr_bih_register r on r.registerid_chr = n.registerid_chr
 inner join t_opr_bih_registerdetail h on h.registerid_chr =
                                          n.registerid_chr
 inner join t_bse_deptdesc dp on dp.deptid_chr = r.deptid_chr
  left join (select count(emr_seq) rowscount, emr_seq
               from t_emr_inhospitalmainrec_gxod
              where status = 0
              group by emr_seq) d on d.emr_seq = n.emr_seq
  left join (select emr_seq, p1.aanaesthesiamodeid, 1 sign
               from t_emr_inhospitalmainrec_gxop p1
              where status = 0
                and p1.seqid =
                    (select min(seqid)
                       from t_emr_inhospitalmainrec_gxop p2
                      where p2.emr_seq = p1.emr_seq
                        and p2.status = 0
                        and p2.aanaesthesiamodeid is not null)) p on p.emr_seq =
                                                                     n.emr_seq
 where n.status = 0
   and n.inpatientdate between ? and ? ";//2
        /// <summary>
        /// ��ѯ������ҳ���
        /// </summary>
        /// <returns></returns>
        internal static string s_strGetQueryInHospitalMainRec
        {
            get { return c_strQueryInHospitalMainRec; }
        }

        private const string c_strQueryPAT_VISIT = @"select patient_id,
       visit_id rhry,
       first_level_nurs_days yjhl,
       spec_level_nurs_days thts,
       identity zw,
       patient_class ryfs
  from pat_visit
 where admission_date_time between ? and ? ";
        /// <summary>
        /// ��ѯ����סԺ����¼������������
        /// </summary>
        /// <returns></returns>
        internal static string s_strGetQueryPAT_VISIT
        {
            get{return c_strQueryPAT_VISIT;}
        }

        private const string c_strQueryIDENTITY_DICT = @"select serial_no,identity_name from identity_dict";
        /// <summary>
        /// ��ѯ�����𣨾��ݣ�
        /// </summary>
        /// <returns></returns>
        internal static string s_strGetQueryIDENTITY_DIC
        {
            get{return c_strQueryIDENTITY_DICT;}
        }

        private const string c_strQueryCHARGE_TYPE_DICT = @"select serial_no,charge_type_name from charge_type_dict";
        /// <summary>
        /// ��ѯ�ѱ𣨾��ݣ�
        /// </summary>
        /// <returns></returns>
        internal static string s_strGetQueryCHARGE_TYPE_DICT
        {
            get{return c_strQueryCHARGE_TYPE_DICT;}
        }
        #endregion �������ͣ�������ҽԺ��

        #region ���ϼ�¼��ѯ
        private const string c_strQueryT_AID_EMR_FORM_AllInActiveView = @"select formid_int,
       formnamespace_vchr,
       formname_vchr,
       formdesc_vchr,
       dllname_vchr,
       opraclassname_vchr,
       opramethodname_vchr,
       formstate_int
  from t_aid_emr_form
 where usageflag_int = 0
   and deactivestate_int = 1
   and formstate_int > 0
   order by formdesc_vchr";
        /// <summary>
        /// ��ȡ���п������ϲ�ѯ�Ĵ���;
        /// ����t_aid_emr_form
        /// </summary>
        /// <returns></returns>
        internal static string s_StrGetQueryT_AID_EMR_FORM_AllInActiveView
        {
            get { return c_strQueryT_AID_EMR_FORM_AllInActiveView; }
        }
        private const string c_strQueryT_AID_EMR_FORM_OneInActiveView = @"select formid_int,
       formnamespace_vchr,
       formname_vchr,
       formdesc_vchr,
       dllname_vchr,
       opraclassname_vchr,
       opramethodname_vchr,
       formstate_int
  from t_aid_emr_form
 where usageflag_int = 0
   and deactivestate_int = 1
   and formstate_int > 0
   and formnamespace_vchr = ?
   and formname_vchr = ?";
        /// <summary>
        /// ���ݴ��������ͷ������ϲ�ѯ�Ĵ���;
        /// ����t_aid_emr_form;
        /// ����2��(formnamespace_vchr,formname_vchr);
        /// </summary>
        /// <returns></returns>
        internal static string s_StrGetQueryT_AID_EMR_FORM_OneInActiveView
        {
            get { return c_strQueryT_AID_EMR_FORM_OneInActiveView; }
        }
        #endregion ���ϼ�¼��ѯ
    }
}
