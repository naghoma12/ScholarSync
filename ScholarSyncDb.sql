USE [ScholarSync]
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([Id], [Name], [PhotoURL], [FilePath], [CreatedDate], [IsDeleted]) VALUES (1, N'Cs', N's1.jfif', N'E:\desktop\depi\ScholarSync\ScholarSync\ScholarSyncMVC\wwwroot\Uploads\department\s1.jfif', CAST(N'2024-10-02T16:08:31.4108379' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241002102133_init', N'8.0.8')
GO
