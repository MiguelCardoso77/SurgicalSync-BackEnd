## Contents

----------------------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

- [Architecture Background](#architecture-background)
  - [Problem Background](#problem-background)
    - [System Overview](#system-overview)
    - [Context](#context)
    - [Driving Requirements](#driving-requirements)
      - [Functional Requirements](#functional-requirements)
      - [Quality Attributes](#quality-attributes)
        - [Functionality](#functionality)
        - [Usability](#usability)
        - [Reliability](#reliability)
        - [Performance](#performance)
        - [Supportability](#supportability)
        - [Design Constraints](#design-constraints)
        - [Implementation Constraints](#implementation-constraints)
        - [Interface Constraints](#interface-constraints)
        - [Physical Constraints](#physical-constraints)
  - [Solution Background](#solution-background)
    - [Architectural Approaches](#architectural-approaches)
    - [Analysis Results](#analysis-results)
    - [Mapping Requirements to Architecture](#mapping-requirements-to-architecture)
</div>


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

![use-case-diagram.svg](diagrams%2Fviews%2Flevel1%2Fscenarios-view%2Fuse-case-diagram.svg)

#### Quality Attributes

---------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

Quality attributes are categorized and systematized according to the FURPS+ model.

</div>

##### FUNCTIONALITY

--------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

1. The system must allow surgical appointment management, including the creation, updating, and deletion of surgery requests, as well as the allocation of doctors, nurses, and rooms.
2. The planning module should optimize appointments based on the availability of resources and patient priorities.
3. The system must provide real-time 3D visualization of room and equipment availability
4. Implementation of role-based user management, allowing administrators, doctors, patients, and other users to access appropriate features.
5. GDPR compliance should be enforced, allowing users to exercise their privacy rights and control over personal data.


</div>

##### USABILITY

--------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

6. The system’s interface should be intuitive, with simple navigation and accessible to all users, including healthcare staff and patients.
7. Clear instructions and error messages should be provided for each user action.
8. The 3D visualization should be user-friendly, with intuitive controls to check room occupancy and status.

</div>

##### RELIABILITY

--------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

9. The system should guarantee high availability and reliability, with failover mechanisms and a business continuity plan to handle hardware/software failures.
10. Critical data such as surgery appointments must be securely stored and recoverable in case of system crashes.
11. Data integrity must be ensured, preventing duplication or loss of important information such as patient profiles or medical history.

</div>

##### PERFORMANCE

--------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

12. The system must handle multiple concurrent requests, particularly during the scheduling of surgeries and 3D visualizations, without performance degradation.
13. Backoffice pages and the 3D visualization module should load quickly, with response times under 2 seconds under normal conditions.
14. The scheduling optimization algorithm should run efficiently, even when managing large datasets of doctors, patients, and rooms.

</div>

##### SUPPORTABILITY

--------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

15. The system’s code should be modular and well-documented to facilitate future maintenance and potential expansions.
16. Support for easy integration of new modules or features, such as advanced reporting or additional types of surgical procedures.
17. Detailed logs should be maintained to assist in error diagnostics and performance monitoring.

</div>

##### DESIGN CONSTRAINTS

--------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

18. The system must consist of a web application of the Single Page Application (SPA) type that allows authorized users to access the different modules of the application, as well as a set of services that implement the business rules components necessary for the operation of the web application.
19. All applications must have a layered organization separating the presentation components (human-computer interface) from the processing and data access components using industry best practices.

</div>

![system-general-view.png](Diagrams%2Fsystem-general-view.png)

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

Main functionalities of each module:

20. Backoffice - Manages information related to the hospital’s core data, including medical professionals (doctors, nurses, technicians), patients, types of surgeries, rooms, and chirurgic requests.
21. Planning/Optimization - Is feed by the information on the Backoffice module, and will generate the schedule of the surgeries and optimize those schedules according to different criteria and the medical professionals  and rooms availability. 2
22. 3D - Will render the hospital floor
24. Business Continuity Plan (BCP) - failover and business continuity aspects for the system must also be taken into account
25. GDPR - The GDPR aspects must be considered in all user stories/requirements.

</div>

###### IMPLEMENTATION CONSTRAINTS

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

26. All modules, including Backoffice, Planning and 3D must be developed as part of the same Single Page Application (SPA).
27. The system should be packaged and deployed as a single artifact to ensure uniform deployment and accessibility.

</div>

###### INTERFACE CONSTRAINTS

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

28. The SPA must provide access to all system modules: Backoffice, Planning, 3D, and GDPR functionalities, ensuring users can easily navigate between them.
29. The Planning module must consume data related to hospital resources (e.g., rooms, medical staff) through the Backoffice API.
30. The Planning module must also consume data regarding scheduled surgeries through the Backoffice API to optimize scheduling. 
31. The 3D module must consume hospital layout and resource availability data (such as room details) via the Backoffice API. 
32. The 3D module must consume data related to scheduled surgeries through the Backoffice API to accurately reflect real-time surgery schedules. 
33. The 3D module must also consume staff service data (such as shifts and assignments) from the Planning module API, ensuring real-time updates in the visual interface.

</div>

###### PHYSICAL CONSTRAINTS

TBD

## Solution Background

---------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The sub-parts of this section provide a description of why the architecture is the way that it is, and a convincing argument that the architecture is the right one to satisfy the behavioral and quality attribute goals levied upon it.

</div>

### Architectural Approaches

-----------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This section provides a rationale for the major design decisions embodied by the software architecture. It describes any design approaches applied to the software architecture, including the use of architectural styles or design patterns, when the scope of those approaches transcends any single architectural view. The section also provides a rationale for the selection of those approaches. It also describes any significant alternatives that were seriously considered and why they were ultimately rejected. The section describes any relevant COTS issues, including any associated trade studies.

</div>

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

Based on non-functional requirements and design constraints, the following approaches/patterns/styles will be imposed:

- Client-Server Architecture - the system will adopt a client-server architecture where each of the modules (e.g., Backoffice, Planning and 3D) will function as servers to provide data and services to various clients (such as the UI/Frontend).
- Web Application (Single Page Application - SPA) - The frontend will be implemented as a Single Page Application (SPA), providing a unified user experience across all modules, including Backoffice, Planning and 3D.
- Service-Oriented Architecture (SOA) - The system will follow a Service-Oriented Architecture (SOA) where each server (e.g., Backoffice, Planning) exposes its functionality through RESTful APIs. This promotes loose coupling between modules and allows them to interact via defined interfaces.
- N-Tier Architecture - An N-Tier architecture will be adopted to deploy different components across multiple layers, both on-premises.
- Layered Architecture (Onion Architecture) - The system will utilize a Layered Architecture, specifically following the Onion Architecture pattern.

</div>

### Analysis Results

-----------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This section describes the results of any quantitative or qualitative analyses that have been performed that provide evidence that the software architecture is fit for purpose. If an Architecture Tradeoff Analysis Method evaluation has been performed, it is included in the analysis sections of its final report. This section refers to the results of any other relevant trade studies, quantitative modeling, or other analysis results.

</div>

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

There are no analysis or evaluation results for now. Qualitative studies on the styles/patterns adopted (namely Onion in Social Network Master Data, but also Dependency Injection in the UI), allow us to empirically advocate that the maintainability, evolutability and testability of the software are high, while at the same time allowing the desired functionalities to be achieved.

</div>


### Mapping Requirements to Architecture

------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This section describes the requirements (original or derived) addressed by the software architecture, with a short statement about where in the architecture each requirement is addressed.

</div>

TBD