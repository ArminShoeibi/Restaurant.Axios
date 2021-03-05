namespace Restaurant.Axios.ViewModels
{
    public record FoodViewModel
    {
        public int FoodId { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Ingredients { get; init; }
        public string Nationality { get; init; }
        public string PhotoPath { get; init; }
        public string DateCreated { get; init; }
        public string DateModified { get; init; } 
    }
}
