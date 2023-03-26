CREATE TABLE [dbo].[Game] (
    [GameID]       INT            NOT NULL,
    [GameTitle]    VARCHAR (250)  NOT NULL,
    [ReleaseDate]  DATETIME       NOT NULL,
    [ConsoleID]    INT            NOT NULL,
    [GameDescription]  VARCHAR (2000) NOT NULL,
    [GameImageURL] VARCHAR (200)  NOT NULL,
    [ESBRRatingID] INT            NOT NULL,
    [Price]        MONEY          NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_Game_Console] FOREIGN KEY ([ConsoleID]) REFERENCES [dbo].[Console] ([ConsoleID]),
    CONSTRAINT [FK_Game_ESBRRating] FOREIGN KEY ([ESBRRatingID]) REFERENCES [dbo].[ESBRRating] ([ESBRRatingID])
);

