# Adding New Patient Profiles

## 1. Use Case Description

As a (non-authenticated) Backoffice User, I want to log in to the system using my credentials, 
so that I can access the backoffice features according to my assigned role.

## 2. Customer Specifications and Clarifications

The client has outlined that the (non-authenticated) Backoffice User Role should possess the 
functionality to log in.

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

- Backoffice users log in using their username and password.
- Role-based access control ensures that users only have access to features appropriate to their
  role (e.g., doctors can manage appointments, admins can manage users and settings).
- After five failed login attempts, the user account is temporarily locked, and a notification is
  sent to the admin.
- Login sessions expire after a period of inactivity to ensure security.

## 5. Dependencies

This user story relies on the following API functionalities:

-   To log in system 
    ```
    POST /Authentication
    ```

## 6. Definition of Ready (DoR)

### 6.1 Clear and Detailed Description

The user story is deemed ready when the requirements are clearly outlined, providing a comprehensive
understanding of the functionality to be implemented. Specifically, the (non-authenticated) Backoffice 
User role is expected to possess the ability to log in system.

### 6.2 Acceptance Criteria

The user story is considered ready when the acceptance criteria referenced in section 4 are clearly
defined. These criteria serve as the benchmark for determining the successful completion of the user story.

### 6.3 Dependencies and Resources

The user story is considered ready when all dependencies and resources required for its implementation
are identified. This includes the API functionalities necessary for the authentication

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