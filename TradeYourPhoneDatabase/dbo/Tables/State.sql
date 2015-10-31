CREATE TABLE [dbo].[State] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [StateNameShort] NVARCHAR (3)  NOT NULL,
    [StateNameLong]  NVARCHAR (40) NOT NULL,
    CONSTRAINT [PrimaryKey_56c5bfe9-b418-42e0-8743-1447cea3a0df] PRIMARY KEY CLUSTERED ([ID] ASC)
);

