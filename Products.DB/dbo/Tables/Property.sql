﻿CREATE TABLE [dbo].[Property] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Type] INT           NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([Id] ASC)
);

