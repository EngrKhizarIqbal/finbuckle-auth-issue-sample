USE [HelpDesk.MultiTenant.Db.Core]
GO

INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'482A02D1-8FDC-4423-B388-E059F4CF1C0B', N'admin@idsa.com.br', N'ADMIN@IDSA.COM.BR', N'admin@idsa.com.br', N'ADMIN@IDSA.COM.BR', 1, N'AQAAAAEAACcQAAAAEDwk3KCAXhLiyLuO06xzHOxp2Kwb0gKTOR/J/0P2RVuOGlFlbBE6OyFx69Wrk3q9+g==', N'D1E6610C-5ECD-47B0-B2D1-715E15967AE2', N'9F92B898-6109-462A-9380-811B0B425252', NULL, 0, 0, NULL, 1, 0);
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'549b8b1e-064f-4a24-9022-4c9b429ef789', N'multi@no.io', N'MULTI@NO.IO', N'multi@no.io', N'MULTI@NO.IO', 1, N'AQAAAAEAACcQAAAAEDwk3KCAXhLiyLuO06xzHOxp2Kwb0gKTOR/J/0P2RVuOGlFlbBE6OyFx69Wrk3q9+g==', N'G4CIHKTSCVCU4CX4QPHHPA5XOXTDARBN', N'80533bac-5afc-43d4-8698-1c0874419bca', NULL, 0, 0, NULL, 1, 0);


INSERT INTO [dbo].[TenantInfo] ([Id], [Identifier], [Name], [ConnectionString], [Items], [Discriminator], [UserId]) VALUES (N'418f19ee-67d5-4541-abb9-4c3a398c0d81', N'idsa', N'IDSA Group', N'Data Source=(localdb)\.\IIS_DB;Initial Catalog=HelpDesk.MultiTenant.idsa.Core;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False', NULL, N'QuodyTenantInfo', N'482A02D1-8FDC-4423-B388-E059F4CF1C0B');
INSERT INTO [dbo].[TenantInfo] ([Id], [Identifier], [Name], [ConnectionString], [Items], [Discriminator], [UserId]) VALUES (N'ce50e260-63d6-4825-9601-3c01050aa768', N'abacus', N'We Are Forge', N'Data Source=(localdb)\.\IIS_DB;Initial Catalog=HelpDesk.MultiTenant.idsa.Core;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False', NULL, N'QuodyTenantInfo', N'549b8b1e-064f-4a24-9022-4c9b429ef789');

GO

USE [HelpDesk.MultiTenant.idsa.Core]
GO

INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'549b8b1e-064f-4a24-9022-4c9b429ef789', N'multi@no.io', N'MULTI@NO.IO', N'multi@no.io', N'MULTI@NO.IO', 1, N'AQAAAAEAACcQAAAAEDwk3KCAXhLiyLuO06xzHOxp2Kwb0gKTOR/J/0P2RVuOGlFlbBE6OyFx69Wrk3q9+g==', N'G4CIHKTSCVCU4CX4QPHHPA5XOXTDARBN', N'80533bac-5afc-43d4-8698-1c0874419bca', NULL, 0, 0, NULL, 1, 0);

SET IDENTITY_INSERT [dbo].[ToDoItems] ON
INSERT INTO [dbo].[ToDoItems] ([Id], [Title], [Completed]) VALUES (1, N'Finbuckle Auth Issue', 0);
INSERT INTO [dbo].[ToDoItems] ([Id], [Title], [Completed]) VALUES (2, N'Tenant Issue', 0);
INSERT INTO [dbo].[ToDoItems] ([Id], [Title], [Completed]) VALUES (3, N'Unknown', 1);
INSERT INTO [dbo].[ToDoItems] ([Id], [Title], [Completed]) VALUES (4, N'Rock and Roll', 0);
SET IDENTITY_INSERT [dbo].[ToDoItems] OFF
