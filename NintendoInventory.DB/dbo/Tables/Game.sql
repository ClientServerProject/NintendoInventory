CREATE TABLE [dbo].[Game] (
    [GameID]          INT             IDENTITY (1, 1) NOT NULL,
    [GameTitle]       VARCHAR (250)   NOT NULL,
    [ReleaseYear]     INT        NULL,
    [ConsoleID]       INT             NULL,
    [GameDescription] VARCHAR (2000)  NULL,
    [GameImageURL]    VARCHAR (200)   NOT NULL,
    [ESBRRatingID]    INT             NULL,
    [Price]           DECIMAL (19, 2) NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_Game_Console] FOREIGN KEY ([ConsoleID]) REFERENCES [dbo].[Console] ([ConsoleID]) ON DELETE SET NULL
);















