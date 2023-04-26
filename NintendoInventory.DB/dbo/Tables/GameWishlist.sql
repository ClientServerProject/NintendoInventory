CREATE TABLE [dbo].[GameWishlist] (
    [WishlistID] INT IDENTITY (1, 1) NOT NULL,
    [GameID]     INT NOT NULL,
    CONSTRAINT [FK_GameWishlist_Game] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID])
);







