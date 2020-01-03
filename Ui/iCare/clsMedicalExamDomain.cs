using System;
using System.Windows;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using com.digitalwave.Utility.Controls;

namespace iCare
{
    /// <summary>
    /// Summary description for clsDonotRun.
    /// </summary>
    internal class clsMedicalExamWithControlValue
    {
        public string m_strControlType;
        public string m_strControlName;
        public string m_strControlText;
        public clsMedicalExamMainRecordValue m_objclsMedicalExamMainRecord = new clsMedicalExamMainRecordValue();
    }

    public class clsMedicalExamDomain
    {
        public clsMedicalExamDomain()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public clsMedicalExamDomain(Form p_frmInput)
        {
            m_mthInitLoadAllMedicalExamControls(p_frmInput);
        }


        #region ��������ؼ�,��ӱԴ,2003-5-26 11:10:49
        public void m_mthClearMedicalExamControls(Form p_frmInput)
        {
            m_mthInitLoadAllMedicalExamControls(p_frmInput);
            //Control [] ccc=(Control []) m_objArrMedicalExamControls.ToArray (typeof(Control ));

            for (int i = 0; i < m_objArrMedicalExamControls.Count; i++)
            {
                Control ctlMedicalExam = (Control)m_objArrMedicalExamControls[i];
                switch (ctlMedicalExam.GetType().Name)
                {
                    case "ctlComboBox":
                        ((ctlComboBox)ctlMedicalExam).SelectedIndex = -1;
                        break;
                    case "ComboBox":        //��ѡ,ʹ��ComboBox��ʵ��
                        ((ComboBox)ctlMedicalExam).SelectedIndex = -1;
                        break;
                    case "ctlBorderTextBox":
                    case "RichTextBox":         //�ı�(����ѡ����ѡ�е��ı�) 
                    case "TextBox":
                        ctlMedicalExam.Text = "";
                        break;
                    case "RadioButton":     //��ѡ��ʹ��RadioButton Group��ʵ��
                        ((RadioButton)ctlMedicalExam).Checked = false;
                        break;
                    case "CheckBox":        //��ѡ
                                            //�������еĶ�ѡѡ��
                        ((CheckBox)ctlMedicalExam).Checked = false;
                        break;
                    case "CheckedListBox":
                        CheckedListBox ctl = (CheckedListBox)ctlMedicalExam;
                        for (int l = 0; l < ctl.Items.Count; l++)
                            ctl.SetItemChecked(l, false);
                        break;
                }

            }
        }
        #endregion

        #region ��Ա����,��ӱԴ,2003-5-22 10:52:42
        ArrayList m_objArrMedicalExamControls = new ArrayList();        //�������е������ؼ�
        ArrayList m_objArrMainRecordWithControls = new ArrayList();             //������������¼
        List<clsMedicalExamDetailRecordValue> m_objArrDetailRecord = new List<clsMedicalExamDetailRecordValue>();           //��������Detail��¼
                                                                    //clsMedicalExamService m_objMedicalExamSer=new clsMedicalExamService();
        private bool m_blnInitialzed = false;
        #endregion

        #region �����������ָ��λ�õ��ַ���,��ӱԴ,2003-5-22 11:05:43
        private string strGetBracketString(string p_strSource, int p_intIndex)
        {
            int i = p_strSource.IndexOf("[", 0);
            int j = 0;
            int intOld = i;
            while (i >= 0 && j <= p_intIndex)
            {
                intOld = i;
                i = p_strSource.IndexOf("[", i + 1);
                j++;
            }
            j = p_strSource.IndexOf("]", intOld + 1);
            if (j > intOld)
                return (p_strSource.Substring(intOld + 1, j - intOld - 1));
            else
                return ("-1");
        }

        #endregion

        #region ��������ID,��ӱԴ,2003-5-22 11:12:56
        private string strGetMedicalExamID()
        {
            string strMedicalExam_ID = "";

            //clsMedicalExamService m_objMedicalExamSer =
            //    (clsMedicalExamService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicalExamService));

            try
            {
                strMedicalExam_ID = (new weCare.Proxy.ProxyEmr01()).Service.strGetMedicalExam_ID( );
            }
            finally
            {
                //m_objMedicalExamSer.Dispose();
            }
            if (strMedicalExam_ID == null || strMedicalExam_ID.Trim() == "")
                return ("00000000000000000001");
            else
                return (strMedicalExam_ID);
        }
        #endregion

        #region ���õݹ����,�õ�����������Tag����Ϣ,��ӱԴ,2003-5-22 10:38:17
        private void m_mthLoadAllMedicalExamControlInfo(Control p_ctlControl)
        {
            if (p_ctlControl.Tag != null && p_ctlControl.Tag.ToString().Length > 1 && p_ctlControl.Tag.ToString().Substring(0, 1) == "[")
            {
                m_objArrMedicalExamControls.Add(p_ctlControl);
            }

            if (p_ctlControl.HasChildren)
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthLoadAllMedicalExamControlInfo(subcontrol);
                }
            }
        }
        #endregion

        #region �������е������ؼ�,��ӱԴ,2003-5-23 16:23:26
        private void m_mthInitLoadAllMedicalExamControls(Form p_frmInput)
        {
            if (m_blnInitialzed) return;
            m_objArrMedicalExamControls.Clear();
            m_mthLoadAllMedicalExamControlInfo(p_frmInput);
            if (m_objArrMedicalExamControls.Count > 0)
                m_blnInitialzed = true;
        }

        private void m_mthInitLoadAllMedicalExamControls1(Control p_ctl)
        {
            if (m_blnInitialzed) return;
            m_objArrMedicalExamControls.Clear();
            m_mthLoadAllMedicalExamControlInfo(p_ctl);
            if (m_objArrMedicalExamControls.Count > 0)
                m_blnInitialzed = true;
        }
        #endregion

        #region ���������ѡ��,��ӱԴ,2003-5-23 16:29:57
        //Control.Tag="[0][0001][000001][0]"	==> [OptionType][ElementID][OptionID][Index]
        public void m_mthSaveMedicalExamRecord(Form p_frmInput, string p_strCateGory_ID, clsMedicalExamInHospital_TargetValue p_objMedicalExamInHospital_Target)
        {
            m_objArrDetailRecord.Clear();
            m_objArrMainRecordWithControls.Clear();

            m_mthInitLoadAllMedicalExamControls(p_frmInput);

            #region �������ؼ���Ϣת�뵽�ṹ����,��ӱԴ,2003-5-23 13:15:05
            string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (p_objMedicalExamInHospital_Target.m_strMedicalExam_ID == null || p_objMedicalExamInHospital_Target.m_strMedicalExam_ID == "")
                p_objMedicalExamInHospital_Target.m_strMedicalExam_ID = strGetMedicalExamID();

            long lngDetailIndex = 1;
            for (int ii = 0; ii < m_objArrMedicalExamControls.Count; ii++)
            {
                //��ǰҪ��¼�Ŀؼ�
                clsMedicalExamWithControlValue objMedicalExamControlsInfo = new clsMedicalExamWithControlValue();

                Control ctlMedicalExam = (Control)m_objArrMedicalExamControls[ii];
                string strTag = ctlMedicalExam.Tag.ToString();


                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strMedicalExam_ID = p_objMedicalExamInHospital_Target.m_strMedicalExam_ID;

                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strCategory_ID = p_strCateGory_ID;
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID = strGetBracketString(strTag, 1);
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID = strGetBracketString(strTag, 2);
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strActivity_Date = strCurrentDate;
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = strGetBracketString(strTag, 0);
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = "-1";
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text = "";
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = "";
                objMedicalExamControlsInfo.m_strControlType = ctlMedicalExam.GetType().Name;
                objMedicalExamControlsInfo.m_strControlName = ctlMedicalExam.Name;
                objMedicalExamControlsInfo.m_strControlText = ctlMedicalExam.Text;
                //д���û�������Ϣ
                switch (ctlMedicalExam.GetType().Name)
                {
                    case "ctlComboBox":
                        if (((ctlComboBox)ctlMedicalExam).SelectedIndex >= 0 && ((ctlComboBox)ctlMedicalExam).SelectedIndex < ((ctlComboBox)ctlMedicalExam).GetItemsCount())
                        {
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = ((ctlComboBox)ctlMedicalExam).SelectedIndex.ToString();
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ctlComboBox)ctlMedicalExam).Text;
                        }
                        else if (((ctlComboBox)ctlMedicalExam).Text != null)
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ctlComboBox)ctlMedicalExam).Text;

                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = "1";
                        break;
                    case "ComboBox":        //��ѡ,ʹ��ComboBox��ʵ��
                        if (((ComboBox)ctlMedicalExam).SelectedIndex >= 0 && ((ComboBox)ctlMedicalExam).SelectedIndex < ((ComboBox)ctlMedicalExam).Items.Count)
                        {
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = ((ComboBox)ctlMedicalExam).SelectedIndex.ToString();
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ComboBox)ctlMedicalExam).Text;
                        }
                        else if (((ComboBox)ctlMedicalExam).Text != null)
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ComboBox)ctlMedicalExam).Text;

                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = "1";
                        break;
                    case "ctlBorderTextBox":
                    case "RichTextBox":         //�ı�(����ѡ����ѡ�е��ı�) 
                    case "TextBox":
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text = ctlMedicalExam.Text;
                        //objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index =strGetBracketString(strTag,3);
                        break;
                    case "RadioButton":     //��ѡ��ʹ��RadioButton Group��ʵ��
                        if (((RadioButton)ctlMedicalExam).Checked)
                        {
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = strGetBracketString(strTag, 3);
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((RadioButton)ctlMedicalExam).Text;
                        }
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = "1";
                        break;
                    case "CheckBox":        //��ѡ
                                            //�������еĶ�ѡѡ��
                        if (((CheckBox)ctlMedicalExam).Checked)
                        {
                            clsMedicalExamDetailRecordValue objMedicalExamDetailRecordRecord = new clsMedicalExamDetailRecordValue();
                            objMedicalExamDetailRecordRecord.m_strActivity_Date = strCurrentDate;
                            objMedicalExamDetailRecordRecord.m_strCategory_ID = p_strCateGory_ID;
                            objMedicalExamDetailRecordRecord.m_strDetailItem_ID = lngDetailIndex.ToString("0000");
                            objMedicalExamDetailRecordRecord.m_strElement_ID = objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID;
                            objMedicalExamDetailRecordRecord.m_strMedicalExam_ID = strGetMedicalExamID();
                            objMedicalExamDetailRecordRecord.m_strOption_ID = objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID;
                            objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes = strGetBracketString(strTag, 3);
                            objMedicalExamDetailRecordRecord.m_strSelected_OptionValue_Text = ctlMedicalExam.Text;
                            m_objArrDetailRecord.Add(objMedicalExamDetailRecordRecord);
                            lngDetailIndex++;
                        }
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = "2";
                        break;
                    case "CheckedListBox":
                        CheckedListBox ctl = (CheckedListBox)ctlMedicalExam;
                        for (int l = 0; l < ctl.Items.Count; l++)
                        {
                            if (ctl.GetItemChecked(l))
                            {
                                clsMedicalExamDetailRecordValue objMedicalExamDetailRecordRecord = new clsMedicalExamDetailRecordValue();
                                objMedicalExamDetailRecordRecord.m_strActivity_Date = strCurrentDate;
                                objMedicalExamDetailRecordRecord.m_strCategory_ID = p_strCateGory_ID;
                                objMedicalExamDetailRecordRecord.m_strDetailItem_ID = lngDetailIndex.ToString("0000");
                                objMedicalExamDetailRecordRecord.m_strElement_ID = objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID;
                                objMedicalExamDetailRecordRecord.m_strMedicalExam_ID = strGetMedicalExamID();
                                objMedicalExamDetailRecordRecord.m_strOption_ID = objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID;
                                objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes = l.ToString();
                                objMedicalExamDetailRecordRecord.m_strSelected_OptionValue_Text = ctl.Items[l].ToString();
                                m_objArrDetailRecord.Add(objMedicalExamDetailRecordRecord);
                                lngDetailIndex++;
                            }
                        }
                        break;
                }
                m_objArrMainRecordWithControls.Add(objMedicalExamControlsInfo);
            }
            #endregion

            //clsMedicalExamWithControlValue[] aa=(clsMedicalExamWithControlValue [])m_objArrMainRecordWithControls.ToArray (typeof(clsMedicalExamWithControlValue));

            #region �ϲ�ͬ��ؼ���Ϊͬһ����¼,��ӱԴ,2003-5-23 13:15:05
            //�ϲ�ͬһ����¼�е�Control
            clsMedicalExamWithControlValue objMedicalExamWithControl = null;
            clsMedicalExamMainRecordValue objMedicalExamRecord = null;
            List<clsMedicalExamMainRecordValue> objArrMedicalExamMainRecordsToDb = new List<clsMedicalExamMainRecordValue>();
            objArrMedicalExamMainRecordsToDb.Clear();
            int intControlsInSameRecord = -1;
            for (int i = 0; i < m_objArrMainRecordWithControls.Count; i++)
            {
                objMedicalExamWithControl = (clsMedicalExamWithControlValue)m_objArrMainRecordWithControls[i];

                #region �����Ƿ��Ѿ�������˿ؼ��ļ�¼,����ʹ��RadioBox���CheckBox��(�������е�TextBox)�ų��ִ����,��ӱԴ,2003-5-23 13:17:48
                intControlsInSameRecord = -1;
                //				if(objMedicalExamWithControl.m_strControlType =="RadioButton" || 
                //					objMedicalExamWithControl.m_strControlType =="CheckBox" ||
                //					objMedicalExamWithControl.m_strControlType == "TextBox" ||
                //					objMedicalExamWithControl.m_strControlType =="RichTextBox" ||
                //					objMedicalExamWithControl.m_strControlType =="ctlBorderTextBox" )
                for (int j = 0; j < objArrMedicalExamMainRecordsToDb.Count; j++)
                {
                    objMedicalExamRecord = (clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[j];
                    //�Ƿ���MainRecord�б�ʾͬһ����¼�еĶ���ؼ�
                    if (objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strElement_ID == objMedicalExamRecord.m_strElement_ID &&
                        objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strOption_ID == objMedicalExamRecord.m_strOption_ID)
                        intControlsInSameRecord = j;            //��¼���Ѿ��е�Index
                }
                #endregion

                #region �ϲ�ͬ��ؼ�Ϊһ����¼,��ӱԴ,2003-5-23 13:19:40
                if (intControlsInSameRecord == -1)              //����û�м�����˿ؼ�ѡ��
                    objArrMedicalExamMainRecordsToDb.Add(objMedicalExamWithControl.m_objclsMedicalExamMainRecord);
                else        //��ͬһ����¼�е���Ϣ������˲�����Ϣ
                {
                    //���ڴ��ı����Ӧ���Բ���ִ�е��ˣ�OptionID�϶�Ψһ
                    //��ѡ�����,RadioBoxʵ��ͬһ����¼����
                    if (int.Parse(objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strOption_Type) == 1)   //��ѡ
                    {
                        //�ḻԭ����¼��Ϣ
                        clsMedicalExamMainRecordValue objOldRecord = (clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[intControlsInSameRecord];
                        //���ڵ�ѡ�е��ı������ѡ����'����'ʱ��ʹ�ÿؼ���Tag���Ը��¸ü�¼��Ϣ��
                        //����û��ѡ��'����'������Ҫ������Index��OptionValue��Ϣ,OptionText��������Ϊ��
                        if ((objMedicalExamWithControl.m_strControlType == "TextBox" ||
                            objMedicalExamWithControl.m_strControlType == "RichTextBox" ||
                            objMedicalExamWithControl.m_strControlType == "ctlBorderTextBox") &&
                            objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index == objOldRecord.m_strSelected_Option_Index)
                        {
                            objOldRecord.m_strSelected_Option_Text = objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text;
                        }
                        else
                        {
                            //���������ԣ��϶����Ѿ�ѡ���˵ĵ�ѡ�ؼ�,objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index !=""��ʾ�ÿؼ�ѡ��
                            if (objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index != "")
                            {
                                objOldRecord.m_strSelected_Option_Index = objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index;
                                objOldRecord.m_strSelected_OptionValue_Text = objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text;
                            }
                        }
                        objArrMedicalExamMainRecordsToDb.RemoveAt(intControlsInSameRecord);
                        objArrMedicalExamMainRecordsToDb.Add(objOldRecord);
                    }
                    else        //��ѡ��ʹ��GroupBox��ʵ��ͬһ����¼����
                    {
                        clsMedicalExamMainRecordValue objOldRecord = (clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[intControlsInSameRecord];

                        objOldRecord.m_strSelected_Option_Index = "";
                        objOldRecord.m_strSelected_OptionValue_Text = "";
                        if (objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text != "")
                            objOldRecord.m_strSelected_Option_Text = objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text;
                        objArrMedicalExamMainRecordsToDb.RemoveAt(intControlsInSameRecord);
                        objArrMedicalExamMainRecordsToDb.Add(objOldRecord);
                    }

                }
                #endregion
            }
            #endregion

            //��ʼд�����ݿ�
            //clsMedicalExamMainRecordValue [] bb=(clsMedicalExamMainRecordValue [])objArrMedicalExamMainRecordsToDb.ToArray (typeof(clsMedicalExamMainRecordValue));
            long lngEff = 0;
            lngSaveMedicalExam(p_objMedicalExamInHospital_Target, objArrMedicalExamMainRecordsToDb, m_objArrDetailRecord, ref lngEff);
        }
        private long lngSaveMedicalExam(clsMedicalExamInHospital_TargetValue p_objMedicalExamInHospital_Target, List<clsMedicalExamMainRecordValue> p_objMedicalExamMainRecord, List<clsMedicalExamDetailRecordValue> p_objMedicalDetalRecord, ref long lngEff)
        {
            //clsMedicalExamService m_objMedicalExamSer =
            //    (clsMedicalExamService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicalExamService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.lngSaveMedicalExam(p_objMedicalExamInHospital_Target, p_objMedicalExamMainRecord, p_objMedicalDetalRecord, ref lngEff);
            }
            finally
            {
                //m_objMedicalExamSer.Dispose();
            }
            return lngRes;
        }
        #endregion

        #region ��ʽ�������Ŀѡ��,��ӱԴ,2003-5-23 16:31:15
        public void m_mthDisplayMedicalExamOptions(Form p_frmInput, string p_strMedicalExam_ID)
        {
            m_mthInitLoadAllMedicalExamControls(p_frmInput);
            m_mthClearMedicalExamControls(p_frmInput);
            clsMedicalExamMainRecordValue[] objMedicalExamMainRecord = null;
            clsMedicalExamDetailRecordValue[] objMedicalDetailRecord = null;

            //clsMedicalExamService m_objMedicalExamSer =
            //    (clsMedicalExamService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicalExamService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.lngLoadMedicalExamOptions(p_strMedicalExam_ID, out objMedicalExamMainRecord, out objMedicalDetailRecord);
            }
            finally
            {
                //m_objMedicalExamSer.Dispose();
            }
            #region ����MainRecord��ֵ,��ӱԴ,2003-5-23 17:24:22
            if (objMedicalExamMainRecord != null && objMedicalExamMainRecord.Length > 0)
            {
                for (int i = 0; i < objMedicalExamMainRecord.Length; i++)
                {
                    clsMedicalExamMainRecordValue objMedicalExamMainInfo = objMedicalExamMainRecord[i];

                    objMedicalExamMainInfo.m_strSelected_Option_Index = (objMedicalExamMainInfo.m_strSelected_Option_Index == null ? "-1" : objMedicalExamMainInfo.m_strSelected_Option_Index);
                    objMedicalExamMainInfo.m_strSelected_Option_Text = (objMedicalExamMainInfo.m_strSelected_Option_Text == null ? "" : objMedicalExamMainInfo.m_strSelected_Option_Text);
                    objMedicalExamMainInfo.m_strSelected_OptionValue_Text = (objMedicalExamMainInfo.m_strSelected_OptionValue_Text == null ? "" : objMedicalExamMainInfo.m_strSelected_OptionValue_Text);

                    //Ѱ�������˸����ԵĿؼ�
                    for (int j = 0; j < m_objArrMedicalExamControls.Count; j++)
                    {
                        clsMedicalExamWithControlValue objMedicalExamControlsInfo = new clsMedicalExamWithControlValue();
                        Control ctlMedicalExam = (Control)m_objArrMedicalExamControls[j];

                        string strTag = ctlMedicalExam.Tag.ToString();

                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID = strGetBracketString(strTag, 1);
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID = strGetBracketString(strTag, 2);
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = strGetBracketString(strTag, 0);
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = strGetBracketString(strTag, 3);
                        switch (ctlMedicalExam.GetType().Name)
                        {
                            case "ctlBorderTextBox":
                            case "RichTextBox":         //�ı�(����ѡ����ѡ�е��ı�) 
                            case "TextBox":
                                if (objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID == objMedicalExamMainInfo.m_strElement_ID &&
                                    objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID == objMedicalExamMainInfo.m_strOption_ID)
                                    ctlMedicalExam.Text = objMedicalExamMainInfo.m_strSelected_Option_Text;
                                break;
                            case "RadioButton":
                                if (objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID == objMedicalExamMainInfo.m_strElement_ID &&
                                    objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID == objMedicalExamMainInfo.m_strOption_ID &&
                                    objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index == objMedicalExamMainInfo.m_strSelected_Option_Index)
                                    ((RadioButton)ctlMedicalExam).Checked = true;
                                break;
                            case "ctlComboBox":
                                if (objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID == objMedicalExamMainInfo.m_strElement_ID &&
                                    objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID == objMedicalExamMainInfo.m_strOption_ID)
                                {
                                    if (((ctlComboBox)ctlMedicalExam).GetItemsCount() > 0 &&
                                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index != null &&
                                        int.Parse(objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index) < ((ctlComboBox)ctlMedicalExam).GetItemsCount())
                                        ((ctlComboBox)ctlMedicalExam).SelectedIndex = int.Parse(objMedicalExamMainInfo.m_strSelected_Option_Index);
                                }
                                break;
                            case "ComboBox":
                                if (objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID == objMedicalExamMainInfo.m_strElement_ID &&
                                    objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID == objMedicalExamMainInfo.m_strOption_ID)
                                {
                                    if (((ComboBox)ctlMedicalExam).Items.Count > 0 &&
                                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index != null &&
                                        int.Parse(objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index) < ((ComboBox)ctlMedicalExam).Items.Count)
                                        ((ComboBox)ctlMedicalExam).SelectedIndex = int.Parse(objMedicalExamMainInfo.m_strSelected_Option_Index);
                                }
                                break;

                        }

                    }
                }
            }
            #endregion

            #region ����Detail Record��ֵ,��ӱԴ,2003-5-23 17:25:14
            if (objMedicalDetailRecord != null && objMedicalDetailRecord.Length > 0)
            {
                for (int i = 0; i < objMedicalDetailRecord.Length; i++)
                {
                    clsMedicalExamDetailRecordValue objMedicalExamDetailInfo = objMedicalDetailRecord[i];
                    //Ѱ�������˸����ԵĿؼ�
                    for (int j = 0; j < m_objArrMedicalExamControls.Count; j++)
                    {
                        clsMedicalExamDetailRecordValue objMedicalExamDetailRecordRecord = new clsMedicalExamDetailRecordValue();
                        Control ctlMedicalExam = (Control)m_objArrMedicalExamControls[j];

                        string strTag = ctlMedicalExam.Tag.ToString();

                        objMedicalExamDetailRecordRecord.m_strElement_ID = strGetBracketString(strTag, 1);
                        objMedicalExamDetailRecordRecord.m_strOption_ID = strGetBracketString(strTag, 2);
                        objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes = strGetBracketString(strTag, 3);
                        switch (ctlMedicalExam.GetType().Name)
                        {
                            case "CheckBox":
                                if (objMedicalExamDetailRecordRecord.m_strElement_ID == objMedicalExamDetailInfo.m_strElement_ID &&
                                    objMedicalExamDetailRecordRecord.m_strOption_ID == objMedicalExamDetailInfo.m_strOption_ID &&
                                    objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes == objMedicalExamDetailInfo.m_strSelected_Option_Indexes)
                                    ((CheckBox)ctlMedicalExam).Checked = true;
                                break;
                            case "CheckedListBox":
                                if (objMedicalExamDetailRecordRecord.m_strElement_ID == objMedicalExamDetailInfo.m_strElement_ID &&
                                    objMedicalExamDetailRecordRecord.m_strOption_ID == objMedicalExamDetailInfo.m_strOption_ID)
                                {
                                    CheckedListBox ctl = (CheckedListBox)ctlMedicalExam;
                                    if (objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes != null && int.Parse(objMedicalExamDetailInfo.m_strSelected_Option_Indexes) < ctl.Items.Count)
                                        ctl.SetItemChecked(int.Parse(objMedicalExamDetailInfo.m_strSelected_Option_Indexes), true);
                                }
                                break;

                        }

                    }

                }
            }
            #endregion

        }
        #endregion

        #region �ϳ��ַ���,��ӱԴ,2003-5-26 12:31:34
        public string m_strGetMedicalExamUnitString(string p_strMedicalExamID)
        {
            clsMedicalExamUnitStringValue[] objMedicalExamUnitString = null;

            //clsMedicalExamService m_objMedicalExamSer =
            //    (clsMedicalExamService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicalExamService));

            long lngRes = 0;
            try
            {
                lngRes = (new weCare.Proxy.ProxyEmr01()).Service.lngGetMedicalExamUnitString(p_strMedicalExamID, out objMedicalExamUnitString);
            }
            finally
            {
                //m_objMedicalExamSer.Dispose();
            }
            if (objMedicalExamUnitString == null || objMedicalExamUnitString.Length <= 0) return ("");
            #region �ϲ��ַ���,��ӱԴ,2003-5-27 8:56:02
            ArrayList objArrMedicalExamUnitString = new ArrayList();
            objArrMedicalExamUnitString.Clear();
            bool blnIsSeries2 = false;
            string strOutput = "";
            string strElement_ID = objMedicalExamUnitString[0].m_strElement_ID;
            string strOption_ID = objMedicalExamUnitString[0].m_strOption_ID;
            for (int i = 0; i < objMedicalExamUnitString.Length; i++)
            {

                switch (int.Parse(objMedicalExamUnitString[i].m_strOption_Type))
                {
                    case 0:
                    case 1:
                        blnIsSeries2 = false;
                        objArrMedicalExamUnitString.Add(objMedicalExamUnitString[i]);
                        break;
                    case 2:
                        if (blnIsSeries2 == false)
                        {
                            clsMedicalExamUnitStringValue objTempMedicalUnitString = objMedicalExamUnitString[i];       //�������һ����¼
                            objTempMedicalUnitString.m_strSelected_OptionValue_DetailText = objMedicalExamUnitString[i].m_strSelected_OptionValue_DetailText + "��";
                            objArrMedicalExamUnitString.Add(objTempMedicalUnitString);
                        }
                        else
                        {
                            int intMedicalIndex = objArrMedicalExamUnitString.Count - 1;
                            clsMedicalExamUnitStringValue objTempMedicalUnitString = (clsMedicalExamUnitStringValue)objArrMedicalExamUnitString[intMedicalIndex];       //�������һ����¼
                            objTempMedicalUnitString.m_strSelected_OptionValue_DetailText += objMedicalExamUnitString[i].m_strSelected_OptionValue_DetailText + "��";
                            objArrMedicalExamUnitString.RemoveAt(intMedicalIndex);
                            objArrMedicalExamUnitString.Add(objTempMedicalUnitString);
                        }
                        blnIsSeries2 = true;
                        break;
                }
            }
            #endregion

            #region �����ַ���,��ӱԴ,2003-5-27 9:11:20
            string strOldElement_ID = "";
            for (int i = 0; i < objArrMedicalExamUnitString.Count; i++)
            {
                clsMedicalExamUnitStringValue objTempMedicalUnitString = (clsMedicalExamUnitStringValue)objArrMedicalExamUnitString[i];
                if (i == 0)
                {
                    strOldElement_ID = objTempMedicalUnitString.m_strElement_ID;
                    strOutput = objTempMedicalUnitString.m_strElementName + ":\r\n";
                }
                if (objTempMedicalUnitString.m_strElement_ID != strOldElement_ID)
                {
                    strOutput += "\r\n\r\n" + objTempMedicalUnitString.m_strElementName + ":\r\n";
                    strOldElement_ID = objTempMedicalUnitString.m_strElement_ID;
                }
                switch (int.Parse(objTempMedicalUnitString.m_strOption_Type))
                {
                    case 0:
                        if (objTempMedicalUnitString.m_strSelected_Option_Text.Trim() != "")
                            strOutput += objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_Option_Text;
                        break;
                    case 1:
                        if (objTempMedicalUnitString.m_strSelected_Option_Text != null && objTempMedicalUnitString.m_strSelected_Option_Text != "")
                            strOutput += objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_Text + "(" + objTempMedicalUnitString.m_strSelected_Option_Text + ")";
                        else
                            strOutput += objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_Text;
                        break;
                    case 2:
                        if (objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Length > 0)
                            objTempMedicalUnitString.m_strSelected_OptionValue_DetailText = objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Substring(0, objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Length - 1);
                        if (objTempMedicalUnitString.m_strSelected_Option_Text != null && objTempMedicalUnitString.m_strSelected_Option_Text != "")
                            strOutput += objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_DetailText + "(" + objTempMedicalUnitString.m_strSelected_Option_Text + ")";
                        else
                            strOutput += objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_DetailText;
                        break;
                }
                strOutput += "; ";


            }

            #endregion
            return (strOutput);
        }
        #endregion

        public string strGetInPatientCaseMedicalExam_ID(string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            //clsMedicalExamService m_objMedicalExamSer =
            //    (clsMedicalExamService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicalExamService));

            string strRes = "";
            try
            {
                strRes = ((new weCare.Proxy.ProxyEmr01()).Service.strGetInPatientCaseMedicalExam_ID(p_strInPatientID, p_strInPatientDate, p_strOpenDate));
            }
            finally
            {
                //m_objMedicalExamSer.Dispose();
            }
            return strRes;
        }

        #region �ϳ��ַ���2,��ӱԴ,2003-6-3 12:31:34
        //		public string m_strGetMedicalExamUnitString(Form p_frmInput)
        //		{
        //			#region 555
        //						m_objArrDetailRecord.Clear ();
        //						m_objArrMainRecordWithControls.Clear ();
        //						
        //						m_mthInitLoadAllMedicalExamControls (p_frmInput);
        //						
        //						#region �������ؼ���Ϣת�뵽�ṹ����,��ӱԴ,2003-5-23 13:15:05
        //						string strCurrentDate=DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss");
        //
        //						long lngDetailIndex=1;
        //						for(int ii=0;ii<m_objArrMedicalExamControls.Count ;ii++ )
        //						{
        //							//��ǰҪ��¼�Ŀؼ�
        //							clsMedicalExamWithControlValue objMedicalExamControlsInfo=new clsMedicalExamWithControlValue();
        //
        //							Control ctlMedicalExam=(Control)m_objArrMedicalExamControls[ii];
        //							string strTag=ctlMedicalExam.Tag.ToString ();
        //							
        //
        //
        //							objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID=strGetBracketString(strTag,1);
        //							objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID=strGetBracketString(strTag,2);
        //							objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strActivity_Date=strCurrentDate;
        //							objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type=strGetBracketString(strTag,0);
        //							objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index ="";
        //							objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text ="";
        //							objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text ="";
        //							objMedicalExamControlsInfo.m_strControlType =ctlMedicalExam.GetType().Name  ;
        //							objMedicalExamControlsInfo.m_strControlName =ctlMedicalExam.Name ;
        //							objMedicalExamControlsInfo.m_strControlText =ctlMedicalExam.Text ;
        //							//д���û�������Ϣ
        //							switch(ctlMedicalExam.GetType().Name)
        //							{ 
        //								case "ctlComboBox":
        //									if(((ctlComboBox)ctlMedicalExam).SelectedIndex >=0 && ((ctlComboBox)ctlMedicalExam).SelectedIndex < ((ctlComboBox)ctlMedicalExam).GetItemsCount())
        //									{
        //										objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index=((ctlComboBox)ctlMedicalExam).SelectedIndex.ToString ();
        //										objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ctlComboBox)ctlMedicalExam).Text ;
        //									}
        //									else if(((ctlComboBox)ctlMedicalExam).Text !=null)
        //										objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ctlComboBox)ctlMedicalExam).Text ;
        //
        //									objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type ="1";
        //									break;
        //								case "ComboBox":		//��ѡ,ʹ��ComboBox��ʵ��
        //									if(((ComboBox)ctlMedicalExam).SelectedIndex >=0 && ((ComboBox)ctlMedicalExam).SelectedIndex < ((ComboBox)ctlMedicalExam).Items.Count )
        //									{
        //										objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index=((ComboBox)ctlMedicalExam).SelectedIndex.ToString ();
        //										objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ComboBox)ctlMedicalExam).Text ;
        //									}
        //									else if(((ComboBox)ctlMedicalExam).Text !=null)
        //										objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ComboBox)ctlMedicalExam).Text ;
        //
        //									objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type ="1";
        //									break;
        //								case "ctlBorderTextBox":
        //								case "RichTextBox":			//�ı�(����ѡ����ѡ�е��ı�) 
        //								case "TextBox":
        //									objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord. m_strSelected_Option_Text=ctlMedicalExam.Text;						
        //									//objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index =strGetBracketString(strTag,3);
        //									break;
        //								case "RadioButton":		//��ѡ��ʹ��RadioButton Group��ʵ��
        //									if(((RadioButton )ctlMedicalExam).Checked )
        //									{
        //										objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index =strGetBracketString(strTag,3);
        //										objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text =  ((RadioButton )ctlMedicalExam).Text ;
        //									}
        //									objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type ="1";
        //									break;
        //								case "CheckBox":		//��ѡ
        //									//�������еĶ�ѡѡ��
        //									if(((CheckBox)ctlMedicalExam).Checked )
        //									{
        //										clsMedicalExamDetailRecordValue objMedicalExamDetailRecordRecord=new clsMedicalExamDetailRecordValue();
        //										objMedicalExamDetailRecordRecord.m_strActivity_Date =strCurrentDate ;
        //										objMedicalExamDetailRecordRecord.m_strDetailItem_ID =lngDetailIndex.ToString ("0000");
        //										objMedicalExamDetailRecordRecord.m_strElement_ID =objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID;
        //										objMedicalExamDetailRecordRecord.m_strMedicalExam_ID =strGetMedicalExamID ();
        //										objMedicalExamDetailRecordRecord.m_strOption_ID =objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID;
        //										objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes 	=strGetBracketString(strTag,3);
        //										objMedicalExamDetailRecordRecord.m_strSelected_OptionValue_Text =ctlMedicalExam.Text;
        //										m_objArrDetailRecord.Add (objMedicalExamDetailRecordRecord);
        //										lngDetailIndex++;
        //									}
        //									objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type ="2";
        //									break;
        //								case "CheckedListBox":
        //									CheckedListBox ctl=(CheckedListBox)ctlMedicalExam;
        //									for(int l=0;l<ctl.Items.Count ;l++)
        //									{
        //										if(ctl.GetItemChecked(l))
        //										{
        //											clsMedicalExamDetailRecordValue objMedicalExamDetailRecordRecord=new clsMedicalExamDetailRecordValue();
        //											objMedicalExamDetailRecordRecord.m_strActivity_Date =strCurrentDate ;
        //											objMedicalExamDetailRecordRecord.m_strDetailItem_ID =lngDetailIndex.ToString ("0000");
        //											objMedicalExamDetailRecordRecord.m_strElement_ID =objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID;
        //											objMedicalExamDetailRecordRecord.m_strMedicalExam_ID =strGetMedicalExamID ();
        //											objMedicalExamDetailRecordRecord.m_strOption_ID =objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID;																
        //											objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes =l.ToString ();
        //											objMedicalExamDetailRecordRecord.m_strSelected_OptionValue_Text = ctl.Items [l].ToString ();							
        //											m_objArrDetailRecord.Add (objMedicalExamDetailRecordRecord);
        //											lngDetailIndex ++;
        //										}
        //									}
        //									break;
        //							}
        //							m_objArrMainRecordWithControls.Add (objMedicalExamControlsInfo);
        //						}
        //						#endregion
        //						
        //						//clsMedicalExamWithControlValue[] aa=(clsMedicalExamWithControlValue [])m_objArrMainRecordWithControls.ToArray (typeof(clsMedicalExamWithControlValue));
        //
        //						#region �ϲ�ͬ��ؼ���Ϊͬһ����¼,��ӱԴ,2003-5-23 13:15:05
        //						//�ϲ�ͬһ����¼�е�Control
        //						clsMedicalExamWithControlValue objMedicalExamWithControl=null;
        //						clsMedicalExamMainRecordValue objMedicalExamRecord=null;
        //						ArrayList objArrMedicalExamMainRecordsToDb=new ArrayList ();
        //						objArrMedicalExamMainRecordsToDb.Clear ();
        //						int intControlsInSameRecord=-1;
        //						for(int i=0;i<m_objArrMainRecordWithControls.Count ;i++)
        //						{
        //							objMedicalExamWithControl=(clsMedicalExamWithControlValue)m_objArrMainRecordWithControls[i];
        //
        //							#region �����Ƿ��Ѿ�������˿ؼ��ļ�¼,����ʹ��RadioBox���CheckBox��(�������е�TextBox)�ų��ִ����,��ӱԴ,2003-5-23 13:17:48
        //							intControlsInSameRecord=-1;
        //							//				if(objMedicalExamWithControl.m_strControlType =="RadioButton" || 
        //							//					objMedicalExamWithControl.m_strControlType =="CheckBox" ||
        //							//					objMedicalExamWithControl.m_strControlType == "TextBox" ||
        //							//					objMedicalExamWithControl.m_strControlType =="RichTextBox" ||
        //							//					objMedicalExamWithControl.m_strControlType =="ctlBorderTextBox" )
        //							for(int j=0;j<objArrMedicalExamMainRecordsToDb.Count ;j++)
        //							{
        //								objMedicalExamRecord=(clsMedicalExamMainRecordValue) objArrMedicalExamMainRecordsToDb[j];
        //								//�Ƿ���MainRecord�б�ʾͬһ����¼�еĶ���ؼ�
        //								if(objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strElement_ID == objMedicalExamRecord.m_strElement_ID &&
        //									objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strOption_ID == objMedicalExamRecord.m_strOption_ID )
        //									intControlsInSameRecord=j;			//��¼���Ѿ��е�Index
        //							}
        //							#endregion
        //
        //							#region �ϲ�ͬ��ؼ�Ϊһ����¼,��ӱԴ,2003-5-23 13:19:40
        //							if(intControlsInSameRecord==-1)				//����û�м�����˿ؼ�ѡ��
        //								objArrMedicalExamMainRecordsToDb.Add (objMedicalExamWithControl.m_objclsMedicalExamMainRecord );
        //							else		//��ͬһ����¼�е���Ϣ������˲�����Ϣ
        //							{
        //								//���ڴ��ı����Ӧ���Բ���ִ�е��ˣ�OptionID�϶�Ψһ
        //								//��ѡ�����,RadioBoxʵ��ͬһ����¼����
        //								if(int.Parse (objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strOption_Type )==1)	//��ѡ
        //								{
        //									//�ḻԭ����¼��Ϣ
        //									clsMedicalExamMainRecordValue objOldRecord=(clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[intControlsInSameRecord];
        //									//���ڵ�ѡ�е��ı������ѡ����'����'ʱ��ʹ�ÿؼ���Tag���Ը��¸ü�¼��Ϣ��
        //									//����û��ѡ��'����'������Ҫ������Index��OptionValue��Ϣ,OptionText��������Ϊ��
        //									if((objMedicalExamWithControl.m_strControlType =="TextBox" || 
        //										objMedicalExamWithControl.m_strControlType =="RichTextBox" || 
        //										objMedicalExamWithControl.m_strControlType =="ctlBorderTextBox") && 
        //										objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index == objOldRecord.m_strSelected_Option_Index )
        //									{
        //										objOldRecord.m_strSelected_Option_Text =objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text   ;
        //									}
        //									else
        //									{
        //										//���������ԣ��϶����Ѿ�ѡ���˵ĵ�ѡ�ؼ�,objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index !=""��ʾ�ÿؼ�ѡ��
        //										if(objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index !="")
        //										{
        //											objOldRecord.m_strSelected_Option_Index =objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index;
        //											objOldRecord.m_strSelected_OptionValue_Text =objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text ;
        //										}
        //									}
        //									objArrMedicalExamMainRecordsToDb.RemoveAt (intControlsInSameRecord);
        //									objArrMedicalExamMainRecordsToDb.Add (objOldRecord);
        //								}
        //								else		//��ѡ��ʹ��GroupBox��ʵ��ͬһ����¼����
        //								{
        //									clsMedicalExamMainRecordValue objOldRecord=(clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[intControlsInSameRecord];
        //
        //									objOldRecord.m_strSelected_Option_Index ="" ;
        //									objOldRecord.m_strSelected_OptionValue_Text ="";
        //									if(objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text !="")
        //										objOldRecord.m_strSelected_Option_Text =objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text;			
        //									objArrMedicalExamMainRecordsToDb.RemoveAt (intControlsInSameRecord);
        //									objArrMedicalExamMainRecordsToDb.Add (objOldRecord);
        //								}
        //
        //							}
        //							#endregion
        //						}
        //						#endregion
        //						//!!!!!!!!!!!!////////////////////////////////////////////////////////////////
        //						clsMedicalExamUnitStringValue [] objMedicalExamUnitString=null;
        //						if(objArrMedicalExamMainRecordsToDb.Count >0)
        //						{
        //							objMedicalExamUnitString=new clsMedicalExamUnitStringValue[objArrMedicalExamMainRecordsToDb.Count];
        //							for(int i=0;i<objArrMedicalExamMainRecordsToDb.Count ;i++)
        //							{
        //								clsMedicalExamMainRecordValue objTempRecord=(clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[i];
        //								long lngCompart=long.Parse (objTempRecord.m_strElement_ID)*10000 + long.Parse (objTempRecord.m_strOption_ID);
        //
        //								objMedicalExamUnitString[i]=new clsMedicalExamUnitStringValue();
        //								objMedicalExamUnitString[i].m_strElement_ID =objTempRecord.m_strElement_ID ;
        //								objMedicalExamUnitString[i].m_strElementName =lngCompart.ToString ();		//��ʱ��������
        //								objMedicalExamUnitString[i].m_strOption_ID =objTempRecord.m_strOption_ID ;
        //								objMedicalExamUnitString[i].m_strOption_Name =objTempRecord.m_strOption_ID ;
        //								objMedicalExamUnitString[i].m_strOption_Type =objTempRecord.m_strOption_Type ;
        //								objMedicalExamUnitString[i].m_strSelected_Option_Index =objTempRecord.m_strSelected_Option_Index ;
        //								objMedicalExamUnitString[i].m_strSelected_Option_Text =objTempRecord.m_strSelected_Option_Text ;
        //								objMedicalExamUnitString[i].m_strSelected_OptionValue_DetailText =objTempRecord.m_strSelected_OptionValue_Text ;
        //								objMedicalExamUnitString[i].m_strSelected_OptionValue_Text =objTempRecord.m_strSelected_OptionValue_Text ;
        //							}
        //
        //							for(int i=0;i<objMedicalExamUnitString.Length -1;i++)
        //								for(int j=i+1;j<objMedicalExamUnitString.Length ;j++)
        //								{
        //									if(long.Parse (objMedicalExamUnitString[i].m_strElementName )>long.Parse (objMedicalExamUnitString[j].m_strElementName ))
        //									{
        //										clsMedicalExamUnitStringValue  objTemp=objMedicalExamUnitString[i];
        //										objMedicalExamUnitString[i]=objMedicalExamUnitString[j];
        //										objMedicalExamUnitString[j]=objTemp ;
        //									}
        //								}
        //							clsMedicalExamElementOptionUnionValue [] objElementOption=m_objMedicalExamSer.lngGetAllExamElementOptionUnion (clsLoginContext.s_ObjLoginContext.m_ObjPrincial);
        //							if(objElementOption!=null && objElementOption.Length >0)
        //							{
        //								for(int i=0;i<objMedicalExamUnitString.Length ;i++)
        //								{
        //									for(int j=0;j<objElementOption.Length ;j++)
        //									{
        //										if(objElementOption[j].m_strOE_ID.Trim () ==objMedicalExamUnitString[i].m_strElement_ID.Trim ())
        //											objMedicalExamUnitString[i].m_strElementName =objElementOption[j].m_strOE_NAME ;
        //										if(objElementOption[j].m_strOE_ID.Trim () ==objMedicalExamUnitString[i].m_strOption_ID.Trim () )
        //											objMedicalExamUnitString[i].m_strOption_Name =objElementOption[j].m_strOE_NAME ;
        ////										objMedicalExamUnitString[i].m_strOption_Type =objTempRecord.m_strOption_Type ;
        //									}
        //								}
        //							}
        //						}
        //						//!!!!!!!!!!!!!!!!!!!!/////////////////////////////////////////////////////////////////
        //						if(objMedicalExamUnitString==null || objMedicalExamUnitString.Length <=0)return("");
        //						#region �ϲ��ַ���,��ӱԴ,2003-5-27 8:56:02
        //						ArrayList objArrMedicalExamUnitString=new ArrayList ();
        //						objArrMedicalExamUnitString.Clear ();
        //						bool blnIsSeries2=false;
        //						string strOutput="";
        //						string strElement_ID=objMedicalExamUnitString[0].m_strElement_ID ;
        //						string strOption_ID=objMedicalExamUnitString[0].m_strOption_ID ;
        //						for(int i=0;i<objMedicalExamUnitString.Length ;i++)
        //						{
        //
        //							switch(int.Parse (objMedicalExamUnitString[i].m_strOption_Type))
        //							{
        //								case 0:
        //								case 1:
        //									blnIsSeries2=false;
        //									objArrMedicalExamUnitString.Add (objMedicalExamUnitString[i]);
        //									break;
        //								case 2:
        //									if(blnIsSeries2==false)
        //									{
        //										clsMedicalExamUnitStringValue objTempMedicalUnitString=objMedicalExamUnitString[i];		//�������һ����¼
        //										objTempMedicalUnitString.m_strSelected_OptionValue_DetailText = objMedicalExamUnitString[i].m_strSelected_OptionValue_DetailText + "��"; 
        //										objArrMedicalExamUnitString.Add (objTempMedicalUnitString );
        //									}
        //									else
        //									{
        //										int intMedicalIndex=objArrMedicalExamUnitString.Count -1;
        //										clsMedicalExamUnitStringValue objTempMedicalUnitString=(clsMedicalExamUnitStringValue) objArrMedicalExamUnitString[intMedicalIndex];		//�������һ����¼
        //										objTempMedicalUnitString.m_strSelected_OptionValue_DetailText += objMedicalExamUnitString[i].m_strSelected_OptionValue_DetailText + "��"; 
        //										objArrMedicalExamUnitString.RemoveAt (intMedicalIndex );
        //										objArrMedicalExamUnitString.Add (objTempMedicalUnitString );
        //									}
        //									blnIsSeries2=true;							
        //									break;
        //							}
        //						}
        //						#endregion
        //
        //						#region �����ַ���,��ӱԴ,2003-5-27 9:11:20
        //						string strOldElement_ID="";
        //						for(int i=0;i<objArrMedicalExamUnitString.Count ;i++)
        //						{
        //							clsMedicalExamUnitStringValue objTempMedicalUnitString=(clsMedicalExamUnitStringValue) objArrMedicalExamUnitString[i];
        //							if(i==0)
        //							{
        //								strOldElement_ID =objTempMedicalUnitString.m_strElement_ID ;
        //								strOutput=objTempMedicalUnitString.m_strElementName + ":\r\n";
        //							}
        //							if(objTempMedicalUnitString.m_strElement_ID !=strOldElement_ID)
        //							{
        //								strOutput +="\r\n\r\n" + objTempMedicalUnitString.m_strElementName + ":\r\n";
        //								strOldElement_ID=objTempMedicalUnitString.m_strElement_ID;
        //							}
        //							switch(int.Parse (objTempMedicalUnitString.m_strOption_Type ))
        //							{
        //								case 0:
        //									if(objTempMedicalUnitString.m_strSelected_Option_Text.Trim () !="")
        //										strOutput +=objTempMedicalUnitString.m_strOption_Name  + objTempMedicalUnitString.m_strSelected_Option_Text ;
        //									break;
        //								case 1:
        //									if(objTempMedicalUnitString.m_strSelected_Option_Text !=null && objTempMedicalUnitString.m_strSelected_Option_Text !="")
        //										strOutput +=objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_Text + "(" + objTempMedicalUnitString.m_strSelected_Option_Text + ")" ;
        //									else
        //										strOutput +=objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_Text;
        //									break;
        //								case 2:
        //									if(objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Length >0)
        //										objTempMedicalUnitString.m_strSelected_OptionValue_DetailText=objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Substring (0,objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Length -1);
        //									if(objTempMedicalUnitString.m_strSelected_Option_Text !=null && objTempMedicalUnitString.m_strSelected_Option_Text !="")
        //										strOutput +=objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_DetailText  + "(" + objTempMedicalUnitString.m_strSelected_Option_Text + ")" ;
        //									else
        //										strOutput +=objTempMedicalUnitString.m_strOption_Name+ objTempMedicalUnitString.m_strSelected_OptionValue_DetailText;
        //									break;
        //							}
        //							strOutput+="; ";
        //
        //																							
        //						}
        //						
        //						#endregion
        //
        //						return(strOutput);
        //
        //			#endregion
        //
        //		}

        #endregion

        /// <summary>
        /// �ϳ��ַ��� new
        /// </summary>
        /// <param name="p_ctl"></param>
        /// <returns></returns>
        public string m_strGetMedicalExamUnitString(Control p_ctl)
        {
            #region 555
            m_objArrDetailRecord.Clear();
            m_objArrMainRecordWithControls.Clear();

            m_mthInitLoadAllMedicalExamControls1(p_ctl);

            #region �������ؼ���Ϣת�뵽�ṹ����,��ӱԴ,2003-5-23 13:15:05

            string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            long lngDetailIndex = 1;
            for (int ii = 0; ii < m_objArrMedicalExamControls.Count; ii++)
            {
                //��ǰҪ��¼�Ŀؼ�
                clsMedicalExamWithControlValue objMedicalExamControlsInfo = new clsMedicalExamWithControlValue();

                Control ctlMedicalExam = (Control)m_objArrMedicalExamControls[ii];

                string strTag = ctlMedicalExam.Tag.ToString();


                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID = strGetBracketString(strTag, 1);
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID = strGetBracketString(strTag, 2);
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strActivity_Date = strCurrentDate;
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = strGetBracketString(strTag, 0);
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = "-1";
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text = "";
                objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = "";
                objMedicalExamControlsInfo.m_strControlType = ctlMedicalExam.GetType().Name;
                objMedicalExamControlsInfo.m_strControlName = ctlMedicalExam.Name;
                objMedicalExamControlsInfo.m_strControlText = ctlMedicalExam.Text;
                //д���û�������Ϣ
                switch (ctlMedicalExam.GetType().Name)
                {
                    case "ctlComboBox":
                        if (((ctlComboBox)ctlMedicalExam).SelectedIndex >= 0 && ((ctlComboBox)ctlMedicalExam).SelectedIndex < ((ctlComboBox)ctlMedicalExam).GetItemsCount())
                        {
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = ((ctlComboBox)ctlMedicalExam).SelectedIndex.ToString();
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ctlComboBox)ctlMedicalExam).Text;
                        }
                        else if (((ctlComboBox)ctlMedicalExam).Text != null)
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ctlComboBox)ctlMedicalExam).Text;

                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = "1";
                        break;
                    case "ComboBox":        //��ѡ,ʹ��ComboBox��ʵ��
                        if (((ComboBox)ctlMedicalExam).SelectedIndex >= 0 && ((ComboBox)ctlMedicalExam).SelectedIndex < ((ComboBox)ctlMedicalExam).Items.Count)
                        {
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = ((ComboBox)ctlMedicalExam).SelectedIndex.ToString();
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ComboBox)ctlMedicalExam).Text;
                        }
                        else if (((ComboBox)ctlMedicalExam).Text != null)
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((ComboBox)ctlMedicalExam).Text;

                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = "1";
                        break;
                    case "ctlBorderTextBox":
                    case "RichTextBox":         //�ı�(����ѡ����ѡ�е��ı�) 
                    case "TextBox":
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text = ctlMedicalExam.Text;
                        //objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index =strGetBracketString(strTag,3);
                        break;
                    case "RadioButton":     //��ѡ��ʹ��RadioButton Group��ʵ��
                        if (((RadioButton)ctlMedicalExam).Checked)
                        {
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index = strGetBracketString(strTag, 3);
                            objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text = ((RadioButton)ctlMedicalExam).Text;
                        }
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = "1";
                        break;
                    case "CheckBox":        //��ѡ
                                            //�������еĶ�ѡѡ��
                        if (((CheckBox)ctlMedicalExam).Checked)
                        {
                            clsMedicalExamDetailRecordValue objMedicalExamDetailRecordRecord = new clsMedicalExamDetailRecordValue();
                            objMedicalExamDetailRecordRecord.m_strActivity_Date = strCurrentDate;
                            objMedicalExamDetailRecordRecord.m_strDetailItem_ID = lngDetailIndex.ToString("0000");
                            objMedicalExamDetailRecordRecord.m_strElement_ID = objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID;
                            objMedicalExamDetailRecordRecord.m_strMedicalExam_ID = strGetMedicalExamID();
                            objMedicalExamDetailRecordRecord.m_strOption_ID = objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID;
                            objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes = strGetBracketString(strTag, 3);
                            objMedicalExamDetailRecordRecord.m_strSelected_OptionValue_Text = ctlMedicalExam.Text;
                            m_objArrDetailRecord.Add(objMedicalExamDetailRecordRecord);
                            lngDetailIndex++;
                        }
                        objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_Type = "2";
                        break;
                    case "CheckedListBox":
                        CheckedListBox ctl = (CheckedListBox)ctlMedicalExam;
                        for (int l = 0; l < ctl.Items.Count; l++)
                        {
                            if (ctl.GetItemChecked(l))
                            {
                                clsMedicalExamDetailRecordValue objMedicalExamDetailRecordRecord = new clsMedicalExamDetailRecordValue();
                                objMedicalExamDetailRecordRecord.m_strActivity_Date = strCurrentDate;
                                objMedicalExamDetailRecordRecord.m_strDetailItem_ID = lngDetailIndex.ToString("0000");
                                objMedicalExamDetailRecordRecord.m_strElement_ID = objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strElement_ID;
                                objMedicalExamDetailRecordRecord.m_strMedicalExam_ID = strGetMedicalExamID();
                                objMedicalExamDetailRecordRecord.m_strOption_ID = objMedicalExamControlsInfo.m_objclsMedicalExamMainRecord.m_strOption_ID;
                                objMedicalExamDetailRecordRecord.m_strSelected_Option_Indexes = l.ToString();
                                objMedicalExamDetailRecordRecord.m_strSelected_OptionValue_Text = ctl.Items[l].ToString();
                                m_objArrDetailRecord.Add(objMedicalExamDetailRecordRecord);
                                lngDetailIndex++;
                            }
                        }
                        break;
                }
                m_objArrMainRecordWithControls.Add(objMedicalExamControlsInfo);
            }
            #endregion

            #region �ϲ�ͬ��ؼ���Ϊͬһ����¼,��ӱԴ,2003-5-23 13:15:05

            //�ϲ�ͬһ����¼�е�Control
            clsMedicalExamWithControlValue objMedicalExamWithControl = null;
            clsMedicalExamMainRecordValue objMedicalExamRecord = null;
            ArrayList objArrMedicalExamMainRecordsToDb = new ArrayList();
            objArrMedicalExamMainRecordsToDb.Clear();
            int intControlsInSameRecord = -1;
            for (int i = 0; i < m_objArrMainRecordWithControls.Count; i++)
            {
                objMedicalExamWithControl = (clsMedicalExamWithControlValue)m_objArrMainRecordWithControls[i];

                #region �����Ƿ��Ѿ�������˿ؼ��ļ�¼,����ʹ��RadioBox���CheckBox��(�������е�TextBox)�ų��ִ����,��ӱԴ,2003-5-23 13:17:48
                intControlsInSameRecord = -1;

                for (int j = 0; j < objArrMedicalExamMainRecordsToDb.Count; j++)
                {
                    objMedicalExamRecord = (clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[j];
                    //�Ƿ���MainRecord�б�ʾͬһ����¼�еĶ���ؼ�
                    if (objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strElement_ID == objMedicalExamRecord.m_strElement_ID &&
                        objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strOption_ID == objMedicalExamRecord.m_strOption_ID)
                        intControlsInSameRecord = j;            //��¼���Ѿ��е�Index
                }
                #endregion

                #region �ϲ�ͬ��ؼ�Ϊһ����¼,��ӱԴ,2003-5-23 13:19:40
                if (intControlsInSameRecord == -1)              //����û�м�����˿ؼ�ѡ��
                    objArrMedicalExamMainRecordsToDb.Add(objMedicalExamWithControl.m_objclsMedicalExamMainRecord);
                else        //��ͬһ����¼�е���Ϣ������˲�����Ϣ
                {
                    //���ڴ��ı����Ӧ���Բ���ִ�е��ˣ�OptionID�϶�Ψһ
                    //��ѡ�����,RadioBoxʵ��ͬһ����¼����
                    if (int.Parse(objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strOption_Type) == 1)   //��ѡ,ctlComboBox
                    {
                        //�ḻԭ����¼��Ϣ
                        clsMedicalExamMainRecordValue objOldRecord = (clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[intControlsInSameRecord];
                        //���ڵ�ѡ�е��ı������ѡ����'����'ʱ��ʹ�ÿؼ���Tag���Ը��¸ü�¼��Ϣ��
                        //����û��ѡ��'����'������Ҫ������Index��OptionValue��Ϣ,OptionText��������Ϊ��
                        if ((objMedicalExamWithControl.m_strControlType == "TextBox" ||
                            objMedicalExamWithControl.m_strControlType == "RichTextBox" ||
                            objMedicalExamWithControl.m_strControlType == "ctlBorderTextBox") &&
                            objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index == objOldRecord.m_strSelected_Option_Index)
                        {
                            objOldRecord.m_strSelected_Option_Text = objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text;
                        }
                        else
                        {
                            //���������ԣ��϶����Ѿ�ѡ���˵ĵ�ѡ�ؼ�,objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index !=""��ʾ�ÿؼ�ѡ��
                            if (objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index != "")
                            {
                                objOldRecord.m_strSelected_Option_Index = objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Index;
                                objOldRecord.m_strSelected_OptionValue_Text = objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_OptionValue_Text;
                            }
                        }
                        objArrMedicalExamMainRecordsToDb.RemoveAt(intControlsInSameRecord);
                        objArrMedicalExamMainRecordsToDb.Add(objOldRecord);
                    }
                    else        //��ѡ��ʹ��GroupBox��ʵ��ͬһ����¼����
                    {
                        clsMedicalExamMainRecordValue objOldRecord = (clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[intControlsInSameRecord];

                        objOldRecord.m_strSelected_Option_Index = "";
                        objOldRecord.m_strSelected_OptionValue_Text = "";
                        if (objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text != "")
                            objOldRecord.m_strSelected_Option_Text = objMedicalExamWithControl.m_objclsMedicalExamMainRecord.m_strSelected_Option_Text;
                        objArrMedicalExamMainRecordsToDb.RemoveAt(intControlsInSameRecord);
                        objArrMedicalExamMainRecordsToDb.Add(objOldRecord);
                    }

                }
                #endregion
            }
            #endregion
            //!!!!!!!!!!!!////////////////////////////////////////////////////////////////
            clsMedicalExamUnitStringValue[] objMedicalExamUnitString = null;
            if (objArrMedicalExamMainRecordsToDb.Count > 0)
            {
                objMedicalExamUnitString = new clsMedicalExamUnitStringValue[objArrMedicalExamMainRecordsToDb.Count];
                for (int i = 0; i < objArrMedicalExamMainRecordsToDb.Count; i++)
                {
                    clsMedicalExamMainRecordValue objTempRecord = (clsMedicalExamMainRecordValue)objArrMedicalExamMainRecordsToDb[i];
                    long lngCompart = long.Parse(objTempRecord.m_strElement_ID) * 10000 + long.Parse(objTempRecord.m_strOption_ID);

                    objMedicalExamUnitString[i] = new clsMedicalExamUnitStringValue();
                    objMedicalExamUnitString[i].m_strElement_ID = objTempRecord.m_strElement_ID;
                    objMedicalExamUnitString[i].m_strElementName = lngCompart.ToString();       //��ʱ��������
                    objMedicalExamUnitString[i].m_strOption_ID = objTempRecord.m_strOption_ID;
                    objMedicalExamUnitString[i].m_strOption_Name = objTempRecord.m_strOption_ID;
                    objMedicalExamUnitString[i].m_strOption_Type = objTempRecord.m_strOption_Type;
                    objMedicalExamUnitString[i].m_strSelected_Option_Index = objTempRecord.m_strSelected_Option_Index;
                    objMedicalExamUnitString[i].m_strSelected_Option_Text = objTempRecord.m_strSelected_Option_Text;
                    objMedicalExamUnitString[i].m_strSelected_OptionValue_DetailText = objTempRecord.m_strSelected_OptionValue_Text;
                    objMedicalExamUnitString[i].m_strSelected_OptionValue_Text = objTempRecord.m_strSelected_OptionValue_Text;
                }

                for (int i = 0; i < objMedicalExamUnitString.Length - 1; i++)
                    for (int j = i + 1; j < objMedicalExamUnitString.Length; j++)
                    {
                        if (long.Parse(objMedicalExamUnitString[i].m_strElementName) > long.Parse(objMedicalExamUnitString[j].m_strElementName))
                        {
                            clsMedicalExamUnitStringValue objTemp = objMedicalExamUnitString[i];
                            objMedicalExamUnitString[i] = objMedicalExamUnitString[j];
                            objMedicalExamUnitString[j] = objTemp;
                        }
                    }

                //clsMedicalExamService m_objMedicalExamSer =
                //    (clsMedicalExamService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicalExamService));

                clsMedicalExamElementOptionUnionValue[] objElementOption = null;
                try
                {
                    objElementOption = (new weCare.Proxy.ProxyEmr01()).Service.lngGetAllExamElementOptionUnion();
                }
                finally
                {
                    //m_objMedicalExamSer.Dispose();
                }
                if (objElementOption != null && objElementOption.Length > 0)
                {
                    for (int i = 0; i < objMedicalExamUnitString.Length; i++)
                    {
                        for (int j = 0; j < objElementOption.Length; j++)
                        {
                            if (objElementOption[j].m_strOE_ID.Trim() == objMedicalExamUnitString[i].m_strElement_ID.Trim())
                                objMedicalExamUnitString[i].m_strElementName = objElementOption[j].m_strOE_NAME;
                            if (objElementOption[j].m_strOE_ID.Trim() == objMedicalExamUnitString[i].m_strOption_ID.Trim())
                                objMedicalExamUnitString[i].m_strOption_Name = objElementOption[j].m_strOE_NAME;
                        }
                    }
                }
            }
            //!!!!!!!!!!!!!!!!!!!!/////////////////////////////////////////////////////////////////
            if (objMedicalExamUnitString == null || objMedicalExamUnitString.Length <= 0) return ("");
            #region �ϲ��ַ���,��ӱԴ,2003-5-27 8:56:02
            ArrayList objArrMedicalExamUnitString = new ArrayList();
            objArrMedicalExamUnitString.Clear();
            bool blnIsSeries2 = false;
            string strElement_ID = objMedicalExamUnitString[0].m_strElement_ID;
            string strOption_ID = objMedicalExamUnitString[0].m_strOption_ID;
            for (int i = 0; i < objMedicalExamUnitString.Length; i++)
            {

                switch (int.Parse(objMedicalExamUnitString[i].m_strOption_Type))
                {
                    case 0:
                    case 1:
                        blnIsSeries2 = false;
                        objArrMedicalExamUnitString.Add(objMedicalExamUnitString[i]);
                        break;
                    case 2:
                        if (blnIsSeries2 == false)
                        {
                            clsMedicalExamUnitStringValue objTempMedicalUnitString = objMedicalExamUnitString[i];       //�������һ����¼
                            objTempMedicalUnitString.m_strSelected_OptionValue_DetailText = objMedicalExamUnitString[i].m_strSelected_OptionValue_DetailText + "��";
                            objArrMedicalExamUnitString.Add(objTempMedicalUnitString);
                        }
                        else
                        {
                            int intMedicalIndex = objArrMedicalExamUnitString.Count - 1;
                            clsMedicalExamUnitStringValue objTempMedicalUnitString = (clsMedicalExamUnitStringValue)objArrMedicalExamUnitString[intMedicalIndex];       //�������һ����¼
                            objTempMedicalUnitString.m_strSelected_OptionValue_DetailText += objMedicalExamUnitString[i].m_strSelected_OptionValue_DetailText + "��";
                            objArrMedicalExamUnitString.RemoveAt(intMedicalIndex);
                            objArrMedicalExamUnitString.Add(objTempMedicalUnitString);
                        }
                        blnIsSeries2 = true;
                        break;
                }
            }
            #endregion

            #region �����ַ���,��ӱԴ,2003-5-27 9:11:20
            string strOldElement_ID = "";
            System.Text.StringBuilder sbOut = new System.Text.StringBuilder("");
            for (int i = 0; i < objArrMedicalExamUnitString.Count; i++)
            {
                clsMedicalExamUnitStringValue objTempMedicalUnitString = (clsMedicalExamUnitStringValue)objArrMedicalExamUnitString[i];
                #region ��ʾ��������
                //				if(i==0)
                //				{
                //					strOldElement_ID =objTempMedicalUnitString.m_strElement_ID ;
                //					sbOut.Append(objTempMedicalUnitString.m_strElementName + ":\r\n");
                //				}
                //				if(objTempMedicalUnitString.m_strElement_ID !=strOldElement_ID)
                //				{
                //					sbOut.Append("\r\n\r\n" + objTempMedicalUnitString.m_strElementName + ":\r\n");
                //					strOldElement_ID=objTempMedicalUnitString.m_strElement_ID;
                //				}
                #endregion
                switch (int.Parse(objTempMedicalUnitString.m_strOption_Type))
                {
                    case 0:
                        if (objTempMedicalUnitString.m_strSelected_Option_Text.Trim() != "")
                            sbOut.Append(objTempMedicalUnitString.m_strOption_Name.Replace("��", objTempMedicalUnitString.m_strSelected_Option_Text) + ",");
                        break;
                    case 1:
                        if (objTempMedicalUnitString.m_strSelected_Option_Text != "" || objTempMedicalUnitString.m_strSelected_OptionValue_Text != "")
                            sbOut.Append(objTempMedicalUnitString.m_strOption_Name.Replace("��", objTempMedicalUnitString.m_strSelected_OptionValue_Text).Replace("��", objTempMedicalUnitString.m_strSelected_Option_Text) + ",");
                        //						else
                        //							strOutput +=objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_Text  + ",";
                        break;
                    case 2:
                        if (objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Length > 0)
                            objTempMedicalUnitString.m_strSelected_OptionValue_DetailText = objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Substring(0, objTempMedicalUnitString.m_strSelected_OptionValue_DetailText.Length - 1) + ",";
                        if (objTempMedicalUnitString.m_strSelected_Option_Text != null && objTempMedicalUnitString.m_strSelected_Option_Text != "")
                            //							strOutput +=objTempMedicalUnitString.m_strOption_Name + objTempMedicalUnitString.m_strSelected_OptionValue_DetailText  + "(" + objTempMedicalUnitString.m_strSelected_Option_Text + ")"  + ",";
                            sbOut.Append(objTempMedicalUnitString.m_strOption_Name.Replace("��", objTempMedicalUnitString.m_strSelected_OptionValue_DetailText + "(" + objTempMedicalUnitString.m_strSelected_Option_Text + ")") + ",");
                        //						else
                        //							strOutput +=objTempMedicalUnitString.m_strOption_Name+ objTempMedicalUnitString.m_strSelected_OptionValue_DetailText  + ",";
                        break;
                }


            }

            #endregion


            string strOutput = sbOut.ToString();
            if (strOutput.Length > 0)
                strOutput = strOutput.Substring(0, strOutput.Length - 1) + "��";

            return (strOutput);

            #endregion
        }

        /*------------------------------------------------------------------------*/
    }


}


