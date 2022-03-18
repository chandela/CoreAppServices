CREATE TABLE [dbo].[UserAssociate] (
    [AssociateId]    BIGINT           IDENTITY (1, 1) NOT NULL,
    [BranchId]         BIGINT           NOT NULL,
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
    CONSTRAINT [PK_UserAssociate_1] PRIMARY KEY CLUSTERED ([AssociateId] ASC),
    CONSTRAINT [FK_UserAssociate_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([BranchId]),
    CONSTRAINT [FK_UserAssociate_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);
