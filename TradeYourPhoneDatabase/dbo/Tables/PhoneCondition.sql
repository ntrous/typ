CREATE TABLE [dbo].[PhoneCondition] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Condition] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PrimaryKey_ab6e3ff5-fd6d-4039-ad1d-dbd4f1d9b265] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneCondition_ID]
    ON [dbo].[PhoneCondition]([ID] ASC);

