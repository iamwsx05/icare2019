using System;
using System.Drawing;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsPrintRecipeDetail
    {
        public clsPrintRecipeDetail(System.Drawing.Printing.PrintPageEventArgs p_obj, clsOutpatientPrintRecipe_VO VO)
        {
            objDraw = p_obj;
            objFontTitle = new Font("SimSun", 16, FontStyle.Bold);
            objFontNormal = new Font("SimSun", 10);
            obj_VO = VO;
        }
        /// <summary>
        /// ��ͼ����
        /// </summary>
        private System.Drawing.Printing.PrintPageEventArgs objDraw;
        private clsOutpatientPrintRecipe_VO obj_VO;
        /// <summary>
        /// ��������
        /// </summary>
        Font objFontTitle;
        /// <summary>
        /// ��������
        /// </summary>
        Font objFontNormal;
        /// <summary>
        /// ��߾�
        /// </summary>
        float fltLeftIndentProp = 0.07f;

        /// <summary>
        /// �м��
        /// </summary>
        private float fltRowHeight = 23F;
        /// <summary>
        /// ������
        /// </summary>
        private float Y = 46F;

        #region ��ӡ����
        /// <summary>
        /// ��ӡ���� 
        /// </summary>
        /// <param name="p_blnDiffOn">�Ƿ��ӡҩƷ������</param>
        private void m_mthPrintTitle(bool p_blnDiffOn)
        {
            //����
            SizeF objFontSize = objDraw.Graphics.MeasureString(obj_VO.m_strHospitalName + "�����շ���ϸ", this.objFontTitle);
            float X = (this.objDraw.PageBounds.Width - objFontSize.Width) / 2;
            Y = this.objDraw.PageBounds.Height * 0.047f - (objFontSize.Height / 2);
            objDraw.Graphics.DrawString(obj_VO.m_strHospitalName + "�����շ���ϸ", objFontTitle, Brushes.Black, X, Y);
            Y += objFontSize.Height + 2;
            //����
            float RX = X + objFontSize.Width;
            objFontSize = objDraw.Graphics.MeasureString("��", this.objFontNormal);
            fltRowHeight = objFontSize.Height;

            using (Pen p = new Pen(Color.Black, 1))
            {
                objDraw.Graphics.DrawLine(p, X, Y, RX, Y);
            }
            //����ʱ��
            Y += this.fltRowHeight / 2;
            X = this.objDraw.PageBounds.Width * fltLeftIndentProp;
            objFontSize = objDraw.Graphics.MeasureString("����ʱ��:", this.objFontNormal);
            objDraw.Graphics.DrawString("����ʱ��:", objFontNormal, Brushes.Black, X, Y);
            X += objFontSize.Width + 2;
            objDraw.Graphics.DrawString(obj_VO.m_strPrintDate, objFontNormal, Brushes.Black, X, Y);
            //���˿���
            X = this.objDraw.PageBounds.Width * 0.357f;
            objFontSize = objDraw.Graphics.MeasureString("���˿���:", this.objFontNormal);
            objDraw.Graphics.DrawString("���˿���:", objFontNormal, Brushes.Black, X, Y);
            X += objFontSize.Width - 2;
            objDraw.Graphics.DrawString(obj_VO.m_strCardID, objFontNormal, Brushes.Black, X, Y);
            //����ID
            X = this.objDraw.PageBounds.Width * 0.64f;
            objFontSize = objDraw.Graphics.MeasureString("��Ʊ����:", this.objFontNormal);
            objDraw.Graphics.DrawString("��Ʊ����:", objFontNormal, Brushes.Black, X, Y);
            X += objFontSize.Width;
            objDraw.Graphics.DrawString(obj_VO.strInvoiceNO, objFontNormal, Brushes.Black, X, Y);
            //����
            X = this.objDraw.PageBounds.Width * fltLeftIndentProp;
            Y += this.fltRowHeight;
            objFontSize = objDraw.Graphics.MeasureString("��������:", this.objFontNormal);
            objDraw.Graphics.DrawString("��������:", objFontNormal, Brushes.Black, X, Y);
            X += objFontSize.Width + 2;
            objDraw.Graphics.DrawString(obj_VO.m_strPatientName, objFontNormal, Brushes.Black, X, Y);
            //�շ�Ա
            X = this.objDraw.PageBounds.Width * fltLeftIndentProp;
            objFontSize = objDraw.Graphics.MeasureString("�շ�Ա :", this.objFontNormal);
            objDraw.Graphics.DrawString("�� �� Ա:", objFontNormal, Brushes.Black, X, Y + fltRowHeight);
            X += objFontSize.Width + 2;
            objDraw.Graphics.DrawString(obj_VO.strCheckOutName, objFontNormal, Brushes.Black, X, Y + fltRowHeight);
            //�Ա�
            X = this.objDraw.PageBounds.Width * 0.357f;
            objFontSize = objDraw.Graphics.MeasureString("��������:", this.objFontNormal);
            objDraw.Graphics.DrawString("��������:", objFontNormal, Brushes.Black, X, Y);
            X += objFontSize.Width - 2;
            objDraw.Graphics.DrawString(obj_VO.m_strPatientType, objFontNormal, Brushes.Black, X, Y);
            //������λ
            X = this.objDraw.PageBounds.Width * 0.357f;
            objFontSize = objDraw.Graphics.MeasureString("������λ:", this.objFontNormal);
            objDraw.Graphics.DrawString("������λ:", objFontNormal, Brushes.Black, X, Y + fltRowHeight);
            X += objFontSize.Width - 2;
            objDraw.Graphics.DrawString(obj_VO.m_strEmployer, objFontNormal, Brushes.Black, X, Y + fltRowHeight);
            //����ID
            X = this.objDraw.PageBounds.Width * 0.64f;
            objFontSize = objDraw.Graphics.MeasureString("ҽ��֤��:", this.objFontNormal);
            objDraw.Graphics.DrawString("ҽ��֤��:", objFontNormal, Brushes.Black, X, Y);
            objDraw.Graphics.DrawString("����ҽ��:", objFontNormal, Brushes.Black, X, Y + fltRowHeight);
            X += objFontSize.Width + 2;
            objDraw.Graphics.DrawString(obj_VO.m_strINSURANCEID, objFontNormal, Brushes.Black, X, Y);
            objDraw.Graphics.DrawString(obj_VO.m_strDiagDrName, objFontNormal, Brushes.Black, X, Y + fltRowHeight);

            Y += this.fltRowHeight * 3;
            X = this.objDraw.PageBounds.Width * fltLeftIndentProp;

            float[] widthflaArr = { 0.34f, 0.45f, 0.65f, 0.73f, 0.81f, 0.89f };
            // 2019-11-01
            if (1 != 1) //   p_blnDiffOn)
                widthflaArr = new float[] { 0.30f, 0.43f, 0.55f, 0.6f, 0.65f, 0.9f, 0.71f, 0.8f };//����������Ϊ���һ���
            //��Ŀ��������
            //����
            objDraw.Graphics.DrawString("��Ŀ����", objFontNormal, Brushes.Black, X, Y);
            //���
            X = this.objDraw.PageBounds.Width * widthflaArr[0];
            objDraw.Graphics.DrawString("���", objFontNormal, Brushes.Black, X, Y);
            //����
            X = this.objDraw.PageBounds.Width * widthflaArr[1];
            objDraw.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y);
            //��λ
            X = this.objDraw.PageBounds.Width * widthflaArr[2];
            objDraw.Graphics.DrawString("��λ", objFontNormal, Brushes.Black, X, Y);
            //������
            X = this.objDraw.PageBounds.Width * widthflaArr[3];
            objDraw.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y);
            //����
            X = this.objDraw.PageBounds.Width * widthflaArr[4];
            objDraw.Graphics.DrawString("����", objFontNormal, Brushes.Black, X, Y);
            // 2019-11-01
            if (1 != 1) //   p_blnDiffOn)
            {
                //�������
                X = this.objDraw.PageBounds.Width * widthflaArr[6];
                objDraw.Graphics.DrawString("ҩƷ������", objFontNormal, Brushes.Black, X, Y);
                //ʵ�����
                X = this.objDraw.PageBounds.Width * widthflaArr[7];
                objDraw.Graphics.DrawString("ʵ�����", objFontNormal, Brushes.Black, X, Y);
            }
            //�ܼ�
            X = this.objDraw.PageBounds.Width * widthflaArr[5];
            objDraw.Graphics.DrawString("�ܽ��", objFontNormal, Brushes.Black, X, Y);

            Y += this.fltRowHeight * 2;

        }
        #endregion
        #region ��ӡ����
        /// <summary>
        /// ��Ŀ������󳤶�
        /// </summary>
        private int intMaxLength = 5;
        public static decimal decTotalDiffCost = 0;
        /// <summary>
        /// ��ӡ���� 
        /// </summary>
        /// <param name="currRows">����</param>
        /// <param name="p_blnDiffOn">�Ƿ��ӡ������</param>
        private void m_mthPrintText(ref int currRows, bool p_blnDiffOn)
        {
            Y += 5;
            float X = this.objDraw.PageBounds.Width * fltLeftIndentProp;
            //��Ŀ��󳤶�
            SizeF size = objDraw.Graphics.MeasureString("��", objFontNormal);
            float ItemNameMaxLength = this.objDraw.PageBounds.Width * 0.30f;
            intMaxLength = (int)Math.Floor(ItemNameMaxLength / size.Width);
            StringFormat objSF = new StringFormat();
            objSF.Alignment = StringAlignment.Far;
            float[] widthflaArr = { 0.34f, 0.45f, 0.655f, 0.78f, 0.84f, 0.94f };
            // 2019-11-01
            if (1 != 1) //   p_blnDiffOn)
                widthflaArr = new float[] { 0.30f, 0.43f, 0.56f, 0.65f, 0.7f, 0.94f, 0.77f, 0.88f };//����������Ϊ���һ���
            for (int i = currRows; i < this.obj_VO.objPRDArr.Count; i++)
            {
                clsOutpatientPrintRecipeDetail_VO objtemp = obj_VO.objPRDArr[i] as clsOutpatientPrintRecipeDetail_VO;
                X = this.objDraw.PageBounds.Width * fltLeftIndentProp;
                int temp = 1;
                if (objtemp.m_strChargeName.Length <= intMaxLength)
                {
                    objDraw.Graphics.DrawString(objtemp.m_strChargeName, objFontNormal, Brushes.Black, X, Y);
                }
                else
                {
                    objDraw.Graphics.DrawString(objtemp.m_strChargeName.Substring(0, intMaxLength), objFontNormal, Brushes.Black, X, Y);
                    objDraw.Graphics.DrawString(objtemp.m_strChargeName.Substring(intMaxLength, objtemp.m_strChargeName.Length - intMaxLength), objFontNormal, Brushes.Black, X, Y + fltRowHeight);
                    temp = 2;
                }
                //���
                X = this.objDraw.PageBounds.Width * widthflaArr[0];
                objDraw.Graphics.DrawString(objtemp.m_strSpec, objFontNormal, Brushes.Black, X, Y);
                //����
                X = this.objDraw.PageBounds.Width * widthflaArr[1];
                objDraw.Graphics.DrawString(objtemp.m_strFrequency, objFontNormal, Brushes.Black, X, Y);
                //��λ
                X = this.objDraw.PageBounds.Width * widthflaArr[2];
                objDraw.Graphics.DrawString(objtemp.m_strUnit, objFontNormal, Brushes.Black, X, Y);

                //����
                X = this.objDraw.PageBounds.Width * widthflaArr[3];
                objDraw.Graphics.DrawString(objtemp.m_strPrice, objFontNormal, Brushes.Black, X, Y, objSF);
                //����
                X = this.objDraw.PageBounds.Width * widthflaArr[4];
                objDraw.Graphics.DrawString(objtemp.m_strCount, objFontNormal, Brushes.Black, X, Y, objSF);
                // 2019-11-01
                if (1 != 1) // p_blnDiffOn)
                {
                    //�������
                    X = this.objDraw.PageBounds.Width * widthflaArr[6];
                    objDraw.Graphics.DrawString(objtemp.m_decTolDiffPrice.ToString(), objFontNormal, Brushes.Black, X, Y, objSF);
                    //ʵ�����
                    X = this.objDraw.PageBounds.Width * widthflaArr[7];
                    objDraw.Graphics.DrawString((clsPublic.ConvertObjToDecimal(objtemp.m_strSumPrice) - Math.Abs(objtemp.m_decTolDiffPrice)).ToString(), objFontNormal, Brushes.Black, X, Y, objSF);
                    decTotalDiffCost += objtemp.m_decTolDiffPrice;
                }
                //�ܽ��
                X = this.objDraw.PageBounds.Width * widthflaArr[5];
                objDraw.Graphics.DrawString(objtemp.m_strSumPrice, objFontNormal, Brushes.Black, X, Y, objSF);

                Y += this.fltRowHeight * temp + 3;
                if (Y + 2 * fltRowHeight > this.objDraw.PageBounds.Height)
                {
                    currRows = i + 1;
                    this.objDraw.HasMorePages = true;
                    return;
                }

            }
            decTotalDiffCost = Math.Abs(decTotalDiffCost) * -1;
            //�����ܽ��
            X = this.objDraw.PageBounds.Width * 0.94f;
            Y += 30;
            // 2019-11-01
            if (1 != 1)   //p_blnDiffOn
            {
                obj_VO.m_strSelfPay = (decimal.Parse(obj_VO.m_strSelfPay)).ToString();
                obj_VO.m_strRecipePrice = (decimal.Parse(obj_VO.m_strRecipePrice) + decTotalDiffCost).ToString();
            }
            string strPrint = "���ʽ��:" + obj_VO.m_strChargeUp + "   " + "�Ը����:" + obj_VO.m_strSelfPay + "   " + "�ϼ�:   " + obj_VO.m_strRecipePrice;
            objDraw.Graphics.DrawString(strPrint, objFontNormal, Brushes.Black, X, Y, objSF);
            if (obj_VO.m_strChargeUp != null && obj_VO.m_strChargeUp.Trim() != "0.00" && obj_VO.m_strChargeUp != null && obj_VO.m_strChargeUp.Trim() != string.Empty && obj_VO.m_strChargeUp != null && obj_VO.m_strChargeUp.Trim() != string.Empty && obj_VO.m_strRegisterFee != null && obj_VO.m_strRegisterFee.Trim() != string.Empty && obj_VO.m_strTreatFee != null && obj_VO.m_strTreatFee.Trim() != string.Empty)
            {

                string m_strTemp = "(���м��˹Һŷѣ�" + obj_VO.m_strRegisterFee + " �������" + obj_VO.m_strTreatFee + " ��������:" + ((decimal)(decimal.Parse(obj_VO.m_strChargeUp) - decimal.Parse(obj_VO.m_strRegisterFee) - decimal.Parse(obj_VO.m_strTreatFee))).ToString("0.00") + ")";
                Y += 25;
                objDraw.Graphics.DrawString(m_strTemp, objFontNormal, Brushes.Black, X, Y, objSF);
            }

        }
        #endregion
        #region ��ӡҳ��
        private void m_mthPrintEnd()
        {

        }
        #endregion
        #region ��ʼ��ӡ

        public void m_mthBegionPrint(bool isOtherPage, ref int currRows, bool blnDiffOn)
        {
            if (isOtherPage == false)
                this.m_mthPrintTitle(blnDiffOn);
            this.m_mthPrintText(ref currRows, blnDiffOn);
            this.m_mthPrintEnd();
        }
        #endregion
    }
}
