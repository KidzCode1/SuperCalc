using SuperCalcCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperCalc
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void tbxNumber1_TextChanged(object sender, TextChangedEventArgs e)
		{
			DecimalFractionator decimalFractionator = DecimalFractionator.Create(tbxNumber1.Text);
			if (decimalFractionator != null)
			{

				tbxWhole1.Text = decimalFractionator.wholeNumber.ToString();
				tbxNumerator1.Text = decimalFractionator.numerator.ToString();
				tbxDenominator1.Text = decimalFractionator.denominator.ToString();
			}
		}

		private void tbxNumber2_TextChanged(object sender, TextChangedEventArgs e)
		{
			DecimalFractionator decimalFractionator = DecimalFractionator.Create(tbxNumber2.Text);
			if (decimalFractionator != null)
			{

				tbxWhole2.Text = decimalFractionator.wholeNumber.ToString();
				tbxNumerator2.Text = decimalFractionator.numerator.ToString();
				tbxDenominator2.Text = decimalFractionator.denominator.ToString();
			}
		}
	}
}
