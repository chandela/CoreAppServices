CREATE TABLE [dbo].[Branch] (
    [BranchId]         BIGINT           IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (100)   NOT NULL,
	[TimeZoneId]     NVARCHAR (200)   NULL,
    [ContactPerson]  NVARCHAR (50)    NULL,
    [Code]           AS               (concat('GE-',CONVERT([varchar],[BranchId]))),
    [DomainName]     NVARCHAR (20)    NULL,
    [IsActive]       BIT              CONSTRAINT [DF_Branch_IsActive] DEFAULT ((0)) NOT NULL,
    [CreateDateTime] DATETIME         CONSTRAINT [DF_Branch_CreateDateTime] DEFAULT (getdate()) NOT NULL,
    [UpdateDateTime] DATETIME         CONSTRAINT [DF_Branch_UpdateDateTime] DEFAULT (getdate()) NOT NULL,
    [CreateUserId]   BIGINT           NULL,
    [UpdateUserId]   BIGINT           NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    [AddressId]      BIGINT           NULL,
    CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED ([BranchId] ASC),
    CONSTRAINT [FK_Branch_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]),
    CONSTRAINT [IX_Branch] UNIQUE NONCLUSTERED ([BranchId] ASC)
);



