using ScamWarning.DTOs;
using ScamWarning.Interfaces;
using ScamWarning.Models;

namespace ScamWarning.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IWarningRepository _warningRepository;

        public CommentService(ICommentRepository commentRepository, IWarningRepository warningRepository)
        {
            _commentRepository = commentRepository;
            _warningRepository = warningRepository;
        }

        public async Task<IEnumerable<CommentDto>> GetByWarningIdAsync(int warningId)
        {
            var comments = await _commentRepository.GetByWarningIdAsync(warningId);
            
            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                Username = c.User.Username
            });
        }

        public async Task<CommentDto> AddAsync(CreateCommentDto dto, int userId, int warningId)
        {
            // Check if warning exists
            var warning = await _warningRepository.GetByIdWithDetailsAsync(warningId);
            if (warning == null)
            {
                throw new KeyNotFoundException($"Warning with id {warningId} not found");
            }

            var comment = new Comment
            {
                Text = dto.Text,
                WarningId = warningId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);

            // Fetch the comment with user info to return DTO
            var comments = await _commentRepository.GetByWarningIdAsync(warningId);
            var createdComment = comments.FirstOrDefault(c => c.Id == comment.Id);

            if (createdComment == null)
            {
                throw new InvalidOperationException("Failed to retrieve created comment");
            }

            return new CommentDto
            {
                Id = createdComment.Id,
                Text = createdComment.Text,
                CreatedAt = createdComment.CreatedAt,
                Username = createdComment.User.Username
            };
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with id {commentId} not found");
            }

            await _commentRepository.DeleteAsync(commentId);
        }
    }
}
