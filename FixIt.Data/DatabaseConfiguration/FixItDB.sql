--SEQUENCES--

CREATE SEQUENCE employee_sequence
    INCREMENT BY 3
    START 9999;

CREATE SEQUENCE department_squence
    INCREMENT BY 2
    START 999;

CREATE SEQUENCE ticket_sequence
    INCREMENT BY 1
    START 100;

-- TABLES --


CREATE TABLE department(
                           DepartmentId INT NOT NULL DEFAULT NEXTVAL('department_squence') PRIMARY KEY,
                           Name VARCHAR(20) NOT NULL,
                           Description VARCHAR(100)
);

CREATE TABLE role(
                     PositionId SERIAL PRIMARY KEY,
                     Name VARCHAR(15),
                     Department INT NOT NULL,
                     Description VARCHAR(75),
                     CONSTRAINT fk_role FOREIGN KEY(Department)
                         REFERENCES department(DepartmentId)
);

CREATE TABLE employee(
                         EmployeeId INT NOT NULL DEFAULT NEXTVAL('employee_sequence') PRIMARY KEY,
                         Name VARCHAR(25) NOT NULL,
                         Lastname VARCHAR(30) NOT NULL,
                         Department INT NOT NULL,
                         Role INT NOT NULL,
                         CONSTRAINT fk_employee_department FOREIGN KEY(Department)
                             REFERENCES department(DepartmentId),
                         CONSTRAINT fk_employee_role FOREIGN KEY(Role)
                             REFERENCES role(PositionId)
);

CREATE TABLE urgencylevel (
                              UrgencyLevelId SERIAL PRIMARY KEY,
                              Name VARCHAR(50) NOT NULL UNIQUE,
                              Description TEXT,
                              Priority SMALLINT NOT NULL CHECK (Priority BETWEEN 1 AND 10),
                              ResolutionTime INTERVAL NOT NULL CHECK (ResolutionTime > '0 hours')
);

CREATE TABLE status (
                        StatusId SERIAL PRIMARY KEY,
                        Name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Ticket (
                        TicketId INT NOT NULL DEFAULT NEXTVAL('ticket_sequence') PRIMARY KEY,
                        Title VARCHAR(255) NOT NULL,
                        Description TEXT,
                        UrgencyId INT NOT NULL,
                        CreatedBy INT NOT NULL,
                        AssignedTo INT NULL,
                        StatusId INT NOT NULL DEFAULT 1,
                        CreatedAt TIMESTAMP DEFAULT NOW(),
                        ClosedAt TIMESTAMP NULL,
                        DueDate TIMESTAMP NULL,
                        FOREIGN KEY (UrgencyId) REFERENCES UrgencyLevel(UrgencyLevelId),
                        FOREIGN KEY (CreatedBy) REFERENCES Employee(EmployeeId),
                        FOREIGN KEY (AssignedTo) REFERENCES Employee(EmployeeId),
                        FOREIGN KEY (StatusId) REFERENCES Status(StatusId)
);

-- TRIGGERS AND FUNCTIONS --

CREATE FUNCTION set_due_date() RETURNS TRIGGER AS $$
BEGIN
SELECT CreatedAt + ResolutionTime
INTO NEW.DueDate
FROM UrgencyLevel
WHERE UrgencyLevel.Id = NEW.UrgencyId;

RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER before_insert_ticket
    BEFORE INSERT ON ticket
    FOR EACH ROW
    EXECUTE FUNCTION set_due_date();


