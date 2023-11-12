CREATE TABLE IF NOT EXISTS `user_info` (
                                           `id` INTEGER NOT NULL,
                                           `username` VARCHAR(255) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    `field` VARCHAR(255) NOT NULL,
    `gender` VARCHAR(255) NOT NULL,
    `role` INTEGER NOT NULL COMMENT '1是admin',
    `createTime` VARCHAR(255) NOT NULL,
    `updateTime` VARCHAR(255) NOT NULL,
    `createdAt` DATETIME NOT NULL,
    `updatedAt` DATETIME NOT NULL,
    PRIMARY KEY (`id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `post` (
                                      `id` INTEGER NOT NULL,
                                      `title` VARCHAR(255) NOT NULL,
    `content` VARCHAR(255) NOT NULL,
    `askerId` INTEGER NOT NULL,
    `createTime` VARCHAR(255) NOT NULL,
    `updateTime` VARCHAR(255) NOT NULL,
    `views` INTEGER NOT NULL,
    `delTag` INTEGER NOT NULL,
    `createdAt` DATETIME NOT NULL,
    `updatedAt` DATETIME NOT NULL,
    PRIMARY KEY (`id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `reply` (
                                       `id` INTEGER NOT NULL,
                                       `content` VARCHAR(255) NOT NULL,
    `likes` INTEGER NOT NULL,
    `userId` INTEGER NOT NULL,
    `postId` INTEGER NOT NULL,
    `isAccepted` JSON NOT NULL,
    `createTime` VARCHAR(255) NOT NULL,
    `updateTime` VARCHAR(255) NOT NULL,
    `delTag` INTEGER NOT NULL,
    `createdAt` DATETIME NOT NULL,
    `updatedAt` DATETIME NOT NULL,
    PRIMARY KEY (`id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `like` (
                                      `id` INTEGER NOT NULL,
                                      `replyId` INTEGER NOT NULL,
                                      `userId` INTEGER NOT NULL,
                                      `createdAt` DATETIME NOT NULL,
                                      `updatedAt` DATETIME NOT NULL,
                                      PRIMARY KEY (`id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;