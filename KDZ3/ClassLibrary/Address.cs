namespace ClassLibrary
{
    public class Address
    {
        public readonly string adress, telephone, web_site, e_mail;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data">Строка без лишних символов, разделенная по ;".</param>
        public Address(string[] data)
        {
            adress = data[2];
            telephone = data[9];
            web_site = data[10];
            e_mail = data[11];
        }
    }
}
