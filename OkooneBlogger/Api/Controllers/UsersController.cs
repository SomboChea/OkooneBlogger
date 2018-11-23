using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OkooneBlogger.Models;
using OkooneBlogger.Repositories;
using OkooneBlogger.Repositories.Interfaces;

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
        public IEnumerable<User> Get()
        {
            return _userRepository.GetAllWithArticles();
        }
        
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userRepository.GetById(id);
        }
    }
}
