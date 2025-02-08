CREATE TABLE [dbo].[UserProfiles] (
    [ID]   UNIQUEIDENTIFIER    NOT NULL,
    [FirstName]   NVARCHAR (MAX)   NULL,
    [LastName]    NVARCHAR (MAX)   NULL,
    [Mobile]      NVARCHAR (MAX)   NULL,
    [Email]       NVARCHAR (MAX)   NULL,
    [DateofBirth] DATE             NULL,
    [Gender]      INT              NULL,
    [Address]     NVARCHAR (MAX)   NULL,
    [IsDeleted] BIT NULL, 
);



