using System.Data.SqlClient;

namespace myAPi
{

    public class SQL
    {
        private string connectionstring = "";

        public SQL(String connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public List<Contact>? GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    String sql = "SELECT Id, FirstName, LastName, PhoneNumber FROM Contacts";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.HasRows)
                                {
                                    contacts.Add(new Contact()
                                    {
                                        id = reader.GetInt32(0),
                                        firstName = reader.GetString(1),
                                        lastName = reader.GetString(2),
                                        phoneNumber = reader.GetString(3)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return contacts;
        }

        public Contact GetContact(int id)
        {
            Contact contact = new Contact();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    String sql = "SELECT Id, FirstName, LastName, PhoneNumber FROM Contacts WHERE Id=" + id;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.HasRows)
                                {
                                    contact = new Contact()
                                    {
                                        id = reader.GetInt32(0),
                                        firstName = reader.GetString(1),
                                        lastName = reader.GetString(2),
                                        phoneNumber = reader.GetString(3)
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return contact;
        }

        public bool AddContact(Contact contact) {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    String sql = $"INSERT INTO Contacts (FirstName, LastName, PhoneNumber) VALUES ('{contact.firstName}', '{contact.lastName}', '{contact.phoneNumber}')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if(command.ExecuteNonQuery() == 1) {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public bool DeleteContact(int id) {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    String sql = $"DELETE FROM Contacts WHERE Id={id}";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if(command.ExecuteNonQuery() == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public Contact UpdateContact(Contact contact) {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    String sql = $"UPDATE Contacts SET FirstName='{contact.firstName}', LastName='{contact.lastName}', PhoneNumber='{contact.phoneNumber}' WHERE Id={contact.id}";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return contact;
        }
    }
}