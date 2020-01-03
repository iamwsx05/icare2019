using System;
using System.Drawing;
using System.Windows.Forms;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// ģ�����ݱ༭�ؼ�
	/// </summary>
	public class ctlTemplateEditer : RichTextBox
	{
		/// <summary>
		/// ѡ����Ŀ��
		/// </summary>
		private class clsSelectInfoGroup
		{
			/// <summary>
			/// ѡ����Ŀ
			/// </summary>
			private string[] m_strSelectInfoArr;
			/// <summary>
			/// �Ѿ�ѡ�����Ŀ
			/// </summary>
            private int m_intSelected;

			/// <summary>
			/// û��ѡ�����Ŀ
			/// </summary>
			private const int c_intNotSelected = -1;

			/// <summary>
			/// û����Ŀ��ѡ��
			/// </summary>
			private const int c_intNoSelectedItems = -2;

			/// <summary>
			/// ���캯��
			/// </summary>
			public clsSelectInfoGroup()
			{
				m_intSelected = c_intNotSelected;
				m_strSelectInfoArr = null;//�����ȸ�ֵ������ʹ�ú�����ֵ				
			}

			/// <summary>
			/// ����ѡ����Ŀ��ʽ���ݣ����ؽ�����ʾ���ݡ�
			/// </summary>
			/// <param name="p_strSelectInfoFormat">ѡ����Ŀ��ʽ���ݡ�������{{}}����</param>
			/// <returns>������ʾ���ݡ�</returns>
			public string m_strSetSelectInfoFormat(string p_strSelectInfoFormat)
			{
				string [] strContentArr = p_strSelectInfoFormat.Split(' ');
				System.Collections.ArrayList arlTemp = new System.Collections.ArrayList(strContentArr.Length);
				System.Text.StringBuilder sbdViewText = new System.Text.StringBuilder(p_strSelectInfoFormat.Length);;
				for(int i=0;i<strContentArr.Length;i++)
				{
					if(strContentArr[i].Trim() != "")
					{
						arlTemp.Add(strContentArr[i]);
						sbdViewText.Append(strContentArr[i]);
						sbdViewText.Append(' ');
					}
				}				

				m_strSelectInfoArr = (string[])arlTemp.ToArray(typeof(string));

				if(sbdViewText.Length == 0)
					return "";
				else
					return sbdViewText.ToString(0,sbdViewText.Length-1);
			}

			/// <summary>
			/// �Ƿ��Ѿ�ѡ��������ѡ���������true
			/// </summary>
			public bool m_blnIsSelected
			{
				get
				{
					return m_intSelected != c_intNotSelected;
				}
			}

			/// <summary>
			/// ѡ�������
			/// </summary>
			public string m_strSelectedInfo
			{
				get
				{
					if(m_intSelected < 0 || m_strSelectInfoArr == null || m_intSelected >= m_strSelectInfoArr.Length)
					{
						return "";
					}
					else
					{
						return m_strSelectInfoArr[m_intSelected];
					}
				}
			}

			/// <summary>
			/// ������Ŀ��ѡ����������ݱ�ѡ�񣬷���true�����򷵻�false��
			/// </summary>
			/// <param name="p_intSelectIndex">ѡ����±�</param>
			/// <param name="p_intStartIndex">��ѡ����Ŀ�Ŀ�ʼ�±�</param>
			/// <param name="p_intTextLength">��ѡ����Ŀ�����ݳ���</param>
			/// <param name="p_intUnslectedIndex">����ѡ����±�</param>
			/// <param name="p_intUnselectedTextLength">����ѡ����Ŀ�����ݳ���</param>
			/// <returns></returns>
			public bool m_blnHandleSelected(int p_intSelectIndex,out int p_intStartIndex,out int p_intTextLength,out int p_intUnslectedIndex,out int p_intUnselectedTextLength)
			{
				if(p_intSelectIndex < 0)
				{
					p_intStartIndex = -1;
					p_intTextLength = 0;
					p_intUnslectedIndex = -1;
					p_intUnselectedTextLength = 0;
					return false;
				}

				int intStartIndex = 0;
				int intEndIndex = m_strSelectInfoArr[0].Length-1;

				if(p_intSelectIndex >= intStartIndex && p_intSelectIndex <= intEndIndex)
				{
					m_mthHandleInfoSelect(0,intStartIndex,out p_intStartIndex,out p_intTextLength,out p_intUnslectedIndex,out p_intUnselectedTextLength);
					return true;
				}
				else
				{
					for(int i=1;i<m_strSelectInfoArr.Length;i++)
					{
						intStartIndex += m_strSelectInfoArr[i-1].Length+1;//��һ���ո�
						intEndIndex = intStartIndex+m_strSelectInfoArr[i].Length-1;
						if(p_intSelectIndex >= intStartIndex && p_intSelectIndex <= intEndIndex)
						{
							m_mthHandleInfoSelect(i,intStartIndex,out p_intStartIndex,out p_intTextLength,out p_intUnslectedIndex,out p_intUnselectedTextLength);
							return true;
						}
					}
				}

				p_intStartIndex = -1;
				p_intTextLength = 0;
				p_intUnslectedIndex = -1;
				p_intUnselectedTextLength = 0;
				return false;
			}		
			/// <summary>
			/// ����ѡ�ѡ��
			/// </summary>
			/// <param name="p_intInfoIndex">ѡ����ѡ��������±�</param>
			/// <param name="p_intStartIndex">ѡ�����ݵĿ�ʼ�±�</param>
			/// <param name="p_intUnslectedIndex">����ѡ����±�</param>
			/// <param name="p_intUnselectedTextLength">����ѡ����Ŀ�����ݳ���</param>
			private void m_mthHandleInfoSelect(int p_intInfoIndex,int p_intStartIndex,out int p_intStartIndex_Out,out int p_intTextLength,out int p_intUnslectedIndex,out int p_intUnselectedTextLength)
			{
				p_intStartIndex_Out = p_intStartIndex;
				if(m_intSelected == c_intNotSelected)
				{
					//ԭ��û��ѡ���
					p_intTextLength = m_strSelectInfoArr[p_intInfoIndex].Length;
					m_intSelected = p_intInfoIndex;

					p_intUnslectedIndex = 0;
					p_intUnselectedTextLength = 0;
					for(int i=0;i<m_strSelectInfoArr.Length;i++)
					{
						p_intUnselectedTextLength += m_strSelectInfoArr[i].Length+1;//��һ���ո�
					}
					p_intUnselectedTextLength -= 1;
				}
				else
				{
					//��ѡ��֮ǰ������
					if(m_intSelected != c_intNoSelectedItems)
					{
						p_intUnslectedIndex = 0;
						for(int i=0;i<m_intSelected;i++)
						{
							p_intUnslectedIndex += m_strSelectInfoArr[i].Length+1;//��һ���ո�
						}
						p_intUnselectedTextLength = m_strSelectInfoArr[m_intSelected].Length;
					}
					else
					{
						p_intUnslectedIndex = 0;
						p_intUnselectedTextLength = 0;
					}

					//����ѡ�������
					if(m_intSelected != p_intInfoIndex)
					{
						p_intTextLength = m_strSelectInfoArr[p_intInfoIndex].Length;
						m_intSelected = p_intInfoIndex;
					}
					else
					{
						p_intTextLength = 0;
						m_intSelected = c_intNoSelectedItems;
					}
				}
			}

			/// <summary>
			/// �ж��Ƿ�ͬһ��ѡ������
			/// </summary>
			/// <param name="p_objOther"></param>
			/// <returns></returns>
			public bool m_blnIsSameInfo(clsSelectInfoGroup p_objOther)
			{
				if(this.m_strSelectInfoArr.Length == p_objOther.m_strSelectInfoArr.Length)
				{
					for(int i=0;i<this.m_strSelectInfoArr.Length;i++)
					{
						if(this.m_strSelectInfoArr[i] != p_objOther.m_strSelectInfoArr[i])
							return false;
					}
					//�����ͬ������ͬ��ѡ����Ŀ
					this.m_intSelected = p_objOther.m_intSelected;
					return true;;
				}

				return false;
			}
		}		


		/// <summary>
		/// ѡ����Ŀ����Ϣ
		/// </summary>
		private class clsGroupInfo
		{
			public clsSelectInfoGroup m_objGroup;

			/// <summary>
			/// ��Ŀ�����ݿ�ʼ�±�
			/// </summary>
			public int m_intStartIndex;

			/// <summary>
			/// ��Ŀ�����ݽ����±�
			/// </summary>
			public int m_intEndIndex;
		}

		/// <summary>
		/// ȫ����ѡ����Ŀ����Ϣ
		/// </summary>
		private clsGroupInfo[] m_objGroupInfosArr;

		public ctlTemplateEditer()
		{
			InitializeComponent();

			this.Cursor = Cursors.Arrow;
			this.ReadOnly = true;		
			this.SelectionBullet = false;
		}

		/// <summary>
		/// ����ģ������
		/// </summary>
		/// <param name="p_strTemplateText">ģ������</param>
		public void m_mthSetTemplateText(string p_strTemplateText)
		{
			p_strTemplateText=p_strTemplateText.Replace("\r\n","\n");
			if(p_strTemplateText.Length == 0)
			{
				this.Text = "";
				return;
			}

			int intIndex = 0;

			int intPreIndex = intIndex;			

			bool blnIsStart = true;

			System.Collections.ArrayList arlTemp = new System.Collections.ArrayList();
			System.Text.StringBuilder sbdTempText = new System.Text.StringBuilder(p_strTemplateText.Length);

			while(intIndex >= 0)
			{				
				string strCurrentSymbol = blnIsStart?"{{":"}}";
				
				intIndex = p_strTemplateText.IndexOf(strCurrentSymbol,intIndex);

				if(intIndex >= 0)
				{
					if(!blnIsStart)
					{
						//�ҵ�ƥ��Ľ�����
						clsGroupInfo objGroup = new clsGroupInfo();
						objGroup.m_objGroup = new clsSelectInfoGroup();
						objGroup.m_intStartIndex = sbdTempText.Length;
						sbdTempText.Append(objGroup.m_objGroup.m_strSetSelectInfoFormat(p_strTemplateText.Substring(intPreIndex,intIndex-intPreIndex)));
						objGroup.m_intEndIndex = sbdTempText.Length-1;
						
						arlTemp.Add(objGroup);

						intPreIndex = intIndex+2;
						blnIsStart = true;
					}
					else
					{
						//�ҵ���ʼ��
						//��ӿ�ʼ����ǰһ��Index������
						sbdTempText.Append(p_strTemplateText.Substring(intPreIndex,intIndex-intPreIndex));

						intPreIndex = intIndex+2;
						blnIsStart = false;
					}
				}				
			}	
		
			if(intPreIndex < p_strTemplateText.Length)
			{
				sbdTempText.Append(p_strTemplateText.Substring(intPreIndex));
			}

			m_objGroupInfosArr = (clsGroupInfo[])arlTemp.ToArray(typeof(clsGroupInfo));
			this.Text = "";
			this.SelectionColor = m_clrNormalText;
			this.Text = sbdTempText.ToString();

			m_mthSetNeedSelectedColor();
		}

		private void InitializeComponent()
		{
			this.m_ctmCopy = new System.Windows.Forms.ContextMenu();
			this.m_ctmCopy.MenuItems.Add("����",new EventHandler(Copy_Click));
			this.ContextMenu = this.m_ctmCopy;
		}

		/// <summary>
		/// ��Ҫѡ����ı���ɫ
		/// </summary>
		private Color m_clrNeedSelected = Color.Red;
		/// <summary>
		/// ��Ҫѡ����ı���ɫ
		/// </summary>
		public Color m_ClrNeedSelected
		{
			get
			{
				return m_clrNeedSelected;
			}
			set
			{
				m_clrNeedSelected = value;
			}
		}
		/// <summary>
		/// ѡ����ı���ɫ
		/// </summary>
		private Color m_clrSelected = Color.Blue;
		/// <summary>
		/// ѡ����ı���ɫ
		/// </summary>
		public Color m_ClrSelected
		{
			get
			{
				return m_clrSelected;
			}
			set
			{
				m_clrSelected = value;
			}
		}
		/// <summary>
		/// ��ѡ����ı���ɫ
		/// </summary>
		private Color m_clrUnselected = Color.Gray;
		private System.Windows.Forms.ContextMenu m_ctmCopy;
		/// <summary>
		/// ��ѡ����ı���ɫ
		/// </summary>
		public Color m_ClrUnselected
		{
			get
			{
				return m_clrUnselected;
			}
			set
			{
				m_clrUnselected = value;
			}
		}
		/// <summary>
		/// ��ͨ���ı���ɫ
		/// </summary>
		private Color m_clrNormalText = Color.Black;
		/// <summary>
		/// ��ͨ���ı���ɫ
		/// </summary>
		public Color m_ClrNormalText
		{
			get
			{
				return m_clrNormalText;
			}
			set
			{
				m_clrNormalText = value;
			}
		}
		/// <summary>
		/// ������Ҫѡ�����ɫ
		/// </summary>
		private void m_mthSetNeedSelectedColor()
		{
			for(int i=0;i<m_objGroupInfosArr.Length;i++)
			{
				this.SelectionStart = m_objGroupInfosArr[i].m_intStartIndex;
				this.SelectionLength = m_objGroupInfosArr[i].m_intEndIndex - m_objGroupInfosArr[i].m_intStartIndex+1;
				this.SelectionColor = m_clrNeedSelected;
			}
		}

		/// <summary>
		/// �Ƿ��Ѿ�ѡ���
		/// </summary>
		public bool m_BlnIsSelected
		{
			get
			{
				if(m_objGroupInfosArr == null)
					return false;
				else
				{
					for(int i=0;i<m_objGroupInfosArr.Length;i++)
					{
						if(!m_objGroupInfosArr[i].m_objGroup.m_blnIsSelected)
							return false;
					}

					return true;
				}				
			}
		}

		/// <summary>
		/// �༭����
		/// </summary>
		public string m_StrEditedText
		{
			get
			{
				System.Text.StringBuilder sbdEditedText = new System.Text.StringBuilder(this.Text.Length);

				int intNormalStart = 0;

				for(int i=0;i<m_objGroupInfosArr.Length;i++)
				{
					if(intNormalStart < m_objGroupInfosArr[i].m_intStartIndex)
					{
						//ѡ����Ŀǰ����ͨģ������
						sbdEditedText.Append(this.Text.Substring(intNormalStart,m_objGroupInfosArr[i].m_intStartIndex-intNormalStart));
					}

					sbdEditedText.Append(m_objGroupInfosArr[i].m_objGroup.m_strSelectedInfo);

					intNormalStart = m_objGroupInfosArr[i].m_intEndIndex+1;
				}

				if(intNormalStart < this.Text.Length)
				{
					sbdEditedText.Append(this.Text.Substring(intNormalStart,this.Text.Length-intNormalStart));
				}

				sbdEditedText.Replace("\n","\r\n");
				return sbdEditedText.ToString();
			}
		}
		
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			//������
			if(m_objGroupInfosArr == null)
				return;

			int intOnIndex = this.GetCharIndexFromPosition(new Point(e.X,e.Y));

			int intStartIndex = 0;
			int intLength = 0;
			int intUnselectedStartIndex = 0;
			int intUnselectedLength = 0;
			clsSelectInfoGroup objSelectedGroup = null;

			for(int i=0;i<m_objGroupInfosArr.Length;i++)
			{
				if(intOnIndex >= m_objGroupInfosArr[i].m_intStartIndex && intOnIndex <= m_objGroupInfosArr[i].m_intEndIndex)
				{					
					if(m_objGroupInfosArr[i].m_objGroup.m_blnHandleSelected(intOnIndex-m_objGroupInfosArr[i].m_intStartIndex,out intStartIndex,out intLength,out intUnselectedStartIndex,out intUnselectedLength))
					{
						objSelectedGroup = m_objGroupInfosArr[i].m_objGroup;						
					}		
					break;
				}
			}	
	        
			if(objSelectedGroup != null)
			{
				//��������ͬ����Ŀ����ͬ��ѡ��
				for(int i=0;i<m_objGroupInfosArr.Length;i++)
				{
					if(m_objGroupInfosArr[i].m_objGroup.m_blnIsSameInfo(objSelectedGroup))
					{
						this.SelectionStart = m_objGroupInfosArr[i].m_intStartIndex+intUnselectedStartIndex;
						this.SelectionLength = intUnselectedLength;
						this.SelectionColor = m_clrUnselected;

						this.SelectionStart = m_objGroupInfosArr[i].m_intStartIndex+intStartIndex;
						this.SelectionLength = intLength;
						this.SelectionColor = m_clrSelected;

						this.SelectionLength = 0;

					}
				}

				this.SelectionStart = intOnIndex;
			}
		}

		private void Copy_Click(object sender,EventArgs e)
		{
			this.Copy();
		}

	}
}
