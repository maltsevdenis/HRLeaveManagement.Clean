using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public ChangeLeaveRequestApprovalHandler(IEmailSender emailSender, ILeaveRequestRepository leaveRequestRepository, 
        ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _emailSender = emailSender;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        leaveRequest.Approved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        if (request.Approved)
        {
            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation = await _leaveAllocationRepository.GetUserAllocations
                (leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
            
            await _leaveAllocationRepository.UpdateAsync(allocation);
        }

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"The approval status for your leave request for {request.StartDate:D} to {request.EndDate:D}" +
                $"has been updated.",
                Subject = "Leave Request Approval Status Updated"
            };
        }
        catch (Exception)
        {
            // log error
        }

        return Unit.Value;
    }
}
