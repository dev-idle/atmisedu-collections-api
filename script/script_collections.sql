USE MISEDU;
GO

CREATE TABLE collectionArticles (
  articleId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  articleTitle nvarchar(256) NOT NULL,
  articleAbstract nvarchar(500) NULL,
  articleContent nvarchar(max) NULL,
  articleThumbnail varchar(128) NULL,
  articlePublishedDate int NOT NULL,
  articleTimeReading int NOT NULL DEFAULT 0,
  articleViews int NOT NULL DEFAULT 0,
  articleIPAddress varchar(32) NOT NULL,
  articleBrowser varchar(32) NOT NULL,
  articleOstype varchar(64) NULL,
  createdBy int NOT NULL,
  createdDate int NOT NULL,
  isActived tinyint NOT NULL DEFAULT 1
);

CREATE TABLE collectionArticlesAward (
  awId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  articleId int NOT NULL,
  awardId int NOT NULL,
  createdBy int NOT NULL,
  createdDate int NOT NULL,
  isActived tinyint NOT NULL DEFAULT 1
);

CREATE TABLE collectionArticlesDiscussion (
  discussionId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  discussionPid int NULL,
  articleId int NOT NULL,
  createdBy int NOT NULL,
  disContent nvarchar(4000) NULL,
  createdDate int NOT NULL,
  isActived tinyint NOT NULL DEFAULT 1
);

CREATE TABLE collectionArticlesDiscussionAward (
  daId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  discussionId int NOT NULL,
  awardId int NOT NULL,
  createdBy int NOT NULL,
  createdDate int NOT NULL,
  isActived tinyint NOT NULL DEFAULT 1
);

CREATE TABLE collectionArticlesDiscussionInteraction (
  interactionId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  discussionId int NOT NULL,
  createdBy int NOT NULL,
  intnType varchar(128) NOT NULL,
  createdDate int NOT NULL,
  isActived tinyint NOT NULL DEFAULT 1
);

CREATE TABLE collectionArticlesInteraction (
  interactionId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  articleId int NOT NULL,
  createdBy int NOT NULL,
  intnType varchar(128) NOT NULL,
  createdDate int NOT NULL,
  isActived tinyint NOT NULL DEFAULT 1
);

CREATE TABLE collectionArticlesTags (
  articleId int NOT NULL,
  keyTags int NOT NULL,
  PRIMARY KEY (articleId, keyTags)
);

CREATE TABLE collectionArticlesVote (
  votesId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  articleId int NOT NULL,
  createdBy int NOT NULL,
  votesPoint tinyint NOT NULL,
  createdDate int NOT NULL,
  isActived tinyint NOT NULL DEFAULT 1
);

CREATE TABLE collectionAwardCatalog (
  awardId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  awIcon varchar(64) NOT NULL,
  awPoint int NOT NULL DEFAULT 0,
  isActived tinyint NOT NULL DEFAULT 1
);

CREATE TABLE collectionTags (
  keyTags int IDENTITY(1,1) PRIMARY KEY NOT NULL,
  tagsName varchar(64) NOT NULL,
  tagsDescription nvarchar(512) NULL,
  tagsIcon varchar(128) NULL,
  tagsStyle varchar(128) NULL,
  createdBy int NOT NULL,
  createdDate int NOT NULL,
  isActived tinyint NOT NULL DEFAULT 1
);
