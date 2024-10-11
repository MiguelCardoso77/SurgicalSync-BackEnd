## Contents

-----------------------------------------------------------------------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

- [Views](#views)
  - [Introduction](#introduction)
  - [Level 1](#level-1)
    - [Logical View](#logical-view)
  - [Level 2](#level-2)
    - [Logical View](#logical-view-1)
    - [Implementation View](#implementation-view)
  - [Level 3 (Backend)](#level-3-backend)
    - [Logical View](#logical-view-2) 
  - [Level 4](#level-4)
    - [Logical View](#logical-view-3)

</div>


# Views

----------------------------------------------------------------------------------------------

## Introduction

------------------------------------------------------

<div style="padding-left: 10px; margin-bottom: 20px; font-size: 15px;">

The combination of two architectural representation models will be adopted: C4 and 4+1.

The 4+1 View Model [Krutchen-1995] proposes the description of the system through complementary views, thus allowing the separate analysis of the requirements of the various software stakeholders, such as users, system administrators, project managers, architects and programmers. Views are thus defined as follows:

Logical view: relating to aspects of the software aimed at responding to business challenges;
Process view: relating to the flow of processes or interactions in the system;
Development view: relating to the organization of the software in its development environment;
Physical view: relating to the mapping of the various software components to hardware, i.e. where the software runs;
Scenario view: relating to the association of business processes with actors capable of triggering them.
The C4 Model [Brown-2020][C4-2020] defends the description of software through four levels of abstraction: system, container, component and code. Each level adopts a finer granularity than the level that precedes it, thus giving access to more details of a smaller part of the system. These levels can be equated to maps, e.g. the system view corresponds to the globe, the container view corresponds to the map of each continent, the component view to the map of each country and the code view to the map of roads and neighborhoods of each city. Different levels allow you to tell different stories to different audiences.

The levels are defined as follows: - Level 1: Description (framing) of the system as a whole; - Level 2: Description of system containers; - Level 3: Description of container components; - Level 4: Description of the code or smaller parts of the components (and as such, will not be covered in this DAS/SAD).

These two models can be said to expand along distinct axes, with the C4 Model presenting the system with different levels of detail and the 4+1 View Model presenting the system from different perspectives. By combining the two models it becomes possible to represent the system from different perspectives, each with various levels of detail.

To visually model/represent both what was implemented and the ideas and alternatives considered, the Unified Modeling Language (UML) [UML-2020] [UMLDiagrams-2020] is used.

</div>

## Level 1

--------------------------------------------------------------------

### Logical View

![logical-view.svg](diagrams%2Fviews%2Flevel1%2Flogical-view%2Flogical-view.svg)

---------------------------------------------------------------------

## Level 2

---------------------------------------------------------------------

### Logical View

![logical-view.png](diagrams%2Fviews%2Flevel2%2Flogical-view%2Flogical-view.png)

----------------------------------------------------------------------

### Implementation View

![implementation-diagram.svg](diagrams%2Fviews%2Flevel2%2Fimplementation-view%2Fimplementation-diagram.svg)

-----------------------------------------------------------------------

## Level 3 (Backend)

------------------------------------------------------------------------

### Logical View

---------------------------------------------------------------------------------

![logical-view.png](diagrams%2Fviews%2Flevel3%2Fbackend%2Flogical-view%2Flogical-view.png)

## Level 4

-----------------------------------------------------------------------------------------

#### Logical View

