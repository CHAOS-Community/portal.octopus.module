CREATE PROCEDURE Job_Set
(
	Id VARCHAR(64),
	Status VARCHAR(255),
	Data MEDIUMTEXT,
	CreatedByUserId BINARY(16)
)
BEGIN

	IF EXISTS(SELECT Id FROM Job WHERE Job.Id = Id) THEN

		UPDATE
			Job
		SET
			Job.Status = Status,
			Job.Data = Data
		WHERE
			Job.Id = Id;

	ELSE

		INSERT INTO Job 
			(Id, Status, Data, DateCreated, CreatedByUserId)
		VALUES
			(Id, Status, Data, NOW(), CreatedByUserId);

	END IF;

	SELECT ROW_COUNT();

END