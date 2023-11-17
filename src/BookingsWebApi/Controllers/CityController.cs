using BookingsWebApi.Dtos;
using BookingsWebApi.Models;
using BookingsWebApi.Repositories;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace BookingsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        private readonly IValidator<CityInsertDto> _validator;
        public CityController(ICityRepository repository, IValidator<CityInsertDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        // Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                City cityFound = await _repository.GetCityById(id);
                _repository.DeleteCity(cityFound);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message, Result = "Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<CityDto> allCities = await _repository.GetAllCities();
            return Ok(new { Data = allCities, Result = "Success" });
        }

        // Admin
        [HttpPost]
        public async Task<IActionResult> PostAsync(CityInsertDto inputData)
        {
            try
            {
                await ValidateInputData(inputData);

                CityDto createdCity = await _repository.AddCity(inputData);
                return Created("/api/city", new { Data = createdCity, Result = "Success" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message, Result = "Error" });
            }
        }

        // Admin
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] CityInsertDto inputData, string id)
        {
            try
            {
                await ValidateInputData(inputData);

                City cityFound = await _repository.GetCityById(id);

                CityDto updatedCity = _repository.UpdateCity(cityFound, inputData);
                return Ok(new { Data = updatedCity, Result = "Success" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message, Result = "Error" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message, Result = "Error" });
            }
        }

        private async Task ValidateInputData(CityInsertDto inputData)
        {
            var validationResult = await _validator.ValidateAsync(inputData);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ArgumentException(string.Join(" ", errorMessages));
            }
        }
    }
}
