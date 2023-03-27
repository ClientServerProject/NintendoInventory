CREATE TABLE [dbo].[Game] (
    [GameID]          INT            IDENTITY (1, 1) NOT NULL,
    [GameTitle]       VARCHAR (250)  NOT NULL,
    [ReleaseDate]     DATETIME       NOT NULL,
    [ConsoleID]       INT            NOT NULL,
    [GameDescription] VARCHAR (2000) NOT NULL,
    [GameImageURL]    VARCHAR (200)  NOT NULL,
    [ESBRRatingID]    INT            NULL,
    [Price]           MONEY          NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_Game_Console] FOREIGN KEY ([ConsoleID]) REFERENCES [dbo].[Console] ([ConsoleID]),
    CONSTRAINT [FK_Game_ESBRRating] FOREIGN KEY ([ESBRRatingID]) REFERENCES [dbo].[ESBRRating] ([ESBRRatingID])
);



