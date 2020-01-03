using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
//using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;

using System.Drawing;//��ͼ��Ҫ����

using System.Drawing.Printing; //��ӡ��Ҫ����


namespace com.digitalwave.iCare.gui.HIS
{
    #region �����շѰ���ݷ���ͳ�Ʊ���������� ��created by weiling.huang  at 2005-9-16
    /// <summary>
    ///�����շѰ���ݷ���ͳ�Ʊ���������� ��created by weiling.huang  at 2005-9-16
    /// </summary>
    public class clsOutpatientChargeIdetityPrint : com.digitalwave.GUI_Base.clsController_Base	//GUI_Base.dll
    {
        #region ���캯��

        public clsOutpatientChargeIdetityPrint()
        {
            m_objDomainManage = new clsDomainOutpatientChargeIdetityPrint();
        }
        #endregion

        #region ����
        /// <summary>
        /// DomainControl����
        /// </summary>
        private clsDomainOutpatientChargeIdetityPrint m_objDomainManage = null;

        /// <summary>
        /// frm�������
        /// </summary>
        private frmOutpatientChargeIdetityPrint m_objFrmViewer;

        /// <summary>
        /// Pen����
        /// </summary>
        private Pen m_objPen = new Pen(Color.Black);
        /// <summary>
        /// ������������
        /// </summary>
        private Font m_objFont = new Font("����", 10);

        /// <summary>
        /// ��������
        /// </summary>
        private string m_strTitle = "�����շѰ���ݷ���ͳ�Ʊ���";

        ///<summary>
        ///��������ȡ�û���ѯ���Ľ������

        ///</summary>
        private clsOutPatientTableInfo_VO[] m_objResultArr = null;

        ///<summary>
        ///��������Ч�˴�

        ///</summary>
        private int m_intPeople = 0; //��Ч�˴�
        ///<summary>
        ///��������Ч�ܺϼ�

        ///</summary>
        private float m_fltTotal = 0;//��Ч�ܺϼ�

        ///<summary>
        ///��������Ч�Ը��ϼ�

        ///</summary>
        private float m_fltSubTotal = 0;//��Ч�Ը��ϼ�
        ///<summary>
        ///��������Ч���ʺϼ�

        ///</summary>
        private float m_fltJiTotal = 0;//��Ч���ʺϼ�

        ///<summary>
        ///��������Ʊ�˴�

        ///</summary>
        private int m_intTuiPeople = 0; //��Ʊ�˴�

        ///<summary>
        ///��������Ʊ�ܺϼ�

        ///</summary>
        private float m_fltTuiTotal = 0;//��Ʊ�ܺϼ�

        ///<summary>
        ///��������Ʊ�Ը��ϼ�

        ///</summary>
        private float m_fltTuiSubTotal = 0;//��Ʊ�Ը��ϼ�

        ///<summary>
        ///��������Ʊ���ʺϼ�

        ///</summary>
        private float m_fltTuiJiTotal = 0;//��Ʊ���ʺϼ�


        ///<summary>
        ///��������ȡ��߾� e.MarginBounds.Left
        ///</summary>
        private float fltLeftAlignWidth;

        ///<summary>
        ///��������¼��ǰ��ӡ���ڸ߶�

        ///</summary>
        private float m_fltCurrentHeight = 0;

        ///<summary>
        ///��������ӡ�ĵ�ǰҳ��
        ///</summary>
        private int m_intCurrentPageIndex = 0;

        ///<summary>
        ///��������ӡ����ҳ��

        ///</summary>
        private int m_intPageTotal = 0;
        ///<summary>
        ///��������¼��ӡ��������¼��Ҳ���Ǵ���ӡ��VO�������ݵ��±�

        ///</summary>
        private int m_intVoIndex = -1;

        private bool m_blnfirst = true;
        private int intPageRowCount = 0;
        #endregion

        #region ���ô������override Set_GUI_Apperance ʵ��
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            // TODO:  ��� Set_GUI_Apperance ʵ��
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objFrmViewer = (frmOutpatientChargeIdetityPrint)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����ؼ�(�ռ�ؼ�,�������������,�շ�Ա������)�ĳ�ʼ�� ��created by weiling.huang  at 2005-9-19
        /// <summary>
        /// ����ؼ�(�ռ�ؼ�,�������������,�շ�Ա������)�ĳ�ʼ�� ��created by weiling.huang  at 2005-9-19
        /// </summary>
        public void m_mthFrmInit()
        {
            //m_mthInitDataTimePicker();//��ʼ���ռ�ؼ�

            m_mthBindPatientTypeList();//��ʼ���������������
            m_mthBindOperatorList();//��ʼ���շ�Ա�б��

        }
        #endregion

        #region �����ռ�ؼ��ĳ�ʼ�� ��created by weiling.huang  at 2005-9-19
        /// <summary>
        /// �����ռ�ؼ��ĳ�ʼ�� ��created by weiling.huang  at 2005-9-19
        /// </summary>
        private void m_mthInitDataTimePicker()
        {
            DateTime dtm = this.m_objDomainManage.m_dtmGetServerDate();
            this.m_objFrmViewer.m_dateTimePickerBegin.Value = Convert.ToDateTime(dtm.Year.ToString() + "-" + dtm.Month.ToString() + "-" + "01"); ;
            this.m_objFrmViewer.m_dateTimePickerEnd.Value = dtm;
        }
        #endregion

        #region ��ò�����ݷ���������ID�б�������Combox��created by weiling.huang  at 2005-9-19
        /// <summary>
        /// ��ò�����ݷ���������ID�б�������Combox��created by weiling.huang  at 2005-9-19
        /// </summary>
        private void m_mthBindPatientTypeList()
        {

            clsPType_VO[] objPriodItems;
            this.m_objDomainManage.m_mthGetPatientCatInfo(out objPriodItems); //��ò�����ݷ���������ID�б�

            if (objPriodItems.Length > 0)
            {

                this.m_objFrmViewer.m_cboIdentity.Items.Insert(0, "ȫ������");
                for (int i1 = 0; i1 < objPriodItems.Length; i1++)
                {
                    this.m_objFrmViewer.m_cboIdentity.Items.Insert(i1 + 1, objPriodItems[i1].m_strPAYTYPENAME_VCHR.Trim());
                }
                this.m_objFrmViewer.m_cboIdentity.Tag = objPriodItems;
                this.m_objFrmViewer.m_cboIdentity.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("û������!", "ϵͳ��ʾ");
            }
        }
        #endregion

        #region ����:����շ�Ա������ID�б�������Combox��created by weiling.huang  at 2005-9-19
        /// <summary>
        /// ����:����շ�Ա������ID�б�������Combox��created by weiling.huang  at 2005-9-19
        /// </summary>
        private void m_mthBindOperatorList()
        {

            clsEChargeInfo_VO[] objEChargeInfoItemsArr;
            this.m_objDomainManage.m_mthGetChargeManInfo(out objEChargeInfoItemsArr); //���������ID�б�

            if (objEChargeInfoItemsArr.Length > 0)
            {

                this.m_objFrmViewer.m_cboOperator.Items.Insert(0, "ȫ��");
                for (int i1 = 0; i1 < objEChargeInfoItemsArr.Length; i1++)
                {
                    this.m_objFrmViewer.m_cboOperator.Items.Insert(i1 + 1, objEChargeInfoItemsArr[i1].m_strLastname_vchr.Trim());
                }
                this.m_objFrmViewer.m_cboOperator.Tag = objEChargeInfoItemsArr;
                this.m_objFrmViewer.m_cboOperator.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("û������!", "ϵͳ��ʾ");
            }
        }
        #endregion

        #region ��������ѯ��ť�¼���������,����������ѯ,��������created by weiling.huang  at 2005-9-19
        /// <summary>
        /// ��������ѯ��ť�¼���������,����������ѯ,��������created by weiling.huang  at 2005-9-19
        /// <summary>
        public void m_mthQuery()
        {
            string p_strPatientTypeId = null;
            string p_strEmployeeID = null;

            #region ȡ����ѡ��Ĳ�������ID
            if (this.m_objFrmViewer.m_cboIdentity.Items.Count > 0 && this.m_objFrmViewer.m_cboIdentity.SelectedIndex != -1)
            {
                int intIndex = this.m_objFrmViewer.m_cboIdentity.SelectedIndex;
                clsPType_VO[] objPType = this.m_objFrmViewer.m_cboIdentity.Tag as clsPType_VO[];
                if (intIndex == 0)
                {
                    p_strPatientTypeId = null; //��־��ѡ����ȫ��

                }
                else
                {
                    p_strPatientTypeId = objPType[intIndex - 1].m_strPAYTYPEID_CHR.ToString();
                }
            }
            #endregion

            #region ȡ����ѡ����շ�ԱID
            if (this.m_objFrmViewer.m_cboOperator.Items.Count > 0 && this.m_objFrmViewer.m_cboOperator.SelectedIndex != -1)
            {
                int intIndex = this.m_objFrmViewer.m_cboOperator.SelectedIndex;
                clsEChargeInfo_VO[] objOp = this.m_objFrmViewer.m_cboOperator.Tag as clsEChargeInfo_VO[];
                if (intIndex == 0)
                {
                    p_strEmployeeID = null; //��־��ѡ����ȫ��

                }
                else
                {
                    p_strEmployeeID = objOp[intIndex - 1].m_strEmpid_chr.ToString();
                }
            }
            #endregion

            //�����û���ѡ��ʱ��,������ݺͲ���Ա���ƻ�ȡ��Ʊ�Ľ�����Ϣ,���浽����m_objResultArr
            this.m_mthGetDataByTimeIndetityOp(out m_objResultArr, p_strPatientTypeId, p_strEmployeeID);

        }
        #endregion

        #region �����������û���ѡ��ʱ��,������ݺͲ���Ա���ƻ�ȡ��Ʊ�Ľ�����Ϣ�ȣ�created by weiling.huang  at 2005-9-19
        /// <summary>
        /// �����û���ѡ��ʱ��,������ݺͲ���Ա���ƻ�ȡ��Ʊ�Ľ�����Ϣ��
        /// </summary>
        /// <param name="p_objResultArr">�������</param>
        /// <param name="p_strdtmBegin">��ѯ������������ʼ����</param>
        /// <param name="p_strdtmEnd">��ѯ������������ֹ����</param>
        /// <param name="p_strPatientTypeId">��ѯ�����������������ID</param>
        /// <param name="p_strEmployeeID">��ѯ�������շ�ԱID</param>
        /// <returns></returns>
        public void m_mthGetDataByTimeIndetityOp(out clsOutPatientTableInfo_VO[] p_objResultArr, string p_strPatientTypeId, string p_strEmployeeID)
        {
            p_objResultArr = null;
            string strDateBegin = this.m_objFrmViewer.m_dateTimePickerBegin.Value.ToShortDateString();
            string strDateEnd = this.m_objFrmViewer.m_dateTimePickerEnd.Value.ToShortDateString();
            long lngRes = 0;

            lngRes = this.m_objDomainManage.m_lngGetDataByTimeIndetityOp(out p_objResultArr, strDateBegin, strDateEnd, p_strPatientTypeId, p_strEmployeeID);
            if (lngRes > 0)
            {

            }
            else
            {
                MessageBox.Show("��ȡ���ݳ���", "ϵͳ������ʾ");
                return;
            }
        }
        #endregion

        #region  ������������ӡ

        public void m_mthQueryClick()
        {
            m_intCurrentPageIndex = 0;//��ʼ����ǰҳΪ0
            m_intPageTotal = 0;//��ʼ��ҳ��Ϊ0
            m_intPeople = 0; //��Ч�˴�
            m_fltTotal = 0;//��Ч�ܺϼ�

            m_fltSubTotal = 0;//��Ч�Ը��ϼ�
            m_fltJiTotal = 0;//��Ч���ʺϼ�
            m_intTuiPeople = 0; //��Ʊ�˴�

            m_fltTuiTotal = 0;//��Ʊ�ܺϼ�

            m_fltTuiSubTotal = 0;//��Ʊ�Ը��ϼ�

            m_fltTuiTotal = 0;//��Ʊ�ܺϼ�

            m_fltTuiSubTotal = 0;//��Ʊ�Ը��ϼ�

            m_fltTuiJiTotal = 0;//��Ʊ���ʺϼ�

            m_intVoIndex = -1;
            m_blnfirst = true;
            intPageRowCount = 0;
            this.m_objFrmViewer.m_printPreviewControl1.Document = this.m_objFrmViewer.m_printDocument1;
            this.m_objFrmViewer.m_printPreviewDialog1.Document = this.m_objFrmViewer.m_printDocument1;

        }
        #endregion

        #region  ����showprintdialog
        public void m_mthClick()
        {
            m_intCurrentPageIndex = 0;//��ʼ����ǰҳΪ0
            m_intPageTotal = 0;//��ʼ��ҳ��Ϊ0
            m_intPeople = 0; //��Ч�˴�
            m_fltTotal = 0;//��Ч�ܺϼ�

            m_fltSubTotal = 0;//��Ч�Ը��ϼ�
            m_fltJiTotal = 0;//��Ч���ʺϼ�
            m_intTuiPeople = 0; //��Ʊ�˴�

            m_fltTuiTotal = 0;//��Ʊ�ܺϼ�

            m_fltTuiSubTotal = 0;//��Ʊ�Ը��ϼ�

            m_fltTuiTotal = 0;//��Ʊ�ܺϼ�

            m_fltTuiSubTotal = 0;//��Ʊ�Ը��ϼ�

            m_fltTuiJiTotal = 0;//��Ʊ���ʺϼ�

            m_intVoIndex = -1;
            m_blnfirst = true;
            intPageRowCount = 0;
            this.m_objFrmViewer.m_printPreviewDialog1.Document = this.m_objFrmViewer.m_printDocument1;
            this.m_objFrmViewer.m_printPreviewDialog1.ShowDialog();

        }
        #endregion

        #region  ��ӡÿһҳʱ������created by weiling.huang  at 2005-9-16
        /// <summary>
        /// ��ӡÿһҳʱ������created by weiling.huang  at 2005-9-16
        /// </summary>
        /// <param name="sender">��������</param>
        /// <param name="e">��ӡ��ز�������</param>
        public void m_printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            m_printDoc_Header(sender, e);//��ӡ��һҳҳͷ

            m_printDoc_HeaderTitleAndBody(sender, e);//��ӡ��һҳ��ͷ��ʵ������
        }
        #endregion

        #region  ��ӡ��һҳҳͷ��created by weiling.huang  at 2005-9-16
        /// <summary>
        /// ��ӡ��һҳҳͷ��created by weiling.huang  at 2005-9-16
        /// </summary>
        /// <param name="sender">��������</param>
        /// <param name="e">��ӡ��ز�������</param>
        public void m_printDoc_Header(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.DrawRectangle(this.m_objPen,e.MarginBounds.X,e.MarginBounds.Y,e.MarginBounds.Width,e.MarginBounds.Height);

            //			this.GetData();
            string strPrint = "";
            if (this.m_intCurrentPageIndex == 0)
            {
                //�������
                SizeF objsize = e.Graphics.MeasureString(this.m_strTitle, new Font("����", 18));
                e.Graphics.DrawString(m_strTitle, new Font("����", 18), Brushes.Black, e.MarginBounds.Left + e.MarginBounds.Width / 2 - objsize.Width / 2, e.MarginBounds.Top + objsize.Height / 2);
                //����

                fltLeftAlignWidth = e.MarginBounds.Left;//���־�����ߵ�λ��

                //��ͳ��������
                float fltHeight = e.MarginBounds.Top + +objsize.Height + 30;//���еĸ߶�

                strPrint = " ͳ�����ڣ�" + this.m_objFrmViewer.m_dateTimePickerBegin.Value.Date.ToShortDateString() + " �� " + this.m_objFrmViewer.m_dateTimePickerEnd.Value.Date.ToShortDateString();
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeftAlignWidth, fltHeight);

                strPrint = " ������ݣ�" + this.m_objFrmViewer.m_cboIdentity.Text.Trim();
                float fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.45;
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.75;
                strPrint = " �շ�Ա��" + this.m_objFrmViewer.m_cboOperator.Text.Trim();
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);
                //���л���

                //��ʵ���˴���
                fltHeight = fltHeight + 35;//���еĸ߶�

                fltLeft = fltLeftAlignWidth;
                strPrint = " ʵ���˴Σ�" + Convert.ToString(this.m_intPeople + this.m_intTuiPeople) + "��";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.25;
                strPrint = " �Ը��ϼƣ�" + Convert.ToString(this.m_fltSubTotal + this.m_fltTuiSubTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.50;
                strPrint = " ���ʺϼƣ�" + Convert.ToString(this.m_fltJiTotal + this.m_fltTuiJiTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.75;
                strPrint = " �ܺϼƣ�" + Convert.ToString(this.m_fltTotal + this.m_fltTuiTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);
                //����

                //����Ʊ�˴���
                objsize = e.Graphics.MeasureString("����", this.m_objFont);
                fltHeight = fltHeight + objsize.Height;//���еĸ߶�

                fltLeft = fltLeftAlignWidth;
                strPrint = " ��Ʊ�˴Σ�" + Convert.ToString(this.m_intTuiPeople) + "��";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.25;
                strPrint = " �Ը��ϼƣ�" + Convert.ToString(this.m_fltTuiSubTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.50;
                strPrint = " ���ʺϼƣ�" + Convert.ToString(this.m_fltTuiJiTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.75;
                strPrint = " �ܺϼƣ�" + Convert.ToString(this.m_fltTuiTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);
                //����

                //����Ч�˴���				
                fltHeight = fltHeight + objsize.Height;//���еĸ߶�

                fltLeft = fltLeftAlignWidth;
                strPrint = " ��Ч�˴Σ�" + Convert.ToString(this.m_intPeople) + "��";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.25;
                strPrint = " �Ը��ϼƣ�" + Convert.ToString(this.m_fltSubTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.50;
                strPrint = " ���ʺϼƣ�" + Convert.ToString(this.m_fltJiTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.75;
                strPrint = " �ܺϼƣ�" + Convert.ToString(this.m_fltTotal);
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);
                //����

                //����ӡʱ����
                fltHeight = fltHeight + 35;//���еĸ߶�

                fltLeft = fltLeftAlignWidth;
                strPrint = " ��ӡʱ�䣺" + this.m_objDomainManage.m_dtmGetServerDate().ToString();
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);

                fltLeft = fltLeftAlignWidth;
                strPrint = "��λ��" + m_fltTotal.ToString() + "Ԫ";
                fltLeft = fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.77;
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, fltHeight);
                //����

                m_fltCurrentHeight = fltHeight + objsize.Height;
            }
        }
        #endregion

        #region  ��ӡ�б�ͷ��ʵ�����ݣ�created by weiling.huang  at 2005-9-16
        /// <summary>
        /// ��ӡ�б�ͷ��ʵ�����ݣ�created by weiling.huang  at 2005-9-16
        /// </summary>
        /// <param name="sender">��������</param>
        /// <param name="e">��ӡ��ز�������</param>
        public void m_printDoc_HeaderTitleAndBody(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region ���ô�ӡ�п���ÿ�еĺ�����

            float fltFirstCol = e.MarginBounds.Width * (float)0.1; //��1�п��

            float fltSeconCol = e.MarginBounds.Width * (float)0.15; //��2�п��

            float fltthCol = e.MarginBounds.Width * (float)0.15; //��3�п��

            float fltthirCol = e.MarginBounds.Width * (float)0.15; //��4�п��

            float fltFiveCol = e.MarginBounds.Width * (float)0.1; //��5�п��

            float fltSixCol = e.MarginBounds.Width * (float)0.1; //��6�п��

            float fltSenCol = e.MarginBounds.Width * (float)0.1; //��7�п��

            float fltNigCol = e.MarginBounds.Width * (float)0.15; //��8�п��


            float fltFirstColLeft = e.MarginBounds.Left; //��1��Left����
            float fltSeconColLeft = fltFirstCol + fltFirstColLeft; //��2��Left����
            float fltthColLeft = fltSeconColLeft + fltSeconCol; //��3��Left����
            float fltthirColLeft = fltthColLeft + fltthCol; //��4��Left����
            float fltFiveColLeft = fltthirColLeft + fltthirCol; //��5��Left����
            float fltSixColLeft = fltFiveColLeft + fltFiveCol; //��6��Left����
            float fltSenColLeft = fltSixColLeft + fltSixCol; //��7��Left����
            float fltNigColLeft = fltSenColLeft + fltSenCol; //��8��Left����
            #endregion

            string strPrint = "";//Ҫ��ӡ����

            float fltZijiHeight = 4; //�����߼��λ�ø� �����

            SizeF objsize = e.Graphics.MeasureString("����", this.m_objFont);
            float fltZiHeight = objsize.Height;// �ָ�
            float fltZiJiWide = 1;// �����������룺���

            float ftlRowHeight = fltZijiHeight + fltZiHeight;//�и�

            StringFormat sf = new StringFormat();
            //sf.Alignment = StringAlignment.Far;



            if (this.m_intCurrentPageIndex == 0)//��һҳ
            {
                //����ͷ

                e.Graphics.DrawLine(this.m_objPen, fltFirstColLeft, m_fltCurrentHeight, fltNigColLeft + fltNigCol, m_fltCurrentHeight);
                //����
                m_fltCurrentHeight += ftlRowHeight;
                e.Graphics.DrawLine(this.m_objPen, fltFirstColLeft, m_fltCurrentHeight, fltNigColLeft + fltNigCol, m_fltCurrentHeight);
                //������

                e.Graphics.DrawLine(this.m_objPen, fltFirstColLeft, m_fltCurrentHeight - ftlRowHeight, fltFirstColLeft, m_fltCurrentHeight);
                e.Graphics.DrawLine(this.m_objPen, fltSeconColLeft, m_fltCurrentHeight - ftlRowHeight, fltSeconColLeft, m_fltCurrentHeight);
                e.Graphics.DrawLine(this.m_objPen, fltthColLeft, m_fltCurrentHeight - ftlRowHeight, fltthColLeft, m_fltCurrentHeight);
                e.Graphics.DrawLine(this.m_objPen, fltthirColLeft, m_fltCurrentHeight - ftlRowHeight, fltthirColLeft, m_fltCurrentHeight);
                e.Graphics.DrawLine(this.m_objPen, fltFiveColLeft, m_fltCurrentHeight - ftlRowHeight, fltFiveColLeft, m_fltCurrentHeight);
                e.Graphics.DrawLine(this.m_objPen, fltSixColLeft, m_fltCurrentHeight - ftlRowHeight, fltSixColLeft, m_fltCurrentHeight);
                e.Graphics.DrawLine(this.m_objPen, fltSenColLeft, m_fltCurrentHeight - ftlRowHeight, fltSenColLeft, m_fltCurrentHeight);
                e.Graphics.DrawLine(this.m_objPen, fltNigColLeft, m_fltCurrentHeight - ftlRowHeight, fltNigColLeft, m_fltCurrentHeight);
                e.Graphics.DrawLine(this.m_objPen, fltNigColLeft + fltNigCol, m_fltCurrentHeight - ftlRowHeight, fltNigColLeft + fltNigCol, m_fltCurrentHeight);

                //���ͷ��
                strPrint = "��������";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltFirstColLeft + fltZiJiWide, m_fltCurrentHeight - ftlRowHeight + fltZijiHeight);

                strPrint = " �� �� �� ��";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltSeconColLeft + fltZiJiWide, m_fltCurrentHeight - ftlRowHeight + fltZijiHeight);

                strPrint = " �� �� �� ��";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltthColLeft + fltZiJiWide, m_fltCurrentHeight - ftlRowHeight + fltZijiHeight);

                strPrint = " �� Ʊ �� ��";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltthirColLeft + fltZiJiWide, m_fltCurrentHeight - ftlRowHeight + fltZijiHeight);

                strPrint = "�Ը����";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltFiveColLeft + fltZiJiWide, m_fltCurrentHeight - ftlRowHeight + fltZijiHeight);

                strPrint = "���ʽ��";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltSixColLeft + fltZiJiWide, m_fltCurrentHeight - ftlRowHeight + fltZijiHeight);

                strPrint = "�ϼƽ��";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltSenColLeft + fltZiJiWide, m_fltCurrentHeight - ftlRowHeight + fltZijiHeight);

                strPrint = "  �� �� Ա";
                e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltNigColLeft + fltZiJiWide, m_fltCurrentHeight - ftlRowHeight + fltZijiHeight);
                //����

                //�����һҳ���ܴ�ӡ����������¼

                int intRowCount = Convert.ToInt32((float.Parse(e.MarginBounds.Height.ToString()) - m_fltCurrentHeight) / ftlRowHeight);
                intRowCount--; //��Ϊ����һ��λ������ӡҳ�� ��������ʾ������Ϊ��1

                //ѭ����ӡ����


                if (this.m_objResultArr.Length > 0)
                {

                    for (int i1 = 0; i1 < this.m_objResultArr.Length && i1 <= intRowCount; i1++)
                    //	for(int i1 = 0 ;i1 < this.m_objResultArr.Length; i1 ++)
                    {
                        e.Graphics.DrawString(" " + m_objResultArr[i1].m_strPatientName, this.m_objFont, Brushes.Black, fltFirstColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);
                        e.Graphics.DrawString(" " + m_objResultArr[i1].m_strPatientCardId, this.m_objFont, Brushes.Black, fltSeconColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);
                        e.Graphics.DrawString(" " + m_objResultArr[i1].m_strRecordDataTime.Date.ToShortDateString(), this.m_objFont, Brushes.Black, fltthColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);
                        e.Graphics.DrawString(" " + m_objResultArr[i1].m_strInvoiceNo, this.m_objFont, Brushes.Black, fltthirColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);

                        SizeF sizeX = e.Graphics.MeasureString(" " + m_objResultArr[i1].m_strSBSUM_MNY.ToString("0.00"), this.m_objFont);
                        e.Graphics.DrawString(" " + m_objResultArr[i1].m_strSBSUM_MNY.ToString("0.00"), this.m_objFont, Brushes.Black, fltSixColLeft - fltZiJiWide - sizeX.Width, m_fltCurrentHeight + fltZijiHeight);

                        sizeX = e.Graphics.MeasureString(" " + m_objResultArr[i1].m_strACCTSUM_MNY.ToString("0.00"), this.m_objFont);
                        e.Graphics.DrawString(" " + m_objResultArr[i1].m_strACCTSUM_MNY.ToString("0.00"), this.m_objFont, Brushes.Black, fltSenColLeft - fltZiJiWide - sizeX.Width, m_fltCurrentHeight + fltZijiHeight);

                        sizeX = e.Graphics.MeasureString(" " + m_objResultArr[i1].m_strTOTALSUM_MNY.ToString("0.00"), this.m_objFont);
                        e.Graphics.DrawString(" " + m_objResultArr[i1].m_strTOTALSUM_MNY.ToString("0.00"), this.m_objFont, Brushes.Black, fltNigColLeft - fltZiJiWide - sizeX.Width, m_fltCurrentHeight + fltZijiHeight);
                        e.Graphics.DrawString("  " + m_objResultArr[i1].m_strLastname_vchr, this.m_objFont, Brushes.Black, fltNigColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);

                        //����
                        m_fltCurrentHeight += ftlRowHeight;
                        e.Graphics.DrawLine(this.m_objPen, fltFirstColLeft, m_fltCurrentHeight, fltNigColLeft + fltNigCol, m_fltCurrentHeight);

                        //������

                        e.Graphics.DrawLine(this.m_objPen, fltFirstColLeft, m_fltCurrentHeight - ftlRowHeight, fltFirstColLeft, m_fltCurrentHeight);
                        e.Graphics.DrawLine(this.m_objPen, fltSeconColLeft, m_fltCurrentHeight - ftlRowHeight, fltSeconColLeft, m_fltCurrentHeight);
                        e.Graphics.DrawLine(this.m_objPen, fltthColLeft, m_fltCurrentHeight - ftlRowHeight, fltthColLeft, m_fltCurrentHeight);
                        e.Graphics.DrawLine(this.m_objPen, fltthirColLeft, m_fltCurrentHeight - ftlRowHeight, fltthirColLeft, m_fltCurrentHeight);
                        e.Graphics.DrawLine(this.m_objPen, fltFiveColLeft, m_fltCurrentHeight - ftlRowHeight, fltFiveColLeft, m_fltCurrentHeight);
                        e.Graphics.DrawLine(this.m_objPen, fltSixColLeft, m_fltCurrentHeight - ftlRowHeight, fltSixColLeft, m_fltCurrentHeight);
                        e.Graphics.DrawLine(this.m_objPen, fltSenColLeft, m_fltCurrentHeight - ftlRowHeight, fltSenColLeft, m_fltCurrentHeight);
                        e.Graphics.DrawLine(this.m_objPen, fltNigColLeft, m_fltCurrentHeight - ftlRowHeight, fltNigColLeft, m_fltCurrentHeight);
                        e.Graphics.DrawLine(this.m_objPen, fltNigColLeft + fltNigCol, m_fltCurrentHeight - ftlRowHeight, fltNigColLeft + fltNigCol, m_fltCurrentHeight);

                        m_intCurrentPageIndex = 1; //��һҳ

                        this.m_intPageTotal = 1; //��ҳ��

                        m_intVoIndex = i1;

                    }
                    #region ������ҳ��

                    intPageRowCount = Convert.ToInt32(Convert.ToDouble(e.MarginBounds.Height) / ftlRowHeight);//�ڶ�ҳ����ʾ�ļ�¼����							
                    intPageRowCount--;//����һ��λ��дҳ��
                    int shengxi = this.m_objResultArr.Length - m_intVoIndex - 1;
                    m_intPageTotal = shengxi / intPageRowCount + 1;//��ҳ��

                    if (shengxi % intPageRowCount != 0)
                    {
                        m_intPageTotal++;
                    }
                    #endregion

                    m_printDoc_PrintFooter(sender, e);//��ҳ��

                    //�ж��Ƿ��ҳ
                    if (m_intVoIndex < this.m_objResultArr.Length)
                    {
                        e.HasMorePages = true;
                    }

                }
            }
            else //��ӡ�ǵ�һҳ
            {
                //ѭ����ӡʣ�¸���
                if (m_intVoIndex != -1)
                {
                    int intShengxia = this.m_objResultArr.Length - m_intVoIndex - 1;
                    if (intShengxia != 0) //��û��ӡ�ļ�¼
                    {
                        int intTemp = m_intVoIndex;
                        m_fltCurrentHeight = e.MarginBounds.Y;
                        for (int i1 = m_intVoIndex + 1; i1 < this.m_objResultArr.Length && i1 < intPageRowCount + intTemp; i1++)
                        {

                            e.Graphics.DrawLine(this.m_objPen, fltFirstColLeft, m_fltCurrentHeight, fltNigColLeft + fltNigCol, m_fltCurrentHeight);
                            e.Graphics.DrawString(" " + m_objResultArr[i1].m_strPatientName, this.m_objFont, Brushes.Black, fltFirstColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);
                            e.Graphics.DrawString(" " + m_objResultArr[i1].m_strPatientCardId, this.m_objFont, Brushes.Black, fltSeconColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);
                            e.Graphics.DrawString(" " + m_objResultArr[i1].m_strRecordDataTime.Date.ToShortDateString(), this.m_objFont, Brushes.Black, fltthColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);
                            e.Graphics.DrawString(" " + m_objResultArr[i1].m_strInvoiceNo, this.m_objFont, Brushes.Black, fltthirColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);

                            SizeF objX = e.Graphics.MeasureString(" " + m_objResultArr[i1].m_strSBSUM_MNY.ToString("0.00"), this.m_objFont);
                            e.Graphics.DrawString(" " + m_objResultArr[i1].m_strSBSUM_MNY.ToString("0.00"), this.m_objFont, Brushes.Black, fltSixColLeft - fltZiJiWide - objX.Width, m_fltCurrentHeight + fltZijiHeight, sf);

                            objX = e.Graphics.MeasureString(" " + m_objResultArr[i1].m_strACCTSUM_MNY.ToString("0.00"), this.m_objFont);
                            e.Graphics.DrawString(" " + m_objResultArr[i1].m_strACCTSUM_MNY.ToString("0.00"), this.m_objFont, Brushes.Black, fltSenColLeft - fltZiJiWide - objX.Width, m_fltCurrentHeight + fltZijiHeight, sf);

                            objX = e.Graphics.MeasureString(" " + m_objResultArr[i1].m_strTOTALSUM_MNY.ToString("0.00"), this.m_objFont);
                            e.Graphics.DrawString(" " + m_objResultArr[i1].m_strTOTALSUM_MNY.ToString("0.00"), this.m_objFont, Brushes.Black, fltNigColLeft - fltZiJiWide - objX.Width, m_fltCurrentHeight + fltZijiHeight, sf);
                            e.Graphics.DrawString(" " + m_objResultArr[i1].m_strLastname_vchr, this.m_objFont, Brushes.Black, fltNigColLeft + fltZiJiWide, m_fltCurrentHeight + fltZijiHeight);

                            //����
                            m_fltCurrentHeight += ftlRowHeight;
                            e.Graphics.DrawLine(this.m_objPen, fltFirstColLeft, m_fltCurrentHeight, fltNigColLeft + fltNigCol, m_fltCurrentHeight);

                            //������

                            e.Graphics.DrawLine(this.m_objPen, fltFirstColLeft, m_fltCurrentHeight - ftlRowHeight, fltFirstColLeft, m_fltCurrentHeight);
                            e.Graphics.DrawLine(this.m_objPen, fltSeconColLeft, m_fltCurrentHeight - ftlRowHeight, fltSeconColLeft, m_fltCurrentHeight);
                            e.Graphics.DrawLine(this.m_objPen, fltthColLeft, m_fltCurrentHeight - ftlRowHeight, fltthColLeft, m_fltCurrentHeight);
                            e.Graphics.DrawLine(this.m_objPen, fltthirColLeft, m_fltCurrentHeight - ftlRowHeight, fltthirColLeft, m_fltCurrentHeight);
                            e.Graphics.DrawLine(this.m_objPen, fltFiveColLeft, m_fltCurrentHeight - ftlRowHeight, fltFiveColLeft, m_fltCurrentHeight);
                            e.Graphics.DrawLine(this.m_objPen, fltSixColLeft, m_fltCurrentHeight - ftlRowHeight, fltSixColLeft, m_fltCurrentHeight);
                            e.Graphics.DrawLine(this.m_objPen, fltSenColLeft, m_fltCurrentHeight - ftlRowHeight, fltSenColLeft, m_fltCurrentHeight);
                            e.Graphics.DrawLine(this.m_objPen, fltNigColLeft, m_fltCurrentHeight - ftlRowHeight, fltNigColLeft, m_fltCurrentHeight);
                            e.Graphics.DrawLine(this.m_objPen, fltNigColLeft + fltNigCol, m_fltCurrentHeight - ftlRowHeight, fltNigColLeft + fltNigCol, m_fltCurrentHeight);



                            m_intVoIndex = i1;
                        }
                        m_intCurrentPageIndex += 1; //��һҳ

                        //this.m_intPageTotal += 1; //��ҳ��

                        m_printDoc_PrintFooter(sender, e);//��ҳ��

                        //�ж��Ƿ��ҳ
                        if (intPageRowCount < intShengxia)
                        {
                            e.HasMorePages = true;
                            return;
                        }
                    }
                }

            }

        }
        #endregion

        #region ��ȡ��ӡ���ݣ�created by weiling.huang  at 2005-9-16
        /// <summary>
        /// ��ȡ��ӡ���ݣ�created by weiling.huang  at 2005-9-16
        /// </summary>
        private void GetData()
        {
            this.m_mthQuery();//�õ�Ҫ��ӡ��������m_objResultArr��

            if (this.m_objResultArr.Length > 0)
            {
                int intPeople = 0; //��Ч�˴�
                float fltTotal = 0;//��Ч�ܺϼ�

                float fltSubTotal = 0;//��Ч�Ը��ϼ�
                float fltJiTotal = 0;//��Ч���ʺϼ�

                int intTuiPeople = 0; //��Ʊ�˴�

                float fltTuiTotal = 0;//��Ʊ�ܺϼ�

                float fltTuiSubTotal = 0;//��Ʊ�Ը��ϼ�

                float fltTuiJiTotal = 0;//��Ʊ���ʺϼ�


                int intLength = this.m_objResultArr.Length;
                for (int i1 = 0; i1 < intLength; i1++)
                {
                    if (m_objResultArr[i1].m_strTOTALSUM_MNY < 0)//��Ʊ
                    {
                        intTuiPeople++;
                        fltTuiTotal += (0 - m_objResultArr[i1].m_strTOTALSUM_MNY);
                        fltTuiSubTotal += (0 - m_objResultArr[i1].m_strSBSUM_MNY);
                        fltTuiJiTotal += (0 - m_objResultArr[i1].m_strACCTSUM_MNY);
                    }
                    else
                    {
                        intPeople++;
                        fltTotal += m_objResultArr[i1].m_strTOTALSUM_MNY;
                        fltSubTotal += m_objResultArr[i1].m_strSBSUM_MNY;
                        fltJiTotal += m_objResultArr[i1].m_strACCTSUM_MNY;
                    }
                }
                this.m_intPeople = intPeople;
                this.m_fltTotal = fltTotal;
                this.m_fltSubTotal = fltSubTotal;
                this.m_fltJiTotal = fltJiTotal;

                this.m_intTuiPeople = intTuiPeople;
                this.m_fltTuiTotal = fltTuiTotal;
                this.m_fltTuiSubTotal = fltTuiSubTotal;
                this.m_fltTuiJiTotal = fltTuiJiTotal;
            }
        }
        #endregion

        #region  ��ӡǰ������created by weiling.huang  at 2005-9-16
        /// <summary>
        /// ��ӡǰ������created by weiling.huang  at 2005-9-16
        /// </summary>
        /// <param name="sender">��������</param>
        /// <param name="e">��ӡ��ز�������</param>
        public void m_printDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GetData();
        }
        #endregion

        #region  ��ӡ�󴥷���created by weiling.huang  at 2005-9-16
        /// <summary>
        /// ��ӡ�󴥷���created by weiling.huang  at 2005-9-16
        /// </summary>
        /// <param name="sender">��������</param>
        /// <param name="e">��ӡ��ز�������</param>
        public void m_printDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        #endregion

        #region  ��������ҳ�ţ�created by weiling.huang  at 2005-9-16
        /// <summary>
        /// ��������ҳ�ţ�created by weiling.huang  at 2005-9-16
        /// </summary>
        /// <param name="sender">��������</param>
        /// <param name="e">��ӡ��ز�������</param>
        public void m_printDoc_PrintFooter(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string strPrint = "��" + this.m_intCurrentPageIndex.ToString() + "ҳ,��" + m_intPageTotal.ToString() + "ҳ";
            float fltLeft = this.fltLeftAlignWidth + float.Parse(e.MarginBounds.Width.ToString()) * (float)0.85;
            e.Graphics.DrawString(strPrint, this.m_objFont, Brushes.Black, fltLeft, this.m_fltCurrentHeight + 4);

            this.m_objFrmViewer.numericUpDown1.Maximum = m_intPageTotal;
        }
        #endregion

        #region  ������������created by weiling.huang  at 2005-9-26
        /// <summary>
        /// ������������created by weiling.huang  at 2005-9-26
        /// </summary>
        private void m_mthExcel(clsOutPatientTableInfo_VO[] p_objResultArr)
        {
            if (p_objResultArr.Length <= 0)
            {
                MessageBox.Show("�޵������ݣ�");
                return;
            }

            DataSet ds = new DataSet();
            ds = m_mthCreateExcelDataSet(p_objResultArr);
            com.digitalwave.iCare.common.ExcelExporter excel = new com.digitalwave.iCare.common.ExcelExporter(ds);
            bool bln = excel.m_mthExport();
            if (bln)
            {
                MessageBox.Show("�����ɹ���");
            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�");
            }

        }
        #endregion

        #region  �����������ӿڣ�created by weiling.huang  at 2005-9-26
        /// <summary>
        /// �����������ӿڣ�created by weiling.huang  at 2005-9-26
        /// </summary>
        public void m_mthOutExcel()
        {
            m_mthExcel(this.m_objResultArr);
        }
        #endregion

        #region  ���������ɵ�������Ҫ�õ���DataTable��created by weiling.huang  at 2005-9-26
        /// <summary>
        /// ���������ɵ�������Ҫ�õ���DataTable��created by weiling.huang  at 2005-9-26
        /// </summary>
        public DataTable m_mthCreateExcelTable()
        {
            DataTable dt = new DataTable("�����շ�");
            System.Data.DataColumn dc = new DataColumn("��������");
            dt.Columns.Add(dc);
            dc = new DataColumn("���￨��");
            dt.Columns.Add(dc);
            dc = new DataColumn("��������");
            dt.Columns.Add(dc);
            dc = new DataColumn("��Ʊ����");
            dt.Columns.Add(dc);
            dc = new DataColumn("�Ը����");
            dt.Columns.Add(dc);
            dc = new DataColumn("���ʽ��");
            dt.Columns.Add(dc);
            dc = new DataColumn("�ϼƽ��");
            dt.Columns.Add(dc);
            dc = new DataColumn("�շ�Ա");
            dt.Columns.Add(dc);
            return dt;
        }
        #endregion

        #region  ���������ɵ�������Ҫ�õ������ݣ�created by weiling.huang  at 2005-9-26
        /// <summary>
        /// ���������ɵ�������Ҫ�õ������ݣ�created by weiling.huang  at 2005-9-26
        /// </summary>
        public DataSet m_mthCreateExcelDataSet(clsOutPatientTableInfo_VO[] p_objResultArr)
        {
            DataTable p_dt = m_mthCreateExcelTable();
            DataRow dr;
            for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
            {
                dr = p_dt.NewRow();
                dr["��������"] = p_objResultArr[i1].m_strPatientName.Trim();
                dr["���￨��"] = p_objResultArr[i1].m_strPatientCardId.Trim();
                dr["��������"] = p_objResultArr[i1].m_strRecordDataTime;
                dr["��Ʊ����"] = p_objResultArr[i1].m_strInvoiceNo.Trim();
                dr["�Ը����"] = p_objResultArr[i1].m_strSBSUM_MNY;
                dr["���ʽ��"] = p_objResultArr[i1].m_strACCTSUM_MNY;
                dr["�ϼƽ��"] = p_objResultArr[i1].m_strTOTALSUM_MNY;
                dr["�շ�Ա"] = p_objResultArr[i1].m_strLastname_vchr.Trim();
                p_dt.Rows.Add(dr);
            }
            DataSet objds = new DataSet();
            objds.Tables.Add(p_dt);
            return objds;
        }
        #endregion

    }
    #endregion
}
