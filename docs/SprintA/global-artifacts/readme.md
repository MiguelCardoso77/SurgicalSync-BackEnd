# Sprint A

## 1. Use Cases

------------------------------------

### 1.1. Use Case Diagram

![use-case-diagram.svg](use-case-diagram.svg)

### 1.2. Use Cases

| **Use_Cases** | **Description** |
|:-------------:|:---------------:|
|               |                 |
|               |                 |

## 2. Domain Model

---------------------------------

The Domain Model represents the essential concepts, entities, aggregates, and their relationships within the project's domain.

### 2.1. Introduction

The domain model serves as a conceptual blueprint of the project's domain. It helps to understand the key entities, their attributes, and relationships, facilitating effective communication and development.

### 2.2. Project Structure

The domain model is structured using UML notation. It consists of entities, aggregates, value objects, and associations between them.

### 2.3. Entities and Aggregates

#### Aggregates

An Aggregate is a cluster of entities and value objects that are treated as a single unit of consistency. Each aggregate has a root entity that is the only entry point for interacting with the state of the aggregate.

- Patient Aggregate : represents a patient and includes details like their name, contact information, medical conditions, and appointment history.
- Operation Request Aggregate : Represents a request for a medical operation for a patient, made by a doctor, with details like deadline and priority.
- Operation Type Aggregate : Contains predefined types of operations or procedures, along with required staff and estimated duration.
- User Aggregate : Represents users in the system (admin, staff, or patient) and includes attributes such as username, role, and email.
- Staff Aggregate : Represents healthcare staff (e.g., doctors, nurses), including their license number, specialization, and availability slots.
- Appointment Aggregate : Manages scheduled operations or consultations, linking patients, staff, rooms, and time slots.
- Surgery Room Aggregate : Represents a surgery room, containing information about the room number, type, capacity, assigned equipment, and maintenance slots.

#### Entities

An Entity is an object in the system that has a unique identity, distinguishing it from other objects. Its identity remains consistent throughout its lifecycle, even if its attributes or properties change. Entities are crucial in modeling objects that need to be tracked and modified over time.

- Patient - Represents a patient receiving medical care, with attributes such as name, contact details, and medical conditions.
  Attributes: FirstName, LastName, FullName, DateOfBirth, MedicalRecordNumber, PhoneNumber, MedicalConditions, EmergencyContact, AppointmentHistory
- OperationRequest - Represents a request for an operation made by a doctor for a patient.
  Attributes: OperationRequestID, DeadlineDate, Priority
- OperationType - Defines types of medical operations or procedures, along with the staff required and estimated duration.
  Attributes: OperationTypeID, Name, RequiredStaff, EstimatedDuration
- User - Represents a system user, such as an admin, healthcare staff, or patient.
  Attributes: Username, Role, Email
- Staff - Represents healthcare professionals such as doctors or nurses, with attributes related to their profession and availability.
  Attributes: FirstName, LastName, FullName, LicenseNumber, Specialization, PhoneNumber, AvailabilitySlots
- Appointment - Represents a scheduled operation or consultation, with a defined time and status.
  Attributes: AppointmentID, Date, Time, Status
- SurgeryRoom - Represents a surgery or consultation room with details about its capacity and equipment.
  Attributes: RoomNumber, Type, Capacity, AssignedEquipment, CurrentStatus, MaintenanceSlots



### 2.5. Domain Model

![domain-model.svg](domain-model.svg)

## 3. Glossary

----------------------------------

[Glossary](glossary.md)


## 4. Supplementary Specification

----------------------------------

[FURPS+](supplementary-specifications.md)