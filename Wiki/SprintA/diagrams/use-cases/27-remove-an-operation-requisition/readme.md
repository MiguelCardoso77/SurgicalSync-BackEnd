# UC27 - Remove an operation requisition

-----------------------------------------------------------------------

## Contents

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

1. [Use Case Description](#1-use-case-description)
2. [Customer Specifications and Clarifications](#2-customer-specifications-and-clarifications)
3. [Diagrams](#3-diagrams)
    - [Level 1](#level-1)
        - [Process View](#process-view)
    - [Level 2](#level-2)
        - [Process View](#process-view-1)
    - [Level 3](#level-3)
        - [Process View](#process-view-2)
            - [Backend Process View](#backend-process-view)
            - [Class Diagram](#class-diagram)
4. [Acceptance Criteria and Tests](#4-acceptance-criteria-and-tests)
5. [Dependencies](#5-dependencies)
6. [Definition of Ready (DoR)](#6-definition-of-ready-dor)
    - [6.1. Clear and Detailed Description](#61-clear-and-detailed-description)
    - [6.2. Acceptance Criteria](#62-acceptance-criteria)
    - [6.3. Dependencies and Resources](#63-dependencies-and-resources)
    - [6.4. Estimation and Sizing](#64-estimation-and-sizing)
7. [Definition of Done (DoD)](#7-definition-of-done-dod)
    - [7.1. Code Quality](#71-code-quality)
    - [7.2. Testing](#72-testing)
    - [7.3. Documentation](#73-documentation)

</div>

-----------------------------------------------------------------------

## 1. Use Case Description

-------------------------------------------------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

As a Doctor, I want to remove an operation requisition, so that the healthcare
activities are provided as necessary.

</div>

## 2. Customer Specifications and Clarifications

--------------------------------------------------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

- **Question**: Should actions like removing an operation type be accessed only through specific methods?
- **Answer**: Yes, operations like removal or deactivation should be available via specific API methods.


</div>

## 3. Diagrams

---------------------------------------------------------------------------

### Level 1

#### Process View

![process-view.svg](level1%2Fprocess-view.svg)

### Level 2

#### Process View

![process-view.svg](level2%2Fprocess-view.svg)

### Level 3

#### Process View

##### Backend Process View

![backend-process-view.svg](level3%2Fbackend-process-view.svg)

##### Class Diagram

![class-diagram.svg](class-diagram.svg)

## 4. Acceptance Criteria and Tests

---------------------------------------------------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

To successfully implement this use case, the following criteria must be met:

- Doctors can delete operation requests they created if the operation has not yet been
  scheduled.
- A confirmation prompt is displayed before deletion.
- Once deleted, the operation request is removed from the patient’s medical record and cannot
  be recovered.
- The system notifies the Planning Module and updates any schedules that were relying on this
  request.

</div>

## 5. Dependencies

-----------------------------------------------------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This use case relies on:

[1-register-backoffice-users](..%2F..%2F..%2F..%2F..%2Fdocs%2FSprintA%2F1-register-backoffice-users)

[12-create-a-new-staff-profile](..%2F..%2F..%2F..%2F..%2Fdocs%2FSprintA%2F12-create-a-new-staff-profile)

[13-create-patient-profile](..%2F..%2F..%2F..%2F..%2Fdocs%2FSprintA%2F13-create-patient-profile)

[30-request-an-operation](..%2F30-request-an-operation)

[37-add-new-operation-types](..%2F..%2F..%2F..%2F..%2Fdocs%2FSprintA%2F37-add-new-operation-types)

</div>

## 6. Definition of Ready (DoR)

------------------------------------------------------------------------------

### 6.1. Clear and Detailed Description

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The use case is deemed ready when the requirements are clearly outlined, providing a comprehensive
understanding of the functionality to be implemented. Specifically, the user with the Doctor role is expected to
possess the capability to remove an operation requisition.

</div>

### 6.2. Acceptance Criteria

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The use case is considered ready when the acceptance criteria referenced in section 4 are clearly
defined. These criteria serve as the benchmark for determining the successful completion of the use case.

</div>

### 6.3. Dependencies and Resources

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The use case is considered ready when all dependencies and resources required for its implementation
are identified. This includes the API functionalities necessary for the operation´s requisition removal.

</div>

### 6.4. Estimation and Sizing

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This use case is estimated to necessitate an allocation of approximately 4 to 12 hours (M Size) for completion.
This estimate is based on the complexity of the use case and the anticipated effort required for its implementation.

</div>

## 7. Definition of Done (DoD)

---------------------------------------------------------------------------------

### 7.1. Code Quality

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The use case is considered complete when the code quality meets the established standards. This
includes adherence to the coding conventions, the implementation of best practices, and the inclusion of
appropriate comments to enhance code readability and maintainability.

</div>

### 7.2. Testing

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The use case is considered concluded when the implemented features are tested thoroughly. This
encompasses unit tests, integration tests, and end-to-end tests to ensure the functionality operates as
expected. Additionally, the use case is considered complete when the implemented features are
validated against the acceptance criteria outlined in section 4.

</div>

### 7.3. Documentation

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The use case is considered finalized when the documentation is updated to reflect the changes
introduced by the implementation. This includes updating the relevant diagrams, README files, and any
other documentation to ensure it accurately represents the current state of the system.

</div>