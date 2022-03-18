CREATE TABLE [dbo].[User]
(
    [UserId]         BIGINT           IDENTITY (1, 1) NOT NULL,
	[UniqueNumber]   BIGINT             NOT NULL,
    [UserName]       NVARCHAR (25)    NOT NULL,
    [Password]       NVARCHAR (128)    NULL,
    [FirstName]      NVARCHAR (50)    NOT NULL,
    [LastName]       NVARCHAR (50)    NOT NULL,
    [Age]            INT              NULL,
    [Gender]         NVARCHAR (75)      NULL,
    [Email]          NVARCHAR (75)    NULL,
    [IsActive]       BIT              NULL,
    [IsDeleted]      BIT              CONSTRAINT [DF_User_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreateDateTime] DATETIME         CONSTRAINT [DF_User_CreateDateTime] DEFAULT (getdate()) NOT NULL,
    [UpdateDateTime] DATETIME         CONSTRAINT [DF_User_UpdateDateTime] DEFAULT (getdate()) NOT NULL,
    [CreateUserId]   BIGINT           NOT NULL,
    [UpdateUserId]   BIGINT           NOT NULL,
    [InternalDataId] UNIQUEIDENTIFIER NOT NULL,
    [IsActivated]    BIT              NULL,
    [AddressId]      BIGINT           NOT NULL,
    [LastLoggedIn]   DATETIME         NULL,
	[UserType]	     nvarchar(100)			  NULL default(0),
	[BranchId]       BIGINT			  NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]),
	CONSTRAINT [FK_User_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([BranchId]),
);


