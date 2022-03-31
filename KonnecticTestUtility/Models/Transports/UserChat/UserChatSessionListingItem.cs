using System;
using System.Collections.Generic;
using System.Text;

namespace KonnecticTestCoreUtility.Models.Transports.UserChat
{
    public class UserChatSessionItem
    {
        public Guid SessionId { get; set; }
        public List<UserInfoModel> SessionMembers{ get; set; }
}
}
