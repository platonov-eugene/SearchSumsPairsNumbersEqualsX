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

namespace SearchSumsPairsNumbersEqualsX.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void imageHelpTestScenarioNumberOne_MouseEnter(object sender, MouseEventArgs e)
        {
            imageHelpTestScenarioNumberOne.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseEnter.png", UriKind.Relative));
        }

        private void imageHelpTestScenarioNumberOne_MouseLeave(object sender, MouseEventArgs e)
        {
            imageHelpTestScenarioNumberOne.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseLeave.png", UriKind.Relative));
        }

        private void imageHelpTestScenarioNumberTwo_MouseEnter(object sender, MouseEventArgs e)
        {
            imageHelpTestScenarioNumberTwo.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseEnter.png", UriKind.Relative));
        }

        private void imageHelpTestScenarioNumberTwo_MouseLeave(object sender, MouseEventArgs e)
        {
            imageHelpTestScenarioNumberTwo.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseLeave.png", UriKind.Relative));
        }

        private void imageHelpTestScenarioNumberThree_MouseEnter(object sender, MouseEventArgs e)
        {
            imageHelpTestScenarioNumberThree.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseEnter.png", UriKind.Relative));
        }

        private void imageHelpTestScenarioNumberThree_MouseLeave(object sender, MouseEventArgs e)
        {
            imageHelpTestScenarioNumberThree.Source = new BitmapImage(new Uri(@"Images/HelpButtonForMouseLeave.png", UriKind.Relative));
        }
    }
}
