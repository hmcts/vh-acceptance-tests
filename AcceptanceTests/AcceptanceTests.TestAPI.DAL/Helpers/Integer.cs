namespace AcceptanceTests.TestAPI.DAL.Helpers
{
    public class Integer
    {
        public int Value { get; set; }

        public Integer() { }
        
        public Integer(int value) { Value = value; }

        public static implicit operator Integer(int x) { return new Integer(x); }

        public static implicit operator int(Integer x) { return x.Value; }

        public override string ToString()
        {
            return $"Integer({Value})";
        }
    }
}
