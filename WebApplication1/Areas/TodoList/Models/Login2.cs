using System;
using System.Collections.Generic;

namespace WebApplication1.Areas.TodoList.Models
{
    public partial class Login2
    {
        public int Id { get; set; }
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte[]? Salt { get; set; }
    }
}
