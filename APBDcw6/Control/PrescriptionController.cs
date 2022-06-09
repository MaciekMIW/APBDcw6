using APBDcw6.Models.DTO;
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
    public class PrescriptionController : ControllerBase
    {
        private readonly IDbService _dbService;
        public PrescriptionController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpPost]
        public async Task<IActionResult> GetPrescription(InputPrescription prescription)
        {
            var result = await _dbService.GetPrescription(prescription);
            return Ok();
        }
    }
}
