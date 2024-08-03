USE [PTS_DEV]
GO
SET IDENTITY_INSERT [dbo].[CardVGA] ON 

INSERT [dbo].[CardVGA] ([Id], [Ma], [Ten], [ThongSo], [CrUserId], [CrDateTime], [Status]) VALUES (1, N'AMD Radeon Graphics', N'AMD Radeon Graphics', N'4GB', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[CardVGA] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([Id], [Ma], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (1, N'
black', N'Màu đen', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Color] ([Id], [Ma], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (2, N'
white', N'Màu trắng', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Color] ([Id], [Ma], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (3, N'silver', N'Màu bạc', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Color] ([Id], [Ma], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (5, N'grey', N'Màu xám', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[CPU] ON 

INSERT [dbo].[CPU] ([Id], [Ma], [Ten], [CrUserId], [CrDateTime], [Status]) VALUES (1, N'Core i5-13500HX', N'Core i5-13500HX', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[CPU] ([Id], [Ma], [Ten], [CrUserId], [CrDateTime], [Status]) VALUES (2, N'Core i5-13420H', N'Core i5-13420H', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[CPU] OFF
GO
SET IDENTITY_INSERT [dbo].[HardDrive] ON 

INSERT [dbo].[HardDrive] ([Id], [Ma], [ThongSo], [CrUserId], [CrDateTime], [Status]) VALUES (1, N'SSD256-PT', N'256 GB', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[HardDrive] OFF
GO
SET IDENTITY_INSERT [dbo].[Ram] ON 

INSERT [dbo].[Ram] ([Id], [Ma], [ThongSo], [CrUserId], [CrDateTime], [Status]) VALUES (2, N'DDR4-PM', N'8GB', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Ram] ([Id], [Ma], [ThongSo], [CrUserId], [CrDateTime], [Status]) VALUES (3, N'DDR5-PM16', N'16GB DDR5', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Ram] OFF
GO
SET IDENTITY_INSERT [dbo].[Screen] ON 

INSERT [dbo].[Screen] ([Id], [Ma], [KichCo], [TanSo], [ChatLieu], [CrUserId], [CrDateTime], [Status]) VALUES (1, N'PM-16K', N'15.6"', N'144Hz', N'IPS', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Screen] OFF
GO
SET IDENTITY_INSERT [dbo].[Manufacturer] ON 

INSERT [dbo].[Manufacturer] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (1, N'Laptop Dell', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Manufacturer] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (2, N'Laptop Acer', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Manufacturer] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (3, N'Laptop HP', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Manufacturer] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (5, N'Laptop Asus', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Manufacturer] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (6, N'Laptop Gigabyte', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Manufacturer] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (7, N'Laptop Lenovo', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Manufacturer] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (8, N'Laptop MSI', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Manufacturer] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (9, N'LaptopSamsung', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Manufacturer] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductType] ON 

INSERT [dbo].[ProductType] ([Id], [Name], [CrUserId], [CrDateTime], [Status]) VALUES (1, N'Laptop Gaming', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[ProductType] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [CrUserId], [CrDateTime], [Status], [ManufacturerEntityId], [ProductTypeEntityId]) VALUES (1, N'Dell Alienware', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1, 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [CrUserId], [CrDateTime], [Status], [ManufacturerEntityId], [ProductTypeEntityId]) VALUES (2, N'Dell XPS 15', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1, 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [CrUserId], [CrDateTime], [Status], [ManufacturerEntityId], [ProductTypeEntityId]) VALUES (3, N'Dell Alain X', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1, 1, 1)
INSERT [dbo].[Product] ([Id], [Name], [CrUserId], [CrDateTime], [Status], [ManufacturerEntityId], [ProductTypeEntityId]) VALUES (4, N'Dell Lattiue', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO

SET IDENTITY_INSERT [dbo].[ProductDetail] ON 
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (1, N'15P00', CAST(15000000.00 AS Decimal(18, 2)), CAST(30000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (2, N'KMMA', CAST(20000000.00 AS Decimal(18, 2)), CAST(25000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (3, N'SKS1K', CAST(15000000.00 AS Decimal(18, 2)), CAST(30000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (4, N'KKSM', CAST(20000000.00 AS Decimal(18, 2)), CAST(25000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (5, N'15P002', CAST(15000000.00 AS Decimal(18, 2)), CAST(30000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (6, N'KMMA2', CAST(20000000.00 AS Decimal(18, 2)), CAST(25000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (7, N'SKS1K2', CAST(15000000.00 AS Decimal(18, 2)), CAST(30000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (8, N'KKSM2', CAST(20000000.00 AS Decimal(18, 2)), CAST(25000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (9, N'15P003', CAST(15000000.00 AS Decimal(18, 2)), CAST(30000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (10, N'KMMA3', CAST(20000000.00 AS Decimal(18, 2)), CAST(25000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (11, N'SKS1K3', CAST(15000000.00 AS Decimal(18, 2)), CAST(30000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (12, N'KKSM3', CAST(20000000.00 AS Decimal(18, 2)), CAST(25000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (13, N'15P003', CAST(15000000.00 AS Decimal(18, 2)), CAST(30000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (14, N'KMMA3', CAST(20000000.00 AS Decimal(18, 2)), CAST(25000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (15, N'SKS1K3', CAST(15000000.00 AS Decimal(18, 2)), CAST(30000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[ProductDetail] ([Id], [Code], [Price], [OldPrice], [Upgrade], [Description], [ProductEntityId], [ColorEntityId], [RamEntityId], [CpuEntityId], 
[HardDriveEntityId], [ScreenEntityId], [CardVGAEntityId], [CrUserId], [DiscountId], [CrDateTime], [UpdUserId], [UpdDateTime], [Status]) 
VALUES (16, N'KKSM3', CAST(20000000.00 AS Decimal(18, 2)), CAST(25000000.00 AS Decimal(18, 2)), NULL, N'f', 1, 1, 2, 1, 1, 1, 1, 1, NULL, CAST(N'2024-07-07T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[ProductDetail] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240616070106_InitDb', N'8.0.0')
GO
