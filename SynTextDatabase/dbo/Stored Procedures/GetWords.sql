CREATE PROCEDURE [dbo].[GetWords]


AS
begin
	SELECT dbo.words.word 
	FROM dbo.words;
end
	
