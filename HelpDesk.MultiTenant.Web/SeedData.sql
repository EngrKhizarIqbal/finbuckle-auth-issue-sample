USING [HelpDesk.MultiTenant.Db.Core]
GO

INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'549b8b1e-064f-4a24-9022-4c9b429ef789', N'test@no.io', N'TEST@NO.IO', N'test@no.io', N'test@NO.IO', 1, N'AQAAAAEAACcQAAAAEDwk3KCAXhLiyLuO06xzHOxp2Kwb0gKTOR/J/0P2RVuOGlFlbBE6OyFx69Wrk3q9+g==', N'G4CIHKTSCVCU4CX4QPHHPA5XOXTDARBN', N'80533bac-5afc-43d4-8698-1c0874419bca', NULL, 0, 0, NULL, 1, 0);

INSERT INTO [dbo].[TenantInfo] ([Id], [Identifier], [Name], [ConnectionString], [Items], [Discriminator], [UserId]) VALUES (N'ce50e260-63d6-4825-9601-3c01050aa768', N'abacus', N'New Tenant', N'Data Source=(localdb)\.\IIS_DB;Initial Catalog=HelpDesk.MultiTenant.idsa.Core;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False', NULL, N'QuodyTenantInfo', N'549b8b1e-064f-4a24-9022-4c9b429ef789');
GO

USING [HelpDesk.MultiTenant.idsa.Core]
GO

INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'549b8b1e-064f-4a24-9022-4c9b429ef789', N'multi@no.io', N'MULTI@NO.IO', N'multi@no.io', N'MULTI@NO.IO', 1, N'AQAAAAEAACcQAAAAEDwk3KCAXhLiyLuO06xzHOxp2Kwb0gKTOR/J/0P2RVuOGlFlbBE6OyFx69Wrk3q9+g==', N'G4CIHKTSCVCU4CX4QPHHPA5XOXTDARBN', N'80533bac-5afc-43d4-8698-1c0874419bca', NULL, 0, 0, NULL, 1, 0);

SET IDENTITY_INSERT [dbo].[ToDoItems] ON
INSERT INTO [dbo].[ToDoItems] ([Id], [Title], [Completed]) VALUES (1, N'Finbuckle Auth Issue', 0)
INSERT INTO [dbo].[ToDoItems] ([Id], [Title], [Completed]) VALUES (2, N'Tenant Issue', 0)
INSERT INTO [dbo].[ToDoItems] ([Id], [Title], [Completed]) VALUES (3, N'Unknown', 1)
INSERT INTO [dbo].[ToDoItems] ([Id], [Title], [Completed]) VALUES (4, N'Rock and Roll', 0)
SET IDENTITY_INSERT [dbo].[ToDoItems] OFF
