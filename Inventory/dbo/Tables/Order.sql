CREATE TABLE [dbo].[Order] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50) NOT NULL,
    [CreatedDate]      DATETIME      NOT NULL,
    [LastModifiedDate] DATETIME      NOT NULL
);

