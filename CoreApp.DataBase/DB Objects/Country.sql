CREATE TABLE [dbo].[Country] (
    [CountryId]   INT           IDENTITY (1, 1) NOT NULL,
    [CountryName] NVARCHAR (50) NOT NULL,
    [IsActive]    BIT           NOT NULL,
    CONSTRAINT [PK_country] PRIMARY KEY CLUSTERED ([CountryId] ASC) WITH (FILLFACTOR = 90)
);

