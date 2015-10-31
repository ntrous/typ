CREATE TABLE [dbo].[QuoteStatusHistory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuoteId] INT NOT NULL, 
    [QuoteStatusId] INT NOT NULL, 
    [StatusDate] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_QuoteStatusHistory_ToQuote] FOREIGN KEY ([QuoteId]) REFERENCES [Quote]([ID]), 
    CONSTRAINT [FK_QuoteStatusHistory_ToQuoteStatus] FOREIGN KEY ([QuoteStatusId]) REFERENCES [QuoteStatus]([ID]), 
    CONSTRAINT [FK_QuoteStatusHistory_ToUser] FOREIGN KEY ([CreatedBy]) REFERENCES [AspNetUsers]([Id])
)

GO

CREATE INDEX [IX_QuoteStatusHistory_QuoteId] ON [dbo].[QuoteStatusHistory] ([QuoteId])
