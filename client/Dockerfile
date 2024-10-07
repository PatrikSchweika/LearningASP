FROM node:20-alpine
WORKDIR /app
EXPOSE 3000

ARG VITE_API_URL
ENV VITE_API_URL=$VITE_API_URL

COPY ["client/package.json", "."]

RUN npm install
RUN npm i -g serve

COPY ["client", "."]

RUN npm run build

CMD [ "serve", "-s", "dist" ]