using AutoMapper;

using BookingsWebApi.Dtos;
using BookingsWebApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BookingsWebApi.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly IBookingsDbContext _context;
        private readonly IMapper _mapper;
        public CityRepository(IBookingsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CityDto>> GetAllCities()
        {
            return await _context.Cities.Select(c => _mapper.Map<CityDto>(c)).ToListAsync();
        }

        public async Task<CityDto> AddCity(CityInsertDto cityInsert)
        {
            City newCity = _mapper.Map<City>(cityInsert);
            newCity.CityId = Guid.NewGuid().ToString();

            await _context.Cities.AddAsync(newCity);
            _context.SaveChanges();

            return _mapper.Map<CityDto>(newCity);
        }

        public async Task<City?> GetCityById(string cityId)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
        }

        public CityDto UpdateCity(CityInsertDto cityInsert, City cityFound)
        {
            City newCity = _mapper.Map<City>(cityInsert);
            newCity.CityId = cityFound.CityId;

            _context.Cities.Update(newCity); // TODO: mudar essa implementação para um método assíncrono.
            _context.SaveChanges();

            CityDto updatedCity = _mapper.Map<CityDto>(newCity);
            return updatedCity;
        }
    }
}