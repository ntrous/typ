CREATE TABLE [dbo].[PhoneStatusHistory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PhoneId] INT NOT NULL, 
    [PhoneStatusId] INT NOT NULL, 
    [StatusDate] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_PhoneStatusHistory_ToPhoneStatus] FOREIGN KEY ([PhoneStatusId]) REFERENCES [PhoneStatus]([Id]), 
    CONSTRAINT [FK_PhoneStatusHistory_ToUser] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_PhoneStatusHistory_ToPhone] FOREIGN KEY ([PhoneId]) REFERENCES [Phone]([Id]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_PhoneStatusHistory_PhoneId] ON [dbo].[PhoneStatusHistory] ([PhoneId])
