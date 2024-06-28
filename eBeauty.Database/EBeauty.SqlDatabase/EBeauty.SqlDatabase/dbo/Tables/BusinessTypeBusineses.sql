CREATE TABLE [dbo].[BusinessTypeBusinesses]
(
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [BusinessTypeId] INT NOT NULL,
    [BusinessId] INT NOT NULL,
    CONSTRAINT [PK_BusinessTypeBusinesses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BusinessTypeBusinesses_BusinessTypes] FOREIGN KEY ([BusinessTypeId]) REFERENCES [dbo].[BusinessTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BusinessTypeBusinesses_Businesses] FOREIGN KEY ([BusinessId]) REFERENCES [dbo].[Businesses] ([Id]) ON DELETE CASCADE
);

GO
CREATE INDEX [IX_BusinessTypeBusinesses_BusinessTypeId] ON [dbo].[BusinessTypeBusinesses] ([BusinessTypeId]);

GO
CREATE INDEX [IX_BusinessTypeBusinesses_BusinessId] ON [dbo].[BusinessTypeBusinesses] ([BusinessId]);