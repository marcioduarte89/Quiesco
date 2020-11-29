CREATE TABLE [dbo].[Room] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [AccommodationType] INT NOT NULL,
    [NrOfOccupants]     INT NOT NULL,
    [PropertyId]        INT NOT NULL,
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Room_Property] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Room_PropertyID]
    ON [dbo].[Room]([PropertyId] ASC);

