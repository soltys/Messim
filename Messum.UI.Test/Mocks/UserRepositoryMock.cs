using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Messim.UI.Models;
using Messim.UI.Repository;
using Moq;

namespace Messum.UI.Test.Mocks
{
    public class UserRepositoryMock
    {
        public static Mock<UserRepository> Create()
        {
            var repo = new Mock<UserRepository>();
            var repoData = new List<User>
                               {
                                   new User
                                       {ID = 1, Username = "soltys", Password = "soltys_password", Subscribents = null},
                                   new User
                                       {ID = 2, Username = "milkman", Password = "milky_way", Subscribents = null},
                                   new User
                                       {
                                           ID = 3,
                                           Username = "ordinaryUser",
                                           Password = "ordinaryPassword",
                                           Subscribents = null
                                       }
                               };
            repo.Setup(r => r.FindAll()).Returns(repoData);
            repo.Setup(r => r.FindBy(It.IsAny<Func<User, bool>>())).Returns((Func<User, bool> where) => repoData.Where(where));
            repo.Setup(r => r.FindById(It.IsAny<int>())).Returns((int id) => repoData.SingleOrDefault(user => user.ID == id));
            repo.Setup(r => r.Add(It.IsAny<User>())).Callback((User newUser) => repoData.Add(newUser));
            return repo;
        }
    }
}
