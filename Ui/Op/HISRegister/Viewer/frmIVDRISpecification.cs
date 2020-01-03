using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms; 

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmIVDRISpecification : Form
    {
        public frmIVDRISpecification(Patient _patVo)
        {
            InitializeComponent();
            this.patVo = _patVo;
        }

        Patient patVo { get; set; }

        public void DrawIVDri(System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region Draw
            float currX = 0;
            float currY = 100;
            float diff = 40;
            int pageHeight = e.PageSettings.PaperSize.Height;
            int pageWidth = e.PageSettings.PaperSize.Width;
            System.Drawing.Font TextFont = new Font("宋体", 12);
            System.Drawing.Font TextFontUnderLine = new Font("宋体", 12, FontStyle.Underline, GraphicsUnit.Point);
            System.Drawing.Font TextWideFont = new Font("宋体", 18, FontStyle.Bold);

            int checkSignPosX = 2;
            int checkSignPosY = 2;
            List<string> lstSyz = new List<string>();
            string[] syzArr = new string[3];
            if (!string.IsNullOrEmpty(this.patVo.syzDesc))
            {
                syzArr = this.patVo.syzDesc.Split('^');
                if (syzArr[1].Trim() != "null")
                {
                    string[] ids = syzArr[1].Split(',');
                    lstSyz.AddRange(ids);
                }
            }
            else
            {
                syzArr[0] = "0";
                syzArr[1] = "";
                syzArr[2] = "";
            }
            string drawStr = "门诊患者静脉输液情况说明书";
            currX += ((float)pageWidth - e.Graphics.MeasureString(drawStr, TextWideFont).Width) / 2;
            e.Graphics.DrawString(drawStr, TextWideFont, Brushes.Black, currX, currY);

            currY += 50;
            currX = 120;
            drawStr = "患者姓名: ";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 5;
            drawStr = patVo.patientName;
            e.Graphics.DrawString(drawStr, TextFontUnderLine, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 10;
            drawStr = "性别: ";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 5;
            drawStr = patVo.sex;
            e.Graphics.DrawString(drawStr, TextFontUnderLine, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 10;
            drawStr = "年龄: ";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 5;
            drawStr = patVo.age + "岁";
            e.Graphics.DrawString(drawStr, TextFontUnderLine, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 10;
            drawStr = "诊疗卡号: ";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 5;
            drawStr = patVo.cardNo;
            e.Graphics.DrawString(drawStr, TextFontUnderLine, Brushes.Black, currX, currY);

            currX = 120;
            currY += diff;
            drawStr = "临床诊断: ";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 5;
            drawStr = patVo.diagDesc;
            e.Graphics.DrawString(drawStr, TextFontUnderLine, Brushes.Black, currX, currY);

            currX = 120;
            currY += diff;
            drawStr = "患者类型: ";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 10;
            drawStr = "□普通门诊";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (Convert.ToInt32(syzArr[0]) == 1) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 5;
            drawStr = "□急诊IV级";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (Convert.ToInt32(syzArr[0]) == 2) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 5;
            drawStr = "□急诊I、II、III级";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (Convert.ToInt32(syzArr[0]) == 3) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);

            currX = 120;
            currY += diff;
            drawStr = "静脉输液适应症:";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);

            diff = 28;  //24;             
            currY += diff;
            drawStr = "□ 补充血容量,改善微循环，维持血压。用于治疗烧伤、失血、休克等。";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (lstSyz.IndexOf("1") >= 0) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currY += diff;
            drawStr = "□ 补充水和电解质，以调节或维持酸碱平衡。用于各种原因引起的脱水、严重";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (lstSyz.IndexOf("2") >= 0) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currY += diff;
            drawStr = "呕吐、腹泻、大手术后、代谢性或呼吸性酸中毒等。";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX + 25, currY);
            currY += diff;
            drawStr = "□ 补充营养，维持热量，促进组织修复，获得正氦平衡。用于慢性消耗疾病、";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (lstSyz.IndexOf("3") >= 0) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currY += diff;
            drawStr = "禁食、不能经口提取食物、管饲不能得到足够营养等。";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX + 25, currY);
            currY += diff;
            drawStr = "□ 输入药物，以达到解毒、脱水利尿、维持血液渗透压、抗肿瘤等治疗。";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (lstSyz.IndexOf("4") >= 0) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currY += diff;
            drawStr = "□ 中重度感染需要静脉给予抗菌药物。";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (lstSyz.IndexOf("5") >= 0) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currY += diff;
            drawStr = "□ 经口服或肌注给药治疗无效的疾病。";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (lstSyz.IndexOf("6") >= 0) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currY += diff;
            drawStr = "□ 各种原因所致不适合胃肠道给药者。";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (lstSyz.IndexOf("7") >= 0) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            currY += diff;
            drawStr = "□ 因诊疗需要的特殊情况(需记录具体情况)";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            if (lstSyz.IndexOf("8") >= 0) e.Graphics.DrawString("√", TextFont, Brushes.Black, currX + checkSignPosX, currY - checkSignPosY);
            if (syzArr[2].Trim() != string.Empty)
            {
                currY += diff;
                drawStr = "     " + syzArr[2].Trim();
                e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            }
            currY += diff + 50;

            currX += 300;
            drawStr = "医生签名:";
            e.Graphics.DrawString(drawStr, TextFont, Brushes.Black, currX, currY);
            currX += e.Graphics.MeasureString(drawStr, TextFont).Width + 10;    //20;

            Image imgEmpSig = ImageSignature.GetEmpSigImage(patVo.doctName);
            if (imgEmpSig != null)
            {
                imgEmpSig = ImageSignature.pictureProcess(imgEmpSig, 579, 268);
                e.Graphics.DrawImage(imgEmpSig, currX, currY - 15, 80, 30);
                //e.Graphics.DrawImage(imgEmpSig, currX, currY - 20, 160, 60);
            }
            else
            {
                TextWideFont = new Font("宋体", 14, FontStyle.Bold);
                e.Graphics.DrawString(patVo.doctName, TextWideFont, Brushes.Black, currX, currY);
            }

            #endregion
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            this.DrawIVDri(e);
        }

        internal bool isSuccess = false;

        private void btnSave_Click(object sender, EventArgs e)
        {
            isSuccess = false;
            int patTypeId = 0;
            if (this.chkType1.Checked)
                patTypeId = 1;
            else if (this.chkType2.Checked)
                patTypeId = 2;
            else if (this.chkType3.Checked)
                patTypeId = 3;
            if (patTypeId == 0)
            {
                MessageBox.Show("请选择患者类型。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<int> lstSyz = new List<int>();
            if (this.chkSyz1.Checked)
                lstSyz.Add(1);
            if (this.chkSyz2.Checked)
                lstSyz.Add(2);
            if (this.chkSyz3.Checked)
                lstSyz.Add(3);
            if (this.chkSyz4.Checked)
                lstSyz.Add(4);
            if (this.chkSyz5.Checked)
                lstSyz.Add(5);
            if (this.chkSyz6.Checked)
                lstSyz.Add(6);
            if (this.chkSyz7.Checked)
                lstSyz.Add(7);
            if (this.chkSyz8.Checked)
                lstSyz.Add(8);
            if ((patTypeId == 1 || patTypeId == 2) && lstSyz.Count == 0)
            {
                MessageBox.Show("请选择适应症。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string syzId = string.Empty;
            if (lstSyz.Count > 0)
            {
                foreach (int item in lstSyz)
                {
                    syzId += item.ToString() + ",";
                }
                syzId = syzId.TrimEnd(',');
            }
            if (syzId == string.Empty) syzId = "null";

            string syzTsqk = lstSyz.IndexOf(8) >= 0 ? this.txtSyz_tsqk.Text.Trim() : string.Empty;
            if (lstSyz.IndexOf(8) >= 0 && syzTsqk == string.Empty)
            {
                MessageBox.Show("请填写特殊情况（具体情况）。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtSyz_tsqk.Focus();
                return;
            }

            this.patVo.syzDesc = patTypeId.ToString() + "^" + syzId + "^" + syzTsqk;
            if (this.patVo.syzDesc.Trim() != string.Empty)
            {
                clsDcl_DoctorWorkstation dcl = new clsDcl_DoctorWorkstation();
                if (dcl.UpdateRecipeIVDRIInstruction(this.patVo.recipeId, this.patVo.syzDesc))
                {
                    isSuccess = true;
                    MessageBox.Show("保存成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.printPreviewControl.InvalidatePreview();
                }
                else
                {
                    MessageBox.Show("保存失败。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请填写：静脉输液适应症", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printDocument.Print();
        }

        private void chkType1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkType1.Checked)
            {
                this.chkType2.Checked = false;
                this.chkType3.Checked = false;
            }
        }

        private void chkType2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkType2.Checked)
            {
                this.chkType1.Checked = false;
                this.chkType3.Checked = false;
            }
        }

        private void chkType3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkType3.Checked)
            {
                this.chkType1.Checked = false;
                this.chkType2.Checked = false;
            }
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            this.printDocument.Print();
        }

        private void btnClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class Patient
    {
        public string recipeId { get; set; }
        public string cardNo { get; set; }
        public string patientName { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string diagDesc { get; set; }
        public string syzDesc { get; set; }
        public string doctCode { get; set; }
        public string doctName { get; set; }
    }

    #region 图片签名.电子签名
    /// <summary>
    /// 图片签名.电子签名
    /// </summary>
    public class ImageSignature
    {
        public static Image GetEmpSigImage(string empName)
        {
            if (string.IsNullOrEmpty(empName) || empName.Trim() == "")
                return null;
            //using (clsQueryCheckResultSvc svc = (clsQueryCheckResultSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsQueryCheckResultSvc)))
            //{
                byte[] signData = (new weCare.Proxy.ProxyLis03()).Service.GetEmpSign(empName);
                if (signData != null)
                {
                    MemoryStream ms = new MemoryStream(signData);
                    return Image.FromStream(ms);
                }
            //}
            return null;
        }

        public static Image pictureProcess(Image sourceImage, int targetWidth, int targetHeight)
        {
            int width;//图片最终的宽
            int height;//图片最终的高
            try
            {
                System.Drawing.Imaging.ImageFormat format = sourceImage.RawFormat;
                Bitmap targetPicture = new Bitmap(targetWidth, targetHeight);
                Graphics g = Graphics.FromImage(targetPicture);
                g.Clear(Color.White);

                //计算缩放图片的大小
                if (sourceImage.Width > targetWidth && sourceImage.Height <= targetHeight)
                {
                    width = targetWidth;
                    height = (width * sourceImage.Height) / sourceImage.Width;
                }
                else if (sourceImage.Width <= targetWidth && sourceImage.Height > targetHeight)
                {
                    height = targetHeight;
                    width = (height * sourceImage.Width) / sourceImage.Height;
                }
                else if (sourceImage.Width <= targetWidth && sourceImage.Height <= targetHeight)
                {
                    width = sourceImage.Width;
                    height = sourceImage.Height;
                }
                else
                {
                    width = targetWidth;
                    height = (width * sourceImage.Height) / sourceImage.Width;
                    if (height > targetHeight)
                    {
                        height = targetHeight;
                        width = (height * sourceImage.Width) / sourceImage.Height;
                    }
                }
                g.DrawImage(sourceImage, (targetWidth - width) / 2, (targetHeight - height) / 2, width, height);
                sourceImage.Dispose();

                return targetPicture;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
    #endregion
}
