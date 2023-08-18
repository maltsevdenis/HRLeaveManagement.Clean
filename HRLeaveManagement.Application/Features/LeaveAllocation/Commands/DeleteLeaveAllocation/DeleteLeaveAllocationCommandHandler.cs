﻿using AutoMapper;

using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation == null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        await _leaveAllocationRepository.DeleteAsync(leaveAllocation);
     }
}
