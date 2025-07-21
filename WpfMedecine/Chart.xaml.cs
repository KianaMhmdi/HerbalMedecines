
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Windows;

using LiveCharts;
using LiveCharts.Wpf;
using WpfMedecine.Data;
using System.Windows.Controls;
using System.Diagnostics;

namespace WpfMedecine
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    /// 
   
    public partial class Chart : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        OwnerPanel o = new OwnerPanel();
        
        public Chart()
        {
            InitializeComponent();


           //CreatChart(o.ProductSelesReportGrid);
            DataContext = this;
            
        }
        public void CreatChart(DataTable d)
        {
            List<string> names = new List<string>();
            List<double> salesCount = new List<double>();


           /* foreach(DataRow row in d.Rows)
            {

                
                    
               
                    names.Add(row["medincineName"].ToString());

               // salesCount.Add(Convert.ToDouble(float.Parse(row["total"].ToString())));
            }*/
           
           
            string messgee = string.Join(",", names);
            MessageBox.Show(messgee, "name");
            names.Add("ooo");
            names.Add("lll");
            salesCount.Add(4);
            salesCount.Add(3);
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="فروش",
                    Values=new ChartValues<double>(salesCount)

                }
            };

            Labels = names.ToArray();
            DataContext = this;
        }
        public class SalesProduct
        {
            public string medincineName { get; set; }
            public double total { get; set; }
        }
        
    }
}
