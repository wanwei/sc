using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.utils
{
    /// <summary>
    /// 图像边界
    /// </summary>
    public class PriceGraphicMapping
    {
        public PriceRectangle PriceRect;

        public Rectangle DrawRect;

        public PointF CalcPoint(PricePoint pricePoint)
        {
            return new PointF(CalcX(pricePoint.X), CalcY(pricePoint.Y));
        }

        /// <summary>
        /// 得到index所在的位置
        /// 如果是柱状图得到块的中间位置
        /// 如果是线图获得连接线的点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float CalcX(float priceX)
        {
            if (PriceRect.PriceWidth == 0)
                return DrawRect.X;
            float percentX = (priceX - PriceRect.PriceLeft) / PriceRect.PriceWidth;
            return DrawRect.Left + DrawRect.Width * percentX;
        }

        /// <summary>
        /// 该方法用来确定一个价格的y轴坐标，供子类调用
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public float CalcY(float priceY)
        {
            if (PriceRect.PriceHeight == 0)
                return DrawRect.Y;
            float percentY = (PriceRect.PriceTop - priceY) / PriceRect.PriceHeight;
            return DrawRect.Top + DrawRect.Height * percentY;
        }

        public float PriceWidth
        {
            get
            {
                return DrawRect.Width / PriceRect.PriceWidth;
            }
        }

        public float PriceHeight
        {
            get
            {
                return DrawRect.Height / PriceRect.PriceHeight;
            }
        }

        public float CalcPriceX(float x)
        {
            float distance = x - DrawRect.X;
            return distance / PriceWidth + PriceRect.PriceLeft;
        }

        public float CalcPriceY(float y)
        {
            float distance = y - DrawRect.Y;
            return distance / PriceHeight + PriceRect.PriceTop;
        }
    }
}