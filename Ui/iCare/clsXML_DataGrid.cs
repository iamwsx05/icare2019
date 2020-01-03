using System;
using com.digitalwave.Utility .Controls ;
using System.Data;
using System.Xml;

namespace iCare
{
	/// <summary>
	/// Ԫ�����Ե���
	/// </summary>
	public class clsElementAttribute
	{
		/// <summary>
		/// Ԫ������
		/// </summary>
		public string m_strElementName;

		/// <summary>
		/// Ԫ��ֵ
		/// </summary>
		public string m_strValue;

		/// <summary>
		/// �Ƿ�ΪctlRichTextBox���͵�ֵ
		/// </summary>
		public bool m_blnIsDST;

		/// <summary>
		/// ��m_blnIsDST==false,�������Ϊ��(null)
		/// </summary>
		public string m_strValueXML;
	}


	/// <summary>
	/// DataGrid��ӦXML��ϵ���࣬Jacky-2003-3-3
	/// </summary>
	public class clsXML_DataGrid
	{
		public clsXML_DataGrid()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region ʵ�ֺ���
		/// <summary>
		/// ���Ԫ������
		/// </summary>
		/// <param name="p_objclsElementAttribute"></param>
		/// <param name="p_xmlWriter"></param>
		private void m_mthAddElementAttibute(clsElementAttribute p_objclsElementAttribute,ref System.Xml.XmlTextWriter p_xmlWriter)
		{
			p_xmlWriter.WriteStartElement(p_objclsElementAttribute.m_strElementName);
			p_xmlWriter.WriteAttributeString("VALUE",p_objclsElementAttribute.m_strValue);
			if(p_objclsElementAttribute.m_blnIsDST==true)
				p_xmlWriter.WriteAttributeString("VALUEXML",p_objclsElementAttribute.m_strValueXML);
			p_xmlWriter.WriteEndElement();
		}

		/// <summary>
		/// �жϵ�ǰ��¼�Ƿ��޸�
		/// </summary>
		/// <param name="p_objclsElementAttributeArr">p_dtbTable���� ==p_objclsElementAttributeArr�ĳ��ȣ�GroupBox�и���+ǩ��+ʱ�䣬ÿ���Ӧһ��clsElementAttribute����</param>
		/// <param name="p_dtbTable">��¼��Ӧ��DataTable</param>
		/// <returns>���޸��˷���true (�������ֵΪfalse,���û��ɸ��ݿ���Ҫ��ȡԭ�����ݿ��е�XML,�򲻸��¸��ֶμ���)</returns>
		public bool m_blnIsModified(clsElementAttribute[] p_objclsElementAttributeArr,System.Data.DataTable p_dtbTable)
		{
			bool blnIsModified=false;
			for(int j0=0;j0<p_objclsElementAttributeArr.Length-2;j0++)//�Ƚϴ����¼�Ƿ��޸ģ���ǩ����ʱ���⣩
			{	
				if(p_dtbTable.Rows.Count==0)
				{
					blnIsModified=true;	break;
				}
				if(p_objclsElementAttributeArr[j0].m_blnIsDST ==false)
				{
					if(p_objclsElementAttributeArr[j0].m_strValue !=p_dtbTable.Rows[0][j0].ToString())
					{
						blnIsModified=true;	break;
					}
				}
				else 
				{
					if(p_objclsElementAttributeArr[j0].m_strValue !=((clsDSTRichTextBoxValue)(p_dtbTable.Rows[0][j0])).m_strText ||
					
						p_objclsElementAttributeArr[j0].m_strValueXML !=((clsDSTRichTextBoxValue)(p_dtbTable.Rows[0][j0])).m_strDSTXml)
					{
						blnIsModified=true;	break;
					}
				}
			}
			return blnIsModified;
		}

		/// <summary>
		/// ��DataTable�Ͷ�Ӧ��GroupBox�е����ݵõ�XML��
		/// </summary>
		/// <param name="p_objclsElementAttributeArr">p_dtbTable���� ==p_objclsElementAttributeArr�ĳ��ȣ�GroupBox�и���+ǩ��+ʱ�䣬ÿ���Ӧһ��clsElementAttribute����</param>
		/// <param name="p_dtbTable">��¼��Ӧ��DataTable</param>
		/// <param name="p_objXmlMemStream"></param>
		/// <param name="p_xmlWriter"></param>
		/// <returns>���������󷵻�null</returns>
		public string m_strGetXMLFromDataTable(clsElementAttribute[] p_objclsElementAttributeArr,System.Data.DataTable p_dtbTable,ref System.IO.MemoryStream p_objXmlMemStream,ref System.Xml.XmlTextWriter p_xmlWriter)
		{
			if( p_objclsElementAttributeArr==null || p_dtbTable.Columns.Count !=p_objclsElementAttributeArr.Length )
				return null;

			p_objXmlMemStream.SetLength(0);			
			p_xmlWriter.WriteStartDocument();
			p_xmlWriter.WriteStartElement("Main");
			
			clsElementAttribute objclsElementAttribute=new clsElementAttribute();			
			
			bool blnIsModified=m_blnIsModified(p_objclsElementAttributeArr,p_dtbTable);			
			if(blnIsModified==true)//������޸ģ���ӵ�ǰֵ
			{
				p_xmlWriter.WriteStartElement("Sub");
				for(int j0=0;j0<p_objclsElementAttributeArr.Length;j0++)
				{			
					m_mthAddElementAttibute(p_objclsElementAttributeArr[j0],ref p_xmlWriter);				
				}
				p_xmlWriter.WriteEndElement();
			}			
			for(int i1=0;i1<p_dtbTable.Rows.Count;i1++)//��дԭ���ĺۼ�
			{
				p_xmlWriter.WriteStartElement("Sub");
				for(int j2=0;j2<p_objclsElementAttributeArr.Length;j2++)
				{					
					objclsElementAttribute.m_blnIsDST=p_objclsElementAttributeArr[j2].m_blnIsDST;//Ĭ��Ϊ��DST��ʽ����Ϊbool����
					objclsElementAttribute.m_strElementName=p_objclsElementAttributeArr[j2].m_strElementName;	
					if(objclsElementAttribute.m_blnIsDST==false)
						objclsElementAttribute.m_strValue=p_dtbTable.Rows[i1][j2].ToString();						
					else 	
					{
						objclsElementAttribute.m_strValue=((clsDSTRichTextBoxValue)(p_dtbTable.Rows[i1][j2])).m_strText;
						objclsElementAttribute.m_strValueXML=((clsDSTRichTextBoxValue)(p_dtbTable.Rows[i1][j2])).m_strDSTXml;
					}					
					
					m_mthAddElementAttibute(objclsElementAttribute,ref p_xmlWriter);
				}
				p_xmlWriter.WriteEndElement();
			}
			
			p_xmlWriter.WriteEndElement();
			p_xmlWriter.WriteEndDocument();
			p_xmlWriter.Flush();
			return System.Text.Encoding.Unicode.GetString(p_objXmlMemStream.ToArray(),39*2,(int)p_objXmlMemStream.Length-39*2);
			
		}	
		
		/// <summary>
		/// ����XML��ʽ����DataGrid�е�����
		/// </summary>
		/// <param name="strXML">��������DataGrid�����Ӧ��XML�ֶ�����</param>
		/// <param name="p_objclsElementAttributeArr">ֻ��Ҫ��ֵ����ÿ���m_blnIsDST��m_strElementName����</param>
		/// <param name="p_dtgGrid">���������ݵ�DataGrid����</param>
		/// <returns>��ȷִ�з���true,�������󷵻�false</returns>
		public bool m_blnSetDataFromXML( string strXML,bool[] p_blnIsDSTArr,System.Xml.XmlParserContext p_objXmlParser,ref System.Windows.Forms.DataGrid p_dtgGrid)
		{		
			if( strXML ==null || strXML.Trim() ==""|| p_blnIsDSTArr==null )
				return false;

			DataTable dtbTable=(DataTable)p_dtgGrid.DataSource;
			p_dtgGrid.CurrentRowIndex=0;
			dtbTable.Rows.Clear();

			XmlTextReader objReader = new XmlTextReader(strXML,XmlNodeType.Element,p_objXmlParser);
			objReader.WhitespaceHandling = WhitespaceHandling.None;			
			
			clsElementAttribute[] objclsElementAttributeArr=new clsElementAttribute[p_blnIsDSTArr.Length];
			for(int j0=0;j0<p_blnIsDSTArr.Length;j0++)
			{
				objclsElementAttributeArr[j0]=new clsElementAttribute();
				objclsElementAttributeArr[j0].m_blnIsDST=p_blnIsDSTArr[j0];//Ĭ��Ϊ��DST��ʽ����Ϊbool����					
			}

			int j2=0;
			while(objReader.Read())
			{				
				switch(objReader.NodeType)
				{
					case XmlNodeType.Element://
						if(objReader.HasAttributes)
						{	
							objclsElementAttributeArr[j2].m_strValue= objReader.GetAttribute("VALUE");
							objclsElementAttributeArr[j2].m_strValueXML= objReader.GetAttribute("VALUEXML");	
						
							if(j2==objclsElementAttributeArr.Length-1)
							{
								Object[] objRes=new object[objclsElementAttributeArr.Length];
								for(int k3=0;k3<objclsElementAttributeArr.Length;k3++)
								{
								
									if(objclsElementAttributeArr[k3].m_blnIsDST==false)
										objRes[k3]=objclsElementAttributeArr[k3].m_strValue=="True" ? true:false;
									
									else 
									{
										clsDSTRichTextBoxValue objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
										objclsDSTRichTextBoxValue.m_strText=objclsElementAttributeArr[k3].m_strValue;
										objclsDSTRichTextBoxValue.m_strDSTXml=objclsElementAttributeArr[k3].m_strValueXML;
										objRes[k3]=objclsDSTRichTextBoxValue;
									}
								}
								dtbTable.Rows.Add(objRes);
								j2=0;

							}
							else
								j2++;
						}	
						break;
				}
			}
			return true;			
		}
		#endregion

		public string m_strReplaceWhiteToBlack(string p_strXML)
		{//-1:��ɫ��-16777216��ɫ
			if(p_strXML==null)
				return null;
			return p_strXML.Replace("C=\"-1\"","C=\"-16777216\"");
		}
		public string m_strReplaceBlackToWhite(string p_strXML)
		{//-1:��ɫ��-16777216��ɫ
			if(p_strXML==null)
				return null;
			return p_strXML.Replace("C=\"-16777216\"","C=\"-1\"");
		}
	}
}
