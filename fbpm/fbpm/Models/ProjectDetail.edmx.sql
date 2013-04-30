
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/30/2013 14:01:38
-- Generated from EDMX file: C:\Users\I048548\Documents\GitHub\FBPM_CS\fbpm\fbpm\Models\ProjectDetail.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [fbpm];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProjectDetailFlatDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FlatDetails] DROP CONSTRAINT [FK_ProjectDetailFlatDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectDetailProjectSchedule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectSchedules] DROP CONSTRAINT [FK_ProjectDetailProjectSchedule];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FlatDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FlatDetails];
GO
IF OBJECT_ID(N'[dbo].[ProjectDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectDetails];
GO
IF OBJECT_ID(N'[dbo].[ProjectSchedules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectSchedules];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FlatDetails'
CREATE TABLE [dbo].[FlatDetails] (
    [FlatID] varchar(25)  NOT NULL,
    [ProjectID] varchar(2)  NOT NULL,
    [SuperBuiltUpArea] bigint  NULL,
    [LayoutImg] varbinary(max)  NULL,
    [ProjectDetailProjectID] varchar(25)  NOT NULL
);
GO

-- Creating table 'ProjectDetails'
CREATE TABLE [dbo].[ProjectDetails] (
    [ProjectID] varchar(25)  NOT NULL,
    [ProjectName] varchar(100)  NULL,
    [NoOfBlocks] int  NULL,
    [NoOfFloors] int  NULL,
    [NoOfFlats] bigint  NULL,
    [Amenities] varchar(1500)  NULL
);
GO

-- Creating table 'ProjectSchedules'
CREATE TABLE [dbo].[ProjectSchedules] (
    [ScheduleID] int  NOT NULL,
    [ProjectID] varchar(2)  NOT NULL,
    [ScheduleText] varchar(200)  NULL,
    [ScheduleDate] datetime  NULL,
    [SchedulePercentage] int  NULL,
    [ProjectDetailProjectID] varchar(25)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [FlatID], [ProjectID] in table 'FlatDetails'
ALTER TABLE [dbo].[FlatDetails]
ADD CONSTRAINT [PK_FlatDetails]
    PRIMARY KEY CLUSTERED ([FlatID], [ProjectID] ASC);
GO

-- Creating primary key on [ProjectID] in table 'ProjectDetails'
ALTER TABLE [dbo].[ProjectDetails]
ADD CONSTRAINT [PK_ProjectDetails]
    PRIMARY KEY CLUSTERED ([ProjectID] ASC);
GO

-- Creating primary key on [ScheduleID], [ProjectID] in table 'ProjectSchedules'
ALTER TABLE [dbo].[ProjectSchedules]
ADD CONSTRAINT [PK_ProjectSchedules]
    PRIMARY KEY CLUSTERED ([ScheduleID], [ProjectID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProjectDetailProjectID] in table 'FlatDetails'
ALTER TABLE [dbo].[FlatDetails]
ADD CONSTRAINT [FK_ProjectDetailFlatDetail]
    FOREIGN KEY ([ProjectDetailProjectID])
    REFERENCES [dbo].[ProjectDetails]
        ([ProjectID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDetailFlatDetail'
CREATE INDEX [IX_FK_ProjectDetailFlatDetail]
ON [dbo].[FlatDetails]
    ([ProjectDetailProjectID]);
GO

-- Creating foreign key on [ProjectDetailProjectID] in table 'ProjectSchedules'
ALTER TABLE [dbo].[ProjectSchedules]
ADD CONSTRAINT [FK_ProjectDetailProjectSchedule]
    FOREIGN KEY ([ProjectDetailProjectID])
    REFERENCES [dbo].[ProjectDetails]
        ([ProjectID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDetailProjectSchedule'
CREATE INDEX [IX_FK_ProjectDetailProjectSchedule]
ON [dbo].[ProjectSchedules]
    ([ProjectDetailProjectID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------