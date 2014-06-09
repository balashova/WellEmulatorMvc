WellEmulatorMvc
===============

To build .wxs that include all .xsd files from WITSML standards directory use heat.exe with next parameters:

heat dir ".\Standards" -out ".\StandardsFileGroup.wxs" -gg -var "var.WellEmulator.Service.TargetDir" -cg StandardsFileGroup -sfrag  -dr "INSTALLFOLDER"

Than replace following string:

$(var.WellEmulator.Service.TargetDir)

by this:

$(var.WellEmulator.Service.TargetDir)Standards




USE [master]
GO

IF EXISTS(select * from sys.databases where name='PDGTM')
DROP DATABASE [PDGTM]
CREATE DATABASE [PDGTM]
GO

IF EXISTS(select * from sys.databases where name='Runtime')
DROP DATABASE [Runtime]
CREATE DATABASE [Runtime]
GO

IF EXISTS(select * from sys.databases where name='WellEmulatorSettings')
DROP DATABASE [WellEmulatorSettings]
CREATE DATABASE [WellEmulatorSettings]
GO



USE [PDGTM]
GO

CREATE TABLE [dbo].[Well] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NULL,
    CONSTRAINT [PK_Well] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Values] (
    [Id]        INT  IDENTITY (1, 1) NOT NULL,
    [WellId]    INT  NOT NULL,
    [OilRate]   REAL CONSTRAINT [DF_Values_OilRate] DEFAULT ((0)) NOT NULL,
    [GasRate]   REAL CONSTRAINT [DF_Values_GasRate] DEFAULT ((0)) NOT NULL,
    [WaterRate] REAL CONSTRAINT [DF_Values_WaterRate] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Values] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Values_WellId] FOREIGN KEY ([WellId]) REFERENCES [dbo].[Well] ([Id]) ON DELETE CASCADE
);



USE [Runtime]
GO

CREATE TABLE [dbo].[History] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [TagName] VARCHAR (50) NOT NULL,
    [Value]   REAL         NOT NULL,
    CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Tag] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [TagName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([Id] ASC)
);



USE [WellEmulatorSettings]
GO

CREATE TABLE [dbo].[MapItems] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [HistorianTag]      VARCHAR (50) NOT NULL,
    [PdgtmTag]          VARCHAR (50) NOT NULL,
    [PdgtmWellName]     VARCHAR (50) NOT NULL,
    [HistorianWellName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_MapItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Settings] (
    [Id]                   INT    IDENTITY (1, 1) NOT NULL,
    [SamplingRate]         BIGINT NOT NULL,
    [ReportAutoSavePeriod] BIGINT NOT NULL,
    [ValuesDelay]          BIGINT NOT NULL,
    [ReplicationPeriod]    BIGINT NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Tags] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Group]    VARCHAR (50) NULL,
    [WellName] VARCHAR (50) NULL,
    [Object]   VARCHAR (50) NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [Value]    REAL         NULL,
    [MaxValue] REAL         NULL,
    [MinValue] REAL         NULL,
    [Delta]    REAL         NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED ([Id] ASC)
);

