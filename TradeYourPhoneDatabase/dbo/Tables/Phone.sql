CREATE TABLE [dbo].[Phone] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [QuoteId]          INT          NULL,
    [PhoneMakeId]      INT          NOT NULL,
    [PhoneModelId]     INT          NOT NULL,
    [PhoneConditionId] INT          NOT NULL,
    [PurchaseAmount]   MONEY        NULL,
    [SaleAmount]       MONEY        NULL,
    [PhoneStatusId]    INT          NOT NULL,
    [IMEI]             VARCHAR (20) NULL,
    [PhoneChecklist] VARCHAR(MAX) NULL, 
    [PhoneNotes] VARCHAR(MAX) NULL, 
    [PhoneDescription] VARCHAR(MAX) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Phone_PhoneCondition] FOREIGN KEY ([PhoneConditionId]) REFERENCES [dbo].[PhoneCondition] ([ID]),
    CONSTRAINT [FK_Phone_PhoneMake] FOREIGN KEY ([PhoneMakeId]) REFERENCES [dbo].[PhoneMake] ([ID]),
    CONSTRAINT [FK_Phone_PhoneModel] FOREIGN KEY ([PhoneModelId]) REFERENCES [dbo].[PhoneModel] ([ID]),
    CONSTRAINT [FK_Phone_PhoneStatus] FOREIGN KEY ([PhoneStatusId]) REFERENCES [dbo].[PhoneStatus] ([Id]),
    CONSTRAINT [FK_Phone_Quote] FOREIGN KEY ([QuoteId]) REFERENCES [dbo].[Quote] ([ID])
);

