using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClanSystem.Models.WebApiModels
{
    public class JsonNotification
    {
        public long ID { get; set; }
        [Required]
        public DateTime SendTime { get; set; }
        public Importance? Importance { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Author { get; set; }

        public JsonNotification()
        {
        }

        public JsonNotification(Notification n)
        {
            ID = n.ID;
            SendTime = n.SendTime;
            Importance = n.Importance;
            Title = n.Title;
            Content = n.Content;
            Author = n.Author.UserName;
        }
    }
}