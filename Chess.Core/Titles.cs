using System.Collections.Generic;

namespace Chess.Core
{
    public class Titles
    {
        private List<Title> _titles = new List<Title>();
        
        public Titles()
        {
            this.Populate();
        }

        private void Populate()
        {
            _titles.Add(new Title(string.Empty));
            _titles.Add(new Title("Maître FIDE"));
            _titles.Add(new Title("Maître International"));
            _titles.Add(new Title("Grand Maître International"));
            _titles.Add(new Title("Grand Maître Honoraire"));
        }
    }
}