using Common.Controls;
using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class frmPrintPageSetting : frmBasePopup
    {
        public frmPrintPageSetting()
        {
            InitializeComponent(); 
        }

        public string PrinterName { set; get; }

        public int PrintPageType { set; get; }

        public List<string> PrintPageScope { set; get; }

        public int PrintPageCopies { set; get; }

        /// <summary>
        /// 起始页码
        /// </summary>
        [DefaultValue(1)]
        public int PrintStartIndex { get; set; }

        //套打
        public bool BlnPreformatted { set; private get; }

        /// <summary>
        /// 打印范围
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupCheck.SelectedIndex != 3)
            {
                txtPageScope.ResetText();
                txtPageScope.Enabled = false;
            }
            else
            {
                txtPageScope.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.PrinterName = cboPrinterList.Text;

            this.PrintPageType = radioGroupCheck.SelectedIndex;

            int intTemp = 0;
            if (int.TryParse(speStartIndex.Text, out intTemp))
            {
                PrintStartIndex = intTemp;
            }

            string strPageScope = txtPageScope.Text.Trim();

            if (this.PrintPageType == 3)
            {
                if (string.IsNullOrEmpty(strPageScope))
                {
                    DialogBox.Msg("请指定打印页.");
                    return;
                }
                PrintPageScope = new List<string>();

                int pos = -1;
                string[] data = strPageScope.Split(',');
                foreach (string item in data)
                {
                    pos = item.IndexOf("-");
                    if (pos < 0)
                        PrintPageScope.Add(Convert.ToString(int.Parse(item) - 1));
                    else
                    {
                        int intStart = int.Parse(item.Substring(0, pos));
                        int intEnd = int.Parse(item.Substring(pos + 1));
                        for (int i = intStart; i <= intEnd; i++)
                        {
                            PrintPageScope.Add(Convert.ToString(i - 1));
                        }
                    }
                }
            }

            int intPageCopies = 0;
            string strPageCopys = this.spePageCopies.Text;
            int.TryParse(strPageCopys, out intPageCopies);
            if (intPageCopies < 1)
            {
                this.spePageCopies.Focus();
                DialogBox.Msg("请指定打印份数。", MessageBoxIcon.Information);
                return;
            }
            else
            {
                PrintPageCopies = intPageCopies;
            }

            //起始页
            //this.PrintStartIndex = 1;
            int intStartPage = PrintStartIndex;
            if (intStartPage > 1)
            {
                //判断页码
                if (radioGroupCheck.SelectedIndex == 2)//偶数页
                {
                    if (intStartPage > 2 && intStartPage % 2 == 1)//奇数
                    {
                        PrintStartIndex = PrintStartIndex - 1;
                    }
                }
                else if (radioGroupCheck.SelectedIndex == 3)//指定页
                {
                    int intPageStart = 0;
                    if (int.TryParse(PrintPageScope[0], out intPageStart) && intStartPage > intPageStart)
                    {
                        this.PrintStartIndex = intStartPage - intPageStart;
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrintPageSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmPrintPageSetting_Load(object sender, EventArgs e)
        {
            foreach (string item in PrinterSettings.InstalledPrinters)
            {
                cboPrinterList.Properties.Items.Add(item);
            }
            PrintDocument doc = new PrintDocument();
            cboPrinterList.Text = doc.PrinterSettings.PrinterName;
            doc = null;
            radioGroupCheck.SelectedIndex = 0;

            if (BlnPreformatted)
            {
                //套打
                radioGroupCheck.Enabled = false;
                spePageCopies.Enabled = false;
                speStartIndex.Enabled = false;
            }
        }

    }
}
