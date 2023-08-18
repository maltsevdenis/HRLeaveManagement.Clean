using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

using MediatR;

using System.ComponentModel.DataAnnotations.Schema;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>
{
    
}
