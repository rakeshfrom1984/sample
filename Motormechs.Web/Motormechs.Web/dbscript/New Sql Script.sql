USE [MotorMechs]
GO

CREATE TABLE [role](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Name] [varchar](100) NULL,
	[IsActive] [bit] NULL
)


GO

CREATE TABLE [Services](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Name] [varchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NULL
	)

	GO

CREATE TABLE [User](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Name] [varchar](100) NULL,
	[Address] [varchar](200) NULL,
	[Email] [varchar](100) NULL,
	[Phone] [varchar](16) NULL,
	[Password] [varchar](30) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[RoleId] [int] NULL,
	[IsActive] [bit] NULL,
	[Token] [nvarchar](50) NULL,
	[IsAdminCreated] [bit] NULL
	)

GO

CREATE TABLE [UserServices](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[UserId] [int] NULL,
	[VehicleId] [int] NULL,
	[CreatedDate] [datetime] NULL default (getdate()),
	[CreatedBy] [int] NULL,
	[IsActive] [bit] NULL default(1),
	[CompleteDate] [datetime] NULL
	)

	
GO

CREATE TABLE [UserServicesDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[UserId] [int] NULL,
	[ServiceId] [int] NULL,
	[CreatedDate] [datetime] NULL default(getdate()),
	[CreatedBy] [int] NULL,
	[IsActive] [bit] NULL default(1),
	[CompleteDate] [datetime] NULL)

	
GO

CREATE TABLE [Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Name] [varchar](100) NULL,
	[Model] [varchar](15) NULL,
	[ManufacturedBy] [varchar](100) NULL,
	[Number] [varchar](100) NULL,
	[Owner] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL  DEFAULT (getdate()),
	[IsActive] [bit] NULL  DEFAULT ((1))



