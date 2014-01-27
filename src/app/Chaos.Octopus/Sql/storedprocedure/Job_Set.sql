CREATE PROCEDURE Job_Set
(
	Id VARCHAR(64),
	Status VARCHAR(255),
	Data MEDIUMTEXT
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
			(Id, Status, Data, DateCreated)
		VALUES
			(Id, Status, Data, NOW());

	END IF;

	SELECT ROW_COUNT();

END