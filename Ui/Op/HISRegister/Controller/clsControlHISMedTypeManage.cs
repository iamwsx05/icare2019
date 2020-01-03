using System;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	#region ҩ�����ά������
	/// <summary>
	/// ҩ�����ά������
	/// Create ��ΰ�� by 2005-09-8
	/// </summary>
	public class clsControlHISMedTypeManage : com.digitalwave.GUI_Base.clsController_Base	//GUI_Base.dll
	{
		#region ���캯��
		public clsControlHISMedTypeManage()
		{
			m_objManage = new clsDomainControlHISMedTypeManage();
			//m_objClsPublicParm = new clsPublicParm();
		}
		#endregion

		#region ����
		/// <summary>
		/// DomainControl����
		/// </summary>
		private clsDomainControlHISMedTypeManage m_objManage = null;
		
		/// <summary>
		/// frm�������
		/// </summary>
		private com.digitalwave.iCare.gui.HIS.frmHISMedTypeManage m_objViewer ;

		#endregion

		#region ���ô������override Set_GUI_Apperance ʵ��
		
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  ��� Set_GUI_Apperance ʵ��
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmHISMedTypeManage)frmMDI_Child_Base_in;
		}
		#endregion

		#region ���������
		/// <summary>
		/// ���������
		/// </summary>
		public void m_mthGetMainItem()
		{		
			clsHISMedType_VO[] objResultArr;
			long ret = m_objManage.m_lngGetMedTypeInfo(out objResultArr,"");
			
			if(objResultArr!=null)
			{
				DataTable dtb ;
				this.m_mthCreateDataTable(out dtb,objResultArr);
				this.InitTree(this.m_objViewer.m_trvMedType.Nodes, dtb,"");				
			}
		}
		#endregion

		#region ���ȹ���һ��TABLE
		/// <summary>
		/// ���ȹ���һ��TABLE,���Եڶ�������дֵ,����һ��TABLE(��һ������
		/// </summary>
		void m_mthCreateDataTable(out System.Data.DataTable p_dtb, clsHISMedType_VO[] p_objMedTypeVoArr)
		{
			p_dtb = new DataTable("tree");
			//��ʼ����
			System.Data.DataColumn dcol = new DataColumn("PHARMAID_CHR");
			p_dtb.Columns.Add(dcol);
			dcol = new DataColumn("PHARMANAME_VCHR");
			p_dtb.Columns.Add(dcol);
			dcol = new DataColumn("ASSISTCODE_VCHR");
			p_dtb.Columns.Add(dcol);
			dcol = new DataColumn("PYCODE_VCHR");
			p_dtb.Columns.Add(dcol);
			dcol = new DataColumn("WBCODE_VCHR");
			p_dtb.Columns.Add(dcol);
			dcol = new DataColumn("PARENTID_CHR");
			p_dtb.Columns.Add(dcol);
			//�������
			
			//��ʼ��д����
			DataRow dr;
			for(int i1=0;i1<p_objMedTypeVoArr.Length;i1++)
			{	
				dr = p_dtb.NewRow();
				dr["PHARMAID_CHR"] = p_objMedTypeVoArr[i1].m_strPHARMAID_CHR;
				dr["PHARMANAME_VCHR"] = p_objMedTypeVoArr[i1].m_strPHARMANAME_VCHR;
				dr["ASSISTCODE_VCHR"] = p_objMedTypeVoArr[i1].m_strASSISTCODE_VCHR;
				dr["PYCODE_VCHR"] = p_objMedTypeVoArr[i1].m_strPYCODE_VCHR;
				dr["WBCODE_VCHR"] = p_objMedTypeVoArr[i1].m_strWBCODE_VCHR;
				if(p_objMedTypeVoArr[i1].m_strPARENTID_CHR == null)
					dr["PARENTID_CHR"] = "";
				else
					dr["PARENTID_CHR"] =p_objMedTypeVoArr[i1].m_strPARENTID_CHR;
				

				p_dtb.Rows.Add(dr);

			}
			//��д���
            
		}
		#endregion

				
		#region ��ʼ��һ�����Ľ�㣺�ݹ鷽��
		/// <summary>
		/// ��ʼ��һ�����Ľ�㣺�ݹ鷽��
		/// </summary>
		private void InitTree(TreeNodeCollection Nds,DataTable p_dtb,string parentId)
		{
			DataView dv=new DataView();
			TreeNode tmpNd;
			
			dv.Table=p_dtb;
			dv.RowFilter="PARENTID_CHR='"+ parentId+"'";
			foreach(DataRowView drv in dv)
			{
				tmpNd=new TreeNode();
				string ID = drv["PHARMAID_CHR"].ToString();
				tmpNd.Text=drv["PHARMANAME_VCHR"].ToString();

				//one Rowת��Ϊһ��VO�������tag
				clsHISMedType_VO objMedTypeVo = new clsHISMedType_VO();
				objMedTypeVo.m_strPHARMAID_CHR = drv["PHARMAID_CHR"].ToString();
				objMedTypeVo.m_strPHARMANAME_VCHR = drv["PHARMANAME_VCHR"].ToString();
				objMedTypeVo.m_strASSISTCODE_VCHR = drv["ASSISTCODE_VCHR"].ToString();
				objMedTypeVo.m_strPYCODE_VCHR = drv["PYCODE_VCHR"].ToString();
				objMedTypeVo.m_strWBCODE_VCHR = drv["WBCODE_VCHR"].ToString();
				objMedTypeVo.m_strPARENTID_CHR = drv["PARENTID_CHR"].ToString();

				tmpNd.Tag = objMedTypeVo;
				//�������
								
				Nds.Add(tmpNd);
			
				InitTree(tmpNd.Nodes,p_dtb,ID);	

			}
		}
		#endregion

		#region ���ؽ��
		/// <summary>
		/// ���ؽ��:�ݹ麯������!�޼�������.
		/// </summary>
		public void BuildTree(string ParentID,TreeNode td)
		{			
			clsHISMedType_VO[] objResultArr;
			long ret = m_objManage.m_lngGetMedTypeInfo(out objResultArr,ParentID);
			
			if(objResultArr!=null)
			{
				TreeNode tr;
				for(int i=0;i<objResultArr.Length;i++)
				{	
					tr =new TreeNode(objResultArr[i].m_strPHARMANAME_VCHR);
					tr.Tag =objResultArr[i];

					td.Nodes.Add(tr);
					BuildTree(objResultArr[i].m_strPHARMAID_CHR,tr);
				}
			} 
		}
		#endregion

		#region ��ʼ������:���Ȱѽ�����ڷ�������
		/// <summary>
		/// ��ʼ������
		/// </summary>
		public void m_mthSetFirstFocus()
		{		
			this.m_objViewer.m_txtMedName.TabIndex = 0;
			this.m_objViewer.m_txtMedName.Focus();

		}
		#endregion

		#region ѡ��ڵ�
		/// <summary>
		/// ѡ��ڵ�
		/// </summary>
		public void m_mthSelectNode(TreeNode p_tr)
		{
			if(p_tr==null)
			{
				return;
			}
			if(p_tr.Tag==null)
			{
				return;
			}
			clsHISMedType_VO objTD_VO =p_tr.Tag as clsHISMedType_VO;
			this.m_objViewer.m_lbeType.Text =p_tr.FullPath;
			m_mthFillTextBoxByVO(objTD_VO);
			this.m_objViewer.m_lbeType.Tag=p_tr;
		

		}
		private void m_mthFillTextBoxByVO(clsHISMedType_VO objTD_VO )
		{
			this.m_objViewer.m_txtMedName.Text =objTD_VO.m_strPHARMANAME_VCHR;
			this.m_objViewer.m_txtMedZhuJi.Text=objTD_VO.m_strASSISTCODE_VCHR;
			this.m_objViewer.m_txtMedPy.Text =objTD_VO.m_strPYCODE_VCHR;
			this.m_objViewer.m_txtMedWb.Text=objTD_VO.m_strWBCODE_VCHR;

		}
		#endregion

		#region �������ڵ�
		/// <summary>
		/// �������ڵ�:���txtbox
		/// </summary>
		public void m_mthAddMain()
		{
			this.m_objViewer.m_lbeType.Text ="";
			this.m_objViewer.m_txtMedName.Text ="";
			this.m_objViewer.m_txtMedPy.Text="";
			this.m_objViewer.m_txtMedWb.Text ="";
			this.m_objViewer.m_txtMedZhuJi.Text="";
			this.m_objViewer.m_txtMedName.Focus();
		}

		#endregion

		#region �����ӽڵ�
		/// <summary>
		/// �����ӽڵ�:���txtbox
		/// </summary>
		public void m_mthAddSub()
		{
			//TreeNode trn = this.m_objViewer.m_lbeType.Tag as TreeNode;
			
			if(this.m_objViewer.m_trvMedType.SelectedNode == null)
			{
				MessageBox.Show("��ѡ��ڵ�");
				return;
			}

			this.m_objViewer.m_lbeType.Text ="";
			this.m_objViewer.m_txtMedName.Text ="";
			this.m_objViewer.m_txtMedPy.Text="";
			this.m_objViewer.m_txtMedWb.Text ="";
			this.m_objViewer.m_txtMedZhuJi.Text="";
			this.m_objViewer.m_txtMedName.Focus();

			TreeNode objTreeNode =this.m_objViewer.m_lbeType.Tag as TreeNode;
			this.m_objViewer.m_lbeType .Text = objTreeNode.FullPath.ToString();
			
		}

		#endregion

		#region ����
		/// <summary>
		/// ������������
		/// </summary>
		/// <param name="p_strUserOp">�û�������־����������㣬�ӽ�㣬�޸�</param>
		
		public void m_mthSave(string p_strUserOp)
		{
			if(this.m_objViewer.m_txtMedName.Text.Trim()=="")
			{
				MessageBox.Show("���Ʋ���Ϊ��");
				return;
			}
			if(!m_mthCheckMaxDataLength())
				return;
			clsHISMedType_VO objTD_VO;
			m_mthGetData(out objTD_VO, p_strUserOp);
			long ret =0;
			if(p_strUserOp == "AddTop" || p_strUserOp =="AddSub")
			{	
				// ����������Ψһ��
				if(m_mthCheck())
					return ;
				//������

				//�����ӷ���	
				clsHISMedType_VO objTDReturn_VO;
				ret =m_objManage.m_lngAddNew(objTD_VO,out objTDReturn_VO);
				if(ret>0)
				{
					MessageBox.Show("����ɹ���");
					TreeNode tr =new TreeNode(objTDReturn_VO.m_strPHARMANAME_VCHR);
					tr.Tag =objTDReturn_VO;

					if(p_strUserOp == "AddTop")
					{
						this.m_objViewer.m_trvMedType.Nodes.Add(tr);
					}
					else
					{							
						((TreeNode)this.m_objViewer.m_lbeType.Tag).Nodes.Add(tr);						
					}
					this.m_objViewer.m_lbeType.Tag =tr;
					this.m_objViewer.m_trvMedType.SelectedNode =tr;
				}
			}
			else if (p_strUserOp == "Save")
			{
				//����������Ψһ�ԣ�
				TreeNode nodeSave = this.m_objViewer.m_lbeType.Tag as TreeNode;
				clsHISMedType_VO objMedSave_VO =nodeSave.Tag as clsHISMedType_VO;
				if(objMedSave_VO.m_strASSISTCODE_VCHR != objTD_VO.m_strASSISTCODE_VCHR)
				{					
					if(m_mthCheck())
					{
						this.m_objViewer.m_strUserOp = "Save";
						return ;
					}
					//������
				}
				//������

				//�����޸ķ���
				ret = this.m_objManage.m_lngModify(objTD_VO);
				if(ret>0)
				{
					MessageBox.Show("����ɹ���");
					TreeNode tr =this.m_objViewer.m_lbeType.Tag as TreeNode;
					tr.Text =objTD_VO.m_strPHARMANAME_VCHR;
					tr.Tag =objTD_VO;
				}
			}
			if(ret<1)
			{
				MessageBox.Show("����ʧ�ܣ�");
			}
		}
		#endregion

		#region �޸Ľ��
		/// <summary>
		/// �޸Ľ��
		/// </summary>		
		public void m_mthAlter()
		{

			TreeNode nodeSave = this.m_objViewer.m_lbeType.Tag as TreeNode;
			if(nodeSave != null)
			{	
				if(this.m_objViewer.m_txtMedName.Text.Trim()=="")
				{
					MessageBox.Show("���Ʋ���Ϊ��");
					return;
				}
				clsHISMedType_VO objTD_VO;
				m_mthGetData(out objTD_VO, "Save"); //ȡ��Ҫ�޸ĵ�����
				long ret =0;
			
				//����������Ψһ�ԣ�
			
				clsHISMedType_VO objMedSave_VO =nodeSave.Tag as clsHISMedType_VO;
				if(objMedSave_VO.m_strASSISTCODE_VCHR != objTD_VO.m_strASSISTCODE_VCHR)
				{					
					if(m_mthCheck())
					{						
						return ;
					}				
				}
				//������

				//�����޸ķ���
				ret = this.m_objManage.m_lngModify(objTD_VO);
				if(ret>0)
				{
					MessageBox.Show("�޸ĳɹ���");
					TreeNode tr =this.m_objViewer.m_lbeType.Tag as TreeNode;
					tr.Text =objTD_VO.m_strPHARMANAME_VCHR;
					tr.Tag =objTD_VO;
				}			
				if(ret<1)
				{
					MessageBox.Show("�޸�ʧ�ܣ�");
				}
			}
			else
			{
				MessageBox.Show("����ѡ��ĳ���࣡");
			}
		}
		#endregion
		#region ȡ�����ݷ�װ��Ҫ�浽���ݿ��VO����
		/// <summary>
		/// ȡ�����ݷ�װ��Ҫ�浽���ݿ��VO����
		/// </summary>
		/// <param name="p_strUserOp">�û�������־����������㣬�ӽ�㣬�޸�</param>
		/// <param name="objTD_VO">VO</param>
		/// 
		public void m_mthGetData(out clsHISMedType_VO objTD_VO, string p_strUserOp)
		{
			objTD_VO =new clsHISMedType_VO();
			
			switch(p_strUserOp)
			{				
				case "AddTop":

					break;
				case "AddSub":		
					TreeNode node = this.m_objViewer.m_lbeType.Tag as TreeNode;
					clsHISMedType_VO objMed_VO =node.Tag as clsHISMedType_VO;
					objTD_VO.m_strPARENTID_CHR = objMed_VO.m_strPHARMAID_CHR;//ȡ�ø�����ID
					break;
			
				case "Save":
					
					TreeNode nodeSave = this.m_objViewer.m_lbeType.Tag as TreeNode;
					clsHISMedType_VO objMedSave_VO =nodeSave.Tag as clsHISMedType_VO;
					objTD_VO.m_strPARENTID_CHR = objMedSave_VO.m_strPARENTID_CHR;//ȡ�ø�����ID
					objTD_VO.m_strPHARMAID_CHR = objMedSave_VO.m_strPHARMAID_CHR;//ȡ��Ҫ�޸ĵĽ���ID
					
					break;
				default:
					break;
			}

			objTD_VO.m_strPHARMANAME_VCHR = this.m_objViewer.m_txtMedName.Text.Trim();
			objTD_VO.m_strPYCODE_VCHR = this.m_objViewer.m_txtMedPy.Text.Trim();
			objTD_VO.m_strWBCODE_VCHR = this.m_objViewer.m_txtMedWb.Text.Trim();
			objTD_VO.m_strASSISTCODE_VCHR = this.m_objViewer.m_txtMedZhuJi.Text.Trim();
            	
		}
		#endregion

		#region ɾ���ڵ�
		/// <summary>
		/// ɾ���ڵ�
		/// </summary>
		public void m_mthDelete()
		{
			if(this.m_objViewer.m_lbeType.Tag==null)
			{
				return;
			}
			TreeNode tr =this.m_objViewer.m_lbeType.Tag as TreeNode;
			clsHISMedType_VO objTD_VO =tr.Tag as clsHISMedType_VO;
			DialogResult dr;
			
			bool blnHasSubNode = false;
			this.m_objManage.m_lngCheckMedTypeIsHasSubById(out blnHasSubNode,objTD_VO.m_strPHARMAID_CHR);
			if(blnHasSubNode)
			{
				MessageBox.Show("�˽ڵ�����ӽ�㣬��ֹɾ����Ӧ��ɾ�����ӽ�㣡","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return ;
				//dr =MessageBox.Show("��ǰ�ڵ������ڵ㣬ɾ���˽ڵ㽫ɾ���˽ڵ�������ӽڵ㣬\n�Ƿ�ɾ����","��ʾ",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
			}
			else
			{
				dr =MessageBox.Show("�Ƿ�ɾ���˽ڵ㣿","��ʾ",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
			}
			if(dr==DialogResult.OK)
			{
				long ret = -1;
				if(blnHasSubNode)//objTD_VO.m_strPARENTID_CHR == "")
				{
					//�ڴ�ɾ����������¼����
					m_mthBianLi(tr, out ret);
				  
				}
				else
				{
					ret =this.m_objManage.m_lngDelete(objTD_VO);//�ڴ�ɾ�������
				}
				
				if(ret>0)
				{
					MessageBox.Show("ɾ���ɹ�");
					
					this.m_objViewer.m_trvMedType.Nodes.Remove(tr);
				}
				else
				{
					MessageBox.Show("ɾ��ʧ��");
				}
			}
		}
		#endregion

		#region ����ĳһ��㣬�������ӽ�㣬Ŀ����ȡ��Ҫɾ�����ӽ��ID�ţ����ڴ����ݿ���ɾ��
		/// <summary>
		/// <param name="p_trn">����ĳһ��㣬�������ӽ��</param> 
		/// <param name="p_lngResult">�����Ľ��</param> 
		/// </summary>
		void m_mthBianLi(TreeNode p_trn,out long p_lngResult)
		{		
			p_lngResult = 1;
			foreach (TreeNode node in p_trn.Nodes)
			{
				clsHISMedType_VO objTD_VO =p_trn.Tag as clsHISMedType_VO;
				p_lngResult =this.m_objManage.m_lngDelete(objTD_VO);//�����ݿ���ɾ���ӽ��
				m_mthBianLi(node, out p_lngResult);
			}			
		}
		#endregion

		#region �޶��û��������ݵĳ���
		bool m_mthCheckMaxDataLength()
		{
			string strError = string.Empty;
			if(this.m_objViewer.m_txtMedName.Text.Length>50)
				strError = "����ҩ�����Ƴ��Ȳ��ܴ���50";
			if(this.m_objViewer.m_txtMedZhuJi.Text.Length>20)
				strError = "�����������Ƴ��Ȳ��ܴ���20";
			if(this.m_objViewer.m_txtMedPy.Text.Length>20)
				strError = "����ƴ�����Ȳ��ܴ���20";
			if(this.m_objViewer.m_txtMedWb.Text.Length>20)
				strError = "����������Ƴ��Ȳ��ܴ���20";
			if(strError != string.Empty)
			{	
				MessageBox.Show(strError);
				return false;
			}
			return true;
		}
		#endregion

		#region �ж��������Ƿ������ظ�
		/// <summary>
		/// �ж��������Ƿ������ظ�
		/// </summary>
		
		/// <param name="p_strZhuJiMa">������</param>		
		/// <returns blnHasThisZhujima>�ɹ���true :֤����������ݱ�֤����е���ͬ��</returns>
		/// 
		public bool m_blnHasThisZhujima(string p_strZhuJiMa)
		{
			bool blnHasThisZhujima = false;
			long lngRet = 0 ;
			
			lngRet = this.m_objManage.m_lngGetMedTypeZhuJiMaById(out blnHasThisZhujima, p_strZhuJiMa);
			if(lngRet>0)
			{
				return 	blnHasThisZhujima;
			}
			else
			{
				MessageBox.Show("����ʧ��");
				return false;
			}
			
		}

		#endregion

		#region  ����������Ψһ��
		/// <summary>
		/// ��������֤���Ѵ��ڡ�
		/// </summary>
		/// <returns></returns>
		bool m_mthCheck()
		{
			if(this.m_objViewer.m_txtMedZhuJi.Text.Trim()!=string.Empty)
			{
				bool blnret = m_blnHasThisZhujima(this.m_objViewer.m_txtMedZhuJi.Text.Trim());				
				if(blnret)
				{
					MessageBox.Show("�������Ѵ��ڣ���������");
					
					this.m_objViewer.m_txtMedZhuJi.Focus();
					this.m_objViewer.m_txtMedZhuJi.Clear();
					return true;
				}
				return false;
			}
			return false;
		}
		#endregion

	}
	#endregion
}
