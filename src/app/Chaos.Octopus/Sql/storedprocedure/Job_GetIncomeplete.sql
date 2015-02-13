CREATE PROCEDURE Job_GetIncomplete
(
)
BEGIN

	SELECT
		*
	FROM
		Job
	WHERE
		Status <> 'complete' AND Status <> 'failed';

END