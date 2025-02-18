using FixIt.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FixIt.Api.Controllers;

[Route("api/[controller]")]
public class TicketsController : ControllerBase {
	private TicketList _ticketsList = new TicketList();
	
	[HttpPost]
	public List<Tickets> AddTickets([FromBody] TicketDto ticket) {
		var tickets = new Tickets(ticket.User, ticket.Description, ticket.Level);
		var listOfTicket = _ticketsList.AddTicket(tickets);
		return listOfTicket;
	}
	
}