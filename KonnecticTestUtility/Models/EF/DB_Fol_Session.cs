using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KonnecticTestCoreUtility.Models.EF
{
    public class DB_Fol_Session
    {
        public Guid FolSessionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
        public int User1_Id { get; set; }
        public int? User2_Id { get; set; }
        public int UserTurn { get; set; }


        public string PrimeWord { get; set; }
        public string Word2 { get; set; }
        public string Word3 { get; set; }
        public string Word4 { get; set; }
        public string Word5 { get; set; }
        public string Word6 { get; set; }
        public string Word7 { get; set; }
        public string Word8 { get; set; }
        public string Word9 { get; set; }
        public string Word10 { get; set; }
        public string Word11 { get; set; }
        public string Word12 { get; set; }
        public string Word13 { get; set; }
        public string Word14 { get; set; }
        public string Word15 { get; set; }
        public string Word16 { get; set; }
        
        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}
