namespace Panda.Domein
{
    public class User
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }


        /*•	Has an Id – a GUID String or an Integer.
•	Has an Username
•	Has a Password
•	Has an Email
•	Has an Role – can be one of the following values (“User”, “Admin”)
*/
    }
}