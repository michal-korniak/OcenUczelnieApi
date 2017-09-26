using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.DTO;
using OcenUczelnie.Infrastructure.Services.Interfaces;
using SimpleCrypto;

namespace OcenUczelnie.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptoService _cryptoService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, ICryptoService cryptoService,
            IMemoryCache memoryCache, ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cryptoService = cryptoService;
            _memoryCache = memoryCache;
            _tokenProvider = tokenProvider;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAllAsync()
        {
            var users = await _userRepository.BrowseAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(string email, string name, string password, string role)
        {
            if (await _userRepository.GetByEmailAsync(email) != null)
                throw new Exception("User with this email already exist.");
            if (await _userRepository.GetByNameAsync(name) != null)
                throw new Exception("User with this name already exist.");

            var salt = _cryptoService.GenerateSalt();
            var hashPassword = _cryptoService.Compute(password, salt);
            var user = new User(Guid.NewGuid(), email, name,
                hashPassword, salt, "user");
            await _userRepository.AddAsync(user);
            _memoryCache.Set("registeredUserId", user.Id, TimeSpan.FromSeconds(5));
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new Exception("Invalid credentials");
            var generatedHash = _cryptoService.Compute(password, user.Salt);
            if (!_cryptoService.Compare(generatedHash, user.Password))
                throw new Exception("Invalid credentials");
            var token = _tokenProvider.CreateToken(user.Id, user.Role);
            _memoryCache.Set("generatedToken", token, TimeSpan.FromSeconds(5));
        }

        public async Task UpdateAsync(Guid id, string email, string name, string password)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (email != null)
                user.Email = email;
            if (name != null)
                user.Name = name;
            if (password != null)
            {
                user.Salt = _cryptoService.GenerateSalt();
                user.Password = _cryptoService.Compute(password, user.Salt);
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeRoleAsync(Guid id, string role)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.Role = role;
            await _userRepository.UpdateAsync(user);
        }
    }
}