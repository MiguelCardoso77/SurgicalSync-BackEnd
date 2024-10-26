# Register As Patient

## 1. Use Case Description

As a Patient, I want to register for the healthcare application.

## 2. Customer Specifications and Clarifications

The client has outlined that the Patient Role should possess the functionality to register for the healthcare application.

- Patients can self-register using the external IAM system.
- The system validates the email address by sending a verification email with a confirmation link.

The patient will receive an email with a link to create the account via OAuth2.

## 3. Diagrams

### Level 1

- Logical View

![logical-view.svg](../../views/level1/logical-view/logical-view.png)
###
- Process View

![process-view.svg](./level1/process-view.svg)

### Level 2

- Logical View

![logical-view.svg](../../views/level2/logical-view/logical-view.png)
###
- Process View

![process-view.svg](./level2/process-view.svg)

### Level 3

#### Logical Views

- BackEnd Logical View
![backend-logical-view.svg](../../views/level3/backend/logical-view/logical-view.png)

#### Process Views

- BackEnd Process View

![backend-process-view.svg](./level3/backend-process-view.svg)
###
- FrontEnd Process View

![frontend-process-view.svg](./level3/frontend-process-view.svg)
###
- Class Diagram View

![class-diagram-view.svg](./class-diagram.svg)

## 4. Acceptance Criteria and Tests

To successfully complete this user story, the following criteria must be met:

- Patients can self-register using the external IAM system.
- During registration, patients provide personal details (e.g., name, email, phone) and create a
  profile.
- The system validates the email address by sending a verification email with a confirmation link.
- Patients cannot list their appointments without completing the registration process.

## 5. Dependencies

This user story relies on the following API functionalities:

-   To update an operation type:
    ```
    PUT /authentication
    ```

## 6. Definition of Ready (DoR)

### 6.1 Clear and Detailed Description

The user story is deemed ready when the requirements are clearly outlined, providing a comprehensive
understanding of the functionality to be implemented. Specifically, the Administrator role is expected to
possess the capability to register new backoffice users, designating their respective roles and
details such as their username and email address.

### 6.2 Acceptance Criteria

The user story is considered ready when the acceptance criteria referenced in section 4 are clearly
defined. These criteria serve as the benchmark for determining the successful completion of the user story.

### 6.3 Dependencies and Resources

The user story is considered ready when all dependencies and resources required for its implementation
are identified. This includes the API functionalities necessary for the creation of backoffice users.

### 6.4 Estimation and Sizing

This user story is estimated to necessitate an allocation of approximately 12 to 18 hours for completion.
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