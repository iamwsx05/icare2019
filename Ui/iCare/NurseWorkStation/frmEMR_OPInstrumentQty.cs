using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using com.digitalwave.Emr.Signature_gui;
using weCare.Core.Entity;
using com.digitalwave.emr.BEDExplorer;

namespace iCare
{
    /// <summary>
    /// ������е�����ϵ�����
    /// </summary>
    public partial class frmEMR_OPInstrumentQty : iCare.frmDiseaseTrackBase
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ��ΪDataGrid����Դ��DataTable
        /// </summary>
        private DataTable m_dtbInstrumentItem = new DataTable();
        private clsHospitalManagerDomain objEmployeeSign = new clsHospitalManagerDomain();
        private clsEmrSignToolCollection m_objSign;
        /// <summary>
        /// ��ǰ��ʾ�ڽ������������Ŀ
        /// </summary>
        private clsEMR_OPInstrument_Dict[] m_objDictArr = null;
        private long m_lngCurrentEMR_SEQ = -1;
        #endregion

        #region ���캯��
        public frmEMR_OPInstrumentQty()
        {
            InitializeComponent();
            //ָ��ҽ������վ��
            intFormType = 1;
            m_mthInitDataTable();

            m_dtgInstrument.DataSource = m_dtbInstrumentItem;

            m_mthSetDataGridStyle();

            m_objSign = new clsEmrSignToolCollection();
            m_objSign.m_mthBindEmployeeSign(m_cmdRecorder, txtSign, 2, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdOperationer, m_lsvOperationer, 1, false, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdInstrumentNurse, m_lsvInstrumentNurse, 2, false, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdCheckNurse, m_lsvCheckNurse, 2, false, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdAssitantor, m_lsvAssitantor, 1, false, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdItinerationNurse, m_lsvItinerationNurse, 2, false, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdCheckNurseName, m_lsvCheckNurseName, 2, false, clsEMRLogin.LoginInfo.m_strEmpID);
        }
        #endregion

        #region ����

        #region ��ʼ������Դ
        /// <summary>
        /// ��ʼ������Դ
        /// </summary>
        private void m_mthInitDataTable()
        {
            string[] strColumnArr = new string[] { "Name1","BeforeOP1","BeforeClose1","AfterClose1",
            "Name2","BeforeOP2","BeforeClose2","AfterClose2",
            "Name3","BeforeOP3","BeforeClose3","AfterClose3"};

            for (int i = 0 ; i < strColumnArr.Length ; i++)
            {
                m_dtbInstrumentItem.Columns.Add(strColumnArr[i]);
            }
        }
        #endregion

        #region ����DataGrid��ʽ
        /// <summary>
        /// ����DataGrid��ʽ
        /// </summary>
        private void m_mthSetDataGridStyle()
        {
            if (m_dtgInstrument.ColumnCount != 12)
                return;
            for (int i = 0 ; i < 12 ; i++)
            {
                if (i == 0 || i == 4 || i == 8)
                {
                    m_dtgInstrument.Columns[i].HeaderText = @"����\����";
                    m_dtgInstrument.Columns[i].ReadOnly = true;
                    m_dtgInstrument.Columns[i].Width = 100;
                    m_dtgInstrument.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                if (i == 1 || i == 5 || i == 9)
                {
                    m_dtgInstrument.Columns[i].HeaderText = @"��ǰ  ���";
                    m_dtgInstrument.Columns[i].ReadOnly = false;
                    m_dtgInstrument.Columns[i].Width = 50;
                    ((DataGridViewTextBoxColumn)m_dtgInstrument.Columns[i]).MaxInputLength = 25;
                    m_dtgInstrument.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                if (i == 2 || i == 6 || i == 10)
                {
                    m_dtgInstrument.Columns[i].HeaderText = @"��ǰ  �˶�";
                    m_dtgInstrument.Columns[i].ReadOnly = false;
                    m_dtgInstrument.Columns[i].Width = 50;
                    ((DataGridViewTextBoxColumn)m_dtgInstrument.Columns[i]).MaxInputLength = 25;
                    m_dtgInstrument.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                if (i == 3 || i == 7 || i == 11)
                {
                    m_dtgInstrument.Columns[i].HeaderText = @"�غ�  �˶�";
                    m_dtgInstrument.Columns[i].ReadOnly = false;
                    m_dtgInstrument.Columns[i].Width = 50;
                    ((DataGridViewTextBoxColumn)m_dtgInstrument.Columns[i]).MaxInputLength = 25;
                    m_dtgInstrument.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }
        #endregion

        #region ���λ�������ǩ������
        protected override void m_mthSetSign(string p_strUserID)
        {
            return;
        }
        #endregion

        #region ��������¼��Ϣ
        /// <summary>
        /// ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            m_dtbInstrumentItem.Clear();
            m_mthSetActiveItemList();

            m_cmdRecorder.Enabled = true;
            txtSign.Text = "";
            txtSign.Tag = null;
            m_lsvAssitantor.Clear();
            m_lsvCheckNurse.Clear();
            m_lsvCheckNurseName.Clear();
            m_lsvInstrumentNurse.Clear();
            m_lsvItinerationNurse.Clear();
            m_lsvOperationer.Clear();

            m_lngCurrentEMR_SEQ = -1;//Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(txtSign);
        }
        #endregion

        #region �ӽ����ȡ�����¼��ֵ
        /// <summary>
        /// �ӽ����ȡ�����¼��ֵ���������ֵ��������null��
        /// </summary>
        /// <returns></returns>
        protected override clsTrackRecordContent m_objGetContentFromGUI()
        {
            //�������У��
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return null;

            if (txtSign.Tag == null)
            {
                clsPublicFunction.ShowInformationMessageBox("���¼��ǩ����");
                return null;
            }
            clsEMR_OPInstrument objInstrument = new clsEMR_OPInstrument();
            objInstrument.m_dtmRecordDate = m_dtpCreateDate.Value;
            clsEmrEmployeeBase_VO objCreat = txtSign.Tag as clsEmrEmployeeBase_VO;
            if (objCreat != null)
            {
                objInstrument.m_strCreateUserID = objCreat.m_strEMPNO_CHR;
            }
            objInstrument.m_strModifyUserID = clsEMRLogin.LoginInfo.m_strEmpNo;
            #region ��ȡǩ������
            int intSignCount = 0;

            intSignCount = m_lsvOperationer.Items.Count + m_lsvAssitantor.Items.Count + m_lsvInstrumentNurse.Items.Count
                + m_lsvItinerationNurse.Items.Count + m_lsvCheckNurse.Items.Count + m_lsvCheckNurseName.Items.Count + 1;
            objInstrument.objSignerArr = new clsEmrSigns_VO[intSignCount];
            m_mthGetSignArr(new Control[] { m_lsvOperationer, m_lsvAssitantor, m_lsvInstrumentNurse, m_lsvCheckNurseName, m_lsvCheckNurse, m_lsvItinerationNurse, txtSign }, ref objInstrument.objSignerArr, ref strUserIDList, ref strUserNameList);
            //int currentSignCount = 0;
            //for (int i = 0 ; i < m_lsvOperationer.Items.Count ; i++)
            //{
            //    objInstrument.objSignerArr[i] = new clsEmrSigns_VO();
            //    objInstrument.objSignerArr[i].objEmployee = new clsEmrEmployeeBase_VO();
            //    objInstrument.objSignerArr[i].objEmployee = (clsEmrEmployeeBase_VO)(m_lsvOperationer.Items[i].Tag);
            //    objInstrument.objSignerArr[i].controlName = "m_lsvOperationer";
            //    objInstrument.objSignerArr[i].m_strFORMID_VCHR = "frmEMR_OPInstrumentQty";
            //    objInstrument.objSignerArr[i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //}
            //currentSignCount = m_lsvOperationer.Items.Count;

            //for (int i = 0 ; i < m_lsvAssitantor.Items.Count ; i++)
            //{
            //    objInstrument.objSignerArr[currentSignCount + i] = new clsEmrSigns_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = new clsEmrEmployeeBase_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = (clsEmrEmployeeBase_VO)(m_lsvAssitantor.Items[i].Tag);
            //    objInstrument.objSignerArr[currentSignCount + i].controlName = "m_lsvAssitantor";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strFORMID_VCHR = "frmEMR_OPInstrumentQty";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //}
            //currentSignCount += m_lsvAssitantor.Items.Count;

            //for (int i = 0 ; i < m_lsvInstrumentNurse.Items.Count ; i++)
            //{
            //    objInstrument.objSignerArr[currentSignCount + i] = new clsEmrSigns_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = new clsEmrEmployeeBase_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = (clsEmrEmployeeBase_VO)(m_lsvInstrumentNurse.Items[i].Tag);
            //    objInstrument.objSignerArr[currentSignCount + i].controlName = "m_lsvInstrumentNurse";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strFORMID_VCHR = "frmEMR_OPInstrumentQty";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //}
            //currentSignCount += m_lsvInstrumentNurse.Items.Count;

            //for (int i = 0 ; i < m_lsvItinerationNurse.Items.Count ; i++)
            //{
            //    objInstrument.objSignerArr[currentSignCount + i] = new clsEmrSigns_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = new clsEmrEmployeeBase_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = (clsEmrEmployeeBase_VO)(m_lsvItinerationNurse.Items[i].Tag);
            //    objInstrument.objSignerArr[currentSignCount + i].controlName = "m_lsvItinerationNurse";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strFORMID_VCHR = "frmEMR_OPInstrumentQty";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //}
            //currentSignCount += m_lsvItinerationNurse.Items.Count;

            //for (int i = 0 ; i < m_lsvCheckNurse.Items.Count ; i++)
            //{
            //    objInstrument.objSignerArr[currentSignCount + i] = new clsEmrSigns_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = new clsEmrEmployeeBase_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = (clsEmrEmployeeBase_VO)(m_lsvCheckNurse.Items[i].Tag);
            //    objInstrument.objSignerArr[currentSignCount + i].controlName = "m_lsvCheckNurse";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strFORMID_VCHR = "frmEMR_OPInstrumentQty";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //}
            //currentSignCount += m_lsvCheckNurse.Items.Count;

            //for (int i = 0 ; i < m_lsvCheckNurseName.Items.Count ; i++)
            //{
            //    objInstrument.objSignerArr[currentSignCount + i] = new clsEmrSigns_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = new clsEmrEmployeeBase_VO();
            //    objInstrument.objSignerArr[currentSignCount + i].objEmployee = (clsEmrEmployeeBase_VO)(m_lsvCheckNurseName.Items[i].Tag);
            //    objInstrument.objSignerArr[currentSignCount + i].controlName = "m_lsvCheckNurseName";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strFORMID_VCHR = "frmEMR_OPInstrumentQty";
            //    objInstrument.objSignerArr[currentSignCount + i].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            //}

            //objInstrument.objSignerArr[intSignCount - 1] = new clsEmrSigns_VO();
            //objInstrument.objSignerArr[intSignCount - 1].objEmployee = new clsEmrEmployeeBase_VO();
            //objInstrument.objSignerArr[intSignCount - 1].objEmployee = (clsEmrEmployeeBase_VO)(txtSign.Tag);
            //objInstrument.objSignerArr[intSignCount - 1].controlName = "txtSign";
            //objInstrument.objSignerArr[intSignCount - 1].m_strFORMID_VCHR = "frmEMR_OPInstrumentQty";
            //objInstrument.objSignerArr[intSignCount - 1].m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            #endregion

            #region ��ȡDataGrid����
            m_txtBedNO.Focus();
            m_dtgInstrument.EndEdit();
            objInstrument.m_objOPInstrument = new clsEMR_OPInstrumentItem[m_objDictArr.Length];
            int intRowNum = m_dtbInstrumentItem.Rows.Count;
            for (int i = 0 ; i < intRowNum ; i++)
            {
                objInstrument.m_objOPInstrument[i] = new clsEMR_OPInstrumentItem();
                objInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo = m_objDictArr[i];
                objInstrument.m_objOPInstrument[i].m_strAfterClose = m_dtbInstrumentItem.Rows[i][3].ToString();
                objInstrument.m_objOPInstrument[i].m_strBeforeClose = m_dtbInstrumentItem.Rows[i][2].ToString();
                objInstrument.m_objOPInstrument[i].m_strBeforeOP = m_dtbInstrumentItem.Rows[i][1].ToString();

                if (m_dtbInstrumentItem.Rows[i][4] != DBNull.Value && i + intRowNum < m_objDictArr.Length)
                {
                    objInstrument.m_objOPInstrument[i + intRowNum] = new clsEMR_OPInstrumentItem();
                    objInstrument.m_objOPInstrument[i + intRowNum].m_objOPInstrumentInfo = m_objDictArr[i + intRowNum];
                    objInstrument.m_objOPInstrument[i + intRowNum].m_strAfterClose = m_dtbInstrumentItem.Rows[i][7].ToString();
                    objInstrument.m_objOPInstrument[i + intRowNum].m_strBeforeClose = m_dtbInstrumentItem.Rows[i][6].ToString();
                    objInstrument.m_objOPInstrument[i + intRowNum].m_strBeforeOP = m_dtbInstrumentItem.Rows[i][5].ToString();
                }

                if (m_dtbInstrumentItem.Rows[i][8] != DBNull.Value && i + 2 * intRowNum < m_objDictArr.Length)
                {
                    objInstrument.m_objOPInstrument[i + 2 * intRowNum] = new clsEMR_OPInstrumentItem();
                    objInstrument.m_objOPInstrument[i + 2 * intRowNum].m_objOPInstrumentInfo = m_objDictArr[i + 2 * intRowNum];
                    objInstrument.m_objOPInstrument[i + 2 * intRowNum].m_strAfterClose = m_dtbInstrumentItem.Rows[i][11].ToString();
                    objInstrument.m_objOPInstrument[i + 2 * intRowNum].m_strBeforeClose = m_dtbInstrumentItem.Rows[i][10].ToString();
                    objInstrument.m_objOPInstrument[i + 2 * intRowNum].m_strBeforeOP = m_dtbInstrumentItem.Rows[i][9].ToString();
                }
            }
            #endregion
            objInstrument.m_lngEMR_SEQ = m_lngCurrentEMR_SEQ;
            return objInstrument;
        }
        #endregion

        #region �������¼��ֵ��ʾ��������
        /// <summary>
        /// �������¼��ֵ��ʾ�������ϡ�
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;

            clsEMR_OPInstrument objInstrument = p_objContent as clsEMR_OPInstrument;
            if (objInstrument == null) return;
            m_dtbInstrumentItem.Clear();

            m_dtpCreateDate.Value = objInstrument.m_dtmRecordDate;
            //clsEmrEmployeeBase_VO objEmpVO = new clsEmrEmployeeBase_VO();
            //objEmployeeSign.m_lngGetEmpByNO(objInstrument.m_strCreateUserID, out objEmpVO);
            //if (objEmpVO != null)
            //{
            //    txtSign.Tag = objEmpVO;
            //    txtSign.Text = objEmpVO.m_strLASTNAME_VCHR;
            //}
            m_lngCurrentEMR_SEQ = objInstrument.m_lngEMR_SEQ;

            #region ǩ������
            if (objInstrument.objSignerArr != null)
            {
                m_mthAddSignToTextBoxBase(new TextBoxBase[] { txtSign }, objInstrument.objSignerArr, new bool[] { true }, false);
                m_mthAddSignToListView(m_lsvOperationer, objInstrument.objSignerArr);
                m_mthAddSignToListView(m_lsvAssitantor, objInstrument.objSignerArr);
                m_mthAddSignToListView(m_lsvInstrumentNurse, objInstrument.objSignerArr);
                m_mthAddSignToListView(m_lsvItinerationNurse, objInstrument.objSignerArr);
                m_mthAddSignToListView(m_lsvCheckNurse, objInstrument.objSignerArr);
                m_mthAddSignToListView(m_lsvCheckNurseName, objInstrument.objSignerArr);
                //m_lsvOperationer.Items.Clear();
                //m_lsvAssitantor.Items.Clear();
                //m_lsvInstrumentNurse.Items.Clear();
                //m_lsvItinerationNurse.Items.Clear();
                //m_lsvCheckNurse.Items.Clear();
                //m_lsvCheckNurseName.Items.Clear();

                //for (int i = 0 ; i < objInstrument.objSignerArr.Length ; i++)
                //{
                //    if (objInstrument.objSignerArr[i].controlName == "m_lsvOperationer")
                //    {
                //        ListViewItem lviNewItem = new ListViewItem(objInstrument.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag = objInstrument.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        m_lsvOperationer.Items.Add(lviNewItem);
                //    }
                //    else if (objInstrument.objSignerArr[i].controlName == "m_lsvAssitantor")
                //    {
                //        ListViewItem lviNewItem = new ListViewItem(objInstrument.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag = objInstrument.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        m_lsvAssitantor.Items.Add(lviNewItem);
                //    }
                //    else if (objInstrument.objSignerArr[i].controlName == "m_lsvInstrumentNurse")
                //    {
                //        ListViewItem lviNewItem = new ListViewItem(objInstrument.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag = objInstrument.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        m_lsvInstrumentNurse.Items.Add(lviNewItem);
                //    }
                //    else if (objInstrument.objSignerArr[i].controlName == "m_lsvItinerationNurse")
                //    {
                //        ListViewItem lviNewItem = new ListViewItem(objInstrument.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag = objInstrument.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        m_lsvItinerationNurse.Items.Add(lviNewItem);
                //    }
                //    else if (objInstrument.objSignerArr[i].controlName == "m_lsvCheckNurse")
                //    {
                //        ListViewItem lviNewItem = new ListViewItem(objInstrument.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag = objInstrument.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        m_lsvCheckNurse.Items.Add(lviNewItem);
                //    }
                //    else if (objInstrument.objSignerArr[i].controlName == "m_lsvCheckNurseName")
                //    {
                //        ListViewItem lviNewItem = new ListViewItem(objInstrument.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                //        //ID ����ظ���
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strEMPID_CHR);
                //        //���� ������
                //        lviNewItem.SubItems.Add(objInstrument.objSignerArr[i].objEmployee.m_strLEVEL_CHR);
                //        //tag��Ϊ����
                //        lviNewItem.Tag = objInstrument.objSignerArr[i].objEmployee;
                //        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                //        m_lsvCheckNurseName.Items.Add(lviNewItem);
                //    }
                //}
            }
            #endregion

            #region ��ʾ���ݵ�DataGrid
            if (objInstrument.m_objOPInstrument != null)
            {
                int ItemsLength = objInstrument.m_objOPInstrument.Length;
                int EveryColumn = ItemsLength % 3 > 0 ? ItemsLength / 3 + 1 : ItemsLength / 3;
                m_dtbInstrumentItem.BeginLoadData();
                for (int i = 0 ; i < EveryColumn ; i++)
                {
                    string[] strOne = new string[] { objInstrument.m_objOPInstrument[i].m_objOPInstrumentInfo.m_strOPInstrumentName,
                        objInstrument.m_objOPInstrument[i].m_strBeforeOP, 
                        objInstrument.m_objOPInstrument[i].m_strBeforeClose, 
                        objInstrument.m_objOPInstrument[i].m_strAfterClose};
                    string[] strTwo = null;
                    if (i + EveryColumn < ItemsLength)
                    {
                        strTwo = new string[] { objInstrument.m_objOPInstrument[i + EveryColumn].m_objOPInstrumentInfo.m_strOPInstrumentName,
                        objInstrument.m_objOPInstrument[i + EveryColumn].m_strBeforeOP, 
                        objInstrument.m_objOPInstrument[i + EveryColumn].m_strBeforeClose, 
                        objInstrument.m_objOPInstrument[i + EveryColumn].m_strAfterClose};
                    }
                    string[] strThree = null;
                    if (i + 2 * EveryColumn < ItemsLength)
                    {
                        strThree = new string[] { objInstrument.m_objOPInstrument[i + 2 * EveryColumn].m_objOPInstrumentInfo.m_strOPInstrumentName, 
                            objInstrument.m_objOPInstrument[i + 2 * EveryColumn].m_strBeforeOP, 
                            objInstrument.m_objOPInstrument[i + 2 * EveryColumn].m_strBeforeClose, 
                            objInstrument.m_objOPInstrument[i + 2 * EveryColumn].m_strAfterClose};
                    }
                    m_dtbInstrumentItem.LoadDataRow(AddObject(strOne, strTwo, strThree), true);
                }
                m_dtbInstrumentItem.EndLoadData();
            }
            #endregion

            m_cmdRecorder.Enabled = false;
            m_dtpCreateDate.Enabled = false;
        }
        #endregion

        #region ��ȡ��ǰ���˵���������
        /// <summary>
        /// ��ȡ��ǰ���˵���������
        /// </summary>
        /// <param name="p_dtmRecordDate">��¼���ڣ��˴���ʾCreateDate</param>
        /// <param name="p_intFormID">����ID</param>
        protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate, int p_intFormID)
        {
            if (m_objBaseCurrentPatient == null || m_objBaseCurrentPatient.m_StrInPatientID == null || m_objBaseCurrentPatient.m_DtmSelectedInDate == DateTime.MinValue || p_dtmRecordDate == DateTime.MinValue)
            {
                clsPublicFunction.ShowInformationMessageBox("��������");
                return;
            }

            clsTrackRecordContent objContent = null;
            long lngRes = m_objDiseaseTrackDomain.m_lngGetDeleteRecordContent(m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), p_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
            if (lngRes > 0 && objContent != null)
            {
                m_objCurrentRecordContent = objContent;
                m_mthSetDeletedGUIFromContent(objContent);
            }
        }
        #endregion

        private int m_intFormID = 126;
        public override int m_IntFormID
        {
            get
            {
                return m_intFormID;
            }
        }

        #region ��ʾ��ɾ����¼
        /// <summary>
        /// ��ʾ��ɾ����¼
        /// </summary>
        /// <param name="p_objContent"></param>
        protected override void m_mthSetDeletedGUIFromContent(clsTrackRecordContent p_objContent)
        {
            if (p_objContent == null)
                return;

            m_mthSetGUIFromContent(p_objContent);
        }
        #endregion

        #region ��ȡ�����ʵ��
        /// <summary>
        /// ��ȡ�����ʵ��
        /// </summary>
        /// <returns></returns>
        protected override clsDiseaseTrackDomain m_objGetDiseaseTrackDomain()
        {
            //��ȡ���̼�¼�������ʵ��
            return new clsDiseaseTrackDomain(enmDiseaseTrackType.OPInstrumentQty);
        }
        #endregion

        #region ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ������
        /// <summary>
        /// ��ѡ��ʱ���¼������������Ϊ��ȫ��ȷ�����ݡ�
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        protected override void m_mthReAddNewRecord(clsTrackRecordContent p_objRecordContent)
        {
            m_mthSetGUIFromContent(p_objRecordContent);
        }
        #endregion

        #region m_strReloadFormTitle
        // ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        public override string m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��
            return "������е�����ϵ�����";
        }
        #endregion

        #region ����
        protected override enmApproveType m_EnmAppType
        {
            get { return enmApproveType.Nurses; }
        }
        protected override string m_StrRecorder_ID
        {
            get
            {
                if (txtSign.Tag != null)
                    return ((clsEmrEmployeeBase_VO)txtSign.Tag).m_strEMPNO_CHR.Trim();
                return "";
            }
        }
        #endregion ����

        #region ��ѯ����ʾ��ǰ���õ���Ŀ�б�
        /// <summary>
        /// ��ѯ����ʾ��ǰ���õ���Ŀ�б�
        /// </summary>
        private void m_mthSetActiveItemList()
        {
            if (m_objDictArr != null && m_objDictArr.Length > 0)
            {
                int ItemsLength = m_objDictArr.Length;
                int EveryColumn = ItemsLength % 3 > 0 ? ItemsLength / 3 + 1 : ItemsLength / 3;
                m_dtbInstrumentItem.BeginLoadData();
                for (int i = 0 ; i < EveryColumn ; i++)
                {
                    string[] strOne = new string[] { m_objDictArr[i].m_strOPInstrumentName, "", "", "" };
                    string[] strTwo = null;
                    if (i + EveryColumn < ItemsLength)
                    {
                        strTwo = new string[] { m_objDictArr[i + EveryColumn].m_strOPInstrumentName, "", "", "" };
                    }
                    string[] strThree = null;
                    if (i + 2 * EveryColumn < ItemsLength)
                    {
                        strThree = new string[] { m_objDictArr[i + 2 * EveryColumn].m_strOPInstrumentName, "", "", "" };
                    }
                    m_dtbInstrumentItem.LoadDataRow(AddObject(strOne, strTwo, strThree), true);
                }
                m_dtbInstrumentItem.EndLoadData();
            }
        }
        #endregion

        #region ����ָ����ʽobject����
        /// <summary>
        /// ����ָ����ʽobject����
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <param name="arr3"></param>
        /// <returns></returns>
        private object[] AddObject(string[] arr1, string[] arr2, string[] arr3)
        {
            object[] obj = new object[12];
            obj[0] = arr1[0];
            obj[1] = arr1[1];
            obj[2] = arr1[2];
            obj[3] = arr1[3];
            obj[4] = arr2[0];
            obj[5] = arr2[1];
            obj[6] = arr2[2];
            obj[7] = arr2[3];
            if (arr3 != null)
            {
                obj[8] = arr3[0];
                obj[9] = arr3[1];
                obj[10] = arr3[2];
                obj[11] = arr3[3];
            }
            return obj;
        }
        #endregion

        #region ��ӡ
        protected override long m_lngSubPrint()
        {
            clsEMR_OPInstrumentQtyPrintTool objPrintTool = new clsEMR_OPInstrumentQtyPrintTool();
            objPrintTool.m_mthInitPrintTool(null);
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
            else
            {
                m_objBaseCurrentPatient.m_StrHISInPatientID = m_ObjCurrentEmrPatientSession.m_strHISInpatientId;
                if (m_objCurrentRecordContent == null)
                {
                    objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_objBaseCurrentPatient.m_DtmSelectedInDate, DateTime.MinValue);
                }
                else
                {
                    objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,
                    m_objBaseCurrentPatient.m_DtmSelectedInDate,
                    m_objCurrentRecordContent.m_dtmOpenDate);
                }                
            }

            objPrintTool.m_mthInitPrintContent();
            objPrintTool.m_mthPrintPage(null);
            return 1;
        }
        #endregion

        #endregion

        #region �¼�

        #region Load����
        private void frmEMR_OPInstrumentQty_Load(object sender, EventArgs e)
        {
            clsEMR_OPInstrumentDomain objDomain = new clsEMR_OPInstrumentDomain();
            long lngRes = objDomain.m_lngGetActiveItemsFromDict(out m_objDictArr);

            m_mthSetActiveItemList();
        }
        #endregion

        #endregion
    }
}