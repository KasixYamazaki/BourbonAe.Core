-- Sample tables (adjust to your actual schema)
CREATE TABLE dbo.Users (
    UserId        NVARCHAR(50)  NOT NULL PRIMARY KEY,
    DisplayName   NVARCHAR(100) NOT NULL,
    PasswordHash  NVARCHAR(400) NOT NULL, -- PBKDF2 format: iterations.saltBase64.hashBase64
    Role          NVARCHAR(50)  NOT NULL DEFAULT 'User',
    IsActive      BIT           NOT NULL DEFAULT 1,
    LastLoginAt   DATETIME2     NULL
);

-- Optional roles table if you need many-to-many (not used in sample service)
CREATE TABLE dbo.Roles (
    RoleName NVARCHAR(50) NOT NULL PRIMARY KEY
);

CREATE TABLE dbo.UserRoles (
    UserId   NVARCHAR(50) NOT NULL,
    RoleName NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleName),
    CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId),
    CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleName) REFERENCES dbo.Roles(RoleName)
);

-- Sample insert (password is 'Pass@w0rd' - generate via AuthService.HashPassword offline)
-- INSERT INTO dbo.Users(UserId, DisplayName, PasswordHash, Role, IsActive) VALUES
-- ('admin', N'管理者', '100000.Base64Salt.Base64Hash', 'Admin', 1);
