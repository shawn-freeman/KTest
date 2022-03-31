using System;

namespace KonnecticTestCoreUtility.Models.Transports.UserChat
{
    public class UserChatMessageInfo 
    {
        public int OwnerId { get; set; }
        public string OwnerUsername { get; set; }
        public int UserChatMessageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Message { get; set; }
        public bool HasSeen { get; set; }
    }
}
