## Contents

----------------------------------------------

# Architecture Background

---------------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

Architecture Background provides information about the software architecture, by:

- describing the background and rationale for the software architecture;
- explaining the constraints and influences that led to the current architecture;
- describing the major architectural approaches that have been utilized in the architecture.

</div>

## Problem Background

--------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The sub-parts of this section explain the constraints that provided the significant influence over the architecture.

</div>

### System Overview

--------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This section describes the general function and purpose for the system or subsystem whose architecture is described in this SAD.

</div>

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The aim of this project is to develop a prototype system for chirurgic requests, appointment, and resource management.

</div>

### Context

--------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This section describes the goals and major contextual factors for the software architecture. The section includes a description of the role software architecture plays in the life cycle, the relationship to system engineering results and artifacts, and any other relevant factors.

</div>

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The system will enable hospitals and clinics to manage surgery
appointments, and patient records. It will also offer real-time 3D visualization of resource
availability within the facility and optimize scheduling and resource usage. Furthermore, the
project will address GDPR compliance, ensuring the system meets data protection and consent
management requirements.
Each module of the system must consider the legal aspects of the GDPR Regulation (EU)
2016/679 and guarantee that users can access the privacy policy and exercise all relevant rights
under this regulation.

</div>


### Driving Requirements

---------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This section lists the functional requirements, quality attributes and design constraints. It may point to a separate requirements document.

</div>

#### Functional Requirements

---------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

1. As an Admin, I want to register new backoffice users (e.g., doctors, nurses,
   technicians, admins) via an out-of-band process, so that they can access the
   backoffice system with appropriate permissions.
2. As a Backoffice User (Admin, Doctor, Nurse, Technician), I want to reset my
   password if I forget it, so that I can regain access to the system securely.
3. As a Patient, I want to register for the healthcare application, so that I can create
   a user profile and book appointments online.
4. As a Patient, I want to update my user profile, so that I can change my personal
   details and preferences.
5. As a Patient, I want to delete my account and all associated data, so that I can
   exercise my right to be forgotten as per GDPR.
6. As a (non-authenticated) Backoffice User, I want to log in to the system using my
   credentials, so that I can access the backoffice features according to my assigned
   role.
7. As a Patient, I want to log in to the healthcare system using my external IAM
   credentials, so that I can access my appointments, medical records, and other
   features securely.
8. As an Admin, I want to create a new patient profile, so that I can register their
   personal details and medical history.
9. As an Admin, I want to edit an existing patient profile, so that I can update their
   information when needed. 
10. As an Admin, I want to delete a patient profile, so that I can remove patients who
    are no longer under car.
11. As an Admin, I want to list/search patient profiles by different attributes, so that I
    can view the details, edit, and remove patient profiles.
12. As an Admin, I want to create a new staff profile, so that I can add them to the
    hospital’s roster.
13. As an Admin, I want to edit a staff’s profile, so that I can update their information.
14. As an Admin, I want to deactivate a staff profile, so that I can remove them from
    the hospital’s active roster without losing their historical data.
15. As an Admin, I want to list/search staff profiles, so that I can see the details,
    edit, and remove staff profiles.
16. As a Doctor, I want to request an operation, so that the Patient has access to the
    necessary healthcare.
17. As a Doctor, I want to update an operation requisition, so that the Patient has
    access to the necessary healthcare
18. As a Doctor, I want to remove an operation requisition, so that the healthcare
    activities are provided as necessary.
19. As a Doctor, I want to list/search operation requisitions, so that I see the details,
    edit, and remove operation requisitions
20. As an Admin, I want to add new types of operations, so that I can reflect the
    available medical procedures in the system.
21. As an Admin, I want to edit existing operation types, so that I can update or correct
    information about the procedure.
22. As an Admin, I want to remove obsolete or no longer performed operation types,
    so that the system stays current with hospital practices.
23. As an Admin, I want to list/search operation types, so that I can see the details,
    edit, and remove operation types.

</div>

![use-case-diagram.svg](views%2Flevel1%2Fscenarios%2Fuse-case-diagram.svg)

#### Quality Attributes

---------------------------------

##### Functionality

--------------------------------

##### Usability

--------------------------------

##### Reliability

--------------------------------

##### Performance

--------------------------------

##### Supportability

--------------------------------

##### Design Constraints

--------------------------------

##### Physical Constraints

--------------------------------

## Solution Background

---------------------------------

### Architectural Approaches

-----------------------------

### Analysis Results

-----------------------------

### Mapping Requirements to Architecture

------------------------------