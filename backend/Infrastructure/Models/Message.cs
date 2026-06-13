using System;

namespace Infrastructure.models
{
    public class Message
    {
        public int IdMessage { get; set; }

        public int IdConversation { get; set; }

        public int IdUtilisateur { get; set; }

        public string ContenuMessage { get; set; } = string.Empty;

        public DateTime DateHeureMessage { get; set; }
    }
}