using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.UseCases.Order.Delete;
using OrderManagement.Application.UseCases.Order.GetAll;
using OrderManagement.Application.UseCases.Order.GetById;
using OrderManagement.Application.UseCases.Order.Register;
using OrderManagement.Application.UseCases.Order.Update;
using OrderManagement.Communication.Requests;
using OrderManagement.Communication.Responses;

namespace OrderManagement.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseOrderJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [EndpointSummary("Rota para registro de um pedido.")]
        public async Task<IActionResult> RegisterOrder(
            [FromBody] RequestOrderJson request,
            [FromServices] IRegisterOrderUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response); 
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseOrderJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [EndpointSummary("Rota para consultar um único pedido.")]
        public async Task<IActionResult> GetOrderById(
            [FromRoute] Guid id,
            [FromServices] IGetOrderByIdUseCase useCase)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseOrderListJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        [EndpointSummary("Rota para consultar todos pedidos.")]
        public async Task<IActionResult> GetAllOrders(
            [FromServices] IGetAllOrdersUseCase useCase)
        {
            var response = await useCase.Execute(); 

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseOrderJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [EndpointSummary("Rota para atualizar um pedido por id.")]
        public async Task<IActionResult> UpdateOrder(
            [FromRoute] Guid id,
            [FromBody] RequestOrderJson request,
            [FromServices] IUpdateOrderUseCase useCase)
        {
            var response = await useCase.Execute(request, id);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [EndpointSummary("Rota para deletar um pedido por id.")]
        public async Task<IActionResult> DeleteOrder(
            [FromRoute] Guid id,
            [FromServices] IDeleteOrderUseCase useCase)
        {
            await useCase.Execute(id);

            return NoContent();
        }
    }
}
