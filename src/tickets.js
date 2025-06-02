class Tickets {
   constructor() {
      this.arr = []
   }

   saveNewTicket(newTicket) {
      this.arr.push(newTicket);
   }

   showTickets(index) {
      let value = this.arr[index].toString();
   }
}

module.exports = Tickets;