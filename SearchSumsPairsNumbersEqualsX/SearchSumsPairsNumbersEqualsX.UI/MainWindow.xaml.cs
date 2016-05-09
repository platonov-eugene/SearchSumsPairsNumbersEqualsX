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

        #region Загрузка тестовых сценариев

        private void SetImageOnHelpTestScenario(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;

            if (image.Source.ToString() == @"pack://application:,,,/SearchSumsPairsNumbersEqualsX.UI;component/Images/HelpButtonForMouseLeave.png")
                image.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseEnter.png", UriKind.Relative));
            else
                image.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseLeave.png", UriKind.Relative));
        }

        private void LoadTestScenarioNumberOne(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            textboxSourceCollectionOfNumbers.Text = "10 9 8 7 6 5 4 3 2 1 ";
            textboxPredeterminatedNumberX.Text = "5";

            CountNumbersInSourceCollection(textboxSourceCollectionOfNumbers);
            ClearAreaFoundPairsNumbers();
            textboxSourceCollectionOfNumbers.Focus();
            Cursor = Cursors.Arrow;
        }

        private void HelpTestScenarioNumberOne(object sender, MouseButtonEventArgs e)
        {
            string messageBoxCaption = "Описание значений тестового сценария №1";
            string messageBoxText = String.Format("Исходная коллекция чисел: {0}.\n\nЗаданное число X: {1}.\n\nОжидаемые пары чисел: {2}.",
                                                  "10 9 8 7 6 5 4 3 2 1",
                                                  "5",
                                                  "[1 + 4], [2 + 3], [3 + 2], [4 + 1]");

            MessageBox.Show(messageBoxText, messageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadTestScenarioNumberTwo(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            textboxSourceCollectionOfNumbers.Text = "5 4 3 2 1 0 -1 -2 -3 -4 -5 ";
            textboxPredeterminatedNumberX.Text = "3";

            CountNumbersInSourceCollection(textboxSourceCollectionOfNumbers);
            ClearAreaFoundPairsNumbers();
            textboxSourceCollectionOfNumbers.Focus();
            Cursor = Cursors.Arrow;
        }

        private void HelpTestScenarioNumberTwo(object sender, MouseButtonEventArgs e)
        {
            string messageBoxCaption = "Описание значений тестового сценария №2";
            string messageBoxText = String.Format("Исходная коллекция чисел: {0}.\n\nЗаданное число X: {1}.\n\nОжидаемые пары чисел: {2}.",
                                                  "5 4 3 2 1 0 -1 -2 -3 -4 -5",
                                                  "3",
                                                  "[-2 + 5], [-1 + 4], [0 + 3], [1 + 2], [2 + 1], [3 + 0], [4 + -1], [5 + -2]");

            MessageBox.Show(messageBoxText, messageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadTestScenarioNumberThree(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            textboxSourceCollectionOfNumbers.Text = "0.5 0.25 0.75 0.5 1 0 1.25 -0.25 1.5 -0.5 ";
            textboxPredeterminatedNumberX.Text = "1";

            CountNumbersInSourceCollection(textboxSourceCollectionOfNumbers);
            ClearAreaFoundPairsNumbers();
            textboxSourceCollectionOfNumbers.Focus();
            Cursor = Cursors.Arrow;
        }

        private void HelpTestScenarioNumberThree(object sender, MouseButtonEventArgs e)
        {
            string messageBoxCaption = "Описание значений тестового сценария №3";
            string messageBoxText = String.Format("Исходная коллекция чисел: {0}.\n\nЗаданное число X: {1}.\n\nОжидаемые пары чисел: {2}.",
                                                  "0.5 0.25 0.75 0.5 1 0 1.25 -0.25 1.5 -0.5",
                                                  "1",
                                                  "[-0.5 + 1.5], [-0.25 + 1.25], [0 + 1], [0.25 + 0.75], [0.5 + 0.5], [0.5 + 0.5], [0.75 + 0.25], [1 + 0], [1.25 + -0.25], [1.5 + -0.5]");

            MessageBox.Show(messageBoxText, messageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        private void DisableSelectionTextInTextBoxes(object sender, MouseEventArgs e)
        {
            textboxSourceCollectionOfNumbers.SelectionLength = 0;
            textboxMinimumRandomValue.SelectionLength = 0;
            textboxMaximumRandomValue.SelectionLength = 0;
            textboxCountNumbersInRandomCollection.SelectionLength = 0;
            textboxPredeterminatedNumberX.SelectionLength = 0;
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
            else if (Convert.ToDecimal(textboxMinimumRandomValue.Text, new CultureInfo("en-EN")) > Convert.ToDecimal(textboxMaximumRandomValue.Text, new CultureInfo("en-EN")))
            {
                textboxMinimumRandomValue.BorderBrush = Brushes.Red;
                textboxMaximumRandomValue.BorderBrush = Brushes.Red;
            }
            else
            {
                Cursor = Cursors.Wait;
                decimal minimumRandomValue = Convert.ToDecimal(textboxMinimumRandomValue.Text, new CultureInfo("en-EN"));
                decimal maximumRandomValue = Convert.ToDecimal(textboxMaximumRandomValue.Text, new CultureInfo("en-EN"));
                int countNumbers = Convert.ToInt32(textboxCountNumbersInRandomCollection.Text, new CultureInfo("en-EN"));
                bool onlyIntegers = radiobuttonOnlyIntegers.IsChecked == true;

                textboxSourceCollectionOfNumbers.Text = new Random().CreateStringRandomNumbers(minimumRandomValue, maximumRandomValue, countNumbers, onlyIntegers);
                CountNumbersInSourceCollection(textboxSourceCollectionOfNumbers);
                ClearAreaFoundPairsNumbers();
                textboxSourceCollectionOfNumbers.Focus();
                Cursor = Cursors.Arrow;
            }
        }

        private void ClearAreaFoundPairsNumbers()
        {
            stackpanelFoundPairsNumbers.Children.Clear();
            labelSearchTime.Content = "00:00:00.000";
            labelNumberFoundPairsNumbers.Content = 0;
        }

        private void SearchSumsPairsNumbersEqualsX(object sender, RoutedEventArgs e)
        {
            textboxPredeterminatedNumberX.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFABADB3"));

            if (textboxPredeterminatedNumberX.Text.Length == 0)
                textboxPredeterminatedNumberX.BorderBrush = Brushes.Red;
            else if (textboxSourceCollectionOfNumbers.Text.Length != 0)
            {
                Cursor = Cursors.Wait;

                DateTime startTime = DateTime.Now;
                ClearAreaFoundPairsNumbers();

                SearchEngineSumsPairsNumbers searchEngineSumsPairsNumbers = new SearchEngineSumsPairsNumbers(textboxSourceCollectionOfNumbers.Text);
                List<Tuple<decimal, decimal>> foundPairsNumbers = searchEngineSumsPairsNumbers.EqualsX(textboxPredeterminatedNumberX.Text);
                
                foreach (Tuple<decimal, decimal> sumPairsNumbers in foundPairsNumbers)
                    stackpanelFoundPairsNumbers.Children.Add(CreateUserControlForSumPairsNumbers(sumPairsNumbers.Item1, sumPairsNumbers.Item2));

                labelNumberFoundPairsNumbers.Content = foundPairsNumbers.Count;

                DateTime endTime = DateTime.Now;
                labelSearchTime.Content = (endTime - startTime).ToString(@"hh\:mm\:ss\.fff");
                
                Cursor = Cursors.Arrow;
            }
        }

        private void SearchSumsPairsNumbersEqualsXByKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchSumsPairsNumbersEqualsX(this, null);
        }

        private Border CreateUserControlForSumPairsNumbers(decimal firstNumber, decimal secondNumber)
        {
            Border border = new Border();
            border.BorderBrush = Brushes.Green;
            border.BorderThickness = new Thickness(2);
            border.Margin = new Thickness(5, 5, 5, 5);
            border.CornerRadius = new CornerRadius(5);
            border.MinWidth = 100;

            Grid grid = new Grid();
            grid.Margin = new Thickness(5, 5, 5, 5);
            RowDefinition rowOneDefinition = new RowDefinition();
            rowOneDefinition.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(rowOneDefinition);
            RowDefinition rowTwoDefinition = new RowDefinition();
            rowTwoDefinition.Height = new GridLength(0.5, GridUnitType.Star);
            grid.RowDefinitions.Add(rowTwoDefinition);
            RowDefinition rowThreeDefinition = new RowDefinition();
            rowThreeDefinition.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(rowThreeDefinition);
            RowDefinition rowFourDefinition = new RowDefinition();
            rowFourDefinition.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(rowFourDefinition);

            TextBlock textblockFirstNumber = new TextBlock();
            textblockFirstNumber.Text = firstNumber >= 0 ? "  " + firstNumber.ToString(new CultureInfo("en-EN")) : " " + firstNumber.ToString(new CultureInfo("en-EN"));
            textblockFirstNumber.FontFamily = new FontFamily("Courier New");
            textblockFirstNumber.VerticalAlignment = VerticalAlignment.Center;
            textblockFirstNumber.FontSize = 18;
            textblockFirstNumber.FontWeight = FontWeights.DemiBold;
            Grid.SetRow(textblockFirstNumber, 0);
            grid.Children.Add(textblockFirstNumber);

            TextBlock textblockPlusSign = new TextBlock();
            textblockPlusSign.Text = "+";
            textblockPlusSign.FontFamily = new FontFamily("Courier New");
            textblockPlusSign.VerticalAlignment = VerticalAlignment.Center;
            textblockPlusSign.FontSize = 18;
            textblockPlusSign.FontWeight = FontWeights.DemiBold;
            Grid.SetRow(textblockPlusSign, 1);
            grid.Children.Add(textblockPlusSign);

            TextBlock textblockSecondNumber = new TextBlock();
            textblockSecondNumber.Text = secondNumber >= 0 ? "  " + secondNumber.ToString(new CultureInfo("en-EN")) : " " + secondNumber.ToString(new CultureInfo("en-EN"));
            textblockSecondNumber.FontFamily = new FontFamily("Courier New");
            textblockSecondNumber.VerticalAlignment = VerticalAlignment.Center;
            textblockSecondNumber.FontSize = 18;
            textblockSecondNumber.FontWeight = FontWeights.DemiBold;
            Grid.SetRow(textblockSecondNumber, 2);
            grid.Children.Add(textblockSecondNumber);

            TextBlock textblockResult = new TextBlock();
            textblockResult.Text = "= " + textboxPredeterminatedNumberX.Text;
            textblockResult.FontFamily = new FontFamily("Courier New");
            textblockResult.Foreground = Brushes.Red;
            textblockResult.VerticalAlignment = VerticalAlignment.Center;
            textblockResult.FontSize = 18;
            textblockResult.FontWeight = FontWeights.DemiBold;
            Grid.SetRow(textblockResult, 3);
            grid.Children.Add(textblockResult);
            
            border.Child = grid;
            return border;
        }
    }
}
