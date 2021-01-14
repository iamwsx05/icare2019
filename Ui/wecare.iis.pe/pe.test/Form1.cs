using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using pe.test.ps.svc;
using System.IO;
using weCare.Core.Dac;
using weCare.Core.Utils;


namespace pe.test
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        Wecare.Svc.PeService svc = new Wecare.Svc.PeService();

        private void btn900100_Click(object sender, EventArgs e)
        {

            string xmlIn = string.Empty;
            //xmlIn = "<Request>";
            xmlIn += "<funcCode>900100</funcCode>";
            //xmlIn += "</Request>";

            this.memRes.Text = svc.CallFunc(xmlIn);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string xmlIn = string.Empty;
            xmlIn = "<funcCode>C00300</funcCode><regNo>123456789</regNo>";

            this.memRes.Text = svc.CallFunc(xmlIn);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int ret = -1;
            try 
            {
                //FileStream fs = new FileStream(@"C:\Users\Administrator\Desktop\jpg\DSC_0625.JPG", FileMode.Open, FileAccess.Read);
                Byte[] btye2 = null ;
                Byte[] btye3 = null;
                //fs.Read(btye2, 0, Convert.ToInt32(fs.Length));
                //fs.Close();
                btye2 = Function.ConvertImageToByte(System.Drawing.Image.FromFile(@"C:\Users\Administrator\Desktop\jpg\zp3\0320zp.jpg"), 4);
                btye3 = Function.ConvertImageToByte(System.Drawing.Image.FromFile(@"C:\Users\Administrator\Desktop\jpg\zp3\1.jpg"), 4);
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                string sql = "update opRegSchedulingDoct set doctPhoto = ? ,face = ? where doctCode = '1063'";
                //string sql = "update opRegSchedulingDoct set doctPhoto = ? where doctCode = '1063'";

                IDataParameter[] parm = svc.CreateParm(2);
                parm[0].Value = btye2;
                parm[1].Value = btye3;
                ret = svc.ExecSql(sql, parm);
            }
            catch(Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }

            if(ret >= 0)
            {
                MessageBox.Show("成功");
            }
            else
            {
                MessageBox.Show("失败");
            }
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            string sql = "select face,doctPhoto from  opRegSchedulingDoct  where doctCode = '1063'";

            DataTable dt =  svc.GetDataTable(sql);

            if(dt != null && dt.Rows.Count > 0)
            {
                this.pictureEdit1.Image = Function.ConvertObjectToImage(dt.Rows[0]["face"]);
                this.pictureEdit2.Image = Function.ConvertObjectToImage(dt.Rows[0]["doctPhoto"]);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            //string sql = "select face,doctPhoto from  opRegSchedulingDoct  where doctCode = '1063'";

            //DataTable dt = svc.GetDataTable(sql);

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    this.pictureEdit1.Image = Function.ConvertByteToImage((byte[])dt.Rows[0]["face"]);
            //    this.pictureEdit2.Image = Function.ConvertByteToImage((byte[])dt.Rows[0]["doctPhoto"]);
            //    this.memBase64Str1.Text = Function.ConvertImageToBase64String(Function.ConvertByteToImage((byte[])dt.Rows[0]["face"]));
            //    this.memBase64Str2.Text = Function.ConvertImageToBase64String(Function.ConvertByteToImage((byte[])dt.Rows[0]["doctPhoto"]));
            //    this.pictureEdit4.Image = Function.ConvertBase64StringToImage(this.memBase64Str1.Text);
            //    this.pictureEdit3.Image = Function.ConvertBase64StringToImage(this.memBase64Str2.Text);
            //}
            this.memImageStr.Text = ImgToBase64String("C:\\Users\\Administrator\\Desktop\\jpg\\zp1.jpg");


        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            //this.memBase64Str1.Text = "";
            //this.memBase64Str2.Text = "";
            //this.memBase64Str1.Text = ReadTxtContent("C:\\Users\\Administrator\\Desktop\\0305.pe.iis\\123jpg.txt");
            //this.memBase64Str2.Text = ReadTxtContent("C:\\Users\\Administrator\\Desktop\\0305.pe.iis\\321jpg.txt");

            

            try
            {
                string base64String = "/9j/4AAQSkZJRgABAgAAAQABAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCAG5AWYDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD36iiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiimswVSzEADuaAHUV594s+LGjeHX+z";




                //string dummyData = base64String.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
                //if (dummyData.Length % 4 > 0)
                //{
                //    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
                //}


                base64String = base64String.Replace("+", "%2B");


                this.pictureEdit1.Image = Function.ConvertBase64StringToImage(base64String) ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            //this.pictureEdit3.Image = Function.ConvertBase64StringToImage(this.memBase64Str2.Text);
        }

        /// <summary>
        /// 读取txt文件内容
        /// </summary>
        /// <param name="Path">文件地址</param>
        public string ReadTxtContent(string Path)
        {
            StreamReader sr = new StreamReader(Path, Encoding.Default);
            string content = string.Empty;
            content = sr.ReadLine();

            return content;
        }

        public string ImgToBase64String(string Imagefilename)
        {
            String strbaser64 = string.Empty;
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                FileStream fs = new FileStream(Imagefilename + ".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                strbaser64 = Convert.ToBase64String(arr);

                // MessageBox.Show("转换成功!");
            }
            catch (Exception ex)
            {

            }

            return strbaser64;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FaceReco_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            FaceReco.YoutuFace faceReco = new FaceReco.YoutuFace();
            //str = faceReco.DetectFace("C:\\Users\\Administrator\\Desktop\\jpg\\zp.jpg", 1);
            str = faceReco.FaceCompare("C:\\Users\\Administrator\\Desktop\\jpg\\11-1.jpg", "C:\\Users\\Administrator\\Desktop\\jpg\\22-1.jpg");
            memRes.Text = str;
        }
    }
}
