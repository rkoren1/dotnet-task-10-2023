using Articles.Domain;
using Articles.Models;
using Articles.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Articles.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        // GET articles/search
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string title)
        {
            var result = await _articleRepository
            .Query()
            .Where(a => a.Title.Contains(title) || a.Content.Contains(title))
            .Take(20)
            .Select(a => new ArticleModel
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Date = a.Date,
                Published = a.Published
            })
            .ToListAsync();

            return Ok(new ArticleListModel { Articles = result });
        }

        // GET articles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _articleRepository.GetAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            var result = new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Date = article.Date,
                Published = article.Published
            };

            return Ok(result);
        }

        // POST articles
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArticleInsertModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                Date = DateTime.UtcNow,
                Published = model.Published
            };

            await _articleRepository.InsertAsync(article);

            return Created($"articles/{article.Id}", article);
        }

        // PUT articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ArticleModel model)
        {
            var existingEntity = await _articleRepository.GetAsync(id);
            if (existingEntity == null)
                return BadRequest();
            existingEntity.Title = model.Title;
            existingEntity.Published = model.Published;
            existingEntity.Content = model.Content;
            if (model.Date == DateTime.MinValue)
                existingEntity.Date = DateTime.UtcNow;
            else
                existingEntity.Date = model.Date;
            await _articleRepository.UpdateAsync(existingEntity);
            return Ok(existingEntity);
        }

        // DELETE articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _articleRepository.GetAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            await _articleRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
