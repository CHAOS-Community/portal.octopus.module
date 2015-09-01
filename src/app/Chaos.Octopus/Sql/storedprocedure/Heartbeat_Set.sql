CREATE PROCEDURE Heartbeat_Set(
	ClusterState TEXT
)
BEGIN

	INSERT INTO Heartbeat
		(CreatedOn, ClusterState)
	VALUES
		(UTC_TIMESTAMP(), ClusterState);

	SELECT ROW_COUNT();
END