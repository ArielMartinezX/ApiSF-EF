namespace Application.DTOs
{
    public class CreateUpdateEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsAlive { get; set; }
    }
}
