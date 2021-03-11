using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class SermonController : BaseApiController
    {
        private readonly GNBCContext _context;
        public SermonController(GNBCContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sermon>>> GetSermons()
        {
            return await _context.Sermons.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sermon>> GetSermons(Guid id)
        {
            return await _context.Sermons.FindAsync(id);
        }
    }
}