using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DDDNetCore.Infraestructure;

public class PlanningBootstrap
{
    private static readonly HttpClient HttpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8888") };

    public static void BootstrapData(SurgicalSyncContext context)
    {
        var currentDate = DateTime.Now.ToString("yyyyMMdd");

        AgendaStaffMethod(context, currentDate);
        TimetableMethod(context, currentDate);
        StaffMethod(context);
        SurgeryMethod(context);
        SurgeryIdMethod(context);
        AssignmentSurgeryMethod(context);
        AgendaOperationRoomMethod(context, currentDate);
        
        Console.WriteLine("Planning Bootstrap Done!");
    }
    
    private static void AgendaStaffMethod(SurgicalSyncContext context, string currentDate)
    {
        var staff = context.Staffs.ToList();
        foreach (var s in staff)
        {
            var id = s.Id.AsString().ToLower();
            var endpoint = $"agendaStaff?staffID={id}&day={currentDate}";
            HttpClient.GetAsync(endpoint);
            
            var x = "http://localhost:8888/agendaStaff?staffID=" + id + "&day=" + currentDate;
            var y = "agenda_staff(" + id + ", " + currentDate + ", " + "[]" + ").";
        }
    }

    private static void TimetableMethod(SurgicalSyncContext context, string currentDate)
    {
        var staff = context.Staffs.ToList();
        foreach (var s in staff)
        {
            var id = s.Id.AsString().ToLower();
            var slots = s.StaffAvailabilitySlots.ConvertIntoMinutes();
            
            var endpoint = $"timetable?staffID={id}&day={currentDate}&slots={slots}";
            HttpClient.GetAsync(endpoint);
            
            var x = "http://localhost:8888/timetable?staffID=" + id + "&day=" + currentDate + "&slots=" + slots;
            var y = "timetable(" + id + ", " + currentDate + ", " + "(" + slots + ")" + ").";
        }
    }

    private static void StaffMethod(SurgicalSyncContext context)
    {
        var staff = context.Staffs.ToList();
        foreach (var s in staff)
        {
            var id = s.Id.AsString().ToLower();
            var type = s.StaffType.ToString().ToLower();
            var specialization = s.StaffSpecialization.ToString().ToLower();
            var oTs = "(oT1, oT2, oT3, oT4, oT5, oT6, oT7, oT8, oT9, oT10)";
            
            var endpoint = $"staff?staffID={id}&role={type}&specialization={specialization}&oTs={oTs}";
            //HttpClient.GetAsync(endpoint);
            
            var x = "http://localhost:8888/staff?staffID=" + id + "&role=" + type + "&specialization=" + specialization + "&oTs=" + oTs;
            var y = "staff(" + id + ", " + type + ", " + specialization + ", " + oTs + ").";
            //Console.WriteLine(y);
        }
    }
    
    private static void SurgeryMethod(SurgicalSyncContext context)
    {
        var operationTypes = context.OperationTypes.ToList();
        foreach (var oT in operationTypes)
        {
            var id = "oT" + oT.Id.AsString();
            var durations = oT.EstimatedDuration.ToString().Split(',');
            var preparationTime = int.Parse(durations[0].Trim());
            var surgeryTime = int.Parse(durations[1].Trim());
            var cleaningTime = int.Parse(durations[2].Trim());
            
            surgeryTime = preparationTime + surgeryTime + cleaningTime;
            
            var endpoint = $"surgery?surgeryID={id}&t1={preparationTime}&t2={surgeryTime}&t3={cleaningTime}";
            HttpClient.GetAsync(endpoint);
            
            var x = "http://localhost:8888/surgery?surgeryID=" + id + "&t1=" + preparationTime + "&t2=" + surgeryTime + "&t3=" + cleaningTime;
            var y = "surgery(" + id + ", " + preparationTime + ", " + surgeryTime + ", " + cleaningTime + ").";
        }
    }

    private static void SurgeryIdMethod(SurgicalSyncContext context)
    {
        var operationRequests = context.OperationRequests.ToList();
        foreach (var oR in operationRequests)
        {
            var id = "oR" + oR.Id.AsString();
            var oT = "oT" + oR.OperationTypeId.AsString();
            
            var endpoint = $"surgeryId?oRID={id}&surgeryID={oT}";
            HttpClient.GetAsync(endpoint);
            
            var x = "http://localhost:8888/surgeryId?oRID=" + id + "&surgeryID=" + oT;
            var y = "surgery_id(" + id + ", " + oT + ").";
        }
    }

    private static void AssignmentSurgeryMethod(SurgicalSyncContext context)
    {
        var operationRequests = context.OperationRequests.ToList();
        foreach (var oR in operationRequests)
        {
            var id = "oR" + oR.Id.AsString();
            var oT = "oT" + oR.OperationTypeId.AsString();

            var staff = new List<string>();
            
            switch (oT)
            {
                case "oT1":
                    staff.Add("d202400001");
                    staff.Add("d202400002");
                    staff.Add("d202400003");
                    staff.Add("d202400017");
                    staff.Add("n202400012");
                    staff.Add("n202400018");
                    staff.Add("n202400009");
                    staff.Add("o202400011");
                    break;
                case "oT2":
                    staff.Add("d202400004");
                    staff.Add("d202400006");
                    staff.Add("d202400007");
                    staff.Add("d202400005");
                    staff.Add("n202400013");
                    staff.Add("n202400019");
                    staff.Add("n202400020");
                    staff.Add("o202400021");
                    break;
                case "oT3":
                    staff.Add("d202400014");
                    staff.Add("d202400024");
                    staff.Add("d202400025");
                    staff.Add("d202400008");
                    staff.Add("n202400015");
                    staff.Add("n202400016");
                    staff.Add("n202400010");
                    staff.Add("o202400023");
                    break;
                case "oT4":
                    staff.Add("d202400006");
                    staff.Add("d202400007");
                    staff.Add("d202400005");
                    staff.Add("n202400013");
                    staff.Add("n202400019");
                    staff.Add("n202400020");
                    staff.Add("o202400021");
                    break;
                case "oT5":
                    staff.Add("d202400014");
                    staff.Add("d202400024");
                    staff.Add("d202400008");
                    staff.Add("n202400015");
                    staff.Add("n202400016");
                    staff.Add("n202400010");
                    staff.Add("o202400023");
                    break;
                case "oT6":
                    staff.Add("d202400002");
                    staff.Add("d202400003");
                    staff.Add("d202400017");
                    staff.Add("n202400012");
                    staff.Add("n202400018");
                    staff.Add("n202400009");
                    staff.Add("o202400011");
                    break;
                case "oT7":
                    staff.Add("d202400004");
                    staff.Add("d202400007");
                    staff.Add("d202400005");
                    staff.Add("n202400013");
                    staff.Add("n202400019");
                    staff.Add("n202400020");
                    staff.Add("o202400021");
                    break;
                case "oT8":
                    staff.Add("d202400001");
                    staff.Add("d202400003");
                    staff.Add("d202400017");
                    staff.Add("n202400012");
                    staff.Add("n202400018");
                    staff.Add("n202400009");
                    staff.Add("o202400011");
                    staff.Add("o202400022");
                    break;
                case "oT9":
                    staff.Add("d202400014");
                    staff.Add("d202400008");
                    staff.Add("n202400015");
                    staff.Add("n202400016");
                    staff.Add("n202400010");
                    staff.Add("o202400023");
                    break;
                case "oT10":
                    staff.Add("d202400014");
                    staff.Add("d202400008");
                    staff.Add("n202400015");
                    staff.Add("n202400016");
                    staff.Add("n202400010");
                    staff.Add("o202400023");
                    break;
            }
            
            var endpoint = $"assignmentSurgery?oRID={id}&staffID={string.Join(",", staff)}";
            //HttpClient.GetAsync(endpoint);
            
            var x = "http://localhost:8888/assignmentSurgery?oRID=" + id + "&staffID=" + staff;
            
            foreach (var s in staff)
            {
                var y = "assignment_surgery(" + id + ", " + s + ").";
                Console.WriteLine(y);
            }
        }
    }

    private static void AgendaOperationRoomMethod(SurgicalSyncContext context, string currentDate)
    {
        var surgeryRooms = context.SurgeryRooms.ToList();
        foreach (var sR in surgeryRooms)
        {
            var id = "sR" + sR.Id.AsString();
            
            var endpoint = $"agendaOperationRoom?room={id}&day={currentDate}";
            HttpClient.GetAsync(endpoint);
            
            var x = "http://localhost:8888/agendaOperationRoom?room=" + id + "&day=" + currentDate;
            var y = "agenda_operation_room(" + id + ", " + currentDate + ", " + "[]" + ").";
        }
    }
}