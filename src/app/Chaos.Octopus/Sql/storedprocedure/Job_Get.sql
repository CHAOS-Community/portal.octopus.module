CREATE PROCEDURE Job_Get
(
	Id	VARCHAR(64),
	Status VARCHAR(255)
)
BEGIN

	SELECT
		*
	FROM
		Job
	WHERE
			(Id IS NULL OR Job.Id = Id)
		AND (Status IS NULL OR Job.Status = Status);

END