CREATE PROCEDURE GetWordRank
	-- Add the parameters for the stored procedure here
	@word nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT wordrank from dbo.words WHERE word = @word
END
