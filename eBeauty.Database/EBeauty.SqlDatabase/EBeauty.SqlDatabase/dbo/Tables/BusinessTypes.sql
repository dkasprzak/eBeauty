CREATE TABLE [dbo].[BusinessTypes]
(
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    CONSTRAINT [PK_BusinessTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE INDEX [BQ_BusinessTypes_Name] ON [dbo].[BusinessTypes] ([Name]);
