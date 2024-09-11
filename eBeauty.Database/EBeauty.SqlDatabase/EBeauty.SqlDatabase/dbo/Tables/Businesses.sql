﻿CREATE TABLE [dbo].[Businesses]
(
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Email] NVARCHAR(50),
    [PhoneNumber] VARCHAR(20),
    [TaxNumber] VARCHAR(10) NOT NULL,
    [Description] NVARCHAR(200),
    [AddressId] INT NOT NULL,
    CONSTRAINT [PK_Businesses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Businesses_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_Address] UNIQUE ([AddressId])
);

GO
CREATE INDEX [IX_Businesses_AddressId] ON [dbo].[Businesses] ([AddressId]);
