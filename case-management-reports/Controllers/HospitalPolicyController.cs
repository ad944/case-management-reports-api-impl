using Microsoft.AspNetCore.Mvc;
using CaseManagementReports.Data;
using CaseManagementReports.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace case_management_reports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalPolicyController : ControllerBase
    {
        private readonly heroku_00644680628e06dContext _context;

        public HospitalPolicyController(heroku_00644680628e06dContext context)
        {
            _context = context;
        }
       

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospitalpolicy>>> Get()
        {
            try
            {
                List<Hospitalpolicy> hospitalpolicies = _context.Hospitalpolicy.ToList();
                if (hospitalpolicies.Count > 0)
                {
                    return hospitalpolicies;
                }
                else
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "no policies found"
                    };
                    return StatusCode(404, response);
                }
            }
            catch(Exception e)
            {
                Response response = new()
                {
                    statuscode = 500,
                    message = "Unexpected error"
                };
                return StatusCode(500, response);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hospitalpolicy>> Get(int id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    Response response = new()
                    {
                        statuscode = 500,
                        message = "Unexpected error"
                    };
                    return StatusCode(500, response);
                }

                var hospitalPolicy = await _context.Hospitalpolicy.FindAsync(id);

                if (hospitalPolicy == null)
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "No policy found for the provided policyIdentificationId"
                    };
                    return StatusCode(404, response);
                }

                return Ok(hospitalPolicy);
               
            }
            catch (Exception e)
            {
                Response response = new()
                {
                    statuscode = 500,
                    message = "Unexpected error"
                };
                return StatusCode(500, response);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Hospitalpolicy>> Post([FromBody] Hospitalpolicy hospitalpolicy)
        {
            try
            {
                if (hospitalpolicy == null)
                {
                    Response response = new()
                    {
                        statuscode = 500,
                        message = "Unexpected error"
                    };
                    return StatusCode(500);
                }

                hospitalpolicy.CreatedDate = DateTime.Now;
                hospitalpolicy.ModifiedDate = DateTime.Now;

                _context.Hospitalpolicy.Add(hospitalpolicy);
                await _context.SaveChangesAsync();
                return Ok(hospitalpolicy);
            }
            catch (Exception e)
            {
                Response response = new()
                {
                    statuscode = 500,
                    message = "Unexpected error"
                };
                return StatusCode(500, response);
            }

            
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Hospitalpolicy>> Put(int id,[FromBody] Hospitalpolicy hospitalpolicy)
        {
            try
            {
                if (id == null || id == 0)
                {
                    Response response = new()
                    {
                        statuscode = 500,
                        message = "Unexpected error"
                    };
                    return StatusCode(500, response);
                }

                /*var hospitalPolicy = await _context.Hospitalpolicy.FindAsync(id);

                if (hospitalPolicy == null)
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "policy not updated. Some data missing"
                    };
                    return StatusCode(404, response);
                }*/

                hospitalpolicy.ModifiedDate = DateTime.Now;

                _context.Hospitalpolicy.Update(hospitalpolicy);
                await _context.SaveChangesAsync();
                return Ok(hospitalpolicy);
            }
            catch (Exception e)
            {
                Response response = new()
                {
                    statuscode = 500,
                    message = "Unexpected error"
                };
                return StatusCode(500, response);
            }
            
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Hospitalpolicy>> Delete(int id)
        {

            try
            {
                if (id == null || id == 0)
                {
                    Response response = new()
                    {
                        statuscode = 500,
                        message = "Unexpected error"
                    };
                    return StatusCode(500, response);
                }

                var hospitalPolicy = await _context.Hospitalpolicy.FindAsync(id);

                if (hospitalPolicy == null)
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "no hospital policy found"
                    };
                    return StatusCode(404, response);
                }

                Hospitalpolicy deleted_hospitalpolicy = new Hospitalpolicy();
                deleted_hospitalpolicy.PolicyIdentificationId = hospitalPolicy.PolicyIdentificationId;
                _context.Hospitalpolicy.Remove(hospitalPolicy);
                await _context.SaveChangesAsync();
                return Ok(deleted_hospitalpolicy);

            }
            catch (Exception e)
            {
                Response response = new()
                {
                    statuscode = 500,
                    message = "Unexpected error"
                };
                return StatusCode(500, response);
            }

            
        }
    }
}
