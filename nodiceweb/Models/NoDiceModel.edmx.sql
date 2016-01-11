
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/10/2016 10:18:16
-- Generated from EDMX file: C:\dev\nodiceweb\nodiceweb\Models\NoDiceModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bterhune];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Season_Teams]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Seasons] DROP CONSTRAINT [FK_Season_Teams];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Seasons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Seasons];
GO
IF OBJECT_ID(N'[dbo].[Teams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Seasons'
CREATE TABLE [dbo].[Seasons] (
    [Id] int  NOT NULL,
    [Year] int  NULL,
    [TeamId] int  NULL,
    [Win] int  NULL,
    [Lost] int  NULL,
    [RunsScored] int  NOT NULL,
    [RunsAllowed] int  NOT NULL,
    [PythScore] float  NOT NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Manager] nvarchar(25)  NULL,
    [League] nvarchar(max)  NOT NULL,
    [Division] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Seasons'
ALTER TABLE [dbo].[Seasons]
ADD CONSTRAINT [PK_Seasons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [PK_Teams]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TeamId] in table 'Seasons'
ALTER TABLE [dbo].[Seasons]
ADD CONSTRAINT [FK_Season_Teams]
    FOREIGN KEY ([TeamId])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Season_Teams'
CREATE INDEX [IX_FK_Season_Teams]
ON [dbo].[Seasons]
    ([TeamId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------