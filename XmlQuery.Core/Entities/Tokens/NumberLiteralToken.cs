namespace XmlQuery.Entities.Tokens
{
    public class NumberLiteralToken : Token
    {
        public double Value { get; private set; }

        public NumberLiteralToken(double value)
        {
            Value = value;
        }
    }
}