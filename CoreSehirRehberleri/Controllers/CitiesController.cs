using AutoMapper;
using CoreSehirRehberleri.Data;
using CoreSehirRehberleri.Dtos;
using CoreSehirRehberleri.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CoreSehirRehberleri.Controllers
{
    [Route("api/cities")]
    [ApiController]
    [Produces("application/json")]
    public class CitiesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;

        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        public IActionResult GetCities()
        {
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);

            return Ok(citiesToReturn);
        }
         
        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody]City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();

            return Ok(city);
        }

        [HttpGet]
        [Route("detail")]
        public IActionResult GetCityById(int id)
        {
            var city=_appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);

            return Ok(cityToReturn);

        }
        [HttpGet]
        [Route("photos")]
        public IActionResult GetPhotoByCity(int cityid)
        {
            var photos = _appRepository.GetPhotoByCity(cityid);
           
            return Ok(photos);
        }



    }
}
