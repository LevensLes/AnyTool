﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Wagwan_clicker
{
    public class FlatTabControl : TabControl
    {
        private int W;
        private int H;

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Alignment = TabAlignment.Top;
        }

        [Category("Colors")]
        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        [Category("Colors")]
        public Color ActiveColor
        {
            get { return _ActiveColor; }
            set { _ActiveColor = value; }
        }

        private Color BGColor = Color.FromArgb(23, 23, 23);
        private Color _BaseColor = Color.FromArgb(45, 47, 49);
        private Color _ActiveColor = Helpers.FlatColor;

        public FlatTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.FromArgb(23, 23, 23);

            Font = new Font("Segoe UI", 10);
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(120, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.UpdateColors();

            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width - 1;
            H = Height - 1;

            var _with13 = G;
            _with13.SmoothingMode = SmoothingMode.HighQuality;
            _with13.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with13.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with13.Clear(_BaseColor);

            try
            {
                SelectedTab.BackColor = BGColor;
            }
            catch
            {
            }

            for (int i = 0; i <= TabCount - 1; i++)
            {
                Rectangle Base = new Rectangle(new Point(GetTabRect(i).Location.X + 2, GetTabRect(i).Location.Y), new Size(GetTabRect(i).Width, GetTabRect(i).Height));
                Rectangle BaseSize = new Rectangle(Base.Location, new Size(Base.Width, Base.Height));

                if (i == SelectedIndex)
                {
                    //-- Base
                    _with13.FillRectangle(new SolidBrush(_BaseColor), BaseSize);

                    //-- Gradiant
                    //.fill
                    _with13.FillRectangle(new SolidBrush(_ActiveColor), BaseSize);

                    //-- ImageList
                    if (ImageList != null)
                    {
                        try
                        {
                            if (ImageList.Images[TabPages[i].ImageIndex] != null)
                            {
                                //-- Image
                                _with13.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(BaseSize.Location.X + 8, BaseSize.Location.Y + 6));
                                //-- Text
                                _with13.DrawString("      " + TabPages[i].Text, Font, Brushes.White, BaseSize, Helpers.CenterSF);
                            }
                            else
                            {
                                //-- Text
                                _with13.DrawString(TabPages[i].Text, Font, Brushes.White, BaseSize, Helpers.CenterSF);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    else
                    {
                        //-- Text
                        _with13.DrawString(TabPages[i].Text, Font, Brushes.White, BaseSize, Helpers.CenterSF);
                    }
                }
                else
                {
                    //-- Base
                    _with13.FillRectangle(new SolidBrush(_BaseColor), BaseSize);

                    //-- ImageList
                    if (ImageList != null)
                    {
                        try
                        {
                            if (ImageList.Images[TabPages[i].ImageIndex] != null)
                            {
                                //-- Image
                                _with13.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(BaseSize.Location.X + 8, BaseSize.Location.Y + 6));
                                //-- Text
                                _with13.DrawString("      " + TabPages[i].Text, Font, new SolidBrush(Color.White), BaseSize, new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Center
                                });
                            }
                            else
                            {
                                //-- Text
                                _with13.DrawString(TabPages[i].Text, Font, new SolidBrush(Color.White), BaseSize, new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Center
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    else
                    {
                        //-- Text
                        _with13.DrawString(TabPages[i].Text, Font, new SolidBrush(Color.White), BaseSize, new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        });
                    }
                }
            }

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

        private void UpdateColors()
        {
            FlatColors colors = Helpers.GetColors(this);

            _ActiveColor = colors.Flat;
        }
    }
}