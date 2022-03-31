using KonnecticTestCoreUtility.Models.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace KonnecticTestCoreUtility.Models.Transports.Fol
{
    public class FolResponseMessage
    {
        public Guid FolSessionId { get; set; }
        public bool Active { get; set; }
        public int UserTurn { get; set; }

        public string PrimeWord { get; set; }
        public WordInfo Word2 { get; set; }
        public WordInfo Word3 { get; set; }
        public WordInfo Word4 { get; set; }
        public WordInfo Word5 { get; set; }
        public WordInfo Word6 { get; set; }
        public WordInfo Word7 { get; set; }
        public WordInfo Word8 { get; set; }
        public WordInfo Word9 { get; set; }
        public WordInfo Word10 { get; set; }
        public WordInfo Word11 { get; set; }
        public WordInfo Word12 { get; set; }
        public WordInfo Word13 { get; set; }
        public WordInfo Word14 { get; set; }
        public WordInfo Word15 { get; set; }
        public WordInfo Word16 { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }

        public FolResponseMessage() { }

        public FolResponseMessage(DB_Fol_Session folSession)
        {
            FolSessionId = folSession.FolSessionId;
            Active = folSession.Active;
            UserTurn = folSession.UserTurn;

            PrimeWord = folSession.PrimeWord;
            Word2 = new WordInfo() { SquenceNumber = 2, Value = folSession.Word2, IsActive = string.IsNullOrWhiteSpace(folSession.Word2) };
            Word3 = new WordInfo() { SquenceNumber = 3, Value = folSession.Word3, IsActive = !string.IsNullOrWhiteSpace(folSession.Word2) };
            Word4 = new WordInfo() { SquenceNumber = 4, Value = folSession.Word4, IsActive = !string.IsNullOrWhiteSpace(folSession.Word3) };
            Word5 = new WordInfo() { SquenceNumber = 5, Value = folSession.Word5, IsActive = !string.IsNullOrWhiteSpace(folSession.Word4) };
            Word6 = new WordInfo() { SquenceNumber = 6, Value = folSession.Word6, IsActive = !string.IsNullOrWhiteSpace(folSession.Word5) };
            Word7 = new WordInfo() { SquenceNumber = 7, Value = folSession.Word7, IsActive = !string.IsNullOrWhiteSpace(folSession.Word6) };
            Word8 = new WordInfo() { SquenceNumber = 8, Value = folSession.Word8, IsActive = !string.IsNullOrWhiteSpace(folSession.Word7) };
            Word9 = new WordInfo() { SquenceNumber = 9, Value = folSession.Word9, IsActive = !string.IsNullOrWhiteSpace(folSession.Word8) };
            Word10 = new WordInfo() { SquenceNumber = 10, Value = folSession.Word10, IsActive = !string.IsNullOrWhiteSpace(folSession.Word9) };
            Word11 = new WordInfo() { SquenceNumber = 11, Value = folSession.Word11, IsActive = !string.IsNullOrWhiteSpace(folSession.Word10) };
            Word12 = new WordInfo() { SquenceNumber = 12, Value = folSession.Word12, IsActive = !string.IsNullOrWhiteSpace(folSession.Word11) };
            Word13 = new WordInfo() { SquenceNumber = 13, Value = folSession.Word13, IsActive = !string.IsNullOrWhiteSpace(folSession.Word12) };
            Word14 = new WordInfo() { SquenceNumber = 14, Value = folSession.Word14, IsActive = !string.IsNullOrWhiteSpace(folSession.Word13) };
            Word15 = new WordInfo() { SquenceNumber = 15, Value = folSession.Word15, IsActive = !string.IsNullOrWhiteSpace(folSession.Word14) };
            Word16 = new WordInfo() { SquenceNumber = 16, Value = folSession.Word16, IsActive = !string.IsNullOrWhiteSpace(folSession.Word15) };

            User1 = folSession.User1;
            User2 = folSession.User2;
        }
    }
}
