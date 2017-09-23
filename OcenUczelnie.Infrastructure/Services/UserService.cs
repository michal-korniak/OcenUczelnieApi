using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.DTO;
using SimpleCrypto;

namespace OcenUczelnie.Infrastructure.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICryptoService _cryptoService;
        private readonly IMemoryCache _memoryCache;

        public UserService(IUserRepository userRepository, IMapper mapper, ICryptoService cryptoService, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cryptoService = cryptoService;
            _memoryCache = memoryCache;
        }
        public async Task<UserDto> Get(Guid id)
        {
            var user=await _userRepository.GetByIdAsync(id);
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAll()
        {
            var users = await _userRepository.BrowseAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(string email, string name, string password, string role)
        {
            if (await _userRepository.GetByEmailAsync(email)!=null)
                throw new Exception("User with this email already exist.");
            if (await _userRepository.GetByNameAsync(name) != null)
                throw new Exception("User with this name already exist.");

            var salt = _cryptoService.GenerateSalt();
            var hashPassword = _cryptoService.Compute(password, salt);
            var user = new User(Guid.NewGuid(), email, name,
                hashPassword, salt, "user");
            await _userRepository.AddAsync(user);
            _memoryCache.Set("registeredUserId", user.Id,TimeSpan.FromSeconds(5));

        }
    }
}