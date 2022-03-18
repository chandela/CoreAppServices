CREATE PROCEDURE [dbo].[UserLogin] @userName nvarchar(25)
AS
  DECLARE @UserId AS bigint
  DECLARE @UserInternalId AS uniqueidentifier
  DECLARE @UserBranchInternalDataId AS uniqueidentifier
  DECLARE @Password nvarchar(25)
  DECLARE @IsActivated bit = 0
  DECLARE @IsAdmin bit = 0
  DECLARE @IsClient bit = 0
  DECLARE @IsAssociate bit = 0
  DECLARE @IsBranchActivated bit = 0
  DECLARE @IsActive bit = 1


  SELECT
    @UserId = u.[UserId],
    @UserInternalId = u.InternalDataId,
    @Password = u.[Password],
    @UserBranchInternalDataId = c.InternalDataId,
    @IsBranchActivated = c.IsActive,
    @IsActivated = CASE ISNULL(ua.UserActivationId,0) WHEN 0 THEN 0 ELSE 1 END
  FROM dbo.[User] u WITH (NOLOCK)
  INNER JOIN Branch c WITH (NOLOCK)
    ON c.BranchId = u.BranchId
  LEFT JOIN UserActivation ua WITH (NOLOCK)
  ON ua.UserId = u.UserId and ISNULL(ua.ActivationDate,0) <> 0
  WHERE u.UserName LIKE @userName 

  SELECT
    @IsAdmin =
              CASE UserId
                WHEN NULL THEN 0
                ELSE 1
              END
  FROM UserAdministrator WITH (NOLOCK)
  WHERE UserId = @UserId

  SELECT
    @IsAssociate =
                  CASE UserId
                    WHEN NULL THEN 0
                    ELSE IsActive
                  END
  FROM UserAssociate WITH (NOLOCK)
  WHERE UserId = @UserId
  AND IsDeleted = 0

  --SELECT
  --  @IsTrainer =
  --              CASE userid
  --                WHEN NULL THEN 0
  --                ELSE IsActive
  --              END
  --FROM UserTrainer WITH (NOLOCK)
  --WHERE USERId = @UserId
  --AND IsDeleted = 0
  

  IF @IsAdmin = 0
    AND @IsAssociate = 0
    AND @IsClient = 0
  BEGIN
    SET @IsActive = 0
  END

  SELECT
    UserId = @UserId,
    UserInternalId = @UserInternalId,
    UserBranchInternalDataId = @UserBranchInternalDataId,
    Password = @Password,
    IsActivated = iif( @IsAdmin = 1 ,CAST(1 as BIT), @IsActivated ),
    IsAdmin = @IsAdmin,
    IsActive = @IsActive,
    IsBranchActivated = @IsBranchActivated
where 
 @UserId is not null

GO