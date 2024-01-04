FROM node:16-alpine
WORKDIR /app
COPY package*.json ./
COPY . .

RUN npm install
RUN npm run build
RUN npm install -g serve
EXPOSE 3000

CMD ["serve","-p","3000","build/"]