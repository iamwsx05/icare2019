using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using Common.Entity;

namespace Common.Controls
{
    public partial class frmNew : frmBasePopup
    {
        string id { get; set; }
        string code { get; set; }
        string name { get; set; }
        DataTable dt { get; set; }

        class EntityData
        {
            public int check { get; set; }
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
        }

        public List<int> lstNo { get; set; }
        List<EntityData> dataSource = null;

        public frmNew(DataTable _dt, string _id, string _code, string _name)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                dt = _dt;
                id = _id;
                code = _code;
                name = _name; 
            }
        }

        private void frmNew_Load(object sender, EventArgs e)
        {
            EntityData vo = null;
            dataSource = new List<EntityData>();
            foreach (DataRow dr in dt.Rows)
            {
                vo = new EntityData();
                vo.id = dr[id].ToString();
                vo.code = dr[code].ToString();
                vo.name = dr[name].ToString();
                dataSource.Add(vo);
            }
            this.gcData.DataSource = dataSource;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            lstNo = new List<int>();
            if (dataSource != null && dataSource.Count > 0)
            {
                for (int i = 0; i < dataSource.Count; i++)
                {
                    if (dataSource[i].check == 1)
                    {
                        lstNo.Add(i);
                    }
                }
            }
            if (lstNo.Count > 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogBox.Msg("请选择项目。");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
