using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Common.Controls;
using Report.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Report.Ui
{
    public partial class frmInfectionusConfirm : XtraForm
    {
        EntityInfectionus dataVo;

        public frmInfectionusConfirm(EntityInfectionus vo)
        {
            InitializeComponent();
            dataVo = new EntityInfectionus();
            dataVo = vo;
        }

        #region 事件
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ConfirmRpt(1);
            this.Close();
        }
        private void btnDenial_Click(object sender, EventArgs e)
        {
            ConfirmRpt(2);
            this.Close();
        }
        #endregion

        #region 方法
        /// <summary>
        /// ConfirmRpt
        /// </summary>
        /// <param name="flg">1 审核通过 2 审核未通过 0 反审核</param>
        #region
        void ConfirmRpt(int flg)
        {
            if (dataVo.rptId <= 0)
                return;
            if ((flg == 1 && dataVo.status == 1) || (flg == 2 && dataVo.status == 2))
                DialogBox.Msg("已审核");
            else
            {
                dataVo.status = flg;
                using (ProxyAdverseEvent proxy = new ProxyAdverseEvent())
                {
                    if (proxy.Service.ComfirmRpt(dataVo) > 0)
                    {
                        if (flg == 1)
                        {
                            DialogBox.Msg("审核成功");
                        }
                        else if (flg == 2)
                        {

                            DialogBox.Msg("审核成功");
                        }
                        else
                        {
                            DialogBox.Msg("反审核成功");
                        }

                        dataVo = null;
                    }
                    else
                    {
                        if (flg == 1)
                            DialogBox.Msg("审核失败");
                        else
                            DialogBox.Msg("反审核失败");
                        dataVo = null;
                    }
                }
            }
        }
        #endregion
        
        #endregion
    }
}