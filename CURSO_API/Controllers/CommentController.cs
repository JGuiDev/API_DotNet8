using CURSO_API.Dtos.Comment;
using CURSO_API.Interfaces;
using CURSO_API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CURSO_API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepository.GetAllAsync();

            var commentDto = comments.Select(s => s.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetID([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.GetIDAsync(id);

            if(comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        [Authorize]

        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentDto commentDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exist!");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepository.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetID), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.UpdateCommentAsync(id, updateDto.ToCommentFromUpdate());

            if(comment == null)
            {
                return NotFound("Comment not found!");
            }

            return Ok(comment.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.DeleteCommentAsync(id);

            if(comment == null)
            {
                return NotFound("Comment not exist!");
            }

            return NoContent();
        }
    }
}
