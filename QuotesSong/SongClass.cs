using System;

namespace QuotesSong
{
    /// <summary>
    /// Class that represents a song in playlist
    /// </summary>
    public class SongClass
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int Duration { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public DateTime dt { get; set; }
    }
}
