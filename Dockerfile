FROM node:22-alpine

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install

RUN npm -g install nodemon

COPY . .

EXPOSE 3000

CMD ["node", "index.js"]