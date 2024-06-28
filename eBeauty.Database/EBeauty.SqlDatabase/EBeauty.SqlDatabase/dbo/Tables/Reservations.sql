CREATE TABLE [dbo].[Reservations]
(
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [UserId] INT NOT NULL,
    [BusinessId] INT NOT NULL,
    [ServiceId] INT NOT NULL,
    [AccountUserId] INT NOT NULL,
    [StartTime] DATETIMEOFFSET NOT NULL,
    [EndTime] DATETIMEOFFSET NOT NULL,
    [Comment] NVARCHAR(MAX),
    [ReservationStatus] NVARCHAR(50),
    CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reservations_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reservations_Businesses] FOREIGN KEY ([BusinessId]) REFERENCES [dbo].[Businesses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reservations_Services] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reservations_AccountUsers] FOREIGN KEY ([AccountUserId]) REFERENCES [dbo].[AccountUsers] ([Id])
);

GO
CREATE INDEX [IX_Reservations_UserId] ON [dbo].[Reservations] ([UserId]);

GO 
CREATE INDEX [IX_Reservations_BusinessId] ON [dbo].[Reservations] ([BusinessId]);

GO
CREATE INDEX [IX_Reservations_ServiceId] ON [dbo].[Reservations] ([ServiceId]);

GO
CREATE INDEX [IX_Reservations_AccountUserId] ON [dbo].[Reservations] ([AccountUserId]);