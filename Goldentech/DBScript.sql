USE [master]
GO
/****** Object:  Database [Test]    Script Date: 17/08/2021 23:28:21 ******/
CREATE DATABASE [Test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Test.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Test_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Test] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Test] SET ARITHABORT OFF 
GO
ALTER DATABASE [Test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Test] SET RECOVERY FULL 
GO
ALTER DATABASE [Test] SET  MULTI_USER 
GO
ALTER DATABASE [Test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Test] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Test] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Test', N'ON'
GO
ALTER DATABASE [Test] SET QUERY_STORE = OFF
GO
USE [Test]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF;
GO
USE [Test]
GO
/****** Object:  Table [dbo].[BPType]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BPType](
	[TypeCode] [nvarchar](1) NOT NULL,
	[TypeName] [nvarchar](20) NULL,
 CONSTRAINT [PK_BPType] PRIMARY KEY CLUSTERED 
(
	[TypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessPartners]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessPartners](
	[BPCode] [nvarchar](128) NOT NULL,
	[BPName] [nvarchar](254) NULL,
	[BPType] [nvarchar](1) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_BusinessPartners] PRIMARY KEY CLUSTERED 
(
	[BPCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemCode] [nvarchar](128) NOT NULL,
	[ItemName] [nvarchar](254) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrders]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BPCode] [nvarchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrdersLines]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrdersLines](
	[LineID] [int] IDENTITY(1,1) NOT NULL,
	[DocID] [int] NULL,
	[ItemCode] [nvarchar](128) NULL,
	[Quantity] [decimal](38, 18) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_PurchaseOrdersLines] PRIMARY KEY CLUSTERED 
(
	[LineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleOrders]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BPCode] [nvarchar](128) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_SaleOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleOrdersLines]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrdersLines](
	[LineID] [int] IDENTITY(1,1) NOT NULL,
	[DocID] [int] NULL,
	[ItemCode] [nvarchar](128) NULL,
	[Quantity] [decimal](38, 18) NULL,
	[CreateDate] [datetime] NULL,
	[LastUpdateDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[LastUpdatedBy] [int] NULL,
 CONSTRAINT [PK_SaleOrdersLines] PRIMARY KEY CLUSTERED 
(
	[LineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleOrdersLinesComments]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleOrdersLinesComments](
	[CommentLineID] [int] IDENTITY(1,1) NOT NULL,
	[DocID] [int] NULL,
	[LineID] [int] NULL,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_SaleOrdersLinesComments] PRIMARY KEY CLUSTERED 
(
	[CommentLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17/08/2021 23:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](1024) NULL,
	[UserName] [nvarchar](254) NOT NULL,
	[Password] [nvarchar](max) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BusinessPartners] ADD  CONSTRAINT [DF_BusinessPartners_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[PurchaseOrders] ADD  CONSTRAINT [DF_PurchaseOrders_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SaleOrders] ADD  CONSTRAINT [DF_SaleOrders_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessPartners]  WITH CHECK ADD  CONSTRAINT [FK_BusinessPartners_BPType] FOREIGN KEY([BPType])
REFERENCES [dbo].[BPType] ([TypeCode])
GO
ALTER TABLE [dbo].[BusinessPartners] CHECK CONSTRAINT [FK_BusinessPartners_BPType]
GO
ALTER TABLE [dbo].[SaleOrders]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrders_BusinessPartners] FOREIGN KEY([BPCode])
REFERENCES [dbo].[BusinessPartners] ([BPCode])
GO
ALTER TABLE [dbo].[SaleOrders] CHECK CONSTRAINT [FK_SaleOrders_BusinessPartners]
GO
ALTER TABLE [dbo].[SaleOrders]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrders_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SaleOrders] CHECK CONSTRAINT [FK_SaleOrders_Users]
GO
ALTER TABLE [dbo].[SaleOrders]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrders_Users1] FOREIGN KEY([LastUpdatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SaleOrders] CHECK CONSTRAINT [FK_SaleOrders_Users1]
GO
ALTER TABLE [dbo].[SaleOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLines_SaleOrders] FOREIGN KEY([DocID])
REFERENCES [dbo].[SaleOrders] ([ID])
GO
ALTER TABLE [dbo].[SaleOrdersLines] CHECK CONSTRAINT [FK_SaleOrdersLines_SaleOrders]
GO
ALTER TABLE [dbo].[SaleOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLines_SaleOrdersLines] FOREIGN KEY([ItemCode])
REFERENCES [dbo].[Items] ([ItemCode])
GO
ALTER TABLE [dbo].[SaleOrdersLines] CHECK CONSTRAINT [FK_SaleOrdersLines_SaleOrdersLines]
GO
ALTER TABLE [dbo].[SaleOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLines_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SaleOrdersLines] CHECK CONSTRAINT [FK_SaleOrdersLines_Users]
GO
ALTER TABLE [dbo].[SaleOrdersLines]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLines_Users1] FOREIGN KEY([LastUpdatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SaleOrdersLines] CHECK CONSTRAINT [FK_SaleOrdersLines_Users1]
GO
ALTER TABLE [dbo].[SaleOrdersLinesComments]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLinesComments_SaleOrders] FOREIGN KEY([DocID])
REFERENCES [dbo].[SaleOrders] ([ID])
GO
ALTER TABLE [dbo].[SaleOrdersLinesComments] CHECK CONSTRAINT [FK_SaleOrdersLinesComments_SaleOrders]
GO
ALTER TABLE [dbo].[SaleOrdersLinesComments]  WITH CHECK ADD  CONSTRAINT [FK_SaleOrdersLinesComments_SaleOrdersLines] FOREIGN KEY([LineID])
REFERENCES [dbo].[SaleOrdersLines] ([LineID])
GO
ALTER TABLE [dbo].[SaleOrdersLinesComments] CHECK CONSTRAINT [FK_SaleOrdersLinesComments_SaleOrdersLines]
GO
USE [master]
GO
ALTER DATABASE [Test] SET  READ_WRITE 
GO
