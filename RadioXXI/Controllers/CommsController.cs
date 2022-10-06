using Microsoft.AspNetCore.Mvc;
using RadioXXI.Business.Interfaces;
using RadioXXI.Models;
using RadioXXI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadioXXI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommsController : Controller
    {
        private readonly ICommsBusiness _business;
        private readonly IPhotoBusiness _photoBusiness;

        public CommsController(ICommsBusiness business, IPhotoBusiness photoBusiness)
        {
            _business = business;
            _photoBusiness = photoBusiness;
        }

        #region FIND COMMS BY ID

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var data = _business.findById(id);

            //GETPHOTOS PRIMER PARAMETRO INDICA SI ES UNA NOTICIA = 1 O SI ES UN COMUNICADO = 2
            var photos = _photoBusiness.getPhotos(2, id);

            var comm = new Comms()
            {
                Title = data.Title,
                Body = data.Body,
                Photos = photos.Select(photo => new Photos
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    Description = photo.Description
                }).ToList(),
            };

            return Ok(comm);
        }

        #endregion

        #region LIST ALL COMMS

        [HttpGet("all")]
        public IActionResult getAll()
        {
            var comms = _business.findAll();

            var listComms = new List<Comms>();

            foreach(var comm in comms)
            {
                var newComm = new Comms()
                {
                    Id = comm.Id,
                    Title = comm.Title,
                    Body = comm.Body
                };

                listComms.Add(newComm);
            }

            return Ok(listComms);
        }

        #endregion

        #region CREATE COMMS

        [HttpPost("new")]
        public IActionResult insert([FromBody] CommsDto newComm)
        {
            try
            {
                _business.insert(newComm);
                var idComm = _business.findByTitle(newComm.Title);

                foreach(var photo in newComm.Photos)
                {
                    var ph = new PhotoDto()
                    {
                        Url = photo.Url,
                        Description = photo.Description,
                    };

                    //EL PRIMER PARAMETRO INDICA SI ES UNA NOTICIA = 1 O ES UN COMUNICADO = 2
                    _photoBusiness.insertPhotos(2, ph, idComm.Id);
                }

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        #endregion

        #region UPDATE COMMS
        
        [HttpPut("update/{id}")]
        public IActionResult update([FromBody] CommsDto update, int id)
        {
            try
            {
                _business.update(update, id);

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        #endregion

        #region DELETE COMMS

        [HttpDelete("delete/{id}")]
        public IActionResult delete(int id)
        {
            try
            {
                var comm = _business.findById(id);

                //GETPHOTOS PRIMER PARAMETRO INDICA SI ES UNA NOTICIA = 1 O SI ES UN COMUNICADO = 2
                var listPhotos = _photoBusiness.getPhotos(2, id);

                foreach(var photo in listPhotos)
                {
                    _photoBusiness.delete(photo.Id);
                }

                _business.delete(id);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        #endregion
    }
}
