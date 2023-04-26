CREATE TABLE [dbo].[Wishlist] (
    [WishlistID] INT IDENTITY (1, 1) NOT NULL,
    [UserID]     INT NOT NULL,
    CONSTRAINT [PK_Wishlist_1] PRIMARY KEY CLUSTERED ([WishlistID] ASC),
    CONSTRAINT [FK_GameWishlist_Wishlist] FOREIGN KEY ([WishlistID]) REFERENCES [dbo].[Wishlist] ([WishlistID]),
    CONSTRAINT [FK_Wishlist_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);





