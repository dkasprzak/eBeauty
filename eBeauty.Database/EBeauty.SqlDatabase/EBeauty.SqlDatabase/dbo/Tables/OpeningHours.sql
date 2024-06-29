CREATE TABLE [dbo].[OpeningHours]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[DayOfWeek] VARCHAR(20) NOT NULL,
	[OpeningTime] TIME,
	[ClosingTime] TIME,
	[BusinessId] INT NOT NULL,
	CONSTRAINT [PK_OpeningHours] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_OpeningHours_Businesses] FOREIGN KEY ([BusinessId]) REFERENCES [dbo].[Businesses] ([Id]) ON DELETE CASCADE
);

GO
CREATE INDEX [OQ_OpeningHours_DayOfWeek] ON [dbo].[OpeningHours] ([DayOfWeek]);

GO 
CREATE INDEX [IX_OpeningHours_BusinessId] ON [dbo].[OpeningHours] ([BusinessId]);