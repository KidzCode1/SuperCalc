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
				SetAnswerDecimal(superNumber.DisplayDecimalStr);
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
			//⁰¹²³⁴⁵⁶⁷⁸⁹ˉ ^ 456  ⁴⁵⁶ 
		}

		void InsertPower(string str)
		{
			int selectionStart = tbxEquation.SelectionStart;
			string equationText = tbxEquation.Text;
			// Insert the str into equationText, at the selectionStart position.
			//string firstPart = equationText.Substring(0, selectionStart);
			//string secondPart = equationText.Substring(selectionStart);
			//string newText = firstPart + str + secondPart;
			tbxEquation.Text = tbxEquation.Text.Insert(selectionStart, str);
			tbxEquation.SelectionStart = selectionStart + 1;
		}

		private void tbxEquation_KeyDown(object sender, KeyEventArgs e)
		{
			if (SuperscriptKeyPressed(e))
			{
				EnterSuperScriptMode();
				e.Handled = true;
				return;
			}
			if (e.Key == Key.Space ||
				e.Key == Key.Add || e.Key == Key.OemPlus ||
				e.Key == Key.Multiply || (e.Key == Key.D8 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift) ||
				e.Key == Key.Divide || e.Key == Key.Oem2)
			{
				ExitSuperScriptMode();
				e.Handled = false;
				return;
			}
			if (e.Key == Key.Enter || e.Key == Key.Escape)
			{
				ExitSuperScriptMode();
				e.Handled = true;
				return;
			}
			if (inSuperScriptMode)
			{
				switch (e.Key)
				{
					case Key.D0:
					case Key.NumPad0:
						InsertPower("⁰");
						break;
					case Key.D1:
					case Key.NumPad1:
						InsertPower("¹");
						break;
					case Key.D2:
					case Key.NumPad2:
						InsertPower("²");
						break;
					case Key.D3:
					case Key.NumPad3:
						InsertPower("³");
						break;
					case Key.D4:
					case Key.NumPad4:
						InsertPower("⁴");
						break;
					case Key.D5:
					case Key.NumPad5:
						InsertPower("⁵");
						break;
					case Key.D6:
					case Key.NumPad6:
						InsertPower("⁶");
						break;
					case Key.D7:
					case Key.NumPad7:
						InsertPower("⁷");
						break;
					case Key.D8:
					case Key.NumPad8:
						InsertPower("⁸");
						break;
					case Key.D9:
					case Key.NumPad9:
						InsertPower("⁹");
						break;
					case Key.Subtract:
					case Key.OemMinus:
						InsertPower("ˉ");
						break;
					default:
						return;
				}
				e.Handled = true;  // Ignore the key.
			}
		}

		private static bool SuperscriptKeyPressed(KeyEventArgs e)
		{
			return e.Key == Key.D6 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
		}
	}
}