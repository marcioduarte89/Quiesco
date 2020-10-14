CREATE TABLE [dbo].[Room] (
    [Id]               INT NOT NULL,
    [Type]             INT NOT NULL,
    [AccomodationType] INT NOT NULL,
    [NrOfOccupants]       INT NOT NULL,
    [PropertyId]       INT NOT NULL,
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Room_Property] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Room_PropertyID]
    ON [dbo].[Room]([PropertyId] ASC);

