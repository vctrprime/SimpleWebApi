namespace SimpleWebApi.Domain.BreakingBad.Quotes
{
    /// <summary>
    /// Quote from Breaking Bad model
    /// </summary>
    public class Quote
    {
        /// <summary>
        /// Quote Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Quote Text
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Quote Author
        /// </summary>
        public string Author { get; set; }
    }
}