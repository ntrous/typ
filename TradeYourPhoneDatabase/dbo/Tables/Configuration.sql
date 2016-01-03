CREATE TABLE [dbo].[Configuration] (
    [Id]               INT            NOT NULL IDENTITY,
    [Key]              VARCHAR (255)  NOT NULL UNIQUE,
    [Value]            VARCHAR (MAX)  NOT NULL,
    [Description]      VARCHAR (MAX)  NOT NULL,
    [CreatedDate]      DATETIME       NOT NULL,
    [LastModifiedDate] DATETIME       NULL,
    [CreatedBy]        NVARCHAR (128) NOT NULL,
    [LastModifiedBy]   NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConfigurationCreatedBy_ToAspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ConfigurationLastModifiedBy_ToAspNetUsers] FOREIGN KEY ([LastModifiedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]) 
);


GO
CREATE NONCLUSTERED INDEX [IX_Configuration_Key]
    ON [dbo].[Configuration]([Key] ASC);

