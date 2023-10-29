using Articles.Domain;
using Articles.Models;
using Articles.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Articles.Controllers
{
    [ApiController]
    [Route("articles/{articleId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleRepository _articleRepository;
        public CommentsController(ICommentRepository commentRepository, IArticleRepository articleRepository)
        {
            _commentRepository = commentRepository;
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getComments([FromRoute] int articleId)
        {

            var comments = await _commentRepository.Query().Where(d => d.ArticleId == articleId)
            .Select(s => new
            {
                Id = s.Id,
                Email = s.Email,
                Title = s.Title,
                Content = s.Content,
                Date = s.Date,
                Published = s.Published,
            }).ToListAsync();
            return Ok(comments);
        }
        [HttpGet("{commentId}")]
        public async Task<IActionResult> getCommentWithCommentId([FromRoute] int articleId, int commentId)
        {

            var comment = await _commentRepository.Query()
            .Where(d => d.ArticleId == articleId && d.Id == commentId)
            .Select(s => new
            {
                Id = s.Id,
                Email = s.Email,
                Title = s.Title,
                Content = s.Content,
                Date = s.Date,
                Published = s.Published,
            }).FirstOrDefaultAsync();
            if (comment == null)
            {
                return NotFound($"Comment with id {commentId} for article with id {articleId} not found");
            }
            return Ok(comment);
        }
        [HttpPost]
        public async Task<IActionResult> addComment([FromBody] CommentModel model, [FromRoute] int articleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var article = await _articleRepository.GetAsync(articleId);
            if (article == null)
            {
                return BadRequest("Invalid Article Id");
            }
            var comment = new Comment
            {
                ArticleId = articleId,
                Email = model.Email,
                Title = model.Title,
                Content = model.Content,
                Date = model.Date,
                Published = model.Published,
                Article = article
            };
            await _commentRepository.InsertAsync(comment);
            return Ok(comment);
        }

    }
}
