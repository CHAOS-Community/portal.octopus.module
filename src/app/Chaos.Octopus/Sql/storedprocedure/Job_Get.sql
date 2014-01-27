CREATE PROCEDURE Job_Get
(
	Status VARCHAR(255)
)
BEGIN

	SELECT
		*
	FROM
		Job
	WHERE
		(Status IS NULL OR Job.Status = Status);

END