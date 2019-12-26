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
    /// 挂号类型(号别)
    /// </summary>
    public partial class frmRegisterType : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterType()
        {
            InitializeComponent();
        }
        #endregion

        #region override

        #region CreateController
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlRegisterType();
            Controller.SetUI(this);
        }
        #endregion

        public override void New()
        {
            ((ctlRegisterType)Controller).New();
        }

        public override void Delete()
        {
            ((ctlRegisterType)Controller).Del();
        }

        public override void Save()
        {
            ((ctlRegisterType)Controller).Save();
        }

        public override void Preview()
        {
            ((ctlRegisterType)Controller).Print();
        }

        #endregion

        #region 事件

        private void frmRegisterType_Load(object sender, EventArgs e)
        {
            ((ctlRegisterType)Controller).Init();
        }

        #endregion
    }
}
