# Build the Docker container
docker build . -t quizforms

# Run the container
docker stop quizformsapp
docker rm quizformsapp
docker run -d -p 5000:5000 --name quizformsapp quizforms

# TIP:
# Check status with: docker ps
# http://localhost:5000/