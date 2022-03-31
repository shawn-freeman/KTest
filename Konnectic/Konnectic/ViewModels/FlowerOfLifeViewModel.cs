using KonnecticTestCoreUtility.Handlers;
using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.Transports.Fol;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Konnectic.ViewModels
{
    public class FlowerOfLifeViewModel : BaseViewModel
    {
        private FolHandler _folHandler = new FolHandler();
        private HubConnection _hubConnection;

        private Guid SessionId;
        private List<int> UserIds;

        private string _selectedWord;
        public string SelectedWord
        {
            get
            {
                if (_selectedWord == null) _selectedWord = "";

                return _selectedWord;
            }
            set
            {
                SetProperty(ref _selectedWord, value);
            }
        }

        private FolResponseMessage _sessionInfo;
        public FolResponseMessage SessionInfo
        {
            get
            {
                if (_sessionInfo == null) _sessionInfo = new FolResponseMessage();

                return _sessionInfo;
            }
            set
            {
                SetProperty(ref _sessionInfo, value);
            }
        }

        private ObservableCollection<string> _wordSuggestions;
        public ObservableCollection<string> WordSuggestions
        {
            get
            {
                if (_wordSuggestions == null) _wordSuggestions = new ObservableCollection<string>();

                return _wordSuggestions;
            }
            set
            {
                SetProperty(ref _wordSuggestions, value);
            }
        }

        public static async Task<FlowerOfLifeViewModel> New(Guid sessionId)
        {
            var model = new FlowerOfLifeViewModel(sessionId);
            await model.Init();
            return model;
        }

        public FlowerOfLifeViewModel(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public async Task Init()
        {
            IsBusy = true;

            ReturnResult<FolResponseMessage> sessionResult = await _folHandler.GetFolSession(SessionId);

            ErrorInfo = sessionResult.Error;

            if (sessionResult.Error.IsError) return;

            SessionInfo = sessionResult.Result;
            WordSuggestions = new ObservableCollection<string>(new List<string>() { "Love", "Happy", "Sad", "Hate", "Ecstatic" });

            _hubConnection = new HubConnectionBuilder()
                //.WithUrl($"http://{id}:5000/chatHub")
                .WithUrl($"{KonnecticTestAppSettings.KonnecticTestBaseUrl}/FolHub")
                .Build();

            _hubConnection.On<FolResponseMessage>(_sessionInfo.FolSessionId.ToString(), (responseMsg) =>
            {
                SessionInfo = responseMsg;
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
            IsBusy = true;
            try
            {
                var msg = new FolUpdateMessage()
                {
                    FolSessionId = SessionInfo.FolSessionId,
                    UserId = App.AccessToken.UserId,
                    UserTurn = SessionInfo.UserTurn,
                    Active = true,
                    NewWord = GetNewWordTuple()
                };
                //await _hubConnection.SendAsync("SendMessage", "User", "TheMessage"); //Send and forget
                await _hubConnection.InvokeAsync("FolUpdate", msg);  //Send and get a response
            }
            catch (Exception ex)
            {
                //send failed
            }
            IsBusy = false;
        }

        private Tuple<string, string> GetNewWordTuple()
        {
            if (SessionInfo.Word3.IsActive) return new Tuple<string, string>("Word3", SelectedWord);
            if (SessionInfo.Word4.IsActive) return new Tuple<string, string>("Word4", SelectedWord);
            if (SessionInfo.Word5.IsActive) return new Tuple<string, string>("Word5", SelectedWord);
            if (SessionInfo.Word6.IsActive) return new Tuple<string, string>("Word6", SelectedWord);
            if (SessionInfo.Word7.IsActive) return new Tuple<string, string>("Word7", SessionInfo.Word7.Value);
            if (SessionInfo.Word8.IsActive) return new Tuple<string, string>("Word8", SessionInfo.Word8.Value);
            if (SessionInfo.Word9.IsActive) return new Tuple<string, string>("Word9", SessionInfo.Word9.Value);
            if (SessionInfo.Word10.IsActive) return new Tuple<string, string>("Word10", SessionInfo.Word10.Value);
            if (SessionInfo.Word11.IsActive) return new Tuple<string, string>("Word11", SessionInfo.Word11.Value);
            if (SessionInfo.Word12.IsActive) return new Tuple<string, string>("Word12", SessionInfo.Word12.Value);
            if (SessionInfo.Word13.IsActive) return new Tuple<string, string>("Word13", SessionInfo.Word13.Value);
            if (SessionInfo.Word14.IsActive) return new Tuple<string, string>("Word14", SessionInfo.Word14.Value);
            if (SessionInfo.Word15.IsActive) return new Tuple<string, string>("Word15", SessionInfo.Word15.Value);
            if (SessionInfo.Word16.IsActive) return new Tuple<string, string>("Word16", SessionInfo.Word16.Value);

            return new Tuple<string, string>("Word2", SelectedWord);
        }
    }
}
