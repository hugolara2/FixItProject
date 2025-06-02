const express = require('express');
const app = express();
const port = 3000;
const Tickets = require('./src/tickets')

tickets = new Tickets();

app.use(express.json());

app.post('/NewTicket', (req, res) => {
   if (!req.body) {
      res.status(400).send('Bad Request');
   } else {
      tickets.saveNewTicket(req.body);
      return res.status(201).json({
         status: "success",
         message: "Ticket created",
      });
   }
});

app.get('/tickets', (req, res) => {
   let response = tickets.showTickets(0);
   res.send(response).status(200);
});

app.listen(port, () => {
   console.log(`Server listening on http://localhost:${port}`);
});