CREATE TABLE [dbo].[PhoneModel] (
    [ID]           INT             IDENTITY (1, 1) NOT NULL,
    [ModelName]    NVARCHAR (255)  NOT NULL,
    [PhoneMakeId]  INT             NOT NULL,
    [PrimaryImage] VARBINARY (MAX) NULL,
    CONSTRAINT [PrimaryKey_53d73b5b-a76b-4cf8-ae9b-80068e525a97] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PhoneModel_1] FOREIGN KEY ([PhoneMakeId]) REFERENCES [dbo].[PhoneMake] ([ID]) ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneModel_ID]
    ON [dbo].[PhoneModel]([ID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhoneModel_PhoneMakeId]
    ON [dbo].[PhoneModel]([PhoneMakeId] ASC);

