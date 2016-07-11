using System.Collections.Generic;
using System.Linq;

namespace AkuznetsovReddit.Core.Models
{
    public class ValidationMessageList : List<ValidationMessage>
    {
        public string GetFirstErrorMsg
        {
            get
            {
                return this.Where(m => m.Type == Enums.MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
            }
        }

        public bool HasError
        {
            get
            {
                return this.Where(x => x.Type == Enums.MessageTypes.Error).Any();
            }
        }
    }
}
