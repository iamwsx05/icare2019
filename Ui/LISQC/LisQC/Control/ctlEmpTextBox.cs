using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using PinkieControls;
using System.Drawing;
using ZedGraph;
using System.Xml;
using System.IO;
using ExpressionEvaluation;
using com.digitalwave.Utility;

namespace com.digitalwave.iCare.gui.LIS.QC.Control
{
    public class ctlEmpTextBox : ctlExtTextBox
    {
        // Fields
        private int m_intShowFlag;
        private string m_strDeptID;

        // Methods
        public ctlEmpTextBox()
        {
            this.m_strDeptID = "000";
            this.m_intShowFlag = 0;
            this.m_mthSetListAppearance();
        }

        public bool m_blnQuery(string p_strInput)
        {
            this.m_mthClear();
            this.m_lsvList.Items.Clear();
            this.m_mthLoadListData(p_strInput);
            if (this.m_lsvList.Items.Count <= 0)
            {
                this.m_mthClear();
                return false;
            }
            else
            {
                this.m_ObjValueObject = this.m_lsvList.Items[0].Tag;
                this.Text = this.m_strGetItemName();
                return true;
            }
        }

        private void m_mthGetEmpInfoByName(string strName)
        {
            if (strName == null)
            {
                this.m_ObjValueObject = null;
                this.Text = null;
                return;
            }

            clsEmployeeVO evo = null;
            (new weCare.Proxy.ProxyBase()).Service.m_lngGetEmpInfoByName(strName.Trim(), out evo);
            if (evo != null)
            {
                this.m_ObjValueObject = evo;
                this.Text = evo.strLastName;
            }
            else
            {
                this.m_ObjValueObject = null;
                this.Text = null;
            }
        }

        protected override void m_mthLoadListData(string p_strInput)
        {
            int num = 0;
            try
            {
                int.Parse(p_strInput);
            }
            catch
            {
                num = 2;
            }

            clsEmployeeVO[] evoArray = null;
            ListViewItem item;
            p_strInput = (p_strInput + "%").ToUpper();

            (new weCare.Proxy.ProxyBase()).Service.m_lngGetEmployeeArrByInput(num, p_strInput, this.m_strDeptID, out evoArray);
            if (evoArray != null && evoArray.Length > 0)
            {
                for (int i = 0; i < evoArray.Length; i++)
                {
                    item = new ListViewItem(evoArray[i].strLastName);
                    item.SubItems.Add(evoArray[i].strEmpNO);
                    item.Tag = evoArray[i];
                    if (evoArray[i].intStatus == 1)
                    {
                        this.m_lsvList.Items.Add(item);
                    }
                    item.BackColor = Color.Moccasin;
                }
            }
        }

        protected override void m_mthSetListAppearance()
        {
            this.m_lsvList.Columns[0].Width = 50;
            this.m_lsvList.Columns.Add("", this.Width, 0);
        }

        protected override string m_strGetItemName()
        {
            if (this.m_ObjValueObject != null)
                return ((clsEmployeeVO)this.m_ObjValueObject).strLastName;
            else
                return string.Empty;
        }

        protected override object m_strGetValueObject()
        {
            if (this.m_lsvList.SelectedItems.Count > 0 && this.m_lsvList.SelectedItems[0].Tag != null)
                return this.m_lsvList.SelectedItems[0].Tag;
            else
                return null;
        }

        // Properties
        public int m_intShowOtherEmp
        {
            get
            {
                return this.m_intShowFlag;
            }
            set
            {
                this.m_intShowFlag = value;
            }
        }

        public clsEmployeeVO m_ObjEmployee
        {
            get
            {
                if (this.m_ObjValueObject == null)
                    return null;
                else
                    return (clsEmployeeVO)this.m_ObjValueObject;
            }
        }

        public string m_StrDeptID
        {
            get
            {
                return this.m_strDeptID;
            }
            set
            {
                this.m_strDeptID = value;
            }
        }

        public string m_StrEmployeeID
        {
            get
            {
                if (this.m_ObjValueObject == null)
                    return string.Empty;
                else
                    return ((clsEmployeeVO)this.m_ObjValueObject).strEmpID;
            }
            set
            {
                if (value == null)
                {
                    this.m_ObjValueObject = null;
                    this.Text = null;
                }
                else
                {
                    clsEmployeeVO evo = null;
                    (new weCare.Proxy.ProxyBase()).Service.m_lngGetEmpInfoByID(value.ToString().Trim(), out evo);
                    if (evo != null)
                    {
                        this.m_ObjValueObject = evo;
                        this.Text = evo.strLastName;
                    }
                    else
                    {
                        this.m_ObjValueObject = null;
                        this.Text = null;
                    }
                }
            }
        }

        public string m_StrEmployeeName
        {
            get
            {
                return this.m_strGetItemName();
            }
            set
            {
                this.m_mthGetEmpInfoByName(value);
            }
        }

        // Nested Types
        public enum enmShowOtherEmp
        {
            Dept,
            All
        }
    }
}
