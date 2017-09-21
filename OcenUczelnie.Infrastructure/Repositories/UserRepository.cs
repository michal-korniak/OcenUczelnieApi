using System;
using System.Collections.Generic;
using System.Linq;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.EF;

namespace OcenUczelnie.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly OcenUczelnieContext _context;

        public UserRepository(OcenUczelnieContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChangesAsync();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChangesAsync();
        }

        public User Get(Guid id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> BrowseAll()
        {
            return _context.Users.ToList();
        }
    }
}