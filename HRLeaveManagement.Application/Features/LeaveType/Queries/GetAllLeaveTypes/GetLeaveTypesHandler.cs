using AutoMapper;

using HRLeaveManagement.Application.Contracts.Persistence;

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypesHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query to database
        var leaveTypes = await _leaveTypeRepository.GetAsync();

        // convert data object to DTO objects
        var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        // return list of DTO objects
        return data;
    }
}
