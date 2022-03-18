CREATE TABLE [dbo].[Address]
(
    [AddressId]      BIGINT           IDENTITY (1, 1) NOT NULL,
    [DoorNo]       NVARCHAR (255)   NULL,
    [City]           NVARCHAR (128)   NOT NULL,
    [State]        NVARCHAR(128)           NOT NULL,
    [ZipCode]        BIGINT    NOT NULL,
	[Country]    NVARCHAR(128)		NOT NULL,
    [IsDeleted]      BIT              NOT NULL,
    [CreateDateTime] DATETIME         NOT NULL,
    [UpdateDateTime] DATETIME         NOT NULL,
    [CreateUserId]   BIGINT           NOT NULL,
    [UpdateUserId]   BIGINT           NOT NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC)
);
