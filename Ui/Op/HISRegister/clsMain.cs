using System;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.Data;
using System.Drawing;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsMain�������� Create by Sam 2004-5-24
	/// </summary>
	public class clsMain
	{
		public clsMain()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public static void Tips(string Values)
		{
			MessageBox.Show(Values,"��ʾ");
		}
		#region ���þ��洰��
		public void m_mthShowWarning(exDataGridSour.exComboBox txtBox,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.Offset(-50,-(ShowWarning.Height-txtBox.Height/2));
			ShowWarning.Location=p;
			ShowWarning.Show();
		}
		public void m_mthShowWarning(TextBox txtBox,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
            ShowWarning.TopMost = false;
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.Offset(-50,-(ShowWarning.Height-txtBox.Height/2));
			ShowWarning.Location=p;
			//ShowWarning.Show();
            m_mthShowForm(ShowWarning);
		}
		public void m_mthShowWarning(com.digitalwave.iCare.gui.HIS.exComboBox txtBox,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.Offset(-50,-(ShowWarning.Height-txtBox.Height/2));
			ShowWarning.Location=p;
			ShowWarning.Show();
		}
		//public void m_mthShowWarning(System.Windows.Forms.TextBox txtBox ,string strWaring)
		//{
		//	frmShowWarning ShowWarning=new frmShowWarning();
		//	ShowWarning.m_GetWaring=strWaring;
		//	Point p= txtBox.Parent.PointToScreen(txtBox.Location);
		//	p.Offset(-50,-(ShowWarning.Height-txtBox.Height/2));
		//	ShowWarning.Location=p;
		//	ShowWarning.Show();
		//}
		public void m_mthShowWarning(com.digitalwave.controls.ctlTextBoxFind txtBox ,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.Offset(-50,-(ShowWarning.Height-txtBox.Height/2));
			ShowWarning.Location=p;
			ShowWarning.Show();
		}
		public void m_mthShowWarning(System.Windows.Forms.Panel txtBox ,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.Offset(-0,-(ShowWarning.Height-txtBox.Height/2));
			ShowWarning.Location=p;
			ShowWarning.Show();
		}
		public void m_mthShowWarning(System.Windows.Forms.GroupBox txtBox ,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.Offset(-0,-(ShowWarning.Height-txtBox.Height/2));
			ShowWarning.Location=p;
			ShowWarning.Show();
		}
		public void m_mthShowWarning(System.Windows.Forms.ComboBox txtBox ,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.Offset(-50,-(ShowWarning.Height-txtBox.Height/2));
			ShowWarning.Location=p;
			ShowWarning.Show();
		}
		public void m_mthShowWarning(System.Windows.Forms.ListView txtBox ,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.X+=(txtBox.Width/2);
			p.Y+=(txtBox.Height/2);
			ShowWarning.Location=p;
			ShowWarning.Show();
		}
		public void m_mthShowWarning(com.digitalwave.controls.datagrid.ctlDataGrid txtBox,string strWaring)
		{
			frmShowWarning ShowWarning=new frmShowWarning();
			ShowWarning.m_GetWaring=strWaring;
			Point p= txtBox.Parent.PointToScreen(txtBox.Location);
			p.X+=(txtBox.Width/2);
			p.Y+=(txtBox.Height/2);
			ShowWarning.Location=p;
			ShowWarning.Show();
		}

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        /// <summary>
        /// ������ǰ�˴�,��������ý���
        /// ע:�����TopMost���Ա���Ϊfalse
        /// kenny add in 2008.08.13
        /// </summary>
        /// <param name="form"></param>
        public static void m_mthShowForm(Form form)
        {
            SetWindowPos(form.Handle, new IntPtr(-1), 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0040 | 0x0010);
        }
		#endregion
		#region ��ȡ���µķ�Ʊ��
		/// <summary>
		/// ��ȡ���µķ�Ʊ��
		/// </summary>
		/// <param name="oldCheckNO">ǰһ�ŷ�Ʊ��</param>
		/// <returns></returns>
		public static string  m_mthGetNewCheckNO(string oldCheckNO)
		{
			string strNewDocument="";
			Encoding ascii=Encoding.ASCII;
			Byte[] byCoding= ascii.GetBytes(oldCheckNO);
			int intEnLengt=-1;
			for(int i1=0;i1<byCoding.Length;i1++)
			{
				if((int)byCoding[i1]<=57)
				{
					intEnLengt=i1-1;
					break;
				}
			}
			if(intEnLengt>-1)
			{
				string strEN=oldCheckNO.Substring(0,intEnLengt+1);
				string strNumber=oldCheckNO.Substring(intEnLengt+1);
				long  intNumber=0;
				try
				{
					strNumber="1"+strNumber;
					intNumber=Convert.ToInt64(strNumber)+1;
					strNumber=intNumber.ToString().Substring(1,strNumber.Length-1);
					strNewDocument=strEN+strNumber;
				}
				catch
				{
					strNewDocument=strEN;
				}
			}
			else
			{
				
				try
				{
					string strOldCheckNo="1"+oldCheckNO;
					long n=Convert.ToInt64(strOldCheckNo)+1;
					string strNewCheckNo=n.ToString().Substring(1,strOldCheckNo.Length-1);
					strNewDocument=strNewCheckNo;
				}
				catch
				{
				}
			}
			return strNewDocument;
		}

		#endregion
		#region ������ת�������Ĵ�д
		private static string[] cstr={"��","Ҽ","��","��","��", "��", "½","��","��","��"};
		private  static string[] wstr={"��","��","Բ","ʰ","��","Ǫ","��","ʰ","��","Ǫ","��","ʰ","��","Ǫ"};
		public static string CurrencyToString(float fltCurrency)
		{
			string str=fltCurrency.ToString("0.00");
			str=str.Replace(".","");
			int len=str.Length;
			int i;
			string tmpstr,rstr;
			rstr="";
			for(i=1;i<=len;i++)
			{
				tmpstr=str.Substring(len-i,1);
				rstr=string.Concat(cstr[Int32.Parse(tmpstr)]+wstr[i-1],rstr);
			}
			rstr=rstr.Replace("ʰ��","ʰ");
			rstr=rstr.Replace("��ʰ","��");
			rstr=rstr.Replace("���","��");
			rstr=rstr.Replace("��Ǫ","��");
			rstr=rstr.Replace("����","��");
			for(i=1;i<=6;i++)
				rstr=rstr.Replace("����","��");
			rstr=rstr.Replace("����","��");
			rstr=rstr.Replace("����","�|");
			rstr=rstr.Replace("����","��");
			rstr=rstr.Replace("������","");
			rstr=rstr.Replace("���","");
			rstr+="��"; 
			rstr=rstr.Replace("����","��");
			return rstr;
		}

        public static string CurrencyToString2(double fltCurrency)
        {
            int num2;
            string str = fltCurrency.ToString("0.00").Replace(".", "");
            int length = str.Length;
            string str3 = "";
            for (num2 = 1; num2 <= length; num2++)
            {
                string s = str.Substring(length - num2, 1);
                str3 = cstr[int.Parse(s)] + wstr[num2 - 1] + str3;
            }
            str3 = str3.Replace("ʰ��", "ʰ").Replace("��ʰ", "��").Replace("���", "��").Replace("��Ǫ", "��").Replace("����", "��");
            for (num2 = 1; num2 <= 6; num2++)
            {
                str3 = str3.Replace("����", "��");
            }
            return (str3.Replace("����", "��").Replace("����", "�|").Replace("����", "��").Replace("������", "").Replace("���", "") + "��").Replace("����", "��");
        } 

		#endregion

		#region �����ݰ�ĳһ���ֶ�������

		public static void m_Detach(DataTable dt ,string fieldName,out ArrayList arrList)
		{
			arrList=new ArrayList();
			DataView myDataView = dt.DefaultView;
			myDataView.Sort = fieldName+" ASC";
			if(dt.Rows.Count==0)
				return;
			Encoding ascii=Encoding.ASCII;
			Byte[] byCoding= ascii.GetBytes(myDataView[0][fieldName].ToString());
			int intEnLengt=-1;
			for(int i1=0;i1<byCoding.Length;i1++)
			{
				if((int)byCoding[i1]<=57)
				{
					intEnLengt=i1-1;
					break;
				}
			}
			string strEng="";
			string strCount="";
			string strEng1="";
			string strCount1="";
			if(intEnLengt==-1)
			{
				arrList.Add(myDataView[0][fieldName].ToString());
				for(int i1=1;i1<myDataView.Count;i1++)
				{
					try
					{
						if(Convert.ToInt32(myDataView[i1-1][fieldName].ToString())==Convert.ToInt32(myDataView[i1][fieldName].ToString()))
						{
							continue;
						}
						if(Convert.ToInt32(myDataView[i1-1][fieldName].ToString())+1==Convert.ToInt32(myDataView[i1][fieldName].ToString()))
						{
							arrList.Add(myDataView[i1][fieldName].ToString());
						}
						else
						{
							arrList.Add(",");
							arrList.Add(myDataView[i1][fieldName].ToString());
						}
					}
					catch
					{
					}
				}
			}
			else
			{
				arrList.Add(myDataView[0][fieldName].ToString());
				for(int i1=1;i1<myDataView.Count;i1++)
				{
					try
					{
						strEng=myDataView[i1-1][fieldName].ToString().Substring(0,intEnLengt+1);
						strCount=myDataView[i1-1][fieldName].ToString().Substring(intEnLengt+1);
						strEng1=myDataView[i1][fieldName].ToString().Substring(0,intEnLengt+1);
						strCount1=myDataView[i1][fieldName].ToString().Substring(intEnLengt+1);
						if(strEng==strEng1&&Convert.ToInt32(strCount)==Convert.ToInt32(strCount1))
						{
							continue;
						}
						if(strEng==strEng1&&Convert.ToInt32(strCount)+1==Convert.ToInt32(strCount1))
						{
							arrList.Add(myDataView[i1][fieldName].ToString());
						}
						else
						{
							arrList.Add(",");
							arrList.Add(myDataView[i1][fieldName].ToString());
						}
					}
					catch
					{
					}
				}

			}
		}
		#endregion

		#region ����û��������ݵ�����
		/// <summary>
		/// ����û��������ݵ�����
		/// </summary>
		/// <param name="strVal">1-���֣�2-���ģ�3-Ӣ�ģ�4-Ӣ�ļ������Ļ����ļ���Ӣ���ַ�</param>
		/// <returns></returns>
		public static int  IsEngOrNumOrChina(string strVal)
		{
			//ת���������ĵ��ַ���
			if(IsNumber(strVal))
				return 1;//����
			Byte[] byteArr = null;
			try
			{
				byteArr = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strVal);
			}
			catch{}
			if(byteArr.Length%2==0&&strVal.Length==byteArr.Length)
			{
				return 3;//Ӣ�Ļ�Ӣ�ļ�������
			}
			if(byteArr.Length%2==0)
			{
				return 2;//����
			}
			else if(strVal.Length==byteArr.Length)
			{
				return 3;//Ӣ�Ļ�Ӣ�ļ�������
			}
			else
			{
				return 4;//Ӣ�ļ������Ļ����ļ���Ӣ���ַ�
			}
		}
		#endregion

		#region ����listView�е���Ŀ
		/// <summary>
		/// ����listView�е���Ŀ
		/// </summary>
		/// <param name="lvwItem"></param>
		/// <param name="findType">1-���������в��ң��κ����͵��ַ�����2-�Ӵ����в��ң����֣���3-�������Ʋ��ң����ģ���4-ƴ������������ң�Ӣ�ģ�</param>
		/// <param name="MatchCol">��listView�еĵڼ��п�ʼ����</param>
		/// <param name="strValues"></param>
		/// <param name="isCode">��ʶ��������listView���Ƿ���ڲ���ʶ���ڵ��к�,-1-������</param>
		/// <returns></returns>
		public static int FindItemByValues(ListView lvwItem,int findType,int MatchCol,int isCode,string strValues)
		{
			if(isCode!=-1)
			{
				for(int i1=0;i1<lvwItem.Items.Count;i1++)
				{
					if(lvwItem.Items[i1].SubItems[isCode].Text.IndexOf(strValues)==0)
						return i1;
				}
			}
			if(findType == 2)//�Ӵ����в���
			{
				for(int i1=0;i1<lvwItem.Items.Count;i1++)
				{
					if(lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues)==0)
						return i1;
				}
			}
			if(findType == 3)//�������Ʋ���
			{
				for(int i1=0;i1<lvwItem.Items.Count;i1++)
				{
					if(lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues)==0)
						return i1;
				}
			}
			if(findType == 4)//ƴ�������������
			{
				for(int i1=0;i1<lvwItem.Items.Count;i1++)
				{
					if(lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues.ToUpper())==0||lvwItem.Items[i1].SubItems[MatchCol+1].Text.IndexOf(strValues.ToUpper())==0)
						return i1;
				}
			}
			return -1;
		}
		#endregion

		#region ��ȡ������Ϣ
		/// <summary>
		/// ��ȡ������Ϣ
		/// </summary>
		/// <param name="strsetid">����ID��</param>
		/// <returns>������ 0--�� 1--��ʱ��false ��true �ǣ������� 1--�� 0--��ʱ��false �ǣ�true ��</returns>
		public static bool m_blGetCollocate(string strsetid)
		{
			bool isTrue;
			clsDomainControl_Register domain=new clsDomainControl_Register();
			domain.m_lngGetCollocate(out isTrue,strsetid);
			return isTrue;
		}

        /// <summary>
        /// ��ȡ������Ϣ
        /// by huafeng.xiao
        /// 2009��9��15��9:31:13
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStatus">����״̬���ʺ϶�״̬����ʹ��</param>
        /// <param name="strsetid">����ID��</param>
        /// <returns></returns>
        public static string m_strGetCollocate(string strsetid)
        {
            string m_strStatus = string.Empty;
            clsDomainControl_Register domain = new clsDomainControl_Register();
            domain.m_lngGetCollocateStatus(out m_strStatus, strsetid);
            return m_strStatus;
        }

		#endregion

		/// <summary>
		/// �ض�������
		/// </summary>
		/// <param name="newELementCount"></param>
		/// <param name="originalArray"></param>
		/// <returns></returns>
		public static object[] redefineArray(int newELementCount, object[] originalArray)
		{
			object[] temp = new object[newELementCount];
			if (newELementCount >= originalArray.Length)
			{
				for(int i = 0; i < originalArray.Length; i ++)
				{
					temp[i] = originalArray[i];
				}
			}
			else
			{
				for(int i = 0; i < temp.Length; i ++)
				{
					temp[i] = originalArray[i];
				}
			}
			return temp;
		}
		/// <summary>
		/// ����listView�е���Ŀ
		/// </summary>
		/// <param name="lvwItem"></param>
		/// <param name="MatchCol"></param>
		/// <param name="strValues"></param>
		/// <returns></returns>
		public static int FindItemByValues(ListView lvwItem,int MatchCol,string strValues)
		{
			if(MatchCol == 2)
			{
				for(int i1=0;i1<lvwItem.Items.Count;i1++)
				{
					if(lvwItem.Items[i1].SubItems[MatchCol-1].Text.IndexOf(strValues)==0
						|| lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues.ToUpper())==0)
						return i1;
				}
			}
			else
			{
				for(int i1=0;i1<lvwItem.Items.Count;i1++)
				{
					if(lvwItem.Items[i1].SubItems[MatchCol].Text.IndexOf(strValues)==0)
						return i1;
				}
			}
			return -1;
		}
		public static int FindItemByValues(DataGrid dg,int MatchCol,string strValues)
		{
			
			
			for(int i1=0;i1<dg.VisibleRowCount;i1++)
			{
				if(dg[i1,MatchCol].ToString().Trim()==strValues)
					return i1;
			}    
			return -1;
		}
		/// <summary>
		/// ��nullת����""
		/// </summary>
		/// <param name="strValues"></param>
		/// <param name="IsZero"></param>
		/// <returns></returns>
		public static string IsNullToString(string strValues,string IsZero)
		{
			if(strValues==null)
			{
				if(IsZero!=null)
					return IsZero;
				else
					return "";
			}
			else
			{
				strValues=ReplaceTo(strValues);
				if(strValues!="")
					return strValues;
				else
				{
					if(IsZero!=null)
						return IsZero;
					else
						return strValues;
				}
			}
		}
		/// <summary>
		/// ȥ���ո�͵�����
		/// </summary>
		/// <param name="strValues"></param>
		/// <returns></returns>
		public static string ReplaceTo(string strValues)
		{
			strValues=strValues.Trim();
			strValues=strValues.Replace(" ","");
			strValues=strValues.Replace("'","");
			return strValues;
		}
		/// <summary>
		/// ֻ����������strTxt��Ϊ��ʱ������С����
		/// </summary>
		/// <param name="KeyChar"></param>
		/// <param name="strTxt"></param>
		/// <returns></returns>
		public static bool ValNumer(char KeyChar,string strTxt)
		{
			if(KeyChar==(char)8) //ɾ����
				return true;
			if(strTxt==null)//ֻ����������
			{
				return char.IsDigit(KeyChar);
			}
			else
			{
				if(char.IsDigit(KeyChar))
					return true;
				else
				{
					if(KeyChar=='.') //�����С����
					{
						if(strTxt.IndexOf(".")>=0) //����Ѿ���С����
							return false;
						else
							return true;
					}
					else //��������Ҳ����С����
						return false;
				}
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		/// <param name="strVal"></param>
		/// <returns></returns>
		public static bool IsNumber(string strVal)
		{
			if(strVal==null)
				return false;
			strVal=ReplaceTo(strVal);
			if(strVal=="")
				return false;
			for(int i=0;i<strVal.Length;i++)
			{
				if(!char.IsNumber(strVal.ToCharArray()[i]))
					return false;
			}
			return true;
		}
		/// <summary>
		/// �Ǵ���һ��С���������
		/// </summary>
		/// <param name="strVal"></param>
		/// <returns></returns>
		public static bool IsNumberWithPoint(string strVal)
		{
			if(strVal==null)
				return false;
			strVal=ReplaceTo(strVal);
			if(strVal=="")
				return false;
			bool WithPoint=false;
			for(int i=0;i<strVal.Length;i++)
			{
				if(strVal.ToCharArray()[i]=='.')
				{
					if(!WithPoint) //û�й�С����
						WithPoint=true;
					else
						return false;
				}
				else
				{
					if(!char.IsNumber(strVal.ToCharArray()[i]))
						return false;
				}
			}
			return true;
		}
		/// <summary>
		/// ����Ƿ�Ϊ����
		/// </summary>
		/// <param name="strVal"></param>
		/// <returns></returns>
		public static bool IsEngOrNum(string strVal)
		{
			if(IsNumber(strVal))
				return true;
			ASCIIEncoding AE = new ASCIIEncoding();
			byte[] ByteArray = AE.GetBytes(strVal);

			for(int x = 0;x <= ByteArray.Length - 1; x++)
			{
				if(ByteArray[x]<0) //�������ַ�
					return false;
			}
			return true;
		}
		/// <summary>
		/// ��ʽ��Grid ����strColNameΪ��ʾ��������strFieldNameΪ�󶨵��ֶΡ�
		/// </summary>
		/// <param name="dtsConstruct"></param>
		/// <param name="strColName"></param>
		/// <param name="strFieldName"></param>
		/// <param name="intWidth"></param>
		public static void m_SetTableStyle(DataGridTableStyle dtsConstruct, string strColName, string strFieldName, int intWidth)
		{
			DataGridTextBoxColumn ColumnStyle = new DataGridTextBoxColumn();//��������ͷ
			ColumnStyle.HeaderText = strColName;
			ColumnStyle.MappingName = strFieldName;
			ColumnStyle.Width = intWidth;
			ColumnStyle.Alignment=HorizontalAlignment.Center;
			ColumnStyle.NullText="";
			dtsConstruct.GridColumnStyles.Add(ColumnStyle);
		}
	}

	//���¶���һListView�Ա�������
	/// <summary>
	///  ��ListView�������� Create By Sam 2004-5-25
	/// </summary>
	public class ListViewItemComparer : IComparer 
	{
		private int col;
		private bool IsAsc=false; //�Ƿ�Ϊ����
		public ListViewItemComparer() 
		{
			col=0;
		}
		public ListViewItemComparer(int column,bool IsAsc,ListView objListView) 
		{
			string strColTxt="";
			for(int i=0;i<objListView.Columns.Count;i++)
			{
				strColTxt=objListView.Columns[i].Text;
				//����
				strColTxt=strColTxt.Replace(" ��","");
				strColTxt=strColTxt.Replace(" ��","");
				objListView.Columns[i].Text=strColTxt;
			}
			
			col=column;
			this.IsAsc=IsAsc;
			strColTxt=objListView.Columns[col].Text;
			if(IsAsc==true)//���������
				objListView.Columns[col].Text=strColTxt+" ��";
			else
				objListView.Columns[col].Text=strColTxt+" ��";
		}
		//�����ּ�ͷ
		public ListViewItemComparer(int column,bool IsAsc) 
		{
			col=column;
			this.IsAsc=IsAsc;
		}

		// created by Sam
		// modify by Cameron Wong on Aug 13, 2004
		// there is a bug in the origional version!
		public int Compare(object x, object y) 
		{
			int i=0;
			i=String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);

			// the following line is added by Cameron Wong on Aug 13, 2004
			if (!IsAsc) i = -i;

/* commented by Cameron Wong on Aug 13, 2004
 
			if(i>0)//strA����strB
			{
				if(IsAsc==true)//���������
					i=1;
				else
					i=-1;
			}
			if(i<0)//strAС��strB
			{
				if(IsAsc==true)//���������
					i=-1;
				else
					i=1;
			}
			else //���
			{
				i=0;
			}
*/
			return i;
		}
	}



	public class ComboBoxItem 
	{
		internal ArrayList sText;
		internal ArrayList sValue;
		private exComboBox objCombo;
		public ComboBoxItem(exComboBox source)
		{
			this.objCombo=source;
			sText=new ArrayList();
			sValue=new ArrayList();
		}
		public void Add(string Text,string Value)
		{
			//this.sText=Text;
			this.objCombo.Items.Add(Text);
			//this.Value=Value;
			sText.Add(Text);
			sValue.Add(Value);
			//this.Add(Text);
		}
		public string this[int index]
		{
			get
			{
				string tmpText;
				if(index>-1 && sText.Count>0)
					tmpText=sText[index].ToString();
				else
					tmpText="";
				return tmpText;
			}
		}
	}
	
	public class exTreeNode:TreeNode
	{
		private string sKey;
		public string Key
		{
			get
			{
				return sKey;
			}
			set
			{
				sKey=value;
			}
		}
	}
	public class exComboBox:ComboBox
	{
		public ComboBoxItem Item;

		public exComboBox()
		{
			Item=new ComboBoxItem(this);
		}
		public void m_mthClear()
		{
			Item.sText.Clear();
			Item.sValue.Clear();
		}
		/// <summary>
		/// ��ǰѡ���ֵ
		/// </summary>
		public string SelectItemValue
		{
			get
			{
				string tmp="";
				if(Item.sValue.Count==0)
					return tmp;
				if(SelectedIndex>-1)
					tmp=Item.sValue[SelectedIndex].ToString();
				else
					tmp="";
				return tmp;
			}
		}
		/// <summary>
		/// ��ǰ��ʾ��ֵ
		/// </summary>
		public string SelectItemText
		{
			get
			{
				string tmp="";
				if(Item.sText.Count==0)
					return tmp;
				if(SelectedIndex>-1)
					tmp=Item.sText[SelectedIndex].ToString();
				else
					tmp="";
				return tmp;
			}
		}
		public void Clear()
		{
			this.SelectedIndex=-1;
			this.Text="";
		}
		/// <summary>
		/// ���ұ����ֵ
		/// </summary>
		/// <param name="Key"></param>
		public void FindKey(string Key)
		{
//			this.Clear();
			if(clsMain.IsNullToString(Key,null)=="")
			{
				this.SelectedIndex=-1;
				return;
			}
			if(Item.sValue.Count==0)
				return;
			bool b_temp =false;
			for(int i1=0;i1<this.Items.Count;i1++)
			{
				if(Item.sValue[i1].ToString()==Key)
				{
					b_temp =true;
					if(this.SelectedIndex!=i1)
					{
						this.SelectedIndex=i1;
						
					}
					return;
				}
			}
			if(!b_temp)
			{
			this.SelectedIndex=-1;
			}
		}	
	}
   

}
