CREATE TABLE [dbo].[words] (
    [entryID]  BIGINT       IDENTITY (176024, 1) NOT NULL,
    [word]     VARCHAR (25) NOT NULL,
    [wordrank] FLOAT (53)   NULL,
    CONSTRAINT [PK_words] PRIMARY KEY CLUSTERED ([entryID] ASC)
);

