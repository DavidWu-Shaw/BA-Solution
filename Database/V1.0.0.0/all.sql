USE [restorder]
GO
/****** Object:  Table [dbo].[tblSetupMenu]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSetupMenu](
	[SetupMenuId] [int] IDENTITY(1,1) NOT NULL,
	[ParentMenuId] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[MenuText] [nvarchar](100) NULL,
	[Tooltip] [nvarchar](200) NULL,
	[NavigateUrl] [varchar](100) NULL,
	[MenuTypeId] [int] NULL,
	[Sort] [int] NULL,
 CONSTRAINT [PK_tblSetupMenu] PRIMARY KEY CLUSTERED 
(
	[SetupMenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSellingPeriod]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSellingPeriod](
	[SellingPeriodId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_tblSellingPeriod] PRIMARY KEY CLUSTERED 
(
	[SellingPeriodId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblScheduleEvent]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblScheduleEvent](
	[ScheduleEventId] [int] IDENTITY(1,1) NOT NULL,
	[ObjectId] [int] NULL,
	[ObjectTypeId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ScheduledTime] [datetime] NOT NULL,
	[ReccuringTypeId] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_tblScheduleEvent] PRIMARY KEY CLUSTERED 
(
	[ScheduleEventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSupplierComplain]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupplierComplain](
	[SupplierComplainId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[CustomerPhone] [nvarchar](50) NULL,
	[Content] [nvarchar](500) NULL,
	[RelatedOrderId] [int] NOT NULL,
 CONSTRAINT [PK_tblSupplierComplain] PRIMARY KEY CLUSTERED 
(
	[SupplierComplainId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSupplier]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupplier](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AddressLine1] [nvarchar](100) NULL,
	[AddressLine2] [nvarchar](100) NULL,
	[Country] [nvarchar](50) NULL,
	[CountryState] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[ContactPhone] [nvarchar](50) NULL,
	[ContactEmail] [nvarchar](50) NULL,
	[ContactFax] [nvarchar](50) NULL,
	[CategoryId] [int] NULL,
	[NationId] [int] NULL,
	[AllowTakeOut] [bit] NULL,
	[AllowReserve] [bit] NULL,
	[AllowBringWine] [bit] NULL,
	[DayStartTime] [datetime] NULL,
	[DayEndTime] [datetime] NULL,
	[Rating] [decimal](8, 2) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tblSupplier] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[UserId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[DomainId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[LanguageId] [int] NULL,
	[MatchId] [int] NULL,
	[FullName] [nvarchar](100) NULL,
	[LastConnectDate] [datetime] NULL,
	[IsBuiltIn] [bit] NULL,
 CONSTRAINT [PK_tblWebUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTransactionItem]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTransactionItem](
	[TransactionItemId] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [int] NOT NULL,
	[ItemDescription] [nvarchar](50) NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[Amount] [decimal](10, 2) NULL,
	[UnitPrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK_tblTransactionItem] PRIMARY KEY CLUSTERED 
(
	[TransactionItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTransaction]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTransaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[TransactionNumber] [nvarchar](50) NULL,
	[CustomerId] [int] NULL,
	[ContactId] [int] NULL,
	[EffectiveDate] [datetime] NULL,
	[DateSigned] [datetime] NULL,
	[Amount] [decimal](10, 2) NULL,
	[CurrencyId] [int] NULL,
	[Notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_tblTransaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSubject]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSubject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[MasterSubjectId] [int] NULL,
	[SubjectType] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[ManageUrl] [varchar](100) NULL,
	[EditUrl] [varchar](100) NULL,
	[ListUrl] [varchar](100) NULL,
	[SubjectLabel] [nvarchar](50) NULL,
	[AllowListFiltering] [bit] NULL,
	[IsAddInGrid] [bit] NULL,
	[IsGridInFormEdit] [bit] NULL,
	[AllowListAdd] [bit] NULL,
	[AllowListEdit] [bit] NULL,
	[AllowListDelete] [bit] NULL,
	[SubjectIdField] [varchar](50) NULL,
	[MasterSubjectIdField] [varchar](50) NULL,
 CONSTRAINT [PK_tblSubject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMainMenu]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMainMenu](
	[MainMenuId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[MenuText] [nvarchar](100) NULL,
	[Tooltip] [nvarchar](200) NULL,
	[NavigateUrl] [varchar](100) NULL,
	[MenuTypeId] [int] NULL,
	[Sort] [int] NULL,
	[ImageUrl] [nvarchar](100) NULL,
 CONSTRAINT [PK_tblMainMenu] PRIMARY KEY CLUSTERED 
(
	[MainMenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblLead]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLead](
	[LeadId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Gender] [char](1) NULL,
	[AddressLine1] [nvarchar](100) NULL,
	[AddressLine2] [nvarchar](100) NULL,
	[Country] [nvarchar](50) NULL,
	[CountryState] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[LeadStageId] [int] NULL,
 CONSTRAINT [PK_tblLead] PRIMARY KEY CLUSTERED 
(
	[LeadId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEntity]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEntity](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[EntityTypeId] [int] NULL,
	[IsBuiltIn] [bit] NULL,
	[AllowAddItem] [bit] NULL,
	[AllowEditItem] [bit] NULL,
	[AllowDeleteItem] [bit] NULL,
	[EntityKey] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblEntity] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLanguage]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLanguage](
	[LanguageId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[IsSystemLanguage] [bit] NOT NULL,
	[DateFormat] [nvarchar](50) NULL,
	[DateTimeFormat] [nvarchar](50) NULL,
	[Culture] [nvarchar](5) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[IsDefaultLanguage] [bit] NOT NULL,
 CONSTRAINT [PK_tblLanguage] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDomain]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDomain](
	[DomainId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LoginEvent] [nvarchar](50) NULL,
	[RelatedSubjectIdField] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblDomain] PRIMARY KEY CLUSTERED 
(
	[DomainId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDocument]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDocument](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NULL,
	[Content] [varbinary](max) NULL,
	[Thumbnail] [varbinary](max) NULL,
	[Notes] [nvarchar](200) NULL,
	[DocTypeId] [int] NULL,
	[IssuedById] [int] NULL,
	[IssuedDate] [datetime] NULL,
	[FileName] [nvarchar](100) NULL,
	[Extension] [nvarchar](10) NULL,
	[ContentType] [nvarchar](100) NULL,
	[ContentLength] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tblDocument] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDataType]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDataType](
	[DataTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[DucType] [nvarchar](50) NULL,
	[Validator] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblDataType] PRIMARY KEY CLUSTERED 
(
	[DataTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCustomer]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCustomer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CustomerTypeId] [int] NULL,
	[AddressLine1] [nvarchar](100) NULL,
	[AddressLine2] [nvarchar](100) NULL,
	[Country] [nvarchar](50) NULL,
	[CountryState] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[CustomerStatusId] [int] NULL,
	[ParentCustomerId] [int] NULL,
	[Phone] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblApplicationSetting]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblApplicationSetting](
	[ApplicationSettingId] [int] IDENTITY(1,1) NOT NULL,
	[SettingKey] [nvarchar](50) NOT NULL,
	[SettingValue] [nvarchar](200) NULL,
	[ValueType] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblApplicationSetting] PRIMARY KEY CLUSTERED 
(
	[ApplicationSettingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAdministrator]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdministrator](
	[AdministratorId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[LastConnectDate] [datetime] NULL,
 CONSTRAINT [PK_tblAdministrator] PRIMARY KEY CLUSTERED 
(
	[AdministratorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEmployee]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[JobTitle] [nvarchar](100) NULL,
	[AddressLine1] [nvarchar](100) NULL,
	[AddressLine2] [nvarchar](100) NULL,
	[Country] [nvarchar](50) NULL,
	[CountryState] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tblEmployee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDomainSetupMenu]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDomainSetupMenu](
	[DomainSetupMenuId] [int] IDENTITY(1,1) NOT NULL,
	[DomainId] [int] NOT NULL,
	[SetupMenuId] [int] NOT NULL,
	[Sort] [int] NULL,
 CONSTRAINT [PK_tblDomainSetupMenu] PRIMARY KEY CLUSTERED 
(
	[DomainSetupMenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDomainMainMenu]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDomainMainMenu](
	[DomainMainMenuId] [int] IDENTITY(1,1) NOT NULL,
	[DomainId] [int] NOT NULL,
	[MainMenuId] [int] NOT NULL,
	[Sort] [int] NULL,
 CONSTRAINT [PK_tblDomainMainMenu] PRIMARY KEY CLUSTERED 
(
	[DomainMainMenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEntityItem]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEntityItem](
	[EntityItemId] [int] IDENTITY(1,1) NOT NULL,
	[EntityId] [int] NOT NULL,
	[Text] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
 CONSTRAINT [PK_tblEntityItem] PRIMARY KEY CLUSTERED 
(
	[EntityItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblContact]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblContact](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[FullName] [nvarchar](100) NULL,
	[FamilyName] [nvarchar](100) NULL,
	[Gender] [char](1) NULL,
	[NickName] [nvarchar](50) NULL,
	[Salutation] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](100) NULL,
	[AddressLine2] [nvarchar](100) NULL,
	[Country] [nvarchar](50) NULL,
	[CountryState] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[CategoryId] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tblContact] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEmployeeCustomer]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeCustomer](
	[EmployeeCustomerId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_tblEmployeeCustomer] PRIMARY KEY CLUSTERED 
(
	[EmployeeCustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLanguageKey]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLanguageKey](
	[LanguageKeyId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[LocalizationKey] [nvarchar](300) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[ApplicationModule] [int] NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsTranslationRequired] [bit] NOT NULL,
 CONSTRAINT [PK_tblLanguageKey] PRIMARY KEY CLUSTERED 
(
	[LanguageKeyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_tblLanguageKey] UNIQUE NONCLUSTERED 
(
	[LocalizationKey] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProduct]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblProduct](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](400) NULL,
	[CategoryId] [int] NULL,
	[SupplierId] [int] NULL,
	[Price] [decimal](8, 2) NULL,
	[Sketch] [varbinary](max) NULL,
	[SellingPeriodId] [int] NULL,
	[Disabled] [bit] NULL,
	[DiscountAmount] [decimal](10, 2) NULL,
 CONSTRAINT [PK_tblProduct] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSubjectInstance]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSubjectInstance](
	[SubjectInstanceId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[InstanceId] [int] NOT NULL,
	[InstanceCreatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblSubjectInstance] PRIMARY KEY CLUSTERED 
(
	[SubjectInstanceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSubjectField]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSubjectField](
	[SubjectFieldId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[FieldKey] [nvarchar](50) NOT NULL,
	[FieldLabel] [nvarchar](100) NULL,
	[HelpText] [nvarchar](200) NULL,
	[DataTypeId] [int] NULL,
	[PickupEntityId] [int] NULL,
	[LookupSubjectId] [int] NULL,
	[IsRequired] [bit] NULL,
	[IsBuiltIn] [bit] NULL,
	[IsReadonly] [bit] NULL,
	[IsHidden] [bit] NULL,
	[IsShowInGrid] [bit] NULL,
	[IsLinkInGrid] [bit] NULL,
	[RowIndex] [int] NULL,
	[ColIndex] [int] NULL,
	[Sort] [smallint] NULL,
	[MaxLength] [int] NULL,
	[MinValue] [decimal](18, 2) NULL,
	[MaxValue] [decimal](18, 2) NULL,
	[NavigateUrlFormatString] [nvarchar](100) NULL,
 CONSTRAINT [PK_tblSubjectField] PRIMARY KEY CLUSTERED 
(
	[SubjectFieldId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSubjectChildList]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSubjectChildList](
	[SubjectChildListId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[ChildListSubjectId] [int] NOT NULL,
	[AllowAdd] [bit] NULL,
	[AllowEdit] [bit] NULL,
	[AllowDelete] [bit] NULL,
	[Sort] [int] NULL,
	[Title] [nvarchar](100) NULL,
	[AllowFiltering] [bit] NULL,
 CONSTRAINT [PK_tblSubjectChildList] PRIMARY KEY CLUSTERED 
(
	[SubjectChildListId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSubjectAction]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSubjectAction](
	[SubjectActionId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[ActionName] [nvarchar](50) NOT NULL,
	[ActionLabel] [nvarchar](100) NULL,
	[HelpText] [nvarchar](200) NULL,
 CONSTRAINT [PK_tblSubjectAction] PRIMARY KEY CLUSTERED 
(
	[SubjectActionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblShipTo]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShipTo](
	[ShipToId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[ContactPhone] [nvarchar](20) NULL,
	[AddressLine1] [nvarchar](100) NULL,
	[AddressLine2] [nvarchar](100) NULL,
	[Country] [nvarchar](50) NULL,
	[CountryState] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](20) NULL,
 CONSTRAINT [PK_tblShipTo] PRIMARY KEY CLUSTERED 
(
	[ShipToId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrder]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrder](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](50) NULL,
	[CustomerId] [int] NULL,
	[ContactId] [int] NULL,
	[TimeOrdered] [datetime] NULL,
	[TimeShipped] [datetime] NULL,
	[TimeShipBy] [datetime] NULL,
	[TimeCancelBy] [datetime] NULL,
	[Amount] [decimal](10, 2) NULL,
	[CurrencyId] [int] NULL,
	[StatusId] [int] NULL,
	[Notes] [nvarchar](500) NULL,
	[SupplierId] [int] NULL,
	[ShipToId] [int] NULL,
	[BillToId] [int] NULL,
 CONSTRAINT [PK_tblOrder] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOpportunity]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOpportunity](
	[OpportunityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[ContactId] [int] NULL,
	[CustomerId] [int] NULL,
	[ProductId] [int] NULL,
	[EstimateAmount] [decimal](18, 2) NULL,
	[SalesStage] [int] NULL,
 CONSTRAINT [PK_tblOpportunity] PRIMARY KEY CLUSTERED 
(
	[OpportunityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSupplierProperty]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupplierProperty](
	[SupplierPropertyId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[PropertyTypeId] [int] NOT NULL,
	[PropertyValue] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tblSupplierProperty] PRIMARY KEY CLUSTERED 
(
	[SupplierPropertyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSubjectLayout]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSubjectLayout](
	[SubjectLayoutId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[ItemTypeId] [int] NOT NULL,
	[SubjectFieldId] [int] NULL,
	[SubjectActionId] [int] NULL,
	[SectionLabel] [nvarchar](100) NULL,
	[RowIndex] [int] NULL,
	[CellIndex] [int] NULL,
 CONSTRAINT [PK_tblSubjectLayout] PRIMARY KEY CLUSTERED 
(
	[SubjectLayoutId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSubjectInstanceFieldValue]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSubjectInstanceFieldValue](
	[SubjectInstanceFieldValueId] [int] NOT NULL,
	[SubjectInstanceId] [int] NOT NULL,
	[SubjectFieldId] [int] NOT NULL,
	[ValueInt] [int] NULL,
	[ValueText] [nvarchar](100) NULL,
	[ValueDate] [datetime] NULL,
	[ValueBool] [bit] NULL,
	[ValueNumeric] [numeric](18, 2) NULL,
 CONSTRAINT [PK_tblSubjectInstanceFieldValue] PRIMARY KEY CLUSTERED 
(
	[SubjectInstanceFieldValueId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProductSketch]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProductSketch](
	[ProductSketchId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Sketch] [image] NULL,
 CONSTRAINT [PK_tblProductSketch] PRIMARY KEY CLUSTERED 
(
	[ProductSketchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProductProperty]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProductProperty](
	[ProductPropertyId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[PropertyTypeId] [int] NOT NULL,
	[PropertyValue] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tblProductProperty] PRIMARY KEY CLUSTERED 
(
	[ProductPropertyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderItem]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderItem](
	[OrderItemId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ItemDescription] [nvarchar](50) NULL,
	[ProductId] [int] NULL,
	[QtyOrdered] [int] NULL,
	[Amount] [decimal](10, 2) NULL,
	[UnitPrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK_tblOrderItem] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblActivity]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblActivity](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[ActivityName] [nvarchar](100) NOT NULL,
	[ActivityTypeId] [int] NULL,
	[Notes] [nvarchar](500) NULL,
	[StartTime] [datetime] NULL,
	[TimeSpent] [decimal](8, 2) NULL,
	[EndTime] [datetime] NULL,
	[ContactId] [int] NULL,
	[CustomerId] [int] NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_tblActivity] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetSubjectFieldInfoByTable]    Script Date: 01/11/2012 15:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:			David Wu
-- Create date:		Mar.03, 2011
-- Description:		
-- =============================================
CREATE PROCEDURE [dbo].[GetSubjectFieldInfoByTable]
	@SubjectId int
AS
BEGIN

declare @tableName varchar(50)
set @tableName = (
select TableName  from dbo.tblSubject where SubjectId = @SubjectId)

select * from 
(SELECT COLUMN_NAME, ORDINAL_POSITION, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = @tableName
) col
left outer join dbo.tblSubjectField on tblSubjectField.FieldKey = col.COLUMN_NAME and tblSubjectField.SubjectId = @SubjectId

order by Sort

END
GO
/****** Object:  Table [dbo].[tblContactContactMethod]    Script Date: 01/11/2012 15:30:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblContactContactMethod](
	[ContactContactMethodId] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[ContactMethodTypeId] [int] NOT NULL,
	[ContactMethodValue] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tblContactContactMethod] PRIMARY KEY CLUSTERED 
(
	[ContactContactMethodId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_tblActivity_tblContact]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblActivity_tblContact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[tblContact] ([ContactId])
GO
ALTER TABLE [dbo].[tblActivity] CHECK CONSTRAINT [FK_tblActivity_tblContact]
GO
/****** Object:  ForeignKey [FK_tblActivity_tblCustomer]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblActivity_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([CustomerId])
GO
ALTER TABLE [dbo].[tblActivity] CHECK CONSTRAINT [FK_tblActivity_tblCustomer]
GO
/****** Object:  ForeignKey [FK_tblActivity_tblEmployee]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblActivity_tblEmployee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tblEmployee] ([EmployeeId])
GO
ALTER TABLE [dbo].[tblActivity] CHECK CONSTRAINT [FK_tblActivity_tblEmployee]
GO
/****** Object:  ForeignKey [FK_tblContact_tblCustomer]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblContact]  WITH CHECK ADD  CONSTRAINT [FK_tblContact_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([CustomerId])
GO
ALTER TABLE [dbo].[tblContact] CHECK CONSTRAINT [FK_tblContact_tblCustomer]
GO
/****** Object:  ForeignKey [FK_tblContactContactMethod_tblContact]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblContactContactMethod]  WITH CHECK ADD  CONSTRAINT [FK_tblContactContactMethod_tblContact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[tblContact] ([ContactId])
GO
ALTER TABLE [dbo].[tblContactContactMethod] CHECK CONSTRAINT [FK_tblContactContactMethod_tblContact]
GO
/****** Object:  ForeignKey [FK_tblDomainMainMenu_tblMainMenu]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblDomainMainMenu]  WITH CHECK ADD  CONSTRAINT [FK_tblDomainMainMenu_tblMainMenu] FOREIGN KEY([MainMenuId])
REFERENCES [dbo].[tblMainMenu] ([MainMenuId])
GO
ALTER TABLE [dbo].[tblDomainMainMenu] CHECK CONSTRAINT [FK_tblDomainMainMenu_tblMainMenu]
GO
/****** Object:  ForeignKey [FK_tblDomainSetupMenu_tblSetupMenu]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblDomainSetupMenu]  WITH CHECK ADD  CONSTRAINT [FK_tblDomainSetupMenu_tblSetupMenu] FOREIGN KEY([SetupMenuId])
REFERENCES [dbo].[tblSetupMenu] ([SetupMenuId])
GO
ALTER TABLE [dbo].[tblDomainSetupMenu] CHECK CONSTRAINT [FK_tblDomainSetupMenu_tblSetupMenu]
GO
/****** Object:  ForeignKey [FK_tblEmployeeCustomer_tblCustomer]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblEmployeeCustomer]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployeeCustomer_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([CustomerId])
GO
ALTER TABLE [dbo].[tblEmployeeCustomer] CHECK CONSTRAINT [FK_tblEmployeeCustomer_tblCustomer]
GO
/****** Object:  ForeignKey [FK_tblEmployeeCustomer_tblEmployee]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblEmployeeCustomer]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployeeCustomer_tblEmployee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tblEmployee] ([EmployeeId])
GO
ALTER TABLE [dbo].[tblEmployeeCustomer] CHECK CONSTRAINT [FK_tblEmployeeCustomer_tblEmployee]
GO
/****** Object:  ForeignKey [FK_tblEntityItem_tblEntity]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblEntityItem]  WITH CHECK ADD  CONSTRAINT [FK_tblEntityItem_tblEntity] FOREIGN KEY([EntityId])
REFERENCES [dbo].[tblEntity] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblEntityItem] CHECK CONSTRAINT [FK_tblEntityItem_tblEntity]
GO
/****** Object:  ForeignKey [FK_tblLanguageKey_tblLanguage]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblLanguageKey]  WITH CHECK ADD  CONSTRAINT [FK_tblLanguageKey_tblLanguage] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[tblLanguage] ([LanguageId])
GO
ALTER TABLE [dbo].[tblLanguageKey] CHECK CONSTRAINT [FK_tblLanguageKey_tblLanguage]
GO
/****** Object:  ForeignKey [FK_tblOpportunity_tblCustomer]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblOpportunity]  WITH CHECK ADD  CONSTRAINT [FK_tblOpportunity_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([CustomerId])
GO
ALTER TABLE [dbo].[tblOpportunity] CHECK CONSTRAINT [FK_tblOpportunity_tblCustomer]
GO
/****** Object:  ForeignKey [FK_tblOrder_tblCustomer]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblOrder]  WITH CHECK ADD  CONSTRAINT [FK_tblOrder_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([CustomerId])
GO
ALTER TABLE [dbo].[tblOrder] CHECK CONSTRAINT [FK_tblOrder_tblCustomer]
GO
/****** Object:  ForeignKey [FK_tblOrderItem_tblOrder]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderItem_tblOrder] FOREIGN KEY([OrderId])
REFERENCES [dbo].[tblOrder] ([OrderId])
GO
ALTER TABLE [dbo].[tblOrderItem] CHECK CONSTRAINT [FK_tblOrderItem_tblOrder]
GO
/****** Object:  ForeignKey [FK_tblOrderItem_tblProduct]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderItem_tblProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tblProduct] ([ProductId])
GO
ALTER TABLE [dbo].[tblOrderItem] CHECK CONSTRAINT [FK_tblOrderItem_tblProduct]
GO
/****** Object:  ForeignKey [FK_tblProduct_tblSellingPeriod]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblProduct]  WITH CHECK ADD  CONSTRAINT [FK_tblProduct_tblSellingPeriod] FOREIGN KEY([SellingPeriodId])
REFERENCES [dbo].[tblSellingPeriod] ([SellingPeriodId])
GO
ALTER TABLE [dbo].[tblProduct] CHECK CONSTRAINT [FK_tblProduct_tblSellingPeriod]
GO
/****** Object:  ForeignKey [FK_tblProduct_tblSupplier]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblProduct]  WITH CHECK ADD  CONSTRAINT [FK_tblProduct_tblSupplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[tblSupplier] ([SupplierId])
GO
ALTER TABLE [dbo].[tblProduct] CHECK CONSTRAINT [FK_tblProduct_tblSupplier]
GO
/****** Object:  ForeignKey [FK_tblProductProperty_tblProduct]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblProductProperty]  WITH CHECK ADD  CONSTRAINT [FK_tblProductProperty_tblProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tblProduct] ([ProductId])
GO
ALTER TABLE [dbo].[tblProductProperty] CHECK CONSTRAINT [FK_tblProductProperty_tblProduct]
GO
/****** Object:  ForeignKey [FK_tblProductSketch_tblProduct]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblProductSketch]  WITH CHECK ADD  CONSTRAINT [FK_tblProductSketch_tblProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tblProduct] ([ProductId])
GO
ALTER TABLE [dbo].[tblProductSketch] CHECK CONSTRAINT [FK_tblProductSketch_tblProduct]
GO
/****** Object:  ForeignKey [FK_tblShipTo_tblCustomer]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblShipTo]  WITH CHECK ADD  CONSTRAINT [FK_tblShipTo_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([CustomerId])
GO
ALTER TABLE [dbo].[tblShipTo] CHECK CONSTRAINT [FK_tblShipTo_tblCustomer]
GO
/****** Object:  ForeignKey [FK_tblSubject_tblSubject]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblSubject_tblSubject] FOREIGN KEY([MasterSubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblSubject] CHECK CONSTRAINT [FK_tblSubject_tblSubject]
GO
/****** Object:  ForeignKey [FK_tblSubjectAction_tblSubject]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectAction]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectAction_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblSubjectAction] CHECK CONSTRAINT [FK_tblSubjectAction_tblSubject]
GO
/****** Object:  ForeignKey [FK_tblSubjectChildList_tblSubject]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectChildList]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectChildList_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblSubjectChildList] CHECK CONSTRAINT [FK_tblSubjectChildList_tblSubject]
GO
/****** Object:  ForeignKey [FK_tblSubjectChildList_tblSubject1]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectChildList]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectChildList_tblSubject1] FOREIGN KEY([ChildListSubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblSubjectChildList] CHECK CONSTRAINT [FK_tblSubjectChildList_tblSubject1]
GO
/****** Object:  ForeignKey [FK_tblSubjectField_tblEntity]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectField]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectField_tblEntity] FOREIGN KEY([PickupEntityId])
REFERENCES [dbo].[tblEntity] ([EntityId])
GO
ALTER TABLE [dbo].[tblSubjectField] CHECK CONSTRAINT [FK_tblSubjectField_tblEntity]
GO
/****** Object:  ForeignKey [FK_tblSubjectField_tblSubject]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectField]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectField_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblSubjectField] CHECK CONSTRAINT [FK_tblSubjectField_tblSubject]
GO
/****** Object:  ForeignKey [FK_tblSubjectField_tblSubject2]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectField]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectField_tblSubject2] FOREIGN KEY([LookupSubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblSubjectField] CHECK CONSTRAINT [FK_tblSubjectField_tblSubject2]
GO
/****** Object:  ForeignKey [FK_tblSubjectInstance_tblSubject]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectInstance]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectInstance_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblSubjectInstance] CHECK CONSTRAINT [FK_tblSubjectInstance_tblSubject]
GO
/****** Object:  ForeignKey [FK_tblSubjectInstanceFieldValue_tblSubjectField]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectInstanceFieldValue]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectInstanceFieldValue_tblSubjectField] FOREIGN KEY([SubjectFieldId])
REFERENCES [dbo].[tblSubjectField] ([SubjectFieldId])
GO
ALTER TABLE [dbo].[tblSubjectInstanceFieldValue] CHECK CONSTRAINT [FK_tblSubjectInstanceFieldValue_tblSubjectField]
GO
/****** Object:  ForeignKey [FK_tblSubjectInstanceFieldValue_tblSubjectInstance]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectInstanceFieldValue]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectInstanceFieldValue_tblSubjectInstance] FOREIGN KEY([SubjectInstanceId])
REFERENCES [dbo].[tblSubjectInstance] ([SubjectInstanceId])
GO
ALTER TABLE [dbo].[tblSubjectInstanceFieldValue] CHECK CONSTRAINT [FK_tblSubjectInstanceFieldValue_tblSubjectInstance]
GO
/****** Object:  ForeignKey [FK_tblSubjectLayout_tblSubject]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectLayout]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectLayout_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblSubjectLayout] CHECK CONSTRAINT [FK_tblSubjectLayout_tblSubject]
GO
/****** Object:  ForeignKey [FK_tblSubjectLayout_tblSubjectAction]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectLayout]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectLayout_tblSubjectAction] FOREIGN KEY([SubjectActionId])
REFERENCES [dbo].[tblSubjectAction] ([SubjectActionId])
GO
ALTER TABLE [dbo].[tblSubjectLayout] CHECK CONSTRAINT [FK_tblSubjectLayout_tblSubjectAction]
GO
/****** Object:  ForeignKey [FK_tblSubjectLayout_tblSubjectField]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSubjectLayout]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectLayout_tblSubjectField] FOREIGN KEY([SubjectFieldId])
REFERENCES [dbo].[tblSubjectField] ([SubjectFieldId])
GO
ALTER TABLE [dbo].[tblSubjectLayout] CHECK CONSTRAINT [FK_tblSubjectLayout_tblSubjectField]
GO
/****** Object:  ForeignKey [FK_tblSupplierProperty_tblSupplier]    Script Date: 01/11/2012 15:30:57 ******/
ALTER TABLE [dbo].[tblSupplierProperty]  WITH CHECK ADD  CONSTRAINT [FK_tblSupplierProperty_tblSupplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[tblSupplier] ([SupplierId])
GO
ALTER TABLE [dbo].[tblSupplierProperty] CHECK CONSTRAINT [FK_tblSupplierProperty_tblSupplier]
GO
