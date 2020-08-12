using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;
using System.ComponentModel; 
using System.IO;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedicineStorageCheck 的摘要说明。
	/// </summary>
	public class clsControlMedicineStorageCheck:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedicineStorageCheck()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		frmMedicineStorageCheck m_objViewer;
		clsDomainControlMedicineStorageCheck objSVC=new clsDomainControlMedicineStorageCheck();
		System.Data.DataTable dtCheckBill;
		System.Data.DataTable dtCheckBillDetail;
		/// <summary>
		/// 盘点明细
		/// </summary>
		private System.Collections.ArrayList m_objItemDetail;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmMedicineStorageCheck)frmMDI_Child_Base_in;
		}
		/// <summary>
		/// 
		/// </summary>
		public void m_mthInitForm()
		{
			this.m_objViewer.m_dtgCheckDetail.m_mthAddEnterToSpaceColumn(8);
			this.m_objViewer.m_dtgCheckDetail.m_mthAddEnterToSpaceColumn(12);
			this.m_objViewer.m_dtpCreateDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");;
			m_mthInitCboStorage();
//			m_mthInitCboMedicinePrepType();
			m_mthInitCboPeriod();
		}


		#region 初始化仓库
		private void m_mthInitCboStorage()
		{
			System.Data.DataTable dt = new DataTable();
			long lngRes=this.objSVC.m_lngGetStorage(this.m_objViewer.strStorageFlag,out dt);
			if(lngRes>0&&dt.Rows.Count>0)
			{
				for(int i1=0;i1<dt.Rows.Count;i1++)
				{
					this.m_objViewer.m_cboStorage.Item.Add(dt.Rows[i1]["STORAGENAME_VCHR"].ToString(),dt.Rows[i1]["STORAGEID_CHR"].ToString());
				}
				this.m_objViewer.m_cboStorage.SelectedIndex=0;
			}
		}
		#endregion 初始化仓库
		/// <summary>
		/// 保存当前财务期
		/// </summary>
		public string strCurrPeriod="";
		private void m_mthInitCboPeriod()
		{
			System.Data.DataTable dt = new DataTable();
			long lngRes=this.objSVC.m_lngGetPeriod(out dt);		
			string nowdate=clsPublicParm.s_datGetServerDate().Date.ToString();
			if(lngRes>0&&dt.Rows.Count>0)
			{			
				this.m_objViewer.m_cboPeriod.DisplayMember = "PERIODNAME";
				this.m_objViewer.m_cboPeriod.ValueMember = "PERIODID_CHR";
				this.m_objViewer.m_cboPeriod.DataSource = dt;

				for(int i1=0;i1<dt.Rows.Count;i1++)
				{
					if(Convert.ToDateTime(nowdate)>=Convert.ToDateTime(dt.Rows[i1]["STARTDATE_DAT"].ToString())&&Convert.ToDateTime(nowdate)<=Convert.ToDateTime(dt.Rows[i1]["ENDDATE_DAT"].ToString()))
					{
						this.m_objViewer.m_cboPeriod.SelectedIndex=i1;
						strCurrPeriod=dt.Rows[i1]["PERIODID_CHR"].ToString();
						break;
					}
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public void m_mthFindCheckBill()
		{
			//获取单据
			if(dtCheckBill == null)
			{
				long lngRes=this.objSVC.m_lngGetCheckBill(out dtCheckBill);
			}
			//清空明细
			this.m_objViewer.m_dtgCheckDetail.m_mthDeleteAllRow();
			//选取仓库下面的盘点单
			m_mthBindCheckBill(this.m_objViewer.m_cboStorage.SelectItemValue,this.m_objViewer.m_cboPeriod.SelectedValue.ToString());			
		}

		#region 变量
		/// <summary>
		/// 保存盘点明细数据的副本
		/// </summary>
		DataTable TempdtCheckBillDetail=null;

		#endregion


		#region 邦定盘点单
		/// <summary>
		/// 邦定盘点单
		/// </summary>
		/// <param name="strStorageID"></param>
		/// <param name="strPeriodID"></param>
		private void m_mthBindCheckBill(string strStorageID,string strPeriodID)
		{	
			this.m_objViewer.m_DglCheckBill1.m_mthDeleteAllRow();
			this.m_objViewer.m_DglCheckBill.m_mthDeleteAllRow();
			System.Data.DataRow[] drs = dtCheckBill.Select("STORAGEID_CHR = '"+strStorageID.Trim()+"' AND PERIODID_CHR='"+strPeriodID+"' and FLAG_INT="+this.m_objViewer.strStorageFlag);
			for(int i = 0 ;i < drs.Length;i++)
			{				
				string strStatue = "";
				switch(drs[i]["PSTATUS_INT"].ToString())
				{
					case "-1":
						strStatue = "未审核";
						break;
					case "0":
						strStatue = "未审核";
						break;
					case "1":
						strStatue = "已审核";
						break;
					case "2":
						strStatue = "被拒绝";
						break;
				}
				object[] objectVale = new object[]{strStatue,drs[i]["STORAGECHECKID_CHR"].ToString(),
													drs[i]["CreaorName"].ToString(),
													drs[i]["CHECK_DAT"].ToString(),													
													drs[i]["AuditName"].ToString(),
													drs[i]["ADUITDATE_DAT"].ToString(),
													drs[i]["PSTATUS_INT"].ToString(),
													drs[i]["REMARK_VCHR"].ToString()};
				if(drs[i]["PSTATUS_INT"].ToString()=="-1"||drs[i]["PSTATUS_INT"].ToString()=="0")
					this.m_objViewer.m_DglCheckBill.m_mthAppendRow(objectVale);
				else
					this.m_objViewer.m_DglCheckBill1.m_mthAppendRow(objectVale);

			}
			//邦定数据表
		//	this.m_objViewer.m_DglCheckBill.m_mthSetDataTable(dt);

		}
		#endregion 邦定盘点单

		#region 新增盘点单
		/// <summary>
		/// 新增盘点单
		/// </summary>
        public void m_mthAddNewCheckBill(string strFlag, clsHISMedType_VO[] medType, clsMedicinePrepType_VO[] PrepType, clsMedicineType_VO[] MedTypeArr, bool isShowZero, bool isShowStop)
		{
			string strStorageID = this.m_objViewer.m_cboStorage.SelectItemValue.Trim();
			string strCreatorID = this.m_objViewer.LoginInfo.m_strEmpID;
			string strCreatorName = this.m_objViewer.LoginInfo.m_strEmpName;
			string strRemark = this.m_objViewer.m_txtRemark.Text.Trim();
			clsStorageCheck_VO m_objItemCheck = new clsStorageCheck_VO();
			m_objItemCheck.m_strRemake = strRemark;
			m_objItemCheck.m_strCheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			m_objItemCheck.m_strCreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			m_objItemCheck.m_strFlag = this.m_objViewer.strStorageFlag;
			m_objItemCheck.m_intStatus = 0;
			m_objItemCheck.m_objStorageOrdType = new clsStorageOrdType_VO();
			m_objItemCheck.m_objStorageOrdType.m_strStorageOrdTypeID = "1001";//
			m_objItemCheck.m_objStorage = new clsStorage_VO();
			m_objItemCheck.m_objCreator = new clsEmployeeVO();
			m_objItemCheck.m_objCreator.strEmpID = strCreatorID;
			m_objItemCheck.m_objStorage.m_strStroageID = strStorageID;
			m_objItemCheck.m_objPeriod = new clsPeriod_VO();
			m_objItemCheck.m_objPeriod.m_strPeriodID = this.m_objViewer.m_cboPeriod.SelectedValue.ToString();
			long lngRes = this.objSVC.m_lngDoAddNewStorageCheck(ref m_objItemCheck);
			if( lngRes == 1)
			{
				if(dtCheckBill != null)
				{
					System.Data.DataRow dr = dtCheckBill.NewRow();
					dr["STORAGECHECKID_CHR"] = m_objItemCheck.m_strStorageCheckID;
					dr["CHECK_DAT"] = m_objItemCheck.m_strCheckDate;
					dr["REMARK_VCHR"] = strRemark;
					dr["STORAGEID_CHR"] = strStorageID;
					dr["CREATORID_CHR"] = strCreatorID;
					dr["PERIODID_CHR"] = m_objItemCheck.m_objPeriod.m_strPeriodID;
					dr["CreaorName"] = strCreatorName;
					dr["PSTATUS_INT"] = "-1";
					dr["CREATEDATE_DAT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
					dr["FLAG_INT"] = this.m_objViewer.strStorageFlag;
					dtCheckBill.Rows.Add(dr);
				}
			}
			this.m_objViewer.groupBox4.Tag=m_objItemCheck.m_strStorageCheckID;
			m_mthBindCheckBill(this.m_objViewer.m_cboStorage.SelectItemValue,this.m_objViewer.m_cboPeriod.SelectedValue.ToString());
			this.m_objViewer.m_DglCheckBill.m_mthSelectARow(this.m_objViewer.m_DglCheckBill.RowCount-1);
            if (medType != null && PrepType != null)
            {
                m_mthGetNew(strFlag, medType, PrepType,null, isShowZero, isShowStop);
            }
            else
            {
                m_mthGetNew(strFlag, null, null, MedTypeArr, isShowZero, isShowStop);
            }
			this.m_objViewer.m_cmbClearDetail.Enabled=true;
			this.m_objViewer.m_cboAddCheckDetail.Enabled=true;
			this.m_objViewer.m_cmdAddCheckBill.Enabled=false;
		
		}
		#endregion 新增盘点单
		/// <summary>
		/// 新增盘点单
		/// </summary>
        public void m_mthGetNew(string strFlag, clsHISMedType_VO[] medType, clsMedicinePrepType_VO[] PrepType, clsMedicineType_VO[] MedicineType, bool isShowZero, bool isShowStop)
		{
            this.objSVC.m_lngGetStorageCheckDetail(this.m_objViewer.m_cboStorage.SelectItemValue, out dtCheckBillDetail, strFlag, medType, PrepType,MedicineType, isShowZero, isShowStop);			
			m_mthBinddtgCheckDetail(dtCheckBillDetail);
			TempdtCheckBillDetail=dtCheckBillDetail.Copy();
			if(this.m_objViewer.m_dtgCheckDetail.RowCount>0)
				this.m_objViewer.m_dtgCheckDetail.CurrentCell=new DataGridCell(0,8);
		}
		/// <summary>
		/// 恢复数据
		/// </summary>
		public void m_mthRenew()
		{
			if(dtCheckBillDetail!=null&&dtCheckBillDetail.Rows.Count>0)
			{
				for(int i1=0;i1<dtCheckBillDetail.Rows.Count;i1++)
				{
					dtCheckBillDetail.Rows[i1]["REALQTY_DEC"]=dtCheckBillDetail.Rows[i1]["curqty_dec"];
				}
			}
		}

		#region 删除单据
		/// <summary>
		/// 删除单据
		/// </summary>
		/// <returns></returns>
		public long m_lngDelCheckBill()
		{
			if( this.m_objViewer.m_DglCheckBill.CurrentCell.RowNumber < 0)
			{
				return 0;
			}
            string strCheckBillID = this.m_objViewer.m_DglCheckBill.m_objGetRow(this.m_objViewer.m_DglCheckBill.CurrentCell.RowNumber)["STORAGECHECKID_CHR"].ToString().Trim();
            long lngRes = this.objSVC.m_lngDelCheckBill(strCheckBillID);
            System.Data.DataRow[] drs = dtCheckBill.Select("STORAGECHECKID_CHR = '" + strCheckBillID + "'");
            if (drs.Length > 0)
            {
                drs[0].Delete();
                dtCheckBill.AcceptChanges();
            }
            if (this.m_objViewer.m_dtgCheckDetail.Columns.Count > 0)
            {
                this.m_objViewer.m_dtgCheckDetail.m_mthDeleteAllRow();
            }
            m_mthBindCheckBill(this.m_objViewer.m_cboStorage.SelectItemValue, this.m_objViewer.m_cboPeriod.SelectedValue.ToString());
            return lngRes;
		}
		#endregion 删除单据

		public void m_mthCboMedicinePrepType()
		{
			
			m_mthFindtgCheckDetail(TempdtCheckBillDetail);
		}
		public void m_mthFindMedicine()
		{
			m_mthFindtgCheckDetail(TempdtCheckBillDetail);
		}
		public void m_mthGetCheckDetail(string strCheckID,string str1)
		{
			//获取单据
            long lngRes = this.objSVC.m_lngGetCheckDetail(this.m_objViewer.m_cboStorage.SelectItemValue, this.m_objViewer.strStorageFlag, strCheckID, out dtCheckBillDetail, str1,this.m_objViewer.m_tabAduit.SelectedIndex==0?false:true);
			m_mthBinddtgCheckDetail(dtCheckBillDetail);		
			TempdtCheckBillDetail=dtCheckBillDetail;
		}

		#region 查找明细
		/// <summary>
		/// 查找明细
		/// </summary>
		/// <param name="dt"></param>
		private void m_mthFindtgCheckDetail(System.Data.DataTable dt)
		{
			if(dt == null)
				return;
			string strWhere="";
			string strText=this.m_objViewer.textBox3.Text.ToUpper();
			string strType="";
			switch(this.m_objViewer.comboBox2.Text)
			{
				case "药品代码":
					strType="ASSISTCODE_CHR";
					break;
				case "药品名称":
					strType="MEDICINENAME_VCHR";
					break;
				case "拼音码":
					strType="PYCODE_CHR";
					break;
				case "五笔码":
					strType="WBCODE_CHR";
					break;
			}
			string strComm="+";
			string[] strObj= strText.Split(strComm.ToCharArray());
			for(int i1=0;i1<strObj.Length;i1++)
			{
				if(i1==0)
				{
					strWhere+=strType +" like '" +strObj[i1].Trim().ToUpper()+"%'";
				}
				else
				{
					strWhere+="  or "+strType +" like '" +strObj[i1].Trim().ToUpper()+"%'";
				}
			}
			System.Data.DataRow[] drs = dt.Select(strWhere);
            if(this.m_objViewer.m_dtgCheckDetail.Columns.Count>0)
			this.m_objViewer.m_dtgCheckDetail.m_mthDeleteAllRow();
			double money = 0;
			double money1 = 0;
			double money2 = 0;

            double money3 = 0;
            double money4 = 0;
            double money5= 0;
			for(int i = 0 ; i < drs.Length;i++)
			{
                if(drs[i]["lostMoney"]!=null&&drs[i]["lostMoney"].ToString()!="")
			        money += double.Parse(drs[i]["lostMoney"].ToString());
                 if (drs[i]["CALCMoney"] != null && drs[i]["CALCMoney"].ToString() != "")
					money1 += double.Parse(drs[i]["CALCMoney"].ToString());
                if (drs[i]["REALMoney"] != null && drs[i]["REALMoney"].ToString() != "")
					money2 += double.Parse(drs[i]["REALMoney"].ToString());

                if (drs[i]["UNITPRICE_MNY"] != null && drs[i]["curqty_dec"] != null)
                {
                    money3 += double.Parse(drs[i]["UNITPRICE_MNY"].ToString()) * double.Parse(drs[i]["curqty_dec"].ToString());
                }
                if (drs[i]["UNITPRICE_MNY"] != null && drs[i]["REALQTY_DEC"] != null)
                {
                    money4 += double.Parse(drs[i]["UNITPRICE_MNY"].ToString()) * double.Parse(drs[i]["REALQTY_DEC"].ToString());
                }
                if (drs[i]["lostSalmoney"]!=null)
                {
                    money5 += double.Parse(drs[i]["lostSalmoney"].ToString());
                }
			}
			this.m_objViewer.label10.Text = money1.ToString();
			this.m_objViewer.label14.Text = money2.ToString();
			this.m_objViewer.m_labLostMoney.Text = money.ToString();


            this.m_objViewer.label26.Text = money3.ToString();
            this.m_objViewer.label29.Text = money4.ToString();
            this.m_objViewer.label32.Text = money5.ToString();
			DataTable tempdt=dt.Clone();
			for(int i3=0;i3<drs.Length;i3++)
			{
				DataRow newRow=tempdt.NewRow();
				for(int i2=0;i2<tempdt.Columns.Count;i2++)
				{
					newRow[i2]=drs[i3][i2];
				}
				tempdt.Rows.Add(newRow);
			}
			this.m_objViewer.m_dtgCheckDetail.m_mthSetDataTable(tempdt);
		}
		#endregion
		#region 返回
		/// <summary>
		/// 返回
		/// </summary>
		public void m_mthReturn()
		{
			dtCheckBillDetail=TempdtCheckBillDetail.Copy();
            m_Counmoney(dtCheckBillDetail);
			this.m_objViewer.m_dtgCheckDetail.m_mthSetDataTable(dtCheckBillDetail);
		}
		#endregion
		#region 邦定单据明细
		/// <summary>
		/// 邦定单据明细
		/// </summary>
		private void m_mthBinddtgCheckDetail(DataTable dt)
		{
			if(dt == null)
				return;
			this.m_objViewer.m_dtgCheckDetail.m_mthDeleteAllRow();
            m_Counmoney(dt);
            temp = m_mthGetUICheckDetail();
		}
		#endregion

        #region 计算
        private void m_Counmoney(DataTable dt)
        {
            double money = 0;
            double money1 = 0;
            double money2 = 0;

            double money3 = 0;
            double money4 = 0;
            double money5 = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i]["lostMoney"] != null && dt.Rows[i]["lostMoney"].ToString() != "")
                    money += double.Parse(dt.Rows[i]["lostMoney"].ToString());
                if (dt.Rows[i]["CALCMoney"] != null && dt.Rows[i]["CALCMoney"].ToString() != "")
                    money1 += double.Parse(dt.Rows[i]["CALCMoney"].ToString());
                if (dt.Rows[i]["REALMoney"] != null && dt.Rows[i]["REALMoney"].ToString() != "")
                    money2 += double.Parse(dt.Rows[i]["REALMoney"].ToString());
                if (dt.Rows[i]["UNITPRICE_MNY"] != null && dt.Rows[i]["curqty_dec"] != null)
                {
                    money3 += double.Parse(dt.Rows[i]["UNITPRICE_MNY"].ToString()) * double.Parse(dt.Rows[i]["curqty_dec"].ToString());
                }
                if (dt.Rows[i]["UNITPRICE_MNY"] != null && dt.Rows[i]["REALQTY_DEC"] != null)
                {
                    money4 += double.Parse(dt.Rows[i]["UNITPRICE_MNY"].ToString()) * double.Parse(dt.Rows[i]["REALQTY_DEC"].ToString());
                }
                if (dt.Rows[i]["lostSalmoney"] != null)
                {
                    money5 += double.Parse(dt.Rows[i]["lostSalmoney"].ToString());
                }
            }
            this.m_objViewer.label10.Text = money1.ToString();
            this.m_objViewer.label14.Text = money2.ToString();
            this.m_objViewer.m_labLostMoney.Text = money.ToString();
            this.m_objViewer.label26.Text = money3.ToString();
            this.m_objViewer.label29.Text = money4.ToString();
            this.m_objViewer.label32.Text = money5.ToString();
            this.m_objViewer.m_dtgCheckDetail.m_mthSetDataTable(dt);
        }
        #endregion
        #region 获取界面上的盘点明细
        /// <summary>
		/// 获取界面上的盘点明细
		/// </summary>
		/// <param name="auditflag"></param>
		/// <returns></returns>
		clsStorageCheckDetail_VO[] temp;
		private clsStorageCheckDetail_VO[] m_mthGetUICheckDetail()
		{
			if(m_objItemDetail != null)
			{
				m_objItemDetail = null;
			}
			if( this.m_objViewer.m_dtgCheckDetail.RowCount==0)
			{
				return temp;
			}
			temp = new clsStorageCheckDetail_VO[this.m_objViewer.m_dtgCheckDetail.RowCount];
			for(int i = 0 ; i <this.m_objViewer.m_dtgCheckDetail.RowCount;i++)
			{
				temp[i]=new clsStorageCheckDetail_VO();
                if (this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["medicineid_chr"].ToString() != "")
                {
                    temp[i].m_strstorageID = this.m_objViewer.m_cboStorage.SelectItemValue;
                    temp[i].m_strMEDICINEID_CHR = this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["medicineid_chr"].ToString();
                    temp[i].m_strStorageCheckID = (string)this.m_objViewer.groupBox4.Tag;
                    temp[i].m_strRowNo = i.ToString();
                    temp[i].m_strSysLotNo = this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["syslotno_chr"].ToString();
                    if (this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["REALQTY_DEC"].ToString().Trim() != "")
                        temp[i].m_fltRealQty = float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["REALQTY_DEC"].ToString());
                    else
                    {
                        temp[i].m_fltRealQty = 0;
                    }
                    temp[i].m_fltCalcQty = float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["curqty_dec"].ToString());
                    temp[i].m_fltBuyPrice = float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["buyunitprice_mny"].ToString());
                    temp[i].m_strUnit = this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["unitid_chr"].ToString();
                    temp[i].m_strCheckReason = this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["checkreason_vchr"].ToString();
                    temp[i].m_fltSalePrice = float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i)["UNITPRICE_MNY"].ToString());
                }
                else
                {
                    temp[i] = null;
                }
			}	
			return temp;
		}
		#endregion 获取界面上的盘点明细

		#region 增加盘点单明细
		public long m_mthAddCheckDetail()
		{
			temp=m_mthGetUICheckDetail();
			
			long lngRes = 0;
			if(temp !=null&& temp.Length> 0)
			{
                lngRes = this.objSVC.m_lngDoAddNewStorageCheckDetail(temp, this.m_objViewer.m_txtRemark.Text);	
				m_mthBindCheckBill(this.m_objViewer.m_cboStorage.SelectItemValue,this.m_objViewer.m_cboPeriod.SelectedValue.ToString());
			}
			return lngRes;
		}
		#endregion 增加盘点单明细

		#region 单据审核
		/// <summary>
		/// 单据审核
		/// </summary>
		/// <returns></returns>
		clsPublicParm PublicClass=new clsPublicParm();
		public long m_mthAuditCheckBill()
		{
			long lngRes = 0;
			if(temp !=null&& temp.Length> 0)
			{
				lngRes=this.objSVC.m_lngAuditCheckBill(temp,this.m_objViewer.LoginInfo.m_strEmpID,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),this.m_objViewer.strStorageFlag);
				if(lngRes == 1)
				{
					m_mthBindCheckBill(this.m_objViewer.m_cboStorage.SelectItemValue,this.m_objViewer.m_cboPeriod.SelectedValue.ToString());
					this.m_objViewer.m_cboAddCheckDetail.Enabled = false;
					for(int i1=0;i1<dtCheckBill.Rows.Count;i1++)
					{
						if(temp[0].m_strStorageCheckID==dtCheckBill.Rows[i1]["STORAGECHECKID_CHR"].ToString())
						{
							dtCheckBill.Rows[i1]["PSTATUS_INT"]=2;
							dtCheckBill.Rows[i1]["AuditName"]=this.m_objViewer.LoginInfo.m_strEmpName;
							dtCheckBill.Rows[i1]["ADUITDATE_DAT"]=DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
							string strStatue = "";
							switch(dtCheckBill.Rows[i1]["PSTATUS_INT"].ToString())
							{
								case "-1":
									strStatue = "未审核";
									break;
								case "0":
									strStatue = "未审核";
									break;
								case "1":
									strStatue = "已审核";
									break;
								case "2":
									strStatue = "被拒绝";
									break;
							}
							object[] objectVale = new object[]{strStatue,dtCheckBill.Rows[i1]["STORAGECHECKID_CHR"].ToString(),
																 dtCheckBill.Rows[i1]["CreaorName"].ToString(),
																  dtCheckBill.Rows[i1]["CHECK_DAT"].ToString(),													
																  dtCheckBill.Rows[i1]["AuditName"].ToString(),
																  dtCheckBill.Rows[i1]["ADUITDATE_DAT"].ToString(),
																  dtCheckBill.Rows[i1]["PSTATUS_INT"].ToString(),
																  dtCheckBill.Rows[i1]["REMARK_VCHR"].ToString()};
							if(dtCheckBill.Rows[i1]["PSTATUS_INT"].ToString()=="-1"||dtCheckBill.Rows[i1]["PSTATUS_INT"].ToString()=="0")
								this.m_objViewer.m_DglCheckBill.m_mthAppendRow(objectVale);
							else
								this.m_objViewer.m_DglCheckBill1.m_mthAppendRow(objectVale);
							break;
						}
					}
				}
			}
			else
			{
				PublicClass.m_mthShowWarning(this.m_objViewer.m_DglCheckBill,"空的盘点单不能被审核!");
			}
			return lngRes;
		}
		#endregion 单据审核

		#region 计算出盘亏
		/// <summary>
		///计算出盘亏 
		/// </summary>
		/// <param name="nRow"></param>
		public void m_mthCalLostName(int nRow)
		{
			try
			{
			if(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["REALQTY_DEC"].ToString()=="")
			{
				this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["lostNum"] = System.DBNull.Value;
				this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["lostMoney"] = System.DBNull.Value;
                this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["lostSalmoney"] = System.DBNull.Value;
			}
				float fNum = float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["REALQTY_DEC"].ToString())-float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["curqty_dec"].ToString());
				this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["lostNum"] = fNum.ToString();
				this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["REALMoney"] = float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["REALQTY_DEC"].ToString())*float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["buyunitprice_mny"].ToString());

				this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["lostMoney"] = float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["REALMoney"].ToString())-float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["CALCMoney"].ToString());
                this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["lostSalmoney"] = fNum * float.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(nRow)["UNITPRICE_MNY"].ToString());
				double money = 0;
				double money1 = 0;
				double money2 = 0;

                double money3 = 0;
                double money4 = 0;
                double money5 = 0;
				for(int i1=0;i1<this.m_objViewer.m_dtgCheckDetail.RowCount;i1++)
				{
					if(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["lostMoney"]!=null)
					{
						money+=double.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["lostMoney"].ToString());
					}
					if(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["CALCMoney"]!=null)
					{
						money1+=double.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["CALCMoney"].ToString());
					}
					if(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["REALMoney"]!=null)
					{
						money2+=double.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["REALMoney"].ToString());
					}

                    if (this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["UNITPRICE_MNY"] != null && this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["curqty_dec"]!=null)
                    {
                        money3 += double.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["UNITPRICE_MNY"].ToString()) * double.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["curqty_dec"].ToString());
                    }
                    if (this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["UNITPRICE_MNY"] != null && this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["REALQTY_DEC"] != null)
                    {
                        money4 += double.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["UNITPRICE_MNY"].ToString()) * double.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["REALQTY_DEC"].ToString());
                    }
                    if (this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["lostSalmoney"] != null)
                    {
                        money5 += double.Parse(this.m_objViewer.m_dtgCheckDetail.m_objGetRow(i1)["lostSalmoney"].ToString());
                    }
				}
				this.m_objViewer.label10.Text = money1.ToString();
				this.m_objViewer.label14.Text = money2.ToString();
				this.m_objViewer.m_labLostMoney.Text = money.ToString();

                this.m_objViewer.label26.Text = money3.ToString();
                this.m_objViewer.label29.Text = money4.ToString();
                this.m_objViewer.label32.Text = money5.ToString();
			}
			catch(Exception ee)
			{
				string str = ee.Message;
			}
		}
		#endregion 计算出盘亏

		#region 单据打印
		public void m_mthPrint()
		{
			 //CreateViewer(false);	
		}
		public void m_mthPiandian()
		{
			if(dtCheckBillDetail != null)
			{
				if(dtCheckBillDetail.Rows.Count >0)
				{
					frmPianDianNew pd = new frmPianDianNew();
					pd.m_dt = dtCheckBillDetail;
					pd.m_date = this.m_objViewer.checkDate ;
                    string strMarkTemp = "";
                    if (this.m_objViewer.reMark != "")
                        strMarkTemp = "(" + this.m_objViewer.reMark + ")";
                    pd.m_hos = "单位:" + this.m_objComInfo.m_strGetHospitalTitle();
                    pd.m_title = this.m_objViewer.m_cboStorage.Text + "盘点报表" + strMarkTemp;
                    pd.m_buyMoney ="盘点进价金额:"+ this.m_objViewer.label14.Text;
                    pd.m_SaleMoney ="盘点零售金额:"+ this.m_objViewer.label29.Text;
					pd.Show();
					
				}
				else
				{
					MessageBox.Show("没有可打印的数据");
				}
			}
		}

		public void m_mthPrintpian()
		{
			if(dtCheckBillDetail != null)
			{
				if(dtCheckBillDetail.Rows.Count >0)
				{
					frmPianDianNew pd = new frmPianDianNew();
					pd.m_dt = dtCheckBillDetail;
					pd.m_date = this.m_objViewer.m_dtpCreateDate.Text ;
					pd.m_hos = "单位:"+this.m_objComInfo.m_strGetHospitalTitle();
					pd.m_title = this.m_objViewer.m_cboStorage.Text +"盘点报表";
					try
					{
						pd.print();
					}
					catch(Exception ee)
					{
						MessageBox.Show(ee.ToString());
					}
					
				}
				else
				{
					MessageBox.Show("没有可打印的数据");
				}
			}
		}

		#region 合并盘点单
		/// <summary>
		/// 合并盘点单
		/// </summary>
		public void m_mthUnionData()
		{
			if(this.m_objViewer.m_DglCheckBill.RowCount>1)
			{
				clsStorageCheck_VO p_objItem=new clsStorageCheck_VO();
                System.Collections.Generic.List<string> objArr =new System.Collections.Generic.List<string>();
				p_objItem.m_strRemake="此盘点单是由:";
				for(int i1=0;i1<this.m_objViewer.m_DglCheckBill.RowCount;i1++)
				{
					objArr.Add(this.m_objViewer.m_DglCheckBill.m_objGetRow(i1)["STORAGECHECKID_CHR"].ToString());
					p_objItem.m_strRemake+="["+this.m_objViewer.m_DglCheckBill.m_objGetRow(i1)["STORAGECHECKID_CHR"].ToString()+"]";
				}
				p_objItem.m_strCreateDate=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				p_objItem.m_strCheckDate=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				p_objItem.m_objStorage=new clsStorage_VO();
				p_objItem.m_objStorage.m_strStroageID=this.m_objViewer.m_cboStorage.SelectItemValue;
				p_objItem.m_objPeriod=new clsPeriod_VO();
				p_objItem.m_objPeriod.m_strPeriodID=this.m_objViewer.m_cboPeriod.SelectedValue.ToString();
				p_objItem.m_objCreator=new clsEmployeeVO();
				p_objItem.m_objCreator.strEmpID=this.m_objViewer.LoginInfo.m_strEmpID;
				p_objItem.m_strFlag=this.m_objViewer.strStorageFlag;
				p_objItem.m_objStorageOrdType=new clsStorageOrdType_VO();
				p_objItem.m_objStorageOrdType.m_strStorageOrdTypeID="1001";
				p_objItem.m_strRemake+="合并而成的!";
				DataTable dtcheckOut=new DataTable();
				if(this.objSVC.m_lngUnionData(objArr,p_objItem,out dtcheckOut)==1)
				{
					#region 生成记录文件
					StreamWriter objStream;
					if(!System.IO.File.Exists(@"D:\盘点合并日志.txt"))
					{
						objStream=System.IO.File.CreateText(@"D:\盘点合并日志.txt");
					}
					else
					{
						objStream = new StreamWriter(@"D:\盘点合并日志.txt");
					}
					if(dtcheckOut.Rows.Count>0)
					{
						objStream.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"盘点合并记录,以下记录在合并过程中出现重复数据而产生的,请做相应的处理!!");
						objStream.WriteLine("");
						for(int i1=0;i1<dtcheckOut.Rows.Count;i1++)
						{
							objStream.WriteLine("盘点单据号:"+dtcheckOut.Rows[i1]["单据号"].ToString()+"  药品名称:"+dtcheckOut.Rows[i1]["药品名称"].ToString()+"  药品批号:"+dtcheckOut.Rows[i1]["药品批号"].ToString()+"   实盘数:"+dtcheckOut.Rows[i1]["实盘数"].ToString()+"   操作类型"+dtcheckOut.Rows[i1]["状态"].ToString()+"");
							objStream.WriteLine("");
						}
						PublicClass.m_mthShowWarning(this.m_objViewer.m_DglCheckBill,@"合并过程中出现重复数据,详细情况请查看{D:\盘点合并日志.txt}文件!");
					}
					else
					{
						objStream.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"一次安全的合并!!");
					}
					objStream.Close();
					dtCheckBill=null;
					m_mthFindCheckBill();
					#endregion
				}
				else
				{
					PublicClass.m_mthShowWarning(this.m_objViewer.m_DglCheckBill,"合并数据失败!");
				}
			}
			else
			{
				PublicClass.m_mthShowWarning(this.m_objViewer.m_DglCheckBill,"当前没有可合并的盘点单!");
			}
		}
		#endregion

		public void m_mthPreDetail()
		{ 
			m_mthPiandian();
		}
//		private CrystalDecisions.Windows.Forms.CrystalReportViewer CreateViewer(bool blisShow)
//		{
//			if(dtCheckBillDetail == null)
//			{
//				PublicClass.m_mthShowWarning(this.m_objViewer.m_dtgCheckDetail,"没有可打印的数据!");
//				return null;
//			}
//			string strMedicinePrepType = "";
////			string strMedicinePrepType = this.m_objViewer.m_cboMedicinePrepType.SelectedValue.ToString();
//			System.Data.DataRow[] drs = dtCheckBillDetail.Select("medicinetypeid_chr like '"+strMedicinePrepType+"'");
//			System.Data.DataTable dtView = dtCheckBillDetail.Clone();			
//			for(int i = 0 ; i < drs.Length;i++)
//			{
//				dtView.ImportRow(drs[i]);		
//			}
//			com.digitalwave.iCare.gui.HIS.baotable.CheckDetail detail = new com.digitalwave.iCare.gui.HIS.baotable.CheckDetail();
//			DataRow seleRow=this.m_objViewer.m_DglCheckBill.m_objGetRow(this.m_objViewer.m_DglCheckBill.CurrentCell.RowNumber);
//			if(this.m_objViewer.m_tabAduit.SelectedIndex==0)
//			{
//				((TextObject)detail.ReportDefinition.ReportObjects["TextCheckBillID"]).Text = seleRow["STORAGECHECKID_CHR"].ToString();
//                ((TextObject)detail.ReportDefinition.ReportObjects["TextCheckDate"]).Text = seleRow["CHECK_DAT"].ToString();
//				((TextObject)detail.ReportDefinition.ReportObjects["TextCreator"]).Text = seleRow["CREATORID_CHR"].ToString();
//			}
//			else
//			{
//				((TextObject)detail.ReportDefinition.ReportObjects["TextCheckBillID"]).Text = seleRow["STORAGECHECKID_CHR"].ToString();
//                ((TextObject)detail.ReportDefinition.ReportObjects["TextCheckDate"]).Text = seleRow["CHECK_DAT"].ToString();
//				((TextObject)detail.ReportDefinition.ReportObjects["TextCreator"]).Text = seleRow["CREATORID_CHR"].ToString();
				
//			}
//			detail.SetDataSource(dtView);
//			if(blisShow==true)
//			{
//				CrystalDecisions.Windows.Forms.CrystalReportViewer viewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
//				detail.Refresh();
//				viewer.ReportSource = detail;
//				return viewer;
//			}
//			else
//			{
//				detail.PrintToPrinter(1,true,0,1);
//				return null;
//			}
			
//		}
		
		#endregion 单据打印
	}
}
