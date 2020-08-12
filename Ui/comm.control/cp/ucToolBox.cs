using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Entity;
using Common.Utils;
using Common.Controls.Emr;

namespace Common.Controls
{
    public delegate void HandleCtrlMouseDoubleClick(object sender, EntityCPObject entityObj);
    public delegate void HandleCtrlMouseMove(object sender, EntityCPObject entityObj);

    public partial class ucToolBox : UserControl
    {
        public ucToolBox()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        public Font TextFont { get; set; }

        public HandleCtrlMouseDoubleClick CtrlMouseDoubleClick;

        public HandleCtrlMouseMove CtrlMouseMove;

        #region Mouse.Event

        #region Line

        private void picLineDU_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.lineDU));
            }
        }

        private void picLineDU_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.lineDU));
                }
            }
        }

        private void picLineUD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.lineUD));
            }
        }

        private void picLineUD_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.lineUD));
                }
            }
        }

        private void picLineRL_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.lineRL));
            }
        }

        private void picLineRL_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.lineRL));
                }
            }
        }

        private void picLineLR_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.lineLR));
            }
        }

        private void picLineLR_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.lineLR));
                }
            }
        }

        private void picLineLRU_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.lineLRU));
            }
        }

        private void picLineLRU_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.lineLRU));
                }
            }
        }

        private void picLineLRD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.lineLRD));
            }
        }

        private void picLineLRD_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.lineLRD));
                }
            }
        }

        private void picLineRLU_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.lineRLU));
            }
        }

        private void picLineRLU_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.lineRLU));
                }
            }
        }

        private void picLineRLD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.lineRLD));
            }
        }

        private void picLineRLD_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.lineRLD));
                }
            }
        }

        #endregion

        #region Node

        private void picNode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.ucNode));
            }
        }

        private void picNode_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.ucNode));
                }
            }
        }

        #endregion

        #region Click

        private void picDefault_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = null;
            SetCurrPic(null);
        }

        private void picLineDU_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.lineDU).ToolboxItem;
        }

        private void picLineUD_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.lineUD).ToolboxItem;
        }

        private void picLineRL_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.lineRL).ToolboxItem;
        }

        private void picLineLR_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.lineLR).ToolboxItem;
        }

        private void picLineLRU_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.lineLRU).ToolboxItem;
        }

        private void picLineLRD_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.lineLRD).ToolboxItem;
        }

        private void picLineRLU_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.lineRLU).ToolboxItem;
        }

        private void picLineRLD_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.lineRLD).ToolboxItem;
        }

        private void picNode_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.ucNode).ToolboxItem;
        }

        #endregion

        #region Note

        private void picNote_MouseClick(object sender, MouseEventArgs e)
        {
            CpObject.ToolboxItem = GetCPObj(enumCPObject.label).ToolboxItem;
        }

        private void picNote_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CtrlMouseDoubleClick != null)
            {
                CtrlMouseDoubleClick(sender, GetCPObj(enumCPObject.label));
            }
        }

        private void picNote_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CtrlMouseMove != null)
                {
                    CtrlMouseMove(sender, GetCPObj(enumCPObject.label));
                }
            }
        }

        #endregion

        private EntityCPObject GetCPObj(enumCPObject enumObj)
        {
            EntityCPObject entityObj = new EntityCPObject();
            entityObj.objEnum = enumObj;

            switch (enumObj)
            {
                case enumCPObject.lineDU:
                    entityObj.objType = typeof(ctlLineDU);
                    entityObj.objImg = picLineDU.Image;
                    break;
                case enumCPObject.lineLR:
                    entityObj.objType = typeof(ctlLineLR);
                    entityObj.objImg = picLineLR.Image;
                    break;
                case enumCPObject.lineLRD:
                    entityObj.objType = typeof(ctlLineLRD);
                    entityObj.objImg = picLineLRD.Image;
                    break;
                case enumCPObject.lineLRU:
                    entityObj.objType = typeof(ctlLineLRU);
                    entityObj.objImg = picLineLRU.Image;
                    break;
                case enumCPObject.lineRL:
                    entityObj.objType = typeof(ctlLineRL);
                    entityObj.objImg = picLineRL.Image;
                    break;
                case enumCPObject.lineRLD:
                    entityObj.objType = typeof(ctlLineRLD);
                    entityObj.objImg = picLineRLD.Image;
                    break;
                case enumCPObject.lineRLU:
                    entityObj.objType = typeof(ctlLineRLU);
                    entityObj.objImg = picLineRLU.Image;
                    break;
                case enumCPObject.lineUD:
                    entityObj.objType = typeof(ctlLineUD);
                    entityObj.objImg = picLineUD.Image;
                    break;
                case enumCPObject.ucNode:
                    entityObj.objType = typeof(ucNode);
                    entityObj.objImg = picNode.Image;
                    break;
                case enumCPObject.label:
                    entityObj.objType = typeof(ctlLabelEf);
                    entityObj.objImg = picDefault.Image;
                    break;
                default:
                    break;
            }
            SetCurrPic(entityObj);

            return entityObj;
        }

        private void SetCurrPic(EntityCPObject entityObj)
        {
            if (entityObj == null)
            {
                picCurr.Image = null;
            }
            else
            {
                picCurr.Image = entityObj.objImg;
            }
        }
        #endregion

        private void picDefault_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void picDefault_MouseMove(object sender, MouseEventArgs e)
        {

        }


    }
}
