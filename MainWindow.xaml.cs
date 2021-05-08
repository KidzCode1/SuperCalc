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

		void SetAnswer(string answer)
		{
			lblAnswer.Content = answer;
		}

		private void tbxEquation_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				// Here...
				if (tbxEquation.Text == "")
				{
					AllGood();  // No need to show errors for empty values!
					SetAnswer("Enter an equation above!");
					tbxEquation.Background = new SolidColorBrush(Color.FromRgb(255, 255, 201));
					return;
				}
				tbxEquation.Background = new SolidColorBrush(Colors.White);

				SuperNumber superNumber = tbxEquation.Text.ToNum();
				SetAnswer(superNumber.DisplayStr);
				AllGood();
			}
			catch (Exception ex)
			{
				ShowError(ex.Message);
			}
		}

		private void btnCopyAnswer_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(lblAnswer.Content as string);
		}

		private void btnCreateTestCase_Click(object sender, RoutedEventArgs e)
		{
			string testCode = $"Assert.AreEqual(\"{lblAnswer.Content}\", \"{tbxEquation.Text}\".ToNum());";
			Clipboard.SetText(testCode);
			MessageBox.Show(testCode, "Copied To The Clipboard");
		}
	}
}
