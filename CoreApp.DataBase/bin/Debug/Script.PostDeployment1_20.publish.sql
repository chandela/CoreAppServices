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
    [DoorNo]         NVARCHAR (255)   NULL,
    [City]           NVARCHAR (128)   NOT NULL,
    [State]          NVARCHAR (128)   NOT NULL,
    [ZipCode]        BIGINT           NOT NULL,
    [Country]        NVARCHAR (128)   NOT NULL,
    [IsDeleted]      BIT              NOT NULL,
    [CreateDateTime] DATETIME         NOT NULL,
    [UpdateDateTime] DATETIME         NOT NULL,
    [CreateUserId]   BIGINT           NOT NULL,
    [UpdateUserId]   BIGINT           NOT NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC)
);


GO
PRINT N'Creating [dbo].[Branch]...';


GO
CREATE TABLE [dbo].[Branch] (
    [BranchId]       BIGINT           IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (100)   NOT NULL,
    [TimeZoneId]     NVARCHAR (200)   NULL,
    [ContactPerson]  NVARCHAR (50)    NULL,
    [Code]           AS               (concat('GE-', CONVERT (VARCHAR, [BranchId]))),
    [DomainName]     NVARCHAR (20)    NULL,
    [IsActive]       BIT              NOT NULL,
    [CreateDateTime] DATETIME         NOT NULL,
    [UpdateDateTime] DATETIME         NOT NULL,
    [CreateUserId]   BIGINT           NULL,
    [UpdateUserId]   BIGINT           NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    [AddressId]      BIGINT           NULL,
    CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED ([BranchId] ASC),
    CONSTRAINT [IX_Branch] UNIQUE NONCLUSTERED ([BranchId] ASC)
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
PRINT N'Creating [dbo].[Country]...';


GO
CREATE TABLE [dbo].[Country] (
    [CountryId]   INT           IDENTITY (1, 1) NOT NULL,
    [CountryName] NVARCHAR (50) NOT NULL,
    [IsActive]    BIT           NOT NULL,
    CONSTRAINT [PK_country] PRIMARY KEY CLUSTERED ([CountryId] ASC) WITH (FILLFACTOR = 90)
);


GO
PRINT N'Creating [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [UserId]         BIGINT           IDENTITY (1, 1) NOT NULL,
    [UniqueNumber]   BIGINT           NOT NULL,
    [UserName]       NVARCHAR (25)    NOT NULL,
    [Password]       NVARCHAR (128)   NULL,
    [FirstName]      NVARCHAR (50)    NOT NULL,
    [LastName]       NVARCHAR (50)    NOT NULL,
    [Age]            INT              NULL,
    [Gender]         NVARCHAR (75)    NULL,
    [Email]          NVARCHAR (75)    NULL,
    [IsActive]       BIT              NULL,
    [IsDeleted]      BIT              NOT NULL,
    [CreateDateTime] DATETIME         NOT NULL,
    [UpdateDateTime] DATETIME         NOT NULL,
    [CreateUserId]   BIGINT           NOT NULL,
    [UpdateUserId]   BIGINT           NOT NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    [IsActivated]    BIT              NULL,
    [AddressId]      BIGINT           NOT NULL,
    [LastLoggedIn]   DATETIME         NULL,
    [UserType]       NVARCHAR (100)   NULL,
    [BranchId]       BIGINT           NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
PRINT N'Creating [dbo].[UserActivation]...';


GO
CREATE TABLE [dbo].[UserActivation] (
    [UserActivationId]        BIGINT           IDENTITY (1, 1) NOT NULL,
    [UserId]                  BIGINT           NOT NULL,
    [ActivationRequestedDate] DATETIME         NOT NULL,
    [ActivationDate]          DATETIME         NULL,
    [IsActive]                BIT              NOT NULL,
    [ExpirationDate]          DATETIME         NOT NULL,
    [InternalDataId]          UNIQUEIDENTIFIER NOT NULL,
    [CreateDateTime]          DATETIME         NOT NULL,
    [UpdateDateTime]          DATETIME         NOT NULL,
    [CreateUserId]            BIGINT           NOT NULL,
    [UpdateUserId]            BIGINT           NOT NULL,
    CONSTRAINT [PK_UserActivationHistory] PRIMARY KEY CLUSTERED ([UserActivationId] ASC)
);


GO
PRINT N'Creating [dbo].[UserAdministrator]...';


GO
CREATE TABLE [dbo].[UserAdministrator] (
    [UserAdministratorId] BIGINT           IDENTITY (1, 1) NOT NULL,
    [BranchId]            BIGINT           NOT NULL,
    [UserId]              BIGINT           NOT NULL,
    [PhotoId]             BIGINT           NULL,
    [CreateDateTime]      DATETIME         NOT NULL,
    [UpdateDateTime]      DATETIME         NOT NULL,
    [CreateUserId]        BIGINT           NOT NULL,
    [UpdateUserId]        BIGINT           NOT NULL,
    [InternalDataId]      UNIQUEIDENTIFIER NOT NULL,
    [IsDeleted]           BIT              NULL,
    CONSTRAINT [PK_UserAdmin] PRIMARY KEY CLUSTERED ([UserAdministratorId] ASC)
);


GO
PRINT N'Creating [dbo].[UserAssociate]...';


GO
CREATE TABLE [dbo].[UserAssociate] (
    [AssociateId]    BIGINT           IDENTITY (1, 1) NOT NULL,
    [BranchId]       BIGINT           NOT NULL,
    [UserId]         BIGINT           NOT NULL,
    [StartDate]      DATETIME         NOT NULL,
    [PhotoId]        BIGINT           NULL,
    [IsDeleted]      BIT              NULL,
    [IsActive]       BIT              NULL,
    [CreateDateTime] DATETIME         NOT NULL,
    [UpdateDateTime] DATETIME         NOT NULL,
    [CreateUserId]   BIGINT           NOT NULL,
    [UpdateUserId]   BIGINT           NOT NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserAssociate_1] PRIMARY KEY CLUSTERED ([AssociateId] ASC)
);


GO
PRINT N'Creating [dbo].[DF_Branch_IsActive]...';


GO
ALTER TABLE [dbo].[Branch]
    ADD CONSTRAINT [DF_Branch_IsActive] DEFAULT ((0)) FOR [IsActive];


GO
PRINT N'Creating [dbo].[DF_Branch_CreateDateTime]...';


GO
ALTER TABLE [dbo].[Branch]
    ADD CONSTRAINT [DF_Branch_CreateDateTime] DEFAULT (getdate()) FOR [CreateDateTime];


GO
PRINT N'Creating [dbo].[DF_Branch_UpdateDateTime]...';


GO
ALTER TABLE [dbo].[Branch]
    ADD CONSTRAINT [DF_Branch_UpdateDateTime] DEFAULT (getdate()) FOR [UpdateDateTime];


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
    ADD DEFAULT (0) FOR [UserType];


GO
PRINT N'Creating [dbo].[FK_Branch_Address]...';


GO
ALTER TABLE [dbo].[Branch] WITH NOCHECK
    ADD CONSTRAINT [FK_Branch_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]);


GO
PRINT N'Creating [dbo].[FK_User_Address]...';


GO
ALTER TABLE [dbo].[User] WITH NOCHECK
    ADD CONSTRAINT [FK_User_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]);


GO
PRINT N'Creating [dbo].[FK_User_Branch]...';


GO
ALTER TABLE [dbo].[User] WITH NOCHECK
    ADD CONSTRAINT [FK_User_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([BranchId]);


GO
PRINT N'Creating [dbo].[FK_UserActivation_User]...';


GO
ALTER TABLE [dbo].[UserActivation] WITH NOCHECK
    ADD CONSTRAINT [FK_UserActivation_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]);


GO
PRINT N'Creating [dbo].[FK_UserAdministrator_User]...';


GO
ALTER TABLE [dbo].[UserAdministrator] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAdministrator_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]);


GO
PRINT N'Creating [dbo].[FK_UserAssociate_Branch]...';


GO
ALTER TABLE [dbo].[UserAssociate] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAssociate_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([BranchId]);


GO
PRINT N'Creating [dbo].[FK_UserAssociate_User]...';


GO
ALTER TABLE [dbo].[UserAssociate] WITH NOCHECK
    ADD CONSTRAINT [FK_UserAssociate_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]);


GO
PRINT N'Creating [dbo].[UserLogin]...';


GO
CREATE PROCEDURE [dbo].[UserLogin] @userName nvarchar(25)
AS
  DECLARE @UserId AS bigint
  DECLARE @UserInternalId AS uniqueidentifier
  DECLARE @UserBranchInternalDataId AS uniqueidentifier
  DECLARE @Password nvarchar(25)
  DECLARE @IsActivated bit = 0
  DECLARE @IsAdmin bit = 0
  DECLARE @IsClient bit = 0
  DECLARE @IsAssociate bit = 0
  DECLARE @IsBranchActivated bit = 0
  DECLARE @IsActive bit = 1


  SELECT
    @UserId = u.[UserId],
    @UserInternalId = u.InternalDataId,
    @Password = u.[Password],
    @UserBranchInternalDataId = c.InternalDataId,
    @IsBranchActivated = c.IsActive,
    @IsActivated = CASE ISNULL(ua.UserActivationId,0) WHEN 0 THEN 0 ELSE 1 END
  FROM dbo.[User] u WITH (NOLOCK)
  INNER JOIN Branch c WITH (NOLOCK)
    ON c.BranchId = u.BranchId
  LEFT JOIN UserActivation ua WITH (NOLOCK)
  ON ua.UserId = u.UserId and ISNULL(ua.ActivationDate,0) <> 0
  WHERE u.UserName LIKE @userName 

  SELECT
    @IsAdmin =
              CASE UserId
                WHEN NULL THEN 0
                ELSE 1
              END
  FROM UserAdministrator WITH (NOLOCK)
  WHERE UserId = @UserId

  SELECT
    @IsAssociate =
                  CASE UserId
                    WHEN NULL THEN 0
                    ELSE IsActive
                  END
  FROM UserAssociate WITH (NOLOCK)
  WHERE UserId = @UserId
  AND IsDeleted = 0

  --SELECT
  --  @IsTrainer =
  --              CASE userid
  --                WHEN NULL THEN 0
  --                ELSE IsActive
  --              END
  --FROM UserTrainer WITH (NOLOCK)
  --WHERE USERId = @UserId
  --AND IsDeleted = 0
  

  IF @IsAdmin = 0
    AND @IsAssociate = 0
    AND @IsClient = 0
  BEGIN
    SET @IsActive = 0
  END

  SELECT
    UserId = @UserId,
    UserInternalId = @UserInternalId,
    UserBranchInternalDataId = @UserBranchInternalDataId,
    Password = @Password,
    IsActivated = iif( @IsAdmin = 1 ,CAST(1 as BIT), @IsActivated ),
    IsAdmin = @IsAdmin,
    IsActive = @IsActive,
    IsBranchActivated = @IsBranchActivated
where 
 @UserId is not null
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
           ,[DoorNo]
           ,[City]
           ,[State]
           ,[ZipCode]
           ,[Country]
           ,[IsDeleted]
           ,[CreateDateTime]
           ,[UpdateDateTime]
           ,[CreateUserId]
           ,[UpdateUserId]
           ,[InternalDataId])
     VALUES
           (1
		   ,'390, Free mont street'
           ,'Hyderabad'
           ,'Telangana'
           ,505209
           ,'India'
           ,0
           ,GETDATE()
           ,GETDATE()
           ,1
           ,1
           ,NEWID()
		   )
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
ALTER TABLE [dbo].[Branch] WITH CHECK CHECK CONSTRAINT [FK_Branch_Address];

ALTER TABLE [dbo].[User] WITH CHECK CHECK CONSTRAINT [FK_User_Address];

ALTER TABLE [dbo].[User] WITH CHECK CHECK CONSTRAINT [FK_User_Branch];

ALTER TABLE [dbo].[UserActivation] WITH CHECK CHECK CONSTRAINT [FK_UserActivation_User];

ALTER TABLE [dbo].[UserAdministrator] WITH CHECK CHECK CONSTRAINT [FK_UserAdministrator_User];

ALTER TABLE [dbo].[UserAssociate] WITH CHECK CHECK CONSTRAINT [FK_UserAssociate_Branch];

ALTER TABLE [dbo].[UserAssociate] WITH CHECK CHECK CONSTRAINT [FK_UserAssociate_User];


GO
PRINT N'Update complete.';


GO