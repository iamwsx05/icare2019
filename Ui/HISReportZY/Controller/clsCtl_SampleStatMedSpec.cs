using System;
using System.Collections.Generic;
using com.digitalwave.iCare.gui.HIS.Reports;
using System.Text;
using Sybase.DataWindow;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Common.Controls;
using weCare.Core.Utils;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class clsCtl_SampleStatMedSpec : com.digitalwave.GUI_Base.clsController_Base
    {
        #region 构造函数
        public clsCtl_SampleStatMedSpec()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            m_objManage = new clsDcl_SampleStatMedSpec();
        }
        #endregion


        /// <summary>
        /// 类变量
        /// </summary>
        private frmSampleStatMedSpec m_objViewer;
        internal bool IsEnglish = false;
        private clsDcl_SampleStatMedSpec m_objManage;
        Dictionary<string, string> dicGroup;


        #region Entity
        /// <summary>
        /// 
        /// </summary>
        public class EnitySapleMedSpec
        {
            public string deptName { get; set; }
            //public string checkItem { get; set; }
            public decimal preMaxTime { get; set; }
            public decimal preMinTime { get; set; }
            public decimal preMidTime { get; set; }
            public decimal lisMaxTime { get; set; }
            public decimal lisMinTime { get; set; }
            public decimal lisMidTime { get; set; }
            public decimal BBS { get; set; }
            //public decimal sampleCount { get; set; }
        }

        public class EntitySamepleStat
        {
            public string HZXM { get; set; }
            public string DEPTNAME { get; set; }
            public string BARCODE { get; set; }
            public string CARDNO { get; set; }
            public string Age { get; set; }
            public string item { get; set; }
            public string ApplyTime { get; set; }
            public string AcceptTime { get; set; }
            public string ConfirmTime { get; set; }
            public string Checker { get; set; }
            public decimal preTime { get; set; }
            public decimal lisTime { get; set; }
            public string color { get; set; }
            public string DBSJ { get; set; }
            public string DYSJ { get; set; }
        }
        #endregion

        #region 设置窗体对象
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmSampleStatMedSpec)frmMDI_Child_Base_in;
        }
        #endregion

        #region init
        /// <summary>
        /// init
        /// </summary>
        public void m_mthInit()
        {
            DataTable dtbResult;
            dicGroup = new Dictionary<string, string>();
            m_objViewer.tabContorl.Visible = false;
            m_objViewer.dteStart.Value = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01");
            m_objViewer.cboPatType.Items.Add("住院");
            m_objViewer.cboPatType.Items.Add("门诊");
            m_objViewer.cboEmergency.Items.Add("急诊项目");
            m_objViewer.cboEmergency.Items.Add("非急诊项目");
            m_objManage.GetAllCheckSpec(out dtbResult);

            for (int i = 0; i < dtbResult.Rows.Count; i++)
            {
                m_objViewer.cbxGroup.Items.Add(dtbResult.Rows[i]["check_category_desc_vchr"].ToString());
                dicGroup.Add(dtbResult.Rows[i]["check_category_id_chr"].ToString(), dtbResult.Rows[i]["check_category_desc_vchr"].ToString());
            }
        }
        #endregion

        #region 列出所有检验项目
        /// <summary>
        /// 列出所有检验项目
        /// </summary>
        public void m_mthListCheckItem()
        {
            DataTable dtbResult;
            string groupId = string.Empty;
            foreach (KeyValuePair<string, string> kvp in dicGroup)
            {
                if (kvp.Value.Equals(this.m_objViewer.cbxGroup.Text))
                {
                    groupId = kvp.Key;
                }
            }

            long lngRes = m_objManage.lngGetAllCheckItem(out dtbResult, groupId);

            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                m_objViewer.dgvItem.DataSource = dtbResult;
            }
        }
        #endregion

        #region  检验标本周转中位数统计表
        /// <summary>
        /// 
        /// </summary>
        public void m_mthGeSampleStatSpec2()
        {
            DataTable dtbResult;
            if (string.IsNullOrEmpty(m_objViewer.cbxGroup.Text) && m_objViewer.dgvCheckItem.Rows.Count < 2)
            {
                MessageBox.Show("请选择专业组或检验项目 ！");
                return;
            }

            List<EnitySapleMedSpec> data = new List<EnitySapleMedSpec>();
            string groupId = string.Empty;
            string enmergencyFlg = string.Empty;
            string patType = string.Empty;
            string dteStart = m_objViewer.dteStart.Text;
            string dteEnd = m_objViewer.dteEnd.Text;
            string applyUnitId = string.Empty;

            if (m_objViewer.dgvCheckItem.Rows.Count >= 2)
            {
                for (int i = 0; i < m_objViewer.dgvCheckItem.Rows.Count - 1; i++)
                {
                    applyUnitId += "'" + m_objViewer.dgvCheckItem.Rows[i].Cells[0].Value.ToString() + "',";
                }

                applyUnitId = "(" + applyUnitId.TrimEnd(',') + ")";
            }

            string strDept = m_objViewer.DeptIdArr;
            if (m_objViewer.cboEmergency.Text.Trim() == "急诊项目")
                enmergencyFlg = "1";
            else if (m_objViewer.cboEmergency.Text.Trim() == "非急诊项目")
                enmergencyFlg = "0";

            if (m_objViewer.cboPatType.Text.Trim() == "住院")
                patType = "1";
            else if (m_objViewer.cboPatType.Text.Trim() == "门诊")
                patType = "2";

            int tsFlg = m_objViewer.rdgTime.SelectedIndex;

            foreach (var item in dicGroup)
            {
                if (item.Value == m_objViewer.cbxGroup.Text)
                    groupId = item.Key;
            }

            if (m_objViewer.dgvCheckItem.Rows.Count >= 2)
                groupId = "";

            clsPublic.PlayAvi("findFILE.avi", "正在查询项目信息，请稍候...");
          

            try
            {
                long lngRes = m_objManage.lngGetSampleMedSpec(out dtbResult, dteStart, dteEnd, groupId, applyUnitId, strDept, enmergencyFlg, patType, tsFlg,false);
                int flgKS = 0;

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        flgKS = 0;
                        string deptName = dr["deptname"].ToString();
                        #region 科室
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (deptName == data[i].deptName)
                            {
                                flgKS = 1;
                            }
                        }

                        if (flgKS == 0)
                        {
                            EnitySapleMedSpec vo = new EnitySapleMedSpec();
                            vo.deptName = deptName;

                            data.Add(vo);
                        }
                        #endregion
                    }

                    if (data.Count > 0)
                    {
                        #region 各科室
                        for (int i = 0; i < data.Count; i++)
                        {
                            DataRow[] drr = dtbResult.Select("deptname = '" + data[i].deptName  +"' and pretime >= 0" + " and listime >= 0");

                            if (drr.Length > 0)
                            {
                                data[i].BBS = drr.Length;

                                for (int drI = 0; drI < drr.Length; drI++)
                                {
                                    DataRow dr = drr[drI];
                                    if (drI == 0)
                                    {
                                        if (dr["pretime"] != DBNull.Value)
                                        {
                                            data[i].preMaxTime = Convert.ToDecimal(dr["pretime"]);
                                            data[i].preMinTime = Convert.ToDecimal(dr["pretime"]);
                                        }

                                        if (dr["listime"] != DBNull.Value)
                                        {
                                            data[i].lisMinTime = Convert.ToDecimal(dr["listime"]);
                                            data[i].lisMaxTime = Convert.ToDecimal(dr["listime"]);
                                        }
                                    }

                                    if (dr["pretime"] != DBNull.Value)
                                    {
                                        if (data[i].preMaxTime < Convert.ToDecimal(dr["pretime"]) && Convert.ToDecimal(dr["pretime"]) > 0)
                                            data[i].preMaxTime = Convert.ToDecimal(dr["pretime"]);
                                        if (data[i].preMinTime > Convert.ToDecimal(dr["pretime"]) && Convert.ToDecimal(dr["pretime"]) > 0)
                                            data[i].preMinTime = Convert.ToDecimal(dr["pretime"]);
                                    }

                                    if (dr["listime"] != DBNull.Value)
                                    {
                                        if (data[i].lisMaxTime < Convert.ToDecimal(dr["listime"]) && Convert.ToDecimal(dr["listime"]) > 0)
                                            data[i].lisMaxTime = Convert.ToDecimal(dr["listime"]);
                                        if (data[i].lisMinTime > Convert.ToDecimal(dr["listime"]) && Convert.ToDecimal(dr["listime"]) > 0)
                                            data[i].lisMinTime = Convert.ToDecimal(dr["listime"]);
                                    }
                                }

                                DataRow[] preDrr = dtbResult.Select("deptname = '" + data[i].deptName + "' and pretime >= 0" + " and listime >= 0", " pretime desc");
                                DataRow[] lisDrr = dtbResult.Select("deptname = '" + data[i].deptName + "' and listime >= 0" + " and pretime >= 0", " listime desc");
                               
                                decimal preMidTime = 0;
                                decimal preMidTime1 = 0;

                                if (preDrr.Length > 0)
                                {
                                    int residPre = preDrr.Length % 2;
                                    int remaindPre = preDrr.Length / 2;
                                    if (preDrr[remaindPre]["pretime"] == DBNull.Value)
                                        preMidTime = 0;
                                    else
                                        preMidTime = Convert.ToDecimal(preDrr[remaindPre]["pretime"]);

                                    if (remaindPre > 0)
                                    {
                                        if (preDrr[remaindPre - 1]["pretime"] == DBNull.Value)
                                            preMidTime1 = 0;
                                        else
                                            preMidTime1 = Convert.ToDecimal(preDrr[remaindPre - 1]["pretime"]);
                                    }

                                    ////偶数
                                    if (residPre == 0)
                                    {
                                        data[i].preMidTime = (preMidTime + preMidTime1) / 2;
                                    }
                                    else
                                    {
                                        data[i].preMidTime = preMidTime;
                                    }
                                }

                                decimal lisMidTime = 0;
                                decimal lisMidTime1 = 0;

                                if (lisDrr.Length > 0)
                                {
                                    int residLis = lisDrr.Length % 2;
                                    int remaindLis = lisDrr.Length / 2;

                                    if (lisDrr[remaindLis]["listime"] == DBNull.Value)
                                        lisMidTime = 0;
                                    else
                                        lisMidTime = Convert.ToDecimal(lisDrr[remaindLis]["listime"]);

                                    if (remaindLis > 0)
                                    {
                                        if (lisDrr[remaindLis - 1]["listime"] == DBNull.Value)
                                            lisMidTime1 = 0;
                                        else
                                            lisMidTime1 = Convert.ToDecimal(lisDrr[remaindLis - 1]["listime"]);
                                    }

                                    ////偶数
                                    if (residLis == 0)
                                    {
                                        data[i].lisMidTime = (lisMidTime + lisMidTime1) / 2;
                                    }
                                    else
                                    {
                                        data[i].lisMidTime = lisMidTime;
                                    }
 
                                }
                                
                            }
                        }
                        #endregion

                        #region 全院
                        EnitySapleMedSpec voAll = new EnitySapleMedSpec();
                        voAll.deptName = "全院";
                        voAll.BBS = dtbResult.Rows.Count;
                        voAll.preMaxTime = 0;
                        voAll.preMinTime = 99999;
                        voAll.lisMaxTime = 0;
                        voAll.lisMinTime = 99999;
                        data.Add(voAll);

                        foreach (DataRow dr in dtbResult.Rows)
                        {
                            if (dr["pretime"] != DBNull.Value)
                            {
                                if (voAll.preMaxTime < Convert.ToDecimal(dr["pretime"]) && Convert.ToDecimal(dr["pretime"]) > 0)
                                    voAll.preMaxTime = Convert.ToDecimal(dr["pretime"]);
                                if (voAll.preMinTime > Convert.ToDecimal(dr["pretime"]) && Convert.ToDecimal(dr["pretime"]) > 0)
                                    voAll.preMinTime = Convert.ToDecimal(dr["pretime"]);
                            }

                            if (dr["listime"] != DBNull.Value)
                            {
                                if (voAll.lisMaxTime < Convert.ToDecimal(dr["listime"]) && Convert.ToDecimal(dr["listime"]) > 0)
                                    voAll.lisMaxTime = Convert.ToDecimal(dr["listime"]);
                                if (voAll.lisMinTime > Convert.ToDecimal(dr["listime"]) && Convert.ToDecimal(dr["listime"]) > 0)
                                    voAll.lisMinTime = Convert.ToDecimal(dr["listime"]);
                            }

                            DataRow[] preDrr = dtbResult.Select("pretime > 0", " pretime desc");
                            DataRow[] lisDrr = dtbResult.Select("listime > 0", " listime desc");

                            int residPre = preDrr.Length % 2;
                            int remaindPre = preDrr.Length / 2;
                            decimal preMidTime = 0;
                            decimal preMidTime1 = 0;

                            if (preDrr[remaindPre]["pretime"] == DBNull.Value)
                                preMidTime = 0;
                            else
                                preMidTime = Convert.ToDecimal(preDrr[remaindPre]["pretime"]);

                            if (remaindPre > 0)
                            {
                                if (preDrr[remaindPre - 1]["pretime"] == DBNull.Value)
                                    preMidTime1 = 0;
                                else
                                    preMidTime1 = Convert.ToDecimal(preDrr[remaindPre - 1]["pretime"]);
                            }

                            ////偶数
                            if (residPre == 0)
                            {
                                voAll.preMidTime = (preMidTime + preMidTime1) / 2;
                            }
                            else
                            {
                                voAll.preMidTime = preMidTime;
                            }

                            int residLis = lisDrr.Length % 2;
                            int remaindLis = lisDrr.Length / 2;
                            decimal lisMidTime = 0;
                            decimal lisMidTime1 = 0;

                            if (lisDrr[remaindLis]["listime"] == DBNull.Value)
                                lisMidTime = 0;
                            else
                                lisMidTime = Convert.ToDecimal(lisDrr[remaindPre]["listime"]);

                            if (remaindLis > 0)
                            {
                                if (lisDrr[remaindLis - 1]["listime"] == DBNull.Value)
                                    lisMidTime1 = 0;
                                else
                                    lisMidTime1 = Convert.ToDecimal(lisDrr[remaindLis - 1]["listime"]);
                            }


                            ////偶数
                            if (residLis == 0)
                            {
                                voAll.lisMidTime = (lisMidTime + lisMidTime1) / 2;
                            }
                            else
                            {
                                voAll.lisMidTime = lisMidTime;
                            }
                        }
                        #endregion
                    }
                }

                else
                {
                    MessageBox.Show("没有相关数据。");
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                clsPublic.CloseAvi();
            }


            m_objViewer.gcData.DataSource = data;

        }

        #endregion

        #region  检验标本周转中位数汇总
        /// <summary>
        /// 
        /// </summary>
        public void m_mthGeSampleStatSpecStat()
        {
            DataTable dtbResult;
            //if (string.IsNullOrEmpty(m_objViewer.cbxGroup.Text) && m_objViewer.dgvCheckItem.Rows.Count < 2)
            //{
            //    MessageBox.Show("请选择专业组或检验项目 ！");
            //    return;
            //}

            List<EntitySamepleStat> data = new List<EntitySamepleStat>();
            string groupId = string.Empty;
            string enmergencyFlg = string.Empty;
            string patType = string.Empty;
            string dteStart = m_objViewer.dteStart.Text;
            string dteEnd = m_objViewer.dteEnd.Text;
            string applyUnitId = string.Empty;

            try
            {
                if (m_objViewer.dgvCheckItem.Rows.Count >= 2)
                {
                    for (int i = 0; i < m_objViewer.dgvCheckItem.Rows.Count - 1; i++)
                    {
                        applyUnitId += "'" + m_objViewer.dgvCheckItem.Rows[i].Cells[0].Value.ToString() + "',";
                    }

                    applyUnitId = "(" + applyUnitId.TrimEnd(',') + ")";
                }

                string strDept = m_objViewer.DeptIdArr;
                if (m_objViewer.cboEmergency.Text.Trim() == "急诊项目")
                    enmergencyFlg = "1";
                else if (m_objViewer.cboEmergency.Text.Trim() == "非急诊项目")
                    enmergencyFlg = "0";

                if (m_objViewer.cboPatType.Text.Trim() == "住院")
                    patType = "1";
                else if (m_objViewer.cboPatType.Text.Trim() == "门诊")
                    patType = "2";

                int tsFlg = m_objViewer.rdgTime.SelectedIndex;
                bool peFlg = m_objViewer.chkPe.Checked;

                foreach (var item in dicGroup)
                {
                    if (item.Value == m_objViewer.cbxGroup.Text)
                        groupId = item.Key;
                }

                if (m_objViewer.dgvCheckItem.Rows.Count >= 2)
                    groupId = "";

                clsPublic.PlayAvi("findFILE.avi", "正在查询项目信息，请稍候...");

                long lngRes = m_objManage.lngGetSampleMedSpec(out dtbResult, dteStart, dteEnd, groupId, applyUnitId, strDept, enmergencyFlg, patType, tsFlg, peFlg);

                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        EntitySamepleStat vo = new EntitySamepleStat();

                        vo.HZXM = dr["HZXM"].ToString();
                        vo.Age = dr["Age"].ToString().Trim();
                        vo.DEPTNAME = dr["DEPTNAME"].ToString();
                        vo.BARCODE = dr["BARCODE"].ToString();
                        vo.CARDNO = string.IsNullOrEmpty(dr["CARDNO"].ToString()) ? dr["patInNo"].ToString() : dr["CARDNO"].ToString();
                        vo.ApplyTime = Function.Datetime(dr["applyTime"]).ToString("yyyy-MM-dd HH:mm");
                        vo.AcceptTime = Function.Datetime(dr["HSSJ"]).ToString("yyyy-MM-dd HH:mm");
                        vo.ConfirmTime = Function.Datetime(dr["SHSJ"]).ToString("yyyy-MM-dd HH:mm");
                        vo.Checker = dr["lastname_vchr"].ToString();
                        vo.item = dr["check_content_vchr"].ToString();
                        vo.color = dr["color"].ToString();
                        if (dr["pattype"].ToString().Trim() == "2")
                            vo.DYSJ = Function.Datetime(dr["DBSJ"]).ToString("yyyy-MM-dd HH:mm");
                        if (dr["pattype"].ToString().Trim() == "1")
                            vo.DBSJ = Function.Datetime(dr["DBSJ"]).ToString("yyyy-MM-dd HH:mm");
                        if (dr["preTime"] != DBNull.Value)
                            vo.preTime = Convert.ToDecimal(dr["preTime"]) > 0 ? Convert.ToDecimal(dr["preTime"]) : 0;
                        if (dr["lisTime"] != DBNull.Value)
                            vo.lisTime = Convert.ToDecimal(dr["lisTime"]) > 0 ? Convert.ToDecimal(dr["lisTime"]) : 0;

                        data.Add(vo);
                    }

                    m_objViewer.gcStat.DataSource = data;
                }
                else
                {
                    MessageBox.Show("没有相关数据。");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                clsPublic.CloseAvi();
            }

            clsPublic.CloseAvi();
        }

        #endregion

        #region m_mthGetCheckItemByName
        /// <summary>
        /// m_mthGetCheckItemByName
        /// </summary>
        public void m_mthGetCheckItemByName()
        {
            DataTable dtbResult;
            string strTempName = string.Empty;
            string strGroupId = string.Empty;
            strTempName = m_objViewer.txtSearchName.Text.Trim();
            string groupId = string.Empty;

            foreach (KeyValuePair<string, string> kvp in dicGroup)
            {
                if (kvp.Value.Equals(this.m_objViewer.cbxGroup.Text))
                {
                    strGroupId = kvp.Key;
                }
            }

            long lngRes = m_objManage.lngGetCheckItemByName(strTempName, strGroupId, out dtbResult);

            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                m_objViewer.dgvItem.DataSource = dtbResult;
            }
        }
        #endregion

        #region 导出EXCEL
        /// <summary>
        /// 
        /// </summary>
        public void m_mthExportToExcel()
        {
            if (m_objViewer.xtraTabControl.SelectedTabPageIndex == 0)
            {
                uiHelper.ExportToXls(m_objViewer.gvData);
            }
            else if (m_objViewer.xtraTabControl.SelectedTabPageIndex == 1)
            {
                uiHelper.ExportToXls(m_objViewer.gvStat);
            }
        }
        #endregion

        #region 关闭
        /// <summary>
        /// 
        /// </summary>
        public void Closed()
        {
            m_objViewer.Close();
        }

        #endregion

        #region 清空
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            m_objViewer.dgvCheckItem.Rows.Clear();
        }
        #endregion
    }
}
