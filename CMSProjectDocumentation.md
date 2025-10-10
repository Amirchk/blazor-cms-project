# Blazor CMS - Solo Dev Documentation

## Quick Overview
A Content Management System built with ASP.NET Core 8.0 and Blazor Server. This doc keeps things simple for solo development.

## Tech Stack
- ASP.NET Core 8.0 + Blazor Server
- Entity Framework Core
- SQL Server
- MudBlazor (UI components)
- ASP.NET Core Identity (auth)

## Project Structure
```
src/
├── CMSProject.Web/              # Blazor app
├── CMSProject.Core/             # Models & interfaces
├── CMSProject.Application/      # Business logic
└── CMSProject.Infrastructure/   # Database & repositories
```

## Database Tables (Main)
- **Content**: Id, Title, Body, AuthorId, Status, CreatedDate, PublishedDate
- **Users**: Id, Username, Email, PasswordHash, Role
- **Media**: Id, FileName, FilePath, FileSize, UploadedDate
- **Categories**: Id, Name, Slug, ParentId

## Features to Build (In Order)

### Phase 1: Foundation
- [ ] Setup project structure
- [ ] Database with EF Core
- [ ] User authentication (login/register)
- [ ] Basic admin dashboard

### Phase 2: Content
- [ ] Create/edit content (simple text editor)
- [ ] List all content
- [ ] Delete content
- [ ] Content categories

### Phase 3: Media
- [ ] Upload images
- [ ] Media library view
- [ ] Delete media

### Phase 4: Polish
- [ ] Rich text editor
- [ ] Content status (draft/published)
- [ ] Search functionality
- [ ] User roles & permissions

## Key Blazor Components to Build
1. **ContentEditor.razor** - Create/edit content
2. **ContentList.razor** - Show all content
3. **MediaUpload.razor** - Upload files
4. **MediaLibrary.razor** - Browse media

## API Endpoints (REST)
```
GET    /api/content          # List all
GET    /api/content/{id}     # Get one
POST   /api/content          # Create
PUT    /api/content/{id}     # Update
DELETE /api/content/{id}     # Delete

POST   /api/media/upload     # Upload file
GET    /api/media            # List media
DELETE /api/media/{id}       # Delete
```

## Configuration (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BlazorCMS;Trusted_Connection=true"
  },
  "FileUpload": {
    "MaxFileSize": 10485760,
    "UploadPath": "wwwroot/uploads"
  }
}
```

## Development Workflow

### Starting Work
```bash
git pull origin main          # Get latest code
dotnet run                    # Start app
```

### After Coding
```bash
git add .
git commit -m "Added content editor"
git push origin main
```

### When Building a Feature
1. Ask Claude for code
2. Test locally
3. Commit and push
4. Move to next feature

## Common Commands
```bash
# Run migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

# Run app
dotnet run --project src/CMSProject.Web

# Build
dotnet build

# Test
dotnet test
```

## Authentication Setup
Using ASP.NET Core Identity:
- Register: Create account
- Login: JWT token
- Roles: Admin, Editor, Viewer

## Security Checklist
- ✅ Passwords hashed (Identity handles this)
- ✅ [Authorize] on API endpoints
- ✅ Validate file uploads (size, type)
- ✅ SQL injection protected (EF Core)
- ✅ XSS protection on content display

## Deployment (When Ready)
1. Publish: `dotnet publish -c Release`
2. Update database: `dotnet ef database update`
3. Upload to server (IIS/Azure/etc)
4. Set environment variables

## Notes for Me
- Always pull before starting work (office ↔ home sync)
- Commit small, commit often
- Use clear commit messages
- Test on both machines before big pushes

## Useful Resources
- [Blazor Docs](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [MudBlazor Components](https://mudblazor.com/)
- [EF Core Docs](https://learn.microsoft.com/en-us/ef/core/)

## Current Status
**Version**: 0.1.0 (Initial Setup)  
**Last Updated**: October 2025

---

**Next Task**: Initialize project and setup database
