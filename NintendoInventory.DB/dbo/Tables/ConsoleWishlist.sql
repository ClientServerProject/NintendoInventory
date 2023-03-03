CREATE TABLE [dbo].[ConsoleWishlist] (
    [WishlistID] INT NOT NULL,
    [ConsoleID]  INT NOT NULL,
    CONSTRAINT [PK_ConsoleWishlist] PRIMARY KEY CLUSTERED ([WishlistID] ASC, [ConsoleID] ASC),
    CONSTRAINT [FK_ConsoleWishlist_Console] FOREIGN KEY ([ConsoleID]) REFERENCES [dbo].[Console] ([ConsoleID]),
    CONSTRAINT [FK_ConsoleWishlist_Wishlist] FOREIGN KEY ([WishlistID]) REFERENCES [dbo].[Wishlist] ([WishlistID])
);

