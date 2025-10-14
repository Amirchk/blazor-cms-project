using CMSProject.Core.Entities;
using CMSProject.Core.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace CMSProject.Web.Services
{
    public class MediaService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public MediaService(IMediaRepository mediaRepository, IWebHostEnvironment environment)
        {
            _mediaRepository = mediaRepository;
            _environment = environment;
        }

        public async Task<(bool Success, string Message, Media? Media)> UploadFileAsync(IBrowserFile file, string uploadedBy)
        {
            // Validate file
            if (file == null || file.Size == 0)
                return (false, "No file selected", null);

            if (file.Size > MaxFileSize)
                return (false, $"File size exceeds {MaxFileSize / 1024 / 1024}MB limit", null);

            var extension = Path.GetExtension(file.Name).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                return (false, $"File type {extension} is not allowed", null);

            try
            {
                // Create uploads directory if it doesn't exist
                var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                // Generate unique filename
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.OpenReadStream(MaxFileSize).CopyToAsync(stream);
                }

                // Create media record
                var media = new Media
                {
                    FileName = file.Name,
                    FilePath = $"/uploads/{uniqueFileName}",
                    FileSize = file.Size,
                    MimeType = file.ContentType,
                    UploadedBy = uploadedBy,
                    AltText = Path.GetFileNameWithoutExtension(file.Name),
                    Description = ""
                };

                var savedMedia = await _mediaRepository.CreateAsync(media);
                return (true, "File uploaded successfully", savedMedia);
            }
            catch (Exception ex)
            {
                return (false, $"Error uploading file: {ex.Message}", null);
            }
        }

        public async Task<bool> DeleteFileAsync(int id)
        {
            var media = await _mediaRepository.GetByIdAsync(id);
            if (media == null)
                return false;

            try
            {
                // Delete physical file
                var filePath = Path.Combine(_environment.WebRootPath, media.FilePath.TrimStart('/'));
                if (File.Exists(filePath))
                    File.Delete(filePath);

                // Delete database record
                await _mediaRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Media>> GetAllMediaAsync()
        {
            return await _mediaRepository.GetAllAsync();
        }

        public string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}