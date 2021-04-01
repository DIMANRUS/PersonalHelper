using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalHelper.Models {
    public enum TypesTodo { 
        Complete, Do
    }
    public class TodoItem {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public DateTime DateRemember { get; set; }
        public TypesTodo TypeTodo { get; set; }
    }
}