using APBDcw6.Models;
using APBDcw6.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDcw6.Control
{
    [ApiController]
    [Route("api/{controller}")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public DoctorsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _dbService.GetAllDoctors();
            return Ok(doctors);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await _dbService.GetDoctor(id);
            return Ok(doctor);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            await _dbService.DeleteDoctor(id);
            return Ok("Doctor "+ id + " deleted!");
        }
        [HttpPost]
        public async Task<IActionResult> AddDoctor(ReturnedDoctor doctor)
        {
            await _dbService.AddDoctor(doctor);
            return Ok();
        }
    }
}
