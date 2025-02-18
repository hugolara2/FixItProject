namespace FixIt.Api;

public class TicketList {
	private List<Tickets> _ticketsList;

	public TicketList() {
		_ticketsList = new List<Tickets>();
	}

	public List<Tickets> AddTicket(Tickets ticket) {
		_ticketsList.Add(ticket);
		var listOfTickets = _ticketsList;
		return listOfTickets;
	}
}