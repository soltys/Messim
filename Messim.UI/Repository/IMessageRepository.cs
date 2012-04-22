using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Messim.UI.Models;

namespace Messim.UI.Repository
{
    interface IMessageRepository
    {
        IEnumerable<Message> GetBestPage(int pageNumber);
        IEnumerable<Message> GetNewPage(int pageNumber);
        IEnumerable<Message> GetReplays(int parrentMessageId);
        void SendMessage(Message newMessage);
        void LikeMessage(int messageId);
        void DislikeMessage(int messageId);

        IEnumerable<Message> GetUserMessages(User user);
        IEnumerable<Message> GetUserMessages(int userId);

    }
}
