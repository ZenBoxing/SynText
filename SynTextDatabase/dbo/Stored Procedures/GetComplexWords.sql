﻿CREATE PROCEDURE [dbo].[GetComplexWords]
	
AS
begin
	set nocount on;

	Select dbo.words.word 
	from dbo.words
	where dbo.words.wordrank > 2058885;
end
