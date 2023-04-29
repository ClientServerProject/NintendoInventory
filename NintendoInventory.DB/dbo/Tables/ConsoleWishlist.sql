CREATE TABLE [dbo].[ConsoleWishlist] (
    [ConsoleID] INT NOT NULL,
    CONSTRAINT [FK_ConsoleWishlist_Console] FOREIGN KEY ([ConsoleID]) REFERENCES [dbo].[Console] ([ConsoleID])
);



