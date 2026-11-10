using API.Models;

namespace API.Interfaces;

public record CreateOperationResponse(bool Flag, int Code, int? exchange, string Message = null!);
public record GetAllOperationHistoryResponse(bool Flag, int Code, List<OperationModel>? Model = null!, string Message = null!);

public record CreateOperationDTO(int UserId, int Total_amount, int Money_paid);
public record GetAllOperationHistoryDTO(int Limit, int Offset);

public interface IOperation
{
    Task<CreateOperationResponse> CreateOperationAsync(CreateOperationDTO createOperationDTO);
    Task<GetAllOperationHistoryResponse> GetAllOperationHitoryAsync(GetAllOperationHistoryDTO getAllOperationHistoryDTO);
}
