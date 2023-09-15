using HRLeaveManagement.BlazorUI.Contracts;
using HRLeaveManagement.BlazorUI.Models.LeaveRequests;

using Microsoft.AspNetCore.Components;

namespace HRLeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Details
{
    [Inject]
    ILeaveRequestService leaveRequestService { get; set; }

    [Inject]
    NavigationManager navigationManager { get; set; } 

    [Parameter]
    public int Id { get; set; }

    string ClassName;
    string HeadingText;

    public LeaveRequestVM Model { get; private set; } = new LeaveRequestVM();

    protected override async Task OnParametersSetAsync()
    {
        Model = await leaveRequestService.GetLeaveRequest(Id);
    }

    protected override async Task OnInitializedAsync()
    {
        if (Model.Approved == null)
        {
            ClassName = "warning";
            HeadingText = "Pending Approval";
        }
        else if (Model.Approved == true)
        {
            ClassName = "success";
            HeadingText = "Approved";
        }
        else
        {
            ClassName = "danger";
            HeadingText = "Rejected";
        }
    }

    async Task ChangeApproval(bool approvalStatus)
    {
        await leaveRequestService.ApproveLeaveRequest(Id, approvalStatus);
        navigationManager.NavigateTo("/leaverequests/");
    }
}
