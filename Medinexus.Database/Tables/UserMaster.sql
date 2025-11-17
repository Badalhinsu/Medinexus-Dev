----------  Table Information ------------------
-- Date            Author          Reason
------------------------------------------------------
-- 17/11/2025      Badalpatel     UserMaster data

----------------------------------------------------


CREATE TABLE UserMaster (
    Id SERIAL PRIMARY KEY,
    firstName VARCHAR(100),
    lastName VARCHAR(100),
    mobileNo VARCHAR(15) UNIQUE,
    email VARCHAR(150) UNIQUE,
    userName VARCHAR(100) UNIQUE,
    password VARCHAR(255),
    createdAt TIMESTAMP WITH TIME ZONE,
    modifiedAt TIMESTAMP WITH TIME ZONE ,
	refereshtoken VARCHAR(255),
	refereshtokenexpiry TIMESTAMP WITH TIME ZONE,
	chemistid int,
	CONSTRAINT fk_chemist
        FOREIGN KEY(chemistid)
        REFERENCES "chemistmaster"(id)
);