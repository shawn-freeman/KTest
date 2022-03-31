using System;
using System.Collections.Generic;

namespace KonnecticTestCoreUtility.Models.EF
{
    public class User
    {
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<User2UserChatSession> User2UserChatSessions { get; set; }
        public ICollection<UserChatMessage> UserChatMessages { get; set; }
    }
}
