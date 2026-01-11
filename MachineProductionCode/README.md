Production Monitoring System
Overview

The Production Monitoring System is a C# console-based application designed to monitor manufacturing operations. It tracks production runs, machine performance, downtime incidents, and calculates Overall Equipment Effectiveness (OEE) using standard industrial formulas.

The project is implemented using C# (.NET Framework 4.7.2), ADO.NET, and SQL Server, following a layered architecture to ensure separation of concerns, maintainability, and correctness.

Objectives

Track production lines and machines

Record production runs with downtime incidents

Calculate OEE metrics accurately

Enforce data integrity using database constraints

Provide a menu-driven console interface for interaction

Technologies Used

C# (.NET Framework 4.7.2)

ADO.NET (SqlConnection, SqlCommand, SqlDataReader)

SQL Server

Console Application

Transaction Management

Repository and Service Patterns

System Architecture

The application follows a layered design:

UI Layer
Handles console input/output and user navigation.

Service Layer
Contains business logic such as OEE calculations.

Repository Layer
Handles all database interactions using ADO.NET.

Model Layer
Represents domain entities mapped to database tables.

Database Layer
SQL Server with normalized tables and foreign key constraints.

Database Design
Tables

ProductionLine

ProductionLineId (PK)

LineName

Location

IsActive

Machine

MachineId (PK)

MachineName

ProductionLineId (FK)

TheoreticalMaxOutput

Status

ProductionRun

RunId (PK)

MachineId (FK)

StartTime

EndTime

PlannedDuration

ActualUnitsProduced

DefectiveUnits

DowntimeMinutes

RunStatus

DowntimeIncident

IncidentId (PK)

RunId (FK)

IncidentType

StartTime

EndTime

Description

ResolvedBy

Foreign key constraints enforce referential integrity between tables.

OEE Calculation
Formulas Used

Availability Efficiency (AE)
AE = (Operating Time / Planned Production Time) × 100


Performance Efficiency (PE)
PE = (Actual Output / Theoretical Maximum Output) × 100


Quality Rate
Quality = (Good Units / Total Units Produced) × 100


Overall Equipment Effectiveness (OEE)
OEE = AE × PE × Quality / 10000


All calculations are implemented in the service layer (OeeService) and are independent of database logic.
Application Flow
User interacts with the console menu
UI layer collects input

Repository layer performs database operations
Service layer performs OEE calculations
Results are displayed in the console


Conclusion

This project demonstrates a production-grade backend design using C# and SQL Server. It applies clean architecture principles, correct database usage, and accurate business logic implementation suitable for real-world manufacturing monitoring scenarios.

Author
Navneeth Ch