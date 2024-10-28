namespace General_api.DTOs
{
    public class BeerUpdateDto
    {
        public int Id { get; set; }
        public int BrandID { get; set; }
        public string Name { get; set; }
        public decimal Alcohol { get; set; }
    }
}
