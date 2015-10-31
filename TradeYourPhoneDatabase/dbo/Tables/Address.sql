CREATE TABLE [dbo].[Address] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [AddressLine1] NVARCHAR (255) NOT NULL,
    [AddressLine2] NVARCHAR (255) NOT NULL,
    [StateId]      INT            NOT NULL,
    [PostCode]     NVARCHAR (4)   NOT NULL,
    [CountryId]    INT            CONSTRAINT [ColumnDefault_c27302e6-339e-4198-9711-773c72f1e2ff] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PrimaryKey_512150ec-4549-4442-8f66-4bd936dc3496] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Address_0] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([ID]),
    CONSTRAINT [FK_Address_1] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([ID])
);

