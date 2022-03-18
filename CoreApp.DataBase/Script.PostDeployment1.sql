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


/*Country*/
SET IDENTITY_INSERT [dbo].Country ON;
insert into Country([CountryId],[CountryName],[IsActive]) values(1,'India',1);
SET IDENTITY_INSERT [dbo].Country OFF;





INSERT INTO ConfigurationSettings (BranchId,IsActive,SettingName,Value)
SELECT
	1,
	1,
	[SettingName],
	[Value]
FROM 
[ConfigurationSettingTemplate]
GO
