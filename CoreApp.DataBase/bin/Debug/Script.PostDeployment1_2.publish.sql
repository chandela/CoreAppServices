﻿/*
Deployment script for QuickMart

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "QuickMart"
:setvar DefaultFilePrefix "QuickMart"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367)) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Creating [dbo].[Address]...';


GO
CREATE TABLE [dbo].[Address] (
    [AddressId]      BIGINT           IDENTITY (1, 1) NOT NULL,
    [Address1]       NVARCHAR (255)   NOT NULL,
    [Address2]       NVARCHAR (255)   NULL,
    [City]           NVARCHAR (128)   NOT NULL,
    [StateId]        BIGINT           NOT NULL,
    [Email]          NVARCHAR (75)    NULL,
    [ZipCode]        NVARCHAR (20)    NOT NULL,
    [Phone]          NVARCHAR (20)    NOT NULL,
    [AddressType]    INT              NULL,
    [IsDeleted]      BIT              NOT NULL,
    [CreateDateTime] DATETIME         NOT NULL,
    [UpdateDateTime] DATETIME         NOT NULL,
    [CreateUserId]   BIGINT           NOT NULL,
    [UpdateUserId]   BIGINT           NOT NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    [GeometryLat]    NVARCHAR (50)    NULL,
    [GeometryLng]    NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC)
);


GO
PRINT N'Creating [dbo].[ConfigurationSettings]...';


GO
CREATE TABLE [dbo].[ConfigurationSettings] (
    [ConfigurationSettingId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [BranchId]               BIGINT         NOT NULL,
    [SettingName]            NVARCHAR (100) NOT NULL,
    [Value]                  NVARCHAR (100) NOT NULL,
    [IsActive]               BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ConfigurationSettingId] ASC)
);


GO
PRINT N'Creating [dbo].[ConfigurationSettingTemplate]...';


GO
CREATE TABLE [dbo].[ConfigurationSettingTemplate] (
    [ConfigurationSettingTemplateId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [SettingName]                    NVARCHAR (100) NOT NULL,
    [Value]                          NVARCHAR (100) NOT NULL,
    [IsActive]                       BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ConfigurationSettingTemplateId] ASC)
);


GO
PRINT N'Creating [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [UserId]         BIGINT           IDENTITY (1, 1) NOT NULL,
    [UserName]       NVARCHAR (25)    NOT NULL,
    [Password]       NVARCHAR (128)   NOT NULL,
    [FirstName]      NVARCHAR (50)    NOT NULL,
    [LastName]       NVARCHAR (50)    NOT NULL,
    [Age]            INT              NULL,
    [Sex]            VARCHAR (1)      NULL,
    [Email]          NVARCHAR (75)    NULL,
    [IsActive]       BIT              NULL,
    [IsDeleted]      BIT              NOT NULL,
    [CreateDateTime] DATETIME         NOT NULL,
    [UpdateDateTime] DATETIME         NOT NULL,
    [CreateUserId]   BIGINT           NOT NULL,
    [UpdateUserId]   BIGINT           NOT NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    [IsActivated]    BIT              NULL,
    [DateOfBirth]    DATETIME         NULL,
    [AddressId]      BIGINT           NOT NULL,
    [Title]          INT              NULL,
    [LastLoggedIn]   DATETIME         NULL,
    [Role]           NVARCHAR (100)   NULL,
    [UserRoleType]   INT              NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
PRINT N'Creating [dbo].[DF_User_IsDeleted]...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];


GO
PRINT N'Creating [dbo].[DF_User_CreateDateTime]...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_CreateDateTime] DEFAULT (getdate()) FOR [CreateDateTime];


GO
PRINT N'Creating [dbo].[DF_User_UpdateDateTime]...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [DF_User_UpdateDateTime] DEFAULT (getdate()) FOR [UpdateDateTime];


GO
PRINT N'Creating unnamed constraint on [dbo].[User]...';


GO
ALTER TABLE [dbo].[User]
    ADD DEFAULT (0) FOR [UserRoleType];


GO
PRINT N'Creating [dbo].[FK_User_Address]...';


GO
ALTER TABLE [dbo].[User] WITH NOCHECK
    ADD CONSTRAINT [FK_User_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]);


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].Address ON
INSERT INTO [dbo].[Address]
           (AddressId
		   ,[Address1]
           ,[Address2]
           ,[City]
           ,[StateId]
           ,[Email]
           ,[ZipCode]
           ,[Phone]
           ,[AddressType]
           ,[IsDeleted]
           ,[CreateDateTime]
           ,[UpdateDateTime]
           ,[CreateUserId]
           ,[UpdateUserId]
           ,[InternalDataId]
           ,[GeometryLat]
           ,[GeometryLng])
     VALUES
           (1
		   ,'390, Free mont street'
           ,''
           ,'Las Vegas'
           ,1
           ,'harireddydear@gmail.com'
           ,'505209'
           ,'9014203070'
           ,1
           ,0
           ,GETDATE()
           ,GETDATE()
           ,1
           ,1
           ,NEWID()
           ,NULL
           ,NULL);

SET IDENTITY_INSERT [Address] OFF


SET IDENTITY_INSERT [dbo].[ConfigurationSettingTemplate] ON
INSERT [dbo].[ConfigurationSettingTemplate] ([ConfigurationSettingTemplateId],[SettingName],[Value],[IsActive]) 
VALUES(1,N'ConnectionString',N'data source=.;initial catalog=QuickMart;Integrated Security=true;',1),
(2,N'SMTPServer',N'smtp.gmail.com',1),
(3,N'Port',N'587',1),
(4,N'UserName',N'harireddydear@gmail.com',1),
(5,N'Password',N'pp.buzops.com',1),
(6,N'HostName',N'pp.buzops.com',1);
SET IDENTITY_INSERT [dbo].[ConfigurationSettingTemplate] OFF
GO

INSERT INTO ConfigurationSettings (BranchId,IsActive,SettingName,Value)
SELECT
	1,
	1,
	[SettingName],
	[Value]
FROM 
[ConfigurationSettingTemplate]
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[User] WITH CHECK CHECK CONSTRAINT [FK_User_Address];


GO
PRINT N'Update complete.';


GO
