using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
using iCare.iCareBaseForm;
using System.Collections;
using com.digitalwave.Utility.SQLConvert;
using iCare.RecordSearch;

namespace iCare
{
    public partial class frmCaseHistorySearch_Combin : frmBaseForm
    {
        #region ȫ�ֱ���
        /// <summary>
        /// HashTable�����Ӧ�Ĳ�ѯ���
        /// </summary>
        private Hashtable m_hstMainSql = new Hashtable();
        private Hashtable m_hasPatientID = new Hashtable();
        private Hashtable m_hasPatientIDAndDate = new Hashtable();
        #endregion

        #region ���췽��
        /// <summary>
        /// סԺ������ҳ��ϲ�ѯ
        /// </summary>
        public frmCaseHistorySearch_Combin()
        {
            InitializeComponent();

            m_mthInitTreeContent();
            m_mthInitArray();
            m_mthAddComboItem();

            m_cboConn.SelectedIndex = 0;
        } 
        #endregion

        #region ��ʼ�������
        /// <summary>
        /// ��ʼ�������
        /// </summary>
        private void m_mthInitTreeContent()
        {
            clsKeyAndFormName objKeyAndFormName;
            TreeNode baseNode = new TreeNode("��������", 0, 0);
            objKeyAndFormName = new clsKeyAndFormName("S1", "", "");
            baseNode.Tag = objKeyAndFormName;
            TreeNode baseNodeChild;
            TreeNode childNodeChild;
            TreeNode ccNodeChild;

            #region ��������
            baseNodeChild = new TreeNode("סԺ��", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>סԺ��", "(b.INPATIENTID_CHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��Ժ����", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��Ժ����", "(b.INPATIENTCOUNT_INT<CONDITION>)", "INT");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>����", "(a.LASTNAME_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>����", "(" + clsDatabaseSQLConvert.s_strGetAgeSQL("a.BIRTH_DAT") + "<CONDITION>)", "INT");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�Ա�", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>�Ա�", "(a.SEX_CHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��������", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��������", "(a.BIRTH_DAT<CONDITION>)", "DATE");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����״��", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("�ѻ�", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "����״��>>�ѻ�", "((select count(a.MARRIED_CHR) from T_BSE_PATIENT where PATIENTID_CHR = a.PATIENTID_CHR and MARRIED_CHR='�ѻ�')<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("δ��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "����״��>>δ��", "((select count(a.MARRIED_CHR) from T_BSE_PATIENT where PATIENTID_CHR = a.PATIENTID_CHR and MARRIED_CHR='δ��')<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("ְҵ", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>ְҵ", "(a.OCCUPATION_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>������", "(a.BIRTHPLACE_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>����", "(a.RACE_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>����", "(a.NATIONALITY_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("���֤��", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>���֤��", "(a.IDCARD_CHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������ַ", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>������ַ", "(a.OFFICEADDRESS_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������λ", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>������λ", "(a.EMPLOYER_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�����绰", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>�����绰", "(a.OFFICEPHONE_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�����ʱ�", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>�����ʱ�", "(a.OFFICEPC_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("���ڵ�ַ", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>���ڵ�ַ", "(a.HOMEADDRESS_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��ϵ������", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��ϵ������", "(a.CONTACTPERSONLASTNAME_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��ϵ���뻼�߹�ϵ", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��ϵ���뻼�߹�ϵ", "(a.PATIENTRELATION_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��ϵ�˵�ַ", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��ϵ�˵�ַ", "(a.CONTACTPERSONADDRESS_VCHR<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��Ժ�Ʊ�", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��Ժ�Ʊ�", "(e.deptname_vchr<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��Ժ����", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��Ժ����", "(b.INPATIENT_DAT<CONDITION>)", "DATE");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��Ժ�Ʊ�", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��Ժ�Ʊ�", "(d.deptname_vchr<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��Ժ����", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>��Ժ����", "(d.MODIFY_DAT<CONDITION>)", "DATE");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("ת������", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>ת������", "(f.MODIFY_DAT<CONDITION>)", "DATE");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("ת��Ʊ�", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>ת��Ʊ�", "(f.deptname_vchr<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("סԺ����", 0, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "��������>>סԺ����", "(" + clsDatabaseSQLConvert.s_strGetDaysBetween2DateSQL("b.INPATIENT_DAT", "d.modify_dat") + "<CONDITION>)", "INT");
            baseNode.Nodes.Add(baseNodeChild);

            m_mthSetCanSearchNodeColor(baseNode);
            m_trvMain.Nodes.Add(baseNode); 
            #endregion

            #region סԺ������ҳ(����)
            baseNode = new TreeNode("סԺ������ҳ����", 2, 2);
            objKeyAndFormName = new clsKeyAndFormName("S1", "frmInHospitalMainRecord_GXForCH", "");
            baseNode.Tag = objKeyAndFormName;

            baseNodeChild = new TreeNode("��Ժ���", 2, 3);
            objKeyAndFormName = new clsKeyAndFormName("S12", "", "");
            baseNodeChild.Tag = objKeyAndFormName;
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("�������", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("txtInHospitalDiagnosis", "סԺ������ҳ>>��Ժ���", "(IHMI.DIAGNOSISDESC<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ͳ����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtSTATCODEOFINHOSPITALDIA", "סԺ������ҳ>>��Ժ���ͳ����", "(IHMI.STATCODE<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ICD��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtICD_10OFINHOSPITALDIA", "סԺ������ҳ>>��Ժ���ICD��", "(IHMI.ICD10<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��Ժ���", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("Σ", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ���>>Σ", "((select count(CONDICTIONWHENIN) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and CONDICTIONWHENIN=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ���>>��", "((select count(CONDICTIONWHENIN) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and CONDICTIONWHENIN=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("һ��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ���>>һ��", "((select count(CONDICTIONWHENIN) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and CONDICTIONWHENIN=3)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��Ժȷ������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժȷ������", "(IHMC.CONFIRMDIAGNOSISDATE<CONDITION>)", "DATE");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�������", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("�������", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("txtDiagnosis", "סԺ������ҳ>>�������", "(IHMC.DIAGNOSIS<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ͳ����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtSTATCODEOFDIAGNOSIS", "סԺ������ҳ>>��(��)�����ͳ����", "(IHMC.STATCODEOFDIAGNOSIS<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ICD��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtICD_10OFDIAGNOSIS", "סԺ������ҳ>>��(��)�����ICD��", "(IHMC.ICD_10OFDIAGNOSIS<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��Ժ��Ҫ���", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("�������", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtMainDiagnosis", "סԺ������ҳ>>��Ժ��Ҫ���", "(IHMC.MAINDIAGNOSIS<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��Ч", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ��Ҫ���>>����", "((select count(MAINCONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MAINCONDITIONSEQ=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��ת", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ��Ҫ���>>��ת", "((select count(MAINCONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MAINCONDITIONSEQ=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("δ��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ��Ҫ���>>δ��", "((select count(MAINCONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MAINCONDITIONSEQ=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ��Ҫ���>>����", "((select count(MAINCONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MAINCONDITIONSEQ=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ��Ҫ���>>����", "(IHMC.OTHERMAINCONDITION<CONDITION>)", "STRING");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��������", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ��Ҫ���>>��������", "((select count(MAINCONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MAINCONDITIONSEQ=5)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("ͳ����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtSTATCODEOFMAIN", "סԺ������ҳ>>��Ժ��Ҫ���ͳ����", "(IHMC.STATCODEOFMAIN<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ICD��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtICD_10OFMAIN", "סԺ������ҳ>>��Ժ��Ҫ���ICD��", "(IHMC.ICD_10OFMAIN<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��Ժ�������", 2, 3);
            objKeyAndFormName = new clsKeyAndFormName("S10", "frmInHospitalMainRecord_GXForCH", "");
            baseNodeChild.Tag = objKeyAndFormName;
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("�������", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������", "(IHMD.DIAGNOSISDESC<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��Ч", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������>>����", "((select count(CONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXOD where EMR_SEQ=IHMC.EMR_SEQ and CONDITIONSEQ=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��ת", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������>>��ת", "((select count(CONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXOD where EMR_SEQ=IHMC.EMR_SEQ and CONDITIONSEQ=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("δ��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������>>δ��", "((select count(CONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXOD where EMR_SEQ=IHMC.EMR_SEQ and CONDITIONSEQ=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������>>����", "((select count(CONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXOD where EMR_SEQ=IHMC.EMR_SEQ and CONDITIONSEQ=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������>>����", "(IHMD.OTHERCONDITION<CONDITION>)", "STRING");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��������", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������>>��������", "((select count(CONDITIONSEQ) from T_EMR_INHOSPITALMAINREC_GXOD where EMR_SEQ=IHMC.EMR_SEQ and CONDITIONSEQ=5)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("ͳ����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������ͳ����", "(IHMD.STATCODE<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ICD��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ�������ICD��", "(IHMD.ICD10<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("����֢(����������)", 2, 3);
            objKeyAndFormName = new clsKeyAndFormName("S9", "frmInHospitalMainRecord_GXForCH", "");
            baseNodeChild.Tag = objKeyAndFormName;
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("����֢(����������)", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtCOMPLICATION", "סԺ������ҳ>>����֢(����������)", "(IHMC.COMPLICATION<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��Ч", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����֢(����������)>>����", "((select count(COMPLICATIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and COMPLICATIONSEQ=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��ת", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����֢(����������)>>��ת", "((select count(COMPLICATIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and COMPLICATIONSEQ=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("δ��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����֢(����������)>>δ��", "((select count(COMPLICATIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and COMPLICATIONSEQ=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����֢(����������)>>����", "((select count(COMPLICATIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and COMPLICATIONSEQ=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����֢(����������)>>����", "(IHMC.OTHERCOMPLICATION<CONDITION>)", "STRING");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��������", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����֢(����������)>>��������", "((select count(COMPLICATIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and COMPLICATIONSEQ=5)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("ͳ����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtSTATCODEOFCOMPLICATION", "סԺ������ҳ>>����֢(����������)ͳ����", "(IHMC.STATCODEOFCOMPLICATION<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ICD��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtICD_10OFCOMPLICATION", "סԺ������ҳ>>����֢(����������)ICD��", "(IHMC.ICD_10OFCOMPLICATION<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("Ժ�ڸ�Ⱦ����", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("Ժ�ڸ�Ⱦ����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtINFECTIONDIAGNOSIS", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����", "(IHMC.INFECTIONDIAGNOSIS<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��Ч", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����>>����", "((select count(INFECTIONCONDICTIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and INFECTIONCONDICTIONSEQ=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��ת", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����>>��ת", "((select count(INFECTIONCONDICTIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and INFECTIONCONDICTIONSEQ=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("δ��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����>>δ��", "((select count(INFECTIONCONDICTIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and INFECTIONCONDICTIONSEQ=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����>>����", "((select count(INFECTIONCONDICTIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and INFECTIONCONDICTIONSEQ=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����>>����", "(IHMC.OTHERINFECTIONCONDICTION<CONDITION>)", "STRING");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��������", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����>>��������", "((select count(INFECTIONCONDICTIONSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and INFECTIONCONDICTIONSEQ=5)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("ͳ����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtSTATCODEOFINFECTION", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����ͳ����", "(IHMC.STATCODEOFINFECTION<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ICD��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtICD_10OFINFECTION", "סԺ������ҳ>>Ժ�ڸ�Ⱦ����ICD��", "(IHMC.ICD_10OFINFECTION<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("�������", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("�������", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtPATHOLOGYDIAGNOSIS", "סԺ������ҳ>>�������", "(IHMC.PATHOLOGYDIAGNOSIS<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��Ч", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "((select count(PATHOLOGYDIAGNOSISSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOLOGYDIAGNOSISSEQ=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��ת", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>��ת", "((select count(PATHOLOGYDIAGNOSISSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOLOGYDIAGNOSISSEQ=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("δ��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>δ��", "((select count(PATHOLOGYDIAGNOSISSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOLOGYDIAGNOSISSEQ=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "((select count(PATHOLOGYDIAGNOSISSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOLOGYDIAGNOSISSEQ=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "(IHMC.OTHERPATHOLOGYDIAGNOSIS<CONDITION>)", "STRING");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("��������", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>��������", "((select count(PATHOLOGYDIAGNOSISSEQ) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOLOGYDIAGNOSISSEQ=5)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("ͳ����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtSTATCODEOFPATHOLOGYDIA", "סԺ������ҳ>>�������ͳ����", "(IHMC.STATCODEOFPATHOLOGYDIA<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ICD��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("m_txtICD_10OFPATHOLOGYDIA", "סԺ������ҳ>>�������ICD��", "(IHMC.ICD_10OFPATHOLOGYDIA<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("���˺��ж����ⲿԭ��", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("m_txtScacheSource", "סԺ������ҳ>>���˺��ж����ⲿԭ��", "(IHMC.SCACHESOURCE<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("���岡", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>���岡>>��", "((select count(NEW5DISEASE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and NEW5DISEASE=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>���岡>>��", "((select count(NEW5DISEASE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and NEW5DISEASE=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("����ת��", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ת��>>��", "((select count(SECONDLEVELTRANSFER) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and SECONDLEVELTRANSFER=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ת��>>��", "((select count(SECONDLEVELTRANSFER) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and SECONDLEVELTRANSFER=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("����ҩ��", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("HBsAg", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("δ��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HBsAg>>δ��", "((select count(HBSAG) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HBSAG=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HBsAg>>����", "((select count(HBSAG) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HBSAG=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HBsAg>>����", "((select count(HBSAG) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HBSAG=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("HCV-Ab", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("δ��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HCV-Ab>>δ��", "((select count(HCV_AB) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HCV_AB=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HCV-Ab>>����", "((select count(HCV_AB) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HCV_AB=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HCV-Ab>>����", "((select count(HCV_AB) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HCV_AB=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("HIV-Ab", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("δ��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HIV-Ab>>δ��", "((select count(HIV_AB) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HIV_AB=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HIV-Ab>>����", "((select count(HIV_AB) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HIV_AB=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>HIV-Ab>>����", "((select count(HIV_AB) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HIV_AB=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            baseNodeChild = new TreeNode("�������", 2, 3);
            objKeyAndFormName = new clsKeyAndFormName("S11", "frmInHospitalMainRecord_GXForCH", "");
            baseNodeChild.Tag = objKeyAndFormName;
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��������������", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>��������������", "(IHMO.OPERATIONDATE<CONDITION>)", "DATE");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��������������", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>��������������", "(IHMO.OPERATIONID<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��������������", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>��������������", "(IHMO.OPERATIONNAME<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����ID", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "(IHMO.OPERATOR<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "(IHMO.OPERATOR in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����ID", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "(IHMO.ASSISTANT1<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "(IHMO.ASSISTANT1 in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����ID", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "(IHMO.ASSISTANT2<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����", "(IHMO.ASSISTANT2 in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����ʽID", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����ʽ", "(IHMO.AANAESTHESIAMODEID<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����ʽ", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����ʽ", "(IHMO.AANAESTHESIAMODEID in (select AANAESTHESIAMODEID from AnaesthesiaMode where AnaesthesiaModeName<CONDITION>))", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("�п����ϵȼ�", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>�п����ϵȼ�", "(IHMO.CUTLEVEL<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����ҽʦID", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����ҽʦ", "(IHMO.ANAESTHETIST<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����ҽʦ", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������>>����ҽʦ", "(IHMO.ANAESTHETIST in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("�������������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������������", "(IHMC.NEONATEDISEASE1<CONDITION> or IHMC.NEONATEDISEASE2<CONDITION> or IHMC.NEONATEDISEASE3<CONDITION> or IHMC.NEONATEDISEASE4<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("���ȴ���", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("m_txtSALVETIMES", "סԺ������ҳ>>���ȴ���", "(IHMC.SALVETIMES<CONDITION>)", "INT");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("���ȳɹ�����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("m_txtSALVESUCCESS", "סԺ������ҳ>>���ȳɹ�����", "(IHMC.SALVESUCCESS<CONDITION>)", "INT");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����>>��", "((select count(HASREMIND) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HASREMIND=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����>>��", "((select count(HASREMIND) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and HASREMIND=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("m_txtREMINDTERM", "סԺ������ҳ>>��������", "(IHMC.REMINDTERM<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�������޵�λ", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������޵�λ>>��", "((select count(REMINDTERMTYPE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and REMINDTERMTYPE=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������޵�λ>>��", "((select count(REMINDTERMTYPE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and REMINDTERMTYPE=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�������޵�λ>>��", "((select count(REMINDTERMTYPE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and REMINDTERMTYPE=3)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��Ϸ������", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("����-��Ժ", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����-��Ժ>>��", "((select count(ACCORDWITHOUTHOSPITAL) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDWITHOUTHOSPITAL=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����-��Ժ>>����", "((select count(ACCORDWITHOUTHOSPITAL) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDWITHOUTHOSPITAL=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����-��Ժ>>����", "((select count(ACCORDWITHOUTHOSPITAL) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDWITHOUTHOSPITAL=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����-��Ժ>>����", "((select count(ACCORDWITHOUTHOSPITAL) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDWITHOUTHOSPITAL=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("��Ժ-��Ժ", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ-��Ժ>>��", "((select count(ACCORDINWITHOUT) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDINWITHOUT=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ-��Ժ>>����", "((select count(ACCORDINWITHOUT) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDINWITHOUT=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ-��Ժ>>����", "((select count(ACCORDINWITHOUT) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDINWITHOUT=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժ-��Ժ>>����", "((select count(ACCORDINWITHOUT) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDINWITHOUT=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("��ǰ-����", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ǰ-����>>��", "((select count(ACCORDBFOPRWITHAF) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDBFOPRWITHAF=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ǰ-����>>����", "((select count(ACCORDBFOPRWITHAF) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDBFOPRWITHAF=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ǰ-����>>����", "((select count(ACCORDBFOPRWITHAF) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDBFOPRWITHAF=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ǰ-����>>����", "((select count(ACCORDBFOPRWITHAF) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDBFOPRWITHAF=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("�ٴ�-����", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�ٴ�-����>>��", "((select count(ACCORDCLINICWITHPATHOLOGY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDCLINICWITHPATHOLOGY=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�ٴ�-����>>����", "((select count(ACCORDCLINICWITHPATHOLOGY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDCLINICWITHPATHOLOGY=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�ٴ�-����>>����", "((select count(ACCORDCLINICWITHPATHOLOGY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDCLINICWITHPATHOLOGY=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�ٴ�-����>>����", "((select count(ACCORDCLINICWITHPATHOLOGY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDCLINICWITHPATHOLOGY=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("����-ʬ��", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����-ʬ��>>��", "((select count(ACCORDDEATHWITHBODYCHECK) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDDEATHWITHBODYCHECK=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����-ʬ��>>����", "((select count(ACCORDDEATHWITHBODYCHECK) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDDEATHWITHBODYCHECK=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����-ʬ��>>����", "((select count(ACCORDDEATHWITHBODYCHECK) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDDEATHWITHBODYCHECK=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����-ʬ��>>����", "((select count(ACCORDDEATHWITHBODYCHECK) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDDEATHWITHBODYCHECK=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            childNodeChild = new TreeNode("�ٴ�-����", 2, 3);
            baseNodeChild.Nodes.Add(childNodeChild);

            ccNodeChild = new TreeNode("��", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�ٴ�-����>>��", "((select count(ACCORDCLINICWITHRADIATE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDCLINICWITHRADIATE=0)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�ٴ�-����>>����", "((select count(ACCORDCLINICWITHRADIATE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDCLINICWITHRADIATE=1)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�ٴ�-����>>����", "((select count(ACCORDCLINICWITHRADIATE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDCLINICWITHRADIATE=2)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            ccNodeChild = new TreeNode("����", 2, 3);
            ccNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�ٴ�-����>>����", "((select count(ACCORDCLINICWITHRADIATE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ACCORDCLINICWITHRADIATE=3)<CONDITION>)", "BOOL");
            childNodeChild.Nodes.Add(ccNodeChild);

            baseNodeChild = new TreeNode("ʾ�̲���", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>ʾ�̲���>>��", "((select count(MODELCASE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MODELCASE=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>ʾ�̲���>>��", "((select count(MODELCASE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MODELCASE=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("���������ơ���顢���Ϊ��Ժ��һ��", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>���������ơ���顢���Ϊ��Ժ��һ��>>��", "((select count(FIRSTCASE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and FIRSTCASE=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>���������ơ���顢���Ϊ��Ժ��һ��>>��", "((select count(FIRSTCASE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and FIRSTCASE=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��������", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��������>>��", "((select count(QUALITY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and QUALITY=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��������>>��", "((select count(QUALITY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and QUALITY=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��������>>��", "((select count(QUALITY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and QUALITY=3)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("����ҩ��ʹ��", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ҩ��ʹ��>>��", "((select count(ANTIBACTERIAL) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ANTIBACTERIAL=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ҩ��ʹ��>>��", "((select count(ANTIBACTERIAL) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and ANTIBACTERIAL=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��ԭѧ�ͼ�", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ԭѧ�ͼ�>>��", "((select count(PATHOGENY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOGENY=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ԭѧ�ͼ�>>��", "((select count(PATHOGENY) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOGENY=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��ԭѧ�ͼ���", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ԭѧ�ͼ���>>����", "((select count(PATHOGENYRESULT) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOGENYRESULT=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ԭѧ�ͼ���>>����", "((select count(PATHOGENYRESULT) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and PATHOGENYRESULT=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��Ѫ��Ӧ", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ѫ��Ӧ>>��", "((select count(BLOODTRANSACTOIN) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODTRANSACTOIN=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ѫ��Ӧ>>��", "((select count(BLOODTRANSACTOIN) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODTRANSACTOIN=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��Һ��Ӧ", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Һ��Ӧ>>��", "((select count(TRANSFUSIONSACTION) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and TRANSFUSIONSACTION=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Һ��Ӧ>>��", "((select count(TRANSFUSIONSACTION) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and TRANSFUSIONSACTION=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("CT���", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>CT���>>��", "((select count(CTCHECK) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and CTCHECK=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>CT���>>��", "((select count(CTCHECK) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and CTCHECK=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("MRI���", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>MRI���>>��", "((select count(MRICHECK) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MRICHECK=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>MRI���>>��", "((select count(MRICHECK) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and MRICHECK=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("Ѫ��", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ѫ��>>����", "((select count(BLOODTYPE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODTYPE=0)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("A", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ѫ��>>A", "((select count(BLOODTYPE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODTYPE=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("B", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ѫ��>>B", "((select count(BLOODTYPE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODTYPE=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("AB", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ѫ��>>AB", "((select count(BLOODTYPE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODTYPE=3)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("O", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ѫ��>>O", "((select count(BLOODTYPE) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODTYPE=4)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("Rh", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Rh>>����", "((select count(BLOODRH) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODRH=0)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Rh>>��", "((select count(BLOODRH) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODRH=1)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Rh>>��", "((select count(BLOODRH) from T_EMR_INHOSPITALMAINREC_GXCON where EMR_SEQ=IHMC.EMR_SEQ and BLOODRH=2)<CONDITION>)", "BOOL");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("��ѪƷ��", 2, 3);
            baseNode.Nodes.Add(baseNodeChild);

            childNodeChild = new TreeNode("��ϸ��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��ϸ��", "(IHMC.RBC<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ѪС��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>ѪС��", "(IHMC.PLT<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("Ѫ��", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>Ѫ��", "(IHMC.PLASM<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("ȫѪ", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>ȫѪ", "(IHMC.WHOLEBLOOD<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            childNodeChild = new TreeNode("����", 2, 3);
            childNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����", "(IHMC.OTHERBLOOD<CONDITION>)", "STRING");
            baseNodeChild.Nodes.Add(childNodeChild);

            baseNodeChild = new TreeNode("������ID", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>������", "(IHMC.DEPTDIRECTORDT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>������", "(IHMC.DEPTDIRECTORDT in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����ҽʦID", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ҽʦ", "(IHMC.DIRECTORDT<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����ҽʦ", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ҽʦ", "(IHMC.DIRECTORDT in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������ҽʦID", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>������������ҽʦ", "(IHMC.SUBDIRECTORDT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������������ҽʦ", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>������������ҽʦ", "(IHMC.SUBDIRECTORDT in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����ҽʦID", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ҽʦ", "(IHMC.DT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����ҽʦ", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ҽʦ", "(IHMC.DT in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��ԺҽʦID", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>סԺҽʦ", "(IHMC.INHOSPITALDOC<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��Ժҽʦ", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>סԺҽʦ", "(IHMC.INHOSPITALDOC in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��ԺҽʦID", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժҽʦ", "(IHMC.OUTHOSPITALDOC<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��Ժҽʦ", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>��Ժҽʦ", "(IHMC.OUTHOSPITALDOC in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����ҽʦID", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ҽʦ", "(IHMC.ATTENDINFORADVANCESSTUDYDT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����ҽʦ", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>����ҽʦ", "(IHMC.ATTENDINFORADVANCESSTUDYDT in (select Empno_Chr from t_bse_employee where Lastname_Vchr<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�о���ʵϰҽʦ", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�о���ʵϰҽʦ", "(IHMC.GRADUATESTUDENTINTERN<CONDITION>))", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("ʵϰҽʦ", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>ʵϰҽʦ", "(IHMC.INTERN<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("סԺ�����ܼ�", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtTotalAmt", "סԺ������ҳ>>סԺ�����ܼ�", "(IHMC.TOTALAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��λ��", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtBedAmt", "סԺ������ҳ>>����", "(IHMC.BEDAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtNurseAmt", "סԺ������ҳ>>�����", "(IHMC.NURSEAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��ҩ��", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtWMAmt", "סԺ������ҳ>>��ҩ��", "(IHMC.WMAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�г�ҩ��", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtCMFinishedAmt", "סԺ������ҳ>>�г�ҩ��", "(IHMC.CMFINISHEDAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�в�ҩ��", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtCMSemiFinishedAmt", "סԺ������ҳ>>�в�ҩ��", "(IHMC.CMSEMIFINISHEDAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtRadiationAmt", "סԺ������ҳ>>�����", "(IHMC.RADIATIONAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtAssayAmt", "סԺ������ҳ>>�����", "(IHMC.ASSAYAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtO2Amt", "סԺ������ҳ>>������", "(IHMC.O2AMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��Ѫ��", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtBloodAmt", "סԺ������ҳ>>��Ѫ��", "(IHMC.BLOODAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("���Ʒ�", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtTreatmentAmt", "סԺ������ҳ>>���Ʒ�", "(IHMC.TREATMENTAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtOperationAmt", "סԺ������ҳ>>������", "(IHMC.OPERATIONAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtCheckAmt", "סԺ������ҳ>>����", "(IHMC.CHECKAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtAnaethesiaAmt", "סԺ������ҳ>>�����", "(IHMC.ANAETHESIAAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtDeliveryChildAmt", "סԺ������ҳ>>������", "(IHMC.DELIVERYCHILDAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("Ӥ����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtBabyAmt", "סԺ������ҳ>>Ӥ����", "(IHMC.BABYAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�㴲��", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtAccompanyAmt", "סԺ������ҳ>>�㴲��", "(IHMC.ACCOMPANYAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("��������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("txtOtherAmt", "סԺ������ҳ>>������", "(IHMC.OTHERAMT<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>������", "(IHMC.CODINGNAME<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("������", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>������", "(IHMC.NEATENNAME<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("�����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>�����", "(IHMC.INPUTMACHINENAME<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            baseNodeChild = new TreeNode("ͳ����", 2, 3);
            baseNodeChild.Tag = m_objGetDateSearchesType("", "סԺ������ҳ>>ͳ����", "(IHMC.STATISTICNAME<CONDITION>)", "STRING");
            baseNode.Nodes.Add(baseNodeChild);

            m_mthSetCanSearchNodeColor(baseNode);
            //m_mthFoundAndSetTemplate2Node(baseNode, "frmInHospitalMainRecord_GXForCH");
            m_trvMain.Nodes.Add(baseNode);
            #endregion

            m_trvMain.Nodes[0].Expand();

            m_trvMain.SelectedNode = m_trvMain.Nodes[0].FirstNode;
        } 
        #endregion

        #region �����������ɫ
        /// <summary>
        /// �����������ɫ
        /// </summary>
        /// <param name="p_trnParent">���</param>
        private void m_mthSetCanSearchNodeColor(TreeNode p_trnParent)
        {
            foreach (TreeNode node in p_trnParent.Nodes)
                node.ForeColor = Color.Blue;
        } 
        #endregion

        #region �������������Ŀ
        /// <summary>
        /// �������������Ŀ
        /// </summary>
        private void m_mthAddComboItem()
        {
            m_cboDateConditionType.AddRangeItems(m_objGetExpItemArr("DATE"));
            m_cboDateConditionType.SelectedIndex = 0;

            m_cboLongTextConditionType.AddRangeItems(m_objGetExpItemArr("STRING"));
            m_cboLongTextConditionType.SelectedIndex = 0;

            m_cboNumberConditionType.AddRangeItems(m_objGetExpItemArr("INT"));
            m_cboNumberConditionType.SelectedIndex = 0;
        } 
        #endregion

        #region ���ò�ѯ����
        /// <summary>
        /// ���ò�ѯ����
        /// </summary>
        /// <param name="p_strControlName">�ؼ���</param>
        /// <param name="p_strControlDesc">�ؼ�����</param>
        /// <param name="p_strFieldName">��ѯ����</param>
        /// <param name="p_strDataType">��������</param>
        /// <returns></returns>
        private clsDataSearchesType m_objGetDateSearchesType(string p_strControlName, string p_strControlDesc, string p_strFieldName, string p_strDataType)
        {
            clsDataSearchesType objSearchesType = new clsDataSearchesType();
            objSearchesType.m_strDataType = p_strDataType;
            objSearchesType.m_strFieldName = p_strFieldName;

            objSearchesType.m_strItemID = p_strControlName;
            objSearchesType.m_strItemDesc = p_strControlDesc;
            if (p_strControlName != "")
                objSearchesType.m_strItemType = "ctlRichTextBox";
            return objSearchesType;
        } 
        #endregion

        #region ʵ��bool���Ͳ�ѯ��checkbox��ѡ
        private void m_chkTrueFalseFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkTrueFalseFalse.Checked)
            {
                m_chkTrueFalseTrue.Checked = false;
            }
        }

        private void m_chkTrueFalseTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkTrueFalseTrue.Checked)
            {
                m_chkTrueFalseFalse.Checked = false;
            }
        } 
        #endregion

        #region �����б�DoubleClickɾ��ѡ��item
        private void m_lstConditionList_DoubleClick(object sender, EventArgs e)
        {
            if (m_lstConditionList.SelectedItems.Count > 0)
                m_lstConditionList.Items.Remove(m_lstConditionList.SelectedItems[0]);
        } 
        #endregion

        #region ��������б�
        private void m_cmdClearCondition_Click(object sender, EventArgs e)
        {
            m_lstConditionList.Items.Clear();
        } 
        #endregion

        #region ��������������б�
        private void m_cmdAddCondition_Click(object sender, EventArgs e)
        {
            if (m_trvMain.SelectedNode == null)
                return;
            clsDataSearchesType objSearchesType = m_trvMain.SelectedNode.Tag as clsDataSearchesType;
            if (objSearchesType == null)
            {
                return;
            }
            clsDateSearchesCondition objCondition = m_objSetInputCondition();
            if (objCondition == null)
                return;
            for (int i = 0; i < m_lstConditionList.Items.Count; i++)
            {
                //���Ʋ��ܲ����ظ�����
                if (((clsDateSearchesCondition)m_lstConditionList.Items[i]).m_strSearchesSQL == objCondition.m_strSearchesSQL)
                    return;
            }

            m_lstConditionList.Items.Add(objCondition);
        } 
        #endregion

        #region ��ò�ѯ����
        /// <summary>
        /// ��ò�ѯ����
        /// </summary>
        private clsDateSearchesCondition m_objSetInputCondition()
        {
            clsDataSearchesType objSearchesType = m_trvMain.SelectedNode.Tag as clsDataSearchesType;
            clsKeyAndFormName objKeyAndFormName = m_objGetParnetTag(m_trvMain.SelectedNode.Parent);
            if (objSearchesType == null || objKeyAndFormName == null)
                return null;

            clsDateSearchesCondition objCondition = new clsDateSearchesCondition();

            string strConn = "����:";
            string strConnType = " and ";
            if (m_cboConn.SelectedIndex == 1)
            {
                strConnType = " or ";
                strConn = "  ��:";
            }

            if (m_pnlLongText.Visible)
            {
                if (m_txtLongTextContent.Text.TrimEnd() == "")
                {
                    MDIParent.ShowInformationMessageBox("�������ܶ�Ϊ�գ�����д���ʵ�������");
                    m_txtLongTextContent.Focus();
                    return null;
                }
                clsExpressionInfo obj = m_cboLongTextConditionType.SelectedItem as clsExpressionInfo;

                objCondition.m_strName = objSearchesType.m_strItemDesc;
                objCondition.m_objSelectedItem = (clsExpressionInfo)m_cboLongTextConditionType.SelectedItem;
                objCondition.m_strFirstValue = m_txtLongTextContent.Text.TrimEnd();
                objCondition.m_strSQLKey = objKeyAndFormName.m_strSQLKey;
                objCondition.m_strSearchesSQL = strConnType + objSearchesType.m_strFieldName.Replace("<CONDITION>", obj.m_strExpressionArr.Replace("<CONTENT>", m_txtLongTextContent.Text.TrimEnd()));
                objCondition.m_strConnTypeDesc = strConn;
            }
            else if (m_pnlDate.Visible)
            {
                clsExpressionInfo obj = m_cboDateConditionType.SelectedItem as clsExpressionInfo;

                objCondition.m_strName = objSearchesType.m_strItemDesc;
                objCondition.m_objSelectedItem = (clsExpressionInfo)m_cboDateConditionType.SelectedItem;
                objCondition.m_strFirstValue = "<FIRST>" + m_dtpFirst.Value.ToString("yyyy��MM��dd�� HHʱ");
                objCondition.m_strSecondValue = (m_dtpSecond.Visible ? "<SECOND>" + m_dtpSecond.Value.ToString("yyyy��MM��dd�� HHʱ") : "");
                objCondition.m_strSQLKey = objKeyAndFormName.m_strSQLKey;
                objCondition.m_strSearchesSQL = strConnType + objSearchesType.m_strFieldName.Replace("<CONDITION>", obj.m_strExpressionArr.Replace("<FIRSTDATE>", clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(m_dtpFirst.Value)).Replace("<ENDDATE>", clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(m_dtpSecond.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"))));
                objCondition.m_strConnTypeDesc = strConn;
            }
            else if (m_pnlTrueFalse.Visible)
            {
                if (m_chkTrueFalseTrue.Checked == false && m_chkTrueFalseFalse.Checked == false)
                {
                    MDIParent.ShowInformationMessageBox("�������ܶ�Ϊ�գ���ѡ��һ����");
                    return null;
                }

                objCondition.m_strName = objSearchesType.m_strItemDesc;
                objCondition.m_blnBoolValue = m_chkTrueFalseTrue.Checked;
                objCondition.m_strSQLKey = objKeyAndFormName.m_strSQLKey;
                objCondition.m_strSearchesSQL = strConnType + objSearchesType.m_strFieldName.Replace("<CONDITION>", (m_chkTrueFalseTrue.Checked ? " > 0 " : " <= 0 "));
                objCondition.m_strConnTypeDesc = strConn;
            }
            else if (m_pnlNumber.Visible)
            {
                if ((m_txtNumberFrom.Text.Trim() == "" && m_txtNumberTo.Visible && m_txtNumberTo.Text.Trim() == "") || (m_txtNumberFrom.Text.Trim() == "" && !m_txtNumberTo.Visible))
                {
                    MDIParent.ShowInformationMessageBox("�������ܶ�Ϊ�գ�����д���ʵ�������");
                    m_txtNumberFrom.Focus();
                    return null;
                }
                clsExpressionInfo obj = m_cboNumberConditionType.SelectedItem as clsExpressionInfo;

                objCondition.m_strName = objSearchesType.m_strItemDesc;
                objCondition.m_objSelectedItem = (clsExpressionInfo)m_cboNumberConditionType.SelectedItem;
                objCondition.m_strFirstValue = "<FIRST>" + m_txtNumberFrom.Text.Trim();
                objCondition.m_strSecondValue = (m_txtNumberTo.Visible ? "<SECOND>" + m_txtNumberTo.Text.Trim() : "");
                objCondition.m_strSQLKey = objKeyAndFormName.m_strSQLKey;
                objCondition.m_strSearchesSQL = strConnType + objSearchesType.m_strFieldName.Replace("<CONDITION>", obj.m_strExpressionArr.Replace("<FIRSTNUMBER>", m_txtNumberFrom.Text.Trim()).Replace("<ENDNUMBER>", m_txtNumberTo.Text.Trim()));
                objCondition.m_strConnTypeDesc = strConn;
            }
            return objCondition;
        } 
        #endregion

        #region ���ؽ��Tag��clsKeyAndFormName����
        /// <summary>
        /// ���ؽ��Tag��clsKeyAndFormName����
        /// </summary>
        /// <param name="p_trnParent">���</param>
        /// <returns></returns>
        private clsKeyAndFormName m_objGetParnetTag(TreeNode p_trnParent)
        {
            if (p_trnParent != null)
            {
                if (p_trnParent.Tag is clsKeyAndFormName)
                    return (clsKeyAndFormName)p_trnParent.Tag;
                else
                    return m_objGetParnetTag(p_trnParent.Parent);
            }
            else
                return null;
        } 
        #endregion

        #region ��ѯ��¼
        private void m_cmdSearch_Click(object sender, EventArgs e)
        {
            m_lsvResultList.Items.Clear();
            m_mthQuery();
        } 
        #endregion

        #region ���������еĴ��壬��ѯ��¼
        /// <summary>
        /// ���������еĴ��壬��ѯ��¼
        /// </summary>
        private void m_mthQuery()
        {
            frmBusynessForm frm = new frmBusynessForm("�������������Ժ�...");
            frm.BusynessEvent += new iCare.frmBusynessForm.BusynessHandler(frm_BusynessEvent);
            frm.ShowDialog();
        } 

        private void frm_BusynessEvent(object sender, BusynessEventArgs e)
        {
            try
            {
                Hashtable hasKey = new Hashtable();
                string strMainSql = @"select distinct <S1>.PatientName,<S1>.inpatientid,<S1>.inpatientdate,<S1>.InDept,<S1>.OutDate,<S1>.PatientSex from ";
                if (m_lstConditionList.Items.Count <= 0)
                {
                    return;
                }
                else
                {
                    bool blnCanReturn = true;
                    //Ĭ����ӣ�����û������
                    hasKey.Add("S1", m_strGetMainSQL("S1"));
                    for (int j = 0; j < m_lstConditionList.Items.Count; j++)
                    {
                        blnCanReturn = false;
                        clsDateSearchesCondition objCondition = m_lstConditionList.Items[j] as clsDateSearchesCondition;
                        if (hasKey.ContainsKey(objCondition.m_strSQLKey))
                        {
                            //���Ϊͬһ��SQL����ֻ���Where����
                            string strTemp = (string)hasKey[objCondition.m_strSQLKey];
                            strTemp += objCondition.m_strSearchesSQL;
                            hasKey[objCondition.m_strSQLKey] = strTemp;
                        }
                        else
                        {
                            string strSql = m_strGetMainSQL(objCondition.m_strSQLKey) + objCondition.m_strSearchesSQL;
                            hasKey.Add(objCondition.m_strSQLKey, strSql);
                        }
                    }
                    if (blnCanReturn)
                    {
                        if (m_lsvResultList.Items.Count > 0)
                            m_lsvResultList.Items[0].Selected = true;
                        e.EndMessage = "�������";
                        e.Closing = true;
                        m_mthAddResultToListView();
                        m_mthClear();
                        return;
                    }
                    if (hasKey.Count > 0)//����ȫ�����������տ�ִ��SQL
                    {
                        string[] strKeysArr = new string[hasKey.Count];
                        hasKey.Keys.CopyTo(strKeysArr, 0);
                        for (int k = 0; k < hasKey.Count; k++)
                        {
                            strMainSql += (k == 0 ? "(" + hasKey[strKeysArr[k]] + ") " + strKeysArr[k] : " inner join (" + hasKey[strKeysArr[k]] + ") " + strKeysArr[k]);
                            if (k > 0)
                                strMainSql += " on " + strKeysArr[k - 1] + ".inpatientid = " + strKeysArr[k] + ".inpatientid and " + strKeysArr[k - 1] + ".inpatientdate = " + strKeysArr[k] + ".inpatientdate ";
                        }
                        strMainSql += " order by <S1>.inpatientid";
                        strMainSql = strMainSql.Replace("<S1>", strKeysArr[0]);
                    }
                }
                DataTable dtValue = new DataTable();
                clsRecordSearchDomain objDomain = new clsRecordSearchDomain();
                objDomain.m_lngSearchesBySQL(strMainSql, ref dtValue);
                e.Messages = "������ӵ��б�...";
                //��Ӳ��ҽ���б�
                if (dtValue.Rows.Count > 0)
                {
                    if (m_hasPatientIDAndDate.Count > 0)
                    {
                        string[] strKeysArr2 = new string[m_hasPatientIDAndDate.Count];
                        m_hasPatientIDAndDate.Keys.CopyTo(strKeysArr2, 0);
                        foreach (string key in strKeysArr2)
                        {
                            bool blnIsFound = false;
                            foreach (DataRow row in dtValue.Rows)
                            {
                                if (key == row["INPATIENTID"].ToString().Trim() + DateTime.Parse(row["INPATIENTDATE"].ToString()).ToString("yyyy��MM��dd��"))
                                {
                                    blnIsFound = true;
                                    break;
                                }
                            }
                            if (!blnIsFound)
                            {
                                m_hasPatientIDAndDate.Remove(key);
                                string[] strKeysArr3 = new string[m_hasPatientID.Count];
                                m_hasPatientID.Keys.CopyTo(strKeysArr3, 0);
                                foreach (string key1 in strKeysArr2)
                                {
                                    blnIsFound = false;
                                    foreach (DataRow row in dtValue.Rows)
                                    {
                                        if (key1 == row["INPATIENTID"].ToString().Trim())
                                        {
                                            blnIsFound = true;
                                            break;
                                        }
                                    }
                                    if (!blnIsFound)
                                        m_hasPatientID.Remove(key1);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtValue.Rows.Count; i++)
                        {
                            string strPatientID = dtValue.Rows[i]["INPATIENTID"].ToString().Trim();
                            string strInPatientDate = DateTime.Parse(dtValue.Rows[i]["INPATIENTDATE"].ToString()).ToString("yyyy��MM��dd��");
                            if (m_hasPatientIDAndDate.ContainsKey(strPatientID + strInPatientDate))
                            {
                                continue;
                            }
                            m_hasPatientIDAndDate.Add(strPatientID + strInPatientDate, dtValue.Rows[i]);
                            if (!m_hasPatientID.ContainsKey(strPatientID))
                                m_hasPatientID.Add(strPatientID, "");
                        }
                    }
                }
                e.EndMessage = "�������";
                e.Closing = true;
            }
            catch
            {
                e.EndMessage = "����ʧ��";
                e.Closing = false;
            }
            finally
            {
                m_mthAddResultToListView();
                m_mthClear();
            }
        }
        #endregion

        #region ��ʾ�����ListView
        /// <summary>
        /// ��ʾ�����ListView
        /// </summary>
        private void m_mthAddResultToListView()
        {
            try
            {
                m_lsvResultList.Items.Clear();
                m_lsvResultList.BeginUpdate();
                if (m_hasPatientIDAndDate.Count > 0)
                {
                    string[] strKeysArr = new string[m_hasPatientIDAndDate.Count];
                    m_hasPatientIDAndDate.Keys.CopyTo(strKeysArr, 0);
                    foreach (string key in strKeysArr)
                    {
                        DataRow drList = m_hasPatientIDAndDate[key] as DataRow;
                        if (drList != null)
                        {
                            ListViewItem item = new ListViewItem(new string[] { drList["INPATIENTID"].ToString().Trim(), 
                            drList["PatientName"].ToString(), 
                            drList["PatientSex"].ToString(), 
                            DateTime.Parse(drList["inpatientdate"].ToString()).ToString("yyyy��MM��dd��"), 
                            DateTime.Parse(drList["OutDate"].ToString()).ToString("yyyy��MM��dd��") });
                            item.Tag = drList;
                            m_lsvResultList.Items.Add(item);
                        }
                    }
                }
            }
            finally
            {
                m_lsvResultList.EndUpdate();
                m_lblResultNums.Text = "��������" + m_lsvResultList.Items.Count.ToString() + "����¼";
            }
            if (m_lsvResultList.Items.Count > 0)
            {
                m_lsvResultList.Focus();
                m_lsvResultList.Items[0].Selected = true;
            }
        } 
        #endregion

        #region ���HashTable
        /// <summary>
        /// ���HashTable
        /// </summary>
        private void m_mthClear()
        {
            m_hasPatientID.Clear();
            m_hasPatientIDAndDate.Clear();
        } 
        #endregion

        #region ��ղ�ѯ���ü�¼�б�
        private void m_cmdClearResult_Click(object sender, EventArgs e)
        {
            m_lsvResultList.Items.Clear();
        } 
        #endregion

        #region ��ʼ������Ĳ�ѯ���
        private void m_mthInitArray()
        {
            #region ��ѯ���˻�������
            string strSql = @"select distinct a.INPATIENTID_CHR inpatientid,
                                            b.INPATIENT_DAT   inpatientdate,
                                            e.deptname_vchr   InDept,
                                            a.LASTNAME_VCHR   PatientName,
                                            d.modify_dat      OutDate,
                                            d.deptname_vchr   OutDept,
                                            a.SEX_CHR         PatientSex
							from T_BSE_PATIENT a
							inner join T_OPR_BIH_REGISTER b on a.PATIENTID_CHR = b.PATIENTID_CHR
							inner join (select l.REGISTERID_CHR,
                                                l.MODIFY_DAT,
                                                l.status_int,
                                                td1.deptname_vchr
                                           from T_OPR_BIH_LEAVE l, T_BSE_DEPTDESC td1
                                          where l.outdeptid_chr = td1.DEPTID_CHR) d on d.status_int = 1
                                                                                  and d.REGISTERID_CHR =
                                                                                      b.REGISTERID_CHR
                             inner join (select tl.REGISTERID_CHR, td2.deptname_vchr
                                           from T_OPR_BIH_TRANSFER tl, T_BSE_DEPTDESC td2
                                          where tl.TARGETDEPTID_CHR = td2.DEPTID_CHR
                                            and tl.modify_dat =
                                                (select min(modify_dat)
                                                   from T_OPR_BIH_TRANSFER
                                                  where tl.registerid_chr = registerid_chr)) e on e.registerid_chr =
                                                                                                  b.registerid_chr
                            inner join T_EMR_INHOSPITALMAINREC_GXCON IHMC on IHMC.registerid_chr = b.registerid_chr
                                                                         and IHMC.CATALOG_DATE is NOT NULL
                          left outer join (select tl2.TARGETDEPTID_CHR,
                                                  tl2.MODIFY_DAT,
                                                  td3.deptname_vchr,
                                                  tl2.registerid_chr
                                             from T_OPR_BIH_TRANSFER tl2, T_BSE_DEPTDESC td3
                                            where tl2.sourcedeptid_chr is not null
                                              and tl2.targetdeptid_chr is not null
                                              and tl2.targetdeptid_chr = td3.deptid_chr) f on f.registerid_chr =
                                                                                              b.registerid_chr
                             where 1 = 1";
            m_hstMainSql.Add("S1", strSql); 
            #endregion

            #region ������ҳ�������
            strSql = @"select distinct IHMD.inpatientid, IHMD.inpatientdate
						from T_EMR_INHOSPITALMAINREC_GXOD IHMD,T_EMR_INHOSPITALMAINREC_GXCON IHMC
						where IHMD.EMR_SEQ = IHMC.EMR_SEQ 
                        and IHMC.CATALOG_DATE is NOT NULL";
            m_hstMainSql.Add("S10", strSql); 
            #endregion

            #region ������ҳ������Ϣ
            strSql = @"select distinct IHMO.inpatientid, IHMO.inpatientdate
						from T_EMR_INHOSPITALMAINREC_GXOP IHMO ,T_EMR_INHOSPITALMAINREC_GXCON IHMC
						where IHMO.EMR_SEQ = IHMC.EMR_SEQ 
                        and IHMC.CATALOG_DATE is NOT NULL";
            m_hstMainSql.Add("S11", strSql);            
            #endregion

            #region ������ҳ��Ժ���
            strSql = @"select distinct IHMI.inpatientid, IHMI.inpatientdate
						from T_EMR_INHOSPITALMAINREC_GXID IHMI ,T_EMR_INHOSPITALMAINREC_GXCON IHMC
						where IHMI.EMR_SEQ = IHMC.EMR_SEQ 
                        and IHMC.CATALOG_DATE is NOT NULL";
            m_hstMainSql.Add("S12", strSql); 
            #endregion
        } 
        #endregion

        #region ����HashTable��ֵ��ȡ�����ѯ���
        /// <summary>
        /// ����HashTable��ֵ��ȡ�����ѯ���
        /// </summary>
        /// <param name="p_strKey">��ֵ</param>
        /// <returns></returns>
        private string m_strGetMainSQL(string p_strKey)
        {
            if (m_hstMainSql.Contains(p_strKey))
                return m_hstMainSql[p_strKey] as string;
            return null;
        } 
        #endregion

        #region �����������ͷ��ر��ʽ
        /// <summary>
        /// �����������ͷ��ر��ʽ
        /// </summary>
        /// <param name="p_strType">������������</param>
        /// <returns></returns>
        private clsExpressionInfo[] m_objGetExpItemArr(string p_strType)
        {
            clsExpressionInfo[] obj = null;
            string[] strItemArr = null;
            string[] strConditionArr = null;
            switch (p_strType)
            {
                case "STRING":
                    strItemArr = new string[] { "���ݰ���", "������", "���ݿ�ͷ��", "���ݽ�β��" };
                    strConditionArr = new String[strItemArr.Length];
                    strConditionArr[0] = @" like '%<CONTENT>%'";
                    strConditionArr[1] = @" = '<CONTENT>'";
                    strConditionArr[2] = @" like '<CONTENT>%'";
                    strConditionArr[3] = @" like '%<CONTENT>'";
                    obj = new clsExpressionInfo[strItemArr.Length];
                    for (int i = 0; i < strItemArr.Length; i++)
                    {
                        obj[i] = new clsExpressionInfo();
                        obj[i].m_strType = strItemArr[i];
                        obj[i].m_strExpressionArr = strConditionArr[i];
                    }
                    return obj;
                case "INT":
                    strItemArr = new string[] { "��Χ", "����", "���ڵ���", "С��", "С�ڵ���", "����", "������" };
                    strConditionArr = new String[strItemArr.Length];
                    strConditionArr[0] = @" between <FIRSTNUMBER> and <ENDNUMBER> ";
                    strConditionArr[1] = @" > <FIRSTNUMBER>";
                    strConditionArr[2] = @" >= <FIRSTNUMBER>";
                    strConditionArr[3] = @" < <FIRSTNUMBER>";
                    strConditionArr[4] = @" <= <FIRSTNUMBER>";
                    strConditionArr[5] = @" = <FIRSTNUMBER>";
                    strConditionArr[6] = @"<> <FIRSTNUMBER>";
                    obj = new clsExpressionInfo[strItemArr.Length];
                    for (int k = 0; k < strItemArr.Length; k++)
                    {
                        obj[k] = new clsExpressionInfo();
                        obj[k].m_strType = strItemArr[k];
                        obj[k].m_strExpressionArr = strConditionArr[k];
                    }
                    return obj;
                case "DATE":
                    strItemArr = new string[] { "���ڷ�Χ", "������" };
                    strConditionArr = new String[strItemArr.Length];
                    strConditionArr[0] = " between <FIRSTDATE> and <ENDDATE> ";
                    strConditionArr[1] = " = <FIRSTDATE> ";
                    obj = new clsExpressionInfo[strItemArr.Length];
                    for (int j = 0; j < strItemArr.Length; j++)
                    {
                        obj[j] = new clsExpressionInfo();
                        obj[j].m_strType = strItemArr[j];
                        obj[j].m_strExpressionArr = strConditionArr[j];
                    }
                    return obj;
                default:
                    break;
            }
            return null;
        } 
        #endregion

        #region ѡ��m_trvMain��㼤���¼�
        private void m_trvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (m_trvMain.SelectedNode == null || m_trvMain.SelectedNode.Tag == null || m_trvMain.SelectedNode.Parent == null)
            {
                m_cmdAddCondition.Enabled = false;
                return;
            }
            string strType = "";
            clsDataSearchesType objSearchesType = m_trvMain.SelectedNode.Tag as clsDataSearchesType;
            if (objSearchesType != null)
                strType = objSearchesType.m_strDataType;
            m_mthSetInputVisible(strType);
        } 
        #endregion

        #region ��ʾ��ѯ��������
        /// <summary>
        /// ��ʾ��ѯ��������
        /// </summary>
        /// <param name="p_strType">��������</param>
        private void m_mthSetInputVisible(string p_strType)
        {
            m_txtLongTextContent.Text = "";
            m_txtNumberFrom.Text = "";
            m_txtNumberTo.Text = "";
            m_pnlLongText.Visible = (p_strType == "STRING");
            m_pnlTrueFalse.Visible = (p_strType == "BOOL");
            m_pnlDate.Visible = (p_strType == "DATE");
            m_pnlNumber.Visible = (p_strType == "INT");
            m_cmdAddCondition.Enabled = (m_pnlLongText.Visible || m_pnlDate.Visible || m_pnlTrueFalse.Visible || m_pnlNumber.Visible);
        } 
        #endregion

        #region ����m_cboDateConditionTypeѡ���ͬ��ʾ��ͬ����ؼ�
        private void m_cboDateConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_cboDateConditionType.SelectedIndex < 0)
                return;
            bool blnVisible = (m_cboDateConditionType.SelectedIndex == 0);
            m_lblDateFrom.Visible = blnVisible;
            m_lblDateTo.Visible = blnVisible;
            m_dtpSecond.Visible = blnVisible;
            if (blnVisible)//���Ƴ�ʼ����Ϊ��ǰ��
            {
                m_dtpFirst.Text = DateTime.Now.ToString("yyyy-MM-01 00:00:00");
                m_dtpSecond.Text = m_dtpFirst.Value.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 00:00:00");
            }
        } 
        #endregion

        #region ����m_cboNumberConditionTypeѡ���ͬ��ʾ��ͬ����ؼ�
        private void m_cboNumberConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_cboNumberConditionType.SelectedIndex < 0)
                return;
            bool blnVisible = (m_cboNumberConditionType.SelectedIndex == 0);
            m_lblNumberTo.Visible = blnVisible;
            m_txtNumberTo.Visible = blnVisible;
        } 
        #endregion

        #region ˫��m_lsvResultListѡ��Item�򿪲�����ҳ����
        private void m_lsvResultList_DoubleClick(object sender, EventArgs e)
        {
            if (m_lsvResultList.Items.Count > 0 && m_lsvResultList.SelectedItems.Count > 0)
            {
                clsPatient objPa = new clsPatient(m_lsvResultList.SelectedItems[0].SubItems[0].Text.Trim());

                frmInHospitalMainRecord_GXForCH frmRecord = new frmInHospitalMainRecord_GXForCH();
                frmRecord.MdiParent = this.MdiParent;
                frmRecord.Show();
                frmRecord.m_mthSetSelectedPatient(objPa);
            }
        } 
        #endregion
    }
}