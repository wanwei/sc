using com.wer.sc.comp.graphic.real;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDataProvider_Main
    {
        private DataReaderFactory dataReaderFac;
        private IDataNavigate dataNavigate;

        private GraphicDataProvider_CandleNav dataProvider_Candle;
        private IGraphicDataProvider_Real dataProvider_Real;

        public GraphicDataProvider_Main(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
            this.DataNavigate = new DataNavigate(dataReaderFac);

            this.dataProvider_Candle = new GraphicDataProvider_CandleNav(dataReaderFac,DataNavigate);
            this.dataProvider_Real = new GraphicDataProvider_RealNav(dataReaderFac, DataNavigate);
        }

        public IGraphicDataProvider_Candle DataProvider_Candle
        {
            get { return dataProvider_Candle; }
        }

        public IGraphicDataProvider_Real DataProvider_Real
        {
            get { return dataProvider_Real; }
        }

        public IDataNavigate DataNavigate
        {
            get
            {
                return dataNavigate;
            }

            set
            {
                dataNavigate = value;
            }
        }
    }
}
