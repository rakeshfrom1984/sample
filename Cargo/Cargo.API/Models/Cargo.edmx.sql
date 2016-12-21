
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/27/2015 22:24:54
-- Generated from EDMX file: C:\Users\rakfali\Desktop\Cargo\Cargo.API\Models\Cargo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [cargo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[SlotBooking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SlotBooking];
GO
IF OBJECT_ID(N'[dbo].[Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Status];
GO
IF OBJECT_ID(N'[dbo].[StatusDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatusDetail];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SlotBookings'
CREATE TABLE [dbo].[SlotBookings] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [BookerName] varchar(50)  NULL,
    [ReachTime] datetime  NULL,
    [CreatedDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [dbo].[Status] (
    [StatusId] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NULL,
    [IsActive] bit  NULL,
    [CreatedDate] datetime  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [Phone] varchar(16)  NULL,
    [Password] varchar(20)  NULL,
    [Token] varchar(100)  NULL,
    [CreatedDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'StatusDetails'
CREATE TABLE [dbo].[StatusDetails] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [SlotId] uniqueidentifier  NULL,
    [StatusId] uniqueidentifier  NULL,
    [Message] nvarchar(200)  NULL,
    [DelayTimeETA] time  NULL,
    [ReasonForDelay] nvarchar(200)  NULL,
    [OnTheWay] bit  NULL,
    [CreatedDate] datetime  NULL,
    [Status] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'SlotBookings'
ALTER TABLE [dbo].[SlotBookings]
ADD CONSTRAINT [PK_SlotBookings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [StatusId] in table 'Status'
ALTER TABLE [dbo].[Status]
ADD CONSTRAINT [PK_Status]
    PRIMARY KEY CLUSTERED ([StatusId] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StatusDetails'
ALTER TABLE [dbo].[StatusDetails]
ADD CONSTRAINT [PK_StatusDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------