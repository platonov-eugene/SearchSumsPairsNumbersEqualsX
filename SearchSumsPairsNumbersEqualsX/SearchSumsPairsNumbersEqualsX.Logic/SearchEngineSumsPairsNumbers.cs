using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SearchSumsPairsNumbersEqualsX.Logic
{
    /// <summary>
    /// Класс осуществляющий поиск пар чисел по заданным критериям
    /// </summary>
    public class SearchEngineSumsPairsNumbers
    {
        /// <summary>
        /// Коллекция чисел
        /// </summary>
        private List<decimal> collectionOfNumbers;

        /// <summary>
        /// Конструктор по умолчанию класса осуществляющего поиск пар чисел по заданным критериям
        /// </summary>
        /// <param name="sourceCollectionOfNumbers">Строковое представление исходной коллекции чисел разделенных знаками пробела</param>
        public SearchEngineSumsPairsNumbers(string sourceCollectionOfNumbers)
        {
            collectionOfNumbers = sourceCollectionOfNumbers.Split(' ').Where(item => item != string.Empty).ToList().ConvertAll(item => Convert.ToDecimal(item, new CultureInfo("en-US")));
        }

        /// <summary>
        /// Возвращает коллекцию пар чисел, сумма которых равна заданному числу X
        /// </summary>
        /// <param name="stringValueOfX">Заданное число X</param>
        /// <returns>Коллекция пар чисел, сумма которых равна заданному числу X</returns>
        public List<Tuple<decimal, decimal>> EqualsX(string stringValueOfX)
        {
            List<Tuple<decimal, decimal>> foundPairsNumbers = new List<Tuple<decimal, decimal>>();
            decimal predeterminatedNumberX = Convert.ToDecimal(stringValueOfX, new CultureInfo("en-US"));

            collectionOfNumbers.Sort();

            for (int i = 0; i < collectionOfNumbers.Count; i++)
            {
                int indexFoundNumber = collectionOfNumbers.BinarySearch(predeterminatedNumberX - collectionOfNumbers[i]);

                if (indexFoundNumber >= 0)
                    foundPairsNumbers.Add(new Tuple<decimal, decimal>(collectionOfNumbers[i], collectionOfNumbers[indexFoundNumber]));
            }

            return foundPairsNumbers;
        }
    }
}
