using Microsoft.EntityFrameworkCore.Query;

namespace MarkRent.API.ViewModel.Request.Hire
{
    public record HirePostViewModel(Guid DeliverAgentId, Guid VehicleId, DateTime EndDate, DateTime EstimatedEndDate, int Plan);
}
