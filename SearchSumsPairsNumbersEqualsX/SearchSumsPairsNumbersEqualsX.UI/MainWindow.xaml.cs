using System;
using System.Collections.Generic;
using System.Globalization;
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
using SearchSumsPairsNumbersEqualsX.Logic;

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
            textboxSourceCollectionOfNumbers.Focus();
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
            if ((e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down) && Keyboard.Modifiers == ModifierKeys.Shift)
                e.Handled = true;
        }

        private void PreviewKeyDownToMatchMultipleNumberFormat(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                if ((sender as TextBox).Text == String.Empty || (sender as TextBox).Text.Last() == ' ')
                    e.Handled = true;
            if ((e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down) && Keyboard.Modifiers == ModifierKeys.Shift)
                e.Handled = true;

            AddingFinalZeroInMultipleNumberFormat(sender, null);
        }

        private void PreviewTextInputToMatchIntegerFormat(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            Regex regularExpression = new Regex(@"^[1-9]{1}[\d]*$");
            e.Handled = !regularExpression.IsMatch(textBox.Text + e.Text);
        }

        private void PreviewTextInputToMatchFloatFormat(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            Regex regularExpression = new Regex(@"^[\-]?[\d]*[\.]?[\d]*$");
            e.Handled = !regularExpression.IsMatch(textBox.Text + e.Text);

            if (!e.Handled)
                if (textBox.Text + e.Text == "-.")
                {
                    textBox.Text = "-0.";
                    textBox.CaretIndex = textBox.Text.Length;
                    e.Handled = true;
                }
                else if (textBox.Text + e.Text == ".")
                {
                    textBox.Text = "0.";
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

        private void PreviewTextInputToMatchMultipleFloatFormat(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            string enteredTextNumber = string.Empty;
            int indexLastWhitespace = textBox.Text.LastIndexOf(" ");

            if (indexLastWhitespace == -1)
                enteredTextNumber = textBox.Text;
            else if (indexLastWhitespace < textBox.Text.Length - 1)
                enteredTextNumber = textBox.Text.Substring(indexLastWhitespace + 1, textBox.Text.Length - indexLastWhitespace - 1);

            if (enteredTextNumber.Length == 9)
            {
                e.Handled = true;
                return;
            }

            Regex regularExpression = new Regex(@"^[\-]?[\d]*[\.]?[\d]*$");
            e.Handled = !regularExpression.IsMatch(enteredTextNumber + e.Text);

            if (!e.Handled)
                if (enteredTextNumber + e.Text == "-.")
                {
                    textBox.Text = textBox.Text.Remove(indexLastWhitespace + 1) + "-0.";
                    textBox.CaretIndex = textBox.Text.Length;
                    e.Handled = true;
                }
                else if (enteredTextNumber + e.Text == ".")
                {
                    textBox.Text = "0.";
                    textBox.CaretIndex = textBox.Text.Length;
                    e.Handled = true;
                }
                else if ((enteredTextNumber == "0" || enteredTextNumber == "-0") && char.IsDigit(e.Text.Last()))
                {
                    enteredTextNumber = enteredTextNumber.Substring(0, enteredTextNumber.Length - 1) + e.Text;
                    textBox.Text = textBox.Text.Remove(indexLastWhitespace + 1) + enteredTextNumber;
                    textBox.CaretIndex = textBox.Text.Length;
                    e.Handled = true;
                }

            CountNumbersInSourceCollection(sender, e.Handled ? string.Empty : e.Text);
        }

        private void CountNumbersInSourceCollection(object sender, string inputText = "")
        {
            labelCountNumbersInSourceCollection.Content = ((sender as TextBox).Text + inputText).Split(' ').Where(item => item != string.Empty).Count();
        }

        private void CountNumbersInSourceCollectionByKeyUp(object sender, KeyEventArgs e)
        {
            CountNumbersInSourceCollection(sender);
        }

        private void DisableSelectionTextInTextBoxes(object sender, MouseEventArgs e)
        {
            textboxSourceCollectionOfNumbers.SelectionLength = 0;
            textboxMinimumRandomValue.SelectionLength = 0;
            textboxMaximumRandomValue.SelectionLength = 0;
            textboxCountNumbersInRandomCollection.SelectionLength = 0;
            textboxPredeterminatedNumberX.SelectionLength = 0;
        }

        private void AddingFinalZeroInNumberFormat(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if ((sender as TextBox).Text.LastOrDefault() == '.')
            {
                textBox.Text += "0";
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void AddingFinalZeroInMultipleNumberFormat(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            string enteredTextNumber = string.Empty;
            int indexLastWhitespace = textBox.Text.LastIndexOf(" ");

            if (indexLastWhitespace == -1)
                enteredTextNumber = textBox.Text;
            else if (indexLastWhitespace < textBox.Text.Length - 1)
                enteredTextNumber = textBox.Text.Substring(indexLastWhitespace + 1, textBox.Text.Length - indexLastWhitespace - 1);

            if (enteredTextNumber.LastOrDefault() == '.')
            {
                textBox.Text += "0";
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void CreateRandomCollection(object sender, RoutedEventArgs e)
        {
            textboxMinimumRandomValue.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            textboxMaximumRandomValue.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            textboxCountNumbersInRandomCollection.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFABADB3"));

            if (textboxMinimumRandomValue.Text.Length == 0)
                textboxMinimumRandomValue.BorderBrush = Brushes.Red;
            else if (textboxMaximumRandomValue.Text.Length == 0)
                textboxMaximumRandomValue.BorderBrush = Brushes.Red;
            else if (textboxCountNumbersInRandomCollection.Text.Length == 0)
                textboxCountNumbersInRandomCollection.BorderBrush = Brushes.Red;
            else if (Convert.ToDouble(textboxMinimumRandomValue.Text, new CultureInfo("en-EN")) > Convert.ToDouble(textboxMaximumRandomValue.Text, new CultureInfo("en-EN")))
            {
                textboxMinimumRandomValue.BorderBrush = Brushes.Red;
                textboxMaximumRandomValue.BorderBrush = Brushes.Red;
            }
            else
            {
                this.Cursor = Cursors.Wait;
                double minimumRandomValue = Convert.ToDouble(textboxMinimumRandomValue.Text, new CultureInfo("en-EN"));
                double maximumRandomValue = Convert.ToDouble(textboxMaximumRandomValue.Text, new CultureInfo("en-EN"));
                int countNumbers = Convert.ToInt32(textboxCountNumbersInRandomCollection.Text, new CultureInfo("en-EN"));
                bool onlyIntegers = radiobuttonOnlyIntegers.IsChecked == true;

                textboxSourceCollectionOfNumbers.Text = new Random().CreateStringRandomNumbers(minimumRandomValue, maximumRandomValue, countNumbers, onlyIntegers);
                CountNumbersInSourceCollection(textboxSourceCollectionOfNumbers);
                this.Cursor = Cursors.Arrow;
            }
        }
    }
}
