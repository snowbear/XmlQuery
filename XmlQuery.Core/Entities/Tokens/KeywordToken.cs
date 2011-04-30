namespace XmlQuery.Entities.Tokens
{
    public class KeywordToken : Token
    {
        public Keyword Keyword { get; private set; }

        public KeywordToken(Keyword keyword)
        {
            Keyword = keyword;
        }
    }
}