CREATE PROCEDURE [dbo].[Person_Create]
	@Id UNIQUEIDENTIFIER,
	@FirstName NVARCHAR(50),
	@LastName NVaRCHAR(50),
	@Age INT
AS
BEGIN
	INSERT INTO People
	VALUES (@Id, @FirstName, @LastName,@Age)

	SELECT * FROM People WHERE Id = @Id
END