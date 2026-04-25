using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CalculatorWPF
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private double ConvertAngle(double angle)
        {
            if (DegMode.IsChecked == true)
                return angle * Math.PI / 180;

            return angle;
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (Display.Text == "0")
                Display.Text = btn.Content.ToString();
            else
                Display.Text += btn.Content.ToString();
        }

        private string ReplaceTrig(string expr)
        {
            expr = Regex.Replace(expr, @"sin\((.*?)\)", m =>
            {
                double val = Convert.ToDouble(new DataTable().Compute(m.Groups[1].Value, null));
                return Math.Sin(ConvertAngle(val)).ToString();
            });

            expr = Regex.Replace(expr, @"cos\((.*?)\)", m =>
            {
                double val = Convert.ToDouble(m.Groups[1].Value);
                return Math.Cos(ConvertAngle(val)).ToString();
            });

            expr = Regex.Replace(expr, @"tg\((.*?)\)", m =>
            {
                double val = Convert.ToDouble(m.Groups[1].Value);
                return Math.Tan(ConvertAngle(val)).ToString();
            });
            expr = Regex.Replace(expr, @"sqrt\((.*?)\)", m =>
            {
                double val = Convert.ToDouble(m.Groups[1].Value);
                return Math.Sqrt(val).ToString();
            });
            return expr;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            Display.Text += "sqrt(";
        }

        private void Sin_Click(object sender, RoutedEventArgs e)
        {
            Display.Text += "sin(";
        }

        private void Cos_Click(object sender, RoutedEventArgs e)
        {
            Display.Text += "cos(";
        }

        private void Tan_Click(object sender, RoutedEventArgs e)
        {
            Display.Text += "tg(";
        }
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string expr = Display.Text;

                expr = expr.Replace(",", ".");

                var result = new DataTable().Compute(expr, null);
                Display.Text = result.ToString();
            }
            catch
            {
                Display.Text = "Ошибка";
            }
        }
        private void Expression_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;

            if (Display.Text == "0")
                Display.Text = btn.Content.ToString();
            else
                Display.Text += btn.Content.ToString();
        }
    }
}