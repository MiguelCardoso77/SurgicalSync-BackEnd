# Create a new staff profile

## 1. Use Case Description

As an Admin, I want to create a new staff profile, so that I can add them to the hospital’s roster.

## 2. Customer Specifications and Clarifications

Represents the professionals providing healthcare.

The client has outlined that the Admin Role should possess the functionality to create new staff profiles.

All staff require the following data:
• Attributes:
- Name
- License Number (unique identifier)
- Specialization` (e.g., cardiology, orthopedics). Each medical professional has only one specialty
- Email
- Phone number
- Availability Slots (the list of time slots the staff defines as being available for appointments)

• Rules:
- A staff must be unique in terms of `License Number`, `Email` and `Phone number`.
- Staff define the availability slots, e.g. slot 1: 2024-09-25:14h00-18h00; slot2: 2024-09-25:19h00/2024-09-26:02h00.
- The availability slots remain unchanged when slots are used for an appointment.
- Staff can handle multiple appointments but cannot be double-booked at the same time.

## 3. Diagrams

### Level 1

###
- Process View

![process-view.svg](./level1/process-view.svg)

### Level 2

###
- Process View

![process-view.svg](./level2/process-view.svg)

### Level 3


#### Process Views

- BackEnd Process View

![backend-process-view.svg](./level3/backend-process-view.svg)

###
- Class Diagram View

![class-diagram-view.svg](./class-diagram.svg)

## 4. Acceptance Criteria and Tests

To successfully complete this user story, the following criteria must be met:

    - Admins can input staff details such as name, contact information, and specialization.
    - A unique staff ID (License Number) is generated upon profile creation.
    - The system ensures that the staff’s email and phone number are unique.
    - The profile is stored securely, and access is based on role-based permissions.

## 5. Dependencies

This user story relies on the following API functionalities:

-   To create a staff profile:
    ```
    POST /staff
    ```

## 6. Definition of Ready (DoR)

### 6.1 Clear and Detailed Description

The user story is deemed ready when the requirements are clearly outlined, providing a comprehensive
understanding of the functionality to be implemented. Specifically, the Administrator role is expected to
possess the capability to create a new staff profile, designating details such as name and email address.

### 6.2 Acceptance Criteria

The user story is considered ready when the acceptance criteria referenced in section 4 are clearly
defined. These criteria serve as the benchmark for determining the successful completion of the user story.

### 6.3 Dependencies and Resources

The user story is considered ready when all dependencies and resources required for its implementation
are identified. This includes the API functionalities necessary for the creation of staff profiles.

### 6.4 Estimation and Sizing

This user story is estimated to necessitate an allocation of approximately 7 to 8 hours for completion.
This estimate is based on the complexity of the user story and the anticipated effort required for its
implementation.

## 7. Definition of Done (DoD)

### 7.1 Code Quality

The user story is considered complete when the code quality meets the established standards. This
includes adherence to the coding conventions, the implementation of best practices, and the inclusion of
appropriate comments to enhance code readability and maintainability.

### 7.2 Testing

The user story is considered concluded when the implemented features are tested thoroughly. This
encompasses unit tests, integration tests, and end-to-end tests to ensure the functionality operates as
expected. Additionally, the user story is considered complete when the implemented features are
validated against the acceptance criteria outlined in section 4.

### 7.3 Documentation

The user story is considered finalized when the documentation is updated to reflect the changes
introduced by the implementation. This includes updating the relevant diagrams, README files, and any
other documentation to ensure it accurately represents the current state of the system.