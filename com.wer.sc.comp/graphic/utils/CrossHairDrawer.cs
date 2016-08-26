using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic.utils
{
    /// <summary>
    /// 十字线
    /// 1.点击屏幕出现十字线
    /// 2.十字线选中的block或者real可以显示详细信息
    /// 3.点左右按钮可以移动十字线
    /// 4.如果在最右端，继续右移整个k线向右移动；向左同理
    /// 5.有十字线的时候再点击一次十字线消失
    /// </summary>
    public class CrossHairDrawer
    {
        private bool showCrossHair;

        private Control control;

        private CrossHairDataPrivider provider;

        private Point crossPoint;

        private bool enable = true;

        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        public bool ShowCrossHair
        {
            get
            {
                return showCrossHair;
            }

            set
            {
                showCrossHair = value;
                //ShowCursor(!value);
            }
        }

        public Point CrossPoint
        {
            get
            {
                return crossPoint;
            }
        }

        //public CrossHairDataPrivider Provider
        //{
        //    get
        //    {
        //        return provider;
        //    }

        //    set
        //    {
        //        provider = value;
        //    }
        //}

        public CrossHairDrawer()
        {
        }

        public void Bind(CrossHairDataPrivider provider)
        {
            if (this.provider != null)
                UnBind();
            this.provider = provider;
            this.control = provider.Control;
            this.control.MouseUp += Control_MouseUp;
            this.control.MouseMove += Control_MouseMove;
            this.control.PreviewKeyDown += Control_PreviewKeyDown;
            //this.provider.AfterGraphicDraw += Provider_AfterGraphicDraw;
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!enable)
                return;
            if (ShowCrossHair)
            {
                if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                    return;
                if (e.KeyCode == Keys.Left)
                {
                    if (this.selectIndex == 0)
                        return;
                    this.selectIndex--;
                    if (this.selectIndex < provider.PriceMapping.PriceRect.PriceLeft)
                    {
                        bool isSuccess = provider.DoMovePrev();
                        if (!isSuccess)
                        {
                            this.selectIndex++;
                            return;
                        }
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    this.selectIndex++;
                    if (this.selectIndex > provider.PriceMapping.PriceRect.PriceRight)
                    {
                        bool isSuccess = provider.DoMoveNext();
                        if (!isSuccess)
                        {
                            this.selectIndex--;
                            return;
                        }
                    }
                }
                provider.DoSelectIndexChange(this.selectIndex);
                crossPoint = provider.GetCrossHairPoint(this.selectIndex);
                provider.DoRedraw();
            }
        }

        private void Provider_AfterGraphicDraw(object sender, GraphicRefreshArgs e)
        {
            //DrawGraphic(e.Graphic);
        }

        public void UnBind()
        {
            if (this.provider == null)
                return;
            this.control.MouseUp -= Control_MouseUp;
            this.control.MouseMove -= Control_MouseMove;
            this.control = null;
        }

        /// <summary>
        /// 设置鼠标状态的计数器（非状态）
        /// </summary>
        /// <param name="bShow">状态</param>
        /// <returns>状态技术</returns>
        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        public static extern int ShowCursor(bool bShow);
       

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (!enable)
                return;
            if (e.Button == MouseButtons.Left)
            {
                Point p = e.Location;
                Rectangle rec = provider.DrawRect;                
                if (!rec.Contains(p)) {
                    return;
                }
                
                this.ShowCrossHair = !ShowCrossHair;
                if (!this.ShowCrossHair)
                {
                    this.selectIndex = -1;
                    provider.DoSelectIndexChange(selectIndex);
                }
                provider.DoRedraw();
            }
        }

        private DateTime lastMouseMoveTime = DateTime.Now;
        private int selectIndex;


        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (!enable)
                return;
            if (ShowCrossHair)
            {
                if (lastMouseMoveTime.AddTicks(50) < DateTime.Now)
                {
                    ChangeCrossPoint(GetMousePosition());                    
                }
            }
            else
            {
                this.selectIndex = -1;
                provider.DoSelectIndexChange(this.selectIndex);
            }
        }

        public void ChangeCrossPoint(Point point)
        {
            this.crossPoint = point;
            if (!provider.DrawRect.Contains(point.X, point.Y))
            {
                ShowCrossHair = false;
                provider.DoSelectIndexChange(-1);
                lastMouseMoveTime = DateTime.Now;
                provider.DoRedraw();
                return;
            }
            int index = (int)provider.PriceMapping.CalcPriceX(point.X);
            int[] range = provider.IndexRange;
            if (index >= range[0] && index <= range[1])
            {
                //if(index>provider.)
                if (this.selectIndex != index)
                {
                    this.selectIndex = index;
                    provider.DoSelectIndexChange(this.selectIndex);
                }
            }
            //如果selectIndex<0，说明鼠标移出了该控件，此时应该不需要显示十字线了
            if (this.selectIndex < 0)
                ShowCrossHair = false;
            lastMouseMoveTime = DateTime.Now;
            provider.DoRedraw();
        }

        private Point GetMousePosition()
        {
            int x = control.PointToClient(Control.MousePosition).X;
            int y = control.PointToClient(Control.MousePosition).Y;
            return new Point(x, y);
        }

        public void DrawGraphic(Graphics g)
        {
            if (!enable)
                return;
            if (ShowCrossHair)
            {
                Pen pen = provider.Pen;
                Rectangle rec = provider.DrawRect;                
                g.DrawLine(pen, new Point(crossPoint.X, rec.Top), new Point(crossPoint.X, rec.Bottom));
                g.DrawLine(pen, new Point(rec.X, crossPoint.Y), new Point(rec.Right, crossPoint.Y));

                DrawCrossHairsPrice(g, crossPoint, rec);
            }
        }

        private void DrawCrossHairsPrice(Graphics g, Point point, Rectangle rec)
        {
            //rec.Right

        }
    }

    /// <summary>
    /// 交互：
    /// 1.能够
    /// 2.
    /// </summary>
    public interface CrossHairDataPrivider
    {
        /// <summary>
        /// 得到要画的控件
        /// </summary>
        Control Control { get; }

        /// <summary>
        /// 得到画十字线的边界
        /// </summary>
        Rectangle DrawRect { get; }

        /// <summary>
        /// 得到十字线的画笔
        /// </summary>
        Pen Pen { get; }

        /// <summary>
        /// 画图事件，十字线画图器可以侦听画图，画完图后再画十字线
        /// </summary>
        //event AfterGraphicDrawHandler AfterGraphicDraw;

        /// <summary>
        /// 获得
        /// </summary>
        PriceGraphicMapping PriceMapping { get; }

        /// <summary>
        /// 得到每个index的交叉点位置
        /// </summary>
        /// <param name="selectIndex"></param>
        /// <returns></returns>
        Point GetCrossHairPoint(int selectIndex);

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="index"></param>
        void DoSelectIndexChange(int index);

        void DoRedraw();

        bool DoMoveNext();

        bool DoMovePrev();

        //GraphicDataProvider_Candle GetDataProvider();
        int[] IndexRange { get; }
    }
}