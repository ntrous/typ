CREATE TABLE [dbo].[PhoneConditionPrice] (
    [ID]               INT   IDENTITY (1, 1) NOT NULL,
    [PhoneModelId]     INT   NOT NULL,
    [PhoneConditionId] INT   NOT NULL,
    [OfferAmount]      MONEY NOT NULL,
    CONSTRAINT [PrimaryKey_c861b325-9a91-4840-8d9e-b925a071133f] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PhoneConditionPrice_0] FOREIGN KEY ([PhoneModelId]) REFERENCES [dbo].[PhoneModel] ([ID]),
    CONSTRAINT [FK_PhoneConditionPrice_1] FOREIGN KEY ([PhoneConditionId]) REFERENCES [dbo].[PhoneCondition] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneConditionPrice_ID]
    ON [dbo].[PhoneConditionPrice]([ID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneConditionPrice_PhoneConditionId]
    ON [dbo].[PhoneConditionPrice]([PhoneConditionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneConditionPrice_PhoneModelId]
    ON [dbo].[PhoneConditionPrice]([PhoneModelId] ASC);

