namespace ClassLibrary
{
    public static class FileWriter 
    {
        /// <summary>
        /// Проверяем имя файла.
        /// </summary>
        /// <param name="filename">Имя файла.</param>
        /// <returns>true, если имя корректно, false иначе.</returns>
        private static bool validateFileName(string filename)
        {
            try
            {
                FileStream fs = File.Open(filename, FileMode.Open);
                if (fs != null) fs.Close();
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (FileNotFoundException)
            {
                return true;
            }
            catch (IOException)
            {
                return true;
            }
            return true;
        } 
        
        /// <summary>
        /// Получаем и проверяем имя файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        private static void getAndCheckFileName(out string fileName)
        {
            do
            {
                Console.WriteLine("Введите имя файла без расширения. Пример: name.");
                fileName = Console.ReadLine();
                if (!validateFileName(fileName))
                {
                    Console.WriteLine("Недопустимое имя файла, повторите попытку!");
                    continue;
                }
                else{ break;}
            } while (true);
        }

        /// <summary>
        /// Получаем корректную директорию и формируем абсолютный путь к файлу.
        /// </summary>
        /// <param name="path">Абсолютный путь к файлу.</param>
        private static void getAndCheckPath(out string path)
        {
            do
            {
                Console.WriteLine($"{Environment.NewLine}Введите директорию, где хотите сохранить файл.{Environment.NewLine}Пример для Windows: C:\\Users\\frubyl\\Desktop\\KDZ\\ClassLibrary");
                path = Console.ReadLine();
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    Console.WriteLine("Такой директории не существует, повторите попытку!");
                }
                else
                {
                    getAndCheckFileName(out string fileName);
                    path = path + Path.DirectorySeparatorChar + fileName + ".csv";
                    break;
                }
            } while (true);
        }

        /// <summary>
        /// Формирует массив для записи в файл с шапкой.
        /// </summary>
        /// <param name="data">Массив, который нужно записать.</param>
        /// <returns>Массив для записи.</returns>
        private static string[] makeAnsWithFirstRow(KinderGarten[] data)
        {
            string[] ans = new string[data.Length+1];
            ans[0] = FileReader.firstRow;
            for (int i = 0;i < data.Length;i++) 
            {
                ans[i+1] = data[i].StringRow;
            }
            return ans;
        }
      
        /// <summary>
        /// Формируем массив для записи без шапки.
        /// </summary>
        /// <param name="data">Массив, который нужно записать.</param>
        /// <param name="flag">Переменная для перегрузки метода.</param>
        /// <returns>Массив для записи.</returns>
        public static string[] makeAns(KinderGarten[] data)
        {
            string[] ans = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                ans[i] = data[i].StringRow;
            }
            return ans;
        }

        /// <summary>
        /// Режим сохранения - создание нового файла.
        /// </summary>
        /// <param name="data">Массив для записи.</param>
        /// <param name="path">Абсолютный путь.</param>
        private static void firstMenuItem(KinderGarten[] data, string path)
        {
            string[] fileData = makeAnsWithFirstRow(data);
            do
            {
                try
                {
                    if (File.Exists(path)) //Если файл существует, предлагаем его перезаписать. Если пользователь не захочет перезаписать файл, запрашиваем абсолютный путь еще раз.
                    {
                        Console.WriteLine("Данный файл уже существует! Введите \"Да\", если хотите презаписать данные, иначе Вам предложат ввести другой путь.");
                        if (Console.ReadLine()?.Trim().ToLower() == "да")
                        {
                            File.WriteAllLines(path, fileData);
                            Console.WriteLine("Файл записан!");
                            break;
                        }
                        else
                        {
                            getAndCheckPath(out path);
                            continue;
                        }
                    }
                    else //Если файла не существует, создаем.
                    {
                        File.WriteAllLines(path, fileData);
                        Console.WriteLine("Файл записан!");
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку!");
                    getAndCheckPath(out path);
                    continue;
                }
            } while (true);
        }

        /// <summary>
        /// Режим сохранения - замена содержимого в существующем файле.
        /// </summary>
        /// <param name="data">Массив для записи.</param>
        /// <param name="path">Абсолютный путь.</param>
        private static void secondMenuItem(KinderGarten[] data, string path)
        {
            string[] fileData = makeAnsWithFirstRow(data);
            do
            {
                try
                {
                    if (!File.Exists(path)) //Если файл не существует, предлагаем его создать. Если пользователь не захочет создать файл, запрашиваем абсолютный путь еще раз.
                    {
                        Console.WriteLine("Данный файл не существует! Введите \"Да\", если хотите создать новый файл, иначе Вам предложат ввести другой путь.");
                        if (Console.ReadLine()?.Trim().ToLower() == "да")
                        {
                            File.WriteAllLines(path, fileData);
                            Console.WriteLine("Файл записан!");
                            break;
                        }
                        else
                        {
                            getAndCheckPath(out path);
                            continue;
                        }
                    }
                    else //Если файл существует, перезаписываем.
                    {
                        File.WriteAllLines(path, fileData);
                        Console.WriteLine("Файл записан!");
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку!");
                    getAndCheckPath(out path);
                    continue;
                }
            } while (true);
        }

        /// <summary>
        /// Проверка структуры файла.
        /// </summary>
        /// <param name="path">Абсолютный путь к файлу.</param>
        /// <returns>true, если верная структура, false иначе.</returns>
        private static bool checkStructure(string path)
        {
            string[] data = File.ReadAllLines(path); 
            if (data.Length == 0)
            {
                return false;
            }
            if(data[0] != FileReader.firstRow) 
            {
                return false;
            }
            for(int i = 1; i < data.Length; i++) 
            {
                if (data[i].Split(";\"").Length != 15)
                {
                    return false;
                }
            }
            return true;

        }

        /// <summary>
        /// Режим сохранения - добавление содержимого в существующем файле.
        /// </summary>
        /// <param name="data">Массив для записи.</param>
        /// <param name="path">Абсолютный путь.</param>
        private static void thirdMenuItem(KinderGarten[] data, string path) 
        {
            string[] fileData = makeAns(data);
            do
            {
                try
                {
                    if (!File.Exists(path))//Если файл не существует, предлагаем его создать. Если пользователь не захочет создать файл, запрашиваем абсолютный путь еще раз.
                    {
                        Console.WriteLine("Данный не файл существует! Введите \"Да\", если хотите создать новый файл, иначе Вам предложат ввести другой путь.");
                        if (Console.ReadLine()?.Trim().ToLower() == "да")
                        {
                            File.WriteAllLines(path, fileData);
                            Console.WriteLine("Файл записан!");
                            break;
                        }
                        else
                        {
                            getAndCheckPath(out path);
                            continue;
                        }
                    }
                    else //Если файл существует, проверяем структуру.
                    {
                        if (!checkStructure(path)) //Если неверная структура, запрашиваем абсолютный путь еще раз.
                        {
                            Console.WriteLine("Неверная структура файла, повторите попытку!");
                            getAndCheckPath(out path);
                            continue;

                        }
                        File.AppendAllLines(path, fileData);
                        Console.WriteLine("Файл записан!");
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку!");
                    getAndCheckPath(out path);
                    continue;
                }
            } while (true);
        }
        /// <summary>
        /// Реализация выбора режима сохранения.
        /// </summary>
        /// <param name="data">Массив для записи.</param>
        public static void WriteData(KinderGarten[] data)
        {
            getAndCheckPath(out string path);
            Console.WriteLine("Выбирети режим сохранения файла. Пример ввода: 1");
            Console.WriteLine("1. Создать новый файл.");
            Console.WriteLine("2. Заменить содержимое в существующем файле.");
            Console.WriteLine("3. Добавить данные в существующий файл.");
            CheckNumber.CheckAndGetNumber(3, out int n);
            switch (n)
            {
                case 1:
                    firstMenuItem(data, path);
                    break;
                case 2:
                    secondMenuItem(data, path);
                    break;
                case 3:
                    thirdMenuItem(data, path);
                    break;
            }
            
        }
    }
}
