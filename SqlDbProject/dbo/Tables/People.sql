CREATE TABLE [dbo].[People] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [FistName] NVARCHAR (100)   NOT NULL,
    [LastName] NVARCHAR (100)   NOT NULL,
    [Age]      INT              NOT NULL,
    CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([Id] ASC)
);

