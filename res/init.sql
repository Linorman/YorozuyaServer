CREATE TABLE IF NOT EXISTS `user_info` (
                                           `id` INTEGER NOT NULL AUTO_INCREMENT,
                                           `username` VARCHAR(255) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    `field` VARCHAR(255) NOT NULL,
    `gender` INTEGER NOT NULL,
    `role` INTEGER NOT NULL COMMENT '1是admin',
    `createTime` VARCHAR(255) NOT NULL,
    `updateTime` VARCHAR(255) NOT NULL,
    PRIMARY KEY (`id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `post` (
                                      `id` INTEGER NOT NULL AUTO_INCREMENT,
                                      `title` VARCHAR(255) NOT NULL,
    `content` VARCHAR(255) NOT NULL,
    `askerId` INTEGER NOT NULL,
    `createTime` VARCHAR(255) NOT NULL,
    `updateTime` VARCHAR(255) NOT NULL,
    `views` INTEGER NOT NULL,
    `delTag` INTEGER NOT NULL DEFAULT 0 COMMENT '0是未删除，1是已删除',
    PRIMARY KEY (`id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `reply` (
                                       `id` INTEGER NOT NULL AUTO_INCREMENT,
                                       `content` VARCHAR(255) NOT NULL,
    `likes` INTEGER NOT NULL,
    `userId` INTEGER NOT NULL,
    `postId` INTEGER NOT NULL,
    `isAccepted` INTEGER NOT NULL DEFAULT 0 COMMENT '0是未采纳，1是已采纳',
    `createTime` VARCHAR(255) NOT NULL,
    `updateTime` VARCHAR(255) NOT NULL,
    `delTag` INTEGER NOT NULL DEFAULT 0 COMMENT '0是未删除，1是已删除',
    PRIMARY KEY (`id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `like` (
                                      `id` INTEGER NOT NULL AUTO_INCREMENT,
                                      `replyId` INTEGER NOT NULL,
                                      `userId` INTEGER NOT NULL,
                                      PRIMARY KEY (`id`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;