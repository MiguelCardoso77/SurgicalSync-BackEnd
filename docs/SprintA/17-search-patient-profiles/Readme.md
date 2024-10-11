# Search Patient Profiles

## 1. Use Case Description

As an Admin, I want to search patient profiles by different attributes, so that I can view the details, edit, and remove patient profiles

## 2. Customer Specifications and Clarifications

The client has outlined that the Admin Role should possess the functionality to search patient profiles.

## 3. Diagrams

### Level 1

- Logical View

![logical-view.svg](../../../Wiki/SprintA/diagrams/views/level1/logical-view/logical-view.svg)
###
- Process View

![process-view.svg](./level1/process-view.svg)

### Level 2

- Logical View

![logical-view.svg](../../../Wiki/SprintA/diagrams/views/level2/logical-view/logical-view.png)
###
- Process View

![process-view.svg](./level2/process-view.svg)

### Level 3

#### Logical Views

-   [MDR Logical View](../general-purpose/level3/mdr-logical-view.svg)
-   [UI Logical View](../general-purpose/level3/ui-logical-view.svg)

#### Implementation Views

-   [MDR Implementation View](./level3/mdr-implementation-view.svg)
-   [UI Implementation View](../general-purpose/level3/ui-implementation-view.svg)

#### Process Views

- BackEnd Process View

![backend-process-view.svg](./level3/backend-process-view.svg)

###
- Class Diagram View

![class-diagram-view.svg](./class-diagram.svg)

## 4. Acceptance Criteria and Tests

To successfully complete this user story, the following criteria must be met:

- Admins can search patient profiles by various attributes, including name, email, date of birth,
  or medical record number.
- The system displays search results in a search view with key patient information (name, email, date
  of birth).
- Admins can select a profile from the search to view, edit, or delete the patient record.
- The search results are paginated, and filters are available to refine the search results.

## 5. Dependencies

This user story relies on the following API functionalities:

-   To search patient profile
    ```
    GET /Patients/id
    ```

## 6. Definition of Ready (DoR)

### 6.1 Clear and Detailed Description

The user story is deemed ready when the requirements are clearly outlined, providing a comprehensive
understanding of the functionality to be implemented. Specifically, the Administrator role is expected to
possess the ability to search patient profiles.

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