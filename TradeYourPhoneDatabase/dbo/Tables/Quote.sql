CREATE TABLE [dbo].[Quote] (
    [ID]                 INT          IDENTITY (1, 1) NOT NULL,
    [CustomerId]         INT          NULL,
    [QuoteStatusId]      INT          NOT NULL,
    [PostageMethodId]    INT          NULL,
    [AgreedToTerms]      BIT          DEFAULT ((0)) NOT NULL,
    [QuoteFinalisedDate] DATETIME     NULL,
    [QuoteExpiryDate]    DATETIME     NULL,
    [QuoteReferenceId]   NVARCHAR (8) NOT NULL,
    [CreatedDate]        DATETIME     NULL,
    [Notes] VARCHAR(MAX) NULL, 
    CONSTRAINT [PrimaryKey_21eb8be7-e598-465b-a48c-12132fce0635] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Quote_0] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Quote_3] FOREIGN KEY ([QuoteStatusId]) REFERENCES [dbo].[QuoteStatus] ([ID]),
    CONSTRAINT [FK_Quote_4] FOREIGN KEY ([PostageMethodId]) REFERENCES [dbo].[PostageMethod] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Quote_CustomerId]
    ON [dbo].[Quote]([CustomerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Quote_PostageMethodId]
    ON [dbo].[Quote]([PostageMethodId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Quote_QuoteStatusId]
    ON [dbo].[Quote]([QuoteStatusId] ASC);

