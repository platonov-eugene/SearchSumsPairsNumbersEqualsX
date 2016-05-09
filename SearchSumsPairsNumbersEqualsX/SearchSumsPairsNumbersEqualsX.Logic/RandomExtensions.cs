using System;

namespace SearchSumsPairsNumbersEqualsX.Logic
{
    /// <summary>
    /// Класс осуществляющий расширение функциональных возможностей класса System.Random
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Возвращает случайное целое число в указанном диапазоне
        /// </summary>
        /// <param name="random">Класс System.Random</param>
        /// <param name="minimumRandomValue">Минимальное значение заданного диапазона</param>
        /// <param name="maximumRandomValue">Максимальное значение заданного диапазона</param>
        /// <returns>Случайное целое число в указанном диапазоне</returns>
        public static double NextInteger(this Random random, double minimumRandomValue, double maximumRandomValue)
        {
            return random.Next((int)minimumRandomValue, (int)maximumRandomValue);
        }

        /// <summary>
        /// Возвращает случайное число с плавающей точкой в указанном диапазоне
        /// </summary>
        /// <param name="random">Класс System.Random</param>
        /// <param name="minimumRandomValue">Минимальное значение заданного диапазона</param>
        /// <param name="maximumRandomValue">Максимальное значение заданного диапазона</param>
        /// <returns>Случайное число с плавающей точкой в указанном диапазоне</returns>
        public static double NextDouble(this Random random, double minimumRandomValue, double maximumRandomValue)
        {
            return random.NextDouble() * (maximumRandomValue - minimumRandomValue) + minimumRandomValue;
        }

        /// <summary>
        /// Возвращает строку случайных чисел в указанном диапазоне разделенных знаком пробела
        /// </summary>
        /// <param name="random">Класс System.Random</param>
        /// <param name="minimumRandomValue">Минимальное значение заданного диапазона</param>
        /// <param name="maximumRandomValue">Максимальное значение заданного диапазона</param>
        /// <param name="countNumbers">Необходимое количество сформированных случайных чисел</param>
        /// <param name="onlyIntegers">Логическое значение указывающее на формирование только целых случайных чисел</param>
        /// <returns>Строка случайных чисел в указанном диапазоне разделенных знаком пробела</returns>
        public static string CreateStringRandomNumbers(this Random random, double minimumRandomValue, double maximumRandomValue, int countNumbers, bool onlyIntegers = true)
        {
            string stringRandomNumbers = string.Empty;

            for (int i = 1; i <= countNumbers; i++)
                if (onlyIntegers)
                    stringRandomNumbers += NextInteger(random, minimumRandomValue, maximumRandomValue) + " ";
                else
                    stringRandomNumbers += Math.Round(NextDouble(random, minimumRandomValue, maximumRandomValue), 2) + " ";

            return stringRandomNumbers;
        }
    }
}
