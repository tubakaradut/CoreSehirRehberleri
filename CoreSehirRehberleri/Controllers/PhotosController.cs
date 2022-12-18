using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CoreSehirRehberleri.Data;
using CoreSehirRehberleri.Dtos;
using CoreSehirRehberleri.Helpers.Cloudinary;
using CoreSehirRehberleri.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreSehirRehberleri.Controllers
{
    [Route("api/cities/{id}/photos")] //api de şehirler için photo ekleneceğini için 
    [ApiController]
    [Produces("application/json")]
    public class PhotosController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        private IOptions<CloudinarySettings> _cloudinaryConfig;

        private Cloudinary _cloudinary;

        public PhotosController(IAppRepository appRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _appRepository = appRepository;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);

        }

        [HttpPost]
        [Route("photoaded")]
        public IActionResult AddPhotoForCity(int cityid, [FromBody] PhotoForCreationDto photoForCreationDto)
        {
            var city = _appRepository.GetCityById(cityid);
            if (city == null)
            {
                return BadRequest("Could not find the city");
            }

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId != city.UserId) //eklenmek istenen şehrin kullanıcısı ile sistemdeki o anki kullanıcı aynı ise eklenmeli
            {
                return Unauthorized();
            }
            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Url.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.City = city;

            if (!city.Photos.Any(p=>p.IsMain))
            {
                photo.IsMain = true;
            }
            city.Photos.Add(photo);

            if (_appRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto",new {id=photo.Id },photoToReturn);
            }

            return BadRequest("Could not add the photo");

        }

        [HttpGet]
        [Route("GetPhoto")]

        public IActionResult GetPhoto(int id)
        {
            var photoToFromDb = _appRepository.GetPhoto(id);
            var photo = _mapper.Map<PhotoForReturnDto>(photoToFromDb);
            return Ok(photo);
        }
    }
}
