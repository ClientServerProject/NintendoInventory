CREATE TABLE [dbo].[GameWishlist] (
    [WishlistID] INT NOT NULL,
    [GameID]     INT NOT NULL,
    CONSTRAINT [PK_GameWishlist] PRIMARY KEY CLUSTERED ([WishlistID] ASC, [GameID] ASC),
    CONSTRAINT [FK_GameWishlist_Game] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID]),
    CONSTRAINT [FK_GameWishlist_Wishlist] FOREIGN KEY ([WishlistID]) REFERENCES [dbo].[Wishlist] ([WishlistID])
);

