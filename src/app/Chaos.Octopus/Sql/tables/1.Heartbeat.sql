CREATE TABLE `Heartbeat` (
  `CreatedOn` datetime NOT NULL,
  `ClusterSate` TEXT NULL,
  PRIMARY KEY (`CreatedOn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
