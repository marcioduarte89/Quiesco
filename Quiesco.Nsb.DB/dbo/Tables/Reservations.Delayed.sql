CREATE TABLE [dbo].[Reservations.Delayed] (
    [Headers]    NVARCHAR (MAX)  NOT NULL,
    [Body]       VARBINARY (MAX) NULL,
    [Due]        DATETIME        NOT NULL,
    [RowVersion] BIGINT          IDENTITY (1, 1) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [Index_Due]
    ON [dbo].[Reservations.Delayed]([Due] ASC);

