CREATE TABLE [dbo].[Customer] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Email]            NVARCHAR (255) NOT NULL,
    [PhoneNumber]      NVARCHAR (50)  NOT NULL,
    [AddressId]        INT            NOT NULL,
    [PaymentDetailsId] INT            NOT NULL,
    [FullName]         VARCHAR (255)  NULL,
    CONSTRAINT [PrimaryKey_2c4dac97-0de0-41dc-b3b8-f5c05f5e0159] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Customer_0] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([ID]),
    CONSTRAINT [FK_Customer_1] FOREIGN KEY ([PaymentDetailsId]) REFERENCES [dbo].[PaymentDetails] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);



