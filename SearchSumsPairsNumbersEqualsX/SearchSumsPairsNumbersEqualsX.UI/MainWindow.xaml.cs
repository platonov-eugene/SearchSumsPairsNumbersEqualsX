using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SearchSumsPairsNumbersEqualsX.UI
{
    /// <summary>
    /// Форма приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор формы приложения по умолчанию
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetImageOnHelpTestScenario(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;

            if (image.Source.ToString() == @"pack://application:,,,/SearchSumsPairsNumbersEqualsX.UI;component/Images/HelpButtonForMouseLeave.png")
                image.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseEnter.png", UriKind.Relative));
            else
                image.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseLeave.png", UriKind.Relative));
        }

        private void PreviewExecutedCommandCopyCutPaste(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste)
                e.Handled = true;
        }

        private void PreviewKeyDownToMatchNumberFormat(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void PreviewTextInputToMatchIntegerFormat(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            Regex regularExpression = new Regex(@"^[1-9]{1}[\d]*$");
            if (textBox.SelectionLength == 0)
                e.Handled = !regularExpression.IsMatch(textBox.Text + e.Text);
            else
                e.Handled = !regularExpression.IsMatch(textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength) + e.Text);
        }

        private void PreviewTextInputToMatchFloatFormat(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            Regex regularExpression = new Regex(@"^[\-]?[\d]*[\.]?[\d]*$");
            if (textBox.SelectionLength == 0)
                e.Handled = !regularExpression.IsMatch(textBox.Text + e.Text);
            else
                e.Handled = !regularExpression.IsMatch(textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength) + e.Text);

            if (!e.Handled && textBox.Text.Length != 0)
                if (textBox.Text + e.Text == "-.")
                {
                    textBox.Text = "-0.";
                    textBox.CaretIndex = textBox.Text.Length;
                    e.Handled = true;
                }
                else if ((textBox.Text == "0" || textBox.Text == "-0") && char.IsDigit(e.Text.Last()))
                {
                    textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1) + e.Text;
                    textBox.CaretIndex = textBox.Text.Length;
                    e.Handled = true;
                }
        }
    }
}
