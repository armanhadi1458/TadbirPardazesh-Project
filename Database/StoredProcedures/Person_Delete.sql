﻿CREATE PROC Person_Delete
	@Id UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM People WHERE Id = @Id
END