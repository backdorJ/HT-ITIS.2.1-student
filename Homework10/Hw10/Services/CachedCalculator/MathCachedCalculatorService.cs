using Hw10.DbModels;
using Hw10.Dto;
using Hw10.ExpressionValidate;
using Hw10.Services.MathCalculator;
using Microsoft.EntityFrameworkCore;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly ApplicationContext _dbContext;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(ApplicationContext dbContext, IMathCalculatorService simpleCalculator)
		=> (_dbContext, _simpleCalculator) = (dbContext, simpleCalculator);

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		try
		{
			ExpressionValidates.Validate(expression ?? string.Empty);
			
			var cachedExpression = await _dbContext.SolvingExpressions
				.FirstOrDefaultAsync(e => e.Expression == expression);

			if (cachedExpression is not null)
			{
				await Task.Delay(1000);
				return new CalculationMathExpressionResultDto(cachedExpression.Result);
			}
			
			var expressionResultDto = await _simpleCalculator.CalculateMathExpressionAsync(expression);

			if (!expressionResultDto.IsSuccess)
				return new CalculationMathExpressionResultDto(expressionResultDto.ErrorMessage);

			var entity = new SolvingExpression
			{
				Expression = expression!,
				Result = expressionResultDto.Result
			};

			await _dbContext.SolvingExpressions.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
				
			return expressionResultDto;
		}
		catch (Exception e)
		{
			return new CalculationMathExpressionResultDto(e.Message);
		}
	}
}