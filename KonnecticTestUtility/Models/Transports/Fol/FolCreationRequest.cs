using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonnecticTestCoreUtility.Models.Transports.Fol
{
    public class FolCreationRequest
    {
        public int requesterUserId { get; set; }
        public int requestedUserId {get; set; }
    }
}
