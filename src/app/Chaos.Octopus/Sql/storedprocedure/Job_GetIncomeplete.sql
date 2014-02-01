CREATE PROCEDURE Job_GetIncomplete
(
)
BEGIN

	SELECT
		*
	FROM
		Job
	WHERE
		Status <> 'complete';

END