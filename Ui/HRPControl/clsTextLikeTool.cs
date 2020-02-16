using System;
using System.Windows.Forms;
using System.Collections;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// 根据控件文本内容模糊查询工具
	/// </summary>
	public abstract class clsTextLikeTool
	{
		/// <summary>
		/// 查询结果列表
		/// </summary>
		private ListView m_lsvLike;

		/// <summary>
		/// 当前进行查询的控件
		/// </summary>
		private Control m_ctlCurrent;

		/// <summary>
		/// 前一次进行查询的控件文本内容
		/// </summary>
		private string m_strPreText;

		/// <summary>
		/// 存放所有的控件
		/// </summary>
		protected ArrayList m_arlControls;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="p_lsvLike">查询结果列表</param>
		public clsTextLikeTool(ListView p_lsvLike)
		{
			m_mthInitComponent(p_lsvLike);
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public clsTextLikeTool()
		{
			m_mthInitComponent(null);
		}

		private bool blnIsAdWidth = true;

		/// <summary>
		/// 初始化函数
		/// </summary>
		/// <param name="p_lsvLike">查询结果列表</param>
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
		/// 添加需要进行模糊查询的控件
		/// </summary>
		/// <param name="p_ctlControl">控件</param>
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
		/// 添加多个需要进行模糊查询的控件
		/// </summary>
		/// <param name="p_ctlArr">控件</param>
		/// <param name="blnIsAd">是否根据textBox自动调整大小</param>
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
		/// 控件KeyDown事件的处理函数
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objKey"></param>
		private void m_mthControlKeyDown(object p_objSender,KeyEventArgs p_objKey)
		{
			/*
			 * Enter：根据控件文本内容进行查询。
			 *            查询结果：0，光标还在控件上；1，自动设置查询结果到控件；>1，显示列表，自动定位到第一个结果。
			 * Down： 如果结果列表没有显示或者控件文本内容改变，根据控件文本内容进行查询。
			 *            查询结果：0，光标还在控件上；>=1，显示列表，自动定位到第一个结果。
			 *        其他情况,自动定位到第一个结果。
			 * ESC：  隐藏结果列表。
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
		/// 结果列表KeyDown事件的处理函数
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objKey"></param>
		private void m_mthListViewKeyDown(object p_objSender,KeyEventArgs p_objKey)
		{
			/*
			 * Space,
			 * Enter：设置查询结果到控件。
			 * Up：   当前结果列表的选择项目是第一个，把光标设置到控件中。
			 * ESC：  隐藏结果列表。
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
		/// 结果列表双击事件的处理函数
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objArg"></param>
		private void m_mthListViewDoubleClick(object p_objSender,EventArgs p_objArg)
		{
			m_mthSetSelectedValue();
		}

		/// <summary>
		/// 根据控件调整结果列表
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
		/// 调整结果列表列的宽度，使之不出现横向滚动条。
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
		/// 初始化结果列表的内容
		/// </summary>
		/// <param name="p_strText">控件的文本</param>
		/// <returns>初始化结果。大于0，成功。</returns>
		private long m_lngInitListItem(string p_strText)
		{
			return m_lngInitListItemSub(p_strText,m_lsvLike);
		}

		/// <summary>
		/// 设置选择的结果列表
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
		/// 控件失去焦点事件的处理函数
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objArg"></param>
		private void m_mthControlLostFocus(object p_objSender,EventArgs p_objArg)
		{
			//如果当前焦点不在当前控件或结果列表上，隐藏结果列表
			if(!(p_objSender != null && m_ctlCurrent != null && p_objSender.Equals(m_ctlCurrent) && m_lsvLike.Focused))
				m_lsvLike.Visible = false;
		}

		/// <summary>
		/// 结果列表失去焦点事件的处理函数
		/// </summary>
		/// <param name="p_objSender"></param>
		/// <param name="p_objArg"></param>
		private void m_mthListViewLostFocus(object p_objSender,EventArgs p_objArg)
		{
			//如果当前焦点不在当前控件或结果列表上，隐藏结果列表
			if(!(m_ctlCurrent != null && m_ctlCurrent.Focused))
				m_lsvLike.Visible = false;
		}

		#region 子类重载函数
		/// <summary>
		/// 初始化结果列表的内容
		/// </summary>
		/// <param name="p_strText">控件的文本</param>
		/// <param name="p_lsvLike">结果列表</param>
		/// <returns>初始化结果。大于0，成功。</returns>
		protected abstract long m_lngInitListItemSub(string p_strText,ListView p_lsvLike);
		
		/// <summary>
		/// 设置选择的结果列表
		/// </summary>
		/// <param name="p_ctlCurrent">当前控件</param>
		/// <param name="p_lviSelected">用户选择的查询项目</param>
		protected abstract void m_mthSetSelectValueSub(Control p_ctlCurrent,ListViewItem p_lviSelected);
		#endregion 子类重载函数
	}
}
