CREATE TABLE [dbo].[Services]
(
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Name] NVARCHAR(200) NOT NULL,
    [Price] DECIMAL(18, 4) NOT NULL,
    [Currency] VARCHAR(5) NOT NULL,
    [Duration] INT NOT NULL,
    [BusinessId] INT NOT NULL,
    [BusinessTypeId] INT NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Services_Businesses] FOREIGN KEY ([BusinessId]) REFERENCES [dbo].[Businesses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Services_BusinessTypes] FOREIGN KEY ([BusinessTypeId]) REFERENCES [dbo].[BusinessTypes] ([Id]) ON DELETE CASCADE,
);


GO
CREATE INDEX [IX_Services_BusinessId] ON [dbo].[Services] ([BusinessId]);

GO
CREATE INDEX [IX_Services_BusinessTypeId] ON [dbo].[Services] ([BusinessTypeId]);