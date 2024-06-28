CREATE TABLE [dbo].[Schedules]
(
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [AccountUserId] INT NOT NULL,
    [StartTime] DATETIMEOFFSET NOT NULL,
    [EndTime] DATETIMEOFFSET NOT NULL,
    CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Schedules_AccountUsers] FOREIGN KEY ([AccountUserId]) REFERENCES [dbo].[AccountUsers] ([Id]) ON DELETE CASCADE
);

GO 
CREATE INDEX [IX_Schedules_AccountUserId] ON [dbo].[Schedules] ([AccountUserId])