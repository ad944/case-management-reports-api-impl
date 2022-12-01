using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CaseManagementReports.Data;
using CaseManagementReports.Models;

namespace case_management_reports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityResourceGroupController : ControllerBase
    {
        private readonly heroku_00644680628e06dContext _context;

        public CommunityResourceGroupController(heroku_00644680628e06dContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Communityresourcegroup>>> Get()
        {
            try
            {
                List<Communityresourcegroup> communityresourcegroups = _context.Communityresourcegroup.ToList();
                if (communityresourcegroups.Count > 0)
                {
                    return communityresourcegroups;
                }
                else
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "No community resource group found"
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
        public async Task<ActionResult<Communityresourcegroup>> Get(int id)
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

                var communityresourcegroup = await _context.Communityresourcegroup.FindAsync(id);

                if (communityresourcegroup == null)
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "No community resource group found for the provided groupId"
                    };
                    return StatusCode(404, response);
                }

                return Ok(communityresourcegroup);

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
        public async Task<ActionResult<Communityresourcegroup>> Post([FromBody] Communityresourcegroup communityresourcegroup)
        {
            try
            {
                if (communityresourcegroup == null)
                {
                    Response response = new()
                    {
                        statuscode = 500,
                        message = "Unexpected error"
                    };
                    return StatusCode(500);
                }
                communityresourcegroup.CreatedDate = DateTime.Now;
                communityresourcegroup.ModifiedDate = DateTime.Now;

                _context.Communityresourcegroup.Add(communityresourcegroup);
                await _context.SaveChangesAsync();
                return Ok(communityresourcegroup);
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
        public async Task<ActionResult<Communityresourcegroup>> Put(int id, [FromBody] Communityresourcegroup communityresourcegroup)
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

                /*var communityresourcegroup_db = await _context.Communityresourcegroup.FindAsync(id);

                if (communityresourcegroup_db == null)
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "community resource group not updated. Some data missing"
                    };
                    return StatusCode(404, response);
                }*/

                communityresourcegroup.ModifiedDate = DateTime.Now;

                _context.Communityresourcegroup.Update(communityresourcegroup);
                await _context.SaveChangesAsync();
                return Ok(communityresourcegroup);
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
        public async Task<ActionResult<Communityresourcegroup>> Delete(int id)
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

                var communityresourcegroup_db = await _context.Communityresourcegroup.FindAsync(id);

                if (communityresourcegroup_db == null)
                {
                    Response response = new()
                    {
                        statuscode = 404,
                        message = "No community resource group found for the provided groupId"
                    };
                    return StatusCode(404, response);
                }

                Communityresourcegroup deleted_communityresourcegroup = new Communityresourcegroup();
                deleted_communityresourcegroup.GroupId = communityresourcegroup_db.GroupId;
                _context.Communityresourcegroup.Remove(communityresourcegroup_db);
                await _context.SaveChangesAsync();
                return Ok(deleted_communityresourcegroup);

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
