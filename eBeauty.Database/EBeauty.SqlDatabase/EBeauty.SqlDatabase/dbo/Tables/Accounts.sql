CREATE TABLE [dbo].[Accounts]
(
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [AccountType] NVARCHAR(50) NOT NULL,
    [CreateDate] DATETIMEOFFSET NOT NULL,
    [BusinessId] INT,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Accounts_Businesses] FOREIGN KEY ([BusinessId]) REFERENCES [dbo].[Businesses] ([Id]) ON DELETE CASCADE
);