using Microsoft.AspNetCore.Mvc;
using Task.Domain.Entities;
using Web.Services;

namespace Web.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _departmentService.GetDepartment());
        }
        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            return Ok(await _departmentService.GetDepartmentByID(Id));
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(Department dep)
        {
            var result = await _departmentService.InsertDepartment(dep);
            if (result.DepartmentId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(Department dep)
        {
            var result = await _departmentService.UpdateDepartment(dep);
            return result != null ? Ok("Updated Successfully") : BadRequest("Error Occured");
        }
        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteDepartment/{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id);
            return result ? new JsonResult("Deleted Successfully") : new JsonResult("Error Occured");
        }
    }
}
