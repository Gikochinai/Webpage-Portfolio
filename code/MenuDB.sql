CREATE TABLE [dbo].[Menu] (
    [item]  VARCHAR(100) NOT NULL,
    [price] VARCHAR(50) NOT NULL,
    UNIQUE NONCLUSTERED ([item] ASC)
);