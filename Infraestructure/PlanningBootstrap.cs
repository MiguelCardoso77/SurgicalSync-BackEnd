using System;
using System.Linq;
using System.Net.Http;

namespace DDDNetCore.Infraestructure;

public class PlanningBootstrap
{
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
    }
    
    private static void AgendaStaffMethod(SurgicalSyncContext context, string currentDate)
    {
        var staff = context.Staffs.ToList();
        foreach (var s in staff)
        {
            var id = s.Id.AsString().ToLower();
            
            new HttpClient() { BaseAddress = new Uri("http://localhost:8888") }.GetAsync($"agendaStaff?staffID={id}&day={currentDate}");
            
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
            
            new HttpClient() { BaseAddress = new Uri("http://localhost:8888") }.GetAsync($"timetable?staffID={id}&day={currentDate}&slots={slots}");
            
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
            var oTs = "oT1,oT3";
            
            new HttpClient() { BaseAddress = new Uri("http://localhost:8888") }.GetAsync($"staff?staffID={id}&role={type}&specialization={specialization}&oTs={oTs}");
            
            var x = "http://localhost:8888/staff?staffID=" + id + "&role=" + type + "&specialization=" + specialization + "&oTs=" + oTs;
            var y = "staff(" + id + ", " + type + ", " + specialization + ", " + oTs + ").";
        }
    }
    
    private static void SurgeryMethod(SurgicalSyncContext context)
    {
        var operationTypes = context.OperationTypes.ToList();
        foreach (var oT in operationTypes)
        {
            var id = "oT" + oT.Id.AsString();
            var durations = oT.EstimatedDuration.ToString().Split(',');
            var preparationTime = durations[0].Trim();
            var surgeryTime = durations[1].Trim();
            var cleaningTime = durations[2].Trim();
            
            new HttpClient() { BaseAddress = new Uri("http://localhost:8888") }.GetAsync($"surgery?surgeryID={id}&t1={preparationTime}&t2={surgeryTime}&t3={cleaningTime}");
            
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
            
            new HttpClient() { BaseAddress = new Uri("http://localhost:8888") }.GetAsync($"surgeryId?oRID={id}&surgeryID={oT}");
            
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
            var staff = oR.StaffId.AsString().ToLower();
            
            new HttpClient() { BaseAddress = new Uri("http://localhost:8888") }.GetAsync($"assignmentSurgery?oRID={id}&staffID={staff}");

            var x = "http://localhost:8888/assignmentSurgery?oRID=" + id + "&staffID=" + staff;
            var y = "assignment_surgery(" + id + ", " + staff + ").";
        }
    }

    private static void AgendaOperationRoomMethod(SurgicalSyncContext context, string currentDate)
    {
        var surgeryRooms = context.SurgeryRooms.ToList();
        foreach (var sR in surgeryRooms)
        {
            var id = "sR" + sR.Id.AsString();
            
            new HttpClient() { BaseAddress = new Uri("http://localhost:8888") }.GetAsync($"agendaOperationRoom?room={id}&day={currentDate}");

            var x = "http://localhost:8888/agendaOperationRoom?room=" + id + "&day=" + currentDate;
            var y = "agenda_operation_room(" + id + ", " + currentDate + ", " + "[]" + ").";
        }
    }
}