using System;
using System.Collections.Generic;
using System.ComponentModel;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.SurgicalSyncTests.Domain.Staffs;
using DDDSample1.Infrastructure;
using NUnit.Framework;

namespace DDDNetCore.Infraestructure
{
    public static class Bootstrap
    {
        public static void BootstrapData(DDDSample1DbContext context)
        {
            var operationTypes = new List<OperationType>
            {
                new(new OperationTypeId("1"), new OperationName("ACL Reconstruction Surgery"),
                    new List<RequiredStaff>
                    {
                        new("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist)"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("45"), new("60"), new("30") }),

                new(new OperationTypeId("2"), new OperationName("Knee Replacement Surgery"),
                    new List<RequiredStaff>
                    {
                        new("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist)"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("45"), new("60"), new("45") }),

                new(new OperationTypeId("3"), new OperationName("Shoulder Replacement Surgery"),
                    new List<RequiredStaff>
                    {
                        new("3 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist)"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("45"), new("90"), new("45") }),

                new(new OperationTypeId("4"), new OperationName("Hip Replacement Surgery"),
                    new List<RequiredStaff>
                    {
                        new("2 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist)"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("45"), new("75"), new("45") }),

                new(new OperationTypeId("5"), new OperationName("Meniscal Injury Treatment"),
                    new List<RequiredStaff>
                    {
                        new("2 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist)"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("45"), new("45"), new("20") }),

                new(new OperationTypeId("6"), new OperationName("Rotator Cuff Repair"),
                    new List<RequiredStaff>
                    {
                        new("2 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist)"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("45"), new("80"), new("30") }),

                new(new OperationTypeId("7"),
                    new OperationName("Ankle Ligaments Reconstruction or Repair"),
                    new List<RequiredStaff>
                    {
                        new("2 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist)"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("30"), new("45"), new("20") }),

                new(new OperationTypeId("8"), new OperationName("Lumbar Discectomy"),
                    new List<RequiredStaff>
                    {
                        new("2 Doctors or Interns in Orthopaedics (minimum 1 Orthopaedist)"),
                        new("1 X-ray Technician"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("20"), new("45"), new("15") }),

                new(new OperationTypeId("9"), new OperationName("Trigger Finger"),
                    new List<RequiredStaff>
                    {
                        new("1 Orthopaedist"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("15"), new("10"), new("15") }),

                new(new OperationTypeId("10"), new OperationName("Carpal Tunnel Syndrome"),
                    new List<RequiredStaff>
                    {
                        new("1 Orthopaedist"),
                        new("1 Orthopaedist"),
                        new("1 Anaesthetist"),
                        new("1 Instrumenting Nurse"),
                        new("1 Circulating Nurse"),
                        new("1 Nurse Anaesthetist"),
                        new("1 Medical Action Assistant")
                    },
                    new List<EstimatedDuration> { new("15"), new("10"), new("15") })
            };

            var patients = new List<Patient>
            {
                new(new PatientName("Diana"), new BirthDate("30 de Junho de 2004"), new Gender("Feminino"),
                    new MedicalRecordNumber("202409000001"), new PhoneNumber("938413938"),
                    new List<MedicalConditions>(),
                    new EmergencyContact("933264402"),
                    new List<AppointmentHistory>(),
                    new UserEmail("1221195@isep.ipp.pt")
                ),

                new(new PatientName("Miguel"), new BirthDate("4 de Julho de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("202409000002"), new PhoneNumber("938745060"),
                    new List<MedicalConditions>(),
                    new EmergencyContact("930923458"),
                    new List<AppointmentHistory>(),
                    new UserEmail("1220772@isep.ipp.pt")
                )
            };

            var staffs = new List<Domain.Staffs.Staff>
            {
                new(new LicenseNumber("D202400001"), new StaffName("Tomás Gonçalves"),
                    new StaffEmail("1220917@isep.ipp.pt"), new StaffPhoneNumber("962754971"),
                    StaffSpecialization.Family_medicine,
                    new List<StaffAvaiabilitySlots>
                    {
                        new StaffAvaiabilitySlots("2024-09-25:14h00-18h00"),
                        new StaffAvaiabilitySlots("2024-09-25:19h00/2024-09-26:02h00")
                    }
                    , StaffType.Doctor
                    ),
                    
                new(new LicenseNumber("N202400002"), new StaffName("Diana Neves"),
                    new StaffEmail("1221195@isep.ipp.pt"), new StaffPhoneNumber("962749672"),
                    StaffSpecialization.Family_medicine,
                    new List<StaffAvaiabilitySlots>
                    {
                        new StaffAvaiabilitySlots("2024-09-25:14h00-18h00"),
                        new StaffAvaiabilitySlots("2024-09-25:19h00/2024-09-26:02h00")
                    }                    ,StaffType.Nurse
                    ),
                
                new(new LicenseNumber("N202400003"), new StaffName("Gonçalo Sousa"),
                new StaffEmail("1221331@isep.ipp.pt"), new StaffPhoneNumber("962749673"),
                StaffSpecialization.Cardiology,
                new List<StaffAvaiabilitySlots>()
                ,StaffType.Nurse
                )
            };

            var users = new List<User>
            {
                new(new UserId("1"), new Username("Diogo"), new UserEmail("1220812@isep.ipp.pt"), UserRole.Doctor)
            };

            var request = new List<OperationRequest>
            {
                new(new OperationRequestId("1"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 01,07)), new OperationTypeId("5"), new MedicalRecordNumber("202409000001"), new LicenseNumber("N202400001")),
                
                new(new OperationRequestId("2"), Priority.ElectiveSurgery, new DeadlineDate(new DateTime(2025, 11,10)), new OperationTypeId("1"), new MedicalRecordNumber("202409000002"), new LicenseNumber("N202400001")),
    
                new(new OperationRequestId("3"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2024, 12, 15)), new OperationTypeId("2"), new MedicalRecordNumber("202409000003"), new LicenseNumber("N202400002")),
    
                new(new OperationRequestId("4"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 01, 20)), new OperationTypeId("2"), new MedicalRecordNumber("202409000001"), new LicenseNumber("N202400001")),
    
                new(new OperationRequestId("5"), Priority.ElectiveSurgery, new DeadlineDate(new DateTime(2026, 02, 25)), new OperationTypeId("3"), new MedicalRecordNumber("202409000002"), new LicenseNumber("N202400003")),
    
                new(new OperationRequestId("6"), Priority.EmergencySurgery, new DeadlineDate(new DateTime(2025, 03, 30)), new OperationTypeId("1"), new MedicalRecordNumber("202409000002"), new LicenseNumber("N202400004")),

                new(new OperationRequestId("7"), Priority.UrgentSurgery, new DeadlineDate(new DateTime(2025, 04, 05)), new OperationTypeId("4"), new MedicalRecordNumber("202409000003"), new LicenseNumber("N202400005")),

            };
            
            context.Patients.AddRange(patients);
            context.OperationTypes.AddRange(operationTypes);
            context.Staffs.AddRange(staffs);
            context.Users.AddRange(users);
            context.OperationRequests.AddRange(request);
            context.SaveChanges();
        }
    }
}