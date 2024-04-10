# React + Vite

## Prerequisites
```bash
$ node -v
v20.11.1
$ npm -v
10.2.4
```

## Run CMD
```bash
npm create vite@latest
```
- Use a suitable name for the app (Here it's `quiz-app` ) 
- Select `react`
- Select `Javascript`
- Copy and Replace All the files here

```bash
cd quiz-app/
npm install
npm install react-router-dom
npm run dev
```

## Run using Docker:

```bash
docker build -t infiniteloop/powerzoofrontend .
docker container run -p 9000:9000 infiniteloop/powerzoofrontend 
```