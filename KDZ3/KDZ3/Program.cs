using ClassLibrary;
//C:\Users\frubyl\Desktop\к.csv
class Program
{
    static void Main(string[] args)
    {
        do
        {
            KinderGarten[] fileData;
            while (!FileReader.GetAndCheckFileData(out fileData)) { continue; } //Запрашиваем файл, пока структура файла неверна.
            Console.WriteLine("Введите пункт меню: ");
            Console.WriteLine("1. Сортировка по возрастанию по полю rayon"); 
            Console.WriteLine("2. Сортировка по убыванию по полю rayon"); 
            Console.WriteLine("3. Фильтр по полю form_of_incorporation"); 
            Console.WriteLine("4. Фильтр по полю vid_uchrezhdeniya"); 
            Console.WriteLine("5. Фильтр по полю vid_uchrezhdeniya и form_of_incorporation");
            Console.WriteLine("6. Вывести первые n эдементов"); 
            Console.WriteLine("7. Вывести последние n элементов"); 
            Console.WriteLine("8. Выход из программы");
            CheckNumber.CheckAndGetNumber(8, out int n);
            switch (n)
            {
                case 1:
                    DataProcessing.SortAscending(fileData);
                    WriteNRows.Print(Indexing.All, fileData, 0);
                    FileWriter.WriteData(fileData);
                    break;
                case 2:
                    DataProcessing.SortDescending(fileData);
                    WriteNRows.Print(Indexing.All, fileData, 0);
                    FileWriter.WriteData(fileData);
                    break;
                case 3:
                    KinderGarten[] filter1 = DataProcessing.Filter(fileData, 2);
                    if (filter1.Length == 0)
                    {
                        Console.WriteLine("Нет таких строк!");
                    }
                    else
                    {
                        WriteNRows.Print(Indexing.All, filter1, 0);
                        FileWriter.WriteData(filter1);
                    }
                    break;
                case 4:
                    KinderGarten[] filter2 = DataProcessing.Filter(fileData, 1);
                    if (filter2.Length == 0)
                    {
                        Console.WriteLine("Нет таких строк!");
                    }
                    else
                    {
                        WriteNRows.Print(Indexing.All, filter2, 0);
                        FileWriter.WriteData(filter2);
                    }
                    break;
                case 5:
                    KinderGarten[] filter3 = DataProcessing.Filter(fileData, 3);
                    if (filter3.Length == 0)
                    {
                        Console.WriteLine("Нет таких строк!");
                    }
                    else
                    {
                        WriteNRows.Print(Indexing.All, filter3, 0);
                        FileWriter.WriteData(filter3);
                    }
                    break;
                case 6:
                    Console.WriteLine("Введите N:");
                    CheckNumber.CheckAndGetNumber(fileData.Length, out int m);
                    WriteNRows.Print(Indexing.TopN, fileData, m);
                    Console.WriteLine("Данные выведены!");
                    break;
                case 7:
                    Console.WriteLine("Введите N:");
                    CheckNumber.CheckAndGetNumber(fileData.Length, out int k);
                    WriteNRows.Print(Indexing.BottomN, fileData, k);
                    Console.WriteLine("Данные выведены!");
                    break;
                case 8: return;
            }
        } while (true);
    }
}
