using System.Collections.Generic;

namespace Tooded_EFCore
{
    public class Kategooria
    {
        public int Id { get; set; }

        public string KategooriaNimetus { get; set; } = string.Empty;

        public ICollection<Toode> Tooted { get; set; }
            = new List<Toode>();
    }
}