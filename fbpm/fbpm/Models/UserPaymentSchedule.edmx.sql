
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/01/2013 19:21:19
-- Generated from EDMX file: C:\Users\I048564\Documents\GitHub\FBPM_CS\fbpm\fbpm\Models\UserPaymentSchedule.edmx
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PaymentSchedule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaymentSchedule];
GO
IF OBJECT_ID(N'[dbo].[UserDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetails];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PaymentSchedule'
CREATE TABLE [dbo].[PaymentSchedule] (
    [ScheduleID] uniqueidentifier  NOT NULL,
    [UserID] varchar(25)  NOT NULL,
    [ScheduleText] varchar(200)  NULL,
    [ScheduleDate] datetime  NULL,
    [SchedulePercentage] int  NULL,
    [BookedDate] datetime  NULL,
    [BookingAmount] decimal(19,4)  NULL,
    [ScheduleAmount] decimal(19,4)  NULL,
    [RemainingAmount] decimal(19,4)  NULL,
    [UserDetailsUserID] varchar(25)  NOT NULL
);
GO

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetails] (
    [UserID] varchar(25)  NOT NULL,
    [Password] varchar(25)  NOT NULL,
    [EmailID] varchar(50)  NULL,
    [Contact1] varchar(12)  NULL,
    [Contact2] varchar(12)  NULL,
    [FullAddress] varchar(200)  NULL,
    [State] varchar(25)  NULL,
    [Country] varchar(20)  NULL,
    [Role] int  NULL,
    [PANNo] varchar(10)  NULL,
    [ProjectName] varchar(100)  NULL,
    [BookedDate] datetime  NULL,
    [BookedAmount] decimal(19,4)  NULL,
    [UserName] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ScheduleID] in table 'PaymentSchedule'
ALTER TABLE [dbo].[PaymentSchedule]
ADD CONSTRAINT [PK_PaymentSchedule]
    PRIMARY KEY CLUSTERED ([ScheduleID] ASC);
GO

-- Creating primary key on [UserID] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [PK_UserDetails]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserDetailsUserID] in table 'PaymentSchedule'
ALTER TABLE [dbo].[PaymentSchedule]
ADD CONSTRAINT [FK_UserDetailsPaymentSchedule]
    FOREIGN KEY ([UserDetailsUserID])
    REFERENCES [dbo].[UserDetails]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetailsPaymentSchedule'
CREATE INDEX [IX_FK_UserDetailsPaymentSchedule]
ON [dbo].[PaymentSchedule]
    ([UserDetailsUserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------