create table archive
(
    LtimeStamp datetime     null,
    logID      int          not null
        primary key,
    LvName     varchar(50)  not null,
    catName    varchar(50)  not null,
    userOP     varchar(50)  not null,
    logMessage varchar(255) not null
);

create table logcategories
(
    catName    varchar(50)  not null
        primary key,
    catComment varchar(255) not null
);

create table loglevel
(
    LvName    varchar(50)  not null
        primary key,
    LvComment varchar(500) not null
);

create table log
(
    LtimeStamp datetime default current_timestamp() null,
    logID      int auto_increment
        primary key,
    LvName     varchar(50)                          not null,
    catName    varchar(50)                          not null,
    userOP     varchar(50)                          not null,
    logMessage varchar(255)                         not null,
    constraint LogCatName_fk
        foreign key (catName) references logcategories (catName),
    constraint LogLvName_fk
        foreign key (LvName) references loglevel (LvName)
);

create table persons
(
    PersonID  int          null,
    LastName  varchar(255) null,
    FirstName varchar(255) null,
    Address   varchar(255) null,
    City      varchar(255) null
);

create table users
(
    user_id int          not null
        primary key,
    b       varchar(200) null
);


