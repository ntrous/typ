CREATE TABLE [dbo].[PhoneMake] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [MakeName] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PrimaryKey_18366627-3180-437a-bda2-5b0b878d9788] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneMake_ID]
    ON [dbo].[PhoneMake]([ID] ASC);

