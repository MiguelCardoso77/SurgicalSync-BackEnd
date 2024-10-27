# Backend Test Planning Module

----------------------------------------------------------

## 1 - Priority Tests

### **Objective :** 

To ensure the robustness and accuracy of the `Priority` enum operations in the `DDDNetCore.Domain.OperationRequests` namespace. This includes verifying valid priority levels, testing parsing capabilities, and ensuring proper error handling for invalid inputs.

### **Test Method :**

Unit testing using NUnit framework to validate the `Priority` enum’s behavior in various scenarios. The tests verify enum containment, string parsing, integer conversion, and exception handling when using invalid values.

### **Description**

|                  **Scenario**                   |                             **Test**                             |                                         **Expected Result**                                          |
|:-----------------------------------------------:|:----------------------------------------------------------------:|:----------------------------------------------------------------------------------------------------:|
|   Verify enum contains valid priority levels    | `WhenCheckingIfEnumContainsPriorityLevels_ThenShouldReturnTrue`  |   Enum should contain `ElectiveSurgery`, `UrgentSurgery`, and `EmergencySurgery` priority levels.    |
|        Check enum string representations        |        `WhenCallingEnumToString_ShouldReturnCorrectValue`        | Enum to string conversions should return "ElectiveSurgery", "UrgentSurgery", and "EmergencySurgery". |
|   Parse valid priority strings to enum values   |      `WhenParsingValidString_ShouldReturnCorrectEnumValue`       |         Valid strings should parse correctly to their corresponding `Priority` enum values.          |
|        Handle invalid formatted strings         | `WhenParsingInvalidFormattedString_ShouldThrowArgumentException` |                         Invalid strings should throw an `ArgumentException`.                         |
|      Handle non-existent priority strings       |     `WhenParsingInvalidString_ShouldThrowArgumentException`      |                      Non-existent strings should throw an `ArgumentException`.                       |
|     Validate invalid integer values in enum     |         `WhenCheckingInvalidEnumValue_ShouldReturnFalse`         |  Invalid integer values (e.g., 999, -1, 3) should not be recognized as part of the `Priority` enum.  |
|         Convert enum values to integers         |     `WhenConvertingEnumToInteger_ShouldReturnExpectedValues`     |                    `ElectiveSurgery`=0, `UrgentSurgery`=1, `EmergencySurgery`=2.                     |
|      Convert integers to valid enum values      |    `WhenConvertingIntegerToEnum_ShouldReturnCorrectEnumValue`    |                  Valid integers should map to corresponding `Priority` enum values.                  |
| Handle out-of-range integers to enum conversion |   `WhenConvertingOutOfRangeIntegerToEnum_ShouldThrowException`   |        Out-of-range integers should throw an exception when forced to be cast as `Priority`.         |

## 2 - Operation Request Tests

### **Objective :**

To verify the functionality and integrity of the `OperationRequest` class in the `DDDNetCore.Domain.OperationRequests` namespace. This includes testing the constructor, property values, status toggles, and methods for changing priority and deadline dates.

### **Test Method :**

Unit testing using NUnit and Moq to mock dependencies and verify that `OperationRequest` handles state changes, attribute updates, and validation properly.

### **Description**

|                    **Scenario**                     |                                                **Test**                                                 |                                            **Expected Result**                                            |
|:---------------------------------------------------:|:-------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------:|
|           Construct with valid parameters           |                           `Constructor_ValidParameters_ShouldCreateInstance`                            |                     `OperationRequest` instance is created with expected properties.                      |
|             Activate operation request              |                           `ActivateOperationRequest_ShouldSetIsActiveToTrue`                            |                                    `IsActive` should be set to `true`.                                    |
|            Deactivate operation request             |                          `DeactivateOperationRequest_ShouldSetIsActiveToFalse`                          |                                   `IsActive` should be set to `false`.                                    |
|     Default constructor sets `IsActive` to true     |                             `Constructor_ShouldSetIsActiveToTrueByDefault`                              |                           `IsActive` should default to `true` on instantiation.                           |
|              Validate property values               |                                    `Properties_ShouldBeCorrectlySet`                                    |                      Properties should match values passed in during instantiation.                       |
|    Private default constructor for DeadlineDate     |                 `Constructor_WithPrivateDefaultConstructor_ShouldSetDeadlineDateToNull`                 |                   DeadlineDate should be `null` if created with a private constructor.                    |
|                Update deadline date                 |                       `ChangeDeadlineDate_ValidNewDate_ShouldUpdateDeadlineDate`                        |                        DeadlineDate should be updated to the new specified value.                         |
|                Update priority level                |                         `ChangePriority_ValidNewPriority_ShouldUpdatePriority`                          |                          Priority should be updated to the new specified level.                           |

## 3 - Operation Request Id Tests

### **Objective :**

To validate the `OperationRequestId` class within `DDDNetCore.Domain.OperationRequests` for correct behavior, including equality comparison, hash code consistency, and exception handling.

### **Test Method :**

Unit testing using NUnit framework to ensure that `OperationRequestId` correctly instantiates, compares, and handles invalid input, and to verify that it produces consistent hash codes for identical IDs.

|                    **Scenario**                     |                                                  **Test**                                                   |                                               **Expected Result**                                               |
|:---------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------------:|
|          Instantiate with valid ID string           |                         `WhenInstantiatingWithValidString_ThenShouldCreateInstance`                         |                     `OperationRequestId` instance should be created with expected ID value.                     |
|            Compare equal and unequal IDs            |                           `WhenComparingIds_ThenEqualsShouldReturnExpectedResult`                           |                 `Equals` should return `true` for identical IDs and `false` for different IDs.                  |
|       Hash code consistency for identical IDs       |                         `WhenGettingHashCodeForSameId_ThenShouldReturnSameHashCode`                         |                             Hash codes should be the same for identical ID values.                              |
|       Hash code consistency for different IDs       |                   `WhenGettingHashCodeForDifferentIds_ThenShouldReturnDifferentHashCodes`                   |                                Hash codes should differ for different ID values.                                |
|             Instantiate with `null` ID              |                  `WhenInstantiatingWithInvalidString_ThenShouldThrowArgumentNullException`                  |               A `NullReferenceException` should be thrown if instantiated with a `null` ID value.               |
|              Compare to `null` object               |                          `WhenComparingWithNullObject_ThenEqualsShouldReturnFalse`                          |                             `Equals` should return `false` when compared to `null`.                             |
|          Compare to different object type           |                     `WhenComparingToObjectOfDifferentType_ThenEqualsShouldReturnFalse`                      |    `Equals` should return `false` when compared to an object of a different type, even with matching values.    |

## 4 - Deadline Date Tests

### **Objective :**

To verify the functionality of the `DeadlineDate` class in the `DDDNetCore.Domain.OperationRequests` namespace. This includes validation of date creation, equality comparison, hash code consistency, and formatted string conversion.

### **Test Method :**

Unit testing using NUnit to ensure that `DeadlineDate` properly validates dates, handles comparisons, produces consistent hash codes for identical dates, and returns a correctly formatted date string.

|                    **Scenario**                     |                                                    **Test**                                                     |                                              **Expected Result**                                               |
|:---------------------------------------------------:|:---------------------------------------------------------------------------------------------------------------:|:--------------------------------------------------------------------------------------------------------------:|
|    Instantiate with valid future or current date    |                           `WhenInstantiatingWithValidDates_ThenShouldCreateInstance`                            |                 `DeadlineDate` instance should be created successfully with the expected date.                 |
|             Instantiate with past date              |                      `WhenInstantiatingWithInvalidDates_ThenShouldThrowArgumentException`                       |          An `ArgumentException` should be thrown with message "Deadline date cannot be in the past."           |
|           Compare equal and unequal dates           |                            `WhenComparingDates_ThenEqualsShouldReturnExpectedResult`                            |               `Equals` should return `true` for matching dates and `false` for differing dates.                |
|              Compare to `null` object               |                            `WhenComparingWithNullObject_ThenEqualsShouldReturnFalse`                            |                            `Equals` should return `false` when compared to `null`.                             |
|      Hash code consistency for identical dates      |                         `WhenGettingHashCodeForSameDates_ThenShouldReturnSameHashCode`                          |                               Hash codes should be identical for the same date.                                |
|      Hash code consistency for different dates      |                    `WhenGettingHashCodeForDifferentDates_ThenShouldReturnDifferentHashCodes`                    |                                 Hash codes should differ for different dates.                                  |
|              Convert to string format               |                             `WhenConvertingToString_ThenShouldReturnFormattedDate`                              |                             Should return date in "yyyy-MM-dd" format as a string.                             |

## 5 - Operation Request Controller Tests

### **Objective :**

Verify the functionality of the `OperationRequestsController` in the `DDDNetCore.Controllers` namespace. The goal is to ensure that the controller correctly handles API calls for creating, updating, retrieving, and deleting `OperationRequest` instances, with appropriate validations and error handling.

### **Test Method :**

Utilize NUnit and Moq for unit testing to ensure that `OperationRequestsController` correctly handles valid and invalid inputs, verifies data consistency, responds properly to error conditions, and produces expected results.

|                    **Scenario**                     |                                                **Test**                                                |                                              **Expected Result**                                              |
|:---------------------------------------------------:|:------------------------------------------------------------------------------------------------------:|:-------------------------------------------------------------------------------------------------------------:|
|       Retrieve `OperationRequest` by valid ID       |                               `GetById_ValidId_ReturnsOperationRequest`                                |          Returns the corresponding `OperationRequestDto` instance for the ID, with `200 OK` status.           |
|      Retrieve `OperationRequest` by invalid ID      |                                  `GetById_InvalidId_ReturnsNotFound`                                   |                     Returns `404 NotFound` when the operation request ID does not exist.                      |
|         Create new valid `OperationRequest`         |                      `Create_ReturnsCreatedResponse_WhenOperationRequestIsValid`                       |      Returns `201 Created` with creation URL and the created object, confirming successful persistence.       |
|              Create with invalid data               |                              `Create_ReturnsBadRequest_WhenDataIsInvalid`                              |     Returns `400 BadRequest` with a detailed message for invalid data (e.g., deadline date in the past).      |
|    Update `OperationRequest` with mismatched ID     |                               `Update_ReturnsBadRequest_WhenIdMismatch`                                |             Returns `400 BadRequest` when the path ID does not match the ID in the request body.              |
|         Update existing `OperationRequest`          |                             `Update_ReturnsOk_WhenOperationRequestIsValid`                             |                      Returns `200 OK` upon successfully updating the `OperationRequest`.                      |
|       Update non-existent `OperationRequest`        |                         `Update_ReturnsNotFound_WhenOperationRequestNotFound`                          |                  Returns `404 NotFound` when trying to update a request that does not exist.                  |
|             Get all `OperationRequests`             |                         `GetAllOperationRequests_ReturnsAllOperationRequests`                          |       Returns a full list of `OperationRequestDto`, confirming that all operation requests are listed.        |
|         Delete existing `OperationRequest`          |                      `Delete_ReturnsOk_WhenOperationRequestIsSuccessfullyDeleted`                      |                  Returns `200 OK` upon successfully deleting an existing operation request.                   |
|       Delete non-existent `OperationRequest`        |                        `Delete_ReturnsNotFound_WhenOperationRequestIsNotFound`                         |                Returns `404 NotFound` if the operation request ID does not exist for deletion.                |

## 6 - Delete Patient Micro Service Tests

### **Objective :**

To validate the functionality of the `DeletePatientMicroService` class within the `DDDNetCore.Application.Services` namespace. The goal is to ensure the service correctly handles patient data deletion requests per GDPR requirements, including cases where the patient is not found, deletion is successful, or errors occur.

### **Test Method :**

Use NUnit and Moq to create unit tests that simulate deletion conditions, such as the presence or absence of the patient in the repository, operation failures, and proper logging for each case.

|                    **Scenario**                     |                                                  **Test**                                                  |                                               **Expected Result**                                               |
|:---------------------------------------------------:|:----------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------------:|
|   Attempt to delete data for non-existent patient   |                       `DeletePatientDataByGDPRAndAccount_NoPatientFound_ReturnsNull`                       |                         Returns `null` when the patient is not found in the repository.                         |
|        Successfully delete existing patient         |                `DeletePatientDataByGDPRAndAccount_PatientExists_ReturnsDeletedPatientData`                 |                    Returns deleted patient data after successfully completing the operation.                    |
|    Fail to delete patient due to database error     |                     `DeletePatientDataByGDPRAndAccount_DatabaseError_ThrowsException`                      |         Throws an exception and logs an error when the delete operation fails due to a database error.          |
|     Validate patient ID format during deletion      |           `DeletePatientDataByGDPRAndAccount_InvalidMedicalRecordNumber_ThrowsArgumentException`           |                 Throws an `ArgumentException` for incorrectly formatted medical record numbers.                 |
|      Confirm log entry for successful deletion      |               `DeletePatientDataByGDPRAndAccount_SuccessfulDeletion_LogsInformationMessage`                |                Logs an informational message upon successful completion of the delete operation.                |
|        Confirm log entry for failed deletion        |                    `DeletePatientDataByGDPRAndAccount_DeletionFailure_LogsErrorMessage`                    |                           Logs an error message upon failure of the delete operation.                           |

## 7 - Patient Name Micro Service Tests

### **Objective :**

To validate the functionality of the PatientNameMicroService class in the DDDNetCore.Application.Services namespace. This includes testing the retrieval of operation requests based on patient names, handling cases where the patient exists, does not exist, or has no operation requests.

### **Test Method :**

Unit testing using NUnit and Moq to ensure that the PatientNameMicroService handles different scenarios correctly when querying operation requests by patient name.

|                        **Scenario**                        |                                        **Test**                                         |                                   **Expected Result**                                   |
|:----------------------------------------------------------:|:---------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------:|
|      Retrieve operation requests when patient exists       |     GetAllOperationRequestsByPatientName_ReturnsOperationRequests_WhenPatientExists     |  Returns a list of operation requests for the existing patient with expected details.   |
|      Retrieve empty list when patient does not exist       |      GetAllOperationRequestsByPatientName_ReturnsEmptyList_WhenPatientDoesNotExist      |        Returns an empty list when the patient does not exist in the repository.         |
| Retrieve empty list when patient has no operation requests | GetAllOperationRequestsByPatientName_ReturnsEmptyList_WhenPatientHasNoOperationRequests | Returns an empty list when the patient exists but has no associated operation requests. |

## 8 - Operation Request Service Tests

### **Objective:**

To validate the functionality of the `OperationRequestService` class in handling various operations related to operation requests, including retrieving, adding, updating, inactivating, and filtering requests based on their status.

### **Test Method:**

Unit testing using NUnit and Moq to ensure that the `OperationRequestService` correctly processes operation requests under different scenarios.

### **Test Scenarios:**

|                        **Scenario**                        |                               **Test**                                |                                      **Expected Result**                                      |
|:----------------------------------------------------------:|:---------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------:|
|      Retrieve operation request by ID when it exists       |  GetByIdAsync_ReturnsOperationRequestDto_WhenOperationRequestExists   | Returns an `OperationRequestDto` with the correct details for the existing operation request. |
|  Retrieve operation request by ID when it does not exist   |       GetByIdAsync_ReturnsNull_WhenOperationRequestDoesNotExist       |                    Returns null when the operation request does not exist.                    |
|      Retrieve all operation requests when they exist       |    GetAllAsync_ReturnsListOfOperationRequestDto_WhenRequestsExist     |              Returns a list of `OperationRequestDto` with all existing requests.              |
|      Retrieve all operation requests when none exist       |           GetAllAsync_ReturnsEmptyList_WhenNoRequestsExist            |                    Returns an empty list when no operation requests exist.                    |
|        Add a new operation request with valid data         |      AddAsync_CreatesNewOperationRequest_WhenValidDtoIsProvided       |         Creates and returns a new `OperationRequestDto` when valid data is provided.          |
|    Update an existing operation request with valid data    |  UpdateAsync_UpdatesExistingOperationRequest_WhenValidDtoIsProvided   |                    Updates and returns the modified `OperationRequestDto`.                    |
|      Update an operation request that does not exist       |       UpdateAsync_ReturnsNull_WhenOperationRequestDoesNotExist        |           Returns null when attempting to update a non-existent operation request.            |
|       Inactivate an operation request when it exists       | InactivateAsync_ReturnsOperationRequestDto_WhenOperationRequestExists |              Returns the inactivated `OperationRequestDto` with correct details.              |
|    Inactivate an operation request that does not exist     |     InactivateAsync_ReturnsNull_WhenOperationRequestDoesNotExist      |             Returns null when the operation request to inactivate does not exist.             |
| Retrieve filtered operation requests based on their status |       GetAllByStatus_ReturnsFilteredRequests_WhenRequestsExist        | Returns the correct number of active/inactive operation requests based on the status filter.  |

## 9 - Operation Request Mapper Tests

### **Objective:**

To validate the functionality of the `OperationRequestMapper` class, ensuring it accurately maps between domain models (`OperationRequest`) and data transfer objects (`OperationRequestDto`).

### **Test Method:**

Unit testing using NUnit and Moq to verify that the `OperationRequestMapper` correctly transforms domain models into DTOs and vice versa.

### **Test Scenarios:**

|             **Scenario**              |                **Test**                |                                   **Expected Result**                                    |
|:-------------------------------------:|:--------------------------------------:|:----------------------------------------------------------------------------------------:|
| Mapping a valid domain model to a DTO |  ToDto_ValidDomain_ReturnsCorrectDto   | Returns an `OperationRequestDto` with the correct details derived from the domain model. |
| Mapping a valid DTO to a domain model | ToDomain_ValidDto_ReturnsCorrectDomain |      Returns an `OperationRequest` with the correct details populated from the DTO.      |

## 10 - Operation Request DTO Tests

### **Objective:**

To validate the functionality and integrity of the `OperationRequestDto` class, ensuring it correctly handles both complete and incomplete operation request data transfer objects.

### **Test Method:**

Unit testing using NUnit to verify that the `OperationRequestDto` behaves as expected when instantiated with various levels of completeness.

### **Test Scenarios:**

|                           **Scenario**                            |                **Test**                 |                           **Expected Result**                            |
|:-----------------------------------------------------------------:|:---------------------------------------:|:------------------------------------------------------------------------:|
| Creating an incomplete operation request DTO with missing fields  | TestCreateIncompleteOperationRequestDto | DTO should retain the specified values and have null for missing fields. |
| Creating a complete operation request DTO with all fields present |  TestCreateCompleteOperationRequestDto  |     DTO should contain the correct values for all specified fields.      |

## 11 - Operation Type Service Tests

### **Objective:**

To validate the functionality of the `OperationTypeService` class in handling various operations related to operation types, including retrieving, adding, updating, and filtering operation types.

### **Test Method:**

Unit testing using NUnit and Moq to ensure that the `OperationTypeService` correctly processes operation types under different scenarios.

### **Test Scenarios:**
|                          **Scenario**                           |                               **Test**                               |                                    **Expected Result**                                     |
|:---------------------------------------------------------------:|:--------------------------------------------------------------------:|:------------------------------------------------------------------------------------------:|
|          Retrieve operation type by ID when it exists           |     GetByIdAsync_ReturnsOperationTypeDto_WhenOperationTypeExists     |  Returns an `OperationTypeDto` with the correct details for the existing operation type.   |
|      Retrieve operation type by ID when it does not exist       |        GetByIdAsync_ReturnsNull_WhenOperationTypeDoesNotExist        |                    Returns null when the operation type does not exist.                    |
|          Retrieve all operation types when they exist           |       GetAllAsync_ReturnsListOfOperationTypeDto_WhenTypesExist       |          Returns a list of `OperationTypeDto` with all existing operation types.           |
|          Retrieve all operation types when none exist           |            GetAllAsync_ReturnsEmptyList_WhenNoTypesExist             |                    Returns an empty list when no operation types exist.                    |
|            Add a new operation type with valid data             |       AddAsync_CreatesNewOperationType_WhenValidDtoIsProvided        |         Creates and returns a new `OperationTypeDto` when valid data is provided.          |
|        Update an existing operation type with valid data        |   UpdateAsync_UpdatesExistingOperationType_WhenValidDtoIsProvided    |                    Updates and returns the modified `OperationTypeDto`.                    |
|          Update an operation type that does not exist           |        UpdateAsync_ReturnsNull_WhenOperationTypeDoesNotExist         |           Returns null when attempting to update a non-existent operation type.            |
|      Retrieve filtered operation types based on their name      |      GetAllByName_ReturnsFilteredTypes_WhenTypesExistWithFilter      |      Returns the correct number of operation types based on the name filter provided.      |
|     Retrieve filtered operation types based on their status     |     GetAllByStatus_ReturnsFilteredTypes_WhenTypesExistWithFilter     |     Returns the correct number of operation types based on the status filter provided.     |
| Retrieve filtered operation types based on their specialization | GetAllBySpecialization_ReturnsFilteredTypes_WhenTypesExistWithFilter | Returns the correct number of operation types based on the specialization filter provided. |

## 12 - Operation Type Mapper Tests

### **Objective:**

To validate the functionality of the `OperationTypeMapper` class, ensuring it accurately maps between domain models (`OperationType`) and data transfer objects (`OperationTypeDto`).

### **Test Method:**

Unit testing using NUnit and Moq to verify that the `OperationTypeMapper` correctly transforms domain models into DTOs and vice versa.

### **Test Scenarios:**
|              **Scenario**               |                 **Test**                  |                                      **Expected Result**                                      |
|:---------------------------------------:|:-----------------------------------------:|:---------------------------------------------------------------------------------------------:|
|  Mapping a valid domain model to a DTO  |    ToDto_ValidDomain_ReturnsCorrectDto    |     Returns an `OperationTypeDto` with the correct details derived from the domain model.     |
|  Mapping a valid DTO to a domain model  |  ToDomain_ValidDto_ReturnsCorrectDomain   |          Returns an `OperationType` with the correct details populated from the DTO.          |
| Mapping a list of domain models to DTOs | ToDtoList_ValidDomains_ReturnsCorrectDtos | Returns a list of `OperationTypeDto` with the correct details derived from the domain models. |

## 13 - Operation Type DTO Tests

### **Objective:**

To validate the functionality and integrity of the `OperationTypeDto` class, ensuring it correctly handles both complete and incomplete operation type data transfer objects.

### **Test Method:**

Unit testing using NUnit to verify that the `OperationTypeDto` behaves as expected when instantiated with various levels of completeness.

### **Test Scenarios:**

|                          **Scenario**                          |               **Test**               |                           **Expected Result**                            |
|:--------------------------------------------------------------:|:------------------------------------:|:------------------------------------------------------------------------:|
| Creating an incomplete operation type DTO with missing fields  | TestCreateIncompleteOperationTypeDto | DTO should retain the specified values and have null for missing fields. |
| Creating a complete operation type DTO with all fields present |  TestCreateCompleteOperationTypeDto  |     DTO should contain the correct values for all specified fields.      |

## 14 - Operation Type Controller Tests

### **Objective:**

To verify the functionality of the `OperationTypesController` in the `DDDNetCore.Controllers` namespace. The goal is to ensure that the controller correctly handles API calls for creating, updating, retrieving, and deleting `OperationType` instances, with appropriate validations and error handling.

### **Test Method:**    

Utilize NUnit and Moq for unit testing to ensure that `OperationTypesController` correctly handles valid and invalid inputs, verifies data consistency, responds properly to error conditions, and produces expected results.

### **Test Scenarios:**

|              **Scenario**              |                         **Test**                          |                                        **Expected Result**                                         |
|:--------------------------------------:|:---------------------------------------------------------:|:--------------------------------------------------------------------------------------------------:|
|  Retrieve `OperationType` by valid ID  |          `GetById_ValidId_ReturnsOperationType`           |      Returns the corresponding `OperationTypeDto` instance for the ID, with `200 OK` status.       |
| Retrieve `OperationType` by invalid ID |            `GetById_InvalidId_ReturnsNotFound`            |                 Returns `404 NotFound` when the operation type ID does not exist.                  |
|    Create new valid `OperationType`    | `Create_ReturnsCreatedResponse_WhenOperationTypeIsValid`  | Returns `201 Created` with creation URL and the created object, confirming successful persistence. |
|  Deleting an existing `OperationType`  | `Delete_ReturnsOk_WhenOperationTypeIsSuccessfullyDeleted` |              Returns `200 OK` upon successfully deleting an existing operation type.               |
|  Delete non-existent `OperationType`   |   `Delete_ReturnsNotFound_WhenOperationTypeIsNotFound`    |            Returns `404 NotFound` if the operation type ID does not exist for deletion.            |

## 15 - Operation Type Tests

### **Objective:**

To verify the functionality and integrity of the `OperationType` class in the `DDDNetCore.Domain.OperationTypes` namespace. The goal is to ensure that the class correctly represents operation types with their attributes and behaviors.

### **Test Method:**

Unit testing using NUnit to validate the behavior of the `OperationType` class in handling different scenarios related to operation types.

### **Test Scenarios:**

|                  **Scenario**                  |                        **Test**                         |                                     **Expected Result**                                      |
|:----------------------------------------------:|:-------------------------------------------------------:|:--------------------------------------------------------------------------------------------:|
| Construct with valid parameters and properties |   `Constructor_ValidParameters_ShouldCreateInstance`    |           `OperationType` instance is created with expected properties and values.           |
|  Validate property values and getter methods   |            `Properties_ShouldBeCorrectlySet`            | Properties should match values passed in during instantiation and be accessible via getters. |
|  Update operation type name with valid input   |         `UpdateName_ValidName_ShouldUpdateName`         |        Operation type name should be successfully updated to the new specified value.        |
| Update operation type name with invalid input  |  `UpdateName_InvalidName_ShouldThrowArgumentException`  |        Attempting to update with an invalid name should throw an `ArgumentException`.        |
|      Update operation type specialization      | `UpdateSpecialization_ValidSpecialization_ShouldUpdate` |   Operation type specialization should be successfully updated to the new specified value.   |
|          Update operation type status          |         `UpdateStatus_ValidStatus_ShouldUpdate`         |       Operation type status should be successfully updated to the new specified value.       |

## 16 - Operation Type Id Tests

### **Objective:**

To validate the `OperationTypeId` class within `DDDNetCore.Domain.OperationTypes` for correct behavior, including equality comparison, hash code consistency, and exception handling.

### **Test Method:**

Unit testing using NUnit framework to ensure that `OperationTypeId` correctly instantiates, compares, and handles invalid input, and to verify that it produces consistent hash codes for identical IDs.

### **Test Scenarios:**

|                   **Scenario**                    |                                 **Test**                                  |                                    **Expected Result**                                     |
|:-------------------------------------------------:|:-------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------:|
|   Instantiate with valid ID string and compare    |        `WhenInstantiatingWithValidString_ThenShouldCreateInstance`        | `OperationTypeId` instance should be created with the expected ID value and be comparable. |
|   Compare equal and unequal IDs for consistency   |          `WhenComparingIds_ThenEqualsShouldReturnExpectedResult`          | `Equals` should return `true` for identical IDs and `false` for different IDs as expected. |
|      Hash code consistency for identical IDs      |        `WhenGettingHashCodeForSameId_ThenShouldReturnSameHashCode`        |  Hash codes should be the same for identical ID values to ensure consistency in hashing.   |
|      Hash code consistency for different IDs      |  `WhenGettingHashCodeForDifferentIds_ThenShouldReturnDifferentHashCodes`  |     Hash codes should differ for different ID values to ensure uniqueness in hashing.      |
|    Instantiate with `null` ID and handle error    | `WhenInstantiatingWithInvalidString_ThenShouldThrowArgumentNullException` |    A `NullReferenceException` should be thrown if instantiated with a `null` ID value.     |
|     Compare to `null` object and handle error     |         `WhenComparingWithNullObject_ThenEqualsShouldReturnFalse`         |   `Equals` should return `false` when compared to `null` to avoid null reference issues.   |
| Compare to different object type and handle error |    `WhenComparingToObjectOfDifferentType_ThenEqualsShouldReturnFalse`     |       `Equals` should return `false` when compared to an object of a different type.       |

## 17 - Required Staff Tests

### **Objective:**

To validate the functionality and integrity of the `RequiredStaff` class in the `DDDNetCore.Domain.OperationTypes` namespace. The goal is to ensure that the class correctly represents the required staff for an operation type with their attributes and behaviors.

### **Test Method:**

Unit testing using NUnit to validate the behavior of the `RequiredStaff` class in handling different scenarios related to operation types.

### **Test Scenarios:**

|                  **Scenario**                  |                        **Test**                         |                                     **Expected Result**                                      |
|:----------------------------------------------:|:-------------------------------------------------------:|:--------------------------------------------------------------------------------------------:|
| Construct with valid parameters and properties |   `Constructor_ValidParameters_ShouldCreateInstance`    |           `RequiredStaff` instance is created with expected properties and values.           |
|  Validate property values and getter methods   |            `Properties_ShouldBeCorrectlySet`            | Properties should match values passed in during instantiation and be accessible via getters. |
|  Update required staff count with valid input  |       `UpdateCount_ValidCount_ShouldUpdateCount`        |       Required staff count should be successfully updated to the new specified value.        |
| Update required staff count with invalid input | `UpdateCount_InvalidCount_ShouldThrowArgumentException` |       Attempting to update with an invalid count should throw an `ArgumentException`.        |
|      Update required staff specialization      | `UpdateSpecialization_ValidSpecialization_ShouldUpdate` |   Required staff specialization should be successfully updated to the new specified value.   |
|          Update required staff status          |         `UpdateStatus_ValidStatus_ShouldUpdate`         |       Required staff status should be successfully updated to the new specified value.       |

## 18 - Estimated Duration Tests

### **Objective:**

To validate the functionality and integrity of the `EstimatedDuration` class in the `DDDNetCore.Domain.OperationTypes` namespace. The goal is to ensure that the class correctly represents the estimated duration for an operation type with its attributes and behaviors.

### **Test Method:**

Unit testing using NUnit to validate the behavior of the `EstimatedDuration` class in handling different scenarios related to operation types.

### **Test Scenarios:**

|                  **Scenario**                  |                           **Test**                            |                                     **Expected Result**                                      |
|:----------------------------------------------:|:-------------------------------------------------------------:|:--------------------------------------------------------------------------------------------:|
| Construct with valid parameters and properties |      `Constructor_ValidParameters_ShouldCreateInstance`       |         `EstimatedDuration` instance is created with expected properties and values.         |
|  Validate property values and getter methods   |               `Properties_ShouldBeCorrectlySet`               | Properties should match values passed in during instantiation and be accessible via getters. |
|   Update estimated duration with valid input   |          `UpdateDuration_ValidDuration_ShouldUpdate`          |        Estimated duration should be successfully updated to the new specified value.         |
|  Update estimated duration with invalid input  | `UpdateDuration_InvalidDuration_ShouldThrowArgumentException` |      Attempting to update with an invalid duration should throw an `ArgumentException`.      |

## 19 - Operation Name Tests

### **Objective:**

To validate the functionality and integrity of the `OperationName` class in the `DDDNetCore.Domain.OperationTypes` namespace. The goal is to ensure that the class correctly represents the name of an operation type with its attributes and behaviors.

### **Test Method:**

Unit testing using NUnit to validate the behavior of the `OperationName` class in handling different scenarios related to operation types.

### **Test Scenarios:**

|                  **Scenario**                  |                       **Test**                        |                                     **Expected Result**                                      |
|:----------------------------------------------:|:-----------------------------------------------------:|:--------------------------------------------------------------------------------------------:|
| Construct with valid parameters and properties |  `Constructor_ValidParameters_ShouldCreateInstance`   |           `OperationName` instance is created with expected properties and values.           |
|  Validate property values and getter methods   |           `Properties_ShouldBeCorrectlySet`           | Properties should match values passed in during instantiation and be accessible via getters. |
|     Update operation name with valid input     |        `UpdateName_ValidName_ShouldUpdateName`        |          Operation name should be successfully updated to the new specified value.           |
|    Update operation name with invalid input    | `UpdateName_InvalidName_ShouldThrowArgumentException` |        Attempting to update with an invalid name should throw an `ArgumentException`.        |

## 20 - Email Tests

### **Objective:**

To validate the functionality and integrity of the `Email` class in the `DDDNetCore.Domain.OperationTypes` namespace. The goal is to ensure that the class correctly represents an email address with its attributes and behaviors.

### **Test Method:**

Unit testing using NUnit to validate the behavior of the `Email` class in handling different scenarios related to email addresses.

### **Test Scenarios:**

|                  **Scenario**                  |                          **Test**                           |                                     **Expected Result**                                      |
|:----------------------------------------------:|:-----------------------------------------------------------:|:--------------------------------------------------------------------------------------------:|
| Construct with valid parameters and properties |     `Constructor_ValidParameters_ShouldCreateInstance`      |               `Email` instance is created with expected properties and values.               |
|  Validate property values and getter methods   |              `Properties_ShouldBeCorrectlySet`              | Properties should match values passed in during instantiation and be accessible via getters. |
|     Update email address with valid input      |          `UpdateAddress_ValidAddress_ShouldUpdate`          |           Email address should be successfully updated to the new specified value.           |
|    Update email address with invalid input     | `UpdateAddress_InvalidAddress_ShouldThrowArgumentException` |      Attempting to update with an invalid address should throw an `ArgumentException`.       |

## 21 - Email Service Tests

### **Objective:**

To validate the functionality of the `EmailService` class within the `DDDNetCore.Application.Services` namespace. The goal is to ensure the service correctly handles sending emails, including cases where the email is successfully sent, fails to send, or errors occur.

### **Test Method:**

Unit testing using NUnit and Moq to create unit tests that simulate sending emails under different conditions, such as successful delivery, delivery failure, and proper logging for each case.

### **Test Scenarios:**

|                 **Scenario**                 |                        **Test**                         |                                    **Expected Result**                                    |
|:--------------------------------------------:|:-------------------------------------------------------:|:-----------------------------------------------------------------------------------------:|
|   Send email successfully with valid data    |            `SendEmail_ValidData_ReturnsTrue`            |            Returns `true` when the email is successfully sent with valid data.            |
|    Fail to send email due to invalid data    |          `SendEmail_InvalidData_ReturnsFalse`           |        Returns `false` when the email fails to send due to invalid data or format.        |
| Fail to send email due to SMTP server error  |          `SendEmail_SMTPError_ThrowsException`          |   Throws an exception and logs an error when the email fails to send due to SMTP error.   |
| Fail to send email due to network connection |        `SendEmail_NetworkError_ThrowsException`         | Throws an exception and logs an error when the email fails to send due to network issues. |
| Validate email address format during sending | `SendEmail_InvalidEmailAddress_ThrowsArgumentException` |        Throws an `ArgumentException` for an invalid email address during sending.         |

## 22 - Authentication Service Tests

### **Objective:**

To validate the functionality of the `AuthenticationService` class within the `DDDNetCore.Application.Services` namespace. The goal is to ensure the service correctly handles user authentication, including cases where the user is successfully authenticated, fails to authenticate, or errors occur.

### **Test Method:**

Unit testing using NUnit and Moq to create unit tests that simulate user authentication under different conditions, such as successful authentication, authentication failure, and proper logging for each case.

### **Test Scenarios:**

|                     **Scenario**                      |                    **Test**                    |                                **Expected Result**                                 |
|:-----------------------------------------------------:|:----------------------------------------------:|:----------------------------------------------------------------------------------:|
| Authenticate user successfully with valid credentials |  `Authenticate_ValidCredentials_ReturnsTrue`   | Returns `true` when the user is successfully authenticated with valid credentials. |
| Fail to authenticate user due to invalid credentials  | `Authenticate_InvalidCredentials_ReturnsFalse` |  Returns `false` when the user fails to authenticate due to invalid credentials.   |

## 23 - Firebase Service Tests

### **Objective:**

To validate the functionality of the `FirebaseService` class within the `DDDNetCore.Application.Services` namespace. The goal is to ensure the service correctly handles Firebase operations, including cases where the service interacts with Firebase successfully, fails to interact, or errors occur.

### **Test Method:**

Unit testing using NUnit and Moq to create unit tests that simulate Firebase operations under different conditions, such as successful interaction, interaction failure, and proper logging for each case.

### **Test Scenarios:**

|                **Scenario**                 |                   **Test**                   |                                             **Expected Result**                                             |
|:-------------------------------------------:|:--------------------------------------------:|:-----------------------------------------------------------------------------------------------------------:|
|     Interact with Firebase successfully     |  `InteractWithFirebase_Success_ReturnsTrue`  |                    Returns `true` when the service interacts with Firebase successfully.                    |
|       Fail to interact with Firebase        | `InteractWithFirebase_Failure_ReturnsFalse`  |                      Returns `false` when the service fails to interact with Firebase.                      |
| Fail to interact with Firebase due to error | `InteractWithFirebase_Error_ThrowsException` | Throws an exception and logs an error when the service encounters an error while interacting with Firebase. |

## 24 - GoogleLoginDto Tests

### **Objective:**

To validate the functionality and integrity of the `GoogleLoginDto` class in the `DDD

### **Test Method:**

Unit testing using NUnit to verify that the `GoogleLoginDto` behaves as expected when instantiated with various levels of completeness.

### **Test Scenarios:**

|                         **Scenario**                         |              **Test**              |                           **Expected Result**                            |
|:------------------------------------------------------------:|:----------------------------------:|:------------------------------------------------------------------------:|
| Creating an incomplete Google login DTO with missing fields  | TestCreateIncompleteGoogleLoginDto | DTO should retain the specified values and have null for missing fields. |
| Creating a complete Google login DTO with all fields present |  TestCreateCompleteGoogleLoginDto  |     DTO should contain the correct values for all specified fields.      |


## 25 -  Staff Availability Slots Tests

## Objective

To ensure the correct behavior of the `StaffAvaiabilitySlots` class in the `DDDNetCore.Domain.Staffs` namespace, validating its construction, equality checks, string representation, and handling of private constructors.

## Test Framework

The tests are implemented using the NUnit framework, focusing on the creation of instances, equality comparisons, hash code generation, and private constructor access.

## Description

| **Scenario**                                                   | **Test Method**          | **Expected Result**                                                                |
|----------------------------------------------------------------|--------------------------|------------------------------------------------------------------------------------|
| Verify proper construction of Staff Availability Slots         | `TestConstructor`        | Object should be constructed with the correct value.                               |
| Verify string representation of Staff Availability Slots       | `TestToString`           | `ToString()` should return the correct value.                                      |
| Check equality between two instances with same value           | `TestEquals`             | Instances with the same availability slots value should be equal.                  |
| Check equal hash codes for instances with same value           | `TestEqualHashCodes`     | Instances with the same value should have the same hash code.                      |
| Check different hash codes for instances with different values | `TestDifferentHashCodes` | Instances with different values should have different hash codes.                  |
| Test private constructor creation                              | `TestPrivateConstructor` | Private constructor should create an instance, but without initializing the value. |

# 26 -  StaffId Tests

## Objective

To ensure the correct behavior of the `StaffId` class in the `DDDNetCore.Domain.Staffs` namespace, validating its construction, string representation, equality, and hash code generation.

## Test Framework

The tests are implemented using the NUnit framework, focusing on the creation of instances, string conversion, equality comparisons, and ensuring that different instances generate different hash codes.

## Description

| **Scenario**                                                   | **Test Method**          | **Expected Result**                                                         |
|----------------------------------------------------------------|--------------------------|-----------------------------------------------------------------------------|
| Verify proper construction of `StaffId`                        | `TestConstructor`        | Object should be constructed with the correct value.                        |
| Verify string representation of `StaffId`                      | `TestToString`           | `ToString()` should return the correct value.                               |
| Check equality between two instances with the same value       | `TestEquals`             | Instances with the same `StaffId` value should be equal.                    |
| Check different hash codes for instances with different values | `TestDifferentHashCodes` | Instances with different `StaffId` values should have different hash codes. |


# 27 - StaffLicenseNumber Tests

## Objective

To ensure the correct behavior of the `StaffLicenseNumber` class in the `DDDNetCore.Domain.Staffs` namespace, validating its construction, string representation, equality checks, and hash code generation, along with handling private constructor instantiation.

## Test Framework

The tests are implemented using the NUnit framework, focusing on object creation, string conversion, equality comparisons, hash code generation, and private constructor access.

## Description

| **Scenario**                                                   | **Test Method**          | **Expected Result**                                                                    |
|----------------------------------------------------------------|--------------------------|----------------------------------------------------------------------------------------|
| Verify proper construction of `StaffLicenseNumber`             | `TestConstructor`        | Object should be constructed with the correct value.                                   |
| Verify string representation of `StaffLicenseNumber`           | `TestToString`           | `ToString()` should return the correct value.                                          |
| Check equality between two instances with the same value       | `TestEquals`             | Instances with the same `StaffLicenseNumber` value should be equal.                    |
| Check equal hash codes for instances with the same value       | `TestEqualHashCodes`     | Instances with the same `StaffLicenseNumber` value should have the same hash code.     |
| Check different hash codes for instances with different values | `TestDifferentHashCodes` | Instances with different `StaffLicenseNumber` values should have different hash codes. |
| Test private constructor creation                              | `TestPrivateConstructor` | Private constructor should create an instance without initializing the value.          |

# 28 - StaffName Tests

## Objective

To ensure the correct behavior of the `StaffName` class in the `DDDNetCore.Domain.Staffs` namespace, validating its construction, string representation, equality checks, and hash code generation, along with testing private constructor instantiation.

## Test Framework

The tests are implemented using the NUnit framework, focusing on object creation, string conversion, equality comparisons, hash code generation, and private constructor access.

## Description

| **Scenario**                                                   | **Test Method**          | **Expected Result**                                                           |
|----------------------------------------------------------------|--------------------------|-------------------------------------------------------------------------------|
| Verify proper construction of `StaffName`                      | `TestConstructor`        | Object should be constructed with the correct value.                          |
| Verify string representation of `StaffName`                    | `TestToString`           | `ToString()` should return the correct value.                                 |
| Check equality between two instances with the same value       | `TestEquals`             | Instances with the same `StaffName` value should be equal.                    |
| Check equal hash codes for instances with the same value       | `TestEqualHashCodes`     | Instances with the same `StaffName` value should have the same hash code.     |
| Check different hash codes for instances with different values | `TestDifferentHashCodes` | Instances with different `StaffName` values should have different hash codes. |
| Test private constructor creation                              | `TestPrivateConstructor` | Private constructor should create an instance without initializing the value. |

# 29 - StaffPhoneNumber Tests

## Objective

To ensure the correct behavior of the `StaffPhoneNumber` class in the `DDDNetCore.Domain.Staffs` namespace, validating its construction, string representation, equality checks, and hash code generation, along with testing private constructor instantiation.

## Test Framework

The tests are implemented using the NUnit framework, focusing on object creation, string conversion, equality comparisons, hash code generation, and private constructor access.

## Description

| **Scenario**                                                   | **Test Method**          | **Expected Result**                                                                  |
|----------------------------------------------------------------|--------------------------|--------------------------------------------------------------------------------------|
| Verify proper construction of `StaffPhoneNumber`               | `TestConstructor`        | Object should be constructed with the correct value.                                 |
| Verify string representation of `StaffPhoneNumber`             | `TestToString`           | `ToString()` should return the correct value.                                        |
| Check equality between two instances with the same value       | `TestEquals`             | Instances with the same `StaffPhoneNumber` value should be equal.                    |
| Check equal hash codes for instances with the same value       | `TestEqualHashCodes`     | Instances with the same `StaffPhoneNumber` value should have the same hash code.     |
| Check different hash codes for instances with different values | `TestDifferentHashCodes` | Instances with different `StaffPhoneNumber` values should have different hash codes. |
| Test private constructor creation                              | `TestPrivateConstructor` | Private constructor should create an instance without initializing the value.        |

# 30-  StaffSpecialization Tests

### Objective

To ensure the correct behavior of the `StaffSpecialization` enum in the `DDDNetCore.Domain.Staffs` namespace, validating its construction, string representation, equality checks, and hash code generation.

### Test Framework

The tests are implemented using the NUnit framework, focusing on enum instantiation, string conversion, equality comparisons, hash code generation, and private constructor testing.

## Description

| **Scenario**                                                   | **Test Method**          | **Expected Result**                                                                     |
|----------------------------------------------------------------|--------------------------|-----------------------------------------------------------------------------------------|
| Verify proper construction of `StaffSpecialization`            | `TestConstructor`        | Enum should be constructed with the correct value.                                      |
| Verify string representation of `StaffSpecialization`          | `TestToString`           | `ToString()` should return the correct enum string representation.                      |
| Check equality between two instances with the same value       | `TestEquals`             | Instances with the same `StaffSpecialization` value should be equal.                    |
| Check equal hash codes for instances with the same value       | `TestEqualHashCodes`     | Instances with the same `StaffSpecialization` value should have the same hash code.     |
| Check different hash codes for instances with different values | `TestDifferentHashCodes` | Instances with different `StaffSpecialization` values should have different hash codes. |
| Test private constructor                                       | `TestPrivateConstructor` | Enum instance `None` should be initialized properly.                                    |

# 31 - Staff Tests

## Objective

To test the behavior of the `Staff` class in the `DDDNetCore.Domain.Staffs` namespace, validating its construction, property modification methods, and overall functionality, including private constructor access.

## Test Framework

These tests utilize the NUnit framework, in combination with Moq to mock dependencies, ensuring the `Staff` class behaves as expected during instantiation and when invoking methods for changing specialization, phone number, email, availability slots, and activation status.

## Test Descriptions

| **Scenario**                                     | **Test Method**                   | **Expected Result**                                                                                                                                                                |
|--------------------------------------------------|-----------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Validate proper construction of a `Staff` object | `TestConstructor`                 | The `Staff` object should be initialized with correct values for name, email, phone number, specialization, availability slots, staff type, activation status, and license number. |
| Change staff specialization                      | `TestChangeStaffSpecialization`   | Specialization should be changed to a new value and correctly reflected.                                                                                                           |
| Change staff phone number                        | `TestChangeStaffPhoneNumber`      | Phone number should be updated to a new value.                                                                                                                                     |
| Change user email                                | `TestChangeUserEmail`             | Email should be updated to a new value.                                                                                                                                            |
| Change availability slots                        | `TestChangeStaffAvaiabilitySlots` | Availability slots should be updated to a new list of slots.                                                                                                                       |
| Activate staff                                   | `TestActivateStaff`               | Staff activation status should be changed to `true`.                                                                                                                               |
| Deactivate staff                                 | `TestDeactivateStaff`             | Staff activation status should be changed to `false`.                                                                                                                              |
| Validate private constructor                     | `TestPrivateConstructor`          | A `Staff` object created using the private constructor should be non-null but have its properties unset (`null`).                                                                  |

# 32 - StaffType Unit Tests

## Objective

To test the behavior of the `StaffType` enum in the `DDDNetCore.Domain.Staffs` namespace, ensuring that the enum can be instantiated and that its values are accessible.

## Test Framework

These tests utilize the NUnit framework to validate the functionality of the `StaffType` enum.

## Test Descriptions

| **Scenario**                          | **Test Method**          | **Expected Result**                                           |
|---------------------------------------|--------------------------|---------------------------------------------------------------|
| Validate instantiation of `StaffType` | `TestPrivateConstructor` | The `StaffType` enum value should be accessible and not null. |

# 33 - StaffController Tests

## Objective

To test the behavior of the `StaffController` class in the `DDDNetCore.Controllers` namespace, ensuring that the controller methods correctly interact with the service and repository layers and return the expected results for various operations.

## Test Framework

These tests utilize the NUnit framework, alongside Moq to mock dependencies, allowing for isolated testing of controller actions such as retrieving, creating, updating, and deactivating staff profiles.

## Test Descriptions

| **Scenario**                                  | **Test Method**                                   | **Expected Result**                                         |
|-----------------------------------------------|---------------------------------------------------|-------------------------------------------------------------|
| Retrieve all staff profiles                   | `GetAll_ReturnsAllStaffProfiles`                  | Should return a list of staff profiles without null values. |
| Retrieve staff profile by valid ID            | `GetById_ValidId_ReturnsStaff`                    | Should return the staff profile matching the provided ID.   |
| Retrieve staff profile by invalid ID          | `GetById_InValidId_ReturnsNotFound`               | Should return a `NotFoundResult`.                           |
| Create a valid staff profile                  | `Create_ValidStaffProfile_ReturnsCreatedAtAction` | Should return `CreatedAtActionResult`.                      |
| Update a valid staff profile                  | `Update_ValidStaffProfile_ReturnsCreatedAtAction` | Should return an updated staff profile.                     |
| Update an invalid staff profile               | `Update_InValidStaffProfile_ReturnsBadRequest`    | Should return a `BadRequestResult`.                         |
| Deactivate a staff profile with a valid ID    | `Deactivate_ValidId_ReturnsOk`                    | Should return `OkObjectResult`.                             |
| Deactivate a staff profile with an invalid ID | `Deactivate_InvalidId_ReturnsNotFound`            | Should return a `NotFoundResult`.                           |

# 34 - StaffService Tests

## Objective

To test the behavior of the `StaffService` class in the `DDDNetCore.Services` namespace, ensuring that the service methods correctly manage staff profiles and return the expected results for various operations.

## Test Framework

These tests utilize the NUnit framework, alongside Moq to mock dependencies, allowing for isolated testing of service methods such as retrieving, creating, updating, and deactivating staff profiles.

## Test Descriptions

| **Scenario**                                | **Test Method**                                                               | **Expected Result**                                        |
|---------------------------------------------|-------------------------------------------------------------------------------|------------------------------------------------------------|
| Retrieve all staff profiles                 | `GetAllAsync_ReturnsListOfStaffDto_WhenRequestsExist`                         | Should return a list of DTOs of staff without null values. |
| Retrieve all staff profiles when none exist | `GetAllAsync_ReturnsEmptyList_WhenNoStaffProfileExist`                        | Should return an empty list.                               |
| Add a new valid staff profile               | `AddAsync_CreatesNewStaffProfile_WhenValidDtoIsProvided`                      | Should create and return the new staff profile.            |
| Update an existing staff profile            | `UpdateAsync_UpdatesExistingStaffProfile_WhenValidDtoIsProvided`              | Should return the updated staff profile.                   |
| Update a non-existent staff profile         | `UpdateAsync_ReturnsNull_WhenStaffProfileDoesNotExist`                        | Should return null.                                        |
| Retrieve staff profiles by name             | `GetAllByStaffNameAsync_ReturnsListOfStaffDtoList_WhenStaffProfileExist`      | Should return a filtered list of DTOs by staff name.       |
| Retrieve staff profiles by specialization   | `GetAllBySpecializationAsync_ReturnsListOfStaffDtoList_WhenStaffProfileExist` | Should return a filtered list of DTOs by specialization.   |
| Retrieve staff profiles by email            | `GetAllByEmailAsync_ReturnsListOfStaffDtoList_WhenStaffProfileExist`          | Should return a filtered list of DTOs by email.            |
| Retrieve a staff profile by valid ID        | `GetByIdAsync_ReturnsListOfStaffDto_WhenRequestsExist`                        | Should return the staff DTO matching the provided ID.      |
| Retrieve a staff profile by invalid ID      | `GetByIdAsync_ReturnsNull_WhenStaffProfileDoesNotExist`                       | Should return null.                                        |
| Generate a license number                   | `GenerateLN_ReturnsStaffId_WhenValidDtoIsProvided`                            | Should return a valid license number.                      |
| Deactivate a staff profile                  | `DeactivateAsync_ReturnsListOfStaffDto_WhenValidStaffIdIsProvided`            | Should return the deactivated staff profile.               |

# 35 - StaffMapper Tests

## Objective

To test the behavior of the `StaffMapper` class in the `DDDNetCore.Application.Mappers` namespace, ensuring that the mapping methods correctly convert between domain models and data transfer objects (DTOs) for staff profiles.

## Test Framework

These tests utilize the NUnit framework, alongside Moq to mock dependencies, allowing for isolated testing of mapping methods such as converting to and from domain models and DTOs.

## Test Descriptions

| **Scenario**                          | **Test Method** | **Expected Result**                                                       |
|---------------------------------------|-----------------|---------------------------------------------------------------------------|
| Convert DTO to Domain Model           | `TestToDomain`  | Should map properties from the DTO to the domain model correctly.         |
| Convert Domain Model to DTO           | `TestToDto`     | Should map properties from the domain model to the DTO correctly.         |
| Convert Domain Model to DTO List      | `TestToDtoList` | Should map properties correctly for a single staff domain model to a DTO. |
| Convert List of Domain Models to DTOs | `TestToListDto` | Should return a list of DTOs matching the number of domain models.        |

# 36 - StaffDto Tests

## Objective

To test the behavior of the `StaffDto` and `StaffDtoList` classes in the `DDDNetCore.Application.DTO` namespace, ensuring that the DTOs are correctly instantiated with both complete and incomplete data.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation of DTO instances, ensuring that properties are set as expected.

## Test Descriptions

| **Scenario**                      | **Test Method**                 | **Expected Result**                                                           |
|-----------------------------------|---------------------------------|-------------------------------------------------------------------------------|
| Create an incomplete StaffDto     | `TestCreateIncompleteStaffDto`  | Should correctly instantiate the DTO with only the ID and StaffName.          |
| Create a complete StaffDto        | `TestCreateCompleteStaffDto`    | Should correctly instantiate the DTO with all properties set.                 |
| Create an incomplete StaffDtoList | `TestCreateIncompleteStaffDto2` | Should correctly instantiate the StaffDtoList with only the ID and StaffName. |
| Create a complete StaffDtoList    | `TestCreateCompleteStaffDto2`   | Should correctly instantiate the StaffDtoList with all relevant properties.   |

# 37 - Patient Tests

## Objective

To test the behavior of the `Patient` class in the `DDDNetCore.Domain.Patients` namespace,
ensuring that the class is correctly instantiated and that its attributes can be modified as expected.

## Test Framework

These tests utilize the NUnit framework alongside Moq to mock dependencies, verifying that the `Patient` class behaves as intended when properties are initialized and updated.

## Test Descriptions

| **Scenario**                                | **Test Method**                | **Expected Result**                                                                          |
|---------------------------------------------|--------------------------------|----------------------------------------------------------------------------------------------|
| Create a Patient with valid attributes      | `TestConstructor`              | Should correctly instantiate the Patient object with the provided attributes.                |
| Change the phone number of a patient        | `TestChangePhoneNumber`        | Should successfully update the patient's phone number.                                       |
| Change the name of a patient                | `TestChangePatientName`        | Should successfully update the patient's name.                                               |
| Change the emergency contact of a patient   | `TestChangeEmergencyContact`   | Should successfully update the patient's emergency contact.                                  |
| Change the birth date of a patient          | `TestChangeBirthDate`          | Should successfully update the patient's birth date.                                         |
| Change the gender of a patient              | `TestChangeGender`             | Should successfully update the patient's gender.                                             |
| Change the appointment history of a patient | `TestChangeAppointmentHistory` | Should successfully update the patient's appointment history.                                |
| Change the medical conditions of a patient  | `TestChangeMedicalConditions`  | Should successfully update the patient's medical conditions.                                 |
| Verify behavior of the private constructor  | `TestPrivateConstructor`       | Should create an instance of `Patient` with null medical conditions and appointment history. |

# 38 - Appointment History Tests

## Objective

To test the behavior of the `AppointmentHistory` class in the `DDDNetCore.Domain.Patients` namespace, ensuring that the class is correctly instantiated and that its methods function as expected.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation and comparison of `AppointmentHistory` instances, ensuring that properties are set and compared correctly.

## Test Descriptions

| **Scenario**                                     | **Test Method**          | **Expected Result**                                                                    |
|--------------------------------------------------|--------------------------|----------------------------------------------------------------------------------------|
| Create an AppointmentHistory with a value        | `TestConstructor`        | Should correctly instantiate the AppointmentHistory object with the specified value.   |
| Convert AppointmentHistory to string             | `TestToString`           | Should return the string representation of the AppointmentHistory value.               |
| Compare two equal AppointmentHistory instances   | `TestEquals`             | Should confirm that two instances with the same value are considered equal.            |
| Check hash codes of equal AppointmentHistory     | `TestEqualHashCodes`     | Should ensure that equal instances have the same hash code.                            |
| Check hash codes of different AppointmentHistory | `TestDifferentHashCodes` | Should ensure that instances with different values have different hash codes.          |
| Verify behavior of the private constructor       | `TestPrivateConstructor` | Should create an instance of `AppointmentHistory` with a null AppointmentHistoryValue. |

# 39 - Birth Date Tests

## Objective

To test the behavior of the `BirthDate` class in the `DDDNetCore.Domain.Patients` namespace, ensuring that the class is correctly instantiated and that its methods function as expected.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation and comparison of `BirthDate` instances, ensuring that properties are set and compared correctly.

## Test Descriptions

| **Scenario**                               | **Test Method**          | **Expected Result**                                                           |
|--------------------------------------------|--------------------------|-------------------------------------------------------------------------------|
| Create a BirthDate with a specific value   | `TestConstructor`        | Should correctly instantiate the BirthDate object with the specified value.   |
| Convert BirthDate to string                | `TestToString`           | Should return the string representation of the BirthDate value.               |
| Compare two equal BirthDate instances      | `TestEquals`             | Should confirm that two instances with the same value are considered equal.   |
| Check hash codes of equal BirthDate        | `TestEqualHashCodes`     | Should ensure that equal instances have the same hash code.                   |
| Check hash codes of different BirthDate    | `TestDifferentHashCodes` | Should ensure that instances with different values have different hash codes. |
| Verify behavior of the private constructor | `TestPrivateConstructor` | Should create an instance of `BirthDate` with a null BirthDateValue.          |

# 40 - Emergency Contact Tests

## Objective

To test the behavior of the `EmergencyContact` class in the `DDDNetCore.Domain.Patients` namespace, ensuring that the class is correctly instantiated and that its methods function as expected.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation and comparison of `EmergencyContact` instances, ensuring that properties are set and compared correctly.

## Test Descriptions

| **Scenario**                                     | **Test Method**          | **Expected Result**                                                                |
|--------------------------------------------------|--------------------------|------------------------------------------------------------------------------------|
| Create an EmergencyContact with a specific value | `TestConstructor`        | Should correctly instantiate the EmergencyContact object with the specified value. |
| Convert EmergencyContact to string               | `TestToString`           | Should return the string representation of the EmergencyContact value.             |
| Compare two equal EmergencyContact instances     | `TestEquals`             | Should confirm that two instances with the same value are considered equal.        |
| Check hash codes of equal EmergencyContact       | `TestEqualHashCodes`     | Should ensure that equal instances have the same hash code.                        |
| Check hash codes of different EmergencyContact   | `TestDifferentHashCodes` | Should ensure that instances with different values have different hash codes.      |
| Verify behavior of the private constructor       | `TestPrivateConstructor` | Should create an instance of `EmergencyContact` with a null EmergencyContactValue. |

# 41 - Gender Tests

## Objective

To test the behavior of the `Gender` class in the `DDDNetCore.Domain.Patients` namespace, ensuring that the class is correctly instantiated and that its methods function as expected.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation and comparison of `Gender` instances, ensuring that properties are set and compared correctly.

## Test Descriptions

| **Scenario**                               | **Test Method**          | **Expected Result**                                                           |
|--------------------------------------------|--------------------------|-------------------------------------------------------------------------------|
| Create a Gender with a specific value      | `TestConstructor`        | Should correctly instantiate the Gender object with the specified value.      |
| Convert Gender to string                   | `TestToString`           | Should return the string representation of the Gender value.                  |
| Compare two equal Gender instances         | `TestEquals`             | Should confirm that two instances with the same value are considered equal.   |
| Check hash codes of equal Gender           | `TestEqualHashCodes`     | Should ensure that equal instances have the same hash code.                   |
| Check hash codes of different Gender       | `TestDifferentHashCodes` | Should ensure that instances with different values have different hash codes. |
| Verify behavior of the private constructor | `TestPrivateConstructor` | Should create an instance of `Gender` with a null GenderValue.                |

# 42 - Medical Conditions Tests

## Objective

To test the behavior of the `MedicalConditions` class in the `DDDNetCore.Domain.Patients` namespace, ensuring that the class is correctly instantiated and that its methods function as expected.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation and comparison of `MedicalConditions` instances, ensuring that properties are set and compared correctly.

## Test Descriptions

| **Scenario**                                    | **Test Method**          | **Expected Result**                                                                  |
|-------------------------------------------------|--------------------------|--------------------------------------------------------------------------------------|
| Create MedicalConditions with a specific value  | `TestConstructor`        | Should correctly instantiate the MedicalConditions object with the specified value.  |
| Convert MedicalConditions to string             | `TestToString`           | Should return the string representation of the MedicalConditions value.              |
| Compare two equal MedicalConditions instances   | `TestEquals`             | Should confirm that two instances with the same value are considered equal.          |
| Check hash codes of equal MedicalConditions     | `TestEqualHashCodes`     | Should ensure that equal instances have the same hash code.                          |
| Check hash codes of different MedicalConditions | `TestDifferentHashCodes` | Should ensure that instances with different values have different hash codes.        |
| Verify behavior of the private constructor      | `TestPrivateConstructor` | Should create an instance of `MedicalConditions` with a null MedicalConditionsValue. |

# 43 - Medical Record Number Tests

## Objective

To test the behavior of the `MedicalRecordNumber` class in the `DDDNetCore.Domain.Patients` namespace, ensuring that the class is correctly instantiated and that its methods function as expected.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation and comparison of `MedicalRecordNumber` instances, ensuring that properties are set and compared correctly.

## Test Descriptions

| **Scenario**                                      | **Test Method**          | **Expected Result**                                                                                                            |
|---------------------------------------------------|--------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| Create MedicalRecordNumber with a specific value  | `TestConstructor`        | Should correctly instantiate the MedicalRecordNumber object with the specified value.                                          |
| Convert MedicalRecordNumber to string             | `TestToString`           | Should return the string representation of the MedicalRecordNumber value.                                                      |
| Compare two equal MedicalRecordNumber instances   | `TestEquals`             | Should confirm that two instances with the same value are considered equal.                                                    |
| Check hash codes of equal MedicalRecordNumber     | `TestEqualHashCodes`     | Should ensure that equal instances have the same hash code.                                                                    |
| Check hash codes of different MedicalRecordNumber | `TestDifferentHashCodes` | Should ensure that instances with different values have different hash codes.                                                  |
| Verify behavior of the private constructor        | `TestPrivateConstructor` | Should create an instance of `MedicalRecordNumber` with a default value and confirm that its string representation is correct. |

# 44 - Patient Name Tests

## Objective

To test the behavior of the `PatientName` class in the `DDDNetCore.Domain.Patients` namespace, ensuring that the class is correctly instantiated and that its methods function as expected.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation and comparison of `PatientName` instances, ensuring that properties are set and compared correctly.

## Test Descriptions

| **Scenario**                               | **Test Method**          | **Expected Result**                                                                                    |
|--------------------------------------------|--------------------------|--------------------------------------------------------------------------------------------------------|
| Create PatientName with a specific value   | `TestConstructor`        | Should correctly instantiate the PatientName object with the specified value.                          |
| Convert PatientName to string              | `TestToString`           | Should return the string representation of the PatientName value.                                      |
| Compare two equal PatientName instances    | `TestEquals`             | Should confirm that two instances with the same value are considered equal.                            |
| Check hash codes of equal PatientName      | `TestEqualHashCodes`     | Should ensure that equal instances have the same hash code.                                            |
| Check hash codes of different PatientName  | `TestDifferentHashCodes` | Should ensure that instances with different values have different hash codes.                          |
| Verify behavior of the private constructor | `TestPrivateConstructor` | Should create an instance of `PatientName` and confirm that it is not null and that its value is null. |

# 45 - Phone Number Tests

## Objective

To test the behavior of the `PhoneNumber` class in the `DDDNetCore.Domain.Patients` namespace, ensuring that the class is correctly instantiated and that its methods function as expected for managing phone number data.

## Test Framework

These tests utilize the NUnit framework to verify the correct creation and comparison of `PhoneNumber` instances, ensuring that properties are set and compared correctly.

## Test Descriptions

| **Scenario**                               | **Test Method**          | **Expected Result**                                                                                    |
|--------------------------------------------|--------------------------|--------------------------------------------------------------------------------------------------------|
| Create PhoneNumber with a specific value   | `TestConstructor`        | Should correctly instantiate the PhoneNumber object with the specified value.                          |
| Convert PhoneNumber to string              | `TestToString`           | Should return the string representation of the PhoneNumber value.                                      |
| Compare two equal PhoneNumber instances    | `TestEquals`             | Should confirm that two instances with the same value are considered equal.                            |
| Check hash codes of equal PhoneNumber      | `TestEqualHashCodes`     | Should ensure that equal instances have the same hash code.                                            |
| Check hash codes of different PhoneNumber  | `TestDifferentHashCodes` | Should ensure that instances with different values have different hash codes.                          |
| Verify behavior of the private constructor | `TestPrivateConstructor` | Should create an instance of `PhoneNumber` and confirm that it is not null and that its value is null. |

# 46 - Patient Service Tests

## Objective

To test the behavior of the `PatientService` class, ensuring that methods for retrieving, adding, and updating patient profiles operate correctly, especially under various scenarios.

## Test Framework

These tests utilize the NUnit framework to verify the functionality of the `PatientService`, including fetching, adding, and updating patient records while ensuring that the interactions with the repository and unit of work are appropriately validated.

## Test Descriptions

| **Scenario**                                                     | **Test Method**                                                                              | **Expected Result**                                                                                                                   |
|------------------------------------------------------------------|----------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------|
| Retrieve all patients when profiles exist                        | `GetAllAsync_ReturnsListOfPatientDto_WhenRequestsExist`                                      | Should return a list of `PatientDto` objects that matches the expected data when patient profiles exist.                              |
| Retrieve patients when no profiles exist                         | `GetAllAsync_ReturnsEmptyList_WhenNoPatientProfileExist`                                     | Should return an empty list when there are no patient profiles in the repository.                                                     |
| Add a new patient profile with valid data                        | `AddAsync_CreatesNewPatientProfile_WhenValidDtoIsProvided`                                   | Should create a new patient profile and return the corresponding `PatientDto`, ensuring that repository methods are called correctly. |
| Update an existing patient profile with valid data               | `UpdateAsync_UpdatesExistingPatientProfile_WhenValidDtoIsProvided`                           | Should update the existing patient profile and return the updated `PatientDto`, ensuring the unit of work commits.                    |
| Update a patient profile that does not exist                     | `UpdateAsync_ReturnsNull_WhenPatientProfileDoesNotExist`                                     | Should return null when attempting to update a non-existent patient profile and not commit the unit of work.                          |
| Retrieve patients by name when profiles exist                    | `GetAllByPatientNameAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist`               | Should return a list of `PatientListDto` objects that match the provided patient name.                                                |
| Retrieve patients by birth date when profiles exist              | `GetAllByBirthDateAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist`                 | Should return a list of `PatientListDto` objects that match the provided birth date.                                                  |
| Retrieve patients by email when profiles exist                   | `GetAllByEmailAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist`                     | Should return a list of `PatientListDto` objects that match the provided email.                                                       |
| Retrieve patients by phone number when profiles exist            | `GetAllByPhoneNumberAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist`               | Should return a list of `PatientListDto` objects that match the provided phone number.                                                |
| Retrieve patients by gender when profiles exist                  | `GetAllByGenderAsync_ReturnsListOfPatientListDto_WhenPatientProfileExist`                    | Should return a list of `PatientListDto` objects that match the provided gender.                                                      |
| Retrieve patient by ID when profiles exist                       | `GetByIdAsyncAsync_ReturnsListOfPatientDto_WhenRequestsExist`                                | Should return the corresponding `PatientDto` when a valid medical record number is provided.                                          |
| Retrieve patient by ID that does not exist                       | `GetByIdAsync_ReturnsNull_WhenPatientProfileDoesNotExist`                                    | Should return null when a non-existent medical record number is provided.                                                             |
| Delete patient profile with valid medical record number          | `DeleteAsync_ReturnsListOfPatientDto_WhenValidMedicalRecordNumberIsProvided`                 | Should delete the patient profile and return null if the deletion is successful.                                                      |
| Delete patient data and account with valid medical record number | `DeletePatientDataAndAccount_ReturnsListOfPatientDto_WhenValidMedicalRecordNumberIsProvided` | Should delete the patient data and account, returning null if the deletion is successful.                                             |

# 47 - Patient Controller Tests

## Objective

To verify the behavior of the `PatientsController` class in the `DDDNetCore.Controllers` namespace, ensuring correct handling of requests for patient data, creation, update, and deletion, with proper responses for valid and invalid cases.

## Test Framework

These tests use the NUnit framework alongside Moq to mock dependencies, isolating the controller and service methods to verify that they function as expected.

## Test Descriptions

| **Scenario**                                | **Test Method**                                     | **Expected Result**                                                                   |
|---------------------------------------------|-----------------------------------------------------|---------------------------------------------------------------------------------------|
| Retrieve all patient profiles               | `GetAll_ReturnsAllPatientProfiles`                  | Should return a list of all patient profiles.                                         |
| Retrieve a patient profile by valid ID      | `GetById_ValidId_ReturnsPatient`                    | Should return a single patient profile matching the ID.                               |
| Retrieve a patient profile by invalid ID    | `GetById_InValidId_ReturnsNotFound`                 | Should return a `NotFound` response.                                                  |
| Create a valid patient profile              | `Create_ValidPatientProfile_ReturnsCreatedAtAction` | Should return `CreatedAtAction` result indicating successful creation.                |
| Update a valid patient profile              | `Update_ValidPatientProfile_ReturnsCreatedAtAction` | Should return a `CreatedAtAction` result after successful update.                     |
| Update an invalid patient profile           | `Update_InValidPatientProfile_ReturnsBadRequest`    | Should return a `BadRequest` result for invalid profile update.                       |
| Delete a patient profile by valid ID        | `Delete_ValidId_ReturnsOk`                          | Should return `Ok` result after successfully deleting the patient.                    |
| Delete a patient profile by invalid ID      | `Delete_InvalidId_ReturnsNotFound`                  | Should return a `NotFound` result if the ID does not match any patient.               |
| Delete patient data and account by valid ID | `DeletePatientDataAndAccount_ValidId_ReturnsOk`     | Should return `Ok` result after successfully deleting the patient's data and account. |

# 48 - Patient Dto Tests

## Objective

To validate the creation and initialization of `PatientDto` and `PatientListDto` objects in the `DDDNetCore.Application.DTO` namespace, ensuring that all properties are correctly assigned and that the object state matches expected values in both complete and incomplete scenarios.

## Test Framework

These tests utilize the NUnit framework to verify that the data transfer objects (DTOs) are created with the expected attributes and that each attribute is assigned correctly according to the input values.

## Test Descriptions

| **Scenario**                       | **Test Method**                      | **Expected Result**                                                                                                              |
|------------------------------------|--------------------------------------|----------------------------------------------------------------------------------------------------------------------------------|
| Create Incomplete Patient DTO      | `TestCreateIncompletePatientDto`     | Initializes `PatientDto` with partial data; properties `MedicalRecordNumber` and `PatientName` should match expected values.     |
| Create Complete Patient DTO        | `TestCreateCompletePatientDto`       | Initializes `PatientDto` with full data; all properties should match the provided values, including lists.                       |
| Create Incomplete Patient List DTO | `TestCreateIncompletePatientListDto` | Initializes `PatientListDto` with partial data; properties `MedicalRecordNumber` and `PatientName` should match expected values. |
| Create Complete Patient List DTO   | `TestCreateCompletePatientListDto`   | Initializes `PatientListDto` with full data; all properties should match the provided values.                                    |

# 49 - Login Dto Tests

## Objective

To verify the functionality and correctness of the `LoginDto` class, ensuring that it can be instantiated properly with the necessary properties.

## Test Framework

These tests utilize the NUnit framework to validate the creation and properties of the `LoginDto` object.

## Test Descriptions

| **Scenario**                    | **Test Method**                | **Expected Result**                                                                                             |
|---------------------------------|--------------------------------|-----------------------------------------------------------------------------------------------------------------|
| Create an incomplete `LoginDto` | `TestCreateIncompleteLoginDto` | Should create a `LoginDto` object with the specified email and password properties set.                         |
| Create a complete `LoginDto`    | `TestCreateCompleteLoginDto`   | Should create a `LoginDto` object with all properties (`Email`, `Password`, `ReturnSecureToken`) set correctly. |

# 50 - Login Response Tests

## Objective

To verify the functionality and correctness of the `LoginResponse` class, ensuring that it can be instantiated properly with the necessary properties.

## Test Framework

These tests utilize the NUnit framework to validate the creation and properties of the `LoginResponse` object.

## Test Descriptions

| **Scenario**                         | **Test Method**                     | **Expected Result**                                                                                                                                                         |
|--------------------------------------|-------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Create an incomplete `LoginResponse` | `TestCreateIncompleteLoginResponse` | Should create a `LoginResponse` object with the specified email and registered status properties set.                                                                       |
| Create a complete `LoginResponse`    | `TestCreateCompleteLoginResponse`   | Should create a `LoginResponse` object with all properties (`Kind`, `LocalId`, `Email`, `DisplayName`, `IdToken`, `Registered`, `RefreshToken`, `ExpiresIn`) set correctly. |

# 51 - User Email Micro Service Tests

## Objective

To verify the functionality and correctness of the `UserEmailMicroService` class, ensuring that it can correctly handle user email verification.

## Test Framework

These tests utilize the NUnit framework and Moq for mocking dependencies to validate the behavior of the `UserEmailMicroService`.

## Test Descriptions

| **Scenario**                          | **Test Method**                                            | **Expected Result**                                                                                     |
|---------------------------------------|------------------------------------------------------------|---------------------------------------------------------------------------------------------------------|
| Verify email using a valid user email | `AddAsync_CreatesNewPatientProfile_WhenValidDtoIsProvided` | Should return a non-null result indicating that the email verification was successful (returns `true`). |
