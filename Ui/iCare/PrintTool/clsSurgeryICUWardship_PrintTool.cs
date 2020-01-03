using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;
using System.Data;
using System.IO;
namespace iCare
{
    /// <summary>
    /// clsSurgeryICUWardship_PrintTool ��ժҪ˵����
    /// </summary>
    public class clsSurgeryICUWardship_PrintTool : frmSurgeryICUWardship,infPrintRecord
    {
        private  System.Data.DataTable printTable = new System.Data.DataTable();
        //���˻�����Ϣ
        protected clsPatient m_objCurrentPatient;
        protected clsRecordsDomain m_objRecordsDomain;
        //����ICU��Ϣ
        private ArrayList personMessageArr = new ArrayList();
        //��ҳ��ͳ��
        public int pageCount = 0;
        //��ǰ��ӡ��ҳ��
        int CurrentCount = 0;
        //��ǰ��ӡ�����ݼ��б�
        int CurrentDTRow = 0;
        //��ǰҳ��ӡ�����ݼ��б�ͷ
        int CurrentPageDTRow = 0;
        //��ǰ��ӡ�ĸ��ӿ�ʼ��
        int CurrentPrintColumn = 3;
        //��ǰ��ӡ���ݵ���󳤶�
        int RowLength = 0;
        //�����¼
        string SpecString = "";
        //��������
        string[] value_arry;
        /// <summary>
        /// Ԥ����־ TRUE��ʾԤ��
        /// </summary>
        public bool printView = false;

        SolidBrush brush1 = new SolidBrush(Color.Black);
        Font font_title = new Font("Arial", 14, System.Drawing.FontStyle.Bold);
        Font font1 = new Font("Arial", 11);
        Font font2 = new Font("Arial", 9);
        Font font3 = new Font("Arial", 6);
        int height_add = 5;
        int right_add = 10;
        int table_x, table_y, table_width, table_height;
        System.Drawing.RectangleF rec;
        System.Drawing.StringFormat format = new StringFormat();


        public clsSurgeryICUWardship_PrintTool()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        public clsSurgeryICUWardship_PrintTool(System.Data.DataTable table1, clsPatient Patient, ArrayList pMessageArr)
        {
            this.printTable = table1;
            this.m_objCurrentPatient = Patient;
            this.pageCount = 2;
            personMessageArr = pMessageArr;
        }

        /// <summary>
        /// ���ô�ӡ��Ϣ
        /// </summary>
        /// <param name="p_objPatient">����</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
        /// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            m_objCurrentPatient = p_objPatient;
            
        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���
        /// </summary>
        public void m_mthInitPrintContent()
        {
            
        }

        /// <summary>
        /// ���ô�ӡ���ݡ��������Ѿ�����ʱʹ�á�
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
        }

        /// <summary>
        /// ��ȡ��ӡ����
        /// </summary>
        /// <returns>��ӡ����</returns>
        public object m_objGetPrintInfo()
        {
            printTable = m_objGetPrintTable(m_objCurrentPatient);
            if (this.printTable.Rows.Count > 0)
                return printTable;
            else
                return null;
            //return printTable;
        }

        /// <summary>
        /// ��ʼ����ӡ����
        /// </summary>
        /// <param name="p_objArg">�ⲿ��Ҫ��ʼ���ı��������ݲ�ͬ��ʵ��ʹ��</param>
        public void m_mthInitPrintTool(object p_objArg)
        {
        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        /// <param name="p_objArg">�ⲿʹ�õ��ı��������ݲ�ͬ��ʵ��ʹ��</param>
        public void m_mthDisposePrintTools(object p_objArg)
        {
        }

        /// <summary>
        /// ��ӡ��ʼ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthBeginPrint(object p_objPrintArg)
        {


        }

        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            PrintPageEventArgs e = (PrintPageEventArgs)p_objPrintArg;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Font drawFont = new Font("Arial", 16);
            this.CurrentCount++;
            if (this.CurrentCount % 2 != 0)
            {
                printTheFront(e);
                if (!this.printView)
                    clsPublicFunction.ShowInformationMessageBox("�뷭תҳ�������ӡ��");
                //MessageBox.Show("�뷭תҳ�������ӡ��","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                printTheBack(e);
            }
            
            if (this.CurrentCount < this.pageCount)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            return;


        }
        public void m_mthPrintPage(PageSettings p_pstDefault, System.Data.DataTable table1, clsPatient Patient, ArrayList pMessageArr)
        {

            //frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
            //frmPreview.m_evtBeginPrint += new PrintEventHandler(frmPreview_m_evtBeginPrint);
            //frmPreview.m_evtEndPrint += new PrintEventHandler(frmPreview_m_evtEndPrint);
            //frmPreview.m_evtPrintContent += new PrintPageEventHandler(frmPreview_m_evtPrintContent);
            //frmPreview.m_evtPrintFrame += new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
            //frmPreview.m_pstDefaultPageSettings = p_pstDefault;
            //frmPreview.ShowDialog();
        }
        #region  �����¼�
        private void frmPreview_m_evtBeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            m_mthBeginPrint(e);

        }
        private void frmPreview_m_evtEndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            m_mthEndPrint(e);
        }
        private void frmPreview_m_evtPrintContent(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Font drawFont = new Font("Arial", 16);
            this.CurrentCount++;
            if (this.CurrentCount % 2 != 0)
            {
                printTheFront(e);
                clsPublicFunction.ShowInformationMessageBox("�뷭תҳ�������ӡ��");
                //MessageBox.Show("�뷭תҳ�������ӡ��","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                printTheBack(e);
            }
            if (this.CurrentCount < this.pageCount)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            return;


        }
        private void frmPreview_m_evtPrintFrame(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //			m_mthPrintTitleInfo(e);	
            //			m_mthPrintRectangleInfo(e);	
            //			m_mthPrintHeaderInfo(e);
        }
        #endregion
        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
        }




        #region �����ӡ
        /// <summary>
        /// �����ӡ
        /// </summary>
        public void printTheFront(System.Drawing.Printing.PrintPageEventArgs e)
        {

            #region ������
            this.table_x = 10;
            this.table_y = 0;
            this.table_width = 1600;
            this.table_height = 100;

            PrintTable title_table = new PrintTable(this.table_x, this.table_y, this.table_width, this.table_height, 3, 2);
            //����׳������������ҽԺ
            rec = new System.Drawing.RectangleF(title_table.Cell(0, 0).x, (title_table.Cell(0, 0).y), title_table.td_width, title_table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, font1, brush1, rec, format);
            rec = new System.Drawing.RectangleF(title_table.Cell(0, 0).x, (title_table.Cell(0, 0).y + 2), title_table.td_width, title_table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("________________________________________", font1, brush1, rec, format);
            //��� ICU �໤��¼
            rec = new System.Drawing.RectangleF(title_table.Cell(1, 0).x, (title_table.Cell(1, 0).y), title_table.td_width, title_table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("��� ICU �໤��¼", font_title, brush1, rec, format);

            #endregion

            #region ����ͷ
            this.table_x = 10;
            this.table_y = 70;
            this.table_width = 1600;
            this.table_height = 30;

            if (this.m_objCurrentPatient != null)
            {
                PrintTable head_table = new PrintTable(this.table_x, this.table_y, this.table_width, this.table_height, 1, 28);
                //����
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 0).x, (head_table.Cell(0, 0).y), head_table.td_width * 2+20, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("���� " + m_objCurrentPatient.m_StrName.ToString().Trim(), font1, brush1, rec, format);

                //�ձ�
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 2).x+20, (head_table.Cell(0, 2).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("�ձ� " + m_objCurrentPatient.m_StrSex.ToString().Trim(), font1, brush1, rec, format);
                //����
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 4).x, (head_table.Cell(0, 4).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("���� " + m_objCurrentPatient.m_ObjPeopleInfo.m_IntAge, font1, brush1, rec, format);
                //����
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 6).x, (head_table.Cell(0, 6).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("���� " + m_objCurrentPatient.m_strBedCode.ToString().Trim(), font1, brush1, rec, format);
                //����
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 8).x, (head_table.Cell(0, 8).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                //
                if (this.personMessageArr.Count > 0)
                {
                    e.Graphics.DrawString("���� " + personMessageArr[0].ToString().Trim(), font1, brush1, rec, format);
                }
                else
                {
                    e.Graphics.DrawString("���� ", font1, brush1, rec, format);

                }
                //ID��
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 10).x, (head_table.Cell(0, 10).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                if (this.personMessageArr.Count > 1)
                {
                    e.Graphics.DrawString("ID�� " + personMessageArr[1].ToString().Trim(), font1, brush1, rec, format);
                }
                else
                {
                    e.Graphics.DrawString("ID�� ", font1, brush1, rec, format);

                }
                //���/��������
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 12).x, (head_table.Cell(0, 12).y), head_table.td_width * 2+100, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                if (this.personMessageArr.Count > 2)
                {
                    e.Graphics.DrawString("���/�������� " + personMessageArr[2].ToString().Trim(), font1, brush1, rec, format);
                }
                else
                {
                    e.Graphics.DrawString("���/�������� ", font1, brush1, rec, format);

                }
                //��������
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 18).x, (head_table.Cell(0, 18).y), head_table.td_width * 4, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                if (this.personMessageArr.Count > 3)
                {
                    e.Graphics.DrawString("�������� " + personMessageArr[3].ToString().Trim(), font1, brush1, rec, format);
                }
                else
                {
                    e.Graphics.DrawString("�������� ", font1, brush1, rec, format);

                }
                //����/�� ICU ��
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 22).x, (head_table.Cell(0, 22).y), head_table.td_width * 3, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                if (this.personMessageArr.Count > 4)
                {
                    e.Graphics.DrawString("����/�� ICU �� " + personMessageArr[4].ToString().Trim(), font1, brush1, rec, format);
                }
                else
                {
                    e.Graphics.DrawString("����/�� ICU �� ", font1, brush1, rec, format);

                }
                //��
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 25).x, (head_table.Cell(0, 25).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("��", font1, brush1, rec, format);
                //��
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 25).x, (head_table.Cell(0, 25).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Far;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("�� " + ((this.CurrentCount + 1) / 2), font1, brush1, rec, format);
                //ҳ
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 27).x, (head_table.Cell(0, 27).y), head_table.td_width, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("ҳ", font1, brush1, rec, format);
            }
            else
            {
                PrintTable head_table = new PrintTable(this.table_x, this.table_y, this.table_width, this.table_height, 1, 28);
                //����
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 0).x, (head_table.Cell(0, 0).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("���� ", font1, brush1, rec, format);

                //�ձ�
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 2).x, (head_table.Cell(0, 2).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("�ձ� ", font1, brush1, rec, format);
                //����
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 4).x, (head_table.Cell(0, 4).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("���� ", font1, brush1, rec, format);
                //����
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 6).x, (head_table.Cell(0, 6).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("���� ", font1, brush1, rec, format);
                //����
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 8).x, (head_table.Cell(0, 8).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("���� ", font1, brush1, rec, format);
                //ID��
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 10).x, (head_table.Cell(0, 10).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("ID�� ", font1, brush1, rec, format);
                //���/��������
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 12).x, (head_table.Cell(0, 12).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("���/�������� ", font1, brush1, rec, format);
                //��������
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 18).x, (head_table.Cell(0, 18).y), head_table.td_width * 4, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("�������� ", font1, brush1, rec, format);
                //����/�� ICU ��
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 22).x, (head_table.Cell(0, 22).y), head_table.td_width * 3, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("����/�� ICU �� ", font1, brush1, rec, format);
                //��
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 25).x, (head_table.Cell(0, 25).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("��", font1, brush1, rec, format);
                //��
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 25).x, (head_table.Cell(0, 25).y), head_table.td_width * 2, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Far;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("�� " + ((this.CurrentCount + 1) / 2), font1, brush1, rec, format);
                //ҳ
                rec = new System.Drawing.RectangleF(head_table.Cell(0, 27).x, (head_table.Cell(0, 27).y), head_table.td_width, head_table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("ҳ", font1, brush1, rec, format);
            }

            #endregion

            #region ����ʽ

            this.table_y = 100;
            this.table_width = 1600;
            this.table_height = 1150 - 100;

            PrintTable table = new PrintTable(this.table_x, this.table_y, this.table_width, this.table_height, 39, 28);
            //������
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, 0).x, table.Cell(0, 0).y, table.Cell(table.row_count, 0).x, table.Cell(table.row_count, 0).y);
            //����ϱ�
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, 0).x, table.Cell(0, 0).y, table.Cell(0, table.column_count).x, table.Cell(0, table.column_count).y);

            //���м�ı��
            for (int i = 0; i < table.row_count; i++)
            {
                if (i > 6 && i < 36)
                {
                    if (i == 13 || i == 21 || i == 29)
                    {
                        e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(i, 0).x, table.Cell(i, 0).y, table.Cell(i, table.column_count).x, table.Cell(i, table.column_count).y);
                    }
                    else
                    {
                        e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(i, 1).x, table.Cell(i, 1).y, table.Cell(i, table.column_count).x, table.Cell(i, table.column_count).y);
                    }
                }
                else if (i > 36 && i < 39)
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(i, 3).x, table.Cell(i, 3).y, table.Cell(i, table.column_count).x, table.Cell(i, table.column_count).y);
                }
                else
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(i, 0).x, table.Cell(i, 0).y, table.Cell(i, table.column_count).x, table.Cell(i, table.column_count).y);
                }
                //	e.Graphics.DrawString(((int)(i)).ToString(),drawFont,drawBrush,float.Parse(table.Cell(0,i).x.ToString()),float.Parse(table.Cell(0,i).y.ToString()));
            }
            for (int i = 0; i < table.column_count; i++)
            {
                if (i == 1)
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(6, 1).x, table.Cell(6, 1).y, table.Cell(36, 1).x, table.Cell(36, 1).y);
                }
                else if (i == 2)
                {
                    continue;
                }
                else if (i == 3)
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, i).x, table.Cell(0, i).y, table.Cell(39, i).x, table.Cell(39, i).y);

                }
                else
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, i).x, table.Cell(0, i).y, table.Cell(table.column_count, i).x, table.Cell(36, i).y);
                }
            }
            //����ұ�
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, table.column_count).x, table.Cell(0, table.column_count).y, table.Cell(table.row_count, table.column_count).x, table.Cell(table.row_count, table.column_count).y);
            //����±�
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(table.row_count, 0).x, table.Cell(table.row_count, 0).y, table.Cell(table.row_count, table.column_count).x, table.Cell(table.row_count, table.column_count).y);

            int n1;
            //����һ�еĸ�������	
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, 0).x, table.Cell(0, 0).y, table.Cell(1, 3).x, table.Cell(1, 3).y);
            //����ʮ�еĸ�������	
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(11, 1).x, table.Cell(11, 1).y, table.Cell(10, 3).x, table.Cell(10, 3).y);
            //�������еĸ�������
            n1 = 4;
            for (int i = 3; i < table.column_count; i++)
            {
                e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(n1, i).x, table.Cell(n1, i).y, table.Cell(n1 - 1, i + 1).x, table.Cell(n1 - 1, i + 1).y);
            }
            //�������еĸ�������
            n1 = 5;
            for (int i = 3; i < table.column_count; i++)
            {
                e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(n1, i).x, table.Cell(n1, i).y, table.Cell(n1 - 1, i + 1).x, table.Cell(n1 - 1, i + 1).y);
            }
            //����22�еĸ�������
            n1 = 22;
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(22, 1).x, table.Cell(22, 1).y, table.Cell(21, 3).x, table.Cell(21, 3).y);
            for (int i = 3; i < table.column_count; i++)
            {
                e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(n1, i).x, table.Cell(n1, i).y, table.Cell(n1 - 1, i + 1).x, table.Cell(n1 - 1, i + 1).y);
            }
            #endregion

            #region ������


            //��Ŀ
            rec = new System.Drawing.RectangleF(table.Cell(0, 0).x, (table.Cell(0, 0).y + height_add), table.td_width * 3, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("��Ŀ", font1, brush1, rec, format);
            //ʱ��
            rec = new System.Drawing.RectangleF(table.Cell(0, 0).x, (table.Cell(0, 0).y), table.td_width * 3, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("ʱ��", font1, brush1, rec, format);
            //��λ
            rec = new System.Drawing.RectangleF(table.Cell(1, 0).x, (table.Cell(1, 0).y), table.td_width * 3, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��    λ", font1, brush1, rec, format);
            //��ʶ
            rec = new System.Drawing.RectangleF(table.Cell(2, 0).x, (table.Cell(2, 0).y), table.td_width * 3, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��    ʶ", font1, brush1, rec, format);
            //ͫ��(��/��)
            rec = new System.Drawing.RectangleF(table.Cell(3, 0).x, (table.Cell(3, 0).y), table.td_width * 3, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("ͫ��(��/��)", font1, brush1, rec, format);
            //�Թⷴ��(��/��)
            rec = new System.Drawing.RectangleF(table.Cell(4, 0).x, (table.Cell(4, 0).y), table.td_width * 3, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("�Թⷴ��(��/��)", font1, brush1, rec, format);
            //����
            rec = new System.Drawing.RectangleF(table.Cell(6, 1).x, (table.Cell(6, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��  ��", font1, brush1, rec, format);
            //δ����
            rec = new System.Drawing.RectangleF(table.Cell(7, 1).x, (table.Cell(7, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("δ����", font1, brush1, rec, format);
            //��  ��
            rec = new System.Drawing.RectangleF(table.Cell(8, 1).x, (table.Cell(8, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��  ��", font1, brush1, rec, format);
            //�� ��
            rec = new System.Drawing.RectangleF(table.Cell(9, 1).x, (table.Cell(9, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("�� ��", font1, brush1, rec, format);
            //CVP
            rec = new System.Drawing.RectangleF(table.Cell(12, 1).x, (table.Cell(12, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("CVP", font1, brush1, rec, format);
            //GS
            rec = new System.Drawing.RectangleF(table.Cell(22, 1).x, (table.Cell(22, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("GS", font1, brush1, rec, format);
            //NS
            rec = new System.Drawing.RectangleF(table.Cell(23, 1).x, (table.Cell(23, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("NS", font1, brush1, rec, format);
            //��  ��
            rec = new System.Drawing.RectangleF(table.Cell(28, 1).x, (table.Cell(28, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��  ��", font1, brush1, rec, format);
            //��  ��
            rec = new System.Drawing.RectangleF(table.Cell(29, 1).x, (table.Cell(29, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��  ��", font1, brush1, rec, format);
            //��  ��
            rec = new System.Drawing.RectangleF(table.Cell(31, 1).x, (table.Cell(31, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��  ��", font1, brush1, rec, format);
            //θ  Һ
            rec = new System.Drawing.RectangleF(table.Cell(33, 1).x, (table.Cell(33, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("θ  Һ", font1, brush1, rec, format);
            //S
            rec = new System.Drawing.RectangleF(table.Cell(10, 1).x, (table.Cell(10, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("S", font1, brush1, rec, format);
            //D
            rec = new System.Drawing.RectangleF(table.Cell(10, 1).x, (table.Cell(10, 1).y + this.height_add), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("D", font1, brush1, rec, format);
            //����Ʒ
            rec = new System.Drawing.RectangleF(table.Cell(21, 1).x, (table.Cell(21, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("����Ʒ", font2, brush1, rec, format);
            //�ۼ���
            rec = new System.Drawing.RectangleF(table.Cell(21, 1).x, (table.Cell(21, 1).y + this.height_add), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("�ۼ���", font2, brush1, rec, format);
            //ѭ��ϵͳ
            rec = new System.Drawing.RectangleF(table.Cell(6, 0).x, (table.Cell(6, 0).y), table.td_width, table.td_height * 7);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("ѭ  ��  ϵ  ͳ", font1, brush1, rec, format);
            //��ҩ������
            rec = new System.Drawing.RectangleF(table.Cell(13, 0).x, (table.Cell(13, 0).y), table.td_width, table.td_height * 8);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��  ҩ  ��  ��  ��", font1, brush1, rec, format);
            //����
            rec = new System.Drawing.RectangleF(table.Cell(21, 0).x, (table.Cell(21, 0).y), table.td_width, table.td_height * 8);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��   ��", font1, brush1, rec, format);
            //����
            rec = new System.Drawing.RectangleF(table.Cell(29, 0).x, (table.Cell(29, 0).y), table.td_width, table.td_height * 7);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��   ��", font1, brush1, rec, format);
            //�����¼
            rec = new System.Drawing.RectangleF(table.Cell(36, 0).x, (table.Cell(36, 0).y), table.td_width * 3, table.td_height * 3);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("�����¼", font1, brush1, rec, format);

            #endregion

            #region ������
            if (this.printTable.Rows.Count < 0)
                return;
            this.RowLength = 0;
            int cell_count = 1;
            this.CurrentPrintColumn = 3;
            string cell_value = "";
            this.CurrentDTRow = this.CurrentPageDTRow;
            while (this.CurrentDTRow < this.printTable.Rows.Count)
            {

                for (int i = 0; i < this.printTable.Columns.Count; i++)
                {
                    if (this.printTable.Rows[this.CurrentDTRow][i].ToString().Trim().Length > this.RowLength)
                    {
                        this.RowLength = this.printTable.Rows[this.CurrentDTRow][i].ToString().Trim().Length;
                        //ռ�õĸ�λ
                        cell_count = (int)this.RowLength / 6;
                    }
                }
                if ((this.CurrentPrintColumn + cell_count) > 28)
                {

                    break;
                }
                format = new StringFormat();
                rec = new System.Drawing.RectangleF(table.Cell(0, this.CurrentPrintColumn).x, (table.Cell(0, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                //ʱ��
                if (this.printTable.Columns.Contains("RecordDateofDay") && this.printTable.Columns.Contains("RecordTime"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["RecordDateofDay"].ToString().Trim() + " " + this.printTable.Rows[this.CurrentDTRow]["RecordTime"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��λ
                rec = new System.Drawing.RectangleF(table.Cell(1, this.CurrentPrintColumn).x, (table.Cell(1, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("PBODYPART"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["PBODYPART"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ʶ
                rec = new System.Drawing.RectangleF(table.Cell(2, this.CurrentPrintColumn).x, (table.Cell(2, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("PCONSCIOUSNESS"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["PCONSCIOUSNESS"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //ͫ��
                rec = new System.Drawing.RectangleF(table.Cell(3, this.CurrentPrintColumn).x, (table.Cell(3, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("PPUPIL"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["PPUPIL"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }

                this.value_arry = cell_value.Split("/".ToCharArray());
                //��
                if (this.value_arry.Length > 0)
                {
                    cell_value = value_arry[0].ToString().Trim();


                    rec = new System.Drawing.RectangleF(table.Cell(3, this.CurrentPrintColumn).x, (table.Cell(3, this.CurrentPrintColumn).y), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Near;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }
                //��
                if (this.value_arry.Length > 1)
                {
                    cell_value = value_arry[1].ToString().Trim();

                    rec = new System.Drawing.RectangleF(table.Cell(3, this.CurrentPrintColumn).x, (table.Cell(3, this.CurrentPrintColumn).y + this.height_add), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Far;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }

                format.Alignment = System.Drawing.StringAlignment.Near;
                //�Թⷴ��
                rec = new System.Drawing.RectangleF(table.Cell(4, this.CurrentPrintColumn).x, (table.Cell(4, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("PREFLECT"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["PREFLECT"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                this.value_arry = cell_value.Split("/".ToCharArray());
                //��
                if (this.value_arry.Length > 0)
                {
                    cell_value = value_arry[0].ToString().Trim();


                    rec = new System.Drawing.RectangleF(table.Cell(4, this.CurrentPrintColumn).x, (table.Cell(4, this.CurrentPrintColumn).y), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Near;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }
                //��
                if (this.value_arry.Length > 1)
                {
                    cell_value = value_arry[1].ToString().Trim();

                    rec = new System.Drawing.RectangleF(table.Cell(4, this.CurrentPrintColumn).x, (table.Cell(4, this.CurrentPrintColumn).y + this.height_add), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Far;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }

                format.Alignment = System.Drawing.StringAlignment.Near;
                //����
                rec = new System.Drawing.RectangleF(table.Cell(6, this.CurrentPrintColumn).x, (table.Cell(6, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("CTEMPERATURE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["CTEMPERATURE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //δ����
                rec = new System.Drawing.RectangleF(table.Cell(7, this.CurrentPrintColumn).x, (table.Cell(7, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("CSMALLTEMPERATURE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["CSMALLTEMPERATURE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //����
                rec = new System.Drawing.RectangleF(table.Cell(8, this.CurrentPrintColumn).x, (table.Cell(8, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("CHEARTRATE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["CHEARTRATE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //����
                rec = new System.Drawing.RectangleF(table.Cell(9, this.CurrentPrintColumn).x, (table.Cell(9, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("CHEARTRHYTHM"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["CHEARTRHYTHM"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //S/D
                rec = new System.Drawing.RectangleF(table.Cell(10, this.CurrentPrintColumn).x, (table.Cell(10, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("CSD"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["CSD"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);

                //CVP
                rec = new System.Drawing.RectangleF(table.Cell(12, this.CurrentPrintColumn).x, (table.Cell(12, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("CCVP"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["CCVP"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ҩ1-8   1
                rec = new System.Drawing.RectangleF(table.Cell(13, this.CurrentPrintColumn).x, (table.Cell(13, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("DPHYSIC1"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["DPHYSIC1"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ҩ1-8  2
                rec = new System.Drawing.RectangleF(table.Cell(14, this.CurrentPrintColumn).x, (table.Cell(14, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("DPHYSIC2"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["DPHYSIC2"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ҩ1-8  3
                rec = new System.Drawing.RectangleF(table.Cell(15, this.CurrentPrintColumn).x, (table.Cell(15, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("DPHYSIC3"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["DPHYSIC3"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ҩ1-8  4
                rec = new System.Drawing.RectangleF(table.Cell(16, this.CurrentPrintColumn).x, (table.Cell(16, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("DPHYSIC4"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["DPHYSIC4"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ҩ1-8  5
                rec = new System.Drawing.RectangleF(table.Cell(17, this.CurrentPrintColumn).x, (table.Cell(17, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("DPHYSIC5"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["DPHYSIC5"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ҩ1-8  6
                rec = new System.Drawing.RectangleF(table.Cell(18, this.CurrentPrintColumn).x, (table.Cell(18, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("DPHYSIC6"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["DPHYSIC6"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ҩ1-8  7
                rec = new System.Drawing.RectangleF(table.Cell(19, this.CurrentPrintColumn).x, (table.Cell(19, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("DPHYSIC7"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["DPHYSIC7"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��ҩ1-8  8
                rec = new System.Drawing.RectangleF(table.Cell(20, this.CurrentPrintColumn).x, (table.Cell(20, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("DPHYSIC8"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["DPHYSIC8"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //����Ʒ/�ۼ���
                rec = new System.Drawing.RectangleF(table.Cell(21, this.CurrentPrintColumn).x, (table.Cell(21, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("IBLOODPRODUCE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["IBLOODPRODUCE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                this.value_arry = cell_value.Split("/".ToCharArray());
                //��
                if (this.value_arry.Length > 0)
                {
                    cell_value = value_arry[0].ToString().Trim();


                    rec = new System.Drawing.RectangleF(table.Cell(21, this.CurrentPrintColumn).x, (table.Cell(21, this.CurrentPrintColumn).y), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Near;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }
                //��
                if (this.value_arry.Length > 1)
                {
                    cell_value = value_arry[1].ToString().Trim();

                    rec = new System.Drawing.RectangleF(table.Cell(21, this.CurrentPrintColumn).x, (table.Cell(21, this.CurrentPrintColumn).y + this.height_add), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Far;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }

                format.Alignment = System.Drawing.StringAlignment.Near;
                //GS
                rec = new System.Drawing.RectangleF(table.Cell(22, this.CurrentPrintColumn).x, (table.Cell(22, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("IGS"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["IGS"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //NS
                rec = new System.Drawing.RectangleF(table.Cell(23, this.CurrentPrintColumn).x, (table.Cell(23, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("INS"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["INS"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //����1-4
                rec = new System.Drawing.RectangleF(table.Cell(24, this.CurrentPrintColumn).x, (table.Cell(24, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);


                if (this.printTable.Columns.Contains("INNAME1"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["INNAME1"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                //����1-4
                rec = new System.Drawing.RectangleF(table.Cell(25, this.CurrentPrintColumn).x, (table.Cell(25, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("INNAME2"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["INNAME2"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                //����1-4
                rec = new System.Drawing.RectangleF(table.Cell(26, this.CurrentPrintColumn).x, (table.Cell(26, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("INNAME3"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["INNAME3"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                //����1-4
                rec = new System.Drawing.RectangleF(table.Cell(27, this.CurrentPrintColumn).x, (table.Cell(27, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("INNAME4"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["INNAME4"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //�����ۼ�
                rec = new System.Drawing.RectangleF(table.Cell(28, this.CurrentPrintColumn).x, (table.Cell(28, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("OTATAL"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["OTATAL"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //�����ۼ�
                rec = new System.Drawing.RectangleF(table.Cell(29, this.CurrentPrintColumn).x, (table.Cell(29, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("OEMIEMCTION"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["OEMIEMCTION"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);

                //�����¼
                rec = new System.Drawing.RectangleF(table.Cell(36, 3).x, (table.Cell(36, 3).y), table.td_width * 28, table.td_height * 3);

                if (this.printTable.Columns.Contains("SESPECIALLYNOTE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["SESPECIALLYNOTE"].ToString().Trim();

                }
                else
                {
                    cell_value = "";
                }
                SpecString += cell_value;
                e.Graphics.DrawString(SpecString, font2, brush1, rec, format);

                //��һ����¼

                this.CurrentDTRow++;

                //��λ
                this.CurrentPrintColumn += cell_count;
                cell_count = 1;
                this.RowLength = 0;
            }
            #endregion
        }

        #endregion

        #region �����ӡ
        /// <summary>
        /// �����ӡ
        /// </summary>
        public void printTheBack(System.Drawing.Printing.PrintPageEventArgs e)
        {


            #region ����ʽ
            PrintTable table = new PrintTable(10, 10, 1600, 1130, 33, 28);
            //������
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, 0).x, table.Cell(0, 0).y, table.Cell(table.row_count, 0).x, table.Cell(table.row_count, 0).y);
            //����ϱ�
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, 0).x, table.Cell(0, 0).y, table.Cell(0, table.column_count).x, table.Cell(0, table.column_count).y);

            //���м�ı��
            for (int i = 0; i < table.row_count; i++)
            {

                if (i == 1 || i == 13 || i == 22 || i == 28)
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(i, 0).x, table.Cell(i, 0).y, table.Cell(i, table.column_count).x, table.Cell(i, table.column_count).y);
                }
                else if (i == 24 || i == 25)
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(i, 2).x, table.Cell(i, 2).y, table.Cell(i, table.column_count).x, table.Cell(i, table.column_count).y);
                }
                else
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(i, 1).x, table.Cell(i, 1).y, table.Cell(i, table.column_count).x, table.Cell(i, table.column_count).y);
                }

            }
            for (int i = 0; i < table.column_count; i++)
            {
                if (i == 1)
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(1, i).x, table.Cell(1, i).y, table.Cell(table.row_count, i).x, table.Cell(table.row_count, i).y);
                }
                else if (i == 2)
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(22, i).x, table.Cell(22, i).y, table.Cell(26, i).x, table.Cell(26, i).y);
                }
                else
                {
                    e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, i).x, table.Cell(0, i).y, table.Cell(table.row_count, i).x, table.Cell(table.row_count, i).y);
                }
            }
            //����ұ�
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, table.column_count).x, table.Cell(0, table.column_count).y, table.Cell(table.row_count, table.column_count).x, table.Cell(table.row_count, table.column_count).y);
            //����±�
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(table.row_count, 0).x, table.Cell(table.row_count, 0).y, table.Cell(table.row_count, table.column_count).x, table.Cell(table.row_count, table.column_count).y);

            int n1;
            //����һ�еĸ�������	
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(0, 0).x, table.Cell(0, 0).y, table.Cell(1, 3).x, table.Cell(1, 3).y);
            //���ڶ�ʮ���еĸ�������	
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(24, 2).x, table.Cell(24, 2).y, table.Cell(23, 3).x, table.Cell(23, 3).y);
            //���ھ��еĸ�������
            n1 = 9;
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(9, 1).x, table.Cell(9, 1).y, table.Cell(8, 3).x, table.Cell(8, 3).y);
            for (int i = 3; i < table.column_count; i++)
            {
                e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(n1, i).x, table.Cell(n1, i).y, table.Cell(n1 - 1, i + 1).x, table.Cell(n1 - 1, i + 1).y);
            }
            //����ʮ���еĸ�������
            n1 = 12;
            e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(12, 1).x, table.Cell(12, 1).y, table.Cell(11, 3).x, table.Cell(11, 3).y);
            for (int i = 3; i < table.column_count; i++)
            {
                e.Graphics.DrawLine(new System.Drawing.Pen(Color.Black), table.Cell(n1, i).x, table.Cell(n1, i).y, table.Cell(n1 - 1, i + 1).x, table.Cell(n1 - 1, i + 1).y);
            }


            #endregion

            #region ������
            format = new StringFormat();

            //��Ŀ
            rec = new System.Drawing.RectangleF(table.Cell(0, 0).x, (table.Cell(0, 0).y + height_add), table.td_width * 3, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("��Ŀ", font1, brush1, rec, format);
            //ʱ��
            rec = new System.Drawing.RectangleF(table.Cell(0, 0).x, (table.Cell(0, 0).y), table.td_width * 3, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("ʱ��", font1, brush1, rec, format);
            //���ʱ��
            rec = new System.Drawing.RectangleF(table.Cell(1, 1).x, (table.Cell(1, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("���ʱ��", font1, brush1, rec, format);
            //�������ͺ�
            rec = new System.Drawing.RectangleF(table.Cell(2, 1).x, (table.Cell(2, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("�������ͺ�", font1, brush1, rec, format);
            //������ʽ
            rec = new System.Drawing.RectangleF(table.Cell(3, 1).x, (table.Cell(3, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("������ʽ", font1, brush1, rec, format);

            //Vt*************
            rec = new System.Drawing.RectangleF(table.Cell(4, 1).x, (table.Cell(4, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("V", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(4, 2).x, (table.Cell(4, 2).y + this.height_add), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("T", font3, brush1, rec, format);
            /*-------------------------------------------*/
            //Expried MV************************8
            rec = new System.Drawing.RectangleF(table.Cell(5, 1).x, (table.Cell(5, 1).y), table.td_width * 2, table.td_height / 2);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("Expried", font2, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(5, 1).x, (table.Cell(5, 1).y + table.td_height / 2), table.td_width * 2, table.td_height / 2);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("MV", font2, brush1, rec, format);
            /*----------------------------------------------*/
            /*-------------------------------------------*/
            //SO2(%)************************8
            rec = new System.Drawing.RectangleF(table.Cell(12, 1).x, (table.Cell(12, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("SO", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(12, 1).x + table.td_width / 4, (table.Cell(12, 1).y), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("2", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(12, 2).x, (table.Cell(12, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("(%)", font1, brush1, rec, format);
            /*----------------------------------------------*/

            //PCO2(K+)************************8
            rec = new System.Drawing.RectangleF(table.Cell(15, 1).x, (table.Cell(15, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("PCO", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(15, 1).x + table.td_width / 4, (table.Cell(15, 1).y), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("2", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(15, 2).x, (table.Cell(15, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("(K", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(15, 2).x + table.td_width / 3, (table.Cell(15, 2).y + table.td_height / 4), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("+", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(15, 2).x, (table.Cell(15, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString(")", font1, brush1, rec, format);
            /*----------------------------------------------*/

            //PO2(Na+)************************
            rec = new System.Drawing.RectangleF(table.Cell(16, 1).x, (table.Cell(16, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("PO", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(16, 1).x + table.td_width / 4, (table.Cell(16, 1).y), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("2", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(16, 2).x, (table.Cell(16, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("(Na", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(16, 2).x + table.td_width / 2, (table.Cell(16, 2).y + table.td_height / 4), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("+", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(16, 2).x, (table.Cell(16, 2).y), table.td_width - table.td_width / 4, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString(")", font1, brush1, rec, format);
            /*----------------------------------------------*/

            //HCO3(Cl+)************************
            rec = new System.Drawing.RectangleF(table.Cell(17, 1).x, (table.Cell(17, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("HCO", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(17, 1).x + table.td_width / 4, (table.Cell(17, 1).y), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("3", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(17, 2).x, (table.Cell(17, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("(Cl", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(17, 2).x + table.td_width / 2, (table.Cell(17, 2).y + table.td_height / 4), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("+", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(17, 2).x, (table.Cell(17, 2).y), table.td_width - table.td_width / 4, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString(")", font1, brush1, rec, format);
            /*----------------------------------------------*/
            //TCO2(Ca++)************************
            rec = new System.Drawing.RectangleF(table.Cell(18, 1).x, (table.Cell(18, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("TCO", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(18, 1).x + table.td_width / 4, (table.Cell(18, 1).y), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("2", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(18, 2).x, (table.Cell(18, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("(Ca", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(18, 2).x + table.td_width / 2, (table.Cell(18, 2).y + table.td_height / 4), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("++", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(18, 2).x, (table.Cell(18, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString(")", font1, brush1, rec, format);
            /*----------------------------------------------*/
            //O2CT************************
            rec = new System.Drawing.RectangleF(table.Cell(21, 1).x, (table.Cell(21, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("O", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(21, 1).x + table.td_width / 4, (table.Cell(21, 1).y), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("2", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(21, 2).x, (table.Cell(21, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("CT", font1, brush1, rec, format);


            /*----------------------------------------------*/


            //����ѹ��
            rec = new System.Drawing.RectangleF(table.Cell(6, 1).x, (table.Cell(6, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("����ѹ��", font1, brush1, rec, format);
            //��������
            rec = new System.Drawing.RectangleF(table.Cell(7, 1).x, (table.Cell(7, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��������", font1, brush1, rec, format);
            //Max.I.P
            rec = new System.Drawing.RectangleF(table.Cell(9, 1).x, (table.Cell(9, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("Max.I.P", font1, brush1, rec, format);
            //������
            rec = new System.Drawing.RectangleF(table.Cell(10, 1).x, (table.Cell(10, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("������", font1, brush1, rec, format);
            //��Ѫ��
            rec = new System.Drawing.RectangleF(table.Cell(13, 1).x, (table.Cell(13, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��Ѫ��", font1, brush1, rec, format);
            //PH
            rec = new System.Drawing.RectangleF(table.Cell(14, 1).x, (table.Cell(14, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("PH", font1, brush1, rec, format);
            //BE(Bun)
            rec = new System.Drawing.RectangleF(table.Cell(19, 1).x, (table.Cell(19, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("BE(Bun)", font1, brush1, rec, format);
            //SAT(Cr)
            rec = new System.Drawing.RectangleF(table.Cell(20, 1).x, (table.Cell(20, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("SAT(Cr)", font1, brush1, rec, format);
            //RA
            rec = new System.Drawing.RectangleF(table.Cell(22, 1).x, (table.Cell(22, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("RA", font1, brush1, rec, format);
            //PA
            rec = new System.Drawing.RectangleF(table.Cell(23, 1).x, (table.Cell(23, 1).y), table.td_width, table.td_height * 3);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("PA", font1, brush1, rec, format);

            //CmH2O
            rec = new System.Drawing.RectangleF(table.Cell(22, 2).x, (table.Cell(22, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("CmH", font2, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(22, 2).x + table.td_width / 2, (table.Cell(22, 2).y + this.height_add), table.td_width - table.td_width / 2, table.td_height - table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("2", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(22, 2).x, (table.Cell(22, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("0", font2, brush1, rec, format);

            //mmHg
            rec = new System.Drawing.RectangleF(table.Cell(22, 2).x, (table.Cell(22, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("mmHg", font2, brush1, rec, format);

            //Mean
            rec = new System.Drawing.RectangleF(table.Cell(24, 2).x, (table.Cell(24, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("Mean", font1, brush1, rec, format);
            //Wedge
            rec = new System.Drawing.RectangleF(table.Cell(25, 2).x, (table.Cell(25, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("Wedge", font1, brush1, rec, format);
            //CO/CI
            rec = new System.Drawing.RectangleF(table.Cell(26, 1).x, (table.Cell(26, 1).y), table.td_width * 2, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("CO/CI", font1, brush1, rec, format);
            //S
            rec = new System.Drawing.RectangleF(table.Cell(23, 2).x, (table.Cell(23, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("S", font1, brush1, rec, format);
            //D
            rec = new System.Drawing.RectangleF(table.Cell(23, 2).x, (table.Cell(23, 2).y + this.height_add), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("D", font1, brush1, rec, format);

            //FIO2
            rec = new System.Drawing.RectangleF(table.Cell(8, 1).x, (table.Cell(8, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("FIO", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(8, 1).x + table.td_width / 2, (table.Cell(8, 1).y), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("2", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(8, 1).x + table.td_width / 2 + 2, (table.Cell(8, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("(%)", font2, brush1, rec, format);


            /*----------------------------------------------*/
            //PEEP
            rec = new System.Drawing.RectangleF(table.Cell(8, 2).x, (table.Cell(8, 2).y + this.height_add), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("PEEP", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(21, 1).x, (table.Cell(21, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("O", font1, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(21, 1).x + table.td_width / 4, (table.Cell(21, 1).y), table.td_width - table.td_width / 5, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("2", font3, brush1, rec, format);

            rec = new System.Drawing.RectangleF(table.Cell(21, 2).x, (table.Cell(21, 2).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("CT", font1, brush1, rec, format);


            /*----------------------------------------------*/

            //̵ɫ
            rec = new System.Drawing.RectangleF(table.Cell(11, 1).x, (table.Cell(11, 1).y), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Near;
            e.Graphics.DrawString("̵ɫ", font1, brush1, rec, format);
            //̵��
            rec = new System.Drawing.RectangleF(table.Cell(11, 2).x, (table.Cell(11, 2).y + this.height_add), table.td_width, table.td_height);
            format.Alignment = System.Drawing.StringAlignment.Far;
            e.Graphics.DrawString("̵��", font1, brush1, rec, format);

            //����ϵͳ
            rec = new System.Drawing.RectangleF(table.Cell(1, 0).x, (table.Cell(1, 0).y), table.td_width, table.td_height * 12);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("��  ��  ϵ  ͳ", font1, brush1, rec, format);
            //ʵ���Ҽ��
            rec = new System.Drawing.RectangleF(table.Cell(13, 0).x, (table.Cell(13, 0).y), table.td_width, table.td_height * 9);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("ʵ  ��  ��  ��  ��", font1, brush1, rec, format);

            //S-G Cath
            rec = new System.Drawing.RectangleF(table.Cell(22, 0).x, (table.Cell(22, 0).y), table.td_width, table.td_height * 6);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.FormatFlags = System.Drawing.StringFormatFlags.DirectionVertical;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("S-G Cath", font1, brush1, rec, format);
            //Pressures
            rec = new System.Drawing.RectangleF(table.Cell(22, 0).x, (table.Cell(22, 0).y), table.td_width - table.td_width / 2, table.td_height * 6);
            format = new StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
            format.Alignment = System.Drawing.StringAlignment.Center;
            format.FormatFlags = System.Drawing.StringFormatFlags.DirectionVertical;
            format.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("Pressures", font2, brush1, rec, format);




            #endregion

            #region ������
            if (this.printTable.Rows.Count < 0)
                return;
            this.RowLength = 0;
            int cell_count = 1;
            this.CurrentPrintColumn = 3;
            string cell_value = "";
            this.CurrentDTRow = this.CurrentPageDTRow;


            while (this.CurrentDTRow < this.printTable.Rows.Count)
            {

                for (int i = 0; i < this.printTable.Columns.Count; i++)
                {
                    if (this.printTable.Rows[this.CurrentDTRow][i].ToString().Trim().Length > this.RowLength)
                    {
                        this.RowLength = this.printTable.Rows[this.CurrentDTRow][i].ToString().Trim().Length;
                        //ռ�õĸ�λ
                        cell_count = (int)this.RowLength / 6;
                    }
                }
                if ((this.CurrentPrintColumn + cell_count) > 28)
                {
                    this.pageCount += 2;
                    break;
                }
                format = new StringFormat();
                rec = new System.Drawing.RectangleF(table.Cell(0, this.CurrentPrintColumn).x, (table.Cell(0, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);
                format.Alignment = System.Drawing.StringAlignment.Near;
                format.LineAlignment = System.Drawing.StringAlignment.Center;
                //ʱ��
                if (this.printTable.Columns.Contains("RecordDateofDay") && this.printTable.Columns.Contains("RecordTime"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["RecordDateofDay"].ToString().Trim() + " " + this.printTable.Rows[this.CurrentDTRow]["RecordTime"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //���ʱ��
                rec = new System.Drawing.RectangleF(table.Cell(1, this.CurrentPrintColumn).x, (table.Cell(1, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BBLUSETIME"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BBLUSETIME"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //�������ͺ�
                rec = new System.Drawing.RectangleF(table.Cell(2, this.CurrentPrintColumn).x, (table.Cell(2, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BBLUSEMACHINETYPE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BBLUSEMACHINETYPE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //������ʽ
                rec = new System.Drawing.RectangleF(table.Cell(3, this.CurrentPrintColumn).x, (table.Cell(3, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BBLUSEMODE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BBLUSEMODE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);



                //VT
                rec = new System.Drawing.RectangleF(table.Cell(4, this.CurrentPrintColumn).x, (table.Cell(4, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BVT"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BVT"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //EXPIRED MV
                rec = new System.Drawing.RectangleF(table.Cell(5, this.CurrentPrintColumn).x, (table.Cell(5, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BEXPIREDMV"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BEXPIREDMV"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //����ѹ��
                rec = new System.Drawing.RectangleF(table.Cell(6, this.CurrentPrintColumn).x, (table.Cell(6, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BBLUESPRESSURE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BBLUESPRESSURE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��������
                rec = new System.Drawing.RectangleF(table.Cell(7, this.CurrentPrintColumn).x, (table.Cell(7, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BBLUSENUM"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BBLUSENUM"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //FIO2(%)  PEEP
                rec = new System.Drawing.RectangleF(table.Cell(8, this.CurrentPrintColumn).x, (table.Cell(8, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BFIO2PEEP"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BFIO2PEEP"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                this.value_arry = cell_value.Split("/".ToCharArray());
                //FIO2(%)
                if (this.value_arry.Length > 0)
                {
                    cell_value = value_arry[0].ToString().Trim();


                    rec = new System.Drawing.RectangleF(table.Cell(8, this.CurrentPrintColumn).x, (table.Cell(8, this.CurrentPrintColumn).y), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Near;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }
                //PEEP
                if (this.value_arry.Length > 1)
                {
                    cell_value = value_arry[1].ToString().Trim();

                    rec = new System.Drawing.RectangleF(table.Cell(8, this.CurrentPrintColumn).x, (table.Cell(8, this.CurrentPrintColumn).y + this.height_add), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Far;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }

                format.Alignment = System.Drawing.StringAlignment.Near;
                //MAX.IP
                rec = new System.Drawing.RectangleF(table.Cell(9, this.CurrentPrintColumn).x, (table.Cell(9, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BMAXIP"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BMAXIP"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //������
                rec = new System.Drawing.RectangleF(table.Cell(10, this.CurrentPrintColumn).x, (table.Cell(10, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BBLUSESOUND"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BBLUSESOUND"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //̵ɫ  ̵��
                rec = new System.Drawing.RectangleF(table.Cell(11, this.CurrentPrintColumn).x, (table.Cell(11, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BPHLEGMCOLOR"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BPHLEGMCOLOR"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                this.value_arry = cell_value.Split("/".ToCharArray());
                //̵ɫ
                if (this.value_arry.Length > 0)
                {
                    cell_value = value_arry[0].ToString().Trim();


                    rec = new System.Drawing.RectangleF(table.Cell(11, this.CurrentPrintColumn).x, (table.Cell(11, this.CurrentPrintColumn).y), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Near;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }
                //̵��
                if (this.value_arry.Length > 1)
                {
                    cell_value = value_arry[1].ToString().Trim();

                    rec = new System.Drawing.RectangleF(table.Cell(11, this.CurrentPrintColumn).x, (table.Cell(11, this.CurrentPrintColumn).y + this.height_add), table.td_width, table.td_height);
                    format.Alignment = System.Drawing.StringAlignment.Far;
                    e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                }

                format.Alignment = System.Drawing.StringAlignment.Near;
                //SO2(%)
                rec = new System.Drawing.RectangleF(table.Cell(12, this.CurrentPrintColumn).x, (table.Cell(12, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("BSQ2"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["BSQ2"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //��Ѫ��
                rec = new System.Drawing.RectangleF(table.Cell(13, this.CurrentPrintColumn).x, (table.Cell(13, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("TCOLLECTBLOODPOINT"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["TCOLLECTBLOODPOINT"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //PH
                rec = new System.Drawing.RectangleF(table.Cell(14, this.CurrentPrintColumn).x, (table.Cell(14, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("TPH"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["TPH"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //PC02(K+)
                rec = new System.Drawing.RectangleF(table.Cell(15, this.CurrentPrintColumn).x, (table.Cell(15, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("TPCO2"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["TPCO2"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //PO2(NA+)
                rec = new System.Drawing.RectangleF(table.Cell(16, this.CurrentPrintColumn).x, (table.Cell(16, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("TP02"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["TP02"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //HCO3(CL-)
                rec = new System.Drawing.RectangleF(table.Cell(17, this.CurrentPrintColumn).x, (table.Cell(17, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("THCO3"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["THCO3"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //TCO2(CA++)
                rec = new System.Drawing.RectangleF(table.Cell(18, this.CurrentPrintColumn).x, (table.Cell(18, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("TTCO2"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["TTCO2"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);

                //BE(BUN)
                rec = new System.Drawing.RectangleF(table.Cell(19, this.CurrentPrintColumn).x, (table.Cell(19, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("TBE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["TBE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //SAT(CR)
                rec = new System.Drawing.RectangleF(table.Cell(20, this.CurrentPrintColumn).x, (table.Cell(20, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("TSAT"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["TSAT"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //O2CT
                rec = new System.Drawing.RectangleF(table.Cell(21, this.CurrentPrintColumn).x, (table.Cell(21, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);


                if (this.printTable.Columns.Contains("TO2CT"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["TO2CT"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //CMH20MMHG
                rec = new System.Drawing.RectangleF(table.Cell(22, this.CurrentPrintColumn).x, (table.Cell(22, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("SCMH2O"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["SCMH2O"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //S/D
                rec = new System.Drawing.RectangleF(table.Cell(23, this.CurrentPrintColumn).x, (table.Cell(23, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("SSD"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["SSD"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //MEAN
                rec = new System.Drawing.RectangleF(table.Cell(24, this.CurrentPrintColumn).x, (table.Cell(24, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("SMEAN"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["SMEAN"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //WEDGE
                rec = new System.Drawing.RectangleF(table.Cell(25, this.CurrentPrintColumn).x, (table.Cell(25, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("SWEDGE"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["SWEDGE"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);
                //CO/CI
                rec = new System.Drawing.RectangleF(table.Cell(26, this.CurrentPrintColumn).x, (table.Cell(26, this.CurrentPrintColumn).y), table.td_width * cell_count, table.td_height);

                if (this.printTable.Columns.Contains("SCOCI"))
                {
                    cell_value = this.printTable.Rows[this.CurrentDTRow]["SCOCI"].ToString().Trim();
                }
                else
                {
                    cell_value = "";
                }
                e.Graphics.DrawString(cell_value, font2, brush1, rec, format);



                //��һ����¼

                this.CurrentDTRow++;
                this.CurrentPageDTRow = this.CurrentDTRow;
                //��λ
                this.CurrentPrintColumn += cell_count;
                cell_count = 1;
                this.RowLength = 0;


            }

            #endregion
        }
        #endregion


        #region �Զ����ӡ��
        public class PrintTable
        {
            /// <summary>
            /// //��ͷx
            /// </summary>
            public int table_x = 0;
            /// <summary>
            /// //��ͷy
            /// </summary>
            public int table_y = 0;
            /// <summary>
            /// ����
            /// </summary>
            public int table_width = 0;
            /// <summary>
            /// ��߶�
            /// </summary>
            public int table_height = 0;
            /// <summary>
            /// ����Ŀ
            /// </summary>
            public int row_count = 0;
            /// <summary>
            /// ����Ŀ
            /// </summary>
            public int column_count = 0;
            /// <summary>
            /// ����
            /// </summary>
            public int td_width = 0;
            /// <summary>
            /// ��߶�
            /// </summary>
            public int td_height = 0;


            /// <summary>
            /// ���캯�� ��ʼ�����ʽ
            /// </summary>
            /// <param name="table_x">��ͷx</param>
            /// <param name="table_y">��ͷy</param>
            /// <param name="table_width">���width</param>
            /// <param name="table_height">���height</param>
            public PrintTable(int table_x, int table_y, int table_width, int table_height, int row_count, int column_count)
            {
                //��ʼ����������С
                this.table_x = table_x;
                this.table_y = table_y;
                this.table_width = table_width;
                this.table_height = table_height;

                //�����к��е���Ŀ������ӵĴ�С                
                this.row_count = row_count;
                this.column_count = column_count;
                this.td_width = (int)this.table_width / this.column_count;
                this.td_height = (int)this.table_height / this.row_count;
            }

            /// <summary>
            /// �õ���ǰ�е�(x,y),(x2,y2);
            /// </summary>
            /// <param name="num">����</param>
            /// <returns></returns>
            public _row Row(int num)
            {
                int x = this.table_x;
                int y = this.table_x + num * this.td_height + this.table_y;
                int y2 = this.table_x + num * this.td_height + this.table_y;
                int x2 = this.table_x + this.table_width;
                return new _row(x, y, x2, y2);
            }

            /// <summary>
            /// �õ���ǰ�е�(x,y),(x2,y2);
            /// </summary>
            /// <param name="num">����</param>
            /// <returns></returns>
            public _column Column(int num)
            {
                int x = this.table_x + num * this.td_width;
                int y = this.table_x;
                int y2 = this.table_x + this.column_count * this.td_width;
                int x2 = this.table_x + num * this.td_width;
                return new _column(x, y, x2, y2);
            }

            #region �Զ���ṹ��
            /// <summary>
            /// �õ���ǰ���ӵ�(x,y),(x2,y2);
            /// </summary>
            /// <param name="num">����</param>
            /// <returns></returns>
            public _cell Cell(int x, int y)
            {
                _row row1 = Row(x);
                _column column1 = Column(y);

                return new _cell(column1.x, row1.y);
            }


            /// <summary>
            /// �нṹ
            /// </summary>
            public struct _row
            {
                public int x, y, x2, y2;

                public _row(int x, int y, int x2, int y2)
                {
                    this.x = x;
                    this.y = y;
                    this.x2 = x2;
                    this.y2 = y2;
                }
            }

            /// <summary>
            /// �нṹ
            /// </summary>
            public struct _column
            {
                public int x, y, x2, y2;

                public _column(int x, int y, int x2, int y2)
                {
                    this.x = x;
                    this.y = y;
                    this.x2 = x2;
                    this.y2 = y2;
                }
            }

            /// <summary>
            /// ���ӽṹ
            /// </summary>
            public struct _cell
            {
                public int x, y;

                public _cell(int x, int y)
                {
                    this.x = x;
                    this.y = y;

                }
            }
            #endregion

        }
        #endregion
        //protected override  void m_mthGetTransDataInfoArr(out clsTransDataInfo[] p_objTansDataInfoArr)
        //{
        //    //string m_strInPatientID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrEMRInPatientID;
        //    //string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");
        //    m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.SURGERYICUWARDSHIP);
        //    p_objTansDataInfoArr = null;
        //    if (m_objCurrentPatient == null)
        //    {
        //        return;
        //    }
        //    m_objRecordsDomain.m_lngGetTransDataInfoArr(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out p_objTansDataInfoArr);

        //}

        private  void m_mthInitDataTable(DataTable p_dtbRecordTable)
        {
            try
            {
                //��ż�¼ʱ����ַ���
                p_dtbRecordTable.Columns.Add("RecordDate");//0
                //��ż�¼���͵�intֵ
                DataColumn dcRecordType = new DataColumn("RecordType", typeof(int));
                p_dtbRecordTable.Columns.Add(dcRecordType);//1
                //��ż�¼��OpenDate�ַ���
                p_dtbRecordTable.Columns.Add("OpenDate");  //2
                //��ż�¼��ModifyDate�ַ���
                p_dtbRecordTable.Columns.Add("ModifyDate"); //3
                DataColumn dc1 = p_dtbRecordTable.Columns.Add("RecordDateofDay");//4
                dc1.DefaultValue = "";
                DataColumn dc2 = p_dtbRecordTable.Columns.Add("RecordTime");//5
                dc2.DefaultValue = "";

                p_dtbRecordTable.Columns.Add("PBODYPART", typeof(clsDSTRichTextBoxValue));//6
                p_dtbRecordTable.Columns.Add("PCONSCIOUSNESS", typeof(clsDSTRichTextBoxValue));//7
                p_dtbRecordTable.Columns.Add("PPUPIL", typeof(clsDSTRichTextBoxValue));//8
                //p_dtbRecordTable.Columns.Add("PPUPLRIGHT",typeof(clsDSTRichTextBoxValue));//8
                p_dtbRecordTable.Columns.Add("PREFLECT", typeof(clsDSTRichTextBoxValue));//9
                //p_dtbRecordTable.Columns.Add("PREFLECTRIGHT",typeof(clsDSTRichTextBoxValue));//9
                DataColumn dc3 = p_dtbRecordTable.Columns.Add("Sign");//10
                dc3.DefaultValue = "";
                p_dtbRecordTable.Columns.Add("CTEMPERATURE", typeof(clsDSTRichTextBoxValue));//11
                p_dtbRecordTable.Columns.Add("CSMALLTEMPERATURE", typeof(clsDSTRichTextBoxValue));//12

                p_dtbRecordTable.Columns.Add("CHEARTRATE", typeof(clsDSTRichTextBoxValue));//13
                p_dtbRecordTable.Columns.Add("CHEARTRHYTHM", typeof(clsDSTRichTextBoxValue));//14
                p_dtbRecordTable.Columns.Add("CSD", typeof(clsDSTRichTextBoxValue));//15
                p_dtbRecordTable.Columns.Add("CCVP", typeof(clsDSTRichTextBoxValue));//16

                p_dtbRecordTable.Columns.Add("DPHYSIC1", typeof(clsDSTRichTextBoxValue));//17
                p_dtbRecordTable.Columns.Add("DPHYSIC2", typeof(clsDSTRichTextBoxValue));//18
                p_dtbRecordTable.Columns.Add("DPHYSIC3", typeof(clsDSTRichTextBoxValue));//19
                p_dtbRecordTable.Columns.Add("DPHYSIC4", typeof(clsDSTRichTextBoxValue));//20
                p_dtbRecordTable.Columns.Add("DPHYSIC5", typeof(clsDSTRichTextBoxValue));//21
                p_dtbRecordTable.Columns.Add("DPHYSIC6", typeof(clsDSTRichTextBoxValue));//22
                p_dtbRecordTable.Columns.Add("DPHYSIC7", typeof(clsDSTRichTextBoxValue));//23
                p_dtbRecordTable.Columns.Add("DPHYSIC8", typeof(clsDSTRichTextBoxValue));//24

                p_dtbRecordTable.Columns.Add("IBLOODPRODUCE", typeof(clsDSTRichTextBoxValue));//25
                //p_dtbRecordTable.Columns.Add("IBLOODPRODUCEADD",typeof(clsDSTRichTextBoxValue));//26
                p_dtbRecordTable.Columns.Add("IGS", typeof(clsDSTRichTextBoxValue));//27
                p_dtbRecordTable.Columns.Add("INS", typeof(clsDSTRichTextBoxValue));//28
                p_dtbRecordTable.Columns.Add("INNAME1", typeof(clsDSTRichTextBoxValue));//29
                p_dtbRecordTable.Columns.Add("INNAME2", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("INNAME3", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("INNAME4", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("INTATAL", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("OTATAL", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("OEMIEMCTION", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("OGASTRICJUICE", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("OUTNAME1", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("OUTNAME2", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("OUTNAME3", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("OUTNAME4", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("SESPECIALLYNOTE", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BBLUSETIME", typeof(clsDSTRichTextBoxValue));//9

                p_dtbRecordTable.Columns.Add("BBLUSEMACHINETYPE", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BBLUSEMODE", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BVT", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BEXPIREDMV", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BBLUESPRESSURE", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BBLUSENUM", typeof(clsDSTRichTextBoxValue));//9

                p_dtbRecordTable.Columns.Add("BFIO2PEEP", typeof(clsDSTRichTextBoxValue));//9
                //p_dtbRecordTable.Columns.Add("BFI02PEEPRIGHT",typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BMAXIP", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BBLUSESOUND", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BPHLEGMCOLOR", typeof(clsDSTRichTextBoxValue));//9
                //p_dtbRecordTable.Columns.Add("BPHLEGMAMOUNT",typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("BSQ2", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("TCOLLECTBLOODPOINT", typeof(clsDSTRichTextBoxValue));//9

                p_dtbRecordTable.Columns.Add("TPH", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("TPCO2", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("TP02", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("THCO3", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("TTCO2", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("TBE", typeof(clsDSTRichTextBoxValue));//9

                p_dtbRecordTable.Columns.Add("TSAT", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("TO2CT", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("SCMH2O", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("SSD", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("SMEAN", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("SWEDGE", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("SCOCI", typeof(clsDSTRichTextBoxValue));//9
                p_dtbRecordTable.Columns.Add("CreateUserID");//9

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        private  void m_mthGetRecord()
        {
            if (m_objCurrentPatient == null) return;
            //��ȡ���˼�¼�б�
            clsTransDataInfo[] objTansDataInfoArr;
            m_objRecordsDomain.m_lngGetTransDataInfoArr(m_objCurrentPatient.m_StrEMRInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out objTansDataInfoArr);

            if (objTansDataInfoArr == null)
            {
                return;
            }

            //����¼ʱ��(CreateDate)����
            //modified by  thfzhang 2005-11-12 Σ�ػ�����Ҫ����
            //if (this.Name != "frmIntensiveTendMain_FC")
            //    m_mthSortTransData(ref objTansDataInfoArr);

            DataTable dtbAddBlank;
            clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
            objAddBlankDomain.m_lngGetBlankRecordContent(m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate, out dtbAddBlank);

            //��Ӽ�¼����DataTable
            object[][] objDataArr;
            for (int i1 = 0; i1 < objTansDataInfoArr.Length; i1++)
            {
                if (dtbAddBlank != null && dtbAddBlank.Rows.Count > 0)
                {
                    //���Ҽ�¼֮ǰ�з���м�¼,�в������
                    foreach (DataRow drtAdd in dtbAddBlank.Rows)
                    {
                        if (objTansDataInfoArr[i1].m_objRecordContent != null)
                        {
                            if (objTansDataInfoArr[i1].m_objRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") == DateTime.Parse(drtAdd["opendate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                            {
                                object[] objBlank = new object[5];
                                objBlank[1] = 100;
                                objBlank[2] = drtAdd[2].ToString();
                                printTable.Rows.Add(objBlank);
                                for (int k3 = 0; k3 < (Int32.Parse(drtAdd[3].ToString()) - 1); k3++)
                                {
                                    printTable.Rows.Add(new object[] { });
                                }
                                break;
                            }
                        }
                    }
                }

                //objDataArr = m_objGetRecordsValueArr(objTansDataInfoArr[i1]);

                //if (objDataArr == null)
                //    continue;

                //printTable.BeginLoadData();
                //for (int j2 = 0; j2 < objDataArr.Length; j2++)
                //{
                //    printTable.LoadDataRow(objDataArr[j2], true);
                //}
                //printTable.EndLoadData();
                
            }
        }
    }
}