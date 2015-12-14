CREATE TABLE [dbo].[Configuration]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Key] VARCHAR(255) NOT NULL, 
    [Value] VARCHAR(MAX) NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [LastModifiedDate] DATETIME NULL, 
    [CreatedBy] NVARCHAR(128) NOT NULL, 
    [LastModifiedBy] NVARCHAR(128) NULL, 
    CONSTRAINT [FK_ConfigurationCreatedBy_ToAspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_ConfigurationLastModifiedBy_ToAspNetUsers] FOREIGN KEY ([LastModifiedBy]) REFERENCES [AspNetUsers]([Id])
)

GO

CREATE INDEX [IX_Configuration_Key] ON [dbo].[Configuration] ([Key])
