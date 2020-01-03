using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Runtime.Serialization;
using weCare.Core.Entity;
using System.Runtime.Serialization.Formatters.Binary;
using com.digitalwave.iCare.middletier.HI;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmRecipeBrowser : Form
    {
        public frmRecipeBrowser()
        {
            InitializeComponent();
        }

        BindingSource bs = new BindingSource();
        DataTable dtFile = new DataTable();
        string CurrFileName { get; set; }
        clsCalcPatientCharge objCalPatientCharge = null;

        private void frmRecipeBrowser_Load(object sender, EventArgs e)
        {
            dtFile.Columns.Add("fileName", typeof(string));
            dtFile.Columns.Add("fullFile", typeof(string));
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "处方文件|*.emr";
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] sarr = openFileDialog.FileNames;
                string[] ssplit = null;
                string fileName = string.Empty;
                foreach (string fullFile in sarr)
                {
                    ssplit = fullFile.Split('\\');
                    fileName = ssplit[ssplit.Length - 1];

                    DataRow dr1 = dtFile.NewRow();
                    dr1[0] = fileName;
                    dr1[1] = fullFile;
                    dtFile.LoadDataRow(dr1.ItemArray, true);
                }
                bs.DataSource = dtFile;
                this.lstRecipe.Items.Clear();
                this.lstRecipe.DataSource = bs;
                this.lstRecipe.DisplayMember = "fileName";
                this.lstRecipe.ValueMember = "fullFile";
            }
        }

        private void lstRecipe_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CurrFileName = string.Empty;
            DataRowView dr = (DataRowView)this.lstRecipe.SelectedItem;
            if (dr != null)
            {
                CurrFileName = dr.Row[1].ToString();
                this.myPrintPreViewControl1.Document = this.printDocument;
            }
        }

        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrFileName))
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(CurrFileName, FileMode.Open);
                clsOutpatientPrintRecipe_VO recipePrtVo = (clsOutpatientPrintRecipe_VO)formatter.Deserialize(stream);
                stream.Close();

                this.objCalPatientCharge = new clsCalcPatientCharge("", "", 0, "东莞市茶山医院", 0, 0);
                clsRecipeType_VO recipeTypeVo = new clsRecipeType_VO();
                recipeTypeVo.REMARK_VCHR = "";
                recipeTypeVo.TYPENAME_VCHR = recipePrtVo.m_strRecordEmpID;
                this.objCalPatientCharge.RecipeTypeInfo = recipeTypeVo;
                this.objCalPatientCharge.PrintRecipeVOInfo = recipePrtVo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;

        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (this.objCalPatientCharge != null)
                this.objCalPatientCharge.m_mthPrintRecipe(e, "1");
        }
    }

}
