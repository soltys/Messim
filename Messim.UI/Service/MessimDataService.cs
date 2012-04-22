using System.Collections.Generic;
using Messim.UI.Models;
using Messim.UI.Repository;

namespace Messim.UI.Service
{
    public class MessimDataService : IMessimDataService
    {
        private IRepository<Message> messageRepository;
        private IRepository<User> userRepository;

        public MessimDataService(IRepository<Message> messageRepository, IRepository<User> userRepository)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<Message> GetBestPage(int pageNumber)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Message> GetNewPage(int pageNumber)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Message> GetReplays(int parrentMessageId)
        {
            throw new System.NotImplementedException();
        }

        public void SendMessage(Message newMessage)
        {
            throw new System.NotImplementedException();
        }

        public void LikeMessage(int messageId)
        {
            throw new System.NotImplementedException();
        }

        public void DislikeMessage(int messageId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Message> GetUserMessages(User user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Message> GetUserMessages(int userId)
        {
            throw new System.NotImplementedException();
        }

        public void CreateUser(User newUser)
        {
            throw new System.NotImplementedException();
        }
    }
}