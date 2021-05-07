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

		void ShowError(string message)
		{
			Background = new SolidColorBrush(Color.FromRgb(255, 125, 125));
			Title = message;
		}
		void AllGood()
		{
			Background = new SolidColorBrush(Colors.White);
			Title = "All Good!";
		}

		private void tbxNumber1_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				// Here...
				if (tbxNumber1.Text == "")
				{
					AllGood();  // No need to show errors for empty values!
					Title = "Um, but we still need a number for tbxNumber1...";
					tbxNumber1.Background = new SolidColorBrush(Color.FromRgb(255, 255, 201));
					return;
				}
				tbxNumber1.Background = new SolidColorBrush(Colors.White);

				SuperNumber superNumber = tbxNumber1.Text.ToNum();
				AllGood();
			}
			catch (Exception ex)
			{
				ShowError(ex.Message);
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
