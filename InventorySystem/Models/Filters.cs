namespace InventorySystem.Models
{
    public class Filters
    {
        public Filters(string filterstring) // filters with filterstring passed in
        {
            filterstring = filterstring ?? "all-all-all"; // filter strings set
            string[] filters = filterstring.Split('-'); // filters list split with -
            CategoryId = filters[0]; // filter 0 = category id
            DateChecked = filters[1]; // filter 1 = datechecked
            StatusId = filters[2]; // filter 2 set to status id
        }
        public string? FilterString { get;} // variable set for filterstring
        public string CategoryId { get;} // variable set for categoryid
        public string DateChecked { get;} // variable set for datechecked
        public string StatusId { get;} // variable set for statusid

        public bool HasCategory => CategoryId.ToLower() != "all"; // boolean variable set for hascategory
        public bool HasDue => DateChecked.ToLower() != "all"; // boolean variable set for hasduedate
        public bool HasStatus => StatusId.ToLower() != "all"; // boolean variable set for hasstatus

        public static Dictionary<string, string> DateCheckedFilterValues =>
            new Dictionary<string, string> // dictionary for datecheckedfiltervalues, key value pairs set:
            {
                {"yesterday", "Yesterday" },
                {"month", "Last Month" },
                {"year", "Last Year" }
            };
        public bool IsYesterday => DateChecked.ToLower() == "yesterday"; // isyesterday variable set
        public bool IsLastMonth => DateChecked.ToLower() == "month"; // islastmonth variable set
        public bool IsLastYear => DateChecked.ToLower() == "year"; // islastyear variable set

    }
}
