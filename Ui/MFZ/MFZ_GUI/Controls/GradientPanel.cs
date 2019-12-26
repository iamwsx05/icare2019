using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.MFZ.Controls
{
    public partial class GradientPanel : Panel
    {
        #region Properties

        Color _gradientStartColor;
        Color _gradientEndColor;
        Image _floatingImage;
        int _imageXOffset;
        int _imageYOffset;
        int _angle;
        float _horizontalFillPercent;
        float _verticalFillPercent;
        bool _flip;

        #endregion

        public GradientPanel()
        {
            _gradientStartColor = Color.White;
            _gradientEndColor = Color.Gray;
            _floatingImage = null;
            _imageXOffset = 0;
            _imageYOffset = 0;
            _angle = 90;
            _horizontalFillPercent = 100;
            _verticalFillPercent = 100;
            _flip = false;

            // Set these properties to reduce flicker on resize
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
        }

        #region Property Definitions
        public Color GradientStartColor
        {
            get
            {
                return _gradientStartColor;
            }
            set
            {
                _gradientStartColor = value;
                this.Invalidate();
            }
        }

        public Color GradientEndColor
        {
            get
            {
                return _gradientEndColor;
            }
            set
            {
                _gradientEndColor = value;
                this.Invalidate();
            }
        }

        // Optional image that is painted as part of the background
        public Image FloatingImage
        {
            get
            {
                return _floatingImage;
            }
            set
            {
                _floatingImage = value;
                Bitmap b = FloatingImage as Bitmap;
                if (b != null)
                {
                    b.MakeTransparent(Color.White);
                }
                this.Invalidate();
            }
        }

        // The x offset from the right for the floating image
        public int imageXOffset
        {
            get
            {
                return _imageXOffset;
            }
            set
            {
                _imageXOffset = value;
                this.Invalidate();
            }
        }

        // The y offset from the bottom for the floating image
        public int imageYOffset
        {
            get
            {
                return _imageYOffset;
            }
            set
            {
                _imageYOffset = value;
                this.Invalidate();
            }
        }

        public int GradientAngle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
                this.Invalidate();
            }
        }

        // How far will the gradient fill horizonatally
        public float HorizontalFillPercent
        {
            get
            {
                return _horizontalFillPercent;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _horizontalFillPercent = value;
                    this.Invalidate();
                }
            }
        }

        // How far the gradient will fill vertically
        public float VerticalFillPercent
        {
            get
            {
                return _verticalFillPercent;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _verticalFillPercent = value;
                    this.Invalidate();
                }
            }
        }

        // Whether or not to flip the image
        public bool Flip
        {
            get
            {
                return _flip;
            }
            set
            {
                _flip = value;
                this.Invalidate();
            }
        }
        #endregion

        /**
		 *  Overrides the normal paint method for the background and paints a gradient
		 */
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Rectangle _fillRectangle;
                Rectangle clientRect = this.ClientRectangle;

                int _newWidth = (int)((float)clientRect.Width * (_horizontalFillPercent / 100));
                int _newHeight = (int)((float)clientRect.Height * (_verticalFillPercent / 100));
                int _newAngle = _angle;
                int _newX = clientRect.X;
                int _newY = clientRect.Y;

                // Check to see if the gradient will cover 100% of the panel
                if (_horizontalFillPercent < 100 || _verticalFillPercent < 100)
                {
                    using (Brush br = new SolidBrush(this.BackColor))
                    {
                        e.Graphics.FillRectangle(br, clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height);
                    }
                }

                // Flip the gradient if necessary
                if (_flip)
                {
                    _newX += (clientRect.Width - _newWidth);
                    _newY += (clientRect.Height - _newHeight);
                    _newAngle = (_angle + 180) % 360;
                    _fillRectangle = new Rectangle(new Point(_newX, _newY), new Size(_newWidth - 1, _newHeight));
                }
                else
                    _fillRectangle = new Rectangle(clientRect.Location, new Size(_newWidth, _newHeight));

                // Paint the gradient
                using (Brush b = new LinearGradientBrush(_fillRectangle, _gradientStartColor, _gradientEndColor, _newAngle))
                {
                    e.Graphics.FillRectangle(b, _newX, _newY, _newWidth, _newHeight);
                }
                if (_floatingImage != null)
                {
                    e.Graphics.DrawImage(_floatingImage, new Point((this.Width - (_floatingImage.Width + _imageXOffset)), (this.Height - (_floatingImage.Height + _imageYOffset))));
                }
            }
            catch { }
        }
    }
}
