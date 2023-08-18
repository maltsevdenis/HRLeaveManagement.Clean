using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQuery : IRequest<LeaveAllocationDetailsDto>
{
    public int Id { get; set; }
}
