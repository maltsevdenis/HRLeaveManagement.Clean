using AutoMapper;

using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IEmailSender _emailSender;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
        ILeaveAllocationRepository leaveAllocationRepository, IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        
        leaveRequest.Cancelled = true;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        if (leaveRequest.Approved == true)
        {
            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation = await _leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
            allocation.NumberOfDays += daysRequested;

            await _leaveAllocationRepository.UpdateAsync(allocation);
        }

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been cancelled successfully.",
                Subject = "Leave Rerquest Cancelled"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception) 
        {
            // log error
        }
        return Unit.Value;
    }
}
