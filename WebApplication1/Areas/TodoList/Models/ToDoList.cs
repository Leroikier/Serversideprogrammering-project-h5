using System;
using System.Collections.Generic;

namespace WebApplication1.Areas.TodoList.Models
{
    public partial class ToDoList
    {
        public int Id { get; set; }
        public string Titel { get; set; } = null!;
        public string? Description { get; set; }
        public string Username { get; set; } = null!;
    }
}
