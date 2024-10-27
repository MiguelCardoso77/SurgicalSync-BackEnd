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

