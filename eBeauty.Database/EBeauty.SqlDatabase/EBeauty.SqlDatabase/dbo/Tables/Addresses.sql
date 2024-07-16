CREATE TABLE [dbo].[Addresses]
(
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Country] NVARCHAR(100) NOT NULL,
    [City] NVARCHAR(100) NOT NULL,
    [Street] NVARCHAR(100) NOT NULL,
    [StreetNumber] NVARCHAR(50) NOT NULL,
    [PostalCode] NVARCHAR(10) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO 
CREATE INDEX [AQ_Addresses_Country] on [dbo].[Addresses] ([Country]);

GO
CREATE INDEX [AQ_Addresses_Street] on [dbo].[Addresses] ([Street]);