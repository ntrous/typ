CREATE TABLE [dbo].[PaymentType] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [PaymentTypeName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PrimaryKey_6832dd2f-8236-4826-9621-637274d3e3ec] PRIMARY KEY CLUSTERED ([ID] ASC)
);

