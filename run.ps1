# Run the container
docker stop quizformsapp
docker rm quizformsapp
docker run -d -p 5000:5000 --name quizformsapp quizforms

# (optional) list active containers
docker ps

# Open with browser on:
# http://localhost:5000/