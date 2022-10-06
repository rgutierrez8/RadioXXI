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
    public class NewsController : Controller
    {
        private readonly INewsBusiness _business;
        private readonly IPhotoBusiness _photoBusiness;
        public NewsController(INewsBusiness business, IPhotoBusiness photoBusiness)
        {
            _business = business;
            _photoBusiness = photoBusiness;
        }

        #region FIND NEWS BY ID

        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            var data = _business.findById(id);

            //GETPHOTOS PRIMER PARAMETRO INDICA SI ES UNA NOTICIA = 1 O SI ES UN COMUNICADO = 2
            var dataPhoto = _photoBusiness.getPhotos(1, id);

            var news = new News()
            {
                Title = data.Title,
                Body = data.Body,
                Banner = data.Banner,
                Photos = dataPhoto.Select(photo => new Photos
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    Description = photo.Description
                }).ToList(),
            };

            return Ok(news);
        }

        #endregion

        #region FIND ALL NEWS

        [HttpGet("All")]
        public IActionResult getAll()
        {
            var dataNews = _business.findAll();

            var listNews = new List<News>();

            foreach(var news in dataNews)
            {
                var data = new News()
                {
                    Id = news.Id,
                    Title = news.Title,
                    Body = news.Body,
                    Banner = news.Banner,
                };

                listNews.Add(data);
            }

            return Ok(listNews);
        }

        #endregion

        #region CREATE NEWS

        [HttpPost("new")]
        public IActionResult insertNews([FromBody] NewsDto news)
        {
            try
            {
                _business.insertNews(news);

                var newsInserted = _business.findByTitle(news.Title);

                foreach (var photo in news.Photos)
                {
                    var phot = new PhotoDto()
                    {
                        Url = photo.Url,
                        Description = photo.Description,
                    };

                    _photoBusiness.insertPhotos(1, phot, newsInserted.Id);
                }

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region UPDATE NEWS

        [HttpPut("update/{id}")]
        public IActionResult update([FromBody] NewsDto update, int id)
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

        #region DELETE NEWS

        [HttpDelete("delete/{id}")]
        public IActionResult delete(int id)
        {
            try
            {
                var news = _business.findById(id);

                //GETPHOTOS PRIMER PARAMETRO INDICA SI ES UNA NOTICIA = 1 O SI ES UN COMUNICADO = 2
                var listPhotos = _photoBusiness.getPhotos(1, news.Id);

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
