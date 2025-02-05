using System; // Importon hapësirën e emrave System për funksionalitete bazë.
using System.Collections.Generic; // Importon hapësirën e emrave për koleksione gjenerike.
using System.Linq; // Importon hapësirën e emrave për operacione LINQ.
using System.Text; // Importon hapësirën e emrave për manipulimin e tekstit.
using System.Threading.Tasks; // Importon hapësirën e emrave për programim asinkron.
using System.Data; // Importon hapësirën e emrave për të punuar me të dhënat.
using System.Data.SqlClient; // Importon hapësirën e emrave për të punuar me SQL Server.

namespace LibraryManagementSystem // Deklaron hapësirën e emrave për sistemin e menaxhimit të bibliotekës.
{
    class DataAddBooks // Deklaron klasën DataAddBooks.
    {
        // Krijon një lidhje me bazën e të dhënave SQL Server.
        SqlConnection connect = new SqlConnection(@"Data Source=mPC\SQLEXPRESS01;Initial Catalog=library;Integrated Security=True;Connect Timeout=30");

        // Deklaron pronat për librin.
        public int ID { set; get; } // Identifikuesi unik i librit.
        public string BookTitle { set; get; } // Titulli i librit.
        public string Author { set; get; } // Autori i librit.
        public string Pulished { set; get; } // Data e publikimit të librit.
        public string image { set; get; } // Imazhi i librit.
        public string Status { set; get; } // Statusi i librit (p.sh., i disponueshëm, i huazuar).

        // Metoda për të marrë të dhënat e librave nga baza e të dhënave.
        public List<DataAddBooks> addBooksData()
        {
            // Krijon një listë për të ruajtur të dhënat e librave.
            List<DataAddBooks> listData = new List<DataAddBooks>();

            // Kontrollon nëse lidhja me bazën e të dhënave është e hapur.
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    // Hap lidhjen me bazën e të dhënave.
                    connect.Open();

                    // Deklaron komandën SQL për të marrë të dhënat e librave që nuk janë fshirë.
                    string selectData = "SELECT * FROM books WHERE date_delete IS NULL";

                    // Ekzekuton komandën SQL.
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        // Lexon të dhënat nga baza e të dhënave.
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Iteron nëpër rreshtat e lexuar nga lexuesi.
                        while (reader.Read())
                        {
                            // Krijon një objekt të ri DataAddBooks dhe e mbush me të dhënat e lexuara.
                            DataAddBooks dab = new DataAddBooks();
                            dab.ID = (int)reader["id"];
                            dab.BookTitle = reader["book_title"].ToString();
                            dab.Author = reader["author"].ToString();
                            dab.Pulished = reader["published_date"].ToString();
                            dab.image = reader["image"].ToString();
                            dab.Status = reader["status"].ToString();

                            // Shton objektin në listën e të dhënave.
                            listData.Add(dab);
                        }

                        // Mbyll lexuesin e të dhënave.
                        reader.Close();
                    }

                }
                catch (Exception ex)
                {
                    // Shfaq një mesazh gabimi nëse ka ndonjë problem me lidhjen.
                    Console.WriteLine("Error conenecting Database: " + ex);
                }
                finally
                {
                    // Mbyll lidhjen me bazën e të dhënave.
                    connect.Close();
                }
            }
            // Kthen listën e të dhënave të librave.
            return listData;
        }
    }
}
