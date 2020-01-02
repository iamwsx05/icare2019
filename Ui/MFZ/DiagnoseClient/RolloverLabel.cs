using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DiagnoseClient
{
    /**
	 *  A RolloverLabel is a Label that changes font when the mouse enters / leaves its area
	 */
    public partial class RolloverLabel : Label
    {
        #region Properties

        Image _iconImage;

        Image _rollOverImage;

        public Image RollOverImage
        {
            get { return _rollOverImage; }
            set { _rollOverImage = value; this.Invalidate(); }
        }
        Image _rollOutImage;

        public Image RollOutImage
        {
            get { return _rollOutImage; }
            set { _rollOutImage = value; this.Invalidate(); }
        }

        Font _rollOverFont;
        Font _rollOutFont;
        Cursor _rollOverCursor;
        Color _rollOverForeColor;
        Color _rollOverBackColor;
        Color _rollOutForeColor;
        Color _rollOutBackColor;

        #endregion

        public RolloverLabel()
        {
            _rollOverFont = null;
            _rollOutFont = this.Font;
            _rollOverCursor = Cursors.Hand;
            _rollOverForeColor = this.ForeColor;
            _rollOverBackColor = this.BackColor;
            _iconImage = null;

            _rollOverImage = this.Image;
            _rollOutImage = this.Image;

            this.MouseEnter += new System.EventHandler(this.mouseRollOver);
            this.MouseLeave += new System.EventHandler(this.mouseRollOut);
        }

        #region Property Definitions

        public Font RollOverFont
        {
            get
            {
                return _rollOverFont;
            }
            set
            {
                _rollOverFont = value;
                this.Invalidate();
            }
        }

        public Cursor RollOverCursor
        {
            get
            {
                return _rollOverCursor;
            }
            set
            {
                _rollOverCursor = value;
                this.Invalidate();
            }
        }

        public Color RollOverForeColor
        {
            get
            {
                return _rollOverForeColor;
            }
            set
            {
                _rollOverForeColor = value;
                this.Invalidate();
            }
        }
        public Color RollOverBackColor
        {
            get
            {
                return _rollOverBackColor;
            }
            set
            {
                _rollOverBackColor = value;
                this.Invalidate();
            }
        }

        public Image IconImage
        {
            get
            {
                return _iconImage;
            }
            set
            {
                _iconImage = value;
                this.Invalidate();
            }
        }

        #endregion

        /**
		 *  Event handler that manages font and cursor changes when the mouse hovers over the RolloverLabel
		 */
        private void mouseRollOver(object sender, EventArgs e)
        {
            this.Cursor = _rollOverCursor;

            if (RollOverFont != null)
            {
                _rollOutFont = this.Font;
                this.Font = _rollOverFont;
            }

            if (_rollOverImage != null)
            {
                _rollOutImage = this.Image;
                this.Image = _rollOverImage;
            }


            _rollOutForeColor = this.ForeColor;
            this.ForeColor = _rollOverForeColor;

            _rollOutBackColor = this.BackColor;
            this.BackColor = _rollOverBackColor;
        }

        /**
         *  Event handler that manages font and cursor changes when the mouse moves off the RolloverLabel
         */
        private void mouseRollOut(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;

            if (_rollOutFont != null)
            {
                this.Font = _rollOutFont;
            }

            if (_rollOutImage != null)
            {
                this.Image = _rollOutImage;
            }

            this.ForeColor = _rollOutForeColor;
            this.BackColor = _rollOutBackColor;
        }
    }
}
