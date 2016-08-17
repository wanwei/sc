﻿using com.wer.sc.data.update;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.check
{
    public partial class FrmDataNavigate : Form
    {
        private DataMgr providerDataMgr;

        public FrmDataNavigate()
        {
            InitializeComponent();
            this.providerDataMgr = new DataMgr();
            List<DataProviderWrap> providers = providerDataMgr.GetProviders();
            for (int i = 0; i < providers.Count; i++)
            {
                DataProviderWrap provider = providers[i];
                cbProvider.Items.Add(provider.GetName());
            }
            cbProvider.SelectedIndex = 0;

            cbPeriod.Items.Add(new CbItem(0, "秒"));
            cbPeriod.Items.Add(new CbItem(1, "分钟"));
            cbPeriod.Items.Add(new CbItem(2, "小时"));
            cbPeriod.Items.Add(new CbItem(3, "天"));
            cbPeriod.Items.Add(new CbItem(4, "周"));
            cbPeriod.SelectedIndex = 1;
        }

        private void btLoadMinuteKLineChart_Click(object sender, EventArgs e)
        {
            DataProviderWrap dataProvider = providerDataMgr.GetProvider(cbProvider.SelectedItem.ToString());
            DataReaderFactory fac = dataProvider.GetFactory();

            double currentTime = double.Parse(tbTime.Text);
            int date = (int)currentTime;

            String code = tbCode.Text;

            IKLineData minuteKLineData = fac.KLineDataReader.GetData(code, date, date, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            TickData tickData = fac.TickDataReader.GetTickData(code, date);

            KLineChartBuilder_FromTick builder = new KLineChartBuilder_FromTick(minuteKLineData, tickData, currentTime);
            KLineChart chart = builder.GetCurrentChart();

            tbData.Clear();
            tbData.Text = chart.ToString();
        }

        private void btIndeier_Click(object sender, EventArgs e)
        {
            double time = double.Parse(tbTime.Text);
            int date = (int)time;
            if (time - date > 0.18)
                date += 1;

            DataProviderWrap dataProvider = providerDataMgr.GetProvider(cbProvider.SelectedItem.ToString());
            DataReaderFactory fac = dataProvider.GetFactory();

            String code = tbCode.Text;
            IKLineData minuteKLineData = fac.KLineDataReader.GetData(code, date, date, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            TickData tickData = fac.TickDataReader.GetTickData(code, date);

            TickDataIndeier indeier = new TickDataIndeier(tickData, minuteKLineData);
            int index = indeier.GetTickIndex(time);

            tbData.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                sb.Append(tickData.ToString()).Append("\r\n");
            }

            for (int i = 0; i < minuteKLineData.Length; i++)
            {
                //sb.Append(splits[i]).Append("\r\n");
                double t = minuteKLineData.Arr_Time[i];
                sb.Append(i + ":" + t + "," + indeier.GetTickSplitIndex(i) + "," + indeier.GetTickSplitIndex(t)).Append("\r\n");
            }
            tbData.AppendText(sb.ToString());
            tbData.AppendText(index.ToString() + "\r\n");
        }

        private void btLoadCurrentKLineChart_Click(object sender, EventArgs e)
        {
            String code = tbCode.Text;
            int start = int.Parse(tbStart.Text);
            int end = int.Parse(tbEnd.Text);
            KLinePeriod period = new KLinePeriod(cbPeriod.SelectedIndex, int.Parse(tbPeriod.Text));

            double time = double.Parse(tbTime.Text);
            int date = (int)time;
            if (time - date > 0.18)
                date += 1;

            DataProviderWrap dataProvider = providerDataMgr.GetProvider(cbProvider.SelectedItem.ToString());
            DataReaderFactory fac = dataProvider.GetFactory();
            IKLineData klineData = fac.KLineDataReader.GetData(code, start, end, period);
            IKLineData minuteKLineData = fac.KLineDataReader.GetData(code, date, date, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            TickData tickData = fac.TickDataReader.GetTickData(code, date);

            CurrentKLineChartBuilder builder = new CurrentKLineChartBuilder(klineData, minuteKLineData, tickData, time);
            IKLineChart chart = builder.GetCurrentChart();

            tbData.Clear();
            tbData.AppendText(chart.ToString());
        }

        private void btLoadAll_Click(object sender, EventArgs e)
        {
            String code = tbCode.Text;
            int start = int.Parse(tbStart.Text);
            int end = int.Parse(tbEnd.Text);
            KLinePeriod period = new KLinePeriod(cbPeriod.SelectedIndex, int.Parse(tbPeriod.Text));

            double time = double.Parse(tbTime.Text);
            int date = (int)time;
            if (time - date > 0.18)
                date += 1;

            DataProviderWrap dataProvider = providerDataMgr.GetProvider(cbProvider.SelectedItem.ToString());
            DataReaderFactory fac = dataProvider.GetFactory();
            DataNavigate navigate = new DataNavigate(fac);

            

            //IKLineData klineData = fac.KLineDataReader.GetData(code, start, end, period);
            //IKLineData minuteKLineData = fac.KLineDataReader.GetData(code, date, date, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            //TickData tickData = fac.TickDataReader.GetTickData(code, date);

            //CurrentKLineChartBuilder builder = new CurrentKLineChartBuilder(klineData, minuteKLineData, tickData, time);
            //IKLineChart chart = builder.GetCurrentChart();

            navigate.Change(code, time, period);

            IKLineData data = navigate.CurrentKLineData;

            tbData.Clear();
            //tbData.AppendText(chart.ToString());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= navigate.CurrentIndex; i++)
            {
                data.BarPos = i;
                sb.Append(data).Append("\r\n");                
            }
            tbData.AppendText(sb.ToString());
        }
    }
}
