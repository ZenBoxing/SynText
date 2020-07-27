CREATE PROCEDURE [dbo].[GetGunningFoxValues]
AS
begin
	set nocount on;
	SELECT dbo.gunningfoxvalues.Id, dbo.gunningfoxvalues.ReadingLevelByGrade
	From dbo.gunningfoxvalues;
end
