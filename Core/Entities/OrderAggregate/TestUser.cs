namespace Core.Entities.OrderAggregate
{
    public class TestUser : BaseEntity
    {
        public TestUser()
        {
        }

        public string Name { get; set; }
        public string dateOfBirth { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string educationStatus { get; set; }
        
    }
}