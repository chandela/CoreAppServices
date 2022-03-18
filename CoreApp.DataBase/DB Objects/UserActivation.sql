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
    CONSTRAINT [PK_UserActivationHistory] PRIMARY KEY CLUSTERED ([UserActivationId] ASC),
    CONSTRAINT [FK_UserActivation_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);