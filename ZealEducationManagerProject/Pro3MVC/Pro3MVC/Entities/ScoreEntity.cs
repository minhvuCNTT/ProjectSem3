namespace Pro3MVC.Entities
{
    public class ScoreEntity
    {
        public int Account_Id { get; set; }
        public string Username { get; set; }
        public string Course_Name { get; set; }
        public int Course_Id { get; set; }
        public string Batch_Name { get; set; }
        public string Subject_Name { get; set; }
        public decimal Score { get; set; }
    }
}