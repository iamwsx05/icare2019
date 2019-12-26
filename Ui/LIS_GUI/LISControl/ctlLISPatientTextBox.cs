using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.controls;
namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// ctlLISPatientTextBox ��ժҪ˵����
    /// ���� 2004.06.29
    /// </summary>
    public class ctlLISPatientTextBox : com.digitalwave.Utility.ctlExtTextBox
    {
        /// <summary> 
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;


        public ctlLISPatientTextBox()
        {
            // �õ����� Windows.Forms ���������������ġ�
            InitializeComponent();

            // TODO: �� InitializeComponent ���ú�����κγ�ʼ��
        }

        /// <summary> 
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strInput"></param>
        protected override void m_mthLoadListData(string p_strInput)
        {
            DataTable dvbResult = null;

            long lngRes = new clsDomainController_ApplicationManage().m_lngGetPatientInfoVOList(p_strInput, out dvbResult);

            ListViewItem lviTemp = null;

            if (dvbResult == null || dvbResult.Rows.Count <= 0)
            {
                DataTable dtbResult = null;

                lngRes = new clsDomainController_ApplicationManage().m_lngGetPatientInfoByCondition(p_strInput, out dtbResult);
                if (dtbResult == null || dtbResult.Rows.Count <= 0)
                    return;

                List<clsPatientTextBoxValue> lstPat = new List<clsPatientTextBoxValue>();
                for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                {
                    clsPatientTextBoxValue objPatientInfo = new clsPatientTextBoxValue();
                    objPatientInfo.strInpatientNo = p_strInput;
                    objPatientInfo.strPatientID = dtbResult.Rows[i1]["patientid_chr"].ToString().Trim();
                    objPatientInfo.strPatientName = dtbResult.Rows[i1]["patient_name_vchr"].ToString().Trim();
                    objPatientInfo.strSex = dtbResult.Rows[i1]["sex_chr"].ToString().Trim();
                    objPatientInfo.strAge = dtbResult.Rows[i1]["age_chr"].ToString().Trim();//
                    objPatientInfo.strPatientBedNO = dtbResult.Rows[i1]["bedno_chr"].ToString().Trim();
                    objPatientInfo.strEmpName = dtbResult.Rows[i1]["employeename"].ToString().Trim();
                    objPatientInfo.strdeptName = dtbResult.Rows[i1]["deptname_vchr"].ToString().Trim();
                    objPatientInfo.strEmpID = dtbResult.Rows[i1]["appl_empid_chr"].ToString().Trim();
                    objPatientInfo.strDeptID = dtbResult.Rows[i1]["appl_deptid_chr"].ToString().Trim();
                    objPatientInfo.strSepcial = dtbResult.Rows[i1]["special_int"].ToString().Trim();
                    objPatientInfo.strPatientype = dtbResult.Rows[i1]["patient_type_id_chr"].ToString().Trim();
                    objPatientInfo.strDiagnose = dtbResult.Rows[i1]["diagnose_vchr"].ToString().Trim();

                    if (lstPat.Exists(t => t.strInpatientNo == objPatientInfo.strInpatientNo && t.strPatientID == objPatientInfo.strPatientID &&
                        t.strPatientName == objPatientInfo.strPatientName && t.strSex == objPatientInfo.strSex && t.strAge == objPatientInfo.strAge &&
                        t.strPatientBedNO == objPatientInfo.strPatientBedNO && t.strEmpName == objPatientInfo.strEmpName && t.strdeptName == objPatientInfo.strdeptName &&
                        t.strEmpID == objPatientInfo.strEmpID && t.strDeptID == objPatientInfo.strDeptID && t.strSepcial == objPatientInfo.strSepcial &&
                        t.strPatientype == objPatientInfo.strPatientype && t.strDiagnose == objPatientInfo.strDiagnose))
                    {
                        continue;
                    }
                    else
                    {
                        lstPat.Add(objPatientInfo);
                    }

                    lviTemp = new ListViewItem(dtbResult.Rows[i1]["patient_inhospitalno_chr"].ToString().Trim());
                    lviTemp.SubItems.Add(dtbResult.Rows[i1]["patient_name_vchr"].ToString().Trim());
                    lviTemp.SubItems.Add(dtbResult.Rows[i1]["sex_chr"].ToString().Trim());
                    lviTemp.SubItems.Add(dtbResult.Rows[i1]["age_chr"].ToString().Trim());
                    lviTemp.SubItems.Add(dtbResult.Rows[i1]["deptname_vchr"].ToString().Trim());
                    lviTemp.SubItems.Add(dtbResult.Rows[i1]["employeename"].ToString().Trim());
                    lviTemp.SubItems.Add(dtbResult.Rows[i1]["bedno_chr"].ToString().Trim());

                    lviTemp.Tag = objPatientInfo;
                    this.m_lsvList.Items.Add(lviTemp);
                }
            }
            else
            {
                clsPatientTextBoxValue objPatientInfo;
                objPatientInfo = new clsPatientTextBoxValue();
                DataRow drTemp = null;
                for (int i = 0; i < dvbResult.Rows.Count; i++)
                {
                    drTemp = dvbResult.Rows[i];
                    objPatientInfo.strInpatientNo = p_strInput;
                    objPatientInfo.strPatientID = dvbResult.Rows[i]["patientid_chr"].ToString().Trim();
                    objPatientInfo.strPatientName = dvbResult.Rows[i]["patientname"].ToString().Trim();
                    objPatientInfo.strSex = dvbResult.Rows[i]["sex_chr"].ToString().Trim();
                    string strBirth = dvbResult.Rows[i]["birth_dat"].ToString().Trim();
                    DateTime dtmBirth;
                    try
                    {
                        objPatientInfo.strAge = frmHandInputReport.m_mthGetAge(strBirth);
                    }
                    catch { };
                    objPatientInfo.strPatientBedNO = dvbResult.Rows[i]["code_chr"].ToString().Trim();
                    objPatientInfo.strEmpName = dvbResult.Rows[i]["casedoctorname"].ToString().Trim();
                    objPatientInfo.strdeptName = dvbResult.Rows[i]["deptname_vchr"].ToString().Trim();
                    objPatientInfo.strSepcial = null;
                    objPatientInfo.strPatientype = "2";
                    objPatientInfo.strDiagnose = dvbResult.Rows[i]["diagnose_vchr"].ToString().Trim();
                    objPatientInfo.strDeptID = dvbResult.Rows[i]["deptid_chr"].ToString().Trim();
                    objPatientInfo.strEmpID = dvbResult.Rows[i]["casedoctor_chr"].ToString().Trim();
                    lviTemp = new ListViewItem(p_strInput);
                    lviTemp.SubItems.Add(objPatientInfo.strPatientName);
                    lviTemp.SubItems.Add(objPatientInfo.strSex);
                    lviTemp.SubItems.Add(objPatientInfo.strAge);
                    lviTemp.SubItems.Add(objPatientInfo.strdeptName);
                    lviTemp.SubItems.Add(objPatientInfo.strEmpName);
                    lviTemp.SubItems.Add(objPatientInfo.strPatientBedNO);
                    lviTemp.Tag = objPatientInfo;
                    this.m_lsvList.Items.Add(lviTemp);
                    if (i > 1)
                    {
                        this.m_lsvList.Show();
                    }
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void m_mthSetListAppearance()
        {

            this.m_lsvList.Clear();
            this.m_lsvList.Width = 570;
            this.m_lsvList.Columns.Add("סԺ��", 80, HorizontalAlignment.Left);
            this.m_lsvList.Columns.Add("����", 80, HorizontalAlignment.Left);
            this.m_lsvList.Columns.Add("�Ա�", 80, HorizontalAlignment.Left);
            this.m_lsvList.Columns.Add("����", 80, HorizontalAlignment.Left);
            this.m_lsvList.Columns.Add("�������", 80, HorizontalAlignment.Left);
            this.m_lsvList.Columns.Add("����ҽ��", 80, HorizontalAlignment.Left);
            this.m_lsvList.Columns.Add("����", 80, HorizontalAlignment.Left);
            this.m_lsvList.View = View.Details;
            this.m_lsvList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.m_lsvList.GridLines = true;
            this.m_lsvList.FullRowSelect = true;
            this.m_lsvList.MultiSelect = false;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object m_strGetValueObject()
        {
            if (this.m_lsvList.SelectedItems.Count != 0 && this.m_lsvList.SelectedItems[0].Tag != null)
                return this.m_lsvList.SelectedItems[0].Tag;
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string m_strGetItemName()
        {
            if (this.m_ObjValueObject == null)
                return null;
            else
                return ((clsPatientTextBoxValue)this.m_ObjValueObject).strInpatientNo;
        }


        protected override bool m_blnCheckTextBoxStringIfEffect()
        {
            return true;
        }


        /// <summary>
        /// ��ò������ݵ�DataRow
        /// </summary>
        public clsPatientTextBoxValue m_dtrPatient
        {
            get
            {
                if (this.m_ObjValueObject != null)
                {
                    return (clsPatientTextBoxValue)this.m_ObjValueObject;
                }
                else
                {
                    return null;
                }
            }
        }
        #region �����������ɵĴ���
        /// <summary> 
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
        /// �޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Name = "ctlLISPatientTextBox1";
            this.Size = new System.Drawing.Size(120, 21);
        }
        #endregion
    }

    /// <summary>
    /// ������Ϣ��װ��
    /// </summary>
    public class clsPatientTextBoxValue
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public string strPatientID;

        /// <summary>
        /// ��������
        /// </summary>
        public string strPatientName;

        /// <summary>
        /// �����Ա�
        /// </summary>
        public string strSex;

        /// <summary>
        /// ��������
        /// </summary>
        public string strAge;

        /// <summary>
        /// ���˴���
        /// </summary>
        public string strPatientBedNO;

        /// <summary>
        /// ���벿��
        /// </summary>
        public string strdeptName;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public string strEmpName;

        /// <summary>
        /// �Ƿ����⴦��
        /// </summary>
        public string strSepcial;
        /// <summary>
        /// ���벿��ID
        /// </summary>
        public string strDeptID;
        /// <summary>
        /// ����ҽ��ID
        /// </summary>
        public string strEmpID;
        /// <summary>
        /// ��������
        /// </summary>
        public string strPatientype;
        /// <summary>
        /// ���
        /// </summary>
        public string strDiagnose;
        /// <summary>
        /// ����סԺ��
        /// </summary>
        public string strInpatientNo;
    }
}