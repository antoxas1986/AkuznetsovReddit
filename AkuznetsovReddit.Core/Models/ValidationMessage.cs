using AkuznetsovReddit.Core.Enums;

namespace AkuznetsovReddit.Core.Models
{
    public class ValidationMessage
    {
        public ValidationMessage(MessageTypes type, string messsageText)
        {
            Type = type;
            Text = messsageText;
        }

        public string Text { get; set; }
        public MessageTypes Type { get; set; }
    }
}
