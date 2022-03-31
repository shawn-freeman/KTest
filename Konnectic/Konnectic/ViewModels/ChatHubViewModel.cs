using KonnecticTestCoreUtility.Handlers;
using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.Transports.UserChat;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konnectic.ViewModels
{
    public class ChatHubViewModel : BaseViewModel
    {
        private UserChatHandler _userChatHandler = new UserChatHandler();
        private HubConnection _hubConnection;
        private UserChatSessionItem _sessionInfo;

        private string _sessionUsernames;
        public string SessionUsernames
        {
            get
            {
                if (_sessionUsernames == null) _sessionUsernames = "";

                return _sessionUsernames;
            }
            set
            {
                SetProperty(ref _sessionUsernames, value);
            }
        }

        private string _messageDraft;
        public string MessageDraft
        {
            get
            {
                if (_messageDraft == null) _messageDraft = "";

                return _messageDraft;
            }
            set
            {
                SetProperty(ref _messageDraft, value);
            }
        }

        ObservableCollection<UserChatMessageInfo> _message;
        public ObservableCollection<UserChatMessageInfo> Messages
        {
            get {
                if (_message == null) _message = new ObservableCollection<UserChatMessageInfo>();

                return _message; }
            set {
                SetProperty(ref _message, value);
            }
        }

        public Action<UserChatMessageInfo> OnConversationUpdated { get; set; }

        public static async Task<ChatHubViewModel> New(UserChatSessionItem session)
        {
            var model = new ChatHubViewModel(session);
            await model.Init();
            return model;
        }

        private ChatHubViewModel(UserChatSessionItem session)
        {
            _sessionInfo = session;

            //TODO: Populate with concatenated string of member usernames
            _sessionUsernames = session.SessionMembers.FirstOrDefault(u => u.UserId != App.AccessToken.UserId).Username;
        }

        public async Task Init()
        {
            IsBusy = true;

            var returnResult = await _userChatHandler.GetUserChatSessionMessages(_sessionInfo.SessionId);

            Messages = returnResult.Error.IsError ?
                new ObservableCollection<UserChatMessageInfo>() :
                new ObservableCollection<UserChatMessageInfo>(returnResult.Result);

            //localhost for UWP / iOS or special IP for Android
            var ip = "localhost";
            if (Device.RuntimePlatform == Device.Android)
                ip = "10.0.2.2";

            _hubConnection = new HubConnectionBuilder()
                //.WithUrl($"http://{id}:5000/chatHub")
                .WithUrl($"{KonnecticTestAppSettings.KonnecticTestBaseUrl}/ChatHub")
                .Build();

            _hubConnection.On<UserChatMessageInfo>(_sessionInfo.SessionId.ToString(), (msgObj) =>
            {
                Messages.Add(msgObj);
                if(OnConversationUpdated != null) OnConversationUpdated.Invoke(msgObj);
            });

            await Connect();
            IsBusy = false;
        }

        private async Task Connect()
        {
            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                //Something has gone wrong
            }
        }

        public async Task SendMessage()
        {
            try
            {
                //await _hubConnection.SendAsync("SendMessage", "User", "TheMessage"); //Send and forget
                await _hubConnection.InvokeAsync("SendMessage", _sessionInfo.SessionId, App.AccessToken.UserId, MessageDraft);  //Send and get a response

                MessageDraft = "";
            }
            catch (Exception ex)
            {
                //send failed
            }
        }
    }
}
