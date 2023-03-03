CREATE TABLE [dbo].[Wishlist] (
    [WishlistID] INT NOT NULL,
    [UserID]     INT NOT NULL,
    CONSTRAINT [PK_Wishlist_1] PRIMARY KEY CLUSTERED ([WishlistID] ASC),
    CONSTRAINT [FK_Wishlist_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

