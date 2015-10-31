CREATE TABLE [dbo].[QuoteStatus] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [QuoteStatusName] NVARCHAR (50) NOT NULL,
    [SortOrder]       INT           NOT NULL,
    CONSTRAINT [PrimaryKey_b085e7c6-deb0-4839-b520-5852bd60b5a7] PRIMARY KEY CLUSTERED ([ID] ASC)
);

