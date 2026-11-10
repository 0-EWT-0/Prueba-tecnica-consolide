using API.Config;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class OperationService : IOperation
{
    private readonly AppContextDB _context;
    private readonly IConfiguration _configuration;

    public OperationService(AppContextDB context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<CreateOperationResponse> CreateOperationAsync(CreateOperationDTO operationDTO)
    {
        try
        {
            // Aca ya entran como enteros y no decimales !

            int totalAmount = operationDTO.Total_amount; // 37630 (376.30)
            int moneyPaid = operationDTO.Money_paid; // 50010 (500.10)

            int totalAmountDecimals = totalAmount % 100; // 30
            int moneyPaidDecimals = moneyPaid % 100; // 10

            if (totalAmountDecimals % 10 != 0) return new CreateOperationResponse(false, 400, null, "Invalid total amount decimals");
            if (moneyPaidDecimals % 10 != 0) return new CreateOperationResponse(false, 400, null, "Invalid money paid decimals");

            if (moneyPaid < totalAmount) return new CreateOperationResponse(false, 400, null, "Money paid cannot be less than total amount");

            int exchange = moneyPaid - totalAmount;

            var newOperation = new OperationModel()
            {
                Total_amount = totalAmount,
                Money_paid = moneyPaid,
                Money_exchange = exchange,
                Id_user_id = operationDTO.UserId,
                Created_at = DateTime.UtcNow,
            };

            _context.Operation.Add(newOperation);
            await _context.SaveChangesAsync();

            return new CreateOperationResponse(true, 201, exchange, "New operation created");

        }
        catch (Exception)
        {
            return new CreateOperationResponse(false, 500, null, "An error occurred while creating an operation");
        }
    }

    public async Task<GetAllOperationHistoryResponse> GetAllOperationHitoryAsync(GetAllOperationHistoryDTO getAllOperationHistoryDTO)
    {
        try
        {
            int offset = getAllOperationHistoryDTO.Offset;
            int limit = getAllOperationHistoryDTO.Limit;

            if (offset > limit)
            {
                return new GetAllOperationHistoryResponse(false, 400, null, "Offset cannot be greater than limit");
            }

            var operations = await _context.Operation.Skip(offset).Take(limit).ToListAsync();

            return new GetAllOperationHistoryResponse(true, 200, operations, "Operations fetch succesful");

        }
        catch (Exception)
        {
            return new GetAllOperationHistoryResponse(false, 500, null, "An error occurred while creating an operation");
        }
    }

}
