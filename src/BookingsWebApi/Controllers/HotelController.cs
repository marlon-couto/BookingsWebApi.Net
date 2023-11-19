using BookingsWebApi.Dtos;
using BookingsWebApi.Models;
using BookingsWebApi.Repositories;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace BookingsWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController : Controller
{
    private readonly IHotelRepository _repository;
    private readonly IValidator<HotelInsertDto> _validator;
    public HotelController(IHotelRepository repository, IValidator<HotelInsertDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        List<HotelDto> allHotels = await _repository.GetAllHotels();
        return Ok(new { Data = allHotels, Result = "Success" });
    }

    // Admin
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] HotelInsertDto inputData)
    {
        try
        {
            await ValidateInputData(inputData);

            City cityFound = await _repository.GetCityById(inputData.CityId);

            HotelDto createdHotel = await _repository.AddHotel(inputData, cityFound);
            return Created("/api/hotel", new { Data = createdHotel, Result = "Success" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ex.Message, Result = "Error" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ex.Message, Result = "Error" });
        }
    }

    // Admin
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromBody] HotelInsertDto inputData, string id)
    {
        try
        {
            await ValidateInputData(inputData);

            Hotel hotelFound = await _repository.GetHotelById(id);
            City cityFound = await _repository.GetCityById(inputData.CityId);

            HotelDto updatedHotel = _repository.UpdateHotel(hotelFound, cityFound, inputData);
            return Ok(new { Data = updatedHotel, Result = "Success" });
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

    // Admin
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            Hotel hotelFound = await _repository.GetHotelById(id);
            _repository.DeleteHotel(hotelFound);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ex.Message, Result = "Error" });
        }
    }

    private async Task ValidateInputData(HotelInsertDto inputData)
    {
        var validationResult = await _validator.ValidateAsync(inputData);
        if (!validationResult.IsValid)
        {
            List<string> errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ArgumentException(string.Join(" ", errorMessages));
        }
    }
}
