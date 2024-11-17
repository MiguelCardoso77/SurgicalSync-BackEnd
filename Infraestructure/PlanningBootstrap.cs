using System;
using System.Linq;

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

            var x = "http://localhost:8888/agendaStaff?staffID=" + id + "&day=" + currentDate;
            Console.WriteLine(x);
        }
    }

    private static void TimetableMethod(SurgicalSyncContext context, string currentDate)
    {
        var staff = context.Staffs.ToList();
        foreach (var s in staff)
        {
            var id = s.Id.AsString().ToLower();
            var slots = s.StaffAvailabilitySlots.ConvertIntoMinutes();
            
            var x = "timetable(" + id + ", " + currentDate + ", " + "(" + slots + ")" + ").";
            Console.WriteLine(x);
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
            
            var x = "staff(" + id + ", " + type + ", " + specialization + ", (oT1, oT3)" + ").";
            Console.WriteLine(x);
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
            
            var x = "surgery(" + id + ", " + preparationTime + ", " + surgeryTime + ", " + cleaningTime + ").";
            Console.WriteLine(x);
        }
    }

    private static void SurgeryIdMethod(SurgicalSyncContext context)
    {
        var operationRequests = context.OperationRequests.ToList();
        foreach (var oR in operationRequests)
        {
            var id = "oR" + oR.Id.AsString();
            var oT = "oT" + oR.OperationTypeId.AsString();
            
            var x = "surgery_id(" + id + ", " + oT + ").";
            Console.WriteLine(x);
        }
    }

    private static void AssignmentSurgeryMethod(SurgicalSyncContext context)
    {
        var operationRequests = context.OperationRequests.ToList();
        foreach (var oR in operationRequests)
        {
            var id = "oR" + oR.Id.AsString();
            var staff = oR.StaffId.AsString().ToLower();

            var x = "assignment_surgery(" + id + ", " + staff + ").";
            Console.WriteLine(x);
        }
    }

    private static void AgendaOperationRoomMethod(SurgicalSyncContext context, string currentDate)
    {
        var surgeryRooms = context.SurgeryRooms.ToList();
        foreach (var sR in surgeryRooms)
        {
            var id = "sR" + sR.Id.AsString();
            
            var x = "agenda_operation_room(" + id + ", " + currentDate + ", " + "[]" + ").";
            Console.WriteLine(x);
        }
    }
}