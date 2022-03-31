using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KonnecticTestCoreUtility.Models.EF
{
    public class User2UserChatSession
    {
        public Guid UserChatSessionId { get; set; }
        public UserChatSession UserChatSession { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
