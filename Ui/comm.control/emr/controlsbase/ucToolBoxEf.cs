using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;

namespace Common.Controls.Emr
{
    public delegate void HandleEfCtrlMouseDoubleClick(object sender, EntityFormObject entityObj);
    public delegate void HandleEfCtrlMouseMove(object sender, EntityFormObject entityObj);

    public partial class ucToolBoxEf : UserControl
    {
        public ucToolBoxEf()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        public HandleEfCtrlMouseDoubleClick CtrlMouseDoubleClick;

        public HandleEfCtrlMouseMove CtrlMouseMove;

        private void ucItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    if (sender is ucToolBoxItem)
                    {
                        CtrlMouseMove(sender, GetEfObj(((ucToolBoxItem)sender).EnumEfObj));
                    }
                    else
                    {
                        CtrlMouseMove(sender, GetEfObj(((ucToolBoxItem)(sender as Control).Parent).EnumEfObj));
                    }
                }
            }
        }

        private void ucItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                if (sender is ucToolBoxItem)
                {
                    CtrlMouseDoubleClick(sender, GetEfObj(((ucToolBoxItem)sender).EnumEfObj));
                }
                else
                {
                    CtrlMouseDoubleClick(sender, GetEfObj(((ucToolBoxItem)(sender as Control).Parent).EnumEfObj));
                }
            }
        }

        private void ucToolBoxEf_Load(object sender, EventArgs e)
        {
            #region 加载控件
            Color InitColor = panelControl1.BackColor;
            ucToolBoxItem boxItem = null;
            for (int i = 0; i < 57; i++)
            {
                boxItem = new ucToolBoxItem();
                switch (i)
                {
                    case 0:
                        boxItem.CaptionImage = Properties.Resources.指针;
                        boxItem.CaptionDesc = "指针";
                        break;
                    case 1:
                        boxItem.CaptionImage = Properties.Resources.水平线;
                        boxItem.EnumEfObj = enumFormObject.ctrlHLine;
                        boxItem.CaptionDesc = "水平线";
                        break;
                    case 2:
                        boxItem.CaptionImage = Properties.Resources.垂直线;
                        boxItem.EnumEfObj = enumFormObject.ctrlVLine;
                        boxItem.CaptionDesc = "垂直线";
                        break;
                    case 3:
                        boxItem.CaptionImage = Properties.Resources.标签;
                        boxItem.EnumEfObj = enumFormObject.ctrlLable;
                        boxItem.CaptionDesc = "标签栏";
                        break;
                    case 4:
                        boxItem.CaptionImage = Properties.Resources.勾选框;
                        boxItem.EnumEfObj = enumFormObject.ctrlCheckBox;
                        boxItem.CaptionDesc = "勾选框";
                        break;
                    case 5:
                        boxItem.CaptionImage = Properties.Resources.文本框;
                        boxItem.EnumEfObj = enumFormObject.ctrlTextBox;
                        boxItem.CaptionDesc = "文本框";
                        break;
                    case 6:
                        boxItem.CaptionImage = Properties.Resources.多行编辑器;
                        boxItem.CaptionDesc = "多行编辑框";
                        boxItem.EnumEfObj = enumFormObject.ctrlMemoEdit;
                        break;
                    case 7:
                        boxItem.CaptionImage = Properties.Resources.图片框;
                        boxItem.EnumEfObj = enumFormObject.ctrlPictureBox;
                        boxItem.CaptionDesc = "图片框";
                        break;
                    case 8:
                        boxItem.CaptionImage = Properties.Resources.住院天数;
                        boxItem.EnumEfObj = enumFormObject.ctrlDatetime;
                        boxItem.CaptionDesc = "日期/时间";
                        break;
                    case 9:
                        boxItem.CaptionImage = Properties.Resources.勾选;
                        boxItem.EnumEfObj = enumFormObject.ctrlCombox;
                        boxItem.CaptionDesc = "下拉列表";
                        break;
                    case 10:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.ctrlButton;
                        boxItem.CaptionDesc = "按钮";
                        break;
                    case 11:
                        boxItem.CaptionImage = Properties.Resources.出生日期;
                        boxItem.EnumEfObj = enumFormObject.ctrlSignature;
                        boxItem.CaptionDesc = "签名控件";
                        break;
                    case 12:
                        boxItem.CaptionImage = Properties.Resources.姓名;
                        boxItem.EnumEfObj = enumFormObject.ctrlDicEmployee;
                        boxItem.CaptionDesc = "职工控件";
                        break;
                    case 13:
                        boxItem.CaptionImage = Properties.Resources.科室;
                        boxItem.EnumEfObj = enumFormObject.ctrlDicDept;
                        boxItem.CaptionDesc = "科室控件";
                        break;
                    case 14:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.ctrlLayout;
                        boxItem.CaptionDesc = "布局控件";
                        break;
                    case 15:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.ctrlPanel;
                        boxItem.CaptionDesc = "Panel";
                        break;
                    case 16:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.ctrlTabpage;
                        boxItem.CaptionDesc = "分页控件";
                        break;
                    case 17:
                        boxItem.CaptionImage = Properties.Resources.Edit_16x16;
                        boxItem.EnumEfObj = enumFormObject.ctrlEmrEditor;
                        boxItem.CaptionDesc = "病历书写控件";
                        break;
                    case 18:
                        boxItem.CaptionImage = Properties.Resources.Grid_16x16;
                        boxItem.EnumEfObj = enumFormObject.ctrlEmrTable;
                        boxItem.CaptionDesc = "病历表格控件";
                        break;
                    case 19:
                        boxItem.CaptionImage = Properties.Resources.Pivot_16x16;
                        boxItem.EnumEfObj = enumFormObject.ctrlDicICD;//.ctrlDicCheckPart;
                        boxItem.CaptionDesc = "ICD";//"检查部位";
                        break;
                    case 20:
                        boxItem.CaptionImage = Properties.Resources.医院名称;
                        boxItem.EnumEfObj = enumFormObject.patHospitalName;
                        boxItem.CaptionDesc = "医院名称";
                        break;
                    case 21:
                        boxItem.CaptionImage = Properties.Resources.姓名;
                        boxItem.EnumEfObj = enumFormObject.patName;
                        boxItem.CaptionDesc = "姓名";
                        break;
                    case 22:
                        boxItem.CaptionImage = Properties.Resources.性别;
                        boxItem.EnumEfObj = enumFormObject.patSex;
                        boxItem.CaptionDesc = "性别";
                        break;
                    case 23:
                        boxItem.CaptionImage = Properties.Resources.年龄;
                        boxItem.EnumEfObj = enumFormObject.patAge;
                        boxItem.CaptionDesc = "年龄";
                        break;
                    case 24:
                        boxItem.CaptionImage = Properties.Resources.出生日期;
                        boxItem.EnumEfObj = enumFormObject.patBirthDay;
                        boxItem.CaptionDesc = "出生日期";
                        break;
                    case 25:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patIpNo;
                        boxItem.CaptionDesc = "住院号";
                        break;
                    case 26:
                        boxItem.CaptionImage = Properties.Resources.入院次数;
                        boxItem.EnumEfObj = enumFormObject.patIpTimes;
                        boxItem.CaptionDesc = "住院次数";
                        break;
                    case 27:
                        boxItem.CaptionImage = Properties.Resources.入院时间;
                        boxItem.EnumEfObj = enumFormObject.patInDate;
                        boxItem.CaptionDesc = "入院时间";
                        break;
                    case 28:
                        boxItem.CaptionImage = Properties.Resources.入院时间;
                        boxItem.EnumEfObj = enumFormObject.patOutDate;
                        boxItem.CaptionDesc = "出院时间";
                        break;
                    case 29:
                        boxItem.CaptionImage = Properties.Resources.住院天数;
                        boxItem.EnumEfObj = enumFormObject.patInDays;
                        boxItem.CaptionDesc = "住院天数";
                        break;
                    case 30:
                        boxItem.CaptionImage = Properties.Resources.科室;
                        boxItem.EnumEfObj = enumFormObject.patDept;
                        boxItem.CaptionDesc = "科室";
                        break;
                    case 31:
                        boxItem.CaptionImage = Properties.Resources.病区;
                        boxItem.EnumEfObj = enumFormObject.patArea;
                        boxItem.CaptionDesc = "病区";
                        break;
                    case 32:
                        boxItem.CaptionImage = Properties.Resources.床号;
                        boxItem.EnumEfObj = enumFormObject.patBedNo;
                        boxItem.CaptionDesc = "床号";
                        break;

                    case 33:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patIdNumber;
                        boxItem.CaptionDesc = "身份证号";
                        break;
                    case 34:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patMarriage;
                        boxItem.CaptionDesc = "婚姻状况";
                        break;
                    case 35:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patNation;
                        boxItem.CaptionDesc = "民族";
                        break;
                    case 36:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patNativePlace;
                        boxItem.CaptionDesc = "籍贯";
                        break;
                    case 37:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patBirthplace;
                        boxItem.CaptionDesc = "出生地";
                        break;
                    case 38:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patHkadr;
                        boxItem.CaptionDesc = "户口地址";
                        break;
                    case 39:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patHomeadr;
                        boxItem.CaptionDesc = "家庭地址";
                        break;
                    case 40:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patCulture;
                        boxItem.CaptionDesc = "文化程度";
                        break;
                    case 41:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patProfession;
                        boxItem.CaptionDesc = "职业";
                        break;
                    case 42:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patCompany;
                        boxItem.CaptionDesc = "工作地点";
                        break;
                    case 43:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patTel;
                        boxItem.CaptionDesc = "电话";
                        break;
                    case 44:
                        boxItem.CaptionImage = Properties.Resources.姓名;
                        boxItem.EnumEfObj = enumFormObject.patContactPerson;
                        boxItem.CaptionDesc = "联系人";
                        break;
                    case 45:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patContactTel;
                        boxItem.CaptionDesc = "联系人电话";
                        break;
                    case 46:
                        boxItem.CaptionImage = Properties.Resources.Properties_16x16;
                        boxItem.EnumEfObj = enumFormObject.patContactRelation;
                        boxItem.CaptionDesc = "与联系人关系";
                        break;
                    case 47:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patHeight;
                        boxItem.CaptionDesc = "身高";
                        break;
                    case 48:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patWeight;
                        boxItem.CaptionDesc = "体重";
                        break;
                    case 49:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patBloodType;
                        boxItem.CaptionDesc = "血型";
                        break;
                    case 50:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patTemperature;
                        boxItem.CaptionDesc = "体温";
                        break;
                    case 51:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patPulse;
                        boxItem.CaptionDesc = "脉搏";
                        break;
                    case 52:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patBreath;
                        boxItem.CaptionDesc = "呼吸";
                        break;
                    case 53:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patBloodPressure;
                        boxItem.CaptionDesc = "血压";
                        break;
                    case 54:
                        boxItem.CaptionImage = Properties.Resources.入院时间;
                        boxItem.EnumEfObj = enumFormObject.patRecordDate;
                        boxItem.CaptionDesc = "记录时间";
                        break;
                    case 55:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patOpNo;
                        boxItem.CaptionDesc = "门诊号";
                        break;
                    case 56:
                        boxItem.CaptionImage = Properties.Resources.住院号;
                        boxItem.EnumEfObj = enumFormObject.patOpIpNo;
                        boxItem.CaptionDesc = "门诊住院号";
                        break;
                    default:
                        return;
                }

                boxItem.InitColor = InitColor;
                if (i > 0)
                {
                    boxItem.picCaption.MouseMove += new MouseEventHandler(ucItem_MouseMove);
                    boxItem.picCaption.MouseDoubleClick += new MouseEventHandler(ucItem_MouseDoubleClick);
                    boxItem.lblCaption.MouseMove += new MouseEventHandler(ucItem_MouseMove);
                    boxItem.lblCaption.MouseDoubleClick += new MouseEventHandler(ucItem_MouseDoubleClick);
                }
                if (i < 20)  // 加载--编辑控件
                {
                    flpTop.Controls.Add(boxItem);
                }
                else    // 加载--宏元素控件
                {
                    fltBottom.Controls.Add(boxItem);
                }
            }
            #endregion

            #region ResetStyles

            foreach (DevExpress.XtraNavBar.ViewInfo.BaseViewInfoRegistrator obj in this.navBarControl.AvailableNavBarViews)
            {
                if (obj.ToString() == ("SkinNav:" + GlobalLogin.SkinName))
                {
                    this.navBarControl.View = obj;
                    //this.navBarControl.NavigationPaneMaxVisibleGroups = 0;
                    this.navBarControl.ResetStyles();
                    break;
                }
            }
            #endregion
        }

        #region GetEfObj
        /// <summary>
        /// GetEfObj
        /// </summary>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        private EntityFormObject GetEfObj(enumFormObject enumObj)
        {
            EntityFormObject entityObj = new EntityFormObject();
            entityObj.objEnum = enumObj;
            switch (enumObj)
            {
                case enumFormObject.ctrlHLine:
                    entityObj.objType = typeof(ctlLine);
                    break;
                case enumFormObject.ctrlVLine:
                    entityObj.objType = typeof(ctlVertLine);
                    break;
                case enumFormObject.ctrlLable:
                    entityObj.objType = typeof(ctlLabelEf);
                    break;
                case enumFormObject.ctrlCheckBox:
                    entityObj.objType = typeof(ctlCheckBox);
                    break;
                case enumFormObject.ctrlTextBox:
                    entityObj.objType = typeof(ctlTextBox);
                    break;
                case enumFormObject.ctrlMemoEdit:
                    entityObj.objType = typeof(ctlMemoEdit);
                    break;
                case enumFormObject.ctrlPictureBox:
                    entityObj.objType = typeof(ctlPictureBox);
                    break;
                case enumFormObject.ctrlDatetime:
                    entityObj.objType = typeof(ctlDatetime);
                    break;
                case enumFormObject.ctrlCombox:
                    entityObj.objType = typeof(ctlComboBox);
                    break;
                case enumFormObject.ctrlButton:
                    entityObj.objType = typeof(ctlButton);
                    break;
                case enumFormObject.ctrlPanel:
                    entityObj.objType = typeof(ctlPanel);
                    break;
                case enumFormObject.ctrlTabpage:
                    entityObj.objType = typeof(ctlTabControl);
                    break;
                case enumFormObject.ctrlDicDept:
                    entityObj.objType = typeof(ctlTreeSelectDept);
                    break;
                case enumFormObject.ctrlDicEmployee:
                    entityObj.objType = typeof(ctlTreeSelectEmployee);
                    break;
                case enumFormObject.ctrlDicICD:
                    entityObj.objType = typeof(ctlTreeSelectICD);
                    break;
                case enumFormObject.ctrlDicCheckPart:
                    entityObj.objType = typeof(ctlTreeSelectCheckPart);
                    break;
                case enumFormObject.ctrlSignature:
                    entityObj.objType = typeof(ctlSignature);
                    break;
                case enumFormObject.ctrlEmrEditor:
                    entityObj.objType = typeof(ctlRichTextBox);
                    break;
                case enumFormObject.ctrlEmrTable:
                    entityObj.objType = typeof(ctlTableLayout4Design);
                    break;
                case enumFormObject.patHospitalName:
                    entityObj.objType = typeof(ctlHospitalName);
                    break;
                case enumFormObject.patName:
                    entityObj.objType = typeof(ctlPatientName);
                    break;
                case enumFormObject.patSex:
                    entityObj.objType = typeof(ctlPatientSex);
                    break;
                case enumFormObject.patAge:
                    entityObj.objType = typeof(ctlPatientAge);
                    break;
                case enumFormObject.patBirthDay:
                    entityObj.objType = typeof(ctlPatientBirthDay);
                    break;
                case enumFormObject.patIpNo:
                    entityObj.objType = typeof(ctlPatientIpNo);
                    break;
                case enumFormObject.patIpTimes:
                    entityObj.objType = typeof(ctlPatientIpTimes);
                    break;
                case enumFormObject.patOpIpNo:
                    entityObj.objType = typeof(ctlPatientOpIpNo);
                    break;
                case enumFormObject.patInDate:
                    entityObj.objType = typeof(ctlPatientInDate);
                    break;
                case enumFormObject.patOutDate:
                    entityObj.objType = typeof(ctlPatientOutDate);
                    break;
                case enumFormObject.patInDays:
                    entityObj.objType = typeof(ctlPatientInDays);
                    break;
                case enumFormObject.patDept:
                    entityObj.objType = typeof(ctlPatientDept);
                    break;
                case enumFormObject.patArea:
                    entityObj.objType = typeof(ctlPatientArea);
                    break;
                case enumFormObject.patBedNo:
                    entityObj.objType = typeof(ctlPatientBedNo);
                    break;
                case enumFormObject.patOpNo:
                    entityObj.objType = typeof(ctlPatientOpNo);
                    break;
                case enumFormObject.patTel:
                    entityObj.objType = typeof(ctlPatientTel);
                    break;
                case enumFormObject.patMarriage:
                    entityObj.objType = typeof(ctlPatientMarriage);
                    break;
                case enumFormObject.patProfession:
                    entityObj.objType = typeof(ctlPatientProfession);
                    break;
                case enumFormObject.patNation:
                    entityObj.objType = typeof(ctlPatientNation);
                    break;
                case enumFormObject.patHomeadr:
                    entityObj.objType = typeof(ctlPatientHomeAddr);
                    break;
                case enumFormObject.patNativePlace:
                    entityObj.objType = typeof(ctlPatientNativePlace);
                    break;
                default:
                    break;
            }
            return entityObj;
        }
        #endregion

        private void ucToolBoxEf_Resize(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                navBarGroup2.GroupClientHeight = this.Height - navBarGroup1.GroupClientHeight - 70; //58;
            }
        }


    }
}
