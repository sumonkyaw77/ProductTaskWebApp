namespace TaskProductWebApplication.Models
{
    public class Product
    {
        public int ProductID { get; set; }       // Primary Key
        public string ProductName { get; set; }  // Product column
        public string Description { get; set; }  // Description column
        public DateTime Created { get; set; }    // Created column
    }
}
