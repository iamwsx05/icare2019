using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Common.Controls
{
    /// <summary>
    /// 可分页移动的控件
    /// </summary>
    public class ctlMovePage : ScrollableControl
    {
        List<Control> _ctl = new List<Control>();
        DevExpress.XtraEditors.PanelControl _panBack = new DevExpress.XtraEditors.PanelControl();
        DevExpress.XtraEditors.PanelControl _panLeft = new DevExpress.XtraEditors.PanelControl();
        DevExpress.XtraEditors.PanelControl _panRight = new DevExpress.XtraEditors.PanelControl();
        const int _pw = 30;

        public ctlMovePage()
        {
            this.BackColor = Color.AliceBlue;
            _panBack.Dock = DockStyle.Fill;
            _panBack.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            _panLeft.Width = 10;
            _panRight.Width = 10;
            _panLeft.Dock = DockStyle.Left;
            _panRight.Dock = DockStyle.Right;
            DevExpress.XtraEditors.LabelControl lbLeft = CreateLab();
            DevExpress.XtraEditors.LabelControl lbRight = CreateLab();
            lbLeft.Text = "<<";
            lbRight.Text = ">>";
            lbLeft.Click += new EventHandler(_panLeft_Click);
            lbRight.Click += new EventHandler(_panRight_Click);
            _panLeft.Controls.Add(lbLeft);
            _panRight.Controls.Add(lbRight);
            this.Disposed += new EventHandler(ctlMovePage_Disposed);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Enabled = false;
            _timer.Interval = 1000 * 3;
        }
        /// <summary>
        /// 超过3秒不点击左右导航，将左右导航缩小
        /// </summary>
        Timer _timer = new Timer();
        void _timer_Tick(object sender, EventArgs e)
        {
            _panLeft.Width = 8;
            _panRight.Width = 8;
            _timer.Stop();
        }
        void ctlMovePage_Disposed(object sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
        }
        private DevExpress.XtraEditors.LabelControl CreateLab()
        {
            DevExpress.XtraEditors.LabelControl lb = new DevExpress.XtraEditors.LabelControl();
            lb.Appearance.Options.UseTextOptions = true;
            lb.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lb.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lb.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            lb.Dock = DockStyle.Fill;
            return lb;
        }

        private int _width
        { get { return this.Width - 16; } }
        private int _height
        { get { return this.Height; } }

        /// <summary>
        /// 增加分页容器
        /// </summary>
        /// <param name="p_ctl">需平均分布在容器内的控件</param>
        /// <param name="p_width">控件的宽度</param>
        /// <param name="p_height">控件的高度</param>
        /// <param name="p_left">顶边距</param>
        /// <param name="p_top">右边距</param>
        /// <returns>总页面数</returns>
        public int addControl(Control[] p_ctl, int p_width, int p_height, int p_top, int p_left)
        {
            List<DevExpress.XtraEditors.PanelControl> lstPan = new List<DevExpress.XtraEditors.PanelControl>();
            DevExpress.XtraEditors.PanelControl pan = new DevExpress.XtraEditors.PanelControl();
            int left = p_left, top = p_top;
            foreach (Control ctl in p_ctl)
            {
                ctl.Bounds = new Rectangle(left, top, p_width, p_height);
                pan.Controls.Add(ctl);
                left += p_width + p_width / 10;
                if ((left + p_width) > _width)
                {
                    left = p_left;
                    top += p_height + p_height / 10;

                    if ((top + p_height) > _height)
                    {
                        lstPan.Add(pan);
                        pan = new DevExpress.XtraEditors.PanelControl();
                        top = p_top;
                    }
                }
            }
            if (pan.Controls.Count > 0) lstPan.Add(pan);
            addControl(lstPan.ToArray());
            return lstPan.Count();
        }
        /// <summary>
        /// 移除所有的页
        /// </summary>
        public void RemoveControl()
        {
            for (int i = _ctl.Count - 1; i >= 0; i--)
            {
                RemoveChild(_ctl[i]);
            }
            _ctl.Clear();
            _iPanel = 0;
            RefreshPanel();
        }
        /// <summary>
        /// 移除指定的页
        /// </summary>
        /// <param name="p_ctl">要移除的页</param>
        public void RemoveControl(Control p_ctl)
        {
            if (_ctl.Equals(p_ctl))
            {
                _ctl.Remove(p_ctl);
                RemoveChild(p_ctl);
                _iPanel = 0;
                RefreshPanel();
            }
        }

        private void RemoveChild(Control control)
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                RemoveChild(control.Controls[i]);
            }
            control.Dispose();
        }
        void _panRight_Click(object sender, EventArgs e)
        {
            if (_panRight.Width == _pw) MovePrev();
            _panRight.Width = _pw;
            _timer.Start();
        }

        void _panLeft_Click(object sender, EventArgs e)
        {
            if (_panLeft.Width == _pw) MoveNext();
            _panLeft.Width = _pw;
            _timer.Start();
        }
        /// <summary>
        /// 增加分页容器
        /// <param name="p_ctl">容器</param>
        /// </summary>
        public void addControl(Control[] p_ctl)
        {
            foreach (Control ctl in p_ctl)
            {
                ctl.Dock = DockStyle.None;
                _ctl.Add(ctl);
                SetMouseEvents(ctl);
                _panBack.Controls.Add(ctl);
            }
            RefreshPanel();
        }
        /// <summary>
        /// 增加分页容器
        /// <param name="p_ctl">容器</param>
        /// </summary>
        public void addControl(Control p_ctl)
        {
            addControl(new Control[] { p_ctl });
        }
        /// <summary>
        /// 下一页
        /// </summary>
        public void MoveNext()
        {
            if (_iPanel < _ctl.Count - 1) _iPanel++;
            pageSwing();
        }
        /// <summary>
        /// 前一页
        /// </summary>
        public void MovePrev()
        {
            if (_iPanel > 0) _iPanel--;
            pageSwing();
        }
        bool _stopSwing = false;
        /// <summary>
        /// 页面摆动效果
        /// </summary>
        private void pageSwing()
        {
            if (_iPanel >= _ctl.Count) return;
            if (_stopSwing) return;
            _stopSwing = true;
            int s1 = _ctl[_iPanel].Left / 20;
            for (int j = 0; j < 20; j++)
            {
                long k = DateTime.Now.Ticks;
                while (DateTime.Now.Ticks - k < 3) { Application.DoEvents(); }
                for (int i = 0; i < _ctl.Count(); i++) _ctl[i].Left += -1 * s1;
            }
            RefreshPanel();

            for (int s = 2; s >= 0; s--)
            {
                for (int p = 0; p < 4; p++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        long k = DateTime.Now.Ticks;
                        while (DateTime.Now.Ticks - k < 3) { Application.DoEvents(); }
                        for (int i = 0; i < _ctl.Count(); i++) _ctl[i].Left += (((p == 0 || p == 3) ? 1 : -1) * s);
                    }
                }
            }
            for (int i = 0; i < _ctl.Count(); i++) _ctl[i].Left = (i - _iPanel) * _width;
            _stopSwing = false;
        }
        /// <summary>
        /// 重排panel
        /// </summary>
        public void RefreshPanel()
        {
            for (int i = 0; i < _ctl.Count(); i++)
            {
                _ctl[i].Visible = (i >= _iPanel - 1 && i <= _iPanel + 1); //前后都是可见的，用于移动
                _ctl[i].Width = _width;
                _ctl[i].Height = _height;
                _ctl[i].Left = (i - _iPanel) * _width;
                _ctl[i].Top = 0;
            }
        }
        private void SetMouseEvents(Control control)
        {
            control.MouseDown += new MouseEventHandler(ctlMouseDown);
            control.MouseMove += new MouseEventHandler(ctlMouseMove);
            control.MouseUp += new MouseEventHandler(ctlMouseUp);
            foreach (Control ctl in control.Controls)
                SetMouseEvents(ctl);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshPanel();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!this.DesignMode)
            {
                _ctl.Clear();
                SetMouseEvents(_panBack);
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    this.Controls[i].Dock = DockStyle.None;
                    _ctl.Add(this.Controls[i]);
                    SetMouseEvents(this.Controls[i]);
                    this.Controls[i].Parent = _panBack;
                }
                //必须按如下顺序装入                
                this.Controls.Add(_panBack);
                this.Controls.Add(_panLeft);
                this.Controls.Add(_panRight);
                RefreshPanel();
            }
        }

        /// <summary>
        /// 当前可见的panel
        /// </summary>
        int _iPanel = 0;
        int _MouseX = 0;
        bool _isMove = false;
        private void ctlMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = ((Control)sender).PointToScreen(new Point(e.X, e.Y)).X;
                if (Math.Abs(x - _MouseX) > 5)
                {
                    foreach (DevExpress.XtraEditors.PanelControl panel in _ctl)
                    {
                        panel.Left += x - _MouseX;
                    }
                    _MouseX = x;
                    _isMove = true;
                }
            }
        }
        private void ctlMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _MouseX = ((Control)sender).PointToScreen(new Point(e.X, e.Y)).X;
                _isMove = false;
                int x1 = this.PointToScreen(new Point(0, 0)).X;
                int x2 = this.PointToScreen(new Point(this.Width, 0)).X;
                if (x1 < _MouseX && _MouseX - x1 < _pw) //必须相对顶层容器的边距
                {
                    _panLeft.Width = _pw;
                    _timer.Start();
                    return;
                }
                if (x2 > _MouseX && x2 - _MouseX < _pw)
                {
                    _panRight.Width = _pw;
                    _timer.Start();
                    return;
                }

            }
        }
        public event MouseEventHandler MouseClick;
        private void ctlMouseUp(object sender, MouseEventArgs e)
        {
            if (_ctl.Count == 0) return;
            if (_ctl[_iPanel].Left > _width / 4) //向左移一页
                MovePrev();
            else if (_ctl[_iPanel].Left < -1 * _width / 4)  //向右移一页
                MoveNext();
            else if (_isMove)
                pageSwing();
            else if (MouseClick != null)
                MouseClick(sender, e);
        }
    }
}
