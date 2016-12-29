CREATE TABLE Song (
    Id       INTEGER       PRIMARY KEY AUTOINCREMENT
                           NOT NULL,
    Name     NVARCHAR (50) NOT NULL,
    Author   NVARCHAR (50) NOT NULL,
    Language NVARCHAR (20),
    Country  NVARCHAR (30) 
);


CREATE TABLE Radiostation (
    Id   INTEGER       PRIMARY KEY AUTOINCREMENT
                       NOT NULL,
    Name NVARCHAR (20) NOT NULL
                       UNIQUE ON CONFLICT ROLLBACK
);


CREATE TABLE PlayList (
    Id           INTEGER        PRIMARY KEY AUTOINCREMENT
                                NOT NULL,
    station_id   INTEGER        NOT NULL,
    song_id      INTEGER        NOT NULL,
    duration     INTEGER        NOT NULL,
    DateTimeSong DATETIME       NOT NULL,
    Site         NVARCHAR (200),
    CONSTRAINT FK_PlayList_0_0 FOREIGN KEY (
        station_id
    )
    REFERENCES Radiostation (Id) MATCH NONE
                                 ON UPDATE CASCADE
                                 ON DELETE CASCADE,
    CONSTRAINT FK_playSong FOREIGN KEY (
        song_id
    )
    REFERENCES Song (Id) MATCH NONE
                                 ON UPDATE CASCADE
                                 ON DELETE CASCADE
);

