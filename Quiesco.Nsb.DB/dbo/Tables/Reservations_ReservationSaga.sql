CREATE TABLE [dbo].[Reservations_ReservationSaga] (
    [Id]                        UNIQUEIDENTIFIER NOT NULL,
    [Metadata]                  NVARCHAR (MAX)   NOT NULL,
    [Data]                      NVARCHAR (MAX)   NOT NULL,
    [PersistenceVersion]        VARCHAR (23)     NOT NULL,
    [SagaTypeVersion]           VARCHAR (23)     NOT NULL,
    [Concurrency]               INT              NOT NULL,
    [Correlation_ReservationId] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_Correlation_ReservationId]
    ON [dbo].[Reservations_ReservationSaga]([Correlation_ReservationId] ASC) WHERE ([Correlation_ReservationId] IS NOT NULL);

