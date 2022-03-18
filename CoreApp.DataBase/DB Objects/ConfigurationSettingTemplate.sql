CREATE TABLE [dbo].[ConfigurationSettingTemplate]
(
    [ConfigurationSettingTemplateId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [SettingName]                    NVARCHAR (100) NOT NULL,
    [Value]                          NVARCHAR (100) NOT NULL,
    [IsActive]                       BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ConfigurationSettingTemplateId] ASC)
);
