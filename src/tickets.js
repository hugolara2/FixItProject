class Tickets {
   constructor() {
      this.arr = []
   }

   saveNewTicket(newTicket) {
      this.arr.push(newTicket);
   }
}

module.exports = Tickets;