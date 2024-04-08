# Use an official Node.js image as the base image
FROM node:21-alpine

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package.json .
COPY package-lock.json .

# Copy the rest of the application code to the working directory
COPY . .

# Install react-router-dom
RUN npm install react-router-dom

# Expose port 3000 to the outside world
EXPOSE 5173

# Run the npm run dev command to start the development server
CMD ["npm", "run", "dev", "--", "--host", "0.0.0.0"]
