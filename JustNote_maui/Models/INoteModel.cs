using System;
using System.Collections.Generic;
using System.Text;

namespace JustNote_maui.Models
{
    public interface INoteModel
    {
        public Type Type { get; }
        int Id { get; set; } 
        string NoteTitle { get; set; }
        DateTime? CreationDataTime { get; set; }
        DateTime LastEditDataTime { get; set; }
    }
}
