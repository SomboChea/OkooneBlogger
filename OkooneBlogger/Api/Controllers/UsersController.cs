using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Models;
using OkooneBlogger.Repositories.Interfaces;
using System.Collections.Generic;

namespace OkooneBlogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> Get(string with = "")
        {
            return with.ToLower().Equals("articles") ? _userRepository.GetAllWithArticles() : _userRepository.GetAll();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userRepository.GetById(id);
        }
    }
}