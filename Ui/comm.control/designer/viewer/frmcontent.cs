using Common.Controls;
using Common.Entity;
using System;
using System.Windows.Forms;

namespace Common.Controls
{
    /// <summary>
    /// 数据内容--数据定义
    /// </summary>
    public partial class frmContent : frmBasePopup
    {
        /// <summary>
        /// 构造
        /// </summary>
        public frmContent()
        {
            InitializeComponent(); 
        }

        /// <summary>
        /// 数据定义值
        /// </summary>
        public string DataConfig { get; set; }

        /// <summary>
        /// 临时数据
        /// </summary>
        string TempDataConfig = string.Empty;

        private void frmContent_Load(object sender, EventArgs e)
        {
            TempDataConfig = DataConfig;
            this.txtConfig.Text = DataConfig;
        }

        private void frmContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (!string.IsNullOrEmpty(TempDataConfig) && TempDataConfig != this.txtConfig.Text)
                {
                    if(DialogBox.Msg("数据已更改，是否保存？", MessageBoxIcon.Question)== System.Windows.Forms.DialogResult.Yes)
                    {
                        btnOk_Click(null, null);
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataConfig = this.txtConfig.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
