using HRLeaveManagement.Application.Features.LeaveRequest.Shared;

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommand : BaseLeaveRequest, IRequest<Unit>
{
    public int Id { get; set; }
    public bool Approved { get; set; }
}
