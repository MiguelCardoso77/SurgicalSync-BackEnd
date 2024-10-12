using System.Collections.Generic;
using DDDNetCore.Domain.OperationTypes;
using DDDSample1.Infrastructure;

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

            context.OperationTypes.AddRange(operationTypes);
            context.SaveChanges();
        }
    }
}