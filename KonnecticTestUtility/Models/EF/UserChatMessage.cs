using System;
using System.Collections.Generic;
using System.Text;

namespace KonnecticTestCoreUtility.Models.EF
{
    public class UserChatMessage
    {
        public int UserChatMessageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Message { get; set; }
        public bool HasSeen { get; set; }

        public User User { get; set; }
        public UserChatSession UserChatSession { get; set; }

    }
}
