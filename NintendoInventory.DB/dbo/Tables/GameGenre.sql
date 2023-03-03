CREATE TABLE [dbo].[GameGenre] (
    [GameID]  INT NOT NULL,
    [GenreID] INT NOT NULL,
    CONSTRAINT [PK_GameGenre] PRIMARY KEY CLUSTERED ([GameID] ASC, [GenreID] ASC),
    CONSTRAINT [FK_Game_GameGenre] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID]),
    CONSTRAINT [FK_GameGenre_Genre1] FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genre] ([GenreID])
);

