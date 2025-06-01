const express = require('express');
const app = express();
const port = 3000;
const Tickets = require('./src/tickets')

tickets = new Tickets();

app.get('/', (req, res) => {
   res.header('Content-Type: headers');
   res.send('Hello World');
});

app.post('/NewTicket', (req, res) => {
   tickets.saveNewTicket(req.body);
   res.send('Ok');
})

app.listen(port, () => {
   console.log(`Server listening on http://localhost:${port}`);
});