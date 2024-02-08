namespace ClassLibrary
{
    public static class WriteNRows
    {
        /// <summary>
        /// Получаем массив для вывода.
        /// </summary>
        /// <param name="name">Каким образом нужно вывести данные.</param>
        /// <param name="data">Массив для вывода.</param>
        /// <param name="n">Какое количество строк выводить.</param>
        /// <returns>Массив для вывода.</returns>
        private static KinderGarten[] makeAns(Indexing name, KinderGarten[] data, int n)
        {
            KinderGarten[] newData = name switch
            {
                Indexing.TopN => data[0..n],
                Indexing.BottomN => data[(data.Length-n)..(data.Length)],
                Indexing.All => data

            };
            return newData;
        }

        /// <summary>
        /// Обрабатываем строку и делаем ее более читаемой.
        /// </summary>
        /// <param name="r">Строка для обработки.</param>
        /// <returns>Обработанную строку.</returns>
        private static string tabString(string r)
        {
            string[] rSplit = r.Split(";\"");
            for (int i = 0; i < rSplit.Length - 1; i++)
            {
                rSplit[i] = rSplit[i].Trim('\"'); //Убираем лишние знаки.
                rSplit[i] = '|' + rSplit[i] + '|'; //Добавляем границы для поля.
            }
            rSplit[rSplit.Length - 1] = rSplit[rSplit.Length - 1].Trim(';').Trim('\"'); //Убираем лишние знаки.
            rSplit[rSplit.Length - 1] = '|' + rSplit[rSplit.Length - 1] + '|'; //Добавляем границы для поля.
            return String.Join(" ", rSplit);
        }
        /// <summary>
        /// Вывод массива.
        /// </summary>
        /// <param name="name">Каким образом выводить.</param>
        /// <param name="data">Массив для вывода.</param>
        /// <param name="n">Количество строк для вывода.</param>
        public static void Print(Indexing name,KinderGarten[] data, int n)
        {
            data = makeAns(name, data, n);
            for(int i = 0; i < data.Length; i++) 
            {
                string curr = tabString(data[i].StringRow); //Обрабатываем строку.
                Console.WriteLine(curr);
                Console.WriteLine();
            }
        }
    }
}
