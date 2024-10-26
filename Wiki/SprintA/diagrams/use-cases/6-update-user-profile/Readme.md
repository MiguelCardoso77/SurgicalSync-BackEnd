# Updating User Profile

## 1. Use Case Description

As a Patient, I want to update my user profile, so that 
I can change my personal details and preferences.

## 2. Customer Specifications and Clarifications

The client has outlined that the Patient Role should possess the functionality to update his user profile

## 3. Diagrams

### Level 1

- Logical View

![logical-view.svg](/Wiki/SprintA/diagrams/views/level1/logical-view/logical-view.png)
###
- Process View

![process-view.svg](./level1/process-view.svg)

### Level 2

- Logical View

![logical-view.svg](/Wiki/SprintA/diagrams/views/level2/logical-view/logical-view.png)
###
- Process View

![process-view.svg](./level2/process-view.svg)

### Level 3

#### Process Views

- BackEnd Process View

![backend-process-view.svg](./level3/backend-process-view.svg)

###
- Class Diagram View

![class-diagram-view.svg](./class-diagram-view.svg)

## 4. Acceptance Criteria and Tests

To successfully complete this user story, the following criteria must be met:

- Patients can log in and update their profile details (e.g., name, contact information,
  preferences).
- Changes to sensitive data, such as email, trigger an additional verification step (e.g.,
  confirmation email).
- All profile updates are securely stored in the system.
- The system logs all changes made to the patient's profile for audit purposes.

## 5. Dependencies

This user story relies on the following API functionalities:

-   To update user profile
    ```
    PUT /users/id
    ```

## 6. Definition of Ready (DoR)

### 6.1 Clear and Detailed Description

The user story is deemed ready when the requirements are clearly outlined, providing a comprehensive
understanding of the functionality to be implemented. Specifically, the Patient role is expected to
possess the ability to update user profile.

### 6.2 Acceptance Criteria

The user story is considered ready when the acceptance criteria referenced in section 4 are clearly
defined. These criteria serve as the benchmark for determining the successful completion of the user story.

### 6.3 Dependencies and Resources

The user story is considered ready when all dependencies and resources required for its implementation
are identified. This includes the API functionalities necessary for the creation of patient profiles.

### 6.4 Estimation and Sizing

This user story is estimated to require an allocation of approximately 4 to 7 hours for completion.
This estimate is based on the complexity of the user story and the expected effort required for its
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