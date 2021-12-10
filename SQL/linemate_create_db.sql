USE [master]

IF db_id('LineMate') is NULL
	CREATE DATABASE [LineMate]
GO

USE [LineMate]
GO

DROP TABLE IF EXISTS [UserProfile];
DROP TABLE IF EXISTS [Team];
DROP TABLE IF EXISTS [Players];
DROP TABLE IF EXISTS [SpecialTeams];
DROP TABLE IF EXISTS [SpecialTeamPlayer];
DROP TABLE IF EXISTS [Events];
GO

CREATE TABLE [UserProfile] (
  [Id] INTEGER PRIMARY KEY NOT NULL IDENTITY,
  [Name] VARCHAR(255) NOT NULL,
  [Email] VARCHAR(255) NOT NULL,
  [FirebaseUserId] nvarchar(28) NOT NULL

  CONSTRAINT UQ_FirebaseUserId UNIQUE(FirebaseUserId)
)

CREATE TABLE [Team] (
  [Id] INTEGER PRIMARY KEY NOT NULL IDENTITY,
  [Name] VARCHAR(255) NOT NULL,
  [CreatedByUserProfileId] INTEGER NOT NULL

  CONSTRAINT [FK_Team_UserProfile] FOREIGN KEY ([CreatedByUserProfileId]) REFERENCES [UserProfile]([Id])
)

CREATE TABLE [Players] (
  [Id] INTEGER PRIMARY KEY NOT NULL IDENTITY,
  [FirstName] VARCHAR(255) NOT NULL,
  [LastName] VARCHAR (255) NOT NULL,
  [Position] VARCHAR(255),
  [JerseyNumber] INTEGER,
  [TeamId] INTEGER,
  [Line] INTEGER NOT NULL

  CONSTRAINT [FK_Players_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team]([Id]) ON DELETE SET NULL
)

CREATE TABLE [SpecialTeams] (
  [Id] INTEGER PRIMARY KEY NOT NULL IDENTITY,
  [Name] VARCHAR(255) NOT NULL
)

CREATE TABLE [SpecialTeamPlayer] (
  [Id] INTEGER PRIMARY KEY NOT NULL IDENTITY,
  [SpecialTeamId] INTEGER NOT NULL,
  [PlayerId] INTEGER NOT NULL

  CONSTRAINT [FK_SpecialTeamPlayer_Players] FOREIGN KEY ([PlayerId]) REFERENCES [Players]([Id]),
  CONSTRAINT [FK_SpecialTeamPlayer_SpecialTeams] FOREIGN KEY ([SpecialTeamId]) REFERENCES [SpecialTeams]([Id])
)

CREATE TABLE [Events] (
  [Id] INTEGER PRIMARY KEY NOT NULL IDENTITY,
  [Title] VARCHAR(255) NOT NULL,
  [CreatedByUserProfileId] INTEGER NOT NULL,
  [DateTime] DATETIME,
  [Location] VARCHAR(255)

  CONSTRAINT [FK_Events_UserProfile] FOREIGN KEY ([CreatedByUserProfileId]) REFERENCES [UserProfile]([Id])
)
GO
