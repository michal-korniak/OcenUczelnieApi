using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, ICryptoService cryptoService,
            IMemoryCache memoryCache, ITokenProvider tokenProvider, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cryptoService = cryptoService;
            _memoryCache = memoryCache;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<User, UserDto>(user);
        }
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
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
            var hashPassword = ComputeHash(password, salt);
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
            if(!user.IsConfirmed)
                throw new Exception("User is not activated.");
            var token = _tokenProvider.CreateToken(user.Id, user.Role);
            _memoryCache.Set("generatedToken", token, TimeSpan.FromSeconds(5));
        }

        public async Task UpdateAsync(Guid id, string name, string password)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (name != null)
                user.Name = name;
            if (password != null)
            {
                var newSalt = _cryptoService.GenerateSalt();
                var newPass = ComputeHash(password, user.Salt);
                user.Salt = newSalt;
                user.Password = newPass;
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeRoleAsync(Guid id, string role)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.Role = role;
            await _userRepository.UpdateAsync(user);
        }

        public async Task GenerateConfirmToken(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user==null)
                throw new Exception("User doesn't exist.");
            if (user.IsConfirmed)
                throw new Exception("User is already activated.");
            var confirmToken = RandomString(8);
            var expiresTime = DateTime.UtcNow.AddHours(1);
            _memoryCache.Set($"confirm-{userId}", confirmToken, expiresTime);

            await _emailSender.SendEmailAsync(user.Email, "Link potwierdzający [OcenUczelnie.pl]",
                $"Twój kod potwierdzający to: \"{confirmToken}\".\nKod jest wazny do {expiresTime:g}");
        }

        public async Task ValidateConfirmToken(Guid userId, string token)
        {
            var validToken = _memoryCache.Get<string>($"confirm-{userId}");
            if (validToken == null)
                throw new Exception("Token is expired.");
            if (validToken != token)
                throw new Exception("Token is invalid.");
            await _userRepository.ConfirmUser(userId);
            _memoryCache.Remove($"confirm-{userId}");
        }

        private string RandomString(uint length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, (int) length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string ComputeHash(string password, string salt)
        {
            if (password.Length < 7)
                throw new Exception("Password should contain at least six characters.");
            return _cryptoService.Compute(password, salt);
        }
    }
}