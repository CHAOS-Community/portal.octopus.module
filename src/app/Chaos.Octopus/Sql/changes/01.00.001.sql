﻿ALTER TABLE `Job` 
ADD COLUMN `CreatedByUserId` BINARY(16) NULL AFTER `DateCreated`;
