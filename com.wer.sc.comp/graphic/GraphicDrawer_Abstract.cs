using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 画图器
    /// 用于画单个图形，比如说k线图、k线图的量、k线图的macd线、分时图、分时图的量等
    /// </summary>
    public abstract class GraphicDrawer_Abstract
    {
        #region 通用属性

        private Rectangle displayRect;

        public Rectangle DisplayRect
        {
            get
            {
                return displayRect;
            }

            set
            {
                displayRect = value;
            }
        }

        private Rectangle frameRect;

        public Rectangle FrameRect
        {
            get
            {
                return frameRect;
            }

            set
            {
                frameRect = value;
            }
        }

        private GraphicMarginInfo margin = new GraphicMarginInfo(0, 20, 60, 0);

        public GraphicMarginInfo MarginInfo
        {
            get { return margin; }
            set { margin = value; }
        }

        private GraphicPaddingInfo padding = new GraphicPaddingInfo(0, 0, 0, 0);

        public GraphicPaddingInfo Padding
        {
            get
            {
                return padding;
            }
            set
            {
                padding = value;
            }
        }
        public void SetDrawRect(Rectangle rect)
        {
            frameRect = new Rectangle(rect.X + margin.MarginLeft, rect.Y + margin.MarginTop, rect.Width - margin.MarginLeft - margin.MarginRight, rect.Height - margin.MarginTop - margin.MarginBottom);
            displayRect = new Rectangle(frameRect.X + padding.PaddingLeft, frameRect.Y + padding.PaddingTop, frameRect.Width - padding.PaddingLeft - padding.PaddingRight, frameRect.Height - padding.PaddingTop - padding.PaddingBottom);
        }

        private ColorConfig colorConfig = new ColorConfig();

        public ColorConfig ColorConfig
        {
            get
            {
                return colorConfig;
            }

            set
            {
                colorConfig = value;
            }
        }
        #endregion

        #region 绑定控件

        internal Control control;

        public virtual void BindControl(Control control)
        {
            if (this.control != null)
                UnBindControl();
            this.control = control;
            control.Paint += Control_Paint;
            control.SizeChanged += Control_SizeChanged;
        }

        public virtual void UnBindControl()
        {
            this.control.Paint -= Control_Paint;
            this.control.SizeChanged -= Control_SizeChanged;
            this.control = null;
        }

        private void Control_SizeChanged(object sender, EventArgs e)
        {
            DrawGraphBind();
        }

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            DrawGraphBind();
        }

        private void DrawGraphBind()
        {
            if (control == null)
                return;
            Rectangle rect = control.DisplayRectangle;
            SetDrawRect(rect);
            DrawGraph();
        }

        #endregion

        #region 画图

        private Object drawObj = new Object();
        private Boolean drawing = false;

        public void DrawGraph()
        {
            if (drawing)
                return;
            DrawGraphSync();
        }

        private void DrawGraphSync()
        {
            lock (drawObj)
            {
                try
                {
                    drawing = true;
                    BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
                    BufferedGraphics myBuffer = currentContext.Allocate(control.CreateGraphics(), control.DisplayRectangle);
                    Graphics g = myBuffer.Graphics;

                    DrawGraph(g);
                    if (AfterGraphicDraw != null)
                        AfterGraphicDraw(this, new GraphicRefreshArgs(g));

                    myBuffer.Render();
                    myBuffer.Dispose();
                    drawing = false;
                }
                finally
                {
                    drawing = false;
                }
            }
        }

        public abstract void DrawGraph(Graphics graphic);

        public event AfterGraphicDrawHandler AfterGraphicDraw;

        #endregion
    }

    public delegate void AfterGraphicDrawHandler(object sender, GraphicRefreshArgs e);

    public class GraphicRefreshArgs
    {
        public Graphics Graphic;

        public GraphicRefreshArgs(Graphics Graphic)
        {
            this.Graphic = Graphic;
        }
    }
}