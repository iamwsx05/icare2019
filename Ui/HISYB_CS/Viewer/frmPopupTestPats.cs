using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmPopupTestPats : Form
    {
        public frmPopupTestPats()
        {
            InitializeComponent();
        }

        public string Dnh { get; set; }

        private void frmPopupTestPats_Load(object sender, EventArgs e)
        {
            List<EntityTestPat> data = new List<EntityTestPat>();
            data.Add(new EntityTestPat() { Dnh = "1170000018020628", Sfzh = "360728199503291311", Xm = "陈晓祥" });
            data.Add(new EntityTestPat() { Dnh = "1170000018183098", Sfzh = "522628198506164811", Xm = "欧益俊" });
            data.Add(new EntityTestPat() { Dnh = "1170000026462015", Sfzh = "422202199210254717", Xm = "聂亮" });
            data.Add(new EntityTestPat() { Dnh = "1170000026551881", Sfzh = "43102619811215302X", Xm = "欧爱燕" });
            this.gridControl1.DataSource = data;
        }

        private void frmPopupTestPats_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EntityTestPat vo = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as EntityTestPat;
            this.Dnh = vo.Dnh;
            this.DialogResult = DialogResult.OK;
        }
    }

    public class EntityTestPat
    {
        public string Dnh { get; set; }

        public string Sfzh { get; set; }

        public string Xm { get; set; }
    }

}
