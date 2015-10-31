CREATE TABLE [dbo].[PostageMethod] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [PostageMethodName] VARCHAR (200) NOT NULL,
    [Description]       VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

