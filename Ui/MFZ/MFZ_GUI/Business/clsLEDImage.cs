using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    internal class clsLEDImage
    {

        private const string DeptArrangeFilePattern="MFZ_GUI_DEPT_ARRANGE_*.bmp";
        private const string PatientQueueFilePattern="MFZ_GUI_Patient_Queue_*.bmp";

        public clsLEDImage() { }

        /// <summary>
        /// 创建医生值班安排图片
        /// </summary>
        /// <param name="deptDoctors">医生安排的列表</param>
        internal void GeneratorDeptArrange(List<string> deptDoctors)
        {
            DeleteImages(DeptArrangeFilePattern);
            DrawImages(DeptArrangeFilePattern, deptDoctors);
        }

        /// <summary>
        /// 创建患者队列图片
        /// </summary>
        /// <param name="deptDoctors">患者队列</param>
        internal void GeneratorPatientQueue(List<string> doctorPatients)
        {
            DeleteImages(PatientQueueFilePattern);

            DrawImages(PatientQueueFilePattern, doctorPatients);
        }

        private void DrawImages(string filePattern, List<string> pages)
        {
            DrawImages(320, 112, filePattern, pages);
        }
        
        private void DrawImages(int width, int height, string filePattern, List<string> pages)
        {
            Font font = new Font("宋体", 9);
            DeleteImages(filePattern);

            for (int i = 0; i < pages.Count; i++)
            {
                GeneratorImage(width, height, font, pages[i], string.Format(filePattern.Replace("*", "{0}"), i));
            }
        }

        private void GeneratorImage(string content, Font font, string fileName)
        {
            GeneratorImage(320, 112, font, content, fileName);
        }
        
        private void GeneratorImage(int width, int height, Font font, string strContent, string fileName)
        {
            Bitmap bmp = new Bitmap(width + 2 + 10, height + 2);
            Graphics g = Graphics.FromImage(bmp);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;

            g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
            StringFormat sf = StringFormat.GenericDefault;

            g.DrawString(strContent, font, Brushes.Red, 0, 0);
            g.Dispose();

            string strFileName = string.Format(@".\LEDImage\{0}", fileName);
            try
            {
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                bmp.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                bmp.Dispose();
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogError(ex.Message);
            }
        }

        private void DeleteImages(string filePattern)
        {
            string[] imgFiles = System.IO.Directory.GetFiles(@".\LEDImage\", filePattern);
            foreach (string strFile in imgFiles)
            {
                try
                {
                    if (File.Exists(strFile))
                    {
                        File.Delete(strFile);
                    }
                }
                catch (Exception ex)
                {
                    new com.digitalwave.Utility.clsLogText().LogError(ex.Message);
                }
            }
        }

    }

    /// <summary>
    /// 显示医生项目名称得辅助类
    /// </summary>
    internal class clsDoctorDiscrible 
    {
        // 不显示医生职称的标志
        private const string VISBLE = ":FLAG";
        // 需要反转的标记
        private const string REVERSE= ":REVERSE";

        private string m_describle;

        /// <summary>
        /// 设置或获取是否显示医生职称
        /// </summary>
        public bool IsVisible
        {
            get { return !m_describle.Contains(VISBLE); }
            set
            {
                if (value)
                {
                    if (m_describle.Contains(VISBLE))
                    {
                        m_describle = m_describle.Replace(VISBLE, string.Empty);
                    }
                }
                else
                {
                    if (!m_describle.Contains(VISBLE))
                    {
                        m_describle += VISBLE;
                    }
                }
            }
        }
        /// <summary>
        /// 设置或获取是否显示反转
        /// </summary>
        public bool IsReverse
        {
            get { return m_describle.Contains(REVERSE); }
            set 
            {
                if (value)
                {
                    if (!m_describle.Contains(REVERSE))
                    {
                        m_describle += REVERSE;
                    }
                }
                else 
                {
                    if (!m_describle.Contains(REVERSE))
                    {
                        m_describle.Replace(REVERSE, string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="desc">描述</param>
        public clsDoctorDiscrible(string desc)
        {
            m_describle = desc;
        }

        /// <summary>
        /// 不包含标志位的描述
        /// </summary>
        public string Describle 
        {
            get { return m_describle.Replace(VISBLE, string.Empty).Replace(REVERSE,string.Empty); }
        }

        /// <summary>
        /// 包含标志位的描述，数据库钟存储的数据格式
        /// </summary>
        public string DbValue 
        {
            get{return m_describle;}
        }

        /// <summary>
        /// 设置显示的项目名称
        /// </summary>
        /// <param name="doctorStyle"></param>
        /// <returns></returns>
        public string GetDescrible(string doctorStyle) 
        {
            if (IsVisible)
            {
                if (IsReverse)
                {
                    return doctorStyle + Describle;
                }
                else
                {
                    return Describle + doctorStyle;
                }
            }
            else 
            {
                return Describle;
            }
        }
    }
}
