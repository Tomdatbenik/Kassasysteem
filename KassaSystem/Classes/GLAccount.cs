namespace KassaSystem.Classes
{
    public class GLAccount
    {
        public string ID = "";
        public string Code = "";
        public string Description = "";
        public GLAccount(string i, string c, string d)
        {
            ID = i;
            Code = c;
            Description = d;
        }
        public GLAccount() { }
        public override string ToString()
        {
            return $"{Code}\t{Description}";
        }
    }
}
