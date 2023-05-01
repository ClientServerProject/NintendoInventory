CREATE TABLE [dbo].[ProfileImage] (
    [ProfileImageID]  INT           IDENTITY (1, 1) NOT NULL,
    [ProfileImageURL] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ProfileImage] PRIMARY KEY CLUSTERED ([ProfileImageID] ASC),
    CONSTRAINT [FK_ProfileImage_ProfileImage] FOREIGN KEY ([ProfileImageID]) REFERENCES [dbo].[ProfileImage] ([ProfileImageID])
);

