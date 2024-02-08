namespace ClassLibrary
{
    public static class CheckNumber
    {
        /// <summary>
        /// Запрашиваем и получаем число, которое не превышает границу.
        /// </summary>
        /// <param name="border">Граница (включительно).</param>
        /// <param name="n">Число.</param>
        public static void CheckAndGetNumber(int border, out int n)
        {
            do
            {
                if (!int.TryParse(Console.ReadLine(), out n) || n <= 0 || n > border)
                {
                    Console.WriteLine("Неверные данные, повторите попытку!");
                }
                else { break; }
            }while (true);
        }
    }
}
