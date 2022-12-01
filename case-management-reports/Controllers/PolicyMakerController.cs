using CaseManagementReports.Data;
using Microsoft.AspNetCore.Mvc;
using CaseManagementReports.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace case_management_reports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyMakerController : ControllerBase
    {
        private readonly heroku_00644680628e06dContext _context;

        public PolicyMakerController(heroku_00644680628e06dContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policymaker>>> Get()
        {
            try
            {
                List<Policymaker> policymakers = _context.Policymaker.ToList();
                if (policymakers.Count > 0)
                {
                    foreach(Policymaker policymaker in policymakers)
                    {
                        List<Hospitalpolicy> hospitalpolicies = _context.Hospitalpolicy.Where(h => h.PolicyMakerId == policymaker.PolicyMakerId).ToList();
                        if(hospitalpolicies.Count > 0)
                        {
                            foreach(Hospitalpolicy hospitalpolicy in hospitalpolicies)
                            {
                                hospitalpolicy.PolicyMaker = null;
                            }
                            policymaker.Hospitalpolicy = hospitalpolicies;
                        }
                        
                    }
                    return policymakers;
                }
                else
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "no policy makers found"
                    };
                    return StatusCode(404, response);
                }
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Policymaker>> Get(int id)
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

                var policymaker = _context.Policymaker.FirstOrDefault(p => p.PolicyMakerId == id);

                if (policymaker == null)
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "No policy maker found for the provided policyMakerId"
                    };
                    return StatusCode(404, response);
                }

                List<Hospitalpolicy> hospitalpolicies = _context.Hospitalpolicy.Where(h => h.PolicyMakerId == policymaker.PolicyMakerId).ToList();
                if (hospitalpolicies.Count > 0)
                {
                    foreach (Hospitalpolicy hospitalpolicy in hospitalpolicies)
                    {
                        hospitalpolicy.PolicyMaker = null;
                    }
                    policymaker.Hospitalpolicy = hospitalpolicies;
                }

                return Ok(policymaker);

            }
            catch (Exception e)
            {
                Response response = new Response();
                response.statuscode = 500;
                response.message = "Unexpected error";
                return StatusCode(500, response);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Policymaker>> Post([FromBody] Policymaker policymaker)
        {
            try
            {
                if (policymaker == null)
                {
                    Response response = new()
                    {
                        statuscode = 500,
                        message = "Unexpected error"
                    };
                    return StatusCode(500, response);
                }

                _context.Policymaker.Add(policymaker);
                await _context.SaveChangesAsync();
                List<Hospitalpolicy> hospitalpolicies = _context.Hospitalpolicy.Where(h => h.PolicyMakerId == policymaker.PolicyMakerId).ToList();
                if (hospitalpolicies.Count > 0)
                {
                    foreach (Hospitalpolicy hospitalpolicy in hospitalpolicies)
                    {
                        hospitalpolicy.PolicyMaker = null;
                    }
                    policymaker.Hospitalpolicy = hospitalpolicies;
                }
                return Ok(policymaker);
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
        public async Task<ActionResult<Policymaker>> Put(int id, [FromBody] Policymaker policymaker)
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

                _context.Policymaker.Update(policymaker);
                await _context.SaveChangesAsync();
                List<Hospitalpolicy> hospitalpolicies = _context.Hospitalpolicy.Where(h => h.PolicyMakerId == policymaker.PolicyMakerId).ToList();
                if (hospitalpolicies.Count > 0)
                {
                    foreach (Hospitalpolicy hospitalpolicy in hospitalpolicies)
                    {
                        hospitalpolicy.PolicyMaker = null;
                    }
                    policymaker.Hospitalpolicy = hospitalpolicies;
                }
                return Ok(policymaker);
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
        public async Task<ActionResult<Policymaker>> Delete(int id)
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

                var policymaker_db =  _context.Policymaker.FirstOrDefault(p => p.PolicyMakerId == id);

                if (policymaker_db == null)
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "No policy maker found for the provided policyMakerId"
                    };
                    return StatusCode(404, response);
                }

                Policymaker deleted_policymaker = new Policymaker();
                deleted_policymaker.PolicyMakerId = policymaker_db.PolicyMakerId;
                _context.Policymaker.Remove(policymaker_db);
                await _context.SaveChangesAsync();
                return Ok(deleted_policymaker);

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
