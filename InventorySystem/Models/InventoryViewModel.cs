namespace InventorySystem.Models
{
    public class InventoryViewModel
    {
        public Filters? Filters { get; set; } = new Filters("all-all-all"); // filters variable
        public List<Status> Statuses { get; set; } = new List<Status>(); // variable for statuses
        public List<Category> Categories { get; set; } = new List<Category>(); // variable for categories list
        public Dictionary<string, string> DateCheckedFilters { get; set; } = new Dictionary<string, string>(); // variable for DateCheckedFilters Dictionary
        public List<Inventory> Inventories { get; set; } = new List<Inventory>(); // variable for Inventories list

        public Inventory CurrentProduct { get; set; } = new Inventory(); // variable for currentproduct
    }
}
