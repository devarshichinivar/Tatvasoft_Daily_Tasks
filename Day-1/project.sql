CREATE TABLE Country (
    Id SERIAL PRIMARY KEY,
    CountryName VARCHAR(255) NOT NULL
);

CREATE TABLE City (
    Id SERIAL PRIMARY KEY,
    CountryId INT NOT NULL,
    CityName VARCHAR(255) NOT NULL,
    CONSTRAINT fk_CountryId FOREIGN KEY (CountryId) REFERENCES Country(Id)
);

CREATE TABLE MissionSkill (
    Id SERIAL PRIMARY KEY,
    SkillName VARCHAR(255) NOT NULL,
    Status VARCHAR(50) NOT NULL
);

CREATE TABLE MissionTheme (
    Id SERIAL PRIMARY KEY,
    ThemeName VARCHAR(255) NOT NULL,
    Status VARCHAR(50) NOT NULL
);

CREATE TABLE Missions (
    Id SERIAL PRIMARY KEY,
    MissionTitle VARCHAR(255) NOT NULL,
    MissionDescription TEXT NOT NULL,
    MissionOrganisationName VARCHAR(255) NOT NULL,
    MissionOrganisationDetail TEXT,
    CountryId INT NOT NULL,
    CityId INT NOT NULL,
    StartDate DATE,
    EndDate DATE,
    MissionType VARCHAR(50),
    TotalSheets INT,
    RegistrationDeadLine DATE,
    MissionThemeId INT NOT NULL,
    MissionSkillId INT NOT NULL,
    MissionImages TEXT,
    MissionDocuments TEXT,
    MissionAvilability VARCHAR(50),
    MissionVideoUrl VARCHAR(255),
    CONSTRAINT fk_CountryId FOREIGN KEY (CountryId) REFERENCES Country(Id),
    CONSTRAINT fk_CityId FOREIGN KEY (CityId) REFERENCES City(Id),
    CONSTRAINT fk_MissionThemeId FOREIGN KEY (MissionThemeId) REFERENCES MissionTheme(Id),
    CONSTRAINT fk_MissionSkillId FOREIGN KEY (MissionSkillId) REFERENCES MissionSkill(Id)
);

CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(50),
    EmailAddress VARCHAR(255) NOT NULL,
    UserType VARCHAR(50),
    Password VARCHAR(255) NOT NULL
);

CREATE TABLE UserDetail (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL,
    Name VARCHAR(255),
    Surname VARCHAR(255),
    EmployeeId VARCHAR(50),
    Manager VARCHAR(255),
    Title VARCHAR(255),
    Department VARCHAR(255),
    MyProfile TEXT,
    WhyIVolunteer TEXT,
    CountryId INT,
    CityId INT,
    Avilability VARCHAR(255),
    LinkdInUrl VARCHAR(255),
    MySkills TEXT,
    UserImage TEXT,
    Status BOOLEAN,
    CONSTRAINT fk_UserId FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT fk_CountryId FOREIGN KEY (CountryId) REFERENCES Country(Id),
    CONSTRAINT fk_CityId FOREIGN KEY (CityId) REFERENCES City(Id)
);

CREATE TABLE MissionApplication (
    Id SERIAL PRIMARY KEY,
    MissionId INT NOT NULL,
    UserId INT NOT NULL,
    AppliedDate TIMESTAMP NOT NULL,
    Status BOOLEAN NOT NULL,
    Sheet INT,
    CONSTRAINT fk_MissionId FOREIGN KEY (MissionId) REFERENCES Missions(Id),
    CONSTRAINT fk_UserId FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE UserSkills (
    Id SERIAL PRIMARY KEY,
    Skill VARCHAR(255) NOT NULL,
    UserId INT NOT NULL,
    CONSTRAINT fk_UserId FOREIGN KEY (UserId) REFERENCES Users(Id)
);