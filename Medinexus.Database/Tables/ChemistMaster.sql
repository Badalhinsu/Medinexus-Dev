----------  Table Information ------------------
-- Date            Author          Reason
------------------------------------------------------
-- 17/11/2025      Badalpatel     ChemistMaster data

----------------------------------------------------


CREATE TABLE ChemistMaster (
    Id SERIAL PRIMARY KEY,
    companyName VARCHAR(200) NOT NULL,
    AddressLine1 VARCHAR(250),
    AddressLine2 VARCHAR(250),
    City VARCHAR(100),
    Pincode VARCHAR(10),
    mobileNo VARCHAR(20),
    Gst_No VARCHAR(50),
    Gst_IssueDate TIMESTAMP with time zone,
    Tin_No VARCHAR(50),
    Tin_IssueDate TIMESTAMP with time zone,
    Cst_No VARCHAR(50),
    Cst_IssueDate TIMESTAMP with time zone,
    DrugLicenceNo1 VARCHAR(50),
    DrugLicenceNo2 VARCHAR(50),
    CreatedAt TIMESTAMP with time zone,
    ModifiedAt TIMESTAMP with time zone
);