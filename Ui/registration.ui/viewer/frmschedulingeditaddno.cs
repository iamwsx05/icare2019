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
using DevExpress.XtraEditors;

namespace Registration.Ui
{
    public partial class frmSchedulingEditAddNo : frmBasePopup
    {
        public frmSchedulingEditAddNo(List<EntityOpRegSchedulingDatePlus> _lstDate)
        {
            InitializeComponent();
            if (!DesignMode)
            { 
                lstDate = _lstDate;
            }
        }

        List<EntityOpRegSchedulingDatePlus> lstDate { get; set; }

        private void frmSchedulingEditAddNo_Load(object sender, EventArgs e)
        {
            this.gcDate.DataSource = this.lstDate;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.gvDate.CloseEditor();
            EntityOpRegSchedulingDayDate vo = null;
            List<EntityOpRegSchedulingDayDate> lstReg = new List<EntityOpRegSchedulingDayDate>();
            foreach (EntityOpRegSchedulingDatePlus item in lstDate)
            {
                if (item.addNo > 0)
                {
                    vo = new EntityOpRegSchedulingDayDate();
                    vo.serNo = item.serNo;
                    vo.regDid = item.regDid;
                    vo.startTime = item.dateScope.Split('~')[0];
                    vo.endTime = item.dateScope.Split('~')[1];
                    vo.amPm = (int)item.amPm;
                    vo.limitNum = (int)item.limitNum + (int)item.addNo;
                    vo.addNo = item.addNo;
                    vo.isHaveQueue = item.isHaveQueue;
                    TimeSpan ts = Convert.ToDateTime(vo.endTime).Subtract(Convert.ToDateTime(vo.startTime));
                    if (ts.TotalMinutes > 0)
                    {
                        vo.freqNum = Math.Ceiling(Convert.ToDecimal((decimal)ts.TotalMinutes / vo.limitNum));
                    }
                    lstReg.Add(vo);
                }
            }
            if (lstReg.Count == 0)
            {
                DialogBox.Msg("请输入加号数量。");
                return;
            }
            if (DialogBox.Msg("加号前 请再次确认？", MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    try
                    {
                        uiHelper.BeginLoading(this);
                        if (proxy.Service.SchedulingAddNo(lstReg) > 0)
                        {
                            DialogBox.Msg("加号成功");
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                        {
                            DialogBox.Msg("加号失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Msg("加号失败:\r\n" + ex.Message);
                    }
                    finally
                    {
                        uiHelper.CloseLoading(this);
                    }
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
