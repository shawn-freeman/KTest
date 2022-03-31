using System;
using System.Collections.Generic;
using System.Text;

namespace KonnecticTestCoreUtility.Models.Transports.UserChat
{
    public class UserChatInfo
    {
        public Guid SessionId { get; set; }
        public int? OwnerUserId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
