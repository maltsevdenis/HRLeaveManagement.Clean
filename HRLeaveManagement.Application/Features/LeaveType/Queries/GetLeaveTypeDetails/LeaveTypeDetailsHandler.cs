﻿using AutoMapper;

using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class LeaveTypeDetailsHandler : IRequestHandler<LeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public LeaveTypeDetailsHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<LeaveTypeDetailsDto> Handle(LeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id) 
            ?? throw new NotFoundException(nameof(LeaveType), request.Id);
        
        var data = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

        return data;
    }
}
