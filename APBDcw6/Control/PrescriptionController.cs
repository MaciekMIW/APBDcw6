using APBDcw6.Models.DTO;
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
        [HttpPost]
        public async Task<IActionResult> GetPrescription(InputPrescription prescription)
        {
            return Ok();
        }
    }
}
