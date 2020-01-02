using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Registration.Ui
{
    /// <summary>
    /// 排班复制
    /// </summary>
    public partial class frmSchedulingCopy : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmSchedulingCopy(string _DeptCode, string _RoomCode, string _DoctCode)
        {
            InitializeComponent();
            if (!DesignMode)
            { 
                ((ctlSchedulingCopy)Controller).DeptCode = _DeptCode;
                ((ctlSchedulingCopy)Controller).RoomCode = _RoomCode;
                ((ctlSchedulingCopy)Controller).DoctCode = _DoctCode;
            }
        }
        #endregion

        #region CreateController
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlSchedulingCopy();
            Controller.SetUI(this);
        }
        #endregion

        #region 事件

        private void frmSchedulingCopy_Load(object sender, EventArgs e)
        {
            ((ctlSchedulingCopy)Controller).Init();
        }

        private void btnAddDoct_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingCopy)Controller).DoctAdd();
        }

        private void btnDelDoct_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingCopy)Controller).DoctDel();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingCopy)Controller).SchedulingCopy();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void lueDoct_HandleDBValueChanged(object sender)
        {
            this.lueRoom.Text = string.Empty;
            ((ctlSchedulingCopy)Controller).LueFilterRoom();
        }

    }
}
