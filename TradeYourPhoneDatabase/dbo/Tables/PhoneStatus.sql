CREATE TABLE [dbo].[PhoneStatus] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [PhoneStatus] VARCHAR (150) NOT NULL,
    [SortOrder]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

