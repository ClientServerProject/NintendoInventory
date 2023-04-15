CREATE TABLE [dbo].[Game] (
    [GameID]          INT             IDENTITY (1, 1) NOT NULL,
    [GameTitle]       VARCHAR (250)   NOT NULL,
<<<<<<< HEAD
    [ReleaseYear]     INT        NULL,
=======
    [ReleaseYear]     INT             NULL,
>>>>>>> 5bc1b291b29bde9b767b92ac7ae7f5e4fb394c5a
    [ConsoleID]       INT             NULL,
    [GameDescription] VARCHAR (2000)  NULL,
    [GameImageURL]    VARCHAR (200)   NOT NULL,
    [ESBRRatingID]    INT             NULL,
    [Price]           DECIMAL (19, 2) NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_Game_Console] FOREIGN KEY ([ConsoleID]) REFERENCES [dbo].[Console] ([ConsoleID]) ON DELETE SET NULL
);

















