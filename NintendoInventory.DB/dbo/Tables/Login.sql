CREATE TABLE [dbo].[Login] (
    [LoginID]       INT           IDENTITY (1, 1) NOT NULL,
    [UserID]        INT           NOT NULL,
    [PasswordHash]  VARCHAR (300) NOT NULL,
    [Salt]          VARCHAR (50)  NOT NULL,
    [LastLoginTime] DATETIME      NOT NULL,
    CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([LoginID] ASC),
    CONSTRAINT [FK_Login_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);





