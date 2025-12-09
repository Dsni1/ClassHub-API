CREATE DATABASE IF NOT EXISTS classhub
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

USE classhub;

-- USERS
CREATE TABLE Users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_name VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL
);

-- ROLES
CREATE TABLE Roles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

-- ORGANISATIONS
CREATE TABLE Organisations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

-- USERROLES
CREATE TABLE UserRoles (
    role_id INT NOT NULL,
    user_id INT NOT NULL,
    organisation_id INT NOT NULL,
    PRIMARY KEY (role_id, user_id, organisation_id),
    FOREIGN KEY (role_id) REFERENCES Roles(id),
    FOREIGN KEY (user_id) REFERENCES Users(id),
    FOREIGN KEY (organisation_id) REFERENCES Organisations(id)
);

-- GROUPS
CREATE TABLE Groups (
    id INT AUTO_INCREMENT PRIMARY KEY,
    organisation_id INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    FOREIGN KEY (organisation_id) REFERENCES Organisations(id)
);

-- GROUPUSERS
CREATE TABLE GroupUsers (
    group_id INT NOT NULL,
    user_id INT NOT NULL,
    PRIMARY KEY (group_id, user_id),
    FOREIGN KEY (group_id) REFERENCES Groups(id),
    FOREIGN KEY (user_id) REFERENCES Users(id)
);

-- CHATROOMS
CREATE TABLE ChatRooms (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255),
    type ENUM('private', 'group') NOT NULL,
    created_at DATETIME NOT NULL,
    group_id INT NULL,
    FOREIGN KEY (group_id) REFERENCES Groups(id)
);

-- CHATROOMUSERS
CREATE TABLE ChatRoomUsers (
    chatroom_id INT NOT NULL,
    user_id INT NOT NULL,
    PRIMARY KEY (chatroom_id, user_id),
    FOREIGN KEY (chatroom_id) REFERENCES ChatRooms(id),
    FOREIGN KEY (user_id) REFERENCES Users(id)
);

-- MESSAGES
CREATE TABLE Messages (
    id INT AUTO_INCREMENT PRIMARY KEY,
    chatroom_id INT NOT NULL,
    user_id INT NOT NULL,
    text VARCHAR(255) NOT NULL,
    created_at DATETIME NOT NULL,
    FOREIGN KEY (chatroom_id) REFERENCES ChatRooms(id),
    FOREIGN KEY (user_id) REFERENCES Users(id)
);
