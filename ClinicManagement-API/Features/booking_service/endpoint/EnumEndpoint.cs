namespace ClinicManagement_API.Features.booking_service.endpoint
{
    public static class EnumEndpoint
    {
        public static void MapEnumEndpoint(this IEndpointRouteBuilder route)
        {
            var app = route.MapGroup("/api/enums");

        }


    }
}