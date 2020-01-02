using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Text.RegularExpressions;
using com.digitalwave.Utility;
using weCare.Core.Entity;
//using com.digitalwave.iCare.gui.HIS.Bill;
using com.digitalwave.iCare.middletier.CryptographyLib; 
using System.Runtime.InteropServices;

namespace com.digitalwave.iCare.gui.HIS
{
    #region סԺ���÷�����
    /// <summary>
    /// סԺ���÷�����
    /// </summary>
    public class clsPublic
    {
        #region ����
        /// <summary>
        /// XML�ļ���
        /// </summary>
        public static string XMLFile = Application.StartupPath + @"\LoginFile.xml";
        /// <summary>
        /// ��Ƶ�ļ����Ŵ���
        /// </summary>
        private static frmAnimation frmAvi = null;
        /// <summary>
        /// �Զ��屳����ɫ
        /// </summary>
        public static Color CustomBackColor = Color.FromArgb(235, 240, 235); //Color.FromArgb(240, 245, 240); //Color.FromArgb(212, 222, 219);
        /// <summary>
        /// PB������ļ�·��
        /// </summary>
        public static string PBLPath = Application.StartupPath + "\\PBReport.pbl";
        #endregion

        #region API
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;

        #region ����/����.�ؼ��ػ�
        /// <summary>
        /// �����ػ�
        /// </summary>
        /// <param name="hwnd"></param>
        public static void SuspendLayout(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_SETREDRAW, 0, IntPtr.Zero);
        }
        /// <summary>
        /// �����ػ�
        /// </summary>
        /// <param name="hwnd"></param>
        public static void ResumeLayout(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_SETREDRAW, 1, IntPtr.Zero);
        }
        #endregion

        #endregion

        #region ������Ƶ
        /// <summary>
        /// ������Ƶ
        /// </summary>
        /// <param name="AviFileName">��ƵAVI�ļ���</param>
        /// <param name="MessageInfo">��ʾ��Ϣ</param>
        public static void PlayAvi(string AviFileName, string MessageInfo)
        {
            frmAvi = new frmAnimation(AviFileName, MessageInfo);
            frmAvi.Show();
            frmAvi.Refresh();
            frmAvi.m_mthStart();
        }

        /// <summary>
        /// ������Ƶ
        /// </summary>
        /// <param name="MessageInfo">��ʾ��Ϣ</param>
        public static void PlayAvi(string MessageInfo)
        {
            frmAvi = new frmAnimation("findFILE.avi", MessageInfo);
            frmAvi.Show();
            frmAvi.Refresh();
            frmAvi.m_mthStart();
        }
        #endregion

        #region �ر���Ƶ
        /// <summary>
        /// �ر���Ƶ
        /// </summary>
        public static void CloseAvi()
        {
            if (frmAvi != null)
            {
                frmAvi.Close();
                frmAvi = null;
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// �������䣬���ݷ��ص�ֵ�õ����꣬�»���
        /// </summary>
        /// <param name="dteBirth">��������</param>
        /// <param name="intAge">����õ�������</param>
        /// <returns></returns>
        public static Age CalcAge(DateTime dteBirth, out int intAge)
        {
            Age age = Age.Year;
            intAge = 0;
            DateTime dteNow = DateTime.Now;
            int intYear = dteBirth.Year;
            int intMonth = dteBirth.Month;
            int intDay = dteBirth.Day;

            if ((dteNow.Year - intYear) > 0)
            {
                intAge = dteNow.Year - intYear;
                age = Age.Year;
            }
            else if ((dteNow.Month - intMonth) > 0)
            {
                intAge = dteNow.Month - intMonth;
                age = Age.Month;
            }
            else
            {
                intAge = dteNow.Day - intDay;
                age = Age.Day;
            }

            return age;

        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="dteBirth">��������</param>
        /// <returns></returns>
        public static string CalcAge(DateTime dteBirth)
        {
            int intAge = 0;
            string strAge = "";
            Age age = Age.Year;
            age = CalcAge(dteBirth, out intAge);
            switch (age)
            {
                case Age.Year:
                    strAge = intAge.ToString();
                    break;
                case Age.Month:
                    strAge = intAge.ToString() + "����";
                    break;
                case Age.Day:
                    strAge = intAge.ToString() + "��";
                    break;
            }
            return strAge;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public enum Age
        {
            /// <summary>
            /// ��
            /// </summary>
            Year,
            /// <summary>
            /// ��
            /// </summary>
            Month,
            /// <summary>
            /// ��
            /// </summary>
            Day
        }
        #endregion

        #region ������ת��Ϊ����
        /// <summary>
        /// ������ת��Ϊ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ConvertObjToDecimal(object obj)
        {
            try
            {
                if (obj != null && obj.ToString() != "")
                {
                    return Convert.ToDecimal(obj.ToString());

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        #endregion

        #region ����ֵ��������
        /// <summary>
        /// ����ֵ��������
        /// </summary>
        /// <param name="d">��ֵ</param>
        /// <param name="decimals">С��λ��</param>
        /// <returns></returns>
        public static decimal Round(decimal d, int decimals)
        {
            try
            {
                if (decimals < 1)
                {
                    return Convert.ToDecimal(Convert.ToInt32(d));
                }
                else
                {
                    string s = "0.";
                    for (int i = 0; i < decimals; i++)
                    {
                        s += "0";
                    }
                    return Convert.ToDecimal(d.ToString(s));
                }
            }
            catch
            {
                return d;
            }
        }
        #endregion

        #region LoginFile.XML��д����
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        public static string m_strReadXML(string parentnode, string childnode, string key)
        {
            string strRet = "";

            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null)
                    {
                        strRet = xndC.Attributes["value"].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                strRet = "";
            }
            return strRet;
        }

        /// <summary>
        /// д����
        /// </summary>
        /// <param name="parentnode"></param>
        /// <param name="childnode"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public static bool m_blnWriteXML(string parentnode, string childnode, string key, string val)
        {
            bool blnRet = false;
            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(XMLFile);

                    XmlNode xndP = xdoc.DocumentElement.SelectNodes(@"//" + parentnode)[0];
                    XmlNode xndC = xndP.SelectSingleNode(@"//" + childnode + @"[@key='" + key + @"']");

                    if (xndP != null && xndC != null)
                    {
                        xndC.Attributes["value"].Value = val;
                        xdoc.Save(XMLFile);
                        blnRet = true;
                    }
                }
            }
            catch
            {
                blnRet = false;
            }
            return blnRet;
        }
        #endregion

        #region ��ȡ�ָ��ַ�����ֵ
        /// <summary>
        /// ��ȡ�ָ��ַ�����ֵ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static List<string> m_ArrGettoken(string str, string sign)
        {
            List<string> val = new List<string>();

            if (str.Trim() == "")
            {
                return val;
            }

            int pos = 0;

            do
            {
                pos = str.IndexOf(sign);
                if (pos > 0)
                {
                    val.Add(str.Substring(0, pos));
                    str = str.Substring(pos + 1);
                }
                else
                {
                    val.Add(str);
                }
            } while (pos > 0);

            return val;
        }

        /// <summary>
        /// ��ȡ�ָ��ַ�����ֵ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static string m_strGettoken(ref string str, string sign)
        {
            string val = null;

            if (str.Trim() == "")
            {
                return val;
            }

            int pos = str.IndexOf(sign);

            if (pos > 0)
            {
                val = str.Substring(0, pos);
                str = str.Substring(pos + 1);
            }
            else
            {
                val = str;
            }

            return val;
        }
        #endregion

        #region Сд���ת��д
        /// <summary>
        /// Сд���ת��д
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string DoubleConvertToCurrency(double money)
        {
            string[] BigNumArr = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
            string[] UnitArr = { "��", "��", "Բ", "ʰ", "��", "Ǫ", "��", "ʰ", "��", "Ǫ", "��", "ʰ", "��", "Ǫ" };

            money = Math.Abs(money);

            string Money = money.ToString("0.00").Replace(".", "");

            int len = Money.Length;
            string s = "";
            string Result = "";

            for (int i = 1; i <= len; i++)
            {
                s = Money.Substring(len - i, 1);
                Result = string.Concat(BigNumArr[Int32.Parse(s)] + UnitArr[i - 1], Result);
            }

            Result = Result.Replace("ʰ��", "ʰ");
            Result = Result.Replace("��ʰ", "��");
            Result = Result.Replace("���", "��");
            Result = Result.Replace("��Ǫ", "��");
            Result = Result.Replace("����", "��");
            for (int i = 1; i <= 6; i++)
            {
                Result = Result.Replace("����", "��");
            }
            Result = Result.Replace("����", "��");
            Result = Result.Replace("����", "�|");
            Result = Result.Replace("����", "��");
            Result = Result.Replace("������", "");
            Result = Result.Replace("���", "");
            Result += "��";
            Result = Result.Replace("����", "��");

            return Result;
        }
        #endregion

        #region ������ת��Ϊ��д����
        /// <summary>
        /// ������ת��Ϊ��д����
        /// </summary>
        /// <param name="m_strNumber">��ʾ�����ַ���</param>
        /// <returns>���ش�д�ĺ��ֽ��</returns>
        public static string NumberToChineseNumber(string m_strNumber)
        {
            string m_strValue;
            switch (m_strNumber)
            {
                case "1":
                    m_strValue = "Ҽ";
                    break;
                case "2":
                    m_strValue = "��";
                    break;
                case "3":
                    m_strValue = "��";
                    break;
                case "4":
                    m_strValue = "��";
                    break;
                case "5":
                    m_strValue = "��";
                    break;
                case "6":
                    m_strValue = "½";
                    break;
                case "7":
                    m_strValue = "��";
                    break;
                case "8":
                    m_strValue = "��";
                    break;
                case "9":
                    m_strValue = "��";
                    break;
                case "0":
                    m_strValue = "��";
                    break;
                default:
                    m_strValue = m_strNumber;
                    break;
            }
            return m_strValue;
        }
        #endregion

        #region ��ȡϵͳ����
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="setid">�������</param>
        /// <returns></returns>
        public static int m_intGetSysParm(string setid)
        { 
            return (new weCare.Proxy.ProxyIP01()).Service.m_intGetSysParm(setid); 
        }
        #endregion

        #region ��ȡ��ǰ���𵥾ݺ�
        /// <summary>
        /// ��ȡ��ǰ���𵥾ݺ�
        /// </summary>
        /// <returns></returns>
        public static string m_strGetCurrPrepayNo()
        {
            string prepayno = clsPublic.m_strReadXML("BeInHospital", "CurrPrepayBillNo", "AnyOne");

            try
            {
                prepayno = Convert.ToString(int.Parse(prepayno) + 1).PadLeft(7, '0');
            }
            catch
            {
                prepayno = "";
                MessageBox.Show("��ǰ���𵥾ݺ����ô������������á�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return prepayno;
        }
        #endregion

        #region �жϰ��𵥾ݺ��Ƿ������Ź���
        /// <summary>
        /// �жϰ��𵥾ݺ��Ƿ������Ź���
        /// </summary>
        /// <param name="prepayno"></param>
        /// <returns></returns>
        public static bool m_blnCheckPrepayNoExpression(string prepayno)
        {
            bool Result = false;

            //��ȡ������ʽ
            string PrepayNoExp = clsPublic.m_strReadXML("BeInHospital", "PrepayBillNoExp", "AnyOne");
            if (PrepayNoExp.Trim() == "")
            {
                return true;
            }

            Regex r = new Regex(PrepayNoExp);
            Match m = r.Match(prepayno);
            if (m.Success)
            {
                Result = true;
            }

            return Result;
        }
        #endregion

        #region �жϰ��𵥾ݺ��Ƿ��ѱ�ʹ��
        /// <summary>
        /// �жϰ��𵥾ݺ��Ƿ��ѱ�ʹ��
        /// </summary>
        /// <param name="prepayno"></param>
        /// <param name="pretype">Ԥ�����ͣ�0 ���� 1 �ֹ�</param>
        /// <returns></returns>
        public static bool m_blnCheckPrepayNoIsUsed(string prepayno, int pretype)
        {
            clsDcl_PrePay objPrepay = new clsDcl_PrePay();

            return objPrepay.m_blnCheckPrepayBillNo(prepayno, pretype);
        }
        #endregion

        #region ��ȡ��ǰ��Ʊ��
        /// <summary>
        /// ��ȡ��ǰ��Ʊ��
        /// </summary>
        /// <param name="OperID">�տ�ԱID</param>
        /// <param name="Type">���ͣ� 1 סԺ��Ʊ 2 Ѻ�� 3 ���﷢Ʊ</param>
        /// <returns></returns>
        public static string m_strGetCurrInvoiceNo(string OperID, int Type)
        {
            string invono = "";

            clsDcl_Charge objCharge = new clsDcl_Charge();

            long l = objCharge.m_lngGetOperInvoNO(OperID, Type, out invono);

            int intNumLenght = 8;//Ԥ���𵥺������ֳ��ȳ���
            intNumLenght = clsPublic.m_intGetSysParm("1085");

            if (invono == "")
            {
                if (Type == 1)
                {
                    invono = clsPublic.m_strReadXML("BeInHospital", "CurrInvoiceNo", "AnyOne");

                    try
                    {
                        invono = invono.Substring(0, 2) + Convert.ToString(Convert.ToDecimal(invono.Substring(2)) + 1).PadLeft(8, '0');
                    }
                    catch
                    {
                        invono = "";
                        MessageBox.Show("��ǰ��Ʊ�����ô������������á�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (Type == 2)
                {
                    invono = clsPublic.m_strReadXML("BeInHospital", "CurrPrepayBillNo", "AnyOne");

                    //try
                    //{
                    //    invono = Convert.ToString(int.Parse(invono) + 1).PadLeft(7, '0');
                    //}
                    //catch
                    //{
                    //    invono = "";
                    //    MessageBox.Show("��ǰ���𵥾ݺ����ô������������á�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                    try
                    {
                        invono = Convert.ToString(int.Parse(invono) + 1).PadLeft(intNumLenght, '0');
                    }
                    catch
                    {
                        invono = "";
                        MessageBox.Show("��ǰ���𵥾ݺ����ô������������á�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (Type == 3)
                {
                }
            }
            else
            {
                //if (Type == 2)
                //{
                //    invono = invono.PadLeft(7, '0');
                //}
                if (Type == 2)
                {
                    if (invono.Length < intNumLenght)
                    {
                        MessageBox.Show("��ǰʹ�õķ�Ʊ����С�ڷ�Ʊ�������ó���,����!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        invono = "";
                    }
                    else
                    {
                        string strHead = invono.Substring(0, invono.Length - intNumLenght);
                        string strLast = invono.Substring(invono.Length - intNumLenght);
                        invono = strHead + Convert.ToString((int.Parse(strLast) + 1)).PadLeft(intNumLenght, '0');
                    }
                }
            }

            return invono;
        }
        #endregion

        #region ���浱ǰ��Ʊ��
        /// <summary>
        /// ���浱ǰ��Ʊ��
        /// </summary>
        /// <param name="OperID">����ԱID</param>
        /// <param name="InvoNo">��Ʊ��(Ѻ�𵥺�)</param>
        /// <param name="Type">���ͣ� 1 סԺ��Ʊ 2 Ѻ�� 3 ���﷢Ʊ</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        public static bool m_blnSaveCurrInvoiceNo(string OperID, string InvoNo, int Type)
        {
            bool b = false;
            clsDcl_Charge objCharge = new clsDcl_Charge();

            long l = objCharge.m_lngRegOperInvoNO(OperID, InvoNo, Type);
            if (l > 0)
            {
                b = true;
            }

            return b;
        }
        #endregion

        #region �жϷ�Ʊ���Ƿ������Ź���
        /// <summary>
        /// �жϷ�Ʊ���Ƿ������Ź���
        /// </summary>
        /// <param name="invono"></param>
        /// <returns></returns>
        public static bool m_blnCheckInvoExpression(string invono)
        {
            bool Result = false;

            //��ȡ������ʽ
            string InvoExp = clsPublic.m_strReadXML("BeInHospital", "InvoiceNoExp", "AnyOne");
            if (InvoExp.Trim() == "")
            {
                return true;
            }

            Regex r = new Regex(InvoExp);
            Match m = r.Match(invono);
            if (m.Success)
            {
                Result = true;
            }

            return Result;
        }
        #endregion

        #region �жϷ�Ʊ���Ƿ��ѱ�ʹ��
        /// <summary>
        /// �жϷ�Ʊ���Ƿ��ѱ�ʹ��
        /// </summary>
        /// <param name="invono"></param>
        /// <returns></returns>
        public static bool m_blnCheckInvoIsUsed(string invono)
        {
            clsDcl_Charge objCharge = new clsDcl_Charge();

            return objCharge.m_blnCheckInvoiceNo(invono);
        }
        #endregion

        #region Ʊ�ݴ�ӡ
        #region ��ӡѺ��
        /// <summary>
        /// ��ӡѺ��
        /// </summary>
        /// <param name="PrePayID"></param>
        /// <param name="RepNo">�ش��º�</param>
        public static void m_mthPrintPrepayBill(string PrePayID, string RepNo)
        {
            DataTable dtPrePay;
            clsDcl_PrePay objPrePay = new clsDcl_PrePay();
            long l = objPrePay.m_lngGetPrepayByPrePayID(PrePayID, out dtPrePay);
            if (l > 0 && dtPrePay.Rows.Count == 1)
            {
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn();

                dc = new DataColumn("��Ʊ���Ժ�", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��Ʊӡˢ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("סԺ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("֧������", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��������", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�������", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("Сд���", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��д���", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�տ�Ա����", typeof(string));
                dt.Columns.Add(dc);

                DataRow dr = dt.NewRow();
                if (RepNo.Trim() == "")
                {
                    dr["��Ʊ���Ժ�"] = dtPrePay.Rows[0]["PREPAYINV_VCHR"].ToString();
                    dr["סԺ��"] = "";
                }
                else
                {
                    dr["��Ʊ���Ժ�"] = RepNo;
                    dr["סԺ��"] = "*REPEAT(" + dtPrePay.Rows[0]["PREPAYINV_VCHR"].ToString() + ")*";
                }
                dr["����"] = Convert.ToDateTime(dtPrePay.Rows[0]["CREATE_DAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                dr["��Ʊӡˢ��"] = dtPrePay.Rows[0]["INPATIENTID_CHR"].ToString();
                dr["����"] = dtPrePay.Rows[0]["LASTNAME_VCHR"].ToString();

                string paytype = dtPrePay.Rows[0]["CUYCATE_INT"].ToString();
                if (paytype == "1")
                {
                    paytype = "�ֽ�";
                }
                else if (paytype == "2")
                {
                    paytype = "֧Ʊ";
                }
                else if (paytype == "3")
                {
                    paytype = "���п�";
                }
                else if (paytype == "4")
                {
                    paytype = "΢��2";
                }
                else if (paytype == "5")
                {
                    paytype = "����";
                }
                else if (paytype == "6")
                {
                    paytype = "֧����";         // ����.֧����
                }
                else if (paytype == "8")
                {
                    paytype = "΢��";
                }
                else if (paytype == "9")
                {
                    paytype = "֧����";         // ����.֧����
                }
                else
                {
                    paytype = "�ֽ�";
                }
                dr["֧������"] = paytype;
                dr["��������"] = "";
                dr["�������"] = "";
                double money = Convert.ToDouble(dtPrePay.Rows[0]["MONEY_DEC"].ToString());
                dr["Сд���"] = "��" + money.ToString("###,##0.00");
                dr["��д���"] = clsPublic.DoubleConvertToCurrency(money);
                dr["�տ�Ա����"] = dtPrePay.Rows[0]["EMPNO_CHR"].ToString();

                dt.Rows.Add(dr);
                //rptPrepayInvoice rptPrepayInvo = new rptPrepayInvoice();

                //try
                //{
                //    rptPrepayInvo.SetDataSource(dt);
                //    rptPrepayInvo.Refresh();
                //    rptPrepayInvo.PrintToPrinter(1, false, 0, 0);
                //    rptPrepayInvo.Close();
                //}
                //catch
                //{
                //    rptPrepayInvo.Close();
                //    MessageBox.Show("��ӡ����,�����Ƿ�װ��ӡ��!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                MessageBox.Show("��ȡԤ������Ϣʧ�ܣ���ӡ����ȡ����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region ��ӡסԺ��Ʊ
        /// <summary>
        /// ��ӡסԺ��Ʊ
        /// </summary>
        /// <param name="ChargeNo">�����</param>
        /// <param name="RepNo">�ش��</param>
        public static void m_mthPrintInvoiceBill(string ChargeNo, string RepNo)
        {
            DataTable dtInvoiceMain;
            DataTable dtInvoiceDet;
            DataTable dtPrepay;
            DataTable dtPayMode;
            DataTable dtItemDate;
            clsDcl_Charge objCharge = new clsDcl_Charge();
            long l = objCharge.m_lngGetInvoiceByChargeNo(ChargeNo, out dtInvoiceMain, out dtInvoiceDet, out dtPrepay, out dtPayMode, out dtItemDate);
            if (l > 0)
            {
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn();

                dc = new DataColumn("סԺ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��Ʊ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("סԺ����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("���㷽ʽ", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��ҩ", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�г�ҩ", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�в�ҩ", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��λ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("CT", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("MRI", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("X��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("B��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("���Ե�ͼ", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("���Ʒ�", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��Ѫ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("������", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("������", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("΢��2", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��������", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("����Ҵ�д", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�ܶ�", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("���ʽ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("���˽ɷѽ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("��Ԥ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("����", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�˿�", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("Ƿ��", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�շѵ�λ", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("���Ա", typeof(string));
                dt.Columns.Add(dc);
                dc = new DataColumn("�շ�Ա", typeof(string));
                dt.Columns.Add(dc);

                DataRow dr = dt.NewRow();
                dr["סԺ��"] = dtInvoiceMain.Rows[0]["inpatientid_chr"].ToString();
                dr["����"] = dtInvoiceMain.Rows[0]["deptname_vchr"].ToString();
                dr["��Ʊ��"] = dtInvoiceMain.Rows[0]["invoiceno_vchr"].ToString();

                DateTime invdate = Convert.ToDateTime(dtInvoiceMain.Rows[0]["invdate_dat"].ToString());
                dr["��"] = invdate.Year.ToString();
                dr["��"] = invdate.Month.ToString();
                dr["��"] = invdate.Day.ToString();

                dr["����"] = dtInvoiceMain.Rows[0]["lastname_vchr"].ToString();
                dr["סԺ����"] = Convert.ToDateTime(dtInvoiceMain.Rows[0]["inpatient_dat"].ToString()).ToString("yyyy-MM-dd");
                dr["���㷽ʽ"] = dtInvoiceMain.Rows[0]["paytypename_vchr"].ToString();

                dr["�ܶ�"] = dtInvoiceMain.Rows[0]["totalsum_mny"].ToString();
                #region �ܶ��дת��
                float fTotalmny = Convert.ToSingle(dtInvoiceMain.Rows[0]["totalsum_mny"].ToString());
                long lTotalmny = Decimal.ToInt64(Decimal.Truncate(Decimal.Multiply(new Decimal(fTotalmny), new decimal(100))));

                string space = "   ";
                string ChinaTotalmny = "";

                //ʮ��
                ChinaTotalmny = clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100000 * 100))) + space;
                //��
                lTotalmny = lTotalmny % (100000 * 100);
                ChinaTotalmny += clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10000 * 100))) + space;
                //ǧ
                lTotalmny = lTotalmny % (10000 * 100);
                ChinaTotalmny += clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1000 * 100))) + space;
                //��
                lTotalmny = lTotalmny % (1000 * 100);
                ChinaTotalmny += clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (100 * 100))) + space;
                //ʮ
                lTotalmny = lTotalmny % (100 * 100);
                ChinaTotalmny += clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10 * 100))) + space;
                //Ԫ
                lTotalmny = lTotalmny % (10 * 100);
                ChinaTotalmny += clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1 * 100))) + space;
                //��
                lTotalmny = lTotalmny % (1 * 100);
                ChinaTotalmny += clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (10))) + space;
                //��
                lTotalmny = lTotalmny % (10);
                ChinaTotalmny += clsPublic.NumberToChineseNumber(Convert.ToString(lTotalmny / (1)));
                #endregion

                dr["����Ҵ�д"] = ChinaTotalmny;
                dr["���ʽ��"] = dtInvoiceMain.Rows[0]["acctsum_mny"].ToString();
                dr["���˽ɷѽ��"] = dtInvoiceMain.Rows[0]["sbsum_mny"].ToString();
                dr["�շ�Ա"] = dtInvoiceMain.Rows[0]["empno_chr"].ToString();

                dt.Rows.Add(dr);
                //rptInChargeInvoice rptChargeInvoice = new rptInChargeInvoice();

                //try
                //{
                //    rptChargeInvoice.SetDataSource(dt);
                //    rptChargeInvoice.Refresh();
                //    rptChargeInvoice.PrintToPrinter(1, false, 0, 0);
                //    rptChargeInvoice.Close();
                //}
                //catch
                //{
                //    rptChargeInvoice.Close();
                //    MessageBox.Show("��ӡ����,�����Ƿ�װ��ӡ��!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                MessageBox.Show("��ȡԤ������Ϣʧ�ܣ���ӡ����ȡ����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #endregion

        #region ��¼���β�ѯ���Ĳ�����Ϣ(��Ժ�Ǽ���ˮ�š�סԺ�š����ƿ���)��LoginFile.XML
        /// <summary>
        /// ��¼���β�ѯ���Ĳ�����Ϣ(��Ժ�Ǽ���ˮ�š�סԺ�š����ƿ���)��LoginFile.XML
        /// </summary>
        public static void m_mthWriteParm(string RegID, string Zyh, string CardNo)
        {
            clsPublic.m_blnWriteXML("BeInHospital", "CurrRegisterID", "AnyOne", RegID);
            clsPublic.m_blnWriteXML("BeInHospital", "CurrInPatientID", "AnyOne", Zyh);
            clsPublic.m_blnWriteXML("BeInHospital", "CurrCardNo", "AnyOne", CardNo);
        }
        #endregion

        #region ��ȡǰһ�μ�¼�Ĳ�����Ϣ(סԺ��)
        /// <summary>
        /// ��ȡǰһ�μ�¼�Ĳ�����Ϣ(סԺ��)
        /// </summary>
        /// <returns></returns>
        public static string m_strReadParmZyh()
        {
            return clsPublic.m_strReadXML("BeInHospital", "CurrInPatientID", "AnyOne");
        }
        #endregion

        #region ��ȡǰһ�μ�¼�Ĳ�����Ϣ(���ƿ���)
        /// <summary>
        /// ��ȡǰһ�μ�¼�Ĳ�����Ϣ(���ƿ���)
        /// </summary>
        /// <returns></returns>
        public static string m_strReadParmCardNo()
        {
            return clsPublic.m_strReadXML("BeInHospital", "CurrCardNo", "AnyOne");
        }
        #endregion

        #region ���ݴ���DataWindow/DataStore����
        /// <summary>
        /// ���ݴ���DataWindow����
        /// </summary>
        /// <param name="DW"></param>
        public static void ExportDataWindow(Sybase.DataWindow.DataWindowControl DW)
        {
            SaveFileDialog FD = new SaveFileDialog();
            FD.Filter = "Excel �ĵ�|*.xls";
            FD.Title = "����";
            FD.ShowDialog();

            if (FD.FileName.Trim() != "")
            {
                DW.SaveAs(FD.FileName.Trim(), Sybase.DataWindow.FileSaveAsType.Excel, true, Sybase.DataWindow.FileSaveAsEncoding.Utf8);
            }
        }

        /// <summary>
        /// DataStore����
        /// </summary>
        /// <param name="DS"></param>
        public static void ExportDataStore(Sybase.DataWindow.DataStore DS)
        {
            SaveFileDialog FD = new SaveFileDialog();
            FD.Filter = "Excel �ĵ�|*.xls";
            FD.Title = "����";
            FD.ShowDialog();

            if (FD.FileName.Trim() != "")
            {
                DS.SaveAs(FD.FileName.Trim(), Sybase.DataWindow.FileSaveAsType.Excel, true, Sybase.DataWindow.FileSaveAsEncoding.Utf8);
            }
        }
        #endregion

        #region ���ݴ���DataWindow/DataStore������HTML,��ת��EXCEL
        /// <summary>
        /// ���ݴ���DataWindow/DataStore������HTML,��ת��EXCEL
        /// </summary>
        /// <param name="DW"></param>
        /// <param name="volExcel"></param>
        public static void ExportDataWindow(Sybase.DataWindow.DataWindowControl DW, clsVolDatawindowToExcel[] volExcel)
        {
            SaveFileDialog FD = new SaveFileDialog();
            FD.Filter = "Excel �ĵ�|*.xls";
            FD.Title = "����";
            FD.ShowDialog();
            string filename = FD.FileName.Trim();

            if (filename != "")
            {
                DW.SaveAs(filename, Sybase.DataWindow.FileSaveAsType.HtmlTable, false, Sybase.DataWindow.FileSaveAsEncoding.Utf8);
                if (volExcel != null)
                {
                    //����
                    object MissingValue = Type.Missing;

                    Excel.Application Excel_app = new Excel.ApplicationClass();
                    //��Excel�ĵ�
                    Excel.Workbook Excel_work = Excel_app.Workbooks.Open(filename, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue);
                    //��ȡ��ǰ�����б�
                    Excel.Worksheet mySheet = (Excel.Worksheet)Excel_work.Worksheets[1];

                    for (int i = volExcel.Length - 1; i >= 0; i--)
                    {
                        Excel.Range range = null;
                        range = (Excel.Range)mySheet.Rows.get_Item(1, Type.Missing);
                        //�ڶ��в������
                        range.Rows.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                        for (int j = 0; j < volExcel[i].m_title_text.Length; j++)
                        {
                            //�ϲ���
                            if (volExcel[i].m_endcommn[j] == "ALL")
                            {
                                range = mySheet.get_Range(volExcel[i].m_firstcommn[j], mySheet.Cells[1, mySheet.UsedRange.Columns.Count]);
                            }
                            else
                            {
                                range = mySheet.get_Range(volExcel[i].m_firstcommn[j], volExcel[i].m_endcommn[j]);
                            }
                            range.Merge(MissingValue);

                            range.Value2 = volExcel[i].m_title_text[j];
                            if (volExcel[i].m_HorizontalAlignment[j] == "0")
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            }
                            else if (volExcel[i].m_HorizontalAlignment[j] == "L")
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            }
                            else
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            }
                            range.RowHeight = volExcel[i].m_rowheight[j];
                        }

                    }

                    mySheet.UsedRange.Rows.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    mySheet = null;
                    Excel_app.ActiveWorkbook.Close(true, filename.ToString(), null);
                    Excel_app.Workbooks.Close();
                    Excel_app.Quit();
                    Excel_app = null;
                }
            }
        }
        /// <summary>
        /// ���ݴ���DataWindow/DataStore������HTML,��ת��EXCEL
        /// </summary>
        /// <param name="DS"></param>
        /// <param name="volExcel"></param>
        public static void ExportDataStore(Sybase.DataWindow.DataStore DS, clsVolDatawindowToExcel[] volExcel)
        {
            SaveFileDialog FD = new SaveFileDialog();
            FD.Filter = "Excel �ĵ�|*.xls";
            FD.Title = "����";
            FD.ShowDialog();
            string filename = FD.FileName.Trim();

            if (filename != "")
            {
                DS.SaveAs(filename, Sybase.DataWindow.FileSaveAsType.HtmlTable, true, Sybase.DataWindow.FileSaveAsEncoding.Utf8);
                if (volExcel != null)
                {

                    //����
                    object MissingValue = Type.Missing;
                    Excel.Application Excel_app = new Excel.ApplicationClass();

                    //��Excel�ĵ�
                    Excel.Workbook Excel_work = Excel_app.Workbooks.Open(filename, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue, MissingValue, MissingValue,
                     MissingValue);
                    //��ȡ��ǰ�����б�
                    Excel.Worksheet mySheet = (Excel.Worksheet)Excel_work.Worksheets[1];

                    for (int i = volExcel.Length - 1; i >= 0; i--)
                    {
                        Excel.Range range = null;
                        range = (Excel.Range)mySheet.Rows.get_Item(1, Type.Missing);
                        //�ڶ��в������
                        range.Rows.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
                        for (int j = 0; j < volExcel[i].m_title_text.Length; j++)
                        {
                            //�ϲ���
                            if (volExcel[i].m_endcommn[j] == "ALL")
                            {
                                range = mySheet.get_Range(volExcel[i].m_firstcommn[j], mySheet.Cells[1, mySheet.UsedRange.Columns.Count]);
                            }
                            else
                            {
                                range = mySheet.get_Range(volExcel[i].m_firstcommn[j], volExcel[i].m_endcommn[j]);
                            }
                            range.Merge(MissingValue);

                            range.Value2 = volExcel[i].m_title_text[j];
                            if (volExcel[i].m_HorizontalAlignment[j] == "0")
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            }
                            else if (volExcel[i].m_HorizontalAlignment[j] == "L")
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            }
                            else
                            {
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            }
                            range.RowHeight = volExcel[i].m_rowheight[j];
                        }

                    }

                    mySheet.UsedRange.Rows.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    mySheet = null;
                    Excel_app.ActiveWorkbook.Close(true, filename.ToString(), null);
                    Excel_app.Workbooks.Close();
                    Excel_app.Quit();
                    Excel_app = null;
                }
            }
        }
        #endregion

        #region ���ݴ���DataWindow/DataStoreԤ����ӡ
        /// <summary>
        /// DataWindowԤ����ӡ
        /// </summary>
        /// <param name="DW"></param>
        public static void PrintDialog(Sybase.DataWindow.DataWindowControl DW)
        {
            frmPrintDialog PrintDialog = new frmPrintDialog(DW);
            PrintDialog.ShowDialog();
        }
        /// <summary>
        /// DataStoreԤ����ӡ
        /// </summary>
        /// <param name="DS"></param>
        public static void PrintDialog(Sybase.DataWindow.DataStore DS)
        {
            frmPrintDialog PrintDialog = new frmPrintDialog(DS);
            PrintDialog.ShowDialog();
        }
        /// <summary>
        /// ָ�����͵�DataStoreԤ����ӡ
        /// </summary>
        /// <param name="DS"></param>
        public static void PrintDialog(Sybase.DataWindow.DataStore DS, string strType, DataTable p_dtPutMed, string p_strEmpID)
        {
            frmPrintDialog PrintDialog = new frmPrintDialog(DS, strType, p_dtPutMed, p_strEmpID);
            PrintDialog.ShowDialog();
        }
        #endregion

        #region ���ݴ�������ͨ�ַ���ת��Ϊ�����ַ���
        /// <summary>
        /// ���ݴ�������ͨ�ַ���ת��Ϊ�����ַ���
        /// </summary>
        /// <param name="str">��ͨ�ַ���</param>        
        /// <param name="len">һ���ַ���</param>
        /// <param name="newstr">�����ַ���</param>
        public static void m_mthConvertNewLineStrLbl(string str, int len, ref string newstr)
        {
            if (str.Length <= len)
            {
                newstr += str;
            }
            else
            {
                newstr += str.Substring(0, len) + "~r~n";
                m_mthConvertNewLineStrLbl(str.Substring(len), len, ref newstr);
            }
        }

        /// <summary>
        /// ���ݴ�������ͨ�ַ���ת��Ϊ�����ַ���
        /// </summary>
        /// <param name="str">��ͨ�ַ���</param>        
        /// <param name="len">һ���ַ���</param>
        /// <param name="newstr">�����ַ���</param>
        public static void m_mthConvertNewLineStrCol(string str, int len, ref string newstr)
        {
            if (str.Length <= len)
            {
                newstr += str;
            }
            else
            {
                newstr += str.Substring(0, len) + " ";
                m_mthConvertNewLineStrCol(str.Substring(len), len, ref newstr);
            }
        }
        #endregion

        #region ��ȡϵͳ����
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="parmcode">��������</param>
        /// <returns>ֵ</returns>        
        public static string m_strGetSysparm(string parmcode)
        {
            return (new weCare.Proxy.ProxyIP01()).Service.m_strGetSysparm(parmcode); 
        }
        #endregion

        #region ��ȡסԺҽ���������ID
        /// <summary>
        /// ��ȡסԺҽ���������ID
        /// </summary>
        /// <returns></returns>
        public static List<string> m_mthGetYBPayID()
        {
            return clsPublic.m_ArrGettoken(clsPublic.m_strGetSysparm("0008"), ";");
        }
        #endregion

        #region ѡ���ӡ����ӡ
        /// <summary>
        /// ѡ���ӡ����ӡ
        /// </summary>
        /// <param name="DW">���ݴ���</param>
        /// <param name="ShowPrintDialog">��ʾ������ӡ����</param>
        public static void ChoosePrintDialog(Sybase.DataWindow.DataWindowControl DW, bool ShowCancelDialog)
        {
            PrintDialog pDiag = new PrintDialog();
            pDiag.UseEXDialog = true;
            pDiag.AllowSomePages = true;
            if (pDiag.ShowDialog() == DialogResult.OK)
            {
                DW.PrintProperties.PrinterName = pDiag.PrinterSettings.PrinterName;

                if (pDiag.PrinterSettings.PrintRange == System.Drawing.Printing.PrintRange.SomePages)
                {
                    DW.PrintProperties.PageRange = pDiag.PrinterSettings.FromPage.ToString() + "-" + pDiag.PrinterSettings.ToPage.ToString();
                }

                DW.Print(ShowCancelDialog);
            }
            pDiag = null;
        }

        /// <summary>
        /// ѡ���ӡ����ӡ
        /// </summary>
        /// <param name="DS">���ݴ洢</param>
        /// <param name="ShowCancelDialog">��ʾ������ӡ����</param>
        public static void ChoosePrintDialog(Sybase.DataWindow.DataStore DS, bool ShowCancelDialog)
        {
            PrintDialog pDiag = new PrintDialog();
            pDiag.UseEXDialog = true;
            pDiag.AllowSomePages = true;
            if (pDiag.ShowDialog() == DialogResult.OK)
            {
                DS.PrintProperties.PrinterName = pDiag.PrinterSettings.PrinterName;

                if (pDiag.PrinterSettings.PrintRange == System.Drawing.Printing.PrintRange.SomePages)
                {
                    DS.PrintProperties.PageRange = pDiag.PrinterSettings.FromPage.ToString() + "-" + pDiag.PrinterSettings.ToPage.ToString();
                }

                DS.Print(ShowCancelDialog);
            }
            pDiag = null;
        }
        #endregion

        #region ���ÿ����Ϣ
        /// <summary>
        /// ���ÿ����Ϣ
        /// </summary>
        /// <param name="frmMdi"></param>
        /// <param name="Index"></param>
        /// <param name="Info"></param>
        public static void SetShortCutInfo(Form frmMdi, int Index, string Info)
        {
            StatusBar objInfoBar = new StatusBar();
            foreach (Control c in frmMdi.Controls)
            {
                if (c is StatusBar)
                {
                    objInfoBar = c as StatusBar;
                    break;
                }
            }

            if (objInfoBar != null)
            {
                objInfoBar.Panels[Index].Text = Info;
                objInfoBar.Panels[Index].ToolTipText = "";
            }
        }
        #endregion

        #region ��ȡ����ICARE�����
        /// <summary>
        /// ��ȡ����ICARE�����
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="UserID"></param>
        /// <param name="Pwd"></param>
        public static void m_mthGetICareParm(out string ServerName, out string UserID, out string Pwd)
        {
            ServerName = "ICARE";
            UserID = "";
            Pwd = "";

            try
            {
                UserID = clsTextTool.s_strReadSecurityString("", "bytICare", "userid", clsSymmetricAlgorithm.enmSecurityStringType.ORACLE);
                clsSymmetricAlgorithm symmetricAlgorithm = new  clsSymmetricAlgorithm();
                clsSymmetricAlgorithm.enmSymmetricAlgorithmType symmetricAlgorithmType =  clsSymmetricAlgorithm.enmSymmetricAlgorithmType.RIJNDAEL;
                symmetricAlgorithm.m_strKey = "daXPc0JKOKOElQOBJRBV9Y81qE8qB3ceXNeynhFOc3c=";
                string password = clsTextTool.s_strReadSecurityString("", "bytICare", "password",  clsSymmetricAlgorithm.enmSecurityStringType.ORACLE);
                Pwd = symmetricAlgorithm.m_strDecrypt(password, symmetricAlgorithmType);
            }
            catch
            {
                UserID = "ICARE";
                Pwd = "ICARE";
            }

            #region ��
            //string tmpfs = clsPublic.XMLFile;
            //clsPublic.XMLFile = Application.StartupPath + @"\HISYB.xml";

            //if (clsPublic.m_strReadXML("ICARE", "ORAServerName", "AnyOne") != "")
            //{
            //    ServerName = clsPublic.m_strReadXML("ICARE", "ORAServerName", "AnyOne");
            //}
            //if (clsPublic.m_strReadXML("ICARE", "ORAUserID", "AnyOne") != "")
            //{
            //    UserID = clsPublic.m_strReadXML("ICARE", "ORAUserID", "AnyOne");
            //}
            //if (clsPublic.m_strReadXML("ICARE", "ORAPassWord", "AnyOne") != "")
            //{
            //    Pwd = clsPublic.m_strReadXML("ICARE", "ORAPassWord", "AnyOne");
            //}

            //clsPublic.XMLFile = tmpfs;
            #endregion
        }
        #endregion

        #region ��ȡ�����Է���
        /// <summary>
        /// ��ȡ�����Է���
        /// </summary>
        /// <param name="RegisterID">������Ժ�Ǽ�ID</param>
        /// <param name="EmpID">��ǰ����ԱID</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        public static bool m_blnChargeContinueItem(string RegisterID, string EmpID)
        {
            bool ret = false;
            clsDcl_Charge objCharge = new clsDcl_Charge();
            long l = objCharge.AutoChargeContinueItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), EmpID, RegisterID);
            if (l > 0)
            {
                ret = true;
            }
            return ret;
        }
        #endregion

        #region �������Դ
        /// <summary>
        /// �������Դ
        /// </summary>
        /// <param name="LogTitle">LOG����</param>
        /// <param name="SQLExpression">SQL���ʽ</param>
        public static void WriteSQLLog(string LogTitle, string SQLExpression)
        {
            //clsLogText log = new clsLogText();
            //log.Log2File(@"d:\code\log.txt", "/******" + LogTitle + "******/ \r\n " + SQLExpression);
            //log = null;
            com.digitalwave.Utility.Log.Output(SQLExpression);
        }
        #endregion

        #region 11�������⴦��
        /// <summary>
        /// 11�������⴦��
        /// </summary>
        /// <param name="HospitalName">ҽԺ����</param>
        /// <param name="objTxt">���ű༭��</param>
        /// <param name="Flag">��־</param>
        public static void CardNo11Init(string HospitalName, System.Windows.Forms.TextBox objTxt, ref bool Flag)
        {
            if (HospitalName.Contains("��ɽ"))
            {
                objTxt.MaxLength = 11;
                Flag = true;
            }
            else
            {
                //objTxt.MaxLength = 10;
                Flag = true;
            }
        }

        /// <summary>
        /// 11�������⴦��
        /// </summary>
        /// <param name="HospitalName">ҽԺ����</param>
        /// <param name="objTxt">���ű༭��</param>
        /// <param name="Flag">��־</param>
        //public static void CardNo11Init(string HospitalName, TextBox objTxt, ref bool Flag)
        //{
        //    if (HospitalName.Contains("��ɽ"))
        //    {
        //        objTxt.MaxLength = 11;
        //        Flag = true;
        //    }
        //    else
        //    {
        //        //objTxt.MaxLength = 10;
        //        Flag = true;
        //    }
        //}

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="Val"></param>
        public static string CardNo11Value(string Val)
        {
            if (Val.Trim().Length > 10)
            {
                Val = Val.Trim().Substring(1, 10);
            }

            return Val;
        }
        #endregion

        #region ���ʱ�䷶Χ
        /// <summary>
        /// ���ʱ�䷶Χ: ��Ҫ���ڼ���ѯʱ���
        /// </summary>
        /// <param name="Days">��������</param>
        /// <param name="BeginDate">��ʼ����</param>
        /// <param name="EndDate">��������</param>
        /// <returns>true �ڷ�Χ�� false ���ڷ�Χ��</returns>
        public static bool m_blnCheckDateRange(string Days, string BeginDate, string EndDate)
        {
            bool b = true;

            Days = Days.Trim();

            int pos = Days.IndexOf("��");

            if (pos >= 0)
            {
                Days = Days.Remove(0, pos + 1);
                if (Days != "" && Microsoft.VisualBasic.Information.IsNumeric(Days))
                {
                    TimeSpan ts = Convert.ToDateTime(EndDate).Subtract(Convert.ToDateTime(BeginDate));

                    if (ts.Days > Convert.ToInt32(Days))
                    {
                        b = false;
                        MessageBox.Show("ʱ��γ���������ķ�Χ��(ϵͳ��ǰ���������Ϊ: " + Days + "����)", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            return b;
        }
        #endregion

        #region ת��Ϊ�������ַ�
        /// <summary>
        /// ת��Ϊ�������ַ�
        /// </summary>
        /// <param name="OrgStr">ԭʼ�ַ���</param>
        /// <param name="Sign">�ָ���</param>
        /// <returns></returns>
        public static string m_strConvertSingleQuoteMark(string OrgStr, string Sign)
        {
            string s = "";

            if (OrgStr.Trim() == "")
            {
                return s;
            }
            else
            {
                List<string> ar = clsPublic.m_ArrGettoken(OrgStr, Sign);

                if (ar.Count > 0)
                {
                    for (int i = 0; i < ar.Count; i++)
                    {
                        s += "'" + ar[i] + "'" + Sign;
                    }

                    return s.Substring(0, s.Length - 1);
                }
            }

            return s;
        }
        #endregion

        #region �����·�Ʊ��
        /// <summary>
        /// �����·�Ʊ��
        /// </summary>        
        public static string m_strNextInvoiceNo(string InvoNo)
        {
            try
            {
                int maxint = Convert.ToInt32(InvoNo.Substring(2, 8)) + 1;
                InvoNo = InvoNo.Substring(0, 2) + maxint.ToString("00000000");
            }
            catch
            {
                InvoNo = "AA00000000";
            }

            return InvoNo;
        }

        #endregion

        #region ��DATATABLE������EXCEL
        /// <summary>
        /// ��DATATABLE������EXCEL
        /// </summary>
        /// <param name="DGV"></param>
        public static void m_mthDataTableExportToExcel(DataGridView DGV)
        {
            DataTable dtExport = new DataTable("dtExoprt");

            string strColName = "";
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
                if (DGV.Columns[i].Visible == false)
                {
                    continue;
                }

                strColName = DGV.Columns[i].HeaderText.Replace("(", "").Replace(")", "").Trim();

                if (DGV.Columns[i].ValueType.FullName.ToLower() == "system.numeric" || DGV.Columns[i].ValueType.FullName.ToLower() == "system.decimal")
                {
                    dtExport.Columns.Add(strColName, typeof(decimal));
                }
                else
                {
                    dtExport.Columns.Add(strColName, typeof(string));
                }
            }

            DataRow dr;
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                dr = dtExport.NewRow();

                int row = 0;
                for (int j = 0; j < DGV.Columns.Count; j++)
                {
                    if (DGV.Columns[j].Visible == false)
                    {
                        continue;
                    }

                    dr[row] = DGV.Rows[i].Cells[j].Value;
                    row++;
                }

                dtExport.Rows.Add(dr);
            }

            DataSet dsExport = new DataSet();
            dsExport.Tables.Add(dtExport);
            com.digitalwave.iCare.common.ExcelExporter excel = new com.digitalwave.iCare.common.ExcelExporter(dsExport);
            bool b = excel.m_mthExport();
            if (b)
            {
                MessageBox.Show("����Excel�ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("����Excelʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            dtExport.Dispose();
            dsExport.Tables.Clear();
            dsExport.Dispose();
        }
        #endregion

        #region ͨ����˴�
        /// <summary>
        /// ͨ����˴�
        /// </summary>
        /// <param name="EmpID">���Σ������ID</param> 
        /// <returns></returns>
        public static DialogResult m_dlgConfirm(out string EmpID)
        {
            EmpID = "";

            frmAuditing f = new frmAuditing();
            if (f.ShowDialog() == DialogResult.OK)
            {
                EmpID = f.EmpID;
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }
        /// <summary>
        /// ͨ����˴�
        /// </summary>
        /// <param name="EmpNo">��Σ�����˹���</param>
        /// <param name="EmpID">���Σ������ID</param> 
        /// <returns></returns>
        public static DialogResult m_dlgConfirm(string EmpNo, out string EmpID)
        {
            EmpID = "";

            frmAuditing f = new frmAuditing(EmpNo);
            if (f.ShowDialog() == DialogResult.OK)
            {
                EmpID = f.EmpID;
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }
        /// <summary>
        /// ͨ����˴�
        /// </summary>
        /// <param name="EmpID">���Σ������ID</param>
        /// <param name="EmpName">���Σ����������</param>
        /// <returns></returns>
        public static DialogResult m_dlgConfirm(out string EmpID, out string EmpName)
        {
            EmpID = "";
            EmpName = "";

            frmAuditing f = new frmAuditing();
            if (f.ShowDialog() == DialogResult.OK)
            {
                EmpID = f.EmpID;
                EmpName = f.EmpName;
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }
        /// <summary>
        /// ͨ����˴�
        /// </summary>
        /// <param name="EmpNo">��Σ�����˹���</param>
        /// <param name="EmpID">���Σ������ID</param>
        /// <param name="EmpName">���Σ����������</param>
        /// <returns></returns>
        public static DialogResult m_dlgConfirm(string EmpNo, out string EmpID, out string EmpName)
        {
            EmpID = "";
            EmpName = "";

            frmAuditing f = new frmAuditing(EmpNo);
            f.txtEmpNo.Text = EmpNo;
            if (f.ShowDialog() == DialogResult.OK)
            {
                EmpID = f.EmpID;
                EmpName = f.EmpName;
                return DialogResult.Yes;
            }

            return DialogResult.No;
        }

        public static DialogResult m_dlgConfimByDefault(string EmpNo)
        {
            frmAuditing f = new frmAuditing(EmpNo, 2);
            if (f.ShowDialog() == DialogResult.OK)
            {
                return DialogResult.Yes;
            }
            return DialogResult.No;
        }
        
        #endregion

        #region ��������ת����(���ַ���ֹ)
        /// <summary>
        /// ��������ת����(���ַ���ֹ)
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string m_mthGetAge(string strInput)
        {
            string strAge = "";
            try
            {
                strAge = new clsBrithdayToAge().m_strGetAge(strInput);
                System.Text.ASCIIEncoding objAscii = new System.Text.ASCIIEncoding();
                byte[] s = objAscii.GetBytes(strAge);
                for (int i1 = 0; i1 < s.Length; i1++)
                {
                    if ((int)s[i1] == 63)
                    {
                        strAge = strAge.Substring(0, i1) + " " + strAge.Substring(i1, 1);
                        break;
                    }
                }
            }
            catch (Exception)
            { }
            
            return strAge;
        }
        #endregion

        #region ��ȡϵͳ����INTֵ
        /// <summary>
        /// ��ȡϵͳ����INTֵ
        /// </summary>
        /// <param name="hasValue"></param>
        /// <param name="ParmCode"></param>
        /// <returns></returns>
        public static int m_intConvertParm(Hashtable hasValue, string ParmCode)
        {
            int intValue = -999;
            try
            {
                if (hasValue.Contains(ParmCode))
                {
                    intValue = int.Parse(hasValue[ParmCode].ToString());
                }
            }
            catch
            {
                MessageBox.Show("�봴�������� ���� " + ParmCode, "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return intValue;
        }
        #endregion

        #region ��ȡϵͳ����STRINGֵ
        /// <summary>
        /// ��ȡϵͳ����STRINGֵ
        /// </summary>
        /// <param name="hasValue"></param>
        /// <param name="ParmCode"></param>
        /// <returns></returns>
        public static string m_strConvertParm(Hashtable hasValue, string ParmCode)
        {
            String strValue = "";
            try
            {
                if (hasValue.Contains(ParmCode))
                {
                    strValue = hasValue[ParmCode].ToString().Trim();
                }
            }
            catch
            {
                MessageBox.Show("�봴�������� ���� " + ParmCode, "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return strValue;
        }
        #endregion

        #region ��ȡϵͳ����BOOLֵ
        /// <summary>
        /// ��ȡϵͳ����BOOLֵ
        /// </summary>
        /// <param name="hasValue"></param>
        /// <param name="ParmCode"></param>
        /// <returns></returns>
        public static bool m_blnConvertParm(Hashtable hasValue, string ParmCode)
        {
            bool blnValue = false;
            try
            {
                if (hasValue.Contains(ParmCode))
                {
                    blnValue = hasValue[ParmCode].ToString().Trim() == "1";
                }
            }
            catch
            {
                MessageBox.Show("�봴�������� ���� " + ParmCode, "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return blnValue;
        }
        #endregion

        #region ��ȡϵͳ����
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="setidarr">�����������</param>
        /// <returns></returns>
        public static Dictionary<string, string> m_hasGetSysSetting(List<string> setidarr)
        { 
            return (new weCare.Proxy.ProxyIP01()).Service.m_hasGetSysSetting(setidarr); 
        }

        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="parmcodearr">������������</param>
        /// <returns></returns>
        public static Dictionary<string, string> m_hasGetSysParm(List<string> parmcodearr)
        {
            return (new weCare.Proxy.ProxyIP01()).Service.m_hasGetSysParm(parmcodearr);
        }
        #endregion

        #region ��ѯ�˲����Ƿ������ﴦ��δ�����(ǰ��Ҫ����������������˵����ﴦ�����õ�סԺ�м��)
        /// <summary>
        ///  ��ѯ�˲����Ƿ������ﴦ��δ�����(ǰ��Ҫ����������������˵����ﴦ�����õ�סԺ�м��)
        /// </summary>
        /// <param name="p_strRegisterId">����סԺ�ǼǺ�</param>
        /// <param name="p_strMessage">���ز���δ���ѵĴ�����</param>
        /// <returns></returns>
        public static long m_lngSelectPatientNoPayRecipe(string p_strRegisterId, out string p_strMessage)
        {
            return (new weCare.Proxy.ProxyIP01()).Service.m_lngSelectPatientNoPayRecipe(p_strRegisterId, out p_strMessage); 
        }

        #endregion
    }
    #endregion

    #region ListView�Ƚ�������
    /// <summary>
    /// ListView�Ƚ�������
    /// </summary>
    public class ListViewItemSort : IComparer
    {
        #region ����
        /// <summary>
        /// �к�
        /// </summary>
        private int col = 0;

        /// <summary>
        /// ��������
        /// </summary>
        private SortOrder sort;
        #endregion

        #region ListView������
        /// <summary>
        /// ListView������
        /// </summary>
        /// <param name="column"></param>
        /// <param name="order"></param>
        /// <param name="lv"></param>
        public ListViewItemSort(int column, SortOrder order, ListView lv)
        {
            col = column;
            sort = order;

            string coltext = "";
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                coltext = lv.Columns[i].Text;
                coltext = coltext.Replace("��", "");
                coltext = coltext.Replace("��", "");
                lv.Columns[i].Text = coltext;
            }

            if (order == SortOrder.Ascending)
            {
                lv.Columns[col].Text = lv.Columns[col].Text + "��";
            }
            else if (order == SortOrder.Descending)
            {
                lv.Columns[col].Text = lv.Columns[col].Text + "��";
            }
        }
        #endregion

        #region �ȽϺ���
        /// <summary>
        /// �ȽϺ���
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            int ret = 0;
            string coltext1 = ((ListViewItem)x).SubItems[col].Text;
            string coltext2 = ((ListViewItem)y).SubItems[col].Text;

            try
            {
                if (coltext1.IndexOf(".") >= 0 || coltext2.IndexOf(".") >= 0)
                {
                    ret = (Convert.ToDouble(coltext1) > Convert.ToDouble(coltext2) ? 1 : -1);
                }
                else
                {
                    ret = (Convert.ToInt32(coltext1) > Convert.ToInt32(coltext2) ? 1 : -1);
                }
            }
            catch
            {
                ret = String.Compare(coltext1, coltext2);	//ת���д�,˵���Ƚϵ��������Ͳ�ͬ.
            }

            if (sort != SortOrder.Ascending)
            {
                ret = -ret;
            }

            return ret;
        }
        #endregion
    }
    #endregion

    #region ����VO
    /// <summary>
    /// �������VO
    /// </summary>
    [Serializable]
    public class clsCheckCat_VO  
    {
        /// <summary>
        /// �������ID
        /// </summary>
        public string CatID = "";
        /// <summary>
        /// �����������
        /// </summary>
        public string CatName = "";
        /// <summary>
        /// ���������
        /// </summary>
        public decimal CatSum = 0;
    }

    /// <summary>
    /// ��Ʊ����VO
    /// </summary>
    [Serializable]
    public class clsInvoiceCat_VO  
    {
        /// <summary>
        /// ��Ʊ����ID
        /// </summary>
        public string CatID = "";
        /// <summary>
        /// ��Ʊ��������
        /// </summary>
        public string CatName = "";
        /// <summary>
        /// ��Ʊ������
        /// </summary>
        public decimal CatSum = 0;
    }
    #endregion

    #region �շ���Ŀ����VO
    /// <summary>
    /// �շ���Ŀ����VO
    /// </summary>
    [Serializable]
    public class clsParmItem_VO  
    {
        /// <summary>
        /// ��ĿID
        /// </summary>
        public string ItemID = "";
        /// <summary>
        /// ��ĿCODE
        /// </summary>
        public string ItemCode = "";
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemName = "";
        /// <summary>
        /// ����
        /// </summary>
        public decimal ItemAmout = 0;
        /// <summary>
        /// ����(����)ID
        /// </summary>
        public string DeptID = "";
        /// <summary>
        /// ����(����)����
        /// </summary>
        public string DeptName = "";
        /// <summary>
        /// �Ƿ�����¼�븺��(����)
        /// </summary>
        public bool AllowNegative = true;

        /// <summary>
        /// �Һű�־�� 0 ȫ�� 1 �ѹҺ� 2 δ�Һ�
        /// </summary>
        public string RegFlag = "";
        /// <summary>
        /// ������־�� 0 ȫ�� 1 ���� 2 ����
        /// </summary>
        public string RecFlag = "";
        /// <summary>
        /// ְ��ID:  00 ȫ�� ...
        /// </summary>
        public string DutyName = "";
        /// <summary>
        /// (ÿ��)��ʼʱ��
        /// </summary>
        public string BeginTime = "00:00";
        /// <summary>
        /// (ÿ��)����ʱ��
        /// </summary>
        public string EndTime = "23:59";
    }
    #endregion

    #region class clsConverter
    /// <summary>
    /// ����ת��(�������쳣)
    /// </summary>
    public class clsConverter
    {
        public static string ToString(object objValue)
        {
            try
            {
                if (objValue == null)
                    return "";
                else
                    return objValue.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static int ToInt(object objValue)
        {
            try
            {
                return Convert.ToInt32(objValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static long ToLong(object objValue)
        {
            try
            {
                return Convert.ToInt64(objValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal ToDecimal(object objValue)
        {
            try
            {
                return Convert.ToDecimal(objValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static DateTime ToDateTime(object objValue)
        {
            try
            {
                return Convert.ToDateTime(objValue);
            }
            catch (Exception)
            {
                return System.DateTime.MinValue;
            }
        }
    }
    #endregion
}
