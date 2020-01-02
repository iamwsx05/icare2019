using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// 液晶电视诊室控件 by kenny
    /// </summary>
    public partial class ctlLCDRoomElement : UserControl
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ctlLCDRoomElement()
        {
            InitializeComponent();
        }
        #endregion

        #region 初始化窗体
        /// <summary>
        /// 病人队列数据
        /// </summary>
        List<weCare.Core.Entity.clsMFZPatientVO> lstPatients = null;
        Timer timer = new Timer();
        private void ctlLCDRoomElement_Load(object sender, EventArgs e)
        {
            // 扩大listview行高
            int n = Convert.ToInt32((this.tableLayoutPanel1.RowStyles[2].Height / 100) * this.tableLayoutPanel1.Height / 4);
            this.tableLayoutPanel5.Height = n;            

            // 根据控件比例,重新设置列宽
            this.columnHeader2.Width = Convert.ToInt32((float)0.38 * this.listView1.Width);
            this.columnHeader3.Width = Convert.ToInt32((float)0.58 * this.listView1.Width);

            // 确定字体
            float intfontw = (float)(this.columnHeader2.Width * 0.8 * 0.75 / 3);
            Font newfont = new Font("宋体", intfontw, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            float intright = (float)(this.columnHeader3.Width * 0.8 * 0.75 / 4);
            intright = (intright > (float)(0.55 * n) ? (float)(0.55 * n) : intright);

            // 列头 [序号] 与columnHeader2 列宽一致
            this.tableLayoutPanel5.ColumnStyles[0].Width = this.columnHeader2.Width;

            // 用imagelist扩大行高            
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, Convert.ToInt32(n * 0.9));
            this.listView1.SmallImageList = imgList;

            // 设置字体
            this.listView1.Font = newfont;
            this.label1.Font = new Font("宋体", intright, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134))); ;
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.label2.Font = new Font("宋体", intright, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134))); ;
            this.label2.TextAlign = ContentAlignment.MiddleCenter;

            //this.listView1.Items[0].SubItems.Add("");
            this.listView1.Items[0].SubItems[1].Font = newfont;
            this.listView1.Items[0].SubItems[2].Font = newfont;
            this.listView1.Items[1].SubItems.Add("");
            this.listView1.Items[2].SubItems.Add("");

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 500;
            //timer.Start();
        }
        #endregion

        #region 计时器
        /// <summary>
        /// 第一位病人(闪动)
        /// </summary>
        private string m_strFirstPatient = string.Empty;
        /// <summary>
        /// 其他病人(滚动)
        /// </summary>
        private string m_strOthPatient = string.Empty;
        /// <summary>
        /// 延时
        /// </summary>
        int iTick = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            // 闪动显示效果
            if (iTick == 2)
            {
                if (this.listView1.Items[0].SubItems[2].Text != "")
                {
                    this.listView1.Items[0].SubItems[2].Text = "";
                }
                else
                {
                    this.listView1.Items[0].SubItems[2].Text = m_strFirstPatient;
                }
                iTick = 0;
            }

            // 滚动显示效果            
            if (m_strOthPatient.Trim() != "")
            {
                string temp = m_strOthPatient.Substring(0, 1);
                m_strOthPatient = m_strOthPatient.Substring(1, m_strOthPatient.Length - 1) + temp;
                this.listView1.Items[3].SubItems[2].Text = m_strOthPatient;
            }
            iTick++;
        }
        #endregion

        #region 动态属性控件
        /// <summary>
        /// 诊室名称
        /// </summary>
        public string m_strRoomName
        {
            set
            {
                if (this.lblRoomName.Text != value)
                {
                    this.lblRoomName.Text = value;
                    Graphics g = this.CreateGraphics();
                    // 单元格宽度
                    int width = this.gradientPanel3.Width;
                    // 设置字体
                    Font font = new Font("宋体", (float)(0.36 * 0.75 * width), FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    // 计算出高度
                    int height = Convert.ToInt32(g.MeasureString(m_lenValue(value), font).Width);

                    this.lblRoomName.Width = Convert.ToInt32(width * 0.36);
                    this.lblRoomName.Height = Convert.ToInt32(height);
                    this.lblRoomName.Font = font;
                    // 控件左上角坐标
                    int intX = Convert.ToInt32(width * 0.32);
                    int intY = Convert.ToInt32(this.gradientPanel3.Height * 0.06);
                    this.lblRoomName.Location = new Point(intX, intY);
                }
            }
            get
            {
                return this.lblRoomName.Text;
            }
        }

        private string m_lenValue(string input)
        {
            int n = input.Length;
            string strtmp = string.Empty;
            for (int i1 = 0; i1 < n; i1++)
            {
                strtmp += "藏";
            }
            return strtmp;
        }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string m_strDeptName
        {
            set
            {
                if (this.lblDeptName.Text != value)
                {
                    this.lblDeptName.Text = value;
                    float fltheight = 0;
                    if (this.gradientPanel1.Width >260)
                    {
                        fltheight = this.gradientPanel2.Height * 0.5f * 0.75f;
                    }
                    //by huafeng.xiao
                    //当屏幕切换到4个诊室或以上时，科室医生姓名根据高度来算，是不太恰当的，这里改成根据控件的宽度来算
                    else
                    {
                        fltheight = this.gradientPanel1.Width * 0.13f;
                    }
                    int height = this.gradientPanel1.Height;
                    int intX = Convert.ToInt32(this.gradientPanel1.Width * 0.05);
                    int intY = Convert.ToInt32(height * 0.2);
                    //this.lblDeptName.Font = new Font("宋体", (float)(0.6 * 0.75 * height), FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    this.lblDeptName.Font = new Font("宋体", fltheight, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    this.lblDeptName.Location = new Point(intX, intY);
                }
            }
            get
            {
                return this.lblDeptName.Text;
            }
        }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string m_strDocName
        {
            set
            {
                if (this.lblDocName.Text != value)
                {
                    this.lblDocName.Text = value;
                    float  fltheight =0;
                    if (this.gradientPanel2.Width >300)
                    {
                        fltheight = this.gradientPanel2.Height *0.6f * 0.75f ;
                    }
                    //by huafeng.xiao
                    //当屏幕切换到4个诊室或以上时，科室医生姓名根据高度来算，是不太恰当的，这里改成根据控件的宽度来算
                    else
                    {
                        fltheight = this.gradientPanel2.Width * 0.17f;
                    }
                    int intX = Convert.ToInt32(this.gradientPanel2.Width * 0.05);
                    int intY = Convert.ToInt32(this.gradientPanel2.Height * 0.2);
                    //this.lblDocName.Font = new Font("宋体", (float)(0.6 * 0.75 * height), FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    this.lblDocName.Font = new Font("宋体", fltheight, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    this.lblDocName.Location = new Point(intX, intY);
                }
            }
            get
            {
                return this.lblDocName.Text;
            }
        }

        /// <summary>
        /// 医生职称
        /// </summary>
        public string m_strDocType
        {
            set
            {
                if (this.lblDocType.Text != value)
                {
                    this.lblDocType.Text = value;
                    //Graphics g = this.CreateGraphics();
                    // 设置字体
                  //  int height = this.gradientPanel4.Height;
                  //  Font font = new Font("宋体", (float)(0.6 * 0.75 * height), FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    float fltheight = 0;
                    if (this.gradientPanel4.Width > 210)
                    {
                        fltheight = this.gradientPanel4.Height * 0.6f * 0.75f;
                    }
                    //by huafeng.xiao
                    //当屏幕切换到4个诊室或以上时，科室医生姓名根据高度来算，是不太恰当的，这里改成根据控件的宽度来算
                    else
                    {
                        fltheight = this.gradientPanel4.Width * 0.13f;
                    }
                    this.lblDocType.Font = new Font("宋体", fltheight, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                  //  this.lblDocType.Font = font;

                    // 控件左上角坐标
                    //int width = g.MeasureString(value, font).Width;
                    int width = this.lblDocType.Width;
                    int intX = this.gradientPanel4.Width - width - 2;
                    int intY = Convert.ToInt32(this.gradientPanel4.Height * 0.2);
                    this.lblDocType.Location = new Point(intX, intY);
                }
            }
            get
            {
                return this.lblDocType.Text;
            }
        }

        /// <summary>
        /// 候诊人数
        /// </summary>
        public string m_strWaitCount
        {
            set
            {
                int height = this.gradientPanel5.Height;
                // 候诊
                float fltheight = 0;
                if (this.gradientPanel5.Width > 150)
                {
                    fltheight = this.gradientPanel5.Height * 0.6f * 0.75f;
                }
                //by huafeng.xiao
                //当屏幕切换到4个诊室或以上时，科室医生姓名根据高度来算，是不太恰当的，这里改成根据控件的宽度来算
                else
                {
                    fltheight = this.gradientPanel5.Width * 0.12f;
                }
                Font font = new Font("宋体", fltheight, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
               // Font font = new Font("宋体", (float)(0.4 * 0.75 * height), FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                this.lblHz.Font = font;
                this.lblHz.Location = new Point(3, Convert.ToInt32(0.3 * height));                

                // 数目
                int intX = this.lblHz.Width + 5;
               // Font fontNumeric = new Font("Arial", (float)(0.4 * 0.75 * height), FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                Font fontNumeric = new Font("Arial", fltheight, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                this.lblCount.Text = value;
                this.lblCount.Font = fontNumeric;
                this.lblCount.Location = new Point(intX, Convert.ToInt32(0.28 * height));

                // 人
                this.lblR.Font = font;
                int tmpX = intX + this.lblCount.Width;
                this.lblR.Location = new Point(tmpX, Convert.ToInt32(0.3 * height));
            }
            get
            {
                return this.lblCount.Text;
            }
        }
        #endregion

        #region 病人队列
        /// <summary>
        /// 病人队列
        /// </summary>
        public List<weCare.Core.Entity.clsMFZPatientVO> m_lstPatients
        {
            set
            {
                this.lstPatients = value;
                this.listView1.Items[0].SubItems[2].Text = "";
                for (int i1 = 0; i1 < 3; i1++)
                {
                    this.listView1.Items[i1].SubItems[2].Text = "";

                    if (i1 < value.Count)
                    {
                        if (i1 == 0) //当前呼叫
                        {
                            this.m_strFirstPatient = value[i1].m_strPatientName;
                            this.m_strOthPatient = "";
                        }
                        //if (i1 == 3) //其他
                        //{
                        //    string strOthPatient = "";
                        //    for (int i2 = 3; i2 < value.Count; i2++)
                        //    {
                        //        strOthPatient += value[i2].m_strPatientName + " ";
                        //    }
                        //    this.m_strOthPatient = strOthPatient;
                        //    this.listView1.Items[i1].SubItems[2].Text = strOthPatient;
                        //    break; // 强行退出循环
                        //}

                        this.listView1.Items[i1].SubItems[2].Text = value[i1].m_strPatientName;
                    }
                }
                // 控制计时器
                //if (this.m_strFirstPatient != "" && this.timer.Enabled == false)
                //{
                //    this.timer.Start();
                //}
                //else if (this.m_strFirstPatient == "" && this.timer.Enabled)
                //{
                //    this.timer.Stop();
                //}
            }
            get
            {
                return this.lstPatients;
            }
        }

        /// <summary>
        /// 重画listView病人列表
        /// </summary>
        public void m_mthSetlistViewReDraw()
        {
            if (this.lstPatients.Count == 0)
            {
                this.listView1.Items[0].SubItems[2].Text = "";
                this.listView1.Items[1].SubItems[2].Text = "";
                this.listView1.Items[2].SubItems[2].Text = "";
                //this.listView1.Items[3].SubItems[2].Text = "";
                if (this.timer.Enabled)
                    this.timer.Stop();

                return;
            }

            this.m_strFirstPatient = string.Empty;
            this.m_strOthPatient = string.Empty;
            for (int i1 = 0; i1 < 3; i1++)
            {
                this.listView1.Items[i1].SubItems[2].Text = "";

                if (i1 < this.lstPatients.Count)
                {
                    if (i1 == 0) //当前呼叫
                    {
                        this.m_strFirstPatient = this.lstPatients[i1].m_strPatientName;
                    }
                    //if (i1 == 3) //更多
                    //{
                    //    string strOthPatient = "";
                    //    for (int i2 = 3; i2 < this.lstPatients.Count; i2++)
                    //    {
                    //        strOthPatient += this.lstPatients[i2].m_strPatientName + " ";
                    //    }
                    //    this.m_strOthPatient = strOthPatient;
                    //    this.listView1.Items[i1].SubItems[2].Text = strOthPatient;
                    //    break; // 强行退出循环
                    //}

                    this.listView1.Items[i1].SubItems[2].Text = this.lstPatients[i1].m_strPatientName;
                }
            }
            // 控制计时器
            //if (this.m_strFirstPatient != "" && this.timer.Enabled == false)
            //{
            //    this.timer.Start();
            //}
            //else if (this.m_strFirstPatient == "" && this.timer.Enabled)
            //{
            //    this.timer.Stop();
            //}
        }
        #endregion

        #region 呼叫特定病人
        /// <summary>
        /// 呼叫特定病人
        /// 优先特定病人顺序，原队列往后退一位
        /// </summary>
        public void m_mthCallSomePatient(weCare.Core.Entity.clsMFZPatientVO callpatient)
        {
            for (int i1 = 0; i1 < 3; i1++)
            {
                if (i1 == 0)
                {
                    this.m_strFirstPatient = callpatient.m_strPatientName;
                    this.listView1.Items[i1].SubItems[2].Text = callpatient.m_strPatientName;
                    continue;
                }

                if (i1 < this.lstPatients.Count+1)
                {
                    int n = i1 - 1;
                    //if (i1 == 3)
                    //{
                    //    string strOthPatient = "";
                    //    for (int i2 = n; i2 < this.lstPatients.Count; i2++)
                    //    {
                    //        strOthPatient += this.lstPatients[i2].m_strPatientName + " ";
                    //    }
                    //    this.m_strOthPatient = strOthPatient;
                    //    this.listView1.Items[i1].SubItems[2].Text = strOthPatient;
                    //    break; // 强行退出循环
                    //}

                    this.listView1.Items[i1].SubItems[2].Text = this.lstPatients[n].m_strPatientName;
                }
            }

            // 控制计时器
            //if (this.m_strFirstPatient != "" && this.timer.Enabled == false)
            //{
            //    this.timer.Start();
            //}
            //else if (this.m_strFirstPatient == "" && this.timer.Enabled)
            //{
            //    this.timer.Stop();
            //}
        }
        #endregion
    }
}
