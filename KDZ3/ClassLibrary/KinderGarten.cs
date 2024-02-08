namespace ClassLibrary
{
    public class KinderGarten
    {
        public readonly string name, okrug, rayon, form_of_incorporation, submission, tip_uchrezhdeniya, vid_uchrezhdeniya, X, Y, global_id;

        private Address address;

        private string originalString; //Исходная строка прочитанного файла.

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data"> Строка без лишних символов, разделенная по ;". </param>
        /// <param name="input">Экземпляр address.</param>
        /// <param name="originalString">Исходная строка.</param>
        public KinderGarten(string[] data, Address input, string originalString)
        {
            address = input;
            this.originalString = originalString;
            name = data[1];
            okrug = data[3];
            rayon = data[4];
            form_of_incorporation = data[5];
            submission = data[6];
            tip_uchrezhdeniya = data[7];
            vid_uchrezhdeniya = data[8];
            X = data[12];
            Y = data[13];
            global_id = data[14];
        }

        public string StringRow => originalString; //Возвращает исходную строку.

        
    }
}
