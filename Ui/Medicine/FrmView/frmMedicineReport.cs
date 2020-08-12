using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.iCare.middletier.HIS;
using weCare.Core.Entity;
using Sybase.DataWindow;
using System.IO;
using System.Reflection;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmMedicineReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmMedicineReport()
        {
            InitializeComponent();
        }

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsControlMedicineReport();
            objController.Set_GUI_Apperance(this);
        }

        private void frmMedicineReport_Load(object sender, EventArgs e)
        {
            string str = Application.StartupPath + @"\report.pbl";
            this.dwRpt1.LibraryList = str;
            this.dwRpt2.LibraryList = str;
            this.dwRpt3.LibraryList = str;
            this.dwRpt1.DataWindowObject = "d_meddoct";
            this.dwRpt2.DataWindowObject = "d_medstock";
            this.dwRpt3.DataWindowObject = "d_medinout";
            this.panelMedList.Height = 0;            
        }

        private void buttonXP2_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 3;
            DataTable table = null;
            ((clsControlMedicineReport)objController).m_lngGetMedicineList(out table);
            this.dtgMedicineList.DataSource = table;
            for (int i = 0; i < this.dtgMedicineList.Rows.Count; i++)
            {
                this.dtgMedicineList.Rows[i].Cells[0].Value = Convert.ToString((int)(i + 1));
            }
        }

        private void dtgMedicineList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string str = this.dtgMedicineList.Rows[e.RowIndex].Cells["colmedicineid_chr"].Value.ToString();
                for (int i = 0; i < this.dtgItem.Rows.Count; i++)
                {
                    if (this.dtgItem.Rows[i].Cells[2].Value.ToString() == str)
                    {
                        MessageBox.Show("该项目已添加。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
                string[] values = new string[] { this.dtgMedicineList.Rows[e.RowIndex].Cells["colitemcode_vchr"].Value.ToString(), this.dtgMedicineList.Rows[e.RowIndex].Cells["colitemname_vchr"].Value.ToString(), this.dtgMedicineList.Rows[e.RowIndex].Cells["colmedicineid_chr"].Value.ToString(), this.dtgMedicineList.Rows[e.RowIndex].Cells["colitemid_chr"].Value.ToString() };
                this.dtgItem.Rows.Add(values);
                this.m_mthSetRowColor();
            }
        }

        private void m_mthSetRowColor()
        {
            for (int i = 0; i < this.dtgItem.Rows.Count; i++)
            {
                if (Math.IEEERemainder((double)i, 2.0) != 0.0)
                {
                    this.dtgItem.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(0xc0, 0xc0, 0xff);
                }
                else
                {
                    this.dtgItem.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
            if (this.dtgItem.Rows.Count > 0)
            {
                this.lblNums.Text = this.dtgItem.Rows.Count.ToString() + " 条";
            }
            else
            {
                this.lblNums.Text = "";
            }
        }

        private void buttonXP6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonXP3_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                ((clsControlMedicineReport)objController).m_mthExportPtDwToExcel(this.dwRpt1);
            }
            else if (this.tabControl1.SelectedIndex == 1)
            {
                ((clsControlMedicineReport)objController).m_mthExportPtDwToExcel(this.dwRpt2);
            }
            else if (this.tabControl1.SelectedIndex == 2)
            {
                ((clsControlMedicineReport)objController).m_mthExportPtDwToExcel(this.dwRpt3);
            }
            else if ((this.tabControl1.SelectedIndex == 3) && (this.dtgMedicineList.Rows.Count > 0))
            {
                DataStore store = new DataStore();
                store.LibraryList = Application.StartupPath + @"\report.pbl";
                store.DataWindowObject = "d_medlist";
                int rowNumber = 0;
                for (int i = 0; i < this.dtgMedicineList.Rows.Count; i++)
                {
                    rowNumber = store.InsertRow(0);
                    store.SetItemString(rowNumber, "col1", this.dtgMedicineList.Rows[i].Cells[0].Value.ToString());
                    store.SetItemString(rowNumber, "col2", this.dtgMedicineList.Rows[i].Cells[1].Value.ToString());
                    store.SetItemString(rowNumber, "col3", this.dtgMedicineList.Rows[i].Cells[2].Value.ToString());
                    store.SetItemString(rowNumber, "col4", this.dtgMedicineList.Rows[i].Cells[3].Value.ToString());
                    store.SetItemString(rowNumber, "col5", this.dtgMedicineList.Rows[i].Cells[4].Value.ToString());
                    store.SetItemString(rowNumber, "col6", this.dtgMedicineList.Rows[i].Cells[5].Value.ToString());
                    store.SetItemString(rowNumber, "col7", this.dtgMedicineList.Rows[i].Cells[6].Value.ToString());
                    store.SetItemString(rowNumber, "col8", this.dtgMedicineList.Rows[i].Cells[7].Value.ToString());
                }
                ((clsControlMedicineReport)objController).m_mthExportPtDwToExcel(store);
                store = null;
            }

        }

        private void buttonXP4_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.dwRpt1.Print(true);
            }
            else if (this.tabControl1.SelectedIndex == 1)
            {
                this.dwRpt2.Print(true);
            }
            else if (this.tabControl1.SelectedIndex == 2)
            {
                this.dwRpt3.Print(true);
            }

        }

        private void frmMedicineReport_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape) && this.dtgMedList.Focus())
            {
                this.panelMedList.Height = 0;                
                this.txtFind.Focus();
            }

        }

        private void dtgMedList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.m_mthAddMed(this.dtgMedList.CurrentCell.RowIndex);
            }

        }

        private void dtgMedList_Leave(object sender, EventArgs e)
        {
            this.panelMedList.Height = 0;
        }

        private void m_mthAddMed(int p_intRow)
        {
            string str = this.dtgMedList.Rows[p_intRow].Cells[2].Value.ToString();
            for (int i = 0; i < this.dtgItem.Rows.Count; i++)
            {
                if (this.dtgItem.Rows[i].Cells[2].Value.ToString() == str)
                {
                    MessageBox.Show("该项目已添加。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            string[] values = new string[] { this.dtgMedList.Rows[p_intRow].Cells[0].Value.ToString(), this.dtgMedList.Rows[p_intRow].Cells[1].Value.ToString(), this.dtgMedList.Rows[p_intRow].Cells[2].Value.ToString(), this.dtgMedList.Rows[p_intRow].Cells[3].Value.ToString() };
            this.dtgItem.Rows.Add(values);
            this.m_mthSetRowColor();
        }

        private void dtgItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.m_mthDelMed(e.RowIndex);
            }

        }

        private void m_mthDelMed(int p_intRow)
        {
            this.dtgItem.Rows.RemoveAt(p_intRow);
            this.m_mthSetRowColor();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "TXT|*.txt";
            dialog.Title = "选择文件";
            dialog.InitialDirectory = @"D:\";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string str = string.Empty;
                List<string> list = new List<string>();
                StreamReader reader = File.OpenText(dialog.FileName);
                do
                {
                    try
                    {
                        str = reader.ReadLine().Trim();
                    }
                    catch
                    {
                        str = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(str))
                    {
                        list.Add(str);
                    }
                }
                while (!string.IsNullOrEmpty(str));
                reader.Close();
                string str3 = string.Empty;
                foreach (string str4 in list)
                {
                    str3 = str3 + "'" + str4 + "',";
                }
                if (!string.IsNullOrEmpty(str3))
                {
                    str3 = str3.Substring(0, str3.Length - 1);
                    DataTable table = new DataTable();
                    long lngRes = ((clsControlMedicineReport)objController).m_lngImpMedItem(str3, out table);
                    if ((lngRes > 0L) && (table.Rows.Count > 0))
                    {
                        DataRow row = null;
                        this.dtgItem.Rows.Clear();
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            row = table.Rows[i];
                            string[] values = new string[] { row["itemcode_vchr"].ToString(), row["itemname_vchr"].ToString(), row["medicineid_chr"].ToString(), row["itemid_chr"].ToString() };
                            this.dtgItem.Rows.Add(values);
                        }
                        this.m_mthSetRowColor();
                    }
                    else
                    {
                        MessageBox.Show("未找到有效药品。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }

        }

        private void delCurrentItem_Click(object sender, EventArgs e)
        {
            if (this.dtgItem.CurrentCell == null) return;
            if (this.dtgItem.CurrentCell.RowIndex >= 0)
            {
                this.m_mthDelMed(this.dtgItem.CurrentCell.RowIndex);
            }

        }

        private void delAllItem_Click(object sender, EventArgs e)
        {
            this.dtgItem.Rows.Clear();
            this.lblNums.Text = "";
        }

        private void txtFind_Enter(object sender, EventArgs e)
        {
            this.txtFind.SelectAll();
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string str = this.txtFind.Text.Trim();
                if (str == string.Empty)
                {
                    MessageBox.Show("请输入查找条件。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    DataTable table = new DataTable();
                    long lngRes = ((clsControlMedicineReport)objController).m_lngGetMedItem(str, out table);
                    if (lngRes > 0L)
                    {
                        if (table.Rows.Count == 0)
                        {                            
                            this.panelMedList.Height = 0;
                            this.panelMedList.Visible = false;
                            MessageBox.Show("没有满足条件的药品信息。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            this.dtgMedList.Rows.Clear();
                            DataRow row = null;
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                row = table.Rows[i];
                                string[] values = new string[] { row["itemcode_vchr"].ToString(), row["itemname_vchr"].ToString(), row["medicineid_chr"].ToString(), row["itemid_chr"].ToString(), row["medspec_vchr"].ToString() };
                                this.dtgMedList.Rows.Add(values);
                            }
                            this.dtgMedList.Rows[0].Cells[0].Selected = true;                            
                            this.panelMedList.Height = 200;
                            this.panelMedList.Visible = true;
                            if (table.Rows.Count == 1)
                            {
                                this.dtgMedList.Focus();
                            }
                        }
                    }
                }
            }
            else if ((e.KeyCode == Keys.Down) && this.panelMedList.Visible)
            {
                this.dtgMedList.Focus();
            }

        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            if (this.dtgItem.Rows.Count == 0)
            {
                MessageBox.Show("请指定要统计的药品。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DateTime time = Convert.ToDateTime(this.dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime time2 = Convert.ToDateTime(this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59");
                if (time > time2)
                {
                    MessageBox.Show("开始日期不能小于结束日期。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    int num2;
                    long num = 0L;
                    string str = string.Empty;
                    for (num2 = 0; num2 < this.dtgItem.Rows.Count; num2++)
                    {
                        str = str + "'" + this.dtgItem.Rows[num2].Cells[3].Value.ToString() + "',";
                    }
                    str = str.Substring(0, str.Length - 1);
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        DataTable table = new DataTable();
                        DataTable table2 = new DataTable();
                        DataTable table3 = new DataTable();
                        num = ((clsControlMedicineReport)objController).m_lngStatDoctUseMed(time, time2, str, out table);
                        num = ((clsControlMedicineReport)objController).m_lngStatMedStock(str, out table2);
                        num = ((clsControlMedicineReport)objController).m_lngStatMedInOutStore(time, time2, str, out table3);
                        this.dwRpt1.SetRedrawOff();
                        this.dwRpt2.SetRedrawOff();
                        this.dwRpt3.SetRedrawOff();
                        this.dwRpt1.Reset();
                        this.dwRpt2.Reset();
                        this.dwRpt3.Reset();
                        int rowNumber = 0;
                        DataRow row = null;
                        string str2 = string.Empty;
                        if ((table != null) && (table.Rows.Count > 0))
                        {
                            for (num2 = 0; num2 < table.Rows.Count; num2++)
                            {
                                row = table.Rows[num2];
                                rowNumber = this.dwRpt1.InsertRow(0);
                                if (row["depttype"].ToString() == "1")
                                {
                                    str2 = "门诊";
                                }
                                else
                                {
                                    str2 = "住院";
                                }
                                this.dwRpt1.SetItemString(rowNumber, "coldeptname", row["deptname"].ToString().Trim() + "(" + str2 + ")");
                                this.dwRpt1.SetItemString(rowNumber, "coldoctname", row["doctname"].ToString().Trim());
                                this.dwRpt1.SetItemString(rowNumber, "colitemcode", row["itemcode_vchr"].ToString().Trim());
                                this.dwRpt1.SetItemString(rowNumber, "colitemname", row["itemname_vchr"].ToString().Trim());
                                this.dwRpt1.SetItemString(rowNumber, "colitemspec", row["itemspec_vchr"].ToString().Trim());
                                this.dwRpt1.SetItemDecimal(rowNumber, "colitemprice", ((clsControlMedicineReport)objController).ConvertObjToDecimal(row["unitprice_mny"].ToString()));
                                this.dwRpt1.SetItemDecimal(rowNumber, "colitemamount", ((clsControlMedicineReport)objController).ConvertObjToDecimal(row["totalqty"].ToString()));
                                this.dwRpt1.SetItemDecimal(rowNumber, "colitemsum", ((clsControlMedicineReport)objController).ConvertObjToDecimal(row["totalsum"].ToString()));
                            }
                            this.dwRpt1.SetSort("coldeptname asc, coldoctname asc, colitemcode asc");
                            this.dwRpt1.Sort();
                        }
                        if ((table2 != null) && (table2.Rows.Count > 0))
                        {
                            for (num2 = 0; num2 < table2.Rows.Count; num2++)
                            {
                                row = table2.Rows[num2];
                                if (((clsControlMedicineReport)objController).ConvertObjToDecimal(row["realgross_int"].ToString()) != 0M)
                                {
                                    rowNumber = this.dwRpt2.InsertRow(0);
                                    this.dwRpt2.SetItemString(rowNumber, "colitemcode", row["itemcode_vchr"].ToString().Trim());
                                    this.dwRpt2.SetItemString(rowNumber, "colitemname", row["medicinename_vchr"].ToString().Trim());
                                    this.dwRpt2.SetItemString(rowNumber, "colproductor", row["productorid_chr"].ToString().Trim());
                                    this.dwRpt2.SetItemString(rowNumber, "colspec", row["medspec_vchr"].ToString().Trim());
                                    this.dwRpt2.SetItemString(rowNumber, "colunit", row["opunit_vchr"].ToString().Trim());
                                    this.dwRpt2.SetItemString(rowNumber, "collotno", row["lotno_vchr"].ToString().Trim());
                                    this.dwRpt2.SetItemString(rowNumber, "colrealgross", row["realgross_int"].ToString().Trim());
                                }
                            }
                            this.dwRpt2.SetSort("colitemcode asc, collotno asc");
                            this.dwRpt2.Sort();
                        }
                        if ((table3 != null) && (table3.Rows.Count > 0))
                        {
                            for (num2 = 0; num2 < table3.Rows.Count; num2++)
                            {
                                row = table3.Rows[num2];
                                rowNumber = this.dwRpt3.InsertRow(0);
                                this.dwRpt3.SetItemString(rowNumber, "colitemcode", row["itemcode_vchr"].ToString().Trim());
                                this.dwRpt3.SetItemString(rowNumber, "colitemname", row["medicinename_vch"].ToString().Trim());
                                this.dwRpt3.SetItemString(rowNumber, "collotno", row["lotno_vchr"].ToString().Trim());
                                this.dwRpt3.SetItemString(rowNumber, "colspec", row["medspec_vchr"].ToString().Trim());
                                this.dwRpt3.SetItemString(rowNumber, "colinamount", (((clsControlMedicineReport)objController).ConvertObjToDecimal(row["inamount"].ToString()) == 0M) ? "" : row["inamount"].ToString());
                                this.dwRpt3.SetItemString(rowNumber, "coloutamount", (((clsControlMedicineReport)objController).ConvertObjToDecimal(row["outamount"].ToString()) == 0M) ? "" : row["outamount"].ToString());
                                this.dwRpt3.SetItemString(rowNumber, "colunit", row["unit"].ToString().Trim());
                                this.dwRpt3.SetItemString(rowNumber, "colcallprice", row["callprice_int"].ToString().Trim());
                                this.dwRpt3.SetItemString(rowNumber, "colretailprice", row["retailprice_int"].ToString().Trim());
                                this.dwRpt3.SetItemString(rowNumber, "coldate", row["meddate"].ToString().Trim());
                                this.dwRpt3.SetItemString(rowNumber, "coldeptname", row["deptname"].ToString().Trim());
                            }
                            this.dwRpt3.SetSort("colitemcode asc, collotno asc, coldate asc");
                            this.dwRpt3.Sort();
                        }
                        this.dwRpt1.Modify("t_date.text = '" + time.ToString("yyyy-MM-dd") + " ~ " + time2.ToString("yyyy-MM-dd") + "'");
                        this.dwRpt3.Modify("t_date.text = '" + time.ToString("yyyy-MM-dd") + " ~ " + time2.ToString("yyyy-MM-dd") + "'");
                        this.dwRpt1.SetRedrawOn();
                        this.dwRpt2.SetRedrawOn();
                        this.dwRpt3.SetRedrawOn();
                        this.dwRpt1.Refresh();
                        this.dwRpt2.Refresh();
                        this.dwRpt3.Refresh();
                        if (this.tabControl1.SelectedIndex == 3)
                        {
                            this.tabControl1.SelectedIndex = 0;
                        }
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }

        }

        private void buttonXP5_Click(object sender, EventArgs e)
        {
            try
            {
                string assemblyFile = Application.StartupPath + @"\hisregister.dll";
                string typeName = "com.digitalwave.iCare.gui.HIS.frmDoctorUsingMedicineReport";
                object obj2 = Assembly.LoadFrom(assemblyFile).CreateInstance(typeName, true);
                Type type = obj2.GetType();
                object[] parameters = null;
                MethodInfo method = type.GetMethod("Show", new Type[0]);
                if (method == null)
                {
                    MessageBox.Show("打开门诊医生报表失败。");
                }
                else
                {
                    Form form = obj2 as Form;
                    form.WindowState = FormWindowState.Maximized;
                    method.Invoke(obj2, parameters);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "打开异常");
            }
        }

        private void dtgMedList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.m_mthAddMed(e.RowIndex);
            }

        }

 


 

    }
}