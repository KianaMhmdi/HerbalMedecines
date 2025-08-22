using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using WpfMedecine.Data;

namespace WpfMedecine
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class Chart : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }

        public Chart(DateTime start, DateTime end)
        {
            InitializeComponent();
            DataContext = this;
            LoadChartData(start, end);
        }

        private void LoadChartData(DateTime start, DateTime end)
        {
            // دریافت داده‌ها از متد موجود
            HerbalDB db = new HerbalDB(); // ایجاد instance با new
            DataTable salesData = db.GetProductSeleareport(start, end);

            // آماده‌سازی داده‌ها برای نمودار
            var values = new ChartValues<int>();
            Labels = new List<string>();

            foreach (DataRow row in salesData.Rows)
            {
                values.Add(Convert.ToInt32(row["total"]));
                Labels.Add(row["medincineName"].ToString());
            }

            // ایجاد سری داده‌ها
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "تعداد فروش",
                    Values = values,
                    DataLabels = true
                }
            };
        }
    }
}