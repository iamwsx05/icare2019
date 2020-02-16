using System;
using System.Windows.Forms;
using System.Collections;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// ���ݿؼ��ı�����ģ����ѯ����
	/// </summary>
	public abstract class clsTextLikeTool
	{
		/// <summary>
		/// ��ѯ����б�
		/// </summary>
		private ListView m_lsvLike;

		/// <summary>
		/// ��ǰ���в�ѯ�Ŀؼ�
		/// </summary>
		private Control m_ctlCurrent;

		/// <summary>
		/// ǰһ�ν��в�ѯ�Ŀؼ��ı�����
		/// </summary>
		private string m_strPreText;

		/// <summary>
		/// ������еĿؼ�
		/// </summary>
		protected ArrayList m_arlControls;

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="p_lsvLike">��ѯ����б�</param>
		public clsTextLikeTool(ListView p_lsvLike)
		{
			m_mthInitComponent(p_lsvLike);
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		public clsTextLikeTool()
		{
			m_mthInitComponent(null);
		}

		private bool blnIsAdWidth = true;

		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <param name="p_lsvLike">��ѯ����б�</param>
		private void m_mthInitComponent(ListView p_lsvLike)
		{
			m_lsvLike = p_lsvLike;

			if(m_lsvLike == null)
				m_lsvLike = new ListView();

			m_lsvLike.DoubleClick += new EventHandler(m_mthListViewDoubleClick);
			m_lsvLike.KeyDown += new KeyEventHandler(m_mthListViewKeyDown);
			m_lsvLike.LostFocus += new EventHandler(m_mthListViewLostFocus);

            m_lsvLike.Visible = false;		
	
			m_arlControls = new ArrayList();
		}

		/// <summary>
		/// �����Ҫ����ģ����ѯ�Ŀؼ�
		/// </summary>
		/// <param name="p_ctlControl">�ؼ�</param>
		public void m_mthAddControl(Control p_ctlControl)
		{
			if(m_lsvLike.Parent != null && !p_ctlControl.Parent.Equals(m_lsvLike.Parent))
			{
				throw new Exception("Input Control's parent is not Equals the previous control'parent.");
			}

			if(m_lsvLike.Parent == null)
			{
				p_ctlControl.Parent.Controls.Add(m_lsvLike);
			}
			blnIsAdWidth = true;
			p_ctlControl.KeyDown += new KeyEventHandler(m_mthControlKeyDown);
			p_ctlControl.LostFocus += new EventHandler(m_mthControlLostFocus);

			m_arlControls.Add(p_ctlControl);
		}

		/// <summary>
		/// ��Ӷ����Ҫ����ģ����ѯ�Ŀؼ�
		/// </summary>
		/// <param name="p_ctlArr">�ؼ�</param>
		/// <param name="blnIsAd">�Ƿ����textBox�Զ�������С</param>
		public void m_mthAddControl(Control[] p_ctlArr,bool blnIsAd)
		{
			blnIsAdWidth = blnIsAd;
			if(p_ctlArr != null && p_ctlArr.Length != 0)
			{
				for(int i=0; i<p_ctlArr.Length; i++)
				{
					if(m_lsvLike.Parent != null && !p_ctlArr[i].Parent.Equals(m_lsvLike.Parent))
					{
						throw new Exception("Input Control's parent is not Equals the previous control'parent.");
					}

					if(m_lsvLike.Parent == null)
					{
						p_ctlArr[i].Parent.Controls.Add(m_lsvLike);
					}

					p_ctlArr[i].KeyDown += new KeyEventHandler(m_mthControlKeyDown);
					p_ctlArr[i].LostFocus += new EventHandler(m_mthControlLostFocus);

					m_arlControls.Add(p_ctlArr[i]);
				}
			}
		}

		/// <summary>
		/// �ؼ�KeyDown�¼��Ĵ�����
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objKey"></param>
		private void m_mthControlKeyDown(object p_objSender,KeyEventArgs p_objKey)
		{
			/*
			 * Enter�����ݿؼ��ı����ݽ��в�ѯ��
			 *            ��ѯ�����0����껹�ڿؼ��ϣ�1���Զ����ò�ѯ������ؼ���>1����ʾ�б��Զ���λ����һ�������
			 * Down�� �������б�û����ʾ���߿ؼ��ı����ݸı䣬���ݿؼ��ı����ݽ��в�ѯ��
			 *            ��ѯ�����0����껹�ڿؼ��ϣ�>=1����ʾ�б��Զ���λ����һ�������
			 *        �������,�Զ���λ����һ�������
			 * ESC��  ���ؽ���б�
			 */
			m_lsvLike.TabIndex = ((Control)p_objSender).TabIndex;
			switch(p_objKey.KeyCode)
			{					
				case Keys.Enter:
					m_lsvLike.Items.Clear();
					m_strPreText = ((Control)p_objSender).Text;
					long lngEnterRes = m_lngInitListItem(m_strPreText);

					if(lngEnterRes > 0)
					{
						m_ctlCurrent = (Control)p_objSender;
							
						if(m_lsvLike.Items.Count == 1)
						{
							m_mthSetSelectedValue();
						}
						else
						{
							m_mthAdjustListView();
							
							if(m_lsvLike.Items.Count > 0)
							{
								m_lsvLike.Items[0].Selected = true;
								m_lsvLike.Items[0].Focused = true;
								m_lsvLike.BringToFront();
								m_lsvLike.Visible = true;
								m_lsvLike.Focus();
							}							
						}
					}
					break;
				case Keys.Down:
					if(m_lsvLike.Visible == false || m_strPreText != ((Control)p_objSender).Text)
					{
						m_lsvLike.Items.Clear();
						m_strPreText = ((Control)p_objSender).Text;
						long lngDownRes = m_lngInitListItem(((Control)p_objSender).Text);

						if(lngDownRes > 0)
						{
							m_ctlCurrent = (Control)p_objSender;
							m_mthAdjustListView();

							m_lsvLike.BringToFront();
							m_lsvLike.Visible = true;
						}
					}

					if(m_lsvLike.Items.Count > 0)
					{
						m_lsvLike.Items[0].Selected = true;
						m_lsvLike.Items[0].Focused = true;
						m_lsvLike.Focus();
					}
					break;
				case Keys.Escape:
					m_lsvLike.Visible = false;
					break;
			}
		}

		/// <summary>
		/// ����б�KeyDown�¼��Ĵ�����
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objKey"></param>
		private void m_mthListViewKeyDown(object p_objSender,KeyEventArgs p_objKey)
		{
			/*
			 * Space,
			 * Enter�����ò�ѯ������ؼ���
			 * Up��   ��ǰ����б��ѡ����Ŀ�ǵ�һ�����ѹ�����õ��ؼ��С�
			 * ESC��  ���ؽ���б�
			 */
			switch(p_objKey.KeyCode)
			{
				case Keys.Space:
				case Keys.Enter:
					m_mthSetSelectedValue();
					break;
				case Keys.Up:
					if(m_lsvLike.Items.Count > 0
						&& m_lsvLike.Items[0].Selected)
					{
						m_lsvLike.Items[0].Selected = false;
						m_ctlCurrent.Focus();
					}
					break;
				case Keys.Escape:
					m_lsvLike.Visible = false;
					break;
			}
		}

		/// <summary>
		/// ����б�˫���¼��Ĵ�����
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objArg"></param>
		private void m_mthListViewDoubleClick(object p_objSender,EventArgs p_objArg)
		{
			m_mthSetSelectedValue();
		}

		/// <summary>
		/// ���ݿؼ���������б�
		/// </summary>
		private void m_mthAdjustListView()
		{
			if(blnIsAdWidth)
				m_lsvLike.Width = m_ctlCurrent.Width;
			m_lsvLike.Left = m_ctlCurrent.Left;
//			m_lsvLike.Top = m_ctlCurrent.Top + m_ctlCurrent.Height;
			m_lsvLike.Top = m_ctlCurrent.Top - m_lsvLike.Height;

			m_mthChangeListViewLastColumnWidth();
		}

		/// <summary>
		/// ��������б��еĿ�ȣ�ʹ֮�����ֺ����������
		/// </summary>
		private void m_mthChangeListViewLastColumnWidth()
		{
			if(m_lsvLike.Columns.Count>0)
			{
				int intLastColumnWidth=m_lsvLike.Width;
				for(int i=0;i<m_lsvLike.Columns.Count-1;i++)
				{
					intLastColumnWidth -= m_lsvLike.Columns[i].Width;
				}
				if(m_lsvLike.Items.Count>6)
					intLastColumnWidth -=18;

				m_lsvLike.Columns[m_lsvLike.Columns.Count-1].Width =intLastColumnWidth;
			}
		}

		/// <summary>
		/// ��ʼ������б������
		/// </summary>
		/// <param name="p_strText">�ؼ����ı�</param>
		/// <returns>��ʼ�����������0���ɹ���</returns>
		private long m_lngInitListItem(string p_strText)
		{
			return m_lngInitListItemSub(p_strText,m_lsvLike);
		}

		/// <summary>
		/// ����ѡ��Ľ���б�
		/// </summary>
		private void m_mthSetSelectedValue()
		{
			if(m_lsvLike.SelectedItems.Count == 1)
			{
				ListViewItem lviSelected = m_lsvLike.SelectedItems[0];

				m_mthSetSelectValueSub(m_ctlCurrent,lviSelected);
			}
			else if(m_lsvLike.Items.Count == 1)
			{
				ListViewItem lviSelected = m_lsvLike.Items[0];

				m_mthSetSelectValueSub(m_ctlCurrent,lviSelected);
			}

			m_lsvLike.Visible = false;
		}
		
		/// <summary>
		/// �ؼ�ʧȥ�����¼��Ĵ�����
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objArg"></param>
		private void m_mthControlLostFocus(object p_objSender,EventArgs p_objArg)
		{
			//�����ǰ���㲻�ڵ�ǰ�ؼ������б��ϣ����ؽ���б�
			if(!(p_objSender != null && m_ctlCurrent != null && p_objSender.Equals(m_ctlCurrent) && m_lsvLike.Focused))
				m_lsvLike.Visible = false;
		}

		/// <summary>
		/// ����б�ʧȥ�����¼��Ĵ�����
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objArg"></param>
		private void m_mthListViewLostFocus(object p_objSender,EventArgs p_objArg)
		{
			//�����ǰ���㲻�ڵ�ǰ�ؼ������б��ϣ����ؽ���б�
			if(!(m_ctlCurrent != null && m_ctlCurrent.Focused))
				m_lsvLike.Visible = false;
		}

		#region �������غ���
		/// <summary>
		/// ��ʼ������б������
		/// </summary>
		/// <param name="p_strText">�ؼ����ı�</param>
		/// <param name="p_lsvLike">����б�</param>
		/// <returns>��ʼ�����������0���ɹ���</returns>
		protected abstract long m_lngInitListItemSub(string p_strText,ListView p_lsvLike);
		
		/// <summary>
		/// ����ѡ��Ľ���б�
		/// </summary>
		/// <param name="p_ctlCurrent">��ǰ�ؼ�</param>
		/// <param name="p_lviSelected">�û�ѡ��Ĳ�ѯ��Ŀ</param>
		protected abstract void m_mthSetSelectValueSub(Control p_ctlCurrent,ListViewItem p_lviSelected);
		#endregion �������غ���
	}
}
