CREATE TABLE [dbo].[GameWishlist] (
    [GameID] INT NOT NULL,
    CONSTRAINT [FK_GameWishlist_Game] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID])
);









