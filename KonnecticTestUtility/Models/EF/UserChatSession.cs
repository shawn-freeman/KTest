using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KonnecticTestCoreUtility.Models.EF
{
    public class UserChatSession
    {
        public Guid UserChatSessionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        
        public ICollection<User2UserChatSession> User2UserChatSessions { get; set; }
        public List<UserChatMessage> UserChatMessages { get; set; }
    }
}
