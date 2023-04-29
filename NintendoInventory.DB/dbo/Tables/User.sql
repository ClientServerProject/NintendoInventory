﻿CREATE TABLE [dbo].[User] (
    [UserID]              INT           IDENTITY (1, 1) NOT NULL,
    [Email]               VARCHAR (150) NOT NULL,
    [FirstName]           VARCHAR (150) NOT NULL,
    [LastName]            VARCHAR (150) NOT NULL,
    [UserProfileImageURL] VARCHAR (200) NULL,
    [DateJoined]          DATETIME      NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);





