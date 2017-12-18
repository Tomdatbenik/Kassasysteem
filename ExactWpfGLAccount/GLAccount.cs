namespace Exact
{
    public class GLAccount
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Price { get; set; }
        public GLAccount(string i, string c, string d)
        {
            ItemCode = i;
            ItemDescription = c;
            Price = d;
        }
        public GLAccount() { }
        public override string ToString()
        {
            return $"{ItemCode}\t{ItemDescription}";
        }
    }
}
