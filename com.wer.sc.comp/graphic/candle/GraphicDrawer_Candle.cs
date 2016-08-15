using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDrawer_Candle : GraphicDrawer_Compound
    {
        private GraphicDataProvider dataProvider;

        public GraphicDataProvider DataProvider
        {
            get
            {
                return dataProvider;
            }

            set
            {
                dataProvider = value;
                drawer_chart.DataProvider = value;
                drawer_mount.DataProvider = value;
            }
        }
        private int selectIndex;
        public int SelectIndex
        {
            get
            {
                return selectIndex;
            }

            set
            {
                selectIndex = value;
            }
        }

        public GraphicDrawer_CandleChart drawer_chart;

        internal GraphicDrawer_CandleMount drawer_mount;

        public GraphicDrawer_Candle()
        {
            this.MarginInfo = new GraphicMarginInfo(0, 20, 20, 20);
            this.Padding = new GraphicPaddingInfo(80, 0, 0, 0);
            this.drawer_chart = new GraphicDrawer_CandleChart();
            this.drawer_chart.MarginInfo = new GraphicMarginInfo(0, 0, 0, 1);
            this.drawer_chart.Padding = new GraphicPaddingInfo(0, 20, 50, 0);

            this.drawer_mount = new GraphicDrawer_CandleMount();
            this.drawer_mount.MarginInfo = new GraphicMarginInfo(0, 0, 0, 0);
            this.drawer_mount.Padding = new GraphicPaddingInfo(0, 0, 50, 0);
            this.AddGraph(drawer_chart, 0.7f);
            this.AddGraph(drawer_mount, 0.3f);

            crossHairDrawer = new CrossHairDrawer();
        }

        CrossHairDrawer crossHairDrawer;

        public override void BindControl(Control control)
        {
            base.BindControl(control);
            CrossHairDataPrivider_Candle crossHairProvider = new CrossHairDataPrivider_Candle(this);
            crossHairDrawer.Bind(crossHairProvider);
        }

        public override void UnBindControl()
        {
            base.UnBindControl();
        }

        public override void DrawGraph(Graphics graphic)
        {
            base.DrawGraph(graphic);
            DrawSelectBlock(graphic);
        }

        public void DrawSelectBlock(Graphics g)
        {
            if (selectIndex < 0)
                return;
            SelectedPointInfo blockInfo = GetBlockInfo(selectIndex);
            if (blockInfo == null)
                return;
            Point p = blockInfo.StartPoint;
            int blockWidth = CalcBlockInfoWidth(blockInfo);
            int blockHeight = CalcBlockInfoHeight(blockInfo);
            g.FillRectangle(new SolidBrush(Color.Black), p.X, p.Y, blockWidth, blockHeight);
            g.DrawRectangle(ColorConfig.Pen_CrossHair, p.X, p.Y, blockWidth, blockHeight);

            Point linePoint = p;
            linePoint.Y += blockInfo.Gap;
            for (int i = 0; i < blockInfo.Lines.Count; i++)
            {
                BlockLineInfo lineInfo = blockInfo.Lines[i];
                g.DrawString(lineInfo.Text, lineInfo.TextFont, lineInfo.TextBrush, linePoint);
                linePoint.Y += blockInfo.LineHeight;
            }
        }

        public SelectedPointInfo GetBlockInfo(int index)
        {
            IKLineData data = dataProvider.GetKLineData();
            KLineChart_KLineData chart = new KLineChart_KLineData(data, index);
            KLineChart_KLineData lastChart = new KLineChart_KLineData(data, index - 1);
            return GetBlockInfo(chart, lastChart);
        }

        private SelectedPointInfo GetBlockInfo(KLineChart_KLineData chart, KLineChart_KLineData lastChart)
        {
            SelectedPointInfo b = new SelectedPointInfo();
            b.LineHeight = 20;
            b.Width = 58;
            b.StartPoint = new Point(DisplayRect.X - b.Width, DisplayRect.Y);

            double lastEndPrice = lastChart != null ? lastChart.End : chart.Start;
            Pen pen = new Pen(Color.White);
            Brush brushNormal = new SolidBrush(Color.White);

            Font font = new Font("New Times Roman", 10, FontStyle.Regular);

            //int len = chart.Time.Length;
            b.Lines.Add(new BlockLineInfo(chart.Time.ToString(), brushNormal, font));
            b.Lines.Add(new BlockLineInfo("开盘", brushNormal, font));
            b.Lines.Add(new BlockLineInfo(chart.Start.ToString(), GetPriceBrush(chart.Start, lastEndPrice), font));
            b.Lines.Add(new BlockLineInfo("最高", brushNormal, font));
            b.Lines.Add(new BlockLineInfo(chart.High.ToString(), GetPriceBrush(chart.High, lastEndPrice), font));
            b.Lines.Add(new BlockLineInfo("最低", brushNormal, font));
            b.Lines.Add(new BlockLineInfo(chart.Low.ToString(), GetPriceBrush(chart.Low, lastEndPrice), font));
            b.Lines.Add(new BlockLineInfo("收盘", brushNormal, font));
            b.Lines.Add(new BlockLineInfo(chart.End.ToString(), GetPriceBrush(chart.End, lastEndPrice), font));

            double uprange = Math.Round(chart.End - lastEndPrice, 2);
            b.Lines.Add(new BlockLineInfo(uprange.ToString(), GetPriceBrush(uprange, 0), font));
            //涨幅
            //double uppercent = Math.Round((chart.EndPrice - lastEndPrice) / lastEndPrice,2);
            double uppercent = Math.Round(uprange / lastEndPrice * 100, 2);
            b.Lines.Add(new BlockLineInfo(uppercent.ToString(), GetPriceBrush(uppercent, 0), font));
            return b;
        }
        private Brush GetPriceBrush(double price, double referPrice)
        {
            Brush brushEarn = new SolidBrush(ColorUtils.GetColor("#CC0000"));
            Brush brushLose = new SolidBrush(ColorUtils.GetColor("#00CC00"));
            return price >= referPrice ? brushEarn : brushLose;
        }
        private int CalcBlockInfoHeight(SelectedPointInfo blockInfo)
        {
            if (blockInfo.Height > 0)
                return blockInfo.Height;
            return (int)(blockInfo.Lines.Count * blockInfo.LineHeight + blockInfo.Gap);
        }

        private int CalcBlockInfoWidth(SelectedPointInfo blockInfo)
        {
            if (blockInfo.Width > 0)
                return blockInfo.Width;
            return 20;
        }
    }
    /// <summary>
    /// 选中的信息
    /// </summary>
    public class SelectedPointInfo
    {
        private int height = -1;
        /// <summary>
        /// 如不设置，系统会自动计算
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int width = -1;
        /// <summary>
        /// 如不设置，系统会自动计算
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int gap;

        public int Gap
        {
            get { return gap; }
            set { gap = value; }
        }

        private int lineHeight;

        public int LineHeight
        {
            get { return lineHeight; }
            set { lineHeight = value; }
        }

        private Point startPoint;

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        private List<BlockLineInfo> lines = new List<BlockLineInfo>();

        public List<BlockLineInfo> Lines
        {
            get { return lines; }
        }
    }

    public class BlockLineInfo
    {
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        private Brush textBrush;

        public Brush TextBrush
        {
            get { return textBrush; }
            set { textBrush = value; }
        }

        private Font textFont;

        public Font TextFont
        {
            get { return textFont; }
            set { textFont = value; }
        }

        public BlockLineInfo()
        {
        }

        public BlockLineInfo(String text, Brush brush, Font font)
        {
            this.text = text;
            this.textBrush = brush;
            this.textFont = font;
        }
    }
    public class CrossHairDataPrivider_Candle : CrossHairDataPrivider
    {
        private GraphicDrawer_Candle drawer;

        public CrossHairDataPrivider_Candle(GraphicDrawer_Candle drawer)
        {
            this.drawer = drawer;
            drawer.AfterGraphicDraw += Drawer_AfterGraphicDraw;
        }

        private void Drawer_AfterGraphicDraw(object sender, GraphicRefreshArgs e)
        {
            if (this.AfterGraphicDraw != null)
                this.AfterGraphicDraw(sender, e);
        }

        public Control Control
        {
            get
            {
                return drawer.control;
            }
        }

        public Rectangle DrawRect
        {
            get
            {
                return drawer.DisplayRect;
            }
        }

        public Pen Pen
        {
            get
            {
                return drawer.drawer_chart.ColorConfig.Pen_CrossHair;
            }
        }

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                return drawer.drawer_chart.PriceMapping;
            }
        }

        public event AfterGraphicDrawHandler AfterGraphicDraw;

        public bool DoMoveNext()
        {
            int lastIndex = drawer.DataProvider.GetKLineData().Length - 1;
            if (this.drawer.drawer_chart.PriceMapping.PriceRect.PriceRight + 1 > lastIndex)
                return false;
            this.drawer.DataProvider.EndIndex++;
            return true;
        }

        public bool DoMovePrev()
        {
            if (this.drawer.drawer_chart.PriceMapping.PriceRect.PriceLeft - 1 < 0)
                return false;
            this.drawer.DataProvider.EndIndex--;
            return true;
        }

        public void DoRedraw()
        {
            this.drawer.DrawGraph();
        }

        public void DoSelectIndexChange(int index)
        {
            this.drawer.SelectIndex = index;
        }

        public Point GetCrossHairPoint(int selectIndex)
        {
            PriceGraphicMapping priceMapping = this.drawer.drawer_chart.PriceMapping;
            float x = priceMapping.CalcX(selectIndex);
            float y = priceMapping.CalcY(drawer.DataProvider.GetKLineData().Arr_End[selectIndex]);
            return new Point((int)x, (int)y);
        }

        public GraphicDataProvider GetDataProvider()
        {
            return this.drawer.DataProvider;
        }
    }
}