using System;

namespace Infrastructure.Models
{
    public class Conversation
    {
        public int IdConversation { get; set; }

        public string? Titre { get; set; }

        public string? LienPhoto { get; set; }
    }
}