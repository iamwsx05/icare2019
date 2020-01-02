using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace com.digitalwave.iCare.LIS
{
    /// <summary>
    /// 数据采集工具类
    /// </summary>
    public class clsDataAcquTool
    {
        /// <summary>
        /// 通过波形数据画图
        /// </summary>
        /// <param name="p_strGraphRawData">波形数据</param>
        /// <param name="p_strName">项目名称</param>
        /// <returns></returns>
        public static byte[] m_bytDecodeGraph(string p_strGraphRawData, string p_strName)
        {
            Bitmap bmp = new Bitmap(256, 150);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, 256, 150);
            g.DrawLine(Pens.Black, new Point(0, 135), new Point(256, 135));
            g.DrawLine(Pens.Black, new Point(1, 0), new Point(1, 135));
            Font fntName = new Font("Times New Roman", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 134);
            Font fntScale = new Font("Times New Roman", 9.5f, FontStyle.Regular, GraphicsUnit.Point, 134);
            g.DrawString(p_strName, fntName, Brushes.Black, (float)5f, (float)0f);
            g.DrawString("0", fntScale, Brushes.Black, (float)0f, (float)136f);
            string strItemName = p_strName;
            int intStep = 0;
            int intPoint = 0;

            switch (strItemName)
            {
                case "RBC":
                    g.DrawLine(Pens.Black, new Point(50, 130), new Point(50, 135));
                    g.DrawString("50", fntScale, Brushes.Black, (float)40f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(100, 130), new Point(100, 135));
                    g.DrawString("100", fntScale, Brushes.Black, (float)90f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(150, 130), new Point(150, 135));
                    g.DrawString("150", fntScale, Brushes.Black, (float)140f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(200, 130), new Point(200, 135));
                    g.DrawString("200", fntScale, Brushes.Black, (float)190f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(250, 130), new Point(250, 135));
                    g.DrawString("250fL", fntScale, Brushes.Black, (float)224f, (float)136f);

                    intPoint = 50;
                    intStep = 5;

                    break;

                case "WBC":
                    g.DrawLine(Pens.Black, new Point(62, 130), new Point(62, 135));
                    g.DrawString("100", fntScale, Brushes.Black, (float)50f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(124, 130), new Point(124, 135));
                    g.DrawString("200", fntScale, Brushes.Black, (float)110f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(186, 130), new Point(186, 135));
                    g.DrawString("300", fntScale, Brushes.Black, (float)170f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(248, 130), new Point(248, 135));
                    g.DrawString("400fL", fntScale, Brushes.Black, (float)224f, (float)136f);

                    intPoint = 50;
                    intStep = 5;
                    break;

                case "PLT":
                    g.DrawLine(Pens.Black, new Point(80, 130), new Point(80, 135));
                    g.DrawString("10", fntScale, Brushes.Black, (float)76f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(160, 130), new Point(160, 135));
                    g.DrawString("20", fntScale, Brushes.Black, (float)160f, (float)136f);
                    g.DrawLine(Pens.Black, new Point(240, 130), new Point(240, 135));
                    g.DrawString("30fL", fntScale, Brushes.Black, (float)230f, (float)136f);

                    intPoint = 40;
                    intStep = 6;
                    break;
            }

            int[] intDataArr = new int[intPoint];
            string strHex = null;
            for (int index = 0; index < intPoint; index++)
            {
                strHex = p_strGraphRawData.Substring(2 * index, 2);

                intDataArr[index] = Convert.ToInt32(strHex, 16);
            }
            int preY = 0;
            for (int i = 0; i < intPoint; i++)
            {
                int y = intDataArr[i] / 2;
                g.DrawLine(Pens.Black, new Point(i * intStep + 1, 135 - preY), new Point((i + 1) * intStep + 1, 135 - y));
                preY = y;
            }
            MemoryStream stream = new MemoryStream();
            bmp.Save(stream, ImageFormat.Gif);
            byte[] bytRes = stream.ToArray();
            stream.Close();
            bmp.Dispose();
            fntName.Dispose();
            fntScale.Dispose();
            return bytRes;
        }
    }
}
