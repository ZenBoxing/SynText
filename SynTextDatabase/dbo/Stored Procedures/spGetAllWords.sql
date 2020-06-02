CREATE PROCEDURE [dbo].[spGetAllWords]


AS
begin
	SELECT dbo.words.word 
	FROM dbo.words;
end
	
