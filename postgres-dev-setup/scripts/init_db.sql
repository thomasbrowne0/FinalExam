-- Drop tables if they exist (in reverse order of creation to respect foreign keys)
DROP TABLE IF EXISTS EmployeeToShift CASCADE;
DROP TABLE IF EXISTS Shift CASCADE;
DROP TABLE IF EXISTS EmployeeToCompany CASCADE;
DROP TABLE IF EXISTS Company CASCADE;
DROP TABLE IF EXISTS Employee CASCADE;

-- Create Employee table
CREATE TABLE Employee (
    EmployeeID SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Birthday DATE,
    Address VARCHAR(255),
    Role VARCHAR(20) CHECK (Role IN ('employee', 'manager', 'CompanyAdmin'))
);

-- Create Company table
CREATE TABLE Company (
    CompanyID SERIAL PRIMARY KEY,
    CompanyName VARCHAR(100) NOT NULL,
    Address VARCHAR(255),
    CompanyAdmin INT,
    FOREIGN KEY (CompanyAdmin) REFERENCES Employee(EmployeeID)
);

-- Create EmployeeToCompany junction table
CREATE TABLE EmployeeToCompany (
    ID SERIAL PRIMARY KEY,
    EmployeeID INT NOT NULL,
    CompanyID INT NOT NULL,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID),
    UNIQUE (EmployeeID, CompanyID)  -- Prevent duplicate assignments
);

-- Create Shift table
CREATE TABLE Shift (
    ShiftID SERIAL PRIMARY KEY,
    EmployeeID INT,
    Description TEXT,
    StartTime TIMESTAMP NOT NULL,
    EndTime TIMESTAMP NOT NULL,
    Location VARCHAR(255),
    CreatedByEmployeeID INT NOT NULL,
    IsPublished BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (CreatedByEmployeeID) REFERENCES Employee(EmployeeID),
    CHECK (EndTime > StartTime)  -- Ensure end time is after start time
);

-- Create EmployeeToShift junction table
CREATE TABLE EmployeeToShift (
    ID SERIAL PRIMARY KEY,
    ShiftID INT NOT NULL,
    EmployeeID INT NOT NULL,
    FOREIGN KEY (ShiftID) REFERENCES Shift(ShiftID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    UNIQUE (ShiftID, EmployeeID)  -- Prevent duplicate assignments
);

-- Insert some sample data
-- Sample employees
INSERT INTO Employee (Name, Birthday, Address, Role) VALUES
('John Smith', '1985-05-15', '123 Main St', 'CompanyAdmin'),
('Jane Doe', '1990-10-23', '456 Oak Ave', 'manager'),
('Bob Johnson', '1988-03-12', '789 Pine Rd', 'employee'),
('Alice Williams', '1992-07-30', '101 Maple Dr', 'employee'),
('Charlie Brown', '1987-12-05', '202 Elm St', 'manager');

-- Sample companies
INSERT INTO Company (CompanyName, Address, CompanyAdmin) VALUES
('Tech Solutions Inc.', '1000 Innovation Way', 1),
('Global Services Ltd.', '500 Business Park', 2);

-- Sample employee-company relationships
INSERT INTO EmployeeToCompany (EmployeeID, CompanyID) VALUES
(1, 1), -- John at Tech Solutions
(2, 2), -- Jane at Global Services
(3, 1), -- Bob at Tech Solutions
(4, 2), -- Alice at Global Services
(5, 1); -- Charlie at Tech Solutions

-- Sample shifts
INSERT INTO Shift (EmployeeID, Description, StartTime, EndTime, Location, CreatedByEmployeeID, IsPublished) VALUES
(3, 'Morning Shift', '2025-05-01 08:00:00', '2025-05-01 16:00:00', 'Main Office', 1, TRUE),
(4, 'Evening Shift', '2025-05-01 16:00:00', '2025-05-02 00:00:00', 'Branch Office', 2, TRUE),
(NULL, 'Weekend Shift', '2025-05-03 09:00:00', '2025-05-03 17:00:00', 'Main Office', 1, FALSE);

-- Sample employee-shift assignments
INSERT INTO EmployeeToShift (ShiftID, EmployeeID) VALUES
(1, 3), -- Bob assigned to Morning Shift
(2, 4), -- Alice assigned to Evening Shift
(1, 5); -- Charlie also assigned to Morning Shift