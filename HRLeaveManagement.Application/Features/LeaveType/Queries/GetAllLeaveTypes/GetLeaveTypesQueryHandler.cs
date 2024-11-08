﻿using AutoMapper;

using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Logging;

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

    public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypesQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        this._logger = logger;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query to database
        var leaveTypes = await _leaveTypeRepository.GetAsync();

        // convert data object to DTO objects
        var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        // return list of DTO objects
        _logger.LogInformation("Leave types were retrieved successfully");
        return data;
    }
}
