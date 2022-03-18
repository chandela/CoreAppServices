CREATE TABLE [dbo].[UserAdministrator] (
    [UserAdministratorId] BIGINT           IDENTITY (1, 1) NOT NULL,
    [BranchId]              BIGINT           NOT NULL,
    [UserId]              BIGINT           NOT NULL,
    [PhotoId]             BIGINT           NULL,
    [CreateDateTime]      DATETIME         NOT NULL,
    [UpdateDateTime]      DATETIME         NOT NULL,
    [CreateUserId]        BIGINT           NOT NULL,
    [UpdateUserId]        BIGINT           NOT NULL,
    [InternalDataId]      UNIQUEIDENTIFIER NOT NULL,
    [IsDeleted]           BIT              NULL,
    CONSTRAINT [PK_UserAdmin] PRIMARY KEY CLUSTERED ([UserAdministratorId] ASC),
    CONSTRAINT [FK_UserAdministrator_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

