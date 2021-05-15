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

		void SetAnswerDecimal(string answer)
		{
			lblAnswerDecimal.Content = answer;
		}

		void SetAnswerImproperFraction(string answer)
		{
			lblAnswerImproperFraction.Content = answer;
		}
		void SetAnswerMixedFraction(string answer)
		{
			lblAnswerMixedNumberFraction.Content = answer;
		}

		private void tbxEquation_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				// Here...
				if (tbxEquation.Text == "")
				{
					AllGood();  // No need to show errors for empty values!
					SetAnswerDecimal("Enter an equation above!");
					tbxEquation.Background = new SolidColorBrush(Color.FromRgb(255, 255, 201));
					return;
				}
				tbxEquation.Background = new SolidColorBrush(Colors.White);

				SuperNumber superNumber = tbxEquation.Text.ToNum();
				SetAnswerDecimal(superNumber.DisplayStr);
				SetAnswerImproperFraction(superNumber.ImproperFractionStr);
				SetAnswerMixedFraction(superNumber.MixedNumberFractionStr);
				AllGood();
			}
			catch (Exception ex)
			{
				ShowError(ex.Message);
			}
		}

		private void btnCopyAnswer_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(lblAnswerDecimal.Content as string);
		}

		private void btnCreateTestCase_Click(object sender, RoutedEventArgs e)
		{
			string testCode = $"Assert.AreEqual(\"{lblAnswerDecimal.Content}\", \"{tbxEquation.Text}\".ToNum());";
			CopyToClipboard(testCode);
		}

		private static void CopyToClipboard(string testCode)
		{
			Clipboard.SetText(testCode);
			MessageBox.Show(testCode, "Copied To The Clipboard");
		}

		private void lblAnswerMixedNumberFraction_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CopyToClipboard(lblAnswerMixedNumberFraction.Content.ToString());
		}

		private void lblAnswerImproperFraction_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CopyToClipboard(lblAnswerImproperFraction.Content.ToString()); 
		}

		private void lblAnswerDecimal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CopyToClipboard(lblAnswerDecimal.Content.ToString());
		}

		bool inSuperScriptMode;
		void EnterSuperScriptMode()
		{
			if (inSuperScriptMode)
				return;

			inSuperScriptMode = true;
			bdrEquation.BorderBrush = new SolidColorBrush(Color.FromRgb(181, 255, 211));
			tbOverTheTopSuperScriptExplosion.Text = "Boom! In superscript mode, suckah!";
			tbOverTheTopSuperScriptExplosion.Visibility = Visibility.Visible;
		}

		void ExitSuperScriptMode()
		{
			if (!inSuperScriptMode)
				return;
			inSuperScriptMode = false;
			bdrEquation.BorderBrush = new SolidColorBrush(Colors.White);
			tbOverTheTopSuperScriptExplosion.Text = "";
			tbOverTheTopSuperScriptExplosion.Visibility = Visibility.Collapsed;
		}

		private void tbxEquation_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.D6 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
			{
				EnterSuperScriptMode();
				e.Handled = true;
				return;
			}
			if (e.Key == Key.Enter)
			{
				ExitSuperScriptMode();
				e.Handled = true;
				return;
			}
		}
	}
}
