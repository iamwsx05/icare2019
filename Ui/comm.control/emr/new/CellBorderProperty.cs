using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common.Controls.Emr
{
    public class CellBorderProperty
    {
        #region 是否画边框边框

        /// <summary>
        /// 是否画顶边框
        /// </summary>
        public bool DrawTopBorder { get; set; }

        /// <summary>
        /// 是否画左边框
        /// </summary>
        public bool DrawLeftBorder { get; set; }

        /// <summary>
        /// 是否画底边框
        /// </summary>
        public bool DrawBottomBorder { get; set; }

        /// <summary>
        /// 是否画右边框
        /// </summary>
        public bool DrawRightBorder { get; set; }
        #endregion

        #region 边框宽度

        public float TopBorderWidth { get; set; }
        public float LeftBorderWidth { get; set; }
        public float BottomBorderWidth { get; set; }
        public float RightBorderWidth { get; set; }

        #endregion

        #region 边框颜色

        public Color TopBorderColor { get; set; }
        public Color LeftBorderColor { get; set; }
        public Color BottomBorderColor { get; set; }
        public Color RightBorderColor { get; set; } 
        #endregion

        public CellBorderProperty()
        {
            TopBorderWidth = ThreeItemConstValue.DefaultBorderWidth;
            LeftBorderWidth = ThreeItemConstValue.DefaultBorderWidth;
            BottomBorderWidth = ThreeItemConstValue.DefaultBorderWidth;
            RightBorderWidth = ThreeItemConstValue.DefaultBorderWidth;

            DrawTopBorder = true;
            DrawLeftBorder = true;
            DrawBottomBorder = true;
            DrawRightBorder = true;

            TopBorderColor = Color.Black;
            LeftBorderColor = Color.Black;
            BottomBorderColor = Color.Black;
            RightBorderColor = Color.Black;
        }
    }
}
