using System;
using System.Data;
using System.Windows.Forms; 
using System.Text.RegularExpressions;
using Sybase.DataWindow;
using com.digitalwave.iCare.gui.HIS.Reports;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsController_StatReport ��ժҪ˵����
    /// </summary>
    public class clsCtl_MicReport : com.digitalwave.GUI_Base.clsController_Base
    {

        #region ���캯��
        public clsCtl_MicReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDcl_MicReport();
        }
        #endregion

        #region ʵ��
        /// <summary>
        /// ϸ���ֲ�
        /// </summary>
        public class EntityBacteriaDistribution
        {
            public string xjmc { get; set; }
            public decimal xjzs { get; set; }
            public string bfb { get; set; }
            public string xjbfb { get; set; }
        }

        /// <summary>
        /// �ۼ������Ա���
        /// </summary>
        public class EntityMicSensitive
        {
            public string antiname { get; set; }
            public decimal miccount { get; set; }
            public decimal sensitive { get; set; }
            public decimal intermediary { get; set; }
            public decimal resistance { get; set; }
            public string sensitiveRate { get; set; }
            public string intermediaryRate { get; set; }
            public string resistanceRate { get; set; }
            public string criticalName { get; set; }
        }

        /// <summary>
        /// �ۼ�MIC���� 
        /// </summary>
        public class EntityMicCumulative
        {
            public string antiname { get; set; }
            public decimal total { get; set; }
            public decimal perCount_1 { get; set; }
            public decimal perCount_2 { get; set; }
            public decimal perCount_3 { get; set; }
            public decimal perCount_4 { get; set; }
            public decimal perCount_5 { get; set; }
            public decimal perCount_6 { get; set; }
            public decimal perCount_7 { get; set; }
            public decimal perCount_8 { get; set; }
            public decimal perCount_9 { get; set; }
            public decimal perCount_10 { get; set; }
            public decimal perCount_11 { get; set; }
            public decimal perCount_12 { get; set; }
            public string per_1 { get; set; }
            public string per_2 { get; set; }
            public string per_3 { get; set; }
            public string per_4 { get; set; }
            public string per_5 { get; set; }
            public string per_6 { get; set; }
            public string per_7 { get; set; }
            public string per_8 { get; set; }
            public string per_9 { get; set; }
            public string per_10 { get; set; }
            public string per_11 { get; set; }
            public string per_12 { get; set; }
        }

        /// <summary>
        /// ϸ���ֲ�����
        /// </summary>
        public class EntityMicdistributiontend
        {
            public string month { get; set; }
            public List<EntityMicdistribution> data;
        }

        public class EntityMicdistribution
        {
            public string antiname { get; set; }
            public string monthPer { get; set; }
            public decimal antiCount { get; set; }
            public string antiPer { get; set; }
        }

        public class EntityMic
        {
            public string antiname { get; set; }
            public decimal antiCount { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class EntityAntiData
        {
            public string antiName { get; set; }
            public decimal anticCount { get; set; }

        }

        /// <summary>
        /// ������
        /// </summary>
        public class EntitySenRate
        {
            public string antiName { get; set; }
            public decimal antiCount { get; set; }
            public decimal antiSenCount { get; set; }
            public string antiSenRate { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class EntityDetail
        {
            public string HZXM { get; set; }//����
            public string SEX { get; set; }//�Ա�
            public string AGE { get; set; }//����
            public string SJKS { get; set; }//����
            public string BGBH { get; set; }//������
            public string BBLX { get; set; }//�걾����
            public string KH { get; set; }//סԺ/�����
            public string CHECKITEM { get; set; }//��Ŀ
            public string RESULT { get; set; }//���
            public string REFRANGE { get; set; }//MIC
        }

        /// <summary>
        /// 
        /// </summary>
        public class EntityCriticalResult
        {
            public string criticalResult { get; set; }
            public decimal criticalCount { get; set; }
        }

        /// <summary>
        /// ���������� Σ��ֵ
        /// </summary>
        public class EntitySenRateTendCritical
        {
            public string month { get; set; }
            public string criticalName { get; set; }
            public decimal criticalCount { get; set; }
        }

        /// <summary>
        /// ϸ���ֲ����� ϸ�� ��/��
        /// </summary>
        public class EnityAntiDisTend
        {
            public string month { get; set; }
            public decimal antiCount { get; set; }
        }

        /// <summary>
        /// ���Ա걾��Դ��
        /// </summary>
        public class EnitySampleSum
        {
            public  string sampleType{ get; set; }
            public decimal count{ get; set; }
            public string countPer { get; set; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public class EntityAntiApp
        {
            public string antiName { get;set;}
            public string appStr { get; set; }
            public decimal antiCount {get;set;}
        }

        /// <summary>
        /// �����
        /// </summary>
        internal int strQuery;
        private frmMicReport m_objViewer;
        string strTempName = null;
        internal string strTempAnti = null;
        internal DateTime dtDateFrom;
        internal DateTime dtDateTo;
        internal bool IsEnglish = false;
        private clsDcl_MicReport m_objManage;
        Dictionary<string, string> dicSampleType = new Dictionary<string, string>();

        internal string sampleId = null;
        internal string DisNo = null;
        internal string Sex = null;
        internal string AgeFrom = null;
        internal string AgeTo = null;
        internal string TestMethod = null;
        internal string DeptIdArr = null;
        internal string CriticalStr = string.Empty;
        internal string GlFlgStr = string.Empty;
        internal DataTable dtGlAnti = null;

        #region ���ô������
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmMicReport)frmMDI_Child_Base_in;
        }
        #endregion

        #region init
        /// <summary>
        /// init
        /// </summary>
        public void m_mthInti()
        {
            DataTable dtbResult;
            m_objViewer.tabContorl.Visible = false;
            m_objViewer.m_cboCondition.Items.AddRange(new object[] { "ϸ���ֲ����Ʊ���", "�����ʱ���", "���������Ʊ���", "�ۼ������Ա���", "�ۼ�MIC����", "ϸ���ֲ�����ͳ��", "ϸ���ֲ����걾����ͳ��", "ϸ���ֲ�������ͳ��", "΢������ϸͳ��","���Ա걾��Դ��" });
            m_objViewer.cbxDistrict.Items.AddRange(new object[] { "סԺ", "����", "���" });
            m_objManage.lngGetSamType(out dtbResult);

            for (int i = 0; i < dtbResult.Rows.Count; i++)
            {
                m_objViewer.cbxSampleType.Items.Add(dtbResult.Rows[i]["sample_type_desc_vchr"].ToString());
                dicSampleType.Add(dtbResult.Rows[i]["sample_type_id_chr"].ToString(), dtbResult.Rows[i]["sample_type_desc_vchr"].ToString());
            }

            m_mthListGlMic();
        }
        #endregion

        #region �г����п�����
        /// <summary>
        /// �г�������
        /// </summary>
        public void m_mthListAnti()
        {
            DataTable dtbResult;
            long lngRes = m_objManage.lngGetAllAnti(out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                m_objViewer.dgvItem.DataSource = dtbResult;
            }
        }
        #endregion

        #region �г�����ϸ��
        /// <summary>
        /// �г�ϸ��
        /// </summary>
        public void m_mthListMic()
        {
            DataTable dtbResult;
            long lngRes = m_objManage.lngGetAllMic(out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                m_objViewer.dgvItem.DataSource = dtbResult;
            }
        }
        #endregion

        #region �г����и���ϸ��
        /// <summary>
        /// �г����и���ϸ��
        /// </summary>
        public void m_mthListGlMic()
        {
            m_objManage.lngGetAllGlMic(out dtGlAnti);
        }
        #endregion

        #region �����ı���仯��ѯϸ��
        /// <summary>
        /// �����ı���仯��ѯϸ��
        /// </summary>
        public void m_mthtxtChangeQueryMic()
        {
            DataTable dtbResult;

            Regex regex = new Regex("^[A-Za-z0-9]+$");
            strTempName = m_objViewer.txtSearchName.Text.Trim();
            if (strTempName == string.Empty)
            {
                return;
            }
            IsEnglish = regex.IsMatch(strTempName);

            long lngRes = m_objManage.lngGetFuzzyQueryMic(strTempName.ToUpper(), IsEnglish, out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                m_objViewer.dgvItem.DataSource = dtbResult;
            }
        }
        #endregion

        #region �����ı���仯��ѯ������
        /// <summary>
        /// �����ı���仯��ѯ������
        /// </summary>
        public void m_mthtxtChangeQueryAnti()
        {
            DataTable dtbResult;

            Regex regex = new Regex("^[A-Za-z0-9]+$");
            strTempName = m_objViewer.txtSearchName.Text.Trim();
            if (strTempName == string.Empty)
            {
                return;
            }
            IsEnglish = regex.IsMatch(strTempName);

            long lngRes = m_objManage.lngGetFuzzyQueryAnti(strTempName.ToUpper(), IsEnglish, out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                m_objViewer.dgvItem.DataSource = dtbResult;
            }
        }
        #endregion

        #region ϸ���ֲ�ͳ�Ʊ���   v
        /// <summary>
        /// ϸ���ֲ�ͳ��
        /// </summary>
        public void m_mthGetBacStatstic()
        {
            DataTable dtbResult;
            if (m_objViewer.dgv_mic.Rows.Count > 1)
            {
                strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                strTempName = null;
            }
            string applicationStr = string.Empty;
            long lngRes = -1;
            DataTable dtbMicAp = null;
            sampleId = getSampleId(this.m_objViewer.cbxSampleType.Text.Trim());
            lngRes = m_objManage.lngGetMicApplication(strTempName, dtDateFrom, dtDateTo, CriticalStr, DeptIdArr, out dtbMicAp);
            if (lngRes > 0 && dtbMicAp.Rows.Count > 0)
            {
                if (dtbMicAp != null && dtbMicAp.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbMicAp.Rows)
                    {
                        applicationStr += "'" + dr["application_id_chr"].ToString() + "',";
                    }
                    applicationStr = "(" + applicationStr.TrimEnd(',') + ")";
                }
            }

            lngRes = m_objManage.lngGetBacteriaDistribution(strTempName, dtDateFrom, dtDateTo,applicationStr, DeptIdArr, sampleId, DisNo, Sex, AgeFrom, AgeTo, TestMethod, out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                this.FillDWBacStatstic(dtbResult);
            }
            else
            {
                MessageBox.Show("û�в鵽û�����ݡ�");
            }
        }

        /// <summary>
        /// ���DW ϸ���ֲ�ͳ�Ʊ���
        /// </summary>
        /// <param name="intall"></param>
        /// <param name="dtRsult"></param>
        public void FillDWBacStatstic(DataTable dtbResult)
        {
            try
            {
                decimal total = 0;
                decimal sumTotal = 0;
                int flg = 0;
                List<EntityBacteriaDistribution> data = new List<EntityBacteriaDistribution>();
                foreach (DataRow dr in dtbResult.Rows)
                {
                    #region ����

                    string age = dr["age_chr"].ToString();
                    double ageDec = 0;

                    if (!string.IsNullOrEmpty(m_objViewer.txtAgeFrom.Text) && !string.IsNullOrEmpty(m_objViewer.txtAgeTo.Text))
                    {
                        double ageFrom = Convert.ToInt32(m_objViewer.txtAgeFrom.Text);
                        double ageTo = Convert.ToInt32(m_objViewer.txtAgeTo.Text);

                        if (age.Contains("��") || age.Contains("��"))
                        {
                            age = age.Replace("��", "").Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = 0.5;
                            else
                                continue;
                        }
                        if (age.Contains("��"))
                        {
                            age = age.Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = Convert.ToInt32(age);
                            else
                                continue;
                        }

                        if (ageDec < ageFrom || ageDec > ageTo)
                            continue;
                    }

                    #endregion

                    flg = 0;
                    string XJMC = dr["XJMC"].ToString().Trim();
                    if (!XJMC.Contains("����"))
                    {
                        if (XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                            continue;
                    }

                    XJMC = XJMC.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "").Replace("��", "(");

                    int idx = XJMC.IndexOf("(");
                    if (idx > 0)
                        XJMC = XJMC.Substring(0, idx);
                    sumTotal++;
                    #region �����ж�

                    if (GlFlgStr == "1")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + XJMC + "' and glys = '" +"+'");
                        if (drr != null && drr.Length > 0)
                        {

                        }
                        else
                            continue;
                    }

                    if (GlFlgStr == "0")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + XJMC + "' and glys = '" + "-'");
                        if (drr != null && drr.Length > 0)
                        {

                        }
                        else
                            continue;
                    }

                    #endregion

                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (XJMC == data[i].xjmc)
                            {
                                data[i].xjzs++;
                                total++;
                                flg = 1;
                                break;
                            }
                        }
                    }

                    if (flg == 0)
                    {
                        EntityBacteriaDistribution vo = new EntityBacteriaDistribution();
                        vo.xjmc = XJMC;
                        vo.xjzs = 1;
                        total++;
                        data.Add(vo);
                    }
                }

                for (int i = 0; i < data.Count; i++)
                {
                    data[i].bfb = (data[i].xjzs / total).ToString("0.00%");
                    data[i].xjbfb = (data[i].xjzs / sumTotal).ToString("0.00%");
                }

                if (data.Count > 0)
                {
                    //ϸ������
                    m_objViewer.lblBacStatstic.Text = "ϸ������:(" + sumTotal + ") " +"        "+ "����ϸ������:(" + total + ") ";
                    m_objViewer.lblBacStatstic.Visible = true;
                }

                m_objViewer.dgvXjfbbbg.DataSource = data;
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }
        #endregion

        #region �ۼ������Ա���   v
        /// <summary>
        /// �ۼ������Ա���
        /// </summary>
        public void m_mthGetMicSensitive()
        {
            DataTable dtbResult = null; ;
            DataTable dtbMicAp = null;
            DataTable dtbGSS = null;
            string applictionStr = string.Empty;
            long lngRes = -1;
            List<EntityMicSensitive> lstCritical = new List<EntityMicSensitive>();
            List<EntityAntiApp> lstAntiApp = new List<EntityAntiApp>();
            sampleId = getSampleId(m_objViewer.cbxSampleType.Text.Trim());
            if (m_objViewer.dgv_mic.Rows.Count > 0)
            {
                for (int i = 0; i < m_objViewer.dgv_mic.Rows.Count; i++)
                {
                    EntityAntiApp vo = new EntityAntiApp();
                    vo.antiName = m_objViewer.dgv_mic.Rows[i].Cells[0].Value.ToString();
                    #region �����ж�

                    if (GlFlgStr == "1")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + vo.antiName + "' and glys = '" + "+'");
                        if (drr == null || drr.Length <= 0)
                        {
                            continue;
                        }
                    }

                    if (GlFlgStr == "0")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + vo.antiName + "' and glys = '" + "-'");
                        if (drr == null || drr.Length <= 0)
                        {
                            continue;
                        }
                    }

                    #endregion
                    lstAntiApp.Add(vo);
                }
            }

            lngRes = m_objManage.lngGetMicApplication("",dtDateFrom, dtDateTo, CriticalStr, DeptIdArr, out dtbMicAp);
            if (lngRes > 0 && dtbMicAp.Rows.Count > 0)
            {
                if (dtbMicAp != null && dtbMicAp.Rows.Count > 0)
                {
                    int flgCrit = 0;

                    foreach (DataRow dr in dtbMicAp.Rows)
                    {
                        #region  �� ϸ��
                        string XJMC = dr["xjmc"].ToString().Trim();

                        if (!XJMC.Contains("����"))
                        {
                            if (XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                                continue;
                        }

                        XJMC = XJMC.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "").Replace("��", "(");

                        int idx = XJMC.IndexOf("(");
                        if (idx > 0)
                            XJMC = XJMC.Substring(0, idx);

                        #endregion

                        #region �����ж�

                        if (GlFlgStr == "1")
                        {
                            DataRow[] drr = dtGlAnti.Select("xjmc = '" + XJMC + "' and glys = '" + "+'");
                            if (drr == null || drr.Length <= 0)
                            {
                                continue;
                            }
                        }

                        if (GlFlgStr == "0")
                        {
                            DataRow[] drr = dtGlAnti.Select("xjmc = '" + XJMC + "' and glys = '" + "-'");
                            if (drr == null || drr.Length <= 0)
                            {
                                continue;
                            }
                        }

                        #endregion

                        #region ϸ������ ��Ӧ APPLICATIONID
                        if (m_objViewer.dgv_mic.Rows.Count > 0)
                        {
                            for (int i = 0; i < lstAntiApp.Count; i++)
                            {
                                if (lstAntiApp[i].antiName == XJMC)
                                {
                                    lstAntiApp[i].appStr += "'" + dr["application_id_chr"].ToString() + "',";
                                    lstAntiApp[i].antiCount++;
                                }
                            }
                        }
                        else
                        {
                            int micFlg = 0;
                            for (int i = 0; i < lstAntiApp.Count; i++)
                            {
                                if (lstAntiApp[i].antiName == XJMC)
                                {
                                    micFlg = 1;
                                    lstAntiApp[i].appStr += "'" + dr["application_id_chr"].ToString() + "',";
                                    lstAntiApp[i].antiCount++;
                                }
                            }
                            if (micFlg == 0)
                            {
                                EntityAntiApp vo = new EntityAntiApp();
                                vo.antiName = XJMC;
                                vo.appStr += "'" + dr["application_id_chr"].ToString() + "',";
                                vo.antiCount = 1;

                                lstAntiApp.Add(vo);
                            }
                        }
                      
                        #endregion

                        applictionStr += "'" + dr["application_id_chr"].ToString() + "',";

                        #region Σ��ֵ
                        string criticalResult = dr["criticalResult"].ToString();
                        flgCrit = 0;
                        for (int i = 0; i < lstCritical.Count; i++)
                        {
                            if (lstCritical[i].criticalName == criticalResult)
                            {
                                lstCritical[i].miccount++;
                                flgCrit = 1;
                                break;
                            }
                        }

                        if (flgCrit == 0 && !string.IsNullOrEmpty(criticalResult))
                        {
                            EntityMicSensitive vo = new EntityMicSensitive();
                            vo.antiname = XJMC;
                            vo.criticalName = criticalResult;
                            vo.miccount = 1;
                            lstCritical.Add(vo);
                        }
                        #endregion
                    }

                    lstAntiApp = antiSort( lstAntiApp);

                    applictionStr = "(" + applictionStr.TrimEnd(',') + ")";
                }
            }
            if (!string.IsNullOrEmpty(applictionStr))
                lngRes = m_objManage.lngGetMicSensitive(applictionStr, sampleId, DisNo, Sex, TestMethod, strTempName, out dtbResult);
            else
            {
                MessageBox.Show("û��������ݡ�");
                return;
            }
                

            #region ������
            List<string> lstGSS = new List<string>();
            lngRes = m_objManage.lngGetGss(applictionStr, out dtbGSS);
            if (lngRes >= 0 && dtbGSS.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbGSS.Rows)
                {
                    string gssName = dr["ITEMNAME"].ToString().Trim();
                    if (string.IsNullOrEmpty(gssName) || gssName == "ϸ������" || gssName == "�������" || gssName.Contains("�������"))
                        continue;
                    else
                        lstGSS.Add(gssName);
                }
            }
            #endregion

            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                this.FillDWMicSensitive(lstAntiApp, dtbResult, strTempName,lstGSS, lstCritical);
            }
            else
            {
                MessageBox.Show("û��������ݡ�");
            }
        }

        public void FillDWMicSensitive(List<EntityAntiApp> lstAntiApp, DataTable dtbResult, string strTempName,List<string> lstGSS, List<EntityMicSensitive> lstCritical)
        {
            try
            {
                string Gss = string.Empty;
                decimal decCritical = 0;
                string criticalName = string.Empty;
                DataTable dtData = new DataTable();
                dtData.Columns.Add("������\\ϸ��", Type.GetType("System.String"));

                #region �� Σ��ֵ
                if (lstCritical.Count > 0)
                {
                    for (int cI = 0; cI < lstCritical.Count; cI++)
                    {
                        DataRow newRow;
                        newRow = dtData.NewRow();
                        newRow[0] = lstCritical[cI].criticalName;
                        dtData.Rows.Add(newRow);
                    }
                }
                #endregion

                #region �� ϸ��
                if (lstAntiApp.Count > 0)
                {
                    for (int mI = 0; mI < lstAntiApp.Count; mI++)
                    {
                        dtData.Columns.Add(lstAntiApp[mI].antiName + "(" + lstAntiApp[mI].antiCount + ")");
                    }
                }
                #endregion

                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < lstAntiApp.Count; i++)
                    {
                        string appStr = "(" + lstAntiApp[i].appStr.TrimEnd(',') + ")";

                        for (int iG = 0; iG < lstGSS.Count; iG++)
                        {
                            int rowFlg = 0;
                            Gss = lstGSS[iG];
                            if (string.IsNullOrEmpty(Gss) || Gss == lstAntiApp[i].antiName || Gss == "ϸ������" || Gss == "�������")
                                continue;
                            DataRow[] drrAntiType = dtbResult.Select("application_id_chr in " + appStr + " and itemname = '" + Gss + "'");

                            if (drrAntiType.Length <= 0 || drrAntiType == null)
                                continue;
                            //����
                            DataRow[] drrAntiTypeS = dtbResult.Select("application_id_chr in " + appStr + " and itemname = '" + Gss + "'" + " and trim(RESULT) = '" + "����'");
                            DataRow[] drrAntiTypeM = dtbResult.Select("application_id_chr in " + appStr + " and itemname = '" + Gss + "'" + " and trim(RESULT) = '" + "�н�'");
                            DataRow[] drrAntiTypeR = dtbResult.Select("application_id_chr in " + appStr + " and itemname = '" + Gss + "'" + " and trim(RESULT) = '" + "��ҩ'");

                            string sPer = string.Empty;
                            string mPer = string.Empty;
                            string rPer = string.Empty;
                            if ( drrAntiType.Length > 0)
                                sPer = (Convert.ToDecimal(drrAntiTypeS.Length) / Convert.ToDecimal(drrAntiType.Length)).ToString("0.00%");
                            if ( drrAntiType.Length > 0)
                                mPer = (Convert.ToDecimal(drrAntiTypeM.Length) / Convert.ToDecimal(drrAntiType.Length)).ToString("0.00%");
                            if ( drrAntiType.Length > 0)
                                rPer = (Convert.ToDecimal(drrAntiTypeR.Length) / Convert.ToDecimal(drrAntiType.Length)).ToString("0.00%");

                            decCritical = 0;
                            criticalName = string.Empty;
                            for (int cI = 0; cI < lstCritical.Count; cI++)
                            {
                                if (lstAntiApp[i].antiName == lstCritical[cI].antiname)
                                {
                                    decCritical = lstCritical[cI].miccount;
                                    criticalName = lstCritical[cI].criticalName;
                                }
                            }

                            for (int rowI = 0; rowI < dtData.Rows.Count; rowI++)
                            {
                                if (dtData.Rows[rowI][0].ToString() == Gss)
                                {
                                    rowFlg = 1;
                                    dtData.Rows[rowI][i + 1] = drrAntiType.Length.ToString() + "/" + rPer + "/" + mPer + "/" + sPer;
                                }
                                #region Σ��ֵ
                                if (dtData.Rows[rowI][0].ToString() == criticalName)
                                {
                                    dtData.Rows[rowI][i + 1] = decCritical;
                                }
                                #endregion
                            }

                            if (rowFlg == 0)
                            {
                                DataRow newRow;
                                newRow = dtData.NewRow();
                                newRow[0] = Gss;
                                newRow[i + 1] = drrAntiType.Length.ToString() + "/" + rPer + "/" + mPer + "/" + sPer;
                                dtData.Rows.Add(newRow);
                            }
                        }
                    }
                }

                m_objViewer.dgvSensitiveRate.DataSource = dtData;
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }
        #endregion

        #region ϸ���ֲ����Ʊ���  v
        /// <summary>
        /// ϸ���ֲ����Ʊ���
        /// </summary>
        public void m_mthGetMicdistributiontend()
        {
            DataTable dtbResult;
            if (m_objViewer.dgv_mic.Rows.Count > 1)
            {
                strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                strTempName = null;
            }
            long lngRes = -1;
            string sampleId = string.Empty;
            string applicationStr = string.Empty;
            DataTable dtbMicAp = null;
            sampleId = getSampleId(m_objViewer.cbxSampleType.Text.Trim());
            lngRes = m_objManage.lngGetMicApplication(strTempName, dtDateFrom, dtDateTo, CriticalStr, DeptIdArr, out dtbMicAp);
            if (lngRes > 0 && dtbMicAp.Rows.Count > 0)
            {
                if (dtbMicAp != null && dtbMicAp.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbMicAp.Rows)
                    {
                        applicationStr += "'" + dr["application_id_chr"].ToString() + "',";
                    }
                    applicationStr = "(" + applicationStr.TrimEnd(',') + ")";
                }
            }
            lngRes = m_objManage.lngGetMicdistributionTend(strTempName, dtDateFrom, dtDateTo,applicationStr, DeptIdArr, sampleId, DisNo, Sex, TestMethod, out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                this.FillDWMicdistributionTend(dtbResult, dtDateFrom, dtDateTo);
            }
            else
            {
                MessageBox.Show("û��������ݡ�");
            }
        }
        /// <summary>
        ///��� �ֲ����Ʊ���
        /// </summary>
        /// <param name="dtbResult"></param>
        public void FillDWMicdistributionTend(DataTable dtbResult, DateTime dtDateFrom, DateTime dteDateTo)
        {
            try
            {
                int flgXJMC = 0;
                decimal totoal = 0;
                List<EntityAntiData> data = new List<EntityAntiData>();
                List<EnityAntiDisTend> disData = new List<EnityAntiDisTend>();
                m_objViewer.dgvMicdistributiontend.DataSource = null;
                DataTable dtData = new DataTable();
                dtData.Columns.Add("ϸ������\\�·�", Type.GetType("System.String"));
                int monthCuont = dteDateTo.Month - dtDateFrom.Month;

                if (monthCuont == 0)
                {
                    dtData.Columns.Add(dtDateFrom.AddMonths(0).ToString("yyyy-MM"), Type.GetType("System.String"));
                }
                else
                {
                    for (int mI = 0; mI < monthCuont + 1; mI++)
                    {
                        dtData.Columns.Add(dtDateFrom.AddMonths(mI).ToString("yyyy-MM"), Type.GetType("System.String"));
                    }
                }

                #region ϸ��
                foreach (DataRow dr in dtbResult.Rows)
                {
                    #region ����

                    string age = dr["age_chr"].ToString();
                    double ageDec = 0;

                    if (!string.IsNullOrEmpty(m_objViewer.txtAgeFrom.Text) && !string.IsNullOrEmpty(m_objViewer.txtAgeTo.Text))
                    {
                        double ageFrom = Convert.ToInt32(m_objViewer.txtAgeFrom.Text);
                        double ageTo = Convert.ToInt32(m_objViewer.txtAgeTo.Text);

                        if (age.Contains("��") || age.Contains("��"))
                        {
                            age = age.Replace("��", "").Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = 0.5;
                            else
                                continue;
                        }
                        if (age.Contains("��"))
                        {
                            age = age.Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = Convert.ToInt32(age);
                            else
                                continue;
                        }

                        if (ageDec < ageFrom || ageDec > ageTo)
                            continue;
                    }
                    #endregion

                    flgXJMC = 0;
                    string XJMC = dr["XJMC"].ToString().Trim();
                    if (!XJMC.Contains("����"))
                    {
                        if (XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                            continue;
                    }

                    XJMC = XJMC.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "").Replace("��", "(");

                    int idx = XJMC.IndexOf("(");
                    if (idx > 0)
                        XJMC = XJMC.Substring(0, idx);

                    #region �����ж�

                    if (GlFlgStr == "1")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + XJMC + "' and glys = '" + "+'");
                        if (drr != null && drr.Length > 0)
                        {

                        }
                        else
                            continue;
                    }

                    if (GlFlgStr == "0")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + XJMC + "' and glys = '" + "-'");
                        if (drr != null && drr.Length > 0)
                        {

                        }
                        else
                            continue;
                    }

                    #endregion

                    #region ��
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (XJMC == data[i].antiName)
                            {
                                data[i].anticCount++;
                                flgXJMC = 1;
                                totoal++;
                                break;
                            }
                        }
                    }
                    if (flgXJMC == 0)
                    {
                        EntityAntiData vo = new EntityAntiData();
                        vo.antiName = XJMC;
                        vo.anticCount = 1;
                        totoal++;
                        data.Add(vo);
                    }
                    #endregion

                    #region ��
                    int flgMonth = 0;
                    string month = string.Empty;
                    if (dr["HSSJ"] != DBNull.Value)
                        month = Convert.ToDateTime(dr["HSSJ"]).ToString("yyyy-MM");
                    else
                        continue;

                    if (disData != null && disData.Count > 0)
                    {
                        for (int i = 0; i < disData.Count; i++)
                        {
                            if (month == disData[i].month)
                            {
                                disData[i].antiCount++;
                                flgMonth = 1;
                                break;
                            }
                        }
                    }

                    if (flgMonth == 0)
                    {
                        EnityAntiDisTend vo = new EnityAntiDisTend();
                        vo.month = month;
                        vo.antiCount = 1;
                        disData.Add(vo);
                    }
                    #endregion
                }

                for (int i = 0; i < data.Count; i++)
                {
                    DataRow newRow;
                    newRow = dtData.NewRow();
                    newRow[0] = data[i].antiName + "(" + data[i].anticCount + ")";
                    dtData.Rows.Add(newRow);
                }

                #endregion

                #region ��ϸ
                string monthPer = string.Empty;
                decimal antiAmount = 0;
                decimal antinCount = 0;
                string amountPer = string.Empty;

                foreach (DataRow dr in dtbResult.Rows)
                {
                    #region ����

                    string age = dr["age_chr"].ToString();
                    double ageDec = 0;

                    if (!string.IsNullOrEmpty(m_objViewer.txtAgeFrom.Text) && !string.IsNullOrEmpty(m_objViewer.txtAgeTo.Text))
                    {
                        double ageFrom = Convert.ToInt32(m_objViewer.txtAgeFrom.Text);
                        double ageTo = Convert.ToInt32(m_objViewer.txtAgeTo.Text);

                        if (age.Contains("��") || age.Contains("��"))
                        {
                            age = age.Replace("��", "").Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = 0.5;
                            else
                                continue;
                        }
                        if (age.Contains("��"))
                        {
                            age = age.Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = Convert.ToInt32(age);
                            else
                                continue;
                        }

                        if (ageDec < ageFrom || ageDec > ageTo)
                            continue;
                    }
                    #endregion

                    flgXJMC = 0;
                    string month = string.Empty;
                    string XJMC = dr["XJMC"].ToString().Trim();
                    XJMC = XJMC.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "");
                    if (dr["HSSJ"] != DBNull.Value)
                        month = Convert.ToDateTime(dr["HSSJ"]).ToString("yyyy-MM");
                    else
                        continue;

                    if (!XJMC.Contains("����"))
                    {
                        if (XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                            continue;
                    }

                    XJMC = XJMC.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "").Replace("��", "(");

                    int idx = XJMC.IndexOf("(");
                    if (idx > 0)
                        XJMC = XJMC.Substring(0, idx);

                    #region �����ж�

                    if (GlFlgStr == "1")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + XJMC + "' and glys = '" + "+'");
                        if (drr != null && drr.Length > 0)
                        {

                        }
                        else
                            continue;
                    }

                    if (GlFlgStr == "0")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + XJMC + "' and glys = '" + "-'");
                        if (drr != null && drr.Length > 0)
                        {

                        }
                        else
                            continue;
                    }

                    #endregion


                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (dtData.Rows[i][0].ToString().Contains(XJMC))
                        {
                            for (int j = 0; j < dtData.Columns.Count; j++)
                            {
                                if (dtData.Columns[j].ToString() == month)
                                {
                                    if (dtData.Rows[i][j] != DBNull.Value)
                                    {
                                        //monthPer = dtData.Rows[i][j].ToString().Split('\\')[0];
                                        antiAmount = Convert.ToDecimal(dtData.Rows[i][j].ToString().Split('\\')[1].Trim('(').Trim(')')) + 1;
                                        amountPer = dtData.Rows[i][j].ToString().Split('\\')[2];
                                        //antinCount = Convert.ToDecimal(dtData.Rows[i][0].ToString().Split('(')[1].Trim(')'));

                                        for (int mI = 0; mI < disData.Count; mI++)
                                        {
                                            if (disData[mI].month == month)
                                                antinCount = disData[mI].antiCount;
                                        }
                                        amountPer = (antiAmount / antinCount).ToString("0.00%");
                                        dtData.Rows[i][j] = "*" + "\\" + antiAmount + "\\" + amountPer;
                                    }
                                    else
                                    {
                                        //antinCount = Convert.ToDecimal(dtData.Rows[i][0].ToString().Split('(')[1].Trim(')'));
                                        antinCount = 0;
                                        for (int mI = 0; mI < disData.Count; mI++)
                                        {
                                            if (disData[mI].month == month)
                                                antinCount = disData[mI].antiCount;
                                        }
                                        amountPer = (1 / antinCount).ToString("0.00%");
                                        dtData.Rows[i][j] = "*" + "\\" + 1 + "\\" + amountPer;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                m_objViewer.dgvMicdistributiontend.DataSource = dtData;
                m_objViewer.lblMicdistributiontend.Text = "������ϸ��: " + totoal + " ��";
                m_objViewer.lblMicdistributiontend.Visible = true;
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }

        #endregion

        #region ���������Ʊ���
        /// <summary>
        /// ���������Ʊ���
        /// </summary>
        public void m_mthGetSensitiveTend()
        {
            DataTable dtbResult = null; ;
            DataTable dtbMicAp = null;
            string applictionStr = string.Empty;
            long lngRes = -1;

            if (m_objViewer.dgv_mic.Rows.Count < 1)
            {
                MessageBox.Show("��ѡ��ϸ����");
                return;
            }
            List<EntitySenRateTendCritical> cTendData = new List<EntitySenRateTendCritical>();
            sampleId = getSampleId(m_objViewer.cbxSampleType.Text.Trim());
            strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();
            lngRes = m_objManage.lngGetMicApplication(strTempName, dtDateFrom, dtDateTo, CriticalStr, DeptIdArr, out dtbMicAp);
            if (lngRes > 0 && dtbMicAp.Rows.Count > 0)
            {
                if (dtbMicAp != null && dtbMicAp.Rows.Count > 0)
                {
                    string month = string.Empty;
                    int flgM = 0;

                    foreach (DataRow dr in dtbMicAp.Rows)
                    {
                        flgM = 0;
                        applictionStr += "'" + dr["application_id_chr"].ToString() + "',";

                        if (dr["HSSJ"] != DBNull.Value && dr["criticalResult"] != DBNull.Value)
                        {
                            month = Convert.ToDateTime(dr["HSSJ"]).ToString("yyyy-MM");

                            for (int i = 0; i < cTendData.Count; i++)
                            {
                                if (month == cTendData[i].month)
                                {
                                    cTendData[i].month = month;
                                    cTendData[i].criticalName = dr["criticalResult"].ToString().Trim();
                                    cTendData[i].criticalCount++;
                                    flgM = 1;
                                }
                            }

                            if (flgM == 0)
                            {
                                EntitySenRateTendCritical vo = new EntitySenRateTendCritical();
                                vo.month = month;
                                vo.criticalName = dr["criticalResult"].ToString().Trim();
                                vo.criticalCount = 1;
                                cTendData.Add(vo);
                            }
                        }
                    }
                    applictionStr = "(" + applictionStr.TrimEnd(',') + ")";
                }
            }
            if (!string.IsNullOrEmpty(applictionStr))
                lngRes = m_objManage.lngGetSensitiveTend(applictionStr, sampleId, DisNo, Sex, strTempAnti, out dtbResult);
            else
                MessageBox.Show("û��������ݡ�");
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                this.FillDWSensitiveTend(dtbResult, strTempName, dtDateFrom, dtDateTo, cTendData);
            }
            else
            {
                MessageBox.Show("û��������ݡ�");
            }
        }

        /// <summary>
        /// ��� ���������Ʊ���
        /// </summary>
        /// <param name="dtbResult"></param>
        public void FillDWSensitiveTend(DataTable dtbResult, string strTempName, DateTime dtDateFrom, DateTime dteDateTo, List<EntitySenRateTendCritical> cTendData)
        {
            try
            {
                int flgS = 0;
                decimal xjzs = 0;
                List<EntitySenRate> data = new List<EntitySenRate>();
                m_objViewer.dgvMicdistributiontend.DataSource = null;
                DataTable dtData = new DataTable();
                dtData.Columns.Add("������\\�·�", Type.GetType("System.String"));
                int monthCuont = dteDateTo.Month - dtDateFrom.Month;
                m_objViewer.dgvSensitiveTend.DataSource = null;

                if (monthCuont == 0)
                {
                    dtData.Columns.Add(dtDateFrom.AddMonths(0).ToString("yyyy-MM"), Type.GetType("System.String"));
                }
                else
                {
                    for (int mI = 0; mI < monthCuont + 1; mI++)
                    {
                        dtData.Columns.Add(dtDateFrom.AddMonths(mI).ToString("yyyy-MM"), Type.GetType("System.String"));
                    }
                }

                #region Σ��ֵ
                decimal cCount = 0;
                string cName = string.Empty;
                if (cTendData.Count > 0)
                {
                    for (int cI = 0; cI < cTendData.Count; cI++)
                    {
                        cCount += cTendData[cI].criticalCount;
                        cName = cTendData[cI].criticalName;
                    }

                    EntitySenRate vo = new EntitySenRate();
                    vo.antiName = cName;
                    data.Add(vo);
                }
                #endregion

                #region ������
                if (dtbResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        #region ����

                        string age = dr["age_chr"].ToString();
                        double ageDec = 0;

                        if (!string.IsNullOrEmpty(m_objViewer.txtAgeFrom.Text) && !string.IsNullOrEmpty(m_objViewer.txtAgeTo.Text))
                        {
                            double ageFrom = Convert.ToInt32(m_objViewer.txtAgeFrom.Text);
                            double ageTo = Convert.ToInt32(m_objViewer.txtAgeTo.Text);

                            if (age.Contains("��") || age.Contains("��"))
                            {
                                age = age.Replace("��", "").Replace("��", "").Trim();
                                if (!string.IsNullOrEmpty(age))
                                    ageDec = 0.5;
                                else
                                    continue;
                            }
                            if (age.Contains("��"))
                            {
                                age = age.Replace("��", "").Trim();
                                if (!string.IsNullOrEmpty(age))
                                    ageDec = Convert.ToInt32(age);
                                else
                                    continue;
                            }

                            if (ageDec < ageFrom || ageDec > ageTo)
                                continue;
                        }
                        #endregion

                        string XJMC = dr["RESULT"].ToString().Trim();
                        string antiName = dr["itemname"].ToString();

                        if (!XJMC.Contains("����"))
                        {
                            if (antiName.Contains("ϸ������") || XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                                continue;
                        }
                        if (XJMC.Contains(strTempName))
                        {
                            xjzs++;
                            continue;
                        }

                        string result = dr["RESULT"].ToString().Trim();
                        flgS = 0;

                        if (data != null && data.Count > 0)
                        {
                            for (int i = 0; i < data.Count; i++)
                            {
                                if (antiName == data[i].antiName)
                                {
                                    data[i].antiCount++;
                                    if (result == "����")
                                        data[i].antiSenCount++;
                                    if (data[i].antiSenCount > 0)
                                        data[i].antiSenRate = (data[i].antiSenCount / data[i].antiCount).ToString("0.00%");
                                    flgS = 1;
                                    break;
                                }
                            }
                        }

                        if (flgS == 0)
                        {
                            EntitySenRate vo = new EntitySenRate();
                            vo.antiCount = 1;
                            vo.antiName = antiName;
                            if (result == "����")
                            {
                                vo.antiSenCount++;
                                if (vo.antiSenCount > 0)
                                    vo.antiSenRate = (vo.antiSenCount / vo.antiCount).ToString("0.00%");
                            }
                            data.Add(vo);
                        }
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        DataRow newRow;
                        newRow = dtData.NewRow();
                        newRow[0] = data[i].antiName;
                        dtData.Rows.Add(newRow);
                    }
                }
                #endregion

                #region Σ��ֵ

                if (cTendData.Count > 0)
                {
                    for (int cI = 0; cI < cTendData.Count; cI++)
                    {
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (dtData.Rows[i][0].ToString() == cTendData[cI].criticalName)
                            {
                                for (int j = 0; j < dtData.Columns.Count; j++)
                                {
                                    if (dtData.Columns[j].ToString() == cTendData[cI].month)
                                    {
                                        dtData.Rows[i][j] = cTendData[cI].criticalCount;
                                    }
                                }
                            }
                        }
                    }
                }
                
                #endregion

                #region ��ϸ
                string monthPer = string.Empty;
                decimal senAmount = 0;
                decimal antinCount = 0;
                string senPer = string.Empty;

                foreach (DataRow dr in dtbResult.Rows)
                {
                    string antiName = dr["itemname"].ToString().Trim();
                    string result = dr["RESULT"].ToString().Trim();
                    string month = Convert.ToDateTime(dr["HSSJ"]).ToString("yyyy-MM");

                    #region ����

                    string age = dr["age_chr"].ToString();
                    double ageDec = 0;

                    if (!string.IsNullOrEmpty(m_objViewer.txtAgeFrom.Text) && !string.IsNullOrEmpty(m_objViewer.txtAgeTo.Text))
                    {
                        double ageFrom = Convert.ToInt32(m_objViewer.txtAgeFrom.Text);
                        double ageTo = Convert.ToInt32(m_objViewer.txtAgeTo.Text);

                        if (age.Contains("��") || age.Contains("��"))
                        {
                            age = age.Replace("��", "").Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = 0.5;
                            else
                                continue;
                        }
                        if (age.Contains("��"))
                        {
                            age = age.Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = Convert.ToInt32(age);
                            else
                                continue;
                        }

                        if (ageDec < ageFrom || ageDec > ageTo)
                            continue;
                    }

                    #endregion

                    if (antiName == "ϸ������")
                        continue;

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (dtData.Rows[i][0].ToString() == antiName)
                        {
                            for (int j = 0; j < dtData.Columns.Count; j++)
                            {
                                if (dtData.Columns[j].ToString() == month)
                                {
                                    if (dtData.Rows[i][j] != DBNull.Value)
                                    {
                                        antinCount = Convert.ToDecimal(dtData.Rows[i][j].ToString().Split('\\')[0]) + 1;
                                        if (result == "����")
                                            senAmount = Convert.ToDecimal(dtData.Rows[i][j].ToString().Split('\\')[1]) + 1;
                                        else
                                            senAmount = Convert.ToDecimal(dtData.Rows[i][j].ToString().Split('\\')[1]);

                                        if (senAmount > 0)
                                            senPer = (senAmount / antinCount).ToString("0.00%");
                                        dtData.Rows[i][j] = antinCount + "\\" + senAmount + "\\" + senPer;
                                    }
                                    else
                                    {
                                        senAmount = 0;
                                        senPer = string.Empty;
                                        if (result == "����")
                                            senAmount = 1;
                                        if (senAmount > 0)
                                            senPer = "100%";
                                        else
                                            senPer = "*";
                                        dtData.Rows[i][j] = 1 + "\\" + senAmount + "\\" + senPer;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                m_objViewer.dgvSensitiveTend.DataSource = dtData;
                m_objViewer.lblSensitiveTend.Text = strTempName + ": " + xjzs + " ��";
                m_objViewer.lblSensitiveTend.Visible = true;
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }
        #endregion

        #region �ۼ�MIC����  v
        /// <summary>
        /// �ۼ�MIC����
        /// </summary>
        public void m_mthGetMicCumulative()
        {
            DataTable dtbResult = null;
            DataTable dtbMicAp = null;
            string applicationStr = string.Empty;
            long lngRes = -1;
            if (m_objViewer.dgv_mic.Rows.Count < 1)
            {
                MessageBox.Show("��ѡ��ϸ����");
                return;
            }
            sampleId = getSampleId(m_objViewer.cbxSampleType.Text.Trim());
            strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();
            lngRes = m_objManage.lngGetMicApplication(strTempName, dtDateFrom, dtDateTo, CriticalStr, DeptIdArr, out dtbMicAp);
            if (lngRes > 0 && dtbMicAp.Rows.Count > 0)
            {
                if (dtbMicAp != null && dtbMicAp.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbMicAp.Rows)
                    {
                        applicationStr += "'" + dr["application_id_chr"].ToString() + "',";
                    }
                    applicationStr = "(" + applicationStr.TrimEnd(',') + ")";
                }
            }
            if (!string.IsNullOrEmpty(applicationStr))
                lngRes = m_objManage.lngGetMicCumulative(applicationStr, sampleId, DisNo, Sex, TestMethod, strTempAnti, out dtbResult);
            else
                MessageBox.Show("û��������ݡ�");
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                this.FillDWMicCumulative(dtbResult, strTempName);
            }
            else
            {
                MessageBox.Show("û��������ݡ�");
            }
        }

        public void FillDWMicCumulative(DataTable dtbResult, string strTempName)
        {
            try
            {
                decimal xjzs = 0;
                int flg = 0;
                List<EntityMicCumulative> data = new List<EntityMicCumulative>();
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        #region ����

                        string age = dr["age_chr"].ToString();
                        double ageDec = 0;

                        if (!string.IsNullOrEmpty(m_objViewer.txtAgeFrom.Text) && !string.IsNullOrEmpty(m_objViewer.txtAgeTo.Text))
                        {
                            double ageFrom = Convert.ToInt32(m_objViewer.txtAgeFrom.Text);
                            double ageTo = Convert.ToInt32(m_objViewer.txtAgeTo.Text);

                            if (age.Contains("��") || age.Contains("��"))
                            {
                                age = age.Replace("��", "").Replace("��", "").Trim();
                                if (!string.IsNullOrEmpty(age))
                                    ageDec = 0.5;
                                else
                                    continue;
                            }
                            if (age.Contains("��"))
                            {
                                age = age.Replace("��", "").Trim();
                                if (!string.IsNullOrEmpty(age))
                                    ageDec = Convert.ToInt32(age);
                                else
                                    continue;
                            }

                            if (ageDec < ageFrom || ageDec > ageTo)
                                continue;
                        }
                        #endregion

                        string XJMC = dr["RESULT"].ToString().Trim();

                        if (!XJMC.Contains("����"))
                        {
                            if (XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                                continue;
                        }
                        if (XJMC.Contains(strTempName))
                        {
                            xjzs++;
                            continue;
                        }

                        string antiName = dr["ITEMNAME"].ToString();
                        string micResult = dr["REFRANGE_VCHR"].ToString().Trim().Replace("��", ">").Replace("��", "<").Trim();
                        if (string.IsNullOrEmpty(strTempName))
                            continue;

                        flg = 0;
                        if (XJMC.Contains("����") || XJMC.Contains("��ҩ") || XJMC.Contains("�н�"))
                        {
                            if (data != null && data.Count > 0)
                            {
                                for (int i = 0; i < data.Count; i++)
                                {
                                    if (antiName == data[i].antiname)
                                    {
                                        if (micResult == "��0.25")
                                            data[i].perCount_1++;
                                        else if (micResult == "��0.5")
                                            data[i].perCount_2++;
                                        else if (micResult == "��1")
                                            data[i].perCount_3++;
                                        else if (micResult == "��2")
                                            data[i].perCount_4++;
                                        else if (micResult == "��4")
                                            data[i].perCount_5++;
                                        else if (micResult == "��8")
                                            data[i].perCount_6++;
                                        else if (micResult == "��16")
                                            data[i].perCount_7++;
                                        else if (micResult == "��32")
                                            data[i].perCount_8++;
                                        else if (micResult == "��64")
                                            data[i].perCount_9++;
                                        else if (micResult == "��128")
                                            data[i].perCount_10++;
                                        else if (micResult == "��256")
                                            data[i].perCount_11++;
                                        else if (micResult == ">256")
                                            data[i].perCount_12++;
                                        else if (micResult == ">0.25")
                                            data[i].perCount_2++;
                                        else if (micResult == ">0.5")
                                            data[i].perCount_3++;
                                        else if (micResult == ">1")
                                            data[i].perCount_4++;
                                        else if (micResult == ">2")
                                            data[i].perCount_5++;
                                        else if (micResult == ">4")
                                            data[i].perCount_6++;
                                        else if (micResult == ">8")
                                            data[i].perCount_7++;
                                        else if (micResult == ">16")
                                            data[i].perCount_8++;
                                        else if (micResult == ">32")
                                            data[i].perCount_9++;
                                        else if (micResult == ">64")
                                            data[i].perCount_10++;
                                        else if (micResult == ">128")
                                            data[i].perCount_11++;
                                        data[i].total++;
                                        flg = 1;
                                        break;
                                    }
                                }
                            }
                            if (flg == 0)
                            {
                                EntityMicCumulative vo = new EntityMicCumulative();
                                vo.antiname = antiName;
                                if (micResult == "��0.25")
                                    vo.perCount_1++;
                                else if (micResult == "��0.5")
                                    vo.perCount_2++;
                                else if (micResult == "��1")
                                    vo.perCount_3++;
                                else if (micResult == "��2")
                                    vo.perCount_4++;
                                else if (micResult == "��4")
                                    vo.perCount_5++;
                                else if (micResult == "��8")
                                    vo.perCount_6++;
                                else if (micResult == "��16")
                                    vo.perCount_7++;
                                else if (micResult == "��32")
                                    vo.perCount_8++;
                                else if (micResult == "��64")
                                    vo.perCount_9++;
                                else if (micResult == "��128")
                                    vo.perCount_10++;
                                else if (micResult == "��256")
                                    vo.perCount_11++;
                                else if (micResult == ">256")
                                    vo.perCount_12++;
                                else if (micResult == ">0.25")
                                    vo.perCount_2++;
                                else if (micResult == ">0.5")
                                    vo.perCount_3++;
                                else if (micResult == ">1")
                                    vo.perCount_4++;
                                else if (micResult == ">2")
                                    vo.perCount_5++;
                                else if (micResult == ">4")
                                    vo.perCount_6++;
                                else if (micResult == ">8")
                                    vo.perCount_7++;
                                else if (micResult == ">16")
                                    vo.perCount_8++;
                                else if (micResult == ">32")
                                    vo.perCount_9++;
                                else if (micResult == ">64")
                                    vo.perCount_10++;
                                else if (micResult == ">128")
                                    vo.perCount_11++;
                                vo.total = 1;
                                data.Add(vo);
                            }
                        }
                    }
                }
                for (int intI = 0; intI < data.Count; intI++)
                {
                    if (data[intI].perCount_1 > 0)
                    {
                        data[intI].per_1 = (data[intI].perCount_1 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_1))
                        {
                            data[intI].per_2 = "100.00%";
                            data[intI].per_3 = "100.00%";
                            data[intI].per_4 = "100.00%";
                            data[intI].per_5 = "100.00%";
                            data[intI].per_6 = "100.00%";
                            data[intI].per_7 = "100.00%";
                            data[intI].per_8 = "100.00%";
                            data[intI].per_9 = "100.00%";
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";
                        }
                    }

                    if (data[intI].perCount_2 > 0)
                    {
                        data[intI].per_2 = (data[intI].perCount_2 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_2))
                        {
                            data[intI].per_3 = "100.00%";
                            data[intI].per_4 = "100.00%";
                            data[intI].per_5 = "100.00%";
                            data[intI].per_6 = "100.00%";
                            data[intI].per_7 = "100.00%";
                            data[intI].per_8 = "100.00%";
                            data[intI].per_9 = "100.00%";
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";
                        }
                    }

                    if (data[intI].perCount_3 > 0)
                    {
                        data[intI].per_3 = (data[intI].perCount_3 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_3))
                        {
                            data[intI].per_4 = "100.00%";
                            data[intI].per_5 = "100.00%";
                            data[intI].per_6 = "100.00%";
                            data[intI].per_7 = "100.00%";
                            data[intI].per_8 = "100.00%";
                            data[intI].per_9 = "100.00%";
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";
                        }
                    }

                    if (data[intI].perCount_4 > 0)
                    {
                        data[intI].per_4 = (data[intI].perCount_4 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_4))
                        {
                            data[intI].per_5 = "100.00%";
                            data[intI].per_6 = "100.00%";
                            data[intI].per_7 = "100.00%";
                            data[intI].per_8 = "100.00%";
                            data[intI].per_9 = "100.00%";
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";
                        }
                    }

                    if (data[intI].perCount_5 > 0)
                    {
                        data[intI].per_5 = (data[intI].perCount_5 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_5))
                        {
                            data[intI].per_6 = "100.00%";
                            data[intI].per_7 = "100.00%";
                            data[intI].per_8 = "100.00%";
                            data[intI].per_9 = "100.00%";
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";
                        }
                    }

                    if (data[intI].perCount_6 > 0)
                    {
                        data[intI].per_6 = (data[intI].perCount_6 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_6))
                        {
                            data[intI].per_7 = "100.00%";
                            data[intI].per_8 = "100.00%";
                            data[intI].per_9 = "100.00%";
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";
                        }
                    }

                    if (data[intI].perCount_7 > 0)
                    {
                        data[intI].per_7 = (data[intI].perCount_7 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_7))
                        {
                            data[intI].per_8 = "100.00%";
                            data[intI].per_9 = "100.00%";
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";

                        }
                    }

                    if (data[intI].perCount_8 > 0)
                    {
                        data[intI].per_8 = (data[intI].perCount_8 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_8))
                        {
                            data[intI].per_9 = "100.00%";
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";
                        }
                    }
                    if (data[intI].perCount_9 > 0)
                    {
                        data[intI].per_9 = (data[intI].perCount_9 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_9))
                        {
                            data[intI].per_10 = "100.00%";
                            data[intI].per_11 = "100.00%";
                        }
                    }

                    if (data[intI].perCount_10 > 0)
                    {
                        data[intI].per_10 = (data[intI].perCount_10 / data[intI].total).ToString("0.00%");
                        if (!string.IsNullOrEmpty(data[intI].per_10))
                        {
                            data[intI].per_11 = "100.00%";
                        }
                    }

                    if (data[intI].perCount_11 > 0)
                    {
                        data[intI].per_11 = (data[intI].perCount_11 / data[intI].total).ToString("0.00%");
                    }

                    if (data[intI].perCount_12 > 0)
                    {
                        data[intI].per_12 = (data[intI].perCount_12 / data[intI].total).ToString("0.00%");
                    }
                }

                m_objViewer.dgvMicCumulative.DataSource = data;
                m_objViewer.lblMicCumulative.Text = strTempName + ":(" + xjzs + ")";
                m_objViewer.lblMicCumulative.Visible = true;

            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }
        #endregion

        #region �����ʱ���ͳ�Ʊ���
        /// <summary>
        /// �����ʱ���ͳ�Ʊ���
        /// </summary>
        public void m_mthGetSensitiveRate()
        {
            DataTable dtbResult = null;
            DataTable dtbMicAp = null;
            string strTempName = string.Empty;
            string applicationStr = string.Empty;
            long lngRes = -1;
            List<EntitySenRate> data = new List<EntitySenRate>();

            if (m_objViewer.dgv_mic.Rows.Count < 1)
            {
                MessageBox.Show("��ѡ��ϸ����");
                return;
            }
            sampleId = getSampleId(m_objViewer.cbxSampleType.Text.Trim());
            strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();

            lngRes = m_objManage.lngGetMicApplication(strTempName, dtDateFrom, dtDateTo, CriticalStr, DeptIdArr, out dtbMicAp);
            if (lngRes > 0 && dtbMicAp.Rows.Count > 0)
            {
                if (dtbMicAp != null && dtbMicAp.Rows.Count > 0)
                {
                    int flgCrit = 0;

                    foreach (DataRow dr in dtbMicAp.Rows)
                    {
                        applicationStr += "'" + dr["application_id_chr"].ToString() + "',";
                        string criticalResult = dr["criticalResult"].ToString();
                        flgCrit = 0;

                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].antiName == criticalResult)
                            {
                                data[i].antiCount++;
                                flgCrit = 1;
                                break;
                            }
                        }

                        if (flgCrit == 0 && !string.IsNullOrEmpty(criticalResult))
                        {
                            EntitySenRate vo = new EntitySenRate();
                            vo.antiName = criticalResult;
                            vo.antiCount = 1;
                            data.Add(vo);
                        }
                    }
                    applicationStr = "(" + applicationStr.TrimEnd(',') + ")";
                }
            }

            if (!string.IsNullOrEmpty(applicationStr))
                lngRes = m_objManage.lngGetMicSensitive(applicationStr, sampleId, DisNo, Sex, strTempName, strTempAnti, out dtbResult);
            else
                MessageBox.Show("û��������ݡ�");

            if (lngRes > 0 && dtbResult != null)
            {
                this.FillDWSensitiveRate(dtbResult, strTempName, data);
            }
            else
            {
                MessageBox.Show("û��������ݡ�");
            }
        }

        /// <summary>
        /// ���DW  �����ʱ���ͳ�Ʊ���
        /// </summary>
        /// <param name="dtbResult"></param>
        public void FillDWSensitiveRate(DataTable dtbResult, string strTempName, List<EntitySenRate> data)
        {
            try
            {
                int flgS = 0;
                //List<EntitySenRate> data = new List<EntitySenRate>();
                decimal xjzs = 0;
                #region ϸ����

                foreach (DataRow dr in dtbResult.Rows)
                {
                    #region ����

                    string age = dr["age_chr"].ToString();
                    double ageDec = 0;

                    if (!string.IsNullOrEmpty(m_objViewer.txtAgeFrom.Text) && !string.IsNullOrEmpty(m_objViewer.txtAgeTo.Text))
                    {
                        double ageFrom = Convert.ToInt32(m_objViewer.txtAgeFrom.Text);
                        double ageTo = Convert.ToInt32(m_objViewer.txtAgeTo.Text);

                        if (age.Contains("��") || age.Contains("��"))
                        {
                            age = age.Replace("��", "").Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = 0.5;
                            else
                                continue;
                        }
                        if (age.Contains("��"))
                        {
                            age = age.Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = Convert.ToInt32(age);
                            else
                                continue;
                        }

                        if (ageDec < ageFrom || ageDec > ageTo)
                            continue;
                    }
                    #endregion

                    flgS = 0;
                    string XJMC = dr["RESULT"].ToString().Trim();

                    if (!XJMC.Contains("����"))
                    {
                        if (XJMC.Contains("ϸ������") || XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                            continue;
                    }
                    if (XJMC.Contains(strTempName))
                    {
                        xjzs++;
                        continue;
                    }

                    string antiName = dr["itemname"].ToString();
                    string result = dr["RESULT"].ToString().Trim();
                    if (string.IsNullOrEmpty(antiName) || antiName.Contains("ϸ������"))
                        continue;

                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (antiName == data[i].antiName)
                            {
                                data[i].antiCount++;
                                if (result == "����")
                                    data[i].antiSenCount++;
                                if (data[i].antiSenCount > 0)
                                    data[i].antiSenRate = (data[i].antiSenCount / data[i].antiCount).ToString("0.00%");
                                flgS = 1;
                                break;
                            }
                        }
                    }

                    if (flgS == 0)
                    {
                        EntitySenRate vo = new EntitySenRate();
                        vo.antiCount = 1;
                        vo.antiName = antiName;
                        if (result == "����")
                        {
                            vo.antiSenCount++;
                            if (vo.antiSenCount > 0)
                                vo.antiSenRate = (vo.antiSenCount / vo.antiCount).ToString("0.00%");
                        }
                        data.Add(vo);
                    }
                }

                #endregion

                m_objViewer.dgvSenRate.DataSource = data;
                m_objViewer.lblSensitiveRate.Text = strTempName + ":(" + xjzs + ")";
                m_objViewer.lblSensitiveRate.Visible = true;

            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }

        #endregion

        #region ϸ����ϸ v
        /// <summary> 
        /// ϸ����ϸ
        /// </summary>
        public void m_mthGetAntiDetail()
        {
            DataTable dtbResult = null;
            DataTable dtbItem;
            if (m_objViewer.dgv_mic.Rows.Count > 1)
            {
                strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                strTempName = null;
            }
            long lngRes = -1;
            sampleId = getSampleId(this.m_objViewer.cbxSampleType.Text.Trim());
            m_objManage.lngGetAntiCheckItem(dtDateFrom, dtDateTo, out dtbItem);
            lngRes = m_objManage.lngGetAntiDetail(strTempName, dtDateFrom, dtDateTo, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
            if (lngRes > 0 && dtbResult != null)
            {
                this.FillDetail(dtbResult, dtbItem);
            }
            else
            {
                MessageBox.Show("û�в鵽û�����ݡ�");
            }
        }

        /// <summary>
        /// ���DW ΢������ϸ
        /// </summary>
        /// <param name="intall"></param>
        /// <param name="dtRsult"></param>
        public void FillDetail(DataTable dtbResult,DataTable dtbItem )
        {
            try
            {
                List<EntityDetail> data = new List<EntityDetail>();
                DataTable dtData = new DataTable();

                #region
                if (dtbItem != null && dtbItem.Rows.Count > 0)
                {

                    dtData.Columns.Add("����", Type.GetType("System.String"));
                    dtData.Columns.Add("�Ա�", Type.GetType("System.String"));
                    dtData.Columns.Add("����", Type.GetType("System.String"));
                    dtData.Columns.Add("סԺ/�����", Type.GetType("System.String"));
                    dtData.Columns.Add("����", Type.GetType("System.String"));
                    dtData.Columns.Add("������", Type.GetType("System.String"));
                    dtData.Columns.Add("�걾����", Type.GetType("System.String"));
                    dtData.Columns.Add("�������", Type.GetType("System.String"));


                    foreach (DataRow dr in dtbItem.Rows)
                    {
                        dtData.Columns.Add(dr["itemname"].ToString().Trim(), Type.GetType("System.String"));
                    }
                }
                #endregion
                string application = string.Empty;
                string lastAppplication = string.Empty;

                foreach (DataRow dr in dtbResult.Rows)
                {
                    #region ����

                    string age = dr["AGE"].ToString();
                    double ageDec = 0.5;

                    if (!string.IsNullOrEmpty(m_objViewer.txtAgeFrom.Text) && !string.IsNullOrEmpty(m_objViewer.txtAgeTo.Text))
                    {
                        double ageFrom = Convert.ToInt32(m_objViewer.txtAgeFrom.Text);
                        double ageTo = Convert.ToInt32(m_objViewer.txtAgeTo.Text);

                        if (age.Contains("��") || age.Contains("��"))
                        {
                            age = age.Replace("��", "").Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = 0.5;
                            else
                                continue;
                        }
                        if (age.Contains("��"))
                        {
                            age = age.Replace("��", "").Trim();
                            if (!string.IsNullOrEmpty(age))
                                ageDec = Convert.ToInt32(age);
                            else
                                continue;
                        }

                        if (ageDec < ageFrom || ageDec > ageTo)
                            continue;
                    }

                    #endregion

                    application = dr["application_id_chr"].ToString();
                    string XJMC = dr["RESULT"].ToString().Trim();
                    string result = dr["RESULT"].ToString().Trim();
                    string itemname = dr["CHECKITEM"].ToString().Trim();
                    if (!XJMC.Contains("����"))
                    {
                        if ((XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��")))
                            continue;
                    }

                    if (application != lastAppplication)
                    {
                        lastAppplication = application;

                        DataRow newRow;
                        newRow = dtData.NewRow();
                        newRow[0] = dr["HZXM"].ToString().Trim();
                        newRow[1] = dr["SEX"].ToString().Trim();

                        newRow[2] = dr["AGE"].ToString().Trim();

                        if (!string.IsNullOrEmpty(dr["ZYH"].ToString()))
                            newRow[3] = dr["ZYH"].ToString().Trim();
                        else
                            newRow[3] = dr["KH"].ToString().Trim();

                        newRow[4] = dr["SJKS"].ToString().Trim();
                        newRow[5] = dr["BGBH"].ToString().Trim();
                        newRow[6] = dr["BBLX"].ToString().Trim();

                        if (itemname.Trim() == "�������")
                            newRow[7] = result;

                        for (int j = 0; j < dtData.Columns.Count; j++)
                        {
                            if (dtData.Columns[j].ToString() == itemname)
                            {
                                newRow[j] = result;
                            }
                        }

                        dtData.Rows.Add(newRow);
                    }
                    else
                    {
                        int rowIdx = dtData.Rows.Count - 1;

                        if (itemname.Trim() == "�������")
                            dtData.Rows[rowIdx][7] = result;

                        for (int j = 0; j < dtData.Columns.Count; j++)
                        {
                            if (dtData.Columns[j].ToString() == itemname)
                            {
                                dtData.Rows[rowIdx][j] = result;
                            }
                        }
                    }
                }

                m_objViewer.dgvDetail.DataSource = dtData;
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }
        #endregion

        #region ϸ���ֲ�������
        /// <summary>
        /// ϸ���ֲ�������
        /// </summary>
        public void m_mthGetDgvSampleType()
        {
            DataTable dtbMicName;

            if (m_objViewer.dgv_mic.Rows.Count > 1)
            {
                strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                strTempName = null;
            }
            long lngRes = -1;
            string sampleId = string.Empty;
            sampleId = getSampleId(m_objViewer.cbxSampleType.Text.Trim());
            lngRes = m_objManage.lngGetAnti(strTempName, dtDateFrom, dtDateTo, DeptIdArr, out dtbMicName);

            if (lngRes > 0 && dtbMicName.Rows.Count > 0)
            {
                this.FillDgvSampleType(dtbMicName);
            }
            else
            {
                MessageBox.Show("û��������ݡ�");
            }
        }
        /// <summary>
        ///��� �ֲ����Ʊ���
        /// </summary>
        /// <param name="dtbResult"></param>
        public void FillDgvSampleType(DataTable dtbMicName)
        {
            try
            {
                long lngRes = -1;
                DataTable dtbResult = null;
                List<EntityAntiData> data = new List<EntityAntiData>();
                List<EnityAntiDisTend> disData = new List<EnityAntiDisTend>();
                m_objViewer.dgvMicdistributiontend.DataSource = null;
                DataTable dtData = new DataTable();
                dtData.Columns.Add(@"�걾����\ϸ��", Type.GetType("System.String"));

                #region ϸ��  ��
                int flgXj = 0;
                string applicationStr = string.Empty;
                foreach (DataRow dr in dtbMicName.Rows)
                {
                    flgXj = 0;
                    string XJMC = dr["XJMC"].ToString().Trim();
                    if (!XJMC.Contains("����"))
                    {
                        if (XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                            continue;
                    }

                    XJMC = XJMC.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "").Replace("��", "(");

                    int idx = XJMC.IndexOf("(");
                    if (idx > 0)
                        XJMC = XJMC.Substring(0, idx);

                    for (int j = 0; j < dtData.Columns.Count; j++)
                    {
                        if (dtData.Columns[j].ToString() == XJMC)
                        {
                            flgXj = 1;
                            break;
                        }
                    }

                    if (flgXj == 0)
                    {
                        dtData.Columns.Add(XJMC, Type.GetType("System.String"));
                    }

                    applicationStr += "'" +dr["application_id_chr"] + "',";
                }
                #endregion

                if (!string.IsNullOrEmpty(applicationStr))
                    applicationStr = "(" + applicationStr.TrimEnd(',') + ")";
                else
                    return;
                lngRes = m_objManage.lngGetAntiSample(strTempName, applicationStr, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
                string sampleTypeLast = string.Empty;

                foreach (DataRow dr in dtbResult.Rows)
                {
                    string sampleType = dr["BBLX"].ToString().Trim();
                    string result = dr["RESULT"].ToString().Trim();
                    if (!result.Contains("����"))
                    {
                        if ((result.Contains("��") || result.Contains("����") || result.Contains("?") || result.Contains("��") || result.Contains("��")))
                            continue;
                    }

                    result = result.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "").Replace("��", "(");

                    int idx = result.IndexOf("(");
                    if (idx > 0)
                        result = result.Substring(0, idx);


                    #region �����ж�

                    if (GlFlgStr == "1")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + result + "' and glys = '" + "+'");
                        if (drr != null && drr.Length > 0)
                        {

                        }
                        else
                            continue;
                    }

                    if (GlFlgStr == "0")
                    {
                        DataRow[] drr = dtGlAnti.Select("xjmc = '" + result + "' and glys = '" + "-'");
                        if (drr != null && drr.Length > 0)
                        {

                        }
                        else
                            continue;
                    }

                    #endregion

                    if (sampleType != sampleTypeLast)
                    {
                        sampleTypeLast = sampleType;

                        DataRow newRow;
                        newRow = dtData.NewRow();
                        newRow[0] = sampleType;

                        for (int j = 0; j < dtData.Columns.Count; j++)
                        {
                            if (dtData.Columns[j].ToString() == result)
                            {
                                newRow[j] = 1;
                            }
                        }

                        dtData.Rows.Add(newRow);
                    }
                    else
                    {
                        int rowIdx = dtData.Rows.Count - 1;

                        for (int j = 0; j < dtData.Columns.Count; j++)
                        {
                            if (dtData.Columns[j].ToString() == result)
                            {
                                if (dtData.Rows[rowIdx][j] != DBNull.Value)
                                    dtData.Rows[rowIdx][j] = Convert.ToInt32(dtData.Rows[rowIdx][j]) + 1;
                                else
                                    dtData.Rows[rowIdx][j] = 1;
                            }
                        }
                    }
                }

                m_objViewer.dgvSampleType.DataSource = dtData;
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }
        #endregion

        #region ϸ���ֲ�������
        /// <summary>
        /// ϸ���ֲ�������
        /// </summary>
        public void m_mthGetDgvAntiByDept()
        {
            DataTable dtbMicName;

            if (m_objViewer.dgv_mic.Rows.Count > 1)
            {
                strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                strTempName = null;
            }
            long lngRes = -1;
            
            lngRes = m_objManage.lngGetAnti(strTempName, dtDateFrom, dtDateTo, DeptIdArr, out dtbMicName);

            if (lngRes > 0 && dtbMicName.Rows.Count > 0)
            {
                this.FillDgvAntiByDept(dtbMicName);
            }
            else
            {
                MessageBox.Show("û��������ݡ�");
            }
        }
        /// <summary>
        ///��� �ֲ����Ʊ���
        /// </summary>
        /// <param name="dtbResult"></param>
        public void FillDgvAntiByDept( DataTable dtbMicName)
        {
            try
            {
                DataTable dtbResult = null;
                long lngRes = -1;
                List<EntityAntiData> data = new List<EntityAntiData>();
                List<EnityAntiDisTend> disData = new List<EnityAntiDisTend>();
                m_objViewer.dgvMicdistributiontend.DataSource = null;
                DataTable dtData = new DataTable();
                dtData.Columns.Add(@"����\ϸ��", Type.GetType("System.String"));

                #region ϸ��  ��
                int flgXj = 0;
                string applicationStr = string.Empty;
                foreach (DataRow dr in dtbMicName.Rows)
                {
                    flgXj = 0;
                    string XJMC = dr["XJMC"].ToString().Trim();
                    if (!XJMC.Contains("����"))
                    {
                        if (XJMC.Contains("��") || XJMC.Contains("����") || XJMC.Contains("?") || XJMC.Contains("��") || XJMC.Contains("��"))
                            continue;
                    }

                    XJMC = XJMC.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "").Replace("��", "(");

                    int idx = XJMC.IndexOf("(");
                    if (idx > 0)
                        XJMC = XJMC.Substring(0, idx);

                    for (int j = 0; j < dtData.Columns.Count; j++)
                    {
                        if (dtData.Columns[j].ToString() == XJMC)
                        {
                            flgXj = 1;
                        }
                    }

                    if (flgXj == 0)
                    {
                        dtData.Columns.Add(XJMC, Type.GetType("System.String"));
                    }

                    applicationStr += dr["application_id_chr"] + ",";
                }
                #endregion

                if (!string.IsNullOrEmpty(applicationStr))
                    applicationStr = "(" + applicationStr.TrimEnd(',') + ")";
                else
                    return;
                lngRes = m_objManage.lngGetAntiByDept(strTempName, applicationStr, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);

                string KsLast = string.Empty;
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        string KS = dr["KS"].ToString().Trim();
                        string result = dr["RESULT"].ToString().Trim();
                        if (!result.Contains("����"))
                        {
                            if ((result.Contains("��") || result.Contains("����") || result.Contains("?") || result.Contains("��") || result.Contains("��")))
                                continue;
                        }

                        result = result.Replace("1��", "").Replace("2��", "").Replace("1.", "").Replace("2.", "").Replace("��", "(");

                        int idx = result.IndexOf("(");
                        if (idx > 0)
                            result = result.Substring(0, idx);

                        #region �����ж�

                        if (GlFlgStr == "1")
                        {
                            DataRow[] drr = dtGlAnti.Select("xjmc = '" + result + "' and glys = '" + "+'");
                            if (drr != null && drr.Length > 0)
                            {

                            }
                            else
                                continue;
                        }

                        if (GlFlgStr == "0")
                        {
                            DataRow[] drr = dtGlAnti.Select("xjmc = '" + result + "' and glys = '" + "-'");
                            if (drr != null && drr.Length > 0)
                            {

                            }
                            else
                                continue;
                        }

                        #endregion

                        if (KS != KsLast)
                        {
                            KsLast = KS;

                            DataRow newRow;
                            newRow = dtData.NewRow();
                            newRow[0] = KS;

                            for (int j = 0; j < dtData.Columns.Count; j++)
                            {
                                if (dtData.Columns[j].ToString() == result)
                                {
                                    newRow[j] = 1;
                                }
                            }

                            dtData.Rows.Add(newRow);
                        }
                        else
                        {
                            int rowIdx = dtData.Rows.Count - 1;

                            for (int j = 0; j < dtData.Columns.Count; j++)
                            {
                                if (dtData.Columns[j].ToString() == result)
                                {
                                    if (dtData.Rows[rowIdx][j] != DBNull.Value)
                                        dtData.Rows[rowIdx][j] = Convert.ToInt32(dtData.Rows[rowIdx][j]) + 1;
                                    else
                                        dtData.Rows[rowIdx][j] = 1;
                                }
                            }
                        }
                    }
                }
                
                m_objViewer.dgvAntiByDept.DataSource = dtData;
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }
        #endregion

        #region ���Ա걾��Դ��
        /// <summary>
        /// ���Ա걾��Դ��
        /// </summary>
        public void m_mthGetDgvSampleSum()
        {
            DataTable dtbMicName;

            if (m_objViewer.dgv_mic.Rows.Count > 1)
            {
                strTempName = m_objViewer.dgv_mic.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                strTempName = null;
            }
            long lngRes = -1;
            string sampleId = string.Empty;
            sampleId = getSampleId(m_objViewer.cbxSampleType.Text.Trim());
            lngRes = m_objManage.lngGetAnti(strTempName, dtDateFrom, dtDateTo, DeptIdArr, out dtbMicName);

            if (lngRes > 0 && dtbMicName.Rows.Count > 0)
            {
                this.FillDgvSampleSum(dtbMicName);
            }
            else
            {
                MessageBox.Show("û��������ݡ�");
            }
        }
        /// <summary>
        ///��� ���Ա걾��Դ��
        /// </summary>
        /// <param name="dtbResult"></param>
        public void FillDgvSampleSum(DataTable dtbMicName)
        {
            try
            {
                long lngRes = -1;
                DataTable dtbResult = null;
                decimal sampleSum = 0;
                List<EnitySampleSum> data = new List<EnitySampleSum>();
                m_objViewer.dgvSampleSum.DataSource = null;
                string applicationStr = string.Empty;
                foreach (DataRow dr in dtbMicName.Rows)
                {
                    applicationStr += "'" + dr["application_id_chr"] + "',";
                }

                if (!string.IsNullOrEmpty(applicationStr))
                    applicationStr = "(" + applicationStr.TrimEnd(',') + ")";
                else
                    return;

                lngRes = m_objManage.lngGetAntiSample(strTempName, applicationStr, DeptIdArr, sampleId, DisNo, Sex, out dtbResult);
                string sampleTypeLast = string.Empty;

                foreach (DataRow dr in dtbResult.Rows)
                {
                    string sampleType = dr["BBLX"].ToString().Trim();
                    sampleSum++;

                    if (sampleType != sampleTypeLast)
                    {
                        sampleTypeLast = sampleType;
                        EnitySampleSum vo = new EnitySampleSum();
                        vo.count = 1;
                        vo.sampleType = sampleType;
                        data.Add(vo);
                    }
                    else
                    {
                        int rowIdx = data.Count -1;
                        data[rowIdx].count++;
                    }
                }

                for (int i = 0; i < data.Count; i++)
                {
                    data[i].countPer = (data[i].count / sampleSum).ToString("0.00%");
                }

                if (data.Count > 0)
                {
                    EnitySampleSum vo = new EnitySampleSum();
                    vo.sampleType = "�ϼ�";
                    vo.count = sampleSum++;
                    vo.countPer = "100.00";
                    data.Add(vo);
                }

                m_objViewer.dgvSampleSum.DataSource = data;
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }

        #endregion


        public List<EntityAntiApp> antiSort(List<EntityAntiApp> data)
        {
            string[] appArr1 = null;
            string[] appArr2 = null;
            List<EntityAntiApp> dataRe = new List<EntityAntiApp>();
            EntityAntiApp vo = new EntityAntiApp();
            for (int i = 0; i < data.Count; i++)
            {
                appArr1 = data[i].appStr.Split(',');

                for (int j = 0; j<data.Count;j++)
                {
                    appArr2 = data[j].appStr.Split(',');

                    if (appArr2.Length < appArr1.Length)
                    {
                        vo = data[j];
                        data[j] = data[i];
                        data[i] = vo;
                    }
                }

            }
            dataRe = data;
            return dataRe;
        }

        #region ����EXCEL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statType"></param>
        public void m_mthExportToExcel(DataGridView dgvData, string caption, string lblStr)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "����Excel�ļ���";
            string dteStart = m_objViewer.m_dtpDatFrom.Text;
            string dteEnd = m_objViewer.m_dtpDatTo.Text;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = saveFileDialog.OpenFile();
                StreamWriter streamWriter = new StreamWriter(stream, Encoding.GetEncoding("gb2312"));
                string text = "";
                string itemStr = string.Empty;
                string title = string.Empty;

                try
                {
                    for (int i = 0; i < dgvData.ColumnCount / 2; i++)
                    {
                        if (dgvData.Columns[i].Visible)
                        {
                            if (i > 0)
                            {
                                text += "\t";
                            }
                        }
                    }

                    text += caption;

                    streamWriter.WriteLine(text);
                    text = dteStart + "~" + dteEnd;
                    streamWriter.WriteLine(text);
                    text = lblStr;
                    streamWriter.WriteLine(lblStr);
                    text = "";
                    for (int i = 0; i < dgvData.ColumnCount; i++)
                    {
                        if (dgvData.Columns[i].Visible)
                        {
                            if (i > 0)
                            {
                                text += "\t";
                            }
                            text += dgvData.Columns[i].HeaderText.Replace("\n", "");
                        }
                    }
                    streamWriter.WriteLine(text);
                    for (int i = 0; i < dgvData.Rows.Count; i++)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        for (int j = 0; j < dgvData.Columns.Count; j++)
                        {
                            if (dgvData.Columns[j].Visible)
                            {
                                if (j > 0)
                                {
                                    stringBuilder.Append("\t");
                                }
                                stringBuilder.Append((dgvData.Rows[i].Cells[j].Value == null) ? "" : dgvData.Rows[i].Cells[j].Value.ToString());
                            }
                        }
                        streamWriter.WriteLine(stringBuilder);
                    }
                    MessageBox.Show("�����ɹ���", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    streamWriter.Close();
                    stream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    streamWriter.Close();
                    stream.Close();
                }
            }
        }
        #endregion

        #region getSampleId
        /// <summary>
        /// getSampleId
        /// </summary>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public string getSampleId(string sampleType)
        {
            string sampleId = string.Empty;

            foreach (var item in dicSampleType)
            {
                if (item.Value == m_objViewer.cbxSampleType.Text)
                    sampleId = item.Key;
            }

            return sampleId;
        }
        #endregion
    }
}