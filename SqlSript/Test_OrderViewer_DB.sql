USE [master]
GO
/****** Object:  Database [Test_OrderViewer_DB]    Script Date: 20.06.2023 19:15:50 ******/
CREATE DATABASE [Test_OrderViewer_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Test_OrderViewer_DB', FILENAME = N'C:\Users\Yurii_S\Test_OrderViewer_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Test_OrderViewer_DB_log', FILENAME = N'C:\Users\Yurii_S\Test_OrderViewer_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Test_OrderViewer_DB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Test_OrderViewer_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Test_OrderViewer_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Test_OrderViewer_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Test_OrderViewer_DB] SET QUERY_STORE = OFF
GO
USE [Test_OrderViewer_DB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Test_OrderViewer_DB]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 20.06.2023 19:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserDataId] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProduct]    Script Date: 20.06.2023 19:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[ProductId] [int] NULL,
 CONSTRAINT [PK_OrderProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 20.06.2023 19:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserData]    Script Date: 20.06.2023 19:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[HashPass] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UserData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([Id], [UserDataId]) VALUES (3, 1)
INSERT [dbo].[Order] ([Id], [UserDataId]) VALUES (4, 1)
INSERT [dbo].[Order] ([Id], [UserDataId]) VALUES (1002, 1)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderProduct] ON 

INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (1, 3, 1)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (2, 3, 2)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (3, 3, 1)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (4, 4, 2)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (5, 4, 4)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (6, 4, 1)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (7, 4, 2)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (1002, 1002, 7)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (1003, 1002, 4)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (1004, 1002, 6)
INSERT [dbo].[OrderProduct] ([Id], [OrderId], [ProductId]) VALUES (1005, 1002, 7)
SET IDENTITY_INSERT [dbo].[OrderProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (1, N'Milk', N'white water', CAST(60.00 AS Decimal(18, 2)))
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (2, N'Juice Tomato', N'white water', CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (4, N'Chocolate Lindt', N'Chocolate Lindt in pralene', CAST(80.00 AS Decimal(18, 2)))
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (6, N'Strowbary', N'red 1 kg', CAST(100.00 AS Decimal(18, 2)))
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price]) VALUES (7, N'Egg', N'egg 1kg', CAST(120.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[UserData] ON 

INSERT [dbo].[UserData] ([Id], [Name], [HashPass]) VALUES (1, N'admin', N'ALT2Vb8ruCdrUUz1PGuMN31b+mNN7wun8rz5EBxIY7sMIFa5Xz9ogYUn58odvEdCew==')
SET IDENTITY_INSERT [dbo].[UserData] OFF
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_UserData] FOREIGN KEY([UserDataId])
REFERENCES [dbo].[UserData] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_UserData]
GO
ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderProduct_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderProduct] CHECK CONSTRAINT [FK_OrderProduct_Order]
GO
ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderProduct_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderProduct] CHECK CONSTRAINT [FK_OrderProduct_Product]
GO
USE [master]
GO
ALTER DATABASE [Test_OrderViewer_DB] SET  READ_WRITE 
GO
