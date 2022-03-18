CREATE TABLE [dbo].[ConfigurationSettings]
(
    [ConfigurationSettingId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [BranchId]                 BIGINT         NOT NULL,
    [SettingName]            NVARCHAR (100) NOT NULL,
    [Value]                  NVARCHAR (100) NOT NULL,
    [IsActive]               BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ConfigurationSettingId] ASC)
);
