using System.Collections.Generic;
using System.ComponentModel;
using DDDNetCore.Domain.OperationTypes;
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
                    new MedicalRecordNumber("1"), new PhoneNumber("938413938"),
                    new List<MedicalConditions>(),
                    new EmergencyContact("933264402"),
                    new List<AppointmentHistory>(),
                    new UserEmail("1221194@isep.ipp.pt")
                ),

                new(new PatientName("Miguel"), new BirthDate("4 de Julho de 2004"), new Gender("Masculino"),
                    new MedicalRecordNumber("2"), new PhoneNumber("938745060"),
                    new List<MedicalConditions>(),
                    new EmergencyContact("930923458"),
                    new List<AppointmentHistory>(),
                    new UserEmail("1220772@isep.ipp.pt")
                )
            };

            

            var staffs = new List<Domain.Staffs.Staff>
            {
                new(new LicenseNumber("N202400001"), new StaffName("Tomás Gonçalves"),
                    new StaffEmail("1220917@isep.ipp.pt"), new StaffPhoneNumber("962754971"),
                    StaffSpecialization.Family_medicine,
                    new List<StaffAvaiabilitySlots>()
                    //, StaffType.Nurse
                    ),
                    
                new(new LicenseNumber("N202400002"), new StaffName("José Mesquita"),
                    new StaffEmail("josemesquita@gmail.com"), new StaffPhoneNumber("962749671"),
                    StaffSpecialization.Family_medicine,
                    new List<StaffAvaiabilitySlots>()
                    //,StaffType.Doctor 
                    )
            };  
            
            context.Patients.AddRange(patients);
            context.OperationTypes.AddRange(operationTypes);
            context.Staffs.AddRange(staffs);
            context.SaveChanges();
        }
    }
}