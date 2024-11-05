using System;
using System.Collections.Generic;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Staffs;

namespace DDDNetCore.Infraestructure
{
    public static class Bootstrap
    {
        public static void BootstrapData(SurgicalSyncContext context)
        {
            var operationTypes = new List<OperationType>
            {
                new(new OperationTypeId("1"), new OperationName("ACL Reconstruction Surgery"),
                    new RequiredStaff("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist), 1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("45, 60, 30")),
                
                new(new OperationTypeId("2"), new OperationName("Knee Replacement Surgery"),
                    new RequiredStaff("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist), 1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("45, 60, 45")),
                    
                new(new OperationTypeId("3"), new OperationName("Shoulder Replacement Surgery"),
                    new RequiredStaff("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist), 1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("45, 90, 45")),

                new(new OperationTypeId("4"), new OperationName("Hip Replacement Surgery"),
                    new RequiredStaff("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist), 1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("45, 75, 45")),

                new(new OperationTypeId("5"), new OperationName("Meniscal Injury Treatment"),
                    new RequiredStaff("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist), 1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("45, 45, 20")),

                new(new OperationTypeId("6"), new OperationName("Rotator Cuff Repair"),
                    new RequiredStaff("2 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist), 1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("45, 80, 30")),

                new(new OperationTypeId("7"),
                    new OperationName("Ankle Ligaments Reconstruction or Repair"),
                    new RequiredStaff("2 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist), 1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("30, 45, 20")),

                new(new OperationTypeId("8"), new OperationName("Lumbar Discectomy"),
                    new RequiredStaff("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist), 1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("20, 45, 15")),

                new(new OperationTypeId("9"), new OperationName("Trigger Finger"),
                    new RequiredStaff("1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("15, 10, 15")),

                new(new OperationTypeId("10"), new OperationName("Carpal Tunnel Syndrome"),
                    new RequiredStaff("1 Orthopaedist, 1 Anaesthetist, 1 Instrumenting Nurse, 1 Circulating Nurse, 1 Nurse Anaesthetist, 1 Medical Action Assistant"),
                    new EstimatedDuration("15, 10, 15"))
            };
            
            /**

            var patients = new List<Patient>
            {
                new(new PatientName("Diana"), new BirthDate("30 de Junho de 2004"), new Gender("Feminino"),
                    new MedicalRecordNumber("202409000001"), new PhoneNumber("938413938"),
                    new MedicalConditions(null),
                    new EmergencyContact("933264402"),
                    new AppointmentHistory(null),
                    new UserEmail("1221195@isep.ipp.pt")
                ),

                new(new PatientName("Miguel"), new BirthDate("4 de Julho de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000002"), new PhoneNumber("938745060"),
                    new MedicalConditions(null),
                    new EmergencyContact("930923458"),
                    new AppointmentHistory(null),
                    new UserEmail("1220000@isep.ipp.pt")
                ),
                
                new(new PatientName("Diogo"), new BirthDate("8 de Janeiro de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000003"), new PhoneNumber("938745065"),
                    new MedicalConditions(null),
                    new EmergencyContact("930923459"),
                    new AppointmentHistory(null),
                    new UserEmail("1220812@isep.ipp.pt")
                )
            };

            var staffs = new List<Domain.Staffs.Staff>
            {
                new(new StaffId("D202400001"), new StaffName("Tomás Gonçalves"),
                    new UserEmail("1220917@isep.ipp.pt"), new StaffPhoneNumber("962754971"),
                    StaffSpecialization.Family_medicine,
                    new StaffAvaiabilitySlots(
                       "2024-09-25:14h00-18h00 ; 2024-09-25:19h00/2024-09-26:02h00")
                    , StaffType.Doctor,
                    isActive:true,
                    new StaffLicenseNumber("00001")

                    ),
                    
              
                new(new StaffId("N202400002"), new StaffName("Diana Neves"),
                    new UserEmail("1221194@isep.ipp.pt"), new StaffPhoneNumber("962749672"),
                    StaffSpecialization.Family_medicine,
                    new StaffAvaiabilitySlots(
                        "2024-09-25:14h00-18h00 ; 2024-09-25:19h00/2024-09-26:02h00")
                     ,StaffType.Nurse,
                    isActive:true,
                    new StaffLicenseNumber("00002")

                ),
                
                new(new StaffId("N202400003"), new StaffName("Gonçalo Sousa"),
                new UserEmail("1221331@isep.ipp.pt"), new StaffPhoneNumber("962749673"),
                StaffSpecialization.Cardiology,
                new StaffAvaiabilitySlots("2024-09-25:14h00-18h00 ; 2024-09-25:19h00/2024-09-26:02h00")
                ,StaffType.Nurse,
                isActive:true,
                new StaffLicenseNumber("00003")
                )
            };

            var users = new List<User>
            {
                new(new UserId("1"), new Username("Diogo"), new UserEmail("1220000@isep.ipp.pt"), UserRole.Doctor),
                new(new UserId("2"), new Username("DiogoR"), new UserEmail("1220812@isep.ipp.pt"), UserRole.Patient),
                new(new UserId("3"), new Username("Tomás"), new UserEmail("1220917@isep.ipp.pt"), UserRole.Patient)

            };

            var request = new List<OperationRequest>
            {
                new(new OperationRequestId("1"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 01,07)), new OperationTypeId("5"), new MedicalRecordNumber("202409000001"), new StaffId("N202400001")),
                
                new(new OperationRequestId("2"), Priority.ElectiveSurgery, new DeadlineDate(new DateTime(2025, 11,10)), new OperationTypeId("1"), new MedicalRecordNumber("202409000002"), new StaffId("N202400001")),
    
                new(new OperationRequestId("3"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2024, 12, 15)), new OperationTypeId("2"), new MedicalRecordNumber("202409000003"), new StaffId("N202400002")),
    
                new(new OperationRequestId("4"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 01, 20)), new OperationTypeId("2"), new MedicalRecordNumber("202409000001"), new StaffId("N202400001")),
    
                new(new OperationRequestId("5"), Priority.ElectiveSurgery, new DeadlineDate(new DateTime(2026, 02, 25)), new OperationTypeId("3"), new MedicalRecordNumber("202409000002"), new StaffId("N202400003")),
    
                new(new OperationRequestId("6"), Priority.EmergencySurgery, new DeadlineDate(new DateTime(2025, 03, 30)), new OperationTypeId("1"), new MedicalRecordNumber("202409000002"), new StaffId("N202400004")),

                new(new OperationRequestId("7"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 04, 05)), new OperationTypeId("4"), new MedicalRecordNumber("202409000003"), new StaffId("N202400005")),

            };
            
            */
            
            //context.Patients.AddRange(patients);
            context.OperationTypes.AddRange(operationTypes);
            //context.Staffs.AddRange(staffs);
            //context.Users.AddRange(users);
            //wcontext.OperationRequests.AddRange(request);
            context.SaveChanges();
        }
    }
}