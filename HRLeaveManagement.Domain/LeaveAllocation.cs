using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Domain;

public class LeaveAllocation : BaseEntity
{
    public string EmployeeId { get; set; }
    public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
}
