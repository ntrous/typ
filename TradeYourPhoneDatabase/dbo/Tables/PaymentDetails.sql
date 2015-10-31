CREATE TABLE [dbo].[PaymentDetails] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [PaymentTypeId] INT            NOT NULL,
    [BSB]           NVARCHAR (6)   NULL,
    [AccountNumber] NVARCHAR (50)  NULL,
    [PaypalEmail]   NVARCHAR (200) NULL,
    CONSTRAINT [PrimaryKey_91de8ff7-e2f5-4c4e-9664-cbef7a4b1246] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PaymentDetails_0] FOREIGN KEY ([PaymentTypeId]) REFERENCES [dbo].[PaymentType] ([ID])
);

