namespace ClassLibrary
{
    public static class FileReader 
    {
        // Шапка таблицы.
        public static string firstRow = "ROWNUM;name;adress;okrug;rayon;form_of_incorporation;submission;tip_uchrezhdeniya;vid_uchrezhdeniya;telephone;web_site;e_mail;X;Y;global_id;";
        
        /// <summary>
        /// Получаем корректный путь к файлу.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        private static void getAndCheckPath(out string path)
        {
            do
            {
                Console.WriteLine("Введите абсолютный путь к csv-файлу для перехода к меню:");
                path = Console.ReadLine();
                if (!File.Exists(path))
                {
                    Console.WriteLine("Вы ввели неверный путь, повторите попытку!");
                }
                else { break; }
            } while (true);
        }

        /// <summary>
        /// Убираем из строки лишние символы.
        /// </summary>
        /// <param name="dataArr"> Строка, которую сплитовали по ;".</param>
        private static void makeDataPretty(string[] dataArr)
        {
            for(int i = 0; i < dataArr.Length-1; i++) 
            {
                dataArr[i] = dataArr[i].Trim('\"');
            }
            dataArr[dataArr.Length-1] = dataArr[dataArr.Length-1].Trim(';').Trim('\"');
        }
        /// <summary>
        /// Получаем и проверяем данные из файла.
        /// </summary>
        /// <param name="fileData">Полученные данные.</param>
        /// <returns>true, если верная структура, false иначе.</returns>
        public static bool GetAndCheckFileData(out KinderGarten[] fileData)
        {
            getAndCheckPath(out string path);
            string[] readData = null;
            //Обработка ошибок при работе с файлом.
            try
            {
                readData = File.ReadAllLines(path);
            }
            catch
            {
                Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку!");
                fileData = null; 
                return false;
            }
            int index = 0; //Индекс текущего элменета итогово массива.
            fileData = new KinderGarten[readData.Length-1];
            if (readData.Length == 0 )  //Проверяем файл на наличие строк.
            {
                Console.WriteLine("Файл пуст, повторите попытку.");
                return false;
            }
            if (readData[0] != firstRow) //Проверяем файл на наличие правильной шапки.
            {
                Console.WriteLine("Неверная структура файла, повторите попытку.");
                return false;
            }
            for(int i = 1; i< readData.Length; i++)  //Проверяем каждую строку на количество полей.
            {
                string[] curr = readData[i].Split(";\"");
                if (curr.Length != 15)  //Если строка не подошла, пропускаем ее.
                {
                    continue;
                }
                makeDataPretty(curr);
                Address address = new Address(curr);
                fileData[index] = new KinderGarten(curr, address, readData[i]); 
                index++;
            }
            Array.Resize(ref fileData, index);
            return true;
        }
    }
}