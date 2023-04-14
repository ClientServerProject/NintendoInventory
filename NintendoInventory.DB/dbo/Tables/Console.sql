CREATE TABLE [dbo].[Console] (
    [ConsoleID]          INT             IDENTITY (1, 1) NOT NULL,
    [ConsoleName]        VARCHAR (250)   NOT NULL,
    [ReleaseYear]        INT             NULL,
    [ConsoleImageURL]    VARCHAR (300)   NOT NULL,
    [Price]              DECIMAL (19, 2) NOT NULL,
    [ConsoleDescription] VARCHAR (1000)  NULL,
    CONSTRAINT [PK_Console] PRIMARY KEY CLUSTERED ([ConsoleID] ASC)
);















