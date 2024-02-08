namespace ClassLibrary
{
    public static class DataProcessing
    {
        /// <summary>
        /// Сортировка по возрастанию.
        /// </summary>
        /// <param name="data">Массив для сортировки.</param>
        public static void SortAscending(KinderGarten[] data)  
        {
            for(int i = 0; i < data.Length; i++)
            {
                for (int j = i+1; j < data.Length; j++)
                {
                    int result = String.Compare(data[i].rayon, data[j].rayon);
                    if (data[i].rayon == String.Empty)
                    {
                        KinderGarten remember = data[i];
                        data[i] = data[j];
                        data[j] = remember;
                    }
                    else if ( result <  0)
                    { 
                        KinderGarten remember = data[i];
                        data[i] = data[j];
                        data[j] = remember;
                    }
                }
            }
        }
        /// <summary>
        /// Сортировка по убыванию.
        /// </summary>
        /// <param name="data">Массив для сортировки.</param>
        public static void SortDescending(KinderGarten[] data) 
        {
            SortAscending(data);
            Array.Reverse(data);
        }

        /// <summary>
        /// Получаем данные для фильтра.
        /// </summary>
        /// <param name="fieldNumber">Номер поля, по которому фильтруем.</param>
        /// <returns>Массив элементов для фильтра.</returns>
        private static string[] getFilterData(int fieldNumber)
        {
            string fieldName = "";
            switch (fieldNumber)
            {
                case 1: fieldName = "form_of_incorporation"; break;
                case 2: fieldName = "vid_uchrezhdeniya"; break;
            }
            do
            {
                Console.WriteLine("Введите через запятую, данные для фильтра для поля {0}:", fieldName);
                string[] input = Console.ReadLine().Split(',');
                if (input.Length == 0)
                {
                    Console.WriteLine("Пустая строка, повторите попытку!");
                }
                else
                {
                    for (int i = 0; i < input.Length; i++)
                    {
                        input[i] = input[i].Trim();
                    }
                    return input;
                }
            } while (true);
        }

        /// <summary>
        /// Фильтр для поля VidUchrezhdeniya.
        /// </summary>
        /// <param name="data">Массив для фильтрации.</param>
        /// <param name="fields">Элементы, которые должны быть в строке.</param>
        /// <returns>Массив подходящих строк.</returns>
        private static KinderGarten[] filterVidUchrezhdeniya(KinderGarten[] data, string[] fields)
        {
            KinderGarten[] ans = new KinderGarten[data.Length];
            int quantityRow = 0; //Количество подходящих строк.
            for (int i = 0; i < data.Length; i++) 
            {
                bool flag = true; //true, если строка подходит, false иначе.
                for (int j = 0; j < fields.Length; j++)
                {
                    if (!data[i].vid_uchrezhdeniya.Contains(fields[j]))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) //Если строка подходит добавляем в массив.
                {
                    ans[quantityRow] = data[i];
                    quantityRow += 1;
                }
            }
            Array.Resize(ref ans, quantityRow); //Убираем пустые элементы массива.
            return ans;
        }

        /// <summary>
        /// Фильтр для поля FormOfIncorporation.
        /// </summary>
        /// <param name="data">Массив для фильтрации.</param>
        /// <param name="fields">Элементы, которые должны быть в строке.</param>
        /// <returns>Массив подходящих строк.</returns>
        private static KinderGarten[] filterFormOfIncorporation(KinderGarten[] data, string[] fields)
        {
            KinderGarten[] ans = new KinderGarten[data.Length];
            int quantityRow = 0; //Количество подходящих строк.
            for (int i = 0; i < data.Length; i++)
            {
                bool flag = true; //true, если строка подходит, false иначе.
                for (int j = 0; j < fields.Length; j++)
                {
                    if (!data[i].form_of_incorporation.Contains(fields[j]))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) //Если строка подходит добавляем в массив.
                {
                    ans[quantityRow] = data[i];
                    quantityRow += 1;
                }
            }
            Array.Resize(ref ans, quantityRow); //Убираем пустые элементы массива.
            return ans;
        }

        /// <summary>
        /// Реализация фильтра.
        /// </summary>
        /// <param name="data">Массив для фильтрации.</param>
        /// <param name="fieldName">По каким полям фильтрация.</param>
        /// <returns>Массив подходящих строк.</returns>
        public static KinderGarten[] Filter(KinderGarten[] data, int fieldName)
        {
            KinderGarten[] ans = null;
            switch (fieldName)
            {
                case 1:
                    string[] fields = getFilterData(1);
                    ans = filterVidUchrezhdeniya(data, fields);
                    break;
                case 2:
                    fields = getFilterData(2);
                    ans = filterFormOfIncorporation(data, fields);
                    break;
                case 3:
                    string[] fieldsVU = getFilterData(1);
                    string[] fieldsFOI = getFilterData(2);
                    ans = filterVidUchrezhdeniya(data, fieldsVU);
                    ans = filterFormOfIncorporation(ans, fieldsFOI);
                    break;
            }
            return ans;
        }    
    }
}
