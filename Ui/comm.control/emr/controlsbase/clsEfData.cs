using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Common.Entity;

namespace Common.Controls.Emr
{
    public class FormObject
    {
        public static ToolboxItem ToolboxItem { get; set; }
        public static Color LableForeColor = Color.FromArgb(21, 66, 139);
    }

    public enum enumFormObject
    {
        ctrlHLine,
        ctrlVLine,
        ctrlLable,
        ctrlCheckBox,
        ctrlTextBox,
        ctrlMemoEdit,
        ctrlPictureBox,
        ctrlDatetime,
        ctrlCombox,
        ctrlButton,
        ctrlSignature,        
        ctrlDicEmployee,
        ctrlDicDept,
        ctrlDicICD,
        ctrlDicCheckPart,
        ctrlLayout,
        ctrlPanel,
        ctrlTabpage,
        ctrlEmrEditor,
        ctrlEmrTable,
        patHospitalName,
        patName,
        patSex,
        patAge,
        patBirthDay,
        patIpNo,
        patIpTimes,
        patInDate,
        patOutDate,
        patInDays,
        patDept,
        patArea,
        patBedNo,
        patIdNumber,
        patMarriage,
        patNation,
        patNativePlace,
        patBirthplace,
        patHkadr,
        patHomeadr,
        patCulture,
        patProfession,
        patCompany,
        patTel,
        patContactPerson,
        patContactTel,
        patContactRelation,
        patHeight,
        patWeight,
        patBloodType,
        patTemperature,
        patPulse,
        patBreath,
        patBloodPressure,
        patRecordDate,
        patOpNo,
        patOpIpNo
    }

    public class EntityFormObject
    {
        public enumFormObject objEnum { get; set; }
        public Type objType { get; set; }
        public ToolboxItem ToolboxItem
        {
            get
            {
                if (objType != null)
                {
                    return new ToolboxItem(objType);
                }
                else
                {
                    return null;
                }
            }
        }
    }   
}
