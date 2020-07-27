CREATE PROCEDURE [dbo].[GetWords]
AS
begin
	set nocount on;
	SELECT dbo.words.word 
	FROM dbo.words;
end
	
