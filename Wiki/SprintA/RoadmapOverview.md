## Contents

-----------------------------------------

- [Documentation Roadmap and Overview](#Documentation-Roadmap-and-Overview)
  - [Purpose and Scope of the SAD](#Purpose-and-Scope-of-the-SAD)
  - [How the SAD is Organized](#How-the-SAD-Is-Organized)
  - [How a View Is Documented](#How-a-View-Is-Documented)


# Documentation Roadmap and Overview

-------------------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

Sub-parts of this section provide information that will help readers or users of the Software Architecture Document (SAD) quickly find information that will enable them to do their jobs. Readers of the SAD seeking an overview should begin here, as should readers interested in finding particular information to answer a specific question.

</div>


## Purpose and Scope of the SAD

------------------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This section explains the SAD's overall purpose and scope, the criteria for deciding which design decisions are architectural (and therefore documented in the SAD), and which design decisions are non-architectural (and therefore documented elsewhere).

</div>

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The software architecture of a system is the structure or structures of that system, which includes software elements and their externally visible properties, and the relationships between them (Bass 2012).

This Software Architecture Document (SAD) describes the software architecture of the system to be developed, in which hospitals and clinics aim to efficiently manage surgery appointments, staff, and resources. The system will allow for the scheduling and optimization of surgeries, provide real-time 3D visualization of hospital resources, and ensure compliance with GDPR regulations, all to improve operational efficiency and patient care.

This SAD is developed in an academic teaching-learning context (in the 5th semester of LEI in the 2024-2025 academic year), in which several skills are being acquired throughout the semester by students, at the same time as they develop the system.

Although students are the main recipients of the SAD, the skills to be acquired by students in the various UCs of the semester allow them to play different roles (different stakeholders/recipients), e.g. (requirements) elicitors, analysts, software architects, programmers/testers, administrators and operators (ops) and users.

</div>


## How the SAD Is Organized

------------------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This section provides a narrative description of the seven major sections of the SAD and the overall contents of each. Readers seeking specific information can use this section to help them locate it more quickly. This SAD is organized into the following seven sections:

This Documentation Roadmap and Overview provides information about this document and its intended audience. It provides the roadmap and document overview.
Architecture Background provides information about the software architecture. It describes the background and rationale for the software architecture. It explains the constraints and influences that led to the current architecture, and it describes the major architectural approaches that have been utilized in the architecture.
Views and
Mapping Between Views; both specify the software architecture.
Referenced Materials, provides look-up information for documents that are cited elsewhere in this SAD.
Glossary and Acronyms is an index of architectural elements and relations giving their definition, and where each is used in this SAD.

</div>

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

This SAD adopts the structure proposed above.

</div>

##  How a View Is Documented

------------------------------------------

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

1. Primary Presentation

<div style="border-left: 4px solid #5b88bd; padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

1. Is usually graphical
2. Should include a key that explains the notation
3. Shows elements and relations among them
4. Shows the information you want to convey about the view first
5. Should identify elements that are external to scope of the view (if external entities are not clearly marked in the diagram, consider adding a context diagram)

</div>

2. Element Catalog
- Explains elements depicted in primary presentation and their properties
- Is usually a table with element name and textual description
- May contain interface documentation
- May contain behavior documentation
3. Variability Guide
- Points where system can be parameterized or reconfigured. Examples:
   - Number of instances in a pool
   - Support for plug-ins or add-ons
   - Support for different versions of OS, database server or runtime environment
- Maybe the view is a reference architecture
   - Provide guidelines to instantiate it
4. Other Information
- Description and rationale for important design decisions (including relevant rejected alternatives)
- Results of analysis, prototypes and experiments
- Context diagram
5. Parent View
- If the current view is the refinement of another view, indicate which one

</div>

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

In this SAD, UML notation will be adopted, namely (but not exclusively) diagrams of components, sequences, packages and nodes. This guarantees 1.1, 1.2 and 1.3.

The organization of views by combining the C4 model (different levels of abstraction/granularity) and the 4+1 view model (several architectural points of view) makes it possible to immediately address requirement 1.4.

By adopting the C4 model, requirement 1.5 is addressed.

</div>

