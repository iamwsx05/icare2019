using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// 带检索功能的库存药品输入框
	/// Create by kong 2004-06-14
	/// </summary>
	public class ctlStorageMedTextBox : com.digitalwave.Utility.ctlExtTextBox	//Utility.dll
	{
		private string m_strStorageID = "";

		/// <summary>
		/// 带模糊查找的库存药品输入框
		/// </summary>
		public ctlStorageMedTextBox()
		{
			this.m_bolLostFocusEffect = false;
			CreateListView();
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtText_KeyDown);
		}

		#region 创建列表头
		/// <summary>
		/// 创建列表头
		/// </summary>
		private void CreateListView()
		{
			// 
			// clhMedID
			// 
//			ColumnHeader clhMedID = new ColumnHeader();
//			clhMedID.Text = "药品代码";
//			clhMedID.Width = 80;
			this.m_lsvList.Columns[0].Width = 80;
			this.m_lsvList.Columns[0].Text = "药品代码";
			// 
			// clhMedName
			// 
			ColumnHeader clhMedName = new ColumnHeader();
			clhMedName.Text = "药品名称";
			clhMedName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			clhMedName.Width = 100;
			// 
			// clhMedSpec
			//
			ColumnHeader clhMedSpec = new ColumnHeader();
			clhMedSpec.Text = "药品规格";
			clhMedSpec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			clhMedSpec.Width = 80;
			// 
			// clhUnit
			// 
			ColumnHeader clhUnit = new ColumnHeader();
			clhUnit.Text = "单位";
			clhUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			clhUnit.Width = 80;
			// 
			// clhLotNo
			// 
			ColumnHeader clhLotNo = new ColumnHeader();
			clhLotNo.Text = "批号";
			clhLotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			clhLotNo.Width = 100;
			// 
			// clhUsefulLife
			// 
			ColumnHeader clhUsefulLife = new ColumnHeader();
			clhUsefulLife.Text = "有效期";
			clhUsefulLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			clhUsefulLife.Width = 100;
			// 
			// clhProductor
			// 
			ColumnHeader clhProductor = new ColumnHeader();
			clhProductor.Text = "产地";
			clhProductor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			clhProductor.Width = 150;
			// 
			// clhBuyPrice
			// 
			ColumnHeader clhBuyPrice = new ColumnHeader();
			clhBuyPrice.Text = "购进价";
			clhBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// clhSaleBuy
			// 
			ColumnHeader clhSaleBuy = new ColumnHeader();
			clhSaleBuy.Text = "零售价";
			clhSaleBuy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// clhStorageNow
			//
			ColumnHeader clhStorageNow = new ColumnHeader();
			clhStorageNow.Text = "库存量";
			clhStorageNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

			this.m_lsvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						//clhMedID,
																						clhMedName,
																						clhMedSpec,
																						clhUnit,
																						clhLotNo,
																						clhUsefulLife,
																						clhProductor,
																						clhBuyPrice,
																						clhSaleBuy,
																						clhStorageNow});
			
		}
		#endregion

		#region 检索数据并填充到列表
		/// <summary>
		/// 检索数据并填充到列表
		/// </summary>
		/// <param name="p_strInput">输入的数据</param>
		protected override void m_mthLoadListData(string p_strInput)
		{
			// TODO:  添加 ctlStorageMedTextBox.m_mthLoadListData 实现
//			int intType = -1;
//			try
//			{
//				int.Parse(p_strInput);
//				intType = 0;
//			}
//			catch
//			{
//				intType = 1;
//			}

			p_strInput += "%";

			p_strInput = p_strInput.ToUpper();
			
			clsStorageMedDetail_VO[] objItems = null;
			string strStorageID = m_strStorageID;
			string strSQL;
			
			if(strStorageID.Trim() == "")
			{
				strSQL = "WHERE STORAGEID_CHR LIKE '" + strStorageID + "%' AND MEDICINEID_CHR like '" + p_strInput + "%' AND USEFULSTATUS_INT=1 ORDER BY MedicineID_chr,UnitId_chr";
			}
			else
			{
				strSQL = "WHERE STORAGEID_CHR = '" + strStorageID + "' AND MEDICINEID_CHR like '" + p_strInput + "%' AND USEFULSTATUS_INT=1 ORDER BY MedicineID_chr,UnitId_chr";
			}

			long lngRes = clsPublicParm.s_lngFindStorageMedicineByAny(strSQL,out objItems);

			if(objItems == null || objItems.Length <= 0)
			{
				return;
			}

			ListViewItem lsvItem = null;
			for(int i =0;i<objItems.Length;i++)
			{
				lsvItem = new ListViewItem(objItems[i].m_objMedicine.m_strMedicineID.Trim());
				lsvItem.SubItems.Add(objItems[i].m_objMedicine.m_strMedicineName.Trim());
				lsvItem.SubItems.Add(objItems[i].m_objMedicine.m_strMedSpec.Trim());
				lsvItem.SubItems.Add(objItems[i].m_objUnit.m_strUnitName.Trim());
				lsvItem.SubItems.Add(objItems[i].m_strLotNo.Trim());
				lsvItem.SubItems.Add(objItems[i].m_strUsefulLife.Trim());
				lsvItem.SubItems.Add(objItems[i].m_objProduct.m_strVendorName.Trim());
				lsvItem.SubItems.Add(objItems[i].m_fltBuyUnitPrice.ToString("######.00"));
				lsvItem.SubItems.Add(objItems[i].m_fltSaleUnitPrice.ToString("######.00"));
				lsvItem.SubItems.Add(objItems[i].m_fltCurQty.ToString("######.00"));
				lsvItem.Tag = objItems[i];

				this.m_lsvList.Items.Add(lsvItem);
			}

		}
		#endregion

		private void m_txtText_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
					m_mthShowList();
					break;
				case Keys.Down:
					m_mthListFocus();
					break;
				case Keys.Escape:
					m_mthListCollapse(false);
					break;
			}
		}

		#region 显示列表
		/// <summary>
		/// 显示列表
		/// </summary>
		private bool m_mthShowList()
		{
			
			this.m_lsvList.Items.Clear();
			m_mthLoadListData(this.Text.Trim());

			if(!m_bolCheckItemExisted())
				return false;

			this.FindForm().Controls.Add(this.m_lsvList);
			Point objL =  this.Parent.PointToScreen(this.Location);
			objL.Offset(0,this.Height);
			objL = this.FindForm().PointToClient(objL);
			this.m_lsvList.Location = objL;
			this.m_lsvList.BringToFront();

			if(m_lsvList.Items.Count == 1)
			{
				m_lsvList.Items[0].Selected = true;
				m_mthListCollapse(true);
				return false;
			}
			else
			{
				m_lsvList.Focus();
				m_lsvList.Items[0].Selected = true;
				
			}
			return true;
		}
		#endregion

		#region 显示列表获得焦点
		/// <summary>
		/// 显示列表获得焦点
		/// </summary>
		private void m_mthListFocus()
		{
			if(!m_bolCheckItemExisted())
				return;
			
			if(m_mthShowList())
			{
				this.m_lsvList.Focus();
				//				this.m_lsvList.EnsureVisible(0);
				if(m_lsvList.Items.Count != 0)
				{
					m_lsvList.Items[0].Selected = true;
					m_lsvList.Items[0].Focused = true;
				}
			}
		}
		#endregion

		#region 判断
		/// <summary>
		/// 判断
		/// </summary>
		/// <returns></returns>
		private bool m_bolCheckItemExisted()
		{
			if(m_lsvList.Items.Count <=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		#endregion

		protected override void m_mthSetListAppearance()
		{
			// TODO:  添加 ctlStorageMedTextBox.m_mthSetListAppearance 实现
			this.m_lsvList.GridLines = true;
			this.m_lsvList.Width = 450;
			this.m_lsvList.Height = 120;
			this.m_lsvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lsvList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
		}
	
		protected override object m_strGetValueObject()
		{
			// TODO:  添加 ctlStorageMedTextBox.m_strGetValueObject 实现
			if(this.m_lsvList.SelectedItems.Count != 0 && this.m_lsvList.SelectedItems[0].Tag != null)
				return this.m_lsvList.SelectedItems[0].Tag;
			else
				return null;
		}
	
		protected override string m_strGetItemName()
		{
			// TODO:  添加 ctlStorageMedTextBox.m_strGetItemName 实现
			if(this.m_ObjValueObject == null)
				return null;
			else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objMedicine == null)
				return null;
			else
				return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objMedicine.m_strMedicineID;
		}

		public override void m_mthClear()
		{
			// TODO:  添加 ctlStorageMedTextBox.m_mthClear 实现
			this.m_lsvList.Items.Clear();
//			base.m_mthClear ();			
		}

		public void m_mthLeave()
		{
			this.m_mthListCollapse(false);
		}


		#region 控件属性

		/// <summary>
		/// 列表宽度
		/// </summary>
		[Browsable(true),
		 Description("列表宽度")
		]
		public int ListViewWidth
		{
			set
			{
				if(value >300)
				{
					this.m_lsvList.Width = value;
				}
			}
			get
			{
				return this.m_lsvList.Width;
			}
		}

		/// <summary>
		/// 列表高度
		/// </summary>
		[Browsable(true),
		 Description("列表宽度")
		]
		public int ListViewHeight
		{
			set
			{
				if(value>100)
				{
					this.m_lsvList.Height = value;
				}
			}
			get
			{
				return this.m_lsvList.Height;
			}
		}

		/// <summary>
		/// 仓库代码
		/// </summary>
		[Browsable(false)]
		public string strStorageID
		{
			get
			{
				return m_strStorageID;
			}
			set
			{
				m_strStorageID = value;
			}
		}

		/// <summary>
		/// 库存药品信息
		/// </summary>
		[Browsable(false)]
		public clsStorageMedDetail_VO objStorageMedicine
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else 																				
					return (clsStorageMedDetail_VO)this.m_ObjValueObject;
			}
		}
		
		/// <summary>
		/// 药品代码
		/// </summary>
		[Browsable(false)]
		public string strMedicineID
		{
			get
			{
				return this.m_strGetItemName();
			}
		}

		/// <summary>
		/// 药品名称
		/// </summary>
		[Browsable(false)]
		public string strMedicineName
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objMedicine == null)
					return null;
				else
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objMedicine.m_strMedicineName;
			}
		}

		/// <summary>
		/// 药品规格
		/// </summary>
		[Browsable(false)]
		public string strMedicineSpec
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objMedicine == null)
					return null;
				else
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objMedicine.m_strMedSpec;
			}
		}

		/// <summary>
		/// 药品信息
		/// </summary>
		[Browsable(false)]
		public clsMedicine_VO objMedicine
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objMedicine == null)
				{
					return null;
				}
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objMedicine;
			}
		}

		/// <summary>
		/// 单位代码
		/// </summary>
		[Browsable(false)]
		public string strUnitID
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objUnit == null)
				{
					return null;
				}
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objUnit.m_strUnitID;
			}
		}

		/// <summary>
		/// 单位名称
		/// </summary>
		[Browsable(false)]
		public string strUnitName
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objUnit == null)
				{
					return null;
				}
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objUnit.m_strUnitName;
			}
		}

		/// <summary>
		/// 单位信息
		/// </summary>
		[Browsable(false)]
		public clsUnit_VO objUnit
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objUnit == null)
				{
					return null;
				}
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objUnit;
			}
		}

		/// <summary>
		/// 库存数量
		/// </summary>
		[Browsable(false)]
		public float fltQty
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return 0;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_fltCurQty;
			}
		}

		/// <summary>
		/// 购进价
		/// </summary>
		[Browsable(false)]
		public float fltBuyPrice
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return 0;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_fltBuyUnitPrice;
			}
		}

		/// <summary>
		/// 零售价
		/// </summary>
		[Browsable(false)]
		public float fltSalePrice
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return 0;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_fltSaleUnitPrice;
			}
		}

		/// <summary>
		/// 批发价
		/// </summary>
		[Browsable(false)]
		public float fltWholeSalePrice
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return 0;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_fltWholesaleUnitPrice;
			}
		}

		/// <summary>
		/// 有效期
		/// </summary>
		[Browsable(false)]
		public string strUsefulLife
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_strUsefulLife;
			}
		}

		/// <summary>
		/// 批号
		/// </summary>
		[Browsable(false)]
		public string strLotNo
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_strLotNo;
			}
		}

		/// <summary>
		/// 系统批号
		/// </summary>
		[Browsable(false)]
		public string strSysLotNo
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_strSysLotNo;
			}
		}

		/// <summary>
		/// 厂家代码
		/// </summary>
		[Browsable(false)]
		public string strProductorID
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objProduct == null)
					return null;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objProduct.m_strVendorID;
			}
		}

		/// <summary>
		/// 厂家名称
		/// </summary>
		[Browsable(false)]
		public string strProductorName
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objProduct == null)
					return null;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objProduct.m_strVendorName;
			}
		}

		/// <summary>
		/// 厂家信息
		/// </summary>
		[Browsable(false)]
		public clsVendor_VO objProductor
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else if(((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objProduct == null)
					return null;
				else																					
					return ((clsStorageMedDetail_VO)this.m_ObjValueObject).m_objProduct;
			}
		}

		#endregion

	}
}
