CREATE PROC SP_CreatePerson
(
	@Id UNIQUEIDENTIFIER,
	@FirstName NVARCHAR(50),
	@LastName NVaRCHAR(50),
	@Age INT
)
AS
BEGIN
	INSERT INTO People
	VALUES (@Id, @FirstName, @LastName,@Age)
END