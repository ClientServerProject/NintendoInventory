CREATE TABLE [dbo].[Console] (
    [ConsoleID]       INT           NOT NULL,
    [ConsoleName]     VARCHAR (250) NOT NULL,
    [ReleaseDate]     DATETIME      NOT NULL,
    [ConsoleImageURL] VARCHAR (300) NOT NULL,
    [Price]           MONEY         NOT NULL,
    CONSTRAINT [PK_Console] PRIMARY KEY CLUSTERED ([ConsoleID] ASC)
);

