USE [InfoStore]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [InfoStore]
GO
/****** Object:  Schema [Info]    Script Date: 28.12.2022 20:58:37 ******/
CREATE SCHEMA [Info]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Info].[Bookmarks]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Info].[Bookmarks](
	[Id] [uniqueidentifier] NOT NULL,
	[Url] [nvarchar](2083) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[GroupId] [uniqueidentifier] NULL,
	[Title] [nvarchar](80) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Bookmarks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Info].[BookmarkTag]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Info].[BookmarkTag](
	[BookmarksId] [uniqueidentifier] NOT NULL,
	[TagsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BookmarkTag] PRIMARY KEY CLUSTERED 
(
	[BookmarksId] ASC,
	[TagsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Info].[Groups]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Info].[Groups](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Info].[Notes]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Info].[Notes](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](80) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Info].[Tags]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Info].[Tags](
	[Id] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Info].[ToDos]    Script Date: 28.12.2022 20:58:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Info].[ToDos](
	[Id] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](200) NOT NULL,
	[DueDateTime] [datetime2](7) NOT NULL,
	[EMail] [nvarchar](200) NULL,
	[Remind] [int] NOT NULL,
	[Repeat] [int] NOT NULL,
	[TimeUnit] [int] NOT NULL,
	[Done] [bit] NOT NULL,
	[Notified] [int] NOT NULL,
	[Overdue] [bit] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ToDos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221107164918_Info', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221107170203_CommentsToDescription', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221108032227_Groups', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221108065549_LongerURL', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221114154821_BookmarkTitle', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221114155005_BookmarkTitleUnique', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221114161702_BookmarkTitleNotUnique', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221116111226_LongerBookmarkTitle', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221116112358_Notes', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221117132504_ToDo', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221122171725_Reminders', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221124062614_ToDoStatus', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221127062851_NoReminder', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221127162442_StatusToDone', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221129055928_ToDoNotified', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221129161101_Overdue', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221221090539_BokkmarksUserId', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221221140809_UserId', N'6.0.12')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221222134751_UserId2', N'6.0.12')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4006ec24-ff03-45a6-be4e-5f3019b4e26d', N'achristov@hotmail.com', N'ACHRISTOV@HOTMAIL.COM', N'achristov@hotmail.com', N'ACHRISTOV@HOTMAIL.COM', 1, N'AQAAAAEAACcQAAAAEKvmXQuBWMFZN/Zi4/2tmqKT6OEQ4kw95O3wKQKHOBlZzTlPEca3mGntOT6+2/A88A==', N'NGPYOHYKMGMFURQRHTWV5GZCWDI2ZGWY', N'a4e3e96a-6578-4723-a85f-b8905f1a086e', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6f097d8e-020d-46f4-a5b5-c67f5e22c2c7', N'demo@codesolidi.com', N'DEMO@CODESOLIDI.COM', N'demo@codesolidi.com', N'DEMO@CODESOLIDI.COM', 1, N'AQAAAAEAACcQAAAAEJjiHJV6L7oTs3UOVKfAzDZYsbEVL67+mzP97l3hDolgOXg0GxtOFStcJlKlnuXs3A==', N'2A5RS53NME5PHJHNMLOC77N63FC2GG6Y', N'03453c93-d53b-4807-9a8b-c96cda65761c', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'fae1939e-c079-4861-2d82-08dac15656f7', N'https://didourebai.medium.com/use-fluentvalidation-in-mvc-679adc6b7d33#:~:text=Use%20FluentValidation%20in%20MVC%201%20Introduction%20FluentValidation%20is,5%20Collections%20...%206%20Customize%20error%20messages%20', N'Use FluentValidation in MVC', N'94cd1750-0675-4744-8453-08dace34d735', N'Fluent Validation', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'41f9d831-1ad9-4faa-2417-08dac277e5a1', N'https://getbootstrap.com/docs/5.1/getting-started/introduction/', N'# Introduction (v5.1)
Get started with Bootstrap, the world’s most popular framework for building responsive, mobile-first sites, with jsDelivr and a template starter page.', N'e45f3bcc-f4ef-40cb-43a4-08dac2775cc0', N'Get Bootstrap (v5.1)', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'a89cf454-fb09-449a-3692-08dac280c03b', N'https://icons.getbootstrap.com/', N'# Bootstrap Icons
Free, high quality, open source icon library with over 1,600 icons. Include them anyway you like—SVGs, SVG sprite, or web fonts. Use them with or without [Bootstrap](https://getbootstrap.com/) in any project.', N'e45f3bcc-f4ef-40cb-43a4-08dac2775cc0', N'Free Bootstrap Icons', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'13537b4d-d783-48f6-e080-08dac3113b10', N'http://www.codesolidi.com', N'My company''s (Code Solidi Ltd.) **home page**. ', N'33061eaa-7d22-45f7-d21f-08dad2b2e6ef', N'Code Solidi Ltd. web site', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'cf4e7374-1543-4627-190d-08dac6520f5c', N'https://github.com/heynickc/awesome-ddd', N'A curated list of Domain-Driven Design (DDD), Command Query Responsibility Segregation (CQRS), Event Sourcing, and Event Storming resources.', N'ef19129c-6130-40aa-1135-08dac61dfb8c', N'Awesome DDD', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'70369e3f-6ebb-4ce3-b7d8-08dac7c2e3ce', N'https://learn.microsoft.com/en-us/archive/msdn-magazine/2018/april/cutting-edge-discovering-asp-net-core-signalr', N'ASP.NET SignalR was introduced a few years ago as a tool for ASP.NET developers to add real-time functionality to applications. Any scenarios in which an ASP.NET-based application had to receive frequent and asynchronous updates from the server—from monitoring systems to gaming—were good use cases for the library. Over the years, I used it also to refresh the UI in CQRS architecture scenarios and to implement a Facebook-like notification system in socialware applications. From a more technical perspective, SignalR is an abstraction layer built over some of the transport mechanisms that can establish a real-time connection between a fully compatible client and server. The client is often a Web browser and the server is often a Web server, but both are not limited to that.

**Interesting implementation:** Using SignalR to Monitor the Progress of a Remote Task	', N'173d8f35-2f1e-45bb-fc0c-08dac7c3c9ba', N'Discovering ASP.NET Core SignalR by Dino Esposito', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'5692bc62-6705-405a-0751-08dac87dda8d', N'https://www.parliament.bg/bg/rulesoftheorganisations', N'# Глави
1. ОБЩИ ПОЛОЖЕНИЯ
2. КОНСТИТУИРАНЕ НА НАРОДНОТО СЪБРАНИЕ И ПРОМЕНИ В РЪКОВОДСТВОТО
3. РЪКОВОДСТВО НА НАРОДНОТО СЪБРАНИЕ
4. ПАРЛАМЕНТАРНИ ГРУПИ
5. КОМИСИИ НА НАРОДНОТО СЪБРАНИЕ
6. ВЗАИМОДЕЙСТВИЕ С НЕПРАВИТЕЛСТВЕНИ ОРГАНИЗАЦИИ
7. СЕСИИ И ЗАСЕДАНИЯ НА НАРОДНОТО СЪБРАНИЕ
8. ВНАСЯНЕ, ОБСЪЖДАНЕ И ПРИЕМАНE НА ЗАКОНОПРОЕКТИ И ДРУГИ АКТОВЕ НА НАРОДНОТО СЪБРАНИЕ
9. ПАРЛАМЕНТАРЕН КОНТРОЛ
10. ПАРЛАМЕНТАРНИ ИЗСЛУШВАНИЯ, ПРОУЧВАНИЯ И АНКЕТИ
11. ПАРЛАМЕНТАРНО НАБЛЮДЕНИЕ И КОНТРОЛ ПО ВЪПРОСИТЕ НА ЕВРОПЕЙСКИЯ СЪЮЗ
12. НАРОДНИ ПРЕДСТАВИТЕЛИ', N'9197ac21-6db4-49d5-4090-08dac87fede7', N'Правилник за организацията и дейността на Народното събрание', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'60a287e8-0d1d-4836-0752-08dac87dda8d', N'https://www.parliament.bg/bg/const', NULL, N'9197ac21-6db4-49d5-4090-08dac87fede7', N'КОНСТИТУЦИЯ', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'0b9eec8b-fe94-431d-0753-08dac87dda8d', N'https://bg.wikipedia.org/wiki/%D0%94%D0%B5%D0%BC%D0%BE%D0%BA%D1%80%D0%B0%D1%86%D0%B8%D1%8F', NULL, N'9197ac21-6db4-49d5-4090-08dac87fede7', N'Демокрация (в Уикипедиа)', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'fa599934-35a3-421b-11a9-08dacd404b6e', N'https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design', N'# Best Practice - An Introduction To Domain-Driven Design

By [David Laribee](https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/%5Carchive%5Cmsdn-magazine%5Cauthors%5CDavid_Laribee) | February 2009

This article discusses:

* Modeling from Ubiquitous Language
* Bounded Contexts and aggregate roots
* Using the Single Responsibility Principle
* Repositories and databases', N'ef19129c-6130-40aa-1135-08dac61dfb8c', N'An Introduction To Domain-Driven Design', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'ea9db5e2-015e-4947-18a9-08dace34a2cb', N'https://docs.fluentvalidation.net/en/latest/', N'# Fluent Validator

A validation library for .NET that uses a fluent interface and lambda expressions for building strongly-typed validation rules.', N'94cd1750-0675-4744-8453-08dace34d735', N'Fluent Validator', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'b9c225b8-0dfe-44b8-42ae-08dad17b5cd8', N'https://makolyte.com/aspdotnet-how-to-use-a-backgroundservice-for-long-running-and-periodic-tasks/#:~:text=ASP.NET%20%E2%80%93%20How%20to%20use%20a%20BackgroundService%20for,and%20verify%20the%20background%20service%20is%20working%20', N'# ASP.NET – How to use a BackgroundService for long-running and periodic tasks

07/27/2022 by [Mak](https://makolyte.com/about/)
', N'6f777983-491b-4bab-7ed0-08dad17bc8f0', N'How to use a BackgroundService for long-running and periodic tasks', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'09a67d3a-89d6-4352-5700-08dad2934ee4', N'https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-7.0&tabs=visual-studio', N'# Background tasks with hosted services in ASP.NET Core
06/04/2022, by [Jeow Li Huan](https://github.com/huan086)

In ASP.NET Core, background tasks can be implemented as hosted services. A hosted service is a class with background task logic that implements the [IHostedService](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0#service-lifetimes) interface. This article provides three hosted service examples:

* Background task that runs on a timer.
* Hosted service that activates a [scoped service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0#service-lifetimes). The scoped service can use [dependency injection (DI)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0).
* Queued background tasks that run sequentially.
', N'6f777983-491b-4bab-7ed0-08dad17bc8f0', N'Background tasks with hosted services in ASP.NET Core', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'f6e60927-2346-43be-4875-08dad2b2f1ec', N'https://www.weblocalizer.eu/', N'Your ultimate ASP.NET Core and Blazor translator ;)', N'94cd1750-0675-4744-8453-08dace34d735', N'Web Localizer', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'5404ddeb-1077-4bf2-8b4c-08dae3261182', N'https://www.michael-whelan.net/replacing-appdomain-in-dotnet-core/', N'# Replacing AppDomain in .Net Core
> posted on 07 Jul 2016 | DotNet Core by Michael Whelan

In the move to .Net Core, Microsoft decided to discontinue certain technologies because they were deemed to be problematic. AppDomain was one of those that did not make the cut. While AppDomains have been discontinued, some of their functionality is still being provided. It is quite hard to find those features though, as they are spread across multiple NuGet packages and there is very little documentation at this stage. Issues on github are the best source of information, but there has been a high churn of APIs in this area and a lot of those discussions and APIs are out-of-date. If you have been hunting around for these features then I hope the code samples here will at least provide you with a good starting point and some clues as to where to find related features.', N'892b79c0-3a70-434b-93fe-08dae3265372', N'Replacing AppDomain in .Net Core', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'09b0e439-57e2-444b-7443-08dae4229cb8', N'https://github.com/Code-Solidi/CoreXF', N'CoreXF is a ASP.NET Core eXtensibility Framework.', N'cc7c1d86-ae25-418e-fcea-08dae4241b80', N'CoreXF', N'6f097d8e-020d-46f4-a5b5-c67f5e22c2c7')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'3f96f20f-8a78-4587-eb87-08dae49c8fdc', N'https://www.youtube.com/watch?v=4wHegx1Xy44', N'# Leveraging DispatchProxy to implement AOP

This capability will allow you to add layers or aspects on top of your existing implementation of the functionality of any routine without polluting your business logic code with side-routines like tracing and logging.

Here''s some useful links:
Demo Code:
https://github.com/hassanhabib/AOPDemo

DispatchProxy in .NET Core Blog:
https://devblogs.microsoft.com/dotnet...', N'892b79c0-3a70-434b-93fe-08dae3265372', N'DispatchProxy and AOP', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Bookmarks] ([Id], [Url], [Description], [GroupId], [Title], [UserId]) VALUES (N'62debb3b-b901-4bd3-ae09-08dae71e48fc', N'https://github.com/Kareadita/Kavita', N'# Kavita

Kavita is a fast, feature rich, cross platform reading server. Built with a focus for manga, and the goal of being a full solution for all your reading needs. Setup your own server and share your reading collection with your friends and family!', N'30f7ad3f-feee-4f82-6437-08dae71ed9dd', N'Kavita reading server', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'e45f3bcc-f4ef-40cb-43a4-08dac2775cc0', N'Bootstrap', N'Bookmarks about Bootstrap css framework by Twitter.', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'b1192a1f-1946-4496-2ab4-08dac3ab36a5', N'Other', N'Where others (non-classified) go.', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'ef19129c-6130-40aa-1135-08dac61dfb8c', N'Domain-driven design (DDD)', N'Domain-driven design (DDD) is a major [software design](https://en.m.wikipedia.org/wiki/Software_design) approach, focusing on modeling software to match a [domain](https://en.m.wikipedia.org/wiki/Domain_(software_engineering)) according to input from that domain''s experts.
', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'173d8f35-2f1e-45bb-fc0c-08dac7c3c9ba', N'SignalR', N'SignalR is an integrated client-and-server library that enables browser-based clients and ASP.NET-based server components to have a bidirectional and multistep conversation. In other words, the conversation isn’t limited to a single, stateless request/response data exchange; rather, it continues until explicitly closed. The conversation takes place over a persistent connection and lets the client send multiple messages to the server and the server reply—and, much more interesting—send asynchronous messages to the client.', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'9197ac21-6db4-49d5-4090-08dac87fede7', N'Обществено устройство', NULL, N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'94cd1750-0675-4744-8453-08dace34d735', N'.NET Libraries', N'.NET Libraries, NuGet packages, and .NET stuff.', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'6f777983-491b-4bab-7ed0-08dad17bc8f0', N'ASP.NET', N'All about ASP.NET (Core).', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'33061eaa-7d22-45f7-d21f-08dad2b2e6ef', N'My Sites', NULL, N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'892b79c0-3a70-434b-93fe-08dae3265372', N'.NET', NULL, N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'cc7c1d86-ae25-418e-fcea-08dae4241b80', N'Demos', N'The Demos bookmark group is a place you won''t use for anything but demo bookmarks.', N'6f097d8e-020d-46f4-a5b5-c67f5e22c2c7')
GO
INSERT [Info].[Groups] ([Id], [Name], [Description], [UserId]) VALUES (N'30f7ad3f-feee-4f82-6437-08dae71ed9dd', N'Curious', N'Interesting and curious projects, articles, and other stuff for further examination and inspection.', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Notes] ([Id], [Title], [Content], [UserId]) VALUES (N'bb66c08a-b715-465a-0661-08dac86bcb0d', N'www.narodovlastie.org', N'# Описание на функционалността

#### Изборни резултати
Народни представители и избирателни райони. *Да се пази ли историческа информация?*
Да се въвеждат 
* райони, 
* брой избиратели, 
* всички кандидати, 
* избраните народни представители (НП)
* направени промени (кой идва на мястото на НП, който вече не е такъв, евентуално защо не е).

#### Документи, и други материали свързани с народното представителство
* Конституция
* Првилник за дейността на Народното събрания
* Класическа и модерна демокрация

#### Функционалност 
##### Без регистрация

Без да е регистриран потребител на сайта може да: 
1. Дава мнение за дейността на НП (на базата на пряко посочване на представителя - проверка на геолокация, да се запазва в cookie) 
2. Участва в допитвания (анкети)
3. Да се информира за работата и оценката на НП. 
4. Да се информира за резултатите от допитвания и др.
5. Дава мнения и препоръки за сайта - функционалност, подобрения и пр.
Да се обяснят ползите при регистрация.

##### След регистрация

В допълнение на горното регистриран потребител може да:
1.  Общува с НП като поставя
* Искане - оценка на изпълнението
* Въпрос - оценка на отговора 
2. Дава оценка за работата на НП:
* Одобрение - upvote
* Неодобрение - downvote


', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Notes] ([Id], [Title], [Content], [UserId]) VALUES (N'ca572759-770a-45ac-3d30-08dad09a316e', N'Какво още...', N'## Notes
1. Да се добавят keywords към notes (sort by occurences, other?)
2. Да се добавяt stopwords

## Tasks
1. Да се добави описание (или бележки?)

## General
1. Да се направят на плъгини и да се ползва CoreXF
2. Да се направи демо акаунт (demo/demo123)
3. Да се добави email-функционалност (емейл клиент)', N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[Notes] ([Id], [Title], [Content], [UserId]) VALUES (N'18320c70-baf8-4089-6e67-08dae41c7fab', N'demo', N'This is the contents of the demo note.', N'6f097d8e-020d-46f4-a5b5-c67f5e22c2c7')
GO
INSERT [Info].[ToDos] ([Id], [Text], [DueDateTime], [EMail], [Remind], [Repeat], [TimeUnit], [Done], [Notified], [Overdue], [UserId]) VALUES (N'293f816a-833c-4623-788b-08dad040e55d', N'First', CAST(N'2022-11-30T06:50:00.0000000' AS DateTime2), N'achristov@hotmail.com', 30, 5, 1, 1, 6, 1, N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[ToDos] ([Id], [Text], [DueDateTime], [EMail], [Remind], [Repeat], [TimeUnit], [Done], [Notified], [Overdue], [UserId]) VALUES (N'6cc86d8b-4c88-4325-788c-08dad040e55d', N'Second', CAST(N'2022-11-29T18:20:00.0000000' AS DateTime2), N'achristov@hotmail.com', 5, 0, 1, 1, 0, 1, N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[ToDos] ([Id], [Text], [DueDateTime], [EMail], [Remind], [Repeat], [TimeUnit], [Done], [Notified], [Overdue], [UserId]) VALUES (N'38c087eb-3457-488b-ee20-08dad21928e5', N'Third', CAST(N'2022-12-23T13:00:00.0000000' AS DateTime2), N'achristov@hotmail.com', 15, 2, 1, 0, 8, 1, N'4006ec24-ff03-45a6-be4e-5f3019b4e26d')
GO
INSERT [Info].[ToDos] ([Id], [Text], [DueDateTime], [EMail], [Remind], [Repeat], [TimeUnit], [Done], [Notified], [Overdue], [UserId]) VALUES (N'a7fddcaa-84cf-4504-63b6-08dae4a0a5fd', N'demo todo', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'demo@codesolidi.com', 0, 0, 0, 0, 0, 0, N'6f097d8e-020d-46f4-a5b5-c67f5e22c2c7')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 28.12.2022 20:58:38 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 28.12.2022 20:58:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 28.12.2022 20:58:38 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 28.12.2022 20:58:38 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 28.12.2022 20:58:38 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 28.12.2022 20:58:38 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 28.12.2022 20:58:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bookmarks_GroupId]    Script Date: 28.12.2022 20:58:38 ******/
CREATE NONCLUSTERED INDEX [IX_Bookmarks_GroupId] ON [Info].[Bookmarks]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Bookmarks_Url]    Script Date: 28.12.2022 20:58:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Bookmarks_Url] ON [Info].[Bookmarks]
(
	[Url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookmarkTag_TagsId]    Script Date: 28.12.2022 20:58:38 ******/
CREATE NONCLUSTERED INDEX [IX_BookmarkTag_TagsId] ON [Info].[BookmarkTag]
(
	[TagsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Groups_Name]    Script Date: 28.12.2022 20:58:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Groups_Name] ON [Info].[Groups]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Notes_Title]    Script Date: 28.12.2022 20:58:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Notes_Title] ON [Info].[Notes]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tags_Text]    Script Date: 28.12.2022 20:58:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tags_Text] ON [Info].[Tags]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ToDos_Text]    Script Date: 28.12.2022 20:58:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ToDos_Text] ON [Info].[ToDos]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Info].[Bookmarks] ADD  DEFAULT (N'') FOR [UserId]
GO
ALTER TABLE [Info].[Groups] ADD  DEFAULT (N'') FOR [UserId]
GO
ALTER TABLE [Info].[Notes] ADD  DEFAULT (N'') FOR [UserId]
GO
ALTER TABLE [Info].[ToDos] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DueDateTime]
GO
ALTER TABLE [Info].[ToDos] ADD  DEFAULT ((0)) FOR [Remind]
GO
ALTER TABLE [Info].[ToDos] ADD  DEFAULT ((0)) FOR [Repeat]
GO
ALTER TABLE [Info].[ToDos] ADD  DEFAULT ((0)) FOR [TimeUnit]
GO
ALTER TABLE [Info].[ToDos] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Done]
GO
ALTER TABLE [Info].[ToDos] ADD  DEFAULT ((0)) FOR [Notified]
GO
ALTER TABLE [Info].[ToDos] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Overdue]
GO
ALTER TABLE [Info].[ToDos] ADD  DEFAULT (N'') FOR [UserId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [Info].[Bookmarks]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarks_Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [Info].[Groups] ([Id])
GO
ALTER TABLE [Info].[Bookmarks] CHECK CONSTRAINT [FK_Bookmarks_Groups_GroupId]
GO
ALTER TABLE [Info].[BookmarkTag]  WITH CHECK ADD  CONSTRAINT [FK_BookmarkTag_Bookmarks_BookmarksId] FOREIGN KEY([BookmarksId])
REFERENCES [Info].[Bookmarks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Info].[BookmarkTag] CHECK CONSTRAINT [FK_BookmarkTag_Bookmarks_BookmarksId]
GO
ALTER TABLE [Info].[BookmarkTag]  WITH CHECK ADD  CONSTRAINT [FK_BookmarkTag_Tags_TagsId] FOREIGN KEY([TagsId])
REFERENCES [Info].[Tags] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Info].[BookmarkTag] CHECK CONSTRAINT [FK_BookmarkTag_Tags_TagsId]
GO
USE [master]
GO
ALTER DATABASE [InfoStore] SET  READ_WRITE 
GO
