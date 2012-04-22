using System.Collections.Generic;
using Messim.UI.Models;

namespace Messim.UI.Service
{
    public interface IMessimDataService
    {
        IEnumerable<Message> GetBestPage(int pageNumber);
        IEnumerable<Message> GetNewPage(int pageNumber);
        IEnumerable<Message> GetReplays(int parrentMessageId);
        void SendMessage(Message newMessage);
        void LikeMessage(int messageId);
        void DislikeMessage(int messageId);

        IEnumerable<Message> GetUserMessages(User user);
        IEnumerable<Message> GetUserMessages(int userId);

        void CreateUser(User newUser);
    }
}