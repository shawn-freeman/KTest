using System;
using System.Collections.Generic;
using System.Text;

namespace KonnecticTestCoreUtility.Models.Transports.Fol
{
    public class FolUpdateMessage
    {
        public Guid FolSessionId { get; set; }
        public int UserId { get; set; }
        public int UserTurn { get; set; }
        public bool Active { get; set; }

        public Tuple<string, string> NewWord { get; set; }
    }
}
