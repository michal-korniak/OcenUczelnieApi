using System;
using System.Collections;
using System.Collections.Generic;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Core.Repositories
{
    public interface IUserRepository: IRepository
    {
        void Add(User user);
        void Remove(User user);
        void Update(User user);
        User Get(Guid id);
        IEnumerable<User> BrowseAll();
    }
}